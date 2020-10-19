
Partial Class AMMSEPA_Connessione2
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Label1.Text = "Eliminare SID " & par.DeCripta(Request.QueryString("SID")) & " SERIAL " & par.DeCripta(Request.QueryString("SER")) & " ?"
            SID.Text = par.DeCripta(Request.QueryString("SID"))
            SERIAL.Text = par.DeCripta(Request.QueryString("SER"))
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "ALTER SYSTEM KILL SESSION '" & SID.Text & "," & SERIAL.Text & "' IMMEDIATE"
            par.cmd.ExecuteNonQuery()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Operazione effettuata! Aggiornare elenco connessioni');self.close();</script>")
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
