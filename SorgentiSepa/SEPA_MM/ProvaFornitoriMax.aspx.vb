
Partial Class _Default
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
    End Sub


    Function Encode(ByVal dec As String) As String

        Dim bt() As Byte
        ReDim bt(dec.Length)

        bt = System.Text.Encoding.ASCII.GetBytes(dec)


        Dim enc As String
        enc = System.Convert.ToBase64String(bt)

        Return enc
    End Function

    Function Decode(ByVal enc As String) As String
        Dim bt() As Byte

        bt = System.Convert.FromBase64String(enc)
        Dim dec As String
        dec = System.Text.Encoding.ASCII.GetString(bt)
        Return dec
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            Dim Esempio As New Fornitori
            'Dim wrapper As New Simple3Des("FORNITORI" & Format(Now, "yyyyMMdd"))
            'Dim plainText As String = wrapper.EncryptData(TextBox1.Text)
            Dim plainText As String = Encode(TextBox1.Text)
            TextBox4.Text = plainText
            Dim Esito As String

            Esito = Esempio.TrasferisciFile(plainText)

            TextBox3.Text = Esito
            TextBox2.Text = Decode(Esito)
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try


    End Sub
End Class
