
Partial Class SIRAPER_Procedure
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", False)
            Exit Sub
        End If
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            apriDettagli()
        End If
    End Sub
    Private Sub apriDettagli()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT ID, (SELECT OPERATORE FROM OPERATORI WHERE ID = ID_OPERATORE) AS OPERATORE, " _
                                & "DECODE(TIPO, 1, 'Elaborazione Siraper', 2, 'Controlli Siraper', 3, 'Creazione XML Siraper') AS TIPO, " _
                                & "TO_CHAR(TO_DATE(DATA_ORA_INIZIO,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS INIZIO," _
                                & "TO_CHAR(TO_DATE(DATA_ORA_FINE,'YYYYMMDDHH24MISS'),'DD/MM/YYYY-HH24:MI:SS') AS FINE, ERRORE," _
                                & "(CASE WHEN ESITO=0 THEN 'IN CORSO' WHEN ESITO=1 THEN 'TERMINATA' WHEN ESITO=2 THEN 'INTERROTTA PER ERRORE' ELSE '' END) AS ESITO, " _
                                & "ROUND(PARZIALE/TOTALE*100)||' %' AS PERCENTUALE " _
                                & "FROM SISCOM_MI.SIRAPER_PROCEDURE " _
                                & "ORDER BY DATA_ORA_INIZIO DESC "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            dgvElaborazioni.DataSource = dt
            dgvElaborazioni.DataBind()
            connData.chiudi()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: SIRAPER_Procedure - apriDettagli - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAggiorna_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAggiorna.Click
        Response.Redirect("Procedure.aspx", False)
    End Sub
End Class
