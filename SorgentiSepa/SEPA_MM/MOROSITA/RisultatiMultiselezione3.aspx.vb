Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Partial Class MOROSITA_RisultatiMultiselezione3
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim listaQueryInquilini3Mesi As New System.Collections.Generic.List(Of String)
    Dim listaQueryInquilini312Mesi As New System.Collections.Generic.List(Of String)
    Dim listaQueryInquilini12Mesi As New System.Collections.Generic.List(Of String)
    Dim listaQueryInquiliniAllMesi As New System.Collections.Generic.List(Of String)
    Dim listaQuerySomme3Mesi As New System.Collections.Generic.List(Of String)
    Dim listaQuerySomme312Mesi As New System.Collections.Generic.List(Of String)
    Dim listaQuerySomme12Mesi As New System.Collections.Generic.List(Of String)
    Dim listaQuerySommeAllMesi As New System.Collections.Generic.List(Of String)
    Dim listaQueryInquilinibolMesi As New System.Collections.Generic.List(Of String)
    Dim listaQuerySommebolMesi As New System.Collections.Generic.List(Of String)
    Dim listaQueryInquiliniAllMesiCredito As New System.Collections.Generic.List(Of String)
    Dim listaQueryInquiliniExcel As New System.Collections.Generic.List(Of String)
    Dim RUcreditori As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            Exit Sub
        End If
        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
            & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
            & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
            & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
            & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
            & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
            & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
            & "</td></tr></table></div></div>"
        Response.Write(Loading)
        If Not IsPostBack Then
            Response.Flush()
            ListaM = Session.Item("LISTAMOROSITA")
            'ListaC = Session.Item("LISTACREDITORI")
            parametriDiRicerca.Text = Session.Item("PARAMETRIDIRICERCA")
            caricaDati3Mesi()
            caricaDati312Mesi()
            caricaDati12Mesi()
            caricaDatiTutti()
            'caricaMorosita()
            caricaDatiTuttiCreditori()
            caricaDatiTuttiBollette()
            pannelliVisibili()
            eliminaTabellaTemporanea()
            'Session.Remove("LISTACREDITORI")
            If IsNothing(Session.Item("LISTAMOROSITA")) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('E\' necessario selezionare almeno un inquilino! Ripetere la ricerca!');location.href='RicercaDebitoriMultiSelezione.aspx';", True)
            End If
            Session.Remove("LISTAMOROSITA")
        End If
    End Sub
    Public Property ListaM() As Object
        Get
            If Not (ViewState("ListaM") Is Nothing) Then
                Return CObj(ViewState("ListaM"))
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As Object)
            ViewState("ListaM") = value
        End Set
    End Property
    'Public Property ListaC() As Object
    '    Get
    '        If Not (ViewState("ListaC") Is Nothing) Then
    '            Return CObj(ViewState("ListaC"))
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    '    Set(ByVal value As Object)
    '        ViewState("ListaC") = value
    '    End Set
    'End Property
    Private Sub caricaDatiTutti()
        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If
            Dim listaContratti As String = ""
            If Not IsNothing(ListaM) Then
                Dim queryInquiliniallMesi As String = ""
                Dim querySommeallMesi As String = ""
                If ListaM(0) = "-1" Then
                    queryInquiliniallMesi = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                    querySommeallMesi = "SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                Else
                    Dim i As Integer = 0
                    Dim fine As Boolean = False
                    While i < 40 And fine = False
                        listaContratti = ""
                        For j As Integer = 0 To 999
                            If 1000 * i + j < ListaM.Count Then
                                listaContratti &= ListaM(1000 * i + j) & ","
                            Else
                                fine = True
                                Exit For
                            End If
                        Next
                        If listaContratti <> "" Then
                            listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                            listaQueryInquiliniAllMesi.Add("SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)


                            listaQuerySommeAllMesi.Add("SELECT " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                   & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                   & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                   & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                   & " SISCOM_MI.ANAGRAFICA, " _
                                   & " SISCOM_MI.INDIRIZZI, " _
                                   & " SISCOM_MI.EDIFICI, " _
                                   & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                   & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                   & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                   & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                   & " SISCOM_MI.SALDI_MESE " _
                                   & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                   & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                   & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                   & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                   & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                   & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                   & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                   & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                   & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                   & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                   & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                   & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                   & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                   & " AND MESE=" & mese & " AND ANNO=" & anno)

                        End If
                        i += 1
                    End While
                    '--------------- TUTTI -----------------------------
                    For Each Items As String In listaQueryInquiliniAllMesi
                        queryInquiliniallMesi &= Items & " UNION "
                    Next
                    For Each Items As String In listaQuerySommeAllMesi
                        querySommeallMesi &= Items & " UNION "
                    Next
                    queryInquiliniallMesi = Left(queryInquiliniallMesi, Len(queryInquiliniallMesi) - 6)
                    querySommeallMesi = Left(querySommeallMesi, Len(querySommeallMesi) - 6)
                End If

                ApriConnessione()
                par.cmd.CommandText = querySommeallMesi
                Dim LettoreALLMesi As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim MorositaTotaleALLMesi As Decimal = 0
                Dim MorositaExGestoriALLMesi As Decimal = 0
                Dim MorositaCanoniALLMesi As Decimal = 0
                Dim MorositaServiziALLMesi As Decimal = 0
                Dim MorositaALERALLMesi As Decimal = 0
                While LettoreALLMesi.Read
                    MorositaTotaleALLMesi += CDec(par.IfNull(LettoreALLMesi("SALDO_TOTALE"), 0))
                    MorositaExGestoriALLMesi += CDec(par.IfNull(LettoreALLMesi("SALDO_GLOBAL"), 0))
                    MorositaCanoniALLMesi += CDec(par.IfNull(LettoreALLMesi("CANONE_ALER"), 0))
                    MorositaServiziALLMesi += CDec(par.IfNull(LettoreALLMesi("SERVIZI_ALER"), 0))
                    MorositaALERALLMesi += CDec(par.IfNull(LettoreALLMesi("SALDO_ALER"), 0))
                End While
                ImportoMorositaTotaleAllMesi.Text = Format(MorositaTotaleALLMesi, "##,#0.00")
                ImportoMorositaExGestoriallMesi.Text = Format(MorositaExGestoriALLMesi, "##,#0.00")
                ImportoMorositaCanoniAllMesi.Text = Format(MorositaCanoniALLMesi, "##,#0.00")
                ImportoMorositaServiziAllMesi.Text = Format(MorositaServiziALLMesi, "##,#0.00")
                ImportoMorositaALERallMesi.Text = Format(MorositaALERALLMesi, "##,#0.00")
                LettoreALLMesi.Close()

                par.cmd.CommandText = queryInquiliniallMesi
                Dim dataAdapterALL As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTableALL As New Data.DataTable
                dataAdapterALL.Fill(dataTableALL)
                Dim nRUmorosiALL As Integer = 0
                Dim nRUmorosiMMALL As Integer = 0
                Dim nRUmorosiCSALL As Integer = 0
                Dim nRUmorosiPLALL As Integer = 0
                Dim nRUmorosiAltriALL As Integer = 0
                Dim RUmorosiALLMesi As Decimal = 0
                Dim RUmorosiMMALLMesi As Decimal = 0
                Dim RUmorosiCSALLMesi As Decimal = 0
                Dim RUmorosiPLALLMesi As Decimal = 0
                Dim RUmorosiAltriALLMesi As Decimal = 0
                Dim LettoreMorosiALL As Oracle.DataAccess.Client.OracleDataReader
                For Each Items As Data.DataRow In dataTableALL.Rows
                    nRUmorosiALL += 1
                    RUmorosiALLMesi += Items.Item(4)
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=" & Items.Item(1)
                    LettoreMorosiALL = par.cmd.ExecuteReader
                    If LettoreMorosiALL.Read Then
                        If par.IfNull(LettoreMorosiALL(0), 0) <> 0 Then
                            nRUmorosiMMALL += 1
                            RUmorosiMMALLMesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosiALL.Close()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE COD_STATO='M20'" _
                                & "AND ID_CONTRATTO = " & Items.Item(1)
                    LettoreMorosiALL = par.cmd.ExecuteReader
                    If LettoreMorosiALL.Read Then
                        If par.IfNull(LettoreMorosiALL(0), 0) <> 0 Then
                            nRUmorosiPLALL += 1
                            RUmorosiPLALLMesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosiALL.Close()
                Next

                RUmorosiAltriALLMesi = RUmorosiALLMesi - RUmorosiMMALLMesi - RUmorosiCSALLMesi - RUmorosiPLALLMesi
                MorositaPLallMesi.Text = Format(RUmorosiPLALLMesi, "##,#0.00")
                MorositaCSallMesi.Text = Format(RUmorosiCSALLMesi, "##,#0.00")
                MorositaMMallMesi.Text = Format(RUmorosiMMALLMesi, "##,#0.00")
                MorositaAltriAllMesi.Text = Format(RUmorosiAltriALLMesi, "##,#0.00")
                nRUmorosiAltriALL = nRUmorosiALL - nRUmorosiMMALL - nRUmorosiCSALL - nRUmorosiPLALL
                NAssegnatariAllMesi.Text = nRUmorosiALL
                NAssegnatariMMallMesi.Text = nRUmorosiMMALL
                NAssegnatariCSallMesi.Text = nRUmorosiCSALL
                NAssegnatariPLallMesi.Text = nRUmorosiPLALL
                NAssegnatariAltriallMesi.Text = nRUmorosiAltriALL
                '------------------------------------------------------------------------------------------
                LabelDataAggiornamento.Text = par.FormattaData(Request.QueryString("d"))
            End If
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub
    Private Sub caricaDatiTuttiBollette()
        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If
            Dim listaContratti As String = ""
            If Not IsNothing(ListaM) Then
                Dim querySommebolMesi As String = ""
                If ListaM(0) = "-1" Then
                    querySommebolMesi = "SELECT " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.morosita_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.morosita_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.morosita_GLOBAL,0)),'999g999g999g990d99')) as saldo_GLOBAL, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.morosita_ALER,0)),'999g999g999g990d99')) as saldo_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.mor_CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.mor_SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.num_bollette_mor_ALER,0)+nvl(num_bollette_mor_global,0)),'999g999g999g990d99')) as bollette " _
                                   & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                   & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                   & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                   & " SISCOM_MI.ANAGRAFICA, " _
                                   & " SISCOM_MI.INDIRIZZI, " _
                                   & " SISCOM_MI.EDIFICI, " _
                                   & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                   & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                   & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                   & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                   & " SISCOM_MI.SALDI_MESE " _
                                   & " , " & ListaM(1) & " " _
                                   & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                   & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                   & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                   & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                   & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                   & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                   & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                   & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                   & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                   & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                   & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                   & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                   & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "




                Else

                    Dim i As Integer = 0
                    Dim fine As Boolean = False
                    While i < 40 And fine = False
                        listaContratti = ""
                        For j As Integer = 0 To 999
                            If 1000 * i + j < ListaM.Count Then
                                listaContratti &= ListaM(1000 * i + j) & ","
                            Else
                                fine = True
                                Exit For
                            End If
                        Next
                        If listaContratti <> "" Then
                            listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                            listaQuerySommebolMesi.Add("SELECT " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.morosita_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.morosita_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.morosita_GLOBAL,0)),'999g999g999g990d99')) as saldo_GLOBAL, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.morosita_ALER,0)),'999g999g999g990d99')) as saldo_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.mor_CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.mor_SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER, " _
                                   & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.num_bollette_mor_ALER,0)+nvl(num_bollette_mor_global,0)),'999g999g999g990d99')) as bollette " _
                                   & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                   & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                   & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                   & " SISCOM_MI.ANAGRAFICA, " _
                                   & " SISCOM_MI.INDIRIZZI, " _
                                   & " SISCOM_MI.EDIFICI, " _
                                   & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                   & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                   & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                   & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                   & " SISCOM_MI.SALDI_MESE " _
                                   & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                   & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                   & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                   & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                   & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                   & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                   & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                   & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                   & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                   & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                   & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                   & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                   & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                   & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                   & " AND MESE=" & mese & " AND ANNO=" & anno)

                        End If
                        i += 1
                    End While
                    '--------------- TUTTI -----------------------------
                    For Each Items As String In listaQuerySommebolMesi
                        querySommebolMesi &= Items & " UNION "
                    Next
                    querySommebolMesi = Left(querySommebolMesi, Len(querySommebolMesi) - 6)
                End If

                ApriConnessione()
                par.cmd.CommandText = querySommebolMesi
                Dim LettorebolMesi As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim MorositaTotalebolMesi As Decimal = 0
                Dim MorositaExGestoribolMesi As Decimal = 0
                Dim MorositaCanonibolMesi As Decimal = 0
                Dim MorositaServizibolMesi As Decimal = 0
                Dim MorositaALERbolMesi As Decimal = 0
                Dim Nbollette As Integer = 0
                While LettorebolMesi.Read
                    MorositaTotalebolMesi += CDec(par.IfNull(LettorebolMesi("SALDO_TOTALE"), 0))
                    MorositaExGestoribolMesi += CDec(par.IfNull(LettorebolMesi("SALDO_GLOBAL"), 0))
                    MorositaCanonibolMesi += CDec(par.IfNull(LettorebolMesi("CANONE_ALER"), 0))
                    MorositaServizibolMesi += CDec(par.IfNull(LettorebolMesi("SERVIZI_ALER"), 0))
                    MorositaALERbolMesi += CDec(par.IfNull(LettorebolMesi("SALDO_ALER"), 0))
                    Nbollette += CInt(par.IfNull(LettorebolMesi("bollette"), 0))
                End While
                ImportoMorositaTotaleBolMesi.Text = Format(MorositaTotalebolMesi, "##,#0.00")
                ImportoMorositaExGestoriBolMesi.Text = Format(MorositaExGestoribolMesi, "##,#0.00")
                ImportoMorositaCanoniBolMesi.Text = Format(MorositaCanonibolMesi, "##,#0.00")
                ImportoMorositaServiziBolMesi.Text = Format(MorositaServizibolMesi, "##,#0.00")
                ImportoMorositaALERBolMesi.Text = Format(MorositaALERbolMesi, "##,#0.00")
                NAssegnatariBolMesi.Text = Nbollette
                LettorebolMesi.Close()
                '------------------------------------------------------------------------------------------
            End If
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub
    Private Sub caricaDatiTuttiCreditori()
        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If
            Dim listaContratti As String = ""
            If Not IsNothing(ListaM) Then
                Dim queryInquiliniALLMesiCredito As String = ""
                If ListaM(0) = "-1" Then
                    queryInquiliniALLMesiCredito = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)<=0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "




                Else


                    Dim i As Integer = 0
                    Dim fine As Boolean = False
                    While i < 40 And fine = False
                        listaContratti = ""
                        For j As Integer = 0 To 999
                            If 1000 * i + j < ListaM.Count Then
                                listaContratti &= ListaM(1000 * i + j) & ","
                            Else
                                fine = True
                                Exit For
                            End If
                        Next
                        If listaContratti <> "" Then
                            listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                            listaQueryInquiliniAllMesiCredito.Add("SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)<=0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)
                        End If
                        i += 1
                    End While

                    '--------------- TUTTI -----------------------------
                    For Each Items As String In listaQueryInquiliniAllMesiCredito
                        queryInquiliniALLMesiCredito &= Items & " UNION "
                    Next
                    queryInquiliniALLMesiCredito = Left(queryInquiliniALLMesiCredito, Len(queryInquiliniALLMesiCredito) - 6)
                End If

                ApriConnessione()
                'conteggio regola

                par.cmd.CommandText = queryInquiliniALLMesiCredito
                Dim LettoreCredito As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim RUcreditori As Integer = 0
                Dim RUregolari As Integer = 0
                Dim importoRUcreditori As Decimal = 0
                While LettoreCredito.Read
                    If par.IfNull(LettoreCredito("debito"), 0) <> 0 Then
                        RUcreditori += 1
                        importoRUcreditori += par.IfNull(LettoreCredito("debito"), 0)
                    Else
                        RUregolari += 1
                    End If
                End While
                LettoreCredito.Close()
                AssegnatariTotali.Text = CInt(NAssegnatariAllMesi.Text) + RUcreditori + RUregolari
                AssegnatariCreditori.Text = RUcreditori
                importoAssegnatariCreditori.Text = Format(-importoRUcreditori, "##,#0.00")
                chiudiConnessione()
                '------------------------------------------------------------------------------------------

            End If
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub
    Private Sub caricaDati3Mesi()
        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If

            Dim listaContratti As String = ""

            If Not IsNothing(ListaM) Then
                Dim queryInquilini3Mesi As String = ""
                Dim querySomme3Mesi As String = ""
                If ListaM(0) = "-1" Then
                    queryInquilini3Mesi = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL)<=3 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                    querySomme3Mesi = "SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL)<=3 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                Else
                    Dim i As Integer = 0
                    Dim fine As Boolean = False
                    While i < 40 And fine = False
                        listaContratti = ""
                        For j As Integer = 0 To 999
                            If 1000 * i + j < ListaM.Count Then
                                listaContratti &= ListaM(1000 * i + j) & ","
                            Else
                                fine = True
                                Exit For
                            End If
                        Next
                        If listaContratti <> "" Then
                            listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                            listaQueryInquilini3Mesi.Add("SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL)<=3 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)


                            listaQuerySomme3Mesi.Add("SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL)<=3 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)


                        End If
                        i += 1
                    End While

                    '--------------- 3 MESI -----------------------------

                    For Each Items As String In listaQueryInquilini3Mesi
                        queryInquilini3Mesi &= Items & " UNION "
                    Next
                    For Each Items As String In listaQuerySomme3Mesi
                        querySomme3Mesi &= Items & " UNION "
                    Next
                    queryInquilini3Mesi = Left(queryInquilini3Mesi, Len(queryInquilini3Mesi) - 6)
                    querySomme3Mesi = Left(querySomme3Mesi, Len(querySomme3Mesi) - 6)
                End If
                ApriConnessione()
                par.cmd.CommandText = querySomme3Mesi
                Dim Lettore3Mesi As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim MorositaTotale3Mesi As Decimal = 0
                Dim MorositaExGestori3Mesi As Decimal = 0
                Dim MorositaCanoni3Mesi As Decimal = 0
                Dim MorositaServizi3Mesi As Decimal = 0
                Dim MorositaALER3Mesi As Decimal = 0
                While Lettore3Mesi.Read
                    MorositaTotale3Mesi += CDec(par.IfNull(Lettore3Mesi("SALDO_TOTALE"), 0))
                    MorositaExGestori3Mesi += CDec(par.IfNull(Lettore3Mesi("SALDO_GLOBAL"), 0))
                    MorositaCanoni3Mesi += CDec(par.IfNull(Lettore3Mesi("CANONE_ALER"), 0))
                    MorositaServizi3Mesi += CDec(par.IfNull(Lettore3Mesi("SERVIZI_ALER"), 0))
                    MorositaALER3Mesi += CDec(par.IfNull(Lettore3Mesi("SALDO_ALER"), 0))
                End While
                ImportoMorositaTotale3Mesi.Text = Format(MorositaTotale3Mesi, "##,#0.00")
                ImportoMorositaExGestori3Mesi.Text = Format(MorositaExGestori3Mesi, "##,#0.00")
                ImportoMorositaCanoni3Mesi.Text = Format(MorositaCanoni3Mesi, "##,#0.00")
                ImportoMorositaServizi3Mesi.Text = Format(MorositaServizi3Mesi, "##,#0.00")
                ImportoMorositaALER3Mesi.Text = Format(MorositaALER3Mesi, "##,#0.00")
                Lettore3Mesi.Close()

                par.cmd.CommandText = queryInquilini3Mesi
                Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable As New Data.DataTable
                dataAdapter.Fill(dataTable)
                DataGridInquilini3Mesi.DataSource = dataTable
                DataGridInquilini3Mesi.DataBind()
                Dim nRUmorosi As Integer = 0
                Dim nRUmorosiMM As Integer = 0
                Dim nRUmorosiCS As Integer = 0
                Dim nRUmorosiPL As Integer = 0
                Dim nRUmorosiAltri As Integer = 0
                Dim RUmorosi3Mesi As Decimal = 0
                Dim RUmorosiMM3Mesi As Decimal = 0
                Dim RUmorosiCS3Mesi As Decimal = 0
                Dim RUmorosiPL3Mesi As Decimal = 0
                Dim RUmorosiAltri3Mesi As Decimal = 0
                Dim LettoreMorosi As Oracle.DataAccess.Client.OracleDataReader
                For Each Items As Data.DataRow In dataTable.Rows
                    nRUmorosi += 1
                    RUmorosi3Mesi += Items.Item(4)
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=" & Items.Item(1)
                    LettoreMorosi = par.cmd.ExecuteReader
                    If LettoreMorosi.Read Then
                        If par.IfNull(LettoreMorosi(0), 0) <> 0 Then
                            nRUmorosiMM += 1
                            RUmorosiMM3Mesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosi.Close()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE COD_STATO='M20'" _
                                & "AND ID_CONTRATTO = " & Items.Item(1)
                    LettoreMorosi = par.cmd.ExecuteReader
                    If LettoreMorosi.Read Then
                        If par.IfNull(LettoreMorosi(0), 0) <> 0 Then
                            nRUmorosiPL += 1
                            RUmorosiPL3Mesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosi.Close()
                Next
                chiudiConnessione()
                RUmorosiAltri3Mesi = RUmorosi3Mesi - RUmorosiMM3Mesi - RUmorosiCS3Mesi - RUmorosiPL3Mesi
                MorositaPL3Mesi.Text = Format(RUmorosiPL3Mesi, "##,#0.00")
                MorositaCS3Mesi.Text = Format(RUmorosiCS3Mesi, "##,#0.00")
                MorositaMM3Mesi.Text = Format(RUmorosiMM3Mesi, "##,#0.00")
                MorositaAltri3Mesi.Text = Format(RUmorosiAltri3Mesi, "##,#0.00")
                nRUmorosiAltri = nRUmorosi - nRUmorosiMM - nRUmorosiCS - nRUmorosiPL
                NAssegnatari3Mesi.Text = nRUmorosi
                NAssegnatariMM3Mesi.Text = nRUmorosiMM
                NAssegnatariCS3Mesi.Text = nRUmorosiCS
                NAssegnatariPL3Mesi.Text = nRUmorosiPL
                NAssegnatariAltri3Mesi.Text = nRUmorosiAltri
                '------------------------------------------------------------------------------------------
            End If

        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub
    Private Sub caricaDati312Mesi()
        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If

            Dim listaContratti As String = ""
            If Not IsNothing(ListaM) Then

                Dim queryInquilini312Mesi As String = ""
                Dim querySomme312Mesi As String = ""
                If ListaM(0) = "-1" Then
                    queryInquilini312Mesi = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 3 " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) <= 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                    querySomme312Mesi = "SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 3 " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) <= 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                Else

                    Dim i As Integer = 0
                    Dim fine As Boolean = False
                    While i < 40 And fine = False
                        listaContratti = ""
                        For j As Integer = 0 To 999
                            If 1000 * i + j < ListaM.Count Then
                                listaContratti &= ListaM(1000 * i + j) & ","
                            Else
                                fine = True
                                Exit For
                            End If
                        Next
                        If listaContratti <> "" Then
                            listaContratti = Left(listaContratti, Len(listaContratti) - 1)

                            listaQueryInquilini312Mesi.Add("SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 3 " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) <= 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)



                            listaQuerySomme312Mesi.Add("SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99'))  as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99'))  as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99'))  as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99'))  as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 3 " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) <= 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)





                        End If
                        i += 1
                    End While



                    '--------------- 3 - 12 MESI -----------------------------
                    For Each Items As String In listaQueryInquilini312Mesi
                        queryInquilini312Mesi &= Items & " UNION "
                    Next
                    For Each Items As String In listaQuerySomme312Mesi
                        querySomme312Mesi &= Items & " UNION "
                    Next
                    queryInquilini312Mesi = Left(queryInquilini312Mesi, Len(queryInquilini312Mesi) - 6)
                    querySomme312Mesi = Left(querySomme312Mesi, Len(querySomme312Mesi) - 6)
                End If

                ApriConnessione()
                par.cmd.CommandText = querySomme312Mesi
                Dim Lettore312Mesi As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim MorositaTotale312Mesi As Decimal = 0
                Dim MorositaExGestori312Mesi As Decimal = 0
                Dim MorositaCanoni312Mesi As Decimal = 0
                Dim MorositaServizi312Mesi As Decimal = 0
                Dim MorositaALER312Mesi As Decimal = 0
                While Lettore312Mesi.Read
                    MorositaTotale312Mesi += CDec(par.IfNull(Lettore312Mesi("SALDO_TOTALE"), 0))
                    MorositaExGestori312Mesi += CDec(par.IfNull(Lettore312Mesi("SALDO_GLOBAL"), 0))
                    MorositaCanoni312Mesi += CDec(par.IfNull(Lettore312Mesi("CANONE_ALER"), 0))
                    MorositaServizi312Mesi += CDec(par.IfNull(Lettore312Mesi("SERVIZI_ALER"), 0))
                    MorositaALER312Mesi += CDec(par.IfNull(Lettore312Mesi("SALDO_ALER"), 0))
                End While
                ImportoMorositaTotale312Mesi.Text = Format(MorositaTotale312Mesi, "##,#0.00")
                ImportoMorositaExGestori312Mesi.Text = Format(MorositaExGestori312Mesi, "##,#0.00")
                ImportoMorositaCanoni312Mesi.Text = Format(MorositaCanoni312Mesi, "##,#0.00")
                ImportoMorositaServizi312Mesi.Text = Format(MorositaServizi312Mesi, "##,#0.00")
                ImportoMorositaALER312Mesi.Text = Format(MorositaALER312Mesi, "##,#0.00")
                Lettore312Mesi.Close()

                par.cmd.CommandText = queryInquilini312Mesi
                Dim dataAdapter312 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable312 As New Data.DataTable
                dataAdapter312.Fill(dataTable312)
                DataGridInquilini312Mesi.DataSource = dataTable312
                DataGridInquilini312Mesi.DataBind()
                Dim nRUmorosi312 As Integer = 0
                Dim nRUmorosiMM312 As Integer = 0
                Dim nRUmorosiCS312 As Integer = 0
                Dim nRUmorosiPL312 As Integer = 0
                Dim nRUmorosiAltri312 As Integer = 0
                Dim RUmorosi312Mesi As Decimal = 0
                Dim RUmorosiMM312Mesi As Decimal = 0
                Dim RUmorosiCS312Mesi As Decimal = 0
                Dim RUmorosiPL312Mesi As Decimal = 0
                Dim RUmorosiAltri312Mesi As Decimal = 0
                Dim LettoreMorosi312 As Oracle.DataAccess.Client.OracleDataReader
                For Each Items As Data.DataRow In dataTable312.Rows
                    nRUmorosi312 += 1
                    RUmorosi312Mesi += Items.Item(4)
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=" & Items.Item(1)
                    LettoreMorosi312 = par.cmd.ExecuteReader
                    If LettoreMorosi312.Read Then
                        If par.IfNull(LettoreMorosi312(0), 0) <> 0 Then
                            nRUmorosiMM312 += 1
                            RUmorosiMM312Mesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosi312.Close()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE COD_STATO='M20'" _
                                & "AND ID_CONTRATTO = " & Items.Item(1)
                    LettoreMorosi312 = par.cmd.ExecuteReader
                    If LettoreMorosi312.Read Then
                        If par.IfNull(LettoreMorosi312(0), 0) <> 0 Then
                            nRUmorosiPL312 += 1
                            RUmorosiPL312Mesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosi312.Close()
                Next
                chiudiConnessione()
                RUmorosiAltri312Mesi = RUmorosi312Mesi - RUmorosiMM312Mesi - RUmorosiCS312Mesi - RUmorosiPL312Mesi
                MorositaPL312Mesi.Text = Format(RUmorosiPL312Mesi, "##,#0.00")
                MorositaCS312Mesi.Text = Format(RUmorosiCS312Mesi, "##,#0.00")
                MorositaMM312Mesi.Text = Format(RUmorosiMM312Mesi, "##,#0.00")
                MorositaAltri312Mesi.Text = Format(RUmorosiAltri312Mesi, "##,#0.00")
                nRUmorosiAltri312 = nRUmorosi312 - nRUmorosiMM312 - nRUmorosiCS312 - nRUmorosiPL312
                NAssegnatari312Mesi.Text = nRUmorosi312
                NAssegnatariMM312Mesi.Text = nRUmorosiMM312
                NAssegnatariCS312Mesi.Text = nRUmorosiCS312
                NAssegnatariPL312Mesi.Text = nRUmorosiPL312
                NAssegnatariAltri312Mesi.Text = nRUmorosiAltri312
                '------------------------------------------------------------------------------------------
            End If
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub
    Private Sub caricaDati12Mesi()
        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If

            Dim listaContratti As String = ""
            If Not IsNothing(ListaM) Then
                Dim queryInquilini12Mesi As String = ""
                Dim querySomme12Mesi As String = ""
                If ListaM(0) = "-1" Then
                    queryInquilini12Mesi = "SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                    querySomme12Mesi = "SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " ," & ListaM(1) & " " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno _
                                    & " and rapporti_utenza.id=" & ListaM(1) & ".id "


                Else


                    Dim i As Integer = 0
                    Dim fine As Boolean = False
                    While i < 40 And fine = False
                        listaContratti = ""
                        For j As Integer = 0 To 999
                            If 1000 * i + j < ListaM.Count Then
                                listaContratti &= ListaM(1000 * i + j) & ","
                            Else
                                fine = True
                                Exit For
                            End If
                        Next
                        If listaContratti <> "" Then
                            listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                            listaQueryInquilini12Mesi.Add("SELECT /*rownum, presso_cor as intestatario,*/ " _
                                    & " NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0) as DEBITO, RAPPORTI_UTENZA.ID, " _
                                    & " trim(RAPPORTI_UTENZA.COD_CONTRATTO) as  CODICE_CONTRATTO ," _
                                    & " case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE) " _
                                    & " else  RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) end " _
                                    & " AS INTESTATARIO ," _
                                    & " TRIM (TO_CHAR( NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0),'9G999G999G999G999G990D99'))   as DEBITO2," _
                                    & " trim(RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) as ""COD_TIPOLOGIA_CONTR_LOC""," _
                                    & " substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) as ""POSIZIONE_CONTRATTO""," _
                                    & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS COD_UNITA_IMMOBILIARE ," _
                                    & " trim(UNITA_IMMOBILIARI.COD_TIPOLOGIA) as ""COD_TIPOLOGIA""," _
                                    & " trim(INDIRIZZI.DESCRIZIONE) || ' ' ||trim(INDIRIZZI.CIVICO) ||' '||" _
                                    & " (SELECT trim(NOME) as ""NOME"" from COMUNI_NAZIONI where COD=INDIRIZZI.COD_COMUNE) as INDIRIZZO," _
                                    & " (case when ANAGRAFICA.RAGIONE_SOCIALE is not null " _
                                    & " then trim(RAGIONE_SOCIALE) " _
                                    & " else RTRIM(LTRIM(ANAGRAFICA.COGNOME||' '||ANAGRAFICA.NOME)) end) as  INTESTATARIO2 " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)

                            listaQuerySomme12Mesi.Add("SELECT " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_TOTALE, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)),'999g999g999g990d99')) as SALDO_GLOBAL, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)),'999g999g999g990d99')) as SALDO_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.CANONE_ALER,0)),'999g999g999g990d99')) as CANONE_ALER, " _
                                    & " trim(to_char(SUM(NVL(SISCOM_MI.SALDI_MESE.SERVIZI_ALER,0)),'999g999g999g990d99')) as SERVIZI_ALER " _
                                    & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                                    & " SISCOM_MI.RAPPORTI_UTENZA, " _
                                    & " SISCOM_MI.ANAGRAFICA, " _
                                    & " SISCOM_MI.INDIRIZZI, " _
                                    & " SISCOM_MI.EDIFICI, " _
                                    & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                                    & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                                    & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                                    & " SISCOM_MI.SALDI_MESE " _
                                    & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                                    & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+)=UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                                    & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                                    & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                                    & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                                    & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                                    & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                                    & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                                    & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                                    & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                                    & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                                    & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                                    & " AND (NUM_BOLLETTE_ALER+NUM_BOLLETTE_GLOBAL) > 12 " _
                                    & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                                    & " AND MESE=" & mese & " AND ANNO=" & anno)

                        End If
                        i += 1
                    End While

                    '--------------- 12 MESI -----------------------------
                    For Each Items As String In listaQueryInquilini12Mesi
                        queryInquilini12Mesi &= Items & " UNION "
                    Next
                    For Each Items As String In listaQuerySomme12Mesi
                        querySomme12Mesi &= Items & " UNION "
                    Next
                    queryInquilini12Mesi = Left(queryInquilini12Mesi, Len(queryInquilini12Mesi) - 6)
                    querySomme12Mesi = Left(querySomme12Mesi, Len(querySomme12Mesi) - 6)
                End If
                ApriConnessione()
                par.cmd.CommandText = querySomme12Mesi
                Dim Lettore12Mesi As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim MorositaTotale12Mesi As Decimal = 0
                Dim MorositaExGestori12Mesi As Decimal = 0
                Dim MorositaCanoni12Mesi As Decimal = 0
                Dim MorositaServizi12Mesi As Decimal = 0
                Dim MorositaALER12Mesi As Decimal = 0
                While Lettore12Mesi.Read
                    MorositaTotale12Mesi += CDec(par.IfNull(Lettore12Mesi("SALDO_TOTALE"), 0))
                    MorositaExGestori12Mesi += CDec(par.IfNull(Lettore12Mesi("SALDO_GLOBAL"), 0))
                    MorositaCanoni12Mesi += CDec(par.IfNull(Lettore12Mesi("CANONE_ALER"), 0))
                    MorositaServizi12Mesi += CDec(par.IfNull(Lettore12Mesi("SERVIZI_ALER"), 0))
                    MorositaALER12Mesi += CDec(par.IfNull(Lettore12Mesi("SALDO_ALER"), 0))
                End While
                ImportoMorositaTotale12Mesi.Text = Format(MorositaTotale12Mesi, "##,#0.00")
                ImportoMorositaExGestori12Mesi.Text = Format(MorositaExGestori12Mesi, "##,#0.00")
                ImportoMorositaCanoni12Mesi.Text = Format(MorositaCanoni12Mesi, "##,#0.00")
                ImportoMorositaServizi12Mesi.Text = Format(MorositaServizi12Mesi, "##,#0.00")
                ImportoMorositaALER12Mesi.Text = Format(MorositaALER12Mesi, "##,#0.00")
                Lettore12Mesi.Close()

                par.cmd.CommandText = queryInquilini12Mesi
                Dim dataAdapter12 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable12 As New Data.DataTable
                dataAdapter12.Fill(dataTable12)
                DataGridInquilini12Mesi.DataSource = dataTable12
                DataGridInquilini12Mesi.DataBind()
                Dim nRUmorosi12 As Integer = 0
                Dim nRUmorosiMM12 As Integer = 0
                Dim nRUmorosiCS12 As Integer = 0
                Dim nRUmorosiPL12 As Integer = 0
                Dim nRUmorosiAltri12 As Integer = 0
                Dim RUmorosi12Mesi As Decimal = 0
                Dim RUmorosiMM12Mesi As Decimal = 0
                Dim RUmorosiCS12Mesi As Decimal = 0
                Dim RUmorosiPL12Mesi As Decimal = 0
                Dim RUmorosiAltri12Mesi As Decimal = 0
                Dim LettoreMorosi12 As Oracle.DataAccess.Client.OracleDataReader
                For Each Items As Data.DataRow In dataTable12.Rows
                    nRUmorosi12 += 1
                    RUmorosi12Mesi += Items.Item(4)
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=" & Items.Item(1)
                    LettoreMorosi12 = par.cmd.ExecuteReader
                    If LettoreMorosi12.Read Then
                        If par.IfNull(LettoreMorosi12(0), 0) <> 0 Then
                            nRUmorosiMM12 += 1
                            RUmorosiMM12Mesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosi12.Close()
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MOROSITA_LETTERE WHERE COD_STATO='M20'" _
                                & "AND ID_CONTRATTO = " & Items.Item(1)
                    LettoreMorosi12 = par.cmd.ExecuteReader
                    If LettoreMorosi12.Read Then
                        If par.IfNull(LettoreMorosi12(0), 0) <> 0 Then
                            nRUmorosiPL12 += 1
                            RUmorosiPL12Mesi += CDec(Items.Item(4))
                        End If
                    End If
                    LettoreMorosi12.Close()
                Next
                chiudiConnessione()
                RUmorosiAltri12Mesi = RUmorosi12Mesi - RUmorosiMM12Mesi - RUmorosiCS12Mesi - RUmorosiPL12Mesi
                MorositaPL12Mesi.Text = Format(RUmorosiPL12Mesi, "##,#0.00")
                MorositaCS12Mesi.Text = Format(RUmorosiCS12Mesi, "##,#0.00")
                MorositaMM12Mesi.Text = Format(RUmorosiMM12Mesi, "##,#0.00")
                MorositaAltri12Mesi.Text = Format(RUmorosiAltri12Mesi, "##,#0.00")
                nRUmorosiAltri12 = nRUmorosi12 - nRUmorosiMM12 - nRUmorosiCS12 - nRUmorosiPL12
                NAssegnatari12Mesi.Text = nRUmorosi12
                NAssegnatariMM12Mesi.Text = nRUmorosiMM12
                NAssegnatariCS12Mesi.Text = nRUmorosiCS12
                NAssegnatariPL12Mesi.Text = nRUmorosiPL12
                NAssegnatariAltri12Mesi.Text = nRUmorosiAltri12
                '------------------------------------------------------------------------------------------
            End If
        Catch ex As Exception
            chiudiConnessione()
        End Try
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
            par.cmd.Dispose()
        End If
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Protected Sub DataGridInquilini3Mesi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridInquilini3Mesi.PageIndexChanged
        DataGridInquilini3Mesi.CurrentPageIndex = e.NewPageIndex
        caricaDati3Mesi()
    End Sub
    Protected Sub DataGridInquilini312Mesi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridInquilini312Mesi.PageIndexChanged
        DataGridInquilini312Mesi.CurrentPageIndex = e.NewPageIndex
        caricaDati312Mesi()
    End Sub
    Protected Sub DataGridInquilini12Mesi_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridInquilini12Mesi.PageIndexChanged
        DataGridInquilini12Mesi.CurrentPageIndex = e.NewPageIndex
        caricaDati12Mesi()
    End Sub

    Private Sub pannelliVisibili()
        Select Case Request.QueryString("t")
            Case "1"
                riepilogoBollette.Visible = False
                panelinquilini312mesi.Visible = False
                panelinquilini12mesi.Visible = False
                panelinquilini3mesi.Visible = False
            Case "2"
                riepilogoBollette.Visible = False
            Case "3"
                riepilogoBollette.Visible = False
                panelinquilini312mesi.Visible = False
                panelinquilini12mesi.Visible = False
                panelinquilini3mesi.Visible = False
                panelmorosita12mesi.Visible = False
                panelmorosita312mesi.Visible = False
                panelmorosita3mesi.Visible = False
        End Select

    End Sub
    Protected Sub btnExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExcel.Click
        'Dim Loading As String = "<div id=""divLoading5"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
        '   & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
        '   & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
        '   & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
        '   & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
        '   & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
        '   & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
        '   & "</td></tr></table></div></div>"
        'Response.Write(Loading)
        'Response.Flush()

        Try
            Dim mese As Integer
            Dim anno As Integer
            If Not IsNothing(Request.QueryString("d")) And Request.QueryString("d") <> "" Then
                mese = CInt(Mid(Request.QueryString("d"), 5, 2))
                anno = CInt(Left(Request.QueryString("d"), 4))
            Else
                mese = Now.Month
                anno = Now.Year
            End If

            Dim listaContratti As String = ""
            If Not IsNothing(ListaM) Then
                Dim i As Integer = 0
                Dim fine As Boolean = False
                While i < 40 And fine = False
                    listaContratti = ""
                    For j As Integer = 0 To 999
                        If 1000 * i + j < ListaM.Count Then
                            listaContratti &= ListaM(1000 * i + j) & ","
                        Else
                            fine = True
                            Exit For
                        End If
                    Next
                    If listaContratti <> "" Then
                        listaContratti = Left(listaContratti, Len(listaContratti) - 1)
                        listaQueryInquiliniExcel.Add(" SELECT  " _
                            & " RAPPORTI_UTENZA.COD_CONTRATTO AS CODICE_CONTRATTUALE, " _
                            & " CASE WHEN ANAGRAFICA.RAGIONE_SOCIALE IS NOT NULL THEN TRIM (RAGIONE_SOCIALE) ELSE RTRIM (LTRIM (COGNOME || ' ' || ANAGRAFICA.NOME)) END AS INTESTATARIO, " _
                            & " NVL (SISCOM_MI.SALDI_MESE.SALDO_GLOBAL, 0) + NVL (SISCOM_MI.SALDI_MESE.SALDO_ALER, 0) AS DEBITO_TOTALE, " _
                            & " NVL (SISCOM_MI.SALDI_MESE.SALDO_GLOBAL, 0) AS IMPORTO_EX_GESTORI, " _
                            & " NVL (SISCOM_MI.SALDI_MESE.CANONE_ALER, 0) AS IMPORTO_ALER_CANONI, " _
                            & " NVL (SISCOM_MI.SALDI_MESE.SERVIZI_ALER, 0) AS IMPORTO_ALER_SERVIZI, " _
                            & " TRIM (RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC) AS TIPOLOGIA_RAPPORTO, " _
                            & " SUBSTR (TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE, 1, 25) AS POSIZIONE_CONTRATTUALE, " _
                            & " UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE AS UNITA_IMMOBILIARE, " _
                            & " TRIM (UNITA_IMMOBILIARI.COD_TIPOLOGIA) AS TIPO_UI, " _
                            & " TRIM (INDIRIZZI.DESCRIZIONE) || ' '  || TRIM (INDIRIZZI.CIVICO) || ' ' || (SELECT TRIM (NOME)  FROM SEPA.COMUNI_NAZIONI  WHERE COD = INDIRIZZI.COD_COMUNE) AS INDIRIZZO, " _
                            & " NVL(NUM_BOLLETTE_ALER,0)+NVL(NUM_BOLLETTE_GLOBAL,0) AS MESI_MOROSITA, " _
                            & " 'NO' AS RICHIESTA_FONDO_SOCIALE, " _
                            & " (CASE WHEN ((SELECT COUNT(ID) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_STATO='M00')>0) THEN ('SI''') ELSE ('NO') END) AS INVIATA_MM, " _
                            & " (CASE WHEN ((SELECT COUNT(ID) FROM SISCOM_MI.MOROSITA_LETTERE WHERE ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_STATO='M20')>0) THEN ('SI''') ELSE ('NO') END) AS AVVIATA_PRATICA_LEGALE " _
                            & " FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, " _
                            & " SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE, " _
                            & " SISCOM_MI.RAPPORTI_UTENZA, " _
                            & " SISCOM_MI.ANAGRAFICA, " _
                            & " SISCOM_MI.INDIRIZZI, " _
                            & " SISCOM_MI.EDIFICI, " _
                            & " SISCOM_MI.UNITA_CONTRATTUALE, " _
                            & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                            & " SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, " _
                            & " SISCOM_MI.SOGGETTI_CONTRATTUALI, " _
                            & " SISCOM_MI.SALDI_MESE " _
                            & " WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID " _
                            & " AND TIPOLOGIA_UNITA_IMMOBILIARI.COD(+) = " _
                            & " UNITA_IMMOBILIARI.COD_TIPOLOGIA " _
                            & " AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID " _
                            & " AND UNITA_IMMOBILIARI.ID_INDIRIZZO = INDIRIZZI.ID(+) " _
                            & " AND UNITA_CONTRATTUALE.ID_UNITA = UNITA_IMMOBILIARI.ID " _
                            & " AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                            & " AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                            & " AND RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR = " _
                            & " TIPOLOGIA_RAPP_CONTRATTUALE.COD " _
                            & " AND SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA = ANAGRAFICA.ID " _
                            & " AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE = 'INTE' " _
                            & " AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL " _
                            & " AND SISCOM_MI.COMPLESSI_IMMOBILIARI.ID <> 1 " _
                            & " AND SALDI_MESE.ID_CONTRATTO = RAPPORTI_UTENZA.ID " _
                            & " AND RAPPORTI_UTENZA.ID IN (" & listaContratti & ")" _
                            & " and NVL(SISCOM_MI.SALDI_MESE.SALDO_GLOBAL,0)+NVL(SISCOM_MI.SALDI_MESE.SALDO_ALER,0)>0 " _
                            & " AND MESE=" & mese & " AND ANNO=" & anno)
                    End If
                    i += 1
                End While
                Dim queryInquiliniExcel As String = ""
                For Each Items As String In listaQueryInquiliniExcel
                    queryInquiliniExcel &= Items & " UNION "
                Next
                queryInquiliniExcel = Left(queryInquiliniExcel, Len(queryInquiliniExcel) - 6)
                ApriConnessione()
                par.cmd.CommandText = queryInquiliniExcel
                Dim dataAdapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dataTable As New Data.DataTable
                dataAdapter.Fill(dataTable)
                chiudiConnessione()

                '#### EXPORT IN EXCEL ####
                Try
                    Dim myExcelFile As New CM.ExcelFile
                    Dim K As Long
                    Dim sNomeFile As String
                    Dim row As System.Data.DataRow
                    sNomeFile = "MOROSITA_" & Format(Now, "yyyyMMddHHmmss")
                    Dim nRighe As Integer = dataTable.Rows.Count
                    With myExcelFile
                        .CreateFile(Server.MapPath("..\FileTemp\" & sNomeFile & ".xls"))
                        .PrintGridLines = False
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                        .SetDefaultRowHeight(14)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                        .SetFont("Arial", 12, CM.ExcelFile.FontFormatting.xlsBold)
                        .SetColumnWidth(1, 15, 30)
                        K = 1
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, "CODICE CONTRATTO", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, "INTESTATARIO", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, "DEBITO TOTALE", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, "IMPORTO EX GESTORI", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, "IMPORTO GESTORE PER CANONI", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "IMPORTO GESTORE PER SERVIZI", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, "TIPOLOGIA RAPPORTO", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, "POSIZIONE CONTRATTUALE", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, "UNITA' IMMOBILIARE", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, "TIPO UI", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, "INDIRIZZO COMPLETO", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, "MESI MOROSITA'", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, "RICHIESTA FONDO SOCIALE", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 14, "INVIATA MESSA IN MORA", 0)
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 15, "AVVIATA PRATICA LEGALE", 0)
                        K += 1
                        For Each row In dataTable.Rows
                            For index As Integer = 0 To 14
                                If index = 2 Or index = 3 Or index = 4 Or index = 5 Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, index + 1, par.IfNull(row.Item(index), 0), 4)
                                ElseIf index = 11 Then
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, index + 1, par.IfNull(row.Item(index), 0), 1)
                                Else
                                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign + CM.ExcelFile.CellAlignment.xlsBottomBorder + CM.ExcelFile.CellAlignment.xlsLeftBorder + CM.ExcelFile.CellAlignment.xlsRightBorder + CM.ExcelFile.CellAlignment.xlsTopBorder, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, index + 1, par.IfNull(row.Item(index), ""), 0)
                                End If
                            Next
                            K += 1
                        Next
                        .CloseFile()
                    End With
                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String
                    zipfic = Server.MapPath("..\FileTemp\" & sNomeFile & ".zip")
                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    Dim strFile As String
                    strFile = Server.MapPath("..\FileTemp\" & sNomeFile & ".xls")
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
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                    File.Delete(strFile)
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "loading", "document.getElementById('divLoading5').style.visibility = 'hidden';", True)
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "var larghezza=Math.floor(screen.width/2)-100;var altezza=Math.floor(screen.height/2)-50;window.open('..\/FileTemp/\" & sNomeFile & ".zip','_blank','top='+altezza+',left='+larghezza+',width=100,height=50,resizable=0');", True)
                    Response.Redirect("..\/FileTemp/\" & sNomeFile & ".zip", True)
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "window.open('..\/FileTemp/\" & sNomeFile & ".zip','_self','');", True)


                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "loading", "document.getElementById('divLoading5').style.visibility = 'hidden';", True)
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "var larghezza=Math.floor(screen.width/2)-100;var altezza=Math.floor(screen.height/2)-50;window.open('..\/FileTemp/\" & sNomeFile & ".zip','_blank','top='+altezza+',left='+larghezza+',width=100,height=50,resizable=0');", True)
                Catch ex As Exception
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "alert('" & ex.Message & "');", True)
                End Try
            End If
        Catch ex As Exception
            chiudiConnessione()
        End Try
    End Sub

    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "redirect", "location.href='RicercaDebitoriMultiSelezione.aspx';", True)
    End Sub
    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Try
            Dim Html As String = ""
            Dim stringWriter As New System.IO.StringWriter
            Dim sourcecode As New HtmlTextWriter(stringWriter)
            stringWriter = New System.IO.StringWriter
            sourcecode = New HtmlTextWriter(stringWriter)
            PanelTotale.RenderControl(sourcecode)
            sourcecode.Flush()
            Html = Html & stringWriter.ToString

            'modifica 20/09/2012
            'modifica creata per eliminare il pagebreak quando è presente una volta sola...
            Dim index As Integer = Html.IndexOf("<p style='page-break-after: always'>&nbsp;</p>")
            Dim html2 As String = Html.Substring(index + 10)
            index = html2.IndexOf("<p style='page-break-after: always'>&nbsp;</p>")
            If index = -1 Then
                Html = Replace(Html, "<p style='page-break-after: always'>&nbsp;</p>", "")
            End If
            '---------------------------------------------------------------------------------


            Dim url As String = Server.MapPath("~\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If
            pdfConverter1.PageWidth = 1200
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = True
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 15
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True
            pdfConverter1.PdfHeaderOptions.HeaderHeight = 60
            pdfConverter1.PdfHeaderOptions.HeaderText = UCase("Analisi statistica del credito")
            pdfConverter1.PdfHeaderOptions.HeaderTextFontName = "Arial"
            pdfConverter1.PdfHeaderOptions.HeaderTextFontSize = 14
            pdfConverter1.PdfHeaderOptions.HeaderTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontType = PdfFontType.HelveticaBold
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextFontSize = 10
            pdfConverter1.PdfHeaderOptions.HeaderBackColor = Drawing.Color.WhiteSmoke
            pdfConverter1.PdfHeaderOptions.HeaderTextColor = Drawing.Color.Blue
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleText = ""
            pdfConverter1.PdfHeaderOptions.HeaderSubtitleTextColor = Drawing.Color.Red
            'pdfConverter1.PdfFooterOptions.FooterText = UCase(footer)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False
            Dim nomefile As String = "StampaDebitori" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFile(Html, url & nomefile)
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "windowopen", "window.open('..\/FileTemp/\" & nomefile & "','','')", True)
        Catch ex As Exception
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('" & ex.Message & "');", True)
        End Try
    End Sub

    'Private Sub caricaMorosita()
    '    labelMorositaExGestori.Text = Format(CDec(ImportoMorositaExGestori3Mesi.Text) + CDec(ImportoMorositaExGestori12Mesi.Text) + CDec(ImportoMorositaExGestori312Mesi.Text), "##,#0.00")
    '    labelMorositaCanoni.Text = Format(CDec(ImportoMorositaCanoni3Mesi.Text) + CDec(ImportoMorositaCanoni12Mesi.Text) + CDec(ImportoMorositaCanoni312Mesi.Text), "##,#0.00")
    '    labelMorositaServizi.Text = Format(CDec(ImportoMorositaServizi3Mesi.Text) + CDec(ImportoMorositaServizi12Mesi.Text) + CDec(ImportoMorositaServizi312Mesi.Text), "##,#0.00")
    '    labelSaldoContabileTotale.Text = Format(CDec(ImportoMorositaTotale3Mesi.Text) + CDec(ImportoMorositaTotale12Mesi.Text) + CDec(ImportoMorositaTotale312Mesi.Text), "##,#0.00")
    '    labelMorositaAssegnatariMM.Text = Format(CDec(MorositaMM3Mesi.Text) + CDec(MorositaMM12Mesi.Text) + CDec(MorositaMM312Mesi.Text), "##,#0.00")
    '    labelMorositaContributoSociale.Text = Format(CDec(MorositaCS3Mesi.Text) + CDec(MorositaCS12Mesi.Text) + CDec(MorositaCS312Mesi.Text), "##,#0.00")
    '    labelMorositaAssegnatariPraticheLegali.Text = Format(CDec(MorositaPL3Mesi.Text) + CDec(MorositaPL312Mesi.Text) + CDec(MorositaPL12Mesi.Text), "##,#0.00")
    '    labelMorositaAltriAssegnatari.Text = Format(CDec(MorositaAltri3Mesi.Text) + CDec(MorositaAltri12Mesi.Text) + CDec(MorositaAltri312Mesi.Text), "##,#0.00")
    'End Sub

    Private Sub eliminaTabellaTemporanea()
        Try
            ApriConnessione()
            par.cmd.CommandText = "drop table " & ListaM(1) & " purge "
            par.cmd.ExecuteNonQuery()
            chiudiConnessione()
        Catch ex As Exception
            chiudiConnessione()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore nella ricerca!');location.href='RicercaDebitoriMultiSelezione.aspx';", True)
        End Try
    End Sub

End Class
