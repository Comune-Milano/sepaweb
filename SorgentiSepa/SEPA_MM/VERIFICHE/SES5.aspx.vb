Imports System.IO

Partial Class AMMSEPA_Controllo_EseguiMultiSiscom
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                FileUpload1.Visible = True
                Button2.Visible = True
            End If
        End If
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim FileName As String
        Dim sBuffer As String
        Dim sSql As String = ""
        Dim n As Int32
        Dim myTrans As Oracle.DataAccess.Client.OracleTransaction
        Dim OracleConn As Oracle.DataAccess.Client.OracleConnection
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()

        Try

            Dim percorso As String = Server.MapPath("..\FileTemp")  '"..\..\FileTemp"

            If FileUpload1.HasFile = True Then
                FileName = percorso & "\" & FileUpload1.FileName

                FileUpload1.SaveAs(FileName)

                If System.IO.File.Exists(FileName) = True Then
                    OracleConn = New Oracle.DataAccess.Client.OracleConnection(par.StringaSiscom)
                    OracleConn.Open()
                    myTrans = OracleConn.BeginTransaction()
                    cmd = OracleConn.CreateCommand()


                    Dim sr = New StreamReader(FileName, System.Text.Encoding.GetEncoding("ISO-8859-1"))

                    Do
                        sBuffer = sr.ReadLine()
                        If Not IsNothing(sBuffer) Then
                            sBuffer = RTrim(sBuffer)
                            If Right(sBuffer, 1) = ";" Then
                                sSql = sSql & " " & Left(sBuffer, Len(sBuffer) - 1)
                                sSql = sSql.Trim
                                If sSql <> "" And sSql.Substring(1, 2) <> "--" Then
                                    cmd.CommandText = sSql
                                    cmd.ExecuteNonQuery()
                                End If

                                n = n + 1
                                sSql = ""
                            Else
                                sSql = sSql & " " & sBuffer
                            End If
                        End If




                    Loop Until sBuffer Is Nothing
                    sr.Close()

                    System.IO.File.Delete(FileName)

                    myTrans.Commit()
                    cmd.Dispose()
                    myTrans.Dispose()
                    OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If

                Response.Write("<script>alert('Sono state eseguite " & n.ToString & " operazioni sql');self.close();</script>")
            End If




        Catch ex As Exception
            If Not OracleConn Is Nothing Then
                If OracleConn.State = Data.ConnectionState.Open Then
                    If Not myTrans Is Nothing Then
                        myTrans.Rollback()
                        myTrans.Dispose()
                    End If
                    OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                OracleConn.Dispose()
            End If

            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub
End Class
