
Partial Class Contratti_Eventi_1
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Visualizza(CLng(Request.QueryString("ID")), Request.QueryString("cod"))
        End If
    End Sub

    Private Function Visualizza(ByVal IdContratto As Long, ByVal Codice As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""
            Dim dt As New System.Data.DataTable

            par.OracleConn.Open()
            par.SettaCommand(par)
           
            Label1.Text = "CONTRATTO CODICE: " & Codice


            par.cmd.CommandText = "SELECT CAF_WEB.COD_CAF AS ENTE,EVENTI_contratti.ID_OPERATORE,to_char(to_date(SUBSTR(DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy') ||' - '||SUBSTR(DATA_ORA,9,2)||':'||SUBSTR(DATA_ORA,11,2)||':'||SUBSTR(DATA_ORA,13,2) as DATA_ORA_1,TAB_EVENTI.DESCRIZIONE " _
           & ",EVENTI_contratti.COD_EVENTO,EVENTI_contratti.MOTIVAZIONE,OPERATORI.OPERATORE FROM CAF_WEB,siscom_mi.EVENTI_contratti,siscom_mi.TAB_EVENTI," _
           & " SEPA.OPERATORI WHERE EVENTI_contratti.ID_contratto=" & IdContratto _
           & " AND EVENTI_contratti.COD_EVENTO=TAB_EVENTI.COD (+) " _
           & " AND EVENTI_contratti.ID_OPERATORE=OPERATORI.ID (+) AND CAF_WEB.ID=OPERATORI.ID_CAF ORDER BY EVENTI_CONTRATTI.DATA_ORA DESC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "RAPPORTI_UTENZA_IMPOSTE,RAPPORTI_UTENZA_IMPOSTE")
            da.Fill(dt)
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            


            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'Do While myReader.Read()



            '    MiaData = Mid(par.IfNull(myReader("DATA_ORA"), "          "), 7, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 5, 2) & "/" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 1, 4)
            '    If IsDate(MiaData) = True Then
            '        MiaData = MiaData & " " & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 9, 2) & ":" & Mid(par.IfNull(myReader("DATA_ORA"), "          "), 11, 2)
            '    Else
            '        MiaData = ""
            '    End If


            '    OPERATORE = par.IfNull(myReader("OPERATORE"), "")


            '    Response.Write("<tr>")
            '    Response.Write("<td>")
            '    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & MiaData & "</span></td>")
            '    Response.Write("<td>")
            '    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_EVENTO"), "") & " - " & par.IfNull(myReader("DESCRIZIONE"), "") & "</span></td>")
            '    Response.Write("<td>")
            '    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("MOTIVAZIONE"), "") & "</span></td>")
            '    Response.Write("<td>")
            '    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & OPERATORE & "</span></td>")
            '    Response.Write("<td>")
            '    Response.Write("<span style='font-size: 10pt; font-family: Arial'>" & par.IfNull(myReader("COD_CAF"), "") & "</span></td>")

            '    Response.Write("</tr>")

            'Loop
            'myReader.Close()
            'par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
