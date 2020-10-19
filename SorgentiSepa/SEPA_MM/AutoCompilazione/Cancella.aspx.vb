
Partial Class AutoCompilazione_Cancella
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim data_pg As String

    Public Property idDom() As Long
        Get
            If Not (ViewState("par_idDom") Is Nothing) Then
                Return CLng(ViewState("par_idDom"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idDom") = value
        End Set

    End Property

    Public Property idDic() As Long
        Get
            If Not (ViewState("par_idDic") Is Nothing) Then
                Return CLng(ViewState("par_idDic"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idDic") = value
        End Set

    End Property

    Protected Sub btnValidaCF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidaCF.Click
        Try



            Dim NOME_FILE As String = ""

            If Len(txtCF.Text) <> 16 Then
                lblErrore.Visible = True
                lblErrore.Text = "Lunghezza non valida (16 caratteri)"
                Exit Sub
            Else
                lblErrore.Visible = False
            End If

            If par.ControllaCF(UCase(txtCF.Text)) = False Then
                lblErrore.Visible = True
                lblErrore.Text = "Codice Fiscale non valido!"
                Exit Sub
            Else
                lblErrore.Visible = False
            End If

            If txtMail.Text = "" Or InStr(txtMail.Text, "@") = 0 Then
                lblErrore.Visible = True
                lblErrore.Text = "Indirizzo e-mail non valido"
                Exit Sub
            Else
                lblErrore.Visible = False
            End If


            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "select * from comp_nucleo_web where cod_fiscale='" & UCase(txtCF.Text) & "'"
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myRec.HasRows = False Then
                lblErroreGen.Visible = True
                lblErroreGen.Text = "Attenzione...Nessuna Domanda con questo codice fiscale."
                par.OracleConn.Close()
                Exit Sub
            Else
                If myRec.Read Then
                    idDic = myRec("id_dichiarazione")
                    lblErroreGen.Visible = False
                End If
            End If
            myRec.Close()

            If idDic <> 0 Then
                par.cmd.CommandText = "select id,NOME_FILE,data_pg from domande_bando_web where id=" & Val(txtNumero.Text) & " and upper(indirizzo_mail)='" & par.PulisciStrSql(UCase(txtMail.Text)) & "'"
                myRec = par.cmd.ExecuteReader()
                If myRec.HasRows = False Then
                    lblErroreGen.Visible = True
                    lblErroreGen.Text = "Attenzione...Nessuna Domanda con questo numero/indirizzo e-mail."
                    par.OracleConn.Close()
                    Exit Sub
                Else
                    If myRec.Read Then
                        idDom = myRec("id")
                        data_pg = myRec("data_pg")
                        NOME_FILE = myRec("NOME_FILE")
                        lblErroreGen.Visible = False
                    End If
                End If
            End If
            myRec.Close()

            If idDom <> 0 Then
                Label2.Visible = True
                Label2.Text = "Domanda " & Format(idDom, "0000000000") & " del " & par.FormattaData(data_pg)
                hp.Visible = True

            End If

            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
            lblErroreGen.Visible = True
            lblErroreGen.Text = ex.Message
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("start.aspx")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Property smtp() As String
        Get
            If Not (ViewState("par_smtp") Is Nothing) Then
                Return CStr(ViewState("par_smtp"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_smtp") = value
        End Set

    End Property

    Public Property PWUtenteMail() As String
        Get
            If Not (ViewState("par_pwUtenteMail") Is Nothing) Then
                Return CStr(ViewState("par_pwUtenteMail"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_pwUtenteMail") = value
        End Set

    End Property

    Public Property MittenteMail() As String
        Get
            If Not (ViewState("par_MittenteMail") Is Nothing) Then
                Return CStr(ViewState("par_MittenteMail"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_MittenteMail") = value
        End Set

    End Property

    Public Property UtenteMail() As String
        Get
            If Not (ViewState("par_UtenteMail") Is Nothing) Then
                Return CStr(ViewState("par_UtenteMail"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_UtenteMail") = value
        End Set

    End Property

    Protected Sub hp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles hp.Click
        Try
            Dim nome As String = ""
            Dim codicefiscale As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            UtenteMail = ""
            PWUtenteMail = ""
            smtp = ""
            MittenteMail = ""


            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=57"
            Dim myReader111 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                UtenteMail = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=58"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                PWUtenteMail = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=59"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                smtp = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "SELECT valore FROM parameter WHERE id=60"
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                MittenteMail = par.IfNull(myReader111(0), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "select * from comp_nucleo_web where progr=0 and id_dichiarazione=" & idDic
            myReader111 = par.cmd.ExecuteReader()
            If myReader111.Read() Then
                nome = par.IfNull(myReader111("cognome"), "") & " " & par.IfNull(myReader111("nome"), "")
                codicefiscale = par.IfNull(myReader111("cod_fiscale"), "")
            End If
            myReader111.Close()

            par.cmd.CommandText = "delete from comp_nucleo_web where id_dichiarazione=" & idDic
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "delete from dichiarazioni_web where id=" & idDic
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "delete from domande_bando_web where id=" & idDom
            par.cmd.ExecuteNonQuery()

            Label2.Text = "La Domanda è stata cancellata!"
            hp.Visible = False
            hp1.Visible = True

            If UtenteMail <> "" Then
                Dim mail As New System.Net.Mail.MailMessage
                Dim smtpClient = New System.Net.Mail.SmtpClient(smtp)
                mail.From = New System.Net.Mail.MailAddress(MittenteMail)
                mail.To.Add(New System.Net.Mail.MailAddress(txtMail.Text))

                'mail.Attachments.Add(Allegato)
                mail.Subject = "Cancellazione Domanda di Bando Erp inserita on-line"

                mail.Body = "Si conferma l'avvenuta cancellazione in data " & Format(Now, "dd/MM/yyyy") & " dei dati inseriti mediante la procedura per la presentazione on-line della domanda di assegnazione di alloggio E.R.P. effettuata a nome di:" & vbCrLf & vbCrLf & "Richiedente:" & nome & vbCrLf & "Codice Fiscale:" & codicefiscale & vbCrLf & "e-mail:" & txtMail.Text & vbCrLf & "N.Registrazione:" & Format(idDom, "0000000000") & vbCrLf & vbCrLf & vbCrLf & "COMUNE DI MILANO" & vbCrLf & "DIREZIONE CENTRALE CASA" & vbCrLf & "SETTORE ASSEGNAZIONE ALLOGGI DI E.R.P." & vbCrLf & "SEZIONE(BANDI)" & vbCrLf & "Via Pirelli, 39 - MILANO"

                'mail.Body = "La informiano che la domanda N. " & Format(idDom, "0000000000") & " è stata cancellata dai nostri sistemi come da Lei richiesto."
                smtpClient.Credentials = New System.Net.NetworkCredential(UtenteMail, PWUtenteMail)
                smtpClient.Send(mail)
                mail.Dispose()
            End If

        Catch ex As Exception

            par.OracleConn.Close()
            lblErroreGen.Visible = True
            lblErroreGen.Text = ex.Message
        End Try



    End Sub

 
    Protected Sub hp1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles hp1.Click
        Response.Redirect("start.aspx")
    End Sub
End Class
