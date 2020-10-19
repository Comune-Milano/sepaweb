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
            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile() as string

            Dim j As Integer


            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='700px'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='400px'><font face='Arial' size='2'>Dettagli Bollette</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='150px'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
            ' MiaSHTML = MiaSHTML & "<td width='200px'><font size='2' face='Arial'>File per Banca</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='150px'><font size='2' face='Arial'>Annulla</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            i = 0
            MIOCOLORE = "#CCFFFF"
            For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/CONTRATTI/ELABORAZIONI/"), FileIO.SearchOption.SearchTopLevelOnly, "EMISSIONE_*.zip")
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
                    MiaSHTML = MiaSHTML & "<td width='400px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & j + 1 & ")<a href='../ALLEGATI/CONTRATTI/ELABORAZIONI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & RicavaFile(ElencoFile(j)) & "</a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf
                    'MiaSHTML = MiaSHTML & "<td width='200px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>XXXXXXXXXXXX</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='150px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'><a href=" & Chr(34) & "javascript:document.getElementById('DaAnnullare').value='" & RicavaFile(ElencoFile(j)) & "';ConfermaAnnullo();" & Chr(34) & ">Annulla</a></font></td>" & vbCrLf

                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If

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

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnConfermaAnnullo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfermaAnnullo.Click
        Try
            Response.Write("<script>alert('Operazione NON DISPONIBILE!');</script>")

            'Dim TROVATO As Boolean = False
            'par.OracleConn.Open()
            'par.SettaCommand(par)
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘‘par.cmd.Transaction = par.myTrans

            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.BOL_BOLLETTE WHERE RIF_FILE_TXT='" & Mid(DaAnnullare.Value, 1, Len(DaAnnullare.Value) - 4) & "'"
            'Dim myReaderJ As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Do While myReaderJ.Read
            '    If par.IfNull(myReaderJ("RIF_FILE"), "") = "" And par.IfNull(myReaderJ("FL_ANNULLATA"), "") = "0" Then
            '        par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='1' WHERE ID=" & par.IfNull(myReaderJ("ID"), "-1")
            '        par.cmd.ExecuteNonQuery()

            '    Else
            '        If par.IfNull(myReaderJ("FL_ANNULLATA"), "") = "0" Then
            '            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA='2' WHERE ID=" & par.IfNull(myReaderJ("ID"), "-1")
            '            par.cmd.ExecuteNonQuery()
            '            TROVATO = True
            '        End If
            '    End If
            'Loop




            'par.myTrans.Commit()
            'par.OracleConn.Close()
            'If TROVATO = True Then
            '    Response.Write("<script>alert('Operazione effettuata. Utilizzare la funzione ANNULLI per generare il file da inviare alla banca!');</script>")
            'Else
            '    Response.Write("<script>alert('Operazione effettuata!');</script>")
            'End If

        Catch ex As Exception
            'par.myTrans.Rollback()
            'par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:Annullo Emissione - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
