Imports Telerik.Web.UI

Partial Class SICUREZZA_ReportAbusivi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)
    End Sub

    Protected Sub RadGridContratti_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridContratti.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridContratti.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text
                End If
            Next
            e.Item.Attributes.Add("onclick", "document.getElementById('idSelezionato').value='" & dataItem("ID").Text & "';document.getElementById('codContratto').value='" & dataItem("cod_contratto").Text & "';" _
                                  & "document.getElementById('txtRUSelected').value='Hai selezionato il contratto " & dataItem("cod_contratto").Text & "';")
            e.Item.Attributes.Add("onDblclick", "apriRU();")
        End If
    End Sub

    Protected Sub RadGridContratti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridContratti.NeedDataSource
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT DISTINCT(RAPPORTI_UTENZA.COD_CONTRATTO) AS CODCONTR,(CASE WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS = 1 AND UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO <> 2 then 'ERP Sociale' WHEN UNITA_IMMOBILIARI.ID_DESTINAZIONE_USO = 2 THEN 'ERP Moderato' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=12 THEN 'CANONE CONVENZ.' WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=8 THEN 'ART.22 C.10 RR 1/2004' " _
                 & "WHEN RAPPORTI_UTENZA.PROVENIENZA_ASS=10 THEN 'FORZE DELL''ORDINE' WHEN RAPPORTI_UTENZA.DEST_USO='C' THEN 'Cooperative' WHEN RAPPORTI_UTENZA.DEST_USO='P' THEN '431 P.O.R.' WHEN RAPPORTI_UTENZA.DEST_USO='D' THEN '431/98 ART.15 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='V' THEN '431/98 ART.15 C.2 R.R.1/2004' WHEN RAPPORTI_UTENZA.DEST_USO='S' THEN '431/98 Speciali' " _
                 & "WHEN RAPPORTI_UTENZA.DEST_USO='0' THEN 'Standard' END) AS TIPO_SPECIFICO, (CASE WHEN RAPPORTI_UTENZA.DURATA_ANNI IS NULL AND RAPPORTI_UTENZA.DURATA_RINNOVO IS NULL THEN RAPPORTI_UTENZA.DURATA_ANNI||''||RAPPORTI_UTENZA.DURATA_RINNOVO ELSE " _
                 & "RAPPORTI_UTENZA.DURATA_ANNI||'+'||RAPPORTI_UTENZA.DURATA_RINNOVO END) AS DURATA,TAB_FILIALI.NOME AS FILIALE_ALER,COMPLESSI_IMMOBILIARI.COD_COMPLESSO,UNITA_IMMOBILIARI.COD_TIPOLOGIA,INDIRIZZI.DESCRIZIONE AS ""INDIRIZZO"",INDIRIZZI.CIVICO,(SELECT NOME FROM COMUNI_NAZIONI WHERE COD=INDIRIZZI.COD_COMUNE) AS COMUNE_UNITA,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE as CUI," _
                 & "RAPPORTI_UTENZA.*,nvl(ANAGRAFICA.CITTADINANZA,'') as cittadinanza,siscom_mi.getstatocontratto(rapporti_utenza.id) as STATO_DEL_CONTRATTO, SISCOM_MI.GETINTESTATARI(RAPPORTI_UTENZA.ID) AS NOME_INTEST," _
                 & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS ""INTESTATARIO"" ," _
                 & "CASE WHEN anagrafica.partita_iva is not null then partita_iva else COD_FISCALE end AS ""COD FISCALE/PIVA"" ,TO_CHAR(TO_DATE(ANAGRAFICA.DATA_NASCITA,'YYYYmmdd'),'DD/MM/YYYY') AS DATA_NASCITA,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA," _
                 & "substr(TIPOLOGIA_RAPP_CONTRATTUALE.DESCRIZIONE,1,25) AS ""POSIZIONE_CONTRATTO"",TIPOLOGIA_OCCUPANTE.DESCRIZIONE AS TIPO_OCCUPANTE,unita_immobiliari.interno,scale_edifici.descrizione AS scala,tipo_livello_piano.descrizione AS piano " _
                 & "FROM " _
                 & "siscom_mi.scale_edifici,siscom_mi.tipo_livello_piano,SISCOM_MI.TAB_FILIALI,SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.TIPOLOGIA_RAPP_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.TIPOLOGIA_OCCUPANTE," _
                 & "SISCOM_MI.INDIRIZZI,SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SOGGETTI_CONTRATTUALI " _
                 & "WHERE (cod_tipologia_contr_loc in ('NONE') or (cod_tipologia_contr_loc in ('L43198') and provenienza_Ass=6 and dest_uso='V')) and " _
                 & "scale_edifici.ID(+)=unita_immobiliari.id_scala AND tipo_livello_piano.cod(+)=unita_immobiliari.cod_tipo_livello_piano AND COMPLESSI_IMMOBILIARI.ID_FILIALE=TAB_FILIALI.ID (+) AND EDIFICI.ID_COMPLESSO=COMPLESSI_IMMOBILIARI.ID (+) AND TIPOLOGIA_RAPP_CONTRATTUALE.COD=RAPPORTI_UTENZA.COD_TIPOLOGIA_RAPP_CONTR  " _
                 & " and cod_tipologia_occupante='INTE' AND EDIFICI.ID (+) =UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID_INDIRIZZO=INDIRIZZI.ID (+)  AND UNITA_CONTRATTUALE.ID_CONTRATTO (+) =RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID (+) =UNITA_CONTRATTUALE.ID_UNITA  AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND " _
                 & "ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND TIPOLOGIA_OCCUPANTE.COD = SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL ORDER BY ""INTESTATARIO"" ASC"

            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(par.cmd.CommandText)
            connData.chiudi()
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Add("ERRORE", Page.Title & " RadGridContratti_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        tabContainer.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
        If Not IsNothing(Request.QueryString("NM")) AndAlso IsNumeric(Request.QueryString("NM")) Then
            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "chiudi", "validNavigation=true;self.close();", True)
        Else
            Response.Redirect("Home.aspx", False)
        End If
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        tabContainer.Height = CType(Me.Master.FindControl("AltezzaContenuto"), HiddenField).Value
    End Sub
End Class
