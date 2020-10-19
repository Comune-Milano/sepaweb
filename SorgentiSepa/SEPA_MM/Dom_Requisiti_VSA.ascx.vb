
Partial Class Dom_Requisiti_VSA
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        If Not IsPostBack Then
            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                    'ElseIf TypeOf CTRL Is DropDownList Then
                    '    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript//:document.getElementById('txtModificato').value='1';")
                End If
            Next
        End If
    End Sub

    Public Sub DisattivaTutto()
        chR1.Enabled = False
        chR2.Enabled = False
        chR3.Enabled = False
        chR4.Enabled = False
        chR5.Enabled = False
        chR6.Enabled = False
        chR7.Enabled = False
        chR8.Enabled = False

        chR9.Enabled = False
        chR10.Enabled = False
        chR11.Enabled = False
        chR12.Enabled = False
        chR13.Enabled = False
        chR14.Enabled = False
        chR15.Enabled = False
        chR16.Enabled = False
        chR17.Enabled = False
        chR18.Enabled = False
    End Sub
End Class
