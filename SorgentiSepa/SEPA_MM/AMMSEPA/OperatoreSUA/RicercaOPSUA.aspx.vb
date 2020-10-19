
Partial Class AMMSEPA_OperatoreSUA_RicercaOPSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtOperatore.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            'par.RiempiDList(Me, par.OracleConn, "cmbEnte", "SELECT * FROM CAF_WEB ORDER BY COD_CAF ASC", "COD_CAF", "ID")
            par.RiempiDList(Me, par.OracleConn, "cmbEnte", "SELECT * FROM CAF_WEB WHERE ID IN (2,63) ORDER BY ID ASC", "COD_CAF", "ID")

            cmbEnte.Items.FindByValue("2").Selected = True
            'cmbEnte.Enabled = False
        End If
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim iEnte As Integer
        iEnte = cmbEnte.SelectedItem.Value
        Response.Write("<script>location.replace('RisultatoRicercaOpSUA.aspx?FE=" & cmbFEsterno.SelectedItem.Value & "&RG=" & DropDownList1.SelectedItem.Value & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&CN=" & par.VaroleDaPassare(txtCognome.Text) & "&EN=" & iEnte & "&OP=" & par.VaroleDaPassare(txtOperatore.Text) & "');</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub
End Class
