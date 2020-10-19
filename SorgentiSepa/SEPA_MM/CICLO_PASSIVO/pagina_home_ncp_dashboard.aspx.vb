Imports System.IO
Imports Telerik.Web.UI
Partial Class CICLO_PASSIVO_pagina_home_ncp_dashboard
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String
    Dim filtriQuery As String
    Dim filtriQueryOdl As String
    'MARIO
    Public lstAppalti As New System.Collections.Generic.List(Of Mario.Appalti)
    Public lstservizi As New System.Collections.Generic.List(Of Mario.VociServizi)
    Public lstprezzi As New System.Collections.Generic.List(Of Mario.ElencoPrezzi)
    Public lstscadenze As New System.Collections.Generic.List(Of Mario.ScadenzeManuali)
    '*** EPIFANI
    Public lstInterventi As New System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi)
    Public lstListaGenerale1 As New System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Public lstListaGenerale2 As New System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)

    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            'Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
        connData = New CM.datiConnessione(par, False, False)
        ' Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            cmbCriticita.LoadContentFile("Gravita.xml")
            CaricaViste()
            SettaStatiSegnalazioni()
            SettaStatiOrdini()
            CaricaAppalti()
            caricaMappa()
            CaricaCategoria()
            HFGriglia.Value = dgvSegnalazioni.ClientID & "," & dgvODL.ClientID
            HFAltezzaFGriglie.Value = "200,200"

            Session.LCID = 1040
            If Not IsNothing(lstListaRapporti) Then
                lstListaRapporti.Clear()
            End If
            'MARIO
            Session.Add("LSTSERVIZI", lstservizi)
            Session.Add("LSTAPPALTI", lstAppalti)
            Session.Add("LSTPREZZI", lstprezzi)
            Session.Add("LSTSCADENZE", lstscadenze)

            '*** EPIFANI
            Session.Add("LSTINTERVENTI", lstInterventi)
            Session.Add("LSTLISTAGENERALE1", lstListaGenerale1)
            Session.Add("LSTLISTAGENERALE2", lstListaGenerale2)

            Try
                Dim connAperta As Boolean = False
                If connData.Connessione.State = Data.ConnectionState.Closed Then
                    connData.apri(False)
                    connAperta = True
                End If
                If Session.Item("LIVELLO") = "1" Then
                    Session.Add("ID_STRUTTURA", "-1")
                Else
                    par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read = True Then
                        Session.Add("ID_STRUTTURA", myReader("ID_UFFICIO"))
                    End If
                    myReader.Close()
                End If
                ImpostaKPI()
                If connAperta = True Then
                    connData.chiudi(False)
                End If
            Catch ex As Exception
                connData.chiudi()
            End Try
        End If
    End Sub
    Private Function EsportaQuerySegnalazioni(Optional ByVal criticica As String = "-1") As String
        Dim query As String = ""
        Dim filtroStatoSegnalazione As String = ""
        If idStatoSegnalazione.Value <> "" Then
            If idStatoSegnalazione.Value = "-1" Then
                filtroStatoSegnalazione = " AND SEGNALAZIONI.ID_STATO IN (0,6,7,10,2)"
            Else
                filtroStatoSegnalazione = " AND SEGNALAZIONI.ID_STATO=" & idStatoSegnalazione.Value
            End If
        End If
        Dim filtroTipologiaSegnalazione As String = ""
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
        Dim condizioneStruttura As String = ""
        If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
        ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
            condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
        Else
            'CONDIZIONE PER ESCLUDERE LA VISIONE
            condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
        End If
        Dim condizioneVista As String = ""

        If idTipologiaSegnalazione.Value = "1" Then
            'CANONE
            If RadButtonBuildingManager.Checked = True Then
                'BUILDING MANAGER
                condizioneVista = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1)=" & idOperatore
            ElseIf RadButtonDirettoreLavori.Checked = True Then
                'DIRETTORE LAVORI
                condizioneVista = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)='" & idOperatore & "')"
                condizioneVista = " and segnalazioni.id_programma_attivita in (select id from siscom_mi.programma_attivita where siscom_mi.getdlfromappalti(programma_attivita.id_gruppo) = " & idOperatore & ")" _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 "
            ElseIf RadButtonFieldQualityManager.Checked = True Then
                'FIELD QUALITY MANAGER
                condizioneVista = condizioneStruttura
            ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                'TECNICO AMMINISTRATIVO
                condizioneVista = condizioneStruttura
            End If
            '***Rif segn. SD 2519/2018***'
            Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
            '***Rif segn. SD 2519/2018***'
            filtroTipologiaSegnalazione = " And NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione) = 1  " & filtroData
        Else

            'EXTRA CANONE
            If RadButtonBuildingManager.Checked = True Then
                'BUILDING MANAGER
                condizioneVista = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1)=" & idOperatore
            ElseIf RadButtonDirettoreLavori.Checked = True Then
                'DIRETTORE LAVORI
                condizioneVista = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)='" & idOperatore & "')"

            ElseIf RadButtonFieldQualityManager.Checked = True Then
                'FIELD QUALITY MANAGER
                condizioneVista = condizioneStruttura
            ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                'TECNICO AMMINISTRATIVO
                condizioneVista = condizioneStruttura
            End If
            filtroTipologiaSegnalazione = " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1 "


        End If



        Dim filtroCrit As String = ""
        If criticica <> "-1" Then
            Dim indice As Integer = 0
            Select Case cmbCriticita.SelectedIndex
                Case 0
                    'TUTTE
                    indice = 5
                Case 1
                    'ROSSO
                    indice = 4
                Case 2
                    'GIALLO
                    indice = 3
                Case 3
                    'VERDE
                    indice = 2
                Case 4
                    'BIANCO
                    indice = 1
                Case 5
                    'BLU
                    indice = 0
            End Select
            If indice < 5 Then
                filtroCrit = " And ID_PERICOLO_SEGNALAZIONE_INIZ=" & indice
            End If
        End If

        Dim filtroCategoria As String = ""
        If IsNumeric(cmbCategoria.SelectedValue) AndAlso cmbCategoria.SelectedValue <> -1 Then
            filtroCategoria = " AND COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_1 = " & par.IfEmpty(cmbCategoria.SelectedValue, "-1")
        End If

        query = " Select  " _
                & " SEGNALAZIONI.ID  " _
                & " ,'false' as CHECK1 " _
                & " ,SEGNALAZIONI.ID AS NUM " _
                & " ,SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,0) AS BUILDING_MANAGER " _
                & " ,ID_PERICOLO_SEGNALAZIONE AS TIPO_INT " _
                & " ,ID_PERICOLO_SEGNALAZIONE, (select DESCRIZIONE FROM SISCOM_MI.PERICOLO_SEGNALAZIONI WHERE ID= ID_PERICOLO_SEGNALAZIONE) AS PERICOLO_SEGNALAZIONE " _
                & " ,REPLACE(TIPO_SEGNALAZIONE_LIVELLO_1.DESCRIZIONE,'#') AS TIPO1 " _
                & " ,REPLACE(TIPO_SEGNALAZIONE_LIVELLO_2.DESCRIZIONE,'#') AS TIPO2 " _
                & " ,TAB_STATI_SEGNALAZIONI.ID AS ID_STATO " _
                & " ,TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO " _
                & " ,(CASE WHEN ID_UNITA IS NOT NULL THEN  " _
                & "     (SELECT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO  " _
                & "         FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI  " _
                & "         WHERE UNITA_IMMOBILIARI.ID=SEGNALAZIONI.ID_UNITA " _
                & "         AND INDIRIZZI.ID=UNITA_IMMOBILIARI.ID_INDIRIZZO " _
                & "     )  " _
                & " ELSE  " _
                & "     (SELECT INDIRIZZI.DESCRIZIONE||' '||INDIRIZZI.CIVICO  " _
                & "         FROM SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI  " _
                & "         WHERE EDIFICI.ID=SEGNALAZIONI.ID_EDIFICIO " _
                & "         AND INDIRIZZI.ID=EDIFICI.ID_INDIRIZZO_PRINCIPALE " _
                & "     ) END) AS INDIRIZZO " _
                & " ,SEGNALAZIONI.NOME ||' '||SEGNALAZIONI.COGNOME_RS AS RICHIEDENTE " _
                & " ,TO_DATE(SUBSTR(SEGNALAZIONI.DATA_ORA_RICHIESTA,1,8),'YYYYMMDD') AS DATA_INSERIMENTO " _
                & " ,(SELECT MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYMMDD')) FROM SISCOM_MI.EVENTI_SEGNALAZIONI WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND VALORE_OLD = 'APERTA' AND VALORE_NEW = 'IN CORSO') AS DATA_PRESA_IN_CARICO " _
                 & " ,(SELECT MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYMMDD')) FROM SISCOM_MI.EVENTI_SEGNALAZIONI WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND VALORE_OLD = 'IN CORSO' AND VALORE_NEW = 'EVASA') AS DATA_CONTABILIZZAZIONE " _
                & " ,(SELECT TO_DATE(MAX(DATA_INIZIO_ORDINE),'YYYYMMDD') FROM SISCOM_MI.MANUTENZIONI WHERE STATO NOT IN (5, 6) AND ID_SEGNALAZIONI = SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                & " ,(SELECT COUNT (SEGNALAZIONI_NOTE.ID_SEGNALAZIONE) FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND SOLLECITO = 1) AS N_SOLLECITI " _
                & " ,(SELECT COUNT (ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE = SEGNALAZIONI.ID) AS FIGLI " _
                & " ,(SELECT COUNT (ID) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = SEGNALAZIONI.ID) AS NUM_MANUTENZIONI " _
                & " ,ROUND(NVL(TEMPO_PRESA_IN_CARICO,0),2) AS TEMPO_PRESA_IN_CARICO " _
                & " ,ROUND(NVL(TEMPO_RISOLUZIONE_TECNICA,0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                & " ,ROUND(NVL(TEMPO_CONTABILIZZAZIONE,0),2) AS TEMPO_CONTABILIZZAZIONE " _
                & " ,(SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=SEGNALAZIONI.ID_EDIFICIO) AS EDIFICIO " _
                & " ,SISCOM_MI.GETNONCONFORMITASEGNALAZIONI(SEGNALAZIONI.ID) AS NON_CONFORMITA     "
        filtriQuery = " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.combinazione_tipologie " _
            & " ,SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 " _
            & " ,SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 " _
            & " ,SISCOM_MI.TAB_STATI_SEGNALAZIONI " _
            & " WHERE  " _
            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
            & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
            & " AND SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_1=TIPO_SEGNALAZIONE_LIVELLO_1.ID(+) " _
            & " AND SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_2=TIPO_SEGNALAZIONE_LIVELLO_2.ID(+) " _
            & " AND SEGNALAZIONI.ID_STATO=TAB_STATI_SEGNALAZIONI.ID(+) " _
             & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & "              segnalazioni.id_tipo_segnalazione " _
                        & "       AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & "              NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & "       AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & "              NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & "       AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & "              NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & "       AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & "              NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & filtroStatoSegnalazione _
                        & filtroCategoria _
            & filtroTipologiaSegnalazione _
            & condizioneVista
        Return query & filtriQuery & filtroCrit
    End Function
    Private Function EsportaQueryODL(Optional ByVal lista As String = "") As String
        Dim condizioneOrdiniEmessi As String = ""
        Dim filtroStatoOrdine As String = ""
        If idStatoOrdine.Value <> "" Then
            filtroStatoOrdine = " AND MANUTENZIONI.STATO=" & idStatoOrdine.Value
        End If
        If ordiniEmessoConSegnalazione.Value = "1" Then
            condizioneOrdiniEmessi = " AND MANUTENZIONI.ID_SEGNALAZIONI IS NOT NULL AND MANUTENZIONI.ID_PIANO_FINANZIARIO IN (SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5) "
            filtroStatoOrdine = ""
        ElseIf ordiniEmessoConSegnalazione.Value = "2" Then
            condizioneOrdiniEmessi = " AND MANUTENZIONI.ID_SEGNALAZIONI IS NULL AND MANUTENZIONI.ID_PIANO_FINANZIARIO IN (SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5) "
            filtroStatoOrdine = ""
        End If
        Dim condizioneRepertorio As String = ""
        If IsNumeric(cmbRepertorio.SelectedValue) AndAlso cmbRepertorio.SelectedValue > 0 Then
            condizioneRepertorio = " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO=" & cmbRepertorio.SelectedValue & ")"
        End If
        Dim queryOdl As String = ""

        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
        Dim condizioneStruttura As String = ""
        If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
        ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
            condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
        Else
            'CONDIZIONE PER ESCLUDERE LA VISIONE
            condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
        End If
        Dim condizioneVista As String = ""
        If RadButtonBuildingManager.Checked = True Then
            'BUILDING MANAGER
            condizioneVista = "AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT EDIFICI.ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")) OR MANUTENZIONI.ID_EDIFICIO IN (SELECT EDIFICI.ID FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")))"
        ElseIf RadButtonDirettoreLavori.Checked = True Then
            'DIRETTORE LAVORI
            condizioneVista = " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO='30000000' AND ID_OPERATORE=" & idOperatore & "))"
        ElseIf RadButtonFieldQualityManager.Checked = True Then
            'FIELD QUALITY MANAGER
            condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
        ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
            'TECNICO AMMINISTRATIVO
            condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
        End If
        queryOdl = "SELECT 'false' as CHECK1, ID, PROGR || '/' || ANNO AS ODL, MANUTENZIONI.ID_SEGNALAZIONI AS SEGNALAZIONE,  " _
            & " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS NUM_REPERTORIO, " _
            & " manutenzioni.stato as id_stato, " _
            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID = MANUTENZIONI.STATO) AS STATO, " _
            & " (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID =MANUTENZIONI.ID_PF_VOCE_IMPORTO) AS VOCE_DGR, " _
            & " (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE ID =MANUTENZIONI.ID_PF_VOCE) AS VOCE_BP, " _
            & " (SELECT MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYY/MM/DD')) FROM SISCOM_MI.EVENTI_MANUTENZIONE  " _
            & " WHERE COD_EVENTO = 'F92' " _
            & " AND MOTIVAZIONE = 'Da Bozza ad Emesso Ordine' AND ID_MANUTENZIONE = MANUTENZIONI.ID) AS DATA_EMISSIONE, " _
            & " TO_DATE(DATA_INIZIO_INTERVENTO,'YYYY/MM/DD') AS DATA_INIZIO_INTERVENTO, " _
            & " TO_DATE(DATA_FINE_INTERVENTO,'YYYY/MM/DD') AS DATA_FINE_INTERVENTO, " _
            & " TO_DATE(MANUTENZIONI.DATA_PGI,'YYYY/MM/DD') AS DATA_PGI, " _
            & " TO_DATE(MANUTENZIONI.DATA_TDL,'YYYY/MM/DD') AS DATA_TDL, " _
            & " (CASE WHEN (SELECT MAX(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO) FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE = MANUTENZIONI.ID) IS NOT NULL THEN " _
            & " TO_DATE((SELECT MAX(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO) FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE = MANUTENZIONI.ID),'YYYY/MM/DD')  " _
            & " ELSE " _
            & " TO_DATE(MANUTENZIONI.DATA_FINE_ORDINE,'YYYY/MM/DD') END) AS DATA_CHIUSURA_LAVORI, " _
            & " (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (ID_PIANO_FINANZIARIO)) AS ID_ESERCIZIO_FINANZIARIO, " _
            & " MANUTENZIONI.IMPORTO_PRESUNTO, " _
            & " PROGR, ANNO, (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS REP_APPALTO, " _
            & "(SELECT ID FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS ID_APPALTO, " _
            & "(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS ID_FORNITORE, " _
            & " MANUTENZIONI.ID_SERVIZIO " _
            & " ,(SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO) AS IMPORTO_PREVENTIVO " _
            & " ,(SELECT IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0) FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO) AS IMPORTO_CONSUNTIVATO "
        filtriQueryOdl = " FROM SISCOM_MI.MANUTENZIONI " _
            & " WHERE STATO IN (0,1,2) " _
            & " AND ID_PF_VOCE_IMPORTO IS NOT NULL " _
            & filtroStatoOrdine _
            & condizioneRepertorio _
            & condizioneOrdiniEmessi _
            & condizioneVista
        Return queryOdl & filtriQueryOdl
    End Function
    Private Sub dgvSegnalazioni_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgvSegnalazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                Case "1"
                    dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "</<tr></table>"
                Case "2"
                    dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "</<tr></table>"
                Case "3"
                    dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "</<tr></table>"
                Case "4"
                    dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "</<tr></table>"
                Case "0"
                    dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                        & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                        & "</<tr></table>"
                Case Else
            End Select
        End If
        If TypeOf e.Item Is GridFilteringItem Then
            Dim fitem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
            par.caricaComboTelerik("SELECT DISTINCT TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS DESCRIZIONE " & filtriQuery _
              & " order by   1 ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoOrdine"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroEventoStatoOrdine.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoOrdine"), RadComboBox).SelectedValue = HFFiltroEventoStatoOrdine.Value.ToString
            End If

            par.caricaComboTelerik("SELECT DISTINCT SISCOM_MI.GETNONCONFORMITASEGNALAZIONI(SEGNALAZIONI.ID) AS DESCRIZIONE " & filtriQuery _
              & " order by   1 ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxNonConformita"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            If Not String.IsNullOrEmpty(Trim(HFFiltroEventoNonConformita.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxNonConformita"), RadComboBox).SelectedValue = HFFiltroEventoNonConformita.Value.ToString
            End If
        End If
    End Sub
    Private Sub ImpostaKPI(Optional ByVal indice As Integer = 0)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim numeroSegnAperte As Integer = 0
            Dim numeroSegnInCorso As Integer = 0
            Dim numeroSegnEvasa As Integer = 0
            Dim numeroSegnChiusa As Integer = 0
            Dim numeroSegnAnnullata As Integer = 0
            'Dim numeroSegnAperte30giorni As Integer = 0
            Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
            Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
            Dim condizioneStruttura As String = ""
            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
            Else
                'CONDIZIONE PER ESCLUDERE LA VISIONE
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
            End If

            Dim filtroCategoria As String = ""
            If IsNumeric(cmbCategoria.SelectedValue) AndAlso cmbCategoria.SelectedValue <> -1 Then
                filtroCategoria = " AND COMBINAZIONE_TIPOLOGIE.ID_TIPO_SEGNALAZIONE_LIVELLO_1 = " & par.IfEmpty(cmbCategoria.SelectedValue, "-1")
            End If

            '******************** KPI 1 ******************** 
            Dim stringaSegnalazioniCanone As String = ""
            If indice = 0 Or indice = 1 Then
                If RadButtonBuildingManager.Checked = True Then
                    'BUILDING MANAGER
                    Dim filtroGroupBy As String = "GROUP BY SEGNALAZIONI.ID_STATO "
                    Dim stringaSegnalazioni As String = "SELECT SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, siscom_mi.combinazione_tipologie " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                        & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                        & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                        & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                        & " AND ID_OPERATORE=" & idOperatore _
                        & " AND EDIFICI.ID>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1 " _
                        & filtroCategoria

                    '***Rif segn. SD 2519/2018***'
                    Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                    '***Rif segn. SD 2519/2018***'
                    Dim stringaSegnalazioniCan As String = "SELECT SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, siscom_mi.combinazione_tipologie " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & " And EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                        & " And INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                        & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                        & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                        & " AND ID_OPERATORE=" & idOperatore _
                        & " AND EDIFICI.ID>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1 " & filtroData _
                        & filtroCategoria
                    'ELENCO SEGNALAZIONI 
                    par.cmd.CommandText = stringaSegnalazioni _
                        & filtroGroupBy
                    stringaSegnalazioniCanone = stringaSegnalazioniCan _
                        & filtroGroupBy

                ElseIf RadButtonDirettoreLavori.Checked = True Then
                    'DIRETTORE LAVORI
                    Dim filtroGroupBy As String = " GROUP BY SEGNALAZIONI.ID_STATO "
                    Dim stringaSegnalazioni As String = "SELECT SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                        & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                        & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                        & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1 " _
                        & filtroCategoria
                    '***Rif segn. SD 2519/2018***'
                    Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                    '***Rif segn. SD 2519/2018***'
                    Dim stringaSegnalazioniCan As String = "SELECT SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, siscom_mi.combinazione_tipologie " _
                        & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE PROGRAMMA_ATTIVITA.ID=SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA " _
                        & " AND SISCOM_MI.GETDLFROMAPPALTI(PROGRAMMA_ATTIVITA.ID_GRUPPO)=" & idOperatore & ") " _
                        & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1 " & filtroData _
                        & filtroCategoria
                    'ELENCO SEGNALAZIONI 
                    par.cmd.CommandText = stringaSegnalazioni _
                        & filtroGroupBy
                    stringaSegnalazioniCanone = stringaSegnalazioniCan _
                        & filtroGroupBy
                ElseIf RadButtonFieldQualityManager.Checked = True Then
                    'FIELD QUALITY MANAGER
                    Dim filtroGroupBy As String = " GROUP BY SEGNALAZIONI.ID_STATO "
                    Dim stringaSegnalazioni As String = "SELECT SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & condizioneStruttura _
                        & " AND EDIFICI.ID>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1 " _
                        & filtroCategoria

                    '***Rif segn. SD 2519/2018***'
                    Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                    '***Rif segn. SD 2519/2018***'
                    Dim stringaSegnalazioniCan As String = "Select SEGNALAZIONI.ID_STATO, COUNT(SEGNALAZIONI.ID) As CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI,combinazione_tipologie " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & condizioneStruttura _
                        & " And EDIFICI.ID>1 " _
                        & " And SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " And ID_SEGNALAZIONE_PADRE Is NULL " _
                        & " And combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1  " & filtroData _
                        & filtroCategoria
                    'ELENCO SEGNALAZIONI 
                    par.cmd.CommandText = stringaSegnalazioni _
                        & filtroGroupBy
                    stringaSegnalazioniCanone = stringaSegnalazioniCan _
                       & filtroGroupBy
                ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                    'TECNICO AMMINISTRATIVO
                    Dim filtroGroupBy As String = " GROUP BY SEGNALAZIONI.ID_STATO "
                    Dim stringaSegnalazioni As String = "Select SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) As CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, COMBINAZIONE_TIPOLOGIE " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & condizioneStruttura _
                        & " And EDIFICI.ID>1 " _
                        & " And SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " And ID_SEGNALAZIONE_PADRE Is NULL " _
                        & " And combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " And nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1 " _
                        & filtroCategoria

                    '***Rif segn. SD 2519/2018***'
                    Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                    '***Rif segn. SD 2519/2018***'
                    Dim stringaSegnalazioniCan As String = "Select SEGNALAZIONI.ID_STATO,COUNT(SEGNALAZIONI.ID) As CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI,combinazione_tipologie " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & condizioneStruttura _
                        & " And EDIFICI.ID>1 " _
                        & " And SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " And ID_SEGNALAZIONE_PADRE Is NULL " _
                        & " And combinazione_tipologie.id_tipo_segnalazione(+) = " _
                        & " segnalazioni.id_tipo_segnalazione " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                        & " And NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                        & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                        & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1  " & filtroData _
                        & filtroCategoria
                    'ELENCO SEGNALAZIONI 
                    par.cmd.CommandText = stringaSegnalazioni _
                        & filtroGroupBy
                    stringaSegnalazioniCanone = stringaSegnalazioniCan _
                       & filtroGroupBy
                End If
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettore.Read
                    Select Case par.IfNull(lettore("ID_STATO"), -1)
                        Case "0"
                            numeroSegnAperte = par.IfNull(lettore("CONTEGGIO"), 0)
                        Case "6"
                            numeroSegnInCorso = par.IfNull(lettore("CONTEGGIO"), 0)
                        Case "7"
                            numeroSegnEvasa = par.IfNull(lettore("CONTEGGIO"), 0)
                    End Select
                End While
                lettore.Close()
                lblNumSegnalazioniAperteExtraCanone.Text = numeroSegnAperte
                lblNumSegnalazioniInCorsoExtraCanone.Text = numeroSegnInCorso
                lblNumSegnalazioniEvaseExtraCanone.Text = numeroSegnEvasa


                numeroSegnAperte = 0
                numeroSegnInCorso = 0
                numeroSegnEvasa = 0
                'CANONE

                par.cmd.CommandText = stringaSegnalazioniCanone
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    Select Case par.IfNull(lettore("ID_STATO"), -1)
                        Case "0"
                            numeroSegnAperte = par.IfNull(lettore("CONTEGGIO"), 0)
                        Case "6"
                            numeroSegnInCorso = par.IfNull(lettore("CONTEGGIO"), 0)
                        Case "7"
                            numeroSegnEvasa = par.IfNull(lettore("CONTEGGIO"), 0)
                    End Select
                End While
                lettore.Close()
                lblNumSegnalazioniAperteCanone.Text = numeroSegnAperte
                lblNumSegnalazioniInCorsoCanone.Text = numeroSegnInCorso
                lblNumSegnalazioniEvaseCanone.Text = numeroSegnEvasa

            End If
            '******************** KPI 1 ******************** 

            Dim condizioneVista As String = ""

            '******************** KPI 2 ******************** 
            If indice = 0 Or indice = 2 Then
                If RadButtonBuildingManager.Checked = True Then
                    'BUILDING MANAGER
                    condizioneVista = "And (MANUTENZIONI.ID_COMPLESSO In (Select EDIFICI.ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID_BM In (Select ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")) OR MANUTENZIONI.ID_EDIFICIO IN (SELECT EDIFICI.ID FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")))"
                    par.cmd.CommandText = "SELECT COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                        & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                        & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                        & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                        & " AND ID_OPERATORE=" & idOperatore _
                        & " AND EDIFICI.ID>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND ID_STATO=0" _
                        & " AND SYSDATE-TO_DATE(SUBSTR(SEGNALAZIONI.DATA_ORA_RICHIESTA,1,8),'YYYYMMDD')>30 " _
                        & " GROUP BY SEGNALAZIONI.ID_STATO "
                ElseIf RadButtonDirettoreLavori.Checked = True Then
                    'DIRETTORE LAVORI
                    condizioneVista = " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO='30000000' AND ID_OPERATORE=" & idOperatore & "))"
                    par.cmd.CommandText = "SELECT COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI " _
                        & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                        & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                        & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND ID_STATO=0" _
                        & " AND SYSDATE-TO_DATE(SUBSTR(SEGNALAZIONI.DATA_ORA_RICHIESTA,1,8),'YYYYMMDD')>30 " _
                        & " GROUP BY SEGNALAZIONI.ID_STATO "
                ElseIf RadButtonFieldQualityManager.Checked = True Then
                    'FIELD QUALITY MANAGER
                    condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
                    par.cmd.CommandText = "SELECT COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & condizioneStruttura _
                        & " AND EDIFICI.ID>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND ID_STATO=0" _
                        & " AND SYSDATE-TO_DATE(SUBSTR(SEGNALAZIONI.DATA_ORA_RICHIESTA,1,8),'YYYYMMDD')>30 " _
                        & " GROUP BY SEGNALAZIONI.ID_STATO "
                ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                    'TECNICO AMMINISTRATIVO
                    condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
                    par.cmd.CommandText = "SELECT COUNT(SEGNALAZIONI.ID) AS CONTEGGIO " _
                        & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI " _
                        & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                        & condizioneStruttura _
                        & " AND EDIFICI.ID>1 " _
                        & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                        & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                        & " AND ID_STATO=0" _
                        & " AND SYSDATE-TO_DATE(SUBSTR(SEGNALAZIONI.DATA_ORA_RICHIESTA,1,8),'YYYYMMDD')>30 " _
                        & " GROUP BY SEGNALAZIONI.ID_STATO "
                End If
                lblSegnAperte30gg.Text = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = "SELECT COUNT(MANUTENZIONI.ID) " _
                    & " FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND MANUTENZIONI.STATO NOT IN (5,6) " _
                    & " AND MANUTENZIONI.STATO = 0 " _
                    & condizioneVista
                lblOdlBozzaNoEmessi.Text = par.cmd.ExecuteScalar
                par.cmd.CommandText = "SELECT COUNT(MANUTENZIONI.ID) " _
                    & " FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND MANUTENZIONI.STATO NOT IN (5,6) " _
                    & " AND MANUTENZIONI.STATO = 1 " _
                    & condizioneVista
                lblODLEmessiNoCons.Text = par.cmd.ExecuteScalar
                par.cmd.CommandText = "SELECT COUNT(MANUTENZIONI.ID) " _
                    & " FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND MANUTENZIONI.STATO NOT IN (5,6) " _
                    & " AND MANUTENZIONI.STATO = 2 " _
                    & condizioneVista
                lblODLConsNoCDP.Text = par.cmd.ExecuteScalar
            End If
            '******************** KPI 2 ******************** 

            '******************** KPI 3 ******************** 
            condizioneVista = ""
            Dim condizioneBPAttivo As String = " AND MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5)"
            If indice = 0 Or indice = 3 Then
                If RadButtonBuildingManager.Checked = True Then
                    'BUILDING MANAGER
                    condizioneVista = "AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT EDIFICI.ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")) OR MANUTENZIONI.ID_EDIFICIO IN (SELECT EDIFICI.ID FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")))"
                ElseIf RadButtonDirettoreLavori.Checked = True Then
                    'DIRETTORE LAVORI
                    condizioneVista = " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO='30000000' AND ID_OPERATORE=" & idOperatore & "))"
                ElseIf RadButtonFieldQualityManager.Checked = True Then
                    'FIELD QUALITY MANAGER
                    condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
                ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                    'TECNICO AMMINISTRATIVO
                    condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
                End If
                par.cmd.CommandText = "SELECT COUNT(MANUTENZIONI.ID) " _
                    & " FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND MANUTENZIONI.STATO IN (0,1,2) " _
                    & condizioneVista _
                    & condizioneBPAttivo _
                    & " AND ID_SEGNALAZIONI IS NOT NULL"
                lblNumODL.Text = par.cmd.ExecuteScalar
                par.cmd.CommandText = "SELECT COUNT(MANUTENZIONI.ID) " _
                    & " FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
                    & " AND MANUTENZIONI.STATO IN (0,1,2) " _
                    & condizioneVista _
                    & condizioneBPAttivo _
                    & " AND ID_SEGNALAZIONI IS NULL"
                lblNumODLNoSegn.Text = par.cmd.ExecuteScalar
            End If
            '******************** KPI 3 ******************** 

            '******************** KPI 4 - KPI 6 ******************** 
            If indice = 0 Or indice = 4 Or indice = 6 Then
                Dim condizioneStato As String = ""
                'If IsNumeric(idStatoOrdine.Value) AndAlso idStatoOrdine.Value >= 0 AndAlso idStatoOrdine.Value <= 2 Then
                '    condizioneStato = " AND MANUTENZIONI.STATO=" & idStatoOrdine.Value
                'End If
                'par.cmd.CommandText = " SELECT COUNT(MANUTENZIONI.ID) AS CONT," _
                '    & " ROUND(AVG(SISCOM_MI.GETTEMPOATTRAVERSAMENTOMANU(MANUTENZIONI.ID)),2) AS TEMPO_ATTRAVERSAMENTO " _
                '    & " FROM SISCOM_MI.MANUTENZIONI " _
                '    & " WHERE MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5)" _
                '    & " AND MANUTENZIONI.STATO NOT IN (5,6) " _
                '    & " AND MANUTENZIONI.STATO IN (2,4) " _
                '    & condizioneVista _
                '    & condizioneStato
                'Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                'For Each riga As Data.DataRow In dt.Rows
                '    lblTempoAttraversamento.Text = par.IfNull(riga.Item("TEMPO_ATTRAVERSAMENTO"), 0) & " giorni"
                'Next
                par.cmd.CommandText = " SELECT COUNT(MANUTENZIONI.ID) AS CONT," _
                    & " ROUND(AVG(SISCOM_MI.GETTEMPOATTRAVERSAMENTOMANU(MANUTENZIONI.ID)),2) AS TEMPO_ATTRAVERSAMENTO " _
                    & " FROM SISCOM_MI.MANUTENZIONI " _
                    & " WHERE MANUTENZIONI.STATO NOT IN (5,6) " _
                    & " and stato in (2,4) " _
                    & condizioneVista _
                    & condizioneStato
                Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                For Each riga As Data.DataRow In dt.Rows
                    lblTempoAttraversamento.Text = par.IfNull(riga.Item("TEMPO_ATTRAVERSAMENTO"), 0) & " giorni"
                Next
            End If
            '******************** KPI 4 - KPI 6 ******************** 

            '******************** KPI 5 ******************** 
            If indice = 0 Or indice = 5 Then
                ImpostaTempoGestione()
            End If
            '******************** KPI 5 ******************** 

            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub ImpostaTempoGestione()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim condizioneCriticitaColore As String = ""
            If IsNumeric(cmbCriticita.SelectedIndex) AndAlso cmbCriticita.SelectedIndex >= 0 Then
                'VA CONSIDERATO IL PERICOLO DI SEGNALAZIONE INIZIALE PERCHè POTREBBE VARIARE NEL TEMPO
                Dim indice As Integer = 0
                Select Case cmbCriticita.SelectedIndex
                    Case 0
                        'TUTTE
                        indice = 5
                    Case 1
                        'ROSSO
                        indice = 4
                    Case 2
                        'GIALLO
                        indice = 3
                    Case 3
                        'VERDE
                        indice = 2
                    Case 4
                        'BIANCO
                        indice = 1
                    Case 5
                        'BLU
                        indice = 0
                End Select
                If indice < 5 Then
                    condizioneCriticitaColore = " AND ID_PERICOLO_SEGNALAZIONE_INIZ=" & indice
                End If
            End If
            Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
            Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
            Dim condizioneStruttura As String = ""
            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
            Else
                'CONDIZIONE PER ESCLUDERE LA VISIONE
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
            End If
            Dim condizioneSelezionato As String = ""
            Dim tempoPresaInCarico As String = ""
            Dim tempoPresaInCaricoCanone As String = ""
            Dim tempoRisoluzioneTecnica As String = ""
            Dim tempoRisoluzioneTecnicaCanone As String = ""
            Dim tempoContabilizzazione As String = ""
            If RadButtonBuildingManager.Checked = True Then
                'BUILDING MANAGER
                tempoPresaInCarico = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

                '***Rif segn. SD 2519/2018***'
                Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                '***Rif segn. SD 2519/2018***'
                tempoPresaInCaricoCanone = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1 " & filtroData


                tempoRisoluzioneTecnica = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"


                tempoRisoluzioneTecnicaCanone = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1 " & filtroData


                tempoContabilizzazione = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE),0),2) AS TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " AND EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_CONTABILIZZAZIONE>0"
            ElseIf RadButtonDirettoreLavori.Checked = True Then
                'DIRETTORE LAVORI
                tempoPresaInCarico = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

                '***Rif segn. SD 2519/2018***'
                Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                '***Rif segn. SD 2519/2018***'
                tempoPresaInCaricoCanone = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE PROGRAMMA_ATTIVITA.ID=SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(PROGRAMMA_ATTIVITA.ID_GRUPPO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1 " & filtroData

                tempoRisoluzioneTecnica = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"


                tempoRisoluzioneTecnicaCanone = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1 " & filtroData

                tempoContabilizzazione = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE),0),2) AS TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_CONTABILIZZAZIONE>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

            ElseIf RadButtonFieldQualityManager.Checked = True Then
                'FIELD QUALITY MANAGER
                tempoPresaInCarico = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

                '***Rif segn. SD 2519/2018***'
                Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                '***Rif segn. SD 2519/2018***'
                tempoPresaInCaricoCanone = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData


                tempoRisoluzioneTecnica = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"


                tempoRisoluzioneTecnicaCanone = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData

                tempoContabilizzazione = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE),0),2) AS TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_CONTABILIZZAZIONE>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

            ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                'TECNICO AMMINISTRATIVO
                tempoPresaInCarico = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

                '***Rif segn. SD 2519/2018***'
                Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
                '***Rif segn. SD 2519/2018***'
                tempoPresaInCaricoCanone = "SELECT ROUND(NVL(AVG(SEGNALAZIONI.TEMPO_PRESA_IN_CARICO),0),2) AS TEMPO_PRESA_IN_CARICO " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData

                tempoRisoluzioneTecnica = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"


                tempoRisoluzioneTecnicaCanone = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA),0),2) AS TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_RISOLUZIONE_TECNICA>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData


                tempoContabilizzazione = "SELECT ROUND(NVL(AVG(SISCOM_MI.SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE),0),2) AS TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_CONTABILIZZAZIONE>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"
            End If
            par.cmd.CommandText = tempoPresaInCarico
            lblTempoPresaInCaricoExtraCanone.Text = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = tempoPresaInCaricoCanone
            lblTempoPresaInCaricoCanone.Text = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = tempoRisoluzioneTecnica
            lblTempoRisoluzioneTecnica.Text = par.IfNull(par.cmd.ExecuteScalar, 0)

            par.cmd.CommandText = tempoRisoluzioneTecnicaCanone
            lblTempoRisoluzioneTecnicaCanone.Text = par.IfNull(par.cmd.ExecuteScalar, 0)

            par.cmd.CommandText = tempoContabilizzazione
            lblTempoContabilizzazione.Text = par.IfNull(par.cmd.ExecuteScalar, 0)
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub cmbCriticita_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCriticita.SelectedIndexChanged
        ImpostaTempoGestione()
    End Sub
    Public Sub caricaMappa()
        Try
            Dim FlagConnessione As Boolean = False
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim latitudine As String = ",(SELECT MAX(COORDINATA_LATITUDINE) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=SEGNALAZIONI.ID_EDIFICIO)) AS COORDINATA_LATITUDINE"
            Dim longitudine As String = ",(SELECT MAX(COORDINATA_LONGITUDINE) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=SEGNALAZIONI.ID_EDIFICIO)) AS COORDINATA_LONGITUDINE"
            Dim filiale As String = ",(SELECT MAX(TO_NUMBER(ID_FILIALE)-100) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=SEGNALAZIONI.ID_EDIFICIO)) AS ID_FILIALE"
            par.cmd.CommandText = EsportaQuerySegnalazioni().Replace("NON_CONFORMITA", "NON_CONFORMITA" & latitudine & longitudine & filiale)
            'par.cmd.CommandText = "SELECT COORDINATA_LATITUDINE,COORDINATA_LONGITUDINE,TO_NUMBER(ID_FILIALE)-100 AS ID_FILIALE " _
            '    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID>1 AND ROWNUM<=100 " _
            '    & " AND COORDINATA_LATITUDINE IS NOT NULL AND COORDINATA_LONGITUDINE IS NOT NULL"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            HFLatitudine.Value = "0"
            HFLongitudine.Value = "0"
            HFColore.Value = "1"
            HFEdificio.Value = ""
            While lettore.Read
                If HFLatitudine.Value = "0" And HFLongitudine.Value = "0" Then
                    HFLatitudine.Value = par.IfNull(lettore("COORDINATA_LATITUDINE"), "").ToString.Replace(",", ".")
                    HFLongitudine.Value = par.IfNull(lettore("COORDINATA_LONGITUDINE"), "").ToString.Replace(",", ".")
                    HFColore.Value = par.IfNull(lettore("ID_FILIALE"), "1").ToString.Replace(",", ".")
                    HFEdificio.Value = par.IfNull(lettore("EDIFICIO"), "").ToString.Replace(",", ".")
                Else
                    HFLatitudine.Value &= "#" & par.IfNull(lettore("COORDINATA_LATITUDINE"), "").ToString.Replace(",", ".")
                    HFLongitudine.Value &= "#" & par.IfNull(lettore("COORDINATA_LONGITUDINE"), "").ToString.Replace(",", ".")
                    HFColore.Value &= "#" & par.IfNull(lettore("ID_FILIALE"), "1").ToString.Replace(",", ".")
                    HFEdificio.Value &= "#" & par.IfNull(lettore("EDIFICIO"), "").ToString.Replace(",", ".")
                End If
            End While
            lettore.Close()
            If ApertaNow Then connData.chiudi(False)
            If IsPostBack Then
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "mappaG", "initialize();", True)
            End If
        Catch ex As Exception
            '************CHIUSURA CONNESSIONE**********
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub CaricaViste()
        'La vista serve ad individuare tutto ciò che deve essere visualizzato a seconda del profilo utente
        If Not IsNothing(Session.Item("MOD_BUILDING_MANAGER")) AndAlso Session.Item("MOD_BUILDING_MANAGER") = 1 Then
            RadButtonBuildingManager.Visible = True
        Else
            RadButtonBuildingManager.Visible = False
        End If
        If (Not IsNothing(Session.Item("FL_AUTORIZZAZIONE_ODL")) Or Not IsNothing(Session.Item("FL_SUPERDIRETTORE"))) AndAlso (Session.Item("FL_AUTORIZZAZIONE_ODL") = 1 Or Session.Item("FL_SUPERDIRETTORE") = 1) Then
            RadButtonDirettoreLavori.Visible = True
        Else
            RadButtonDirettoreLavori.Visible = False
        End If
        If Not IsNothing(Session.Item("FL_FQM")) AndAlso Session.Item("FL_FQM") = 1 Then
            RadButtonFieldQualityManager.Visible = True
        Else
            RadButtonFieldQualityManager.Visible = False
        End If
        If Not IsNothing(Session.Item("FL_CP_TECN_AMM")) AndAlso Session.Item("FL_CP_TECN_AMM") = 1 Then
            RadButtonTecnicoAmministrativo.Visible = True
        Else
            RadButtonTecnicoAmministrativo.Visible = False
        End If
        If RadButtonBuildingManager.Visible = True Then
            RadButtonBuildingManager.Checked = True
            btnEmettiSal.Enabled = False
        ElseIf RadButtonDirettoreLavori.Visible = True Then
            RadButtonDirettoreLavori.Checked = True
            btnEmettiSal.Enabled = True
        ElseIf RadButtonFieldQualityManager.Visible = True Then
            RadButtonFieldQualityManager.Checked = True
            btnEmettiSal.Enabled = False
        ElseIf RadButtonTecnicoAmministrativo.Visible = True Then
            RadButtonTecnicoAmministrativo.Checked = True
            btnEmettiSal.Enabled = False
        End If
    End Sub
    Protected Sub RadButtonBuildingManager_Click(sender As Object, e As System.EventArgs) Handles RadButtonBuildingManager.Click
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
        caricaMappa()
        ImpostaKPI()
    End Sub
    Protected Sub RadButtonDirettoreLavori_Click(sender As Object, e As System.EventArgs) Handles RadButtonDirettoreLavori.Click
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
        caricaMappa()
        ImpostaKPI()
    End Sub
    Protected Sub RadButtonFieldQualityManager_Click(sender As Object, e As System.EventArgs) Handles RadButtonFieldQualityManager.Click
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
        caricaMappa()
        ImpostaKPI()
    End Sub
    Protected Sub RadButtonTecnicoAmministrativo_Click(sender As Object, e As System.EventArgs) Handles RadButtonTecnicoAmministrativo.Click
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
        caricaMappa()
        ImpostaKPI()
    End Sub
    Private Sub SettaStatiSegnalazioni(Optional ByVal stato As Integer = 0, Optional ByVal tipologiaSegnalazione As Integer = 0)
        'GESTIONE DEI VALORI PREDEFINITI
        idStatoSegnalazione.Value = stato
        lblNumSegnalazioniAperteExtraCanone.CssClass = "nonindicato"
        lblNumSegnalazioniInCorsoExtraCanone.CssClass = "nonindicato"
        lblNumSegnalazioniEvaseExtraCanone.CssClass = "nonindicato"

        lblNumSegnalazioniAperteCanone.CssClass = "nonindicato"
        lblNumSegnalazioniInCorsoCanone.CssClass = "nonindicato"
        lblNumSegnalazioniEvaseCanone.CssClass = "nonindicato"
        If tipologiaSegnalazione = 0 Then
            Select Case stato
                Case 0
                    'APERTA
                    lblNumSegnalazioniAperteExtraCanone.CssClass = "indicato"
                    btnPrendiInCarico.Enabled = True
                    btnManutenzione.Enabled = True
                    btnChiudiSegn.Enabled = True
                Case 6
                    'IN CORSO
                    lblNumSegnalazioniInCorsoExtraCanone.CssClass = "indicato"
                    btnPrendiInCarico.Enabled = False
                    btnManutenzione.Enabled = True
                    btnChiudiSegn.Enabled = True
                Case 7
                    'EVASA
                    lblNumSegnalazioniEvaseExtraCanone.CssClass = "indicato"
                    btnPrendiInCarico.Enabled = False
                    btnManutenzione.Enabled = False
                    btnChiudiSegn.Enabled = True
            End Select
        ElseIf tipologiaSegnalazione = 1 Then
            Select Case stato
                Case 0
                    'APERTA
                    lblNumSegnalazioniAperteCanone.CssClass = "indicato"
                    btnPrendiInCarico.Enabled = True
                    btnManutenzione.Enabled = False
                    btnChiudiSegn.Enabled = True
                Case 6
                    'IN CORSO
                    lblNumSegnalazioniInCorsoCanone.CssClass = "indicato"
                    btnPrendiInCarico.Enabled = False
                    btnManutenzione.Enabled = False
                    btnChiudiSegn.Enabled = True
                Case 7
                    'EVASA
                    lblNumSegnalazioniEvaseCanone.CssClass = "indicato"
                    btnPrendiInCarico.Enabled = False
                    btnManutenzione.Enabled = False
                    btnChiudiSegn.Enabled = True
            End Select
        End If

    End Sub
    Private Sub SettaStatiOrdini(Optional ByVal stato As Integer = 0)
        'GESTIONE DEI VALORI PREDEFINITI
        idStatoOrdine.Value = stato
        lblOdlBozzaNoEmessi.CssClass = "nonindicato"
        lblODLEmessiNoCons.CssClass = "nonindicato"
        lblODLConsNoCDP.CssClass = "nonindicato"
        Select Case stato
            Case 0
                lblOdlBozzaNoEmessi.CssClass = "indicato"
            Case 1
                lblODLEmessiNoCons.CssClass = "indicato"
            Case 2
                lblODLConsNoCDP.CssClass = "indicato"
        End Select
    End Sub
    Private Sub dgvSegnalazioni_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvSegnalazioni.NeedDataSource
        Try
            Dim Query As String = EsportaQuerySegnalazioni()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            dgvSegnalazioni.DataSource = dt
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub dgvODL_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvODL.NeedDataSource
        Try
            Dim Query As String = EsportaQueryODL()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            dgvODL.DataSource = dt
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub btnPrendiInCarico_Click(sender As Object, e As EventArgs) Handles btnPrendiInCarico.Click
        Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQuerySegnalazioni())
        CheckBoxSegnalazioni(dt)
        Dim dtFiltrata As New Data.DataTable
        Dim view As New Data.DataView(dt)
        view.RowFilter = "CHECK1 = 'TRUE'"
        dtFiltrata = view.ToTable
        If dtFiltrata.Rows.Count > 0 Then
            For Each riga As Data.DataRow In dtFiltrata.Rows
                If riga.Item("ID_STATO") = "0" Then
                    Salva(6, riga.Item("ID"))
                End If
            Next
            RadNotificationNote.Show()
            dgvSegnalazioni.Rebind()
            NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
            caricaMappa()
        Else
            RadWindowManager1.RadAlert("Selezionare una segnalazione in stato APERTA!", 300, 150, "Attenzione", "", "null")
        End If
        'REIMPOSTO IL CALCOLO DEI KPI
        ImpostaKPI(1)
    End Sub
    Function Salva(ByVal Stato As Integer, ByVal id As String) As Boolean
        Try
            Salva = False
            ' APRO CONNESSIONE
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Session.Add("LAVORAZIONE", "1")
            Dim dataInCarico As String = ""
            If Stato = 1 Then
                dataInCarico = " , data_in_carico = '" & Format(Now, "yyyyMMdd") & "'"
            End If
            ' SEGNALAZIONI
            par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=" & Stato & dataInCarico & " where ID=" & id
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & id & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','APERTA','IN CORSO')"
            par.cmd.ExecuteNonQuery()
            WriteEvent("F02", "AGGIORNAMENTO SEGNALAZIONE", id)

            WriteEventSegnalazione("F233", "Modifica stato segnalazione", "APERTA", "IN CORSO", id)
            '************************************
            Salva = True
            '************CHIUSURA CONNESSIONE**********
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Salva = False
            '************CHIUSURA CONNESSIONE**********
            connData.chiudi(False)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function
    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String, ByVal idSegnalazione As String)
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES ( " & idSegnalazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
            & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"
            par.cmd.ExecuteNonQuery()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub WriteEventSegnalazione(ByVal cod As String, ByVal motivo As String, Optional ByVal valoreVecchio As String = "", Optional ByVal valoreNuovo As String = "", Optional idSegn As Integer = 0)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If
            If idSegn = 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegn & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,VALORE_OLD,VALORE_NEW) " _
                & "VALUES ( " & idSegn & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "','" & par.PulisciStrSql(valoreVecchio) & "','" & par.PulisciStrSql(valoreNuovo) & "')"
                par.cmd.ExecuteNonQuery()
            End If
            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub btnProcediEF_Click(sender As Object, e As EventArgs) Handles btnProcediEF.Click
        Try
            If Me.cmbEsercizio.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare  l\'esercizio finanaziario!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbServizio.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare il Servizio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbServizioVoce.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare la voce DGR!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbAppalto.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare il numero di repertorio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If
            If txtannullo.Value = "1" Then
                txtannullo.Value = "0"
                connData.chiudi(True)
                Session.Add("ID", 0)
                Response.Write("<script>location.replace('CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?SE=" & Me.cmbServizio.SelectedValue.ToString _
                                                                        & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                        & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                        & "&CO=" & idSelected.Value _
                                                                        & "&TIPOR=0" _
                                                                        & "&ED=" & statoSegnalazione.Value _
                                                                        & "&NUOVA=1 " _
                                                                        & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                       & "&PROVENIENZA=SEGNALAZIONI&NASCONDIINDIETRO=1" & "');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaEsercizio()

        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try
            ' APRO CONNESSIONE
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Me.cmbEsercizio.Items.Clear()
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE , SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                               & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                               & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                               & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by 1 desc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                Me.cmbEsercizio.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))

            End While
            myReader1.Close()
            'par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO")

            If ApertaNow Then connData.chiudi(False)

            Me.cmbEsercizio.Enabled = True
            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                'Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                '  Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)


                Me.cmbServizio.Enabled = True
                Me.cmbServizioVoce.Enabled = True
                Me.cmbAppalto.Enabled = True
                CaricaServizi()
            End If



        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try

            Me.txtSTATO_PF.Value = -1

            If ID_ESERCIZIO < 0 Then Exit Sub

            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                Me.txtSTATO_PF.Value = par.IfNull(myReaderF("ID_STATO"), -1)
            End If
            myReaderF.Close()

            par.cmd.Parameters.Clear()

            If ApertaNow Then connData.chiudi(False)

        Catch ex As Exception
            ' Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaServizi()


        Try

            Me.cmbServizio.Items.Clear()
            ' Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            '    Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbEsercizio.SelectedValue <> "-1" Then

                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If



                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                End If

                Me.cmbServizio.Items.Clear()
                '  Me.cmbServizio.Items.Add(New ListItem(" ", -1))


                Select Case Me.tipo.Value
                    Case "C"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "


                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                               & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                               & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                     & " where ID_COMPLESSO=" & Me.identificativo.Value & ") ) ) " _
                                          & " order by DESCRIZIONE asc"

                    Case "E"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                               & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                               & " where ID_EDIFICIO=" & Me.identificativo.Value & ") ) " _
                                          & " order by DESCRIZIONE asc"

                    Case "U"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select
                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                                   & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                         & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & Me.identificativo.Value & ")))) " _
                                          & " order by DESCRIZIONE asc"
                    Case Else

                        'par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                        '                   & " from SISCOM_MI.TAB_SERVIZI " _
                        '                   & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                        '                                & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                        '                                & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                        '                                                                   & " from SISCOM_MI.LOTTI " _
                        '                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                        '                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        'Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                        '    Case 6
                        '        If Session.Item("FL_COMI") <> 1 Then
                        '            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        '        End If
                        '    Case 7
                        '        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        'End Select
                        'par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                        '                                          & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                        '                                                                           & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                                           & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                        '                                                                                                 & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & Me.identificativo.Value & ")))) " _
                        '                  & " order by DESCRIZIONE asc"

                End Select
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)

                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                'End While
                'myReader1.Close()

                Me.cmbServizio.SelectedValue = "-1"
                '**************************
                If ApertaNow Then connData.chiudi(False)
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        par.cmd.CommandText = "SELECT ID_UNITA, ID_EDIFICIO, ID_COMPLESSO FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSelected.Value
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        For Each riga As Data.DataRow In dt.Rows
            'CONTROLLO UNITA
            If par.IfNull(riga.Item("ID_UNITA"), "-1") <> "-1" Then
                tipo.Value = "U"
                identificativo.Value = par.IfNull(riga.Item("ID_UNITA"), "-1")
                'CONTROLLO EDIFICIO
            ElseIf par.IfNull(riga.Item("ID_EDIFICIO"), "-1") <> "-1" Then
                tipo.Value = "E"
                identificativo.Value = par.IfNull(riga.Item("ID_EDIFICIO"), "-1")
            Else
                tipo.Value = "C"
                identificativo.Value = par.IfNull(riga.Item("ID_COMPLESSO"), "-1")
            End If
        Next
        CaricaServizi()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub btnManutenzione_Click(sender As Object, e As EventArgs) Handles btnManutenzione.Click
        Try
            If Not String.IsNullOrEmpty(idSelected.Value) Then
                If idStatoSegnalazione.Value <= "6" Then
                    Dim ApertaNow As Boolean = False
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                    Else
                        connData.apri(False)
                        ApertaNow = True
                    End If

                    par.cmd.CommandText = "SELECT ID_UNITA, ID_EDIFICIO, ID_COMPLESSO FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSelected.Value
                    Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                    For Each riga As Data.DataRow In dt.Rows
                        'CONTROLLO UNITA
                        If par.IfNull(riga.Item("ID_UNITA"), "-1") <> "-1" Then
                            tipo.Value = "U"
                            identificativo.Value = par.IfNull(riga.Item("ID_UNITA"), "-1")
                            'CONTROLLO EDIFICIO
                        ElseIf par.IfNull(riga.Item("ID_EDIFICIO"), "-1") <> "-1" Then
                            tipo.Value = "E"
                            identificativo.Value = par.IfNull(riga.Item("ID_EDIFICIO"), "-1")
                        Else
                            tipo.Value = "C"
                            identificativo.Value = par.IfNull(riga.Item("ID_COMPLESSO"), "-1")
                        End If
                    Next

                    CaricaEsercizio()
                    If ApertaNow Then connData.chiudi(False)
                    Dim Script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    If Not String.IsNullOrWhiteSpace(Script) Then
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", Script, True)
                    End If
                Else
                    RadWindowManager1.RadAlert("Verificare che la segnalazione selezionata non sia in stato <strong>evasa</strong> o <strong>chiusa</strong>!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
            End If

        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged
        FiltraDettaglio()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub FiltraDettaglio()
        Dim sWhere As String = ""
        Dim i As Integer = 0


        Try


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbServizioVoce.Items.Clear()
            '  Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            '  Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizio.SelectedValue <> "-1" Then


                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If



                par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                    & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                     & "where  PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                  & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                  & " where  ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )" _
                                                                  & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO<>15" _
                                     & "  and PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                       & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")"

                Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & "  and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                     & " order by DESCRIZIONE asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                '    i = i + 1
                'End While
                'myReader1.Close()
                '**************************
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizioVoce, "ID", "DESCRIZIONE", True)
                If ApertaNow Then connData.chiudi(False)

                If i = 2 Then
                    cmbServizioVoce.Items(1).Selected = True
                    FiltraAppalti()
                End If

            End If


        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Private Sub FiltraAppalti()
        Dim i As Integer = 0


        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If


            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                par.cmd.CommandText = " select ID,TRIM(NUM_REPERTORIO) AS NUM_REPERTORIO,TRIM(NUM_REPERTORIO) || ' - ' || (select ragione_sociale from siscom_mi.fornitori where fornitori.id=appalti.id_fornitore) as fornitore " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                 & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                 & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
                                    & "  and ID_STATO=1" _
                                    & " order by NUM_REPERTORIO "

            Else

                If ApertaNow Then connData.chiudi(False)
                Exit Sub
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "FORNITORE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " ") & "-" & par.IfNull(myReader1("FORNITORE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()

            If ApertaNow Then connData.chiudi(False)

            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If


        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub cmbServizioVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizioVoce.SelectedIndexChanged
        FiltraAppalti()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub btnChiudiEF_Click(sender As Object, e As EventArgs) Handles btnChiudiEF.Click
        cmbEsercizio.ClearSelection()
        cmbServizio.ClearSelection()
        cmbServizioVoce.ClearSelection()
        cmbAppalto.ClearSelection()
    End Sub





    Private Sub btnVisualizzaManu_Click(sender As Object, e As EventArgs) Handles btnVisualizzaManu.Click
        Try
            If Not String.IsNullOrEmpty(idSelectedManu.Value) Then
                Session.Add("ID", idSelectedManu.Value)
                ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "manu", "location.replace('CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?REP=" & par.VaroleDaPassare(repAppalto.Value) _
                    & "&ODL=" & progrManutenzione.Value _
                    & "&ANNO=" & annoManutenzione.Value _
                    & "&EF_R=" & idEsercizioFinanziario.Value _
                    & "&PROVENIENZA=RICERCA_DIRETTA&NASCONDIINDIETRO=1');", True)
            Else
                RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            End If

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnEmettiSal_Click(sender As Object, e As EventArgs) Handles btnEmettiSal.Click
        Try
            If IsNumeric(cmbRepertorio.SelectedValue) AndAlso cmbRepertorio.SelectedValue > 0 Then
                Dim continua As Boolean = True
                Dim oDataGridItem As GridDataItem
                'Dim chkExport As System.Web.UI.WebControls.CheckBox
                Dim chkExport As RadButton
                Dim Trovato As Boolean
                Dim i As Integer

                Dim gen As Epifani.ListaGenerale


                For Each oDataGridItem In Me.dgvODL.Items

                    chkExport = oDataGridItem.FindControl("CheckBox1")

                    If chkExport.Checked Then

                        ' CONTROLLO SE GIA INSERITO nella LISTA
                        Trovato = False
                        If Not IsNothing(lstListaRapporti) Then
                            For Each gen In lstListaRapporti
                                If gen.STR = oDataGridItem.Cells(2).Text Then  ''SISCOM_MI.MANUTENZIONI.ID
                                    Trovato = True
                                    Exit For
                                End If
                            Next
                        End If


                        If Trovato = False Then
                            gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(2).Text)
                            lstListaRapporti.Add(gen)
                            'Me.Label3.Value = Val(Label3.Value) + 1
                            gen = Nothing
                        End If
                    Else

                        ' CONTROLLO SE GIA INSERITO nella LISTA
                        Trovato = False
                        For Each gen In lstListaRapporti

                            If gen.STR = oDataGridItem.Cells(2).Text Then
                                Trovato = True
                                Exit For
                            End If
                        Next

                        If Trovato = True Then
                            i = 0
                            For Each gen In lstListaRapporti
                                If gen.STR = oDataGridItem.Cells(2).Text Then
                                    lstListaRapporti.RemoveAt(i)
                                    'Me.Label3.Value = Val(Label3.Value) - 1
                                    Exit For
                                End If
                                i = i + 1
                            Next
                            gen = Nothing
                            Dim indice As Integer = 0
                            For Each gen In lstListaRapporti
                                gen.ID = indice
                                indice += 1
                            Next
                        End If
                    End If
                Next

                If lstListaRapporti.Count > 0 Then
                    Dim ElencoID_Rapporti As String = ""
                    For Each gen In lstListaRapporti
                        If ElencoID_Rapporti <> "" Then
                            ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                        Else
                            ElencoID_Rapporti = gen.STR
                        End If
                    Next

                    ImpostaValori()
                    Session.Add("ID", 0)



                    Session.Remove("NOME_FILE")
                    Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQueryODL(ElencoID_Rapporti))
                    If dt.Rows.Count > 0 Then
                        For Each riga As Data.DataRow In dt.Rows
                            If riga.Item("id_stato") <> "2" Then
                                continua = False
                            End If
                        Next
                    Else
                        continua = False
                    End If
                    If continua = True Then
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "SAL", "location.replace('CicloPassivo/MANUTENZIONI/SAL.aspx?FO=" & idFornitore.Value _
                            & "&AP=" & idAppalto.Value _
                            & "&SV=" & idServizio.Value _
                            & "&DAL=" & dataEmissione.Value _
                            & "&AL=" & dataEmissione.Value _
                            & "&EF_R=" & idEsercizioFinanziario.Value _
                            & "&PROVENIENZA=RISULTATI_SAL" _
                            & "&NASCONDIINDIETRO=1" _
                            & "');", True)
                    Else
                        RadWindowManager1.RadAlert("Verificare che le manutenzioni selezionate siano in stato <strong>consuntivato</strong>!", 300, 150, "Attenzione", "", "null")
                    End If
                Else
                    RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare obbligatoriamente il repertorio!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs)
        'Dim numeroCheckati As Integer = 0
        'For Each elemento As GridDataItem In dgvODL.Items
        '    If CType(elemento.FindControl("CheckBox1"), RadButton).Checked = True Then
        '        numeroCheckati += 1
        '    End If
        'Next
        'Select Case numeroCheckati
        '    Case 0
        '        txtmia.Text = "Nessun ODL selezionato"
        '    Case 1
        '        txtmia.Text = "Selezionato 1 ODL"
        '    Case Else
        '        txtmia.Text = "Sono stati selezionati " & numeroCheckati & " ODL"
        'End Select
    End Sub
    Protected Sub ButtonSelAll_Click(sender As Object, e As System.EventArgs)
        Try
            If hiddenSelTutti.Value = "1" Then
                For Each riga As GridItem In dgvODL.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = True
                Next
            Else
                For Each riga As GridItem In dgvODL.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = False
                Next
            End If
        Catch ex As Exception
            'If par.OracleConn.State = Data.ConnectionState.Open Then
            '    connData.chiudi()
            'End If
            'Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ButtonSelAll_Click - " & ex.Message)
            'Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ButtonSelAllSegnalazioni_Click(sender As Object, e As System.EventArgs)
        Try
            If hiddenSelTuttiSegnalazioni.Value = "1" Then
                For Each riga As GridItem In dgvSegnalazioni.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = True
                Next
            Else
                For Each riga As GridItem In dgvSegnalazioni.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = False
                Next
            End If
        Catch ex As Exception
            'If par.OracleConn.State = Data.ConnectionState.Open Then
            '    connData.chiudi()
            'End If
            'Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ButtonSelAll_Click - " & ex.Message)
            'Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ImpostaValori()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQueryODL())
            CheckBox(dt)
            If dt.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dt.Rows
                    If riga.Item("CHECK1") = "TRUE" Then
                        idFornitore.Value = par.IfNull(riga.Item("ID_FORNITORE"), "")
                        idAppalto.Value = par.IfNull(riga.Item("ID_APPALTO"), "")
                        idServizio.Value = par.IfNull(riga.Item("ID_SERVIZIO"), "")
                        dataEmissione.Value = par.IfNull(riga.Item("DATA_EMISSIONE"), "")
                        idEsercizioFinanziario.Value = par.IfNull(riga.Item("ID_ESERCIZIO_FINANZIARIO"), "")
                    End If
                Next
            End If


            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then connData.chiudi(True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub CheckBox(ByRef table As Data.DataTable)
        Try

            Dim row As Data.DataRow
            For i As Integer = 0 To dgvODL.Items.Count - 1
                If DirectCast(dgvODL.Items(i).FindControl("CheckBox1"), RadButton).Checked = False Then
                    row = table.Select("id = " & dgvODL.Items(i).Cells(2).Text)(0)
                    row.Item("CHECK1") = "FALSE"
                Else
                    row = table.Select("id = " & dgvODL.Items(i).Cells(2).Text)(0)
                    row.Item("CHECK1") = "TRUE"
                End If
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - CheckBox - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub CheckBoxSegnalazioni(ByRef table As Data.DataTable)
        Try

            Dim row As Data.DataRow
            For i As Integer = 0 To dgvSegnalazioni.Items.Count - 1
                If DirectCast(dgvSegnalazioni.Items(i).FindControl("CheckBox1"), RadButton).Checked = False Then
                    If table.Select("id = " & dgvSegnalazioni.Items(i).Cells(2).Text & " and id_Stato=0").Length > 0 Then
                        row = table.Select("id = " & dgvSegnalazioni.Items(i).Cells(2).Text & " and id_Stato=0")(0)
                        row.Item("CHECK1") = "FALSE"
                    End If
                Else
                    If table.Select("id = " & dgvSegnalazioni.Items(i).Cells(2).Text & " and id_Stato=0").Length > 0 Then
                        row = table.Select("id = " & dgvSegnalazioni.Items(i).Cells(2).Text & " and id_Stato=0")(0)
                        row.Item("CHECK1") = "TRUE"
                    End If
                End If
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - CheckBoxSegnalazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnChiudiSegn_Click(sender As Object, e As EventArgs) Handles btnChiudiSegn.Click
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQuerySegnalazioni())
            CheckBoxSegnalazioni(dt)
            Dim dtFiltrata As New Data.DataTable
            Dim view As New Data.DataView(dt)
            view.RowFilter = "CHECK1 = 'TRUE'"
            dtFiltrata = view.ToTable
            Dim aggiornate As Integer = 0
            If dtFiltrata.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dtFiltrata.Rows
                    If riga.Item("ID_STATO") = "6" Or riga.Item("ID_STATO") = "7" Or riga.Item("ID_STATO") = "0" Then
                        'LA SEGNALAZIONE PUò ESSERE CHIUSA SE è NELLO STATO APERTO,IN CORSO O EVASA
                        par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = " & riga.Item("ID") & " AND STATO NOT IN (5,6) "
                        Dim numero As Integer = CInt(par.cmd.ExecuteScalar)
                        If numero = 0 Then
                            aggiornate += 1
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO=10,DATA_CHIUSURA=TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') WHERE ID =" & riga.Item("ID")
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & riga.Item("ID") & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','IN CORSO','CHIUSA')"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                                                    & " VALUES ( " & riga.Item("ID") & ",'Chiusura segnalazione da Dashboard Ciclo Passivo',TO_CHAR(SYSDATE,'YYYYMMDDHH24MI')," & Session.Item("ID_OPERATORE") & ",2) "
                            par.cmd.ExecuteNonQuery()
                        End If
                    End If
                Next
                dgvSegnalazioni.Rebind()
                NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
                caricaMappa()
                ImpostaKPI(1)
                'RadNotificationNote.Show()
                If aggiornate > 0 Then
                    RadWindowManager1.RadAlert("Numero segnalazioni chiuse: " & aggiornate & "!", 300, 150, "Attenzione", "", "null")
                Else
                    RadWindowManager1.RadAlert("Nessuna segnalazione è stata chiusa!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
                'selezionare una segnalazione
            End If
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub cmbRepertorio_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbRepertorio.SelectedIndexChanged
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
    End Sub

    Private Sub btnVisualizzaSegnalazione_Click(sender As Object, e As EventArgs) Handles btnVisualizzaSegnalazione.Click
        Try
            If idSelected.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alert", "alert('Nessuna riga selezionata!');", True)
            Else
                Session.Add("ID", idSelected.Value)
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "segnalazione", "location.replace('CicloPassivo/MANUTENZIONI/Segnalazioni.aspx?IDS=" & idSelected.Value & "&NASCONDIINDIETRO=1');", True)
                'Response.Write("<script>location.replace('CicloPassivo/MANUTENZIONI/Segnalazioni.aspx?IDS=" & idSelected.Value & "&NASCONDIINDIETRO=1');</script>")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub btnCaricaAppalti_Click(sender As Object, e As EventArgs) Handles btnCaricaAppalti.Click
        CaricaAppalti()
    End Sub

    Private Sub CaricaAppalti()
        par.caricaComboTelerik("SELECT ID_GRUPPO AS ID, NUM_REPERTORIO||'-'||(SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=ID_FORNITORE) AS NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE = '" & Session.Item("ID_OPERATORE") & "' AND DATA_FINE_INCARICO='30000000') ORDER BY ID desc", cmbRepertorio, "ID", "NUM_REPERTORIO", True)
    End Sub

    Private Sub CaricaCategoria()
        par.caricaComboTelerik("SELECT ID, REPLACE(DESCRIZIONE,'#','') AS DESCRIZIONE " _
                               & " FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 " _
                               & " WHERE ID_TIPO_SEGNALAZIONE = 1 " _
                               & " ORDER BY DESCRIZIONE ASC", cmbCategoria, "ID", "DESCRIZIONE", True)
    End Sub

    Private Sub btnOrdiniBozza_Click(sender As Object, e As EventArgs) Handles btnOrdiniBozza.Click
        ordiniBozza.Value = "1"
        idSelected.Value = "-1"
        cmbRepertorio.ClearSelection()
        cmbRepertorio.Items.Clear()
        cmbRepertorio.Enabled = False
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnAperte_Click(sender As Object, e As EventArgs) Handles btnSegnAperte.Click
        idTipologiaSegnalazione.Value = ""
        idStatoSegnalazione.Value = "0"
        SettaStatiSegnalazioni(0)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnAnnullate_Click(sender As Object, e As EventArgs) Handles btnSegnAnnullate.Click
        idTipologiaSegnalazione.Value = ""
        idStatoSegnalazione.Value = "2"
        SettaStatiSegnalazioni(2)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnInCorso_Click(sender As Object, e As EventArgs) Handles btnSegnInCorso.Click
        idTipologiaSegnalazione.Value = ""
        idStatoSegnalazione.Value = "6"
        SettaStatiSegnalazioni(6)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnEvase_Click(sender As Object, e As EventArgs) Handles btnSegnEvase.Click
        idTipologiaSegnalazione.Value = ""
        idStatoSegnalazione.Value = "7"
        SettaStatiSegnalazioni(7)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnChiuse_Click(sender As Object, e As EventArgs) Handles btnSegnChiuse.Click
        idTipologiaSegnalazione.Value = ""
        idStatoSegnalazione.Value = "10"
        SettaStatiSegnalazioni(10)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnAllSegn_Click(sender As Object, e As EventArgs) Handles btnAllSegn.Click
        idTipologiaSegnalazione.Value = ""
        idStatoSegnalazione.Value = ""
        SettaStatiSegnalazioni(-1)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub


    Private Sub btnSegnAperteCanone_Click(sender As Object, e As EventArgs) Handles btnSegnAperteCanone.Click
        idTipologiaSegnalazione.Value = "1"
        idStatoSegnalazione.Value = "0"
        SettaStatiSegnalazioni(0, 1)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnAnnullateCanone_Click(sender As Object, e As EventArgs) Handles btnSegnAnnullateCanone.Click
        idTipologiaSegnalazione.Value = "1"
        idStatoSegnalazione.Value = "2"
        SettaStatiSegnalazioni(2, 1)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnInCorsoCanone_Click(sender As Object, e As EventArgs) Handles btnSegnInCorsoCanone.Click
        idTipologiaSegnalazione.Value = "1"
        idStatoSegnalazione.Value = "6"
        SettaStatiSegnalazioni(6, 1)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnSegnEvaseCanone_Click(sender As Object, e As EventArgs) Handles btnSegnEvaseCanone.Click
        idTipologiaSegnalazione.Value = "1"
        idStatoSegnalazione.Value = "7"
        SettaStatiSegnalazioni(7, 1)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub

    Private Sub btnSegnChiuseCanone_Click(sender As Object, e As EventArgs) Handles btnSegnChiuseCanone.Click
        idTipologiaSegnalazione.Value = "1"
        idStatoSegnalazione.Value = "10"
        SettaStatiSegnalazioni(10, 1)
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    'Private Sub btnAllSegn_Click(sender As Object, e As EventArgs) Handles btnAllSegnCanone.Click
    '    idStatoSegnalazione.Value = ""
    '    SettaStatiSegnalazioni(-1)
    '    dgvSegnalazioni.Rebind()
    '    NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
    '    caricaMappa()
    'End Sub



    Private Sub btnSegnAperte30gg_Click(sender As Object, e As EventArgs) Handles btnSegnAperte30gg.Click
        idStatoSegnalazione.Value = "0"
        segn30gg.Value = "1"
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
    End Sub
    Private Sub btnODLBozzaNonEmessi_Click(sender As Object, e As EventArgs) Handles btnODLBozzaNonEmessi.Click
        idStatoOrdine.Value = "0"
        ordiniEmessoConSegnalazione.Value = "0"
        SettaStatiOrdini(0)
        dgvODL.Rebind()
        cmbRepertorio.Enabled = False
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
    End Sub
    Private Sub btnODLEmessiNoCons_Click(sender As Object, e As EventArgs) Handles btnODLEmessiNoCons.Click
        idStatoOrdine.Value = "1"
        ordiniEmessoConSegnalazione.Value = "0"
        SettaStatiOrdini(1)
        CaricaAppalti()
        dgvODL.Rebind()
        cmbRepertorio.Enabled = False
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
    End Sub
    Private Sub btnODLConsNoCdP_Click(sender As Object, e As EventArgs) Handles btnODLConsNoCdP.Click
        idStatoOrdine.Value = "2"
        ordiniEmessoConSegnalazione.Value = "0"
        SettaStatiOrdini(2)
        dgvODL.Rebind()
        cmbRepertorio.Enabled = True
        If RadButtonDirettoreLavori.Checked = True Then
            btnEmettiSal.Enabled = True
        Else
            btnEmettiSal.Enabled = False
        End If
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
    End Sub
    Private Sub btnODLEmessi_Click(sender As Object, e As EventArgs) Handles btnODLEmessi.Click
        ordiniEmessoConSegnalazione.Value = "1"
        lblNumODL.CssClass = "indicato"
        lblNumODLNoSegn.CssClass = "nonindicato"
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
    End Sub
    Private Sub btnODLEmessiNoSegn_Click(sender As Object, e As EventArgs) Handles btnODLEmessiNoSegn.Click
        ordiniEmessoConSegnalazione.Value = "2"
        lblNumODL.CssClass = "nonindicato"
        lblNumODLNoSegn.CssClass = "indicato"
        dgvODL.Rebind()
        NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
    End Sub
    Private Sub CaricaAppaltiODLBozza()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim filtroStato As String = ""
            filtroStato = "  MANUTENZIONI.STATO IN (0) "
            Dim filtroDLNoSegn As String = " And id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "

            par.cmd.CommandText = "SELECT ID, NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN " _
                & " (SELECT id_appalto " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  " & filtroStato _
                                & filtroDLNoSegn _
                                & ") " _
                & " ORDER BY ID desc"


            par.caricaComboTelerik(par.cmd.CommandText, cmbRepertorio, "ID", "NUM_REPERTORIO", True)

            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Not IsPostBack Then
            NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
            NumeroOdl.Text = "Numero ODL: " & dgvODL.Items.Count
        End If
    End Sub

    'Private Sub btnExportODL_Click(sender As Object, e As EventArgs) Handles btnExportODL.Click
    '    Try
    '        connData.apri(False)
    '        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
    '        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
    '        Dim condizioneVista As String = ""
    '        Dim condizioneBPAttivo As String = " AND MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5)"

    '        If RadButtonBuildingManager.Checked = True Then
    '            'BUILDING MANAGER
    '            condizioneVista = "AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT EDIFICI.ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")) OR MANUTENZIONI.ID_EDIFICIO IN (SELECT EDIFICI.ID FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")))"
    '        ElseIf RadButtonDirettoreLavori.Checked = True Then
    '            'DIRETTORE LAVORI
    '            condizioneVista = " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO='30000000' AND ID_OPERATORE=" & idOperatore & "))"
    '        ElseIf RadButtonFieldQualityManager.Checked = True Then
    '            'FIELD QUALITY MANAGER
    '            condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
    '        ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
    '            'TECNICO AMMINISTRATIVO
    '            condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
    '        End If
    '        Dim condizioneStato As String = ""
    '        'If IsNumeric(idStatoOrdine.Value) AndAlso idStatoOrdine.Value >= 0 AndAlso idStatoOrdine.Value <= 2 Then
    '        '    condizioneStato = " AND MANUTENZIONI.STATO=" & idStatoOrdine.Value
    '        'End If
    '        par.cmd.CommandText = " SELECT MANUTENZIONI.PROGR || '/' || MANUTENZIONI.ANNO AS ODL, MANUTENZIONI.ID_SEGNALAZIONI AS SEGNALAZIONE,  " _
    '        & " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS NUM_REPERTORIO, " _
    '        & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID = MANUTENZIONI.STATO) AS STATO, " _
    '        & " (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID =MANUTENZIONI.ID_PF_VOCE_IMPORTO) AS VOCE_DGR, " _
    '        & " (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE ID =MANUTENZIONI.ID_PF_VOCE) AS VOCE_BP, " _
    '        & " (SELECT MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYY/MM/DD')) FROM SISCOM_MI.EVENTI_MANUTENZIONE  " _
    '        & " WHERE COD_EVENTO = 'F92' " _
    '        & " AND MOTIVAZIONE = 'Da Bozza ad Emesso Ordine' AND ID_MANUTENZIONE = MANUTENZIONI.ID) AS DATA_EMISSIONE, " _
    '        & " TO_DATE(DATA_INIZIO_INTERVENTO,'YYYY/MM/DD') AS DATA_INIZIO_INTERVENTO, " _
    '        & " TO_DATE(DATA_FINE_INTERVENTO,'YYYY/MM/DD') AS DATA_FINE_INTERVENTO, " _
    '        & " (CASE WHEN (SELECT MAX(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO) FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE = MANUTENZIONI.ID) IS NOT NULL THEN " _
    '        & " TO_DATE((SELECT MAX(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO) FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE = MANUTENZIONI.ID),'YYYY/MM/DD')  " _
    '        & " ELSE " _
    '        & " TO_DATE(MANUTENZIONI.DATA_FINE_ORDINE,'YYYY/MM/DD') END) AS DATA_CHIUSURA_LAVORI, " _
    '        & " MANUTENZIONI.IMPORTO_PRESUNTO " _
    '        & " , (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS REPERTORIO_APPALTO " _
    '        & " ,(SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO) AS IMPORTO_PREVENTIVO " _
    '        & " ,(SELECT IMPORTO_APPROVATO-NVL(RIT_LEGGE_IVATA,0) FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO) AS IMPORTO_CONSUNTIVATO " _
    '        & " FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI " _
    '        & " WHERE ID_PF_VOCE_IMPORTO IS NOT NULL " _
    '        & " AND PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
    '        & " AND MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5)" _
    '        & " AND MANUTENZIONI.STATO NOT IN (5,6) " _
    '        & condizioneVista _
    '        & condizioneStato
    '        Dim dt As New Data.DataTable
    '        dt = par.getDataTableGrid(par.cmd.CommandText)
    '        connData.chiudi(False)
    '        If dt.Rows.Count > 0 Then
    '            Dim xls As New ExcelSiSol
    '            Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportODL", "ExportODL", dt)
    '            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
    '            Else
    '                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing, "")
    '            End If
    '        Else
    '            par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Nessun record da esportare!", 450, 150, "Attenzione", Nothing, "")
    '        End If
    '    Catch ex As Exception
    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Private Sub btnEsportaSegnalazioni_Click(sender As Object, e As EventArgs) Handles btnEsportaSegnalazioni.Click
        Try
            connData.apri(False)
            SettaStatiSegnalazioni(-1)


            Dim Query As String = EsportaQuerySegnalazioni(cmbCriticita.SelectedIndex)
            Dim dt As New Data.DataTable
            dt = par.getDataTableFilterSortRadGrid(Query, dgvSegnalazioni)
            connData.chiudi(False)
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSegnalazioni", "ExportSegnalazioni", dt)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing, "")
                End If
            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Nessun record da esportare!", 450, 150, "Attenzione", Nothing, "")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub cmbCategoria_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCategoria.SelectedIndexChanged
        dgvSegnalazioni.Rebind()
        NumeroSegnalazioni.Text = "Numero segnalazioni: " & dgvSegnalazioni.Items.Count
        caricaMappa()
        ImpostaKPI()
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home_ncp.aspx""</script>")
    End Sub

    Private Sub btnStampaTempoAttraversamento_Click(sender As Object, e As EventArgs) Handles btnStampaTempoAttraversamento.Click
        Try
            Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
            Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
            Dim condizioneVista As String = ""
            Dim condizioneBPAttivo As String = " AND MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5)"
            If RadButtonBuildingManager.Checked = True Then
                'BUILDING MANAGER
                condizioneVista = "AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT EDIFICI.ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")) OR MANUTENZIONI.ID_EDIFICIO IN (SELECT EDIFICI.ID FROM SISCOM_MI.EDIFICI WHERE ID_BM IN (SELECT ID_BM FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE FINE_VALIDITA='30000101' AND TIPO_OPERATORE=1 AND ID_OPERATORE=" & idOperatore & ")))"
            ElseIf RadButtonDirettoreLavori.Checked = True Then
                'DIRETTORE LAVORI
                condizioneVista = " AND MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO='30000000' AND ID_OPERATORE=" & idOperatore & "))"
            ElseIf RadButtonFieldQualityManager.Checked = True Then
                'FIELD QUALITY MANAGER
                condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
            ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                'TECNICO AMMINISTRATIVO
                condizioneVista = " AND (MANUTENZIONI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") OR MANUTENZIONI.ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"
            End If

            Dim condizioneStato As String = ""
            'par.cmd.CommandText = " SELECT progr || '/' || anno as ODL, " _
            '    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=STATO) AS STATO,(select MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYMMDD')) from SISCOM_MI.EVENTI_MANUTENZIONE WHERE MOTIVAZIONE = 'Da Bozza ad Emesso Ordine' AND ID_MANUTENZIONE=manutenzioni.id) as da_bozza_a_emesso, " _
            '    & " (select MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYMMDD')) from SISCOM_MI.EVENTI_MANUTENZIONE WHERE MOTIVAZIONE = 'Da Emesso Ordine a Consuntivato' AND ID_MANUTENZIONE=manutenzioni.id) as da_emesso_a_consuntivato, " _
            '    & " SISCOM_MI.GETTEMPOATTRAVERSAMENTOMANU(manutenzioni.id) as tempo_attraversamento " _
            '    & " FROM SISCOM_MI.MANUTENZIONI " _
            '    & " WHERE MANUTENZIONI.ID_PIANO_FINANZIARIO=(SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN WHERE ID_sTATO=5)" _
            '    & " AND MANUTENZIONI.STATO NOT IN (5,6) " _
            '    & " AND MANUTENZIONI.STATO IN (2,4) " _
            '    & condizioneVista _
            '    & condizioneStato

            par.cmd.CommandText = " SELECT progr || '/' || anno as ODL, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID=STATO) AS STATO,(select MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYMMDD')) from SISCOM_MI.EVENTI_MANUTENZIONE WHERE MOTIVAZIONE = 'Da Bozza ad Emesso Ordine' AND ID_MANUTENZIONE=manutenzioni.id) as da_bozza_a_emesso, " _
                & " (select MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYMMDD')) from SISCOM_MI.EVENTI_MANUTENZIONE WHERE MOTIVAZIONE = 'Da Emesso Ordine a Consuntivato' AND ID_MANUTENZIONE=manutenzioni.id) as da_emesso_a_consuntivato, " _
                & " SISCOM_MI.GETTEMPOATTRAVERSAMENTOMANU(manutenzioni.id) as tempo_attraversamento " _
                & " FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE  MANUTENZIONI.STATO NOT IN (5,6) " _
                & " AND MANUTENZIONI.STATO IN (2,4) " _
                & condizioneVista _
                & condizioneStato

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "TEMPO_ATTRAVERSAMENTO", "TEMPO_ATTRAVERSAMENTO", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnTempisticheExtraCanone_Click(sender As Object, e As EventArgs) Handles btnTempisticheExtraCanone.Click
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            Dim condizioneCriticitaColore As String = ""
            If IsNumeric(cmbCriticita.SelectedIndex) AndAlso cmbCriticita.SelectedIndex >= 0 Then
                'VA CONSIDERATO IL PERICOLO DI SEGNALAZIONE INIZIALE PERCHè POTREBBE VARIARE NEL TEMPO
                Dim indice As Integer = 0
                Select Case cmbCriticita.SelectedIndex
                    Case 0
                        'TUTTE
                        indice = 5
                    Case 1
                        'ROSSO
                        indice = 4
                    Case 2
                        'GIALLO
                        indice = 3
                    Case 3
                        'VERDE
                        indice = 2
                    Case 4
                        'BIANCO
                        indice = 1
                    Case 5
                        'BLU
                        indice = 0
                End Select
                If indice < 5 Then
                    condizioneCriticitaColore = " AND ID_PERICOLO_SEGNALAZIONE_INIZ=" & indice
                End If
            End If
            Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
            Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
            Dim condizioneStruttura As String = ""
            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
            Else
                'CONDIZIONE PER ESCLUDERE LA VISIONE
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
            End If
            Dim condizioneSelezionato As String = ""
            If RadButtonBuildingManager.Checked = True Then
                'BUILDING MANAGER
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA, SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " And EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " And INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"
            ElseIf RadButtonDirettoreLavori.Checked = True Then
                'DIRETTORE LAVORI
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA, SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID_SEGNALAZIONI=SEGNALAZIONI.ID " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"
            ElseIf RadButtonFieldQualityManager.Checked = True Then
                'FIELD QUALITY MANAGER
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA, SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"

            ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                'TECNICO AMMINISTRATIVO
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA, SEGNALAZIONI.TEMPO_CONTABILIZZAZIONE " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) <> 1"
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "TEMPO_ATTRAVERSAMENTO_EXTRACANONE", "TEMPO_ATTRAVERSAMENTO_EXTRACANONE", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnTempisticheCanone_Click(sender As Object, e As EventArgs) Handles btnTempisticheCanone.Click
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If

            Dim condizioneCriticitaColore As String = ""
            If IsNumeric(cmbCriticita.SelectedIndex) AndAlso cmbCriticita.SelectedIndex >= 0 Then
                'VA CONSIDERATO IL PERICOLO DI SEGNALAZIONE INIZIALE PERCHè POTREBBE VARIARE NEL TEMPO
                Dim indice As Integer = 0
                Select Case cmbCriticita.SelectedIndex
                    Case 0
                        'TUTTE
                        indice = 5
                    Case 1
                        'ROSSO
                        indice = 4
                    Case 2
                        'GIALLO
                        indice = 3
                    Case 3
                        'VERDE
                        indice = 2
                    Case 4
                        'BIANCO
                        indice = 1
                    Case 5
                        'BLU
                        indice = 0
                End Select
                If indice < 5 Then
                    condizioneCriticitaColore = " AND ID_PERICOLO_SEGNALAZIONE_INIZ=" & indice
                End If
            End If
            Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
            Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
            Dim condizioneStruttura As String = ""
            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ") "
            Else
                'CONDIZIONE PER ESCLUDERE LA VISIONE
                condizioneStruttura = " AND EDIFICI.ID_COMPLESSO=0 "
            End If
            '***Rif segn. SD 2519/2018***'
            Dim filtroData As String = " and SUBSTR (segnalazioni.DATA_ORA_RICHIESTA, 1, 8) >= '20180802'"
            '***Rif segn. SD 2519/2018***'
            Dim condizioneSelezionato As String = ""
            If RadButtonBuildingManager.Checked = True Then
                'BUILDING MANAGER

                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.BUILDING_MANAGER_OPERATORI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & " And EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                    & " And INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                    & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                    & " AND ID_OPERATORE=" & idOperatore _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData
            ElseIf RadButtonDirettoreLavori.Checked = True Then
                'DIRETTORE LAVORI
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE PROGRAMMA_ATTIVITA.ID=SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA " _
                    & " AND SISCOM_MI.GETDLFROMAPPALTI(PROGRAMMA_ATTIVITA.ID_GRUPPO)=" & idOperatore & ") " _
                    & " AND SEGNALAZIONI.ID_EDIFICIO>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData
            ElseIf RadButtonFieldQualityManager.Checked = True Then
                'FIELD QUALITY MANAGER
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData

            ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                'TECNICO AMMINISTRATIVO
                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID AS SEGNALAZIONE, " _
                    & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE TAB_STATI_SEGNALAZIONI.ID = ID_STATO) AS STATO," _
                    & " SEGNALAZIONI.TEMPO_PRESA_IN_CARICO, SEGNALAZIONI.TEMPO_RISOLUZIONE_TECNICA " _
                    & " FROM SISCOM_MI.SEGNALAZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.COMBINAZIONE_TIPOLOGIE " _
                    & " WHERE SEGNALAZIONI.ID_EDIFICIO = EDIFICI.ID " _
                    & condizioneStruttura _
                    & " AND EDIFICI.ID>1 " _
                    & " AND ID_STATO IN (6,7,10) " _
                    & condizioneCriticitaColore _
                    & condizioneSelezionato _
                    & " AND SEGNALAZIONI.ID_TIPO_SEGNALAZIONE=1 " _
                    & " AND ID_SEGNALAZIONE_PADRE IS NULL " _
                    & " AND TEMPO_PRESA_IN_CARICO>0 " _
                    & " AND combinazione_tipologie.id_tipo_segnalazione(+) = " _
                    & " segnalazioni.id_tipo_segnalazione " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_1(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_1, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_2(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_2, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_3(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_3, 0) " _
                    & " AND NVL (combinazione_tipologie.id_tipo_segnalazione_livello_4(+), 0) = " _
                    & " NVL (segnalazioni.id_tipo_segn_livello_4, 0) " _
                    & " AND nvl(NVL (segnalazioni.id_tipologia_manutenzione, combinazione_tipologie.id_tipo_manutenzione),0) = 1" & filtroData
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "TEMPO_ATTRAVERSAMENTO_CANONE", "TEMPO_ATTRAVERSAMENTO_CANONE", dt)
            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
