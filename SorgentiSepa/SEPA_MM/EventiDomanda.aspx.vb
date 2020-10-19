
Partial Class EventiDomanda
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Private Sub EventiDomanda_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If IsPostBack = False Then
            Visualizza(CLng(Request.QueryString("ID")), Request.QueryString("PG"))
        End If
    End Sub

    Private Function Visualizza(ByVal IdDomanda As Long, ByVal Codice As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""
            Dim dt As New System.Data.DataTable

            par.OracleConn.Open()
            par.SettaCommand(par)

            Label1.Text = "DOMANDA PG: " & Codice



            par.cmd.CommandText = "SELECT CAF_WEB.COD_CAF AS ENTE,EVENTI_BANDI.ID_OPERATORE,to_char(to_date(SUBSTR(DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy') ||' - '||SUBSTR(DATA_ORA,9,2)||':'||SUBSTR(DATA_ORA,11,2)||':'||SUBSTR(DATA_ORA,13,2) as DATA_ORA_1,TAB_EVENTI.DESCRIZIONE ,EVENTI_BANDI.COD_EVENTO,EVENTI_BANDI.MOTIVAZIONE,OPERATORI.OPERATORE FROM CAF_WEB,EVENTI_BANDI,TAB_EVENTI, SEPA.OPERATORI WHERE EVENTI_BANDI.ID_DOMANDA=" & IdDomanda & " AND EVENTI_BANDI.COD_EVENTO=TAB_EVENTI.COD (+)  AND EVENTI_BANDI.ID_OPERATORE=OPERATORI.ID (+) AND CAF_WEB.ID=OPERATORI.ID_CAF ORDER BY EVENTI_BANDI.DATA_ORA DESC"

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,DOMANDE_BANDO")
            da.Fill(dt)
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()




            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
