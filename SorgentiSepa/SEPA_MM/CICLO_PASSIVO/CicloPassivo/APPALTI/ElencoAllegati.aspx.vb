Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip
Partial Class Contratti_ElencoAllegati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try

                par.OracleConn.Open()
                par.SettaCommand(par)

                Dim MiaSHTML As String
                Dim MIOCOLORE As String
                Dim i As Integer
                Dim ElencoFile() as string
                Dim cartella As String = ""
                Dim j As Integer

                If Request.QueryString("T") = "3" Then
                    cartella = "Appalti"

                    par.cmd.CommandText = "select * from siscom_mi.appalti where id=" & Request.QueryString("cod")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        Label1.Text = "Elenco allegati appalto num. repertorio " & par.IfNull(myReader("num_repertorio"), "")
                    End If
                    myReader.Close()
                Else
                    cartella = "Fornitori"
                    par.cmd.CommandText = "select * from siscom_mi.fornitori where id=" & Request.QueryString("cod")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        If par.IfNull(myReader("ragione_sociale"), "") <> "" Then
                            Label1.Text = "Allegati " & par.IfNull(myReader("ragione_sociale"), "")
                        Else
                            Label1.Text = "Allegati " & par.IfNull(myReader("cognome"), "") & " " & par.IfNull(myReader("nome"), "")
                        End If
                    End If
                    myReader.Close()
                End If


                MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='900px'>" & vbCrLf
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='150px'><font size='2' face='Arial'>Data Documento</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='200px'><font size='2' face='Arial'>Tipo Allegato</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='350px'><font size='2' face='Arial'>Descrizione</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Download</font></td>" & vbCrLf                'solo lettura

                i = 0
                MIOCOLORE = "#CCFFFF"
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../../../ALLEGATI/" & cartella & "/"), FileIO.SearchOption.SearchTopLevelOnly, "*_" & Request.QueryString("cod") & ".zip")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next

                Dim kk As Long
                Dim jj As Long
                Dim scambia

                For kk = 0 To i - 2
                    For jj = kk + 1 To i - 1
                        If CLng(Mid(RicavaFile(ElencoFile(kk)), 1, 8)) < CLng(Mid(RicavaFile(ElencoFile(jj)), 1, 8)) Then
                            scambia = ElencoFile(kk)
                            ElencoFile(kk) = ElencoFile(jj)
                            ElencoFile(jj) = scambia
                        End If
                    Next
                Next

                If i > 0 Then
                    For j = 0 To i - 1
                        'j = i - 1
                        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(UCase(Mid(RicavaFile(ElencoFile(j)), 1, 8))) & "</font></td>"



                        par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MODELLI_ALLEGATI WHERE COD='" & UCase(Mid(RicavaFile(ElencoFile(j)), 10, 3)) & "'"
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader5.HasRows = True Then
                            If myReader5.Read Then
                                MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                            End If
                        Else
                            MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>NON DEFINITO</font></td>" & vbCrLf
                        End If
                        myReader5.Close()

                        Dim erroreTrasferimento As Boolean = False
                        Dim DESCRIZIONE_ALLEGATO As String = ""
                        Try
                            Dim inStream As New ZipInputStream(File.OpenRead(ElencoFile(j)))
                            Dim outStream As FileStream
                            'Dim entry As ZipEntry
                            Dim buff(2047) As Byte
                            Dim bytes As Integer
                            Dim entry As ICSharpCode.SharpZipLib.Zip.ZipEntry

                            Do While True
                                Try
                                    entry = inStream.GetNextEntry()
                                    If entry Is Nothing Then

                                        Exit Do
                                    End If
                                    Try
                                        If InStr(UCase(entry.Name), "_DESCRIZIONE.TXT") > 0 Then
                                            outStream = File.Create(Server.MapPath("../../../FileTemp/") & entry.Name, 2048)
                                            Do While True
                                                bytes = inStream.Read(buff, 0, 2048)
                                                If bytes = 0 Then

                                                    Exit Do
                                                End If
                                                outStream.Write(buff, 0, bytes)
                                            Loop
                                            outStream.Close()

                                            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("../../../FileTemp/") & entry.Name, System.Text.Encoding.GetEncoding("iso-8859-1"))
                                            Dim contenuto As String = sr1.ReadToEnd()
                                            sr1.Close()

                                            DESCRIZIONE_ALLEGATO = Mid(contenuto, 32, Len(contenuto))
                                        End If
                                    Catch
                                    End Try

                                Catch ex As ZipException
                                    DESCRIZIONE_ALLEGATO = ""
                                    Response.Write(ex.Message)
                                End Try
                            Loop
                            inStream.Close()
                        Catch ex As Exception
                            DESCRIZIONE_ALLEGATO = "Errore...file corrotto in fase di trasferimento!"
                            erroreTrasferimento = True
                        End Try

                        MiaSHTML = MiaSHTML & "<td width='350px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & DESCRIZIONE_ALLEGATO & "</font></td>" & vbCrLf


                        If erroreTrasferimento = False Then
                            MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a  alt='Download' href='../../../ALLEGATI/" & cartella & "/" & RicavaFile(ElencoFile(j)) & "' target='_blank'><img src='../../../ImmMaschere/MenuTopDownload.gif' border='0'></a></font></td>" & vbCrLf
                        Else
                            MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'></font></td>" & vbCrLf
                        End If


                        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                        If MIOCOLORE = "#CCFFFF" Then
                            MIOCOLORE = "#FFFFCC"
                        Else
                            MIOCOLORE = "#CCFFFF"
                        End If
                        'If j = 10 Then Exit For
                    Next j
                End If
                MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                Label3.Text = MiaSHTML




                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Catch ex As Exception
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label3.Text = ex.Message
            End Try
        End If
    End Sub

    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function
End Class
