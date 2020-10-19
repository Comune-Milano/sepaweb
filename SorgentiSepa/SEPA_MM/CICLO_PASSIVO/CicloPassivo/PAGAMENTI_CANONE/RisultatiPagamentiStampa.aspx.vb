'*** LISTA RISULTATO PAGAMENTI A CANONE da EMETTERE LA STAMPA DEL PAGAMENTO

Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data


Partial Class PAGAMENTI_CANONE_RisultatiPagamentiStampa
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreStruttura As String

    Public sOrdinamento As String

    Public sValoreTipo As String = ""
    Public sSelectWhere As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            BindGrid()
            sValoreTipo = Request.QueryString("TIPO")
            If sValoreTipo = "APPROVATI" Then
                lblTitolo.Text = "Ordini - Pagamenti a canone - Approvati"
            Else
                lblTitolo.Text = "Ordini - Pagamenti a canone - Emesso Sal"
            End If
            Session.Add("NOME_FILE", "")
        End If

    End Sub


    Private Sub BindGrid()
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
            sValoreAppalto = Request.QueryString("AP")
            sValoreStruttura = Request.QueryString("ST")

            sValoreTipo = Request.QueryString("TIPO")
            sOrdinamento = Request.QueryString("ORD")


            Select Case sValoreTipo

                Case "APPROVATI"
                    sSelectWhere = "  and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                                 & "   and SISCOM_MI.PAGAMENTI.ID_STATO=0 "

                    If sValoreStruttura <> "-1" Then
                        sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                                  & " where TIPO_PAGAMENTO=6 " _
                                                                                  & "   and ID_STATO in (1,2) and ID_STRUTTURA=" & sValoreStruttura & ")"

                    End If

                    Me.btnStampaPagamento.Visible = False
                    ' Me.btnExport.Visible = True

                    Me.btnVisualizza.Visible = True


                Case "APPROVATI_SCADENZA"

                    sSelectWhere = " and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                               & "   and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                               & "   and  SISCOM_MI.PAGAMENTI.DATA_STAMPA is null "


                    If sValoreStruttura <> "-1" Then
                        sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                                      & " where TIPO_PAGAMENTO=6 " _
                                                                                      & "   and ID_STATO=2 and ID_STRUTTURA=" & sValoreStruttura & ")"


                    End If

                    Me.btnStampaPagamento.Visible = True
                    ' Me.btnExport.Visible = True    'Non entrano tutti i bottoni nella maschera, per il momento quando visualizzo la stampa elimino export
                    Me.btnVisualizza.Visible = True

                Case "DA_STAMPARE_PAG"

                    sSelectWhere = " and SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                               & "   and  SISCOM_MI.PAGAMENTI.ID_STATO>0 "

                    If sValoreStruttura <> "-1" Then
                        sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                                  & " where TIPO_PAGAMENTO=6 " _
                                                                                  & "   and ID_STATO>=1 and ID_STRUTTURA=" & sValoreStruttura & ")"

                    End If

                    Me.btnStampaPagamento.Visible = True
                    '     Me.btnExport.Visible = True    'Non entrano tutti i bottoni nella maschera, per il momento quando visualizzo la stampa elimino export
                    Me.btnVisualizza.Visible = True

            End Select



            'sOrdinamento = Request.QueryString("ORD")
            'sOrder = " order by DATA_EMISSIONE desc,PROG_ANNO asc"

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

            'STATO PAGAMENTO    0=PRENOTATO 1=EMESSO 5=PAGATO

            '& " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE"","
            '& " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_PRENOTATO,'9G999G990D99')) AS ""IMPORTO_PRENOTATO"", " 
            '& " SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _

            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",(SISCOM_MI.PAGAMENTI.PROGR_APPALTO||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""SAL_ANNO"",'' as ""DATA_PRENOTAZIONE""," _
                                 & " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_EMISSIONE""," _
                                 & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                 & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                 & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                 & " SISCOM_MI.PAGAMENTI.DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                                 & " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                                 & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO"",SISCOM_MI.PAGAMENTI.ID_FORNITORE,SISCOM_MI.PAGAMENTI.ID_APPALTO " _
                                 & ",(select num_Repertorio from siscom_mi.appalti where appalti.id=pagamenti.id_appalto) as repertorio " _
                         & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.TAB_STATI_PAGAMENTI" _
                         & " where SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) "

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                                                         & " from SISCOM_MI.PRENOTAZIONI  " _
                                                                         & " where ID_VOCE_PF in ( select distinct(ID) from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") ) "
            End If

            'If par.IfEmpty(sValoreStato, -1) <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.STATO_FIRMA=" & sValoreStato
            'End If

            If sValoreFornitore <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            End If


            If sValoreAppalto <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_APPALTO=" & sValoreAppalto
            End If

            'If sValoreServizio <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI where ID_VOCE_PF_IMPORTO=" & sValoreServizio & ")"
            'End If


            sStringaSql = sStringaSql & sSelectWhere & " order by SISCOM_MI.PAGAMENTI.ANNO desc,SISCOM_MI.PAGAMENTI.PROGR desc"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            Dim ds As New Data.DataSet

            da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            DataGrid1.DataSource = ds
            DataGrid1.DataBind()

            '    Label1.Text = " " & ds.Tables(0).Rows.Count

            da.Dispose()
            ds.Dispose()

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


    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "PROG/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "SAL/ANNO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "EMISSIONE", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "BENEFICIARIO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "IMP. CONSUNTIVATO", 0)
    '                .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "DESCRIZIONE", 0)

    '                K = 2

    '                .SetColumnWidth(1, 3, 15)
    '                .SetColumnWidth(3, 3, 30)
    '                .SetColumnWidth(4, 4, 30)
    '                .SetColumnWidth(5, 5, 20)
    '                .SetColumnWidth(6, 6, 45)

    '                For Each oDataGridItem In Me.DataGrid1.Items

    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, oDataGridItem.Cells(1).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, oDataGridItem.Cells(2).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, oDataGridItem.Cells(4).Text, 0)
    '                    .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, oDataGridItem.Cells(5).Text, 0)

    '                    If oDataGridItem.Cells(7).Text = "&nbsp;" Then
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, 0, 4)
    '                    Else
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsNumber, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsRightAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, oDataGridItem.Cells(7).Text, 4)
    '                    End If

    '                    If oDataGridItem.Cells(8).Text = "&nbsp;" Then
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, "", 0)
    '                    Else
    '                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, oDataGridItem.Cells(8).Text, 0)
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

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click


        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", txtid.Text)

            'SELETTIVA
            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")

            sValoreStruttura = Request.QueryString("ST")

            sValoreTipo = Request.QueryString("TIPO")
            sOrdinamento = Request.QueryString("ORD")

            Session.Remove("NOME_FILE")

            Response.Write("<script>location.replace('Pagamenti.aspx?FO=" & sValoreFornitore _
                                                                 & "&AP=" & sValoreAppalto _
                                                                 & "&ID_A=" & Me.txtIdAppalto.Value _
                                                                 & "&ID_F=" & Me.txtIdFornitore.Value _
                                                                 & "&ST=" & sValoreStruttura _
                                                                 & "&TIPO=" & sValoreTipo _
                                                                 & "&ORD=" & sOrdinamento & "');</script>")


        End If
    End Sub

    Protected Sub btnStampaPagamento_Click(sender As Object, e As System.EventArgs) Handles btnStampaPagamento.Click



        'Response.Write("<script>window.open('StampaPagamento.aspx?PAG=" & Me.txtid.Text & "&CON=" & lIdConnessione & "','AttPagamento','');self.close();</script>")
        Response.Write("<script>window.open('StampaPagamento.aspx?PAG=" & Me.txtid.Text & "&CHIAMANTE=RICERCHE','AttPagamento','');</script>")

        'Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")

        'If sValoreTipo = "SCADENZA" Then
        '    BindGrid()
        'End If

        Me.txtStampa.Value = ""
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("NOME_FILE")

        sValoreTipo = Request.QueryString("TIPO")

        Response.Write("<script>location.replace('RicercaPagamentiS.aspx?TIPO=" & sValoreTipo & "');</script>")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("NOME_FILE")

        Page.Dispose()

        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid1.ItemCommand
        Try
            If e.CommandName = RadGrid.ExportToExcelCommandName Then
                isExporting.Value = "1"
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)

            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "';document.getElementById('txtIdFornitore').value='" & dataItem("ID_FORNITORE").Text & "';document.getElementById('txtIdAppalto').value='" & dataItem("ID_APPALTO").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
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
            sValoreAppalto = Request.QueryString("AP")
            sValoreStruttura = Request.QueryString("ST")

            sValoreTipo = Request.QueryString("TIPO")
            sOrdinamento = Request.QueryString("ORD")


            Select Case sValoreTipo

                Case "APPROVATI"
                    sSelectWhere = "  and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                                 & "   and SISCOM_MI.PAGAMENTI.ID_STATO=0 "

                    If sValoreStruttura <> "-1" Then
                        sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                                  & " where TIPO_PAGAMENTO=6 " _
                                                                                  & "   and ID_STATO in (1,2) and ID_STRUTTURA=" & sValoreStruttura & ")"

                    End If
                    Me.btnStampaPagamento.Visible = False
                    ' Me.btnExport.Visible = True
                    Me.btnVisualizza.Visible = True
                Case "APPROVATI_SCADENZA"
                    sSelectWhere = " and  SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                               & "   and  SISCOM_MI.PAGAMENTI.ID_STATO=1 " _
                               & "   and  SISCOM_MI.PAGAMENTI.DATA_STAMPA is null "
                    If sValoreStruttura <> "-1" Then
                        sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                                      & " where TIPO_PAGAMENTO=6 " _
                                                                                      & "   and ID_STATO=2 and ID_STRUTTURA=" & sValoreStruttura & ")"


                    End If

                    Me.btnStampaPagamento.Visible = True
                    ' Me.btnExport.Visible = True    'Non entrano tutti i bottoni nella maschera, per il momento quando visualizzo la stampa elimino export
                    Me.btnVisualizza.Visible = True

                Case "DA_STAMPARE_PAG"

                    sSelectWhere = " and SISCOM_MI.PAGAMENTI.TIPO_PAGAMENTO=6" _
                               & "   and  SISCOM_MI.PAGAMENTI.ID_STATO>0 "

                    If sValoreStruttura <> "-1" Then
                        sSelectWhere = sSelectWhere & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI " _
                                                                                  & " where TIPO_PAGAMENTO=6 " _
                                                                                  & "   and ID_STATO>=1 and ID_STRUTTURA=" & sValoreStruttura & ")"

                    End If

                    Me.btnStampaPagamento.Visible = True
                    '     Me.btnExport.Visible = True    'Non entrano tutti i bottoni nella maschera, per il momento quando visualizzo la stampa elimino export
                    Me.btnVisualizza.Visible = True

            End Select



            'sOrdinamento = Request.QueryString("ORD")
            'sOrder = " order by DATA_EMISSIONE desc,PROG_ANNO asc"

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

            'STATO PAGAMENTO    0=PRENOTATO 1=EMESSO 5=PAGATO

            '& " to_char(to_date(substr(SISCOM_MI.PAGAMENTI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE"","
            '& " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_PRENOTATO,'9G999G990D99')) AS ""IMPORTO_PRENOTATO"", " 
            '& " SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF_IMPORTO " _

            sStringaSql = " select SISCOM_MI.PAGAMENTI.ID,(SISCOM_MI.PAGAMENTI.PROGR||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""PROG_ANNO"",(SISCOM_MI.PAGAMENTI.PROGR_APPALTO||'/'||SISCOM_MI.PAGAMENTI.ANNO) as ""SAL_ANNO"",'' as ""DATA_PRENOTAZIONE""," _
                                 & " to_date(substr(SISCOM_MI.PAGAMENTI.DATA_EMISSIONE,1,8),'YYYYmmdd') as ""DATA_EMISSIONE""," _
                                 & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                                 & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                                 & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                                 & " SISCOM_MI.PAGAMENTI.DESCRIZIONE,'' as  ""IMPORTO_PRENOTATO""," _
                                 & " TRIM(TO_CHAR(SISCOM_MI.PAGAMENTI.IMPORTO_CONSUNTIVATO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUNTIVATO""," _
                                 & " TAB_STATI_PAGAMENTI.DESCRIZIONE AS ""STATO"",SISCOM_MI.PAGAMENTI.ID_FORNITORE,SISCOM_MI.PAGAMENTI.ID_APPALTO " _
                                 & ",(select num_Repertorio from siscom_mi.appalti where appalti.id=pagamenti.id_appalto) as repertorio " _
                         & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.TAB_STATI_PAGAMENTI" _
                         & " where SISCOM_MI.PAGAMENTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                         & "  and  SISCOM_MI.PAGAMENTI.ID_STATO=SISCOM_MI.TAB_STATI_PAGAMENTI.ID (+) "

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.ID in (select distinct(ID_PAGAMENTO) " _
                                                                         & " from SISCOM_MI.PRENOTAZIONI  " _
                                                                         & " where ID_VOCE_PF in ( select distinct(ID) from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") ) "
            End If

            'If par.IfEmpty(sValoreStato, -1) <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.STATO_FIRMA=" & sValoreStato
            'End If

            If sValoreFornitore <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_FORNITORE=" & sValoreFornitore
            End If


            If sValoreAppalto <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID_APPALTO=" & sValoreAppalto
            End If

            'If sValoreServizio <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PAGAMENTI.ID in (select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI where ID_VOCE_PF_IMPORTO=" & sValoreServizio & ")"
            'End If


            sStringaSql = sStringaSql & sSelectWhere & sOrder


            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "PAGAMENTIEMESSOSAL", "PAGAMENTIEMESSOSAL", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub PAGAMENTI_CANONE_RisultatiPagamentiStampa_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
