Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Condomini_RptContabilita
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

            Dim totConguaglioGp As Double = 0
            Dim totPreventivo As Double = 0
            Dim totRata1 As Double = 0
            Dim totRata2 As Double = 0
            Dim totRata3 As Double = 0
            Dim totRata4 As Double = 0
            Dim totRata5 As Double = 0
            Dim totRata6 As Double = 0

            Dim Rata1 As String = ""
            Dim Rata2 As String = ""
            Dim Rata3 As String = ""
            Dim Rata4 As String = ""
            Dim Rata5 As String = ""
            Dim Rata6 As String = ""
            Dim nRate As String = ""
            Dim sStatoBil As String = ""
            If Request.QueryString("IDGEST") <> "" Then

                par.cmd.CommandText = "SELECT TIPOLOGIA,DENOMINAZIONE,NOME as CITTA FROM SISCOM_MI.CONDOMINI, COMUNI_NAZIONI WHERE CONDOMINI.ID = " & Request.QueryString("IDCONDOMINIO") & " AND COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
                Dim Reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If Reader.Read Then
                    Me.lblTitle.Text = "CONDOMINIO : " & Reader("DENOMINAZIONE") & " - " & Reader("CITTA")
                    'TipoCond = Reader("TIPOLOGIA")

                End If
                Reader.Close()



                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_GESTIONE WHERE ID = " & Request.QueryString("IDGEST")
                Reader = par.cmd.ExecuteReader
                If Reader.Read Then
                    Dim Tipo As String
                    If Reader("TIPO") = "O" Then
                        Tipo = "ORDINARIO"
                    Else
                        Tipo = "STRAORDINARIO"
                    End If

                    Me.lblPeriodo.Text = "Gestione: dal " & par.FormattaData(Reader("DATA_INIZIO")) & " al " & par.FormattaData(Reader("DATA_FINE")) & "</br>Numero Rate: " & Reader("N_RATE") & " </br> Tipologia: " & Tipo
                    Rata1 = "Rata 1 " & par.FormattaData(par.IfNull(Reader("RATA_1_SCAD"), ""))
                    Rata2 = "Rata 2 " & par.FormattaData(par.IfNull(Reader("RATA_2_SCAD"), ""))
                    Rata3 = "Rata 3 " & par.FormattaData(par.IfNull(Reader("RATA_3_SCAD"), ""))
                    Rata4 = "Rata 4 " & par.FormattaData(par.IfNull(Reader("RATA_4_SCAD"), ""))
                    Rata5 = "Rata 5 " & par.FormattaData(par.IfNull(Reader("RATA_5_SCAD"), ""))
                    Rata6 = "Rata 6 " & par.FormattaData(par.IfNull(Reader("RATA_6_SCAD"), ""))
                    nRate = par.IfNull(Reader("N_RATE"), "0")
                    sStatoBil = par.IfNull(Reader("STATO_BILANCIO"), "P")
                End If
                Reader.Close()

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader


                par.cmd.CommandText = "select distinct(id_piano_finanziario) as id_piano_f from siscom_mi.cond_voci_spesa_pf where id_voce_cond in (select id_voce from siscom_mi.cond_gestione_dett where id_gestione =" & Request.QueryString("IDGEST") & ")"


                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    idPianoF.Value = par.IfNull(myReader1("ID_PIANO_F"), 0)
                End If
                myReader1.Close()




                par.cmd.CommandText = "SELECT COND_VOCI_SPESA.FL_TOTALE,COND_VOCI_SPESA.ID AS IDVOCE, " _
                                    & "COND_VOCI_SPESA.DESCRIZIONE,'' AS ID_GESTIONE,'' AS CONGUAGLIO_GP, '' AS PREVENTIVO,'' AS RATA_1,'' AS RATA_2,'' AS RATA_3,'' AS RATA_4, '' AS RATA_5,'' AS RATA_6, " _
                                    & "COND_VOCI_SPESA_PF.ID_VOCE_PF, COND_VOCI_SPESA_PF.ID_VOCE_PF_IMPORTO " _
                                    & "FROM SISCOM_MI.COND_VOCI_SPESA,SISCOM_MI.COND_VOCI_SPESA_PF WHERE FL_TOTALE = 1 AND COND_VOCI_SPESA.ID = ID_VOCE_COND AND ID_PIANO_FINANZIARIO = " & idPianoF.Value & " ORDER BY idvoce ASC"


                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dt)

                Dim row As Data.DataRow

                For Each row In dt.Rows
                    par.cmd.CommandText = "SELECT CONGUAGLIO_GP, PREVENTIVO" _
                                        & " FROM SISCOM_MI.COND_GESTIONE_DETT" _
                                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST")
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        row.Item("CONGUAGLIO_GP") = IsNumFormat(myReader1("CONGUAGLIO_GP"), "", "##,##0.00")
                        row.Item("PREVENTIVO") = IsNumFormat(myReader1("PREVENTIVO"), "", "##,##0.00")

                        totConguaglioGp = totConguaglioGp + CDbl(par.IfNull(myReader1("CONGUAGLIO_GP"), 0))
                        totPreventivo = totPreventivo + CDbl(par.IfNull(myReader1("PREVENTIVO"), 0))

                    End If
                    myReader1.Close()
                    'IMPORTO SINGOLE RATE
                    '**************RATA 1
                    If Not String.IsNullOrEmpty(Rata1.Replace("Rata 1 ", "")) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST") & " AND RATA_SCAD = " & par.AggiustaData(Rata1.Replace("Rata 1 ", ""))
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_1") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                            totRata1 = totRata1 + CDbl(par.IfNull(myReader1("IMPORTO"), 0))

                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 2
                    If Not String.IsNullOrEmpty(Rata2.Replace("Rata 2 ", "")) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST") & " AND RATA_SCAD = " & par.AggiustaData(Rata2.Replace("Rata 2 ", ""))
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_2") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                            totRata2 = totRata2 + CDbl(par.IfNull(myReader1("IMPORTO"), 0))

                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 3
                    If Not String.IsNullOrEmpty(Rata3.Replace("Rata 3 ", "")) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST") & " AND RATA_SCAD = " & par.AggiustaData(Rata3.Replace("Rata 3 ", ""))
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_3") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                            totRata3 = totRata3 + CDbl(par.IfNull(myReader1("IMPORTO"), 0))
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 4
                    If Not String.IsNullOrEmpty(Rata4.Replace("Rata 4 ", "")) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST") & " AND RATA_SCAD = " & par.AggiustaData(Rata4.Replace("Rata 4 ", ""))
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_4") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                            totRata4 = totRata4 + CDbl(par.IfNull(myReader1("IMPORTO"), 0))
                        End If
                        myReader1.Close()

                    End If
                    '**************RATA 5
                    If Not String.IsNullOrEmpty(Rata5.Replace("Rata 5 ", "")) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST") & " AND RATA_SCAD = " & par.AggiustaData(Rata5.Replace("Rata 5 ", ""))
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_5") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                            totRata5 = totRata5 + CDbl(par.IfNull(myReader1("IMPORTO"), 0))
                        End If
                        myReader1.Close()
                    End If
                    '**************RATA 6
                    If Not String.IsNullOrEmpty(Rata6.Replace("Rata 6 ", "")) Then
                        par.cmd.CommandText = "SELECT IMPORTO " _
                        & " FROM SISCOM_MI.COND_GESTIONE_DETT_SCAD" _
                        & " WHERE ID_VOCE = " & row.Item("IDVOCE") & " AND ID_GESTIONE = " & Request.QueryString("IDGEST") & " AND RATA_SCAD = " & par.AggiustaData(Rata6.Replace("Rata 6 ", ""))
                        myReader1 = par.cmd.ExecuteReader
                        If myReader1.Read Then
                            row.Item("RATA_6") = IsNumFormat(myReader1("IMPORTO"), 0, "##,##0.00")
                            totRata6 = totRata6 + CDbl(par.IfNull(myReader1("IMPORTO"), 0))
                        End If
                    End If

                Next

                row = dt.NewRow()
                row.Item("DESCRIZIONE") = "T O T A L E"
                row.Item("CONGUAGLIO_GP") = Format(totConguaglioGp, "##,##0.00")
                row.Item("PREVENTIVO") = Format(totPreventivo, "##,##0.00")
                row.Item("RATA_1") = Format(totRata1, "##,##0.00")
                row.Item("RATA_2") = Format(totRata2, "##,##0.00")
                row.Item("RATA_3") = Format(totRata3, "##,##0.00")
                row.Item("RATA_4") = Format(totRata4, "##,##0.00")
                row.Item("RATA_5") = Format(totRata5, "##,##0.00")
                row.Item("RATA_6") = Format(totRata6, "##,##0.00")
                dt.Rows.Add(row)

                DataGridInquilini.DataSource = dt

                DataGridInquilini.Columns("3").HeaderText = Rata1 '<---1rata
                DataGridInquilini.Columns("4").HeaderText = Rata2 '<---2rata
                DataGridInquilini.Columns("5").HeaderText = Rata3 '<---3rata
                DataGridInquilini.Columns("6").HeaderText = Rata4 '<---4rata
                DataGridInquilini.Columns("7").HeaderText = Rata5 '<---5rata
                DataGridInquilini.Columns("8").HeaderText = Rata6 '<---6rata
                Select Case nRate

                    Case 1
                        DataGridInquilini.Columns("4").Visible = False '<---4rata
                        DataGridInquilini.Columns("5").Visible = False '<---4rata
                        DataGridInquilini.Columns("6").Visible = False '<---4rata
                        DataGridInquilini.Columns("7").Visible = False '<---5rata
                        DataGridInquilini.Columns("8").Visible = False '<---6rata
                    Case 2
                        DataGridInquilini.Columns("5").Visible = False '<---4rata
                        DataGridInquilini.Columns("6").Visible = False '<---4rata
                        DataGridInquilini.Columns("7").Visible = False '<---5rata
                        DataGridInquilini.Columns("8").Visible = False '<---6rata

                    Case 3
                        DataGridInquilini.Columns("6").Visible = False '<---4rata
                        DataGridInquilini.Columns("7").Visible = False '<---5rata
                        DataGridInquilini.Columns("8").Visible = False '<---6rata
                    Case 4
                        DataGridInquilini.Columns("7").Visible = False '<---5rata
                        DataGridInquilini.Columns("8").Visible = False '<---6rata

                    Case 5
                        DataGridInquilini.Columns("8").Visible = False '<---6rata

                End Select

                DataGridInquilini.DataBind()

                Session.Add("MIADTPREV", dt)


                If sStatoBil <> "P" Then

                    Dim dt2 As New Data.DataTable
                    Dim totConsuntivo As Double = 0
                    Dim totConguaglio As Double = 0
                    par.cmd.CommandText = "SELECT COND_VOCI_SPESA.ID AS IDVOCE, COND_VOCI_SPESA.DESCRIZIONE,ID_GESTIONE, TRIM(TO_CHAR(PREVENTIVO,'9G999G990D99')) AS PREVENTIVO, TRIM(TO_CHAR(CONSUNTIVO,'9G999G990D99')) AS CONSUNTIVO,TRIM(TO_CHAR(CONGUAGLIO ,'9G999G990D99')) AS CONGUAGLIO  FROM SISCOM_MI.COND_VOCI_SPESA, SISCOM_MI.COND_GESTIONE_DETT WHERE ID_GESTIONE =" & Request.QueryString("IDGEST") & "  AND COND_VOCI_SPESA.ID = ID_VOCE"
                    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da2.Fill(dt2)

                    Dim row2 As Data.DataRow

                    For Each row2 In dt2.Rows
                        totConsuntivo = totConsuntivo + CDbl(par.IfNull(row2.Item("CONSUNTIVO"), 0))
                        totConguaglio = totConguaglio + CDbl(par.IfNull(row2.Item("CONGUAGLIO"), 0))
                    Next


                    row2 = dt2.NewRow()
                    row2.Item("DESCRIZIONE") = "T O T A L E"
                    row2.Item("PREVENTIVO") = Format(totPreventivo, "##,##0.00")
                    row2.Item("CONSUNTIVO") = Format(totConsuntivo, "##,##0.00")
                    row2.Item("CONGUAGLIO") = Format(totConguaglio, "##,##0.00")
                    dt2.Rows.Add(row2)

                    DataGridConsuntivo.DataSource = dt2
                    DataGridConsuntivo.DataBind()
                    Session.Add("MIADTCONS", dt2)

                Else
                    Me.lblConsuntivo.Visible = False
                    Me.btnExportCons.Visible = False
                End If

            End If


            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnExportPrev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportPrev.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADTPREV"), Data.DataTable)
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "DESCRIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CONGUAGLIO GEST. PREC.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "PREVENTIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "RATA 1", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "RATA 2", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "RATA 3", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "RATA 4", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "RATA 5", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "RATA 6", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONGUAGLIO_GP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PREVENTIVO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RATA_1"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RATA_2"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RATA_3"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RATA_4"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RATA_5"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("RATA_6"), "")))

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

    Protected Sub btnExportCons_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportCons.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADTCONS"), Data.DataTable)
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "VOCE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CONSUNTIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "PREVENTIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CONGUAGLIO", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONSUNTIVO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PREVENTIVO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CONGUAGLIO"), "")))

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
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

End Class
