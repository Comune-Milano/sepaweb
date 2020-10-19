
Partial Class EstremiDocumento
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim lIdDomanda As String = ""
    Dim NOME As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Try
                lIdDomanda = Request.QueryString("ID")
                Label1.Text = par.DeCripta(Request.QueryString("I"))

                par.OracleConn.Open()
                par.SettaCommand(par)

                Select Case Request.QueryString("T")
                    Case "1"
                        par.cmd.CommandText = "select * from domande_bando where id=" & lIdDomanda
                    Case "2"
                        par.cmd.CommandText = "select * from domande_bando_CAMBI where id=" & lIdDomanda
                    Case "3"
                        par.cmd.CommandText = "select * from domande_bando_VSA where id=" & lIdDomanda
                End Select


                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    lblCartaNumero.Text = par.IfNull(myReader("carta_i"), "")
                    lblCartaDel.Text = par.FormattaData(par.IfNull(myReader("carta_i_data"), ""))
                    lblCartaRilasciata.Text = par.IfNull(myReader("carta_i_rilasciata"), "")

                    Select Case Request.QueryString("T")
                        Case "1"
                            If par.IfNull(myReader("fl_nato_estero"), "0") <> "0" Then
                                lblNatoEstero.Visible = True
                            End If

                            If par.IfNull(myReader("fl_nato_estero"), "0") <> "0" Then
                                lblNatoEstero.Visible = True
                            End If
                    End Select


                    lblPermessoNumero.Text = par.IfNull(myReader("permesso_sogg_n"), "")
                    lblPermessoDel.Text = par.FormattaData(par.IfNull(myReader("permesso_sogg_data"), ""))
                    lblPermessoScade.Text = par.FormattaData(par.IfNull(myReader("permesso_sogg_scade"), ""))
                    lblPermessoRinnovo.Text = par.FormattaData(par.IfNull(myReader("permesso_sogg_data_rinnovo"), ""))
                    If par.IfNull(myReader("permesso_sogg_cont"), "0") <> "0" Then
                        lblContinuativo.Visible = True
                    End If
                    lblSoggiornoNumero.Text = par.IfNull(myReader("carta_sogg_n"), "")
                    lblSoggiornoDel.Text = par.FormattaData(par.IfNull(myReader("carta_sogg_DATA"), ""))
                    Select Case par.IfNull(myReader("tipo_lavoro"), "0")
                        Case "1"
                            lblSoggiornoLavoro.Text = "AUTONOMO"
                        Case "2"
                            lblSoggiornoLavoro.Text = "DIPENDENTE"
                        Case Else
                            lblSoggiornoLavoro.Text = ""
                    End Select

                End If
                myReader.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write(ex.Message)
            End Try

        End If
    End Sub
End Class
