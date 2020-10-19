Imports System.Convert
Imports System.IO


Partial Class Contratti_aaa
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        'Response.Write(Request.Form.Item("url_redirect"))
        Dim i As Integer = 0
        Dim sbXML As String = ""
        Dim pos As Integer = 0
        Dim pos1 As Integer = 0
        Dim NumBollettino As String = ""
        Dim DescErrore As String = ""
        Dim IdTransazione As String = ""

        'Response.Write(Request.Form("xml"))
        'Response.Write("-----")
        'Response.Write(Request.Form("pdf"))
        Try



            sbXML = Decode(Request.Form("xml"))

            pos = InStr(sbXML, "<codice_identificativo_bollettino>")
            If pos > 0 Then
                pos1 = InStr(sbXML, "</codice_identificativo_bollettino>")
                NumBollettino = Mid(sbXML, pos + 34, pos1 - pos - 34)
                'Response.Write("2 - BOLLETTINO:" & NumBollettino)
                If ScriviXML(sbXML, NumBollettino) = True Then
                    'Response.Write("Il Bollettino è stato correttamente creato.</br>Numero Bollettino: " & NumBollettino)
                    If ScriviPDF(Request.Form("pdf"), NumBollettino) = True Then

                        pos = InStr(sbXML, "<id_transazione>")

                        If pos > 0 Then
                            pos1 = InStr(sbXML, "</id_transazione>")
                            IdTransazione = Trim(Mid(sbXML, pos + 16, pos1 - pos - 16))

                            Try
                                par.OracleConn.Open()
                                par.SettaCommand(par)
                                par.cmd.CommandText = "update siscom_mi.bol_bollette set FL_STAMPATO='1',rif_bollettino='" & NumBollettino & "' where id=" & IdTransazione
                                par.cmd.ExecuteNonQuery()
                                par.cmd.Dispose()
                                par.OracleConn.Close()
                                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                                Response.Write("<table style=" & Chr(34) & "width:100%;" & Chr(34) & "><tr><td style=" & Chr(34) & "font-family: Arial; font-size: 12pt; font-weight: bold" & Chr(34) & "><span style=" & Chr(34) & "font-family: Arial; font-size: 16pt; font-weight: bold" & Chr(34) & ">SEPA@Web</span><br />Memorizzazione effettuata correttamente.</td></tr></table>")
                            Catch ex As Exception
                                par.OracleConn.Close()
                                Response.Write("</br>ERRORE DB:</br>" & ex.Message)
                            End Try
                        End If
                    End If
                End If
            Else
                pos = InStr(sbXML, "<descrizione_errore>")
                If pos > 0 Then
                    If ScriviXML(sbXML, "Errore_" & Format(Now, "yyyyMMddHHmmss")) = True Then

                    End If
                    pos1 = InStr(sbXML, "</descrizione_errore>")
                    DescErrore = Mid(sbXML, pos + 21, pos1 - pos + 1)
                    Response.Write("Si è verificato il seguente errore:</br>" & DescErrore)
                End If
            End If


        Catch ex As Exception
            Response.Write("ERRORE Generale:</br>" & ex.Message)
        End Try
    End Sub

    Function ScriviXML(ByVal TestoXML As String, ByVal Bollettino As String) As Boolean

        Dim outputFileName As String = ""

        Try
            ScriviXML = False

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Bollettino & ".xml", False, System.Text.Encoding.Default)
            sr.WriteLine(TestoXML)
            sr.Close()
            ScriviXML = True

        Catch ex As Exception
            ScriviXML = False
            Response.Write("Errore Generazione XML:</br>" & ex.Message)
        End Try


    End Function


    Function ScriviPDF(ByVal base64String As String, ByVal Bollettino As String) As Boolean
        Dim binaryData() As Byte
        Dim outFile As System.IO.FileStream
        Dim outputFileName As String = ""

        Try
            ScriviPDF = False
            outputFileName = Server.MapPath("..\ALLEGATI\CONTRATTI\ELABORAZIONI") & "\MAV\" & Bollettino & ".pdf"
            binaryData = System.Convert.FromBase64String(base64String)
            outFile = New System.IO.FileStream(outputFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write)
            outFile.Write(binaryData, 0, binaryData.Length - 1)
            outFile.Close()
            'Response.Write("</br><a href=" & Chr(34) & "ELABORAZIONI/" & Bollettino & ".pdf" & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Clicca qui per scaricare il Mav in formato PDF</a>")
            ScriviPDF = True

        Catch ex As Exception
            ScriviPDF = False
            Response.Write("Errore Generazione PDF:</br>" & ex.Message)
        End Try


    End Function

    Function Decode(ByVal enc As String) As String
        Dim bt() As Byte

        bt = System.Convert.FromBase64String(enc)


        Dim dec As String
        dec = System.Text.Encoding.ASCII.GetString(bt)


        Return dec


    End Function
End Class
