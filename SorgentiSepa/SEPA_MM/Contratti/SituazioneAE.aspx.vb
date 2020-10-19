Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing
Imports System.Data

Partial Class Contratti_SituazioneAE
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Altezza As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
        End If
        Dim Str As String = ""

        If Not IsPostBack Then
            '            BindGrid()
            cmbAnno.Items.Add("(nessun filtro)")
            For ii = 0 To 20
                cmbAnno.Items.Add(CStr(Year(Now) - ii))
            Next

            cmbMese.Items.Add("(nessun filtro)")
            cmbMese.Items.Add("Gennaio")
            cmbMese.Items.Add("Febbraio")
            cmbMese.Items.Add("Marzo")
            cmbMese.Items.Add("Aprile")
            cmbMese.Items.Add("Maggio")
            cmbMese.Items.Add("Giugno")
            cmbMese.Items.Add("Luglio")
            cmbMese.Items.Add("Agosto")
            cmbMese.Items.Add("Settembre")
            cmbMese.Items.Add("Ottobre")
            cmbMese.Items.Add("Novembre")
            cmbMese.Items.Add("Dicembre")

            Dim anno As String = Request.QueryString("ANNO")
            Dim mese As String = Request.QueryString("MESE")

            Dim item As RadComboBoxItem
            If anno = "0" Then
                item = cmbAnno.FindItemByText("(nessun filtro)")
            Else
                item = cmbAnno.FindItemByText(anno)
            End If
            item.Selected = True

            If mese = "0" Then
                item = cmbMese.FindItemByText("(nessun filtro)")
                item.Selected = True
            Else
                cmbMese.SelectedIndex = mese
            End If

            BindGrid()
        End If
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvDocumenti.Rebind()
    End Sub
    Private Sub BindGrid()
        'TO_DATE(SUBSTR(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,1,8),'yyyymmdd') AS DATA_GENERAZIONE

        Dim lAddwhere As String = " (1=1) "

        If Not cmbAnno.Text Is Nothing Then
            If cmbAnno.Text <> "(nessun filtro)" And cmbAnno.Text <> "" Then
                lAddwhere += " AND (case when RAPPORTI_UTENZA_IMPOSTE.ANNO Is null then substr(DATA_CREAZIONE,1,4) else to_char(anno) end) ='" & cmbAnno.Text & "' "
            ElseIf cmbAnno.Text = "" And Request.QueryString("ANNO") <> "0" Then
                lAddwhere += " AND (case when RAPPORTI_UTENZA_IMPOSTE.ANNO Is null then substr(DATA_CREAZIONE,1,4) else to_char(anno) end) ='" & Request.QueryString("ANNO") & "' "
            End If
        Else
            lAddwhere += " AND (case when RAPPORTI_UTENZA_IMPOSTE.ANNO Is null then substr(DATA_CREAZIONE,1,4) else to_char(anno) end) ='" & Year(Now) & "' "
        End If

        If Not cmbMese.Text Is Nothing Then
            If cmbMese.Text <> "(nessun filtro)" And cmbMese.Text <> "" Then
                lAddwhere += "AND substr(DATA_CREAZIONE,5,2) = '" & cmbMese.SelectedIndex.ToString.PadLeft(2, "0") & "' "
            ElseIf cmbMese.Text = "" And Request.QueryString("MESE") <> "0" Then
                lAddwhere += "AND substr(DATA_CREAZIONE,5,2) = '" & Request.QueryString("MESE").PadLeft(2, "0") & "' "
            End If
        End If

        sStrSql = " SELECT " _
                & " TAB_FASI_REGISTRAZIONE.DESCRIZIONE AS STATO, " _
                & " RAPPORTI_UTENZA.COD_CONTRATTO," _
                & " REPLACE(SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID),',','') AS INTESTATARIO, " _
                & " (case when length(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE)=8 then to_date(SUBSTR(RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,1,8),'yyyymmdd') " _
                & " else TO_DATE(DATA_CREAZIONE,'YYYYMMDDHH24MISS') end) " _
                & " AS DATA_GENERAZIONE,  " _
                & " RAPPORTI_UTENZA_IMPOSTE.DATA_CREAZIONE,(case when RAPPORTI_UTENZA_IMPOSTE.ANNO Is null then substr(DATA_CREAZIONE,1,4) else to_char(anno) end) as anno, RAPPORTI_UTENZA_IMPOSTE.COD_TRIBUTO, " _
                & " (CASE WHEN RAPPORTI_UTENZA_IMPOSTE.COD_TRIBUTO='107T' THEN 'PRIMA REGISTR.' WHEN RAPPORTI_UTENZA_IMPOSTE.COD_TRIBUTO='112T' THEN 'ANN.SUCCESSIVA'" _
                & " WHEN RAPPORTI_UTENZA_IMPOSTE.COD_TRIBUTO='113T' THEN 'RISOLUZIONE'" _
                & " WHEN RAPPORTI_UTENZA_IMPOSTE.COD_TRIBUTO='114T' THEN 'PROROGA'" _
                & " WHEN RAPPORTI_UTENZA_IMPOSTE.COD_TRIBUTO='115T' THEN 'PRIMA REGISTR.' END) TIPO_MOVIMENTO," _
                & " (CASE" _
                & " WHEN data_Decorrenza_ae = data_decorrenza" _
                & "      AND reg_telematica IS NOT NULL" _
                & " THEN" _
                & "    'REGISTRATO'" _
                & " WHEN     data_Decorrenza_ae <> data_decorrenza" _
                & "      AND reg_telematica IS NOT NULL" _
                & " THEN" _
                & "    'RI-REGISTRATO'" _
                & " WHEN reg_telematica IS NULL" _
                & " THEN" _
                & " ''" _
                & "  END)" _
                & " AS modalita," _
                & " TO_NUMBER(RAPPORTI_UTENZA_IMPOSTE.IMPORTO_CANONE) AS IMPORTO_CANONE, " _
                & " TO_NUMBER(RAPPORTI_UTENZA_IMPOSTE.IMPORTO_TRIBUTO) AS importo_registro, " _
                & " (SELECT IMPOSTA_BOLLO FROM SISCOM_MI.DATI_GENERALI_RLI WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                & " DATI_GENERALI_RLI.ID =(SELECT MAX(ID) FROM SISCOM_MI.DATI_GENERALI_RLI WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID ) ) AS IMP_BOLLO," _
                & " TO_NUMBER(RAPPORTI_UTENZA_IMPOSTE.IMPORTO_SANZIONE) AS IMPORTO_SANZIONE, " _
                & " TO_NUMBER(RAPPORTI_UTENZA_IMPOSTE.GIORNI_SANZIONE) AS GIORNI_SANZIONE, " _
                & " TO_NUMBER(RAPPORTI_UTENZA_IMPOSTE.IMPORTO_INTERESSI) AS IMPORTO_INTERESSI, " _
                & " (case when (select count(bol_Bollette_voci.id) from siscom_mi.bol_Bollette_voci " _
                & " where id_voce=93 and bol_Bollette_voci.id_bolletta in (select bol_bollette.id from siscom_mi.bol_bollette  " _
                & " where id_bolletta_Storno is null and bol_bollette.id_contratto=RAPPORTI_UTENZA.id))>0 then 'SI' else 'NO' end)  " _
                & " as REGISTRO_RIBALTATO_BOLLETTA , (case when (select count(id) from siscom_mi.bol_Bollette_voci  " _
                & " where id_voce=8 and bol_Bollette_voci.id_bolletta in (select id from siscom_mi.bol_bollette where  " _
                & " /*id_tipo=10 and*/ id_bolletta_Storno is null and bol_bollette.id_contratto=RAPPORTI_UTENZA.id))>0 then 'SI' else 'NO' end) " _
                & "  as BOLLO_RIBALTATO_BOLLETTA , " _
                & " RAPPORTI_UTENZA_IMPOSTE.FILE_SCARICATO,TAB_FILIALI.NOME AS SEDE_TERR " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE, SISCOM_MI.RAPPORTI_UTENZA, SISCOM_MI.TAB_FASI_REGISTRAZIONE, SISCOM_MI.TAB_FILIALI," _
                & " SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI " _
                & " WHERE RAPPORTI_UTENZA.ID = RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO And TAB_FASI_REGISTRAZIONE.ID = RAPPORTI_UTENZA_IMPOSTE.ID_FASE_REGISTRAZIONE  " _
                & " AND DATA_DECORRENZA_AE>='20141201'" _
                               & " and complessi_immobiliari.id_filiale = tab_filiali.ID(+)" _
                               & " and edifici.id_complesso = complessi_immobiliari.ID" _
                               & " AND edifici.ID = unita_immobiliari.id_edificio " _
                               & " And rapporti_utenza.ID = unita_contrattuale.id_contratto " _
                               & " And unita_contrattuale.id_unita = unita_immobiliari.ID " _
                               & " AND unita_contrattuale.id_unita_principale is null " _
                               & " AND  " & lAddwhere _
                & " ORDER BY nvl(DATA_CREAZIONE,'19000101') DESC,RAPPORTI_UTENZA_IMPOSTE.ID_CONTRATTO ASC"

    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvDocumenti.AllowPaging = False
        dgvDocumenti.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvDocumenti.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvDocumenti.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvDocumenti.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvDocumenti.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvDocumenti.AllowPaging = True
        dgvDocumenti.Rebind()
    End Sub

    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSituazioneAE", "SituazioneAE", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('');</script>")
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property


    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDocumenti.NeedDataSource
        Try
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Contratti - Situazione Registrazione AE - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub btnAvviaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnAvviaRicerca.Click
        BindGrid()
        dgvDocumenti.Rebind()
    End Sub

End Class
