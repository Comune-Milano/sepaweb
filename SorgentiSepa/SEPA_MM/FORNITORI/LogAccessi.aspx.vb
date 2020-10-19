
Imports Telerik.Web.UI

Partial Class FORNITORI_LogAccessi
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

            Session.Add("ERRORE", "Provenienza: Fornitori - Log Accessi - Carica - " & ex.Message)
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

        sStrSql = " SELECT (SELECT OPERATORE FROM OPERATORI WHERE ID = ID_OPERATORE) AS OPERATORE, " _
                & " COUNT (ID_OPERATORE) AS NUMERO, " _
                & " SUBSTR(GETDATAORA(MAX(OPERATORI_WEB_LOG.DATA_ORA_IN)), 1, LENGTH(GETDATAORA(MAX(OPERATORI_WEB_LOG.DATA_ORA_IN))) - 1) AS DATA  " _
                & " FROM OPERATORI_WEB_LOG " _
                & " WHERE ID_OPERATORE IN (SELECT ID FROM OPERATORI WHERE NVL(MOD_FO_ID_FO,0)>0 AND MOD_FO_LIMITAZIONI =1)  " _
                & " GROUP BY ID_OPERATORE " _
                & " ORDER BY 1"
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
