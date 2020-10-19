Imports Telerik.Web.UI

Partial Class Contratti_Tab_Sicurezza
    Inherits System.Web.UI.UserControl
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub RadGridInterventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridInterventi.NeedDataSource
        Try
            'par.cmd.CommandText = " select interventi_sicurezza.id as ID_INTERVENTO_SICUREZZA,unita_immobiliari.cod_unita_immobiliare," _
            '        & " getdata (data_apertura) as data_svolgimento_intervento," _
            '        & " tipo_intervento.descrizione as tipo," _
            '        & " tab_stati_interventi.descrizione as stato," _
            '        & " (select tab_stati_alloggio_arrivo.descrizione as statoallarrivo from  siscom_mi.tab_stati_alloggio_arrivo where interventi_sicurezza.id_stato_alloggio_arrivo=tab_stati_alloggio_arrivo.id) as stato_alloggio_arrivo," _
            '        & " (select tab_stati_contrattuale_nucleo.descrizione as statoallarrivo from  siscom_mi.tab_stati_contrattuale_nucleo where interventi_sicurezza.id_new_stato_contr_nucleo=tab_stati_contrattuale_nucleo.id) as nuovo_stato_nucleo," _
            '        & " (case when id_new_stato_ui=1 then 'Libero' when id_new_stato_ui=0 then 'Occupato' end) as nuovo_stato_ui," _
            '        & " (case when fl_messo_in_sicurezza=1 then 'Sì' when fl_messo_in_sicurezza=0 then 'No' end) as messo_in_sicurezza," _
            '        & " nvl( (select  (case when count(id_tipo_messo_in_sicurezza)>0 then 'Sì' end)  from siscom_mi.interventi_tipo_in_sicurezza where id_intervento =interventi_sicurezza.id and id_tipo_messo_in_sicurezza in (5,6)" _
            '        & " group by id_intervento having count(id_tipo_messo_in_sicurezza)>0) ,'No' )as sloggiato, " _
            '        & " interventi_sicurezza.assegnatario," _
            '        & " interventi_sicurezza.assegnatario_2 as ""CO-ASSEGNATARIO"" " _
            '        & " from siscom_mi.unita_immobiliari," _
            '        & " siscom_mi.indirizzi," _
            '        & " siscom_mi.interventi_sicurezza," _
            '        & " siscom_mi.tab_stati_interventi," _
            '        & " siscom_mi.tipo_intervento" _
            '        & " where indirizzi.id(+) = unita_immobiliari.id_indirizzo" _
            '        & " and interventi_sicurezza.id_unita = unita_immobiliari.id(+)" _
            '        & " and tab_stati_interventi.id(+) = interventi_sicurezza.id_stato" _
            '        & " and tipo_intervento.id(+) = interventi_sicurezza.id_tipo_intervento" _
            '        & " and interventi_sicurezza.id_stato=5 " _
            '        & " and interventi_sicurezza.id_unita =" & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value _
            '        & " order by data_apertura desc"
            par.cmd.CommandText = " SELECT interventi_sicurezza.id as ID_INTERVENTO_SICUREZZA,edifici.denominazione as edificio,tab_filiali.nome as sede_territoriale,unita_immobiliari.cod_unita_immobiliare," _
                    & "UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA,INDIRIZZI.DESCRIZIONE ||' '||INDIRIZZI.CIVICO AS INDIRIZZO, " _
                    & " INTERVENTI_SICUREZZA.ID AS NUM,TIPO_INTERVENTO.DESCRIZIONE AS TIPO, " _
                    & " TAB_STATI_INTERVENTI.DESCRIZIONE AS STATO,data_ora_inserimento,GETDATA(data_Apertura) as data_apertura, " _
                    & " (select tab_stati_alloggio_arrivo.descrizione as statoallarrivo from  siscom_mi.tab_stati_alloggio_arrivo where interventi_sicurezza.id_stato_alloggio_arrivo=tab_stati_alloggio_arrivo.id) as stato_alloggio_arrivo," _
                    & " (select tab_stati_contrattuale_nucleo.descrizione as statoallarrivo from  siscom_mi.tab_stati_contrattuale_nucleo where interventi_sicurezza.id_new_stato_contr_nucleo=tab_stati_contrattuale_nucleo.id) as nuovo_stato_nucleo," _
                    & " (case when id_new_stato_ui=1 then 'Libero' when id_new_stato_ui=0 then 'Occupato' end) as nuovo_stato_ui," _
                    & " (case when fl_messo_in_sicurezza=1 then 'Sì' when fl_messo_in_sicurezza=0 then 'No' end) as messo_in_sicurezza," _
                    & " TO_CHAR(TO_DATE(SUBSTR(data_ora_inserimento,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ORA_INSERIM,INTERVENTI_SICUREZZA.ASSEGNATARIO,INTERVENTI_SICUREZZA.ASSEGNATARIO_2 " _
                    & " FROM siscom_mi.complessi_immobiliari,siscom_mi.edifici,siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO,siscom_mi.INDIRIZZI,SISCOM_MI.INTERVENTI_SICUREZZA,SISCOM_MI.SEGNALAZIONI,SISCOM_MI.TAB_STATI_INTERVENTI,SISCOM_MI.TIPO_INTERVENTO,siscom_mi.TAB_FILIALI " _
                    & " WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) AND INDIRIZZI.ID(+) = UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) AND " _
                    & " INTERVENTI_SICUREZZA.ID_UNITA=UNITA_IMMOBILIARI.ID and " _
                    & " complessi_immobiliari.id=edifici.id_complesso and " _
                    & " UNITA_IMMOBILIARI.id_Edificio=edifici.id " _
                    & " AND TAB_STATI_INTERVENTI.ID(+) = INTERVENTI_SICUREZZA.ID_STATO " _
                    & " AND SEGNALAZIONI.ID(+) = INTERVENTI_SICUREZZA.ID_SEGNALAZIONE " _
                    & " AND complessi_immobiliari.id_filiale = tab_filiali.ID(+) " _
                    & " AND TIPO_INTERVENTO.ID(+)=INTERVENTI_SICUREZZA.ID_TIPO_INTERVENTO " _
                    & " and interventi_sicurezza.id_stato=5 " _
                    & " and interventi_sicurezza.id_unita =" & CType(Me.Page.FindControl("txtIdUnita"), HiddenField).Value _
                    & " order by data_apertura desc"
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)

        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridInterventi_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridInterventi_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridInterventi.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                Dim query As String = "SELECT ANAGRAFICA_SOGG_COINVOLTI.id,COD_FISC_SOGG_COINVOLTO,SESSO_SOGG_COINVOLTO, COD_LUOGO_NASCITA,(SELECT nome FROM comuni_nazioni where cod=COD_LUOGO_NASCITA) AS luogo_nasc, COD_TIPOLOGIA_OCCUPANTE, (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_OCCUPANTE_SOGG_COINV where id=cod_tipologia_occupante) AS occupante," _
                    & " COGNOME_SOGG_COINVOLTO, TO_CHAR(TO_DATE(SUBSTR(DATA_NASC_SOGG_COINVOLTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_NASC_SOGG_COINVOLTO, " _
                    & " INDIRIZZO_RESIDENZA, NOME_SOGG_COINVOLTO" _
                    & " FROM SISCOM_MI.ANAGRAFICA_SOGG_COINVOLTI,SISCOM_MI.ELENCO_SOGG_COINV_SICUREZZA WHERE " _
                    & " ANAGRAFICA_SOGG_COINVOLTI.id=ELENCO_SOGG_COINV_SICUREZZA.ID_ANAGR_SOGG_COINV and id_intervento=" & dataItem("ID_INTERVENTO_SICUREZZA").Text & " order by COGNOME_SOGG_COINVOLTO asc"
                e.DetailTableView.DataSource = GetDataTable(query)
        End Select
    End Sub

    Public Function GetDataTable(query As String) As Data.DataTable
        Dim myDataTable As New Data.DataTable
        par.cmd.CommandText = query
        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        da.Fill(myDataTable)
        Return myDataTable
    End Function

    Protected Sub RadGridInterventi_PreRender(sender As Object, e As System.EventArgs) Handles RadGridInterventi.PreRender
        Try
            HideExpandColumnRecursive(RadGridInterventi.MasterTableView)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridInterventi_PreRender - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub HideExpandColumnRecursive(ByVal tableView As GridTableView)
        Dim nestedViewItems As GridItem() = tableView.GetItems(GridItemType.NestedView)
        For Each nestedViewItem As GridNestedViewItem In nestedViewItems
            For Each nestedView As GridTableView In nestedViewItem.NestedTableViews
                Dim cell As TableCell = nestedView.ParentItem("ExpandColumn")
                If nestedView.Items.Count = 0 And cell.Controls.Count > 0 Then
                    cell.Controls(0).Visible = False
                    cell.Text = "&nbsp"
                    nestedViewItem.Visible = False
                End If
                If nestedView.HasDetailTables Then
                    HideExpandColumnRecursive(nestedView)
                End If
            Next
        Next
    End Sub

    Protected Sub RadGridSegn_DetailTableDataBind(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles RadGridSegn.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                Dim query As String = " SELECT SEGNALAZIONI.ID," _
                        & " (SELECT REPLACE (DESCRIZIONE, '#', '') AS DESCRIZIONE" _
                        & " FROM SISCOM_MI.TIPO_SEGNALAZIONE" _
                        & " WHERE TIPO_SEGNALAZIONE.ID = ID_TIPO_SEGNALAZIONE)" _
                        & " AS CATEGORIA," _
                        & " (SELECT REPLACE (DESCRIZIONE, '#', '') AS DESCRIZIONE" _
                        & " FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1" _
                        & " WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID = ID_TIPO_SEGN_LIVELLO_1)" _
                        & " AS CATEGORIA1," _
                        & " ID_PERICOLO_SEGNALAZIONE,'' AS CRITICITA," _
                        & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO," _
                        & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_APERTURA," _
                        & " (SELECT TO_CHAR (TO_DATE (SUBSTR (DATA_ORA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY')" _
                        & " FROM siscom_mi.eventi_segnalazioni" _
                        & " WHERE     id_segnalazione = segnalazioni.id" _
                        & " AND cod_Evento = 'F287'" _
                        & " AND valore_new = 'IN CORSO') AS DATA_IN_CORSO," _
                        & " (SELECT TO_CHAR (TO_DATE (SUBSTR (DATA_ORA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY')" _
                        & " FROM siscom_mi.eventi_segnalazioni" _
                        & " WHERE id_segnalazione = segnalazioni.id" _
                        & " AND cod_Evento = 'F287'" _
                        & " AND valore_new = 'EVASA') AS DATA_EVASIONE," _
                        & " SISCOM_MI.GETDATA (DATA_CHIUSURA) AS DATA_CHIUSURA," _
                        & " (SELECT SUM (SOLLECITO)" _
                        & " FROM SISCOM_MI.SEGNALAZIONI_NOTE" _
                        & " WHERE sollecito > 0" _
                        & " AND SEGNALAZIONI_NOTE.ID_SEGNALAZIONE = SEGNALAZIONI.ID) AS NUM_SOLLECITI" _
                        & " FROM siscom_mi.tab_stati_segnalazioni, siscom_mi.segnalazioni" _
                        & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato" _
                        & " AND segnalazioni.id_stato <> -1" _
                        & " AND segnalazioni.id_Segnalazione_padre = " & par.IfEmpty(dataItem("id_Segnalazione_padre").Text, 0)
                e.DetailTableView.DataSource = GetDataTable(query)
        End Select
    End Sub

    Protected Sub RadGridSegn_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridSegn.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
           
            Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                Case "1"
                    dataItem("CRITICITA").Controls.Clear()
                    Dim img As Image = New Image()
                    img.ImageUrl = "../GESTIONE_CONTATTI/Immagini/Ball-white-128.png"
                    dataItem("CRITICITA").Controls.Add(img)
                Case "2"
                    dataItem("CRITICITA").Controls.Clear()
                    Dim img As Image = New Image()
                    img.ImageUrl = "../GESTIONE_CONTATTI/Immagini/Ball-green-128.png"
                    dataItem("CRITICITA").Controls.Add(img)
                Case "3"
                    dataItem("CRITICITA").Controls.Clear()
                    Dim img As Image = New Image()
                    img.ImageUrl = "../GESTIONE_CONTATTI/Immagini/Ball-yellow-128.png"
                    dataItem("CRITICITA").Controls.Add(img)
                Case "4"
                    dataItem("CRITICITA").Controls.Clear()
                    Dim img As Image = New Image()
                    img.ImageUrl = "../GESTIONE_CONTATTI/Immagini/Ball-red-128.png"
                    dataItem("CRITICITA").Controls.Add(img)
                Case "0"
                    dataItem("CRITICITA").Controls.Clear()
                    Dim img As Image = New Image()
                    img.ImageUrl = "../GESTIONE_CONTATTI/Immagini/Ball-blue-128.png"
                    dataItem("CRITICITA").Controls.Add(img)
                Case Else
            End Select

        End If
    End Sub

    Protected Sub RadGridSegn_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridSegn.NeedDataSource
        Try
            par.cmd.CommandText = " SELECT SEGNALAZIONI.ID,ID_SEGNALAZIONE_PADRE," _
                    & " (SELECT REPLACE (DESCRIZIONE, '#', '') AS DESCRIZIONE" _
                    & " FROM SISCOM_MI.TIPO_SEGNALAZIONE" _
                    & " WHERE TIPO_SEGNALAZIONE.ID = ID_TIPO_SEGNALAZIONE)" _
                    & " AS CATEGORIA," _
                    & " (SELECT REPLACE (DESCRIZIONE, '#', '') AS DESCRIZIONE" _
                    & " FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1" _
                    & " WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID = ID_TIPO_SEGN_LIVELLO_1)" _
                    & " AS CATEGORIA1," _
                    & " ID_PERICOLO_SEGNALAZIONE,'' as CRITICITA," _
                    & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO," _
                    & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_APERTURA," _
                    & " (SELECT max(TO_CHAR (TO_DATE (SUBSTR (DATA_ORA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY'))" _
                    & " FROM siscom_mi.eventi_segnalazioni" _
                    & " WHERE     id_segnalazione = segnalazioni.id" _
                    & " AND cod_Evento = 'F287'" _
                    & " AND valore_new = 'IN CORSO') AS DATA_IN_CORSO," _
                    & " (SELECT max(TO_CHAR (TO_DATE (SUBSTR (DATA_ORA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY'))" _
                    & " FROM siscom_mi.eventi_segnalazioni" _
                    & " WHERE id_segnalazione = segnalazioni.id" _
                    & " AND cod_Evento = 'F287'" _
                    & " AND valore_new = 'EVASA') AS DATA_EVASIONE," _
                    & " SISCOM_MI.GETDATA (DATA_CHIUSURA) AS DATA_CHIUSURA," _
                    & " (SELECT SUM (SOLLECITO)" _
                    & " FROM SISCOM_MI.SEGNALAZIONI_NOTE" _
                    & " WHERE sollecito > 0" _
                    & " AND SEGNALAZIONI_NOTE.ID_SEGNALAZIONE = SEGNALAZIONI.ID) AS NUM_SOLLECITI" _
                    & " FROM siscom_mi.tab_stati_segnalazioni, siscom_mi.segnalazioni" _
                    & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato" _
                    & " AND segnalazioni.id_stato <> -1" _
                    & " AND segnalazioni.id_contratto = " & CType(Me.Page.FindControl("txtIdContratto"), HiddenField).Value
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)

        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridSegn_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridSegn_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGridSegn.PreRender
        Try
            HideExpandColumnRecursive(RadGridSegn.MasterTableView)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridSegn_PreRender - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
