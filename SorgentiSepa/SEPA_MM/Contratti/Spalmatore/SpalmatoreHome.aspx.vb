Imports Telerik.Web.UI

Partial Class Spalmatore_SpalmatoreHome
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Session.Item("MLoading") = ""
    End Sub

End Class
