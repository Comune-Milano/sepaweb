
Imports System.Data
Imports Telerik.Web.UI

Partial Class Contratti_RptAzLegali
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGridAzLegali.ClientID
        End If
    End Sub

    Private Sub RadGridAzLegali_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGridAzLegali.NeedDataSource
        Try
            par.cmd.CommandText = " SELECT RAPPORTI_UTENZA.ID," _
                & " RAPPORTI_UTENZA.COD_CONTRATTO, " _
                & " cod_Tipologia_contr_loc as TIPO_RU, " _
                & " (CASE WHEN RAPPORTI_UTENZA.DEST_USO ='B' THEN 'POSTO AUTO COPERTO,SCOPERTO,BOX,MOTOBOX ETC.'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='N' THEN 'NEGOZIO, MAGAZZINO, LABORATORIO, UFFICIO, ETC.'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='R' THEN 'RESIDENZIALE'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='0' THEN 'STANDARD'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='C' THEN 'COOPERATIVE'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='P' THEN '431 P.O.R.'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='A' THEN '392/78 ASSOCIAZIONI'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='D' THEN '431/98 Art.15 R.R.1/2004'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='V' THEN '431/98 Art.15 C.2 R.R.1/2004'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='S' THEN '431/98 SPECIALI'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='Z' THEN 'FORZE DELL''ORDINE'" _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='X' THEN 'CONCESSIONE SPAZI P.' " _
                & " WHEN RAPPORTI_UTENZA.DEST_USO ='Y' THEN 'COMODATO D''USO GRATUITO'  " _
                & " END) AS DEST_D_USO, " _
                & " getdata (data_decorrenza) as data_decorrenza, " _
                & " getdata (data_consegna) as data_consegna, " _
                & " getdata (data_scadenza) as data_scadenza, " _
                & " getdata (data_scadenza_rinnovo) as data_scadenza_rinnovo, " _
                & " getdata (data_disdetta_locatario) as data_disdetta_locatario, " _
                & " RAPPORTI_UTENZA.DURATA_ANNI, " _
                & " RAPPORTI_UTENZA.DURATA_RINNOVO, " _
                & " delibera, " _
                & " getdata (data_delibera) as data_delibera, " _
                & " getdata (MOROSITA_PREGR_DATA_DECORR) AS MOROSITA_PREGR_DATA_DECORR, " _
                & " MOROSITA_PREGR_NUM_ID " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA, siscom_mi.rapporti_utenza_controllo " _
                & " WHERE rapporti_utenza.id = rapporti_utenza_controllo.id_contratto(+) " _
                & " AND morosita_pregr = 1 "
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)

        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Add("ERRORE", Page.Title & " RadGridAzLegali_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub


    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        RadGridAzLegali.AllowPaging = False
        RadGridAzLegali.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In RadGridAzLegali.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In RadGridAzLegali.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In RadGridAzLegali.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In RadGridAzLegali.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        RadGridAzLegali.AllowPaging = True
        RadGridAzLegali.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        RadGridAzLegali.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportDecrDecadenza", "DecrDecadenza", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('');</script>")
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

End Class
