Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_ReportCompleto
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            caricaAnno()
            caricaCapitolo()
            caricaVoce()
            caricaServizio()
            txtDataAl.SelectedDate = CDate(Now)
            txtDataPagamento.SelectedDate = CDate(Now)
            If connAperta = True Then
                connData.chiudi(False)
            End If
        End If
    End Sub
    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            If IsNumeric(dataItem("ID_VOCE").Text) Then
                Dim anno As Integer = 0
                If IsNumeric(dataItem("DATA_AL").Text) Then
                    anno = CInt(dataItem("DATA_AL").Text)
                End If
                Dim anno2 As Integer = 0
                If IsNumeric(dataItem("DATA_PAGAMENTO").Text) Then
                    anno2 = CInt(dataItem("DATA_PAGAMENTO").Text)
                End If
                Dim voce As Integer = CInt(dataItem("ID_VOCE").Text)
                Dim servizio As Integer = 0
                If IsNumeric(dataItem("ID_SERVIZIO").Text) Then
                    servizio = CInt(dataItem("ID_SERVIZIO").Text)
                End If
                '**** PRENOTATO-P1 ****
                dataItem("PRENOTATO").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("PRENOTATO").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("PRENOTATO").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'P1'," & anno & "," & anno2 & ");")
                '**** CONSUNTIVATO-C1 ****
                dataItem("CONSUNTIVATO").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CONSUNTIVATO").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CONSUNTIVATO").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'C1'," & anno & "," & anno2 & ");")
                '**** CERTIFICATO-CE1 ****
                dataItem("CERTIFICATO").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CERTIFICATO").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CERTIFICATO").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'CE1'," & anno & "," & anno2 & ");")
                '**** CERTIFICATO-CE2 ****
                dataItem("CERTIFICATO_IMPONIBILE").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CERTIFICATO_IMPONIBILE").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CERTIFICATO_IMPONIBILE").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'CE2'," & anno & "," & anno2 & ");")
                '**** CERTIFICATO-CE3 ****
                dataItem("CERTIFICATO_IVA").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CERTIFICATO_IVA").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CERTIFICATO_IVA").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'CE3'," & anno & "," & anno2 & ");")
                '**** FATTURATO-F1 ****
                dataItem("FATTURATO").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("FATTURATO").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("FATTURATO").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'F1'," & anno & "," & anno2 & ");")
                '**** PAGATO-F1 ****
                dataItem("PAGATO").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("PAGATO").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("PAGATO").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'PA1'," & anno & "," & anno2 & ");")
                '**** PAGATO RITENUTA-F1 ****
                dataItem("PAGATO_RITENUTE").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("PAGATO_RITENUTE").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("PAGATO_RITENUTE").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'RP1'," & anno & "," & anno2 & ");")
                '**** RITENUTA CONSUNTIVATA-CR1 ****
                dataItem("CONSUNTIVATO_RIT").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CONSUNTIVATO_RIT").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CONSUNTIVATO_RIT").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'CR1'," & anno & "," & anno2 & ");")
                '**** RITENUTA CONSUNTIVATA-CR1 ****
                dataItem("CONSUNTIVATO_RIT_IMPONIBILE").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CONSUNTIVATO_RIT_IMPONIBILE").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CONSUNTIVATO_RIT_IMPONIBILE").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'CR2'," & anno & "," & anno2 & ");")
                '**** RITENUTA CONSUNTIVATA-CR1 ****
                dataItem("CONSUNTIVATO_RIT_IVA").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("CONSUNTIVATO_RIT_IVA").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("CONSUNTIVATO_RIT_IVA").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'CR3'," & anno & "," & anno2 & ");")
                '**** RITENUTA CONSUNTIVATA-CR1 ****
                dataItem("PAGATO_IVA").Attributes.Add("onmouseover", "this.style.backgroundColor='#FF9900';this.style.cursor='pointer';")
                dataItem("PAGATO_IVA").Attributes.Add("onmouseout", "this.style.backgroundColor='';")
                dataItem("PAGATO_IVA").Attributes.Add("onDblclick", "ApriDettaglio(" & voce & "," & servizio & ",'PI1'," & anno & "," & anno2 & ");")
            End If
        ElseIf TypeOf e.Item Is GridFilteringItem Then
            par.caricaComboTelerik("SELECT DISTINCT ANNO AS DESCRIZIONE FROM SISCOM_MI.REPORT_SINTESI_COMPLETO ORDER BY ANNO DESC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAnno"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro As New RadComboBoxItem
            altro.Value = "NON DEFINITO"
            altro.Text = "NON DEFINITO"
            TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAnno"), RadComboBox).Items.Add(altro)
            If Not String.IsNullOrEmpty(Trim(HFFiltroAnno.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxFiltroAnno"), RadComboBox).SelectedValue = HFFiltroAnno.Value.ToString
            End If
        End If
    End Sub
    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim aggiornamento As String = ""
            par.cmd.CommandText = "SELECT SISCOM_MI.GETDATAORA(MAX(DATA_CALCOLO)) FROM SISCOM_MI.REPORT_SINTESI_COMPLETO"
            aggiornamento &= "(Ultimo aggiornamento: " & par.IfNull(par.cmd.ExecuteScalar, "-")
            par.cmd.CommandText = "SELECT SISCOM_MI.GETDATA(MAX(DATA_AL)) FROM SISCOM_MI.REPORT_SINTESI_COMPLETO"
            aggiornamento &= "-Data Report al: " & par.IfNull(par.cmd.ExecuteScalar, "-")
            par.cmd.CommandText = "SELECT SISCOM_MI.GETDATA(MAX(DATA_PAGAMENTO)) FROM SISCOM_MI.REPORT_SINTESI_COMPLETO"
            aggiornamento &= "-Data Pagamento al: " & par.IfNull(par.cmd.ExecuteScalar, "-") & ")"

            ultimoAggiornamento.Text = aggiornamento
            Dim condizioneRicerca As String = ""
            If IsNumeric(RadComboBoxCapitolo.SelectedValue) AndAlso RadComboBoxCapitolo.SelectedValue > 0 Then
                condizioneRicerca = " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=REPORT_SINTESI_COMPLETO.ID_VOCE AND PF_VOCI.ID_CAPITOLO=" & RadComboBoxCapitolo.SelectedValue & ")"
            End If
            If IsNumeric(RadComboBoxVoce.SelectedValue) AndAlso RadComboBoxVoce.SelectedValue > 0 Then
                If condizioneRicerca = "" Then
                    condizioneRicerca = " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=REPORT_SINTESI_COMPLETO.ID_VOCE AND PF_VOCI.CODICE IN (SELECT CODICE FROM SISCOM_MI.PF_VOCI WHERE ID=" & RadComboBoxVoce.SelectedValue & "))"
                Else
                    condizioneRicerca &= " AND EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=REPORT_SINTESI_COMPLETO.ID_VOCE AND PF_VOCI.CODICE IN (SELECT CODICE FROM SISCOM_MI.PF_VOCI WHERE ID=" & RadComboBoxVoce.SelectedValue & "))"
                End If
            End If
            If IsNumeric(RadComboBoxServizio.SelectedValue) AndAlso RadComboBoxServizio.SelectedValue > 0 Then
                If condizioneRicerca = "" Then
                    condizioneRicerca = " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE PF_VOCI_IMPORTO.ID=REPORT_SINTESI_COMPLETO.ID_sERVIZIO AND PF_VOCI_IMPORTO.DESCRIZIONE IN (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID=" & RadComboBoxServizio.SelectedValue & "))"
                Else
                    condizioneRicerca &= " AND EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE PF_VOCI_IMPORTO.ID=REPORT_SINTESI_COMPLETO.ID_sERVIZIO AND PF_VOCI_IMPORTO.DESCRIZIONE IN (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID=" & RadComboBoxServizio.SelectedValue & "))"
                End If
            End If
            Dim listaAnni As String = ""
            For Each item As ListItem In CheckBoxListFiltroAnno.Items
                If item.Selected = True Then
                    If listaAnni = "" Then
                        listaAnni = item.Value
                    Else
                        listaAnni &= "," & item.Value
                    End If
                End If
            Next
            If listaAnni <> "" Then
                If condizioneRicerca = "" Then
                    condizioneRicerca = " WHERE EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=REPORT_SINTESI_cOMPLETO.ID_VOCE AND PF_VOCI.ID_PIANO_FINANZIARIO IN (" & listaAnni & "))"
                Else
                    condizioneRicerca &= " AND EXISTS (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID=REPORT_SINTESI_cOMPLETO.ID_VOCE AND PF_VOCI.ID_PIANO_FINANZIARIO IN (" & listaAnni & "))"
                End If
            End If
            Dim query As String = "SELECT * FROM SISCOM_MI.REPORT_SINTESI_COMPLETO " & condizioneRicerca
            Dim dt As Data.DataTable = par.getDataTableGrid(query)
            TryCast(sender, RadGrid).DataSource = dt
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid1.AllowPaging = False
        DataGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Completo", "Completo", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub ButtonAggiorna_Click(sender As Object, e As System.EventArgs) Handles ButtonAggiorna.Click
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SISCOM_MI.RPT_SINTESI_COMPLETO"
            par.cmd.CommandType = Data.CommandType.StoredProcedure
            par.cmd.Parameters.Add("dataPagamento", par.AggiustaData(txtDataPagamento.SelectedDate))
            par.cmd.Parameters.Add("dataAl", par.AggiustaData(txtDataAl.SelectedDate))
            par.cmd.ExecuteNonQuery()
            par.cmd.Parameters.Clear()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        DataGrid1.Rebind()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Private Sub caricaAnno()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim query As String = "SELECT PF_MAIN.ID,SUBSTR(INIZIO,1,4) AS ANNO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                & " WHERE SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO ORDER BY 1 DESC"
            par.caricaCheckBoxList(query, CheckBoxListFiltroAnno, "ID", "ANNO")
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
        End Try
    End Sub
    Private Sub caricaCapitolo()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SELECT ID,COD,DESCRIZIONE FROM SISCOM_MI.PF_CAPITOLI WHERE ID IN (SELECT ID_CAPITOLO FROM SISCOM_MI.PF_VOCI) ORDER BY 2 ASC"
            Dim adapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            adapter.Dispose()
            Dim itemVuoto As New RadComboBoxItem()
            itemVuoto.Text = ""
            itemVuoto.Value = "-1"
            RadComboBoxCapitolo.Items.Clear()
            RadComboBoxCapitolo.Items.Add(itemVuoto)
            For Each dataRow As DataRow In dataTable.Rows
                Dim item As New RadComboBoxItem()
                item.Text = DirectCast(dataRow("COD"), String)
                item.Value = dataRow("ID").ToString()
                item.Attributes.Add("DESCRIZIONE", dataRow("DESCRIZIONE"))
                RadComboBoxCapitolo.Items.Add(item)
                item.DataBind()
            Next
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
        End Try
    End Sub

    Private Sub caricaVoce()
        Dim condizioneRicerca As String = ""
        If IsNumeric(RadComboBoxCapitolo.SelectedValue) AndAlso RadComboBoxCapitolo.SelectedValue > 0 Then
            condizioneRicerca = " WHERE PF_VOCI.ID_CAPITOLO=" & RadComboBoxCapitolo.SelectedValue
        End If
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SELECT MAX(ID) AS ID,CODICE,DESCRIZIONE FROM SISCOM_MI.PF_VOCI " & condizioneRicerca & " GROUP BY CODICE,DESCRIZIONE ORDER BY CODICE ASC"
            Dim adapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            adapter.Dispose()
            Dim itemVuoto As New RadComboBoxItem()
            itemVuoto.Text = ""
            itemVuoto.Value = "-1"
            RadComboBoxVoce.Items.Clear()
            RadComboBoxVoce.Items.Add(itemVuoto)
            For Each dataRow As DataRow In dataTable.Rows
                Dim item As New RadComboBoxItem()
                item.Text = DirectCast(dataRow("CODICE"), String)
                item.Value = dataRow("ID").ToString()
                item.Attributes.Add("DESCRIZIONE", dataRow("DESCRIZIONE"))
                RadComboBoxVoce.Items.Add(item)
                item.DataBind()
            Next
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
        End Try
    End Sub

    Private Sub caricaServizio()
        Dim condizioneRicerca As String = ""
        If IsNumeric(RadComboBoxCapitolo.SelectedValue) AndAlso RadComboBoxCapitolo.SelectedValue > 0 Then
            condizioneRicerca = " AND PF_VOCI_IMPORTO.ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE PF_VOCI.ID_CAPITOLO=" & RadComboBoxCapitolo.SelectedValue & ")"
        End If
        If IsNumeric(RadComboBoxVoce.SelectedValue) AndAlso RadComboBoxVoce.SelectedValue > 0 Then
            condizioneRicerca &= " AND PF_VOCI_IMPORTO.ID_VOCE=" & RadComboBoxVoce.SelectedValue
        End If
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            par.cmd.CommandText = "SELECT MAX(PF_VOCI_IMPORTO.ID) AS ID," _
                & " CODICE AS COD," _
                & " PF_VOCI_IMPORTO.DESCRIZIONE AS DESCRIZIONE " _
                & " FROM SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.PF_VOCI " _
                & " WHERE PF_VOCI.ID = PF_VOCI_IMPORTO.ID_VOCE " _
                & condizioneRicerca _
                & " GROUP BY PF_VOCI.CODICE,PF_VOCI_IMPORTO.DESCRIZIONE " _
                & " ORDER BY 2 ASC "
            Dim adapter As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dataTable As New DataTable()
            adapter.Fill(dataTable)
            adapter.Dispose()
            Dim itemVuoto As New RadComboBoxItem()
            itemVuoto.Text = ""
            itemVuoto.Value = "-1"
            RadComboBoxServizio.Items.Clear()
            RadComboBoxServizio.Items.Add(itemVuoto)
            For Each dataRow As DataRow In dataTable.Rows
                Dim item As New RadComboBoxItem()
                item.Text = DirectCast(dataRow("DESCRIZIONE"), String)
                item.Value = dataRow("ID").ToString()
                item.Attributes.Add("DESCRIZIONE", dataRow("COD"))
                RadComboBoxServizio.Items.Add(item)
                item.DataBind()
            Next
            If RadComboBoxServizio.Items.Count = 1 Then
                RadComboBoxServizio.Enabled = False
            ElseIf RadComboBoxServizio.Items.Count = 2 Then
                RadComboBoxServizio.SelectedValue = RadComboBoxServizio.Items(1).Value
                RadComboBoxServizio.Enabled = False
            Else
                RadComboBoxServizio.Enabled = True
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
        End Try
    End Sub

    Protected Sub RadComboBoxVoce_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxVoce.SelectedIndexChanged
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            caricaServizio()
            DataGrid1.Rebind()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub

    Protected Sub RadComboBoxCapitolo_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxCapitolo.SelectedIndexChanged
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            caricaVoce()
            caricaServizio()
            DataGrid1.Rebind()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
        End Try
    End Sub

    Protected Sub RadComboBoxServizio_SelectedIndexChanged(sender As Object, e As Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxServizio.SelectedIndexChanged
        DataGrid1.Rebind()
    End Sub

    Protected Sub CheckBoxListFiltroAnno_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CheckBoxListFiltroAnno.SelectedIndexChanged
        DataGrid1.Rebind()
    End Sub
End Class
