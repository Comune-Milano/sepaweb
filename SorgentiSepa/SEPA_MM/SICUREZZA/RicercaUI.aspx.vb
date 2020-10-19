Imports Telerik.Web.UI

Partial Class SICUREZZA_RicercaUI
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then

            par.caricaComboTelerik("SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc", cmbIndirizzo, "descrizione", "descrizione", True)
            par.caricaCheckBoxList("SELECT cod, descrizione FROM SISCOM_MI.tipologia_unita_immobiliari order by descrizione asc", CheckBoxListTipo, "COD", "DESCRIZIONE")
            par.caricaComboTelerik("SELECT id,descrizione FROM SISCOM_MI.DESTINAZIONI_USO_UI", cmbDestUso, "ID", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT cod,descrizione FROM SISCOM_MI.TIPO_DISPONIBILITA order by cod asc", cmbDisponibilita, "COD", "DESCRIZIONE", True)

            caricaComplessi()
            caricaEdifici()
            txtSupNettaDa.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtSupNettaDa.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            txtSupNettaA.Attributes.Add("onblur", "javascript:valid(this,'onlynumbers');AutoDecimal(this,2);return false;")
            txtSupNettaA.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

        End If

    End Sub

    Private Sub caricaEdifici()
        Try
            connData.apri()
            Dim sStringaSqlComplesso As String = ""
            If cmbComplesso.SelectedValue <> "-1" Then
                sStringaSqlComplesso = " AND ID_COMPLESSO=" & cmbComplesso.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & sStringaSqlComplesso & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - caricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaComplessi()
        Try
            connData.apri()
            Dim sStringaSqlEdificio As String = ""
            If cmbEdificio.SelectedValue <> "" Then
                sStringaSqlEdificio = " AND ID IN (SELECT id_complesso from siscom_mi.edifici where id=" & cmbEdificio.SelectedValue & ")"
            End If
            par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & sStringaSqlEdificio & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - caricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub filtraindirizzi()
        Try
            If Me.cmbEdificio.SelectedValue <> "-1" Or Me.cmbComplesso.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    connData.apri()
                End If
                Me.cmbIndirizzo.Items.Clear()

                cmbIndirizzo.Items.Add(" ")

                If Me.cmbEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
                ElseIf Me.cmbComplesso.SelectedValue <> "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue & ") order by descrizione asc"
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
                End While
                myReader1.Close()

                connData.chiudi()
            End If

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - filtraindirizzi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.Text <> "-1" Then
            Me.cmbComplesso.Items.Clear()
            caricaComplessi()
            Me.filtraindirizzi()
        End If
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            Dim query As String = ""
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    connData.apri()
                End If
                Dim CondEdifici As String


                If Me.cmbEdificio.SelectedValue <> -1 Then
                    CondEdifici = "ID_EDIFICIO =" & Me.cmbEdificio.SelectedValue
                Else
                    CondEdifici = "ID_EDIFICIO <> 1"
                End If

                query = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT id_indirizzo FROM siscom_mi.unita_immobiliari WHERE ID_EDIFICIO<>1 AND " & CondEdifici & " ) order by civico asc"
                par.caricaComboTelerik(query, cmbCivico, "civico", "civico", True, , , False)




                If cmbCivico.SelectedValue <> "-1" Then
                    If Me.cmbEdificio.SelectedValue <> -1 Then
                        CondEdifici = " AND ID_EDIFICIO =" & Me.cmbEdificio.SelectedValue
                    Else
                        CondEdifici = ""
                    End If

                    query = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO<>1 AND UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')" & CondEdifici & ") order by descrizione asc"
                    par.caricaComboTelerik(query, cmbScala, "id", "descrizione", True, , , False)

                End If


                cmbInterno.Items.Clear()
                If Me.cmbCivico.SelectedValue <> "-1" Then

                    query = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where ID_EDIFICIO<>1 AND unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    par.caricaComboTelerik(query, cmbInterno, "interno", "interno", True, , , False)
                End If


                connData.chiudi()
            End If

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - cmbIndirizzo_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "chiudi", "validNavigation=true;self.close();", True)
        Else
            Response.Redirect("Home.aspx", False)
        End If
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            caricaEdifici()
            Me.filtraindirizzi()
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
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
            Dim query1 As String = ""
            Dim sCompara As String = ""
            Dim sStringaSql As String = ""
            Dim sStringaSql1 As String

            Dim bTrovato As Boolean
            Dim sValore As String
            
            bTrovato = False
            sStringaSql1 = "SELECT DISTINCT ROWNUM,  EDIFICI.DENOMINAZIONE, SISCOM_MI.UNITA_IMMOBILIARI.ID, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA,TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE ,UNITA_IMMOBILIARI.INTERNO,  (SCALE_EDIFICI.DESCRIZIONE) AS SCALA ,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,identificativi_catastali.SUB,TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,UNITA_IMMOBILIARI.S_NETTA,NVL(TO_CHAR(identificativi_catastali.rendita),'NO') AS RENDITA,(INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO) AS INDIRIZZO,INDIRIZZI.LOCALITA AS COMUNE FROM SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI, SISCOM_MI.INDIRIZZI, SEPA.COMUNI_NAZIONI, SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.identificativi_catastali where SISCOM_MI.TIPO_LIVELLO_PIANO.COD (+) = UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND UNITA_IMMOBILIARI.ID_SCALA= SCALE_EDIFICI.ID(+) AND TIPOLOGIA_UNITA_IMMOBILIARI.COD= UNITA_IMMOBILIARI.COD_TIPOLOGIA and SISCOM_MI.EDIFICI.ID = SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO and SISCOM_MI.UNITA_IMMOBILIARI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID (+) and SISCOM_MI.INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD (+)AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND EDIFICI.ID <> 1 "

            If cmbEdificio.SelectedValue <> "-1" Then
                sValore = cmbEdificio.SelectedValue

                bTrovato = True
                sStringaSql = "AND UNITA_IMMOBILIARI.ID_EDIFICIO = " & par.PulisciStrSql(sValore) & ""
            End If

            If cmbCondominio.SelectedValue <> "-1" Then
                sStringaSql = " AND EDIFICI.CONDOMINIO = " & cmbCondominio.SelectedValue & ""
            End If

            If cmbComplesso.SelectedValue <> "-1" Then
                sValore = cmbComplesso.SelectedValue
                bTrovato = True
                sStringaSql = sStringaSql & " AND EDIFICI.ID_COMPLESSO =" & sValore
            End If

            If par.IfEmpty(cmbIndirizzo.SelectedValue, "-1") <> "-1" AndAlso cmbIndirizzo.SelectedValue <> "" Then
                sValore = cmbIndirizzo.SelectedValue
                sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (sValore) & "' "
                If par.IfEmpty(cmbCivico.SelectedValue, "-1") <> "-1" Then
                    sValore = cmbCivico.SelectedValue
                    sStringaSql = sStringaSql & " AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
                End If
                sStringaSql = sStringaSql & ")"
            End If

            If par.IfEmpty(cmbScala.SelectedValue, "-1") <> "-1" Then
                sValore = cmbScala.SelectedValue
                sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.ID_SCALA = " & sValore
            End If

            'PARAMETRO DI RICERCA HANDICAP
            If cmbHandicap.SelectedValue <> "-1" AndAlso cmbHandicap.SelectedValue <> "" Then
                If cmbHandicap.SelectedValue = 0 Then
                    sStringaSql = sStringaSql & " AND 'NO'=(CASE WHEN (SELECT DISTINCT UNITA_STATO_MANUTENTIVO.ID_UNITA FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND UNITA_STATO_MANUTENTIVO.ID_UNITA = UI.ID  AND UNITA_STATO_MANUTENTIVO.HANDICAP=0)>0 THEN 'NO' END) "
                ElseIf cmbHandicap.SelectedValue = 1 Then
                    sStringaSql = sStringaSql & " AND 'SI'=(CASE WHEN (SELECT DISTINCT UNITA_STATO_MANUTENTIVO.ID_UNITA FROM SISCOM_MI.UNITA_STATO_MANUTENTIVO,SISCOM_MI.UNITA_IMMOBILIARI UI WHERE UI.ID=unita_immobiliari.ID AND UNITA_STATO_MANUTENTIVO.ID_UNITA = UI.ID AND UNITA_STATO_MANUTENTIVO.HANDICAP=1)>0 THEN 'SI' END) "
                End If
            End If

            'PARAMETRO DI RICERCA STATO DISPONIBILITA
            If cmbDisponibilita.SelectedValue <> "-1" AndAlso cmbDisponibilita.SelectedValue <> "" Then
                sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.COD_TIPO_DISPONIBILITA = '" & cmbDisponibilita.SelectedValue & "' "
            End If

            If Not IsNothing(cmbDestUso.SelectedValue) Then
                If cmbDestUso.SelectedValue <> "-1" AndAlso cmbDestUso.SelectedValue <> "" Then
                    sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = " & cmbDestUso.SelectedValue & " "
                End If

            End If

            Dim SSTRAPPOGGIO As String = ""
            If txtSupNettaDa.Text <> "" Then
                SSTRAPPOGGIO = " UNITA_IMMOBILIARI.S_NETTA>=" & par.VirgoleInPunti(txtSupNettaDa.Text)
            End If
            If txtSupNettaA.Text <> "" Then
                If SSTRAPPOGGIO <> "" Then
                    SSTRAPPOGGIO = SSTRAPPOGGIO & " AND UNITA_IMMOBILIARI.S_NETTA<=" & par.VirgoleInPunti(txtSupNettaA.Text)
                Else
                    SSTRAPPOGGIO = " UNITA_IMMOBILIARI.S_NETTA<=" & par.VirgoleInPunti(txtSupNettaA.Text)
                End If
            End If

            If SSTRAPPOGGIO <> "" Then
                sStringaSql = sStringaSql & " AND " & SSTRAPPOGGIO & " "
            End If

            If txtCodUI.Text <> "" Then
                'If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtCodUI.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " and UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "'"
            End If


            Dim listaTipoUI As String = ""
            For Each item As ListItem In CheckBoxListTipo.Items
                If item.Selected = True Then
                    If listaTipoUI = "" Then
                        listaTipoUI = "'" & item.Value & "'"
                    Else
                        listaTipoUI &= ",'" & item.Value & "'"
                    End If
                End If
            Next
            If listaTipoUI <> "" Then
                sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA in ( " & listaTipoUI & ") "
            End If

            If par.IfEmpty(cmbInterno.SelectedValue, "-1") <> "-1" Then
                sValore = cmbInterno.SelectedValue
                sStringaSql = sStringaSql & " AND UNITA_IMMOBILIARI.INTERNO ='" & par.PulisciStrSql(sValore) & "' "
            End If

            If sStringaSql <> "" Then
                sStringaSql1 = sStringaSql1 & sStringaSql
            End If
            sStringaSql1 = sStringaSql1 & " ORDER BY DENOMINAZIONE ASC, SCALA ASC, INTERNO ASC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql1, par.OracleConn)
            da.Fill(dt)

            Session.Item("DataGridUI") = dt
            RadGridUnita.CurrentPageIndex = 0
            RadGridUnita.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovati - " & dt.Rows.Count & " unità"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovato - " & dt.Rows.Count & " unità"
            ElseIf dt.Rows.Count = 0 Then
                lblRisultati.Text = ""
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
            Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
            SvuotaCampi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - CaricaRisultati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SvuotaCampi()
        CheckBoxListTipo.ClearSelection()
        cmbDestUso.ClearSelection()
        cmbDisponibilita.ClearSelection()
        cmbHandicap.ClearSelection()
        cmbInterno.ClearSelection()
        cmbScala.ClearSelection()
        txtSupNettaA.Text = ""
        txtSupNettaDa.Text = ""
        cmbComplesso.ClearSelection()
        cmbEdificio.ClearSelection()
        cmbIndirizzo.ClearSelection()
        cmbCondominio.ClearSelection()
        cmbCivico.ClearSelection()
    End Sub

    Protected Sub RadGridUnita_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridUnita.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridUnita.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldIdUI').value='" & dataItem("ID").Text & "';document.getElementById('codUI').value='" & dataItem("cod_unita_immobiliare").Text & "';" _
                                             & "document.getElementById('txtUISelected').value='Hai selezionato l\'unità " & dataItem("cod_unita_immobiliare").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriUI();")
        End If
    End Sub

    Protected Sub RadGridUnita_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridUnita.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridUI"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridUI"), Data.DataTable)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dt As Data.DataTable = CType(Session.Item("DataGridUI"), Data.DataTable)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportElencoUI", "ExportElencoUI", dt, True, "../FileTemp/", False)

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

    Protected Sub btnNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNuovaRicerca.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=0;", True)
        MultiViewRicerca.ActiveViewIndex = 0
        MultiViewBottoni.ActiveViewIndex = 0
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        SvuotaCampi()
    End Sub

    Protected Sub cmbCivico_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCivico.SelectedIndexChanged
        Try
            Dim query As String = ""
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    connData.apri()
                End If
                If cmbCivico.Text <> "" Then
                    Me.cmbScala.Items.Clear()
                    If cmbCivico.SelectedValue <> "-1" Then
                        'par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')) order by descrizione asc"
                        query = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')) order by descrizione asc"
                        par.caricaComboTelerik(query, cmbScala, "id", "descrizione", True, , , False)
                    End If
                End If

                cmbInterno.ClearSelection()
                If Me.cmbCivico.SelectedValue <> "-1" Then
                    query = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    par.caricaComboTelerik(query, cmbInterno, "interno", "interno", True, , , False)
                End If
                connData.chiudi()
            End If

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - cmbCivico_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbScala_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbScala.SelectedIndexChanged
        Try
            Dim query As String = ""
            If Me.cmbScala.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    connData.apri()
                End If

                cmbInterno.ClearSelection()
                If Me.cmbScala.SelectedValue <> "" Then
                    query = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari WHERE ID_EDIFICIO<>1 AND ID_SCALA = " & Me.cmbScala.SelectedValue.ToString & " order by unita_immobiliari.interno asc"
                    par.caricaComboTelerik(query, cmbInterno, "interno", "interno", True, , , False)
                End If
            Else
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbInterno.ClearSelection()
                If Me.cmbCivico.SelectedValue <> "" Then
                    query = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where ID_EDIFICIO<>1 AND unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    par.caricaComboTelerik(query, cmbInterno, "interno", "interno", True, , , False)
                End If

                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca UI - cmbScala_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
