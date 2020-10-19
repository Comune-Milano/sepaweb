
Partial Class Dom_Contratto_FSA
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        txtDataDecorrenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataDecorrenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataScadenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataStipula.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataReg.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
    End Sub

    Public Sub DisattivaTutto()
        txtDataDecorrenza.Enabled = False
        txtDataDecorrenza.Enabled = False
        txtDataScadenza.Enabled = False
        txtDataStipula.Enabled = False
        txtDataReg.Enabled = False
        txtEstremi.Enabled = False
        txtAffitto.Enabled = False
        txtRiscaldamento.Enabled = False
        txtSpese.Enabled = False
        cmbStatoC.Enabled = False
        cmbTipoContratto.Enabled = False
        cmbTipoFigura.Enabled = False
        txtIdonei.Enabled = False
        TxtMesi.Enabled = False
        txtDataDisdetta.Enabled = False
    End Sub
End Class
