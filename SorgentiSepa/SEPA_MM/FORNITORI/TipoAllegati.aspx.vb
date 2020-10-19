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
            If Session.Item("MOD_FORNITORI_PARAM") <> "1" Then
                Response.Redirect("../AccessoNegato.htm", False)
            End If
            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                BindGrid()
                'CType(Master.FindControl("optMenu"), HiddenField).Value = "1"
            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza: Fornitori - Parametri - Carica - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub BindGrid()

        sStrSql = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI ORDER BY DESCRIZIONE ASC"
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

    Protected Sub RadGridTipi_BatchEditCommand(sender As Object, e As Telerik.Web.UI.GridBatchEditingEventArgs) Handles RadGridTipi.BatchEditCommand
        Try
            Dim TotaleOperazioni As Integer = e.Commands.Count
            If TotaleOperazioni > 0 Then
                connData.apri(True)
                Dim NrInsert As Integer = 0
                Dim NrUpdate As Integer = 0
                Dim NrDelete As Integer = 0
                Dim NrDeleteNo As Integer = 0
                For Each command As GridBatchEditingCommand In e.Commands
                    Dim oldValues As Hashtable = command.OldValues
                    Dim newValues As Hashtable = command.NewValues
                    If command.Type = GridBatchEditingCommandType.Insert Then
                        Dim DescrizioneCompleta As String = UCase(newValues("DESCRIZIONE"))
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI (ID,DESCRIZIONE) VALUES " _
                                            & "(SISCOM_MI.SEQ_SEGNALAZIONI_FO_ALL_TIPI.NEXTVAL, " & par.insDbValue(DescrizioneCompleta, True) & ")"
                        NrInsert = NrInsert + par.cmd.ExecuteNonQuery

                        Modificato.Value = "0"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
                    ElseIf command.Type = GridBatchEditingCommandType.Update Then
                        Dim DescrizioneCompleta As String = UCase(newValues("DESCRIZIONE"))
                        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI SET DESCRIZIONE = " & par.insDbValue(newValues("DESCRIZIONE"), True) & " " _
                                            & "WHERE ID = " & par.insDbValue(newValues("ID"), False)
                        NrUpdate = NrUpdate + par.cmd.ExecuteNonQuery

                        Modificato.Value = "0"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
                    ElseIf command.Type = GridBatchEditingCommandType.Delete Then
                        Try
                            par.cmd.CommandText = "DELETE FROM SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI WHERE ID = " & par.insDbValue(newValues("ID"), False)
                            NrDelete = NrDelete + par.cmd.ExecuteNonQuery

                            Modificato.Value = "0"
                            ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Naviga", "validNavigation = true;", True)
                        Catch ex1 As Oracle.DataAccess.Client.OracleException
                            If ex1.Number = 2292 Then
                                NrDeleteNo = NrDeleteNo + 1
                            End If
                        End Try
                    End If
                Next
                connData.chiudi(True)
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata" & "<br/>" _
                                       & "Doc. inseriti: " & NrInsert & ",<br/> " _
                                       & "Doc modificati: " & NrUpdate & ",<br/> " _
                                       & "Doc eliminato: " & NrDelete & ",<br/> " _
                                       & "Doc non eliminate perchè utilizzati: " & NrDeleteNo & ".", 300, 150, "Info", Nothing, Nothing)
                
            End If
            RadGridTipi.Rebind()
            Modificato.Value = "0"
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - GestTipiDoc - BatchEditCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridTipi_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridTipi.ItemCommand
        If UCase(e.CommandName) = "REBINDGRID" Then
            Modificato.Value = "0"
        End If
    End Sub

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

    Protected Sub RadGridTipi_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridTipi.ItemDataBound
        If isExporting.Value = "1" Then
            If e.Item.ItemIndex > 0 Then
                Dim context As RadProgressContext = RadProgressContext.Current
                If context.SecondaryTotal <> NumeroElementi Then
                    context.SecondaryTotal = NumeroElementi
                End If
                'context.SecondaryTotal = NumeroElementi
                context.SecondaryValue = e.Item.ItemIndex.ToString()
                context.SecondaryPercent = Int((e.Item.ItemIndex.ToString() * 100) / NumeroElementi)
                context.CurrentOperationText = "Export excel in corso"
            End If
        End If
    End Sub

    Protected Sub RadGridTipi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridTipi.NeedDataSource
        If sStrSql <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSql)
            Dim dt As System.Data.DataTable = TryCast(sender, RadGrid).DataSource
            NumeroElementi = dt.Rows.Count
        End If
    End Sub

    Protected Sub RadGridTipi_PreRender(sender As Object, e As System.EventArgs) Handles RadGridTipi.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Sub VisualizzaAlert(ByVal TestoMessaggio As String, ByVal Tipo As Integer)
        Select Case Tipo
            Case 1
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','success');", True)
            Case 2
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "warn", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','warn');", True)
            Case 3
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "error", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','error');", True)
        End Select
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click

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
        RadGridTipi.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        RadGridTipi.ExportSettings.IgnorePaging = True
        RadGridTipi.ExportSettings.ExportOnlyData = True
        RadGridTipi.ExportSettings.OpenInNewWindow = True
        RadGridTipi.MasterTableView.ExportToExcel()
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
