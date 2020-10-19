Imports System.IO
Imports System.Math
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ResiduiFinali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Dim totale2 As Decimal = 0
    Dim totale3 As Decimal = 0
    Dim totale4 As Decimal = 0
    Dim totale5 As Decimal = 0
    Dim totale7 As Decimal = 0
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
            CaricaRitLeggeCertificate()
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
                Titolo.Text = "Dettaglio Residui Finali - Voce " & voce & " - " & Esercizio
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
                & "TRIM(TO_CHAR(IMPORTO_PRENOTATO,'999G999G990D99')) AS IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "(CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
                & "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))) " _
                & "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END) AS IMPORTO_LIQUIDATO, " _
                & "TRIM(TO_CHAR(NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))AS RIT_LEGGE, " _
                & "(CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END) AS DIFFERENZA, " _
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
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & condizioneStruttura _
                & "/*AND NVL(IMPORTO_LIQUIDATO,0)=0 */AND ((CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
                & "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99'))) " _
                & "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END)='0,00' " _
                & "OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END)<>'0,00') " _
                & "/*AND ((CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) = 0 OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) IS NULL) " _
                & "*/" _
                & "UNION " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
                & "PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "'CONSUNTIVATO' AS STATO_PRENOTAZIONE, " _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0)/*-NVL(RIT_LEGGE_IVATA,0)*/,'999G999G990D99')) as IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "(CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
                & "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))) " _
                & "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END) AS IMPORTO_LIQUIDATO, " _
                & "TRIM(TO_CHAR(NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))AS RIT_LEGGE, " _
                & "(CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END) AS DIFFERENZA, " _
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
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & condizioneStruttura _
                & "/*AND NVL(IMPORTO_LIQUIDATO,0)=0 */AND ((CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
                & "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99'))) " _
                & "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END)='0,00' " _
                & "OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END)<>'0,00') " _
                & "/*AND ((CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) = 0 OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) IS NULL) " _
                & "*/" _
                & "UNION " _
                & " SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
                & "PF_VOCI.CODICE AS CODICE," _
                & "PF_VOCI.DESCRIZIONE AS VOCE," _
                & "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "'CERTIFICATO' AS STATO_PRENOTAZIONE, " _
                & "(CASE WHEN PRENOTAZIONI.IMPORTO_APPROVATO IS NULL THEN TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_PRENOTATO,'999G999G990D99')) ELSE TRIM(TO_CHAR(NVL(PRENOTAZIONI.IMPORTO_APPROVATO,0)-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) END) AS IMPORTO_PRENOTATO," _
                & "TAB_FILIALI.NOME AS FILIALE, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
                & "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
                & "(CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
                & "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))) " _
                & "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END) AS IMPORTO_LIQUIDATO, " _
                & "TRIM(TO_CHAR(NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))AS RIT_LEGGE, " _
                & "(CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END) AS DIFFERENZA, " _
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
                & "AND ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID AND DATA_MANDATO<='" & DATAAL & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) = 0 OR " _
                & "(SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID AND DATA_MANDATO<='" & DATAAL & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) IS NULL " _
                & ") " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
                & "AND PRENOTAZIONI.ID_STATO=2 " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
                & "AND DATA_cERTIFICAZIONE<='" & DATAAL & "' " _
                & condizioneStruttura _
                & "AND NVL(IMPORTO_LIQUIDATO,0)=0 /*AND ((CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
                & "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99'))) " _
                & "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END)='0,00' " _
                & "OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END)<>'0,00') " _
                & "*//*AND ((CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) = 0 OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) IS NULL)*/ " _
                & "ORDER BY 7,1,2 "
            '& " UNION " _
            '& "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR," _
            '& "PF_VOCI.CODICE AS CODICE," _
            '& "PF_VOCI.DESCRIZIONE AS VOCE," _
            '& "TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO," _
            '& "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
            '& "'RIT. LEGGE CERTIFICATA' AS STATO_PRENOTAZIONE, " _
            '& "TRIM(TO_CHAR(-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99')) AS IMPORTO_PRENOTATO," _
            '& "TAB_FILIALI.NOME AS FILIALE, " _
            '& "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI WHERE ID IN (SELECT ID_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS SERVIZIO, " _
            '& "(SELECT DESCRIZIONE FROM SISCOM_MI.TAB_SERVIZI_VOCI WHERE ID IN (SELECT ID_VOCE_SERVIZIO FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN(SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI AA WHERE AA.ID=PRENOTAZIONI.ID))) AS VOCE_DGR, " _
            '& "(CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
            '& "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))) " _
            '& "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END) AS IMPORTO_LIQUIDATO, " _
            '& "TRIM(TO_CHAR(NVL(RIT_LEGGE_IVATA,0),'999G999G990D99'))AS RIT_LEGGE, " _
            '& "(CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END) AS DIFFERENZA, " _
            '& "FORNITORI.RAGIONE_SOCIALE AS FORNITORE,PRENOTAZIONI.ID,APPALTI.NUM_REPERTORIO " _
            '& "FROM SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI, SISCOM_MI.PRENOTAZIONI, SISCOM_MI.PF_VOCI, SISCOM_MI.TAB_STATI_PAGAMENTI," _
            '& "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.TAB_FILIALI " _
            '& "WHERE PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
            '& "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+) " _
            '& "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
            '& "AND PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID " _
            '& "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
            '& "AND TAB_STATI_PAGAMENTI.ID(+)=PAGAMENTI.ID_STATO " _
            '& "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
            '& "AND TAB_STATI_ODL.ID(+)=MANUTENZIONI.STATO " _
            '& "AND PF_VOCI.ID_PIANO_FINANZIARIO='" & Request.QueryString("ID_PF") & "' " _
            '& "AND PF_VOCI.ID IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0 AND CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("ID_VOCE") & "') " _
            '& "AND DATA_CERTIFICAZIONE<='" & DATAAL & "' " _
            '& "AND ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID AND DATA_MANDATO<='" & DATAAL & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) = 0 OR " _
            '& "(SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PAGAMENTI.ID AND DATA_MANDATO<='" & DATAAL & "' AND PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PF_VOCI.ID) IS NULL " _
            '& ") " _
            '& "AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL " _
            '& "AND PRENOTAZIONI.ID_STATO=2 " _
            '& "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
            '& "AND PRENOTAZIONI.ANNO='" & ANNO & "' " _
            '& "AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=2  AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' " _
            '& "AND ID_VOCE_PF in (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0 AND CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR PF_VOCI.ID=PF_VOCI.ID_VOCE_MADRE START WITH ID='" & Request.QueryString("ID_VOCE") & "') AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL) " _
            '& condizioneStruttura _
            '& "/*AND NVL(IMPORTO_LIQUIDATO,0)=0 */AND ((CASE WHEN ((SELECT SUM(NVL(IMPORTO,0)) FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE PAGAMENTI_LIQUIDATI.ID_VOCE_PF=PRENOTAZIONI.ID_VOCE_PF AND PAGAMENTI_LIQUIDATI.ID_PAGAMENTO=PRENOTAZIONI.ID_PAGAMENTO)>=(FLOOR(PRENOTAZIONI.IMPORTO_APPROVATO-NVL(PRENOTAZIONI.RIT_LEGGE_IVATA,0)))) " _
            '& "THEN (TRIM(TO_CHAR(PRENOTAZIONI.IMPORTO_APPROVATO,'999G999G990D99'))) " _
            '& "ELSE (TRIM(TO_CHAR(0,'999G999G990D99'))) END)='0,00' " _
            '& "OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (TRIM(TO_CHAR(-IMPORTO_PRENOTATO+IMPORTO_APPROVATO,'999G999G990D99'))) ELSE ('0,00') END)<>'0,00') " _
            '& "/*AND ((CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) = 0 OR (CASE WHEN (IMPORTO_PRENOTATO IS NOT NULL AND IMPORTO_APPROVATO IS NOT NULL AND DATA_CONSUNTIVAZIONE > '" & DATAAL & "') THEN (-IMPORTO_PRENOTATO+IMPORTO_APPROVATO) ELSE (0) END) IS NULL)*/ " _
            '& "ORDER BY 7,1,2 "



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
        Dim nomeFile As String = EsportaExcelAutomaticoDaDataGrid_Residui(DataGrid, "ExportResidui")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    

    Protected Sub DataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                'LEGENDA:
                'PRENOTAZIONI DI PAGAMENTI LIQUIDATI IN GIALLO
                'PRENOTAZIONI CHE HANNO SUBITO VARIAZIONI IN BISQUE
                'PRENOTAZIONI DI PAGAMENTI LIQUIDATI CHE HANNO SUBITO VARIAZIONI IN VERDE

                If e.Item.Cells(9).Text <> "&nbsp;" AndAlso CType((e.Item.Cells(9).Text), Double) <> 0 Then

                    If e.Item.Cells(8).Text <> "&nbsp;" AndAlso CType((e.Item.Cells(8).Text), Double) <> 0 Then
                        'VARIAZIONI E LIQUIDAZIONE VERDE
                        e.Item.BackColor = Drawing.Color.GreenYellow

                    Else
                        'SOLO VARIAZIONI BISQUE
                        e.Item.BackColor = Drawing.Color.Bisque
                    End If
                End If


                If e.Item.Cells(8).Text <> "&nbsp;" AndAlso CType((e.Item.Cells(8).Text), Double) <> 0 Then

                    If e.Item.Cells(9).Text <> "&nbsp;" AndAlso CType((e.Item.Cells(9).Text), Double) <> 0 Then
                        'VARIAZIONI E LIQUIDAZIONE VERDE
                        e.Item.BackColor = Drawing.Color.GreenYellow

                    Else
                        'SOLO LIQUIDAZIONI GIALLO
                        e.Item.BackColor = Drawing.Color.Yellow

                    End If
                End If


                If e.Item.BackColor = Drawing.Color.Yellow Or e.Item.BackColor = Drawing.Color.GreenYellow Then
                    e.Item.Visible = False
                Else
                    totale += CType((e.Item.Cells(7).Text), Double)
                    totale2 += CType((e.Item.Cells(8).Text), Double)
                    totale3 += CType((e.Item.Cells(9).Text), Double)
                End If


            Case ListItemType.Footer
                e.Item.Cells(7).Text = Format(totale, "#,##0.00")
                e.Item.Cells(8).Text = Format(totale2, "#,##0.00")
                e.Item.Cells(9).Text = Format(totale3, "#,##0.00")

        End Select
    End Sub

    Function EsportaExcelAutomaticoDaDataGrid_Residui(ByVal datagrid As DataGrid, Optional ByVal nomeFile As String = "", Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True) As String
        'A DIFFERENZA DELLA FUNCTION GLOBAL, QUI BISOGNA ELIMINARE TUTTE LE RIGHE DI PAGAMENTI LIQUIDATI
        Try
            'CONTO IL NUMERO DELLE COLONNE DEL DATAGRID
            Dim NumeroColonneDatagrid As Integer = datagrid.Columns.Count
            'CONTO IL NUMERO DELLE COLONNE VISIBILI DEL DATAGRID
            Dim NumeroColonneVisibiliDatagrid As Integer = 0
            For indiceColonna As Integer = 0 To NumeroColonneDatagrid - 1 Step 1
                If datagrid.Columns.Item(indiceColonna).Visible = True Then
                    NumeroColonneVisibiliDatagrid = NumeroColonneVisibiliDatagrid + 1
                End If
            Next
            'INIZIALIZZAZIONE RIGHE, COLONNE E FILENAME
            Dim FileExcel As New CM.ExcelFile
            Dim indiceRighe As Long = 0
            Dim IndiceColonne As Long = 1
            Dim FileName As String = nomeFile & Format(Now, "yyyyMMddHHmmss")
            Dim LarghezzaMinimaColonna As Integer = 30
            Dim allineamentoCella As String = "Center"
            Dim LarghezzaDataGrid As Integer = Max(datagrid.Width.Value, 200)
            Dim TipoLarghezzaDataGrid As UnitType = datagrid.Width.Type
            Dim LarghezzaColonnaHeader As Decimal = 0
            Dim LarghezzaColonnaItem As Decimal = 0
            'SETTO A ZERO LA VARIABILE DELLE RIGHE
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls"))
                .PrintGridLines = False
                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                .SetDefaultRowHeight(14)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                Dim indiceVisibile As Integer = 1
                For j = 0 To NumeroColonneDatagrid - 1 Step 1
                    'GESTIONE LARGHEZZA DELLE COLONNE TRAMITE FATTORE DATO IN INPUT OPZIONALE
                    If datagrid.Columns.Item(j).Visible = True Then
                        If datagrid.Columns.Item(j).HeaderStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value * 100 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).HeaderStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If

                        If datagrid.Columns.Item(j).ItemStyle.Width.Type = UnitType.Pixel Then
                            If TipoLarghezzaDataGrid = UnitType.Pixel Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value * 100 / LarghezzaDataGrid
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        Else
                            If TipoLarghezzaDataGrid = UnitType.Percentage Then
                                LarghezzaColonnaHeader = datagrid.Columns.Item(j).ItemStyle.Width.Value
                            Else
                                LarghezzaColonnaHeader = 0
                            End If
                        End If
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, Max(LarghezzaMinimaColonna, 30))
                        'GESTIONE DELLE INTESTAZIONI
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, IndiceColonne, indiceVisibile, datagrid.Columns.Item(j).HeaderText, 0)
                        indiceVisibile = indiceVisibile + 1
                    End If
                Next
                indiceRighe = indiceRighe + 1
                For Each Items As DataGridItem In datagrid.Items
                    indiceRighe = indiceRighe + 1
                    Dim Cella As Integer = 0
                    For IndiceColonne = 0 To NumeroColonneDatagrid - 1
                        'RIEPILOGO ALLINEAMENTI
                        'CENTER 2,LEFT 1,RIGHT 3
                        'CONSIDERO DI FORMATO NUMERICO TUTTE LE CELLE CON ALLINEAMENTO A DESTRA
                        If Items.Cells(8).Text = "0,00" Then 'PAGAMENTI NON LIQUIDATI

                            If datagrid.Columns.Item(IndiceColonne).Visible = True Then
                                allineamentoCella = datagrid.Columns.Item(IndiceColonne).ItemStyle.HorizontalAlign
                                Select Case EliminazioneLink
                                    Case False
                                        Select Case allineamentoCella
                                            Case 1
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                            Case 2
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                            Case 3
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), 4)
                                            Case Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), 0)
                                        End Select

                                    Case True
                                        Select Case allineamentoCella
                                            Case 1
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 2
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 3
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", "")), 4)
                                            Case Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                        End Select
                                    Case Else
                                        Select Case allineamentoCella
                                            Case 1
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 2
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                            Case 3
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", "")), 4)
                                            Case Else
                                                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", "")), 0)
                                        End Select
                                End Select
                                Cella = Cella + 1
                            End If
                        End If

                    Next

                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            'COSTRUZIONE ZIPFILE
            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream

            Dim strFile As String
            strFile = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".xls")
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            Dim zipfic As String
            zipfic = System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & FileName & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            File.Delete(strFile)
            Dim FileNameZip As String = FileName & ".zip"
            Return FileNameZip
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub CaricaRitLeggeCertificate()
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
                TitoloRitLeggeCertificate.Text = "Dettaglio ritenute di legge - Voce " & voce & " - " & Esercizio
            End If
            LETTORE.Close()

            If Not IsNothing(Request.QueryString("ID_STRUTTURA")) Then
                If Request.QueryString("ID_STRUTTURA") = "-1" Then
                    condizioneStruttura = ""
                Else
                    condizioneStruttura = " AND PRENOTAZIONI.ID_STRUTTURA=" & Request.QueryString("ID_STRUTTURA") & " "
                End If
            End If

            par.cmd.CommandText = "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'RIT.LEGGE PRENOTATA' AS STATOP,'CERTIFICATA' AS STATOA,ROUND(NVL(RIT_LEGGE_IVATA,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE fl_cc=0 and CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=2  AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' " _
                & "AND ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE fl_cc=0 and CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ")  AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL) " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND NVL(IMPORTO_RIT_LIQUIDATO,0)=0 " _
                & "AND NVL(RIT_LEGGE_IVATA,0)>0 " _
                & "AND PRENOTAZIONI.ID_sTATO<>-1 " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'RIT.LEGGE PRENOTATA' AS STATOP,'NON CERTIFICATA' AS STATOA,ROUND(NVL(RIT_LEGGE_IVATA,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE fl_cc=0 and CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND ID_PAGAMENTO_RIT_LEGGE IS NULL " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND NVL(IMPORTO_RIT_LIQUIDATO,0)=0 " _
                & "AND NVL(RIT_LEGGE_IVATA,0)>0 " _
                & "AND PRENOTAZIONI.ID_sTATO<>-1 " _
                & "ORDER BY 12,13"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                datagridritlegge.DataSource = dt
                datagridritlegge.DataBind()
            Else
                ErroreRitLeggeCertificate.Text = "Nessun dato disponibile per la voce selezionata!"
                ErroreRitLeggeCertificate.Visible = False
                TitoloRitLeggeCertificate.Visible = False
                DataGridRitLegge.Visible = False
                btnStampaRit.Visible = False
                btnExcelRit.Visible = False
            End If

            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ErroreRitLeggeCertificate.Text = "Si è verificato un errore durante il caricamento dei dati!"
        End Try
    End Sub

    Protected Sub DataGridRitLegge_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridRitLegge.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                totale5 += CType((e.Item.Cells(6).Text), Double)
            Case ListItemType.Footer
                e.Item.Cells(6).Text = Format(totale5, "#,##0.00")
        End Select
    End Sub
   

    Protected Sub btnExcelRit_Click(sender As Object, e As System.EventArgs) Handles btnExcelRit.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRitLegge, "ExportResiduiRitLegge")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub btnStampaRit_Click(sender As Object, e As System.EventArgs) Handles btnStampaRit.Click
        Dim nomeFile As String = par.StampaDataGridPDF(DataGridRitLegge, "StampaResiduiRitLegge", TitoloRitLeggeCertificate.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub
End Class
