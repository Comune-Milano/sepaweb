
Partial Class CambioIntestazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.cmd.CommandText = "SELECT * FROM domande_bando_vsa WHERE FL_AUTORIZZAZIONE = 0 and (id_motivo_domanda = 3 or id_motivo_domanda = 2) and PG='" & par.PulisciStrSql(txtPG.Text) & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Response.Write("<script>document.location.href=""CambioIntestazione1.aspx?ID=" & myReader("id") & "&PG=" & txtPG.Text & """</script>")
            Else
                lblErr.Visible = True
            End If
            myReader.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            If Not IsPostBack Then
                IDmotivo.Value = Request.QueryString("MOT")

                txtPG.Text = Request.QueryString("PG")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

    End Sub
End Class
