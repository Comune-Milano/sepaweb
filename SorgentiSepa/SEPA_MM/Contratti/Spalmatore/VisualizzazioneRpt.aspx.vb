
Imports Telerik.Web.UI

Partial Class Contratti_Spalmatore_VisualizzazioneRpt
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Private Sub Contratti_Spalmatore_VisualizzazioneRpt_Load(sender As Object, e As EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            HFGriglia.Value = RadGridReport.ClientID.ToString.Replace("ctl00", "MasterPage")
        End If
    End Sub

    Private Sub RadGridReport_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGridReport.NeedDataSource
        Try
            Dim Query As String = " SELECT  REPORT.id," _
                            & " OPERATORI.OPERATORE,SISCOM_MI.GETDATAORA(INIZIO) AS INIZIO, TIPOLOGIA_REPORT.DESCRIZIONE AS TIPO_ESTRAZIONE, " _
                            & " SISCOM_MI.GETDATAORA(FINE) AS FINE, " _
                            & " ESITO_REPORT.DESCRIZIONE AS ESITO, ERRORE,'<a href=''javascript:void(0);'' onclick=""window.open(''../../ALLEGATI/REPORT/'||NOMEFILE||''',''_blank'','''');"">'||NOMEFILE||'<a>' AS NOMEFILE " _
                            & " FROM SISCOM_MI.REPORT,SEPA.OPERATORI,SISCOM_MI.ESITO_REPORT,SISCOM_MI.TIPOLOGIA_REPORT " _
                            & " WHERE REPORT.ID_OPERATORE=OPERATORI.ID AND TIPOLOGIA_REPORT.ID=ID_TIPOLOGIA_REPORT" _
                            & " and ESITO_REPORT.ID=ESITO and tipologia=3 order by id desc"
            RadGridReport.DataSource = par.getDataTableGrid(Query)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", True)
        End Try
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click
        Response.Redirect("SpalmatoreHome.aspx", False)
    End Sub

    Private Sub RadGridReport_DeleteCommand(sender As Object, e As GridCommandEventArgs) Handles RadGridReport.DeleteCommand
        Try
            connData.apri(True)

            par.cmd.CommandText = "delete from siscom_mi.report where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Operazione effettuata.", 300, 150, "Info", Nothing, Nothing)
            RadGridReport.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
End Class
