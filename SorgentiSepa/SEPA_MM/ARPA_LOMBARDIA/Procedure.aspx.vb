
Partial Class ARPA_LOMBARDIA_Procedure
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = dgvProcedure.ClientID.ToString.Replace("ctl00", "MasterHomePage")
        End If
    End Sub
    Protected Sub dgvProcedure_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvProcedure.NeedDataSource
        Try
            Dim Query As String = "SELECT ARPA_PROCEDURE.ID, OPERATORI.OPERATORE, " & CType(Me.Master, Object).StringaSiscom & "GETDATAORA(DATA_ORA_INIZIO) AS DATA_ORA_INIZIO, DATA_ORA_INIZIO AS VALORE_ORDER_I, " _
                                & CType(Me.Master, Object).StringaSiscom & "GETDATAORA(DATA_ORA_FINE) AS DATA_ORA_FINE, DATA_ORA_FINE AS VALORE_ORDER_F, " _
                                & "ARPA_PROCEDURE_TIPO.DESCRIZIONE AS TIPO, ARPA_PROCEDURE_ESITO.DESCRIZIONE AS ESITO, PARZIALE || '%' AS PERCENTUALE " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_PROCEDURE, OPERATORI, " _
                                & CType(Me.Master, Object).StringaSiscom & "ARPA_PROCEDURE_ESITO, " & CType(Me.Master, Object).StringaSiscom & "ARPA_PROCEDURE_TIPO " _
                                & "WHERE OPERATORI.ID = ARPA_PROCEDURE.ID_OPERATORE " _
                                & "AND ARPA_PROCEDURE_ESITO.ID(+) = ARPA_PROCEDURE.ESITO " _
                                & "AND ARPA_PROCEDURE_TIPO.ID(+) = ARPA_PROCEDURE.ID_TIPO "
            dgvProcedure.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: ARPA_LOMBARDIA_Procedure - dgvProcedure_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
