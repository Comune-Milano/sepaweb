Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class ANAUT_NuovaListaCSV
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim FileName As String = UploadOnServer()
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".csv") Then
                    ReadFileCSV(FileName)
                Else
                    Response.Write("<script>alert('Tipo file non valido. Selezionare un file csv');</script>")
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE csv ##########
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

    Private Sub ReadFileCSV(ByVal FileCSV As String)
        Try
            Dim sContenutoRiga As String = ""
            Dim dt As New Data.DataTable
            Dim ContaRighe As Integer = 0
            Dim Nuova As Data.DataRow

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            dtCompleta = New Data.DataTable
            dtCompleta.Clear()
            dtCompleta.Columns.Clear()
            dtCompleta.Rows.Clear()
            dtCompleta.Columns.Add("ID_LISTA")
            dtCompleta.Columns.Add("RU")
            dtCompleta.Columns.Add("MOTIVAZIONE")

            Dim sr1 As StreamReader = New StreamReader(FileCSV, System.Text.Encoding.GetEncoding("iso-8859-1"))
            Do While sr1.Peek() >= 0
                sContenutoRiga = sr1.ReadLine()
                If sContenutoRiga <> "" And UCase(sContenutoRiga) <> "RU" Then
                    par.cmd.CommandText = "SELECT * FROM UTENZA_LISTE_CDETT WHERE COD_CONTRATTO='" & UCase(sContenutoRiga) & "' AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU =(SELECT MAX(ID) FROM UTENZA_BANDI WHERE STATO=1))"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("ID_LISTA_CONV"), "-1") = "-1" Then
                            Nuova = dtCompleta.NewRow
                            Nuova.Item("ID_LISTA") = myReader("ID_LISTA")
                            Nuova.Item("RU") = UCase(sContenutoRiga)
                            Nuova.Item("MOTIVAZIONE") = "OK"
                            dtCompleta.Rows.Add(Nuova)
                        Else
                            Nuova = dtCompleta.NewRow
                            Nuova.Item("ID_LISTA") = "-1"
                            Nuova.Item("RU") = UCase(sContenutoRiga)
                            Nuova.Item("MOTIVAZIONE") = "PRESENTE IN ALTRA LISTA DI CONVOCAZIONE"
                            dtCompleta.Rows.Add(Nuova)
                        End If
                    Else
                        Nuova = dtCompleta.NewRow
                        Nuova.Item("ID_LISTA") = "-1"
                        Nuova.Item("RU") = UCase(sContenutoRiga)
                        Nuova.Item("MOTIVAZIONE") = "CONTRATTO IN NESSUN ELENCO DI CONVOCAZIONE"
                        dtCompleta.Rows.Add(Nuova)
                    End If
                    myReader.Close()

                    ContaRighe += 1
                End If
            Loop
            sr1.Close()

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If ContaRighe > 0 Then
                HttpContext.Current.Session.Add("AA", dtCompleta)
                Response.Redirect("NuovaListaExcel1.aspx?F=" & par.Cripta(FileCSV))
            Else
                Response.Write("<script>alert('Nessun contratto presente nel file');</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:ReadFile " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
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

End Class
