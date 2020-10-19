Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class ANAUT_AutocertStServ
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\MODELLI\AutocertStatoServizio.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            Dim idDich As Long = 0
            contenuto = Replace(contenuto, "$codContratto$", Request.QueryString("COD"))

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.PG,UTENZA_DICHIARAZIONI.ID AS IDDICH,COMUNI_NAZIONI.nome AS comune,COMUNI_NAZIONI.sigla,UTENZA_COMP_NUCLEO.nome,UTENZA_COMP_NUCLEO.cognome,UTENZA_COMP_NUCLEO.data_nascita " _
                                & "FROM UTENZA_COMP_NUCLEO, COMUNI_NAZIONI, UTENZA_DICHIARAZIONI " _
                                & "WHERE UTENZA_COMP_NUCLEO.id_dichiarazione=UTENZA_DICHIARAZIONI.ID AND UTENZA_COMP_NUCLEO.progr=0 AND " _
                                & "COMUNI_NAZIONI.ID = UTENZA_DICHIARAZIONI.ID_LUOGO_NAS_DNTE " _
                                & "AND UTENZA_DICHIARAZIONI.ID=" & Request.QueryString("ID")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                idDich = par.IfNull(myReader("IDDICH"), 0)
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), ""))
                contenuto = Replace(contenuto, "$dataNasc$", par.FormattaData(par.IfNull(myReader("data_nascita"), "")))
                contenuto = Replace(contenuto, "$comuNasc$", par.IfNull(myReader("comune"), ""))
                contenuto = Replace(contenuto, "$prov$", par.IfNull(myReader("sigla"), ""))
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            '************ 11/09/2012 CODICE A BARRE ************
            Dim PercorsoBarCode As String = par.RicavaBarCode(4, idDich)
            contenuto = Replace(contenuto, "$barcode$", Server.MapPath("..\..\FileTemp\") & PercorsoBarCode)
            '************ FINE 11/09/2012 CODICE A BARRE ************

            Dim NomeFile1 As String = "04_" & Request.QueryString("COD") & "-" & Format(Now, "yyyyMMddHHmmss")

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
            sr.WriteLine(contenuto)
            sr.Close()


            Dim url As String = Server.MapPath("..\..\ALLEGATI\ANAGRAFE_UTENZA\") & NomeFile1

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

            pdfConverter1.PdfDocumentOptions.LeftMargin = 40
            pdfConverter1.PdfDocumentOptions.RightMargin = 40
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 1
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            pdfConverter1.PdfDocumentOptions.ShowHeader = False


            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")
            IO.File.Delete(url & ".htm")

            Response.Redirect("..\..\ALLEGATI\ANAGRAFE_UTENZA\" & NomeFile1 & ".pdf")

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza: Autocert.stato di servizio" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
