
Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Contabilita_RisultatoLog
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim datadal As String
    Dim dataal As String
    Dim Elaborazioni As String = ""
    Dim dt As New Data.DataTable
    Dim sStringaSql As String

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
            Elaborazioni = Request.QueryString("E")
            Cerca()
            Response.Flush()
        End If
    End Sub

    

    Private Sub Cerca()
        Dim bTrovato As Boolean
        'Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""


        If datadal <> "" Then
            sCompara = " >= "
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.RENDICONTAZIONE.DATA_LOG" & sCompara & " '" & datadal & "' "
            ' sStringaSql = sStringaSql & " SISCOM_MI.MAV_ERRORI.DATA_ORA " & sCompara & " '" & par.PulisciStrSql(dataal) & "%' "
        End If

        If dataal <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sCompara = " <= "
            bTrovato = True
            sStringaSql = sStringaSql & "SISCOM_MI.RENDICONTAZIONE.DATA_LOG" & sCompara & " '" & dataal & "' "

        End If


        'If bolletta <> "" Then
        '    If bTrovato = True Then sStringaSql = sStringaSql & " AND "
        '    sCompara = " = "

        '    bTrovato = True
        '    sStringaSql = sStringaSql & " SISCOM_MI.RENDICONTAZIONE.ID_BOLLETTA " & sCompara & " '" & par.PulisciStrSql(bolletta) & "' "
        'End If




        sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(SISCOM_MI.RENDICONTAZIONE.DATA_LOG,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA"", SUBSTR(SISCOM_MI.RENDICONTAZIONE.ORA_LOG,1,2)||':'||SUBSTR(SISCOM_MI.RENDICONTAZIONE.ORA_LOG,3,2)||':'||SUBSTR(SISCOM_MI.RENDICONTAZIONE.ORA_LOG,5,2) AS ""ORA"", SISCOM_MI.RENDICONTAZIONE.LOG FROM SISCOM_MI.RENDICONTAZIONE"


        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY DATA_LOG DESC, ORA_log DESC "

        dt.Columns.Add("DATA")
        dt.Columns.Add("ORA")
        dt.Columns.Add("LOG")

        Dim RIGA As System.Data.DataRow

        par.OracleConn.Open()
        Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSQL1, par.OracleConn)
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
        While myReader.Read()
            If Elaborazioni = "1" Then
                If InStr(UCase(par.IfNull(myReader("LOG"), " ")), "ELABORAZIONE FILE:") > 0 Then
                    RIGA = dt.NewRow()
                    RIGA.Item("DATA") = par.IfNull(myReader("DATA"), " ")
                    RIGA.Item("ORA") = par.IfNull(myReader("ORA"), " ")
                    RIGA.Item("LOG") = "<a href='ElencoBollette.aspx?F=" & par.Cripta(par.IfNull(myReader("LOG"), " ")) & "' target='_blank'>" & par.IfNull(myReader("LOG"), " ") & "</a>"
                    dt.Rows.Add(RIGA)
                End If
            Else
                RIGA = dt.NewRow()
                RIGA.Item("DATA") = par.IfNull(myReader("DATA"), " ")
                RIGA.Item("ORA") = par.IfNull(myReader("ORA"), " ")
                If InStr(UCase(par.IfNull(myReader("LOG"), " ")), "ELABORAZIONE FILE:") > 0 Then
                    RIGA.Item("LOG") = "<a href=""javascript:window.open('ElencoBollette.aspx?F=" & par.Cripta(par.IfNull(myReader("LOG"), " ")) & "','ElencoBollette','fullscreen=0,resizable=1,statusbar=0,width=1000,height=800');void(0);"">" & par.IfNull(myReader("LOG"), " ") & "</a>"
                Else
                    RIGA.Item("LOG") = par.IfNull(myReader("LOG"), " ")
                End If
                dt.Rows.Add(RIGA)
            End If


        End While
        'Label3.Text = "0"
        'Do While myReader.Read()
        'Label3.Text = CInt(Label3.Text) + 1
        'Loop
        'Label3.Text = Label3.Text

        Session.Add("MIADT", dt)
        DataGrid1.DataSource = dt
        DataGrid1.DataBind()

        Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & dt.Rows.Count

        cmd.Dispose()
        myReader.Close()
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
        Response.Write("<script>document.location.href=""LogRendicontazione.aspx""</script>")
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "DATA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "ORA", 0)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "LOG", 0)


                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ORA"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("LOG"), 0)))


                    i = i + 1
                    K = K + 1
                Next

                .CloseFile()
            End With

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            '
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
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
            Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")


        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class