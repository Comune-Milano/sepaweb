Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_RisultatoRicPostAler
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreBA As String
    Dim sValoreFI As String
    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sStringaSql As String
    Dim sStringaSqlX As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()
            sValoreBA = Request.QueryString("BA")
            sValoreFI = Request.QueryString("FI")
            sValoreSDAL = Request.QueryString("SDAL")
            sValoreSAL = Request.QueryString("SAL")

            If sValoreFI = "-1" Then sValoreFI = ""
            If sValoreBA = -1 Then sValoreBA = ""
            LBLID.Value = "-1"
            Cerca()

        End If
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""
        sStringaSqlX = ""



        If sValoreFI <> "" Then

            sValore = sValoreFI

            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA FROM UTENZA_SPORTELLI_PATRIMONIO WHERE ID_SPORTELLO=" & sValoreFI & " AND ID_AU=" & sValoreBA & ") "
            sStringaSqlX = sStringaSqlX & " UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA FROM UTENZA_SPORTELLI_PATRIMONIO WHERE ID_SPORTELLO=" & sValoreFI & " AND ID_AU=" & sValoreBA & ") "
        End If

        If sValoreSDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            If bTrovato = True Then sStringaSqlX = sStringaSqlX & " AND "

            sValore = sValoreSDAL
            bTrovato = True
            sStringaSql = sStringaSql & " DIFFIDE_LETTERE.DATA_GENERAZIONE>='" & par.PulisciStrSql(sValore) & "' "
            sStringaSqlX = sStringaSqlX & " DIFFIDE_LETTERE.DATA_GENERAZIONE>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreSAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            If bTrovato = True Then sStringaSqlX = sStringaSqlX & " AND "

            sValore = sValoreSAL
            bTrovato = True
            sStringaSql = sStringaSql & " DIFFIDE_LETTERE.DATA_GENERAZIONE<='" & par.PulisciStrSql(sValore) & "' "
            sStringaSqlX = sStringaSqlX & " DIFFIDE_LETTERE.DATA_GENERAZIONE<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreFI = "" Then
            sValoreFI = "-1"
        End If
        sStringaSQL1 = "SELECT (CASE WHEN DIFFIDE_LETTERE.TIPO=0 THEN 'DOC. MANCANTE' ELSE 'NON RISPONDENTE' END) AS TIPOLOGIA,(SELECT TIPOLOGIA_ESITI_POSTALER.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_ESITI_POSTALER WHERE ID=POSTALER_ESITI.ID_TIPO_ESITI_POSTALER) AS DESCRIZIONE ," _
                     & "TO_CHAR(TO_DATE(POSTALER_ESITI.DATA_ESITO,'YYYYmmdd'),'DD/MM/YYYY') as DATA_ESITO,POSTALER.ID,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,RAPPORTI_UTENZA.COD_CONTRATTO,TO_CHAR(TO_DATE(DIFFIDE_LETTERE.DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') as DATA_GENERAZIONE," _
                     & "UNITA_CONTRATTUALE.ID_UNITA,(SELECT descrizione FROM UTENZA_SPORTELLI WHERE ID=" & sValoreFI & ") AS FILIALE " _
                     & "FROM SISCOM_MI.POSTALER,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.UNITA_CONTRATTUALE," _
                     & "SISCOM_MI.DIFFIDE_LETTERE,SISCOM_MI.ANAGRAFICA,SISCOM_MI.POSTALER_ESITI,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                     & "WHERE " _
                     & "POSTALER.ID=POSTALER_ESITI.ID_POSTALER (+) AND POSTALER.ID_LETTERA=DIFFIDE_LETTERE.ID AND " _
                     & "UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA " _
                     & "And UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID And UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE Is NULL And DIFFIDE_LETTERE.ID_AU = " & sValoreBA & " And " _
                     & " SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID" _
                     & " And ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA And DIFFIDE_LETTERE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                     & "AND soggetti_contrattuali.cod_tipologia_occupante='INTE' "
        If sStringaSqlX <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSqlX
        End If


        '        sStringaSQL1 = sStringaSQL1 & " UNION " _
        '                     & "SELECT (CASE WHEN DIFFIDE_LETTERE.TIPO=0 THEN 'DOC. MANCANTE' ELSE 'NON RISPONDENTE' END) AS TIPOLOGIA,(SELECT TIPOLOGIA_ESITI_POSTALER.DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_ESITI_POSTALER WHERE ID=POSTALER_ESITI.ID_TIPO_ESITI_POSTALER) AS DESCRIZIONE , " _
        '                     & "TO_CHAR(TO_DATE(POSTALER_ESITI.DATA_ESITO,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_ESITO,POSTALER.ID,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,RAPPORTI_UTENZA.COD_CONTRATTO," _
        '& "TO_CHAR(TO_DATE(DIFFIDE_LETTERE.DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_GENERAZIONE,UNITA_CONTRATTUALE.ID_UNITA,'' AS FILIALE  " _
        '& " FROM " _
        '        & " SISCOM_MI.POSTALER, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.FILIALI_VIRTUALI, SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.DIFFIDE_LETTERE, " _
        '& " SISCOM_MI.ANAGRAFICA, SISCOM_MI.POSTALER_ESITI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.SOGGETTI_CONTRATTUALI " _
        '& " WHERE  POSTALER.ID=POSTALER_ESITI.ID_POSTALER (+) AND POSTALER.ID_LETTERA=DIFFIDE_LETTERE.ID AND TAB_FILIALI.ID=FILIALI_VIRTUALI.id_filiale (+) AND " _
        '& " FILIALI_VIRTUALI.ID_contratto=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND " _
        '& " UNITA_CONTRATTUALE.ID_contratto = RAPPORTI_UTENZA.ID AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND DIFFIDE_LETTERE.ID_AU = " & sValoreBA & " AND  SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND " _
        '& " ANAGRAFICA.ID = SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND DIFFIDE_LETTERE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.cod_tipologia_occupante='INTE' "

            'sStringaSQL1 = "SELECT RAPPORTI_UTENZA.ID AS IDC,COD_CONTRATTO,ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA,TO_CHAR(TO_DATE(DATA_DECORRENZA,'YYYYmmdd'),'DD/MM/YYYY') AS DECORRENZA,TO_CHAR(TO_DATE(DATA_RICONSEGNA,'YYYYmmdd'),'DD/MM/YYYY') AS SCADENZA, " _
            '            & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO_UNITA,INDIRIZZI.CIVICO AS CIVICO_UNITA,INDIRIZZI.CAP AS CAP_UNITA,INDIRIZZI.LOCALITA AS COMUNE_UNITA," _
            '            & "(TAB_FILIALI.NOME ) AS FILIALE  FROM SISCOM_MI.ANAGRAFICA, SISCOM_MI.SOGGETTI_CONTRATTUALI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.INDIRIZZI, SISCOM_MI.UNITA_IMMOBILIARI, siscom_mi.TAB_FILIALI, siscom_mi.COMPLESSI_IMMOBILIARI, siscom_mi.EDIFICI WHERE COMPLESSI_IMMOBILIARI.id_filiale=TAB_FILIALI.ID (+) AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.id_complesso AND EDIFICI.ID=UNITA_IMMOBILIARI.id_edificio  AND  " _
            '            & " (anagrafica.ragione_sociale is null or anagrafica.ragione_sociale='') and " _
            '            & " ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SUBSTR(COD_CONTRATTO,1,6)<>'000000' AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE=SUBSTR(RAPPORTI_UTENZA.COD_CONTRATTO,1,17)  " _
            '            & " AND (COD_TIPOLOGIA_CONTR_LOC='ERP' OR COD_TIPOLOGIA_CONTR_LOC='EQC392' or (COD_TIPOLOGIA_CONTR_LOC='CON2' AND EX_GESTORE='030')) AND (DATA_RICONSEGNA IS NULL OR DATA_RICONSEGNA>='20100101')" _
            '            & " AND COD_CONTRATTO IS NOT NULL AND SUBSTR(COD_CONTRATTO,1,1)<>'4' AND " _
            '            & " COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE COD_CONVOCAZIONE IS NULL AND RAPPORTO IS NOT NULL AND ID_BANDO=" & sValoreBA & " AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) " _
            '            & " AND RAPPORTI_UTENZA.ID NOT IN (SELECT ID_CONTRATTO FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & sValoreBA & ")"



        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
            End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY cognome ASC,nome asc"


        BindGrid()
    End Function


    Private Sub BindGrid()

        par.OracleConn.Open()
        Dim dt As New System.Data.DataTable

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
        da.Fill(dt)

        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        HttpContext.Current.Session.Add("AA1", dt)


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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaRicPostAler.aspx""</script>")
    End Sub


    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String
        Dim row As System.Data.DataRow
        Dim dt As New System.Data.DataTable

        dt = CType(HttpContext.Current.Session.Item("AA1"), Data.DataTable)
        HttpContext.Current.Session.Remove("AA1")

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


            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COGNOME", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "NOME", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "COD.CONTRATTO", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "FILIALE", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "DATA DIFFIDA", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DATA ESITO", 12)
            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "ESITO", 12)

            K = 2
            For Each row In dt.Rows
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COGNOME"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("NOME"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("COD_CONTRATTO"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("FILIALE"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_GENERAZIONE"), "")))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfEmpty(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DATA_ESITO"), "")), ""))
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfEmpty(par.PulisciStrSql(par.IfNull(dt.Rows(i).Item("DESCRIZIONE"), "")), ""))
                i = i + 1
                K = K + 1
            Next

            .CloseFile()
        End With

        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\Filetemp\" & sNomeFile & ".zip")

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
    End Sub


End Class
