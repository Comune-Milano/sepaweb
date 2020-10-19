Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_ElencoVerificheChiusura
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable
  
    Dim INDICEBANDO As Long = 0
    Dim DataInzio As String = ""

    Dim Tipo1 As Integer = 0
    Dim Tipo2 As Integer = 0
    Dim Tipo3 As Integer = 0
    Dim Tipo4 As Integer = 0
    Dim Tipo5 As Integer = 0
    Dim Tipo6 As Integer = 0
    Dim Tipo7 As Integer = 0
    Dim Tipo8 As Integer = 0
    Dim Tipo9 As Integer = 0
    Dim Tipo10 As Integer = 0
    Dim Tipo11 As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDatiAU()
            CaricaDati()
        End If

    End Sub

    Public Property ClasseB1() As String
        Get
            If Not (ViewState("par_ClasseB1") Is Nothing) Then
                Return CStr(ViewState("par_ClasseB1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ClasseB1") = value
        End Set
    End Property

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

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
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



                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "SPORTELLO/FILIALE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COD.CONTRATTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "DATA APP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "ORE APP.", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "TIPO DIFFIDA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DATA GENERAZIONE DIFFIDA", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "PROTOCOLO AU", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "DATA INSERIMENTO AU", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "STATO AU", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "NUM. COMPONENTI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "N.INVALIDI 100% INDENNITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "N.INVALIDI 100%", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "N.INVALIDI TRA 66%-99%", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 14, "ISEE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 15, "ISE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 16, "ISR", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 17, "ISP", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 18, "PSE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 19, "VSE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 20, "MINORI 15 ANNI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 21, "MAGGIORI 65 ANNI", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 22, "LIMITE PATR.SUPERATO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 23, "REDD. PREVAL.DIPENDENTE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 24, "CLASSE B1", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 25, "AU DA VERIFICARE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 26, "SOSP. PER VAR.INTESTAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 27, "SOSP. PER CAMBIO INTESTAZIONE", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 28, "SOSP. PER VER.TITOLARITA'", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 29, "SOSP. PER DECRETO RILASCIO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 30, "SOSP. PER AMPLIAMENTO", 12)
                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 31, "SOSP.PER DOC.MANCANTE", 12)


                K = 2
                For Each row In DT.Rows
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("SPORTELLO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD_CONTRATTO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_APP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ORE_APP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DIFFIDA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_GENERAZIONE_DIFFIDA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PG_AU"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_INSERIMENTO_AU"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("STATO_AU"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMPONENTI"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("N_INV_100_CON"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("N_INV_100_SENZA"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("N_INV_100_66"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ISEE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ISE_ERP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ISR_ERP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("ISP_ERP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PSE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("VSE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PRESENZA_MIN_15"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PRESENZA_MAG_65"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PATR_SUPERATO"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PREVALENTE_DIP"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INIZIO_B1"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_DA_VERIFICARE"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_SOSP_1"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_SOSP_2"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_SOSP_3"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_SOSP_4"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_SOSP_5"), "")))
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 31, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FL_SOSP_7"), "")))
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

    Private Function CaricaB1(anno As String)
        Try
            ClasseB1 = "??"
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & anno & " WHERE sotto_area='A5'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                ClasseB1 = "DA " & CDbl(myReader("ISEE_ERP")) + 0.01
            End If
            myReader.Close()

            PAR.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & anno & " WHERE SOTTO_AREA='B1'"
            myReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                ClasseB1 = ClasseB1 & " A " & myReader("ISEE_ERP")
            End If
            myReader.Close()
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 942 Then
                PAR.OracleConn.Close()
                PAR.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label4.Text = "ATTENZIONE...non è stata trovata la tabella classi isee per questa AU. Assicurarsi di aver inserito i valori necessari tramite la funzione presente nella maschera di Apertura/Chiusura AU nel menu"
                Label4.Visible = True

            Else
                Label1.Text = EX1.Message
                Label1.Visible = True
                PAR.OracleConn.Close()
                PAR.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Function

    Private Function CaricaDatiAU()
        Try
            Dim AnnoAU As String = ""

            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & Request.QueryString("IDB")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then

                INDICEBANDO = PAR.IfNull(myReader("ID"), 0)
                DataInzio = PAR.IfNull(myReader("DATA_INIZIO"), "")

                AnnoAU = myReader("anno_au")

                Tipo1 = PAR.IfNull(myReader("ERP_1"), 0) 'ERP SOCIALE
                Tipo2 = PAR.IfNull(myReader("ERP_2"), 0) 'ERP MODERATO
                Tipo3 = PAR.IfNull(myReader("ERP_ART_22"), 0) 'ART 200 C 10
                Tipo4 = PAR.IfNull(myReader("ERP_4"), 0)
                Tipo5 = PAR.IfNull(myReader("ERP_5"), 0)
                Tipo10 = PAR.IfNull(myReader("ERP_3"), 0)
                Tipo6 = PAR.IfNull(myReader("L43198"), 0)
                Tipo7 = PAR.IfNull(myReader("L39278"), 0)
                Tipo8 = PAR.IfNull(myReader("ERP_FF_OO"), 0) 'FF.OO.
                Tipo9 = PAR.IfNull(myReader("ERP_CONV"), 0) 'ERP CONVENZIONATO
                Tipo11 = PAR.IfNull(myReader("OA"), 0)

                Dim ss As String = ""

                If Tipo1 = 1 Then ss = ss & "Erp Sociale,"
                If Tipo2 = 1 Then ss = ss & "Erp Moderato,"
                If Tipo3 = 1 Then ss = ss & "ART.22 C.10 RR 1/2004,"
                If Tipo4 = 1 Then ss = ss & "4.	art.15 comma 2-vizi amministrativi,"
                If Tipo5 = 1 Then ss = ss & "5.	Legge 10/86,"
                If Tipo6 = 1 Then ss = ss & "431/98,"
                If Tipo7 = 1 Then ss = ss & "392/78,"
                If Tipo8 = 1 Then ss = ss & "Erp FF.OO.,"
                If Tipo9 = 1 Then ss = ss & "Erp Convenzionato,"
                If Tipo10 = 1 Then ss = ss & "Erp Art.15 let. a, b - 431 Deroga,"
                If Tipo11 = 1 Then ss = ss & "Occupazioni Abusive,"
                ss = Mid(ss, 1, Len(ss) - 1)
                Label1.Text = "Convocabili AU: " & PAR.IfNull(myReader("descrizione"), "") & " (" & ss & ") - " & PAR.DeCripta(Request.QueryString("Q")) & " - " & Format(Now, "dd/MM/yyyy HH:mm") & " - " & Session.Item("operatore")
                Label3.Text = " Anno " & myReader("anno_au") & " Redditi " & myReader("anno_isee")
            End If
            myReader.Close()

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaB1(annoAU)
        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Function

    Private Function CaricaDati()
        Try
            Dim S As String = ""
            Dim S1 As String = ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim S2 As String = ""
            'Dim ss As String = "("

            'If Request.QueryString("TIPOC") = "" Then

            '    If Tipo1 = 1 Then 'erp sociale
            '        ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
            '    End If

            '    If Tipo2 = 1 Then 'erp moderato
            '        ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
            '    End If

            '    If Tipo3 = 1 Then 'ART 22 C 10
            '        ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
            '    End If

            '    If Tipo8 = 1 Then 'FF.OO.
            '        ss = ss & " unita_immobiliari.id_destinazione_uso = 10 or "
            '    End If

            '    If Tipo9 = 1 Then 'convenzionato
            '        ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
            '    End If

            '    If Tipo6 = 1 Then
            '        ss = ss & " rapporti_utenza.dest_uso = 'P' or rapporti_utenza.dest_uso = 'S' OR rapporti_utenza.dest_uso = '0' or "
            '    End If

            '    If Tipo10 = 1 Then
            '        ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
            '    End If

            '    If Tipo4 = 1 And ss = "(" Then
            '        ss = ss & "rapporti_utenza.dest_uso='X' OR "
            '    End If

            '    If Tipo5 = 1 And ss = "(" Then
            '        ss = ss & "rapporti_utenza.dest_uso='X' OR "
            '    End If

            '    If Tipo11 = 1 Then 'OCCUPAZIONI ABUSIVE
            '        ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
            '    End If

            '    If ss = "(" Then
            '        ss = "(rapporti_utenza.dest_uso='X') "
            '    Else
            '        ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
            '    End If

            'Else

            '    Select Case Request.QueryString("TIPOC")
            '        Case "1"
            '            ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
            '        Case "2"
            '            ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
            '        Case "3"
            '            ss = ss & " unita_immobiliari.id_destinazione_uso = 10 or "
            '        Case "4"
            '            ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
            '        Case "5"
            '            ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
            '        Case "6"

            '        Case "7"
            '            ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
            '        Case "8"

            '        Case "9"

            '        Case "10"
            '            ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
            '    End Select

            '    If ss = "(" Then
            '        ss = "(rapporti_utenza.dest_uso='X') "
            '    Else
            '        ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
            '    End If

            'End If

           
            Tabella = "SELECT " _
                    & "NULL AS PG_AU, " _
                    & "NULL AS DATA_INSERIMENTO_AU, " _
                    & "NULL AS STATO_AU, " _
                    & "NULL AS NUM_COMPONENTI, " _
                    & "NULL AS N_INV_100_CON, " _
                    & "NULL AS N_INV_100_SENZA, " _
                    & "NULL AS N_INV_100_66, " _
                    & "NULL AS ISEE, " _
                    & "NULL AS ISE_ERP, " _
                    & "NULL AS ISR_ERP, " _
                    & "NULL AS ISP_ERP, " _
                    & "NULL AS PSE, " _
                    & "NULL AS VSE, " _
                    & "NULL AS PATR_SUPERATO, " _
                    & "NULL AS PREVALENTE_DIP, " _
                    & "NULL AS PRESENZA_MIN_15, " _
                    & "NULL AS PRESENZA_MAG_65, " _
                    & "NULL AS FL_DA_VERIFICARE, " _
                    & "NULL AS FL_SOSP_1, " _
                    & "NULL AS FL_SOSP_2, " _
                    & "NULL AS FL_SOSP_3, " _
                    & "NULL AS FL_SOSP_4, " _
                    & "NULL AS FL_SOSP_5, " _
                    & "NULL AS FL_SOSP_7, " _
                    & "'" & ClasseB1 & "' AS INIZIO_B1, " _
                    & "(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & INDICEBANDO & ") AS DIFFIDA, " _
                    & "(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & INDICEBANDO & ") AS DATA_GENERAZIONE_DIFFIDA, " _
                    & "RAPPORTI_UTENZA.COD_CONTRATTO,CONVOCAZIONI_AU.ID_CONTRATTO,CONVOCAZIONI_AU.ID_GRUPPO,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_APP,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_APP, " _
                    & "CONVOCAZIONI_AU.ORE_APP,CONVOCAZIONI_AU.ID_FILIALE,CONVOCAZIONI_AU.ID_SPORTELLO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO " _
                    & "FROM  " _
                    & "siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA,UTENZA_SPORTELLI " _
                    & "WHERE " _
                    & " " & Session.Item("SX") _
                    & " unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL AND unita_immobiliari.ID=unita_contrattuale.id_unita AND " _
                    & "UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND " _
                    & "RAPPORTI_UTENZA.COD_CONTRATTO NOT IN (SELECT RAPPORTO FROM UTENZA_DICHIARAZIONI WHERE NVL(ID_BANDO,'0')=" & INDICEBANDO & ") AND " _
                    & "RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND CONVOCAZIONI_AU.ID_STATO<>1 AND  " _
                    & "CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE fl_confermata=1 AND fl_stampata=1 AND id_au=" & INDICEBANDO & ")  " _
                     & "" _
                    & "UNION " _
                     & "" _
                    & "SELECT " _
                    & "UTENZA_DICHIARAZIONI.PG AS PG_AU, " _
                    & "TO_CHAR(TO_DATE(UTENZA_DICHIARAZIONI.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INSERIMENTO_AU, " _
                    & "(CASE WHEN UTENZA_DICHIARAZIONI.ID_STATO=0 THEN 'DA COMPLETARE' WHEN UTENZA_DICHIARAZIONI.ID_STATO=1 THEN 'COMPLETA' WHEN UTENZA_DICHIARAZIONI.ID_STATO=2 THEN 'DA CANCELLARE' END) AS STATO_AU, " _
                    & "UTENZA_DICHIARAZIONI.N_COMP_NUCLEO AS NUM_COMPONENTI, " _
                    & "UTENZA_DICHIARAZIONI.N_INV_100_CON, " _
                    & "UTENZA_DICHIARAZIONI.N_INV_100_SENZA, " _
                    & "UTENZA_DICHIARAZIONI.N_INV_100_66, " _
                    & "UTENZA_DICHIARAZIONI.ISEE, " _
                    & "UTENZA_DICHIARAZIONI.ISE_ERP, " _
                    & "UTENZA_DICHIARAZIONI.ISR_ERP, " _
                    & "UTENZA_DICHIARAZIONI.ISP_ERP, " _
                    & "UTENZA_DICHIARAZIONI.PSE, " _
                    & "UTENZA_DICHIARAZIONI.VSE, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.PATR_SUPERATO,'0','NO','1','SI') AS PATR_SUPERATO, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.PREVALENTE_DIP,'0','NO','1','SI') AS PREVALENTE_DIP, " _
                    & "UTENZA_DICHIARAZIONI.PRESENZA_MIN_15, " _
                    & "UTENZA_DICHIARAZIONI.PRESENZA_MAG_65, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE,'0','NO','1','SI') AS FL_DA_VERIFICARE, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_SOSP_1,'0','NO','1','SI') AS FL_SOSP_1, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_SOSP_2,'0','NO','1','SI') AS FL_SOSP_2, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_SOSP_3,'0','NO','1','SI') AS FL_SOSP_3, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_SOSP_4,'0','NO','1','SI') AS FL_SOSP_4, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_SOSP_5,'0','NO','1','SI') AS FL_SOSP_5, " _
                    & "DECODE(UTENZA_DICHIARAZIONI.FL_SOSP_7,'0','NO','1','SI') AS FL_SOSP_7, " _
                    & "'" & ClasseB1 & "' AS INIZIO_B1, " _
                    & "(SELECT (CASE WHEN TIPO=0 THEN 'INCOMPLETA' ELSE 'NON RISPONDENTE' END) FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & INDICEBANDO & ") AS DIFFIDA, " _
                    & "(SELECT TO_CHAR(TO_DATE(DATA_GENERAZIONE,'YYYYmmdd'),'DD/MM/YYYY') FROM SISCOM_MI.DIFFIDE_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND ID_AU=" & INDICEBANDO & ") AS DATA_GENERAZIONE_DIFFIDA, " _
                    & "RAPPORTI_UTENZA.COD_CONTRATTO,CONVOCAZIONI_AU.ID_CONTRATTO,CONVOCAZIONI_AU.ID_GRUPPO,TO_CHAR(TO_DATE(CONVOCAZIONI_AU.DATA_APP,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_APP, " _
                    & "CONVOCAZIONI_AU.ORE_APP,CONVOCAZIONI_AU.ID_FILIALE,CONVOCAZIONI_AU.ID_SPORTELLO,UTENZA_SPORTELLI.DESCRIZIONE AS SPORTELLO " _
                    & "FROM  " _
                    & "siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,SISCOM_MI.CONVOCAZIONI_AU,SISCOM_MI.RAPPORTI_UTENZA,UTENZA_DICHIARAZIONI,UTENZA_SPORTELLI " _
                    & "WHERE " _
                    & " " & Session.Item("SX1") _
                    & " unita_contrattuale.id_contratto=rapporti_utenza.ID AND unita_contrattuale.id_unita_principale IS NULL AND unita_immobiliari.ID=unita_contrattuale.id_unita AND " _
                    & "UTENZA_SPORTELLI.ID=CONVOCAZIONI_AU.ID_SPORTELLO AND " _
                    & "RAPPORTI_UTENZA.COD_CONTRATTO=UTENZA_DICHIARAZIONI.RAPPORTO AND UTENZA_DICHIARAZIONI.ID_BANDO=" & INDICEBANDO & " AND  " _
                    & "RAPPORTI_UTENZA.ID=CONVOCAZIONI_AU.ID_CONTRATTO AND CONVOCAZIONI_AU.ID_STATO<>1 AND  " _
                    & "CONVOCAZIONI_AU.ID_GRUPPO IN (SELECT ID FROM SISCOM_MI.CONVOCAZIONI_AU_GRUPPI WHERE fl_confermata=1 AND fl_stampata=1 AND id_au=" & INDICEBANDO & ")  "

            PAR.cmd.CommandText = Tabella
            da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
            da.Fill(DT)


            DataGridRateEmesse.DataSource = DT
            DataGridRateEmesse.DataBind()
            Session.Add("MIADT", DT)
            Label2.Text = " - " & DT.Rows.Count & " nella lista"
            Session.Remove("SX")
        Catch ex As Exception
            Label1.Text = ex.Message
        End Try
    End Function
End Class
