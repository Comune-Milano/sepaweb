
Partial Class Dic_Note_Cert
    Inherits UserControlSetIdMode


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Public Sub DisattivaTutto()
        txtNote.Enabled = False
        cmbAltroReddito.Enabled = False
        cmbAnagrafica.Enabled = False
        cmbComponenti.Enabled = False
        cmbDetrazioni.Enabled = False
        cmbFamiglia.Enabled = False
        cmbImmobiliare.Enabled = False
        cmbMobiliare.Enabled = False
        cmbReddito.Enabled = False
        cmbSottoscrittore.Enabled = False
    End Sub
End Class
