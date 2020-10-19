Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_Report_RisultatoDocGest
    Inherits System.Web.UI.Page
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
        Response.Flush()

        If Not IsPostBack Then
            CaricaDati()
        End If
    End Sub

    Protected Sub ApriConnessione()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub chiudiConnessione()
        If Not IsNothing(par.OracleConn) Then
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("sStringaSQL1") Is Nothing) Then
                Return ViewState("sStringaSQL1")
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("sStringaSQL1") = value
        End Set
    End Property

    Public Property nomeFile() As String
        Get
            If Not (ViewState("nomeFile") Is Nothing) Then
                Return ViewState("nomeFile")
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("nomeFile") = value
        End Set
    End Property

    Private Sub CaricaDati()
        Try
            '######## DATA STIPULA ##################
            Dim DataStipulaDal As String = ""
            If Not IsNothing(Request.QueryString("DataStipulaDa")) Then
                DataStipulaDal = Request.QueryString("DataStipulaDa")
            End If
            Dim condizioneDataStipulaDal As String = ""
            If DataStipulaDal <> "" Then
                condizioneDataStipulaDal = " AND DATA_STIPULA>='" & DataStipulaDal & "' "
            End If

            Dim DataStipulaAl As String = ""
            If Not IsNothing(Request.QueryString("DataContabileA")) Then
                DataStipulaAl = Request.QueryString("DataContabileA")
            End If
            Dim condizioneDataStipulaAl As String = ""
            If DataStipulaAl <> "" Then
                condizioneDataStipulaAl = " AND DATA_STIPULA<='" & DataStipulaAl & "' "
            End If
            '##########################################


            '######## DATA EMISSIONE ##################
            Dim DataEmissioneDal As String = ""
            If Not IsNothing(Request.QueryString("DataEmissioneDal")) Then
                DataEmissioneDal = Request.QueryString("DataEmissioneDal")
            End If
            Dim condizioneDataEmissioneDal As String = ""
            If DataEmissioneDal <> "" Then
                condizioneDataEmissioneDal = " AND DATA_EMISSIONE>='" & DataEmissioneDal & "' "
            End If

            Dim DataEmissioneAl As String = ""
            If Not IsNothing(Request.QueryString("DataEmissioneAl")) Then
                DataEmissioneAl = Request.QueryString("DataEmissioneAl")
            End If
            Dim condizioneDataEmissioneAl As String = ""
            If DataEmissioneAl <> "" Then
                condizioneDataEmissioneAl = " AND DATA_EMISSIONE<='" & DataEmissioneAl & "'"
            End If
            '##########################################


            '######## DATA RIFERIMENTO ##################
            Dim DataRiferimentoDal As String = ""
            If Not IsNothing(Request.QueryString("RiferimentoDal")) Then
                DataRiferimentoDal = Request.QueryString("RiferimentoDal")
            End If
            Dim condizioneDataRiferimentoDal As String = ""
            If DataRiferimentoDal <> "" Then
                condizioneDataRiferimentoDal = " AND RIFERIMENTO_DA>='" & DataRiferimentoDal & "' "
            End If

            Dim DataRiferimentoAl As String = ""
            If Not IsNothing(Request.QueryString("RiferimentoAl")) Then
                DataRiferimentoAl = Request.QueryString("RiferimentoAl")
            End If
            Dim condizioneDataRiferimentoAl As String = ""
            If DataRiferimentoAl <> "" Then
                condizioneDataRiferimentoAl = " AND RIFERIMENTO_A<='" & DataRiferimentoAl & "' "
            End If
            '##########################################


            '################ IMPORTO #################
            Dim ImportoDa As String = ""
            If Not IsNothing(Request.QueryString("ImportoDa")) Then
                ImportoDa = Request.QueryString("ImportoDa")
            End If
            Dim condizioneImportoDa As String = ""
            If ImportoDa <> "" Then
                condizioneImportoDa = " AND IMPORTO_TOTALE>=" & par.VirgoleInPunti(CDec(ImportoDa)) & " "
            End If

            Dim ImportoA As String = ""
            If Not IsNothing(Request.QueryString("ImportoA")) Then
                ImportoA = Request.QueryString("ImportoA")
            End If
            Dim condizioneImportoA As String = ""
            If ImportoA <> "" Then
                condizioneImportoA = " AND IMPORTO_TOTALE<=" & par.VirgoleInPunti(CDec(ImportoA)) & " "
            End If
            '##########################################


            Dim CodContr As String = ""
            Dim sValore As String = ""
            Dim sCompara As String = ""
            If Not IsNothing(Request.QueryString("CodContratto")) Then
                CodContr = Request.QueryString("CodContratto")
            End If
            Dim condizioneCodContr As String = ""
            If CodContr <> "" Then
                sValore = CodContr
                If InStr(CodContr, "*") Then
                    sCompara = " LIKE "
                    par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                condizioneCodContr = " AND RAPPORTI_UTENZA.COD_CONTRATTO " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If


            Dim credito As String = ""
            If Not IsNothing(Request.QueryString("Credito")) Then
                credito = Request.QueryString("Credito")
            End If
            Dim condizioneCredito As String = ""
            If credito <> "" Then
                Select Case credito
                    Case "0"
                        condizioneCredito = " AND IMPORTO_TOTALE>0 "
                    Case "1"
                        condizioneCredito = " AND IMPORTO_TOTALE<0"
                End Select
            End If


            Dim elaborati As String = ""
            If Not IsNothing(Request.QueryString("Elaborati")) Then
                elaborati = Request.QueryString("Elaborati")
            End If
            Dim condizioneElaborati As String = ""
            If elaborati <> "" Then
                Select Case elaborati
                    Case "0"
                        condizioneElaborati = " AND BOL_BOLLETTE_GEST.TIPO_APPLICAZIONE='N' "
                End Select
            End If

            'FILIALI
            Dim listaFiliali As System.Collections.Generic.List(Of String) = Session.Item("listaFiliali")
            Session.Remove("listaFiliali")
            Dim listaFilialiSi As Boolean = False
            Dim condizionelistaFiliali As String = ""
            Dim fromCondizioneFiliale = ""
            If Not IsNothing(listaFiliali) Then
                For Each Items As String In listaFiliali
                    condizionelistaFiliali &= Items & ","
                Next
            End If
            If condizionelistaFiliali <> "" Then
                condizionelistaFiliali = Left(condizionelistaFiliali, Len(condizionelistaFiliali) - 1)
                condizionelistaFiliali = " and tab_filiali.ID in (" & condizionelistaFiliali & ") "
                'fromCondizioneFiliale = ",siscom_mi.filiali_ui,siscom_mi.tab_filiali"
                listaFilialiSi = True
            End If

            'listaQuartieri
            Dim listaQuartieri As System.Collections.Generic.List(Of String) = Session.Item("listaQuartieri")
            Session.Remove("listaQuartieri")
            Dim listaQuartieriSi As Boolean = False
            Dim condizionelistaQuartieri As String = ""
            Dim formCondizQuartComplEdifici As String = ""
            If Not IsNothing(listaFiliali) Then
                For Each Items As String In listaQuartieri
                    condizionelistaQuartieri &= Items & ","
                Next
            End If
            If condizionelistaQuartieri <> "" Then
                condizionelistaQuartieri = Left(condizionelistaQuartieri, Len(condizionelistaQuartieri) - 1)
                condizionelistaQuartieri = " AND complessi_immobiliari.id = edifici.id_complesso AND unita_immobiliari.id_edificio = edifici.id AND ID_QUARTIERE IN (" & condizionelistaQuartieri & ") "
                formCondizQuartComplEdifici = "siscom_mi.complessi_immobiliari,siscom_mi.edifici, "
                listaFilialiSi = True
            End If

            Dim listaComplessi As System.Collections.Generic.List(Of String) = Session.Item("listaComplessi")
            Session.Remove("listaComplessi")
            Dim listaComplessiSi As Boolean = False
            Dim condizionelistaComplessi As String = ""
            If Not IsNothing(listaComplessi) Then
                For Each Items As String In listaComplessi
                    condizionelistaComplessi &= Items & ","
                Next
            End If
            If condizionelistaComplessi <> "" Then
                condizionelistaComplessi = Left(condizionelistaComplessi, Len(condizionelistaComplessi) - 1)
                If condizionelistaQuartieri <> "" Then
                    condizionelistaComplessi = " AND COMPLESSI_IMMOBILIARI.ID IN (" & condizionelistaComplessi & ") "
                Else
                    formCondizQuartComplEdifici = "siscom_mi.complessi_immobiliari,siscom_mi.edifici,"
                    condizionelistaComplessi = " AND complessi_immobiliari.id = edifici.id_complesso AND unita_immobiliari.id_edificio = edifici.id AND COMPLESSI_IMMOBILIARI.ID IN (" & condizionelistaComplessi & ") "
                End If

                listaComplessiSi = True
            End If

            Dim listaEdifici As System.Collections.Generic.List(Of String) = Session.Item("listaEdifici")
            Session.Remove("listaEdifici")
            Dim listaEdificiSi As Boolean = False
            Dim condizionelistaEdifici As String = ""
            If Not IsNothing(listaEdifici) Then
                For Each Items As String In listaEdifici
                    condizionelistaEdifici &= Items & ","
                Next
            End If
            If condizionelistaEdifici <> "" Then
                condizionelistaEdifici = Left(condizionelistaEdifici, Len(condizionelistaEdifici) - 1)
                If condizionelistaQuartieri <> "" Or condizionelistaComplessi <> "" Then
                    condizionelistaEdifici = " AND EDIFICI.ID IN (" & condizionelistaEdifici & ") "
                Else
                    formCondizQuartComplEdifici = "siscom_mi.complessi_immobiliari,siscom_mi.edifici,"
                    condizionelistaEdifici = " AND complessi_immobiliari.id = edifici.id_complesso AND unita_immobiliari.id_edificio = edifici.id AND EDIFICI.ID IN (" & condizionelistaEdifici & ") "
                End If

                listaEdificiSi = True
            End If

            Dim listaIndirizzo As System.Collections.Generic.List(Of String) = Session.Item("listaIndirizzo")
            Session.Remove("listaIndirizzo")
            Dim listaIndirizzoSi As Boolean = False
            Dim condizionelistaIndirizzo As String = ""
            If Not IsNothing(listaIndirizzo) Then
                For Each Items As String In listaIndirizzo
                    condizionelistaIndirizzo &= Items & ","
                Next
            End If
            If condizionelistaIndirizzo <> "" Then
                condizionelistaIndirizzo = Left(condizionelistaIndirizzo, Len(condizionelistaIndirizzo) - 1)
                condizionelistaIndirizzo = " AND ID_INDIRIZZO IN (" & condizionelistaIndirizzo & ") "
                listaIndirizzoSi = True
            End If

            Dim listaTipoRapporto As System.Collections.Generic.List(Of String) = Session.Item("listaTipoRapporto")
            Session.Remove("listaTipoRapporto")
            Dim listaTipoRapportoSi As Boolean = False
            Dim condizionelistaTipoRapporto As String = ""
            If Not IsNothing(listaTipoRapporto) Then
                For Each Items As String In listaTipoRapporto
                    condizionelistaTipoRapporto &= "'" & Items & "',"
                Next
            End If
            If condizionelistaTipoRapporto <> "" Then
                condizionelistaTipoRapporto = Left(condizionelistaTipoRapporto, Len(condizionelistaTipoRapporto) - 1)
                condizionelistaTipoRapporto = " AND COD_TIPOLOGIA_CONTR_LOC IN (" & condizionelistaTipoRapporto.ToUpper & ") "
                listaTipoRapportoSi = True
            End If


            '######## CONDIZIONE TIPOLOGIA ############
            Dim listaTipoUI As System.Collections.Generic.List(Of String) = Session.Item("listaTipoUI")
            Session.Remove("listaTipoUI")
            Dim listaTipoUISi As Boolean = False
            Dim condizionelistaTipoUI As String = ""
            If Not IsNothing(listaTipoUI) Then
                For Each Items As String In listaTipoUI
                    condizionelistaTipoUI &= "'" & Items & "',"
                Next
            End If
            If condizionelistaTipoUI <> "" Then
                condizionelistaTipoUI = Left(condizionelistaTipoUI, Len(condizionelistaTipoUI) - 1)
                condizionelistaTipoUI = " AND UNITA_IMMOBILIARI.COD_TIPOLOGIA IN (" & condizionelistaTipoUI & ") "
                listaTipoUISi = True
            End If
            '########################################################


            '######## CONDIZIONE STATO CONTRATTO ############
            Dim listaStatoContr As System.Collections.Generic.List(Of String) = Session.Item("listaStatoContr")
            Session.Remove("listaStatoContr")
            Dim listaStatoContrSi As Boolean = False
            Dim condizionelistaStatoContr As String = ""
            If Not IsNothing(listaStatoContr) Then
                For Each Items As String In listaStatoContr
                    condizionelistaStatoContr &= "'" & Items & "',"
                Next
            End If
            If condizionelistaStatoContr <> "" Then
                condizionelistaStatoContr = Left(condizionelistaStatoContr, Len(condizionelistaStatoContr) - 1)
                condizionelistaStatoContr = " AND SISCOM_MI.GETSTATOCONTRATTO(RAPPORTI_UTENZA.ID) IN (" & condizionelistaStatoContr.ToUpper & ") "
                listaStatoContrSi = True
            End If
            '########################################################


            '######## CONDIZIONE AREA CANONE ############
            Dim listaAreaCanone As System.Collections.Generic.List(Of String) = Session.Item("listaAreaCanone")
            Session.Remove("listaAreaCanone")
            Dim listaAreaCanoneSi As Boolean = False
            Dim condizionelistaAreaCanone As String = ""
            Dim areeSelezionate As Integer = 0
            Dim fromCondizioneAreaCanone As String = ""
            If Not IsNothing(listaAreaCanone) Then
                For Each Items As String In listaAreaCanone
                    condizionelistaAreaCanone &= "'" & Items & "',"
                    areeSelezionate += 1
                Next
            End If
            If condizionelistaAreaCanone <> "" And areeSelezionate <> 5 And areeSelezionate <> 0 Then
                condizionelistaAreaCanone = Left(condizionelistaAreaCanone, Len(condizionelistaAreaCanone) - 1)
                condizionelistaAreaCanone = " AND rapporti_utenza_ae.id_contratto=rapporti_utenza.id and id_area_economica in (" & condizionelistaAreaCanone & ") "
                fromCondizioneAreaCanone = " SISCOM_MI.rapporti_utenza_Ae, "
                listaAreaCanoneSi = True
            End If

            '########################################################


            '############## CONDIZIONE TIPO DOCUMENTO ###############
            Dim listaTipoDoc As System.Collections.Generic.List(Of String) = Session.Item("listaTipoDoc")
            Session.Remove("listaTipoDoc")
            Dim listaTipoDocSi As Boolean = False
            Dim condizionelistaTipoDoc As String = ""
            If Not IsNothing(listaTipoDoc) Then
                For Each Items As String In listaTipoDoc
                    condizionelistaTipoDoc &= "'" & Items & "',"
                Next
            End If
            If condizionelistaTipoDoc <> "" Then
                condizionelistaTipoDoc = Left(condizionelistaTipoDoc, Len(condizionelistaTipoDoc) - 1)
                condizionelistaTipoDoc = " AND BOL_BOLLETTE_GEST.ID_TIPO IN (" & condizionelistaTipoDoc & ") "
                listaTipoDocSi = True
            End If
            '########################################################

            ApriConnessione()

            par.cmd.CommandText = "SELECT " _
                & "DISTINCT (bol_bollette_gest.ID) AS id_boll_gest, " _
                & "rapporti_utenza.ID AS ID,  " _
                & "cod_contratto,unita_immobiliari.cod_unita_immobiliare, " _
                & "(CASE WHEN anagrafica.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM(anagrafica.cognome|| ' ' || anagrafica.nome)) END) AS intestatario, " _
                & "indirizzi.descrizione ||', '|| indirizzi.civico AS indirizzo, " _
                & "indirizzi.localita,indirizzi.civico, " _
                & "tipologia_contratto_locazione.descrizione AS tipo_cont ," _
                & "(CASE WHEN rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2 THEN 'ERP Sociale' " _
                & "WHEN unita_immobiliari.id_destinazione_uso = 2 THEN 'ERP Moderato' " _
                & "WHEN rapporti_utenza.provenienza_ass = 12 THEN 'CANONE CONVENZ.' " _
                & "WHEN rapporti_utenza.provenienza_ass = 10 THEN 'FORZE DELL''ORDINE' " _
                & "WHEN rapporti_utenza.dest_uso = 'C' THEN 'Cooperative' " _
                & "WHEN rapporti_utenza.dest_uso = 'P' THEN '431 P.O.R.' " _
                & "WHEN rapporti_utenza.dest_uso = 'D' THEN '431/98 ART.15 R.R.1/2004' " _
                & "WHEN rapporti_utenza.dest_uso = 'S' THEN '431/98 Speciali' " _
                & "WHEN rapporti_utenza.dest_uso = '0' THEN 'Standard' END) AS tipo_spec, " _
                & "siscom_mi.getstatocontratto (rapporti_utenza.ID) AS statocontr, " _
                & "tipologia_unita_immobiliari.descrizione AS tipo_ui, tab_filiali.nome as nomefiliale," _
                & "(SELECT area_economica.descrizione FROM siscom_mi.canoni_ec, " _
                & "siscom_mi.area_economica WHERE canoni_ec.id_area_economica = area_economica.ID " _
                & "AND canoni_ec.id_contratto = rapporti_utenza.ID AND (inizio_validita_can <=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231' " _
                & "AND canoni_ec.fine_validita_can >=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231') " _
                & "AND data_calcolo IN (SELECT MAX (data_calcolo) FROM siscom_mi.canoni_ec WHERE canoni_ec.id_contratto = rapporti_utenza.ID " _
                & "AND (inizio_validita_can <=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231' " _
                & "AND canoni_ec.fine_validita_can >=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231'))) AS areaannocomp, " _
                & "(SELECT sotto_area FROM siscom_mi.canoni_ec WHERE canoni_ec.id_contratto = rapporti_utenza.ID  " _
                & "AND (inizio_validita_can <=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231' " _
                & "AND canoni_ec.fine_validita_can >=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231') " _
                & "AND data_calcolo IN (SELECT MAX (data_calcolo) FROM siscom_mi.canoni_ec WHERE canoni_ec.id_contratto = rapporti_utenza.ID " _
                & "AND (inizio_validita_can <=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231' " _
                & "AND canoni_ec.fine_validita_can >=''|| SUBSTR (bol_bollette_gest.riferimento_da,1,4)|| '1231'))) AS classeannocomp, " _
                & "(SELECT area_economica.descrizione FROM siscom_mi.canoni_ec,siscom_mi.area_economica WHERE canoni_ec.id_area_economica = area_economica.ID " _
                & "AND canoni_ec.id_contratto = rapporti_utenza.ID AND data_calcolo IN (SELECT MAX (data_calcolo) FROM siscom_mi.canoni_ec WHERE canoni_ec.id_contratto = rapporti_utenza.ID)) AS areaattuale, " _
                & "(SELECT sotto_area FROM siscom_mi.canoni_ec WHERE canoni_ec.id_contratto = rapporti_utenza.ID AND data_calcolo IN (SELECT MAX (data_calcolo) FROM siscom_mi.canoni_ec  " _
                & "WHERE canoni_ec.id_contratto = rapporti_utenza.ID)) AS classeattuale, " _
                & "TO_CHAR (TO_DATE (data_stipula, 'yyyymmdd'),'dd/mm/yyyy') AS data_stip, " _
                & "TO_CHAR (TO_DATE (data_riconsegna, 'yyyymmdd'),'dd/mm/yyyy') AS data_ricons, " _
                & "tipo_bollette_gest.descrizione AS tipo_doc, " _
                & "TO_CHAR (TO_DATE (data_emissione, 'yyyymmdd'),'dd/mm/yyyy') AS data_emiss, " _
                & "TO_CHAR (TO_DATE (riferimento_da, 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim1, " _
                & "TO_CHAR (TO_DATE (riferimento_a, 'yyyymmdd'),'dd/mm/yyyy') AS data_riferim2, " _
                & "TO_CHAR (bol_bollette_gest.importo_totale,'9G999G990D99') AS imp_emesso, " _
                & "CASE WHEN (SELECT DISTINCT (edifici.ID) FROM siscom_mi.edifici " _
                & "WHERE edifici.ID =unita_immobiliari.id_edificio AND edifici.ID IN ( " _
                & "SELECT DISTINCT (id_edificio) FROM siscom_mi.cond_edifici " _
                & "WHERE id_edificio = edifici.ID)) IS NOT NULL THEN 'SI' else 'NO' END AS condom, " _
                & "(SELECT sum(valore) FROM siscom_mi.dimensioni WHERE dimensioni.id_unita_immobiliare = unita_immobiliari.ID " _
                & "AND cod_tipologia = 'SUP_NETTA') AS supnetta, " _
                & "(SELECT sum(valore) FROM siscom_mi.dimensioni WHERE dimensioni.id_unita_immobiliare = unita_immobiliari.ID " _
                & "AND cod_tipologia = 'SUP_CONV') AS supconv " _
            & "FROM siscom_mi.bol_bollette_gest," _
            & fromCondizioneAreaCanone _
            & formCondizQuartComplEdifici _
            & "siscom_mi.rapporti_utenza," _
            & "siscom_mi.unita_contrattuale," _
            & "siscom_mi.tipologia_contratto_locazione," _
            & "siscom_mi.tipologia_unita_immobiliari," _
            & "siscom_mi.tipo_bollette_gest," _
            & "siscom_mi.unita_immobiliari," _
            & "siscom_mi.soggetti_contrattuali," _
            & "siscom_mi.anagrafica," _
            & "siscom_mi.indirizzi, " _
            & "siscom_mi.tab_filiali," _
            & "siscom_mi.filiali_ui " _
            & "WHERE bol_bollette_gest.id_contratto = rapporti_utenza.ID " _
        & "AND rapporti_utenza.ID = unita_contrattuale.id_contratto " _
        & "AND unita_contrattuale.id_unita = unita_immobiliari.ID " _
        & "AND unita_immobiliari.id_unita_principale IS NULL " _
        & "AND unita_contrattuale.id_unita_principale IS NULL " _
        & "AND tipologia_contratto_locazione.cod =rapporti_utenza.cod_tipologia_contr_loc " _
        & "AND bol_bollette_gest.id_tipo = tipo_bollette_gest.ID(+) " _
        & "AND anagrafica.ID = soggetti_contrattuali.id_anagrafica " _
        & "AND soggetti_contrattuali.id_contratto = rapporti_utenza.ID " _
        & "AND unita_immobiliari.id_indirizzo = indirizzi.ID(+) " _
        & "AND cod_tipologia_occupante = 'INTE' " _
        & "AND unita_immobiliari.cod_tipologia = tipologia_unita_immobiliari.cod " _
        & "AND unita_immobiliari.ID(+) = filiali_ui.id_ui " _
        & "AND filiali_ui.id_filiale = tab_filiali.ID(+) " _
        & "AND nvl(inizio_validita,'19000101') <= TO_CHAR (SYSDATE, 'yyyymmdd') " _
        & "AND nvl(fine_validita,'29990101') > TO_CHAR (SYSDATE, 'yyyymmdd')" _
        & condizioneCodContr _
        & condizioneCredito _
        & condizioneDataEmissioneAl _
        & condizioneDataEmissioneDal _
        & condizioneDataRiferimentoAl _
        & condizioneDataRiferimentoDal _
        & condizioneDataStipulaAl _
        & condizioneDataStipulaDal _
        & condizioneElaborati _
        & condizioneImportoA _
        & condizioneImportoDa _
        & condizionelistaAreaCanone _
        & condizionelistaComplessi _
        & condizionelistaEdifici _
        & condizionelistaFiliali _
        & condizionelistaIndirizzo _
        & condizionelistaQuartieri _
        & condizionelistaStatoContr _
        & condizionelistaTipoDoc _
        & condizionelistaTipoRapporto _
        & condizionelistaTipoUI _
        & "ORDER BY " & Request.QueryString("Ordinamento")

            sStringaSQL1 = par.cmd.CommandText
            Dim dAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dTable As New Data.DataTable()

            dAdapter.Fill(dTable)
            dAdapter.Dispose()

            If dTable.Rows.Count > 0 Then

                DataGrDettaglio.DataSource = dTable
                DataGrDettaglio.DataBind()
                dTable.Dispose()

                lblNumRisult.Text = " - Trovate: " & Format(dTable.Rows.Count, "##,##") & " scritture"
            Else
                lblNumRisult.Text = " - Trovate: 0 scritture"
            End If


            chiudiConnessione()

        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CaricaDati() " & ex.Message)
            Response.Write("<script>document.location.href=""../../Errore.aspx""</script>")
        End Try
    End Sub

    Protected Sub DataGrDettaglio_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrDettaglio.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrDettaglio.CurrentPageIndex = e.NewPageIndex
            CaricaDati()
        End If
    End Sub

    Protected Sub ImageButtonExcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonExcel.Click

        Try
            Dim dt As New Data.DataTable

            ApriConnessione()

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            da = New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(dt)
            da.Dispose()

            chiudiConnessione()

            Dim xls As New ExcelSiSol
            Dim pathFileCompleta As String = ""
            nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2003_xls, "ExportGest", "Documenti_Gestionali", dt, True)
            'Dim nomeFile As String = par.EsportaExcelDaDTWithDatagrid(dtExport, Me.dgvUnitaImmob, "ExportUnitaImmobiliari", , , , False)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msgEX", "document.location.href=""..\/..\/FileTemp/\" & nomeFile & """", True)
                ' Response.Write("<script type='text/javascript' language='javascript'>downloadExcel('..\/..\/FileTemp/\" & nomeFile & "');</script>")
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msgEx", "window.open('DownloadDocGest.aspx?NOME=" & nomeFile & "','newPg','');", True)
                'pathFileCompleta = "..\/..\/FileTemp/\" & nomeFile
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;document.location.href=""DownloadDocGest.aspx?NOME=" & pathFileCompleta & """;}", True)

                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../FileTemp/" & nomeFile & "','downloadExcel','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>document.location.href=""../../Errore.aspx""</script>")
        End Try
    End Sub

End Class
