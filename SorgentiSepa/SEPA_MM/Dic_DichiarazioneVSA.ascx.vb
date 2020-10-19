
Partial Class Dic_Dichiarazione
    Inherits UserControlSetIdMode
    Dim par As New CM.Global

    Protected Sub txtCF_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCF.TextChanged
        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            CFLABEL.Visible = True
            CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
            CFLABEL.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
            txtCF.Text = ""
            LinkButton1.Text = ""
            LinkButton1.Visible = False
        Else
            'CFLABEL.Visible = False
            CFLABEL.Text = ""
            LinkButton1.Visible = False
            LinkButton2.Visible = False
            If txtCF.Text <> "" Then
                If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                    CompletaDati(UCase(txtCF.Text))
                    If txtbinserito.Text = "0" Then
                        CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 11) & par.MiaFormat("NO", 12)
                        CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Value = 0
                        CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("txtprogr"), TextBox).Text = "0"
                        txtbinserito.Text = "1"
                    End If
                    If Correlazioni(UCase(txtCF.Text)) = True Then
                        CFLABEL.Visible = True
                        CFLABEL.Text = "Trovato in BANDO ERP!"
                        CFLABEL.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';window.open('correlazioni.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Text & "&NUOVA=" & Label20.Text & "&CONN=" & CType(Me.Page, Object).lIdConnessDICH & "','Correlazioni','top=0,left=0,width=600,height=400');")
                    End If
                    If CLng(txtId.Text) >= 500000 Then
                        If Correlazioni1(UCase(txtCF.Text)) = True Then
                            LinkButton1.Visible = True
                            LinkButton1.Visible = True
                            LinkButton1.Text = "Trovato in BANDO CAMBI!"
                            LinkButton1.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';window.open('correlazioni1.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Text & "&NUOVA=" & Label20.Text & "&CONN=" & CType(Me.Page, Object).lIdConnessDICH & "','Correlazioni','top=0,left=0,width=600,height=400');")
                        End If
                    End If

                    If CLng(txtId.Text) >= 500000 Then
                        If Correlazioni2(UCase(txtCF.Text)) = True Then
                            LinkButton2.Visible = True
                            LinkButton2.Text = "Trovato in ANAGRAFE UT."
                            LinkButton2.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';window.open('correlazioni2.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Text & "&NUOVA=" & Label20.Text & "&CONN=" & CType(Me.Page, Object).lIdConnessDICH & "','Correlazioni','top=0,left=0,width=600,height=380');")
                        End If
                    End If

                    If CorrelazioniVSA(UCase(txtCF.Text)) = True Then

                        If CFLABEL.Text = "" Then
                            CFLABEL.Visible = True
                            CFLABEL.Text = "Trovato in GEST.LOCATARI!"
                            CFLABEL.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';window.open('correlazioni3.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Text & "&NUOVA=" & Label20.Text & "&CONN=" & CType(Me.Page, Object).lIdConnessDICH & "','Correlazioni','top=0,left=0,width=600,height=400');")
                        Else
                            If LinkButton1.Visible = False Then
                                LinkButton1.Visible = True
                                LinkButton1.Text = "Trovato in GEST.LOCATARI!"
                                LinkButton1.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';window.open('correlazioni3.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Text & "&NUOVA=" & Label20.Text & "&CONN=" & CType(Me.Page, Object).lIdConnessDICH & "','Correlazioni','top=0,left=0,width=600,height=400');")
                            Else
                                If LinkButton2.Visible = False Then
                                    LinkButton2.Visible = True
                                    LinkButton2.Text = "Trovato in GEST.LOCATARI!"
                                    LinkButton2.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';window.open('correlazioni3.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & txtId.Text & "&NUOVA=" & Label20.Text & "&CONN=" & CType(Me.Page, Object).lIdConnessDICH & "','Correlazioni','top=0,left=0,width=600,height=400');")
                                End If
                            End If
                        End If

                    End If
                Else
                    CFLABEL.Visible = True
                    CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
                    CFLABEL.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                    txtCF.Text = ""
                    LinkButton1.Text = ""
                    LinkButton1.Visible = False
                End If
            Else
                CFLABEL.Visible = True
                CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
                CFLABEL.Attributes.Add("OnClick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('H1').value='0';")
                LinkButton1.Text = ""
                LinkButton1.Visible = False
            End If

            'CFLABEL.Visible = False
            'CFLABEL.Text = ""
        End If

    End Sub

    Private Function CaricaComponenti()
        Dim MIAS As String
        Dim INDENNITA As String

        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


        txtbinserito.Text = "1"
        par.cmd.CommandText = "SELECT COMP_NUCLEO_CAMBI.*,T_TIPO_PARENTELA.DESCRIZIONE FROM COMP_NUCLEO_CAMBI,T_TIPO_PARENTELA where COMP_NUCLEO_CAMBI.id_DICHIARAZIONE=" & txtId.Text & " AND COMP_NUCLEO_CAMBI.GRADO_PARENTELA=T_TIPO_PARENTELA.COD ORDER BY PROGR ASC"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader.Read
            If par.IfNull(myReader("INDENNITA_ACC"), "0") = "1" Then
                INDENNITA = "SI"
            Else
                INDENNITA = "NO"
            End If
            MIAS = ""
            MIAS = par.MiaFormat(par.IfNull(myReader("COGNOME"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("NOME"), ""), 25) & " " & par.MiaFormat(par.FormattaData(myReader("DATA_NASCITA")), 10) & " " & par.MiaFormat(par.IfNull(myReader("COD_FISCALE"), ""), 16) & " " & par.MiaFormat(par.IfNull(myReader("DESCRIZIONE"), ""), 25) & " " & par.MiaFormat(par.IfNull(myReader("PERC_INVAL"), ""), 6) & " " & par.MiaFormat(par.IfNull(myReader("USL"), ""), 5) & " " & par.MiaFormat(INDENNITA, 2)
            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items.Add(New ListItem(MIAS, myReader("PROGR")))
            If par.IfNull(myReader("PERC_INVAL"), "0") = "100" And INDENNITA = "SI" Then
                CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox2"), ListBox).Items.Add(New ListItem(par.MiaFormat(myReader("COGNOME") & "," & myReader("NOME"), 52) & " ", myReader("PROGR")))
            End If
            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("txtprogr"), TextBox).Text = myReader("PROGR") + 1

        End While
        myReader.Close()
    End Function

    Private Sub CompletaDati(ByVal CF As String)
        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
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
        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI.ID FROM DICHIARAZIONI,COMP_NUCLEO WHERE DICHIARAZIONI.ID=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO.COD_FISCALE='" & CF & "' OR COMP_NUCLEO.COD_FISCALE='" & CF & "') AND COMP_NUCLEO.ID_DICHIARAZIONE<>" & txtId.Text

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            'txtCAPRes.Text = myReader(0)
            Correlazioni = True
        End If
        myReader.Close()

    End Function

    Private Function Correlazioni1(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni1 = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI_CAMBI.ID FROM DICHIARAZIONI_CAMBI,COMP_NUCLEO_CAMBI WHERE DICHIARAZIONI_CAMBI.ID=COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_CAMBI.COD_FISCALE='" & CF & "' OR COMP_NUCLEO_CAMBI.COD_FISCALE='" & CF & "') AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE<>" & txtId.Text

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            Correlazioni1 = True
        End If
        myReader.Close()

    End Function


    Private Function Correlazioni2(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni2 = False

        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "' OR UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "') AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE<>" & txtId.Text

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            Correlazioni2 = True
        End If
        myReader.Close()

    End Function

    Private Function CorrelazioniVSA(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item(CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & CType(Me.Page, Object).lIdConnessDICH), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans

        CorrelazioniVSA = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI_vsa.ID FROM DICHIARAZIONI_vsa,COMP_NUCLEO_vsa WHERE DICHIARAZIONI_vsa.ID=COMP_NUCLEO_vsa.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_vsa.COD_FISCALE='" & CF & "' OR COMP_NUCLEO_vsa.COD_FISCALE='" & CF & "') AND COMP_NUCLEO_vsa.ID_DICHIARAZIONE<>" & txtId.Text

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then

            CorrelazioniVSA = True
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
            txtbinserito.Text = "0"
        Else
            cmbPrNas.Visible = True
            Label6.Visible = True
            Label7.Visible = True
            cmbComuneNas.Visible = True
            txtCF.Text = ""
            CFLABEL.Visible = False
            txtDataNascita.Text = ""
            txtbinserito.Text = "0"
        End If
        par.SetFocusControl(Page, "Dic_Dichiarazione1_cmbPrNas")
    End Sub

    Protected Sub cmbComuneNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComuneNas.SelectedIndexChanged
        'txtCF.Text = ""
        'CFLABEL.Visible = False
        'txtDataNascita.Text = ""
        'txtbinserito.Text = "0"
        'par.SetFocusControl(Page, "Dic_Dichiarazione1_txtDataNascita")
    End Sub

    Protected Sub txtDataNascita_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataNascita.TextChanged
        CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)


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
        par.SetFocusControl(Page, "Dic_Dichiarazione1_cmbPrRes")
    End Sub

    Protected Sub cmbPrRes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrRes.SelectedIndexChanged
        Dim item As ListItem
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

        item = cmbPrRes.SelectedItem
        'par.OracleConn.Open()
        par.RiempiDList(Me, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")
        'par.OracleConn.Close()
        txtCAPRes.Text = ""
        par.SetFocusControl(Page, "Dic_Dichiarazione1_cmbComuneRes")


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
        par.SetFocusControl(Page, "Dic_Dichiarazione1_cmbTipoIRes")
    End Sub

    Protected Sub cmbPrNas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbPrNas.SelectedIndexChanged
        Dim item As ListItem

        txtCF.Text = ""
        CFLABEL.Visible = False
        txtDataNascita.Text = ""
        txtbinserito.Text = "0"
        item = cmbPrNas.SelectedItem
        par.RiempiDList(Me, par.OracleConn, "cmbComuneNas", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & par.PulisciStrSql(item.Text) & "' ORDER BY NOME ASC", "NOME", "ID")

        par.SetFocusControl(Page, "Dic_Dichiarazione1_cmbComuneNas")
    End Sub

    Protected Sub txtData1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtData1.TextChanged
        'txtData1.Text = Trim(Left(txtData1.Text, 10))
        'Label19.Text = ""
        'If Len(txtData1.Text) = 0 Then
        '    Label19.Text = "*"
        'ElseIf Not par.ControllaData(txtData1) Then
        '    Label19.Visible = True
        '    Label19.Text = "GG/MM/AAAA o GGMMAAAA"
        'Else
        '    Label19.Visible = False
        '    par.SetFocusControl(Page, "Dic_Dichiarazione1_txtCognome")
        'End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            txtCognome.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Dichiarazione1$txtCF,Dic_Dichiarazione1$txtbinserito);")
            txtNome.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Dichiarazione1$txtCF,Dic_Dichiarazione1$txtbinserito);")
            cmbComuneNas.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Dichiarazione1$txtCF,Dic_Dichiarazione1$txtbinserito);")

            txtDataNascita.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtData1.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            'txtDataNascita.Attributes.Add("OnChange", "javascript:AzzeraCF(Dic_Dichiarazione1$txtCF,Dic_Dichiarazione1$txtbinserito);")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("OnChange", "javascript:AttendiCF();")
            txtIndRes.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")


            '**********modifiche campi***********
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

        End If
    End Sub

    'Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles CFLABEL.Click
    '    If Len(txtCF.Text) Then
    '        'Dim scriptblock As String

    '        'scriptblock = "<script>window.open('correlazioni.aspx?" & "CF=" & txtCF.Text & "&ID=" & txtId.Text & "','Correlazioni','top=0,left=0,width=600,height=400');</script>"
    '        'Page.RegisterClientScriptBlock("show", scriptblock)
    '        'Response.Write("<script>window.open('correlazioni.aspx?" & "CF=" & txtCF.Text & "&ID=" & txtId.Text & "','Correlazioni','top=0,left=0,width=600,height=400');</script>")
    '    End If
    'End Sub

    Protected Sub txtCognome_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCognome.TextChanged
        'txtCF.Text = ""
        'txtDataNascita.Text = ""
        'CFLABEL.Visible = False
        'Session.Item("bInseritoC") = 0
        'par.SetFocusControl(Page, "Dic_Dichiarazione1_txtNome")
    End Sub

    Protected Sub txtNome_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNome.TextChanged
        'txtCF.Text = ""
        'CFLABEL.Visible = False
        'txtDataNascita.Text = ""
        'Session.Item("bInseritoC") = 0
        'par.SetFocusControl(Page, "Dic_Dichiarazione1_txtCF")
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
        chTitolare.Enabled = False
    End Sub

    Protected Sub txtDataNascita_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDataNascita.Unload
        If CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items.Count > 0 Then
            'CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items.Remove(CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0))
            'CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items.Add(New ListItem("", 0))
            CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 11) & par.MiaFormat("NO", 12)
        End If

    End Sub
End Class