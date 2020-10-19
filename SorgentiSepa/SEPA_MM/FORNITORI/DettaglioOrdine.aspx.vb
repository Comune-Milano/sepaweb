Imports Telerik.Web.UI
Partial Class FORNITORI_DettaglioOrdine
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Dim StatoF As String = ""

    Protected Sub RadAgenda_AppointmentDataBound(sender As Object, e As Telerik.Web.UI.SchedulerEventArgs) Handles RadAgenda.AppointmentDataBound
        e.Appointment.AllowDelete = False
        e.Appointment.AllowEdit = False
        'If Len(e.Appointment.Description) > 8 Then
        '    StatoF = Mid(e.Appointment.Description, 9, 2)
        'Else
        '    StatoF = ""
        'End If
        'If StatoF = "1" Or StatoF = "2" Then
        If Format(CDate(Mid(e.Appointment.Description, 1, 10)), "yyyyMMdd") < Format(e.Appointment.End, "yyyyMMdd") Then
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#df0528")
            iTotFuoriTempo = iTotFuoriTempo + 1
        Else
            e.Appointment.BackColor = System.Drawing.ColorTranslator.FromHtml("#089210")
            iTotRegolari = iTotRegolari + 1
        End If
        'Else
        'e.Appointment.BackColor = Drawing.Color.LightGreen
        'End If
        If e.Appointment.Visible = True Then
            e.Appointment.CssClass = "ChangeCursor"
        End If

        'If InStr(e.Appointment.Subject, "BOZZA") > 0 Then 'BOZZA
        '    e.Appointment.BackColor = Drawing.Color.LightGray
        '    e.Appointment.ToolTip = Replace(e.Appointment.Subject, "Stato: BOZZA -", "")
        '    e.Appointment.Subject = e.Appointment.ToolTip
        'End If
        'If InStr(e.Appointment.Subject, "EMESSO") > 0 Then 'EMESSO
        '    e.Appointment.BackColor = Drawing.Color.LightGreen
        '    e.Appointment.ToolTip = Replace(e.Appointment.Subject, "Stato: EMESSO -", "")
        '    e.Appointment.Subject = e.Appointment.ToolTip
        'End If
        'If InStr(e.Appointment.Subject, "CONSUNTIVATO") > 0 Then 'CONSUNTIVATO
        '    e.Appointment.BackColor = Drawing.Color.LightBlue
        '    e.Appointment.ToolTip = Replace(e.Appointment.Subject, "Stato: CONSUNTIVATO -", "")
        '    e.Appointment.Subject = e.Appointment.ToolTip
        'End If
        'If InStr(e.Appointment.Subject, "INTEGRATO") > 0 Then ' INTEGRATO
        '    e.Appointment.BackColor = Drawing.Color.LightYellow
        '    e.Appointment.ToolTip = Replace(e.Appointment.Subject, "Stato: INTEGRATO -", "")
        '    e.Appointment.Subject = e.Appointment.ToolTip
        'End If
        'If InStr(e.Appointment.Subject, "EMESSO PAGAMENTO") > 0 Then 'EMESSO PAGAMENTO
        '    e.Appointment.BackColor = Drawing.Color.LightCyan
        '    e.Appointment.ToolTip = Replace(e.Appointment.Subject, "Stato: EMESSO PAGAMENTO -", "")
        '    e.Appointment.Subject = e.Appointment.ToolTip
        'End If
        'If InStr(e.Appointment.Subject, "ANNULLATO") > 0 Then 'ANNULLATO
        '    e.Appointment.BackColor = Drawing.Color.LightCoral
        '    e.Appointment.ToolTip = Replace(e.Appointment.Subject, "Stato: ANNULLATO -", "")
        '    e.Appointment.Subject = e.Appointment.ToolTip
        'End If

        ''If sOrdine <> "" Then
        ''    If UCase(e.Appointment.Subject) = UCase(sOrdine) Then
        ''        RadAgenda.SelectedDate = e.Appointment.Start
        ''        e.Appointment.BackColor = Drawing.Color.Red
        ''        bTrovatoElemento = "1"
        ''        bDettagliTrovatoElemento = "Data " & e.Appointment.Start & " Fornitore: " & e.Appointment.Resources(0).Text
        ''    End If
        ''End If

        RadAgenda.AllowDelete = False
        RadAgenda.AllowEdit = False
        RadAgenda.AllowInsert = False
    End Sub

    Protected Sub RidimensionaEventi()
        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub
    Protected Sub RadAgenda_NavigationComplete(sender As Object, e As Telerik.Web.UI.SchedulerNavigationCompleteEventArgs) Handles RadAgenda.NavigationComplete
        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub RadAjaxManager1_AjaxRequest(ByVal sender As Object, ByVal e As Telerik.Web.UI.AjaxRequestEventArgs)
        If e.Argument = "InitialPageLoad" Then

        End If
    End Sub

    Private Function CaricaDatiMaschera()
        Me.connData = New CM.datiConnessione(par, False, False)
        RicavaOrdini()
        imgAccettaDate.Visible = False
        Panel2.Visible = True
        If sOrdine <> "X" Then
            If indiceM.Value <> "" Then
                CaricaDati(indiceM.Value)
                CaricaPreventivi(indiceM.Value)
                CaricaAllegati(indiceM.Value)
                CaricaIrregolarita()
                CaricaEventi()
                dgvInterventi.Rebind()
                RadGridPreventivi.Rebind()
                RadGridAllegati.Rebind()
                RadGridIrregolarita.Rebind()
                dgvEventi.Rebind()
            End If
        End If
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                Response.Expires = 0
                If Session.Item("OPERATORE") = "" Then
                    Response.Redirect("../AccessoNegato.htm", False)
                End If
                If Session.Item("MOD_FORNITORI") <> "1" Then
                    Response.Redirect("../AccessoNegato.htm", False)
                End If
                If Session.Item("MOD_FORNITORI_ODL") <> "1" Then
                    Response.Redirect("../AccessoNegato.htm", False)
                End If

                If InStr(Request.QueryString("D"), "_") > 0 Then
                    sOrdine = Replace(Request.QueryString("D"), "_", "/")
                Else
                    If InStr(UCase(Request.QueryString("D")), "REP") > 0 Then
                        'sOrdine = Replace(Mid(Request.QueryString("D"), InStr(Request.QueryString("D"), "ODL"), Len(Request.QueryString("D"))), "ODL ", "")
                        sOrdine = Trim(Replace(Mid(Request.QueryString("D"), 1, InStr(Request.QueryString("D"), "REP.") - 1), "ODL ", ""))
                    Else
                        sOrdine = Request.QueryString("D")
                    End If

                End If

                sGiorno = Request.QueryString("T")

                CaricaDatiMaschera()
                If Session.Item("MOD_FORNITORI_SLE") = "1" Then
                    SolaLettura()
                End If
            Catch ex As Exception

                Session.Add("ERRORE", "Provenienza: Fornitori - Ordini - Carica - " & ex.Message)
                Response.Redirect("../Errore.aspx", True)
            End Try
        End If
    End Sub

    Private Sub SolaLettura()
        RadGridPreventivi.Enabled = False
        RadGridAllegati.Enabled = False

        RadGridIrregolarita.Enabled = False
        RadGridIrregolarita.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
        RadGridIrregolarita.MasterTableView.GetColumn("EditCommandColumn").Display = False
        RadGridIrregolarita.MasterTableView.GetColumn("DeleteColumn").Display = False

        
    End Sub

    Private Function CaricaCriteri() As String
        CaricaCriteri = ""
        Dim sStrStato As String = ""
        Dim bTrovato As Boolean = False
        Dim sCompara As String = ""

        If Replace(sOrdine, "X", "") <> "" Then
            If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            CaricaCriteri = CaricaCriteri & " (MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO) ='" & sOrdine & "' "
            bTrovato = True
        End If

        If Replace(sGiorno, "X", "") <> "" Then
            If bTrovato = True Then CaricaCriteri = CaricaCriteri & " AND "
            CaricaCriteri = CaricaCriteri & " (CASE WHEN MANUTENZIONI.DATA_PGI IS NULL THEN TO_DATE (NVL (MANUTENZIONI.DATA_INIZIO_INTERVENTO, (SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91')),'yyyymmdd') ELSE TO_DATE (MANUTENZIONI.DATA_PGI, 'yyyymmdd') END) = TO_DATE('" & Replace(sGiorno, "-", "") & "','yyyymmdd')  "
            bTrovato = True
        End If
        If CaricaCriteri <> "" Then CaricaCriteri = CaricaCriteri & " and "
    End Function

    Private Sub VerificaOperatore()
        Try
            connData.apri()
            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("id_operatore")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                If par.IfNull(myReader("MOD_FO_ID_FO"), "0") <> "0" Then
                    sStrAppalti = " AND APPALTI.ID_FORNITORE=" & par.IfNull(myReader("MOD_FO_ID_FO"), "0")
                Else
                    sStrAppalti = ""
                End If
            End If
            myReader.Close()
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - VerificaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Public Property sOrdine() As String
        Get
            If Not (ViewState("par_sOrdine") Is Nothing) Then
                Return CStr(ViewState("par_sOrdine"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sOrdine") = value
        End Set
    End Property

    Public Property iTotRegolari() As Integer
        Get
            If Not (ViewState("par_iTotRegolari") Is Nothing) Then
                Return CInt(ViewState("par_iTotRegolari"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotRegolari") = value
        End Set
    End Property

    Public Property iTotFuoriTempo() As Integer
        Get
            If Not (ViewState("par_iTotFuoriTempo") Is Nothing) Then
                Return CInt(ViewState("par_iTotFuoriTempo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_iTotFuoriTempo") = value
        End Set
    End Property

    Public Property sGiorno() As String
        Get
            If Not (ViewState("par_sGiorno") Is Nothing) Then
                Return CStr(ViewState("par_sGiorno"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sGiorno") = value
        End Set
    End Property

    Public Property sStrSql() As String
        Get
            If Not (ViewState("par_sStrSql") Is Nothing) Then
                Return CStr(ViewState("par_sStrSql"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSql") = value
        End Set
    End Property

    Public Property sStrSqlInt() As String
        Get
            If Not (ViewState("par_sStrSqlInt") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlInt"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlInt") = value
        End Set
    End Property

    Public Property sStrAppalti() As String
        Get
            If Not (ViewState("par_sStrAppalti") Is Nothing) Then
                Return CStr(ViewState("par_sStrAppalti"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrAppalti") = value
        End Set
    End Property

    Public Property bTrovatoElemento() As String
        Get
            If Not (ViewState("par_bTrovatoElemento") Is Nothing) Then
                Return CStr(ViewState("par_bTrovatoElemento"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_bTrovatoElemento") = value
        End Set
    End Property

    Public Property bDettagliTrovatoElemento() As String
        Get
            If Not (ViewState("par_bDettagliTrovatoElemento") Is Nothing) Then
                Return CStr(ViewState("par_bDettagliTrovatoElemento"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_bDettagliTrovatoElemento") = value
        End Set
    End Property

    Private Sub RicavaOrdini()
        Try
            Dim dt As System.Data.DataTable
            Dim s As String = CaricaCriteri()
            Try
                RadAgenda.DataStartField = "DATA_INIZIO_INTERVENTO"
                RadAgenda.DataEndField = "DATA_FINE1"
                RadAgenda.DataSubjectField = "DESCRIZIONE_ORDINE"
                RadAgenda.DataKeyField = "ID_MANUTENZIONE"
                RadAgenda.DataDescriptionField = "DATA_FINE"

                ''sStrSql = "select (CASE WHEN MANUTENZIONI.DATA_TDL IS NULL THEN TO_DATE (nvl(MANUTENZIONI.DATA_FINE_INTERVENTO,(SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91')) || '235959', 'yyyymmddHH24MISS') ELSE (CASE WHEN SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL THEN TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO || '235959', 'yyyymmddHH24MISS') ELSE TO_DATE (MANUTENZIONI.DATA_TDL || '235959', 'yyyymmddHH24MISS') END)END) AS DATA_FINE," _
                ''        & "(CASE WHEN SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL THEN (case when MANUTENZIONI.DATA_TDL IS NULL then TO_DATE (TO_CHAR (SYSDATE, 'YYYYMMDD') || '235959','yyyymmddHH24MISS') else TO_DATE (MANUTENZIONI.DATA_TDL || '235959', 'yyyymmddHH24MISS') end) ELSE TO_DATE (SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') END) AS DATA_FINE1," _
                ''        & "FORNITORI.RAGIONE_SOCIALE,MANUTENZIONI.ID AS ID_MANUTENZIONE, " _
                ''        & "MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO AS NUM_ODL, " _
                ''        & "(CASE WHEN MANUTENZIONI.DATA_PGI IS NULL THEN to_date(nvl(MANUTENZIONI.DATA_INIZIO_INTERVENTO,(SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91')),'yyyymmdd') ELSE to_date(MANUTENZIONI.DATA_PGI,'yyyymmdd') END) AS DATA_INIZIO_INTERVENTO " _
                ''        & ", " _
                ''        & " " _
                ''        & "FORNITORI.RAGIONE_SOCIALE||'- Stato: '||TAB_STATI_ODL.DESCRIZIONE||' - Inizio presunto:'||to_date(DATA_INIZIO_INTERVENTO,'yyyymmdd')||' Fine presunta: '||to_date(MANUTENZIONI.DATA_FINE_INTERVENTO,'yyyymmdd')||' - Data PGI: '||to_date(DATA_PGI,'yyyymmdd')||' Data TDL: '||to_date(DATA_TDL,'yyyymmdd') AS DESCRIZIONE_ORDINE " _
                ''        & "from " _
                ''        & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI,siscom_mi.TAB_STATI_ODL,SISCOM_MI.SEGNALAZIONI_FORNITORI,SISCOM_MI.APPALTI " _
                ''        & "WHERE " _
                ''        & s _
                ''        & " APPALTI.ID=MANUTENZIONI.ID_APPALTO AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID AND FORNITORI.ID=APPALTI.ID_FORNITORE and TAB_STATI_ODL.id(+)=manutenzioni.stato " _
                ''        & "ORDER BY FORNITORI.RAGIONE_SOCIALE"
                'sStrSql = "select (CASE WHEN MANUTENZIONI.DATA_TDL IS NULL THEN TO_DATE (NVL (MANUTENZIONI.DATA_FINE_INTERVENTO,(SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91')) || '235959', 'yyyymmddHH24MISS') ELSE (CASE WHEN SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL THEN TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') ELSE (CASE WHEN NVL ((SELECT id_tipo FROM siscom_mi.SEGNALAZIONI_FO_IRR WHERE  id_segnalazione =SEGNALAZIONI_FORNITORI.ID AND data_ora =(SELECT MAX (data_ora) FROM siscom_mi.SEGNALAZIONI_FO_IRR FOIRR WHERE FOIRR.id_segnalazione =SEGNALAZIONI_FORNITORI.ID)), -1) <> 0  THEN TO_DATE (SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') ELSE TO_DATE(MANUTENZIONI.DATA_FINE_INTERVENTO|| '235959','yyyymmddHH24MISS') END)  END) END) AS DATA_FINE," _
                '        & "(CASE WHEN SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL THEN (case when MANUTENZIONI.DATA_TDL IS NULL then TO_DATE (TO_CHAR (SYSDATE, 'YYYYMMDD') || '235959','yyyymmddHH24MISS') else TO_DATE (MANUTENZIONI.DATA_TDL || '235959', 'yyyymmddHH24MISS') end) ELSE TO_DATE (SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') END) AS DATA_FINE1," _
                '        & "FORNITORI.RAGIONE_SOCIALE,MANUTENZIONI.ID AS ID_MANUTENZIONE, " _
                '        & "MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO AS NUM_ODL, " _
                '        & "(CASE WHEN MANUTENZIONI.DATA_PGI IS NULL THEN to_date(nvl(MANUTENZIONI.DATA_INIZIO_INTERVENTO,(SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91')),'yyyymmdd') ELSE to_date(MANUTENZIONI.DATA_PGI,'yyyymmdd') END) AS DATA_INIZIO_INTERVENTO " _
                '        & ", " _
                '        & " " _
                '        & "FORNITORI.RAGIONE_SOCIALE||'- Stato: '||TAB_STATI_ODL.DESCRIZIONE||' - Inizio presunto:'||to_date(DATA_INIZIO_INTERVENTO,'yyyymmdd')||' Fine presunta: '||to_date(MANUTENZIONI.DATA_FINE_INTERVENTO,'yyyymmdd')||' - Data PGI: '||to_date(DATA_PGI,'yyyymmdd')||' Data TDL: '||to_date(DATA_TDL,'yyyymmdd') AS DESCRIZIONE_ORDINE " _
                '        & "from " _
                '        & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI,siscom_mi.TAB_STATI_ODL,SISCOM_MI.SEGNALAZIONI_FORNITORI,SISCOM_MI.APPALTI " _
                '        & "WHERE " _
                '        & s _
                '        & " APPALTI.ID=MANUTENZIONI.ID_APPALTO AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID AND FORNITORI.ID=APPALTI.ID_FORNITORE and TAB_STATI_ODL.id(+)=manutenzioni.stato " _
                '        & "ORDER BY FORNITORI.RAGIONE_SOCIALE"
                sStrSql = "select (CASE WHEN MANUTENZIONI.DATA_TDL IS NULL THEN TO_DATE (NVL (MANUTENZIONI.DATA_FINE_INTERVENTO,(SELECT SUBSTR (DATA_ORA, 1, 8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE = MANUTENZIONI.ID AND COD_EVENTO = 'F91')) || '235959', 'yyyymmddHH24MISS') ELSE (CASE WHEN SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL THEN TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') ELSE (CASE WHEN NVL ((SELECT id_tipo FROM siscom_mi.SEGNALAZIONI_FO_IRR WHERE  id_segnalazione =SEGNALAZIONI_FORNITORI.ID AND data_ora =(SELECT MAX (data_ora) FROM siscom_mi.SEGNALAZIONI_FO_IRR FOIRR WHERE FOIRR.id_segnalazione =SEGNALAZIONI_FORNITORI.ID)), -1) <> 0  THEN TO_DATE (SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') ELSE TO_DATE(MANUTENZIONI.DATA_FINE_INTERVENTO|| '235959','yyyymmddHH24MISS') END)  END) END) AS DATA_FINE," _
                        & "(CASE WHEN SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO IS NULL THEN (CASE WHEN MANUTENZIONI.DATA_TDL IS NULL THEN (CASE WHEN TO_DATE (TO_CHAR (SYSDATE, 'YYYYMMDD') || '235959','yyyymmddHH24MISS')<TO_DATE (MANUTENZIONI.DATA_INIZIO_INTERVENTO || '235959','yyyymmddHH24MISS') THEN TO_DATE (MANUTENZIONI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') ELSE TO_DATE (TO_CHAR (SYSDATE, 'YYYYMMDD') || '235959','yyyymmddHH24MISS') END) ELSE TO_DATE (MANUTENZIONI.DATA_TDL || '235959','yyyymmddHH24MISS') END) ELSE TO_DATE (SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO || '235959','yyyymmddHH24MISS') END) AS DATA_FINE1," _
                        & "FORNITORI.RAGIONE_SOCIALE,MANUTENZIONI.ID AS ID_MANUTENZIONE, " _
                        & "MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO AS NUM_ODL, " _
                        & "(CASE WHEN MANUTENZIONI.DATA_PGI IS NULL THEN to_date(nvl(MANUTENZIONI.DATA_INIZIO_INTERVENTO,(SELECT SUBSTR(DATA_ORA,1,8) FROM SISCOM_MI.EVENTI_MANUTENZIONE WHERE ID_MANUTENZIONE=MANUTENZIONI.ID AND COD_EVENTO='F91')),'yyyymmdd') ELSE to_date(MANUTENZIONI.DATA_PGI,'yyyymmdd') END) AS DATA_INIZIO_INTERVENTO " _
                        & ", " _
                        & " " _
                        & "FORNITORI.RAGIONE_SOCIALE||'- Stato: '||TAB_STATI_ODL.DESCRIZIONE||' - Inizio presunto:'||to_date(DATA_INIZIO_INTERVENTO,'yyyymmdd')||' Fine presunta: '||to_date(MANUTENZIONI.DATA_FINE_INTERVENTO,'yyyymmdd')||' - Data PGI: '||to_date(DATA_PGI,'yyyymmdd')||' Data TDL: '||to_date(DATA_TDL,'yyyymmdd') AS DESCRIZIONE_ORDINE " _
                        & "from " _
                        & "SISCOM_MI.FORNITORI, SISCOM_MI.MANUTENZIONI,siscom_mi.TAB_STATI_ODL,SISCOM_MI.SEGNALAZIONI_FORNITORI,SISCOM_MI.APPALTI " _
                        & "WHERE " _
                        & s _
                        & " APPALTI.ID=MANUTENZIONI.ID_APPALTO AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE(+)=MANUTENZIONI.ID AND FORNITORI.ID=APPALTI.ID_FORNITORE and TAB_STATI_ODL.id(+)=manutenzioni.stato " _
                        & "ORDER BY FORNITORI.RAGIONE_SOCIALE"

                '
                dt = par.getDataTableGrid(sStrSql)
                RadAgenda.DataSource = dt
                RadAgenda.DataBind()
                RadAgenda.SelectedView = SchedulerViewType.TimelineView
                RadAgenda.GroupingDirection = DirectCast([Enum].Parse(GetType(GroupingDirection), "Vertical"), GroupingDirection)

                RadAgenda.ResourceTypes.Clear()
                Dim restype1 As New ResourceType("NUM_ODL")

                restype1.DataSource = par.getDataTableGrid("select distinct PROGR||'/'||ANNO AS NUM_ODL from SISCOM_MI.MANUTENZIONI WHERE " & Mid(s, 1, Len(s) - 4) & " order by NUM_ODL ASC")
                restype1.KeyField = "NUM_ODL"
                restype1.TextField = "NUM_ODL"
                restype1.ForeignKeyField = "NUM_ODL"

                RadAgenda.ResourceTypes.Add(restype1)
                RadAgenda.GroupBy = "NUM_ODL"
                RadAgenda.DataBind()

                RadAgenda.TimelineView.NumberOfSlots = 14
                If sGiorno <> "X" Then
                    RadAgenda.SelectedDate = sGiorno
                Else
                    RadAgenda.SelectedDate = dt.Rows(0).Item("DATA_INIZIO_INTERVENTO").ToString
                End If

                RadAgenda.Visible = True
            Catch ex1 As Exception
                ' VisualizzaAlert("Errore nella rappresentazione grafica dell'ODL. Le date di inizio-fine non sono compatibili", 2)
                RadAgenda.Visible = True
            End Try

            If sGiorno = "X" Then
                connData.apri()
                par.cmd.CommandText = "select * from siscom_mi.manutenzioni where progr||'/'||anno='" & sOrdine & "'"
                Session.Add("g3", "select * from siscom_mi.manutenzioni where progr||'/'||anno='" & sOrdine & "'")
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    If par.IfNull(myReader("data_pgi"), "") <> "" Then
                        sGiorno = par.FormattaData(par.IfNull(myReader("data_pgi"), Format(Now, "dd/MM/yyyy")))
                    Else
                        sGiorno = par.FormattaData(par.IfNull(myReader("data_inizio_intervento"), Format(Now, "dd/MM/yyyy")))
                    End If

                    indiceM.Value = par.IfNull(myReader("id"), "")
                End If
                myReader.Close()
            End If



        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Segnalazioni - RicavaOrdini - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Private Sub CaricaDati(ByVal sIndice As String)
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri()
            Dim indiceSegnalazione As String = ""

            imgAccettaDate.Visible = False
            par.cmd.CommandText = "select segnalazioni_fornitori.*,TAB_STATI_SEGNALAZIONI_FO.descrizione as statoI from siscom_mi.segnalazioni_fornitori,SISCOM_MI.TAB_STATI_SEGNALAZIONI_FO where TAB_STATI_SEGNALAZIONI_FO.ID=SEGNALAZIONI_FORNITORI.ID_STATO AND segnalazioni_fornitori.id_manutenzione=" & sIndice
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblStatoIntervento.Text = par.IfNull(myReader("statoI"), "--")
                indiceSegnalazione = par.IfNull(myReader("ID_SEGNALAZIONE"), "")
                sIndiceIntervento = par.IfNull(myReader("id"), "-1")

                lblDataFINEINT.Text = par.FormattaData(par.IfNull(myReader("DATA_FINE_INTERVENTO"), ""))
                If par.IfNull(myReader("id_stato"), "0") = "8" And par.IfNull(myReader("fl_pr_contab"), "0") = "0" Then
                    If (Not IsNothing(Session.Item("FL_SUPERDIRETTORE"))) AndAlso (Session.Item("FL_SUPERDIRETTORE") = 1) Then
                        btnPrContab.Visible = True
                    Else
                        par.cmd.CommandText = "SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO = '30000000' AND ID_GRUPPO IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID = " & sIndice & ") "
                        Dim idOperatore As String = par.IfNull(par.cmd.ExecuteScalar, "")
                        If Session.Item("ID_OPERATORE") = idOperatore Then
                            btnPrContab.Visible = True
                        Else
                        btnPrContab.Visible = False
                    End If
                    End If
                Else
                    btnPrContab.Visible = False
                End If
                If par.IfNull(myReader("fl_rdo"), "0") = "1" Then
                    lblRDORichiesto.Visible = True
                    lblRDORichiesto.Text = "PREVENTIVO RICHIESTO!"

                    lblStatoPR.Visible = True
                    par.cmd.CommandText = "select SEGNALAZIONI_FO_PREV_STATI.descrizione as stato_pr from siscom_mi.SEGNALAZIONI_FO_PREVENTIVI,siscom_mi.SEGNALAZIONI_FO_PREV_STATI where SEGNALAZIONI_FO_PREV_STATI.id(+)=SEGNALAZIONI_FO_PREVENTIVI.id_stato and SEGNALAZIONI_FO_PREVENTIVI.id_segnalazione=" & par.IfNull(myReader("id"), 0)
                    Dim myReaderPR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderPR.Read Then
                        lblRDORichiesto.Text = par.IfNull(myReaderPR("stato_pr"), "")
                    End If
                    myReaderPR.Close()
                Else
                    lblRDORichiesto.Visible = True
                    RadTabStrip1.Tabs(2).Visible = False
                    lblStatoPR.Visible = False
                End If
            End If
            myReader.Close()

            If sIndice <> "0" Then
                par.cmd.CommandText = "select fornitori.ragione_sociale, manutenzioni.*,pf_main.id_esercizio_finanziario from SISCOM_MI.APPALTI,siscom_mi.fornitori,siscom_mi.manutenzioni,siscom_mi.pf_main where APPALTI.ID=MANUTENZIONI.ID_APPALTO AND fornitori.id(+)=APPALTI.id_fornitore and pf_main.id(+)=manutenzioni.id_piano_finanziario and manutenzioni.id=" & sIndice
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    vNUM_ODL.Value = par.IfNull(myReader1("PROGR"), "")
                    vANNO_ODL.Value = par.IfNull(myReader1("ANNO"), "")
                    vEF_F.Value = par.IfNull(myReader1("ID_ESERCIZIO_FINANZIARIO"), "")
                    lblFornitore.Text = " di " & Mid(par.IfNull(myReader1("ragione_sociale"), ""), 1, 40)
                    lblOrdine.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_ORDINE"), ""))
                    If par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), "") <> "" Then
                        lblInizioODL.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
                    Else
                        lblInizioODL.Text = "---"
                    End If
                    If par.IfNull(myReader1("DATA_FINE_INTERVENTO"), "") <> "" Then
                        lblFineODL.Text = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
                    Else
                        lblFineODL.Text = "---"
                    End If
                    lblRichiestaODL.Text = Mid(par.IfNull(myReader1("DESCRIZIONE"), "---"), 1, 100)
                    lblDanneggiatoODL.Text = par.IfNull(myReader1("DANNEGGIATO"), "---")
                    Dim Esito As String = CaricaInterventi(sIndice)

                    If par.IfNull(myReader1("data_pgi"), "") <> "" Then
                        lblDataPGI.Text = par.FormattaData(par.IfNull(myReader1("data_pgi"), ""))
                    Else
                        lblDataPGI.Text = "---"
                    End If
                    If par.IfNull(myReader1("data_tdl"), "") <> "" Then
                        lblDataTDL.Text = par.FormattaData(par.IfNull(myReader1("data_tdl"), ""))
                    Else
                        lblDataTDL.Text = "---"
                    End If

                    If par.IfNull(myReader1("ID_SCALA"), "0") <> "0" Then
                        par.cmd.CommandText = "select scale_edifici.descrizione as scala,indirizzi.* from siscom_mi.indirizzi,siscom_mi.EDIFICI,siscom_mi.scale_edifici where indirizzi.id(+)=EDIFICI.id_indirizzo_PRINCIPALE and EDIFICI.id=scale_edifici.id_edificio and scale_edifici.id=" & myReader1("id_scala")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblUbicazione.Text = "SCALA " & par.IfNull(myReader2("scala"), "") & " dell'EDIFICIO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                        End If
                        myReader2.Close()
                    ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "0") <> "0" Then
                        par.cmd.CommandText = "select indirizzi.* from siscom_mi.indirizzi,siscom_mi.EDIFICI where indirizzi.id(+)=EDIFICI.id_indirizzo_PRINCIPALE and EDIFICI.id=" & myReader1("id_EDIFICIO")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblUbicazione.Text = "EDIFICIO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                        End If
                        myReader2.Close()
                    ElseIf par.IfNull(myReader1("ID_COMPLESSO"), "0") <> "0" Then
                        par.cmd.CommandText = "select indirizzi.* from siscom_mi.indirizzi,siscom_mi.complessi_immobiliari where indirizzi.id(+)=complessi_immobiliari.id_indirizzo_riferimento and complessi_immobiliari.id=" & myReader1("id_complesso")
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblUbicazione.Text = "COMPLESSO in " & par.IfNull(myReader2("DESCRIZIONE"), "") & " N. " & par.IfNull(myReader2("CIVICO"), "") & " " & par.IfNull(myReader2("CAP"), "") & " - " & par.IfNull(myReader2("LOCALITA"), "")
                        End If
                        myReader2.Close()
                    End If
                    par.cmd.CommandText = "SELECT NUM_REPERTORIO,DATA_REPERTORIO,DESCRIZIONE FROM SISCOM_MI.APPALTI WHERE ID=" & myReader1("id_APPALTO")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        lblNumContratto.Text = par.IfNull(myReader3("NUM_REPERTORIO"), "") & " del " & par.FormattaData(par.IfNull(myReader3("DATA_REPERTORIO"), ""))
                        lblDescrizioneContratto.Text = Mid(par.IfNull(myReader3("DESCRIZIONE"), ""), 1, 50)
                    End If
                    myReader3.Close()

                    Dim DataRiferimento As String = ""
                    If lblDataPGI.Text = "---" Or lblDataTDL.Text = "---" Then
                        DataRiferimento = ""
                    Else
                        DataRiferimento = par.AggiustaData(lblDataPGI.Text) & par.AggiustaData(lblDataTDL.Text)
                    End If
                    Dim dateVisisbili As Boolean = False
                    If (Not IsNothing(Session.Item("FL_SUPERDIRETTORE"))) AndAlso (Session.Item("FL_SUPERDIRETTORE") = 1) Then
                        dateVisisbili = True
                    Else
                        par.cmd.CommandText = "SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO = '30000000' AND ID_GRUPPO IN (SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID = " & sIndice & ") "
                        Dim idOperatore As String = par.IfNull(par.cmd.ExecuteScalar, "")
                        If Session.Item("ID_OPERATORE") = idOperatore Then
                            dateVisisbili = True
                        Else
                            dateVisisbili = False
                        End If
                    End If

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EVENTI_SEGNALAZIONI_FO WHERE COD_EVENTO IN ('F264','F265') AND ID_SEGNALAZIONE_FO=" & sIndiceIntervento & " AND ID_SEGNALAZIONE_FO NOT IN (SELECT ID_SEGNALAZIONE FROM SISCOM_MI.SEGNALAZIONI_FO_IRR WHERE RIFERIMENTO='" & DataRiferimento & "')"
                    Dim myReader4 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader4.HasRows = True Then
                        par.cmd.CommandText = "select * from siscom_mi.eventi_segnalazioni_fo where cod_evento='F272' and ID_SEGNALAZIONE_FO=" & sIndiceIntervento & " AND MOTIVAZIONE='" & lblDataPGI.Text & " - " & lblDataTDL.Text & "'"
                        Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader5.HasRows = True Then
                            imgAccettaDate.Visible = False
                        Else
                            If par.AggiustaData(lblDataPGI.Text) >= par.AggiustaData(lblInizioODL.Text) And par.AggiustaData(lblDataPGI.Text) <= par.AggiustaData(lblFineODL.Text) Then
                                If par.AggiustaData(lblDataTDL.Text) >= par.AggiustaData(lblInizioODL.Text) And par.AggiustaData(lblDataTDL.Text) <= par.AggiustaData(lblFineODL.Text) Then
                                    imgAccettaDate.Visible = False
                                Else
                                    If Session.Item("MOD_FORNITORI_SLE") = "0" Then
                                        If dateVisisbili Then
                                        imgAccettaDate.Visible = True
                                    Else
                                        imgAccettaDate.Visible = False
                                    End If
                                    Else
                                        imgAccettaDate.Visible = False
                                    End If
                                End If
                            Else
                                If Session.Item("MOD_FORNITORI_SLE") = "0" Then
                                    If dateVisisbili Then
                                    imgAccettaDate.Visible = True
                                    Else
                                        imgAccettaDate.Visible = False
                                    End If
                                Else
                                    imgAccettaDate.Visible = False
                                End If
                            End If
                        End If
                        myReader5.Close()
                    Else
                        imgAccettaDate.Visible = False
                    End If
                    myReader4.Close()


                    lblLinkPDF.Text = "<a href='#' onclick='javascript:StampaOrdine();'>Clicca per Visualizzare</a>"
                    If Session.Item("MOD_FORNITORI_SLE") = "0" Then
                        lblLinkODL.Text = "<a href='#' onclick='javascript:StampaODL();'>Clicca per Visualizzare</a>"
                    Else
                        lblLinkODL.Text = ""
                    End If


                    Session.Add("ID", par.IfNull(myReader1("ID"), "-1"))
                    indiceM.Value = par.IfNull(myReader1("ID"), "-1")
                Else

                End If
                myReader1.Close()
            Else

                lblOrdine.Text = "---"
            End If

            If indiceSegnalazione <> "" Then
                par.cmd.CommandText = "select SEGNALAZIONI.*,RAPPORTI_UTENZA.COD_CONTRATTO,(SELECT COGNOME FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS COGNOME_INT,(SELECT NOME FROM SISCOM_MI.ANAGRAFICA,SISCOM_MI.SOGGETTI_CONTRATTUALI WHERE ANAGRAFICA.ID=SOGGETTI_CONTRATTUALI.ID_ANAGRAFICA AND SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' AND SOGGETTI_CONTRATTUALI.ID_CONTRATTO=RAPPORTI_UTENZA.ID) AS NOME_INT from SISCOM_MI.RAPPORTI_UTENZA,siscom_mi.SEGNALAZIONI where RAPPORTI_UTENZA.ID(+)=SEGNALAZIONI.ID_CONTRATTO AND SEGNALAZIONI.id=" & indiceSegnalazione
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    lblsegnalazione.Text = par.IfNull(myReader1("ID"), "") & " del " & par.FormattaData(Mid(par.IfNull(myReader1("DATA_ORA_RICHIESTA"), ""), 1, 8))
                    lblCodicecontratto.Text = par.IfNull(myReader1("COD_CONTRATTO"), "---")
                    lblCognomeIntestatario.Text = par.IfNull(myReader1("COGNOME_INT"), "---")
                    lblNomeIntestatario.Text = par.IfNull(myReader1("NOME_INT"), "---")

                    If par.IfNull(myReader1("ID_UNITA"), 0) <> 0 Then
                        par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,scale_edifici.descrizione as scala,UNITA_IMMOBILIARI.INTERNO,UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,INDIRIZZI.DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA AS INDIRIZZO FROM SISCOM_MI.TIPO_LIVELLO_PIANO,siscom_mi.scale_edifici,SISCOM_MI.INDIRIZZi,SISCOM_MI.UNITA_IMMOBILIARI WHERE TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND scale_edifici.id(+)=unita_immobiliari.id_scala and INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=" & par.IfNull(myReader1("ID_UNITA"), 0)
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblCodiceUnita.Text = par.IfNull(myReader2("COD_UNITA_IMMOBILIARE"), "---")
                            lblIndirizzo.Text = par.IfNull(myReader2("INDIRIZZO"), "---")
                            lblScala.Text = par.IfNull(myReader2("SCALA"), "---")
                            lblInterno.Text = par.IfNull(myReader2("INTERNO"), "---")
                            lblPiano.Text = par.IfNull(myReader2("PIANO"), "---")
                        End If
                        myReader2.Close()
                    Else
                        lblCodiceUnita.Text = "---"
                    End If

                    If par.IfNull(myReader1("ID_EDIFICIO"), 0) <> 0 Then
                        par.cmd.CommandText = "SELECT EDIFICI.DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID=" & par.IfNull(myReader1("ID_EDIFICIO"), 0)
                        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader2.Read Then
                            lblEdificio.Text = par.IfNull(myReader2("DENOMINAZIONE"), "---")
                        End If
                        myReader2.Close()
                    End If

                    lblrichiesta.Text = par.IfNull(myReader1("DESCRIZIONE_RIC"), "---")
                End If
                myReader1.Close()

            Else
                lblsegnalazione.Text = "---"
                RadTabStrip1.Tabs(1).Visible = False
            End If



            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Fornitori - CaricaIntervento - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Property sStrSqlPreventivi() As String
        Get
            If Not (ViewState("par_sStrSqlPreventivi") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlPreventivi"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlPreventivi") = value
        End Set
    End Property

    Protected Sub RadGridPreventivi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridPreventivi.NeedDataSource
        If sStrSqlPreventivi <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSqlPreventivi)
        End If
    End Sub

    Private Sub CaricaPreventivi(ByVal sIndice As String)
        sStrSqlPreventivi = "select SEGNALAZIONI_FO_PREVENTIVI.ID,SEGNALAZIONI_FO_PREVENTIVI.DESCRIZIONE,(SELECT TO_CHAR(SUM(NVL(IMPORTO,0)),'9G999G999G999G999G990D99') FROM SISCOM_MI.SEGNALAZIONI_FO_PREV_DETT WHERE ID_PREVENTIVO=SEGNALAZIONI_FO_PREVENTIVI.ID) AS IMPORTO,SEGNALAZIONI_FO_PREVENTIVI.NUMERO,substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,7,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,5,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_PR,1,4) AS DATA_PR,substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,7,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,5,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_INIZIO,1,4) AS DATA_INIZIO,substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,7,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,5,2)||'/'||substr(SEGNALAZIONI_FO_PREVENTIVI.DATA_FINE,1,4) AS DATA_FINE,SEGNALAZIONI_FO_PREV_STATI.DESCRIZIONE AS STATO from SISCOM_MI.SEGNALAZIONI_FO_PREVENTIVI,SISCOM_MI.SEGNALAZIONI_FO_PREV_STATI,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE SEGNALAZIONI_FO_PREV_STATI.ID (+)=SEGNALAZIONI_FO_PREVENTIVI.ID_STATO AND SEGNALAZIONI_FO_PREVENTIVI.ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE=" & sIndice & " order by data_pr"
    End Sub

    Protected Sub dgvInterventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvInterventi.NeedDataSource
        If sStrSqlInt <> "" Then
            TryCast(sender, RadGrid).DataSource = par.getDataTableGrid(sStrSqlInt)
        End If
    End Sub

    Public Property sStrSqlAllegati() As String
        Get
            If Not (ViewState("par_sStrSqlAllegati") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlAllegati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlAllegati") = value
        End Set
    End Property

    Private Sub CaricaAllegati(ByVal sIndice As String)
        sStrSqlAllegati = "select operatori.operatore,SEGNALAZIONI_FO_ALLEGATI.ID,TO_CHAR(TO_DATE(SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,9,2)||':'||SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,11,2) AS DATA_ORA,SEGNALAZIONI_FO_ALLEGATI.DESCRIZIONE,(CASE WHEN NOME_FILE IS NOT NULL THEN replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/FORNITORI/'||NOME_FILE||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') ELSE '' END) AS NOME_FILE,SEGNALAZIONI_FO_ALL_TIPI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI,SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI,SISCOM_MI.SEGNALAZIONI_FORNITORI,operatori WHERE operatori.id(+)=SEGNALAZIONI_FO_ALLEGATI.id_operatore and SEGNALAZIONI_FO_ALL_TIPI.ID(+)=SEGNALAZIONI_FO_ALLEGATI.ID_TIPO AND SEGNALAZIONI_FO_ALLEGATI.ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE=" & sIndice & " ORDER BY DATA_ORA DESC"
    End Sub

    Private Sub CaricaIrregolarita()
        sStrSqlIrregolarita = "select DECODE(SEGNALAZIONI_FO_IRR.VISIBILE,0,'NO',1,'SI') AS VISIBILE,SEGNALAZIONI_FO_IRR.ID,SEGNALAZIONI_FO_IRR.ID_TIPO,TO_CHAR(TO_DATE(SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,9,2)||':'||SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,11,2) AS DATA_ORA,SEGNALAZIONI_FO_IRR.DESCRIZIONE,SEGNALAZIONI_FO_TIPI_IRR.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.SEGNALAZIONI_FO_IRR,SISCOM_MI.SEGNALAZIONI_FO_TIPI_IRR WHERE SEGNALAZIONI_FO_TIPI_IRR.ID(+)=SEGNALAZIONI_FO_IRR.ID_TIPO AND ID_SEGNALAZIONE=" & sIndiceIntervento & " ORDER BY DATA_ORA DESC"

    End Sub

    Private Function CaricaEventi()
        Try
            sStrSqlEventi = "select OPERATORI.OPERATORE,TO_CHAR (TO_DATE (substr(data_ora,1,8), 'yyyymmdd'),'dd/mm/yyyy') AS DATA_EVENTO,MOTIVAZIONE,TAB_EVENTI.DESCRIZIONE AS EVENTO from OPERATORI,SISCOM_MI.TAB_EVENTI,siscom_mi.EVENTI_SEGNALAZIONI_FO WHERE OPERATORI.ID=EVENTI_SEGNALAZIONI_FO.ID_OPERATORE AND TAB_EVENTI.COD=EVENTI_SEGNALAZIONI_FO.COD_EVENTO AND ID_SEGNALAZIONE_FO=" & sIndiceIntervento & " ORDER BY DATA_ORA DESC "
        Catch ex As Exception

        End Try
    End Function

    Protected Sub RadGridAllegati_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridAllegati.NeedDataSource
        Try
            If sStrSqlAllegati <> "" Then
                RadGridAllegati.DataSource = par.getDataTableGrid(sStrSqlAllegati)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Eventi - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Public Sub btnVisDettagli_Click(sender As Object, e As System.EventArgs) Handles btnVisDettagli.Click
        If indiceM.Value <> "" Then
            Try
                CaricaDati(indiceM.Value)
                CaricaPreventivi(indiceM.Value)
                CaricaAllegati(indiceM.Value)
                CaricaIrregolarita()
                CaricaEventi()
                dgvInterventi.Rebind()
                RadGridPreventivi.Rebind()
                RadGridAllegati.Rebind()
                RadGridIrregolarita.Rebind()
                dgvEventi.Rebind()
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza: Fornitori - VisualizzaDettagli - " & ex.Message)
                Response.Redirect("../Errore.aspx", False)
            End Try
        Else
            'CType(Master.FindControl("RadWindowManager1"), RadWindowManager).RadAlert("Nessun dettaglio da visualizzare!", 300, 150, "Info", Nothing, Nothing)
            VisualizzaAlert("Nessun dettaglio da visualizzare", 2)
        End If
    End Sub

    Private Function CaricaInterventi(ByVal vIdManutenzione As Long) As String
        Try
            CaricaInterventi = ""
            Dim sSQL_DettaglioIMPIANTO As String = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                    & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                            & "(select  CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                            & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                    & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END " _
                                            & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                    & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                & " ELSE '' " _
                                & " END) as DETTAGLIO "
            sStrSqlInt = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                   & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(SELECT DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA FROM SISCOM_MI.INDIRIZZI,SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=IMPIANTI.ID_COMPLESSO AND INDIRIZZI.ID=COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO) AS INDIRIZZO_BENE " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                   & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA) AS INDIRIZZO_BENE " _
                   & " from SISCOM_MI.INDIRIZZI,SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) AND INDIRIZZI.ID(+)=COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                  & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA) AS INDIRIZZO_BENE " _
                  & " from SISCOM_MI.INDIRIZZI,SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) AND INDIRIZZI.ID(+)=EDIFICI.ID_INDIRIZZO_PRINCIPALE " _
            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                  & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,(DESCRIZIONE||' '||CIVICO||' '||CAP||' '||LOCALITA) AS INDIRIZZO_BENE " _
                  & " from  SISCOM_MI.INDIRIZZI,SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) AND INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                   & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO,'' AS INDIRIZZO_BENE " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) "

        Catch ex As Exception
            CaricaInterventi = "Errore " & ex.Message
        End Try

    End Function

    Public Property sStrSqlIrregolarita() As String
        Get
            If Not (ViewState("par_sStrSqlIrregolarita") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlIrregolarita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlIrregolarita") = value
        End Set
    End Property

    Public Property sStrSqlEventi() As String
        Get
            If Not (ViewState("par_sStrSqlEventi") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlEventi"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlEventi") = value
        End Set
    End Property

    Public Property sIndiceIntervento() As String
        Get
            If Not (ViewState("par_sIndiceIntervento") Is Nothing) Then
                Return CStr(ViewState("par_sIndiceIntervento"))
            Else
                Return "-1"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIndiceIntervento") = value
        End Set
    End Property


    Protected Sub RadGridIrregolarita_DeleteCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridIrregolarita.DeleteCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri(True)

            par.cmd.CommandText = "delete from SISCOM_MI.SEGNALAZIONI_FO_IRR where id=" & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndiceIntervento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F269','" & par.PulisciStrSql(e.Item.Cells(5).Text) & "')"
            par.cmd.ExecuteNonQuery()

            connData.chiudi(True)
            VisualizzaAlert("Operazione effettuata", 1)

            RadGridIrregolarita.Rebind()
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - EliminaIrregolarita - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_InsertCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridIrregolarita.InsertCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)

            connData.apri(True)

            Dim Descrizione As String = ""
            Descrizione = CType(userControl.FindControl("txtDescrizioneIrregolarita"), RadTextBox).Text

            Dim tipoDoc As String = ""
            Dim DescrtipoDoc As String = ""
            Dim Visibile As String = ""
            If CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue <> "-1" Then
                tipoDoc = par.insDbValue(CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue, True, False)
            Else
                tipoDoc = "NULL"
            End If
            If CType(userControl.FindControl("cmbVisibilita"), RadComboBox).SelectedValue <> "-1" Then
                Visibile = par.insDbValue(CType(userControl.FindControl("cmbVisibilita"), RadComboBox).SelectedValue, True, False)
            Else
                Visibile = ""
            End If
            If tipoDoc <> "''" Then
                If Visibile <> "''" Then
                    If CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedItem.Text <> "-1" Then
                        DescrtipoDoc = par.insDbValue(CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedItem.Text, True, False)
                    Else
                        DescrtipoDoc = ""
                    End If
                    par.cmd.CommandText = "Insert into SISCOM_MI.SEGNALAZIONI_FO_IRR (ID,ID_SEGNALAZIONE, DATA_ORA, ID_TIPO, DESCRIZIONE,VISIBILE) Values (SISCOM_MI.SEQ_SEGNALAZIONI_FO_IRR.NEXTVAL," & sIndiceIntervento & ", '" & Format(Now, "yyyyMMddHHmmss") & "'," & tipoDoc & ", '" & par.PulisciStrSql(Descrizione) & "'," & Visibile & ")"
                    par.cmd.ExecuteNonQuery()

                    'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndiceIntervento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F268','" & par.PulisciStrSql(DescrtipoDoc & " - " & Descrizione) & "')"
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndiceIntervento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F268','')"
                    par.cmd.ExecuteNonQuery()

                    connData.chiudi(True)

                    VisualizzaAlert("Operazione effettuata", 1)
                    RadGridIrregolarita.Rebind()
                Else
                    VisualizzaAlert("Indicare se visibile o meno", 2)
                End If
            Else
                VisualizzaAlert("Scegliere una tipologia di non conformità valida", 2)
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Inserisci non conformità - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridIrregolarita.ItemCommand
        Try
            If e.CommandName = RadGrid.InitInsertCommandName Then '"Add new" button clicked

                Dim editColumn As GridEditCommandColumn = CType(RadGridIrregolarita.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                editColumn.Visible = False

            ElseIf (e.CommandName = RadGrid.RebindGridCommandName AndAlso e.Item.OwnerTableView.IsItemInserted) Then
                e.Canceled = True
            Else
                Dim editColumn As GridEditCommandColumn = CType(RadGridIrregolarita.MasterTableView.GetColumn("EditCommandColumn"), GridEditCommandColumn)
                If Not editColumn.Visible Then
                    editColumn.Visible = True
                End If
            End If

        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - non conformità - ItemCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_ItemDeleted(sender As Object, e As Telerik.Web.UI.GridDeletedEventArgs) Handles RadGridIrregolarita.ItemDeleted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True

            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - ItemDeleted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_ItemInserted(sender As Object, e As Telerik.Web.UI.GridInsertedEventArgs) Handles RadGridIrregolarita.ItemInserted
        Try
            If Not e.Exception Is Nothing Then
                e.ExceptionHandled = True
                e.KeepInInsertMode = False
                'DisplayMessage(True, "Employee cannot be inserted. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee inserted")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - ItemInserted - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_ItemUpdated(sender As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles RadGridIrregolarita.ItemUpdated
        Try
            If Not e.Exception Is Nothing Then
                e.KeepInEditMode = True
                e.ExceptionHandled = True
                'DisplayMessage(True, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " cannot be updated. Reason: " + e.Exception.Message)
            Else
                'DisplayMessage(False, "Employee " + e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("EmployeeID").ToString() + " updated")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - ItemUpdated - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridIrregolarita.NeedDataSource
        Try
            If sStrSqlIrregolarita <> "" Then
                RadGridIrregolarita.DataSource = par.getDataTableGrid(sStrSqlIrregolarita)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadGridIrregolarita_PreRender(sender As Object, e As System.EventArgs) Handles RadGridIrregolarita.PreRender
        'ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "ridimensiona", "Ridimensiona();", True)
    End Sub

    Protected Sub RadGridIrregolarita_UpdateCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridIrregolarita.UpdateCommand
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            Dim userControl As GridEditableItem = CType(e.Item, GridEditableItem)
            connData.apri(True)

            Dim Descrizione As String = ""
            Descrizione = CType(userControl.FindControl("txtDescrizioneIrregolarita"), RadTextBox).Text

            Dim tipoDoc As String = ""
            Dim Visibile As String = ""
            If CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue <> "-1" Then
                tipoDoc = par.insDbValue(CType(userControl.FindControl("cmbTipologia"), RadComboBox).SelectedValue, True, False)
            Else
                tipoDoc = "NULL"
            End If
            If CType(userControl.FindControl("cmbVisibilita"), RadComboBox).SelectedValue <> "-1" Then
                Visibile = par.insDbValue(CType(userControl.FindControl("cmbVisibilita"), RadComboBox).SelectedValue, True, False)
            Else
                Visibile = ""
            End If
            If tipoDoc <> "''" Then
                If Visibile <> "''" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FO_IRR SET ID_TIPO=" & tipoDoc & ",DESCRIZIONE='" & par.PulisciStrSql(Descrizione) & "',VISIBILE=" & Visibile & " WHERE  ID = " & e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("ID").ToString()
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi(True)
                    VisualizzaAlert("Operazione effettuata", 1)
                    RadGridIrregolarita.Rebind()
                    RadGridIrregolarita.EditIndexes.Clear()
                Else
                    VisualizzaAlert("Indicare se visibile o meno", 2)
                End If
            Else
                VisualizzaAlert("Scegliere una tipologia di non conformità valida", 2)
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - UpdateCommand - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub OnItemDataBoundHandlerIRR(ByVal sender As Object, ByVal e As GridItemEventArgs)
        Try
            If (TypeOf e.Item Is GridEditFormItem AndAlso e.Item.IsInEditMode) Then
                Dim radCombo1 As RadComboBox = CType(e.Item.FindControl("cmbTipologia"), RadComboBox)

                par.caricaComboTelerik("select id,descrizione from SISCOM_MI.SEGNALAZIONI_FO_TIPI_IRR WHERE UTILIZZABILE=1 order by descrizione desc", radCombo1, "ID", "DESCRIZIONE", True, "-1")
                If e.Item.DataItem.GetType.Name = "DataRowView" Then
                    radCombo1.SelectedValue = par.IfNull(e.Item.DataItem("ID_TIPO"), "-1")
                    If e.Item.DataItem("ID_TIPO") = 0 Then
                        radCombo1.Enabled = False
                    Else
                        radCombo1.Enabled = True
                    End If
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Non Conformità - OnItemDataBoundHandlerIRR - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub dgvEventi_ExportCellFormatting(sender As Object, e As Telerik.Web.UI.ExportCellFormattingEventArgs) Handles dgvEventi.ExportCellFormatting

    End Sub

    Protected Sub dgvEventi_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles dgvEventi.NeedDataSource
        Try
            If sStrSqlEventi <> "" Then
                dgvEventi.DataSource = par.getDataTableGrid(sStrSqlEventi)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Fornitori - Dettaglio Ordini Eventi - NeedDataSource - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Protected Sub RadAgenda_PreRender(sender As Object, e As System.EventArgs) Handles RadAgenda.PreRender
        lblRegolari.Text = "ORDINE REGOLARE (" & iTotRegolari / 3 & " nella lista)"
        lblFuoriTempo.Text = "ORDINE FUORI TEMPO (" & iTotFuoriTempo / 3 & " nella lista)"
        lblNumeroOrdini.Text = " - " & (iTotRegolari / 3) + (iTotFuoriTempo / 3) & " ordine/i in elenco"
    End Sub

    Protected Sub btnPrContab_Click(sender As Object, e As System.EventArgs) Handles btnPrContab.Click
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri()
            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET FL_PR_CONTAB=1,ID_STATO=9 WHERE ID=" & sIndiceIntervento
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndiceIntervento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F271','')"
            par.cmd.ExecuteNonQuery()
            connData.chiudi()
            btnPrContab.Visible = False
            'RadWindowManager1.RadAlert("Operazione effettuata!", 300, 150, "Info", Nothing, Nothing)
            VisualizzaAlert("Operazione effettuata", 1)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Richiesta Pr. Contab. - " & ex.Message)
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

    Protected Sub RadButtonConfermaModifica_Click(sender As Object, e As EventArgs) Handles RadButtonConfermaModifica.Click
        Try
            Me.connData = New CM.datiConnessione(par, False, False)
            connData.apri()
            If RadioButtonRifiuta.Checked = True Then
                par.cmd.CommandText = "Insert into SISCOM_MI.SEGNALAZIONI_FO_IRR (ID,ID_SEGNALAZIONE, DATA_ORA, ID_TIPO, DESCRIZIONE,RIFERIMENTO) Values (SISCOM_MI.SEQ_SEGNALAZIONI_FO_IRR.NEXTVAL," & sIndiceIntervento & ", '" & Format(Now, "yyyyMMddHHmmss") & "',0, 'Le date " & lblDataPGI.Text & " - " & lblDataTDL.Text & " non sono state accettate.','" & par.AggiustaData(lblDataPGI.Text) & par.AggiustaData(lblDataTDL.Text) & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndiceIntervento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F268','" & par.PulisciStrSql("MODIFICA PGI/TDL NON ACCETTATA - " & lblDataPGI.Text & " - " & lblDataTDL.Text) & "')"
                par.cmd.ExecuteNonQuery()

                connData.chiudi()
                CaricaIrregolarita()
                RadGridIrregolarita.Rebind()

            End If
            If RadioButtonAccetta.Checked = True Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI_FO (ID_SEGNALAZIONE_FO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES (" & sIndiceIntervento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','F272','" & par.PulisciStrSql("" & lblDataPGI.Text & " - " & lblDataTDL.Text) & "')"
                par.cmd.ExecuteNonQuery()
                CaricaEventi()
                dgvEventi.Rebind()
                connData.chiudi()
            End If
            imgAccettaDate.Visible = False
            RadButtonConfermaModifica.Visible = False
            VisualizzaAlert("Operazione effettuata", 1)
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Fornitori - Accetta-Rifiuta data PGI/TDL - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class
