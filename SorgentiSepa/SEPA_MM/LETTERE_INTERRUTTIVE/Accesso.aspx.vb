
Partial Class LETTERE_INTERRUTTIVE_Accesso
    Inherits System.Web.UI.Page

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Try
            If UCase(txtUtente.Text) = "LETTERE" And txtUtente0.Text = "14072014" Then
                Session.Add("OPERATORE_LETTERE", "1")
                Response.Redirect("Lettere.aspx", True)
            Else
                lblErrore.Visible = True
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class
