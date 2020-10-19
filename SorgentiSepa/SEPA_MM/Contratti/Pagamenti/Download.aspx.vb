Imports System.Data.OleDb
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums


Partial Class Contratti_Pagamenti_Download
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='divPre' style='position:absolute; background-color:#ffffff; text-align:center; width:100%; height:95%; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br/><br/><br/><br/><br/><br/><br/><br/><img src='../../Contabilita/IMMCONTABILITA/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()

            Stampa()

        End If

    End Sub

    Private Function Stampa()
        Dim Html As String = Session.Item("HtmlExport")
        'Dim stringWriter As New System.IO.StringWriter
        'Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            'Response.Flush()

            'dgvRptPagExtraMav.RenderControl(sourcecode)
            'sourcecode.Flush()
            'Html = stringWriter.ToString


            Dim url As String = Server.MapPath("..\..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 35
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PageWidth = 1250
            pdfConverter1.PdfHeaderOptions.HeaderText = Request.QueryString("TITLE")
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontStyle = Drawing.FontStyle.Bold

            pdfConverter1.PdfFooterOptions.FooterText = ""
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True


            Dim nomefile As String = "Exp_PgExtraMav" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(par.EliminaLink(Html), url & nomefile)
            Response.Write("<script>window.location.href ='../../FileTemp/" & nomefile & "'</script>")


        Catch ex As Exception
            Session.Add("ERRORE", "Report Pagamenti Extra Mav:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try
    End Function
End Class
