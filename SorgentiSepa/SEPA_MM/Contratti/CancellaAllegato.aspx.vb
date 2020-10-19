
Partial Class Contratti_CancellaAllegato
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            NomeF.Value = Request.QueryString("NOME")
            NomeD.Value = Request.QueryString("D")
            NomeX.Value = Request.QueryString("X")
            lblTitolo.Text = "Vuoi cancellare il file con descrizione - " & par.DeCripta(Request.QueryString("D")) & "? Si ricorda che non sarà possibile annullare questa operazione."
            

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

    Protected Sub imgConferma_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgConferma.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
              & "VALUES (" & par.DeCripta(NomeX.Value) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
              & "'F198','CANCELLATO ALLEGATO DESCRIZIONE:" & par.PulisciStrSql(par.DeCripta(NomeD.Value)) & " - FILE:" & par.PulisciStrSql(par.DeCripta(NomeF.Value)) & "')"
            par.cmd.ExecuteNonQuery()

            My.Computer.FileSystem.DeleteFile(Server.MapPath("../ALLEGATI/CONTRATTI/") & par.DeCripta(NomeF.Value))
            Response.Write("<script>opener.document.getElementById('form1').submit();</script>")
            Response.Write("<script>alert('Operazione effettuata! Il file è stato cancellato correttamente.');window.close();</script>")

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub
End Class
