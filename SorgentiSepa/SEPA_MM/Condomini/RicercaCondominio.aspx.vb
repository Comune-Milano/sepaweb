
Partial Class Condomini_RicercaCondominio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            CaricaComplessi()
            CaricaEdifici()
            CaricaIndirizzi()
            CaricaAmministratori()


        End If
    End Sub

    Private Sub CaricaComplessi()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim condizione As String = ""
            cmbComplesso.Items.Clear()

            If par.IfEmpty(Me.cmbEdificio.SelectedValue, "-1") <> "-1" Then
                condizione = " and edifici.id = " & Me.cmbEdificio.SelectedValue
            Else
                condizione = "AND edifici.ID IN (SELECT DISTINCT(id_edificio) FROM siscom_mi.cond_edifici)"

            End If

            'cmbComplesso.Items.Add(New ListItem("---", -1))

            '***CARICAMENTO LISTA COMPLESSI
            'par.cmd.CommandText = "SELECT DISTINCT complessi_immobiliari.ID, cod_complesso, complessi_immobiliari.denominazione " _
            '                    & "FROM siscom_mi.complessi_immobiliari,siscom_mi.edifici " _
            '                    & "WHERE edifici.id_complesso = complessi_immobiliari.ID " _
            '                    & " " & condizione _
            '                    & "ORDER BY complessi_immobiliari.denominazione ASC"
            'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While lettore.Read
            '    cmbComplesso.Items.Add(New ListItem(par.IfNull(lettore("denominazione"), " ") & "- -" & " cod." & par.IfNull(lettore("cod_complesso"), " "), par.IfNull(lettore("id"), -1)))
            'End While
            'lettore.Close()

            par.caricaComboBox("SELECT DISTINCT complessi_immobiliari.ID, cod_complesso ||'- -' ||COMPLESSI_IMMOBILIARI.denominazione as complesso FROM siscom_mi.complessi_immobiliari,siscom_mi.edifici WHERE edifici.id_complesso = complessi_immobiliari.ID " & condizione & " ORDER BY COMPLESSO ASC", Me.cmbComplesso, "ID", "complesso", True)

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
    Private Sub CaricaEdifici()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim condizione As String = ""
            cmbEdificio.Items.Clear()

            If par.IfEmpty(Me.cmbComplesso.SelectedValue, "-1") <> "-1" Then
                condizione = " and edifici.id_complesso = " & Me.cmbComplesso.SelectedValue
            Else

            End If
            cmbEdificio.Items.Add(New ListItem("---", -1))

            par.cmd.CommandText = "select id, denominazione,cod_edificio from siscom_mi.edifici " _
                        & "where id in (select distinct(id_edificio) from siscom_mi.cond_edifici) " & condizione & " order by denominazione asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While lettore.Read

                cmbEdificio.Items.Add(New ListItem(par.IfNull(lettore("denominazione"), " ") & "- -" & "cod." & par.IfNull(lettore("COD_EDIFICIO"), " "), par.IfNull(lettore("id"), -1)))

            End While
            lettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub
    Private Sub CaricaIndirizzi()
        Try
            Me.cmbIndirizzo.Items.Clear()
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim condizione As String = ""
            cmbIndirizzo.Items.Add(New ListItem("---", -1))
            If par.IfEmpty(Me.cmbEdificio.SelectedValue, "-1") <> "-1" Then
                condizione = " where edifici.id = " & Me.cmbEdificio.SelectedValue
            ElseIf par.IfEmpty(Me.cmbComplesso.SelectedValue, "-1") <> "-1" Then
                condizione = " where edifici.id_complesso = " & Me.cmbComplesso.SelectedValue
            End If
            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi " _
                                & "WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI " & condizione & ") and id <> 1 " _
                                & "order by descrizione asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While lettore.Read
                cmbIndirizzo.Items.Add(par.IfNull(lettore("descrizione"), " "))
            End While
            lettore.Close()

            Me.cmbCivico.Items.Clear()
            cmbCivico.Items.Add(New ListItem("---", -1))

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")


        End Try
    End Sub
    Private Sub CaricaAmministratori()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            cmbAmministratore.Items.Add(New ListItem("---", -1))

            par.cmd.CommandText = "SELECT id, (cognome ||' '|| nome) as amministratore FROM siscom_mi.cond_amministratori order by cognome asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                cmbAmministratore.Items.Add(New ListItem(par.IfNull(lettore("amministratore"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub
    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If par.IfEmpty(Me.cmbComplesso.SelectedValue, "-1") = "-1" Then
            CaricaComplessi()
        End If
        CaricaIndirizzi()
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        If par.IfEmpty(Me.cmbEdificio.SelectedValue, "-1") = "-1" Then
            CaricaEdifici()
        End If
        CaricaIndirizzi()

    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            Me.cmbCivico.Items.Clear()
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            cmbCivico.Items.Add(New ListItem("---", -1))

            par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' AND ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by civico asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While lettore.Read
                cmbCivico.Items.Add(par.IfNull(lettore("civico"), " "))
            End While
            lettore.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
            End If
            Session.Add("ERRORE", "Provenienza: ApriRicerca " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAvviaRicerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAvviaRicerca.Click

        Response.Redirect("RisRicCondomini.aspx?COMP=" & Me.cmbComplesso.SelectedValue & "&EDIF=" & Me.cmbEdificio.SelectedValue & "&IND=" & par.VaroleDaPassare(Me.cmbIndirizzo.SelectedItem.Text) & "&CIV=" & par.VaroleDaPassare(Me.cmbCivico.SelectedItem.Text) & "&AMMINIST=" & Me.cmbAmministratore.SelectedValue)

    End Sub
End Class
