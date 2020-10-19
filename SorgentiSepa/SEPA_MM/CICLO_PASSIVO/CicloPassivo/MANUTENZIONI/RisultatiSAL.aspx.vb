'*** LISTA RISULTATO MANUTENZIONI CONSUNTIVATI da EMETTERE , RISTAMPARE o ANNULLARE i SAL
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data


Partial Class MANUTENZIONI_RisultatiSAL
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID

            If Not IsNothing(lstListaRapporti) Then
                lstListaRapporti.Clear()
            End If


            'BindGrid()

            Session.Add("NOME_FILE", "")

        End If

    End Sub


    Private Sub BindGrid()
        Dim FlagConnessione As Boolean


        Try

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

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " ubicato in: " & Left(Replace(e.Item.Cells(3).Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        'e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " ubicato in: " & Left(Replace(e.Item.Cells(3).Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

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

    'Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click


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


    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "ODL/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "DATA", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "UBICAZIONE", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "SERVIZIO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "VOCE DGR", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "STATO", 0)

    '                K = 2

    '                .SetColumnWidth(1, 2, 10)
    '                .SetColumnWidth(3, 5, 45)
    '                .SetColumnWidth(6, 6, 15)

    '                For Each oDataGridItem In Me.DataGrid1.Items

    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, oDataGridItem.Cells(7).Text & "/" & oDataGridItem.Cells(8).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, oDataGridItem.Cells(2).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, oDataGridItem.Cells(3).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, oDataGridItem.Cells(4).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, oDataGridItem.Cells(5).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, oDataGridItem.Cells(6).Text, 0)

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





    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Dim FlagConnessione As Boolean = False
        Dim sFiliale As String = "-1"
        If Session.Item("LIVELLO") <> "1" Then
            sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
        End If
        Try
            Dim sOrder As String
            Dim sStringaSql As String

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

            sOrder = "  order by ODL_ANNO asc,PROGR asc  "

            Dim condizioneVision As String = ""
            If Not IsNothing(Request.QueryString("CPV")) AndAlso Request.QueryString("CPV") <> "-1" Then
                condizioneVision = " AND MANUTENZIONI.PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & Request.QueryString("CPV") & "')"
                If Not IsNothing(Request.QueryString("NSV")) AndAlso Request.QueryString("NSV") <> "-1" Then
                    condizioneVision = " AND MANUTENZIONI.PROGR||'/'||ANNO IN (SELECT CODICE_ODL FROM SISCOM_MI.IMPORT_STR WHERE CODICE_PROGETTO_VISION='" & Request.QueryString("CPV") & "' AND NUMERO_SAL='" & Request.QueryString("NSV") & "')"
                End If
            End If


            sStringaSql = " select  MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                        & " (MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) as ""ODL_ANNO""," _
                                        & " to_char(to_date(substr(MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INIZIO_ORDINE," _
                                        & " COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                        & " TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO""," _
                                        & " PF_VOCI_IMPORTO.DESCRIZIONE AS ""SERVIZIO_VOCI""," _
                                        & " TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                                        & " SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_STATI_ODL " _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=2 " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PAGAMENTO is null " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                        & "   and siscom_mi.manutenzioni.id not in (select id_manutenzione from siscom_mi.integrazione_Str where stato=1) " _
                        & condizioneVision


            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR
            End If

            If sFiliale <> "-1" Then
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
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=" & sValoreServizio
            Else
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO is not null "
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
                                    & "SISCOM_MI.TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO""," _
                                    & "SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE AS ""SERVIZIO_VOCI""," _
                                    & " TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                                    & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO " _
                          & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_STATI_ODL " _
                          & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                          & "   and   SISCOM_MI.MANUTENZIONI.STATO=2 " _
                          & "   and   SISCOM_MI.MANUTENZIONI.ID_PAGAMENTO is null " _
                          & "   and   SISCOM_MI.MANUTENZIONI.STATO=SISCOM_MI.TAB_STATI_ODL.ID (+) " _
                          & "   and siscom_mi.manutenzioni.id not in (select id_manutenzione from siscom_mi.integrazione_Str where stato=1) " _
                          & condizioneVision

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR
            End If


            If sFiliale <> "-1" Then
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
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=" & sValoreServizio
            Else
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO is not null "
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

            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            Session.Add("DT_EXP", dt)
            TryCast(sender, RadGrid).DataSource = dt
            NumeroElementi = dt.Rows.Count
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)

        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.EventArgs) Handles btnProcedi.Click
        Dim oDataGridItem As GridDataItem
        'Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim chkExport As RadButton
        Dim Trovato As Boolean
        Dim i As Integer

        Dim gen As Epifani.ListaGenerale


        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            If chkExport.Checked Then

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                If Not IsNothing(lstListaRapporti) Then
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(0).Text Then  ''SISCOM_MI.MANUTENZIONI.ID
                            Trovato = True
                            Exit For
                        End If
                    Next
                End If
                

                If Trovato = False Then
                    gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(3).Text)
                    lstListaRapporti.Add(gen)
                    'Me.Label3.Value = Val(Label3.Value) + 1
                    gen = Nothing
                End If
            Else

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(3).Text Then
                        Trovato = True
                        Exit For
                    End If
                Next

                If Trovato = True Then
                    i = 0
                    For Each gen In lstListaRapporti
                        If gen.STR = oDataGridItem.Cells(3).Text Then

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

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreServizio = Request.QueryString("SV")

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")


            sOrdinamento = Request.QueryString("ORD")

            Session.Remove("NOME_FILE")

            Response.Write("<script>location.replace('SAL.aspx?FO=" & sValoreFornitore _
                                                           & "&AP=" & sValoreAppalto _
                                                           & "&SV=" & sValoreServizio _
                                                           & "&DAL=" & sValoreData_Dal _
                                                           & "&AL=" & sValoreData_Al _
                                                           & "&ORD=" & sOrdinamento _
                                                           & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                           & "&PROVENIENZA=RISULTATI_SAL" _
                                                    & "');</script>")

        Else
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        If Not IsNothing(lstListaRapporti) Then
            lstListaRapporti.Clear()
        End If
        Session.Remove("NOME_FILE")
        Response.Write("<script>document.location.href=""RicercaSAL.aspx""</script>")
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        If Not IsNothing(lstListaRapporti) Then
            lstListaRapporti.Clear()
        End If
        Session.Remove("NOME_FILE")
        Page.Dispose()
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Private Sub MANUTENZIONI_RisultatiSAL_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
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
End Class
