
Partial Class Contratti_ParametriDepCauz
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            If Session.Item("ID_OPERATORE") = "1" Then
                txtData.Enabled = True
            Else
                txtData.Enabled = False
            End If

            caricaDati()
        Else
            If RadioButton1.Checked = True Then
                txtRata.Attributes("style") = "visibility: hidden"
                'txtAnno.Attributes("style") = "visibility: hidden"
                'Label1.Attributes("style") = "visibility: hidden"
            Else
                txtRata.Attributes("style") = "visibility: visible"
                'txtAnno.Attributes("style") = "visibility: visible"
                'Label1.Attributes("style") = "visibility: visible"

            End If
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

            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE (ID=70 OR ID=72)", par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Me.txtData.Text = dt.Select("ID=70")(0).ItemArray(1)
                Me.txtData.Attributes.Add("ID", 70)

                Me.txtRata.Text = Trim(dt.Select("ID=72")(0).ItemArray(1))
                Me.txtRata.Attributes.Add("ID", 72)

                'Me.txtAnno.Text = Trim(dt.Select("ID=73")(0).ItemArray(1))
                'Me.txtAnno.Attributes.Add("ID", 73)
            End If

            par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID = 71"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Select Case par.IfNull(myReader("VALORE"), "0")
                    Case "1"
                        RadioButton1.Checked = True
                        txtRata.Attributes("style") = "visibility: hidden"
                        'txtAnno.Attributes("style") = "visibility: hidden"
                        'Label1.Attributes("style") = "visibility: hidden"
                    Case "2"
                        RadioButton2.Checked = True
                        txtRata.Attributes("style") = "visibility: visible"
                        'txtAnno.Attributes("style") = "visibility: visible"
                        'Label1.Attributes("style") = "visibility: visible"
                    Case Else
                        'RadioButton3.Checked = True
                End Select
            End If
            myReader.Close()

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
            Dim lOK As Boolean = True

            If Not IsDate(Me.txtData.Text & "/2001") Then
                Response.Write("<SCRIPT>alert('ATTENZIONE! Data (GG/MM) non valida!');</SCRIPT>")
                lOK = False
            End If

            If lOK And RadioButton2.Checked = True And (Not IsNumeric(Trim(txtRata.Text))) Then
                Response.Write("<SCRIPT>alert('ATTENZIONE! Rata non valida! Inserire un numero compreso tra 1 e 12.');</SCRIPT>")
                lOK = False
            End If

            If lOK And RadioButton2.Checked = True And (IsNumeric(Trim(txtRata.Text))) Then
                If CInt(par.IfEmpty(Trim(txtRata.Text), 0)) < 1 Or CInt(par.IfEmpty(Trim(txtRata.Text), 0)) > 12 Then
                    Response.Write("<SCRIPT>alert('ATTENZIONE! Rata non valida! Inserire un numero compreso tra 1 e 12.');</SCRIPT>")
                    lOK = False
                End If
            End If

            If lOK Then
                '////////////////////////////
                '// Tutto OK posso salvare 
                If par.IfEmpty(Me.txtData.Text, "Null") <> "Null" Then
                    For Each CTRL In Me.Form1.Controls
                        If TypeOf CTRL Is TextBox Then
                            If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                                STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & par.PulisciStrSql(par.IfEmpty(DirectCast(CTRL, TextBox).Text, " ")) & "' WHERE ID = " & DirectCast(CTRL, TextBox).Attributes("ID").ToUpper.ToString
                                par.cmd.CommandText = STRIUPDATE
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                        End If
                    Next

                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '2' WHERE ID = 71"

                    If RadioButton1.Checked = True Then
                        STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '1' WHERE ID = 71"
                    End If
                    If RadioButton2.Checked = True Then
                        STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '2' WHERE ID = 71"
                    End If

                    par.cmd.CommandText = STRIUPDATE
                    par.cmd.ExecuteNonQuery()

                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                End If
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
