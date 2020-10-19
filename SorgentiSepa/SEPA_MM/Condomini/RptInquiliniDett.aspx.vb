Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class CondominiNew_RptInquiliniDett
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Public Property TipoCond() As String
        Get
            If Not (ViewState("par_TipoCond") Is Nothing) Then
                Return CStr(ViewState("par_TipoCond"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_TipoCond") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:500; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"


        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()

            Cerca()
        End If

    End Sub
    Private Sub Cerca()

        Dim TotProp As Double = 0
        Dim TotComProp As Double = 0
        Dim TotGest As Double = 0
        Dim TotRiscald As Double = 0
        '*******************APERURA CONNESSIONE*********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim sStringaSQL As String = ""
        par.cmd.CommandText = "SELECT TIPOLOGIA,DENOMINAZIONE,NOME as CITTA FROM SISCOM_MI.CONDOMINI, COMUNI_NAZIONI WHERE CONDOMINI.ID = " & Request.QueryString("IdCond") & " AND COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
        Dim Reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If Reader.Read Then
            Me.lblTitle.Text = "CONDOMINIO : " & Reader("DENOMINAZIONE") & " - " & Reader("CITTA")
            TipoCond = Reader("TIPOLOGIA")
        End If
        Reader.Close()

        sStringaSQL = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO , " _
                            & "RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID AS ID_UI, " _
                            & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA," _
                            & "TIPO_DISPONIBILITA.DESCRIZIONE AS OCCUPAZIONE,'' AS STATO_RAPP, '' AS STATOVISUAL , " _
                            & "POSIZIONE_BILANCIO,'' AS NUM_COMP_NUCLEO, '' AS NUM_OSPITI, TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO, " _
                            & "TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC,TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST, " _
                            & "TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC,TO_CHAR(MILL_PRES_ASS,'9G999G990D9999') AS MILL_PRES_ASS," _
                            & "COND_UI.NOTE,UNITA_CONTRATTUALE.ID_CONTRATTO," _
                            & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN ''  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
                            & "(CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='CHIUSO' THEN '' ELSE siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0)END )AS INTESTATARIO, " _
                            & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) AS NOMINATIVO," _
                            & "INDIRIZZI.DESCRIZIONE,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA " _
                            & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, " _
                            & "SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA, SISCOM_MI.INDIRIZZI," _
                            & "SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
                            & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD " _
                            & "AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA " _
                            & "AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD " _
                            & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+)  AND COND_UI.ID_UI(+) = UNITA_IMMOBILIARI.ID " _
                            & "AND COD_TIPO_DISPONIBILITA <> 'VEND' AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND  COND_UI.ID_INTESTARIO=ANAGRAFICA.ID(+) " _
                            & "AND (cond_ui.id_condominio=" & Request.QueryString("IdCond") & ")  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL  " _
                            & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & Request.QueryString("IdCond") & ") " _
                            & "UNION " _
                            & "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO, " _
                            & "RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID AS ID_UI,  UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE , " _
                            & "TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TIPO_DISPONIBILITA.DESCRIZIONE AS OCCUPAZIONE,'' AS STATO_RAPP," _
                            & "'' AS STATOVISUAL , '' AS POSIZIONE_BILANCIO,'' AS NUM_COMP_NUCLEO, '' AS NUM_OSPITI, '' AS MIL_PRO,''AS MIL_ASC, " _
                            & "'' AS MIL_COMPRO,''AS MIL_GEST, '' AS MIL_RISC, '' AS MILL_PRES_ASS, '' AS NOTE,UNITA_CONTRATTUALE.ID_CONTRATTO," _
                            & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN '' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
                            & "(CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='CHIUSO' THEN '' ELSE siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0)END )AS INTESTATARIO," _
                            & "'' AS NOMINATIVO,INDIRIZZI.DESCRIZIONE,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA " _
                            & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, " _
                            & "SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI, " _
                            & "SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
                            & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID " _
                            & "AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA  AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                            & "AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+)  " _
                            & "AND COD_TIPO_DISPONIBILITA <> 'VEND' AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                            & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & Request.QueryString("IdCond") & ") " _
                            & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE id_condominio = " & Request.QueryString("IdCond") & ") " _
                            & "AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                            & "ORDER BY DESCRIZIONE ASC,INTERNO ASC,SCALA ASC, ID_UI ASC,DATA_DECORRENZA DESC"




        par.cmd.CommandText = sStringaSQL

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(dt)
        dt = FiltraContrattiVeri(dt)
        Dim row As Data.DataRow

        For Each row In dt.Rows
            TotProp = TotProp + CDbl(par.IfNull(row.Item("MIL_PRO"), 0))
            TotComProp = TotComProp + CDbl(par.IfNull(row.Item("MIL_COMPRO"), 0))
            TotGest = TotGest + CDbl(par.IfNull(row.Item("MIL_GEST"), 0))
            TotRiscald = TotRiscald + CDbl(par.IfNull(row.Item("MIL_RISC"), 0))
        Next

        row = dt.NewRow()
        row.Item("POSIZIONE_BILANCIO") = "T O T A L E"
        row.Item("MIL_PRO") = Format(TotProp, "0.0000")
        row.Item("MIL_COMPRO") = Format(TotComProp, "0.0000")
        row.Item("MIL_GEST") = Format(TotGest, "0.0000")
        row.Item("MIL_RISC") = Format(TotRiscald, "0.0000")

        dt.Rows.Add(row)

        DataGridInquilini.DataSource = dt
        DataGridInquilini.DataBind()

        '*********************CHIUSURA CONNESSIONE**********************
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Session.Add("MIADT", dt)


        'NascondiColonne()


    End Sub
    Private Function FiltraContrattiVeri(ByVal Table As Data.DataTable) As Data.DataTable
        FiltraContrattiVeri = Table.Clone()
        Dim idUi As Integer = 0
        Try
            Dim rSelect As Data.DataRow()

            For i As Integer = 0 To Table.Rows.Count - 1
                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
                    If Table.Rows(i).Item("ID_UI") <> idUi Then
                        rSelect = Table.Select("ID_UI = " & Table.Rows(i).Item("ID_UI") & " AND STATO_DT_SELECT LIKE '%IN CORSO%'")
                        If rSelect.Length > 0 Then
                            FiltraContrattiVeri.Rows.Add(rSelect(0).ItemArray)
                            idUi = rSelect(0).Item("ID_UI")
                        Else
                            FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                            idUi = Table.Rows(i).Item("ID_UI")
                        End If
                    End If
                Else
                    FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                End If
            Next

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " FiltraContrattiVeri"
        End Try
        Return FiltraContrattiVeri
    End Function


    Protected Sub btnExportPrev_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExportPrev.Click
        Try




            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            sNomeFile = "CondExpAmministratori_" & Format(Now, "yyyyMMddHHmmss")

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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "CODICE UNITA IMMOBILIARE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "POSIZIONE DI BILANCIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "STATO OCCUPAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MILL. PROPIETA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "MILLESIMI ASCENSORE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "MILLESIMI COMPROPIETA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "MILLESIMI GESTIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "MILLESIMI RISCALDAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "PRESENZA ASSEMBLEA", 12)

                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_UNITA_IMMOBILIARE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("OCCUPAZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("SCALA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOMINATIVO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_PRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_ASC"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_COMPRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_GEST"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_RISC"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_PRES_ASS"), "")))

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
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
End Class
