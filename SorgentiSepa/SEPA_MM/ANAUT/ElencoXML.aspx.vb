Imports System.IO

Partial Class ANAUT_ElencoXML
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim smNomeFile As String
        Dim MiaSHTML As String
        Dim ElencoFile() As String
        Dim I As Long
        Dim J As Long
        Dim POS As Integer
        Dim MIOCOLORE As String
        Try




            smNomeFile = Session.Item("ID_CAF") & "_"
            POS = Len(smNomeFile) + 1

            MiaSHTML = "<p><b><font face='Arial' size='2'>Elenco file inviati</font></b></p><table border='0' cellpadding='1' cellspacing='1' width='500px'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='250px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='250px'><font size='2' face='Arial'>Data Invio</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            I = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("IMPORT/"), FileIO.SearchOption.SearchTopLevelOnly, smNomeFile & "*.zip")
                ReDim Preserve ElencoFile(I)
                ElencoFile(I) = foundFile
                I = I + 1

            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To I - 2
                For jj = kk + 1 To I - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), POS, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), POS, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If I > 0 Then
                For J = 0 To I - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & RicavaFile(ElencoFile(J)) & "</font></td>" & vbCrLf
                    '                    MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & Mid(RicavaFile(ElencoFile(J)), POS + 6, 2) & "/" & Mid(RicavaFile(ElencoFile(J)), POS + 4, 2) & "/" & Mid(RicavaFile(ElencoFile(J)), POS, 4) & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='250px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(J)).CreationTime & "</font></td>" & vbCrLf

                    '                    MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><p align='center'><a href='Allegati_1.aspx?NOME=" & RicavaFile(ElencoFile(J)) & "&EXT=ZIP' target='_blank'><img border='0' src='../ImmMaschere/MenuTopDownload.gif'></a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If
                    If J = 10 Then Exit For
                Next J
            End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            Response.Write(MiaSHTML)


        Catch ex As Exception
            'par.OracleConn.Close()
            Response.Write(ex.ToString)
        End Try
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
