
Imports System.Data
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabilePerAppalti
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
            HFGriglia.Value = DataGridAppalti.ClientID
            'caricaRisultati()
        End If
    End Sub

    '    Private Sub caricaRisultati()
    '        Try
    '            connData.apri()
    '            Dim dataTablefinale As New Data.DataTable
    '            dataTablefinale.Columns.Add("ID")
    '            dataTablefinale.Columns.Add("ID_GRUPPO")
    '            dataTablefinale.Columns.Add("NUM_REPERTORIO")
    '            dataTablefinale.Columns.Add("STATO")
    '            dataTablefinale.Columns.Add("DESCRIZIONE")
    '            dataTablefinale.Columns.Add("DATA_REPERTORIO")
    '            dataTablefinale.Columns.Add("TIPO_CONTRATTO")
    '            dataTablefinale.Columns.Add("ST")
    '            dataTablefinale.Columns.Add("CIG")
    '            dataTablefinale.Columns.Add("FORNITORE")
    '            dataTablefinale.Columns.Add("TIPO")

    '            dataTablefinale.Columns.Add("BUDGET")
    '            dataTablefinale.Columns.Add("TOTALE_CANONE_IVA_ESCLUSA")
    '            dataTablefinale.Columns.Add("BUDGET_VARIAZIONI")
    '            dataTablefinale.Columns.Add("TOTALE_CONTRATTUALE")
    '            dataTablefinale.Columns.Add("ONERI")
    '            dataTablefinale.Columns.Add("TOTALE")
    '            dataTablefinale.Columns.Add("RESIDUO")


    '            dataTablefinale.Columns.Add("BUDGET2")
    '            dataTablefinale.Columns.Add("TOTALE_CONSUMO_IVA_ESCLUSA")
    '            dataTablefinale.Columns.Add("BUDGET_VARIAZIONI2")
    '            dataTablefinale.Columns.Add("TOTALE_CONTRATTUALE2")
    '            dataTablefinale.Columns.Add("ONERI2")
    '            dataTablefinale.Columns.Add("TOTALE2")
    '            dataTablefinale.Columns.Add("RESIDUO2")

    '            dataTablefinale.Columns.Add("DATA_SCADENZA")


    '            Dim contatore As Integer = 0
    '            Dim condizioneFornitori As String = ""
    '            If Not IsNothing(Request.QueryString("IDF")) AndAlso Request.QueryString("IDF").ToString <> "-1" Then
    '                condizioneFornitori = " AND APPALTI.ID_FORNITORE=" & Request.QueryString("IDF").ToString
    '            End If
    '            Dim condizioneDirLavori As String = ""
    '            If Not IsNothing(Request.QueryString("IDDL")) AndAlso Request.QueryString("IDDL").ToString <> "-1" Then
    '                condizioneDirLavori = " AND ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE = " & Request.QueryString("IDDL").ToString & ")"
    '            End If
    '            Dim idTipo As Integer = 0
    '            If Not IsNothing(Request.QueryString("IDT")) Then
    '                idTipo = Request.QueryString("IDT")
    '            End If
    '            Dim condizioneEsercizio As String = ""
    '            If Not IsNothing(Request.QueryString("IDEF")) AndAlso Request.QueryString("IDEF").ToString <> "-1" Then
    '                Select Case idTipo
    '                    Case 0
    '                        'tutti
    '                        condizioneEsercizio = " AND (APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & ") " _
    '                            & " OR (APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE " _
    '                            & " ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & " )))))"
    '                    Case 1
    '                        'patrimoniali
    '                        condizioneEsercizio = " AND APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & ") "
    '                    Case 2
    '                        'non patrimoniali
    '                        condizioneEsercizio = " AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE " _
    '                            & " ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & " )))"
    '                End Select
    '            End If
    '            Dim condizioneTipo As String = ""
    '            Select Case idTipo
    '                Case 0
    '                    condizioneTipo = ""
    '                Case 1
    '                    condizioneTipo = " AND APPALTI.TIPO='P' "
    '                Case 2
    '                    condizioneTipo = " AND APPALTI.TIPO='N' "
    '            End Select
    '            Dim condizioneSedeTerritoriale As String = ""
    '            If Not IsNothing(Request.QueryString("IDST")) AndAlso Request.QueryString("IDST").ToString <> "-1" Then
    '                condizioneFornitori = " AND APPALTI.ID_STRUTTURA=" & Request.QueryString("IDST").ToString
    '            End If
    '            Dim condizioneDate As String = ""
    '            If Not IsNothing(Request.QueryString("DI")) AndAlso IsDate(Request.QueryString("DI").ToString) Then
    '                condizioneDate &= " AND DATA_REPERTORIO>='" & par.FormatoDataDB(Request.QueryString("DI").ToString) & "'"
    '            End If
    '            If Not IsNothing(Request.QueryString("DF")) AndAlso IsDate(Request.QueryString("DF").ToString) Then
    '                condizioneDate &= " AND DATA_REPERTORIO<='" & par.FormatoDataDB(Request.QueryString("DF").ToString) & "'"
    '            End If
    '            Dim condizioneCIG As String = ""
    '            If Not IsNothing(Request.QueryString("CIG")) AndAlso Request.QueryString("CIG") <> "" Then
    '                If Request.QueryString("CIG").ToString.Contains("*") Then
    '                    condizioneCIG = " AND UPPER(CIG) LIKE '" & par.PulisciStrSql(Request.QueryString("CIG")).Replace("*", "%") & "' "
    '                Else
    '                    condizioneCIG = " AND UPPER(CIG)='" & par.PulisciStrSql(Request.QueryString("CIG")) & "' "
    '                End If
    '            End If
    '            par.cmd.CommandText = "SELECT DISTINCT rownum as id,ID_GRUPPO,NUM_REPERTORIO,TAB_STATI_APPALTI.DESCRIZIONE AS STATO,APPALTI.DESCRIZIONE,SISCOM_MI.GETDATA(DATA_REPERTORIO) AS DATA_REPERTORIO,CIG," _
    '                & " (SELECT (CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE ELSE COGNOME||' '||NOME END) " _
    '                & " FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=APPALTI.ID_FORNITORE) AS FORNITORE " _
    '& ",(SELECT OPERATORI.COGNOME || ' - ' || OPERATORI.NOME FROM OPERATORI WHERE ID = (SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO = APPALTI.ID_GRUPPO AND DATA_FINE_INCARICO = '30000000')) AS DL " _
    '                & " ,(CASE WHEN TIPO='P' THEN 'PATRIMONIALE' ELSE 'NON PATRIMONIALE' END) AS TIPO_CONTRATTO " _
    '                & " ,(SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS ST, " _
    '               & " SISCOM_MI.GETDATA (APPALTI.DATA_FINE) AS DATA_SCADENZA, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 3) AS BUDGET, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 6) " _
    '& " AS TOTALE_CANONE_IVA_ESCLUSA, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 5) " _
    '& " AS TOTALE_CONTRATTUALE, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 4) AS ONERI, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 7) AS BUDGET_VARIAZIONI, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 2) " _
    '& " AS TOTALE, " _
    '& " NVL(SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 1),0) " _
    '& " AS RESIDUO, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 3) AS BUDGET2, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 6) " _
    '& " AS TOTALE_CONSUMO_IVA_ESCLUSA, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 5) " _
    '& " AS TOTALE_CONTRATTUALE2, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 4) AS ONERI2, " _
    '& " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 7) AS BUDGET_VARIAZIONI2, " _
    '& " sISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 2) " _
    '& " AS TOTALE2, " _
    '& " nvl(SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 1),0) " _
    '& " AS RESIDUO2" _
    '                & " FROM SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_APPALTI WHERE APPALTI.ID_STATO=TAB_STATI_APPALTI.ID AND ID_GRUPPO  IS NOT NULL " _
    '                & condizioneFornitori _
    '                & condizioneEsercizio _
    '                & condizioneSedeTerritoriale _
    '                & condizioneDate _
    '                & condizioneCIG _
    '                & condizioneTipo _
    '                & condizioneDirLavori _
    '                & " /*AND NUM_REPERTORIO='2014/37'*/ " _
    '                & " ORDER BY TIPO_CONTRATTO,ID_GRUPPO,DATA_REPERTORIO ASC "
    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            Dim dt As New Data.DataTable
    '            da.Fill(dt)
    '            da.Dispose()


    '            If dt.Rows.Count > 0 Then
    '                DataGridAppalti.DataSource = dt
    '                DataGridAppalti.DataBind()
    '                ' ImageButtonEsporta.Visible = True
    '                DataGridAppalti.Visible = True
    '                lblRis.Visible = False
    '            Else
    '                '   ImageButtonEsporta.Visible = False
    '                DataGridAppalti.Visible = False
    '                lblRis.Visible = True
    '            End If
    '            connData.chiudi()
    '        Catch ex As Exception
    '            connData.chiudi()
    '            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaRisultati - " & ex.Message)
    '            Response.Redirect("../../../Errore.aspx", False)
    '        End Try
    '    End Sub

    'Protected Sub DataGridAppalti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppalti.ItemDataBound
    '    If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
    '        If e.Item.Cells(0).Text > 0 Then
    '            Dim rigaPrecedente As Integer = CDec(e.Item.Cells(0).Text - 1)
    '            Dim colorePrecedente As Drawing.Color = DataGridAppalti.Items(rigaPrecedente).BackColor
    '            Dim gruppoPrecedente As Integer = DataGridAppalti.Items(rigaPrecedente).Cells(1).Text
    '            If e.Item.Cells(1).Text = gruppoPrecedente Then
    '                e.Item.BackColor = colorePrecedente
    '            Else
    '                If colorePrecedente = Drawing.Color.White Then
    '                    e.Item.BackColor = Drawing.Color.LightGray
    '                Else
    '                    e.Item.BackColor = Drawing.Color.White
    '                End If
    '            End If
    '        Else
    '            e.Item.BackColor = Drawing.Color.White
    '        End If
    '    End If
    'End Sub

    'Protected Sub ImageButtonEsporta_Click(sender As Object, e As System.EventArgs) Handles ImageButtonEsporta.Click
    'Try
    '    Dim xls As New ExcelSiSol
    '    Dim nomeFile = par.EsportaExcelAutomaticoDaDataGrid(DataGridAppalti, "ExportSituazioneContabilePerAppalti", , , "", False)
    '    If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
    '        Response.Redirect("../../../FileTemp/" & nomeFile, False)
    '    Else
    '        Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
    '    End If
    'Catch ex As Exception
    '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ImageButtonEsporta_Click - " & ex.Message)
    '    Response.Redirect("../../../Errore.aspx", False)
    'End Try
    'End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGridAppalti.AllowPaging = False
        DataGridAppalti.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGridAppalti.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGridAppalti.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGridAppalti.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGridAppalti.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGridAppalti.AllowPaging = True
        DataGridAppalti.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGridAppalti.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SITCONTABILEAPPALTI", "SITCONTABILEAPPALTI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiSituazioneContabilePerAppalti_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Private Sub DataGridAppalti_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridAppalti.NeedDataSource
        Try
            connData.apri()
            Dim dataTablefinale As New Data.DataTable
            dataTablefinale.Columns.Add("ID")
            dataTablefinale.Columns.Add("ID_GRUPPO")
            dataTablefinale.Columns.Add("NUM_REPERTORIO")
            dataTablefinale.Columns.Add("STATO")
            dataTablefinale.Columns.Add("DESCRIZIONE")
            dataTablefinale.Columns.Add("DATA_REPERTORIO")
            dataTablefinale.Columns.Add("TIPO_CONTRATTO")
            dataTablefinale.Columns.Add("ST")
            dataTablefinale.Columns.Add("CIG")
            dataTablefinale.Columns.Add("FORNITORE")
            dataTablefinale.Columns.Add("DL")
            dataTablefinale.Columns.Add("CONDIZIONE_PAGAMENTO")
            dataTablefinale.Columns.Add("TIPO")

            dataTablefinale.Columns.Add("BUDGET")
            dataTablefinale.Columns.Add("TOTALE_CANONE_IVA_ESCLUSA")
            dataTablefinale.Columns.Add("BUDGET_VARIAZIONI")
            dataTablefinale.Columns.Add("TOTALE_CONTRATTUALE")
            dataTablefinale.Columns.Add("ONERI")
            dataTablefinale.Columns.Add("TOTALE")
            dataTablefinale.Columns.Add("RESIDUO")


            dataTablefinale.Columns.Add("BUDGET2")
            dataTablefinale.Columns.Add("TOTALE_CONSUMO_IVA_ESCLUSA")
            dataTablefinale.Columns.Add("BUDGET_VARIAZIONI2")
            dataTablefinale.Columns.Add("TOTALE_CONTRATTUALE2")
            dataTablefinale.Columns.Add("ONERI2")
            dataTablefinale.Columns.Add("TOTALE2")
            dataTablefinale.Columns.Add("RESIDUO2")

            dataTablefinale.Columns.Add("DATA_SCADENZA")


            Dim contatore As Integer = 0
            Dim condizioneFornitori As String = ""
            If Not IsNothing(Request.QueryString("IDF")) AndAlso Request.QueryString("IDF").ToString <> "-1" Then
                condizioneFornitori = " AND APPALTI.ID_FORNITORE=" & Request.QueryString("IDF").ToString
            End If
            Dim condizioneDirLavori As String = ""
            If Not IsNothing(Request.QueryString("IDDL")) AndAlso Request.QueryString("IDDL").ToString <> "-1" Then
                condizioneDirLavori = " AND ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE = " & Request.QueryString("IDDL").ToString & ")"
            End If
            Dim idTipo As Integer = 0
            If Not IsNothing(Request.QueryString("IDT")) Then
                idTipo = Request.QueryString("IDT")
            End If
            Dim condizioneEsercizio As String = ""
            If Not IsNothing(Request.QueryString("IDEF")) AndAlso Request.QueryString("IDEF").ToString <> "-1" Then
                Select Case idTipo
                    Case 0
                        'tutti
                        condizioneEsercizio = " AND (APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & ") " _
                            & " OR (APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE " _
                            & " ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & " )))))"
                    Case 1
                        'patrimoniali
                        condizioneEsercizio = " AND APPALTI.ID_LOTTO IN (SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & ") "
                    Case 2
                        'non patrimoniali
                        condizioneEsercizio = " AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE " _
                            & " ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & Request.QueryString("IDEF").ToString & " )))"
                End Select
            End If
            Dim condizioneTipo As String = ""
            Select Case idTipo
                Case 0
                    condizioneTipo = ""
                Case 1
                    condizioneTipo = " AND APPALTI.TIPO='P' "
                Case 2
                    condizioneTipo = " AND APPALTI.TIPO='N' "
            End Select
            Dim condizioneSedeTerritoriale As String = ""
            If Not IsNothing(Request.QueryString("IDST")) AndAlso Request.QueryString("IDST").ToString <> "-1" Then
                condizioneFornitori = " AND APPALTI.ID_STRUTTURA=" & Request.QueryString("IDST").ToString
            End If
            Dim condizioneDate As String = ""
            If Not IsNothing(Request.QueryString("DI")) AndAlso IsDate(Request.QueryString("DI").ToString) Then
                condizioneDate &= " AND DATA_REPERTORIO>='" & par.FormatoDataDB(Request.QueryString("DI").ToString) & "'"
            End If
            If Not IsNothing(Request.QueryString("DF")) AndAlso IsDate(Request.QueryString("DF").ToString) Then
                condizioneDate &= " AND DATA_REPERTORIO<='" & par.FormatoDataDB(Request.QueryString("DF").ToString) & "'"
            End If
            Dim condizioneCIG As String = ""
            If Not IsNothing(Request.QueryString("CIG")) AndAlso Request.QueryString("CIG") <> "" Then
                If Request.QueryString("CIG").ToString.Contains("*") Then
                    condizioneCIG = " AND UPPER(CIG) LIKE '" & par.PulisciStrSql(Request.QueryString("CIG")).Replace("*", "%") & "' "
                Else
                    condizioneCIG = " AND UPPER(CIG)='" & par.PulisciStrSql(Request.QueryString("CIG")) & "' "
                End If
            End If
            par.cmd.CommandText = "SELECT DISTINCT ID_GRUPPO,NUM_REPERTORIO,TAB_STATI_APPALTI.DESCRIZIONE AS STATO,APPALTI.DESCRIZIONE,SISCOM_MI.GETDATA(DATA_REPERTORIO) AS DATA_REPERTORIO,CIG," _
                & " (SELECT (CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE ELSE COGNOME||' '||NOME END) " _
                & " FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=APPALTI.ID_FORNITORE) AS FORNITORE " _
                & ",(SELECT OPERATORI.COGNOME || ' - ' || OPERATORI.NOME FROM OPERATORI WHERE ID = (SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO = APPALTI.ID_GRUPPO AND DATA_FINE_INCARICO = '30000000')) AS DL " _
                & " ,(SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTO WHERE TIPO_PAGAMENTO.ID = ID_TIPO_PAGAMENTO) AS CONDIZIONE_PAGAMENTO" _
                & " ,(CASE WHEN TIPO='P' THEN 'PATRIMONIALE' ELSE 'NON PATRIMONIALE' END) AS TIPO_CONTRATTO " _
                & " ,(SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID=ID_STRUTTURA) AS ST, " _
               & " SISCOM_MI.GETDATA (APPALTI.DATA_FINE) AS DATA_SCADENZA, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 3) AS BUDGET, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 6) " _
                & " AS TOTALE_CANONE_IVA_ESCLUSA, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 5) " _
                & " AS TOTALE_CONTRATTUALE, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 4) AS ONERI, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 7) AS BUDGET_VARIAZIONI, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 2) " _
                & " AS TOTALE, " _
                & " NVL(SISCOM_MI.GETRESIDUOAPP (appalti.id, 2, 1),0) " _
                & " AS RESIDUO, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 3) AS BUDGET2, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 6) " _
                & " AS TOTALE_CONSUMO_IVA_ESCLUSA, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 5) " _
                & " AS TOTALE_CONTRATTUALE2, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 4) AS ONERI2, " _
                & " SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 7) AS BUDGET_VARIAZIONI2, " _
                & " sISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 2) " _
                & " AS TOTALE2, " _
                & " nvl(SISCOM_MI.GETRESIDUOAPP (appalti.id, 1, 1),0) " _
                & " AS RESIDUO2" _
                & " FROM SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_APPALTI WHERE APPALTI.ID_STATO=TAB_STATI_APPALTI.ID AND ID_GRUPPO  IS NOT NULL " _
                & condizioneFornitori _
                & condizioneEsercizio _
                & condizioneSedeTerritoriale _
                & condizioneDate _
                & condizioneCIG _
                & condizioneTipo _
                & condizioneDirLavori _
                & " /*AND NUM_REPERTORIO='2014/37'*/ " _
                & " ORDER BY TIPO_CONTRATTO,ID_GRUPPO,DATA_REPERTORIO ASC "
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt


            If dt.Rows.Count > 0 Then

                ' ImageButtonEsporta.Visible = True
                DataGridAppalti.Visible = True
                lblRis.Visible = False
            Else
                '   ImageButtonEsporta.Visible = False
                DataGridAppalti.Visible = False
                lblRis.Visible = True
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaRisultati - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub
End Class
