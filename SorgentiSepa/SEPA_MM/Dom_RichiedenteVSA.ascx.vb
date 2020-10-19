Namespace CM

Partial  Class Dom_Richiedente
    Inherits UserControlSetIdMode
    Dim par As New [Global]()

#Region " Codice generato da Progettazione Web Form "

    'Chiamata richiesta da Progettazione Web Form.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: questa chiamata al metodo è richiesta da Progettazione Web Form.
        'Non modificarla nell'editor del codice.
        InitializeComponent()
    End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            If Not IsPostBack Then
                'txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtIndRes.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtPresso.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                txtIndirizzoRec.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                'cmbNazioneNas.Items.FindByText("ITALIA").Selected = True

                '**********modifiche campi***********
                Dim CTRL As Control
                For Each CTRL In Me.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                    End If
                Next

            End If

        End Sub


    Private Sub txtCognome_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCognome.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtNome")
    End Sub

    Private Sub txtNome_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNome.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_cmbSesso")
    End Sub

    Private Sub cmbSesso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSesso.SelectedIndexChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtCF")
    End Sub

    Private Sub txtCF_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
            par.SetFocusControl(Page, "Dom_Richiedente1_cmbResidenza")
    End Sub

        Private Sub cbmNazioneNas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            par.SetFocusControl(Page, "Dom_Richiedente1_cmbPrNas")
        End Sub

    Private Sub cmbPrNas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrNas.SelectedIndexChanged
        Dim item As ListItem

        item = cmbPrNas.SelectedItem
        par.RiempiDList(Me, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

        Par.SetFocusControl(Page, "Dom_Richiedente1_cmbComuneNas")
    End Sub

    Private Sub cmbComuneNas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComuneNas.SelectedIndexChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtDataNascita")
    End Sub

    Private Sub txtDataNascita_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDataNascita.TextChanged

        txtDataNascita.Text = Trim(Left(txtDataNascita.Text, 10))
        lblErrData.Text = ""
        If Len(txtDataNascita.Text) = 0 Then
            lblErrData.Text = "*"
        ElseIf Not par.ControllaData(txtDataNascita) Then
            lblErrData.Text = "GG/MM/AAAA o GGMMAAAA"
        Else
            par.SetFocusControl(Page, "Dom_Richiedente1_cmbNazioneRes")
        End If

    End Sub

    Private Sub cmbNazioneRes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNazioneRes.SelectedIndexChanged
        If cmbNazioneRes.Items.FindByText("ITALIA").Selected = False Then
            cmbPrRes.Visible = False
            Label11.Visible = False
            Label10.Visible = False
            cmbComuneRes.Visible = False
        Else
            cmbPrRes.Visible = True
            Label11.Visible = True
            Label10.Visible = True
            cmbComuneRes.Visible = True
        End If
        par.SetFocusControl(Page, "Dom_Richiedente1_cmbPrRes")
    End Sub

    Private Sub cmbPrRes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPrRes.SelectedIndexChanged
        Dim item As ListItem
            Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

            item = cmbPrRes.SelectedItem
            'par.OracleConn.Open()
            par.RiempiDList(Me, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")
            'par.OracleConn.Close()
            txtCAPRes.Text = ""
            par.SetFocusControl(Page, "Dom_Richiedente1_cmbComuneRes")


            item = cmbComuneRes.SelectedItem
            par.OracleConn.Open()
            par.SettaCommand(par)
            cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            If myReader.Read() Then
                txtCAPRes.Text = myReader(0)
            End If
            myReader.Close()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Sub

        Private Sub cmbComuneRes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComuneRes.SelectedIndexChanged
            Dim item As ListItem
            Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

            item = cmbComuneRes.SelectedItem
            par.OracleConn.Open()
            par.SettaCommand(par)
            cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            If myReader.Read() Then
                txtCAPRes.Text = myReader(0)
            End If
            myReader.Close()
            par.OracleConn.Close()
            par.OracleConn = Nothing
            par.SetFocusControl(Page, "Dom_Richiedente1_cmbTipoIRes")
        End Sub

    Private Sub cmbTipoIRes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoIRes.SelectedIndexChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtIndRes")
    End Sub

    Private Sub txtIndRes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIndRes.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtCivicoRes")
    End Sub

    Private Sub txtCivicoRes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCivicoRes.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtCAPRes")
    End Sub

    Private Sub txtCAPRes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCAPRes.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtTelRes")
    End Sub

    Private Sub txtTelRes_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTelRes.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtPresso")
    End Sub

    Private Sub txtPresso_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPresso.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_cmbProvRec")
    End Sub

    Private Sub cmbProvRec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbProvRec.SelectedIndexChanged
        Dim item As ListItem
            Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

            item = cmbProvRec.SelectedItem
            par.RiempiDList(Me, par.OracleConn, "cmbComuneRec", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

            par.SetFocusControl(Page, "Dom_Richiedente1_cmbComuneRec")


            item = cmbComuneRec.SelectedItem
            par.OracleConn.Open()
            par.SettaCommand(par)
            cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            If myReader.Read() Then
                txtCAPRec.Text = myReader(0)
            End If
            myReader.Close()
            par.OracleConn.Close()
            par.OracleConn = Nothing
        End Sub

        Private Sub cmbComuneRec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbComuneRec.SelectedIndexChanged
            Dim item As ListItem
            Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

            item = cmbComuneRec.SelectedItem
            par.OracleConn.Open()
            par.SettaCommand(par)
            cmd.CommandText = "SELECT CAP FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(item.Text) & "'"

            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()
            If myReader.Read() Then
                txtCAPRec.Text = myReader(0)
            End If
            myReader.Close()
            par.OracleConn.Close()
            par.OracleConn = Nothing
            par.SetFocusControl(Page, "Dom_Richiedente1_txtCAPRec")
        End Sub

    Private Sub txtCAPRec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCAPRec.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_cmbTipoIRec")
    End Sub

    Private Sub cmbTipoIRec_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipoIRec.SelectedIndexChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtIndirizzoRec")
    End Sub

    Private Sub txtIndirizzoRec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtIndirizzoRec.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtCivicoRec")
    End Sub

    Private Sub txtCivicoRec_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCivicoRec.TextChanged
        par.SetFocusControl(Page, "Dom_Richiedente1_txtTelRec")
    End Sub

    Private Sub cmbNazioneNas_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNazioneNas.SelectedIndexChanged
        If cmbNazioneNas.Items.FindByText("ITALIA").Selected = False Then
            cmbPrNas.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            cmbComuneNas.Visible = False
        Else
            cmbPrNas.Visible = True
            Label6.Visible = True
            Label7.Visible = True
            cmbComuneNas.Visible = True
        End If
        par.SetFocusControl(Page, "Dom_Richiedente1_cmbPrNascita")
    End Sub

    Public Function DisattivaRichiedente()
        txtCognome.Enabled = False
        txtNome.Enabled = False
        cmbSesso.Enabled = False
        txtCF.Enabled = False
        cmbNazioneNas.Enabled = False
        cmbPrNas.Enabled = False
        cmbComuneNas.Enabled = False
        txtDataNascita.Enabled = False
    End Function

    Public Function DisattivaIndirizzo()
        cmbNazioneRes.Enabled = False
        cmbPrRes.Enabled = False
        cmbComuneRes.Enabled = False
        cmbTipoIRes.Enabled = False
        txtIndRes.Enabled = False
        txtCivicoRes.Enabled = False
            txtCAPRes.Enabled = False
            txtTelRes.Enabled = False
    End Function

        Public Sub DisattivaTutto()
            txtCognome.Enabled = False
            txtNome.Enabled = False
            cmbSesso.Enabled = False
            txtCF.Enabled = False
            cmbResidenza.Enabled = False
            cmbNazioneNas.Enabled = False
            cmbPrNas.Enabled = False
            cmbComuneNas.Enabled = False
            txtDataNascita.Enabled = False
            cmbNazioneRes.Enabled = False
            cmbPrRes.Enabled = False
            cmbComuneRes.Enabled = False
            cmbTipoIRes.Enabled = False
            txtIndRes.Enabled = False
            txtCivicoRes.Enabled = False
            txtCAPRes.Enabled = False
            txtTelRes.Enabled = False
            txtPresso.Enabled = False
            cmbProvRec.Enabled = False
            cmbComuneRec.Enabled = False
            txtCAPRec.Enabled = False
            cmbTipoIRec.Enabled = False
            txtIndirizzoRec.Enabled = False
            txtCivicoRec.Enabled = False
            txtTelRec.Enabled = False

            cmbResidenza.Enabled = False


        End Sub

    End Class

End Namespace
