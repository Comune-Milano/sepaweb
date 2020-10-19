
Partial Class ListaNews
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim MioLink As String
    Dim Rilevante As String
    Dim NUMERO_NEWS As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then

            Response.Write("<strong><span style='font-family: Arial'><table border='0' cellpadding cellspacing width='450' background='../IMG/News_1.jpg' height='32'><tr><td height='32'>&nbsp;</tr></table>")


            Response.Write("<table width='450' bgcolor='#DFE2E5' style='border-right: dimgray 1px solid; border-left: dimgray 1px solid;'><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")


            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select WEB_NEWS_ENTI.RILEVANTE,WEB_NEWS_ENTI.LINK,WEB_NEWS_ENTI.ID,WEB_NEWS_ENTI.DATA_V,WEB_NEWS_ENTI.MESSAGGIO_BREVE from WEB_REL_NEWS_ENTI,WEB_NEWS_ENTI where WEB_REL_NEWS_ENTI.ID_ENTE=" & par.IfNull(Session.Item("ID_CAF"), -1) & " and WEB_NEWS_ENTI.ID=WEB_REL_NEWS_ENTI.ID_NEWS  AND WEB_NEWS_ENTI.DATA_V<=" & Format(Now, "yyyyMMdd") & " and WEB_NEWS_ENTI.DATA_F>=" & Format(Now, "yyyyMMdd")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            NUMERO_NEWS = 0
            While myReader.Read
                Response.Write("<tr>")
                NUMERO_NEWS = NUMERO_NEWS + 1
                If par.IfNull(myReader("RILEVANTE"), "0") = "0" Then
                    Rilevante = ""
                Else
                    Rilevante = "<img border='0' src='IMG/Alert.gif' width='17' height='17'>"
                End If
                Response.Write("<td>")
                Response.Write("<span style='font-size: 9pt'><span style='color: #ff0000'>" & Rilevante & par.FormattaData(par.IfNull(myReader("DATA_V"), "")) & "</span>")
                Response.Write("</td>")

                Response.Write("<td>")
                Response.Write("<span style='font-size: 9pt'>" & par.IfNull(myReader("MESSAGGIO_BREVE"), "") & "</span>")
                Response.Write("</td>")

                Response.Write("<td>")
                If par.IfNull(myReader("LINK"), "") = "" Then
                    MioLink = ""
                Else
                    MioLink = "<a href='" & par.IfNull(myReader("LINK"), "") & "' target='_blank'>Link</a>"
                End If
                Response.Write("<span style='font-size: 9pt'><a href=javascript:VisMessaggio(" & par.IfNull(myReader("ID"), "") & ");>Messaggio Completo</a> " & MioLink & "</span>")
                Response.Write("</td>")

                Response.Write("</tr>")
            End While
            Response.Write("<tr><td>&nbsp;</td><td></td><td></td></tr>")
            Session.Item("NEWS") = NUMERO_NEWS
            Response.Write("</table>")

            Response.Write("<table width='450' bgcolor='#DFE2E5' style='border-right: dimgray 1px solid; border-left: dimgray 1px solid; border-bottom: dimgray 1px solid;'><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>")
            Response.Write("</table>")

            Response.Write("</span></strong>")

            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End If
    End Sub
End Class
