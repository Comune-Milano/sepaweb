
Partial Class MANUTENZIONI_DettaglioEdificio
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If


        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If


        par.cmd.CommandText = "SELECT DESCRIZIONE_TABELLA, DESCRIZIONE, COD_TIPOLOGIA FROM SISCOM_MI.TABELLE_MILLESIMALI WHERE ID =" & Request.QueryString("ID") & ""
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader
        myReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Me.TabMillesimale.Text = par.IfNull(myReader("DESCRIZIONE_TABELLA"), "")
            Me.descTab.Text = par.IfNull(myReader("DESCRIZIONE"), "")
        End If
        myReader.Close()
        par.cmd.CommandText = "SELECT EDIFICI.denominazione, EDIFICI.cod_edificio,INDIRIZZI.descrizione,INDIRIZZI.civico, INDIRIZZI.cap,INDIRIZZI.localita, (SELECT COUNT(DISTINCT(id_unita_immobiliare)) FROM SISCOM_MI.VALORI_MILLESIMALI WHERE id_tabella=" & Request.QueryString("ID") & ") AS tot_unita FROM SISCOM_MI.EDIFICI, SISCOM_MI.TABELLE_MILLESIMALI,siscom_mi.INDIRIZZI WHERE TABELLE_MILLESIMALI.ID = " & Request.QueryString("ID") & " AND EDIFICI.ID = TABELLE_MILLESIMALI.ID_EDIFICIO and indirizzi.id=edifici.id_indirizzo_principale"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        myReader1 = par.cmd.ExecuteReader()
        If myReader1.Read Then
            lblCodEdificio.Text = par.IfNull(myReader1("COD_EDIFICIO"), "")
            lblDenominazione.Text = par.IfNull(myReader1("DENOMINAZIONE"), "")
            lblIndirizzo.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
            lblCivico.Text = par.IfNull(myReader1("CIVICO"), "")
            lblCap.Text = par.IfNull(myReader1("CAP"), "")
            lblLocalita.Text = par.IfNull(myReader1("LOCALITA"), "")
            lblTotUnita.Text = par.IfNull(myReader1("TOT_UNITA"), "")
        End If
        myReader1.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
End Class
