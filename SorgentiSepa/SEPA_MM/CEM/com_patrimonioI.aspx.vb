
Partial Class VSA_com_patrimonioI
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Write("<script></script>")
        If Not IsPostBack Then

            TxtMutuo.Text = par.Elimina160(Request.QueryString("MU"))
            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtPerc.Text = par.Elimina160(Request.QueryString("PER"))
            txtValore.Text = par.Elimina160(Request.QueryString("VAL"))


            If txtOperazione.Text = "1" Then
                cmbResidenza.SelectedIndex = -1
                cmbResidenza.Items.FindByText(par.Elimina160(Request.QueryString("RES"))).Selected = True

                cmbTipo.SelectedIndex = -1
                cmbTipo.Items.FindByText(UCase(par.Elimina160(Request.QueryString("TIPO")))).Selected = True

                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
            Else
                txtValore.Text = "0"
                TxtMutuo.Text = "0"
                txtPerc.Text = "100"
            End If
        End If
    End Sub

    Function Riempi(ByVal testo As String)
        Dim pos As Integer
        Dim Valore1 As String
        Dim Valore2 As String

        pos = 1

        Valore1 = ""
        Valore2 = ""
        Do While pos <= Len(testo)
            If Mid(testo, pos, 1) <> ";" Then
                Valore1 = Valore1 & Mid(testo, pos, 1)
            Else
                pos = pos + 1
                Do While pos <= Len(testo)
                    If Mid(testo, pos, 1) <> "!" Then
                        Valore2 = Valore2 & Mid(testo, pos, 1)
                    Else
                        cmbComponente.Items.Add(New ListItem(Valore1, Valore2))
                        Valore1 = ""
                        Valore2 = ""
                        Exit Do
                    End If
                    pos = pos + 1
                Loop
            End If
            pos = pos + 1
        Loop
    End Function

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        If txtPerc.Text = "" Then
            L1.Visible = True
            L1.Text = "(Valorizzare)"
            Exit Sub
        Else
            L1.Visible = False
        End If
        If txtValore.Text = "" Then
            L2.Visible = True
            L2.Text = "(Valorizzare)"
            Exit Sub
        Else
            L2.Visible = False
        End If

        If TxtMutuo.Text = "" Then
            L3.Visible = True
            L3.Text = "(Valorizzare)"
            Exit Sub
        Else
            L3.Visible = False
        End If

        If Val(TxtMutuo.Text) > Val(txtValore.Text) Then
            L3.Visible = True
            L3.Text = "(Mutuo inferiore a Valore)"
            Exit Sub
        Else
            L3.Visible = False
        End If

        If IsNumeric(txtPerc.Text) = True Then
            L1.Visible = False
        Else
            L1.Visible = True
            L1.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If CDbl(par.PuntiInVirgole(txtPerc.Text)) > 100 Then
            L1.Visible = True
            L1.Text = "(Valore massimo=100%)"
        Else
            L1.Visible = False
        End If

        If IsNumeric(txtValore.Text) = True Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore Numerico)"
            Exit Sub
        End If



        If CDbl(txtValore.Text) >= 0 Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If IsNumeric(TxtMutuo.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If



        If CDbl(TxtMutuo.Text) >= 0 Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If cmbResidenza.SelectedItem.Text = "SI" And cmbTipo.SelectedItem.Text <> "FABBRICATI" Then
            L4.Visible = True
            L4.Text = "(SI solo se FABBRICATI)"
        Else
            L4.Visible = False
        End If

        If InStr(txtValore.Text, ".") = 0 Then
            L2.Visible = False
            If InStr(txtValore.Text, ",") = 0 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore interi)"
            End If
        Else
            L2.Visible = True
            L2.Text = "(Valore interi)"
        End If

        If InStr(TxtMutuo.Text, ".") = 0 Then
            L3.Visible = False
            If InStr(TxtMutuo.Text, ",") = 0 Then
                L3.Visible = False
            Else
                L3.Visible = True
                L3.Text = "(Valore interi)"
            End If
        Else
            L3.Visible = True
            L3.Text = "(Valore interi)"
        End If

        If L4.Visible = True Or L1.Visible = True Or L2.Visible = True Or L3.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbTipo.SelectedItem.Text, 20) & " " & par.MiaFormat(txtPerc.Text, 6) & " " & par.MiaFormat(Val(txtValore.Text), 8) & ",00 " & par.MiaFormat(Val(TxtMutuo.Text), 8) & ",00 " & cmbResidenza.SelectedItem.Text

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub
End Class
