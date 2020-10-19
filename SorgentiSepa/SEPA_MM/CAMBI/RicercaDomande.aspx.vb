
Partial Class CAMBI_RicercaDomande
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
            Label3.Text = "La ricerca sarà effettuata solo sulle domande di bando cambi acquisite dall'ente " & Session.Item("CAAF")
            If cmbStato.Items.Count = 0 Then
                cmbStato.Items.Add("TUTTI")
                cmbStato.Items.Add("FORMALIZZAZIONE")
                cmbStato.Items.Add("ISTRUTTORIA")
                cmbStato.Items.Add("IDONEE")
                cmbStato.Items.Add("GRADUATORIA")
                cmbStato.Items.Add("ASSEGNAZIONE")
                cmbStato.Items.Add("CONTRATTO")
                cmbStato.Items.Add("RESPINTE")
                cmbStato.Items.FindByText("TUTTI").Selected = True
            End If
            If cmbBando.Items.Count = 0 Then

                Dim lsiFrutto As New ListItem("TUTTI", "-2")
                cmbBando.Items.Add(lsiFrutto)

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT * FROM BANDI_cambio WHERE ID<>-1 ORDER BY ID ASC"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                    cmbBando.Items.Add(lsiFrutto)
                End While
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("RisultatoRicDom.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text.ToUpper) & "&NM=" & par.VaroleDaPassare(txtNome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(txtCF.Text.ToUpper) & "&PG=" & par.VaroleDaPassare(txtPG.Text.ToUpper) & "&ST=" & cmbStato.SelectedItem.Text & "&BA=" & cmbBando.SelectedItem.Value)
    End Sub
End Class
