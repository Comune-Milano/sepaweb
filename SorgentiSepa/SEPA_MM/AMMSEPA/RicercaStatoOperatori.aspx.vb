
Partial Class AMMSEPA_RicercaStatoOperatori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sua As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbEnte", "SELECT * FROM CAF_WEB ORDER BY COD_CAF ASC", "COD_CAF", "ID")
                par.RiempiDListConVuoto(Me, par.OracleConn, "cmbFiliale", "SELECT id,nome FROM siscom_mi.tab_filiali order by nome", "NOME", "ID")
                If sua = 0 Then
                    cmbfiliale.Enabled = False
                End If
            Else
                If cmbEnte.SelectedValue = 2 Then
                    cmbfiliale.Enabled = True
                Else
                    cmbfiliale.Enabled = False
                    cmbfiliale.SelectedValue = -1
                End If
            End If
            If Session.Item("ID_CAF") = 2 And Session.Item("RESPONSABILE") = 1 Then
                cmbEnte.SelectedValue = 2
                cmbEnte.Enabled = False
                cmbfiliale.Enabled = True
                sua = 1
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Write("<script>location.replace('RisultatoStatoOperatori.aspx?ENTE=" & cmbEnte.SelectedValue & "&FILIALE=" & cmbfiliale.SelectedValue & "');</script>")
    End Sub
End Class
