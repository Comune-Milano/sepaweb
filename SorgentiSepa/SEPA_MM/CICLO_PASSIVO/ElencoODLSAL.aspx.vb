Imports Telerik.Web.UI
Imports System.Data
Imports System.IO

Partial Class CICLO_PASSIVO_CicloPassivo_ElencoODLSAL
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim sStringaSqlFiltri As String
    Dim sStringaSqlFiltri1 As String
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGrid1.ClientID
        End If
    End Sub
    Public Property FiltroDettagli() As String
        Get
            If Not (ViewState("FiltroDettagli") Is Nothing) Then
                Return CStr(ViewState("FiltroDettagli"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("FiltroDettagli") = value
        End Set
    End Property
    Protected Sub RadGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        'If e.CommandName = RadGrid.FilterCommandName Then
        '    Dim filterPair As Pair = DirectCast(e.CommandArgument, Pair)
        '    Dim filterBox As TextBox = CType((CType(e.Item, GridFilteringItem))(filterPair.Second.ToString()).Controls(0), TextBox)
        '    FiltroDettagli &= CreaFiltroEspressione(filterPair.First, filterPair.Second, filterBox.Text)
        'End If
    End Sub

    Private Function CreaFiltroEspressione(tipoFiltro As Object, colonnaFiltro As Object, valoreFiltro As String) As String
        CreaFiltroEspressione = ""
        Try
            Select Case colonnaFiltro.ToString.ToUpper
                Case "FORNITORE"
                    Select Case tipoFiltro.ToString.ToUpper
                        Case "CONTAINS"
                            CreaFiltroEspressione &= " AND UPPER(RAGIONE_SOCIALE) LIKE '%" & par.PulisciStrSql(valoreFiltro.ToUpper) & "%'"
                        Case "EQUAL TO"
                            CreaFiltroEspressione &= " AND UPPER(RAGIONE_SOCIALE) = '" & par.PulisciStrSql(valoreFiltro.ToUpper) & "'"
                    End Select
            End Select
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private gridMessage1 As String = Nothing
    Private gridMessage2 As String = Nothing
    Protected Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        If TypeOf e.Item Is GridFilteringItem Then
            Dim fitem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
            For Each col As GridColumn In RadGrid1.MasterTableView.RenderColumns
                If col.ColumnType = "GridDateTimeColumn" And col.UniqueName = "ORDERDATE" Then
                    Dim LiteralFrom As LiteralControl = DirectCast(fitem(col.UniqueName).Controls(0), LiteralControl)
                    LiteralFrom.Text = "Dal "
                    'From text
                    Dim LiteralTo As LiteralControl = DirectCast(fitem(col.UniqueName).Controls(3), LiteralControl)
                    'To text
                    LiteralTo.Text = "Al "
                End If
            Next
            par.caricaComboTelerik("SELECT DISTINCT  (CASE WHEN (ABS((SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
               & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) - NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) < 1 " _
               & " AND " _
               & " ABS((SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
               & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) - NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) > 0 )" _
               & " THEN 'COMPLETO' " _
               & " WHEN (SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
               & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) >= NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) " _
               & " THEN 'COMPLETO' " _
               & " When NVL((Select SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) = 0 Then 'DA LIQUIDARE' " _
               & " ELSE 'PARZIALE' END " _
               & " ) AS DESCRIZIONE " & sStringaSqlFiltri _
              & " order by   1 ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroLiquidazione"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro As New RadComboBoxItem
            altro.Value = "NON DEFINITO"
            altro.Text = "NON DEFINITO"
            'TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroLiquidazione"), RadComboBox).Items.Add(altro)
            If Not String.IsNullOrEmpty(Trim(HFFiltroEventoLiquidazione.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroLiquidazione"), RadComboBox).SelectedValue = HFFiltroEventoLiquidazione.Value.ToString
            End If
            par.caricaComboTelerik("SELECT DISTINCT DECODE(STATO_FIRMA,0,'NON FIRMATO',1,'FIRMATO CON RISERVA',2,'FIRMATO') AS DESCRIZIONE " & sStringaSqlFiltri _
              & " order by   1 ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoSal"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro1 As New RadComboBoxItem
            altro1.Value = "NON DEFINITO"
            altro1.Text = "NON DEFINITO"
            'TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoSal"), RadComboBox).Items.Add(altro1)
            If Not String.IsNullOrEmpty(Trim(HFFiltroEventoStatoSal.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoSal"), RadComboBox).SelectedValue = HFFiltroEventoStatoSal.Value.ToString
            End If


            par.caricaComboTelerik("SELECT DISTINCT (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) AS DESCRIZIONE " & sStringaSqlFiltri _
                & " UNION " _
                & " SELECT DISTINCT (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) AS DESCRIZIONE " & sStringaSqlFiltri1 _
                & " order by   1 ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStato"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro2 As New RadComboBoxItem
            altro2.Value = "NON DEFINITO"
            altro2.Text = "NON DEFINITO"
            'TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStato"), RadComboBox).Items.Add(altro2)
            If Not String.IsNullOrEmpty(Trim(HFFiltroEventoStato.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStato"), RadComboBox).SelectedValue = HFFiltroEventoStato.Value.ToString
            End If
        End If
    End Sub
    Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Try
            connData.apri(False)
            Dim Query As String = EsportaQueryODL()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            RadGrid1.DataSource = dt
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        RadGrid1.AllowPaging = False
        RadGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In RadGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In RadGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In RadGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    If col.UniqueName = "NUMERO_SAL" Then
                        dr(col.UniqueName) = row("NRSAL").Text.Replace("&nbsp;", "")
                    Else
                        dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                    End If
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In RadGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        RadGrid1.AllowPaging = True
        RadGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        RadGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ODLsuSAL", "ODLsuSAL", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
    Protected Sub EsportaConDett_Click(sender As Object, e As System.EventArgs)
        FiltroDettagli = RadGrid1.MasterTableView.GetEntitySqlFilterExpression()
        FiltroDettagli = FiltroDettagli.Replace("it.PROGRESSIVO", " PROGR_SAL ")
        FiltroDettagli = FiltroDettagli.Replace("it.FORNITORE", " RAGIONE_SOCIALE ")
        FiltroDettagli = FiltroDettagli.Replace("it.REPERTORIO", " NUM_REPERTORIO ")
        FiltroDettagli = FiltroDettagli.Replace("it.NUMERO_SAL", " nvl(PAGAMENTI.PROGR_APPALTO,0) ")
        FiltroDettagli = FiltroDettagli.Replace("it.ODL", " to_char(MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) ")
        FiltroDettagli = FiltroDettagli.Replace("it.STATO", " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) ")
        FiltroDettagli = FiltroDettagli.Replace("it.DATA_ORDINE", " TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') ").Replace(" 00:00'", "','yyyy-mm-dd')").Replace("DATETIME", "TO_DATE(")

        FiltroDettagli = FiltroDettagli.Replace("it.DATA_CONSUNTIVO", " TO_DATE(PRENOTAZIONI.DATA_CONSUNTIVAZIONE,'YYYYMMDD') ")
        FiltroDettagli = FiltroDettagli.Replace("it.UBICAZIONE", " UPPER((CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & "(SELECT    INDIRIZZI.TIPO_INDIRIZZO || ' ' || INDIRIZZI.DESCRIZIONE " _
                & "|| ' ' || CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID =  " _
                & "(SELECT COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                & "WHERE COMPLESSI_IMMOBILIARI.ID = manutenzioni.ID_COMPLESSO)) " _
                & "WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT    INDIRIZZI.TIPO_INDIRIZZO " _
                & "|| ' ' || INDIRIZZI.DESCRIZIONE || ' ' || CIVICO FROM SISCOM_MI.INDIRIZZI " _
                & "WHERE INDIRIZZI.ID = (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI " _
                & "WHERE EDIFICI.ID = MANUTENZIONI.ID_EDIFICIO)) ELSE NULL END)) ").Replace("""", "'")
        FiltroDettagli = FiltroDettagli.Replace("it.TOT_NETTO_DI_ONERI", " IMPONIBILE ")
        FiltroDettagli = FiltroDettagli.Replace("it.IVA", " IVA ")
        FiltroDettagli = FiltroDettagli.Replace("it.TOT_NETTO_DI_ONERI_E_IVA", " IMPORTO_APPROVATO ")

        FiltroDettagli = FiltroDettagli.Replace("it.TOT_LORDO_ESCLUSO_ONERI", " MAN_CALC_LORDO_E_ONERI ")
        FiltroDettagli = FiltroDettagli.Replace("it.SEDE_TERRITORIALE", " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND MANUTENZIONI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID) " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID AND MANUTENZIONI.ID_EDIFICIO=EDIFICI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO) " _
                & " ELSE NULL END) ").Replace("""", "'")
        FiltroDettagli = FiltroDettagli.Replace("it.BUILDING_MANAGER", " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & " /*(SELECT NULL SUBSTR(WM_CONCAT(OPERATORI.COGNOME||' '||OPERATORI.NOME),1,100) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI,SEPA.OPERATORI WHERE BUILDING_MANAGER_OPERATORI.ID=EDIFICI.ID_BM AND OPERATORI.ID=ID_OPERATORE AND COMPLESSI_IMMOBILIARI.ID=MANUTENZIONI.ID_COMPLESSO AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID)*/ " _
                & " NULL " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT OPERATORI.COGNOME||' '||OPERATORI.NOME FROM SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI,SEPA.OPERATORI WHERE BUILDING_MANAGER_OPERATORI.ID=EDIFICI.ID_BM AND OPERATORI.ID=ID_OPERATORE AND EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) " _
                & " ELSE NULL END) ").Replace("""", "'")
        If Not String.IsNullOrEmpty(FiltroDettagli) Then
            FiltroDettagli = " AND " & FiltroDettagli.ToUpper
        End If

        Dim stringa = "SELECT  1,MANUTENZIONI.ID," _
                & " PROGR_SAL AS PROGRESSIVO, " _
                & " RAGIONE_SOCIALE AS FORNITORE, " _
                & " to_char(NUM_REPERTORIO) AS REPERTORIO, " _
                & "(SELECT MAX(CODICE_PROGETTO_VISION) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS CODICE_PROGETTO_VISION, " _
                & "(SELECT MAX(NUMERO_SAL) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS NUMERO_SAL_VISION, " _
                & " TO_DATE (MANUTENZIONI.DATA_INIZIO_ORDINE, 'YYYYMMDD') AS DATA_INIZIO_ORDINE, " _
                & "TO_DATE (MANUTENZIONI.DATA_INIZIO_INTERVENTO, 'YYYYMMDD') AS DATA_INIZIO_INTERVENTO, TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO, 'YYYYMMDD') AS DATA_FINE_INTERVENTO, " _
                & " 'javascript:window.open('''||(CASE WHEN PAGAMENTI.TIPO_PAGAMENTO=3 THEN 'CicloPassivo/MANUTENZIONI/Sal' WHEN PAGAMENTI.TIPO_PAGAMENTO=7 THEN 'CicloPassivo/RRS/SAL_RRS' ELSE NULL END )||'.aspx?PROVENIENZA=CHIAMATA_DIRETTA&ID='||pagamenti.id||''',''_blank'',''resizable=no,height=700,width=1300,top=100,left=100,scrollbars=no'');void(0);' AS APRI_SAL, " _
                & " 'javascript:window.open('''||(CASE WHEN PAGAMENTI.TIPO_PAGAMENTO=3 THEN 'CicloPassivo/MANUTENZIONI/Manutenzioni' WHEN PAGAMENTI.TIPO_PAGAMENTO=7 THEN 'CicloPassivo/RRS/ManutenzioniRRS' ELSE NULL END )||'.aspx?PROVENIENZA=RICERCA_DIRETTA&ID=' || manutenzioni.id || ''',''_blank'',''resizable=no,height=700,width=1300,top=100,left=100,scrollbars=no'');void(0);' AS APRI_ODL, " _
                & " nvl(PAGAMENTI.PROGR_APPALTO,0) AS NUMERO_SAL, " _
                & " TO_DATE(PAGAMENTI.DATA_EMISSIONE,'YYYYMMDD') AS DATA_SAL, " _
                & " to_char(MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) AS ODL, " _
                & " TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') AS DATA_ORDINE, " _
                & " TO_DATE(PRENOTAZIONI.DATA_CONSUNTIVAZIONE,'YYYYMMDD') AS DATA_CONSUNTIVO, " _
                & " (SELECT A.TIPOLOGIA||'-'|| " _
                & " (CASE IMPIANTI.COD_TIPOLOGIA " _
                & " WHEN 'AN' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'CF' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'CI' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'EL' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'GA' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'ID' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'ME' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'SO' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - ' " _
                & " || (SELECT (CASE " _
                & " WHEN SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO " _
                & " IS NULL " _
                & " THEN " _
                & " 'Num. Matr. ' " _
                & " || SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                & " ELSE " _
                & " 'Num. Imp. ' " _
                & " || SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO " _
                & " END) " _
                & " FROM SISCOM_MI.I_SOLLEVAMENTO " _
                & " WHERE ID = IMPIANTI.ID) " _
                & " WHEN 'TA' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TE' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TR' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TU' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TV' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " ELSE " _
                & " '' " _
                & " END) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, SISCOM_MI.IMPIANTI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.ID_IMPIANTO IS NOT NULL " _
                & " AND A.ID_IMPIANTO = " _
                & " SISCOM_MI.IMPIANTI.ID(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'COMPLESSO' " _
                & " AND A.ID_COMPLESSO = " _
                & " SISCOM_MI.COMPLESSI_IMMOBILIARI.ID(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.EDIFICI.DENOMINAZIONE " _
                & " || ' - -Cod.' " _
                & " || SISCOM_MI.EDIFICI.COD_EDIFICIO) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, SISCOM_MI.EDIFICI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'EDIFICIO' " _
                & " AND A.ID_EDIFICIO = " _
                & " SISCOM_MI.EDIFICI.ID(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.EDIFICI.DENOMINAZIONE " _
                & " || ' - -Scala ' " _
                & " || (SELECT descrizione " _
                & " FROM siscom_mi.scale_edifici " _
                & " WHERE siscom_mi.scale_edifici.id = unita_immobiliari.id_scala) " _
                & " || ' - -Interno ' " _
                & " || SISCOM_MI.UNITA_IMMOBILIARI.INTERNO " _
                & " || ' - ' " _
                & " || SISCOM_MI.INTESTATARI_UI.INTESTATARIO) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, " _
                & " SISCOM_MI.EDIFICI, " _
                & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                & " SISCOM_MI.INTESTATARI_UI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'UNITA IMMOBILIARE' " _
                & " AND A.ID_UNITA_IMMOBILIARE = " _
                & " SISCOM_MI.UNITA_IMMOBILIARI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.INTESTATARI_UI.ID_UI(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE " _
                & " || ' - ' " _
                & " || SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, " _
                & " SISCOM_MI.UNITA_COMUNI, " _
                & " SISCOM_MI.TIPO_UNITA_COMUNE " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'UNITA COMUNE' " _
                & " AND A.ID_UNITA_COMUNE = " _
                & " SISCOM_MI.UNITA_COMUNI.ID(+) " _
                & " AND SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA = " _
                & " SISCOM_MI.TIPO_UNITA_COMUNE.COD(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.edifici.denominazione " _
                & " || ' - ' " _
                & " || siscom_mi.scale_edifici.descrizione) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, " _
                & " SISCOM_MI.EDIFICI, " _
                & " SISCOM_MI.SCALE_EDIFICI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'SCALA' " _
                & " AND A.ID_SCALA = " _
                & " SISCOM_MI.SCALE_EDIFICI.ID(+) " _
                & " AND SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID) " _
                & " AS UBICAZIONE, " _
                & " UPPER((CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & "(SELECT    INDIRIZZI.TIPO_INDIRIZZO || ' ' || INDIRIZZI.DESCRIZIONE " _
                & "|| ' ' || CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID =  " _
                & "(SELECT COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                & "WHERE COMPLESSI_IMMOBILIARI.ID = manutenzioni.ID_COMPLESSO)) " _
                & "WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT    INDIRIZZI.TIPO_INDIRIZZO " _
                & "|| ' ' || INDIRIZZI.DESCRIZIONE || ' ' || CIVICO FROM SISCOM_MI.INDIRIZZI " _
                & "WHERE INDIRIZZI.ID = (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI " _
                & "WHERE EDIFICI.ID = MANUTENZIONI.ID_EDIFICIO)) ELSE NULL END)) AS VIA," _
                & " IMPONIBILE AS TOT_NETTO_DI_ONERI,IVA AS IVA, " _
                & " (SELECT SUM(MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM siscom_mi.MANUTENZIONI_CONSUNTIVI WHERE MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI= manutenzioni_interventi.id AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO <>'RIMBORSO OPERE SPECIALISTICHE') as importo_consuntivo," _
                & " IMPORTO_APPROVATO AS TOT_NETTO_DI_ONERI_E_IVA, " _
                & " /*(SELECT SUM(MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI IN ( SELECT ID FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE' ))*/MANUTENZIONI.MAN_CALC_LORDO_E_ONERI AS TOT_LORDO_ESCLUSO_ONERI, " _
                & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND MANUTENZIONI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID) " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID AND MANUTENZIONI.ID_EDIFICIO=EDIFICI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO) " _
                & " ELSE NULL END) AS SEDE_TERRITORIALE, " _
                & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & " /*(SELECT NULL SUBSTR(WM_CONCAT(OPERATORI.COGNOME||' '||OPERATORI.NOME),1,100) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI,SEPA.OPERATORI WHERE BUILDING_MANAGER_OPERATORI.ID=EDIFICI.ID_BM AND OPERATORI.ID=ID_OPERATORE AND COMPLESSI_IMMOBILIARI.ID=MANUTENZIONI.ID_COMPLESSO AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID)*/ " _
                & " NULL " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT OPERATORI.COGNOME||' '||OPERATORI.NOME FROM SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI,SEPA.OPERATORI WHERE BUILDING_MANAGER_OPERATORI.ID=EDIFICI.ID_BM AND OPERATORI.ID=ID_OPERATORE AND EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) " _
                & " ELSE NULL END) " _
                & " AS BUILDING_MANAGER, " _
                & " (SELECT TAB_FILIALI.MAIL_REF_AMMINISTRATIVO FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID = ( " _
                & " (SELECT COMPLESSI_IMMOBILIARI.ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN ( " _
                & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN MANUTENZIONI.ID_COMPLESSO WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) ELSE 0 END) " _
                & " )))) AS MAIL_PER_APPUNTAMENTO,(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) AS STATO " _
                & " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                & " WHERE " _
                & " MANUTENZIONI.ID_APPALTO=APPALTI.ID " _
                & " AND APPALTI.ID_FORNITORE=FORNITORI.ID " _
                & " AND PAGAMENTI.ID=MANUTENZIONI.ID_PAGAMENTO " _
                & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                & " AND MANUTENZIONI.ID=MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE " _
                & " AND MANUTENZIONI.STATO NOT IN (5,6)" _
                & FiltroDettagli _
                & " UNION " _
                & " SELECT  1,MANUTENZIONI.ID,  " _
                & " PROGR_SAL AS PROGRESSIVO, " _
                & " RAGIONE_SOCIALE AS FORNITORE, " _
                & " to_char(NUM_REPERTORIO) AS REPERTORIO, " _
                & "(SELECT MAX(CODICE_PROGETTO_VISION) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS CODICE_PROGETTO_VISION, " _
                & "(SELECT MAX(NUMERO_SAL) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS NUMERO_SAL_VISION, " _
                & " TO_DATE (MANUTENZIONI.DATA_INIZIO_ORDINE, 'YYYYMMDD') AS DATA_INIZIO_ORDINE, " _
                & "TO_DATE (MANUTENZIONI.DATA_INIZIO_INTERVENTO, 'YYYYMMDD') AS DATA_INIZIO_INTERVENTO, TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO, 'YYYYMMDD') AS DATA_FINE_INTERVENTO, " _
                & " NULL AS APRI_SAL, " _
                & " NULL AS NUMERO_SAL, " _
                & " NULL AS DATA_SAL, " _
                & " to_char(MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) AS ODL, " _
                & " TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') AS DATA_ORDINE, " _
                & " TO_DATE(PRENOTAZIONI.DATA_CONSUNTIVAZIONE,'YYYYMMDD') AS DATA_CONSUNTIVO, " _
                & " (SELECT A.TIPOLOGIA||'-'|| " _
                & " (CASE IMPIANTI.COD_TIPOLOGIA " _
                & " WHEN 'AN' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'CF' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'CI' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'EL' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'GA' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'ID' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'ME' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'SO' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - ' " _
                & " || (SELECT (CASE " _
                & " WHEN SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO " _
                & " IS NULL " _
                & " THEN " _
                & " 'Num. Matr. ' " _
                & " || SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                & " ELSE " _
                & " 'Num. Imp. ' " _
                & " || SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO " _
                & " END) " _
                & " FROM SISCOM_MI.I_SOLLEVAMENTO " _
                & " WHERE ID = IMPIANTI.ID) " _
                & " WHEN 'TA' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TE' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TR' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TU' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " WHEN 'TV' " _
                & " THEN " _
                & " SISCOM_MI.IMPIANTI.DESCRIZIONE " _
                & " || ' - - Cod. ' " _
                & " || SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                & " ELSE " _
                & " '' " _
                & " END) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, SISCOM_MI.IMPIANTI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.ID_IMPIANTO IS NOT NULL " _
                & " AND A.ID_IMPIANTO = " _
                & " SISCOM_MI.IMPIANTI.ID(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'COMPLESSO' " _
                & " AND A.ID_COMPLESSO = " _
                & " SISCOM_MI.COMPLESSI_IMMOBILIARI.ID(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.EDIFICI.DENOMINAZIONE " _
                & " || ' - -Cod.' " _
                & " || SISCOM_MI.EDIFICI.COD_EDIFICIO) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, SISCOM_MI.EDIFICI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'EDIFICIO' " _
                & " AND A.ID_EDIFICIO = " _
                & " SISCOM_MI.EDIFICI.ID(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.EDIFICI.DENOMINAZIONE " _
                & " || ' - -Scala ' " _
                & " || (SELECT descrizione " _
                & " FROM siscom_mi.scale_edifici " _
                & " WHERE siscom_mi.scale_edifici.id = unita_immobiliari.id_scala) " _
                & " || ' - -Interno ' " _
                & " || SISCOM_MI.UNITA_IMMOBILIARI.INTERNO " _
                & " || ' - ' " _
                & " || SISCOM_MI.INTESTATARI_UI.INTESTATARIO) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, " _
                & " SISCOM_MI.EDIFICI, " _
                & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                & " SISCOM_MI.INTESTATARI_UI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'UNITA IMMOBILIARE' " _
                & " AND A.ID_UNITA_IMMOBILIARE = " _
                & " SISCOM_MI.UNITA_IMMOBILIARI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID(+) " _
                & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID = SISCOM_MI.INTESTATARI_UI.ID_UI(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE " _
                & " || ' - ' " _
                & " || SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, " _
                & " SISCOM_MI.UNITA_COMUNI, " _
                & " SISCOM_MI.TIPO_UNITA_COMUNE " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'UNITA COMUNE' " _
                & " AND A.ID_UNITA_COMUNE = " _
                & " SISCOM_MI.UNITA_COMUNI.ID(+) " _
                & " AND SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA = " _
                & " SISCOM_MI.TIPO_UNITA_COMUNE.COD(+) " _
                & " UNION " _
                & " SELECT A.TIPOLOGIA||'-'|| " _
                & " (   SISCOM_MI.edifici.denominazione " _
                & " || ' - ' " _
                & " || siscom_mi.scale_edifici.descrizione) " _
                & " AS DETTAGLIO " _
                & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI A, " _
                & " SISCOM_MI.EDIFICI, " _
                & " SISCOM_MI.SCALE_EDIFICI " _
                & " WHERE     A.ID_MANUTENZIONE = MANUTENZIONI.ID AND A.ID=MANUTENZIONI_INTERVENTI.ID " _
                & " AND A.TIPOLOGIA = 'SCALA' " _
                & " AND A.ID_SCALA = " _
                & " SISCOM_MI.SCALE_EDIFICI.ID(+) " _
                & " AND SISCOM_MI.SCALE_EDIFICI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID) " _
                & " AS UBICAZIONE, " _
                & " UPPER((CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & "(SELECT    INDIRIZZI.TIPO_INDIRIZZO || ' ' || INDIRIZZI.DESCRIZIONE " _
                & "|| ' ' || CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID =  " _
                & "(SELECT COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                & "WHERE COMPLESSI_IMMOBILIARI.ID = manutenzioni.ID_COMPLESSO)) " _
                & "WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT    INDIRIZZI.TIPO_INDIRIZZO " _
                & "|| ' ' || INDIRIZZI.DESCRIZIONE || ' ' || CIVICO FROM SISCOM_MI.INDIRIZZI " _
                & "WHERE INDIRIZZI.ID = (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI " _
                & "WHERE EDIFICI.ID = MANUTENZIONI.ID_EDIFICIO)) ELSE NULL END)) AS VIA," _
                & " IMPORTO_APPROVATO AS IMP_NETTO_DI_ONERI_E_IVA, " _
                & " IVA AS IVA, " _
                & " (SELECT SUM(MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM siscom_mi.MANUTENZIONI_CONSUNTIVI WHERE MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI= manutenzioni_interventi.id AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO <>'RIMBORSO OPERE SPECIALISTICHE') as importo_consuntivo," _
                & " IMPONIBILE AS IMP_NETTO_DI_ONERI, " _
                & " /*(SELECT SUM(MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI IN ( SELECT ID FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE' ))*/MANUTENZIONI.MAN_CALC_LORDO_E_ONERI AS TOT_LORDO_ESCLUSO_ONERI, " _
                & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND MANUTENZIONI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID) " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID AND MANUTENZIONI.ID_EDIFICIO=EDIFICI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO) " _
                & " ELSE NULL END) AS SEDE_TERRITORIALE, " _
                & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & " /*(SELECT NULL SUBSTR(WM_CONCAT(OPERATORI.COGNOME||' '||OPERATORI.NOME),1,100) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI,SEPA.OPERATORI WHERE BUILDING_MANAGER_OPERATORI.ID=EDIFICI.ID_BM AND OPERATORI.ID=ID_OPERATORE AND COMPLESSI_IMMOBILIARI.ID=MANUTENZIONI.ID_COMPLESSO AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID)*/ " _
                & " NULL " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT OPERATORI.COGNOME||' '||OPERATORI.NOME FROM SISCOM_MI.EDIFICI,SISCOM_MI.BUILDING_MANAGER_OPERATORI,SEPA.OPERATORI WHERE BUILDING_MANAGER_OPERATORI.ID=EDIFICI.ID_BM AND OPERATORI.ID=ID_OPERATORE AND EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) " _
                & " ELSE NULL END) " _
                & " AS BUILDING_MANAGER, " _
                & " (SELECT TAB_FILIALI.MAIL_REF_AMMINISTRATIVO FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID = ( " _
                & " (SELECT COMPLESSI_IMMOBILIARI.ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN ( " _
                & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN MANUTENZIONI.ID_COMPLESSO WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=MANUTENZIONI.ID_EDIFICIO) ELSE 0 END) " _
                & " )))) AS MAIL_PER_APPUNTAMENTO,(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) AS STATO " _
                & " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                & " WHERE " _
                & " MANUTENZIONI.ID_APPALTO=APPALTI.ID " _
                & " AND APPALTI.ID_FORNITORE=FORNITORI.ID " _
                & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                & " AND MANUTENZIONI.STATO=2 " _
                & " AND MANUTENZIONI.ID=MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE" _
                & FiltroDettagli _
                & " ORDER BY 2,1"
        Dim dt As Data.DataTable = par.getDataTableGrid(stringa)


        DataGridODL.DataSource = dt
        DataGridODL.DataBind()
        AggiustaDataGrid()
        Dim xls As New ExcelSiSol
        Dim nomeFile As String = xls.EsportaExcelDaDataGridParziale(ExcelSiSol.Estensione.Office2007_xlsx, "ExportPagamenti", "ExportPagamenti", DataGridODL, , , , 0, , True)
        'Dim xls As New ExcelSiSol
        'Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ODLsuSALDettagliato", "ODLsuSALDettagliato", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If

        RadGrid1.Rebind()
    End Sub
    Protected Sub DataGridODL_PreRender(sender As Object, e As System.EventArgs) Handles DataGridODL.PreRender
        'AggiustaDataGrid()
    End Sub

    Protected Sub AggiustaDataGrid()
        Dim idprecedente As String = ""
        Dim progressivoPrecedente As String = ""
        Dim fornitorePrecedente As String = ""
        Dim repertorioPrecedente As String = ""
        Dim numeroSalPrecedente As String = ""
        Dim dataSalPrecedente As String = ""
        Dim odlPrecedente As String = ""
        Dim dataOrdinePrecedente As String = ""
        Dim dataConsuntivoPrecedente As String = ""
        Dim ubicazionePrecedente As String = ""
        Dim totNettodiOneriPrecedente As String = ""
        Dim ivaPrecedente As String = ""
        Dim totNettoOneriEIvaPrecedente As String = ""
        Dim importoLordoPrecedente As String = ""
        Dim sedeTerritorialePrecedente As String = ""
        Dim buildingManagerPrecedente As String = ""
        Dim mailAppuntamentoPrecedente As String = ""
        Dim statoPrecedente As String = ""

        For i As Integer = DataGridODL.Items.Count - 1 To 0 Step -1
            If i = DataGridODL.Items.Count - 1 Then
                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ID")).Attributes.Add("rowspan", 1)
                idprecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ID")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).Attributes.Add("rowspan", 1)
                progressivoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "FORNITORE")).Attributes.Add("rowspan", 1)
                fornitorePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "FORNITORE")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).Attributes.Add("rowspan", 1)
                repertorioPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).Attributes.Add("rowspan", 1)
                numeroSalPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ODL")).Attributes.Add("rowspan", 1)
                odlPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ODL")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "STATO")).Attributes.Add("rowspan", 1)
                statoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "STATO")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).Attributes.Add("rowspan", 1)
                dataOrdinePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).Attributes.Add("rowspan", 1)
                dataConsuntivoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).Attributes.Add("rowspan", 1)
                ubicazionePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).Attributes.Add("rowspan", 1)
                totNettodiOneriPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "IVA")).Attributes.Add("rowspan", 1)
                ivaPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "IVA")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).Attributes.Add("rowspan", 1)
                totNettoOneriEIvaPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).Attributes.Add("rowspan", 1)
                importoLordoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).Attributes.Add("rowspan", 1)
                sedeTerritorialePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).Attributes.Add("rowspan", 1)
                buildingManagerPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).Text

                DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).Attributes.Add("rowspan", 1)
                mailAppuntamentoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).Text

            Else
                If idprecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ID")).Text _
                    And progressivoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).Text _
                    And fornitorePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "FORNITORE")).Text _
                  And odlPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ODL")).Text _
                  And repertorioPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).Text _
                  And dataOrdinePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).Text _
                  And dataConsuntivoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).Text _
                  And totNettodiOneriPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).Text _
                  And ivaPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "IVA")).Text _
                And totNettoOneriEIvaPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).Text Then


                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "FORNITORE")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "FORNITORE")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "FORNITORE")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "FORNITORE")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "ODL")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ODL")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "ODL")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "ODL")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "STATO")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "STATO")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "STATO")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "STATO")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).RowSpan = 1

                    'DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).Visible = False
                    'DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).RowSpan) + 1
                    'DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "IVA")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "IVA")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "IVA")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "IVA")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).RowSpan = 1

                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).Visible = False
                    DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).RowSpan = Math.Max(1, DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).RowSpan) + 1
                    DataGridODL.Items(i + 1).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).RowSpan = 1
                End If




            End If
            If i = DataGridODL.Items.Count - 1 Then
                DataGridODL.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
            Else
                If idprecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ID")).Text Then
                    DataGridODL.Items(i).BackColor = DataGridODL.Items(i + 1).BackColor
                Else
                    If DataGridODL.Items(i + 1).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD") Then
                        DataGridODL.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#BBBBBB")
                    Else
                        DataGridODL.Items(i).BackColor = Drawing.ColorTranslator.FromHtml("#DDDDDD")
                    End If
                End If

            End If
            idprecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ID")).Text
            progressivoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "PROGRESSIVO")).Text
            fornitorePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "FORNITORE")).Text
            repertorioPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "REPERTORIO")).Text
            numeroSalPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "NUMERO_SAL")).Text
            odlPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "ODL")).Text
            statoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "STATO")).Text
            dataOrdinePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_ORDINE")).Text
            dataConsuntivoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "DATA_CONSUNTIVO")).Text
            ubicazionePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "UBICAZIONE")).Text
            totNettodiOneriPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI")).Text
            ivaPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "IVA")).Text
            totNettoOneriEIvaPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_NETTO_DI_ONERI_E_IVA")).Text
            importoLordoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "TOT_LORDO_ESCLUSO_ONERI")).Text
            sedeTerritorialePrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "SEDE_TERRITORIALE")).Text
            buildingManagerPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "BUILDING_MANAGER")).Text
            mailAppuntamentoPrecedente = DataGridODL.Items(i).Cells(par.IndDGC(DataGridODL, "MAIL_PER_APPUNTAMENTO")).Text

            If i = 0 Then
                DataGridODL.Items(i).BackColor = System.Drawing.ColorTranslator.FromHtml("#507CD1")
                DataGridODL.Items(i).ForeColor = Drawing.Color.White
                DataGridODL.Items(i).Font.Bold = True
                For K As Integer = 0 To DataGridODL.Columns.Count - 1
                    DataGridODL.Items(i).Cells(K).HorizontalAlign = HorizontalAlign.Center
                Next
            End If
        Next
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home_ncp.aspx""</script>")
    End Sub

    Private Function EsportaQueryODL() As String
        Dim filtro As String = ""
        Dim filtroLiquidazione As String = ""
        Dim sValore As String
        Dim sCompara As String
        If Not IsNothing(Request.QueryString("NUM_REPERTORIO")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("NUM_REPERTORIO")) Then
                sValore = Request.QueryString("NUM_REPERTORIO")
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                filtro &= " AND APPALTI.NUM_REPERTORIO " & sCompara & " '" & par.PulisciStrSql(sValore) & "'"
            End If
        End If
        If Not IsNothing(Request.QueryString("FO")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("FO")) And Request.QueryString("FO") <> "-1" Then
                filtro &= " AND APPALTI.ID_FORNITORE = '" & par.PulisciStrSql(UCase(Request.QueryString("FO"))) & "'"
            End If
        End If
        If Not String.IsNullOrEmpty(Request.QueryString("LIQUIDAZIONE")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("LIQUIDAZIONE")) Then
                filtroLiquidazione &= " AND (CASE WHEN (ABS((SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
                       & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) - NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) < 1 " _
                       & " AND " _
                       & " ABS((SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
                       & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) - NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) > 0 )" _
                       & " THEN 'COMPLETO' " _
                       & " WHEN (SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
                       & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) >= NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) " _
                       & " THEN 'COMPLETO' " _
                       & " When NVL((Select SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) = 0 Then 'DA LIQUIDARE' " _
                       & " ELSE 'PARZIALE' END " _
                       & " ) = '" & par.PulisciStrSql(UCase(Request.QueryString("LIQUIDAZIONE"))) & "'"
            End If
        End If
        If Not IsNothing(Request.QueryString("ES")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("ES")) And Request.QueryString("ES") <> "-1" Then
                filtro &= " AND (SELECT ID FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN " _
               & " (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN " _
               & " (SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = SISCOM_MI.APPALTI.ID ))))) = " & Request.QueryString("ES")
            End If
        End If
        If Not IsNothing(Request.QueryString("SERVIZIO")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("SERVIZIO")) And Request.QueryString("SERVIZIO") <> "-1" Then
                filtro &= " AND MANUTENZIONI.ID_SERVIZIO = " & Request.QueryString("SERVIZIO")
            End If
        End If
        If Not IsNothing(Request.QueryString("STATOSAL")) Then
            If Not String.IsNullOrEmpty(Request.QueryString("STATOSAL")) And Request.QueryString("STATOSAL") <> "-1" Then
                filtroLiquidazione &= " AND STATO_FIRMA = '" & par.PulisciStrSql(UCase(Request.QueryString("STATOSAL"))) & "'"
            End If
        End If
        EsportaQueryODL = " Select  MANUTENZIONI.ID, " _
               & " PROGR_SAL As PROGRESSIVO, " _
               & " (SELECT MIN(GETDATA(INIZIO) || ' - ' || GETDATA(FINE)) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN " _
               & " (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN " _
               & " (SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = SISCOM_MI.APPALTI.ID ))))) AS ESERCIZIO_FINANZIARIO, " _
               & " RAGIONE_SOCIALE As FORNITORE, " _
               & " to_char(NUM_REPERTORIO) As REPERTORIO, " _
               & "LTRIM(RTRIM((Case When APPALTI.ANNO Is Not NULL Then TO_CHAR(APPALTI.ANNO) Else SUBSTR(APPALTI.NUM_REPERTORIO,1,4) End)||LTRIM(RTRIM(TO_CHAR(APPALTI.PROGR,'0000000000'))))) AS REP_ORD, " _
               & "(SELECT MAX(CODICE_PROGETTO_VISION) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS CODICE_PROGETTO_VISION, " _
               & "(SELECT MAX(NUMERO_SAL) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS NUMERO_SAL_VISION, " _
               & " TO_DATE (MANUTENZIONI.DATA_INIZIO_ORDINE, 'YYYYMMDD') AS DATA_INIZIO_ORDINE, " _
               & "TO_DATE (MANUTENZIONI.DATA_INIZIO_INTERVENTO, 'YYYYMMDD') AS DATA_INIZIO_INTERVENTO, TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO, 'YYYYMMDD') AS DATA_FINE_INTERVENTO, " _
               & " 'javascript:window.open('''||(CASE WHEN PAGAMENTI.TIPO_PAGAMENTO=3 THEN 'CicloPassivo/MANUTENZIONI/Sal' WHEN PAGAMENTI.TIPO_PAGAMENTO=7 THEN 'CicloPassivo/RRS/SAL_RRS' ELSE NULL END )||'.aspx?PROVENIENZA=CHIAMATA_DIRETTA&ID='||pagamenti.id||'&AP='||appalti.id||''',''_blank'',''resizable=no,height=700,width=1300,top=100,left=100,scrollbars=no'');void(0);' AS APRI_SAL, " _
               & " 'javascript:window.open(''CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?PROVENIENZA=ODLSAL&X=CH&ODL='|| MANUTENZIONI.PROGR || '&ANNO='|| MANUTENZIONI.ANNO || '&ID=' || manutenzioni.id || ''',''_blank'',''resizable=no,height=700,width=1300,top=100,left=100,scrollbars=no'');void(0);' AS APRI_ODL, " _
               & " nvl(PAGAMENTI.PROGR_APPALTO,0) AS NUMERO_SAL, " _
               & " TO_DATE(PAGAMENTI.DATA_EMISSIONE,'YYYYMMDD') AS DATA_SAL, DECODE(STATO_FIRMA,0,'NON FIRMATO',1,'FIRMATO CON RISERVA',2,'FIRMATO') AS STATO_SAL, " _
               & " (CASE WHEN (ABS((SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
               & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) - NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) < 1 " _
               & " AND " _
               & " ABS((SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
               & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) - NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0)) > 0 )" _
               & " THEN 'COMPLETO' " _
               & " WHEN (SELECT SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID) " _
               & " + NVL((SELECT SUM(IVA) FROM PRENOTAZIONI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) >= NVL(PAGAMENTI.IMPORTO_CONSUNTIVATO,0) " _
               & " THEN 'COMPLETO' " _
               & " When NVL((Select SUM(IMPORTO) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE ID_PAGAMENTO = PAGAMENTI.ID),0) = 0 Then 'DA LIQUIDARE' " _
               & " ELSE 'PARZIALE' END " _
               & " ) AS STATO_LIQUIDAZIONE, " _
               & " to_char(MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) AS ODL, " _
               & " TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') AS DATA_ORDINE, " _
               & " TO_DATE(PRENOTAZIONI.DATA_CONSUNTIVAZIONE,'YYYYMMDD') AS DATA_CONSUNTIVO, " _
               & "(SELECT SUM (MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM siscom_mi.MANUTENZIONI_CONSUNTIVI " _
               & "WHERE     MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI in  " _
               & "(select manutenzioni_interventi.id from siscom_mi.manutenzioni_interventi where id_manutenzione = manutenzioni.id) " _
               & "AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO <> 'RIMBORSO OPERE SPECIALISTICHE') AS importo_consuntivo, " _
               & " (CASE " _
               & " WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL " _
               & " THEN (SELECT INDIRIZZI.TIPO_INDIRIZZO||' '||INDIRIZZI.DESCRIZIONE||' '||CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=  " _
               & " (SELECT COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
               & " WHERE COMPLESSI_IMMOBILIARI.ID = ID_COMPLESSO)) " _
               & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL " _
               & " THEN " _
               & " (SELECT INDIRIZZI.TIPO_INDIRIZZO||' '||INDIRIZZI.DESCRIZIONE||' '||CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID= " _
               & " (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI " _
               & " WHERE EDIFICI.ID = MANUTENZIONI.ID_EDIFICIO)) " _
               & " ELSE " _
               & " NULL " _
               & " END) " _
               & " AS UBICAZIONE, " _
               & " IMPORTO_APPROVATO AS IMP_NETTO_DI_ONERI_E_IVA, " _
               & " IVA AS IVA, " _
               & " IMPONIBILE AS IMP_NETTO_DI_ONERI, " _
               & " /*(SELECT SUM(MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI IN ( SELECT ID FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE' ))*/MANUTENZIONI.MAN_CALC_LORDO_E_ONERI AS IMPORTO_LORDO, " _
               & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND MANUTENZIONI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID) " _
               & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID AND MANUTENZIONI.ID_EDIFICIO=EDIFICI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO) " _
               & " ELSE NULL END) AS SEDE_TERRITORIALE, " _
               & " SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID) AS BUILDING_MANAGER, " _
               & " (SELECT TAB_FILIALI.MAIL_REF_AMMINISTRATIVO FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID = ( " _
               & " (SELECT COMPLESSI_IMMOBILIARI.ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN ( " _
               & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN ID_COMPLESSO WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) ELSE 0 END) " _
               & " )))) AS MAIL_PER_APPUNTAMENTO,(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) AS STATO, MANUTENZIONI.IMPORTO_CONSUNTIVATO AS IMPORTO_LORDO_ONERI, " _
               & " SISCOM_MI.GETOGGETTOMANUTENZIONE(MANUTENZIONI.ID) AS PATRIMONIO " _
               & " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI " _
               & " WHERE " _
               & " MANUTENZIONI.ID_APPALTO=APPALTI.ID " _
               & " AND APPALTI.ID_FORNITORE=FORNITORI.ID " _
               & " AND PAGAMENTI.ID=MANUTENZIONI.ID_PAGAMENTO " _
               & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO" _
               & " AND MANUTENZIONI.STATO not in (5,6) " _
               & filtro _
               & filtroLiquidazione _
               & " UNION " _
               & " SELECT  MANUTENZIONI.ID,  " _
               & " PROGR_SAL AS PROGRESSIVO, " _
               & " (SELECT MIN(GETDATA(INIZIO) || ' - ' || GETDATA(FINE)) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN " _
               & " (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN " _
               & " (SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = SISCOM_MI.APPALTI.ID ))))) AS ESERCIZIO_FINANZIARIO, " _
               & " RAGIONE_SOCIALE AS FORNITORE, " _
               & " to_char(NUM_REPERTORIO) AS REPERTORIO, " _
               & "LTRIM(RTRIM((CASE WHEN APPALTI.ANNO IS NOT NULL THEN TO_CHAR(APPALTI.ANNO) ELSE SUBSTR(APPALTI.NUM_REPERTORIO,1,4) END)||LTRIM(RTRIM(TO_CHAR(APPALTI.PROGR,'0000000000'))))) AS REP_ORD, " _
               & "(SELECT MAX(CODICE_PROGETTO_VISION) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS CODICE_PROGETTO_VISION, " _
               & "(SELECT MAX(NUMERO_SAL) FROM SISCOM_MI.IMPORT_STR WHERE CODICE_ODL = MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO) AS NUMERO_SAL_VISION, " _
               & " TO_DATE (MANUTENZIONI.DATA_INIZIO_ORDINE, 'YYYYMMDD') AS DATA_INIZIO_ORDINE, " _
               & "TO_DATE (MANUTENZIONI.DATA_INIZIO_INTERVENTO, 'YYYYMMDD') AS DATA_INIZIO_INTERVENTO, TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO, 'YYYYMMDD') AS DATA_FINE_INTERVENTO, " _
               & " NULL AS APRI_SAL, " _
               & " NULL AS APRI_ODL," _
               & " null AS NUMERO_SAL, " _
               & " NULL AS DATA_SAL, NULL AS STATO_SAL, " _
               & " NULL AS STATO_LIQUIDAZIONE, " _
               & " to_char(MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) AS ODL, " _
               & " TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') AS DATA_ORDINE, " _
               & " TO_DATE(PRENOTAZIONI.DATA_CONSUNTIVAZIONE,'YYYYMMDD') AS DATA_CONSUNTIVO, " _
               & "(SELECT SUM (MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM siscom_mi.MANUTENZIONI_CONSUNTIVI " _
               & "WHERE     MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI in  " _
               & "(select manutenzioni_interventi.id from siscom_mi.manutenzioni_interventi where id_manutenzione = manutenzioni.id) " _
               & "AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO <> 'RIMBORSO OPERE SPECIALISTICHE') AS importo_consuntivo, " _
               & " (CASE " _
               & " WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL " _
               & " THEN (SELECT INDIRIZZI.TIPO_INDIRIZZO||' '||INDIRIZZI.DESCRIZIONE||' '||CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=  " _
               & " (SELECT COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
               & " WHERE COMPLESSI_IMMOBILIARI.ID = MANUTENZIONI.ID_COMPLESSO)) " _
               & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL " _
               & " THEN " _
               & " (SELECT INDIRIZZI.TIPO_INDIRIZZO||' '||INDIRIZZI.DESCRIZIONE||' '||CIVICO FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID= " _
               & " (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI " _
               & " WHERE EDIFICI.ID = MANUTENZIONI.ID_EDIFICIO)) " _
               & " ELSE " _
               & " NULL " _
               & " END) " _
               & " AS UBICAZIONE, " _
               & " IMPORTO_APPROVATO AS IMP_NETTO_DI_ONERI_E_IVA, " _
               & " IVA AS IVA, " _
               & " IMPONIBILE AS IMP_NETTO_DI_ONERI, " _
               & " /*(SELECT SUM(MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI IN ( SELECT ID FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE' ))*/MANUTENZIONI.MAN_CALC_LORDO_E_ONERI AS IMPORTO_LORDO, " _
               & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND MANUTENZIONI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID) " _
               & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT TAB_FILIALI.NOME||'-'||INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID AND MANUTENZIONI.ID_EDIFICIO=EDIFICI.ID AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID AND COMPLESSI_IMMOBILIARI.ID=EDIFICI.ID_COMPLESSO) " _
               & " ELSE NULL END) AS SEDE_TERRITORIALE, " _
               & " SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID) AS BUILDING_MANAGER, " _
               & " (SELECT TAB_FILIALI.MAIL_REF_AMMINISTRATIVO FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID = ( " _
               & " (SELECT COMPLESSI_IMMOBILIARI.ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN ( " _
               & " (CASE WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN ID_COMPLESSO WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) ELSE 0 END) " _
               & " )))) AS MAIL_PER_APPUNTAMENTO,(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=MANUTENZIONI.STATO) AS STATO, MANUTENZIONI.IMPORTO_CONSUNTIVATO AS IMPORTO_LORDO_ONERI, " _
               & " SISCOM_MI.GETOGGETTOMANUTENZIONE(MANUTENZIONI.ID) AS PATRIMONIO " _
               & " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI " _
               & " WHERE " _
               & " MANUTENZIONI.ID_APPALTO=APPALTI.ID " _
               & " AND APPALTI.ID_FORNITORE=FORNITORI.ID " _
               & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
               & " AND MANUTENZIONI.STATO=2" _
               & filtro
        sStringaSqlFiltri = " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI " _
               & " WHERE " _
               & " MANUTENZIONI.ID_APPALTO=APPALTI.ID " _
               & " AND APPALTI.ID_FORNITORE=FORNITORI.ID " _
               & " AND PAGAMENTI.ID=MANUTENZIONI.ID_PAGAMENTO " _
               & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO" _
               & " AND MANUTENZIONI.STATO not in (5,6) " _
               & filtro
        sStringaSqlFiltri1 = " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI " _
               & " WHERE " _
               & " MANUTENZIONI.ID_APPALTO=APPALTI.ID " _
               & " AND APPALTI.ID_FORNITORE=FORNITORI.ID " _
               & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
               & " AND MANUTENZIONI.STATO=2 " _
               & filtro
    End Function

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            connData.apri(False)
            Dim Query As String = EsportaQueryODL()
            Dim dt As New Data.DataTable
            dt = par.getDataTableFilterSortRadGrid(Query, RadGrid1)
            connData.chiudi(False)
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportODL", "ExportODL", dt)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager).RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing, Nothing)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", "function f(){NotificaTelerik('" & CType(Me.Master.FindControl("RadNotificationNote"), RadNotification).ClientID & "', 'Attenzione', '" & par.Messaggio_NoExport & "'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNuovaRicerca.Click
        Response.Redirect("RicercaODLSAL.aspx", False)
    End Sub
End Class
