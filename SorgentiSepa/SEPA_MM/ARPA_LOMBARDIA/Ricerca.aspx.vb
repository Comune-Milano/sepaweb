Imports Telerik.Web.UI
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.IO

Partial Class ARPA_LOMBARDIA_Ricerca
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim lock As New SepacomLock

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            HFGriglia.Value = RadGridElaborazioni.ClientID.ToString.Replace("ctl00", "MasterHomePage")
        End If
    End Sub
    Protected Sub RadGridElaborazioni_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridElaborazioni.NeedDataSource
        Try
            Dim Query As String = "SELECT ARPA_ELABORAZIONI.ID, ANNO, OPERATORI.OPERATORE, GETDATAORA(DATA_ORA) AS DATA_ORA, CF_ENTE_PROPRIETARIO, DATA_ORA AS DATA_ORA_ORDER " _
                                & "FROM " & CType(Me.Master, Object).StringaSiscom & "ARPA_ELABORAZIONI, OPERATORI " _
                                & "WHERE OPERATORI.ID(+) = ARPA_ELABORAZIONI.ID_OPERATORE "
            RadGridElaborazioni.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: SSR_VENETO_Ricerca - RadGridElaborazioni_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub
End Class
