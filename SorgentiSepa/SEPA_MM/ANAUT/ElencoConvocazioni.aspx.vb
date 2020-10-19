Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoConvocazioni
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                Dim S As String = ""
                Dim S1 As String = ""
                Dim da As Oracle.DataAccess.Client.OracleDataAdapter
                Dim S2 As String = ""

                If PAR.DeCriptaMolto(Request.QueryString("idF")) <> "TUTTE LE SEDI" Then
                    S = "AND ID_FILIALE=" & PAR.DeCriptaMolto(Request.QueryString("idF")) & "  "
                End If

                If PAR.DeCriptaMolto(Request.QueryString("idO")) <> "TUTTI GLI SPORTELLI" And PAR.DeCriptaMolto(Request.QueryString("idO")) <> "-1" Then
                    S1 = "ID_SPORTELLO=" & PAR.DeCriptaMolto(Request.QueryString("idO")) & " and "
                End If

                If PAR.DeCriptaMolto(Request.QueryString("idL")) <> "TUTTE LE LISTE" And PAR.DeCriptaMolto(Request.QueryString("idL")) <> "-1" Then
                    S2 = "CONVOCAZIONI_AU_GRUPPI.ID=" & PAR.DeCriptaMolto(Request.QueryString("idL")) & " AND "
                End If

                'Tabella = "SELECT CONVOCAZIONI_AU_GRUPPI.DESCRIZIONE AS DESCRIZIONE_CON,TAB_FILIALI.NOME AS FILIALE,CONVOCAZIONI_AU.N_OPERATORE AS OPERATORE,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_APP,'YYYYmmdd'),'DD/MM/YYYY') AS GIORNO,CONVOCAZIONI_AU.ORE_APP AS INIZIO," _
                '        & "CONVOCAZIONI_AU.ORE_FINE_APP AS FINE,RAPPORTI_UTENZA.COD_CONTRATTO,CONVOCAZIONI_AU.COGNOME,CONVOCAZIONI_AU.NOME,TO_CHAR(CONVOCAZIONI_AU.ID,'0000000000') AS N_CONVOCAZIONE " _
                '        & "FROM " _
                '        & "SISCOM_MI.CONVOCAZIONI_AU_GRUPPI,SISCOM_MI.TAB_FILIALI, SISCOM_MI.CONVOCAZIONI_AU, SISCOM_MI.RAPPORTI_UTENZA " _
                '        & "WHERE " _
                '        & S1 & " " _
                '        & "CONVOCAZIONI_AU_GRUPPI.ID=CONVOCAZIONI_AU.ID_GRUPPO AND TAB_FILIALI.ID=CONVOCAZIONI_AU.ID_FILIALE AND RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO " & S & " AND N_OPERATORE IS NOT NULL AND " _
                '        & S2 & " CONVOCAZIONI_AU_GRUPPI.ID_AU=" & PAR.DeCriptaMolto(Request.QueryString("idB")) & " AND CONVOCAZIONI_AU.ID IN (SELECT id_convocazione FROM siscom_mi.CONVOCAZIONI_AU_LETTERE)" _
                '        & "ORDER BY FILIALE ASC,DATA_APP ASC,INIZIO ASC,TO_NUMBER(N_OPERATORE) ASC"

                Tabella = "SELECT CONVOCAZIONI_AU_GRUPPI.DESCRIZIONE AS DESCRIZIONE_CON,TAB_FILIALI.NOME AS FILIALE,'' AS OPERATORE,CONVOCAZIONI_AU_LETTERE.DATA_APP AS GIORNO,CONVOCAZIONI_AU_LETTERE.ORE_APP AS INIZIO," _
                       & "'' AS FINE,CONVOCAZIONI_AU_LETTERE.COD_CONTRATTO,CONVOCAZIONI_AU_LETTERE.INDIRIZZO_1 AS NOMINATIVO,'' AS NOME,TO_CHAR(CONVOCAZIONI_AU_LETTERE.ID_CONVOCAZIONE,'0000000000') AS N_CONVOCAZIONE " _
                       & "FROM " _
                       & "SISCOM_MI.CONVOCAZIONI_AU_GRUPPI,SISCOM_MI.TAB_FILIALI, SISCOM_MI.CONVOCAZIONI_AU_LETTERE " _
                       & "WHERE " _
                       & S1 & " " _
                       & "CONVOCAZIONI_AU_GRUPPI.ID=CONVOCAZIONI_AU_LETTERE.ID_GRUPPO AND TAB_FILIALI.ID=CONVOCAZIONI_AU_LETTERE.ID_FILIALE " & S & " AND " _
                       & S2 & " CONVOCAZIONI_AU_GRUPPI.ID_AU=" & PAR.DeCriptaMolto(Request.QueryString("idB")) & " " _
                       & "ORDER BY FILIALE ASC,SUBSTR(DATA_APP,7,4) ASC,SUBSTR(DATA_APP,4,2) ASC,SUBSTR(DATA_APP,1,2) ASC,INIZIO ASC"



                PAR.cmd.CommandText = Tabella
                da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
                da.Fill(DT)


                DataGridRateEmesse.DataSource = DT
                DataGridRateEmesse.DataBind()
                Session.Add("MIADT", DT)

            Catch ex As Exception

            End Try
        End If
    End Sub

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try


            Dim myExcelFile As New CM.ExcelFile
            Dim i As Long
            Dim K As Long
            Dim sNomeFile As String
            Dim row As System.Data.DataRow

            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "FILIALE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "N.CONVOCAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "GIORNO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ORA INIZIO", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "ORA FINE", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "SPORTELLO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "COD.CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NOMINATIVO.", 12)
                '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "NOME INT.", 12)


                K = 2
                For Each row In DT.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FILIALE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("N_CONVOCAZIONE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("GIORNO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INIZIO"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FINE"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("OPERATORE"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD_CONTRATTO"), 0)))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NOMINATIVO"), 0)))
                    '.WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NOME"), 0)))
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
