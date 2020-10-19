Imports System.IO

Partial Class AMMSEPA_Controllo_AggRendiconto
    Inherits PageSetIdMode
    Dim PAR As New CM.Global


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If PAR.VerificaPW(TextBox1.Text) = True Then
                FileUpload1.Visible = True
                Button2.Visible = True
            End If
        End If
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim FileName As String
        Try
            Dim percorso As String = Server.MapPath("..\FileTemp")  '"..\..\FileTemp"

            If FileUpload1.HasFile = True Then
                FileName = percorso & "\" & FileUpload1.FileName
                FileUpload1.SaveAs(FileName)

                If System.IO.File.Exists(FileName) = True Then

                    'Dim Attr As System.IO.FileAttributes
                    'Attr = System.IO.File.GetAttributes(FileName)

                    'Diagnostics.FileVersionInfo.GetVersionInfo(FileName)
                    Dim myFileVersionInfo As Diagnostics.FileVersionInfo = Diagnostics.FileVersionInfo.GetVersionInfo(FileName)

                    Dim sVer As String
                    sVer = myFileVersionInfo.FileVersion

                    Dim objStream As Stream = File.Open(FileName, FileMode.Open)
                    Dim buffer(objStream.Length) As Byte
                    objStream.Read(buffer, 0, objStream.Length)
                    objStream.Close()
                    System.IO.File.Delete(FileName)

                    PAR.OracleConn.Open()

                    PAR.SettaCommand(PAR)
                    'PAR.cmd.CommandText = "TRUNCATE TABLE SISCOM_MI.RENDICONTO_AGG DROP STORAGE"
                    PAR.cmd.CommandText = "DELETE SISCOM_MI.RENDICONTO_AGG"
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.RENDICONTO_AGG (VER,EXE) VALUES ('" & sVer & "' ,:TESTO) "

                    Dim parmData As New Oracle.DataAccess.Client.OracleParameter
                    With parmData
                        .Direction = Data.ParameterDirection.Input
                        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                        .ParameterName = "TESTO"
                        .Value = buffer
                    End With

                    PAR.cmd.Parameters.Add(parmData)
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.Parameters.Remove(parmData)

                    buffer = Nothing
                    objStream = Nothing

                    PAR.cmd.Dispose()
                    PAR.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If


                Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")
            End If

        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click
        Dim FileName As String
        Try
            Dim percorso As String = Server.MapPath("..\FileTemp")  '"..\..\FileTemp"

            If FileUpload1.HasFile = True Then
                FileName = percorso & "\" & FileUpload1.FileName
                FileUpload1.SaveAs(FileName)

                If System.IO.File.Exists(FileName) = True Then

                    'Dim Attr As System.IO.FileAttributes
                    'Attr = System.IO.File.GetAttributes(FileName)

                    'Diagnostics.FileVersionInfo.GetVersionInfo(FileName)
                    Dim myFileVersionInfo As Diagnostics.FileVersionInfo = Diagnostics.FileVersionInfo.GetVersionInfo(FileName)

                    Dim sVer As String
                    sVer = myFileVersionInfo.FileVersion

                    Dim objStream As Stream = File.Open(FileName, FileMode.Open)
                    Dim buffer(objStream.Length) As Byte
                    objStream.Read(buffer, 0, objStream.Length)
                    objStream.Close()
                    System.IO.File.Delete(FileName)

                    PAR.OracleConn.Open()

                    PAR.SettaCommand(PAR)
                    'PAR.cmd.CommandText = "TRUNCATE TABLE SISCOM_MI.SCAMBIODATI_AGG_MM DROP STORAGE"
                    PAR.cmd.CommandText = "DELETE SISCOM_MI.SCAMBIODATI_AGG_MM"
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.CommandText = "INSERT INTO SISCOM_MI.SCAMBIODATI_AGG_MM (VER,EXE) VALUES ('" & sVer & "' ,:TESTO) "

                    Dim parmData As New Oracle.DataAccess.Client.OracleParameter
                    With parmData
                        .Direction = Data.ParameterDirection.Input
                        .OracleDbType = Oracle.DataAccess.Client.OracleDbType.Blob
                        .ParameterName = "TESTO"
                        .Value = buffer
                    End With

                    PAR.cmd.Parameters.Add(parmData)
                    PAR.cmd.ExecuteNonQuery()
                    PAR.cmd.Parameters.Remove(parmData)

                    buffer = Nothing
                    objStream = Nothing

                    PAR.cmd.Dispose()
                    PAR.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If

                Response.Write("<script>alert('Operazione Effettuata!');self.close();</script>")
            End If
        Catch ex As Exception
            PAR.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class
