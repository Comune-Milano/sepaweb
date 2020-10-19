Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class Contratti_CruscottoElabCauzT
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public Altezza As Int64 = 0
    Dim xls As New ExcelSiSol


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Dim Str As String = ""

            If Not IsPostBack Then
                sIndiceReport = Request.QueryString("IDELAB")
                Titolo()
                BindGrid()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Elaborazione interessi su deposito cauzionale - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub Titolo()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim testo As String = ""
            Dim id_from As String
            Dim id_to As String
            Dim param() As String
            Dim l_where As String

            param = Split(sIndiceReport, "@")
            id_from = param(0)
            id_to = param(1)

            l_where = " and ADEGUAMENTO_INTERESSI.IMPORTO <> 0 AND ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)

            par.cmd.CommandText = "SELECT DISTINCT substr(adeguamento_interessi.data,1,4) FROM SISCOM_MI.ADEGUAMENTO_INTERESSI WHERE ADEGUAMENTO_INTERESSI.IMPORTO <> 0 " & l_where & " order by 1 asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                testo = testo & myReader1(0) & "-"
            End While
            myReader1.Close()
            If testo <> "" Then
                testo = Mid(testo, 1, Len(testo) - 1)
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = Label1.Text & " Anno " & testo

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Private Sub BindGrid()
        Dim id_from As String
        Dim id_to As String
        Dim param() As String
        Dim l_where As String

        param = Split(sIndiceReport, "@")
        id_from = param(0)
        id_to = param(1)

        l_where = " WHERE ADEGUAMENTO_INTERESSI.IMPORTO <> 0 AND ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)

        sStrSql = "SELECT DISTINCT TIPOLOGIA_CONTRATTO_LOCAZIONE.COD," _
                    & " TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS DESCRIZIONE_TIPO, " _
                    & " SUM(ADEGUAMENTO_INTERESSI.IMPORTO) AS IMPORTO_TOTALE " _
                    & "         FROM SISCOM_MI.ADEGUAMENTO_INTERESSI " _
                    & " INNER JOIN SISCOM_MI.RAPPORTI_UTENZA ON (RAPPORTI_UTENZA.ID=ID_CONTRATTO) " _
                    & " INNER JOIN SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ON (TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) " _
                    & l_where _
                    & " GROUP BY TIPOLOGIA_CONTRATTO_LOCAZIONE.COD, TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE " _
                    & " ORDER BY 2"

    End Sub

    Public Property sIndiceReport() As String
        Get
            If Not (ViewState("par_sIndiceReport") Is Nothing) Then
                Return CStr(ViewState("par_sIndiceReport"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIndiceReport") = value
        End Set
    End Property

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Protected Sub dgvDocumenti_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles dgvDocumenti.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                If True Then
                    Dim IndiceS As String = Replace(dataItem("COD").Text, "&nbsp;", "-1")
                    Dim id_from As String
                    Dim id_to As String
                    Dim param() As String
                    Dim l_where As String

                    param = Split(sIndiceReport, "@") 'Split(Request.QueryString("IDELAB"), "@")
                    id_from = param(0)
                    id_to = param(1)

                    l_where = " AND ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to) & " AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = '" & IndiceS & "'"

                   
                    Dim sstrSql1 As String = " SELECT DISTINCT SUBSTR (ADEGUAMENTO_INTERESSI_VOCI.DAL, 1, 4) AS ANNO," _
                             & " SUM (ADEGUAMENTO_INTERESSI_VOCI.IMPORTO) AS IMPORTO " _
                             & "FROM SISCOM_MI.ADEGUAMENTO_INTERESSI, SISCOM_MI.ADEGUAMENTO_INTERESSI_VOCI, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                             & "WHERE TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC And RAPPORTI_UTENZA.ID = ADEGUAMENTO_INTERESSI.ID_CONTRATTO And " _
                             & "ADEGUAMENTO_INTERESSI_VOCI.ID_ADEGUAMENTO = ADEGUAMENTO_INTERESSI.ID And ADEGUAMENTO_INTERESSI_VOCI.IMPORTO <> 0 " & l_where _
                             & " GROUP BY SUBSTR (ADEGUAMENTO_INTERESSI_VOCI.DAL, 1, 4) ORDER BY 1"


                    e.DetailTableView.DataSource = par.getDataTableGrid(sstrSql1)
                End If
            Case "Dettagli1"
                '////////////////////////////////
                ' Eventuale ulteriore dettaglio
        End Select
    End Sub

    Protected Sub dgvDocumenti_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvDocumenti.ItemCommand
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
            'If e.CommandName = "My_ExportToExcel" Then
            export()
        End If

    End Sub

    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDocumenti.NeedDataSource
        Try
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Elaborazione Interessi Cauzionali - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Sub export()
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim DT As New Data.DataTable
            '            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            Dim id_from As String
            Dim id_to As String
            Dim param() As String
            Dim l_where As String

            param = Split(sIndiceReport, "@")
            id_from = param(0)
            id_to = param(1)

            l_where = " WHERE ADEGUAMENTO_INTERESSI.IMPORTO <> 0 AND ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)

            sStrSql = "SELECT DISTINCT TIPOLOGIA_CONTRATTO_LOCAZIONE.COD, TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS DESCRIZIONE_TIPO,  " _
                        & " SUBSTR(ADEGUAMENTO_INTERESSI.DATA,1,4) as ANNO, " _
                        & " SUM(ADEGUAMENTO_INTERESSI.IMPORTO) AS IMPORTO " _
                        & " FROM SISCOM_MI.ADEGUAMENTO_INTERESSI " _
                        & " INNER JOIN SISCOM_MI.RAPPORTI_UTENZA ON (RAPPORTI_UTENZA.ID=ID_CONTRATTO) " _
                        & " INNER JOIN SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ON (TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) " _
                        & l_where _
                        & " GROUP BY TIPOLOGIA_CONTRATTO_LOCAZIONE.COD, TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE, SUBSTR(ADEGUAMENTO_INTERESSI.DATA,1,4) " _
                        & " ORDER BY 1, 2"

            DT = par.getDataTableGrid(sStrSql)

            Dim nomefile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportElaborazioneInteressi", "Foglio", DT)

            If IO.File.Exists(Server.MapPath("..\filetemp\" & nomefile)) Then
                Response.Redirect("..\FileTemp\" & nomefile)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
