'*** RICERCA MANUTENZIONI da emetterei i SAL

Partial Class MANUTENZIONI_RicercaSAL
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)

        If Not IsPostBack Then



            CaricaEsercizio()
            FiltraCodiceProgetto()
            FiltraNumeroSalVision()
            txtDataDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If

    End Sub


    Private Sub CaricaEsercizio()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            Dim query As String = "SELECT SISCOM_MI.PF_MAIN.ID, " _
                & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYMMDD'),'DD/MM/YYYY')||' - '||" _
                & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYMMDD'),'DD/MM/YYYY')||' ('|| " _
                & " SISCOM_MI.PF_STATI.DESCRIZIONE||')' AS DESCRIZIONE " _
                & " FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                & " WHERE SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                & " AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                & " AND SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID " _
                & " AND  SISCOM_MI.PF_MAIN.ID IN " _
                & " (SELECT DISTINCT(ID_PIANO_FINANZIARIO) " _
                & " FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=2 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=1 " _
                & " AND ID_PAGAMENTO IS NULL) " _
                & " AND ID_PAGAMENTO IS NULL " _
                & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") _
                & ") ORDER BY 1 DESC "
            par.caricaComboTelerik(query, cmbEsercizio, "ID", "DESCRIZIONE", False)
            If cmbEsercizio.SelectedValue <> "" And cmbEsercizio.SelectedValue <> "-1" Then

                FiltraFornitori()
                FiltraAppalti()
                FiltraServizioVoce()
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If


        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraFornitori()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioneAppalto As String = ""
            Dim condizioneCodiceProgettoVision As String = ""
            If cmbAppalto.SelectedValue <> "-1" And cmbAppalto.SelectedValue <> "" Then
                condizioneAppalto = " AND ID_APPALTO=" & cmbAppalto.SelectedValue
            End If


            If DropDownListProgettoVision.SelectedValue <> "-1" And DropDownListProgettoVision.SelectedValue <> "" Then
                condizioneCodiceProgettoVision = "AND FORNITORI.ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue.Replace("'", "''") & "')))"
            End If

            Dim query As String = " SELECT ID," _
                & " (CASE WHEN COD_FORNITORE||' - '||TRIM(RAGIONE_SOCIALE) IS NOT NULL THEN RAGIONE_SOCIALE ELSE COD_FORNITORE||' - '||TRIM(COGNOME)||' '||TRIM(NOME) END) AS DESCRIZIONE " _
                & " ,RAGIONE_SOCIALE,COGNOME,NOME " _
                & " FROM SISCOM_MI.FORNITORI " _
                & " WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI " _
                & " WHERE ID IN " _
                & " (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=2 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=1 " _
                & " AND ID_PAGAMENTO IS NULL) " _
                & condizioneAppalto _
                & ")) " _
                & condizioneCodiceProgettoVision _
                & " ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            par.caricaComboTelerik(query, cmbFornitore, "ID", "DESCRIZIONE", True, "-1", "")
            If cmbFornitore.Items.Count = 2 Then
                cmbFornitore.SelectedValue = cmbFornitore.Items(1).Value
            Else

                cmbFornitore.SelectedValue = "-1"
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraAppalti(Optional ByVal idAppaltoSelezionato As Integer = 0)

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If


            Dim condizioneFornitore As String = ""
            Dim condizioneCodiceProgettoVision As String = ""
            If cmbFornitore.SelectedValue <> "-1" And cmbFornitore.SelectedValue <> "" Then
                condizioneFornitore = " AND ID_FORNITORE=" & cmbFornitore.SelectedValue
            End If

            If DropDownListProgettoVision.SelectedValue <> "-1" And DropDownListProgettoVision.SelectedValue <> "" Then
                condizioneCodiceProgettoVision = "AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue.Replace("'", "''") & "'))"
            End If

            Dim query As String = " SELECT  SISCOM_MI.APPALTI.ID," _
                & " TRIM(SISCOM_MI.APPALTI.NUM_REPERTORIO) || ' - ' || TRIM(SISCOM_MI.APPALTI.DESCRIZIONE) AS DESCRIZIONE " _
                & ",NUM_REPERTORIO " _
                & " FROM SISCOM_MI.APPALTI " _
                & " WHERE ID IN (SELECT DISTINCT(ID_APPALTO) FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=2 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=1 " _
                & " AND ID_PAGAMENTO IS NULL) " _
                & condizioneFornitore _
                & ") " _
                & condizioneCodiceProgettoVision _
                & "ORDER BY NUM_REPERTORIO"
            par.caricaComboTelerik(query, cmbAppalto, "ID", "DESCRIZIONE", True, "-1", "")
            If cmbAppalto.Items.Count = 2 Then
                cmbAppalto.SelectedValue = cmbAppalto.Items(1).Value
            Else
                cmbAppalto.SelectedValue = "-1"
            End If
            If idAppaltoSelezionato > 0 Then
                cmbAppalto.SelectedValue = idAppaltoSelezionato
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraServizioVoce()

        Try

            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            Dim condizioneAppalto As String = ""
            If cmbAppalto.SelectedValue <> "-1" Then
                condizioneAppalto = " AND ID_APPALTO=" & cmbAppalto.SelectedValue
            End If

            Dim query As String = "SELECT ID," _
                & " TRIM(DESCRIZIONE) AS DESCRIZIONE " _
                & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                & " WHERE ID IN (SELECT DISTINCT(ID_PF_VOCE_IMPORTO) FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=2 " _
                & " AND ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID FROM SISCOM_MI.PRENOTAZIONI  " _
                & " WHERE  TIPO_PAGAMENTO=3 AND PRENOTAZIONI.ID_STATO>=1 " _
                & " AND ID_PAGAMENTO IS NULL) " _
                & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") _
                & condizioneAppalto _
                & ") " _
                & " ORDER BY DESCRIZIONE ASC"
            par.caricaComboTelerik(query, cmbServizioVoce, "ID", "DESCRIZIONE", True, "-1", "")
            If cmbServizioVoce.Items.Count = 2 Then
                cmbServizioVoce.SelectedValue = cmbServizioVoce.Items(1).Value

            Else
                cmbServizioVoce.SelectedValue = "-1"
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If

        Catch ex As Exception
            connData.chiudi()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged

        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If

            FiltraFornitori()
            FiltraAppalti()
            FiltraServizioVoce()
        FiltraCodiceProgetto()
        FiltraNumeroSalVision()
        If connAperta = True Then
            connData.chiudi(False)
        End If

    End Sub

    Protected Sub cmbFornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If


        FiltraAppalti()
        FiltraServizioVoce()
        If connAperta = True Then
            connData.chiudi(False)
        End If

    End Sub

    Protected Sub cmbAppalto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAppalto.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If

        FiltraFornitori()
        FiltraAppalti(cmbAppalto.SelectedValue)
        FiltraServizioVoce()
        If connAperta = True Then
            connData.chiudi(False)
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
                    Response.Write("<script>alert('Attenzione...Inserire un range di date corretto!');</script>")

                    ControlloCampi = False
                    Exit Sub
                End If
            End If


            If par.IfEmpty(Me.cmbEsercizio.Text, "Null") = "Null" Or Me.cmbEsercizio.Text = "-1" Then
                RadWindowManager1.RadAlert("E\' obbligatorio selezionare il piano finanziario!", 300, 150, "Attenzione", "", "null")

                ControlloCampi = False
                cmbEsercizio.Focus()
                Exit Sub
            End If

            If par.IfEmpty(Me.cmbFornitore.Text, "Null") = "Null" Or Me.cmbFornitore.Text = "-1" Then
                RadWindowManager1.RadAlert("E\'obbligatorio inserire il fornitore!", 300, 150, "Attenzione", "", "null")

                ControlloCampi = False
                cmbFornitore.Focus()
                Exit Sub
            End If


            If par.IfEmpty(Me.cmbAppalto.Text, "Null") = "Null" Or Me.cmbAppalto.Text = "-1" Then
                RadWindowManager1.RadAlert("E\' obbligatorio inserire il contratto di appalto!", 300, 150, "Attenzione", "", "null")

                ControlloCampi = False
                cmbAppalto.Focus()
                Exit Sub
            End If





            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiSAL.aspx?EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                          & "&FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                          & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                          & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                          & "&DAL=" & par.IfEmpty(dataDAL, "") _
                                                                          & "&AL=" & par.IfEmpty(dataAL, "") _
                                                                          & "&CPV=" & DropDownListProgettoVision.SelectedValue _
                                                                          & "&NSV=" & DropDownListNumeroSALVision.SelectedValue _
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
    Private Sub DropDownListProgettoVision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownListProgettoVision.SelectedIndexChanged
        Dim connAperta As Boolean = False
        If connData.Connessione.State = Data.ConnectionState.Closed Then
            connData.apri(False)
            connAperta = True
        End If
        par.caricaComboTelerik("select distinct numero_sal from siscom_mi.import_str where codice_progetto_vision='" & DropDownListProgettoVision.SelectedValue & "'", DropDownListNumeroSALVision, "numero_sal", "numero_sal", True, "-1", " ")
        FiltraFornitori()
        FiltraAppalti()
        FiltraServizioVoce()
        FiltraCodiceProgetto(DropDownListProgettoVision.SelectedValue)
        If connAperta = True Then
            connData.chiudi(False)
        End If
    End Sub
    Protected Sub DropDownListNumeroSALVision_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownListNumeroSALVision.SelectedIndexChanged
        FiltraFornitori()
        FiltraAppalti()
    End Sub
    Private Sub FiltraCodiceProgetto(Optional ByVal codiceProgetto As String = "")
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = "SELECT DISTINCT CODICE_PROGETTO_VISION " _
                & " FROM SISCOM_MI.IMPORT_STR " _
                & " WHERE CODICE_ODL IN (SELECT PROGR||'/'||ANNO " _
                & " FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE MANUTENZIONI.ID_PIANO_FINANZIARIO=" & cmbEsercizio.SelectedValue _
                & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
                & " AND STATO=2 " _
                & " AND ID_PAGAMENTO IS NULL " _
                & " AND ID_STRUTTURA=" & Session.Item("ID_STRUTTURA") _
                & ")"
            par.caricaComboTelerik(query, DropDownListProgettoVision, "CODICE_PROGETTO_VISION", "CODICE_PROGETTO_VISION", True, "-1", "")
            'If DropDownListProgettoVision.Items.Count = 2 Then
            '    DropDownListProgettoVision.SelectedValue = DropDownListProgettoVision.Items(1).Value
            'End If
            If codiceProgetto <> "" Then
                DropDownListProgettoVision.SelectedValue = codiceProgetto
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub FiltraNumeroSalVision()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = "SELECT DISTINCT NUMERO_SAL FROM SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & DropDownListProgettoVision.SelectedValue & "'"
            par.caricaComboTelerik(query, DropDownListNumeroSALVision, "NUMERO_SAL", "NUMERO_SAL", True, "-1", "")
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
