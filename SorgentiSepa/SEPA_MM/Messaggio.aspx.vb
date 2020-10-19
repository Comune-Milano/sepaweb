
Partial Class Messaggio
    Inherits PageSetIdMode
    Dim sValore As String
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            sValore = Request.QueryString("ID")
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select WEB_NEWS_ENTI.MESSAGGIO_LUNGO from WEB_NEWS_ENTI where WEB_NEWS_ENTI.ID=" & sValore
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                TextBox1.Text = par.IfNull(myReader("MESSAGGIO_LUNGO"), "")
            End If
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
        End If
    End Sub
End Class
