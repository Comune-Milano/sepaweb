Imports Telerik.Web.UI
Imports System.Data

Partial Class CICLO_PASSIVO_CicloPassivo_PAGAMENTI_RisultatiRicercaPagamentiUtenza
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public sValoreEsercizioFinanziarioR As String
    Public sValoreFornitore As String
    Public sValoreStruttura As String
    Public sOrdinamento As String
    Public sSelectWhere As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            Hftipo.Value = Request.QueryString("TIPO")
            If Request.QueryString("TIPO") = "C" Then
                Me.lblTitolo.Text = "Custodi - Ricerca CDP emessi"
            ElseIf Request.QueryString("TIPO") = "U" Then
                Me.lblTitolo.Text = "Utenze - Ricerca CDP emessi"
            ElseIf Request.QueryString("TIPO") = "M" Then
                Me.lblTitolo.Text = "Multe - Ricerca CDP emessi"
            ElseIf Request.QueryString("TIPO") = "COSAP" Then
                Me.lblTitolo.Text = "Cosap - Ricerca Cosap emessi"
            End If
            ' BindGrid()
        End If
    End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdFornitore').value='" & e.Item.Cells(10).Text & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(11).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdFornitore').value='" & e.Item.Cells(10).Text & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(11).Text & "'")
    '    End If
    'End Sub

    'Protected Sub btnStampaPagamento_Click(sender As Object, e As System.EventArgs) Handles btnStampaPagamento.Click
    '    If Not String.IsNullOrEmpty(Trim(txtid.Text)) Then
    '        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Apri('FatturePagaUt.aspx');", True)

    '        ' Dim script As String = "function f(){$find(""" + RadWindow1.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
    '        '  RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

    '        '  ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "msg", "window.showModalDialog('FatturePagaUt.aspx?IDPAG=" & txtid.Text & "&TIPO=" & Request.QueryString("TIPO") & "', 'window', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');", True)
    '    Else
    '        Response.Write("<script>alert('Nessun pagamento selezionato!');</script>")
    '    End If
    '    txtid.Text = ""
    'End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Response.Write("<script>location.replace('RicercaPagamentiUtenza.aspx?TIPO=" & Request.QueryString("TIPO") & "');</script>")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub


    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "';document.getElementById('txtIdFornitore').value='" & dataItem("ID_FORNITORE").Text & "';document.getElementById('txtIdAppalto').value='" & dataItem("ID_APPALTO").Text & "'")

            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnStampaPagamento').click();")
        End If
    End Sub





    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Dim FlagConnessione As Boolean
        Dim sStringaSql As String
        Dim sSelectWhere As String = ""
        Dim sOrder As String = ""
        Try
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))
            sValoreFornitore = Request.QueryString("FO")
            sValoreStruttura = Request.QueryString("ST")
            sOrdinamento = Request.QueryString("ORD")
            If Request.QueryString("TIPO") = "U" Then
                sSelectWhere = "  and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 12 " _
                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO IN (1,2) "
            ElseIf Request.QueryString("TIPO") = "C" Then
                sSelectWhere = "  and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 13 " _
                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO IN (1,2) "

            ElseIf Request.QueryString("TIPO") = "M" Then
                sSelectWhere = "  and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 14 " _
                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO IN (1,2) "
            ElseIf Request.QueryString("TIPO") = "COSAP" Then
                sSelectWhere = "  and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO = 16 " _
                            & "   and SISCOM_MI.PAGAMENTI.ID_STATO IN (1,2) "

            End If

            If sValoreStruttura <> "-1" Then
                If Request.QueryString("TIPO") = "U" Then
                    sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                & " where TIPO_PAGAMENTO=12 " _
                                                & "   and ID_STATO in (1,2) and ID_STRUTTURA=" & sValoreStruttura & ")"
                ElseIf Request.QueryString("TIPO") = "C" Then
                    sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                & " where TIPO_PAGAMENTO=13 " _
                                                & "   and ID_STATO in (1,2) and ID_STRUTTURA=" & sValoreStruttura & ")"
                ElseIf Request.QueryString("TIPO") = "M" Then
                    sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                & " where TIPO_PAGAMENTO=14 " _
                                                & "   and ID_STATO in (1,2) and ID_STRUTTURA=" & sValoreStruttura & ")"
                ElseIf Request.QueryString("TIPO") = "COSAP" Then
                    sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                & " where TIPO_PAGAMENTO=16 " _
                                                & "   and ID_STATO in (1,2) and ID_STRUTTURA=" & sValoreStruttura & ")"
                End If

            End If
            Select Case sOrdinamento
                Case "DATA_EMISSIONE"
                    sOrder = " order by SISCOM_MI.PAGAMENTI.DATA_EMISSIONE desc"
                Case "PROG_ANNO"
                    sOrder = " order by SISCOM_MI.PAGAMENTI.ANNO desc,SISCOM_MI.PAGAMENTI.PROGR desc"
                Case "SAL_ANNO"
                    sOrder = " order by SISCOM_MI.PAGAMENTI.ANNO desc,SISCOM_MI.PAGAMENTI.PROGR_APPALTO desc"
                Case "FORNITORE"
                    sOrder = " order by BENEFICIARIO desc"
                Case Else
                    sOrder = ""
            End Select
            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",(SISCOM_MI.PAGAMENTI.PROGR_APPALTO||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""SAL_ANNO"",'' as ""DATA_PRENOTAZIONE""," _
                        & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_EMISSIONE""," _
                        & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                        & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                        & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                        & " SISCOM_MI.PAGAMENTI.DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                        & " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                        & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO"",SISCOM_MI.PAGAMENTI.ID_FORNITORE,SISCOM_MI.PAGAMENTI.ID_APPALTO " _
                        & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.TAB_STATI_PAGAMENTI" _
                        & " where SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                        & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) "
            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                                                         & " from SISCOM_MI.PRENOTAZIONI  " _
                                                                         & " where ID_VOCE_PF in ( select distinct(ID) from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") ) "
            End If
            If sValoreFornitore <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            End If
            sStringaSql = sStringaSql & sSelectWhere & sOrder
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'da.Dispose()
            'DataGrid1.DataSource = dt
            'DataGrid1.DataBind()
            'Label1.Text = " " & dt.Rows.Count


            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "setDimensioni();", True)
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid1.IsExporting Then
            e.Item.Visible = False
        End If
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "PAGAMENTI", "PAGAMENTI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
