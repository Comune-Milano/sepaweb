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

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreFornitore As String
    Public sValoreStruttura As String

    Public sValoreDataP_Dal As String
    Public sValoreDataP_Al As String

    Public sValoreStato As String

    Public sValoreODL As String
    Public sValoreAnno As String

    Public sOrdinamento As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            'BindGrid()
            Session.Add("NOME_FILE", "")

        End If

    End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    If e.Item.ItemType = ListItemType.Item Then
    '        ---------------------------------------------------         
    '         Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        ---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " del beneficiario: " & Left(Replace(e.Item.Cells(3).Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtStatoPagamento').value='" & e.Item.Cells(9).Text & "';document.getElementById('txtIdVoce').value='" & e.Item.Cells(10).Text & "'")

    '    End If

    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        ---------------------------------------------------         
    '         Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        ---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(e.Item.Cells(1).Text, "'", "\'") & " del beneficiario: " & Left(Replace(e.Item.Cells(3).Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtStatoPagamento').value='" & e.Item.Cells(9).Text & "';document.getElementById('txtIdVoce').value='" & e.Item.Cells(10).Text & "'")

    '    End If

    'End Sub



    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "", "")
        Else
            Session.Add("ID", txtid.Text)

            'SELETTIVA
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStato = Request.QueryString("ST")
            sValoreStruttura = Request.QueryString("STR")

            sValoreFornitore = Request.QueryString("FO")

            sValoreDataP_Dal = Request.QueryString("DALP")
            sValoreDataP_Al = Request.QueryString("ALP")

            'sValoreDataE_Dal = Request.QueryString("DALE")
            'sValoreDataE_Al = Request.QueryString("ALE")


            'DIRETTA
            sValoreODL = Strings.Trim(Request.QueryString("ODL"))
            sValoreAnno = Strings.Trim(Request.QueryString("ANNO"))

            sOrdinamento = Request.QueryString("ORD")

            '& "&DALE=" & sValoreDataE_Dal & "&ALE=" & sValoreDataE_Al


            Session.Remove("NOME_FILE")

            Response.Write("<script>location.replace('Pagamenti.aspx?V=" & Me.txtIdVoce.Text _
                                                                & "&ST=" & sValoreStato _
                                                                & "&FO=" & sValoreFornitore _
                                                                & "&STR=" & sValoreStruttura _
                                                                & "&DALP=" & sValoreDataP_Dal _
                                                                & "&ALP=" & sValoreDataP_Al _
                                                                & "&STATO=" & Me.txtStatoPagamento.Text _
                                                                & "&ODL=" & sValoreODL _
                                                                & "&ANNO=" & sValoreAnno _
                                                                & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                & "&ORD=" & sOrdinamento & "');</script>")

        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("NOME_FILE")

        sValoreStato = Request.QueryString("ST")

        If sValoreStato = "RICERCA_DIRETTA" Then
            Response.Write("<script>document.location.href=""RicercaPagamentiD.aspx""</script>")
        Else
            Response.Write("<script>document.location.href=""RicercaPagamenti.aspx""</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("NOME_FILE")

        Page.Dispose()

        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            ' e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "'")
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato il pagamento PROGR/ANNO: " & Replace(dataItem("PROG_ANNO").Text, "'", "\'") & " del beneficiario: " & Left(Replace(dataItem("BENEFICIARIO").Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "';document.getElementById('txtStatoPagamento').value='" & dataItem("STATO").Text & "';document.getElementById('txtIdVoce').value='" & dataItem("ID_VOCE_PF").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Dim FlagConnessione As Boolean
        Dim sOrder As String
        Dim sCompara As String
        Dim sStringaSql As String
        Try
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'SELETTIVA
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreFornitore = Request.QueryString("FO")
            sValoreStruttura = Request.QueryString("STR")

            sValoreDataP_Dal = Request.QueryString("DALP")
            sValoreDataP_Al = Request.QueryString("ALP")

            sValoreStato = Request.QueryString("ST")


            If sValoreStato = "RICERCA_DIRETTA" Then
                If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sValoreStruttura = Session.Item("ID_STRUTTURA")
                End If
            End If


            'DIRETTA
            sValoreODL = Strings.Trim(Request.QueryString("ODL"))
            sValoreAnno = Strings.Trim(Request.QueryString("ANNO"))


            sOrdinamento = Request.QueryString("ORD")

            sOrder = " order by DATA_ORDINE desc"

            Select Case sOrdinamento
                Case "ODL"
                    sOrder = " order by SISCOM_MI.ODL.ANNO desc ,SISCOM_MI.ODL.PROGR desc " 'PROG_ANNO desc"
                Case "BENEFICIARIO"
                    sOrder = " order by BENEFICIARIO asc,SISCOM_MI.ODL.ANNO desc ,SISCOM_MI.ODL.PROGR desc " 'PROG_ANNO desc"
                Case "STATO"
                    sOrder = " order by STATO asc,SISCOM_MI.ODL.ANNO desc ,SISCOM_MI.ODL.PROGR desc " 'PROG_ANNO desc"

                Case Else
                    sOrder = ""
            End Select


            sStringaSql = " select SISCOM_MI.ODL.ID,(SISCOM_MI.ODL.PROGR||'/'||SISCOM_MI.ODL.ANNO) as ""PROG_ANNO""," _
                     & " to_date(substr(SISCOM_MI.ODL.DATA_ORDINE,1,8),'YYYYmmdd') as ""DATA_ORDINE""," _
                     & " case when SISCOM_MI.FORNITORI.RAGIONE_SOCIALE is not null " _
                     & "     then  FORNITORI.COD_FORNITORE || ' - ' || FORNITORI.RAGIONE_SOCIALE " _
                     & "     else  FORNITORI.COD_FORNITORE || ' - ' || RTRIM(LTRIM(FORNITORI.COGNOME ||' ' ||FORNITORI.NOME)) end  AS ""BENEFICIARIO""," _
                     & " SISCOM_MI.ODL.DESCRIZIONE," _
                     & " TRIM(TO_CHAR(SISCOM_MI.ODL.PREN_LORDO,'9G999G999G999G999G990D99')) AS ""PREN_LORDO"", " _
                     & " TRIM(TO_CHAR(SISCOM_MI.ODL.CONS_LORDO,'9G999G999G999G999G990D99')) AS ""CONS_LORDO""," _
                     & " SISCOM_MI.PF_VOCI.CODICE AS ""CODICE_BP"",SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE_BP""," _
                     & " SISCOM_MI.TAB_STATI_ODL.DESCRIZIONE AS ""STATO""," _
                     & " SISCOM_MI.ODL.ID_VOCE_PF " _
             & " from SISCOM_MI.ODL,SISCOM_MI.FORNITORI,SISCOM_MI.TAB_STATI_ODL,SISCOM_MI.PF_VOCI " _
             & " where  SISCOM_MI.ODL.ID_FORNITORE  =SISCOM_MI.FORNITORI.ID (+) " _
             & "   and  SISCOM_MI.ODL.ID_VOCE_PF    =SISCOM_MI.PF_VOCI.ID  (+)" _
             & "   and  SISCOM_MI.ODL.ID_STATO      =SISCOM_MI.TAB_STATI_ODL.ID "


            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.ODL.ID_VOCE_PF in ( select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If


            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            If IsNothing(sValoreStruttura) = False And sValoreStruttura <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.ODL.ID_STRUTTURA=" & sValoreStruttura
            End If


            If par.IfEmpty(sValoreStato, -1) <> "RICERCA_DIRETTA" Then
                If par.IfEmpty(sValoreStato, -1) <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.ODL.ID_STATO=" & sValoreStato
                End If

                If par.IfEmpty(sValoreFornitore, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and  SISCOM_MI.ODL.ID_FORNITORE=" & sValoreFornitore
                End If

                If par.IfEmpty(sValoreDataP_Dal, "") <> "" Then
                    sCompara = " >= "
                    sStringaSql = sStringaSql & " and SISCOM_MI.ODL.DATA_ORDINE" & sCompara & " '" & sValoreDataP_Dal & "' "
                End If

                If par.IfEmpty(sValoreDataP_Al, "") <> "" Then
                    sCompara = " <= "
                    sStringaSql = sStringaSql & " and SISCOM_MI.ODL.DATA_ORDINE" & sCompara & " '" & sValoreDataP_Al & "' "
                End If

                'If sValoreDataE_Dal <> "" Then
                '    sCompara = " >= "
                '    sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.DATA_EMISSIONE" & sCompara & " '" & sValoreDataE_Dal & "' "
                'End If

                'If sValoreDataE_Al <> "" Then
                '    sCompara = " <= "
                '    sStringaSql = sStringaSql & " and SISCOM_MI.PAGAMENTI.DATA_EMISSIONE" & sCompara & " '" & sValoreDataE_Al & "' "
                'End If

            Else

                If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.ODL.PROGR=" & sValoreODL
                End If

                If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                    sStringaSql = sStringaSql & " and SISCOM_MI.ODL.ANNO=" & sValoreAnno
                End If

            End If

            sStringaSql = sStringaSql & " order by SISCOM_MI.ODL.ANNO desc ,SISCOM_MI.ODL.PROGR desc"


            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
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
