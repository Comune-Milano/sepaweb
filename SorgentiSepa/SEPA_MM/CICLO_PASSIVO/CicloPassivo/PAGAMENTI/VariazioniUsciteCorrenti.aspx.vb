Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_VariazioniUsciteCorrenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Dim totale2 As Decimal = 0
    Dim totale3 As Decimal = 0
    Dim totale4 As Decimal = 0
    Dim totale5 As Decimal = 0
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
                Titolo.Text = "Dettaglio Variazioni - Voce " & voce & " - " & Esercizio
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
                & "'PRENOTATO' AS STATOP,'CONSUNTIVATO' AS STATOA,-ROUND(NVL(IMPORTO_PRENOTATO,0)-NVL(IMPORTO_APPROVATO,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ID_STATO = 1 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND DATA_CONSUNTIVAZIONE>'" & DATAAL & "' " _
                & "AND DATA_CERTIFICAZIONE IS NULL " _
                & "AND (-ROUND(NVL(IMPORTO_PRENOTATO,0)-NVL(IMPORTO_APPROVATO,0),2))<>0 " _
                & "/*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'PRENOTATO' AS STATOP,'CERTIFICATO' AS STATOA,-ROUND(NVL(IMPORTO_PRENOTATO,0)-NVL(IMPORTO_APPROVATO,0)+NVL(RIT_LEGGE_IVATA,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ID_STATO = 2 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND DATA_CONSUNTIVAZIONE>'" & DATAAL & "' " _
                & "AND DATA_CERTIFICAZIONE>'" & DATAAL & "' " _
                & "AND (-ROUND(NVL(IMPORTO_PRENOTATO,0)-NVL(IMPORTO_APPROVATO,0)+NVL(RIT_LEGGE_IVATA,0),2))<>0 " _
                & "/*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'PRENOTATO' AS STATOP,'ANNULLATO' AS STATOA,-ROUND(NVL(IMPORTO_PRENOTATO,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ID_STATO = -3 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND NVl(DATA_CONSUNTIVAZIONE,'30000000')>'" & DATAAL & "' " _
                & "AND NVl(DATA_CERTIFICAZIONE,'30000000')>'" & DATAAL & "' " _
                & "AND PRENOTAZIONI.DATA_ANNULLO>'" & DATAAL & "' " _
                & "AND (-ROUND(NVL(IMPORTO_PRENOTATO,0),2))<>0 " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'CONSUNTIVATO' AS STATOP,'CERTIFICATO' AS STATOA,-ROUND(NVL(RIT_LEGGE_IVATA,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ID_STATO = 2 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND DATA_CONSUNTIVAZIONE<='" & DATAAL & "' " _
                & "AND DATA_CERTIFICAZIONE>'" & DATAAL & "' " _
                & "AND (-ROUND(NVL(RIT_LEGGE_IVATA,0),2))<>0 " _
                & "/*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'CONSUNTIVATO' AS STATOP,'ANNULLATO' AS STATOA,-ROUND(NVL(IMPORTO_PRENOTATO,0)-NVL(RIT_LEGGE_IVATA,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ID_STATO = -3 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND DATA_CONSUNTIVAZIONE<='" & DATAAL & "' " _
                & "AND NVL(DATA_CERTIFICAZIONE,'30000000')>'" & DATAAL & "' " _
                & "AND PRENOTAZIONI.DATA_ANNULLO>'" & DATAAL & "' " _
                & "AND (-ROUND(NVL(IMPORTO_PRENOTATO,0)-NVL(RIT_LEGGE_IVATA,0),2))<>0 " _
                & "UNION ALL " _
                & "SELECT PRENOTAZIONI.ID_PAGAMENTO,MANUTENZIONI.PROGR,CODICE,PF_VOCI.DESCRIZIONE,TIPO_PAGAMENTI.DESCRIZIONE AS DESCRIZIONE_TIPO,TAB_FILIALI.NOME,FORNITORI.RAGIONE_SOCIALE," _
                & "CASE WHEN PRENOTAZIONI.ID_STATO=0 THEN 'EMESSO' WHEN PRENOTAZIONI.ID_STATO=1 THEN 'CONSUNTIVATA' WHEN PRENOTAZIONI.ID_STATO=2 THEN 'CERTIFICATO' ELSE '' END AS STATO," _
                & "(CASE WHEN PRENOTAZIONI.TIPO_PAGAMENTO=3 OR PRENOTAZIONI.TIPO_PAGAMENTO=7 THEN 'N. ODL ' || (CASE WHEN MANUTENZIONI.PROGR IS NOT NULL THEN MANUTENZIONI.PROGR ||'/'|| MANUTENZIONI.ANNO ELSE '' END)  || ' del ' || TO_CHAR(TO_DATE(MANUTENZIONI.DATA_INIZIO_ORDINE,'yyyyMMdd'),'dd/MM/yyyy') ELSE (CASE WHEN(PAGAMENTI.TIPO_PAGAMENTO=4) THEN(PAGAMENTI.DESCRIZIONE) ELSE(APPALTI.DESCRIZIONE || (CASE WHEN(PAGAMENTI.DESCRIZIONE) IS NULL THEN('') ELSE(', ' || PAGAMENTI.DESCRIZIONE) END)) END) END) AS DESCRIZIONE_PAG," _
                & "TRIM(TO_CHAR(ROUND(NVL(IMPORTO_PRENOTATO,0),2),'999G999G990D99')) AS IMPORTO, " _
                & "TRIM(TO_CHAR(ROUND(NVL(RIT_LEGGE_IVATA,0),2),'999G999G990D99')) AS RIT_LEGGE_IVATA, " _
                & "/*TRIM(TO_CHAR((CASE WHEN PRENOTAZIONI.IMPORTO_LIQUIDATO IS NULL THEN 0 ELSE PRENOTAZIONI.IMPORTO_LIQUIDATO END),'999G999G990D99')) AS IMPORTO_LIQUIDATO,*/ " _
                & "'CERTIFICATO' AS STATOP,'ANNULLATO' AS STATOA,-ROUND(NVL(IMPORTO_APPROVATO,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "AND PRENOTAZIONI.ID_STATO = -3 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "AND DATA_CONSUNTIVAZIONE<='" & DATAAL & "' " _
                & "AND DATA_CERTIFICAZIONE<='" & DATAAL & "' " _
                & "AND PRENOTAZIONI.DATA_ANNULLO>'" & DATAAL & "' " _
                & "AND (-ROUND(NVL(IMPORTO_APPROVATO,0),2))<>0 " _
                & "ORDER BY 12,13"





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

    Protected Sub DataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                totale4 += CType((e.Item.Cells(10).Text), Double)
            Case ListItemType.Footer
                e.Item.Cells(10).Text = Format(totale4, "#,##0.00")
        End Select
    End Sub

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
                & "'RIT.LEGGE PRENOTATA' AS STATOP,(CASE WHEN (IMPORTO_RIT_LIQUIDATO=RIT_LEGGE_IVATA) THEN ('LIQUIDATA') WHEN (ID_PAGAMENTO_RIT_LEGGE IS NOT NULL) THEN ('CERTIFICATA') ELSE ('NON CERTIFICATA') END) AS STATOA,-ROUND(NVL(RIT_LEGGE_IVATA,0),2) AS VARIAZIONE," _
                & "TRIM(TO_CHAR(NVL(IMPORTO_APPROVATO,0),'999G999G990D99')) AS IMPORTO_CONSUNTIVATO,APPALTI.NUM_REPERTORIO " _
                & "FROM SISCOM_MI.APPALTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.TIPO_PAGAMENTI,SISCOM_MI.MANUTENZIONI,SISCOM_MI.PAGAMENTI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.FORNITORI " _
                & "WHERE ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") " _
                & "AND PRENOTAZIONI.ID_APPALTO=APPALTI.ID(+)" _
                & "AND PRENOTAZIONI.ANNO=" & ANNO & " " _
                & condizioneStruttura _
                & "/*AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=2  AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' " _
                & "AND ID_VOCE_PF in (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 AND FL_CC=0 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = " & Request.QueryString("ID_VOCE") & ") AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL)*/ " _
                & "AND PRENOTAZIONI.ID_sTATO<>-1 " _
                & "AND NVL(RIT_LEGGE_IVATA,0)>0 " _
                & "AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF " _
                & "AND PRENOTAZIONI.TIPO_PAGAMENTO=TIPO_PAGAMENTI.ID " _
                & "AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO(+)=PRENOTAZIONI.ID " _
                & "AND PRENOTAZIONI.ID_PAGAMENTO=PAGAMENTI.ID(+) " _
                & "AND TAB_FILIALI.ID=PRENOTAZIONI.ID_STRUTTURA " _
                & "AND FORNITORI.ID=PRENOTAZIONI.ID_FORNITORE " _
                & "ORDER BY 12,13"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                DataGridRitLegge.DataSource = dt
                DataGridRitLegge.DataBind()
            Else
                ErroreRitLeggeCertificate.Text = "Nessun dato disponibile per la voce selezionata!"
                ErroreRitLeggeCertificate.Visible = False
                TitoloRitLeggeCertificate.Visible = False
                DataGridRitLegge.Visible = False
                btnExportRitl.Visible = False
                btnStampaRit.Visible = False
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

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGrid, "ExportStampaUsciteCorrenti")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
        Dim nomeFile As String = par.StampaDataGridPDF(DataGrid, "StampaUsciteCorrenti", Titolo.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub btnExportRitl_Click(sender As Object, e As System.EventArgs) Handles btnExportRitl.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRitLegge, "ExportStampaUsciteCorrentiRit")
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Redirect("../../../FileTemp/" & nomeFile, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If
    End Sub

    Protected Sub btnStampaRit_Click(sender As Object, e As System.EventArgs) Handles btnStampaRit.Click
        Dim nomeFile As String = par.StampaDataGridPDF(DataGridRitLegge, "StampaUsciteCorrentiRit", TitoloRitLeggeCertificate.Text)
        If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            Response.Write("<script>window.open('../../../FileTemp/" & nomeFile & "');</script>")
            FIN.Value = "1"
        Else
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
        End If
    End Sub
End Class