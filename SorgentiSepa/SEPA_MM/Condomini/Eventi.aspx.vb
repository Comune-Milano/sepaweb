
Partial Class Condomini_Eventi
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        If IsPostBack = False Then
            Visualizza(Request.QueryString("IDCOND"))
        End If


    End Sub
    Private Function Visualizza(ByVal IdCondominio As String)
        Try
            Dim OPERATORE As String = ""
            Dim MiaData As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)
            'HttpContext.Current.Session.Add("CONNESSIONE", par.OracleConn)

            'par.cmd.CommandText = "SELECT RAPPORTI_UTENZA.COD_CONTRATTO WHERE RAPPORTI_UTENZA.ID=" & IdContratto
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            'Response.Write("CONDOMINI, ID_CONDOMINIO: <strong>" & Codice & "</strong><br />")
            'End If
            'myReader.Close()


            If par.IfEmpty(IdCondominio, 0) > 0 Then

                par.cmd.CommandText = "SELECT CONDOMINI.DENOMINAZIONE AS CONDOMINIO, SEPA.OPERATORI.OPERATORE, TO_CHAR(TO_DATE(EVENTI_CONDOMINI.DATA_ORA,'yyyyMMddHH24MISS'),'DD/MM/YYYY HH:MM:SS') AS DATA_EVENTO, SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS TIPO_EVENTO, EVENTI_CONDOMINI.MOTIVAZIONE FROM SISCOM_MI.EVENTI_CONDOMINI, SEPA.OPERATORI, SISCOM_MI.TAB_EVENTI, SISCOM_MI.CONDOMINI WHERE EVENTI_CONDOMINI.COD_EVENTO = TAB_EVENTI.COD AND EVENTI_CONDOMINI.ID_CONDOMINIO = CONDOMINI.ID AND EVENTI_CONDOMINI.ID_OPERATORE = OPERATORI.ID AND ID_CONDOMINIO = " & IdCondominio & " ORDER BY DATA_ORA DESC"

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)
                DataGridEventi.DataSource = dt
                DataGridEventi.DataBind()

            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception

            par.OracleConn.Close()
            Response.Write(ex.Message)

        End Try
    End Function
End Class
