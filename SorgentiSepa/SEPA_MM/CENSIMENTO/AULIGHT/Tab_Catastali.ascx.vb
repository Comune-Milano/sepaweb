
Partial Class CENSIMENTO_Tab_Catastali
    Inherits UserControlSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        FrmSolaLettura()
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
