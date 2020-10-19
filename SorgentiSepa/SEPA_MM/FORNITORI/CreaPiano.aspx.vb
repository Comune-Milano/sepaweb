
Partial Class FORNITORI_CreaPiano
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            connData.apri()
            caricaProfilo()
            caricaFornitore()
            CaricaCronoprogramma()
            CaricaAttivitaCronoProgramma()
            caricaAppalti()
            connData.chiudi()
        End If
    End Sub
    Private Sub caricaProfilo()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or Session.Item("FL_SUPERDIRETTORE") = "1" Then
                'DIRETTORE LAVORI
                gestioneDirettoreLavori()
            End If
            If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                'FORNITORE ESTERNO
                gestioneFornitoreEsterno()
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CreaPiano - caricaProfilo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub gestioneFornitoreEsterno()
        par.cmd.CommandText = "SELECT MOD_FO_ID_FO FROM SEPA.OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE")
        Dim idOperatore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        idFornitore.Value = idOperatore
        idDirettoreLavori.Value = 0
    End Sub

    Private Sub gestioneDirettoreLavori()
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        idDirettoreLavori.Value = idOperatore
        idFornitore.Value = 0
    End Sub
    Protected Sub btnCrea_Click(sender As Object, e As System.EventArgs) Handles btnCrea.Click
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If controlloDati() Then
                If controlloDate() Then
                    If ControlloPeriodo(par.AggiustaData(txtPeriodoCronoprogrammaInizio.SelectedDate), par.AggiustaData(txtPeriodoCronoprogrammaFine.SelectedDate)) Then
                        Dim idPiano As Integer = 0
                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PROGRAMMA_ATTIVITA.NEXTVAL FROM DUAL"
                        idPiano = par.IfNull(par.cmd.ExecuteScalar, 0)
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PROGRAMMA_ATTIVITA(ID,ID_FORNITORE,ID_GRUPPO,DATA_INSERIMENTO,ID_STATO, ID_TIPO_CRONOPROGRAMMA," _
                                            & " DATA_INIZIO, DATA_FINE, ATTIVITA_CRONOPROGRAMMA) " _
                                            & "VALUES(" & idPiano & "," & cmbFornitore.SelectedValue & "," & cmbAppalto.SelectedValue & ",'" & Format(Now, "yyyyMMdd") & "',0, " & cmbTipoCronoprogramma.SelectedValue & "," _
                                            & par.AggiustaData(txtPeriodoCronoprogrammaInizio.SelectedDate) & "," & par.AggiustaData(txtPeriodoCronoprogrammaFine.SelectedDate) & "," & cmbAttivitaCronoprogramma.SelectedValue & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PROGRAMMA_aTTIVITA(ID_PROGRAMMA_aTTIVITA,ID_OPERATORE,DATA_ORA,COD_eVENTO,MOTIVAZIONE)" _
                            & "VALUES(" & idPiano & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F312','Creazione piano per il programma delle attività')"
                        par.cmd.ExecuteNonQuery()
                        RadWindowManager1.RadAlert("Cronoprogramma inserito correttamente!", 350, 150, "Successo", "Ricarica", Nothing)
                    Else
                        RadWindowManager1.RadAlert("Risulta già inserito un cronoprogramma sull\' appalto <strong>" & cmbAppalto.SelectedItem.Text & "</strong> per il periodo selezionato!", 350, 150, "Attenzione", Nothing, Nothing)
                    End If
                Else
                    RadWindowManager1.RadAlert("Valorizzare correttamente le date!", 350, 150, "Attenzione", Nothing, Nothing)
                End If
            Else
                RadWindowManager1.RadAlert("Valorizzare correttamente i campi obbligatori!", 350, 150, "Attenzione", Nothing, Nothing)
            End If

            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CreaPiano - btnCrea_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function controlloDati() As Boolean
        controlloDati = False
        If cmbAppalto.SelectedValue > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function controlloDate() As Boolean
        If Not IsDate(Me.txtPeriodoCronoprogrammaInizio.SelectedDate) Or Not IsDate(Me.txtPeriodoCronoprogrammaFine.SelectedDate) Then
            controlloDate = False
        ElseIf CInt(par.AggiustaData(txtPeriodoCronoprogrammaFine.SelectedDate) - par.AggiustaData(txtPeriodoCronoprogrammaInizio.SelectedDate)) >= 0 Then
            controlloDate = True
        Else
            controlloDate = False
        End If
    End Function

    Private Function ControlloPeriodo(ByVal dataInizio As String, ByVal datafine As String) As Boolean
        par.cmd.CommandText = "SELECT COUNT (*) " _
                    & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA " _
                    & " WHERE  ID_TIPO_CRONOPROGRAMMA =  " & cmbTipoCronoprogramma.SelectedValue _
                    & " AND ATTIVITA_CRONOPROGRAMMA = " & cmbAttivitaCronoprogramma.SelectedValue _
                    & " AND ID_GRUPPO =  " & cmbAppalto.SelectedValue _
                    & " AND( ((" & dataInizio & " >= DATA_INIZIO " _
                    & " AND " & dataInizio & "<=DATA_FINE) OR (" & datafine & ">=DATA_INIZIO AND " & datafine & "<=DATA_FINE)) " _
                    & " OR( " & dataInizio & " <= DATA_INIZIO " _
                    & " AND " & datafine & " >= DATA_FINE)) "
        Dim numero As Integer = par.cmd.ExecuteScalar
        If numero > 0 Then
            ControlloPeriodo = False
        Else
            ControlloPeriodo = True
        End If
    End Function

    Private Sub caricaFornitore()
        par.caricaComboTelerik("Select ID,RAGIONE_SOCIALE " _
            & " FROM SISCOM_MI.FORNITORI " _
            & " WHERE ID=" & idFornitore.Value, cmbFornitore, "ID", "RAGIONE_SOCIALE", False)
    End Sub

    Private Sub caricaAppalti()
        par.caricaComboTelerik("Select DISTINCT ID_GRUPPO As ID,NUM_REPERTORIO||'-'||DESCRIZIONE AS DESCRIZIONE " _
            & " FROM SISCOM_MI.APPALTI " _
            & " WHERE ID_FORNITORE=" & idFornitore.Value _
            & " AND ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & ") ORDER BY 2 DESC", cmbAppalto, "ID", "DESCRIZIONE", True)
    End Sub

    Private Sub CaricaCronoprogramma()

        par.caricaComboTelerik("SELECT ID, DESCRIZIONE " _
            & " FROM SISCOM_MI.TAB_TIPOLOGIA_CRONOPROGRAMMA " _
            & " ORDER BY DESCRIZIONE ASC", cmbTipoCronoprogramma, "ID", "DESCRIZIONE", False)
    End Sub

    Private Sub CaricaAttivitaCronoProgramma()
        par.caricaComboTelerik("SELECT ID, REPLACE(DESCRIZIONE,'#','') AS DESCRIZIONE " _
                            & " FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1  " _
                            & " WHERE ID IN  " _
                            & " (SELECT ID_TIPO_SEGNALAZIONE_LIVELLO_1 FROM SISCOM_MI.COMBINAZIONE_TIPOLOGIE WHERE ID_TIPO_MANUTENZIONE = 1)  " _
                            & " ORDER BY DESCRIZIONE ASC", cmbAttivitaCronoprogramma, "ID", "DESCRIZIONE", False)
    End Sub

End Class
