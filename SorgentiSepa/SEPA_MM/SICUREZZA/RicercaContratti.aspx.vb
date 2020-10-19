Imports Telerik.Web.UI

Partial Class SICUREZZA_RicercaContratti
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
            par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1  ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            par.caricaComboTelerik("SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc", cmbIndirizzo, "descrizione", "descrizione", True)
            par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", cmbTipo, "COD", "DESCRIZIONE", True)
            caricaEdifici()
        End If

    End Sub

    Private Sub caricaEdifici()
        Try
            connData.apri()
            Dim condizioneComplesso As String = ""
            If cmbComplesso.SelectedValue <> "-1" Then
                condizioneComplesso = " AND ID_COMPLESSO=" & cmbComplesso.SelectedValue
            End If
            par.caricaComboTelerik("SELECT ID,COD_EDIFICIO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID<>1 " & condizioneComplesso & " ORDER BY DENOMINAZIONE", cmbEdificio, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Contratti - caricaEdifici - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaComplessi()
        Try
            connData.apri()
            Dim condizioneEdificio As String = ""
            If cmbEdificio.SelectedValue <> "" Then
                condizioneEdificio = " AND ID IN (SELECT id_complesso from siscom_mi.edifici where id=" & cmbEdificio.SelectedValue & ")"
            End If
            par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneEdificio & " ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Contratti - caricaComplessi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            caricaEdifici()
            Me.filtraindirizzi()
        End If
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

                'cmbIndirizzo.Text = " "

                connData.chiudi()
            End If

        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Contratti - filtraindirizzi - " & ex.Message)
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

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "chiudi", "validNavigation=true;self.close();", True)
        Else
            Response.Redirect("Home.aspx", False)
        End If
    End Sub

    Protected Sub RadGridContratti_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridContratti.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridContratti.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldIdContr').value='" & dataItem("ID").Text & "';document.getElementById('codContratto').value='" & dataItem("cod_contratto").Text & "';" _
                                             & "document.getElementById('txtRUSelected').value='Hai selezionato il contratto " & dataItem("cod_contratto").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriRU();")
        End If
    End Sub

    Protected Sub RadGridContratti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridContratti.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridRU"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridRU"), Data.DataTable)
        MultiViewBottoni.ActiveViewIndex = 1
        MultiViewRicerca.ActiveViewIndex = 1
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
            Dim sStringaSql As String = ""
            Dim sStringaSql1 As String

            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String

            bTrovato = False
            sStringaSql = ""

            If txtCognome.Text <> "" Then
                sValore = txtCognome.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " ANAGRAFICA.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If

            If txtNome.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = txtNome.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " ANAGRAFICA.NOME " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If


            If txtCF.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtCF.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " ANAGRAFICA.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If

            If txtpiva.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtpiva.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " ANAGRAFICA.PARTITA_IVA " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If

            If txtUnita.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtUnita.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "'"
            End If

            If cmbStato.SelectedItem.Value <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = cmbStato.SelectedItem.Value
                bTrovato = True
                sStringaSql = sStringaSql & " SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID)='" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If

            If txtRagione.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtRagione.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " ANAGRAFICA.RAGIONE_SOCIALE" & sCompara & "'" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If

            If Not IsNothing(txtStipulaDal.SelectedDate) Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtStipulaDal.SelectedDate
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA>='" & par.AggiustaData(sValore) & "' "
            End If

            If Not IsNothing(txtStipulaAl.SelectedDate) Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtStipulaAl.SelectedDate
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_STIPULA<='" & par.AggiustaData(sValore) & "' "
            End If

           
            If Not IsNothing(txtSloggioDal.SelectedDate) Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtSloggioDal.SelectedDate
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA>='" & par.AggiustaData(sValore) & "' "
            End If

            If Not IsNothing(txtSloggioAl.SelectedDate) Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "

                sValore = txtSloggioAl.SelectedDate
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.DATA_RICONSEGNA<='" & par.AggiustaData(sValore) & "' "
            End If

            If txtCod.Text <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = txtCod.Text
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore.ToUpper) & "' "
            End If

            If cmbTipo.SelectedValue <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = cmbTipo.SelectedValue
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & " RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC ='" & par.PulisciStrSql(sValore) & "' "
            End If

            If cmbEdificio.SelectedValue <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = cmbEdificio.SelectedItem.Value
                bTrovato = True
                sStringaSql = sStringaSql & " EDIFICI.ID = " & cmbEdificio.SelectedValue
            End If
            If cmbComplesso.SelectedValue <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = cmbComplesso.SelectedItem.Value
                bTrovato = True
                sStringaSql = sStringaSql & " COMPLESSI_IMMOBILIARI.ID= " & cmbComplesso.SelectedValue & ""
            End If

            If cmbIndirizzo.SelectedValue <> "" And cmbIndirizzo.SelectedValue <> "-1" Then
                sValore = cmbIndirizzo.SelectedItem.Value
                sStringaSql = sStringaSql & " UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (sValore) & "' "
                sStringaSql = sStringaSql & ")"
            End If

            sStringaSql1 = "SELECT DISTINCT(RAPPORTI_UTENZA.COD_CONTRATTO) AS CODCONTR,(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004' " _
             & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='V' THEN '431/98 ART.15 C.2 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
             & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO, (CASE WHEN RAPPORTI_UTENZA.DURATA_ANNI IS NULL AND RAPPORTI_UTENZA.DURATA_RINNOVO IS NULL THEN RAPPORTI_UTENZA.DURATA_ANNI||''||RAPPORTI_UTENZA.DURATA_RINNOVO ELSE " _
             & "RAPPORTI_UTENZA.DURATA_ANNI||'+'||RAPPORTI_UTENZA.DURATA_RINNOVO END) AS DURATA,TAB_FILIALI.NOME AS FILIALE_ALER,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as CUI," _
             & "RAPPORTI_UTENZA.*,nvl(ANAGRAFICA.CITTADINANZA,'') as cittadinanza,siscom_mi.getstatocontratto(rapporti_utenza.id) as STATO_DEL_CONTRATTO, SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS NOME_INTEST," _
             & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"" ," _
             & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ,TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NASCITA,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA," _
             & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) AS ""POSIZIONE_CONTRATTO"",TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCCUPANTE,unita_immobiliari.interno,scale_edifici.descrizione AS scala,tipo_livello_piano.descrizione AS piano " _
             & "FROM " _
             & "siscom_mi.scale_edifici,siscom_mi.tipo_livello_piano,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
             & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
             & "WHERE " _
             & "scale_edifici.ID(+)=unita_immobiliari.id_scala AND tipo_livello_piano.cod(+)=unita_immobiliari.cod_tipo_livello_piano AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+) AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
             & " and cod_tipologia_occupante='INTE' AND EDIFICI.ID (+) =UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID (+) =UNITA_CONTRATTUALE.ID_UNITA  AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
             & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND TIPOLOGIA_OCCUPANTE.COD = SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL "
            If sStringaSql <> "" Then
                If Left(sStringaSql, 4) = " AND" Then
                    sStringaSql = Replace(sStringaSql, "AND", " ")
                End If
                sStringaSql1 = sStringaSql1 & " AND " & sStringaSql
            End If
            sStringaSql1 = sStringaSql1 & " ORDER BY ""INTESTATARIO"" ASC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql1, par.OracleConn)
            da.Fill(dt)

            Session.Item("DataGridRU") = dt
            RadGridContratti.CurrentPageIndex = 0
            RadGridContratti.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovati - " & dt.Rows.Count & " contratti"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovato - " & dt.Rows.Count & " contratto"
            ElseIf dt.Rows.Count = 0 Then
                lblRisultati.Text = ""
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
            Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
            SvuotaCampi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Contratti - CaricaRisultati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNuovaRicerca.Click
        ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=0;", True)
        MultiViewRicerca.ActiveViewIndex = 0
        MultiViewBottoni.ActiveViewIndex = 0
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        SvuotaCampi()
        par.caricaComboTelerik("SELECT ID,COD_COMPLESSO||' - '||DENOMINAZIONE AS NOME,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1  ORDER BY DENOMINAZIONE", cmbComplesso, "ID", "NOME", True)
        par.caricaComboTelerik("SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc", cmbIndirizzo, "descrizione", "descrizione", True)
        par.caricaComboTelerik("SELECT * FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY DESCRIZIONE ASC", cmbTipo, "COD", "DESCRIZIONE", True)
        caricaEdifici()
    End Sub

    Private Sub SvuotaCampi()
        txtCF.Text = ""
        txtCod.Text = ""
        txtCognome.Text = ""
        txtNome.Text = ""
        txtpiva.Text = ""
        txtRagione.Text = ""
        txtUnita.Text = ""
        txtSloggioAl.Clear()
        txtSloggioDal.Clear()
        txtStipulaAl.Clear()
        txtStipulaDal.Clear()
        cmbComplesso.ClearSelection()
        cmbEdificio.ClearSelection()
        cmbIndirizzo.ClearSelection()
        cmbStato.ClearSelection()
        cmbTipo.ClearSelection()
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Multi.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub
End Class
