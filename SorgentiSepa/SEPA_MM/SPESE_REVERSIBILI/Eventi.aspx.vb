
Imports System.Data
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_Eventi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Eventi"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            If Not IsPostBack Then
                'CaricaGridEventi()
                'HFElencoGriglie.Value = dgvEdifici.ClientID.ToString.Replace("ctl00", "MasterPage") & ","

            End If
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

    Private Sub dgvEventi_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvEventi.NeedDataSource
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT (SELECT OPERATORE FROM SEPA.OPERATORI WHERE OPERATORI.ID = ID_OPERATORE) AS OPERATORE, " _
                                & " GETDATAORA(DATA_ORA) AS DATA_ORA, CAMPO, " _
                                & " (case when VAL_PRECEDENTE = 'FALSE' THEN 'NO' " _
                                & " WHEN VAL_PRECEDENTE = 'TRUE' THEN 'SÌ' ELSE VAL_PRECEDENTE END) AS VAL_PRECEDENTE, " _
                                & " (case when VAL_IMPOSTATO = 'FALSE' THEN 'NO' " _
                                & " WHEN VAL_IMPOSTATO = 'TRUE' THEN 'SÌ' ELSE VAL_IMPOSTATO END) AS VAL_IMPOSTATO, " _
                                & " (SELECT DESCRIZIONE FROM SISCOM_MI.SPESE_REVER_LOG_OP WHERE ID=ID_OPERAZIONE) AS OPERAZIONE  " _
                                & " FROM SISCOM_MI.SPESE_REVER_LOG WHERE ID_PF =  " & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") _
                                & " ORDER BY DATA_ORA DESC"
            Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvEdifici_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Protected Sub EsportaAppalti_Click(sender As Object, e As System.EventArgs)
        dgvEventi.AllowPaging = False
        dgvEventi.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In dgvEventi.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                dtRecords.Columns.Add(colString)
            End If
        Next
        For Each row As GridDataItem In dgvEventi.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In dgvEventi.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In dgvEventi.Columns
            If col.Visible = True Then
                Dim colString As String = col.HeaderText
                dtRecords.Columns(i).ColumnName = colString
                i += 1
            End If
        Next
        Esporta(dtRecords)
        dgvEventi.AllowPaging = True
        dgvEventi.Rebind()
    End Sub
    Protected Sub RefreshEventi_Click(sender As Object, e As System.EventArgs)
        dgvEventi.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "Eventi", "Eventi", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            Response.Write("<script>alert('');</script>")
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Private Sub btnEsci_Click(sender As Object, e As EventArgs) Handles btnEsci.Click

    End Sub
End Class
