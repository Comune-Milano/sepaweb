Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing
Partial Class Contratti_VisualizzaEstrazioni_RU
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Public Altezza As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Protected Sub RadGridReport_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridReport.NeedDataSource
        Try
            'REPORT MOROSITA' CONTABILITA' - 258/2017
            Dim cond As String = " AND TIPOLOGIA = 1 "
            If Not IsNothing(Request.QueryString("TIPO")) Then
                If Not String.IsNullOrEmpty(Request.QueryString("TIPO").ToString) Then
                    If Request.QueryString("TIPO").ToString = "RPT_MOR" Then
                        cond = " AND TIPOLOGIA = 2 "
                    End If
                    If Request.QueryString("TIPO").ToString = "RPT_EM" Then
                        cond = " AND TIPOLOGIA = 2 "
                    End If
                End If
            End If
            
            PAR.cmd.CommandText = " SELECT  REPORT.id," _
                            & " OPERATORI.OPERATORE,SISCOM_MI.GETDATAORA(INIZIO) AS INIZIO, TIPOLOGIA_REPORT.DESCRIZIONE AS TIPO_ESTRAZIONE, " _
                            & " SISCOM_MI.GETDATAORA(FINE) AS FINE, " _
                            & " ESITO_REPORT.DESCRIZIONE AS ESITO, ERRORE,'<a href=''javascript:void(0);'' onclick=""window.open(''../ALLEGATI/REPORT/'||NOMEFILE||''',''_blank'','''');"">'||NOMEFILE||'<a>' AS NOMEFILE " _
                            & " FROM SISCOM_MI.REPORT,SEPA.OPERATORI,SISCOM_MI.ESITO_REPORT,SISCOM_MI.TIPOLOGIA_REPORT " _
                            & " WHERE REPORT.ID_OPERATORE=OPERATORI.ID AND TIPOLOGIA_REPORT.ID=ID_TIPOLOGIA_REPORT" _
                            & " and ESITO_REPORT.ID=ESITO " & cond & " order by id desc"
            TryCast(sender, RadGrid).DataSource = PAR.getDataTableGrid(PAR.cmd.CommandText)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridReport_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

End Class
