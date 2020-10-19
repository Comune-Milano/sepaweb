
Partial Class Contratti_ElencoElaborazMassive
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            EstraiFile()
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

    Private Sub EstraiFile()
        Try
            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()
            Dim pos As Integer
            Dim j As Integer

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='620px'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/ELABORAZIONI/"), FileIO.SearchOption.SearchTopLevelOnly, "Elaboraz_Massiva_Gestionali_" & "*.xls")
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = foundFile
                i = i + 1
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 29, 15)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 29, 15)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If i > 0 Then
                For j = 0 To i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/CONTRATTI/ELABORAZIONI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & Mid(RicavaFile(ElencoFile(j)), 1, 28) & Format(Date.Parse(par.FormattaData((Mid(RicavaFile(ElencoFile(j)), 29, 8)))), "d_MMM_yyyy") & Mid(RicavaFile(ElencoFile(j)), 43, 4) & "</a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

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
            lblReport.Text = MiaSHTML

        Catch ex As Exception
            lblReport.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
End Class
