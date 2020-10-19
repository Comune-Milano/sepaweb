'*** RICERCA PAGAMENTI DA APPROVARE o APPROVATI ma DA STAMPARE o RISTAMPARE

Partial Class PAGAMENTI_CANONE_RicercaPagamentiS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String = "-1"
    Public sBP_Generale As String = ""

    Public sValoreTipo As String = ""
    Public sSelectWhere As String = ""

    Public sValoreStato As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            sValoreTipo = Request.QueryString("TIPO")
            If sValoreTipo = "APPROVATI" Then
                lblTitolo.Text = "Ordini - Pagamenti a canone - Approvati"
            Else
                lblTitolo.Text = "Ordini - Pagamenti a canone - Emesso Sal"
            End If
            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            '  Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            CaricaStrutture()

        End If

    End Sub



    Private Function SettaSelectRicercaStruttura() As String
        Try


            sValoreTipo = Request.QueryString("TIPO")   ''APPROVATI,APPROVATI_SCADENZA,DA_STAMPARE_PAG


            SettaSelectRicercaStruttura = ""

            Select Case sValoreTipo

                Case "APPROVATI"
                    'PRENOTAZIONI.ID_STATO=1 + PAGAMENTI.ID_STATO=0 (bottone Approva di Pagamenti)

                    SettaSelectRicercaStruttura = " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                                                & "   and SISCOM_MI.PAGAMENTI.ID_STATO=0 " _
                                                & "   and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                                                                 & " from  SISCOM_MI.PRENOTAZIONI " _
                                                                                 & " where TIPO_PAGAMENTO=6 " _
                                                                                 & "   and ID_STATO in (1,2) "


                    If Me.cmbStruttura.SelectedValue <> "-1" Then
                        SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                    End If

                    If Me.cmbEsercizio.SelectedValue <> "-1" Then
                        SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_VOCE_PF in ( select distinct(ID) " _
                                                                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                                                                   & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                    End If


                Case "APPROVATI_SCADENZA" 'ICONA PAGINA HOME
                    'PRENOTAZIONI.ID_STATO=2 + PAGAMENTI.ID_STATO=1 + PAGAMENTI.DATA_STAMPA is null (bottone Stampa SAL Pagamenti) senza PAGAMENTI

                    SettaSelectRicercaStruttura = " where  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                                                & "   and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                                                & "   and  SISCOM_MI.PAGAMENTI.DATA_STAMPA is null " _
                                                & "   and  SISCOM_MI.PAGAMENTI.ID in (select  distinct(ID_PAGAMENTO)  " _
                                                                                 & " from  SISCOM_MI.PRENOTAZIONI " _
                                                                                 & " where TIPO_PAGAMENTO=6 " _
                                                                                 & "   and ID_STATO=2 "


                    If Me.cmbStruttura.SelectedValue <> "-1" Then
                        SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                    End If

                    If Me.cmbEsercizio.SelectedValue <> "-1" Then
                        SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_VOCE_PF in ( select distinct(ID) " _
                                                                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                                                                   & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                    End If


                Case "DA_STAMPARE_PAG"

                    SettaSelectRicercaStruttura = " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                                                & "   and SISCOM_MI.PAGAMENTI.ID_STATO>0 " _
                                                & "   and  SISCOM_MI.PAGAMENTI.ID in (select  distinct(ID_PAGAMENTO)  " _
                                                                                 & " from  SISCOM_MI.PRENOTAZIONI " _
                                                                                 & " where TIPO_PAGAMENTO=6 " _
                                                                                 & "   and ID_STATO>=1 "


                    If Me.cmbStruttura.SelectedValue <> "-1" Then
                        SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                    End If

                    If Me.cmbEsercizio.SelectedValue <> "-1" Then
                        SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " and PRENOTAZIONI.ID_VOCE_PF in ( select distinct(ID) " _
                                                                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                                                                   & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                    End If


            End Select

            SettaSelectRicercaStruttura = SettaSelectRicercaStruttura & " )"
        Catch ex As Exception

        End Try
    End Function


    'CARICO COMBO STRUTTURE (FILIARI)
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


            'If Session.Item("LIVELLO") <> "1" Then
            If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                sFiliale = Session.Item("ID_STRUTTURA")
            Else
                sBP_Generale = Session.Item("ID_STRUTTURA")
            End If
            'End If

            Me.cmbStruttura.Items.Clear()
            'Me.cmbStruttura.Items.Add(New ListItem(" ", -1))


            If sFiliale <> "-1" Then
                par.cmd.CommandText = " select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                par.cmd.CommandText = " select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                   & " where  ID in (select ID_STRUTTURA from SISCOM_MI.PRENOTAZIONI where TIPO_PAGAMENTO=6) or id = " & sBP_Generale _
                                   & " order by NOME asc"

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                ' Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))

                If sFiliale <> "-1" Then
                    Me.cmbStruttura.SelectedValue = par.IfNull(myReader1("ID"), -1)
                    Me.cmbStruttura.Enabled = False
                End If

            End While
            myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)

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

            'CaricaFornitori()
            'CaricaAppalti()


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


    'CARICO COMBO FORNITORI
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
            '  Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                    & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione " _
                                & " from SISCOM_MI.FORNITORI " _
                                & " where ID in (select ID_FORNITORE from SISCOM_MI.PAGAMENTI " & sSelectWhere & ")"

            par.cmd.CommandText = par.cmd.CommandText & " order by COD_FORNITORE, RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
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


    'CARICO COMBO APPALTI (NUM. REPERTORIO)
    Private Sub CaricaAppalti()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0

        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            sSelectWhere = SettaSelectRicercaStruttura()

            Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,trim(SISCOM_MI.APPALTI.NUM_REPERTORIO) || ' - ' || trim(SISCOM_MI.APPALTI.DESCRIZIONE) as NUM_REPERTORIO " _
                                & " from  SISCOM_MI.APPALTI " _
                                & " where ID in (select ID_APPALTO  from SISCOM_MI.PAGAMENTI " & sSelectWhere

            If Me.cmbFornitore.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & Me.cmbFornitore.SelectedValue
            End If

            par.cmd.CommandText = par.cmd.CommandText & " ) order by SISCOM_MI.APPALTI.NUM_REPERTORIO asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                ' Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "NUM_REPERTORIO", True)

            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            If i = 2 Then
                Me.cmbAppalto.Items(1).Selected = True
            Else
                Me.cmbAppalto.SelectedValue = "-1"
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
        CaricaAppalti()
    End Sub

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged

        CaricaEsercizio()

    End Sub


    'CARICO COMBO ESERCIZI FINANZIARI
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
            If Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                    & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-'|| " _
                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                   & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                   & " where ID in (select distinct(ID_VOCE_PF) " _
                                                                                & " from SISCOM_MI.PRENOTAZIONI " _
                                                                                & " where TIPO_PAGAMENTO=6 " _
                                                                                & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & " ) )  order by T_ESERCIZIO_FINANZIARIO.ID desc"

            Else
                par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                    & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-'|| " _
                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                   & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                   & " where ID in (select distinct(ID_VOCE_PF) " _
                                                                                & " from SISCOM_MI.PRENOTAZIONI " _
                                                                                & " where TIPO_PAGAMENTO=6 ) ) order by T_ESERCIZIO_FINANZIARIO.ID desc"
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                ' Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
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
                    CaricaAppalti()
                Case Else
                    Me.cmbEsercizio.Enabled = True
                    If ID_ANNO_EF_CORRENTE <> -1 Then
                        Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                    End If
                    CaricaFornitori()
                    CaricaAppalti()

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


    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        CaricaFornitori()
        CaricaAppalti()

    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click

        Try

            sValoreTipo = Request.QueryString("TIPO")   ''APPROVATI,APPROVATI_SCADENZA,DA_STAMPARE_PAG

            Response.Write("<script>location.replace('RisultatiPagamentiStampa.aspx?FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                                & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                                & "&ST=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                                & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                                & "&TIPO=" & sValoreTipo & "');</script>")


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub


End Class
