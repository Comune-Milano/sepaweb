'*** RICERCA MANUTENZIONI da CONSUNTIVARE

Partial Class MANUTENZIONI_RicercaConsuntivi
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            'And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE")))
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = Session.Item("ID_STRUTTURA")
            Else
                sFiliale = "-1"
            End If

            CaricaEsercizio()

          
        End If

    End Sub


    Public Property sFiliale() As String
        Get
            If Not (ViewState("par_sFiliale") Is Nothing) Then
                Return CStr(ViewState("par_sFiliale"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sFiliale") = value
        End Set

    End Property

    'CARICO COMBO FORNITORI
    Private Sub CaricaFornitori()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO L'ELENCO DEI FORNITORI
            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                 & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione " _
                                & " from SISCOM_MI.FORNITORI " _
                                & " where FL_BLOCCATO=0 " _
                                & "   and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                 & " where ID in (select ID_APPALTO " _
                                                                              & " from SISCOM_MI.MANUTENZIONI " _
                                                                              & " where ID_PF_VOCE_IMPORTO is not null " _
                                                                              & "   and STATO=1 " _
                                                                              & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            If sFiliale <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
            End If
            par.cmd.CommandText = par.cmd.CommandText & ")) order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
            '        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '        Else
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '        End If
            '    Else
            '        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            '        Else
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            '        End If
            '    End If
            'End While
            'myReader1.Close()
            '**************************

            Me.cmbFornitore.SelectedValue = "-1"

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO APPALTI
    Private Sub CaricaAppalti()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO L'ELENCO DEGLI APPALTI
            Me.cmbAppalto.Items.Clear()
            '  Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select ID,trim(DESCRIZIONE) as DESCRIZIONE " _
                                & " from  SISCOM_MI.APPALTI " _
                                & " where ID in (select ID_APPALTO " _
                                            & " from SISCOM_MI.MANUTENZIONI " _
                                            & " where ID_PF_VOCE_IMPORTO is not null " _
                                            & "   and STATO=1 " _
                                            & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            If sFiliale <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
            End If
            par.cmd.CommandText = par.cmd.CommandText & ") order by DESCRIZIONE asc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "DESCRIZIONE", True)

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            ' Me.cmbAppalto.SelectedValue = "-1"
            '**************************

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraAppalti()
        Dim FlagConnessione As Boolean
        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select ID,TRIM(NUM_REPERTORIO) || ' - ' ||trim(DESCRIZIONE) as DESCRIZIONE " _
                                & " from  SISCOM_MI.APPALTI " _
                                & " where ID_FORNITORE = " & Me.cmbFornitore.SelectedValue.ToString _
                                & "  and  ID in (select ID_APPALTO " _
                                             & " from SISCOM_MI.MANUTENZIONI " _
                                             & " where ID_PF_VOCE_IMPORTO is not null " _
                                             & " and STATO=1 " _
                                             & " and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            If sFiliale <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
            End If
            par.cmd.CommandText = par.cmd.CommandText & ") order by DESCRIZIONE asc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraFornitori()
        Dim FlagConnessione As Boolean

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbFornitore.Items.Clear()
            '     Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end)  " _
                 & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione " _
                & "from SISCOM_MI.FORNITORI " _
                                & " where FL_BLOCCATO=0 " _
                                & "  and ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                            & " where  ID=" & Me.cmbAppalto.SelectedValue & ")" _
                                & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
            '        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '        Else
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '        End If
            '    Else
            '        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            '        Else
            '            Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            '        End If
            '    End If
            'End While
            'myReader1.Close()
            '**************************

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub cmbFornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged

        If Me.cmbAppalto.SelectedValue = "-1" Then
            If Me.cmbFornitore.SelectedValue <> "-1" Then
                FiltraAppalti()
            Else
                CaricaAppalti()
                CaricaFornitori()
            End If
        End If

    End Sub

    Protected Sub cmbAppalto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAppalto.SelectedIndexChanged

        If Me.cmbFornitore.SelectedValue = "-1" Then
            If Me.cmbAppalto.SelectedValue <> "-1" Then
                FiltraFornitori()
            Else
                CaricaFornitori()
                CaricaAppalti()
            End If
        End If

    End Sub
 Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean
        Try
            Dim dataDAL As String = ""
            If Not IsNothing(txtDataDAL.SelectedDate) Then
                dataDAL = txtDataDAL.SelectedDate
            End If

            Dim dataAL As String = ""
            If Not IsNothing(txtDataAL.SelectedDate) Then
                dataAL = txtDataAL.SelectedDate
            End If
            ControlloCampi = True
            If Strings.Len(Me.txtDataAL.SelectedDate) > 0 Then
                If par.AggiustaData(dataAL) < par.AggiustaData(dataDAL) Then
                    RadWindowManager1.RadAlert("Attenzione...Controllare il range delle date!", 300, 150, "Attenzione", "", "null")

                    ControlloCampi = False
                    Exit Sub
                End If
            End If

            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiConsuntivi.aspx?FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                               & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                               & "&DAL=" & par.IfEmpty(par.AggiustaData(dataDAL), "") _
                                                                               & "&AL=" & par.IfEmpty(par.AggiustaData(dataAL), "") _
                                                                               & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                               & "&PROVENIENZA=RICERCA_CONSUNTIVI" & "');</script>")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
  Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaEsercizio()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            If sFiliale <> "-1" Then

                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO," _
                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_PF_VOCE_IMPORTO is not null " _
                                                                   & "   and ID_STRUTTURA=" & sFiliale & ")" _
                                  & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by 1 desc"

            Else
                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO," _
                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_PF_VOCE_IMPORTO is not null) " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by 1 desc"

            End If


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                'Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", False)

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                '  Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If
                CaricaFornitori()
                CaricaAppalti()
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        CaricaFornitori()
        CaricaAppalti()
    End Sub

   

  
End Class
