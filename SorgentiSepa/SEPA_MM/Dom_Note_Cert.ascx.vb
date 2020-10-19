
Partial Class Dom_Note_Cert
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Public Sub DisattivaTutto()
        txtNote.Enabled = False

        cmbAnagrafica.Enabled = False
        cmbDichiara.Enabled = False

        cmbLocazione.Enabled = False

        cmbNucleo.Enabled = False

        cmbSottoscrittore.Enabled = False
        ChNonIdoneo.Enabled = False
        ChCartacea.Enabled = False


    End Sub
End Class
