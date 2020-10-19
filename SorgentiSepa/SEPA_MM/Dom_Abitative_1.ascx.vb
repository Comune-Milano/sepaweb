
Partial Class Dom_Abitative_1
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Public Sub DisattivaTutto()
        cmbA1.Enabled = False
        cmbA2.Enabled = False
        cmbA3.Enabled = False
        cmbA4.Enabled = False
        cmbA5.Enabled = False
        ChMorosita.Enabled = False
        chMG.Enabled = False
    End Sub

End Class
