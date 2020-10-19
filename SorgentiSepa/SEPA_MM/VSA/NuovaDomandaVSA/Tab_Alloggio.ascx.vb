
Partial Class VSA_NuovaDomandaVSA_Tab_Alloggio
    Inherits UserControlSetIdMode
    Dim par As New CM.Global
    Public IdDichiarazione As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        txtDecorrenza.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        txtDataNascita.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
        If Not IsPostBack Then
            '**********modifiche campi***********
            Dim CTRL As Control
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0'")
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                End If
            Next
        End If
    End Sub

    Public Sub DisattivaTutto()
        txtComune.Enabled = False
        txtCAP.Enabled = False
        txtIndirizzo.Enabled = False
        txtCivico.Enabled = False

        txtInterno.Enabled = False
        txtScala.Enabled = False
        'txtPiano.Enabled = False
        cmbPianoUnita.Enabled = False
        cmbAscensore.Enabled = False
        txtLocali.Enabled = False
        txtNumContratto.Enabled = False
        txtDecorrenza.Enabled = False
        txtCognome.Enabled = False
        txtNome.Enabled = False
        txtCF.Enabled = False
        cmbSesso.Enabled = False
        cmbNazioneNas.Enabled = False
        cmbPrNas.Enabled = False
        cmbComuneNas.Enabled = False
        txtDataNascita.Enabled = False
        txtCodiceUnita.Enabled = False
        txtNetta.Enabled = False
        HSL.Value = "1"
        cmbTipoU.Enabled = False
    End Sub

    Function Disattiva()
        txtComune.Enabled = False
        txtCAP.Enabled = False
        txtIndirizzo.Enabled = False
        txtCivico.Enabled = False

        txtInterno.Enabled = False
        txtScala.Enabled = False
        'txtPiano.Enabled = False
        cmbPianoUnita.Enabled = False
        'cmbAscensore.Enabled = False
        'txtLocali.Enabled = False
        txtNumContratto.Enabled = False
        txtDecorrenza.Enabled = False
        txtNetta.Enabled = False
    End Function

    Protected Sub cmbNazioneNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazioneNas.SelectedIndexChanged
        If cmbNazioneNas.Items.FindByText("ITALIA").Selected = False Then
            cmbPrNas.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            cmbComuneNas.Visible = False
            txtDataNascita.Text = ""
            Label20.Visible = False
            Label21.Visible = False

        Else
            cmbPrNas.Visible = True
            Label6.Visible = True
            Label7.Visible = True
            cmbComuneNas.Visible = True
            txtDataNascita.Text = ""
            Label20.Visible = True
            Label21.Visible = True
        End If
    End Sub

    Protected Sub cmbPrNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrNas.SelectedIndexChanged
        Dim item As ListItem

        txtDataNascita.Text = ""
        item = cmbPrNas.SelectedItem
        par.RiempiDList(Me, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

    End Sub

    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
        If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
            Label14.Visible = False
            CompletaDati(UCase(txtCF.Text))
        Else
            txtCF.Text = ""
            Label14.Visible = True
        End If
    End Sub

    Private Sub CompletaDati(ByVal CF As String)
        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDOMANDA), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDOMANDA), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans



        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(CF, 12, 4) & "'"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read() Then

            If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                cmbNazioneNas.SelectedIndex = -1
                cmbNazioneNas.Items.FindByText(myReader1("NOME")).Selected = True
                cmbPrNas.SelectedIndex = -1
                cmbComuneNas.SelectedIndex = -1
                cmbPrNas.Visible = False
                cmbComuneNas.Visible = False
            Else
                cmbNazioneNas.SelectedIndex = -1
                cmbNazioneNas.Items.FindByText("ITALIA").Selected = True

                cmbPrNas.SelectedIndex = -1
                cmbPrNas.Items.FindByText(myReader1("SIGLA")).Selected = True

                par.cmd.CommandText = "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                cmbComuneNas.SelectedIndex = -1
                While myReader2.Read
                    Dim lsiFrutto As New ListItem(myReader2("NOME"), myReader2("ID"))
                    cmbComuneNas.Items.Add(lsiFrutto)
                End While
                myReader2.Close()
                cmbComuneNas.SelectedIndex = -1
                cmbComuneNas.Items.FindByText(myReader1("NOME")).Selected = True
            End If
            Dim MIADATA As String
            txtDataNascita.Text = ""
            If Val(Mid(CF, 10, 2)) > 40 Then
                MIADATA = Format(Val(Mid(CF, 10, 2)) - 40, "00")
            Else
                MIADATA = Mid(CF, 10, 2)
            End If

            Select Case Mid(CF, 9, 1)
                Case "A"
                    MIADATA = MIADATA & "/01"
                Case "B"
                    MIADATA = MIADATA & "/02"
                Case "C"
                    MIADATA = MIADATA & "/03"
                Case "D"
                    MIADATA = MIADATA & "/04"
                Case "E"
                    MIADATA = MIADATA & "/05"
                Case "H"
                    MIADATA = MIADATA & "/06"
                Case "L"
                    MIADATA = MIADATA & "/07"
                Case "M"
                    MIADATA = MIADATA & "/08"
                Case "P"
                    MIADATA = MIADATA & "/09"
                Case "R"
                    MIADATA = MIADATA & "/10"
                Case "S"
                    MIADATA = MIADATA & "/11"
                Case "T"
                    MIADATA = MIADATA & "/12"
            End Select
            If Mid(CF, 7, 1) = "0" Then
                MIADATA = MIADATA & "/200" & Mid(CF, 8, 1)
                If Format(CDate(MIADATA), "yyyyMMdd") > Format(Now, "yyyyMMdd") Then
                    MIADATA = Mid(MIADATA, 1, 6) & "19" & Mid(MIADATA, 9, 2)
                End If
            Else
                MIADATA = MIADATA & "/19" & Mid(CF, 7, 2)
            End If
            txtDataNascita.Text = MIADATA
        End If
        myReader1.Close()

        cmbSesso.SelectedIndex = -1
        cmbSesso.Items.FindByText(par.RicavaSesso(CF)).Selected = True




    End Sub

    Protected Sub cmbTipoU_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoU.SelectedIndexChanged
        If cmbTipoU.SelectedItem.Text = "E.R.P." Then
            txtNumContratto.Enabled = False
            txtCodiceUnita.Enabled = False
        Else
            txtNumContratto.Enabled = True
            txtCodiceUnita.Enabled = True
        End If
    End Sub
End Class

