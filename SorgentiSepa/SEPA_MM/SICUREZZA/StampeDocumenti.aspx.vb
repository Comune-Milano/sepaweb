Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class SICUREZZA_StampeDocumenti
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            idIntervento.Value = Request.QueryString("IDINTERV")
            tipoIntervento.Value = Request.QueryString("TIPO")
            statoIntervento.Value = Request.QueryString("ST")

            Select Case tipoIntervento.Value
                Case "1", "2", "5"
                    If statoIntervento.Value = "4" Then
                        pdfChiusFlagranza()
                    Else
                        pdfAperFlagranza()
                    End If
                Case "3"
                    pdfChiusFlagranza()
                Case "4"
                    pdfAperFlagranza()
            End Select
        End If
    End Sub

    Private Function pdfModuloRelazione(ByVal contenuto As String) As String
        'Try
        '    connData.apri()
        'Dim sr1 As StreamReader

        'Select Case tipoIntervento.Value
        '    Case "1"
        '        sr1 = New StreamReader(Server.MapPath("Documenti\FLAGRANZA_SCHEDA GRUPPO TUTELA DEL PATRIMONIO CHIUSURA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        '    Case "2"
        '        sr1 = New StreamReader(Server.MapPath("Documenti\PROGRAMMATO_SCHEDA GRUPPO TUTELA DEL PATRIMONIO CHIUSURA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        '    Case "3"
        '        sr1 = New StreamReader(Server.MapPath("Documenti\SERVIZI_SCHEDA GRUPPO TUTELA DEL PATRIMONIO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        'End Select


        'Dim contenuto As String = sr1.ReadToEnd()
        'sr1.Close()

        Dim idUnita As Long = 0
        Dim idContr As Long = 0
        Dim modVerifica As Long = 0
        Dim statoContr As Integer = 0
        Dim statoUI As String = ""
        par.cmd.CommandText = "select interventi_sicurezza.*,segnalazioni.cognome_rs ||' '||segnalazioni.nome as soggettoChiamante,segnalazioni.id_Contratto,DESCRIZIONE_RIC,tipo_intervento.descrizione as tipo_interv from siscom_mi.interventi_sicurezza,siscom_mi.segnalazioni,siscom_mi.tipo_intervento where tipo_intervento.id=interventi_sicurezza.id_tipo_intervento and interventi_sicurezza.id_segnalazione=segnalazioni.id and interventi_sicurezza.id=" & idIntervento.Value
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If myReader.Read Then
            idUnita = par.IfNull(myReader("ID_UNITA"), 0)

            contenuto = Replace(contenuto, "$dataApertura$", par.FormattaData(par.IfNull(myReader("DATA_APERTURA"), "")))
            contenuto = Replace(contenuto, "$descrizioneSegnalaz$", par.IfNull(myReader("DESCRIZIONE_RIC"), ""))
            modVerifica = par.IfNull(myReader("ID_MOD_VERIFICA"), 0)
            statoContr = par.IfNull(myReader("ID_NEW_STATO_CONTR_NUCLEO"), 0)

            Select Case par.IfNull(myReader("ID_NEW_STATO_UI"), -1)
                Case 1
                    statoUI = "Libero"
                Case 0
                    statoUI = "Occupato"
            End Select
            contenuto = Replace(contenuto, "$assegnatario$", par.IfNull(myReader("assegnatario"), ""))
            If par.IfNull(myReader("assegnatario_2"), "") <> "" Then
                contenuto = Replace(contenuto, "$assegnatario2$", ", " & par.IfNull(myReader("assegnatario_2"), ""))
            Else
                contenuto = Replace(contenuto, "$assegnatario2$", "")
            End If
            contenuto = Replace(contenuto, "$tipoIntervento$", par.IfNull(myReader("tipo_interv"), ""))
            contenuto = Replace(contenuto, "$soggettoChiamante$", par.IfNull(myReader("soggettoChiamante"), ""))
            contenuto = Replace(contenuto, "$descrizioneIntervento$", par.IfNull(myReader("descrizione_interv"), ""))
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT rapporti_utenza.*,siscom_mi.getintestatari(id) as intest,(select descrizione from siscom_mi.TIPOLOGIA_CONTRATTO_LOCAZIONE where cod_tipologia_contr_loc=TIPOLOGIA_CONTRATTO_LOCAZIONE.cod) as tipoContr FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza WHERE UNITA_CONTRATTUALE.id_contratto=rapporti_utenza.id and id_unita = " & idUnita & " and rapporti_utenza.id = (siscom_mi.getultimoru(id_unita)) "
        Dim lettoreIdC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreIdC.Read Then
            idContr = par.IfNull(lettoreIdC("id"), 0)
            contenuto = Replace(contenuto, "$dataDecorrenza$", par.FormattaData(par.IfNull(lettoreIdC("data_decorrenza"), "")))
            contenuto = Replace(contenuto, "$dataDisdetta$", par.FormattaData(par.IfNull(lettoreIdC("data_disdetta_locatario"), "")))
            contenuto = Replace(contenuto, "$nominativoIntest$", par.IfNull(lettoreIdC("intest"), ""))
            contenuto = Replace(contenuto, "$tipoContratto$", par.IfNull(lettoreIdC("tipoContr"), ""))
        End If
        lettoreIdC.Close()

        par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.cod_unita_immobiliare, UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA, " _
                                & "INDIRIZZI.DESCRIZIONE as nomeVia,INDIRIZZI.CIVICO,TIPO_DISPONIBILITA.DESCRIZIONE AS STATO_ALL,INDIRIZZI.localita as luogo " _
                                & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.INDIRIZZI,siscom_mi.TIPO_DISPONIBILITA " _
                                & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                                & "and TIPO_DISPONIBILITA.COD(+)=COD_TIPO_DISPONIBILITA AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                                & "AND UNITA_IMMOBILIARI.ID = " & idUnita
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("interno"), ""))
            contenuto = Replace(contenuto, "$nomeVia$", par.IfNull(myReader("nomeVia"), ""))
            contenuto = Replace(contenuto, "$numCivico$", par.IfNull(myReader("civico"), ""))
            contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("scala"), ""))
            contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("piano"), ""))
            contenuto = Replace(contenuto, "$statoUI$", par.IfNull(myReader("STATO_ALL"), ""))
            contenuto = Replace(contenuto, "$codiceUI$", par.IfNull(myReader("cod_unita_immobiliare"), ""))
            contenuto = Replace(contenuto, "$luogo$", par.IfNull(myReader("luogo"), ""))
        End If
        myReader.Close()

        par.cmd.CommandText = "select tab_filiali.* from siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where " _
            & " unita_immobiliari.id=" & idUnita & " and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            contenuto = Replace(contenuto, "$sedeTerritoriale$", par.IfNull(myReader("NOME"), ""))
        End If
        myReader.Close()

        Dim tblCompon As String = ""

        Dim numComp As Integer = 0

        par.cmd.CommandText = "SELECT ANAGRAFICA_SOGG_COINVOLTI.id,COD_FISC_SOGG_COINVOLTO,SESSO_SOGG_COINVOLTO, COD_LUOGO_NASCITA,(SELECT nome FROM comuni_nazioni where cod=COD_LUOGO_NASCITA) AS luogo_nasc, COD_TIPOLOGIA_OCCUPANTE, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_OCCUPANTE_SOGG_COINV where id=cod_tipologia_occupante) AS occupante," _
           & " COGNOME_SOGG_COINVOLTO, TO_CHAR(TO_DATE(SUBSTR(DATA_NASC_SOGG_COINVOLTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_NASC_SOGG_COINVOLTO, " _
           & " INDIRIZZO_RESIDENZA, NOME_SOGG_COINVOLTO" _
           & " FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA WHERE " _
           & " ANAGRAFICA_SOGG_COINVOLTI.id=ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_SOGG_COINVOLTO asc"
        Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt3 As New Data.DataTable
        da3.Fill(dt3)
        da3.Dispose()
        If dt3.Rows.Count > 0 Then

            For Each row As Data.DataRow In dt3.Rows
                numComp = numComp + 1

                tblCompon = tblCompon & "" & numComp & "." _
                    & " " & par.IfNull(row.Item("COGNOME_SOGG_COINVOLTO"), "") & " " & par.IfNull(row.Item("NOME_SOGG_COINVOLTO"), "") & "" _
                    & " - " & par.FormattaData(par.IfNull(row.Item("DATA_NASC_SOGG_COINVOLTO"), "")) & "" _
                    & " - " & par.IfNull(row.Item("LUOGO_NASC"), "") & "" _
                    & " - " & par.IfNull(row.Item("OCCUPANTE"), "") & "<br />"
            Next

        End If


        contenuto = Replace(contenuto, "$occupanti$", tblCompon)


        '**** Cerco componenti
        Dim tblCompon2 As String = ""
        par.cmd.CommandText = "SELECT ANAGRAFICA.*,TIPOLOGIA_PARENTELA.DESCRIZIONE AS PARENTE FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_PARENTELA,SISCOM_MI.SOGGETTI_CONTRATTUALI" _
            & " WHERE SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_PARENTELA = TIPOLOGIA_PARENTELA.COD AND ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = " & idContr _
            & "AND NVL(SOGGETTI_CONTRATTUALI.DATA_FINE,'29991231')>'" & Format(Now, "yyyyMMdd") & "'"

        Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt4 As New Data.DataTable
        da4.Fill(dt4)
        da4.Dispose()
        If dt4.Rows.Count > 0 Then
            For Each row As Data.DataRow In dt4.Rows
                tblCompon2 = tblCompon2 & "" _
                    & " " & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "" _
                    & " - " & par.FormattaData(par.IfNull(row.Item("DATA_NASCITA"), "")) & "" _
                    & " - " & par.IfNull(row.Item("COD_FISCALE"), "") & "" _
                    & " - " & par.IfNull(row.Item("PARENTE"), "") & "<br />"
            Next
        End If
        contenuto = Replace(contenuto, "$elencoComponenti$", tblCompon2)

        par.cmd.CommandText = "select TAB_STATI_ALLOGGIO_ARRIVO.ID, TAB_STATI_ALLOGGIO_ARRIVO.descrizione as statoAllArrivo from siscom_mi.interventi_sicurezza,siscom_mi.TAB_STATI_ALLOGGIO_ARRIVO where interventi_sicurezza.id=" & idIntervento.Value & " " _
                & " and interventi_sicurezza.id_Stato_alloggio_arrivo=TAB_STATI_ALLOGGIO_ARRIVO.id"
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            contenuto = Replace(contenuto, "$statoAlloggio$", par.IfNull(myReader("statoAllArrivo"), ""))
        Else
            contenuto = Replace(contenuto, "$statoAlloggio$", "")
        End If
        myReader.Close()

        par.cmd.CommandText = "select MOTIVI_RECUPERO_SICUREZZA.ID, MOTIVI_RECUPERO_SICUREZZA.descrizione as motivoRecupero from siscom_mi.interventi_sicurezza,siscom_mi.MOTIVI_RECUPERO_SICUREZZA where interventi_sicurezza.id=" & idIntervento.Value & " " _
                & " and interventi_sicurezza.id_motivo_recupero=MOTIVI_RECUPERO_SICUREZZA.id"
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            contenuto = Replace(contenuto, "$motivoRecupero$", par.IfNull(myReader("motivoRecupero"), ""))
        Else
            contenuto = Replace(contenuto, "$motivoRecupero$", "")
        End If
        myReader.Close()

        Dim ospiti As String = ""
        par.cmd.CommandText = "SELECT * FROM siscom_mi.OSPITI,siscom_mi.RAPPORTI_UTENZA WHERE OSPITI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.ID=" & idContr
        myReader = par.cmd.ExecuteReader
        While myReader.Read
            ospiti += par.IfNull(myReader("NOMINATIVO"), "") & "<br/>"
        End While
        myReader.Close()

        contenuto = Replace(contenuto, "$elencoOspiti$", ospiti)

        Dim tblEnte As String = ""
        par.cmd.CommandText = "SELECT ANAGRAFICA_ENTI_COINVOLTI.id,ID_TIPO_ENTE,(select descrizione from siscom_mi.TIPI_ENTI_COINVOLTI where TIPI_ENTI_COINVOLTI.id=ID_TIPO_ENTE) as DESCRIZIONE_ENTE," _
               & " COGNOME_ENTE_COINVOLTO, NOME_ENTE_COINVOLTO, " _
               & " RUOLO, EMAIL," _
               & " TELEFONO" _
               & " FROM SISCOM_MI.ANAGRAFICA_ENTI_COINVOLTI,SISCOM_MI.ELENCO_ENTI_COINVOLTI WHERE ANAGRAFICA_ENTI_COINVOLTI.id=ELENCO_ENTI_COINVOLTI.ID_ANAGR_ENTE_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_ENTE_COINVOLTO asc"
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        da.Dispose()
        If dt.Rows.Count > 0 Then
            For Each row As Data.DataRow In dt.Rows
                tblEnte = tblEnte & "" _
                    & " " & par.IfNull(row.Item("COGNOME_ENTE_COINVOLTO"), "") & " " & par.IfNull(row.Item("NOME_ENTE_COINVOLTO"), "") & "" _
                    & " " & par.IfNull(row.Item("RUOLO"), "") & "" _
                    & " " & par.IfNull(row.Item("DESCRIZIONE_ENTE"), "") & "<br/>"
            Next
        End If

        contenuto = Replace(contenuto, "$entiCoinvolti$", tblEnte)

        Dim infoChiusura As String = ""

        If statoUI <> "" Then
            infoChiusura = "Nuovo stato UI: " & statoUI & " <br />"
        End If

        par.cmd.CommandText = "select * from siscom_mi.TAB_STATI_CONTRATTUALE_NUCLEO where id= " & statoContr
        myReader = par.cmd.ExecuteReader
        If myReader.Read Then
            infoChiusura += "Nuovo stato contratt. nucleo: " & par.IfNull(myReader("descrizione"), "") & "<br/>"
        End If
        myReader.Close()


        par.cmd.CommandText = "select tipi_Servizio_intervento.descrizione as tipoInterv from SISCOM_MI.INTERVENTI_VOCI_SERVIZIO,siscom_mi.tipi_Servizio_intervento where INTERVENTI_VOCI_SERVIZIO.id_servizio=tipi_Servizio_intervento.id and id_intervento=" & idIntervento.Value
        Dim daS As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dtS As New Data.DataTable
        daS.Fill(dtS)
        daS.Dispose()
        If dtS.Rows.Count > 0 Then
            infoChiusura += "Servizi: "
            For Each row As Data.DataRow In dtS.Rows
                infoChiusura += par.IfNull(row.Item("tipoInterv"), "") & ", "
            Next
            infoChiusura = Mid(infoChiusura, 1, Len(infoChiusura) - 2)
        End If


        contenuto = Replace(contenuto, "$informazioniChiusura$", infoChiusura)


        'Dim url As String = Server.MapPath("..\FileTemp\")
        'Dim pdfConverter1 As PdfConverter = New PdfConverter

        'Me.SettaPdf(pdfConverter1)

        'Dim nomefile As String = ""
        'Select Case tipoIntervento.Value
        '    Case "1"
        '        nomefile = "CHIUS_FLAGR_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
        '    Case "2"
        '        nomefile = "CHIUS_PROGR_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
        '    Case "3"
        '        nomefile = "INTERV_SERV_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
        'End Select

        'pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))

        'Me.ZippaFiles(nomefile)
        'Response.Redirect("..\ALLEGATI\SICUREZZA\" & nomefile & ".zip", False)

        'connData.chiudi()

        'Catch ex As Exception
        '    connData.chiudi()
        '    Session.Add("ERRORE", "Provenienza: Sicurezza - StampeDocumenti - pdfAperFlagranza - " & ex.Message)
        '    Response.Redirect("../Errore.aspx", False)
        'End Try

        Return contenuto

    End Function

    Private Sub pdfAperFlagranza()
        Try
            connData.apri()
            Dim sr1 As StreamReader

            Select Case tipoIntervento.Value
                Case "1"
                    sr1 = New StreamReader(Server.MapPath("Documenti\FLAGRANZA_SCHEDA GRUPPO TUTELA DEL PATRIMONIO APERTURA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Case "2", "5"
                    sr1 = New StreamReader(Server.MapPath("Documenti\PROGRAMMATO_SCHEDA GRUPPO TUTELA DEL PATRIMONIO APERTURA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Case "4"
                    sr1 = New StreamReader(Server.MapPath("Documenti\VERIFICA_SCHEDA_GRUPPO TUTELA PATRIMONIO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            End Select

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim idUnita As Long = 0
            Dim idContr As Long = 0
            Dim modVerifica As Long = 0
            Dim statoContr As Integer = 0
            par.cmd.CommandText = "select interventi_sicurezza.*,segnalazioni.id_Contratto,DESCRIZIONE_RIC,tipo_intervento.descrizione as tipo_interv from siscom_mi.interventi_sicurezza,siscom_mi.segnalazioni,siscom_mi.tipo_intervento where tipo_intervento.id=interventi_sicurezza.id_tipo_intervento and interventi_sicurezza.id_segnalazione=segnalazioni.id and interventi_sicurezza.id=" & idIntervento.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                idUnita = par.IfNull(myReader("ID_UNITA"), 0)
                idContr = par.IfNull(myReader("ID_CONTRATTO"), 0)
                contenuto = Replace(contenuto, "$dataApertura$", par.FormattaData(par.IfNull(myReader("DATA_APERTURA"), "")))
                contenuto = Replace(contenuto, "$oraApertura$", Mid(Trim(par.IfNull(myReader("ORA_INIZIO_INTERVENTO"), "")), 1, 2) & ":" & Mid(Trim(par.IfNull(myReader("ORA_INIZIO_INTERVENTO"), "")), 3, 2))
                contenuto = Replace(contenuto, "$descrizioneSegnalaz$", par.IfNull(myReader("DESCRIZIONE_RIC"), ""))
                modVerifica = par.IfNull(myReader("ID_MOD_VERIFICA"), 0)
                statoContr = par.IfNull(myReader("ID_NEW_STATO_CONTR_NUCLEO"), 0)
                contenuto = Replace(contenuto, "$assegnatario$", par.IfNull(myReader("assegnatario"), ""))
                contenuto = Replace(contenuto, "$assegnatario2$", par.IfNull(myReader("assegnatario_2"), ""))
                contenuto = Replace(contenuto, "$tipoIntervento$", par.IfNull(myReader("tipo_interv"), ""))
                Select Case par.IfNull(myReader("id_richiedente"), "")
                    Case "1"
                        contenuto = Replace(contenuto, "$richiedente1$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "2"
                        contenuto = Replace(contenuto, "$richiedente2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "3"
                        contenuto = Replace(contenuto, "$richiedente3$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "4"
                        contenuto = Replace(contenuto, "$richiedente4$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "5"
                        contenuto = Replace(contenuto, "$richiedente5$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "6"
                        contenuto = Replace(contenuto, "$richiedente6$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End Select
            End If
            myReader.Close()

            If idContr = 0 Then
                par.cmd.CommandText = "SELECT rapporti_utenza.* FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza WHERE UNITA_CONTRATTUALE.id_contratto=rapporti_utenza.id and id_contratto = " & idContr & " "
                Dim lettoreIdC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreIdC.Read Then
                    contenuto = Replace(contenuto, "$dataDecorrenza$", par.FormattaData(par.IfNull(lettoreIdC("data_decorrenza"), "")))
                    contenuto = Replace(contenuto, "$dataDisdetta$", par.FormattaData(par.IfNull(lettoreIdC("data_disdetta_locatario"), "")))
                End If
                lettoreIdC.Close()
            End If

            contenuto = Replace(contenuto, "$richiedente1$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$richiedente2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$richiedente3$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$richiedente4$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$richiedente5$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$richiedente6$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            par.cmd.CommandText = "select * from siscom_mi.MODALITA_VERIFICA_SICUREZZA where id= " & modVerifica
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$modalitaVerifica$", par.IfNull(myReader("descrizione"), ""))
            Else
                contenuto = Replace(contenuto, "$modalitaVerifica$", "")
            End If
            myReader.Close()


            par.cmd.CommandText = "select * from siscom_mi.TAB_STATI_CONTRATTUALE_NUCLEO where id= " & statoContr
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$statoAllVer$", par.IfNull(myReader("descrizione"), ""))
            Else
                contenuto = Replace(contenuto, "$statoAllVer$", "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.cod_unita_immobiliare, UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA, " _
                & "INDIRIZZI.DESCRIZIONE as nomeVia,INDIRIZZI.CIVICO,TIPO_DISPONIBILITA.DESCRIZIONE AS STATO_ALL " _
                & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.INDIRIZZI,siscom_mi.TIPO_DISPONIBILITA " _
                & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                & "and TIPO_DISPONIBILITA.COD(+)=COD_TIPO_DISPONIBILITA AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                & "AND UNITA_IMMOBILIARI.ID = " & idUnita
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("interno"), ""))
                contenuto = Replace(contenuto, "$nomeVia$", par.IfNull(myReader("nomeVia"), ""))
                contenuto = Replace(contenuto, "$numCivico$", par.IfNull(myReader("civico"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("scala"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("piano"), ""))
                contenuto = Replace(contenuto, "$statoUI$", par.IfNull(myReader("STATO_ALL"), ""))
                contenuto = Replace(contenuto, "$codiceUI$", par.IfNull(myReader("cod_unita_immobiliare"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "select tab_filiali.* from siscom_mi.tab_filiali,siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.unita_immobiliari where " _
                & " unita_immobiliari.id=" & idUnita & " and edifici.id=unita_immobiliari.id_edificio and complessi_immobiliari.id=edifici.id_complesso and tab_filiali.id=complessi_immobiliari.id_filiale "
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$sedeTerritoriale$", par.IfNull(myReader("NOME"), ""))
            End If
            myReader.Close()

            'Dim tblCompon As String = ""

            'Dim numComp As Integer = 0

            'par.cmd.CommandText = "SELECT ANAGRAFICA_SOGG_COINVOLTI.id,COD_FISC_SOGG_COINVOLTO,SESSO_SOGG_COINVOLTO, COD_LUOGO_NASCITA,(SELECT nome FROM comuni_nazioni where cod=COD_LUOGO_NASCITA) AS luogo_nasc, COD_TIPOLOGIA_OCCUPANTE, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_OCCUPANTE_SOGG_COINV where id=cod_tipologia_occupante) AS occupante," _
            '   & " COGNOME_SOGG_COINVOLTO, TO_CHAR(TO_DATE(SUBSTR(DATA_NASC_SOGG_COINVOLTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_NASC_SOGG_COINVOLTO, " _
            '   & " INDIRIZZO_RESIDENZA, NOME_SOGG_COINVOLTO" _
            '   & " FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA WHERE " _
            '   & " ANAGRAFICA_SOGG_COINVOLTI.id=ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_SOGG_COINVOLTO asc"
            'Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt3 As New Data.DataTable
            'da3.Fill(dt3)
            'da3.Dispose()
            'If dt3.Rows.Count > 0 Then

            '    For Each row As Data.DataRow In dt3.Rows
            '        numComp = numComp + 1
            '        'tblCompon = tblCompon & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & numComp & "</td>" _
            '        '   & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(row.Item("COGNOME_SOGG_COINVOLTO"), "") & " " & par.IfNull(row.Item("NOME_SOGG_COINVOLTO"), "") & "</td>" _
            '        '   & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.FormattaData(par.IfNull(row.Item("DATA_NASC_SOGG_COINVOLTO"), "")) & "</td>" _
            '        '   & "<td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & par.IfNull(row.Item("luogo_nasc"), "") & "</td></tr>"

            '        tblCompon = tblCompon & "<tr><td class='MsoNormal'>" & numComp & "</td>" _
            '            & "<td class='MsoNormal' colspan='12'>" & par.IfNull(row.Item("COGNOME_SOGG_COINVOLTO"), "") & " " & par.IfNull(row.Item("NOME_SOGG_COINVOLTO"), "") & "</td>" _
            '            & "<td class='MsoNormal' colspan='4'>" & par.FormattaData(par.IfNull(row.Item("DATA_NASC_SOGG_COINVOLTO"), "")) & "</td>" _
            '            & "<td class='MsoNormal' colspan='4'>" & par.IfNull(row.Item("COD_FISC_SOGG_COINVOLTO"), "") & "</td>" _
            '            & "<td class='MsoNormal'colspan='4'>" & par.IfNull(row.Item("OCCUPANTE"), "") & "</td></tr>"
            '    Next

            'End If


            'contenuto = Replace(contenuto, "$occupanti$", tblCompon)


            contenuto = Replace(contenuto, "$zonacitta$", "- - -")

            contenuto = Replace(contenuto, "$flagCC$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flag112$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$flagMM$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flagCustode$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$flagComitato$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flagCittadino$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$flagChiave$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            Dim tblNote As String = ""
            par.cmd.CommandText = "select * from siscom_mi.interventi_note where id_intervento=" & idIntervento.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                tblNote = "<table width='100%'>"
                For Each elemento As Data.DataRow In dt.Rows
                    tblNote = tblNote & "<tr><td>" & par.IfNull(elemento.Item("note"), "") & "</td>" _
                        & "</tr>"
                Next
                tblNote = tblNote & "</table>"
            End If

            contenuto = Replace(contenuto, "$noteIntervento$", tblNote)

            If tipoIntervento.Value = "4" Then
                contenuto = pdfModuloRelazione(contenuto)
            End If

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            Select Case tipoIntervento.Value
                Case "1"
                    nomefile = "APER_FLAGR_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
                Case "2"
                    nomefile = "APER_PROGR_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
                Case "4"
                    nomefile = "INTER_VERIFICA_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
            End Select
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))

            Me.ZippaFiles(nomefile)
            ScriviAllegato(idIntervento.Value, nomefile & ".zip")
            Response.Redirect("..\ALLEGATI\SICUREZZA\" & nomefile & ".zip", False)

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - StampeDocumenti - pdfAperFlagranza - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub pdfChiusFlagranza()
        Try
            connData.apri()
            Dim sr1 As StreamReader
            Select Case tipoIntervento.Value
                Case "1"
                    sr1 = New StreamReader(Server.MapPath("Documenti\FLAGRANZA_SCHEDA GRUPPO TUTELA DEL PATRIMONIO CHIUSURA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Case "2"
                    sr1 = New StreamReader(Server.MapPath("Documenti\PROGRAMMATO_SCHEDA GRUPPO TUTELA DEL PATRIMONIO CHIUSURA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Case "3"
                    sr1 = New StreamReader(Server.MapPath("Documenti\SERVIZI_SCHEDA GRUPPO TUTELA DEL PATRIMONIO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Case "5"
                    sr1 = New StreamReader(Server.MapPath("Documenti\RECUPERO_AMMVO_SCHEDA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))

            End Select



            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim idUnita As Long = 0
            Dim idContr As Long = 0


            par.cmd.CommandText = "select interventi_sicurezza.*,DESCRIZIONE_RIC,segnalazioni.id_contratto from siscom_mi.interventi_sicurezza,siscom_mi.segnalazioni where interventi_sicurezza.id_segnalazione=segnalazioni.id and interventi_sicurezza.id=" & idIntervento.Value
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                idUnita = par.IfNull(myReader("ID_UNITA"), 0)
                idContr = par.IfNull(myReader("ID_CONTRATTO"), 0)
                contenuto = Replace(contenuto, "$dataApertura$", par.FormattaData(par.IfNull(myReader("DATA_APERTURA"), "")))
                contenuto = Replace(contenuto, "$oraApertura$", Mid(Trim(par.IfNull(myReader("ORA_INIZIO_INTERVENTO"), "")), 1, 2) & ":" & Mid(Trim(par.IfNull(myReader("ORA_INIZIO_INTERVENTO"), "")), 3, 2))

                If par.IfNull(myReader("FL_ABBANDONO_ALLOGGIO"), "0") = "1" Then
                    contenuto = Replace(contenuto, "$flSiNoAbbandono$", "<img src='block_SI_Checked.gif' alt='si' width='15' height='15' border='1'/> <img src='block_NO.gif' alt='checked' width='15' height='15' border='1' />")
                Else
                    contenuto = Replace(contenuto, "$flSiNoAbbandono$", "<img src='block_SI.gif' alt='si' width='15' height='15' border='1'/> <img src='block_NO_Checked.gif' alt='checked' width='15' height='15' border='1' />")
                End If

                If par.IfNull(myReader("FL_PROTOCOLLO_ATTIVO"), "0") = "1" Then
                    contenuto = Replace(contenuto, "$flNoProtocollo$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
                    contenuto = Replace(contenuto, "$flSiProtocollo$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                Else
                    contenuto = Replace(contenuto, "$flSiProtocollo$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
                    contenuto = Replace(contenuto, "$flNoProtocollo$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If

                If par.IfNull(myReader("ID_TIPO_SERV_SOCIALE"), "") = "13" Then
                    contenuto = Replace(contenuto, "$flAccettaProp$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If
                If par.IfNull(myReader("ID_TIPO_SERV_SOCIALE"), "") = "14" Then
                    contenuto = Replace(contenuto, "$flRifiutaProp$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If
                If par.IfNull(myReader("INGRESSO_ALLOGGIO"), "") = "1" Then
                    contenuto = Replace(contenuto, "$ingressoAll1$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If
                If par.IfNull(myReader("INGRESSO_ALLOGGIO"), "") = "2" Then
                    contenuto = Replace(contenuto, "$ingressoAll2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If

                If par.IfNull(myReader("ID_NEW_STATO_CONTR_NUCLEO"), "") = "2" Then
                    contenuto = Replace(contenuto, "$condizioneAll$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If
                If par.IfNull(myReader("ID_NEW_STATO_CONTR_NUCLEO"), "") = "3" Then
                    contenuto = Replace(contenuto, "$condizioneAll2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End If
            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$ingressoAll1$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$ingressoAll2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$flRifiutaProp$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flAccettaProp$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$condizioneAll$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$condizioneAll2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            Dim tblNote As String = ""
            par.cmd.CommandText = "select * from siscom_mi.interventi_note where id_intervento=" & idIntervento.Value
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                tblNote = "<table width='100%'>"
                For Each elemento As Data.DataRow In dt.Rows
                    tblNote = tblNote & "<tr><td>" & par.IfNull(elemento.Item("note"), "") & "</td>" _
                        & "</tr>"
                Next
                tblNote = tblNote & "</table>"
            End If

            contenuto = Replace(contenuto, "$noteIntervento$", tblNote)

            If idContr = 0 Then
                par.cmd.CommandText = "SELECT id_contratto,cod_contratto FROM siscom_mi.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza WHERE UNITA_CONTRATTUALE.id_contratto=rapporti_utenza.id and id_unita = " & idUnita & " and rapporti_utenza.id = (siscom_mi.getultimoru(id_unita))  "
                Dim lettoreIdC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreIdC.Read Then
                    idContr = par.IfNull(lettoreIdC("id_contratto"), 0)
                End If
                lettoreIdC.Close()
            End If


            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA, " _
                                    & "INDIRIZZI.DESCRIZIONE as nomeVia,INDIRIZZI.CIVICO " _
                                    & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.INDIRIZZI " _
                                    & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                                    & "AND UNITA_IMMOBILIARI.ID = " & idUnita
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$interno$", par.IfNull(myReader("interno"), ""))
                contenuto = Replace(contenuto, "$nomeVia$", par.IfNull(myReader("nomeVia"), ""))
                contenuto = Replace(contenuto, "$numCivico$", par.IfNull(myReader("civico"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("scala"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("piano"), ""))
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT siscom_mi.getstatocontratto(id) as StatoContr,cod_tipologia_contr_loc from siscom_mi.rapporti_utenza where id=" & idContr
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                Select Case par.IfNull(myReader("StatoContr"), 0)
                    Case "IN CORSO"
                        contenuto = Replace(contenuto, "$statoInCorso$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "CHIUSO"
                        contenuto = Replace(contenuto, "$statoChiuso$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "IN CORSO S.T."
                        contenuto = Replace(contenuto, "$statoST$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case Else
                        contenuto = Replace(contenuto, "$statoAltro$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End Select

                Select Case par.IfNull(myReader("cod_tipologia_contr_loc"), 0)
                    Case "ERP"
                        contenuto = Replace(contenuto, "$tipoErp$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "EQC392"
                        contenuto = Replace(contenuto, "$tipoEQC$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "L43198"
                        contenuto = Replace(contenuto, "$tipo431$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "NONE"
                        contenuto = Replace(contenuto, "$tipoAbusivo$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End Select

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$tipoErp$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$tipoEQC$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$tipo431$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$tipoAbusivo$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$statoInCorso$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$statoChiuso$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$statoST$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$statoAltro$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")



            par.cmd.CommandText = "select TAB_STATI_ALLOGGIO_ARRIVO.ID, TAB_STATI_ALLOGGIO_ARRIVO.descrizione as statoAllArrivo from siscom_mi.interventi_sicurezza,siscom_mi.TAB_STATI_ALLOGGIO_ARRIVO where interventi_sicurezza.id=" & idIntervento.Value & " " _
                & " and interventi_sicurezza.id_Stato_alloggio_arrivo=TAB_STATI_ALLOGGIO_ARRIVO.id"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                Select Case par.IfNull(myReader("ID"), 0)
                    Case "1"
                        contenuto = Replace(contenuto, "$statoAll1$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "2"
                        contenuto = Replace(contenuto, "$statoAll2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "3"
                        contenuto = Replace(contenuto, "$statoAll3$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    Case "4"
                        contenuto = Replace(contenuto, "$statoAll4$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                End Select

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$statoAll1$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$statoAll2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$statoAll3$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$statoAll4$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            par.cmd.CommandText = "SELECT ANAGRAFICA_ENTI_COINVOLTI.id,ID_TIPO_ENTE,(select descrizione from siscom_mi.TIPI_ENTI_COINVOLTI where TIPI_ENTI_COINVOLTI.id=ID_TIPO_ENTE) as DESCRIZIONE_ENTE," _
    & " COGNOME_ENTE_COINVOLTO, NOME_ENTE_COINVOLTO, " _
    & " RUOLO, EMAIL," _
    & " TELEFONO" _
    & " FROM SISCOM_MI.ANAGRAFICA_ENTI_COINVOLTI,SISCOM_MI.ELENCO_ENTI_COINVOLTI WHERE ANAGRAFICA_ENTI_COINVOLTI.id=ELENCO_ENTI_COINVOLTI.ID_ANAGR_ENTE_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_ENTE_COINVOLTO asc"

            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable
            da2.Fill(dt2)
            da2.Dispose()
            If dt2.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt2.Rows
                    Select Case par.IfNull(row.Item("ID_TIPO_ENTE"), 0)
                        Case "1"
                            contenuto = Replace(contenuto, "$flPoliziaLocale$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "2"
                            contenuto = Replace(contenuto, "$flPoliziaStato$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "3"
                            contenuto = Replace(contenuto, "$flCarabinieri$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "4"
                            contenuto = Replace(contenuto, "$flCortesi$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "5"
                            contenuto = Replace(contenuto, "$flServiziSociali$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "6"
                            contenuto = Replace(contenuto, "$fl118$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "7"
                            contenuto = Replace(contenuto, "$flA2Agas$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "8"
                            contenuto = Replace(contenuto, "$flA2Aluce$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "9"
                            contenuto = Replace(contenuto, "$flMedico$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "10"
                            contenuto = Replace(contenuto, "$flAllSystem$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "11"
                            contenuto = Replace(contenuto, "$flAltro$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    End Select
                Next
            End If

            contenuto = Replace(contenuto, "$flPoliziaLocale$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flPoliziaStato$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flCarabinieri$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flCortesi$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flServiziSociali$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$fl118$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flA2Agas$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flA2Aluce$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flMedico$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flAltro$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flAllSystem$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            Dim tblCompon As String = ""

            Dim numComp As Integer = 0

            par.cmd.CommandText = "SELECT ANAGRAFICA_SOGG_COINVOLTI.id,COD_FISC_SOGG_COINVOLTO,SESSO_SOGG_COINVOLTO, COD_LUOGO_NASCITA,(SELECT nome FROM comuni_nazioni where cod=COD_LUOGO_NASCITA) AS luogo_nasc, COD_TIPOLOGIA_OCCUPANTE, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_OCCUPANTE_SOGG_COINV where id=cod_tipologia_occupante) AS occupante," _
               & " COGNOME_SOGG_COINVOLTO, TO_CHAR(TO_DATE(SUBSTR(DATA_NASC_SOGG_COINVOLTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_NASC_SOGG_COINVOLTO, " _
               & " INDIRIZZO_RESIDENZA, NOME_SOGG_COINVOLTO" _
               & " FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA WHERE " _
               & " cod_tipologia_occupante=4 and ANAGRAFICA_SOGG_COINVOLTI.id=ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV and id_intervento=" & idIntervento.Value & " order by COGNOME_SOGG_COINVOLTO asc"
            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt3 As New Data.DataTable
            da3.Fill(dt3)
            da3.Dispose()
            If dt3.Rows.Count > 0 Then

                For Each row As Data.DataRow In dt3.Rows
                    numComp = numComp + 1
                    'tblCompon = tblCompon & "<tr><td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & numComp & "</td>" _
                    '   & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.IfNull(row.Item("COGNOME_SOGG_COINVOLTO"), "") & " " & par.IfNull(row.Item("NOME_SOGG_COINVOLTO"), "") & "</td>" _
                    '   & "<td align='left' style='border-width: 1px; border-color: #000000; border-style: none solid solid solid;'>" & par.FormattaData(par.IfNull(row.Item("DATA_NASC_SOGG_COINVOLTO"), "")) & "</td>" _
                    '   & "<td align='left' style='border-bottom-style: solid; border-bottom-width: 1px;border-bottom-color: #000000'>" & par.IfNull(row.Item("luogo_nasc"), "") & "</td></tr>"

                    tblCompon = tblCompon & "<tr><td class='MsoNormal'>" & numComp & "</td>" _
                        & "<td class='MsoNormal' colspan='8'>" & par.IfNull(row.Item("COGNOME_SOGG_COINVOLTO"), "") & " " & par.IfNull(row.Item("NOME_SOGG_COINVOLTO"), "") & "</td>" _
                        & "<td class='MsoNormal' colspan='4'>" & par.FormattaData(par.IfNull(row.Item("DATA_NASC_SOGG_COINVOLTO"), "")) & "</td>" _
                        & "<td class='MsoNormal' colspan='8'>" & par.IfNull(row.Item("luogo_nasc"), "") & "</td></tr>"

                Next

            End If


            contenuto = Replace(contenuto, "$occupantiAbusivi$", tblCompon)



            par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_TIPO_ENTE_GAS where id_intervento=" & idIntervento.Value
            Dim da4 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt4 As New Data.DataTable
            da4.Fill(dt4)
            da4.Dispose()
            If dt4.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt4.Rows
                    Select Case par.IfNull(row.Item("ID_TIPO_INTERVENTO_ENTE"), 0)
                        Case "11"
                            contenuto = Replace(contenuto, "$flDistaccoLinea1$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "12"
                            contenuto = Replace(contenuto, "$flRimContatore1$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "15"
                            contenuto = Replace(contenuto, "$flNonInterv1$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    End Select
                Next
            End If

            contenuto = Replace(contenuto, "$flNonInterv1$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flDistaccoLinea1$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flRimContatore1$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_TIPO_ENTE_LUCE where id_intervento=" & idIntervento.Value
            Dim da5 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt5 As New Data.DataTable
            da5.Fill(dt5)
            da5.Dispose()
            If dt5.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt5.Rows
                    Select Case par.IfNull(row.Item("ID_TIPO_INTERVENTO_ENTE"), 0)
                        Case "11"
                            contenuto = Replace(contenuto, "$flDistaccoLinea2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "12"
                            contenuto = Replace(contenuto, "$flRimContatore2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "15"
                            contenuto = Replace(contenuto, "$flNonInterv2$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    End Select
                Next
            End If



            contenuto = Replace(contenuto, "$flNonInterv2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flDistaccoLinea2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flRimContatore2$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            par.cmd.CommandText = "select * from siscom_mi.INTERVENTI_TIPO_IN_SICUREZZA where id_intervento=" & idIntervento.Value
            Dim da6 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt6 As New Data.DataTable
            da6.Fill(dt6)
            da6.Dispose()
            If dt6.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt6.Rows
                    Select Case par.IfNull(row.Item("ID_TIPO_MESSO_IN_SICUREZZA"), 0)
                        Case "1"
                            contenuto = Replace(contenuto, "$flPortaBlindata$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "2"
                            contenuto = Replace(contenuto, "$flSostCilindro$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "3"
                            contenuto = Replace(contenuto, "$flLastraturaPorta$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "4"
                            contenuto = Replace(contenuto, "$flLastraturaFinestre$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "5"
                            contenuto = Replace(contenuto, "$flTraslocoInt$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "6"
                            contenuto = Replace(contenuto, "$flSgomberoInt$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                    End Select
                Next
            End If



            contenuto = Replace(contenuto, "$flPortaBlindata$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flSostCilindro$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flLastraturaPorta$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flLastraturaFinestre$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flTraslocoInt$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flSgomberoInt$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")

            contenuto = Replace(contenuto, "$flDecessoIntest$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flIntestEmigrato$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            par.cmd.CommandText = "select * from SISCOM_MI.INTERVENTI_TIPO_SERVIZIO where id_intervento=" & idIntervento.Value
            Dim da7 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt7 As New Data.DataTable
            da7.Fill(dt7)
            da7.Dispose()
            If dt7.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt7.Rows
                    Select Case par.IfNull(row.Item("ID_TIPO_INTERVENTO_SERVIZIO"), 0)
                        Case "12"
                            contenuto = Replace(contenuto, "$flDistaccoCont$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "9"
                            contenuto = Replace(contenuto, "$flTrasloco$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")
                        Case "1"
                            contenuto = Replace(contenuto, "$flPortaBlindata0$", "<img src='block_checked.gif' alt='no' width='10' height='10' border='1' />")

                    End Select
                Next
            End If

            contenuto = Replace(contenuto, "$flDistaccoCont$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flTrasloco$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")
            contenuto = Replace(contenuto, "$flPortaBlindata0$", "<img src='block.gif' alt='no' width='10' height='10' border='1' />")


            contenuto = pdfModuloRelazione(contenuto)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Me.SettaPdf(pdfConverter1)

            Dim nomefile As String = ""
            Select Case tipoIntervento.Value
                Case "1"
                    nomefile = "CHIUS_FLAGR_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
                Case "2"
                    nomefile = "CHIUS_PROGR_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
                Case "3"
                    nomefile = "INTERV_SERV_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
                Case "5"
                    nomefile = "RECUPER_AMMVO_" & Request.QueryString("IDINTERV") & "-" & Format(Now, "yyyyMMddHHmmss")
            End Select

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))

            Me.ZippaFiles(nomefile)
            ScriviAllegato(idIntervento.Value, nomefile & ".zip")
            Response.Redirect("..\ALLEGATI\SICUREZZA\" & nomefile & ".zip", False)

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - StampeDocumenti - pdfChiusFlagranza - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Private Sub ScriviAllegato(ByVal idInterv As Integer, ByVal nomeFile As String)
        Dim oraAllegato As String = Format(Now, "yyyyMMddHHmmss")

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_INTERVENTI (ID,NOME_FILE,ID_INTERVENTO,DATA_ORA,DESCRIZIONE) VALUES " _
                                        & "(SISCOM_MI.SEQ_ALLEGATI_INTERVENTI.NEXTVAL,'" & par.PulisciStrSql(nomeFile) & "'," & idIntervento.Value & "," _
                                                & "'" & oraAllegato & "', 'Stampa Verbale')"
        par.cmd.ExecuteNonQuery()
    End Sub

    Private Sub SettaPdf(ByVal pdf As PdfConverter)
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
        If Licenza <> "" Then
            pdf.LicenseKey = Licenza
        End If

        pdf.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdf.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        pdf.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdf.PdfDocumentOptions.ShowHeader = False
        pdf.PdfDocumentOptions.ShowFooter = False
        pdf.PdfDocumentOptions.LeftMargin = 20
        pdf.PdfDocumentOptions.RightMargin = 20
        pdf.PdfDocumentOptions.TopMargin = 30
        pdf.PdfDocumentOptions.BottomMargin = 10
        pdf.PdfDocumentOptions.GenerateSelectablePdf = True

        pdf.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
        pdf.PdfFooterOptions.DrawFooterLine = False


    End Sub

    Private Sub ZippaFiles(ByVal nomefile As String)
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\ALLEGATI\SICUREZZA\" & nomefile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\FileTemp\" & nomefile & ".pdf")
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '
        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        Dim sFile As String = Path.GetFileName(strFile)
        Dim theEntry As ZipEntry = New ZipEntry(sFile)
        Dim fi As New FileInfo(strFile)
        theEntry.DateTime = fi.LastWriteTime
        theEntry.Size = strmFile.Length
        strmFile.Close()
        objCrc32.Reset()
        objCrc32.Update(abyBuffer)
        theEntry.Crc = objCrc32.Value
        strmZipOutputStream.PutNextEntry(theEntry)
        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()

        File.Delete(strFile)
    End Sub

End Class
