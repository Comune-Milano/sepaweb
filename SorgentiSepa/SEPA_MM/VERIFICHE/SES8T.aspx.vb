Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class VERIFICHE_SES8T
    Inherits PageSetIdMode
    Dim OracleConn As Oracle.DataAccess.Client.OracleConnection
    Dim cmd As New Oracle.DataAccess.Client.OracleCommand()
    Dim PAR As New CM.Global
    Public Altezza As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Me.TextBox1.Focus()

            If dgvDocumenti.ExportSettings.IgnorePaging = True Then
                btnExport1.Text = "EXPORT CON PAGINAZIONE= OFF"
            Else
                btnExport1.Text = "EXPORT CON PAGINAZIONE= ON"
            End If
        Else
            Me.TextBox2.Focus()
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If PAR.VerificaPW(TextBox1.Text) = True Then
                Button2.Visible = True
                TextBox2.Visible = True
                btnExport.Visible = True
                btnExport1.Visible = True
                Me.TextBox2.Focus()
            End If
        End If
    End Sub

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

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        Try
            dgvDocumenti.Visible = True
            BindGrid()

        Catch ex As Exception
            dgvDocumenti.Visible = False
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDocumenti.NeedDataSource
        If sStrSql <> "" Then
            TryCast(sender, RadGrid).DataSource = getDataTableGrid(sStrSql)

        End If
    End Sub

    Private Function getDataTableGrid(ByVal Query As String) As Data.DataTable
        OracleConn = New Oracle.DataAccess.Client.OracleConnection(PAR.StringaSiscom)
        getDataTableGrid = New Data.DataTable
        Dim ConOpenNow As Boolean = False
        If OracleConn.State = Data.ConnectionState.Closed Then
            ConOpenNow = True
            OracleConn.Open()
            cmd = OracleConn.CreateCommand()
        End If
        cmd.CommandText = Query
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(cmd)
        da.Fill(getDataTableGrid)
        da.Dispose()
        If ConOpenNow = True Then
            OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If
    End Function

    Private Sub BindGrid()
        sStrSql = TextBox2.Text
        dgvDocumenti.Rebind()
    End Sub

    Protected Sub btnExport1_Click(sender As Object, e As System.EventArgs) Handles btnExport1.Click
        If dgvDocumenti.ExportSettings.IgnorePaging = True Then
            dgvDocumenti.ExportSettings.IgnorePaging = False
        Else
            dgvDocumenti.ExportSettings.IgnorePaging = True
        End If
        If dgvDocumenti.ExportSettings.IgnorePaging = True Then
            btnExport1.Text = "EXPORT CON PAGINAZIONE= OFF"
        Else
            btnExport1.Text = "EXPORT CON PAGINAZIONE= ON"
        End If
        dgvDocumenti.Rebind()
    End Sub
End Class
