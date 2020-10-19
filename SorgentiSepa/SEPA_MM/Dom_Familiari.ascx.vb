
Partial Class Dom_Familiari
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Public Sub DisattivaTutto()
        cmbF1.Enabled = False
        cmbF2.Enabled = False
        cmbF3.Enabled = False
        cmbF4.Enabled = False
        cmbF5.Enabled = False
        cmbF6.Enabled = False
        cmbF7.Enabled = False
    End Sub
End Class
