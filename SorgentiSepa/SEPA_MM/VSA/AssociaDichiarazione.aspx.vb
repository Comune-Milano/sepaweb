
Partial Class VSA_AssociaDichiarazione
    Inherits PageSetIdMode
    Dim clMioPar As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPG.Text = Session.Item("ID_NUOVA_DIC")
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button1.Click
        Response.Redirect("RisultatoDichiarazioni.aspx?PG=" & clMioPar.VaroleDaPassare(txtPG.Text) & "&CF=" & clMioPar.VaroleDaPassare(txtCF.Text))

    End Sub
End Class
