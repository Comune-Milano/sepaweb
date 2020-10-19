
Partial Class Contratti_Pagamenti_TipologiaAnno
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaAnni()
        End If
    End Sub
    Private Sub caricaAnni()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = " SELECT id,to_char(to_date(validita_da,'yyyymmdd'),'dd/mm/yyyy') as validita_da," _
                & " to_char(to_date(validita_a,'yyyymmdd'),'dd/mm/yyyy') as validita_a,anno,'' as elimina from siscom_mi.bol_bollette_es_contabile "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGridAnno.DataSource = dt
                DataGridAnno.DataBind()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            controllaEliminazione()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore nel caricamento degli anni!');", True)
        End Try
    End Sub
    Private Sub controllaEliminazione()
        For Each item As DataGridItem In DataGridAnno.Items
            item.Cells(3).Text = "<table border=""0""><tr><td><img src=""../../NuoveImm/matita.png"" style=""cursor: pointer"" onclick=""javascript:modifica(" & item.Cells(4).Text & ");void(0);"" alt=""elimina"" width=""16"" height=""16"" />" _
                & "</td><td><img src=""../../NuoveImm/Elimina.png"" style=""cursor: pointer"" onclick=""javascript:elimina(" & item.Cells(4).Text & ");void(0);"" alt=""elimina"" /></td></tr></table>"
        Next
    End Sub
    Protected Sub ButtonElimina_Click(sender As Object, e As System.EventArgs) Handles ButtonElimina.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.bol_bollette_es_contabile WHERE ID=" & HiddenFieldId.Value
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            caricaAnni()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'eliminazione dell\'anno!');", True)
        End Try
    End Sub
    Protected Sub ImageButtonAggiungiSpesa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAggiungiSpesa.Click
        Response.Redirect("NuovaTipologiaAnno.aspx")
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub
End Class

