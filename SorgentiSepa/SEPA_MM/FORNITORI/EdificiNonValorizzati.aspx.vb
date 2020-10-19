
Partial Class FORNITORI_EdificiNonValorizzati
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            connData.apri()

            connData.chiudi(True)
        End If
    End Sub

    Protected Sub RadGridEdifici_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridEdifici.NeedDataSource
        Try
            connData.apri(False)
            Dim Query As String = EsportaQueryCronoprogramma(True)
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            RadGridEdifici.DataSource = dt
            'DataGridCronoprogramma.MasterTableView.Width = 200%
            connData.chiudi(False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - RadGridEdifici_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub


    Private Sub FORNITORI_PianoDettaglioCrono_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Function EsportaQuery(Optional rielaborato As Boolean = False) As String
        Dim idCronoprogramma As Integer = Request.QueryString("idcrono")
        Dim dataInizio As Integer = 0
        Dim dataFine As Integer = 0
        Dim idGruppo As Integer = 0
        Dim idAttivitaCronoprogramma As Integer = 0
        par.cmd.CommandText = "SELECT DATA_INIZIO, DATA_FINE, ID_GRUPPO, ATTIVITA_CRONOPROGRAMMA FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = " & idCronoprogramma
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            dataInizio = par.IfNull(lettore("DATA_INIZIO"), "-1")
            dataFine = par.IfNull(lettore("DATA_FINE"), "-1")
            idGruppo = par.IfNull(lettore("ID_GRUPPO"), "-1")
            idAttivitaCronoprogramma = par.IfNull(lettore("ATTIVITA_CRONOPROGRAMMA"), "-1")
        End If
        lettore.Close()
        Dim stringa As String = ""
        par.cmd.CommandText = "SELECT SISCOM_MI.GETDIFFDATAORA(" & dataFine & "," & dataInizio & ") FROM DUAL"
        Dim numeroDate As Integer = CInt(par.cmd.ExecuteScalar)
        Dim idPiano As String = idCronoprogramma
        For i As Integer = 1 To numeroDate + 1
            stringa &= ", TO_CHAR((SELECT wm_concat(SISCOM_MI.GETDATA (DATA)) " _
                    & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT A " _
                    & " WHERE ID_EDIFICIO = SISCOM_MI.PROGRAMMA_ATTIVITA_DETT.ID_EDIFICIO " _
                    & " AND INDICE = " & i _
                    & " AND FL_CANCELLATO = 0 " _
                    & " AND A.ID_PROGRAMMA_ATTIVITA =PROGRAMMA_ATTIVITA_DETT.ID_PROGRAMMA_ATTIVITA)) AS ""DATA " & i & """"
            'stringa &= ",(SELECT SISCOM_MI.GETDATA(DATA) FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE ID_PROGRAMMA_ATTIVITA=" & idPiano & " AND INDICE=" & i & " AND ID_EDIFICIO=EDIFICI.ID AND FL_CANCELLATO = 0) AS ""DATA " & i & """"

        Next
        EsportaQuery = "SELECT DISTINCT " _
                & " ID_PROGRAMMA_ATTIVITA AS ""CRONOPROGRAMMA"", " _
                & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID=ID_PROGRAMMA_ATTIVITA)) AS FORNITORE," _
                & " (SELECT COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE ID = ID_EDIFICIO) AS ""CODICE EDIFICIO"", (SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = ID_EDIFICIO) AS EDIFICIO " _
                & stringa _
                & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                & " WHERE     ID_PROGRAMMA_ATTIVITA <> " & idCronoprogramma _
                & " AND ID_EDIFICIO IN (SELECT ID_EDIFICIO FROM SISCOM_MI.EDIFICI_APPROVATI where id_programma_attivita_sel = " & idPiano & ") " _
                & "AND DATA BETWEEN " & dataInizio & " AND " & dataFine _
                & " ORDER BY 3"
    End Function

    Private Function EsportaQueryCronoprogramma(Optional rielaborato As Boolean = False) As String
        Dim idCronoprogramma As Integer = Request.QueryString("idcrono")
        Dim dataInizio As Integer = 0
        Dim dataFine As Integer = 0
        Dim idGruppo As Integer = 0
        Dim idAttivitaCronoprogramma As Integer = 0
        par.cmd.CommandText = "SELECT DATA_INIZIO, DATA_FINE, ID_GRUPPO, ATTIVITA_CRONOPROGRAMMA FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = " & idCronoprogramma
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            dataInizio = par.IfNull(lettore("DATA_INIZIO"), "-1")
            dataFine = par.IfNull(lettore("DATA_FINE"), "-1")
            idGruppo = par.IfNull(lettore("ID_GRUPPO"), "-1")
            idAttivitaCronoprogramma = par.IfNull(lettore("ATTIVITA_CRONOPROGRAMMA"), "-1")
        End If
        lettore.Close()


        Dim stringa As String = ""
        par.cmd.CommandText = "SELECT SISCOM_MI.GETDIFFDATAORA(" & dataFine & "," & dataInizio & ") FROM DUAL"
        Dim numeroDate As Integer = CInt(par.cmd.ExecuteScalar)
        Dim idPiano As String = idCronoprogramma



        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
        Dim condizioneStruttura As String = ""
        Dim condizioneVista As String = ""
        If idOperatore <> "1" Then

            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                condizioneStruttura = " and EDIFICI.ID (select id from siscom_mi.edifici where " _
                        & " EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")) "
            Else
                'CONDIZIONE PER ESCLUDERE LA VISIONE
                condizioneStruttura = " and EDIFICI.ID in (select id from siscom_mi.edifici where " _
                        & " EDIFICI.ID_COMPLESSO=0 ) "
            End If
            If Session.Item("FL_SUPERDIRETTORE") = "0" Then
                Dim tipologia As String = Request.QueryString("TIPOLOGIA")
                Select Case tipologia
                    Case "BM"
                        condizioneVista = " and EDIFICI.ID  in " _
                                       & " (select edifici.id " _
                                       & " from siscom_mi.edifici,siscom_mi.BUILDING_MANAGER_OPERATORI " _
                                       & " where EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                                       & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                                       & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                                       & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                                       & " and BUILDING_MANAGER_OPERATORI.id_operatore = " & idOperatore & ")"
                    Case "FQM"
                        condizioneVista = condizioneStruttura
                    Case "TA"
                        condizioneVista = condizioneStruttura
                End Select
            End If
        End If
        Dim condizioneEdificiNonValorizzati As String = " And id_edificio Not In  (Select id_edificio from siscom_mi.programma_attivita_dett where id_programma_attivita = " & idPiano & " And fl_cancellato = 0) "

        EsportaQueryCronoprogramma = "SELECT DISTINCT " _
                & " COD_EDIFICIO AS ""CODICE EDIFICIO""," _
                & " DENOMINAZIONE " _
                & stringa _
                & " FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI " _
                & " WHERE APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & idGruppo _
                & " AND EDIFICI.ID=APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & condizioneVista _
                & condizioneEdificiNonValorizzati _
                & " ORDER BY DENOMINAZIONE"
    End Function
End Class
