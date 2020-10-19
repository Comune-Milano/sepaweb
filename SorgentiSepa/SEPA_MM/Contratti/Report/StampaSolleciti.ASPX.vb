'Imports System.IO
'Imports ExpertPdf.HtmlToPdf
'Imports System.Drawing


Partial Class Contratti_Report_StampaSolleciti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim IdBolletta As Double = 0
    Dim importo As Double = 0
    Dim importo1 As Double = 0
    Dim DT As New Data.DataTable


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim DESCRIZIONE_T As String = ""

                If Request.QueryString("sDAL") <> "" Then
                    DESCRIZIONE_T = DESCRIZIONE_T & "Solleciti dal: " & par.FormattaData(Request.QueryString("sDAL")) & "</br>"
                End If

                If Request.QueryString("sAL") <> "" Then
                    DESCRIZIONE_T = DESCRIZIONE_T & "Solleciti al: " & par.FormattaData(Request.QueryString("sAL")) & "</br>"
                End If

                If Request.QueryString("eDAL") <> "" Then
                    DESCRIZIONE_T = DESCRIZIONE_T & "Emissione dal: " & par.FormattaData(Request.QueryString("eDAL")) & "</br>"
                End If

                If Request.QueryString("eAL") <> "" Then
                    DESCRIZIONE_T = DESCRIZIONE_T & "Emissione al: " & par.FormattaData(Request.QueryString("eAL")) & "</br>"
                End If

                If Request.QueryString("rDAL") <> "" Then
                    DESCRIZIONE_T = DESCRIZIONE_T & "Periodo di riferimento dal: " & par.FormattaData(Request.QueryString("rDAL")) & "</br>"
                End If

                If Request.QueryString("rAL") <> "" Then
                    DESCRIZIONE_T = DESCRIZIONE_T & "Periodo di riferimento al: " & par.FormattaData(Request.QueryString("rAL")) & "</br>"
                End If


                Response.Write("<table style='width:100%;'><tr><td style='font-family: ARIAL; font-size: 12pt; font-weight: bold;text-align: center;'>SOLLECITI EMESSI</td></tr><tr><td style='font-family: ARIAL; font-size: 10pt; font-weight: bold; text-align: left;'>" & DESCRIZIONE_T & "</td></tr></table></br></br>")
                Response.Write("<table cellpadding='0' cellspacing='0' border=0 width='100%'>")
                Response.Write("<tr style='font-family: arial; font-size: 9pt; font-weight: bold'><td align='left' bgcolor='#507cd1' style='color: #FFFFFF' >N.BOLLETTA</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>CONTRATTO</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>RATA</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>INTESTATARIO</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>EMISSIONE</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>SCADENZA</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>SOLLECITO</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'>PAGATA IL</td><td align='right' bgcolor='#507cd1' style='color: #FFFFFF'>IMPORTO</td><td align='left' bgcolor='#507cd1' style='color: #FFFFFF'></td></tr>")

                Dim miocolore As String = ""


                DT.Columns.Add("ID")
                DT.Columns.Add("CONTRATTO")
                DT.Columns.Add("RATA")
                DT.Columns.Add("INTESTATARIO")
                DT.Columns.Add("EMISSIONE")
                DT.Columns.Add("SCADENZA")
                DT.Columns.Add("SOLLECITO")
                DT.Columns.Add("PAGATA")
                DT.Columns.Add("IMPORTO")

                Dim RIGA As System.Data.DataRow

                miocolore = "#ffffff"
                par.cmd.CommandText = Session.Item("REPORT_S")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Do While myReader.Read

                    Response.Write("<tr>")
                    If IdBolletta <> par.IfNull(myReader("id"), 0) Then

                        RIGA = DT.NewRow()

                        RIGA.Item("ID") = Format(par.IfNull(myReader("id"), "0"), "0000000000")
                        RIGA.Item("CONTRATTO") = par.IfNull(myReader("contratto"), "")
                        RIGA.Item("RATA") = par.IfNull(myReader("rata"), "")
                        RIGA.Item("INTESTATARIO") = par.IfNull(myReader("intestatario"), "")
                        RIGA.Item("EMISSIONE") = par.IfNull(myReader("emissione"), "")
                        RIGA.Item("SCADENZA") = par.IfNull(myReader("scadenza"), "")
                        RIGA.Item("SOLLECITO") = par.IfNull(myReader("SOLLECITO"), "")
                        If par.IfNull(myReader("pagata"), "") <> "" Then
                            RIGA.Item("PAGATA") = par.IfNull(myReader("PAGATA"), "") 'par.IfNull(myReader("importo"), "0,00") 
                        Else
                            RIGA.Item("PAGATA") = "" '"0,00"
                        End If

                        RIGA.Item("IMPORTO") = par.IfNull(myReader("importo"), "0,00")

                        DT.Rows.Add(RIGA)


                        If miocolore = "#dcdcdc" Then
                            miocolore = "#ffffff"
                        Else
                            miocolore = "#dcdcdc"
                        End If
                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & Format(par.IfNull(myReader("id"), "0"), "0000000000") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & Format(par.IfNull(myReader("id"), "0"), "0000000000") & "</td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("contratto"), "&nbsp;") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../Contratto.aspx?LT=1&ID=" & par.IfNull(myReader("id_contratto"), "-1") & "&COD=" & par.IfNull(myReader("contratto"), "") & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');" & Chr(34) & ">" & par.IfNull(myReader("contratto"), "&nbsp;") & "</a></td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("rata"), "&nbsp;") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("rata"), "&nbsp;") & "</td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("intestatario"), "&nbsp;") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '><a href=" & Chr(34) & "../../Contabilita/DatiUtenza.aspx?C=RisUtenza&IDCONT=" & par.IfNull(myReader("id_contratto"), "-1") & "&IDANA=" & par.IfNull(myReader("IDANA"), "-1") & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">" & par.IfNull(myReader("intestatario"), "&nbsp;") & "</a></td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("emissione"), "&nbsp;") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("emissione"), "&nbsp;") & "</td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("scadenza"), "&nbsp;") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("scadenza"), "&nbsp;") & "</td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("sollecito"), "&nbsp;") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("sollecito"), "&nbsp;") & "</td>")

                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfEmpty(par.IfNull(myReader("pagata"), "&nbsp;"), "&nbsp;") & "</td>")
                        
                        Response.Write("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("importo"), "0,00") & "</td>")

                        ' ''sr.WriteLine("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '><a href='../../Contabilita/DettaglioBolletta.aspx?IDANA=" & par.IfNull(myReader("idana"), "0") & "&IDBOLL=" & par.IfNull(myReader("id"), "0") & "' target='_blank'>Dettagli</a></td>")
                        'Response.Write("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '><a href='../../Contabilita/DettaglioBolletta.aspx?IDANA=" & par.IfNull(myReader("idana"), "0") & "&IDBOLL=" & par.IfNull(myReader("id"), "0") & "' target='_blank'>Dettagli</a></td>")

                        IdBolletta = par.IfNull(myReader("id"), 0)
                        importo = importo + par.IfNull(myReader("importo"), 0)
                        If par.IfNull(myReader("pagata"), "") <> "" Then
                            importo1 = importo1 + par.IfNull(myReader("importo"), 0)
                        End If

                    Else
                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal;'>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal;'>&nbsp;</td>")

                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")



                        'sr.WriteLine("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("sollecito"), "") & "</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";font-family: arial; font-size: 9pt; font-weight: normal; '>" & par.IfNull(myReader("sollecito"), "") & "</td>")

                        'sr.WriteLine("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                        ''sr.WriteLine("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                        Response.Write("<td style='background-color: " & miocolore & ";text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                        RIGA = DT.NewRow()

                        RIGA.Item("ID") = ""
                        RIGA.Item("CONTRATTO") = ""
                        RIGA.Item("RATA") = ""
                        RIGA.Item("INTESTATARIO") = ""
                        RIGA.Item("EMISSIONE") = ""
                        RIGA.Item("SCADENZA") = ""
                        RIGA.Item("SOLLECITO") = par.IfNull(myReader("SOLLECITO"), "")
                        
                            RIGA.Item("PAGATA") = "" '"0,00"


                        RIGA.Item("IMPORTO") = ""

                        DT.Rows.Add(RIGA)


                        'RIGA = DT.NewRow()

                        'RIGA.Item("ID") = Format(par.IfNull(myReader("id"), "0"), "0000000000")
                        'RIGA.Item("CONTRATTO") = par.IfNull(myReader("contratto"), "")
                        'RIGA.Item("RATA") = par.IfNull(myReader("rata"), "")
                        'RIGA.Item("INTESTATARIO") = par.IfNull(myReader("intestatario"), "")
                        'RIGA.Item("EMISSIONE") = par.IfNull(myReader("emissione"), "")
                        'RIGA.Item("SCADENZA") = par.IfNull(myReader("scadenza"), "")
                        'RIGA.Item("SOLLECITO") = par.IfNull(myReader("sollecito"), "")
                        'If par.IfNull(myReader("pagata"), "") <> "" Then
                        '    RIGA.Item("PAGATA") = par.IfNull(myReader("importo"), "0,00")
                        'Else
                        '    RIGA.Item("PAGATA") = "0,00"
                        'End If
                        'RIGA.Item("IMPORTO") = par.IfNull(myReader("importo"), "0,00")



                        'DT.Rows.Add(RIGA)
                    End If
                        Response.Write("</tr>")

                Loop
                myReader.Close()

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")


                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>TOTALI</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: bold; '>TOTALI</td>")


                'sr.WriteLine("<td style='text-align: right;font-family: arial; font-size: 9pt; font-weight: bold; '>" & Format(importo, "##,##0.00") & "</td>")
                Response.Write("<td style='font-family: arial; font-size: 9pt; font-weight: bold; '>" & Format(importo1, "##,##0.00") & "</td>")

                'sr.WriteLine("<td style='font-family: arial; font-size: 9pt; font-weight: normal; '>" & Format(importo1, "##,##0.00") & "</td>")
                Response.Write("<td style='text-align: right;font-family: arial; font-size: 9pt; font-weight: bold; '>" & Format(importo, "##,##0.00") & "</td>")

                ''sr.WriteLine("<td style='text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")
                'Response.Write("<td style='text-align: right;font-family: arial; font-size: 9pt; font-weight: normal; '>&nbsp;</td>")

                'sr.WriteLine("</table>")

                'sr.WriteLine("<body>")
                'sr.WriteLine("</html>")

                'Response.Write("</table>")

                RIGA = DT.NewRow()

                RIGA.Item("ID") = ""
                RIGA.Item("CONTRATTO") = ""
                RIGA.Item("RATA") = ""
                RIGA.Item("INTESTATARIO") = ""
                RIGA.Item("EMISSIONE") = ""
                RIGA.Item("SCADENZA") = ""
                RIGA.Item("SOLLECITO") = ""
                RIGA.Item("PAGATA") = Format(importo1, "##,##0.00")
                RIGA.Item("IMPORTO") = Format(importo, "##,##0.00")

                DT.Rows.Add(RIGA)

                'sr.Close()
                Session.Add("MIADTS", DT)
                'DataGridPagamenti.DataSource = DT
                'DataGridPagamenti.DataBind()

                'Dim url As String = Server.MapPath("..\Varie\Solleciti_") & NomeFile1
                'Dim pdfConverter1 As PdfConverter = New PdfConverter

                'Dim Licenza As String = Session.Item("LicenzaPdfMerge"))
                'Licenza = Session.Item("LicenzaHtmlToPdf"))
                'If Licenza <> "" Then
                '    pdfConverter1.LicenseKey = Licenza
                'End If

                'pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
                'pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
                'pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
                'pdfConverter1.PdfDocumentOptions.ShowHeader = False
                'pdfConverter1.PdfDocumentOptions.ShowFooter = False
                'pdfConverter1.PdfDocumentOptions.LeftMargin = 20
                'pdfConverter1.PdfDocumentOptions.RightMargin = 20
                'pdfConverter1.PdfDocumentOptions.TopMargin = 20
                'pdfConverter1.PdfDocumentOptions.BottomMargin = 20
                'pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

                'pdfConverter1.PdfDocumentOptions.ShowHeader = False
                'pdfConverter1.PdfFooterOptions.FooterText = ("")
                'pdfConverter1.PdfFooterOptions.FooterTextColor = Color.Blue
                'pdfConverter1.PdfFooterOptions.DrawFooterLine = False
                'pdfConverter1.PdfFooterOptions.PageNumberText = ""
                'pdfConverter1.PdfFooterOptions.ShowPageNumber = False
                'pdfConverter1.SavePdfFromUrlToFile(url & ".htm", url & ".pdf")
                'File.Delete(url & ".htm")

                'Response.Write("</br><p style='font-family: arial; font-size: 10pt; font-weight: bold'>Totale Solleciti: " & Format(importo, "##,##0.00") & " Euro</p>")
                'Response.Write("</br><p style='font-family: arial; font-size: 10pt; font-weight: bold'>di cui già incassati: " & Format(importo1, "##,##0.00") & " Euro</p>")
                'Response.Write("</br></br><p style='font-family: arial; font-size: 10pt; font-weight: bold'><a href='..\Varie\" & NomeFile1 & ".pdf' target='_blank'>Clicca qui per esportare in formato PDF</a></p>")
                'Response.Write("</br></br><p style='font-family: arial; font-size: 10pt; font-weight: bold'><a href='SollecitiExport.aspx' target='_blank'>Clicca qui per esportare in formato Excel</a></p>")


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(Err.Description)
            End Try
        End If

    End Sub


End Class
