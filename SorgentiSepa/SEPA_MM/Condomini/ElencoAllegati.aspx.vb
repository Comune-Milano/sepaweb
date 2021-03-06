﻿Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports ICSharpCode.SharpZipLib.Zip

Partial Class Contratti_ElencoAllegati
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        'If Not IsPostBack Then

        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim MiaSHTML As String
            Dim MiaSHTML_Stampa As String = ""
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile() as string

            Dim j As Integer
            Dim Conduttore As String = ""


            Label1.Text = "Condominio cod. " & Format(CDbl(Request.QueryString("cod")), "00000")



            par.cmd.CommandText = "select CONDOMINI.DENOMINAZIONE FROM SISCOM_MI.CONDOMINI WHERE ID = " & Request.QueryString("cod")
            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader3.Read Then
                Label1.Text = Label1.Text & "<br/>Denominazione: " & par.IfNull(myReader3("DENOMINAZIONE"), "")
            End If
            myReader3.Close()

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='900px'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='150px'><font size='2' face='Arial'>Data Documento</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='200px'><font size='2' face='Arial'>Tipo Allegato</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='350px'><font size='2' face='Arial'>Descrizione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Download</font></td>" & vbCrLf                'solo lettura
            If Request.QueryString("LT") <> "1" Then
                MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Elimina</font></td>" & vbCrLf
            End If

            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            MiaSHTML_Stampa = "CONDOMINIO CODICE " & Format(CDbl(Request.QueryString("cod")), "00000") & " - Data Stampa:" & Format(Now, "dd/MM/yyyy") & "<br/>" & Label1.Text & "<br/><br/>Elenco Allegati<br/><br/>"
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<tr>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='10%'><font size='2' face='Arial'>Data Documento</font></td>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='20%'><font size='2' face='Arial'>Tipo Allegato</font></td>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='70%'><font size='2' face='Arial'>Descrizione</font></td>" & vbCrLf
            MiaSHTML_Stampa = MiaSHTML_Stampa & "</tr>" & vbCrLf


            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONDOMINI/"), FileIO.SearchOption.SearchTopLevelOnly, "*_" & Request.QueryString("cod") & ".zip")
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


            '0300030080200X29903


            If i > 0 Then
                For j = 0 To i - 1
                    'j = i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(UCase(Mid(RicavaFile(ElencoFile(j)), 1, 8))) & "</font></td>"


                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<tr>" & vbCrLf
                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='10%'><font size='1' face='Arial'>" & par.FormattaData(UCase(Mid(RicavaFile(ElencoFile(j)), 1, 8))) & "</font></td>"

                    par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MODELLI_ALLEGATI WHERE COD='" & UCase(Mid(RicavaFile(ElencoFile(j)), 10, RicavaFile(ElencoFile(j)).Substring(10).IndexOf("_") + 1)) & "'"
                    Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader5.HasRows = True Then
                        If myReader5.Read Then
                            MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                            MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='90%'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                        End If
                    Else
                        MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>NON DEFINITO</font></td>" & vbCrLf
                        MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='90%'><font size='1' face='Arial'>NON DEFINITO</font></td>" & vbCrLf
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
                                        outStream = File.Create(Server.MapPath("../FileTemp/") & "/" & entry.Name, 2048)
                                        Do While True
                                            bytes = inStream.Read(buff, 0, 2048)
                                            If bytes = 0 Then

                                                Exit Do
                                            End If
                                            outStream.Write(buff, 0, bytes)
                                        Loop
                                        outStream.Close()

                                        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("../FileTemp/") & "/" & entry.Name, System.Text.Encoding.GetEncoding("iso-8859-1"))
                                        Dim contenuto As String = sr1.ReadToEnd()
                                        sr1.Close()

                                        DESCRIZIONE_ALLEGATO = Mid(contenuto, 32, Len(contenuto))
                                    End If
                                Catch
                                End Try

                            Catch ex As ZipException
                                DESCRIZIONE_ALLEGATO = ""
                                Response.Write(ex.Message)
                                Exit Do
                            End Try
                        Loop
                        inStream.Close()
                    Catch ex As Exception
                        DESCRIZIONE_ALLEGATO = "Errore...file corrotto in fase di trasferimento!"
                        erroreTrasferimento = True
                    End Try


                    MiaSHTML = MiaSHTML & "<td width='350px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & DESCRIZIONE_ALLEGATO & "</font></td>" & vbCrLf
                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='70%'><font size='1' face='Arial'>" & DESCRIZIONE_ALLEGATO & "</font></td>" & vbCrLf

                    If erroreTrasferimento = False Then
                        MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a  alt='Download' href='../ALLEGATI/CONDOMINI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'><img src='../ImmMaschere/MenuTopDownload.gif' border='0'></a></font></td>" & vbCrLf
                        If Request.QueryString("LT") <> "1" Then
                            MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><a href='CancellaAllegato.aspx?NOME=" & RicavaFile(ElencoFile(j)) & "&EXT=ZIP' target='_blank'><img border='0' src='../NuoveImm/Elimina.png'></a></td>" & vbCrLf
                        End If
                    Else
                        MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'></td>" & vbCrLf
                        If Request.QueryString("LT") <> "1" Then
                            MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'></td>" & vbCrLf
                        End If
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


            '***********************************************AGGIUNTA ALLEGATI MOROSITA STAMPATA****************************************

            i = 0
            MIOCOLORE = "#CCFFFF"

            par.cmd.CommandText = "select id from siscom_mi.cond_morosita where id_condominio = " & Request.QueryString("cod") & " order by rif_da asc"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While lettore.Read
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/MOROSITA_CONDOMINI/"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & par.IfNull(lettore("id"), 0) & "-*.zip")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next
            End While
            If i > 0 Then
                MiaSHTML = MiaSHTML & "<tr>&nbsp;&nbsp;</tr><tr><td colspan = 5  style='border-bottom-style: double; border-bottom-width: thin; border-bottom-color: #FF0000; text-align: center;'>ELENCO FILE GENERATI PER MOROSITA CONDOMINIALE</td></tr>"

                For j = 0 To i - 1
                    'j = i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(UCase(Right(ElencoFile(j).ToString.Replace(".zip", ""), 14).Substring(0, 8))) & "</font></td>"


                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<tr>" & vbCrLf
                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='10%'><font size='1' face='Arial'>" & par.FormattaData(UCase(Mid(RicavaFile(ElencoFile(j)), 1, 8))) & "</font></td>"

                    'par.cmd.CommandText = "select * FROM SISCOM_MI.TAB_MODELLI_ALLEGATI WHERE COD='" & UCase(Mid(RicavaFile(ElencoFile(j)), 10, 3)) & "'"
                    'Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    'If myReader5.HasRows = True Then
                    '    If myReader5.Read Then
                    '        MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                    '        MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='90%'><font size='1' face='Arial'>" & par.IfNull(myReader5("DESCRIZIONE"), "--") & "</font></td>" & vbCrLf
                    '    End If
                    'Else
                    MiaSHTML = MiaSHTML & "<td width='550px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>MOROSITA CONDOMINIALE</font></td>" & vbCrLf
                    MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='90%'><font size='1' face='Arial'>MOROSITA</font></td>" & vbCrLf
                    '        End If
                    'myReader5.Close()
                    'Dim DESCRIZIONE_ALLEGATO As String = ""

                    'Dim inStream As New ZipInputStream(File.OpenRead(ElencoFile(j)))
                    'Dim outStream As FileStream
                    ''Dim entry As ZipEntry
                    'Dim buff(2047) As Byte
                    'Dim bytes As Integer
                    'Dim entry As ICSharpCode.SharpZipLib.Zip.ZipEntry

                    'Do While True
                    '    Try
                    '        entry = inStream.GetNextEntry()
                    '        If entry Is Nothing Then

                    '            Exit Do
                    '        End If
                    '        Try
                    '            If InStr(UCase(entry.Name), "_DESCRIZIONE.TXT") > 0 Then
                    '                outStream = File.Create(Server.MapPath("../FileTemp/") & "/" & entry.Name, 2048)
                    '                Do While True
                    '                    bytes = inStream.Read(buff, 0, 2048)
                    '                    If bytes = 0 Then

                    '                        Exit Do
                    '                    End If
                    '                    outStream.Write(buff, 0, bytes)
                    '                Loop
                    '                outStream.Close()

                    '                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("../FileTemp/") & "/" & entry.Name, System.Text.Encoding.GetEncoding("iso-8859-1"))
                    '                Dim contenuto As String = sr1.ReadToEnd()
                    '                sr1.Close()

                    '                DESCRIZIONE_ALLEGATO = Mid(contenuto, 32, Len(contenuto))
                    '            End If
                    '        Catch
                    '        End Try

                    '    Catch ex As ZipException
                    '        DESCRIZIONE_ALLEGATO = ""
                    '        Response.Write(ex.Message)
                    '    End Try
                    'Loop
                    'inStream.Close()

                    MiaSHTML = MiaSHTML & "<td width='350px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>EMISSIONE LETTERE MAV</font></td>" & vbCrLf
                    'MiaSHTML_Stampa = MiaSHTML_Stampa & "<td width='70%'><font size='1' face='Arial'>" & DESCRIZIONE_ALLEGATO & "</font></td>" & vbCrLf


                    MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a  alt='Download' href='../ALLEGATI/MOROSITA_CONDOMINI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'><img src='../ImmMaschere/MenuTopDownload.gif' border='0'></a></font></td>" & vbCrLf
                    If Session.Item("ID_OPERATORE") = "1" Then
                        MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><a href='CancellaAllegato.aspx?CALL=MORFILE&NOME=" & RicavaFile(ElencoFile(j)) & "&EXT=ZIP' target='_blank'><img border='0' src='../NuoveImm/Elimina.png'></a></td>" & vbCrLf
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
            MiaSHTML_Stampa = MiaSHTML_Stampa & "</table>" & vbCrLf
            Label3.Text = MiaSHTML
            Label4.Text = MiaSHTML_Stampa


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label3.Text = ex.Message
        End Try

    End Sub
    Private Sub AllegaFileMorosita()

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

    Protected Sub btnStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnStampa.Click
        Dim NomeFile As String = "Allegati_" & Format(Now, "yyyyMMddHHmmss")


        Dim url As String = NomeFile
        Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

        Dim pdfConverter As PdfConverter = New PdfConverter

        If Licenza <> "" Then
            pdfConverter.LicenseKey = Licenza
        End If

        pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
        pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
        pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
        pdfConverter.PdfDocumentOptions.ShowHeader = False
        pdfConverter.PdfDocumentOptions.ShowFooter = False
        pdfConverter.PdfDocumentOptions.LeftMargin = 20
        pdfConverter.PdfDocumentOptions.RightMargin = 20
        pdfConverter.PdfDocumentOptions.TopMargin = 5
        pdfConverter.PdfDocumentOptions.BottomMargin = 5
        pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

        pdfConverter.PdfDocumentOptions.ShowHeader = False
        pdfConverter.PdfFooterOptions.FooterText = ("")
        pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
        pdfConverter.PdfFooterOptions.DrawFooterLine = False
        pdfConverter.PdfFooterOptions.PageNumberText = ""
        pdfConverter.PdfFooterOptions.ShowPageNumber = False


        pdfConverter.SavePdfFromHtmlStringToFile(Label4.Text, Server.MapPath("..\FileTemp\") & NomeFile & ".pdf")
        Response.Write("<script>window.open('../FileTemp/" & NomeFile & ".pdf','Stampa','');</script>")
    End Sub
End Class
