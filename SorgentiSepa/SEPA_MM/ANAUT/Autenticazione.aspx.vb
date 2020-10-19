
Partial Class ANAUT_Autenticazione
    Inherits PageSetIdMode

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnAccedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAccedi.Click
        If Request.QueryString("OP") = "2" Then
            If txtpw.Text = "creaxml" Then
                Response.Write("<script>document.location.href=""CreaXML.aspx""</script>")
            Else
                Label1.Visible = True
                Label1.Text = "Password Errata!"
            End If
        Else
            If txtpw.Text = "inviaxml" Then
                Response.Write("<script>document.location.href=""InviaXML.aspx""</script>")
            Else
                Label1.Visible = True
                Label1.Text = "Password Errata!"
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Request.QueryString("OP") = "2" Then
            Label2.Text = "Download XML"
        Else
            Label2.Text = "Invia XML"

        End If

    End Sub
End Class
