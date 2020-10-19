'*** LISTA RISULTATO SAL da RISTAMPARE, ANNULLARE o CAMBIARE FIRMA
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data


Partial Class MANUTENAZIONI_RisultatiSAL_FIRMA
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreStruttura As String
    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreServizio As String

    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public sValoreStato As String

    Public sValoreADP As String
    Public sValoreAnno As String
    Private isFilter As Boolean = False

    Public sOrdinamento As String


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            BindGrid()

            Session.Add("NOME_FILE", "")

        End If

    End Sub

    Private Sub BindGrid()
        Dim FlagConnessione As Boolean
        Dim sOrder As String
        Dim sStringaSql As String


        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If




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



    '  Protected Sub DataGrid1_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    'If e.Item.ItemType = ListItemType.Item Then
    '    '---------------------------------------------------         
    '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '    '---------------------------------------------------         
    '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    'End If

    'If e.Item.ItemType = ListItemType.AlternatingItem Then
    '    '---------------------------------------------------         
    '    ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '    '---------------------------------------------------         
    '    e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '    e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '    e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    'End If

    ' End Sub


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    'Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click

    '    Dim oDataGridItem As DataGridItem
    '    Dim myExcelFile As New CM.ExcelFile
    '    Dim K As Long

    '    Dim NomeFile As String = CType(HttpContext.Current.Session.Item("NOME_FILE"), String)


    '    If Strings.Trim(NomeFile) = "" Then

    '        If Me.DataGrid1.Items.Count > 0 Then

    '            NomeFile = "Export_" & Format(Now, "yyyyMMddHHmmss")
    '            Session.Add("NOME_FILE", NomeFile)

    '            With myExcelFile

    '                .CreateFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".xls")

    '                .PrintGridLines = False
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
    '                .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
    '                .SetDefaultRowHeight(14)
    '                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
    '                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
    '                .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
    '                .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)


    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "PROG/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "SAL/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "EMISSIONE", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "BENEFICIARIO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "IMP. CONSUNTIVATO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NUM. REPERTORIO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DESCRIZIONE", 0)


    '                K = 2

    '                .SetColumnWidth(1, 3, 12)
    '                .SetColumnWidth(4, 4, 45)
    '                .SetColumnWidth(5, 6, 20)
    '                .SetColumnWidth(7, 7, 45)


    '                For Each oDataGridItem In Me.DataGrid1.Items

    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, oDataGridItem.Cells(1).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, oDataGridItem.Cells(2).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, oDataGridItem.Cells(4).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, oDataGridItem.Cells(5).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, oDataGridItem.Cells(6).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, oDataGridItem.Cells(8).Text, 0)
    '                    If Trim(oDataGridItem.Cells(9).Text) = "&nbsp;" Then
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, " ", 0)
    '                    Else
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, oDataGridItem.Cells(9).Text, 0)
    '                    End If
    '                    K = K + 1
    '                Next

    '                .CloseFile()
    '            End With

    '            Dim objCrc32 As New Crc32()
    '            Dim strmZipOutputStream As ZipOutputStream
    '            Dim zipfic As String

    '            zipfic = Server.MapPath("..\..\..\FileTemp\" & NomeFile & ".zip")

    '            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
    '            strmZipOutputStream.SetLevel(6)
    '            '
    '            Dim strFile As String
    '            strFile = Server.MapPath("..\..\..\FileTemp\" & NomeFile & ".xls")

    '            Dim strmFile As FileStream = File.OpenRead(strFile)
    '            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '            '
    '            strmFile.Read(abyBuffer, 0, abyBuffer.Length)

    '            Dim sFile As String = Path.GetFileName(strFile)
    '            Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '            Dim fi As New FileInfo(strFile)
    '            theEntry.DateTime = fi.LastWriteTime
    '            theEntry.Size = strmFile.Length
    '            strmFile.Close()
    '            objCrc32.Reset()
    '            objCrc32.Update(abyBuffer)
    '            theEntry.Crc = objCrc32.Value
    '            strmZipOutputStream.PutNextEntry(theEntry)
    '            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '            strmZipOutputStream.Finish()
    '            strmZipOutputStream.Close()

    '            File.Delete(strFile)
    '            Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".zip")
    '            'Response.Write("<script>window.open('../../../FileTemp/" & Me.FileNameXLS.Value & ".xls','','');</script>")

    '        Else
    '            Response.Write("<script>alert('Nessun Pagamento Trovato!');</script>")
    '        End If

    '    Else
    '        Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".zip")
    '    End If

    'End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.EventArgs) Handles btnProcedi.Click
        If Me.txtid.Text = "" Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Else

            Session.Add("ID", txtid.Text)

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreStato = Request.QueryString("ST")

            sOrdinamento = Request.QueryString("ORD")

            Session.Remove("NOME_FILE")

            sValoreADP = Request.QueryString("ADP")

            Response.Write("<script>location.replace('SAL.aspx?FO=" & sValoreFornitore _
                                                           & "&AP=" & sValoreAppalto _
                                                           & "&SV=" & sValoreServizio _
                                                           & "&ST=" & sValoreStato _
                                                           & "&STR=" & sValoreStruttura _
                                                           & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                           & "&ADP=" & sValoreADP _
                                                           & "&PROVENIENZA=RISULTATI_SAL_FIRMA" _
                                                           & "');</script>")

        End If

    End Sub

    Protected Sub brnRicerca_Click(sender As Object, e As System.EventArgs) Handles brnRicerca.Click
        Session.Remove("NOME_FILE")
        Response.Write("<script>document.location.href=""RicercaSAL_FIRMA.aspx""</script>")
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Session.Remove("NOME_FILE")
        Page.Dispose()
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub


    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnProcedi').click();")
        End If
    End Sub

    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
            If e.CommandName = RadGrid.FilterCommandName Then
                isFilter = True
            Else
                isFilter = False
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            Dim sOrder As String
            Dim sStringaSql As String
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStruttura = Request.QueryString("STR")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreStato = Request.QueryString("ST")

            sValoreADP = Request.QueryString("ADP")
            sValoreAnno = Request.QueryString("ANNO")



            sOrder = " order by SISCOM_MI.APPALTI.NUM_REPERTORIO desc, SISCOM_MI.PAGAMENTI.PROGR_APPALTO desc, SISCOM_MI.PAGAMENTI.ANNO desc, SISCOM_MI.PAGAMENTI.DATA_EMISSIONE desc"

            'sOrder = "  order by SISCOM_MI.PAGAMENTI.ANNO desc,SISCOM_MI.PAGAMENTI.PROGR asc  "


            'STATO PAGAMENTO    0=PRENOTATO 1=EMESSO 5=PAGATO

            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",(SISCOM_MI.PAGAMENTI.PROGR_APPALTO||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""SAL_ANNO"",'' as ""DATA_PRENOTAZIONE""," _
                                 & " TO_DATE(SISCOM_MI.pagamenti.DATA_EMISSIONE,'YYYYMMDD') as ""DATA_EMISSIONE""," _
                                 & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                 & "     then  COD_FORNITORE || ' - ' || trim(FORNITORI.RAGIONE_SOCIALE) " _
                                 & "     else  COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                 & " SISCOM_MI.PAGAMENTI.DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                                 & " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                                 & " SISCOM_MI.APPALTI.NUM_REPERTORIO," _
                                 & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO""" _
                         & " from SISCOM_MI.PAGAMENTI," _
                              & " SISCOM_MI.FORNITORI," _
                              & " SISCOM_MI.TAB_STATI_PAGAMENTI," _
                              & " SISCOM_MI.APPALTI" _
                         & " where SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=3 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) "


            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO " _
                                                                         & " from SISCOM_MI.PRENOTAZIONI " _
                                                                         & " where ID in (select ID_PRENOTAZIONE_PAGAMENTO " _
                                                                                      & " from SISCOM_MI.MANUTENZIONI " _
                                                                                      & " where ID_PF_VOCE_IMPORTO is not null " _
                                                                                      & " and manutenzioni.stato<>5 " _
                                                                                      & " and ID_PIANO_FINANZIARIO = " & sValoreEsercizioFinanziarioR & ")" _
                                                                         & " and TIPO_PAGAMENTO=3 " _
                                                                         & " and ID_STATO>=2 " _
                                                                         & " and ID_PAGAMENTO is not null ) "

            End If


            If sValoreADP <> "" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.PROGR=" & sValoreADP
            End If

            If sValoreAnno <> "" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ANNO=" & sValoreAnno
            End If

            If par.IfEmpty(sValoreStato, -1) <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.STATO_FIRMA=" & sValoreStato
            End If

            If sValoreFornitore <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            End If

            If sValoreAppalto <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_APPALTO=" & sValoreAppalto
            End If

            If sValoreServizio <> "-1" Then
                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                              & " where ID_VOCE_PF_IMPORTO=" & sValoreServizio _
                                                                              & "   and ID_STRUTTURA=" & sValoreStruttura & ")"
                Else
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI where ID_VOCE_PF_IMPORTO=" & sValoreServizio & ")"
                End If
            Else
                If sValoreStruttura <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                              & " where TIPO_PAGAMENTO=3 and ID_STRUTTURA=" & sValoreStruttura & ") "

                End If
            End If

            sStringaSql = sStringaSql & sOrder
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "MANUTENZIONE", "MANUTENZIONE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
