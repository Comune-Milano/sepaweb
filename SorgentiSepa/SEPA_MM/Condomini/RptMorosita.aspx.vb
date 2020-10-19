Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RptMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try



            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            End If

            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"


            Response.Write(Str)


            If Not IsPostBack Then
                Cerca()
                Response.Flush()

            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub Cerca()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If Request.QueryString("IDMOROSITA") <> "" Then

                par.cmd.CommandText = "SELECT TIPOLOGIA,DENOMINAZIONE,NOME as CITTA FROM SISCOM_MI.CONDOMINI, COMUNI_NAZIONI WHERE CONDOMINI.ID = " & Request.QueryString("IDCONDOMINIO") & " AND COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
                Dim Reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If Reader.Read Then
                    Me.lblTitle.Text = "CONDOMINIO : " & Reader("DENOMINAZIONE") & " - " & Reader("CITTA")
                    'TipoCond = Reader("TIPOLOGIA")

                End If
                Reader.Close()



                par.cmd.CommandText = "SELECT COND_MOROSITA.*, (COND_AMMINISTRATORI.COGNOME||' '||COND_AMMINISTRATORI.NOME)AS AMMINISTRATORE FROM SISCOM_MI.COND_MOROSITA," _
                    & " SISCOM_MI.COND_AMMINISTRATORI WHERE COND_MOROSITA.ID= " & Request.QueryString("IDMOROSITA") & " AND COND_AMMINISTRATORI.ID = COND_MOROSITA.ID_AMMINISTRATORE "
                Reader = par.cmd.ExecuteReader()
                If Reader.Read Then
                    Me.lblPeriodo.Text = "Morosità condominiale a consuntivo dal: " & par.IfNull(par.FormattaData(par.IfNull(Reader("RIF_DA"), "")), "- -") & " al: " & par.IfNull(par.FormattaData(par.IfNull(Reader("RIF_A"), "")), "- -")
                    Me.lblTitle.Text = lblTitle.Text & "</br>Amministratore: " & par.IfNull(Reader("AMMINISTRATORE"), "- -")
                    Me.lblTitle.Text = lblTitle.Text & "</br>Richiesta del: " & par.FormattaData(par.IfNull(Reader("DATA_RICHIESTA"), ""))
                End If
                Reader.Close()

                par.cmd.CommandText = "SELECT COND_UI.POSIZIONE_BILANCIO,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END AS ""INTESTATARIO"",TRIM(TO_CHAR(IMPORTO,'9G999G999G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_MOROSITA_INQUILINI, SISCOM_MI.ANAGRAFICA,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.COND_UI WHERE ANAGRAFICA.ID = COND_MOROSITA_INQUILINI.ID_INTESTATARIO AND COND_MOROSITA_INQUILINI.ID_UI= UNITA_IMMOBILIARI.ID AND UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND COND_UI.ID_UI = COND_MOROSITA_INQUILINI.ID_UI   AND ID_MOROSITA = " & Request.QueryString("IDMOROSITA") & " AND ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & " ORDER BY INTESTATARIO ASC"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)
                Dim Tot As Double = 0
                Dim row As Data.DataRow
                For Each row In dt.Rows

                    Tot = Tot + par.IfEmpty(row.Item("IMPORTO").ToString.Replace(".", ""), 0)

                Next

                row = dt.NewRow
                row.Item("POSIZIONE_BILANCIO") = "T O T A L E"
                row.Item("INTERNO") = "---"
                row.Item("SCALA") = "---"
                row.Item("INTESTATARIO") = "---"
                row.Item("IMPORTO") = Format(Tot, "##,##0.00")
                dt.Rows.Add(row)

                DataGridMorosita.DataSource = dt
                DataGridMorosita.DataBind()




            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("MIADT", dt)
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "CondExp_" & Format(Now, "yyyyMMddHHmmss")

            i = 0

            With myExcelFile

                .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE A BILANCIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "INTESTATARIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "IMPORTO", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO"), "")))

                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            'Dim objCrc32 As New Crc32()
            'Dim strmZipOutputStream As ZipOutputStream
            'Dim zipfic As String

            'zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            'strmZipOutputStream.SetLevel(6)
            ''
            'Dim strFile As String
            'strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            'Dim strmFile As FileStream = File.OpenRead(strFile)
            'Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            ''
            'strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            'Dim sFile As String = Path.GetFileName(strFile)
            'Dim theEntry As ZipEntry = New ZipEntry(sFile)
            'Dim fi As New FileInfo(strFile)
            'theEntry.DateTime = fi.LastWriteTime
            'theEntry.Size = strmFile.Length
            'strmFile.Close()
            'objCrc32.Reset()
            'objCrc32.Update(abyBuffer)
            'theEntry.Crc = objCrc32.Value
            'strmZipOutputStream.PutNextEntry(theEntry)
            'strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            'strmZipOutputStream.Finish()
            'strmZipOutputStream.Close()

            'File.Delete(strFile)
            Response.Redirect("..\/FileTemp\/" & sNomeFile & ".xls")

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExport0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport0.Click
        Dim Html As String = ""
        Dim stringWriter As New System.IO.StringWriter
        Dim sourcecode As New HtmlTextWriter(stringWriter)

        Try
            DataGridMorosita.RenderControl(sourcecode)
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

            Dim nomefile As String = "Exp_Morosita_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Me.lblTitle.Text & "<br/>" & Me.lblPeriodo.Text & "<br/>" & Html, url & nomefile)
            Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpMorosita','');</script>")

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
End Class
