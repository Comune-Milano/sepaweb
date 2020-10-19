Imports System.Web
Imports System.Web.SessionState
Imports System.IO
Imports System.Diagnostics

Partial Class PaginaErrore
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Label1.Text = Session.Item("ErroreGenerico")
    End Sub
End Class
