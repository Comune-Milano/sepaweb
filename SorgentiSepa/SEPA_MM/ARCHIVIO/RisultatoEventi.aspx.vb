Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ARCHIVIO_RisultatoEventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:500px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"
        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            Cerca()
        End If
    End Sub

    Private Function Cerca()
        Try
            StringaSQL = "select EVENTI_CONTRATTI_ARCHIVIO.ID_OPERATORE, tab_eventi.descrizione as evento, SUBSTR(EVENTI_CONTRATTI_ARCHIVIO.DATA_ORA,7,2)||'/'||SUBSTR(EVENTI_CONTRATTI_ARCHIVIO.DATA_ORA,5,2)||'/'||SUBSTR(EVENTI_CONTRATTI_ARCHIVIO.DATA_ORA,1,4)||' - '||SUBSTR(EVENTI_CONTRATTI_ARCHIVIO.DATA_ORA,9,2)||':'||SUBSTR(EVENTI_CONTRATTI_ARCHIVIO.DATA_ORA,11,2) AS ""DATA_ORA"", EVENTI_CONTRATTI_ARCHIVIO.MOTIVAZIONE, operatori.ID, operatori.OPERATORE, siscom_mi.tab_eventi.COD, siscom_mi.tab_eventi.DESCRIZIONE" _
                                    & " from SISCOM_MI.EVENTI_CONTRATTI_ARCHIVIO, operatori, siscom_mi.tab_eventi" _
                                    & " where EVENTI_CONTRATTI_ARCHIVIO.ID_OPERATORE = operatori.ID AND EVENTI_CONTRATTI_ARCHIVIO.COD_EVENTO = siscom_mi.tab_eventi.COD "

            If Request.QueryString("OPERATORE") <> -1 Then
                StringaSQL = StringaSQL & " and id_operatore = " & Request.QueryString("OPERATORE") & ""
            End If
            If Request.QueryString("DATADAL") <> "000000" Then
                StringaSQL = StringaSQL & " AND data_ora > " & Request.QueryString("DATADAL") & ""
            End If
            If Request.QueryString("DATAAL") <> "000000" Then
                StringaSQL = StringaSQL & " AND data_ora < " & Request.QueryString("DATAAL") & ""
            End If

            If Request.QueryString("OP") <> "TUTTI" Then
                '
                StringaSQL = StringaSQL & " AND COD_EVENTO = '" & Request.QueryString("OP") & "' "
            End If

            StringaSQL = StringaSQL & " order by EVENTI_CONTRATTI_ARCHIVIO.DATA_ORA desc"
            BindGrid()
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Public Property StringaSQL() As String
        Get
            If Not (ViewState("par_sStringaSQL") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL") = value
        End Set

    End Property

    Private Sub BindGrid()
        Try
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(StringaSQL, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "EVENTI_CONTRATTI_ARCHIVIO")
            Datagrid2.DataSource = ds
            Datagrid2.DataBind()
            Label4.Text = "  - " & Datagrid2.Items.Count & " nella pagina - Totale: " & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ds.Tables(0).Rows.Count = 0 Then
                Response.Write("<script>alert('La ricerca non ha prodotto risultati!');document.location.href=""RicercaEventi.aspx""</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        ExportXLS_Chiama100()
    End Sub

    Private Function ExportXLS_Chiama100()

        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim dt As New Data.DataTable
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try
            par.OracleConn.Open()
            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter

            da = New Oracle.DataAccess.Client.OracleDataAdapter(StringaSQL, par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                i = 0
                With myExcelFile

                    .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
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
                    .SetColumnWidth(1, 1, 30)
                    .SetColumnWidth(2, 2, 20)
                    .SetColumnWidth(3, 3, 30)
                    .SetColumnWidth(4, 4, 15)
                    .SetColumnWidth(5, 5, 45)
                    .SetColumnWidth(6, 6, 20)
                    .SetColumnWidth(7, 7, 45)
                    .SetColumnWidth(8, 8, 20)
                    .SetColumnWidth(9, 9, 25)
                    .SetColumnWidth(10, 10, 20)
                    .SetColumnWidth(11, 11, 25)
                    .SetColumnWidth(12, 12, 20)
                    .SetColumnWidth(13, 13, 20)
                    .SetColumnWidth(14, 14, 20)
                    .SetColumnWidth(15, 15, 55)
                    .SetColumnWidth(16, 16, 60)
                    .SetColumnWidth(17, 17, 30)
                    .SetColumnWidth(18, 18, 20)
                    .SetColumnWidth(19, 19, 35)
                    .SetColumnWidth(20, 20, 20)
                    .SetColumnWidth(21, 21, 25)
                    .SetColumnWidth(22, 22, 20)
                    .SetColumnWidth(23, 23, 20)
                    .SetColumnWidth(24, 24, 20)
                    .SetColumnWidth(25, 25, 20)
                    .SetColumnWidth(26, 26, 20)
                    .SetColumnWidth(27, 27, 20)
                    .SetColumnWidth(28, 28, 20)



                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "DATA/ORA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "OPERATORE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "EVENTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "NOTE", 12)

                    K = 2
                    For Each row In dt.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_ORA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OPERATORE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("EVENTO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MOTIVAZIONE"), "")))
                        i = i + 1
                        K = K + 1
                    Next

                    .CloseFile()
                End With

            End If

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream
            Dim zipfic As String

            zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)

            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
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

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            ' Response.Write("<script>window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');</script>")
            Response.Redirect("..\FileTemp\" & FileCSV & ".zip")

        Catch ex As Exception
            par.OracleConn.Close()

        End Try




    End Function

End Class
