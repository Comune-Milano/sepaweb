
Partial Class Dom_Dichiara_Cambi
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        txtCIData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtCSData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtPSData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtPSScade.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtPSRinnovo.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
    End Sub

    Public Sub DisattivaTutto()
        cmbPresentaD.Enabled = False

        cmbFattaAU.Enabled = False
        'cmbFattaERP.Enabled = False
        'cmbFaiERP.Enabled = False
        'txtPgERP.Enabled = False

        txtCIData.Enabled = False
        txtCINum.Enabled = False
        txtCIRilascio.Enabled = False
        txtCSData.Enabled = False
        txtCSNum.Enabled = False
        txtPSData.Enabled = False
        txtPSNum.Enabled = False
        txtPSRinnovo.Enabled = False
        txtPSScade.Enabled = False


    End Sub

    Protected Sub txtCIData_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCIData.TextChanged

    End Sub
End Class
