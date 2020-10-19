
Partial Class VSA_Indici
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            L1.Text = Request.QueryString("V10")
            L2.Text = Request.QueryString("V7")
            L3.Text = Request.QueryString("V9")
            L4.Text = Request.QueryString("V8")
            LBLPSE.Text = Request.QueryString("V12")
            LBLVSE.Text = Request.QueryString("V13")
            Label13.Text = "Pg " & Request.QueryString("PG")
        End If
    End Sub
End Class
