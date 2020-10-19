Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ANAUT_Stampe_NotificaISE
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim NomeFile As String

        Try


            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\MODELLI\NotificaISEPosta.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\VSA\ModelliCambio22\ComposizioneNucleo.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            If Request.QueryString("AU") <> "1" Then
                contenuto = Replace(contenuto, "$notificapostale$", "<table cellpadding='0' cellspacing='0' style='border: 1px solid #000000' width='100%'><tr><td style='font-family: ARIAL; font-size: 16pt; font-weight: bold'>PER NOTIFICAZIONE POSTALE</td><td>&nbsp;</td></tr><tr><td style='font-family: arial; font-size: 14pt; font-weight: normal' width='50%'>Non essendo stato possibile notificarLe<br />le risultanze della verifica nell’incontro programmato,<br />Le trasmettiamo la presente comunicazione per<br />via Postale.</td><td style='font-family: arial; font-size: 14pt; font-weight: normal' valign='top' width='50%'><b>Comunicazione Postale</b><br />Rif. MM S.P.A.<br />$protocollo$</td></tr></table>")
                contenuto = Replace(contenuto, "$testoresponsabile$", "Il responsabile di Filiale")
                contenuto = Replace(contenuto, "$altrotesto$", ".")
            Else
                contenuto = Replace(contenuto, "$notificapostale$", "")
                contenuto = Replace(contenuto, "$testoresponsabile$", "Il responsabile")
                contenuto = Replace(contenuto, "$altrotesto$", "sulla base della documentazione pervenuta tramite il suo Sindacato.<br />Per qualsiasi chiarimento in merito, la invitiamo a contattare la sua organizzazione sindacale.")
            End If

            contenuto = Replace(contenuto, "$codcontratto$", Request.QueryString("COD"))
            contenuto = Replace(contenuto, "$protocollo$", Request.QueryString("PG"))
            contenuto = Replace(contenuto, "$intestatario$", par.DeCripta(Request.QueryString("1")))

            contenuto = Replace(contenuto, "$indirizzo$", par.DeCripta(Request.QueryString("5")))
            contenuto = Replace(contenuto, "$localita$", par.DeCripta(Request.QueryString("6")))

            contenuto = Replace(contenuto, "$nominativo$", par.DeCripta(Request.QueryString("2")))
            contenuto = Replace(contenuto, "$indirizzo0$", par.DeCripta(Request.QueryString("3")))
            contenuto = Replace(contenuto, "$indirizzo1$", par.DeCripta(Request.QueryString("4")))

            contenuto = Replace(contenuto, "$nomefiliale$", par.DeCripta(Request.QueryString("7")))
            contenuto = Replace(contenuto, "$indirizzofiliale$", par.DeCripta(Request.QueryString("8")))
            contenuto = Replace(contenuto, "$capfiliale$", par.DeCripta(Request.QueryString("9")))
            contenuto = Replace(contenuto, "$cittafiliale$", par.DeCripta(Request.QueryString("10")))
            contenuto = Replace(contenuto, "$telfax$", par.DeCripta(Request.QueryString("11")))
            contenuto = Replace(contenuto, "$referente$", par.DeCripta(Request.QueryString("12")))
            contenuto = Replace(contenuto, "$centrodicosto$", par.DeCripta(Request.QueryString("13")) & "/" & Request.QueryString("PG"))
            contenuto = Replace(contenuto, "$indirizzo2$", par.DeCripta(Request.QueryString("14")))
            contenuto = Replace(contenuto, "$responsabile$", par.DeCripta(Request.QueryString("15")))
            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))




            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                 & "VALUES (" & Request.QueryString("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                 & "'F176','','I')"
            par.cmd.ExecuteNonQuery()

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select * from UTENZA_DICHIARAZIONI where id=" & Request.QueryString("ID")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Select Case myReader("ID_BANDO")
                    Case "1"
                        contenuto = Replace(contenuto, "$ANNO$", "2007")
                        contenuto = Replace(contenuto, "$ANNOREDDITI$", "2007")
                    Case "2"
                        contenuto = Replace(contenuto, "$ANNO$", "2009")
                        contenuto = Replace(contenuto, "$ANNOREDDITI$", "2008")
                    Case "3"
                        contenuto = Replace(contenuto, "$ANNO$", "2011")
                        contenuto = Replace(contenuto, "$ANNOREDDITI$", "2010")
                End Select
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Dim NomeFile1 As String = "XX_" & Request.QueryString("COD") & "_" & Format(Now, "yyyyMMddHHmmss")

            Dim NomeFileMerge As String = "01_" & Request.QueryString("COD") & "_" & Request.QueryString("ID") & "-" & Format(Now, "yyyyMMddHHmmss")

            Dim NomeFileDichiarazione As String = "02_" & Request.QueryString("COD") & "_" & Request.QueryString("ID") & ".pdf"

            'scrivo il nuovo modulo compilato
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()


            Dim url As String = Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile1
            Dim url1 As String = Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFileDichiarazione

            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = ""

            Licenza = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 40
            pdfConverter1.PdfDocumentOptions.RightMargin = 40
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 30
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")
            IO.File.Delete(url & ".htm")

            Dim pdfDocumentOptions As New ExpertPdf.MergePdf.PdfDocumentOptions()
            pdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.Normal
            pdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            Dim pdfMerge As New ExpertPdf.MergePdf.PDFMerge(pdfDocumentOptions)

            Licenza = Session.Item("LicenzaPdfMerge")
            If Licenza <> "" Then
                pdfMerge.LicenseKey = Licenza
            End If

            pdfMerge.AppendPDFFile(url & ".pdf")

            If IO.File.Exists(url1) = True Then
                pdfMerge.AppendPDFFile(url1)
            End If
            pdfMerge.SaveMergedPDFToFile(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFileMerge & ".pdf")
            IO.File.Delete(url & ".pdf")

            

            Response.Redirect("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & NomeFileMerge & ".pdf")



        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:Stampa Notifica ISE Post" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
