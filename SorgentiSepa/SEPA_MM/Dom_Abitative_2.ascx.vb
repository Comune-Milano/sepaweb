
Partial Class Dom_Abitative_2
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        cmbA9.Enabled = False
    End Sub

    Public Sub DisattivaTutto()
        cmbA6.Enabled = False
        cmbA7.Enabled = False
        cmbA8.Enabled = False
        cmbA9.Enabled = False
        chAO.Enabled = False
        txtAnnoCanone.Enabled = False
        txtSpese.Enabled = False
        txtAnnoAcc.Enabled = False
        txtSpeseAcc.Enabled = False
    End Sub
End Class
