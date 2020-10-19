
Partial Class com_reddito
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

            txtOperazione.Value = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Value = par.Elimina160(Request.QueryString("RI"))
            
            If txtOperazione.Value = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
                cmbComponente.Enabled = False
                cmbTipoReddito.ClearSelection()
                cmbTipoReddito.Items.FindByText(par.Elimina160(Request.QueryString("TIPOR"))).Selected = True
                cmbTipoAgrari.ClearSelection()
                cmbTipoAgrari.Items.FindByText(par.Elimina160(Request.QueryString("TIPOA"))).Selected = True

                txtIRPEF.Text = par.Elimina160(Request.QueryString("IR"))
                txtAGRARI.Text = par.Elimina160(Request.QueryString("AG"))
            Else
                txtIRPEF.Text = "0"
                txtAGRARI.Text = "0"
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
        L1.Visible = False
        L2.Visible = False


        If cmbTipoReddito.SelectedItem.Value <> "-1" Then
            If IsNumeric(txtIRPEF.Text) = False Then
                L1.Visible = True
                L1.Text = "(Valore numerico)"
                Exit Sub
            End If

            If CDbl(txtIRPEF.Text) < 0 Then
                L1.Visible = True
                L1.Text = "(Valore superiori a 0)"
                Exit Sub
            End If

            If InStr(txtIRPEF.Text, ".") > 0 Then
                L1.Visible = True
                L1.Text = "(Valore interi)"
                Exit Sub
            End If

            If InStr(txtIRPEF.Text, ",") > 0 Then
                L1.Visible = True
                L1.Text = "(Valore interi)"
                Exit Sub
            End If

        Else
            txtIRPEF.Text = "0"
        End If

        If cmbTipoAgrari.SelectedItem.Value <> "-1" Then
            If IsNumeric(txtAGRARI.Text) = False Then
                L2.Visible = True
                L2.Text = "(Valore numerico)"
                Exit Sub
            End If

            If CDbl(txtAGRARI.Text) < 0 Then
                L2.Visible = True
                L2.Text = "(Valore superiori a 0)"
                Exit Sub
            End If

            If InStr(txtAGRARI.Text, ".") > 0 Then
                L2.Visible = True
                L2.Text = "(Valore interi)"
            End If

            If InStr(txtAGRARI.Text, ",") > 0 Then
                L2.Visible = True
                L2.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            txtAGRARI.Text = "0"
        End If


        If cmbTipoAgrari.SelectedItem.Value = "-1" And cmbTipoReddito.SelectedItem.Value = "-1" Then
            L1.Visible = True
            L1.Text = "(Specificare un valore)"
            L2.Visible = True
            L2.Text = "(Specificare un valore)"
        End If

        If L1.Visible = True Or L2.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Value
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 35) & " " & par.MiaFormat(cmbTipoReddito.SelectedItem.Text, 25) & " " & par.MiaFormat(txtIRPEF.Text, 8) & ",00" & " " & par.MiaFormat(cmbTipoAgrari.SelectedItem.Text, 25) & " " & par.MiaFormat(txtAGRARI.Text, 8) & ",00"

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
    End Sub
End Class
