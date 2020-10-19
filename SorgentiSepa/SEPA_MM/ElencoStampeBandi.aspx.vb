
Partial Class ANAUT_Stampe_ElencoStampe
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("V") <> par.Cripta(Format(Now, "yyyyMMdd") & "SEPA") Then
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
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
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("ALLEGATI/BANDI_ERP/STAMPE/"), FileIO.SearchOption.SearchTopLevelOnly, "*_" & Request.QueryString("ID") & "-*.pdf")

                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = foundFile
                i = i + 1

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
                        Case "00"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='ALLEGATI/BANDI_ERP/STAMPE/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Dichiarazione</a></font></td>" & vbCrLf
                        Case "01"
                            MiaSHTML = MiaSHTML & "<td width='500px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='ALLEGATI/BANDI_ERP/STAMPE/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>Domanda di Bando</a></font></td>" & vbCrLf
                        
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
