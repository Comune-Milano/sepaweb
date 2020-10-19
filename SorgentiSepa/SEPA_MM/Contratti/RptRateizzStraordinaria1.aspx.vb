Imports System.Data
Imports Telerik.Web.UI

Partial Class Contratti_RptRateizzStraordinaria1
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
                & " siscom_mi.getstatocontratto(rapporti_utenza.id) as stato_contratto," _
                & " (SELECT COUNT(ID) FROM siscom_mi.bol_bollette WHERE id_contratto=rapporti_utenza.id and id_tipo=5 and id_bolletta_storno is null and nvl(importo_pagato,0)=0 ) AS NUM_RATE_PAGATE," _
                & " (SELECT COUNT(ID) FROM siscom_mi.bol_bollette WHERE id_contratto=rapporti_utenza.id and id_tipo=1 and id_bolletta_storno is null and nvl(importo_pagato,0)=0 ) AS NUM_ORD_PAGATE" _
                & " from siscom_mi.rapporti_utenza,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.indirizzi where rapporti_utenza.id in (select id_Contratto from siscom_mi.bol_rateizzazioni " _
                & " where nvl(fl_annullata,0)=0" _
                & " and nvl(id_Stato,1)=0" _
                & " and nvl(id_dic_redditi,0)<>0)" _
                & " and unita_immobiliari.id=unita_contrattuale.id_unita" _
                & " and unita_immobiliari.id_unita_principale is null" _
                & " and rapporti_utenza.id=unita_contrattuale.id_contratto" _
                & " and indirizzi.id=unita_immobiliari.id_indirizzo" _
                & " and ( (SELECT COUNT(ID) FROM siscom_mi.bol_bollette WHERE id_contratto=rapporti_utenza.id and id_tipo=5 and id_bolletta_storno is null and nvl(importo_pagato,0)=0 ) between 1 and 9 " _
                & " or " _
                & " (SELECT COUNT(ID) FROM siscom_mi.bol_bollette WHERE id_contratto=rapporti_utenza.id and id_tipo=1 and id_bolletta_storno is null and nvl(importo_pagato,0)=0 ) between 1 and 9 ) order by 2 asc"
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
