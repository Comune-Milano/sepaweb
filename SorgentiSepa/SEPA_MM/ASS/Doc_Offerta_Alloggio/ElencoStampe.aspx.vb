
Partial Class ANAUT_Stampe_ElencoStampe
    Inherits PageSetIdMode

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile() as string

            Dim j As Integer

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='90%'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='33%'><font face='Arial' size='2'>Tipo stampa</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='33%'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../../ALLEGATI/ABBINAMENTI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Request.QueryString("IDDOM") & "*.pdf")
                If InStr(foundFile, "20_") = 0 Then
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                End If
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

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
                    Select Case Mid(RicavaFile(ElencoFile(j)), 1, 2)
                        Case "A1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Telegramma Invito</a></font></td>" & vbCrLf
                        Case "A2"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Permesso di Visita Alloggio</a></font></td>" & vbCrLf
                        Case "A3"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Modulo Accettazione Alloggio Offerto</a></font></td>" & vbCrLf
                        Case "A4"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Diffida Offerta Altro Alloggio</a></font></td>" & vbCrLf
                        Case "A5"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Diffida Offerta Stesso Alloggio</a></font></td>" & vbCrLf
                        Case "A6"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Modulo Offerta Alloggio</a></font></td>" & vbCrLf
                        Case "A7"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Rapporto sintetico Alloggio</a></font></td>" & vbCrLf
                        Case "C1"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Composizione Nucleo</a></font></td>" & vbCrLf
                        Case "C2"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Documentaz. Mancante</a></font></td>" & vbCrLf
                        Case "C3"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Negativo</a></font></td>" & vbCrLf
                        Case "C4"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Negativo All. Adeguato</a></font></td>" & vbCrLf
                        Case "C5"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Negativo ISEE</a></font></td>" & vbCrLf
                        Case "C6"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Negativo Morosità</a></font></td>" & vbCrLf
                        Case "C7"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Negativo Requisiti</a></font></td>" & vbCrLf
                        Case "C8"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Positivo</a></font></td>" & vbCrLf
                        Case "C9"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Positivo Morosità</a></font></td>" & vbCrLf
                        Case "C10"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Negativo Ricorso</a></font></td>" & vbCrLf
                        Case "C11"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../../ALLEGATI/ABBINAMENTI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>C.A. Art.22 - Esito Positivo Ricorso</a></font></td>" & vbCrLf
                    End Select

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
            Label3.Text = MiaSHTML

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
