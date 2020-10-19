
Partial Class AMMSEPA_TestoEvento
    Inherits PageSetIdMode


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Write(Request.QueryString("t"))
    End Sub

    Function DeCripta(ByVal testo As String) As String
        Dim EncStr As String
        Dim EncKey As String, TempEncKey As String
        Dim EncLen As Integer
        Dim EncPos As Integer
        Dim EncKeyPos As Integer
        Dim tempChar As String
        Dim TA As Integer, TB As Integer, TC As Integer
        Dim x, JustChanged

        JustChanged = True
        DeCripta = testo
        TempEncKey = "S"
        EncKey = ""
        EncStr = ""
        EncPos = 1
        EncKeyPos = 1
        For x = 1 To Len(TempEncKey)
            EncKey = EncKey & Asc(Mid$(TempEncKey, x, 1))
        Next
        EncLen = Len(EncKey)

        For x = 1 To Len(testo) Step 2
            TB = Asc(Mid$(EncKey, EncKeyPos, 1))
            EncKeyPos = EncKeyPos + 1
            If EncKeyPos > EncLen Then EncKeyPos = 1
            tempChar = Mid$(testo, x, 2)
            TA = Val("&H" + tempChar)
            TC = TB Xor TA
            EncStr = EncStr & Chr(TC)
        Next
        DeCripta = EncStr

    End Function
End Class
