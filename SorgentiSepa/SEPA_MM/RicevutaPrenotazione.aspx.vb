
Partial Class RicevutaPrenotazione
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Label1.Text = Request.QueryString("V1")
            Label2.Text = Request.QueryString("V2")
            Label3.Text = Request.QueryString("V3")
            Label4.Text = Request.QueryString("V4")
            lblOperatore.Text = Session.Item("OPERATORE")
            lblData.Text = Format(Now, "dd/MM/yyyy")
        End If
    End Sub
End Class
