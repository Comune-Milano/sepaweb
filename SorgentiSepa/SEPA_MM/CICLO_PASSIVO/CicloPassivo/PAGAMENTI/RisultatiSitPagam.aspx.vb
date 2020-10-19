Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
'Imports System.Drawing

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSitPag
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt2 As New Data.DataTable
    Dim RIGA As System.Data.DataRow
    Dim TestoPagina As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:350px; left:450px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            lbltitolo.Text = "Situazione pagamenti"
            Pagamenti()
        End If

    End Sub

    Private Sub Pagamenti()

        Dim s As String
        Dim i As Integer = 0
        Dim conta As Integer = 0
        Dim totImpoPrenot As Double
        Dim totImpoApprov As Double
        Dim totComplessPrenot As Double
        Dim totComplessApprov As Double
        Dim dt1 As New Data.DataTable

        dt2.Columns.Add("CODICE")
        dt2.Columns.Add("VOCE")
        dt2.Columns.Add("NUM_ADP")
        dt2.Columns.Add("ANNO")
        dt2.Columns.Add("IMPORTO_PRENOTATO")
        dt2.Columns.Add("IMPORTO_APPROVATO")
        dt2.Columns.Add("FORNITORE")
        dt2.Columns.Add("REPERTORIO")
        dt2.Columns.Add("STATO_PAGAMENTO")
        dt2.Columns.Add("NOME")
        dt2.Columns.Add("ID_PRENOTAZIONE")
        dt2.Columns.Add("ID_PAGAMENTO")

        If Request.QueryString("Str") <> "-1" Then
            s = " AND PRENOTAZIONI.ID_STRUTTURA = " & Request.QueryString("Str") & ""
        Else
            s = " "
        End If

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT SISCOM_MI.PF_VOCI.CODICE,SISCOM_MI.PF_VOCI.DESCRIZIONE AS VOCE,SISCOM_MI.PAGAMENTI.progr AS NUM_ADP,SISCOM_MI.PAGAMENTI.anno,TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO,'9G999G990D99')) AS IMPORTO_PRENOTATO,TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_APPROVATO,'9G999G990D99')) AS IMPORTO_APPROVATO,SISCOM_MI.FORNITORI.RAGIONE_SOCIALE AS FORNITORE,SISCOM_MI.APPALTI.NUM_REPERTORIO AS REPERTORIO,SISCOM_MI.Getstatopagamento(PAGAMENTI.ID) AS STATO_PAGAMENTO,SISCOM_MI.TAB_FILIALI.NOME,SISCOM_MI.PRENOTAZIONI.ID AS ID_PRENOTAZIONE,SISCOM_MI.PAGAMENTI.ID AS ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.TAB_FILIALI WHERE SISCOM_MI.PRENOTAZIONI.id_pagamento = SISCOM_MI.PAGAMENTI.ID(+) AND SISCOM_MI.PRENOTAZIONI.id_fornitore=SISCOM_MI.FORNITORI.ID AND SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF=SISCOM_MI.PF_VOCI.ID AND SISCOM_MI.PRENOTAZIONI.id_appalto=SISCOM_MI.APPALTI.ID(+) AND SISCOM_MI.TAB_FILIALI.ID=SISCOM_MI.PRENOTAZIONI.id_struttura " & s & " AND PF_VOCI.ID_PIANO_FINANZIARIO = " & Request.QueryString("PF") & " ORDER BY 1,2 ASC"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt1)

            For i = 0 To dt1.Rows.Count - 1

                RIGA = dt2.NewRow()
                RIGA.Item("CODICE") = par.IfNull(dt1.Rows(i).Item(0), "")
                RIGA.Item("VOCE") = par.IfNull(dt1.Rows(i).Item(1), "").ToString.ToUpper
                RIGA.Item("NUM_ADP") = par.IfNull(dt1.Rows(i).Item(2), "")
                RIGA.Item("ANNO") = par.IfNull(dt1.Rows(i).Item(3), "")
                RIGA.Item("IMPORTO_PRENOTATO") = par.IfNull(dt1.Rows(i).Item(4), "0")
                RIGA.Item("IMPORTO_APPROVATO") = par.IfNull(dt1.Rows(i).Item(5), "0")
                RIGA.Item("FORNITORE") = par.IfNull(dt1.Rows(i).Item(6), "")
                RIGA.Item("REPERTORIO") = par.IfNull(dt1.Rows(i).Item(7), "")
                RIGA.Item("STATO_PAGAMENTO") = par.IfNull(dt1.Rows(i).Item(8), "")
                RIGA.Item("NOME") = par.IfNull(dt1.Rows(i).Item(9), "")
                dt2.Rows.Add(RIGA)

                totImpoPrenot = totImpoPrenot + par.IfNull(dt1.Rows(i).Item("IMPORTO_PRENOTATO"), "0")
                totImpoApprov = totImpoApprov + par.IfNull(dt1.Rows(i).Item("IMPORTO_APPROVATO"), "0")
                totComplessPrenot = totComplessPrenot + par.IfNull(dt1.Rows(i).Item("IMPORTO_PRENOTATO"), "0")
                totComplessApprov = totComplessApprov + par.IfNull(dt1.Rows(i).Item("IMPORTO_APPROVATO"), "0")

                If i < dt1.Rows.Count - 1 Then
                    If dt1.Rows(i).Item("CODICE") <> dt1.Rows(i + 1).Item("CODICE") Then
                        RIGA = dt2.NewRow()
                        RIGA.Item("CODICE") = "TOTALE VOCE"
                        RIGA.Item("IMPORTO_PRENOTATO") = Format(totImpoPrenot, "##,##0.00")
                        RIGA.Item("IMPORTO_APPROVATO") = Format(totImpoApprov, "##,##0.00")
                        dt2.Rows.Add(RIGA)
                        totImpoPrenot = 0
                        totImpoApprov = 0
                    End If
                Else
                    RIGA = dt2.NewRow()
                    RIGA.Item("CODICE") = "TOTALE VOCE"
                    RIGA.Item("IMPORTO_PRENOTATO") = Format(totImpoPrenot, "##,##0.00")
                    RIGA.Item("IMPORTO_APPROVATO") = Format(totImpoApprov, "##,##0.00")
                    dt2.Rows.Add(RIGA)
                    totImpoPrenot = 0
                    totImpoApprov = 0
                End If

            Next

            Session.Add("MIADT", dt2)
            DataGrid1.DataSource = dt2
            DataGrid1.DataBind()

            If Request.QueryString("Str") <> "-1" Then
                DataGrid1.Columns(9).Visible = False
            End If

            conta = dt1.Rows.Count

            If conta > 0 Then
                If Request.QueryString("Str") <> "-1" Then
                    lblTot.Text = "Risultano <b>" & Format(conta, "##,##") & "</b> pagamenti riferiti alla struttura <b><i>" & Request.QueryString("NomeStr").ToLower & "</b></i>. Gli importi <i>prenotati</i> e <i>approvati</i> <u>complessivi</u> ammontano rispettivamente a: <b>" & Format(totComplessPrenot, "##,##0.00") & "</b> euro e <b>" & Format(totComplessApprov, "##,##0.00") & "</b> euro."
                Else
                    lblTot.Text = "Risultano <b>" & Format(conta, "##,##") & "</b> pagamenti riferiti a <b>tutte le strutture</b>. Gli importi <i>prenotati</i> e <i>approvati</i> <u>complessivi</u> ammontano rispettivamente a: <b>" & Format(totComplessPrenot, "##,##0.00") & "</b> euro e <b>" & Format(totComplessApprov, "##,##0.00") & "</b> euro."
                End If
            Else
                lblTot.Text = "Non risulta nessun pagamento per la struttura <b><i>" & Request.QueryString("NomeStr").ToLower & "</b></i>"
                DataGrid1.Visible = False
            End If

            For Each di As DataGridItem In DataGrid1.Items
                If di.Cells(0).Text.Contains("TOTALE") Then
                    di.ForeColor = Drawing.Color.Red
                    di.BackColor = Drawing.Color.Lavender
                    For j As Integer = 0 To di.Cells.Count - 1
                        di.Cells(j).Font.Bold = True
                    Next
                End If
            Next

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt2 = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "sit_pagamenti" & Format(Now, "yyyyMMddHHmm")
            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                '.SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
                .SetColumnWidth(1, 1, 20)
                .SetColumnWidth(2, 2, 55)
                .SetColumnWidth(3, 3, 12)
                .SetColumnWidth(4, 4, 12)
                .SetColumnWidth(5, 5, 25)
                .SetColumnWidth(6, 6, 25)
                .SetColumnWidth(7, 7, 50)
                .SetColumnWidth(8, 8, 16)
                .SetColumnWidth(9, 9, 20)
                .SetColumnWidth(10, 10, 40)

                If Request.QueryString("NomeStr") <> "--" Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "Situazione pagamenti - struttura " & Request.QueryString("NomeStr"))
                Else
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "Situazione pagamenti complessiva")
                End If
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 1, "CODICE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 2, "VOCE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 3, "NUM. ADP", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 4, "ANNO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 5, "IMPORTO PRENOTATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 6, "IMPORTO APPROVATO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 7, "FORNITORE", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 8, "REPERTORIO", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 9, "STATO PAGAMENTO", 0)
                If Request.QueryString("Str") = "-1" Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 3, 10, "NOME", 0)
                End If

                K = 4
                For Each row In dt2.Rows
                    If dt2.Rows(i).Item("CODICE").ToString.Contains("TOTALE") Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont2, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt2.Rows(i).Item("CODICE"), ""), 0)
                    Else
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt2.Rows(i).Item("CODICE"), ""), 0)
                    End If
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt2.Rows(i).Item("VOCE"), "").ToString.ToLower, 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt2.Rows(i).Item("NUM_ADP"), ""), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt2.Rows(i).Item("ANNO"), ""), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt2.Rows(i).Item("IMPORTO_PRENOTATO"), ""), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt2.Rows(i).Item("IMPORTO_APPROVATO"), ""), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt2.Rows(i).Item("FORNITORE"), "").ToString.ToLower, 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt2.Rows(i).Item("REPERTORIO"), "").ToString.ToLower, 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt2.Rows(i).Item("STATO_PAGAMENTO"), "").ToString.ToLower, 0)
                    If Request.QueryString("Str") = "-1" Then
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt2.Rows(i).Item("NOME"), "").ToString.ToLower, 0)
                    End If
                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
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
            Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")


        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub

    
    Protected Sub btnStampaPDF_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampaPDF.Click

        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            DataGrid1.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            If Request.QueryString("Str") = "-1" Then
                pdfConverter1.PageWidth = "1700"
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 30
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            If Request.QueryString("Str") <> "-1" Then
                pdfConverter1.PdfHeaderOptions.HeaderText = Me.lbltitolo.Text & " - " & Request.QueryString("NomeStr")
            Else
                pdfConverter1.PdfHeaderOptions.HeaderText = Me.lbltitolo.Text & " strutture"
            End If
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 9
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.RoyalBlue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            Dim nomefile As String = "SitPagamenti" & Format(Now, "yyyyMMddHH") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','SitPagamenti','');</script>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
