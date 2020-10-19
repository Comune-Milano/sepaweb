Imports System.Collections.Generic
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
'12/01/2015 PUCCIA Nuova connessione  tls ssl
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Partial Class RATEIZZAZIONE_PeriodoRateizz
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
    Dim Esito As New MavOnline.rispostaMAVOnlineWS
    Dim pp As New MavOnline.MAVOnlineBeanService
    Dim binaryData() As Byte
    Dim outFile As System.IO.FileStream
    Dim outputFileName As String = ""
    Dim i As Long = 0

    Public percentuale As Long = 0


    Dim sPosteAler As String = ""               'TUTTI i CAMPI
    Dim sPosteAlerNominativo As String = ""     '1)  Nominativo Postale
    Dim sPosteAlerInd As String = ""            '3)  Indirizzo
    Dim sPosteAlerScala As String = ""          '6)  Scala
    Dim sPosteAlerInterno As String = ""        '7)  Interno
    Dim sPosteAlerCAP As String = ""            '8)  CAP
    Dim sPosteAlerLocalita As String = ""       '9)  Località
    Dim sPosteAlerProv As String = ""           '10) Provincia
    Dim sPosteAlerCodUtente As String = ""      '11) Codice Utente (POSTALER.ID)
    Dim sPosteAlerAcronimo As String = ""       '12) Acronimo
    Dim sPosteDefault As String = ""            ' per i campi 2-4-5 (Presso, casella postale, indirizzo casella postale)
    Dim sPosteAlerIA As String = ""

    '12/01/2015 PUCCIA Nuova connessione  tls ssl
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Str As String = ""

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../CONTRATTI/Immagini/load.gif' alt='Caricamento in corso' ><br>Caricamento in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If
        If Not IsPostBack Then
            Response.Flush()

            TrovaPeriodo()
            trovaBollRateizz()
        End If

    End Sub

    Private Sub TrovaPeriodo()
        Dim giornoNow As String = Format(Now, "dd")
        Dim meseNow As String = Format(Now, "MM")
        Dim annoNow As String = Format(Now, "yyyy")
        Dim Periodi As New SortedList(Of Integer, String)

        Me.lblAnno.Text = annoNow
        Periodi.Add(1, "Gennaio - Febbraio - Marzo")
        Periodi.Add(4, "Aprile - Maggio - Giugno")
        Periodi.Add(7, "Luglio - Agosto - Settembre")
        Periodi.Add(10, "Ottobre - Novembre - Dicembre")

        If meseNow <= 3 Then
            Me.cmbPeriodoBol.Items.Add(New ListItem(Periodi(4), 4))
            Me.lblAnno.Text = annoNow

        ElseIf meseNow > 3 AndAlso meseNow <= 6 Then
            Me.cmbPeriodoBol.Items.Add(New ListItem(Periodi(7), 7))
            Me.lblAnno.Text = annoNow

        ElseIf meseNow > 6 AndAlso meseNow <= 9 Then
            Me.cmbPeriodoBol.Items.Add(New ListItem(Periodi(10), 10))
            Me.lblAnno.Text = annoNow

        ElseIf meseNow > 9 AndAlso meseNow <= 12 Then
            Me.cmbPeriodoBol.Items.Add(New ListItem(Periodi(1), 1))
            Me.lblAnno.Text = annoNow + 1


        End If

        Me.cmbPeriodoBol.Enabled = False

    End Sub
    Private Sub trovaBollRateizz()
        Try
            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim Inizio As String = Format(CDate("01/" & Me.cmbPeriodoBol.SelectedValue.ToString & "/" & Me.lblAnno.Text), "dd/MM/yyyy")
            Dim Fine As String = Date.Parse(Inizio, New System.Globalization.CultureInfo("it-IT", False)).AddMonths(3).AddDays(-1).ToString("dd/MM/yyyy")
            Dim NumRateizzazioni As Integer
            Dim ImportoTot As Decimal

            par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni_dett " _
                                & "where fl_annullata = 0 and id_bolletta is null and " _
                                & "data_emissione >=" & par.AggiustaData(Inizio) & "and data_emissione <=" & par.AggiustaData(Fine) _
                                & " order by id_rateizzazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            par.cmd.CommandText = "select count(distinct(id_rateizzazione)) as num_rateizzazioni from siscom_mi.bol_rateizzazioni_dett " _
                                & "where fl_annullata = 0 and id_bolletta is null and " _
                                & "data_emissione >=" & par.AggiustaData(Inizio) & "and data_emissione <=" & par.AggiustaData(Fine)

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                NumRateizzazioni = par.IfNull(lettore("num_rateizzazioni"), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "select sum(importo_rata) as tot_rateizz from siscom_mi.bol_rateizzazioni_dett " _
                                & "where fl_annullata = 0 and id_bolletta is null and " _
                                & "data_emissione >=" & par.AggiustaData(Inizio) & "and data_emissione <=" & par.AggiustaData(Fine)

            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                ImportoTot = par.IfNull(lettore("tot_rateizz"), 0)
            End If
            lettore.Close()



            Me.lblNumBollette.Text = dt.Rows.Count
            Me.lblNumRateizz.Text = NumRateizzazioni
            Me.lblImpBollett.Text = "€. " & Format(ImportoTot, "##,##0.00")

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            Me.lblErrore.Text = "trovaBollRateizz - " & ex.Message
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Public Property DisabilitaExpect100Continue() As String
        Get
            If Not (ViewState("par_DisabilitaExpect100Continue") Is Nothing) Then
                Return CStr(ViewState("par_DisabilitaExpect100Continue"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_DisabilitaExpect100Continue") = value
        End Set
    End Property

    Protected Sub btnConfirm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfirm.Click
        Try


            '******APERTURA CONNESSIONE*****
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '**********apertura transazione
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans





            Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
            pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
            pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

            Dim Licenza As String = Session.Item("LicenzaPdfMerge")
            If Licenza <> "" Then
                pdfMerge.LicenseKey = Licenza
            End If

            Dim Contatore As Long = 0
            Dim NUMERORIGHE As Long = 0


            Dim APPLICABOLLO As Double = 0
            Dim SPESEmav As Double = 0
            Dim BOLLO As Double = 0
            Dim Inizio As String = Format(CDate("01/" & Me.cmbPeriodoBol.SelectedValue.ToString & "/" & Me.lblAnno.Text), "dd/MM/yyyy")
            Dim Fine As String = Date.Parse(Inizio, New System.Globalization.CultureInfo("it-IT", False)).AddMonths(3).AddDays(-1).ToString("dd/MM/yyyy")
            Dim idRateizzazione As String = "0"
            Dim IdBolletaCreata As String
            Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
            Dim Tot_Bolletta As Decimal
            Dim causalepagamento As String = ""

            Dim IdContratto As String = ""
            Dim presso_cor As String = ""
            Dim civico_cor As String = ""
            Dim luogo_cor As String = ""
            Dim cap_cor As String = ""
            Dim indirizzo_cor As String = ""
            Dim tipo_cor As String = ""
            Dim sigla_cor As String = ""
            Dim idUnita As String = ""
            Dim idEdificio As String = ""
            Dim idComplesso As String = ""
            Dim num_bollettino As String = ""



            Dim FILIALE As String = ""
            Dim ESTREMI As String = ""
            Dim ESTREMI0 As String = ""
            Dim ESTREMI1 As String = ""
            Dim ESTREMI2 As String = ""
            Dim ESTREMI3 As String = ""
            Dim ESTREMI4 As String = ""
            Dim ESTREMI6 As String = ""
            Dim ESTREMI5 As String = ""
            Dim INTESTATARIO As String = ""
            Dim INDIRIZZO_POSTALE As String = ""
            Dim INDIRIZZO_POSTALE0 As String = ""
            Dim INDIRIZZO_POSTALE1 As String = ""
            Dim INDIRIZZO_POSTALE2 As String = ""
            Dim NOMEFILIALE As String = ""
            Dim INDIRIZZO As String = ""
            Dim LOCALITA As String = ""
            Dim INDICELETTERA As String = "0"


            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI WHERE PARAMETRO='EXPECT100CONTINUE'"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                DisabilitaExpect100Continue = par.IfNull(myReaderA("valore"), "0")
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                SPESEmav = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            myReaderA = par.cmd.ExecuteReader()
            If myReaderA.Read Then
                BOLLO = CDbl(par.PuntiInVirgole(myReaderA("VALORE")))
            End If
            myReaderA.Close()



            par.cmd.CommandText = "select * from siscom_mi.bol_rateizzazioni_dett " _
                        & "where fl_annullata = 0 and id_bolletta is null and " _
                        & "data_emissione >=" & par.AggiustaData(Inizio) & "and data_emissione <=" & par.AggiustaData(Fine) _
                        & " order by id_rateizzazione asc"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            Dim dtBol As New Data.DataTable

            Dim elFileCreati() As String
            Dim numFileCreati As Integer = 0

            da.Fill(dt)
            Dim PersNotValid As String = ""

            par.cmd.CommandText = "SELECT ANAGRAFICA.ID AS ID_ANA , anagrafica.sesso,ragione_sociale, COGNOME ,NOME,PARTITA_IVA, COD_FISCALE  " _
                                        & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE, SISCOM_MI.ANAGRAFICA, SISCOM_MI.RAPPORTI_UTENZA, siscom_mi.bol_bollette " _
                                        & "WHERE id_rateizzazione in (select id_rateizzazione from siscom_mi.bol_rateizzazioni_dett " _
                                        & "where fl_annullata = 0 and id_bolletta is null and " _
                                        & "data_emissione >=" & par.AggiustaData(Inizio) & "and data_emissione <=" & par.AggiustaData(Fine) & ")" _
                                        & "AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.DATA_FINE>= TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') " _
                                        & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                        & "AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                        & "GROUP BY id_tipo,bol_bollette.id_contratto,id_unita, cod_affittuario,intestatario,indirizzo,cap_citta,presso,id_complesso,id_edificio," _
                                        & "ANAGRAFICA.ID,anagrafica.sesso,ragione_sociale, COGNOME ,NOME,PARTITA_IVA, COD_FISCALE , RAPPORTI_UTENZA.COD_CONTRATTO"
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dtBol)

            For Each riga As Data.DataRow In dtBol.Rows
                If Len(par.IfNull(riga.Item("PARTITA_IVA"), 0)) = 11 Or par.ControllaCF(par.IfNull(riga.Item("COD_FISCALE"), 0)) = True Then
                Else
                    If par.IfNull(riga.Item("ragione_sociale"), "") <> "" Then
                        PersNotValid = PersNotValid & riga.Item("ragione_sociale") & vbCrLf
                    Else
                        PersNotValid = PersNotValid & riga.Item("COGNOME") & " " & riga.Item("NOME") & vbCrLf
                    End If

                End If
            Next
            If PersNotValid <> "" Then
                PersNotValid = "I seguenti nominativi hanno P.IVA/COD.FISCALE errati!Si prega di verificane la correttezza prima di emettere le bollette di Rateizzazione." & vbCrLf & PersNotValid

                outputFileName = "Rateizzazione_" & Format(Now, "yyyyMMddHHmmss") & ".txt"
                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & outputFileName, False, System.Text.Encoding.Default)
                sr.WriteLine(PersNotValid)
                sr.Close()
                lblErrore.Visible = True
                lblErrore.Text = "<a href = " & Chr(34) & "../FileTemp/" & outputFileName & Chr(34) & " target = " & Chr(34) & "_blank" & Chr(34) & ">Ci sono stati degli errori durante la fase di creazione dei M.A.V.!! Clicca qui per visualizzare.</a>"
                par.myTrans.Rollback()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Exit Sub
            End If

            If NUMERORIGHE = 0 Then
                NUMERORIGHE = dt.Rows.Count
            End If

            If dt.Rows.Count = 0 Then
                Response.Write("<script>alert('Nessuna rateizzazione da bollettare per questo periodo!');</script>")


                par.myTrans.Rollback()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub

            End If
            '************per ogni recod in Bol_rateizzazioni
            For Each r As Data.DataRow In dt.Rows
                ''calcolo la data di scadenza della bolletta
                Contatore = Contatore + 1
                percentuale = (Contatore * 100) / NUMERORIGHE
                Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                Response.Flush()

                '**********recupero informazioni della bolletta rateizzata e dati relativi al contratto
                If idRateizzazione <> r.Item("id_rateizzazione") Then
                    dtBol.Clear()
                    dtBol.Dispose()

                    idRateizzazione = r.Item("id_rateizzazione")
                    par.cmd.CommandText = "SELECT bol_bollette.id_tipo, bol_bollette.id_contratto,id_unita, cod_affittuario,intestatario,indirizzo,cap_citta,presso,id_complesso,id_edificio, " _
                                        & "ANAGRAFICA.ID AS ID_ANA , anagrafica.sesso,ragione_sociale, COGNOME ,NOME,PARTITA_IVA, COD_FISCALE , RAPPORTI_UTENZA.COD_CONTRATTO " _
                                        & "FROM SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE, SISCOM_MI.ANAGRAFICA, SISCOM_MI.RAPPORTI_UTENZA, siscom_mi.bol_bollette " _
                                        & "WHERE id_rateizzazione = " & idRateizzazione _
                                        & "AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND SOGGETTI_CONTRATTUALI.DATA_FINE>= TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd') " _
                                        & "AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                        & "AND RAPPORTI_UTENZA.ID = SOGGETTI_CONTRATTUALI.ID_CONTRATTO " _
                                        & "GROUP BY id_tipo,bol_bollette.id_contratto,id_unita, cod_affittuario,intestatario,indirizzo,cap_citta,presso,id_complesso,id_edificio," _
                                        & "ANAGRAFICA.ID,anagrafica.sesso,ragione_sociale, COGNOME ,NOME,PARTITA_IVA, COD_FISCALE , RAPPORTI_UTENZA.COD_CONTRATTO"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dtBol)
                End If




                If dtBol.Rows.Count > 0 Then

                    Tot_Bolletta = 0
                    Dim Titolo As String = ""
                    Dim Nome As String = ""
                    Dim Cognome As String = ""
                    Dim CF As String = ""
                    Dim Cod_contratto As String = par.IfNull(dtBol.Rows(0).Item("COD_CONTRATTO"), "0")

                    par.cmd.CommandText = "select complessi_immobiliari.id as idcomplesso,edifici.id as idedificio," _
                                        & "siscom_mi.rapporti_utenza.*,unita_contrattuale.id_unita from SISCOM_MI.EDIFICI," _
                                        & "siscom_mi.complessi_immobiliari,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale, " _
                                        & "siscom_mi.rapporti_utenza where complessi_immobiliari.id=edifici.id_complesso " _
                                        & "and unita_immobiliari.id=unita_contrattualE.id_unita and edifici.id=unita_immobiliari.id_edificio " _
                                        & "and unita_contrattuale.id_contratto=rapporti_utenza.id and unita_contrattuale.id_unita_principale is null " _
                                        & "and cod_contratto='" & dtBol.Rows(0).Item("COD_CONTRATTO") & "'"
                    Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123.Read Then
                        IdContratto = par.IfNull(myReader123("id"), "-1")
                        idUnita = par.IfNull(myReader123("id_unita"), "-1")
                        presso_cor = par.IfNull(myReader123("presso_cor"), "")
                        luogo_cor = par.IfNull(myReader123("luogo_cor"), "")
                        civico_cor = par.IfNull(myReader123("civico_cor"), "")
                        cap_cor = par.IfNull(myReader123("cap_cor"), "")
                        indirizzo_cor = par.IfNull(myReader123("VIA_cor"), "")
                        tipo_cor = par.IfNull(myReader123("tipo_cor"), "")
                        sigla_cor = par.IfNull(myReader123("sigla_cor"), "")
                        idEdificio = par.IfNull(myReader123("idedificio"), "0")
                        idComplesso = par.IfNull(myReader123("idcomplesso"), "0")
                    End If
                    myReader123.Close()

                    Dim Nome1 As String = ""
                    Dim Nome2 As String = ""

                    If UCase(Cognome & " " & Nome) <> UCase(presso_cor) Then
                        Nome1 = Cognome & " " & Nome
                        Nome2 = presso_cor
                    Else
                        Nome1 = presso_cor
                    End If

                    If par.IfNull(dtBol.Rows(0).Item("ragione_sociale"), "") <> "" Then
                        Titolo = ""
                        Cognome = par.IfNull(dtBol.Rows(0).Item("ragione_sociale"), "")
                        Nome = ""
                        CF = par.IfNull(dtBol.Rows(0).Item("partita_iva"), "")
                    Else
                        If par.IfNull(dtBol.Rows(0).Item("sesso"), "") = "M" Then
                            Titolo = "Sign."
                        Else
                            Titolo = "Sign.ra"
                        End If
                        Cognome = par.IfNull(dtBol.Rows(0).Item("cognome"), "")
                        Nome = par.IfNull(dtBol.Rows(0).Item("nome"), "")
                        CF = par.IfNull(dtBol.Rows(0).Item("cod_fiscale"), "")
                    End If


                    '*******************SCRIVO IN BOL_BOLLETTE LA BOLLETTA DI RATEIZZAZIONE


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID," _
                        & "N_RATA," _
                        & "DATA_EMISSIONE," _
                        & "DATA_SCADENZA," _
                        & "DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, " _
                        & "DATA_PAGAMENTO, " _
                        & "NOTE, ID_CONTRATTO, " _
                        & "ID_ESERCIZIO_F, " _
                        & "ID_UNITA," _
                        & "FL_ANNULLATA," _
                        & "PAGABILE_PRESSO," _
                        & "COD_AFFITTUARIO, " _
                        & "INTESTATARIO, " _
                        & "INDIRIZZO, " _
                        & "CAP_CITTA, " _
                        & "PRESSO, " _
                        & "RIFERIMENTO_DA, " _
                        & "RIFERIMENTO_A, " _
                        & "FL_STAMPATO, " _
                        & "ID_COMPLESSO, " _
                        & "DATA_INS_PAGAMENTO, " _
                        & "IMPORTO_PAGATO, " _
                        & "NOTE_PAGAMENTO, " _
                        & "ANNO, " _
                        & "OPERATORE_PAG, " _
                        & "ID_EDIFICIO, " _
                        & "DATA_ANNULLO_PAG, " _
                        & "OPERATORE_ANNULLO_PAG, " _
                        & "RIF_FILE, " _
                        & "ID_TIPO) " _
                        & "VALUES " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, " _
                        & "999 ," _
                        & "'" & r.Item("data_emissione") & "'," _
                        & "'" & r.Item("data_scadenza") & "', " _
                        & "NULL," _
                        & "NULL," _
                        & "NULL," _
                        & "'BOLLETTA DI RATEIZZAZIONE'," _
                        & IdContratto & "," _
                        & par.RicavaEsercizioCorrente & ", " _
                        & par.IfNull(idUnita, 0) & "," _
                        & "'0'," _
                        & "''," _
                        & par.IfNull(dtBol.Rows(0).Item("ID_ANA"), 0) & "," _
                        & "'" & par.PulisciStrSql(CF) & "', " _
                        & "'" & tipo_cor & " " & par.PulisciStrSql(indirizzo_cor) & ", " & par.PulisciStrSql(civico_cor) & "'," _
                        & "'" & par.PulisciStrSql(cap_cor & " " & luogo_cor & "(" & sigla_cor & ")") & "'," _
                        & "'" & par.PulisciStrSql(Nome2) & "'," _
                        & "'" & r.Item("data_emissione") & "'," _
                        & "'" & r.Item("data_emissione") & "', " _
                        & "'0', " _
                        & idComplesso & "," _
                        & "'', " _
                        & "NULL, " _
                        & "'', " _
                        & Year(Now) & "," _
                        & "'', " & idEdificio & "," _
                        & "NULL, " _
                        & "NULL, " _
                        & "'MOD'," _
                        & "5)"

                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                    myReaderB = par.cmd.ExecuteReader()
                    If myReaderB.Read Then
                        IdBolletaCreata = myReaderB(0)
                    Else
                        IdBolletaCreata = -1
                    End If
                    myReaderB.Close()




                    '***************DATI POSTALER************************************************
                    Dim x As Integer = 0

                    Dim dict As New System.Data.DataTable
                    Dim ROW1 As System.Data.DataRow

                    dict.Columns.Add("NOME")
                    dict.Columns.Add("INDIRIZZO")
                    dict.Columns.Add("CAP")
                    dict.Columns.Add("LOCALITA")
                    dict.Columns.Add("TELEFONI")
                    dict.Columns.Add("REFERENTE")
                    dict.Columns.Add("RESPONSABILE")
                    dict.Columns.Add("NVERDE")
                    dict.Columns.Add("ACRONIMO")


                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select tab_filiali.*,indirizzi.descrizione as descr,indirizzi.civico,indirizzi.cap,indirizzi.localita from siscom_mi.indirizzi,siscom_mi.tab_filiali where indirizzi.id=tab_filiali.id_indirizzo AND id_tipo_st=0 and acronimo is not null"
                    myReaderX = par.cmd.ExecuteReader()
                    Do While myReaderX.Read
                        ROW1 = dict.NewRow()
                        ROW1.Item("NOME") = par.IfNull(myReaderX("nome"), "")
                        ROW1.Item("INDIRIZZO") = par.IfNull(myReaderX("descr"), "") & " " & par.IfNull(myReaderX("civico"), "")
                        ROW1.Item("CAP") = par.IfNull(myReaderX("cap"), "")
                        ROW1.Item("LOCALITA") = par.IfNull(myReaderX("LOCALITA"), "")
                        ROW1.Item("TELEFONI") = "Tel:" & par.IfNull(myReaderX("n_telefono"), "") & " - Fax:" & par.IfNull(myReaderX("n_fax"), "") & " - n.verde:" & par.IfNull(myReaderX("n_telefono_verde"), "")
                        ROW1.Item("REFERENTE") = Session.Item("NOME_OPERATORE")
                        ROW1.Item("RESPONSABILE") = par.IfNull(myReaderX("responsabile"), "")
                        ROW1.Item("NVERDE") = par.IfNull(myReaderX("n_telefono_verde"), "")
                        ROW1.Item("ACRONIMO") = par.IfNull(myReaderX("ACRONIMO"), "")
                        dict.Rows.Add(ROW1)
                    Loop
                    myReaderX.Close()




                    INTESTATARIO = Trim(par.IfNull(Cognome, "") & " " & par.IfNull(Nome, ""))
                    INDIRIZZO_POSTALE = Trim(Replace(UCase(par.IfNull(presso_cor, "")), "C/O", ""))
                    INDIRIZZO_POSTALE0 = par.IfNull(tipo_cor, "") & " " & par.IfNull(indirizzo_cor, "") & " " & par.IfNull(civico_cor, "")
                    INDIRIZZO_POSTALE1 = par.IfNull(cap_cor, "") & " " & par.IfNull(luogo_cor, "") & " " & par.IfNull(sigla_cor, "")
                    INDIRIZZO_POSTALE2 = ""
                    sPosteAlerCAP = par.IfNull(cap_cor, "")
                    sPosteAlerLocalita = par.IfNull(luogo_cor, "")
                    sPosteAlerProv = par.IfNull(sigla_cor, "")
                    sPosteAlerCodUtente = Format(CLng(par.IfNull(dtBol.Rows(0).Item("ID_ANA"), "")), "000000000000")

                    Dim FF As String = ""
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                    par.cmd.CommandText = "select scale_edifici.descrizione as SCA,unita_immobiliari.*,TIPO_LIVELLO_PIANO.LIVELLO from siscom_mi.scale_edifici,siscom_mi.unita_immobiliari,SISCOM_MI.TIPO_LIVELLO_PIANO where unita_immobiliari.id_scala=scale_edifici.id (+) and TIPO_LIVELLO_PIANO.COD=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND cod_unita_immobiliare='" & Mid(Cod_contratto, 1, 17) & "'"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        If par.IfNull(myReader1("INTERNO"), "") <> "" Then
                            FF = FF & "INTERNO " & par.IfNull(myReader1("INTERNO"), "") & " "
                            sPosteAlerInterno = par.IfNull(myReader1("INTERNO"), "")
                        End If

                        Dim ll As Integer = 0
                        If par.IfNull(myReader1("SCA"), "") <> "" Then
                            FF = FF & "SCALA " & par.IfNull(myReader1("SCA"), "") & " "
                            sPosteAlerScala = par.IfNull(myReader1("SCA"), "")

                            For ll = 1 To Strings.Len(par.IfNull(myReader1("SCA"), ""))
                                If Char.IsDigit(Strings.Mid(par.IfNull(myReader1("SCA"), ""), ll, 1)) = False Then
                                    sPosteAlerScala = Strings.Mid(par.IfNull(myReader1("SCA"), ""), ll, Strings.Len(par.IfNull(myReader1("SCA"), "")))  'POSTE
                                    Exit For
                                End If
                            Next ll

                        End If
                        If par.IfNull(myReader1("LIVELLO"), -100) <> -100 Then
                            If CInt(myReader1("LIVELLO")) - myReader1("LIVELLO") = 0 Then
                                If myReader1("LIVELLO") - 0.5 = 0 Then
                                    FF = FF & "PIANO T" & " "
                                Else
                                    FF = FF & "PIANO " & myReader1("LIVELLO") & " "
                                End If
                            Else
                                If myReader1("LIVELLO") - 0.5 = 0 Then
                                    FF = FF & "PIANO T" & " "
                                Else
                                    FF = FF & "PIANO " & myReader1("LIVELLO") - 0.5 & " "
                                End If

                            End If
                        End If
                        INDIRIZZO_POSTALE2 = Mid(FF, 1, 30)
                    End If
                    myReader1.Close()



                    par.cmd.CommandText = "select indirizzi.*,comuni_nazioni.nome,comuni_nazioni.sigla from comuni_nazioni,siscom_mi.indirizzi where comuni_nazioni.cod=indirizzi.cod_comune and indirizzi.id in (select id_indirizzo from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & Mid(Cod_contratto, 1, 17) & "')"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        INDIRIZZO = par.IfNull(myReader1("descrizione"), "") & " " & par.IfNull(myReader1("civico"), "")
                        LOCALITA = par.IfNull(myReader1("cap"), "") & " " & par.IfNull(myReader1("nome"), "") & " " & par.IfNull(myReader1("sigla"), "")
                    End If
                    myReader1.Close()

                    If NOMEFILIALE <> "&nbsp;" Then

                        For Each ROW1 In dict.Rows
                            If NOMEFILIALE = par.IfNull(dict.Rows(x).Item("NOME"), "") Then

                                FILIALE = par.IfNull(dict.Rows(x).Item("NOME"), "")
                                ESTREMI = par.IfNull(dict.Rows(x).Item("INDIRIZZO"), "")
                                ESTREMI0 = par.IfNull(dict.Rows(x).Item("CAP"), "")
                                ESTREMI1 = par.IfNull(dict.Rows(x).Item("LOCALITA"), "")
                                ESTREMI2 = par.IfNull(dict.Rows(x).Item("TELEFONI"), "")
                                ESTREMI3 = par.IfNull(dict.Rows(x).Item("REFERENTE"), "")
                                ESTREMI4 = par.IfNull(dict.Rows(x).Item("RESPONSABILE"), "")
                                ESTREMI6 = par.IfNull(dict.Rows(x).Item("NVERDE"), "")
                                ESTREMI5 = "GL0000/" & par.IfNull(dict.Rows(x).Item("ACRONIMO"), "")
                                sPosteAlerAcronimo = par.IfNull(dict.Rows(x).Item("ACRONIMO"), "")
                                Exit For
                            End If
                            x = x + 1
                        Next

                    Else


                    End If

                    ' '' Ricavo ID di POSTALER per PostAler.txt

                    par.cmd.CommandText = " select SISCOM_MI.SEQ_POSTALER.NEXTVAL FROM dual "
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        sPosteAlerIA = myReader1(0)
                    End If
                    myReader1.Close()

                    If sPosteAler <> "" Then
                        sPosteAler = sPosteAler & vbCrLf
                    End If
                    sPosteAler = sPosteAler _
                           & INDIRIZZO_POSTALE.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteDefault.PadRight(50).Substring(0, 50) & ";" _
                           & INDIRIZZO_POSTALE0.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteDefault.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteDefault.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteAlerScala.PadRight(2).Substring(0, 2) & ";" _
                           & sPosteAlerInterno.PadRight(3).Substring(0, 3) & ";" _
                           & sPosteAlerCAP.PadRight(5).Substring(0, 5) & ";" _
                           & sPosteAlerLocalita.PadRight(50).Substring(0, 50) & ";" _
                           & sPosteAlerProv.PadRight(2).Substring(0, 2) & ";" _
                           & sPosteAlerCodUtente.PadRight(12).Substring(0, 12) & ";" _
                           & sPosteAlerAcronimo.PadRight(4).Substring(0, 4) & ";" _
                           & sPosteAlerIA.PadRight(16).Substring(0, 16) & ";"


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_RATEIZ_LETTERE (ID,ID_CONTRATTO,ID_BOLLETTA,ID_RATEIZZAZIONE,DATA_GENERAZIONE) " _
                    & "VALUES (SISCOM_MI.SEQ_BOL_RATEIZ_LETTERE.NEXTVAL," & IdContratto & "," & IdBolletaCreata & "," & r.Item("ID_RATEIZZAZIONE") & ",'" & Format(Now, "yyyyMMdd") & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = " select SISCOM_MI.SEQ_BOL_RATEIZ_LETTERE.CURRVAL FROM dual "
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        INDICELETTERA = myReader1(0)
                    End If
                    myReader1.Close()


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.POSTALER (ID,ID_LETTERA,ID_TIPO_LETTERA) " _
                                       & "VALUES (" & sPosteAlerIA & "," & INDICELETTERA & ",2)"
                    par.cmd.ExecuteNonQuery()


                    i = i + 1


                    '*********************FINE POSTALER***************************





                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_RATEIZZAZIONI_DETT SET ID_BOLLETTA = " & IdBolletaCreata & " WHERE ID = " & r.Item("ID")
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & IdBolletaCreata & ",677" _
                                        & "," & par.VirgoleInPunti(r.Item("QUOTA_CAPITALI")) & ")"
                    par.cmd.ExecuteNonQuery()

                    Tot_Bolletta = Tot_Bolletta + r.Item("QUOTA_CAPITALI")


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & IdBolletaCreata & ",678" _
                    & "," & par.VirgoleInPunti(r.Item("QUOTA_INTERESSI")) & ")"
                    par.cmd.ExecuteNonQuery()
                    Tot_Bolletta = Tot_Bolletta + r.Item("QUOTA_INTERESSI")


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                            & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & IdBolletaCreata & ",407" _
                                            & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                    par.cmd.ExecuteNonQuery()

                    Tot_Bolletta = Tot_Bolletta + SPESEmav

                    If Tot_Bolletta >= APPLICABOLLO Then

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & IdBolletaCreata & ",95" _
                                                    & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                        par.cmd.ExecuteNonQuery()
                        Tot_Bolletta = Tot_Bolletta + BOLLO
                    End If


                    If dtBol.Rows(0).Item("ID_TIPO") <> 4 Then
                        par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=31"
                        Dim letty As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If letty.Read Then
                            causalepagamento = par.IfNull(letty("valore"), "")
                        End If
                        letty.Close()
                    Else
                        par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta where id=32"
                        Dim letty As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If letty.Read Then
                            causalepagamento = par.IfNull(letty("valore"), "")
                        End If
                        letty.Close()

                    End If

                    'If Session.Item("AmbienteDiTest") = "1" Then
                    '    causalepagamento = "COMMITEST01"
                    '    'pp.Url = "https://web1.unimaticaspa.it/pagamenti20-test-ws/services/MAVOnline"
                    '    pp.Url = "https://demoweb.infogroup.it/pagamenti20-test-ws/services/MAVOnline"

                    'End If
                    If Session.Item("AmbienteDiTest") = "1" Then
                        causalepagamento = "COMMITEST01"
                        'pp.Url = "https://incassonline-coll.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    Else
                        'pp.Url = "https://incassonline.eng-dhub.it/pagamenti-ws/services/MAVOnlineIG/MavOnLineService"
                        pp.Url = Session.Item("indirizzoMavOnLine")
                    End If

                    RichiestaEmissioneMAV.codiceEnte = "commi"
                    RichiestaEmissioneMAV.tipoPagamento = causalepagamento
                    RichiestaEmissioneMAV.idOperazione = Format(CLng(IdBolletaCreata), "0000000000")
                    RichiestaEmissioneMAV.codiceDebitore = Format(CDbl(dtBol.Rows(0).Item("ID_ANA")), "0000000000")



                    RichiestaEmissioneMAV.causalePagamento = CreaCausale("BOLLETTA RATEIZZAZIONE NUM " & r.Item("NUM_RATA"), IdBolletaCreata)

                    RichiestaEmissioneMAV.scadenzaPagamento = Mid(r.Item("data_emissione"), 1, 4) & "-" & Mid(r.Item("data_emissione"), 5, 2) & "-" & Mid(r.Item("data_emissione"), 7, 2)

                    RichiestaEmissioneMAV.importoPagamentoInCentesimi = Val(Tot_Bolletta * 100)
                    RichiestaEmissioneMAV.userName = Format(CDbl(dtBol.Rows(0).Item("ID_ANA")), "0000000000")
                    RichiestaEmissioneMAV.codiceFiscaleDebitore = CF


                    RichiestaEmissioneMAV.cognomeORagioneSocialeDebitore = Mid(Cognome, 1, 30)
                    If Nome <> "" Then
                        RichiestaEmissioneMAV.nomeDebitore = Mid(Nome, 1, 30)
                    End If


                    If Len(dtBol.Rows(0).Item("INDIRIZZO")) <= 23 Then
                        RichiestaEmissioneMAV.indirizzoDebitore = tipo_cor & " " & indirizzo_cor & ", " & civico_cor
                    Else
                        RichiestaEmissioneMAV.indirizzoDebitore = Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 1, 23)
                        RichiestaEmissioneMAV.frazioneDebitore = Mid(Mid(tipo_cor & " " & indirizzo_cor & ", " & civico_cor, 24, Len(tipo_cor & " " & indirizzo_cor & ", " & civico_cor)), 1, 28)
                    End If

                    RichiestaEmissioneMAV.capDebitore = Mid(cap_cor, 1, 5)
                    RichiestaEmissioneMAV.localitaDebitore = Mid(luogo_cor, 1, 23)
                    RichiestaEmissioneMAV.provinciaDebitore = Mid(sigla_cor, 1, 2)
                    RichiestaEmissioneMAV.nazioneDebitore = "IT"



                    '12/01/2015 PUCCIA Nuova connessione  tls ssl
                    If DisabilitaExpect100Continue = "1" Then
                        System.Net.ServicePointManager.Expect100Continue = False
                    End If
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateHandler

                    '/*/*/*/*/*tls v1
                    Dim v As String = ""
                    par.cmd.CommandText = "select valore from siscom_mi.parametri where parametro='SSL MAV ON LINE'"
                    v = par.cmd.ExecuteScalar
                    System.Net.ServicePointManager.SecurityProtocol = CType(v, Net.SecurityProtocolType)
                    '/*/*/*/*/*tls v1

                    Esito = pp.CreaMAVOnline(RichiestaEmissioneMAV)

                    If Esito.codiceRisultato = "0" Then


                        outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Format(CLng(IdBolletaCreata), "0000000000") & ".pdf"
                        binaryData = System.Convert.FromBase64String(Esito.pdfDocumento)
                        outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length - 1)
                        outFile.Close()


                        num_bollettino = Esito.numeroMAV
                        par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & num_bollettino & "' where  id=" & IdBolletaCreata
                        par.cmd.ExecuteNonQuery()
                        WriteEvent(IdContratto, "F174", "GENERATA BOLLETTA PER LA RATA N." & r.Item("NUM_RATA"))

                        ReDim Preserve elFileCreati(numFileCreati)
                        elFileCreati(numFileCreati) = outputFileName
                        numFileCreati = numFileCreati + 1

                        pdfMerge.AppendPDFFile(outputFileName)
                    Else


                        par.myTrans.Rollback()
                        '*********************CHIUSURA CONNESSIONE**********************
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        Dim FileDaCreare As String = Format(CLng(IdBolletaCreata), "0000000000")
                        If System.IO.File.Exists(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare & ".xml") = True Then
                            FileDaCreare = FileDaCreare & "_" & Format(Now, "yyyyMMddHHmmss")
                        End If


                        outputFileName = Format(CLng(IdBolletaCreata), "0000000000") & ".xml"


                        binaryData = System.Convert.FromBase64String(par.IfNull(Esito.descrizioneTecnicaRisultato, ""))
                        outFile = New System.IO.FileStream(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & FileDaCreare, System.IO.FileMode.Create, System.IO.FileAccess.Write)
                        outFile.Write(binaryData, 0, binaryData.Length)
                        outFile.Close()

                        lblErrore.Visible = True
                        lblErrore.Text = "<a href = " & Chr(34) & "../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/" & FileDaCreare & Chr(34) & " target = " & Chr(34) & "_blank" & Chr(34) & ">Ci sono stati degli errori durante la fase di creazione dei M.A.V.!! Clicca qui per visualizzare.</a>"

                        For i As Integer = 0 To numFileCreati - 1
                            System.IO.File.Delete(elFileCreati(i))
                        Next

                        Exit Sub
                    End If




                End If


            Next



            '*****************CREO IL FILE TXT****************************
            Dim NomeFile1 As String = "Rateizzazione_" & Format(Now, "yyyyMMddHHmmss")
            Dim sr2 As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\RATEIZZAZIONI\") & "POSTALER_" & NomeFile1 & ".txt", False, System.Text.Encoding.Default)
            sr2.WriteLine(sPosteAler)
            sr2.Close()
            Dim urlPostAler As String = Server.MapPath("..\ALLEGATI\CONTRATTI\RATEIZZAZIONI\") & "POSTALER_" & NomeFile1

            '************************CREO IL FILE PDF CONTENENTE TUTTI I MAV
            pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\ALLEGATI\CONTRATTI\RATEIZZAZIONI\") & NomeFile1 & ".pdf")
            Dim url As String = Server.MapPath("..\ALLEGATI\CONTRATTI\RATEIZZAZIONI\") & NomeFile1

            '***********************ZIP DEI DUE FILE GENERATI***********************
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String
            zipfic = Server.MapPath("..\ALLEGATI\CONTRATTI\RATEIZZAZIONI\" & NomeFile1 & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            '***********************ZIP DEL FILE PDF***********************
            Dim strFile As String
            strFile = url & ".pdf"
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

            '***********************ZIP DEL FILE TXT***********************
            strFile = urlPostAler & ".txt"
            strmFile = File.OpenRead(strFile)
            Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
            Dim sFile1 As String = Path.GetFileName(strFile)
            theEntry = New ZipEntry(sFile1)
            fi = New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer1)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)

            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()

            IO.File.Delete(url & ".pdf")
            IO.File.Delete(urlPostAler & ".txt")
            '***********************CHIUSURA ELABORAZIONE FILE ZIP***********************

            par.myTrans.Commit()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            trovaBollRateizz()


            Response.Write("<script>parent.main.location.replace('../RATEIZZAZIONE/ElencoEmissioni.aspx');</script>")



        Catch ex As Exception
            Me.lblErrore.Visible = True
            Me.lblErrore.Text = "BtnConferma - " & ex.Message

            '*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************Annullo Operazioni in transazione**********************
                par.myTrans.Rollback()

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try


    End Sub
    Private Function CreaCausale(ByVal Tipo As String, ByVal idb As Long) As String
        Try
            Dim sCausale As String = ""
            Dim sImporto As String = ""
            Dim iDifferenza As Integer = 0
            Dim sDescrizione As String = ""

            sCausale = ""

            par.cmd.CommandText = "select t_voci_bolletta.descrizione,bol_bollette_voci.importo from siscom_mi.bol_bollette,siscom_mi.t_voci_bolletta,siscom_mi.bol_bollette_voci where bol_bollette_voci.id_bolletta=bol_bollette.id and t_voci_bolletta.id=bol_bollette_voci.id_voce and bol_bollette.id=" & idb & " order by t_voci_bolletta.descrizione asc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                sImporto = Format(par.IfNull(myReader("importo"), "0"), "##,##0.00")

                'If sImporto < 1 And sImporto > 0 Then
                '    sImporto = Format(CDbl(sImporto), "0.00")
                'End If

                If sImporto < 1 And sImporto > 0 Then
                    sImporto = "0" & sImporto
                End If

                If sImporto > -1 And sImporto < 0 Then
                    sImporto = "-0" & Replace(sImporto, "-", "")
                End If

                iDifferenza = 55 - Len(sImporto)
                sDescrizione = par.IfNull(myReader("descrizione"), "")
                sCausale = sCausale & Mid(sDescrizione.PadRight(iDifferenza), 1, iDifferenza) & sImporto
            Loop
            CreaCausale = sCausale
            myReader.Close()
            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "CreaCausale - " & ex.Message
        End Try

    End Function

    Public Sub WriteEvent(ByVal ID_CONTRATTO As String, ByVal CodEvento As String, Optional ByVal Motivazione As String = "")

        Dim ConnOpenNow As Boolean = False
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES ( " & ID_CONTRATTO & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                                & "'" & CodEvento & "','" & par.PulisciStrSql(Motivazione) & "')"
            par.cmd.ExecuteNonQuery()

            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = "WriteEvent - " & ex.Message
            If ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        End Try

    End Sub


End Class
