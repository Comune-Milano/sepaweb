
Partial Class InfoUtente
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Chiamante = Session.Item("id_operatore")
            lblmessaggio.Visible = False

            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim lsiFrutto As New ListItem("--", "0")
                cmbUfficio.Items.Add(lsiFrutto)
                par.cmd.CommandText = "SELECT TAB_FILIALI.ID,TAB_FILIALI.NOME,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP,COMUNI_NAZIONI.NOME AS ""COMUNE"" FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI,COMUNI_NAZIONI WHERE COMUNI_NAZIONI.COD=INDIRIZZI.COD_COMUNE AND INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO ORDER BY TAB_FILIALI.NOME asc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader.Read
                    lsiFrutto = New ListItem(par.IfNull(myReader("NOME"), "") & "-" & par.IfNull(myReader("descrizione"), "") & " " & par.IfNull(myReader("civico"), ""), myReader("ID"))
                    cmbUfficio.Items.Add(lsiFrutto)
                End While
                myReader.CLOSE()

                par.cmd.CommandText = "SELECT * FROM operatori WHERE ID=" & Chiamante
                myReader = par.cmd.ExecuteReader()

                If myReader.Read Then
                    lblOperatore.Text = par.IfNull(myReader("operatore"), "")
                    txtCognome.Text = par.IfNull(myReader("cognome"), "")
                    txtNome.Text = par.IfNull(myReader("nome"), "")
                    txtCF.Text = par.IfNull(myReader("cod_fiscale"), "")
                    txtCA.Text = par.IfNull(myReader("cod_ana"), "")
                    cmbTipoOp.SelectedIndex = -1
                    cmbTipoOp.Items.FindByValue(par.IfNull(myReader("tipo_operatore"), 0)).Selected = True

                    cmbUfficio.SelectedIndex = -1
                    If par.IfNull(myReader("ID_UFFICIO"), 0) <> "-1" Then
                        cmbUfficio.Items.FindByValue(par.IfNull(myReader("ID_UFFICIO"), 0)).Selected = True
                    End If
                    If cmbUfficio.SelectedValue <> 0 Then
                        Dim fl_cambio_struttura As Integer = par.IfNull(myReader("FL_CAMBIO_STRUTTURA"), 0)
                        If fl_cambio_struttura = 0 Then
                            cmbUfficio.Enabled = False
                        End If
                    End If
                    idCaf.Value = par.IfNull(myReader("ID_CAF"), "")
                    If idCaf.Value <> "2" Then
                        cmbUfficio.Enabled = False
                        cmbTipoOp.Enabled = False
                    End If
                End If
                myReader.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblmessaggio.Visible = True
                lblmessaggio.Text = ex.Message
            End Try
        End If
    End Sub

    Public Property Chiamante() As String
        Get
            If Not (ViewState("par_Chiamante") Is Nothing) Then
                Return CStr(ViewState("par_Chiamante"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Chiamante") = value
        End Set

    End Property

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If txtCognome.Text <> "" And txtNome.Text <> "" Then
            If par.ControllaCF(UCase(txtCF.Text)) = True Then
                If par.ControllaCFNomeCognome(UCase(txtCF.Text), UCase(txtCognome.Text), UCase(txtNome.Text)) = True Then
                    If cmbTipoOp.SelectedValue <> 0 And cmbUfficio.SelectedValue <> 0 Then
                        Try
                            par.OracleConn.Open()
                            par.SettaCommand(par)

                            If idCaf.Value <> "2" Then
                                par.cmd.CommandText = "update operatori set " _
                                    & "DATA_INFO_UTENTE='" & Format(Now, "yyyyMMdd") & "'," _
                                    & "cognome='" & par.PulisciStrSql(UCase(txtCognome.Text)) & "'," _
                                    & "nome='" & par.PulisciStrSql(UCase(txtNome.Text)) & "'," _
                                    & "cod_fiscale='" & par.PulisciStrSql(UCase(txtCF.Text)) & "'," _
                                    & "cod_ana='" & par.PulisciStrSql(UCase(txtCA.Text)) & "' " _
                                    & " where id=" & Chiamante
                            Else
                                par.cmd.CommandText = "update operatori set " _
                                    & "DATA_INFO_UTENTE='" & Format(Now, "yyyyMMdd") & "'," _
                                    & "cognome='" & par.PulisciStrSql(UCase(txtCognome.Text)) & "'," _
                                    & "nome='" & par.PulisciStrSql(UCase(txtNome.Text)) & "'," _
                                    & "cod_fiscale='" & par.PulisciStrSql(UCase(txtCF.Text)) & "'," _
                                    & "cod_ana='" & par.PulisciStrSql(UCase(txtCA.Text)) & "'," _
                                    & "tipo_operatore=" & cmbTipoOp.SelectedItem.Value & "," _
                                    & "id_ufficio=" & cmbUfficio.SelectedItem.Value _
                                    & " where id=" & Chiamante
                            End If
                            
                            par.cmd.ExecuteNonQuery()

                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            Response.Redirect("AreaPrivata.aspx")

                        Catch ex As Exception
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            lblmessaggio.Visible = True
                            lblmessaggio.Text = ex.Message
                        End Try
                    Else
                        lblmessaggio.Text = "Specificare la tipologia di operatore e l\'ufficio di appartenenza"
                        lblmessaggio.Visible = True
                    End If

                Else
                    lblmessaggio.Text = "Codice fiscale errato!"
                    lblmessaggio.Visible = True
                End If
            Else
                lblmessaggio.Text = "Codice fiscale assente o errato!"
                lblmessaggio.Visible = True
            End If
        Else
            lblmessaggio.Text = "Cognome e/o Nome assente"
            lblmessaggio.Visible = True
        End If
    End Sub
End Class
