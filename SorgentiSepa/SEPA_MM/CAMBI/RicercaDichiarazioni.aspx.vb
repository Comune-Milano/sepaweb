
Partial Class CAMBI_RicercaDichiarazioni
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
            txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")




            If cmbBando.Items.Count = 0 Then
                Dim lsiFrutto As New ListItem("TUTTI", "-2")
                cmbBando.Items.Add(lsiFrutto)

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_STATI_DICHIARAZIONE ORDER BY COD ASC", "DESCRIZIONE", "COD")
                cmbStato.Items.Add("TUTTI")
                cmbStato.Items.FindByText("TUTTI").Selected = True

                par.cmd.CommandText = "SELECT * FROM BANDI_cambio WHERE ID<>-1 ORDER BY ID ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                    cmbBando.Items.Add(lsiFrutto)
                End While
                myReader.Close()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim iStato As Integer

        If cmbStato.Items.FindByText("TUTTI").Selected = True Then
            iStato = -1
        Else
            iStato = cmbStato.SelectedItem.Value
        End If
        Response.Write("<script>location.replace('RisultatoRicercaD.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text.ToUpper) & "&NM=" & par.VaroleDaPassare(txtNome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(txtCF.Text.ToUpper) & "&PG=" & par.VaroleDaPassare(txtPG.Text.ToUpper) & "&ST=" & iStato & "&BA=" & cmbBando.SelectedItem.Value & "');</script>")
    End Sub
End Class
