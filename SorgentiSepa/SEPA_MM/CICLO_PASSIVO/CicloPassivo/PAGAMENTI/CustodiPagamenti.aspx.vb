
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_CustodiPagamenti
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
            HFGriglia.Value = dgvFattUtenze.ClientID
            par.caricaComboTelerik("select cod_fornitore||' - '||ragione_sociale as descrizione,id  from siscom_mi.fornitori where id in (select id_fornitore from siscom_mi.pagamenti_utenze_voci where id_tipo_utenza = 4) order by fornitori.ragione_sociale asc", cmbFornitore, "id", "descrizione", True)
            HFAltezzaSottratta.Value = 400
            CaricaPagCustodi()
        End If

    End Sub
    Private Sub CaricaPagCustodi(Optional ByVal filtro As String = "")
        Try
            Session.Remove("idSel")
            idSelezionati.Value = ""
            connData.apri()
            If Request.QueryString("PAGATE") = 0 Then
                filtro += " and id_prenotazione is null "
            ElseIf Request.QueryString("PAGATE") = 1 Then
                filtro += " and id_prenotazione is not null "
                Me.btnEliminaFattura.Visible = False
                Me.btnCrea.Visible = False
                Me.btnCreaCdp.Visible = False
            End If
            par.cmd.CommandText = "SELECT PAGAMENTI_CUSTODI.ID,'false' AS CHECKED, " _
                                & " NOME_FILE, " _
                                & " siscom_mi.Getdata(DATA_CARICAMENTO) AS DATA_CARICAMENTO, " _
                                & " FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                                & " (SELECT Getdata(INIZIO)||' - '||Getdata(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO, " _
                                & " SIGLA,ANNO,MESE,COD_CUSTODE," _
                                & " (select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP, " _
                                & " /*(select descrizione from siscom_mi.pf_voci_importo where id = id_voce_pf_importo) as SERVIZIO_BP,*/ " _
                                & " trim(TO_CHAR((IMPORTO),'9G999G999G999G999G990D99')) AS IMPORTO " _
                                & " FROM siscom_mi.PAGAMENTI_CUSTODI, siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = PAGAMENTI_CUSTODI.ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore  " & filtro
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Me.dgvFattUtenze.CurrentPageIndex = 0
            BindDGV(dt)
            Me.lblTitolo.Text = "Custodi - Custodi con CDP - record trovati: " & dt.Rows.Count
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
    Private Sub BindDGV(ByVal dt As Data.DataTable)
        Session.Add("dtFatUt", dt)
        GestSelection()
        Me.dgvFattUtenze.DataSource = dt
        Me.dgvFattUtenze.DataBind()
        If Request.QueryString("PAGATE") = 1 Then
            dgvFattUtenze.Columns("2").Visible = False
        End If
    End Sub
    Private Sub GestSelection()
        If par.IfEmpty(idSelezionati.Value, "") <> "" Then
            Dim lst As New Generic.List(Of Integer)
            Dim s As String()
            s = Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(",")).Split(",")
            For Each i As String In s
                lst.Add(i)
            Next
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("dtFatUt"), Data.DataTable)
            For Each r As Data.DataRow In dt.Rows
                If lst.Contains(r.Item("id")) Then
                    r("CHECKED") = "true"
                Else
                    r("CHECKED") = "false"
                End If
            Next
            Session.Item("dtFatUt") = dt
        End If

    End Sub

    'Protected Sub dgvFattUtenze_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvFattUtenze.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
    '        'DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")
    '        DirectCast(e.Item.FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "IsSelected(this," & e.Item.Cells(2).Text & ")")
    '    ElseIf e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        'e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        'e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='gainsboro'}")
    '        'e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
    '        'DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")
    '        ' DirectCast(e.Item.FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "IsSelected(this," & e.Item.Cells(2).Text & ")")

    '    End If
    '    If e.Item.ItemType = ListItemType.Header Then
    '        If selAll.Value = 0 Then

    '            DirectCast(e.Item.FindControl("chkSelAll"), CheckBox).Checked = False
    '        Else
    '            DirectCast(e.Item.FindControl("chkSelAll"), CheckBox).Checked = True

    '        End If

    '    End If

    'End Sub

    Protected Sub dgvFattUtenze_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvFattUtenze.ItemDataBound
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
        CaricaPagCustodi(CreaFiltro)
    End Sub
    Private Function CreaFiltro() As String
        Dim strFiltro As String = ""
        Dim primo As Boolean = False


        If Not String.IsNullOrEmpty(Me.txtCodCustode.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " COD_CUSTODE like '" & par.PulisciStrSql(Me.txtCodCustode.Text.Replace("*", "%")) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtAnno.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " anno like '" & par.PulisciStrSql(Me.txtAnno.Text.Replace("*", "%")) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtMese.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " MESE like '" & par.PulisciStrSql(Me.txtMese.Text.Replace("*", "%")) & "'"

        End If
        If Me.cmbFornitore.SelectedValue <> "-1" Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If

            strFiltro += " id_fornitore = " & Me.cmbFornitore.SelectedValue
        End If


        CreaFiltro = strFiltro
    End Function
    Protected Sub btnPulisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPulisci.Click
        Pulisci()
    End Sub
    Private Sub Pulisci()
        Me.txtCodCustode.Text = ""
        Me.txtAnno.Text = ""
        Me.txtMese.Text = ""
        Me.cmbFornitore.SelectedValue = "-1"
        CaricaPagCustodi()

    End Sub




    Protected Sub btnCrea_Click(sender As Object, e As System.EventArgs) Handles btnCrea.Click
        CaricaPagCustodi()
    End Sub


    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            dgvFattUtenze.CurrentPageIndex = numero
            BindDGV(CType(Session.Item("dtFatUt"), Data.DataTable))
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        End If
    End Sub

    Protected Sub chkSelAll_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim dt As New Data.DataTable
        dt = Session.Item("dtFatUt")
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

    Protected Sub btnCreaCdp_Click(sender As Object, e As System.EventArgs) Handles btnCreaCdp.Click
        Try
            If Not String.IsNullOrEmpty(idSelezionati.Value) Then
                Dim TipoAnomalia As Integer = 0
                '*/*/**/**/*/*/*/*/*/*/*/CONTROLLO SE SELEZIONATE PAGAMENTI CUSTODI. SU ESERCIZI DIVERSI
                Session.Add("idSel", Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(",")))
                par.cmd.CommandText = "select distinct PAGAMENTI_UTENZE_VOCI.id_piano_finanziario,PAGAMENTI_UTENZE_VOCI.id_fornitore from siscom_mi.pagamenti_custodi,SISCOM_MI.PAGAMENTI_UTENZE_VOCI,siscom_mi.pf_voci where " _
                    & "PAGAMENTI_UTENZE_VOCI.ID = pagamenti_custodi.ID_PARAM_UTENZA AND pf_voci.id = PAGAMENTI_UTENZE_VOCI.id_voce_pf and pagamenti_custodi.id in (" & Session.Item("idSel") & ")"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 1 Then
                    TipoAnomalia = 1
                End If
                '*/*/**/**/*/*/*/*/*/*/*/CONTROLLO SE ESISTE IN ELENCO CUSTODI
                Dim lstCustodiMancanti As String = ""

                par.cmd.CommandText = "SELECT DISTINCT (cod_custode) AS pod_manca" _
                                    & "           FROM siscom_mi.PAGAMENTI_CUSTODI, siscom_mi.PAGAMENTI_UTENZE_VOCI" _
                                    & "          WHERE PAGAMENTI_UTENZE_VOCI.ID = PAGAMENTI_CUSTODI.id_param_utenza" _
                                    & "            AND cod_custode NOT IN (" _
                                    & "                   SELECT matricola" _
                                    & "                     FROM siscom_mi.ANAGRAFICA_CUSTODI" _
                                    & "                    )" _
                                    & "            AND PAGAMENTI_CUSTODI.ID IN (" & Session.Item("idSel") & ")"

                Dim dtAnom1 As New Data.DataTable
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dtAnom1)
                da.Dispose()
                If dtAnom1.Rows.Count > 0 Then
                    connData.apri()
                    For Each r As Data.DataRow In dtAnom1.Rows
                        par.cmd.CommandText = " SELECT LTRIM(matricola,'0')" _
                                            & "  FROM siscom_mi.ANAGRAFICA_CUSTODI where LTRIM(matricola,'0') = '" & r.Item("pod_manca").ToString & "'"
                        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If reader.HasRows = True Then

                        Else
                            TipoAnomalia = 2
                            lstCustodiMancanti += "\n- " & r.Item("pod_manca").ToString
                        End If
                        reader.Close()
                    Next
                    connData.chiudi()
                End If
                Select Case TipoAnomalia
                    Case 0
                        Dim script As String = "function f(){var oWnd=$find(""" + RadWindow1.ClientID + """);oWnd.setUrl('FatturePagaUt.aspx?TIPO=C');oWnd.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "document.getElementById('btnCrea').click();", True)
                    Case 1
                        RadWindowManager1.RadAlert("Impossibile selezionare pagamenti custodi legati ad esercizie/o fornitori diversi.", 300, 150, "Attenzione", "", "null")
                    Case 2
                        RadWindowManager1.RadAlert("Impossibile procedere.Mancano i CUSTODI:" & lstCustodiMancanti, 300, 150, "Attenzione", "", "null")
                End Select
            Else
                RadWindowManager1.RadAlert("Nessun record selezionato, impossibile creare il CDP", 300, 150, "Attenzione", "", "null")

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





    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub btnExportXls_Click(sender As Object, e As System.EventArgs) Handles btnExportXls.Click
        Dim dtNEW As New Data.DataTable
        'dtNEW =
        dtNEW.Merge(CType(Session.Item("dtFatUt"), Data.DataTable))
        'CType(Session.Item("dtFatUt"), Data.DataTable).Merge(dtNEW)
        dtNEW.Columns.Remove("ID")
        dtNEW.Columns.Remove("CHECKED")
        'dtNEW.AcceptChanges()
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "PgCustodi", "Export", dtNEW)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")

        End If
    End Sub

    Protected Sub btnEliminaFatturaTutte_Click(sender As Object, e As System.EventArgs) Handles btnEliminaFatturaTutte.Click
        connData.apri(True)
        par.cmd.CommandText = "delete from siscom_mi.PAGAMENTI_CUSTODI where  id_prenotazione is null " & CreaFiltro()
        par.cmd.ExecuteNonQuery()
        connData.chiudi(True)
        CaricaPagCustodi()
        RadNotificationNote.Text = "Record eliminati correttamente!"
        RadNotificationNote.Show()

    End Sub

    Protected Sub btnEliminaFattura_Click(sender As Object, e As System.EventArgs) Handles btnEliminaFattura.Click
        If Not String.IsNullOrEmpty(idSelezionati.Value) Then
            Me.idSelezionati.Value = Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(","))

            connData.apri(True)
            par.cmd.CommandText = "delete from siscom_mi.PAGAMENTI_CUSTODI where id in (" & Me.idSelezionati.Value & ") and id_prenotazione is null"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            CaricaPagCustodi()
            RadWindowManager1.RadAlert("Record eliminato correttamente!", 300, 150, "Attenzione", "", "null")

        Else
            RadWindowManager1.RadAlert("Selezionare almeno una recod per eliminarlo!", 300, 150, "Attenzione", "", "null")

        End If
    End Sub

    Protected Sub dgvFattUtenze_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvFattUtenze.PageIndexChanged
        cambiataPagina(e.NewPageIndex)
    End Sub

    Private Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbFornitore.SelectedIndexChanged
        CaricaPagCustodi(CreaFiltro)
    End Sub

    Private Sub CICLO_PASSIVO_CicloPassivo_PAGAMENTI_CustodiPagamenti_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Private Sub dgvFattUtenze_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvFattUtenze.NeedDataSource
        Try
            Dim strFiltro As String = ""
            Dim primo As Boolean = False


            If Not String.IsNullOrEmpty(Me.txtCodCustode.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " COD_CUSTODE like '" & par.PulisciStrSql(Me.txtCodCustode.Text.Replace("*", "%")) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtAnno.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " anno like '" & par.PulisciStrSql(Me.txtAnno.Text.Replace("*", "%")) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtMese.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " MESE like '" & par.PulisciStrSql(Me.txtMese.Text.Replace("*", "%")) & "'"

            End If
            If Me.cmbFornitore.SelectedValue <> "-1" Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If

                strFiltro += " id_fornitore = " & Me.cmbFornitore.SelectedValue
            End If
            Session.Remove("idSel")
            idSelezionati.Value = ""
            connData.apri()
            If Request.QueryString("PAGATE") = 0 Then
                strFiltro += " and id_prenotazione is null "
            ElseIf Request.QueryString("PAGATE") = 1 Then
                strFiltro += " and id_prenotazione is not null "
                Me.btnEliminaFattura.Visible = False
                Me.btnCrea.Visible = False
                Me.btnCreaCdp.Visible = False
            End If
            par.cmd.CommandText = "SELECT PAGAMENTI_CUSTODI.ID,'false' AS CHECKED, " _
                                & " NOME_FILE, " _
                                & " siscom_mi.Getdata(DATA_CARICAMENTO) AS DATA_CARICAMENTO, " _
                                & " FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                                & " (SELECT Getdata(INIZIO)||' - '||Getdata(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO, " _
                                & " SIGLA,ANNO,MESE,COD_CUSTODE," _
                                & " (select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP, " _
                                & " /*(select descrizione from siscom_mi.pf_voci_importo where id = id_voce_pf_importo) as SERVIZIO_BP,*/ " _
                                & " trim(TO_CHAR((IMPORTO),'9G999G999G999G999G990D99')) AS IMPORTO " _
                                & " FROM siscom_mi.PAGAMENTI_CUSTODI, siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = PAGAMENTI_CUSTODI.ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore  " & strFiltro
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            'Me.dgvFattUtenze.CurrentPageIndex = 0
            Session.Add("dtFatUt", dt)
            GestSelection()
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            If Request.QueryString("PAGATE") = 1 Then
                dgvFattUtenze.Columns("2").Visible = False
            End If
            Me.lblTitolo.Text = "CUSTODI - record trovati: " & dt.Rows.Count
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub


End Class
