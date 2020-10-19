
Partial Class AutoCompilazione_privacy
    Inherits PageSetIdMode

    Protected Sub imgProsegui_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgProsegui.Click
        If RadioButton1.Checked = True Then
            Response.Redirect("start.aspx")
        Else
            Response.Redirect("..\Portale.aspx")
        End If
    End Sub
End Class
