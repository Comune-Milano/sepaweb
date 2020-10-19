
Partial Class ANAUT_TempisticaDiffide
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            cARICA()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Private Function Carica()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT VALORE FROM parameter WHERE ID=118"
            Dim myRec55 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            TextBox1.Text = "0"
            If myRec55.Read Then
                TextBox1.Text = par.IfNull(myRec55(0), "0")
            End If
            myRec55.Close()

            par.cmd.CommandText = "SELECT VALORE FROM parameter WHERE ID=119"
            myRec55 = par.cmd.ExecuteReader()
            TextBox2.Text = "0"
            If myRec55.Read Then
                TextBox2.Text = par.IfNull(myRec55(0), "0")
            End If
            myRec55.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:TempisticheDiffide - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='" & par.PulisciStrSql(TextBox1.Text) & "' WHERE ID=118"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='" & par.PulisciStrSql(TextBox2.Text) & "' WHERE ID=119"
            par.cmd.ExecuteNonQuery()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Operazione effettuata!');</script>")

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:TempisticheDiffide - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
        
    End Sub
End Class
