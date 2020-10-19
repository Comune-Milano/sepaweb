Imports Telerik.Web.UI
Imports System.Data

'*** RICERCA PAGAMENTI SELETTIVA

Partial Class PAGAMENTI_CANONE_RicercaPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sSelectWhere As String = ""
    Public sStringaSql As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            ' BindGrid()
        End If
    End Sub


    Private Sub BindGrid()

        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)

            Dim ds As New Data.DataSet()

            da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            'Label1.Text = " " & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Num. Repertorio: " & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('txtID_FORNITORE').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtID_APPALTO').value='" & e.Item.Cells(1).Text & "'")


    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il Num. Repertorio: " & Replace(e.Item.Cells(4).Text, "'", "\'") & "';document.getElementById('txtID_FORNITORE').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtID_APPALTO').value='" & e.Item.Cells(1).Text & "'")

    '    End If

    'End Sub



    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Dim sValoreTipo As String = ""

        Try


            If Me.txtID_APPALTO.Value = "" Then
                RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "", "")
            Else

                sValoreTipo = Request.QueryString("TIPO")   'DA_APPROVARE,DA_APPROVARE_IN_SCADENZA

                Response.Write("<script>location.replace('RisultatiPagamenti.aspx?FO=" & Me.txtID_FORNITORE.Value _
                                                                              & "&AP=" & Me.txtID_APPALTO.Value _
                                                                              & "&TIPO=" & sValoreTipo & "');</script>")


            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            ' e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il Num. Repertorio: " & Replace(dataItem("NUM_REPERTORIO").Text, "'", "\'") & "';document.getElementById('txtID_FORNITORE').value='" & dataItem("ID_FORNITORE").Text & "';document.getElementById('txtID_APPALTO').value='" & dataItem("ID_GRUPPO").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnCerca').click();")
        End If
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try

            Dim sFiliale As String = ""
            Dim sValoreTipo As String = ""


            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If


            sValoreTipo = Request.QueryString("TIPO")   'DA_APPROVARE,DA_APPROVARE_IN_SCADENZA
            Select Case sValoreTipo

                Case "DA_APPROVARE"
                    sSelectWhere = " where  TIPO_PAGAMENTO=6" _
                                 & "   and  ID_STATO=0 " _
                                 & "   and  IMPORTO_PRENOTATO>0 "

                    If sFiliale <> "" Then
                        sSelectWhere = sSelectWhere & " and " & sFiliale & ")"
                    Else
                        sSelectWhere = sSelectWhere & ")"
                    End If

                Case "DA_APPROVARE_IN_SCADENZA"

                    sSelectWhere = " where  TIPO_PAGAMENTO=6" _
                               & "   and  ID_STATO=0 " _
                               & "   and  IMPORTO_PRENOTATO>0 " _
                               & "   and  (NVL(TO_DATE(DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) "

                    If sFiliale <> "" Then
                        sSelectWhere = sSelectWhere & " and " & sFiliale & ")"
                    Else
                        sSelectWhere = sSelectWhere & ")"
                    End If

            End Select


            sStringaSql = "select distinct FORNITORI.ID as ID_FORNITORE,APPALTI.id_gruppo AS id_gruppo," _
                              & " trim(FORNITORI.COD_FORNITORE) as COD_FORNITORE," _
                              & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & " then  trim(RAGIONE_SOCIALE)  " _
                                 & " else  trim(COGNOME) || ' ' || trim(NOME) end as FORNITORE," _
                              & " trim(SISCOM_MI.APPALTI.NUM_REPERTORIO) || ' - ' || trim(SISCOM_MI.APPALTI.DESCRIZIONE) as NUM_REPERTORIO " _
                    & " from SISCOM_MI.FORNITORI, SISCOM_MI.APPALTI " _
                    & " where SISCOM_MI.APPALTI.id_gruppo IN (SELECT DISTINCT id_gruppo FROM siscom_mi.APPALTI a1 WHERE a1.ID IN (" _
                    & "(select distinct(ID_APPALTO) from SISCOM_MI.PRENOTAZIONI " & sSelectWhere _
                    & "  ) )and SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE (+) " _
                    & " order by FORNITORE "
            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "PAGAMENTIDAAPPROVARE", "PAGAMENTIDAAPPROVARE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
