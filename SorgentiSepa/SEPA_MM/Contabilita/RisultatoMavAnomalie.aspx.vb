Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contabilita_RisultatoMavAnomalie
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim datadal As String
    Dim dataal As String
    Dim datValutaadal As String
    Dim datValutaaal As String

    Dim nfile As String
    Dim bolletta As String
    Dim nConto As String
    Dim sStringaSql As String
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:250px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()

            datadal = UCase(Request.QueryString("DAL"))
            dataal = UCase(Request.QueryString("AL"))
            nfile = UCase(Request.QueryString("FI"))
            bolletta = UCase(Request.QueryString("BO"))
            nConto = UCase(Request.QueryString("NCONTO"))
            datValutaadal = UCase(Request.QueryString("VALUTADAL"))
            datValutaaal = UCase(Request.QueryString("VALUTAAL"))
            Cerca()
            Response.Flush()
        End If
    End Sub

    Private Sub Cerca(Optional esporta As Boolean = False)
        Dim bTrovato As Boolean
        'Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""


        If datadal <> "" Then
            sCompara = " >= "
            bTrovato = True
            sStringaSql = sStringaSql & "SUBSTR(SISCOM_MI.MAV_ANOMALIE.DATA_ORA,1,8)" & sCompara & " '" & datadal & "' "
            ' sStringaSql = sStringaSql & " SISCOM_MI.MAV_ANOMALIE.DATA_ORA " & sCompara & " '" & par.PulisciStrSql(dataal) & "%' "
        End If

        If dataal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sCompara = " <= "
            bTrovato = True
            sStringaSql = sStringaSql & "SUBSTR(SISCOM_MI.MAV_ANOMALIE.DATA_ORA,1,8)" & sCompara & " '" & dataal & "' "

        End If

        If datValutaadal <> "" Then
            sCompara = " >= "
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.MAV_ANOMALIE.DATA_VALUTA" & sCompara & " '" & datValutaadal & "' "
            ' sStringaSql = sStringaSql & " SISCOM_MI.MAV_ANOMALIE.DATA_ORA " & sCompara & " '" & par.PulisciStrSql(dataal) & "%' "
        End If

        If datValutaaal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sCompara = " <= "
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.MAV_ANOMALIE.DATA_VALUTA" & sCompara & " '" & datValutaaal & "' "

        End If


        If nfile <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sCompara = " LIKE "
            bTrovato = True
            sStringaSql = sStringaSql & " UPPER(SISCOM_MI.MAV_ANOMALIE.FILE_MAV) " & sCompara & " '%" & par.PulisciStrSql(nfile) & "%' "
        End If


        If bolletta <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sCompara = " LIKE "

            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.MAV_ANOMALIE.ID_BOLLETTA " & sCompara & " '%" & par.PulisciStrSql(bolletta) & "%' "
        End If

        If nConto <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sCompara = " LIKE "

            bTrovato = True
            sStringaSql = sStringaSql & " SISCOM_MI.MAV_ANOMALIE.NUMERO_CONTO " & sCompara & " '%" & par.PulisciStrSql(nConto) & "%' "

        End If


        sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(SUBSTR(SISCOM_MI.MAV_ANOMALIE.DATA_ORA,1,8),'YYYYmmdd'),'DD/MM/YYYY')||' '||SUBSTR(SISCOM_MI.MAV_ANOMALIE.DATA_ORA,10,2)||':'||SUBSTR(SISCOM_MI.MAV_ANOMALIE.DATA_ORA,12,2)||':'||SUBSTR(SISCOM_MI.MAV_ANOMALIE.DATA_ORA,14,2) AS ""DATA_ORA"", SISCOM_MI.MAV_ANOMALIE.FILE_MAV AS ""FILE"", SISCOM_MI.MAV_ANOMALIE.DESCRIZIONE, SISCOM_MI.MAV_ANOMALIE.ID_BOLLETTA AS ""BOLLETTA"", SISCOM_MI.MAV_ANOMALIE.IMPORTO,TO_CHAR(TO_DATE(DATA_PAGAMENTO,'yyyymmdd'),'DD/MM/YYYY') AS DATA_PAGAMENTO,TO_CHAR(TO_DATE(data_valuta,'yyyymmdd'),'DD/MM/YYYY') AS data_valuta,SISCOM_MI.MAV_ANOMALIE.NUMERO_CONTO FROM SISCOM_MI.MAV_ANOMALIE"


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DATA_ORA DESC, id_bolletta DESC"

        'dt.Columns.Add("DATA E ORA")
        'dt.Columns.Add("FILE")
        'dt.Columns.Add("DESCRIZIONE")
        'dt.Columns.Add("BOLLETTA")
        'dt.Columns.Add("IMPORTO")

        'Dim RIGA As System.Data.DataRow

        par.OracleConn.Open()
        par.SettaCommand(par)
        par.cmd.CommandText = sStringaSQL1
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
        Dim dt As New Data.DataTable
        da.Fill(dt)

        If esporta = True Then
            If dt.Rows.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(dt, DataGrid1, "ExportAnomalieMav", 90 / 100, , , False)
                If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                    Response.Redirect("..\/FileTemp\/" & nomefile, False)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!');</script>")
            End If
        Else
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()

        End If


        Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count

        par.OracleConn.Close()
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        'LBLID.Text = e.Item.Cells(0).Text
        'Label2.Text = "Hai selezionato: " & e.Item.Cells(1).Text & " " & e.Item.Cells(2).Text

    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
        End If

    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            Cerca()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""AnomalieRendicontazione.aspx""</script>")
    End Sub
    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        'Try


        '    'Dim myExcelFile As New CM.ExcelFile
        '    'Dim i As Long
        '    'Dim K As Long
        '    'Dim sNomeFile As String
        '    'Dim row As System.Data.DataRow

        '    'dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
        '    'sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

        '    'i = 0

        '    'With myExcelFile

        '    '    .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
        '    '    .PrintGridLines = False
        '    '    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
        '    '    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
        '    '    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
        '    '    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
        '    '    .SetDefaultRowHeight(14)
        '    '    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
        '    '    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
        '    '    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
        '    '    .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


        '    '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "DATA E ORA", 0)
        '    '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "FILE", 0)
        '    '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DESCRIZIONE", 0)
        '    '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "BOLLETTA", 0)
        '    '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "IMPORTO", 0)

        '    '    K = 2
        '    '    For Each row In dt.Rows
        '    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA E ORA"), 0)))
        '    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILE"), 0)))
        '    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), 0)))
        '    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("BOLLETTA"), 0)))
        '    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO"), 0)))

        '    '        i = i + 1
        '    '        K = K + 1
        '    '    Next

        '    '    .CloseFile()
        '    'End With

        '    'Dim objCrc32 As New Crc32()
        '    'Dim strmZipOutputStream As ZipOutputStream
        '    'Dim zipfic As String

        '    'zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

        '    'strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        '    'strmZipOutputStream.SetLevel(6)
        '    ''
        '    'Dim strFile As String
        '    'strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
        '    'Dim strmFile As FileStream = File.OpenRead(strFile)
        '    'Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '    ''
        '    'strmFile.Read(abyBuffer, 0, abyBuffer.Length)

        '    'Dim sFile As String = Path.GetFileName(strFile)
        '    'Dim theEntry As ZipEntry = New ZipEntry(sFile)
        '    'Dim fi As New FileInfo(strFile)
        '    'theEntry.DateTime = fi.LastWriteTime
        '    'theEntry.Size = strmFile.Length
        '    'strmFile.Close()
        '    'objCrc32.Reset()
        '    'objCrc32.Update(abyBuffer)
        '    'theEntry.Crc = objCrc32.Value
        '    'strmZipOutputStream.PutNextEntry(theEntry)
        '    'strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        '    'strmZipOutputStream.Finish()
        '    'strmZipOutputStream.Close()

        '    'File.Delete(strFile)
        '    'Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")


        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try

        Try
            If DataGrid1.Items.Count > 0 Then
                Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGrid1, "ExportAnomalieMav", 90 / 100, , , False)
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
End Class
