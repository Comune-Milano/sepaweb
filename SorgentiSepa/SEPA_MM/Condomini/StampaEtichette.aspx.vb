
Partial Class Condomini_StampaEtichette
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaAmministratori()
            CaricaCondomini()
            Me.rdbTipoEtichetta.SelectedValue = 0
        End If
    End Sub
    Private Sub CaricaAmministratori()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select id, (cognome || ' ' || nome ) as amministratore from siscom_mi.cond_amministratori where id in (select distinct id_amministratore from siscom_mi.cond_amministrazione) order by cognome asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkAmministratori.DataSource = dt
            Me.chkAmministratori.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub CaricaCondomini()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini order by denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Me.chkCondomini.DataSource = dt
            Me.chkCondomini.DataBind()
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaCondomini " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub CaricaCheckAmministratori()
        Try
            chkCondomini.Items.Clear()
            Dim StringaCheckAmministratori As String = ""
            For Each Items As ListItem In chkAmministratori.Items
                If Items.Selected = True Then
                    StringaCheckAmministratori = StringaCheckAmministratori & Items.Value & ","
                End If
            Next
            If StringaCheckAmministratori <> "" Then
                StringaCheckAmministratori = Left(StringaCheckAmministratori, Len(StringaCheckAmministratori) - 1)
                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                par.cmd.CommandText = "select id, denominazione from siscom_mi.condomini " _
                                    & "WHERE SISCOM_MI.CONDOMINI.ID IN (select id_condominio from siscom_mi.cond_amministrazione where siscom_mi.cond_amministrazione.ID_AMMINISTRATORE in (" & StringaCheckAmministratori & ") and siscom_mi.cond_amministrazione.DATA_FINE is null) " _
                                    & "order by denominazione asc"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                Me.chkCondomini.DataSource = dt
                Me.chkCondomini.DataBind()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                CaricaAmministratori()
                CaricaCondomini()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza: CaricaCheckAmministratori " & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnSelAmm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelAmm.Click
        If SelAmminist.Value = 0 Then
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = True
            Next
            SelAmminist.Value = 1
        Else
            For Each i As ListItem In chkAmministratori.Items
                i.Selected = False
            Next
            SelAmminist.Value = 0
        End If
        CaricaCheckAmministratori()
    End Sub
    Protected Sub btnSelCondomini_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelCondomini.Click
        If SelCondomini.Value = 0 Then
            For Each i As ListItem In chkCondomini.Items
                i.Selected = True
            Next
            SelCondomini.Value = 1
        Else
            For Each i As ListItem In chkCondomini.Items
                i.Selected = False
            Next
            SelCondomini.Value = 0
        End If
    End Sub
    Protected Sub chkAmministratori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAmministratori.SelectedIndexChanged
        CaricaCheckAmministratori()
    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.href=""pagina_home.aspx"";</script>")
    End Sub
    Protected Sub btnStampaEtichette_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampaEtichette.Click
        Dim ammSelezionati As String = ""
        Dim CondSelezionati As String = ""
        For Each i As ListItem In chkAmministratori.Items
            If i.Selected = True Then
                ammSelezionati += i.Value & ","
            End If
        Next
        For Each i As ListItem In chkCondomini.Items
            If i.Selected = True Then
                CondSelezionati += i.Value & ","
            End If
        Next
        If ammSelezionati <> "" Then
            ammSelezionati = ammSelezionati.Substring(0, ammSelezionati.LastIndexOf(","))
        End If
        If CondSelezionati <> "" Then
            CondSelezionati = CondSelezionati.Substring(0, CondSelezionati.LastIndexOf(","))
        End If
        If ammSelezionati = "" And CondSelezionati = "" Then
            Response.Write("<script>alert('Selezionare almeno un criterio (Amministratore/Condomini) per generare le etichette')</script>")
            Exit Sub
        End If
        If rdbTipoEtichetta.SelectedValue = 0 And ammSelezionati = "" Then
            Response.Write("<script>alert('Selezionare almeno un criterio Amministratore per generare le etichette')</script>")
            Exit Sub
        ElseIf rdbTipoEtichetta.SelectedValue = 1 And CondSelezionati = "" Then
            Response.Write("<script>alert('Selezionare almeno un criterio Condomini per generare le etichette')</script>")
            Exit Sub
        End If
        If ammSelezionati <> "" Then
            Session.Add("AMMSEL", ammSelezionati)
        End If
        If CondSelezionati <> "" Then
            Session.Add("CONDSEL", CondSelezionati)
        End If
        Response.Write("<script>window.open('GeneraEtichette.aspx?TIPO=" & Me.rdbTipoEtichetta.SelectedValue & "','Etichetta','')</script>")
    End Sub
End Class
