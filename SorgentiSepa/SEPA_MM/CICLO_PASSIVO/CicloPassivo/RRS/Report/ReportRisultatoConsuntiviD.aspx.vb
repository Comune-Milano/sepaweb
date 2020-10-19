'*** STAMPA RISULTATO RICERCA MANUTENZIONE da CONSUNTIVARE

Partial Class ReportRisultatoConsuntiviD
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
                lblTitolo.Text = "ELENCO MANUTENZIONI GESTIONE NON PATRIMONIALE"

                'Passato = Request.QueryString("Pas")

                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreRepertorio = Strings.Trim(Request.QueryString("REP"))
                sValoreODL = Strings.Trim(Request.QueryString("ODL"))

                sValoreAnno = Strings.Trim(Request.QueryString("ANNO"))

                sOrdinamento = Request.QueryString("ORD")

                Select Case sOrdinamento
                    Case "UBICAZIONE"
                        sOrder = " order by UBICAZIONE asc"
                    Case "VOCE"
                        sOrder = " order by VOCE,UBICAZIONE"
                    Case "DATA ODL"
                        sOrder = " order by DATA_ODL desc"
                    Case "NUM REPERTORIO"
                        sOrder = " order by NUM_REPERTORIO"
                    Case "FORNITORE"
                        sOrder = " order by FORNITORE,UBICAZIONE"

                    Case Else
                        sOrder = ""
                End Select


                sStringaSql = " select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                    & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                    & "SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                                    & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "  then RAGIONE_SOCIALE else COGNOME || ' ' || NOME end as ""FORNITORE""," _
                                    & "SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL""" _
                            & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI" _
                            & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                            & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null " _
                            & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO IS NULL  " _
                            & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                            & "   and   SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)  " _
                            & "   and   SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                            & "   and   SISCOM_MI.MANUTENZIONI.STATO=1"


                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
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

                sStringaSql = sStringaSql & "union  select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                        & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                        & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                        & "SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                                        & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                        & "  then RAGIONE_SOCIALE else COGNOME || ' ' || NOME end as ""FORNITORE""," _
                                        & "SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL"" " _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO IS NULL  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                        & "   and   SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                        & "   and  SISCOM_MI.MANUTENZIONI.STATO=1"

                If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
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
