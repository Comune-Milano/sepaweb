Imports Microsoft.VisualBasic
Imports System.Collections.ObjectModel
Imports System.IO

Public Class VerificaFirmaFile
    Public Function Verificafile(ByVal nFile As String) As String
        Dim xlsx As Byte() = {80, 75, 3, 4, 20, 0, 6, 0} 'excel 2010
        Dim xls As Byte() = {208, 207, 17, 224, 161, 177, 26, 225} 'excel 2003
        Dim jpg As Byte() = {255, 216, 255, 224, 0, 16, 74, 70} 'jpg
        Dim pdf As Byte() = {37, 80, 68, 70, 45, 49, 46, 52} 'pdf
        Dim zip As Byte() = {80, 75, 3, 4, 20, 0, 0, 0} 'zip
        Try
            Verificafile = ""
            Dim fs As FileStream = File.Open(nFile, FileMode.Open)
            fs.Position = 0

            Using br As New BinaryReader(fs)
                Dim tempfile As Byte()
                tempfile = br.ReadBytes(8)
                If test_bytes(xls, tempfile) = True Then Verificafile = "xls"
                If test_bytes(xlsx, tempfile) = True Then Verificafile = "xlsx"
                If test_bytes(jpg, tempfile) = True Then Verificafile = "jpg"
                If test_bytes(pdf, tempfile) = True Then Verificafile = "pdf"
                If test_bytes(zip, tempfile) = True Then Verificafile = "zip"
            End Using
        Catch ex As Exception
            Verificafile = ""
        End Try
    End Function

    Private Function test_bytes(ByVal file1 As Byte(), ByVal file2 As Byte()) As Boolean
        For i As Integer = 0 To file1.Length - 1
            If (file1(i) <> file2(i)) Then
                Return False
                ' Exit For
            End If
        Next i
        Return True

    End Function
End Class
