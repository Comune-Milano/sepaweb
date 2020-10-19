Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Stampa
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim url As String = Server.MapPath("DOMANDE") & "\" & Request.QueryString("FILE") & ".pdf"
        ''Server.MapPath("DOMANDE") & UCase(txtCF.Text) & "_" & Format(Now, "yyyyMMdd") & ".htm"
        'Dim downloadName As String = Request.QueryString("FILE") & ".pdf"


        'Dim pdfConverter As PdfConverter = New PdfConverter
        ''pdfConverter.LicenseKey = "P38cBx6AWW7b9c81TjEGxnrazP+J7rOjs+9omJ3TUycauK+cL WdrITM5T59hdW5r"
        'pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        'pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        'pdfConverter.PdfDocumentOptions.ShowHeader = False
        'pdfConverter.PdfDocumentOptions.ShowFooter = False
        'pdfConverter.PdfDocumentOptions.LeftMargin = 5
        'pdfConverter.PdfDocumentOptions.RightMargin = 5
        'pdfConverter.PdfDocumentOptions.TopMargin = 5
        'pdfConverter.PdfDocumentOptions.BottomMargin = 5
        'pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        'pdfConverter.PdfDocumentOptions.ShowHeader = False
        ''pdfConverter.PdfHeaderOptions.HeaderText = "DOMANDA NUMERO XXX"
        ''pdfConverter.PdfHeaderOptions.HeaderSubtitleText = "Da compilare e spedire a:"

        'pdfConverter.PdfFooterOptions.FooterText = ("")
        'pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
        'pdfConverter.PdfFooterOptions.DrawFooterLine = False
        'pdfConverter.PdfFooterOptions.PageNumberText = ""
        'pdfConverter.PdfFooterOptions.ShowPageNumber = False

        'Dim downloadBytes() As Byte = pdfConverter.GetPdfFromUrlBytes(url)
        'Dim response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response

        'response.Clear()
        'response.AddHeader("Content-Type", "binary/octet-stream")
        'response.AddHeader("Content-Disposition", ("attachment; filename=" _
        '                + (downloadName + ("; size=" + downloadBytes.Length.ToString))))
        'response.Flush()
        'response.BinaryWrite(downloadBytes)
        'response.Flush()
        'response.End()

        'Dim objPDF As New Pintexx.Components.Web.pinPDF

        'objPDF.URL = Server.MapPath("DOMANDE") & "\" & Request.QueryString("FILE") & ".htm"
        'objPDF.OutputPath = Server.MapPath("DOMANDE") & "\"
        'objPDF.FileName = Request.QueryString("FILE") & ".pdf"

        'If Not objPDF.Process() Then
        '    Response.Write("Error: " + objPDF.ErrorMessage)
        'Else


        Response.ContentType = "binary/octet-stream" 'TipoFile
        Response.AddHeader("Content-Disposition", "attachment;filename=" & Request.QueryString("FILE") & ".pdf")
        Response.BufferOutput = True

        Response.BinaryWrite(IO.File.ReadAllBytes(url))
        Response.End()
        Response.Flush()


        'End If

    End Sub
End Class
