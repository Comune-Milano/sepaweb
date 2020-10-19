
Partial Class Dom_Requisiti
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Public Sub DisattivaTutto()
        chR1.Enabled = False
        chR2.Enabled = False
        chR3.Enabled = False
        chR4.Enabled = False
        chR5.Enabled = False
        chR6.Enabled = False
        chR7.Enabled = False
        chR8.Enabled = False
    End Sub
End Class
