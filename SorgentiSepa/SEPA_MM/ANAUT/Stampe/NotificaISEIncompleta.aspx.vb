Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class ANAUT_Stampe_NotificaISE
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Try


            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\MODELLI\IncompletaISEPosta.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))

            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            If Request.QueryString("AU") <> "1" Then
                contenuto = Replace(contenuto, "$testoresponsabile$", "Il responsabile di Filiale")
            Else
                contenuto = Replace(contenuto, "$testoresponsabile$", "Il responsabile")
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
            contenuto = Replace(contenuto, "$numeroverde$", par.DeCripta(Request.QueryString("13")))
            contenuto = Replace(contenuto, "$responsabile$", par.DeCripta(Request.QueryString("14")))
            contenuto = Replace(contenuto, "$centrodicosto$", par.DeCripta(Request.QueryString("15")) & "/" & Request.QueryString("PG"))
            contenuto = Replace(contenuto, "$indirizzo2$", par.DeCripta(Request.QueryString("16")))
            contenuto = Replace(contenuto, "$data$", Format(Now, "dd/MM/yyyy"))

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim i As Integer = 1
            Dim elencodoc As String = ""
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select * from utenza_doc_mancante where id_dichiarazione=" & Request.QueryString("ID") & " order by descrizione asc"
            myReader = par.cmd.ExecuteReader()
            Dim j As Integer = 1

            elencodoc = "<table style='width:100%;' cellpadding='0' cellspacing='0'>"


            Do While myReader.Read
                
                elencodoc = elencodoc & "<tr><td width='2%' valign='top'>" & j & ")</td><td>" & par.Elimina160(myReader("descrizione")) & ";</td></tr>"
                j = j + 1
                If Len(par.Elimina160(myReader("descrizione"))) <= 86 Then
                    i = i + 1
                Else
                    i = i + 2
                End If
            Loop
            myReader.Close()
            elencodoc = elencodoc & "</table>"

            par.cmd.CommandText = "select * from UTENZA_DICHIARAZIONI where id=" & Request.QueryString("ID")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Select Case myReader("ID_BANDO")
                    Case "1"
                        contenuto = Replace(contenuto, "$ANNO$", "2007")
                        contenuto = Replace(contenuto, "$ANNOREDDITI$", "2006")
                    Case "2"
                        contenuto = Replace(contenuto, "$ANNO$", "2009")
                        contenuto = Replace(contenuto, "$ANNOREDDITI$", "2008")
                    Case "3"
                        contenuto = Replace(contenuto, "$ANNO$", "2011")
                        contenuto = Replace(contenuto, "$ANNOREDDITI$", "2010")
                End Select
            End If
            myReader.Close()

            If i > 13 Then
                contenuto = Replace(contenuto, "$spazi$", "")
                contenuto = Replace(contenuto, "$spazi1$", "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />")
                contenuto = Replace(contenuto, "$acapo$", "<p style='page-break-before: always'>&nbsp;</p><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />")
            Else
                contenuto = Replace(contenuto, "$spazi$", "")
                contenuto = Replace(contenuto, "$spazi1$", "")
                contenuto = Replace(contenuto, "$acapo$", "")
            End If

            contenuto = Replace(contenuto, "$documenti$", elencodoc)

            par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                 & "VALUES (" & Request.QueryString("ID") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                 & "'F177','','I')"
            par.cmd.ExecuteNonQuery()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Dim NomeFile1 As String = "03_" & Request.QueryString("COD") & "_" & Request.QueryString("ID") & "-" & Format(Now, "yyyyMMddHHmmss")
            'Dim NomeFileMerge As String = "03_" & Request.QueryString("COD") & "-" & Format(Now, "yyyyMMddHHmmss")
            'Dim NomeFileDichiarazione As String = "02_" & Request.QueryString("COD") & ".pdf"

            'scrivo il nuovo modulo compilato
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

            'If i > 6 Then
            '    pdfConverter1.PdfDocumentOptions.ShowFooter = True
            '    pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Black
            '    pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            '    pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            '    pdfConverter1.PdfFooterOptions.ShowPageNumber = True
            'Else
            '    pdfConverter1.PdfDocumentOptions.ShowFooter = False
            'End If

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

            Session.Add("ERRORE", "Provenienza:Stampa Notifica ISE Post" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
