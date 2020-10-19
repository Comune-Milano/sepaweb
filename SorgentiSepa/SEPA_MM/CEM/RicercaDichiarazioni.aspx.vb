
Partial Class VSA_RicercaDichiarazioni
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


            par.RiempiDList(Me, par.OracleConn, "cmbStato", "SELECT * FROM T_STATI_DICHIARAZIONE ORDER BY COD ASC", "DESCRIZIONE", "COD")
            cmbStato.Items.Add("TUTTI")
            cmbStato.Items.FindByText("TUTTI").Selected = True

            'If cmbBando.Items.Count = 0 Then
            'Dim lsiFrutto As New ListItem("TUTTI", "-2")
            'cmbBando.Items.Add(lsiFrutto)

            Try
                'par.OracleConn.Open()
                'par.SettaCommand(par)
                'par.cmd.CommandText = "SELECT * FROM BANDI_VSA WHERE ID<>-1 ORDER BY ID ASC"
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader.Read
                '    lsiFrutto = New ListItem(myReader("DESCRIZIONE"), myReader("ID"))
                '    cmbBando.Items.Add(lsiFrutto)
                'End While
                'myReader.Close()
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                'par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try
        End If
        'End If
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
        Response.Write("<script>location.replace('RisultatoRicercaD.aspx?CG=" & par.VaroleDaPassare(txtCognome.Text) & "&NM=" & par.VaroleDaPassare(txtNome.Text) & "&CF=" & par.VaroleDaPassare(txtCF.Text) & "&PG=" & par.VaroleDaPassare(txtPG.Text) & "&ST=" & iStato & "');</script>")
    End Sub
End Class
