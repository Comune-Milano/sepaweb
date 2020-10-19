'*** RICERCA MANUTENZIONI RRS

Partial Class RRS_RicercaRRS
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then


            sFiliale = "-1"
            sBP_Generale = "-1"

            If Session.Item("LIVELLO") <> "1" Then
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = Session.Item("ID_STRUTTURA")
                Else
                    sBP_Generale = Session.Item("ID_STRUTTURA")
                End If
            End If


            CaricaStrutture()

            'FiltraComplessi()
            'FiltraEdifici()
            'FiltraVociPF()
            'FiltraFornitori()
            'FiltraAppalti()

            CaricaStati()

            'Me.txtDataDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'Me.txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

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

    Public Property sBP_Generale() As String
        Get
            If Not (ViewState("par_sBP_Generale") Is Nothing) Then
                Return CStr(ViewState("par_sBP_Generale"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sBP_Generale") = value
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

            Me.cmbStruttura.Items.Clear()
            ' Me.cmbStruttura.Items.Add(New ListItem(" ", -1))
            If sFiliale <> "-1" Then
                par.cmd.CommandText = "select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                par.cmd.CommandText = "select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                   & " where  ID in (select distinct(ID_STRUTTURA) from SISCOM_MI.MANUTENZIONI  where ID_PF_VOCE_IMPORTO is null) " _
                                   & " order by NOME asc"
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))

            'End While
            'myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If sFiliale <> "-1" Then
                Me.cmbStruttura.SelectedValue = sFiliale
                Me.cmbStruttura.Enabled = False
                CaricaEsercizio()
            Else
                If sBP_Generale <> "-1" Then
                    Me.cmbStruttura.SelectedValue = sBP_Generale
                    CaricaEsercizio()
                Else
                    Me.cmbStruttura.SelectedValue = "-1"
                    CaricaEsercizio()
                End If
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO COMBO COMPLESSI
    Private Sub FiltraComplessi()
        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))
            Dim filtroPiano As String = ""
            If cmbEsercizio.SelectedValue <> "-1" Then
                filtroPiano = "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            End If
            par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                               & " where ID in (select ID_COMPLESSO from SISCOM_MI.MANUTENZIONI " _
                                            & " where ID_PF_VOCE_IMPORTO is null " _
                                            & filtroPiano
            If Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & ")"
            Else
                par.cmd.CommandText = par.cmd.CommandText & ")"
            End If
            par.cmd.CommandText = par.cmd.CommandText & " order by DENOMINAZIONE asc"


            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    ' Me.cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbComplesso, "ID", "DENOMINAZIONE", True)
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


            Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue _
                                   & " order by DENOMINAZIONE asc"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then
                Dim filtroPiano As String = ""
                If cmbEsercizio.SelectedValue <> "-1" Then
                    filtroPiano = "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                End If
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is null " _
                                                & filtroPiano _
                                                & "   and ID_STRUTTURA=" & cmbStruttura.SelectedValue & ")" _
                                   & " order by DENOMINAZIONE asc"
            Else
                Dim filtroPiano As String = ""
                If cmbEsercizio.SelectedValue <> "-1" Then
                    filtroPiano = "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ")"
                End If
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is null " _
                                            & filtroPiano & " ) " _
                                   & " order by DENOMINAZIONE asc"

            End If



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


            If cmbEdificio.Items.Count = 2 Then
                Me.cmbEdificio.Items(1).Selected = True
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraVociPF()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.cmbVocePF.Items.Clear()
            'Me.cmbVocePF.Items.Add(New ListItem(" ", -1))
            Dim filtroPiano As String = ""
            If cmbEsercizio.SelectedValue <> "-1" Then
                filtroPiano = "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            End If

            par.cmd.CommandText = "select ID,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.PF_VOCI " _
                                   & " where ID in (select ID_PF_VOCE from SISCOM_MI.MANUTENZIONI " _
                                                & " where  ID_PF_VOCE_IMPORTO is null " _
                                                & filtroPiano


            If Me.cmbEdificio.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & "   and  ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue
            ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & "   and  ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue
            End If

            If Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
            End If

            par.cmd.CommandText = par.cmd.CommandText & " ) order by DESCRIZIONE asc"
            par.caricaComboTelerik(par.cmd.CommandText, cmbVocePF, "ID", "DESCRIZIONE", True)

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbVocePF.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If cmbVocePF.Items.Count = 2 Then
                Me.cmbVocePF.Items(1).Selected = True
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

            Me.cmbFornitore.Items.Clear()
            '  Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            Dim filtroPiano As String = ""
            If cmbEsercizio.SelectedValue <> "-1" Then
                filtroPiano = "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            End If
            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                    & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                                               & "where FL_BLOCCATO=0 " _
                                               & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                               & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                                           & "  where  ID_PF_VOCE_IMPORTO is null " _
                                                                                           & filtroPiano



            If Me.cmbVocePF.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & "    and  ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue
            End If

            If Me.cmbStruttura.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
            End If

            par.cmd.CommandText = par.cmd.CommandText & " )) order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)
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

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If cmbFornitore.Items.Count = 2 Then
                Me.cmbFornitore.Items(1).Selected = True
            End If


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



            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            If Me.cmbFornitore.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select DISTINCT  ID_GRUPPO AS ID,TRIM(NUM_REPERTORIO) as NUM_REPERTORIO " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID_FORNITORE=" & Me.cmbFornitore.SelectedValue _
                                    & "   and ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is null "

                If Me.cmbVocePF.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue
                End If

                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                End If

                par.cmd.CommandText = par.cmd.CommandText & " ) order by NUM_REPERTORIO asc"

            Else
                Dim filtroPiano As String = ""
                If cmbEsercizio.SelectedValue <> "-1" Then
                    filtroPiano = "   AND ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
                End If
                par.cmd.CommandText = " select DISTINCT  ID_GRUPPO AS ID,TRIM(NUM_REPERTORIO) as NUM_REPERTORIO " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PF_VOCE_IMPORTO is null " _
                                                & filtroPiano


                If Me.cmbVocePF.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue
                End If


                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
                End If

                par.cmd.CommandText = par.cmd.CommandText & " ) order by NUM_REPERTORIO asc"
            End If

            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "NUM_REPERTORIO", True)

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If cmbAppalto.Items.Count = 2 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If



        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaVociPF()

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.cmbVocePF.Items.Clear()
            ' Me.cmbVocePF.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.PF_VOCI " _
                               & " where ID in (select ID_PF_VOCE from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is null) " _
                               & " order by DESCRIZIONE asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            par.caricaComboTelerik(par.cmd.CommandText, cmbVocePF, "ID", "DESCRIZIONE", True)
            'While myReader1.Read
            '    Me.cmbVocePF.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            Me.cmbVocePF.SelectedValue = "-1"
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

            Me.cmbFornitore.Items.Clear()
            'Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                    & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                                & "where FL_BLOCCATO=0 " _
                                & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                    & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_PF_VOCE_IMPORTO is null)) " _
                                & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

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
            par.caricaComboTelerik(par.cmd.CommandText, cmbFornitore, "ID", "DESCRIZIONE", True)
            Me.cmbFornitore.SelectedValue = "-1"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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

            par.cmd.CommandText = "select * from SISCOM_MI.TAB_STATI_ODL where ID<6 order by ID"
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    Me.cmbStato.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbStato, "ID", "DESCRIZIONE", True)
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
        FiltraComplessi()
        FiltraEdifici()
        FiltraVociPF()
        FiltraFornitori()
        FiltraAppalti()

    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        FiltraEdifici()
        FiltraVociPF()
        FiltraFornitori()
        FiltraAppalti()

    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged

        FiltraVociPF()
        FiltraFornitori()
        FiltraAppalti()
    End Sub

    Protected Sub cmbVocePF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVocePF.SelectedIndexChanged
        FiltraFornitori()
        FiltraAppalti()
    End Sub

    Protected Sub cmbFornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged
        FiltraAppalti()
    End Sub
 Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean

        Try

            ControlloCampi = True

            If Strings.Len(Me.txtDataAL.SelectedDate) > 0 Then
                If par.AggiustaData(Me.txtDataAL.SelectedDate) < par.AggiustaData(Me.txtDataDAL.SelectedDate) Then
                    RadWindowManager1.RadAlert("Attenzione...Controllare il range delle date!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Sub
                End If
            End If


            Dim dataDal As String = ""
            If Not IsNothing(txtDataDAL.SelectedDate) Then
                dataDal = txtDataDAL.SelectedDate
            End If
            Dim dataAl As String = ""
            If Not IsNothing(txtDataAL.SelectedDate) Then
                dataAl = txtDataAL.SelectedDate
            End If

            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiRRS.aspx?CO=" & Me.cmbComplesso.SelectedValue.ToString _
                                                                         & "&ED=" & Me.cmbEdificio.SelectedValue.ToString _
                                                                         & "&SV=" & Me.cmbVocePF.SelectedValue.ToString _
                                                                         & "&FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                         & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                         & "&DAL=" & par.IfEmpty(par.AggiustaData(dataDal), "") _
                                                                         & "&AL=" & par.IfEmpty(par.AggiustaData(dataAl), "") _
                                                                         & "&STR=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                         & "&ST=" & Me.cmbStato.SelectedValue.ToString _
                                                                         & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                         & "&AUT=" & Me.DropDownListAutorizzazione.SelectedValue.ToString _
                                                                         & "&PROVENIENZA=RICERCA_RRS" & "');</script>")
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

                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                    & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                                    & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                    & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                    & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_PF_VOCE_IMPORTO is  null " _
                                                                   & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & ")" _
                                    & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"

            Else
                par.cmd.CommandText = "Select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                    & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                    & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                                    & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                    & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                    & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_PF_VOCE_IMPORTO is  null)" _
                                    & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"
            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                '  Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", True)

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            'If i = 1 Then
            '    Me.cmbEsercizio.Items(0).Selected = True
            'ElseIf i = 0 Then
            '    Me.cmbEsercizio.Items.Clear()
            '    ' Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
            'End If
            'If Me.cmbEsercizio.Items.Count <= 1 Then
            '    Me.cmbEsercizio.Enabled = False
            'Else
            '    Me.cmbEsercizio.Enabled = True
            'End If

            If i > 0 Then
                'If ID_ANNO_EF_CORRENTE <> -1 Then
                '    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                'End If

                FiltraComplessi()
                FiltraEdifici()
                FiltraVociPF()
                FiltraFornitori()
                FiltraAppalti()
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

        FiltraComplessi()
        FiltraEdifici()
        FiltraVociPF()
        FiltraFornitori()
        FiltraAppalti()

    End Sub
    Protected Sub cmbStato_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStato.SelectedIndexChanged
        Me.DropDownListAutorizzazione.ClearSelection()
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
