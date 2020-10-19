Imports System.IO

Partial Class AMMSEPA_Controllo_UploadFile
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                FileUpload1.Visible = True
                Label1.Visible = True
                Button7.Visible = True
                Label2.Visible = True
                Label3.Visible = True
                TextBox2.Visible = True
            End If
        End If
    End Sub

    Protected Sub Button7_Click(sender As Object, e As System.EventArgs) Handles Button7.Click
        If FileUpload1.HasFile = True Then
            If InStr(UCase(FileUpload1.FileName), ".EXE") > 0 Then
                Response.Write("<script>alert('File non compatibile!');</script>")
                Exit Sub
            End If
        End If
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim valore As String = TextBox2.Text

            'par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=124"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    valore = par.IfNull(myReader("VALORE"), "")
            'End If
            'myReader.Close()

            If File.Exists(Server.MapPath("..\ALLEGATI\" & valore & "\" & FileUpload1.FileName)) = True Then
                Response.Write("<script>alert('Errore: Il file che si tenta di inviare è gia presente sul server. Cancellare prima di inviare!');</script>")
            Else
                FileUpload1.SaveAs(Server.MapPath("..\ALLEGATI\" & valore & "\" & FileUpload1.FileName))
                Response.Write("<script>alert('Operazione effettuata!');</script>")
            End If

            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try

    End Sub
End Class
