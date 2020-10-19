'*** RICERCA PAGAMENTI

Partial Class PAGAMENTI_RicercaPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String = "-1"
    Public sBP_Generale As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            If Session.Item("LIVELLO") <> "1" Then
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = Session.Item("ID_STRUTTURA")
                Else
                    sBP_Generale = Session.Item("ID_STRUTTURA")
                End If
            End If

            CaricaStrutture()

            'CaricaFornitori()
            CaricaStati()

            'Me.txtDataP1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'Me.txtDataP2.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            'Me.txtDataE1.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            'Me.txtDataE2.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

        End If

    End Sub



    'CARICO COMBO STRUTTURE (FILIARI)
    Private Sub CaricaStrutture()
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbStruttura.Items.Clear()
            'Me.cmbStruttura.Items.Add(New ListItem(" ", -1))


            If sFiliale <> "-1" Then
                par.cmd.CommandText = "select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                par.cmd.CommandText = "select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                   & " where  ID in (select ID_STRUTTURA from SISCOM_MI.ODL) " _
                                   & " order by NOME asc"
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbStruttura, "ID", "NOME", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
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
            '************CHIUSURA CONNESSIONE**********
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
        Dim FlagConnessione As Boolean = False
        Dim sStr1 As String = ""

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                 & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione " _
                               & " from SISCOM_MI.FORNITORI " _
                               & " where ID in (select ID_FORNITORE from SISCOM_MI.ODL "


            If Me.cmbStruttura.SelectedValue <> "-1" Then
                sStr1 = " where ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue
            End If

            If Me.cmbEsercizio.SelectedValue <> "-1" Then
                If sStr1 = "" Then
                    sStr1 = " where "
                Else
                    sStr1 = sStr1 & " and "
                End If
                sStr1 = sStr1 & " ID_VOCE_PF in ( select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "
            End If

            par.cmd.CommandText = par.cmd.CommandText & sStr1 & " ) order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
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

            Me.cmbFornitore.SelectedValue = "-1"


        Catch ex As Exception
            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    'CARICO COMBO STATI
    Private Sub CaricaStati()
        Dim FlagConnessione As Boolean = False

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbStato.Items.Clear()
            '   Me.cmbStato.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select * from SISCOM_MI.TAB_STATI_ODL where DESCRIZIONE<>'INTEGRATO' order by ID"
            par.caricaComboTelerik(par.cmd.CommandText, cmbStato, "ID", "DESCRIZIONE", True)

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    Me.cmbStato.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
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

            Me.cmbStato.SelectedValue = "-1"


        Catch ex As Exception
            '************CHIUSURA CONNESSIONE**********
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
            Dim DataP1 As String = ""
            If Not IsNothing(txtDataP1.SelectedDate) Then
                DataP1 = txtDataP1.SelectedDate
            End If
            Dim DataP2 As String = ""
            If Not IsNothing(txtDataP2.SelectedDate) Then
                DataP2 = txtDataP2.SelectedDate
            End If
            '& "&DALE=" & par.IfEmpty(par.AggiustaData(Me.txtDataE1.Text), "") & "&ALE=" & par.IfEmpty(par.AggiustaData(Me.txtDataE2.Text), "")
            Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & Me.cmbStato.SelectedValue.ToString _
                                                                          & "&FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                          & "&STR=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                          & "&DALP=" & par.IfEmpty(par.AggiustaData(DataP1), "") _
                                                                          & "&ALP=" & par.IfEmpty(par.AggiustaData(DataP2), "") _
                                                                          & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                          & "');</script>")

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


    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        'CaricaFornitori()
        CaricaEsercizio()
    End Sub



    'CARICO COMBO ESERCIZIO FINANZIARIO
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
                                    & " SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || " _
                                    & " SISCOM_MI.PF_STATI.DESCRIZIONE || ')'  as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                   & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                   & " where ID in (select distinct(ID_VOCE_PF) " _
                                                                                & " from SISCOM_MI.ODL " _
                                                                                & " where ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & ") ) order by 1 desc"

            Else
                par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                    & " SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || " _
                                    & " SISCOM_MI.PF_STATI.DESCRIZIONE || ')'  as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                   & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.PF_VOCI " _
                                                                   & " where ID in (select distinct(ID_VOCE_PF) " _
                                                                                & " from SISCOM_MI.ODL ) ) order by 1 desc"

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
                    Me.cmbEsercizio.Items.Add(New Telerik.Web.UI.RadComboBoxItem(" ", -1))
                    Me.cmbEsercizio.Enabled = False
                Case 1
                    Me.cmbEsercizio.Items(0).Selected = True
                    Me.cmbEsercizio.Enabled = False

                    CaricaFornitori()
                Case Else

                    If ID_ANNO_EF_CORRENTE <> -1 Then
                        Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                    End If
                    CaricaFornitori()
                    Me.cmbEsercizio.Enabled = True
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
    End Sub

   
End Class
