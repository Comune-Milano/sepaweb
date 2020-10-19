
Partial Class PED_VisFoto
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Request.QueryString("T") = "1" Then
            Image1.ImageUrl = "..\FOTO_PED\" & Request.QueryString("FILE")
        Else
            Image1.ImageUrl = "..\PLANIMETRIE\" & Request.QueryString("FILE")
        End If


    End Sub
End Class
