Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contratti_Comunicazioni_LetteraBolletta
    Inherits PageSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            Try

                PAR.OracleConn.Open()
                par.SettaCommand(par)


                PAR.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=12"
                Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReaderA.Read Then
                    txtTesto.Text = Replace((PAR.IfNull(myReaderA("VALORE"), "")), "<br />", vbCrLf)
                End If
                myReaderA.Close()

                PAR.cmd.CommandText = "select valore from siscom_MI.parametri_BOLLETTA where ID=13"
                myReaderA = PAR.cmd.ExecuteReader()
                If myReaderA.Read Then
                    txtNote.Text = Replace((PAR.IfNull(myReaderA("VALORE"), "")), "<br />", vbCrLf)
                End If
                myReaderA.Close()

                PAR.OracleConn.Close()

            Catch ex As Exception
                PAR.OracleConn.Close()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
            End Try
        End If
    End Sub


    Protected Sub btnAnteprima_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnteprima.Click
        Dim NomeFile As String

        Try


            NomeFile = Format(Now, "yyyyMMddHHmmss")

            'apro e memorizzo il testo base del contratto

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\TestoModelli\LetteraBollette.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()


            contenuto = Replace(contenuto, "$mese$", Format(Now, "MMMM") & " " & Year(Now))
            contenuto = Replace(contenuto, "$codcontratto$", "XXXXXXXXXXXXXXX")
            contenuto = Replace(contenuto, "$nominativo$", "XXXXXXXXXXXXXXX")
            contenuto = Replace(contenuto, "$indirizzo$", "XXXXXXXXXXXXXXX")
            contenuto = Replace(contenuto, "$capcitta$", "XXXXXXXXXXXXXXX")
            contenuto = Replace(contenuto, "$oggetto$", "Bollettazione del $periodo$")
            contenuto = Replace(contenuto, "$codcontratto$", "XXXXXXXXXXXXXXX")
            contenuto = Replace(contenuto, "$testolettera$", Replace(txtTesto.Text, vbCrLf, "<br />"))
            contenuto = Replace(contenuto, "$note$", Replace(txtNote.Text, vbCrLf, "<br />"))
            contenuto = Replace(contenuto, "$periodo$", "XXXXXXXXXXXXXXX")

            'scrivo il nuovo contratto compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()

            Dim url As String = NomeFile
            Dim pdfConverter As PdfConverter = New PdfConverter
            ''pdfConverter.LicenseKey = "P38cBx6AWW7b9c81TjEGxnrazP+J7rOjs+9omJ3TUycauK+cL WdrITM5T59hdW5r"
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfDocumentOptions.ShowFooter = False
            pdfConverter.PdfDocumentOptions.LeftMargin = 5
            pdfConverter.PdfDocumentOptions.RightMargin = 5
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False


            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\") & NomeFile & ".htm", Server.MapPath("..\..\ALLEGATI\CONTRATTI\StampeLettere\") & NomeFile & ".pdf")
            Response.Write("<script>window.open('../../ALLEGATI/CONTRATTI/StampeLettere/" & NomeFile & ".pdf" & "','Lettera','');</script>")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnHome_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub


    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        Try

            PAR.OracleConn.Open()
            par.SettaCommand(par)


            PAR.cmd.CommandText = "UPDATE siscom_MI.parametri_BOLLETTA SET VALORE='" & PAR.PulisciStrSql(Replace(txtTesto.Text, vbCrLf, "<br />")) & "' WHERE ID=12"
            PAR.cmd.ExecuteNonQuery()

            PAR.cmd.CommandText = "UPDATE siscom_MI.parametri_BOLLETTA SET VALORE='" & PAR.PulisciStrSql(Replace(txtNote.Text, vbCrLf, "<br />")) & "' WHERE ID=13"
            PAR.cmd.ExecuteNonQuery()

            PAR.OracleConn.Close()
            Response.Write("<script>alert('Operazione Effettuata!');</script>")

        Catch ex As Exception
            PAR.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub
End Class
