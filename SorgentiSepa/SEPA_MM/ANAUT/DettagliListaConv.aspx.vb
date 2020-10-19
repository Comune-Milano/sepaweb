Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_DettagliListaConv
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable
    Dim INDICEBANDO As Long = 0
    Dim DataInzio As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
            CaricaDatiLista()
            CaricaDati()
        End If
    End Sub

    Private Function CaricaDatiLista()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If Request.QueryString("ID") = "" Then
                'PAR.cmd.CommandText = "SELECT * FROM UTENZA_LISTE_CONV WHERE id=" & Request.QueryString("ID")
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                'If myReader.Read Then
                '    Label1.Text = PAR.IfNull(myReader("criteri"), "")
                '    Label3.Text = "LISTA " & PAR.IfNull(myReader("DESCRIZIONE"), "")
                '    Label4.Text = "Lista Convocabili"
                'End If
                'myReader.Close()
            Else
                PAR.cmd.CommandText = "SELECT * FROM UTENZA_LISTE WHERE id=" & Request.QueryString("ID")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                If myReader.Read Then
                    Label1.Text = PAR.IfNull(myReader("criteri"), "") & " - " & PAR.FormattaData(Mid(myReader("DATA_ORA"), 1, 8)) & " " & Mid(myReader("DATA_ORA"), 9, 2) & ":" & Mid(myReader("DATA_ORA"), 11, 2) & " - Operatore:" & myReader("OPERATORE")
                    Label3.Text = "LISTA DI CONVOCAZIONE " & PAR.IfNull(myReader("DESCRIZIONE"), "")
                    Label4.Text = ""
                End If
                myReader.Close()
            End If

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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


            If Request.QueryString("ID") = "" Then
                'Tabella = "SELECT * FROM UTENZA_LISTE_CDETT WHERE ID_LISTA=" & Request.QueryString("ID") & "  ORDER BY FILIALE,SEDE,INTESTATARIO"
            Else
                Tabella = "SELECT replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../Contratti/Contratto.aspx?LT=1$ID='||ID_contratto||''',''Contratto'',''height=780,width=1160'');£>'||COD_CONTRATTO||'</a>','$','&'),'£','" & Chr(34) & "') as  COD_CONTRATTO_1,UTENZA_LISTE_CDETT.* FROM UTENZA_LISTE_CDETT WHERE ID_LISTA_CONV=" & Request.QueryString("ID") & "  ORDER BY FILIALE,SEDE,INTESTATARIO"
            End If
            BindGrid()
            'PAR.cmd.CommandText = Tabella
            'da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
            'da.Fill(DT)


            'DataGridRateEmesse.DataSource = DT
            'DataGridRateEmesse.DataBind()
            'Session.Add("MIADT", DT)
            'Label2.Text = DT.Rows.Count & " nella lista"
        Catch ex As Exception

        End Try
    End Function

    Private Sub BindGrid()
        Dim da As Oracle.DataAccess.Client.OracleDataAdapter

        PAR.cmd.CommandText = Tabella
        da = New Oracle.DataAccess.Client.OracleDataAdapter(Tabella, PAR.OracleConn)
        da.Fill(DT)

        DataGridRateEmesse.DataSource = DT
        DataGridRateEmesse.DataBind()
        Session.Add("MIADT", DT)
        Label2.Text = DT.Rows.Count & " nella lista"

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

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    Try


    '        Dim myExcelFile As New CM.ExcelFile
    '        Dim i As Long
    '        Dim K As Long
    '        Dim sNomeFile As String
    '        Dim row As System.Data.DataRow

    '        DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
    '        sNomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")

    '        i = 0

    '        With myExcelFile

    '            .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
    '            .PrintGridLines = False
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
    '            .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
    '            .SetDefaultRowHeight(14)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
    '            .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
    '            .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)

    '            K = 1
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, Label1.Text)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, "")
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, "")

    '            K = 2
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CONTRATTO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "INTESTATARIO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "QUARTIERE", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "EDIFICIO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "INDIRIZZO UNITA", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "TIPO IND. CORR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "IND. CORR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "CIVICO CORR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "LUOGO CORR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "PROVINCIA CORR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "CAP CORR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "FILIALE", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "SEDE", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "TIPOLOGIA CONTR.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "TIPO SPECIFICO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, "DATA STIPULA", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, "DATA SLOGGIO", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, "UNITA VENDUTA", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, "BOLLETTAZIONE SPESE", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, "NUM.COMPONENTI", 12)

    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, "MINORI 15 ANNI", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, "MAGGIORI 65 ANNI", 12)

    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, "NUM.COMP. 66-99% INV.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, "NUM.COMP. 100% INV.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, "NUM.COMP. 100% INV. ACC.", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, "REDD. PREVALENTEMENTE DIPENDENTE", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, "REDDITI IMMOBILIARI", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, "AREA ECONOMICA", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, "CLASSE", 12)
    '            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, "MOTIVAZIONE ESCLUSIONE", 12)



    '            K = 3
    '            For Each row In DT.Rows
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("COD_CONTRATTO"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INTESTATARIO"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("QUARTIERE"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("EDIFICIO"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("INDIRIZZO_UI"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_COR"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("VIA_COR"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CIVICO_COR"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("LUOGO_COR"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("SIGLA_COR"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CAP_COR"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("FILIALE"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("SEDE"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_CONTRATTO"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("TIPO_SPECIFICO"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 16, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_STIPULA"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 17, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("DATA_SLOGGIO"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 18, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("UI_VENDUTA"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 19, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("BOLL_SPESE"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 20, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP"), "")))

    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 21, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("MINORI_15"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 22, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("MAGGIORI_65"), "")))


    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 23, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP_66"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 24, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP_100"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 25, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("NUM_COMP_100_CON"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 26, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("PREVALENTE_DIPENDENTE"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 27, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("REDDITI_IMMOBILIARI"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 28, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("AREA_ECONOMICA"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 29, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("CLASSE"), "")))
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 30, PAR.PulisciStrSql(PAR.IfNull(DT.Rows(i).Item("MOTIVAZIONE"), "")))

    '                i = i + 1
    '                K = K + 1
    '            Next

    '            .CloseFile()
    '        End With

    '        Dim objCrc32 As New Crc32()
    '        Dim strmZipOutputStream As ZipOutputStream
    '        Dim zipfic As String

    '        zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")

    '        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '        strmZipOutputStream.SetLevel(6)
    '        '
    '        Dim strFile As String
    '        strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
    '        Dim strmFile As FileStream = File.OpenRead(strFile)
    '        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '        '
    '        strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '        Dim sFile As String = Path.GetFileName(strFile)
    '        Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '        Dim fi As New FileInfo(strFile)
    '        theEntry.DateTime = fi.LastWriteTime
    '        theEntry.Size = strmFile.Length
    '        strmFile.Close()
    '        objCrc32.Reset()
    '        objCrc32.Update(abyBuffer)
    '        theEntry.Crc = objCrc32.Value
    '        strmZipOutputStream.PutNextEntry(theEntry)
    '        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '        strmZipOutputStream.Finish()
    '        strmZipOutputStream.Close()

    '        File.Delete(strFile)
    '        Response.Redirect("..\FileTemp\" & sNomeFile & ".zip")


    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Sub

    Protected Sub DataGridRateEmesse_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridRateEmesse.PageIndexChanged
        If e.NewPageIndex >= 0 Then

            DataGridRateEmesse.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub DataGridRateEmesse_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DataGridRateEmesse.SelectedIndexChanged

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            If DT.Rows.Count > 0 Then

                'DT.Columns.Remove("IDC")
                'DT.Columns.Remove("ID_FILIALE")
                'DT.Columns.Remove("ID_SPORTELLO")

                Dim nomefile As String = PAR.EsportaExcelDaDTWithDatagrid(DT, DataGridRateEmesse, "ExportAssegnatari", , False, , False)

                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
