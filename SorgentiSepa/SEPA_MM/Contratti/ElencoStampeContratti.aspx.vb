Imports System.IO

Partial Class Contratti_ElencoStampeContratti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Try

 
                Dim MiaSHTML As String
                Dim MIOCOLORE As String
                Dim i As Integer
                Dim ElencoFile() as string

                Dim j As Integer


                Label1.Text = "Contratto Codice " & Request.QueryString("cod")

                MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='650px'>" & vbCrLf
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='400px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='150px'><font size='2' face='Arial'>Data Stampa</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Download</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                i = 0
                MIOCOLORE = "#CCFFFF"
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/StampeContratti/"), FileIO.SearchOption.SearchTopLevelOnly, "cod_" & Request.QueryString("cod") & "*.html")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/StampeContratti/"), FileIO.SearchOption.SearchTopLevelOnly, "cod_" & Request.QueryString("cod") & "*.pdf")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next


                Dim kk As Long
                Dim jj As Long
                Dim scambia

                For kk = 0 To i - 2
                    For jj = kk + 1 To i - 1
                        If Len(RicavaFile(ElencoFile(kk))) = 43 Then
                            If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "_") + 21, 14)) < CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(jj)), "_") + 21, 14)) Then
                                scambia = ElencoFile(kk)
                                ElencoFile(kk) = ElencoFile(jj)
                                ElencoFile(jj) = scambia
                            End If
                        End If
                    Next
                Next


                If i > 0 Then
                    For j = 0 To i - 1
                        'j = i - 1
                        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='400px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/CONTRATTI/StampeContratti/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a  alt='Download' href='StampeContratti/Download.aspx?V=" & par.Cripta(RicavaFile(ElencoFile(j))) & "' target='_blank'><img src='../ImmMaschere/MenuTopDownload.gif' border='0'></a></font></td>" & vbCrLf

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
            Catch ex As Exception
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
