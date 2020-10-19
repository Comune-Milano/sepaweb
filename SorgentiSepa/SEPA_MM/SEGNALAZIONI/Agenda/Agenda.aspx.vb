
Partial Class SEGNALAZIONI_Agenda_Agenda
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("../../AccessoNegato.htm", False)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            If Not SettaFiliali() Then
                Response.Write("<script>alert('Per accedere alla gestione degli appuntamenti dell\'agenda\nè necessario loggarsi con sede territoriale amministrativa!');self.close();</script>")
                Exit Sub
            End If
            lblData.Text = "Data Odierna: " & Format(Now, "dd/MM/yyyy")
            settaIndirizzi()
            SettaCalendari(True)
            SettaMese()
            CalcolaQuattroSlotDisponibili()
        End If
        
    End Sub
    Private Function SettaFiliali() As Boolean
        Try
            connData.apri()
            If Session.Item("ID_STRUTTURA") = "-1" Then
                par.caricaComboBox("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbStruttura, "ID", "NOME", False)
                If cmbStruttura.Items.Count >= "1" Then
                    cmbStruttura.Enabled = True
                    connData.chiudi()
                    Return True
                Else
                    connData.chiudi()
                    Return False
                    'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - SettaFiliali - " & "")
                    'Response.Redirect("../../pagina_home.aspx", True)
                End If
            Else
                par.caricaComboBox("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID=" & Session.Item("ID_STRUTTURA") & " AND TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbStruttura, "ID", "NOME", False)
                If cmbStruttura.Items.Count = "1" Then
                    cmbStruttura.Enabled = False
                    connData.chiudi()
                    Return True
                Else
                    connData.chiudi()
                    Return False
                    'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - SettaFiliali - " & "")
                    'Response.Redirect("../../pagina_home.aspx", True)
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - SettaFiliali - " & ex.Message)
            Return False
        End Try
    End Function
    Private Sub SettaCalendari(Optional Load As Boolean = False)
        Try
            MeseSelezionato.Value = Date.Today.Month
            AnnoSelezionato.Value = Date.Today.Year
            Dim Mese As Date = Date.Today
            If Load = False Then
                Mese = CalendarioMese.VisibleDate
            End If
            Dim MesePrecedente As Date = Mese.AddMonths(-1)
            Dim MeseSuccessivo As Date = Mese.AddMonths(1)
            CalendarioPrec.TodaysDate = MesePrecedente
            CalendarioNext.TodaysDate = MeseSuccessivo
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - SettaCalendari - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaMese()
        Try
            PulisciBloccoDate()
            Dim dataIniziale As Date = "#01/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value & "#"
            Dim PrimoGiorno As String = dataIniziale.ToString("dddd")
            Dim GiorniMese As Integer = CInt(DateTime.DaysInMonth(AnnoSelezionato.Value, MeseSelezionato.Value))
            Dim PrimoInserimentoGiorno As Boolean = True
            Dim PrimoNumeroGiorno As Integer = 1
            For i As Integer = 1 To GiorniMese Step 1
                If PrimoInserimentoGiorno Then
                    Select Case PrimoGiorno
                        Case "lunedì"
                            BloccoData1.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 1
                        Case "martedì"
                            BloccoData2.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 2
                        Case "mercoledì"
                            BloccoData3.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 3
                        Case "giovedì"
                            BloccoData4.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 4
                        Case "venerdì"
                            BloccoData5.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 5
                        Case "sabato"
                            BloccoData6.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 6
                        Case "domenica"
                            BloccoData7.Text = "&nbsp;" & i
                            PrimoNumeroGiorno = 7
                    End Select
                    PrimoInserimentoGiorno = False
                Else
                    CType(Page.FindControl("BloccoData" & PrimoNumeroGiorno), Label).Text = "&nbsp;" & i
                End If
                If TrovaAppuntamentoGiornaliero(i, PrimoNumeroGiorno) = False Then
                    Exit Sub
                End If
                PrimoNumeroGiorno = PrimoNumeroGiorno + 1
            Next
            lblMese.Text = par.AggiustaTestoUcFirst(Format(CDate(GiornoSelezionato.Value & "/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value), "MMMM yyyy"))
            SettaBlocchiVuoti()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - SettaMese - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Function TrovaAppuntamentoGiornaliero(ByVal Giorno As Integer, ByVal Appuntamento As Integer) As Boolean
        Try
            TrovaAppuntamentoGiornaliero = False
            connData.apri(False)
            Dim condizioneStruttura As String = ""
            If cmbStruttura.SelectedValue <> "-1" Then
                condizioneStruttura = " AND ID_STRUTTURA=" & cmbStruttura.SelectedValue
            End If
            par.cmd.CommandText = "SELECT ID_STRUTTURA,ID_SPORTELLO, APPUNTAMENTI_SPORTELLI.DESCRIZIONE, COUNT (*) AS CONTEGGIO " _
                & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                & " WHERE SISCOM_MI.APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=SISCOM_MI.TAB_FILIALI.ID " _
                & " AND DATA_APPUNTAMENTO = '" & Format(CInt(AnnoSelezionato.Value), "0000") & Format(CInt(MeseSelezionato.Value), "00") _
                & Format(CInt(Giorno), "00") & "' " _
                & condizioneStruttura _
                & " AND ID_OPERATORE_ELIMINAZIONE IS NULL " _
                & " AND APPUNTAMENTI_SPORTELLI.ID=ID_SPORTELLO " _
                & " GROUP BY ID_STRUTTURA,ID_SPORTELLO,APPUNTAMENTI_SPORTELLI.DESCRIZIONE ORDER BY 3"
            Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim idFiliale As Integer = 0
            Dim conteggio As Integer = 0
            Dim filiale As String = ""
            CType(Page.FindControl("Label" & Appuntamento), Label).Text = ""
            Dim contatore As Integer = 0
            Dim stileTabella As String = " cellpadding=""0"" cellspacing=""0"" style=""font-family:Arial;font-size:7pt;color:gray;font-weight:bold;width:95%"""
            Dim tdStyle As String = " style=""cursor:pointer;"""
            While MyReader.Read
                contatore += 1
                idFiliale = par.IfNull(MyReader("ID_STRUTTURA"), 0)
                conteggio = par.IfNull(MyReader("CONTEGGIO"), 0)
                filiale = par.IfNull(MyReader("DESCRIZIONE"), 0)
                '<a href="""" style=""padding: 4px; margin: 4px;text-decoration:blink; font-size: 9pt; font-weight: bold; cursor: pointer; text-align: center; text-indent: inherit""></a>
                '                    lblSegnalazioni.Text &= "<td style=""padding:3px;width:70px;height:30px;font-size: 10pt; font-weight: bold;cursor:pointer;color:#FFFFFF;font-family: Arial;float:left;text-align:center;background-color:" & colore & ";"" onclick=""javascript:ApriSegnalazioni(" & riga.Item("ID") & ");"">" _
                '                        & par.IfNull(riga.Item("NOME"), "").ToString _
                '                        & "</td><td>&nbsp;</td>"
                If contatore <= 4 Then
                    CType(Page.FindControl("Label" & Appuntamento), Label).Text &= "<table " & stileTabella & "><tr><td " & tdStyle & " onclick=""javascript:DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & idFiliale & ");"">" & Left(filiale, 12) & "</td><td " & tdStyle & " class=""notifica2"" onclick=""javascript:DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & idFiliale & ");"">" & conteggio & "</td></tr></table>"
                End If
                'CType(Page.FindControl("Appuntamento" & Appuntamento), Image).Attributes.Add("onclick", "javascript:DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "');")
            End While
            MyReader.Close()
            connData.chiudi(False)
            CType(Page.FindControl("Dettaglio" & Appuntamento), Label).Text = "<img onclick=""javascript:DettagliAppuntamenti('" & Format(CInt(Giorno), "00") & "', '" & Format(CInt(MeseSelezionato.Value), "00") & "', '" & Format(CInt(AnnoSelezionato.Value), "0000") & "'," & cmbStruttura.SelectedValue & ");"" src=""../../NuoveImm/Aggiungi.png"" style=""cursor:pointer"" width=""12"" height=""12"" />"
            TrovaAppuntamentoGiornaliero = True
        Catch ex As Exception
            TrovaAppuntamentoGiornaliero = False
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - TrovaAppuntamentoGiornaliera - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Function
    Private Sub PulisciBloccoDate()
        Try
            For i As Integer = 1 To 42 Step 1
                CType(Page.FindControl("BloccoData" & i), Label).Text = ""
                CType(Page.FindControl("Label" & i), Label).Text = ""
                CType(Page.FindControl("Dettaglio" & i), Label).Text = ""
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - PulisciBloccoDate - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Private Sub SettaBlocchiVuoti()
        Try
            For i As Integer = 1 To 42 Step 1
                If String.IsNullOrEmpty(CType(Page.FindControl("BloccoData" & i), Label).Text) Then
                    CType(Page.FindControl("BloccoData" & i), Label).Text = "&nbsp;"
                    CType(Page.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#CCCCCC"
                Else
                    Dim dataGiorno As Date = Format(CInt(CType(Page.FindControl("BloccoData" & i), Label).Text.ToString.Replace("&nbsp;", "")), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000")
                    If Not par.IsFestivo(dataGiorno, True) Then

                        If Format(Now, "dd/MM/yyyy") = dataGiorno Then
                            CType(Page.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#FFFFCC"
                            'ElseIf Format(CInt(CType(Page.FindControl("BloccoData" & i), Label).Text.ToString.Replace("&nbsp;", "")), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000") = Format(CInt(GiornoSelezionato.Value), "00") & "/" & Format(CInt(MeseSelezionato.Value), "00") & "/" & Format(CInt(AnnoSelezionato.Value), "0000") Then
                            '    CType(Page.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#FF9933"
                        Else
                            CType(Page.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#FFFFFF"
                        End If
                    Else
                        'CType(Page.FindControl("BloccoData" & i), Label).Text = "&nbsp;"
                        CType(Page.FindControl("tdbloccodata" & i), HtmlTableCell).BgColor = "#B20000"
                        'CType(Page.FindControl("Label" & i), Label).Text = "&nbsp;"
                        'CType(Page.FindControl("Dettaglio" & i), Label).Text = "&nbsp;"
                    End If
                End If
            Next
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - SettaBlocchiVuoti - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioMese_VisibleMonthChanged(sender As Object, e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles CalendarioMese.VisibleMonthChanged
        Try
            SettaCalendari()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CalendarioMese_VisibleMonthChanged - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioPrec_SelectionChanged(sender As Object, e As System.EventArgs) Handles CalendarioPrec.SelectionChanged
        Try
            Dim DataSelezionata As Date = CalendarioPrec.SelectedDate
            GiornoSelezionato.Value = DataSelezionata.ToString("dd")
            MeseSelezionato.Value = DataSelezionata.ToString("MM")
            AnnoSelezionato.Value = DataSelezionata.ToString("yyyy")
            SettaMese()
            CalcolaQuattroSlotDisponibili()
            CalendarioPrec.SelectedDate = Nothing
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CalendarioPrec_SelectionChanged - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioMese_SelectionChanged(sender As Object, e As System.EventArgs) Handles CalendarioMese.SelectionChanged
        Try
            Dim DataSelezionata As Date = CalendarioMese.SelectedDate
            GiornoSelezionato.Value = DataSelezionata.ToString("dd")
            MeseSelezionato.Value = DataSelezionata.ToString("MM")
            AnnoSelezionato.Value = DataSelezionata.ToString("yyyy")
            SettaMese()
            CalcolaQuattroSlotDisponibili()
            CalendarioMese.SelectedDate = Nothing
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CalendarioMese_SelectionChanged - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub CalendarioNext_SelectionChanged(sender As Object, e As System.EventArgs) Handles CalendarioNext.SelectionChanged
        Try
            Dim DataSelezionata As Date = CalendarioNext.SelectedDate
            GiornoSelezionato.Value = DataSelezionata.ToString("dd")
            MeseSelezionato.Value = DataSelezionata.ToString("MM")
            AnnoSelezionato.Value = DataSelezionata.ToString("yyyy")
            SettaMese()
            CalcolaQuattroSlotDisponibili()
            CalendarioNext.SelectedDate = Nothing
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CalendarioNext_SelectionChanged - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnOggi_Click(sender As Object, e As System.EventArgs) Handles btnOggi.Click
        Try
            GiornoSelezionato.Value = Date.Today.ToString("dd")
            MeseSelezionato.Value = Date.Today.Month
            AnnoSelezionato.Value = Date.Today.Year
            SettaMese()
            CalcolaQuattroSlotDisponibili()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - btnOggi_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        Try
            Dim DataIndietro As Date = CDate("01/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value).AddMonths(-1)
            GiornoSelezionato.Value = DataIndietro.ToString("dd")
            MeseSelezionato.Value = DataIndietro.ToString("MM")
            AnnoSelezionato.Value = DataIndietro.ToString("yyyy")
            SettaMese()
            CalcolaQuattroSlotDisponibili()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - btnIndietro_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnAvanti_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAvanti.Click
        Try
            Dim DataAvanti As Date = CDate("01/" & MeseSelezionato.Value & "/" & AnnoSelezionato.Value).AddMonths(1)
            GiornoSelezionato.Value = DataAvanti.ToString("dd")
            MeseSelezionato.Value = DataAvanti.ToString("MM")
            AnnoSelezionato.Value = DataAvanti.ToString("yyyy")
            SettaMese()
            CalcolaQuattroSlotDisponibili()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - btnAvanti_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbStruttura_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        SettaMese()
    End Sub

    'Private Sub TrovaSegnalazioni()
    '    Dim FlagConnessione As Boolean = False
    '    Try
    '        lblSegnalazioni.Text = "<div style=""overflow:auto;position: absolute; top: 420px;left: 138px;width:500px;height:70px;""><table cellpadding=""2"" cellspacing=""2""><tr>"
    '        '04/04/2014, non importa la struttura di appartenenza tutti vedono tutto
    '        'gestione de
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '            FlagConnessione = True
    '        End If
    '        'tipo richiesta=1, segnalazione guasti per strutture tecniche
    '        Dim TipoRichiesta As Integer = 1
    '        'tipo struttura=0, struttura tecnica
    '        Dim TipologiaStruttura As Integer = 0
    '        'lista colori strutture
    '        Dim listaColori As New Generic.List(Of String)
    '        listaColori.Clear()
    '        listaColori.Add("#0066CC")
    '        listaColori.Add("#CC3333")
    '        listaColori.Add("#CC6600")
    '        listaColori.Add("#000000")
    '        listaColori.Add("#339933")
    '        Dim listaTipologieGuasti As String = ""
    '        par.cmd.CommandText = "SELECT ID FROM TIPOLOGIE_GUASTI WHERE ID_TIPO_ST=" & TipologiaStruttura
    '        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        While lettore.Read
    '            listaTipologieGuasti &= par.IfNull(lettore("ID"), "0") & ","
    '        End While
    '        lettore.Close()
    '        If listaTipologieGuasti <> "" Then
    '            listaTipologieGuasti = Left(listaTipologieGuasti, Len(listaTipologieGuasti) - 1)
    '            par.cmd.CommandText = "SELECT ID,INITCAP(NOME) AS NOME FROM TAB_FILIALI WHERE ID_TIPO_ST=0"
    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            Dim dt As New Data.DataTable
    '            da.Fill(dt)
    '            da.Dispose()
    '            Dim idStruttura As String = ""
    '            Dim numeroBottoniInseriti As Integer = 5
    '            Dim colore As String = ""
    '            For Each riga As Data.DataRow In dt.Rows
    '                idStruttura = par.IfNull(riga("ID"), "")

    '                If idStruttura = "8" Then
    '                    'COMPARTO IMPIANTI
    '                    par.cmd.CommandText = "SELECT SEGNALAZIONI.ID, " _
    '                        & " ID_COMPLESSO AS IDENTIFICATIVO," _
    '                        & " (CASE WHEN (ID_COMPLESSO IS NOT NULL) THEN ('C') WHEN (ID_EDIFICIO IS NOT NULL) THEN ('E') WHEN (ID_UNITA IS NOT NULL) THEN ('U') ELSE ('') END) AS TIPO_S," _
    '                        & " COGNOME_RS||' '||NOME AS RICHIEDENTE," _
    '                        & " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
    '                        & " SUBSTR(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' AS DESCRIZIONE_RIC  " _
    '                        & " FROM SEGNALAZIONI " _
    '                        & " WHERE SEGNALAZIONI.ID_STATO=0" _
    '                        & " AND SEGNALAZIONI.TIPO_RICHIESTA=" & TipoRichiesta _
    '                        & " AND SEGNALAZIONI.ID_TIPOLOGIE  IN (" & listaTipologieGuasti & ")" _
    '                        & " AND FL_COMPARTO_IMPIANTI=1"
    '                Else
    '                    'ALTRI COMPARTI
    '                    par.cmd.CommandText = "SELECT SEGNALAZIONI.ID, " _
    '                        & " ID_COMPLESSO AS IDENTIFICATIVO," _
    '                        & " (CASE WHEN (ID_COMPLESSO IS NOT NULL) THEN ('C') WHEN (ID_EDIFICIO IS NOT NULL) THEN ('E') WHEN (ID_UNITA IS NOT NULL) THEN ('U') ELSE ('') END) AS TIPO_S," _
    '                        & " COGNOME_RS||' '||NOME AS RICHIEDENTE," _
    '                        & " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
    '                        & " SUBSTR(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' AS DESCRIZIONE_RIC  " _
    '                        & " FROM SEGNALAZIONI " _
    '                        & " WHERE SEGNALAZIONI.ID_STATO=0" _
    '                        & " AND SEGNALAZIONI.TIPO_RICHIESTA=" & TipoRichiesta _
    '                        & " AND SEGNALAZIONI.ID_TIPOLOGIE  IN (" & listaTipologieGuasti & ")" _
    '                        & " AND SEGNALAZIONI.ID_sTRUTTURA = " & idStruttura _
    '                        & " AND FL_COMPARTO_IMPIANTI=0"
    '                End If

    '                '& " UNION " _
    '                '& " SELECT SEGNALAZIONI.ID, " _
    '                '& " ID_EDIFICIO AS IDENTIFICATIVO," _
    '                '& " 'E' AS TIPO_S," _
    '                '& " COGNOME_RS||' '||NOME AS RICHIEDENTE," _
    '                '& " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
    '                '& " SUBSTR(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' AS DESCRIZIONE_RIC  " _
    '                '& " FROM SEGNALAZIONI " _
    '                '& " WHERE SEGNALAZIONI.ID_STATO=0" _
    '                '& " AND SEGNALAZIONI.TIPO_RICHIESTA=" & TipoRichiesta _
    '                '& " AND SEGNALAZIONI.ID_TIPOLOGIE  IN (" & listaTipologieGuasti & ")" _
    '                '& " AND SEGNALAZIONI.ID_UNITA IS NULL " _
    '                '& " AND SEGNALAZIONI.ID_EDIFICIO IN (SELECT ID FROM EDIFICI WHERE ID_COMPLESSO IN " _
    '                '& " (SELECT ID FROM COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & "))" _
    '                '& " UNION " _
    '                '& " SELECT SEGNALAZIONI.ID, " _
    '                '& " ID_UNITA AS IDENTIFICATIVO," _
    '                '& " 'U' AS TIPO_S," _
    '                '& " COGNOME_RS||' '||NOME AS RICHIEDENTE," _
    '                '& " TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,1,8),'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
    '                '& " SUBSTR(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' AS DESCRIZIONE_RIC  " _
    '                '& " FROM SEGNALAZIONI " _
    '                '& " WHERE SEGNALAZIONI.ID_STATO=0" _
    '                '& " AND SEGNALAZIONI.TIPO_RICHIESTA=" & TipoRichiesta _
    '                '& " AND SEGNALAZIONI.ID_TIPOLOGIE  IN (" & listaTipologieGuasti & ")" _
    '                '& " AND SEGNALAZIONI.ID_COMPLESSO IS NULL " _
    '                '& " AND SEGNALAZIONI.ID_EDIFICIO IS NULL " _
    '                '& " AND SEGNALAZIONI.ID_UNITA IN (SELECT ID FROM UNITA_IMMOBILIARI WHERE ID_EDIFICIO IN " _
    '                '& " (SELECT ID FROM EDIFICI WHERE ID_COMPLESSO IN " _
    '                '& " (SELECT ID FROM COMPLESSI_IMMOBILIARI WHERE ID_FILIALE=" & idStruttura & ")))"

    '                lettore = par.cmd.ExecuteReader
    '                If lettore.HasRows Then
    '                    colore = ""
    '                    colore = listaColori(numeroBottoniInseriti Mod 5)
    '                    '<a href="""" style=""padding: 4px; margin: 4px;text-decoration:blink; font-size: 9pt; font-weight: bold; cursor: pointer; text-align: center; text-indent: inherit""></a>
    '                    lblSegnalazioni.Text &= "<td style=""padding:3px;width:70px;height:30px;font-size: 10pt; font-weight: bold;cursor:pointer;color:#FFFFFF;font-family: Arial;float:left;text-align:center;background-color:" & colore & ";"" onclick=""javascript:ApriSegnalazioni(" & riga.Item("ID") & ");"">" _
    '                        & par.IfNull(riga.Item("NOME"), "").ToString _
    '                        & "</td><td>&nbsp;</td>"
    '                    numeroBottoniInseriti += 1
    '                End If
    '                lettore.Close()
    '            Next
    '        Else
    '            'nessuna tipologia guasto da far vedere
    '        End If
    '        '************CHIUSURA CONNESSIONE**********
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            FlagConnessione = False
    '        End If
    '        lblSegnalazioni.Text &= "</tr></table></div>"
    '    Catch ex As Exception
    '        '************CHIUSURA CONNESSIONE**********
    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If
    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Protected Sub btnAggiorna_Click(sender As Object, e As System.EventArgs) Handles btnAggiorna.Click
        SettaMese()
        CalcolaQuattroSlotDisponibili()
    End Sub

    Private Sub CalcolaQuattroSlotDisponibili()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT COUNT(*)-1 FROM SISCOM_MI.TAB_FILIALI WHERE ID_TIPO_ST=0"
            Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim totAppSport As Integer = 0
            If lett.Read Then
                totAppSport = par.IfNull(lett(0), 5) * 4
            End If
            lett.Close()
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
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ORARIO ASC"
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
                        cont = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
                            lettore.Close()
                            If contSportello = 0 Then
                                sportelloFilialeSlot1 = 4
                                OrarioFilialeSlot1 = orario.Item("ORARIO")
                                checkGiornoSlot1 = True
                                Exit While
                            End If
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
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ORARIO ASC"
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
                        cont = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                            contSportello = par.cmd.ExecuteScalar
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
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ORARIO ASC"
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
                        cont = par.cmd.ExecuteScalar
                        lettore.Close()
                        If cont < totAppSport Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 AND ID<>" & cmbStruttura.SelectedValue
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
                                contSportello = par.cmd.ExecuteScalar
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
                                contSportello = par.cmd.ExecuteScalar
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
                                contSportello = par.cmd.ExecuteScalar
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
                                contSportello = par.cmd.ExecuteScalar
                                lettore.Close()
                                If contSportello = 0 Then
                                    idFilialeSlot3 = par.IfNull(LettoreFiliali("id"), "")
                                    sportelloFilialeSlot3 = 4
                                    OrarioFilialeSlot3 = orario.Item("ORARIO")
                                    checkGiornoSlot3 = True
                                    checkExit = True
                                    Exit While
                                End If
                            End While
                            lettore.Close()
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
                    par.cmd.CommandText = "SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ORARIO ASC"
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
                        cont = par.cmd.ExecuteScalar
                        lettore.Close()
                        If cont < totAppSport Then
                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TAB_FILIALI WHERE TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 AND ID<>" & cmbStruttura.SelectedValue
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
                                contSportello = par.cmd.ExecuteScalar
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
                                contSportello = par.cmd.ExecuteScalar
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
                                contSportello = par.cmd.ExecuteScalar
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
                                contSportello = par.cmd.ExecuteScalar
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
                            End While
                            lettore.Close()
                            If checkExit = True Then
                                Exit While
                            End If
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
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CalcolaPrimiDueSlotDisponibili - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnEsci_Click(sender As Object, e As System.EventArgs) Handles btnEsci.Click
        Response.Write("<script>if(window.opener.document.getElementById('btnControllaAppuntamento')!=null){window.opener.document.getElementById('btnControllaAppuntamento').click();};self.close();</script>")
    End Sub

    Private Sub settaIndirizzi()
        Try
            connData.apri()
            par.cmd.CommandText = "SELECT NOME, DESCRIZIONE||' '||CIVICO||','||LOCALITA AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 AND TAB_FILIALI.ID_INDIRIZZO=INDIRIZZI.ID "
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
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - settaIndirizzi - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
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
                & " AND APPUNTAMENTI_CALL_CENTER.ID_ORARIO=APPUNTAMENTI_ORARI.ID(+) order by 1 desc" _
                & condizioneStruttura
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                Dim xls As New ExcelSiSol
                Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportAppuntamenti", "ExportAppuntamenti", dt, True, , True)
                If IO.File.Exists(Server.MapPath("..\/..\/FileTemp\/") & nomeFile) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante l\'esportazione. Riprovare!');", True)
                End If
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ButtonExportAgendaAppuntamenti_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
End Class
