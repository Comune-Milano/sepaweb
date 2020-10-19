
Partial Class Contratti_ParametriRichiedente
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        cmbTipoRich.Attributes.Add("onchange", "javascript:disabilitaTextBox();")

        If Not IsPostBack Then
            CaricaDati2()
        End If
    End Sub

    Private Sub CaricaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID >= 40 or ID=8 OR ID=10"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            If dt.Rows.Count > 0 Then
                Me.txtCognome.Text = dt.Select("ID=40")(0).ItemArray(1)
                Me.txtCognome.Attributes.Add("ID", 40)

                Me.txtCodFiscale.Text = dt.Select("ID=41")(0).ItemArray(1)
                Me.txtCodFiscale.Attributes.Add("ID", 41)

                Me.cmbTipoRich.SelectedValue = dt.Select("ID=42")(0).ItemArray(1)
                Me.cmbTipoRich.Attributes.Add("ID", 42)

                '<input id="rdbTipoRichiedente1" type="radio" value="LOCATORE" title="Locatore" />Locatore
                '<input id="rdbTipoRichiedente2" type="radio" value="CONDUTTORE" title="Locatore" />Conduttore

                If cmbTipoRich.SelectedValue = "2" Then
                    HF_rdbTipoRich.Value = "MEDIATORE"
                End If

                If dt.Select("ID=43")(0).ItemArray(1) = "LOCATORE" Then
                    HF_rdbTipoRich.Value = "LOCATORE"
                    lblTipoRich.Text = "<input id='rdbTipoRichiedente1' type='radio' name='tipoRich' checked='checked' value='LOCATORE' title='Locatore' onclick='disabilitaTextBox();' />Locatore<input id='rdbTipoRichiedente2' name='tipoRich' type='radio' value='CONDUTTORE' title='Conduttore' onclick='disabilitaTextBox();' />Conduttore"
                Else
                    HF_rdbTipoRich.Value = "CONDUTTORE"
                    lblTipoRich.Text = "<input id='rdbTipoRichiedente1' type='radio' name='tipoRich' value='LOCATORE' title='Locatore' onclick='disabilitaTextBox();' />Locatore<input id='rdbTipoRichiedente2' name='tipoRich' type='radio' checked='checked' value='CONDUTTORE' title='Conduttore' onclick='disabilitaTextBox();' />Conduttore"
                End If
                nomeMediatore.Value = dt.Select("ID=40")(0).ItemArray(1)
                codFiscMediatore.Value = dt.Select("ID=41")(0).ItemArray(1)

                nomeLocatore.Value = dt.Select("ID=8")(0).ItemArray(1)
                codFiscLocatore.Value = dt.Select("ID=10")(0).ItemArray(1)

            End If

            
            If cmbTipoRich.SelectedValue = "1" And HF_rdbTipoRich.Value = "LOCATORE" Then
                par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=8 OR ID=10"
                Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt2 As New Data.DataTable
                da2.Fill(dt2)
                da2.Dispose()
                If dt2.Rows.Count > 0 Then
                    Me.txtCognome.Text = dt2.Select("ID=8")(0).ItemArray(1)
                    Me.txtCognome.Attributes.Add("ID", 8)

                    Me.txtCodFiscale.Text = dt2.Select("ID=10")(0).ItemArray(1)
                    Me.txtCodFiscale.Attributes.Add("ID", 10)
                    'nomeLocatore.Value = txtCognome.Text
                    'codFiscLocatore.Value = txtCodFiscale.Text
                End If
            End If

            'If cmbTipoRich.SelectedValue = "1" And rdbTipoRichiedente.SelectedValue = "LOCATORE" Then
            '    par.cmd.CommandText = "SELECT ID, VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=8 OR ID=10"
            '    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            '    Dim dt2 As New Data.DataTable
            '    da2.Fill(dt2)
            '    da2.Dispose()
            '    If dt2.Rows.Count > 0 Then
            '        Me.txtCognome.Text = dt2.Select("ID=8")(0).ItemArray(1)
            '        Me.txtCognome.Attributes.Add("ID", 8)

            '        Me.txtCodFiscale.Text = dt2.Select("ID=10")(0).ItemArray(1)
            '        Me.txtCodFiscale.Attributes.Add("ID", 10)
            '    End If
            'ElseIf cmbTipoRich.SelectedValue = "1" And rdbTipoRichiedente.SelectedValue = "CONDUTTORE" Then
            '    Me.txtCognome.Text = ""

            '    Me.txtCodFiscale.Text = ""
            'End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " CaricaDati() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaDati2()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT * from SISCOM_MI.PARAMETRI_RICHIEDENTE_XML"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                For Each rowP As Data.DataRow In dt.Rows
                    Select Case rowP.Item("ID_TIPO_RICH")
                        Case 0
                            lblTipoRich.Text = "<input id='rdbTipoRichiedente1' type='radio' name='tipoRich' checked='checked' value='LOCATORE' title='Locatore' onclick='disabilitaTextBox();' />Locatore<input id='rdbTipoRichiedente2' name='tipoRich' type='radio' value='CONDUTTORE' title='Conduttore' onclick='disabilitaTextBox();' />Conduttore"
                            HF_rdbTipoRich.Value = "LOCATORE"
                            cmbTipoRich.SelectedValue = "1"
                            txtCognome.Text = par.IfNull(rowP.Item("DENOMINAZIONE"), "")
                            txtCodFiscale.Text = par.IfNull(rowP.Item("CODFISC_PIVA"), "")
                            txtCognRapp.Text = par.IfNull(rowP.Item("COGNOME_RAPP"), "")
                            txtNomeRapp.Text = par.IfNull(rowP.Item("NOME_RAPP"), "")
                            txtCFrapp.Text = par.IfNull(rowP.Item("CODFISC_RAPP"), "")
                            nomeLocatore.Value = txtCognome.Text
                            codFiscLocatore.Value = txtCodFiscale.Text
                            nomeRapprLegale.Value = txtNomeRapp.Text
                            cognomeRapprLegale.Value = txtCognRapp.Text
                            codFiscaleRapprLegale.Value = txtCFrapp.Text
                        Case 1
                            lblTipoRich.Text = "<input id='rdbTipoRichiedente1' type='radio' name='tipoRich' value='LOCATORE' title='Locatore' onclick='disabilitaTextBox();' />Locatore<input id='rdbTipoRichiedente2' name='tipoRich' type='radio' checked='checked' value='CONDUTTORE' title='Conduttore' onclick='disabilitaTextBox();' />Conduttore"
                            HF_rdbTipoRich.Value = "CONDUTTORE"
                            cmbTipoRich.SelectedValue = "1"
                            txtCognome.Text = ""
                            txtCodFiscale.Text = ""
                            txtCognRapp.Text = ""
                            txtNomeRapp.Text = ""
                            txtCFrapp.Text = ""
                            'nomeLocatore.Value = txtCognome.Text
                            'codFiscLocatore.Value = txtCodFiscale.Text
                        Case 2
                            lblTipoRich.Text = "<input id='rdbTipoRichiedente1' type='radio' name='tipoRich' value='LOCATORE' title='Locatore' onclick='disabilitaTextBox();' />Locatore<input id='rdbTipoRichiedente2' name='tipoRich' type='radio' value='CONDUTTORE' title='Conduttore' onclick='disabilitaTextBox();' />Conduttore"
                            HF_rdbTipoRich.Value = "MEDIATORE"
                            cmbTipoRich.SelectedValue = "2"
                            txtCognome.Text = par.IfNull(rowP.Item("DENOMINAZIONE"), "")
                            txtCodFiscale.Text = par.IfNull(rowP.Item("CODFISC_PIVA"), "")
                            txtCognRapp.Text = par.IfNull(rowP.Item("COGNOME_RAPP"), "")
                            txtNomeRapp.Text = par.IfNull(rowP.Item("NOME_RAPP"), "")
                            txtCFrapp.Text = par.IfNull(rowP.Item("CODFISC_RAPP"), "")
                            nomeMediatore.Value = txtCognome.Text
                            codFiscMediatore.Value = txtCodFiscale.Text
                            nomeRapprLegale.Value = txtNomeRapp.Text
                            cognomeRapprLegale.Value = txtCognRapp.Text
                            codFiscaleRapprLegale.Value = txtCFrapp.Text
                    End Select
                Next
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " CaricaDati() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        SalvaDati2()
    End Sub

    Private Sub SalvaDati2()
        Try
            If errore.Value = "0" Then
                Dim StrUpdate As String = ""
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim idTipoParam As Integer = 0
                Select Case HF_rdbTipoRich.Value
                    Case "LOCATORE"
                        idTipoParam = 0
                    Case "CONDUTTORE"
                        idTipoParam = 1
                    Case "MEDIATORE"
                        idTipoParam = 2
                End Select

                StrUpdate = " UPDATE SISCOM_MI.PARAMETRI_RICHIEDENTE_XML " _
                & "SET ID_TIPO_RICH  = " & idTipoParam & "," _
                & "DENOMINAZIONE = '" & par.PulisciStrSql(par.IfEmpty(txtCognome.Text, "")).ToUpper & "'," _
                & "CODFISC_PIVA  = '" & par.IfEmpty(txtCodFiscale.Text, "").ToUpper & "'," _
                & "COGNOME_RAPP  = '" & par.PulisciStrSql(par.IfEmpty(txtCognRapp.Text, "")).ToUpper & "'," _
                & "NOME_RAPP     = '" & par.PulisciStrSql(par.IfEmpty(txtNomeRapp.Text, "")).ToUpper & "'," _
                & "CODFISC_RAPP  = '" & par.PulisciStrSql(par.IfEmpty(txtCFrapp.Text, "")).ToUpper & "'"
                par.cmd.CommandText = StrUpdate
                par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaDati2()
            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " SalvaDati() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub SalvaDati()
        Try
            If errore.Value = "0" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim CTRL As Control = Nothing
                Dim StrUpdate As String
                If cmbTipoRich.SelectedValue = "2" Then
                    For Each CTRL In form1.Controls
                        If TypeOf CTRL Is TextBox Then
                            If DirectCast(CTRL, TextBox).Text.ToString <> "" Then
                                If DirectCast(CTRL, TextBox).Attributes("ID") <> "8" And DirectCast(CTRL, TextBox).Attributes("ID") <> "10" Then
                                    StrUpdate = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & par.PulisciStrSql(DirectCast(CTRL, TextBox).Text) & "' WHERE ID = " & DirectCast(CTRL, TextBox).Attributes("ID").ToUpper.ToString
                                    par.cmd.CommandText = StrUpdate
                                    par.cmd.ExecuteNonQuery()
                                    par.cmd.CommandText = ""
                                End If
                            End If
                        End If
                        If TypeOf CTRL Is DropDownList Then
                            If DirectCast(CTRL, DropDownList).Text.ToString <> "" Then
                                StrUpdate = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & par.PulisciStrSql(DirectCast(CTRL, DropDownList).SelectedValue) & "' WHERE ID = " & DirectCast(CTRL, DropDownList).Attributes("ID").ToUpper.ToString
                                par.cmd.CommandText = StrUpdate
                                par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = ""
                            End If
                        End If
                        'If TypeOf CTRL Is RadioButtonList Then
                        '    If DirectCast(CTRL, RadioButtonList).Text.ToString <> "" Then
                        '        StrUpdate = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & par.PulisciStrSql(DirectCast(CTRL, RadioButtonList).SelectedValue) & "' WHERE ID = " & DirectCast(CTRL, RadioButtonList).Attributes("ID").ToUpper.ToString
                        '        par.cmd.CommandText = StrUpdate
                        '        par.cmd.ExecuteNonQuery()
                        '        par.cmd.CommandText = ""
                        '    End If
                        'End If
                    Next
                Else
                    If HF_rdbTipoRich.Value <> "" Then
                        StrUpdate = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & HF_rdbTipoRich.Value & "' WHERE ID = 43"
                        par.cmd.CommandText = StrUpdate
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        StrUpdate = "UPDATE SISCOM_MI.PARAMETRI_BOLLETTA SET VALORE = '" & cmbTipoRich.SelectedValue & "' WHERE ID = 42"
                        par.cmd.CommandText = StrUpdate
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                    End If
                End If

                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaDati()
            End If

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " SalvaDati() -" & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
