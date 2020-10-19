
Partial Class AMMSEPA_Connessioni1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Carica(par.DeCripta(Request.QueryString("ID")))

    End Sub

    Function Carica(ByVal id As String)
        Try
            Dim i As Integer = 0
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT   sql_text  FROM v$sqltext_with_newlines  WHERE hash_value = " & id & " ORDER BY piece"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Do While myReader.Read
                Label1.Text = Label1.Text & par.IfNull(myReader("sql_text"), "0") & "</br>"
            Loop
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function
End Class
