Imports System.IO

Partial Class ANAUT_ElencoLettereConvocazioni
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Private Function CaricaLista()

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim MiaSHTML As String
            Dim MIOCOLORE As String
            Dim i As Integer
            Dim ElencoFile()
            Dim pos As Integer
            Dim j As Integer

            Dim Struttura As String = ""
            If cmbFiliale.SelectedItem.Text = "TUTTI GLI SPORTELLI" Then
                Struttura = ""
            Else
                Struttura = " where descrizione like '" & Format(CInt(cmbFiliale.SelectedItem.Value), "000") & "%' "
            End If

            MiaSHTML = "<table border='0' cellpadding='1' cellspacing='1' width='100%'>" & vbCrLf
            MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='60%'><font face='Arial' size='2'>File</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='20%'><font size='2' face='Arial'>Data Creazione</font></td>" & vbCrLf
            MiaSHTML = MiaSHTML & "<td width='20%'><font size='2' face='Arial'>Data Spedizione</font></td>" & vbCrLf
            If Session.Item("MOD_AU_ELIMINA_F_CONV") = "1" Then
                MiaSHTML = MiaSHTML & "<td width='20%'><font size='2' face='Arial'>Elimina</font></td>" & vbCrLf
            End If
            MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

            MIOCOLORE = "#CCFFFF"
            par.cmd.CommandText = "SELECT  * FROM siscom_mi.CONVOCAZIONI_AU_STAMPE " & Struttura & " order by substr(descrizione,instr(descrizione,'.ZIP')-14,14)"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Do While myReader.Read
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/ANAGRAFE_UTENZA/CONVOCAZIONI/" & par.IfNull(myReader("DESCRIZIONE"), "") & "' target='_blank'>" & Mid(par.IfNull(myReader("DESCRIZIONE"), ""), 5, Len(par.IfNull(myReader("DESCRIZIONE"), "")) - 17) & "</a></font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(Mid(par.IfNull(myReader("DESCRIZIONE"), ""), InStr(par.IfNull(myReader("DESCRIZIONE"), ""), ".ZIP") - 14, 14)) & "</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_STAMPA"), "")) & "</font></td>" & vbCrLf
                If Session.Item("MOD_AU_ELIMINA_F_CONV") = "1" Then
                    MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "Eliminafile('" & par.IfNull(myReader("DESCRIZIONE"), "") & "');" & Chr(34) & ">Elimina</a></font></td>" & vbCrLf
                End If
                MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                If MIOCOLORE = "#CCFFFF" Then
                    MIOCOLORE = "#FFFFCC"
                Else
                    MIOCOLORE = "#CCFFFF"
                End If
            Loop
            myReader.Close()




            'i = 0
            'MIOCOLORE = "#CCFFFF"
            'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/ANAGRAFE_UTENZA/CONVOCAZIONI/"), FileIO.SearchOption.SearchTopLevelOnly, "*" & Struttura & "_*.zip")
            '    ReDim Preserve ElencoFile(i)
            '    ElencoFile(i) = foundFile
            '    i = i + 1
            'Next

            'Dim kk As Long
            'Dim jj As Long
            'Dim scambia

            'For kk = 0 To i - 2
            '    For jj = kk + 1 To i - 1
            '        'If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "-") + 1, 8)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "-") + 1, 8)) Then
            '        If CLng(Mid(RicavaFile(ElencoFile(kk)), Len(RicavaFile(ElencoFile(kk))) - 11, 8)) < CLng(Mid(RicavaFile(ElencoFile(jj)), Len(RicavaFile(ElencoFile(jj))) - 11, 8)) Then
            '            scambia = ElencoFile(kk)
            '            ElencoFile(kk) = ElencoFile(jj)
            '            ElencoFile(jj) = scambia
            '        End If
            '    Next
            'Next


            'If i > 0 Then
            '    For j = 0 To i - 1
            '        MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
            '        MiaSHTML = MiaSHTML & "<td width='60%' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><a href='../ALLEGATI/ANAGRAFE_UTENZA/CONVOCAZIONI/" & RicavaFile(ElencoFile(j)) & "' target='_blank'>" & Mid(RicavaFile(ElencoFile(j)), 5, Len(RicavaFile(ElencoFile(j))) - 17) & "</a></font></td>" & vbCrLf
            '        MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & My.Computer.FileSystem.GetFileInfo(ElencoFile(j)).CreationTime & "</font></td>" & vbCrLf

            '        par.cmd.CommandText = "SELECT  * FROM SISCOM_MI.CONVOCAZIONI_AU_STAMPE WHERE DESCRIZIONE='" & par.PulisciStrSql(RicavaFile(ElencoFile(j))) & "'"
            '        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReader.HasRows = True Then
            '            If myReader.Read Then
            '                MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & par.FormattaData(par.IfNull(myReader("DATA_STAMPA"), "")) & "</font></td>" & vbCrLf
            '                If Session.Item("MOD_AU_ELIMINA_F_CONV") = "1" Then
            '                    MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'><a href='javascript:void(0)' onclick=" & Chr(34) & "Eliminafile('" & RicavaFile(ElencoFile(j)) & "');" & Chr(34) & ">Elimina</a></font></td>" & vbCrLf
            '                End If
            '            End If
            '        Else
            '            MiaSHTML = MiaSHTML & "<td width='20%' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>---</font></td>" & vbCrLf
            '        End If
            '        myReader.Close()


            '        MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
            '        If MIOCOLORE = "#CCFFFF" Then
            '            MIOCOLORE = "#FFFFCC"
            '        Else
            '            MIOCOLORE = "#CCFFFF"
            '        End If

            '    Next j
            'End If
            MiaSHTML = MiaSHTML & "</table>" & vbCrLf
            Label3.Text = MiaSHTML

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label3.Text = ex.Message
        End Try
       
    End Function

    Private Function RicavaFiliale(ByVal file As String) As String
        Try
            RicavaFiliale = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & CInt(Mid(file, 10, 3))

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                RicavaFiliale = myReader2("NOME")
            End If
            myReader2.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            If Session.Item("MOD_AU_CONV_VIS_TUTTO") = "1" Then
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM UTENZA_SPORTELLI WHERE FL_ELIMINATO=0 order by descrizione asc", "DESCRIZIONE", "ID")
                cmbFiliale.Items.Add("TUTTI GLI SPORTELLI")
                cmbFiliale.Items.FindByText("TUTTI GLI SPORTELLI").Selected = True
            Else
                par.RiempiDList(Me, par.OracleConn, "cmbFiliale", "SELECT * FROM UTENZA_SPORTELLI WHERE ID_FILIALE IN (SELECT ID FROM UTENZA_FILIALI WHERE FL_ELIMINATO=0 AND ID_STRUTTURA IN (SELECT ID FROM siscom_mi.tab_filiali where id in (select id_UFFICIO from operatori where id=" & Session.Item("ID_OPERATORE") & "))) order by DESCRIZIONE asc", "DESCRIZIONE", "ID")
                cmbFiliale.Enabled = False
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            CaricaLista()
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



    Protected Sub cmbFiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        CaricaLista()
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnElimina.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "select * from SISCOM_MI.CONVOCAZIONI_AU_STAMPE WHERE DESCRIZIONE='" & nomeFile.Value & "'"
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.CONVOCAZIONI_AU_GRUPPI SET FL_STAMPATA=0 WHERE ID=" & myReader2("ID_GRUPPO")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.CONVOCAZIONI_AU_LETTERE WHERE IDENTIFICATIVO='" & myReader2("IDENTIFICATIVO") & "'"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.CONVOCAZIONI_AU_STAMPE WHERE DESCRIZIONE='" & nomeFile.Value & "'"
                par.cmd.ExecuteNonQuery()

                File.Delete(Server.MapPath("../ALLEGATI/ANAGRAFE_UTENZA/CONVOCAZIONI/") & nomeFile.Value)
            End If
            myReader2.Close()

            par.myTrans.Commit()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            Response.Write("<script>alert('Operazione effettuata');</script>")
            CaricaLista()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label3.Text = ex.Message
        End Try
    End Sub
End Class
