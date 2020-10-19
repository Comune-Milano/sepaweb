
Partial Class ANAUT_com_convenzionale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim COMPONENTE As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        Response.Write("<script></script>")
        If Not IsPostBack = True Then

            txtOperazione.Text = par.Elimina160(Request.QueryString("OP"))
            COMPONENTE = par.Elimina160(Request.QueryString("COMPONENTE"))
            Riempi(COMPONENTE)

            cmbCond.Items.Add(New ListItem("-------", "0"))
            cmbCond.Items.Add(New ListItem("01-Occupato", "1"))
            cmbCond.Items.Add(New ListItem("02-In cerca di prima occupazione", "2"))
            cmbCond.Items.Add(New ListItem("03-Disoccupato", "3"))
            cmbCond.Items.Add(New ListItem("04-Casalinga", "4"))
            cmbCond.Items.Add(New ListItem("05-Studente", "5"))
            cmbCond.Items.Add(New ListItem("06-Infante", "6"))
            cmbCond.Items.Add(New ListItem("07-Pensionato", "7"))
            cmbCond.Items.Add(New ListItem("08-In servizio militare di leva", "8"))
            cmbCond.Items.Add(New ListItem("09-Altra condizione non professionale", "9"))
            cmbCond.Items.Add(New ListItem("10-Varie", "10"))
            cmbCond.Items.Add(New ListItem("11-Lavoro Saltuario", "11"))

            cmbProf.Items.Add(New ListItem("------", "0"))
            cmbProf.Items.Add(New ListItem("01-Dirigente", "1"))
            cmbProf.Items.Add(New ListItem("02-Impiegato", "2"))
            cmbProf.Items.Add(New ListItem("03-Operaio", "3"))
            cmbProf.Items.Add(New ListItem("04-Apprendista", "4"))
            cmbProf.Items.Add(New ListItem("05-Lavoratore a domicilio", "5"))
            cmbProf.Items.Add(New ListItem("06-Militare in carriera", "6"))
            cmbProf.Items.Add(New ListItem("07-Imprenditore", "7"))
            cmbProf.Items.Add(New ListItem("08-Libero professionista", "8"))
            cmbProf.Items.Add(New ListItem("09-Lavoratore in proprio", "9"))
            cmbProf.Items.Add(New ListItem("10-Coadiuvante", "10"))
            cmbProf.Items.Add(New ListItem("11-Titolare di una pensione", "11"))
            cmbProf.Items.Add(New ListItem("12-Titolare di due pensione", "12"))
            cmbProf.Items.Add(New ListItem("13-Titolare di tre pensione", "13"))
            cmbProf.Items.Add(New ListItem("14-Titolare di quattro pensione", "14"))
            cmbProf.Items.Add(New ListItem("15-Aiuti economici", "15"))

            txtRiga.Text = par.Elimina160(Request.QueryString("RI"))

            If txtOperazione.Text = "1" Then
                cmbComponente.Items.FindByValue(par.Elimina160(Request.QueryString("COMP"))).Selected = True
                'cmbCond.Items.FindByValue(par.Elimina160(Trim(Request.QueryString("1")))).Selected = True
                'cmbProf.Items.FindByValue(par.Elimina160(Trim(Request.QueryString("2")))).Selected = True

                cmbCond.Items.FindByValue("0").Selected = True
                cmbProf.Items.FindByValue("0").Selected = True


                txt1.Text = par.Elimina160(Request.QueryString("3"))
                txt2.Text = par.Elimina160(Request.QueryString("4"))
                txt3.Text = par.Elimina160(Request.QueryString("5"))
                txt4.Text = par.Elimina160(Request.QueryString("6"))
                Txt5.Text = par.Elimina160(Request.QueryString("7"))
                txt6.Text = par.Elimina160(Request.QueryString("8"))
                txt7.Text = par.Elimina160(Request.QueryString("9"))
            Else
                txt1.Text = "0"
                txt2.Text = "0"
                txt3.Text = "0"
                txt4.Text = "0"
                Txt5.Text = "0"
                txt6.Text = "0"
                txt7.Text = ""
            End If

        End If
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalva.Click
        ''1
        If IsNumeric(txt1.Text) = True Then
            L1.Visible = False
        Else
            L1.Visible = True
            L1.Text = "(Valore numerico)"
            Exit Sub
        End If



        If CDbl(txt1.Text) >= 0 Then
            L1.Visible = False
        Else
            L1.Visible = True
            L1.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If

        ''2
        If IsNumeric(txt2.Text) = True Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore numerico)"
            Exit Sub
        End If


        If CDbl(txt2.Text) >= 0 Then
            L2.Visible = False
        Else
            L2.Visible = True
            L2.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If


        ''3
        If IsNumeric(txt3.Text) = True Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore numerico)"
            Exit Sub
        End If


        If CDbl(txt3.Text) >= 0 Then
            L3.Visible = False
        Else
            L3.Visible = True
            L3.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If


        ''4
        If IsNumeric(txt4.Text) = True Then
            L4.Visible = False
        Else
            L4.Visible = True
            L4.Text = "(Valore numerico)"
            Exit Sub
        End If


        If CDbl(txt4.Text) >= 0 Then
            L4.Visible = False
        Else
            L4.Visible = True
            L4.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If


        ''5
        If IsNumeric(Txt5.Text) = True Then
            L5.Visible = False
        Else
            L5.Visible = True
            L5.Text = "(Valore numerico)"
            Exit Sub
        End If


        If CDbl(Txt5.Text) >= 0 Then
            L5.Visible = False
        Else
            L5.Visible = True
            L5.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If


        ''6
        If IsNumeric(txt6.Text) = True Then
            L6.Visible = False
        Else
            L6.Visible = True
            L6.Text = "(Valore numerico)"
            Exit Sub
        End If



        If CDbl(txt6.Text) >= 0 Then
            L6.Visible = False
        Else
            L6.Visible = True
            L6.Text = "(Valore superiori o uguali a 0)"
            Exit Sub
        End If



        ' ''7
        'If IsNumeric(txt7.Text) = True Then
        '    L7.Visible = False
        'Else
        '    L7.Visible = True
        '    L7.Text = "(Valore numerico)"
        '    Exit Sub
        'End If


        'If CDbl(txt7.Text) >= 0 Then
        '    L7.Visible = False
        'Else
        '    L7.Visible = True
        '    L7.Text = "(Valore superiori o uguali a 0)"
        '    Exit Sub
        'End If

        If InStr(txt1.Text, ".") = 0 Then
            L1.Visible = False
            If InStr(txt1.Text, ",") = 0 Then
                L1.Visible = False
            Else
                L1.Visible = True
                L1.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            L1.Visible = True
            L1.Text = "(Valore interi)"
            Exit Sub
        End If

        'If InStr(txt7.Text, ".") = 0 Then
        '    L7.Visible = False
        '    If InStr(txt7.Text, ",") = 0 Then
        '        L7.Visible = False
        '    Else
        '        L7.Visible = True
        '        L7.Text = "(Valore interi)"
        '        Exit Sub
        '    End If
        'Else
        '    L7.Visible = True
        '    L7.Text = "(Valore interi)"
        '    Exit Sub
        'End If

        If InStr(txt6.Text, ".") = 0 Then
            L6.Visible = False
            If InStr(txt6.Text, ",") = 0 Then
                L6.Visible = False
            Else
                L6.Visible = True
                L6.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            L6.Visible = True
            L6.Text = "(Valore interi)"
            Exit Sub
        End If

        If InStr(Txt5.Text, ".") = 0 Then
            L5.Visible = False
            If InStr(Txt5.Text, ",") = 0 Then
                L5.Visible = False
            Else
                L5.Visible = True
                L5.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            L5.Visible = True
            L5.Text = "(Valore interi)"
            Exit Sub
        End If

        If InStr(txt4.Text, ".") = 0 Then
            L4.Visible = False
            If InStr(txt4.Text, ",") = 0 Then
                L4.Visible = False
            Else
                L4.Visible = True
                L4.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            L4.Visible = True
            L4.Text = "(Valore interi)"
            Exit Sub
        End If
        If InStr(txt3.Text, ".") = 0 Then
            L3.Visible = False
            If InStr(txt3.Text, ",") = 0 Then
                L3.Visible = False
            Else
                L3.Visible = True
                L3.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            L3.Visible = True
            L3.Text = "(Valore interi)"
            Exit Sub
        End If

        If InStr(txt2.Text, ".") = 0 Then
            L2.Visible = False
            If InStr(txt2.Text, ",") = 0 Then
                L2.Visible = False
            Else
                L2.Visible = True
                L2.Text = "(Valore interi)"
                Exit Sub
            End If
        Else
            L2.Visible = True
            L2.Text = "(Valore interi)"
            Exit Sub
        End If

        If L1.Visible = True Or L2.Visible = True Or L3.Visible = True Or L4.Visible = True Or L5.Visible = True Or L6.Visible = True Or L7.Visible = True Then
            Exit Sub
        End If

        Cache(Session.Item("GRiga")) = txtRiga.Text
        Cache(Session.Item("GProgressivo")) = cmbComponente.SelectedItem.Value
        'Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 35) & " " & par.MiaFormat(cmbCond.SelectedItem.Value, 5) & " " & par.MiaFormat(cmbProf.SelectedItem.Value, 5) & " " & Chr(160) & Chr(160) & par.MiaFormat(txt1.Text, 7) & ",00 " & par.MiaFormat(txt2.Text, 7) & ",00 " & par.MiaFormat(txt3.Text, 7) & ",00 " & par.MiaFormat(txt4.Text, 7) & ",00 " & par.MiaFormat(Txt5.Text, 7) & ",00 " & par.MiaFormat(txt6.Text, 7) & ",00 " & par.MiaFormat(txt7.Text, 7) & ",00"
        Cache(Session.Item("GLista")) = par.MiaFormat(cmbComponente.SelectedItem.Text, 35) & " " & par.MiaFormat("", 5) & " " & par.MiaFormat("", 5) & " " & Chr(160) & Chr(160) & par.MiaFormat(txt1.Text, 7) & ",00 " & par.MiaFormat(txt2.Text, 7) & ",00 " & par.MiaFormat(txt3.Text, 7) & ",00 " & par.MiaFormat(txt4.Text, 7) & ",00 " & par.MiaFormat(Txt5.Text, 7) & ",00 " & par.MiaFormat(txt6.Text, 7) & ",00 " & par.MiaFormat("", 7) & "   "

        Response.Clear()
        Response.Write("<script>window.close();</script>")
        Response.End()
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


    Protected Sub txt4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txt4.TextChanged

    End Sub
End Class
