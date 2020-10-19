
Partial Class ANAUT_menu_funzioni
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub avviso_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles avviso.Click
        Response.Write("<script>window.open('../avviso.aspx','Avviso','top=0,left=0,width=400,height=150');</script>")
    End Sub

 

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("OPERATORE") <> "" Then
            lblOperatore.Text = Session.Item("OPERATORE")
            If Session.Item("PW") = "SEPA" Then
                avviso.Visible = True
            Else
                avviso.Visible = False
            End If
            LinkButton1.Attributes.Add("OnClick", "javascript:parent.main.Uscita=1;")
        Else
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
    End Sub

    Public Function Spegni()
        avviso.Visible = False
    End Function

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        If Session.Item("LAVORAZIONE") = "1" Then
            Response.Write("<script>alert('Prima di procedere chiudere la maschera utilizzando la funzione USCITA')</script>")
            Exit Sub
        End If
        Response.Write("<script>top.location.href=""../Chiusura.htm""</script>")

    End Sub
End Class
