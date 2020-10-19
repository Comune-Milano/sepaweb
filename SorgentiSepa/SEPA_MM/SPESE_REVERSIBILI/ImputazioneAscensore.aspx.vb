Imports Telerik.Web.UI
Partial Class SPESE_REVERSIBILI_ImputazioneAscensore
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        Page.Title = "Imputazione ascensori"
        CType(Master.FindControl("lblTitoloModulo"), Label).Text = ""
        'If controlloProfilo() Then
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request.QueryString("HiddenIdContratto")) Then
                CType(Master, Object).NascondiMenu()
                HiddenContratto.Value = Request.QueryString("HiddenIdContratto")
                HiddenEsercizio.Value = Request.QueryString("HIDDENPIANO")
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("IDPRENOTAZIONE")) Then
                CType(Master, Object).NascondiMenu()
                HiddenPrenotazione.Value = Request.QueryString("IDPRENOTAZIONE")
                HiddenEsercizio.Value = Request.QueryString("HIDDENPIANO")
            End If
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Schede di imputazione ascensori"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            CaricaGridEdifici()
            HFElencoGriglie.Value = dgvEdifici.ClientID.ToString.Replace("ctl00", "MasterPage")
        End If
        'End If
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
        btnSalva.Enabled = False
    End Sub
    Protected Sub Esporta_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_SOLLEVAMENTO")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_IMP_ASCENSORE", "SCHEDA_IMP_ASCENSORE", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        dgvEdifici.Rebind()
    End Sub
    Protected Sub Refresh_Click(sender As Object, e As System.EventArgs)
        AggiornaValori()
        dgvEdifici.Rebind()
    End Sub
    Protected Sub dgvEdifici_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvEdifici.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_SOLLEVAMENTO")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvEdifici_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        Try
            Dim dt As Data.DataTable = Session.Item("DT_SOLLEVAMENTO")
            AggiornaValori()
            connData.apri()
            For Each riga As Data.DataRow In dt.Rows
                par.cmd.CommandText = "UPDATE SISCOM_MI.I_SOLLEVAMENTO " _
                                    & " SET    TIPOLOGIA            = " & par.IfNull(riga.Item("TIPOLOGIA"), 0) & ", " _
                                    & "        NUM_FERMATE          = " & par.IfNull(riga.Item("FERMATE"), 0) & ", " _
                                    & "        PU_VISITA_MENSILE    = " & par.VirgoleInPunti(par.IfNull(riga.Item("PU_VISITA_MENSILE"), 0)) & ", " _
                                    & "        N_VISITE_MENSILI     = " & par.VirgoleInPunti(par.IfNull(riga.Item("N_VISITE_MENSILI"), 0)) & " " _
                                    & " WHERE  ID                   = " & riga.Item("ID")
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.I_SOLLEVAMENTO_TMP " _
                                   & " SET    TIPOLOGIA             = " & par.IfNull(riga.Item("TIPOLOGIA"), 0) & ", " _
                                   & "        NUM_FERMATE           = " & par.IfNull(riga.Item("FERMATE"), 0) & ", " _
                                   & "        PU_VISITA_MENSILE     = " & par.VirgoleInPunti(par.IfNull(riga.Item("PU_VISITA_MENSILE"), 0)) & ", " _
                                   & "        N_VISITE_MENSILI      = " & par.VirgoleInPunti(par.IfNull(riga.Item("N_VISITE_MENSILI"), 0)) & " " _
                                   & " WHERE  ID                    = " & riga.Item("ID") _
                                   & " and id_prenotazione          = " & HiddenPrenotazione.Value
                par.cmd.ExecuteNonQuery()
            Next
            connData.chiudi(True)
            RadWindowManager1.RadAlert("Operazione completata.", 300, 150, "Successo", "", "null")
            'RadNotificationNote.Text = "Operazione completata!"
            'RadNotificationNote.Show()
            CaricaGridEdifici()
            dgvEdifici.Rebind()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: btnSalva_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento degli edifici!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub
    Protected Sub dgvEdifici_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles dgvEdifici.PageIndexChanged
        AggiornaValori()
        dgvEdifici.CurrentPageIndex = e.NewPageIndex
    End Sub
    Private Sub CaricaGridEdifici()
        Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
        connData.apri()
        par.cmd.CommandText = "select id from siscom_mi.pf_main where id_esercizio_finanziario = " & HiddenEsercizio.Value
        Dim idpiano As Integer = par.cmd.ExecuteScalar

        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.I_SOLLEVAMENTO_TMP WHERE ID_PRENOTAZIONE=" & HiddenPrenotazione.Value
        Dim numero As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        If numero = 0 Then
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.I_SOLLEVAMENTO_TMP (SELECT I_SOLLEVAMENTO.*," & HiddenPrenotazione.Value & " FROM SISCOM_MI.I_SOLLEVAMENTO)"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "update SISCOM_MI.I_SOLLEVAMENTO_TMP set N_VISITE_MENSILI=1 where ID_PRENOTAZIONE=" & HiddenPrenotazione.Value
            par.cmd.ExecuteNonQuery()
        End If
        par.cmd.CommandText = "SELECT IMPIANTI.ID, (SELECT    COMPLESSI_IMMOBILIARI.COD_COMPLESSO || ' - ' || COMPLESSI_IMMOBILIARI.DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID = IMPIANTI.ID_COMPLESSO)AS COMPLESSO, " _
                            & " (SELECT COD_EDIFICIO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO) AS COD_EDIFICIO, (SELECT DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO) AS NOME_EDIFICIO, " _
                            & " (SELECT RAGIONE_SOCIALE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID = (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_FORNITORI_EDIFICI WHERE     ID_EDIFICIO = IMPIANTI.ID_EDIFICIO AND ID_PF = 0 AND ID_TIPO_SPESA = 3)) AS FORNITORE, " _
                            & " (SELECT ID_FORNITORE FROM SISCOM_MI.IMPUTAZIONE_FORNITORI_EDIFICI WHERE     ID_EDIFICIO = IMPIANTI.ID_EDIFICIO AND ID_PF = 0 AND ID_TIPO_SPESA = 3) AS ID_FORNITORE, I_SOLLEVAMENTO_TMP.MATRICOLA AS MATRICOLA_IMPIANTO, " _
                            & " (SELECT INDIRIZZI.DESCRIZIONE || ' ' || INDIRIZZI.CIVICO FROM SISCOM_MI.INDIRIZZI WHERE ID IN (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO)) AS INDIRIZZO_IMPIANTO, " _
                            & " (SELECT WM_CONCAT (DESCRIZIONE) FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.IMPIANTI_SCALE WHERE ID_IMPIANTO = IMPIANTI.ID)) AS SCALA_IMPIANTO,  " _
                            & " (SELECT LOCALITA FROM SISCOM_MI.INDIRIZZI WHERE ID IN (SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = IMPIANTI.ID_EDIFICIO)) AS LOCALITA, TIPOLOGIA, NUM_FERMATE AS FERMATE, " _
                            & " (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS PU_VISITA_MENSILE, I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI,  " _
                            & " I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & ")) AS TOTALE_IMPIANTO, " _
                            & " I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))  " _
                            & " * (  1 -  (SELECT MAX(SCONTO_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100)  " _
                            & " * (  1 +   (SELECT MAX(IVA_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100) AS TOT_ANNUO_SCONTATO, " _
                            & " ROUND (I_SOLLEVAMENTO_TMP.N_VISITE_MENSILI * (SELECT IMPORTO FROM SISCOM_MI.ELENCO_PREZZI_UNITARIO WHERE     ELENCO_PREZZI_UNITARIO.TIPOLOGIA = I_SOLLEVAMENTO_TMP.TIPOLOGIA AND PREZZO = 14 AND ID_ESERCIZIO_FINANZIARIO IN (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID = " & idpiano & "))  " _
                            & " * (  1 -   (SELECT MAX(SCONTO_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100)  " _
                            & " * (  1 +    (SELECT MAX(IVA_CANONE) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO=APPALTI_LOTTI_PATRIMONIO.ID_APPALTO) / 100)  " _
                            & " , 2) AS TOT_ANNUO_SCONTATO_RETT  " _
                            & " FROM SISCOM_MI.IMPIANTI, SISCOM_MI.I_SOLLEVAMENTO_TMP, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.EDIFICI " _
                            & " WHERE     IMPIANTI.ID = I_SOLLEVAMENTO_TMP.ID " _
                            & " AND COD_TIPOLOGIA = 'SO' " _
                            & " AND EDIFICI.ID = IMPIANTI.ID_EDIFICIO  " _
                            & " AND IMPIANTI.ID_EDIFICIO = APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                            & " AND SISCOM_MI.APPALTI_LOTTI_PATRIMONIO. ID_APPALTO =  " & HiddenContratto.Value _
                            & "AND I_SOLLEVAMENTO_TMP.ID_PRENOTAZIONE=" & HiddenPrenotazione.Value
        Dim DT As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
        Session.Item("DT_SOLLEVAMENTO") = DT
    End Sub
    Private Sub AggiornaValori()
        Try
            Dim dt As New Data.DataTable
            dt = CType(Session.Item("DT_SOLLEVAMENTO"), Data.DataTable)
            Dim row As Data.DataRow
            connData.apri()
            For Each item As GridDataItem In dgvEdifici.Items
                Dim txtTipologia As String = CType(item.FindControl("txtTipologia"), RadNumericTextBox).Text
                Dim txtFermate As String = CType(item.FindControl("txtFermate"), RadNumericTextBox).Text
                ' Dim txtPuVisitaMensile As String = CType(item.FindControl("txtPuVisitaMensile"), RadNumericTextBox).Text
                Dim txtNumVisiteMensili As String = CType(item.FindControl("txtNumVisiteMensili"), RadNumericTextBox).Text
                row = dt.Select("id = " & item("ID").Text)(0)
                If par.IfNull(row.Item("TIPOLOGIA"), "0") <> txtTipologia Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_IMPIANTI (ID_IMPIANTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F304','Modifica valore ''TIPOLOGIA'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("TIPOLOGIA").ToString), "- - -") & "  a  " & par.PulisciStrSql(txtTipologia) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("TIPOLOGIA") = par.IfEmpty(txtTipologia, 0)

                If par.IfNull(row.Item("FERMATE"), "0") <> txtFermate Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_IMPIANTI (ID_IMPIANTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F304','Modifica valore ''FERMATE'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("FERMATE").ToString), "- - -") & "  a  " & par.PulisciStrSql(txtFermate) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("FERMATE") = par.IfEmpty(txtFermate, 0)

                'If par.IfNull(row.Item("PU_VISITA_MENSILE"), "0") <> txtPuVisitaMensile Then
                '    'SALVATAGGIO EVENTO NELL' EDIFICIO
                '    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_IMPIANTI (ID_IMPIANTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                '                    & "VALUES (" & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                '                    & "'F304','Modifica valore ''PU VISITA MENSILE'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("PU_VISITA_MENSILE").ToString), "- - -") & "  a  " & par.PulisciStrSql(txtPuVisitaMensile) & "')"
                '    par.cmd.ExecuteNonQuery()
                'End If
                'row.Item("PU_VISITA_MENSILE") = par.IfEmpty(txtPuVisitaMensile, 0)

                If par.IfNull(row.Item("N_VISITE_MENSILI"), "0") <> txtNumVisiteMensili Then
                    'SALVATAGGIO EVENTO NELL' EDIFICIO
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_IMPIANTI (ID_IMPIANTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & item("ID").Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F304','Modifica valore ''NUMERO VISITE MENSILI'' da  " & par.IfEmpty(par.PulisciStrSql(row.Item("N_VISITE_MENSILI").ToString), "- - -") & "  a  " & par.PulisciStrSql(txtNumVisiteMensili) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
                row.Item("N_VISITE_MENSILI") = par.IfEmpty(txtNumVisiteMensili, 0)
            Next
            connData.chiudi(True)
            Session.Item("DT_SOLLEVAMENTO") = dt
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: AggiornaCheck - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il salvataggio!", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx?';}", "null")
        End Try
    End Sub

    Private Sub SPESE_REVERSIBILI_ImputazioneAscensore_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "setVisibilitaPulsanti();", True)
    End Sub
End Class