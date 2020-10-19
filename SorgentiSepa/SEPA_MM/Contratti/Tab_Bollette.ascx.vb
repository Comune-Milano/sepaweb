Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Tab_Bollette
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lstBollette.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Bollette1_lstBollette');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Bollette1_V1').value=obj1.options[obj1.selectedIndex].text;document.getElementById('Tab_Bollette1_V3').value=obj1.options[obj1.selectedIndex].value;}")
        lstVociBolletta.Attributes.Add("OnClick", "javascript:obj1=document.getElementById('Tab_Bollette1_lstVociBolletta');if (obj1.selectedIndex!=-1) {document.getElementById('Tab_Bollette1_V2').value=obj1.options[obj1.selectedIndex].text;}")

        'txtEmissione.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtPeriodoAl.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtPeriodoDa.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtScadenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        If Not IsPostBack Then
            txtEmissione.Text = Format(Now, "dd/MM/yyyy")
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
                            ‘‘par.cmd.Transaction = par.myTrans

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE ID=" & lstBollette.Items(i).Value
                            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderJ.Read Then
                                If par.IfNull(myReaderJ("id_rateizzazione"), "0") = "0" And par.IfNull(myReaderJ("ID_TIPO"), "0") <> "4" And par.IfNull(myReaderJ("ID_TIPO"), "0") <> "5" And par.IfNull(myReaderJ("FL_ANNULLATA"), "0") = "0" And par.IfNull(myReaderJ("data_pagamento"), "") = "" And par.IfNull(myReaderJ("id_bolletta_ric"), "0") = "0" Then
                                    'VERIFICARE ANNULLO DEL MAV ON LINE
                                    If par.IfNull(myReaderJ("RIF_FILE"), "") = "" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "MAV" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "MOD" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "FIN" Or par.IfNull(myReaderJ("RIF_FILE"), "") = "REC" Then
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='1',DATA_ANNULLO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & lstBollette.Items(i).Value
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='1',DATA_ANNULLO='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & lstBollette.Items(i).Value
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

    Protected Sub img_InserisciBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciBolletta.Click
        Dim I As Integer = 0

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

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


            If txtPeriodoDa.Text <> "" And txtPeriodoAl.Text <> "" And txtScadenza.Text <> "" And IsDate(txtPeriodoDa.Text) = True And IsDate(txtPeriodoAl.Text) = True Then
                If Mid(par.AggiustaData(txtPeriodoDa.Text), 1, 6) = Mid(par.AggiustaData(txtPeriodoAl.Text), 1, 6) Then

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

                    If tot_bol > 0 Then

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
                                                & "'1', " & myReaderS("ID_COMPLESSO") & ", '', NULL, '', " _
                                                & Year(Now) & ", '', " & myReaderS("ID_EDIFICIO") & ", NULL, NULL,'MOD',1)"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_BOLLETTE.CURRVAL FROM DUAL"
                            Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderA.Read Then
                                Dim ID_BOLLETTA As Long = myReaderA(0)
                                TOTALE_BOLLETTA = 0

                                For I = 0 To lstVociBolletta.Items.Count - 1
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & ID_BOLLETTA & "," & lstVociBolletta.Items(I).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10)) & ",'" & par.PulisciStrSql(par.RicavaTesto(lstVociBolletta.Items(I).Text, 43, 30)) & "')"
                                    par.cmd.ExecuteNonQuery()
                                    TOTALE_BOLLETTA = TOTALE_BOLLETTA + CDbl(par.RicavaTesto(lstVociBolletta.Items(I).Text, 32, 10))
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


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                            & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F08','')"
                            par.cmd.ExecuteNonQuery()


                            lstBollette.Items.Clear()
                            'CARICAMENTO bollette
                            Dim num_bolletta As String = ""
                            Dim importobolletta As Double = 0
                            I = 0


                            par.cmd.CommandText = "select TIPO_BOLLETTE.ACRONIMO,bol_bollette.* from SISCOM_MI.TIPO_BOLLETTE,siscom_mi.bol_bollette where BOL_BOLLETTE.ID_TIPO=TIPO_BOLLETTE.ID (+) AND  bol_bollette.id_unita=" & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value & " and bol_bollette.id_contratto=" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & " order by bol_bollette.data_emissione desc,BOL_BOLLETTE.ANNO DESC,BOL_BOLLETTE.N_RATA DESC,bol_bollette.id desc"
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
                        End If
                        myReaderS.Close()
                        lstVociBolletta.Items.Clear()

                        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"
                    Else
                        txtAppare.Value = "1"
                        Response.Write("<script>alert('Attenzione... La bolletta non può avere un importo totale inferiore o uguale a 0, considerando anche l\'eventuale importo del bollo e delle spese mav!');</script>")
                    End If

                Else
                    txtAppare.Value = "1"
                    Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
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


        'txtImportoVoce.Text = ""
        'txtPerRate.Text = ""
        'txtDaRata.Text = ""
    End Sub

    Protected Sub btnInserisciVoce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnInserisciVoce.Click
        lstVociBolletta.Items.Add(New ListItem(par.MiaFormat(cmbVoceSchema.SelectedItem.Text, 30) & " " & par.MiaFormat(txtImportoVoce.Text, 10) & " " & par.MiaFormat(txtnotevoce.Text, 30), cmbVoceSchema.SelectedItem.Value))

        par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
        & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
        & "'F16','')"
        par.cmd.ExecuteNonQuery()

    End Sub

    Protected Sub img_ChiudiBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_ChiudiBolletta.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        imgSalva.Visible = False
        img_InserisciBolletta.Visible = True
        lstVociBolletta.Items.Clear()
    End Sub

    Protected Sub btnModificaBolletta_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnModificaBolletta.Click
        Try

            Dim MiaVar1 As String = ""
            Dim MiaVar2 As String = ""

            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            If lstBollette.SelectedIndex >= 0 Then
                Dim i As Integer
                For i = 0 To lstBollette.Items.Count - 1
                    MiaVar1 = Replace(Replace(Trim(lstBollette.Items(i).Text), Chr(160), ""), " ", "")
                    MiaVar2 = Replace(Replace(Trim(V1.Value), Chr(160), ""), " ", "")

                    If Replace(Replace(MiaVar1, Chr(13), ""), Chr(10), "") = Replace(Replace(MiaVar2, Chr(13), ""), Chr(10), "") Then
                        Try
                            lstVociBolletta.Items.Clear()
                            img_InserisciBolletta.Visible = False
                            imgSalva.Visible = True
                            txtAppare.Value = "1"
                            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)
                            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                            ‘‘par.cmd.Transaction = par.myTrans
                            IdBolletta.Value = lstBollette.Items(i).Value

                            par.cmd.CommandText = "SELECT BOL_BOLLETTE.* from siscom_mi.bol_bollette WHERE BOL_BOLLETTE.ID=" & lstBollette.Items(i).Value
                            Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderJ.Read Then
                                If (par.IfNull(myReaderJ("rif_file"), "") = "FIN" Or (par.IfNull(myReaderJ("n_rata"), "99") = "99" And par.IfNull(myReaderJ("data_pagamento"), "") = "" And par.IfNull(myReaderJ("rif_file"), "") = "MOD" And par.IfNull(myReaderJ("rif_file_txt"), "") = "")) Then
                                    If par.IfNull(myReaderJ("FL_ANNULLATA"), "") = "0" And par.IfNull(myReaderJ("rif_bollettino"), "") = "" And par.IfNull(myReaderJ("ID_BOLLETTA_RIC"), "") = "" Then
                                        txtPeriodoDa.Text = par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_DA"), ""))
                                        txtPeriodoAl.Text = par.FormattaData(par.IfNull(myReaderJ("RIFERIMENTO_A"), ""))
                                        txtEmissione.Text = par.FormattaData(par.IfNull(myReaderJ("DATA_EMISSIONE"), ""))
                                        txtScadenza.Text = par.FormattaData(par.IfNull(myReaderJ("DATA_SCADENZA"), ""))
                                        txtNote.Text = par.IfNull(myReaderJ("NOTE"), "")

                                        myReaderJ.Close()

                                        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*,T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE T_VOCI_BOLLETTA.ID=BOL_BOLLETTE_VOCI.ID_VOCE AND BOL_BOLLETTE_VOCI.ID_BOLLETTA=" & lstBollette.Items(i).Value & " ORDER BY BOL_BOLLETTE_VOCI.ID ASC"
                                        myReaderJ = par.cmd.ExecuteReader()
                                        Do While myReaderJ.Read
                                            lstVociBolletta.Items.Add(New ListItem(par.MiaFormat(myReaderJ("DESCRIZIONE"), 30) & " " & par.MiaFormat(Format(myReaderJ("IMPORTO"), "0.00"), 10) & " " & par.MiaFormat(par.IfNull(myReaderJ("NOTE"), ""), 30), myReaderJ("ID_VOCE")))
                                        Loop
                                        myReaderJ.Close()
                                    Else
                                        myReaderJ.Close()
                                        Response.Write("<script>alert('Non è possibile procedere...Bolletta annullata, mav già emesso o bolletta riclassificata!');</script>")
                                        imgSalva.Visible = False
                                        img_InserisciBolletta.Visible = True

                                        txtAppare.Value = "0"


                                    End If
                                Else
                                    myReaderJ.Close()
                                    Response.Write("<script>alert('Bolletta Non Modificabile!');</script>")
                                    imgSalva.Visible = False
                                    img_InserisciBolletta.Visible = True
                                    txtAppare.Value = "0"

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
                    End If
                Next
            Else
                Response.Write("<SCRIPT>alert('Selezionare una bolletta della lista!');</SCRIPT>")
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Function SalvaBolletta()
        Dim K As Integer = 0
        Dim I As Integer = 0

        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans



            If Mid(par.AggiustaData(txtPeriodoDa.Text), 1, 6) = Mid(par.AggiustaData(txtPeriodoAl.Text), 1, 6) Then

                Dim tot_bol As Double = 0

                For K = 0 To lstVociBolletta.Items.Count - 1
                    tot_bol = tot_bol + par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10))
                Next

                Dim Tipologia As String = ""
                par.cmd.CommandText = "select id_tipo from siscom_mi.bol_bollette where id=" & IdBolletta.Value
                Dim myReaderS11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderS11.Read Then
                    If par.IfNull(myReaderS11(0), "02") = "3" Then
                        Tipologia = "FIN"
                    End If
                End If
                myReaderS11.Close()


                'If tot_bol > 0 Or Tipologia = "FIN" Then

                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET " _
                                    & "DATA_EMISSIONE='" & par.AggiustaData(txtEmissione.Text) _
                                    & "', DATA_SCADENZA='" & par.AggiustaData(txtScadenza.Text) _
                                    & "', NOTE='" & par.PulisciStrSql(txtNote.Text) _
                                    & "', RIFERIMENTO_DA='" & par.AggiustaData(txtPeriodoDa.Text) _
                                    & "', RIFERIMENTO_A='" & par.AggiustaData(txtPeriodoAl.Text) _
                                    & "' WHERE ID=" & IdBolletta.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.BOL_BOLLETTE_VOCI WHERE ID_BOLLETTA=" & IdBolletta.Value
                par.cmd.ExecuteNonQuery()

                For K = 0 To lstVociBolletta.Items.Count - 1
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.BOL_BOLLETTE_VOCI (ID,ID_BOLLETTA,ID_VOCE,IMPORTO,NOTE) VALUES " _
                                        & "(SISCOM_MI.SEQ_BOL_BOLLETTE_VOCI.NEXTVAL," & IdBolletta.Value & "," & lstVociBolletta.Items(K).Value & "," & par.VirgoleInPunti(par.RicavaTesto(lstVociBolletta.Items(K).Text, 32, 10)) & ",'" & par.PulisciStrSql(par.RicavaTesto(lstVociBolletta.Items(K).Text, 43, 30)) & "')"
                    par.cmd.ExecuteNonQuery()
                Next

                'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                '& "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                '& "'F10','')"
                'par.cmd.ExecuteNonQuery()

                imgSalva.Visible = False
                img_InserisciBolletta.Visible = True

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
            Else
                txtAppare.Value = "1"
                Response.Write("<script>alert('Attenzione... Il periodo di riferimento deve essere compreso nello stesso anno e mese.\nSe il periodo è composto da più mesi, inserire più bollette.');</script>")
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
    End Function

    Protected Sub imgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSalva.Click
        SalvaBolletta()
    End Sub

    Public Function SVUOTA()
        lstVociBolletta.Items.Clear()
    End Function


    Protected Sub img_EliminaVoce_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles img_EliminaVoce.Click
        CType(Me.Page.FindControl("USCITA"), HiddenField).Value = "0"
        If lstVociBolletta.SelectedIndex >= 0 Then

            Dim i As Integer
            For i = 0 To lstVociBolletta.Items.Count - 1
                If Trim(lstVociBolletta.Items(i).Text) = Trim(V2.Value) Then
                    lstVociBolletta.Items.Remove(lstVociBolletta.Items(i))
                    CType(Me.Page.FindControl("txtmodificato"), HiddenField).Value = "1"

                    par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans


                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                    & "VALUES (" & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                    & "'F17','')"
                    par.cmd.ExecuteNonQuery()

                    Exit Sub
                End If
            Next
        Else
            Response.Write("<SCRIPT>alert('Selezionare un Elemento della lista!');</SCRIPT>")
        End If

    End Sub


    Public Sub DisattivaTutto()
        lstBollette.Enabled = True
        imgSalva.Visible = False
        ImgAnteprima.Visible = True
        ImgMavOnLine.Visible = False
    End Sub

    Public Sub DisattivaTuttoVirtuale()
        lstBollette.Enabled = True

        imgSalva.Visible = False

        ImgAnteprima.Visible = True

        ImgMavOnLine.Visible = False


    End Sub

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

        par.OracleConn = CType(HttpContext.Current.Session.Item(txtConnessione.Value), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Value), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

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




    End Sub


End Class
