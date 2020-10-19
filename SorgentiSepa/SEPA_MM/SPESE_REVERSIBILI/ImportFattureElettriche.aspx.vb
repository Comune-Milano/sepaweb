Imports System.Data
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_ImportFattureElettriche
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Gestione - Fatture elettriche"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            If Not IsPostBack Then
                settaCampi()
                caricaPiano()
                CaricaGridUnita()
                HFGriglia.Value = dgvEdifici.ClientID.ToString.Replace("ctl00", "MasterPage")
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
        btnImporta.Enabled = False
    End Sub
    Private Sub settaCampi()
        Try
            connData.apri(False)
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.CRITERI_RIPARTIZIONE ORDER BY DESCRIZIONE ASC", cmbCriterioRipartizioneForzaMotrice, "ID", "DESCRIZIONE", True)
            cmbCriterioRipartizioneForzaMotrice.SelectedValue = 4
            cmbCriterioRipartizioneForzaMotrice.Enabled = False
            par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.CRITERI_RIPARTIZIONE ORDER BY DESCRIZIONE ASC", cmbCriterioRipartizioneLuce, "ID", "DESCRIZIONE", True)
            cmbCriterioRipartizioneLuce.SelectedValue = 4
            cmbCriterioRipartizioneLuce.Enabled = False
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: settaCampi - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub Seleziona_Tutti(sender As Object, e As System.EventArgs)
        Try
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Seleziona_Tutti - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Private Sub AggiornaCheck()
        Try
            Dim querySelect As String = Trim(HiddenIdEdificio.Value)
            If querySelect.EndsWith("OR") Then
                querySelect = querySelect.Substring(0, querySelect.Length - 2)
            End If
            If Not String.IsNullOrEmpty(querySelect) Then
                Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
                Dim rows = dtdett.Select(querySelect)
                For Each r As Data.DataRow In rows
                    If r.Item("CHECK1") = "TRUE" Then
                        r.Item("CHECK1") = "FALSE"
                    Else
                        r.Item("CHECK1") = "TRUE"
                    End If
                Next
                HiddenIdEdificio.Value = ""
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaCheck - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub dgvEdifici_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvEdifici.PageIndexChanged
        AggiornaCheck()
        dgvEdifici.CurrentPageIndex = e.NewPageIndex
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_EDIFICI")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        dtNuova.Columns(1).ColumnName = "SUDDIVISIONE 90-10"
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_FATTURE_ELETTRICHE", "SCHEDA_IMP_FATTURE_ELETTRICHE", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvEdifici.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        dgvEdifici.Rebind()
    End Sub
    Private Sub Esporta(ByVal dt As Data.DataTable)
        Dim xls As New ExcelSiSol
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "CondizioniEsecuzione", "CondizioniEsecuzione", dt)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub
    Private Sub dgvEdifici_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvEdifici.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_EDIFICI")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvEdifici_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Private Sub CaricaGridUnita()
        par.cmd.CommandText = "SELECT ID, COD_COMPLESSO || ' - ' || DENOMINAZIONE AS DENOMINAZIONE , (CASE WHEN ID IN (100000010,300000009) THEN 'TRUE' ELSE 'FALSE' END) AS CHECK1 FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID <> 1 ORDER BY COMPLESSI_IMMOBILIARI.DENOMINAZIONE ASC"
        Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_EDIFICI") = dt
    End Sub
    Protected Sub dgvEdifici_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles dgvEdifici.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            CType(dataItem.FindControl("CheckBox1"), CheckBox).Attributes.Add("onclick", "javascript:selezionaCheckSingolo(" & dataItem("ID").Text & ");")
        End If
    End Sub
    Protected Sub headerChkbox_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim dtdett As Data.DataTable = Session.Item("DT_EDIFICI")
            For Each r As Data.DataRow In dtdett.Rows
                r.Item("CHECK1") = CStr(Not CBool(r.Item("CHECK1")))
            Next
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalva_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub btnImporta_Click(sender As Object, e As EventArgs) Handles btnImporta.Click
        Try
            connData.apri(True)
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            par.cmd.CommandText = "SELECT ID_PIANO_FINANZIARIO FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
            Dim idpiano As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar, 0))
            If idpiano = 0 Then
                'piano finanziario non selezionato in sede di creazione dell'elaborazione
                par.cmd.CommandText = "SELECT MAX(ID) FROM SISCOM_MI.PF_MAIN"
                idpiano = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            AggiornaCheck()
            Dim dt As Data.DataTable = Session.Item("DT_EDIFICI")
            Dim view As New Data.DataView(dt)
            Dim dtFiltrata As New Data.DataTable
            view.RowFilter = "CHECK1 = 'TRUE'"
            dtFiltrata = view.ToTable
            Dim filtro90_10 As String = ""
            Dim idComplesso As String = ""
            For Each riga As Data.DataRow In dtFiltrata.Rows
                idComplesso &= riga.Item("ID") & ","
            Next
            If Not String.IsNullOrEmpty(idComplesso) Then
                idComplesso = idComplesso.Substring(0, idComplesso.LastIndexOf(","))
            End If
            par.cmd.CommandText = "SISCOM_MI.CONSUNTIVO_ELETTRICO"
            par.cmd.CommandType = Data.CommandType.StoredProcedure
            par.cmd.Parameters.Add("ris", 0).Direction = Data.ParameterDirection.ReturnValue
            par.cmd.Parameters.Add("idPf", idElaborazione)
            par.cmd.Parameters.Add("rip", cmbCriterioRipartizioneLuce.SelectedValue)
            par.cmd.Parameters.Add("idPiano", idpiano)
            par.cmd.Parameters.Add("rip2", cmbCriterioRipartizioneForzaMotrice.SelectedValue)
            par.cmd.Parameters.Add("complessi", idComplesso)
            par.cmd.Parameters.Add("perc1", par.VirgoleInPunti(CInt(par.IfEmpty(txtDivisioneLuce.Text, txtDivisioneLuce.DisplayText))) / 100)
            par.cmd.Parameters.Add("perc2", par.VirgoleInPunti(CInt(par.IfEmpty(txtDivisioneForzaMotrice.Text, txtDivisioneForzaMotrice.DisplayText))) / 100)
            par.cmd.ExecuteNonQuery()
            Dim risOp As Integer = par.cmd.Parameters("ris").Value
            Dim Tempo As String = Format(Now, "yyyyMMddHHmmss")
            par.cmd.Parameters.Clear()
            par.cmd.CommandType = Data.CommandType.Text
            If risOp > 0 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SPESE_REVER_LOG (ID_PF,ID_OPERATORE, DATA_ORA, CAMPO, VAL_PRECEDENTE, VAL_IMPOSTATO, ID_OPERAZIONE) " _
                    & " VALUES (" & Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") & "," & Session.Item("ID_OPERATORE") & ", '" & Tempo & "', '-'," _
                    & "'-', '-' , 6)"
                par.cmd.ExecuteNonQuery()
            End If
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Operazione effettuata: importate " & risOp & " fatture elettriche.", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?';}", "null")
            'RadNotificationNote.Text = "Operazione effettuata: importate " & ris & " fatture elettriche"
            'RadNotificationNote.Show()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnImporta_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub dgvEdifici_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles dgvEdifici.ItemCommand
        Try
            Select Case e.CommandName.ToUpper
                Case "FILTER"
                    AggiornaCheck()
            End Select
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalva_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
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
                    txtDataInizioForzaMotrice.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_INIZIO_C"), ""))
                    txtDataFineForzaMotrice.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_FINE_C"), ""))
                    txtDataInizioForzaMotrice.Enabled = False
                    txtDataFineForzaMotrice.Enabled = False

                    txtDataInizioLuce.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_INIZIO_C"), ""))
                    txtDataFineLuce.SelectedDate = CDate(par.IfNull(lettore("DATA_RIFERIMENTO_FINE_C"), ""))
                    txtDataInizioLuce.Enabled = False
                    txtDataFineLuce.Enabled = False
                Catch ex As Exception
                    txtDataInizioForzaMotrice.Enabled = True
                    txtDataFineForzaMotrice.Enabled = True

                    txtDataInizioLuce.Enabled = True
                    txtDataFineLuce.Enabled = True
                End Try
            End If
            lettore.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            RadWindowManager1.RadAlert("Errore durante il caricamento del piano finanziario", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
        End Try
    End Sub
End Class
