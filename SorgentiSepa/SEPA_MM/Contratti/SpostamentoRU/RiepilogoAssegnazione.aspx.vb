Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic

Partial Class Contratti_SpostamentoRU_RiepilogoAssegnazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HiddenScelta.Value = Request.QueryString("SCELTA")
            Select Case HiddenScelta.Value
                Case 1
                    lblScelta.Text = "SPOSTAMENTO"
                Case 2
                    lblScelta.Text = "ANNULLAMENTO"
                Case 3
                    lblScelta.Text = "VARIAZIONE DECORRENZA"
                    lblDataAtt.Visible = True
                    txtDataAttuale.Visible = True
                    lblDataNuova.Visible = True
                    txtDataNuova.Visible = True
                    lblDataCons.Visible = True
                    txtDataConsegna.Visible = True
                    VisualizzDataDecorr()
                    txtDataNuova.Attributes.Add("onblur", "javascript:confronta_data(document.getElementById('txtDataAttuale').value,document.getElementById('txtDataNuova').value);")
                    txtDataNuova.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    txtDataConsegna.Attributes.Add("onclick", "javascript:document.getElementById('txtDataConsegna').value=document.getElementById('txtDataNuova').value;")
                    txtDataConsegna.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                    txtDataConsegna.Attributes.Add("onblur", "javascript:confronta_data2(document.getElementById('txtDataNuova').value,document.getElementById('txtDataConsegna').value);")
            End Select
            lblCodContr.Text = Request.QueryString("COD")
            tipoUI = Request.QueryString("TIPOUI")
            CaricaInfoIntestatario()
            CaricaComboMotivi()
        End If
    End Sub

    Public Property idUNITAnew() As Long
        Get
            If Not (ViewState("par_idUNITAnew") Is Nothing) Then
                Return CLng(ViewState("par_idUNITAnew"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idUNITAnew") = value
        End Set

    End Property

    Public Property idContratto() As Long
        Get
            If Not (ViewState("par_idContratto") Is Nothing) Then
                Return CLng(ViewState("par_idContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idContratto") = value
        End Set

    End Property

    Public Property idAnagrafica() As Long
        Get
            If Not (ViewState("par_idAnagrafica") Is Nothing) Then
                Return CLng(ViewState("par_idAnagrafica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAnagrafica") = value
        End Set

    End Property

    Public Property tipoUI() As String
        Get
            If Not (ViewState("par_tipoUI") Is Nothing) Then
                Return CStr(ViewState("par_tipoUI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipoUI") = value
        End Set

    End Property

    Private Sub CaricaComboMotivi()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from SISCOM_MI.MOTIVI_SPOSTAM_ANNULL order by motivazione asc"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbMotivazioni.Items.Add(New ListItem(" - seleziona - ", -1))
            While myReader0.Read
                cmbMotivazioni.Items.Add(New ListItem(myReader0("MOTIVAZIONE"), myReader0("ID")))
            End While
            myReader0.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub VisualizzDataDecorr()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                txtDataAttuale.Text = par.FormattaData(par.IfNull(myReader0("DATA_DECORRENZA"), ""))
            End If
            myReader0.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaInfoIntestatario()
        Try
            'Dim idContratto As Long = 0
            'Dim idAnagrafica As Long = 0
            Dim comuneNasc As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idContratto = par.IfNull(myReader0("ID"), "0")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.SOGGETTI_CONTRATTUALI where ID_CONTRATTO=" & idContratto & " AND COD_TIPOLOGIA_OCCUPANTE ='INTE'"
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), "0")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT * from siscom_mi.ANAGRAFICA where ID=" & idAnagrafica
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                lblCognome.Text = par.IfNull(myReader0("COGNOME"), "")
                lblNome.Text = par.IfNull(myReader0("NOME"), "")
                lblCF.Text = par.IfNull(myReader0("COD_FISCALE"), "")
                lblSesso.Text = par.IfNull(myReader0("SESSO"), "")
                lblDataNasc.Text = par.FormattaData(par.IfNull(myReader0("DATA_NASCITA"), ""))
                lblCittadinanza.Text = par.IfNull(myReader0("CITTADINANZA"), "")

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & par.IfNull(myReader0("COD_COMUNE_NASCITA"), "") & "'"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    comuneNasc = par.IfNull(myReader1("NOME"), "")
                End If
                myReader1.Close()

                lblComNasc.Text = comuneNasc
                If par.IfNull(myReader0("NUM_DOC"), "") <> "" Then
                    Select Case par.IfNull(myReader0("TIPO_DOC"), 0)
                        Case 0
                            lblTipoDoc.Text = "CARTA D'IDENTITA'"
                        Case 1
                            lblTipoDoc.Text = "PASSAPORTO"
                        Case 2
                            lblTipoDoc.Text = "PATENTE DI GUIDA"
                    End Select
                End If
                
                lblNumDoc.Text = par.IfNull(myReader0("NUM_DOC"), "")
                lblDataRil.Text = par.FormattaData(par.IfNull(myReader0("DATA_DOC"), ""))
                lblDocSogg.Text = par.IfNull(myReader0("DOC_SOGGIORNO"), "")
                lblCAP.Text = par.IfNull(myReader0("CAP_RESIDENZA"), "")
                lblComune.Text = par.IfNull(myReader0("COMUNE_RESIDENZA"), "")
                lblProv.Text = par.IfNull(myReader0("PROVINCIA_RESIDENZA"), "")
                lblVia.Text = par.IfNull(myReader0("INDIRIZZO_RESIDENZA"), "")
                lblCivico.Text = par.IfNull(myReader0("CIVICO_RESIDENZA"), "")
                lblTel.Text = par.IfNull(myReader0("TELEFONO"), "")
            End If
            myReader0.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    'Private Sub InsertUIAssegn()
    '    Try
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)

    '        Dim nome As String = ""
    '        Dim cognome As String = ""
    '        Dim cf_iva As String = ""
    '        Dim CanoneCorrente As Decimal = 0
    '        Dim idUI As Long = 0
    '        Dim codUI As String = ""

    '        codUI = Request.QueryString("CODUI")
    '        idUI = Request.QueryString("IDUI")


    '        cognome = lblCognome.Text
    '        nome = lblNome.Text
    '        cf_iva = lblCF.Text


    '        par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where ID=" & idContratto
    '        Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader0.Read Then
    '            CanoneCorrente = par.IfNull(myReader0("imp_canone_iniziale"), "0")
    '            par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.RAPPORTI_UTENZA_AD_CANONE WHERE ID_CONTRATTO=" & idContratto
    '            Dim myReaderX1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReaderX1.Read Then
    '                CanoneCorrente = CanoneCorrente + par.IfNull(myReaderX1(0), 0)
    '            End If
    '            myReaderX1.Close()
    '        End If
    '        myReader0.Close()


    '        par.cmd.CommandText = "Insert into siscom_mi.UNITA_ASSEGNATE (ID_DOMANDA, ID_UNITA, DATA_ASSEGNAZIONE, " _
    '                & "GENERATO_CONTRATTO, ID_DICHIARAZIONE, COGNOME_RS, NOME, CF_PIVA, PROVENIENZA, N_OFFERTA,CANONE,ID_ANAGRAFICA) " _
    '                & " Values " _
    '                & "(-1, " & idUI & ", '" & Format(Now, "yyyymmdd") & "', 0, -1, " _
    '                & "'" & par.PulisciStrSql(cognome) & "', '" & par.PulisciStrSql(nome) _
    '                & "', '" & par.PulisciStrSql(cf_iva) & "', '', 0," & par.VirgoleInPunti(CanoneCorrente) & "," & idAnagrafica & ")"
    '        par.cmd.ExecuteNonQuery()

    '        par.cmd.CommandText = "update SISCOM_MI.UI_USI_DIVERSI set stato='8',assegnato='1',DATA_PRENOTATO='" & Format(Now, "yyyymmdd") & "' where COD_ALLOGGIO='" & codUI & "'"
    '        par.cmd.ExecuteNonQuery()


    '        Response.Write("<script>alert('Operazione Effettuata con successo!');</script>")
    '        btnProcedi.Enabled = False


    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        If Request.QueryString("SCELTA") = "3" Then
            If ConfAnnull.Value = "1" Then
                VariazioneDecorrenza()
            End If
        Else
            If ConfAnnull.Value = "1" Then
                ChiudiContratto()
            End If
        End If
    End Sub

    Private Sub VariazioneDecorrenza()
        Try
            Dim dataDecorrenza As String = ""
            Dim numRateCanone As Integer = 0
            Dim dataFineBollett As String = ""
            Dim giorniBollettati As Integer = 0
            Dim importoPagatoCANONE As Decimal = 0
            Dim importoPagatoTOT As Decimal = 0
            Dim importoGiornalieroCANONE As Decimal = 0
            Dim importoGiornaliero300 As Decimal = 0
            Dim importoGiornaliero301 As Decimal = 0
            Dim importoGiornaliero302 As Decimal = 0
            Dim importoGiornaliero303 As Decimal = 0
            Dim dataNuovaDecorr As String = ""
            Dim creditoCANONE As Decimal = 0
            Dim creditoSPESA300 As Decimal = 0
            Dim creditoSPESA301 As Decimal = 0
            Dim creditoSPESA302 As Decimal = 0
            Dim creditoSPESA303 As Decimal = 0
            Dim creditoTOTALE As Decimal = 0
            Dim giorniDaBollettare As Integer = 0
            Dim nuovoImportoDaBollett As Decimal = 0
            Dim registrazioneXML As String = ""
            Dim impCanoneIniz As Decimal = 0
            Dim importoNewBolletta As Decimal = 0
            'Dim importoNew300 As Decimal = 0
            'Dim importoNew301 As Decimal = 0
            'Dim importoNew302 As Decimal = 0
            'Dim importoNew303 As Decimal = 0
            Dim dataProssimoPeriodo As String = ""
            Dim rataProssima As Integer = 0
            Dim annoDecorrenza As Integer = 0
            Dim durataAnni As Integer = 0
            Dim durataAnniRinn As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idContratto = par.IfNull(myReader0("ID"), "")
                dataDecorrenza = par.IfNull(myReader0("DATA_DECORRENZA"), "")
                numRateCanone = par.IfNull(myReader0("NRO_RATE"), 4)
                registrazioneXML = par.IfNull(myReader0("REG_TELEMATICA"), "")
                impCanoneIniz = par.IfNull(myReader0("IMP_CANONE_INIZIALE"), 0)
                durataAnni = par.IfNull(myReader0("DURATA_ANNI"), 0)
                durataAnniRinn = par.IfNull(myReader0("DURATA_RINNOVO"), 0)
            End If
            myReader0.Close()

            'DESCRIZIONE = "dal " & CType(Tab_Contratto1.FindControl("txtDataDecorrenza"), TextBox).Text & " al " & Format(DateAdd(DateInterval.Day, -1, CDate(par.FormattaData(dataProssimoPeriodo))), "dd/MM/yyyy")



            Dim prossimaBolletta As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_prossima_bol WHERE ID_CONTRATTO=" & idContratto
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                prossimaBolletta = par.IfNull(myReader0("PROSSIMA_BOLLETTA"), "")

            End If
            myReader0.Close()


            Dim idUnita As Long = 0
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.unita_contrattuale WHERE ID_CONTRATTO=" & idContratto
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idUnita = par.IfNull(myReader0("ID_UNITA"), 0)
            End If
            myReader0.Close()

            Dim mesePrecUltimaBolletta As String = ""

            If Mid(prossimaBolletta, 5, 1) = "0" Then
                If Mid(prossimaBolletta, 5, 4) = "01" Then
                    mesePrecUltimaBolletta = "12"
                Else
                    mesePrecUltimaBolletta = "0" & Mid(prossimaBolletta, 6, 4) - 1
                End If
            Else
                mesePrecUltimaBolletta = Mid(prossimaBolletta, 5, 4) - 1
            End If

            Dim annoBolletta As Integer = Mid(prossimaBolletta, 1, 4)

            If mesePrecUltimaBolletta = "12" Then
                annoBolletta = annoBolletta - 1
            End If

            Select Case mesePrecUltimaBolletta
                Case "01", "03", "05", "07", "08", "10", "12"
                    dataFineBollett = annoBolletta & mesePrecUltimaBolletta & "31"
                Case "02"
                    If Date.IsLeapYear(annoBolletta) = True Then 'CONTROLLO SE L'ANNO E' BISESTILE
                        dataFineBollett = annoBolletta & mesePrecUltimaBolletta & "29"
                    Else
                        dataFineBollett = annoBolletta & mesePrecUltimaBolletta & "28"
                    End If
                Case Else
                    dataFineBollett = annoBolletta & mesePrecUltimaBolletta & "30"
            End Select

            '******* CALCOLO I GIORNI BOLLETTATI CONSIDERANDO I MESI SEMPRE DI 30 GIORNI (DATEDIFF(DateInterval.Days) CONSIDERA INVECE I GIORNI EFFETTIVI DI CIASCUN MESE)
            Dim numMesiBolle As Integer = 0
            numMesiBolle = DateDiff(DateInterval.Month, CDate(par.FormattaData(dataDecorrenza)), CDate(par.FormattaData(dataFineBollett))) - 1
            giorniBollettati = numMesiBolle * 30
            Dim giorniPrimoMese As Integer = 0
            Dim giorniUltimoMese As Integer = 0
            If CInt(dataDecorrenza.Substring(6, 2)) > 30 Then
                giorniPrimoMese = 30
            Else
                giorniPrimoMese = (30 - dataDecorrenza.Substring(6, 2)) + 1
            End If
            If CInt(dataFineBollett.Substring(6, 2)) = 28 And CInt(dataFineBollett.Substring(4, 2)) = 2 Then
                giorniUltimoMese = 30
            Else
                If giorniUltimoMese = 31 Then
                    giorniUltimoMese = 30
                Else
                    giorniUltimoMese = dataFineBollett.Substring(6, 2)
                End If
            End If


            giorniBollettati = giorniBollettati + giorniPrimoMese + giorniUltimoMese

            'giorniBollettati = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataDecorrenza)), CDate(par.FormattaData(dataFineBollett)))

            importoPagatoCANONE = (impCanoneIniz / 12) / 30


            Dim importoSpesa300 As Decimal = 0
            Dim importoSpesa301 As Decimal = 0
            Dim importoSpesa302 As Decimal = 0
            Dim importoSpesa303 As Decimal = 0
            Dim importoSpesaTOT As Decimal = 0

            par.cmd.CommandText = "SELECT * from SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & Year(Now) & ""
            Dim daSpese As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtSpese As New Data.DataTable
            daSpese = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daSpese.Fill(dtSpese)
            daSpese.Dispose()
            For Each row As Data.DataRow In dtSpese.Rows
                Select Case row.Item("ID_VOCE")
                    Case "300"
                        importoSpesa300 = row.Item("IMPORTO")
                    Case "301"
                        importoSpesa301 = row.Item("IMPORTO")
                    Case "302"
                        importoSpesa302 = row.Item("IMPORTO")
                    Case "303"
                        importoSpesa303 = row.Item("IMPORTO")
                End Select
            Next

            importoGiornaliero300 = importoSpesa300 / giorniBollettati
            importoGiornaliero301 = importoSpesa301 / giorniBollettati
            importoGiornaliero302 = importoSpesa302 / giorniBollettati
            importoGiornaliero303 = importoSpesa303 / giorniBollettati

            
            importoSpesaTOT = ((importoGiornaliero300 * 30) + (importoGiornaliero301 * 30) + (importoGiornaliero302 * 30) + (importoGiornaliero303 * 30))

            importoPagatoCANONE = (importoPagatoCANONE * giorniBollettati)


            'importoPagatoTOT = importoPagatoCANONE + importoSpesaTOT

            If importoPagatoCANONE <> 0 Then

                importoGiornalieroCANONE = importoPagatoCANONE / giorniBollettati
                'importoSpesaTOT = importoSpesaTOT / giorniBollettati
                

                dataNuovaDecorr = txtDataNuova.Text

                annoDecorrenza = Year(CDate(dataNuovaDecorr))

                'giorniDaBollettare = DateDiff(DateInterval.Day, CDate(par.FormattaData(dataNuovaDecorr)), CDate(par.FormattaData(dataFineBollett)))
                Dim numMesiDaBolle As Integer = 0
                numMesiDaBolle = DateDiff(DateInterval.Month, CDate(par.FormattaData(dataNuovaDecorr)), CDate(par.FormattaData(dataFineBollett))) - 1
                giorniDaBollettare = numMesiDaBolle * 30
                Dim giorniPrimoMeseDaB As Integer = 0
                Dim giorniUltimoMeseDaB As Integer = 0
                If CInt(dataNuovaDecorr.Substring(6, 2)) > 30 Then
                    giorniPrimoMeseDaB = 30
                Else
                    giorniPrimoMeseDaB = (30 - dataNuovaDecorr.Substring(6, 2)) + 1
                End If
                If CInt(dataFineBollett.Substring(6, 2)) = 28 And CInt(dataFineBollett.Substring(4, 2)) = 2 Then
                    giorniUltimoMeseDaB = 30
                Else
                    If giorniUltimoMeseDaB = 31 Then
                        giorniUltimoMeseDaB = 30
                    Else
                        giorniUltimoMeseDaB = dataFineBollett.Substring(6, 2)
                    End If
                End If

                giorniDaBollettare = giorniDaBollettare + giorniPrimoMeseDaB + giorniUltimoMeseDaB


                If giorniDaBollettare > 0 Then
                    nuovoImportoDaBollett = importoGiornalieroCANONE * giorniDaBollettare

                    '***** CREDITO DOVUTO AL PERIODO DI INUTILIZZO DA RIPORTARE NELLA PROSSIMA BOLLETTA *****
                    creditoCANONE = Format(importoPagatoCANONE - nuovoImportoDaBollett, "##,##0.00")

                    If importoSpesaTOT <> 0 Then
                        If importoSpesa300 <> 0 Then
                            nuovoImportoDaBollett = importoGiornaliero300 * giorniDaBollettare
                            creditoSPESA300 = Format(importoSpesa300 - nuovoImportoDaBollett, "##,##0.00")
                        End If
                        If importoSpesa301 <> 0 Then
                            nuovoImportoDaBollett = importoGiornaliero301 * giorniDaBollettare
                            creditoSPESA301 = Format(importoSpesa301 - nuovoImportoDaBollett, "##,##0.00")
                        End If
                        If importoSpesa302 <> 0 Then
                            nuovoImportoDaBollett = importoGiornaliero302 * giorniDaBollettare
                            creditoSPESA302 = Format(importoSpesa302 - nuovoImportoDaBollett, "##,##0.00")
                        End If
                        If importoSpesa303 <> 0 Then
                            nuovoImportoDaBollett = importoGiornaliero303 * giorniDaBollettare
                            creditoSPESA303 = Format(importoSpesa303 - nuovoImportoDaBollett, "##,##0.00")
                        End If
                    End If
                    creditoTOTALE = creditoCANONE + creditoSPESA300 + creditoSPESA301 + creditoSPESA302 + creditoSPESA303
                Else
                    creditoCANONE = Format(importoPagatoCANONE, "##,##0.00")
                    If importoSpesaTOT <> 0 Then
                        creditoSPESA300 = Format(importoSpesa300, "##,##0.00")
                        creditoSPESA301 = Format(importoSpesa301, "##,##0.00")
                        creditoSPESA302 = Format(importoSpesa302, "##,##0.00")
                        creditoSPESA303 = Format(importoSpesa303, "##,##0.00")
                    End If
                    creditoTOTALE = creditoCANONE + creditoSPESA300 + creditoSPESA301 + creditoSPESA302 + creditoSPESA303
                End If

                rataProssima = par.ProssimaRata(numRateCanone, par.AggiustaData(dataNuovaDecorr), dataProssimoPeriodo)

                If rataProssima < Mid(prossimaBolletta, 5, 4) Then
                    If annoDecorrenza = annoBolletta Then
                        rataProssima = Mid(prossimaBolletta, 5, 4)
                    End If
                End If


                'importoNewBolletta = PrevisioneNuovaBoll(impCanoneIniz, numRateCanone, rataProssima, annoBolletta)
                importoNewBolletta = Format((impCanoneIniz / numRateCanone) + importoSpesaTOT, "##,##0.00")

                Dim rate As Integer = 0
                Dim ESERCIZIOF As Long = 0
                ESERCIZIOF = par.RicavaEsercizioCorrente

                Dim impRateizzato As Decimal = 0
                If creditoCANONE > importoNewBolletta Then
                    rate = Format(creditoCANONE / importoNewBolletta, "##,##0.00")
                    rate = Format(rate, "0")
                    impRateizzato = Format(creditoCANONE / rate, "##,##0.00")

                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 118, " & (par.VirgoleInPunti(creditoCANONE * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato * (-1))) & " , " & annoDecorrenza & ")"
                    par.cmd.ExecuteNonQuery()

                Else
                    rate = 1

                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 118, " & (par.VirgoleInPunti(creditoCANONE * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(creditoCANONE * (-1))) & " , " & annoDecorrenza & ")"
                    par.cmd.ExecuteNonQuery()

                End If

                If importoSpesa300 <> 0 Then
                    If creditoSPESA300 > importoNewBolletta Then
                        rate = Format(creditoSPESA300 / importoNewBolletta, "##,##0.00")
                        rate = Format(rate, "0")
                        impRateizzato = Format(creditoSPESA300 / rate, "##,##0.00")

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 300, " & (par.VirgoleInPunti(creditoSPESA300 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    Else
                        rate = 1

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 300, " & (par.VirgoleInPunti(creditoSPESA300 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(creditoSPESA300 * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    End If
                End If

                If importoSpesa301 <> 0 Then
                    If creditoSPESA301 > importoNewBolletta Then
                        rate = Format(creditoSPESA301 / importoNewBolletta, "##,##0.00")
                        rate = Format(rate, "0")
                        impRateizzato = Format(creditoSPESA301 / rate, "##,##0.00")

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 301, " & (par.VirgoleInPunti(creditoSPESA301 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    Else
                        rate = 1

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 301, " & (par.VirgoleInPunti(creditoSPESA301 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(creditoSPESA301 * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    End If
                End If

                If importoSpesa302 <> 0 Then
                    If creditoSPESA302 > importoNewBolletta Then
                        rate = Format(creditoSPESA302 / importoNewBolletta, "##,##0.00")
                        rate = Format(rate, "0")
                        impRateizzato = Format(creditoSPESA302 / rate, "##,##0.00")

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 302, " & (par.VirgoleInPunti(creditoSPESA302 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    Else
                        rate = 1

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 302, " & (par.VirgoleInPunti(creditoSPESA302 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(creditoSPESA302 * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    End If
                End If

                If importoSpesa303 <> 0 Then
                    If creditoSPESA303 > importoNewBolletta Then
                        rate = Format(creditoSPESA303 / importoNewBolletta, "##,##0.00")
                        rate = Format(rate, "0")
                        impRateizzato = Format(creditoSPESA303 / rate, "##,##0.00")

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 303, " & (par.VirgoleInPunti(creditoSPESA303 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    Else
                        rate = 1

                        par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO) Values (siscom_mi.seq_bol_schema.nextval, " & idContratto & ", " & idUnita & ", " & ESERCIZIOF & ", 303, " & (par.VirgoleInPunti(creditoSPESA303 * (-1))) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(creditoSPESA303 * (-1))) & " , " & annoDecorrenza & ")"
                        par.cmd.ExecuteNonQuery()

                    End If
                End If



                dataScadenza1.Value = Format(DateAdd(DateInterval.Year, durataAnni, CDate(par.FormattaData(dataNuovaDecorr))), "dd/MM/yyyy")
                dataScadenza2.Value = Format(DateAdd(DateInterval.Year, durataAnniRinn, CDate(par.FormattaData(dataScadenza1.Value))), "dd/MM/yyyy")


                '***** AGGIORNO IL CONTRATTO CON LA NUOVA DATA DI DECORRENZA/CONSEGNA *****
                If registrazioneXML = "" Then
                    If Not IsNothing(Session.Item("lIdConnessione")) Then
                        Dim par1 As New CM.Global
                        par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
                        par1.cmd = par1.OracleConn.CreateCommand()
                        par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)
                        ‘'par1.cmd.Transaction = par1.myTrans

                        par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET DATA_DECORRENZA = '" & par.AggiustaData(dataNuovaDecorr) & "',DATA_CONSEGNA='" & par.AggiustaData(txtDataConsegna.Text) & "'," _
                            & "DATA_SCADENZA='" & par.AggiustaData(dataScadenza1.Value) & "',DATA_SCADENZA_RINNOVO='" & par.AggiustaData(dataScadenza2.Value) & "' where ID=" & idContratto
                        par1.cmd.ExecuteNonQuery()

                        par1.myTrans.Commit()
                        par1.myTrans = par1.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
                        par1.Dispose()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                End If
            End If


            '*** METTERE NUOVO EVENTO PER LA VARIAZIONE DELLA DATA DI DECORRENZA ***
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F191','" & par.PulisciStrSql(cmbMotivazioni.SelectedItem.Text) & " (DATA DECORRENZA INIZIALE: " & par.FormattaData(dataDecorrenza) & " - DATA DECORRENZA NUOVA: " & par.FormattaData(dataNuovaDecorr) & ")')"
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F05',' (IMPORTI STORNATI PER VARIAZIONE DECORRENZA)')"
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione Effettuata con successo! Il credito calcolato è stato memorizzato nello schema bollette.');opener.document.getElementById('dataScad1').value = '" & dataScadenza1.Value & "';opener.document.getElementById('dataScad2').value = '" & dataScadenza2.Value & "';opener.document.getElementById('imgSalva').click();self.close();</script>")

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function PrevisioneNuovaBoll(ByVal impCanoneIniz As Decimal, ByVal numrateCanone As Integer, ByVal rataProssima As Integer, ByVal annoBolletta As Integer) As Decimal
        Dim canoneMensile As Decimal = 0
        Dim adeguamCanone As Decimal = 0
        Dim importoSchemaBoll As Decimal = 0
        Dim sommaTOT As Decimal = 0

        Try
            canoneMensile = impCanoneIniz / numrateCanone

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA_AD_CANONE where ID_CONTRATTO=" & idContratto
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable()
            da1.Fill(dt)
            da1.Dispose()
            If dt.Rows.Count > 0 Then
                For Each row As Data.DataRow In dt.Rows
                    adeguamCanone = adeguamCanone + (par.IfNull(row.Item("IMPORTO"), 0) / numrateCanone)
                Next
            End If

            par.cmd.CommandText = "SELECT SUM(IMPORTO_SINGOLA_RATA) AS IMP_BOLLETTA FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & annoBolletta & " AND DA_RATA=" & rataProssima
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoSchemaBoll = par.IfNull(myReader0("IMP_BOLLETTA"), 0)
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT SUM(IMPORTO_SINGOLA_RATA) AS IMP_BOLLETTA FROM SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & annoBolletta & " AND PER_RATE=" & numrateCanone
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoSchemaBoll = importoSchemaBoll + par.IfNull(myReader0("IMP_BOLLETTA"), 0)
            End If
            myReader0.Close()

            sommaTOT = canoneMensile + adeguamCanone + importoSchemaBoll

            If sommaTOT > 71.469999999999999 Then
                sommaTOT = sommaTOT + 1.8100000000000001
            End If

            sommaTOT = Format(sommaTOT, "##,##0.00")

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return sommaTOT

    End Function

    Private Sub ChiudiContratto()
        Try
            Dim idContratto As Long = 0
            Dim codUIold As String = ""
            Dim idUNITAOLD As Long = 0
            Dim dataConsegna As String = ""
            Dim dataRiconsegna As String = ""
            Dim idRUCambioUI As Long = 0
            Dim idDomBANDO As Long = 0

            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT * from siscom_mi.RAPPORTI_UTENZA where COD_CONTRATTO='" & Request.QueryString("COD") & "'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idContratto = par.IfNull(myReader0("ID"), "")
                dataConsegna = par.IfNull(myReader0("DATA_CONSEGNA"), "")
                idDomBANDO = par.IfNull(myReader0("ID_DOMANDA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID AND " _
            & " RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & idContratto
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                codUIold = par.IfNull(myReader0("COD_UNITA_IMMOBILIARE"), "")
                idUNITAOLD = par.IfNull(myReader0("ID"), "")
            End If
            myReader0.Close()

            'IMPOSTO DATA SLOGGIO UGUALE ALLA DATA DI CONSEGNA IN QUANTO L'INQUILINO NON E' MAI ENTRATO
            dataRiconsegna = dataConsegna


            'LIBERO L'UNITA' IMMOBILIARE
            par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='LIBE' where id=" & idUNITAOLD
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "update siscom_mi.unita_immobiliari set cod_tipo_disponibilita='LIBE' where id_unita_principale=" & idUNITAOLD
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE ALLOGGI SET data_disponibilita='" & dataRiconsegna & "',PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null,stato='5' WHERE COD_ALLOGGIO='" & codUIold & "'"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.ui_usi_diversi SET data_disponibilita='" & dataRiconsegna & "',PRENOTATO='0', ASSEGNATO='0', ID_PRATICA=null,stato='5' WHERE COD_ALLOGGIO='" & codUIold & "'"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO siscom_mi.RU_CAMBIO_UI (ID,ID_ANAGRAFICA,ID_CONTRATTO_OLD,ID_MOTIVAZIONE,DATA) VALUES (SISCOM_MI.SEQ_RU_CAMBIO_UI.NEXTVAL," & idAnagrafica & "," & idContratto & "," & cmbMotivazioni.SelectedValue & ",'" & Format(Now, "yyyyMMdd") & "')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_RU_CAMBIO_UI.CURRVAL FROM DUAL"
            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderS.Read Then
                idRUCambioUI = myReaderS(0)
            End If
            myReaderS.Close()

            Dim note As String = ""


            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            da1.Dispose()
            For Each row As Data.DataRow In dt1.Rows

                note = "(storno) " & row.Item("NOTE")

                par.cmd.CommandText = "INSERT INTO siscom_mi.BOL_BOLLETTE ( ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO," _
                & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, FL_ANNULLATA," _
                & "PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA," _
                & "RIFERIMENTO_A, FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, ANNO," _
                & "OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG, RIF_FILE, RIF_BOLLETTINO," _
                & "RIF_FILE_TXT, DATA_VALUTA, DATA_VALUTA_CREDITORE, RIF_CONTABILE, RIF_FILE_RENDICONTO, DATA_ANNULLO," _
                & "FL_INCASSI, DATA_MORA, IMPORTO_TOTALE, NUM_BOLLETTA, ID_MOROSITA, ID_TIPO, ID_BOLLETTA_RIC," _
                & "FL_SOLLECITO, IMP_TMP, ID_RATEIZZAZIONE, IMP_PAGATO_OLD, QUOTA_SIND_B, IMPORTO_RIC_B," _
                & "QUOTA_SIND_PAGATA_B, IMPORTO_RIC_PAGATO_B, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO," _
                & "FL_PAG_PARZ, ID_MOROSITA_LETTERA, FL_PAG_MAV, ID_STATO ) VALUES ( " _
                & "SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") & "', '', '', '', '', '" & par.PulisciStrSql(note) & "', " & idContratto & "" _
                & ", " & par.IfNull(row.Item("ID_ESERCIZIO_F"), "") & ", " & row.Item("ID_UNITA") & ", '0','" & par.IfNull(row.Item("PAGABILE_PRESSO"), "") & "'," & par.IfNull(row.Item("COD_AFFITTUARIO"), "") & ", '" & par.PulisciStrSql(row.Item("INTESTATARIO")) & "', '" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) & "'" _
                & ", '" & par.IfNull(row.Item("PRESSO"), "") & "', '" & Format(Now, "yyyyMMdd") & "', '" & Format(Now, "yyyyMMdd") & "', '" & par.IfNull(row.Item("FL_STAMPATO"), "") & "', " & par.IfNull(row.Item("ID_COMPLESSO"), "") & ", '',0, '', " & par.IfNull(row.Item("ANNO"), "") & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), "") & "" _
                & ", '', '', '" & par.IfNull(row.Item("RIF_FILE"), "") & "', '" & par.IfNull(row.Item("RIF_BOLLETTINO"), "") & "', '" & par.IfNull(row.Item("RIF_FILE_TXT"), "") & "', '', '', '" & par.IfNull(row.Item("RIF_CONTABILE"), "") & "', '" & par.IfNull(row.Item("RIF_FILE_RENDICONTO"), "") & "', '', " & par.IfNull(row.Item("FL_INCASSI"), "") & ", '', " & par.VirgoleInPunti((row.Item("IMPORTO_TOTALE") * (-1))) & ", '" & row.Item("NUM_BOLLETTA") & "'" _
                & ", " & par.IfNull(row.Item("ID_MOROSITA"), "NULL") & ", " & par.IfNull(row.Item("ID_TIPO"), "") & ", " & par.IfNull(row.Item("ID_BOLLETTA_RIC"), "NULL") & "," & par.IfNull(row.Item("FL_SOLLECITO"), "NULL") & "," & par.IfNull(row.Item("IMP_TMP"), "NULL") & "," & par.IfNull(row.Item("ID_RATEIZZAZIONE"), "NULL") & ", " & par.IfNull(row.Item("IMP_PAGATO_OLD"), "NULL") & "," & par.IfNull(row.Item("QUOTA_SIND_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RIC_B"), "NULL") & "," _
                & par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), "NULL") & "," & par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), "NULL") & "," & par.IfNull(row.Item("FL_PAG_PARZ"), "NULL") & "," & par.IfNull(row.Item("ID_MOROSITA_LETTERA"), "NULL") & "," & par.IfNull(row.Item("FL_PAG_MAV"), "NULL") & "," & par.IfNull(row.Item("ID_STATO"), "NULL") & ")"
                par.cmd.ExecuteNonQuery()
            Next

            'VERIFICO LA PRESENZA DI BOLLETTE PAGATE
            Dim pagate As Boolean = False
            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & idContratto & " AND (NVL(IMPORTO_PAGATO,0)>0 OR (FL_ANNULLATA<>0 AND NVL(IMPORTO_PAGATO,0)>0))"
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable
            da2.Fill(dt2)
            da2.Dispose()
            If dt2.Rows.Count > 0 Then
                pagate = True
            End If

            If HiddenScelta.Value = "1" Then
                If pagate = False Then
                    par.cmd.CommandText = "INSERT INTO siscom_mi.RU_CAMBIO_UI_CREDITI (ID,ID_RU_CAMBIO_UI,ID_VOCE,IMPORTO,FL_RIASSEGNATO) VALUES (SISCOM_MI.SEQ_RU_CAMBIO_UI_CREDITI.NEXTVAL," & idRUCambioUI & ",NULL,0,0)"
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            par.cmd.CommandText = "SELECT BOL_BOLLETTE.ID,BOL_BOLLETTE.ID_TIPO,BOL_BOLLETTE_VOCI.ID_VOCE,BOL_BOLLETTE_VOCI.IMPORTO,BOL_BOLLETTE_VOCI.IMP_PAGATO from siscom_mi.BOL_BOLLETTE,siscom_mi.BOL_BOLLETTE_VOCI where BOL_BOLLETTE_VOCI.ID_BOLLETTA = BOL_BOLLETTE.ID AND ID_CONTRATTO=" & idContratto
            Dim da3 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt3 As New Data.DataTable
            da3.Fill(dt3)
            da3.Dispose()
            For Each row3 As Data.DataRow In dt3.Rows
                If HiddenScelta.Value = "1" Then
                    If pagate = True Then
                        If par.IfNull(row3.Item("IMP_PAGATO"), 0) <> 0 Then
                            par.cmd.CommandText = "INSERT INTO siscom_mi.RU_CAMBIO_UI_CREDITI (ID,ID_RU_CAMBIO_UI,ID_VOCE,IMPORTO,FL_RIASSEGNATO,ID_TIPO_BOLL) VALUES (SISCOM_MI.SEQ_RU_CAMBIO_UI_CREDITI.NEXTVAL," & idRUCambioUI & "," & row3.Item("ID_VOCE") & "," & par.VirgoleInPunti(par.IfEmpty(row3.Item("IMPORTO"), 0) * (-1)) & ",0," & par.IfNull(row3.Item("ID_TIPO"), 0) & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                End If
            Next

            par.cmd.CommandText = "SELECT cond_edifici.* FROM SISCOM_MI.COND_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=" & idUNITAOLD & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND COND_EDIFICI.ID_EDIFICIO=EDIFICI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AVVISI (ID_TIPO,ID_UI,DATA,VISTO,ID_CONDOMINIO,ID_CONTRATTO) VALUES (1," & idUNITAOLD & ",'" & Format(Now, "yyyyMMdd") & "',0," & myReader("ID_CONDOMINIO") & "," & idContratto & ")"
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()

            Dim strMotivazSpostam As String = ""
            If HiddenScelta.Value = "1" Then
                strMotivazSpostam = " (spostamento contratto per altra UI)"
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES (" & idContratto & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F18','" & par.PulisciStrSql(cmbMotivazioni.SelectedItem.Text) & strMotivazSpostam & "')"
            par.cmd.ExecuteNonQuery()

            If idDomBANDO >= 500000 Then
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET fl_proposta='0',id_stato='9' WHERE ID=" & idDomBANDO & ""
                par.cmd.ExecuteNonQuery()
            ElseIf idDomBANDO >= 800000 Then
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET fl_proposta='0',id_stato='9' WHERE ID=" & idDomBANDO & ""
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET fl_proposta='0',id_stato='9' WHERE ID=" & idDomBANDO & ""
                par.cmd.ExecuteNonQuery()
            End If

            par.myTrans.Commit()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If Not IsNothing(Session.Item("lIdConnessione")) Then
                Dim par1 As New CM.Global
                par1.OracleConn = CType(HttpContext.Current.Session.Item(Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleConnection)
                par1.cmd = par1.OracleConn.CreateCommand()
                par1.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & Session.Item("lIdConnessione")), Oracle.DataAccess.Client.OracleTransaction)
                ‘'par1.cmd.Transaction = par1.myTrans

                par1.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET DATA_RICONSEGNA = '" & dataRiconsegna & "' where ID=" & idContratto
                par1.cmd.ExecuteNonQuery()

                par1.myTrans.Commit()
                par1.myTrans = par1.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & Session.Item("lIdConnessione"), par1.myTrans)
                par1.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Response.Write("<script>alert('Operazione Effettuata con successo!');</script>")
            btnProcedi.Enabled = False

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property codiceUI() As String
        Get
            If Not (ViewState("par_codiceUI") Is Nothing) Then
                Return CStr(ViewState("par_codiceUI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_codiceUI") = value
        End Set

    End Property

    
End Class
