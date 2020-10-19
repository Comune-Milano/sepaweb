Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class AMMSEPA_Controllo_DownloadFile
    Inherits System.Web.UI.Page
    Dim par As New CM.Global


    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        If Not String.IsNullOrEmpty(Me.txtFileName.Text) Then
            Dim percorso As String = ""
            Dim fileName As String = Me.txtFileName.Text

            If Not String.IsNullOrEmpty(Me.txtPercorso.Text) Then
                percorso = Me.txtPercorso.Text & "\"
            End If

            If File.Exists(Server.MapPath("..\" & percorso & fileName)) = True Then
                Dim nomefile As String = Server.MapPath("..\" & percorso & fileName)
                If fileName.Contains(".config") Then
                    fileName = fileName.Replace(".config", ".txt")
                End If
                If percorso.Contains("FileTemp") = False Then
                    File.Copy(nomefile, Server.MapPath("..\FileTemp\") & fileName, True)
                End If
                If File.Exists(Server.MapPath("..\FileTemp\" & fileName)) Then
                    Dim nFile As String = ""
                    Dim perc As String = Server.MapPath("..\FileTemp\")
                    Dim fileDescrizione As String = ""
                    Dim zipfic As String = ""
                    Dim zipFileName As String = "download" & Format(Now, "yyyyMMddHHmmss") & ".zip"
                    zipfic = perc & zipFileName
                    nFile = perc & fileName


                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream

                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    Dim strFile As String
                    strFile = nFile
                    Dim strmFile As FileStream = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                    Dim sFile As String = Path.GetFileName(strFile)
                    Dim theEntry As ZipEntry = New ZipEntry(sFile)
                    Dim fi As New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                    File.Delete(strFile)
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                    Response.Write("<script>alert('File trovato...verrà ora scaricato!');window.open('../FileTemp/" & zipFileName & "','download','');</script>")
                Else
                    Response.Write("<script>alert('File NON trovato!');</script>")

                End If

            End If

        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.TblFoundFile.Visible = False
    End Sub

    Protected Sub btnAccedi_Click(sender As Object, e As System.EventArgs) Handles btnAccedi.Click
        If InStr(txtPw.Text, "'") = 0 Then
            If par.VerificaPW(txtPw.Text) = True Then
                Me.TblFoundFile.Visible = True
                Me.txtPw.Visible = False
                Me.btnAccedi.Visible = False
            Else
                Response.Write("<script>alert('NON AUTORIZZATO!');</script>")
            End If
        End If
    End Sub
End Class
