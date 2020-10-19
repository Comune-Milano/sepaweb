
Partial Class SICUREZZA_ElencoFascicoli
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then
                CType(Me.Master.FindControl("NavigationMenu"), Telerik.Web.UI.RadMenu).Visible = False
                EstraiFile()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Sicurezza - Gestione Gruppi - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub EstraiFile()
        Try
            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()

            Dim pos As Integer
            Dim j As Integer
            Dim ff As Integer

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='99%'>" & vbCrLf

            lblTbl.Text = "<table border='0' cellpadding='1' cellspacing='1' width='99%'>" & vbCrLf _
                          & "<tr>" & vbCrLf _
                           & "<td width='70%'><font face='Arial' size='2'>File PDF</font></td>" & vbCrLf _
                           & "<td><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf _
                           & "</tr></table>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/SICUREZZA/"), FileIO.SearchOption.SearchTopLevelOnly, "SEC_*.pdf")
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = foundFile
                i = i + 1
            Next


            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "_") + 22, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "_") + 22, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next


            If i > 0 Then
                For j = 0 To i - 1
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='70%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/SICUREZZA/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

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
            lblFascicoli.Text = MiaSHTML

        Catch ex As Exception
            lblFascicoli.Text = ex.Message
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

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "chiudi", "validNavigation=true;self.close();", True)
    End Sub
End Class
