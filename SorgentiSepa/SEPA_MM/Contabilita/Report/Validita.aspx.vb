
Partial Class Contabilita_Report_Validita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            caricaDati()
        End If
    End Sub
    Private Sub caricaDati()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = " SELECT " _
                & " T_VOCI_BOLLETTA_TIPI.DESCRIZIONE AS TIPO, " _
                & " TO_CHAR(TO_DATE(VALIDITA_DA,'YYYYMMDD'),'DD/MM/YYYY') AS VALIDITA_DA, " _
                & " TO_CHAR(TO_DATE(VALIDITA_A,'YYYYMMDD'),'DD/MM/YYYY') AS VALIDITA_A, " _
                & " T_VOCI_BOLLETTA_CAP.DESCRIZIONE AS CAPITOLO, " _
                & " DECODE(USO,1,'ABITATIVO-NON ABITATIVO',2,'ABITATIVO',3,'NON ABITATIVO','') AS USO, " _
                & " T_VOCI_BOLLETTA_COMPETENZA.DESCRIZIONE AS COMPETENZA, " _
                & " '' AS ELIMINA,T_VOCI_BOLLETTA_TIPI_CAP.ID " _
                & " FROM SISCOM_MI.T_VOCI_BOLLETTA_TIPI_CAP,SISCOM_MI.T_VOCI_BOLLETTA_CAP, " _
                & " SISCOM_MI.T_VOCI_BOLLETTA_COMPETENZA,SISCOM_MI.T_VOCI_BOLLETTA_TIPI " _
                & " WHERE T_VOCI_BOLLETTA_TIPI_CAP.ID_TIPO = T_VOCI_BOLLETTA_TIPI.ID " _
                & " AND T_VOCI_BOLLETTA_CAP.ID=T_VOCI_BOLLETTA_TIPI_CAP.ID_CAPITOLO " _
                & " AND T_VOCI_BOLLETTA_COMPETENZA.ID=T_VOCI_BOLLETTA_TIPI_CAP.COMPETENZA "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGrid1.DataSource = dt
                DataGrid1.DataBind()
                controllaEliminazione()
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore nel caricamento dei capitoli!');", True)
        End Try
    End Sub

    Protected Sub ImageButtonAggiungiCapitolo_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonAggiungiCapitolo.Click
        Response.Redirect("NuovaValidita.aspx")
    End Sub

    Protected Sub ImageButtonEsci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButtonEsci.Click
        Response.Redirect("../pagina_home.aspx")
    End Sub
    Private Sub controllaEliminazione()
        For Each item As DataGridItem In DataGrid1.Items
            item.Cells(6).Text = "<table border=""0""><tr><td><img src=""../../NuoveImm/matita.png"" style=""cursor: pointer"" onclick=""javascript:modifica(" & item.Cells(7).Text & ");void(0);"" alt=""elimina"" width=""16"" height=""16"" />" _
                & "</td><td><img src=""../../NuoveImm/Elimina.png"" style=""cursor: pointer"" onclick=""javascript:elimina(" & item.Cells(7).Text & ");void(0);"" alt=""elimina"" /></td></tr></table>"
        Next
    End Sub

    Protected Sub ButtonElimina_Click(sender As Object, e As System.EventArgs) Handles ButtonElimina.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "DELETE FROM SISCOM_MI.t_voci_bolletta_tipi_cap WHERE ID=" & HiddenFieldId.Value
            par.cmd.ExecuteNonQuery()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            caricaDati()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType, "msg", "alert('Errore durante l\'eliminazione del capitolo!');", True)
        End Try
    End Sub
End Class
