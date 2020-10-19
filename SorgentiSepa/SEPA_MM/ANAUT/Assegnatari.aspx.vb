

Partial Class ANAUT_Assegnatari
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'Dim Str As String

        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"

        'Response.Write(Str)

        If Not IsPostBack Then
            ' Response.Flush()
            LBLID.Value = "-1"
            CercaAperte()
        End If
    End Sub

    Private Function CercaAperte()


        sStringaSQL1 = "SELECT id,DESCRIZIONE,TO_CHAR(TO_DATE(DATA_INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO,TO_CHAR(TO_DATE(DATA_FINE,'YYYYmmdd'),'DD/MM/YYYY') as DATA_FINE,ANNO_AU,ANNO_ISEE FROM UTENZA_BANDI where UTENZA_BANDI.stato=1 ORDER BY ID DESC"

        BindGrid()
    End Function

    Private Function Cerca()


        sStringaSQL1 = "SELECT id,DESCRIZIONE,TO_CHAR(TO_DATE(DATA_INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO,TO_CHAR(TO_DATE(DATA_FINE,'YYYYmmdd'),'DD/MM/YYYY') as DATA_FINE,ANNO_AU,ANNO_ISEE FROM UTENZA_BANDI ORDER BY ID DESC"

        BindGrid()
    End Function


    Private Sub BindGrid()

        par.OracleConn.Open()
        Dim dt As New System.Data.DataTable

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_BANDI")
        da.Fill(dt)

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        'HttpContext.Current.Session.Add("AA1", dt)


        Label9.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count


        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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

    'Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
    '    Dim myExcelFile As New CM.ExcelFile
    '    Dim i As Long
    '    Dim K As Long
    '    Dim sNomeFile As String
    '    Dim row As System.Data.DataRow
    '    Dim dt As New System.Data.DataTable

    '    dt = CType(HttpContext.Current.Session.Item("AA1"), Data.DataTable)
    '    HttpContext.Current.Session.Remove("AA1")

    '    sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

    '    i = 0

    '    With myExcelFile

    '        .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
    '        .PrintGridLines = False
    '        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
    '        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
    '        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
    '        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
    '        .SetDefaultRowHeight(14)
    '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
    '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
    '        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
    '        .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COGNOME", 12)
    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOME", 12)
    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COD.CONTRATTO", 12)
    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "FILIALE", 12)
    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DATA DIFFIDA", 12)
    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DATA ESITO", 12)
    '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "ESITO", 12)

    '        K = 2
    '        For Each row In dt.Rows
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COGNOME"), "")))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME"), "")))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILIALE"), "")))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_GENERAZIONE"), "")))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfEmpty(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_ESITO"), "")), ""))
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfEmpty(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")), ""))
    '            i = i + 1
    '            K = K + 1
    '        Next

    '        .CloseFile()
    '    End With

    '    Dim objCrc32 As New Crc32()
    '    Dim strmZipOutputStream As ZipOutputStream
    '    Dim zipfic As String

    '    zipfic = Server.MapPath("..\Filetemp\" & sNomeFile & ".zip")

    '    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '    strmZipOutputStream.SetLevel(6)
    '    '
    '    Dim strFile As String
    '    strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
    '    Dim strmFile As FileStream = File.OpenRead(strFile)
    '    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '    '
    '    strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '    Dim sFile As String = Path.GetFileName(strFile)
    '    Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '    Dim fi As New FileInfo(strFile)
    '    theEntry.DateTime = fi.LastWriteTime
    '    theEntry.Size = strmFile.Length
    '    strmFile.Close()
    '    objCrc32.Reset()
    '    objCrc32.Update(abyBuffer)
    '    theEntry.Crc = objCrc32.Value
    '    strmZipOutputStream.PutNextEntry(theEntry)
    '    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '    strmZipOutputStream.Finish()
    '    strmZipOutputStream.Close()
    '    File.Delete(strFile)
    '    Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")
    'End Sub




    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)
        Catch ex As Exception
            
        End Try
    End Sub

    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptMsg').text('" & Messaggio & "');")
            sb.Append("$('#ScriptMsg').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');self.close();}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.Cells(1).Text <> "DESCRIZIONE" Then
            If e.Item.ItemType = ListItemType.Item Then
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#eeeeee'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
            Else
                '---------------------------------------------------         
                ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
                '---------------------------------------------------         
                e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#ffffc0'}")
                e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='#dcdcdc'}")
                e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';")
            End If
        End If
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged
        
    End Sub

    Protected Sub rdbSolo_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSolo.CheckedChanged
        CercaAperte()
    End Sub

    Protected Sub rdbSolo0_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbSolo0.CheckedChanged
        Cerca()
    End Sub
End Class
