
Imports System.Data
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureUtenze
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
            par.caricaComboTelerik("select cod_fornitore||' - '||ragione_sociale as descrizione,id  from siscom_mi.fornitori where id in (select id_fornitore from siscom_mi.pagamenti_utenze_voci where id_tipo_utenza <> 4) order by fornitori.ragione_sociale asc", cmbFornitore, "id", "descrizione", True)


            txtEmDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtEmAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtScadDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtScadAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            txtCaricDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtCaricAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtInizioAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFinPerDal.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFinPerAl.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            HFGriglia.Value = dgvFattUtenze.ClientID




        End If

    End Sub
    'Private Sub CaricaFattureUtenza(Optional ByVal filtro As String = "")
    '    Try
    '        Session.Remove("idSel")
    '        Me.idSelezionati.Value = ""
    '        connData.apri()

    '        par.cmd.CommandText = "select FATTURE_UTENZE.ID,'false' as CHECKED, " _
    '                            & " NOME_FILE, " _
    '                            & " siscom_mi.getdata(DATA_CARICAMENTO) as DATA_CARICAMENTO, " _
    '                            & " FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
    '                            & " (SELECT GETDATA(INIZIO)||' - '||GETDATA(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO, " _
    '                            & " siscom_mi.getdata(DATA_EMISSIONE) as data_emissione, " _
    '                            & " siscom_mi.getdata(DATA_SCADENZA) as data_scadenza, " _
    '                            & " NUMERO_FATTURA, " _
    '                            & " ANNO_FATTURA, " _
    '                            & " siscom_mi.getdata(DATA_INIZIO_PERIODO) as data_inizio_periodo, " _
    '                            & " siscom_mi.getdata(DATA_FINE_PERIODO) as data_fine_periodo, " _
    '                            & " POD," _
    '                            & " NOME_VIA_FORNITURA, " _
    '                            & " NUMERO_CIVICO_FORNITURA, " _
    '                            & " BARRATO_FORNITURA, " _
    '                            & " CAP_FORNITURA, " _
    '                            & " LOCALITA_FORNITURA, " _
    '                            & " PROVINCIA_FORNITURA," _
    '                            & " (select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP, " _
    '                            & " /*(select descrizione from siscom_mi.pf_voci_importo where id = id_voce_pf_importo) as SERVIZIO_BP,*/ " _
    '                            & " trim(TO_CHAR((TOTALE_ONERI_DIVERSI),'9G999G999G999G999G990D99')) AS FORM_TOTALE_ONERI_DIVERSI," _
    '                            & " trim(TO_CHAR((BASE_IMPONIBILE),'9G999G999G999G999G990D99')) AS FORM_BASE_IMPONIBILE , " _
    '                            & " trim(TO_CHAR((IVA),'9G999G999G999G999G990D99')) AS FORM_IVA," _
    '                            & " trim(TO_CHAR((TOTALE_BOLLETTA),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTA," _
    '                            & " trim(TO_CHAR((TOTALE_BOLLETTINO),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTINO " _
    '                            & " FROM siscom_mi.FATTURE_UTENZE, siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
    '                            & " WHERE PAGAMENTI_UTENZE_VOCI.ID = FATTURE_UTENZE. ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore AND id_prenotazione IS NULL " & filtro
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dt As New Data.DataTable
    '        da.Fill(dt)
    '        da.Dispose()
    '        connData.chiudi()
    '        Me.dgvFattUtenze.CurrentPageIndex = 0
    '        BindDGV(dt)
    '        Me.lblTitolo.Text = "Fatture Utenze - record trovati: " & dt.Rows.Count
    '    Catch ex As Exception
    '        If connData.Connessione.State = Data.ConnectionState.Open Then
    '            connData.chiudi()
    '        End If
    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

    '    End Try
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

    Private Sub BindDGV(ByVal dt As Data.DataTable)

        Session.Add("dtFatUt", dt)
        GestSelection()
        Me.dgvFattUtenze.DataSource = dt
        Me.dgvFattUtenze.DataBind()

    End Sub
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
    'Protected Sub dgvFattUtenze_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvFattUtenze.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
    '        'DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")
    '        DirectCast(e.Item.FindControl("chkSel"), CheckBox).Attributes.Add("onclick", "IsSelected(this," & e.Item.Cells(0).Text & ")")
    '    ElseIf e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='gainsboro'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
    '        'DirectCast(e.Item.FindControl("chkSelezione"), CheckBox).Attributes.Add("onchange", "IsSelected(this)")
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

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'aricaFattureUtenza(CreaFiltro)
        dgvFattUtenze.Rebind()
    End Sub
    Private Function CreaFiltro() As String
        Dim strFiltro As String = ""
        Dim primo As Boolean = False
        If Not String.IsNullOrEmpty(Me.txtEmDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "
            End If

            strFiltro += " data_emissione >= '" & par.AggiustaData(Me.txtEmDal.Text) & "'"
        End If
        If Not String.IsNullOrEmpty(Me.txtEmAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " data_emissione <= '" & par.AggiustaData(Me.txtEmAl.Text) & "'"

        End If

        If Not String.IsNullOrEmpty(Me.txtScadDal.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " data_scadenza >= '" & par.AggiustaData(Me.txtScadDal.Text) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtScadAl.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " data_scadenza <= '" & par.AggiustaData(Me.txtScadAl.Text) & "'"

        End If
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
        '************FILTRO POD*****************

        If Not String.IsNullOrEmpty(Me.txtPOD.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " pod like '" & par.PulisciStrSql(Me.txtPOD.Text.Replace("*", "%")) & "'"

        End If
        If Not String.IsNullOrEmpty(Me.txtNumFattura.Text) Then
            If primo = True Then
                strFiltro += " where "
                primo = False
            Else
                strFiltro += " and "

            End If
            strFiltro += " NUMERO_FATTURA like '" & par.PulisciStrSql(Me.txtNumFattura.Text.Replace("*", "%")) & "'"

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
        Me.txtEmDal.Text = ""
        Me.txtEmAl.Text = ""
        Me.txtScadDal.Text = ""
        Me.txtScadAl.Text = ""
        Me.txtPOD.Text = ""
        Me.txtNumFattura.Text = ""
        Me.txtAnno.Text = ""
        Me.cmbFornitore.SelectedValue = "-1"
        Me.txtCaricDal.Text = ""
        Me.txtCaricAl.Text = ""
        Me.txtInizioDal.Text = ""
        Me.txtInizioAl.Text = ""
        Me.txtFinPerDal.Text = ""
        Me.txtFinPerAl.Text = ""
        'CaricaFattureUtenza()
        dgvFattUtenze.Rebind()

    End Sub

    Private Function splitIN(ByVal ins As String, ByVal preIn As String, ByVal noAnd As Boolean) As String
        Dim condIdSel As String = ""
        Dim s As Generic.List(Of String)
        Dim link As String = "AND"
        If noAnd = True Then
            link = ""
        End If
        s = par.QueryINSplit(ins, " " & preIn & " (#SOST#) ", "#SOST#")
        condIdSel = ""
        Select Case s.Count
            Case 1
                condIdSel &= " " & link & " " & s(0) & " "
            Case Else
                condIdSel &= " " & link & " ( "
                For i As Integer = 0 To s.Count - 1
                    If i = 0 Then
                        condIdSel &= s(i) & " "
                    Else
                        condIdSel &= " OR " & s(i) & " "
                    End If
                Next
                condIdSel &= " ) "
        End Select
        Return condIdSel
    End Function
    Protected Sub btnCreaCdp_Click(sender As Object, e As System.EventArgs) Handles btnCreaCdp.Click
        Try

            If Not String.IsNullOrEmpty(idSelezionati.Value) Then
                Dim TipoAnomalia As Integer = 0
                '*/*/**/**/*/*/*/*/*/*/*/CONTROLLO SE SELEZIONATE FATTURE UT. SU ESERCIZI DIVERSI
                Session.Add("idSel", Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(",")))
                par.cmd.CommandText = "select distinct PAGAMENTI_UTENZE_VOCI.id_piano_finanziario,PAGAMENTI_UTENZE_VOCI.id_fornitore from siscom_mi.fatture_utenze,SISCOM_MI.PAGAMENTI_UTENZE_VOCI,siscom_mi.pf_voci where " _
                    & "PAGAMENTI_UTENZE_VOCI.ID = FATTURE_UTENZE.ID_PARAM_UTENZA AND pf_voci.id = PAGAMENTI_UTENZE_VOCI.id_voce_pf " & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", False)
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 1 Then
                    TipoAnomalia = 1
                End If
                '*/*/**/**/*/*/*/*/*/*/*/CONTROLLO SE ESISTE IN ELENCO POD
                par.cmd.CommandText = "SELECT DISTINCT(POD) as pod_manca FROM siscom_mi.FATTURE_UTENZE,siscom_mi.PAGAMENTI_UTENZE_VOCI  WHERE " _
                                    & " PAGAMENTI_UTENZE_VOCI.ID = FATTURE_UTENZE.ID_PARAM_UTENZA " _
                                    & " AND POD NOT IN (SELECT POD FROM siscom_mi.POD WHERE fl_attivo = 1 and ID_TIPO_FORNITURA = ID_TIPO_UTENZA AND POD.ID_FORNITORE = PAGAMENTI_UTENZE_VOCI.ID_FORNITORE)" _
                                    & splitIN(Session.Item("idSel"), " FATTURE_UTENZE.ID IN", False)
                Dim dtpod As New Data.DataTable
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                da.Fill(dtpod)
                da.Dispose()
                Dim lstPodMancanti As String = ""

                If dtpod.Rows.Count > 0 Then
                    For Each r As Data.DataRow In dtpod.Rows
                        lstPodMancanti &= "\n - " & r.Item("pod_manca").ToString
                    Next
                    TipoAnomalia = 2
                End If

                Select Case TipoAnomalia
                    Case 0
                        Dim script As String = "function f(){var oWnd=$find(""" + RadWindow1.ClientID + """);oWnd.setUrl('FatturePagaUt.aspx?TIPO=U');oWnd.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                       ' ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "document.getElementById('btnCrea').click();", True)

                    Case 1
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Ipossibile selezionare fatture legate ad esercizie/o fornitori diversi.');", True)

                    Case 2
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "alert('Impossibile procedere.Mancano i pod:" & lstPodMancanti & " ');", True)

                End Select

            Else
                Response.Write(<script>alert('Nessuna fattura selezionata, impossibile creare il CDP');</script>)
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


    'Protected Sub dgvFattUtenze_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgvFattUtenze.PageIndexChanged

    '    cambiataPagina(e.NewPageIndex)
    'End Sub
    Sub cambiataPagina(ByVal numero As Integer)
        If numero >= 0 Then
            'Label3.Text = "0"
            dgvFattUtenze.CurrentPageIndex = numero
            BindDGV(CType(Session.Item("dtFatUt"), Data.DataTable))
        End If
    End Sub


    Protected Sub btnCrea_Click(sender As Object, e As System.EventArgs) Handles btnCrea.Click
        Pulisci()
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "FtUtenze", "Export", dtNEW)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
        End If
    End Sub

    Protected Sub btnEliminaFattura_Click(sender As Object, e As System.EventArgs) Handles btnEliminaFattura.Click
        If Not String.IsNullOrEmpty(idSelezionati.Value) Then
            Me.idSelezionati.Value = Me.idSelezionati.Value.Substring(0, Me.idSelezionati.Value.LastIndexOf(","))

            connData.apri(True)
            par.cmd.CommandText = "delete from siscom_mi.fatture_utenze where id in (" & Me.idSelezionati.Value & ") and id_prenotazione is null"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
            'CaricaFattureUtenza()
            dgvFattUtenze.Rebind()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Record eliminato correttamente!');", True)

        Else
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Selezionare almeno una fattura per eliminarla!');", True)

        End If
    End Sub

    Protected Sub btnEliminaFattura0_Click(sender As Object, e As System.EventArgs) Handles btnEliminaFattura0.Click
        connData.apri(True)
        par.cmd.CommandText = "delete from siscom_mi.fatture_utenze where  id_prenotazione is null " & CreaFiltro()
        par.cmd.ExecuteNonQuery()
        connData.chiudi(True)
        'CaricaFattureUtenza()
        dgvFattUtenze.Rebind()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Record eliminati correttamente!');", True)

    End Sub

    Private Sub dgvFattUtenze_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvFattUtenze.NeedDataSource
        Try
            Dim strFiltro As String = ""
            Dim primo As Boolean = False
            If Not String.IsNullOrEmpty(Me.txtEmDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "
                End If

                strFiltro += " data_emissione >= '" & par.AggiustaData(Me.txtEmDal.Text) & "'"
            End If
            If Not String.IsNullOrEmpty(Me.txtEmAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " data_emissione <= '" & par.AggiustaData(Me.txtEmAl.Text) & "'"

            End If

            If Not String.IsNullOrEmpty(Me.txtScadDal.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " data_scadenza >= '" & par.AggiustaData(Me.txtScadDal.Text) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtScadAl.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " data_scadenza <= '" & par.AggiustaData(Me.txtScadAl.Text) & "'"

            End If
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
            '************FILTRO POD*****************

            If Not String.IsNullOrEmpty(Me.txtPOD.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " pod like '" & par.PulisciStrSql(Me.txtPOD.Text.Replace("*", "%")) & "'"

            End If
            If Not String.IsNullOrEmpty(Me.txtNumFattura.Text) Then
                If primo = True Then
                    strFiltro += " where "
                    primo = False
                Else
                    strFiltro += " and "

                End If
                strFiltro += " NUMERO_FATTURA like '" & par.PulisciStrSql(Me.txtNumFattura.Text.Replace("*", "%")) & "'"

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
            Me.idSelezionati.Value = ""
            connData.apri()

            par.cmd.CommandText = "select FATTURE_UTENZE.ID,'false' as CHECKED, " _
                                & " NOME_FILE, " _
                                & " siscom_mi.getdata(DATA_CARICAMENTO) as DATA_CARICAMENTO, " _
                                & " FORNITORI.RAGIONE_SOCIALE AS FORNITORE," _
                                & " (SELECT GETDATA(INIZIO)||' - '||GETDATA(FINE) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID = (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.PF_VOCI WHERE ID = PAGAMENTI_UTENZE_VOCI.ID_VOCE_PF))) AS PIANO, " _
                                & " siscom_mi.getdata(DATA_EMISSIONE) as data_emissione, " _
                                & " siscom_mi.getdata(DATA_SCADENZA) as data_scadenza, " _
                                & " NUMERO_FATTURA, " _
                                & " ANNO_FATTURA, " _
                                & " siscom_mi.getdata(DATA_INIZIO_PERIODO) as data_inizio_periodo, " _
                                & " siscom_mi.getdata(DATA_FINE_PERIODO) as data_fine_periodo, " _
                                & " POD," _
                                & " NOME_VIA_FORNITURA, " _
                                & " NUMERO_CIVICO_FORNITURA, " _
                                & " BARRATO_FORNITURA, " _
                                & " CAP_FORNITURA, " _
                                & " LOCALITA_FORNITURA, " _
                                & " PROVINCIA_FORNITURA," _
                                & " (select (PF_VOCI.CODICE /*||' '||PF_VOCI.DESCRIZIONE*/) from siscom_mi.pf_voci where id = id_voce_pf) as VOCE_BP, " _
                                & " /*(select descrizione from siscom_mi.pf_voci_importo where id = id_voce_pf_importo) as SERVIZIO_BP,*/ " _
                                & " trim(TO_CHAR((TOTALE_ONERI_DIVERSI),'9G999G999G999G999G990D99')) AS FORM_TOTALE_ONERI_DIVERSI," _
                                & " trim(TO_CHAR((BASE_IMPONIBILE),'9G999G999G999G999G990D99')) AS FORM_BASE_IMPONIBILE , " _
                                & " trim(TO_CHAR((IVA),'9G999G999G999G999G990D99')) AS FORM_IVA," _
                                & " trim(TO_CHAR((TOTALE_BOLLETTA),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTA," _
                                & " trim(TO_CHAR((TOTALE_BOLLETTINO),'9G999G999G999G999G990D99')) AS FORM_TOTALE_BOLLETTINO " _
                                & " FROM siscom_mi.FATTURE_UTENZE, siscom_mi.FORNITORI,siscom_mi.PAGAMENTI_UTENZE_VOCI " _
                                & " WHERE PAGAMENTI_UTENZE_VOCI.ID = FATTURE_UTENZE. ID_PARAM_UTENZA AND FORNITORI.ID = PAGAMENTI_UTENZE_VOCI.id_fornitore AND id_prenotazione IS NULL " & strFiltro
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'da.Dispose()
            'connData.chiudi()
            'Me.dgvFattUtenze.CurrentPageIndex = 0
            'BindDGV(dt)

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            Session.Add("dtFatUt", dt)
            GestSelection()
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            connData.chiudi()
            Me.lblTitolo.Text = "Utenze - Ricerca fatture - record trovati: " & dt.Rows.Count
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaModalita - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        dgvFattUtenze.AllowPaging = False
        dgvFattUtenze.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvFattUtenze.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvFattUtenze.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvFattUtenze.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvFattUtenze.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvFattUtenze.AllowPaging = True
        dgvFattUtenze.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvFattUtenze.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "UTENZE", "UTENZE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('');</script>")
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Private Sub cmbFornitore_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbFornitore.SelectedIndexChanged
        dgvFattUtenze.Rebind()
    End Sub

    Private Sub btnSalva0_Click(sender As Object, e As EventArgs) Handles btnSalva0.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub
End Class
