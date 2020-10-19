Imports Telerik.Web.UI
Imports System.Data

Partial Class SPESE_REVERSIBILI_RisultatiRicercaConsuntivi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                'caricaRisultati()
            End If
            HFGriglia.Value = DataGridUI.ClientID.ToString.Replace("ctl00", "MasterPage")
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Ricerca"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
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
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
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
    Protected Sub ButtonNuovaRicerca_Click(sender As Object, e As System.EventArgs) Handles ButtonNuovaRicerca.Click
        Response.Redirect("RicercaConsuntivi.aspx", True)
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        Try
            DataGridUI.AllowPaging = False
            DataGridUI.Rebind()
            Dim dtRecords As New DataTable()
            For Each col As GridColumn In DataGridUI.Columns
                Dim colString As New DataColumn(col.UniqueName)
                If col.Visible = True Then
                    If Not col.ColumnType = "GridTemplateColumn" Then
                        If Not col.UniqueName = "DeleteColumn" Then
                            dtRecords.Columns.Add(colString)
                        End If
                    End If
                End If
            Next
            For Each row As GridDataItem In DataGridUI.Items
                ' loops through each rows in RadGrid
                Dim dr As DataRow = dtRecords.NewRow()
                For Each col As GridColumn In DataGridUI.Columns
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
            For Each col As GridColumn In DataGridUI.Columns
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
            If Request.QueryString("TIPO") = "ND" Then
                dtRecords.Columns.Remove("EDIFICIO")
                dtRecords.Columns.Remove("SUPERFICIE_NETTA")
                dtRecords.Columns.Remove("SUPERFICIE_CATASTALE")
                dtRecords.Columns.Remove("SCALA")
                dtRecords.Columns.Remove("INTERNO")
                dtRecords.Columns.Remove("PIANO")
                dtRecords.Columns.Remove("MILLESIMI_SERVIZI_COMPLESSO_P")
                dtRecords.Columns.Remove("MILLESIMI_SERVIZI_EDIFICIO_P")
                dtRecords.Columns.Remove("MILLESIMI_RISCALDAMENTO_P")
                dtRecords.Columns.Remove("MILLESIMI_SCALA_ASCENSORE_P")
                dtRecords.Columns.Remove("ACQUA")
                dtRecords.Columns.Remove("ALTRO")
                dtRecords.Columns.Remove("CONDUZIONE")
                dtRecords.Columns.Remove("CUSTODI")
                dtRecords.Columns.Remove("CUSTODI_AUTOGESTIONE")
                dtRecords.Columns.Remove("FOGNATURA")
                dtRecords.Columns.Remove("PARTI_COMUNI")
                dtRecords.Columns.Remove("PULIZIA")
                dtRecords.Columns.Remove("PULIZIA_AUTOGESTIONE")
                dtRecords.Columns.Remove("PULIZIA_PARTI_COMUNI")
                dtRecords.Columns.Remove("UTENZE_ELETTRICHE")
                dtRecords.Columns.Remove("UTENZE_IDRICHE")
                dtRecords.Columns.Remove("VARIE")
                dtRecords.Columns.Remove("VARIE_AUTOGESIONE")
                dtRecords.Columns.Remove("VERDE")
                dtRecords.Columns.Remove("VERDE_AUTOGESTIONE")
                dtRecords.Columns.Remove("RISCALDAMENTO_ACQUA")
                dtRecords.Columns.Remove("RISCALDAMENTO_APPALTO")
                dtRecords.Columns.Remove("RISCALDAMENTO_AUTOGESTIONE")
                dtRecords.Columns.Remove("RISCALDAMENTO_FORZA_MOTRICE")
                dtRecords.Columns.Remove("RISCALDAMENTO_GAS_AUTOGESTIONE")
                dtRecords.Columns.Remove("RISCALDAMENTO_GAS_METANO")
                dtRecords.Columns.Remove("RISCALDAMENTO_GESTIONE_CALORE")
                dtRecords.Columns.Remove("RISCALDAMENTO_TR_10")
                dtRecords.Columns.Remove("RISCALDAMENTO_TR_21")
                dtRecords.Columns.Remove("ASCENSORI_FORZA_MOTRICE")
                dtRecords.Columns.Remove("ASCENSORI_MANUTENZIONE")
                dtRecords.AcceptChanges()
            End If
            DataGridUI.AllowPaging = True
            DataGridUI.Rebind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGridUI.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportConsuntivi", "ExportConsuntivi", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Protected Sub DataGridUI_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridUI.NeedDataSource
        Try
            Dim Complesso As String = Request.QueryString("COMPLESSO")
            Dim Edificio As String = Request.QueryString("EDIFICIO")
            Dim Indirizzo As String = Request.QueryString("INDIRIZZO")
            Dim Civico As String = Request.QueryString("CIVICO")
            Dim Interno As String = Request.QueryString("INTERNO")
            Dim Scala As String = Request.QueryString("SCALA")
            Dim Ascensore As String = Request.QueryString("ASCENSORE")
            Dim Tipologia As String = "'" & Replace(Request.QueryString("TIPOLOGIA"), ",", "','") & "'"
            Dim intestatario As String = Request.QueryString("INTESTATARIO")
            Dim codUI As String = Request.QueryString("CODUI")
            Dim IdElaborazione As String = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            Dim inizio As String = ""
            Dim fine As String = ""
            connData.apri()
            par.cmd.CommandText = "SELECT DATA_RIFERIMENTO_INIZIO_C,DATA_RIFERIMENTO_FINE_C FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & IdElaborazione
            Dim lettoreD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreD.Read Then
                inizio = par.IfNull(lettoreD(0), "")
                fine = par.IfNull(lettoreD(1), "")
            End If
            lettoreD.Close()

            Dim condizione As String = ""
            If Complesso <> "-1" Then
                condizione &= " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO=" & Complesso & "))"
            End If
            If Edificio <> "-1" Then
                condizione &= " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO=" & Edificio & ")"
            End If
            If Interno <> "-1" Then
                condizione &= " AND INTERNO ='" & par.formatoStringaDB(Interno) & "' "
            End If
            If Scala <> "-1" Then
                condizione &= " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_SCALA = " & Scala & ")"
            End If
            If Ascensore <> "-1" Then
                If Ascensore = 0 Then
                    condizione = condizione & " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE (EDIFICI.NUM_ASCENSORI = 0 OR EDIFICI.NUM_ASCENSORI IS NULL))) "
                ElseIf Ascensore = 1 Then
                    condizione = condizione & " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE EDIFICI.NUM_ASCENSORI > 0 )"
                End If
            End If
            If Indirizzo <> "-1" Then
                condizione = condizione & " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN (SELECT ID FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (SELECT INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.DESCRIZIONE = '" & Indirizzo & "' "
                If Civico <> "-1" Then
                    condizione = condizione & "AND INDIRIZZI.CIVICO = '" & Civico & "'"
                End If
                condizione = condizione & ")))"
            End If
            If Indirizzo <> "-1" Then
                condizione = condizione & "AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO IN (SELECT INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.DESCRIZIONE = '" & Indirizzo & "' "
                If Civico <> "-1" Then
                    condizione = condizione & "AND INDIRIZZI.CIVICO = '" & Civico & "'"
                End If
                condizione = condizione & "))"
            End If
            If Tipologia <> "''" Then
                condizione &= " AND ID_UNITA IN (SELECT ID FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.COD_TIPOLOGIA IN (" & Tipologia & "))"
            End If
            If Len(codUI) = 17 Then
                condizione &= " AND COD_UNITA_IMMOBILIARE like '" & UCase(codUI.Replace("'", "''").Replace("*", "%")) & "'"
            End If
            If Not String.IsNullOrEmpty(intestatario) Then
                condizione &= " AND INTESTATARIO like '" & UCase(intestatario.Replace("'", "''").Replace("*", "%")) & "'"
            End If

            par.cmd.CommandText = " SELECT ID_UNITA,ID_CONTRATTO, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_DISPONIBILITA WHERE COD=(SELECT COD_TIPO_DISPONIBILITA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID=ID_UNITA)) AS TIPOLOGIA_UNITA," _
                & " COD_CONTRATTO, " _
                & " INTESTATARIO, " _
                & " STATO, " _
                & " NUMERO_GIORNI, " _
                & " COD_UNITA_IMMOBILIARE, " _
                & " EDIFICIO AS EDIFICIO, " _
                & " SUPERFICIE_NETTA, " _
                & " SUPERFICIE_CATASTALE, " _
                & " TIPOLOGIA, " _
                & " SCALA, " _
                & " INTERNO, " _
                & " PIANO, " _
                & " MILLESIMI_SERVIZI_COMPLESSO, " _
                & " MILLESIMI_SERVIZI_COMPLESSO_P, " _
                & " MILLESIMI_SERVIZI_EDIFICIO, " _
                & " MILLESIMI_SERVIZI_EDIFICIO_P, " _
                & " MILLESIMI_RISCALDAMENTO, " _
                & " MILLESIMI_RISCALDAMENTO_P, " _
                & " MILLESIMI_SCALA_ASCENSORE, " _
                & " MILLESIMI_SCALA_ASCENSORE_P, " _
                & " SERVIZI, " _
                & " SERV_COMP_ACQUA+SERV_EDI_ACQUA AS ACQUA,  " _
                & " SERV_COMP_ALTRO+SERV_EDI_ALTRO AS ALTRO,  " _
                & " SERV_COMP_CONDUZIONE+SERV_EDI_CONDUZIONE AS CONDUZIONE,  " _
                & " SERV_COMP_CUSTODI+SERV_EDI_CUSTODI AS CUSTODI,  " _
                & " SERV_COMP_CUSTODI_AUTO+SERV_EDI_CUSTODI_AUTO AS CUSTODI_AUTOGESTIONE,  " _
                & " SERV_COMP_FOGNATURA+SERV_EDI_FOGNATURA AS FOGNATURA, " _
                & " SERV_COMP_PARTI_COMUNI+SERV_EDI_PARTI_COMUNI AS PARTI_COMUNI,  " _
                & " SERV_COMP_PULIZIA+SERV_EDI_PULIZIA AS PULIZIA,  " _
                & " SERV_COMP_PULIZIA_AUTO+SERV_EDI_PULIZIA_AUTO AS PULIZIA_AUTOGESTIONE,  " _
                & " SERV_COMP_PULIZIA_PC+SERV_EDI_PULIZIA_PC AS PULIZIA_PARTI_COMUNI,  " _
                & " SERV_COMP_UTENZE_ELETTR+SERV_EDI_UTENZE_ELETTR AS UTENZE_ELETTRICHE,  " _
                & " SERV_COMP_UTENZE_IDRICHE+SERV_EDI_UTENZE_IDRICHE AS UTENZE_IDRICHE,  " _
                & " SERV_COMP_VARIE+SERV_EDI_VARIE AS VARIE,  " _
                & " SERV_COMP_VARIE_AUTO+SERV_EDI_VARIE_AUTO AS VARIE_AUTOGESIONE,  " _
                & " SERV_COMP_VERDE+SERV_EDI_VERDE AS VERDE,  " _
                & " SERV_COMP_VERDE_AUTO+SERV_EDI_VERDE_AUTO AS VERDE_AUTOGESTIONE,  " _
                & " RISCALDAMENTO, " _
                & " RISCALDAMENTO_ACQUA, " _
                & " RISCALDAMENTO_APPALTO, " _
                & " RISCALDAMENTO_AUTOGESTIONE, " _
                & " RISCALDAMENTO_FORZA_MOTRICE, " _
                & " RISCALDAMENTO_GAS_AUTOGESTIONE, " _
                & " RISCALDAMENTO_GAS_METANO, " _
                & " RISCALDAMENTO_GESTIONE_CALORE, " _
                & " RISCALDAMENTO_TR_10, " _
                & " RISCALDAMENTO_TR_21, " _
                & " SCALA_ASCENSORE, " _
                & " ASCENSORI_FORZA_MOTRICE, " _
                & " ASCENSORI_MANUTENZIONE," _
                & " MONTASCALE, " _
                & " TOTALE_ONERI, " _
                & " TOTALE_BOLLETTATO, " _
                & " TOTALE_CONGUAGLIO " _
                & " FROM SISCOM_MI.EXPORT_CONGUAGLI WHERE ID_PF=" & IdElaborazione _
                & condizione _
                & " ORDER BY COD_UNITA_IMMOBILIARE"

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            'Dim dtExp As Data.DataTable = dt
            If Request.QueryString("TIPO") = "ND" Then
                DataGridUI.MasterTableView.GetColumn("EDIFICIO").Display = False
                DataGridUI.MasterTableView.GetColumn("SUPERFICIE_NETTA").Display = False
                DataGridUI.MasterTableView.GetColumn("SUPERFICIE_CATASTALE").Display = False
                DataGridUI.MasterTableView.GetColumn("SCALA").Display = False
                DataGridUI.MasterTableView.GetColumn("INTERNO").Display = False
                DataGridUI.MasterTableView.GetColumn("PIANO").Display = False
                DataGridUI.MasterTableView.GetColumn("MILLESIMI_SERVIZI_COMPLESSO_P").Display = False
                DataGridUI.MasterTableView.GetColumn("MILLESIMI_SERVIZI_EDIFICIO_P").Display = False
                DataGridUI.MasterTableView.GetColumn("MILLESIMI_RISCALDAMENTO_P").Display = False
                DataGridUI.MasterTableView.GetColumn("MILLESIMI_SCALA_ASCENSORE_P").Display = False
                DataGridUI.MasterTableView.GetColumn("ACQUA").Display = False
                DataGridUI.MasterTableView.GetColumn("ALTRO").Display = False
                DataGridUI.MasterTableView.GetColumn("CONDUZIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("CUSTODI").Display = False
                DataGridUI.MasterTableView.GetColumn("CUSTODI_AUTOGESTIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("FOGNATURA").Display = False
                DataGridUI.MasterTableView.GetColumn("PARTI_COMUNI").Display = False
                DataGridUI.MasterTableView.GetColumn("PULIZIA").Display = False
                DataGridUI.MasterTableView.GetColumn("PULIZIA_AUTOGESTIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("PULIZIA_PARTI_COMUNI").Display = False
                DataGridUI.MasterTableView.GetColumn("UTENZE_ELETTRICHE").Display = False
                DataGridUI.MasterTableView.GetColumn("UTENZE_IDRICHE").Display = False
                DataGridUI.MasterTableView.GetColumn("VARIE").Display = False
                DataGridUI.MasterTableView.GetColumn("VARIE_AUTOGESIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("VERDE").Display = False
                DataGridUI.MasterTableView.GetColumn("VERDE_AUTOGESTIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_ACQUA").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_APPALTO").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_AUTOGESTIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_FORZA_MOTRICE").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_GAS_AUTOGESTIONE").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_GAS_METANO").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_GESTIONE_CALORE").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_TR_10").Display = False
                DataGridUI.MasterTableView.GetColumn("RISCALDAMENTO_TR_21").Display = False
                DataGridUI.MasterTableView.GetColumn("ASCENSORI_FORZA_MOTRICE").Display = False
                DataGridUI.MasterTableView.GetColumn("ASCENSORI_MANUTENZIONE").Display = False

                'dtExp.Columns.Remove("EDIFICIO")
                'dtExp.Columns.Remove("SUPERFICIE_NETTA")
                'dtExp.Columns.Remove("SUPERFICIE_CATASTALE")
                'dtExp.Columns.Remove("SCALA")
                'dtExp.Columns.Remove("INTERNO")
                'dtExp.Columns.Remove("PIANO")
                'dtExp.Columns.Remove("MILLESIMI_SERVIZI_COMPLESSO_P")
                'dtExp.Columns.Remove("MILLESIMI_SERVIZI_EDIFICIO_P")
                'dtExp.Columns.Remove("MILLESIMI_RISCALDAMENTO_P")
                'dtExp.Columns.Remove("MILLESIMI_SCALA_ASCENSORE_P")
                'dtExp.Columns.Remove("ACQUA")
                'dtExp.Columns.Remove("ALTRO")
                'dtExp.Columns.Remove("CONDUZIONE")
                'dtExp.Columns.Remove("CUSTODI")
                'dtExp.Columns.Remove("CUSTODI_AUTOGESTIONE")
                'dtExp.Columns.Remove("FOGNATURA")
                'dtExp.Columns.Remove("PARTI_COMUNI")
                'dtExp.Columns.Remove("PULIZIA")
                'dtExp.Columns.Remove("PULIZIA_AUTOGESTIONE")
                'dtExp.Columns.Remove("PULIZIA_PARTI_COMUNI")
                'dtExp.Columns.Remove("UTENZE_ELETTRICHE")
                'dtExp.Columns.Remove("UTENZE_IDRICHE")
                'dtExp.Columns.Remove("VARIE")
                'dtExp.Columns.Remove("VARIE_AUTOGESIONE")
                'dtExp.Columns.Remove("VERDE")
                'dtExp.Columns.Remove("VERDE_AUTOGESTIONE")
                'dtExp.Columns.Remove("RISCALDAMENTO_ACQUA")
                'dtExp.Columns.Remove("RISCALDAMENTO_APPALTO")
                'dtExp.Columns.Remove("RISCALDAMENTO_AUTOGESTIONE")
                'dtExp.Columns.Remove("RISCALDAMENTO_FORZA_MOTRICE")
                'dtExp.Columns.Remove("RISCALDAMENTO_GAS_AUTOGESTIONE")
                'dtExp.Columns.Remove("RISCALDAMENTO_GAS_METANO")
                'dtExp.Columns.Remove("RISCALDAMENTO_GESTIONE_CALORE")
                'dtExp.Columns.Remove("RISCALDAMENTO_TR_10")
                'dtExp.Columns.Remove("RISCALDAMENTO_TR_21")
                'dtExp.Columns.Remove("ASCENSORI_FORZA_MOTRICE")
                'dtExp.Columns.Remove("ASCENSORI_MANUTENZIONE")

            End If

            'ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " DataGrid1_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Sub SPESE_REVERSIBILI_RisultatiRicercaConsuntivi_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
    End Sub
End Class
