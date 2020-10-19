Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_ResiduoConsumo
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


            par.cmd.CommandText = "SELECT PROGR,anno ,stato,TAB_STATI_ODL.DESCRIZIONE AS DESC_STATO,(select (case when id_stato=0 then importo_prenotato when id_stato>0 then importo_approvato else 0 end)  from siscom_mi.prenotazioni where id_prenotazione_pagamento=prenotazioni.id )as importo_tot,rit_legge,id_struttura,TAB_FILIALI.nome AS filiale,MANUTENZIONI.descrizione " _
                                & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TAB_FILIALI WHERE " _
                                & "MANUTENZIONI.stato=TAB_STATI_ODL.ID AND TAB_FILIALI.ID(+) = id_struttura " _
                                & " " & par.cmd.CommandText & " AND STATO not in (5,6) and exists (select id from siscom_mi.prenotazioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) AND MANUTENZIONI.id_appalto IN   " _
                                & "(SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo = (SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID = " & Request.QueryString("ID") & " )) " _
                                & "ORDER BY anno,progr ASC"
            par.OracleConn.Open()
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable()
            Dim Totale As Decimal
            da.Fill(dt)
            par.OracleConn.Close()

            For Each r As Data.DataRow In dt.Rows
                Totale = Math.Round(Totale + CDec(par.IfNull(r.Item("importo_tot"), 0)), 2)
            Next
            Me.txtTotAppalto.Text = Format(CDec(Request.QueryString("TOTCON")), "##,##0.00")
            Me.txtTotManutenzioni.Text = Format(Totale, "##,##0.00")
            Me.txtResiduo.Text = Format((CDec(Me.txtTotAppalto.Text.Replace(".", "")) - Totale), "##,##0.00")

            Dim row As Data.DataRow
            row = dt.NewRow()
            row.Item("importo_tot") = Format(Totale, "##,##0.00")
            row.Item("desc_stato") = "TOTALE"
            dt.Rows.Add(row)

            Me.dgvresiduo.DataSource = dt
            Me.dgvresiduo.DataBind()

            'For Each r As DataGridItem In dgvresiduo.Items
            '    r.Cells(par.IndDGC(dgvresiduo, "importo_tot")).Text = Format(CDec(par.IfNull(r.Cells(par.IndDGC(dgvresiduo, "importo_tot")).Text, 0)), "##,##0.00")
            '    r.Cells(par.IndDGC(dgvresiduo, "rit_legge")).Text = Format(CDec(par.IfNull(r.Cells(par.IndDGC(dgvresiduo, "rit_legge")).Text, 0)), "##,##0.00")
            'Next


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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "RESIDUOCONSUMO", "RESIDUOCONSUMO", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub dgvresiduo_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvresiduo.NeedDataSource
        par.cmd.CommandText = "SELECT PROGR,anno ,stato,TAB_STATI_ODL.DESCRIZIONE AS DESC_STATO,(select (case when id_stato=0 then importo_prenotato when id_stato>0 then importo_approvato else 0 end)  from siscom_mi.prenotazioni where id_prenotazione_pagamento=prenotazioni.id )as importo_tot,rit_legge,id_struttura,TAB_FILIALI.nome AS filiale,MANUTENZIONI.descrizione " _
                               & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TAB_FILIALI WHERE " _
                               & "MANUTENZIONI.stato=TAB_STATI_ODL.ID AND TAB_FILIALI.ID(+) = id_struttura " _
                               & " " & par.cmd.CommandText & " AND STATO not in (5,6) and exists (select id from siscom_mi.prenotazioni where manutenzioni.id_prenotazione_pagamento=prenotazioni.id) AND MANUTENZIONI.id_appalto IN   " _
                               & "(SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo = (SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID = " & Request.QueryString("ID") & " )) " _
                               & "ORDER BY anno,progr ASC"
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Dim Totale As Decimal

        For Each r As Data.DataRow In dt.Rows
            Totale = Math.Round(Totale + CDec(par.IfNull(r.Item("importo_tot"), 0)), 2)
        Next
        Me.txtTotAppalto.Text = Format(CDec(Request.QueryString("TOTCON")), "##,##0.00")
        Me.txtTotManutenzioni.Text = Format(Totale, "##,##0.00")
        Me.txtResiduo.Text = Format((CDec(Me.txtTotAppalto.Text.Replace(".", "")) - Totale), "##,##0.00")

        Dim row As Data.DataRow
        row = dt.NewRow()
        row.Item("importo_tot") = Format(Totale, "##,##0.00")
        row.Item("desc_stato") = "TOTALE"
        dt.Rows.Add(row)

        TryCast(sender, RadGrid).DataSource = dt
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_APPALTI_ResiduoConsumo_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
