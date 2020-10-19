Imports Telerik.Web.UI
Imports System.Data

Partial Class SPESE_REVERSIBILI_AnomalieConguagli
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Anomalie"
            HFGriglia.Value = RadGrid1.ClientID.ToString.Replace("ctl00", "MasterPage")
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare almeno una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
    End Sub
    Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
        Dim stringa As String = " SELECT TIPOLOGIA_CARATURE.DESCRIZIONE AS TIPOLOGIA " _
            & " ,CRITERI_RIPARTIZIONE.DESCRIZIONE AS RIPARTIZIONE " _
            & " ,TIPOLOGIA_DIVISIONE.DESCRIZIONE AS DIVISIONE " _
            & " ,(CASE   " _
            & " WHEN PF_CONS_RIPARTIZIONI.ID_COMPLESSO IS NOT NULL THEN COMPLESSI_IMMOBILIARI.DENOMINAZIONE  " _
            & " WHEN PF_CONS_RIPARTIZIONI.ID_LOTTO IS NOT NULL THEN LOTTI.DESCRIZIONE  " _
            & " WHEN PF_CONS_RIPARTIZIONI.ID_IMPIANTO IS NOT NULL THEN IMPIANTI.DESCRIZIONE  " _
            & " WHEN PF_CONS_RIPARTIZIONI.ID_SCALA IS NOT NULL THEN SCALE_EDIFICI.DESCRIZIONE  " _
            & " WHEN PF_CONS_RIPARTIZIONI.ID_EDIFICIO IS NOT NULL THEN EDIFICI.DENOMINAZIONE  " _
            & " WHEN PF_CONS_RIPARTIZIONI.ID_AGGREGAZIONE IS NOT NULL THEN AGGREGAZIONE_POD.DENOMINAZIONE  " _
            & " ELSE NULL END) AS OGGETTO  " _
            & " FROM SISCOM_MI.ANOMALIE_CONSUNTIVO,SISCOM_MI.TIPOLOGIA_CARATURE,SISCOM_MI.PF_CONS_RIPARTIZIONI,SISCOM_MI.CRITERI_RIPARTIZIONE,SISCOM_MI.TIPOLOGIA_DIVISIONE  " _
            & " , SISCOM_MI.LOTTI,SISCOM_MI.IMPIANTI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.AGGREGAZIONE_POD " _
            & " WHERE ANOMALIE_CONSUNTIVO.TIPOLOGIA=TIPOLOGIA_CARATURE.ID AND PF_CONS_RIPARTIZIONI.ID=ID_DETTAGLIO " _
            & " AND CRITERI_RIPARTIZIONE.ID=ID_CRITERIO_RIPARTIZIONE AND TIPOLOGIA_DIVISIONE.ID=ID_TIPOLOGIA_DIVISIONE " _
            & " AND LOTTI.ID(+)=PF_CONS_RIPARTIZIONI.ID_LOTTO  " _
            & " AND IMPIANTI.ID(+)=PF_CONS_RIPARTIZIONI.ID_IMPIANTO  " _
            & " AND SCALE_EDIFICI.ID(+)=PF_CONS_RIPARTIZIONI.ID_SCALA  " _
            & " AND EDIFICI.ID(+)=PF_CONS_RIPARTIZIONI.ID_EDIFICIO  " _
            & " AND AGGREGAZIONE_POD.ID(+)=PF_CONS_RIPARTIZIONI.ID_AGGREGAZIONE  " _
            & " AND COMPLESSI_IMMOBILIARI.ID(+) = PF_CONS_RIPARTIZIONI.ID_COMPLESSO AND ANOMALIE_CONSUNTIVO.ID_PF=" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
        Dim dt As Data.DataTable = par.getDataTableGrid(stringa)
        TryCast(sender, RadGrid).DataSource = dt
    End Sub

    Private Sub SPESE_REVERSIBILI_AnomalieConguagli_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub

    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        Try
            RadGrid1.AllowPaging = False
            RadGrid1.Rebind()
            Dim dtRecords As New DataTable()
            For Each col As GridColumn In RadGrid1.Columns
                Dim colString As New DataColumn(col.UniqueName)
                If col.Visible = True Then
                    If Not col.ColumnType = "GridTemplateColumn" Then
                        If Not col.UniqueName = "DeleteColumn" Then
                            dtRecords.Columns.Add(colString)
                        End If
                    End If
                End If
            Next
            For Each row As GridDataItem In RadGrid1.Items
                ' loops through each rows in RadGrid
                Dim dr As DataRow = dtRecords.NewRow()
                For Each col As GridColumn In RadGrid1.Columns
                    'loops through each column in RadGrid
                    If col.Visible = True Then
                        If Not col.ColumnType = "GridTemplateColumn" Then
                            If Not col.UniqueName = "DeleteColumn" Then
                                dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                            End If
                        End If
                    End If
                Next
                dtRecords.Rows.Add(dr)
            Next
            Dim i As Integer = 0
            For Each col As GridColumn In RadGrid1.Columns
                If col.Visible = True Then
                    If Not col.ColumnType = "GridTemplateColumn" Then
                        If Not col.UniqueName = "DeleteColumn" Then
                            Dim colString As String = col.HeaderText
                            dtRecords.Columns(i).ColumnName = colString
                            i += 1
                        End If
                    End If
                End If
            Next
            Esporta(dtRecords)

            RadGrid1.AllowPaging = True
            RadGrid1.Rebind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        RadGrid1.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportaAnomalie", "ExportaAnomalie", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub
End Class