
Partial Class AMMSEPA_Controllo_CVarie
    Inherits PageSetIdMode
    Dim ElencoFile() As String
    Dim ElencoFileData() As String
    Dim ElencoFileCancella() As String

    Dim MIOCOLORE As String
    Dim i As Long
    Dim j As Long
    Dim MiaSHTML As String
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button5.Visible = False
        End If
    End Sub

    Private Function Carica()
        Try
            Dim NomeFile As String = ""
            CheckBoxList1.Items.Clear()
            Dim J As Long
            Dim testoricerca As String = "*.*"

            If TextBox2.Text <> "" Then
                testoricerca = "*" & TextBox2.Text & "*.*"
            End If

            i = 0
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../FileTemp/"), FileIO.SearchOption.SearchTopLevelOnly, testoricerca)
                ReDim Preserve ElencoFile(i)
                ReDim Preserve ElencoFileData(i)
                ReDim Preserve ElencoFileCancella(i)
                ElencoFile(i) = foundFile
                ElencoFileCancella(i) = foundFile
                ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")
                i = i + 1
            Next
            If i > 0 Then
                For j = 0 To i - 1
                    NomeFile = RicavaFile(ElencoFile(j))
                    If UCase(NomeFile) <> "MAX.MAX" Then
                        CheckBoxList1.Items.Add("<a href='..\FileTemp\" & NomeFile & "' target='_blank'>" & J + 1 & ") " & NomeFile & " - " & ElencoFileData(J) & "</a>")
                    End If
                Next j
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function Carica5()
        'Try
        '    Dim NomeFile As String = ""
        '    CheckBoxList1.Items.Clear()
        '    Dim J As Long

        '    i = 0
        '    For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/MOROSITA_CONTRATTI/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
        '        ReDim Preserve ElencoFile(i)
        '        ReDim Preserve ElencoFileData(i)
        '        ReDim Preserve ElencoFileCancella(i)
        '        ElencoFile(i) = foundFile
        '        ElencoFileCancella(i) = foundFile
        '        ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

        '        i = i + 1
        '    Next

        '    If i > 0 Then
        '        For J = 0 To i - 1

        '            NomeFile = RicavaFile(ElencoFile(J))
        '            If UCase(NomeFile) <> "MAX.MAX" Then
        '                Label3.Text = Label3.Text & "<a href='..\..\ALLEGATI\MOROSITA_CONTRATTI\" & NomeFile & "' target='_blank'>" & J + 1 & ") " & NomeFile & " - " & ElencoFileData(J) & "</a><br/>"
        '            End If
        '        Next J
        '    End If



        'Catch ex As Exception
        '    Response.Write(ex.Message)
        'End Try
        Try
            Dim NomeFile As String = ""
            CheckBoxList1.Items.Clear()
            Dim J As Long

            i = 0
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/MOROSITA_CONTRATTI/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                ReDim Preserve ElencoFile(i)
                ReDim Preserve ElencoFileData(i)
                ReDim Preserve ElencoFileCancella(i)
                ElencoFile(i) = foundFile
                ElencoFileCancella(i) = foundFile
                ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")
                i = i + 1
            Next
            If i > 0 Then
                For J = 0 To i - 1
                    NomeFile = RicavaFile(ElencoFile(J))
                    If UCase(NomeFile) <> "MAX.MAX" Then
                        CheckBoxList5.Items.Add("<a href='..\..\ALLEGATI\MOROSITA_CONTRATTI\" & NomeFile & "' target='_blank'>" & J + 1 & ") " & NomeFile & " - " & ElencoFileData(J) & "</a>")
                    End If
                Next J
            End If

        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function Carica6()
        Try
            Dim NomeFile As String = ""
            CheckBoxList1.Items.Clear()
            Dim J As Long

            i = 0
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/MOROSITA_CONDOMINI/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                ReDim Preserve ElencoFile(i)
                ReDim Preserve ElencoFileData(i)
                ReDim Preserve ElencoFileCancella(i)
                ElencoFile(i) = foundFile
                ElencoFileCancella(i) = foundFile
                ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

                i = i + 1
            Next

            If i > 0 Then
                For J = 0 To i - 1

                    NomeFile = RicavaFile(ElencoFile(J))
                    If UCase(NomeFile) <> "MAX.MAX" Then
                        Label4.Text = Label4.Text & "<a href='..\..\ALLEGATI\MOROSITA_CONDOMINI\" & NomeFile & "' target='_blank'>" & J + 1 & ") " & NomeFile & " - " & ElencoFileData(J) & "</a><br/>"
                    End If
                Next J
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

    Private Function Carica7()
        Try
            Dim NomeFile As String = ""
            CheckBoxList1.Items.Clear()
            Dim J As Long

            i = 0
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONDOMINI/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                ReDim Preserve ElencoFile(i)
                ReDim Preserve ElencoFileData(i)
                ReDim Preserve ElencoFileCancella(i)
                ElencoFile(i) = foundFile
                ElencoFileCancella(i) = foundFile
                ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

                i = i + 1
            Next

            If i > 0 Then
                For J = 0 To i - 1

                    NomeFile = RicavaFile(ElencoFile(J))
                    If UCase(NomeFile) <> "MAX.MAX" Then
                        Label5.Text = Label5.Text & "<a href='..\..\ALLEGATI\CONDOMINI\" & NomeFile & "' target='_blank'>" & J + 1 & ") " & NomeFile & " - " & ElencoFileData(J) & "</a><br/>"
                    End If
                Next J
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function
    'Private Function Carica1()
    '    Try
    '        Dim NomeFile As String = ""
    '        CheckBoxList2.Items.Clear()

    '        i = 0
    '        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ANAUT/EXPORT/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
    '            ReDim Preserve ElencoFile(i)
    '            ReDim Preserve ElencoFileData(i)
    '            ReDim Preserve ElencoFileCancella(i)
    '            ElencoFile(i) = foundFile
    '            ElencoFileCancella(i) = foundFile
    '            ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

    '            i = i + 1
    '        Next

    '        If i > 0 Then
    '            For j = 0 To i - 1

    '                NomeFile = RicavaFile(ElencoFile(j))
    '                If UCase(NomeFile) <> "MAX" Then
    '                    CheckBoxList2.Items.Add(NomeFile)
    '                End If
    '            Next j
    '        End If



    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Function

    'Private Function Carica3()
    '    Try
    '        Dim NomeFile As String = ""
    '        CheckBoxList4.Items.Clear()

    '        i = 0
    '        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../CONTRATTI/STAMPELETTERE/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
    '            ReDim Preserve ElencoFile(i)
    '            ReDim Preserve ElencoFileData(i)
    '            ReDim Preserve ElencoFileCancella(i)
    '            ElencoFile(i) = foundFile
    '            ElencoFileCancella(i) = foundFile
    '            ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

    '            i = i + 1
    '        Next

    '        If i > 0 Then
    '            For j = 0 To i - 1

    '                NomeFile = RicavaFile(ElencoFile(j))
    '                If UCase(NomeFile) <> "MAX" Then
    '                    CheckBoxList4.Items.Add(NomeFile)
    '                End If
    '            Next j
    '        End If



    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Function

    'Private Function Carica2()
    '    Try
    '        Dim NomeFile As String = ""
    '        CheckBoxList3.Items.Clear()

    '        i = 0
    '        For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../CONTRATTI/STAMPECONTRATTI/"), FileIO.SearchOption.SearchTopLevelOnly, "*.xml")
    '            ReDim Preserve ElencoFile(i)
    '            ReDim Preserve ElencoFileData(i)
    '            ReDim Preserve ElencoFileCancella(i)
    '            ElencoFile(i) = foundFile
    '            ElencoFileCancella(i) = foundFile
    '            ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

    '            i = i + 1
    '        Next

    '        If i > 0 Then
    '            For j = 0 To i - 1

    '                NomeFile = RicavaFile(ElencoFile(j))
    '                If UCase(NomeFile) <> "MAX" Then
    '                    CheckBoxList3.Items.Add(NomeFile)
    '                End If
    '            Next j
    '        End If



    '    Catch ex As Exception
    '        Response.Write(ex.Message)
    '    End Try
    'End Function

    Private Function Carica4()
        Try
            Dim NomeFile As String = ""
            CheckBoxList3.Items.Clear()
            Dim J As Long = 0

            i = 0
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/ELABORAZIONI/MAV/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
                ReDim Preserve ElencoFile(i)
                ReDim Preserve ElencoFileData(i)
                ReDim Preserve ElencoFileCancella(i)
                ElencoFile(i) = foundFile
                ElencoFileCancella(i) = foundFile
                ElencoFileData(i) = Format(My.Computer.FileSystem.GetFileInfo(ElencoFile(i)).CreationTime.Date, "dd/MM/yyyy")

                i = i + 1
            Next

            If i > 0 Then
                For j = 0 To i - 1

                    NomeFile = RicavaFile(ElencoFile(j))
                    If UCase(NomeFile) <> "MAX" Then

                        Label2.Text = Label2.Text & "<a href='..\..\ALLEGATI\contratti\elaborazioni\mav\" & NomeFile & "' target='_blank'>" & j + 1 & ") " & NomeFile & " - " & ElencoFileData(j) & "</a><br/>"
                    End If
                Next j
            End If



        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
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



    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try


            Dim a As Integer = 0
            Dim i As Integer = 0
            Dim Vettore(1000) As String

            a = CheckBoxList1.Items.Count.ToString
            While i < a
                If Me.CheckBoxList1.Items(i).Selected = True Then
                    My.Computer.FileSystem.DeleteFile(Server.MapPath("../CONTRATTI/VARIE/") & CheckBoxList1.Items(i).Text)
                End If
                i = i + 1

            End While
            Carica()
            Label1.Text = ""
        Catch ex As Exception
            Label1.Text = ex.Message
            Carica()
        End Try
    End Sub

    'Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
    '    Try


    '        Dim a As Integer = 0
    '        Dim i As Integer = 0
    '        Dim Vettore(1000) As String

    '        a = CheckBoxList2.Items.Count.ToString
    '        While i < a
    '            If Me.CheckBoxList2.Items(i).Selected = True Then
    '                My.Computer.FileSystem.DeleteFile(Server.MapPath("../ANAUT/EXPORT/") & CheckBoxList2.Items(i).Text)
    '            End If
    '            i = i + 1

    '        End While
    '        Carica1()
    '        Label1.Text = ""
    '    Catch ex As Exception
    '        Label1.Text = ex.Message
    '        Carica1()
    '    End Try
    'End Sub

    'Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    Try


    '        Dim a As Integer = 0
    '        Dim i As Integer = 0
    '        Dim Vettore(1000) As String

    '        a = CheckBoxList3.Items.Count.ToString
    '        While i < a
    '            If Me.CheckBoxList3.Items(i).Selected = True Then
    '                My.Computer.FileSystem.DeleteFile(Server.MapPath("../CONTRATTI/STAMPECONTRATTI/") & CheckBoxList3.Items(i).Text)
    '            End If
    '            i = i + 1

    '        End While
    '        Carica2()
    '        Label1.Text = ""
    '    Catch ex As Exception
    '        Label1.Text = ex.Message
    '        Carica2()
    '    End Try
    'End Sub

    'Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
    '    Try


    '        Dim a As Integer = 0
    '        Dim i As Integer = 0
    '        Dim Vettore(1000) As String

    '        a = CheckBoxList4.Items.Count.ToString
    '        While i < a
    '            If Me.CheckBoxList4.Items(i).Selected = True Then
    '                My.Computer.FileSystem.DeleteFile(Server.MapPath("../CONTRATTI/STAMPELETTERE/") & CheckBoxList4.Items(i).Text)
    '            End If
    '            i = i + 1

    '        End While
    '        Carica3()
    '        Label1.Text = ""
    '    Catch ex As Exception
    '        Label1.Text = ex.Message
    '        Carica3()
    '    End Try
    'End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                Carica()
                'Carica1()
                'Carica2()
                'Carica3()
                Carica4()
                Carica5()
                Carica6()
                Carica7()
                Button2.Visible = True
            End If
        End If
    End Sub

    Protected Sub Button7_Click(sender As Object, e As System.EventArgs) Handles Button7.Click
        Try


            Dim a As Integer = 0
            Dim i As Integer = 0
            Dim Vettore(1000) As String

            a = CheckBoxList5.Items.Count.ToString
            While i < a
                If Me.CheckBoxList5.Items(i).Selected = True Then
                    My.Computer.FileSystem.DeleteFile(Server.MapPath("../ALLEGATI/MOROSITA_CONTRATTI/") & CheckBoxList5.Items(i).Text)
                End If
                i = i + 1

            End While
            Carica5()
            Label1.Text = ""
        Catch ex As Exception
            Label1.Text = ex.Message
            Carica5()
        End Try
    End Sub

    Protected Sub Button5_Click(sender As Object, e As System.EventArgs) Handles Button5.Click

    End Sub

    Protected Sub Button3_Click(sender As Object, e As System.EventArgs) Handles Button3.Click

    End Sub
End Class
