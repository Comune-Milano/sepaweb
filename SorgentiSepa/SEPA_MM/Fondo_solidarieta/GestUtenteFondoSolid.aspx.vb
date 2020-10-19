
Partial Class Fondo_solidarieta_GestUtenteFondoSolid
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim Chiamante As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            par.caricaComboBox("SELECT cod, 'ZONA' ||' '||zona as nomeZona FROM ZONA_ALER where nominativo is not null order by zona", cmbUtente, "COD", "nomeZona")
        End If
    End Sub

    Private Sub caricaPassword(ByVal zona As Integer)
        Try
            connData.apri()

            par.cmd.CommandText = "select * from siscom_mi.UTENZE_FONDO_SOLIDARIETA where id_zona=" & zona & " and password is null"
            Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore0.Read Then
                par.cmd.CommandText = "UPDATE siscom_mi.UTENZE_FONDO_SOLIDARIETA SET PASSWORD='" + par.ComputeHash("SEPA", "SHA512", Nothing) + "' WHERE ID_ZONA=" & zona
                par.cmd.ExecuteNonQuery()
            End If
            lettore0.Close()

            connData.chiudi()

        Catch ex As Exception
            connData.chiudi()
            lblMessaggio.Visible = True
            lblMessaggio.Text = "Operazione annullata a causa di un errore!"
            Session.Item("OPERATORE") = ""
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        If Page.IsValid = True Then
            Dim i As Integer

            If txtPw.Text = "" Or txtNPw.Text = "" Or txtCNPw.Text = "" Then
                lblMessaggio.Visible = True
                lblMessaggio.Text = "Attenzione: Tutti i campi devono essere valorizzati!"
                Exit Sub
            End If

            If txtNPw.Text <> txtCNPw.Text Then
                lblMessaggio.Visible = True
                lblMessaggio.Text = "'Nuova Password' e 'Conferma Nuova Password' non coincidono!"
                Exit Sub
            End If

            If ModuloControlloPw(txtNPw.Text) = False Then
                Exit Sub
            End If

            Try
                connData.apri(True)

                par.cmd.CommandText = "select * from siscom_mi.UTENZE_FONDO_SOLIDARIETA where id_zona=" & cmbUtente.SelectedValue & " and password is not null"
                Dim lettore0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore0.Read Then
                    Dim PwMatch As Boolean = par.VerifyHash(txtPw.Text, "SHA512", Trim(par.IfNull(lettore0("PASSWORD"), ""))).ToString()
                    If PwMatch = True Then
                        par.cmd.CommandText = "UPDATE siscom_mi.UTENZE_FONDO_SOLIDARIETA SET PASSWORD='" + par.ComputeHash(txtCNPw.Text, "SHA512", Nothing) + "' WHERE ID=" & par.IfNull(lettore0("ID"), 0)
                        par.cmd.ExecuteNonQuery()
                        lblMessaggio.Text = ""
                        Response.Write("<script>alert('Operazione Effettuata!');</script>")
                    Else
                        lblMessaggio.Visible = True
                        lblMessaggio.Text = "La password attuale non è esatta!"
                    End If
                
                End If
                lettore0.Close()

                connData.chiudi(True)

            Catch EX As Exception
                connData.chiudi(False)
                lblMessaggio.Visible = True
                lblMessaggio.Text = "Operazione annullata a causa di un errore!"
                Session.Item("OPERATORE") = ""
            End Try
        Else
            lblMessaggio.Visible = True
        End If

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
            lblMessaggio.Visible = True
            lblMessaggio.Text = "Non sono ammessi i caratteri < > ' *"
            ModuloControlloPw = False
            myReader.Close()
            par.OracleConn.Close()
            Exit Function
        End If

        If Len(pw) < MinLunghezza Then
            lblMessaggio.Visible = True
            lblMessaggio.Text = "La lunghezza della password deve essere di almeno " & MinLunghezza & " caratteri!"
            ModuloControlloPw = False
            myReader.Close()
            par.OracleConn.Close()
            Exit Function
        End If

        If AlfaNumerica = 1 Then
            If IsNumeric(pw) = True Then
                lblMessaggio.Visible = True
                lblMessaggio.Text = "La password deve contenere caratteri e numeri!"
                ModuloControlloPw = False
                myReader.Close()
                par.OracleConn.Close()
                Exit Function
            End If

            If InStr(1, pw, "0") = 0 And InStr(1, pw, "1") = 0 And InStr(1, pw, "2") = 0 And InStr(1, pw, "3") = 0 And InStr(1, pw, "4") = 0 And InStr(1, pw, "5") = 0 And InStr(1, pw, "6") = 0 And InStr(1, pw, "7") = 0 And InStr(1, pw, "8") = 0 And InStr(1, pw, "9") = 0 Then
                lblMessaggio.Visible = True
                lblMessaggio.Text = "La password deve contenere caratteri e numeri!"
                ModuloControlloPw = False
                myReader.Close()
                par.OracleConn.Close()
                Exit Function
            End If
        End If
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

    Protected Sub cmbUtente_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbUtente.SelectedIndexChanged
        txtCNPw.Text = ""
        txtNPw.Text = ""
        txtPw.Text = ""
        caricaPassword(cmbUtente.SelectedValue)
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>location.href='../AMMSEPA/pagina_home.aspx';</script>")
    End Sub

    Protected Sub imgAzzera_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgAzzera.Click
        If cmbUtente.SelectedValue <> -1 Then
            Try
                connData.apri(True)

                par.cmd.CommandText = "update siscom_mi.UTENZE_FONDO_SOLIDARIETA set password='" & par.ComputeHash("SEPA", "SHA512", Nothing) & "' where id_zona=" & cmbUtente.SelectedValue
                par.cmd.ExecuteNonQuery()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")

                connData.chiudi(True)
            Catch ex As Exception
                connData.chiudi(False)
                lblMessaggio.Visible = True
                lblMessaggio.Text = ex.Message
            End Try
        Else
            Response.Write("<script>alert('Nessun utente è stato selezionato!');</script>")
        End If
    End Sub
End Class
