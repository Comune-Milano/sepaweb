
Partial Class Dom_Alloggio_FSA
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

    End Sub

    Public Sub DisattivaTutto()
        txtAnno.Enabled = False
        txtFoglio.Enabled = False
        txtNumLocali.Enabled = False
        txtParticella.Enabled = False
        txtSub.Enabled = False
        txtSuperficie.Enabled = False
        ChAuto.Enabled = False
        ChBox.Enabled = False
        ChCucina.Enabled = False
        ChDegrado.Enabled = False
        ChPotabile.Enabled = False
        ChImproprio.Enabled = False
        ChIndivisa.Enabled = False
        ChReq.Enabled = False
        ChRiscaldamento.Enabled = False
        ChServizi.Enabled = False
        cmbCat.Enabled = False

    End Sub
End Class
