Imports Telerik.Web.UI

Partial Class SICUREZZA_RicercaInterventi
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
                par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_INTERVENTI ORDER BY id ASC", CheckBoxListStato, "ID", "DESCRIZIONE")
                For Each elemento As ListItem In CheckBoxListStato.Items
                    If elemento.Value <> "4" And elemento.Value <> "5" Then
                        elemento.Selected = True
                    End If
                Next
                par.caricaCheckBoxList("SELECT * FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0", chkListStatoSegn, "ID", "DESCRIZIONE")
                For Each elemento2 As ListItem In chkListStatoSegn.Items
                    If elemento2.Text <> "CHIUSA" Then
                        elemento2.Selected = True
                    End If
                Next
                par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPO_INTERVENTO ORDER BY DESCRIZIONE ASC", cmbTipoInterv, "ID", "DESCRIZIONE", True)
                par.caricaComboTelerik("SELECT ID, OPERATORE FROM OPERATORI WHERE NVL(MOD_SICUREZZA,0)=1 ORDER BY OPERATORE ASC", cmbAssegnatario, "ID", "OPERATORE", True)
                par.caricaComboTelerik("SELECT ID, OPERATORE FROM OPERATORI WHERE NVL(MOD_SICUREZZA,0)=1 ORDER BY OPERATORE ASC", cmbCoAssegnatario, "ID", "OPERATORE", True)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Interventi - Page_Load - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - caricaComplessi - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - caricaEdifici - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Segnalazioni - caricaStrutture - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbComplesso.SelectedIndexChanged
        caricaEdifici()
        cmbEdificio.Focus()
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        CaricaRisultati()
        MultiViewRicerca.ActiveViewIndex = 1
        MultiViewBottoni.ActiveViewIndex = 1
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub

    Private Sub CaricaRisultati()
        Try
            Dim dt As New Data.DataTable
            Dim CondizioneRicerca As String = ""
            If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
                CondizioneRicerca &= " AND SEGNALAZIONI.ID=" & TextBoxNumero.Text
            End If
            Dim listaTipologie As String = ""

            listaTipologie = cmbTipoInterv.SelectedValue
            If IsNumeric(cmbTipoInterv.SelectedValue) AndAlso cmbTipoInterv.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND INTERVENTI_SICUREZZA.ID_TIPO_INTERVENTO=" & cmbTipoInterv.SelectedValue
            End If

            Dim listaStati As String = ""
            For Each elemento As ListItem In CheckBoxListStato.Items
                If elemento.Selected = True Then
                    If listaStati = "" Then
                        listaStati = elemento.Value
                    Else
                        listaStati &= "," & elemento.Value
                    End If
                End If
            Next
            If listaStati <> "" Then
                CondizioneRicerca &= " AND (INTERVENTI_SICUREZZA.ID_STATO in (" & listaStati & ") or INTERVENTI_SICUREZZA.id_Stato is null) "
            End If


            Dim listaStatiSegn As String = ""
            For Each elemento2 As ListItem In chkListStatoSegn.Items
                If elemento2.Selected = True Then
                    If listaStatiSegn = "" Then
                        listaStatiSegn = elemento2.Value
                    Else
                        listaStatiSegn &= "," & elemento2.Value
                    End If
                End If
            Next
            If listaStatiSegn <> "" Then
                CondizioneRicerca &= " AND (SEGNALAZIONI.ID_STATO IN (" & listaStatiSegn & ") or SEGNALAZIONI.id_Stato is null) "
            End If


            Dim dataMin As String = ""
            Dim dataMax As String = ""
            If Not IsNothing(txtDal.SelectedDate) Then
                dataMin = par.AggiustaData(txtDal.SelectedDate)
            End If
            If dataMin <> "" Then
                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_INSERIMENTO,1,8)>='" & dataMin & "'  "
            End If
            If Not IsNothing(txtAl.SelectedDate) Then
                dataMax = par.AggiustaData(txtAl.SelectedDate)
            End If
            If dataMax <> "" Then
                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_INSERIMENTO,1,8)<='" & dataMax & "' "
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

            If cmbAssegnatario.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND INTERVENTI_SICUREZZA.ASSEGNATARIO = '" & cmbAssegnatario.SelectedItem.Text & "'"
            End If
            If cmbCoAssegnatario.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND INTERVENTI_SICUREZZA.ASSEGNATARIO_2= '" & cmbCoAssegnatario.SelectedItem.Text & "'"
            End If


            par.cmd.CommandText = " SELECT distinct INTERVENTI_SICUREZZA.ID,edifici.denominazione as edificio,tab_filiali.nome as sede_territoriale," _
                & "UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA,INDIRIZZI.DESCRIZIONE ||' '||INDIRIZZI.CIVICO AS INDIRIZZO, " _
               & " INTERVENTI_SICUREZZA.ID AS NUM,TIPO_INTERVENTO.DESCRIZIONE AS TIPO, " _
               & " TAB_STATI_INTERVENTI.DESCRIZIONE AS STATO,data_ora_inserimento, " _
               & " TO_CHAR(TO_DATE(SUBSTR(data_ora_inserimento,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ORA_INSERIM,INTERVENTI_SICUREZZA.ASSEGNATARIO,INTERVENTI_SICUREZZA.ASSEGNATARIO_2 " _
               & " FROM siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.INDIRIZZI,SISCOM_MI.INTERVENTI_SICUREZZA,SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TAB_STATI_INTERVENTI,SISCOM_MI.TIPO_INTERVENTO,siscom_mi.TAB_FILIALI " _
               & " WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) AND " _
               & " INTERVENTI_SICUREZZA.ID_UNITA=UNITA_IMMOBILIARI.ID and " _
               & " complessi_immobiliari.id=edifici.id_complesso and " _
               & " UNITA_IMMOBILIARI.id_Edificio=edifici.id " _
               & " AND TAB_STATI_INTERVENTI.ID(+) = INTERVENTI_SICUREZZA.ID_STATO " _
               & " AND SEGNALAZIONI.ID(+) = INTERVENTI_SICUREZZA.ID_SEGNALAZIONE " _
               & " AND complessi_immobiliari.id_filiale = tab_filiali.ID(+) " _
               & " AND TIPO_INTERVENTO.ID(+)=INTERVENTI_SICUREZZA.ID_TIPO_INTERVENTO " _
               & CondizioneRicerca _
               & " ORDER BY data_ora_inserimento DeSC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)

            Session.Item("DataGridInterv") = dt
            RadGridInterventi.CurrentPageIndex = 0
            RadGridInterventi.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovati - " & dt.Rows.Count & " interventi"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovato - " & dt.Rows.Count & " intervento"
            ElseIf dt.Rows.Count = 0 Then
                lblRisultati.Text = ""
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
            Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Interventi - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub RadGridInterventi_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridInterventi.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridInterventi.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldIntervento').value='" & dataItem("NUM").Text & "';" _
                                             & "document.getElementById('txtInterventoSelected').value='Hai selezionato l\'intervento numero " & dataItem("NUM").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriIntervento();")
        End If
    End Sub

    Protected Sub RadGridInterventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridInterventi.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridInterv"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridInterv"), Data.DataTable)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dt As Data.DataTable = CType(Session.Item("DataGridInterv"), Data.DataTable)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportInterventi", "ExportInterventi", dt, True, "../FileTemp/", False)

            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)

            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
            Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Intervento - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If IsNumeric(HiddenFieldIntervento.Value) AndAlso HiddenFieldIntervento.Value <> "-1" AndAlso HiddenFieldIntervento.Value <> "0" Then
            Dim apertura As String = "window.open('NuovoIntervento.aspx?NM=1&IDI=" & HiddenFieldIntervento.Value & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessuna intervento selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
    End Sub
End Class
