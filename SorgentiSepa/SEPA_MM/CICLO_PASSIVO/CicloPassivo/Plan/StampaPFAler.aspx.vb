Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class Contabilita_CicloPassivo_Plan_StampaPF
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim IDP As String = "0"
    Dim TestoPagina As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            IDP = Request.QueryString("ID")
            Dim Contatore As Integer = 1
            Dim Capitolo1 As Boolean = False
            Dim Capitolo2 As Boolean = False
            Dim Capitolo3 As Boolean = False
            Dim Capitolo4 As Boolean = False

            Dim Totale1 As Boolean = True
            Dim Totale2 As Boolean = True
            Dim Totale3 As Boolean = True
            Dim Totale4 As Boolean = True

            Dim T1 As Boolean = False
            Dim T2 As Boolean = False
            Dim T3 As Boolean = False
            Dim T4 As Boolean = False

            Dim Totale As Double = 0
            Dim TotaleALER As Double = 0


            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                TestoPagina = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>PIANO FINANZIARIO " & Request.QueryString("P") & "</p></br>"

                If Request.QueryString("T") <> "1" Then

                    TestoPagina = TestoPagina & "<table style='border: 1px solid #000000; width: 100%;' cellpadding=0 cellspacing = 0'>"
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 12pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO RICHIESTO (iva compresa)</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO APPROVATO (iva compresa)</td></tr>"



                    par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_piano_finanziario=" & IDP & " order by codice asc,indice asc"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()



                    While myReader1.Read

                        If Capitolo1 = False Then
                            If myReader1("indice") >= 1 And myReader1("indice") <= 20 Then
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                                Capitolo1 = True
                                Contatore = Contatore + 1
                            End If
                        End If

                        If Capitolo2 = False Then
                            If myReader1("indice") >= 21 And myReader1("indice") <= 40 Then

                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                                Contatore = Contatore + 1
                                Totale1 = False
                                Totale = 0
                                TotaleALER = 0
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                                Contatore = Contatore + 1
                                Capitolo2 = True
                                T1 = True
                            End If
                        End If


                        If Capitolo3 = False Then
                            If myReader1("indice") >= 41 And myReader1("indice") <= 45 Then

                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                                Contatore = Contatore + 1
                                Totale2 = False
                                Totale = 0
                                TotaleALER = 0

                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                                Contatore = Contatore + 1
                                Capitolo3 = True
                                T1 = True
                                T2 = True
                            End If
                        End If

                        If Capitolo4 = False Then
                            If myReader1("indice") >= 46 And myReader1("indice") <= 51 Then

                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                                Contatore = Contatore + 1
                                Totale3 = False
                                Totale = 0
                                TotaleALER = 0
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                                Contatore = Contatore + 1
                                Capitolo4 = True
                                T1 = True
                                T2 = True
                                T3 = True
                            End If
                        End If

                        par.cmd.CommandText = "select sum(valore_lordo),sum(valore_lordo_ALER) from siscom_mi.pf_voci_struttura where  id_voce=" & par.IfNull(myReader1("ID"), "-1")


                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then


                            TestoPagina = TestoPagina & "<tr style='font-family: ARIAL; font-size: 9pt;border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(par.IfNull(myReader2(0), 0), "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(par.IfNull(myReader2(1), 0), "##,##0.00") & "</td></tr>"

                            'If par.IfNull(myReader1("ID_VOCE_MADRE"), "-1") = "-1" Then
                            Totale = Totale + par.IfNull(myReader2(0), "0")
                            TotaleALER = TotaleALER + par.IfNull(myReader2(1), "0")
                            'End If
                        End If
                        myReader2.Close()



                        Contatore = Contatore + 1
                        If Contatore = 20 Then
                            Contatore = 1
                            TestoPagina = TestoPagina & "</table>"
                            TestoPagina = TestoPagina & "<p style='page-break-before: always'>&nbsp;</p>"
                            TestoPagina = TestoPagina & "<table style='border: 1px solid #000000; width: 100%;' cellpadding=0 cellspacing = 0'>"
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 12pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO RICHIESTO (iva compresa)</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>IMPORTO APPROVATO (iva compresa)</td></tr>"

                        End If
                    End While
                    myReader1.Close()



                    If Capitolo1 = False Then

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                        Contatore = Contatore + 1
                    End If


                    If Capitolo2 = False Then


                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                        Contatore = Contatore + 1
                        Totale1 = False
                        Totale = 0
                        TOTALEALER = 0

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                        Contatore = Contatore + 1
                        Capitolo2 = True
                        T1 = True

                    End If


                    If Capitolo3 = False Then


                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                        Contatore = Contatore + 1
                        Totale2 = False
                        Totale = 0
                        TOTALEALER = 0

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                        Contatore = Contatore + 1
                        Capitolo3 = True
                        T1 = True
                        T2 = True

                    End If

                    If Capitolo4 = False Then
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                        Contatore = Contatore + 1
                        Totale3 = False
                        Totale = 0
                        TOTALEALER = 0

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"
                        Contatore = Contatore + 1
                        Capitolo4 = True
                        T1 = True
                        T2 = True
                        T3 = True

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleALER, "##,##0.00") & "</td></tr>"
                        Contatore = Contatore + 1
                        Totale3 = False
                        Totale = 0
                        TOTALEALER = 0
                    End If

                    TestoPagina = TestoPagina & "</table>"

                

                End If


                Dim NomeFile As String = "PF_" & Format(Now, "yyyyMMddHHmmss")
                Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
                sr.WriteLine(TestoPagina)
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
                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfDocumentOptions.ShowFooter = False
                pdfConverter.PdfDocumentOptions.LeftMargin = 20
                pdfConverter.PdfDocumentOptions.RightMargin = 20
                pdfConverter.PdfDocumentOptions.TopMargin = 5
                pdfConverter.PdfDocumentOptions.BottomMargin = 5
                pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

                pdfConverter.PdfDocumentOptions.ShowHeader = False
                pdfConverter.PdfFooterOptions.FooterText = ("")
                pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
                pdfConverter.PdfFooterOptions.DrawFooterLine = False
                pdfConverter.PdfFooterOptions.PageNumberText = ""
                pdfConverter.PdfFooterOptions.ShowPageNumber = False


                pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
                IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
                Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".pdf")

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try




        End If


    End Sub

    'Private Function ValoreIvaCompresa(ByVal Importo As String, ByVal Iva As String) As Double
    '    ValoreIvaCompresa = CDbl(Importo + ((Importo * Iva) / 100))
    'End Function

End Class
