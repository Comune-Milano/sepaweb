Imports System.IO

Partial Class Contratti_ElencoSimulazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            EstraiFile()
        End If
    End Sub

    Private Sub EstraiFile()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()
            Dim ElencoFile1()
            Dim pos As Integer
            Dim j As Integer
            Dim ff As Integer


            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf


            lblTbl2.Text = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf _
                          & "<tr height='25px'>" & vbCrLf _
                           & "<td width='250px' height='25px'><font face='Arial' size='2'>File</font></td>" & vbCrLf _
                           & "<td width='250px' height='25px'><font face='Arial' size='2'>Dettagli File</font></td>" & vbCrLf _
                           & "<td width='100px' height='25px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf _
                           & "</tr></table>" & vbCrLf


            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/ELABORAZIONI/IMPOSTE/"), FileIO.SearchOption.SearchTopLevelOnly, "SUC_*.zip")
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = foundFile
                i = i + 1
            Next

            ff = 0
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/ELABORAZIONI/IMPOSTE/"), FileIO.SearchOption.SearchTopLevelOnly, "IMPOSTE*.xls")
                ReDim Preserve ElencoFile1(ff)
                ElencoFile1(ff) = foundFile
                ff = ff + 1
            Next

            Dim kk As Long
            Dim jj As Long
            Dim scambia

            For kk = 0 To i - 2
                For jj = kk + 1 To i - 1
                    If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "_") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "_") + 1, 14)) Then
                        scambia = ElencoFile(kk)
                        ElencoFile(kk) = ElencoFile(jj)
                        ElencoFile(jj) = scambia
                    End If
                Next
            Next

            For kk = 0 To ff - 2
                For jj = kk + 1 To ff - 1
                    If CLng(Mid(RicavaFile(ElencoFile1(kk)), InStr(RicavaFile(ElencoFile1(kk)), "_") + 1, 14)) < CLng(Mid(RicavaFile(ElencoFile1(jj)), InStr(RicavaFile(ElencoFile1(jj)), "_") + 1, 14)) Then
                        scambia = ElencoFile1(kk)
                        ElencoFile1(kk) = ElencoFile1(jj)
                        ElencoFile1(jj) = scambia
                    End If
                Next
            Next

            If i > 0 Then
                For j = 0 To i - 1
                    MiaSHTML = MiaSHTML & "<tr height='25px'>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td height='25px' width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/CONTRATTI/ELABORAZIONI/IMPOSTE/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td height='25px' width='250px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/CONTRATTI/ELABORAZIONI/IMPOSTE/" & Mid(RicavaFile(ElencoFile1(j)), 1, Len(RicavaFile(ElencoFile1(j))) - 4) & ".xls' target='_blank'>" & Mid(RicavaFile(ElencoFile1(j)), 1, Len(RicavaFile(ElencoFile1(j))) - 4) & ".xls" & "</a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td height='25px' width='100px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).LastWriteTime & "</font></td>" & vbCrLf

                    par.cmd.CommandText = "select * FROM SISCOM_MI.RAPPORTI_UTENZA_IMPOSTE_ANN WHERE UPPER(NOME_FILE)='" & UCase(RicavaFile(ElencoFile(j))) & "'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.HasRows = False Then
                        MiaSHTML = MiaSHTML & "<td align='center'  height='25px' width='100px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'><a href='#' onclick=" & Chr(34) & "javascript:AnnullaInvio('" & par.Cripta(RicavaFile(ElencoFile(j))) & "');" & Chr(34) & "><img alt='Annulla Invio' src='../NuoveImm/Elimina.png'/></a></font></td>" & vbCrLf
                    Else
                        MiaSHTML = MiaSHTML & "<td align='center'  height='25px' width='100px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>ANNULLATO</font></td>" & vbCrLf
                    End If
                    myReader.Close()

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
            lblAnnSucc.Text = MiaSHTML

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            lblAnnSucc.Text = ex.Message
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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

    Protected Sub btnHome_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnHome.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnAggiorna_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAggiorna.Click
        EstraiFile()
    End Sub
End Class
