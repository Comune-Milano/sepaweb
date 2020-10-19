
Partial Class Contratti_ParametriBolletta
    Inherits PageSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            tipo.Value = Request.QueryString("T")
            caricaDati()

        End If
        txtDataNascita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

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
            da = New Oracle.DataAccess.Client.OracleDataAdapter("SELECT ID, VALORE FROM PARAMETER", par.OracleConn)
            da.Fill(dt)

            If dt.Rows.Count > 0 Then

                Select Case tipo.Value
                    Case "0"
                        txtUfficio.Text = par.IfNull(dt.Select("ID=67")(0).ItemArray(1), "")
                        txtUfficio.Attributes.Add("ID", 67)

                        txtCognome.Text = par.IfNull(dt.Select("ID=68")(0).ItemArray(1), "")
                        txtCognome.Attributes.Add("ID", 68)

                        Me.txtNome.Text = par.IfNull(dt.Select("ID=69")(0).ItemArray(1), "")
                        Me.txtNome.Attributes.Add("ID", 69)

                        txtDataNascita.Text = par.FormattaData(par.IfNull(dt.Select("ID=70")(0).ItemArray(1), ""))
                        txtDataNascita.Attributes.Add("ID", 70)

                        txtComuneNascita.Text = par.IfNull(dt.Select("ID=71")(0).ItemArray(1), "")
                        txtComuneNascita.Attributes.Add("ID", 71)

                        txtProvinciaNascita.Text = par.IfNull((dt.Select("ID=72")(0).ItemArray(1)), "")
                        txtProvinciaNascita.Attributes.Add("ID", 72)

                        txtIndirizzo.Text = par.IfNull(dt.Select("ID=73")(0).ItemArray(1), "")
                        txtIndirizzo.Attributes.Add("ID", 73)

                        txtTelefono.Text = par.IfNull(dt.Select("ID=74")(0).ItemArray(1), "")
                        txtTelefono.Attributes.Add("ID", 74)

                        txtComuneResidenza.Text = par.IfNull(dt.Select("ID=75")(0).ItemArray(1), "")
                        txtComuneResidenza.Attributes.Add("ID", 75)

                        txtContratto.Text = par.IfNull(dt.Select("ID=76")(0).ItemArray(1), "")
                        txtContratto.Attributes.Add("ID", 76)

                    Case "1"
                        txtUfficio.Text = par.IfNull(dt.Select("ID=85")(0).ItemArray(1), "")
                        txtUfficio.Attributes.Add("ID", 85)

                        txtCognome.Text = par.IfNull(dt.Select("ID=86")(0).ItemArray(1), "")
                        txtCognome.Attributes.Add("ID", 86)

                        Me.txtNome.Text = par.IfNull(dt.Select("ID=87")(0).ItemArray(1), "")
                        Me.txtNome.Attributes.Add("ID", 87)

                        txtDataNascita.Text = par.FormattaData(par.IfNull(dt.Select("ID=88")(0).ItemArray(1), ""))
                        txtDataNascita.Attributes.Add("ID", 88)

                        txtComuneNascita.Text = par.IfNull(dt.Select("ID=89")(0).ItemArray(1), "")
                        txtComuneNascita.Attributes.Add("ID", 89)

                        txtProvinciaNascita.Text = par.IfNull((dt.Select("ID=90")(0).ItemArray(1)), "")
                        txtProvinciaNascita.Attributes.Add("ID", 90)

                        txtIndirizzo.Text = par.IfNull(dt.Select("ID=91")(0).ItemArray(1), "")
                        txtIndirizzo.Attributes.Add("ID", 91)

                        txtTelefono.Text = par.IfNull(dt.Select("ID=92")(0).ItemArray(1), "")
                        txtTelefono.Attributes.Add("ID", 92)

                        txtComuneResidenza.Text = par.IfNull(dt.Select("ID=93")(0).ItemArray(1), "")
                        txtComuneResidenza.Attributes.Add("ID", 93)

                        txtContratto.Text = par.IfNull(dt.Select("ID=94")(0).ItemArray(1), "")
                        txtContratto.Attributes.Add("ID", 94)
                    Case "2"
                        txtUfficio.Text = par.IfNull(dt.Select("ID=95")(0).ItemArray(1), "")
                        txtUfficio.Attributes.Add("ID", 95)

                        txtCognome.Text = par.IfNull(dt.Select("ID=96")(0).ItemArray(1), "")
                        txtCognome.Attributes.Add("ID", 96)

                        Me.txtNome.Text = par.IfNull(dt.Select("ID=97")(0).ItemArray(1), "")
                        Me.txtNome.Attributes.Add("ID", 97)

                        txtDataNascita.Text = par.FormattaData(par.IfNull(dt.Select("ID=98")(0).ItemArray(1), ""))
                        txtDataNascita.Attributes.Add("ID", 98)

                        txtComuneNascita.Text = par.IfNull(dt.Select("ID=99")(0).ItemArray(1), "")
                        txtComuneNascita.Attributes.Add("ID", 99)

                        txtProvinciaNascita.Text = par.IfNull((dt.Select("ID=100")(0).ItemArray(1)), "")
                        txtProvinciaNascita.Attributes.Add("ID", 100)

                        txtIndirizzo.Text = par.IfNull(dt.Select("ID=101")(0).ItemArray(1), "")
                        txtIndirizzo.Attributes.Add("ID", 101)

                        txtTelefono.Text = par.IfNull(dt.Select("ID=102")(0).ItemArray(1), "")
                        txtTelefono.Attributes.Add("ID", 102)

                        txtComuneResidenza.Text = par.IfNull(dt.Select("ID=103")(0).ItemArray(1), "")
                        txtComuneResidenza.Attributes.Add("ID", 103)

                        txtContratto.Text = par.IfNull(dt.Select("ID=104")(0).ItemArray(1), "")
                        txtContratto.Attributes.Add("ID", 104)

                    Case "3"
                        txtUfficio.Text = par.IfNull(dt.Select("ID=105")(0).ItemArray(1), "")
                        txtUfficio.Attributes.Add("ID", 105)

                        txtCognome.Text = par.IfNull(dt.Select("ID=106")(0).ItemArray(1), "")
                        txtCognome.Attributes.Add("ID", 106)

                        Me.txtNome.Text = par.IfNull(dt.Select("ID=107")(0).ItemArray(1), "")
                        Me.txtNome.Attributes.Add("ID", 107)

                        txtDataNascita.Text = par.FormattaData(par.IfNull(dt.Select("ID=108")(0).ItemArray(1), ""))
                        txtDataNascita.Attributes.Add("ID", 108)

                        txtComuneNascita.Text = par.IfNull(dt.Select("ID=109")(0).ItemArray(1), "")
                        txtComuneNascita.Attributes.Add("ID", 109)

                        txtProvinciaNascita.Text = par.IfNull((dt.Select("ID=110")(0).ItemArray(1)), "")
                        txtProvinciaNascita.Attributes.Add("ID", 110)

                        txtIndirizzo.Text = par.IfNull(dt.Select("ID=111")(0).ItemArray(1), "")
                        txtIndirizzo.Attributes.Add("ID", 111)

                        txtTelefono.Text = par.IfNull(dt.Select("ID=112")(0).ItemArray(1), "")
                        txtTelefono.Attributes.Add("ID", 112)

                        txtComuneResidenza.Text = par.IfNull(dt.Select("ID=113")(0).ItemArray(1), "")
                        txtComuneResidenza.Attributes.Add("ID", 113)

                        txtContratto.Text = par.IfNull(dt.Select("ID=114")(0).ItemArray(1), "")
                        txtContratto.Attributes.Add("ID", 114)
                End Select


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
