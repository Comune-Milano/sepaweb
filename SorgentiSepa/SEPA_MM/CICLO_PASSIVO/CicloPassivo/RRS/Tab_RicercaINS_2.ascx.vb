' TAB RICERCA RRS (COMPLESSO o EDIFICIO -VOCE-APPALTO)

Imports Telerik.Web.UI

Partial Class Tab_RicercaINS_2
    Inherits UserControlSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            CaricaEsercizio()
        End If
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub



    'Protected Sub BtnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnCerca.Click
    '    Dim FlagConnessione As Boolean
    '    Dim i As Integer

    '    Try

    '        FlagConnessione = False
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            FlagConnessione = True
    '        End If

    '        If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then

    '            If RBL1.SelectedIndex = 0 Then
    '                'COMPLESSO 
    '                par.cmd.CommandText = "select DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
    '                          & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
    '                          & "   and ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                       & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI )" _
    '                                       & "   and ID>1)" _
    '                          & " order by DESCRIZIONE asc"


    '                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                While myReader1.Read
    '                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
    '                End While

    '            Else
    '                'EDIFICI 
    '                par.cmd.CommandText = "select DISTINCT DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
    '                          & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
    '                          & "   and ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI where ID>1) " _
    '                          & " order by DESCRIZIONE asc"

    '                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                i = 1
    '                While myReader1.Read
    '                    ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), i))
    '                    i = i + 1
    '                End While

    '                myReader1.Close()
    '            End If
    '        End If


    '        'If ListEdifci.Items.Count = 0 Then
    '        '    Me.LblNoResult.Visible = True
    '        'Else
    '        '    Me.LblNoResult.Visible = False
    '        'End If

    '        '*********************CHIUSURA CONNESSIONE**********************
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Me.TextBox1.Value = 2

    '    Catch ex As Exception

    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try

    'End Sub

    'Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click

    '    Try
    '        If Me.ListEdifci.SelectedValue.ToString <> "" Then
    '            If RBL1.SelectedIndex = 0 Then
    '                Me.cmbIndirizzo.SelectedValue = Me.ListEdifci.SelectedValue.ToString
    '            Else
    '                Me.cmbIndirizzo.SelectedValue = Me.ListEdifci.SelectedItem.Text
    '            End If

    '            Me.TxtDescInd.Text = ""
    '            Me.ListEdifci.Items.Clear()
    '            Me.TextBox1.Value = 1
    '            Me.LblNoResult.Visible = False
    '        Else
    '            Me.TxtDescInd.Text = ""
    '            Me.ListEdifci.Items.Clear()
    '            Me.LblNoResult.Visible = False
    '            Me.TextBox1.Value = 1
    '        End If


    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message
    '    End Try
    'End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged

        Me.TextBox1.Value = ""
        'Me.TxtDescInd.Text = ""
        'Me.ListEdifci.Items.Clear()

    End Sub




    Private Sub FiltraAppalti()
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            If Me.cmbVoce.SelectedValue <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                End If

                par.cmd.CommandText = " select DISTINCT  ID,NUM_REPERTORIO || ' - ' || " _
                                    & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = APPALTI.ID_FORNITORE) AS NUM_REPERTORIO " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_VOCI_PF where ID_PF_VOCE=" & Me.cmbVoce.SelectedValue & ")" _
                                    & "   And ID_STATO=1" _
                                    & "   And TIPO='N'" _
                                    & sFiliale _
                                    & " order by NUM_REPERTORIO "

                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    i = i + 1
                '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " "), par.IfNull(myReader1("ID"), -1)))
                'End While

                'myReader1.Close()
                par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "NUM_REPERTORIO", True)
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                If cmbAppalto.Items.Count = 2 Then
                    Me.cmbAppalto.Items(1).Selected = True
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

    Protected Sub RBL1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL1.SelectedIndexChanged
        CaricaIndirizzi()
        Me.TextBox1.Value = ""
        'Me.TxtDescInd.Text = ""
        'Me.ListEdifci.Items.Clear()
    End Sub


    'CARICO COMBO INDIRIZZI del TAB. RICERCA 2
    Private Sub CaricaIndirizzi()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New RadComboBoxItem(" ", -1))

            If RBL1.SelectedIndex = 0 Then
                par.cmd.CommandText = "select MAX(ID) AS ID,trim(DESCRIZIONE) as DESCRIZIONE  from SISCOM_MI.INDIRIZZI " _
                                  & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                    & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI )" _
                                                    & "   and ID>1) " _
                                  & " GROUP BY DESCRIZIONE order by DESCRIZIONE asc"

                'par.caricaComboTelerik(par.cmd.CommandText, cmbIndirizzo, "ID", "DESCRIZIONE", True)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.cmbIndirizzo.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), "-1")))
                End While
                myReader1.Close()
            Else
                par.cmd.CommandText = "select distinct DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                   & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI where ID>1)" _
                                   & " order by DESCRIZIONE asc"
                'par.caricaComboTelerik(par.cmd.CommandText, cmbIndirizzo, "ID", "DESCRIZIONE", True)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.cmbIndirizzo.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("DESCRIZIONE"), "-1")))
                End While
                myReader1.Close()
            End If
            Me.cmbIndirizzo.SelectedValue = "-1"
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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


    'CARICO COMBO PF_VOCI
    Private Sub CaricaVoci()
        Dim FlagConnessione As Boolean


        Try
            ' APRO CONNESSIONE
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If


            Me.cmbVoce.Items.Clear()
            ' Me.cmbVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI.DESCRIZIONE) as DESCRIZIONE " _
                               & " from SISCOM_MI.PF_VOCI " _
                               & " where ID in (select ID_PF_VOCE from SISCOM_MI.APPALTI_VOCI_PF " _
                                            & " where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='N' " & sFiliale & ")) " _
                              & "    and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "


            Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                Case 6
                    If Session.Item("FL_COMI") <> 1 Then
                        par.cmd.CommandText = par.cmd.CommandText & " and FL_CC=1  "
                    End If
                Case 7
                    par.cmd.CommandText = par.cmd.CommandText & " and FL_CC=1  "

            End Select

            par.cmd.CommandText = par.cmd.CommandText & " order by DESCRIZIONE asc"

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbVoce, "ID", "DESCRIZIONE", True)
            Me.cmbVoce.SelectedValue = "-1"
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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


    Protected Sub cmbVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVoce.SelectedIndexChanged
        FiltraAppalti()
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
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                                & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID ORDER BY SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID DESC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If
                '  Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", False)
            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                ' Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If
            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)

                CaricaVoci()
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
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        CaricaVoci()

    End Sub

    'RICAVA LO STATO DELL'ESERCIZIO SELEZIONATO (5,6,7)
    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try
            Me.txtSTATO_PF.Value = -1

            If ID_ESERCIZIO < 0 Then Exit Sub

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                Me.txtSTATO_PF.Value = par.IfNull(myReaderF("ID_STATO"), -1)
            End If
            myReaderF.Close()

            par.cmd.Parameters.Clear()

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


End Class
