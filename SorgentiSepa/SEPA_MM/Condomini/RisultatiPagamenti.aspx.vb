Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RisultatiPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim sUnita(19) As String
    Dim sDecina(9) As String

    Dim annoADPDa As String = ""
    Dim annoADPA As String = ""
    Dim numADPDa As String = ""
    Dim numADPA As String = ""
    Dim impADPDa As String = ""
    Dim impADPA As String = ""
    Dim numMandDa As String = ""
    Dim numMandA As String = ""
    Dim dataMandA As String = ""
    Dim dataMandDa As String = ""
    Dim condSelezionati As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Cerca()
        End If
    End Sub

    Private Sub Cerca()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim condizione As String = ""
            Dim condMandati As String = ""
            If Not String.IsNullOrEmpty(Request.QueryString("ANNODA")) Then
                condizione = condizione & " AND PAGAMENTI.ANNO >= " & Request.QueryString("ANNODA")
                annoADPDa = Request.QueryString("ANNODA")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("ANNOA")) Then
                condizione = condizione & " AND PAGAMENTI.ANNO <= " & Request.QueryString("ANNOA")
                annoADPA = Request.QueryString("ANNOA")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("NUMADPDA")) Then
                condizione = condizione & " AND PAGAMENTI.PROGR >= " & Request.QueryString("NUMADPDA")
                numADPDa = Request.QueryString("NUMADPDA")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("NUMADPA")) Then
                condizione = condizione & " AND PAGAMENTI.PROGR <= " & Request.QueryString("NUMADPA")
                numADPA = Request.QueryString("NUMADPA")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("IMPADPDA")) Then
                condizione = condizione & " AND PAGAMENTI.IMPORTO_CONSUNTIVATO >= " & Request.QueryString("IMPADPDA")
                impADPDa = Request.QueryString("IMPADPDA")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("IMPADPA")) Then
                condizione = condizione & " AND PAGAMENTI.IMPORTO_CONSUNTIVATO <= " & Request.QueryString("IMPADPA")
                impADPA = Request.QueryString("IMPADPA")
            End If
            If par.IfEmpty(Request.QueryString("COND"), "-1") <> "-1" Then
                condizione = condizione & " AND (CONDOMINI.ID in (" & Request.QueryString("COND") & "))"
            End If

            If Not String.IsNullOrEmpty(Request.QueryString("NUMMANDDA")) Or Not String.IsNullOrEmpty(Request.QueryString("NUMMANDA")) Or Not String.IsNullOrEmpty(Request.QueryString("DATAMANDDA")) Or Not String.IsNullOrEmpty(Request.QueryString("DATAMANDA")) Then
                condMandati = " AND PAGAMENTI.ID IN(SELECT ID_PAGAMENTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE "
                Dim E As Boolean = False
                If Not String.IsNullOrEmpty(Request.QueryString("NUMMANDDA")) Then
                    If E = True Then
                        condMandati = condMandati & " AND"
                    Else
                        E = True
                    End If
                    condMandati = condMandati & "  NUM_MANDATO >=" & Request.QueryString("NUMMANDDA")
                    numMandDa = Request.QueryString("NUMMANDDA")

                End If
                If Not String.IsNullOrEmpty(Request.QueryString("NUMMANDA")) Then
                    If E = True Then
                        condMandati = condMandati & " AND"
                    Else
                        E = True
                    End If
                    condMandati = condMandati & " NUM_MANDATO <=" & Request.QueryString("NUMMANDA")
                    numMandA = Request.QueryString("NUMMANDA")
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("DATAMANDDA")) Then
                    If E = True Then
                        condMandati = condMandati & " AND"
                    Else
                        E = True
                    End If
                    condMandati = condMandati & " DATA_MANDATO >=" & Request.QueryString("DATAMANDDA")
                    dataMandDa = Request.QueryString("DATAMANDDA")
                End If
                If Not String.IsNullOrEmpty(Request.QueryString("DATAMANDA")) Then
                    If E = True Then
                        condMandati = condMandati & " AND"
                    Else
                        E = True
                    End If
                    condMandati = condMandati & " DATA_MANDATO <=" & Request.QueryString("DATAMANDA")
                    dataMandA = Request.QueryString("DATAMANDA")
                End If
                condMandati = condMandati & ")"
            End If

            par.cmd.CommandText = "SELECT condomini.ID as id_condominio,pagamenti.id as id_pagamento,'<a href=""javascript:parent.main.location.replace(''Condominio.aspx?IdCond='|| CONDOMINI.ID ||'&CALL=RisultatiPagamenti&ANNODA=" & annoADPDa & "&ANNOA=" & annoADPA & "&NUMADPDA=" & numADPDa & "&NUMADPA=" & numADPA & "&IMPADPDA=" & impADPDa & "&IMPADPA=" & impADPA & "&NUMMANDA=" & numMandDa & "&NUMMANA=" & numMandA & "&DATAMANDDA=" & dataMandDa & "&DATAMANDA=" & dataMandA & "&CONDSELEZ=" & Request.QueryString("COND") & "'');"">'||REPLACE(condomini.denominazione,'''','')||'</a>' AS DENOMINAZIONE, " _
                                & "TRIM (TO_CHAR (importo_consuntivato, '9G999G999G990D99')) AS importo_approvato, " _
                                & "(pagamenti.progr ||'/'||pagamenti.anno) AS PROGR_ANNO, " _
                                & "TO_CHAR (TO_DATE (pagamenti.data_emissione, 'yyyymmdd'), 'dd/mm/yyyy') AS data_emissione, " _
                                & "TO_CHAR (TO_DATE (prenotazioni.data_scadenza, 'yyyymmdd'), 'dd/mm/yyyy') AS data_scadenza, " _
                                & "'' AS voce_budget, " _
                                & "prenotazioni.descrizione " _
                                & "FROM siscom_mi.prenotazioni, " _
                                & "siscom_mi.condomini, " _
                                & "siscom_mi.pagamenti, " _
                                & "siscom_mi.fornitori " _
                                & "WHERE pagamenti.ID = prenotazioni.id_pagamento " _
                                & "AND condomini.id_fornitore = fornitori.ID " _
                                & "AND prenotazioni.id_fornitore = fornitori.ID " _
                                & "AND prenotazioni.id_stato = 2 " _
                                & "AND id_pagamento IS NOT NULL " _
                                & condizione & condMandati _
                                & " GROUP BY condomini.ID ," _
                                & "pagamenti.ID," _
                                & "condomini.denominazione," _
                                & "importo_consuntivato," _
                                & "pagamenti.progr," _
                                & "pagamenti.anno," _
                                & "pagamenti.data_emissione," _
                                & "prenotazioni.data_scadenza," _
                                & "prenotazioni.descrizione " _
                                & "ORDER BY condomini.denominazione ASC, pagamenti.data_emissione asc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            da.Fill(dt)
            Dim Totale As Double = 0
            Dim r As Data.DataRow
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim tbVoci As String = ""
            For Each row As Data.DataRow In dt.Rows
                Totale = Totale + row.Item("IMPORTO_APPROVATO")
                par.cmd.CommandText = "select descrizione from siscom_mi.pf_voci where id in (select id_voce_pf from siscom_mi.prenotazioni where id_pagamento = " & row.Item("id_pagamento") & ")"
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    'tbVoci = "<table style='width:100%;'>"
                    While lettore.Read
                        'tbVoci = tbVoci & "<tr><td style='text-align: left; font-size:7pt;font-family :Arial ;border: 1px dotted #C0C0C0;'>" & par.IfNull(lettore("descrizione"), "") & "</td></tr>"
                        tbVoci &= par.IfNull(lettore("descrizione"), "") & "; "
                    End While
                End If
                lettore.Close()
                row.Item("voce_budget") = tbVoci '& "</table>"
                tbVoci = ""
            Next

            r = dt.NewRow
            r.Item("DESCRIZIONE") = "T O T A L E"
            r.Item("IMPORTO_APPROVATO") = Format(Totale, "##,##0.00")
            dt.Rows.Add(r)

            Session.Add("DTPAGA", dt)
            DataGridPagamenti.DataSource = dt
            DataGridPagamenti.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub




    Protected Sub DataGridPagamenti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridPagamenti.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(2).Text.Substring(e.Item.Cells(2).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "") & "; con Pagamento emesso il:" & e.Item.Cells(5).Text.Replace("'", "\'") & "';")
        End If

        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la il Condominio: " & e.Item.Cells(2).Text.Substring(e.Item.Cells(2).Text.IndexOf(">") + 1).Replace("</a>", "").Replace("\", "") & "; con Pagamento emesso il:" & e.Item.Cells(5).Text.Replace("'", "\'") & "';")
        End If
    End Sub


    Protected Sub DataGridPagamenti_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridPagamenti.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGridPagamenti.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If

    End Sub

    Protected Sub btnExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExcel.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridPagamenti, "PagametiCondomini", , True, , False)
        If System.IO.File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("..\/FileTemp\/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            DataGridPagamenti.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 40
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderText = "PAGAMENTI CONDOMINIALI"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 12
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold

            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10

            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "Exp_PagamentiCondominio_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(par.EliminaLink(Html), url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPagScadenza','');</script>")

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub
    Protected Sub btnNuovaRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnNuovaRicerca.Click
        Response.Write("<script>parent.main.location.replace('RicPagamenti.aspx');</script>")
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")
    End Sub
End Class
