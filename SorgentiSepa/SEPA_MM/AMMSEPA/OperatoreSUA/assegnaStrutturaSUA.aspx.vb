
Partial Class AMMSEPA_OperatoreSUA_assegnaStrutturaSUA
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then

                Dim ds As New Data.DataSet()
                cmbfiliale.Items.Clear()
                cmbfiliale.Items.Add(New ListItem("Tutte"))
                cmbfiliale.Items.Add(New ListItem("Nessuna"))
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT id,nome FROM siscom_mi.tab_filiali order by nome"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While myReader.Read
                    cmbfiliale.Items.Add(New ListItem(par.IfNull(myReader(1), ""), par.IfNull(myReader(0), 0)))
                End While
                myReader.Close()
                par.OracleConn.Close()
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim scelta As String
        scelta = cmbfiliale.SelectedValue
        Response.Write("<script>location.replace('RisultatoassegnaStrutturaSUA.aspx?scelta=" & scelta & "');</script>")
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../pagina_home.aspx""</script>")
    End Sub
End Class
