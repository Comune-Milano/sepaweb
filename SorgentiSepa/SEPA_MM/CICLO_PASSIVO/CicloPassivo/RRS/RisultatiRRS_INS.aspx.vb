Imports Telerik.Web.UI
Imports System.Data

'*** LISTA RISULTATO RRS Proviene da : RicercaRRS_INS.aspx

Partial Class RRS_RisultatiRRS_INS
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String
    Dim sOrder As String

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreIndirizzoR As String
    Public sValoreVoceR As String
    Public sValoreAppaltoR As String

    Public sValoreUbicazione As String

    Public sValoreProvenienza As String
    Public sOrdinamento As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            HFGriglia.Value = DataGrid1.ClientID
            '*LBLID.Text = Request.QueryString("T")



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

            ' Label1.Text = " " & ds.Tables(0).Rows.Count
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
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(e.Item.Cells(par.IndDGC(DataGrid1, "NUM_REPERTORIO")).Text, "'", "\'") & " dell\'edificio: " & Left(Replace(e.Item.Cells(par.IndDGC(DataGrid1, "DESC_EDIFICIO")).Text, "'", "\'"), 30) & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(e.Item.Cells(2).Text, "") & "';document.getElementById('txtIdComplesso').value='" & par.IfEmpty(e.Item.Cells(3).Text, "") & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdServizio').value='" & e.Item.Cells(1).Text & "'")

    '    End If
    '    If e.Item.ItemType = ListItemType.AlternatingItem Then
    '        '---------------------------------------------------         
    '        ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
    '        '---------------------------------------------------         
    '        e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
    '        e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
    '        e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(e.Item.Cells(par.IndDGC(DataGrid1, "NUM_REPERTORIO")).Text, "'", "\'") & " dell\'edificio: " & Left(Replace(e.Item.Cells(par.IndDGC(DataGrid1, "DESC_EDIFICIO")).Text, "'", "\'"), 30) & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(e.Item.Cells(2).Text, "") & "';document.getElementById('txtIdComplesso').value='" & par.IfEmpty(e.Item.Cells(3).Text, "") & "';document.getElementById('txtIdAppalto').value='" & e.Item.Cells(0).Text & "';document.getElementById('txtIdServizio').value='" & e.Item.Cells(1).Text & "'")

    '    End If

    'End Sub



    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If par.IfEmpty(Me.txtIdServizio.Value, 0) <= 0 Then
            RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Else
            Session.Add("ID", 0)

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
            sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))

            sOrdinamento = Request.QueryString("ORD")
            sValoreProvenienza = Strings.Trim(Request.QueryString("PROVENIENZA"))


            Response.Write("<script>location.replace('ManutenzioniRRS.aspx?ED=" & par.IfEmpty(Me.txtIdEdificio.Value, -1) _
                                                                        & "&CO=" & par.IfEmpty(Me.txtIdComplesso.Value, -1) _
                                                                        & "&AP=" & par.IfEmpty(Me.txtIdAppalto.Value, -1) _
                                                                        & "&SV=" & par.IfEmpty(Me.txtIdServizio.Value, -1) _
                                                                        & "&IN_R=" & par.VaroleDaPassare(sValoreIndirizzoR) _
                                                                        & "&SV_R=" & sValoreVoceR _
                                                                        & "&AP_R=" & sValoreAppaltoR _
                                                                        & "&ORD=" & sOrdinamento _
                                                                        & "&UBI=" & sValoreUbicazione _
                                                                        & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                        & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")


        End If
    End Sub

    'Protected Sub btnStampa_Click(sender As Object, e As System.EventArgs) Handles btnStampa.Click
    '    Dim scriptblock As String
    '    Try
    '        sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
    '        sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
    '        sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

    '        sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))

    '        sOrdinamento = Request.QueryString("ORD")
    '        sValoreProvenienza = Strings.Trim(Request.QueryString("PROVENIENZA"))
    '        btnStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportRisultatoRRS_INS.aspx?IN_R=" & par.VaroleDaPassare(sValoreIndirizzoR) _
    '                                                                                                 & "&SV_R=" & sValoreVoceR _
    '                                                                                                 & "&AP_R=" & sValoreAppaltoR _
    '                                                                                                 & "&ORD=" & sOrdinamento _
    '                                                                                                 & "&UBI=" & sValoreUbicazione _
    '                                                                                                 & "');")
    '        scriptblock = "<script language='javascript' type='text/javascript'>" _
    '        & "window.open('Report/ReportRisultatoRRS_INS.aspx?IN_R=" & par.VaroleDaPassare(sValoreIndirizzoR) _
    '                                                     & "&SV_R=" & sValoreVoceR _
    '                                                     & "&AP_R=" & sValoreAppaltoR _
    '                                                     & "&ORD=" & sOrdinamento _
    '                                                     & "&UBI=" & sValoreUbicazione _
    '                                                     & "','Report');" _
    '                                                     & "</script>"

    '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
    '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
    '        End If

    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Session.Remove("IMP1")

        Response.Write("<script>document.location.href='RicercaRRS_INS.aspx?';</script>")
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Protected Sub Page_PreRenderComplete(sender As Object, e As System.EventArgs) Handles Me.PreRenderComplete
        If isExporting.Value = "1" Then
            Dim context As RadProgressContext = RadProgressContext.Current
            context.CurrentOperationText = "Export in corso..."
            context("ProgressDone") = True
            context.OperationComplete = True
            context.SecondaryTotal = 0
            context.SecondaryValue = 0
            context.SecondaryPercent = 0
            isExporting.Value = "0"
        End If
    End Sub



    Protected Sub DataGrid1_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)

            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato Num. Reper.: " & Replace(dataItem("NUM_REPERTORIO").Text, "'", "\'") & " dell\'edificio: " & Left(Replace(dataItem("DESC_EDIFICIO").Text, "'", "\'"), 30) & "';document.getElementById('txtIdEdificio').value='" & par.IfEmpty(dataItem("ID_EDIFICIO").Text, "") & "';document.getElementById('txtIdComplesso').value='" & par.IfEmpty(dataItem("id_complesso").Text, "") & "';document.getElementById('txtIdAppalto').value='" & dataItem("ID_APPALTO").Text & "';document.getElementById('txtIdServizio').value='" & dataItem("ID_PF_VOCE").Text & "'")
            e.Item.Attributes.Add("onDblclick", "document.getElementById('btnVisualizza').click();")
        End If
    End Sub

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            Dim sStringaSqlPatrimonio As String = ""
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
            sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))

            sOrdinamento = Request.QueryString("ORD")



            Select Case sOrdinamento
                Case "NUM_REPERTORIO"
                    sOrder = " order by NUM_REPERTORIO asc"
                Case "INDIRIZZO"
                    sOrder = " order by INDIRIZZO asc"
                Case "VOCE"
                    sOrder = " order by COD_VOCE,VOCE asc"
                Case Else
                    sOrder = ""
            End Select


            DataGrid1.Visible = True

            If sValoreUbicazione = 0 Then
                'COMPLESSO

                sStringaSql = "select APPALTI_VOCI_PF.ID_APPALTO,APPALTI_VOCI_PF.ID_PF_VOCE,EDIFICI.ID as ""ID_EDIFICIO"",COMPLESSI_IMMOBILIARI.ID as ""ID_COMPLESSO""," _
                                  & " APPALTI.NUM_REPERTORIO," _
                                  & " PF_VOCI.CODICE AS ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""VOCE""," _
                                  & " SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO"", " _
                                  & " EDIFICI.DENOMINAZIONE as ""DESC_EDIFICIO"", " _
                                  & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS ""INDIRIZZO"", appalti.descrizione as descrizione_appalti " _
                         & " from  SISCOM_MI.APPALTI_VOCI_PF, " _
                               & " SISCOM_MI.APPALTI, " _
                               & " SISCOM_MI.PF_VOCI, " _
                               & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                               & " SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI " _
                         & " where SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                & " where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')" _
                         & "   and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID=SISCOM_MI.EDIFICI.ID_COMPLESSO (+) " _
                         & "   and SISCOM_MI.COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO=SISCOM_MI.INDIRIZZI.ID (+) " _
                         & "   and APPALTI_VOCI_PF.ID_PF_VOCE=" & sValoreVoceR _
                         & "   and APPALTI_VOCI_PF.ID_APPALTO=" & sValoreAppaltoR _
                         & "   and APPALTI_VOCI_PF.ID_PF_VOCE=PF_VOCI.ID (+) " _
                         & "   and APPALTI_VOCI_PF.ID_APPALTO=APPALTI.ID (+) "

            Else

                sStringaSql = "select APPALTI_VOCI_PF.ID_APPALTO,APPALTI_VOCI_PF.ID_PF_VOCE,EDIFICI.ID as ""ID_EDIFICIO"",COMPLESSI_IMMOBILIARI.ID as ""ID_COMPLESSO""," _
                                  & " APPALTI.NUM_REPERTORIO," _
                                  & " PF_VOCI.CODICE AS ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""VOCE""," _
                                  & " SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""COMPLESSO"", " _
                                  & " EDIFICI.DENOMINAZIONE as DESC_EDIFICIO, " _
                                  & " (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP)  AS INDIRIZZO, appalti.descrizione as descrizione_appalti " _
                         & " from  SISCOM_MI.APPALTI_VOCI_PF, " _
                               & " SISCOM_MI.APPALTI, " _
                               & " SISCOM_MI.PF_VOCI, " _
                               & " SISCOM_MI.COMPLESSI_IMMOBILIARI," _
                               & " SISCOM_MI.EDIFICI,SISCOM_MI.INDIRIZZI " _
                         & " where SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                & " where DESCRIZIONE like '%" & par.PulisciStrSql(sValoreIndirizzoR) & "%')" _
                         & "   and SISCOM_MI.EDIFICI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                         & "   and SISCOM_MI.EDIFICI.ID_INDIRIZZO_PRINCIPALE=SISCOM_MI.INDIRIZZI.ID (+) " _
                         & "   and APPALTI_VOCI_PF.ID_PF_VOCE=" & sValoreVoceR _
                         & "   and APPALTI_VOCI_PF.ID_APPALTO=" & sValoreAppaltoR _
                         & "   and APPALTI_VOCI_PF.ID_PF_VOCE=PF_VOCI.ID (+) " _
                         & "   and APPALTI_VOCI_PF.ID_APPALTO=APPALTI.ID (+) "

            End If

            sStringaSql = sStringaSql & " order by NUM_REPERTORIO asc"
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "MANUTENZIONENONPATRIMONIALE", "MANUTENZIONENONPATRIMONIALE", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class
