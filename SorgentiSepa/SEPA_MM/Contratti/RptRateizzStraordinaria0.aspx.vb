Imports System.Data
Imports Telerik.Web.UI

Partial Class Contratti_RptRateizzStraordinaria0
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGrid1.ClientID
        End If
    End Sub

    Protected Sub Esporta_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ''04/09/2019 max, questa procedura non funziona con un numero elevato di record in quanto il primo
        ''rebind fa andare tutto in timeout.
        ''Viene Passata alla funzione direttamente la datatable in sessione
        Esporta(CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable))
        'RadGrid1.AllowPaging = False
        'RadGrid1.Rebind()
        'Dim dtRecords As New DataTable()
        'For Each col As GridColumn In RadGrid1.Columns
        '    Dim colString As New DataColumn(col.UniqueName)
        '    If col.Visible = True Then
        '        dtRecords.Columns.Add(colString)
        '    End If
        'Next
        'For Each row As GridDataItem In RadGrid1.Items
        '    ' loops through each rows in RadGrid
        '    Dim dr As DataRow = dtRecords.NewRow()
        '    For Each col As GridColumn In RadGrid1.Columns
        '        'loops through each column in RadGrid
        '        If col.Visible = True Then
        '            dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
        '        End If
        '    Next
        '    dtRecords.Rows.Add(dr)
        'Next
        'Dim i As Integer = 0
        'For Each col As GridColumn In RadGrid1.Columns
        '    If col.Visible = True Then
        '        Dim colString As String = col.HeaderText
        '        dtRecords.Columns(i).ColumnName = colString
        '        i += 1
        '    End If
        'Next
        'Esporta(dtRecords)
        'RadGrid1.AllowPaging = True
        'RadGrid1.Rebind()
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

    Protected Sub RadGrid1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Try
            ''MAX rifaccio la query usando le colonne che effettimanente si vedono in griglia e che saranno pronte poi per l'export
            par.cmd.CommandText = " select RAPPORTI_UTENZA.COD_CONTRATTO AS ""COD_CONTRATTO"",siscom_mi.getintestatari(rapporti_utenza.id) as INTESTATARIO," _
                & " INDIRIZZI.DESCRIZIONE ||' '||INDIRIZZI.CIVICO AS INDIRIZZO,domande_bando_vsa.pg as ""PG_DOMANDA""," _
                & " (case when vsa_decisioni_rev_c.cod_decisione = 1 then 'SOTTOPOSTA A DECISIONE' " _
                & " when vsa_decisioni_rev_c.cod_decisione = 2 then 'ACCOLTA' when vsa_decisioni_rev_c.cod_decisione = 3 then 'NON ACCOLTA' " _
                & " when vsa_decisioni_rev_c.cod_decisione = 4 then 'SOTTOPOSTA A REVISIONE' when vsa_decisioni_rev_c.cod_decisione = 5 then  " _
                & " 'REVISIONE ACCOLTA' when vsa_decisioni_rev_c.cod_decisione = 6 then 'REVISIONE NON ACCOLTA' else 'NESSUNA DECISIONE' end) as ""STATO_DOMANDA"", " _
                & "dichiarazioni_vsa.pg as ""PG_DICHIARAZIONE"", " _
                & " t_stati_dichiarazione.descrizione as ""STATO_DICHIARAZIONE""," _
                & "  vsa_decisioni_rev_c.note," _
                & " getdata(domande_bando_vsa.data_autorizzazione) as ""DATA_AUTORIZZAZIONE"", " _
                & " (case when nvl(domande_bando_vsa.fl_autorizzazione,0) = 1 then 'SI' else 'NO' end) as ""AUTORIZZATA"", " _
                & " getdata(domande_bando_vsa.data_presentazione) as ""DATA_PRESENTAZIONE"", " _
                & " getdata(domande_bando_vsa.data_evento) as ""DATA_EVENTO"" " _
                & " from siscom_mi.rapporti_utenza,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.indirizzi," _
                & " dichiarazioni_vsa,domande_bando_vsa,t_motivo_domanda_vsa,vsa_decisioni_rev_c,t_stati_dichiarazione where" _
                & " unita_immobiliari.id=unita_contrattuale.id_unita" _
                & " and unita_contrattuale.id_unita_principale is null" _
                & " and rapporti_utenza.id=unita_contrattuale.id_contratto" _
                & " and indirizzi.id=unita_immobiliari.id_indirizzo" _
                & " and domande_bando_vsa.contratto_num=rapporti_utenza.cod_contratto and id_motivo_domanda=12 " _
                & " and t_stati_dichiarazione.cod=dichiarazioni_vsa.id_Stato " _
                & " and dichiarazioni_vsa.id=domande_bando_vsa.id_dichiarazione " _
                & " and dichiarazioni_vsa.id_Stato<>2 " _
                & " and t_motivo_domanda_vsa.id = domande_bando_vsa.id_motivo_domanda " _
                & " and (vsa_decisioni_rev_c.data = (select max(data) from vsa_decisioni_rev_c vsdrc where vsdrc.id_domanda=vsa_decisioni_rev_c.id_domanda) " _
                & "  or domande_bando_vsa.id not in (select id_domanda from vsa_decisioni_rev_c) )" _
                & " and vsa_decisioni_rev_c.id_domanda(+) = domande_bando_vsa.id " _
                & " order by 2 asc"
            'par.cmd.CommandText = " select rapporti_utenza.id, cod_contratto,siscom_mi.getintestatari(rapporti_utenza.id) as INTESTATARIO," _
            '    & " INDIRIZZI.DESCRIZIONE ||' '||INDIRIZZI.CIVICO AS INDIRIZZO," _
            '    & " siscom_mi.getstatocontratto(rapporti_utenza.id) as stato_contratto, " _
            '    & " getdata(domande_bando_vsa.data_presentazione) as data_presentazione, " _
            '    & " getdata(domande_bando_vsa.data_autorizzazione) as data_autorizzazione, " _
            '    & " getdata(domande_bando_vsa.data_evento) as data_evento,dichiarazioni_vsa.pg as pg_dich, " _
            '    & " domande_bando_vsa.pg as pg_dom, vsa_decisioni_rev_c.note," _
            '    & " t_stati_dichiarazione.descrizione as stato_dich," _
            '    & " (case when nvl(domande_bando_vsa.fl_autorizzazione,0) = 1 then 'SI' else 'NO' end) as fl_autorizzazione, " _
            '    & " (case when vsa_decisioni_rev_c.cod_decisione = 1 then 'SOTTOPOSTA A DECISIONE' " _
            '    & " when vsa_decisioni_rev_c.cod_decisione = 2 then 'ACCOLTA' when vsa_decisioni_rev_c.cod_decisione = 3 then 'NON ACCOLTA' " _
            '    & " when vsa_decisioni_rev_c.cod_decisione = 4 then 'SOTTOPOSTA A REVISIONE' when vsa_decisioni_rev_c.cod_decisione = 5 then  " _
            '    & " 'REVISIONE ACCOLTA' when vsa_decisioni_rev_c.cod_decisione = 6 then 'REVISIONE NON ACCOLTA' else 'NESSUNA DECISIONE' end) as stato_domanda " _
            '    & " from siscom_mi.rapporti_utenza,siscom_mi.unita_immobiliari,siscom_mi.unita_contrattuale,siscom_mi.indirizzi," _
            '    & " dichiarazioni_vsa,domande_bando_vsa,t_motivo_domanda_vsa,vsa_decisioni_rev_c,t_stati_dichiarazione where" _
            '    & " unita_immobiliari.id=unita_contrattuale.id_unita" _
            '    & " and unita_contrattuale.id_unita_principale is null" _
            '    & " and rapporti_utenza.id=unita_contrattuale.id_contratto" _
            '    & " and indirizzi.id=unita_immobiliari.id_indirizzo" _
            '    & " and domande_bando_vsa.contratto_num=rapporti_utenza.cod_contratto and id_motivo_domanda=12 " _
            '    & " and t_stati_dichiarazione.cod=dichiarazioni_vsa.id_Stato " _
            '    & " and dichiarazioni_vsa.id=domande_bando_vsa.id_dichiarazione " _
            '    & " and dichiarazioni_vsa.id_Stato<>2 " _
            '    & " and t_motivo_domanda_vsa.id = domande_bando_vsa.id_motivo_domanda " _
            '    & " and (vsa_decisioni_rev_c.data = (select max(data) from vsa_decisioni_rev_c vsdrc where vsdrc.id_domanda=vsa_decisioni_rev_c.id_domanda) " _
            '    & "  or domande_bando_vsa.id not in (select id_domanda from vsa_decisioni_rev_c) )" _
            '    & " and vsa_decisioni_rev_c.id_domanda(+) = domande_bando_vsa.id " _
            '    & " order by 3 asc"
            DT = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = DT
            'dt memorizzata in sessione in modo che venga usata nell'export
            Session.Add("MIADT", DT)

        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Add("ERRORE", Page.Title & " RadGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "ResizeGrid", "ResizeGrid();", True)
    End Sub
End Class
