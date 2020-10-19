
Partial Class ANAUT_menu_funzioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Inserire qui il codice utente necessario per inizializzare la pagina
        If Session.Item("OPERATORE") <> "" Then

            'LinkButton1.Attributes.Add("OnClick", "javascript:parent.main.Uscita=1;")
        Else
            'Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
    End Sub



    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Session.Add("IDANA", "")
        If Session.Item("CONTRATTOAPERTO") = "1" Then
            Response.Write("<script>window.close();</script>")
        Else
            Response.Write("<script>top.location.href=""../../Chiusura.htm""</script>")
        End If

    End Sub
End Class
