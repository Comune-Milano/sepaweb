
Partial Class CENSIMENTO_Tab_Catastali
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        TxtNote.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        TxtDitta.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

        TxtDataAcquisiz.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        TxtDataFineVal.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        '*********************************************************************************************
        TxtDataAcquisiz.Attributes.Add("onfocus", "javascript:selectText(this);")
        TxtDataFineVal.Attributes.Add("onfocus", "javascript:selectText(this);")
        '*********************************************************************************************
        TxtDataAcquisiz.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        TxtDataFineVal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        If Session("PED2_SOLOLETTURA") = "1" Then
            FrmSolaLettura()
        End If



        If Session("SLE") = "1" Then
            FrmSolaLettura()
        End If

    End Sub
    Private Sub FrmSolaLettura()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
    End Sub

End Class
