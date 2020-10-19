
Partial Class Dom_Dichiara_FSA
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
	        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        'txtOccupanti.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtOccupanti.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtDataScadenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtDataStipula.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        'txtDataReg.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

    End Sub
    Public Sub DisattivaTutto()
        txtOccupanti.Enabled = False
        txtCoabitanti.Enabled = False
        txtAutonomo.Enabled = False
        txtDipendenti.Enabled = False
        txtPensione.Enabled = False
        txtSubordinato.Enabled = False
        ChDifficolta.Enabled = False
        txtNoteIndigente.Enabled = False

        cmbContributo.Enabled = False
        txtIban.Enabled = False
        txtBanca.Enabled = False
        txtUbicazione.Enabled = False
        txtIntestato.Enabled = False
    End Sub


End Class
