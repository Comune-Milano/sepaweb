Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_ModificaManualeCaratura
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                CType(Master.FindControl("TitoloMaster"), Label).Text = "CDR - Modifica selettiva"
                CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
                caricaListaComplessi()
                caricaListaTipologie()
                caricaListaEdifici()
                caricaListaIndirizzi()
                caricaListaAscensori()
                caricaListaCivicoInternoPiano()
            End If
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()

    End Sub
    Private Sub caricaListaComplessi()
        Try
            connData.apri()
            par.caricaComboTelerik("SELECT COMPLESSI_IMMOBILIARI.ID,DENOMINAZIONE || ' - cod.' || COMPLESSI_IMMOBILIARI.COD_COMPLESSO AS DESCRIZIONE" _
                            & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI " _
                            & " WHERE  COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO=INDIRIZZI.ID " _
                            & " AND COMPLESSI_IMMOBILIARI.ID<>1 AND COMPLESSI_IMMOBILIARI.ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE FL_PREVENTIVI=1) " _
                            & " ORDER BY DENOMINAZIONE ASC", DrLComplesso, "ID", "DESCRIZIONE", True, , "")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Caricamento complessi - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante il caricamento dei complessi!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub caricaListaTipologie()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT COD, DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI ORDER BY DESCRIZIONE ASC", chkListTipologie, "COD", "DESCRIZIONE")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Caricamento tipologia - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante il caricamento delle tipologie!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub caricaListaEdifici()
        Try
            connData.apri()
            Dim condizioneComplesso As String = ""
            If DrLComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " and complessi_immobiliari.id='" & DrLComplesso.SelectedValue & "' "
            End If
            par.caricaComboTelerik("SELECT DISTINCT EDIFICI.ID," _
                  & " EDIFICI.DENOMINAZIONE||' - '||EDIFICI.COD_EDIFICIO AS DENOMINAZIONE " _
                  & " FROM SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                  & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                  & " AND EDIFICI.ID<>1 AND FL_PREVENTIVI=1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE ASC",
                  cmbEdificio, "ID", "DENOMINAZIONE", True, , "")
            If cmbEdificio.Items.Count = 2 Then
                cmbEdificio.Items.Remove(cmbEdificio.Items.FindItemByValue("-1"))
                cmbEdificio.Enabled = False
            ElseIf cmbEdificio.Items.Count = 1 Then
                cmbEdificio.Enabled = False
            Else
                cmbEdificio.Enabled = True
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Caricamento edifici - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub caricaListaIndirizzi()
        Try
            connData.apri()
            Dim condizioneEdifici As String = ""
            Dim condizioneComplessi As String = ""
            If DrLComplesso.SelectedValue <> "-1" Then
                condizioneComplessi = " and id_edificio in (select id from SISCOM_MI.edifici where id_complesso='" & DrLComplesso.SelectedValue & "') "
            End If
            If cmbEdificio.SelectedValue <> "-1" Then
                condizioneEdifici = " and id_edificio='" & cmbEdificio.SelectedValue & "' "
            End If
            par.caricaComboTelerik("SELECT distinct descrizione FROM SISCOM_MI.indirizzi " _
                & " WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO " _
                & " FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                & " where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1 " _
                & condizioneEdifici & condizioneComplessi & ")  order by descrizione asc", cmbIndirizzo, "DESCRIZIONE", "DESCRIZIONE", True, , "")
            If cmbIndirizzo.Items.Count = 2 Then
                cmbIndirizzo.Items.Remove(cmbIndirizzo.Items.FindItemByValue("-1"))
                cmbIndirizzo.Enabled = False
            ElseIf cmbIndirizzo.Items.Count = 1 Then
                cmbIndirizzo.Enabled = False
            Else
                cmbIndirizzo.Enabled = True
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Caricamento indirizzi - " & ex.Message)
            RadWindowManager1.RadAlert("Errore durante il caricamento degli indirizzi!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub caricaListaAscensori()
        cmbAscensore.Items.Clear()
        cmbAscensore.Items.Add(New RadComboBoxItem("", "-1"))
        cmbAscensore.Items.Add(New RadComboBoxItem("NO", 0))
        cmbAscensore.Items.Add(New RadComboBoxItem("SI", 1))
        cmbAscensore.SelectedValue = "-1"
    End Sub
    Private Sub caricaListaCivicoInternoPiano()
        If cmbIndirizzo.SelectedValue <> "-1" Then
            Try
                connData.apri()
                Dim condizioneEdifici As String = ""
                If cmbEdificio.SelectedValue <> "-1" Then
                    condizioneEdifici = " and id_Edificio='" & cmbEdificio.SelectedValue & "' "
                End If
                par.caricaComboTelerik("SELECT DISTINCT CIVICO FROM SISCOM_MI.INDIRIZZI " _
                    & " WHERE DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' " _
                    & " AND ID IN ( SELECT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                    & " WHERE ID_EDIFICIO <> 1 " & condizioneEdifici & " ) ORDER BY CIVICO ASC", cmbCivico, "CIVICO", "CIVICO", True, , "")
                Dim myreader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If cmbCivico.Items.Count = 2 Then
                    cmbCivico.Items.Remove(cmbCivico.Items.FindItemByValue("-1"))
                    cmbCivico.Enabled = False
                ElseIf cmbCivico.Items.Count = 1 Then
                    cmbCivico.Enabled = False
                Else
                    cmbCivico.Enabled = True
                End If
                '********************SCALA**********************
                If cmbCivico.SelectedValue <> "" Then
                    par.caricaComboTelerik("SELECT SCALE_EDIFICI.ID,DESCRIZIONE " _
                        & "FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN " _
                        & "(SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                        & "WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in " _
                        & "(select id from SISCOM_MI.indirizzi " _
                        & "where civico='" & cmbCivico.SelectedValue & "' " _
                        & "and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')" _
                        & condizioneEdifici & " AND ID_EDIFICIO<>1) order by descrizione asc", cmbScala, "ID", "DESCRIZIONE", True, , "")
                    If cmbScala.Items.Count = 2 Then
                        cmbScala.Items.Remove(cmbScala.Items.FindItemByValue("-1"))
                        cmbScala.Enabled = False
                    ElseIf cmbScala.Items.Count = 1 Then
                        cmbScala.Enabled = False
                    Else
                        cmbScala.Enabled = True
                    End If
                Else
                    cmbScala.Items.Clear()
                    cmbScala.Items.Add(New RadComboBoxItem("", "-1"))
                    cmbScala.Enabled = False
                End If
                '********************INTERNO**********************
                If cmbScala.SelectedValue <> "" Then
                    par.caricaComboTelerik("SELECT DISTINCT UNITA_IMMOBILIARI.INTERNO " _
                        & "FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                        & "WHERE ID_SCALA = " & cmbScala.SelectedValue & " ORDER BY UNITA_IMMOBILIARI.INTERNO ASC", cmbInterno, "INTERNO", "INTERNO", True, , "")
                    If cmbInterno.Items.Count = 2 Then
                        cmbInterno.Items.Remove(cmbInterno.Items.FindItemByValue("-1"))
                        cmbInterno.Enabled = False
                    ElseIf cmbInterno.Items.Count = 1 Then
                        cmbInterno.Enabled = False
                    Else
                        cmbInterno.Enabled = True
                    End If
                Else
                    cmbInterno.Items.Clear()
                    cmbInterno.Items.Add(New RadComboBoxItem("", "-1"))
                    cmbInterno.Enabled = False
                End If
                connData.chiudi()
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza: Ricerca preventivi - " & ex.Message)
                RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento civico, interno e piano!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
            End Try
        Else
            cmbCivico.Items.Clear()
            cmbCivico.Items.Add(New RadComboBoxItem("", "-1"))
            cmbCivico.Enabled = False
            cmbScala.Items.Clear()
            cmbScala.Items.Add(New RadComboBoxItem("", "-1"))
            cmbScala.Enabled = False
            cmbInterno.Items.Clear()
            cmbInterno.Items.Add(New RadComboBoxItem("", "-1"))
            cmbInterno.Enabled = False
        End If
    End Sub
    Protected Sub DaRicerca(edificio As String)
        caricaListaComplessi()
        caricaListaEdifici()
        cmbEdificio.SelectedValue = edificio
        caricaListaIndirizzi()
        caricaListaCivicoInternoPiano()
        caricaListaAscensori()
    End Sub
    Protected Sub cmbCivico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCivico.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                connData.apri()
                If cmbCivico.Text <> "" Then
                    If cmbCivico.SelectedValue <> "" Then
                        par.caricaComboTelerik("SELECT SCALE_EDIFICI.ID,DESCRIZIONE " _
                            & "FROM SISCOM_MI.SCALE_EDIFICI " _
                            & "WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA " _
                            & "FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                            & "WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO IN " _
                            & "(SELECT ID FROM SISCOM_MI.INDIRIZZI " _
                            & "WHERE CIVICO='" & cmbCivico.SelectedValue & "' " _
                            & "AND DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')) " _
                            & "ORDER BY DESCRIZIONE ASC", cmbScala, "ID", "DESCRIZIONE", True, , "")
                        If cmbScala.Items.Count = 2 Then
                            cmbScala.Items.Remove(cmbScala.Items.FindItemByValue("-1"))
                            cmbScala.Enabled = False
                        ElseIf cmbScala.Items.Count = 1 Then
                            cmbScala.Enabled = False
                        Else
                            cmbScala.Enabled = True
                        End If
                    Else
                        cmbScala.Items.Clear()
                        cmbScala.Items.Add(New RadComboBoxItem("", "-1"))
                    End If
                End If
                If cmbCivico.SelectedValue <> "" Then
                    par.caricaComboTelerik("SELECT DISTINCT UNITA_IMMOBILIARI.INTERNO " _
                        & "FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                        & "WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO IN " _
                        & "(SELECT ID FROM SISCOM_MI.INDIRIZZI " _
                        & "WHERE CIVICO='" & cmbCivico.SelectedValue & "' " _
                        & "AND DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') " _
                        & "ORDER BY INTERNO ASC", cmbInterno, "INTERNO", "INTERNO", True, , "")
                    If cmbInterno.Items.Count = 2 Then
                        cmbInterno.Items.Remove(cmbInterno.Items.FindItemByValue("-1"))
                        cmbInterno.Enabled = False
                    ElseIf cmbInterno.Items.Count = 1 Then
                        cmbInterno.Enabled = False
                    Else
                        cmbInterno.Enabled = True
                    End If
                Else
                    cmbInterno.Items.Clear()
                    cmbInterno.Items.Add(New RadComboBoxItem("", "-1"))
                End If
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Ricerca preventivi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento civico, interno e piano!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                connData.apri()
                Dim CondEdifici As String
                If cmbEdificio.SelectedValue <> "-1" Then
                    CondEdifici = "ID_EDIFICIO =" & cmbEdificio.SelectedValue
                Else
                    CondEdifici = "ID_EDIFICIO <> 1"
                End If
                par.caricaComboTelerik("SELECT DISTINCT CIVICO FROM SISCOM_MI.INDIRIZZI WHERE DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE " & CondEdifici & " ) ORDER BY CIVICO ASC", cmbCivico, "CIVICO", "CIVICO", True, , "")
                If cmbCivico.Items.Count = 2 Then
                    cmbCivico.Items.Remove(cmbCivico.Items.FindItemByValue("-1"))
                    cmbCivico.Enabled = False
                ElseIf cmbCivico.Items.Count = 1 Then
                    cmbCivico.Enabled = False
                Else
                    cmbCivico.Enabled = True
                End If

                If cmbCivico.SelectedValue <> "" Then
                    If cmbEdificio.SelectedValue <> "-1" Then
                        CondEdifici = " AND ID_EDIFICIO =" & cmbEdificio.SelectedValue
                    Else
                        CondEdifici = ""
                    End If
                    par.caricaComboTelerik("SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT ID FROM SISCOM_MI.INDIRIZZI WHERE CIVICO='" & cmbCivico.SelectedValue & "' AND DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')" & CondEdifici & ") ORDER BY DESCRIZIONE ASC", cmbScala, "ID", "DESCRIZIONE", True, , "")
                Else
                    cmbScala.Items.Clear()
                    cmbScala.Items.Add(New RadComboBoxItem("", "-1"))
                End If
                If cmbScala.Items.Count = 2 Then
                    cmbScala.Items.Remove(cmbScala.Items.FindItemByValue("-1"))
                    cmbScala.Enabled = False
                ElseIf cmbScala.Items.Count = 1 Then
                    cmbScala.Enabled = False
                Else
                    cmbScala.Enabled = True
                End If
                If cmbCivico.SelectedValue <> "" Then
                    par.caricaComboTelerik("SELECT DISTINCT UNITA_IMMOBILIARI.INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT ID FROM SISCOM_MI.INDIRIZZI WHERE CIVICO='" & cmbCivico.SelectedValue & "' AND DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') ORDER BY INTERNO ASC", cmbInterno, "INTERNO", "INTERNO", True, , "")
                Else
                    cmbInterno.Items.Clear()
                    cmbInterno.Items.Add(New RadComboBoxItem("", "-1"))
                End If
                If cmbInterno.Items.Count = 2 Then
                    cmbInterno.Items.Remove(cmbInterno.Items.FindItemByValue("-1"))
                    cmbInterno.Enabled = False
                ElseIf cmbInterno.Items.Count = 1 Then
                    cmbInterno.Enabled = False
                Else
                    cmbInterno.Enabled = True
                End If
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Ricerca preventivi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento civico, interno e piano!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Protected Sub cmbScala_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbScala.SelectedIndexChanged
        Try
            connData.apri()
            If cmbScala.SelectedValue <> "-1" Then
                If cmbScala.SelectedValue <> "" Then
                    par.caricaComboTelerik("SELECT DISTINCT UNITA_IMMOBILIARI.INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_SCALA = " & cmbScala.SelectedValue & " ORDER BY UNITA_IMMOBILIARI.INTERNO ASC", cmbInterno, "INTERNO", "INTERNO", True, , "")
                    If cmbInterno.Items.Count = 2 Then
                        cmbInterno.Items.Remove(cmbInterno.Items.FindItemByValue("-1"))
                        cmbInterno.Enabled = False
                    ElseIf cmbInterno.Items.Count = 1 Then
                        cmbInterno.Enabled = False
                    Else
                        cmbInterno.Enabled = True
                    End If
                Else
                    cmbInterno.Items.Clear()
                    cmbInterno.Items.Add(New RadComboBoxItem("", "-1"))
                End If
            Else
                If cmbCivico.SelectedValue <> "" Then
                    par.caricaComboTelerik("SELECT DISTINCT UNITA_IMMOBILIARI.INTERNO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT ID FROM SISCOM_MI.INDIRIZZI WHERE CIVICO='" & cmbCivico.SelectedValue & "' AND DESCRIZIONE='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') ORDER BY INTERNO ASC", cmbInterno, "INTERNO", "INTERNO", True, , "")
                    If cmbInterno.Items.Count = 2 Then
                        cmbInterno.Items.Remove(cmbInterno.Items.FindItemByValue("-1"))
                        cmbInterno.Enabled = False
                    ElseIf cmbInterno.Items.Count = 1 Then
                        cmbInterno.Enabled = False
                    Else
                        cmbInterno.Enabled = True
                    End If
                Else
                    cmbInterno.Items.Clear()
                    cmbInterno.Items.Add(New RadComboBoxItem("", "-1"))
                End If
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Ricerca preventivi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento civico, interno e piano!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        caricaListaIndirizzi()
        caricaListaCivicoInternoPiano()
        caricaListaAscensori()
    End Sub
    Protected Sub DrLComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLComplesso.SelectedIndexChanged
        caricaListaEdifici()
        caricaListaIndirizzi()
        caricaListaCivicoInternoPiano()
        caricaListaAscensori()
    End Sub
    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        If HiddenFieldSelezionaTutti.Value = "0" Then
            'seleziono tutti
            For Each item As ListItem In chkListTipologie.Items
                item.Selected = True
            Next
            HiddenFieldSelezionaTutti.Value = "1"
        Else
            'deseleziono tutti
            For Each item As ListItem In chkListTipologie.Items
                item.Selected = False
            Next
            HiddenFieldSelezionaTutti.Value = "0"
        End If
    End Sub

    Protected Sub ButtonAvviaricerca_Click(sender As Object, e As System.EventArgs) Handles ButtonAvviaricerca.Click
        If cmbEdificio.SelectedValue <> "-1" Or DrLComplesso.SelectedValue <> "-1" Or Trim(TextBoxUI.Text) <> "" Then
            Try
                Dim condizioneTipologia As String = ""
                Dim listaTipologieSelezionate As String = ""
                For Each item As ListItem In chkListTipologie.Items
                    If item.Selected = True Then
                        listaTipologieSelezionate &= item.Value & ","
                    End If
                Next
                If listaTipologieSelezionate <> "" Then
                    listaTipologieSelezionate = Left(listaTipologieSelezionate, Len(listaTipologieSelezionate) - 1)
                End If
                Response.Redirect("RisultatiModificaManuale.aspx?EDIFICIO=" & cmbEdificio.SelectedValue _
                    & "&CIVICO=" & cmbCivico.SelectedValue _
                    & "&INDIRIZZO=" & par.VaroleDaPassare(par.PulisciStrSql(cmbIndirizzo.SelectedValue)) _
                    & "&COMPLESSO=" & par.VaroleDaPassare(par.PulisciStrSql(DrLComplesso.SelectedValue)) _
                    & "&INTERNO=" & cmbInterno.SelectedValue _
                    & "&SCALA=" & cmbScala.SelectedValue _
                    & "&CODUI=" & TextBoxUI.Text _
                    & "&ASCENSORE=" & cmbAscensore.SelectedValue & "&TIPOLOGIA=" & listaTipologieSelezionate, False)
            Catch ex As Exception
                Session.Add("ERRORE", "Provenienza: Ricerca preventivi - " & ex.Message)
                RadWindowManager1.RadAlert("Si è verificato un errore durante la ricerca!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
            End Try
        Else
            RadWindowManager1.RadAlert("E\' necessario selezionare almeno un complesso o un edificio oppure indicare il codice dell\'unità immobiliare!", 300, 150, "Attenzione", "", "null")

        End If
    End Sub
End Class
