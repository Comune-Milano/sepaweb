Imports Telerik.Web.UI
Imports System.Data

Partial Class MANUTENZIONI_EventiAppalti
    Inherits System.Web.UI.Page
    Public par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
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
        End If
    End Sub
    Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Try
            Dim stringa As String = "SELECT TO_DATE(SISCOM_MI.EVENTI_APPALTI.DATA_ORA,'YYYYMMDDHH24MISS') AS ""DATA_ORA""," _
              & " SISCOM_MI.TAB_EVENTI.DESCRIZIONE,SISCOM_MI.EVENTI_APPALTI.COD_EVENTO,SISCOM_MI.EVENTI_APPALTI.MOTIVAZIONE," _
              & " SEPA.OPERATORI.OPERATORE,SISCOM_MI.EVENTI_APPALTI.ID_OPERATORE,SEPA.CAF_WEB.COD_CAF " _
              & " FROM SEPA.CAF_WEB, SISCOM_MI.EVENTI_APPALTI, SISCOM_MI.TAB_EVENTI,SEPA.OPERATORI " _
              & " WHERE SISCOM_MI.EVENTI_APPALTI.ID_APPALTO= " & Request.QueryString("ID_APPALTO") _
              & " AND SISCOM_MI.EVENTI_APPALTI.COD_EVENTO=SISCOM_MI.TAB_EVENTI.COD (+) " _
              & " AND SISCOM_MI.EVENTI_APPALTI.ID_OPERATORE=SEPA.OPERATORI.ID (+) " _
              & " AND SEPA.CAF_WEB.ID=SEPA.OPERATORI.ID_CAF " _
              & " ORDER BY EVENTI_APPALTI.DATA_ORA DESC, EVENTI_APPALTI.COD_EVENTO DESC"
            Dim dt As Data.DataTable = par.getDataTableGrid(stringa)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
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
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "EventiContratto", "EventiContratto", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
