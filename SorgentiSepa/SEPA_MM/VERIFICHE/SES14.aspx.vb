
Partial Class AMMSEPA_Controllo_stampe
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

        End If
    End Sub

    Private Function Carica()
        Try
            Dim j As Long = 0

            Dim NomeFile As String = ""
            CheckBoxList1.Items.Clear()

            i = 0

            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../CONTRATTI/stampecontratti/"), FileIO.SearchOption.SearchTopLevelOnly, "*.*")
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
                        CheckBoxList1.Items.Add(NomeFile)
                    End If
                Next j
            End If
            'MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            'Response.Write(MiaSHTML)


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

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim a As Integer = 0
        Dim i As Integer = 0

        a = CheckBoxList1.Items.Count.ToString
        While i < a
            Me.CheckBoxList1.Items(i).Selected = True
            i = i + 1
        End While
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try


            Dim a As Integer = 0
            Dim i As Integer = 0
            Dim Vettore(1000) As String

            a = CheckBoxList1.Items.Count.ToString
            While i < a
                If Me.CheckBoxList1.Items(i).Selected = True Then
                    My.Computer.FileSystem.DeleteFile(Server.MapPath("../CONTRATTI/stampecontratti/") & CheckBoxList1.Items(i).Text)
                End If
                i = i + 1

            End While
            Carica()
        Catch ex As Exception
            Label1.Text = ex.Message
            Carica()
        End Try
    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                CheckBoxList1.Visible = True
                Button1.Visible = True
                Button2.Visible = True
                Carica()
            End If
        End If
    End Sub
End Class
