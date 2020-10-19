
Partial Class AMMSEPA_Controllo_SISCOM
    Inherits PageSetIdMode
    Dim OracleConn As Oracle.DataAccess.Client.OracleConnection
    Dim cmd As New Oracle.DataAccess.Client.OracleCommand()
    Dim PAR As New CM.Global

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                Button2.Visible = True
                TextBox2.Visible = True
            End If
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Try

            OracleConn = New Oracle.DataAccess.Client.OracleConnection(PAR.StringaSiscom)


            OracleConn.Open()
            cmd = OracleConn.CreateCommand()

            Dim sComando As String = TextBox2.Text

            sComando = Replace(sComando, "chr(10)", Chr(10))
            sComando = Replace(sComando, "chr(13)", Chr(13))

            cmd.CommandText = sComando
            cmd.ExecuteNonQuery()

            cmd.Dispose()
            OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('operazione effettuata!');</script>")

        Catch ex As Exception
            cmd.Dispose()
            OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
