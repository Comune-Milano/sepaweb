
Partial Class FSA_com_patrimonio
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
            txtABI.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtCAB.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            'txtCIN.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtInter.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            txtABI.Text = par.Elimina160(Request.QueryString("ABI"))
            'txtCAB.Text = par.Elimina160(Request.QueryString("CAB"))
            'txtCIN.Text = par.Elimina160(Request.QueryString("CIN"))
            txtInter.Text = par.Elimina160(Request.QueryString("INT"))
            txtImporto.Text = par.Elimina160(Request.QueryString("IMP"))

            If txtOperazione.Text = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
            Else
                txtABI.Text = "000000000000000000000000000"
                'txtCAB.Text = "00000"
                'txtCIN.Text = "0"
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
        'If txtCAB.Text = "" Then
        '    L1.Visible = True
        '    L1.Text = "(Valorizare CAB)"
        '    Exit Sub
        'Else
        '    If Len(txtCAB.Text) <> 5 Then
        '        L1.Visible = True
        '        L1.Text = "(ABI/CAB 5 caratteri)"
        '        Exit Sub
        '    Else
        '        L1.Visible = False
        '    End If
        'End If

        If txtABI.Text = "" Then
            L1.Visible = True
            L1.Text = "(Valorizare)"
            Exit Sub
        Else
            If Len(txtABI.Text) <> 27 Then
                L1.Visible = True
                L1.Text = "(IBAN 27 caratteri)"
                Exit Sub
            Else
                L1.Visible = False
            End If
        End If

        'If txtCIN.Text = "" Then
        '    L1.Visible = True
        '    L1.Text = "(Valorizare CIN)"
        '    Exit Sub
        'Else
        '    L1.Visible = False
        'End If


        If txtInter.Text = "" Then
            L2.Visible = True
        Else
            L2.Visible = False
        End If

        If IsNumeric(txtImporto.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If CDbl(txtImporto.Text) >= 0 Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        If InStr(txtImporto.Text, ".") = 0 Then
            L3.Visible = False
            If InStr(txtImporto.Text, ",") = 0 Then
                L3.Visible = False
            Else
                L3.Visible = True
                L3.Text = "(Valore interi)"
            End If
        Else
            L3.Visible = True
            L3.Text = "(Valore interi)"
        End If

        If L1.Visible = True Or L2.Visible = True Or L3.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(txtABI.Text, 27) & " " & par.MiaFormat(txtInter.Text, 16) & " " & par.MiaFormat(txtImporto.Text, 8) & ",00 "

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub
End Class
