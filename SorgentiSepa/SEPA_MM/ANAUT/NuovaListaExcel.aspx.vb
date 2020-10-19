Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class ANAUT_NuovaListaExcel
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim conndata As CM.datiConnessione = Nothing
    Public percentuale As Long = 0

    Public Property dtCompleta() As Data.DataTable
        Get
            If Not (ViewState("dtCompleta") Is Nothing) Then
                Return ViewState("dtCompleta")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtCompleta") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


    End Sub

    Protected Sub btnProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim FileName As String = UploadOnServer()
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And (FileName.Contains(".xls") Or FileName.Contains(".xlsx")) Then
                    ReadFile(FileName)
                Else
                    Response.Write("<script>alert('Tipo file non valido. Selezionare un file xls o xlsx');</script>")
                End If
            End If
        Catch ex As Exception

        End Try

    End Sub

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE EXCEL ##########
            If FileUpload.HasFile = True Then
                'nomeFile.Value = FileUpload.FileName
                UploadOnServer = Server.MapPath("..\FileTemp\") & FileUpload.FileName
                FileUpload.SaveAs(UploadOnServer)

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")

        End Try

        Return UploadOnServer
    End Function

    Private Sub ReadFile(ByVal file As String)
        Dim Nuova As Data.DataRow
        'Dim strConn As String = "Provider=Microsoft.ACE.OLEDB.12.0;" _
        '         & "data source=" & file & ";" _
        '         & "Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'"

        Dim strConn As String = "Provider=Microsoft.jet.OLEDB.4.0;" & _
                       "Data Source=" & file & ";" & _
                       "Extended Properties=""Excel 8.0;HDR=YES"""

        Dim objConn As New OleDbConnection(strConn)

        Dim strSql As String = "Select * from [RU$] "
        Dim dt As New Data.DataTable
        Dim ContaRighe As Integer = 0

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Try
                objConn.Open()
                Dim da As New OleDb.OleDbDataAdapter(strSql, objConn)
                da.Fill(dt)
                objConn.Dispose()

                dtCompleta = New Data.DataTable
                dtCompleta.Clear()
                dtCompleta.Columns.Clear()
                dtCompleta.Rows.Clear()
                dtCompleta.Columns.Add("ID_LISTA")
                dtCompleta.Columns.Add("RU")
                dtCompleta.Columns.Add("MOTIVAZIONE")

                If dt.Rows.Count > 0 Then
                    For Each row As Data.DataRow In dt.Rows

                        If RigoVuoto(row, dt.Columns.Count) = False And (row.Item(0).ToString.ToUpper.Contains("RU") = False) Then
                            If RigoVuoto(dt.Rows(ContaRighe), dt.Columns.Count) = False Then

                                par.cmd.CommandText = "SELECT * FROM UTENZA_LISTE_CDETT WHERE COD_CONTRATTO='" & row.Item(0).ToString & "' AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU =(SELECT MAX(ID) FROM UTENZA_BANDI WHERE STATO=1))"
                                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                                If myReader.Read Then
                                    If par.IfNull(myReader("ID_LISTA_CONV"), "-1") = "-1" Then
                                        Nuova = dtCompleta.NewRow
                                        Nuova.Item("ID_LISTA") = myReader("ID_LISTA")
                                        Nuova.Item("RU") = row.Item(0).ToString
                                        Nuova.Item("MOTIVAZIONE") = "OK"
                                        dtCompleta.Rows.Add(Nuova)
                                    Else
                                        Nuova = dtCompleta.NewRow
                                        Nuova.Item("ID_LISTA") = "-1"
                                        Nuova.Item("RU") = row.Item(0).ToString
                                        Nuova.Item("MOTIVAZIONE") = "PRESENTE IN ALTRA LISTA DI CONVOCAZIONE"
                                        dtCompleta.Rows.Add(Nuova)
                                    End If
                                Else
                                    Nuova = dtCompleta.NewRow
                                    Nuova.Item("ID_LISTA") = "-1"
                                    Nuova.Item("RU") = row.Item(0).ToString
                                    Nuova.Item("MOTIVAZIONE") = "CONTRATTO IN NESSUN ELENCO DI CONVOCAZIONE"
                                    dtCompleta.Rows.Add(Nuova)
                                End If
                                myReader.Close()
                            End If
                        End If
                        ContaRighe += 1
                    Next
                End If
                HttpContext.Current.Session.Add("AA", dtCompleta)
                'Response.Redirect("NuovaListaExcel1.aspx?F=" & par.Cripta(file))
                Response.Redirect("NuovaListaExcel1.aspx")

            Catch ex As Exception
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:ReadFile " & ex.Message)
                Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
                Exit Sub

            End Try

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

       
        
    End Sub

    Protected Sub CreaDT()

        '######### SVUOTA E CREA COLONNE DATATABLE #########
        dtCompleta.Clear()
        dtCompleta.Columns.Clear()
        dtCompleta.Rows.Clear()
        dtCompleta.Columns.Add("ID_LISTA")
        dtCompleta.Columns.Add("RU")
        dtCompleta.Columns.Add("MOTIVAZIONE")
    End Sub

    Private Function RigoVuoto(ByVal row As Data.DataRow, ByVal dtcol As Integer) As Boolean

        RigoVuoto = False
        Try
            For n As Integer = 0 To dtcol - 1
                If IsDBNull(row.Item(n)) Then
                    RigoVuoto = True
                Else
                    RigoVuoto = False
                    Exit For
                End If
            Next
        Catch ex As Exception

        End Try
        Return RigoVuoto

    End Function
End Class

