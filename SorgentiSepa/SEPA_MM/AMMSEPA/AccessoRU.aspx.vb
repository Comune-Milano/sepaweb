
Partial Class AMMSEPA_AccessoRU
    Inherits PageSetIdMode
    Dim PAR As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            PAR.OracleConn.Open()
            PAR.SettaCommand(PAR)
            PAR.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=123"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                If PAR.IfNull(myReader("VALORE"), "1") = "1" Then
                    Label3.Text = "ATTUALEMENTE IN MODIFICA"
                Else
                    Label3.Text = "ATTUALEMENTE IN SOLA LETTURA"
                End If
            End If
            myReader.Close()
            PAR.OracleConn.Close()
        End If
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

    Protected Sub ImgSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgSalva.Click
        Try
            lblErrore.Visible = False
            PAR.OracleConn.Open()
            PAR.SettaCommand(PAR)
            PAR.cmd.CommandText = "UPDATE SEPA.PARAMETER SET VALORE='0' WHERE ID=123"
            PAR.cmd.ExecuteNonQuery()

            Try
                Dim operatore As String = Session.Item("ID_OPERATORE")
                Eventi_Gestione(operatore, "F02", "RU IN SOLA LETTURA")
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

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try
            lblErrore.Visible = False
            PAR.OracleConn.Open()
            PAR.SettaCommand(PAR)
            PAR.cmd.CommandText = "UPDATE SEPA.PARAMETER SET VALORE='1' WHERE ID=123"
            PAR.cmd.ExecuteNonQuery()

            Try
                Dim operatore As String = Session.Item("ID_OPERATORE")
                Eventi_Gestione(operatore, "F02", "RU IN MODIFICA")
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
End Class
