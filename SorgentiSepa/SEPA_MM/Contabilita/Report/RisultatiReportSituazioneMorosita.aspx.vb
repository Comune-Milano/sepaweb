Imports System.IO
Imports ExpertPdf.HtmlToPdf

Partial Class Contabilita_Report_RisultatiReportSituazioneMorosita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            caricaDati()
            filtriRicerca = Session.Item("filtriRicerca")
            Session.Remove("filtriRicerca")
        End If
    End Sub
    Public Property filtriRicerca() As String
        Get
            If Not (ViewState("filtriRicerca") Is Nothing) Then
                Return CStr(ViewState("filtriRicerca"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("filtriRicerca") = value
        End Set
    End Property

    Protected Sub chiudiConnessione()
        par.cmd.Dispose()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub ApriConnessione()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
    End Sub
    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Write("<script>self.close();</script>")
    End Sub

    Private Sub caricaDati(Optional Export As Boolean = False)

        '######## DATA AGGIORNAMENTO ##################
        Dim DataAggiornamento As String = ""
        Dim DataAggiornamentoDal As String = ""

        If Not IsNothing(Request.QueryString("DataAggiornamento")) And Request.QueryString("DataAggiornamento") <> "" Then
            DataAggiornamento = Request.QueryString("DataAggiornamento")
        Else
            DataAggiornamento = Format(Now, "yyyyMMdd")
        End If

        If Not IsNothing(Request.QueryString("DataAggiornamentoDal")) And Request.QueryString("DataAggiornamentoDal") <> "" Then
            DataAggiornamentoDal = Request.QueryString("DataAggiornamentoDal")
        Else
            DataAggiornamentoDal = "19000101"
        End If

        Dim condizioneDataAggiornamento As String = ""
        If DataAggiornamento <> "" Then
            condizioneDataAggiornamento = " data_operazione<=" & DataAggiornamento & " "
        End If

        If DataAggiornamentoDal <> "" Then
            condizioneDataAggiornamento &= " AND data_operazione>=" & DataAggiornamentoDal & " "
        End If
        '##########################################

        '######## DATA CONTABILE ##################
        Dim DataContabileDal As String = ""
        If Not IsNothing(Request.QueryString("DataContabileDa")) Then
            DataContabileDal = Request.QueryString("DataContabileDa")
        End If
        Dim condizioneDataContabileDal As String = ""
        If DataContabileDal <> "" Then
            condizioneDataContabileDal = " AND DATA_VALUTA>=" & DataContabileDal & " "
        End If

        Dim DataContabileAl As String = ""
        If Not IsNothing(Request.QueryString("DataContabileA")) Then
            DataContabileAl = Request.QueryString("DataContabileA")
        End If
        Dim condizioneDataContabileAl As String = ""
        If DataContabileAl <> "" Then
            condizioneDataContabileAl = " AND DATA_VALUTA<=" & DataContabileAl & " "
        End If
        '##########################################

        '######## DATA PAGAMENTO ##################
        Dim DataPagamentoDal As String = ""
        If Not IsNothing(Request.QueryString("DataPagamentoDal")) Then
            DataPagamentoDal = Request.QueryString("DataPagamentoDal")
        End If
        Dim condizioneDataPagamentoDal As String = ""
        If DataPagamentoDal <> "" Then
            condizioneDataPagamentoDal = " AND data_pagamento>=" & DataPagamentoDal & " "
        End If
        Dim DataPagamentoAl As String = ""
        If Not IsNothing(Request.QueryString("DataPagamentoAl")) Then
            DataPagamentoAl = Request.QueryString("DataPagamentoAl")
        End If
        Dim condizioneDataPagamentoAl As String = ""
        If DataPagamentoAl <> "" Then
            condizioneDataPagamentoAl = " AND data_pagamento<=" & DataPagamentoAl & " "
        End If
        '##########################################

        '########## DATA RIFERIMENTO ##############
        Dim dataRiferimentoDal As String = ""
        If Not IsNothing(Request.QueryString("RiferimentoDal")) Then
            dataRiferimentoDal = Request.QueryString("RiferimentoDal")
        End If
        Dim condizioneRiferimentoDal As String = ""
        If dataRiferimentoDal <> "" Then
            condizioneRiferimentoDal = " AND BOL_BOLLETTE.RIFERIMENTO_DA >=" & dataRiferimentoDal
        End If
        Dim dataRiferimentoAl As String = ""
        If Not IsNothing(Request.QueryString("RiferimentoAl")) Then
            dataRiferimentoAl = Request.QueryString("RiferimentoAl")
        End If
        Dim condizioneRiferimentoAl As String = ""
        If dataRiferimentoAl <> "" Then
            condizioneRiferimentoAl = " AND BOL_BOLLETTE.RIFERIMENTO_A <=" & dataRiferimentoAl
        End If
        '############################################# 

        '######## TIPOLOGIA CONDOMINI ##################
        Dim tipologiaCondominio As String = ""
        Dim condizioneTipologiaCondominio As String = ""
        If Not IsNothing(Request.QueryString("Condominio")) Then
            tipologiaCondominio = Request.QueryString("Condominio")
            Select Case tipologiaCondominio
                Case -1
                    'nessuna condizione
                    condizioneTipologiaCondominio = ""
                Case 0
                    'non in condominio
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=0 "
                Case 1
                    'condomini gestione diretta
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=1 "
                Case 2
                    'condomini gestione indiretta
                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=2 "
                Case 3
                    'tutti i condominilista RU

                    condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO>0 "
                Case Else
                    'nessuna condizione
                    condizioneTipologiaCondominio = ""
            End Select
        End If
        '##########################################

        '######## CONDIZIONE VOCI BOLLETTA ############
        Dim listaVoci As System.Collections.Generic.List(Of String) = Session.Item("listaVoci")
        Dim vociSi As Boolean = False
        Session.Remove("listaVoci")
        Dim condizioneListaVoci As String = ""
        If Not IsNothing(listaVoci) Then
            For Each Items As String In listaVoci
                condizioneListaVoci &= Items & ","
            Next
        End If
        If condizioneListaVoci <> "" Then
            condizioneListaVoci = Left(condizioneListaVoci, Len(condizioneListaVoci) - 1)
            condizioneListaVoci = " AND ID_VOCE IN (" & condizioneListaVoci & ") "
            vociSi = True
        End If
        '########################################################

        '######## CONDIZIONE MACROCATEGORIE ############
        Dim listaMacroCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaMacrocategorie")
        Session.Remove("listaMacrocategorie")
        Dim macroCategoriaSi As Boolean = False
        Dim condizionemacroCategoria As String = ""
        If Not IsNothing(listaMacroCategorie) Then
            For Each Items As String In listaMacroCategorie
                condizionemacroCategoria &= Items & ","
            Next
        End If
        If condizionemacroCategoria <> "" Then
            condizionemacroCategoria = Left(condizionemacroCategoria, Len(condizionemacroCategoria) - 1)
            condizionemacroCategoria = " AND ID_GRUPPO_VOCE_BOLLETTA IN (" & condizionemacroCategoria & ") "
            macroCategoriaSi = True
        End If
        '#######################################################

        '######## CONDIZIONE CAPITOLI ############
        Dim listaCapitoli As System.Collections.Generic.List(Of String) = Session.Item("listaCapitoli")
        Session.Remove("listaCapitoli")
        Dim condizioneCapitoli As String = ""
        If Not IsNothing(listaCapitoli) Then
            For Each Items As String In listaCapitoli
                condizioneCapitoli &= Items & ","
            Next
        End If
        If condizioneCapitoli <> "" Then
            condizioneCapitoli = Left(condizioneCapitoli, Len(condizioneCapitoli) - 1)
            condizioneCapitoli = " AND BBV.ID_CAP IN (" & condizioneCapitoli & ") "
        End If
        '########################################################

        '######## CONDIZIONE TIPOLOGIA UI ############
        Dim listatipologieUI As System.Collections.Generic.List(Of String) = Session.Item("listatipologieUI")
        Session.Remove("listatipologieUI")
        Dim TipologiaUISi As Boolean = False
        Dim condizionetipologiaUI As String = ""
        If Not IsNothing(listatipologieUI) Then
            For Each Items As String In listatipologieUI
                condizionetipologiaUI &= "'" & Items & "',"
            Next
        End If
        If condizionetipologiaUI <> "" Then
            condizionetipologiaUI = Left(condizionetipologiaUI, Len(condizionetipologiaUI) - 1)
            condizionetipologiaUI = " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA IN (" & condizionetipologiaUI & ") "
            TipologiaUISi = True
        End If
        '########################################################

        '######## ANNO ES CONTABILE ############
        Dim listaEserciziContabili As System.Collections.Generic.List(Of String) = Session.Item("listaEserciziContabili")
        Session.Remove("listaEserciziContabili")
        Dim condizioneListaEserciziContabili As String = ""
        If Not IsNothing(listaEserciziContabili) Then
            For Each Items As String In listaEserciziContabili
                condizioneListaEserciziContabili &= Items & ","
            Next
        End If
        Dim condizioneEsercizioContabile As String = ""
        If condizioneListaEserciziContabili <> "" Then
            condizioneListaEserciziContabili = Left(condizioneListaEserciziContabili, Len(condizioneListaEserciziContabili) - 1)
        End If
        If condizioneListaEserciziContabili <> "" Then
            condizioneEsercizioContabile = " AND BOL_BOLLETTE_ES_CONTABILE.ID IN  (" & condizioneListaEserciziContabili & ") "
        End If
        '########################################################

        '######## condizione tipologia contrattuale ##################        
        Dim listaTipologieContrattuali As System.Collections.Generic.List(Of String) = Session.Item("listaTipologieContrattuali")
        Session.Remove("listaTipologieContrattuali")
        Dim condizioneListaTipologiaContrattuale As String = ""

        If Not IsNothing(listaTipologieContrattuali) Then
            For Each Items As String In listaTipologieContrattuali
                condizioneListaTipologiaContrattuale &= "'" & Items & "',"
            Next
        End If
        Dim condizioneTipologiaContrattuale As String = ""
        If condizioneListaTipologiaContrattuale <> "" Then
            condizioneListaTipologiaContrattuale = Left(condizioneListaTipologiaContrattuale, Len(condizioneListaTipologiaContrattuale) - 1)
        End If
        If condizioneListaTipologiaContrattuale <> "" Then
            condizioneTipologiaContrattuale = " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC IN (" & condizioneListaTipologiaContrattuale & ")"
        End If
        '##########################################

        '######## condizione tipologia contrattuale ##################        
        Dim listaClasseAppartenenza As System.Collections.Generic.List(Of String) = Session.Item("listaClasseAppartenenza")
        Session.Remove("listaClasseAppartenenza")
        Dim condizionelistaClasseAppartenenza As String = ""

        If Not IsNothing(listaClasseAppartenenza) Then
            For Each Items As String In listaClasseAppartenenza
                condizionelistaClasseAppartenenza &= "'" & Items & "',"
            Next
        End If
        Dim condizioneClasseAppartenenza As String = ""
        If condizionelistaClasseAppartenenza <> "" Then
            condizionelistaClasseAppartenenza = Left(condizionelistaClasseAppartenenza, Len(condizionelistaClasseAppartenenza) - 1)
        End If
        If condizionelistaClasseAppartenenza <> "" Then
            condizioneClasseAppartenenza = " AND SOTTO_AREA IN (" & condizionelistaClasseAppartenenza & ")"
        End If
        '##########################################

        Dim condizioneEdificio As String = ""
        Dim condizioneComplesso As String = ""

        If Not IsNothing(Request.QueryString("Edificio")) And Request.QueryString("Edificio") <> "TUTTI" Then
            condizioneEdificio = " AND UNITA_IMMOBILIARI.ID_EDIFICIO = " & Request.QueryString("Edificio")
        End If
        If Not IsNothing(Request.QueryString("Complesso")) And Request.QueryString("Complesso") <> "TUTTI" Then
            condizioneComplesso = " AND COMPLESSI_IMMOBILIARI.ID = " & Request.QueryString("Complesso")
        End If

        '######## lista indirizzi ##################        
        Dim listaIndirizzi As System.Collections.Generic.List(Of String) = Session.Item("listaIndirizzi")
        Session.Remove("listaIndirizzi")
        Dim condizionelistaIndirizzi As String = ""

        If Not IsNothing(listaIndirizzi) Then
            For Each Items As String In listaIndirizzi
                condizionelistaIndirizzi &= "'" & Items & "',"
            Next
        End If
        Dim condizioneIndirizzi As String = ""
        If condizionelistaIndirizzi <> "" Then
            condizionelistaIndirizzi = Left(condizionelistaIndirizzi, Len(condizionelistaIndirizzi) - 1)
        End If
        If condizionelistaIndirizzi <> "" Then
            condizioneIndirizzi = " AND UNITA_IMMOBILIARI.ID_INDIRIZZO IN ( SELECT ID FROM SISCOM_MI.INDIRIZZI WHERE DESCRIZIONE IN (" & condizionelistaIndirizzi & ") )"
        End If
        '##########################################

        Dim listaBollettazione As System.Collections.Generic.List(Of String) = Session.Item("listaTipologiaBollettazione")
        Session.Remove("listaTipologiaBollettazione")
        Dim condizioneTipologiaBollettazione As String = ""
        If Not IsNothing(listaBollettazione) Then
            For Each Items As String In listaBollettazione
                condizioneTipologiaBollettazione &= Items & "," & CStr(CInt(Items) + 100) & ","
            Next
        End If
        If condizioneTipologiaBollettazione <> "" Then
            condizioneTipologiaBollettazione = Left(condizioneTipologiaBollettazione, Len(condizioneTipologiaBollettazione) - 1)
            condizioneTipologiaBollettazione = " AND BOL_BOLLETTE.ID_TIPO IN (" & condizioneTipologiaBollettazione & ") "
        Else
            condizioneTipologiaBollettazione = ""
        End If

        Dim ingiunto As Integer = 0
        If Not IsNothing(Request.QueryString("ingiunto")) Then
            ingiunto = Request.QueryString("ingiunto")
        End If
        Dim condizioneIngiunzione As String = ""
        If ingiunto = "1" Then
            condizioneIngiunzione = " AND nvl(importo_ingiunzione,0)>0"
        End If

        Dim ruolo As Integer = 0
        If Not IsNothing(Request.QueryString("ruolo")) Then
            ruolo = Request.QueryString("ruolo")
        End If
        Dim condizioneRuolo As String = ""
        If ruolo = "1" Then
            condizioneRuolo = " AND nvl(importo_ruolo,0)>0"
        End If

        Try            
            ApriConnessione()

            Dim qSelect As String = ""
            Dim qFrom As String = ""
            Dim qWhere As String = ""
            Dim qGroup As String = ""
            Dim estraiLista As Boolean = False
            Dim listaCodiciRU As String = Session.Item("listaRU")
            
            If Not IsNothing(listaCodiciRU) Then
                qFrom = " $RPT_CONTAB_MOROSITA_TMP$ , "
                qWhere = " $RPT_CONTAB_MOROSITA_TMP$.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND "
                estraiLista = True                
            End If            

            If Not IsNothing(Request.QueryString("TIPO")) Then
                If Not String.IsNullOrEmpty(Request.QueryString("TIPO").ToString) Then
                    If Request.QueryString("TIPO").ToString = "EM" Then
                        qSelect = " BOL_BOLLETTE.DATA_PAGAMENTO , BOL_BOLLETTE.DATA_SCADENZA , "
                        qGroup = " BOL_BOLLETTE.DATA_PAGAMENTO , BOL_BOLLETTE.DATA_SCADENZA , "
                    End If
                End If
            End If

            par.cmd.CommandText = "SELECT " _
                                & qSelect _
                                & "         BOL_BOLLETTE.ANNO , " _
                                & "         INITCAP ( CEIL ( TO_NUMBER ( SUBSTR (NVL (BOL_BOLLETTE.RIFERIMENTO_DA, '00000000'), 5, 2)) / 2)) || '° BIMESTRE' AS BIMESTRE , " _
                                & "         INITCAP(substr(nvl(VOCE,''),1,17)) AS VOCE, " _
                                & "         TO_DATE(BOL_BOLLETTE.RIFERIMENTO_DA,'YYYYMMDD') AS DAL ," _
                                & "         TO_DATE(BOL_BOLLETTE.RIFERIMENTO_A,'YYYYMMDD') AS AL ," _
                                & "         TO_DATE(BOL_BOLLETTE.DATA_EMISSIONE,'YYYYMMDD') AS EMISSIONE," _
                                & "         TO_DATE(BOL_BOLLETTE.DATA_SCADENZA,'YYYYMMDD')  AS SCADENZA," _
                                & "         INITCAP(substr(nvl(MACROCATEGORIA,''),1,17)) AS MACROCATEGORIA, " _
                                & "         INITCAP ((CASE WHEN TIPO_USO = 2 THEN 'Usi Abitativi' ELSE 'Usi Diversi' END)) AS USI_ABITATIVI , " _
                                & "         INITCAP (NVL (TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE, '')) AS TIPO_UI , " _
                                & "         BOL_BOLLETTE.INTESTATARIO," _
                                & "         ''''||RAPPORTI_UTENZA.COD_CONTRATTO AS COD_CONTRATTO, " _
                                & "         BOL_BOLLETTE.NUM_BOLLETTA, " _
                                & "         INITCAP(NVL(TIPO_BOLLETTE.DESCRIZIONE, '')) AS BOLLETTAZIONE," _
                                & "         INITCAP(CAPITOLO) AS CAPITOLO, " _
                                & "         NVL(BOL_BOLLETTE.IMPORTO_RUOLO,0) AS IMPORTO_RUOLO,NVL(BOL_BOLLETTE.IMPORTO_INGIUNZIONE,0) AS IMPORTO_INGIUNZIONE, " _
                                & "         DECODE(nvl(bol_bollette.id_rateizzazione,0),0,'NO','SI') as RAT, " _
                                & "         DECODE(nvl(bol_bollette.id_bolletta_ric,0),0,'NO','SI') as MOR,  " _
                                & "         SUM(BBV.IMPORTO) AS TOTALE, " _
                                & "         SUM(NVL(BBV.IMP_PAGATO,0)) AS INCASSATO,     " _
                                & "         SUM( NVL(BBV.IMPORTO, 0) - NVL( BBV.IMP_PAGATO, 0)) AS DA_PAGARE, " _
                                & "         RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC AS TIPOLOGIA_CONTRATTUALE, " _
                                & "         DECODE(ID_AREA_ECONOMICA,1, 'PROTEZIONE',2, 'ACCESSO',3, 'PERMANENZA',4, 'DECADENZA') || ' - '|| SOTTO_AREA AS CLASSE_APPARTENENZA, " _
                                & "         COMPLESSI_IMMOBILIARI.DENOMINAZIONE  AS COMPLESSO, " _
                                & "         EDIFICI.DENOMINAZIONE  AS EDIFICIO, " _
                                & "         INDIRIZZI.DESCRIZIONE || ' ' || INDIRIZZI.CIVICO   AS INDIRIZZO " _
                                & " FROM " _
                                & qFrom _
                                & "         SISCOM_MI.BOL_BOLLETTE," _
                                & "         SISCOM_MI.RAPPORTI_UTENZA," _
                                & "         SISCOM_MI.UNITA_IMMOBILIARI," _
                                & "         SISCOM_MI.INDIRIZZI," _
                                & "         SISCOM_MI.EDIFICI," _
                                & "         SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                                & "         SISCOM_MI.CANONI_EC," _
                                & "         SISCOM_MI.V_BOL_BOLLETTE_VOCI BBV," _
                                & "         SISCOM_MI.TIPO_BOLLETTE," _
                                & "         SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI," _
                                & "         SISCOM_MI.BOL_BOLLETTE_ES_CONTABILE " _
                                & " WHERE  " _
                                & qWhere _
                                & "         BBV.FL_NO_REPORT                                = 0 AND " _
                                & "         UNITA_IMMOBILIARI.ID                            = BOL_BOLLETTE.ID_UNITA             AND " _
                                & "         UNITA_IMMOBILIARI.ID_INDIRIZZO                  = INDIRIZZI.ID(+)                   AND " _
                                & "         UNITA_IMMOBILIARI.ID_EDIFICIO                   = EDIFICI.ID                        AND " _
                                & "         EDIFICI.ID_COMPLESSO                            = COMPLESSI_IMMOBILIARI.ID          AND " _
                                & "         BOL_BOLLETTE.ID_CONTRATTO                       = RAPPORTI_UTENZA.ID                AND " _
                                & "         BOL_BOLLETTE.ID                                 = BBV.ID_BOLLETTA                   AND " _
                                & "         BOL_BOLLETTE.ID_TIPO                            = TIPO_BOLLETTE.ID                  AND " _
                                & "         TIPOLOGIA_UNITA_IMMOBILIARI.COD                 = UNITA_IMMOBILIARI.COD_TIPOLOGIA   AND " _
                                & "         BOL_BOLLETTE.DATA_EMISSIONE               BETWEEN BOL_BOLLETTE_ES_CONTABILE.VALIDITA_DA AND BOL_BOLLETTE_ES_CONTABILE.VALIDITA_A AND " _
                                & "         RAPPORTI_UTENZA.ID_CANONI_EC                    = CANONI_EC.ID(+)                   AND " _
                                & "         BBV.FL_NO_REPORT                                = 0                                 AND " _
                                & "         BOL_BOLLETTE.DATA_SCADENZA                      < TO_CHAR(SYSDATE, 'yyyymmdd')      AND " _
                                & "         ABS(NVL(BBV.IMP_PAGATO, 0))                          < ABS(BBV.IMPORTO)             AND " _
                                & "         NVL(BOL_BOLLETTE.FL_ANNULLATA,0) = 0                                                AND " _
                                & "         NVL(BBV.FL_NO_SALDO,0) = 0                                                              " _
                                & condizioneCapitoli _
                                & condizioneListaVoci _
                                & condizioneEsercizioContabile _
                                & condizionetipologiaUI _
                                & condizioneTipologiaContrattuale _
                                & condizioneRiferimentoAl _
                                & condizioneRiferimentoDal _
                                & condizioneClasseAppartenenza _
                                & condizioneEdificio _
                                & condizioneComplesso _
                                & condizioneIndirizzi _
                                & condizioneTipologiaBollettazione _
                                & condizioneIngiunzione _
                                & condizioneRuolo _
                                & "GROUP BY " _
                                & qGroup _
                                & "         BOL_BOLLETTE.ANNO, " _
                                & "         BOL_BOLLETTE.INTESTATARIO , " _
                                & "         BOL_BOLLETTE.NUM_BOLLETTA , " _
                                & "         RIFERIMENTO_DA, " _
                                & "         VOCE, " _
                                & "         TIPO_BOLLETTE.DESCRIZIONE, " _
                                & "         CAPITOLO, " _
                                & " NVL(BOL_BOLLETTE.IMPORTO_RUOLO,0),NVL(BOL_BOLLETTE.IMPORTO_INGIUNZIONE,0)," _
                                & "         TIPOLOGIA_UNITA_IMMOBILIARI.DESCRIZIONE, " _
                                & "         TIPO_USO , " _
                                & "         MACROCATEGORIA, " _
                                & "         nvl(bol_bollette.id_rateizzazione,0)," _
                                & "         nvl(bol_bollette.id_bolletta_ric,0)," _
                                & "         RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC," _
                                & "         CANONI_EC.ID_AREA_ECONOMICA, " _
                                & "         CANONI_EC.SOTTO_AREA, " _
                                & "         COMPLESSI_IMMOBILIARI.DENOMINAZIONE," _
                                & "         EDIFICI.DENOMINAZIONE," _
                                & "         INDIRIZZI.DESCRIZIONE, " _
                                & "         INDIRIZZI.CIVICO, " _
                                & "         BOL_BOLLETTE.RIFERIMENTO_DA," _
                                & "         BOL_BOLLETTE.RIFERIMENTO_A," _
                                & "         BOL_BOLLETTE.DATA_EMISSIONE," _
                                & "         BOL_BOLLETTE.DATA_SCADENZA, " _
                                & "         RAPPORTI_UTENZA.COD_CONTRATTO"

            EstraiDati(par.cmd.CommandText, 11, estraiLista)
            Response.Write("<script>alert('Estrazione avviata! Attendere il completamento dell\'operazione.');location.href='../../Contratti/VisualizzaEstrazioni_RU.aspx?TIPO=RPT_MOR';</script>")

            chiudiConnessione()

        Catch ex As Exception
            chiudiConnessione()
            Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
        End Try
    End Sub

    Private Sub caricaDatiExtraMAV(tipoIncasso As String, Optional Export As Boolean = False)
        If tipoIncasso = 2 Or tipoIncasso = 0 Then

            '######## DATA AGGIORNAMENTO ##################
            Dim DataAggiornamento As String = ""
            Dim DataAggiornamentoDal As String = ""

            If Not IsNothing(Request.QueryString("DataAggiornamento")) And Request.QueryString("DataAggiornamento") <> "" Then
                DataAggiornamento = Request.QueryString("DataAggiornamento")
            Else
                DataAggiornamento = Format(Now, "yyyyMMdd")
            End If

            If Not IsNothing(Request.QueryString("DataAggiornamentoDal")) And Request.QueryString("DataAggiornamentoDal") <> "" Then
                DataAggiornamentoDal = Request.QueryString("DataAggiornamentoDal")
            Else
                DataAggiornamentoDal = "19000101"
            End If

            Dim condizioneDataAggiornamento As String = ""
            If DataAggiornamento <> "" Then
                condizioneDataAggiornamento = " data_operazione<=" & DataAggiornamento & " "
            End If

            If DataAggiornamentoDal <> "" Then
                condizioneDataAggiornamento &= " and data_operazione>=" & DataAggiornamentoDal & " "
            End If
            '##########################################


            '######## DATA CONTABILE ##################
            Dim DataContabileDal As String = ""
            If Not IsNothing(Request.QueryString("DataContabileDa")) Then
                DataContabileDal = Request.QueryString("DataContabileDa")
            End If
            Dim condizioneDataContabileDal As String = ""
            If DataContabileDal <> "" Then
                condizioneDataContabileDal = " AND DATA_VALUTA>=" & DataContabileDal & " "
            End If

            Dim DataContabileAl As String = ""
            If Not IsNothing(Request.QueryString("DataContabileA")) Then
                DataContabileAl = Request.QueryString("DataContabileA")
            End If
            Dim condizioneDataContabileAl As String = ""
            If DataContabileAl <> "" Then
                condizioneDataContabileAl = " AND DATA_VALUTA<=" & DataContabileAl & " "
            End If
            '##########################################

            '######## DATA PAGAMENTO ##################
            Dim DataPagamentoDal As String = ""
            If Not IsNothing(Request.QueryString("DataPagamentoDal")) Then
                DataPagamentoDal = Request.QueryString("DataPagamentoDal")
            End If
            Dim condizioneDataPagamentoDal As String = ""
            If DataPagamentoDal <> "" Then
                condizioneDataPagamentoDal = " AND data_pagamento>=" & DataPagamentoDal & " "
            End If
            Dim DataPagamentoAl As String = ""
            If Not IsNothing(Request.QueryString("DataPagamentoAl")) Then
                DataPagamentoAl = Request.QueryString("DataPagamentoAl")
            End If
            Dim condizioneDataPagamentoAl As String = ""
            If DataPagamentoAl <> "" Then
                condizioneDataPagamentoAl = " AND data_pagamento<=" & DataPagamentoAl & " "
            End If
            '##########################################
            '##########################################################################
            Dim dataRiferimentoDal As String = ""
            If Not IsNothing(Request.QueryString("RiferimentoDal")) Then
                dataRiferimentoDal = Request.QueryString("RiferimentoDal")
            End If
            Dim condizioneRiferimentoDal As String = ""
            If dataRiferimentoDal <> "" Then
                condizioneRiferimentoDal = " AND RIFERIMENTO_DA_BOL_BOLLETTE>=" & dataRiferimentoDal
            End If
            Dim dataRiferimentoAl As String = ""
            If Not IsNothing(Request.QueryString("RiferimentoAl")) Then
                dataRiferimentoAl = Request.QueryString("RiferimentoAl")
            End If
            Dim condizioneRiferimentoAl As String = ""
            If dataRiferimentoAl <> "" Then
                condizioneRiferimentoAl = " AND RIFERIMENTO_A_BOL_BOLLETTE<=" & dataRiferimentoAl
            End If
            '##########################################################################


            '######## TIPOLOGIA INCASSO ##################
            Dim tipologiaIncasso As String = ""
            Dim condizioneTipologiaIncasso As String = ""
            If Not IsNothing(Request.QueryString("TipologiaIncasso")) Then
                tipologiaIncasso = Request.QueryString("TipologiaIncasso")
                Select Case tipologiaIncasso
                    'Case 0
                    '    'TUTTE
                    '    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO > 0 "
                    'Case 1
                    '    'MAV
                    '    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 1 "
                    'Case 2
                    '    'EXTRAMAV
                    '    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO >= 2 "
                    'Case Else
                    '    'TUTTE
                    '    condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO > 0 "
                    Case 1
                        'MAV
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 1 "
                    Case 2
                        'EXTRAMAV
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 2 "
                    Case 3
                        'ALTRO
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 3 "
                    Case 4
                        'CREDITO GESTIONALE
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 4 "
                    Case 5
                        'CREDITO GESTIONALE C/C 59
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 5 "
                    Case 6
                        'CREDITO GESTIONALE C/C 60
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO = 6 "
                    Case Else
                        'TUTTE
                        condizioneTipologiaIncasso = " AND ID_TIPO_PAGAMENTO > 0 "
                End Select
            End If
            '##########################################

            '######## TIPOLOGIA CONDOMINI ##################
            Dim tipologiaCondominio As String = ""
            Dim condizioneTipologiaCondominio As String = ""
            If Not IsNothing(Request.QueryString("Condominio")) Then
                tipologiaCondominio = Request.QueryString("Condominio")
                Select Case tipologiaCondominio
                    Case -1
                        'nessuna condizione
                        condizioneTipologiaCondominio = ""
                    Case 0
                        'non in condominio
                        condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=0 "
                    Case 1
                        'condomini gestione diretta
                        condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=1 "
                    Case 2
                        'condomini gestione indiretta
                        condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO=2 "
                    Case 3
                        'tutti i condomini
                        condizioneTipologiaCondominio = " AND ID_TIPO_CONDOMINIO>0 "
                    Case Else
                        'nessuna condizione
                        condizioneTipologiaCondominio = ""
                End Select
            End If
            '##########################################

            '######## TIPOLOGIA CONTO CORRENTE ##################
            Dim tipologiaContoCorrente As String = ""
            Dim condizioneTipologiaContoCorrente As String = ""
            If Not IsNothing(Request.QueryString("TipologiaContoCorrente")) Then
                tipologiaContoCorrente = Request.QueryString("TipologiaContoCorrente")
                Select Case tipologiaContoCorrente
                    Case 0
                        'TUTTI
                        condizioneTipologiaContoCorrente = " AND ID_CC>0 "
                    Case 1
                        '59
                        condizioneTipologiaContoCorrente = " AND ID_CC=1 "
                    Case 2
                        '60
                        condizioneTipologiaContoCorrente = " AND ID_CC=2 "
                    Case Else
                        'TUTTI
                        condizioneTipologiaContoCorrente = " AND ID_CC>0 "
                End Select
            End If
            '##########################################

            '######## CONDIZIONE VOCI ############
            Dim listaVoci As System.Collections.Generic.List(Of String) = Session.Item("listaVoci")
            Dim vociSi As Boolean = False
            Session.Remove("listaVoci")
            Dim condizioneListaVoci As String = ""
            If Not IsNothing(listaVoci) Then
                For Each Items As String In listaVoci
                    condizioneListaVoci &= Items & ","
                Next
            End If
            If condizioneListaVoci <> "" Then
                condizioneListaVoci = Left(condizioneListaVoci, Len(condizioneListaVoci) - 1)
                condizioneListaVoci = " AND ID_T_VOCE_BOLLETTA IN (" & condizioneListaVoci & ") "
                vociSi = True
            End If
            '########################################################

            '######## CONDIZIONE CATEGORIE ############
            Dim listaCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaCategorie")
            Session.Remove("listaCategorie")
            Dim categoriaSi As Boolean = False
            Dim condizioneCategoria As String = ""
            If Not IsNothing(listaCategorie) Then
                For Each Items As String In listaCategorie
                    condizioneCategoria &= Items & ","
                Next
            End If
            If condizioneCategoria <> "" Then
                condizioneCategoria = Left(condizioneCategoria, Len(condizioneCategoria) - 1)
                condizioneCategoria = " AND ID_TIPO_VOCE_BOLLETTA IN (" & condizioneCategoria & ") "
                categoriaSi = True
            End If
            '########################################################

            '######## CONDIZIONE MACROCATEGORIE ############
            Dim listaMacroCategorie As System.Collections.Generic.List(Of String) = Session.Item("listaMacrocategorie")
            Session.Remove("listaMacrocategorie")
            Dim macroCategoriaSi As Boolean = False
            Dim condizionemacroCategoria As String = ""
            If Not IsNothing(listaMacroCategorie) Then
                For Each Items As String In listaMacroCategorie
                    condizionemacroCategoria &= Items & ","
                Next
            End If
            If condizionemacroCategoria <> "" Then
                condizionemacroCategoria = Left(condizionemacroCategoria, Len(condizionemacroCategoria) - 1)
                condizionemacroCategoria = " AND ID_GRUPPO_VOCE_BOLLETTA IN (" & condizionemacroCategoria & ") "
                macroCategoriaSi = True
            End If
            '########################################################

            '######## CONDIZIONE COMPETENZA ############
            Dim listaCompetenza As System.Collections.Generic.List(Of String) = Session.Item("listaCompetenza")
            Session.Remove("listaCompetenza")
            Dim competenzaSi As Boolean = False
            Dim condizioneCompetenza As String = ""
            If Not IsNothing(listaCompetenza) Then
                For Each Items As String In listaCompetenza
                    condizioneCompetenza &= Items & ","
                Next
            End If
            If condizioneCompetenza <> "" Then
                condizioneCompetenza = Left(condizioneCompetenza, Len(condizioneCompetenza) - 1)
                condizioneCompetenza = " AND ID_COMPETENZA IN (" & condizioneCompetenza & ") "
                competenzaSi = True
            End If
            '########################################################

            '######## CONDIZIONE TIPOLOGIA BOLLETTAZIONE ############
            Dim listaBollettazione As System.Collections.Generic.List(Of String) = Session.Item("listaTipologiaBollettazione")
            Session.Remove("listaTipologiaBollettazione")
            Dim condizioneTipologiaBollettazione As String = ""
            If Not IsNothing(listaBollettazione) Then
                For Each Items As String In listaBollettazione
                    condizioneTipologiaBollettazione &= Items & "," & CStr(CInt(Items) + 100) & ","
                Next
            End If
            If condizioneTipologiaBollettazione <> "" Then
                'condizioneTipologiaBollettazione &= "22,"
                condizioneTipologiaBollettazione = Left(condizioneTipologiaBollettazione, Len(condizioneTipologiaBollettazione) - 1)
                condizioneTipologiaBollettazione = " AND ID_TIPO_BOLLETTA IN (" & condizioneTipologiaBollettazione & ") "
            Else
                'condizioneTipologiaBollettazione = " AND (BOL_BOLLETTE.ID_TIPO IN (1,2,7) OR BOL_BOLLETTE.ID_TIPO>20) "
                condizioneTipologiaBollettazione = ""
            End If
            '########################################################

            '######## CONDIZIONE CAPITOLI ############
            Dim listaCapitoli As System.Collections.Generic.List(Of String) = Session.Item("listaCapitoli")
            Session.Remove("listaCapitoli")
            Dim condizioneCapitoli As String = ""
            If Not IsNothing(listaCapitoli) Then
                For Each Items As String In listaCapitoli
                    condizioneCapitoli &= Items & ","
                Next
            End If
            If condizioneCapitoli <> "" Then
                condizioneCapitoli = Left(condizioneCapitoli, Len(condizioneCapitoli) - 1)
                condizioneCapitoli = " AND CAP IN (" & condizioneCapitoli & ") "
            End If
            '########################################################

            '######## CONDIZIONE TIPOLOGIA ############
            Dim listatipologieUI As System.Collections.Generic.List(Of String) = Session.Item("listatipologieUI")
            Session.Remove("listatipologieUI")
            Dim TipologiaUISi As Boolean = False
            Dim condizionetipologiaUI As String = ""
            If Not IsNothing(listatipologieUI) Then
                For Each Items As String In listatipologieUI
                    condizionetipologiaUI &= "'" & Items & "',"
                Next
            End If
            If condizionetipologiaUI <> "" Then
                condizionetipologiaUI = Left(condizionetipologiaUI, Len(condizionetipologiaUI) - 1)
                condizionetipologiaUI = " AND TIPOLOGIA_UNITA_IMMOBILIARE IN (" & condizionetipologiaUI & ") "
                TipologiaUISi = True
            End If
            '########################################################

            '######## ANNO ES CONTABILE ############
            Dim listaEserciziContabili As System.Collections.Generic.List(Of String) = Session.Item("listaEserciziContabili")
            Session.Remove("listaEserciziContabili")
            Dim condizioneListaEserciziContabili As String = ""
            If Not IsNothing(listaEserciziContabili) Then
                For Each Items As String In listaEserciziContabili
                    condizioneListaEserciziContabili &= Items & ","
                Next
            End If
            Dim condizioneEsercizioContabile As String = ""
            If condizioneListaEserciziContabili <> "" Then
                condizioneListaEserciziContabili = Left(condizioneListaEserciziContabili, Len(condizioneListaEserciziContabili) - 1)
            End If
            If condizioneListaEserciziContabili <> "" Then
                condizioneEsercizioContabile = " AND ID_ES_CONTABILE IN (" & condizioneListaEserciziContabili & ") "
            End If
            '########################################################

            '######## tipo extramav ############
            Dim condizioneTipoExtraMAV As String = ""
            If Not IsNothing(Request.QueryString("TipoExtraMAV")) AndAlso Request.QueryString("TipoExtraMAV") <> "" AndAlso Request.QueryString("TipoExtraMAV") <> 0 Then
                If Request.QueryString("TipoExtraMAV") <> 0 Then
                    condizioneTipoExtraMAV &= " AND ID_TIPO_INCASSO_EXTRAMAV=" & Request.QueryString("TipoExtraMAV")
                End If
            End If
            '########################################################

            '######## condizione numero assegno ##################
            Dim numeroAssegno As String = ""
            If Not IsNothing(Request.QueryString("NumeroAssegno")) Then
                numeroAssegno = Request.QueryString("NumeroAssegno")
            End If
            Dim condizioneNumeroAssegno As String = ""
            If numeroAssegno <> "" Then
                'condizioneNumeroAssegno = " AND ID_VOCE_BOLLETTA IN " _
                '    & " (SELECT ID_VOCE_BOLLETTA FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
                '    & " WHERE ID_EVENTO_PRINCIPALE IN (SELECT ID FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI " _
                '    & " WHERE ID_INCASSO_EXTRAMAV IN (SELECT ID FROM SISCOM_MI.INCASSI_EXTRAMAV " _
                '    & " WHERE NUMERO_ASSEGNO='" & numeroAssegno & "'))) "
                condizioneNumeroAssegno = " AND ID_INCASSO_EXTRAMAV IN (SELECT ID FROM SISCOM_MI.INCASSI_EXTRAMAV " _
                    & " WHERE NUMERO_ASSEGNO='" & numeroAssegno & "') "
            End If
            '##########################################

            Dim selectMacroCategoria As String = ""
            Dim selectCategoria As String = ""
            Dim selectVoci As String = ""
            Dim selectTipologiaUI As String = ""
            Dim selectCompetenza As String = ""
            Dim groupByMacroCategoria As String = ""
            Dim groupByCategoria As String = ""
            Dim groupByVoci As String = ""
            Dim groupByTipologiaUI As String = ""
            Dim groupByCompetenza As String = ""



            selectMacroCategoria = " INITCAP(substr(nvl(MACROCATEGORIA,''),1,17)) AS MACROCATEGORIA, "
            'selectMacroCategoria = " INITCAP(nvl(MACROCATEGORIA,'')) AS MACROCATEGORIA, "
            groupByMacroCategoria = ",nvl(MACROCATEGORIA,'') "


            selectCategoria = " INITCAP(substr(nvl(CATEGORIA,''),1,17)) AS CATEGORIA, "
            'selectCategoria = " INITCAP(nvl(CATEGORIA,'')) AS CATEGORIA, "
            groupByCategoria = ",nvl(CATEGORIA,'') "



            selectVoci = " INITCAP(substr(nvl(VOCE,''),1,17)) AS VOCE, "
            'selectVoci = " INITCAP(nvl(VOCE,'')) AS VOCE, "
            groupByVoci = ",nvl(VOCE,'') "


            selectCompetenza = " INITCAP(COMPETENZA) AS COMPETENZA, "
            groupByCompetenza = " ,COMPETENZA "





            selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI, INITCAP(NVL(TIPO_UI,'')) AS TIPO_UI, "
            groupByTipologiaUI = ",USI_ABITATIVI,TIPO_UI "


            Dim dettaglio As String = ""
            Dim dettaglioGroup As String = ""

            dettaglio = " ANNO, " _
                & " INITCAP(BIMESTRE) AS BIMESTRE, "
            dettaglioGroup = ",ANNO, BIMESTRE "

            Try


                ApriConnessione()

                par.cmd.CommandText = " SELECT cod_intestatario,intestatario,num_bolletta," _
                    & " INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE,INITCAP(CAPITOLO) AS CAPITOLO, " _
                    & " COMPETENZA_ACC, " _
                    & dettaglio _
                    & selectCompetenza _
                    & selectMacroCategoria _
                    & selectCategoria _
                    & selectVoci _
                    & selectTipologiaUI _
                    & " TRIM(TO_CHAR((NVL(IMPORTO_PAGATO,0)),'999G999G990D99')) AS IMPORTO " _
                    & " FROM SISCOM_MI.BOL_BOLLETTE_VOCI_PAGAMENTI " _
                    & " WHERE " _
                    & condizioneDataAggiornamento _
                    & condizioneDataContabileDal _
                    & condizioneDataContabileAl _
                    & condizioneDataPagamentoDal _
                    & condizioneDataPagamentoAl _
                    & condizioneRiferimentoAl _
                    & condizioneRiferimentoDal _
                    & condizioneTipologiaIncasso _
                    & " AND ID_INCASSO_EXTRAMAV IN (SELECT ID_INCASSO_EXTRAMAV FROM SISCOM_MI.INCASSI_aTTRIBUITI) " _
                    & condizioneTipoExtraMAV _
                    & condizioneTipologiaCondominio _
                    & condizioneTipologiaContoCorrente _
                    & condizioneListaVoci _
                    & condizioneCategoria _
                    & condizionemacroCategoria _
                    & condizioneCompetenza _
                    & condizioneTipologiaBollettazione _
                    & condizioneCapitoli _
                    & condizioneNumeroAssegno _
                    & condizionetipologiaUI _
                    & condizioneEsercizioContabile _
                    & " AND FL_NO_REPORT=0 "


                If Export = False Then
                    hfCaricaExtraMAV.Value = par.cmd.CommandText

                    Dim capitolisi As Boolean = True
                    Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dTable As New Data.DataTable
                    dAdapter.Fill(dTable)
                    'Dim valorePrecedente As Decimal = 0
                    'For Each Items As Data.DataRow In dTable.Rows
                    '    If Not IsDBNull(Items.Item(11)) Then
                    '        valorePrecedente = Items.Item(11)
                    '    Else
                    '        Items.Item(11) = valorePrecedente
                    '    End If
                    'Next
                    'Dim dataTableRibaltata As New Data.DataTable
                    'dataTableRibaltata.Columns.Clear()
                    'For Each colonna As Data.DataColumn In dTable.Columns
                    '    dataTableRibaltata.Columns.Add(colonna.ColumnName)
                    'Next
                    'Dim Nrighe As Integer = dTable.Rows.Count
                    'Dim riga As Data.DataRow
                    'Dim rigaPrec As Data.DataRow
                    'Dim rigaConfronto As Data.DataRow
                    'Dim bollettazionePrecedente As String = ""
                    'Dim annoCompetenza As String = ""
                    'Dim annoPrecedente As String = ""
                    'Dim capitoloPrecedente As String = ""
                    'Dim bimestrePrecedente As String = ""
                    'Dim competenzaPrecedente As String = ""
                    'Dim macrocategoriaPrecedente As String = ""
                    'Dim categoriaPrecedente As String = ""
                    'Dim vocePrecedente As String = ""
                    'Dim tipoUIPrecedente As String = ""
                    'rigaPrec = dataTableRibaltata.NewRow
                    'For i As Integer = 0 To Nrighe - 1
                    '    riga = dataTableRibaltata.NewRow
                    '    rigaConfronto = dataTableRibaltata.NewRow
                    '    If i = 0 Then
                    '        Dim indice As Integer = 0
                    '        riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>Totale</font>"
                    '        indice += 1
                    '        riga.Item("CAPITOLO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("COMPETENZA_ACC") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("ANNO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("BIMESTRE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("COMPETENZA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("MACROCATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("CATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("VOCE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("USI_ABITATIVI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("TIPO_UI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                    '        indice += 1
                    '        riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                    '        rigaPrec = riga

                    '    Else
                    '        Dim indice2 As Integer = 0

                    '        rigaConfronto.Item("BOLLETTAZIONE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("CAPITOLO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("COMPETENZA_ACC") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("ANNO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("BIMESTRE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("COMPETENZA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("MACROCATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("CATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("VOCE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("USI_ABITATIVI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("TIPO_UI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                    '        indice2 += 1
                    '        rigaConfronto.Item("IMPORTO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")

                    '        Dim indice As Integer = 0
                    '        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '            If bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                riga.Item("BOLLETTAZIONE") = ""
                    '            Else
                    '                riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                    '                bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                capitoloPrecedente = ""
                    '                annoPrecedente = ""
                    '                bimestrePrecedente = ""
                    '                competenzaPrecedente = ""
                    '                macrocategoriaPrecedente = ""
                    '                categoriaPrecedente = ""
                    '                vocePrecedente = ""
                    '                tipoUIPrecedente = ""
                    '            End If
                    '        Else
                    '            riga.Item("BOLLETTAZIONE") = ""
                    '        End If
                    '        indice += 1

                    '        If capitolisi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("CAPITOLO") = ""
                    '                Else
                    '                    riga.Item("CAPITOLO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                    '                    capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    annoPrecedente = ""
                    '                    bimestrePrecedente = ""
                    '                    competenzaPrecedente = ""
                    '                    macrocategoriaPrecedente = ""
                    '                    categoriaPrecedente = ""
                    '                    vocePrecedente = ""
                    '                    tipoUIPrecedente = ""
                    '                End If
                    '            Else
                    '                riga.Item("CAPITOLO") = ""
                    '            End If

                    '        End If
                    '        indice += 1
                    '        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '            If annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                riga.Item("COMPETENZA_ACC") = ""
                    '            Else
                    '                riga.Item("COMPETENZA_ACC") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                    '                annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                annoPrecedente = ""
                    '                bimestrePrecedente = ""
                    '                competenzaPrecedente = ""
                    '                macrocategoriaPrecedente = ""
                    '                categoriaPrecedente = ""
                    '                vocePrecedente = ""
                    '                tipoUIPrecedente = ""
                    '            End If
                    '        Else
                    '            riga.Item("COMPETENZA_ACC") = ""
                    '        End If
                    '        indice += 1

                    '        If dettaglioSi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If annoPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("ANNO") = ""
                    '                Else
                    '                    riga.Item("ANNO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    annoPrecedente = riga.Item("ANNO")
                    '                    bimestrePrecedente = ""
                    '                    competenzaPrecedente = ""
                    '                    macrocategoriaPrecedente = ""
                    '                    categoriaPrecedente = ""
                    '                    vocePrecedente = ""
                    '                    tipoUIPrecedente = ""
                    '                End If
                    '            Else
                    '                riga.Item("ANNO") = ""
                    '            End If
                    '        End If
                    '        indice += 1

                    '        If dettaglioSi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If bimestrePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("BIMESTRE") = ""
                    '                Else
                    '                    riga.Item("BIMESTRE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    bimestrePrecedente = riga.Item("BIMESTRE")
                    '                    competenzaPrecedente = ""
                    '                    macrocategoriaPrecedente = ""
                    '                    categoriaPrecedente = ""
                    '                    vocePrecedente = ""
                    '                    tipoUIPrecedente = ""
                    '                End If
                    '            Else
                    '                riga.Item("BIMESTRE") = ""
                    '            End If
                    '        End If

                    '        indice += 1
                    '        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '            If competenzaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                riga.Item("COMPETENZA") = ""
                    '            Else
                    '                riga.Item("COMPETENZA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                competenzaPrecedente = riga.Item("COMPETENZA")
                    '                macrocategoriaPrecedente = ""
                    '                categoriaPrecedente = ""
                    '                vocePrecedente = ""
                    '                tipoUIPrecedente = ""
                    '            End If
                    '        Else
                    '            riga.Item("COMPETENZA") = ""
                    '        End If
                    '        indice += 1
                    '        If macroCategoriaSi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If macrocategoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("MACROCATEGORIA") = ""
                    '                Else
                    '                    riga.Item("MACROCATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    macrocategoriaPrecedente = riga.Item("MACROCATEGORIA")
                    '                    categoriaPrecedente = ""
                    '                    vocePrecedente = ""
                    '                    tipoUIPrecedente = ""
                    '                End If
                    '            Else
                    '                riga.Item("MACROCATEGORIA") = ""
                    '            End If

                    '        End If
                    '        indice += 1
                    '        If categoriaSi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If categoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("CATEGORIA") = ""
                    '                Else
                    '                    riga.Item("CATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    categoriaPrecedente = riga.Item("CATEGORIA")
                    '                    vocePrecedente = ""
                    '                    tipoUIPrecedente = ""
                    '                End If
                    '            Else
                    '                riga.Item("CATEGORIA") = ""
                    '            End If

                    '        End If
                    '        indice += 1
                    '        If vociSi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If vocePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("VOCE") = ""
                    '                Else
                    '                    riga.Item("VOCE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    vocePrecedente = riga.Item("VOCE")
                    '                    tipoUIPrecedente = ""
                    '                End If
                    '            Else
                    '                riga.Item("VOCE") = ""
                    '            End If

                    '        End If
                    '        indice += 1
                    '        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '            If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                riga.Item("USI_ABITATIVI") = ""
                    '            Else
                    '                If IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice + 1)) Then
                    '                    riga.Item("USI_ABITATIVI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    tipoUIPrecedente = ""
                    '                Else
                    '                    riga.Item("USI_ABITATIVI") = ""
                    '                End If
                    '                'tipoUIPrecedente = riga.Item("USI_ABITATIVI")
                    '            End If
                    '        Else
                    '            riga.Item("USI_ABITATIVI") = ""
                    '        End If
                    '        indice += 1
                    '        If TipologiaUISi Then
                    '            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                    '                    riga.Item("TIPO_UI") = ""
                    '                Else
                    '                    riga.Item("TIPO_UI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '                    tipoUIPrecedente = riga.Item("TIPO_UI")
                    '                End If
                    '            Else
                    '                riga.Item("TIPO_UI") = ""
                    '            End If
                    '            indice += 1
                    '        Else
                    '            indice += 1
                    '        End If
                    '        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                    '            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Or par.IfNull(riga.Item("COMPETENZA_ACC"), "") <> "" Then
                    '                riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                    '            Else
                    '                riga.Item("IMPORTO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                    '            End If

                    '        Else
                    '            riga.Item("IMPORTO") = ""
                    '        End If

                    '    End If
                    '    If par.IfNull(rigaPrec.Item("TIPO_UI"), "") = par.IfNull(rigaConfronto.Item("TIPO_UI"), "") _
                    '        And par.IfNull(rigaPrec.Item("IMPORTO"), "") = par.IfNull(rigaConfronto.Item("IMPORTO"), "") _
                    '        And par.IfNull(riga.Item("competenza_acc"), "") = "" _
                    '        And par.IfNull(riga.Item("anno"), "") = "" _
                    '        And par.IfNull(riga.Item("capitolo"), "") = "" _
                    '        And par.IfNull(riga.Item("bimestre"), "") = "" _
                    '        And par.IfNull(riga.Item("competenza"), "") = "" _
                    '        And par.IfNull(riga.Item("macrocategoria"), "") = "" _
                    '        And par.IfNull(riga.Item("bollettazione"), "") = "" _
                    '        And par.IfNull(riga.Item("categoria"), "") = "" _
                    '        And par.IfNull(riga.Item("voce"), "") = "" _
                    '        And par.IfNull(riga.Item("USI_ABITATIVI"), "") = "" Then
                    '        'RIGHE UGUALI DA ELIMINARE

                    '        If par.IfNull(riga.Item("TIPO_UI"), "") = par.IfNull(dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(9), "") Then
                    '            riga.Item("TIPO_UI") = ""
                    '        End If

                    '    Else
                    '        'If (Nrighe - 1 - i) <> 1 Then 'And (Nrighe - 1 - i) <> 2
                    '        dataTableRibaltata.Rows.Add(riga)
                    '        rigaPrec = rigaConfronto
                    '        'End If
                    '    End If
                    'Next

                    If dTable.Rows.Count > 0 Then
                        'Dim indiceVisibile As Integer = 1
                        'If Not capitolisi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        ''indiceVisibile = 4
                        'indiceVisibile = 3
                        'If Not dettaglioSi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        'indiceVisibile += 1
                        'If Not dettaglioSi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        'indiceVisibile += 1
                        'If Not competenzaSi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        'indiceVisibile += 1
                        'If Not macroCategoriaSi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        'indiceVisibile += 1
                        'If Not categoriaSi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        'indiceVisibile += 1
                        'If Not vociSi Then
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        'indiceVisibile += 1
                        'If Not TipologiaUISi Then
                        '    'datagridincassiextramav.Columns.Item(indiceVisibile).Visible = False
                        '    indiceVisibile += 1
                        '    DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                        'End If
                        DataGridIncassiExtraMAV.DataSource = dTable
                        DataGridIncassiExtraMAV.DataBind()
                        'ImageButtonExcel.Visible = True
                        'ImageButtonStampa.Visible = True
                        LabelTitoloIncassiAttribuiti.Text = "Riepilogo incassi attribuiti "
                        If DataContabileAl <> "" And DataContabileDal <> "" Then
                            LabelTitolo.Text = "Situazione Incassi attribuiti dal " & par.FormattaData(DataContabileDal) & " al " & par.FormattaData(DataContabileAl)
                        ElseIf DataContabileAl <> "" And DataContabileDal = "" Then
                            LabelTitolo.Text = "Situazione Incassi attribuitial " & par.FormattaData(DataContabileAl)
                        ElseIf DataContabileAl = "" And DataContabileDal <> "" Then
                            LabelTitolo.Text = "Situazione Incassi attribuiti dal " & par.FormattaData(DataContabileDal)
                        ElseIf DataContabileAl = "" And DataContabileDal = "" Then
                            LabelTitolo.Text = "Situazione Incassi attribuiti"
                        End If
                        DataGridIncassiExtraMAV.Visible = True
                        Me.exportExtra.Visible = True
                    Else
                        DataGridIncassiExtraMAV.Visible = False
                        Me.exportExtra.Visible = False

                        'LabelErrore.Text = "La ricerca non ha prodotto nessun risultato! Modificare i parametri di ricerca e riprovare"
                        'ImageButtonExcel.Visible = False
                        'ImageButtonStampa.Visible = False
                        'LabelTitolo.Text = "Situazione Incassi"
                    End If
                Else

                    Session.Add("query", hfCaricaExtraMAV.Value)
                    Response.Write("<script>window.showModalDialog('RptXLS.aspx?NOMEFILE=ExportExtramav','DettGest', 'status:no;dialogWidth:400px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');</script>")

                End If


                chiudiConnessione()
            Catch ex As Exception
                chiudiConnessione()
                Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
            End Try
        Else
            LabelTitoloIncassiAttribuiti.Visible = False
            DataGridIncassiExtraMAV.Visible = False
            Me.exportExtra.Visible = False
        End If

    End Sub

    Private Sub caricaIncassiNonAttribuibili(tipologiaIncasso As String, Optional Export As Boolean = False)
        Try

            If tipologiaIncasso = 2 Or tipologiaIncasso = 0 Then
                Dim dataDalIncassiNonAttribuibili As String = ""
                Dim dataAlIncassiNonAttribuibili As String = ""
                Dim condizioneDataDal As String = ""
                Dim condizioneDataAl As String = ""

                If Not IsNothing(Request.QueryString("IncassiDal")) Then
                    dataDalIncassiNonAttribuibili = Request.QueryString("IncassiDal")
                    If dataDalIncassiNonAttribuibili <> "" Then
                        condizioneDataDal = " WHERE DATA_INCASSO>=" & dataDalIncassiNonAttribuibili & " "
                    End If
                End If

                If Not IsNothing(Request.QueryString("IncassiAl")) Then
                    dataAlIncassiNonAttribuibili = Request.QueryString("IncassiAl")
                    If dataAlIncassiNonAttribuibili <> "" Then
                        If condizioneDataDal = "" Then
                            condizioneDataAl = " WHERE DATA_INCASSO<=" & dataAlIncassiNonAttribuibili & " "
                        Else
                            condizioneDataAl = " AND DATA_INCASSO<=" & dataAlIncassiNonAttribuibili & " "
                        End If
                    End If
                End If

                ApriConnessione()
                par.cmd.CommandText = "SELECT " _
                    & " TO_CHAR(TO_DATE(DATA_INCASSO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_INCASSO " _
                    & ",INITCAP(NOMINATIVO) AS NOMINATIVO " _
                    & ",TRIM(TO_CHAR(IMPORTO,'999G999G990D99')) AS IMPORTO " _
                    & ",CAUSALE " _
                    & ",NOTE " _
                    & ",CASE WHEN FL_ATTRIBUITO=1 THEN 'Sì' ELSE 'NO' END AS ATTRIBUITO " _
                    & ",TO_CHAR(TO_DATE(DATA_aTTRIBUZIONE,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_ATTRIBUZIONE " _
                    & " FROM SISCOM_MI.INCASSI_NON_ATTRIBUIBILI " & condizioneDataDal & condizioneDataAl


                If Export = False Then
                    hfcaricaNonAttribuibili.Value = par.cmd.CommandText

                    Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dTable As New Data.DataTable
                    dAdapter.Fill(dTable)


                    If dTable.Rows.Count > 0 Then
                        DataGridIncassiNonAttribuibili.DataSource = dTable
                        DataGridIncassiNonAttribuibili.DataBind()
                        DataGridIncassiNonAttribuibili.Visible = True
                        Me.exportnonAttrib.Visible = True

                        If condizioneDataAl <> "" And condizioneDataDal <> "" Then
                            LabelTitoloNonAttribuibili.Text &= " dal " & par.FormattaData(dataDalIncassiNonAttribuibili) & " al " & par.FormattaData(dataAlIncassiNonAttribuibili)
                        ElseIf condizioneDataAl = "" And condizioneDataDal <> "" Then
                            LabelTitoloNonAttribuibili.Text &= " dal " & par.FormattaData(dataDalIncassiNonAttribuibili)
                        ElseIf condizioneDataAl <> "" And condizioneDataDal = "" Then
                            LabelTitoloNonAttribuibili.Text &= " al " & par.FormattaData(dataAlIncassiNonAttribuibili)
                        ElseIf condizioneDataAl = "" And condizioneDataDal = "" Then
                        End If
                        LabelTitoloNonAttribuibili.Visible = False

                    Else
                        DataGridIncassiNonAttribuibili.Visible = False
                        Me.exportnonAttrib.Visible = False

                    End If
                    LabelTitoloNonAttribuibili.Visible = True
                Else
                    Session.Add("query", hfcaricaNonAttribuibili.Value)
                    Response.Write("<script>window.showModalDialog('RptXLS.aspx?NOMEFILE=ExportNonAttribuibili','DettGest', 'status:no;dialogWidth:400px;dialogHeight:400px;dialogHide:true;help:no;scroll:no');</script>")


                End If
            Else
                LabelTitoloNonAttribuibili.Visible = False
                DataGridIncassiNonAttribuibili.Visible = False
                Me.exportnonAttrib.Visible = False

            End If
            chiudiConnessione()

        Catch ex As Exception
            chiudiConnessione()
            Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
        End Try
    End Sub

    Function StampaDataGridPDF_1(ByVal datagrid As DataGrid, ByVal nomeStampa As String, Optional ByVal titolo As String = "", Optional ByVal footer As String = "", Optional ByVal larghezzaPagina As Integer = 1800, Optional ByVal orientamentoLandscape As Boolean = True, Optional ByVal mostraNumeriPagina As Boolean = True, Optional ByVal contaRighe As Boolean = False, Optional righe As Integer = 25, Optional ByVal ripetiIntestazioniSoloConContaRighe As Boolean = False, Optional ByVal sottotitolo As String = "", Optional ByVal DataGrid2 As DataGrid = Nothing, Optional ByVal DataGrid3 As DataGrid = Nothing) As String
        Try
            'RENDERCONTROL DEL DATAGRID
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            stringWriter = New System.IO.StringWriter
            sourcecode = New HtmlTextWriter(stringWriter)
            datagrid.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = Html & stringWriter.ToString
            'ELIMINAZIONE EVENTUALI HYPERLINK
            Html = par.EliminaLink(Html)
            If contaRighe = True And righe > 0 Then
                Dim TitoliDaRipetere As String = ""
                If ripetiIntestazioniSoloConContaRighe = True Then
                    Dim indiceInizioPrimoTR As Integer = Html.IndexOf("</tr>")
                    TitoliDaRipetere = Left(Html, indiceInizioPrimoTR + 5)
                End If


                Dim htmldaConsiderare As String = Html
                Dim nuovoHtml As String = ""
                Dim indiceTRiniziale As Integer = 0
                Dim indiceTRfinale As Integer = 0
                Dim contatoreRighe As Integer = 0
                Dim stringaAdd As String = ""
                While indiceTRiniziale <> -1
                    indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                    If indiceTRiniziale <> -1 Then
                        contatoreRighe += 1
                        htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                        indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                        If indiceTRfinale <> -1 Then
                            stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                        End If
                    End If
                    If contatoreRighe = righe Then
                        nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(Html, Html.IndexOf("<tr ") - 1)
                        contatoreRighe = 0
                    Else
                        nuovoHtml &= stringaAdd
                    End If
                End While
                Html = Left(Html, Html.IndexOf("<tr ") - 1) & nuovoHtml
            End If


            If Not IsNothing(DataGrid2) Then
                Dim html2 As String = ""

                stringWriter = New System.IO.StringWriter
                sourcecode = New HtmlTextWriter(stringWriter)
                DataGrid2.RenderControl(sourcecode)
                sourcecode.Flush()
                html2 = html2 & stringWriter.ToString
                'ELIMINAZIONE EVENTUALI HYPERLINK
                html2 = par.EliminaLink(html2)
                If contaRighe = True And righe > 0 Then
                    Dim TitoliDaRipetere As String = ""
                    If ripetiIntestazioniSoloConContaRighe = True Then
                        Dim indiceInizioPrimoTR As Integer = html2.IndexOf("</tr>")
                        TitoliDaRipetere = Left(html2, indiceInizioPrimoTR + 5)
                    End If


                    Dim htmldaConsiderare As String = html2
                    Dim nuovoHtml As String = ""
                    Dim indiceTRiniziale As Integer = 0
                    Dim indiceTRfinale As Integer = 0
                    Dim contatoreRighe As Integer = 0
                    Dim stringaAdd As String = ""
                    While indiceTRiniziale <> -1
                        indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                        If indiceTRiniziale <> -1 Then
                            contatoreRighe += 1
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                            indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                            If indiceTRfinale <> -1 Then
                                stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                                htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                            End If
                        End If
                        If contatoreRighe = righe Then
                            nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(html2, html2.IndexOf("<tr ") - 1)
                            contatoreRighe = 0
                        Else
                            nuovoHtml &= stringaAdd
                        End If
                    End While
                    html2 = Left(html2, html2.IndexOf("<tr ") - 1) & nuovoHtml
                End If
                Html = Html & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & html2
            End If


            If Not IsNothing(DataGrid3) Then
                Dim html3 As String = ""

                stringWriter = New System.IO.StringWriter
                sourcecode = New HtmlTextWriter(stringWriter)
                DataGrid3.RenderControl(sourcecode)
                sourcecode.Flush()
                html3 = html3 & stringWriter.ToString
                'ELIMINAZIONE EVENTUALI HYPERLINK
                html3 = par.EliminaLink(html3)
                If contaRighe = True And righe > 0 Then
                    Dim TitoliDaRipetere As String = ""
                    If ripetiIntestazioniSoloConContaRighe = True Then
                        Dim indiceInizioPrimoTR As Integer = html3.IndexOf("</tr>")
                        TitoliDaRipetere = Left(html3, indiceInizioPrimoTR + 5)
                    End If


                    Dim htmldaConsiderare As String = html3
                    Dim nuovoHtml As String = ""
                    Dim indiceTRiniziale As Integer = 0
                    Dim indiceTRfinale As Integer = 0
                    Dim contatoreRighe As Integer = 0
                    Dim stringaAdd As String = ""
                    While indiceTRiniziale <> -1
                        indiceTRiniziale = htmldaConsiderare.IndexOf("<tr ")
                        If indiceTRiniziale <> -1 Then
                            contatoreRighe += 1
                            htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRiniziale)
                            indiceTRfinale = htmldaConsiderare.IndexOf("</tr>") + 5
                            If indiceTRfinale <> -1 Then
                                stringaAdd = Left(htmldaConsiderare, indiceTRfinale)
                                htmldaConsiderare = Right(htmldaConsiderare, Len(htmldaConsiderare) - indiceTRfinale)
                            End If
                        End If
                        If contatoreRighe = righe Then
                            nuovoHtml &= stringaAdd & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & TitoliDaRipetere & Left(html3, html3.IndexOf("<tr ") - 1)
                            contatoreRighe = 0
                        Else
                            nuovoHtml &= stringaAdd
                        End If
                    End While
                    html3 = Left(html3, html3.IndexOf("<tr ") - 1) & nuovoHtml
                End If
                Html = Html & "</table><p style='page-break-after: always'>&nbsp;</p><table>" & html3
            End If





            Dim url As String = Server.MapPath("~\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = System.Web.HttpContext.Current.Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PageWidth = larghezzaPagina
            If orientamentoLandscape = True Then
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            Else
                pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            End If
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 65
            pdfConverter1.PdfHeaderOptions.HeaderText = UCase(titolo)
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 8
            pdfConverter1.PdfHeaderOptions.HeaderTextAlign = HorizontalTextAlign.Left
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold


            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = sottotitolo
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 7
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")


            'pdfConverter1.PdfHeaderOptions.HeaderImage = Drawing.Image.FromFile(Server.MapPath("~\NuoveImm\") & "rett2.png")


            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            pdfConverter1.PdfFooterOptions.FooterText = "Report Situazione Incassi, stampato da " & Session.Item("NOME_OPERATORE") & " il " & Format(Now, "dd/MM/yyyy") & " alle " & Format(Now, "HH:mm")
            pdfConverter1.PdfFooterOptions.FooterTextFontName = "Arial"
            pdfConverter1.PdfFooterOptions.FooterTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfFooterOptions.FooterTextFontSize = 8
            'pdfConverter1.PdfFooterOptions.FooterText = UCase(footer)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            pdfConverter1.PdfFooterOptions.FooterHeight = 30
            pdfConverter1.PdfFooterOptions.DrawFooterLine = True
            If mostraNumeriPagina = True Then
                pdfConverter1.PdfFooterOptions.PageNumberText = "Pag."
                pdfConverter1.PdfFooterOptions.PageNumberTextFontName = "Arial"
                pdfConverter1.PdfFooterOptions.PageNumberTextFontSize = 8
                pdfConverter1.PdfFooterOptions.ShowPageNumber = True
                pdfConverter1.PdfFooterOptions.PageNumberTextColor = Drawing.ColorTranslator.FromHtml("#507CD1")
            Else
                pdfConverter1.PdfFooterOptions.PageNumberText = ""
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            End If

            Dim nomefile As String = nomeStampa & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile, Server.MapPath("~\NuoveImm\"))

            Return nomefile
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Protected Sub export1_Click(sender As Object, e As System.EventArgs) Handles exportIncassi.Click
        Dim nomefile1 As String = ""
        'Dim xls As New ExcelSiSol
        If DataGridIncassi.Visible = True Then
            'nomefile1 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassi", "ExportIncassi", DataGridIncassi, True, , )
            'nomefile1 = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridIncassi, "ExportIncassi", , False, , False)
            caricaDati(True)
        End If

    End Sub

    Protected Sub export2_Click(sender As Object, e As System.EventArgs) Handles exportnonAttrib.Click
        Dim nomefile1 As String = ""
        'Dim xls As New ExcelSiSol
        If DataGridIncassiNonAttribuibili.Visible = True Then
            '            nomefile1 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassiNonAttribuibili", "ExportIncassiNonAttribuibili", DataGridIncassiNonAttribuibili, True, , )
            caricaIncassiNonAttribuibili(Request.QueryString("TipologiaIncasso"), True)
        End If


    End Sub

    Protected Sub export3_Click(sender As Object, e As System.EventArgs) Handles exportExtra.Click
        Dim nomefile1 As String = ""
        'Dim xls As New ExcelSiSol
        If DataGridIncassiExtraMAV.Visible = True Then
            'nomefile1 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassiExtraMAV", "ExportIncassiExtraMAV", DataGridIncassiExtraMAV, True, , )
            caricaDatiExtraMAV(Request.QueryString("TipologiaIncasso"), True)
        End If


    End Sub

    Protected Sub DataGridIncassi_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIncassi.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGridIncassi.CurrentPageIndex = e.NewPageIndex
            caricaDati()
        End If

    End Sub

    Protected Sub DataGridIncassiExtraMAV_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIncassiExtraMAV.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGridIncassiExtraMAV.CurrentPageIndex = e.NewPageIndex
            caricaDatiExtraMAV(Request.QueryString("TipologiaIncasso"))
        End If

    End Sub

    Protected Sub DataGridIncassiNonAttribuibili_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridIncassiNonAttribuibili.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGridIncassiNonAttribuibili.CurrentPageIndex = e.NewPageIndex
            caricaIncassiNonAttribuibili(Request.QueryString("TipologiaIncasso"))
        End If

    End Sub

    Private Sub EstraiDati(ByVal strQuery As String, ByVal idTipoReport As Integer, Optional ByVal lista As Boolean = False)
        Try
            Dim sComando As String = strQuery
            ApriConnessione()

            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_REPORT.NEXTVAL FROM DUAL"
            Dim idReport As Integer = par.cmd.ExecuteScalar

            If lista Then
                '258/2017
                Dim listaCodiciRU As String = Session.Item("listaRU")
                Session.Remove("listaRU")
                Dim stringaSplit As String()
                Dim sost As String = ""

                If Not IsNothing(listaCodiciRU) Then

                    par.cmd.CommandText = "CREATE TABLE RPT_CONTAB_MOROSITA_TMP_" & idReport & " ( ID_CONTRATTO   NUMBER, COD_CONTRATTO  VARCHAR2(100 BYTE) )"
                    par.cmd.ExecuteNonQuery()

                    sost = "RPT_CONTAB_MOROSITA_TMP_" & idReport

                    stringaSplit = listaCodiciRU.Split("|")
                    For Each item As String In stringaSplit
                        par.cmd.CommandText = "INSERT INTO RPT_CONTAB_MOROSITA_TMP_" & idReport & " ( COD_CONTRATTO, ID_CONTRATTO) VALUES " _
                                            & "('" & item & "',(SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO = '" & item & "'))"
                        par.cmd.ExecuteNonQuery()
                    Next
                End If
                Dim temp As String = strQuery
                strQuery = temp.Replace("$RPT_CONTAB_MOROSITA_TMP$", sost)
            End If

            If Len(strQuery) < 4000 Then

                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE, Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'" & strQuery.ToString.Replace("'", "''") & "', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,NULL)"
            Else
                par.cmd.CommandText = " INSERT INTO SISCOM_MI.REPORT ( " _
                    & " ID, ID_TIPOLOGIA_REPORT, ID_OPERATORE,  " _
                    & " INIZIO, FINE, ESITO,  " _
                    & " Q1,  " _
                    & " PARAMETRI, PARZIALE, TOTALE,  " _
                    & " ERRORE, NOMEFILE,Q4)  " _
                    & " VALUES (" & idReport & ", " _
                    & idTipoReport & ", " _
                    & Session.Item("id_operatore") & ", " _
                    & Format(Now, "yyyyMMddHHmmss") & " , " _
                    & "NULL, " _
                    & "0, " _
                    & "'', " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL, " _
                    & "NULL,:TEXT_DATA)"


                Dim paramData As New Oracle.DataAccess.Client.OracleParameter
                With paramData
                    .Direction = Data.ParameterDirection.Input
                    .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Clob
                    .ParameterName = "TEXT_DATA"
                    .Value = strQuery
                End With

                par.cmd.Parameters.Add(paramData)
                paramData = Nothing

            End If
            par.cmd.ExecuteNonQuery()

            chiudiConnessione()
            Dim p As New System.Diagnostics.Process
            Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
            Dim dicParaConnection As New Generic.Dictionary(Of String, String)
            Dim sParametri As String = ""
            For i As Integer = 0 To elParameter.Length - 1
                Dim s As String() = elParameter(i).Split("=")
                If s.Length > 1 Then
                    dicParaConnection.Add(s(0), s(1))
                End If
            Next
            sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idReport
            p.StartInfo.UseShellExecute = False
            p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/Report.exe")
            p.StartInfo.Arguments = sParametri
            p.Start()
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza: Estrazioni_RU -  EstraiDati - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

End Class
