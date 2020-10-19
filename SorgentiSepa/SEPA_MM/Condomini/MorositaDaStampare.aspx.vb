
Partial Class Condomini_MorositaDaStampare
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim dt As New Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Str As String
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:500; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"


        Response.Write(Str)

        If Not IsPostBack Then
            Response.Flush()

            Cerca()
        End If
    End Sub
    Private Sub Cerca()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'query con dati relativi alla morosita
            'par.cmd.CommandText = "SELECT distinct CONDOMINI.ID,CONDOMINI.DENOMINAZIONE,(COND_AMMINISTRATORI.COGNOME ||' ' || COND_AMMINISTRATORI.NOME)AS AMMINISTRATORE,('DAL '||" _
            '& " TO_CHAR(TO_DATE(RIF_DA,'yyyymmdd'),'dd/mm/yyyy') ||' AL ' ||TO_CHAR(TO_DATE(RIF_A,'yyyymmdd'),'dd/mm/yyyy')) AS RIFERIMENTO,TO_CHAR(TO_DATE(DATA_RICHIESTA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_RICHIESTA" _
            '& " ,COND_MOROSITA.ID FROM SISCOM_MI.CONDOMINI, SISCOM_MI.COND_MOROSITA, SISCOM_MI.COND_AMMINISTRATORI" _
            '& " WHERE CONDOMINI.ID = COND_MOROSITA.ID_CONDOMINIO AND COND_MOROSITA.ID_AMMINISTRATORE = COND_AMMINISTRATORI.ID"

            'QUERY CON SOLO I DATI DEI CONDOMINI PER I QUALI è NECESSASRIO STAMAPRE I MAV
            par.cmd.CommandText = "SELECT CONDOMINI.ID,CONDOMINI.DENOMINAZIONE,(COND_AMMINISTRATORI.COGNOME ||' ' || COND_AMMINISTRATORI.NOME)AS AMMINISTRATORE " _
            & "FROM SISCOM_MI.CONDOMINI, SISCOM_MI.COND_AMMINISTRAZIONE, SISCOM_MI.COND_AMMINISTRATORI " _
            & " WHERE CONDOMINI.ID = COND_AMMINISTRAZIONE.ID_CONDOMINIO AND COND_AMMINISTRAZIONE.ID_AMMINISTRATORE = COND_AMMINISTRATORI.ID AND COND_AMMINISTRAZIONE.DATA_FINE IS NULL AND CONDOMINI.ID IN " _
            & " (SELECT DISTINCT id_condominio FROM siscom_mi.cond_morosita_inquilini,siscom_mi.cond_morosita WHERE COND_MOROSITA.FL_COMPLETO = 1 AND cond_morosita.ID = cond_morosita_inquilini.id_morosita AND" _
            & " id_intestatario NOT IN (SELECT id_anagrafica FROM siscom_mi.cond_morosita_lettere where bollettino is not null)) order by denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            da.Fill(dt)


            For Each row As Data.DataRow In dt.Rows
                row.Item("DENOMINAZIONE") = "<a href=""javascript:parent.main.location.replace('Condominio.aspx?IdCond=" & row.Item("ID") & "&CALL=MorositaDaStampare');"">" & row.Item("DENOMINAZIONE") & "</a>"
            Next
            DataGridMorDaStampare.DataSource = dt
            DataGridMorDaStampare.DataBind()

            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")

    End Sub
End Class
