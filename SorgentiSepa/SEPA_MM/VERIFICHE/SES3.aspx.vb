
Partial Class AMMSEPA_Controllo_Esegui
    Inherits PageSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If PAR.VerificaPW(TextBox1.Text) = True Then
                Button2.Visible = True
                TextBox2.Visible = True
            End If
        End If
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            PAR.OracleConn.Open()
            par.SettaCommand(par)

            Dim sComando As String = TextBox2.Text

            sComando = Replace(sComando, "chr(10)", Chr(10))
            sComando = Replace(sComando, "chr(13)", Chr(13))

            PAR.cmd.CommandText = sComando
            Dim i As Integer = PAR.cmd.ExecuteNonQuery()



            PAR.cmd.Dispose()
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('operazione effettuata! " & i & "');</script>")

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
End Class
