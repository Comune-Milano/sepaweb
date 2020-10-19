
Partial Class ARPA_LOMBARDIA_Log
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridLog.ClientID.ToString.Replace("ctl00", "MasterHomePage")
        End If
    End Sub
    Protected Sub RadGridLog_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridLog.NeedDataSource
        Try
            Dim Query As String = "SELECT " & CType(Me.Master, Object).StringaSiscom & "GETDATAORA(DATA_ORA) AS DATA_ORA, " _
                                & "OPERATORI.OPERATORE, DESCRIZIONE, DATA_ORA AS DATA_ORA_ORDER " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_LOG, OPERATORI " _
                                & "WHERE OPERATORI.ID = ARPA_LOG.ID_OPERATORE"
            RadGridLog.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Log - RadGridGestione_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
