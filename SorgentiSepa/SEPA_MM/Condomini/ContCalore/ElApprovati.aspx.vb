Imports ExpertPdf.HtmlToPdf
Imports System.IO

Partial Class Condomini_ContCalore_ElApprovati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property dataTableElApprovati() As Data.DataTable
        Get
            If Not (ViewState("dataTableElApprovati") Is Nothing) Then
                Return ViewState("dataTableElApprovati")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dataTableElApprovati") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            FindContCalore(Request.QueryString("TIPO"), Request.QueryString("IDCONTRIBUTO"))
        End If
    End Sub
    Private Sub FindContCalore(ByVal tipoCalcolo As String, ByVal idContCalore As String)
        Try
            Dim stato As Integer = Request.QueryString("STATO")
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            If tipoCalcolo = "NUOVO" Then
                tipoCalcolo = 2
                If Request.QueryString("STATO") = 1 Then
                    Me.lblTitolo.Text = "ELENCO DEGLI AVENTI DIRITTO A PREVENTIVO PER IL CONTRIBUTO CALORE " & Request.QueryString("ANNO")
                Else
                    Me.lblTitolo.Text = "ELENCO DEGLI NON AVENTI DIRITTO A PREVENTIVO PER IL CONTRIBUTO CALORE " & Request.QueryString("ANNO")
                End If
            ElseIf tipoCalcolo = "CONGUAGLIO" Then
                tipoCalcolo = 4
                If Request.QueryString("STATO") = 1 Then
                    Me.lblTitolo.Text = "ELENCO DEGLI AVENTI DIRITTO A CONSUNTIVO PER IL CONTRIBUTO CALORE " & Request.QueryString("ANNO")
                Else
                    Me.lblTitolo.Text = "ELENCO DEGLI NON AVENTI DIRITTO A CONSUNTIVO PER IL CONTRIBUTO CALORE " & Request.QueryString("ANNO")
                End If
            End If
            If idContCalore = 0 Then
                Response.Write("<script>alert('ERRORE IN FASE DI ESTRAZIONE DEI DATI!');self.close();</script>")
                Exit Sub
            End If
            If tipoCalcolo = 4 Or (tipoCalcolo = 2 And stato = 1) Then
                par.cmd.CommandText = "SELECT id_cont_calore,cont_calore_elaborazione.id_contratto, condomini.ID AS id_condominio,condomini.denominazione AS condominio, " _
                                    & "cont_calore_elaborazione.id_anagrafica, " _
                                    & "cont_calore_elaborazione.id_unita, rapporti_utenza.cod_contratto, " _
                                    & "tipo_calcolo_cont_calore.descrizione AS tipo_calcolo, " _
                                    & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale " _
                                    & "ELSE RTRIM (LTRIM (cognome || ' ' || nome))END ) AS nominativo, " _
                                    & "cont_calore_elaborazione.importo_riconosciuto " _
                                    & "FROM siscom_mi.cont_calore_elaborazione, " _
                                    & "siscom_mi.rapporti_utenza, " _
                                    & "siscom_mi.tipo_calcolo_cont_calore, " _
                                    & "siscom_mi.cond_ui,siscom_mi.condomini, " _
                                    & "siscom_mi.cont_calore_anno, " _
                                    & "siscom_mi.anagrafica, " _
                                    & "siscom_mi.soggetti_contrattuali " _
                                    & "WHERE id_cont_calore = " & idContCalore & " AND tipo_calcolo = " & tipoCalcolo & " " _
                                    & "AND rapporti_utenza.ID = cont_calore_elaborazione.id_contratto " _
                                    & "AND tipo_calcolo_cont_calore.ID = cont_calore_elaborazione.tipo_calcolo " _
                                    & "AND cond_ui.id_ui = id_unita AND id_intestario = anagrafica.ID " _
                                    & "AND cond_ui.id_condominio = condomini.ID " _
                                    & "AND cont_calore_anno.ID = cont_calore_elaborazione.id_cont_calore " _
                                    & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
                                    & "AND rapporti_utenza.ID = soggetti_contrattuali.id_contratto " _
                                    & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                                    & "AND cont_calore_elaborazione.stato = " & stato & " " _
                                    & "and cont_calore_elaborazione.id_condominio = condomini.id " _
                                    & "AND CONDOMINI.TIPO_GESTIONE = 'D' "
                If Request.QueryString("COND") <> "-1" Then
                    par.cmd.CommandText += " AND CONDOMINI.ID = " & Request.QueryString("COND") & " "
                End If
                par.cmd.CommandText += "ORDER BY condominio ASC,nominativo ASC "
            Else
                par.cmd.CommandText = "SELECT DISTINCT id_cont_calore,cont_calore_elaborazione.id_contratto, " _
                                    & "cont_calore_elaborazione.id_anagrafica, " _
                                    & "cont_calore_elaborazione.id_unita, rapporti_utenza.cod_contratto, " _
                                    & "tipo_calcolo_cont_calore.descrizione AS tipo_calcolo, " _
                                    & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale " _
                                    & "ELSE RTRIM (LTRIM (cognome || ' ' || nome))END ) AS nominativo, " _
                                    & "cont_calore_elaborazione.importo_riconosciuto " _
                                    & "FROM siscom_mi.cont_calore_elaborazione, " _
                                    & "siscom_mi.rapporti_utenza, " _
                                    & "siscom_mi.tipo_calcolo_cont_calore, " _
                                    & "siscom_mi.cond_ui, " _
                                    & "siscom_mi.cont_calore_anno, " _
                                    & "siscom_mi.anagrafica, " _
                                    & "siscom_mi.soggetti_contrattuali " _
                                    & "WHERE id_cont_calore = " & idContCalore & " AND tipo_calcolo = " & tipoCalcolo & " " _
                                    & "AND rapporti_utenza.ID = cont_calore_elaborazione.id_contratto " _
                                    & "AND tipo_calcolo_cont_calore.ID = cont_calore_elaborazione.tipo_calcolo " _
                                    & "AND cond_ui.id_ui = id_unita AND id_intestario = anagrafica.ID " _
                                    & "AND cont_calore_anno.ID = cont_calore_elaborazione.id_cont_calore " _
                                    & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
                                    & "AND rapporti_utenza.ID = soggetti_contrattuali.id_contratto " _
                                    & "AND soggetti_contrattuali.cod_tipologia_occupante = 'INTE' " _
                                    & "AND cont_calore_elaborazione.stato = " & stato & " "
                par.cmd.CommandText += "ORDER BY nominativo ASC "
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dataTableElApprovati = New Data.DataTable
            da.Fill(dataTableElApprovati)
            da.Dispose()
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If
            If dataTableElApprovati.Rows.Count > 0 Then
                If tipoCalcolo = 4 Or (tipoCalcolo = 2 And stato = 1) Then
                    CaricaPerCondominio(dataTableElApprovati)
                Else
                    Carica(dataTableElApprovati)
                End If
            Else
                Response.Write("<script>alert('Nessun avente diritto approvato per questo contributo calore');self.close();</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                ''*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearPool(par.OracleConn)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>Approva" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';self.close();</script>")
        End Try
    End Sub
    Private Sub CaricaPerCondominio(ByVal dt As Data.DataTable)
        Dim id_condominio As Integer = 0
        Dim condominio As String = ""
        Dim tableIntesta As String = "<table style='width:100%;'>"
        Dim TableCondominio As String = "<table style='width:100%;'><tr><td>COD.CONTRATTO</td><td>NOMINATIVO</td><td>IMPORTO</td>"
        Dim primo As Boolean = True
        For Each row As Data.DataRow In dt.Rows
            If id_condominio <> CInt(row.Item("id_condominio")) Then
                id_condominio = row.Item("id_condominio")
                If primo = True Then
                    primo = False
                    tableIntesta = tableIntesta & "<tr><td style='font-family: Arial; font-size: 10pt; font-weight: 700; color: #0033CC; text-align: left'>CONDOMINIO " & row.Item("condominio") & "</td></tr><tr><td>"
                    TableCondominio = "<table cellpadding=""1"" cellspacing=""1"" border=""1"" frame=""border"" style='width:100%;'><tr><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='30%'>COD.CONTRATTO</td><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='40%'>NOMINATIVO</td><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='30%'>IMPORTO</td>"
                Else
                    TableCondominio = TableCondominio & "</table>"
                    tableIntesta = tableIntesta & TableCondominio
                    tableIntesta = tableIntesta & "</td></tr><tr><td>&nbsp;</td></tr><tr><td style='font-family: Arial; font-size: 10pt; font-weight: 700; color: #0033CC; text-align: left'>CONDOMINIO " & row.Item("condominio") & "</td></tr><tr><td>"
                    TableCondominio = "<table cellpadding=""1"" cellspacing=""1"" border=""1"" frame=""border"" style='width:100%;'><tr><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='30%'>COD.CONTRATTO</td><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='40%'>NOMINATIVO</td><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='30%'>IMPORTO</td>"
                End If
            End If
            TableCondominio = TableCondominio & "<tr><td style='font-family: Arial; font-size: 8pt; text-align: left' width='30%'>" & row.Item("COD_CONTRATTO") & "</td><td style='font-family: Arial; font-size: 8pt; text-align: left' width='40%'>" & row.Item("NOMINATIVO") & "</td><td style='font-family: Arial; font-size: 8pt; text-align: right' width='30%'>" & row.Item("IMPORTO_RICONOSCIUTO") & "</td></tr>"
        Next
        TableCondominio = TableCondominio & "</table>"
        tableIntesta = tableIntesta & TableCondominio
        Me.lblRisultati.Text = tableIntesta
    End Sub
    Private Sub Carica(ByVal dt As Data.DataTable)
        Dim tableIntesta As String = "<table style='width:100%;'>"
        Dim TableCondominio As String = "<table cellpadding=""1"" cellspacing=""1"" border=""1"" frame=""border"" style='width:100%;'><tr><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='30%'>COD.CONTRATTO</td><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='40%'>NOMINATIVO</td><td style='font-family: Arial; font-size: 8pt; font-weight: 700; text-align: center' width='30%'>IMPORTO</td></tr>"
        Dim conta As Integer = 0
        For Each row As Data.DataRow In dt.Rows
            TableCondominio = TableCondominio & "<tr><td style='font-family: Arial; font-size: 8pt; text-align: left' width='30%'>" & row.Item("COD_CONTRATTO") & "</td><td style='font-family: Arial; font-size: 8pt; text-align: left' width='40%'>" & row.Item("NOMINATIVO") & "</td><td style='font-family: Arial; font-size: 8pt; text-align: right' width='30%'>" & row.Item("IMPORTO_RICONOSCIUTO") & "</td></tr>"
            conta = conta + 1
        Next
        TableCondominio = TableCondominio & "</table>"
        tableIntesta = tableIntesta & TableCondominio
        Me.lblRisultati.Text = tableIntesta
    End Sub
    Protected Sub btnStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Try
            Dim url As String = Server.MapPath("..\..\FileTemp\")
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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 20
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            Dim Titolo As String = ""
            If Request.QueryString("STATO") = 1 Then
                Titolo = "StampaElApprovatiContCalore_"
            Else
                Titolo = "StampaElNonApprovatiContCalore_"
            End If
            Dim nomefile As String = Titolo & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(lblTitolo.Text & lblRisultati.Text, url & nomefile, Server.MapPath("..\..\NuoveImm\"))
            Response.Write("<script>window.open('../../FileTemp/" & nomefile & "','ElApprovati','');</script>")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>Approva" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            Dim Titolo As String = ""
            If Request.QueryString("STATO") = 1 Then
                Titolo = "ExportElApprovatiContCalore"
            Else
                Titolo = "ExportElNonApprovatiContCalore"
            End If
            If dataTableElApprovati.Rows.Count > 0 Then
                Dim nomefile As String = EsportaExcelDaDT(dataTableElApprovati, Titolo, True)
                If File.Exists(Server.MapPath("..\..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun risultato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & "<br/>Approva" & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
    Function EsportaExcelDaDT(ByVal dt As Data.DataTable, ByVal nomeFile As String, Optional ByVal EliminazioneLink As Boolean = True) As String
        Try
            Dim NumeroColonneDT As Integer = dt.Columns.Count
            Dim FileExcel As New CM.ExcelFile
            Dim indiceRighe As Long = 0
            Dim IndiceColonne As Integer = 1
            Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
            Dim Condominio As String = ""
            Dim primorigo As Boolean = True
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetColumnWidth(1, 1, 25)
                .SetColumnWidth(2, 2, 50)
                .SetColumnWidth(3, 3, 15)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                For Each riga As Data.DataRow In dt.Rows
                    indiceRighe = indiceRighe + 1
                    Dim Cella As Integer = 0
                    If Request.QueryString("TIPO") = "CONGUAGLIO" Or (Request.QueryString("TIPO") = "NUOVO" And Request.QueryString("STATO") = 1) Then
                        For IndiceColonne = 0 To NumeroColonneDT - 1
                            If Replace(par.IfNull(riga.Item(3), 0), "&nbsp;", "") = Condominio Then
                                Select Case IndiceColonne
                                    Case 6, 8, 9
                                        Select Case EliminazioneLink
                                            Case False
                                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 4)
                                                Else
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 0)
                                                End If
                                            Case True
                                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                                Else
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                                End If
                                            Case Else
                                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                                Else
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                                End If
                                        End Select
                                        Cella = Cella + 1
                                    Case Else

                                End Select
                            Else
                                If primorigo = True Then
                                    primorigo = False
                                Else
                                    indiceRighe = indiceRighe + 1
                                End If
                                Condominio = Replace(par.IfNull(riga.Item(3), 0), "&nbsp;", "")
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont2, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "CONDOMINIO " & Condominio, 0)
                                indiceRighe = indiceRighe + 1
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "CODICE CONTRATTO", 0)
                                Cella = Cella + 1
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "NOMINATIVO", 0)
                                Cella = Cella + 1
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "IMPORTO", 0)
                                Cella = Cella + 1
                                indiceRighe = indiceRighe + 1
                                Cella = 0
                                Select Case IndiceColonne
                                    Case 6, 8, 9
                                        Select Case EliminazioneLink
                                            Case False
                                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 4)
                                                Else
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 0)
                                                End If
                                            Case True
                                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                                Else
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                                End If
                                            Case Else
                                                If IsNumeric(riga.Item(IndiceColonne)) Then
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                                Else
                                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                                End If
                                        End Select
                                        Cella = Cella + 1
                                    Case Else

                                End Select
                            End If
                        Next
                    Else
                        For IndiceColonne = 0 To NumeroColonneDT - 1
                            If primorigo = True Then
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "CODICE CONTRATTO", 0)
                                Cella = Cella + 1
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "NOMINATIVO", 0)
                                Cella = Cella + 1
                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, "IMPORTO", 0)
                                indiceRighe = indiceRighe + 1
                                Cella = 0
                                primorigo = False
                            End If
                            Select Case IndiceColonne
                                Case 4, 6, 7
                                    Select Case EliminazioneLink
                                        Case False
                                            If IsNumeric(riga.Item(IndiceColonne)) Then
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 4)
                                            Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", ""), 0)
                                            End If
                                        Case True
                                            If IsNumeric(riga.Item(IndiceColonne)) Then
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                            Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                            End If
                                        Case Else
                                            If IsNumeric(riga.Item(IndiceColonne)) Then
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 4)
                                            Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(riga.Item(IndiceColonne), 0), "&nbsp;", "")), 0)
                                            End If
                                    End Select
                                    Cella = Cella + 1
                                Case Else

                            End Select
                        Next
                    End If
                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            Dim FileNameXls As String = FileName & ".xls"
            Return FileNameXls
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Class
