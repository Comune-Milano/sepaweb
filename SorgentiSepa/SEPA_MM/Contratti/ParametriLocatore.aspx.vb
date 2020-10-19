
Partial Class Contratti_ParametriStampaContr
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

            par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA"
            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID >= 8 AND ID < 43", par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                Me.txtLocatore.Text = dt.Select("ID=8")(0).ItemArray(1)
                Me.txtLocatore.Attributes.Add("ID", 8)

                Me.txtDirettore.Text = dt.Select("ID=9")(0).ItemArray(1)
                Me.txtDirettore.Attributes.Add("ID", 9)

                Me.TxtCodFisc.Text = dt.Select("ID=10")(0).ItemArray(1)
                Me.TxtCodFisc.Attributes.Add("ID", 10)

                Me.TxtComune.Text = dt.Select("ID=11")(0).ItemArray(1)
                Me.TxtComune.Attributes.Add("ID", 11)

                Me.txtCivico.Text = dt.Select("ID=21")(0).ItemArray(1)
                Me.txtCivico.Attributes.Add("ID", 21)

                Me.txtIndirizzo.Text = dt.Select("ID=20")(0).ItemArray(1)
                Me.txtIndirizzo.Attributes.Add("ID", 20)

                Me.txtProvincia.Text = dt.Select("ID=19")(0).ItemArray(1)
                Me.txtProvincia.Attributes.Add("ID", 19)

                'Me.txtDirigente.Text = dt.Select("ID=40")(0).ItemArray(1)
                'Me.txtDirigente.Attributes.Add("ID", 40)

                'Me.txtluogodirigente.Text = dt.Select("ID=41")(0).ItemArray(1)
                'Me.txtluogodirigente.Attributes.Add("ID", 41)

                'Me.txtDataDirigente.Text = dt.Select("ID=42")(0).ItemArray(1)
                'Me.txtDataDirigente.Attributes.Add("ID", 42)

            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
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
            If par.IfEmpty(Me.txtIndirizzo.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtProvincia.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtCivico.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtCodFisc.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtDirettore.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtLocatore.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtComune.Text, "Null") <> "Null" Then


                For Each CTRL In Me.Form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                            STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & par.PulisciStrSql(DirectCast(CTRL, TextBox).Text) & "' WHERE ID = " & DirectCast(CTRL, TextBox).Attributes("ID").ToUpper.ToString
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
