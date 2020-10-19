Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing

Partial Class Contratti_CruscottoElabCauz
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public Altezza As Int64 = 0
    Dim xls As New ExcelSiSol


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Dim Str As String = ""

            If Not IsPostBack Then
                sIndiceReport = Request.QueryString("IDELAB")
                BindGrid()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Elaborazione interessi su deposito cauzionale - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub BindGrid()
        Dim id_from As String
        Dim id_to As String
        Dim param() As String
        Dim l_where As String

        param = Split(sIndiceReport, "@")
        id_from = param(0)
        id_to = param(1)

        'l_where = " WHERE (id_contratto >= 3319 and id_contratto <= 10304) and ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)
        l_where = " WHERE ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)

        sStrSql = "SELECT DISTINCT ADEGUAMENTO_INTERESSI.ID_CONTRATTO AS IDC, RAPPORTI_UTENZA.cod_contratto as cod_contratto1, " _
                    & " REPLACE('<a href=£javascript:void(0)£ onclick=£window.open(''Contratto.aspx?ID='||RAPPORTI_UTENZA.ID||'&COD='||RAPPORTI_UTENZA.COD_CONTRATTO||''','''||RAPPORTI_UTENZA.ID||''',''height=780,top=0,left=0,width=1160'');void(0);£>' || RAPPORTI_UTENZA.COD_CONTRATTO || '</a>','£','" & Chr(34) & "') as COD_CONTRATTO, " _
                    & "TO_DATE (DATA, 'yyyymmdd') as data_calcolo, " _
                    & "ADEGUAMENTO_INTERESSI.IMPORTO AS somma_interessi, " _
                    & "0 as SOMMA_MORA, " _
                    & "ID_ANAGRAFICA, " _
                    & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTESTATARIO " _
                    & "        FROM SISCOM_MI.ADEGUAMENTO_INTERESSI " _
                    & "INNER JOIN SISCOM_MI.ADEGUAMENTO_INTERESSI_VOCI ON (ADEGUAMENTO_INTERESSI.ID=ID_ADEGUAMENTO) " _
                    & "INNER JOIN SISCOM_MI.ANAGRAFICA ON (SISCOM_MI.ANAGRAFICA.ID = ADEGUAMENTO_INTERESSI.ID_ANAGRAFICA) " _
                    & "inner join SISCOM_MI.rapporti_utenza on (rapporti_utenza.id=ID_CONTRATTO) " _
                    & l_where _
                    & "ORDER BY INTESTATARIO, ID_CONTRATTO , data_calcolo desc "
    End Sub

    Public Property sIndiceReport() As String
        Get
            If Not (ViewState("par_sIndiceReport") Is Nothing) Then
                Return CStr(ViewState("par_sIndiceReport"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIndiceReport") = value
        End Set
    End Property

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Protected Sub dgvDocumenti_DetailTableDataBind(sender As Object, e As Telerik.Web.UI.GridDetailTableDataBindEventArgs) Handles dgvDocumenti.DetailTableDataBind
        Dim dataItem As GridDataItem = DirectCast(e.DetailTableView.ParentItem, GridDataItem)
        Select Case e.DetailTableView.Name
            Case "Dettagli"
                If True Then
                    Dim IndiceS As String = Replace(dataItem("IDC").Text, "&nbsp;", "-1")
                    Dim id_from As String
                    Dim id_to As String
                    Dim param() As String
                    Dim l_where As String

                    param = Split(sIndiceReport, "@") 'Split(Request.QueryString("IDELAB"), "@")
                    id_from = param(0)
                    id_to = param(1)

                    l_where = " WHERE ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)

                    Dim sstrSql1 As String = "SELECT TO_DATE (DAL, 'YYYYMMDD') AS DAL , TO_DATE (AL, 'YYYYMMDD') AS AL , GIORNI, TASSO, ADEGUAMENTO_INTERESSI_VOCI.IMPORTO AS IMPORTO, (CASE WHEN FL_APPLICATO=0 THEN 'NO' ELSE 'SI' END) AS PAGATI " _
                                            & "FROM SISCOM_MI.ADEGUAMENTO_INTERESSI " _
                                            & "INNER JOIN SISCOM_MI.ADEGUAMENTO_INTERESSI_VOCI ON (ADEGUAMENTO_INTERESSI.ID=ID_ADEGUAMENTO) " _
                                            & l_where _
                                            & "AND ID_CONTRATTO = " & IndiceS
                    '                                            & "ORDER BY  DAL ASC "
                    e.DetailTableView.DataSource = par.getDataTableGrid(sstrSql1)
                End If
            Case "Dettagli1"
                '////////////////////////////////
                ' Eventuale ulteriore dettaglio
        End Select
    End Sub

    Protected Sub dgvDocumenti_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles dgvDocumenti.ItemCommand
        If e.CommandName = Telerik.Web.UI.RadGrid.ExportToExcelCommandName Then
            'If e.CommandName = "My_ExportToExcel" Then
            Export()
        End If


        'Select Case e.CommandName
        '    Case "ExportToExcel"
        '        dgvDocumenti.MasterTableView.HierarchyDefaultExpanded = True
        '        dgvDocumenti.MasterTableView.DetailTables(0).HierarchyDefaultExpanded = True
        'End Select
    End Sub

    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvDocumenti.NeedDataSource
        Try
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Elaborazione Interessi Cauzionali - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try

    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Sub export()
        Try
            Dim myExcelFile As New CM.ExcelFile
            Dim DT As New Data.DataTable
            '            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            Dim id_from As String
            Dim id_to As String
            Dim param() As String
            Dim l_where As String

            param = Split(sIndiceReport, "@")
            id_from = param(0)
            id_to = param(1)

            l_where = " WHERE ADEGUAMENTO_INTERESSI.ID >= " & CStr(id_from) & " AND ADEGUAMENTO_INTERESSI.ID<=" & CStr(id_to)

            sStrSql = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO AS COD_CONTRATTO, " _
                        & "CASE WHEN anagrafica.ragione_sociale is not null THEN ragione_sociale  ELSE RTRIM(LTRIM(COGNOME ||' ' ||ANAGRAFICA.NOME)) END AS INTESTATARIO, " _
                        & "ADEGUAMENTO_INTERESSI.IMPORTO AS TOTALE_INTERESSI, " _
                        & "TO_DATE (DATA, 'YYYYMMDD') AS CALCOLATI_AL, " _
                        & "TO_DATE (DAL, 'YYYYMMDD') AS DAL , TO_DATE (AL, 'YYYYMMDD') AS AL , GIORNI, TASSO, ADEGUAMENTO_INTERESSI_VOCI.IMPORTO AS IMPORTO, (CASE WHEN FL_APPLICATO=0 THEN 'NO' ELSE 'SI' END) AS PAGATI " _
                        & "FROM SISCOM_MI.ADEGUAMENTO_INTERESSI " _
                        & "INNER JOIN SISCOM_MI.ADEGUAMENTO_INTERESSI_VOCI ON (ADEGUAMENTO_INTERESSI.ID=ID_ADEGUAMENTO) " _
                        & "INNER JOIN SISCOM_MI.ANAGRAFICA ON (SISCOM_MI.ANAGRAFICA.ID = ADEGUAMENTO_INTERESSI.ID_ANAGRAFICA) " _
                        & "INNER JOIN SISCOM_MI.RAPPORTI_UTENZA ON (RAPPORTI_UTENZA.ID=ID_CONTRATTO) " _
                        & l_where _
                        & "ORDER BY INTESTATARIO, CALCOLATI_AL DESC, DAL DESC "

            DT = par.getDataTableGrid(sStrSql)

            Dim nomefile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportElaborazioneInteressi", "Foglio", DT)

            If IO.File.Exists(Server.MapPath("..\filetemp\" & nomefile)) Then
                Response.Redirect("..\FileTemp\" & nomefile)
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Sub

End Class
