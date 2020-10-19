Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Condomini_RptLiberiAbusivi
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Dim dt2 As New Data.DataTable
    Public Property vIdConnModale() As String
        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Request.QueryString("IDCON") <> "" Then
                vIdConnModale = Request.QueryString("IDCON")
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

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


            If Request.QueryString("IDGESTIONE") <> "" Then

                'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_"
                Dim Reader As Oracle.DataAccess.Client.OracleDataReader
                If Request.QueryString("CHIAMA") = "P" Then
                    par.cmd.CommandText = "SELECT DISTINCT CONDOMINI.DENOMINAZIONE, COND_GESTIONE.DATA_INIZIO, COND_GESTIONE.DATA_FINE,NOME as CITTA FROM SISCOM_MI.COND_UI_GESTIONE_P,SISCOM_MI.COND_GESTIONE, SISCOM_MI.CONDOMINI,COMUNI_NAZIONI WHERE COND_UI_GESTIONE_P.ID_GESTIONE = COND_GESTIONE.ID AND COND_GESTIONE.ID_CONDOMINIO = CONDOMINI.ID AND ID_GESTIONE =" & Request.QueryString("IDGESTIONE") & " and COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
                Else
                    par.cmd.CommandText = "SELECT DISTINCT CONDOMINI.DENOMINAZIONE, COND_GESTIONE.DATA_INIZIO, COND_GESTIONE.DATA_FINE,NOME as CITTA FROM SISCOM_MI.COND_UI_GESTIONE_C,SISCOM_MI.COND_GESTIONE, SISCOM_MI.CONDOMINI,COMUNI_NAZIONI WHERE COND_UI_GESTIONE_C.ID_GESTIONE = COND_GESTIONE.ID AND COND_GESTIONE.ID_CONDOMINIO = CONDOMINI.ID AND ID_GESTIONE =" & Request.QueryString("IDGESTIONE") & " and COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
                End If

                Reader = par.cmd.ExecuteReader()
                If Reader.Read Then
                    Me.lblSottotitolo.Text = "Condominio: " & Reader("DENOMINAZIONE") & " - " & Reader("CITTA") & " </br>"
                    If Request.QueryString("CHIAMA") = "P" Then
                        Me.lblSottotitolo.Text += "Gestione: Preventivo </br>Periodo di Gestione : " & par.FormattaData(par.IfNull(Reader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(Reader("data_fine"), ""))
                    Else
                        Me.lblSottotitolo.Text += "Gestione: Consuntivo </br>Periodo di Gestione : " & par.FormattaData(par.IfNull(Reader("DATA_INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(Reader("data_fine"), ""))

                    End If

                End If
                Reader.Close()

                Dim TotImporto As Double = 0

                If Request.QueryString("CHIAMA") = "P" Then

                    par.cmd.CommandText = "SELECT ID_GESTIONE, COND_UI_GESTIONE_P.POSIZIONE_BILANCIO,COND_UI_GESTIONE_P.ID_UI, STATO, trim(TO_CHAR(IMPORTO,'9G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE =" & Request.QueryString("IDGESTIONE") & " ORDER BY POSIZIONE_BILANCIO ASC"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)

                    Dim row As Data.DataRow

                    For Each row In dt.Rows
                        TotImporto = TotImporto + par.IfNull(row.Item("IMPORTO"), 0)
                    Next
                    row = dt.NewRow()
                    row.Item("POSIZIONE_BILANCIO") = "TOTALE"
                    row.Item("IMPORTO") = Format(TotImporto, "##,##0.00")
                    dt.Rows.Add(row)

                    'par.cmd.CommandText = "SELECT ID_GESTIONE, COND_UI_GESTIONE_P.POSIZIONE_BILANCIO,COND_UI_GESTIONE_P.ID_UI, STATO, trim(TO_CHAR(IMPORTO,'9G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_P WHERE ID_GESTIONE =" & Request.QueryString("IDGESTIONE") & " and stato = 'IN CORSO ABUSIVO'  ORDER BY POSIZIONE_BILANCIO ASC"
                    'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    'da.Fill(dt2)

                    'For Each row In dt2.Rows
                    '    TotServizi = TotServizi + par.IfNull(row.Item("IMPORTO_SERVIZI"), 0)
                    '    TotAscensore = TotAscensore + par.IfNull(row.Item("IMPORTO_ASCENSORE"), 0)
                    '    TotRiscaldam = TotRiscaldam + par.IfNull(row.Item("IMPORTO_RISCALDAMENTO"), 0)
                    '    TotTOTALE = TotTOTALE + par.IfNull(row.Item("TOT"), 0)
                    'Next
                    'row = dt2.NewRow()
                    'row.Item("POSIZIONE_BILANCIO") = "TOTALE"
                    'row.Item("IMPORTO_SERVIZI") = Format(TotServizi, "##,##0.00")
                    'row.Item("IMPORTO_ASCENSORE") = Format(TotAscensore, "##,##0.00")
                    'row.Item("IMPORTO_RISCALDAMENTO") = Format(TotRiscaldam, "##,##0.00")
                    'row.Item("TOT") = Format(TotTOTALE, "##,##0.00")
                    'dt2.Rows.Add(row)
                Else
                    par.cmd.CommandText = "SELECT ID_GESTIONE, COND_UI_GESTIONE_C.POSIZIONE_BILANCIO,COND_UI_GESTIONE_C.ID_UI, STATO, trim(TO_CHAR(IMPORTO,'9G999G990D99')) AS IMPORTO FROM SISCOM_MI.COND_UI_GESTIONE_C WHERE ID_GESTIONE =" & Request.QueryString("IDGESTIONE") & " ORDER BY POSIZIONE_BILANCIO ASC"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)

                    Dim row As Data.DataRow

                    For Each row In dt.Rows
                        TotImporto = TotImporto + par.IfNull(row.Item("IMPORTO"), 0)
                    Next
                    row = dt.NewRow()
                    row.Item("POSIZIONE_BILANCIO") = "TOTALE"
                    row.Item("IMPORTO") = Format(TotImporto, "##,##0.00")
                    dt.Rows.Add(row)


                    'row.Item("POSIZIONE_BILANCIO") = "TOTALE"
                    'row.Item("IMPORTO_SERVIZI") = Format(TotServizi, "##,##0.00")
                    'row.Item("IMPORTO_ASCENSORE") = Format(TotAscensore, "##,##0.00")
                    'row.Item("IMPORTO_RISCALDAMENTO") = Format(TotRiscaldam, "##,##0.00")
                    'row.Item("TOT") = Format(TotTOTALE, "##,##0.00")

                    'dt.Rows.Add(row)
                    'TotServizi = 0
                    'TotAscensore = 0
                    'TotRiscaldam = 0
                    'TotTOTALE = 0

                    'par.cmd.CommandText = "SELECT ID_GESTIONE, COND_UI_GESTIONE_C.POSIZIONE_BILANCIO,COND_UI_GESTIONE_C.ID_UI, STATO, trim(TO_CHAR(IMPORTO_SERVIZI,'9G999G990D99')) AS IMPORTO_SERVIZI, trim(TO_CHAR(IMPORTO_ASCENSORE,'9G999G990D99')) AS IMPORTO_ASCENSORE,trim(TO_CHAR(IMPORTO_RISCALDAMENTO,'9G999G990D99')) AS IMPORTO_RISCALDAMENTO,trim(TO_CHAR((nvl(IMPORTO_RISCALDAMENTO,0)+nvl(IMPORTO_ASCENSORE,0)+nvl(IMPORTO_SERVIZI,0)),'9G999G990D99')) AS TOT FROM SISCOM_MI.COND_UI_GESTIONE_C WHERE ID_GESTIONE =" & Request.QueryString("IDGESTIONE") & " and stato = 'IN CORSO ABUSIVO' ORDER BY POSIZIONE_BILANCIO ASC"
                    'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    'da.Fill(dt2)

                    'For Each row In dt2.Rows
                    '    TotServizi = TotServizi + par.IfNull(row.Item("IMPORTO_SERVIZI"), 0)
                    '    TotAscensore = TotAscensore + par.IfNull(row.Item("IMPORTO_ASCENSORE"), 0)
                    '    TotRiscaldam = TotRiscaldam + par.IfNull(row.Item("IMPORTO_RISCALDAMENTO"), 0)
                    '    TotTOTALE = TotTOTALE + par.IfNull(row.Item("TOT"), 0)
                    'Next

                    'row = dt2.NewRow()
                    'row.Item("POSIZIONE_BILANCIO") = "TOTALE"
                    'row.Item("IMPORTO_SERVIZI") = Format(TotServizi, "##,##0.00")
                    'row.Item("IMPORTO_ASCENSORE") = Format(TotAscensore, "##,##0.00")
                    'row.Item("IMPORTO_RISCALDAMENTO") = Format(TotRiscaldam, "##,##0.00")
                    'row.Item("TOT") = Format(TotTOTALE, "##,##0.00")
                    'dt2.Rows.Add(row)
                End If


                DataGridLibeAbus.DataSource = dt
                DataGridLibeAbus.DataBind()

                'DataGridAbus.DataSource = dt2
                'DataGridAbus.DataBind()

                Session.Add("DTLIBERE", dt)
                'Session.Add("DTABUSIVI", dt2)
            End If


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

            dt = CType(HttpContext.Current.Session.Item("DTLIBERE"), Data.DataTable)
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE BILANCIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "STATO", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "SERVIZI", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ASCENSORE", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "RISCALDAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "IMPORTO", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STATO"), "")))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO_SERVIZI"), "")))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO_ASCENSORE"), "")))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO_RISCALDAMENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO"), "")))

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

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnExport0_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport0.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("DTABUSIVI"), Data.DataTable)
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE BILANCIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "STATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "SERVIZI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ASCENSORE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "RISCALDAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TOTALE", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("STATO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO_SERVIZI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO_ASCENSORE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("IMPORTO_RISCALDAMENTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TOT"), "")))

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

            'Response.Write("<script>window.open('Export/" & sNomeFile & ".zip','','');</script>") nella stessa pagina chiede dove salvare
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
