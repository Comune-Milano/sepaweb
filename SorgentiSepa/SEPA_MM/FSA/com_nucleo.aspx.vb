
Partial Class FSA_com_nucleo
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Write("<script></script>")

        If Not IsPostBack = True Then

            Dim TESTO As String

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            txtProgr.Text = par.Elimina160(Request.QueryString("PR"))
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtASL.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtData.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")

            Dim lsiFrutto As New ListItem("NO", "0")
            cmbAcc.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("SI", "1")
            cmbAcc.Items.Add(lsiFrutto)
            cmbAcc.SelectedIndex = -1
            cmbAcc.SelectedItem.Text = "NO"


            TESTO = par.Elimina160(Request.QueryString("COGNOME"))
            TESTO = par.Elimina160(Request.QueryString("PARENTI"))
            If txtOperazione.Text = "1" Then
                txtCognome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("COGNOME"), 1, 25))
                txtNome.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("NOME"), 1, 25))
                txtData.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("DATA"), 1, 10))
                txtCF.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("CF"), 1, 16))

                cmbParenti.SelectedIndex = -1
                If par.Elimina160(par.RicavaTesto(Request.QueryString("PARENTI"), 1, 25)) <> "" Then
                    cmbParenti.Items.FindByText(par.Elimina160(par.RicavaTesto(Request.QueryString("PARENTI"), 1, 25))).Selected = True
                Else
                    cmbParenti.Items.FindByValue("1").Selected = True
                End If
                txtInv.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("INV"), 1, 6))
                txtASL.Text = par.Elimina160(par.RicavaTesto(Request.QueryString("ASL"), 1, 6))
                cmbAcc.SelectedIndex = -1
                If par.Elimina160(par.RicavaTesto(Request.QueryString("ACC"), 1, 2)) <> "" Then
                    cmbAcc.Items.FindByText(par.Elimina160(par.RicavaTesto(Request.QueryString("ACC"), 1, 2))).Selected = True
                Else
                    cmbAcc.Items.FindByValue(0).Selected = True
                End If
                If txtRiga.Text = "0" Then
                    txtCognome.Enabled = False
                    txtNome.Enabled = False
                    txtData.Enabled = False
                    txtCF.Enabled = False
                Else
                    txtCognome.Enabled = True
                    txtNome.Enabled = True
                    txtData.Enabled = True
                    txtCF.Enabled = True
                End If
            End If
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtCognome.Text = "" Then
            L1.Visible = True
        Else
            L1.Visible = False
        End If
        If txtNome.Text = "" Then
            L2.Visible = True
        Else
            L2.Visible = False
        End If

        If Len(txtData.Text) <> 10 Then
            L3.Visible = True
            L3.Text = "(Data non valida (10 car.))"
        Else
            L3.Visible = False
        End If

        If IsDate(txtData.Text) = False Then
            L3.Visible = True
            L3.Text = "(Data non valida)"
        Else
            L3.Visible = False
        End If

        If txtCF.Text = "" Then
            L4.Visible = True
            Exit Sub
        Else
            L4.Visible = False
        End If

        If IsNumeric(txtInv.Text) = True Then
            If CDbl(txtInv.Text) > 100 Then
                L5.Visible = True
                L5.Text = "(Valore massimo=100%)"
            Else
                L5.Visible = False
            End If
        Else
            L5.Visible = True
            L5.Text = "(Valore Numerico)"
        End If

        If cmbAcc.SelectedItem.Text = "SI" And txtInv.Text <> "100" Then
            L7.Visible = True
            L7.Text = "(SI solo se 100%)"
        Else
            L7.Visible = False
        End If

        If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = False Then
            L4.Visible = True
            L4.Text = "(Errato)"
        Else
            L4.Visible = False
        End If

        If L7.Visible = True Or L6.Visible = True Or L5.Visible = True Or L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L4.Visible = True Then

            'Response.Clear()
            'Response.Write("<script>alert('dati errati');</script>")
            'Response.Write("<script>window.close();</script>")
            'Response.End()
            Exit Sub
        End If


        If txtOperazione.Text = "0" Then
            Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat(cmbParenti.SelectedItem.Text, 25) & " " & par.MiaFormat(txtInv.Text, 6) & " " & par.MiaFormat(txtASL.Text, 5) & " " & par.MiaFormat(cmbAcc.SelectedItem.Text, 2)

            If Val(txtInv.Text) = 100 And cmbAcc.SelectedItem.Text = "SI" Then
                Cache(Session.Item("GSpese")) = par.MiaFormat(txtCognome.Text & "," & txtNome.Text, 52) & " "
            Else
                Cache.Remove(Session.Item("GSpese"))
            End If
        Else
            Cache(Session.Item("GRiga")) = txtRiga.Text
            Cache(Session.Item("GLista")) = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtData.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat(cmbParenti.SelectedItem.Text, 25) & " " & par.MiaFormat(txtInv.Text, 6) & " " & par.MiaFormat(txtASL.Text, 5) & " " & par.MiaFormat(cmbAcc.SelectedItem.Text, 2)
            If Val(txtInv.Text) = 100 And cmbAcc.SelectedItem.Text = "SI" Then
                Cache(Session.Item("GSpese")) = par.MiaFormat(txtCognome.Text & "," & txtNome.Text, 52) & " "
            Else
                Cache.Remove(Session.Item("GSpese"))
            End If
        End If
        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub
End Class
