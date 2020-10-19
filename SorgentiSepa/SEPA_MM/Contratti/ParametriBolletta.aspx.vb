
Partial Class Contratti_ParametriBolletta
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
            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA", par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then
                Me.TxtBollo.Text = dt.Select("ID=0")(0).ItemArray(1)
                Me.TxtBollo.Attributes.Add("ID", 0)

                Me.TxtRecesCont.Text = dt.Select("ID=1")(0).ItemArray(1)
                Me.TxtRecesCont.Attributes.Add("ID", 1)

                Me.txtNonStamp.Text = dt.Select("ID=2")(0).ItemArray(1)
                Me.txtNonStamp.Attributes.Add("ID", 2)

                Me.txtScadDataEmiss.Text = dt.Select("ID=3")(0).ItemArray(1)
                Me.txtScadDataEmiss.Attributes.Add("ID", 3)

                Me.txtRegAnnualita.Text = dt.Select("ID=6")(0).ItemArray(1)
                Me.txtRegAnnualita.Attributes.Add("ID", 6)

                Me.txtScadErp.Text = dt.Select("ID=7")(0).ItemArray(1)
                Me.txtScadErp.Attributes.Add("ID", 7)

                Me.TxtMesiAffAntici.Text = dt.Select("ID=16")(0).ItemArray(1)
                Me.TxtMesiAffAntici.Attributes.Add("ID", 16)

                Me.txtSpPost.Text = dt.Select("ID=17")(0).ItemArray(1)
                Me.txtSpPost.Attributes.Add("ID", 17)

                Me.txtBolloContratto.Text = dt.Select("ID=18")(0).ItemArray(1)
                Me.txtBolloContratto.Attributes.Add("ID", 18)

                Me.TxtBollo0.Text = dt.Select("ID=25")(0).ItemArray(1)
                Me.TxtBollo0.Attributes.Add("ID", 25)

                Me.txtPerERP.Text = dt.Select("ID=22")(0).ItemArray(1)
                Me.txtPerERP.Attributes.Add("ID", 22)

                Me.txtPerNOERP.Text = dt.Select("ID=23")(0).ItemArray(1)
                Me.txtPerNOERP.Attributes.Add("ID", 23)

                Me.txtPerUSD.Text = dt.Select("ID=24")(0).ItemArray(1)
                Me.txtPerUSD.Attributes.Add("ID", 24)

                Me.txtSpMAV.Text = dt.Select("ID=26")(0).ItemArray(1)
                Me.txtSpMAV.Attributes.Add("ID", 26)

                Me.txtSpPost0.Text = dt.Select("ID=29")(0).ItemArray(1)
                Me.txtSpPost0.Attributes.Add("ID", 29)

                Me.txtSpPost1.Text = dt.Select("ID=30")(0).ItemArray(1)
                Me.txtSpPost1.Attributes.Add("ID", 30)

                Me.TxtNumCopie.Text = dt.Select("ID=45")(0).ItemArray(1)
                Me.TxtNumCopie.Attributes.Add("ID", 45)
            End If

            par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA where id=27"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Select Case par.IfNull(myReader("VALORE"), "0")
                    Case "0"
                        RadioButton1.Checked = True
                    Case "1"
                        RadioButton2.Checked = True
                    Case "2"
                        RadioButton3.Checked = True
                End Select
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA where id=28"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                Select Case par.IfNull(myReader("VALORE"), "0")
                    Case "0"
                        RadioButton4.Checked = True
                    Case "1"
                        RadioButton5.Checked = True
                    Case "2"
                        RadioButton6.Checked = True
                End Select
            End If
            myReader.Close()

            par.cmd.Dispose()
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
            If par.IfEmpty(Me.txtBolloContratto.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtBollo.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtMesiAffAntici.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtNonStamp.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.TxtRecesCont.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtRegAnnualita.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtScadDataEmiss.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtScadErp.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtSpPost.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtPerERP.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtPerNOERP.Text, "Null") <> "Null" AndAlso par.IfEmpty(Me.txtPerUSD.Text, "Null") <> "Null" Then

                For Each CTRL In Me.Form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                            STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & par.VirgoleInPunti(DirectCast(CTRL, TextBox).Text) & "' WHERE ID = " & DirectCast(CTRL, TextBox).Attributes("ID").ToUpper.ToString
                            par.cmd.CommandText = STRIUPDATE
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = ""
                        End If
                    End If
                Next

                STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '0' WHERE ID = 27"

                If RadioButton1.Checked = True Then
                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '0' WHERE ID = 27"
                End If
                If RadioButton2.Checked = True Then
                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '1' WHERE ID = 27"
                End If
                If RadioButton3.Checked = True Then
                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '2' WHERE ID = 27"
                End If


                par.cmd.CommandText = STRIUPDATE
                par.cmd.ExecuteNonQuery()


                STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '0' WHERE ID = 28"

                If RadioButton4.Checked = True Then
                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '0' WHERE ID = 28"
                End If
                If RadioButton5.Checked = True Then
                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '1' WHERE ID = 28"
                End If
                If RadioButton6.Checked = True Then
                    STRIUPDATE = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '2' WHERE ID = 28"
                End If


                par.cmd.CommandText = STRIUPDATE
                par.cmd.ExecuteNonQuery()


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
