Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class VSA_StampeRateizzStraord
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim idc As Long = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            Select Case Request.QueryString("TIPO")
                Case "EsNegativo"
                    RiempiDatagridBoll()
                    pdfEsitoNeg()
                Case "EsPositivo"
                    RiempiDatagridBoll()
                    RiempiGridRate()
                    pdfEsitoPosit()
            End Select
        End If
    End Sub

    Private Sub RiempiDatagridBoll()
        Try
            Dim num_bolletta As String = ""
            Dim I As Integer = 0
            Dim importobolletta As Decimal = 0
            Dim importopagato As Decimal = 0
            Dim residuo As Decimal = 0
            Dim morosita As Integer = 0
            Dim riclass As Integer = 0
            Dim indiceMorosita As Integer = 0
            Dim indiceBolletta As Integer = 0
            Dim storno As Integer = 0

            Dim condEmissione As String = ""
            Dim CondRiferimento As String = ""
            Dim CondIncasso As String = ""
            'Segn 2052/2018
            Dim CondRateizza As String = " and BOL_BOLLETTE.id_tipo NOT IN (22,25)  " _
                              & " and id_Bolletta_storno is null " _
                              & " and nvl(importo_ruolo,0)=0 " _
                              & " and nvl(importo_ingiunzione,0)=0  " _
                              & " And (FL_ANNULLATA = 0 Or (FL_ANNULLATA = 1 And IMPORTO_PAGATO Is Not NULL)) " _
                              & " And bol_bollette.id_bolletta_ric Is null " _
                              & " And bol_bollette.id_rateizzazione Is null "

          
            condEmissione = " and (bol_bollette.DATA_SCADENZA<= '20161231' or BOL_BOLLETTE.id_tipo=26 or BOL_BOLLETTE.id_tipo=27) "
            
            connData.apri()

            par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.*,BOL_BOLLETTE.NUM_BOLLETTA as NUM_BOLL" _
                & " from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) and bol_bollette.id_contratto in (select id from siscom_mi.rapporti_utenza where cod_contratto='" & Request.QueryString("NUMCONT") & "') " _
                & " and (bol_bollette.fl_annullata = 0 OR (bol_bollette.fl_annullata <> 0 AND data_pagamento IS NOT NULL)) " & " " & condEmissione & " " & CondRiferimento & " " & CondIncasso & " " & CondRateizza & " ORDER BY BOL_BOLLETTE.data_emissione DESC,RIFERIMENTO_DA DESC,RIFERIMENTO_A DESC,DATA_SCADENZA DESC"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery As New Data.DataTable
            dt1 = New Data.DataTable
            Dim rowDT As System.Data.DataRow

            dt1.Columns.Add("id")
            dt1.Columns.Add("num")
            dt1.Columns.Add("num_boll")
            dt1.Columns.Add("num_tipo")
            dt1.Columns.Add("riferimento_da")
            dt1.Columns.Add("riferimento_a")
            dt1.Columns.Add("data_emissione")
            dt1.Columns.Add("data_scadenza")
            dt1.Columns.Add("importo_totale")
            dt1.Columns.Add("importobolletta")
            dt1.Columns.Add("imp_pagato")
            dt1.Columns.Add("imp_residuo")
            dt1.Columns.Add("data_pagamento")
            dt1.Columns.Add("fl_mora")
            dt1.Columns.Add("fl_rateizz")
            dt1.Columns.Add("importo_ruolo")
            dt1.Columns.Add("imp_ruolo_pagato")
            dt1.Columns.Add("sgravio")
            dt1.Columns.Add("note")
            dt1.Columns.Add("id_tipo")

            da1.Fill(dtQuery)
            da1.Dispose()

            Dim TOTimportobolletta As Decimal = 0
            Dim TOTimportopagato As Decimal = 0
            Dim TOTimportoresiduo As Decimal = 0
            Dim TOTImpRuolo As Decimal = 0
            Dim TOTImpRuoloPag As Decimal = 0
            Dim TOTimportoEmesso As Decimal = 0
            Dim numero As Integer = 1


            For Each row As Data.DataRow In dtQuery.Rows

               

                indiceMorosita = 0
                indiceBolletta = 0
                rowDT = dt1.NewRow()
               

                Select Case par.IfNull(row.Item("n_rata"), "")
                    Case "99" 'bolletta manuale
                        num_bolletta = "MA"
                    Case "999" 'bolletta automatica
                        num_bolletta = "AU"
                    Case "99999" 'bolletta di conguaglio
                        num_bolletta = "CO"
                    Case Else
                        num_bolletta = Format(par.IfNull(row.Item("n_rata"), "??"), "00")
                End Select

                importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_B"), 0) '


                TOTimportobolletta = TOTimportobolletta + importobolletta


                importopagato = (par.IfNull(row.Item("IMPORTO_PAGATO"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), 0))
                TOTimportopagato = TOTimportopagato + importopagato

                Dim STATO As String = ""
                If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> "0" Then
                    STATO = "ANNUL."
                Else
                    STATO = "VALIDA"
                End If
                If par.IfNull(row.Item("id_bolletta_ric"), "0") <> "0" Or par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
                    STATO = "RICLA."
                    riclass = 1
                End If

                If par.IfNull(row.Item("id_bolletta_storno"), "0") <> "0" Then
                    STATO = "STORN."
                End If

                residuo = importobolletta - (importopagato + par.IfNull(row.Item("imp_ruolo_pagato"), 0))
                TOTimportoresiduo = TOTimportoresiduo + residuo


                rowDT.Item("NUM") = numero & ")"
                rowDT.Item("NUM_BOLL") = par.IfNull(row.Item("NUM_BOLL"), "")
                rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "")
                rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
                rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
                rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))
                rowDT.Item("data_scadenza") = par.FormattaData(par.IfNull(row.Item("data_scadenza"), ""))
                rowDT.Item("importo_totale") = Format(par.IfNull(row.Item("importo_totale"), 0), "##,##0.00")
                rowDT.Item("importobolletta") = Format(importobolletta, "##,##0.00")
                rowDT.Item("imp_pagato") = Format(importopagato, "##,##0.00")
                rowDT.Item("imp_residuo") = Format(residuo, "##,##0.00")
                rowDT.Item("data_pagamento") = par.FormattaData(par.IfNull(row.Item("data_pagamento"), ""))
                rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
                rowDT.Item("importo_ruolo") = Format(par.IfNull(row.Item("importo_ruolo"), 0), "##,##0.00")
                rowDT.Item("imp_ruolo_pagato") = Format(par.IfNull(row.Item("imp_ruolo_pagato"), 0), "##,##0.00")

                If par.IfNull(row.Item("imp_ruolo_pagato"), 0) > 0 Then
                    rowDT.Item("sgravio") = ImpostaFlSgravio(par.IfNull(row.Item("ID"), 0), CDec(par.IfNull(row.Item("IMPORTO_RUOLO"), 0)))
                Else
                    rowDT.Item("sgravio") = "N"
                End If

                TOTImpRuolo = TOTImpRuolo + rowDT.Item("importo_ruolo")
                TOTimportoEmesso = TOTimportoEmesso + rowDT.Item("importo_totale")
                TOTImpRuoloPag = TOTImpRuoloPag + rowDT.Item("imp_ruolo_pagato")

                If par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
                    rowDT.Item("fl_rateizz") = "SI"
                Else
                    rowDT.Item("fl_rateizz") = "NO"
                End If

                indiceMorosita = par.IfNull(row.Item("id_morosita"), 0)

                If indiceMorosita <> 0 Then
                    rowDT.Item("fl_mora") = "SI"
                Else
                    rowDT.Item("fl_mora") = "NO"
                End If

                rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)

                rowDT.Item("id") = par.IfNull(row.Item("id"), 0)

                If rowDT.Item("id_tipo") = "3" Or rowDT.Item("id_tipo") = "4" Then
                    morosita = 1
                End If

                Select Case par.IfNull(row.Item("id_tipo"), 0)
                    Case "3"
                        indiceBolletta = par.IfNull(row.Item("id"), 0)
                    Case "4"
                        indiceMorosita = par.IfNull(row.Item("id_morosita"), "")
                        indiceBolletta = 0
                    Case "5"
                        indiceBolletta = par.IfNull(row.Item("id"), 0)
                    Case "22"
                        storno = 1
                End Select

                If par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
                    indiceBolletta = par.IfNull(row.Item("id"), 0)
                End If

                If par.IfNull(row.Item("id_rateizzazione"), 0) <> 0 Then
                    indiceBolletta = par.IfNull(row.Item("id"), 0)
                End If

                dt1.Rows.Add(rowDT)
                numero = numero + 1
            Next

            rowDT = dt1.NewRow()
            rowDT.Item("id") = -1
            rowDT.Item("num_tipo") = ""
            rowDT.Item("riferimento_da") = ""
            rowDT.Item("riferimento_a") = "TOTALE"
            rowDT.Item("data_emissione") = ""
            rowDT.Item("data_scadenza") = ""
            rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
            rowDT.Item("imp_pagato") = Format(TOTimportopagato, "##,##0.00")
            rowDT.Item("imp_residuo") = Format(TOTimportoresiduo, "##,##0.00")
            rowDT.Item("importo_totale") = Format(TOTimportoEmesso, "##,##0.00")
            rowDT.Item("importo_ruolo") = Format(TOTImpRuolo, "##,##0.00")
            rowDT.Item("imp_ruolo_pagato") = Format(TOTImpRuoloPag, "##,##0.00")
            rowDT.Item("data_pagamento") = ""
            rowDT.Item("note") = ""
            rowDT.Item("fl_mora") = ""
            rowDT.Item("fl_rateizz") = ""
            rowDT.Item("id_tipo") = ""
            dt1.Rows.Add(rowDT)

            DataGrid1Pdf.DataSource = dt1

            DataGrid1Pdf.DataBind()

            connData.chiudi()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub RiempiGridRate()
        Try
            connData.apri()
            Dim table As New Data.DataTable
            par.cmd.CommandText = "SELECT num_rata AS numrata, " _
                                & "to_char(to_date(data_emissione,'yyyymmdd'),'dd/mm/yyyy') AS emissione, " _
                                & "to_char(to_date(data_scadenza,'yyyymmdd'),'dd/mm/yyyy') AS scadenza, " _
                                & "trim(TO_CHAR(importo_rata,'9G999G999G999G999G990D99')) AS importorata, " _
                                & "trim(TO_CHAR(quota_capitali,'9G999G999G999G999G990D99')) AS quotacapitali, " _
                                & "trim(TO_CHAR(quota_interessi,'9G999G999G999G999G990D99')) AS quotainteressi, " _
                                & "trim(TO_CHAR(residuo,'9G999G999G999G999G990D99')) AS importoresiduo " _
                                & "FROM siscom_mi.bol_rateizzazioni_dett WHERE num_rata > 0 AND EXISTS " _
                                & " (SELECT ID FROM siscom_mi.bol_rateizzazioni where bol_rateizzazioni.id=id_rateizzazione " _
                                & " and id_dic_redditi = " & Request.QueryString("IDDICHIARAZ") & " and fl_annullata = 0) order by num_rata asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(table)
            If table.Rows.Count = 0 Then
                table = CaricaSimulazioneRate()
            End If
            DataGridRate.DataSource = table
            DataGridRate.DataBind()
            connData.chiudi()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Function CaricaSimulazioneRate() As Data.DataTable
        Dim giorniScad As Integer = 0
        Dim tasso As Decimal = 0
        '******APERTURA CONNESSIONE*****
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim dt As New Data.DataTable
        par.cmd.CommandText = "SELECT TASSO FROM SISCOM_MI.TAB_INTERESSI_LEGALI WHERE ANNO = (SELECT MAX(ANNO) FROM SISCOM_MI.TAB_INTERESSI_LEGALI) "
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

        If lettore.Read Then
            tasso = par.IfNull(lettore("TASSO"), 0)
        End If
        lettore.Close()

        par.cmd.CommandText = "SELECT VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE DESCRIZIONE = 'GIORNO DEL MESE SCADENZA RATEIZZAZIONE'"
        lettore = par.cmd.ExecuteReader
        If lettore.Read Then
            giorniScad = lettore(0)
        End If
        lettore.Close()

        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Dim nRate As Integer = 0
        If Not String.IsNullOrEmpty(Request.QueryString("NRATE")) Then
            nRate = Request.QueryString("NRATE")
        End If

        If Not String.IsNullOrEmpty(par.IfEmpty(Request.QueryString("CAPITALE"), 0)) And Not String.IsNullOrEmpty(par.IfEmpty(Request.QueryString("EMISSIONE"), "")) Then

            If Not String.IsNullOrEmpty(Request.QueryString("NRATE")) Then
                dt = par.Ammortamento(Request.QueryString("CAPITALE").ToString.Replace(".", ""), Request.QueryString("NRATE"), tasso, 12, Request.QueryString("EMISSIONE"), giorniScad)
            ElseIf Not String.IsNullOrEmpty(Request.QueryString("IMPRATA")) Then
                dt = par.AmmortamentoPerRata(Request.QueryString("CAPITALE").ToString.Replace(".", ""), Request.QueryString("IMPRATA").Replace(".", ""), tasso, 12, Request.QueryString("EMISSIONE"), giorniScad, nRate)
            End If

            DataGridRate.DataSource = dt
            DataGridRate.DataBind()


        End If

        Return dt
    End Function

    Private Function ottieniDataPres(ByVal idDom As Long) As String
        Dim dataPres As String = ""

        par.cmd.CommandText = "select data_presentazione from domande_bando_vsa where id=" & idDom
        Dim lettoreData As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreData.Read Then
            dataPres = par.FormattaData(par.IfNull(lettoreData(0), ""))
        End If
        lettoreData.Close()
       
        Return dataPres
    End Function

    Private Function ottieniIDContr(ByVal codContr As String) As Long
        Dim idContr As Long = 0

        par.cmd.CommandText = "select id from siscom_mi.rapporti_utenza where cod_contratto='" & codContr & "'"
        Dim lettoreIDc As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettoreIDc.Read Then
            idContr = lettoreIDc(0)
        End If
        lettoreIDc.Close()
        
        Return idContr

    End Function

    Protected Sub pdfEsitoNeg()

        Try
            connData.apri()

            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)

            Me.DataGrid1Pdf.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim sr1 As StreamReader

            sr1 = New StreamReader(Server.MapPath("ModelliRateizzazione\EsitoNegativo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim siglaRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim tbDoc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 5


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$cod_ui$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            'MOTIVI ESITO NEGATIVO MEMORIZZATI NEL CAMPO NOTE
            Dim motivi As String = ""
            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=3 OR COD_DECISIONE=8) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                motivi = par.IfNull(myReader("NOTE"), "")
                motivi = Replace(motivi, ",", ";<br/>")
                contenuto = Replace(contenuto, "$motiviNEG$", motivi & ".")
            Else
                contenuto = Replace(contenuto, "$motiviNEG$", " ")
            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOUI,UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
            & "INDIRIZZI.DESCRIZIONE AS INDIR,INDIRIZZI.CIVICO AS CIV,INDIRIZZI.CAP AS CAPIND,INDIRIZZI.LOCALITA AS LOC " _
            & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI," _
            & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE " _
            & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
            & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND UNITA_IMMOBILIARI.id_unita_principale is null " _
            & "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD = unita_immobiliari.COD_TIPOLOGIA AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIR"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LOC"), ""))
                contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIV"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIAN"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SC"), ""))
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            contenuto = Replace(contenuto, "$estrattoconto$", Html)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 40
            pdfConverter1.PdfDocumentOptions.RightMargin = 40
            pdfConverter1.PdfDocumentOptions.TopMargin = 20

            pdfConverter1.PdfDocumentOptions.BottomMargin = 20
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.FitWidth = True
            
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False




            Dim nomefile As String = "Negativo" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            ' pdfConverter1.SavePdfFromHtmlStringToFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            connData.chiudi()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Protected Sub pdfEsitoPosit()

        Try
            connData.apri()

            Dim HtmlEstrattoConto As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)

            Me.DataGrid1Pdf.RenderControl(sourcecode)
            sourcecode.Flush()
            HtmlEstrattoConto = stringWriter.ToString

            Dim HtmlRate As String = ""
            Dim stringWriter2 As New System.IO.StringWriter
            Dim sourcecode2 As New HtmlTextWriter(stringWriter2)

            Me.DataGridRate.RenderControl(sourcecode2)
            sourcecode2.Flush()
            HtmlRate = stringWriter2.ToString

            Dim sr1 As StreamReader

            sr1 = New StreamReader(Server.MapPath("ModelliRateizzazione\EsitoPositivo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim luogoRes As String
            Dim siglaRes As String
            Dim codUi As String = ""
            Dim codContr As String = ""
            Dim id_dom As Long
            Dim tbDoc As String = "<table style='width:100%;'>"
            Dim NumDoc As Integer = 5


            codUi = Request.QueryString("CODUNITA")
            codContr = Request.QueryString("NUMCONT")

            contenuto = Replace(contenuto, "$cod_ui$", codUi)
            contenuto = Replace(contenuto, "$codcontratto$", codContr)

            idc = ottieniIDContr(codContr)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI,DICHIARAZIONI_VSA WHERE COMUNI_NAZIONI.ID = DICHIARAZIONI_VSA.ID_LUOGO_RES_DNTE AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                luogoRes = par.IfNull(myReader("NOME"), "")
                siglaRes = par.IfNull(myReader("SIGLA"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID AS ID_DOM,DICHIARAZIONI_VSA.PG AS PG_DICH,DOMANDE_BANDO_VSA.PG AS PG_DOM,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOT_DOMANDA,T_TIPO_INDIRIZZO.DESCRIZIONE AS TIPO_VIA,DICHIARAZIONI_VSA.*,DOMANDE_BANDO_VSA.*,COMP_NUCLEO_VSA.* " _
                & "FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA,DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,T_TIPO_INDIRIZZO WHERE DICHIARAZIONI_VSA.ID = COMP_NUCLEO_VSA.ID_DICHIARAZIONE " _
                & "AND DICHIARAZIONI_VSA.ID = DOMANDE_BANDO_VSA.ID_DICHIARAZIONE AND T_TIPO_INDIRIZZO.COD = DICHIARAZIONI_VSA.ID_TIPO_IND_RES_DNTE AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA = T_MOTIVO_DOMANDA_VSA.ID AND DICHIARAZIONI_VSA.ID = " & Request.QueryString("IDDICHIARAZ") & " AND COMP_NUCLEO_VSA.PROGR = 0"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                contenuto = Replace(contenuto, "$pgdomanda$", par.IfNull(myReader("PG_DOM"), ""))
                contenuto = Replace(contenuto, "$pgdichiarazione$", par.IfNull(myReader("PG_DICH"), ""))
                contenuto = Replace(contenuto, "$data_pg$", par.FormattaData(par.IfNull(myReader("DATA_PG"), "")))
                contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), ""))
                'contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("TIPO_VIA"), "") & " " & par.IfNull(myReader("IND_RES_DNTE"), "") & ", " & par.IfNull(myReader("CIVICO_RES_DNTE"), ""))
                'contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP_RES_DNTE"), "") & " " & luogoRes & " " & siglaRes)
                contenuto = Replace(contenuto, "$procedura$", par.IfNull(myReader("MOT_DOMANDA"), ""))
                id_dom = par.IfNull(myReader("ID_DOM"), "")

            End If
            myReader.Close()

            contenuto = Replace(contenuto, "$dataPres$", ottieniDataPres(id_dom))


            'MOTIVI ESITO NEGATIVO MEMORIZZATI NEL CAMPO NOTE
            Dim motivi As String = ""
            par.cmd.CommandText = "SELECT * FROM VSA_DECISIONI_REV_C WHERE ID_DOMANDA=" & id_dom & " AND (COD_DECISIONE=3 OR COD_DECISIONE=8) ORDER BY COD_DECISIONE ASC"
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                motivi = par.IfNull(myReader("NOTE"), "")
                motivi = Replace(motivi, ",", ";<br/>")
                contenuto = Replace(contenuto, "$motiviNEG$", motivi & ".")
            Else
                contenuto = Replace(contenuto, "$motiviNEG$", " ")
            End If
            myReader.Close()


            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.cmd.CommandText = "SELECT * FROM DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & id_dom
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.IfNull(myReader("COMUNE"), "") & "'"
                Dim lettoreComu As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If lettoreComu.Read Then
                    siglaRes = par.IfNull(lettoreComu("SIGLA"), "")
                End If
                lettoreComu.Close()
                contenuto = Replace(contenuto, "$indirizzo0$", "INTERNO " & par.IfNull(myReader("INTERNO"), "") & " PIANO " & par.IfNull(myReader("PIANO"), ""))
                contenuto = Replace(contenuto, "$indirizzo2$", par.IfNull(myReader("INDIRIZZO"), "") & ", " & par.IfNull(myReader("CIVICO"), ""))
                contenuto = Replace(contenuto, "$indirizzo1$", par.IfNull(myReader("CAP"), "") & " " & par.IfNull(myReader("COMUNE"), "") & " " & siglaRes)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOUI,UNITA_IMMOBILIARI.*,UNITA_CONTRATTUALE.NUM_VANI,SCALE_EDIFICI.DESCRIZIONE AS SC,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIAN," _
            & "INDIRIZZI.DESCRIZIONE AS INDIR,INDIRIZZI.CIVICO AS CIV,INDIRIZZI.CAP AS CAPIND,INDIRIZZI.LOCALITA AS LOC " _
            & "FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI," _
            & "SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE " _
            & "UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) and " _
            & "EDIFICI.ID = UNITA_IMMOBILIARI.ID_EDIFICIO AND EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND UNITA_IMMOBILIARI.id_unita_principale is null " _
            & "AND TIPOLOGIA_UNITA_IMMOBILIARI.COD = unita_immobiliari.COD_TIPOLOGIA AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND UNITA_CONTRATTUALE.ID_CONTRATTO=" & idc
            myReader = par.cmd.ExecuteReader
            If myReader.Read Then
                contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReader("INDIR"), ""))
                contenuto = Replace(contenuto, "$localita$", par.IfNull(myReader("LOC"), ""))
                contenuto = Replace(contenuto, "$numciv$", par.IfNull(myReader("CIV"), ""))
                contenuto = Replace(contenuto, "$piano$", par.IfNull(myReader("PIAN"), ""))
                contenuto = Replace(contenuto, "$scala$", par.IfNull(myReader("SC"), ""))
                contenuto = Replace(contenuto, "$numalloggio$", par.IfNull(myReader("INTERNO"), ""))
            End If
            myReader.Close()

            'Richiamo funzione per la Tabella a piè di pagina contenente le informazioni sulla filiale
            contenuto = caricaRespFiliale(idc, contenuto)

            contenuto = Replace(contenuto, "$estrattoconto$", HtmlEstrattoConto)
            contenuto = Replace(contenuto, "$pianoRateizzazione$", HtmlRate)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 40
            pdfConverter1.PdfDocumentOptions.RightMargin = 40
            pdfConverter1.PdfDocumentOptions.TopMargin = 20

            pdfConverter1.PdfDocumentOptions.BottomMargin = 20
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False




            Dim nomefile As String = "Positivo" & Request.QueryString("IDDICHIARAZ") & "-" & Format(Now, "yyyyMMddHHmmss")

            ' pdfConverter1.SavePdfFromHtmlStringToFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\" & Replace(Session.Item("Firme_Responsabili"), "/", "\")))


            '***********ZIPPO IL FILE PDF CREATO E LO SALVO NEGLI ALLEGATI/LOCATARI
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\ALLEGATI\LOCATARI\" & nomefile & ".zip")

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
            Response.Redirect("..\ALLEGATI\LOCATARI\" & nomefile & ".zip", False)

            'Response.Write("<script>window.open('../ALLEGATI/LOCATARI/" & nomefile & ".zip""','RevCan','');</script>")

            connData.chiudi()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Private Function caricaRespFiliale(ByVal idContra As String, ByVal conten As String) As String

        Dim Responsabile As String = ""
        Dim Acronimo As String = ""
        Dim dataPresent As String = ""
        Dim CentroDiCosto As String = ""
        Dim StringaIntera As String = ""

        par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & Request.QueryString("IDDICHIARAZ")
        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If myReader0.Read Then
            dataPresent = par.IfNull(myReader0("DATA_PRESENTAZIONE"), "")
        End If
        myReader0.Close()

        'If dataPresent < "20141201" Then
        '    dataPresent = "20141201"
        'Else
        '    dataPresent = Format(Now, "yyyyMMdd")
        'End If

        dataPresent = Format(Now, "yyyyMMdd")

        par.cmd.CommandText = "SELECT tab_filiali.*,indirizzi.descrizione AS descr, indirizzi.civico,indirizzi.cap, indirizzi.localita FROM siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.FILIALI_UI WHERE unita_contrattuale.id_unita_principale IS NULL AND unita_contrattuale.id_contratto =" & idContra & " AND UNITA_IMMOBILIARI.ID = FILIALI_UI.ID_UI AND FILIALI_UI.ID_FILIALE=TAB_FILIALI.ID AND indirizzi.ID = tab_filiali.id_indirizzo AND unita_immobiliari.ID = unita_contrattuale.id_unita AND INIZIO_VALIDITA <='" & dataPresent & "' AND FINE_VALIDITA >= '" & dataPresent & "'"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If myReader.Read Then
            conten = Replace(conten, "$nomefiliale$", par.IfNull(myReader("NOME"), ""))
            conten = Replace(conten, "$indirizzofiliale$", par.IfNull(myReader("DESCR"), "") & " " & par.IfNull(myReader("CIVICO"), ""))
            conten = Replace(conten, "$capfiliale$", par.IfNull(myReader("CAP"), ""))
            conten = Replace(conten, "$cittafiliale$", par.IfNull(myReader("LOCALITA"), ""))

            Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
            Acronimo = par.IfNull(myReader("ACRONIMO"), "")
            CentroDiCosto = par.IfNull(myReader("CENTRO_DI_COSTO"), "")

            conten = Replace(conten, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
            conten = Replace(conten, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))

            conten = Replace(conten, "$responsabile$", Responsabile)
            conten = Replace(conten, "$acronimo$", "PCC/" & Acronimo)
            conten = Replace(conten, "$nverde$", par.IfNull(myReader("N_TELEFONO_VERDE"), ""))

            If CentroDiCosto <> "" Then
                StringaIntera = CentroDiCosto & "/"
            End If
            If Acronimo <> "" Then
                StringaIntera = StringaIntera & Acronimo & "/"
            End If

            conten = Replace(conten, "$cds/acr/pg$", StringaIntera & Request.QueryString("PROT"))
            conten = Replace(conten, "$centrodicosto$", StringaIntera & Request.QueryString("PROT"))


            If par.IfNull(myReader("firma"), "") <> "" Then
                'conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader("firma"), "") & "' />")
                conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & par.IfNull(myReader("firma"), "") & "' />")
            Else
                conten = Replace(conten, "$firmaresponsabile$", "")
            End If
        Else
            par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali,siscom_mi.unita_contrattuale,siscom_mi.filiali_virtuali where filiali_virtuali.id_contratto=unita_contrattuale.id_contratto and unita_contrattuale.id_unita_principale is null and unita_contrattuale.id_contratto=" & idContra & " and indirizzi.id=tab_filiali.id_indirizzo and tab_filiali.id=filiali_virtuali.id_filiale"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader2.Read Then
                conten = Replace(conten, "$nomefiliale$", par.IfNull(myReader2("NOME"), ""))
                conten = Replace(conten, "$indirizzofiliale$", par.IfNull(myReader2("DESCR"), "") & " " & par.IfNull(myReader2("CIVICO"), ""))
                conten = Replace(conten, "$capfiliale$", par.IfNull(myReader2("CAP"), ""))
                conten = Replace(conten, "$cittafiliale$", par.IfNull(myReader2("LOCALITA"), ""))

                Responsabile = par.IfNull(myReader("RESPONSABILE"), "")
                Acronimo = par.IfNull(myReader("ACRONIMO"), "")
                conten = Replace(conten, "$telfiliale$", par.IfNull(myReader("N_TELEFONO"), ""))
                conten = Replace(conten, "$faxfiliale$", par.IfNull(myReader("N_FAX"), ""))
                conten = Replace(conten, "$responsabile$", Responsabile)
                conten = Replace(conten, "$acronimo$", Acronimo)
                conten = Replace(conten, "$nverde$", par.IfNull(myReader2("N_TELEFONO_VERDE"), ""))
                conten = Replace(conten, "$centrodicosto$", "GL0000/" & Acronimo & "/" & Request.QueryString("PROT"))
                If par.IfNull(myReader2("firma"), "") <> "" Then
                    ' conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='../" & Session.Item("Firme_Responsabili") & par.IfNull(myReader2("firma"), "") & "' />")
                    conten = Replace(conten, "$firmaresponsabile$", "<img alt='Firma Responsabile' src='" & par.IfNull(myReader2("firma"), "") & "' />")

                Else
                    conten = Replace(conten, "$firmaresponsabile$", "")
                End If
            Else
                conten = Replace(conten, "$nomefiliale$", "")
                conten = Replace(conten, "$indirizzofiliale$", "")
                conten = Replace(conten, "$capfiliale$", "")
                conten = Replace(conten, "$cittafiliale$", "")
                Responsabile = ""
                Acronimo = ""
                conten = Replace(conten, "$telfiliale$", "")
                conten = Replace(conten, "$faxfiliale$", "")
                conten = Replace(conten, "$responsabile$", Responsabile)
                conten = Replace(conten, "$acronimo$", Acronimo)
                conten = Replace(conten, "$nverde$", "")
                conten = Replace(conten, "$centrodicosto$", "")
                conten = Replace(conten, "$firmaresponsabile$", "")
            End If
            myReader2.Close()
        End If
        myReader.Close()

        conten = Replace(conten, "$referente$", Session.Item("NOME_OPERATORE"))

        conten = Replace(conten, "$firmaResp$", "Il resp. della sede territoriale")


        'If Acronimo = "FILE" Or Acronimo = "FIRO" Or Acronimo = "FISE" Then
        '    conten = Replace(conten, "$firmaResp$", "Il Responsabile di Coordinamento di Filiali")
        '    conten = Replace(conten, "$sede$", "")
        '    conten = Replace(conten, "$coordinatore$", "Luigi Serati")
        '    conten = Replace(conten, "$firmaCoord$", "Luigi Serati")
        '    conten = Replace(conten, "$cognCoord$", "SERATI")
        '    conten = Replace(conten, "$nomeCoord$", "LUIGI")
        '    conten = Replace(conten, "$dataNascCoord$", "09/11/1952")
        '    conten = Replace(conten, "$luogoNascCoord$", "INVERUNO")
        '    conten = Replace(conten, "$provinciaNascCoord$", "MI")
        '    conten = Replace(conten, "$indirizzoCondomini$", "Milano Sud Ovest, Legnano, Rozzano")
        'Else
        '    conten = Replace(conten, "$firmaResp$", "Il Responsabile di Filiale")
        '    conten = Replace(conten, "$sede$", "MILANO")
        '    conten = Replace(conten, "$coordinatore$", Responsabile)
        '    conten = Replace(conten, "$firmaCoord$", "Giuseppe Riefolo")
        '    conten = Replace(conten, "$cognCoord$", "RIEFOLO")
        '    conten = Replace(conten, "$nomeCoord$", "GIUSEPPE")
        '    conten = Replace(conten, "$dataNascCoord$", "08/01/1954")
        '    conten = Replace(conten, "$luogoNascCoord$", "BARLETTA")
        '    conten = Replace(conten, "$provinciaNascCoord$", "BT")
        conten = Replace(conten, "$indirizzoCondomini$", "Via T. Pini, 1")
        'End If

        Return conten

    End Function

    Private Function ImpostaFlSgravio(ByVal idBollRuolo As Long, ByVal importoRuolo As Decimal) As String
        Dim tiposgravio As String = "N"
        Dim impPagatoRuolo As Decimal = 0
        Dim flSgravio As Integer = 0

        par.cmd.CommandText = "select * from siscom_mi.BOL_BOLLETTE_PAGAMENTI_RUOLO,siscom_mi.incassi_ruoli where " _
                & " BOL_BOLLETTE_PAGAMENTI_RUOLO.ID_INCASSO_RUOLO=incassi_ruoli.id and id_bolletta=" & idBollRuolo
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt1R As New Data.DataTable
        da1.Fill(dt1R)
        da1.Dispose()
        If dt1R.Rows.Count > 0 Then
            For Each rowRuolo As Data.DataRow In dt1R.Rows
                impPagatoRuolo = impPagatoRuolo + par.IfNull(rowRuolo.Item("importo_pagato"), 0)
                If par.IfNull(rowRuolo.Item("fl_sgravio"), 0) = 1 Then
                    flSgravio = 1
                End If
            Next
        End If

        If flSgravio = 1 Then
            If importoRuolo = impPagatoRuolo Then
                tiposgravio = "ST"
            Else
                tiposgravio = "SP"
            End If
        Else
            tiposgravio = "N"
        End If

        Return tiposgravio
    End Function

    Private Sub PrintPdf()
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            Me.DataGrid1Pdf.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString
            Html = par.EliminaLink(Html)

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If


            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 0
            pdfConverter1.PdfDocumentOptions.RightMargin = 0
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            'pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PageWidth = 1400
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            'pdfConverter1.PdfFooterOptions.FooterText = ("Estratto Conto " & LblTitolo.Text & " " & Format(Now, "hh:mm") & " - OPERATORE: " & Session.Item("OPERATORE"))
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_EstrattoContab_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFile("" & Html, url & nomefile)

            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Public Property dt1() As Data.DataTable
        Get
            If Not (ViewState("dt1") Is Nothing) Then
                Return ViewState("dt1")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dt1") = value
        End Set
    End Property
End Class
