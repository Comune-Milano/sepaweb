
Partial Class MANUTENZIONI_Ricerca_Utenze_Fornitori
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            End If
            If Not IsPostBack Then
                RiempiCampi()

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Private Sub RiempiCampi()
        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            '*********************COMBO FORNITORE**********************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ANAGRAFICA_FORNITORI"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbFornitore.Items.Add(New ListItem(" ", -1))

            While myReader.Read
                cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader("DESCRIZIONE"), " "), par.IfNull(myReader("ID"), -1)))
            End While
            myReader.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Try
            If Me.cmbFornitore.SelectedValue <> "-1" Then
                Response.Write("<script>window.open('Report_Ut_Fornitori.aspx?TIPO=" & Me.RadioButtonList1.SelectedValue.ToString & "&FORNITORE=" & Me.cmbFornitore.SelectedValue.ToString & "' );</script>")
            Else
                Response.Write("<script>alert('Scegliere un Fornitore!')</script>")

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub
End Class
