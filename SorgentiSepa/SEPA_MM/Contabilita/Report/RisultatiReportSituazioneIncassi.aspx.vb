Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip

Partial Class Contabilita_Report_RisultatiReportSituazioneIncassi
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

    Private Sub caricaDati()

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

        'If Not IsNothing(listaBollettazione) Then
        '    condizioneTipologiaBollettazione &= " AND("
        '    Dim a As Integer = 0
        '    For Each Items As String In listaBollettazione
        '        If a <> 0 Then
        '            condizioneTipologiaBollettazione &= " OR "
        '        End If
        '        Select Case Items
        '            Case 3
        '                condizioneTipologiaBollettazione &= "(ID_TIPO_BOLLETTA = 3 OR (ID_TIPO_BOLLETTA<>3 AND ID_STATO_BOLLETTA=2)) "
        '            Case 4
        '                condizioneTipologiaBollettazione &= "(ID_TIPO_BOLLETTA = 4 OR (ID_TIPO_BOLLETTA<>4 AND ID_STATO_BOLLETTA=2)) "
        '            Case 5
        '                condizioneTipologiaBollettazione &= "(ID_TIPO_BOLLETTA = 5 OR (ID_TIPO_BOLLETTA<>5 AND ID_STATO_BOLLETTA=3)) "
        '            Case Else
        '                condizioneTipologiaBollettazione &= "(ID_TIPO_BOLLETTA = " & Items & " AND ID_STATO_BOLLETTA=1) "
        '        End Select
        '        a += 1
        '    Next
        '    condizioneTipologiaBollettazione &= " )"
        'End If
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
        Dim DataAssDal As String = ""
        Dim DataAssAl As String = ""
        If Not IsNothing(Request.QueryString("NumeroAssegno")) Then
            numeroAssegno = Request.QueryString("NumeroAssegno")
        End If
        If Not IsNothing(Request.QueryString("DataAssegnDa")) Then
            DataAssDal = Request.QueryString("DataAssegnDa")
        End If
        If Not IsNothing(Request.QueryString("DataAssegnAl")) Then
            DataAssAl = Request.QueryString("DataAssegnAl")
        End If
        Dim condizioneNumeroAssegno As String = ""
        If numeroAssegno <> "" Then
            'condizioneNumeroAssegno = " AND ID_VOCE_BOLLETTA IN " _
            '    & " (SELECT ID_VOCE_BOLLETTA FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZ_DETT " _
            '    & " WHERE ID_EVENTO_PRINCIPALE IN (SELECT ID FROM SISCOM_MI.EVENTI_PAGAMENTI_PARZIALI " _
            '    & " WHERE ID_INCASSO_EXTRAMAV IN (SELECT ID FROM SISCOM_MI.INCASSI_EXTRAMAV " _
            '    & " WHERE NUMERO_ASSEGNO='" & numeroAssegno & "'))) "
            condizioneNumeroAssegno = " AND ID_INCASSO_EXTRAMAV IN (SELECT ID FROM SISCOM_MI.INCASSI_EXTRAMAV " _
                                    & " WHERE NUMERO_ASSEGNO='" & numeroAssegno & "'"
            If Not String.IsNullOrEmpty(DataAssDal) Or Not String.IsNullOrEmpty(DataAssAl) Then
                If DataAssDal <> "" Then
                    condizioneNumeroAssegno += " AND DATA_ASSEGNO >= '" & DataAssDal & "'"
                End If
                If DataAssAl <> "" Then
                    condizioneNumeroAssegno += " AND DATA_ASSEGNO <= '" & DataAssAl & "'"
                End If
                condizioneNumeroAssegno += ") "
                
            Else
                condizioneNumeroAssegno += ") "

            End If
        ElseIf Not String.IsNullOrEmpty(DataAssDal) Or Not String.IsNullOrEmpty(DataAssAl) Then

            Dim primo As Boolean = True

            condizioneNumeroAssegno = " AND ID_INCASSO_EXTRAMAV IN (SELECT ID FROM SISCOM_MI.INCASSI_EXTRAMAV " _
                                    & " WHERE "


            If DataAssDal <> "" Then
                If primo = False Then
                    condizioneNumeroAssegno += " AND "
                Else
                    primo = False
                End If
                condizioneNumeroAssegno += "  DATA_ASSEGNO >= '" & DataAssDal & "'"
            End If
            If DataAssAl <> "" Then
                If primo = False Then
                    condizioneNumeroAssegno += " AND "
                Else
                    primo = False
                End If
                condizioneNumeroAssegno += "  DATA_ASSEGNO <= '" & DataAssAl & "'"
            End If
            condizioneNumeroAssegno += ") "

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

            Dim dettaglioSi As Boolean
            If Request.QueryString("Dettaglio") = "1" Then
                dettaglioSi = True
            Else
                dettaglioSi = False
            End If

            If macroCategoriaSi Then
                selectMacroCategoria = " INITCAP(substr(nvl(MACROCATEGORIA,''),1,17)) AS MACROCATEGORIA, "
                'selectMacroCategoria = " INITCAP(nvl(MACROCATEGORIA,'')) AS MACROCATEGORIA, "
                groupByMacroCategoria = ",nvl(MACROCATEGORIA,'') "
            Else
                selectMacroCategoria = " '' AS MACROCATEGORIA, "
                groupByMacroCategoria = ""
            End If

            If categoriaSi Then
                selectCategoria = " INITCAP(substr(nvl(CATEGORIA,''),1,17)) AS CATEGORIA, "
                'selectCategoria = " INITCAP(nvl(CATEGORIA,'')) AS CATEGORIA, "
                groupByCategoria = ",nvl(CATEGORIA,'') "
            Else
                selectCategoria = " '' AS CATEGORIA, "
                groupByCategoria = ""
            End If

            If vociSi Then
                selectVoci = " INITCAP(substr(nvl(VOCE,''),1,17)) AS VOCE, "
                'selectVoci = " INITCAP(nvl(VOCE,'')) AS VOCE, "
                groupByVoci = ",nvl(VOCE,'') "
            Else
                selectVoci = " '' AS VOCE, "
                groupByVoci = ""
            End If

            If competenzaSi Then
                selectCompetenza = " INITCAP(COMPETENZA) AS COMPETENZA, "
                groupByCompetenza = " ,COMPETENZA "
            Else
                selectCompetenza = " '' AS COMPETENZA, "
                groupByCompetenza = ""
            End If

            If TipologiaUISi Then
                selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI, INITCAP(NVL(TIPO_UI,'')) AS TIPO_UI, "
                groupByTipologiaUI = ",USI_ABITATIVI,TIPO_UI "
            Else
                selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI,'' AS TIPO_UI, "
                groupByTipologiaUI = ", USI_ABITATIVI "
            End If

            Dim dettaglio As String = ""
            Dim dettaglioGroup As String = ""
            If dettaglioSi Then
                dettaglio = " ANNO, " _
                    & " INITCAP(BIMESTRE) AS BIMESTRE, "
                dettaglioGroup = ",ANNO, BIMESTRE "
            Else
                dettaglio = " '' AS ANNO, '' AS BIMESTRE, "
                dettaglioGroup = " "

            End If

            Try


                ApriConnessione()
                par.cmd.CommandText = " SELECT " _
                    & " INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE,INITCAP(CAPITOLO) AS CAPITOLO, " _
                    & " COMPETENZA_ACC, " _
                    & dettaglio _
                    & selectCompetenza _
                    & selectMacroCategoria _
                    & selectCategoria _
                    & selectVoci _
                    & selectTipologiaUI _
                    & " TRIM(TO_CHAR(SUM(NVL(IMPORTO_PAGATO,0)),'999G999G990D99')) AS IMPORTO " _
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
                    & " AND FL_NO_REPORT=0 " _
                    & " GROUP BY ROLLUP (INITCAP(BOLLETTAZIONE),INITCAP(CAPITOLO)," _
                    & " COMPETENZA_ACC " _
                    & dettaglioGroup _
                    & groupByCompetenza _
                    & groupByMacroCategoria _
                    & groupByCategoria _
                    & groupByVoci _
                    & groupByTipologiaUI _
                    & ") "


                Dim capitolisi As Boolean = True
                Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dTable As New Data.DataTable
                dAdapter.Fill(dTable)
                Dim valorePrecedente As Decimal = 0

                For Each Items As Data.DataRow In dTable.Rows
                    If Not IsDBNull(Items.Item(11)) Then
                        valorePrecedente = Items.Item(11)
                    Else
                        Items.Item(11) = valorePrecedente
                    End If
                Next
                Dim dataTableRibaltata As New Data.DataTable
                dataTableRibaltata.Columns.Clear()
                For Each colonna As Data.DataColumn In dTable.Columns
                    dataTableRibaltata.Columns.Add(colonna.ColumnName)
                Next
                Dim Nrighe As Integer = dTable.Rows.Count
                Dim riga As Data.DataRow
                Dim rigaPrec As Data.DataRow
                Dim rigaConfronto As Data.DataRow
                Dim bollettazionePrecedente As String = ""
                Dim annoCompetenza As String = ""
                Dim annoPrecedente As String = ""
                Dim capitoloPrecedente As String = ""
                Dim bimestrePrecedente As String = ""
                Dim competenzaPrecedente As String = ""
                Dim macrocategoriaPrecedente As String = ""
                Dim categoriaPrecedente As String = ""
                Dim vocePrecedente As String = ""
                Dim tipoUIPrecedente As String = ""
                rigaPrec = dataTableRibaltata.NewRow
                For i As Integer = 0 To Nrighe - 1
                    riga = dataTableRibaltata.NewRow
                    rigaConfronto = dataTableRibaltata.NewRow
                    If i = 0 Then
                        Dim indice As Integer = 0
                        riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>Totale</font>"
                        indice += 1
                        riga.Item("CAPITOLO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("COMPETENZA_ACC") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("ANNO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("BIMESTRE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("COMPETENZA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("MACROCATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("CATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("VOCE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("USI_ABITATIVI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("TIPO_UI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        rigaPrec = riga

                    Else
                        Dim indice2 As Integer = 0

                        rigaConfronto.Item("BOLLETTAZIONE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CAPITOLO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("COMPETENZA_ACC") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("ANNO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("BIMESTRE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("COMPETENZA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("MACROCATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("VOCE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("USI_ABITATIVI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("TIPO_UI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("IMPORTO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        Dim indice As Integer = 0
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("BOLLETTAZIONE") = ""
                            Else
                                riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                capitoloPrecedente = ""
                                annoCompetenza = ""
                                annoPrecedente = ""
                                bimestrePrecedente = ""
                                competenzaPrecedente = ""
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("BOLLETTAZIONE") = ""
                        End If
                        indice += 1
                        If capitolisi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CAPITOLO") = ""
                                Else
                                    riga.Item("CAPITOLO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                    capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoCompetenza = ""
                                    annoPrecedente = ""
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CAPITOLO") = ""
                            End If
                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("COMPETENZA_ACC") = ""
                            Else
                                riga.Item("COMPETENZA_ACC") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                annoPrecedente = ""
                                bimestrePrecedente = ""
                                competenzaPrecedente = ""
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("COMPETENZA_ACC") = ""
                        End If
                        indice += 1
                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If annoPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("ANNO") = ""
                                Else
                                    riga.Item("ANNO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoPrecedente = riga.Item("ANNO")
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("ANNO") = ""
                            End If
                        End If
                        indice += 1

                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If bimestrePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("BIMESTRE") = ""
                                Else
                                    riga.Item("BIMESTRE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    bimestrePrecedente = riga.Item("BIMESTRE")
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("BIMESTRE") = ""
                            End If
                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If competenzaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("COMPETENZA") = ""
                            Else
                                riga.Item("COMPETENZA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                competenzaPrecedente = riga.Item("COMPETENZA")
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("COMPETENZA") = ""
                        End If
                        indice += 1
                        If macroCategoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If macrocategoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("MACROCATEGORIA") = ""
                                Else
                                    riga.Item("MACROCATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    macrocategoriaPrecedente = riga.Item("MACROCATEGORIA")
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("MACROCATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If categoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If categoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CATEGORIA") = ""
                                Else
                                    riga.Item("CATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    categoriaPrecedente = riga.Item("CATEGORIA")
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If vociSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If vocePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("VOCE") = ""
                                Else
                                    riga.Item("VOCE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    vocePrecedente = riga.Item("VOCE")
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("VOCE") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("USI_ABITATIVI") = ""
                            Else
                                If IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice + 1)) Then
                                    riga.Item("USI_ABITATIVI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = ""
                                Else
                                    riga.Item("USI_ABITATIVI") = ""
                                End If
                                'tipoUIPrecedente = riga.Item("USI_ABITATIVI")
                            End If
                        Else
                            riga.Item("USI_ABITATIVI") = ""
                        End If
                        indice += 1
                        If TipologiaUISi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("TIPO_UI") = ""
                                Else
                                    riga.Item("TIPO_UI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = riga.Item("TIPO_UI")
                                End If
                            Else
                                riga.Item("TIPO_UI") = ""
                            End If
                            indice += 1
                        Else
                            indice += 1
                        End If
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Or par.IfNull(riga.Item("COMPETENZA_ACC"), "") <> "" Then
                                riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("IMPORTO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("IMPORTO") = ""
                        End If

                    End If
                    'rigaPrec.Item("ACCERTATO") = par.IfNull(rigaConfronto.Item("ACCERTATO"), "")
                    If par.IfNull(rigaPrec.Item("TIPO_UI"), "") = par.IfNull(rigaConfronto.Item("TIPO_UI"), "") _
                        And par.IfNull(rigaPrec.Item("IMPORTO"), "") = par.IfNull(rigaConfronto.Item("IMPORTO"), "") _
                        And par.IfNull(riga.Item("competenza_acc"), "") = "" _
                        And par.IfNull(riga.Item("anno"), "") = "" _
                        And par.IfNull(riga.Item("capitolo"), "") = "" _
                        And par.IfNull(riga.Item("bimestre"), "") = "" _
                        And par.IfNull(riga.Item("competenza"), "") = "" _
                        And par.IfNull(riga.Item("macrocategoria"), "") = "" _
                        And par.IfNull(riga.Item("bollettazione"), "") = "" _
                        And par.IfNull(riga.Item("categoria"), "") = "" _
                        And par.IfNull(riga.Item("voce"), "") = "" _
                        And par.IfNull(riga.Item("USI_ABITATIVI"), "") = "" Then
                        'RIGHE UGUALI DA ELIMINARE

                        'If par.IfNull(rigaPrec.Item("TIPO_UI"), "") <> "" Then
                        'dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(13) = par.IfNull(rigaConfronto.Item("ACCERTATO"), "")
                        'End If

                        If par.IfNull(riga.Item("TIPO_UI"), "") = par.IfNull(dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(9), "") Then
                            riga.Item("TIPO_UI") = ""
                        End If

                    Else
                        'If (Nrighe - 1 - i) <> 1 Then 'And (Nrighe - 1 - i) <> 2
                        dataTableRibaltata.Rows.Add(riga)
                        rigaPrec = rigaConfronto
                        'End If
                    End If
                Next

                If dataTableRibaltata.Rows.Count > 0 Then
                    Dim indiceVisibile As Integer = 1
                    If Not capitolisi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    'indiceVisibile = 4
                    indiceVisibile = 3
                    If Not dettaglioSi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not dettaglioSi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not competenzaSi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not macroCategoriaSi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not categoriaSi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not vociSi Then
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not TipologiaUISi Then
                        'DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                        indiceVisibile += 1
                        DataGridIncassi.Columns.Item(indiceVisibile).Visible = False
                    End If
                    DataGridIncassi.DataSource = dataTableRibaltata
                    DataGridIncassi.DataBind()
                    ImageButtonExcel.Visible = True
                    ImageButtonStampa.Visible = True
                    'ImageButtonStampaAccerta.Visible = True
                    LabelTitolo.Text = "Situazione Incassi"
                    If DataContabileAl <> "" And DataContabileDal <> "" Then
                        LabelTitolo.Text = "Situazione Incassi dal " & par.FormattaData(DataContabileDal) & " al " & par.FormattaData(DataContabileAl)
                    ElseIf DataContabileAl <> "" And DataContabileDal = "" Then
                        LabelTitolo.Text = "Situazione Incassi al " & par.FormattaData(DataContabileAl)
                    ElseIf DataContabileAl = "" And DataContabileDal <> "" Then
                        LabelTitolo.Text = "Situazione Incassi dal " & par.FormattaData(DataContabileDal)
                    ElseIf DataContabileAl = "" And DataContabileDal = "" Then
                        LabelTitolo.Text = "Situazione Incassi"
                    End If
                Else
                    LabelErrore.Text = "La ricerca non ha prodotto nessun risultato! Modificare i parametri di ricerca e riprovare"
                    ImageButtonExcel.Visible = False
                    ImageButtonStampa.Visible = False
                    'ImageButtonStampaAccerta.Visible = False
                    LabelTitolo.Text = "Situazione Incassi"
                End If
                chiudiConnessione()

                caricaIncassiNonAttribuibili(tipologiaIncasso)
                'caricaDatiExtraMAV(tipologiaIncasso)
            Catch ex As Exception
                chiudiConnessione()
                Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
            End Try
    End Sub

    Private Sub caricaDatiExtraMAV(tipoIncasso As String)
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
                DataAggiornamentoDAL = Request.QueryString("DataAggiornamentoDal")
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

            Dim dettaglioSi As Boolean
            If Request.QueryString("Dettaglio") = "1" Then
                dettaglioSi = True
            Else
                dettaglioSi = False
            End If

            If macroCategoriaSi Then
                selectMacroCategoria = " INITCAP(substr(nvl(MACROCATEGORIA,''),1,17)) AS MACROCATEGORIA, "
                'selectMacroCategoria = " INITCAP(nvl(MACROCATEGORIA,'')) AS MACROCATEGORIA, "
                groupByMacroCategoria = ",nvl(MACROCATEGORIA,'') "
            Else
                selectMacroCategoria = " '' AS MACROCATEGORIA, "
                groupByMacroCategoria = ""
            End If

            If categoriaSi Then
                selectCategoria = " INITCAP(substr(nvl(CATEGORIA,''),1,17)) AS CATEGORIA, "
                'selectCategoria = " INITCAP(nvl(CATEGORIA,'')) AS CATEGORIA, "
                groupByCategoria = ",nvl(CATEGORIA,'') "
            Else
                selectCategoria = " '' AS CATEGORIA, "
                groupByCategoria = ""
            End If

            If vociSi Then
                selectVoci = " INITCAP(substr(nvl(VOCE,''),1,17)) AS VOCE, "
                'selectVoci = " INITCAP(nvl(VOCE,'')) AS VOCE, "
                groupByVoci = ",nvl(VOCE,'') "
            Else
                selectVoci = " '' AS VOCE, "
                groupByVoci = ""
            End If

            If competenzaSi Then
                selectCompetenza = " INITCAP(COMPETENZA) AS COMPETENZA, "
                groupByCompetenza = " ,COMPETENZA "
            Else
                selectCompetenza = " '' AS COMPETENZA, "
                groupByCompetenza = ""
            End If

            If TipologiaUISi Then
                selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI, INITCAP(NVL(TIPO_UI,'')) AS TIPO_UI, "
                groupByTipologiaUI = ",USI_ABITATIVI,TIPO_UI "
            Else
                selectTipologiaUI = " INITCAP(USI_ABITATIVI) AS USI_ABITATIVI,'' AS TIPO_UI, "
                groupByTipologiaUI = ", USI_ABITATIVI "
            End If

            Dim dettaglio As String = ""
            Dim dettaglioGroup As String = ""
            If dettaglioSi Then
                dettaglio = " ANNO, " _
                    & " INITCAP(BIMESTRE) AS BIMESTRE, "
                dettaglioGroup = ",ANNO, BIMESTRE "
            Else
                dettaglio = " '' AS ANNO, '' AS BIMESTRE, "
                dettaglioGroup = " "

            End If

            Try


                ApriConnessione()
                par.cmd.CommandText = " SELECT " _
                    & " INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE,INITCAP(CAPITOLO) AS CAPITOLO, " _
                    & " COMPETENZA_ACC, " _
                    & dettaglio _
                    & selectCompetenza _
                    & selectMacroCategoria _
                    & selectCategoria _
                    & selectVoci _
                    & selectTipologiaUI _
                    & " TRIM(TO_CHAR(SUM(NVL(IMPORTO_PAGATO,0)),'999G999G990D99')) AS IMPORTO " _
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
                    & " AND FL_NO_REPORT=0 " _
                    & " GROUP BY ROLLUP (INITCAP(BOLLETTAZIONE),INITCAP(CAPITOLO)," _
                    & " COMPETENZA_ACC " _
                    & dettaglioGroup _
                    & groupByCompetenza _
                    & groupByMacroCategoria _
                    & groupByCategoria _
                    & groupByVoci _
                    & groupByTipologiaUI _
                    & ") "


                Dim capitolisi As Boolean = True
                Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dTable As New Data.DataTable
                dAdapter.Fill(dTable)
                Dim valorePrecedente As Decimal = 0
                For Each Items As Data.DataRow In dTable.Rows
                    If Not IsDBNull(Items.Item(11)) Then
                        valorePrecedente = Items.Item(11)
                    Else
                        Items.Item(11) = valorePrecedente
                    End If
                Next
                Dim dataTableRibaltata As New Data.DataTable
                dataTableRibaltata.Columns.Clear()
                For Each colonna As Data.DataColumn In dTable.Columns
                    dataTableRibaltata.Columns.Add(colonna.ColumnName)
                Next
                Dim Nrighe As Integer = dTable.Rows.Count
                Dim riga As Data.DataRow
                Dim rigaPrec As Data.DataRow
                Dim rigaConfronto As Data.DataRow
                Dim bollettazionePrecedente As String = ""
                Dim annoCompetenza As String = ""
                Dim annoPrecedente As String = ""
                Dim capitoloPrecedente As String = ""
                Dim bimestrePrecedente As String = ""
                Dim competenzaPrecedente As String = ""
                Dim macrocategoriaPrecedente As String = ""
                Dim categoriaPrecedente As String = ""
                Dim vocePrecedente As String = ""
                Dim tipoUIPrecedente As String = ""
                rigaPrec = dataTableRibaltata.NewRow
                For i As Integer = 0 To Nrighe - 1
                    riga = dataTableRibaltata.NewRow
                    rigaConfronto = dataTableRibaltata.NewRow
                    If i = 0 Then
                        Dim indice As Integer = 0
                        riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>Totale</font>"
                        indice += 1
                        riga.Item("CAPITOLO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("COMPETENZA_ACC") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("ANNO") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("BIMESTRE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("COMPETENZA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("MACROCATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("CATEGORIA") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("VOCE") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("USI_ABITATIVI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("TIPO_UI") = dTable.Rows(Nrighe - 1 - i).Item(indice)
                        indice += 1
                        riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & dTable.Rows(Nrighe - 1 - i).Item(indice) & "</font>"
                        rigaPrec = riga

                    Else
                        Dim indice2 As Integer = 0

                        rigaConfronto.Item("BOLLETTAZIONE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CAPITOLO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("COMPETENZA_ACC") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("ANNO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("BIMESTRE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("COMPETENZA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("MACROCATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("CATEGORIA") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("VOCE") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("USI_ABITATIVI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("TIPO_UI") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")
                        indice2 += 1
                        rigaConfronto.Item("IMPORTO") = par.IfNull(dTable.Rows(Nrighe - 1 - i).Item(indice2), "")

                        Dim indice As Integer = 0
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("BOLLETTAZIONE") = ""
                            Else
                                riga.Item("BOLLETTAZIONE") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                bollettazionePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                capitoloPrecedente = ""
                                annoPrecedente = ""
                                bimestrePrecedente = ""
                                competenzaPrecedente = ""
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("BOLLETTAZIONE") = ""
                        End If
                        indice += 1

                        If capitolisi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CAPITOLO") = ""
                                Else
                                    riga.Item("CAPITOLO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                    capitoloPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoPrecedente = ""
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CAPITOLO") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("COMPETENZA_ACC") = ""
                            Else
                                riga.Item("COMPETENZA_ACC") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                                annoCompetenza = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                annoPrecedente = ""
                                bimestrePrecedente = ""
                                competenzaPrecedente = ""
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("COMPETENZA_ACC") = ""
                        End If
                        indice += 1

                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If annoPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("ANNO") = ""
                                Else
                                    riga.Item("ANNO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    annoPrecedente = riga.Item("ANNO")
                                    bimestrePrecedente = ""
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("ANNO") = ""
                            End If
                        End If
                        indice += 1

                        If dettaglioSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If bimestrePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("BIMESTRE") = ""
                                Else
                                    riga.Item("BIMESTRE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    bimestrePrecedente = riga.Item("BIMESTRE")
                                    competenzaPrecedente = ""
                                    macrocategoriaPrecedente = ""
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("BIMESTRE") = ""
                            End If
                        End If

                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If competenzaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("COMPETENZA") = ""
                            Else
                                riga.Item("COMPETENZA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                competenzaPrecedente = riga.Item("COMPETENZA")
                                macrocategoriaPrecedente = ""
                                categoriaPrecedente = ""
                                vocePrecedente = ""
                                tipoUIPrecedente = ""
                            End If
                        Else
                            riga.Item("COMPETENZA") = ""
                        End If
                        indice += 1
                        If macroCategoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If macrocategoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("MACROCATEGORIA") = ""
                                Else
                                    riga.Item("MACROCATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    macrocategoriaPrecedente = riga.Item("MACROCATEGORIA")
                                    categoriaPrecedente = ""
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("MACROCATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If categoriaSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If categoriaPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("CATEGORIA") = ""
                                Else
                                    riga.Item("CATEGORIA") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    categoriaPrecedente = riga.Item("CATEGORIA")
                                    vocePrecedente = ""
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("CATEGORIA") = ""
                            End If

                        End If
                        indice += 1
                        If vociSi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If vocePrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("VOCE") = ""
                                Else
                                    riga.Item("VOCE") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    vocePrecedente = riga.Item("VOCE")
                                    tipoUIPrecedente = ""
                                End If
                            Else
                                riga.Item("VOCE") = ""
                            End If

                        End If
                        indice += 1
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                            If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                riga.Item("USI_ABITATIVI") = ""
                            Else
                                If IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice + 1)) Then
                                    riga.Item("USI_ABITATIVI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = ""
                                Else
                                    riga.Item("USI_ABITATIVI") = ""
                                End If
                                'tipoUIPrecedente = riga.Item("USI_ABITATIVI")
                            End If
                        Else
                            riga.Item("USI_ABITATIVI") = ""
                        End If
                        indice += 1
                        If TipologiaUISi Then
                            If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                If tipoUIPrecedente = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then
                                    riga.Item("TIPO_UI") = ""
                                Else
                                    riga.Item("TIPO_UI") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                                    tipoUIPrecedente = riga.Item("TIPO_UI")
                                End If
                            Else
                                riga.Item("TIPO_UI") = ""
                            End If
                            indice += 1
                        Else
                            indice += 1
                        End If
                        If Not IsDBNull(dTable.Rows(Nrighe - 1 - i).Item(indice)) Then

                            If par.IfNull(riga.Item("BOLLETTAZIONE"), "") <> "" Or par.IfNull(riga.Item("CAPITOLO"), "") <> "" Or par.IfNull(riga.Item("COMPETENZA_ACC"), "") <> "" Then
                                riga.Item("IMPORTO") = "<font style='font-weight:bold;font-style:italic;'>" & CStr(dTable.Rows(Nrighe - 1 - i).Item(indice)) & "</font>"
                            Else
                                riga.Item("IMPORTO") = CStr(dTable.Rows(Nrighe - 1 - i).Item(indice))
                            End If

                        Else
                            riga.Item("IMPORTO") = ""
                        End If

                    End If
                    If par.IfNull(rigaPrec.Item("TIPO_UI"), "") = par.IfNull(rigaConfronto.Item("TIPO_UI"), "") _
                        And par.IfNull(rigaPrec.Item("IMPORTO"), "") = par.IfNull(rigaConfronto.Item("IMPORTO"), "") _
                        And par.IfNull(riga.Item("competenza_acc"), "") = "" _
                        And par.IfNull(riga.Item("anno"), "") = "" _
                        And par.IfNull(riga.Item("capitolo"), "") = "" _
                        And par.IfNull(riga.Item("bimestre"), "") = "" _
                        And par.IfNull(riga.Item("competenza"), "") = "" _
                        And par.IfNull(riga.Item("macrocategoria"), "") = "" _
                        And par.IfNull(riga.Item("bollettazione"), "") = "" _
                        And par.IfNull(riga.Item("categoria"), "") = "" _
                        And par.IfNull(riga.Item("voce"), "") = "" _
                        And par.IfNull(riga.Item("USI_ABITATIVI"), "") = "" Then
                        'RIGHE UGUALI DA ELIMINARE

                        If par.IfNull(riga.Item("TIPO_UI"), "") = par.IfNull(dataTableRibaltata.Rows.Item(dataTableRibaltata.Rows.Count - 1).Item(9), "") Then
                            riga.Item("TIPO_UI") = ""
                        End If

                    Else
                        'If (Nrighe - 1 - i) <> 1 Then 'And (Nrighe - 1 - i) <> 2
                        dataTableRibaltata.Rows.Add(riga)
                        rigaPrec = rigaConfronto
                        'End If
                    End If
                Next

                If dataTableRibaltata.Rows.Count > 0 Then
                    Dim indiceVisibile As Integer = 1
                    If Not capitolisi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    'indiceVisibile = 4
                    indiceVisibile = 3
                    If Not dettaglioSi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not dettaglioSi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not competenzaSi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not macroCategoriaSi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not categoriaSi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not vociSi Then
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    indiceVisibile += 1
                    If Not TipologiaUISi Then
                        'datagridincassiextramav.Columns.Item(indiceVisibile).Visible = False
                        indiceVisibile += 1
                        DataGridIncassiExtraMAV.Columns.Item(indiceVisibile).Visible = False
                    End If
                    DataGridIncassiExtraMAV.DataSource = dataTableRibaltata
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
                Else
                    DataGridIncassiExtraMAV.Visible = False
                    'LabelErrore.Text = "La ricerca non ha prodotto nessun risultato! Modificare i parametri di ricerca e riprovare"
                    'ImageButtonExcel.Visible = False
                    'ImageButtonStampa.Visible = False
                    'LabelTitolo.Text = "Situazione Incassi"
                End If
                chiudiConnessione()
            Catch ex As Exception
                chiudiConnessione()
                Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
            End Try
        Else
            LabelTitoloIncassiAttribuiti.Visible = False
            DataGridIncassiExtraMAV.Visible = False
        End If

    End Sub

    Private Sub caricaIncassiNonAttribuibili(tipologiaIncasso As String)
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
                Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dTable As New Data.DataTable
                dAdapter.Fill(dTable)
                chiudiConnessione()
                If dTable.Rows.Count > 0 Then
                    DataGridIncassiNonAttribuibili.DataSource = dTable
                    DataGridIncassiNonAttribuibili.DataBind()
                    DataGridIncassiNonAttribuibili.Visible = True
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
                End If
                LabelTitoloNonAttribuibili.Visible = True
            Else
                LabelTitoloNonAttribuibili.Visible = False
                DataGridIncassiNonAttribuibili.Visible = False
            End If

        Catch ex As Exception
            chiudiConnessione()
            Response.Write("<script>alert('Si è verificato un errore durante il caricamento dei dati! Ripetere la ricerca!');self.close();</script>")
        End Try
    End Sub

    Protected Sub ImageButtonStampa_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonStampa.Click
        Dim Loading As String = "<div id=""divLoading5"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        Response.Flush()
        Try
            Dim nomeFile As String = ""
            If DataGridIncassiNonAttribuibili.Items.Count > 0 And DataGridIncassiExtraMAV.Items.Count > 0 Then
                nomeFile = StampaDataGridPDF_1(DataGridIncassi, "StampaIncassi", LabelTitolo.Text, , 1400, , , True, 48, True, filtriRicerca, DataGridIncassiNonAttribuibili, DataGridIncassiExtraMAV)
            ElseIf DataGridIncassiNonAttribuibili.Items.Count > 0 And DataGridIncassiExtraMAV.Items.Count = 0 Then
                nomeFile = StampaDataGridPDF_1(DataGridIncassi, "StampaIncassi", LabelTitolo.Text, , 1400, , , True, 48, True, filtriRicerca, DataGridIncassiNonAttribuibili)
            ElseIf DataGridIncassiNonAttribuibili.Items.Count = 0 And DataGridIncassiExtraMAV.Items.Count > 0 Then
                nomeFile = StampaDataGridPDF_1(DataGridIncassi, "StampaIncassi", LabelTitolo.Text, , 1400, , , True, 48, True, filtriRicerca, , DataGridIncassiExtraMAV)
            ElseIf DataGridIncassiNonAttribuibili.Items.Count = 0 And DataGridIncassiExtraMAV.Items.Count = 0 Then
                nomeFile = StampaDataGridPDF_1(DataGridIncassi, "StampaIncassi", LabelTitolo.Text, , 1400, , , True, 48, True, filtriRicerca)
            End If
            'Dim nomeFile As String = StampaDataGridPDF_1(DataGridIncassi, "StampaIncassi", LabelTitolo.Text, , 1400, , , True, 50, True, filtriRicerca, DataGridIncassiNonAttribuibili, DataGridIncassiExtraMAV)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                Response.Write("<script>window.open('../../FileTemp/" & nomeFile & "');</script>")
                HiddenFieldPrimoPiano.Value = "1"
            Else
                Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!')</script>")
            End If
        Catch ex As Exception
            Response.Write("<script>alert('Si è verificato un errore durante la stampa. Riprovare!');</script>")
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
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 6.5
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

    Protected Sub ImageButtonExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExcel.Click
        Dim xls As New ExcelSiSol
        Dim nomefile1 As String = ""
        Dim nomefile2 As String = ""
        Dim nomefile3 As String = ""
        If DataGridIncassi.Visible = True Then
            nomefile1 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassi", "ExportIncassi", DataGridIncassi, True, , True)
            'nomefile1 = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridIncassi, "ExportIncassi", , False, , False)
        End If
        If DataGridIncassiNonAttribuibili.Visible = True Then
            nomefile2 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassiNonAttribuibili", "ExportIncassiNonAttribuibili", DataGridIncassiNonAttribuibili, True, , True)
            'nomefile2 = par.EsportaExcelAutomaticoDaDataGrid(DataGridIncassiNonAttribuibili, "ExportIncassiNonAttribuibili", , False, , False)
        End If
        If DataGridIncassiExtraMAV.Visible = True Then
            nomefile3 = xls.EsportaExcelDaDataGrid(ExcelSiSol.Estensione.Office2007_xlsx, "ExportIncassiExtraMAV", "ExportIncassiExtraMAV", DataGridIncassiExtraMAV, True, , True)
            'nomefile3 = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridIncassiExtraMAV, "ExportIncassiExtraMAV", , False, , False)
        End If
        Dim nome As String = "Incassi"
        'COSTRUZIONE ZIPFILE
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String
        zipfic = Server.MapPath("~\FileTemp\" & nome & ".zip")
        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        Dim strFile As String
        Dim strmFile As FileStream
        Dim theEntry As ZipEntry
        If File.Exists(Server.MapPath("~\FileTemp\") & nomefile1) Then
            strFile = Server.MapPath("~\FileTemp\" & nomefile1)
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
            File.Delete(strFile)
        End If
        If File.Exists(Server.MapPath("~\FileTemp\") & nomefile2) Then
            strFile = Server.MapPath("~\FileTemp\" & nomefile2)
            strmFile = File.OpenRead(strFile)
            Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
            Dim sFile1 As String = Path.GetFileName(strFile)
            theEntry = New ZipEntry(sFile1)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer1)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
            File.Delete(strFile)
        End If
        If File.Exists(Server.MapPath("~\FileTemp\") & nomefile3) Then
            strFile = Server.MapPath("~\FileTemp\" & nomefile3)
            strmFile = File.OpenRead(strFile)
            Dim abyBuffer2(Convert.ToInt32(strmFile.Length - 1)) As Byte
            strmFile.Read(abyBuffer2, 0, abyBuffer2.Length)
            Dim sFile2 As String = Path.GetFileName(strFile)
            theEntry = New ZipEntry(sFile2)
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer2)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer2, 0, abyBuffer2.Length)
            File.Delete(strFile)
        End If

        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()
        Dim FileNameZip As String = nome & ".zip"

        If File.Exists(Server.MapPath("~\FileTemp\") & FileNameZip) Then
            Response.Redirect("../../FileTemp/" & FileNameZip, False)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        End If

        'Dim nomeFile As String = EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(DataGridIncassi, "ExportIncassi", , , , False)
        'If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
        '    Response.Redirect("../../FileTemp/" & nomeFile, False)
        'Else
        '    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
        'End If
    End Sub

    Function EsportaExcelAutomaticoDaDataGrid_eliminazioneFont(ByVal datagrid As DataGrid, Optional ByVal nomeFile As String = "", Optional ByVal FattoreLarghezzaColonne As Decimal = 4.75, Optional ByVal EliminazioneLink As Boolean = True, Optional ByVal Titolo As String = "", Optional ByVal creazip As Boolean = True) As String
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
            Dim LarghezzaDataGrid As Integer = Math.Max(datagrid.Width.Value, 200)
            Dim TipoLarghezzaDataGrid As UnitType = datagrid.Width.Type
            Dim LarghezzaColonnaHeader As Decimal = 0
            Dim LarghezzaColonnaItem As Decimal = 0
            'SETTO A ZERO LA VARIABILE DELLE RIGHE
            indiceRighe = 0
            With FileExcel
                'CREO IL FILE 
                .CreateFile(server.MapPath("~\FileTemp\" & FileName & ".xls"))
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
                If Titolo <> "" Then
                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, Titolo, 0)
                    indiceRighe += 1
                    IndiceColonne += 1
                End If
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
                        LarghezzaMinimaColonna = FattoreLarghezzaColonne * Math.Max(LarghezzaColonnaHeader, LarghezzaColonnaItem)
                        .SetColumnWidth(indiceVisibile, indiceVisibile, Math.Max(LarghezzaMinimaColonna, 30))
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
                        If datagrid.Columns.Item(IndiceColonne).Visible = True Then
                            allineamentoCella = datagrid.Columns.Item(IndiceColonne).ItemStyle.HorizontalAlign
                            Select Case EliminazioneLink
                                Case False
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", ""), 0)
                                    End Select

                                Case True
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                    End Select
                                Case Else
                                    Select Case allineamentoCella
                                        Case 1
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 2
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                        Case 3
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, 0), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 4)
                                        Case Else
                                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, indiceRighe, Cella + 1, par.EliminaLink(Replace(Replace(Replace(par.IfNull(Items.Cells(IndiceColonne).Text, ""), "&nbsp;", ""), "<font style='font-weight:bold;font-style:italic;'>", ""), "</font>", "")), 0)
                                    End Select
                            End Select
                            Cella = Cella + 1
                        End If
                    Next

                Next
                'CHIUSURA FILE
                .CloseFile()
            End With
            If creazip = True Then
                'COSTRUZIONE ZIPFILE
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream

                Dim strFile As String
                strFile = server.MapPath("~\FileTemp\" & FileName & ".xls")
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
                zipfic = server.MapPath("~\FileTemp\" & FileName & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                File.Delete(strFile)
                Dim FileNameZip As String = FileName & ".zip"
                Return FileNameZip
            Else
                Dim FileNameExcel As String = FileName & ".xls"
                Return FileNameExcel
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Function DataTableALCSV(ByVal table As Data.DataTable, ByVal filename As String, ByVal sepcar As String, Optional ByVal creazip As Boolean = True) As String
        Dim sr As System.IO.StreamWriter = Nothing
        Dim sep As String = sepcar
        Dim intestazione As String = ""
        Dim flag_inizio As Integer = 0
        Dim indiceRighe As Long = 0
        Dim nome As String = filename & Format(Now, "yyyyMMddHHmmss")
        Try
            'CREO IL FILE CSV
            If table.Rows.Count <= 65536 Then
                Dim nomefile As String = nome & ".csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    Dim stringa As String = ""
                    For Each col As Data.DataColumn In table.Columns
                        If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                            If row(col.ColumnName).ToString <> "" Then
                                stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                stringa = par.RimuoviHTML(stringa)
                            Else
                                stringa = stringa & "" & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Else
                            stringa = stringa & row(col.ColumnName) & sep
                            stringa = par.RimuoviHTML(stringa)
                        End If
                    Next
                    sr.WriteLine(stringa)
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Dim FileNameCSV As String = nomefile
                Return FileNameCSV
            Else
                Dim nomefile1 As String = nome & "_1.csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile1))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    If indiceRighe <= 65535 Then
                        Dim stringa As String = ""
                        For Each col As Data.DataColumn In table.Columns
                            If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                                If row(col.ColumnName).ToString <> "" Then
                                    stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                Else
                                    stringa = stringa & "" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                End If
                            Else
                                stringa = stringa & row(col.ColumnName) & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Next
                        sr.WriteLine(stringa)
                    End If
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                Dim nomefile2 As String = nome & "_2.csv"
                sr = New System.IO.StreamWriter(System.Web.Hosting.HostingEnvironment.MapPath("~\FileTemp\" & nomefile2))
                intestazione = ""
                flag_inizio = 0
                'RIEMPIO L'INTESTAZIONE
                For Each col As Data.DataColumn In table.Columns
                    'CONTROLLO IL PRIMO INSERIMENTO PER EVITARE IL CASO "SYLK: formato di file non valido"
                    If flag_inizio = 0 Then
                        intestazione = intestazione & """" & col.ColumnName & """" & sep
                        flag_inizio = 1
                    Else
                        intestazione = intestazione & col.ColumnName & sep
                    End If
                Next
                sr.WriteLine(intestazione)
                'RIEMPIO LE RIGHE CON I DATI
                indiceRighe = 0
                For Each row As Data.DataRow In table.Rows
                    indiceRighe = indiceRighe + 1
                    If indiceRighe > 65535 Then
                        Dim stringa As String = ""
                        For Each col As Data.DataColumn In table.Columns
                            If col.ToString = "CODICE_UI" Or col.ToString = "SCALA" Or col.ToString = "INTERNO" Or col.ToString = "COD_CONTRATTO" Then
                                If row(col.ColumnName).ToString <> "" Then
                                    stringa = stringa & "=""" & row(col.ColumnName) & """" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                Else
                                    stringa = stringa & "" & sep
                                    stringa = par.RimuoviHTML(stringa)
                                End If
                            Else
                                stringa = stringa & row(col.ColumnName) & sep
                                stringa = par.RimuoviHTML(stringa)
                            End If
                        Next
                        sr.WriteLine(stringa)
                    End If
                Next
                'CHIUSURA CREAZIONE FILE CSV
                sr.Flush()
                sr.Close()
                sr.Dispose()
                'CREAZIONE FILE ZIP
                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String
                zipfic = Server.MapPath("..\FileTemp\" & nome & ".zip")
                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)
                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & nomefile1)
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
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                File.Delete(strFile)

                strFile = Server.MapPath("..\FileTemp\" & nomefile2)
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer1(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer1, 0, abyBuffer1.Length)
                Dim sFile1 As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile1)
                fi = New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer1)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer1, 0, abyBuffer1.Length)
                File.Delete(strFile)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()
                Dim FileNameZip As String = nome & ".zip"
                Return FileNameZip
            End If
        Catch ex As Exception
            If Not sr Is Nothing Then
                sr.Close()
            End If
            Return ""
        End Try
    End Function

End Class
