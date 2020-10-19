Imports System.Data
Imports Telerik.Web.UI

Partial Class Contratti_RptRateizzStraordinaria2
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Sub Contratti_RptRateizzStraordinaria1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGrid1.ClientID
        End If
    End Sub
    Private Sub RadGrid1_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Try
            par.cmd.CommandText = " select cod_contratto,siscom_mi.getintestatari(rapporti_utenza.id) as INTESTATARIO ,INDIRIZZI.DESCRIZIONE ||' '||INDIRIZZI.CIVICO AS INDIRIZZO," _
                & " siscom_mi.getstatocontratto(rapporti_utenza.id) as stato_contratto, " _
                & " getdata(domande_bando_vsa.data_presentazione) as data_presentazione, " _
                & " domande_bando_vsa.pg as pg_domanda " _
                & " from siscom_mi.rapporti_utenza,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.indirizzi,domande_bando_vsa,dichiarazioni_vsa where" _
                & " unita_immobiliari.id=unita_contrattuale.id_unita" _
                & " and unita_immobiliari.id_unita_principale is null" _
                & " and rapporti_utenza.id=unita_contrattuale.id_contratto" _
                & " and indirizzi.id=unita_immobiliari.id_indirizzo" _
                & " and domande_bando_vsa.contratto_num=rapporti_utenza.cod_contratto and id_motivo_domanda=12 and nvl(FL_NUCLEO_CAPIENTE,0)=1 " _
                & " and dichiarazioni_vsa.id=domande_bando_vsa.id_dichiarazione " _
                & " and dichiarazioni_vsa.id_Stato<>2 " _
                & " order by 2 asc"
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)

        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Add("ERRORE", Page.Title & " RadGrid1_NeedDataSource - " & ex.Message)
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportRateizzStr", "RateizzStr", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('');</script>")
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub
End Class
