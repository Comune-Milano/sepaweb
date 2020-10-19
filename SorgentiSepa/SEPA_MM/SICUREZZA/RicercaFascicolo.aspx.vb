Imports Telerik.Web.UI

Partial Class SICUREZZA_RicercaFascicolo
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
                caricaStrutture()
                caricaComplessi()
                caricaEdifici()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Fascicolo - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaStrutture()
        Try
            connData.apri()
            par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME ASC", CheckBoxListSedi, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Fascicolo - caricaStrutture - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaComplessi()
        Try
            connData.apri()
            Dim condizioneSedeTerritoriale As String = ""
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
                condizioneSedeTerritoriale = " AND ID_FILIALE in (" & listaSedi & ")"
            End If
            par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneSedeTerritoriale & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Fascicolo - caricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub caricaEdifici()
        Try
            connData.apri()
            Dim condizioneComplesso As String = ""
            If cmbComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO=" & cmbComplesso.SelectedValue
            Else
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
                    condizioneComplesso = " AND ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE in ( " & listaSedi & ")) "
                End If
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Fascicolo - caricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        CaricaRisultati()
        MultiViewRicerca.ActiveViewIndex = 1
        MultiViewBottoni.ActiveViewIndex = 1
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Private Sub CaricaRisultati()
        Try
            Dim dt As New Data.DataTable
            Dim CondizioneRicerca As String = ""

            Dim bTrovato As Boolean = False
            Dim sValore As String = True
            Dim sCompara As String = True

            If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID=" & TextBoxNumero.Text
            End If

            If Trim(txtNumIntervento.Text) <> "" AndAlso IsNumeric(txtNumIntervento.Text) Then
                CondizioneRicerca &= " AND INTERVENTI_SICUREZZA.ID=" & txtNumIntervento.Text
            End If
            If Trim(txtNumIntervento.Text) <> "" AndAlso IsNumeric(txtNumFascicolo.Text) Then
                CondizioneRicerca &= " AND FASCICOLI_SICUREZZA.ID=" & txtNumFascicolo.Text
            End If

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
                CondizioneRicerca &= " AND TAB_FILIALI.ID in (" & listaSedi & ") "
            End If

            If cmbEdificio.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID_EDIFICIO = " & cmbEdificio.SelectedValue
            End If
            If cmbComplesso.SelectedValue <> "-1" Then
                CondizioneRicerca &= " and segnalazioni.id_edificio in (select id from siscom_mi.edifici where id_complesso= " & cmbComplesso.SelectedValue & ")"
            End If

            If txtCodUI.Text <> "" Then
                sValore = txtCodUI.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                CondizioneRicerca = CondizioneRicerca & " AND UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "'"
            End If

            par.cmd.CommandText = "select distinct fascicolo_sicurezza.id as num,cod_unita_immobiliare,edifici.denominazione as indirizzo from siscom_mi.fascicolo_sicurezza,siscom_mi.unita_immobiliari," _
                & " siscom_mi.tab_filiali,siscom_mi.interventi_sicurezza,siscom_mi.segnalazioni,SISCOM_mi.edifici where " _
                & " fascicolo_sicurezza.id_unita=unita_immobiliari.id and fascicolo_sicurezza.id=interventi_sicurezza.id_fascicolo AND SEGNALAZIONI.ID(+) = INTERVENTI_SICUREZZA.ID_SEGNALAZIONE " _
               & " AND EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND INTERVENTI_SICUREZZA.id_sede_territoriale = tab_filiali.ID(+) " & CondizioneRicerca & " order by fascicolo_sicurezza.id asc "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)

            Session.Item("DataGridFasc") = dt
            RadGridFascicoli.CurrentPageIndex = 0
            RadGridFascicoli.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovati - " & dt.Rows.Count & " fascicoli"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovato - " & dt.Rows.Count & " fascicolo"
            ElseIf dt.Rows.Count = 0 Then
                lblRisultati.Text = ""
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
            Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Fascicolo - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridFascicoli_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridFascicoli.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"

                Dim query As String = "SELECT INTERVENTI_SICUREZZA.ID, INTERVENTI_SICUREZZA.ID AS NUM,TIPO_INTERVENTO.DESCRIZIONE AS TIPO,TAB_STATI_INTERVENTI.DESCRIZIONE AS STATO,TO_CHAR(TO_DATE(SUBSTR(data_ora_inserimento,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ORA_INSERIM," _
                                      & " INTERVENTI_SICUREZZA.ASSEGNATARIO,INTERVENTI_SICUREZZA.ASSEGNATARIO_2  FROM SISCOM_MI.interventi_sicurezza,SISCOM_MI.TAB_STATI_INTERVENTI,SISCOM_MI.TIPO_INTERVENTO WHERE " _
                                      & " TAB_STATI_INTERVENTI.ID(+) = INTERVENTI_SICUREZZA.ID_STATO AND TIPO_INTERVENTO.ID(+)=INTERVENTI_SICUREZZA.ID_TIPO_INTERVENTO and ID_unita in (select id from siscom_mi.unita_immobiliari where cod_unita_immobiliare='" & dataItem("COD_UNITA_IMMOBILIARE").Text & "')" _
                                      & " order by INTERVENTI_SICUREZZA.ID desc"
                e.DetailTableView.DataSource = GetDataTable(query)
                MultiViewRicerca.ActiveViewIndex = 1
                MultiViewBottoni.ActiveViewIndex = 1


        End Select
    End Sub

    Public Function GetDataTable(query As String) As Data.DataTable
        Dim myDataTable As New Data.DataTable
        par.cmd.CommandText = query
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(myDataTable)
        Return myDataTable
    End Function

    Protected Sub RadGridFascicoli_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridFascicoli.ItemDataBound
       
        If TypeOf e.Item Is GridDataItem Then
            If TypeOf e.Item Is GridDataItem AndAlso e.Item.OwnerTableView.Name = "Dettagli" Then
                Dim dataItem As GridDataItem = e.Item


                dataItem.Attributes.Add("onclick", "document.getElementById('idIntervento').value=" & dataItem("ID").Text & ";" _
                                             & "document.getElementById('txtFascicoloSelected').value='Hai selezionato l\'intervento numero " & dataItem("NUM").Text & "';")
                dataItem.Attributes.Add("onDblclick", " window.open('NuovoIntervento.aspx?NM=1&IDI= " & dataItem("ID").Text & "', '');")

            End If

        End If

    End Sub

    Protected Sub RadGridFascicoli_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridFascicoli.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridFasc"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridFasc"), Data.DataTable)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbComplesso.SelectedIndexChanged
        caricaEdifici()
        cmbEdificio.Focus()
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        If IsNumeric(HiddenFieldFascicolo.Value) AndAlso HiddenFieldFascicolo.Value <> "-1" AndAlso HiddenFieldFascicolo.Value <> "0" Then
            Dim apertura As String = "window.open('StampaFascicolo.aspx?IDF=" & HiddenFieldFascicolo.Value & "', 'sFasc' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun fascicolo selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub btnElencoStampe_Click(sender As Object, e As System.EventArgs) Handles btnElencoStampe.Click
        Dim apertura As String = "window.open('ElencoFascicoli.aspx', 'efasc' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If IsNumeric(idIntervento.Value) AndAlso idIntervento.Value <> "-1" AndAlso idIntervento.Value <> "0" Then
            Dim apertura As String = "window.open('NuovoIntervento.aspx?NM=1&IDI=" & idIntervento.Value & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun intervento selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub
End Class
