
Partial Class Errore
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Session.Item("ERRORE")
        Try


            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "insert into SISCOM_MI.LOG_ERRORI (DATA_ORA,DESCRIZIONE,OPERATORE) VALUES ('" & Format(Now, "yyyyMMddHHmmss") & "','" & par.PulisciStrSql(Label1.Text) & "'," & Session.Item("ID_OPERATORE") & ")"
            par.cmd.ExecuteNonQuery()

            'par.cmd.CommandText = "select * from parameter where id=66"
            'Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderJ.Read Then
            '    'HyperLink2.NavigateUrl = "mailto:" & par.IfNull(myReaderJ("valore"), "") & "?subject=Errore&body=Data e Ora: " & Now & " Descrizione Errore: " & Label1.Text
            '    HyperLink2.NavigateUrl = "mailto:support@sistemiesoluzionisrl.it?subject=Errore SEPAWEB&body=Data e Ora: " & Now & " Descrizione Errore: " & Label1.Text
            '    'ALESSANDRO.GOBBI@comune.milano.it
            'End If
            'myReaderJ.Close()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub
End Class
