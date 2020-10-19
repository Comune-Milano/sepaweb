Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiEstrazioneCDP
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim IsExport As Boolean = False

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            caricaPagamenti()
        End If
    End Sub
    Private Sub caricaPagamenti()
        Try
            Dim PF As String = ""
            Dim condizionePF As String = ""
            If Not IsNothing(Request.QueryString("PF")) Then
                PF = CInt(Request.QueryString("PF"))
            End If
            If IsNumeric(PF) AndAlso PF > 0 Then
                condizionePF = " AND PAGAMENTI.ID IN (SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & PF & "))"
            End If
            Dim FORNITORE As String = ""
            Dim condizioneFORNITORE As String = ""
            If Not IsNothing(Request.QueryString("FOR")) Then
                FORNITORE = CInt(Request.QueryString("FOR"))
            End If
            If IsNumeric(FORNITORE) AndAlso FORNITORE > 0 Then
                condizioneFORNITORE = " AND PAGAMENTI.ID_FORNITORE=" & FORNITORE
            End If
            Dim REPERTORIO As String = ""
            Dim condizioneREPERTORIO As String = ""
            If Not IsNothing(Request.QueryString("REP")) Then
                REPERTORIO = CInt(Request.QueryString("REP"))
            End If
            If IsNumeric(REPERTORIO) AndAlso REPERTORIO > 0 Then
                condizionePF = " AND PAGAMENTI.ID_APPALTO=" & Request.QueryString("REP")
            End If
            Dim DGR As String = ""
            Dim condizioneDGR As String = ""
            If Not IsNothing(Request.QueryString("DGR")) Then
                DGR = CInt(Request.QueryString("DGR"))
            End If
            If IsNumeric(DGR) AndAlso DGR > 0 Then
                condizioneDGR = " AND PAGAMENTI.ID IN (SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO=" & Request.QueryString("DGR") & ")"
            End If
            Dim condizioneData As String = ""
            If Not IsNothing(Request.QueryString("DA")) AndAlso IsDate(Request.QueryString("DA")) Then
                condizioneData &= " AND DATA_EMISSIONE>='" & par.AggiustaData(Request.QueryString("DA").ToString) & "'"
            End If
            If Not IsNothing(Request.QueryString("A")) AndAlso IsDate(Request.QueryString("A")) Then
                condizioneData &= " AND DATA_EMISSIONE<='" & par.AggiustaData(Request.QueryString("A").ToString) & "'"
            End If
            connData.apri()
            par.cmd.CommandText = " SELECT PROGR,ANNO,PROGR || '/' || ANNO AS ADP, " _
                & " SISCOM_MI.GETDATA(DATA_EMISSIONE) AS DATA_EMISSIONE, " _
                & " SISCOM_MI.GETDATA(DATA_sTAMPA) AS DATA_STAMPA, " _
                & " PAGAMENTI.DESCRIZIONE, " _
                & " /*TRIM(TO_CHAR(IMPORTO_CONSUNTIVATO,'999G999G990D99')) AS IMPORTO, */" _
                & " ROUND(IMPORTO_CONSUNTIVATO,2) AS IMPORTO, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTI WHERE PAGAMENTI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID) AS TIPO_PAGAMENTO, " _
                & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE PAGAMENTI.ID_FORNITORE=FORNITORI.ID) AS FORNITORE, " _
                & " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE PAGAMENTI.ID_APPALTO=APPALTI.ID) AS REPERTORIO " _
                & " FROM SISCOM_MI.PAGAMENTI " _
                & " WHERE PROGR>0 " _
                & " AND ID_STATO>0 " _
                & " AND IMPORTO_CONSUNTIVATO<>0 " _
                & condizioneData _
                & condizionePF _
                & condizioneREPERTORIO _
                & condizioneFORNITORE _
                & condizioneDGR _
                & "ORDER BY ANNO ASC,PROGR ASC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            Session.Item("ESTRAZIONECDP") = dt
            If dt.Rows.Count > 0 Then
                DataGrid1.Rebind()
                lblErrore.Text = ""
                DataGrid1.Visible = True
            Else
                lblErrore.Text = "Nessun pagamento trovato"
                DataGrid1.Visible = False
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - caricaPagamenti - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid1_InfrastructureExporting(sender As Object, e As Telerik.Web.UI.GridInfrastructureExportingEventArgs) Handles DataGrid1.InfrastructureExporting
        For Each cell As Telerik.Web.UI.ExportInfrastructure.Cell In e.ExportStructure.Tables(0).Columns(1).Cells
            cell.Format = "\@"
        Next
        For Each cell As Telerik.Web.UI.ExportInfrastructure.Cell In e.ExportStructure.Tables(0).Columns(7).Cells
            cell.Format = "\@"
        Next
        For Each cell As Telerik.Web.UI.ExportInfrastructure.Cell In e.ExportStructure.Tables(0).Columns(4).Cells
            cell.Format = "#,##0.00"
        Next
    End Sub
    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        DataGrid1.DataSource = CType(Session.Item("ESTRAZIONECDP"), Data.DataTable)
    End Sub
    Protected Sub DataGrid1_PreRender(sender As Object, e As System.EventArgs) Handles DataGrid1.PreRender
        If CInt(AltezzaRadGrid.Value) > 0 And CInt(LarghezzaRadGrid.Value) Then
            DataGrid1.Height = CInt(AltezzaRadGrid.Value)
            DataGrid1.Width = CInt(LarghezzaRadGrid.Value)
        End If
        If IsExport Then
            For Each items As Telerik.Web.UI.GridDataItem In DataGrid1.MasterTableView.Items
                Dim val As Double = Convert.ToDouble(items("IMPORTO").Text)
                Dim newvalue As String = String.Format("{0:f2}", val)
                items("IMPORTO").Text = newvalue
            Next
        End If
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CONTRATTI", "CONTRATTI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub


End Class
