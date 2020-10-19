Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_ExportCarMassivo
    Inherits System.Web.UI.Page

    Private Sub Contratti_ExportCarMassivo_Load(sender As Object, e As EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(Trim(Session.Item("OPERATORE"))) Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim NomeFile As String = "ExportRisultatiCarMassivo_" & Format(Now, "yyyyMMddHHmmss")
            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & NomeFile & ".txt", False, System.Text.Encoding.Default)
            sr.WriteLine(Session.Item("ExportCarMassivo"))
            sr.Close()

            Dim objCrc32 As New Crc32()
            Dim strmZipOutputStream As ZipOutputStream

            strmZipOutputStream = New ZipOutputStream(File.Create(Server.MapPath("..\FileTemp\") & NomeFile & ".zip"))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            strFile = Server.MapPath("..\FileTemp\") & NomeFile & ".txt"
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
            strmZipOutputStream.Finish()
            strmZipOutputStream.Close()
            Response.Redirect("..\FileTemp\" & NomeFile & ".zip")
        End If
    End Sub
End Class
