
Partial Class CONS_StatPrenotazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtDal.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtAl.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'btnCerca.Attributes.Add("OnClick", "javascript:Attendi();")
        End If
    End Sub



    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If Len(txtDal.Text) <> 10 Or Len(txtAl.Text) <> 10 Then
            Response.Write("<script>alert('Valorizzare con date valide!');</script>")
            Exit Sub
        Else
            Response.Write("<script>window.open('RisultatoStatPrenotazioni.aspx?DAL=" & par.VaroleDaPassare(txtDal.Text) & "&AL=" & par.VaroleDaPassare(txtAl.Text) & "','Consultazioni','');</script>")
        End If
    End Sub
End Class
