Imports System.Data.OleDb
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports Telerik.Web.UI
Imports System.Data

Partial Class MANUTENZIONI_RisultatiAppalti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim sStringaSql2 As String
    Public numero As String
    'Public tipo As String
    Public fornitore As String
    Public filiale As String
    Public esercizio As String
    Public cig As String
    Public descrizione As String
    Public DL As String
    Public lotto As String
    Public datadal As String
    Public dataal As String
    Dim dt As New Data.DataTable
    Private isFilter As Boolean = False


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            HFGriglia.Value = DataGrid3.ClientID
        End If
    End Sub


    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Protected Sub DataGrid3_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles DataGrid3.ItemCommand
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

    Protected Sub DataGrid3_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid3.NeedDataSource
        Try
            'par.cmd.CommandText = "SELECT ID_TIPO_ST FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & Session.Item("ID_STRUTTURA")
            'Dim lettTipoSt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If lettTipoSt.Read Then
            '    Select Case par.IfNull(lettTipoSt("id_tipo_st"), -1)
            '        Case 0
            '        Case 1
            '        Case 2
            '    End Select
            'End If
            'lettTipoSt.Close()

            numero = UCase(Request.QueryString("NU"))
            lotto = UCase(Request.QueryString("LO"))
            fornitore = UCase(Request.QueryString("FO"))
            'tipo = UCase(Request.QueryString("TI"))
            datadal = UCase(Request.QueryString("DAL"))
            dataal = UCase(Request.QueryString("AL"))
            filiale = Request.QueryString("ST")
            esercizio = Request.QueryString("EF")
            cig = Request.QueryString("CIG")
            descrizione = Request.QueryString("DESC")
            DL = Request.QueryString("DL")
            Dim bTrovato As Boolean
            Dim sValore As String
            Dim sCompara As String


            bTrovato = False
            sStringaSql = ""


            If numero <> "" Then
                sValore = numero
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.APPALTI.NUM_REPERTORIO" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If fornitore <> "" And fornitore <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = fornitore
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.APPALTI.ID_FORNITORE" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If

            If lotto <> "" And lotto <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = lotto
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.APPALTI.ID_LOTTO" & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
            End If
            If Not String.IsNullOrEmpty(descrizione) Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = descrizione
                Call par.ConvertiJolly(sValore)
                bTrovato = True
                sStringaSql = sStringaSql & "UPPER(SISCOM_MI.APPALTI.DESCRIZIONE) LIKE '%" & par.PulisciStrSql(sValore.ToUpper.Replace("*", "")) & "%' "
            End If
            If DL <> "" And DL <> "-1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sValore = DL
                If InStr(sValore, "*") Then
                    sCompara = " LIKE "
                    Call par.ConvertiJolly(sValore)
                Else
                    sCompara = " = "
                End If
                bTrovato = True
                sStringaSql = sStringaSql & "ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO = '30000000' AND ID_OPERATORE = " & sValore & ")"
            End If


            If datadal <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sCompara = " >= "
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.APPALTI.DATA_REPERTORIO" & sCompara & " '" & datadal & "' "
            End If

            If dataal <> "" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sCompara = " <= "
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.APPALTI.DATA_REPERTORIO" & sCompara & " '" & dataal & "' "
            End If
            If Session.Item("BP_GENERALE") <> 1 Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                sCompara = " = "
                bTrovato = True
                sStringaSql = sStringaSql & "SISCOM_MI.APPALTI.ID_STRUTTURA " & sCompara & " " & Session.Item("id_struttura") & " "

            End If

            Dim condizioneCIG As String = ""
            If Not IsNothing(Request.QueryString("CIG")) AndAlso Request.QueryString("CIG") <> "" Then
                If Request.QueryString("CIG").ToString.Contains("*") Then
                    condizioneCIG = " UPPER(CIG) LIKE '" & par.PulisciStrSql(cig).Replace("*", "%") & "' "
                Else
                    condizioneCIG = " UPPER(CIG)='" & par.PulisciStrSql(cig) & "' "
                End If
            End If

            '**********Peppe MODIFY 28/02/2011************
            If Request.QueryString("TIPO") = "P" Then

                sStringaSQL1 = "select  distinct(SISCOM_MI.APPALTI.ID_GRUPPO) AS ID, (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NULL THEN FORNITORI.COGNOME||' '||FORNITORI.NOME ELSE FORNITORI.RAGIONE_SOCIALE END)AS FORNITORE , SISCOM_MI.APPALTI.NUM_REPERTORIO, " _
                    & "LTRIM(RTRIM((CASE WHEN ANNO IS NOT NULL THEN TO_CHAR(ANNO) ELSE SUBSTR(NUM_REPERTORIO,1,4) END)||LTRIM(RTRIM(TO_CHAR(PROGR,'0000000000'))))) AS REP_ORD, " _
                             & " TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYMMDD') AS ""DATA_REPERTORIO"",ANNO, PROGR, " _
                             & " SISCOM_MI.APPALTI.DESCRIZIONE, TO_DATE(SISCOM_MI.APPALTI.DATA_INIZIO,'YYYYMMDD') AS ""DATA_INIZIO"", TO_DATE(SISCOM_MI.APPALTI.DATA_FINE,'YYYYMMDD') AS ""DATA_FINE"", SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", " _
                             & "SISCOM_MI.APPALTI.PENALI, '' AS ""RIFINIZIO"", " _
                             & " '' AS ""RIFINE"", '' AS ""COSTO"", " _
                             & " SISCOM_MI.LOTTI.DESCRIZIONE AS ""DESCRIZIONE_LOTTO"",SISCOM_MI.APPALTI.DATA_REPERTORIO as ""DATA_REPERTORIO_NO_FORMAT"", " _
                             & " round((SELECT SUM((importo_consumo-round(sconto_consumo*importo_consumo/100,2)+oneri_sicurezza_consumo)+round((importo_consumo-round(sconto_consumo*importo_consumo/100,2)+oneri_sicurezza_consumo)*iva_consumo/100,2)) FROM SISCOM_MI.APPALTI_LOTTI_sERVIZI B WHERE B.ID_APPALTO=APPALTI.ID),2) AS TOT_CONSUMO," _
                             & " round((SELECT SUM((importo_canone-round(sconto_canone*importo_canone/100,2)+oneri_sicurezza_canone)+round((importo_canone-round(sconto_canone*importo_canone/100,2)+oneri_sicurezza_canone)*iva_canone/100,2)) FROM SISCOM_MI.APPALTI_LOTTI_sERVIZI B WHERE B.ID_APPALTO=APPALTI.ID),2) AS TOT_CANONE, " _
                             & " (CASE WHEN ID_STATO= 0 THEN 'BOZZA' WHEN ID_STATO = 1 THEN 'ATTIVO' WHEN ID_STATO = 5 THEN 'CHIUSO' ELSE '' END) AS STATO " _
                             & " ,(SELECT ID_LOTTO FROM SISCOM_MI.APPALTI C WHERE C.ID=APPALTI.ID_GRUPPO) AS ID_LOTTO " _
                             & " from SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI, SISCOM_MI.LOTTI, SISCOM_MI.FORNITORI "


                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                'peppe modify 22/12/2011 lego gli appalti ap pf_scelto
                Dim condLotto As String = ""
                If esercizio <> "-1" AndAlso IsNumeric(esercizio) AndAlso esercizio > 0 Then
                    condLotto = "AND LOTTI.ID_ESERCIZIO_FINANZIARIO = " & esercizio
                End If
                sStringaSql2 = "SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) AND " _
                                & "SISCOM_MI.APPALTI.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) AND APPALTI.TIPO = 'P' AND FORNITORI.ID(+) = APPALTI.ID_FORNITORE " & condLotto

                If filiale <> "" And filiale <> "-1" Then
                    sStringaSql2 = sStringaSql2 & " AND "
                    sValore = filiale
                    sCompara = " = "
                    sStringaSql2 = sStringaSql2 & "SISCOM_MI.lotti.id_filiale = " & sValore
                End If

                If sStringaSql <> "" Or sStringaSql2 <> "" Then
                    If condizioneCIG <> "" Then
                        sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql & sStringaSql2 & " AND " & condizioneCIG
                    Else
                        sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql & sStringaSql2
                    End If
                Else
                    If condizioneCIG <> "" Then
                        sStringaSQL1 = sStringaSQL1 & " where " & condizioneCIG
                    End If
                End If
                sStringaSQL1 = sStringaSQL1 & " AND APPALTI.ID = APPALTI.ID_GRUPPO"
                'sStringaSQL1 = sStringaSQL1 & " ORDER BY ANNO ASC, PROGR ASC"
            Else
                '********RICERCA APPALTI NON PATRIMONIALI
                sStringaSQL1 = "select  distinct(SISCOM_MI.APPALTI.ID), (CASE WHEN FORNITORI.RAGIONE_SOCIALE IS NULL THEN FORNITORI.COGNOME||' '||FORNITORI.NOME ELSE FORNITORI.RAGIONE_SOCIALE END)AS FORNITORE, SISCOM_MI.APPALTI.NUM_REPERTORIO, " _
                                & " TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYMMDD') AS ""DATA_REPERTORIO"", ANNO, PROGR, " _
                                & "LTRIM(RTRIM((CASE WHEN ANNO IS NOT NULL THEN TO_CHAR(ANNO) ELSE SUBSTR(NUM_REPERTORIO,1,4) END)||LTRIM(RTRIM(TO_CHAR(PROGR,'0000000000'))))) AS REP_ORD, " _
                                & " SISCOM_MI.APPALTI.DESCRIZIONE, TO_DATE(SISCOM_MI.APPALTI.DATA_INIZIO,'YYYYMMDD') AS ""DATA_INIZIO"", TO_DATE(SISCOM_MI.APPALTI.DATA_FINE,'YYYYMMDD') AS ""DATA_FINE"", SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", " _
                                & " TRIM(TO_CHAR((SELECT SUM(IMPORTO_CANONE) FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = APPALTI.ID),'9G999G999G999G999G990D99')) AS ""ASTA_CANONE"", " _
                                & " TRIM(TO_CHAR((SELECT SUM(IMPORTO_CONSUMO) FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = APPALTI.ID),'9G999G999G999G999G990D99')) AS ""ASTA_CONSUMO"", " _
                                & " SISCOM_MI.APPALTI.PENALI,'' AS ""RIFINIZIO"", " _
                                & " '' AS ""RIFINE"", '' AS ""COSTO"",'' AS DESCRIZIONE_LOTTO, " _
                                & " TRIM(TO_CHAR(NVL(round(APPALTI.TOT_CONSUMO,2),0),'9G999G999G999G999G990D99')) AS TOT_CONSUMO,TRIM(TO_CHAR(NVL(round(APPALTI.TOT_CANONE,2),0),'9G999G999G999G999G990D99')) AS TOT_CANONE, " _
                                & " (CASE WHEN ID_STATO= 0 THEN 'BOZZA' WHEN ID_STATO = 1 THEN 'ATTIVO' WHEN ID_STATO = 5 THEN 'CHIUSO' ELSE '' END) AS STATO , APPALTI.ID_LOTTO " _
                                & " from SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_VOCI_PF, SISCOM_MI.FORNITORI "

                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                Dim condLotto As String = ""
                If esercizio <> "-1" AndAlso IsNumeric(esercizio) AndAlso esercizio > 0 Then
                    condLotto = "AND pf_main.id_esercizio_finanziario =" & esercizio
                End If
                sStringaSql2 = "SISCOM_MI.APPALTI_VOCI_PF.ID_APPALTO(+)=SISCOM_MI.APPALTI.ID AND APPALTI.TIPO = 'N' AND FORNITORI.ID(+) = APPALTI.ID_FORNITORE " _
                    & "AND appalti_voci_pf.id_pf_voce IN (SELECT pf_voci.ID FROM siscom_mi.pf_voci, siscom_mi.pf_main WHERE id_piano_finanziario = pf_main.ID " & condLotto & " )"

                If sStringaSql <> "" Or sStringaSql2 <> "" Then
                    If condizioneCIG <> "" Then
                        sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql & sStringaSql2 & " AND " & condizioneCIG
                    Else
                        sStringaSQL1 = sStringaSQL1 & " where " & sStringaSql & sStringaSql2
                    End If
                Else
                    If condizioneCIG <> "" Then
                        sStringaSQL1 = sStringaSQL1 & " where " & condizioneCIG
                    End If
                End If
                sStringaSQL1 = sStringaSQL1 & " AND APPALTI.ID = APPALTI.ID_GRUPPO"
                'sStringaSQL1 = sStringaSQL1 & " ORDER BY ANNO ASC, PROGR ASC"



            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(sStringaSQL1)
            TryCast(sender, RadGrid).DataSource = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
            Session.Add("MIADT", dt)
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../Pagina_home_ncp.aspx""</script>")
    End Sub
    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        Try
            If txtid.Text = "" Then
                RadWindowManager1.RadAlert("Non hai selezionato alcuna riga!", 300, 150, "", "", "null")
                Exit Sub
            Else
                If Request.QueryString("TIPO") = "P" Then

                    numero = UCase(Request.QueryString("NU"))
                    lotto = UCase(Request.QueryString("LO"))
                    fornitore = UCase(Request.QueryString("FO"))
                    'tipo = UCase(Request.QueryString("TI"))
                    datadal = UCase(Request.QueryString("DAL"))
                    dataal = UCase(Request.QueryString("AL"))
                    If Not IsNothing(Request.QueryString("CIG")) Then
                        cig = UCase(Request.QueryString("CIG"))
                    Else
                        cig = ""
                    End If



                    If lotto = "" Then
                        lotto = idLotto.Value
                    End If

                    Session.Add("IDA", txtid.Text)
                    Response.Redirect("Appalti.aspx?CIG=" & cig & "&FO=" & par.PulisciStrSql(fornitore) & "&NU=" & par.PulisciStrSql(numero) & "&LO=" & par.PulisciStrSql(lotto) & "&DAL=" & par.IfEmpty(datadal, "") & "&AL=" & par.IfEmpty(dataal, "") & "&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF"))

                ElseIf Request.QueryString("TIPO") = "N" Then

                    numero = UCase(Request.QueryString("NU"))
                    lotto = UCase(Request.QueryString("LO"))
                    fornitore = UCase(Request.QueryString("FO"))
                    'tipo = UCase(Request.QueryString("TI"))
                    datadal = UCase(Request.QueryString("DAL"))
                    dataal = UCase(Request.QueryString("AL"))
                    If Not IsNothing(Request.QueryString("CIG")) Then
                        cig = UCase(Request.QueryString("CIG"))
                    Else
                        cig = ""
                    End If

                    Response.Redirect("AppaltiNP.aspx?CIG=" & cig & "&FO=" & par.PulisciStrSql(fornitore) & "&NU=" & par.PulisciStrSql(numero) & "&DAL=" & par.IfEmpty(datadal, "") & "&AL=" & par.IfEmpty(dataal, "") & "&IDA=" & txtid.Text & "&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF"))

                End If

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Protected Sub DataGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid3.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGrid3.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub
    Protected Sub DataGrid3_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            e.Item.Attributes.Add("onclick", "document.getElementById('txtmia').value='Hai selezionato l\'appalto numero: " & Replace(dataItem("NUM_REPERTORIO").Text, "'", "\'") & "';document.getElementById('txtid').value='" & dataItem("ID").Text & "';document.getElementById('idLotto').value='" & dataItem("ID_LOTTO").Text & "'")
            e.Item.Attributes.Add("onDblclick", "ApriAppaltoSelezionato();")
        End If
    End Sub

    Protected Sub btnRicerca_Click(sender As Object, e As System.EventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaAppalti.aspx""</script>")
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGrid3.AllowPaging = False
        DataGrid3.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGrid3.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In DataGrid3.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGrid3.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGrid3.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        DataGrid3.AllowPaging = True
        DataGrid3.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGrid3.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CONTRATTI", "CONTRATTI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub

    Private Sub MANUTENZIONI_RisultatiAppalti_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class

