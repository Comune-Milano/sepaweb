Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_StampaPFserv
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("BP_GENERALE") <> 1 Then
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
            ApriConnessione()
            Dim DATAAL As String = ""
            Dim anno As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID='" & Request.QueryString("EF_R") & "' "
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                Esercizio = "Esercizio Finanziario " & par.FormattaData(par.IfNull(LETTORE("INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(LETTORE("FINE"), ""))
                DATAAL = par.IfNull(LETTORE("FINE"), "")
                anno = Left(DATAAL, 4)
                Titolo2.Text = "Situazione Contabile Generale Per Servizi - " & Esercizio
                Titolo.Text = "Situazione Contabile Generale Per Servizi e Voci - " & Esercizio
            End If
            LETTORE.Close()

            par.cmd.CommandText = "SELECT PF_VOCI.CODICE,TAB_SERVIZI.DESCRIZIONE," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN ((id_stato = 0) or (id_stato = 1 and data_consuntivazione >'" & DATAAL & "' and data_certificazione is null) or (id_stato = 2 and data_certificazione >'" & DATAAL & "' and data_consuntivazione > '" & DATAAL & "'  )) THEN IMPORTO_PRENOTATO ELSE 0 END),2),'999G999G990D99')) AS IMPORTO_PRENOTATO," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN (data_consuntivazione <='" & DATAAL & "' and ((id_stato=1 and data_certificazione is null) or (id_stato=2 and data_certificazione >'" & DATAAL & "'))) THEN importo_approvato-nvl(rit_legge_ivata,0) ELSE 0 END),2),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN (data_consuntivazione <='" & DATAAL & "' and ((id_stato=1 and data_certificazione is null) or (id_stato=2 and data_certificazione >'" & DATAAL & "'))) THEN nvl(rit_legge_ivata,0) ELSE 0 END),2),'999G999G990D99')) AS RITENUTE_CONSUNTIVATE," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN (id_stato=2 and data_certificazione<='" & DATAAL & "') THEN IMPORTO_APPROVATO-nvl(rit_legge_ivata,0) ELSE 0 END),2),'999G999G990D99')) AS IMPORTO_CERTIFICATO " _
                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.TAB_SERVIZI, SISCOM_MI.PF_VOCI " _
                & "WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID " _
                & "AND PF_VOCI_IMPORTO.id_servizio=TAB_SERVIZI.ID " _
                & "AND PRENOTAZIONI.id_stato>=0 " _
                & "AND PRENOTAZIONI.id_voce_pf=PF_VOCI.ID " _
                & "AND  PF_VOCI.id_piano_finanziario='" & Request.QueryString("EF_R") & "' " _
                & "AND prenotazioni.anno='" & anno & "' " _
                & "GROUP BY PF_VOCI.codice, PF_VOCI_IMPORTO.id_servizio,TAB_SERVIZI.descrizione ORDER BY PF_VOCI.CODICE, TAB_SERVIZI.descrizione"


            'par.cmd.CommandText = "SELECT " _
            '    & "PF_VOCI.CODICE," _
            '    & "TAB_SERVIZI.DESCRIZIONE," _
            '    & "ROUND(SUM(CASE id_stato WHEN 0 THEN importo_prenotato ELSE 0 END),2) AS IMPORTO_PRENOTATO," _
            '    & "ROUND(SUM(CASE id_stato WHEN 1 THEN importo_approvato ELSE 0 END),2) AS IMPORTO_CONSUNTIVATO, " _
            '    & "'' AS RITENUTE_CONSUNTIVATE," _
            '    & "ROUND(SUM(CASE id_stato WHEN 2 THEN " _
            '    & "CASE WHEN ID_PAGAMENTO_RIT_LEGGE IS NULL THEN (importo_approvato - NVL(rit_legge_ivata,0)) ELSE importo_approvato END  ELSE 0 END),2) AS IMPORTO_CERTIFICATO " _
            '    & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI " _
            '    & "WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID " _
            '    & "AND PF_VOCI_IMPORTO.id_servizio=TAB_SERVIZI.ID " _
            '    & "AND PRENOTAZIONI.id_stato>=0 " _
            '    & "AND PRENOTAZIONI.id_voce_pf=PF_VOCI.ID " _
            '    & "AND  PF_VOCI.id_piano_finanziario='" & Request.QueryString("EF_R") & "' " _
            '    & "GROUP BY PF_VOCI.codice, PF_VOCI_IMPORTO.id_servizio,TAB_SERVIZI.descrizione " _
            '    & "ORDER BY PF_VOCI.CODICE, TAB_SERVIZI.descrizione"



            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)


            par.cmd.CommandText = "SELECT TAB_SERVIZI.DESCRIZIONE," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN ((id_stato = 0) or (id_stato = 1 and data_consuntivazione >'" & DATAAL & "' and data_certificazione is null) or (id_stato = 2 and data_certificazione >'" & DATAAL & "' and data_consuntivazione > '" & DATAAL & "'  )) THEN IMPORTO_PRENOTATO ELSE 0 END),2),'999G999G990D99')) AS IMPORTO_PRENOTATO," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN (data_consuntivazione <='" & DATAAL & "' and ((id_stato=1 and data_certificazione is null) or (id_stato=2 and data_certificazione >'" & DATAAL & "'))) THEN importo_approvato-nvl(rit_legge_ivata,0) ELSE 0 END),2),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN (data_consuntivazione <='" & DATAAL & "' and ((id_stato=1 and data_certificazione is null) or (id_stato=2 and data_certificazione >'" & DATAAL & "'))) THEN nvl(rit_legge_ivata,0) ELSE 0 END),2),'999G999G990D99')) AS RITENUTE_CONSUNTIVATE," _
                & "TRIM(TO_CHAR(ROUND(SUM(CASE WHEN (id_stato=2 and data_certificazione<='" & DATAAL & "') THEN IMPORTO_APPROVATO-nvl(rit_legge_ivata,0) ELSE 0 END),2),'999G999G990D99')) AS IMPORTO_CERTIFICATO " _
                & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.TAB_SERVIZI, SISCOM_MI.PF_VOCI " _
                & "WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID " _
                & "AND PF_VOCI_IMPORTO.id_servizio=TAB_SERVIZI.ID " _
                & "AND PRENOTAZIONI.id_stato>=0 " _
                & "AND PRENOTAZIONI.id_voce_pf=PF_VOCI.ID " _
                & "AND  PF_VOCI.id_piano_finanziario='" & Request.QueryString("EF_R") & "' " _
                & "AND prenotazioni.anno='" & anno & "' " _
                & "GROUP BY PF_VOCI_IMPORTO.id_servizio,TAB_SERVIZI.descrizione " _
                & "ORDER BY  TAB_SERVIZI.descrizione"

            'par.cmd.CommandText = "SELECT " _
            '    & "/*PF_VOCI.CODICE,*/" _
            '    & "TAB_SERVIZI.DESCRIZIONE," _
            '    & "ROUND(SUM(CASE id_stato WHEN 0 THEN importo_prenotato ELSE 0 END),2) AS IMPORTO_PRENOTATO," _
            '    & "ROUND(SUM(CASE id_stato WHEN 1 THEN importo_approvato ELSE 0 END),2) AS IMPORTO_CONSUNTIVATO, " _
            '    & "'' AS RITENUTE_CONSUNTIVATE," _
            '    & "ROUND(SUM(CASE id_stato WHEN 2 THEN " _
            '    & "CASE WHEN ID_PAGAMENTO_RIT_LEGGE IS NULL THEN (importo_approvato - NVL(rit_legge_ivata,0)) ELSE importo_approvato END  ELSE 0 END),2) AS IMPORTO_CERTIFICATO " _
            '    & "FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI " _
            '    & "WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO = PF_VOCI_IMPORTO.ID " _
            '    & "AND PF_VOCI_IMPORTO.id_servizio=TAB_SERVIZI.ID " _
            '    & "AND PRENOTAZIONI.id_stato>=0 " _
            '    & "AND PRENOTAZIONI.id_voce_pf=PF_VOCI.ID " _
            '    & "AND  PF_VOCI.id_piano_finanziario='" & Request.QueryString("EF_R") & "' " _
            '    & "GROUP BY PF_VOCI_IMPORTO.id_servizio,TAB_SERVIZI.descrizione " _
            '    & "ORDER BY  TAB_SERVIZI.descrizione"


            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable
            da2.Fill(dt2)

            If dt.Rows.Count > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                DataGrid2.DataSource = dt2
                DataGrid2.DataBind()
            Else
                Errore.Text = "Nessun dato disponibile per l'esercizio finanziario e selezionato!"
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

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.MultiEsportaExcelAutomaticoDaDataGrid(DataGrid2, DataGrid1, "ExportStampaSitContServ", , , , 1, 0)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim nomeFile As String = par.MultiStampaDataGridPDF(DataGrid2, DataGrid1, "StampaSitContServ", , Titolo2.Text, Titolo.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub
End Class
