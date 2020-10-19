
Partial Class com_patrimonio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Write("<script></script>")
        If Not IsPostBack Then
           
            txtIban.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            txtOperazione.Value = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)
            txtRiga.Value = par.Elimina160(Request.QueryString("RI"))

            If txtOperazione.Value = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
                nuovo.Value = "1"

                Select Case par.Elimina160(Request.QueryString("TIPO"))
                    Case "SALDO AL 31/12"
                        cmbScegliTipo.ClearSelection()
                        cmbScegliTipo.Items.FindByValue("0").Selected = True
                        txtSaldo.Text = par.Elimina160(Request.QueryString("IMP"))
                        tipologia.Value = "0"
                    Case "TITOLI AL 31/12-ASS. VITA"
                        cmbScegliTipo.ClearSelection()
                        cmbScegliTipo.Items.FindByValue("1").Selected = True
                        txtValTitoli.Text = par.Elimina160(Request.QueryString("IMP"))
                        tipologia.Value = "1"
                    Case "PARTECIPAZIONI AL 31/12"
                        cmbScegliTipo.ClearSelection()
                        cmbScegliTipo.Items.FindByValue("2").Selected = True
                        txtValPartecipazioni.Text = par.Elimina160(Request.QueryString("IMP"))
                        tipologia.Value = "2"
                End Select

                Select Case par.Elimina160(Request.QueryString("SOTTOTIPO"))
                    Case "AUTOCERTIFICATO"
                        sottotipologia.Value = "0"
                        txtIban.Text = par.Elimina160(Request.QueryString("IBAN"))
                    Case "SALDO CONTO"
                        txtIban.Text = par.Elimina160(Request.QueryString("IBAN"))
                        sottotipologia.Value = "1"
                    Case "SALDO TITOLI"
                        txtIban.Text = par.Elimina160(Request.QueryString("IBAN"))
                        sottotipologia.Value = "2"
                    Case "SALDO PREMI VERSATI"
                        sottotipologia.Value = "3"
                    Case "DOCUMENTAZIONE CCIAA"
                        sottotipologia.Value = "4"
                    Case "730"
                        sottotipologia.Value = "5"
                    Case "UNICO"
                        sottotipologia.Value = "6"
                    Case "BILANCIO"
                        sottotipologia.Value = "7"
                End Select

                txtIntermediario.Text = par.Elimina160(Request.QueryString("INTERMEDIARIO"))

            Else

                tipologia.Value = "0"
                sottotipologia.Value = "0"
                nuovo.Value = "0"
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
        Dim TestoDaInserire As String = ""
        Dim SottoTipo As String = ""
        Dim IMPORTO As String = ""
        Dim IBAN As String = ""


        L3.Visible = False
        L1.Visible = False
        L2.Visible = False
        Label14.Visible = False

        Select Case cmbScegliTipo.SelectedItem.Value
            Case 0
                Select Case sottotipologia.Value
                    Case "0"
                        If IsNumeric(txtSaldo.Text) = False Then
                            L3.Visible = True
                            L3.Text = "(Valore Numerico)"
                            Exit Sub
                        End If
                        If CDbl(txtSaldo.Text) < 0 Then
                            L3.Visible = True
                            L3.Text = "(Valore superiori o uguali a 0)"
                            Exit Sub
                        End If
                        If InStr(txtSaldo.Text, ".") > 0 Then
                            L3.Visible = True
                            L3.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        If InStr(txtSaldo.Text, ",") > 0 Then
                            L3.Visible = True
                            L3.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        SottoTipo = "AUTOCERTIFICATO"
                        IMPORTO = txtSaldo.Text
                        IBAN = UCase(txtIban.Text)

                    Case "1"
                        If IsNumeric(txtSaldo.Text) = False Then
                            L3.Visible = True
                            L3.Text = "(Valore Numerico)"
                            Exit Sub
                        End If
                        If CDbl(txtSaldo.Text) < 0 Then
                            L3.Visible = True
                            L3.Text = "(Valore superiori o uguali a 0)"
                            Exit Sub
                        End If
                        If InStr(txtSaldo.Text, ".") > 0 Then
                            L3.Visible = True
                            L3.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        If InStr(txtSaldo.Text, ",") > 0 Then
                            L3.Visible = True
                            L3.Text = "(Valore interi)"
                            Exit Sub
                        End If

                        If txtIban.Text = "" Then
                            L1.Visible = True
                            L1.Text = "(Valorizare)"
                            Exit Sub
                        Else
                            If Len(txtIban.Text) <> 27 Then
                                L1.Visible = True
                                L1.Text = "(IBAN 27 caratteri)"
                                Exit Sub
                            End If
                        End If
                        SottoTipo = "SALDO CONTO"
                        IMPORTO = txtSaldo.Text
                        IBAN = UCase(txtIban.Text)
                End Select
                TestoDaInserire = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbScegliTipo.SelectedItem.Text, 27) & " " & par.MiaFormat(SottoTipo, 25) & " " & par.MiaFormat(IMPORTO, 10) & ",00 " & par.MiaFormat(IBAN, 27) & " " & par.MiaFormat(txtIntermediario.Text, 50)
            Case 1
                Select Case sottotipologia.Value
                    Case "0"
                        If IsNumeric(txtValTitoli.Text) = False Then
                            L2.Visible = True
                            L2.Text = "(Valore Numerico)"
                            Exit Sub
                        End If
                        If CDbl(txtValTitoli.Text) < 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore superiori o uguali a 0)"
                            Exit Sub
                        End If
                        If InStr(txtValTitoli.Text, ".") > 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        If InStr(txtValTitoli.Text, ",") > 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        SottoTipo = "AUTOCERTIFICATO"
                        IMPORTO = txtValTitoli.Text
                        IBAN = UCase(txtIban.Text)
                    Case "2"
                        If IsNumeric(txtValTitoli.Text) = False Then
                            L2.Visible = True
                            L2.Text = "(Valore Numerico)"
                            Exit Sub
                        End If
                        If CDbl(txtValTitoli.Text) < 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore superiori o uguali a 0)"
                            Exit Sub
                        End If
                        If InStr(txtValTitoli.Text, ".") > 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        If InStr(txtValTitoli.Text, ",") > 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore interi)"
                            Exit Sub
                        End If

                        If txtIban.Text = "" Then
                            L1.Visible = True
                            L1.Text = "(Valorizare)"
                            Exit Sub
                        Else
                            If Len(txtIban.Text) <> 27 Then
                                L1.Visible = True
                                L1.Text = "(IBAN 27 caratteri)"
                                Exit Sub
                            End If
                        End If

                        SottoTipo = "SALDO TITOLI"
                        IMPORTO = txtValTitoli.Text
                        IBAN = UCase(txtIban.Text)
                    Case "3"
                        If IsNumeric(txtValTitoli.Text) = False Then
                            L2.Visible = True
                            L2.Text = "(Valore Numerico)"
                            Exit Sub
                        End If
                        If CDbl(txtValTitoli.Text) < 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore superiori o uguali a 0)"
                            Exit Sub
                        End If
                        If InStr(txtValTitoli.Text, ".") > 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        If InStr(txtValTitoli.Text, ",") > 0 Then
                            L2.Visible = True
                            L2.Text = "(Valore interi)"
                            Exit Sub
                        End If
                        SottoTipo = "SALDO PREMI VERSATI"
                        IMPORTO = txtValTitoli.Text
                        IBAN = ""
                End Select

                TestoDaInserire = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbScegliTipo.SelectedItem.Text, 27) & " " & par.MiaFormat(SottoTipo, 25) & " " & par.MiaFormat(IMPORTO, 10) & ",00 " & par.MiaFormat(IBAN, 27) & " " & par.MiaFormat(txtIntermediario.Text, 50)
            Case 2
                Select Case sottotipologia.Value
                    Case "0"

                        SottoTipo = "AUTOCERTIFICATO"
                    Case "4"

                        SottoTipo = "DOCUMENTAZIONE CCIAA"
                    Case "5"

                        SottoTipo = "730"
                    Case "6"

                        SottoTipo = "UNICO"
                    Case "7"

                        SottoTipo = "BILANCIO"

                End Select
                If IsNumeric(txtValPartecipazioni.Text) = False Then
                    Label14.Visible = True
                    Label14.Text = "(Valore Numerico)"
                    Exit Sub
                End If
                If CDbl(txtValPartecipazioni.Text) < 0 Then
                    Label14.Visible = True
                    Label14.Text = "(Valore superiori o uguali a 0)"
                    Exit Sub
                End If
                If InStr(txtValPartecipazioni.Text, ".") > 0 Then
                    Label14.Visible = True
                    Label14.Text = "(Valore interi)"
                    Exit Sub
                End If
                If InStr(txtValPartecipazioni.Text, ",") > 0 Then
                    Label14.Visible = True
                    Label14.Text = "(Valore interi)"
                    Exit Sub
                End If
                IMPORTO = txtValPartecipazioni.Text
                IBAN = UCase(txtIban.Text)

                TestoDaInserire = par.MiaFormat(cmbComponente.SelectedItem.Text, 25) & " " & par.MiaFormat(cmbScegliTipo.SelectedItem.Text, 27) & " " & par.MiaFormat(SottoTipo, 25) & " " & par.MiaFormat(IMPORTO, 10) & ",00 " & par.MiaFormat(IBAN, 27) & " " & par.MiaFormat(txtIntermediario.Text, 50)



        End Select

        If cmbScegliTipo.SelectedItem.Value <> 3 Then
            Cache(Session.Item("GRiga")) = txtRiga.Value
            Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
            Cache(Session.Item("GLista")) = TestoDaInserire

            Response.Clear()
            Response.Write("<script>window.close();</script>")
            Response.End()
        End If

    End Sub
End Class
