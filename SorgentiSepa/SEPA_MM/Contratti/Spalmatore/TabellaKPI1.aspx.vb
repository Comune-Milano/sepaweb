Imports Telerik.Web.UI
Imports System.IO

Partial Class Contratti_Spalmatore_TabellaKPI1
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGridKPI1.ClientID.ToString.Replace("ctl00", "MasterPage")
        End If
    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub

    Protected Sub RadGridKPI1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridKPI1.NeedDataSource
        Try
            Dim Query As String = " SELECT COD_CONTRATTO from SISCOM_MI.SPALM_KPI1"
            queryXLS = Query
            RadGridKPI1.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Public Property queryXLS() As String
        Get
            If Not (ViewState("par_queryXLS") Is Nothing) Then
                Return CStr(ViewState("par_queryXLS"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_queryXLS") = value
        End Set

    End Property

    Protected Sub Esporta_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            connData.apri()
            par.cmd.CommandText = queryXLS
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtRecords As New Data.DataTable()
            da.Fill(dtRecords)
            da.Dispose()
            connData.chiudi()
            Esporta(dtRecords)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Export_KPI1", "Export_KPI1", dt)
        If IO.File.Exists(Server.MapPath("..\..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!", True)
        End If
    End Sub

    Private Sub LeggiFile(ByVal nomeFile As String)

    End Sub

    Private Function UploadOnServer() As String
        UploadOnServer = ""

        Dim fileName As String = ""

        For Each file As UploadedFile In FileUpload1.UploadedFiles
            fileName = file.GetName()
            UploadOnServer = file.GetNameWithoutExtension() & "_" & Format(Now, "yyyyMMddHHmmss") & file.GetExtension
            UploadOnServer = Server.MapPath("..\..\FileTemp\") & UploadOnServer
            file.SaveAs(UploadOnServer)
        Next

        Return UploadOnServer
    End Function

    Protected Sub btnSalvaAllegato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalvaAllegato.Click
        Try
            Dim errore As String = ""
            Dim totRU As Integer = 0
            Dim totRUEffettivi As Integer = 0
            Dim FileName As String = UCase(UploadOnServer())
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".XLSX") Then
                    connData.apri(True)

                    Dim xls As New ExcelSiSol
                    Dim dtFoglio1 As New Data.DataTable

                    Using pck As New OfficeOpenXml.ExcelPackage()
                        Using stream = File.Open(FileName, FileMode.Open, FileAccess.Read)
                            pck.Load(stream)
                        End Using
                        Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
                        dtFoglio1 = xls.WorksheetToDataTable(ws, True)
                    End Using

                    Dim codRU As String = ""

                    Dim conta As Boolean = False
                    If dtFoglio1.Rows.Count > 0 Then
                        If dtFoglio1.Columns.Count = 1 Then
                            par.cmd.CommandText = "delete from siscom_mi.spalm_kpi1"
                            par.cmd.ExecuteNonQuery()
                            For Each row2 As Data.DataRow In dtFoglio1.Rows
                                Dim codice As String = par.PulisciStrSql(row2.Item(0))
                                par.cmd.CommandText = "SELECT ID,COD_CONTRATTO FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO = '" & Trim(codice.ToUpper) & "'"
                                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                If myReader.Read Then
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.spalm_kpi1 (ID_CONTRATTO,COD_CONTRATTO) VALUES (" & myReader("ID") & ",'" & myReader("COD_CONTRATTO") & "')"
                                    par.cmd.ExecuteNonQuery()
                                    conta = True
                                End If
                                myReader.Close()
                            Next
                            If conta = True Then
                                connData.chiudi(True)
                                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
                            Else
                                connData.chiudi(False)
                                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun contratto importato. Contenuto del file non valido!", 300, 150, "Attenzione", Nothing, Nothing)
                            End If
                        Else
                            connData.chiudi(False)
                            CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun contratto importato. Contenuto del file non valido!", 300, 150, "Attenzione", Nothing, Nothing)
                        End If
                    Else
                        connData.chiudi(False)
                        CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun contratto importato. Contenuto del file non valido!", 300, 150, "Attenzione", Nothing, Nothing)
                    End If
                Else
                    CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore. Formato file non corretto!", 300, 150, "Attenzione", Nothing, Nothing)
                End If
            Else
                CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Errore. Nessun file selezionato o formato non corretto!", 300, 150, "Attenzione", Nothing, Nothing)
            End If
            RadGridKPI1.Rebind()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
End Class
