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



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If txtid.Text = "" Then
            Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
        Else
            Session.Add("ID", txtid.Text)

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreRepertorio = Request.QueryString("REP")
            sValoreODL = Request.QueryString("ODL")
            sValoreAnno = Request.QueryString("ANNO")

            sOrdinamento = Request.QueryString("ORD")

            Response.Write("<script>location.replace('ManutenzioniRRS.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                    & "&ODL=" & sValoreODL _
                                                                    & "&ANNO=" & sValoreAnno _
                                                                    & "&ORD=" & sOrdinamento _
                                                                    & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                    & "&PROVENIENZA=RICERCA_CONSUNTIVI_RRS_D" & "');</script>")


        End If

    End Sub



    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound


    '    If e.Item.ItemType = ListItemType.Item Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(2).Text, "'", "\'") & " ubicato in: " & Left(Replace(e.Item.Cells(4).Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '        ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(e.Item.Cells(2).Text, "'", "\'") & " ubicato in: " & Left(Replace(e.Item.Cells(4).Text, "'", "\'"), 30) & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")

    '        ''            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato l'impianto del Cod. Complesso: " & e.Item.Cells(1).Text & "';document.getElementById('txtid').value='" & e.Item.Cells(0).Text & "'")
    '        ';document.getElementById('txtImpianto').value='" & e.Item.Cells(2).Text & "'"
    '    End If

    'End Sub
    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato l\'ODL/ANNO: " & Replace(dataItem("ODL_ANNO").Text, "'", "\'") & " ubicato in: " & Replace(dataItem("UBICAZIONE").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID_MANUTENZIONE").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub



    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""RicercaConsuntiviRRS_D.aspx""</script>")
    End Sub


    'Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
    '    Dim scriptblock As String

    '    Try
    '        sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

    '        sValoreRepertorio = Request.QueryString("REP")
    '        sValoreODL = Request.QueryString("ODL")
    '        sValoreAnno = Request.QueryString("ANNO")


    '        sOrdinamento = Request.QueryString("ORD")


    '        btnStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportRisultatoConsuntiviD.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
    '                                                                                                        & "&ODL=" & sValoreODL _
    '                                                                                                        & "&ANNO=" & sValoreAnno _
    '                                                                                                        & "&EF_R=" & sValoreEsercizioFinanziarioR _
    '                                                                                                        & "&ORD=" & sOrdinamento & "');")

    '        scriptblock = "<script language='javascript' type='text/javascript'>" _
    '        & "window.open('Report/ReportRisultatoConsuntiviD.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
    '                                                           & "&ODL=" & sValoreODL _
    '                                                           & "&ANNO=" & sValoreAnno _
    '                                                           & "&EF_R=" & sValoreEsercizioFinanziarioR _
    '                                                           & "&ORD=" & sOrdinamento & "','Report');</script>"

    '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
    '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
    '        End If

    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try
    'End Sub



    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try

            Dim sFiliale As String = ""
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
                Case "VOCE"
                    sOrder = " order by VOCE,UBICAZIONE"
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
                                & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd') as DATA_INIZIO_ORDINE," _
                                & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                & "SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                                & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                & "  then RAGIONE_SOCIALE else COGNOME || ' ' || NOME end as ""FORNITORE""," _
                                & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL""" _
                        & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI" _
                        & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO is null " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO IS NULL  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)  " _
                        & "   and   SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                        & "   and   SISCOM_MI.MANUTENZIONI.STATO=1"

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If

            If sFiliale <> "" Then
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
                                    & " (SISCOM_MI.MANUTENZIONI.PROGR||'/'||SISCOM_MI.MANUTENZIONI.ANNO) as ""ODL_ANNO"",to_date(substr(SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE,1,8),'YYYYmmdd') as DATA_INIZIO_ORDINE," _
                                    & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""UBICAZIONE""," _
                                    & "SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE""," _
                                    & " case when FORNITORI.RAGIONE_SOCIALE is not null " _
                                    & "  then RAGIONE_SOCIALE else COGNOME || ' ' || NOME end as ""FORNITORE""," _
                                    & "SISCOM_MI.MANUTENZIONI.PROGR,SISCOM_MI.MANUTENZIONI.ANNO,SISCOM_MI.MANUTENZIONI.DATA_INIZIO_ORDINE as  ""DATA_ODL"" " _
                    & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.EDIFICI, SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                    & " where   SISCOM_MI.MANUTENZIONI.ID_COMPLESSO is null  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE_IMPORTO IS NULL  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_PF_VOCE=SISCOM_MI.PF_VOCI.ID (+)  " _
                    & "   and   SISCOM_MI.MANUTENZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+)   " _
                    & "   and   SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)  " _
                    & "   and  SISCOM_MI.MANUTENZIONI.STATO=1"

            If par.IfEmpty(sValoreEsercizioFinanziarioR, "-1") <> "-1" Then
                sStringaSql = sStringaSql & " and SISCOM_MI.MANUTENZIONI.ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR & ") "
            End If

            If sFiliale <> "" Then
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

            sStringaSql = sStringaSql & sOrder
            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSql)
            TryCast(sender, RadGrid).DataSource = dt
            Session.Item("RISULTATI_RRS") = dt
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CONSUNTIVINONPATRIMONIALE", "CONSUNTIVINONPATRIMONIALE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub

End Class
