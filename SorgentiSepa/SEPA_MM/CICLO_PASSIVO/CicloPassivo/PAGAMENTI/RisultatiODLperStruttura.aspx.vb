Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing


Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiODLperStruttura
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Session.Item("BP_GENERALE") = 1 Then
            StampaTabella()
        Else
            If Session.Item("ID_STRUTTURA") = Request.QueryString("IDS") Then
                StampaTabella()
            Else
                Response.Write("<script>top.location.href=""../../../AccessoNegato.htm""</script>")
            End If
        End If

    End Sub

    Protected Sub StampaTabella()
        Try
            '*****************APERTURA CONNESSIONE***************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim esercizioFinanziario As String = Request.QueryString("EF")
            Dim idStruttura As String = Request.QueryString("IDS")
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID='" & idStruttura & "'"
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            lblTitolo.Text = ""
            If LETTORE.Read Then
                lblTitolo.Text = "SITUAZIONE ODL - STRUTTURA " & par.IfNull(LETTORE(0), "")
            End If
            LETTORE.Close()

            '######## ELENCO DEGLI ODL PER STRUTTURA ED ESERCIZIO FINANZIARIO ######
            par.cmd.CommandText = "SELECT TIPO_PAGAMENTO,PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "MANUTENZIONI.PROGR ||'/'||MANUTENZIONI.ANNO AS N_ODL," _
                & "TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_PRENOTATO,0),'999G999G990D99')) AS IMPORTO_PRENOTATO," _
                & "TRIM(TO_CHAR(TO_DATE(PRENOTAZIONI.DATA_PRENOTAZIONE,'yyyyMMdd'),'dd/MM/yyyy')) AS DATA_PRENOTAZIONE," _
                & "TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_APPROVATO," _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "NUM_REPERTORIO AS REPERTORIO,PRENOTAZIONI.ID_PAGAMENTO AS ID_P " _
                & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.FORNITORI," _
                & "SISCOM_MI.APPALTI " _
                & "WHERE MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO = PRENOTAZIONI.ID(+) " _
                & "AND APPALTI.ID=MANUTENZIONI.ID_APPALTO " _
                & "AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID(+) " _
                & "AND PF_VOCI.ID(+)=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND MANUTENZIONI.STATO<>0 " _
                & "AND MANUTENZIONI.STATO<>5 " _
                & "AND PRENOTAZIONI.ID_STATO<>-3 " _
                & "AND MANUTENZIONI.ID_PIANO_FINANZIARIO='" & esercizioFinanziario & "' " _
                & "AND MANUTENZIONI.ID_STRUTTURA='" & idStruttura & "' ORDER BY PF_VOCI.CODICE,MANUTENZIONI.PROGR ASC"

            Dim lettoreOdl As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim RIGA As Data.DataRow
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("CODICE")
            dt.Columns.Add("VOCE")
            dt.Columns.Add("N_ODL")
            dt.Columns.Add("IMPORTO_PRENOTATO")
            dt.Columns.Add("DATA_PRENOTAZIONE")
            dt.Columns.Add("IMPORTO_APPROVATO")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("REPERTORIO")
            Dim i As Integer = 0
            Dim codice As String = ""
            Dim totalePrenotato As Decimal = 0
            Dim totaleApprovato As Decimal = 0
            Dim totaliPrenotati As Decimal = 0
            Dim totaliApprovati As Decimal = 0
            While lettoreOdl.Read
                i = i + 1
                codice = par.IfNull(lettoreOdl("CODICE"), "")
                If i = 1 Then
                    codicevoce.Value = codice
                    totalePrenotato = totalePrenotato + CDec(par.IfNull(lettoreOdl("IMPORTO_PRENOTATO"), 0))
                    totaleApprovato = totaleApprovato + CDec(par.IfNull(lettoreOdl("IMPORTO_APPROVATO"), 0))
                Else
                    If codice = codicevoce.Value Then
                        totalePrenotato = totalePrenotato + CDec(par.IfNull(lettoreOdl("IMPORTO_PRENOTATO"), 0))
                        totaleApprovato = totaleApprovato + CDec(par.IfNull(lettoreOdl("IMPORTO_APPROVATO"), 0))
                    Else
                        'INSERISCO LA RIGA DEI TOTALI
                        RIGA = dt.NewRow
                        RIGA.Item("CODICE") = "TOTALE VOCE"
                        RIGA.Item("VOCE") = ""
                        RIGA.Item("N_ODL") = ""
                        RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotato, "##,##0.00")
                        RIGA.Item("DATA_PRENOTAZIONE") = ""
                        RIGA.Item("IMPORTO_APPROVATO") = Format(totaleApprovato, "##,##0.00")
                        RIGA.Item("FORNITORE") = ""
                        RIGA.Item("REPERTORIO") = ""
                        dt.Rows.Add(RIGA)
                        totaliPrenotati = totaliPrenotati + totalePrenotato
                        totaliApprovati = totaliApprovati + totaleApprovato
                        'PER LA VOCE SUCCESSIVA
                        totalePrenotato = par.IfNull(lettoreOdl("IMPORTO_PRENOTATO"), "")
                        totaleApprovato = par.IfNull(lettoreOdl("IMPORTO_APPROVATO"), "")
                        codicevoce.Value = codice
                    End If
                End If
                RIGA = dt.NewRow
                RIGA.Item("CODICE") = par.IfNull(lettoreOdl("CODICE"), "")
                RIGA.Item("VOCE") = par.IfNull(lettoreOdl("VOCE"), "")
                If CStr(par.IfNull(lettoreOdl("ID_P"), "")) = "" Then
                    RIGA.Item("N_ODL") = par.IfNull(lettoreOdl("N_ODL"), "")
                Else
                    RIGA.Item("N_ODL") = "<a href=""javascript:ApriPagamenti(" & par.IfNull(lettoreOdl("ID_P"), "") & "," & par.IfNull(lettoreOdl("TIPO_PAGAMENTO"), "0") & ");"">" & par.IfNull(lettoreOdl("N_ODL"), "") & "</a>"
                End If
                RIGA.Item("IMPORTO_PRENOTATO") = par.IfNull(lettoreOdl("IMPORTO_PRENOTATO"), "")
                RIGA.Item("DATA_PRENOTAZIONE") = par.IfNull(lettoreOdl("DATA_PRENOTAZIONE"), "")
                RIGA.Item("IMPORTO_APPROVATO") = par.IfNull(lettoreOdl("IMPORTO_APPROVATO"), "")
                RIGA.Item("FORNITORE") = par.IfNull(lettoreOdl("FORNITORE"), "")
                RIGA.Item("REPERTORIO") = par.IfNull(lettoreOdl("REPERTORIO"), "")
                dt.Rows.Add(RIGA)
            End While
            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCE"
            RIGA.Item("VOCE") = ""
            RIGA.Item("N_ODL") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totalePrenotato, "##,##0.00")
            RIGA.Item("DATA_PRENOTAZIONE") = ""
            RIGA.Item("IMPORTO_APPROVATO") = Format(totaleApprovato, "##,##0.00")
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("REPERTORIO") = ""
            dt.Rows.Add(RIGA)
            totaliPrenotati = totaliPrenotati + totalePrenotato
            totaliApprovati = totaliApprovati + totaleApprovato

            RIGA = dt.NewRow
            RIGA.Item("CODICE") = "TOTALE VOCI"
            RIGA.Item("VOCE") = ""
            RIGA.Item("N_ODL") = ""
            RIGA.Item("IMPORTO_PRENOTATO") = Format(totaliPrenotati, "##,##0.00")
            RIGA.Item("DATA_PRENOTAZIONE") = ""
            RIGA.Item("IMPORTO_APPROVATO") = Format(totaliApprovati, "##,##0.00")
            RIGA.Item("FORNITORE") = ""
            RIGA.Item("REPERTORIO") = ""
            dt.Rows.Add(RIGA)

            lettoreOdl.Close()
            If dt.Rows.Count > 2 Then
                DataGrid1.Visible = True
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                For Each di As DataGridItem In DataGrid1.Items
                    If di.Cells(0).Text.Contains("TOTALE VOCE") Then
                        di.ForeColor = Drawing.Color.Red
                        di.BackColor = Drawing.Color.Lavender
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    ElseIf di.Cells(0).Text.Contains("TOTALE VOCI") Then
                        di.ForeColor = Drawing.Color.Black
                        di.BackColor = Drawing.Color.Ivory
                        For j As Integer = 0 To di.Cells.Count - 1
                            di.Cells(j).Font.Bold = True
                        Next
                    End If
                Next
            Else
                DataGrid1.Visible = False
                btnExport.Visible = False
                btnStampaPDF.Visible = False
                lblErrore.Text = "La ricerca di ODL per struttura ed esercizio finanziario selezionati non ha prodotto risultati"
            End If

            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            Response.Write(ex.Message)
            '*****************CHIUSURA CONNESSIONE***************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    
    Public Function eliminaLink(ByVal html As String) As String
        Dim check As Integer = 0
        Dim nuovoHtml_parte1 As String = ""
        Dim nuovoHtml_parte2 As String = ""
        Dim nuovoHtml As String = html
        While check = 0
            Dim inizioStringa As Integer = 0
            Dim fineStringa As Integer = nuovoHtml.IndexOf("<a href")
            If fineStringa <> -1 Then
                nuovoHtml_parte1 = nuovoHtml.Substring(inizioStringa, fineStringa)
                nuovoHtml_parte2 = nuovoHtml.Substring(fineStringa + 1)
                Dim finestringa_parte2 As Integer = nuovoHtml_parte2.IndexOf(">")
                nuovoHtml = Replace(nuovoHtml_parte1 & nuovoHtml_parte2.Substring(finestringa_parte2 + 1), "</a>", "")
            Else
                check = 1
            End If
        End While
        Return nuovoHtml
    End Function

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        '#### EXPORT IN EXCEL ####

        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow
            Dim datatable As Data.DataTable
            datatable = dt
            sNomeFile = "ODL_" & Format(Now, "yyyyMMddHHmmss")
            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
                .SetColumnWidth(1, 1, 20)
                .SetColumnWidth(2, 2, 70)
                .SetColumnWidth(3, 3, 20)
                .SetColumnWidth(4, 4, 22)
                .SetColumnWidth(5, 5, 22)
                .SetColumnWidth(6, 6, 22)
                .SetColumnWidth(7, 7, 50)
                .SetColumnWidth(8, 8, 20)
                K = 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont3, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, lblTitolo.Text)
                K = K + 1
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "VOCE")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "NUM. ODL")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "IMPORTO PRENOTATO")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "DATA PRENOTAZIONE")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO APPROVATO")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "FORNITORE")
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "REPERTORIO")
                K = K + 1
                For Each row In datatable.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, row.Item("CODICE"), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, row.Item("VOCE"), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, eliminaLink(row.Item("N_ODL")), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, row.Item("IMPORTO_PRENOTATO"), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, row.Item("DATA_PRENOTAZIONE"), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, row.Item("IMPORTO_APPROVATO"), 4)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, row.Item("FORNITORE"), 0)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, row.Item("REPERTORIO"), 0)

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\..\..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
            Response.Redirect("..\..\..\FileTemp\" & sNomeFile & ".zip")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnStampaPDF_Click(sender As Object, e As System.EventArgs) Handles btnStampaPDF.Click
        Try

            Dim NomeFile As String = "ODL_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            Me.DataGrid1.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = stringWriter.ToString
            Html = eliminaLink(Html)

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
            Dim pdfConverter As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If

            pdfConverter.PageWidth = "1250"
            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter.PdfHeaderOptions.HeaderText = lblTitolo.Text
            pdfConverter.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter.PdfHeaderOptions.HeaderTextColor = Color.Black
            pdfConverter.PdfDocumentOptions.ShowHeader = True
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False
            pdfConverter.PdfDocumentOptions.ShowFooter = True
            pdfConverter.SavePdfFromHtmlStringToFile(Html, url & NomeFile)
            DataGrid1.HeaderStyle.ForeColor = Color.White
            Response.Write("<script>window.open('..\\..\\..\\FileTemp\\" & NomeFile & "','Stampe');</script>")
        Catch ex As Exception

        End Try
    End Sub
End Class
