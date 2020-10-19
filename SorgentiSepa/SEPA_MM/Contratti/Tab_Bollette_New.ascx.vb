Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Tab_Bollette
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim SumImportoVOCI As Object

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        'lstBollette.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Bollette1_lstBollette');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Bollette1_V1').value=obj1.options[obj1.selectedIndex].text;document.getElementById('Tab_Bollette1_V3').value=obj1.options[obj1.selectedIndex].value;}")
        lstVociBolletta.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Bollette1_lstVociBolletta');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Bollette1_V2').value=obj1.options[obj1.selectedIndex].text;}")
        lstVociBoll.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Bollette1_lstVociBoll');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Bollette1_V2').value=obj1.options[obj1.selectedIndex].text;}")
        Me.cmbEmetti.Attributes.Add("onchange", "javascript:NascondiDivEmissione();")
        MenuStampeG.Attributes.Add("onclick", "javascript:document.getElementById('Tab_Bollette1_menuGest').value='1';")
        MenuStampeC.Attributes.Add("onclick", "javascript:document.getElementById('Tab_Bollette1_menuGest').value='0';")

        CaricaMenuC()
        CaricaMenuG()
        If Not IsPostBack Then
            btnStorno.Style("visibility") = "hidden"
            txtEmissione.Text = Format(Now, "dd/MM/yyyy")
            txtDataEmiss.Text = Format(Now, "dd/MM/yyyy")
            'SettaDimensioneCella()
            
            RicavaPercentSost()
            'CType(Me.Page, Object).CaricaTabBollette()

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_BOLLETTE_GEST where UTILIZZABILE=1 ORDER BY DESCRIZIONE ASC", cmbTipoGest, "ID", "DESCRIZIONE", True)
            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_BOLLETTE ORDER BY DESCRIZIONE ASC", cmbTipoCont, "ID", "DESCRIZIONE", True)
        Else
            'par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_BOLLETTE_GEST where ID <= 6 ORDER BY DESCRIZIONE ASC", cmbTipoGest, "ID", "DESCRIZIONE", True)
        End If

    End Sub


    Protected Sub btnAnnullaBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnullaBolletta.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        Dim MiaVar1 As String = ""
        Dim MiaVar2 As String = ""


        If txtannullo.Value = "1" Then
            If lstBollette.SelectedIndex >= 0 Then
                Dim i As Integer
                For i = 0 To lstBollette.Items.Count - 1
                    MiaVar1 = Replace(Replace(Trim(lstBollette.Items(i).Text), Chr(160), ""), " ", "")
                    MiaVar2 = Replace(Replace(Trim(V1.Value), Chr(160), ""), " ", "")

                    If Replace(Replace(MiaVar1, Chr(13), ""), Chr(10), "") = Replace(Replace(MiaVar2, Chr(13), ""), Chr(10), "") Then

                        Try
                            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                            '‘par.cmd.Transaction = par.myTrans

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & lstBollette.Items(i).Value
                            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderJ.Read Then
                                If par.IfNull(myReaderJ("id_rateizzazione"), "0") = "0" And par.IfNull(myReaderJ("ID_TIPO"), "0") <> "4" And par.IfNull(myReaderJ("ID_TIPO"), "0") <> "5" And par.IfNull(myReaderJ("FL_ANNULLATA"), "0") = "0" And par.IfNull(myReaderJ("data_pagamento"), "") = "" And par.IfNull(myReaderJ("id_bolletta_ric"), "0") = "0" Then
                                    'VERIFICARE ANNULLO DEL MAV ON LINE
                                    If par.IfNull(myReaderJ("RIF_FILE"), "") = "" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "MAV" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "MOD" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "FIN" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "REC" Then
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='1',DATA_ANNULLO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & lstBollette.Items(i).Value
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='2',DATA_ANNULLO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & lstBollette.Items(i).Value
                                        par.cmd.ExecuteNonQuery()
                                        'Response.Write("<script>alert('Si ricorda che per annullare definitivamente la bolletta è necessario usare la funzione ANNULLI nel menu a sinistra!');</script>")
                                        Response.Write("<script>alert('Bolletta Annullata!');</script>")

                                    End If

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                            & "VALUES (" & txtIdContratto.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                            & "'F07','BOLL. NUMERO " & par.PulisciStrSql(Mid(Trim(lstBollette.Items(i).Text), 1, 2)) & "')"
                                    par.cmd.ExecuteNonQuery()


                                    lstBollette.Items(i).Text = Replace(lstBollette.Items(i).Text, "VALIDA" & Chr(160), "ANNUL." & Chr(160))

                                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                                Else
                                    Response.Write("<script>alert('Non è possibile procedere...Potrebbe trattarsi di Bolletta già annullata, già pagata, riclassificata, morosità o rateizzazione!');</script>")
                                End If
                            End If
                            myReaderJ.Close()
                            Exit Sub
                        Catch ex As Exception
                            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                            Session.Item("LAVORAZIONE") = "0"
                            par.myTrans.Rollback()
                            par.OracleConn.Close()
                            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                        End Try
                    End If
                Next
            Else
                Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
            End If
        End If
    End Sub

    'Protected Sub btnAnteprimaBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnteprimaBolletta.Click
    '    Dim NomeFile As String

    '    Try
    '        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '        If lstBollette.SelectedIndex >= 0 Then
    '            Dim i As Integer
    '            For i = 0 To lstBollette.Items.Count - 1
    '                If Trim(lstBollette.Items(i).Text) = Trim(V1.Value) Then
    '                    Try
    '                        par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
    '                        par.SettaCommand(par)
    '                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
    '                        ‘‘par.cmd.Transaction = par.myTrans

    '                        NomeFile = Format(Now, "yyyyMMddHHmmss")

    '                        'apro e memorizzo il testo base del contratto

    '                        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\LetteraFattura.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
    '                        Dim contenuto As String = sr1.ReadToEnd()
    '                        sr1.Close()

    '                        par.cmd.CommandText = "SELECT BOL_BOLLETTE.*,RAPPORTI_UTENZA.COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.BOL_BOLLETTE WHERE RAPPORTI_UTENZA.ID=BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID=" & lstBollette.Items(i).Value
    '                        Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                        If myReaderJ.Read Then
    '                            contenuto = Replace(contenuto, "$id$", Format(par.IfNull(myReaderJ("id"), "0"), "0000000000"))
    '                            contenuto = Replace(contenuto, "$mese$", Format(Now, "MMMM") & " " & Year(Now))
    '                            contenuto = Replace(contenuto, "$nominativo$", par.IfNull(myReaderJ("INTESTATARIO"), ""))
    '                            contenuto = Replace(contenuto, "$indirizzo$", par.IfNull(myReaderJ("INDIRIZZO"), ""))
    '                            contenuto = Replace(contenuto, "$capcitta$", par.IfNull(myReaderJ("CAP_CITTA"), ""))
    '                            contenuto = Replace(contenuto, "$oggetto$", "Bollettazione $periodo$")
    '                            contenuto = Replace(contenuto, "$codcontratto$", par.IfNull(myReaderJ("COD_CONTRATTO"), ""))
    '                            contenuto = Replace(contenuto, "$testolettera$", "")
    '                            contenuto = Replace(contenuto, "$note$", "")
    '                            contenuto = Replace(contenuto, "$periodo$", "Dal " & par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_DA"), "")) & " al " & par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_A"), "")))
    '                            contenuto = Replace(contenuto, "$scadenza$", par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), "")))
    '                            If par.IfNull(myReaderJ("N_RATA"), "") <> "99" And par.IfNull(myReaderJ("N_RATA"), "") <> "9999" Then
    '                                contenuto = Replace(contenuto, "$causale$", "RATA N. " & par.IfNull(myReaderJ("N_RATA"), ""))
    '                            Else
    '                                contenuto = Replace(contenuto, "$causale$", "")
    '                            End If
    '                        End If
    '                        myReaderJ.Close()

    '                        Dim IMPORTO As Double = 0
    '                        Dim DETTAGLIO As String = ""
    '                        Dim TOTALE As Double = 0

    '                        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & lstBollette.Items(i).Value & " ORDER BY BOL_BOLLETTE_VOCI.ID ASC"
    '                        myReaderJ = par.cmd.ExecuteReader()
    '                        DETTAGLIO = "<table width='80%'>"


    '                        Do While myReaderJ.Read
    '                            TOTALE = TOTALE + myReaderJ("IMPORTO")
    '                            IMPORTO = IMPORTO + myReaderJ("IMPORTO")
    '                            DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'>" & myReaderJ("DESCRIZIONE") & "</span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(myReaderJ("IMPORTO"), "##,##0.00") & "</span></td></tr>"
    '                            'DETTAGLIO = DETTAGLIO & par.MiaFormat(myReaderJ("DESCRIZIONE"), 40) & " " & Format(myReaderJ("IMPORTO"), "##,##0.00").ToString.PadLeft(15) & "<br />"
    '                        Loop
    '                        myReaderJ.Close()

    '                        DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'> </span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'> </span></td></tr>"
    '                        DETTAGLIO = DETTAGLIO & "<tr><td><span style='font-size: 10pt; font-family: Courier New'>TOTALE</span></td><td style='text-align: right'><span style='font-size: 10pt; font-family: Courier New'>" & Format(TOTALE, "##,##0.00") & "</span></td></tr>"
    '                        DETTAGLIO = DETTAGLIO & "</table><br /><span style='font-size: 8pt; font-family: Arial'>Tutti gli importi sono espressi in Euro</span>"

    '                        contenuto = Replace(contenuto, "$importo$", Format(IMPORTO, "0.00") & " Euro")
    '                        contenuto = Replace(contenuto, "$dettaglio$", DETTAGLIO)


    '                        'scrivo il nuovo contratto compilato
    '                        Dim sr As StreamWriter = New StreamWriter(Server.MapPath("StampeLettere\") & NomeFile & ".htm", True, System.Text.Encoding.Default)
    '                        sr.WriteLine(contenuto)
    '                        sr.Close()

    '                        'Dim url As String = NomeFile
    '                        'Dim pdfConverter As PdfConverter = New PdfConverter
    '                        ' ''pdfConverter.LicenseKey = "P38cBx6AWW7b9c81TjEGxnrazP+J7rOjs+9omJ3TUycauK+cL WdrITM5T59hdW5r"
    '                        'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
    '                        'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
    '                        'pdfConverter.PdfDocumentOptions.ShowHeader = False
    '                        'pdfConverter.PdfDocumentOptions.ShowFooter = False
    '                        'pdfConverter.PdfDocumentOptions.LeftMargin = 5
    '                        'pdfConverter.PdfDocumentOptions.RightMargin = 5
    '                        'pdfConverter.PdfDocumentOptions.TopMargin = 5
    '                        'pdfConverter.PdfDocumentOptions.BottomMargin = 5
    '                        'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

    '                        'pdfConverter.PdfDocumentOptions.ShowHeader = False
    '                        'pdfConverter.PdfFooterOptions.FooterText = ("")
    '                        'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
    '                        'pdfConverter.PdfFooterOptions.DrawFooterLine = False
    '                        'pdfConverter.PdfFooterOptions.PageNumberText = ""
    '                        'pdfConverter.PdfFooterOptions.ShowPageNumber = False


    '                        'pdfConverter.SavePdfFromUrlToFile(Server.MapPath("StampeLettere\") & NomeFile & ".htm", Server.MapPath("StampeLettere\") & NomeFile & ".pdf")
    '                        Response.Write("<script>var fin;fin=window.open('StampeLettere/" & NomeFile & ".htm" & "','" & Format(Now, "yyyyMMddHHmmss") & "','top=0,left=0,resizable=yes,scrollbars=yes,menubar=yes,toolbar=yes');fin.focus();</script>")

    '                        Exit Sub
    '                    Catch ex As Exception
    '                        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '                        Session.Item("LAVORAZIONE") = "0"
    '                        par.myTrans.Rollback()
    '                        par.OracleConn.Close()
    '                        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '                        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '                    End Try
    '                End If
    '            Next
    '        Else
    '            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
    '        End If

    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '        Session.Item("LAVORAZIONE") = "0"

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Private Sub InserisciBoll()
        Dim I As Integer = 0

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim BOLLO As Double = 0
            Dim APPLICABOLLO As Double = 0
            Dim TOTALE_BOLLETTA As Double = 0
            Dim SPESEmav As Double = 0


            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderX.Read Then
                BOLLO = CDbl(par.PuntiInVirgole(myReaderX("VALORE")))
            End If
            myReaderX.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=25"
            myReaderX = par.cmd.ExecuteReader()
            If myReaderX.Read Then
                APPLICABOLLO = CDbl(par.PuntiInVirgole(myReaderX("VALORE")))
            End If
            myReaderX.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderX = par.cmd.ExecuteReader()
            If myReaderX.Read Then
                SPESEmav = CDbl(par.PuntiInVirgole(myReaderX("VALORE")))
            End If
            myReaderX.Close()

            Dim acrBoll As String = ""
            If cmbTipoCont.SelectedValue = -1 Then
                txtAppare.Value = "1"
                Response.Write("<script>alert('Specificare la tipologia di bolletta da inserire');</script>")
                Exit Sub
            Else
                par.cmd.CommandText = "select ACRONIMO from SISCOM_MI.TIPO_BOLLETTE WHERE ID=" & cmbTipoCont.SelectedValue
                acrBoll = par.IfNull(par.cmd.ExecuteScalar, "")
            End If

            If txtPeriodoDa.Text <> "" And txtPeriodoAl.Text <> "" And txtScadenza.Text <> "" And IsDate(txtPeriodoDa.Text) = True And IsDate(txtPeriodoAl.Text) = True Then
                If Mid(par.AggiustaData(txtPeriodoDa.Text), 1, 4) = Mid(par.AggiustaData(txtPeriodoAl.Text), 1, 4) Or Mid(acrBoll, 1, 3) = "STR" Then

                    'max 19/10/2017 verifico se sono gia presenti bollette di DPC non stornate sulla stessa anagrafica
                    If cmbTipoCont.SelectedValue = "9" Then
                        par.cmd.CommandText = "select id FROM SISCOM_MI.bol_bollette where id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " and id_tipo=9 and id_bolletta_storno is null and cod_affittuario=" & CType(Me.Page.FindControl("txtCodAffittuario"), HiddenField).Value
                        Dim myReaderDPC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderDPC.HasRows = True Then
                            txtAppare.Value = "1"
                            myReaderDPC.Close()
                            Response.Write("<script>alert('Attenzione...esiste già una bolletta di DEPOSITO CAUZIONALE emessa e NON STORNATA per questo contratto! Operazione annullata!');</script>")
                            Exit Sub
                        End If
                        myReaderDPC.Close()
                    End If
                    '-----

                    Dim tot_bol As Double = 0
                    Dim kk As Integer = 0

                    For kk = 0 To lstVociBolletta.Items.Count - 1
                        tot_bol = tot_bol + par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(kk).Text, 32, 10))
                    Next

                    If SPESEmav > 0 Then
                        tot_bol = tot_bol + +SPESEmav
                    End If
                    If TOTALE_BOLLETTA > APPLICABOLLO Then
                        tot_bol = tot_bol + BOLLO
                    End If

                    'If tot_bol > 0 Then
                    Dim IMP_DPC As Decimal = 0

                    If tot_bol > 0 Then
                        Dim ID_BOLLETTA As Long = 0
                        par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
                        Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderS.Read Then
                            par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                                                & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                                                & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                                                & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                                                & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                                                & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                                                & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                                                & "Values " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 99, '" & par.AggiustaData(txtEmissione.Text) _
                                                & "', '" & par.AggiustaData(txtScadenza.Text) & "', NULL, " _
                                                & "NULL, '','" & par.PulisciStrSql(txtNote.Text) & "'," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value _
                                                & " ," & par.RicavaEsercizioCorrente & ", " _
                                                & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value _
                                                & ", '0', '', " & CType(Me.Page.FindControl("txtCodAffittuario"), HiddenField).Value _
                                                & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                                                & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.IfNull(myReaderS("civico_COR"), "")) _
                                                & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") _
                                                & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") & "', NULL, '" & par.AggiustaData(txtPeriodoDa.Text) & "', '" & par.AggiustaData(txtPeriodoAl.Text) & "', " _
                                                & "'0', " & myReaderS("ID_COMPLESSO") & ", '', NULL, '', " _
                                                & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ", NULL, NULL,'MOD', " & cmbTipoCont.SelectedValue & ")"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                ID_BOLLETTA = myReaderA(0)
                                TOTALE_BOLLETTA = 0

                                For I = 0 To lstVociBolletta.Items.Count - 1
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & lstVociBolletta.Items(I).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10)) & ",'" & par.PulisciStrSql(par.RicavaTesto(lstVociBolletta.Items(I).Text, 43, 30)) & "')"
                                    par.cmd.ExecuteNonQuery()
                                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10))
                                    If lstVociBolletta.Items(I).Value = 7 Then
                                        IMP_DPC = Replace(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10), ".", "")
                                    End If
                                Next

                                If SPESEmav > 0 Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",407" _
                                    & "," & par.VirgoleInPunti(Format(SPESEmav, "0.00")) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + SPESEmav
                                End If



                                If TOTALE_BOLLETTA > APPLICABOLLO Then

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & ",95" _
                                                        & "," & par.VirgoleInPunti(Format(BOLLO, "0.00")) & ")"
                                    par.cmd.ExecuteNonQuery()

                                End If



                            End If
                            myReaderA.Close()

                            par.cmd.CommandText = "update siscom_mi.bol_bollette_voci_emissioni set id_bol_bollette_voci=id_bol_bollette_voci where id_bolletta=" & ID_BOLLETTA
                            par.cmd.ExecuteNonQuery()

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F08','Importo: " & TOTALE_BOLLETTA & " euro; Tipologia: " & cmbTipoCont.SelectedItem.Text & "; Riferimento: " & txtPeriodoDa.Text & " - " & txtPeriodoAl.Text & "; Emissione: " & txtEmissione.Text & " Scadenza:" & txtScadenza.Text & "')"
                            par.cmd.ExecuteNonQuery()

                            'MAX 28/08/2017 aggiorno valore deposito cauzionale se viene inserito una bolletta di questo tipo
                            If IMP_DPC > 0 And cmbTipoCont.SelectedValue = 9 Then

                                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(IMP_DPC) & " WHERE ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                                    & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                                    & "'F02','L''IMPORTO DEL DEPOSITO CAUZIONALE VIENE IMPOSTATO A EURO " & IMP_DPC & " A SEGUITO DI INSERIMENTO MANUALE BOLLETTA DEPOSITO CAUZIONALE')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE  (ID, ID_CONTRATTO, ID_ANAGRAFICA,    DATA, IMPORTO, ID_BOLLETTA,   NOTE, DATA_COSTITUZIONE, FL_ORIGINALE,   DATA_RESTITUZIONE, IMPORTO_RESTITUZIONE,RESTITUIBILE) " _
                                                    & " VALUES (SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & CType(Me.Page.FindControl("txtCodAffittuario"), HiddenField).Value & ",'" & par.AggiustaData(txtDataEmiss.Text) & "'," & par.VirgoleInPunti(IMP_DPC) & " ," & ID_BOLLETTA & ",'','" & par.AggiustaData(txtDataEmiss.Text) & "',1,NULL,NULL,1)"
                                par.cmd.ExecuteNonQuery()

                                Dim ID_DPC As Long = 0
                                par.cmd.CommandText = "select SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.CURRVAL FROM DUAL"
                                Dim myReaderDPC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReaderDPC.Read Then
                                    ID_DPC = myReaderDPC(0)
                                End If
                                myReaderDPC.Close()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & ID_DPC & ",NULL,NULL,1)"
                                par.cmd.ExecuteNonQuery()



                            End If
                            '----fine


                        End If
                        myReaderS.Close()
                        lstVociBolletta.Items.Clear()
                        CType(Me.Page, Object).SalvaDati()
                    Else
                        txtAppare.Value = "1"
                        Response.Write("<script>alert('Attenzione... Non è possibile inserire bollette negative! Si prega di inserire il documento nella partita gestionale.');</script>")
                    End If
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
                    'Else
                    'txtAppare.Value = "1"
                    'Response.Write("<script>alert('Attenzione... La bolletta non può avere un importo totale inferiore o uguale a 0, considerando anche l\'eventuale importo del bollo e delle spese mav!');</script>")
                    'End If
                Else
                    txtAppare.Value = "1"
                    Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno solare.\nSe il periodo è composto da più anni, inserire più bollette.');</script>")
                End If
            Else
                txtAppare.Value = "1"
                Response.Write("<script>alert('Inserire delle date valide in tutti i campi!!');</script>")
            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub InserisciBollGest()
        Dim I As Integer = 0

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

        If lstVociBolletta.Items.Count = 0 Then
            txtAppare.Value = "1"
            Response.Write("<script>alert('Inserire almeno una voce per la scrittura gestionale!');</script>")
            Exit Sub
        End If

        If cmbTipoGest.SelectedValue = -1 Then
            txtAppare.Value = "1"
            Response.Write("<script>alert('Specificare la tipologia di gestionale da inserire');</script>")
            Exit Sub
        End If

        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim TOTALE_BOLLETTA As Double = 0

            Dim idAnagr As Long = 0
            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE " _
                & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
            Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreDati.Read Then
                idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
            End If
            lettoreDati.Close()


            If txtPeriodoDa.Text <> "" And txtPeriodoAl.Text <> "" And IsDate(txtPeriodoDa.Text) = True And IsDate(txtPeriodoAl.Text) = True Then
                'If Mid(par.AggiustaData(txtPeriodoDa.Text), 1, 6) = Mid(par.AggiustaData(txtPeriodoAl.Text), 1, 6) Then

                'INSERIMENTO BOLLETTA GEST.
                For I = 0 To lstVociBolletta.Items.Count - 1
                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10))
                Next

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                        & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                        & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & par.RicavaEsercizioCorrente & "," _
                        & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value & "," & idAnagr & ",'" & par.AggiustaData(txtPeriodoDa.Text) & "','" & par.AggiustaData(txtPeriodoAl.Text) & "'," & par.VirgoleInPunti(TOTALE_BOLLETTA) & "," _
                        & "'" & par.AggiustaData(txtEmissione.Text) & "','',''," & cmbTipoGest.SelectedValue & ",'N',NULL,'" & par.PulisciStrSql(txtNote.Text) & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F08','Nuova gestionale: " & TOTALE_BOLLETTA & " euro; Tipologia: " & cmbTipoGest.SelectedItem.Text & "; Riferimento: " & txtPeriodoDa.Text & " - " & txtPeriodoAl.Text & "; Emissione: " & txtEmissione.Text & " ')"
                par.cmd.ExecuteNonQuery()

                Dim ID_BOLLETTA_GEST As Long
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                Dim myReaderBG As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderBG.Read Then
                    ID_BOLLETTA_GEST = myReaderBG(0)
                End If
                myReaderBG.Close()

                For I = 0 To lstVociBolletta.Items.Count - 1
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID,ID_BOLLETTA_GEST,ID_VOCE,IMPORTO) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & ID_BOLLETTA_GEST & "," & lstVociBolletta.Items(I).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10)) & ")"
                    par.cmd.ExecuteNonQuery()
                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10))
                Next

                CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                txtTotBoll.Text = ""

                lstVociBolletta.Items.Clear()

                txtTotBoll.Text = ""
                menuGest.Value = "0"
                cmbTipoGest.SelectedValue = -1
                CType(Me.Page, Object).SalvaDati()
                'Else
                '    txtAppare.Value = "1"
                '    Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
                '    menuGest.Value = "1"
                'End If
            Else
                txtAppare.Value = "1"
                Response.Write("<script>alert('Inserire delle date valide in tutti i campi!!');</script>")
                menuGest.Value = "1"
            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub img_InserisciBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciBolletta.Click
        If menuGest.Value = "0" Then
            txtDataScad.ReadOnly = False
            InserisciBoll()
        Else
            txtDataScad.ReadOnly = True
            InserisciBollGest()
        End If

    End Sub

    Protected Sub btnInserisciVoce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnInserisciVoce.Click
        lstVociBolletta.Items.Add(New ListItem(par.MiaFormat(cmbVoceSchema.SelectedItem.Text, 30) & " " & par.MiaFormat(txtImportoVoce.Text, 10) & " " & par.MiaFormat(txtnotevoce.Text, 30), cmbVoceSchema.SelectedItem.Value))

        Dim tot_bol As Decimal = 0

        For K = 0 To lstVociBolletta.Items.Count - 1
            tot_bol = tot_bol + par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F16','Importo voce: " & par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10) & " euro; Tipologia: " & par.RicavaTesto(par.PulisciStrSql(lstVociBolletta.Items(K).Text), 1, 31) & "')"
            par.cmd.ExecuteNonQuery()

        Next

        txtTotBoll.Text = Format(tot_bol, "0.00")

        par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans


    End Sub

    Protected Sub btnInserisciVoce2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnInserisciVoce2.Click
        lstVociBoll.Items.Add(New ListItem(par.MiaFormat(cmbVoceSchema2.SelectedItem.Text, 30) & " " & par.MiaFormat(txtImportoVoce2.Text, 10) & " " & par.MiaFormat(txtNoteVoci2.Text, 30), cmbVoceSchema2.SelectedItem.Value))

        Dim tot_bol As Decimal = 0

        For K = 0 To lstVociBoll.Items.Count - 1
            tot_bol = tot_bol + par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10)
        Next

        txtTotSt.Text = Format(tot_bol, "0.00")


        'par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        'par.SettaCommand(par)
        'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
        '''par.cmd.Transaction = par.myTrans

        'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        '& "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        '& "'F16','')"
        'par.cmd.ExecuteNonQuery()
    End Sub


    Protected Sub img_ChiudiBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiBolletta.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        imgSalva.Visible = False
        img_InserisciBolletta.Visible = True
        txtTotBoll.Text = ""
        lstVociBolletta.Items.Clear()
        txtTotBoll.Text = ""
        menuGest.Value = "0"
        cmbTipoCont.SelectedValue = -1
        cmbTipoGest.SelectedValue = -1
    End Sub

    Private Sub ModificaBoll()
        Try
            Dim MiaVar1 As String = ""
            Dim MiaVar2 As String = ""

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            If V3.Value <> "" Then
                Dim i As Integer
                'For i = 0 To lstBollette.Items.Count - 1
                'MiaVar1 = Replace(Replace(Trim(lstBollette.Items(i).Text), Chr(160), ""), " ", "")
                'MiaVar2 = Replace(Replace(Trim(V1.Value), Chr(160), ""), " ", "")

                Try
                    lstVociBolletta.Items.Clear()
                    img_InserisciBolletta.Visible = False
                    imgSalva.Visible = True
                    'txtAppare.Value = "1"
                    par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                    'IdBolletta.Value = lstBollette.Items(i).Value


                    Dim tot_bol As Decimal = 0

                    par.cmd.CommandText = "SELECT BOL_BOLLETTE.* from siscom_mi.bol_bollette WHERE BOL_BOLLETTE.ID=" & V3.Value
                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        If (par.IfNull(myReaderJ("rif_file"), "") = "FIN" Or (par.IfNull(myReaderJ("n_rata"), "99") = "99" And par.IfNull(myReaderJ("data_pagamento"), "") = "" And par.IfNull(myReaderJ("rif_file"), "") = "MOD" And par.IfNull(myReaderJ("rif_file_txt"), "") = "")) Then
                            If par.IfNull(myReaderJ("FL_ANNULLATA"), "") = "0" And par.IfNull(myReaderJ("rif_bollettino"), "") = "" And par.IfNull(myReaderJ("ID_BOLLETTA_RIC"), "") = "" Then

                                
                                txtPeriodoDa.Text = par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_DA"), ""))
                                txtPeriodoAl.Text = par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_A"), ""))
                                txtEmissione.Text = par.FormattaData(par.IfNull(myReaderJ("DATA_EMISSIONE"), ""))
                                txtScadenza.Text = par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), ""))
                                txtNote.Text = par.IfNull(myReaderJ("NOTE"), "")
                                cmbTipoCont.SelectedValue = par.IfNull(myReaderJ("ID_TIPO"), -1)

                                myReaderJ.Close()

                                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & V3.Value & " ORDER BY BOL_BOLLETTE_VOCI.ID ASC"
                                myReaderJ = par.cmd.ExecuteReader()
                                Do While myReaderJ.Read
                                    lstVociBolletta.Items.Add(New ListItem(par.MiaFormat(myReaderJ("DESCRIZIONE"), 30) & " " & par.MiaFormat(Format(myReaderJ("IMPORTO"), "0.00"), 10) & " " & par.MiaFormat(par.IfNull(myReaderJ("NOTE"), ""), 30), myReaderJ("ID_VOCE")))
                                Loop
                                myReaderJ.Close()
                                txtAppare.Value = "1"

                                For K As Integer = 0 To lstVociBolletta.Items.Count - 1
                                    tot_bol = tot_bol + par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)
                                Next

                                txtTotBoll.Text = Format(tot_bol, "0.00")
                            Else
                                myReaderJ.Close()
                                Response.Write("<script>alert('Non è possibile procedere...Bolletta annullata, mav già emesso o bolletta riclassificata!');</script>")
                                imgSalva.Visible = False
                                img_InserisciBolletta.Visible = True
                                V3.Value = ""
                                txtAppare.Value = "0"

                            End If
                        Else
                            myReaderJ.Close()
                            Response.Write("<script>alert('Bolletta Non Modificabile!');</script>")
                            imgSalva.Visible = False
                            img_InserisciBolletta.Visible = True
                            txtAppare.Value = "0"
                            V3.Value = ""
                        End If
                    End If
                    Exit Sub

                Catch ex As Exception
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                    Session.Item("LAVORAZIONE") = "0"
                    par.myTrans.Rollback()
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End Try
                'Next
            Else
                Response.Write("<SCRIPT>alert('Selezionare una bolletta della lista!');</SCRIPT>")
                txtAppare.Value = "0"
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub ModificaBollGest()
        Try
            Dim MiaVar1 As String = ""
            Dim MiaVar2 As String = ""
            Dim tot_bol As Decimal = 0

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            If V3.Value <> "" Then
                Dim i As Integer

                Try
                    lstVociBolletta.Items.Clear()
                    img_InserisciBolletta.Visible = False
                    imgSalva.Visible = True
                    par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans
                    'IdBolletta.Value = lstBollette.Items(i).Value


                    par.cmd.CommandText = "SELECT BOL_BOLLETTE_GEST.*,UTILIZZABILE from siscom_mi.bol_bollette_GEST,siscom_mi.TIPO_BOLLETTE_GEST WHERE bol_bollette_GEST.id_tipo=TIPO_BOLLETTE_GEST.id and BOL_BOLLETTE_GEST.ID=" & V3.Value
                    Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderJ.Read Then
                        If par.IfNull(myReaderJ("UTILIZZABILE"), 1) = 1 Then
                            If par.IfNull(myReaderJ("TIPO_APPLICAZIONE"), "") = "N" Then
                                txtPeriodoDa.Text = par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_DA"), ""))
                                txtPeriodoAl.Text = par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_A"), ""))
                                txtEmissione.Text = par.FormattaData(par.IfNull(myReaderJ("DATA_EMISSIONE"), ""))
                                'txtScadenza.Text = par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), ""))
                                txtNote.Text = par.IfNull(myReaderJ("NOTE"), "")

                                cmbTipoGest.SelectedValue = par.IfNull(myReaderJ("ID_TIPO"), "")

                                myReaderJ.Close()

                                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.*,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI_GEST.ID_VOCE AND BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST=" & V3.Value & " ORDER BY BOL_BOLLETTE_VOCI_GEST.ID ASC"
                                myReaderJ = par.cmd.ExecuteReader()
                                Do While myReaderJ.Read
                                    lstVociBolletta.Items.Add(New ListItem(par.MiaFormat(myReaderJ("DESCRIZIONE"), 30) & " " & par.MiaFormat(Format(myReaderJ("IMPORTO"), "0.00"), 10), myReaderJ("ID_VOCE")))
                                Loop
                                myReaderJ.Close()
                                txtAppare.Value = "1"

                                For K As Integer = 0 To lstVociBolletta.Items.Count - 1
                                    tot_bol = tot_bol + par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)
                                Next

                                txtTotBoll.Text = Format(tot_bol, "0.00")
                            Else
                                myReaderJ.Close()
                                Response.Write("<script>alert('Documento non modificabile!');</script>")
                                imgSalva.Visible = False
                                img_InserisciBolletta.Visible = True
                                txtAppare.Value = "0"
                                V3.Value = ""
                            End If
                        Else
                            myReaderJ.Close()
                            Response.Write("<script>alert('Documento non modificabile!');</script>")
                            imgSalva.Visible = False
                            img_InserisciBolletta.Visible = True
                            txtAppare.Value = "0"
                            V3.Value = ""
                        End If
                    End If
                    Exit Sub

                Catch ex As Exception
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
                    Session.Item("LAVORAZIONE") = "0"
                    par.myTrans.Rollback()
                    par.OracleConn.Close()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Write("<script>top.location.href='../Errore.aspx';</script>")
                End Try
                'Next
            Else
                Response.Write("<SCRIPT>alert('Selezionare una bolletta della lista!');</SCRIPT>")
                txtAppare.Value = "0"
            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnModificaBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModificaBolletta.Click
        If menuGest.Value = "1" Then

            txtDataScad.ReadOnly = True
            ModificaBollGest()
        Else
            txtDataScad.ReadOnly = False
            ModificaBoll()
        End If
    End Sub

    Private Sub CreaStornoEnuovaBoll()
        Dim id_Tipo_Bolletta As Integer = 0

        Try
            If cmbEmetti.SelectedValue = "1" And lblAvvisoBoll.Visible = True Then
                If CDec(txtTotSt.Text) > tot_bolDaSt Then
                    V3.Value = ""
                    txtAppare1.Value = "0"
                    lstVociBoll.Items.Clear()
                    txtCompetenzaAl.Text = ""
                    txtCompetenzaDal.Text = ""
                    'txtDataEmiss.Text = ""
                    txtDataScad.Text = ""
                    txtNoteBoll.Text = ""
                    CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "0"
                    Response.Write("<script>alert('Attenzione... L\'importo della bolletta da emettere non può essere superiore al credito disponibile per la ripartizione!');</script>")
                    Exit Sub
                End If
            End If
            'If Mid(par.AggiustaData(txtCompetenzaDal.Text), 1, 6) <> Mid(par.AggiustaData(txtCompetenzaAl.Text), 1, 6) Then
            '    Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
            '    Exit Sub
            'End If
            If cmbEmetti.SelectedValue = "1" Then
                If par.AggiustaData(txtDataEmiss.Text) > par.AggiustaData(txtDataScad.Text) Then
                    txtAppare1.Value = "1"
                    'txtDataEmiss.Text = ""
                    txtDataScad.Text = ""
                    Response.Write("<script>alert('Attenzione... La data di emissione non può essere successiva a quella di scadenza!');</script>")
                    Exit Sub
                End If
            End If

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

            Dim SumImportoVOCI As Decimal = 0

            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim pagata As Boolean = False
            Dim dataPagamento As String = ""
            Dim dataValuta As String = ""
            Dim idAnagrafica As Long = 0
            Dim importoTot As Decimal = 0
            Dim ManualeAutomaticaBolletta As String = ""
            '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE,siscom_mi.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA where BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND T_VOCI_BOLLETTA.ID(+)=BOL_BOLLETTE_VOCI.ID_voce AND " _
                & "ID_CONTRATTO=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " AND BOL_BOLLETTE.ID=" & V3.Value
            Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt0 As New Data.DataTable
            da0.Fill(dt0)
            da0.Dispose()
            Dim voceNoSaldoSindacati As Boolean = False
            Dim nomeSindacato As String = ""
            Dim voceNoSaldo As Boolean = False
            Dim dtVoceNoSaldo As New Data.DataTable
            Dim RIGAvoceNoSaldo As System.Data.DataRow

            dtVoceNoSaldo.Columns.Add("id_voce")
            dtVoceNoSaldo.Columns.Add("imp_pagato")
            Dim importoTOTnoSaldo As Decimal = 0
            
            If dt0.Rows.Count > 0 Then

                For Each rowBoll As Data.DataRow In dt0.Rows
                    'max 28/08/2017 ricavo tipo bolletta. se 9 (DPC) devo inserire nota in nella corrispondente in STORICO_DEP_CAUZIONALE
                    id_Tipo_Bolletta = par.IfNull(rowBoll("ID_TIPO"), 0)
                    If rowBoll("N_RATA") = "99" Then
                        ManualeAutomaticaBolletta = " MANUALE "
                    Else
                        ManualeAutomaticaBolletta = " AUTOMATICA "
                    End If
                    '-----FINE
                    importoTot = importoTot + par.IfNull(rowBoll("IMPORTO"), 0)

                    If par.IfNull(rowBoll.Item("fl_no_saldo"), 0) = 1 Then
                        If par.IfNull(rowBoll.Item("IMP_PAGATO"), 0) > 0 Then
                            If par.IfNull(rowBoll.Item("gruppo"), 0) = 5 Then
                                nomeSindacato = par.IfNull(rowBoll.Item("descrizione"), "")
                                voceNoSaldoSindacati = True
                            Else
                                voceNoSaldo = True
                            End If
                            RIGAvoceNoSaldo = dtVoceNoSaldo.NewRow()
                            RIGAvoceNoSaldo.Item("id_voce") = par.IfNull(rowBoll.Item("id_voce"), 0)
                            RIGAvoceNoSaldo.Item("imp_pagato") = par.IfNull(rowBoll.Item("imp_pagato"), 0)
                            importoTOTnoSaldo = importoTOTnoSaldo + par.IfNull(rowBoll.Item("imp_pagato"), 0)
                            dtVoceNoSaldo.Rows.Add(RIGAvoceNoSaldo)
                        End If
                    End If
                Next

                If par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(dt0.Rows(0).Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) > 0) Then
                    pagata = True
                    dataPagamento = par.IfNull(dt0.Rows(0).Item("DATA_PAGAMENTO"), "")
                    dataValuta = par.IfNull(dt0.Rows(0).Item("DATA_VALUTA"), "")
                Else
                    pagata = False
                    dataPagamento = Format(Now, "yyyyMMdd")
                    dataValuta = Format(Now, "yyyyMMdd")
                End If
            End If

            Dim noteEventoRimbors As String = ""

            If voceNoSaldoSindacati = True And cmbEmetti.SelectedValue <> 1 Then
                noteEventoRimbors = "<b>(<u>EURO " & Format(importoTOTnoSaldo, "0.00") & " RIMBORSABILE DAL SINDACATO " & nomeSindacato & "</u>)</b>"
                Response.Write("<script>alert('Attenzione... Le quote sindacali non sono rimborsabili. L\'utente dovrà recarsi presso il sindacato " & nomeSindacato & "');</script>")
            End If
            If voceNoSaldo = True And cmbEmetti.SelectedValue <> 1 Then
                noteEventoRimbors = "<b>(<u>EURO " & Format(importoTOTnoSaldo, "0.00") & " RIMBORSABILE DA ALER</u>)</b>"
                Response.Write("<script>alert('Attenzione... Le voci Facility non sono rimborsabili. L\'utente dovrà rivolgersi all\'ALER');</script>")
            End If

            'RICAVO ID ANAGRAFICA
            Dim idAnagr As Long = 0
            par.cmd.CommandText = "SELECT SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE " _
                & " RAPPORTI_UTENZA.ID=SOGGETTI_CONTRATTUALI.ID_CONTRATTO AND RAPPORTI_UTENZA.ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " AND COD_TIPOLOGIA_OCCUPANTE='INTE'"
            Dim lettoreDati As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreDati.Read Then
                idAnagr = par.IfNull(lettoreDati("ID_ANAGRAFICA"), 0)
            End If
            lettoreDati.Close()

            Dim dataAttuale As String = ""
            Dim dataInizioCompet As String = ""
            Dim dataFineCompet As String = ""
            dataAttuale = Format(Now, "dd/MM/yyyy")
            If dataAttuale <> "" Then
                dataInizioCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & "01"
                dataFineCompet = Right(dataAttuale, 4) & dataAttuale.Substring(3, 2) & DateTime.DaysInMonth(Right(dataAttuale, 4), dataAttuale.Substring(3, 2))
            End If

            'STORNA BOLLETTA SELEZIONATA
            Dim note As String = ""
            Dim pagataParz As Boolean = False
            Dim idBollGest As Long = 0
            If pagata = True Then

                If importoTot > par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0) Then
                    importoTot = par.IfNull(dt0.Rows(0).Item("IMPORTO_PAGATO"), 0)
                    pagataParz = True
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_GEST (ID, ID_CONTRATTO, ID_ESERCIZIO_F, ID_UNITA, ID_ANAGRAFICA,RIFERIMENTO_DA, RIFERIMENTO_A," _
                            & "IMPORTO_TOTALE, DATA_EMISSIONE, DATA_PAGAMENTO, DATA_VALUTA, ID_TIPO,TIPO_APPLICAZIONE, ID_OPERATORE_APPLICAZIONE, NOTE) " _
                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.NEXTVAL," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & par.RicavaEsercizioCorrente & "," & dt0.Rows(0).Item("ID_UNITA") & "," & idAnagr & ",'" & dataInizioCompet & "','" & dataFineCompet & "'," & par.VirgoleInPunti(importoTot * -1) & "," _
                            & "'" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_PAGAMENTO") & "','" & dt0.Rows(0).Item("DATA_VALUTA") & "',4,'N',NULL,'ECCEDENZA PER PAGAMENTO BOLLETTA STORNATA " & dt0.Rows(0).Item("NUM_BOLLETTA") & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_BOL_BOLLETTE_GEST.CURRVAL FROM DUAL"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    idBollGest = myReader(0)
                End If
                myReader.Close()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & ",712," & par.VirgoleInPunti(importoTot * -1) & ")"
                par.cmd.ExecuteNonQuery()

                Dim impGest As Decimal = 0
                If cmbEmetti.SelectedValue <> "1" Then
                    If dtVoceNoSaldo.Rows.Count > 0 Then
                        For Each rowVoceNoS As Data.DataRow In dtVoceNoSaldo.Rows
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                               & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & "," & rowVoceNoS.Item("id_voce") & "," & par.VirgoleInPunti(par.IfNull(rowVoceNoS.Item("imp_pagato"), 0)) & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                        par.cmd.CommandText = "select sum(importo) from SISCOM_MI.BOL_BOLLETTE_VOCI_GEST where id_bolletta_gest= " & idBollGest
                        impGest = par.IfNull(par.cmd.ExecuteScalar, 0)

                        par.cmd.CommandText = "update siscom_mi.bol_bollette_gest set importo_totale=" & par.VirgoleInPunti(impGest) & " where id=" & idBollGest
                        par.cmd.ExecuteNonQuery()
                    End If
                End If



            End If

            par.cmd.CommandText = "SELECT * from siscom_mi.BOL_BOLLETTE where ID_CONTRATTO=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "AND ID=" & V3.Value
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt1 As New Data.DataTable
            da1.Fill(dt1)
            da1.Dispose()
            For Each row As Data.DataRow In dt1.Rows

                note = "STORNO PER " & cmbMotivoStorno.SelectedItem.Text & " NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA") & txtNoteBoll.Text & ". "

                par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 999, '" & Format(Now, "yyyyMMdd") _
                        & "', '" & Format(Now, "yyyyMMdd") & "', NULL,NULL,NULL,'" & par.PulisciStrSql(note) & "'," _
                        & "" & par.IfNull(row.Item("ID_CONTRATTO"), 0) _
                        & " ," & par.RicavaEsercizioCorrente & ", " _
                        & par.IfNull(row.Item("ID_UNITA"), 0) _
                        & ", '0', '" & par.PulisciStrSql(par.IfNull(row.Item("PAGABILE_PRESSO"), "")) & "', " & par.IfNull(row.Item("COD_AFFITTUARIO"), 0) & "" _
                        & ", '" & par.PulisciStrSql(par.IfNull(row.Item("INTESTATARIO"), "")) & "', " _
                        & "'" & par.PulisciStrSql(par.IfNull(row.Item("INDIRIZZO"), "")) _
                        & "', '" & par.PulisciStrSql(par.IfNull(row.Item("CAP_CITTA"), "")) _
                        & "', '" & par.PulisciStrSql(par.IfNull(row.Item("PRESSO"), "")) & "', '" & par.IfNull(row.Item("RIFERIMENTO_DA"), "") _
                        & "', '" & par.IfNull(row.Item("RIFERIMENTO_A"), "") & "', " _
                        & "'1', " & par.IfNull(row.Item("ID_COMPLESSO"), 0) & ",'', '', " _
                        & Year(Now) & ", '', " & par.IfNull(row.Item("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD',22)"
                par.cmd.ExecuteNonQuery()
            Next

            Dim ID_BOLLETTA_STORNO As Long = 0
            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
            Dim myReaderST As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderST.Read Then
                ID_BOLLETTA_STORNO = myReaderST(0)
            End If
            myReaderST.Close()

            Dim ID_VOCE_STORNO As Long = 0

            par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE.ID= " & V3.Value
            Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtBVoci As New Data.DataTable
            daBVoci.Fill(dtBVoci)
            daBVoci.Dispose()
            For Each row As Data.DataRow In dtBVoci.Rows

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI ( ID, ID_BOLLETTA, ID_VOCE, IMPORTO, NOTE, IMP_PAGATO_OLD," _
                    & "IMP_PAGATO_BAK, IMPORTO_RICLASSIFICATO, IMPORTO_RICLASSIFICATO_PAGATO, COMPETENZA_INIZIO," _
                    & "COMPETENZA_FINE, FL_ACCERTATO ) VALUES ( SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL, " & ID_BOLLETTA_STORNO & ", " & row.Item("ID_VOCE") & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & ",'STORNO'," _
                    & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_OLD"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMP_PAGATO_BAK"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO"), 0)) & ", " & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO_RICLASSIFICATO_PAGATO"), 0)) & "," _
                    & "'" & par.IfNull(row.Item("COMPETENZA_INIZIO"), "") & "', '" & par.IfNull(row.Item("COMPETENZA_FINE"), "") & "'," & par.IfNull(row.Item("FL_ACCERTATO"), 0) & ")"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.CURRVAL FROM DUAL"
                Dim myReaderIDV As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderIDV.Read Then
                    ID_VOCE_STORNO = myReaderIDV(0)
                End If
                myReaderIDV.Close()

                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0) * -1) & " WHERE ID=" & ID_VOCE_STORNO
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set IMP_PAGATO=" & par.VirgoleInPunti(par.IfNull(row.Item("IMPORTO"), 0)) & " WHERE ID=" & row.Item("ID")
                par.cmd.ExecuteNonQuery()

                SumImportoVOCI = SumImportoVOCI + par.IfNull(row.Item("IMP_PAGATO"), 0)
            Next

            'par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNATA=" & V3.Value & ",IMPORTO_PAGATO=" & par.VirgoleInPunti(SumImportoVOCI * -1) & " WHERE ID=" & ID_BOLLETTA_STORNO
            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set ID_BOLLETTA_STORNO=" & ID_BOLLETTA_STORNO & ",FL_STAMPATO='1',DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & V3.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette set DATA_PAGAMENTO='" & dataPagamento & "',DATA_VALUTA='" & dataValuta & "' WHERE ID=" & ID_BOLLETTA_STORNO
            par.cmd.ExecuteNonQuery()

            Dim strPagata As String = ""
            If pagata = True Then
                strPagata = "(precedentam. pagata) "
            Else
                strPagata = "(non precedentem. pagata) "
            End If
            If pagataParz = True Then
                strPagata = "(parzialm. pagata) "
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE COD_TIPOLOGIA_OCCUPANTE='INTE' AND ID_CONTRATTO=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idAnagrafica = par.IfNull(myReader0("ID_ANAGRAFICA"), 0)
            End If
            myReader0.Close()

            'max 28/08/2017 ricavo tipo bolletta. se 9 (DPC) devo inserire nota in nella corrispondente in STORICO_DEP_CAUZIONALE
            If id_Tipo_Bolletta = 9 Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.STORICO_DEP_CAUZIONALE SET NOTE='BOLLETTA " & ManualeAutomaticaBolletta & " STORNATA',FL_ORIGINALE=0,RESTITUIBILE=0 WHERE ID_BOLLETTA=" & V3.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_DEPOSITO_CAUZ=0 WHERE ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F02','L''IMPORTO DEL DEPOSITO CAUZIONALE VIENE IMPOSTATO A EURO 0,00 A SEGUITO DI STORNO BOLLETTA DEPOSITO CAUZIONALE')"
                par.cmd.ExecuteNonQuery()

            End If
            '-----FINE

            If cmbEmetti.SelectedValue = "1" Then

                'SCRITTURA NUOVA BOLLETTA
                'If Mid(par.AggiustaData(txtCompetenzaDal.Text), 1, 6) = Mid(par.AggiustaData(txtCompetenzaAl.Text), 1, 6) Then

                par.cmd.CommandText = "select RAPPORTI_UTENZA.*,EDIFICI.ID_COMPLESSO,UNITA_CONTRATTUALE.ID_EDIFICIO FROM SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.RAPPORTI_UTENZA WHERE UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND EDIFICI.ID=UNITA_CONTRATTUALE.ID_EDIFICIO"
                Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS.Read Then
                    par.cmd.CommandText = "Insert into SISCOM_MI.BOL_BOLLETTE " _
                        & "(ID, N_RATA, DATA_EMISSIONE, DATA_SCADENZA, DATA_I_SOLLECITO, " _
                        & "DATA_II_SOLLECITO, DATA_PAGAMENTO, NOTE, ID_CONTRATTO, ID_ESERCIZIO_F, " _
                        & "ID_UNITA, FL_ANNULLATA, PAGABILE_PRESSO, COD_AFFITTUARIO, INTESTATARIO, " _
                        & "INDIRIZZO, CAP_CITTA, PRESSO, RIFERIMENTO_DA, RIFERIMENTO_A, " _
                        & "FL_STAMPATO, ID_COMPLESSO, DATA_INS_PAGAMENTO, IMPORTO_PAGATO, NOTE_PAGAMENTO, " _
                        & "ANNO, OPERATORE_PAG, ID_EDIFICIO, DATA_ANNULLO_PAG, OPERATORE_ANNULLO_PAG,RIF_FILE,ID_TIPO) " _
                        & "Values " _
                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE.NEXTVAL, 99, '" & par.AggiustaData(txtDataEmiss.Text) _
                        & "', '" & par.AggiustaData(txtDataScad.Text) & "', NULL,NULL,'','" & par.PulisciStrSql(txtNoteBoll.Text) & "'," _
                        & "" & dt0.Rows(0).Item("ID_CONTRATTO") _
                        & " ," & par.RicavaEsercizioCorrente & ", " _
                        & dt0.Rows(0).Item("ID_UNITA") _
                        & ", '0', '', " & idAnagrafica _
                        & ", '" & par.PulisciStrSql(par.IfNull(myReaderS("PRESSO_COR"), "")) & "', " _
                        & "'" & par.PulisciStrSql(par.IfNull(myReaderS("TIPO_COR"), "") & " " & par.IfNull(myReaderS("VIA_COR"), "") & ", " & par.PulisciStrSql(par.IfNull(myReaderS("CIVICO_COR"), ""))) _
                        & "', '" & par.PulisciStrSql(par.IfNull(myReaderS("CAP_COR"), "") & " " & par.IfNull(myReaderS("LUOGO_COR"), "") & "(" & par.IfNull(myReaderS("SIGLA_COR"), "") & ")") _
                        & "', '', '" & par.AggiustaData(txtCompetenzaDal.Text) _
                        & "', '" & par.AggiustaData(txtCompetenzaAl.Text) & "', " _
                        & "'0', " & par.IfNull(myReaderS("ID_COMPLESSO"), 0) & ", '', NULL, '', " _
                        & Year(Now) & ", '', " & par.IfNull(myReaderS("ID_EDIFICIO"), 0) & ", NULL, NULL,'MOD'," & dt0.Rows(0).Item("ID_TIPO") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderS.Close()

                Dim ID_BOLLETTA_NEW As Long = 0
                par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderA.Read Then
                    ID_BOLLETTA_NEW = myReaderA(0)
                End If
                myReaderA.Close()

                Dim importoBollNuova As Decimal = 0
                Dim flNoSaldo As Integer = 0


                Dim IMP_DEP_CAUZ As Decimal = 0

                For K = 0 To lstVociBoll.Items.Count - 1
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA_NEW & "," & lstVociBoll.Items(K).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10)) & ",'" & par.PulisciStrSql(par.RicavaTesto(lstVociBoll.Items(K).Text, 43, 30)) & "')"
                    par.cmd.ExecuteNonQuery()
                    importoBollNuova = importoBollNuova + CDec(par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10))
                    'MAX 28/08/2017 RICAVO IMPORTO DPC
                    If lstVociBoll.Items(K).Value = 7 Then
                        IMP_DEP_CAUZ = Replace(par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10), ".", "")
                    End If
                Next

                'max 28/08/2017 ricavo tipo bolletta. se 9 (DPC) devo inserire in STORICO_DEP_CAUZIONALE
                If id_Tipo_Bolletta = 9 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE " _
                        & "(ID, ID_CONTRATTO, ID_ANAGRAFICA, " _
                        & "DATA, IMPORTO, ID_BOLLETTA,  " _
                        & "NOTE, DATA_COSTITUZIONE, FL_ORIGINALE,  " _
                        & " DATA_RESTITUZIONE, IMPORTO_RESTITUZIONE, RESTITUIBILE)" _
                        & " VALUES (SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & idAnagrafica & ",'" & par.AggiustaData(txtDataEmiss.Text) & "'," & par.VirgoleInPunti(IMP_DEP_CAUZ) & " ," & ID_BOLLETTA_NEW & ",'','" & par.AggiustaData(txtDataEmiss.Text) & "',1,NULL,NULL,0)"
                    par.cmd.ExecuteNonQuery()

                    Dim ID_DPC As Long = 0
                    par.cmd.CommandText = "select SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.CURRVAL FROM DUAL"
                    Dim myReaderDPC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderDPC.Read Then
                        ID_DPC = myReaderDPC(0)
                    End If
                    myReaderDPC.Close()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & ID_DPC & ",NULL,NULL,1)"
                    par.cmd.ExecuteNonQuery()


                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(IMP_DEP_CAUZ) & " WHERE ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F02','L''IMPORTO DEL DEPOSITO CAUZIONALE VIENE IMPOSTATO A EURO " & IMP_DEP_CAUZ & " A SEGUITO DI RIEMISSIONE BOLLETTA DEPOSITO CAUZIONALE')"
                    par.cmd.ExecuteNonQuery()

                End If
                '-----FINE

                Dim impNuovaVoceNoS As Decimal = 0
                For K = 0 To lstVociBoll.Items.Count - 1
                    par.cmd.CommandText = "select fl_no_saldo FROM siscom_mi.t_voci_bolletta where id=" & lstVociBoll.Items(K).Value
                    Dim myReaderA1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA1.Read Then
                        flNoSaldo = myReaderA1(0)
                        If flNoSaldo = 1 Then
                            impNuovaVoceNoS = impNuovaVoceNoS + CDec(par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10))
                        End If
                    End If
                    myReaderA1.Close()
                Next

                Dim impGestT As Decimal = 0
                If dtVoceNoSaldo.Rows.Count > 0 Then
                    For Each rowVoceNoS As Data.DataRow In dtVoceNoSaldo.Rows
                        If flNoSaldo = 0 Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                            & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & "," & rowVoceNoS.Item("id_voce") & "," & par.VirgoleInPunti(par.IfNull(rowVoceNoS.Item("imp_pagato"), 0)) & ")"
                            par.cmd.ExecuteNonQuery()
                        Else
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID, ID_BOLLETTA_GEST, ID_VOCE, IMPORTO) " _
                           & "VALUES (SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & idBollGest & "," & rowVoceNoS.Item("id_voce") & "," & par.VirgoleInPunti(par.IfNull(rowVoceNoS.Item("imp_pagato"), 0)) & ")"
                            par.cmd.ExecuteNonQuery()
                        End If
                    Next
                    par.cmd.CommandText = "select sum(importo) from SISCOM_MI.BOL_BOLLETTE_VOCI_GEST where id_bolletta_gest= " & idBollGest
                    impGestT = par.IfNull(par.cmd.ExecuteScalar, 0)

                    par.cmd.CommandText = "update siscom_mi.bol_bollette_gest set importo_totale=" & par.VirgoleInPunti(impGestT) & " where id=" & idBollGest
                    par.cmd.ExecuteNonQuery()
                End If


                'If voceNoSaldoSindacati And importoTOTnoSaldo > impNuovaVoceNoS Then
                '    Response.Write("<script>alert('Attenzione... Le voci non contabilizzate non verranno rimborsate. L\'utente dovrà recarsi presso l\'ente di competenza.');</script>")
                'End If
                Dim noteEventoRimbors2 As String = ""
                If voceNoSaldoSindacati = True And importoTOTnoSaldo > impNuovaVoceNoS Then
                    noteEventoRimbors2 = "<b>(<u>EURO " & Format(importoTOTnoSaldo, "0.00") & " RIMBORSABILE DAL SINDACATO " & nomeSindacato & "</u>)</b>"

                    Response.Write("<script>alert('Attenzione... Le quote sindacali non sono rimborsabili. L\'utente dovrà recarsi presso il sindacato " & nomeSindacato & "');</script>")
                End If
                If voceNoSaldo = True And importoTOTnoSaldo > impNuovaVoceNoS Then
                    noteEventoRimbors2 = "<b>(<u>EURO " & Format(importoTOTnoSaldo, "0.00") & " RIMBORSABILE DA ALER</u>)</b>"

                    Response.Write("<script>alert('Attenzione... Le voci Facility non sono rimborsabili. L\'utente dovrà rivolgersi all\'ALER');</script>")
                End If






                If importoBollNuova < 0 Then
                    txtAppare.Value = "1"
                    par.myTrans.Rollback()
                    par.myTrans = par.OracleConn.BeginTransaction()
                    HttpContext.Current.Session.Add("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione, par.myTrans)
                    Response.Write("<script>alert('Attenzione... Non è possibile inserire bollette negative! Si prega di inserire il documento nella partita gestionale.');</script>")
                    Exit Sub
                End If

                '***** 27/11/2013 Modifica come da richiesta Gestore *****
                If idBollGest <> 0 Then
                    PagaBollettaStorno(idBollGest, ID_BOLLETTA_NEW, idAnagr, dataInizioCompet, dataFineCompet, dataPagamento, dataValuta, noteEventoRimbors2)
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F203','NUM.BOLLETTA " & dt0.Rows(0).Item("NUM_BOLLETTA") & " " & strPagata & " DI EURO " & dt0.Rows(0).Item("IMPORTO_TOTALE") & " STORNATA PER " & cmbMotivoStorno.SelectedItem.Text & " " & noteEventoRimbors & "')"
                par.cmd.ExecuteNonQuery()

                Dim noteEventiBolPagata As String = ""
                If idBollGest <> 0 Then
                    noteEventiBolPagata = " (pagato con credito da storno)"
                Else
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F08','Importo: " & importoBollNuova & " euro" & noteEventiBolPagata & "; Tipologia: " & par.PulisciStrSql(lblTipoBoll.Text) & "; Riferimento: " & txtCompetenzaDal.Text & " - " & txtCompetenzaAl.Text & "; Emissione: " & txtDataEmiss.Text & " Scadenza:" & txtDataScad.Text & "')"
                    par.cmd.ExecuteNonQuery()
                End If


                CType(Me.Page, Object).SalvaDati()
                    'Else
                    'Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
                    'End If
                Else
                    CType(Me.Page, Object).SalvaDati()
            End If

            V3.Value = ""
            txtAppare1.Value = "0"
            lstVociBoll.Items.Clear()
            txtCompetenzaAl.Text = ""
            txtCompetenzaDal.Text = ""
            'txtDataEmiss.Text = ""
            txtDataScad.Text = ""
            txtNoteBoll.Text = ""
            cmbEmetti.SelectedValue = "1"
            confermaStorno.Value = "0"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub PagaBollettaStorno(ByVal idBollGest As Long, ByVal idBollNew As Long, ByVal idAnagr As Long, ByVal dataInizioCompet As String, ByVal dataFineCompet As String, ByVal dataPagamento As String, ByVal datavaluta As String, Optional noteEvento As String = "")

        Dim importoMorosita As Decimal = 0
        Dim impNuovoPAGATO As Decimal = 0
        Dim diffCreditoMoros As Decimal = 0
        Dim idTipoBoll As Long = 0
        Dim impCredito As Decimal = 0
        Dim impCredIniziale As Decimal = 0

        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI_GEST.*,data_pagamento,data_valuta,fl_no_saldo FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST,siscom_mi.t_voci_bolletta WHERE BOL_BOLLETTE_VOCI_GEST.id_voce=siscom_mi.t_voci_bolletta.id and BOL_BOLLETTE_GEST.ID=SISCOM_MI.BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST AND BOL_BOLLETTE_GEST.ID=" & idBollGest & " ORDER BY fl_no_saldo desc"
        Dim daGest As Oracle.DataAccess.Client.OracleDataAdapter
        Dim dtGest As New Data.DataTable
        daGest = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        daGest.Fill(dtGest)
        daGest.Dispose()
        Dim strVociNoSaldo As String = ""
        Dim importoVoceNoSaldo As Decimal = 0
        If dtGest.Rows.Count > 0 Then
            For Each rowGest As Data.DataRow In dtGest.Rows
                impCredIniziale = Math.Abs(par.IfNull(rowGest.Item("IMPORTO"), 0))
                impCredito = impCredIniziale + importoVoceNoSaldo

                If par.IfNull(rowGest.Item("fl_no_saldo"), 0) = 1 Then
                    importoVoceNoSaldo = impCredito ' * -1
                    impCredito = importoVoceNoSaldo
                    strVociNoSaldo = " and fl_no_saldo=1 "
                Else
                    strVociNoSaldo = " and fl_no_saldo=0 "
                End If

                '16/07/2015 Eliminata condizione che esclude le quote sindacali come richiesto dal comune
                par.cmd.CommandText = "select distinct(bol_bollette.id),bol_bollette.*,bol_bollette_voci.*,BOL_BOLLETTE_VOCI.ID AS ID_VOCE1 from siscom_mi.bol_bollette,siscom_mi.bol_bollette_voci,siscom_mi.t_voci_bolletta where BOL_BOLLETTE_VOCI.id_voce=siscom_mi.t_voci_bolletta.id and bol_bollette.id=bol_bollette_voci.id_bolletta and bol_bollette.id=" & idBollNew & strVociNoSaldo & " ORDER BY IMPORTO ASC"
                Dim daMoros1 As Oracle.DataAccess.Client.OracleDataAdapter
                Dim dtMoros1 As New Data.DataTable
                daMoros1 = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                daMoros1.Fill(dtMoros1)
                daMoros1.Dispose()
                If dtMoros1.Rows.Count > 0 Then
                    '(0)*** AGGIORNO LA VOCE degli INCASSI EXTRAMAV ***
                    par.cmd.CommandText = "INSERT INTO siscom_mi.incassi_extramav (ID, id_tipo_pag, motivo_pagamento, id_contratto,data_pagamento, riferimento_da, riferimento_a, fl_annullata,importo, id_operatore,id_bolletta_gest,data_ora)" _
                        & "VALUES (siscom_mi.seq_incassi_extramav.NEXTVAL,12,'RIPARTIZ. CREDITO DA STORNO'," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & ",'" & Format(Now, "yyyyMMdd") & "','','',0," & par.VirgoleInPunti(impCredito) & "," & Session.Item("ID_OPERATORE") & "," & idBollGest & ",'" & Format(Now, "yyyyMMddHHmmss") & "')"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select siscom_mi.seq_incassi_extramav.currval from dual"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        idIncasso = par.IfNull(lettore(0), "")
                    End If
                    lettore.Close()

                    idEventoPrincipale = WriteEvent(True, "null", "F205", impCredito, Format(Now, "dd/MM/yyyy"), "null", idIncasso, CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value, "COPERTURA VOCI BOLLETTA CON CREDITO DA STORNO")

                    For Each row1 As Data.DataRow In dtMoros1.Rows
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & row1.Item("ID_BOLLETTA") & " AND DATA_PAGAMENTO IS NOT NULL"
                        lettore = par.cmd.ExecuteReader
                        If Not lettore.Read Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET DATA_VALUTA='" & datavaluta _
                                        & "',DATA_PAGAMENTO='" & dataPagamento & "',FL_PAG_PARZ = NVL(FL_PAG_PARZ,0) + 1" _
                                        & " WHERE ID=" & row1.Item("ID_BOLLETTA")
                            par.cmd.ExecuteNonQuery()
                        End If
                        lettore.Close()

                        If dtMoros1.Rows(0).Item("ID_TIPO") <> "4" Then
                            importoMorosita = par.IfNull(row1.Item("IMPORTO"), 0) - par.IfNull(row1.Item("IMP_PAGATO"), 0)
                            If impCredito <> 0 Then
                                diffCreditoMoros = importoMorosita - Math.Abs(impCredito)

                                If diffCreditoMoros >= 0 Then
                                    '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO in BOL_BOLLETTE_VOCI_PAGAM ***
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                    & "(" & row1.Item("ID_VOCE1") & ",'" & rowGest.Item("DATA_PAGAMENTO") & "'," & par.VirgoleInPunti(Math.Round(impCredito, 2)) & ",4," & idIncasso & ")"
                                    par.cmd.ExecuteNonQuery()

                                    WriteEvent(False, row1.Item("ID_VOCE1"), "F205", impCredito, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value, "")

                                    impCredito = impCredito + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                    'ATTRAVERSO IL CREDITO, INIZIO AD INCASSARE LE BOLLETTE (scaduta e sollecitata) PARTENDO DALLE PIù VECCHIE

                                    '()*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(Math.Round(impCredito, 2)) & " where id=" & row1.Item("ID_VOCE1")
                                    par.cmd.ExecuteNonQuery()

                                    impCredito = 0
                                Else
                                    impNuovoPAGATO = importoMorosita + par.IfNull(row1.Item("IMP_PAGATO"), 0)
                                    '(1)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                    par.cmd.CommandText = "UPDATE siscom_mi.bol_bollette_voci set imp_pagato=" & par.VirgoleInPunti(impNuovoPAGATO) & " where id=" & row1.Item("ID_VOCE1")
                                    par.cmd.ExecuteNonQuery()

                                    '(2)*** AGGIORNO LA VOCE CORRISPONDENTE CON L'IMPORTO ***
                                    If importoMorosita <> 0 Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI (ID_VOCE_BOLLETTA,DATA_PAGAMENTO,IMPORTO_PAGATO,ID_TIPO_PAGAMENTO,ID_INCASSO_EXTRAMAV) VALUES " _
                                        & "(" & row1.Item("ID_VOCE1") & ",'" & rowGest.Item("DATA_PAGAMENTO") & "'," & par.VirgoleInPunti(Math.Round(importoMorosita, 2)) & ",4," & idIncasso & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If

                                    WriteEvent(False, row1.Item("ID_VOCE1"), "F205", importoMorosita, Format(Now, "dd/MM/yyyy"), idEventoPrincipale, idIncasso, CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value, "")

                                    impCredito = Math.Abs(impCredito) - importoMorosita
                                End If
                            Else
                                Exit For
                            End If
                        End If
                    Next
                Else
                    If importoVoceNoSaldo <> 0 Then
                        importoVoceNoSaldo = 0
                        End If
                 
                End If
            Next
        End If
        If idBollGest <> 0 Then
            ValorizzColonneImporti(idBollGest)
        End If
        Dim importoRipartito As Decimal = 0

        Dim impPagatoNOSaldo As Decimal = 0
        Dim impGestNOSaldo As Decimal = 0
        If impCredito > 0 Then
            importoRipartito = impCredIniziale - impCredito

            par.cmd.CommandText = "select sum(nvl(imp_pagato,0)) as importo_pagato from siscom_mi.bol_Bollette_voci,siscom_mi.t_voci_bolletta where bol_Bollette_voci.id_voce=t_voci_bolletta.id " _
                & " and id_bolletta=" & idBollNew & " And fl_no_saldo=1"
            impPagatoNOSaldo = par.IfNull(par.cmd.ExecuteScalar, 0)

            par.cmd.CommandText = "select sum(nvl(importo,0)) as importo_gest from siscom_mi.bol_Bollette_voci_gest,siscom_mi.t_voci_bolletta where bol_Bollette_voci_gest.id_voce=t_voci_bolletta.id " _
                & " and id_bolletta_gest=" & idBollGest & " And fl_no_saldo=1"
            impGestNOSaldo = par.IfNull(par.cmd.ExecuteScalar, 0)

            If impGestNOSaldo > 0 Then
                If impGestNOSaldo = impPagatoNOSaldo Then
                    par.cmd.CommandText = "delete from siscom_mi.bol_Bollette_voci_gest where id_voce in (select id from siscom_mi.t_voci_bolletta where fl_no_saldo=1) and id_bolletta_gest=" & idBollGest
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST Set IMPORTO_TOTALE=0 WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()
                Else
                    If impGestNOSaldo > impPagatoNOSaldo Then
                        par.cmd.CommandText = "update siscom_mi.bol_Bollette_voci_gest set importo=" & par.VirgoleInPunti((Math.Abs(impGestNOSaldo - impPagatoNOSaldo))) _
                            & " where id_voce in (select id from siscom_mi.t_voci_bolletta where fl_no_saldo=1) and id_bolletta_gest=" & idBollGest
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST Set IMPORTO_TOTALE=" & par.VirgoleInPunti((Math.Abs(impGestNOSaldo - impPagatoNOSaldo))) _
                           & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()

                    End If
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST Set IMPORTO_TOTALE=IMPORTO_totale+" & par.VirgoleInPunti(par.IfNull(impCredito * (-1), 0)) & "" _
                    & " WHERE ID=" & idBollGest
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_GEST Set IMPORTO=" & par.VirgoleInPunti(par.IfNull(impCredito * (-1), 0)) & "" _
                    & " WHERE id_voce in (select id from siscom_mi.t_voci_bolletta where fl_no_saldo=0) and id_bolletta_gest=" & idBollGest
                par.cmd.ExecuteNonQuery()

            Else
                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST Set IMPORTO_TOTALE=" & par.VirgoleInPunti(par.IfNull(impCredito * (-1), 0)) & "" _
                    & " WHERE ID=" & idBollGest
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_VOCI_GEST Set IMPORTO=" & par.VirgoleInPunti(par.IfNull(impCredito * (-1), 0)) & "" _
                    & " WHERE id_voce in (select id from siscom_mi.t_voci_bolletta where fl_no_saldo=0) and id_bolletta_gest=" & idBollGest
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F204','ECCEDENZA PARI A EURO -" & (impCredito - impGestNOSaldo) & " IN SEGUITO A STORNO DI EURO " & Format(impCredIniziale, "##,##0.00") & " DI CUI EURO " & importoRipartito & " UTILIZZATI PER RICOPRIRE LA NUOVA BOLLETTA EMESSA " & noteEvento & "')"
            par.cmd.ExecuteNonQuery()

        Else
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='T' ,DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()
        End If

        'Next
        'End If
    End Sub
    Private Sub ValorizzColonneImporti(ByVal idBollGest As Decimal)

        Dim incassato As Decimal = 0
        Dim credito As Decimal = 0
        Dim importoEccedenza As Decimal = 0


        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & idBollGest
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            credito = par.IfNull(lettore("IMPORTO_TOTALE"), 0)
        End If
        lettore.Close()

        If idIncasso <> 0 Then
            par.cmd.CommandText = "SELECT id_bolletta,round(SUM(importo_pagato),2) AS incassato " _
                        & "FROM siscom_mi.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                        & "WHERE FL_NO_REPORT=0 AND id_incasso_extramav = " & idIncasso & " " _
                        & "GROUP BY id_bolletta "
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            If dtVoci.Rows.Count > 0 Then
                For Each rowB As Data.DataRow In dtVoci.Rows
                    incassato = incassato + par.IfNull(rowB.Item("incassato"), 0)
                Next
            End If
        End If
        importoEccedenza = Math.Abs(credito) - Math.Abs(incassato)
        par.cmd.CommandText = "update siscom_mi.incassi_extramav set importo_incassato = " & par.insDbValue(incassato, False) & ", " _
                & "importo_eccedenza=" & par.insDbValue((importoEccedenza) * -1, False) & " where id = " & idIncasso
        par.cmd.ExecuteNonQuery()

    End Sub
    Public Function WriteEvent(ByVal TipoPadre As Boolean, ByVal ID_VOCE As String, ByVal CodEvento As String, ByVal Importo As Decimal, ByVal DataPagamento As String, ByVal idEvPadre As String, ByVal vIdIncassoExtramav As Long, ByVal idContratto As Long, Optional ByVal Motivazione As String = "", Optional ByVal idMain As String = "") As String
        Dim idPadre As String = "null"

        If TipoPadre = True Then
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZIALI.NEXTVAL FROM DUAL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                idPadre = lettore(0)
            End If
            lettore.Close()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI (ID,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_CONTRATTO,ID_MAIN,ID_INCASSO_EXTRAMAV,IMPORTO) " _
            & "VALUES ( " & idPadre & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
            & "'" & CodEvento & "','" & Motivazione & "'," & idContratto & "," & par.IfEmpty(idMain, "NULL") & "," & vIdIncassoExtramav & "," & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"

            par.cmd.ExecuteNonQuery()
        Else
            'evento figlio
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT (ID,ID_EVENTO_PRINCIPALE,ID_VOCE_BOLLETTA,IMPORTO) " _
                                & "VALUES ( SISCOM_MI.SEQ_EVENTI_PAGAMENTI_PARZ_DETT.NEXTVAL," & idEvPadre & "," & ID_VOCE & ", " _
                                & " " & par.VirgoleInPunti(par.IfEmpty(Importo, 0)) & ")"
            par.cmd.ExecuteNonQuery()
        End If

        Return idPadre

    End Function

    Private Sub SalvaBolletta()
        Dim K As Integer = 0
        Dim I As Integer = 0
        Dim IMP_DPC As Decimal = 0

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

            'MAX 04/11/2019 Aggiunto controllo su inserimento bolletta senza voci
            If lstVociBolletta.Items.Count = 0 Then
                txtAppare.Value = "1"
                Response.Write("<script>alert('Attenzione... Non è possibile inserire bollette prive di voci!\nAggiungere almeno una voce di bolletta');</script>")
                Exit Sub
            End If
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans



            If Mid(par.AggiustaData(txtPeriodoDa.Text), 1, 4) = Mid(par.AggiustaData(txtPeriodoAl.Text), 1, 4) Then

                Dim tot_bol As Double = 0

                For K = 0 To lstVociBolletta.Items.Count - 1
                    tot_bol = tot_bol + par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10))
                Next

                Dim Tipologia As String = ""
                par.cmd.CommandText = "select id_tipo from siscom_mi.bol_bollette where id=" & V3.Value
                Dim myReaderS11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS11.Read Then
                    If par.IfNull(myReaderS11(0), "02") = "3" Then
                        Tipologia = "FIN"
                    End If
                End If
                myReaderS11.Close()



                If cmbTipoCont.SelectedValue = -1 Then
                    txtAppare.Value = "1"
                    Response.Write("<script>alert('Specificare la tipologia di bolletta da inserire');</script>")
                    Exit Sub
                End If

                '01/02/2019
                Dim dataScadenza As String = ""
                Dim noteBolletta As String = ""
                Dim dataEmissione As String = ""
                Dim riferimentoDa As String = ""
                Dim riferimentoA As String = ""
                Dim tipologiaBoll As Integer = 0
                par.cmd.CommandText = "select * from siscom_mi.bol_bollette where id=" & V3.Value
                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt1 As New Data.DataTable
                da1.Fill(dt1)
                da1.Dispose()
                If dt1.Rows.Count > 0 Then
                    For Each row1 As Data.DataRow In dt1.Rows
                        dataScadenza = par.FormattaData(par.IfNull(row1.Item("data_scadenza"), "29991231"))
                        noteBolletta = par.IfNull(row1.Item("note"), "")
                        dataEmissione = par.FormattaData(par.IfNull(row1.Item("data_emissione"), "29991231"))
                        riferimentoDa = par.FormattaData(par.IfNull(row1.Item("riferimento_da"), "29991231"))
                        riferimentoA = par.FormattaData(par.IfNull(row1.Item("riferimento_a"), "29991231"))
                        tipologiaBoll = par.IfNull(row1.Item("id_tipo"), 1)
                    Next
                End If


                par.cmd.CommandText = "select count(id_bol_bollette_voci) as ACCERT from siscom_mi.bol_Bollette_voci_emissioni where " _
                    & " nvl(FL_ACCERTATO_BOL_BOLLETTE_VOCI,0)=1 and id_bolletta=" & V3.Value
                Dim vociAccertate As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If vociAccertate > 0 Then
                    vociAccertate = 1
                End If


                If dataScadenza = txtScadenza.Text And noteBolletta = txtNote.Text Then
                    If vociAccertate = 0 Then
                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & V3.Value
                        par.cmd.ExecuteNonQuery()

                        For K = 0 To lstVociBolletta.Items.Count - 1
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                                & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & V3.Value & "," & lstVociBolletta.Items(K).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)) & ",'" & par.PulisciStrSql(par.RicavaTesto(lstVociBolletta.Items(K).Text, 43, 30)) & "')"
                            par.cmd.ExecuteNonQuery()

                            If lstVociBolletta.Items(K).Value = 7 Then
                                IMP_DPC = Replace(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10), ".", "")
                            End If
                        Next
                    Else
                        txtAppare.Value = "1"
                        Response.Write("<script>alert('Attenzione...le voci della bolletta che si cerca di modificare risultano già accertate. Operazione annullata!');</script>")
                        Exit Sub
                    End If
                Else
                    If dataEmissione = txtEmissione.Text And riferimentoDa = txtPeriodoDa.Text And riferimentoA = txtPeriodoAl.Text And tipologiaBoll = cmbTipoCont.SelectedValue Then

                    Else
                        If vociAccertate = 1 Then
                            txtAppare.Value = "1"
                            Response.Write("<script>alert('Attenzione...le voci della bolletta che si cerca di modificare risultano già accertate. Operazione annullata!');</script>")
                            Exit Sub
                        End If
                    End If
                End If


                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET " _
                                    & "DATA_EMISSIONE='" & par.AggiustaData(txtEmissione.Text) _
                                    & "', DATA_SCADENZA='" & par.AggiustaData(txtScadenza.Text) _
                                    & "', NOTE='" & par.PulisciStrSql(txtNote.Text) _
                                    & "', RIFERIMENTO_DA='" & par.AggiustaData(txtPeriodoDa.Text) _
                                    & "', RIFERIMENTO_A='" & par.AggiustaData(txtPeriodoAl.Text) & "', " _
                                    & "ID_TIPO=" & cmbTipoCont.SelectedValue & " WHERE ID=" & V3.Value
                par.cmd.ExecuteNonQuery()


                'max 19/10/2017 verifico se sono gia presenti bollette di DPC non stornate sulla stessa anagrafica
                If cmbTipoCont.SelectedValue = "9" Then
                    par.cmd.CommandText = "select id FROM SISCOM_MI.bol_bollette where id<>" & V3.Value & " and  id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " and id_tipo=9 and id_bolletta_storno is null and cod_affittuario=" & CType(Me.Page.FindControl("txtCodAffittuario"), HiddenField).Value
                    Dim myReaderDPC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderDPC.HasRows = True Then
                        txtAppare.Value = "1"
                        myReaderDPC.Close()
                        Response.Write("<script>alert('Attenzione...esiste già una bolletta di DEPOSITO CAUZIONALE emessa e NON STORNATA per questo contratto! Operazione annullata!');</script>")
                        Exit Sub
                    End If
                    myReaderDPC.Close()
                End If
                '-----

                'max 28/08/2017 ricavo tipo bolletta. se 9 (DPC) devo inserire in STORICO_DEP_CAUZIONALE
                If IMP_DPC > 0 And cmbTipoCont.SelectedValue = 9 Then
                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.STORICO_DEP_CAUZIONALE VALUES (SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.NEXTVAL," & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & idAnagrafica & ",'" & par.AggiustaData(txtDataEmiss.Text) & "'," & par.VirgoleInPunti(IMP_DEP_CAUZ) & " ," & ID_BOLLETTA_NEW & ",'','" & par.AggiustaData(txtDataEmiss.Text) & "',1,NULL,NULL)"
                    'par.cmd.ExecuteNonQuery()

                    'Dim ID_DPC As Long = 0
                    'par.cmd.CommandText = "select SISCOM_MI.SEQ_STORICO_DEP_CAUZIONALE.CURRVAL FROM DUAL"
                    'Dim myReaderDPC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReaderDPC.Read Then
                    '    ID_DPC = myReaderDPC(0)
                    'End If
                    'myReaderDPC.Close()
                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.RAPPORTI_UTENZA_DEP_PROV VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & ID_DPC & ",NULL,NULL,1)"
                    'par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.STORICO_DEP_CAUZIONALE SET IMPORTO=" & par.VirgoleInPunti(IMP_DPC) & " WHERE ID_BOLLETTA=" & V3.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.RAPPORTI_UTENZA SET IMP_DEPOSITO_CAUZ=" & par.VirgoleInPunti(IMP_DPC) & " WHERE ID=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                        & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                        & "'F02','L''IMPORTO DEL DEPOSITO CAUZIONALE VIENE IMPOSTATO A EURO " & IMP_DPC & " A SEGUITO DI MODIFICA BOLLETTA DEPOSITO CAUZIONALE')"
                    par.cmd.ExecuteNonQuery()

                End If
                '-----FINE

                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                '& "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                '& "'F10','')"
                'par.cmd.ExecuteNonQuery()

                imgSalva.Visible = False
                img_InserisciBolletta.Visible = True

                CType(Me.Page, Object).CaricaTabBollette()

                lstBollette.Items.Clear()
                'CARICAMENTO bollette
                Dim num_bolletta As String = ""
                Dim importobolletta As Double = 0
                I = 0

                'par.cmd.CommandText = "select bol_bollette.* from siscom_mi.bol_bollette where bol_bollette.id_unita=" & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value & " and bol_bollette.id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " order by bol_bollette.data_emissione desc"
                par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.* from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND bol_bollette.id_unita=" & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value & " and bol_bollette.id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader2.Read
                    Select Case par.IfNull(myReader2("n_rata"), "")
                        Case "99" 'bolletta manuale
                            num_bolletta = "MA"
                        Case "999" 'bolletta automatica
                            num_bolletta = "AU"
                        Case "99999" 'bolletta di conguaglio
                            num_bolletta = "CO"
                        Case Else
                            num_bolletta = Format(par.IfNull(myReader2("n_rata"), "??"), "00")
                    End Select

                    par.cmd.CommandText = "select SUM(IMPORTO) FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & myReader2("ID")
                    Dim myReaderS1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderS1.Read Then
                        importobolletta = par.IfNull(myReaderS1(0), "0,00")
                    End If
                    myReaderS1.Close()
                    Dim STATO As String = ""
                    If par.IfNull(myReader2("FL_ANNULLATA"), "0") <> "0" Then
                        STATO = "ANNUL."
                    Else
                        STATO = "VALIDA"
                    End If

                    If par.IfNull(myReader2("id_bolletta_ric"), "0") <> "0" Then
                        STATO = "RICLA."
                    End If

                    'lstBollette.Items.Add(New ListItem(par.MiaFormat(num_bolletta, 2) & " " & par.MiaFormat(STATO, 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_da"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_a"), "")), 12) & " " & par.MiaFormat(Format(importobolletta, "0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_emissione"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_scadenza"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_pagamento"), "")), 12) & " " & par.IfNull(myReader2("note"), ""), myReader2("ID")))
                    lstBollette.Items.Add(New ListItem(par.MiaFormat(num_bolletta, 2) & " " & par.MiaFormat(STATO, 7) & " " & par.IfNull(myReader2("ACRONIMO"), "---") & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_da"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("riferimento_a"), "")), 12) & " " & par.MiaFormat(Format(importobolletta, "0.00"), 10) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_emissione"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_scadenza"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_pagamento"), "")), 12) & " " & par.MiaFormat(par.IfNull(myReader2("note"), ""), 35) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_i_sollecito"), "")), 12) & " " & par.MiaFormat(par.FormattaData(par.IfNull(myReader2("data_ii_sollecito"), "")), 12), myReader2("ID")))


                    If I Mod 2 <> 0 Then
                        lstBollette.Items(I).Attributes.CssStyle.Add("background-color", "#dcdada")
                    Else
                        lstBollette.Items(I).Attributes.CssStyle.Add("background-color", "white")
                    End If

                    Select Case par.IfNull(myReader2("ID_TIPO"), "0")
                        Case "3"
                            lstBollette.Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                        Case "4"
                            lstBollette.Items(I).Attributes.CssStyle.Add("background-color", "yellow")
                    End Select

                    I = I + 1
                Loop
                myReader2.Close()
                lstVociBolletta.Items.Clear()
                'Else
                '    txtAppare.Value = "1"
                '    Response.Write("<script>alert('Attenzione... La bolletta non può avere un importo totale inferiore o uguale a 0!');</script>")
                'End If

                V3.Value = ""
                txtTotBoll.Text = ""
                CType(Me.Page, Object).SalvaDati()
            Else
                txtAppare.Value = "1"
                Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno.');</script>")
                'Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
            End If



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


        'txtImportoVoce.Text = ""
        'txtPerRate.Text = ""
        'txtDaRata.Text = ""
    End Sub

    Private Sub SalvaBollettaGest()
        Dim K As Integer = 0
        Dim I As Integer = 0

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            'If Mid(par.AggiustaData(txtPeriodoDa.Text), 1, 6) = Mid(par.AggiustaData(txtPeriodoAl.Text), 1, 6) Then

            Dim tot_bol As Decimal = 0
            For K = 0 To lstVociBolletta.Items.Count - 1
                tot_bol = tot_bol + CDec(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10))
            Next

            tot_bol = Format(tot_bol, "##,##0.00")
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET " _
                                & "DATA_EMISSIONE='" & par.AggiustaData(par.IfEmpty(txtEmissione.Text, "")) _
                                & "',ID_TIPO=" & cmbTipoGest.SelectedValue _
                                & ",IMPORTO_TOTALE=" & par.VirgoleInPunti(tot_bol) _
                                & ", NOTE='" & par.PulisciStrSql(txtNote.Text) _
                                & "', RIFERIMENTO_DA='" & par.AggiustaData(txtPeriodoDa.Text) _
                                & "', RIFERIMENTO_A='" & par.AggiustaData(txtPeriodoAl.Text) _
                                & "' WHERE ID=" & V3.Value
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & V3.Value
            par.cmd.ExecuteNonQuery()

            For K = 0 To lstVociBolletta.Items.Count - 1
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID,ID_BOLLETTA_GEST,ID_VOCE,IMPORTO) VALUES " _
                                    & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & V3.Value & "," & lstVociBolletta.Items(K).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)) & ")"
                par.cmd.ExecuteNonQuery()
            Next

            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            '& "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            '& "'F10','')"
            'par.cmd.ExecuteNonQuery()

            imgSalva.Visible = False
            img_InserisciBolletta.Visible = True

            CaricaGestionale()

            lstBollette.Items.Clear()
            lstVociBolletta.Items.Clear()
            V3.Value = ""
            'Else
            'txtAppare.Value = "1"
            'Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
            'End If

            CType(Me.Page, Object).SalvaDati()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub



    'Private Sub CaricaTabBollette()
    '    Try
    '        Dim num_bolletta As String = ""
    '        Dim I As Integer = 0
    '        Dim importobolletta As Decimal = 0
    '        Dim importopagato As Decimal = 0
    '        Dim residuo As Decimal = 0
    '        Dim morosita As Integer = 0
    '        Dim riclass As Integer = 0
    '        Dim indiceMorosita As Integer = 0
    '        Dim indiceBolletta As Integer = 0
    '        Dim storno As Integer = 0

    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)
    '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
    '            ‘‘par.cmd.Transaction = par.myTrans
    '        End If

    '        'lblSaldoCont.Text = Format(par.CalcolaSaldoAttuale(txtIdContratto.Value), "##,##0.00") & " €"
    '        lblSaldoCont.Text = "<a href='javascript:void(0)' style='text-decoration: none;' link {color:#993333;} visited {color:#993333;} onclick=" & Chr(34) & "javascript:document.getElementById('USCITA').value='1';window.open('../Contabilita/DatiUtenza.aspx?C=RisUtenza&IDANA=" & CType(Me.Page.FindControl("txtCodAffittuario"), HiddenField).Value & "&IDCONT=" & txtIdContratto.Value & "','EstrattoConto','');" & Chr(34) & " alt='Visualizza Dettagli'>" & Format(par.CalcolaSaldoAttuale(txtIdContratto.Value), "##,##0.00") & " €</a>"

    '        'par.cmd.CommandText = "select bol_bollette.id as id,TO_CHAR(TO_DATE(riferimento_da,'yyyymmdd'),'dd/mm/yyyy') as riferimento_da,TO_CHAR(TO_DATE(riferimento_a,'yyyymmdd'),'dd/mm/yyyy') as riferimento_a,TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') as data_emissione,TO_CHAR(TO_DATE(data_scadenza,'yyyymmdd'),'dd/mm/yyyy') as data_scadenza,TO_CHAR(TO_DATE(data_pagamento,'yyyymmdd'),'dd/mm/yyyy') as data_pagamento,note,id_tipo," _
    '        '  & "((case when n_rata ='99' then 'MA' when n_rata ='999' then 'AU' when n_rata ='99999' then 'CO' ELSE TO_CHAR(n_rata) END)" _
    '        '  & "|| ' ' ||" _
    '        '  & "(case when NVL(ID_BOLLETTA_RIC,0) <> 0 OR NVL(ID_RATEIZZAZIONE,0) <> 0 THEN 'RICLA.' when NVL(FL_ANNULLATA,0) <> 0 then 'ANNULL.' ELSE 'VALIDA'  END)" _
    '        '  & "|| ' ' ||" _
    '        '  & "ACRONIMO) AS NUM_TIPO," _
    '        '  & "(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0)) AS imp_residuo," _
    '        '  & "(CASE WHEN ID_TIPO = 4 THEN 'SI' ELSE 'NO' END) AS FL_MORA," _
    '        '  & "(CASE WHEN NVL(ID_RATEIZZAZIONE,0)<>0 THEN 'SI' ELSE 'NO' END) AS fl_rateizz,importo_pagato as imp_pagato,importo_totale as importobolletta" _
    '        '  & " from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND bol_bollette.id_unita=" & UnitaContratto & " and bol_bollette.id_contratto=" & lIdContratto & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"
    '        par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.* from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) and bol_bollette.id_contratto=" & txtIdContratto.Value & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"
    '        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dtQuery As New Data.DataTable
    '        Dim dt1 As New Data.DataTable
    '        Dim rowDT As System.Data.DataRow

    '        dt1.Columns.Add("id")
    '        dt1.Columns.Add("num_tipo")
    '        dt1.Columns.Add("riferimento_da")
    '        dt1.Columns.Add("riferimento_a")
    '        dt1.Columns.Add("data_emissione")
    '        dt1.Columns.Add("data_scadenza")
    '        dt1.Columns.Add("importobolletta")
    '        dt1.Columns.Add("imp_pagato")
    '        dt1.Columns.Add("imp_residuo")
    '        dt1.Columns.Add("data_pagamento")
    '        dt1.Columns.Add("note")
    '        dt1.Columns.Add("fl_mora")
    '        dt1.Columns.Add("fl_rateizz")
    '        dt1.Columns.Add("id_tipo")
    '        dt1.Columns.Add("dettagli")
    '        dt1.Columns.Add("anteprima")
    '        dt1.Columns.Add("importo_ruolo")

    '        da1.Fill(dtQuery)
    '        da1.Dispose()

    '        Dim TOTimportobolletta As Decimal = 0
    '        Dim TOTimportopagato As Decimal = 0
    '        Dim TOTimportoresiduo As Decimal = 0
    '        Dim TOTImpRuolo As Decimal = 0

    '        For Each row As Data.DataRow In dtQuery.Rows
    '            indiceMorosita = 0
    '            indiceBolletta = 0
    '            rowDT = dt1.NewRow()

    '            Select Case par.IfNull(row.Item("n_rata"), "")
    '                Case "99" 'bolletta manuale
    '                    num_bolletta = "MA"
    '                Case "999" 'bolletta automatica
    '                    num_bolletta = "AU"
    '                Case "99999" 'bolletta di conguaglio
    '                    num_bolletta = "CO"
    '                Case Else
    '                    num_bolletta = Format(par.IfNull(row.Item("n_rata"), "??"), "00")
    '            End Select

    '            importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_B"), 0) '
    '            If par.IfNull(row.Item("FL_ANNULLATA"), 0) = 0 Or (par.IfNull(row.Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(row.Item("data_pagamento"), "") = "") Then
    '                TOTimportobolletta = TOTimportobolletta + importobolletta
    '            End If

    '            importopagato = (par.IfNull(row.Item("IMPORTO_PAGATO"), "0,00") - par.IfNull(row.Item("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(row.Item("QUOTA_SIND_PAGATA_B"), 0))
    '            If par.IfNull(row.Item("FL_ANNULLATA"), 0) = 0 Or (par.IfNull(row.Item("FL_ANNULLATA"), 0) <> 0 And par.IfNull(row.Item("data_pagamento"), "") = "") Then
    '                TOTimportopagato = TOTimportopagato + importopagato
    '            End If

    '            Dim STATO As String = ""
    '            If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> "0" Then
    '                STATO = "ANNUL."
    '            Else
    '                STATO = "VALIDA"
    '            End If
    '            If par.IfNull(row.Item("id_bolletta_ric"), "0") <> "0" Or par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
    '                STATO = "RICLA."
    '                riclass = 1
    '            End If
    '            If par.IfNull(row.Item("id_bolletta_storno"), "0") <> "0" Then
    '                STATO = "STORN."
    '            End If

    '            residuo = importobolletta - importopagato
    '            TOTimportoresiduo = TOTimportoresiduo + residuo

    '            'Select Case par.IfNull(row.Item("ID_TIPO"), "0")
    '            '    Case "3"
    '            '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
    '            '    Case "4"
    '            '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
    '            'End Select

    '            rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "---")
    '            rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
    '            rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
    '            rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))
    '            rowDT.Item("data_scadenza") = par.FormattaData(par.IfNull(row.Item("data_scadenza"), ""))
    '            rowDT.Item("importobolletta") = Format(importobolletta, "##,##0.00")
    '            rowDT.Item("imp_pagato") = Format(importopagato, "##,##0.00")
    '            rowDT.Item("imp_residuo") = Format(residuo, "##,##0.00")
    '            rowDT.Item("data_pagamento") = par.FormattaData(par.IfNull(row.Item("data_pagamento"), ""))
    '            rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
    '            rowDT.Item("importo_ruolo") = Format(par.IfNull(row.Item("IMPORTO_RUOLO"), 0), "##,##0.00")
    '            'If par.IfNull(row.Item("ID_TIPO"), "0") = 4 Then
    '            '    rowDT.Item("fl_mora") = "<a href=javascript:apriMorosita();void(0); onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & ">SI</a>"
    '            'Else
    '            '    rowDT.Item("fl_mora") = "NO"
    '            'End If
    '            TOTImpRuolo = TOTImpRuolo + rowDT.Item("importo_ruolo")
    '            If par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
    '                rowDT.Item("fl_rateizz") = "<a href=""javascript:void(0)"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';window.open('DettRateizz.aspx?IDB=" & par.IfNull(row.Item("id"), 0) & "', '', 'height=550,top=0,left=0,width=800');" & Chr(34) & ">SI</a>"
    '            Else
    '                rowDT.Item("fl_rateizz") = "NO"
    '            End If

    '            indiceMorosita = par.IfNull(row.Item("id_morosita"), 0)

    '            If indiceMorosita <> 0 And par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
    '                rowDT.Item("fl_mora") = "<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & ">SI</a>"
    '            Else
    '                rowDT.Item("fl_mora") = "NO"
    '            End If

    '            rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)

    '            rowDT.Item("id") = par.IfNull(row.Item("id"), 0)

    '            If rowDT.Item("id_tipo") = "3" Or rowDT.Item("id_tipo") = "4" Then
    '                morosita = 1
    '            End If

    '            Select Case par.IfNull(row.Item("id_tipo"), 0)
    '                Case "3"
    '                    indiceBolletta = par.IfNull(row.Item("id"), 0)
    '                Case "4"
    '                    indiceMorosita = par.IfNull(row.Item("id_morosita"), "")
    '                    indiceBolletta = 0
    '                Case "5"
    '                    indiceBolletta = par.IfNull(row.Item("id"), 0)
    '                    indiceBolletta = 0
    '                Case "22"
    '                    storno = 1

    '            End Select

    '            If par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
    '                indiceBolletta = par.IfNull(row.Item("id"), 0)
    '            End If

    '            If par.IfNull(row.Item("id_rateizzazione"), 0) <> 0 Then
    '                indiceBolletta = par.IfNull(row.Item("id"), 0)
    '            End If

    '            If indiceBolletta = 0 Then
    '                rowDT.Item("dettagli") = ""

    '            Else
    '                If indiceMorosita = 0 And par.IfNull(row.Item("importo_ric_b"), 0) <> 0 Then
    '                    DataGridContab.Columns(2).Visible = True
    '                    divContabCon.Value = "1"
    '                    rowDT.Item("dettagli") = "<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '                End If
    '            End If

    '            rowDT.Item("anteprima") = "<a href=""javascript:ApriAnteprima();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"


    '            If par.IfNull(row.Item("id_tipo"), 0) = 22 Then
    '                DataGridContab.Columns(2).Visible = True

    '                divContabCon.Value = "1"
    '                rowDT.Item("dettagli") = "<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '            End If

    '            dt1.Rows.Add(rowDT)
    '        Next
    '        lblImpRuolo.Text = Format(TOTImpRuolo, "##,##0.00") & " €"

    '        rowDT = dt1.NewRow()
    '        rowDT.Item("id") = -1
    '        rowDT.Item("num_tipo") = ""
    '        rowDT.Item("riferimento_da") = ""
    '        rowDT.Item("riferimento_a") = "TOTALE"
    '        rowDT.Item("data_emissione") = ""
    '        rowDT.Item("data_scadenza") = ""
    '        rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
    '        rowDT.Item("imp_pagato") = Format(TOTimportopagato, "##,##0.00")
    '        rowDT.Item("imp_residuo") = Format(TOTimportoresiduo, "##,##0.00")
    '        rowDT.Item("data_pagamento") = ""
    '        rowDT.Item("note") = ""
    '        rowDT.Item("fl_mora") = ""
    '        rowDT.Item("fl_rateizz") = ""
    '        rowDT.Item("importo_ruolo") = Format(TOTImpRuolo, "##,##0.00")
    '        rowDT.Item("id_tipo") = ""
    '        rowDT.Item("dettagli") = ""
    '        rowDT.Item("anteprima") = ""

    '        dt1.Rows.Add(rowDT)

    '        DataGridContab.DataSource = dt1
    '        DataGridContab.DataBind()

    '        'If morosita = 1 Then
    '        '    For Each di As DataGridItem In DataGridContab.Items
    '        '        If di.Cells(15).Text.Contains("3") Or di.Cells(15).Text.Contains("4") Then
    '        '            di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
    '        '        End If
    '        '    Next
    '        'End If
    '        If riclass = 1 Then
    '            For Each di As DataGridItem In DataGridContab.Items
    '                If di.Cells(3).Text.Contains("RICLA.") Then
    '                    di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
    '                End If
    '            Next
    '        End If

    '        If storno = 1 Then
    '            For Each di As DataGridItem In DataGridContab.Items
    '                If di.Cells(16).Text = "22" Then
    '                    di.ForeColor = Drawing.ColorTranslator.FromHtml("blue")
    '                End If
    '                If di.Cells(3).Text.Contains("STORN.") Then
    '                    di.ForeColor = Drawing.ColorTranslator.FromHtml("blue")
    '                End If
    '            Next
    '            'For Each row As Data.DataRow In dtQuery.Rows
    '            '    If par.IfNull(row.Item("ID_BOLLETTA_STORNATA"), 0) <> "0" Then
    '            '        For Each di As DataGridItem In DataGridContab.Items
    '            '            If di.Cells(0).Text = par.IfNull(row.Item("ID_BOLLETTA_STORNO"), "") Then
    '            '                di.ForeColor = Drawing.ColorTranslator.FromHtml("blue")
    '            '                Exit For
    '            '            End If
    '            '        Next
    '            '    End If
    '            'Next
    '        End If
    '        For Each di As DataGridItem In DataGridContab.Items
    '            If di.Cells(5).Text.Contains("TOTALE") Then
    '                For j As Integer = 0 To di.Cells.Count - 1
    '                    If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
    '                        di.Cells(j).Font.Bold = True
    '                        di.Cells(j).Font.Underline = True
    '                    End If
    '                Next
    '                'di.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
    '                'di.ForeColor = Drawing.ColorTranslator.FromHtml("white")
    '                'di.Attributes.Add("onmouseover", "this.style.backgroundColor='#006699'")
    '                'di.Attributes.Add("onmouseout", "this.style.backgroundColor='#006699'")
    '            End If
    '        Next

    '        CaricaGestionale()

    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaTabBoll) - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub

    'Private Sub CaricaGestionale()
    '    Try
    '        Dim num_bolletta As String = ""
    '        Dim I As Integer = 0
    '        Dim importobolletta As Decimal = 0
    '        Dim importopagato As Decimal = 0
    '        Dim residuo As Decimal = 0
    '        Dim morosita As Integer = 0
    '        Dim riclass As Integer = 0
    '        Dim indiceMorosita As Integer = 0
    '        Dim indiceBolletta As Integer = 0

    '        'par.cmd.CommandText = "select bol_bollette.id as id,TO_CHAR(TO_DATE(riferimento_da,'yyyymmdd'),'dd/mm/yyyy') as riferimento_da,TO_CHAR(TO_DATE(riferimento_a,'yyyymmdd'),'dd/mm/yyyy') as riferimento_a,TO_CHAR(TO_DATE(data_emissione,'yyyymmdd'),'dd/mm/yyyy') as data_emissione,TO_CHAR(TO_DATE(data_scadenza,'yyyymmdd'),'dd/mm/yyyy') as data_scadenza,TO_CHAR(TO_DATE(data_pagamento,'yyyymmdd'),'dd/mm/yyyy') as data_pagamento,note,id_tipo," _
    '        '  & "((case when n_rata ='99' then 'MA' when n_rata ='999' then 'AU' when n_rata ='99999' then 'CO' ELSE TO_CHAR(n_rata) END)" _
    '        '  & "|| ' ' ||" _
    '        '  & "(case when NVL(ID_BOLLETTA_RIC,0) <> 0 OR NVL(ID_RATEIZZAZIONE,0) <> 0 THEN 'RICLA.' when NVL(FL_ANNULLATA,0) <> 0 then 'ANNULL.' ELSE 'VALIDA'  END)" _
    '        '  & "|| ' ' ||" _
    '        '  & "ACRONIMO) AS NUM_TIPO," _
    '        '  & "(NVL(IMPORTO_TOTALE,0) - NVL(IMPORTO_PAGATO,0)) AS imp_residuo," _
    '        '  & "(CASE WHEN ID_TIPO = 4 THEN 'SI' ELSE 'NO' END) AS FL_MORA,'' as sposta,'' as anteprima," _
    '        '  & "(CASE WHEN NVL(ID_RATEIZZAZIONE,0)<>0 THEN 'SI' ELSE 'NO' END) AS fl_rateizz,importo_pagato as imp_pagato,importo_totale as importobolletta" _
    '        '  & " from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND bol_bollette.id_unita=" & UnitaContratto & " and bol_bollette.id_contratto=" & lIdContratto & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"
    '        par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.* from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND bol_bollette.id_unita=" & CType(Me.Page, Object).UnitaContratto & " and bol_bollette.id_contratto=" & txtIdContratto.Value & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"
    '        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dtQuery2 As New Data.DataTable
    '        Dim dt1 As New Data.DataTable
    '        Dim rowDT As System.Data.DataRow

    '        dt1.Columns.Add("id")
    '        dt1.Columns.Add("num_tipo")
    '        dt1.Columns.Add("riferimento_da")
    '        dt1.Columns.Add("riferimento_a")
    '        dt1.Columns.Add("data_emissione")
    '        dt1.Columns.Add("data_scadenza")
    '        dt1.Columns.Add("importobolletta")
    '        dt1.Columns.Add("data_pagamento")
    '        dt1.Columns.Add("note")
    '        dt1.Columns.Add("dettagli")
    '        dt1.Columns.Add("anteprima")
    '        dt1.Columns.Add("sposta")
    '        dt1.Columns.Add("id_tipo")

    '        da1.Fill(dtQuery2)
    '        da1.Dispose()

    '        Dim TOTimportobolletta As Decimal = 0

    '        For Each row As Data.DataRow In dtQuery2.Rows
    '            indiceMorosita = 0
    '            indiceBolletta = 0
    '            rowDT = dt1.NewRow()

    '            Select Case par.IfNull(row.Item("n_rata"), "")
    '                Case "99" 'bolletta manuale
    '                    num_bolletta = "MA"
    '                Case "999" 'bolletta automatica
    '                    num_bolletta = "AU"
    '                Case "99999" 'bolletta di conguaglio
    '                    num_bolletta = "CO"
    '                Case Else
    '                    num_bolletta = Format(par.IfNull(row.Item("n_rata"), "??"), "00")
    '            End Select

    '            importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
    '            TOTimportobolletta = TOTimportobolletta + importobolletta

    '            importopagato = par.IfNull(row.Item("IMPORTO_PAGATO"), "0,00")

    '            Dim STATO As String = ""
    '            If par.IfNull(row.Item("FL_ANNULLATA"), "0") <> "0" Then
    '                STATO = "ANNUL."
    '            Else
    '                STATO = "VALIDA"
    '            End If
    '            If par.IfNull(row.Item("id_bolletta_ric"), "0") <> "0" Or par.IfNull(row.Item("ID_RATEIZZAZIONE"), "0") <> "0" Then
    '                STATO = "RICLA."
    '                riclass = 1
    '            End If

    '            residuo = importobolletta - importopagato
    '            'Select Case par.IfNull(row.Item("ID_TIPO"), "0")
    '            '    Case "3"
    '            '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
    '            '    Case "4"
    '            '        CType(Tab_Bollette1.FindControl("lstBollette"), ListBox).Items(I).Attributes.CssStyle.Add("background-color", "yellow")
    '            'End Select

    '            rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "")
    '            rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
    '            rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
    '            rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))
    '            rowDT.Item("data_scadenza") = par.FormattaData(par.IfNull(row.Item("data_scadenza"), ""))
    '            rowDT.Item("importobolletta") = Format(importobolletta, "##,##0.00")
    '            rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
    '            'rowDT.Item("anteprima") = "<a href='javascript:window.open('''',''Dettagli'',''height=580,top=0,left=0,width=780'');void(0);'><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

    '            rowDT.Item("sposta") = "<a href=""javascript:alert('Non_disponibile!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '            rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)
    '            rowDT.Item("id") = par.IfNull(row.Item("id"), 0)

    '            If rowDT.Item("id_tipo") = "3" Or rowDT.Item("id_tipo") = "4" Then
    '                morosita = 1
    '            End If

    '            Select Case par.IfNull(row.Item("id_tipo"), 0)
    '                Case "3"
    '                    indiceBolletta = par.IfNull(row.Item("id"), 0)
    '                Case "4"
    '                    indiceMorosita = par.IfNull(row.Item("id_morosita"), "")
    '                    indiceBolletta = 0
    '                Case "5"
    '                    indiceBolletta = par.IfNull(row.Item("id"), 0)
    '            End Select

    '            If par.IfNull(row.Item("id_bolletta_ric"), 0) <> 0 Then
    '                indiceBolletta = par.IfNull(row.Item("id"), 0)
    '            End If

    '            If par.IfNull(row.Item("id_rateizzazione"), 0) <> 0 Then
    '                indiceBolletta = par.IfNull(row.Item("id"), 0)
    '            End If

    '            If indiceBolletta = 0 Then
    '                rowDT.Item("dettagli") = ""
    '            Else
    '                DataGridGest.Columns(10).Visible = True
    '                rowDT.Item("dettagli") = "<a href=""javascript:apriMorosita();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Dettagli Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/Img_Info.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
    '            End If

    '            rowDT.Item("anteprima") = "<a href=""javascript:ApriAnteprima();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

    '            dt1.Rows.Add(rowDT)
    '        Next

    '        rowDT = dt1.NewRow()
    '        rowDT.Item("id") = -1
    '        rowDT.Item("num_tipo") = ""
    '        rowDT.Item("riferimento_da") = ""
    '        rowDT.Item("riferimento_a") = "TOTALE"
    '        rowDT.Item("data_emissione") = ""
    '        rowDT.Item("data_scadenza") = ""
    '        rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
    '        rowDT.Item("data_pagamento") = ""
    '        rowDT.Item("note") = ""
    '        rowDT.Item("id_tipo") = ""
    '        rowDT.Item("dettagli") = ""
    '        rowDT.Item("anteprima") = ""
    '        dt1.Rows.Add(rowDT)

    '        DataGridGest.DataSource = dt1
    '        DataGridGest.DataBind()


    '        If morosita = 1 Then
    '            For Each di As DataGridItem In DataGridGest.Items
    '                If di.Cells(11).Text.Contains("3") Or di.Cells(11).Text.Contains("4") Then
    '                    di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
    '                End If
    '            Next
    '        End If
    '        If riclass = 1 Then
    '            For Each di As DataGridItem In DataGridGest.Items
    '                If di.Cells(1).Text.Contains("RICLA.") Then
    '                    di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
    '                End If
    '            Next
    '        End If

    '        For Each di As DataGridItem In DataGridGest.Items
    '            If di.Cells(3).Text.Contains("TOTALE") Then
    '                For j As Integer = 0 To di.Cells.Count - 1
    '                    If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
    '                        di.Cells(j).Font.Bold = True
    '                        di.Cells(j).Font.Underline = True
    '                    End If
    '                Next
    '                'di.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
    '                'di.ForeColor = Drawing.ColorTranslator.FromHtml("white")
    '                'di.Attributes.Add("onmouseover", "this.style.backgroundColor='#006699'")
    '                'di.Attributes.Add("onmouseout", "this.style.backgroundColor='#006699'")
    '            End If
    '        Next

    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaSchemaBoll) - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Private Sub CaricaGestionale()
        Try
            Dim num_bolletta As String = ""
            Dim I As Integer = 0
            Dim importobolletta As Decimal = 0
            Dim importobolletta2 As Decimal = 0
            Dim importopagato As Decimal = 0
            Dim residuo As Decimal = 0
            Dim morosita As Integer = 0
            Dim riclass As Integer = 0
            Dim indiceMorosita As Integer = 0
            Dim indiceBolletta As Integer = 0

            par.cmd.CommandText = "select TIPO_BOLLETTE_GEST.ACRONIMO,TIPO_BOLLETTE_GEST.FL_RIPARTIBILE,bol_bollette_gest.* from SISCOM_MI.TIPO_BOLLETTE_GEST,siscom_mi.bol_bollette_GEST where BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID (+)" _
                & "AND bol_bollette_gest.id_contratto=" & txtIdContratto.Value & " AND FL_VISUALIZZABILE=1 order by bol_bollette_gest.data_emissione desc,bol_bollette_gest.id desc"
            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtQuery2 As New Data.DataTable
            Dim dt1 As New Data.DataTable
            Dim rowDT As System.Data.DataRow

            dt1.Columns.Add("id")
            dt1.Columns.Add("num_tipo")
            dt1.Columns.Add("riferimento_da")
            dt1.Columns.Add("riferimento_a")
            dt1.Columns.Add("data_emissione")
            dt1.Columns.Add("importobolletta")
            dt1.Columns.Add("data_pagamento")
            dt1.Columns.Add("note")
            dt1.Columns.Add("anteprima")
            dt1.Columns.Add("sposta")
            dt1.Columns.Add("dettagli")
            dt1.Columns.Add("id_tipo")
            dt1.Columns.Add("tipo_appl")

            da1.Fill(dtQuery2)
            da1.Dispose()

            Dim TOTimportobolletta As Decimal = 0
            Dim importoVoceEmessa As Decimal = 0

            For Each row As Data.DataRow In dtQuery2.Rows
                indiceMorosita = 0
                indiceBolletta = 0
                rowDT = dt1.NewRow()

                importobolletta = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
                If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "N") = "N" Then
                    importobolletta2 = par.IfNull(row.Item("IMPORTO_TOTALE"), "0,00")
                End If

                'CONTROLLO IN BOL_BOLLETTE_VOCI SE è stata emessa quella bolletta (in bol_bollette)
                par.cmd.CommandText = "SELECT ID AS ID_VOCE_GEST FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & par.IfNull(row.Item("ID"), 0)
                Dim daVociGest As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtVociGest As New Data.DataTable
                Dim rowDTVociGest As System.Data.DataRow
                daVociGest.Fill(dtVociGest)
                daVociGest.Dispose()


                If dtVociGest.Rows.Count > 0 Then
                    For Each rowDTVociGest In dtVociGest.Rows
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_VOCE_BOLLETTA_GEST=" & par.IfNull(rowDTVociGest.Item("ID_VOCE_GEST"), 0)
                        Dim daVociNew As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtVociNew As New Data.DataTable
                        Dim rowDTVociNew As System.Data.DataRow
                        daVociNew.Fill(dtVociNew)
                        daVociNew.Dispose()

                        If dtVociNew.Rows.Count > 0 Then
                            For Each rowDTVociNew In dtVociNew.Rows
                                importoVoceEmessa += par.IfNull(rowDTVociNew.Item("IMPORTO"), 0)
                            Next
                        End If
                    Next
                End If

                Dim STATO As String = ""

                residuo = importobolletta - importoVoceEmessa

                TOTimportobolletta = TOTimportobolletta + (importobolletta2 - importoVoceEmessa)

                rowDT.Item("num_tipo") = num_bolletta & " " & STATO & " " & par.IfNull(row.Item("ACRONIMO"), "---")
                rowDT.Item("riferimento_da") = par.FormattaData(par.IfNull(row.Item("riferimento_da"), ""))
                rowDT.Item("riferimento_a") = par.FormattaData(par.IfNull(row.Item("riferimento_a"), ""))
                rowDT.Item("data_emissione") = par.FormattaData(par.IfNull(row.Item("data_emissione"), ""))

                rowDT.Item("importobolletta") = Format(residuo, "##,##0.00")
                rowDT.Item("note") = par.IfNull(row.Item("NOTE"), "")
                rowDT.Item("anteprima") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettaglioVociGest.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Dettagli" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Details-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"

                If rowDT.Item("importobolletta") > 0 Then
                    If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "P" Then
                        rowDT.Item("sposta") = "<a href=""javascript:alert('Attenzione questa scrittura risulta già distribuita in rate come voce nello schema bollette!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    Else
                        rowDT.Item("sposta") = "<a href=""javascript:ScegliElaborazione();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    End If
                Else
                    If par.IfNull(row.Item("TIPO_APPLICAZIONE"), "") = "P" Then
                        rowDT.Item("sposta") = "<a href=""javascript:alert('Attenzione il credito è stato gestito parzialmente!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    Else
                        rowDT.Item("sposta") = "<a href=""javascript:ScegliElaborazione();void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_move.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                    End If
                End If

                rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)
                rowDT.Item("id") = par.IfNull(row.Item("id"), 0)


                'rowDT.Item("anteprima") = "<a href=javascript:ApriAnteprima();void(0); onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                'rowDT.Item("anteprima") = "<a href=""javascript:alert('Non disponibile!');void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Info bolletta" & Chr(34) & " title=" & Chr(34) & "Visualizza Bolletta" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "16px" & Chr(34) & " src=" & Chr(34) & "../NuoveImm/search-icon.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                rowDT.Item("tipo_appl") = par.IfNull(row.Item("TIPO_APPLICAZIONE"), "")
                importobolletta = 0
                importobolletta2 = 0

                Select Case rowDT.Item("TIPO_APPL")
                    Case "P"
                        DataGridGest.Columns(3).Visible = True

                        divGestCon.Value = "1"
                        If par.IfNull(rowDT.Item("importobolletta"), 0) < 0 Then
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.showModalDialog('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest', 'status:no;dialogWidth:630px;dialogHeight:250px;dialogHide:true;help:no;scroll:yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        Else
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        End If
                    Case "T"
                        DataGridGest.Columns(3).Visible = True
                        divGestCon.Value = "1"
                        If par.IfNull(rowDT.Item("importobolletta"), 0) < 0 Then
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.showModalDialog('DettagliGestionaleCrediti.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest', 'status:no;dialogWidth:630px;dialogHeight:250px;dialogHide:true;help:no;scroll:yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        Else
                            rowDT.Item("dettagli") = "<a onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & " href=""javascript:window.open('DettagliGestionale.aspx?IDBOLL=" & par.IfNull(row.Item("id"), 0) & "','DettGest','height=250,top=200,left=410,width=630,resizable=yes,menubar=no,toolbar=no,scrollbars=yes');void(0);""><img alt=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " title=" & Chr(34) & "Dettagli Elaborazione" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "20px" & Chr(34) & " src=" & Chr(34) & "Immagini/ImgDett.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                        End If
                    Case "N"
                        rowDT.Item("dettagli") = ""

                End Select
                If par.IfNull(row.Item("FL_RIPARTIBILE"), 0) = 0 Then
                    rowDT.Item("sposta") = "<a href=""javascript:void(0);"" onclick=" & Chr(34) & "document.getElementById('USCITA').value='1';" & Chr(34) & "><img alt=" & Chr(34) & "Attiva funzione" & Chr(34) & " title=" & Chr(34) & "Sposta in partita contabile" & Chr(34) & " border=" & Chr(34) & "0" & Chr(34) & " width=" & Chr(34) & "15px" & Chr(34) & " src=" & Chr(34) & "Immagini/Img_divieto.png" & Chr(34) & " style=" & Chr(34) & "cursor:pointer" & Chr(34) & " /></a>"
                End If
                dt1.Rows.Add(rowDT)
            Next

            rowDT = dt1.NewRow()
            rowDT.Item("id") = -1
            rowDT.Item("num_tipo") = ""
            rowDT.Item("riferimento_da") = ""
            rowDT.Item("riferimento_a") = "TOTALE"
            rowDT.Item("data_emissione") = ""
            rowDT.Item("importobolletta") = Format(TOTimportobolletta, "##,##0.00")
            rowDT.Item("data_pagamento") = ""
            rowDT.Item("note") = ""
            rowDT.Item("id_tipo") = ""
            rowDT.Item("tipo_appl") = ""
            rowDT.Item("anteprima") = ""
            rowDT.Item("dettagli") = ""
            dt1.Rows.Add(rowDT)

            If Session.Item("MOD_ELAB_SING_GEST") = 1 Then
                DataGridGest.Columns(1).Visible = True
            Else
                DataGridGest.Columns(1).Visible = False
            End If

            DataGridGest.DataSource = dt1
            DataGridGest.DataBind()


            For Each di As DataGridItem In DataGridGest.Items
                If di.Cells(6).Text.Contains("TOTALE") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        If di.Cells(j).Text <> "&nbsp;" And di.Cells(j).Text <> "-1" Then
                            di.Cells(j).Font.Bold = True
                            di.Cells(j).Font.Underline = True
                        End If
                    Next
                    'di.BackColor = Drawing.ColorTranslator.FromHtml("#006699")
                    'di.ForeColor = Drawing.ColorTranslator.FromHtml("white")
                    'di.Attributes.Add("onmouseover", "this.style.backgroundColor='#006699'")
                    'di.Attributes.Add("onmouseout", "this.style.backgroundColor='#006699'")
                End If
            Next

            For Each di As DataGridItem In DataGridGest.Items
                If di.Cells(12).Text.Contains("P") Then
                    di.ForeColor = Drawing.ColorTranslator.FromHtml("red")
                    di.ToolTip = "Documento già elaborato con scrittura delle voci in schema bollette! L'importo scalerà in base alle future emissioni."
                End If
                If di.Cells(12).Text.Contains("T") Then
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(j).Font.Strikeout = True
                    Next
                End If
            Next

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE", par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " (funzione CaricaSchemaBoll) - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        If menuGest.Value = "1" Then
            SalvaBollettaGest()
            txtTotBoll.Text = ""
        Else
            SalvaBolletta()
            txtTotBoll.Text = ""
        End If
    End Sub

    Public Function SVUOTA()
        lstVociBolletta.Items.Clear()
    End Function


    Private Sub SettaDimensioneCella()
        For Each di As DataGridItem In DataGridGest.Items
            For j As Integer = 0 To di.Cells.Count - 1
                di.Cells(j).Width = 200
            Next
        Next
    End Sub

    Protected Sub img_EliminaVoce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_EliminaVoce.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstVociBolletta.SelectedIndex >= 0 Then

            Dim tot_bol As Decimal = 0

            Dim i As Integer
            For i = 0 To lstVociBolletta.Items.Count - 1
                If Trim(lstVociBolletta.Items(i).Text) = Trim(V2.Value) Then
                    lstVociBolletta.Items.Remove(lstVociBolletta.Items(i))
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                    par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    For K = 0 To lstVociBolletta.Items.Count - 1
                        tot_bol = tot_bol + par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F17','Importo voce: " & tot_bol & " euro ; Tipologia: " & par.RicavaTesto(lstVociBolletta.Items(K).Text, 1, 31) & "')"
                        par.cmd.ExecuteNonQuery()
                    Next

                    txtTotBoll.Text = Format(tot_bol, "0.00")


                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un Elemento della lista!');</SCRIPT>")
        End If

    End Sub


    Public Function DisattivaTutto()
        lstBollette.Enabled = True

        imgSalva.Visible = False

        ImgAnteprima.Visible = True

        ImgMavOnLine.Visible = False


    End Function

    Public Function DisattivaTuttoVirtuale()
        lstBollette.Enabled = True

        imgSalva.Visible = False

        ImgAnteprima.Visible = True

        ImgMavOnLine.Visible = False


    End Function

    'Protected Sub btnMavonline_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnMavonline.Click
    '    Try
    '        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '        If lstBollette.SelectedIndex >= 0 Then
    '            Dim i As Integer
    '            For i = 0 To lstBollette.Items.Count - 1
    '                If Trim(lstBollette.Items(i).Text) = Trim(V1.Value) Then
    '                    IdBolletta.Value = lstBollette.Items(i).Value
    '                    'Response.Write("<script>var fin;fin=window.open('Sondrio.aspx?ID=" & IdBolletta.Value & "','MAV','top=0,left=0');fin.focus();</script>")
    '                    Response.Write("<script>window.showModalDialog('Sondrio.aspx?ID=" & IdBolletta.Value & "','status:no;dialogWidth:550px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');</script>")


    '                End If
    '            Next
    '        Else
    '            Response.Write("<SCRIPT>alert('Selezionare un componente della lista!');</SCRIPT>")
    '        End If

    '    Catch ex As Exception
    '        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
    '        Session.Item("LAVORAZIONE") = "0"

    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
    '    End Try
    'End Sub


    Protected Sub img_CopiaSchema_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_CopiaSchema.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "select bol_schema.id,bol_schema.importo,bol_schema.importo_singola_rata,bol_schema.id_voce,t_voci_bolletta.descrizione from siscom_mi.t_voci_bolletta,siscom_mi.bol_schema where bol_schema.anno=" & Year(Now) & " and t_voci_bolletta.id=bol_schema.id_voce and bol_schema.id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " order by t_voci_bolletta.descrizione asc"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader2.Read
                lstVociBolletta.Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("descrizione"), ""), 30) & " " & par.MiaFormat(Format(par.IfNull(myReader2("importo_singola_rata"), "0,00"), "0.00"), 10) & " " & par.MiaFormat("", 30), par.IfNull(myReader2("id_voce"), "-1")))
            Loop
            myReader2.Close()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F16','Copia di tutte le voci dallo schema anno " & Year(Now) & "')"
            par.cmd.ExecuteNonQuery()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Public Property idEventoPrincipale() As Long
        Get
            If Not (ViewState("par_idEventoPrincipale") Is Nothing) Then
                Return CLng(ViewState("par_idEventoPrincipale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEventoPrincipale") = value
        End Set

    End Property

    Public Property idIncasso() As Long
        Get
            If Not (ViewState("par_idIncasso") Is Nothing) Then
                Return CLng(ViewState("par_idIncasso"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idIncasso") = value
        End Set

    End Property

   
    Public Property tot_bolDaSt() As Decimal
        Get
            If Not (ViewState("par_percenSost") Is Nothing) Then
                Return CDec(ViewState("par_percenSost"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_percenSost") = value
        End Set

    End Property

    Private Sub RicavaPercentSost()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)

            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_GEST_CREDITO WHERE ID=1"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                percenSost.Value = par.IfNull(myReader("PERC_SOSTEN"), 1)
            End If
            myReader.Close()

            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub DataGridGest_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridGest.ItemDataBound
        'Dim item As MenuItem

        'If e.Item.ItemType = ListItemType.Header Then
        '    e.Item.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")

        '    item = New MenuItem("Crea Nuova Bolletta", "NBoll", "", "javascript:IMG1_onclick();")
        '    CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)

        '    item = New MenuItem("Modifica Bolletta", "MBoll", "", "javascript:document.getElementById('Tab_Bollette1_btnModificaBolletta').click();")
        '    CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)
        'End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFD784';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';" _
                                & "document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";document.getElementById('Tab_Bollette1_importo').value=" & par.VirgoleInPunti(e.Item.Cells(8).Text) & ";")
        End If


        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#D6D6D6';}")

        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";document.getElementById('Tab_Bollette1_importo').value=" & e.Item.Cells(5).Text & ";")
        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#F7F7F7';}")
        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";document.getElementById('Tab_Bollette1_importo').value=" & e.Item.Cells(5).Text & ";")

        'End If
    End Sub

    Protected Sub DataGridContab_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridContab.ItemDataBound
        'Dim item As MenuItem
        ''item = New MenuItem("Crea Nuova Bolletta", "NBoll", "", "javascript:ApriMav();")

        'If e.Item.ItemType = ListItemType.Header Then
        '    e.Item.Attributes.Add("onclick", "javascript:document.getElementById('USCITA').value='1';")

        '    item = New MenuItem("Crea Nuova Bolletta", "NBoll", "", "javascript:IMG1_onclick();")
        '    CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)

        '    item = New MenuItem("Modifica Bolletta", "MBoll", "", "javascript:document.getElementById('Tab_Bollette1_btnModificaBolletta').click();")
        '    CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)

        '    'Emissione MAV B.P.S.
        '    item = New MenuItem("Emissione MAV B.P.S.", "Emav", "", "javascript:ApriMav();")
        '    CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)

        '    item = New MenuItem("Emissione MAV Intesa S.P.", "Emav2", "", "javascript:ApriModulo();")
        '    CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)

        '    If ControllaSolleciti() = 1 Then
        '        item = New MenuItem("Solleciti", "Soll", "", "javascript:ApriSolleciti();")
        '        CType(e.Item.FindControl("MenuStampe"), Menu).Items(0).ChildItems.Add(item)
        '    End If
        'End If

        'If e.Item.ItemType = ListItemType.Item Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#D6D6D6';}")

        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";")

        'End If
        'If e.Item.ItemType = ListItemType.AlternatingItem Then
        '    '---------------------------------------------------         
        '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
        '    '---------------------------------------------------         
        '    e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#FFD784';}")
        '    e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='lightsteelblue') {this.style.backgroundColor='#F7F7F7';}")
        '    e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor='';}Selezionato=this;this.style.backgroundColor='lightsteelblue';document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";")

        'End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='#FFD784';};")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='lightsteelblue';" _
                                & "document.getElementById('Tab_Bollette1_V3').value=" & e.Item.Cells(0).Text & ";")

        End If

    End Sub

    Private Function ControllaSolleciti() As Integer
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)

            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If

            'par.cmd.CommandText = "SELECT BOL_BOLLETTE_SOLLECITI.data_invio,bol_bollette.* FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_SOLLECITI WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_SOLLECITI.ID_BOLLETTA AND ID_BOLLETTA in (select id from siscom_mi.bol_bollette where id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & ") order by data_invio desc"
            par.cmd.CommandText = "SELECT BOL_BOLLETTE_SOLLECITI.data_invio, bol_bollette.*  FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.BOL_BOLLETTE_SOLLECITI,SISCOM_MI.RAPPORTI_UTENZA  WHERE BOL_BOLLETTE.ID = BOL_BOLLETTE_SOLLECITI.ID_BOLLETTA  AND BOL_BOLLETTE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.ID= " & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " ORDER BY data_invio DESC"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sollecito = 1
            Else
                sollecito = 0
            End If
            myReader.Close()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return sollecito

    End Function

    Public Property sollecito() As Integer
        Get
            If Not (ViewState("par_sollecito") Is Nothing) Then
                Return CInt(ViewState("par_sollecito"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_sollecito") = value
        End Set

    End Property

    Private Sub CaricaMenuG()
        Dim item As MenuItem

        MenuStampeG.Items(0).ChildItems.Clear()

        item = New MenuItem("Crea Nuova Scrittura", "NBoll", "", "javascript:IMG1_onclick();")
        MenuStampeG.Items(0).ChildItems.Add(item)

        item = New MenuItem("Modifica Scrittura", "MBoll", "", "javascript:document.getElementById('Tab_Bollette1_btnModificaBolletta').click();")
        MenuStampeG.Items(0).ChildItems.Add(item)

        item = New MenuItem("Annulla Scrittura", "ABoll", "", "javascript:ConfermaAnnullaGest();")
        MenuStampeG.Items(0).ChildItems.Add(item)

        item = New MenuItem("Elimina Scrittura", "EBoll", "", "javascript:ConfermaElimGest();")
        MenuStampeG.Items(0).ChildItems.Add(item)

        item = New MenuItem("Spostamento Doc. Gestionali", "SDocG", "", "javascript:ConfermaSpostamento();")
        MenuStampeG.Items(0).ChildItems.Add(item)

        If Session.Item("FL_FORZARIMBORSO") = "0" Then
            If HRimborso.Value = "1" Then
                item = New MenuItem("Rimborsa Credito", "RMCR", "", "javascript:ApriModuloRimborso();")
                MenuStampeG.Items(0).ChildItems.Add(item)
            End If
        Else
            item = New MenuItem("Rimborsa Credito", "RMCR", "", "javascript:ApriModuloRimborso();")
            MenuStampeG.Items(0).ChildItems.Add(item)
        End If

        If Session.Item("MOD_CONT_DEP") = "1" And HDeposito.Value = "1" Then
            item = New MenuItem("Rimborsa Deposito Cauzionale", "RMDC", "", "javascript:ApriModuloRimborsoDC();")
            MenuStampeG.Items(0).ChildItems.Add(item)
        End If

    End Sub

    Private Sub CaricaMenuC()
        Dim item As MenuItem

        MenuStampeC.Items(0).ChildItems.Clear()

        If Session.Item("MOD_CREAZ_BOLL") = 1 Then
            item = New MenuItem("Crea Nuova Bolletta", "NBoll", "", "javascript:IMG1_onclick();")
            MenuStampeC.Items(0).ChildItems.Add(item)

            item = New MenuItem("Modifica Bolletta", "MBoll", "", "javascript:document.getElementById('Tab_Bollette1_btnModificaBolletta').click();")
            MenuStampeC.Items(0).ChildItems.Add(item)
        End If

        ''Emissione MAV B.P.S.
        'item = New MenuItem("Emissione MAV Dep.Cauz.(S.P.)", "Emav", "", "javascript:ApriModulo('D');")
        'MenuStampeC.Items(0).ChildItems.Add(item)

        If Session.Item("MOD_CREAZ_MAVONLINE") = 1 Then
            item = New MenuItem("Emissione MAV Intesa S.P.", "Emav2", "", "javascript:ApriModulo();")
            MenuStampeC.Items(0).ChildItems.Add(item)
        End If
        If ControllaSolleciti() = 1 Then
            item = New MenuItem("Solleciti", "Soll", "", "javascript:ApriSolleciti();")
            MenuStampeC.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("Storno", "Storno", "", "javascript:document.getElementById('Tab_Bollette1_txtAppare1').value='1';StornoBolletta();")
        MenuStampeC.Items(0).ChildItems.Add(item)

        If Session.Item("MOD_SING_INGIUNZIONI") = 1 Then
            item = New MenuItem("Ingiunzione", "Ing", "", "javascript:Ingiunzione();")
            MenuStampeC.Items(0).ChildItems.Add(item)
        End If
    End Sub


    Protected Sub btnStorno_Click(sender As Object, e As System.EventArgs) Handles btnStorno.Click
        Try
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            tot_bolDaSt = 0
            Dim riclass As Integer = 0
            Dim ratMor As Integer = 0
            Dim fl_storno As Integer = 0
            Dim fl_ruolo As Integer = 0
            Dim creditiGen As Integer = 0
            If V3.Value <> "" Then

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.TIPO_BOLLETTE WHERE BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID AND BOL_BOLLETTE.ID=" & V3.Value
                Dim myReaderBol As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderBol.Read Then
                    If par.IfNull(myReaderBol("id_tipo"), -1) = 4 Or par.IfNull(myReaderBol("id_tipo"), -1) = 5 Then
                        ratMor = 1
                    End If

                    If par.IfNull(myReaderBol("id_tipo"), -1) = 25 Then
                        creditiGen = 1
                    End If

                    If par.IfNull(myReaderBol("id_bolletta_ric"), "0") <> "0" Or par.IfNull(myReaderBol("ID_RATEIZZAZIONE"), "0") <> "0" Or par.IfNull(myReaderBol("FL_ANNULLATA"), "0") <> "0" Then
                        riclass = 1
                    End If
                    If par.IfNull(myReaderBol("id_bolletta_storno"), "0") <> "0" Or par.IfNull(myReaderBol("id_tipo"), "0") = "22" Then
                        fl_storno = 1
                    End If
                    If par.IfNull(myReaderBol("importo_ruolo"), "0") <> "0" Then
                        fl_ruolo = 1
                    End If
                    lblNBoll.Text = par.IfNull(myReaderBol("NUM_BOLLETTA"), "")
                    lblTipoBoll.Text = par.IfNull(myReaderBol("DESCRIZIONE"), "")
                    If lblTipoBoll.Text.Length >= 25 Then
                        lblTipoBoll.Text = Mid(par.IfNull(myReaderBol("DESCRIZIONE"), ""), 1, 20) & "..."
                        lblTipoBoll.ToolTip = par.IfNull(myReaderBol("DESCRIZIONE"), "")
                    End If
                    lblRiferimAl.Text = par.FormattaData(par.IfNull(myReaderBol("RIFERIMENTO_A"), ""))
                    lblRiferimDal.Text = par.FormattaData(par.IfNull(myReaderBol("RIFERIMENTO_DA"), ""))
                    lblDataEmiss.Text = par.FormattaData(par.IfNull(myReaderBol("DATA_EMISSIONE"), ""))
                    lblDataScad.Text = par.FormattaData(par.IfNull(myReaderBol("DATA_SCADENZA"), ""))
                    lblImpTOT.Text = Format(par.IfNull(myReaderBol("IMPORTO_TOTALE"), 0), "##,##0.00")
                    lblImpPag.Text = Format(par.IfNull(myReaderBol("IMPORTO_PAGATO"), 0), "##,##0.00")
                    If par.IfNull(myReaderBol("IMPORTO_PAGATO"), 0) > 0 Or (par.IfNull(myReaderBol("FL_ANNULLATA"), 0) <> 0 And par.IfNull(myReaderBol("IMPORTO_PAGATO"), 0) > 0) Then
                        If par.IfNull(myReaderBol("IMPORTO_TOTALE"), 0) > par.IfNull(myReaderBol("IMPORTO_PAGATO"), 0) Then
                            lblAvvisoBoll.Visible = True
                            lblAvvisoBoll.Text = "PAGATA PARZ."
                        Else
                            lblAvvisoBoll.Visible = True
                            lblAvvisoBoll.Text = "PAGATA"
                        End If
                    Else
                        lblAvvisoBoll.Visible = False
                    End If
                    'If par.IfNull(myReaderBol("IMPORTO_PAGATO"), 0) > 0 Then
                    '    lblAvvisoBoll.Visible = True
                    'Else
                    '    lblAvvisoBoll.Visible = False
                    'End If
                    txtCompetenzaAl.Text = par.FormattaData(par.IfNull(myReaderBol("RIFERIMENTO_A"), ""))
                    txtCompetenzaDal.Text = par.FormattaData(par.IfNull(myReaderBol("RIFERIMENTO_DA"), ""))
                End If
                myReaderBol.Close()

                Dim cancVoce As Boolean = False
                Dim totVocePagata As Decimal = 0

                par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.descrizione FROM SISCOM_MI.BOL_BOLLETTE,SISCOM_MI.BOL_BOLLETTE_VOCI,SISCOM_MI.T_VOCI_BOLLETTA WHERE BOL_BOLLETTE.ID=BOL_BOLLETTE_VOCI.ID_BOLLETTA AND BOL_BOLLETTE_VOCI.ID_VOCE=T_VOCI_BOLLETTA.ID AND BOL_BOLLETTE.ID= " & V3.Value
                Dim daBVoci As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtBVoci As New Data.DataTable
                daBVoci.Fill(dtBVoci)
                daBVoci.Dispose()
                For Each row As Data.DataRow In dtBVoci.Rows
                    lstVociBoll.Items.Add(New ListItem(par.MiaFormat(row.Item("DESCRIZIONE"), 30) & " " & par.MiaFormat(Format(row.Item("IMPORTO"), "0.00"), 10) & " " & par.MiaFormat(par.IfNull(row.Item("NOTE"), ""), 30), row.Item("ID_VOCE")))
                    If par.IfNull(row.Item("ID_VOCE"), 0) < 10000 Then
                        cancVoce = False
                    Else
                        cancVoce = True
                    End If
                    totVocePagata = totVocePagata + par.IfNull(row.Item("IMP_PAGATO"), 0)
                Next


                For K = 0 To lstVociBoll.Items.Count - 1
                    tot_bolDaSt = tot_bolDaSt + par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10)
                Next

                txtTotSt.Text = Format(tot_bolDaSt, "0.00")

                tot_bolDaSt = totVocePagata

                If cancVoce = True Then
                    lstVociBoll.Items.Clear()
                    txtTotSt.Text = Format(0, "0.00")
                End If

                par.caricaComboBox("SELECT * FROM SISCOM_MI.TAB_MOTIVI_STORNO ORDER BY ID ASC", cmbMotivoStorno, "ID", "DESCRIZIONE")

                If riclass = 1 Or fl_storno = 1 Or ratMor = 1 Or creditiGen = 1 Or fl_ruolo = 1 Then
                    txtAppare1.Value = "0"
                    Response.Write("<script>alert('Non è possibile stornare la bolletta selezionata!');</script>")
                Else
                    txtAppare1.Value = "1"
                End If
            Else
                txtAppare1.Value = "0"
                Response.Write("<script>alert('Selezionare una bolletta della lista!');</script>")
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ImgCopiaVoci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgCopiaVoci.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            Dim idBolletta As Long = 0
            par.cmd.CommandText = "SELECT id FROM SISCOM_MI.BOL_BOLLETTE WHERE ID_CONTRATTO=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " AND N_RATA<>999 AND N_RATA<>99 AND FL_ANNULLATA= 0 AND ID_TIPO=1 ORDER BY RIFERIMENTO_DA DESC"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                idBolletta = par.IfNull(myReader0("ID"), 0)
            End If
            myReader0.Close()

            'par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.ID, BOL_BOLLETTE_VOCI.importo,BOL_BOLLETTE_VOCI.id_voce, t_voci_bolletta.descrizione FROM siscom_mi.t_voci_bolletta, siscom_mi.BOL_BOLLETTE_VOCI WHERE t_voci_bolletta.ID = BOL_BOLLETTE_VOCI.id_voce AND BOL_BOLLETTE_VOCI.id_BOLLETTA =" & idBolletta & " ORDER BY t_voci_bolletta.descrizione ASC"
            par.cmd.CommandText = "select bol_schema.id,bol_schema.importo,bol_schema.importo_singola_rata,bol_schema.id_voce,t_voci_bolletta.descrizione from siscom_mi.t_voci_bolletta,siscom_mi.bol_schema where bol_schema.anno=" & Year(Now) & " and t_voci_bolletta.id=bol_schema.id_voce and bol_schema.id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " order by t_voci_bolletta.descrizione asc"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader2.Read
                lstVociBoll.Items.Add(New ListItem(par.MiaFormat(par.IfNull(myReader2("descrizione"), ""), 30) & " " & par.MiaFormat(Format(par.IfNull(myReader2("importo_singola_rata"), "0,00"), "0.00"), 10) & " " & par.MiaFormat("", 30), par.IfNull(myReader2("id_voce"), "-1")))
            Loop
            myReader2.Close()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F16','Copia di tutte le voci dallo schema anno " & Year(Now) & "')"
            par.cmd.ExecuteNonQuery()

            Dim tot_bol As Decimal = 0

            For K = 0 To lstVociBoll.Items.Count - 1
                tot_bol = tot_bol + par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10)
            Next

            txtTotSt.Text = Format(tot_bol, "0.00")

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ImgEliminaVoci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgEliminaVoci.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstVociBoll.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To lstVociBoll.Items.Count - 1
                If Trim(lstVociBoll.Items(i).Text) = Trim(V2.Value) Then
                    lstVociBoll.Items.Remove(lstVociBoll.Items(i))
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                    Dim tot_bol As Decimal = 0

                    For K = 0 To lstVociBoll.Items.Count - 1
                        tot_bol = tot_bol + par.RicavaTesto(lstVociBoll.Items(K).Text, 32, 10)
                    Next

                    txtTotSt.Text = Format(tot_bol, "0.00")


                    par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    '& "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    '& "'F17','')"
                    'par.cmd.ExecuteNonQuery()

                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un Elemento della lista!');</SCRIPT>")
        End If
    End Sub

    Protected Sub ImgSalvaBoll_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalvaBoll.Click
        If confermaStorno.Value = "1" Then
            CreaStornoEnuovaBoll()
            txtTotSt.Text = ""
        End If
    End Sub

    Protected Sub ImgAnnullaBoll_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAnnullaBoll.Click
        lstVociBoll.Items.Clear()
        txtCompetenzaAl.Text = ""
        txtCompetenzaDal.Text = ""
        txtDataEmiss.Text = ""
        txtDataScad.Text = ""
        txtNoteBoll.Text = ""
        cmbEmetti.SelectedValue = "1"
        txtTotSt.Text = ""
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"

    End Sub

    Protected Sub DataGridContab_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridContab.PageIndexChanged
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "1"
        If e.NewPageIndex >= 0 Then
            DataGridContab.CurrentPageIndex = e.NewPageIndex
            'CType(Me.Page, Object).CaricaTabBollette()
            CType(Me.Page, Object).CaricaTabBollette()
        End If
    End Sub

    Protected Sub btnEliminaGest_Click(sender As Object, e As System.EventArgs) Handles btnEliminaGest.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT IMPORTO_TOTALE,DESCRIZIONE,ID_TIPO,UTILIZZABILE FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & V3.Value & " AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read = False Then
                Response.Write("<script>alert('Impossibile cancellare il documento selezionato poichè risulta elaborato!')</script>")
            Else
                If par.IfNull(myReader0("UTILIZZABILE"), 1) = 1 Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & V3.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE ID_BOLLETTA_GEST=" & V3.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F230','Tipo: " & par.IfNull(myReader0("DESCRIZIONE"), "") & " - Importo: euro " & par.VirgoleInPunti(Format(par.IfNull(myReader0("IMPORTO_TOTALE"), ""), "##,##0.00")) & "')"
                    par.cmd.ExecuteNonQuery()

                    CType(Me.Page, Object).SalvaDati()
                Else
                    Response.Write("<script>alert('Operazione non consentita sul tipo di documento selezionato!')</script>")
                End If
            End If
            myReader0.Close()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnullaGest_Click(sender As Object, e As System.EventArgs) Handles btnAnnullaGest.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT IMPORTO_TOTALE,DESCRIZIONE,ID_TIPO,UTILIZZABILE,BOL_BOLLETTE_GEST.note FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & V3.Value & " AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read = False Then
                Response.Write("<script>alert('Impossibile annullare il documento selezionato poichè risulta elaborato!')</script>")
            Else
                If par.IfNull(myReader0("UTILIZZABILE"), 1) = 1 Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.BOL_BOLLETTE_VOCI_GEST WHERE BOL_BOLLETTE_GEST.ID=" & V3.Value & " AND BOL_BOLLETTE_GEST.ID=SISCOM_MI.BOL_BOLLETTE_VOCI_GEST.ID_BOLLETTA_GEST"
                    Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt0 As New Data.DataTable
                    da0.Fill(dt0)
                    da0.Dispose()
                    If dt0.Rows.Count > 0 Then

                        For Each rowBoll As Data.DataRow In dt0.Rows
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI_GEST (ID,ID_BOLLETTA_GEST,ID_VOCE,IMPORTO) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI_GEST.NEXTVAL," & V3.Value & "," & rowBoll.Item("ID_VOCE") & "," & par.VirgoleInPunti(rowBoll.Item("IMPORTO") * (-1)) & ")"
                            par.cmd.ExecuteNonQuery()
                        Next
                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET IMPORTO_TOTALE=0.00,NOTE='" & par.IfNull(myReader0("NOTE"), "") & " (ANNULLATA)' WHERE ID=" & V3.Value
                        par.cmd.ExecuteNonQuery()
                    End If

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F231','Importo annullato pari a: euro " & par.VirgoleInPunti(Format(par.IfNull(myReader0("IMPORTO_TOTALE"), ""), "##,##0.00")) & "')"
                    par.cmd.ExecuteNonQuery()

                    CType(Me.Page, Object).SalvaDati()
                Else
                    Response.Write("<script>alert('Operazione non consentita sul tipo di documento selezionato!')</script>")
                End If
            End If
            myReader0.Close()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbNumElementi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbNumElementi.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("NumElementiVisualizzati"), HiddenField).Value = cmbNumElementi.SelectedItem.Value
        CType(Me.Page, Object).CaricaTabBollette()
    End Sub

    Protected Sub RdStornoNO_CheckedChanged(sender As Object, e As System.EventArgs) Handles RdStornoNO.CheckedChanged
        CType(Me.Page.FindControl("EscludiS"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page, Object).CaricaTabBollette()
    End Sub

    Protected Sub RdStornoSI_CheckedChanged(sender As Object, e As System.EventArgs) Handles RdStornoSI.CheckedChanged
        CType(Me.Page.FindControl("EscludiS"), HiddenField).Value = "1"
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page, Object).CaricaTabBollette()
    End Sub
 Protected Sub btnSbloccaGest_Click(sender As Object, e As System.EventArgs) Handles btnSbloccaGest.Click
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim tipoOperaz As String = ""
            par.cmd.CommandText = "SELECT IMPORTO_TOTALE,DESCRIZIONE,ID_TIPO,FL_RIPARTIBILE,BOL_BOLLETTE_GEST.note FROM SISCOM_MI.BOL_BOLLETTE_GEST,SISCOM_MI.TIPO_BOLLETTE_GEST WHERE BOL_BOLLETTE_GEST.ID=" & V3.Value & " AND BOL_BOLLETTE_GEST.ID_TIPO=TIPO_BOLLETTE_GEST.ID AND TIPO_APPLICAZIONE='N'"
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read = False Then
                Response.Write("<script>alert('Impossibile sbloccare il documento selezionato poichè risulta elaborato!')</script>")
            Else
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & V3.Value & ""
                Dim da0 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt0 As New Data.DataTable
                da0.Fill(dt0)
                da0.Dispose()
                If dt0.Rows.Count > 0 Then

                    If par.IfNull(myReader0("FL_RIPARTIBILE"), 0) = 0 Then
                        If par.IfNull(dt0.Rows(0).Item("FL_SBLOCCATA"), "2") = "2" Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_SBLOCCATA=1 WHERE ID=" & V3.Value
                            tipoOperaz = "Sbloccata"
                        Else
                            If par.IfNull(dt0.Rows(0).Item("FL_SBLOCCATA"), "2") = "1" Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_SBLOCCATA=0 WHERE ID=" & V3.Value
                                tipoOperaz = "Bloccata"
                            Else
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_SBLOCCATA=1 WHERE ID=" & V3.Value
                                tipoOperaz = "Sbloccata"
                            End If
                        End If
                    Else
                        If par.IfNull(dt0.Rows(0).Item("FL_SBLOCCATA"), "2") = "2" Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_SBLOCCATA=0 WHERE ID=" & V3.Value
                            tipoOperaz = "Bloccata"
                        Else
                            If par.IfNull(dt0.Rows(0).Item("FL_SBLOCCATA"), "2") = "1" Then
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_SBLOCCATA=0 WHERE ID=" & V3.Value
                                tipoOperaz = "Bloccata"
                            Else
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET FL_SBLOCCATA=1 WHERE ID=" & V3.Value
                                tipoOperaz = "Sbloccata"
                            End If
                        End If
                    End If
                    par.cmd.ExecuteNonQuery()
                End If

                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                '            & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                '            & "'F02','" & tipoOperaz & " scrittura gestionale pari a: euro " & par.VirgoleInPunti(Format(par.IfNull(myReader0("IMPORTO_TOTALE"), ""), "##,##0.00")) & "')"
                'par.cmd.ExecuteNonQuery()


                CType(Me.Page, Object).SalvaDati()
            End If
            myReader0.Close()

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

End Class
