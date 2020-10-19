
Partial Class GESTIONE_CONTATTI_AgendaGestioneContattiGiorn
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                GiornoSelezionato.Value = Right(Request.QueryString("giorno"), 2)
                MeseSelezionato.Value = Mid(Request.QueryString("giorno"), 5, 2)
                AnnoSelezionato.Value = Left(Request.QueryString("giorno"), 4)
                If Not IsNothing(Request.QueryString("PROV")) Then
                    provenienza.Value = Request.QueryString("PROV")
                End If
                If Not IsNothing(Request.QueryString("IDS")) Then
                    idSegnalazione.Value = Request.QueryString("IDS")
                End If
                If provenienza.Value = "SEG" And idSegnalazione.Value <> "-1" And Not IsNothing(Request.QueryString("IDF")) Then
                    If IsNumeric(Request.QueryString("IDF")) Then
                        idStrutturaPredefinita.Value = Request.QueryString("IDF")
                    End If
                End If
                CaricaSediTerritoriali()
                lblData.Text = "Data Odierna: " & Format(Now, "dd/MM/yyyy")
                settaIndirizzi()
                SettaCalendari(True)
                SettaMese()
                CalcolaQuattroSlotDisponibili()
            End If
            If provenienza.Value = "SEG" Then
                btnIndietroSegnalazione.Visible = True
            Else
                btnIndietroSegnalazione.Visible = False
            End If



        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 1
            'If CType(Me.Master.FindControl("operatoreFiliale"), HiddenField).Value = "1" Then
            '    'Se l'operatore è un operatore di filiale può operare solo all'interno della sua sede territoriale di appartenenza
            '    If Not IsNothing(cmbStruttura.Items.FindByValue(Session.Item("ID_STRUTTURA"))) Then
            '        If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '            cmbStruttura.SelectedValue = Session.Item("ID_STRUTTURA")
            '            cmbStruttura.Enabled = False
            '        End If
            '    Else
            '        If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "0" Then
            '            par.modalDialogMessage("Agenda e Segnalazioni", "Non è possibile caricare la sede territoriale di appartenenza. Contattare l\'amministratore di sistema.", Page, "info", "Home.aspx")
            '            Exit Sub
            '        Else
            '            cmbStruttura.Enabled = True
            '        End If
            '    End If
            'End If
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            Else
                If CType(Me.Master.FindControl("FLGC_SL"), HiddenField).Value = "1" Then
                    solaLettura()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaSediTerritoriali()
        Try
            connData.apri()
            Dim idFilialeAmministrativa As Integer = 0
            Dim idFiliale As Integer = 0
            If IsNumeric(idSegnalazione.Value) Then
                par.cmd.CommandText = " SELECT (CASE  " _
                    & " WHEN ID_UNITA IS NOT NULL THEN (CASE WHEN (SELECT (CASE WHEN NVL(ID_FILIALE_AMMINISTRATIVA,0)=0 THEN NULL ELSE ID_FILIALE_AMMINISTRATIVA END) FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE SEGNALAZIONI.ID_UNITA=UNITA_IMMOBILIARI.ID) IS NOT NULL THEN (SELECT ID_FILIALE_AMMINISTRATIVA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE SEGNALAZIONI.ID_UNITA=UNITA_IMMOBILIARI.ID) ELSE (SELECT (CASE WHEN ID_FILIALE_AMM IS NOT NULL THEN ID_FILIALE_AMM ELSE ID_FILIALE END) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=(SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=(SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=ID_UNITA)))END)" _
                    & " WHEN ID_EDIFICIO IS NOT NULL THEN (SELECT (CASE WHEN ID_FILIALE_AMM IS NOT NULL THEN ID_FILIALE_AMM ELSE ID_FILIALE END) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=(SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=SEGNALAZIONI.ID_EDIFICIO)) " _
                    & " WHEN ID_COMPLESSO IS NOT NULL THEN (SELECT (CASE WHEN ID_FILIALE_AMM IS NOT NULL THEN ID_FILIALE_AMM ELSE ID_FILIALE END) FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=SEGNALAZIONI.ID_COMPLESSO) " _
                    & " ELSE NULL END) " _
                    & " FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID = " & idSegnalazione.Value
                idFilialeAmministrativa = par.IfNull(par.cmd.ExecuteScalar, 0)
                par.cmd.CommandText = " SELECT (CASE  " _
                    & " WHEN ID_UNITA IS NOT NULL THEN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=(SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=(SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID=ID_UNITA))) " _
                    & " WHEN ID_EDIFICIO IS NOT NULL THEN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=(SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO))" _
                    & " WHEN ID_COMPLESSO IS NOT NULL THEN (SELECT ID_FILIALE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID=SEGNALAZIONI.ID_COMPLESSO) " _
                    & " ELSE NULL END) " _
                    & " FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID = " & idSegnalazione.Value
                idFiliale = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            If idFilialeAmministrativa > 0 Then
                idStrutturaPredefinita.Value = idFilialeAmministrativa
            End If
            par.caricaComboBox("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 ORDER BY NOME", cmbStruttura, "ID", "NOME", False)
            connData.chiudi()
            If Not IsNothing(cmbStruttura.Items.FindByValue(Request.QueryString("STRUTT"))) Then
                cmbStruttura.SelectedValue = Request.QueryString("STRUTT")
            ElseIf Not IsNothing(cmbStruttura.Items.FindByValue(idStrutturaPredefinita.Value)) Then
                cmbStruttura.SelectedValue = idStrutturaPredefinita.Value
            End If
            'If idFilialeAmministrativa > idFiliale Then
            '    cmbStruttura.Enabled = False
            'End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - CaricaSediTerritoriali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaCalendari(Optional Load As Boolean = False)
        Try
            Dim Mese As Date = GiornoSelezionato.Value & "/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value
            If Load = False Then
                Mese = CalendarioMese.VisibleDate
            End If
            Dim MesePrecedente As Date = Mese.AddMonths(-1)
            Dim MeseSuccessivo As Date = Mese.AddMonths(1)
            CalendarioPrec.TodaysDate = MesePrecedente
            CalendarioMese.TodaysDate = Mese
            CalendarioNext.TodaysDate = MeseSuccessivo
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - SettaCalendari - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaMese()
        Try
            Dim mpContentPlaceHolder As ContentPlaceHolder
            mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            PulisciBloccoDate()
            Dim dataIniziale As Date = "#" & GiornoSelezionato.Value & "/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value & "#"
            Dim PrimoGiorno As String = dataIniziale.ToString("dddd")
            Dim GiorniMese As Integer = 1
            Dim PrimoInserimentoGiorno As Boolean = True
            Dim PrimoNumeroGiorno As Integer = 1
            connData.apri(False)
            For i As Integer = 1 To GiorniMese Step 1
                If PrimoInserimentoGiorno Then
                    lblGiorno.Text = PrimoGiorno
                    'Select Case PrimoGiorno
                    '    Case "lunedì"
                    '        BloccoData1.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 1
                    '    Case "martedì"
                    '        BloccoData2.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 2
                    '    Case "mercoledì"
                    '        BloccoData3.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 3
                    '    Case "giovedì"
                    '        BloccoData4.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 4
                    '    Case "venerdì"
                    '        BloccoData5.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 5
                    '    Case "sabato"
                    '        BloccoData6.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 6
                    '    Case "domenica"
                    '        BloccoData7.Text = "&nbsp;" & i
                    '        PrimoNumeroGiorno = 7
                    'End Select
                    PrimoInserimentoGiorno = False
                Else
                    CType(mpContentPlaceHolder.FindControl("BloccoData" & PrimoNumeroGiorno), Label).Text = "&nbsp;" & i
                End If
                If TrovaAppuntamentoGiornaliero(i, PrimoNumeroGiorno) = False Then
                    Exit Sub
                End If
                PrimoNumeroGiorno = PrimoNumeroGiorno + 1
            Next
            connData.chiudi()
            lblMese.Text = par.AggiustaTestoUcFirst(Format(CDate(GiornoSelezionato.Value & "/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value), "dd MMMM yyyy"))
            SettaBlocchiVuoti()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - SettaMese - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function TrovaAppuntamentoGiornaliero(ByVal Giorno As Integer, ByVal Appuntamento As Integer) As Boolean
        Try
            Dim mpContentPlaceHolder As ContentPlaceHolder
            mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            TrovaAppuntamentoGiornaliero = False
            Dim connessioneAperta As Boolean = True
            If connData.Connessione.State = Data.ConnectionState.Closed Then
                connessioneAperta = False
                connData.apri(False)
            End If
            Dim condizioneStruttura As String = ""
            If cmbStruttura.SelectedValue <> "-1" Then
                condizioneStruttura = " AND ID_STRUTTURA=" & cmbStruttura.SelectedValue
            End If
            'par.cmd.CommandText = "SELECT ID_STRUTTURA,ID_SPORTELLO, APPUNTAMENTI_SPORTELLI.DESCRIZIONE, COUNT (*) AS CONTEGGIO " _
            '    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
            '    & " WHERE SISCOM_MI.APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID " _
            '    & " AND DATA_APPUNTAMENTO = '" & Format(CInt(AnnoSelezionato.Value), "0000") & Format(CInt(MeseSelezionato.Value), "00") _
            '    & Format(CInt(Giorno), "00") & "' " _
            '    & condizioneStruttura _
            '    & " /*AND ID_OPERATORE_ELIMINAZIONE IS NULL*/ " _
            '    & " AND APPUNTAMENTI_SPORTELLI.ID_FILIALE = TAB_FILIALI.ID " _
            '    & " AND NVL(ID_OPERATORE_ELIMINAZIONE,0)=0 " _
            '    & " AND APPUNTAMENTI_SPORTELLI.ID=ID_SPORTELLO " _
            '    & " GROUP BY ID_STRUTTURA,ID_SPORTELLO,APPUNTAMENTI_SPORTELLI.DESCRIZIONE ORDER BY 3"
            ''Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'da.Dispose()
            'If connessioneAperta = False Then
            '    connData.chiudi(False)
            'End If
            'Dim idFiliale As Integer = 0
            'Dim conteggio As Integer = 0
            'Dim filiale As String = ""
            'CType(mpContentPlaceHolder.FindControl("Label" & Appuntamento), Label).Text = ""
            'Dim contatore As Integer = 0
            'Dim stileTabella As String = " cellpadding=""0"" cellspacing=""0"" style=""font-family:Arial;font-size:7pt;color:gray;font-weight:bold;width:95%"""
            'Dim tdStyle As String = " style=""cursor:pointer;"""
            'For Each elemTab As Data.DataRow In dt.Rows
            '    contatore += 1
            '    idFiliale = par.IfNull(elemTab.Item("ID_STRUTTURA"), 0)
            '    conteggio = par.IfNull(elemTab.Item("CONTEGGIO"), 0)
            '    filiale = par.IfNull(elemTab.Item("DESCRIZIONE"), 0)
            '    If contatore <= 6 Then
            '        CType(mpContentPlaceHolder.FindControl("Label" & Appuntamento), Label).Text &= "<table " & stileTabella & "><tr><td " & tdStyle & " onclick=""javascript:validNavigation=true;DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & idFiliale & "," & idSegnalazione.Value & ");"">" & Left(filiale, 12) & "</td><td " & tdStyle & " class=""notifica2"" onclick=""javascript:validNavigation=true;DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & idFiliale & "," & idSegnalazione.Value & ");"">" & conteggio & "</td></tr></table>"
            '    End If
            'Next


            Dim tabellaOrari As String = "<table cellpadding=""1"" cellspacing=""1"" style=""width:100%;font-size:7pt"">"
            par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE = " & cmbStruttura.SelectedValue & " ORDER BY INDICE ASC"
            Dim daSP As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtSP As New Data.DataTable
            daSP.Fill(dtSP)
            daSP.Dispose()
            If dtSP.Rows.Count > 0 Then
                Dim array(dtSP.Rows.Count - 1) As String
                Dim i As Integer = 0
                For Each rigaSP As Data.DataRow In dtSP.Rows
                    array(i) = rigaSP.Item("DESCRIZIONE").ToString
                    i += 1
                Next
                tabellaOrari &= "<tr><td style=""width:4%;""></td><td style=""width:16%;"">" & array(0) & "</td><td style=""width:16%;"">" & array(1) & "</td><td style=""width:16%;"">" & array(2) & "</td><td style=""width:16%;"">" & array(3) & "</td><tr>"
            Else
            tabellaOrari &= "<tr><td style=""width:4%;""></td><td style=""width:16%;"">SPORTELLO 1</td><td style=""width:16%;"">SPORTELLO 2</td><td style=""width:16%;"">SPORTELLO 3</td><td style=""width:16%;"">SPORTELLO 4</td><tr>"
            End If
            Dim dat As String = Format(CInt(AnnoSelezionato.Value), "0000") & Format(CInt(MeseSelezionato.Value), "00") & Format(CInt(GiornoSelezionato.Value), "00")

            par.cmd.CommandText = " SELECT ORARIO," _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 1 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1  " _
                & "     THEN ('style=""width:16%;cursor:pointer;background-color:#B20000""') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 1))=0 THEN 'style=""width:16%;cursor:pointer;background-color:Green""' ELSE 'style=""width:16%;cursor:pointer;background-color:#DDDDDD""' END) END) AS SP1, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 2 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('style=""width:16%;cursor:pointer;background-color:#B20000""') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 2))=0 THEN 'style=""width:16%;cursor:pointer;background-color:Green""' ELSE 'style=""width:16%;cursor:pointer;background-color:#DDDDDD""' END) END) AS SP2, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 3 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('style=""width:16%;cursor:pointer;background-color:#B20000""') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 3))=0 THEN 'style=""width:16%;cursor:pointer;background-color:Green""' ELSE 'style=""width:16%;cursor:pointer;background-color:#DDDDDD""' END) END) AS SP3, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 4 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('style=""width:16%;cursor:pointer;background-color:#B20000""') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 4))=0 THEN 'style=""width:16%;cursor:pointer;background-color:Green""' ELSE 'style=""width:16%;cursor:pointer;background-color:#DDDDDD""' END) END) AS SP4, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 5 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('style=""width:16%;cursor:pointer;background-color:#B20000""') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 5))=0 THEN 'style=""width:16%;cursor:pointer;background-color:Green""' ELSE 'style=""width:16%;cursor:pointer;background-color:#DDDDDD""' END) END) AS SP5, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 6 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('style=""width:16%;cursor:pointer;background-color:#B20000""') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 6))=0 THEN 'style=""width:16%;cursor:pointer;background-color:Green""' ELSE 'style=""width:16%;cursor:pointer;background-color:#DDDDDD""' END) END) AS SP6, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 1 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('SPORTELLO CHIUSO') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 1))=0 THEN '' ELSE (SELECT '<table><tr><td>'||id_Segnalazione||'</td></tr><tr><td>'||cognome||' - '||nome||'</td></tr><tr><td>'||telefono||'  '||cellulare||'</td></tr><tr><td>'||note||'</td></tr></table>' FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 1)) END) END) AS CONT_SP1, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 2 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('SPORTELLO CHIUSO') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 2))=0 THEN '' ELSE (SELECT '<table><tr><td>'||id_Segnalazione||'</td></tr><tr><td>'||cognome||' - '||nome||'</td></tr><tr><td>'||telefono||'  '||cellulare||'</td></tr><tr><td>'||note||'</td></tr></table>' FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 2)) END) END) AS CONT_SP2, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 3 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('SPORTELLO CHIUSO') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 3))=0 THEN '' ELSE (SELECT '<table><tr><td>'||id_Segnalazione||'</td></tr><tr><td>'||cognome||' - '||nome||'</td></tr><tr><td>'||telefono||'  '||cellulare||'</td></tr><tr><td>'||note||'</td></tr></table>' FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 3)) END) END) AS CONT_SP3, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 4 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('SPORTELLO CHIUSO') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 4))=0 THEN '' ELSE (SELECT '<table><tr><td>'||id_Segnalazione||'</td></tr><tr><td>'||cognome||' - '||nome||'</td></tr><tr><td>'||telefono||'  '||cellulare||'</td></tr><tr><td>'||note||'</td></tr></table>' FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 4)) END) END) AS CONT_SP4, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 5 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('SPORTELLO CHIUSO') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 5))=0 THEN '' ELSE (SELECT '<table><tr><td>'||id_Segnalazione||'</td></tr><tr><td>'||cognome||' - '||nome||'</td></tr><tr><td>'||telefono||'  '||cellulare||'</td></tr><tr><td>'||note||'</td></tr></table>' FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 5)) END) END) AS CONT_SP5, " _
                & " (CASE WHEN (SELECT COUNT (ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_ORARIO = APPUNTAMENTI_ORARI.ID " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE = 6 AND ID_FILIALE = " & cmbStruttura.SelectedValue & ") " _
                & "     AND ID_FILIALE = " & cmbStruttura.SelectedValue & " AND GIORNO = " & dat & ") = 1 " _
                & "     THEN ('SPORTELLO CHIUSO') ELSE (CASE WHEN (SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 6))=0 THEN '' ELSE (SELECT '<table><tr><td>'||id_Segnalazione||'</td></tr><tr><td>'||cognome||' - '||nome||'</td></tr><tr><td>'||telefono||'  '||cellulare||'</td></tr><tr><td>'||note||'</td></tr></table>' FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA = " & cmbStruttura.SelectedValue & " " _
                & "     AND DATA_APPUNTAMENTO = " & dat & " AND ID_ORARIO = APPUNTAMENTI_ORARI.ID AND DATA_ELIMINAZIONE IS NULL " _
                & "     AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE = " & cmbStruttura.SelectedValue & " " _
                & "     AND INDICE = 6)) END) END) AS CONT_SP6 " _
                & " FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY APPUNTAMENTI_ORARI.INDICE"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()

            Dim stringaLinkIni = "onclick=""javascript:validNavigation=true;DettagliAppuntamenti('" & Format(CInt(GiornoSelezionato.Value), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & cmbStruttura.SelectedValue & "," & idSegnalazione.Value
            Dim stringaLinkFin = ");"""

            If par.IsFestivo(CDate(Format(CInt(GiornoSelezionato.Value), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000")), True) Then
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(0).Item("ORARIO") & "</td><td " & stringaLinkIni & ",1,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(0).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",1,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(0).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",1,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(0).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",1,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(0).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(1).Item("ORARIO") & "</td><td " & stringaLinkIni & ",2,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(1).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",2,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(1).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",2,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(1).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",2,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(1).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(2).Item("ORARIO") & "</td><td " & stringaLinkIni & ",3,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(2).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",3,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(2).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",3,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(2).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",3,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(2).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(3).Item("ORARIO") & "</td><td " & stringaLinkIni & ",4,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(3).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",4,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(3).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",4,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(3).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",4,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(3).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(4).Item("ORARIO") & "</td><td " & stringaLinkIni & ",5,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(4).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",5,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(4).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",5,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(4).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",5,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(4).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(5).Item("ORARIO") & "</td><td " & stringaLinkIni & ",6,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(5).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",6,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(5).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",6,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(5).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",6,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(5).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(6).Item("ORARIO") & "</td><td " & stringaLinkIni & ",7,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(6).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",7,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(6).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",7,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(6).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",7,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(6).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(7).Item("ORARIO") & "</td><td " & stringaLinkIni & ",8,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(7).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",8,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(7).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",8,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(7).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",8,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(7).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(8).Item("ORARIO") & "</td><td " & stringaLinkIni & ",9,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(8).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",9,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(8).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",9,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(8).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",9,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(8).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(9).Item("ORARIO") & "</td><td " & stringaLinkIni & ",10,1" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(9).Item("CONT_SP1") & "</td><td " & stringaLinkIni & ",10,2" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(9).Item("CONT_SP2") & "</td><td " & stringaLinkIni & ",10,3" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(9).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",10,4" & stringaLinkFin & " style=""width:16%;cursor:pointer;background-color:#B20000"">" & dt.Rows(9).Item("CONT_SP4") & "</td><tr>"
            Else
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(0).Item("ORARIO") & "</td><td " & stringaLinkIni & ",1,1" & stringaLinkFin & " " & dt.Rows(0).Item("SP1") & ">" & dt.Rows(0).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",1,2" & stringaLinkFin & " " & dt.Rows(0).Item("SP2") & ">" & dt.Rows(0).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",1,3" & stringaLinkFin & " " & dt.Rows(0).Item("SP3") & ">" & dt.Rows(0).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",1,4" & stringaLinkFin & " " & dt.Rows(0).Item("SP4") & ">" & dt.Rows(0).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(1).Item("ORARIO") & "</td><td " & stringaLinkIni & ",2,1" & stringaLinkFin & " " & dt.Rows(1).Item("SP1") & ">" & dt.Rows(1).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",2,2" & stringaLinkFin & " " & dt.Rows(1).Item("SP2") & ">" & dt.Rows(1).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",2,3" & stringaLinkFin & " " & dt.Rows(1).Item("SP3") & ">" & dt.Rows(1).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",2,4" & stringaLinkFin & " " & dt.Rows(1).Item("SP4") & ">" & dt.Rows(1).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(2).Item("ORARIO") & "</td><td " & stringaLinkIni & ",3,1" & stringaLinkFin & " " & dt.Rows(2).Item("SP1") & ">" & dt.Rows(2).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",3,2" & stringaLinkFin & " " & dt.Rows(2).Item("SP2") & ">" & dt.Rows(2).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",3,3" & stringaLinkFin & " " & dt.Rows(2).Item("SP3") & ">" & dt.Rows(2).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",3,4" & stringaLinkFin & " " & dt.Rows(2).Item("SP4") & ">" & dt.Rows(2).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(3).Item("ORARIO") & "</td><td " & stringaLinkIni & ",4,1" & stringaLinkFin & " " & dt.Rows(3).Item("SP1") & ">" & dt.Rows(3).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",4,2" & stringaLinkFin & " " & dt.Rows(3).Item("SP2") & ">" & dt.Rows(3).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",4,3" & stringaLinkFin & " " & dt.Rows(3).Item("SP3") & ">" & dt.Rows(3).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",4,4" & stringaLinkFin & " " & dt.Rows(3).Item("SP4") & ">" & dt.Rows(3).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(4).Item("ORARIO") & "</td><td " & stringaLinkIni & ",5,1" & stringaLinkFin & " " & dt.Rows(4).Item("SP1") & ">" & dt.Rows(4).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",5,2" & stringaLinkFin & " " & dt.Rows(4).Item("SP2") & ">" & dt.Rows(4).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",5,3" & stringaLinkFin & " " & dt.Rows(4).Item("SP3") & ">" & dt.Rows(4).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",5,4" & stringaLinkFin & " " & dt.Rows(4).Item("SP4") & ">" & dt.Rows(4).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(5).Item("ORARIO") & "</td><td " & stringaLinkIni & ",6,1" & stringaLinkFin & " " & dt.Rows(5).Item("SP1") & ">" & dt.Rows(5).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",6,2" & stringaLinkFin & " " & dt.Rows(5).Item("SP2") & ">" & dt.Rows(5).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",6,3" & stringaLinkFin & " " & dt.Rows(5).Item("SP3") & ">" & dt.Rows(5).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",6,4" & stringaLinkFin & " " & dt.Rows(5).Item("SP4") & ">" & dt.Rows(5).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(6).Item("ORARIO") & "</td><td " & stringaLinkIni & ",7,1" & stringaLinkFin & " " & dt.Rows(6).Item("SP1") & ">" & dt.Rows(6).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",7,2" & stringaLinkFin & " " & dt.Rows(6).Item("SP2") & ">" & dt.Rows(6).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",7,3" & stringaLinkFin & " " & dt.Rows(6).Item("SP3") & ">" & dt.Rows(6).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",7,4" & stringaLinkFin & " " & dt.Rows(6).Item("SP4") & ">" & dt.Rows(6).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(7).Item("ORARIO") & "</td><td " & stringaLinkIni & ",8,1" & stringaLinkFin & " " & dt.Rows(7).Item("SP1") & ">" & dt.Rows(7).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",8,2" & stringaLinkFin & " " & dt.Rows(7).Item("SP2") & ">" & dt.Rows(7).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",8,3" & stringaLinkFin & " " & dt.Rows(7).Item("SP3") & ">" & dt.Rows(7).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",8,4" & stringaLinkFin & " " & dt.Rows(7).Item("SP4") & ">" & dt.Rows(7).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(8).Item("ORARIO") & "</td><td " & stringaLinkIni & ",9,1" & stringaLinkFin & " " & dt.Rows(8).Item("SP1") & ">" & dt.Rows(8).Item("CONT_SP1") & "</td><td  " & stringaLinkIni & ",9,2" & stringaLinkFin & " " & dt.Rows(8).Item("SP2") & ">" & dt.Rows(8).Item("CONT_SP2") & "</td><td  " & stringaLinkIni & ",9,3" & stringaLinkFin & " " & dt.Rows(8).Item("SP3") & ">" & dt.Rows(8).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",9,4" & stringaLinkFin & " " & dt.Rows(8).Item("SP4") & ">" & dt.Rows(8).Item("CONT_SP4") & "</td><tr>"
                tabellaOrari &= "<tr><td style=""height:70px;"">" & dt.Rows(9).Item("ORARIO") & "</td><td " & stringaLinkIni & ",10,1" & stringaLinkFin & "  " & dt.Rows(9).Item("SP1") & ">" & dt.Rows(9).Item("CONT_SP1") & "</td><td " & stringaLinkIni & ",10,2" & stringaLinkFin & " " & dt.Rows(9).Item("SP2") & ">" & dt.Rows(9).Item("CONT_SP2") & "</td><td " & stringaLinkIni & ",10,3" & stringaLinkFin & " " & dt.Rows(9).Item("SP3") & ">" & dt.Rows(9).Item("CONT_SP3") & "</td><td " & stringaLinkIni & ",10,4" & stringaLinkFin & " " & dt.Rows(9).Item("SP4") & ">" & dt.Rows(9).Item("CONT_SP4") & "</td><tr>"
            End If

            'Dim ris As Integer = 0
            'Dim dis As Integer = 0
            'For iii As Integer = 1 To 10
            '    par.cmd.CommandText = "select orario from siscom_mi.appuntamenti_orari where indice=" & iii
            '    tabellaOrari &= "<tr><td style=""width:80px;wrap:false;"">" & par.IfNull(par.cmd.ExecuteScalar.ToString.Replace(" ", ""), "") & "</td>"
            '    For jjj As Integer = 1 To 6
            '        dis = 0
            '        par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS " _
            '                        & " WHERE ID_ORARIO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_ORARI WHERE INDICE=" & iii & ")" _
            '                        & " AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE INDICE=" & jjj _
            '                        & " AND ID_FILIALE=" & cmbStruttura.SelectedValue & ") " _
            '                        & " AND ID_FILIALE = " & cmbStruttura.SelectedValue _
            '                        & " AND GIORNO = " & Format(CInt(AnnoSelezionato.Value), "0000") & Format(CInt(MeseSelezionato.Value), "00") & Format(CInt(Giorno), "00")
            '        dis = par.IfNull(par.cmd.ExecuteScalar, 0)
            '        If dis = 1 Then
            '            tabellaOrari &= "<td style=""width:20px;background-color:#B20000"">0</td>"
            '        Else
            '            ris = 0
            '            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_STRUTTURA=" & cmbStruttura.SelectedValue & " AND DATA_APPUNTAMENTO=" _
            '                & Format(CInt(AnnoSelezionato.Value), "0000") & Format(CInt(MeseSelezionato.Value), "00") & Format(CInt(Giorno), "00") _
            '                & " AND ID_ORARIO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_ORARI WHERE APPUNTAMENTI_ORARI.INDICE=" & iii & ")" _
            '                & " AND ID_SPORTELLO IN (SELECT ID FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE APPUNTAMENTI_SPORTELLI.ID_FILIALE=" & cmbStruttura.SelectedValue & " AND INDICE =" & jjj & ")" _
            '                & " AND DATA_ELIMINAZIONE IS NULL"
            '            ris = par.IfNull(par.cmd.ExecuteScalar, 0)
            '            If ris = 0 Then
            '                tabellaOrari &= "<td onclick=""alert('Slot libero.');"" style=""cursor:pointer;width:20px;background-color:Green"">0</td>"
            '            Else
            '                tabellaOrari &= "<td style=""width:20px;background-color:#DDDDDD""></td>"
            '            End If
            '        End If
            '    Next
            '    tabellaOrari &= "</tr>"
            'Next



            'Dim lettoreRis As Oracle.DataAccess.Client.OracleDataReader
            'For iii As Integer = 1 To 8
            '    ris = 0
            '    par.cmd.CommandText = "SELECT NVL(COUNT(ID),0) FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI_CHIUS WHERE ID_FILIALE=" & cmbStruttura.SelectedValue & " AND GIORNO=" _
            '        & "'" & Format(CInt(AnnoSelezionato.Value), "0000") & Format(CInt(MeseSelezionato.Value), "00") & Format(CInt(Giorno), "00") & "' " _
            '        & " AND ID_ORARIO=" & iii
            '    lettoreRis = par.cmd.ExecuteReader
            '    If lettoreRis.Read Then
            '        ris = par.IfNull(lettoreRis(0), 0)
            '    End If
            '    lettoreRis.Close()
            '    If ris = 0 Then
            '        Select Case iii
            '            Case 1
            '                tabellaOrari &= "<tr><td style=""background-color:green"">09:00-09:30</td></tr>"
            '            Case 2
            '                tabellaOrari &= "<tr><td style=""background-color:green"">09:30-10:00</td></tr>"
            '            Case 3
            '                tabellaOrari &= "<tr><td style=""background-color:green"">10:00-10:30</td></tr>"
            '            Case 4
            '                tabellaOrari &= "<tr><td style=""background-color:green"">10:30-11:00</td></tr>"
            '            Case 5
            '                tabellaOrari &= "<tr><td style=""background-color:green"">11:00-11:30</td></tr>"
            '            Case 6
            '                tabellaOrari &= "<tr><td style=""background-color:green"">14:00-14:30</td></tr>"
            '            Case 7
            '                tabellaOrari &= "<tr><td style=""background-color:green"">14:30-15:00</td></tr>"
            '            Case 8
            '                tabellaOrari &= "<tr><td style=""background-color:green"">15:00-15:30</td></tr>"
            '        End Select
            '    Else
            '        Select Case iii
            '            Case 1
            '                tabellaOrari &= "<tr><td style=""background-color:red"">09:00-09:30</td></tr>"
            '            Case 2
            '                tabellaOrari &= "<tr><td style=""background-color:red"">09:30-10:00</td></tr>"
            '            Case 3
            '                tabellaOrari &= "<tr><td style=""background-color:red"">10:00-10:30</td></tr>"
            '            Case 4
            '                tabellaOrari &= "<tr><td style=""background-color:red"">10:30-11:00</td></tr>"
            '            Case 5
            '                tabellaOrari &= "<tr><td style=""background-color:red"">11:00-11:30</td></tr>"
            '            Case 6
            '                tabellaOrari &= "<tr><td style=""background-color:red"">14:00-14:30</td></tr>"
            '            Case 7
            '                tabellaOrari &= "<tr><td style=""background-color:red"">14:30-15:00</td></tr>"
            '            Case 8
            '                tabellaOrari &= "<tr><td style=""background-color:red"">15:00-15:30</td></tr>"
            '        End Select
            '    End If
            'Next
            tabellaOrari &= "</table>"



            CType(mpContentPlaceHolder.FindControl("LabelOrari" & Appuntamento), Label).Text = tabellaOrari
            'CType(mpContentPlaceHolder.FindControl("Dettaglio" & Appuntamento), Label).Text = "<img onclick=""javascript:validNavigation=true;DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & cmbStruttura.SelectedValue & "," & idSegnalazione.Value & ");"" src=""../NuoveImm/Aggiungi.png"" style=""cursor:pointer"" width=""12"" height=""12"" />"
            TrovaAppuntamentoGiornaliero = True
        Catch ex As Exception
            TrovaAppuntamentoGiornaliero = False
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - TrovaAppuntamentoGiornaliero - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function
    Private Sub PulisciBloccoDate()
        Try
            Dim mpContentPlaceHolder As ContentPlaceHolder
            mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            For i As Integer = 1 To 1 Step 1
                CType(mpContentPlaceHolder.FindControl("BloccoData" & i), Label).Text = ""
                'CType(mpContentPlaceHolder.FindControl("Label" & i), Label).Text = ""
                CType(mpContentPlaceHolder.FindControl("LabelOrari" & i), Label).Text = ""
                'CType(mpContentPlaceHolder.FindControl("Dettaglio" & i), Label).Text = ""
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - PulisciBloccoDate - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaBlocchiVuoti()
        Try
            Dim mpContentPlaceHolder As ContentPlaceHolder
            mpContentPlaceHolder = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            For i As Integer = 1 To 1 Step 1
                If String.IsNullOrEmpty(CType(mpContentPlaceHolder.FindControl("BloccoData" & i), Label).Text) Then
                    CType(mpContentPlaceHolder.FindControl("BloccoData" & i), Label).Text = "&nbsp;"
                    CType(mpContentPlaceHolder.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#CCCCCC"
                Else
                    Dim dataGiorno As Date = Format(CInt(CType(mpContentPlaceHolder.FindControl("BloccoData" & i), Label).Text.ToString.Replace("&nbsp;", "")), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000")
                    If Not par.IsFestivo(dataGiorno, True) Then
                        If Format(Now, "dd/MM/yyyy") = dataGiorno Then
                            CType(mpContentPlaceHolder.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#FFFFCC"
                            'ElseIf Format(CInt(CType(mpContentPlaceHolder.FindControl("BloccoData" & i), Label).Text.ToString.Replace("&nbsp;", "")), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000") = Format(CInt(GiornoSelezionato.Value), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000") Then
                            '    CType(mpContentPlaceHolder.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#FF9933"
                        Else
                            CType(mpContentPlaceHolder.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#FFFFFF"
                        End If
                    Else
                        'CType(mpContentPlaceHolder.FindControl("BloccoData" & i), Label).Text = "&nbsp;"
                        CType(mpContentPlaceHolder.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#B20000"
                        'CType(mpContentPlaceHolder.FindControl("Label" & i), Label).Text = "&nbsp;"
                        CType(mpContentPlaceHolder.FindControl("LabelOrari" & i), Label).Text = "&nbsp;"
                        'CType(mpContentPlaceHolder.FindControl("Dettaglio" & i), Label).Text = "&nbsp;"
                    End If
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - SettaBlocchiVuoti - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioMese_VisibleMonthChanged(sender As Object, e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles CalendarioMese.VisibleMonthChanged
        Try
            SettaCalendari()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - CalendarioMese_VisibleMonthChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioPrec_SelectionChanged(sender As Object, e As System.EventArgs) Handles CalendarioPrec.SelectionChanged
        Try
            Dim DataSelezionata As Date = CalendarioPrec.SelectedDate
            GiornoSelezionato.Value = DataSelezionata.ToString("dd")
            MeseSelezionato.Value = DataSelezionata.ToString("MM")
            AnnoSelezionato.Value = DataSelezionata.ToString("yyyy")
            Response.Redirect("AgendaGestioneContattiGiorn.aspx?giorno=" & AnnoSelezionato.Value & MeseSelezionato.Value & GiornoSelezionato.Value, False)
            'SettaMese()
            'CalcolaQuattroSlotDisponibili()
            'CalendarioPrec.SelectedDate = Nothing
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - CalendarioPrec_SelectionChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioMese_SelectionChanged(sender As Object, e As System.EventArgs) Handles CalendarioMese.SelectionChanged
        Try
            Dim DataSelezionata As Date = CalendarioMese.SelectedDate
            GiornoSelezionato.Value = DataSelezionata.ToString("dd")
            MeseSelezionato.Value = DataSelezionata.ToString("MM")
            AnnoSelezionato.Value = DataSelezionata.ToString("yyyy")
            Response.Redirect("AgendaGestioneContattiGiorn.aspx?giorno=" & AnnoSelezionato.Value & MeseSelezionato.Value & GiornoSelezionato.Value, False)
            'SettaMese()
            'CalcolaQuattroSlotDisponibili()
            'CalendarioMese.SelectedDate = Nothing
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - CalendarioMese_SelectionChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioNext_SelectionChanged(sender As Object, e As System.EventArgs) Handles CalendarioNext.SelectionChanged
        Try
            Dim DataSelezionata As Date = CalendarioNext.SelectedDate
            GiornoSelezionato.Value = DataSelezionata.ToString("dd")
            MeseSelezionato.Value = DataSelezionata.ToString("MM")
            AnnoSelezionato.Value = DataSelezionata.ToString("yyyy")
            Response.Redirect("AgendaGestioneContattiGiorn.aspx?giorno=" & AnnoSelezionato.Value & MeseSelezionato.Value & GiornoSelezionato.Value, False)
            'SettaMese()
            'CalcolaQuattroSlotDisponibili()
            'CalendarioNext.SelectedDate = Nothing
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - CalendarioNext_SelectionChanged - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnOggi_Click(sender As Object, e As System.EventArgs) Handles btnOggi.Click
        Try
            GiornoSelezionato.Value = Date.Today.Day.ToString.PadLeft(2, "0")
            MeseSelezionato.Value = Date.Today.Month.ToString.PadLeft(2, "0")
            AnnoSelezionato.Value = Date.Today.Year.ToString.PadLeft(4, "0")
            Response.Redirect("AgendaGestioneContattiGiorn.aspx?giorno=" & AnnoSelezionato.Value & MeseSelezionato.Value & GiornoSelezionato.Value & "&STRUTT=" & cmbStruttura.SelectedValue, False)
            'SettaMese()
            'CalcolaQuattroSlotDisponibili()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - btnOggi_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnSettimana_Click(sender As Object, e As System.EventArgs) Handles btnSettimana.Click
        Try
            Response.Redirect("AgendaGestioneContattiSett.aspx?STRUTT=" & cmbStruttura.SelectedValue, False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - btnSettimana_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnMese_Click(sender As Object, e As System.EventArgs) Handles btnMese.Click
        Try
            Response.Redirect("AgendaGestioneContatti.aspx?STRUTT=" & cmbStruttura.SelectedValue, False)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - btnMese_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        Try
            Dim DataIndietro As Date = CDate(GiornoSelezionato.Value & "/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value).AddDays(-1)
            GiornoSelezionato.Value = DataIndietro.ToString("dd")
            MeseSelezionato.Value = DataIndietro.ToString("MM")
            AnnoSelezionato.Value = DataIndietro.ToString("yyyy")
            SettaCalendari(True)
            SettaMese()
            CalcolaQuattroSlotDisponibili()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - btnIndietro_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAvanti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAvanti.Click
        Try
            Dim DataAvanti As Date = CDate(GiornoSelezionato.Value & "/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value).AddDays(1)
            GiornoSelezionato.Value = DataAvanti.ToString("dd")
            MeseSelezionato.Value = DataAvanti.ToString("MM")
            AnnoSelezionato.Value = DataAvanti.ToString("yyyy")
            SettaCalendari(True)
            SettaMese()
            CalcolaQuattroSlotDisponibili()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - btnAvanti_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub cmbStruttura_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        SettaMese()
    End Sub
    Protected Sub btnAggiorna_Click(sender As Object, e As System.EventArgs) Handles btnAggiorna.Click
        SettaMese()
        CalcolaQuattroSlotDisponibili()
    End Sub
    Private Sub CalcolaQuattroSlotDisponibili()
        Try
            connData.apri()
            'dataOdierna
            Dim dataodierna As Date = Now

            Dim checkGiornoSlot1 As Boolean = False
            Dim checkGiornoSlot2 As Boolean = False
            Dim checkGiornoSlot3 As Boolean = False
            Dim checkGiornoSlot4 As Boolean = False

            Dim dataMinimaSlot1 As Date = dataodierna
            Dim dataMinimaSlot2 As Date = dataodierna
            Dim dataMinimaSlot3 As Date = dataodierna
            Dim dataMinimaSlot4 As Date = dataodierna

            Dim idFilialeSlot3 As String = ""
            Dim idFilialeSlot4 As String = ""

            Dim DataSlot1 As String = ""
            Dim FilialeSlot1 As String = ""
            Dim OrarioFilialeSlot1 As String = ""
            Dim sportelloFilialeSlot1 As String = ""
            Dim DataSlot2 As String = ""
            Dim FilialeSlot2 As String = ""
            Dim OrarioFilialeSlot2 As String = ""
            Dim sportelloFilialeSlot2 As String = ""
            Dim DataSlot3 As String = ""
            Dim FilialeSlot3 As String = ""
            Dim OrarioFilialeSlot3 As String = ""
            Dim sportelloFilialeSlot3 As String = ""
            Dim DataSlot4 As String = ""
            Dim FilialeSlot4 As String = ""
            Dim OrarioFilialeSlot4 As String = ""
            Dim sportelloFilialeSlot4 As String = ""


            'SLOT 1
            '************************************************************************************************************************
            While checkGiornoSlot1 = False
                dataMinimaSlot1 = dataMinimaSlot1.AddDays(1)
                If Not par.IsFestivo(dataMinimaSlot1) And Not par.IsWeekEnd(dataMinimaSlot1, True) Then
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    Dim cont As Integer = 0
                    Dim contSportello As Integer = 0
                    For Each orario As Data.DataRow In dt.Rows
                        cont = 0
                        par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                            & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                            & " AND DATA_ELIMINAZIONE IS NULL " _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID "
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            cont = par.IfNull(lettore(0), 0)
                        Else
                            cont = 0
                        End If
                        lettore.Close()
                        If cont < 4 Then
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=1"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot1 = 1
                                OrarioFilialeSlot1 = orario.Item("ORARIO")
                                checkGiornoSlot1 = True
                                Exit While
                            End If
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=2"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot1 = 2
                                OrarioFilialeSlot1 = orario.Item("ORARIO")
                                checkGiornoSlot1 = True
                                Exit While
                            End If
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=3"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot1 = 3
                                OrarioFilialeSlot1 = orario.Item("ORARIO")
                                checkGiornoSlot1 = True
                                Exit While
                            End If
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=4"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot1 = 4
                                OrarioFilialeSlot1 = orario.Item("ORARIO")
                                checkGiornoSlot1 = True
                                Exit While
                            End If
                            contSportello = 0
                            'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                            '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                            '    & " AND DATA_ELIMINAZIONE IS NULL " _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                            '    & " AND INDICE=5"
                            'lettore = par.cmd.ExecuteReader
                            'If lettore.Read Then
                            '    contSportello = par.IfNull(lettore(0), 0)
                            'Else
                            '    contSportello = 0
                            'End If
                            'lettore.Close()
                            'If contSportello = 0 Then
                            '    sportelloFilialeSlot1 = 5
                            '    OrarioFilialeSlot1 = orario.Item("ORARIO")
                            '    checkGiornoSlot1 = True
                            '    Exit While
                            'End If
                            'contSportello = 0
                            'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                            '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot1, "yyyyMMdd") & "'" _
                            '    & " AND DATA_ELIMINAZIONE IS NULL " _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                            '    & " AND INDICE=6"
                            'lettore = par.cmd.ExecuteReader
                            'If lettore.Read Then
                            '    contSportello = par.IfNull(lettore(0), 0)
                            'Else
                            '    contSportello = 0
                            'End If
                            'lettore.Close()
                            'If contSportello = 0 Then
                            '    sportelloFilialeSlot1 = 6
                            '    OrarioFilialeSlot1 = orario.Item("ORARIO")
                            '    checkGiornoSlot1 = True
                            '    Exit While
                            'End If
                        End If
                    Next
                End If
            End While

            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & cmbStruttura.SelectedValue
            Dim lettoreF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreF.Read Then
                FilialeSlot1 = par.IfNull(lettoreF("NOME"), "FILIALE")
            End If
            lettoreF.Close()
            '************************************************************************************************************************

            'SLOT 2
            '************************************************************************************************************************
            While checkGiornoSlot2 = False
                dataMinimaSlot2 = dataMinimaSlot2.AddDays(1)
                If Not par.IsFestivo(dataMinimaSlot2) And Not par.IsWeekEnd(dataMinimaSlot2, True) Then
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    Dim cont As Integer = 0
                    Dim contSportello As Integer = 0
                    For Each orario As Data.DataRow In dt.Rows
                        cont = 0
                        par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                            & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                            & " AND DATA_ELIMINAZIONE IS NULL " _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID "
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            cont = par.IfNull(lettore(0), 0)
                        Else
                            cont = 0
                        End If
                        lettore.Close()
                        If cont < 4 Then
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=1"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot2 = 1
                                OrarioFilialeSlot2 = orario.Item("ORARIO")
                                If sportelloFilialeSlot1 = sportelloFilialeSlot2 And OrarioFilialeSlot1 = OrarioFilialeSlot2 Then
                                    checkGiornoSlot2 = False
                                Else
                                    checkGiornoSlot2 = True
                                    Exit While
                                End If
                            End If
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=2"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot2 = 2
                                OrarioFilialeSlot2 = orario.Item("ORARIO")
                                If sportelloFilialeSlot1 = sportelloFilialeSlot2 And OrarioFilialeSlot1 = OrarioFilialeSlot2 Then
                                    checkGiornoSlot2 = False
                                Else
                                    checkGiornoSlot2 = True
                                    Exit While
                                End If
                            End If
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=3"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot2 = 3
                                OrarioFilialeSlot2 = orario.Item("ORARIO")
                                If sportelloFilialeSlot1 = sportelloFilialeSlot2 And OrarioFilialeSlot1 = OrarioFilialeSlot2 Then
                                    checkGiornoSlot2 = False
                                Else
                                    checkGiornoSlot2 = True
                                    Exit While
                                End If
                            End If
                            contSportello = 0
                            par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                                & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                                & " AND DATA_ELIMINAZIONE IS NULL " _
                                & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                & " AND INDICE=4"
                            lettore = par.cmd.ExecuteReader
                            If lettore.Read Then
                                contSportello = par.IfNull(lettore(0), 0)
                            Else
                                contSportello = 0
                            End If
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot2 = 4
                                OrarioFilialeSlot2 = orario.Item("ORARIO")
                                If sportelloFilialeSlot1 = sportelloFilialeSlot2 And OrarioFilialeSlot1 = OrarioFilialeSlot2 Then
                                    checkGiornoSlot2 = False
                                Else
                                    checkGiornoSlot2 = True
                                    Exit While
                                End If
                            End If
                            contSportello = 0
                            'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                            '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                            '    & " AND DATA_ELIMINAZIONE IS NULL " _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                            '    & " AND INDICE=5"
                            'lettore = par.cmd.ExecuteReader
                            'If lettore.Read Then
                            '    contSportello = par.IfNull(lettore(0), 0)
                            'Else
                            '    contSportello = 0
                            'End If
                            'lettore.Close()
                            'If contSportello = 0 Then
                            '    sportelloFilialeSlot2 = 5
                            '    OrarioFilialeSlot2 = orario.Item("ORARIO")
                            '    If sportelloFilialeSlot1 = sportelloFilialeSlot2 And OrarioFilialeSlot1 = OrarioFilialeSlot2 Then
                            '        checkGiornoSlot2 = False
                            '    Else
                            '        checkGiornoSlot2 = True
                            '        Exit While
                            '    End If
                            'End If
                            'contSportello = 0
                            'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbStruttura.SelectedValue _
                            '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot2, "yyyyMMdd") & "'" _
                            '    & " AND DATA_ELIMINAZIONE IS NULL " _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                            '    & " AND INDICE=6"
                            'lettore = par.cmd.ExecuteReader
                            'If lettore.Read Then
                            '    contSportello = par.IfNull(lettore(0), 0)
                            'Else
                            '    contSportello = 0
                            'End If
                            'lettore.Close()
                            'If contSportello = 0 Then
                            '    sportelloFilialeSlot2 = 6
                            '    OrarioFilialeSlot2 = orario.Item("ORARIO")
                            '    If sportelloFilialeSlot1 = sportelloFilialeSlot2 And OrarioFilialeSlot1 = OrarioFilialeSlot2 Then
                            '        checkGiornoSlot2 = False
                            '    Else
                            '        checkGiornoSlot2 = True
                            '        Exit While
                            '    End If
                            'End If
                        End If
                    Next
                End If
            End While

            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & cmbStruttura.SelectedValue
            lettoreF = par.cmd.ExecuteReader
            If lettoreF.Read Then
                FilialeSlot2 = par.IfNull(lettoreF("NOME"), "FILIALE")
            End If
            lettoreF.Close()
            '************************************************************************************************************************

            'SLOT 3
            '************************************************************************************************************************
            While checkGiornoSlot3 = False
                dataMinimaSlot3 = dataMinimaSlot3.AddDays(1)
                If Not par.IsFestivo(dataMinimaSlot3) And Not par.IsWeekEnd(dataMinimaSlot3, True) Then
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    Dim cont As Integer = 0
                    Dim contSportello As Integer = 0
                    For Each orario As Data.DataRow In dt.Rows
                        cont = 0
                        par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA<>" & cmbStruttura.SelectedValue _
                            & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                            & " AND DATA_ELIMINAZIONE IS NULL " _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID "
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            cont = par.IfNull(lettore(0), 0)
                        Else
                            cont = 0
                        End If
                        lettore.Close()
                        If cont < 12 Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID<>" & cmbStruttura.SelectedValue
                            Dim LettoreFiliali As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim checkExit As Boolean = False
                            While LettoreFiliali.Read
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=1"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot3 = 1
                                    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                    checkGiornoSlot3 = True
                                    checkExit = True
                                    Exit While
                                End If
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=2"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot3 = 2
                                    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                    checkGiornoSlot3 = True
                                    checkExit = True
                                    Exit While
                                End If
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=3"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot3 = 3
                                    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                    checkGiornoSlot3 = True
                                    checkExit = True
                                    Exit While
                                End If
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=4"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot3 = 4
                                    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                    checkGiornoSlot3 = True
                                    checkExit = True
                                    Exit While
                                End If
                                contSportello = 0
                                'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                                '    & " AND DATA_ELIMINAZIONE IS NULL " _
                                '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                '    & " AND INDICE=5"
                                'lettore = par.cmd.ExecuteReader
                                'If lettore.Read Then
                                '    contSportello = par.IfNull(lettore(0), 0)
                                'Else
                                '    contSportello = 0
                                'End If
                                'lettore.Close()
                                'If contSportello = 0 Then
                                '    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                '    sportelloFilialeSlot3 = 5
                                '    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                '    checkGiornoSlot3 = True
                                '    checkExit = True
                                '    Exit While
                                'End If
                                'contSportello = 0
                                'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot3, "yyyyMMdd") & "'" _
                                '    & " AND DATA_ELIMINAZIONE IS NULL " _
                                '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                '    & " AND INDICE=6"
                                'lettore = par.cmd.ExecuteReader
                                'If lettore.Read Then
                                '    contSportello = par.IfNull(lettore(0), 0)
                                'Else
                                '    contSportello = 0
                                'End If
                                'lettore.Close()
                                'If contSportello = 0 Then
                                '    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                '    sportelloFilialeSlot3 = 6
                                '    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                '    checkGiornoSlot3 = True
                                '    checkExit = True
                                '    Exit While
                                'End If
                            End While
                            LettoreFiliali.Close()
                            If checkExit = True Then
                                Exit While
                            End If
                        End If
                    Next
                End If
            End While

            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & idFilialeSlot3
            lettoreF = par.cmd.ExecuteReader
            If lettoreF.Read Then
                FilialeSlot3 = par.IfNull(lettoreF("NOME"), "FILIALE")
            End If
            lettoreF.Close()
            '************************************************************************************************************************

            'SLOT 4
            '************************************************************************************************************************
            While checkGiornoSlot4 = False
                dataMinimaSlot4 = dataMinimaSlot4.AddDays(1)
                If Not par.IsFestivo(dataMinimaSlot4) And Not par.IsWeekEnd(dataMinimaSlot4, True) Then
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ID"
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dt As New Data.DataTable
                    da.Fill(dt)
                    da.Dispose()
                    Dim cont As Integer = 0
                    Dim contSportello As Integer = 0
                    For Each orario As Data.DataRow In dt.Rows
                        cont = 0
                        par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA<>" & cmbStruttura.SelectedValue _
                            & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                            & " AND DATA_ELIMINAZIONE IS NULL " _
                            & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID "
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            cont = par.IfNull(lettore(0), 0)
                        Else
                            cont = 0
                        End If
                        lettore.Close()
                        If cont < 12 Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0 AND ID<>" & cmbStruttura.SelectedValue
                            Dim LettoreFiliali As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            Dim checkExit As Boolean = False
                            While LettoreFiliali.Read
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=1"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot4 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot4 = 1
                                    OrarioFilialeSlot4 = orario.Item("ORARIO")
                                    If sportelloFilialeSlot3 = sportelloFilialeSlot4 And OrarioFilialeSlot3 = OrarioFilialeSlot4 And idFilialeSlot3 = idFilialeSlot4 Then
                                        checkGiornoSlot4 = False
                                    Else
                                        checkGiornoSlot4 = True
                                        checkExit = True
                                        Exit While
                                    End If
                                End If
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=2"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot4 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot4 = 2
                                    OrarioFilialeSlot4 = orario.Item("ORARIO")
                                    If sportelloFilialeSlot3 = sportelloFilialeSlot4 And OrarioFilialeSlot3 = OrarioFilialeSlot4 And idFilialeSlot3 = idFilialeSlot4 Then
                                        checkGiornoSlot4 = False
                                    Else
                                        checkGiornoSlot4 = True
                                        checkExit = True
                                        Exit While
                                    End If
                                End If
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=3"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot4 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot4 = 3
                                    OrarioFilialeSlot4 = orario.Item("ORARIO")
                                    If sportelloFilialeSlot3 = sportelloFilialeSlot4 And OrarioFilialeSlot3 = OrarioFilialeSlot4 And idFilialeSlot3 = idFilialeSlot4 Then
                                        checkGiornoSlot4 = False
                                    Else
                                        checkGiornoSlot4 = True
                                        checkExit = True
                                        Exit While
                                    End If
                                End If
                                contSportello = 0
                                par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                                    & " AND DATA_ELIMINAZIONE IS NULL " _
                                    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                    & " AND INDICE=4"
                                lettore = par.cmd.ExecuteReader
                                If lettore.Read Then
                                    contSportello = par.IfNull(lettore(0), 0)
                                Else
                                    contSportello = 0
                                End If
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot4 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot4 = 4
                                    OrarioFilialeSlot4 = orario.Item("ORARIO")
                                    If sportelloFilialeSlot3 = sportelloFilialeSlot4 And OrarioFilialeSlot3 = OrarioFilialeSlot4 And idFilialeSlot3 = idFilialeSlot4 Then
                                        checkGiornoSlot4 = False
                                    Else
                                        checkGiornoSlot4 = True
                                        checkExit = True
                                        Exit While
                                    End If
                                End If
                                contSportello = 0
                                'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                                '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                                '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                                '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                                '    & " AND DATA_ELIMINAZIONE IS NULL " _
                                '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                                '    & " AND INDICE=5"
                                'lettore = par.cmd.ExecuteReader
                                'If lettore.Read Then
                                '    contSportello = par.IfNull(lettore(0), 0)
                                'Else
                                '    contSportello = 0
                                'End If
                                'lettore.Close()
                                'If contSportello = 0 Then
                                '    idFilialeSlot4 = par.IfNull(LettoreFiliali("id"), "")
                                '    sportelloFilialeSlot4 = 5
                                '    OrarioFilialeSlot4 = orario.Item("ORARIO")
                                '    If sportelloFilialeSlot3 = sportelloFilialeSlot4 And OrarioFilialeSlot3 = OrarioFilialeSlot4 And idFilialeSlot3 = idFilialeSlot4 Then
                                '        checkGiornoSlot4 = False
                                '    Else
                                '        checkGiornoSlot4 = True
                                '        checkExit = True
                                '        Exit While
                                '    End If
                                'End If
                            End While
                            LettoreFiliali.Close()
                            If checkExit = True Then
                                Exit While
                            End If
                            contSportello = 0
                            'par.cmd.CommandText = "SELECT NVL(COUNT(*),0) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                            '    & " WHERE ID_ORARIO=" & orario.Item("ID") _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & par.IfNull(LettoreFiliali("ID"), "") _
                            '    & " AND DATA_APPUNTAMENTO='" & Format(dataMinimaSlot4, "yyyyMMdd") & "'" _
                            '    & " AND DATA_ELIMINAZIONE IS NULL " _
                            '    & " AND APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID " _
                            '    & " AND INDICE=6"
                            'lettore = par.cmd.ExecuteReader
                            'If lettore.Read Then
                            '    contSportello = par.IfNull(lettore(0), 0)
                            'Else
                            '    contSportello = 0
                            'End If
                            'lettore.Close()
                            'If contSportello = 0 Then
                            '    idFilialeSlot4 = par.IfNull(LettoreFiliali("id"), "")
                            '    sportelloFilialeSlot4 = 6
                            '    OrarioFilialeSlot4 = orario.Item("ORARIO")
                            '    If sportelloFilialeSlot3 = sportelloFilialeSlot4 And OrarioFilialeSlot3 = OrarioFilialeSlot4 And idFilialeSlot3 = idFilialeSlot4 Then
                            '        checkGiornoSlot4 = False
                            '    Else
                            '        checkGiornoSlot4 = True
                            '        checkExit = True
                            '        Exit While
                            '    End If
                            'End If
                        End If
                    Next
                End If
            End While

            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & idFilialeSlot4
            lettoreF = par.cmd.ExecuteReader
            If lettoreF.Read Then
                FilialeSlot4 = par.IfNull(lettoreF("NOME"), "FILIALE")
            End If
            lettoreF.Close()
            '************************************************************************************************************************

            sportelloFilialeSlot1 = "Sportello " & sportelloFilialeSlot1
            sportelloFilialeSlot2 = "Sportello " & sportelloFilialeSlot2
            sportelloFilialeSlot3 = "Sportello " & sportelloFilialeSlot3
            sportelloFilialeSlot4 = "Sportello " & sportelloFilialeSlot4

            lblFilialeCompetenza.Text = "<table style=""border:1px solid gray;background-color:#ffffff"">" _
                & "<tr><td style=""background-color:gray;color:white"">" & FilialeSlot1 & "</td><td style=""background-color:gray;color:white"">" & FilialeSlot2 & "</td><td style=""background-color:gray;color:white"">" & FilialeSlot3 & "</td><td style=""background-color:gray;color:white"">" & FilialeSlot4 & "</td></tr>" _
                & "<tr><td>" & Format(dataMinimaSlot1, "dd/MM/yyyy") & "</td><td>" & Format(dataMinimaSlot2, "dd/MM/yyyy") & "</td><td>" & Format(dataMinimaSlot3, "dd/MM/yyyy") & "</td><td>" & Format(dataMinimaSlot4, "dd/MM/yyyy") & "</td></tr>" _
                & "<tr><td>" & OrarioFilialeSlot1 & "</td><td>" & OrarioFilialeSlot2 & "</td><td>" & OrarioFilialeSlot3 & "</td><td>" & OrarioFilialeSlot4 & "</td></tr>" _
                & "<tr><td>" & sportelloFilialeSlot1 & "</td><td>" & sportelloFilialeSlot2 & "</td><td>" & sportelloFilialeSlot3 & "</td><td>" & sportelloFilialeSlot4 & "</td></tr>" _
                & "</table>"

            'lblAltraFiliale.Text = OrarioFiliale1
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - CalcolaQuattroSlotDisponibili - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub settaIndirizzi()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT NOME, DESCRIZIONE||' '||CIVICO||','||LOCALITA AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE ID_TIPO_ST=0 AND TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim tabella As String = "<table style=""font-size:7pt;font-family:Arial"">"
            While lettore.Read
                tabella &= "<tr><td>" & par.IfNull(lettore("nome"), "") & "</td><td>-</td><td>" & par.IfNull(lettore("indirizzo"), "") & "</td></tr>"
            End While
            lettore.Close()
            connData.chiudi()
            tabella &= "</table>"
            lblIndirizziFiliali.Text = tabella
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - settaIndirizzi - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub ButtonExportAgendaAppuntamenti_Click(sender As Object, e As System.EventArgs) Handles ButtonExportAgendaAppuntamenti.Click
        Try
            connData.apri()
            Dim condizioneStruttura As String = ""
            'If Session.Item("ID_STRUTTURA") <> "-1" Then
            '    condizioneStruttura = " AND ID_sTRUTTURA=" & cmbStruttura.SelectedValue
            'End If
            par.cmd.CommandText = " SELECT  ID_sEGNALAZIONE AS ""N°"", " _
                & " TO_CHAR(TO_dATE(DATA_APPUNTAMENTO,'YYYYMMDD'),'DD/MM/YYYY') AS DATA_APPUNTAMENTO, " _
                & " APPUNTAMENTI_SPORTELLI.DESCRIZIONE, " _
                & " APPUNTAMENTI_ORARI.ORARIO, " _
                & " TAB_FILIALI.NOME AS FILIALE, " _
                & " CASE WHEN DATA_INSERIMENTO IS NOT NULL THEN  " _
                & " TO_CHAR(TO_dATE(SUBSTR(DATA_INSERIMENTO,1,8),'YYYYMMDD'),'DD/MM/YYYY')||'-'||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2)||':'||SUBSTR(DATA_INSERIMENTO,13,2)  " _
                & " ELSE NULL END " _
                & " AS DATA_INSERIMENTO, " _
                & " OPERATORI.OPERATORE, " _
                 & " (CASE " _
                & " WHEN (SELECT SEGNALAZIONI.ID_UNITA " _
                & " FROM SISCOM_MI.SEGNALAZIONI " _
                & " WHERE SEGNALAZIONI.ID = ID_sEGNALAZIONE) " _
                & " IS NOT NULL " _
                & " THEN " _
                & " (SELECT MAX (COD_CONTRATTO) " _
                & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                & " WHERE ID IN " _
                & " (SELECT ID_CONTRATTO " _
                & " FROM SISCOM_MI. " _
                & " UNITA_CONTRATTUALE " _
                & " WHERE UNITA_CONTRATTUALE. " _
                & " ID_UNITA = " _
                & " (SELECT SEGNALAZIONI.ID_UNITA " _
                & " FROM SISCOM_MI. " _
                & " SEGNALAZIONI " _
                & " WHERE SEGNALAZIONI.ID = ID_sEGNALAZIONE)) " _
                & " AND (SELECT SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                & " 1, " _
                & " 8) " _
                & " FROM SISCOM_MI.SEGNALAZIONI " _
                & " WHERE SEGNALAZIONI.ID = ID_sEGNALAZIONE) BETWEEN NVL ( " _
                & " RAPPORTI_UTENZA. " _
                & " DATA_DECORRENZA, " _
                & " '10000000') " _
                & " AND NVL ( " _
                & " RAPPORTI_UTENZA. " _
                & " DATA_RICONSEGNA, " _
                & " '30000000')) " _
                & " ELSE " _
                & " NULL " _
                & "         END) " _
                & " AS ""CODICE CONTRATTO"", " _
                & " APPUNTAMENTI_CALL_CENTER.NOME,APPUNTAMENTI_CALL_CENTER.COGNOME,APPUNTAMENTI_CALL_CENTER.TELEFONO AS TELEFONO_1,APPUNTAMENTI_CALL_CENTER.CELLULARE AS TELEFONO_2,APPUNTAMENTI_CALL_CENTER.EMAIL,APPUNTAMENTI_CALL_CENTER.NOTE " _
                & " ,CASE WHEN DATA_ELIMINAZIONE IS NOT NULL THEN  " _
                & " TO_CHAR(TO_dATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'YYYYMMDD'),'DD/MM/YYYY')||'-'||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2)||':'||SUBSTR(DATA_ELIMINAZIONE,13,2)  " _
                & " ELSE NULL END " _
                & " AS DATA_ELIMINAZIONE " _
                & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.TAB_FILIALI,SEPA.OPERATORI,SISCOM_MI.APPUNTAMENTI_SPORTELLI,SISCOM_MI.APPUNTAMENTI_ORARI " _
                & " WHERE APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=TAB_FILIALI.ID(+) " _
                & " AND APPUNTAMENTI_CALL_CENTER.ID_OPERATORE=OPERATORI.ID(+) " _
                & " AND APPUNTAMENTI_cALL_cENTER.ID_SPORTELLO=APPUNTAMENTI_SPORTELLI.ID(+) " _
                & " AND APPUNTAMENTI_CALL_CENTER.ID_ORARIO=APPUNTAMENTI_ORARI.ID(+) " _
                & condizioneStruttura _
                & " order by 1 desc "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportAppuntamenti", "ExportAppuntamenti", dt, True, , True)
                If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Page, Page.GetType, "nav", "validNavigation=true;", True)
                    par.modalDialogMessage("Agenda", "Esportazione eseguita correttamente.", Page, "successo", "..\/FileTemp\/" & nomeFile)
                    WriteEvent("F239", "")
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    par.modalDialogMessage("Agenda", "Si è verificato un errore durante l\'esportazione. Riprovare!", Page, "errore", , )
                    'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
                End If
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda - ButtonExportAgendaAppuntamenti_Click - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                connData.apri()
                connOpNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (NULL," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
                & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"
            par.cmd.ExecuteNonQuery()
            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
                connData.chiudi()
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda Agenda e Segnalazioni - WriteEvent - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietroSegnalazione_Click(sender As Object, e As System.EventArgs) Handles btnIndietroSegnalazione.Click
        Response.Redirect("Segnalazione.aspx?IDS=" & idSegnalazione.Value)
    End Sub
    Private Sub solaLettura()
        Try
            Dim mpContentPlaceHolderContenuto As ContentPlaceHolder
            mpContentPlaceHolderContenuto = CType(Master.FindControl("CPContenuto"), ContentPlaceHolder)
            Dim CTRL As Control = Nothing
            For Each CTRL In mpContentPlaceHolderContenuto.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadioButtonList Then
                    DirectCast(CTRL, RadioButtonList).Enabled = False
                ElseIf TypeOf CTRL Is Button Then
                    DirectCast(CTRL, Button).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is CheckBoxList Then
                    DirectCast(CTRL, CheckBoxList).Enabled = False
                End If
            Next
            If CType(Me.Master.FindControl("supervisore"), HiddenField).Value = "1" Then
                cmbStruttura.Enabled = True
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Agenda Agenda e Segnalazioni - solaLettura - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Redirect("Home.aspx")
    End Sub

End Class

