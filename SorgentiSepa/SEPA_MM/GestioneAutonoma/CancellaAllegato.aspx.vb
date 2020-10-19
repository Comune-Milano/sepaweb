
Partial Class Contratti_CancellaAllegato
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then



            Try
                'par.OracleConn.Open()
                'par.SettaCommand(par)

                My.Computer.FileSystem.DeleteFile(Server.MapPath("../ALLEGATI/AUTOGESTIONI/") & Request.QueryString("NOME"))
                Response.Write("<script>opener.document.getElementById('form1').submit();</script>")
                Response.Write("Il File " & RicavaFile(Request.QueryString("NOME")) & " è stato cancellato correttamente<br /><br /><a href='javascript:window.close();'>CHIUDI FINESTRA</a>")







                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try

        End If

    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

End Class
