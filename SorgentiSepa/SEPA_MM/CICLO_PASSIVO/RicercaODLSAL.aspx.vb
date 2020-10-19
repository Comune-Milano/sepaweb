Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_RicercaODLSAL
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            RiempiCampi()
            CaricaEsercizio()
        End If
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Write("<script>document.location.href=""Pagina_home_ncp.aspx""</script>")
    End Sub

    Private Sub btnAvviaRicerca_Click(sender As Object, e As EventArgs) Handles btnAvviaRicerca.Click
        Response.Redirect("ElencoODLSAL.aspx?NUM_REPERTORIO=" & par.PulisciStrSql(txtNumRepertorio.Text) _
                           & "&FO=" & par.PulisciStrSql(cmbfornitore.SelectedValue) _
                           & "&LIQUIDAZIONE=" & par.PulisciStrSql(cmbLiquidazione.SelectedValue) _
                           & "&ES=" & par.PulisciStrSql(cmbesercizio.SelectedValue) _
                           & "&STATOSAL=" & par.PulisciStrSql(cmbStatoSAL.SelectedValue) _
                           & "&SERVIZIO=" & par.PulisciStrSql(cmbServizio.SelectedValue) _
                           , False)
    End Sub

    Private Sub RiempiCampi()
        Try
            connData.apri()
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "SELECT ID, COD_FORNITORE, RAGIONE_SOCIALE, COGNOME, NOME FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI) ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            myReader1 = par.cmd.ExecuteReader()
            cmbfornitore.Items.Add(New RadComboBoxItem(" ", -1))
            While myReader1.Read
                If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
                    cmbfornitore.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("COD_FORNITORE"), " ") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
                Else
                    cmbfornitore.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("COD_FORNITORE"), " ") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
                End If
            End While
            cmbfornitore.SelectedValue = -1
            myReader1.Close()


            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI ORDER BY DESCRIZIONE ASC", cmbServizio, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaEsercizio()
        Try
            Dim i As Integer = 0
            Dim ID_ANNO_EF_CORRENTE As Long = -1
            '*****PEPPE MODIFY 30/09/2010*****
            '************APERTURA CONNESSIONE**********
            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE SISCOM_MI.PF_MAIN.ID_STATO=5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || PF_STATI.descrizione || ')' as stato FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,siscom_mi.PF_STATI " _
                                & "WHERE id_stato > = 5 and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND id_stato = PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbesercizio, "ID", "STATO", True)
            If i = 1 Then
                Me.cmbesercizio.Items(0).Selected = True
                Me.cmbesercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbesercizio.Items.Clear()
                Me.cmbesercizio.Enabled = False
            End If

            'If i > 0 Then
            '    If ID_ANNO_EF_CORRENTE <> -1 Then
            '        Me.cmbesercizio.SelectedValue = ID_ANNO_EF_CORRENTE
            '    End If
            'End If


            '************CHIUSURA CONNESSIONE**********
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
