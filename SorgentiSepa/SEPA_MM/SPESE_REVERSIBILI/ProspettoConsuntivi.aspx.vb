Imports System.Math
Imports Telerik.Web.UI
Imports System.Data

Partial Class SPESE_REVERSIBILI_ProspettoConsuntivi
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Prospetto"
        End If
        HFGriglia.Value = DataGridProspetto.ClientID.ToString.Replace("ctl00", "MasterPage")
        If Not IsPostBack Then
            caricaPiano()
        End If
    End Sub
    Private Function controlloProfilo() As Boolean
        'CONTROLLO DELLA SESSIONE OPERATORE E DELL'ABILITAZIONE ALLE SPESE REVERSIBILI
        If Session.Item("OPERATORE") <> "" Then
            If Session.Item("fl_spese_reversibili") = "0" Then
                Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - Operatore non abilitato alla gestione delle spese reversibili")
                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
                Return False
            End If
        Else
            RadWindowManager1.RadAlert("Accesso negato o sessione scaduta! E\' necessario rieseguire il login!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
            Return False
        End If
        If Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Then
            RadWindowManager1.RadAlert("E\' necessario selezionare una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
        ButtonAnomalia.Enabled = False
        ButtonElimina.Enabled = False
        ButtonEliminaVoci.Enabled = False


    End Sub

    Protected Sub ButtonEliminaVoci_Click(sender As Object, e As System.EventArgs) Handles ButtonEliminaVoci.Click
        If ConfermaEliminazioneProspetto.Value = 1 Then
            Try
                connData.apri()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.ANOMALIE_CONSUNTIVO WHERE ID_PF= " & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_CONSUNTIVI WHERE ID_PF= " & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = " DELETE FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID_PF=" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                par.cmd.ExecuteNonQuery()
                Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                          & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '- - -'," _
                          & "'- - -', '- - -' , 20)"
                par.cmd.ExecuteNonQuery()
                connData.chiudi()
                DataGridProspetto.Rebind()
                'caricaProspettoCompleto()

                RadWindowManager1.RadAlert("Le voci di prospetto sono state eliminate correttamente!", 300, 150, "Attenzione", "", "null")
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza: ButtonEliminaVoci_Click - " & ex.Message)
                RadWindowManager1.RadAlert("Si è verificato un errore durante l\'eliminazione del prospetto!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")

            End Try
        End If
    End Sub
    Protected Sub ButtonAnomalia_Click(sender As Object, e As System.EventArgs) Handles ButtonAnomalia.Click
        AvviaElaborazione()
    End Sub
    Protected Sub DataGridProspetto_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridProspetto.ItemDataBound
        Try
            If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
                Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
                e.Item.Attributes.Add("onclick", "document.getElementById('txtID').value='" & dataItem("ID").Text & "'")
            End If
        Catch ex As Exception
            Session.Add("ERRORE", Page.Title & " DataGridProspetto_NeedDataSource - " & ex.Message)
            Response.Redirect("../../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridProspetto_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DataGridProspetto.NeedDataSource
        Try
            'CARICO IL PROSPETTO COMPLETO DELLE SPESE DA RIPARTIRE PER QUESTA ELABORAZIONE
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            connData.apri()
            par.cmd.CommandText = " SELECT  " _
                & " DECODE(FL_MANUALE,0,PF_VOCI_IMPORTO.DESCRIZIONE,1,PF_CONS_RIPARTIZIONI.DESCRIZIONE) AS VOCE_SPESA, " _
                & " INITCAP(CRITERI_RIPARTIZIONE.DESCRIZIONE) AS CRITERIO_RIPARTIZIONE, " _
                & " DECODE(PF_CONS_RIPARTIZIONI.FL_MANUALE,1,'Sì',0,'No') AS FL_MANUALE, " _
                & " INITCAP(TIPOLOGIA_DIVISIONE.DESCRIZIONE) AS TIPOLOGIA_DIVISIONE, " _
                & " (CASE  " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_COMPLESSO IS NOT NULL THEN 'COMPLESSO' " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_LOTTO IS NOT NULL THEN 'LOTTO' " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_IMPIANTO IS NOT NULL THEN 'IMPIANTO' " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_SCALA IS NOT NULL THEN 'SCALA' " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_EDIFICIO IS NOT NULL THEN 'EDIFICIO' " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_AGGREGAZIONE IS NOT NULL THEN 'AGGREGAZIONE' " _
                & " ELSE NULL END) AS TIPO, " _
                & " (CASE  " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_COMPLESSO IS NOT NULL THEN COMPLESSI_IMMOBILIARI.DENOMINAZIONE " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_LOTTO IS NOT NULL THEN LOTTI.DESCRIZIONE " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_IMPIANTO IS NOT NULL THEN IMPIANTI.DESCRIZIONE " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_SCALA IS NOT NULL THEN SCALE_EDIFICI.DESCRIZIONE " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_EDIFICIO IS NOT NULL THEN EDIFICI.DENOMINAZIONE " _
                & " WHEN PF_CONS_RIPARTIZIONI.ID_AGGREGAZIONE IS NOT NULL THEN AGGREGAZIONE.DENOMINAZIONE " _
                & " ELSE NULL END) AS OGGETTO, " _
                & " COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS COMPLESSO, " _
                & " LOTTI.DESCRIZIONE AS LOTTO, " _
                & " IMPIANTI.DESCRIZIONE AS IMPIANTO, " _
                & " SCALE_EDIFICI.DESCRIZIONE AS SCALA, " _
                & " EDIFICI.DENOMINAZIONE AS EDIFICIO, " _
                & " AGGREGAZIONE.DENOMINAZIONE AS AGGREGAZIONE, " _
                & "  IMPORTO, " _
                & " PF_CONS_RIPARTIZIONI.ID AS ID,'' AS ELIMINA,INITCAP(TIPOLOGIA_CARATURE.DESCRIZIONE) AS TIPO_SPESA " _
                & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.CRITERI_RIPARTIZIONE,SISCOM_MI.TIPOLOGIA_DIVISIONE, " _
                & " SISCOM_MI.LOTTI,SISCOM_MI.IMPIANTI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI,SISCOM_MI.TIPOLOGIA_CARATURE, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.AGGREGAZIONE " _
                & " WHERE PF_VOCI_IMPORTO.ID(+)=PF_CONS_RIPARTIZIONI.ID_VOCE_SPESA " _
                & " AND CRITERI_RIPARTIZIONE.ID=PF_CONS_RIPARTIZIONI.ID_CRITERIO_RIPARTIZIONE " _
                & " AND TIPOLOGIA_DIVISIONE.ID=PF_CONS_RIPARTIZIONI.ID_TIPOLOGIA_DIVISIONE " _
                & " AND LOTTI.ID(+)=PF_CONS_RIPARTIZIONI.ID_LOTTO " _
                & " AND IMPIANTI.ID(+)=PF_CONS_RIPARTIZIONI.ID_IMPIANTO " _
                & " AND SCALE_EDIFICI.ID(+)=PF_CONS_RIPARTIZIONI.ID_SCALA " _
                & " AND EDIFICI.ID(+)=PF_CONS_RIPARTIZIONI.ID_EDIFICIO " _
                & " AND AGGREGAZIONE.ID(+)=PF_CONS_RIPARTIZIONI.ID_AGGREGAZIONE " _
                & " AND TIPOLOGIA_CARATURE.ID=ID_TIPO_SPESA " _
                & " AND COMPLESSI_IMMOBILIARI.ID(+) = PF_CONS_RIPARTIZIONI.ID_COMPLESSO " _
                & " AND ID_PF=" & idElaborazione _
                & " AND FL_NON_REVERSIBILE=0"

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            TryCast(sender, RadGrid).DataSource = dt
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " caricaProspettoCompleto - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento del prospetto!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub btnRefreshGrid_Click(sender As Object, e As System.EventArgs) Handles btnRefreshGrid.Click
        DataGridProspetto.Rebind()
    End Sub

    Protected Sub DataGridProspetto_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles DataGridProspetto.ItemCreated
        If TypeOf e.Item Is GridFilteringItem And DataGridProspetto.IsExporting Then
            e.Item.Visible = False
        End If
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        DataGridProspetto.AllowPaging = False
        DataGridProspetto.Rebind()
        Dim dtRecords As New DataTable()
        For Each col As GridColumn In DataGridProspetto.Columns
            Dim colString As New DataColumn(col.UniqueName)
            If col.Visible = True Then
                If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" Then
                    dtRecords.Columns.Add(colString)
                End If

            End If
        Next
        For Each row As GridDataItem In DataGridProspetto.Items
            ' loops through each rows in RadGrid
            Dim dr As DataRow = dtRecords.NewRow()
            For Each col As GridColumn In DataGridProspetto.Columns
                'loops through each column in RadGrid
                If col.Visible = True Then
                    If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" Then
                        dr(col.UniqueName) = row(col.UniqueName).Text.Replace("&nbsp;", "")
                    End If
                End If
            Next
            dtRecords.Rows.Add(dr)
        Next
        Dim i As Integer = 0
        For Each col As GridColumn In DataGridProspetto.Columns
            If col.Visible = True Then
                If col.ColumnType.ToUpper <> "GRIDBUTTONCOLUMN" Then
                    Dim colString As String = col.HeaderText
                    dtRecords.Columns(i).ColumnName = colString
                    i += 1
                End If
            End If
        Next
        Esporta(dtRecords)
        DataGridProspetto.AllowPaging = True
        DataGridProspetto.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        DataGridProspetto.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CONSUNTIVI", "CONSUNTIVI", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub

    Protected Sub DataGridProspetto_PreRender(sender As Object, e As System.EventArgs) Handles DataGridProspetto.PreRender
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            DataGridProspetto.MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            DataGridProspetto.MasterTableView.Columns.FindByUniqueName("Ricalcola").Visible = False
            DataGridProspetto.MasterTableView.Columns.FindByUniqueName("modificaServizio").Visible = False
            DataGridProspetto.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            DataGridProspetto.Rebind()
        End If
    End Sub

    Private Sub AvviaElaborazione()
        If HiddenFieldConfermaSimulazione.Value = "1" Then
            connData.apri()
            'ELABORAZIONE SELEZIONATA
            Dim idElaborazione As String = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID_STATO_APPLICAZIONE FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim lettoreElaborazioni As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim applicazioneBol As Boolean = False
            If lettoreElaborazioni.Read Then
                If par.IfNull(lettoreElaborazioni("ID_STATO_APPLICAZIONE"), 4) = 1 Or par.IfNull(lettoreElaborazioni("ID_STATO_APPLICAZIONE"), 4) = 2 Then
                    applicazioneBol = True
                End If
            End If
            lettoreElaborazioni.Close()
            If applicazioneBol = False Then
                'AVVIO DELL'ELABORAZIONE
                'AGGIORNAMENTO DELLE SPESE REVERSIBILI
                par.cmd.CommandText = "UPDATE SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI " _
                    & " SET " _
                    & " DATA_ORA_INIZIO='" & Format(Now, "yyyyMMddHHmmss") & "', " _
                    & " NOME_OPERATORE='" & Session.Item("OPERATORE") & "', " _
                    & " ESITO=0, " _
                    & " ERRORE=NULL," _
                    & " DESCRIZIONE_ERRORE=NULL," _
                    & " DATA_RIFERIMENTO_INIZIO_C='" & RadDatePickerDa.SelectedDate & "', " _
                    & " DATA_RIFERIMENTO_FINE_C='" & RadDatePickerA.SelectedDate & "', " _
                    & " TOTALE=100, " _
                    & " TIPO=2 " _
                    & " WHERE ID=" & idElaborazione
                par.cmd.ExecuteNonQuery()
                connData.chiudi()

                If IO.Directory.Exists(Server.MapPath("~\ALLEGATI\SPESE_REVERSIBILI\")) = False Then
                    IO.Directory.CreateDirectory(Server.MapPath("~\ALLEGATI\SPESE_REVERSIBILI\"))
                End If

                'RICHIAMO CALCOLO DEL CONSUNTIVO/CONGUAGLIO
                Try
                    Dim p As New System.Diagnostics.Process
                    Dim elParameter As String() = System.Configuration.ConfigurationManager.AppSettings("ConnectionString").ToString.Split(";")
                    Dim dicParaConnection As New Generic.Dictionary(Of String, String)
                    Dim sParametri As String = ""
                    For i As Integer = 0 To elParameter.Length - 1
                        Dim s As String() = elParameter(i).Split("=")
                        If s.Length > 1 Then
                            dicParaConnection.Add(s(0), s(1))
                        End If
                    Next
                    sParametri = dicParaConnection("Data Source") & "#" & dicParaConnection("User Id") & "#" & dicParaConnection("Password") & "#" & idElaborazione
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.FileName = System.Web.Hosting.HostingEnvironment.MapPath("~/SERVCODE/SPESE_REVERSIBILI.exe")
                    p.StartInfo.Arguments = sParametri
                    p.Start()
                    connData.apri()
                    Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID=" & txtID.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                          & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '- - -'," _
                          & "'- - -', '- - -' , 19)"
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                    RadWindowManager1.RadAlert("Simulazione avviata correttamente!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?r=1';}", "null")
                Catch ex As Exception
                    connData.apri()
                    'IN CASO DI ERRORE RICHIAMO
                    par.cmd.CommandText = "UPDATE SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI SET ESITO=2,ERRORE='CALCOLO NON AVVIATO' WHERE ID=" & idElaborazione
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                    RadWindowManager1.RadAlert("Errore durante l\'avvio del calcolo!", 300, 150, "Attenzione", "", "null")

                End Try
            Else
                'CONSUNTIVI GIà APPLICATI
                RadWindowManager1.RadAlert("I consuntivi/conguagli per questa elaborazione sono già stati applicati!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                connData.chiudi()
                Exit Sub
            End If
        End If
    End Sub

    Protected Sub btnInserisci_Click(sender As Object, e As System.EventArgs) Handles btnInserisci.Click
        txtID.Value = "0"
        DataGridProspetto.Rebind()
    End Sub

    Protected Sub btnModifica_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        txtID.Value = "0"
        DataGridProspetto.Rebind()
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Try
            connData.apri()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.ANOMALIE_CONSUNTIVO WHERE ID_DETTAGLIO= " & txtID.Value
            par.cmd.ExecuteNonQuery()
            'par.cmd.CommandText = "DELETE FROM PF_CONSUNTIVI WHERE ID_PF_CONS_RIPARTIZIONI = " & txtID.Value
            'par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "SELECT UPPER(DESCRIZIONE) || ' CON IMPORTO ' || TRIM(TO_CHAR(PF_CONS_RIPARTIZIONI.IMPORTO,'999G999G999G990D99')) AS T " _
                                & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID=" & txtID.Value
            Dim descrizione As String = par.cmd.ExecuteScalar
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID=" & txtID.Value
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                          & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '" & par.PulisciStrSql(descrizione) & " '," _
                          & "'- - -', '- - -' , 18)"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            txtID.Value = "0"
            DataGridProspetto.Rebind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub caricaPiano()
        Try
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            connData.apri()
            par.cmd.CommandText = "SELECT DATA_RIFERIMENTO_INIZIO_C,DATA_RIFERIMENTO_FINE_C FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                Try
                    RadDatePickerDa.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_INIZIO_C"), ""))
                    RadDatePickerA.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_FINE_C"), ""))
                    RadDatePickerDa.Enabled = False
                    RadDatePickerA.Enabled = False
                Catch ex As Exception
                    RadDatePickerDa.Enabled = True
                    RadDatePickerA.Enabled = True
                End Try
            End If
            lettore.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            RadWindowManager1.RadAlert("Errore durante il caricamento del piano finanziario", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")

        End Try
    End Sub

    Private Sub SPESE_REVERSIBILI_ProspettoConsuntivi_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        connData.apri()
        Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
        'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
        par.cmd.CommandText = "SELECT APPLICAZIONE_BOL_CONS,APPLICAZIONE_BOL_SCHEMA_CONS,APPLICAZIONE_BOL_PREV,APPLICAZIONE_BOL_SCHEMA_PREV FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
        Dim APPLICAZIONE_BOL_CONS As Integer = 1
        Dim APPLICAZIONE_BOL_SCHEMA_CONS As Integer = 1
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            APPLICAZIONE_BOL_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_CONS"), 1)
            APPLICAZIONE_BOL_SCHEMA_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_SCHEMA_CONS"), 1)
        End If
        lettore.Close()
        connData.chiudi()
        If APPLICAZIONE_BOL_CONS = 1 Or APPLICAZIONE_BOL_SCHEMA_CONS = 1 Then
            btnInserisci.Visible = False
            btnModifica.Visible = False
            btnElimina.Visible = False
            ButtonAnomalia.Visible = False
            ButtonEliminaVoci.Visible = False
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
End Class