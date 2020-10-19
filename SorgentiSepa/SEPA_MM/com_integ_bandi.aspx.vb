
Partial Class com_integ
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
            txtRiga.value = par.Elimina160(Request.QueryString("RI"))

            If txtOperazione.Text = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
                cmbContributi.Items.FindByText(Trim(par.Elimina160(Request.QueryString("CONT")))).Selected = True
                txtImporto.Text = par.Elimina160(Request.QueryString("IM"))
            Else
                txtImporto.Text = "0"
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
        L3.Visible = False

        If cmbContributi.SelectedItem.Value = "-1" Then
            L3.Visible = True
            L3.Text = "(Specificare Tipo)"
            Exit Sub
        End If

        If par.IfEmpty(txtImporto.Text, "") = "" Then
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If IsNumeric(txtImporto.Text) = False Then
            L3.Visible = True
            L3.Text = "(Valore Numerico)"
            Exit Sub
        End If

        If InStr(txtImporto.Text, ".") > 0 Then
            L3.Visible = True
            L3.Text = "(Valore interi)"
            Exit Sub
        End If

        If InStr(txtImporto.Text, ",") > 0 Then
            L3.Visible = True
            L3.Text = "(Valore interi)"
            Exit Sub
        End If

        If Val(txtImporto.Text) < 0 Then
            L3.Visible = True
            L3.Text = "(Valori superiori o uguali a 0)"
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Value
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 45) & " " & par.MiaFormat(cmbContributi.SelectedItem.Text, 24) & " " & par.MiaFormat(Val(txtImporto.Text), 8) & ",00 "

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub
End Class
