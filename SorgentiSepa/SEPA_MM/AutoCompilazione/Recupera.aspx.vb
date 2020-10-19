
Partial Class Recupera
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnValidaCF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidaCF.Click

        Try

            Dim idDic As Long = 0
            Dim idDom As Long = 0
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
                par.cmd.CommandText = "select id,NOME_FILE from domande_bando_web where id_dichiarazione=" & idDic & " and upper(indirizzo_mail)='" & UCase(txtmail.Text) & "'"
                myRec = par.cmd.ExecuteReader()
                If myRec.HasRows = False Then
                    lblErroreGen.Visible = True
                    lblErroreGen.Text = "Attenzione...Nessuna Domanda con questo indirizzo e-mail."
                    par.OracleConn.Close()
                    Exit Sub
                Else
                    If myRec.Read Then
                        idDom = myRec("id")
                        NOME_FILE = myRec("NOME_FILE")
                        lblErroreGen.Visible = False
                    End If
                End If
            End If
            myRec.Close()

            If idDom <> 0 Then
                Label2.Visible = True
                Label2.Text = "Il numero di Domanda è: " & Format(idDom, "0000000000")
                hp.Visible = True
                hp.NavigateUrl = "../ALLEGATI/DOMANDE/" & NOME_FILE
                'hp.NavigateUrl = Server.MapPath("DOMANDE\") & NOME_FILE
            End If

            par.OracleConn.Close()

        Catch ex As Exception
            par.OracleConn.Close()
            lblErroreGen.Visible = True
            lblErroreGen.Text = ex.Message
        End Try

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Redirect("start.aspx")
    End Sub
End Class
