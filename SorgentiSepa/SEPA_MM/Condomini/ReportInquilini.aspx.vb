Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic


Partial Class Condomini_ReportInquilini
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim NUMERORIGHE As Long = 0
    Dim Contatore As Long = 0
    Public percentuale As Long = 0
    Dim lstNomiFileXls As New List(Of String)
    Dim Str As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../Immagini/load.gif' alt='Estrazione in corso...' ><br>Estrazione in corso...</br><div align=" & Chr(34) & "left" & Chr(34) & " id=" & Chr(34) & "AA" & Chr(34) & " style=" & Chr(34) & "background-color: #FFFFFF; border: 1px solid #000000; width: 100px;" & Chr(34) & "><img alt='' src=" & Chr(34) & "barra.gif" & Chr(34) & " id=" & Chr(34) & "barra" & Chr(34) & " height=" & Chr(34) & "10" & Chr(34) & " width=" & Chr(34) & "100" & Chr(34) & " /></div>"
        Str = Str & "</div> <br /><script  language=" & Chr(34) & "javascript" & Chr(34) & " type=" & Chr(34) & "text/javascript" & Chr(34) & ">var scarica; scarica = ''; var testo; testo = ''; var tempo; tempo=0; function Mostra() {document.getElementById('barra').style.width = tempo + 'px';}setInterval(" & Chr(34) & "Mostra()" & Chr(34) & ", 100);</script>"

        Response.Write(Str)

        If Not IsPostBack Then
            Estrazione()
        End If
    End Sub
    Private Sub Estrazione()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini order by denominazione asc"
            Dim dt_ElCondomini As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt_ElCondomini)
            lstNomiFileXls.Clear()
            NUMERORIGHE = dt_ElCondomini.Rows.Count

            For Each riga As Data.DataRow In dt_ElCondomini.Rows
                FindInquilini(riga.Item("ID"), riga.Item("DENOMINAZIONE"))
                Contatore = Contatore + 1
                percentuale = (Contatore * 100) / NUMERORIGHE
                Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                Response.Flush()
            Next
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Dim zipfic As String = ""
            Dim nomefile As String = "ExptInquilini_Condomini" & Format(Now, "yyyyMMddHHmmss")

            If lstNomiFileXls.Count > 0 Then

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream

                zipfic = Server.MapPath("..\FileTemp\" & nomefile & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                '
                NUMERORIGHE = lstNomiFileXls.Count
                Contatore = 0
                percentuale = 0
                For Each sNomeFile As String In lstNomiFileXls
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
                    File.Delete(strFile)
                    Contatore = Contatore + 1
                    percentuale = (Contatore * 100) / NUMERORIGHE
                    Response.Write("<script>tempo=" & Format(percentuale, "0") & ";</script>")
                    Response.Flush()
                    System.IO.File.Delete(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                Next
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

            End If
            Response.Write("<script language=javascript>document.getElementById('dvvvPre').style.visibility = 'hidden';")
            Response.Write("</script>")
            Response.Write("<br><br><a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../FileTemp/" & nomefile & ".zip','Expt', '')" & Chr(34) & ">Clicca QUI per scaricare il file.</a>")

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
    Private Sub FindInquilini(ByVal IdCondominio As String, ByVal NomeCond As String)

        If Not String.IsNullOrEmpty(IdCondominio) Then

            par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO , " _
                                & "RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID AS ID_UI, " _
                                & "UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE ,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA," _
                                & "TIPO_DISPONIBILITA.DESCRIZIONE AS OCCUPAZIONE,'' AS STATO_RAPP, '' AS STATOVISUAL , " _
                                & "POSIZIONE_BILANCIO,'' AS NUM_COMP_NUCLEO, '' AS NUM_OSPITI, TO_CHAR(MIL_PRO,'9G999G990D9999') AS MIL_PRO, " _
                                & "TO_CHAR(MIL_ASC,'9G999G990D9999')AS MIL_ASC,TO_CHAR(MIL_COMPRO,'9G999G990D9999')AS MIL_COMPRO,TO_CHAR(MIL_GEST,'9G999G990D9999')AS MIL_GEST, " _
                                & "TO_CHAR(MIL_RISC,'9G999G990D9999') AS MIL_RISC,TO_CHAR(MILL_PRES_ASS,'9G999G990D9999') AS MILL_PRES_ASS," _
                                & "COND_UI.NOTE,UNITA_CONTRATTUALE.ID_CONTRATTO," _
                                & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN ''  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO, (CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
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
                                & "AND (cond_ui.id_condominio=" & IdCondominio & ")  AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL  " _
                                & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & IdCondominio & ") " _
                                & "UNION " _
                                & "SELECT UNITA_IMMOBILIARI.ID_EDIFICIO,ID_CONTRATTO,RAPPORTI_UTENZA.COD_CONTRATTO, " _
                                & "RAPPORTI_UTENZA.DATA_DECORRENZA, UNITA_IMMOBILIARI.ID AS ID_UI,  UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE , " _
                                & "TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE AS TIPOLOGIA,TIPO_DISPONIBILITA.DESCRIZIONE AS OCCUPAZIONE,'' AS STATO_RAPP," _
                                & "'' AS STATOVISUAL , '' AS POSIZIONE_BILANCIO,'' AS NUM_COMP_NUCLEO, '' AS NUM_OSPITI, '' AS MIL_PRO,''AS MIL_ASC, " _
                                & "'' AS MIL_COMPRO,''AS MIL_GEST, '' AS MIL_RISC, '' AS MILL_PRES_ASS, '' AS NOTE,UNITA_CONTRATTUALE.ID_CONTRATTO," _
                                & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN '' ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO, (CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select," _
                                & "(CASE WHEN SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0)='CHIUSO' THEN '' ELSE siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0)END )AS INTESTATARIO," _
                                & "'' AS NOMINATIVO,INDIRIZZI.DESCRIZIONE,UNITA_IMMOBILIARI.INTERNO,SCALE_EDIFICI.DESCRIZIONE AS SCALA " _
                                & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI, " _
                                & "SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.INDIRIZZI,SISCOM_MI.SCALE_EDIFICI, " _
                                & "SISCOM_MI.TIPO_DISPONIBILITA, SISCOM_MI.RAPPORTI_UTENZA " _
                                & "WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID " _
                                & "AND SCALE_EDIFICI.ID(+)= UNITA_IMMOBILIARI.ID_SCALA  AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                & "AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = TIPO_DISPONIBILITA.COD AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+)  " _
                                & "AND COD_TIPO_DISPONIBILITA <> 'VEND' AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                & "AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & IdCondominio & ") " _
                                & "AND unita_immobiliari.ID NOT IN (SELECT id_ui FROM siscom_mi.cond_ui WHERE id_condominio = " & IdCondominio & ") " _
                                & "AND RAPPORTI_UTENZA.ID(+) = UNITA_CONTRATTUALE.ID_CONTRATTO AND UNITA_IMMOBILIARI.ID_UNITA_PRINCIPALE IS NULL " _
                                & "ORDER BY DESCRIZIONE ASC,INTERNO ASC,SCALA ASC, ID_UI ASC,DATA_DECORRENZA DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable

            da.Fill(dt)
            dt = FiltraContrattiVeri(dt)

            CreaFileExcel(dt, Format(CDbl(IdCondominio), "00000"), NomeCond)



        End If

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


    Private Sub CreaFileExcel(ByVal Elenco As Data.DataTable, ByVal Cod As String, ByVal Nome As String)
        Try

            If Elenco.Rows.Count > 0 Then
                Dim myExcelFile As New CM.ExcelFile
                Dim i As Long
                Dim K As Long
                Dim sNomeFile As String
                Dim row As System.Data.DataRow

                sNomeFile = Cod & "_" & Nome.Replace("/", "").Replace("\", "").Replace(".", "").Replace("'", "").Trim & "_" & Format(Now, "yyyyMMddHHmmss")
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

                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD. UNITA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "TIPOLOGIA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "INTERNO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SCALA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "STATO OCCUPAZIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "INTESTATARIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "NOMINATIVO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "RAPPORTO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "POS. BILANCIO", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "N.COMP.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "N.OSPITI.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "MIL.PROP.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "MIL.ASC.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "MIL.COMPRO.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "MIL.GESTIONE", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "MIL.RISCALD.", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "MIL.PRES.ASSEMBLEA", 12)
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "NOTE", 12)



                    K = 2
                    For Each row In Elenco.Rows
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("COD_UNITA_IMMOBILIARE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("TIPOLOGIA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("INTERNO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("SCALA"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("OCCUPAZIONE"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("INTESTATARIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("NOMINATIVO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("STATO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("POSIZIONE_BILANCIO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("NUM_COMP_NUCLEO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("NUM_OSPITI"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("MIL_PRO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("MIL_ASC"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("MIL_COMPRO"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("MIL_GEST"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("MIL_RISC"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("MILL_PRES_ASS"), "")))
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, par.PulisciStrSql(par.IfNull(Elenco.Rows(i).Item("NOTE"), "")))

                        i = i + 1
                        K = K + 1
                    Next

                    .CloseFile()

                End With

                lstNomiFileXls.Add(sNomeFile)




            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try

    End Sub
End Class
