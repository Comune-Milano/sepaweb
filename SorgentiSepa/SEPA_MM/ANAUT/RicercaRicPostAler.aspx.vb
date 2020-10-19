
Partial Class ANAUT_RicercaRicPostAler
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi where id>1 ORDER BY id desc", "DESCRIZIONE", "ID")
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from UTENZA_SPORTELLI where ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & cmbBando.SelectedItem.Value & ") ORDER BY DESCRIZIONE asc", "DESCRIZIONE", "ID")
                'cmbFiliale.Items.Add(New ListItem("TUTTE", "-1"))
                'cmbFiliale.Items.FindByText("TUTTE").Selected = True

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Write("<script>location.replace('RisultatoRicPostAler.aspx?BA=" & cmbBando.SelectedItem.Value & "&FI=" & cmbFiliale.SelectedItem.Value & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "');</script>")
    End Sub

    Protected Sub cmbBando_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from UTENZA_SPORTELLI where ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & cmbBando.SelectedItem.Value & ") ORDER BY DESCRIZIONE asc", "DESCRIZIONE", "ID")
    End Sub
End Class
