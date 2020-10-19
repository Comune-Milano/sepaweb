
Partial Class ANAUT_VisualizzaContratto
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim IndirizzoCollegamento As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "SELECT * FROM PARAMETER where ID=128"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    IndirizzoCollegamento = par.IfNull(myReader("VALORE"), "")
                End If
                myReader.Close()
                If IndirizzoCollegamento = "" Then
                    par.OracleConn.Close()
                    par.cmd.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Response.Write("<p style='font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold'>Non è possibile effettuare il collegamento</p>")
                Else
                    par.OracleConn.Close()
                    par.cmd.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Dim ImpostaChiave As New Fornitori
                    Dim Chiave As String = par.getPageId & "_" & Format(Now, "yyyyMMddHHmmss")
                    Dim esito As String = ImpostaChiave.ImpostaChiave(Chiave)
                    If esito = "1" Then
                        Dim COlight As String = Session.Item("COLIGHT")
                        Response.Redirect(par.DeCripta(IndirizzoCollegamento) & "ContrattoLight.aspx?ID=" & par.CriptaMolto(Format(Now, "yyyyMMddHH") & "#" & COlight & "@" & Chiave), True)
                    Else
                        Response.Write("<p style='font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold'>Non è possibile effettuare il collegamento</p>")
                    End If
                End If
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<p style='font-family: arial, Helvetica, sans-serif; font-size: 10pt; font-weight: bold'>Non è possibile effettuare il collegamento</p><br />" & ex.Message)
            End Try
        End If
    End Sub
End Class
