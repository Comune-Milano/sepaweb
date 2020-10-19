
Partial Class Contratti_Pagamenti_TipologiaPagamenti
    Inherits PageSetIdMode
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
                & " TIPO_PAG_PARZ.DESCRIZIONE, " _
                & " TIPO_PAG_PARZ.ID, " _
                & " '' AS ELIMINA,UTILIZZABILE, " _
                & " DECODE((SELECT COUNT(*) FROM SISCOM_MI.INCASSI_EXTRAMAV WHERE ID_TIPO_PAG=SISCOM_MI.TIPO_PAG_PARZ.ID),0,1,0) AS FL_ELIMINABILE " _
                & " FROM SISCOM_MI.TIPO_PAG_PARZ "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count > 0 Then
                DataGridTipoPagamenti.DataSource = dt
                DataGridTipoPagamenti.DataBind()
                controllaEliminazione()
                controllaUtilizzabilita()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore nel caricamento delle tipologie di pagamento!')</script>")
        End Try
    End Sub
    Private Sub controllaEliminazione()
        For Each item As DataGridItem In DataGridTipoPagamenti.Items
            If item.Cells(2).Text = "1" Then
                item.Cells(3).Text = "<table border=""0""><tr><td><img src=""../Immagini/matita.png"" style=""cursor: pointer"" onclick=""javascript:modifica(" & item.Cells(4).Text & ");void(0);"" alt=""elimina"" width=""16"" height=""16"" />" _
                & "</td><td><img src=""../Immagini/Elimina.png"" style=""cursor: pointer"" onclick=""javascript:elimina(" & item.Cells(4).Text & ");void(0);"" alt=""elimina"" /></td></tr></table>"
            End If
        Next
    End Sub
    Protected Sub ButtonElimina_Click(sender As Object, e As System.EventArgs) Handles ButtonElimina.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.TIPO_PAG_PARZ WHERE ID=" & HiddenFieldId.Value
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            caricaTipologie()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante l\'eliminazione della tipologia di pagamento!')</script>")
        End Try
    End Sub

    Protected Sub ImageButtonAggiungiSpesa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAggiungiSpesa.Click
        Response.Redirect("NuovaTipologiaPagamento.aspx")
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            For Each riga As DataGridItem In DataGridTipoPagamenti.Items
                If CType(riga.FindControl("CheckBox1"), CheckBox).Checked = True Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.TIPO_PAG_PARZ SET UTILIZZABILE=1 WHERE ID=" & riga.Cells(4).Text
                    par.cmd.ExecuteReader()
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.TIPO_PAG_PARZ SET UTILIZZABILE=0 WHERE ID=" & riga.Cells(4).Text
                    par.cmd.ExecuteReader()
                End If
            Next
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Utilizzabilità impostata correttamente!');</script>")
            caricaTipologie()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Errore durante la modifica delle tipologie!')</script>")
        End Try
    End Sub
    Private Sub controllaUtilizzabilita()
        For Each riga As DataGridItem In DataGridTipoPagamenti.Items
            If riga.Cells(5).Text = "1" Then
                CType(riga.FindControl("CheckBox1"), CheckBox).Checked = True
            Else
                CType(riga.FindControl("CheckBox1"), CheckBox).Checked = False
            End If
        Next
    End Sub
End Class
