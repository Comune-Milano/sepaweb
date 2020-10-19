Imports System.IO
Imports Telerik.Web.UI
Imports System.Web.UI.WebControls
Imports xi = Telerik.Web.UI.ExportInfrastructure
Imports System.Web.UI
Imports System.Web
Imports Telerik.Web.UI.GridExcelBuilder
Imports System.Drawing
Partial Class VERIFICHE_Report
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Public Altezza As Integer = 0

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Timer1.Enabled = False
            Me.TextBox1.Focus()
            RadGridReport.Visible = False
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As System.EventArgs) Handles Button1.Click
        If InStr(TextBox1.Text, "'") = 0 Then
            If PAR.VerificaPW(TextBox1.Text) = True Then
                RadGridReport.Visible = True
                RadGridReport.Rebind()
                Timer1.Enabled = True
            End If
        End If
    End Sub

    Protected Sub dgvDocumenti_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridReport.NeedDataSource
        Try
            PAR.cmd.CommandText = " SELECT  REPORT.id,ROUND((PARZIALE/NVL(TOTALE,1))*100,1)||'%' AS AVANZAMENTO," _
                & " OPERATORI.OPERATORE,SISCOM_MI.GETDATAORA(INIZIO) AS INIZIO, " _
                & " SISCOM_MI.GETDATAORA(FINE) AS FINE,Q1 AS QUERY, " _
                & " ESITO_REPORT.DESCRIZIONE AS ESITO, ERRORE,'<a href=''javascript:void(0);'' onclick=""window.open(''../ALLEGATI/REPORT/'||NOMEFILE||''',''_blank'','''');"">'||NOMEFILE||'<a>' AS NOMEFILE " _
                & " FROM SISCOM_MI.REPORT,SEPA.OPERATORI,SISCOM_MI.ESITO_REPORT " _
                & " WHERE REPORT.ID_OPERATORE=OPERATORI.ID AND ID_TIPOLOGIA_REPORT=0 " _
                & " AND ESITO_REPORT.ID=ESITO order by report.id desc "
            TryCast(sender, RadGrid).DataSource = PAR.getDataTableGrid(PAR.cmd.CommandText)
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " RadGridReport_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As System.EventArgs) Handles Timer1.Tick
        RadGridReport.Rebind()
    End Sub
End Class
