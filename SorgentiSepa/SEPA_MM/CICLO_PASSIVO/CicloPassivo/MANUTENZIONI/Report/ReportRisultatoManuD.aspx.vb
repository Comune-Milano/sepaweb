'*** STAMPA RISULTATO RICERCA MANUTENZIONE

Partial Class ReportRisultatoManuD
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String


    Public sValoreEsercizioFinanziarioR As String

    Public sValoreRepertorio As String
    Public sValoreODL As String

    Public sValoreAnno As String

    Dim sOrdinamento As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO MANUTENZIONI"


                Dim sFiliale As String = "-1"
                If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                End If


                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreRepertorio = Strings.Trim(Request.QueryString("REP"))
                sValoreODL = Strings.Trim(Request.QueryString("ODL"))

                sValoreAnno = Strings.Trim(Request.QueryString("ANNO"))

                sOrdinamento = Request.QueryString("ORD")


                Select Case sOrdinamento
                    Case "UBICAZIONE"
                        sOrder = " order by UBICAZIONE asc"
                    Case "SERVIZIO"
                        sOrder = " order by SERVIZIO,SERVIZIO_VOCI,UBICAZIONE"
                    Case "DATA ODL"
                        sOrder = " order by DATA_ODL desc"
                    Case "NUM REPERTORIO"
                        sOrder = " order by NUM_REPERTORIO"
                    Case "FORNITORE"
                        sOrder = " order by FORNITORE,UBICAZIONE"
                    Case "STRUTTURA_ALER"
                        sOrder = " order by STRUTTURA_ALER,UBICAZIONE"
                    Case "VOCE_BP"
                        sOrder = " order by VOCE_BP,UBICAZIONE"
                    Case Else
                        sOrder = ""
                End Select

                sStringaSql = "select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                        & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                        & " trim(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE) as ""UBICAZIONE""," _
                                        & "TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI.IMPORTO_TOT,'9G999G999G999G999G990D99')) AS ""IMPORTO_TOT"", " _
                                        & " trim(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) as ""SERVIZIO""," _
                                        & " trim(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) as ""SERVIZIO_VOCI""," _
                                        & " trim(SISCOM_MI.PF_VOCI.CODICE) as ""CODICE_BP""," _
                                        & " trim(SISCOM_MI.PF_VOCI.DESCRIZIONE) as ""VOCE_BP""," _
                                        & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                        & "  then COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) else COD_FORNITORE || ' - ' || trim(FORNITORI.COGNOME)|| ' ' ||trim(FORNITORI.NOME) end as ""FORNITORE""," _
                                        & "SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO"", MANUTENZIONI.ID_SEGNALAZIONI AS NUM_SEGN," _
                                        & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL""," _
                                        & "trim(SISCOM_MI.TAB_FILIALI.NOME) as ""STRUTTURA_ALER""" _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI" _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE is null " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                            & " and SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                            & " and SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                            & " and SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                            & " and SISCOM_MI.MANUTENZIONI.STATO<6 "


                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                End If


                If sFiliale <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
                End If

                If par.IfEmpty(sValoreRepertorio, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.APPALTI.NUM_REPERTORIO LIKE '" & par.PulisciStrSql(sValoreRepertorio) & "'"
                End If

                If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.PROGR=" & sValoreODL
                End If

                If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ANNO=" & sValoreAnno
                End If


                If sStringaSql <> "" Then sStringaSql = sStringaSql & " union "
                sStringaSql = sStringaSql & "  select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                        & "trim(SISCOM_MI.EDIFICI.DENOMINAZIONE) AS ""UBICAZIONE""," _
                                        & "TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI.IMPORTO_TOT,'9G999G999G999G999G990D99')) AS ""IMPORTO_TOT"", " _
                                        & "trim(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) as ""SERVIZIO""," _
                                        & "trim(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) as ""SERVIZIO_VOCI""," _
                                        & "trim(SISCOM_MI.PF_VOCI.CODICE) as ""CODICE_BP""," _
                                        & "trim(SISCOM_MI.PF_VOCI.DESCRIZIONE) as ""VOCE_BP""," _
                                        & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                        & "  then COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE else COD_FORNITORE || ' - ' || FORNITORI.COGNOME|| ' ' ||FORNITORI.NOME end as ""FORNITORE""," _
                                    & "SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO"",MANUTENZIONI.ID_SEGNALAZIONI AS NUM_SEGN," _
                                        & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL""," _
                                        & "trim(SISCOM_MI.TAB_FILIALI.NOME) as ""STRUTTURA_ALER""" _
                    & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI " _
                    & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                        & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE is null " _
                        & " and SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                        & " and SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                        & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                        & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                        & " and SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                        & " and SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                        & " and SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                        & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                        & " and SISCOM_MI.MANUTENZIONI.STATO<6 "


                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                End If


                If sFiliale <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
                End If

                If par.IfEmpty(sValoreRepertorio, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.APPALTI.NUM_REPERTORIO LIKE '" & par.PulisciStrSql(sValoreRepertorio) & "'"
                End If

                If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.PROGR=" & sValoreODL
                End If

                If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ANNO=" & sValoreAnno
                End If


                sStringaSql = sStringaSql & sOrder



                par.OracleConn.Open()
                Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(sStringaSql, par.OracleConn)
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

                lblTotale.Text = "0"
                Do While myReader.Read()
                    lblTotale.Text = CInt(lblTotale.Text) + 1
                Loop

                lblTotale.Text = "TOTALE MANUTENZIONI TROVATE: " & lblTotale.Text
                myReader.Close()

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

                Dim ds As New Data.DataSet()
                da.Fill(ds)

                DataGrid1.DataSource = ds
                DataGrid1.DataBind()

                'If sValoreImpianto = "SO" Then
                '    DataGrid2.DataSource = ds
                '    DataGrid2.DataBind()
                'Else
                '    DataGrid1.DataSource = ds
                '    DataGrid1.DataBind()
                '    '*******************************
                'End If


                par.cmd.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch EX1 As Oracle.DataAccess.Client.OracleException
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            Catch ex As Exception
                par.OracleConn.Close()
                par.OracleConn.Dispose()
            End Try
        End If



    End Sub

    Public Property Passato() As String
        Get
            If Not (ViewState("par_Passato") Is Nothing) Then
                Return CStr(ViewState("par_Passato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Passato") = value
        End Set

    End Property



End Class
