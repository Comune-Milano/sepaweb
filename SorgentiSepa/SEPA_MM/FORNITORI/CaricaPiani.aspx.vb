
Partial Class FORNITORI_CaricaPiani
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            connData.apri()
            caricaProfilo()
            connData.chiudi()
            CaricaViste()
        End If
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
        If RadButtonDirettoreLavori.Visible = True Then
            RadButtonDirettoreLavori.Checked = True
        ElseIf RadButtonBuildingManager.Visible = True Then
            RadButtonBuildingManager.Checked = True
        ElseIf RadButtonFieldQualityManager.Visible = True Then
            RadButtonFieldQualityManager.Checked = True
        ElseIf RadButtonTecnicoAmministrativo.Visible = True Then
            RadButtonTecnicoAmministrativo.Checked = True
        End If
    End Sub
    Private Sub caricaProfilo()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or Session.Item("FL_SUPERDIRETTORE") = "1" Then
                'DIRETTORE LAVORI
                gestioneDirettoreLavori()
            End If
            If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                'FORNITORE ESTERNO
                gestioneFornitoreEsterno()
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CreaPiano - caricaProfilo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub gestioneFornitoreEsterno()
        par.cmd.CommandText = "SELECT MOD_FO_ID_FO FROM SEPA.OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE")
        Dim idOperatore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        idFornitore.Value = idOperatore
        idDirettoreLavori.Value = 0
    End Sub

    Private Sub gestioneDirettoreLavori()
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        idDirettoreLavori.Value = idOperatore
        idFornitore.Value = 0
        btnCreaProgramma.Visible = False
    End Sub
    Protected Sub RadGridPiani_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPiani.NeedDataSource
        Try
            Dim Query As String = EsportaQueryPiano()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            RadGridPiani.DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - RadGridPiani_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
    Private Function EsportaQueryPiano() As String
        Dim condizioneDirettoreLavori As String = ""
        Dim condizioneFornitori As String = ""

        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
        Dim condizioneStruttura As String = ""
        Dim condizioneVista As String = ""
        'If idDirettoreLavori.Value > 0 Then
        '    condizioneDirettoreLavori = " AND APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE=" & idDirettoreLavori.Value & " AND DATA_FINE_INCARICO='30000000')"
        'End If
        If idOperatore <> "1" Then
            If idFornitore.Value > 0 Then
                condizioneFornitori = " AND FORNITORI.ID=" & idFornitore.Value _
                    & " AND appalti.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.OPERATORI_FO_APPALTI WHERE ID_OPERATORE = " & Session.Item("ID_OPERATORE") & ") "
            Else
                If Session.Item("FL_SUPERDIRETTORE") = "0" Then

                    If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
                    ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                        condizioneStruttura = " and programma_attivita.id in (select distinct id_programma_attivita from siscom_mi.programma_attivita_dett where id_edificio in (select id from siscom_mi.edifici where " _
                        & " EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & "))) "
                    Else
                        'CONDIZIONE PER ESCLUDERE LA VISIONE
                        condizioneStruttura = " and programma_attivita.id in (select distinct id_programma_attivita from siscom_mi.programma_attivita_dett where id_edificio in (select id from siscom_mi.edifici where " _
                        & " EDIFICI.ID_COMPLESSO=0 )) "
                    End If

                    If RadButtonBuildingManager.Checked = True Then
                        'BUILDING MANAGER
                        condizioneVista = " and programma_attivita.id in ( select distinct id_programma_attivita from siscom_mi.programma_attivita_dett where id_edificio in " _
                                        & " (select edifici.id " _
                                        & " from siscom_mi.edifici,siscom_mi.BUILDING_MANAGER_OPERATORI " _
                                        & " where EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                                        & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                                        & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                                        & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                                        & " and BUILDING_MANAGER_OPERATORI.id_operatore = " & idOperatore & "))"
                    ElseIf RadButtonDirettoreLavori.Checked = True Then
                        'DIRETTORE LAVORI
                        'condizioneVista = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE SISCOM_MI.GETDLFROMAPPALTI(MANUTENZIONI.ID_APPALTO)='" & idOperatore & "')"
                        condizioneVista = " AND APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE=" & idDirettoreLavori.Value & " AND DATA_FINE_INCARICO='30000000')"
                    ElseIf RadButtonFieldQualityManager.Checked = True Then
                        'Coordinatore qualità
                        condizioneVista = condizioneStruttura
                    ElseIf RadButtonTecnicoAmministrativo.Checked = True Then
                        'TECNICO AMMINISTRATIVO
                        condizioneVista = condizioneStruttura
                    End If
                Else
                    RadButtonBuildingManager.Visible = False
                    RadButtonDirettoreLavori.Visible = False
                    RadButtonTecnicoAmministrativo.Visible = False
                    RadButtonFieldQualityManager.Visible = False
                End If
            End If
        End If

        Dim stringa As String = " SELECT  " _
            & " PROGRAMMA_aTTIVITA.ID,  " _
            & " PROGRAMMA_aTTIVITA.ID_STATO,  " _
            & " RAGIONE_SOCIALE AS FORNITORE, " _
            & " NUM_REPERTORIO||'-'||APPALTI.DESCRIZIONE AS APPALTO,   " _
            & " TO_DATE(DATA_INSERIMENTO,'YYYYMMDD') AS DATA_INSERIMENTO,  " _
            & " TO_DATE(DATA_ULTIMA_MODIFICA,'YYYYMMDD') AS DATA_ULTIMA_MODIFICA, " _
            & " TO_DATE(DATA_ULTIMA_APPROVAZIONE,'YYYYMMDD') AS DATA_ULTIMA_APPROVAZIONE,  " _
            & " (CASE WHEN PROGRAMMA_ATTIVITA.ID_sTATO=0 THEN 'NON APPROVATO' ELSE 'APPROVATO' END) AS STATO, " _
            & " (SELECT UPPER(DESCRIZIONE) FROM SISCOM_MI.TAB_TIPOLOGIA_CRONOPROGRAMMA WHERE ID = ID_TIPO_CRONOPROGRAMMA) AS TIPOLOGIA_CRONOPROGRAMMA, " _
            & " (SELECT REPLACE (DESCRIZIONE, '#', '') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID = PROGRAMMA_ATTIVITA.ATTIVITA_CRONOPROGRAMMA) AS ATTIVITA_CRONOPROGRAMMA, " _
            & " TO_DATE (PROGRAMMA_ATTIVITA.DATA_INIZIO, 'YYYYMMDD') AS DATA_INIZIO, " _
            & " TO_DATE (PROGRAMMA_ATTIVITA.DATA_FINE, 'YYYYMMDD') AS DATA_FINE " _
            & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            & " WHERE " _
            & " PROGRAMMA_aTTIVITA.ID_FORNITORE=FORNITORI.ID " _
            & " AND FORNITORI.ID=APPALTI.ID_FORNITORE " _
            & " AND APPALTI.ID_GRUPPO=PROGRAMMA_ATTIVITA.ID_GRUPPO " _
            & condizioneDirettoreLavori _
            & condizioneFornitori _
            & condizioneVista _
            & " AND APPALTI.ID_GRUPPO=APPALTI.ID "
        Return stringa
    End Function

    Private Sub FORNITORI_CaricaPiani_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub RadButtonBuildingManager_Click(sender As Object, e As System.EventArgs) Handles RadButtonBuildingManager.Click
        RadGridPiani.Rebind()

    End Sub
    Protected Sub RadButtonDirettoreLavori_Click(sender As Object, e As System.EventArgs) Handles RadButtonDirettoreLavori.Click
        RadGridPiani.Rebind()

    End Sub
    Protected Sub RadButtonFieldQualityManager_Click(sender As Object, e As System.EventArgs) Handles RadButtonFieldQualityManager.Click
        RadGridPiani.Rebind()

    End Sub
    Protected Sub RadButtonTecnicoAmministrativo_Click(sender As Object, e As System.EventArgs) Handles RadButtonTecnicoAmministrativo.Click
        RadGridPiani.Rebind()

    End Sub

    Private Sub FORNITORI_CaricaPiani_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        caricaProfilo()
    End Sub
End Class
