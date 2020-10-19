
Partial Class CAMBI_indici
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
            L5.Text = Request.QueryString("V4")
            L6.Text = Request.QueryString("V5")
            L7.Text = Request.QueryString("V6")
            L8.Text = Request.QueryString("V1")
            L9.Text = Request.QueryString("V2")
            L10.Text = Request.QueryString("V3")
            LBLPSE.Text = Request.QueryString("V12")
            LBLVSE.Text = Request.QueryString("V13")
            Label10.Text = Request.QueryString("V11")
            Label13.Text = "Pg " & Request.QueryString("PG")
        End If
    End Sub
End Class
