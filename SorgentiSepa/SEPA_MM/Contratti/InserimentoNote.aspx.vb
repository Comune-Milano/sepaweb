Imports System
Imports System.Data
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contratti_InserimentoNote
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public percentuale As Long = 0
    Dim NomeF As String = ""

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Or Session.Item("MOD_CONT_NOTE") = "0" Or Session.Item("CONT_LETTURA") = "1" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

        End If
    End Sub

    Public Property dtCompleta() As Data.DataTable
        Get
            If Not (ViewState("dtCompleta") Is Nothing) Then
                Return ViewState("dtCompleta")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtCompleta") = value
        End Set
    End Property

    Protected Sub btnProcedi_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnProcedi.Click
        Try
            Dim FileName As String = UCase(UploadOnServer())
            Dim objFile As Object
            objFile = Server.CreateObject("Scripting.FileSystemObject")

            If Not String.IsNullOrEmpty(FileName) Then
                If objFile.FileExists(FileName) And FileName.Contains(".TXT") Then
                    ReadFileTXT(FileName)
                Else
                    Response.Write("<script>alert('Tipo file non valido. Selezionare un file txt');</script>")
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:Import Note - ReadFileTXT " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ReadFileTXT(ByVal FileTXT As String)
        Try
            Dim sContenutoRiga As String = ""
            Dim dt As New Data.DataTable
            Dim ContaRighe As Long = 0
            Dim ContaRigheErrate As Long = 0
            Dim FileCreato As String = ""
            Dim Indice As Long = 1
            Dim ReportContratti As String = ""
            Dim COD_CONTRATTO As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            FileCreato = Replace(FileTXT, ".TXT", "_Report.TXT")
            Dim sr As StreamWriter = New StreamWriter(FileCreato, False, System.Text.Encoding.Default)
            Dim sr1 As StreamReader = New StreamReader(FileTXT, System.Text.Encoding.GetEncoding("iso-8859-1"))
            Do While sr1.Peek() >= 0
                sContenutoRiga = sr1.ReadLine()
                If sContenutoRiga <> "" Then
                    If InStr(sContenutoRiga, "#") > 0 Then
                        COD_CONTRATTO = Mid(sContenutoRiga, 1, InStr(sContenutoRiga, "#") - 1)
                        par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.ID AS IDC FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & par.PulisciStrSql(COD_CONTRATTO) & "'"
                        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader.HasRows = True Then
                            If myReader.Read Then
                                par.cmd.CommandText = "Insert into SISCOM_MI.GESTIONE_STORICO_NOTE (ID, ID_RIFERIMENTO, NOTE, DATA_ORA, ID_OPERATORE, ID_PROVENIENZA, DATA_EVENTO) Values (SISCOM_MI.SEQ_GESTIONE_STORICO_NOTE.NEXTVAL, " & myReader("IDC") & ", '" & par.PulisciStrSql(Mid(sContenutoRiga, InStr(sContenutoRiga, "#") + 1, Len(sContenutoRiga))) & "', '" & Format(Now, "yyyyMMddHHmmss") & "', " & Session.Item("ID_OPERATORE") & " , 1, '" & Format(Now, "yyyyMMdd") & "')"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) VALUES (" & myReader("IDC") & "," & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F02', 'NOTA IMPORTATA DA INSERIMENTO MASSIVO')"
                                par.cmd.ExecuteNonQuery()

                                sr.WriteLine("IMPORT-Riga " & Indice & "-" & COD_CONTRATTO & "-Nota importata con successo")
                                ContaRighe += 1
                            End If
                        Else
                            ContaRigheErrate += 1
                            sr.WriteLine("ERRORE-Riga " & Indice & "-" & COD_CONTRATTO & "-Contratto non trovato")
                        End If
                        myReader.Close()

                    Else
                        ContaRigheErrate += 1
                        sr.WriteLine("ERRORE-Riga " & Indice & "--separatore # non trovato")
                    End If

                End If
                Indice += 1
            Loop
            sr1.Close()
            sr.Close()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write("<script>alert('Operazione effettuata. Sono state importate " & ContaRighe & " note e scartate " & ContaRigheErrate & " note');</script>")
            If ContaRighe > 0 Then
                Dim filename As String = Replace(Replace(UCase(FileCreato), UCase(Server.MapPath("..\FileTemp\")), ""), ".TXT", "")
                ZippaFiles(filename)
                Response.Write("<script>window.open('../FileTemp/" & filename & ".zip','','');</script>")
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Import Note - ReadFileTXT " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ZippaFiles(ByVal nomefile As String)
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        Dim zipfic As String

        zipfic = Server.MapPath("..\FileTemp\" & nomefile & ".zip")

        strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        strFile = Server.MapPath("..\FileTemp\" & nomefile & ".txt")
        Dim strmFile As FileStream = File.OpenRead(strFile)
        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
        '
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

        File.Delete(strFile)

    End Sub


    Private Function UploadOnServer() As String
        UploadOnServer = ""
        Try
            '########## UPLOAD FILE TXT ##########
            If FileUpload.HasFile = True Then
                UploadOnServer = Replace(UCase(FileUpload.FileName), ".TXT", "_" & Format(Now, "yyyyMMddHHmmss")) & ".TXT"
                UploadOnServer = Server.MapPath("..\FileTemp\") & UploadOnServer
                FileUpload.SaveAs(UploadOnServer)
            End If
        Catch ex As Exception
            UploadOnServer = ""
            Session.Add("ERRORE", "Provenienza:Import Note - UploadOnServer " & ex.Message)
            Response.Write("<script>top.location.href='../../Errore.aspx';</script>")
        End Try

        Return UploadOnServer
    End Function

End Class
