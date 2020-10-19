
Partial Class Contratti_Anagrafica_EventiAU
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            Visualizza(CLng(par.IfEmpty(Request.QueryString("ID"), "0")))

        End If
    End Sub

    Private Sub Visualizza(ByVal IdAnagrafica As Long)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""
            Dim dt As New System.Data.DataTable

            par.OracleConn.Open()
            par.SettaCommand(par)

            Label1.Text = "EVENTI"
            par.cmd.CommandText = "select * from siscom_mi.anagrafica where id=" & IdAnagrafica
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label1.Text = "EVENTI - " & par.IfNull(myReader("COGNOME"), "") & " " & par.IfNull(myReader("NOME"), "") & " " & par.IfNull(myReader("RAGIONE_SOCIALE"), "")
            End If
            myReader.Close()


            par.cmd.CommandText = "SELECT CAF_WEB.COD_CAF AS ENTE,EVENTI_ANAGRAFICA.ID_OPERATORE,to_char(to_date(SUBSTR(DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy') ||' - '||SUBSTR(DATA_ORA,9,2)||':'||SUBSTR(DATA_ORA,11,2) as DATA_ORA,TAB_EVENTI.DESCRIZIONE " _
           & ",EVENTI_ANAGRAFICA.COD_EVENTO,EVENTI_ANAGRAFICA.MOTIVAZIONE,OPERATORI.OPERATORE FROM CAF_WEB,siscom_mi.EVENTI_ANAGRAFICA,siscom_mi.TAB_EVENTI," _
           & " SEPA.OPERATORI WHERE EVENTI_ANAGRAFICA.ID_ANAGRAFICA=" & IdAnagrafica _
           & " AND EVENTI_ANAGRAFICA.COD_EVENTO=TAB_EVENTI.COD (+) " _
           & " AND EVENTI_ANAGRAFICA.ID_OPERATORE=OPERATORI.ID (+) AND CAF_WEB.ID=OPERATORI.ID_CAF ORDER BY EVENTI_ANAGRAFICA.DATA_ORA DESC"


            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim ds As New Data.DataSet()
            da.Fill(ds, "EVENTI_ANAGRAFICA,EVENTI_ANAGRAFICA")
            da.Fill(dt)
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()




            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Sub

    

   
End Class
