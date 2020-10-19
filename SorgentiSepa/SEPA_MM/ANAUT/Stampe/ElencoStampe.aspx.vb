
Partial Class ANAUT_Stampe_ElencoStampe
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
        End If
        If Not IsPostBack Then
            Try


                Dim MiaSHTML As String
                Dim MIOCOLORE As String
                Dim i As Integer
                Dim ElencoFile()
                Dim pos As Integer
                Dim j As Integer



                MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='90%'><tr><td width='60%' class='style1'><strong>TIPO STAMPA</strong></td><td width='40%' class='style1'><strong>DATA STAMPA</strong></td></tr><tr><td width='60%'>&nbsp;</td><td width='40%'>&nbsp;</td></tr>" & vbCrLf



                i = 0
                MIOCOLORE = "#E8E8E8"
                par.OracleConn.Open()
                par.SettaCommand(par)
                par.cmd.CommandText = "select id_bando from sepa.utenza_dichiarazioni where id = " & Request.QueryString("ID")

                Dim idBando As String = par.cmd.ExecuteScalar
                par.OracleConn.Close()

                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../../ALLEGATI/ANAGRAFE_UTENZA/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Request.QueryString("COD") & "_" & Request.QueryString("ID") & "*.*")
                    'If InStr(foundFile, "02_0") = 0 Then
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                    'End If
                Next
                If i = 0 Then
                    MIOCOLORE = "#E8E8E8"
                    If idBando < 5 Then
                        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../../ALLEGATI/ANAGRAFE_UTENZA/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Request.QueryString("COD") & "-*.pdf")
                            'If InStr(foundFile, "02_0") = 0 Then
                            ReDim Preserve ElencoFile(i)
                            ElencoFile(i) = foundFile
                            i = i + 1
                            'End If
                        Next
                    End If

                End If

                Dim kk As Long
                Dim jj As Long
                Dim scambia

                For kk = 0 To i - 2
                    For jj = kk + 1 To i - 1
                        If CLng(Replace(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 14), "_", "")) < CLng(Replace(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 14), "_", "")) Then
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
                            Case "00"
                                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Dichiarazione</a></font></td>" & vbCrLf
                            Case "02"
                                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Ricevuta dichiarazione calcolo ISEE-ERP</a></font></td>" & vbCrLf
                            Case "01"
                                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Notifica ISE per posta</a></font></td>" & vbCrLf
                            Case "03"
                                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Diffida per documentazione mancante</a></font></td>" & vbCrLf
                            Case "04"
                                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Autocertificazione stato di servizio</a></font></td>" & vbCrLf
                            Case "05"
                                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Frontespizio</a></font></td>" & vbCrLf
                        End Select


                        MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='2' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                        If MIOCOLORE = "#E8E8E8" Then
                            MIOCOLORE = "#FFFFFF"
                        Else
                            MIOCOLORE = "#E8E8E8"
                        End If

                    Next j
                End If


                'NUOVA GESTIONE
                i = 0
                For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../../ALLEGATI/ANAGRAFE_UTENZA/"), FileIO.SearchOption.SearchTopLevelOnly, "*_" & Request.QueryString("ID") & "$*.pdf")
                    ReDim Preserve ElencoFile(i)
                    ElencoFile(i) = foundFile
                    i = i + 1
                Next
                For kk = 0 To i - 2
                    For jj = kk + 1 To i - 1
                        If CLng(Replace(Mid(RicavaFile(ElencoFile(kk)), 1, 14), "_", "")) < CLng(Replace(Mid(RicavaFile(ElencoFile(jj)), 1, 14), "_", "")) Then
                            scambia = ElencoFile(kk)
                            ElencoFile(kk) = ElencoFile(jj)
                            ElencoFile(jj) = scambia
                        End If
                    Next
                Next
                If i > 0 Then
                    For j = 0 To i - 1
                        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='2'><a href='../../ALLEGATI/ANAGRAFE_UTENZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaNomeDoc(RicavaFile(ElencoFile(j))) & "</a></font></td>" & vbCrLf
                        MiaSHTML = MiaSHTML & "<td width='40%' bgcolor='" & MIOCOLORE & "'><font size='2' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

                        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                        If MIOCOLORE = "#E8E8E8" Then
                            MIOCOLORE = "#FFFFFF"
                        Else
                            MIOCOLORE = "#E8E8E8"
                        End If

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

    Private Function RicavaNomeDoc(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "$" Then
                Exit For
            End If
        Next

        RicavaNomeDoc = Right(sFile, Len(sFile) - N)
        RicavaNomeDoc = Mid(RicavaNomeDoc, 1, Len(RicavaNomeDoc) - 4)
    End Function

End Class
