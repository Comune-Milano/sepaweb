Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_ResiduoCanone
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If par.IfEmpty(Request.QueryString("ID"), 0) > 0 Then
                CaricaRiepilogo()
                HFGriglia.Value = dgvresiduo.ClientID
            End If
        End If


    End Sub
    Private Sub CaricaRiepilogo()
        Try



        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvresiduo.AllowPaging = False
        dgvresiduo.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvresiduo.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvresiduo.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvresiduo.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvresiduo.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvresiduo.AllowPaging = True
        dgvresiduo.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvresiduo.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "RESIDUOCANONE", "RESIDUOCANONE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
    Private Sub MANUTENZIONI_RisultatiAppalti_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub



    Private Sub dgvresiduo_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvresiduo.NeedDataSource
        par.cmd.CommandText = "SELECT PROGR,ANNO,DATA_EMISSIONE,DATA_STAMPA," _
                                & " ROUND((SELECT NVL(SUM((CASE WHEN IMPORTO_APPROVATO IS NOT NULL THEN NVL(IMPORTO_APPROVATO,0) ELSE NVL(IMPORTO_PRENOTATO,0) END)),0) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI.ID AND PRENOTAZIONI.ID_STATO<>-3),2) AS IMPORTO_CONSUNTIVATO," _
                                & " ROUND((SELECT NVL(SUM(RIT_LEGGE_IVATA),0) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=PAGAMENTI.ID AND PRENOTAZIONI.ID_STATO<>-3),2) AS RIT_LEGGE,DESCRIZIONE                   " _
                                & " FROM SISCOM_MI.PAGAMENTI WHERE PAGAMENTI.ID_APPALTO IN " _
                                & " (SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo = (SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID = " & Request.QueryString("ID") & " )) " _
                                & " AND PAGAMENTI.ID_STATO > 0 AND PAGAMENTI.TIPO_PAGAMENTO = 6 " _
                                & " ORDER BY ANNO,PROGR ASC"

        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Dim Totale As Decimal



        For Each r As Data.DataRow In dt.Rows
            Totale = Math.Round(Totale + CDec(par.IfNull(r.Item("IMPORTO_CONSUNTIVATO"), 0)), 2)
        Next
        Me.txtTotAppalto.Text = Format(CDec(Request.QueryString("TOTCAN")), "##,##0.00")
        Me.txtTotPagamenti.Text = Format(Totale, "##,##0.00")
        Me.txtResiduo.Text = Format((CDec(Me.txtTotAppalto.Text.Replace(".", "")) - Totale), "##,##0.00")

        Dim row As Data.DataRow
        row = dt.NewRow()
        row.Item("IMPORTO_CONSUNTIVATO") = Format(Totale, "##,##0.00")
        row.Item("DATA_STAMPA") = "TOTALE"
        dt.Rows.Add(row)






        TryCast(sender, RadGrid).DataSource = dt
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
