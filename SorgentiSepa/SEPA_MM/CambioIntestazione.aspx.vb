
Partial Class CambioIntestazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM domande_bando WHERE id_stato<>'10' and pg='" & par.PulisciStrSql(txtPG.Text) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Response.Write("<script>document.location.href=""CambioIntestazione1.aspx?ID=" & myReader("id") & "&PG=" & txtPG.Text & """</script>")
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write("<br>")
            Response.Write("<br>")
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub
End Class
