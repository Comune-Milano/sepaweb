Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data

Partial Class MANUTENZIONI_RisultatiManutenzioniSTR
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set
    End Property
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Session.Item("OPERATORE") = "" Or Session.Item("FL_ESTRAZIONE_STR") <> "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Response.Flush()
            HFGriglia.Value = DataGrid1.ClientID
            Dim sValoreEsercizioFinanziarioR As String = Strings.Trim(Request.QueryString("EF_R"))
            Dim sValoreStruttura As String = Strings.Trim(Request.QueryString("FI"))
            Dim sValoreLotto As String = Strings.Trim(Request.QueryString("LO"))
            Dim sValoreComplesso As String = Strings.Trim(Request.QueryString("CO"))
            Dim sValoreEdificio As String = Strings.Trim(Request.QueryString("ED"))
            Dim sValoreServizio As String = Strings.Trim(Request.QueryString("SE"))
            Dim sValoreFornitore As String = Strings.Trim(Request.QueryString("FO"))
            Dim sValoreAppalto As String = Strings.Trim(Request.QueryString("AP"))
            Dim sValoreBP As String = Strings.Trim(Request.QueryString("BP"))
            Dim sValoreUnita As String = Strings.Trim(Request.QueryString("UI"))
            Dim sValoreData_Dal As String = Request.QueryString("DAL")
            Dim sValoreData_Al As String = Request.QueryString("AL")
            Dim sValoreStato As String = Request.QueryString("ST")
            Dim sValoreProvenienza As String = Request.QueryString("PROVENIENZA")
            Dim sOrdinamento As String = Request.QueryString("ORD")
            Dim sAutorizzazione As String = Request.QueryString("AUT")
            Dim condizioneAutorizzazione As String = ""
            Select Case sAutorizzazione
                Case "0"
                    condizioneAutorizzazione = " AND MANUTENZIONI.FL_AUTORIZZAZIONE=0 "
                Case "1"
                    condizioneAutorizzazione = " AND MANUTENZIONI.FL_AUTORIZZAZIONE=1 "
                Case "-1"
            End Select
            Dim sStringaSql As String = ""
            If par.IfEmpty(sValoreEdificio, "-1") = "-1" Then
                sStringaSql = "SELECT " _
                    & " SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                    & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) AS ""ODL_ANNO""," _
                    & " TO_CHAR(TO_DATE(SUBSTR(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS ""DATA_INIZIO_ORDINE""," _
                    & " TRIM(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE) AS ""UBICAZIONE""," _
                    & " TRIM ( TO_CHAR ( NVL ( (SELECT sum(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO) FROM SISCOM_MI.manutenzioni_interventi WHERE manutenzioni_interventi.id_manutenzione = manutenzioni.id), 0), '9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO"" ," _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_PRENOTATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"", " _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_APPROVATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_CON"", " _
                    & " TRIM(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) AS ""SERVIZIO""," _
                    & " TRIM(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS ""SERVIZIO_VOCI""," _
                    & " (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NOT NULL THEN COD_FORNITORE || ' - ' || TRIM(FORNITORI.RAGIONE_SOCIALE) ELSE COD_FORNITORE || ' - ' || TRIM(FORNITORI.COGNOME)|| ' ' ||TRIM(FORNITORI.NOME) END) AS ""FORNITORE""," _
                    & " TRIM(SISCOM_MI.PF_VOCI.CODICE) AS ""CODICE_BP""," _
                    & " TRIM(SISCOM_MI.PF_VOCI.DESCRIZIONE) AS ""VOCE_BP""," _
                    & " TRIM(SISCOM_MI.TAB_FILIALI.NOME) AS ""STRUTTURA_ALER""," _
                    & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                    & " SISCOM_MI.MANUTENZIONI.PROGR," _
                    & " SISCOM_MI.MANUTENZIONI.ANNO," _
                    & " SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE AS ""DATA_ODL""," _
                    & " SISCOM_MI.MANUTENZIONI.DESCRIZIONE AS ""DESCRIZIONE"" " _
                    & " ,(CASE WHEN SISCOM_MI.MANUTENZIONI.FL_AUTORIZZAZIONE=1 THEN 'AUTORIZZATO' else 'NON AUTORIZZATO' END) AS ""AUTORIZZAZIONE"" " _
                    & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI " _
                    & " WHERE SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_EDIFICIO IS NULL  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE IS NULL " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                    & " AND SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                    & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO<6 " _
                    & " and siscom_mi.manutenzioni.id not in (select id_manutenzione from siscom_mi.integrazione_str where stato=1) " _
                    & condizioneAutorizzazione

                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                End If
                If IsNothing(sValoreStruttura) = False And sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=" & sValoreStruttura
                ElseIf par.IfEmpty(sValoreLotto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.ID_STRUTTURA IN (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE ID=" & sValoreLotto & ") "
                End If
                If par.IfEmpty(sValoreLotto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.APPALTI.ID_LOTTO ='" & sValoreLotto & "' "
                End If
                If par.IfEmpty(sValoreBP, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=" & sValoreBP
                End If
                If par.IfEmpty(sValoreComplesso, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=" & sValoreComplesso
                End If
                If par.IfEmpty(sValoreServizio, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.ID_SERVIZIO='" & sValoreServizio & "' "
                End If
                If par.IfEmpty(sValoreStato, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.STATO=" & sValoreStato
                Else
                    '**** modifica per prendere tutte le manutenzioni in stato emesso o consuntivato
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.STATO in (1,2)"
                End If
                If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                    If par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                    Else
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_FORNITORE=" & sValoreFornitore & ")"
                    End If
                ElseIf par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                End If
                If sValoreData_Dal <> "" Then
                    If sValoreData_Al <> "" Then
                        sStringaSql = sStringaSql & " AND (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " AND SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                    Else
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                    End If
                ElseIf sValoreData_Al <> "" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
                End If
                If sValoreProvenienza = "RICERCA_SFITTI" Then
                    If par.IfEmpty(sValoreUnita, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI=" & sValoreUnita
                    Else
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI IS NOT NULL "
                    End If
                End If
            End If
            If par.IfEmpty(sValoreComplesso, "-1") = "-1" Or par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then
                If sStringaSql <> "" Then sStringaSql = sStringaSql & " UNION "
                sStringaSql = sStringaSql & "  SELECT " _
                    & " SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                    & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO""," _
                    & " TO_CHAR(TO_DATE(SUBSTR(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS ""DATA_INIZIO_ORDINE""," _
                    & " TRIM(SISCOM_MI.EDIFICI.DENOMINAZIONE) AS ""UBICAZIONE""," _
                    & " TRIM ( TO_CHAR ( NVL ( (SELECT sum(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO) FROM SISCOM_MI.manutenzioni_interventi WHERE manutenzioni_interventi.id_manutenzione = manutenzioni.id), 0), '9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO"" ," _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_PRENOTATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_PRE"", " _
                    & " TRIM(TO_CHAR(nvl((select IMPORTO_APPROVATO from SISCOM_MI.prenotazioni where prenotazioni.id=id_prenotazione_pagamento),0),'9G999G999G999G999G990D99')) AS ""IMPORTO_CON"", " _
                    & " TRIM(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) AS ""SERVIZIO""," _
                    & " TRIM(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) AS ""SERVIZIO_VOCI""," _
                    & " (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NOT NULL THEN COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE ELSE COD_FORNITORE || ' - ' || FORNITORI.COGNOME|| ' ' ||FORNITORI.NOME END) AS ""FORNITORE""," _
                    & " TRIM(SISCOM_MI.PF_VOCI.CODICE) AS ""CODICE_BP""," _
                    & " TRIM(SISCOM_MI.PF_VOCI.DESCRIZIONE) AS ""VOCE_BP""," _
                    & " TRIM(SISCOM_MI.TAB_FILIALI.NOME) AS ""STRUTTURA_ALER""," _
                    & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                    & " SISCOM_MI.MANUTENZIONI.PROGR," _
                    & " SISCOM_MI.MANUTENZIONI.ANNO," _
                    & " SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE AS ""DATA_ODL""," _
                    & " SISCOM_MI.MANUTENZIONI.DESCRIZIONE AS ""DESCRIZIONE"" " _
                    & " ,(CASE WHEN SISCOM_MI.MANUTENZIONI.FL_AUTORIZZAZIONE=1 THEN 'AUTORIZZATO' else 'NON AUTORIZZATO' END) AS ""AUTORIZZAZIONE"" " _
                    & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI " _
                    & " WHERE SISCOM_MI.MANUTENZIONI.ID_COMPLESSO IS NULL  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE IS NULL " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                    & " AND SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                    & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                    & " AND SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                    & " AND SISCOM_MI.MANUTENZIONI.STATO<6 " _
                    & " and siscom_mi.manutenzioni.id not in (select id_manutenzione from siscom_mi.integrazione_str where stato=1) " _
                    & condizioneAutorizzazione
                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                End If
                If IsNothing(sValoreStruttura) = False And sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=" & sValoreStruttura
                ElseIf par.IfEmpty(sValoreLotto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.ID_STRUTTURA IN (SELECT ID_FILIALE FROM SISCOM_MI.LOTTI WHERE ID=" & sValoreLotto & ") "
                End If
                If par.IfEmpty(sValoreLotto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.APPALTI.ID_LOTTO ='" & sValoreLotto & "' "
                End If
                If par.IfEmpty(sValoreBP, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=" & sValoreBP
                End If
                If par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=" & sValoreEdificio
                End If
                If par.IfEmpty(sValoreServizio, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.ID_SERVIZIO='" & sValoreServizio & "' "
                End If
                If par.IfEmpty(sValoreStato, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.STATO=" & sValoreStato
                Else
                    '**** modifica per prendere solo manutenzioni in stato emesso o consuntivato
                    sStringaSql = sStringaSql & " AND  SISCOM_MI.MANUTENZIONI.STATO in (1,2)"
                End If
                If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                    If sValoreAppalto <> -1 Then
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                    Else
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_FORNITORE=" & sValoreFornitore & ")"
                    End If
                ElseIf par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                End If
                If sValoreData_Dal <> "" Then
                    If sValoreData_Al <> "" Then
                        sStringaSql = sStringaSql & " AND (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " AND SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                    Else
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                    End If
                ElseIf sValoreData_Al <> "" Then
                    sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
                End If
                If sValoreProvenienza = "RICERCA_SFITTI" Then
                    If par.IfEmpty(sValoreUnita, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI=" & sValoreUnita
                    Else
                        sStringaSql = sStringaSql & " AND SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI IS NOT NULL "
                    End If
                End If
            End If
            sStringaSQL1 = sStringaSql & " ORDER BY anno desc, progr asc"
        End If
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaManutenzioniSTR.aspx""</script>")
    End Sub
    'Protected Sub imgExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgExport.Click
    '    Dim strmZipOutputStream As ZipOutputStream = Nothing
    '    Try
    '        Dim listaOrdini As String = ""
    '        Dim contElementiSelezionati As Integer = 0
    '        For Each elemento As GridDataItem In DataGrid1.Items
    '            If CType(elemento.FindControl("Checkbox1"), CheckBox).Checked = True Then
    '                contElementiSelezionati += 1
    '                If listaOrdini = "" Then
    '                    listaOrdini = " MANUTENZIONI.ID =" & elemento.Item("idmanutenzione").Text
    '                Else
    '                    listaOrdini &= " OR MANUTENZIONI.ID=" & elemento.Item("idmanutenzione").Text
    '                End If

    '            End If
    '        Next
    '        If contElementiSelezionati > 0 Then
    '            listaOrdini = " and (" & listaOrdini & ")"
    '            connData.apri(True)
    '            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.MANUTENZIONI WHERE " & listaOrdini.Replace(" and", "") & " FOR UPDATE NOWAIT"
    '            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '            lettore.Read()
    '            lettore.Close()
    '            Dim adesso As String = Format(Now, "yyyyMMddHHmmss")
    '            Dim xls As New ExcelSiSol
    '            Dim export As String = ""
    '            Dim FileExcel = xls.IstanziaFileExcel()
    '            Dim FileComplessivo = xls.IstanziaFile()
    '            Dim WorkSheet = xls.IstanziaWorkSheet()
    '            par.cmd.CommandText = " SELECT DISTINCT NUM_REPERTORIO AS ""NUMERO CONTRATTO""/*, " _
    '                & " (SELECT GETDATA(B.DATA_INIZIO) FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""DATA INIZIO CONTRATTO"", " _
    '                & " (SELECT GETDATA(B.DATA_FINE) FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""DATA FINE CONTRATTO"", " _
    '                & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS ""CODICE FORNITORE"", " _
    '                & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS ""RAGIONE SOCIALE"", " _
    '                & " (SELECT SUM(IMPORTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=APPALTI.ID_GRUPPO) AS ""IMPORTO LORDO"", " _
    '                & " (SELECT DISTINCT (SCONTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=APPALTI.ID_GRUPPO AND SCONTO_CONSUMO<>0) AS ""RIBASSO D'ASTA"", " _
    '                & " (SELECT SUM(ONERI_SICUREZZA_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=APPALTI.ID_GRUPPO) AS ""ONERI SICUREZZA DIRETTI"", " _
    '                & " (SELECT B.CIG FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""CIG"", " _
    '                & " (SELECT B.CUP FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""CUP"", " _
    '                & " (SELECT OPERATORI.COGNOME||' '||OPERATORI.NOME FROM SEPA.OPERATORI WHERE ID IN (SELECT APPALTI_DL.ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO=APPALTI.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO='30000000')) AS ""DIRETTORE LAVORI"", " _
    '                & " (SELECT B.RUP_COGNOME||' '||B.RUP_NOME FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""RUP"" */" _
    '                & " FROM " _
    '                & " SISCOM_MI.APPALTI " _
    '                & " WHERE ID_STATO=1 AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.STATO IN (1,2)" & listaOrdini & " AND MANUTENZIONI.ID NOT IN (SELECT ID_MANUTENZIONE FROM SISCOM_MI.INTEGRAZIONE_STR WHERE INTEGRAZIONE_STR.STATO=1)) " _
    '                & " ORDER BY 1 "
    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            Dim dtContratti As New Data.DataTable
    '            da.Fill(dtContratti)
    '            da.Dispose()
    '            Dim nome As String = "IntegrazioneSTR-" & Format(Now, "yyyyMMddHHmmss")
    '            Dim objCrc32 As New Crc32()
    '            Dim zipfic As String
    '            zipfic = Server.MapPath("..\..\..\FileTemp\" & nome & ".zip")
    '            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '            strmZipOutputStream.SetLevel(6)
    '            Dim strFile As String
    '            Dim strmFile As FileStream
    '            Dim theEntry As ZipEntry
    '            Dim contatore As Integer = 0
    '            Dim ElencoFile() As String = Nothing
    '            Dim i As Integer = 0
    '            Dim dtInterventi As Data.DataTable
    '            Dim dtConsuntivo As Data.DataTable
    '            Dim dtDGR As Data.DataTable
    '            Dim conteggioInterventi As Integer = 0
    '            Dim dtAppalto As New Data.DataTable
    '            For Each riga As Data.DataRow In dtContratti.Rows
    '                par.cmd.CommandText = " SELECT DISTINCT " _
    '                    & " NUM_REPERTORIO AS ""Numero Contratto"", " _
    '                    & " (SELECT to_date(B.DATA_INIZIO,'YYYYMMDD') FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Data Contratto"", " _
    '                    & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = ID_FORNITORE) AS ""Codice Fornitore"", " _
    '                    & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = ID_FORNITORE) AS ""Descrizione Fornitore"", " _
    '                    & " (SELECT SUBSTR(B.RUP_COGNOME || ' ' || B.RUP_NOME,1,50) FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Codice RUP"", " _
    '                    & " (SELECT B.RUP_COGNOME || ' ' || B.RUP_NOME FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Descrizione RUP"", " _
    '                    & " (SELECT SUBSTR(OPERATORI.COGNOME || ' ' || OPERATORI.NOME,1,50) FROM SEPA.OPERATORI WHERE ID IN (SELECT APPALTI_DL.ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO = APPALTI.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO = '30000000')) AS ""Codice Direttore Lavori"", " _
    '                    & " (SELECT OPERATORI.COGNOME || ' ' || OPERATORI.NOME FROM SEPA.OPERATORI WHERE ID IN (SELECT APPALTI_DL.ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO = APPALTI.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO = '30000000')) AS ""Descrizione Direttore Lavori"", " _
    '                    & " (SELECT to_date(B.DATA_INIZIO,'YYYYMMDD') FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Data Inizio Lavori"", " _
    '                    & " (SELECT to_date(B.DATA_FINE,'YYYYMMDD') FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Data Fine Lavori"", " _
    '                    & " (SELECT SUM (IMPORTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = APPALTI.ID_GRUPPO) AS ""Importo base d'asta"", " _
    '                    & " (SELECT SUM (ONERI_SICUREZZA_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = APPALTI.ID_GRUPPO) AS ""oneri"", " _
    '                    & " -(SELECT MAX (SCONTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = APPALTI.ID_GRUPPO AND SCONTO_CONSUMO <> 0) AS ""% ribasso/aumento"", " _
    '                    & " 0.5*NVL(APPALTI.FL_RIT_LEGGE,0) AS ""ritenuta"", " _
    '                    & " (SELECT B.CUP FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""C.U.P."", " _
    '                    & " (SELECT B.CIG FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""C.I.G."" " _
    '                    & " FROM SISCOM_MI.APPALTI " _
    '                    & " WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "' "
    '                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                dtAppalto = New Data.DataTable
    '                da.Fill(dtAppalto)
    '                da.Dispose()
    '                dtAppalto.Columns.Item("ritenuta").ColumnName = "% ritenuta garanzia assicurazione"
    '                dtAppalto.Columns.Item("oneri").ColumnName = "Importo oneri sicurezza a forfait"
    '                Dim nomeFile As String = "Integrazione Sep@Web - STR Vision - " & riga.Item("NUMERO CONTRATTO").ToString.Replace("/", "_")
    '                FileExcel = xls.CreaFile(FileComplessivo, ExcelSiSol.Estensione.Office2007_xlsx, nomeFile, False)
    '                WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "Dati contrattuali")
    '                xls.LoadExcelFromDT(WorkSheet, dtAppalto, True, False, True)
    '                par.cmd.CommandText = "SELECT DISTINCT CODICE AS ""Codice DGR"",VOCE_SERVIZIO AS ""Descrizione DGR"",SERVIZIO AS ""Servizio"" FROM SISCOM_MI.CODIFICA_STR WHERE ID IN " _
    '                    & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (SELECT MANUTENZIONI.ID_PF_VOCE_IMPORTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "' )) " _
    '                    & " UNION " _
    '                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (SELECT MANUTENZIONI.ID_PF_VOCE_IMPORTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "' )) " _
    '                    & " )"
    '                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                dtDGR = New Data.DataTable
    '                da.Fill(dtDGR)
    '                da.Dispose()
    '                WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "DGR")
    '                xls.LoadExcelFromDT(WorkSheet, dtDGR, True, False, True)
    '                par.cmd.CommandText = "SELECT DISTINCT " _
    '                    & " PROGR||'/'||ANNO AS ""Codice/Numero ODL"", " _
    '                    & " MANUTENZIONI.DESCRIZIONE AS ""Descrizione ODL"", " _
    '                    & " to_date(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') AS ""Data"", " _
    '                    & " (SELECT CODIFICA_STR.CODICE " _
    '                    & " FROM SISCOM_MI.CODIFICA_STR " _
    '                    & " WHERE CODIFICA_STR.ID IN (    SELECT ID " _
    '                    & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
    '                    & " CONNECT BY PRIOR ID = ID_OLD " _
    '                    & " START WITH ID = " _
    '                    & " MANUTENZIONI.ID_PF_VOCE_IMPORTO " _
    '                    & " UNION " _
    '                    & " SELECT ID " _
    '                    & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
    '                    & " CONNECT BY PRIOR ID_OLD = ID " _
    '                    & " START WITH ID = " _
    '                    & " MANUTENZIONI.ID_PF_VOCE_IMPORTO)) " _
    '                    & " AS ""Codice DGR"", " _
    '                    & " MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO AS ""Importo Preventivato"", " _
    '                    & " (select tab_servizi.descrizione from siscom_mi.tab_servizi where id in (select id_Servizio from siscom_mi.pf_voci_importo where pf_voci_importo.id=MANUTENZIONI.ID_PF_VOCE_IMPORTO)) as ""Attività"", " _
    '                    & " (CASE " _
    '                    & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI " _
    '                    & " WHERE UNITA_IMMOBILIARI.ID=MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE)  " _
    '                    & " WHEN TIPOLOGIA = 'EDIFICIO' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_EDIFICIO " _
    '                    & " FROM SISCOM_MI.EDIFICI " _
    '                    & " WHERE EDIFICI.ID = MANUTENZIONI_INTERVENTI.ID_EDIFICIO) " _
    '                    & " WHEN TIPOLOGIA = 'COMPLESSO' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_COMPLESSO " _
    '                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                    & " WHERE COMPLESSI_IMMOBILIARI.ID = " _
    '                    & " MANUTENZIONI_INTERVENTI.ID_COMPLESSO) " _
    '                    & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_IMPIANTO " _
    '                    & " FROM SISCOM_MI.IMPIANTI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_IMPIANTO " _
    '                    & " FROM SISCOM_MI.IMPIANTI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_IMPIANTO " _
    '                    & " FROM SISCOM_MI.IMPIANTI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'UNITA COMUNE' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_UNITA_COMUNE " _
    '                    & " FROM SISCOM_MI.UNITA_COMUNI " _
    '                    & " WHERE NVL (ID_EDIFICIO, 2) > 1 " _
    '                    & " AND NVL (ID_COMPLESSO, 2) > 1 " _
    '                    & " AND UNITA_COMUNI.ID = " _
    '                    & " MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE) " _
    '                    & " WHEN TIPOLOGIA = 'SCALA' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_eDIFICIO||LPAD(SCALE_EDIFICI.DESCRIZIONE,4,'0') FROM SISCOM_MI.EDIFICI,siscom_mi.SCALE_EDIFICI WHERE EDIFICI.ID=SCALE_EDIFICI.ID_EDIFICIO AND SCALE_EDIFICI.ID=MANUTENZIONI_INTERVENTI.ID_sCALA) " _
    '                    & " ELSE " _
    '                    & " 'ALTRO' " _
    '                    & " END) " _
    '                    & " AS ""Codice Elemento Patrimonio"", " _
    '                    & " (CASE " _
    '                    & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' THEN 'UI' " _
    '                    & " WHEN TIPOLOGIA = 'EDIFICIO' THEN 'ED' " _
    '                    & " WHEN TIPOLOGIA = 'COMPLESSO' THEN 'CO' " _
    '                    & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' THEN 'IM' " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' THEN 'IM' " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' THEN 'IM' " _
    '                    & " WHEN TIPOLOGIA = 'UNITA COMUNE' THEN 'UC' " _
    '                    & " WHEN TIPOLOGIA = 'SCALA' THEN 'SC' " _
    '                    & " ELSE 'AL' " _
    '                    & " END) " _
    '                    & " AS ""Tipologia"", /*(CASE WHEN TIPOLOGIA='UNITA IMMOBILIARE' THEN 'UNITA'' IMMOBILIARE'   WHEN TIPOLOGIA='EDIFICIO' THEN 'EDIFICIO'  WHEN TIPOLOGIA='COMPLESSO' THEN 'COMPLESSO'  WHEN TIPOLOGIA='SOLLEVAMENTO' THEN 'IMPIANTO'  WHEN TIPOLOGIA='CENTRALE IDRICA' THEN 'IMPIANTO'  WHEN TIPOLOGIA='CENTRALE TERMICA' THEN 'IMPIANTO'  WHEN TIPOLOGIA='UNITA COMUNE' THEN 'UNITA'' COMUNE'  WHEN TIPOLOGIA='SCALA' THEN 'SCALA'  ELSE   'ALTRO'  END    ) AS ""TIPOLOGIA"",*/ " _
    '                    & " LPAD (MANUTENZIONI_INTERVENTI.ID, 10, '0') AS ""Codice Intervento"", " _
    '                    & " 'INSERT INTO SISCOM_MI.INTEGRAZIONE_STR ( CODICE_ELEMENTO_PATRIMONIO, DATA_ORA_ESTRAZIONE, ID_APPALTO, ID_INTERVENTO, ID_MANUTENZIONE, ID_OPERATORE_ESTRAZIONE, ID_PF_VOCE_IMPORTO, IMPORTO_PREVENTIVO, STATO, TIPOLOGIA) " _
    '                    & " VALUES ('''||(CASE " _
    '                    & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' " _
    '                    & " THEN " _
    '                    & " (SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " _
    '                    & " FROM SISCOM_MI.UNITA_IMMOBILIARI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE = " _
    '                    & " UNITA_IMMOBILIARI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'EDIFICIO' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_EDIFICIO " _
    '                    & " FROM SISCOM_MI.EDIFICI " _
    '                    & " WHERE EDIFICI.ID = MANUTENZIONI_INTERVENTI.ID_EDIFICIO) " _
    '                    & " WHEN TIPOLOGIA = 'COMPLESSO' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_COMPLESSO " _
    '                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                    & " WHERE COMPLESSI_IMMOBILIARI.ID = " _
    '                    & " MANUTENZIONI_INTERVENTI.ID_COMPLESSO) " _
    '                    & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_IMPIANTO " _
    '                    & " FROM SISCOM_MI.IMPIANTI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_IMPIANTO " _
    '                    & " FROM SISCOM_MI.IMPIANTI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_IMPIANTO " _
    '                    & " FROM SISCOM_MI.IMPIANTI " _
    '                    & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
    '                    & " WHEN TIPOLOGIA = 'UNITA COMUNE' " _
    '                    & " THEN " _
    '                    & " (SELECT UNITA_COMUNI.COD_UNITA_COMUNE  " _
    '                    & " FROM SISCOM_MI.UNITA_COMUNI " _
    '                    & " WHERE NVL (ID_EDIFICIO, 2) > 1 " _
    '                    & " AND NVL (ID_COMPLESSO, 2) > 1 " _
    '                    & " AND UNITA_COMUNI.ID = " _
    '                    & " MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE) " _
    '                    & " WHEN TIPOLOGIA = 'SCALA' " _
    '                    & " THEN " _
    '                    & " (SELECT COD_eDIFICIO||LPAD(SCALE_EDIFICI.DESCRIZIONE,4,'0') FROM SISCOM_MI.EDIFICI,siscom_mi.SCALE_EDIFICI WHERE EDIFICI.ID=SCALE_EDIFICI.ID_EDIFICIO AND SCALE_EDIFICI.ID=MANUTENZIONI_INTERVENTI.ID_sCALA) " _
    '                    & " ELSE " _
    '                    & " 'ALTRO' " _
    '                    & " END)||'''," & Format(Now, "yyyyMMddHHmmss") & ",'||MANUTENZIONI.ID_APPALTO||','||MANUTENZIONI_INTERVENTI.ID||','||MANUTENZIONI.ID||'," & Session.Item("ID_OPERATORE") & ",'||MANUTENZIONI.ID_PF_VOCE_IMPORTO||','||nvl(replace(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,',','.'),0)||',1,'''||(CASE " _
    '                    & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' THEN 'UI' " _
    '                    & " WHEN TIPOLOGIA = 'EDIFICIO' THEN 'ED' " _
    '                    & " WHEN TIPOLOGIA = 'COMPLESSO' THEN 'CO' " _
    '                    & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' THEN 'IM' " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' THEN 'IM' " _
    '                    & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' THEN 'IM' " _
    '                    & " WHEN TIPOLOGIA = 'UNITA COMUNE' THEN 'UC' " _
    '                    & " WHEN TIPOLOGIA = 'SCALA' THEN 'SC' " _
    '                    & " ELSE 'AL' " _
    '                    & " END)||''')' AS INSERIMENTO " _
    '                    & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.MANUTENZIONI_INTERVENTI " _
    '                    & " WHERE     ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID " _
    '                    & " FROM siscom_mi.PRENOTAZIONI " _
    '                    & " WHERE TIPO_PAGAMENTO = 3) " _
    '                    & " AND MANUTENZIONI.ID = MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE " _
    '                    & " AND MANUTENZIONI.STATO IN (1, 2) " _
    '                    & listaOrdini _
    '                    & " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "') " _
    '                    & " ORDER BY 2, 1 "
    '                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                dtInterventi = New Data.DataTable
    '                da.Fill(dtInterventi)
    '                da.Dispose()
    '                For Each Item As Data.DataRow In dtInterventi.Rows
    '                    par.cmd.CommandText = Item("INSERIMENTO")
    '                    par.cmd.ExecuteNonQuery()
    '                    conteggioInterventi += 1
    '                Next
    '                dtInterventi.Columns.Remove("INSERIMENTO")
    '                WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "ODL")
    '                xls.LoadExcelFromDT(WorkSheet, dtInterventi, True, False, True)
    '                par.cmd.CommandText = "SELECT '' as ""Codice Progetto Vision""," _
    '                    & "'' as ""Numero Contratto""," _
    '                    & "'' as ""Numero SAL""," _
    '                    & "'' as ""Data SAL""," _
    '                    & "'' as ""Codice ODL""," _
    '                    & "'' as ""Codice Elemento""," _
    '                    & "'' as ""Codice DGR""," _
    '                    & "'' as ""Importo"" " _
    '                    & " FROM DUAL "
    '                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '                dtConsuntivo = New Data.DataTable
    '                da.Fill(dtConsuntivo)
    '                da.Dispose()
    '                WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "Consuntivo")
    '                xls.LoadExcelFromDT(WorkSheet, dtConsuntivo, True, False, True)
    '                xls.SetMetaData(FileComplessivo, nomeFile, "S&S Sistemi & Soluzioni S.r.l.")
    '                If xls.ChiudiDocumentoClean(FileComplessivo, FileExcel) Then
    '                    export = FileExcel.NomeFileStruttura & FileExcel.Estensione
    '                End If
    '                If File.Exists(Server.MapPath("~\/FileTemp\/") & export) Then
    '                    contatore += 1
    '                    strFile = Server.MapPath("~\/FileTemp\/") & export
    '                    strmFile = File.OpenRead(strFile)
    '                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
    '                    Dim sFile As String = Path.GetFileName(strFile)
    '                    theEntry = New ZipEntry(sFile)
    '                    Dim fi As New FileInfo(strFile)
    '                    theEntry.DateTime = fi.LastWriteTime
    '                    theEntry.Size = strmFile.Length
    '                    strmFile.Close()
    '                    objCrc32.Reset()
    '                    objCrc32.Update(abyBuffer)
    '                    theEntry.Crc = objCrc32.Value
    '                    strmZipOutputStream.PutNextEntry(theEntry)
    '                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '                    ReDim Preserve ElencoFile(i)
    '                    ElencoFile(i) = "..\..\..\FileTemp\" & export
    '                    i = i + 1
    '                End If
    '            Next
    '            strmZipOutputStream.Finish()
    '            strmZipOutputStream.Close()
    '            If conteggioInterventi > 0 Then
    '                If Not String.IsNullOrEmpty(nome) Then
    '                    If System.IO.File.Exists(Server.MapPath("~\/FileTemp\/") & nome & ".zip") Then
    '                        DataGrid1.Rebind()
    '                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('File creato correttamente');", True)
    '                        Response.Redirect("../../../FileTemp/" & nome & ".zip", False)
    '                    Else
    '                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Il file non è stato creato correttamente!\nRiprovare o contattare l\'amministratore di Sistema!');", True)
    '                    End If
    '                Else
    '                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Il file non è stato creato correttamente!\nRiprovare o contattare l\'amministratore di Sistema!');", True)
    '                End If
    '            Else
    '                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Interventi selezionati già in elaborazione, nessun intervento estratto!');", True)
    '            End If
    '            connData.chiudi(True)
    '        Else
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Selezionare almeno un ordine!');", True)
    '        End If
    '    Catch EX1 As Oracle.DataAccess.Client.OracleException
    '        If EX1.Number = 54 Then
    '            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Uno degli ordini selezionati è aperto in modifica! Impossibile procedere con l\'estrazione!');", True)
    '        Else
    '            If strmZipOutputStream.IsFinished = False Then
    '                strmZipOutputStream.Finish()
    '                strmZipOutputStream.Close()
    '            End If
    '            If connData.Connessione.State = Data.ConnectionState.Open Then
    '                connData.chiudi(False)
    '            End If
    '            Session.Item("LAVORAZIONE") = "0"
    '            Session.Add("ERRORE", Page.Title & " imgExport_Click - " & EX1.Message)
    '            Response.Redirect("../../../Errore.aspx", False)
    '        End If
    '    Catch ex As Exception
    '        If strmZipOutputStream.IsFinished = False Then
    '            strmZipOutputStream.Finish()
    '            strmZipOutputStream.Close()
    '        End If
    '        If connData.Connessione.State = Data.ConnectionState.Open Then
    '            connData.chiudi(False)
    '        End If
    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", Page.Title & " imgExport_Click - " & ex.Message)
    '        Response.Redirect("../../../Errore.aspx", False)
    '    End Try
    'End Sub
    

    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid1.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" And col.ColumnType.ToUpper <> "GRIDTEMPLATECOLUMN" Then
                    dtRecords.Columns.Add(colString)
                End If
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" And col.ColumnType.ToUpper <> "GRIDTEMPLATECOLUMN" Then
                        dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                    End If
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" And col.ColumnType.ToUpper <> "GRIDTEMPLATECOLUMN" Then
                    Dim colString As String = col.HeaderText
                    dtRecords.Columns(i).ColumnName = colString
                    i += 1
                End If
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "STR", "STR", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub imgExport_Click(sender As Object, e As EventArgs) Handles imgExport.Click
        Dim strmZipOutputStream As ZipOutputStream = Nothing
        Try
            Dim listaOrdini As String = ""
            Dim contElementiSelezionati As Integer = 0
            For Each elemento As GridDataItem In DataGrid1.Items
                If CType(elemento.FindControl("Checkbox1"), CheckBox).Checked = True Then
                    contElementiSelezionati += 1
                    If listaOrdini = "" Then
                        listaOrdini = " MANUTENZIONI.ID =" & elemento.Item("idmanutenzione").Text
                    Else
                        listaOrdini &= " OR MANUTENZIONI.ID=" & elemento.Item("idmanutenzione").Text
                    End If

                End If
            Next
            If contElementiSelezionati > 0 Then
                listaOrdini = " and (" & listaOrdini & ")"
                connData.apri(True)
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.MANUTENZIONI WHERE " & listaOrdini.Replace(" and", "") & " FOR UPDATE NOWAIT"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                lettore.Read()
                lettore.Close()
                Dim adesso As String = Format(Now, "yyyyMMddHHmmss")
                Dim xls As New ExcelSiSol
                Dim export As String = ""
                Dim FileExcel = xls.IstanziaFileExcel()
                Dim FileComplessivo = xls.IstanziaFile()
                Dim WorkSheet = xls.IstanziaWorkSheet()
                par.cmd.CommandText = " SELECT DISTINCT NUM_REPERTORIO AS ""NUMERO CONTRATTO""/*, " _
                    & " (SELECT GETDATA(B.DATA_INIZIO) FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""DATA INIZIO CONTRATTO"", " _
                    & " (SELECT GETDATA(B.DATA_FINE) FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""DATA FINE CONTRATTO"", " _
                    & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS ""CODICE FORNITORE"", " _
                    & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS ""RAGIONE SOCIALE"", " _
                    & " (SELECT SUM(IMPORTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=APPALTI.ID_GRUPPO) AS ""IMPORTO LORDO"", " _
                    & " (SELECT DISTINCT (SCONTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=APPALTI.ID_GRUPPO AND SCONTO_CONSUMO<>0) AS ""RIBASSO D'ASTA"", " _
                    & " (SELECT SUM(ONERI_SICUREZZA_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=APPALTI.ID_GRUPPO) AS ""ONERI SICUREZZA DIRETTI"", " _
                    & " (SELECT B.CIG FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""CIG"", " _
                    & " (SELECT B.CUP FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""CUP"", " _
                    & " (SELECT OPERATORI.COGNOME||' '||OPERATORI.NOME FROM SEPA.OPERATORI WHERE ID IN (SELECT APPALTI_DL.ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO=APPALTI.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO='30000000')) AS ""DIRETTORE LAVORI"", " _
                    & " (SELECT B.RUP_COGNOME||' '||B.RUP_NOME FROM SISCOM_MI.APPALTI B WHERE B.ID=APPALTI.ID_GRUPPO) AS ""RUP"" */" _
                    & " FROM " _
                    & " SISCOM_MI.APPALTI " _
                    & " WHERE ID_STATO=1 AND APPALTI.ID IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.STATO IN (1,2)" & listaOrdini & " AND MANUTENZIONI.ID NOT IN (SELECT ID_MANUTENZIONE FROM SISCOM_MI.INTEGRAZIONE_STR WHERE INTEGRAZIONE_STR.STATO=1)) " _
                    & " ORDER BY 1 "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtContratti As New Data.DataTable
                da.Fill(dtContratti)
                da.Dispose()
                Dim nome As String = "IntegrazioneSTR-" & Format(Now, "yyyyMMddHHmmss")
                Dim objCrc32 As New Crc32()
                Dim zipfic As String
                zipfic = Server.MapPath("..\..\..\FileTemp\" & nome & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                Dim strmFile As FileStream
                Dim theEntry As ZipEntry
                Dim contatore As Integer = 0
                Dim ElencoFile() As String = Nothing
                Dim i As Integer = 0
                Dim dtInterventi As Data.DataTable
                Dim dtConsuntivo As Data.DataTable
                Dim dtDGR As Data.DataTable
                Dim conteggioInterventi As Integer = 0
                Dim dtAppalto As New Data.DataTable
                For Each riga As Data.DataRow In dtContratti.Rows
                    par.cmd.CommandText = " SELECT DISTINCT " _
                        & " NUM_REPERTORIO AS ""Numero Contratto"", " _
                        & " (SELECT to_date(B.DATA_INIZIO,'YYYYMMDD') FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Data Contratto"", " _
                        & " (SELECT COD_FORNITORE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = ID_FORNITORE) AS ""Codice Fornitore"", " _
                        & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = ID_FORNITORE) AS ""Descrizione Fornitore"", " _
                        & " (SELECT SUBSTR(B.RUP_COGNOME || ' ' || B.RUP_NOME,1,50) FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Codice RUP"", " _
                        & " (SELECT B.RUP_COGNOME || ' ' || B.RUP_NOME FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Descrizione RUP"", " _
                        & " (SELECT SUBSTR(OPERATORI.COGNOME || ' ' || OPERATORI.NOME,1,50) FROM SEPA.OPERATORI WHERE ID IN (SELECT APPALTI_DL.ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO = APPALTI.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO = '30000000')) AS ""Codice Direttore Lavori"", " _
                        & " (SELECT OPERATORI.COGNOME || ' ' || OPERATORI.NOME FROM SEPA.OPERATORI WHERE ID IN (SELECT APPALTI_DL.ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO = APPALTI.ID_GRUPPO AND APPALTI_DL.DATA_FINE_INCARICO = '30000000')) AS ""Descrizione Direttore Lavori"", " _
                        & " (SELECT to_date(B.DATA_INIZIO,'YYYYMMDD') FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Data Inizio Lavori"", " _
                        & " (SELECT to_date(B.DATA_FINE,'YYYYMMDD') FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""Data Fine Lavori"", " _
                        & " (SELECT SUM (IMPORTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = APPALTI.ID_GRUPPO) AS ""Importo base d'asta"", " _
                        & " (SELECT SUM (ONERI_SICUREZZA_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = APPALTI.ID_GRUPPO) AS ""oneri"", " _
                        & " -(SELECT MAX (SCONTO_CONSUMO) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = APPALTI.ID_GRUPPO AND SCONTO_CONSUMO <> 0) AS ""% ribasso/aumento"", " _
                        & " 0.5*NVL(APPALTI.FL_RIT_LEGGE,0) AS ""ritenuta"", " _
                        & " (SELECT B.CUP FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""C.U.P."", " _
                        & " (SELECT B.CIG FROM SISCOM_MI.APPALTI B WHERE B.ID = APPALTI.ID_GRUPPO) AS ""C.I.G."" " _
                        & " FROM SISCOM_MI.APPALTI " _
                        & " WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "' "
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    dtAppalto = New Data.DataTable
                    da.Fill(dtAppalto)
                    da.Dispose()
                    dtAppalto.Columns.Item("ritenuta").ColumnName = "% ritenuta garanzia assicurazione"
                    dtAppalto.Columns.Item("oneri").ColumnName = "Importo oneri sicurezza a forfait"
                    Dim nomeFile As String = "Integrazione Sep@Web - STR Vision - " & riga.Item("NUMERO CONTRATTO").ToString.Replace("/", "_")
                    FileExcel = xls.CreaFile(FileComplessivo, ExcelSiSol.Estensione.Office2007_xlsx, nomeFile, False)
                    WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "Dati contrattuali")
                    xls.LoadExcelFromDT(WorkSheet, dtAppalto, True, False, True)
                    par.cmd.CommandText = "SELECT DISTINCT CODICE AS ""Codice DGR"",VOCE_SERVIZIO AS ""Descrizione DGR"",SERVIZIO AS ""Servizio"" FROM SISCOM_MI.CODIFICA_STR WHERE ID IN " _
                        & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (SELECT MANUTENZIONI.ID_PF_VOCE_IMPORTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "' )) " _
                        & " UNION " _
                        & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (SELECT MANUTENZIONI.ID_PF_VOCE_IMPORTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "' )) " _
                        & " )"
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    dtDGR = New Data.DataTable
                    da.Fill(dtDGR)
                    da.Dispose()
                    WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "DGR")
                    xls.LoadExcelFromDT(WorkSheet, dtDGR, True, False, True)
                    par.cmd.CommandText = "SELECT DISTINCT " _
                        & " PROGR||'/'||ANNO AS ""Codice/Numero ODL"", " _
                        & " MANUTENZIONI.DESCRIZIONE AS ""Descrizione ODL"", " _
                        & " to_date(MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') AS ""Data"", " _
                        & " (SELECT CODIFICA_STR.CODICE " _
                        & " FROM SISCOM_MI.CODIFICA_STR " _
                        & " WHERE CODIFICA_STR.ID IN (    SELECT ID " _
                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                        & " CONNECT BY PRIOR ID = ID_OLD " _
                        & " START WITH ID = " _
                        & " MANUTENZIONI.ID_PF_VOCE_IMPORTO " _
                        & " UNION " _
                        & " SELECT ID " _
                        & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                        & " CONNECT BY PRIOR ID_OLD = ID " _
                        & " START WITH ID = " _
                        & " MANUTENZIONI.ID_PF_VOCE_IMPORTO)) " _
                        & " AS ""Codice DGR"", " _
                        & " MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO AS ""Importo Preventivato"", " _
                        & " (select tab_servizi.descrizione from siscom_mi.tab_servizi where id in (select id_Servizio from siscom_mi.pf_voci_importo where pf_voci_importo.id=MANUTENZIONI.ID_PF_VOCE_IMPORTO)) as ""Attività"", " _
                        & " (CASE " _
                        & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' " _
                        & " THEN " _
                        & " (SELECT COD_UNITA_IMMOBILIARE FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                        & " WHERE UNITA_IMMOBILIARI.ID=MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE)  " _
                        & " WHEN TIPOLOGIA = 'EDIFICIO' " _
                        & " THEN " _
                        & " (SELECT COD_EDIFICIO " _
                        & " FROM SISCOM_MI.EDIFICI " _
                        & " WHERE EDIFICI.ID = MANUTENZIONI_INTERVENTI.ID_EDIFICIO) " _
                        & " WHEN TIPOLOGIA = 'COMPLESSO' " _
                        & " THEN " _
                        & " (SELECT COD_COMPLESSO " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                        & " WHERE COMPLESSI_IMMOBILIARI.ID = " _
                        & " MANUTENZIONI_INTERVENTI.ID_COMPLESSO) " _
                        & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' " _
                        & " THEN " _
                        & " (SELECT COD_IMPIANTO " _
                        & " FROM SISCOM_MI.IMPIANTI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
                        & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' " _
                        & " THEN " _
                        & " (SELECT COD_IMPIANTO " _
                        & " FROM SISCOM_MI.IMPIANTI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
                        & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' " _
                        & " THEN " _
                        & " (SELECT COD_IMPIANTO " _
                        & " FROM SISCOM_MI.IMPIANTI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
                        & " WHEN TIPOLOGIA = 'UNITA COMUNE' " _
                        & " THEN " _
                        & " (SELECT COD_UNITA_COMUNE " _
                        & " FROM SISCOM_MI.UNITA_COMUNI " _
                        & " WHERE NVL (ID_EDIFICIO, 2) > 1 " _
                        & " AND NVL (ID_COMPLESSO, 2) > 1 " _
                        & " AND UNITA_COMUNI.ID = " _
                        & " MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE) " _
                        & " WHEN TIPOLOGIA = 'SCALA' " _
                        & " THEN " _
                        & " (SELECT COD_eDIFICIO||LPAD(SCALE_EDIFICI.DESCRIZIONE,4,'0') FROM SISCOM_MI.EDIFICI,siscom_mi.SCALE_EDIFICI WHERE EDIFICI.ID=SCALE_EDIFICI.ID_EDIFICIO AND SCALE_EDIFICI.ID=MANUTENZIONI_INTERVENTI.ID_sCALA) " _
                        & " ELSE " _
                        & " 'ALTRO' " _
                        & " END) " _
                        & " AS ""Codice Elemento Patrimonio"", " _
                        & " (CASE " _
                        & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' THEN 'UI' " _
                        & " WHEN TIPOLOGIA = 'EDIFICIO' THEN 'ED' " _
                        & " WHEN TIPOLOGIA = 'COMPLESSO' THEN 'CO' " _
                        & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' THEN 'IM' " _
                        & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' THEN 'IM' " _
                        & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' THEN 'IM' " _
                        & " WHEN TIPOLOGIA = 'UNITA COMUNE' THEN 'UC' " _
                        & " WHEN TIPOLOGIA = 'SCALA' THEN 'SC' " _
                        & " ELSE 'AL' " _
                        & " END) " _
                        & " AS ""Tipologia"", /*(CASE WHEN TIPOLOGIA='UNITA IMMOBILIARE' THEN 'UNITA'' IMMOBILIARE'   WHEN TIPOLOGIA='EDIFICIO' THEN 'EDIFICIO'  WHEN TIPOLOGIA='COMPLESSO' THEN 'COMPLESSO'  WHEN TIPOLOGIA='SOLLEVAMENTO' THEN 'IMPIANTO'  WHEN TIPOLOGIA='CENTRALE IDRICA' THEN 'IMPIANTO'  WHEN TIPOLOGIA='CENTRALE TERMICA' THEN 'IMPIANTO'  WHEN TIPOLOGIA='UNITA COMUNE' THEN 'UNITA'' COMUNE'  WHEN TIPOLOGIA='SCALA' THEN 'SCALA'  ELSE   'ALTRO'  END    ) AS ""TIPOLOGIA"",*/ " _
                        & " LPAD (MANUTENZIONI_INTERVENTI.ID, 10, '0') AS ""Codice Intervento"", " _
                        & " 'INSERT INTO SISCOM_MI.INTEGRAZIONE_STR ( CODICE_ELEMENTO_PATRIMONIO, DATA_ORA_ESTRAZIONE, ID_APPALTO, ID_INTERVENTO, ID_MANUTENZIONE, ID_OPERATORE_ESTRAZIONE, ID_PF_VOCE_IMPORTO, IMPORTO_PREVENTIVO, STATO, TIPOLOGIA) " _
                        & " VALUES ('''||(CASE " _
                        & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' " _
                        & " THEN " _
                        & " (SELECT UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE " _
                        & " FROM SISCOM_MI.UNITA_IMMOBILIARI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE = " _
                        & " UNITA_IMMOBILIARI.ID) " _
                        & " WHEN TIPOLOGIA = 'EDIFICIO' " _
                        & " THEN " _
                        & " (SELECT COD_EDIFICIO " _
                        & " FROM SISCOM_MI.EDIFICI " _
                        & " WHERE EDIFICI.ID = MANUTENZIONI_INTERVENTI.ID_EDIFICIO) " _
                        & " WHEN TIPOLOGIA = 'COMPLESSO' " _
                        & " THEN " _
                        & " (SELECT COD_COMPLESSO " _
                        & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                        & " WHERE COMPLESSI_IMMOBILIARI.ID = " _
                        & " MANUTENZIONI_INTERVENTI.ID_COMPLESSO) " _
                        & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' " _
                        & " THEN " _
                        & " (SELECT COD_IMPIANTO " _
                        & " FROM SISCOM_MI.IMPIANTI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
                        & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' " _
                        & " THEN " _
                        & " (SELECT COD_IMPIANTO " _
                        & " FROM SISCOM_MI.IMPIANTI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
                        & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' " _
                        & " THEN " _
                        & " (SELECT COD_IMPIANTO " _
                        & " FROM SISCOM_MI.IMPIANTI " _
                        & " WHERE MANUTENZIONI_INTERVENTI.ID_IMPIANTO = IMPIANTI.ID) " _
                        & " WHEN TIPOLOGIA = 'UNITA COMUNE' " _
                        & " THEN " _
                        & " (SELECT UNITA_COMUNI.COD_UNITA_COMUNE  " _
                        & " FROM SISCOM_MI.UNITA_COMUNI " _
                        & " WHERE NVL (ID_EDIFICIO, 2) > 1 " _
                        & " AND NVL (ID_COMPLESSO, 2) > 1 " _
                        & " AND UNITA_COMUNI.ID = " _
                        & " MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE) " _
                        & " WHEN TIPOLOGIA = 'SCALA' " _
                        & " THEN " _
                        & " (SELECT COD_eDIFICIO||LPAD(SCALE_EDIFICI.DESCRIZIONE,4,'0') FROM SISCOM_MI.EDIFICI,siscom_mi.SCALE_EDIFICI WHERE EDIFICI.ID=SCALE_EDIFICI.ID_EDIFICIO AND SCALE_EDIFICI.ID=MANUTENZIONI_INTERVENTI.ID_sCALA) " _
                        & " ELSE " _
                        & " 'ALTRO' " _
                        & " END)||'''," & Format(Now, "yyyyMMddHHmmss") & ",'||MANUTENZIONI.ID_APPALTO||','||MANUTENZIONI_INTERVENTI.ID||','||MANUTENZIONI.ID||'," & Session.Item("ID_OPERATORE") & ",'||MANUTENZIONI.ID_PF_VOCE_IMPORTO||','||nvl(replace(MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,',','.'),0)||',1,'''||(CASE " _
                        & " WHEN TIPOLOGIA = 'UNITA IMMOBILIARE' THEN 'UI' " _
                        & " WHEN TIPOLOGIA = 'EDIFICIO' THEN 'ED' " _
                        & " WHEN TIPOLOGIA = 'COMPLESSO' THEN 'CO' " _
                        & " WHEN TIPOLOGIA = 'SOLLEVAMENTO' THEN 'IM' " _
                        & " WHEN TIPOLOGIA = 'CENTRALE IDRICA' THEN 'IM' " _
                        & " WHEN TIPOLOGIA = 'CENTRALE TERMICA' THEN 'IM' " _
                        & " WHEN TIPOLOGIA = 'UNITA COMUNE' THEN 'UC' " _
                        & " WHEN TIPOLOGIA = 'SCALA' THEN 'SC' " _
                        & " ELSE 'AL' " _
                        & " END)||''')' AS INSERIMENTO " _
                        & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                        & " WHERE     ID_PRENOTAZIONE_PAGAMENTO IN (SELECT ID " _
                        & " FROM siscom_mi.PRENOTAZIONI " _
                        & " WHERE TIPO_PAGAMENTO = 3) " _
                        & " AND MANUTENZIONI.ID = MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE " _
                        & " AND MANUTENZIONI.STATO IN (1, 2) " _
                        & listaOrdini _
                        & " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE NUM_REPERTORIO='" & riga.Item("NUMERO CONTRATTO") & "') " _
                        & " ORDER BY 2, 1 "
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    dtInterventi = New Data.DataTable
                    da.Fill(dtInterventi)
                    da.Dispose()
                    For Each Item As Data.DataRow In dtInterventi.Rows
                        par.cmd.CommandText = Item("INSERIMENTO")
                        par.cmd.ExecuteNonQuery()
                        conteggioInterventi += 1
                    Next
                    dtInterventi.Columns.Remove("INSERIMENTO")
                    WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "ODL")
                    xls.LoadExcelFromDT(WorkSheet, dtInterventi, True, False, True)
                    par.cmd.CommandText = "SELECT '' as ""Codice Progetto Vision""," _
                        & "'' as ""Numero Contratto""," _
                        & "'' as ""Numero SAL""," _
                        & "'' as ""Data SAL""," _
                        & "'' as ""Codice ODL""," _
                        & "'' as ""Codice Elemento""," _
                        & "'' as ""Codice DGR""," _
                        & "'' as ""Importo"", " _
                        & "'' as ""Oneri sicurezza"" " _
                        & " FROM DUAL "
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    dtConsuntivo = New Data.DataTable
                    da.Fill(dtConsuntivo)
                    da.Dispose()
                    WorkSheet = xls.AggiungiNuovoWorkSheetEAssegna(FileComplessivo, WorkSheet, "Consuntivo")
                    xls.LoadExcelFromDT(WorkSheet, dtConsuntivo, True, False, True)
                    xls.SetMetaData(FileComplessivo, nomeFile, "S&S Sistemi & Soluzioni S.r.l.")
                    If xls.ChiudiDocumentoClean(FileComplessivo, FileExcel) Then
                        export = FileExcel.NomeFileStruttura & FileExcel.Estensione
                    End If
                    If File.Exists(Server.MapPath("~\/FileTemp\/") & export) Then
                        contatore += 1
                        strFile = Server.MapPath("~\/FileTemp\/") & export
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                        Dim sFile As String = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile)
                        Dim fi As New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                        ReDim Preserve ElencoFile(i)
                        ElencoFile(i) = "..\..\..\FileTemp\" & export
                        i = i + 1
                    End If
                Next
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                If conteggioInterventi > 0 Then
                    If Not String.IsNullOrEmpty(nome) Then
                        If System.IO.File.Exists(Server.MapPath("~\/FileTemp\/") & nome & ".zip") Then
                            DataGrid1.Rebind()
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('File creato correttamente');", True)
                            'Response.Redirect("../../../FileTemp/" & nome & ".zip", False)
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nome & ".zip','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                        Else
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Il file non è stato creato correttamente!\nRiprovare o contattare l\'amministratore di Sistema!');", True)
                        End If
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Il file non è stato creato correttamente!\nRiprovare o contattare l\'amministratore di Sistema!');", True)
                    End If
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Interventi selezionati già in elaborazione, nessun intervento estratto!');", True)
                End If
                connData.chiudi(True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Selezionare almeno un ordine!');", True)
            End If
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Uno degli ordini selezionati è aperto in modifica! Impossibile procedere con l\'estrazione!');", True)
            Else
                If strmZipOutputStream.IsFinished = False Then
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                End If
                If connData.Connessione.State = Data.ConnectionState.Open Then
                    connData.chiudi(False)
                End If
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", Page.Title & " imgExport_Click - " & EX1.Message)
                Response.Redirect("../../../Errore.aspx", False)
            End If
        Catch ex As Exception
            If strmZipOutputStream.IsFinished = False Then
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
            End If
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " imgExport_Click - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStringaSQL1)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub CheckBoxTutti_CheckedChanged(sender As Object, e As System.EventArgs)
        If CType(sender, RadButton).Checked = True Then
            For Each elemento As GridDataItem In DataGrid1.Items
                CType(elemento.FindControl("Checkbox1"), CheckBox).Checked = True
            Next
        Else
            For Each elemento As GridDataItem In DataGrid1.Items
                CType(elemento.FindControl("Checkbox1"), CheckBox).Checked = False
            Next
        End If
    End Sub

    Private Sub MANUTENZIONI_RisultatiManutenzioniSTR_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
