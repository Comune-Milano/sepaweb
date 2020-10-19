
Partial Class ANAUT_Tab_InfoContratto
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If CType(Me.Page.FindControl("inUscita"), HiddenField).Value = "1" Then
            Exit Sub
        End If
        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            End If
        Next
        txtDataCessazione.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtDataDec.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub
End Class
