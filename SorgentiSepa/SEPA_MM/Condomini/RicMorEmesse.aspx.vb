
Partial Class Condomini_RicMorEmesse
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        If Not IsPostBack Then
            CaricaCondomini()

            txtRifDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtRifAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If
    End Sub
    Private Sub CaricaCondomini()
        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT ID, denominazione FROM siscom_mi.CONDOMINI WHERE ID IN (" _
                                & "SELECT DISTINCT id_condominio FROM siscom_mi.COND_MOROSITA,SISCOM_MI.COND_MOROSITA_LETTERE " _
                                & "WHERE COND_MOROSITA.ID = COND_MOROSITA_LETTERE.id_morosita " _
                                & "AND COND_MOROSITA_LETTERE.bollettino IS NOT NULL " _
                                & ") ORDER BY denominazione ASC "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.cmbCondominio.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbCondominio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        If Not String.IsNullOrEmpty(txtRifDa.Text) And Not String.IsNullOrEmpty(txtRifAl.Text) Then
            If par.AggiustaData(txtRifDa.Text) > par.AggiustaData(txtRifAl.Text) Then
                Response.Write("<script>alert('La data di riferimento finale deve essere maggiore di quella di inizio. Riprovare!');</script>")
                Exit Sub
            End If
        End If
        Response.Write("<script>parent.main.location.replace('RptMorEmesse.aspx?IDCOND=" & Me.cmbCondominio.SelectedValue.ToString & "&DAL=" & Me.txtRifDa.Text & "&AL=" & Me.txtRifAl.Text & "');</script>")
    End Sub
End Class
