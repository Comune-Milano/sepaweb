
Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_SLA
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub cmbPriorita_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPriorita.SelectedIndexChanged
        Try
            If CType(Me.Page, Object).lIdConnessione <> "" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            End If

            par.cmd.CommandText = "select SLA_VERIFICHE.ID AS ID_VERIFICA,SLA_VERIFICHE.DESCRIZIONE,SLA_default.ORE,SLA_default.GIORNI from SISCOM_MI.SLA_default,SISCOM_MI.SLA_VERIFICHE where SLA_VERIFICHE.ID=SLA_default.ID_VERIFICA AND SLA_default.id_priorita=" & cmbPriorita.SelectedItem.Value & " ORDER BY SLA_VERIFICHE.DESCRIZIONE"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.DataGridSLA.DataSource = dt
            Me.DataGridSLA.DataBind()

        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_SLA-Priorità"
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaPriorita()
            If CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1" Then
                FrmSoloLettura()
            End If
        End If
    End Sub

    Private Sub CaricaPriorita()
        Try
            If CType(Me.Page, Object).lIdConnessione <> "" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            Else
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            End If

            par.cmd.CommandText = "select SLA_TEMPI.ID_PRIORITA,SLA_TEMPI.ID_VERIFICA,SLA_VERIFICHE.DESCRIZIONE, SLA_TEMPI.ORE, SLA_TEMPI.GIORNI from SISCOM_MI.SLA_TEMPI, SISCOM_MI.SLA_VERIFICHE where SLA_VERIFICHE.ID = SLA_TEMPI.ID_VERIFICA And id_appalto = " & CType(Me.Page, Object).vIdAppalti & " ORDER BY SLA_VERIFICHE.DESCRIZIONE"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.DataGridSLA.DataSource = dt
            Me.DataGridSLA.DataBind()

            cmbPriorita.SelectedIndex = -1
            If dt.Rows.Count > 0 Then
                cmbPriorita.Items.FindItemByValue(dt.Rows(0).Item("ID_PRIORITA")).Selected = True
            Else
                cmbPriorita.Items.FindItemByValue("-1").Selected = True
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("LblErrore"), Label).Visible = True
            CType(Me.Page.FindControl("LblErrore"), Label).Text = ex.Message & " Tab_SLA"
        End Try
    End Sub
    Private Sub FrmSoloLettura()
        cmbPriorita.Enabled = False
    End Sub
End Class
