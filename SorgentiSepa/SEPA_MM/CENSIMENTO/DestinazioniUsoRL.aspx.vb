Imports Telerik.Web.UI
Imports System.Data
Imports Telerik.Web.UI.Upload

Partial Class CENSIMENTO_DestinazioniUsoRL
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Public Property AsyncPostBackTimeout As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        Try
            If Not IsPostBack Then
                HFGriglia.Value = dgvDestUsoRL.ClientID & "," & dgvUnitaImmobiliari.ClientID
                connData.apri(False)
                CaricaGridUnita()
                connData.chiudi(False)
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
   
    Protected Sub dgvDestUsoRL_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvDestUsoRL.ItemCommand
        Try
            If e.CommandName.ToString.Equals("Delete") Then
                connData.apri(False)
                par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_DESTINAZIONE_USO_RL = " & HiddenFieldIdDestUsoRL.Value
                Dim numOccorrenze As Integer = par.cmd.ExecuteScalar
                If numOccorrenze = 0 Then
                    par.cmd.CommandText = "delete from SISCOM_MI.DESTINAZIONI_USO_RL_UI where id = " & HiddenFieldIdDestUsoRL.Value
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    dgvDestUsoRL.Rebind()
                Else
                    RadWindowManager1.RadAlert("La destinazione d\'uso RL che si vuol eliminare è in uso dal sistema!<br />Impossibile procedere.", 300, 150, "Attenzione", "", "null")
                    connData.chiudi(False)
                End If
            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub dgvDestUsoRL_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvDestUsoRL.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('HiddenFieldIdDestUsoRL').value = '" & dataItem("ID").Text & "';document.getElementById('HiddenFieldDescrizioneDestUsoRL').value = '" & Replace(dataItem("DESCRIZIONE").Text, "'", "\'") & "';")
        End If
    End Sub

    Protected Sub dgvDestUsoRL_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDestUsoRL.NeedDataSource
        Try
            connData.apri(False)
            par.cmd.CommandText = "SELECT ID, DESCRIZIONE FROM SISCOM_MI.DESTINAZIONI_USO_RL_UI"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSalvaDestUsoRL_Click(sender As Object, e As System.EventArgs) Handles btnSalvaDestUsoRL.Click
        Try
            connData.apri(False)
            If Not String.IsNullOrEmpty(txtDestUsoRL.Text) Then
                par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.DESTINAZIONI_USO_RL_UI WHERE UPPER(DESCRIZIONE) = " & par.insDbValue(txtDestUsoRL.Text.ToUpper, True)
                Dim numOccorrenze As Integer = par.cmd.ExecuteScalar
                If numOccorrenze = 0 Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.DESTINAZIONI_USO_RL_UI " _
                    & "(ID,DESCRIZIONE) VALUES (SISCOM_MI.SEQ_DESTINAZIONI_USO_RL_UI.NEXTVAL, " & par.insDbValue(txtDestUsoRL.Text, True) & ")"
                    par.cmd.ExecuteNonQuery()
                    dgvDestUsoRL.Rebind()
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType, "exit", "document.getElementById('btnChiudiDestUsoRL').click();", True)
                Else
                    RadWindowManager1.RadAlert("La destinazione d\'uso RL che si vuol inserire è già presente nel sistema!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Impossibile salvare una destinazione d\'uso RL vuota! <br />Valorizzare il campo e riprovare.", 300, 150, "Attenzione", "", "null")
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
            connData.chiudi(True)
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvDestUsoRL.AllowPaging = False
        dgvDestUsoRL.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvDestUsoRL.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                If Not col.UniqueName = "DeleteColumn" Then
                    dtRecords.Columns.Add(colString)
                End If
            End If
        Next
        For Each row As GridDataItem In dgvDestUsoRL.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvDestUsoRL.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    If Not col.UniqueName = "DeleteColumn" Then
                        dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                    End If
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvDestUsoRL.Columns
            If col.Visible = True Then
                If Not col.UniqueName = "DeleteColumn" Then
                    Dim colString As String = col.HeaderText
                    dtRecords.Columns(i).ColumnName = colString
                    i += 1
                End If
            End If
        Next
        Esporta(dtRecords)
        dgvDestUsoRL.AllowPaging = True
        dgvDestUsoRL.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvDestUsoRL.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "DestinazioniUsoRL", "DestinazioniUsoRL", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Protected Sub btnAbbinamento_Click(sender As Object, e As System.EventArgs) Handles btnAbbinamento.Click
        If HiddenFieldIdDestUsoRL.Value <> "-1" Then
            MultiViewRicerca.SelectedIndex = 1
            dgvUnitaImmobiliari.Columns(1).Visible = True
            btnConfermaAbbinamento.Visible = True
            lblDestinazioneRL.Text = "Hai selezionato la destinazione d' uso RL <strong>" & HiddenFieldDescrizioneDestUsoRL.Value & "</strong>"
            dgvUnitaImmobiliari.Rebind()
        Else
            RadWindowManager1.RadAlert("Impossibile procedere!<br />Selezionare una destinazione d\'uso prima di effettuare l\'abbinamento.", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        End If
    End Sub

    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        ' MultiViewRicerca.ActiveViewIndex = 0
        MultiViewRicerca.SelectedIndex = 0
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
    End Sub

  

    Protected Sub dgvUnitaImmobiliari_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvUnitaImmobiliari.ItemDataBound
        If TypeOf e.Item Is GridFilteringItem Then
            par.caricaComboTelerik("SELECT distinct DESCRIZIONE FROM  SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE, siscom_mi.unita_immobiliari, siscom_mi.tipologia_Contratto_locazione " _
                                & "WHERE     RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                                & "AND tipologia_Contratto_locazione.cod = RAPPORTI_UTENZA.cod_tipologia_Contr_loc " _
                                & "AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID  " _
                                & "AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE) AND unita_immobiliari.ID_UNITA_PRINCIPALE IS NULL ORDER BY DESCRIZIONE ASC ", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoContratto"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro As New RadComboBoxItem
            altro.Value = "Non definito"
            altro.Text = "Non definito"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoContratto"), RadComboBox).Items.Add(altro)
            par.caricaComboTelerik("SELECT DISTINCT (CASE WHEN     RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 THEN 'ERP Sociale' " _
                                & "WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 12 THEN 'CANONE CONVENZ.' " _
                                & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 8 THEN 'ART.22 C.10 RR 1/2004' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 10 THEN 'FORZE DELL''ORDINE' " _
                                & "WHEN RAPPORTI_UTENZA.DEST_USO = 'C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO = 'A' THEN 'Associazioni' WHEN RAPPORTI_UTENZA.DEST_USO = 'Y' THEN 'Comodati d''uso' " _
                                & "WHEN RAPPORTI_UTENZA.DEST_USO = 'P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO = 'D' THEN 'Deroghe 431/98 (ART.15 R.R.1/2004)' WHEN RAPPORTI_UTENZA.DEST_USO = 'V' THEN 'Deroghe 431/98 (ART.15 C.2 R.R.1/2004)' " _
                                & "WHEN RAPPORTI_UTENZA.DEST_USO = 'S' THEN '431/98 Speciali' WHEN RAPPORTI_UTENZA.DEST_USO = '0' THEN  'Standard' END) AS DESCRIZIONE  " _
                                & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE, SISCOM_MI.UNITA_IMMOBILIARI  " _
                                & "WHERE     RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO  " _
                                & "AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                & "AND RAPPORTI_UTENZA.DEST_USO  IS NOT NULL AND RAPPORTI_UTENZA.PROVENIENZA_ASS IS NOT NULL " _
                                & "AND (CASE WHEN     RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 THEN 'ERP Sociale' " _
                                & "WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 12 THEN 'CANONE CONVENZ.' " _
                                & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 8 THEN 'ART.22 C.10 RR 1/2004' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 10 THEN 'FORZE DELL''ORDINE' " _
                                & "WHEN RAPPORTI_UTENZA.DEST_USO = 'C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO = 'A' THEN 'Associazioni' WHEN RAPPORTI_UTENZA.DEST_USO = 'Y' THEN 'Comodati d''uso' " _
                                & "WHEN RAPPORTI_UTENZA.DEST_USO = 'P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO = 'D' THEN 'Deroghe 431/98 (ART.15 R.R.1/2004)' WHEN RAPPORTI_UTENZA.DEST_USO = 'V' THEN 'Deroghe 431/98 (ART.15 C.2 R.R.1/2004)' " _
                                & "WHEN RAPPORTI_UTENZA.DEST_USO = 'S' THEN '431/98 Speciali' WHEN RAPPORTI_UTENZA.DEST_USO = '0' THEN  'Standard' END) IS NOT NULL " _
                                & "ORDER BY 1 ASC  ", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoContrattoSpecifico"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro1 As New RadComboBoxItem
            altro1.Value = "Non definito"
            altro1.Text = "Non definito"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoContrattoSpecifico"), RadComboBox).Items.Add(altro1)
            Dim altro2 As New RadComboBoxItem
            altro2.Value = "Non definito"
            altro2.Text = "Non definito"
            par.caricaComboTelerik("select DISTINCT descrizione from siscom_mi.destinazioni_uso_rl_ui, SISCOM_MI.UNITA_IMMOBILIARI WHERE SISCOM_MI.UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO_RL = DESTINAZIONI_USO_RL_UI.ID " _
                                    & "AND UNITA_IMMOBILIARI.ID IN (SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE) AND ID_UNITA_PRINCIPALE IS NULL ORDER BY DESCRIZIONE ASC ", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoDestinazioneRL"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoDestinazioneRL"), RadComboBox).Items.Add(altro2)
            par.caricaComboTelerik(" SELECT DISTINCT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI,SISCOM_MI.UNITA_IMMOBILIARI " _
                                 & " WHERE TIPOLOGIA_UNITA_IMMOBILIARI.COD = UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                 & " AND UNITA_IMMOBILIARI.ID IN  " _
                                 & " (SELECT ID_UNITA  " _
                                 & " FROM SISCOM_MI.UNITA_CONTRATTUALE) " _
                                 & " AND ID_UNITA_PRINCIPALE IS NULL " _
                                 & " ORDER BY DESCRIZIONE ASC ", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipologiaUI"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")


            Dim altro3 As New RadComboBoxItem
            altro3.Value = "Tutti"
            altro3.Text = "Tutti"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStatoContratto"), RadComboBox).Items.Add(altro3)
            Dim altro3a As New RadComboBoxItem
            altro3a.Value = "Attivo"
            altro3a.Text = "Attivo"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStatoContratto"), RadComboBox).Items.Add(altro3a)
            Dim altro3b As New RadComboBoxItem
            altro3b.Value = "Chiuso"
            altro3b.Text = "Chiuso"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStatoContratto"), RadComboBox).Items.Add(altro3b)
            Dim altro3c As New RadComboBoxItem
            altro3c.Value = "Non definito"
            altro3c.Text = "Non definito"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStatoContratto"), RadComboBox).Items.Add(altro3c)



            If Not String.IsNullOrEmpty(Trim(HFFiltrotipocontratto.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoContratto"), RadComboBox).SelectedValue = HFFiltrotipocontratto.Value.ToString
            End If
            If Not String.IsNullOrEmpty(Trim(HFFiltrotipocontrattoSpecifico.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoContrattoSpecifico"), RadComboBox).SelectedValue = HFFiltrotipocontrattoSpecifico.Value.ToString
            End If
            If Not String.IsNullOrEmpty(Trim(HFFiltroDestUsoRL.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipoDestinazioneRL"), RadComboBox).SelectedValue = HFFiltroDestUsoRL.Value.ToString
            End If
            If Not String.IsNullOrEmpty(Trim(HFFiltroTipologiaUI.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroTipologiaUI"), RadComboBox).SelectedValue = HFFiltroTipologiaUI.Value.ToString
            End If
            If Not String.IsNullOrEmpty(Trim(HFFiltroStatoContratto.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroStatoContratto"), RadComboBox).SelectedValue = HFFiltroStatoContratto.Value.ToString
        End If
        End If
    End Sub

    Protected Sub dgvUnitaImmobiliari_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvUnitaImmobiliari.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_UNITA")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            'CheckBox()
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub EsportaUnita_Click(sender As Object, e As System.EventArgs)
        dgvUnitaImmobiliari.AllowPaging = False
        dgvUnitaImmobiliari.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvUnitaImmobiliari.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                If Not col.ColumnType = "GridTemplateColumn" Then
                    If Not col.UniqueName = "DeleteColumn" Then
                        dtRecords.Columns.Add(colString)
                    End If
                End If
            End If
        Next
        For Each row As GridDataItem In dgvUnitaImmobiliari.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvUnitaImmobiliari.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    If Not col.ColumnType = "GridTemplateColumn" Then
                        If Not col.UniqueName = "DeleteColumn" Then
                            dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                        End If
                    End If
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvUnitaImmobiliari.Columns
            If col.Visible = True Then
                If Not col.ColumnType = "GridTemplateColumn" Then
                    If Not col.UniqueName = "DeleteColumn" Then
                        Dim colString As String = col.HeaderText
                        dtRecords.Columns(i).ColumnName = colString
                        i += 1
                    End If
                End If
            End If
        Next
        EsportaUnita(dtRecords)
        dgvUnitaImmobiliari.AllowPaging = True
        dgvUnitaImmobiliari.Rebind()
    End Sub
    Protected Sub RefreshUnita_Click(sender As Object, e As System.EventArgs)
        dgvUnitaImmobiliari.Rebind()
    End Sub
    Private Sub EsportaUnita(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "DestinazioniUsoRL", "DestinazioniUsoRL", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub


    Protected Sub CheckBox()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_UNITA"), Data.DataTable)
            Dim row As Data.DataRow
            For i As Integer = 0 To dgvUnitaImmobiliari.Items.Count - 1
                If DirectCast(dgvUnitaImmobiliari.Items(i).FindControl("CheckBox1"), CheckBox).Checked = False Then
                    row = dt.Select("id = " & dgvUnitaImmobiliari.Items(i).Cells(2).Text)(0)
                    row.Item("CHECKALL") = "FALSE"
                Else
                    row = dt.Select("id = " & dgvUnitaImmobiliari.Items(i).Cells(2).Text)(0)
                    row.Item("CHECKALL") = "TRUE"
                End If
            Next
            Session.Item("DT_UNITA") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - CheckBox - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnConfermaAbbinamento_Click(sender As Object, e As System.EventArgs) Handles btnConfermaAbbinamento.Click
        Try
            isExporting.Value = "1"
            CheckBox()
            Dim DT As Data.DataTable = Session.Item("dt_unita")
            Dim continua As Boolean = False
            For Each riga As Data.DataRow In DT.Rows
                If riga("CHECKALL") = True Then
                    continua = True
                    Exit For
                End If
            Next
            If continua Then
                connData.apri(False)
            
                Dim i As Integer = 0
                Dim dtFiltrata As New Data.DataTable
                Dim view As New Data.DataView(dt)
                'selezione bollette scadute, se non trovo scadute prendo le non scadute
                view.RowFilter = "CHECKALL = 'TRUE'"
                dtFiltrata = view.ToTable

                For Each riga As Data.DataRow In dtFiltrata.Rows

                    par.cmd.CommandText = "UPDATE SISCOM_MI.UNITA_IMMOBILIARI " _
                                         & "SET ID_DESTINAZIONE_USO_RL = " & HiddenFieldIdDestUsoRL.Value _
                                         & " WHERE ID = " & par.IfEmpty(riga.Item("ID").ToString, -1)
                    par.cmd.ExecuteNonQuery()
                    Dim motivazione As String = "DESTINAZIONE D'USO RL IMPOSTATA A: " & HiddenFieldDescrizioneDestUsoRL.Value.ToUpper
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_UI (ID_UI,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & par.IfEmpty(riga.Item("ID").ToString, -1) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02'," & par.insDbValue(motivazione, True) & ")"
                    par.cmd.ExecuteNonQuery()

                Next
                connData.chiudi(True)
                CaricaGridUnita()
                dgvUnitaImmobiliari.Rebind()
                RadNotificationNote.Text = "Operazione completata correttamente!"
                RadNotificationNote.Show()
                HiddenCheck.Value = "0"
            Else
                RadWindowManager1.RadAlert("Impossibile procedere! <br />Selezionare almeno un elemento da abbinare.", 300, 150, "Attenzione", "", "null")
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
            End If

        Catch ex As Exception

        End Try
    End Sub



    Protected Sub dgvUnitaImmobiliari_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvUnitaImmobiliari.PageIndexChanged
        CheckBox()
        dgvUnitaImmobiliari.CurrentPageIndex = e.NewPageIndex
    End Sub

    Private Sub CaricaGridUnita()
        par.cmd.CommandText = "SELECT DISTINCT UNITA_IMMOBILIARI.ID,'FALSE' AS CHECKALL, 'FALSE' AS CHECK1, UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UI, " _
                            & "nvl((SELECT DESCRIZIONE FROM SISCOM_MI.DESTINAZIONI_USO_RL_UI WHERE ID IN (SELECT ID_DESTINAZIONE_USO_RL FROM SISCOM_MI.UNITA_IMMOBILIARI UNI WHERE UNI.ID = UNITA_IMMOBILIARI.ID)), 'Non definito') AS DESTINAZIONE_USO_RL, " _
                            & "nvl((SELECT tipologia_Contratto_locazione.descrizione FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE, siscom_mi.tipologia_Contratto_locazione  " _
                            & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO  " _
                            & "AND tipologia_Contratto_locazione.cod = RAPPORTI_UTENZA.cod_tipologia_Contr_loc AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                            & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND RAPPORTI_UTENZA.ID IN  " _
                            & "(SELECT MAX (A.ID) FROM SISCOM_MI.RAPPORTI_UTENZA A, SISCOM_MI.tipologia_Contratto_locazione C, SISCOM_MI.UNITA_CONTRATTUALE B WHERE     A.ID = B.ID_CONTRATTO  " _
                            & "AND B.ID_UNITA = UNITA_IMMOBILIARI.ID AND C.cod = A.cod_tipologia_Contr_loc AND B.ID_UNITA_PRINCIPALE IS NULL)),'Non definito') AS TIPO_CONTRATTO,  " _
                            & "NVL ((SELECT (CASE WHEN     RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 THEN 'ERP Sociale'  " _
                            & "WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' " _
                            & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 12 THEN 'CANONE CONVENZ.' " _
                            & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 8 THEN 'ART.22 C.10 RR 1/2004' " _
                            & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 10 THEN 'FORZE DELL''ORDINE' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'C' THEN 'Cooperative' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'A' THEN 'Associazioni' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'Y' THEN 'Comodati d''uso' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'P' THEN '431 P.O.R.' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'D' THEN 'Deroghe 431/98 (ART.15 R.R.1/2004)' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'V' THEN 'Deroghe 431/98 (ART.15 C.2 R.R.1/2004)' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = 'S' THEN '431/98 Speciali' " _
                            & "WHEN RAPPORTI_UTENZA.DEST_USO = '0' THEN 'Standard' " _
                            & "END) FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE " _
                            & "WHERE     RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO " _
                            & "AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                            & "AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                            & "AND RAPPORTI_UTENZA.ID IN (SELECT MAX (A.ID) " _
                            & "FROM SISCOM_MI.RAPPORTI_UTENZA A,SISCOM_MI.UNITA_CONTRATTUALE B  " _
                            & "WHERE     A.ID = B.ID_CONTRATTO " _
                            & "AND B.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                            & "AND B.ID_UNITA_PRINCIPALE IS NULL)),  " _
                            & "'Non definito') AS TIPO_SPECIFICO, " _
                            & "NVL ( (SELECT (CASE WHEN RAPPORTI_UTENZA.DATA_RICONSEGNA IS NULL THEN 'Attivo' else 'Chiuso'" _
                            & " END) FROM SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.UNITA_CONTRATTUALE" _
                            & " WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO" _
                            & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID" _
                            & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL" _
                            & " AND RAPPORTI_UTENZA.ID IN" _
                            & " (SELECT MAX (A.ID)" _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA A," _
                            & " SISCOM_MI.UNITA_CONTRATTUALE B" _
                            & " WHERE A.ID = B.ID_CONTRATTO" _
                            & " AND B.ID_UNITA = UNITA_IMMOBILIARI.ID" _
                            & " AND B.ID_UNITA_PRINCIPALE IS NULL)),'Non definito') AS STATO_CONTRATTO," _
                            & "(SELECT descrizione  FROM siscom_mi.tipologia_unita_immobiliari WHERE tipologia_unita_immobiliari.cod = UNITA_IMMOBILIARI.COD_TIPOLOGIA) AS tipologia_ui, " _
                            & "INDIRIZZI.DESCRIZIONE AS INDIRIZZO, INDIRIZZI.CIVICO, (SELECT NOME FROM COMUNI_NAZIONI WHERE COD = INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA, " _
                            & "unita_immobiliari.interno, scale_edifici.descrizione AS scala, tipo_livello_piano.descrizione AS piano FROM siscom_mi.scale_edifici, siscom_mi.tipo_livello_piano, " _
                            & "SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI WHERE     scale_edifici.ID(+) = unita_immobiliari.id_scala AND tipo_livello_piano.cod(+) = unita_immobiliari.cod_tipo_livello_piano " _
                            & "AND EDIFICI.ID(+) = UNITA_IMMOBILIARI.ID_EDIFICIO  AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+)  AND unita_immobiliari.id_edificio <> 1 and unita_immobiliari.id <> 1 AND id_unita_principale IS NULL ORDER BY UNITA_IMMOBILIARI.ID "
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_UNITA") = dt
    End Sub

    Protected Sub chkSelTutti_CheckedChanged(sender As Object, e As System.EventArgs)
        dgvUnitaImmobiliari.AllowPaging = False
        dgvUnitaImmobiliari.Rebind()
        If HiddenCheck.Value = "0" Then
            HiddenCheck.Value = "1"
            For i As Integer = 0 To dgvUnitaImmobiliari.Items.Count - 1
                DirectCast(dgvUnitaImmobiliari.Items(i).FindControl("CheckBox1"), CheckBox).Checked = True
            Next
        Else
            HiddenCheck.Value = "0"
            For i As Integer = 0 To dgvUnitaImmobiliari.Items.Count - 1
                DirectCast(dgvUnitaImmobiliari.Items(i).FindControl("CheckBox1"), CheckBox).Checked = False
            Next
        End If
        CheckBox()
        Dim int As Integer = dgvUnitaImmobiliari.Items.Count
        dgvUnitaImmobiliari.AllowPaging = True
        dgvUnitaImmobiliari.Rebind()
    End Sub

    'Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs)
    '    CheckBox()
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
    'End Sub

    Protected Sub btnChiudiDestUsoRL_Click(sender As Object, e As System.EventArgs) Handles btnChiudiDestUsoRL.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
    End Sub

    Protected Sub btnVisualizzaUnita_Click(sender As Object, e As System.EventArgs) Handles btnVisualizzaUnita.Click
        MultiViewRicerca.SelectedIndex = 1
        btnConfermaAbbinamento.Visible = False
        lblDestinazioneRL.Text = ""
        dgvUnitaImmobiliari.Columns(1).Visible = False
        dgvUnitaImmobiliari.Rebind()
       
    End Sub
End Class
