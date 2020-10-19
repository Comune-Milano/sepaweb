Imports System.IO
Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_StampaPFcorrenti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim totale As Decimal = 0
    Dim totale2 As Decimal = 0
    Dim totale3 As Decimal = 0
    Dim totale4 As Decimal = 0
    Dim stringaCapitoli As String = ""
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
            CreaStringaCapitoli()
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
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_MAIN.ID='" & Request.QueryString("EF_R") & "' "
            Dim LETTORE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LETTORE.Read Then
                Esercizio = "Esercizio Finanziario " & par.FormattaData(par.IfNull(LETTORE("INIZIO"), "")) & " - " & par.FormattaData(par.IfNull(LETTORE("FINE"), ""))
                DATAAL = par.IfNull(LETTORE("FINE"), "")
                ANNO = Left(DATAAL, 4)
                ANNO2 = CStr(CInt(ANNO) + 1)
                Titolo.Text = "Situazione Uscite Correnti - " & Esercizio
            End If
            LETTORE.Close()

            '& "TRIM(TO_CHAR((" _
            '    & "(SELECT SUM(NVL(VALORE_LORDO,0)+NVL(ASSESTAMENTO_VALORE_LORDO,0)+NVL(VARIAZIONI,0)) FROM SISCOM_MI.PF_VOCI_STRUTTURA  WHERE ID_VOCE IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = pf_voci.ID))" _
            '    & "+NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE ID_VOCE_DA IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = pf_voci.ID) AND SUBSTR(DATA_ORA,1,8)>'" & DATAAL & "'),0)" _
            '    & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) FROM SISCOM_MI.EVENTI_VARIAZIONI WHERE ID_VOCE_A IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = pf_voci.ID) AND SUBSTR(DATA_ORA,1,8)>'" & DATAAL & "'),0) " _
            '    & "-NVL((SELECT SUM(NVL(IMPORTO_APPROVATO,0)) FROM SISCOM_MI.PF_ASSESTAMENTO_VOCI,SISCOM_MI.PF_ASSESTAMENTO WHERE ID_VOCE IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = pf_voci.ID) AND PF_ASSESTAMENTO.ID=PF_ASSESTAMENTO_VOCI.ID_ASSESTAMENTO AND ID_STATO>=5 AND DATA_APP_COMUNE>'" & DATAAL & "'),0)" _
            '    & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (SELECT ID FROM siscom_mi.pf_voci a WHERE CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = pf_voci.ID)),0)" _
            '    & "),'999G999G990D99'))" _
            '    & "AS DISPONIBILITA_RESIDUA," _

            If Not IsNothing(Request.QueryString("S")) Then
                If Request.QueryString("S") = "-1" Then
                    condizioneStruttura = ""
                Else
                    condizioneStruttura = " ID_STRUTTURA=" & Request.QueryString("S") & " AND "
                End If
            End If

            Dim dt As New Data.DataTable
            par.cmd.CommandText = "SELECT ID,CODICE FROM SISCOM_MI.PF_VOCI WHERE FL_CC <> 1 And ID_PIANO_FINANZIARIO = '" & Request.QueryString("EF_R") & "' ORDER BY CODICE"
            Dim LettoreVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim voce As Integer = 0
            While LettoreVoci.Read
                voce = par.IfNull(LettoreVoci(0), 0)
                par.cmd.CommandText = "SELECT ID  FROM siscom_mi.pf_voci a WHERE FL_CC =0 And CONNECT_BY_ISLEAF = 1 CONNECT BY PRIOR a.ID = a.id_voce_madre START WITH ID = a.ID AND a.id = '" & voce & "'"
                Dim sottovoci As String = ""
                Dim LettoreSottoVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim i As Integer = 0
                While LettoreSottoVoci.Read
                    If i = 0 Then
                        sottovoci = par.IfNull(LettoreSottoVoci(0), "0")
                    Else
                        sottovoci = sottovoci & "," & par.IfNull(LettoreSottoVoci(0), "0")
                    End If
                    i += 1
                End While
                LettoreSottoVoci.Close()
                If i <> 0 Then
                    par.cmd.CommandText = "SELECT CODICE,DESCRIZIONE," _
                            & "'<a href=""javascript:window.open(''Residui.aspx?ID_STRUTTURA=" & Request.QueryString("S") & "&ID_VOCE='||PF_VOCI.ID||'&ID_PF='||pf_voci.ID_PIANO_FINANZIARIO|| ''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=100,scrollbars=yes'');void(0);"">' ||" _
                            & "TRIM(TO_CHAR(" _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 0  AND DATA_CONSUNTIVAZIONE IS NULL AND DATA_CERTIFICAZIONE IS NULL AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 1  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE IS NULL AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND NVL(DATA_CONSUNTIVAZIONE,30000000) > " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO =1  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE IS NULL AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2 AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & ",'999G999G990D99'))" _
                            & "|| '</a>' " _
                            & "AS DISPONIBILITA_RESIDUA, " _
                            & "'<a href=""javascript:window.open(''VariazioniUsciteCorrenti.aspx?ID_STRUTTURA=" & Request.QueryString("S") & "&ID_VOCE='||PF_VOCI.ID||'&ID_PF='||pf_voci.ID_PIANO_FINANZIARIO|| ''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=150,scrollbars=yes'');void(0);"">' ||" _
                            & "TRIM(TO_CHAR((" _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2))-SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 1  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE IS NULL AND ANNO=" & ANNO & " /*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ AND ID_VOCE_PF IN (" & sottovoci & ")),0)  " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2))-SUM(ROUND(IMPORTO_APPROVATO,2))+SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO=" & ANNO & " /*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ AND ID_VOCE_PF IN (" & sottovoci & ")),0)  " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND NVL(DATA_CONSUNTIVAZIONE,30000000) > " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO=" & ANNO & " /*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ AND ID_VOCE_PF IN (" & sottovoci & ")),0)  " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO <> -1 AND NVL(IMPORTO_RIT_LIQUIDATO,0)=0 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=2  AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL)),0) " _
                            & "+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO <> -1 AND NVL(IMPORTO_RIT_LIQUIDATO,0)=0 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND ID_PAGAMENTO_RIT_LEGGE IS NULL),0) " _
                            & "+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO <> -1 AND NVL(IMPORTO_RIT_LIQUIDATO,0)>0 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "/*-(case when (SELECT SUM((CASE WHEN DATA_CONSUNTIVAZIONE>'" & DATAAL & "' AND PRENOTAZIONI.ANNO='" & ANNO & "' AND PRENOTAZIONI.ID_STATO>0 THEN PRENOTAZIONI.IMPORTO_PRENOTATO-NVL(IMPORTO_APPROVATO,0) ELSE 0 END)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_VOCE_PF IN (" & sottovoci & ")) is null then 0 else(SELECT SUM((CASE WHEN DATA_CONSUNTIVAZIONE>'" & DATAAL & "' AND PRENOTAZIONI.ANNO='" & ANNO & "' AND PRENOTAZIONI.ID_STATO>0 THEN PRENOTAZIONI.IMPORTO_PRENOTATO-NVL(IMPORTO_APPROVATO,0) ELSE 0 END)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_VOCE_PF IN (" & sottovoci & ")) end)" _
                            & "-NVL((SELECT SUM(DECODE(NVL(IMPORTO_APPROVATO,0),0,IMPORTO_PRENOTATO,IMPORTO_APPROVATO)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3 AND DATA_ANNULLO>'" & DATAAL & "' AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "*/),'999G999G990D99'))" _
                            & "|| '</a>' " _
                            & "AS VARIAZIONI, " _
                            & "'<a href=""javascript:window.open(''PagamentiUsciteCorrenti.aspx?ID_STRUTTURA=" & Request.QueryString("S") & "&ID_VOCE='||PF_VOCI.ID||'&ID_PF='||pf_voci.ID_PIANO_FINANZIARIO|| ''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=200,scrollbars=yes'');void(0);"">' ||" _
                            & "TRIM(TO_CHAR((" _
                            & "NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0)" _
                            & "+NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0)" _
                            & "),'999G999G990D99'))" _
                            & "|| '</a>' " _
                            & "AS PAGAMENTI," _
                            & "'<a href=""javascript:window.open(''ResiduiFinali.aspx?ID_STRUTTURA=" & Request.QueryString("S") & "&ID_VOCE='||PF_VOCI.ID||'&ID_PF='||pf_voci.ID_PIANO_FINANZIARIO|| ''',''_blank'',''resizable=yes,height=800,width=1000,top=0,left=250,scrollbars=yes'');void(0);"">' ||" _
                            & "TRIM(TO_CHAR((" _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 0  AND DATA_CONSUNTIVAZIONE IS NULL AND DATA_CERTIFICAZIONE IS NULL AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 1  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE IS NULL AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND NVL(DATA_CONSUNTIVAZIONE,30000000) > " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO =1  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE IS NULL AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO<='" & DATAAL & "' AND ANNO_MANDATO='" & ANNO & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & " " _
                            & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO IN (SELECT DISTINCT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0)" _
                            & "-NVL((SELECT SUM(ROUND(NVL(IMPORTO,0),2)) AS IMPORTO FROM SISCOM_MI.PAGAMENTI_RIT_LIQUIDATI WHERE " & condizioneStruttura & " DATA_MANDATO>'" & DATAAL & "' AND ANNO_MANDATO>='" & ANNO2 & "' AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_STATO=2) AND ID_VOCE_PF IN (" & sottovoci & ")),0)" _
                            & " " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2))-SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 1  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE IS NULL AND ANNO=" & ANNO & " /*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ AND ID_VOCE_PF IN (" & sottovoci & ")),0)  " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2))-SUM(ROUND(IMPORTO_APPROVATO,2))+SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE > " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO=" & ANNO & " /*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ AND ID_VOCE_PF IN (" & sottovoci & ")),0)  " _
                            & "-NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE > " & DATAAL & " AND ANNO=" & ANNO & " /*AND NVL(IMPORTO_LIQUIDATO,0)>0*/ AND ID_VOCE_PF IN (" & sottovoci & ")),0)  " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_PRENOTATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND NVL(DATA_CONSUNTIVAZIONE,30000000) > " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND NVL(DATA_CERTIFICAZIONE,30000000) > " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "-NVL((SELECT SUM(ROUND(IMPORTO_APPROVATO,2)-ROUND(NVL(RIT_LEGGE_IVATA,0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3  AND DATA_CONSUNTIVAZIONE <= " & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND DATA_ANNULLO > " & DATAAL & " AND ANNO=" & ANNO & " AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO <> -1 AND NVL(IMPORTO_RIT_LIQUIDATO,0)=0 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=2  AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL)),0) " _
                            & "+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO <> -1 AND NVL(IMPORTO_RIT_LIQUIDATO,0)=0 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND ID_PAGAMENTO_RIT_LEGGE IS NULL),0) " _
                            & "+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO <> -1 AND NVL(IMPORTO_RIT_LIQUIDATO,0)>0 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "/*-(((case when (SELECT SUM((CASE WHEN DATA_CONSUNTIVAZIONE>'" & DATAAL & "' AND PRENOTAZIONI.ANNO='" & ANNO & "' AND PRENOTAZIONI.ID_STATO>0 THEN PRENOTAZIONI.IMPORTO_PRENOTATO-NVL(IMPORTO_APPROVATO,0) ELSE 0 END)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_VOCE_PF IN (" & sottovoci & ")) is null then 0 else(SELECT SUM((CASE WHEN DATA_CONSUNTIVAZIONE>'" & DATAAL & "' AND PRENOTAZIONI.ANNO='" & ANNO & "' AND PRENOTAZIONI.ID_STATO>0 THEN PRENOTAZIONI.IMPORTO_PRENOTATO-NVL(IMPORTO_APPROVATO,0) ELSE 0 END)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_VOCE_PF IN (" & sottovoci & ")) end)))" _
                            & "-NVL((SELECT SUM(DECODE(NVL(IMPORTO_APPROVATO,0),0,IMPORTO_PRENOTATO,IMPORTO_APPROVATO)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO=-3 AND DATA_ANNULLO>'" & DATAAL & "' AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ")),0) " _
                            & "*/),'999G999G990D99')) " _
                            & "|| '</a>' " _
                            & "AS RESIDUI_FINALI " _
                            & "FROM SISCOM_MI.PF_VOCI WHERE FL_CC<>1 AND ID_PIANO_FINANZIARIO='" & Request.QueryString("EF_R") & "' AND PF_VOCI.ID='" & voce & "' ORDER BY CODICE"
                    '& "/*+NVL((SELECT SUM(ROUND(NVL(RIT_LEGGE_IVATA, 0),2)) FROM SISCOM_MI.PRENOTAZIONI WHERE " & condizioneStruttura & " ID_STATO= 2 /*AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & "*/ AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND ID_PAGAMENTO_RIT_LEGGE IN (SELECT DISTINCT ID_PAGAMENTO_RIT_LEGGE  FROM SISCOM_MI.PRENOTAZIONI WHERE ID_STATO=2  AND DATA_CONSUNTIVAZIONE <=" & DATAAL & " AND DATA_CERTIFICAZIONE <= " & DATAAL & " AND ANNO='" & ANNO & "' AND ID_VOCE_PF IN (" & sottovoci & ") AND PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE IS NOT NULL)),0)*/ " _
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    da.Fill(dt)
                End If
            End While
            LettoreVoci.Close()
            If dt.Rows.Count > 0 Then
                DataGrid.DataSource = dt
                DataGrid.DataBind()
            Else
                Errore.Text = "Nessun dato disponibile per l'esercizio finanziario selezionato!"
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
        Dim codice As String = ""
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                codice = Trim(e.Item.Cells(0).Text)
                If InStr(stringaCapitoli, codice) > 0 Then
                    totale += CType(par.EliminaLink(e.Item.Cells(2).Text), Double)
                    totale2 += CType(par.EliminaLink(e.Item.Cells(3).Text), Double)
                    totale3 += CType(par.EliminaLink(e.Item.Cells(4).Text), Double)
                    totale4 += CType(par.EliminaLink(e.Item.Cells(5).Text), Double)
                End If
            Case ListItemType.Footer
                e.Item.Cells(2).Text = Format(totale, "##,##0.00")
                e.Item.Cells(3).Text = Format(totale2, "##,##0.00")
                e.Item.Cells(4).Text = Format(totale3, "##,##0.00")
                e.Item.Cells(5).Text = Format(totale4, "##,##0.00")
        End Select
    End Sub
    Private Sub CreaStringaCapitoli()
        ApriConnessione()
        par.cmd.CommandText = "SELECT codice FROM siscom_mi.PF_VOCI WHERE ID_PIANO_FINANZIARIO=" & Request.QueryString("EF_R") & " AND ID_VOCE_MADRE IS NULL"
        Dim lettoreVoci As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While lettoreVoci.Read
            stringaCapitoli = stringaCapitoli & par.IfNull(lettoreVoci(0), "") & ","
        End While
        lettoreVoci.Close()
        stringaCapitoli = Left(stringaCapitoli, Len(stringaCapitoli) - 1)
        chiudiConnessione()
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
End Class