
Partial Class Prenotazione
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub btnUscita_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUscita.Click
        Response.Write("<script>window.close();</script>")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            Label6.Visible = False
            btnMemo.Attributes.Add("onclick", "this.style.visibility='hidden'")
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtTel.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        End If
    End Sub

    Protected Sub btnMemo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMemo.Click
        Try
            Label6.Visible = False
            If txtCognome.Text = "" Then
                Response.Write("<script>alert('Inserire il Cognome!');</script>")
                Exit Sub
            End If

            If txtNome.Text = "" Then
                Response.Write("<script>alert('Inserire il Nome!');</script>")
                Exit Sub
            End If

            If txtCF.Text = "" Then
                Response.Write("<script>alert('Inserire il Codice Fiscale!');</script>")
                Exit Sub
            End If

            If txtTel.Text = "" Then
                Response.Write("<script>alert('Inserire il recapito telefonico!');</script>")
                Exit Sub
            End If

            If par.ControllaCF(UCase(txtCF.Text)) = False Then
                Response.Write("<script>alert('Codice Fiscale Errato!');</script>")
                Exit Sub
            End If

            par.OracleConn.Open()
            par.SettaCommand(par)

            If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                par.cmd.CommandText = "SELECT DICHIARAZIONI.ID FROM DICHIARAZIONI,COMP_NUCLEO WHERE DICHIARAZIONI.ID=COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO.COD_FISCALE='" & UCase(txtCF.Text) & "')"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read() Then
                    CFLABEL.Visible = True
                    Panel1.Visible = True
                    LSI.Visible = True
                    LNO.Visible = True
                    Label5.Visible = True
                    CFLABEL.Text = UCase(txtCF.Text) & ": TROVATO IN ALTRE DICHIARAZIONI!"
                    CFLABEL.Attributes.Add("OnClick", "javascript:window.open('correlazioni.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=-1','Correlazioni','top=30,left=30,width=600,height=400');")
                Else
                    CFLABEL.Text = ""
                    CFLABEL.Visible = False
                    CFLABEL.Visible = False
                    Panel1.Visible = False
                    LSI.Visible = False
                    LNO.Visible = False
                    Label5.Visible = False
                    txtCognome.Enabled = False
                    txtNome.Enabled = False
                    txtCF.Enabled = False
                    txtTel.Enabled = False

                    'par.OracleConn.Close()
                    'par.OracleConn.Dispose()
                    Memorizza()
                End If
                myReader.Close()
            Else
                Response.Write("<script>alert('Codice Fiscale Errato!');</script>")
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Exit Sub
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Sub

    Private Function Memorizza()
        Dim scriptblock As String
        Try
            'par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "INSERT INTO DOMANDE_PRENOTAZIONI (ID,COGNOME,NOME,CF,TELEFONO,DATA_INS,ID_OPERATORE,ENTE) VALUES (SEQ_DOMANDE_PRENOTAZIONI.NEXTVAL,'" & par.PulisciStrSql(txtCognome.Text) & "','" & par.PulisciStrSql(txtNome.Text) & "','" & par.PulisciStrSql(txtCF.Text) & "','" & par.PulisciStrSql(txtTel.Text) & "','" & Format(Now, "yyyyMMddHHmm") & "'," & Session.Item("ID_OPERATORE") & ",'" & Session.Item("CAAF") & "')"
            par.cmd.ExecuteNonQuery()

            btnMemo.Visible = False
            btnRicevuta.Visible = True

            par.OracleConn.Close()
            par.OracleConn.Dispose()

            scriptblock = "<script language='javascript' type='text/javascript'>" _
            & "window.open('RicevutaPrenotazione.aspx?V1=" & par.VaroleDaPassare(txtCognome.Text) & "&V2=" & par.VaroleDaPassare(txtNome.Text) & "&V3=" & par.VaroleDaPassare(txtCF.Text) & "&V4=" & par.VaroleDaPassare(txtTel.Text) & "','Ricevuta');" _
            & "</script>"
            If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            Label6.Visible = True
            Label6.Text = EX1.Message
            par.OracleConn.Close()
            par.OracleConn.Dispose()

        Catch ex As Exception
            Label6.Visible = True
            Label6.Text = ex.Message
            par.OracleConn.Close()
            par.OracleConn.Dispose()
        End Try
    End Function

    Protected Sub LSI_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LSI.Click
        CFLABEL.Text = ""
        CFLABEL.Visible = False
        CFLABEL.Visible = False
        Panel1.Visible = False
        LSI.Visible = False
        LNO.Visible = False
        Label5.Visible = False
        par.OracleConn.Open()
        Memorizza()
        txtCognome.Enabled = False
        txtNome.Enabled = False
        txtCF.Enabled = False
        txtTel.Enabled = False
    End Sub

    Protected Sub LNO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LNO.Click
        CFLABEL.Text = ""
        CFLABEL.Visible = False
        CFLABEL.Visible = False
        Panel1.Visible = False
        LSI.Visible = False
        LNO.Visible = False
        Label5.Visible = False
    End Sub

    Protected Sub btnRicevuta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRicevuta.Click
        Response.Write("<script>window.open('RicevutaPrenotazione.aspx?V1=" & par.VaroleDaPassare(txtCognome.Text) & "&V2=" & par.VaroleDaPassare(txtNome.Text) & "&V3=" & par.VaroleDaPassare(txtCF.Text) & "&V4=" & par.VaroleDaPassare(txtTel.Text) & "','Ricevuta');</script>")
    End Sub
End Class
