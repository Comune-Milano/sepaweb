Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.IO

Partial Class ASS_ESTERNA_InviaXML
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try


                Dim MiaSHTML As String

                Dim I As Long


                Dim MIOCOLORE As String




                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                MiaSHTML = "</br></br></br></br></br></br><p><b><font face='Arial' size='2'>Elenco file Inviati</font></b></p><table border='0' cellpadding='1' cellspacing='1' width='500px'>" & vbCrLf
                MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='200px'><font face='Arial' size='2'>Nome del File</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>Data Invio</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='100px'><font size='2' face='Arial'>N. Abbinamenti</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "<td width='100px'><font face='Arial' size='2'><p align='center'>Dettagli</font></td>" & vbCrLf
                MiaSHTML = MiaSHTML & "</tr>" & vbCrLf

                I = 0
                MIOCOLORE = "#CCFFFF"



                par.cmd.CommandText = "select * from DEC_import where id_caf=" & Session.Item("ID_CAF") & " order by data_import desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader.Read()
                    MiaSHTML = MiaSHTML & "<tr>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='200px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & par.IfNull(myReader("nome_file"), "") & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font size='1' face='Arial'>" & Mid(par.IfNull(myReader("data_import"), ""), 7, 2) & "/" & Mid(par.IfNull(myReader("data_import"), ""), 5, 2) & "/" & Mid(par.IfNull(myReader("data_import"), ""), 1, 4) & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'>" & par.IfNull(myReader("n_dichiarazioni"), "") & "</font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "<td width='100px' bgcolor='" & MIOCOLORE & "'><font face='Arial' size='1'><p align='center'><a href='REPORT\" & Mid(par.IfNull(myReader("NOME_FILE"), "SSSSSS"), 1, Len(par.IfNull(myReader("NOME_FILE"), "SSSSSS")) - 3) & "HTML' target='_blank'><img border='0' src='../ImmMaschere/MenuTopDownload.gif'></a></font></td>" & vbCrLf
                    MiaSHTML = MiaSHTML & "</tr>" & vbCrLf
                    If MIOCOLORE = "#CCFFFF" Then
                        MIOCOLORE = "#FFFFCC"
                    Else
                        MIOCOLORE = "#CCFFFF"
                    End If
                End While
                myReader.Close()
                par.OracleConn.Close()
                MiaSHTML = MiaSHTML & "</table>" & vbCrLf
                Response.Write(MiaSHTML)
                ' Button1.Attributes.Add("OnClick", "javascript:Attendi();")
            Catch ex As Exception
                par.OracleConn.Close()
                Response.Write(ex.ToString)
            End Try
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Response.Write("<script>document.location.href=""ScegliXML.aspx""</script>")
    End Sub
End Class
