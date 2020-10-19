
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RicercaPagamentiUtenza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public sFiliale As String = "-1"
    Public sBP_Generale As String = ""
    Public sSelectWhere As String = ""
    Public sValoreStato As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            'Me.cmbFornitore.Items.Clear()
            'Me.cmbFornitore.Items.Add(New ListItem(" ", -1))
            If Request.QueryString("TIPO") = "C" Then
                Me.lblTitolo.Text = "Custodi - Ricerca CDP emessi"
            ElseIf Request.QueryString("TIPO") = "M" Then
                Me.lblTitolo.Text = "Multe - Ricerca CDP emessi"
            ElseIf Request.QueryString("TIPO") = "COSAP" Then
                Me.lblTitolo.Text = "Cosap - Ricerca CDP emessi"

            End If
            CaricaStrutture()
        End If
    End Sub
    Private Sub CaricaStrutture()
        Dim FlagConnessione As Boolean
        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                sFiliale = Session.Item("ID_STRUTTURA")
            Else
                sBP_Generale = Session.Item("ID_STRUTTURA")
            End If
            'Me.cmbStruttura.Items.Clear()
            'Me.cmbStruttura.Items.Add(New ListItem(" ", -1))
            If sFiliale <> "-1" Then
                par.cmd.CommandText = " select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                If Request.QueryString("TIPO") <> "M" Then
                    par.cmd.CommandText = " select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                       & " where  ID in (select ID_STRUTTURA from SISCOM_MI.PRENOTAZIONI where TIPO_PAGAMENTO=12) or id = " & sBP_Generale _
                                       & " order by NOME asc"
                ElseIf Request.QueryString("TIPO") <> "COSAP" Then
                    par.cmd.CommandText = " select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                      & " where  ID in (select ID_STRUTTURA from SISCOM_MI.PRENOTAZIONI where TIPO_PAGAMENTO=16) or id = " & sBP_Generale _
                                      & " order by NOME asc"
                Else
                    par.cmd.CommandText = " select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                    & " where  ID in (select ID_STRUTTURA from SISCOM_MI.PRENOTAZIONI where TIPO_PAGAMENTO=14) or id = " & sBP_Generale _
                    & " order by NOME asc"

                End If

            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '    If sFiliale <> "-1" Then
            '        Me.cmbStruttura.SelectedValue = par.IfNull(myReader1("ID"), -1)
            '        Me.cmbStruttura.Enabled = False
            '    End If
            'End While
            'myReader1.Close()
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            If sFiliale <> "-1" Then
                Me.cmbStruttura.SelectedValue = sFiliale
                Me.cmbStruttura.Enabled = False
                CaricaEsercizio()
            Else
                If sBP_Generale <> "" Then
                    Me.cmbStruttura.SelectedValue = sBP_Generale
                    CaricaEsercizio()
                Else
                    Me.cmbStruttura.SelectedValue = "-1"
                End If
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
    Private Sub CaricaEsercizio()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1
        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            Me.cmbEsercizio.Items.Clear()
            If Request.QueryString("TIPO") = "COSAP" Then
                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                        & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                        & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                        & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                        & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                        & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                        & " from SISCOM_MI.PF_VOCI " _
                                        & " where ID in (select distinct(ID_VOCE_PF) " _
                                        & " from SISCOM_MI.PRENOTAZIONI " _
                                        & " where TIPO_PAGAMENTO=16 " _
                                        & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & " ) ) order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.id desc"
                ElseIf Request.QueryString("TIPO") <> "M" Then
                    If Me.cmbStruttura.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                        & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                        & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                        & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                        & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                        & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                        & " from SISCOM_MI.PF_VOCI " _
                                        & " where ID in (select distinct(ID_VOCE_PF) " _
                                        & " from SISCOM_MI.PRENOTAZIONI " _
                                        & " where TIPO_PAGAMENTO=12 " _
                                        & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & " ) ) order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.id desc"
                Else
                    par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                        & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                        & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                        & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                        & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                        & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                        & " from SISCOM_MI.PF_VOCI " _
                                        & " where ID in (select distinct(ID_VOCE_PF) " _
                                        & " from SISCOM_MI.PRENOTAZIONI " _
                                        & " where TIPO_PAGAMENTO=12 ) ) order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.id desc"
                End If

            Else
                    par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                        & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                        & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                        & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                        & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                        & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                        & " from SISCOM_MI.PF_VOCI " _
                                        & " where ID in (select distinct(ID_VOCE_PF) " _
                                        & " from SISCOM_MI.PRENOTAZIONI " _
                                        & " where TIPO_PAGAMENTO=16 ) ) order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.id desc"
                End If


            Else
                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                        & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                        & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                        & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                        & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                        & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                        & " from SISCOM_MI.PF_VOCI " _
                                        & " where ID in (select distinct(ID_VOCE_PF) " _
                                        & " from SISCOM_MI.PRENOTAZIONI " _
                                        & " where TIPO_PAGAMENTO=14 " _
                                        & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & " ) ) order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.id desc"
                Else
                    par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                        & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || ' (' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                        & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                        & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                        & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                        & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                        & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                        & " from SISCOM_MI.PF_VOCI " _
                                        & " where ID in (select distinct(ID_VOCE_PF) " _
                                        & " from SISCOM_MI.PRENOTAZIONI " _
                                        & " where TIPO_PAGAMENTO=14 ) ) order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.id desc"
                End If
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
            '**************************
            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If
            Select Case i
                Case 0
                    Me.cmbEsercizio.Items.Clear()
                    ' Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                    Me.cmbEsercizio.Enabled = False
                Case 1
                    Me.cmbEsercizio.Items(0).Selected = True
                    Me.cmbEsercizio.Enabled = False
                    CaricaFornitori()
                Case Else
                    Me.cmbEsercizio.Enabled = True
                    If ID_ANNO_EF_CORRENTE <> -1 Then
                        Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                    End If
                    CaricaFornitori()
            End Select
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
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        CaricaEsercizio()
    End Sub
    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        CaricaFornitori()
    End Sub
    Private Function SettaSelectRicercaStruttura() As String
        SettaSelectRicercaStruttura = ""
        Try
            If Request.QueryString("TIPO") <> "M" Then
                SettaSelectRicercaStruttura = " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 12" _
                                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO = 1 " _
                                            & "   and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                            & " from  SISCOM_MI.PRENOTAZIONI " _
                                            & " where TIPO_PAGAMENTO=12 " _
                                            & "   and ID_STATO in (1,2) "
                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                End If
                If Me.cmbEsercizio.SelectedValue <> "-1" Then
                    SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_VOCE_PF in ( select distinct(ID) " _
                                                                              & " from SISCOM_MI.PF_VOCI " _
                                                                              & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                End If
                SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " )"
            ElseIf Request.QueryString("TIPO") <> "COSAP" Then
                SettaSelectRicercaStruttura = " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 16" _
                                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO = 1 " _
                                            & "   and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                            & " from  SISCOM_MI.PRENOTAZIONI " _
                                            & " where TIPO_PAGAMENTO=16 " _
                                            & "   and ID_STATO in (1,2) "
                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                End If
                If Me.cmbEsercizio.SelectedValue <> "-1" Then
                    SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_VOCE_PF in ( select distinct(ID) " _
                                                                              & " from SISCOM_MI.PF_VOCI " _
                                                                              & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                End If
                SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " )"

            Else
                SettaSelectRicercaStruttura = " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 14" _
                                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO = 1 " _
                                            & "   and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                            & " from  SISCOM_MI.PRENOTAZIONI " _
                                            & " where TIPO_PAGAMENTO=14 " _
                                            & "   and ID_STATO in (1,2) "
                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                End If
                If Me.cmbEsercizio.SelectedValue <> "-1" Then
                    SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_VOCE_PF in ( select distinct(ID) " _
                                                                              & " from SISCOM_MI.PF_VOCI " _
                                                                              & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                End If
                SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " )"

            End If

        Catch ex As Exception
            SettaSelectRicercaStruttura = ""
        End Try
    End Function
    Private Sub CaricaFornitori()
        Dim FlagConnessione As Boolean
        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            sSelectWhere = SettaSelectRicercaStruttura()
            Me.cmbFornitore.Items.Clear()
            'Me.cmbFornitore.Items.Add(New ListItem(" ", -1))
            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione " _
                                & " from SISCOM_MI.FORNITORI " _
                                & " where ID in (select ID_FORNITORE from SISCOM_MI.PAGAMENTI " & sSelectWhere & ")"
            par.cmd.CommandText = par.cmd.CommandText & " order by COD_FORNITORE, RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
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
            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)
            Me.cmbFornitore.SelectedValue = "-1"
            '**************************
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

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Try
            Response.Write("<script>location.replace('RisultatiRicercaPagamentiUtenza.aspx?FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                                       & "&ST=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                                       & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString & "&TIPO=" & Request.QueryString("TIPO") & "');</script>")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


End Class
