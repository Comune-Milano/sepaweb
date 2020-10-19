Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contabilita_DettaglioBolletta
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                Dim COLORE As String = "#E6E6E6"
                Dim bTrovato As Boolean = False
                Dim TotDaPagare As Double = 0
                Dim TotFinalePagato As Double = 0
                Dim TotFinaleDaPagare As Double = 0
                Dim testoTabella As String = ""
                Dim testoTabellaVoci As String = ""
                Dim sStringaSql As String = ""

                Dim Contatore As Integer = 0


                If Request.QueryString("DAL") <> "" Then
                    bTrovato = True
                    sStringaSql = sStringaSql & " BOL_BOLLETTE.DATA_EMISSIONE>='" & par.PulisciStrSql(Request.QueryString("DAL")) & "' "
                End If

                If Request.QueryString("AL") <> "" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                    sStringaSql = sStringaSql & " BOL_BOLLETTE.DATA_EMISSIONE<='" & par.PulisciStrSql(Request.QueryString("AL")) & "' "
                End If

                If Request.QueryString("IDBOLL") <> "" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                    sStringaSql = sStringaSql & " BOL_BOLLETTE.ID='" & par.PulisciStrSql(Request.QueryString("IDBOLL")) & "' "
                End If

                If Request.QueryString("IDBollCOP") <> "" Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                    sStringaSql = sStringaSql & " BOL_BOLLETTE.ID in " & par.PulisciStrSql(Request.QueryString("IDBollCOP")) & ""
                End If
                If Request.QueryString("PAGPARZ") = 1 Then
                    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                    sStringaSql = " NVL(FL_PAG_PARZ,0) > 0 "
                End If

                If sStringaSql <> "" Then
                    sStringaSql = sStringaSql & " AND "
                End If

                '*****lblrange di date
                If Request.QueryString("DAL") <> "" Or Request.QueryString("AL") <> "" Then
                    If Request.QueryString("DAL") <> "" Then
                        lblRangeDate.Text = lblRangeDate.Text & " Dal " & par.FormattaData(Request.QueryString("DAL"))
                    End If

                    If Request.QueryString("AL") <> "" Then
                        lblRangeDate.Text = lblRangeDate.Text & " Al " & par.FormattaData(Request.QueryString("AL"))
                    End If
                Else
                    Me.lblRangeDate.Visible = False
                End If

                If Request.QueryString("PAGPARZ") = 1 Then
                    Me.lblRangeDate.Text = "- Sulle quali sono stati registrati dei Pagamenti Parziali"
                    Me.lblRangeDate.Visible = True
                End If


                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf _
                                & "<tr>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong></strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUMERO BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>TIPO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial;'><strong>NUMERO RATA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>RIFERIMENTO</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA EMISSIONE</strong></span></td>" _
                                & "<td style='height: 19px'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA SCADENZA</strong></span></td>" _
                                & "<td style='height: 19px; text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>VOCI DELLA BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE BOLLETTA</strong></span></td>" _
                                & "<td style='height: 19px;text-align:center'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>DATA PAGAMENTO</strong></span></td>" _
                                & "<td style='height: 19px;text-align:right'>" _
                                & "<span style='font-size: 8pt; font-family: Arial'><strong>TOT PAGATO</strong></span></td>" _
                                & "</tr>"
                testoTabellaVoci = "<table cellpadding='0' cellspacing='0' width='100%'>" _
                    & "<tr>" _
                    & "<td style='height: 15'>" _
                    & "<span style='font-size: 6pt; font-family:Courier New'><strong>DESCRIZIONE</strong></span></td>" _
                    & "<td style='height: 15px;text-align:right'>" _
                    & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMPORTO</strong></span></td>" _
                    & "<td style='height: 15px;text-align:right'>" _
                    & "<span style='font-size: 6pt; font-family: Courier New'><strong>IMP.PAGATO</strong></span></td>" _
                    & "</tr>"
                '******APERTURA CONNESSIONE*****
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                ''ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
                Dim UltimoPagam As String = 0
                'par.cmd.CommandText = "SELECT to_char(to_date(MAX(DATA_PAGAMENTO),'yyyymmdd'),'dd/mm/yyyy') AS ULTIMA_BOLL FROM SISCOM_MI.BOL_BOLLETTE WHERE n_rata<>999 and N_RATA<>99"
                'Dim myReaderTEMP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReaderTEMP.Read Then
                UltimoPagam = par.FormattaData(Format(Now, "yyyyMMdd")) 'par.IfNull(myReaderTEMP("ULTIMA_BOLL"), 0)
                'End If
                'myReaderTEMP.Close()
                'ULTIMO AGGIORNAMENTO DELLE INFORMAZIONI
                If Request.QueryString("IDANA") <> "" And Request.QueryString("IDCONT") <> "" Then

                    'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.INTESTATARI_RAPPORTO WHERE  " & sStringaSql & " INTESTATARI_RAPPORTO.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND INTESTATARI_RAPPORTO.DATA_FINE>= TO_CHAR(TO_DATE(CURRENT_DATE,'dd/mm/yyyy'),'yyyymmdd')  AND INTESTATARI_RAPPORTO.ID_ANAGRAFICA =  " & Request.QueryString("IDANA") & " ORDER BY BOL_BOLLETTE.ID DESC"
                    par.cmd.CommandText = "SELECT BOL_BOLLETTE.*,(to_char(to_date(riferimento_da,'yyyymmdd'),'dd/mm/yyyy')||' - '||to_char(to_date(riferimento_a,'yyyymmdd'),'dd/mm/yyyy')) AS riferimento, (CASE WHEN ID_BOLLETTA_RIC IS NULL THEN TIPO_BOLLETTE.ACRONIMO ELSE TIPO_BOLLETTE.ACRONIMO ||'/RIC'END) AS TIPO,ID_TIPO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.TIPO_BOLLETTE " _
                                        & "WHERE SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND " & sStringaSql & " SOGGETTI_CONTRATTUALI.ID_CONTRATTO= BOL_BOLLETTE.ID_CONTRATTO AND BOL_BOLLETTE.ID_CONTRATTO = " & Request.QueryString("IDCONT") _
                                        & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA =  " & Request.QueryString("IDANA") & " AND TIPO_BOLLETTE.ID(+) = BOL_BOLLETTE.ID_TIPO AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE IN ('INTE','EXINTE') ORDER BY BOL_BOLLETTE.ID DESC"

                    myReader = par.cmd.ExecuteReader
                    Dim Nbolletta As String
                    Dim coloreAnnullo As String
                    Do While myReader.Read
                        Nbolletta = ""
                        If par.IfNull(myReader("N_RATA"), "") = "99" Then
                            Nbolletta = "MA/" & par.IfNull(myReader("ANNO"), "")
                        ElseIf par.IfNull(myReader("N_RATA"), "") = "999" Then
                            Nbolletta = "AU/" & par.IfNull(myReader("ANNO"), "")
                        ElseIf par.IfNull(myReader("N_RATA"), "") = "99999" Then
                            Nbolletta = "CO/" & par.IfNull(myReader("ANNO"), "")
                        Else
                            Nbolletta = par.IfNull(myReader("N_RATA"), "") & "/" & par.IfNull(myReader("ANNO"), "")
                        End If

                        Contatore = Contatore + 1
                        'If par.IfNull(myReader("FL_ANNULLATA"), "0") = "0" Then
                        If par.IfNull(myReader("FL_ANNULLATA"), "0") <> 0 Then
                            coloreAnnullo = "bgcolor = 'red'"
                        Else
                            coloreAnnullo = ""
                        End If
                        testoTabella = testoTabella _
                        & "<tr " & coloreAnnullo & ">" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Contatore & ")</span></td>" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(myReader("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("NUM_BOLLETTA"), "- - -") & "</a></span></td>"


                        If par.IfNull(myReader("TIPO"), "n.d.") = "MOR" Or par.IfNull(myReader("TIPO"), "n.d.") = "FIN" Then
                            testoTabella = testoTabella _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DetMorosita.aspx?IDBOLL=" & par.IfNull(myReader("ID"), "") & "','DettMorosita" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader("TIPO"), "n.d.") & "</a></span></td>"
                        Else
                            testoTabella = testoTabella _
                            & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & par.IfNull(myReader("TIPO"), "n.d.") & "</span></td>"
                        End If

                        testoTabella = testoTabella _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & Nbolletta & "</span></td>" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial;text-align:center'>" & par.IfNull(myReader("RIFERIMENTO"), "") & "</span></td>" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_EMISSIONE"), "")) & "</span></td>" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_SCADENZA"), "")) & "</span></td>" _
                        & "<td style='height: 19px;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                        & "<span style='font-size: 8pt; font-family: Courier New''>" & testoTabellaVoci & "</span>"

                        par.cmd.CommandText = "SELECT BOL_BOLLETTE_VOCI.*, T_VOCI_BOLLETTA.DESCRIZIONE FROM SISCOM_MI.BOL_BOLLETTE_VOCI, SISCOM_MI.T_VOCI_BOLLETTA WHERE ID_BOLLETTA = " & par.IfNull(myReader("ID"), 0) & " AND ID_VOCE = T_VOCI_BOLLETTA.ID"
                        myReader2 = par.cmd.ExecuteReader

                        Do While myReader2.Read
                            testoTabella = testoTabella _
                            & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 15px'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('DettaglioImpPagati.aspx?IDBOLLETTA=" & par.IfNull(myReader("ID"), 0) & "&IDVOCE=" & par.IfNull(myReader2("ID"), "") & "','DettPagati" & Format(Now, "hhss") & "','');" & Chr(34) & ">" & par.IfNull(myReader2("DESCRIZIONE"), "") & "</a></span></td>" _
                            & "<td style='height: 15px;text-align:right'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMPORTO"), 0)), "##,##0.00") & "</span></td>" _
                            & "<td style='height: 15px;text-align:right'>" _
                            & "<span style='font-size: 6pt; font-family: Courier New'>€." & Format((par.IfNull(myReader2("IMP_PAGATO"), 0)), "##,##0.00") & "</span></td>"

                            'TotDaPagare = TotDaPagare + par.IfNull(myReader2("IMPORTO"), 0)
                            If COLORE = "#FFFFFF" Then
                                COLORE = "#E6E6E6"
                            Else
                                COLORE = "#FFFFFF"
                            End If
                        Loop
                        myReader2.Close()
                        TotDaPagare = par.IfNull(myReader("IMPORTO_TOTALE"), 0)
                        ' - par.IfNull(myReader("IMPORTO_RIC_B"), 0)


                        If par.IfNull(myReader("ID_TIPO"), 1) = 4 Then
                            testoTabella = testoTabella & "</table></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(myReader("IMPORTO_TOTALE"), 0), "##,##0.00") & "<br/>MOROSITA' €." & Format(par.IfNull(myReader("IMPORTO_RIC_B"), 0), "##,##0.00") & "</span></td>" _
                                                        & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>"
                        ElseIf par.IfNull(myReader("ID_TIPO"), 1) = 5 Then
                            testoTabella = testoTabella & "</table></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(par.IfNull(myReader("IMPORTO_TOTALE"), 0), "##,##0.00") & "<br/>RATEIZ.' €." & Format(par.IfNull(myReader("IMPORTO_RIC_B"), 0), "##,##0.00") & "</span></td>" _
                                                        & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>"
                        Else
                            testoTabella = testoTabella & "</table></td>" _
                                                        & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format(TotDaPagare, "##,##0.00") & "</span></td>" _
                                                        & "<td style='height: 19px;text-align:center;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                        & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>" & par.FormattaData(par.IfNull(myReader("DATA_PAGAMENTO"), "")) & "&nbsp;</span></td>"
                        End If

                        testoTabella = testoTabella & "<td style='height: 19px;text-align:right;border-bottom-style: dotted; border-bottom-width: thin; border-bottom-color: #ACACAC'>" _
                                                    & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format((par.IfNull(myReader("IMPORTO_PAGATO"), 0)), "##,##0.00") & "</span></td></tr>"

                        ' & "<span style='font-size: 8pt; font-family: Arial; vertical-align :top '>€." & Format((par.IfNull(myReader("IMPORTO_PAGATO"), 0) - par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0)), "##,##0.00") & "</span></td></tr>"

                        TotFinalePagato = TotFinalePagato + (par.IfNull(myReader("IMPORTO_PAGATO"), 0))
                        '- par.IfNull(myReader("IMPORTO_RIC_PAGATO_B"), 0) - par.IfNull(myReader("QUOTA_SIND_PAGATA_B"), 0))
                        TotFinaleDaPagare = TotFinaleDaPagare + TotDaPagare
                    Loop
                    myReader.Close()
                    testoTabella = testoTabella _
                    & "<tr>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>TOTALE</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinaleDaPagare, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong></strong></span></td>" _
                    & "<td style='height: 19px;text-align:right'>" _
                    & "<span style='font-size: 8pt; font-family: Arial'><strong>€." & Format(TotFinalePagato, "##,##0.00") & "</strong></span></td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "<td style='height: 19px'>" _
                    & "</td>" _
                    & "</tr>"
                End If
                Me.TBL_DETTAGLIO_BOLLETTA.Text = testoTabella & "</table>"

                If Request.QueryString("IDANA") <> "" Then

                    par.cmd.CommandText = "SELECT CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"" FROM SISCOM_MI.ANAGRAFICA WHERE ANAGRAFICA.ID = " & Request.QueryString("IDANA")
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        Me.LblIntestazione.Text = myReader("INTESTATARIO") & " - informazioni aggiornate al: " & UltimoPagam
                    End If
                    myReader.Close()
                End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                Me.lblErrore.Visible = True
                lblErrore.Text = ex.Message
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Dim Html As String = ""
        'Dim stringWriter As New System.IO.StringWriter
        'Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try

            Html = TBL_DETTAGLIO_BOLLETTA.Text

            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = (LblIntestazione.text & " - Dettaglio Bollette Emesse " & Me.lblRangeDate.text)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_Bollette_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(LblIntestazione.text & "<br />Dettaglio Bollette Emesse " & Me.lblRangeDate.text & "<br /><br />" & Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPdfBollette','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
