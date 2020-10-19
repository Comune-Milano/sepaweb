
Imports Telerik.Web.UI

Partial Class Gestione_locatari_ElencoSegnaposto
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../AccessoNegato.htm", True)
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)
    End Sub

    Private Sub dgvMarcatori_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvMarcatori.NeedDataSource
        Try
            par.cmd.CommandText = "select cod,descrizione FROM GEST_LOCATARI_MARCATORI WHERE QUERY IS NOT NULL AND ID>0 ORDER BY cod ASC"
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)

            TryCast(sender, RadGrid).DataSource = dt

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            par.visualizzaErrore(ex, Me.Page)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
