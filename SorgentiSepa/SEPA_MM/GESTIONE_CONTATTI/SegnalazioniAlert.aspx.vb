
Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_SegnalazioniAlert
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Private Sub GESTIONE_CONTATTI_SegnalazioniAlert_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
                    HiddenNoMenu.Value = 1
                    NoMenu()

            End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Ricerca Segnalazioni - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub NoMenu()
        Try
            CType(Me.Master.FindControl("NavigationMenu"), System.Web.UI.WebControls.Menu).Visible = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Segnalazione - NoMenu - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub dgvSegnalazioni_PreRender(sender As Object, e As EventArgs) Handles dgvSegnalazioni.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Sub dgvSegnalazioni_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgvSegnalazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            If dataItem("TIPOLOGIA").Text = "1" Then
                Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                    Case "1"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "2"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "3"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "4"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "0"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""../CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case Else
                End Select
                CType(dataItem("DESCRIZIONE"), TableCell).ToolTip = CType(dataItem("DESCRIZIONE"), TableCell).Text
                If Trim(Len(CType(dataItem("DESCRIZIONE"), TableCell).Text)) > 50 Then
                    CType(dataItem("DESCRIZIONE"), TableCell).Text = Mid(CType(dataItem("DESCRIZIONE"), TableCell).Text, 1, 50) & "..."
                End If
            End If

        End If
    End Sub
    Private Sub dgvSegnalazioni_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvSegnalazioni.NeedDataSource
        Try
            Dim Query As String = EsportaQuerySegnalazioni()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            dgvSegnalazioni.DataSource = dt
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - SegnalazioniCanone - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Function EsportaQuerySegnalazioni() As String
        Dim filtroDL As String = ""
        If Session.Item("FL_SUPERDIRETTORE") <> "1" And Session.Item("FL_FQM") <> "1" Then
            filtroDL = " AND ID_PROGRAMMA_ATTIVITA IN (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO = '30000000' AND ID_OPERATORE = " & Session.Item("ID_OPERATORE") & "))"
        End If
        EsportaQuerySegnalazioni = " SELECT SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (SELECT MAX (COD_CONTRATTO) " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                            & " WHERE ID IN " _
                            & " (SELECT ID_CONTRATTO " _
                            & " FROM SISCOM_MI. " _
                            & " UNITA_CONTRATTUALE " _
                            & " WHERE UNITA_CONTRATTUALE. " _
                            & " ID_UNITA = " _
                            & " SEGNALAZIONI.ID_UNITA) " _
                            & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                            & " 1, " _
                            & " 8) " _
                            & "  BETWEEN NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_DECORRENZA, " _
                            & " '10000000') " _
                            & " AND NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_RICONSEGNA, " _
                            & " '30000000')) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & ",ID_sEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA, SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPOLOGIA,(CASE WHEN (SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0)>0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI_PRESENTI " _
                            & " FROM siscom_mi.tab_stati_segnalazioni, " _
                            & " siscom_mi.segnalazioni, " _
                            & " siscom_mi.tab_filiali, " _
                            & " siscom_mi.edifici, " _
                            & " siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.TIPOLOGIE_GUASTI, " _
                            & " siscom_mi.combinazione_tipologie, " _
                            & " OPERATORI " _
                            & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & " AND segnalazioni.id_stato  in (0,6,7) " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & " AND id_Segnalazione_padre is null  /*AND ID_TIPO_SEGNALAZIONE = 1*/ " _
                            & " and combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                            & " and segnalazioni.id_tipologia_manutenzione<>combinazione_tipologie.id_tipo_manutenzione " _
                            & " and segnalazioni.id_tipologia_manutenzione is not null and segnalazioni.fl_tipologia_confermata = 0 " _
                            & " and segnalazioni.FL_RICH_MOD_TIPOLOGIA = 1 " _
                            & filtroDL _
                            & "  union " _
                            & " SELECT SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                             & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=SEGNALAZIONI.ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (SELECT MAX (COD_CONTRATTO) " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                            & " WHERE ID IN " _
                            & " (SELECT ID_CONTRATTO " _
                            & " FROM SISCOM_MI. " _
                            & " UNITA_CONTRATTUALE " _
                            & " WHERE UNITA_CONTRATTUALE. " _
                            & " ID_UNITA = " _
                            & " SEGNALAZIONI.ID_UNITA) " _
                            & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                            & " 1, " _
                            & " 8) " _
                            & "  BETWEEN NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_DECORRENZA, " _
                            & " '10000000') " _
                            & " AND NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_RICONSEGNA, " _
                            & " '30000000')) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & " ,ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE" _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA,SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPOLOGIA,(CASE WHEN (SELECT COUNT(*) FROM SISCOM_MI.ALLEGATI_WS WHERE ID_OGGETTO = SEGNALAZIONI.ID AND STATO = 0)>0 THEN 'SÌ' ELSE 'NO' END) AS ALLEGATI_PRESENTI " _
                            & " FROM siscom_mi.tab_stati_segnalazioni, " _
                            & " siscom_mi.segnalazioni, " _
                            & " siscom_mi.tab_filiali, " _
                            & " siscom_mi.edifici, " _
                            & " siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.TIPOLOGIE_GUASTI, " _
                            & " siscom_mi.combinazione_tipologie, " _
                            & " OPERATORI " _
                            & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & " /*AND segnalazioni.id_stato in (0,6,7)*/ " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONE_PADRE " _
                            & " FROM SISCOM_MI.SEGNALAZIONI " _
                            & " ,siscom_mi.tab_filiali " _
                            & " WHERE ID_SEGNALAZIONE_PADRE IS NOT NULL " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & ") /*AND ID_TIPO_SEGNALAZIONE = 1*/ " _
                            & " and combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                            & " and segnalazioni.id_tipologia_manutenzione<>combinazione_tipologie.id_tipo_manutenzione " _
                            & " and segnalazioni.FL_RICH_MOD_TIPOLOGIA = 1 " _
                            & " and segnalazioni.id_tipologia_manutenzione is not null and segnalazioni.fl_tipologia_confermata = 0 " _
                            & filtroDL _



        'EsportaQuery = "Select ID FROM SISCOM_MI.SEGNALAZIONI " _
        '    & " WHERE siscom_mi.GETBUILDINGMANAGERSEGNALAZIONI(segnalazioni.id,1) = '" & Session.Item("ID_OPERATORE") & "'" _
        '    & " AND ID_TIPO_SEGNALAZIONE = 1 AND ID_STATO IN (0,6,7)"
    End Function

End Class
