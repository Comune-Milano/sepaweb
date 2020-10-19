
Partial Class ASS_IntroProcessoDec
    Inherits PageSetIdMode
    Dim par As New CM.Global()

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If

    End Sub
    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script>document.location.href=""RisultatoRicOfferta.aspx?PR=1&T=" & cmbTipo.SelectedValue & "&CG=" & par.PulisciStrSql(txtCognome.Text.ToUpper) & "&NM=" & par.PulisciStrSql(txtNome.Text.ToUpper) & "&PG=" & par.PulisciStrSql(txtPG.Text) & "&OF=" & par.PulisciStrSql(txtNumOff.Text) & "&CONTR=" & cmbContratto.SelectedValue & "&REV=" & Valore01(cmbRevoca.Checked) & """</script>")
    End Sub
End Class
