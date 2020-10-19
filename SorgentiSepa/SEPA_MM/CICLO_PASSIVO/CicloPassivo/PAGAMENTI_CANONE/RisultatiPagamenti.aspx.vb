'*** LISTA RISULTATO PAGAMENTI

Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data


Partial Class PAGAMENTI_RisultatiPagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreFornitore As String
    Public sValoreAppalto As String

    Public sValoreDataP_Dal As String
    Public sValoreDataP_Al As String

    Public sValoreDataS_Dal As String
    Public sValoreDataS_Al As String

    Public sValoreStato As String
    Public sValoreTipo As String

    Public sOrdinamento As String


    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))

        If Not IsPostBack Then

            lstListaRapporti.Clear()
            HFGriglia.Value = DataGrid1.ClientID

            ' BindGrid()
            Session.Add("NOME_FILE", "")

        End If

    End Sub



    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim numeroCheckati As Integer = 0
        For Each elemento As DataGridItem In DataGrid1.Items
            If CType(elemento.FindControl("CheckBox1"), CheckBox).Checked = True Then
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

    Protected Sub CheckBox2_CheckedChanged(sender As Object, e As System.EventArgs)
        Dim numeroCheckati As Integer = 0
        If CType(sender, CheckBox).Checked = True Then
            For Each elemento As DataGridItem In DataGrid1.Items
                CType(elemento.FindControl("CheckBox1"), CheckBox).Checked = True
                numeroCheckati += 1
            Next
        Else
            For Each elemento As DataGridItem In DataGrid1.Items
                CType(elemento.FindControl("CheckBox1"), CheckBox).Checked = False
            Next
            numeroCheckati = 0
        End If
        Select Case numeroCheckati
            Case 0
                txtmia.Text = "Nessun ODL selezionato"
            Case 1
                txtmia.Text = "Selezionato 1 ODL"
            Case Else
                txtmia.Text = "Sono stati selezionati " & numeroCheckati & " ODL"
        End Select
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click

        Dim oDataGridItem As GridDataItem
        '   Dim chkExport As System.Web.UI.WebControls.CheckBox
        Dim chkExport As RadButton
        Dim Trovato As Boolean
        Dim i As Integer

        Dim gen As Epifani.ListaGenerale




        For Each oDataGridItem In Me.DataGrid1.Items

            chkExport = oDataGridItem.FindControl("CheckBox1")

            If chkExport.Checked Then

                ' CONTROLLO SE GIA INSERITO nella LISTA
                Trovato = False
                For Each gen In lstListaRapporti
                    If gen.STR = oDataGridItem.Cells(0).Text Then  ''SISCOM_MI.MANUTENZIONI.ID
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
                    If gen.STR = oDataGridItem.Cells(0).Text Then
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

            'SELETTIVA
            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")


            'DATE
            sValoreDataS_Dal = Request.QueryString("DALS")
            sValoreDataS_Al = Request.QueryString("ALS")

            sValoreDataP_Dal = Request.QueryString("DALP")
            sValoreDataP_Al = Request.QueryString("ALP")


            sValoreStato = Request.QueryString("ST")
            sValoreTipo = Request.QueryString("TIPO")


            sOrdinamento = Request.QueryString("ORD")


            Session.Remove("NOME_FILE")

            Response.Write("<script>location.replace('Pagamenti.aspx?ST=" & sValoreStato _
                                                                 & "&FO=" & sValoreFornitore _
                                                                 & "&AP=" & sValoreAppalto _
                                                                 & "&DALS=" & sValoreDataS_Dal _
                                                                 & "&ALS=" & sValoreDataS_Al _
                                                                 & "&DALP=" & sValoreDataP_Dal _
                                                                 & "&ALP=" & sValoreDataP_Al _
                                                                 & "&TIPO=" & sValoreTipo _
                                                                 & "&ID_A=" & sValoreAppalto _
                                                                 & "&ID_F=" & sValoreFornitore _
                                                                 & "&ORD=" & sOrdinamento & "');</script>")

        Else
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click

        lstListaRapporti.Clear()
        Session.Remove("NOME_FILE")


        sValoreTipo = Request.QueryString("TIPO")   'DA_APPROVARE, DA_APPROVARE_IN_SCADENZA

        If sValoreTipo = "DA_APPROVARE" Then

            Response.Write("<script>document.location.href=""RicercaPagamenti.aspx?TIPO=" & sValoreTipo & """</script>")
        ElseIf sValoreTipo = "DA_APPROVARE_IN_SCADENZA" Then
            Response.Write("<script>document.location.href=""RicercaPagamenti.aspx?TIPO=" & sValoreTipo & """</script>")
        Else
            Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
        End If

    End Sub

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        lstListaRapporti.Clear()
        Session.Remove("NOME_FILE")

        'Page.Dispose()

        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
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

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Dim FlagConnessione As Boolean
        Dim sStringaSql As String
        Dim sSelectWhere As String = ""

        Dim dt As New Data.DataTable

        Try


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " SISCOM_MI.PRENOTAZIONI.ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'SELETTIVA
            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")


            'DATE
            sValoreDataS_Dal = Request.QueryString("DALS")
            sValoreDataS_Al = Request.QueryString("ALS")

            sValoreDataP_Dal = Request.QueryString("DALP")
            sValoreDataP_Al = Request.QueryString("ALP")


            sValoreStato = Request.QueryString("ST")
            sValoreTipo = Request.QueryString("TIPO")   'DA_APPROVARE, DA_APPROVARE_IN_SCADENZA

            sOrdinamento = Request.QueryString("ORD")

            'And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) DA GESTIRE IN SEGUITO 19 MAGGIO 2011

            dt.Columns.Add("ID_APPALTO")
            dt.Columns.Add("DATA_SCADENZA")
            dt.Columns.Add("ID_FORNITORE")
            dt.Columns.Add("APPALTO")
            dt.Columns.Add("ANNO")
            dt.Columns.Add("DATA_PRENOTAZIONE")
            dt.Columns.Add("FORNITORE")
            dt.Columns.Add("PREN_LORDO")
            dt.Columns.Add("CONS_LORDO")
            dt.Columns.Add("DATA SCADENZA")
            dt.Columns.Add("DESCRIZIONE")
            dt.Columns.Add("STATO")


            'distinct(ID_APPALTO) come era prima, raggruppamento per APPALTO, DATA_SCADENZA
            If sValoreTipo = "DA_APPROVARE" Then
                sStringaSql = "select SISCOM_MI.PRENOTAZIONI.ID as ID_PRENOTAZIONE,SISCOM_MI.PRENOTAZIONI.ID_APPALTO,SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,SISCOM_MI.PRENOTAZIONI.ID_FORNITORE," _
                              & " (APPALTI.NUM_REPERTORIO ||' - '||APPALTI.DESCRIZIONE) as ""APPALTO"",prenotazioni.ANNO," _
                              & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE""," _
                              & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                              & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                              & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""FORNITORE""," _
                              & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO,'9G999G999G999G999G990D99')) AS ""PREN_LORDO"", " _
                              & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_APPROVATO,'9G999G999G999G999G990D99')) AS ""CONS_LORDO"", " _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA SCADENZA""," _
                         & " APPALTI.NUM_REPERTORIO as ""NUM_REPERTORIO""" _
                         & ",(SELECT 'ANNO B.P. '||SUBSTR(INIZIO,1,4) || ' -  '|| DESCRIZIONE FROM SISCOM_MI.PF_VOCI, SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN  WHERE PF_VOCI.ID = ID_VOCE_PF AND T_eSERCIZIO_FINANZIARIO.ID =PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND PF_VOCI.ID_PIANO_FINANZIARIO=PF_MAIN.ID) AS DESCRIZIONE " _
                                 & ",(SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE PF_VOCI_IMPORTO.ID=PRENOTAZIONI.ID_VOCE_PF_IMPORTO ) AS STATO " _
                 & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                 & " where  SISCOM_MI.PRENOTAZIONI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                 & "  and   SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)"

                'DataGrid1.Columns(par.IndDGC(DataGrid1, "DESCRIZIONE")).HeaderText = "VOCE BP"
                'DataGrid1.Columns(par.IndDGC(DataGrid1, "STATO")).HeaderText = "VOCE DGR"
            Else
                sStringaSql = "select SISCOM_MI.PRENOTAZIONI.ID as ID_PRENOTAZIONE,SISCOM_MI.PRENOTAZIONI.ID_APPALTO,SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,SISCOM_MI.PRENOTAZIONI.ID_FORNITORE," _
                              & " (APPALTI.NUM_REPERTORIO ||' - '||APPALTI.DESCRIZIONE) as ""APPALTO"",prenotazioni.ANNO," _
                              & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE""," _
                              & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                              & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                              & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""FORNITORE""," _
                              & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO,'9G999G999G999G999G990D99')) AS ""PREN_LORDO"", " _
                              & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_APPROVATO,'9G999G999G999G999G990D99')) AS ""CONS_LORDO"", " _
                         & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA SCADENZA""," _
                         & " SISCOM_MI.PRENOTAZIONI.DESCRIZIONE," _
                         & " decode(PRENOTAZIONI.ID_STATO,0,'DA APPROVARE',1,'APPROVATO',2,'EMESSO SAL') AS ""STATO"", " _
                         & " APPALTI.NUM_REPERTORIO as ""NUM_REPERTORIO""" _
                 & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                 & " where  SISCOM_MI.PRENOTAZIONI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+) " _
                 & "  and   SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)"
                'DataGrid1.Columns(par.IndDGC(DataGrid1, "DESCRIZIONE")).HeaderText = "DESCRIZIONE"
                'DataGrid1.Columns(par.IndDGC(DataGrid1, "STATO")).HeaderText = "STATO"
            End If


            'If Session.Item("LIVELLO") <> "1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF in (select ID_VOCE from SISCOM_MI.PF_VOCI_STRUTTURA where " & sFiliale & ")"
            'End If


            Select Case sValoreTipo

                Case "DA_APPROVARE"
                    sSelectWhere = "  and SISCOM_MI.PRENOTAZIONI.TIPO_PAGAMENTO=6" _
                                 & "  and SISCOM_MI.PRENOTAZIONI.ID_STATO=0 " _
                                 & "  and SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO>0 "

                    If sFiliale <> "" Then
                        sSelectWhere = sSelectWhere & " and " & sFiliale
                    End If

                Case "DA_APPROVARE_IN_SCADENZA"

                    sSelectWhere = " and  SISCOM_MI.PRENOTAZIONI.TIPO_PAGAMENTO=6" _
                               & "   and  SISCOM_MI.PRENOTAZIONI.ID_STATO=0 " _
                               & "   and  SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO>0 " _
                               & "   and  (NVL(TO_DATE(SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) "

                    If sFiliale <> "" Then
                        sSelectWhere = sSelectWhere & " and " & sFiliale
                    End If

            End Select


            'If par.IfEmpty(sValoreStato, -1) <> "-1" Then
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PRENOTAZIONI.ID_STATO=" & sValoreStato
            'Else
            '    sStringaSql = sStringaSql & " and  SISCOM_MI.PRENOTAZIONI.ID_STATO>=0"
            'End If

            If par.IfEmpty(sValoreAppalto, -1) <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PRENOTAZIONI.ID_APPALTO in (select id from siscom_mi.appalti where id_gruppo =" & sValoreAppalto & ")"
            End If

            If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and  SISCOM_MI.PRENOTAZIONI.ID_FORNITORE=" & sValoreFornitore
            End If

            sStringaSql = sStringaSql & sSelectWhere & " order by SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA desc"

            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSql, par.OracleConn)
            'Dim ds As New Data.DataSet()

            'da.Fill(ds) ', "DOMANDE_BANDO,COMP_NUCLEO")

            'DataGrid1.DataSource = ds
            'DataGrid1.DataBind()

            '' Label1.Text = " " & ds.Tables(0).Rows.Count

            'da.Dispose()
            'ds.Dispose()


            Dim dtFinale As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dtFinale
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "MANUTENZIONI", "MANUTENZIONI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub PAGAMENTI_RisultatiPagamenti_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class
