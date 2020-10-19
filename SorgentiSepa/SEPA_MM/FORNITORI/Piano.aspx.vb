Imports System.IO
Imports Telerik.Web.UI

Partial Class FORNITORI_Piano
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            TipoAllegato.Value = par.getIdOggettoAllegatiWs("Cronoprogramma attività")
            idCronoprogramma.Value = Request.QueryString("ID")
            connData.apri()
            caricaProfilo()
            caricaPiano()
            VerificaPulsanti()
            connData.chiudi()
            Dim idPiano As String = 0
            HiddenProvenienza.Value = Request.QueryString("PROVENIENZA")

            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If
            lblTitolo.Text &= " " & idPiano
        End If
    End Sub

    Private Sub SolaLettura()
        btnSalva.Visible = False
        btnModifica.Visible = False
        btnApp.Visible = False
        btnAllegati.Visible = False
        btnUploadRendicontazione.Visible = False

    End Sub

    Private Sub caricaProfilo()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or Session.Item("FL_SUPERDIRETTORE") = "1" Then
                'DIRETTORE LAVORI
                gestioneDirettoreLavori()
            End If
            If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                'FORNITORE ESTERNO
                gestioneFornitoreEsterno()
            End If
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CreaPiano - caricaProfilo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub gestioneFornitoreEsterno()
        par.cmd.CommandText = "SELECT MOD_FO_ID_FO FROM SEPA.OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE")
        Dim idOperatore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        idFornitore.Value = idOperatore
        idDirettoreLavori.Value = 0
        btnApp.Visible = False
        btnMostraEdifici.Visible = False
    End Sub

    Private Sub gestioneDirettoreLavori()
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        idDirettoreLavori.Value = idOperatore
        idFornitore.Value = 0
        btnElimina.Visible = False
        btnModifica.Visible = False
        'btnEdificiNonValorizzati.Visible = False
    End Sub

    Private Sub caricaPiano()
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idPiano As String = 0
            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If
            Dim condizioneDirettoreLavori As String = ""
            Dim condizioneFornitori As String = ""
            'If idDirettoreLavori.Value > 0 Then
            '    condizioneDirettoreLavori = " AND APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE=" & idDirettoreLavori.Value & " AND DATA_FINE_INCARICO='30000000')"
            'End If
            'If idFornitore.Value > 0 Then
            '    condizioneFornitori = " AND FORNITORI.ID=" & idFornitore.Value
            'End If
            par.cmd.CommandText = " SELECT  " _
                & " PROGRAMMA_aTTIVITA.ID,  " _
                & " PROGRAMMA_aTTIVITA.ID_STATO,  " _
                & " PROGRAMMA_aTTIVITA.ID_GRUPPO,  " _
                & " RAGIONE_SOCIALE AS FORNITORE, " _
                & " NUM_REPERTORIO||'-'||APPALTI.DESCRIZIONE AS APPALTO,   " _
                & " (SELECT REPLACE (DESCRIZIONE, '#', '') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE ID = PROGRAMMA_ATTIVITA.ATTIVITA_CRONOPROGRAMMA) AS ATTIVITA_CRONOPROGRAMMA, " _
                & " (SELECT UPPER(DESCRIZIONE) FROM SISCOM_MI.TAB_TIPOLOGIA_CRONOPROGRAMMA WHERE ID = ID_TIPO_CRONOPROGRAMMA) AS TIPOLOGIA_CRONOPROGRAMMA, " _
                & " PROGRAMMA_ATTIVITA.ATTIVITA_CRONOPROGRAMMA AS ID_ATTIVITA_CRONOPROGRAMMA, " _
                & " TO_CHAR(TO_DATE(DATA_INSERIMENTO,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO,  " _
                & " TO_CHAR(TO_DATE(DATA_ULTIMA_MODIFICA,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ULTIMA_MODIFICA, " _
                & " TO_CHAR(TO_DATE(DATA_ULTIMA_APPROVAZIONE,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_ULTIMA_APPROVAZIONE,  " _
                & " (CASE WHEN PROGRAMMA_ATTIVITA.ID_sTATO=0 THEN 'NON APPROVATO' ELSE 'APPROVATO' END) AS STATO, " _
                & " TO_DATE (PROGRAMMA_ATTIVITA.DATA_INIZIO, 'YYYYMMDD') AS DATA_INIZIO, " _
                & " TO_DATE (PROGRAMMA_ATTIVITA.DATA_FINE, 'YYYYMMDD') AS DATA_FINE " _
                & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                & " WHERE " _
                & " PROGRAMMA_aTTIVITA.ID_FORNITORE=FORNITORI.ID " _
                & " AND FORNITORI.ID=APPALTI.ID_FORNITORE " _
                & " AND APPALTI.ID_GRUPPO=PROGRAMMA_ATTIVITA.ID_GRUPPO " _
                & condizioneDirettoreLavori _
                & condizioneFornitori _
                & " AND APPALTI.ID_GRUPPO=APPALTI.ID " _
                & " AND PROGRAMMA_ATTIVITA.ID=" & idPiano
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                txtFornitore.Text = par.IfNull(lettore("FORNITORE"), "")
                txtAppalto.Text = par.IfNull(lettore("APPALTO"), "")
                txtAttivita.Text = par.IfNull(lettore("ATTIVITA_CRONOPROGRAMMA"), "")
                txtTipologia.Text = par.IfNull(lettore("TIPOLOGIA_CRONOPROGRAMMA"), "")
                txtDataInizio.SelectedDate = par.IfNull(lettore("DATA_INIZIO"), "")
                txtDataFine.SelectedDate = par.IfNull(lettore("DATA_FINE"), "")
                txtDataInserimento.Text = par.IfNull(lettore("DATA_INSERIMENTO"), "")
                txtDataUltimaModifica.Text = par.IfNull(lettore("DATA_ULTIMA_MODIFICA"), "")
                txtDataUltimaApprovazione.Text = par.IfNull(lettore("DATA_ULTIMA_APPROVAZIONE"), "")
                txtStato.Text = par.IfNull(lettore("STATO"), "")
                idGruppo.Value = par.IfNull(lettore("ID_GRUPPO"), 0)
                idStato.Value = par.IfNull(lettore("ID_STATO"), 0)
                idAttivitaCronoprogramma.Value = par.IfNull(lettore("ID_ATTIVITA_CRONOPROGRAMMA"), 0)
            End If
            lettore.Close()
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CreaPiano - caricaProfilo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As System.EventArgs) Handles btnDownload.Click
        Try
            Dim connAperta As Boolean = False
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connData.apri(False)
                connAperta = True
            End If
            Dim idPiano As String = 0
            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If

            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQueryCronoprogramma())
            Dim xls As New ExcelSiSol
            Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportCronoprogramma_" & idPiano & "_" & txtTipologia.Text & "_" & txtAttivita.Text & "_", "ExportCronoprogramma_" & txtTipologia.Text & "_" & txtAttivita.Text & "_", dt, True)

            par.EffettuaDownloadFile(Me.Page, nomeFile)
            If connAperta = True Then
                connData.chiudi(False)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnAllega_Click(sender As Object, e As System.EventArgs) Handles btnModifica.Click
        Try
            Dim nFile As String = ""
            Dim estensioneFile As String = ""
            For Each file As UploadedFile In CType(RadUploadAllegato, RadAsyncUpload).UploadedFiles
                nFile = file.GetName()
                estensioneFile = file.GetExtension()
                Dim nFileS = Mid(nFile, Len(nFile) - 3, 4)
                file.SaveAs(Server.MapPath("..\FileTemp\") & nFile)
                System.Threading.Thread.Sleep(3000)
            Next
            If File.Exists(Server.MapPath("..\FileTemp\") & nFile) AndAlso estensioneFile = ".xlsx" Then
                Dim xls As New ExcelSiSol
                Dim dtFoglio1 As New Data.DataTable
                Using pck As New OfficeOpenXml.ExcelPackage()
                    Using stream = File.Open(Server.MapPath("..\FileTemp\") & nFile, FileMode.Open, FileAccess.Read)
                        pck.Load(stream)
                        stream.Close()
                    End Using
                    Dim ws As OfficeOpenXml.ExcelWorksheet = pck.Workbook.Worksheets(1)
                    dtFoglio1 = xls.WorksheetToDataTable(ws, True)
                End Using
                connData.apri(True)
                Dim codEdificio As String = ""
                Dim idPiano As String = 0
                If IsNumeric(Request.QueryString("ID")) Then
                    idPiano = Request.QueryString("ID")
                End If
                Dim ris As Integer = 0
                Dim dateFuoriRange As Boolean = False
                ' par.cmd.CommandText = "DELETE FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE PROGRAMMA_ATTIVITA_DETT.ID_PROGRAMMA_ATTIVITA=" & idPiano & " AND ID_PROGRAMMA_ATTIVITA IN (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID_STATO=0)"
                par.cmd.CommandText = "update siscom_mi.PROGRAMMA_ATTIVITA_DETT " _
                                    & "set fl_cancellato = 1 " _
                                    & "WHERE PROGRAMMA_ATTIVITA_DETT.ID_PROGRAMMA_ATTIVITA = " & idPiano
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.PROGRAMMA_ATTIVITA SET ID_STATO=0,DATA_ULTIMA_MODIFICA='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idPiano
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "SELECT SISCOM_MI.GETDIFFDATAORA(" & par.AggiustaData(txtDataFine.SelectedDate) & "," & par.AggiustaData(txtDataInizio.SelectedDate) & ") FROM DUAL"
                Dim numeroDate As Integer = CInt(par.cmd.ExecuteScalar)
                For Each riga1 As Data.DataRow In dtFoglio1.Rows
                    For i As Integer = 1 To numeroDate + 1
                        'Inserimento di un try con catch vuoto perchè da errore se nel radgrid ci sono intestazioni di colonne non presenti nell'excel
                        'Es. Data dal 3/10 al 9/10
                        'Se aumento il range di date o lo diminiusco, e carico un excel vecchio avrò date diverse da quelle del radgrid
                        'L'UPLOAD DEVE ESSERE COMUNQUE EFFETTUATO, MA NON VERRANNO CONSIDERATE LE DATE DIVERSE
                        Try
                            If IsDate(riga1.Item("DATA " & i).ToString) Then
                                If par.AggiustaData(riga1.Item("DATA " & i).ToString) >= par.AggiustaData(txtDataInizio.SelectedDate) And
                                 par.AggiustaData(riga1.Item("DATA " & i).ToString) <= par.AggiustaData(txtDataFine.SelectedDate) Then

                                    par.cmd.CommandText = "select data " _
                                                        & " From siscom_mi.PROGRAMMA_ATTIVITA_DETT " _
                                                        & " Where PROGRAMMA_ATTIVITA_DETT.ID_PROGRAMMA_ATTIVITA = " & idPiano _
                                                        & " And Data = " & par.AggiustaData(riga1.Item("DATA " & i).ToString) _
                                                        & " and id_edificio = (Select ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO='" & riga1.Item("CODICE EDIFICIO").ToString & "')" _
                                                        & " and indice = " & i

                                    Dim data As String = par.cmd.ExecuteScalar
                                    If data = par.AggiustaData(riga1.Item("DATA " & i).ToString) Then
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                                                            & " Set FL_CANCELLATO = 0, fl_approvato = 0 " _
                                                            & " Where PROGRAMMA_ATTIVITA_DETT.ID_PROGRAMMA_ATTIVITA = " & idPiano _
                                                            & " And Data = " & par.AggiustaData(riga1.Item("DATA " & i).ToString) _
                                                            & " and id_edificio = (Select ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO='" & riga1.Item("CODICE EDIFICIO").ToString & "')" _
                                                            & " and indice = " & i
                                        ris = par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PROGRAMMA_ATTIVITA_DETT (ID_PROGRAMMA_ATTIVITA,DATA,ID_EDIFICIO,INDICE, DATA_ORA_OPERAZIONE, ID_OPERATORE) " _
                                                        & " VALUES(" & idPiano & "," & par.AggiustaData(riga1.Item("DATA " & i).ToString) & "," _
                                                        & " (Select ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO='" & riga1.Item("CODICE EDIFICIO").ToString & "')," & i & "," & Format(Now, "yyyyMMddHHmm") & "," & Session.Item("ID_OPERATORE") & ")"
                                        ris = par.cmd.ExecuteNonQuery()
                                    End If



                                Else
                                    dateFuoriRange = True
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    Next
                Next
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PROGRAMMA_ATTIVITA(ID_PROGRAMMA_ATTIVITA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE)" _
                    & "VALUES(" & idPiano & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F310','Modifica del piano per il programma delle attività')"
                par.cmd.ExecuteNonQuery()



                Dim alertNotifica As String = "Operazione effettuata!"
                Dim condizioneEdificiNonValorizzati As String = " And id_edificio Not In  (Select id_edificio from siscom_mi.programma_attivita_dett where id_programma_attivita = " & idPiano & " And fl_cancellato = 0) "

                Dim query As String = "SELECT DISTINCT " _
                    & " COD_EDIFICIO AS ""CODICE EDIFICIO""," _
                    & " DENOMINAZIONE " _
                    & " FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI " _
                    & " WHERE APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & idGruppo.Value _
                    & " AND EDIFICI.ID=APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                    & condizioneEdificiNonValorizzati _
                    & " ORDER BY DENOMINAZIONE"
                Dim dt As Data.DataTable = par.getDataTableGrid(query)
                If dt.Rows.Count > 0 Then
                    alertNotifica &= "<br />Cronoprogramma incompleto, premere il pulsante <strong>Mostra edifici non valorizzati</strong> per verifiche! "
                End If
                connData.chiudi(True)
                If dateFuoriRange Then
                    alertNotifica &= "<br />Le date fuori range non sono state considerate!"
                End If
                RadWindowManager1.RadAlert(alertNotifica, 350, 150, "Attenzione", Nothing, Nothing)
                caricaPiano()
                DataGridCronoprogramma.Rebind()
                File.Delete(Server.MapPath("..\FileTemp\") & nFile)
            Else
                RadWindowManager1.RadAlert("Tipo file non valido. Selezionare un file <strong>.xlsx</strong>", 350, 150, "Attenzione", Nothing, Nothing)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnDownload_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub VisualizzaAlert(ByVal TestoMessaggio As String, ByVal Tipo As Integer)
        Select Case Tipo
            Case 1
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "Success", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','success');", True)
            Case 2
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "warn", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','warn');", True)
            Case 3
                ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "error", "$.notify('" & Replace(TestoMessaggio, "'", "\'") & "','error');", True)
        End Select
    End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Try
            connData.apri(True)
            Dim idPiano As String = 0
            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If
            par.cmd.CommandText = "SELECT ID_STATO FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID=" & idPiano
            Dim idStato As Integer = par.IfNull(par.cmd.ExecuteScalar, -1)
            Dim eliminato As Boolean = False
            If idStato = 0 Then
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_PROGRAMMA_ATTIVITA = " & idPiano
                Dim numero As Integer = par.cmd.ExecuteScalar
                If numero = 0 Then
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.PROGRAMMA_ATTIVITA_dETT WHERE ID_PROGRAMMA_aTTIVITA=" & idPiano
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID=" & idPiano
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PROGRAMMA_aTTIVITA(ID_PROGRAMMA_aTTIVITA,ID_OPERATORE,DATA_ORA,COD_eVENTO,MOTIVAZIONE)" _
                            & "VALUES(" & idPiano & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F314','Eliminazione del piano per il programma delle attività')"
                    par.cmd.ExecuteNonQuery()
                    eliminato = True
                Else
                    RadWindowManager1.RadAlert("Il cronoprogramma è legato a una o più segnalazioni! <br />Impossibile eliminare!", 350, 150, "Attenzione", Nothing, Nothing)
                End If

            Else
                VisualizzaAlert("Il piano è approvato e pertanto non può essere eliminato!", 2)
            End If
            connData.chiudi(True)
            If eliminato = True Then
                VisualizzaAlert("Piano eliminato correttamente!", 1)
                Response.Redirect("CaricaPiani.aspx", False)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnElimina_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnApp_Click(sender As Object, e As System.EventArgs) Handles btnApp.Click
        Try
            Dim idPiano As String = 0
            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If
            connData.apri(False)

            Dim condizioneEdificiNonValorizzati As String = " And id_edificio Not In  (Select id_edificio from siscom_mi.programma_attivita_dett where id_programma_attivita = " & idPiano & " And fl_cancellato = 0) "

            Dim query As String = "SELECT DISTINCT " _
                & " COD_EDIFICIO AS ""CODICE EDIFICIO""," _
                & " DENOMINAZIONE " _
                & " FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI " _
                & " WHERE APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & idGruppo.Value _
                & " AND EDIFICI.ID=APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & condizioneEdificiNonValorizzati _
                & " ORDER BY DENOMINAZIONE"
            Dim dt As Data.DataTable = par.getDataTableGrid(query)
            If dt.Rows.Count > 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "attenzione", "document.getElementById('btnApp1').click()", True)

            Else
                ApprovaCronoprogramma()
            End If
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnUploadRendicontazione_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnApp1_Click(sender As Object, e As System.EventArgs) Handles btnApp1.Click
        ApprovaCronoprogramma()

    End Sub
    Private Sub btnUploadRendicontazione_Click(sender As Object, e As EventArgs) Handles btnUploadRendicontazione.Click
        Try
            connData.apri(True)
            Dim nFile As String = ""
            Dim nFileShort As String = ""
            Dim nFileExt As String = ""
            Dim descrizione As String = "Scheda di rendicontazione"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("Scheda di rendicontazione")
            par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
            Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
            For Each fileTemp As UploadedFile In CType(radUploadFileRendicontazione, RadAsyncUpload).UploadedFiles
                nFile = fileTemp.GetName()
                nFileShort = fileTemp.GetNameWithoutExtension()
                nFileExt = fileTemp.GetExtension()
                Dim nFileS = Mid(nFile, Len(nFile) - 3, 4)
                'If File.Exists(Server.MapPath("..\FileTemp\") & nFile) Then
                '    File.Delete(Server.MapPath("..\FileTemp\") & nFile)
                'End If
                fileTemp.SaveAs(Server.MapPath("..\FileTemp\") & nFile, True)
                par.AllegaDocumentoWS(Server.MapPath("../FileTemp/" & nFile), nFile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, idCronoprogramma.Value, "../ALLEGATI/CRONOPROGRAMMA/", , nFileShort & "_" & Format(Now, "yyyyMMddHHmmss") & nFileExt)
            Next
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnUploadRendicontazione_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub DataGridCronoprogramma_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles DataGridCronoprogramma.NeedDataSource
        Try
            connData.apri(False)
            Dim Query As String = EsportaQueryCronoprogramma(True)
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            DataGridCronoprogramma.DataSource = dt
            'DataGridCronoprogramma.MasterTableView.Width = 200%
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - DataGridCronoprogramma_NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function EsportaQueryCronoprogramma(Optional rielaborato As Boolean = False) As String
        Dim stringa As String = ""
        par.cmd.CommandText = "SELECT SISCOM_MI.GETDIFFDATAORA(" & par.AggiustaData(txtDataFine.SelectedDate) & "," & par.AggiustaData(txtDataInizio.SelectedDate) & ") FROM DUAL"
        Dim numeroDate As Integer = CInt(par.cmd.ExecuteScalar)
        Dim idPiano As String = 0
        If IsNumeric(Request.QueryString("ID")) Then
            idPiano = Request.QueryString("ID")
        End If
        For i As Integer = 1 To numeroDate + 1
            stringa &= ",(SELECT SISCOM_MI.GETDATA(DATA) FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE ID_PROGRAMMA_ATTIVITA=" & idPiano & " AND INDICE=" & i & " AND ID_EDIFICIO=EDIFICI.ID AND FL_CANCELLATO = 0) AS ""DATA " & i & """"
            If rielaborato Then
                stringa &= ",(SELECT COUNT(*) FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE ID_PROGRAMMA_ATTIVITA=" & idPiano & " AND INDICE=" & i & " AND ID_EDIFICIO=EDIFICI.ID AND FL_CANCELLATO = 1) AS RIELABORATO" & i
                stringa &= ",NVL((SELECT nvl(min(FL_APPROVATO),0) FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE ID_PROGRAMMA_ATTIVITA=" & idPiano & " AND INDICE=" & i & " AND ID_EDIFICIO=EDIFICI.ID),0) AS ""FL_APPROVATO" & i & """"
            End If
        Next
        numDate.Value = numeroDate + 1


        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        Dim idStruttura As Integer = Session.Item("ID_STRUTTURA")
        Dim condizioneStruttura As String = ""
        Dim condizioneVista As String = ""
        If idOperatore <> "1" Then

            If IsNumeric(idStruttura) AndAlso idStruttura = 105 Then
            ElseIf IsNumeric(idStruttura) AndAlso idStruttura < 105 Then
                condizioneStruttura = " and EDIFICI.ID (select id from siscom_mi.edifici where " _
                        & " EDIFICI.ID_COMPLESSO IN (SELECT ID FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")) "
            Else
                'CONDIZIONE PER ESCLUDERE LA VISIONE
                condizioneStruttura = " and EDIFICI.ID in (select id from siscom_mi.edifici where " _
                        & " EDIFICI.ID_COMPLESSO=0 ) "
            End If
            If Session.Item("FL_SUPERDIRETTORE") = "0" Then
                Dim tipologia As String = Request.QueryString("TIPOLOGIA")
                Select Case tipologia
                    Case "BM"
                        condizioneVista = " and EDIFICI.ID  in " _
                                       & " (select edifici.id " _
                                       & " from siscom_mi.edifici,siscom_mi.BUILDING_MANAGER_OPERATORI " _
                                       & " where EDIFICI.ID_BM=BUILDING_MANAGER_OPERATORI.ID_BM " _
                                       & " AND INIZIO_VALIDITA <=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                                       & " AND FINE_VALIDITA >=TO_CHAR (SYSDATE, 'yyyyMMdd') " _
                                       & " AND BUILDING_MANAGER_OPERATORI.TIPO_OPERATORE = 1 " _
                                       & " and BUILDING_MANAGER_OPERATORI.id_operatore = " & idOperatore & ")"
                    Case "FQM"
                        condizioneVista = condizioneStruttura
                    Case "TA"
                        condizioneVista = condizioneStruttura
                End Select
            End If
        End If

        EsportaQueryCronoprogramma = "SELECT DISTINCT " _
                & " COD_EDIFICIO AS ""CODICE EDIFICIO""," _
                & " DENOMINAZIONE " _
                & stringa _
                & " FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.EDIFICI " _
                & " WHERE APPALTI_LOTTI_PATRIMONIO.ID_APPALTO = " & idGruppo.Value _
                & " AND EDIFICI.ID=APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO " _
                & condizioneVista _
                        & " ORDER BY DENOMINAZIONE"
    End Function

    Private Sub DataGridCronoprogramma_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles DataGridCronoprogramma.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            For i As Integer = 1 To numDate.Value
                Dim codiceEdificio As String = dataItem("CODICE EDIFICIO").Text
                If idStato.Value = "0" Then
                    'And dataItem("FL_APPROVATO" & i).Text = 0
                    If dataItem("RIELABORATO" & i).Text > 0 Then
                        dataItem("DATA " & i).BackColor = Drawing.Color.Red
                    End If
                End If
                dataItem("DATA " & i).Attributes.Add("onmouseover", "this.style.color='#FF9900';this.style.cursor='pointer';")
                dataItem("DATA " & i).Attributes.Add("onmouseout", "this.style.color='';")
                dataItem("DATA " & i).Attributes.Add("onDblclick", "ApriDettaglioPiano(" & i & "," & Request.QueryString("ID") & ",'" & codiceEdificio & "');")
                DataGridCronoprogramma.MasterTableView.GetColumn("RIELABORATO" & i).Visible = False
                DataGridCronoprogramma.MasterTableView.GetColumn("FL_APPROVATO" & i).Visible = False
            Next
        End If
    End Sub

    Private Sub DataGridCronoprogramma_ColumnCreated(sender As Object, e As GridColumnCreatedEventArgs) Handles DataGridCronoprogramma.ColumnCreated
        e.Column.AutoPostBackOnFilter = True
        e.Column.CurrentFilterFunction = GridKnownFunction.Contains
    End Sub

    Private Sub btnSalva_Click(sender As Object, e As EventArgs) Handles btnSalva.Click
        Try
            connData.apri(True)
            Dim idPiano As String = 0
            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If
            Dim continua As Boolean = False
            Dim datafine As Integer = CInt(par.AggiustaData(txtDataFine.SelectedDate))
            par.cmd.CommandText = "select data_fine from siscom_mi.programma_attivita where id = " & idPiano
            Dim dataFineDB As Integer = par.cmd.ExecuteScalar
            par.cmd.CommandText = "SELECT nvl(MAX(DATA),0) FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT WHERE FL_CANCELLATO = 0 AND ID_PROGRAMMA_ATTIVITA = " & idPiano
            Dim maxDataInserita As Integer = par.cmd.ExecuteScalar
            'par.cmd.CommandText = "SELECT NVL(DATA_FINE_INIZIALE,0) from siscom_mi.programma_attivita where id = " & idPiano
            'Dim dataFineInizialeDB As Integer = par.cmd.ExecuteScalar
            'If dataFineInizialeDB = 0 Then
            '    par.cmd.CommandText = "update siscom_mi.programma_attivita set data_fine_iniziale = data_fine where id = " & idPiano
            '    par.cmd.ExecuteNonQuery()
            '    If dataFineDB - datafine <= 0 Then
            '        continua = True
            '    End If
            'Else
            '    If dataFineInizialeDB - datafine <= 0 Then
            '        continua = True
            '    End If
            'End If

            ' If continua Then
            If dataFineDB <> datafine Then
                If maxDataInserita <> 0 AndAlso maxDataInserita - par.AggiustaData(txtDataFine.SelectedDate) > 0 Then
                    'Ci sono delle date nel cronoprogramma superiori alla data di fine che si vuole impostare
                    RadWindowManager1.RadAlert("Sono presenti delle date successive alla data di fine che si vuole impostare!", 350, 150, "Attenzione", Nothing, Nothing)
                    txtDataFine.SelectedDate = par.FormattaData(dataFineDB)
                Else
                    If controlloDate() Then
                        If ControlloPeriodo(par.AggiustaData(txtDataInizio.SelectedDate), par.AggiustaData(txtDataFine.SelectedDate)) Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PROGRAMMA_ATTIVITA " _
                                    & " set data_inizio = " & par.AggiustaData(txtDataInizio.SelectedDate) _
                                    & ", data_fine = " & par.AggiustaData(txtDataFine.SelectedDate) _
                                    & ",ID_STATO = 0 " _
                                    & " where id = " & idPiano
                            par.cmd.ExecuteNonQuery()
                            'gestione date in programma_attivita_dett
                            ' Cancellare where data inizio > a quella inserita e reset date
                            par.cmd.CommandText = "update siscom_mi.programma_attivita_dett set fl_cancellato = 1 where data > " & par.AggiustaData(txtDataFine.SelectedDate) _
                                & " and fl_cancellato = 0 and id_programma_attivita = " & idPiano
                            par.cmd.ExecuteNonQuery()
                            connData.chiudi(True)
                            If dataFineDB <> par.AggiustaData(txtDataFine.SelectedDate) Then
                                WriteEvent("F315", "Modifica data fine lavori", par.FormattaData(dataFineDB), txtDataFine.SelectedDate)
                            End If
                            RadWindowManager1.RadAlert("Cronoprogramma aggiornato correttamente!", 350, 150, "Successo", "", Nothing)
                            caricaPiano()
                            DataGridCronoprogramma.Rebind()
                        Else
                            RadWindowManager1.RadAlert("Risulta già inserito un cronoprogramma per il periodo selezionato!", 350, 150, "Attenzione", Nothing, Nothing)
                        End If
                    Else
                        RadWindowManager1.RadAlert("Valorizzare correttamente le date!", 350, 150, "Attenzione", Nothing, Nothing)
                    End If
                End If
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - btnSalva_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Function controlloDate() As Boolean
        If Not IsDate(Me.txtDataInizio.SelectedDate) Or Not IsDate(Me.txtDataFine.SelectedDate) Then
            controlloDate = False
        ElseIf CInt(par.AggiustaData(txtDataFine.SelectedDate) - par.AggiustaData(txtDataInizio.SelectedDate)) >= 0 Then
            controlloDate = True
        Else
            controlloDate = False
        End If
    End Function

    Private Function ControlloPeriodo(ByVal dataInizio As String, ByVal datafine As String) As Boolean
        Dim idPiano As String = 0
        If IsNumeric(Request.QueryString("ID")) Then
            idPiano = Request.QueryString("ID")
        End If
        Dim id_tipo_cronoprogramma As String = ""
        Dim attivita_cronoprogramma As String = ""
        Dim id_gruppo As String = ""

        par.cmd.CommandText = "select ID_TIPO_CRONOPROGRAMMA, ATTIVITA_CRONOPROGRAMMA, ID_GRUPPO from  SISCOM_MI.PROGRAMMA_ATTIVITA where id = " & idPiano
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            id_tipo_cronoprogramma = par.IfNull(lettore("ID_TIPO_CRONOPROGRAMMA"), "-1")
            attivita_cronoprogramma = par.IfNull(lettore("ATTIVITA_CRONOPROGRAMMA"), "-1")
            id_gruppo = par.IfNull(lettore("ID_GRUPPO"), "-1")
        End If
        lettore.Close()
        par.cmd.CommandText = "SELECT COUNT (*) " _
                    & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA " _
                    & " WHERE  ID_TIPO_CRONOPROGRAMMA =  " & id_tipo_cronoprogramma _
                    & " AND ATTIVITA_CRONOPROGRAMMA = " & attivita_cronoprogramma _
                    & " AND ID_GRUPPO =  " & id_gruppo _
                    & " AND( ((" & dataInizio & " >= DATA_INIZIO " _
                    & " AND " & dataInizio & "<=DATA_FINE) OR (" & datafine & ">=DATA_INIZIO AND " & datafine & "<=DATA_FINE)) " _
                    & " OR( " & dataInizio & " <= DATA_INIZIO " _
                    & " AND " & datafine & " >= DATA_FINE)) and id <> " & idPiano
        Dim numero As Integer = par.cmd.ExecuteScalar
        If numero > 0 Then
            ControlloPeriodo = False
        Else
            ControlloPeriodo = True
        End If
    End Function


    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String, Optional ByVal valoreVecchio As String = "", Optional ByVal valoreNuovo As String = "", Optional idSegn As Integer = 0)
        Dim connOpNow As Boolean = False
        Dim idPiano As String = 0
        If IsNumeric(Request.QueryString("ID")) Then
            idPiano = Request.QueryString("ID")
        End If
        Try
            connData.apri(True)
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PROGRAMMA_ATTIVITA ( " _
                     & "    COD_EVENTO, DATA_ORA, ID_OPERATORE,  " _
                     & "    ID_PROGRAMMA_ATTIVITA, MOTIVAZIONE, VALORE_NEW,  " _
                     & "    VALORE_OLD)  " _
                     & " VALUES ( " _
                     & par.insDbValue(cod, True) & "  /* COD_EVENTO */, " _
                     & "'" & Format(Now, "yyyyMMddHHmmss") & "'  /* DATA_ORA */, " _
                     & Session.Item("ID_OPERATORE") & "  /* ID_OPERATORE */, " _
                     & idPiano & "  /* ID_PROGRAMMA_ATTIVITA */, " _
                     & par.insDbValue(motivo, True) & "  /* MOTIVAZIONE */, " _
                     & par.insDbValue(valoreNuovo, True) & "  /* VALORE_NEW */, " _
                     & par.insDbValue(valoreVecchio, True) & "  /* VALORE_OLD */ )"
            par.cmd.ExecuteNonQuery()
            connData.chiudi(True)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Cronoprogramma - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If Not String.IsNullOrEmpty(Request.QueryString("SL")) AndAlso IsNumeric(Request.QueryString("SL")) Then
            SolaLettura()
        End If
    End Sub

    Private Sub FORNITORI_Piano_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Private Sub VerificaPulsanti()
        Dim tipologia As String = Request.QueryString("TIPOLOGIA")
        Select Case tipologia
            Case "BM"
                btnDownload.Visible = True
                btnUploadRendicontazione.Visible = False
                btnApp.Visible = False
                txtDataFine.Enabled = False
            Case "FQM"
                btnDownload.Visible = True
                btnUploadRendicontazione.Visible = False
                btnApp.Visible = False
            Case "TA"
                btnDownload.Visible = True
                btnUploadRendicontazione.Visible = False
                btnApp.Visible = False
        End Select
    End Sub

    Private Sub ApprovaCronoprogramma()
        Try
            Dim idPiano As String = 0
            If IsNumeric(Request.QueryString("ID")) Then
                idPiano = Request.QueryString("ID")
            End If
            connData.apri(True)
            Dim continua As Boolean = True
            'Verifico che non siano presenti già altre date approvate per ogni edificio in questione
            par.cmd.CommandText = "SELECT SISCOM_MI.GETDIFFDATAORA(" & par.AggiustaData(txtDataFine.SelectedDate) & "," & par.AggiustaData(txtDataInizio.SelectedDate) & ") FROM DUAL"
            Dim numeroDate As Integer = CInt(par.cmd.ExecuteScalar)
            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQueryCronoprogramma)
            For Each riga As Data.DataRow In dt.Rows
                For i As Integer = 1 To numeroDate + 1
                    If par.IfNull(riga.Item("DATA " & i), "") <> "" Then
                        'par.cmd.CommandText = "SELECT TO_DATE(DATA,'yyyyMMdd') as data " _
                        '            & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT  " _
                        '            & " WHERE ID_EDIFICIO =  " _
                        '            & " (SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = '" & riga.Item("CODICE EDIFICIO") & "')  " _
                        '            & " AND ID_PROGRAMMA_ATTIVITA IN  " _
                        '            & " (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID_STATO = 1 and attivita_cronoprogramma = " & idAttivitaCronoprogramma.Value & ")  " _
                        '            & " AND FL_CANCELLATO = 0 " _
                        '            & " AND ID_PROGRAMMA_ATTIVITA <>  " & idPiano _
                        '            & " AND INDICE = " & i
                        'Dim dtDate As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                        'If dtDate.Rows.Count > 0 Then
                        '    For Each data As Data.DataRow In dtDate.Rows
                        '        If CDate(data.Item("DATA")) = CDate(riga.Item("DATA " & i)) Then
                        '            continua = False
                        '        End If
                        '    Next
                        'End If
                        par.cmd.CommandText = "SELECT count(*) " _
                                    & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT  " _
                                    & " WHERE ID_EDIFICIO =  " _
                                    & " (SELECT ID FROM SISCOM_MI.EDIFICI WHERE COD_EDIFICIO = '" & riga.Item("CODICE EDIFICIO") & "')  " _
                                    & " AND ID_PROGRAMMA_ATTIVITA IN  " _
                                    & " (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID_STATO = 1 and attivita_cronoprogramma = " & idAttivitaCronoprogramma.Value & ")  " _
                                    & " AND FL_CANCELLATO = 0 " _
                                    & " AND ID_PROGRAMMA_ATTIVITA <>  " & idPiano _
                                    & " AND data = " & par.AggiustaData(riga.Item("DATA " & i)) _
                                    & " AND ID_EDIFICIO IN (SELECT ID_EDIFICIO FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = (SELECT ID_GRUPPO FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID = PROGRAMMA_ATTIVITA_DETT.ID_PROGRAMMA_ATTIVITA))"

                        Dim numero As Integer = par.cmd.ExecuteScalar
                        If numero > 0 Then
                            continua = False
                        End If
                    End If
                Next
            Next
            If continua Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.PROGRAMMA_ATTIVITA SET ID_STATO=1,DATA_ULTIMA_APPROVAZIONE='" & Format(Now, "yyyyMMdd") & "' WHERE ID=" & idPiano
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "UPDATE SISCOM_MI.PROGRAMMA_ATTIVITA_DETT SET FL_APPROVATO=1 WHERE ID_PROGRAMMA_ATTIVITA = " & idPiano
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PROGRAMMA_aTTIVITA(ID_PROGRAMMA_aTTIVITA,ID_OPERATORE,DATA_ORA,COD_eVENTO,MOTIVAZIONE)" _
                            & "VALUES(" & idPiano & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F311','Approvazione del piano per il programma delle attività')"
                par.cmd.ExecuteNonQuery()


                par.cmd.CommandText = "  SELECT segnalazioni.id " _
                                    & "  FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI  " _
                                    & "  WHERE nvl(combinazione_tipologie.id_tipo_segnalazione(+),0) = nvl(segnalazioni.id_tipo_segnalazione,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                    & "  And COMBINAZIONE_TIPOLOGIE.ID_TIPO_MANUTENZIONE = 1 " _
                                    & "  And SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA Is NULL " _
                                    & "  And SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, 1, 8) >= '20180802' "
                Dim dtAggiornamento As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                If dtAggiornamento.Rows.Count > 0 Then
                    par.cmd.CommandText = "SELECT DISTINCT ID_EDIFICIO " _
                        & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                        & " WHERE ID_PROGRAMMA_ATTIVITA = " & idPiano
                    Dim dtEdifici As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                    If dtEdifici.Rows.Count > 0 Then
                        For Each riga As Data.DataRow In dtEdifici.Rows
                            Dim dataP As String = ""
                            Dim dataProgrammataIntervento As String = "NULL"
                            Dim lett As Oracle.DataAccess.Client.OracleDataReader
                            par.cmd.CommandText = "SELECT GETDATA(MIN(DATA)) " _
                                                & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                                                & " WHERE ID_EDIFICIO= " & par.IfNull(riga.Item("ID_EDIFICIO"), "-1") _
                                                & " AND DATA>'" & Format(Now, "yyyyMMdd") & "' " _
                                                & " AND ID_PROGRAMMA_ATTIVITA = " & idPiano _
                                                & " AND FL_CANCELLATO = 0 " _
                                                & " ORDER BY 1 "
                            lett = par.cmd.ExecuteReader
                            If lett.Read Then
                                If IsDate(par.IfNull(lett(0), 0)) Then
                                    dataP = par.IfNull(lett(0), "")
                                End If
                            End If
                            lett.Close()
                            If IsDate(dataP) Then
                                dataProgrammataIntervento = par.AggiustaData(dataP)
                            End If

                            Dim dataPrec As String = ""
                            Dim dataProgrammataUltimoIntervento As String = "NULL"
                            par.cmd.CommandText = "SELECT GETDATA(MAX(DATA)) " _
                                                & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA_DETT " _
                                                & " WHERE ID_EDIFICIO=" & par.IfNull(riga.Item("ID_EDIFICIO"), "-1") _
                                                & " And DATA<='" & Format(Now, "yyyyMMdd") & "' " _
                                                & " AND ID_PROGRAMMA_ATTIVITA = " & idPiano _
                                                & " AND FL_CANCELLATO = 0 " _
                                                & " ORDER BY 1 "
                            lett = par.cmd.ExecuteReader
                            If lett.Read Then
                                If IsDate(par.IfNull(lett(0), 0)) Then
                                    dataPrec = par.IfNull(lett(0), 0)
                                End If
                            End If
                            lett.Close()
                            If IsDate(dataPrec) Then
                                dataProgrammataUltimoIntervento = par.AggiustaData(dataPrec)
                            End If
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_PROGRAMMA_ATTIVITA = " & idPiano & "," _
                                    & " DATA_PROGRAMMATA_INT = " & dataProgrammataIntervento & "," _
                                    & " DATA_PROGRAMMATA_INT2 = " & dataProgrammataUltimoIntervento & " " _
                                    & " WHERE ID IN " _
                                    & "  ( " _
                                    & "  SELECT segnalazioni.id " _
                                    & "  FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI  " _
                                    & "  WHERE nvl(combinazione_tipologie.id_tipo_segnalazione(+),0) = nvl(segnalazioni.id_tipo_segnalazione,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                    & "  And nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                    & "  And COMBINAZIONE_TIPOLOGIE.ID_TIPO_MANUTENZIONE = 1 " _
                                    & "  And SEGNALAZIONI.ID_PROGRAMMA_ATTIVITA Is NULL " _
                                    & "  AND SEGNALAZIONI.ID_EDIFICIO = " & par.IfNull(riga.Item("ID_EDIFICIO"), "-1") _
                                    & "  And SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, 1, 8) >= '20180802' " _
                                    & " and SEGNALAZIONI.ID_TIPO_SEGN_LIVELLO_1 = " & par.IfEmpty(idAttivitaCronoprogramma.Value, -1) _
                                    & "  ) "
                            par.cmd.ExecuteNonQuery()
                        Next
                    End If
                End If
                VisualizzaAlert("Operazione effettuata!", 1)
            Else
                RadWindowManager1.RadAlert("Sono presenti edifici già approvati!<br />Per maggiori dettagli premere il pulsante <strong>Mostra edifici approvati</strong> ", 350, 150, "Attenzione", Nothing, Nothing)
            End If
            connData.chiudi(True)
            caricaPiano()

            DataGridCronoprogramma.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Piano - bntApprova_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
