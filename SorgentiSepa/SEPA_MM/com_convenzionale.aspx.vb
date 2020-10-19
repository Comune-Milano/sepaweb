
Partial Class com_convenzionale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))
            txt1.Text = par.Elimina160(Request.QueryString("1"))
            txt2.Text = par.Elimina160(Request.QueryString("2"))
            Txt3.Text = par.Elimina160(Request.QueryString("3"))
            txt4.Text = par.Elimina160(Request.QueryString("4"))
            If txtOperazione.Text = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
            Else
                txt1.Text = "0"
                txt2.Text = "0"
                Txt3.Text = "0"
                Txt4.Text = "0"

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
        If IsNumeric(txt1.Text) = True Then
            L1.Visible = False
        Else
            L1.Visible = True
            L1.Text = "(Valore numerico)"
            Exit Sub
        End If



        If IsNumeric(txt2.Text) = True Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore numerico)"
            Exit Sub
        End If



        If IsNumeric(Txt3.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            Exit Sub
        End If



        If IsNumeric(txt4.Text) = True Then
            L4.Visible = False
        Else
            L4.Visible = True
            Exit Sub
        End If



        If CDbl(txt1.Text) >= 0 Then
            L1.Visible = False
        Else
            L1.Visible = True
            L1.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If CDbl(txt2.Text) >= 0 Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If CDbl(Txt3.Text) >= 0 Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If CDbl(txt4.Text) >= 0 Then
            L4.Visible = False
        Else
            L4.Visible = True
            L4.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If


        If InStr(txt1.Text, ".") = 0 Then
            L1.Visible = False
            If InStr(txt1.Text, ",") = 0 Then
                L1.Visible = False
            Else
                L1.Visible = True
                L1.Text = "(Valore interi)"
            End If
        Else
            L1.Visible = True
            L1.Text = "(Valore interi)"
        End If

        If InStr(txt2.Text, ".") = 0 Then
            L2.Visible = False
            If InStr(txt2.Text, ",") = 0 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore interi)"
            End If
        Else
            L2.Visible = True
            L2.Text = "(Valore interi)"
        End If

        If InStr(Txt3.Text, ".") = 0 Then
            L3.Visible = False
            If InStr(Txt3.Text, ",") = 0 Then
                L3.Visible = False
            Else
                L3.Visible = True
                L3.Text = "(Valore interi)"
            End If
        Else
            L3.Visible = True
            L3.Text = "(Valore interi)"
        End If

        If InStr(txt4.Text, ".") = 0 Then
            L4.Visible = False
            If InStr(txt4.Text, ",") = 0 Then
                L4.Visible = False
            Else
                L4.Visible = True
                L4.Text = "(Valore interi)"
            End If
        Else
            L4.Visible = True
            L4.Text = "(Valore interi)"
        End If

        If L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L4.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 35) & " " & par.MiaFormat(txt1.Text, 7) & ",00 " & par.MiaFormat(txt2.Text, 7) & ",00" & " " & par.MiaFormat(Txt3.Text, 7) & ",00" & " " & par.MiaFormat(txt4.Text, 7) & ",00"

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub

    Protected Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txt3.TextChanged

    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt4.TextChanged

    End Sub
End Class
