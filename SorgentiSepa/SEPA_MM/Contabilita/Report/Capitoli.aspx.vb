
Partial Class Contabilita_Report_Capitoli
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaCapitoli()
        End If
    End Sub
    Private Sub caricaCapitoli()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT T_VOCI_BOLLETTA_CAP.*,'' as elimina FROM SISCOM_MI.T_VOCI_BOLLETTA_CAP ORDER BY NLSSORT(descrizione,'NLS_SORT=BINARY')"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGridCapitoli.DataSource = dt
                DataGridCapitoli.DataBind()
                controllaEliminazione()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore nel caricamento dei capitoli!');", True)
        End Try
    End Sub
    Private Sub controllaEliminazione()
        For Each item As DataGridItem In DataGridCapitoli.Items
            item.Cells(1).Text = "<table border=""0""><tr><td><img src=""../../NuoveImm/matita.png"" style=""cursor: pointer"" onclick=""javascript:modifica(" & item.Cells(2).Text & ");void(0);"" alt=""elimina"" width=""16"" height=""16"" />" _
                & "</td><td><img src=""../../NuoveImm/Elimina.png"" style=""cursor: pointer"" onclick=""javascript:elimina(" & item.Cells(2).Text & ");void(0);"" alt=""elimina"" /></td></tr></table>"
        Next
    End Sub
    Protected Sub ButtonElimina_Click(sender As Object, e As System.EventArgs) Handles ButtonElimina.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.t_voci_bolletta_cap WHERE ID=" & HiddenFieldId.Value
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            caricaCapitoli()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'eliminazione del capitolo!');", True)
        End Try
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub

    Protected Sub ImageButtonAggiungiCapitolo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAggiungiCapitolo.Click
        Response.Redirect("NuovoCapitolo.aspx")
    End Sub
End Class