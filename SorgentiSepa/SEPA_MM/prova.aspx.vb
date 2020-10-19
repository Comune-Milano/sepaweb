


Partial Class prova
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Response.Write("ok")
    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Response.Write("ok1")
    End Sub

    Protected Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick


        Dim arrUtenti() As String
        Dim mes As String = ""
        Dim trovato As Boolean = False

        arrUtenti = Split(Application("Message"), "|")

        For i = 0 To UBound(arrUtenti)
            If InStr(arrUtenti(i), Session("OPERATORE")) > 0 Then

                mes = "<div id=" & Chr(34) & "Messaggio" & Chr(34) & " style=" & Chr(34) & "border: 1px solid #CC0000;position: absolute; bottom: 0px; right: 0px; width: 200px; height: 200px; background-color: #FFFF99" & Chr(34) & "><table style=" & Chr(34) & "width:100%;" & Chr(34) & "><tr><td align='right'><img style=" & Chr(34) & "cursor:pointer;" & Chr(34) & " alt=" & Chr(34) & "chiudi" & Chr(34) & " src=" & Chr(34) & "NuoveImm/Stop-icon.png" & Chr(34) & " onclick=" & Chr(34) & "Chiudi()" & Chr(34) & "/></td></tr><tr><td style=" & Chr(34) & "font-family: arial; font-size: 9pt; font-weight: bold; color: #000000" & Chr(34) & ">Messaggio da Amministratore</td></tr><tr><td style=" & Chr(34) & "font-family: arial; font-size: 8pt; font-weight: normal; color: #000000" & Chr(34) & ">" & Trim(Mid(arrUtenti(i), InStr(arrUtenti(i), "#") + 1, Len(arrUtenti(i)))) & "</td></tr></table></div>"

                trovato = True
                Application.Lock()
                Application("Message") = Replace(Application("Message"), arrUtenti(i), "")
                Application.UnLock()
                vis.Value = "1"
                Exit For
            End If

        Next

        If trovato = True Then
            lblTitolo.Text = mes
        Else
            If vis.Value <> "1" Then
                lblTitolo.Text = ""
            End If

        End If
    End Sub
End Class
