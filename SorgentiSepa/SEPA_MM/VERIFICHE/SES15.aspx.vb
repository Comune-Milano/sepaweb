Imports System.IO

Partial Class AMMSEPA_Controllo_Default
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load




    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        Try

            If File.Exists(Server.MapPath("~\") & TextBox1.Text) Then
                Dim fileCreatedDate As DateTime = File.GetCreationTime(Server.MapPath("~\") & TextBox1.Text)
                Dim UltimoAccesso As DateTime = File.GetLastAccessTime(Server.MapPath("~\") & TextBox1.Text)
                Dim UltimaModifica As DateTime = File.GetLastWriteTime(Server.MapPath("~\") & TextBox1.Text)
                Label1.Text = "Data creazione del file :" & Format(fileCreatedDate, "dd/MM/yyyy-HH:mm:ss") & "<br />"
                Label1.Text &= "Data ultimo accesso :" & Format(UltimoAccesso, "dd/MM/yyyy-HH:mm:ss") & "<br />"
                Label1.Text &= "Data ultima modifica :" & Format(UltimaModifica, "dd/MM/yyyy-HH:mm:ss")
            Else
                Label1.Text = "File non trovato"
            End If
        Catch ex As Exception
            Label1.Text = "File non trovato"
        End Try
        
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.EventArgs) Handles Button2.Click
        
        If InStr(TextBox2.Text, "'") = 0 Then
            If par.VerificaPW(TextBox2.Text) = True Then
                MultiView1.ActiveViewIndex = 1
            End If
        End If
    End Sub
End Class
