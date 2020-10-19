Imports Telerik.Web.UI
Imports System.Data

'*** LISTA RISULTATO MANUTENZIONI da CONSUNTIVARE (ricerca DIRETTA)

Partial Class MANUTENZIONI_RisultatiConsuntiviD
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreRepertorio As String
    Public sValoreODL As String
    Public sValoreAnno As String

    Public sOrdinamento As String
    Private isFilter As Boolean = False


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            '*LBLID.Text = Request.QueryString("T")
            'And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE")))



        End If


    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Else
            Session.Add("ID", txtid.Text)

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreRepertorio = Request.QueryString("REP")
            sValoreODL = Request.QueryString("ODL")
            sValoreAnno = Request.QueryString("ANNO")

            sOrdinamento = Request.QueryString("ORD")

            Response.Write("<script>location.replace('Manutenzioni.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                    & "&ODL=" & sValoreODL _
                                                                    & "&ANNO=" & sValoreAnno _
                                                                    & "&ORD=" & sOrdinamento _
                                                                    & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                    & "&PROVENIENZA=RICERCA_CONSUNTIVI_D" & "');</script>")


        End If
    End Sub

    

    Protected Sub btnNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles btnNuovaRicerca.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""RicercaConsuntiviD.aspx""</script>")
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
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

    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(dataItem("ODL_ANNO").Text, "'", "\'") & " ubicato in: " & Left(Replace(dataItem("UBICAZIONE").Text.Replace("&nbsp;", "-"), "'", "\'"), 30) & "';document.getElementById('txtid').value='" & dataItem("ID_MANUTENZIONE").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            Dim sFiliale As String = "-1"
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreRepertorio = Strings.Trim(Request.QueryString("REP"))
            sValoreODL = Strings.Trim(Request.QueryString("ODL"))

            sValoreAnno = Strings.Trim(Request.QueryString("ANNO"))

            sOrdinamento = Request.QueryString("ORD")


            Select Case sOrdinamento
                Case "UBICAZIONE"
                    sOrder = " order by UBICAZIONE asc"
                Case "SERVIZIO"
                    sOrder = " order by SERVIZIO,SERVIZIO_VOCI,UBICAZIONE"
                Case "DATA ODL"
                    sOrder = " order by DATA_ODL desc"
                Case "NUM REPERTORIO"
                    sOrder = " order by NUM_REPERTORIO"
                Case "FORNITORE"
                    sOrder = " order by FORNITORE,UBICAZIONE"

                Case Else
                    sOrder = ""
            End Select


            sStringaSql = " select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') as DATA_INIZIO_ORDINE," _
                                & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                & "SISCOM_MI.TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO""," _
                                & "SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE AS ""SERVIZIO_VOCI""," _
                                & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "  then COD_FORNITORE || ' - ' || RAGIONE_SOCIALE else COD_FORNITORE || ' - ' || COGNOME || ' ' || NOME end as ""FORNITORE""," _
                                & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL""" _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI" _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO is not null  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)  " _
                        & "   and   SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=1"

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If


            If sFiliale <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
            End If

            If par.IfEmpty(sValoreRepertorio, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.APPALTI.NUM_REPERTORIO LIKE '" & par.PulisciStrSql(sValoreRepertorio).Replace("*", "%") & "'"
            End If

            If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.PROGR=" & sValoreODL
            End If

            If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ANNO=" & sValoreAnno
            End If

            sStringaSql = sStringaSql & "union  select  SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.MANUTENZIONI.ID AS ""ID_MANUTENZIONE""," _
                                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",TO_DATE(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,'YYYYMMDD') as DATA_INIZIO_ORDINE," _
                                    & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                    & "SISCOM_MI.TAB_SERVIZI.DESCRIZIONE AS ""SERVIZIO""," _
                                    & "SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE AS ""SERVIZIO_VOCI""," _
                                    & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "  then  COD_FORNITORE || ' - ' || RAGIONE_SOCIALE else COD_FORNITORE || ' - ' || COGNOME || ' ' || NOME end as ""FORNITORE""," _
                                    & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL"" " _
                    & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.TAB_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                    & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO is not null  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID (+)  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                    & "   and   SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                    & "   and  SISCOM_MI.MANUTENZIONI.STATO=1"

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If


            If sFiliale <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI." & sFiliale
            End If

            If par.IfEmpty(sValoreRepertorio, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.APPALTI.NUM_REPERTORIO LIKE '" & par.PulisciStrSql(sValoreRepertorio).Replace("*", "%") & "'"
            End If

            If par.IfEmpty(sValoreODL, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.PROGR=" & sValoreODL
            End If

            If par.IfEmpty(sValoreAnno, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ANNO=" & sValoreAnno
            End If

            sStringaSql = sStringaSql & " order by NUM_REPERTORIO"

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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CONSUNTIVI", "CONSUNTIVI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
