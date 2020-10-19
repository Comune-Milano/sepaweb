
Partial Class cf_CodFiscale
    Inherits PageSetIdMode


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Session.Remove("CodF")
            Session.Add("CodF", "")
            Cognome.Value = Request.QueryString("Cog")
            Nome.Value = Request.QueryString("Nom")
        End If
       

    End Sub



    
    Protected Sub Calcola_Click(sender As Object, e As System.EventArgs) Handles Calcola.Click
        Session.Item("CodF") = CodiceFiscale.Value
        If CodiceFiscale.Value <> "" Then
            Response.Write("<script> self.close();</script>")
        End If

    End Sub
End Class
