Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class Condomini_RptSituazPat
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
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            If Request.QueryString("IDCON") <> "" Then
                vIdConnModale = Request.QueryString("IDCON")
            End If

            Cerca()
        End If

    End Sub
    Private Sub Cerca()

        Dim TotProp As Double = 0
        Dim TotComProp As Double = 0
        Dim TotGest As Double = 0
        Dim TotRiscald As Double = 0

        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Dim sStringaSQL As String = ""
        par.cmd.CommandText = "SELECT TIPOLOGIA,DENOMINAZIONE,NOME as CITTA FROM SISCOM_MI.CONDOMINI, COMUNI_NAZIONI  WHERE CONDOMINI.ID = " & Request.QueryString("IDCONDOMINIO") & " AND COMUNI_NAZIONI.COD = CONDOMINI.COD_COMUNE"
        Dim Reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If Reader.Read Then
            Me.lblTitle.Text = "CONDOMINIO : " & Reader("DENOMINAZIONE") & " - " & Reader("CITTA")
            TipoCond = Reader("TIPOLOGIA")
        End If
        Reader.Close()

        Select Case Request.QueryString("CHIAMA")

            Case "PREV"

                sStringaSQL = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO,NVL(ID_CONTRATTO,'') AS ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ," _
                & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO,TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC," _
                & " TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST,TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC," _
                & " (CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  " _
                & " SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select,(CASE WHEN siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO)IS NULL" _
                & " THEN(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END)" _
                & " WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN " _
                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('O.A. - '||ragione_sociale)  ELSE (RTRIM(LTRIM('O.A. - '||COGNOME ||' ' ||NOME))) END) ELSE" _
                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) END)  AS INTESTATARIO," _
                & " COND_UI_PREVENTIVI.POSIZIONE_BILANCIO,SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,NVL(INDIRIZZI.CIVICO,'') AS CIVICO_COR,UNITA_IMMOBILIARI.INTERNO " _
                & " FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI_PREVENTIVI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA" _
                & ",SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.RAPPORTI_UTENZA" _
                & " WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID" _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI_PREVENTIVI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND'" _
                & " AND (cond_ui_PREVENTIVI.ID_GESTIONE=" & Request.QueryString("IDGESTIONE") & " ) AND ANAGRAFICA.ID(+)=COND_UI_PREVENTIVI.ID_INTESTARIO AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA" _
                & " AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO ORDER BY POSIZIONE_BILANCIO ASC"


            Case "CONSU"

                sStringaSQL = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO, NVL(ID_CONTRATTO,'') AS ID_CONTRATTO, UNITA_IMMOBILIARI.ID AS ID_UI, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ," _
                & " TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO,TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC," _
                & " TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST,TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC," _
                & " (CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  " _
                & " SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO,(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select,(CASE WHEN siscom_mi.Getintestatari(UNITA_CONTRATTUALE.ID_CONTRATTO)IS NULL" _
                & " THEN(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END)" _
                & " WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)= 'IN CORSO ABUSIVO' THEN " _
                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ('O.A. - '||ragione_sociale)  ELSE (RTRIM(LTRIM('O.A. - '||COGNOME ||' ' ||NOME))) END)  ELSE" _
                & " (CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||NOME)) END) END)  AS INTESTATARIO," _
                & " COND_UI_CONSUNTIVI.POSIZIONE_BILANCIO,SCALE_EDIFICI.DESCRIZIONE,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,NVL(INDIRIZZI.CIVICO,'') AS CIVICO_COR,UNITA_IMMOBILIARI.INTERNO " _
                & " FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI_CONSUNTIVI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.ANAGRAFICA" _
                & ",SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.RAPPORTI_UTENZA" _
                & " WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID" _
                & " AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI_CONSUNTIVI.ID_UI(+) = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND'" _
                & " AND (cond_ui_CONSUNTIVI.ID_GESTIONE=" & Request.QueryString("IDGESTIONE") & " ) AND ANAGRAFICA.ID(+)=COND_UI_CONSUNTIVI.ID_INTESTARIO AND SCALE_EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_SCALA" _
                & " AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO ORDER BY POSIZIONE_BILANCIO ASC"

        End Select


        'sStringaSQL = sStringaSQL & " AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & ")"


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


        Session.Add("MIADT", dt)


        NascondiColonne()


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

    Private Sub NascondiColonne()

        If TipoCond = "C" Then

            Me.DataGridInquilini.Columns(12).Visible = False
            Me.DataGridInquilini.Columns(13).Visible = False
            Me.DataGridInquilini.Columns(14).Visible = False

        ElseIf TipoCond = "S" Then
            Me.DataGridInquilini.Columns(10).Visible = False
            Me.DataGridInquilini.Columns(11).Visible = False
            Me.DataGridInquilini.Columns(13).Visible = False
            Me.DataGridInquilini.Columns(14).Visible = False


        ElseIf TipoCond = "T" Then
            Me.DataGridInquilini.Columns(10).Visible = False
            Me.DataGridInquilini.Columns(11).Visible = False
            Me.DataGridInquilini.Columns(12).Visible = False
            Me.DataGridInquilini.Columns(13).Visible = False

        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If TipoCond = "C" Then
            ExportAsCondominio()
        ElseIf TipoCond = "S" Then

            ExportAsSuperCondominio()
        ElseIf TipoCond = "T" Then

            ExportAsCentTermica()
        End If
    End Sub
    Private Sub ExportAsCondominio()
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MILL.PROP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "MILL. ASC.", 12)
                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_PRO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_ASC"), "")))
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
    Private Sub ExportAsSuperCondominio()
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MILL.COMP.", 12)
                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_COMPRO"), "")))
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
    Private Sub ExportAsCentTermica()
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "POSIZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "CIVICO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INTERNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SCALA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "PIANO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "TIPO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "MILL/SUP RISC.", 12)
                K = 2
                For Each row In dt.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("CIVICO_COR"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTERNO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("PIANO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("TIPOLOGIA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("INTESTATARIO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("MIL_RISC"), "")))

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
