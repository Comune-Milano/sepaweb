
Partial Class CriptaPW
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then

                Button1.Visible = True
                TextBox1.Visible = False
                Button6.Visible = False
            End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Try
            Dim passwordHash As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            'par.cmd.CommandText = "CREATE TABLE OPERATORI_PRE_HASH_" & Format(Now, "ss") & " AS SELECT * FROM OPERATORI"
            'par.cmd.ExecuteNonQuery()

            'par.cmd.CommandText = "CREATE TABLE STORICO_PW_PRE_HASH_" & Format(Now, "ss") & " AS SELECT * FROM STORICO_PW"
            'par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM OPERATORI"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                passwordHash = par.ComputeHash(par.DeCripta(par.IfNull(myReader("PW"), "")), "SHA512", Nothing)
                par.cmd.CommandText = "UPDATE OPERATORI SET PW='" & passwordHash & "' WHERE ID=" & myReader("ID")
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM STORICO_PW"
            myReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                passwordHash = par.ComputeHash(par.DeCripta(par.IfNull(myReader("PW"), "")), "SHA512", Nothing)
                par.cmd.CommandText = "UPDATE STORICO_PW SET PW='" & passwordHash & "' WHERE ID=" & myReader("ID")
                par.cmd.ExecuteNonQuery()
            Loop
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("Operazione Effettuata")
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
