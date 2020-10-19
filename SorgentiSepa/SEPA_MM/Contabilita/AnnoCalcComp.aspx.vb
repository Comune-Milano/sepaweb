
Partial Class Contabilita_AnnoCalcComp
    Inherits PageSetIdMode
    'Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try



            For i = 2009 To Year(Now)
                cmbAnnoBollette.Items.Add(New ListItem(i, i))
            Next
            If Month(Now) = 12 Then
                cmbAnnoBollette.Items.Add(New ListItem(Year(Now) + 1, Year(Now) + 1))
            End If

            '******APERTURA CONNESSIONE*****
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)
            'End If

            'par.cmd.CommandText = "SELECT DISTINCT ANNO FROM SISCOM_MI.BOL_BOLLETTE"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            cmbAnnoBollette.Items.Add(New ListItem(" ", -1))
            cmbAnnoBollette.Text = Year(Now)

            'While myReader.Read
            '    cmbAnnoBollette.Items.Add(New ListItem(par.IfNull(myReader("ANNO"), " "), par.IfNull(myReader("ANNO"), "")))
            'End While

            'myReader.Close()
            Me.CHIAMATA.Value = Request.QueryString("CHIAMA")
            If Me.CHIAMATA.Value = "MENSI" Then
                Me.lblSottotitolo.Text = "Scegliere l'anno per il quale si vuole calcolare l'ammontare MENSILE del Rimborso Spese al Gestore"
            Else
                Me.lblSottotitolo.Text = "Scegliere l'anno per il quale si vuole calcolare l'ammontare del Rimborso Spese al Gestore"

            End If
            '*********************CHIUSURA CONNESSIONE**********************
            'par.cmd.Dispose()
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
