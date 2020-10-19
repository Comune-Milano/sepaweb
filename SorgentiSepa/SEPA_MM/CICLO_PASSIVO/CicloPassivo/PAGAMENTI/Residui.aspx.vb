Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_Residui
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Dim condizioneStruttura As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_RESIDUI") <> 1 Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            CaricaTabella()
        End If
    End Sub
    Protected Sub CaricaTabella()
        Dim Esercizio As String = ""
        Try
            Dim ANNO As String = ""
            Dim ANNO2 As String = ""
            ApriConnessione()
            Dim DATAAL As String = ""
            Dim voce As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID=" & Request.QueryString("ID_VOCE")
            Dim LettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettoreVoce.Read Then
                voce = par.IfNull(LettoreVoce("CODICE"), "") & "  " & par.IfNull(LettoreVoce("DESCRIZIONE"), "")
            End If
            LettoreVoce.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID='" & Request.QueryString("ID_PF") & "' "
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                Esercizio = "Esercizio Finanziario " & par.FormattaData(par.IfNull(LETTORE("INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(LETTORE("FINE"), ""))
                DATAAL = par.IfNull(LETTORE("FINE"), "")
                ANNO = Left(DATAAL, 4)
                ANNO2 = CStr(CInt(ANNO) + 1)
                Titolo.Text = "Dettaglio Residui - Voce " & voce & " - " & Esercizio
            End If
            LETTORE.Close()

            If Not IsNothing(Request.QueryString("ID_STRUTTURA")) Then
                If Request.QueryString("ID_STRUTTURA") = "-1" Then
                    condizioneStruttura = ""
                Else
                    condizioneStruttura = " AND PRENOTAZIONI.ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA") & " "
                End If
            End If

            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
                & "PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "'PRENOTATO' AS STATO_PRENOTAZIONE, " _
                & "TRIM(TO_CHAR(ROUND(IMPORTO_PRENOTATO,2),'999G999G990D99')) AS IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE,PRENOTAZIONI.ID,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("ID_PF") & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0 AND CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("ID_VOCE") & "') " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND ((PRENOTAZIONI.id_stato = 0) or " _
                & "(PRENOTAZIONI.id_stato = 1 and PRENOTAZIONI.data_consuntivazione >'" & DATAAL & "') or " _
                & "(PRENOTAZIONI.id_stato = 2 and PRENOTAZIONI.data_certificazione >'" & DATAAL & "' and PRENOTAZIONI.data_consuntivazione > '" & DATAAL & "'  )) " _
                & "AND (PRENOTAZIONI.IMPORTO_PRENOTATO<>0 OR PRENOTAZIONI.IMPORTO_APPROVATO<>0) " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
                & "PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "'CONSUNTIVATO' AS STATO_PRENOTAZIONE, " _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_APPROVATO,0),2)/*-ROUND(NVL(RIT_LEGGE_IVATA,0),2)*/,'999G999G990D99')) as IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE,PRENOTAZIONI.ID,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("ID_PF") & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0 AND CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("ID_VOCE") & "') " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND DATA_CONSUNTIVAZIONE<='" & DATAAL & "' " _
                & "and ((PRENOTAZIONI.id_stato=1 and PRENOTAZIONI.data_certificazione is null) or " _
                & "(PRENOTAZIONI.id_stato=2 and PRENOTAZIONI.data_certificazione >'" & DATAAL & "'))" _
                & "AND NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)>0 " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
                & "PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "'CERTIFICATO' AS STATO_PRENOTAZIONE, " _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(ROUND(PRENOTAZIONI.IMPORTO_PRENOTATO,2),'999G999G990D99')) ELSE TRIM(TO_CHAR(ROUND(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0),2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE,PRENOTAZIONI.ID,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("ID_PF") & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0 AND CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("ID_VOCE") & "') " _
                & "AND DATA_CERTIFICAZIONE<='" & DATAAL & "' " _
                & "AND ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID AND DATA_MANDATO<='" & DATAAL & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) = 0 OR " _
                & "(SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID AND DATA_MANDATO<='" & DATAAL & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) IS NULL " _
                & ") " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND PRENOTAZIONI.ID_STATO=2 " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
                & "PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "'ANNULLATO' AS STATO_PRENOTAZIONE, " _
                & "/*TRIM(TO_CHAR(DECODE(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0),0,ROUND(IMPORTO_PRENOTATO,2),ROUND(IMPORTO_APPROVATO,2)),'999G999G999G990D99')) AS IMPORTO_PRENOTATO,*/" _
                & "TRIM(TO_CHAR(ROUND(IMPORTO_PRENOTATO,2),'999G999G999G990D99')) AS IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "FORNITORI.RAGIONE_SOCIALE AS FORNITORE,PRENOTAZIONI.ID,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
                & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
                & "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
                & "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("ID_PF") & "' " _
                & "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0 AND CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("ID_VOCE") & "') " _
                & "AND PRENOTAZIONI.DATA_ANNULLO>'" & DATAAL & "' " _
                & "AND PRENOTAZIONI.ID_STATO=-3 " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "ORDER BY 7,1,2 "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                DataGrid.DataSource = dt
                DataGrid.DataBind()
            Else
                Errore.Text = "Nessun dato disponibile per la voce selezionata!"
            End If

            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            btnStampa.Visible = False
            btnExport.Visible = False
            Errore.Text = "Si è verificato un errore durante il caricamento dei dati!"
        End Try
    End Sub
    Protected Sub chiudiConnessione()
        'CHIUSURA CONNESSIONE
        '************************
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        '************************
    End Sub
    Protected Sub ApriConnessione()
        'APERTURA CONNESSIONE E TRANSAZIONE
        '************************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        '************************
    End Sub
  Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim nomeFile As String = par.StampaDataGridPDF(DataGrid, "StampaResidui", Titolo.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub
  Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGrid, "ExportResidui")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub


    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Protected Sub DataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                totale += Math.Round(CType(e.Item.Cells(7).Text, Decimal), 2)
            Case ListItemType.Footer
                e.Item.Cells(7).Text = Format(totale, "#,##0.00")
        End Select
    End Sub

  
  
End Class