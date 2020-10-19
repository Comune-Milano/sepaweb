'*** LISTA RISULTATO MANUTENZIONI CONSUNTIVATI da EMETTERE , RISTAMPARE o ANNULLARE i SAL RRS
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI


Partial Class RRS_RisultatiSAL_RRS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreServizio As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sOrdinamento As String

    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))

        If Not IsPostBack Then

            HFGriglia.Value = DataGrid1.ClientID
            lstListaRapporti.Clear()

            ' BindGrid()

            Session.Add("NOME_FILE", "")

        End If

    End Sub


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click

        lstListaRapporti.Clear()
        Session.Remove("NOME_FILE")

        Page.Dispose()
        Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")

    End Sub



    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If

    'End Sub

    'Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
    '    ''If e.NewPageIndex >= 0 Then
    '    ''    DataGrid1.CurrentPageIndex = e.NewPageIndex
    '    ''    BindGrid()
    '    ''End If

    'End Sub



    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
    End Function



    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Protected Sub btnProcedi_Click(sender As Object, e As System.EventArgs) Handles btnProcedi.Click
        Dim oDataGridItem As GridDataItem
        Dim chkExport As RadButton
        'Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim Trovato As Boolean
        Dim i As Integer

        Dim gen As Epifani.ListaGenerale


        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            If chkExport.Checked Then

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(3).Text Then  ''SISCOM_MI.MANUTENZIONI.ID
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = False Then
                    gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(2).Text)
                    lstListaRapporti.Add(gen)
                    'Me.Label3.Value = Val(Label3.Value) + 1
                    gen = Nothing
                End If
            Else

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(2).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = True Then
                    i = 0
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(2).Text Then

                            lstListaRapporti.RemoveAt(i)
                            'Me.Label3.Value = Val(Label3.Value) - 1
                            Exit For
                        End If
                        i = i + 1
                    Next
                    gen = Nothing

                    Dim indice As Integer = 0
                    For Each gen In lstListaRapporti
                        gen.ID = indice
                        indice += 1
                    Next

                End If
            End If
        Next


        If lstListaRapporti.Count > 0 Then

            Session.Add("ID", 0)

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreEsercizioFinanziarioR = Request.QueryString("EF_R")
            sOrdinamento = Request.QueryString("ORD")

            Session.Remove("NOME_FILE")

            Response.Write("<script>location.replace('SAL_RRS.aspx?FO=" & sValoreFornitore _
                                                               & "&AP=" & sValoreAppalto _
                                                               & "&SV=" & sValoreServizio _
                                                               & "&DAL=" & sValoreData_Dal _
                                                               & "&AL=" & sValoreData_Al _
                                                               & "&ORD=" & sOrdinamento _
                                                               & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                               & "&PROVENIENZA=RISULTATI_SAL_RRS" _
                                                    & "');</script>")

        Else
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        lstListaRapporti.Clear()
        Session.Remove("NOME_FILE")

        Response.Write("<script>document.location.href=""RicercaSAL_RRS.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        lstListaRapporti.Clear()
        Session.Remove("NOME_FILE")

        Page.Dispose()

        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub



    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
                DataGrid1.MasterTableView.GetColumn("ODL").Visible = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            'e.Item.Attributes.Add("onclick", "document.getElementById('txtid').value='" & dataItem("ID_MANUTENZIONE").Text & "'")
            'e.Item.Attributes.Add("onDblclick", "document.getElementById('btnProcedi').click();")
        End If
    End Sub

   


    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Dim FlagConnessione As Boolean
        Dim sOrder As String
        Dim sStringaSql As String

        Try

            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If


            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sOrdinamento = Request.QueryString("ORD")

            sOrder = "  order by ODL_ANNO desc  "


            sStringaSql = " select  MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                & " (MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) as ""ODL_ANNO""," _
                                & " to_char(to_date(substr(MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                & " COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                & " PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                                & " TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                                & " MANUTENZIONI.PROGR,MANUTENZIONI.ANNO,MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_STATI_ODL " _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO is null  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=2 " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PAGAMENTO is null " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) "

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR
            End If

            If sFiliale <> "" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
            End If

            If sValoreFornitore <> -1 Then
                If sValoreAppalto <> -1 Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                Else
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & sValoreFornitore & ")"
                End If
            ElseIf sValoreAppalto <> -1 Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
            End If

            If sValoreServizio <> -1 Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=" & sValoreServizio
            End If

            If sValoreData_Dal <> "" Then
                If sValoreData_Al <> "" Then
                    sStringaSql = sStringaSql & " and (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                Else
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                End If
            ElseIf sValoreData_Al <> "" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
            End If


            sStringaSql = sStringaSql & " union  select  SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_char(to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                    & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                    & " PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                                    & " TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                                    & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                          & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.PF_VOCI,SISCOM_MI.TAB_STATI_ODL " _
                          & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO is null  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.STATO=2 " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_PAGAMENTO is null " _
                          & "   and   SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) "

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR
            End If

            If sFiliale <> "" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
            End If

            If sValoreFornitore <> -1 Then
                If sValoreAppalto <> -1 Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
                Else
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_FORNITORE=" & sValoreFornitore & ")"
                End If
            ElseIf sValoreAppalto <> -1 Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_APPALTO=" & sValoreAppalto
            End If

            If sValoreServizio <> -1 Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=" & sValoreServizio
            End If

            If sValoreData_Dal <> "" Then
                If sValoreData_Al <> "" Then
                    sStringaSql = sStringaSql & " and (SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al & ")"
                Else
                    sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE>=" & sValoreData_Dal
                End If
            ElseIf sValoreData_Al <> "" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE<=" & sValoreData_Al
            End If
            sStringaSql = sStringaSql & sOrder
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            Session.Add("DT_EXP", dt)
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

    Protected Sub ButtonSelAll_Click(sender As Object, e As System.EventArgs)
        Try
            If hiddenSelTutti.Value = "1" Then
                For Each riga As GridItem In DataGrid1.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = True
                Next
            Else
                For Each riga As GridItem In DataGrid1.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = False
                Next
            End If
        Catch ex As Exception
            'If par.OracleConn.State = Data.ConnectionState.Open Then
            '    connData.chiudi()
            'End If
            'Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ButtonSelAll_Click - " & ex.Message)
            'Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub RRS_RisultatiSAL_RRS_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        'DataGrid1.AllowPaging = False
        'DataGrid1.Rebind()
        'Dim dtRecords As New DataTable()
        'For Each col As GridColumn In DataGrid1.Columns
        '    Dim colString As New DataColumn(col.UniqueName)
        '    If col.Visible = True Or col.UniqueName = "ODL_ANNO" Then
        '        dtRecords.Columns.Add(colString)
        '    End If
        'Next
        'For Each row As GridDataItem In DataGrid1.Items
        '    ' loops through each rows in RadGrid
        '    Dim dr As DataRow = dtRecords.NewRow()
        '    For Each col As GridColumn In DataGrid1.Columns
        '        'loops through each column in RadGrid
        '        If col.Visible = True Or col.UniqueName = "ODL_ANNO" Then
        '            dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
        '        End If
        '    Next
        '    dtRecords.Rows.Add(dr)
        'Next
        'Dim i As Integer = 0
        'For Each col As GridColumn In DataGrid1.Columns
        '    If col.Visible = True Or col.UniqueName = "ODL_ANNO" Then
        '        Dim colString As String = col.HeaderText
        '        dtRecords.Columns(i).ColumnName = colString
        '        i += 1
        '    End If
        'Next
        'dtRecords.Columns.RemoveAt(0)
        Dim dtRecords As Data.DataTable = Session.Item("DT_EXP")
        dtRecords.Columns.Remove(dtRecords.Columns.Item("ID_MANUTENZIONE"))
        dtRecords.Columns.Remove(dtRecords.Columns.Item("STATO"))
        dtRecords.Columns.Remove(dtRecords.Columns.Item("PROGR"))
        dtRecords.Columns.Remove(dtRecords.Columns.Item("ANNO"))
        dtRecords.Columns.Remove(dtRecords.Columns.Item("ID_PRENOTAZIONE_PAGAMENTO"))

        Esporta(dtRecords)
        DataGrid1.AllowPaging = True
        DataGrid1.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ODL", "ODL", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim numeroCheckati As Integer = 0
        For Each elemento As GridDataItem In DataGrid1.Items
            If CType(elemento.FindControl("CheckBox1"), RadButton).Checked = True Then
                numeroCheckati += 1
            End If
        Next
        Select Case numeroCheckati
            Case 0
                txtmia.Text = "Nessun ODL selezionato"
            Case 1
                txtmia.Text = "Selezionato 1 ODL"
            Case Else
                txtmia.Text = "Sono stati selezionati " & numeroCheckati & " ODL"
        End Select
    End Sub
End Class
