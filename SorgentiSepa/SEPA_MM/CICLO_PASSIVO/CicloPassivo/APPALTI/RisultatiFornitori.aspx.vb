Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data
Imports System
Imports System.Collections.Generic
Imports System.Web.UI.WebControls

Partial Class APPALTI_RisultatiFornitori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim connData As CM.datiConnessione = Nothing
    Private Property StringaRicerca() As String
        Get
            If Not (ViewState("StringaRicerca") Is Nothing) Then
                Return CStr(ViewState("StringaRicerca"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("StringaRicerca") = value
        End Set

    End Property
    Private Property Filtro1() As String
        Get
            If Not (ViewState("Filtro1") Is Nothing) Then
                Return CStr(ViewState("Filtro1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("Filtro1") = value
        End Set
    End Property
    Private Property Filtro2() As String
        Get
            If Not (ViewState("Filtro2") Is Nothing) Then
                Return CStr(ViewState("Filtro2"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("Filtro2") = value
        End Set
    End Property
    Private Property FiltroComplessivo() As String
        Get
            If Not (ViewState("FiltroComplessivo") Is Nothing) Then
                Return CStr(ViewState("FiltroComplessivo"))
            Else
                Return ""
            End If
        End Get
        Set(ByVal value As String)
            ViewState("FiltroComplessivo") = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        labelNORisultati.Visible = False
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGrid1.ClientID
            IDFornitoreSelezionato.Value = "-1"
        End If
    End Sub
    Private gridMessage1 As String = Nothing
    Private gridMessage2 As String = Nothing
    Protected Sub RadGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('IDFornitoreSelezionato').value='" & dataItem("ID").Text & "';document.getElementById('txtmia').value='Hai selezionato il fornitore " & Replace(dataItem("RAGIONE_SOCIALE").Text, "'", "\'") & "';")
                e.Item.Attributes.Add("onDblclick", "ApriSchedaFornitore();")
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Try
            Dim codiceFiscale As String = UCase(Request.QueryString("CF"))
            Dim codiceFornitore As String = UCase(Request.QueryString("CO"))
            Dim RagioneSociale As String = UCase(Request.QueryString("RA"))
            Dim partitaIVA As String = UCase(Request.QueryString("PI"))
            CODFIS.Value = codiceFiscale
            PARIVA.Value = partitaIVA
            RAGSOC.Value = RagioneSociale
            CODFOR.Value = codiceFornitore
            StringaRicerca = "SELECT * FROM SISCOM_MI.FORNITORI WHERE "
            If codiceFornitore <> "---" Then
                If codiceFornitore.IndexOf("*") <> -1 Then
                    codiceFornitore = Replace(codiceFornitore, "*", "%")
                    StringaRicerca &= "SISCOM_MI.FORNITORI.COD_FORNITORE LIKE '" & par.PulisciStrSql(codiceFornitore) & "' AND "
                Else
                    StringaRicerca &= "SISCOM_MI.FORNITORI.COD_FORNITORE = '" & par.PulisciStrSql(codiceFornitore) & "' AND "
                End If

            Else
                StringaRicerca &= "(SISCOM_MI.FORNITORI.COD_FORNITORE LIKE '%' OR SISCOM_MI.FORNITORI.COD_FORNITORE IS NULL) AND "
            End If
            If RagioneSociale <> "---" Then
                If RagioneSociale.IndexOf("*") <> -1 Then
                    RagioneSociale = Replace(RagioneSociale, "*", "%")
                    StringaRicerca &= " upper(SISCOM_MI.FORNITORI.RAGIONE_SOCIALE) LIKE '" & par.PulisciStrSql(RagioneSociale.ToUpper) & "' AND "
                Else
                    StringaRicerca &= " upper(SISCOM_MI.FORNITORI.RAGIONE_SOCIALE) = '" & par.PulisciStrSql(RagioneSociale.ToUpper) & "' AND "
                End If
            Else
                StringaRicerca &= "(SISCOM_MI.FORNITORI.RAGIONE_SOCIALE LIKE '%' OR SISCOM_MI.FORNITORI.RAGIONE_SOCIALE IS NULL) AND "
            End If
            If codiceFiscale <> "---" Then
                If codiceFiscale.IndexOf("*") <> -1 Then
                    codiceFiscale = Replace(codiceFiscale, "*", "%")
                    StringaRicerca &= "SISCOM_MI.FORNITORI.COD_FISCALE LIKE '" & par.PulisciStrSql(codiceFiscale) & "' AND "
                Else
                    StringaRicerca &= "SISCOM_MI.FORNITORI.COD_FISCALE='" & par.PulisciStrSql(codiceFiscale) & "' AND "
                End If

            Else
                StringaRicerca &= "(SISCOM_MI.FORNITORI.COD_FISCALE LIKE '%' OR SISCOM_MI.FORNITORI.COD_FISCALE IS NULL) AND "
            End If
            If partitaIVA <> "---" Then
                If partitaIVA.IndexOf("*") <> -1 Then
                    partitaIVA = Replace(partitaIVA, "*", "%")
                    StringaRicerca &= "SISCOM_MI.FORNITORI.PARTITA_IVA LIKE '" & par.PulisciStrSql(partitaIVA) & "' "
                Else
                    StringaRicerca &= "SISCOM_MI.FORNITORI.PARTITA_IVA='" & par.PulisciStrSql(partitaIVA) & "' "
                End If
            Else
                StringaRicerca &= "(SISCOM_MI.FORNITORI.PARTITA_IVA LIKE '%' OR SISCOM_MI.FORNITORI.PARTITA_IVA IS NULL)"
            End If
            'StringaRicerca &= & "ORDER BY RAGIONE_SOCIALE ASC"
            par.cmd.CommandText = StringaRicerca
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            If connData.Connessione.State = Data.ConnectionState.Open Then
                connData.chiudi()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " RadGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadButtonNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles RadButtonNuovaRicerca.Click
        If Not IsNothing(Session.Item("dtExport")) Then
            Session.Remove("dtExport")
        End If
        Response.Redirect("RicercaFornitore.aspx")
    End Sub
    Protected Sub RadButtonEsci_Click(sender As Object, e As System.EventArgs) Handles RadButtonEsci.Click
        If Not IsNothing(Session.Item("dtExport")) Then
            Session.Remove("dtExport")
        End If
        Response.Redirect("../../pagina_home_ncp.aspx")
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        RadGrid1.AllowPaging = False
        RadGrid1.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In RadGrid1.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In RadGrid1.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In RadGrid1.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In RadGrid1.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        RadGrid1.AllowPaging = True
        RadGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        RadGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "FORNITORI", "FORNITORI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
    Private Function ShowCheckedItems(ByVal comboBox As RadComboBox, ByVal campo As String) As String
        RadGrid1.MasterTableView.FilterExpression = ""
        Dim filterExpression As String = ""
        Dim filtro As String = ""
        Select Case campo
            Case "COD_FORNITORE"
                Filtro1 = ""
                filterExpression = " [COD_FORNITORE] IN ("
                filtro = " AND COD_FORNITORE IN ("
                Dim elementi As String = HFFiltroEvento.Value
                filterExpression &= elementi & ")"
                filtro &= elementi & ")"
                If elementi = "" Then
                    filtro = ""
                    filterExpression = ""
                    HFFiltroEvento.Value = ""
                End If
                Filtro1 = filterExpression
            Case "RAGIONE_SOCIALE"
                Filtro2 = ""
                filterExpression = "[RAGIONE_SOCIALE] IN ("
                filtro = " AND RAGIONE_SOCIALE IN ("
                Dim elementi As String = HFFiltroEvento2.Value
                filterExpression &= elementi & ")"
                filtro &= elementi & ")"
                If elementi = "" Then
                    filtro = ""
                    filterExpression = ""
                    HFFiltroEvento2.Value = ""
                End If
                Filtro2 = filterExpression
            Case Else
                filtro = ""
                filterExpression = ""
        End Select
        filterExpression = ""
        If Filtro1 <> "" Then
            If filterExpression = "" Then
                filterExpression = Filtro1
            Else
                filterExpression &= " AND " & Filtro1
            End If
        End If
        If Filtro2 <> "" Then
            If filterExpression = "" Then
                filterExpression = Filtro2
            Else
                filterExpression &= " AND " & Filtro2
            End If
        End If
        FiltroComplessivo = filterExpression
        Return filterExpression
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs)
        Select Case sender.CommandArgument
            Case "COD_FORNITORE"
                Dim filtro = ShowCheckedItems(TryCast(TryCast(sender.parent, GridTableCell).FindControl("RadComboBoxFiltroCliente"), RadComboBox), sender.CommandArgument)
            Case "RAGIONE_SOCIALE"
                ShowCheckedItems(TryCast(TryCast(sender.parent, GridTableCell).FindControl("RadComboBoxFiltroCliente2"), RadComboBox), sender.CommandArgument)
        End Select
    End Sub
    Protected Sub RicaricaComboTelerik(ByVal RadCombo As RadComboBox, ByVal campo As String, ByVal filtro As String)
        RadCombo.Items.Clear()
        RadCombo.DataSource = GetDataTable(StringaRicerca & " " & filtro, campo)
        RadCombo.DataTextField = campo
        RadCombo.DataValueField = campo
        RadCombo.DataBind()
    End Sub

    Protected Function RicaricaComboTelerik2(ByVal RadCombo As RadComboBox, ByVal campo As String, ByVal filtro As String) As Data.DataTable
        RadCombo.Items.Clear()
        Return GetDataTable(StringaRicerca & " " & filtro, campo)
    End Function

    Public Function GetDataTable(ByVal StringaRicerca As String, ByVal campo As String) As DataTable
        Dim indiceFrom As String = StringaRicerca.LastIndexOf("FROM")
        If indiceFrom = -1 Then
            Return Nothing
        Else
            connData.apri()
            StringaRicerca = Right(StringaRicerca, Len(StringaRicerca) - indiceFrom)
            par.cmd.CommandText = String.Format("SELECT DISTINCT {0} " & StringaRicerca & " ORDER BY 1", campo)
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            connData.chiudi()
            Return dt
        End If
    End Function

    Protected Sub RadComboBoxFiltroCliente_OnItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs)
        'Dim combo As RadComboBox = TryCast(sender, RadComboBox)
        'Dim lista As New Generic.List(Of String)
        'lista.AddRange(HFFiltroEvento.Value.Split(","c))
        'Dim dt As Data.DataTable = RicaricaComboTelerik2(combo, "COD_FORNITORE", "")
        'combo.Items.Clear()
        'For Each row As DataRow In dt.Rows
        '    Dim item As New RadComboBoxItem(row("COD_FORNITORE").ToString(), row("COD_FORNITORE").ToString())
        '    combo.Items.Add(item)
        '    If lista.Contains("'" & item.Value & "'") Then
        '        item.Checked = True
        '    End If
        'Next
    End Sub
    Protected Sub RadComboBoxFiltroCliente2_OnItemsRequested(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs)
        'Dim combo As RadComboBox = TryCast(sender, RadComboBox)
        'Dim lista As New Generic.List(Of String)
        'lista.AddRange(HFFiltroEvento2.Value.Split(","c))
        'Dim dt As Data.DataTable = RicaricaComboTelerik2(combo, "RAGIONE_SOCIALE", "")
        'combo.Items.Clear()
        'For Each row As DataRow In dt.Rows
        '    Dim item As New RadComboBoxItem(row("RAGIONE_SOCIALE").ToString(), row("RAGIONE_SOCIALE").ToString())
        '    combo.Items.Add(item)
        '    If lista.Contains("'" & item.Value & "'") Then
        '        item.Checked = True
        '    End If
        'Next
    End Sub

    Protected Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        'If e.CommandName = RadGrid.FilterCommandName Then
        '    Dim filterPair As Pair = DirectCast(e.CommandArgument, Pair)
        '    gridMessage1 = "Current Filter function: '" + filterPair.First + "' for column '" + filterPair.Second + "'"
        '    Dim filterBox As TextBox = CType((CType(e.Item, GridFilteringItem))(filterPair.Second.ToString()).Controls(0), TextBox)
        '    gridMessage2 = "<br> Entered pattern for search: " + filterBox.Text
        '    MsgBox(gridMessage1)

        '    'RadGrid1.MasterTableView.FilterExpression = FilterExpression
        '    'RadGrid1.MasterTableView.Rebind();
        'End If
    End Sub

    Protected Sub RadGrid1_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid1.PreRender
        If RadGrid1.MasterTableView.FilterExpression.Contains("COD_FORNITORE") Or RadGrid1.MasterTableView.FilterExpression.Contains("RAGIONE_SOCIALE") Then
            RadGrid1.MasterTableView.FilterExpression = FiltroComplessivo
        Else
            If RadGrid1.MasterTableView.FilterExpression = "" Then
                RadGrid1.MasterTableView.FilterExpression &= FiltroComplessivo
            Else
                RadGrid1.MasterTableView.FilterExpression &= " and " & FiltroComplessivo
            End If
        End If
        RadGrid1.MasterTableView.Rebind()
    End Sub

    Private Sub APPALTI_RisultatiFornitori_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Private Sub RadGrid1_PageIndexChanged(sender As Object, e As GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        RadGrid1.CurrentPageIndex = e.NewPageIndex
    End Sub
End Class
