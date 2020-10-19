Namespace CM

    Partial Class impostapw
        Inherits PageSetIdMode
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
            lblmessaggio.Visible = False

            If Not IsPostBack Then
                Try

                    Dim Inattivita As Integer = 0
                    Dim GiorniScadenza As Integer = 0
                    Dim MinLunghezza As Integer = 0

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=64"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Tentativi = par.IfNull(myReader("VALORE"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=65"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Inattivita = par.IfNull(myReader("VALORE"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=61"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        GiorniScadenza = par.IfNull(myReader("VALORE"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=62"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        MinLunghezza = par.IfNull(myReader("VALORE"), 0)
                    End If
                    myReader.Close()

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Label5.Text = "In quest'area puoi modificare la password con cui ti connetti a SEPA@Web. La Password deve essere composta da almeno " & MinLunghezza & " caratteri alfanumerici. Almeno 2 maiuscole, numeri o caratteri speciali. Non può essere uguale alle ultime 4 password impostate. Si ricorda inoltre che la validità della password è di " & GiorniScadenza & " giorni e che se non utilizzata per " & Inattivita & " giorni, l'utenza sarà revocata."

                Catch ex As Exception
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If
            
        End Sub

        Private Sub btnSalva_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalva.Click
            Try
                If Page.IsValid = True Then
                    Dim i As Integer = 0

                    If txtPw.Text = "" Or txtNPw.Text = "" Or txtCNPw.Text = "" Then
                        lblmessaggio.Visible = True
                        lblmessaggio.Text = "Attenzione: Tutti i campi devono essere valorizzati!"
                        Exit Sub
                    End If
                    If txtNPw.Text <> txtCNPw.Text Then
                        lblmessaggio.Visible = True
                        lblmessaggio.Text = "'Nuova Password' e 'Conferma Nuova Password' non coincidono!"
                        Exit Sub
                    End If

                    If ModuloControlloPw(txtNPw.Text) = False Then
                        Exit Sub
                    End If

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "SELECT * FROM OPERATORI WHERE ID=" + Session.Item("ID_OPERATORE")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        Dim PwMatch As Boolean = par.VerifyHash(txtPw.Text, "SHA512", Trim(par.IfNull(myReader("PW"), ""))).ToString()
                        If PwMatch = True Then
                            par.cmd.CommandText = "select * from storico_pw where tipo_operatore='I' and id_operatore=" & Session.Item("ID_OPERATORE") & " ORDER BY ID DESC"
                            myReader = par.cmd.ExecuteReader()
                            i = 1
                            Do While myReader.Read
                                Dim PwMatch1 As Boolean = par.VerifyHash(txtNPw.Text, "SHA512", Trim(par.IfNull(myReader("pw"), ""))).ToString()
                                If PwMatch1 = True Then
                                    lblmessaggio.Visible = True
                                    lblmessaggio.Text = "La password deve essere diversa dalle ultime 4 inserite!"
                                    myReader.Close()
                                    par.OracleConn.Close()
                                    Exit Sub
                                End If
                                If i = 4 Then Exit Do
                                i = i + 1
                            Loop
                            myReader.Close()

                            Dim myTrans As Oracle.DataAccess.Client.OracleTransaction
                            myTrans = par.OracleConn.BeginTransaction()
                            par.SettaCommand(par)

                            Try
                                par.cmd.CommandText = "UPDATE OPERATORI SET PW_DATA_INSERIMENTO='" & Format(Now, "yyyyMMdd") & "',PW='" + par.ComputeHash(txtCNPw.Text, "SHA512", Nothing) + "' WHERE ID=" + Session.Item("ID_OPERATORE")
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO STORICO_PW (ID,DATA_PW,PW,ID_OPERATORE,TIPO_OPERATORE) values (SEQ_STORICO_PW.NEXTVAL,'" & Format(Now, "yyyyMMdd") & "','" & par.ComputeHash(txtCNPw.Text, "SHA512", Nothing) & "'," & Session.Item("ID_OPERATORE") & ",'I')"
                                par.cmd.ExecuteNonQuery()

                                myTrans.Commit()
                                lblmessaggio.Visible = True
                                lblmessaggio.Text = "La password è stata modificata con successo!"
                                Response.Write("<script>alert('Password Modificata con successo!');document.location.href=""AreaPrivata.aspx""</script>")
                            Catch EX As Exception
                                myTrans.Rollback()
                                lblmessaggio.Visible = True
                                lblmessaggio.Text = "Operazione annullata a causa di un errore!"
                            Finally
                                If Not par.OracleConn Is Nothing Then
                                    par.OracleConn.Close()
                                End If
                            End Try
                        Else
                            lblmessaggio.Visible = True
                            lblmessaggio.Text = "La password attuale non è esatta ed è stata revocata!"
                            NTentativi = NTentativi + 1
                            If NTentativi = Tentativi Then
                                par.cmd.CommandText = "update OPERATORI set revoca=2,motivo_revoca='Limite Tentativi di accesso raggiunto' where  id=" & Session.Item("ID_OPERATORE")
                                par.cmd.ExecuteNonQuery()
                                Session.Item("OPERATORE") = ""
                                Response.Redirect("Portale.aspx", False)
                            End If
                        End If
                    Else
                        lblmessaggio.Visible = True
                    End If
                    myReader.Close()
                End If
            Catch EX As Exception
                lblmessaggio.Visible = True
                lblmessaggio.Text = "ERRORE: " + EX.ToString
            Finally
                If Not par.OracleConn Is Nothing Then
                    par.OracleConn.Close()
                End If
            End Try
        End Sub

        Private Sub btnAnnulla_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnnulla.Click
            Response.Write("<script>document.location.href=""AreaPrivata.aspx""</script>")

        End Sub

        Private Function ModuloControlloPw(ByVal pw As String) As Boolean
            ModuloControlloPw = False


            Dim tentativi As Integer = 0
            Dim Inattivita As Integer = 0
            Dim GiorniScadenza As Integer = 0
            Dim MinLunghezza As Integer = 0
            Dim AlfaNumerica As Integer = 0

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=65"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Inattivita = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=61"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                GiorniScadenza = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=62"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                MinLunghezza = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM PARAMETER WHERE ID=63"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                AlfaNumerica = par.IfNull(myReader("VALORE"), 0)
            End If
            myReader.Close()

            If InStr(pw, "<") > 0 Or InStr(pw, ">") > 0 Or InStr(pw, "'") > 0 Or InStr(pw, "*") > 0 Then
                lblmessaggio.Visible = True
                lblmessaggio.Text = "Non sono ammessi i caratteri < > ' *"
                ModuloControlloPw = False
                myReader.Close()
                par.OracleConn.Close()
                Exit Function
            End If

            If Len(pw) < MinLunghezza Then
                lblmessaggio.Visible = True
                lblmessaggio.Text = "La lunghezza della password deve essere di almeno " & MinLunghezza & " caratteri!"
                ModuloControlloPw = False
                myReader.Close()
                par.OracleConn.Close()
                Exit Function
            End If

            'controllo presenza di almeno 2 caratteri affinchè siano maiuscole

            Dim contaObbligo As Integer = 0
            For Each s As String In txtCNPw.Text.ToCharArray
                If Not IsNumeric(s.ToString) Then
                    If s.ToString = s.ToString.ToUpper Then
                        contaObbligo = contaObbligo + 1
                    End If
                End If

                If IsNumeric(s.ToString) Then
                    contaObbligo = contaObbligo + 1
                End If
                If Asc(s.ToString) >= 123 Then
                    contaObbligo = contaObbligo + 1
                End If
            Next

            If contaObbligo < 2 Then
                lblmessaggio.Visible = True
                lblmessaggio.Text = "La password deve contenere almeno 2 caratteri tra maiuscole, numeri, caratteri speciali"
                myReader.Close()
                par.OracleConn.Close()
                Exit Function


            End If
            'If AlfaNumerica = 1 Then
            '    If IsNumeric(pw) = True Then
            '        lblmessaggio.Visible = True
            '        lblmessaggio.Text = "La password deve contenere caratteri e numeri!"
            '        ModuloControlloPw = False
            '        myReader.Close()
            '        par.OracleConn.Close()
            '        Exit Function
            '    End If

            '    If InStr(1, pw, "0") = 0 And InStr(1, pw, "1") = 0 And InStr(1, pw, "2") = 0 And InStr(1, pw, "3") = 0 And InStr(1, pw, "4") = 0 And InStr(1, pw, "5") = 0 And InStr(1, pw, "6") = 0 And InStr(1, pw, "7") = 0 And InStr(1, pw, "8") = 0 And InStr(1, pw, "9") = 0 Then
            '        lblmessaggio.Visible = True
            '        lblmessaggio.Text = "La password deve contenere caratteri e numeri!"
            '        ModuloControlloPw = False
            '        myReader.Close()
            '        par.OracleConn.Close()
            '        Exit Function
            '    End If
            'End If
            par.OracleConn.Close()
            ModuloControlloPw = True
        End Function

        Public Property NTentativi() As Integer
            Get
                If Not (ViewState("par_NTentativi") Is Nothing) Then
                    Return CInt(ViewState("par_NTentativi"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Integer)
                ViewState("par_NTentativi") = value
            End Set
        End Property

        Public Property Tentativi() As Integer
            Get
                If Not (ViewState("par_Tentativi") Is Nothing) Then
                    Return CInt(ViewState("par_Tentativi"))
                Else
                    Return 0
                End If
            End Get

            Set(ByVal value As Integer)
                ViewState("par_Tentativi") = value
            End Set
        End Property
    End Class

End Namespace
