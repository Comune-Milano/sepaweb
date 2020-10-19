
Partial Class AMMSEPA_AnnullaRevoca
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub ImgSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try
            lblErrore.Visible = False
            PAR.OracleConn.Open()
            par.SettaCommand(par)
            PAR.cmd.CommandText = "UPDATE SEPA.OPERATORI SET APPOGGIO='',REVOCA='0',MOTIVO_REVOCA='' WHERE LIVELLO_WEB<>1 AND REVOCA='1' AND FL_ELIMINATO='0' and APPOGGIO='3'"
            PAR.cmd.ExecuteNonQuery()

            Try
                Dim operatore As String = Session.Item("ID_OPERATORE")
                Eventi_Gestione(operatore, "F02", "RIPRISTINO OPERATORI REVOCATI CON LA FUNZIONE REVOCA TUTTI")
            Catch ex As Exception
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try

            PAR.OracleConn.Close()
            Response.Write("<script>alert('Operazione Offettuata!');location.href='pagina_home.aspx';</script>")

        Catch ex As Exception
            PAR.OracleConn.Close()
            lblErrore.Text = ex.Message
            lblErrore.Visible = True
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>location.href='pagina_home.aspx';</script>")
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
