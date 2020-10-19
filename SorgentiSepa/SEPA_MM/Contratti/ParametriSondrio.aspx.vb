
Partial Class Contratti_ParametriSondrio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            caricaDati()

        End If
    End Sub

    Private Sub caricaDati()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable

            par.cmd.CommandText = "SELECT ID, VALORE FROM PARAMETER"
            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, VALORE FROM PARAMETER WHERE ID >=78 AND ID <= 84", par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                Me.txtIndirizzo.Text = dt.Select("ID=78")(0).ItemArray(1)
                Me.txtIndirizzo.Attributes.Add("ID", 78)

                Me.txtServizio.Text = dt.Select("ID=80")(0).ItemArray(1)
                Me.txtServizio.Attributes.Add("ID", 80)

                Me.TxtSIA.Text = dt.Select("ID=79")(0).ItemArray(1)
                Me.TxtSIA.Attributes.Add("ID", 79)

                Me.TxtSottoServizio.Text = dt.Select("ID=81")(0).ItemArray(1)
                Me.TxtSottoServizio.Attributes.Add("ID", 81)

                Me.txtLista.Text = dt.Select("ID=82")(0).ItemArray(1)
                Me.txtLista.Attributes.Add("ID", 82)

                Me.txturl.Text = dt.Select("ID=84")(0).ItemArray(1)
                Me.txturl.Attributes.Add("ID", 84)

            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim CTRL As Control = Nothing
            Dim STRIUPDATE As String
            If par.IfEmpty(Me.txtIndirizzo.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtSIA.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtServizio.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtSottoServizio.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtLista.Text, "Null") <> "Null" Then


                For Each CTRL In Me.Form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                            STRIUPDATE = "UPDATE PARAMETER SET VALORE = '" & par.PulisciStrSql(DirectCast(CTRL, TextBox).Text) & "' WHERE ID = " & DirectCast(CTRL, TextBox).Attributes("ID").ToUpper.ToString
                            par.cmd.CommandText = STRIUPDATE
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                        End If
                    End If

                Next

                Response.Write("<SCRIPT>alert('Operazione eseguita correttamente!');</SCRIPT>")
            Else
                Response.Write("<SCRIPT>alert('ATTENZIONE! Tutti i dati sono obbligatori!');</SCRIPT>")

            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
