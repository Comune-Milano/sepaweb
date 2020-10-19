Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_MulteCaricate
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = dgvMulte.ClientID
            HFAltezzaSottratta.Value = 300
            txtCaricDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtCaricAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFinPerDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFinPerAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            CaricaMulte("")

        End If
    End Sub
    Private Sub CaricaMulte(ByVal filtro As String)
        Try
            If Request.QueryString("PAGATE") = 0 Then
                filtro = " and id_prenotazione is null " & filtro
            ElseIf Request.QueryString("PAGATE") = 1 Then
                filtro = " and id_prenotazione is not null " & filtro
                Me.btnEliminaFattura.Visible = False
                Me.btnCrea.Visible = False
                Me.btnCreaCdp.Visible = False
                btnEliminaFattureTutte.Visible = False
            End If
            Session.Remove("idSel")
            idSelezionati.Value = ""
            connData.apri()
            'If idPagamento.Value = 0 Then
            par.cmd.CommandText = "select " _
                & "multe.id,'false' as CHECKED, nome_file,GETDATA(data_caricamento) AS DATA_CARICAMENTO,FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "(SELECT Getdata(INIZIO)||' - '||Getdata(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO," _
                & "(select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP," _
                & "GETDATA(data_inizio_periodo) AS DATA_INIZIO_PERIODO,GETDATA(data_fine_periodo) AS DATA_FINE_PERIODO, " _
                & "(case when id_complesso is not null then 'COMPLESSO' WHEN ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' WHEN ID_UNITA IS NOT NULL THEN 'UNITA' ELSE '' END) AS TIPO_PATRIMONIO, " _
                & "COD_PATRIMONIO,NOTE AS NOTE,RTRIM(LTRIM(TO_CHAR(IMPORTO,'9G999G999G999D99'))) AS IMPORTO,(select progr||'/'||anno from siscom_mi.pagamenti where id = (select id_pagamento from siscom_mi.prenotazioni where id = multe.id_prenotazione)) as CDP " _
                & " from siscom_mi.multe,siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = multe.ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore  " & filtro
            'Else
            'par.cmd.CommandText = "select " _
            '                    & " id,nome_file,GETDATA(data_caricamento) AS DATA_CARICAMENTO,GETDATA(data_inizio_periodo) AS DATA_INIZIO_PERIODO,GETDATA(data_fine_periodo) AS DATA_FINE_PERIODO, " _
            '                    & "(case when id_complesso is not null then 'COMPLESSO' WHEN ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' WHEN ID_UNITA IS NOT NULL THEN 'UNITA' ELSE '' END) AS TIPO_PATRIMONIO, " _
            '                    & "COD_PATRIMONIO,NOTE AS NOTE,RTRIM(LTRIM(TO_CHAR(IMPORTO,'9G999G999G999D99'))) AS IMPORTO " _
            '                    & "from siscom_mi.multe where id_prenotazione in (select id from siscom_mi.prenotazioni where id_pagamento =  " & idPagamento.Value & ")"


            'End If

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Me.dgvMulte.CurrentPageIndex = 0
            BindDGV(dt)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            Me.lblTitolo.Text = "Multe - Multe con CDP - tot record: " & dt.Rows.Count
            If Request.QueryString("PAGATE") = 0 Then
                dgvMulte.Columns(14).Visible = False
            End If
            If Request.QueryString("PAGATE") = 1 Then
                dgvMulte.Columns(2).Visible = False
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub

    'Protected Sub dgvMulte_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvMulte.ItemDataBound
    '    If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
    '        ' DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")
    '        DirectCast(e.Item.FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "IsSelected(this," & e.Item.Cells(0).Text & ")")
    '    End If
    '    If e.Item.ItemType = ListItemType.Header Then
    '        If selAll.Value = 0 Then

    '            DirectCast(e.Item.FindControl("chkSelAll"), CheckBox).Checked = False
    '        Else
    '            DirectCast(e.Item.FindControl("chkSelAll"), CheckBox).Checked = True

    '        End If

    '    End If

    'End Sub

    Protected Sub dgvMulte_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvMulte.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            DirectCast(e.Item.FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "IsSelected(this," & dataItem("ID").Text & ")")
        End If
        If e.Item.ItemType = ListItemType.Header Then
            If selAll.Value = 0 Then

                DirectCast(e.Item.FindControl("chkSelAll"), CheckBox).Checked = False
            Else
                DirectCast(e.Item.FindControl("chkSelAll"), CheckBox).Checked = True

            End If

        End If
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        CaricaMulte(CreaFiltro)

    End Sub
    Private Function CreaFiltro() As String
        Dim strFiltro As String = ""
        Dim primo As Boolean = False
        '************FILTRO DATA CARICAMENTO*****************
        If Not String.IsNullOrEmpty(Me.txtCaricDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_CARICAMENTO >= '" & par.AggiustaData(Me.txtCaricDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtCaricAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_CARICAMENTO <= '" & par.AggiustaData(Me.txtCaricAl.Text) & "'"

        End If

        '************FILTRO DATA INIZIO PERIODO*****************
        If Not String.IsNullOrEmpty(Me.txtInizioDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_INIZIO_PERIODO >= '" & par.AggiustaData(Me.txtInizioDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtInizioAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_INIZIO_PERIODO <= '" & par.AggiustaData(Me.txtInizioAl.Text) & "'"

        End If

        '************FILTRO DATA FINE PERIODO*****************
        If Not String.IsNullOrEmpty(Me.txtFinPerDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_FINE_PERIODO >= '" & par.AggiustaData(Me.txtFinPerDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtFinPerAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " DATA_FINE_PERIODO <= '" & par.AggiustaData(Me.txtFinPerAl.Text) & "'"

        End If

        CreaFiltro = strFiltro
    End Function

    Protected Sub btnPulisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPulisci.Click
        Pulisci()

    End Sub
    Private Sub Pulisci()
        Me.txtCaricDal.Text = ""
        Me.txtCaricAl.Text = ""
        Me.txtInizioDal.Text = ""
        Me.txtInizioAl.Text = ""
        Me.txtFinPerDal.Text = ""
        Me.txtFinPerAl.Text = ""
        CaricaMulte("")

    End Sub


    Protected Sub chkSelAll_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim dt As New Data.DataTable
        dt = Session.Item("dtMulte")
        Me.idSelezionati.Value = ""
        If DirectCast(sender, CheckBox).Checked = True Then
            For Each r As Data.DataRow In dt.Rows
                r("CHECKED") = "true"
                Me.idSelezionati.Value += r("ID").ToString & ","
            Next
            selAll.Value = 1
        Else
            For Each r As Data.DataRow In dt.Rows
                r("CHECKED") = "false"
            Next
            selAll.Value = 0

        End If
        BindDGV(dt)


    End Sub
    Private Sub BindDGV(ByVal dt As Data.DataTable)
        Session.Add("dtMulte", dt)
        GestSelection()
        Me.dgvMulte.DataSource = dt
        Me.dgvMulte.DataBind()

    End Sub

    'Protected Sub dgvMulte_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvMulte.PageIndexChanged
    '    cambiataPagina(e.NewPageIndex)
    'End Sub
    Private Sub GestSelection()
        If par.IfEmpty(idSelezionati.Value, "") <> "" Then
            Dim lst As New Generic.List(Of Integer)
            Dim s As String()
            s = Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(",")).Split(",")
            For Each i As String In s
                lst.Add(i)
            Next
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("dtMulte"), Data.DataTable)
            For Each r As Data.DataRow In dt.Rows
                If lst.Contains(r.Item("id")) Then
                    r("CHECKED") = "true"
                Else
                    r("CHECKED") = "false"
                End If
            Next
            Session.Item("dtMulte") = dt
        End If

    End Sub
    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            dgvMulte.CurrentPageIndex = numero
            GestSelection()
            BindDGV(CType(Session.Item("dtMulte"), Data.DataTable))
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        End If
    End Sub

    Protected Sub btnCrea_Click(sender As Object, e As System.EventArgs) Handles btnCrea.Click
        Pulisci()
    End Sub

    Protected Sub btnCreaCdp_Click(sender As Object, e As System.EventArgs) Handles btnCreaCdp.Click
        Try

            If Not String.IsNullOrEmpty(idSelezionati.Value) Then
                Dim TipoAnomalia As Integer = 0
                '*/*/**/**/*/*/*/*/*/*/*/CONTROLLO SE SELEZIONATE FATTURE UT. SU ESERCIZI DIVERSI
                Session.Add("idSel", Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(",")))
                par.cmd.CommandText = "select distinct PAGAMENTI_UTENZE_VOCI.id_piano_finanziario,PAGAMENTI_UTENZE_VOCI.id_fornitore " _
                                    & " from siscom_mi.multe,SISCOM_MI.PAGAMENTI_UTENZE_VOCI,siscom_mi.pf_voci where " _
                                    & " PAGAMENTI_UTENZE_VOCI.ID = MULTE.ID_PARAM_UTENZA AND pf_voci.id = PAGAMENTI_UTENZE_VOCI.id_voce_pf and MULTE.id in (" & Session.Item("idSel") & ")"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 1 Then
                    TipoAnomalia = 1
                End If

                Select Case TipoAnomalia
                    Case 0
                        Dim script As String = "function f(){var oWnd=$find(""" + RadWindow1.ClientID + """);oWnd.setUrl('FatturePagaUt.aspx?TIPO=M');oWnd.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "document.getElementById('btnCrea').click();", True)

                    Case 1
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Ipossibile selezionare fatture legate ad esercizie/o fornitori diversi.');", True)

                End Select

            Else
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Nessuna MULTA selezionata, impossibile creare il CDP');", True)

            End If
            'Pulisci()
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnCreaCdp_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnEliminaFattura_Click(sender As Object, e As System.EventArgs) Handles btnEliminaFattura.Click
        If Not String.IsNullOrEmpty(idSelezionati.Value) Then
            Me.idSelezionati.Value = Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(","))

            connData.apri(True)
            par.cmd.CommandText = "delete from siscom_mi.multe where id in (" & Me.idSelezionati.Value & ") and id_prenotazione is null"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            CaricaMulte("")
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Record eliminato correttamente!');", True)

        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Selezionare almeno una multa per eliminarla!');", True)

        End If
    End Sub

    Protected Sub btnEliminaFattureTutte_Click(sender As Object, e As System.EventArgs) Handles btnEliminaFattureTutte.Click
        connData.apri(True)
        par.cmd.CommandText = "delete from siscom_mi.multe where  id_prenotazione is null " & CreaFiltro()
        par.cmd.ExecuteNonQuery()
        connData.chiudi(True)
        CaricaMulte("")
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Record eliminati correttamente!');", True)
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub btnExportXls_Click(sender As Object, e As System.EventArgs) Handles btnExportXls.Click
        Dim dtNEW As New Data.DataTable
        'dtNEW =
        dtNEW.Merge(CType(Session.Item("dtMulte"), Data.DataTable))
        'CType(Session.Item("dtMulte"), Data.DataTable).Merge(dtNEW)
        dtNEW.Columns.Remove("ID")
        dtNEW.Columns.Remove("CHECKED")
        'dtNEW.AcceptChanges()
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Multe", "Export", dtNEW)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
        End If
    End Sub

    Protected Sub dgvMulte_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvMulte.PageIndexChanged
        cambiataPagina(e.NewPageIndex)
    End Sub

    Private Sub dgvMulte_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvMulte.NeedDataSource
        Try
            Dim strFiltro As String = ""
            Dim primo As Boolean = False
            '************FILTRO DATA CARICAMENTO*****************
            If Not String.IsNullOrEmpty(Me.txtCaricDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_CARICAMENTO >= '" & par.AggiustaData(Me.txtCaricDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtCaricAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_CARICAMENTO <= '" & par.AggiustaData(Me.txtCaricAl.Text) & "'"

            End If

            '************FILTRO DATA INIZIO PERIODO*****************
            If Not String.IsNullOrEmpty(Me.txtInizioDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_INIZIO_PERIODO >= '" & par.AggiustaData(Me.txtInizioDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtInizioAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_INIZIO_PERIODO <= '" & par.AggiustaData(Me.txtInizioAl.Text) & "'"

            End If

            '************FILTRO DATA FINE PERIODO*****************
            If Not String.IsNullOrEmpty(Me.txtFinPerDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_FINE_PERIODO >= '" & par.AggiustaData(Me.txtFinPerDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtFinPerAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " DATA_FINE_PERIODO <= '" & par.AggiustaData(Me.txtFinPerAl.Text) & "'"

            End If
            If Request.QueryString("PAGATE") = 0 Then
                strFiltro = " and id_prenotazione is null " & strFiltro
            ElseIf Request.QueryString("PAGATE") = 1 Then
                strFiltro = " and id_prenotazione is not null " & strFiltro
                Me.btnEliminaFattura.Visible = False
                Me.btnCrea.Visible = False
                Me.btnCreaCdp.Visible = False
                btnEliminaFattureTutte.Visible = False
            End If
            Session.Remove("idSel")
            idSelezionati.Value = ""
            connData.apri()
            'If idPagamento.Value = 0 Then
            par.cmd.CommandText = "select " _
                & "multe.id,'false' as CHECKED, nome_file,GETDATA(data_caricamento) AS DATA_CARICAMENTO,FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                & "(SELECT Getdata(INIZIO)||' - '||Getdata(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO," _
                & "(select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP," _
                & "GETDATA(data_inizio_periodo) AS DATA_INIZIO_PERIODO,GETDATA(data_fine_periodo) AS DATA_FINE_PERIODO, " _
                & "(case when id_complesso is not null then 'COMPLESSO' WHEN ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' WHEN ID_UNITA IS NOT NULL THEN 'UNITA' ELSE '' END) AS TIPO_PATRIMONIO, " _
                & "COD_PATRIMONIO,NOTE AS NOTE,RTRIM(LTRIM(TO_CHAR(IMPORTO,'9G999G999G999D99'))) AS IMPORTO,(select progr||'/'||anno from siscom_mi.pagamenti where id = (select id_pagamento from siscom_mi.prenotazioni where id = multe.id_prenotazione)) as CDP " _
                & " from siscom_mi.multe,siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = multe.ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore  " & strFiltro
            'Else
            'par.cmd.CommandText = "select " _
            '                    & " id,nome_file,GETDATA(data_caricamento) AS DATA_CARICAMENTO,GETDATA(data_inizio_periodo) AS DATA_INIZIO_PERIODO,GETDATA(data_fine_periodo) AS DATA_FINE_PERIODO, " _
            '                    & "(case when id_complesso is not null then 'COMPLESSO' WHEN ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' WHEN ID_UNITA IS NOT NULL THEN 'UNITA' ELSE '' END) AS TIPO_PATRIMONIO, " _
            '                    & "COD_PATRIMONIO,NOTE AS NOTE,RTRIM(LTRIM(TO_CHAR(IMPORTO,'9G999G999G999D99'))) AS IMPORTO " _
            '                    & "from siscom_mi.multe where id_prenotazione in (select id from siscom_mi.prenotazioni where id_pagamento =  " & idPagamento.Value & ")"


            'End If

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)

            Session.Add("dtMulte", dt)
            GestSelection()
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            connData.chiudi()
            Me.lblTitolo.Text = "ELENCO MULTE CARICATE - tot record: " & dt.Rows.Count
            If Request.QueryString("PAGATE") = 0 Then
                dgvMulte.Columns(14).Visible = False
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_PAGAMENTI_MulteCaricate_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
