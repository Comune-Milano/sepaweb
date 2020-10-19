
Partial Class News
    Inherits PageSetIdMode
    Public psNews As String
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim ID As Long
        Dim Tipo As String
        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            psNews = ""

            ID = Request.QueryString("id")
            Tipo = Request.QueryString("T")
            If ID = -1 Then 'pubbliche
                par.cmd.CommandText = "select WEB_NEWS_PUBBLICHE.* from WEB_NEWS_PUBBLICHE where WEB_NEWS_PUBBLICHE.DATA_V<='" & Format(Now, "yyyyMMdd") & "' and WEB_NEWS_PUBBLICHE.DATA_F>='" & Format(Now, "yyyyMMdd") & "' order by data_v desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()
                    psNews = psNews & "<h1>" & par.IfNull(myReader("messaggio_breve"), "") & "</h1><div class='service_box'><h3>" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & "</h3><p>" & par.IfNull(myReader("messaggio_lungo"), "") & "</p> </div>"
                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            If ID = -2 Then 'private
                par.cmd.CommandText = "select WEB_NEWS_ENTI.* from WEB_REL_NEWS_ENTI,WEB_NEWS_ENTI where WEB_REL_NEWS_ENTI.ID_ENTE=" & Session.Item("ID_CAF") & " and WEB_NEWS_ENTI.ID=WEB_REL_NEWS_ENTI.ID_NEWS  AND WEB_NEWS_ENTI.DATA_V<='" & Format(Now, "yyyyMMdd") & "' and WEB_NEWS_ENTI.DATA_F>='" & Format(Now, "yyyyMMdd") & "' order by data_v desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()
                    psNews = psNews & "<h1>" & par.IfNull(myReader("messaggio_breve"), "") & "</h1><div class='service_box'><h3>" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & "</h3><p>" & par.IfNull(myReader("messaggio_lungo"), "") & "</p> </div>"
                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            If ID >= 0 And Tipo = 0 Then 'pubbliche
                par.cmd.CommandText = "select WEB_NEWS_PUBBLICHE.* from WEB_NEWS_PUBBLICHE where WEB_NEWS_PUBBLICHE.id=" & ID & " and WEB_NEWS_PUBBLICHE.DATA_V<='" & Format(Now, "yyyyMMdd") & "' and WEB_NEWS_PUBBLICHE.DATA_F>='" & Format(Now, "yyyyMMdd") & "' order by data_v desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()
                    psNews = psNews & "<h1>" & par.IfNull(myReader("messaggio_breve"), "") & "</h1><div class='service_box'><h3>" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & "</h3><p>" & par.IfNull(myReader("messaggio_lungo"), "") & "</p> </div>"
                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            If ID >= 0 And Tipo = 1 Then 'pubbliche
                par.cmd.CommandText = "select WEB_NEWS_ENTI.* from WEB_REL_NEWS_ENTI,WEB_NEWS_ENTI where WEB_NEWS_ENTI.id=" & ID & " and WEB_REL_NEWS_ENTI.ID_ENTE=" & Session.Item("ID_CAF") & " and WEB_NEWS_ENTI.ID=WEB_REL_NEWS_ENTI.ID_NEWS  AND WEB_NEWS_ENTI.DATA_V<='" & Format(Now, "yyyyMMdd") & "' and WEB_NEWS_ENTI.DATA_F>='" & Format(Now, "yyyyMMdd") & "' order by data_v desc"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                Do While myReader.Read()
                    psNews = psNews & "<h1>" & par.IfNull(myReader("messaggio_breve"), "") & "</h1><div class='service_box'><h3>" & par.FormattaData(par.IfNull(myReader("data_v"), "")) & "</h3><p>" & par.IfNull(myReader("messaggio_lungo"), "") & "</p> </div>"
                Loop
                myReader.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "NEWS - " & ex.Message)
            Response.Write("<script>top.location.href='/Errori/Errore.aspx';</script>")
        End Try
    End Sub
End Class
