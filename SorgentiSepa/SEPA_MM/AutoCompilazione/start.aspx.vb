
Partial Class AutoCompilazione_start
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim Str As String
    Dim da_fare As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try

                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select * from PARAMETER where descrizione='CARICAMENTO AUTO DOMANDE'"
                Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myRec.Read Then
                    If myRec("valore") = "0" Then
                        da_fare = "NO"
                    End If
                Else
                    da_fare = "NO"
                End If
                myRec.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                If da_fare = "NO" Then
                    Response.Redirect("NonDisponibile.aspx")
                End If


            Catch ex As Exception

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("ERRORE:" & ex.Message)
            End Try
        End If
    End Sub

    Protected Sub btnValidaCF_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidaCF.Click
        Try

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

            If par.ControllaCFNomeCognome(UCase(txtCF.Text), UCase(txtCognome.Text), UCase(txtNome.Text)) = False Then
                Label5.Visible = True
                Label5.Text = "Cognome e Nome non coincidono con il codice fiscale!"
                Exit Sub
            Else
                Label5.Visible = False
            End If

            If txtNumero.Text = "" Then
                lblErrore1.Visible = True
                lblErrore1.Text = "Inserire il numero dei componenti"
                Exit Sub
            Else
                lblErrore1.Visible = False
            End If

            If Val(txtNumero.Text) <= 0 Then
                lblErrore1.Visible = True
                lblErrore1.Text = "Inserire un numero maggiore o uguale a 1"
                Exit Sub
            Else
                lblErrore1.Visible = False
            End If

            If Val(txtNumero.Text) > 10 Then
                lblErrore1.Visible = True
                lblErrore1.Text = "Inserire un numero minore o uguale a 10"
                Exit Sub
            Else
                lblErrore1.Visible = False
            End If

            par.OracleConn.Open()


            par.SettaCommand(par)
            par.cmd.CommandText = "select * from comp_nucleo where cod_fiscale='" & UCase(txtCF.Text) & "'"
            Dim myRec As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myRec.Read Then
                lblErroreGen.Visible = True
                lblErroreGen.Text = "Attenzione...Il codice fiscale inserito è già presente nei nostri archivi. Si prega di rivolgersi presso il Comune di Milano"
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            Else
                lblErroreGen.Visible = False
            End If

            par.cmd.CommandText = "select * from comp_nucleo_web where cod_fiscale='" & UCase(txtCF.Text) & "'"
            myRec = par.cmd.ExecuteReader()
            If myRec.Read Then
                lblErroreGen.Visible = True
                lblErroreGen.Text = "Attenzione...è stata già inserita una Domanda con questo codice fiscale. Per visualizzare nuovamente la Domanda, utilizzare la funzione --Recupera il numero di una Domanda inserita.-- nel menù a sinistra."
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            Else
                lblErroreGen.Visible = False
            End If



            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("FATTO", "0")
            Response.Redirect("Default.aspx?A=" & par.Cripta(UCase(txtCF.Text)) & "&B=" & txtNumero.Text - 1 & "&C=" & par.Cripta(txtCognome.Text) & "&D=" & par.Cripta(txtNome.Text))

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", ex.Message)
            Response.Write("<script>top.location.href='Errore.aspx';</script>")
        End Try
    End Sub
End Class
