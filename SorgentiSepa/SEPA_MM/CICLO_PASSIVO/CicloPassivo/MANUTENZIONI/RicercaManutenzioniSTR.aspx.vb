'*** RICERCA MANUTENZIONI

Imports Telerik.Web.UI

Partial Class MANUTENZIONI_RicercaManutenzioniSTR
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String = "-1"
    Public sBP_Generale As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_ESTRAZIONE_STR") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            vId = 0
            '**TipoImmobile = Session.Item("BUILDING_TYPE")
            vId = Session.Item("ID")


            If Session.Item("LIVELLO") <> "1" Then
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = Session.Item("ID_STRUTTURA")
                Else
                    sBP_Generale = Session.Item("ID_STRUTTURA")
                End If
            End If

            CaricaStrutture()

            CaricaStati()

            Me.txtDataDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub

    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property


    'CARICO COMBO STRUTTURE (FILIARI)
    Private Sub CaricaStrutture()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'Me.cmbStruttura.Items.Clear()
            'Me.cmbStruttura.Items.Add(New ListItem(" ", -1))


            If sFiliale <> "-1" Then
                par.cmd.CommandText = "select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                par.cmd.CommandText = "select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                   & " where  ID in (select distinct(ID_STRUTTURA) from SISCOM_MI.MANUTENZIONI  where ID_PF_VOCE_IMPORTO is not null) " _
                                   & " order by NOME asc"
            End If

            'par.cmd.CommandText = "select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
            '                   & " where  ID in (select ID_FILIALE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                                 & " where ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI)) " _
            '                   & " order by NOME asc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)
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
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If sFiliale <> "-1" Then
                Me.cmbStruttura.SelectedValue = sFiliale
                Me.cmbStruttura.Enabled = False
                CaricaEsercizio()

                'FiltraLotti()


                'FiltraComplessi()
                'FiltraEdifici()
                'FiltraServizi()
                'FiltraFornitori()
                'FiltraAppalti()

                'CaricaVociBP()

            Else
                If sBP_Generale <> "" Then
                    Me.cmbStruttura.SelectedValue = sBP_Generale
                    CaricaEsercizio()

                    'FiltraLotti()

                    'FiltraComplessi()
                    'FiltraEdifici()
                    'FiltraServizi()
                    'FiltraFornitori()
                    'FiltraAppalti()

                    'CaricaVociBP()

                Else
                    Me.cmbStruttura.SelectedValue = "-1"
                    CaricaEsercizio()
                    'CaricaLotti()

                    'CaricaComplessi()
                    'CaricaEdifici()
                    'CaricaServizi()
                    'CaricaFornitori()
                    'CaricaAppalti()

                    'CaricaVociBP()

                End If
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO LOTTI
    Private Sub CaricaLotti()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'Me.cmbLotto.Items.Clear()
            'Me.cmbLotto.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select distinct ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.LOTTI " _
                   & " where  ID in (select ID_LOTTO from SISCOM_MI.APPALTI " _
                                 & " where  ID in (select ID_APPALTO  from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null)) " _
                   & " order by DESCRIZIONE asc"

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    Me.cmbLotto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbLotto, "ID", "DESCRIZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbLotto.SelectedValue = "-1"


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO COMPLESSI
    Private Sub CaricaComplessi()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select distinct ID,TRIM(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                               & " where ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null) " _
                               & " order by DENOMINAZIONE asc"
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbComplesso, "ID", "DESCRIZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbComplesso.SelectedValue = "-1"

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO EDIFICI
    Private Sub CaricaEdifici()

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            '**** CARICO L'ELENCO EDIFICI (FABBRICATI)
            'Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            'par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(SISCOM_MI.EDIFICI.ID,1,1)= " & gest & " order by SISCOM_MI.EDIFICI.DENOMINAZIONE asc"

            par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                               & " from SISCOM_MI.EDIFICI " _
                               & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null) " _
                               & " order by DENOMINAZIONE asc"


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            'Me.cmbEdificio.SelectedValue = "-1"
            par.caricaComboTelerik(par.cmd.CommandText, cmbEdificio, "ID", "DESCRIZIONE", True)
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaServizi()

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            'Me.cmbServizio.Items.Clear()
            'Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                               & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null) " _
                               & " order by DESCRIZIONE asc"

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)

            Me.cmbServizio.SelectedValue = "-1"
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO FORNITORI
    Private Sub CaricaFornitori()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
               & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                   & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                               & "where FL_BLOCCATO=0 " _
                               & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                   & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null)) " _
                               & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"



            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)

            'Me.cmbFornitore.Items.Clear()
            'Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            'par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE from SISCOM_MI.FORNITORI " _
            '                    & "where FL_BLOCCATO=0 " _
            '                    & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
            '                                        & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null)) " _
            '                    & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

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
            ''**************************

            'Me.cmbFornitore.SelectedValue = "-1"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO APPALTI (NUM. REPERTORIO)
    Private Sub CaricaAppalti()

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "select ID,trim(NUM_REPERTORIO) || ' ' ||  fornitori.ragione_sociale as ragione_sociale " _
                                & "from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                & "where appalti.ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null) and fornitori.id = appalti.id_fornitore " _
                                & "order by NUM_REPERTORIO asc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "RAGIONE_SOCIALE", True)
            'Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            'par.cmd.CommandText = "select ID,trim(NUM_REPERTORIO) as NUM_REPERTORIO, fornitori.ragione_sociale " _
            '                    & "from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
            '                    & "where appalti.ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is not null) and fornitori.id = appalti.id_fornitore " _
            '                    & "order by NUM_REPERTORIO asc"

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Dim spazio As String = " - "
            '    'Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " "), par.IfNull(myReader1("ID"), -1)))
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " ") & spazio & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbAppalto.SelectedValue = "-1"


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO COMBO VOCI BP 
    Private Sub CaricaVociBP()

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbVoceBP.Items.Clear()
            par.cmd.CommandText = "select ID,trim(CODICE) || ' - ' || trim(DESCRIZIONE) as DESCRIZIONE " _
                            & "from  SISCOM_MI.PF_VOCI " _
                            & "where ID in (select distinct(ID_VOCE) from SISCOM_MI.PF_VOCI_IMPORTO " _
                                        & " where ID in (select distinct(ID_PF_VOCE_IMPORTO) from SISCOM_MI.MANUTENZIONI " _
                                                    & " where ID_PF_VOCE_IMPORTO is not null " _
                                                    & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "

            If sFiliale <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
            End If

            par.cmd.CommandText = par.cmd.CommandText & ")) order by DESCRIZIONE DESC"

            par.caricaComboTelerik(par.cmd.CommandText, cmbVoceBP, "ID", "DESCRIZIONE", True)
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbVoceBP.SelectedValue = "-1"


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO STATI
    Private Sub CaricaStati()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbStato.Items.Clear()
            ' Me.cmbStato.Items.Add(New ListItem(" ", -1))
            '**** modifica per prendere solo ODL in stato consuntivato o emesso
            par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_STATI_ODL where ID IN (1,2) order by ID"
            par.caricaComboTelerik(par.cmd.CommandText, cmbStato, "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    Me.cmbStato.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbStato.SelectedValue = "-1"


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try



    End Sub


    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged

        CaricaEsercizio()

    End Sub

    Protected Sub cmbLotto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbLotto.SelectedIndexChanged

        FiltraComplessi()

    End Sub


    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged

        'If Me.cmbComplesso.SelectedValue <> "-1" Then
        FiltraEdifici()
        'FiltraServizi()
        'FiltraFornitori()
        'FiltraAppalti()
        'Else

        'If Me.cmbStruttura.SelectedValue = "-1" Then CaricaStrutture()
        'If Me.cmbLotto.SelectedValue = "-1" Then CaricaLotti()

        'CaricaEdifici()
        'CaricaServizi()
        'CaricaFornitori()
        'CaricaAppalti()
        'End If

    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged

        FiltraServizi()
        'FiltraFornitori()
        'FiltraAppalti()


        'If Me.cmbEdificio.SelectedValue <> "-1" Then
        'FiltraStrutture()
        'FiltraLotti()
        'FiltraComplessi()
        'FiltraServizi()
        'FiltraFornitori()
        'FiltraAppalti()
        'Else
        'If Me.cmbStruttura.SelectedValue = "-1" Then CaricaStrutture()
        'If Me.cmbLotto.SelectedValue = "-1" Then CaricaLotti()
        'If Me.cmbComplesso.SelectedValue = "-1" Then CaricaComplessi()

        'CaricaServizi()
        'CaricaFornitori()
        'CaricaAppalti()
        'End If

    End Sub

    Protected Sub cmbServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged

        FiltraFornitori()
        FiltraAppalti()


        'If Me.cmbServizio.SelectedValue <> "-1" Then
        'FiltraStrutture()
        'FiltraLotti()
        'FiltraComplessi()
        'FiltraEdifici()
        'FiltraFornitori()
        'FiltraAppalti()
        'Else
        'If Me.cmbStruttura.SelectedValue = "-1" Then CaricaStrutture()
        'If Me.cmbLotto.SelectedValue = "-1" Then CaricaLotti()
        'If Me.cmbComplesso.SelectedValue = "-1" Then CaricaComplessi()
        'If Me.cmbEdificio.SelectedValue = "-1" Then CaricaEdifici()

        'CaricaFornitori()
        'CaricaAppalti()
        'End If
    End Sub


    Protected Sub cmbFornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged

        FiltraAppalti()

        'If Me.cmbFornitore.SelectedValue <> "-1" Then
        '    FiltraAppalti()
        'End If
        'FiltraStrutture()
        'FiltraLotti()
        'FiltraComplessi()
        'FiltraEdifici()
        'FiltraServizi()
        'FiltraAppalti()
        'Else
        'If Me.cmbStruttura.SelectedValue = "-1" Then CaricaStrutture()
        'If Me.cmbLotto.SelectedValue = "-1" Then CaricaLotti()
        'If Me.cmbComplesso.SelectedValue = "-1" Then CaricaComplessi()
        'If Me.cmbEdificio.SelectedValue = "-1" Then CaricaEdifici()
        'If Me.cmbServizio.SelectedValue = "-1" Then CaricaServizi()

        'CaricaAppalti()
        'End If

    End Sub

    Protected Sub cmbAppalto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAppalto.SelectedIndexChanged

        'If Me.cmbFornitore.SelectedValue = "-1" Then
        '    FiltraFornitori()
        'End If
        'If Me.cmbAppalto.SelectedValue <> "-1" Then
        'FiltraStrutture()
        'FiltraLotti()
        'FiltraComplessi()
        'FiltraEdifici()
        'FiltraServizi()
        'FiltraFornitori()
        'Else
        'If Me.cmbStruttura.SelectedValue = "-1" Then CaricaStrutture()
        'If Me.cmbLotto.SelectedValue = "-1" Then CaricaLotti()
        'If Me.cmbComplesso.SelectedValue = "-1" Then CaricaComplessi()
        'If Me.cmbEdificio.SelectedValue = "-1" Then CaricaEdifici()
        'If Me.cmbServizio.SelectedValue = "-1" Then CaricaServizi()
        'If Me.cmbFornitore.SelectedValue = "-1" Then CaricaFornitori()

        'End If


    End Sub


    Private Sub FiltraLotti()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'If Me.cmbLotto.SelectedValue <> "-1" Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If

            'Me.cmbLotto.Items.Clear()
            'Me.cmbLotto.Items.Add(New ListItem(" ", -1))

            '**** CARICO L'ELENCO STRUTTURE (TAB_FILIALI)

            If Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.LOTTI " _
                                   & " where  ID in (select ID_LOTTO from SISCOM_MI.APPALTI " _
                                                 & " where  ID in (select ID_APPALTO  from SISCOM_MI.MANUTENZIONI " _
                                                               & " where ID_PF_VOCE_IMPORTO is not null " _
                                                               & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue _
                                                               & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & "))" _
                                   & " order by DESCRIZIONE asc"

            Else
                par.cmd.CommandText = "select distinct ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.LOTTI " _
                                   & " where  ID in (select ID_LOTTO from SISCOM_MI.APPALTI " _
                                                 & " where  ID in (select ID_APPALTO  from SISCOM_MI.MANUTENZIONI " _
                                                              & " where ID_PF_VOCE_IMPORTO is not null " _
                                                              & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & "))" _
                                   & " order by DESCRIZIONE asc"
            End If


            'If Me.cmbComplesso.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_COMPLESSO=" & cmbComplesso.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_COMPLESSO=" & cmbComplesso.SelectedValue
            '    End If
            'ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & cmbStruttura.SelectedValue & ") "
            '    Else
            '        sWhere = sWhere & "     ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & cmbStruttura.SelectedValue & ") "
            '    End If
            'End If

            'If Me.cmbEdificio.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_EDIFICIO=" & cmbEdificio.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_EDIFICIO=" & cmbEdificio.SelectedValue
            '    End If
            'End If

            'If Me.cmbServizio.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_SERVIZIO=" & cmbServizio.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_SERVIZIO=" & cmbServizio.SelectedValue
            '    End If
            'End If


            'If Me.cmbFornitore.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            '    Else
            '        sWhere = sWhere & "     ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            '    End If
            'End If

            'If Me.cmbAppalto.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_APPALTO=" & cmbAppalto.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_APPALTO=" & cmbAppalto.SelectedValue
            '    End If
            'End If

            'If sWhere <> "" Then
            '    par.cmd.CommandText = par.cmd.CommandText & " where " & sWhere & ")) order by DESCRIZIONE asc"
            'Else
            '    par.cmd.CommandText = par.cmd.CommandText & ")) order by DESCRIZIONE asc"
            'End If


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbLotto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbLotto, "ID", "DESCRIZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If i = 1 Then
                Me.cmbLotto.Items(1).Selected = True
            End If

            FiltraComplessi()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraComplessi()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'If Me.cmbComplesso.SelectedValue <> "-1" Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If

            'Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            If Me.cmbLotto.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   and ID_APPALTO in (select ID from SISCOM_MI.APPALTI " _
                                                                     & "where ID_LOTTO=" & cmbLotto.SelectedValue & ")) " _
                                   & " order by DENOMINAZIONE asc"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & ")" _
                                   & " order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & ")" _
                                   & " order by DENOMINAZIONE asc"
            End If


            'par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                   & " where  ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI "


            'If Me.cmbStruttura.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & cmbStruttura.SelectedValue & ") "
            '    Else
            '        sWhere = sWhere & "     ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE=" & cmbStruttura.SelectedValue & ") "
            '    End If
            'ElseIf Me.cmbLotto.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) "
            '    Else
            '        sWhere = sWhere & "     ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) "
            '    End If
            'End If

            'If Me.cmbEdificio.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_EDIFICIO=" & cmbEdificio.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_EDIFICIO=" & cmbEdificio.SelectedValue
            '    End If
            'End If

            'If Me.cmbServizio.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_SERVIZIO=" & cmbServizio.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_SERVIZIO=" & cmbServizio.SelectedValue
            '    End If
            'End If


            'If Me.cmbFornitore.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            '    Else
            '        sWhere = sWhere & "     ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            '    End If
            'End If

            'If Me.cmbAppalto.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_APPALTO=" & cmbAppalto.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_APPALTO=" & cmbAppalto.SelectedValue
            '    End If
            'End If

            'If sWhere <> "" Then
            '    par.cmd.CommandText = par.cmd.CommandText & " where " & sWhere & ") order by DENOMINAZIONE asc"
            'Else
            '    par.cmd.CommandText = par.cmd.CommandText & ") order by DENOMINAZIONE asc"
            'End If


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            '*************
            par.caricaComboTelerik(par.cmd.CommandText, cmbComplesso, "ID", "DENOMINAZIONE", True)

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            'If i = 1 Then
            '    Me.cmbComplesso.Items(1).Selected = True
            'End If

            FiltraEdifici()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraEdifici()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'If Me.cmbEdificio.SelectedValue <> "-1" Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If


            'Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue _
                                   & " order by DENOMINAZIONE asc"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & ")" _
                                   & " order by DENOMINAZIONE asc"

            Else
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & ") " _
                                   & " order by DENOMINAZIONE asc"

            End If

            'ElseIf Me.cmbLotto.SelectedValue <> "-1" Then
            'par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
            '                   & " from SISCOM_MI.EDIFICI " _
            '                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                                          & " where ID_FILIALI in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) " _
            '                   & " order by DENOMINAZIONE asc"
            'ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
            'par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
            '                   & " from SISCOM_MI.EDIFICI " _
            '                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                                          & " where ID_FILIALI=" & cmbStruttura.SelectedValue & ") " _
            '                   & " order by DENOMINAZIONE asc"

            'Else
            'par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
            '                   & " from SISCOM_MI.EDIFICI " _
            '                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI) " _
            '                   & " order by DENOMINAZIONE asc"
            'End If

            ''par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
            ''                   & " from SISCOM_MI.EDIFICI " _
            ''                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI "



            ''If Me.cmbComplesso.SelectedValue <> "-1" Then
            ''    If sWhere <> "" Then
            ''        sWhere = sWhere & " and ID_COMPLESSO=" & cmbComplesso.SelectedValue
            ''    Else
            ''        sWhere = sWhere & "     ID_COMPLESSO=" & cmbComplesso.SelectedValue
            ''    End If
            ''ElseIf Me.cmbLotto.SelectedValue <> "-1" Then
            ''    If sWhere <> "" Then
            ''        sWhere = sWhere & " and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) "
            ''    Else
            ''        sWhere = sWhere & "     ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) "
            ''    End If
            ''End If

            ''If Me.cmbServizio.SelectedValue <> "-1" Then
            ''    If sWhere <> "" Then
            ''        sWhere = sWhere & " and ID_SERVIZIO=" & cmbServizio.SelectedValue
            ''    Else
            ''        sWhere = sWhere & "     ID_SERVIZIO=" & cmbServizio.SelectedValue
            ''    End If
            ''End If


            ''If Me.cmbFornitore.SelectedValue <> "-1" Then
            ''    If sWhere <> "" Then
            ''        sWhere = sWhere & " and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            ''    Else
            ''        sWhere = sWhere & "     ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            ''    End If
            ''End If

            ''If Me.cmbAppalto.SelectedValue <> "-1" Then
            ''    If sWhere <> "" Then
            ''        sWhere = sWhere & " and ID_APPALTO=" & cmbAppalto.SelectedValue
            ''    Else
            ''        sWhere = sWhere & "     ID_APPALTO=" & cmbAppalto.SelectedValue
            ''    End If
            ''End If

            ''If sWhere <> "" Then
            ''    par.cmd.CommandText = par.cmd.CommandText & " where " & sWhere & ") order by DENOMINAZIONE asc"
            ''Else
            ''    par.cmd.CommandText = par.cmd.CommandText & ") order by DENOMINAZIONE asc"
            ''End If


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbEdificio, "ID", "DENOMINAZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            'If i = 1 Then
            '    Me.cmbEdificio.Items(1).Selected = True
            'End If

            FiltraServizi()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraServizi()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'If Me.cmbServizio.SelectedValue <> "-1" Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If

            'Me.cmbServizio.Items.Clear()
            'Me.cmbServizio.Items.Add(New ListItem(" ", -1))


            If Me.cmbEdificio.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where  ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   and  ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue & ") " _
                                   & " order by DESCRIZIONE asc"

            ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select  ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   and ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue & ") " _
                                   & " order by DESCRIZIONE asc"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then

                par.cmd.CommandText = "select  ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & ")" _
                                    & " order by DESCRIZIONE asc"

            Else
                par.cmd.CommandText = "select  ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is not null " _
                                                & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & " ) " _
                                   & " order by DESCRIZIONE asc"
            End If

            'If Me.cmbEdificio.SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
            '                       & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI " _
            '                                    & " where ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue & ")" _
            '                       & " order by DESCRIZIONE asc"
            'ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
            '       & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI " _
            '                    & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue & ")" _
            '       & " order by DESCRIZIONE asc"
            'Else

            '    par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
            '                       & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI) " _
            '                       & " order by DESCRIZIONE asc"
            'End If



            'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
            '                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.MANUTENZIONI "


            'If Me.cmbComplesso.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_COMPLESSO=" & cmbComplesso.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_COMPLESSO=" & cmbComplesso.SelectedValue
            '    End If
            'ElseIf Me.cmbLotto.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) "
            '    Else
            '        sWhere = sWhere & "     ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID_FILIALE in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & cmbLotto.SelectedValue & ")) "
            '    End If
            'End If

            'If Me.cmbEdificio.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_EDIFICIO=" & cmbEdificio.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_EDIFICIO=" & cmbEdificio.SelectedValue
            '    End If
            'End If


            'If Me.cmbFornitore.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            '    Else
            '        sWhere = sWhere & "     ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & cmbFornitore.SelectedValue & ") "
            '    End If
            'End If

            'If Me.cmbAppalto.SelectedValue <> "-1" Then
            '    If sWhere <> "" Then
            '        sWhere = sWhere & " and ID_APPALTO=" & cmbAppalto.SelectedValue
            '    Else
            '        sWhere = sWhere & "     ID_APPALTO=" & cmbAppalto.SelectedValue
            '    End If
            'End If

            'If sWhere <> "" Then
            '    par.cmd.CommandText = par.cmd.CommandText & " where " & sWhere & ") order by DESCRIZIONE asc"
            'Else
            '    par.cmd.CommandText = par.cmd.CommandText & ") order by DESCRIZIONE asc"
            'End If


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If i = 1 Then
                Me.cmbServizio.Items(1).Selected = True
            End If

            FiltraFornitori()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraAppalti()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'If Me.cmbAppalto.SelectedValue <> "-1" Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If

            Me.cmbAppalto.Items.Clear()
            If Me.cmbFornitore.SelectedValue <> "-1" Then
                par.cmd.CommandText = "SELECT APPALTI.ID,TRIM(NUM_REPERTORIO) || ' - ' || FORNITORI.RAGIONE_SOCIALE AS RAGIONE_SOCIALE " _
                                    & "FROM  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                    & "WHERE APPALTI.ID_FORNITORE=" & Me.cmbFornitore.SelectedValue _
                                    & "  AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                                                & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                                                & "   AND ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & ") " _
                                    & "  AND FORNITORI.ID = APPALTI.ID_FORNITORE" _
                                    & "  order by NUM_REPERTORIO asc"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = "SELECT APPALTI.ID,TRIM(NUM_REPERTORIO) || ' - ' || FORNITORI.RAGIONE_SOCIALE AS RAGIONE_SOCIALE " _
                                    & "FROM  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                    & "WHERE APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                                                & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                                                & "   AND ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                & "   AND ID_STRUTTURA=" & cmbStruttura.SelectedValue & ")" _
                                    & "  AND FORNITORI.ID = APPALTI.ID_FORNITORE" _
                                    & "  order by NUM_REPERTORIO asc"
            Else
                par.cmd.CommandText = "SELECT APPALTI.ID,TRIM(NUM_REPERTORIO) || ' - ' || FORNITORI.RAGIONE_SOCIALE AS RAGIONE_SOCIALE " _
                                    & "FROM  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                    & "WHERE APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                                                & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                                                & "   AND ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & ") " _
                                    & "  AND FORNITORI.ID = APPALTI.ID_FORNITORE" _
                                    & "  order by NUM_REPERTORIO asc"

            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "RAGIONE_SOCIALE", True)
            'Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            'If Me.cmbFornitore.SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "select appalti.ID,trim(NUM_REPERTORIO) as NUM_REPERTORIO, fornitori.ragione_sociale " _
            '                        & "from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
            '                        & "where appalti.ID_FORNITORE=" & Me.cmbFornitore.SelectedValue _
            '                        & "  and appalti.ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
            '                                    & " where ID_PF_VOCE_IMPORTO is not null " _
            '                                    & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & ") " _
            '                        & "  and fornitori.id = appalti.id_fornitore" _
            '                        & "  order by NUM_REPERTORIO asc"

            'ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "select appalti.ID,trim(NUM_REPERTORIO) as NUM_REPERTORIO, fornitori.ragione_sociale " _
            '                        & "from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
            '                        & "where appalti.ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
            '                                    & " where ID_PF_VOCE_IMPORTO is not null " _
            '                                    & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
            '                                    & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & ")" _
            '                        & "  and fornitori.id = appalti.id_fornitore" _
            '                        & "  order by NUM_REPERTORIO asc"
            'Else

            '    par.cmd.CommandText = "select appalti.ID,trim(NUM_REPERTORIO) as NUM_REPERTORIO, fornitori.ragione_sociale " _
            '                        & "from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
            '                        & "where appalti.ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
            '                                    & " where ID_PF_VOCE_IMPORTO is not null " _
            '                                    & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & ") " _
            '                        & "  and fornitori.id = appalti.id_fornitore" _
            '                        & "  order by NUM_REPERTORIO asc"

            'End If




            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Dim spazio As String = " - "
            '    i = i + 1
            '    'Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " "), par.IfNull(myReader1("ID"), -1)))
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " ") & spazio & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If
        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraFornitori()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'If Me.cmbFornitore.SelectedValue <> "-1" Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If

            Me.cmbFornitore.Items.Clear()
            'cmbFornitore.Items.Add(New ListItem(" ", -1))

            'If Me.cmbServizio.SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE from SISCOM_MI.FORNITORI " _
            '                        & "where FL_BLOCCATO=0 " _
            '                        & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
            '                                                        & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
            '                                                                    & "  where  ID_PF_VOCE_IMPORTO is not null " _
            '                                                                    & "    and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
            '                                                                    & "    and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue & ")) " _
            '                        & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            'ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE from SISCOM_MI.FORNITORI " _
            '                     & "where FL_BLOCCATO=0 " _
            '                     & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
            '                                                     & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
            '                                                                 & " where ID_PF_VOCE_IMPORTO is not null " _
            '                                                                 & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
            '                                                                 & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & "))" _
            '                   & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"


            'Else
            '    par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE from SISCOM_MI.FORNITORI " _
            '                        & "where FL_BLOCCATO=0 " _
            '                        & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
            '                                                        & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
            '                                                                     & " where ID_PF_VOCE_IMPORTO is not null " _
            '                                                                     & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & " )) " _
            '                        & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            'End If
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    i = i + 1
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

            If Me.cmbServizio.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE,  " _
                                    & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                    & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                                    & "where FL_BLOCCATO=0 " _
                                    & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                    & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                                & "  where  ID_PF_VOCE_IMPORTO is not null " _
                                                                                & "    and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                                                & "    and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue & ")) " _
                                    & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                    & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                    & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                                 & "where FL_BLOCCATO=0 " _
                                 & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                 & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                             & " where ID_PF_VOCE_IMPORTO is not null " _
                                                                             & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                                             & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & "))" _
                               & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"


            Else
                par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                    & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                    & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                                    & "where FL_BLOCCATO=0 " _
                                    & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                    & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                                 & " where ID_PF_VOCE_IMPORTO is not null " _
                                                                                 & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " & " )) " _
                                    & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If i = 1 Then
                Me.cmbFornitore.Items(1).Selected = True
            End If

            FiltraAppalti()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean

        Try

            ControlloCampi = True

            If Strings.Len(Me.txtDataAL.SelectedDate.ToString) > 0 Then
                If par.AggiustaData(Me.txtDataAL.SelectedDate) < par.AggiustaData(Me.txtDataDAL.SelectedDate) Then
                    RadWindowManager1.RadAlert("Attenzione...Controllare il range delle date!", 300, 150, "Attenzione", "", "null")

                    ControlloCampi = False
                    Exit Sub
                End If
            End If

            If ControlloCampi = True Then
                Dim dataDal As String = ""
                If Not IsNothing(txtDataDAL.SelectedDate) Then
                    dataDal = txtDataDAL.SelectedDate
                End If
                Dim dataAl As String = ""
                If Not IsNothing(txtDataAL.SelectedDate) Then
                    dataAl = txtDataAL.SelectedDate
                End If
                Response.Write("<script>location.replace('RisultatiManutenzioniSTR.aspx?FI=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                                 & "&LO=" & Me.cmbLotto.SelectedValue.ToString _
                                                                                 & "&CO=" & Me.cmbComplesso.SelectedValue.ToString _
                                                                                 & "&ED=" & Me.cmbEdificio.SelectedValue.ToString _
                                                                                 & "&SE=" & Me.cmbServizio.SelectedValue.ToString _
                                                                                 & "&FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                                 & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                                 & "&BP=" & Me.cmbVoceBP.SelectedValue.ToString _
                                                                                 & "&DAL=" & par.IfEmpty(par.AggiustaData(dataDal), "") _
                                                                                 & "&AL=" & par.IfEmpty(par.AggiustaData(dataAl), "") _
                                                                                 & "&ST=" & Me.cmbStato.SelectedValue.ToString _
                                                                                 & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                                 & "&AUT=" & Me.DropDownListAutorizzazione.SelectedValue.ToString _
                                                                                 & "&PROVENIENZA=RICERCA_MANUTENZIONI" & "');</script>")
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


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub



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

            Me.cmbEsercizio.Items.Clear()


            If Me.cmbStruttura.SelectedValue <> "-1" Then

                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE, SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_PF_VOCE_IMPORTO is not null " _
                                                                   & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & ") " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by 1 desc"

            Else
                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE, SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
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

                Me.cmbEsercizio.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            'par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", False)

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                '   Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
            End If


            If Me.cmbEsercizio.Items.Count <= 1 Then
                Me.cmbEsercizio.Enabled = False
            Else
                Me.cmbEsercizio.Enabled = True
            End If

            If i > 0 Then

                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                FiltraLotti()

                FiltraComplessi()
                FiltraEdifici()
                FiltraServizi()
                FiltraFornitori()
                FiltraAppalti()

                CaricaVociBP()
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

        FiltraLotti()

        FiltraComplessi()
        FiltraEdifici()
        FiltraServizi()
        FiltraFornitori()
        FiltraAppalti()

        CaricaVociBP()

    End Sub

    Protected Sub cmbStato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStato.SelectedIndexChanged
        If Me.cmbStato.SelectedValue = 2 Then
            Me.DropDownListAutorizzazione.ClearSelection()
            Me.DropDownListAutorizzazione.SelectedValue = 1
            Me.DropDownListAutorizzazione.Enabled = False
        Else
            Me.DropDownListAutorizzazione.Enabled = True
            'Me.DropDownListAutorizzazione.SelectedValue = -1
        End If
    End Sub
End Class
