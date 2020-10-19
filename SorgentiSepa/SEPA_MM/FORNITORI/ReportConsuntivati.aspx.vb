Imports System.IO
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
            If Session.Item("MOD_FORNITORI_RPT") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                VerificaOperatore()
                BindGrid()
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Report Consuntivati - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Public Property iIndiceFornitore() As String
        Get
            If Not (ViewState("par_iIndiceFornitore") Is Nothing) Then
                Return CStr(ViewState("par_iIndiceFornitore"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_iIndiceFornitore") = value
        End Set
    End Property

    Private Sub VerificaOperatore()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("LIVELLO_WEB"), "0") = "1" Or par.IfNull(myReader("FL_SUPERDIRETTORE"), "0") = "1" Then
                    iIndiceFornitore = "0"
                Else
                    If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
                        iIndiceFornitore = par.IfNull(myReader("MOD_FO_ID_FO"), 0)
                    Else
                        iIndiceFornitore = "0"
                    End If
                End If
            End If
            myReader.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - VerificaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub


    Private Sub BindGrid()
        Dim s As String = iIndiceFornitore
        'If iIndiceFornitore = "0" Then
        '    s = "(select distinct id_fornitore from siscom_mi.appalti)"
        'Else
        '    s = iIndiceFornitore
        'End If
        sStrSql = "select MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO AS NUM_ODL," _
                & "APPALTI.NUM_REPERTORIO,MANUTENZIONI.DESCRIZIONE AS DESCR_MA, " _
                & "to_date(MANUTENZIONI.DATA_PGI,'yyyymmdd') AS DATA_PGI, " _
                & "to_date(MANUTENZIONI.DATA_TDL,'yyyymmdd') AS DATA_TDL, " _
                & "to_date(SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO,'yyyymmdd') AS DATA_FINE_INTERVENTO, " _
                & "(CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI_FO_IRR WHERE VISIBILE=1 AND ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID)>1 THEN 'SI' ELSE 'NO' END)  AS NON_CONFORMITA, " _
                & " (CASE WHEN MANUTENZIONI.ID_UNITA_IMMOBILIARI IS NOT NULL THEN " _
                & " (SELECT DESCRIZIONE || ' n.' || civico || ' Località: ' || localita || ' ('|| cap || ')'  FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID = (SELECT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID =  MANUTENZIONI.ID_UNITA_IMMOBILIARI))  " _
                & " WHEN MANUTENZIONI.ID_EDIFICIO IS NOT NULL THEN " _
                & " (SELECT DESCRIZIONE || ' n.' || civico || ' Località: ' || localita || ' ('|| cap || ')'  FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID = (SELECT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = MANUTENZIONI.ID_EDIFICIO)) " _
                & " WHEN MANUTENZIONI.ID_COMPLESSO IS NOT NULL THEN " _
                & " (SELECT DESCRIZIONE || ' n.' || civico || ' Località: ' || localita || ' ('|| cap || ')'  FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID = (SELECT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID = MANUTENZIONI.ID_COMPLESSO)) " _
                & " END) AS INDIRIZZO " _
                & "from " _
                & "SISCOM_MI.APPALTI, SISCOM_MI.MANUTENZIONI, SISCOM_MI.SEGNALAZIONI_FORNITORI " _
                & "WHERE " _
                & "manutenzioni.id_appalto=appalti.id and appalti.modulo_fornitori=1 AND appalti.MODULO_FORNITORI_GE=1 AND " _
                & "SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID AND MANUTENZIONI.STATO=2 " _
                & "AND APPALTI.ID_FORNITORE IN (" & s & ") " _
                & "ORDER BY NVL(DATA_FINE_INTERVENTO,TO_DATE('19000101','yyyymmdd')) DESC, NVL(DATA_TDL,TO_DATE('19000101','yyyymmdd')) DESC, NVL(DATA_PGI,TO_DATE('19000101','yyyymmdd')) DESC, MANUTENZIONI.ANNO DESC,MANUTENZIONI.PROGR DESC"
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

    Protected Sub RadGridRPTConsuntivi_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridRPTConsuntivi.ItemDataBound
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

    Protected Sub RadGridRPTConsuntivi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridRPTConsuntivi.NeedDataSource
        If sStrSql <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
            Dim dt As System.Data.DataTable = TryCast(sender, RadGrid).DataSource
            NumeroElementi = dt.Rows.Count
        End If
    End Sub

    Protected Sub RadGridRPTConsuntivi_PreRender(sender As Object, e As System.EventArgs) Handles RadGridRPTConsuntivi.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        'isExporting.Value = "1"
        'Dim context As RadProgressContext = RadProgressContext.Current
        'context.SecondaryTotal = 0
        'context.SecondaryValue = 0
        'context.SecondaryPercent = 0
        'RadGridRPTConsuntivi.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        'RadGridRPTConsuntivi.ExportSettings.IgnorePaging = True
        'RadGridRPTConsuntivi.ExportSettings.ExportOnlyData = True
        'RadGridRPTConsuntivi.ExportSettings.OpenInNewWindow = True
        'RadGridRPTConsuntivi.MasterTableView.ExportToExcel()

        connData.apri(False)
        Dim dt As New Data.DataTable
        dt = par.getDataTableFilterSortRadGrid(sStrSql, RadGridRPTConsuntivi)
        connData.chiudi(False)
        If dt.Rows.Count > 0 Then
            Dim xls As New ExcelSiSol
            Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportConsuntivatiS", "ExportConsuntivati", dt)
            If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
            Else
                par.MessaggioAlert(CType(Me.Master.FindControl("RadWindowManagerMaster"), RadWindowManager), "Si è verificato un errore durante l\'esportazione. Riprovare!", 450, 150, "Attenzione", Nothing)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", "function f(){NotificaTelerik('" & CType(Me.Master.FindControl("RadNotificationNote"), RadNotification).ClientID & "', 'Attenzione', '" & par.Messaggio_NoExport & "'); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", True)
        End If

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
