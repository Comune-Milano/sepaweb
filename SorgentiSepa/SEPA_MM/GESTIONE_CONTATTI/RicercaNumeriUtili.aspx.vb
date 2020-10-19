
Partial Class GESTIONE_CONTATTI_RicercaNumeriUtili
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                lblTitolo.Text = "Ricerca Numeri Utili"
                caricaIndirizzi()
                caricaSediTerritoriali()
                caricaCategoria()
                caricaTipologiaNumeri()
                caricaFasceOrarie()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", "Home.aspx")
                Exit Sub
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaCategoria()
        Try
            connData.apri()
            par.caricaComboBox("select * from siscom_mi.tipo_segnalazione order by 2 asc", DropDownListCategoria, "id", "descrizione", True)
            connData.chiudi()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Gestione Numeri Utili - caricaCategoria - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaNumeriUtili()
        Try

            Dim condizioneRicerca As String = ""
            Dim listaSedi As String = ""
            Dim sedeCentrale As String = ""
            For Each item As ListItem In CheckBoxListSedi.Items
                If item.Selected = True Then
                    If item.Value = 0 Then
                        sedeCentrale = item.Value
                    Else
                        If listaSedi = "" Then
                            listaSedi = item.Value
                        Else
                            listaSedi &= "," & item.Value
                        End If
                    End If
                End If
            Next
            If listaSedi <> "" And sedeCentrale <> "" Then
                condizioneRicerca &= "AND (TAB_FILIALI.ID IN (" & listaSedi & ") OR TAB_FILIALI.ID IS NULL)"
            ElseIf listaSedi <> "" And sedeCentrale = "" Then
                condizioneRicerca &= "AND TAB_FILIALI.ID IN (" & listaSedi & ") "
            ElseIf listaSedi = "" And sedeCentrale <> "" Then
                condizioneRicerca &= "AND TAB_FILIALI.ID IS NULL "
            End If

            If DropDownListTipologiaNumeroUtile.SelectedValue <> "-1" Then
                condizioneRicerca &= " AND TIPOLOGIE_NUMERI_UTILI.ID=" & DropDownListTipologiaNumeroUtile.SelectedValue
            End If

            'If DropDownListOraInizio.SelectedValue <> "-1" And DropDownListMinutiInizio.SelectedValue <> "-1" Then
            '    Dim fasciaIniziale As String = DropDownListOraInizio.SelectedValue & ":" & DropDownListMinutiInizio.SelectedValue
            '    If Len(fasciaIniziale) = 5 Then
            '        condizioneRicerca &= " AND SUBSTR(FASCIA_ORARIA,1,5)>='" & fasciaIniziale & "'"
            '    End If
            'End If

            'If DropDownListOraFine.SelectedValue <> "-1" And DropDownListMinutifine.SelectedValue <> "-1" Then
            '    Dim fasciaFinale As String = DropDownListOraFine.SelectedValue & ":" & DropDownListMinutifine.SelectedValue
            '    If Len(fasciaFinale) = 5 Then
            '        condizioneRicerca &= " AND SUBSTR(FASCIA_ORARIA,7,5)<='" & fasciaFinale & "'"
            '    End If
            'End If

            If DropDownListFasce.SelectedValue <> "-1" Then
                condizioneRicerca &= " AND ID_TIPO_FASCIA=" & DropDownListFasce.SelectedValue
            End If

            If DropDownListCategoria.SelectedValue <> "-1" Then
                condizioneRicerca &= "AND ID_TIPO_SEGNALAZIONE=" & DropDownListCategoria.SelectedValue
            End If

            If DropDownListIndirizzo.SelectedValue <> "-1" Then
                condizioneRicerca &= " and TAB_FILIALI.ID=" & DropDownListIndirizzo.SelectedValue
            End If

            connData.apri(False)
            par.cmd.CommandText = "SELECT NUMERI_UTILI.ID,DESCRIZIONE AS TIPO, VALORE," _
                & " (case when id_tipo_fascia = 1 " _
                & " then 'LUN-VEN 09:00-18:00' " _
                & " when id_tipo_fascia = 2 " _
                & " then 'LUN-VEN 18:00-09:00' " _
                & " when id_tipo_fascia = 3 " _
                & " then 'SAB-DOM' " _
                & " when id_tipo_fascia = 4 " _
                & " THEN giorni ||' '||FASCIA_ORARIA " _
                & " ELSE " _
                & " NULL " _
                & " END " _
                & " ) " _
                & " AS FASCIA, " _
                & " (SELECT TIPO_SEGNALAZIONE.DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_sEGNALAZIONE.ID=ID_TIPO_sEGNALAZIONE) AS CATEGORIA, " _
                & " (CASE WHEN TAB_FILIALI.NOME IS NULL THEN 'SEDE CENTRALE' ELSE TAB_FILIALI.NOME END) AS SEDE_TERRITORIALE,'' AS ELIMINA " _
                & " FROM SISCOM_MI.NUMERI_UTILI, SISCOM_MI.TIPOLOGIE_NUMERI_UTILI, SISCOM_MI.TAB_FILIALI " _
                & " WHERE NUMERI_UTILI.ID_TIPOLOGIE_NUMERI_UTILI = TIPOLOGIE_NUMERI_UTILI.ID " _
                & " AND TAB_FILIALI.ID(+)=NUMERI_UTILI.ID_STRUTTURA " _
                & condizioneRicerca
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi(False)
            If dt.Rows.Count > 0 Then
                DataGridNumeriUtili.DataSource = dt
                DataGridNumeriUtili.DataBind()
                MultiView1.ActiveViewIndex = 1
                MultiView2.ActiveViewIndex = 1
            Else
                par.modalDialogMessage("Agenda e Segnalazioni", "Nessun numero trovato!", Page, "info")
                MultiView1.ActiveViewIndex = 0
                MultiView2.ActiveViewIndex = 0
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - caricaNumeriUtili - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaSediTerritoriali()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=105 UNION SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI)"
            par.caricaCheckBoxList(query, CheckBoxListSedi, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - caricaSediTerritoriali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub solaLettura()
        Try
            Dim mpContentPlaceHolderContenuto As ContentPlaceHolder
            mpContentPlaceHolderContenuto = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            Dim CTRL As Control = Nothing
            For Each CTRL In mpContentPlaceHolderContenuto.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaTipologiaNumeri()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_NUMERI_UTILI"
            par.caricaComboBox(query, DropDownListTipologiaNumeroUtile, "ID", "DESCRIZIONE", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - caricaTipologiaNumeri - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaFasceOrarie()
        Try
            'DropDownListOraInizio.Items.Clear()
            'DropDownListOraInizio.Items.Add(New ListItem("---", "-1"))
            'DropDownListOraFine.Items.Clear()
            'DropDownListOraFine.Items.Add(New ListItem("---", "-1"))
            'DropDownListMinutiInizio.Items.Clear()
            'DropDownListMinutiInizio.Items.Add(New ListItem("---", "-1"))
            'DropDownListMinutifine.Items.Clear()
            'DropDownListMinutifine.Items.Add(New ListItem("---", "-1"))
            'For i As Integer = 0 To 24
            '    DropDownListOraInizio.Items.Add(CStr(i).PadLeft(2, "0"))
            '    DropDownListOraFine.Items.Add(CStr(i).PadLeft(2, "0"))
            'Next
            ''DropDownListOraInizio.SelectedValue = "00"
            ''DropDownListOraFine.SelectedValue = "24"
            'For i As Integer = 0 To 45 Step 15
            '    DropDownListMinutiInizio.Items.Add(CStr(i).PadLeft(2, "0"))
            '    DropDownListMinutifine.Items.Add(CStr(i).PadLeft(2, "0"))
            'Next
            ''DropDownListMinutiInizio.SelectedValue = "00"
            ''DropDownListMinutifine.SelectedValue = "45"
            connData.apri()
            par.caricaComboBox("select id,descrizione from siscom_mi.tipo_fascia order by 1", DropDownListFasce, "id", "descrizione", True)
            connData.chiudi()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - caricaFasceOrarie - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub btnNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNuovaRicerca.Click
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        CaricaNumeriUtili()
    End Sub
    Private Sub caricaIndirizzi()
        Try
            Dim listaSedi As String = ""
            For Each item As ListItem In CheckBoxListSedi.Items
                If item.Selected = True Then
                    If listaSedi = "" Then
                        listaSedi = item.Value
                    Else
                        listaSedi &= "," & item.Value
                    End If
                End If
            Next
            If listaSedi <> "" Then
                listaSedi = " and filiali_ui.id_filiale in (" & listaSedi & ")"
            End If
            connData.apri()
            Dim QUERY As String = "SELECT DISTINCT FILIALI_UI.ID_FILIALE as id,DESCRIZIONE FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.FILIALI_UI " _
                & " WHERE INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                & " AND FILIALI_UI.ID_UI=UNITA_IMMOBILIARI.ID " _
                & " AND FILIALI_UI.FINE_VALIDITA='30000101' " _
                & listaSedi & " order by 2 asc"

            par.caricaComboBox(QUERY, DropDownListIndirizzo, "id", "descrizione", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Numeri Utili - caricaIndirizzi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub CheckBoxListSedi_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListSedi.SelectedIndexChanged
        caricaIndirizzi()
    End Sub
End Class
