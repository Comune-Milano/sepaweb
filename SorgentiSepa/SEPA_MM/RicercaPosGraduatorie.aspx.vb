
Partial Class RicercaPosGraduatorie
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")



                par.RiempiDList(Me, par.OracleConn, "cmbGraduatoria", "SELECT * FROM bandi where id in (select distinct id_bando from bandi_graduatoria_storico)  ORDER BY id desc", "DESCRIZIONE", "ID")
                'Dim lsiFrutto As New ListItem("TUTTE", "-1")
                'cmbGraduatoria.Items.Add(lsiFrutto)
                'cmbGraduatoria.ClearSelection()
                'cmbGraduatoria.Items.FindByText("TUTTE").Selected = True

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim iStato As Integer

        iStato = cmbGraduatoria.SelectedItem.Value

        Response.Write("<script>location.replace('RisultatoPosGraduatorie.aspx?SP=" & cmbSpeciale.SelectedItem.Value & "&CG=" & par.VaroleDaPassare(txtCognome.Text.ToUpper) & "&NM=" & par.VaroleDaPassare(txtNome.Text.ToUpper) & "&CF=" & par.VaroleDaPassare(txtCF.Text.ToUpper) & "&PG=" & par.VaroleDaPassare(txtPG.Text.ToUpper) & "&ST=" & iStato & "');</script>")
    End Sub
End Class
