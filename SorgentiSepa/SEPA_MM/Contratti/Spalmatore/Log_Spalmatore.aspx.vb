Imports System.Data
Imports Telerik.Web.UI
Partial Class Contratti_Spalmatore_Log_Spalmatore
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Sub Contratti_Spalmatore_Log_Spalmatore_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = dgvLogSpalmatore.ClientID.ToString.Replace("ctl00", "MasterPage")
        End If
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvLogSpalmatore.AllowPaging = False
        dgvLogSpalmatore.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvLogSpalmatore.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvLogSpalmatore.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvLogSpalmatore.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvLogSpalmatore.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvLogSpalmatore.AllowPaging = True
        dgvLogSpalmatore.Rebind()
    End Sub

    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportLog", "ExportLog", dt)
        If IO.File.Exists(Server.MapPath("..\..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Private Sub dgvLogSpalmatore_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvLogSpalmatore.NeedDataSource
        Try
            Dim Query As String = " SELECT " _
            & " ID_ELABORAZIONE, siscom_mi.getdata(DATA_ELABORAZIONE) as DATA_ELABORAZIONE, ID_RU, " _
            & " COD_RU, TIPO_RU, STATO, " _
            & " TIPO_RU_SPECIFICO, siscom_mi.getdata(DATA_SCAD_BOLL) as DATA_SCAD_BOLL, SALDO, SALDO_SPALM," _
            & " ID_BOLL_GEST, ID_TIPO_GESTIONALE, ACRONIMO, " _
            & " siscom_mi.getdata(DATA_EMISSIONE) as DATA_EMISSIONE, IMPORTO_GESTIONALE, SEMAFORO, " _
            & " IMPORTO_PROCESSATO, (CASE WHEN KPI1=0 THEN 'NO' ELSE 'SI' END) AS KPI1, " _
            & " (CASE WHEN VENDUTO=0 THEN 'NO' ELSE 'SI' END) AS VENDUTO, " _
            & " (CASE WHEN ART_15_C2BIS=0 THEN 'NO' ELSE 'SI' END) AS ART_15_C2BIS, " _
            & " (CASE WHEN RU_CON_RATEIZZ=0 THEN 'NO' ELSE 'SI' END) AS RU_CON_RATEIZZ, " _
            & " (CASE WHEN PRESENZA_MOR is null THEN '' WHEN PRESENZA_MOR=0 THEN 'NO' WHEN PRESENZA_MOR=1 THEN 'SI' END) AS PRESENZA_MOR," _
            & " NUOVO_SALDO, NUOVO_SALDO_SPALM, " _
            & " ID_CREDITO_RESIDUO,IMP_GEST_RESIDUO,ESITO_LAVORAZIONE" _
            & " FROM SISCOM_MI.LOG_ELABORAZIONE_SPALMATORE"
            dgvLogSpalmatore.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub
End Class
