
Partial Class AMMSEPA_ParametriErrori
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select * from parameter where id=66"
                Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderJ.Read Then
                    txtmail.Text = par.IfNull(myReaderJ("valore"), "")
                End If
                myReaderJ.Close()
                par.OracleConn.Close()
            End If

        Catch ex As Exception
            par.OracleConn.Close()
        End Try

    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "update parameter set valore='" & par.PulisciStrSql(txtmail.Text) & "' where id=66"
            par.cmd.ExecuteNonQuery()

            Try
                Dim operatore As String = Session.Item("ID_OPERATORE")
                Eventi_Gestione(operatore, "F02", "MODIFICA INVIO EMAIL PER SEGNALAZIONE ERRORI A: " & txtmail.Text)
            Catch ex As Exception
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try

            par.OracleConn.Close()

            Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Function Eventi_Gestione(ByVal operatore As String, ByVal cod_evento As String, ByVal motivazione As String) As Integer
        Dim data As String = Format(Now, "yyyyMMddHHmmss")
        Try
            PAR.cmd.CommandText = "INSERT INTO EVENTI_GESTIONE (ID_OPERATORE, COD_EVENTO, DATA_ORA, MOTIVAZIONE) VALUES" _
                            & " (" & operatore & ",'" & cod_evento & "'," & data & ",'" & motivazione & "')"
            PAR.cmd.ExecuteNonQuery()
        Catch ex As Exception
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
        Return 0
    End Function
End Class
