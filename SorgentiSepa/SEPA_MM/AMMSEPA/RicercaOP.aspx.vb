
Partial Class AMMSEPA_RicercaOP
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim iEnte As Integer


        If cmbEnte.Items.FindByText("TUTTI").Selected = True Then
            iEnte = -1
        Else
            iEnte = cmbEnte.SelectedItem.Value
        End If


        Response.Write("<script>location.replace('RisultatoRicercaOp.aspx?RG=" & DropDownList1.SelectedItem.Value & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&CN=" & par.VaroleDaPassare(txtCognome.Text) & "&EN=" & iEnte & "&OP=" & par.VaroleDaPassare(txtOperatore.Text) & "');</script>")

    End Sub

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



            par.RiempiDList(Me, par.OracleConn, "cmbEnte", "SELECT * FROM CAF_WEB ORDER BY COD_CAF ASC", "COD_CAF", "ID")
            cmbEnte.Items.Add("TUTTI")
            cmbEnte.Items.FindByText("TUTTI").Selected = True

            'If cmbBando.Items.Count = 0 Then
            '    Dim lsiFrutto As New ListItem("TUTTI", "-2")
            '    cmbBando.Items.Add(lsiFrutto)

            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            '    par.cmd.CommandText = "SELECT * FROM BANDI_FSA WHERE ID<>-1 ORDER BY ID ASC"
            '    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    While myReader.Read
            '        lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
            '        cmbBando.Items.Add(lsiFrutto)
            '    End While
            '    par.OracleConn.Close()
            'End If
        End If
    End Sub
End Class
