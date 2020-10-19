
Partial Class VSA_RicercaDomande
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
            Label3.Text = "La ricerca sarà effettuata solo sulle domande acquisite dall'ente " & Session.Item("CAAF")
            If cmbStato.Items.Count = 0 Then
                cmbStato.Items.Add("TUTTI")
                cmbStato.Items.Add("FORMALIZZAZIONE")
                cmbStato.Items.Add("ISTRUTTORIA")
                cmbStato.Items.Add("IDONEE")
                'cmbStato.Items.Add("GRADUATORIA")
                'cmbStato.Items.Add("ASSEGNAZIONE")
                'cmbStato.Items.Add("CONTRATTO")
                cmbStato.Items.Add("RESPINTE")
                cmbStato.Items.FindByText("TUTTI").Selected = True
            End If
            If cmbBando.Items.Count = 0 Then

                Dim lsiFrutto As New ListItem("TUTTI", "-2")
                cmbBando.Items.Add(lsiFrutto)
                Try


                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    par.cmd.CommandText = "SELECT * FROM T_MOTIVO_DOMANDA_VSA ORDER BY DESCRIZIONE ASC"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader.Read
                        lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                        cmbBando.Items.Add(lsiFrutto)
                    End While
                    myReader.Close()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    Response.Write(ex.Message)
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If

        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("RisultatoRicercaDom.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&PG=" & par.VaroleDaPassare(txtPG.Text) & "&ST=" & cmbStato.SelectedItem.Text & "&BA=" & cmbBando.SelectedItem.Value)

    End Sub
End Class
