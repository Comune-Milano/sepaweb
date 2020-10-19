
Partial Class ANAUT_RicercaIncompleti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "SELECT * FROM utenza_bandi where id>1 and stato=1 ORDER BY id desc", "DESCRIZIONE", "ID")
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "select * from UTENZA_SPORTELLI where ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE ID_BANDO=" & cmbBando.SelectedItem.Value & ") ORDER BY DESCRIZIONE asc", "DESCRIZIONE", "ID")
                'cmbFiliale.Items.Add(New ListItem("TUTTI", "TUTTI"))

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End Try
        End If

        txtStipulaDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        txtStipulaAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        ADal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        AAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        If cmbFiliale.SelectedItem.Value <> "-1" Then
            Response.Write("<script>location.replace('RisultatoIncompleti.aspx?BA=" & cmbBando.SelectedItem.Value & "&NSP=" & par.Cripta(cmbFiliale.SelectedItem.Text) & "&FI=" & cmbFiliale.SelectedItem.Value & "&SDAL=" & par.AggiustaData(txtStipulaDal.Text) & "&SAL=" & par.AggiustaData(txtStipulaAl.Text) & "&ADAL=" & par.AggiustaData(ADal.Text) & "&AAL=" & par.AggiustaData(AAl.Text) & "&ES=" & CheckBox1.Checked & "&GD=" & CheckBox2.Checked & "&DV=" & CheckBox3.Checked & "');</script>")
        Else
            Response.Write("<script>alert('Scelta non valida!');</script>")
        End If


    End Sub

    Protected Sub cmbBando_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBando.SelectedIndexChanged

    End Sub
End Class
