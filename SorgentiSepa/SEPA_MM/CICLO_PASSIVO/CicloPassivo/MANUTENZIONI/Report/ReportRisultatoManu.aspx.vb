'*** STAMPA RISULTATO RICERCA MANUTENZIONE

Partial Class ReportRisultatoManu
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreStruttura As String
    Public sValoreLotto As String

    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreServizio As String
    Public sValoreBP As String
    Public sValoreUnita As String
    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreProvenienza As String

    Public sValoreStato As String

    Dim sOrdinamento As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Try
                lblTitolo.Text = "ELENCO MANUTENZIONI"

                'Passato = Request.QueryString("Pas")

                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreStruttura = Strings.Trim(Request.QueryString("FI"))
                sValoreLotto = Strings.Trim(Request.QueryString("LO"))

                sValoreComplesso = Strings.Trim(Request.QueryString("CO"))
                sValoreEdificio = Strings.Trim(Request.QueryString("ED"))
                sValoreServizio = Strings.Trim(Request.QueryString("SE"))

                sValoreFornitore = Strings.Trim(Request.QueryString("FO"))
                sValoreAppalto = Strings.Trim(Request.QueryString("AP"))

                sValoreBP = Strings.Trim(Request.QueryString("BP"))

                sValoreUnita = Strings.Trim(Request.QueryString("UI"))

                sValoreData_Dal = Request.QueryString("DAL")
                sValoreData_Al = Request.QueryString("AL")

                sValoreStato = Request.QueryString("ST")
                sValoreProvenienza = Request.QueryString("PROVENIENZA")

                sOrdinamento = Request.QueryString("ORD")

                'Dim sFiliale As String = ""
                'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                'End If

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

                sStringaSql = ""

                If par.IfEmpty(sValoreEdificio, "-1") = "-1" Then
                    sStringaSql = "select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                        & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                        & "trim(SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE) as ""UBICAZIONE""," _
                                        & "TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI.IMPORTO_TOT,'9G999G999G999G999G990D99')) AS ""IMPORTO_TOT"", " _
                                        & "trim(SISCOM_MI.TAB_SERVIZI.DESCRIZIONE) as ""SERVIZIO""," _
                                        & "trim(SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE) as ""SERVIZIO_VOCI""," _
                                        & "trim(SISCOM_MI.PF_VOCI.CODICE) as ""CODICE_BP""," _
                                        & "trim(SISCOM_MI.PF_VOCI.DESCRIZIONE) as ""VOCE_BP""," _
                                        & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                        & "  then FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE else FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.COGNOME|| ' ' ||FORNITORI.NOME end as ""FORNITORE""," _
                                        & "SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO"", MANUTENZIONI.ID_SEGNALAZIONI AS NUM_SEGN," _
                                        & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL""," _
                                        & "trim(SISCOM_MI.TAB_FILIALI.NOME) as ""STRUTTURA_ALER""" _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_FILIALI" _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE is null " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                            & " and SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                            & " and SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                            & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID (+) " _
                            & " and SISCOM_MI.MANUTENZIONI.STATO<6 "

                    If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
                    End If


                    If IsNothing(sValoreStruttura) = False And sValoreStruttura <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=" & sValoreStruttura
                    ElseIf par.IfEmpty(sValoreLotto, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_STRUTTURA in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & sValoreLotto & ") "
                    End If

                    If par.IfEmpty(sValoreBP, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=" & sValoreBP
                    End If

                    If par.IfEmpty(sValoreComplesso, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=" & sValoreComplesso
                    End If

                    If par.IfEmpty(sValoreServizio, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_SERVIZIO='" & sValoreServizio & "' "
                    End If

                    If par.IfEmpty(sValoreStato, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.STATO=" & sValoreStato
                    End If

                    If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                        If par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                        Else
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & sValoreFornitore & ")"
                        End If
                    ElseIf par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                    End If

                    If sValoreData_Dal <> "" Then
                        If sValoreData_Al <> "" Then
                            sStringaSql = sStringaSql & " and (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                        Else
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                        End If
                    ElseIf sValoreData_Al <> "" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
                    End If

                    If sValoreProvenienza = "RICERCA_SFITTI" Then
                        If par.IfEmpty(sValoreUnita, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI=" & sValoreUnita
                        Else
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null "
                        End If
                    End If
                End If

                If par.IfEmpty(sValoreComplesso, "-1") = "-1" Or par.IfEmpty(sValoreEdificio, "-1") <> "-1" Then
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
                                        & "  then FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE else FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.COGNOME|| ' ' ||FORNITORI.NOME end as ""FORNITORE""," _
                                        & "SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO"", MANUTENZIONI.ID_SEGNALAZIONI AS NUM_SEGN," _
                                        & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as ""DATA_ODL""," _
                                        & "TRIM(SISCOM_MI.TAB_FILIALI.NOME) as ""STRUTTURA_ALER""" _
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

                    If IsNothing(sValoreStruttura) = False And sValoreStruttura <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_STRUTTURA=" & sValoreStruttura
                    ElseIf par.IfEmpty(sValoreLotto, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_STRUTTURA in (select ID_FILIALE from SISCOM_MI.LOTTI where ID=" & sValoreLotto & ") "
                    End If

                    If par.IfEmpty(sValoreBP, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.PF_VOCI_IMPORTO.ID_VOCE=" & sValoreBP
                    End If

                    If par.IfEmpty(sValoreServizio, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_SERVIZIO='" & sValoreServizio & "' "
                    End If


                    If par.IfEmpty(sValoreServizio, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.ID_SERVIZIO='" & sValoreServizio & "' "
                    End If

                    If par.IfEmpty(sValoreStato, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and  SISCOM_MI.MANUTENZIONI.STATO=" & sValoreStato
                    End If

                    If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                        If sValoreAppalto <> -1 Then
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                        Else
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & sValoreFornitore & ")"
                        End If
                    ElseIf par.IfEmpty(sValoreAppalto, "-1") <> "-1" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                    End If

                    If sValoreData_Dal <> "" Then
                        If sValoreData_Al <> "" Then
                            sStringaSql = sStringaSql & " and (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                        Else
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                        End If
                    ElseIf sValoreData_Al <> "" Then
                        sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
                    End If

                    If sValoreProvenienza = "RICERCA_SFITTI" Then
                        If par.IfEmpty(sValoreUnita, "-1") <> "-1" Then
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI=" & sValoreUnita
                        Else
                            sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_UNITA_IMMOBILIARI is not null "
                        End If
                    End If

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
