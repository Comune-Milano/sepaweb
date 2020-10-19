
Partial Class VSA_com_uscita
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack = True Then

            'MotiviUscita()
            Dim TESTO As String

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataNasc.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataUscita.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtDataNasc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataUscita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            TESTO = par.Elimina160(Request.QueryString("COGNOME"))
            If txtOperazione.Text = "1" Then
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 25))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 25))
                txtDataNasc.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))
            End If

            txtCognome.Enabled = False
            txtNome.Enabled = False
            txtCF.Enabled = False
            txtDataNasc.Enabled = False

        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        
        If Len(txtDataUscita.Text) <> 10 Then
            lblValorizzaUsc.Visible = True
            lblValorizzaUsc.Text = "(Data non valida (10 car.))"
            Exit Sub
        Else
            lblValorizzaUsc.Visible = False
        End If

        If IsDate(txtDataUscita.Text) = False Then
            lblValorizzaUsc.Visible = True
            lblValorizzaUsc.Text = "(Data non valida)"
            Exit Sub
        Else
            lblValorizzaUsc.Visible = False
        End If

        If cmbMotivoUscita.SelectedValue = "-1" Then
            L5.Visible = True
            Exit Sub
        Else
            L5.Visible = False
        End If


       
        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNasc.Text, 10) & " " & par.MiaFormat(txtCF.Text, 21) & " " & cmbMotivoUscita.SelectedValue & " " & txtDataUscita.Text


        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()

    End Sub

    Private Sub MotiviUscita()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbMotivoUscita.Items.Add(New ListItem("- seleziona -", -1))
            par.cmd.CommandText = "select * from t_causali_domanda_vsa where id_motivo = 1"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                cmbMotivoUscita.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("cod"), -1)))
            End While
            lettore.Close()

            cmbMotivoUscita.Items.Add(New ListItem("altro", -2))

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
