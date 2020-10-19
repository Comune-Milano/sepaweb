Imports Telerik.Web.UI

Partial Class SICUREZZA_RicercaProcedimenti
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                'CaricaRisultati()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Procedimenti - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        CaricaRisultati()
        MultiViewRicerca.ActiveViewIndex = 1
        MultiViewBottoni.ActiveViewIndex = 1
    End Sub

    Private Sub CaricaRisultati()
        Try
            Dim dt As New Data.DataTable
            Dim CondizioneRicerca As String = ""
            If Trim(TextBoxNumero.Text) <> "" AndAlso IsNumeric(TextBoxNumero.Text) Then
                CondizioneRicerca &= " AND ID_INTERVENTO=" & TextBoxNumero.Text
            End If
            If IsNumeric(cmbTipoProc.SelectedValue) AndAlso cmbTipoProc.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND INTERVENTI_SICUREZZA.TIPO='" & cmbTipoProc.SelectedValue & "'"
            End If
            If IsNumeric(cmbStatoProc.SelectedValue) AndAlso cmbStatoProc.SelectedValue <> "-1" Then
                CondizioneRicerca &= " AND PROCEDIMENTI_SICUREZZA.STATO=" & cmbStatoProc.SelectedValue
            End If
            If txtReferente.Text <> "" Then
                CondizioneRicerca &= " AND INTERVENTI_SICUREZZA.REFERENTE='" & par.PulisciStrSql(txtReferente.Text.ToUpper) & "'"
            End If

            Dim dataMin As String = ""
            Dim dataMax As String = ""
            If Not IsNothing(txtDal.SelectedDate) Then
                dataMin = par.AggiustaData(txtDal.SelectedDate)
            End If
            If dataMin <> "" Then
                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_INSERIMENTO,1,8)>='" & dataMin & "'  "
            End If
            If Not IsNothing(txtAl.SelectedDate) Then
                dataMax = par.AggiustaData(txtAl.SelectedDate)
            End If
            If dataMax <> "" Then
                CondizioneRicerca &= " AND SUBSTR(DATA_ORA_INSERIMENTO,1,8)<='" & dataMax & "' "
            End If
            par.cmd.CommandText = " SELECT distinct PROCEDIMENTI_SICUREZZA.ID," _
                           & " PROCEDIMENTI_SICUREZZA.ID AS NUM,(CASE WHEN STATO=1 THEN 'In Corso' WHEN STATO=2 THEN 'Chiuso' END) as STATO, TIPO, " _
                           & " REFERENTE, " _
                           & " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_INSERIMENTO,0,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ORA_INSERIMENTO " _
                           & " ,DATA_APERTURA " _
                           & " FROM SISCOM_MI.PROCEDIMENTI_SICUREZZA " _
                           & " WHERE 1=1 " _
                           & CondizioneRicerca _
            & " ORDER BY DATA_APERTURA DESC "

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)

            Session.Item("DataGridProc") = dt
            RadGridProcedimenti.CurrentPageIndex = 0
            RadGridProcedimenti.Rebind()
            If dt.Rows.Count > 1 Then
                lblRisultati.Text = "Trovati - " & dt.Rows.Count & " procedimenti"
            ElseIf dt.Rows.Count = 1 Then
                lblRisultati.Text = "Trovato - " & dt.Rows.Count & " procedimento"
            ElseIf dt.Rows.Count = 0 Then
                lblRisultati.Text = ""
            End If
            MultiViewRicerca.ActiveViewIndex = 1
            MultiViewBottoni.ActiveViewIndex = 1
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Procedimenti - BindGrid - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx", False)
    End Sub

    Protected Sub RadGridProcedimenti_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridProcedimenti.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For Each column As GridColumn In RadGridProcedimenti.MasterTableView.RenderColumns
                If (TypeOf column Is GridBoundColumn) Then
                    dataItem(column.UniqueName).ToolTip = dataItem(column.UniqueName).Text.ToString.Replace("&nbsp;", "")
                End If
            Next
            dataItem.Attributes.Add("onclick", "document.getElementById('HiddenFieldProcedimento').value='" & dataItem("NUM").Text & "';" _
                                             & "document.getElementById('txtProcedimSelected').value='Hai selezionato il procedimento " & dataItem("NUM").Text & "';")
            dataItem.Attributes.Add("onDblclick", "apriProc();")
        End If
    End Sub

    Protected Sub RadGridProcedimenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridProcedimenti.NeedDataSource
        Dim dt As Data.DataTable = CType(Session.Item("DataGridProc"), Data.DataTable)
        TryCast(sender, RadGrid).DataSource = CType(Session.Item("DataGridProc"), Data.DataTable)
    End Sub

    Protected Sub btnVisualizza_Click(sender As Object, e As System.EventArgs) Handles btnVisualizza.Click
        If IsNumeric(HiddenFieldProcedimento.Value) AndAlso HiddenFieldProcedimento.Value <> "-1" AndAlso HiddenFieldProcedimento.Value <> "0" Then
            Dim apertura As String = "window.open('NuovoProcedimento.aspx?NM=1&IDP=" & HiddenFieldProcedimento.Value & "', 'nint' + '" & Format(Now, "yyyyMMddHHmmss") & "', 'height=' + screen.height/3*2 + ',top=100,left=100,width=' + screen.width/3*2 + ',scrollbars=no,resizable=yes');"
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType, "apri", apertura, True)
        Else
            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun procedimento selezionato!", 300, 150, "Attenzione", Nothing, Nothing)
        End If
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Try
            Dim dt As Data.DataTable = CType(Session.Item("DataGridProc"), Data.DataTable)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportProced", "ExportProced", dt, True, "../FileTemp/", False)

            If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)

            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            ScriptManager.RegisterStartupScript(Page, Page.GetType, "noC", "noCaricamento=1;", True)
            MultiViewBottoni.ActiveViewIndex = 1
            MultiViewRicerca.ActiveViewIndex = 1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Ricerca Procedimento - btnExport_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
