
Partial Class AMMSEPA_Connessioni
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Function Carica()
        Try
            Dim i As Integer = 0
            Dim TestoRidotto As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

            Response.Write("<table BORDER=1px width=100%>")
            If Request.QueryString("RC") = "SS2015" Then
                Response.Write("<tr><td></td><td>SID</td><td>SERIAL</td><td>USER</td><td>USERNAME</td><td>STATUS</td><td>SCHEMA</td><td>OSUSER</td><td>MACHINE</td><td>TERMINAL</td><td>PROGRAM</td><td>LOGON_TIME</td><td>SQL BREVE</td><td>SQL COMPLETO</td><td>OPERAZIONE</td></tr>")
            Else
                Response.Write("<tr><td></td><td>SID</td><td>SERIAL</td><td>USER</td><td>USERNAME</td><td>STATUS</td><td>SCHEMA</td><td>OSUSER</td><td>MACHINE</td><td>TERMINAL</td><td>PROGRAM</td><td>LOGON_TIME</td><td>SQL BREVE</td><td>SQL COMPLETO</td></tr>")
            End If

            par.cmd.CommandText = "SELECT   s.prev_hash_value,s.saddr, s.SID, s.serial#, s.audsid, s.paddr, s.user#, s.username, s.ownerid,  s.lockwait, s.status, s.server,         s.schemaname, s.osuser, s.machine, s.terminal,         UPPER (s.program) program, s.TYPE, s.sql_address,         s.sql_id, s.sql_child_number, s.prev_sql_addr, s.prev_hash_value,         s.prev_sql_id, s.prev_child_number, s.module,         s.row_wait_obj#, s.row_wait_file#, s.row_wait_block#,         s.row_wait_row#, s.logon_time, s.last_call_et, s.pdml_enabled,         s.resource_consumer_group, s.pdml_status, s.pddl_status, s.pq_status,         s.current_queue_duration, s.client_identifier,         s.blocking_session_status, s.blocking_instance, s.blocking_session,         s.wait_class, s.wait_time, s.seconds_in_wait, s.state,         s.service_name    FROM v$session s   WHERE ((s.username IS NOT NULL)          AND (NVL (s.osuser, 'x') <> 'SYSTEM')          AND (s.TYPE <> 'BACKGROUND')		  AND (SCHEMANAME IN ('SEPA','SISCOM_MI'))         ) ORDER BY ""PROGRAM"", ownerid,LOGON_TIME"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            i = 1
            Do While myReader.Read
                TestoRidotto = ""
                par.cmd.CommandText = "SELECT   sql_text  FROM v$sqltext_with_newlines  WHERE hash_value = " & par.IfNull(myReader("prev_hash_value"), "0") & " ORDER BY piece"
                myReader1 = par.cmd.ExecuteReader()
                Do While myReader1.Read
                    TestoRidotto = TestoRidotto & par.IfNull(myReader1("sql_text"), "0") & " "
                Loop
                myReader1.Close()

                If Request.QueryString("RC") = "SS2015" Then
                    Response.Write("<tr><td>" & i & "</td><td>" & par.IfNull(myReader("SID"), "") & "</td><td>" & par.IfNull(myReader("SERIAL#"), "") & "</td><td>" & par.IfNull(myReader("USER#"), "") & "</td><td>" & par.IfNull(myReader("USERNAME"), "") & "</td><td>" & par.IfNull(myReader("STATUS"), "") & "</td><td>" & par.IfNull(myReader("SCHEMANAME"), "") & "</td><td>" & par.IfNull(myReader("OSUSER"), "") & "</td><td>" & par.IfNull(myReader("MACHINE"), "") & "</td><td>" & par.IfNull(myReader("TERMINAL"), "") & "</td><td>" & par.IfNull(myReader("PROGRAM"), "") & "</td><td>" & par.IfNull(myReader("LOGON_TIME"), "") & "</td><td>" & Mid(TestoRidotto, 1, 200) & "</td><td><a href=" & Chr(34) & "SES1_1.aspx?ID=" & par.Cripta(par.IfNull(myReader("prev_hash_value"), "0")) & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Visualizza</a></td><td><a href=" & Chr(34) & "SES1_2.aspx?SID=" & par.Cripta(par.IfNull(myReader("SID"), "")) & "&SER=" & par.Cripta(par.IfNull(myReader("SERIAL#"), "")) & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Elimina</a></td></tr>")
                Else
                    Response.Write("<tr><td>" & i & "</td><td>" & par.IfNull(myReader("SID"), "") & "</td><td>" & par.IfNull(myReader("SERIAL#"), "") & "</td><td>" & par.IfNull(myReader("USER#"), "") & "</td><td>" & par.IfNull(myReader("USERNAME"), "") & "</td><td>" & par.IfNull(myReader("STATUS"), "") & "</td><td>" & par.IfNull(myReader("SCHEMANAME"), "") & "</td><td>" & par.IfNull(myReader("OSUSER"), "") & "</td><td>" & par.IfNull(myReader("MACHINE"), "") & "</td><td>" & par.IfNull(myReader("TERMINAL"), "") & "</td><td>" & par.IfNull(myReader("PROGRAM"), "") & "</td><td>" & par.IfNull(myReader("LOGON_TIME"), "") & "</td><td>" & Mid(TestoRidotto, 1, 200) & "</td><td><a href=" & Chr(34) & "SES1_1.aspx?ID=" & par.Cripta(par.IfNull(myReader("prev_hash_value"), "0")) & Chr(34) & " target=" & Chr(34) & "_blank" & Chr(34) & ">Visualizza</a></td></tr>")
                End If

                i = i + 1
            Loop
            Response.Write("</table>")
            myReader.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Carica()
    End Sub

    Protected Sub Button6_Click(sender As Object, e As System.EventArgs) Handles Button6.Click

        If InStr(TextBox1.Text, "'") = 0 Then
            If par.VerificaPW(TextBox1.Text) = True Then
                Carica()
                Button1.Visible = True
                TextBox1.Visible = False
                Button6.Visible = False
            End If
        End If
    End Sub
End Class
