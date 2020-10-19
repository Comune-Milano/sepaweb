Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_GestioneFatUtenze
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Public Property NumeroElementi() As Integer
        Get
            If Not (ViewState("par_NumeroElementi") Is Nothing) Then
                Return CStr(ViewState("par_NumeroElementi"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_NumeroElementi") = value
        End Set
    End Property

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)

        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../../Portale.aspx""</script>")
            Exit Sub
        End If
        If Not IsPostBack Then
            HFGriglia.Value = dgvTipoUtenze.ClientID
            CaricaEsercizio()
            'CaricaElenco()

        End If

    End Sub
    'Private Sub CaricaElenco()
    '    Try


    '        par.cmd.CommandText = "SELECT pagamenti_utenze_voci.id,PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO, ID_TIPO_UTENZA, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_FORNITORE, id_struttura,(SELECT Getdata(inizio) ||' - '|| Getdata(fine) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID =(SELECT id_esercizio_finanziario FROM SISCOM_MI.PF_MAIN WHERE ID = pagamenti_utenze_voci.id_piano_finanziario)) AS pf , " _
    '                            & "TIPO_UTENZE.descrizione AS TIPO_UTENZE,(PF_VOCI.CODICE ||' '||PF_VOCI.DESCRIZIONE) AS VOCE_PIANO,PF_VOCI_IMPORTO.descrizione AS voce_bp,FORNITORI.ragione_sociale AS fornitore,tab_filiali.nome as struttura ,decode(fl_attivo,0,'NO',1,'SI') AS ATTIVO " _
    '                            & "FROM siscom_mi.PAGAMENTI_UTENZE_VOCI,siscom_mi.TIPO_UTENZE,siscom_mi.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI,siscom_mi.FORNITORI,siscom_mi.tab_filiali " _
    '                            & "WHERE TIPO_UTENZE.ID = id_tipo_utenza " _
    '                            & "AND PF_VOCI_IMPORTO.ID(+) = id_voce_pf_importo " _
    '                            & "AND pf_voci.ID = id_voce_pf " _
    '                            & "AND FORNITORI.ID = id_fornitore " _
    '                            & "and tab_filiali.id = id_struttura order by TIPO_UTENZE.descrizione asc , PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO desc"
    '        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '        Dim dt As New Data.DataTable
    '        da.Fill(dt)
    '        da.Dispose()
    '        Me.idSel.Value = 0
    '        Me.idPiano.Value = 0
    '        Me.idTipoUtenza.Value = 0
    '        Me.idFornitore.Value = 0
    '        Me.idVocePf.Value = 0
    '        Me.idVocePfImporto.Value = 0
    '        Me.idStruttura.Value = 0
    '        Me.confElimina.Value = 0
    '        Me.dgvTipoUtenze.DataSource = dt
    '        Me.dgvTipoUtenze.DataBind()
    '    Catch ex As Exception
    '        If connData.Connessione.State = Data.ConnectionState.Open Then
    '            connData.chiudi()
    '        End If
    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", Page.Title & " CaricaElenco - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")

    '    End Try
    'End Sub

    'Protected Sub dgvTipoUtenze_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgvTipoUtenze.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         

    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''} " _
    '                              & "Selezionato=this;this.style.backgroundColor='red';" _
    '                              & "document.getElementById('idPiano').value = '" & e.Item.Cells(0).Text & "';" _
    '                              & "document.getElementById('idTipoUtenza').value = '" & e.Item.Cells(1).Text & "';" _
    '                              & "document.getElementById('idFornitore').value = '" & e.Item.Cells(4).Text & "';" _
    '                              & "document.getElementById('idVocePf').value = '" & e.Item.Cells(2).Text & "';" _
    '                              & "document.getElementById('idVocePfImporto').value = '" & e.Item.Cells(3).Text & "';" _
    '                              & "document.getElementById('idStruttura').value = '" & e.Item.Cells(5).Text & "';" _
    '                              & "document.getElementById('idSel').value = '" & e.Item.Cells(6).Text & "';")


    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         

    '        e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
    '        e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''}Selezionato=this;this.style.backgroundColor='red';")
    '        e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''} " _
    '                  & "Selezionato=this;this.style.backgroundColor='red';" _
    '                  & "document.getElementById('idPiano').value = '" & e.Item.Cells(0).Text & "';" _
    '                  & "document.getElementById('idTipoUtenza').value = '" & e.Item.Cells(1).Text & "';" _
    '                  & "document.getElementById('idFornitore').value = '" & e.Item.Cells(4).Text & "';" _
    '                  & "document.getElementById('idVocePf').value = '" & e.Item.Cells(2).Text & "';" _
    '                  & "document.getElementById('idVocePfImporto').value = '" & e.Item.Cells(3).Text & "';" _
    '                  & "document.getElementById('idStruttura').value = '" & e.Item.Cells(5).Text & "';" _
    '                  & "document.getElementById('idSel').value = '" & e.Item.Cells(6).Text & "';")

    '    End If

    'End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Try
            'If Me.idPiano.Value > 0 And _
            '   Me.idTipoUtenza.Value > 0 And _
            '   Me.idFornitore.Value > 0 And _
            '   Me.idVocePf.Value > 0 And _
            '   Me.idVocePfImporto.Value > 0 And _
            '   Me.idStruttura.Value > 0 And _
            If Me.confElimina.Value = 1 Then
                If idSel.Value > 0 Then
                    connData.apri(True)
                    Dim ESISTE As Integer = 0

                    If idTipoUtenza.Value <> 4 Then
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.FATTURE_UTENZE WHERE ID_PARAM_UTENZA = " & idSel.Value
                        ESISTE = par.cmd.ExecuteScalar
                    Else
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PAGAMENTI_CUSTODI WHERE ID_PARAM_UTENZA = " & idSel.Value
                        ESISTE = par.cmd.ExecuteScalar

                    End If

                    If ESISTE = 0 Then

                        'par.cmd.CommandText = "delete from siscom_mi.PAGAMENTI_UTENZE_VOCI where " _
                        '                    & "ID_PIANO_FINANZIARIO =  " & Me.idPiano.Value & " and  " _
                        '                    & "ID_TIPO_UTENZA =  " & Me.idTipoUtenza.Value & " and  " _
                        '                    & "ID_VOCE_PF = " & Me.idVocePf.Value & "and  " _
                        '                    & "ID_VOCE_PF_IMPORTO =  " & Me.idVocePfImporto.Value & " and  " _
                        '                    & "ID_FORNITORE = " & Me.idFornitore.Value & "  and  " _
                        '                    & "ID_STRUTTURA = " & Me.idStruttura.Value & " "
                        par.cmd.CommandText = "delete from siscom_mi.PAGAMENTI_UTENZE_VOCI where " _
                            & " ID =  " & Me.idSel.Value & ""

                        par.cmd.ExecuteNonQuery()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Impossibile eliminare il dato perchè già in uso!\nVerrà trasformato in NON ATTIVO');", True)

                        par.cmd.CommandText = "update siscom_mi.pagamenti_utenze_voci set fl_attivo = 0 where id = " & idSel.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    connData.chiudi(True)
                    'CaricaElenco()

                Else
                    Me.idPiano.Value = 0
                    Me.idTipoUtenza.Value = 0
                    Me.idFornitore.Value = 0
                    Me.idVocePf.Value = 0
                    Me.idVocePfImporto.Value = 0
                    Me.idStruttura.Value = 0
                    Me.confElimina.Value = 0

                End If
            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnElimina_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub dgvTipoUtenze_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvTipoUtenze.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('idPiano').value = '" & dataItem("id_piano_finanziario").Text & "';" _
                                             & "document.getElementById('idTipoUtenza').value = '" & dataItem("id_tipo_utenza").Text & "';" _
                                             & "document.getElementById('idFornitore').value = '" & dataItem("id_fornitore").Text & "';" _
                                             & "document.getElementById('idVocePf').value = '" & dataItem("id_voce_pf").Text & "';" _
                                             & "document.getElementById('idVocePfImporto').value = '" & dataItem("id_voce_pf_importo").Text & "';" _
                                             & "document.getElementById('idStruttura').value = '" & dataItem("id_struttura").Text & "';" _
                                             & "document.getElementById('idSel').value = '" & dataItem("id").Text & "';")
        End If
        If isExporting.Value = "1" Then
            If e.Item.ItemIndex > 0 Then
                Dim context As RadProgressContext = RadProgressContext.Current
                If context.SecondaryTotal <> NumeroElementi Then
                    context.SecondaryTotal = NumeroElementi
                End If
                context.SecondaryValue = e.Item.ItemIndex.ToString()
                context.SecondaryPercent = Int((e.Item.ItemIndex.ToString() * 100) / NumeroElementi)
                context.CurrentOperationText = "Export excel in corso"
            End If
        End If
    End Sub

    Protected Sub dgvTipoUtenze_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvTipoUtenze.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
            If e.CommandName.ToString.Equals("Delete") Then
                'If Me.idPiano.Value > 0 And _
                '   Me.idTipoUtenza.Value > 0 And _
                '   Me.idFornitore.Value > 0 And _
                '   Me.idVocePf.Value > 0 And _
                '   Me.idVocePfImporto.Value > 0 And _
                '   Me.idStruttura.Value > 0 And _
                '   Me.confElimina.Value = 1 Then

           
                If idSel.Value > 0 Then
                    connData.apri(True)
                    Dim ESISTE As Integer = 0

                    If idTipoUtenza.Value <> 4 Then
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.FATTURE_UTENZE WHERE ID_PARAM_UTENZA = " & idSel.Value
                        ESISTE = par.cmd.ExecuteScalar
                    Else
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.PAGAMENTI_CUSTODI WHERE ID_PARAM_UTENZA = " & idSel.Value
                        ESISTE = par.cmd.ExecuteScalar

                    End If

                    If ESISTE = 0 Then

                        'par.cmd.CommandText = "delete from siscom_mi.PAGAMENTI_UTENZE_VOCI where " _
                        '                    & "ID_PIANO_FINANZIARIO =  " & Me.idPiano.Value & " and  " _
                        '                    & "ID_TIPO_UTENZA =  " & Me.idTipoUtenza.Value & " and  " _
                        '                    & "ID_VOCE_PF = " & Me.idVocePf.Value & "and  " _
                        '                    & "ID_VOCE_PF_IMPORTO =  " & Me.idVocePfImporto.Value & " and  " _
                        '                    & "ID_FORNITORE = " & Me.idFornitore.Value & "  and  " _
                        '                    & "ID_STRUTTURA = " & Me.idStruttura.Value & " "
                        par.cmd.CommandText = "delete from siscom_mi.PAGAMENTI_UTENZE_VOCI where " _
                            & " ID =  " & Me.idSel.Value & ""

                        par.cmd.ExecuteNonQuery()
                    Else
                        ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Impossibile eliminare il dato perchè già in uso!\nVerrà trasformato in NON ATTIVO');", True)

                        par.cmd.CommandText = "update siscom_mi.pagamenti_utenze_voci set fl_attivo = 0 where id = " & idSel.Value
                        par.cmd.ExecuteNonQuery()

                    End If
                    connData.chiudi(True)
                    'CaricaElenco()
                    dgvTipoUtenze.Rebind()

                Else
                    Me.idPiano.Value = 0
                    Me.idTipoUtenza.Value = 0
                    Me.idFornitore.Value = 0
                    Me.idVocePf.Value = 0
                    Me.idVocePfImporto.Value = 0
                    Me.idStruttura.Value = 0
                    Me.confElimina.Value = 0

                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub

    Protected Sub dgvTipoUtenze_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvTipoUtenze.NeedDataSource
        Try
            par.cmd.CommandText = "SELECT pagamenti_utenze_voci.id,PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO, ID_TIPO_UTENZA, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_FORNITORE, id_struttura,(SELECT Getdata(inizio) ||' - '|| Getdata(fine) FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID =(SELECT id_esercizio_finanziario FROM SISCOM_MI.PF_MAIN WHERE ID = pagamenti_utenze_voci.id_piano_finanziario)) AS pf , " _
                                & "TIPO_UTENZE.descrizione AS TIPO_UTENZE,(PF_VOCI.CODICE ||' '||PF_VOCI.DESCRIZIONE) AS VOCE_PIANO,PF_VOCI_IMPORTO.descrizione AS voce_bp,FORNITORI.ragione_sociale AS fornitore,tab_filiali.nome as struttura ,decode(fl_attivo,0,'NO',1,'SI') AS ATTIVO " _
                                & "FROM siscom_mi.PAGAMENTI_UTENZE_VOCI,siscom_mi.TIPO_UTENZE,siscom_mi.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI,siscom_mi.FORNITORI,siscom_mi.tab_filiali " _
                                & "WHERE TIPO_UTENZE.ID = id_tipo_utenza " _
                                & "AND PF_VOCI_IMPORTO.ID(+) = id_voce_pf_importo " _
                                & "AND pf_voci.ID = id_voce_pf " _
                                & "AND FORNITORI.ID = id_fornitore " _
                                & "and tab_filiali.id = id_struttura order by TIPO_UTENZE.descrizione asc , PAGAMENTI_UTENZE_VOCI.ID_PIANO_FINANZIARIO desc"
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'da.Dispose()
            Me.idSel.Value = 0
            Me.idPiano.Value = 0
            Me.idTipoUtenza.Value = 0
            Me.idFornitore.Value = 0
            Me.idVocePf.Value = 0
            Me.idVocePfImporto.Value = 0
            Me.idStruttura.Value = 0
            Me.confElimina.Value = 0
            'Me.dgvTipoUtenze.DataSource = dt
            'Me.dgvTipoUtenze.DataBind()
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            NumeroElementi = dt.Rows.Count
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " CaricaElenco - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub





    Private Sub CaricaEsercizio()

        Dim sql As String = ""
        sql = "select SISCOM_MI.PF_MAIN.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY')||' - '||TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY')||' '||SISCOM_MI.PF_STATI.DESCRIZIONE as descrizione " _
                    & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                    & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                    & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                    & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by id desc"
        par.caricaComboTelerik(sql, cmbEsercizio, "ID", "descrizione", True)

        par.caricaComboTelerik("select id,descrizione from siscom_mi.TIPO_UTENZE order by descrizione asc", cmbTipoTracciato, "id", "descrizione", True)
        par.caricaComboTelerik("select cod_fornitore||' - '||ragione_sociale as descrizione,id  from siscom_mi.fornitori order by fornitori.ragione_sociale asc", cmbFornitore, "id", "descrizione", True)
        par.caricaComboTelerik("select id,nome from siscom_mi.tab_filiali  order by nome asc", cmbStruttura, "id", "nome", True)


    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        If Me.cmbEsercizio.SelectedValue <> -1 Then
            par.caricaComboTelerik("select id,codice||' - '||descrizione as descrizione from siscom_mi.pf_voci where id_voce_madre is not null and id_piano_finanziario =" & Me.cmbEsercizio.SelectedValue & " /*and id in (select id_voce from siscom_mi.pf_voci_importo)*/ order by codice asc", cmbPfVoci, "id", "descrizione", True)
            par.caricaComboTelerik("SELECT * FROM siscom_mi.TAB_SERVIZI WHERE ID IN (SELECT id_servizio FROM siscom_mi.TAB_SERVIZI_VOCI WHERE id_voce IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE  id_piano_finanziario =" & Me.cmbEsercizio.SelectedValue & ")) and id in (select id_servizio from siscom_mi.pf_voci_importo) ", cmbServizio, "ID", "descrizione", True)
        Else
            Me.cmbPfVoci.Items.Clear()
            Me.cmbServizio.Items.Clear()
            Me.cmbPfVociImporto.Items.Clear()
        End If
    End Sub
    Private Sub CaricaPfVociImporto()
        If Me.cmbPfVoci.SelectedValue <> "-1" And Me.cmbServizio.SelectedValue <> "-1" Then
            par.caricaComboTelerik("SELECT ID,descrizione FROM siscom_mi.PF_VOCI_IMPORTO WHERE id_voce =" & Me.cmbPfVoci.SelectedValue & "  AND id_servizio =" & Me.cmbServizio.SelectedValue & " AND id_lotto = (SELECT ID FROM siscom_mi.LOTTI WHERE id_filiale = " & Session.Item("ID_STRUTTURA") & " AND id_esercizio_finanziario = (SELECT ID_ESERCIZIO_FINANZIARIO FROM siscom_mi.PF_MAIN WHERE ID = " & Me.cmbEsercizio.SelectedValue & ")) order by descrizione asc", cmbPfVociImporto, "ID", "DESCRIZIONE", True)
        Else
            Me.cmbPfVociImporto.Items.Clear()
        End If

    End Sub
    Protected Sub cmbServizio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged
        CaricaPfVociImporto()
    End Sub

    Protected Sub cmbPfVoci_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPfVoci.SelectedIndexChanged
        CaricaPfVociImporto()
    End Sub
    Private Function Controlli() As Boolean
        Controlli = True
        Dim msg As String = ""
        If Me.cmbStruttura.SelectedValue = -1 Then
            msg += "\n- Scegliere la struttura;"

        End If
        If Me.cmbEsercizio.SelectedValue = -1 Then
            msg += "\n- Scegliere il piano finanziario;"

        End If
        If Me.cmbTipoTracciato.SelectedValue = -1 Then
            msg += "\n- Scegliere il tipo tracciato;"

        End If
        If Me.cmbFornitore.SelectedValue = -1 Then
            msg += "\n- Scegliere il fornitore;"

        End If
        If par.IfEmpty(Me.cmbPfVoci.SelectedValue, -1) = -1 Then
            msg += "\n- Scegliere la voce del B.P.;"
        End If
        '************************PUCCIA MODIFICA DEL 06/07/2015**********************************
        'ESCLUSIONE OBBLIGATORIETA' PF_VOCI_IMPORTO, PER POSSIBILITA DI CREARE ORDINI SU CAPITOLI DI SPESA SENZA RECORD IN PF_VOCI_IMPORTO.

        'If par.IfEmpty(Me.cmbServizio.SelectedValue, -1) = -1 Then
        '    msg += "\n- Scegliere il servizio;"
        'End If
        'If par.IfEmpty(Me.cmbPfVociImporto.SelectedValue, -1) = -1 Then
        '    msg += "\n- Scegliere la voce servizio;"
        'End If
        '************************PUCCIA FINE MODIFICA DEL 06/07/2015*****************************

        If String.IsNullOrEmpty(msg) Then
            Dim condizVociImp As String = ""
            connData.apri()
            If par.IfEmpty(Me.cmbPfVociImporto.SelectedValue, -1) <> -1 Then
                condizVociImp = " AND ID_VOCE_PF_IMPORTO = " & Me.cmbPfVociImporto.SelectedValue & " "
            End If
            par.cmd.CommandText = "select * from siscom_mi.PAGAMENTI_UTENZE_VOCI where ID_PIANO_FINANZIARIO = " & Me.cmbEsercizio.SelectedValue & " AND ID_VOCE_PF = " & Me.cmbPfVoci.SelectedValue _
                                & " " & condizVociImp & " and ID_FORNITORE = " & Me.cmbFornitore.SelectedValue & " and id_tipo_utenza = " & Me.cmbTipoTracciato.SelectedValue
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                msg += "\n- Esiste già questo abbinamento"
            End If
            lettore.Close()
            connData.chiudi()
        End If

        If Not String.IsNullOrEmpty(msg) Then
            Controlli = False

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgEroore", "alert('Impossibile procedere! " & msg & "')", True)
        End If

    End Function

    Protected Sub btnSalvaFattureUtenze_Click(sender As Object, e As System.EventArgs) Handles btnSalvaFattureUtenze.Click
        Try
            If Controlli() = True Then
                connData.apri(True)
                par.cmd.CommandText = "insert into siscom_mi.pagamenti_utenze_voci (id,ID_PIANO_FINANZIARIO, ID_TIPO_UTENZA, ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_FORNITORE, ID_STRUTTURA) values " _
                                    & "(siscom_mi.seq_pagamenti_utenze_voci.nextval," & Me.cmbEsercizio.SelectedValue & "," & Me.cmbTipoTracciato.SelectedValue & "," & Me.cmbPfVoci.SelectedValue & "," & par.insDbValue(Me.cmbPfVociImporto.SelectedValue, False, False, True) & "," & Me.cmbFornitore.SelectedValue & "," & Me.cmbStruttura.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
                connData.chiudi(True)
                dgvTipoUtenze.Rebind()
                PulisciRadWindowFattUtenza()
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgOk", "alert('Operazione eseguita correttamente');", True)

            End If
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " btnCarica_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub PulisciRadWindowFattUtenza()
        Try
            cmbStruttura.SelectedValue = "-1"
            cmbEsercizio.SelectedValue = "-1"
            cmbTipoTracciato.SelectedValue = "-1"
            cmbFornitore.SelectedValue = "-1"
            cmbPfVoci.SelectedValue = "-1"
            cmbServizio.SelectedValue = "-1"
            cmbPfVociImporto.SelectedValue = "-1"
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " btnCarica_Click - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnChiudiRadFattureUtenze_Click(sender As Object, e As System.EventArgs) Handles btnChiudiRadFattureUtenze.Click
        PulisciRadWindowFattUtenza()
    End Sub
End Class
