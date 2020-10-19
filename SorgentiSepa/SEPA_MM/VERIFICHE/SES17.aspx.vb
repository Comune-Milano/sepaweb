
Partial Class AMMSEPA_Controllo_Aggiornamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Session.Item("OPERATORE") = "" Then
        '    Response.Write("<script>top.location.href=""../../AccessoNegato.htm""</script>")
        '    Exit Sub
        'End If
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                Carica()
            End If
        End If
    End Sub

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from aggiornamenti_fatti order by data_esecuzione desc"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                Label1.Text = Label1.Text & par.IfNull(myReader("nome"), " ") & " eseguito il " & par.FormattaData(par.IfNull(myReader("data_esecuzione"), "")) & "</br>"

            End While
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function
End Class
