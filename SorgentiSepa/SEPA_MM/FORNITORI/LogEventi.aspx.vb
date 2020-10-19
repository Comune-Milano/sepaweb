Imports Telerik.Web.UI

Partial Class FORNITORI_Default
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            If Session.Item("MOD_FORNITORI_LOG") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                ' VerificaOperatore()
                BindGrid()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Log Eventi - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    'Public Property iIndiceFornitore() As String
    '    Get
    '        If Not (ViewState("par_iIndiceFornitore") Is Nothing) Then
    '            Return CStr(ViewState("par_iIndiceFornitore"))
    '        Else
    '            Return "0"
    '        End If
    '    End Get

    '    Set(ByVal value As String)
    '        ViewState("par_iIndiceFornitore") = value
    '    End Set
    'End Property

    'Private Sub VerificaOperatore()
    '    Try
    '        connData.apri()
    '        par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
    '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader.Read Then
    '            If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Or par.IfNull(myReader("FL_SUPERDIRETTORE"), "0") = "1" Then
    '                iIndiceFornitore = "0"
    '            Else
    '                If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
    '                    iIndiceFornitore = par.IfNull(myReader("MOD_FO_ID_FO"), 0)
    '                Else
    '                    iIndiceFornitore = "0"
    '                End If
    '            End If
    '        End If
    '        myReader.Close()
    '        connData.chiudi()
    '    Catch ex As Exception
    '        connData.chiudi()
    '        Session.Add("ERRORE", "Provenienza: Fornitori - LogEventi - VerificaOperatore - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try

    'End Sub

    Private Sub BindGrid()
        'Dim s As String = iIndiceFornitore

        '/////////////////////
        '// 1433/2019: Fornitore preso da repertorio (appalti)
        'sStrSql = "select OPERATORI.OPERATORE,OPERATORI.COGNOME,OPERATORI.NOME,(SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE ID=OPERATORI.mod_fo_id_fo) AS FORNITORE," _
        sStrSql = "select OPERATORI.OPERATORE,OPERATORI.COGNOME,OPERATORI.NOME, (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE APPALTI.ID=SEGNALAZIONI_FORNITORI.ID_APPALTO AND SEGNALAZIONI_FORNITORI.ID=EVENTI_SEGNALAZIONI_FO.ID_SEGNALAZIONE_FO AND FORNITORI.ID = APPALTI.ID_FORNITORE ) AS FORNITORE," _
                & "TO_DATE (substr(EVENTI_SEGNALAZIONI_FO.DATA_ORA,1,8), 'yyyymmdd') AS DATA_EVENTO,TAB_EVENTI.DESCRIZIONE AS EVENTO,EVENTI_SEGNALAZIONI_FO.MOTIVAZIONE " _
                & ",(select MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO from SISCOM_MI.MANUTENZIONI,SISCOM_MI.SEGNALAZIONI_FORNITORI where MANUTENZIONI.ID=SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE AND SEGNALAZIONI_FORNITORI.ID=EVENTI_SEGNALAZIONI_FO.ID_SEGNALAZIONE_FO) AS ODL, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID IN (SELECT STATO  " _
                & " FROM SISCOM_MI.MANUTENZIONI, SISCOM_MI.SEGNALAZIONI_FORNITORI  " _
                & " WHERE     MANUTENZIONI.ID = SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE  " _
                & " AND SEGNALAZIONI_FORNITORI.ID =  " _
                & " EVENTI_SEGNALAZIONI_FO.ID_SEGNALAZIONE_FO))  " _
                & " AS STATO,  " _
                & "(SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE APPALTI.ID=SEGNALAZIONI_FORNITORI.ID_APPALTO AND SEGNALAZIONI_FORNITORI.ID=EVENTI_SEGNALAZIONI_FO.ID_SEGNALAZIONE_FO) AS NUM_REPERTORIO " _
                & "from SISCOM_MI.EVENTI_SEGNALAZIONI_FO,operatori,SISCOM_MI.TAB_EVENTI " _
                & "WHERE TAB_EVENTI.COD (+)=EVENTI_SEGNALAZIONI_FO.COD_EVENTO AND OPERATORI.ID=EVENTI_SEGNALAZIONI_FO.ID_OPERATORE " _
                & "ORDER BY DATA_ORA DESC,OPERATORE"
    End Sub

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

    Protected Sub RadGridRPTLOG_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridRPTLOG.ItemDataBound
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

    Protected Sub RadGridRPTLOG_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridRPTLOG.NeedDataSource
        If sStrSql <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
            Dim dt As System.Data.DataTable = TryCast(sender, RadGrid).DataSource
            NumeroElementi = dt.Rows.Count
        End If
    End Sub

    Protected Sub RadGridRPTLOG_PreRender(sender As Object, e As System.EventArgs) Handles RadGridRPTLOG.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        isExporting.Value = "1"
        Dim context As RadProgressContext = RadProgressContext.Current
        context.SecondaryTotal = 0
        context.SecondaryValue = 0
        context.SecondaryPercent = 0
        RadGridRPTLOG.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        RadGridRPTLOG.ExportSettings.IgnorePaging = True
        RadGridRPTLOG.ExportSettings.ExportOnlyData = True
        RadGridRPTLOG.ExportSettings.OpenInNewWindow = True
        RadGridRPTLOG.MasterTableView.ExportToExcel()
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
End Class
