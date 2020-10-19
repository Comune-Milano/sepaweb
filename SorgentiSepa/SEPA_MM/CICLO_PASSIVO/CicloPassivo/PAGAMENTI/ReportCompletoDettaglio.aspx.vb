Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ReportCompletoDettaglio
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim total As Decimal = 0
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
        End If
    End Sub
    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            Dim idVoce As Integer = Request.QueryString("idv")
            Dim idVoceServizio As Integer = Request.QueryString("idvs")
            Dim anno As Integer = Request.QueryString("ida")
            Dim anno2 As Integer = Request.QueryString("ida2")
            Dim condizioneRicerca As String = ""
            If IsNumeric(idVoceServizio) AndAlso idVoceServizio > 0 Then
                condizioneRicerca &= " AND PF_VOCI_IMPORTO.ID=" & idVoceServizio
            End If
            Dim tipo As String = Request.QueryString("tipo")
            Dim query As String = ""
            Dim condizioneStatoNormale As String = ""
            Dim condizioneStatoAnnullato As String = ""
            Dim importo As String = ""
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.REPORT_SINTESI_DETTAGLIO WHERE CODICE='" & tipo.ToUpper & "'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                condizioneStatoNormale = par.IfNull(lettore("NORMALE"), "").Replace("#ANNO#", anno).Replace("#ANNO#2", anno2)
                condizioneStatoAnnullato = par.IfNull(lettore("ANNULLATO"), "").Replace("#ANNO#", anno).Replace("#ANNO2#", anno2)
                importo = par.IfNull(lettore("IMPORTO"), "").Replace("#ANNO#", anno).Replace("#ANNO2#", anno2)
            End If
            lettore.Close()

            query = "SELECT PRENOTAZIONI.ID" _
                & ",ID_VOCE_PF" _
                & ",ID_VOCE_PF_IMPORTO" _
                & ",PF_VOCI.DESCRIZIONE AS VOCE" _
                & ",PF_VOCI_IMPORTO.DESCRIZIONE AS SERVIZIO" _
                & "," & importo & " AS IMPORTO" _
                & ",TIPO_PAGAMENTI.DESCRIZIONE AS TIPO" _
                & ",(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO IN (3,7) THEN (SELECT PROGR||'/'||ANNO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID) ELSE '-' END) AS ODL" _
                & ",(CASE WHEN PRENOTAZIONI.ID_APPALTO IS NOT NULL AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL THEN PAGAMENTI.PROGR_APPALTO||'/'||PAGAMENTI.ANNO ELSE '-' END) AS SAL" _
                & ",(CASE WHEN PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL THEN PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO ELSE '-' END) AS CDP" _
                & ",PRENOTAZIONI.DESCRIZIONE AS NOTE" _
                & ",(CASE WHEN PRENOTAZIONI.ID_APPALTO IS NOT NULL THEN APPALTI.NUM_REPERTORIO ELSE '-' END) AS REPERTORIO" _
                & ",(CASE WHEN PRENOTAZIONI.ID_FORNITORE IS NOT NULL THEN FORNITORI.RAGIONE_SOCIALE ELSE '-' END) AS FORNITORE" _
                & ",TAB_STATI_PRENOTAZIONI.DESCRIZIONE AS STATO_ATTUALE " _
                & " FROM SISCOM_MI.PRENOTAZIONI " _
                & ",SISCOM_MI.PF_VOCI" _
                & ",SISCOM_MI.PF_VOCI_IMPORTO" _
                & ",SISCOM_MI.TIPO_PAGAMENTI" _
                & ",SISCOM_MI.PAGAMENTI" _
                & ",SISCOM_MI.FORNITORI" _
                & ",SISCOM_MI.APPALTI" _
                & ",SISCOM_MI.TAB_STATI_PRENOTAZIONI" _
                & " WHERE PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI CONNECT BY PRIOR ID=ID_VOCE_MADRE START WITH ID=" & idVoce & ")" _
                & " AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF" _
                & " AND PF_VOCI_IMPORTO.ID(+)=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                & condizioneStatoNormale _
                & " AND TIPO_PAGAMENTI.ID=PRENOTAZIONI.TIPO_PAGAMENTO " _
                & " AND PAGAMENTI.ID(+)=PRENOTAZIONI.ID_PAGAMENTO " _
                & " AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID(+)" _
                & " AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & " AND PRENOTAZIONI.ID_STATO=TAB_STATI_PRENOTAZIONI.ID(+)" _
                & condizioneRicerca
            If condizioneStatoAnnullato <> "" Then
                query &= " UNION " _
                    & " SELECT PRENOTAZIONI.ID" _
                    & ",ID_VOCE_PF" _
                    & ",ID_VOCE_PF_IMPORTO" _
                    & ",PF_VOCI.DESCRIZIONE AS VOCE" _
                    & ",PF_VOCI_IMPORTO.DESCRIZIONE AS SERVIZIO" _
                    & "," & importo & " AS IMPORTO" _
                    & ",TIPO_PAGAMENTI.DESCRIZIONE AS TIPO" _
                    & ",(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO IN (3,7) THEN (SELECT PROGR||'/'||ANNO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID) ELSE '-' END) AS ODL" _
                    & ",(CASE WHEN PRENOTAZIONI.ID_APPALTO IS NOT NULL AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL THEN PAGAMENTI.PROGR_APPALTO||'/'||PAGAMENTI.ANNO ELSE '-' END) AS SAL" _
                    & ",(CASE WHEN PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL THEN PAGAMENTI.PROGR||'/'||PAGAMENTI.ANNO ELSE '-' END) AS CDP" _
                    & ",PRENOTAZIONI.DESCRIZIONE AS NOTE" _
                    & ",(CASE WHEN PRENOTAZIONI.ID_APPALTO IS NOT NULL THEN APPALTI.NUM_REPERTORIO ELSE '-' END) AS REPERTORIO" _
                    & ",(CASE WHEN PRENOTAZIONI.ID_FORNITORE IS NOT NULL THEN FORNITORI.RAGIONE_SOCIALE ELSE '-' END) AS FORNITORE" _
                    & ",TAB_STATI_PRENOTAZIONI.DESCRIZIONE AS STATO_ATTUALE " _
                    & " FROM SISCOM_MI.PRENOTAZIONI " _
                    & ",SISCOM_MI.PF_VOCI" _
                    & ",SISCOM_MI.PF_VOCI_IMPORTO" _
                    & ",SISCOM_MI.TIPO_PAGAMENTI" _
                    & ",SISCOM_MI.PAGAMENTI" _
                    & ",SISCOM_MI.FORNITORI" _
                    & ",SISCOM_MI.APPALTI" _
                    & ",SISCOM_MI.TAB_STATI_PRENOTAZIONI" _
                    & " WHERE PRENOTAZIONI.ID_VOCE_PF IN (SELECT ID FROM SISCOM_MI.PF_VOCI CONNECT BY PRIOR ID=ID_VOCE_MADRE START WITH ID=" & idVoce & ")" _
                    & " AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF" _
                    & " AND PF_VOCI_IMPORTO.ID(+)=PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _
                    & condizioneStatoAnnullato _
                    & " AND TIPO_PAGAMENTI.ID=PRENOTAZIONI.TIPO_PAGAMENTO " _
                    & " AND PAGAMENTI.ID(+)=PRENOTAZIONI.ID_PAGAMENTO " _
                    & " AND PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID(+)" _
                    & " AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                    & " AND PRENOTAZIONI.ID_STATO=TAB_STATI_PRENOTAZIONI.ID(+)" _
                    & condizioneRicerca
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(query)
            TryCast(sender, RadGrid).DataSource = dt
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
        End Try
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Completo", "Completo", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
