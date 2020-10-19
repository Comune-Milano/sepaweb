
Partial Class AMMSEPA_RicercaOP
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=61"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtNumGiorni.Text = par.IfNull(myReader("VALORE"), "0")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=62"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtLunghezza.Text = par.IfNull(myReader("VALORE"), "0")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=63"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("VALORE"), "0") = "0" Then
                    ChNumLet.Checked = False
                Else
                    ChNumLet.Checked = True
                End If
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=64"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtTentativi.Text = par.IfNull(myReader("VALORE"), "0")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=65"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                txtAttivita.Text = par.IfNull(myReader("VALORE"), "0")
            End If
            myReader.Close()

            par.OracleConn.Close()
            'End If
        End If
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='" & txtNumGiorni.Text & "' WHERE ID=61"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='" & txtLunghezza.Text & "' WHERE ID=62"
            par.cmd.ExecuteNonQuery()


            If ChNumLet.Checked = True Then
                par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='1' WHERE ID=63"
            Else
                par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='0' WHERE ID=63"
            End If
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='" & txtTentativi.Text & "' WHERE ID=64"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE PARAMETER SET VALORE='" & txtAttivita.Text & "' WHERE ID=65"
            par.cmd.ExecuteNonQuery()

            Try
                Dim operatore As String = Session.Item("ID_OPERATORE")
                Eventi_Gestione(operatore, "F02", "AGGIORNAMENTO PARAMETRI PER CREAZIONE E MODIFICA PASSWORD")
            Catch ex As Exception
                lblErrore.Text = ex.Message
                lblErrore.Visible = True
            End Try

            par.OracleConn.Close()
            Response.Write("<script>alert('Operazione Effettuata!');</script>")

        Catch ex As Exception
            par.OracleConn.Close()
            Response.Write(ex.Message)
        End Try
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
