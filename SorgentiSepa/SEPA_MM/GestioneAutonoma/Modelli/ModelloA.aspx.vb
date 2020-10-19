Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class GestioneAutonoma_Modelli_ModelloA
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Dim NomeFile As String

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\ModA.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            'CICLO CHE ESEGUE LO SPLIT SULLA STRINGA CONTENENTE (IDEDIFICIO1,IDEDIFICIO2,IDEDIFICIO3...IDEDIFCION)
            Dim DenEdifici As String = ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim voci() As String
            voci = Request.QueryString("EDSELEZIONATI").Split(New Char() {","})

            For Each stringa As String In voci
                par.cmd.CommandText = "SELECT EDIFICI.DENOMINAZIONE,DESCRIZIONE, CIVICO, CAP, LOCALITA FROM SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND EDIFICI.ID =  " & stringa
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    DenEdifici = DenEdifici & "<br/>" & par.IfNull(myReader1("DENOMINAZIONE"), "") & " - "
                    DenEdifici = DenEdifici & par.IfNull(myReader1("DESCRIZIONE"), "") & " "
                    DenEdifici = DenEdifici & par.IfNull(myReader1("CIVICO"), "") & " "
                    DenEdifici = DenEdifici & par.IfNull(myReader1("LOCALITA"), "") & " - "
                    DenEdifici = DenEdifici & par.IfNull(myReader1("CAP"), "")
                End If
            Next
            contenuto = Replace(contenuto, "$edfici$", DenEdifici)




            par.OracleConn.Close()


            Dim url As String = Server.MapPath("..\Stampe\")
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
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            NomeFile = "Richiesta_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & NomeFile)

            Response.Redirect("..\Stampe\" & NomeFile)


        Catch ex As Exception
            par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza:Gestione Autonoma ModB" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub



End Class
