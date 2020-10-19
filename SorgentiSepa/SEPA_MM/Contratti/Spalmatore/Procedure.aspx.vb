
Partial Class Contratti_Spalmatore_Procedure
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = dgvProcedure.ClientID.ToString.Replace("ctl00", "MasterPage")
        End If
    End Sub
    Protected Sub dgvProcedure_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvProcedure.NeedDataSource
        Try
            Dim Query As String = " SELECT ELABORAZIONE_SCRITTURE_GEST.ID, nome_operatore as operatore,GETDATAORA(DATA_ORA_INIZIO) AS DATA_ORA_INIZIO, DATA_ORA_INIZIO AS VALORE_ORDER_I, " _
                                & " GETDATAORA(DATA_ORA_FINE) AS DATA_ORA_FINE, DATA_ORA_FINE AS VALORE_ORDER_F, " _
                                & " TIPO_ELABORAZIONE_GESTIONALI.DESCRIZIONE AS TIPO, ESITO_ELABORAZIONE_GESTIONALI.DESCRIZIONE AS ESITO, PARZIALE || '%' AS PERCENTUALE " _
                                & " FROM siscom_mi.ELABORAZIONE_SCRITTURE_GEST, siscom_mi.ESITO_ELABORAZIONE_GESTIONALI," _
                                & " siscom_mi.TIPO_ELABORAZIONE_GESTIONALI " _
                                & " WHERE /*OPERATORI.ID = ELABORAZIONE_SCRITTURE_GEST.ID_OPERATORE " _
                                & " AND*/ ESITO_ELABORAZIONE_GESTIONALI.ID(+) = ELABORAZIONE_SCRITTURE_GEST.ESITO " _
                                & " AND TIPO_ELABORAZIONE_GESTIONALI.ID(+) = ELABORAZIONE_SCRITTURE_GEST.TIPO order by 1 desc"
            dgvProcedure.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub
End Class
