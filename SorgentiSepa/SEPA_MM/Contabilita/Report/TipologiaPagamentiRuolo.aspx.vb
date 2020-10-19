
Partial Class Contabilita_Report_TipologiaPagamentiRuolo
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaTipologie()
        End If
    End Sub
    Private Sub caricaTipologie()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT " _
                & " TIPO_PAG_RUOLO.DESCRIZIONE, " _
                & " TIPO_PAG_RUOLO.ID, " _
                & " '' AS ELIMINA, " _
                & " DECODE((SELECT COUNT(*) FROM SISCOM_MI.INCASSI_RUOLI WHERE ID_TIPO_PAG=SISCOM_MI.TIPO_PAG_RUOLO.ID),0,1,0) AS FL_ELIMINABILE " _
                & " FROM SISCOM_MI.TIPO_PAG_RUOLO order by descrizione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGridTipoPagamenti.DataSource = dt
                DataGridTipoPagamenti.DataBind()
                controllaEliminazione()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore nel caricamento delle tipologie di pagamento!');", True)
        End Try
    End Sub
    Private Sub controllaEliminazione()
        For Each item As DataGridItem In DataGridTipoPagamenti.Items
            'If item.Cells(2).Text = "1" Then
            item.Cells(2).Text = "<table border=""0""><tr><td><img src=""../../NuoveImm/matita.png"" style=""cursor: pointer"" onclick=""javascript:modifica(" & item.Cells(3).Text & ");void(0);"" alt=""elimina"" width=""16"" height=""16"" />" _
            & "</td><td><img src=""../../NuoveImm/Elimina.png"" style=""cursor: pointer"" onclick=""javascript:elimina(" & item.Cells(3).Text & ");void(0);"" alt=""elimina"" /></td></tr></table>"
            'End If
        Next
    End Sub
    Protected Sub ButtonElimina_Click(sender As Object, e As System.EventArgs) Handles ButtonElimina.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TIPO_PAG_RUOLO WHERE ID=" & HiddenFieldId.Value
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            caricaTipologie()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'eliminazione della tipologia di pagamento!');", True)
        End Try
    End Sub

    Protected Sub ImageButtonAggiungiSpesa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAggiungiSpesa.Click
        Response.Redirect("NuovaTipPagamentoRuolo.aspx")
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub

End Class
