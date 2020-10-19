
Partial Class AutoCompilazione_Valida
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim s As String
            s = par.DeCriptaMolto(Request.QueryString("ID"))
            If InStr(s, "SistemieSoluzionisrl-ValidazioneDomandaNumero") <> 0 Then
                s = Mid(s, 47, 10)
                par.OracleConn.Open()
                par.SettaCommand(par)
                If IsNumeric(s) = True Then
                    par.cmd.CommandText = "select * from domande_bando_web where id=" & CDbl(s)
                    Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader11.Read Then
                        If par.IfNull(myReader11("confermato"), "0") = "0" Then
                            par.cmd.CommandText = "update domande_bando_web set confermato='1',data_confermato='" & Format(Now, "yyyyMMdd") & "' where id=" & CDbl(s)
                            par.cmd.ExecuteNonQuery()
                            Label1.Text = "E'stata confermata la volonta di presentare domanda di bando E.R.P. per questa domanda.<br>Sarai contattato per concordare un appuntamento."
                        Else
                            Label1.Text = "ATTENZIONE...E'stata già confermata la volonta di presentare domanda di bando E.R.P."
                        End If

                    Else
                        Label1.Text = "ATTENZIONE...Impossibile validare. Il link non è corretto!"
                    End If
                    myReader11.Close()
                    par.OracleConn.Close()
                Else
                    Label1.Text = "ATTENZIONE...Impossibile validare. Il link non è corretto!"
                    par.OracleConn.Close()
                End If

            Else
                Label1.Text = "ATTENZIONE...Impossibile validare. Il link non è corretto!"

            End If

        Catch ex As Exception
            Response.Write(ex.Message)
            par.OracleConn.Close()
        End Try
    End Sub
End Class
