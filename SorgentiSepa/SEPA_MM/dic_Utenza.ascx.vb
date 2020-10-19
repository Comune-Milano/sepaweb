
Partial Class dic_Utenza
    Inherits UserControlSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If	
        If Not IsPostBack Then
            txtCognome.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Utenza1$txtCF,Dic_Utenza1$txtbinserito);")
            txtNome.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Utenza1$txtCF,Dic_Utenza1$txtbinserito);")
            cmbComuneNas.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Utenza1$txtCF,Dic_Utenza1$txtbinserito);")

            txtDataNascita.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataCessazione.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataDec.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            txtData1.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            'txtDataNascita.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Utenza1$txtCF,Dic_Utenza1$txtbInserito);")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("OnChange", "javascript:AttendiCF();")
            txtIndRes.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

        End If
        Dim CTRL As Control
        For Each CTRL In Me.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            End If
        Next
    End Sub

    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            CFLABEL.Visible = True
            CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
            txtCF.Text = ""
        Else
            'CFLABEL.Visible = False
            CFLABEL.Text = ""
            If txtCF.Text <> "" Then
                If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                    If Correlazioni(UCase(txtCF.Text)) = True Then
                        CFLABEL.Visible = True
                        CFLABEL.Text = UCase(txtCF.Text) & ": TROVATO IN ALTRE DICHIARAZIONI!"
                        CompletaDati(UCase(txtCF.Text))
                        CFLABEL.Attributes.Add("OnClick", "javascript:window.open('../CorrelazioniUtenza.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Value & "','Correlazioni','top=0,left=0,width=600,height=400');")
                        If txtbinserito.Value = "0" Or txtbinserito.Value = "" Then
                            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)
                            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Value = 0
                            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("txtProgr"), TextBox).Text = "0"
                            txtbinserito.Value = "1"
                            'CType(Me.Parent.FindControl("UpdatePanel1"), Microsoft.Web.UI.UpdatePanel).DataBind()
                            'CType(Me.Parent.FindControl("Up"), Microsoft.Web.UI.UpdatePanel).Update()
                        End If
                    Else
                        'CFLABEL.Visible = False
                        CFLABEL.Text = ""
                        CompletaDati(UCase(txtCF.Text))
                        If txtbinserito.Value = "0" Or txtbinserito.Value = "" Then
                            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)
                            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Value = 0
                            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("txtProgr"), TextBox).Text = "0"
                            txtbinserito.Value = "1"
                            'CType(Me.Parent.FindControl("UpdatePanel1"), Microsoft.Web.UI.UpdatePanel).Update()
                            'CType(Me.Parent.FindControl("Up"), Microsoft.Web.UI.UpdatePanel).Update()
                        End If
                        par.SetFocusControl(Page, "Dic_Utenza1_cmbNazioneNas")
                    End If
                Else
                    CFLABEL.Visible = True
                    CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
                End If
            Else
                'CFLABEL.Visible = False
                CFLABEL.Text = ""
            End If
        End If
    End Sub

    Private Sub CompletaDati(ByVal CF As String)
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


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

    End Sub

    Private Function Correlazioni(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE"), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE"), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni = False

        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "' OR UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "') AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE<>" & txtId.Value

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            'txtCAPRes.Text = myReader(0)
            Correlazioni = True
        End If
        myReader.Close()

    End Function

    Protected Sub cmbNazioneNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazioneNas.SelectedIndexChanged
        If cmbNazioneNas.Items.FindByText("ITALIA").Selected = False Then
            cmbPrNas.Visible = False
            Label6.Visible = False
            Label7.Visible = False
            cmbComuneNas.Visible = False
            txtCF.Text = ""
            CFLABEL.Visible = False
            txtDataNascita.Text = ""
            txtbinserito.Value = "0"
        Else
            cmbPrNas.Visible = True
            Label6.Visible = True
            Label7.Visible = True
            cmbComuneNas.Visible = True
            txtCF.Text = ""
            CFLABEL.Visible = False
            txtDataNascita.Text = ""
            txtbinserito.Value = "0"
        End If
        par.SetFocusControl(Page, "Dic_Utenza1_cmbPrNas")
    End Sub

    Protected Sub cmbNazioneRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbNazioneRes.SelectedIndexChanged
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
        par.SetFocusControl(Page, "Dic_Utenza1_cmbPrRes")
    End Sub

    Protected Sub cmbPrRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrRes.SelectedIndexChanged
        Dim item As ListItem
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

        item = cmbPrRes.SelectedItem
        'par.OracleConn.Open()
        par.RiempiDList(Me, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")
        'par.OracleConn.Close()
        txtCAPRes.Text = ""
        par.SetFocusControl(Page, "Dic_Utenza1_cmbComuneRes")


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

    Protected Sub cmbComuneRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComuneRes.SelectedIndexChanged
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
        par.SetFocusControl(Page, "Dic_Utenza1_cmbTipoIRes")
    End Sub

    Protected Sub cmbPrNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrNas.SelectedIndexChanged
        Dim item As ListItem

        txtCF.Text = ""
        CFLABEL.Visible = False
        txtDataNascita.Text = ""
        txtbinserito.Value = "0"
        item = cmbPrNas.SelectedItem
        par.RiempiDList(Me, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

        par.SetFocusControl(Page, "Dic_Utenza1_cmbComuneNas")
    End Sub

    Public Sub DisattivaTutto()
        txtData1.Enabled = False
        txtCognome.Enabled = False
        txtNome.Enabled = False
        txtCF.Enabled = False
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
        txtDataCessazione.Enabled = False
        txtDataDec.Enabled = False

        ch1.Enabled = False
        Ch2.Enabled = False
        Ch3.Enabled = False
        Ch4.Enabled = False
        Ch5.Enabled = False
        Ch6.Enabled = False
        txtPosizione.Enabled = False
        TxtRapporto.Enabled = False
        TxtRapportoReale.Enabled = False
        txtCodAlloggio.Enabled = False
        R1.Enabled = False
        R2.Enabled = False
        txtDataCessazione.Enabled = False
        txtScala.Enabled = False
        txtPiano.Enabled = False
        txtAlloggio.Enabled = False
        txtFoglio.Enabled = False
        txtMappale.Enabled = False
        txtSub.Enabled = False

        X1.Enabled = False
        X2.Enabled = False
        Sosp1.Enabled = False
        Sosp2.Enabled = False
        Sosp3.Enabled = False
        Sosp4.Enabled = False
        Sosp5.Enabled = False
        Sosp6.Enabled = False
        Sosp7.Enabled = False

        chPosta.Enabled = False
        chInServizio.Enabled = False
    End Sub



    Protected Sub txtDataNascita_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataNascita.TextChanged
        CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)
    End Sub

    Protected Sub txtDataNascita_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataNascita.Unload
        If txtCognome.Text <> "" Then
            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)
        End If
    End Sub


End Class
