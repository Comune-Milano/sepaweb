Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_RptAppuntamenti
    Inherits PageSetIdMode
    Dim dt As New Data.DataTable
    Dim par As New CM.Global

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

        If Not IsPostBack Then
            Response.Flush()
            Cerca()
            
        End If
    End Sub
    Private Sub Cerca()
        Try

            '********CONNESSIONE OPEN*********
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = " SELECT rapporti_utenza.cod_contratto as COD_CONTR, " _
                                & " TO_CHAR (TO_DATE (SUBSTR (sl_sloggio.data_app_pre_sloggio, 1, 8), 'yyyyMMdd'), 'dd/MM/yyyy') AS DATA_APP_PRESL, " _
                                & " CASE WHEN SUBSTR(sl_sloggio.data_app_pre_sloggio, 9, 2) IS NULL THEN '' ELSE SUBSTR (sl_sloggio.data_app_pre_sloggio, 9, 2) || ':' || SUBSTR (sl_sloggio.data_app_pre_sloggio, 11, 2) END AS ORA_APP_PRESL, " _
                                & " TO_CHAR (TO_DATE (SUBSTR (sl_sloggio.data_app_rapporto_sloggio, 1, 8), 'yyyyMMdd'), 'dd/MM/yyyy') AS DATA_APP_SLOGGIO, " _
                                & " CASE WHEN SUBSTR(sl_sloggio.data_app_rapporto_sloggio, 9, 2) IS NULL THEN '' ELSE SUBSTR (sl_sloggio.data_app_rapporto_sloggio, 9, 2) || ':' || SUBSTR (sl_sloggio.data_app_rapporto_sloggio, 11, 2) END AS ORA_APP_SLOGGIO, " _
                                & " tab_quartieri.nome AS QUARTIERE, comuni_nazioni.nome AS comune, " _
                                & " unita_immobiliari.cod_unita_immobiliare as cod_unita, " _
                                & " (indirizzi.descrizione || ', ' || indirizzi.civico) AS indirizzo " _
                                & " FROM siscom_mi.rapporti_utenza, siscom_mi.unita_contrattuale, siscom_mi.unita_immobiliari, siscom_mi.sl_sloggio, siscom_mi.identificativi_catastali, " _
                                & " siscom_mi.tab_quartieri, comuni_nazioni, siscom_mi.indirizzi, siscom_mi.edifici, siscom_mi.complessi_immobiliari " _
                                & "WHERE unita_contrattuale.id_unita = unita_immobiliari.ID AND rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                                & " And data_disdetta_locatario Is Not NULL " _
                                & " AND sl_sloggio.id_contratto = rapporti_utenza.ID(+) AND (data_app_pre_sloggio IS NOT NULL OR data_app_rapporto_sloggio IS NOT NULL) " _
                                & " AND complessi_immobiliari.ID = edifici.id_complesso AND edifici.ID = unita_immobiliari.id_edificio AND indirizzi.cod_comune = comuni_nazioni.cod(+) " _
                                & " AND complessi_immobiliari.id_quartiere = tab_quartieri.ID AND unita_immobiliari.id_catastale = identificativi_catastali.ID(+) AND unita_immobiliari.id_indirizzo = indirizzi.ID(+) " _
                                & " and unita_immobiliari.id_unita_principale is null and unita_contrattuale.id_unita_principale is null"





            par.cmd.CommandText = par.cmd.CommandText & " ORDER BY quartiere ASC"
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)



            dgvRptAppSL.DataSource = dt
            dgvRptAppSL.DataBind()
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

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try


            Dim nomefile As String = par.EsportaExcelAutomaticoDaDataGrid(Me.dgvRptAppSL, "ExportRptAppuntamentiSL", , , , False)
            If File.Exists(Server.MapPath("..\FileTemp\") & nomefile) Then
                Response.Redirect("..\/FileTemp\/" & nomefile, False)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If


            'Dim myExcelFile As New CM.ExcelFile
            'Dim i As Long
            'Dim K As Long
            'Dim sNomeFile As String
            'Dim row As System.Data.DataRow

            'dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            'sNomeFile = "CondExp_" & Format(Now, "yyyyMMddHHmmss")

            'i = 0

            'With myExcelFile

            '    .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
            '    .PrintGridLines = False
            '    .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
            '    .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
            '    .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
            '    .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
            '    .SetDefaultRowHeight(14)
            '    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
            '    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
            '    .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
            '    .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)
            '    .SetColumnWidth(1, 3, 40)
            '    .SetColumnWidth(4, 4, 20)
            '    .SetColumnWidth(5, 7, 35)
            '    .SetColumnWidth(8, 11, 25)

            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "QUARTIERE", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD. UNITA' IMMOBILIARE", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INDIRIZZO", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "COMUNE", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DATA APP. PRE-SLOGGIO", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "ORA APP. PRE-SLOGGIO", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA APP. SLOGGIO", 12)
            '    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "ORA APP. SLOGGIO", 12)


            '    K = 2
            '    For Each row In dt.Rows
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("QUARTIERE"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_UNITA"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INDIRIZZO"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COMUNE"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_APP_PRESL"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ORA_APP_PRESL"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_APP_SLOGGIO"), "")))
            '        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("ORA_APP_SLOGGIO"), "")))


            '        i = i + 1
            '        K = K + 1
            '    Next

            '    .CloseFile()
            'End With

            ''Dim objCrc32 As New Crc32()
            ''Dim strmZipOutputStream As ZipOutputStream
            ''Dim zipfic As String

            ''zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

            ''strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            ''strmZipOutputStream.SetLevel(6)

            ''Dim strFile As String
            ''strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
            ''Dim strmFile As FileStream = File.OpenRead(strFile)
            ''Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            ' ''
            ''strmFile.Read(abyBuffer, 0, abyBuffer.Length)

            ''Dim sFile As String = Path.GetFileName(strFile)
            ''Dim theEntry As ZipEntry = New ZipEntry(sFile)
            ''Dim fi As New FileInfo(strFile)
            ''theEntry.DateTime = fi.LastWriteTime
            ''theEntry.Size = strmFile.Length
            ''strmFile.Close()
            ''objCrc32.Reset()
            ''objCrc32.Update(abyBuffer)
            ''theEntry.Crc = objCrc32.Value
            ''strmZipOutputStream.PutNextEntry(theEntry)
            ''strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            ''strmZipOutputStream.Finish()
            ''strmZipOutputStream.Close()

            ''  File.Delete(strFile)
            'Response.Redirect("..\/FileTemp\/" & sNomeFile & ".xls", False)

            ''Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>")
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
