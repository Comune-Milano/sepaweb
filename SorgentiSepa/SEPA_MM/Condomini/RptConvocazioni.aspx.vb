Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Condomini_RptConvocazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property datatableRptConvocazioni() As Data.DataTable
        Get
            If Not (ViewState("datatableRptConvocazioni") Is Nothing) Then
                Return ViewState("datatableRptConvocazioni")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("datatableRptConvocazioni") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        'Dim TOTPerc As Double = 0
        Response.Write(Str)
        If Not IsPostBack Then
            Response.Flush()
            RiempiDataGrid()
        End If
    End Sub
    Private Sub RiempiDataGrid()
        Dim TOTMill As Double = 0
        Dim row As Data.DataRow
        Try
            '********CONNESSIONE OPEN*********
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT cond_convocazioni.ID,PROTOCOLLO_ALER,ID_CONDOMINIO,DATA_CONVOCAZIONE AS DATA_ORDINA, to_char(to_date(DATA_CONVOCAZIONE,'yyyymmdd'),'dd/mm/yyyy') AS DATA_CONVOCAZIONE,(SUBSTR(ORA_CONVOCAZIONE,0,2)||':'|| SUBSTR(ORA_CONVOCAZIONE,3,4))AS ORA, (CASE (TIPO) WHEN 'O' THEN 'ORD' WHEN 'S' THEN 'STR' WHEN 'C' THEN 'COST' END) AS TIPO, CONDOMINI.DENOMINAZIONE,CONDOMINI.TIPOLOGIA,COMUNI.COMU_DESCR AS COMUNE, to_char(to_date(NVL(DATA_ARRIVO_ALER,DATA_ARRIVO),'yyyymmdd'),'dd/mm/yyyy') AS DATA_ARRIVO,'' AS PERCENTUALE,TRIM(TO_CHAR(cond_convocazioni.millesimi,'9G999G990D9999')) as MILL,COND_CONVOCAZIONI.ALTRE_PRESENZE,COND_CONVOCAZIONI.DELEGATO,(COND_AMMINISTRATORI.COGNOME||' '||COND_AMMINISTRATORI.NOME) AS AMMRE FROM SISCOM_MI.COND_CONVOCAZIONI, SISCOM_MI.CONDOMINI,COMUNI,SISCOM_MI.COND_AMMINISTRATORI WHERE COND_CONVOCAZIONI.ID_AMMINISTRATORE = COND_AMMINISTRATORI.ID AND COND_CONVOCAZIONI.ID_CONDOMINIO = CONDOMINI.ID AND CONDOMINI.COD_COMUNE = COMUNI.COMU_COD "
            If Request.QueryString("DAL") <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " AND DATA_CONVOCAZIONE >= " & Request.QueryString("DAL")
            End If
            If Request.QueryString("AL") <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " AND DATA_CONVOCAZIONE <= " & Request.QueryString("AL")
            End If
            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY DATA_ORDINA,ora ,DELEGATO ASC"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            datatableRptConvocazioni = New Data.DataTable
            da.Fill(datatableRptConvocazioni)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            For Each row In datatableRptConvocazioni.Rows
                If row.Item("TIPOLOGIA") = "C" Then
                    'par.cmd.CommandText = "SELECT TRIM(TO_CHAR(SUM(MIL_PRO),'9999990D9999'))AS MIL_PRO FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & row.Item("ID_CONDOMINIO")
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then
                    '    row.Item("MILL") = par.IfNull(myReader1("MIL_PRO"), "")
                    'TOTMill = TOTMill + CDbl(par.IfNull(myReader1("MIL_PRO"), 0))
                    'End If
                    'myReader1.Close()
                    par.cmd.CommandText = "SELECT PERC_MILLEISMI_PRO FROM SISCOM_MI.COND_CONVOCAZIONI WHERE ID = '" & row.Item("ID") & "'"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        row.Item("PERCENTUALE") = par.IfNull(myReader1("PERC_MILLEISMI_PRO"), "")
                    End If
                    myReader1.Close()
                    TOTMill = TOTMill + CDbl(par.IfNull(row.Item("MILL"), 0))
                ElseIf row.Item("TIPOLOGIA") = "S" Then
                    'par.cmd.CommandText = "SELECT TO_CHAR(SUM(MIL_COMPRO),'9999990D9999')AS MIL_COMPRO FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & row.Item("ID_CONDOMINIO")
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then
                    '    row.Item("MILL") = par.IfNull(myReader1("MIL_COMPRO"), "")
                    '    TOTMill = TOTMill + CDbl(par.IfNull(myReader1("MIL_COMPRO"), 0))
                    'End If
                    'myReader1.Close()
                    par.cmd.CommandText = "SELECT PERC_MILLESIMI_CONPRO FROM SISCOM_MI.COND_CONVOCAZIONI WHERE ID = '" & row.Item("ID") & "'"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        row.Item("PERCENTUALE") = par.IfNull(myReader1("PERC_MILLESIMI_CONPRO"), "")
                    End If
                    myReader1.Close()
                    TOTMill = TOTMill + CDbl(par.IfNull(row.Item("MILL"), 0))
                ElseIf row.Item("TIPOLOGIA") = "T" Then
                    'par.cmd.CommandText = "SELECT TO_CHAR(SUM(MIL_RISC),'9999990D9999')AS MIL_RISC FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & row.Item("ID_CONDOMINIO")
                    'myReader1 = par.cmd.ExecuteReader()
                    'If myReader1.Read Then
                    '    row.Item("MILL") = par.IfNull(myReader1("MIL_RISC"), "")
                    '    TOTMill = TOTMill + CDbl(par.IfNull(myReader1("MIL_RISC"), 0))
                    'End If
                    'myReader1.Close()
                    par.cmd.CommandText = "SELECT PERC_SUPEFICI FROM SISCOM_MI.COND_CONVOCAZIONI WHERE ID = '" & row.Item("ID") & "'"
                    myReader1 = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        row.Item("PERCENTUALE") = par.IfNull(myReader1("PERC_SUPEFICI"), "")
                    End If
                    myReader1.Close()
                    TOTMill = TOTMill + CDbl(par.IfNull(row.Item("MILL"), 0))
                End If
            Next
            BindGrid()
            'Session.Add("MIADT", dt)
            '********CONNESSIONE CLOSED*********
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub BindGrid()
        Try
            DataGridRPTConv.DataSource = datatableRptConvocazioni
            DataGridRPTConv.DataBind()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Esporta()
    End Sub
    Private Sub Esporta()
        Try
            If DataGridRPTConv.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRPTConv, "ExportRptConvocazioni", 90 / 100, , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " " & ex.Message)
            Response.Write("<script>parent.location.href=""../Errore.aspx"";</script>")
        End Try
    End Sub
    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        'PrintPdf()
        Response.Write("<script>window.open('../FileTemp/" & par.StampaDataGridPDF(Me.DataGridRPTConv, "RptConvocazioni", "REPORT CONVOCAZIONI CONDOMINIALI", , , True, True) & "','Rateizza','');</script>")
    End Sub
    'Private Sub PrintPdf()
    '    Dim Html As String = ""
    '    Dim stringWriter As New System.IO.StringWriter
    '    Dim sourcecode As New HtmlTextWriter(stringWriter)
    '    Try
    '        Me.DataGridRPTConv.RenderControl(sourcecode)
    '        sourcecode.Flush()
    '        Html = stringWriter.ToString
    '        Dim url As String = Server.MapPath("..\FileTemp\")
    '        Dim pdfConverter1 As PdfConverter = New PdfConverter
    '        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
    '        If Licenza <> "" Then
    '            pdfConverter1.LicenseKey = Licenza
    '        End If
    '        pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
    '        pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
    '        pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
    '        pdfConverter1.PdfDocumentOptions.ShowHeader = True
    '        pdfConverter1.PdfDocumentOptions.ShowFooter = True
    '        pdfConverter1.PdfDocumentOptions.LeftMargin = 10
    '        pdfConverter1.PdfDocumentOptions.RightMargin = 5
    '        pdfConverter1.PdfDocumentOptions.TopMargin = 5
    '        pdfConverter1.PdfDocumentOptions.BottomMargin = 5
    '        pdfConverter1.PdfHeaderOptions.HeaderHeight = 25
    '        pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
    '        pdfConverter1.PdfHeaderOptions.HeaderText = "REPORT CONVOCAZIONI CONDOMINIALI"
    '        pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
    '        pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
    '        pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold
    '        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
    '        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10
    '        pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
    '        pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
    '        pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
    '        pdfConverter1.PdfFooterOptions.FooterText = ("")
    '        pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
    '        pdfConverter1.PdfFooterOptions.DrawFooterLine = False
    '        pdfConverter1.PdfFooterOptions.PageNumberText = "pag."
    '        pdfConverter1.PdfFooterOptions.ShowPageNumber = True
    '        Dim nomefile As String = "RptConvocazioni_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
    '        pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
    '        Response.Write("<script>window.open('../FileTemp/" & nomefile & "','Rateizza','');</script>")
    '    Catch ex As Exception

    '    End Try
    'End Sub
End Class
