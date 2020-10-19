Imports System.Collections
Imports System.Data.OleDb
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO
Imports System.Math
Imports Telerik.Web.UI
Imports System.Data

Partial Class RitenuteLegge

    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable
    Public DataAttuale As String = ""
    Private isFilter As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim dataNow As Date = Now.Date
        DataAttuale = dataNow
        ' lblTitolo.Text = "Ritenute di Legge"

        '##### CARICAMENTO PAGINA #####
        'Dim Str As String
        'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        'Str = Str & "font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        'Str = Str & "<" & "/div>"
        'Response.Write(Str)
        Try
            If Not IsPostBack Then
                HFGriglia.Value = DataGrid1.ClientID
                If Not IsNothing(Session.Item("dt_Ritenute")) Then
                    Session.Remove("dt_Ritenute")
                End If
                '##### CARICAMENTO FORNITORI #####
                'apriConnessione()
                'par.cmd.CommandText = "SELECT DISTINCT SISCOM_MI.FORNITORI.ID,SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI WHERE(SISCOM_MI.PRENOTAZIONI.ID_APPALTO = SISCOM_MI.APPALTI.ID And SISCOM_MI.APPALTI.FL_RIT_LEGGE = 1) AND SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE ORDER BY RAGIONE_SOCIALE"
                'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'ddlFornitori.Items.Clear()
                'ddlFornitori.Items.Add("Qualsiasi")
                'While myReader.Read
                '    ddlFornitori.Items.Add(New ListItem(UCase(par.IfNull(myReader("RAGIONE_SOCIALE"), "")), par.IfNull(myReader("ID"), "")))
                'End While
                'myReader.Close()
                'chiudiConnessione()
                '#################################

                '    BindGrid()

            End If

        Catch ex As Exception

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub apriConnessione()
        Try
            If par.OracleConn.State = 0 Then
                par.OracleConn.Open()
            End If
            par.cmd = par.OracleConn.CreateCommand
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('RitenuteLegge.aspx');</script>")
        End Try

    End Sub

    Protected Sub chiudiConnessione()
        Try
            If par.OracleConn.State = 1 Then

                par.cmd.Dispose()
                par.OracleConn.Close()

            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Response.Write("<script>parent.main.location.replace('RitenuteLegge.aspx');</script>")
        End Try

    End Sub


    Protected Sub BindGrid()
        Try
            apriConnessione()
            'If ddlFornitori.SelectedValue = "Qualsiasi" Then
            '    par.cmd.CommandText = "SELECT SISCOM_MI.APPALTI.DESCRIZIONE,SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.DATA_REPERTORIO,(SELECT SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE) AS RAGIONE_SOCIALE,(SELECT SUM(NVL(SISCOM_MI.PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID) AS SOMMA_RIT_LEGGE_IVATA FROM SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.ID IN (SELECT DISTINCT SISCOM_MI.APPALTI.ID FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID AND SISCOM_MI.APPALTI.FL_RIT_LEGGE=1)"
            'Else
            '    par.cmd.CommandText = "SELECT SISCOM_MI.APPALTI.DESCRIZIONE,SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.DATA_REPERTORIO,(SELECT SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE) AS RAGIONE_SOCIALE,(SELECT SUM(NVL(SISCOM_MI.PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID) AS SOMMA_RIT_LEGGE_IVATA FROM SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.ID IN (SELECT DISTINCT SISCOM_MI.APPALTI.ID FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID AND SISCOM_MI.APPALTI.FL_RIT_LEGGE='1' AND SISCOM_MI.APPALTI.ID_FORNITORE='" & ddlFornitori.SelectedValue & "')"
            'End If

            par.cmd.CommandText = "SELECT SISCOM_MI.APPALTI.DESCRIZIONE,SISCOM_MI.APPALTI.NUM_REPERTORIO,getdata(SISCOM_MI.APPALTI.DATA_REPERTORIO) as data_repertorio, " _
                & "(SELECT SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE) AS RAGIONE_SOCIALE," _
                & "(SELECT SUM(NVL(SISCOM_MI.PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_sTATO<>-3 AND PRENOTAZIONI.TIPO_PAGAMENTO IN (3,6,7) AND SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID) AS SOMMA_RIT_LEGGE_IVATA " _
                & "FROM SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.ID IN " _
                & "(SELECT DISTINCT SISCOM_MI.APPALTI.ID FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID " _
                & "AND SISCOM_MI.APPALTI.FL_RIT_LEGGE=1) "

            Dim totale As Double = 0
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            dt.Clear()
            dt.Columns.Clear()
            dt.Rows.Clear()
            dt.Columns.Add("DESCRIZIONE")
            dt.Columns.Add("NUM_REPERTORIO")
            dt.Columns.Add("DATA_REPERTORIO")
            dt.Columns.Add("RAGIONE_SOCIALE")
            dt.Columns.Add("SOMMA_RIT_LEGGE_IVATA")
            Dim ROW As Data.DataRow
            While myReader.Read
                ROW = dt.NewRow()
                ROW.Item("DESCRIZIONE") = UCase(par.IfNull(myReader("DESCRIZIONE"), ""))
                ROW.Item("NUM_REPERTORIO") = UCase(par.IfNull(myReader("NUM_REPERTORIO"), ""))
                Dim dataRepertorio As String = (par.IfNull(myReader("DATA_REPERTORIO"), ""))

                If dataRepertorio <> "" Then
                    ROW.Item("DATA_REPERTORIO") = Right(dataRepertorio, 2) & "/" & Mid(dataRepertorio, 5, 2) & "/" & Left(dataRepertorio, 4)
                Else
                    ROW.Item("DATA_REPERTORIO") = ""
                End If

                ROW.Item("RAGIONE_SOCIALE") = UCase(par.IfNull(myReader("RAGIONE_SOCIALE"), ""))
                Dim arrotondata As String = Round(CDbl((par.IfNull(myReader("SOMMA_RIT_LEGGE_IVATA"), 0))), 2).ToString("#,##0.00")
                ROW.Item("SOMMA_RIT_LEGGE_IVATA") = arrotondata
                dt.Rows.Add(ROW)
                totale = totale + arrotondata

            End While

            ROW = dt.NewRow
            ROW.Item("DESCRIZIONE") = ""
            ROW.Item("NUM_REPERTORIO") = ""
            ROW.Item("DATA_REPERTORIO") = ""
            ROW.Item("RAGIONE_SOCIALE") = ""
            ROW.Item("SOMMA_RIT_LEGGE_IVATA") = "TOT.   " & totale.ToString("#,##0.00")
            dt.Rows.Add(ROW)

            If Not IsNothing(Session.Item("dt_Ritenute")) Then
                Session.Item("dt_Ritenute") = dt
            Else
                Session.Add("dt_Ritenute", dt)
            End If
            myReader.Close()



        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
        par.cmd.Dispose()
        chiudiConnessione()

    End Sub



    'Protected Sub ddlFornitori_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFornitori.SelectedIndexChanged
    '    BindGrid()
    'End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.EventArgs) Handles btnHome.Click
        Response.Redirect("../../pagina_home_ncp.aspx")
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

    Protected Sub DataGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGrid1.NeedDataSource
        Try
            apriConnessione()
            'If ddlFornitori.SelectedValue = "Qualsiasi" Then
            '    par.cmd.CommandText = "SELECT SISCOM_MI.APPALTI.DESCRIZIONE,SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.DATA_REPERTORIO,(SELECT SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE) AS RAGIONE_SOCIALE,(SELECT SUM(NVL(SISCOM_MI.PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID) AS SOMMA_RIT_LEGGE_IVATA FROM SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.ID IN (SELECT DISTINCT SISCOM_MI.APPALTI.ID FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID AND SISCOM_MI.APPALTI.FL_RIT_LEGGE=1)"
            'Else
            '    par.cmd.CommandText = "SELECT SISCOM_MI.APPALTI.DESCRIZIONE,SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.DATA_REPERTORIO,(SELECT SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE) AS RAGIONE_SOCIALE,(SELECT SUM(NVL(SISCOM_MI.PRENOTAZIONI.RIT_LEGGE_IVATA,0)) FROM SISCOM_MI.PRENOTAZIONI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID) AS SOMMA_RIT_LEGGE_IVATA FROM SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.ID IN (SELECT DISTINCT SISCOM_MI.APPALTI.ID FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID AND SISCOM_MI.APPALTI.FL_RIT_LEGGE='1' AND SISCOM_MI.APPALTI.ID_FORNITORE='" & ddlFornitori.SelectedValue & "')"
            'End If

            par.cmd.CommandText = "SELECT SISCOM_MI.APPALTI.DESCRIZIONE,SISCOM_MI.APPALTI.NUM_REPERTORIO,TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYMMDD') as data_repertorio, " _
                                & "(SELECT SISCOM_MI.FORNITORI.RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE SISCOM_MI.FORNITORI.ID=SISCOM_MI.APPALTI.ID_FORNITORE) AS RAGIONE_SOCIALE, " _
                                & "(SELECT SUM(NVL(SISCOM_MI.PRENOTAZIONI.RIT_LEGGE_IVATA,0)) " _
                                & "FROM SISCOM_MI.PRENOTAZIONI " _
                                & "WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID) AS SOMMA_RIT_LEGGE_IVATA " _
                                & " FROM SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.ID IN (SELECT DISTINCT SISCOM_MI.APPALTI.ID FROM SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI WHERE SISCOM_MI.PRENOTAZIONI.ID_APPALTO=SISCOM_MI.APPALTI.ID AND SISCOM_MI.APPALTI.FL_RIT_LEGGE=1)"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt

            'Dim totale As Double = 0
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'dt.Clear()
            'dt.Columns.Clear()
            'dt.Rows.Clear()
            'dt.Columns.Add("DESCRIZIONE")
            'dt.Columns.Add("NUM_REPERTORIO")
            'dt.Columns.Add("DATA_REPERTORIO")
            'dt.Columns.Add("RAGIONE_SOCIALE")
            'dt.Columns.Add("SOMMA_RIT_LEGGE_IVATA")
            'Dim ROW As Data.DataRow
            'While myReader.Read
            '    ROW = dt.NewRow()
            '    ROW.Item("DESCRIZIONE") = UCase(par.IfNull(myReader("DESCRIZIONE"), ""))
            '    ROW.Item("NUM_REPERTORIO") = UCase(par.IfNull(myReader("NUM_REPERTORIO"), ""))
            '    Dim dataRepertorio As String = (par.IfNull(myReader("DATA_REPERTORIO"), ""))

            '    If dataRepertorio <> "" Then
            '        ROW.Item("DATA_REPERTORIO") = Right(dataRepertorio, 2) & "/" & Mid(dataRepertorio, 5, 2) & "/" & Left(dataRepertorio, 4)
            '    Else
            '        ROW.Item("DATA_REPERTORIO") = ""
            '    End If

            '    ROW.Item("RAGIONE_SOCIALE") = UCase(par.IfNull(myReader("RAGIONE_SOCIALE"), ""))
            '    Dim arrotondata As String = Round(CDbl((par.IfNull(myReader("SOMMA_RIT_LEGGE_IVATA"), 0))), 2).ToString("#,##0.00")
            '    ROW.Item("SOMMA_RIT_LEGGE_IVATA") = arrotondata
            '    dt.Rows.Add(ROW)
            '    totale = totale + arrotondata

            'End While

            'ROW = dt.NewRow
            'ROW.Item("DESCRIZIONE") = ""
            'ROW.Item("NUM_REPERTORIO") = ""
            'ROW.Item("DATA_REPERTORIO") = ""
            'ROW.Item("RAGIONE_SOCIALE") = ""
            'ROW.Item("SOMMA_RIT_LEGGE_IVATA") = "TOT.   " & totale.ToString("#,##0.00")
            'dt.Rows.Add(ROW)

            'If Not IsNothing(Session.Item("dt_Ritenute")) Then
            '    Session.Item("dt_Ritenute") = dt
            'Else
            '    Session.Add("dt_Ritenute", dt)
            'End If
            'myReader.Close()

            'If dt.Rows.Count > 0 Then
            '    'DataGrid1.DataSource = dt
            '    'DataGrid1.DataBind()
            'Else
            '    'ERRORE
            'End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
        Catch ex As Exception
            chiudiConnessione()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
        par.cmd.Dispose()
        chiudiConnessione()
    End Sub

    Private Sub EsportaExcel()
        isExporting.Value = "1"
        Dim context As RadProgressContext = RadProgressContext.Current
        context.SecondaryTotal = 0
        context.SecondaryValue = 0
        context.SecondaryPercent = 0
        DataGrid1.ExportSettings.Excel.Format = GridExcelExportFormat.Xlsx
        DataGrid1.ExportSettings.IgnorePaging = True
        DataGrid1.ExportSettings.ExportOnlyData = True
        DataGrid1.ExportSettings.OpenInNewWindow = True
        DataGrid1.MasterTableView.ExportToExcel()
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
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "FORNITORI", "FORNITORI", dt)
        If IO.File.Exists(Server.MapPath("..\/..\/..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!');</script>")
        End If
    End Sub
End Class