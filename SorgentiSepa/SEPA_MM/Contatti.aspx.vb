Imports System.IO

Partial Class Contatti
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim POS As Integer
        Dim POS1 As Integer
        Dim POS2 As Integer
        Dim POS3 As Integer
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Write("<table cellpadding='0' cellspacing='0' width='100%'>")
            Response.Write("<tr>")
            Response.Write("<td colspan='' rowspan='' style='text-align: center'>")
            Response.Write("<img src='IMG/Banner800.gif' style='z-index: 100; left: 0px; position: static; top: 0px' width='800' height='83' /></td>")
            Response.Write("</tr>")
            Response.Write("</table>")
            Response.Write("<table style='z-index: 100; left: 0px; width: 100%; position: static; top: 0px'>")

            Response.Write("<tr>")
            Response.Write("<td style='width: 25%'>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>Elenco Contatti</strong></span></td>")
            Response.Write("<td style='width: 25%px'>")
            Response.Write("</td>")
            Response.Write("<td>")
            Response.Write("</td>")
            Response.Write("</tr>")
            Response.Write("<tr>")
            Response.Write("<td style='width: 25%'>")
            Response.Write("</td>")
            Response.Write("<td style='width: 25%'>")
            Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>Telefono</strong></span></td>")
            Response.Write("<td>")
            Response.Write("<strong><span style='font-size: 10pt; font-family: Arial'>e-mail</span></strong></td>")
            Response.Write("<td>")
            Response.Write("<strong><span style='font-size: 10pt; font-family: Arial'>Altro</span></strong></td>")
            Response.Write("</tr>")

            ' Create an instance of StreamReader to read from a file.
            Dim sr As StreamReader = New StreamReader(Server.MapPath("contatti.txt"))
            Dim line As String
            ' Read and display the lines from the file until the end 
            ' of the file is reached.
            Do
                line = sr.ReadLine()
                POS = InStr(line, ";", CompareMethod.Text)
                Response.Write("<tr>")
                Response.Write("<td style='width: 25%' height: 21px>")
                If POS > 0 Then
                    Response.Write(Mid(line, 1, POS - 1) & "</td>")

                    Response.Write("<td style='width: 25%' height: 21px>")
                    POS1 = InStr(Mid(line, POS + 1, Len(line)), ";", CompareMethod.Text)
                    Response.Write("<span style='font-size: 10pt; font-family: Arial'><strong>" & Mid(line, POS + 1, POS1 - 1) & "</strong></span></td>")
                    Response.Write("<td>")
                    POS2 = InStr(Mid(line, POS + POS1 + 1, Len(line)), ";", CompareMethod.Text)
                    Response.Write("<strong><span style='font-size: 10pt; font-family: Arial'>" & Mid(line, POS + POS1 + 1, POS2 - 1) & "</span></strong>")
                    Response.Write("</td>")
                    Response.Write("<td>")
                    POS3 = InStr(Mid(line, POS + POS1 + POS2 + 1, Len(line)), ";", CompareMethod.Text)
                    Response.Write("<strong><span style='font-size: 10pt; font-family: Arial'>" & Mid(line, POS + POS1 + POS2 + 1, POS3 - 1) & "</span></strong>")
                    Response.Write("</td>")
                    Response.Write("</tr>")
                End If
            Loop Until line Is Nothing
            sr.Close()


            Response.Write("</table>")
        Catch Ex As Exception

            'Label1.Text = Ex.Message
        End Try

    End Sub
End Class
