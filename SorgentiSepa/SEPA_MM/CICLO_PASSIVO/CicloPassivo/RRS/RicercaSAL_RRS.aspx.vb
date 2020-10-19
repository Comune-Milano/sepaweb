'*** RICERCA MANUTENZIONI RRS da emetterei i SAL

Partial Class RRS_RicercaSAL_RRS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sOpz1 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            '    Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            CaricaEsercizio()

            'CaricaFornitori()
            'CaricaAppalti()

            'Me.txtDataDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            ' Me.txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If

    End Sub



    'CARICO COMBO ESERCIZI FINANZIARI
    Private Sub CaricaEsercizio()
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                RadWindowManager1.RadAlert("Impossibile visualizzare", 300, 150, "Attenzione", "", "null")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If


            sOpz1 = " and ID_PAGAMENTO is null "

            par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE || ')' as STATO " _
                                & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                                & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                                & "   and SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                              & " from SISCOM_MI.MANUTENZIONI " _
                                                              & " where ID_PF_VOCE_IMPORTO is null and STATO=2 "

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and SISCOM_MI.MANUTENZIONI." & sFiliale & sOpz1 & ") order by 1 desc"
            Else
                par.cmd.CommandText = par.cmd.CommandText & sOpz1 & ") order by 1 desc"
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
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO", False)
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            'If i = 1 Then
            '    Me.cmbEsercizio.Items(0).Selected = True
            '    Me.cmbEsercizio.Enabled = False
            'ElseIf i = 0 Then
            '    Me.cmbEsercizio.Items.Clear()
            '    'Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
            '    Me.cmbEsercizio.Enabled = False
            'End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                FiltraFornitori()
                FiltraAppalti()
                FiltraServizioVoce()
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try



    End Sub


    Private Sub FiltraAppalti()
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            sOpz1 = " and ID_PAGAMENTO is null "

            If Me.cmbFornitore.SelectedValue = "-1" Then
                par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,trim(SISCOM_MI.APPALTI.NUM_REPERTORIO) || ' - ' || trim(SISCOM_MI.APPALTI.DESCRIZIONE) as DESCRIZIONE " _
                                    & " from SISCOM_MI.APPALTI " _
                                    & " where ID in (select distinct(ID_APPALTO) from SISCOM_MI.MANUTENZIONI " _
                                                 & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue _
                                                 & "   and ID_PF_VOCE_IMPORTO is null " _
                                                 & "   and STATO=2 " & sOpz1 & sFiliale & ") "
            Else

                par.cmd.CommandText = "select SISCOM_MI.APPALTI.ID,trim(SISCOM_MI.APPALTI.NUM_REPERTORIO) || ' - ' || trim(SISCOM_MI.APPALTI.DESCRIZIONE) as DESCRIZIONE " _
                                    & "from  SISCOM_MI.APPALTI " _
                                    & "where SISCOM_MI.APPALTI.ID_FORNITORE= " & Me.cmbFornitore.SelectedValue.ToString _
                                    & " and  ID in (select distinct(ID_APPALTO) from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue _
                                                 & "  and ID_PF_VOCE_IMPORTO is null " _
                                                 & "  and STATO=2 " & sOpz1 & sFiliale & ") "
            End If

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    '  Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "DESCRIZIONE", True)

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


    Private Sub FiltraFornitori()
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione from SISCOM_MI.FORNITORI " _
                                & "where ID in (select distinct(ID_FORNITORE) from SISCOM_MI.APPALTI " _
                                & " where ID in (select distinct(ID_APPALTO) from SISCOM_MI.MANUTENZIONI " _
                                                          & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue _
                                                          & "  and ID_PF_VOCE_IMPORTO is null " _
                                                          & "  and STATO=2 " _
                                                          & "  and ID_PAGAMENTO is null " & sFiliale

            If par.IfEmpty(Me.cmbAppalto.SelectedValue.ToString, "-1") <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_APPALTO=" & Me.cmbAppalto.SelectedValue
            End If

            par.cmd.CommandText = par.cmd.CommandText & " )) order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
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

            '    i = i + 1
            'End While
            'myReader1.Close()
            '**************************

            If cmbFornitore.Items.Count = 2 Then
                Me.cmbFornitore.Items(1).Selected = True
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraServizioVoce()
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbServizioVoce.Items.Clear()
            ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))


            sOpz1 = " and ID_PAGAMENTO is null "

            'ID_PF_VOCE_IMPORTO
            If Me.cmbAppalto.SelectedValue = "-1" Then
                par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.PF_VOCI " _
                                    & "where ID in (select distinct(ID_PF_VOCE) from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue _
                                                & "   and ID_PF_VOCE_IMPORTO is null " _
                                                & "   and STATO=2 " & sOpz1 & sFiliale & ") " _
                                    & " order by DESCRIZIONE ASC"

            Else
                par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.PF_VOCI " _
                                    & "where ID in (select distinct(ID_PF_VOCE) from SISCOM_MI.MANUTENZIONI " _
                                                & " where ID_PIANO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue _
                                                 & "  and ID_APPALTO= " & Me.cmbAppalto.SelectedValue.ToString _
                                                 & "  and ID_PF_VOCE_IMPORTO is null " _
                                                 & "  and STATO=2 " & sOpz1 & sFiliale & ") " _
                                    & " order by DESCRIZIONE ASC"
            End If

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()
            '**************************
            par.caricaComboTelerik(par.cmd.CommandText, cmbServizioVoce, "ID", "DESCRIZIONE", True)
            If cmbServizioVoce.Items.Count = 2 Then
                Me.cmbServizioVoce.Items(1).Selected = True
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged

        If Me.cmbEsercizio.SelectedValue = "-1" Then
            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            '     Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

        Else
            Me.cmbFornitore.Items.Clear()
            ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            FiltraFornitori()
            FiltraAppalti()
            FiltraServizioVoce()
        End If

    End Sub

    Protected Sub cmbFornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged

        Me.cmbAppalto.Items.Clear()
        ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

        Me.cmbServizioVoce.Items.Clear()
        ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))


        FiltraAppalti()
        FiltraServizioVoce()

    End Sub

    Protected Sub cmbAppalto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAppalto.SelectedIndexChanged
        Me.cmbFornitore.Items.Clear()
        ' Me.cmbFornitore.Items.Add(New ListItem(" ", -1))


        Me.cmbServizioVoce.Items.Clear()
        ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

        FiltraFornitori()
        FiltraServizioVoce()

    End Sub


    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function



    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean

        Try
            'Response.Write("<script>parent.main.location.replace('CicloPassivo/MANUTENZIONI/RicercaPagamentiAnnulla.aspx');</script>")


            ControlloCampi = True
            Dim dataDAL As String = ""
            If Not IsNothing(txtDataDAL.SelectedDate) Then
                dataDAL = txtDataDAL.SelectedDate
            End If
            Dim dataAL As String = ""
            If Not IsNothing(txtDataAl.SelectedDate) Then
                dataAL = txtDataAl.SelectedDate
            End If
            If Strings.Len(Me.txtDataAl.SelectedDate) > 0 Then
                If par.AggiustaData(Me.txtDataAl.SelectedDate) < par.AggiustaData(Me.txtDataDAL.SelectedDate) Then

                    RadWindowManager1.RadAlert("Attenzione...Controllare il range delle date!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Sub
                End If
            End If


            If par.IfEmpty(Me.cmbEsercizio.Text, "Null") = "Null" Or Me.cmbEsercizio.Text = "-1" Then
                RadWindowManager1.RadAlert("Inserire l\'esercizio finanziario!", 300, 150, "Attenzione", "", "null")
                ControlloCampi = False
                cmbEsercizio.Focus()
                Exit Sub
            End If

            If par.IfEmpty(Me.cmbFornitore.Text, "Null") = "Null" Or Me.cmbFornitore.Text = "-1" Then
                RadWindowManager1.RadAlert("Inserire il fornitore!", 300, 150, "Attenzione", "", "null")
                ControlloCampi = False
                cmbFornitore.Focus()
                Exit Sub
            End If

            If par.IfEmpty(Me.cmbAppalto.Text, "Null") = "Null" Or Me.cmbAppalto.Text = "-1" Then
                RadWindowManager1.RadAlert("Inserire l\'appalto!", 300, 150, "Attenzione", "", "null")
                ControlloCampi = False
                cmbAppalto.Focus()
                Exit Sub
            End If



            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiSAL_RRS.aspx?EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                          & "&FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                          & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                          & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                          & "&DAL=" & par.IfEmpty(par.AggiustaData(dataDAL), "") _
                                                                          & "&AL=" & par.IfEmpty(par.AggiustaData(dataAL), "") _
                                                                          & "&ORD=PAGAMENTI');</script>")
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub
End Class
