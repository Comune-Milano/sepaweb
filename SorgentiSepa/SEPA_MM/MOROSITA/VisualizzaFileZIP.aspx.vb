Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
'12/01/2015 PUCCIA Nuova connessione  tls ssl
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Security

Partial Class Morosita_CreaLettere3
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim pp As New MavOnline.MAVOnlineBeanService
    Dim RichiestaEmissioneMAV As New MavOnline.richiestaMAVOnlineWS
    Dim Esito As New MavOnline.rispostaMAVOnlineWS
    Dim binaryData() As Byte
    Dim outFile As System.IO.FileStream
    Dim outputFileName As String = ""
    Dim myExcelFile As New CM.ExcelFile
    Dim sNomeFile As String

    '12/01/2015 PUCCIA Nuova connessione  tls ssl
    Private Shared Function CertificateHandler(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal SSLerror As SslPolicyErrors) As Boolean
        Return True
    End Function
    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
        End If

        Dim MIOCOLORE As String
        Dim MiaSHTML As String

        Dim ElencoFile()
        Dim scambia

        Dim i, j As Integer
        Dim IndiceMorosita As String = ""

        Dim Str As String = ""


        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()

        If Not IsPostBack Then
            Try
                IndiceMorosita = Request.QueryString("IDMOR")

                MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='450px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                i = 0
                MIOCOLORE = "#CCFFFF"

                'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("Varie\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & IndiceMorosita & "-*.zip")
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("..\ALLEGATI\MOROSITA_CONTRATTI\"), FileIO.SearchOption.SearchTopLevelOnly, "Morosita_" & IndiceMorosita & "-*.zip")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next


                For kk = 0 To i - 2
                    For jj = kk + 1 To i - 1
                        If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14)) Then
                            scambia = ElencoFile(kk)
                            ElencoFile(kk) = ElencoFile(jj)
                            ElencoFile(jj) = scambia
                        End If
                    Next
                Next

                If i > 0 Then
                    For j = 0 To i - 1
                        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                        'MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='Varie/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/MOROSITA_CONTRATTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                        If MIOCOLORE = "#CCFFFF" Then
                            MIOCOLORE = "#FFFFCC"
                        Else
                            MIOCOLORE = "#CCFFFF"
                        End If
                        If j = 10 Then Exit For
                    Next j
                End If
                MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                Response.Write(MiaSHTML)



            Catch ex As Exception

                Response.Write(ex.Message)

            End Try
        End If
    End Sub



End Class
