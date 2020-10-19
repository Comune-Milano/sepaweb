Imports ExpertPdf.HtmlToPdf
Imports System.IO

Partial Class VSA_Autorizzazioni
    Inherits PageSetIdMode

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Autorizzazioni()
        End If

    End Sub

    Protected Sub Autorizzazioni()

        Try
            Dim NomeFile As String = "Autorizzazioni" & Request.QueryString("IDom") & "_" & Format(Now, "yyyyMMddhhmm")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            Dim msg As String = "<html><head></head><body><table><tr><td><br/>Da definire</td></tr></table></body></html>"
            sr.WriteLine(msg)
            sr.Close()
            Dim url As String = NomeFile
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            Dim pdfConverter As PdfConverter = New PdfConverter
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PageWidth = 1100
            pdfConverter.PdfDocumentOptions.ShowHeader = True
            pdfConverter.PdfDocumentOptions.ShowFooter = True
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfHeaderOptions.HeaderText = "AUTORIZZAZIONI"
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\FileTemp\") & NomeFile & ".pdf")
            IO.File.Delete(Server.MapPath("..\FileTemp\") & NomeFile & ".htm")
            Response.Redirect("..\FileTemp\" & NomeFile & ".pdf")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub

End Class
