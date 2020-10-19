
Partial Class CALL_CENTER_Agenda_DettagliAppuntamenti
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Session.Item("MOD_CALL") = "0" Then
            Response.Write("<script>top.location.href=""../../AccessoNegato.htm""</script>")
            Exit Sub
        End If

        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            TextBoxDataAppuntamento.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            TextBoxDataAppuntamento.Text = par.FormattaData(Request.QueryString("DATA"))
            If Not IsNothing(Request.QueryString("IDS")) Then
                idSegnalazione.Value = Request.QueryString("IDS")
            Else
                idSegnalazione.Value = "-1"
            End If
            lbltitolo.Text = "DETTAGLI APPUNTAMENTI " & par.FormattaData(Request.QueryString("DATA"))
            par.caricaComboBox("SELECT TAB_FILIALI.ID,NOME||'-'||DESCRIZIONE||' '||CIVICO||','||LOCALITA AS NOME FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbFiliale, "ID", "NOME", False)
            par.caricaComboBox("SELECT TAB_FILIALI.ID,NOME||'-'||DESCRIZIONE||' '||CIVICO||','||LOCALITA AS NOME FROM SISCOM_MI.TAB_FILIALI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID=TAB_FILIALI.ID_INDIRIZZO AND TAB_FILIALI.ID>100 AND ID_TIPO_ST=0 ORDER BY NOME", cmbFilialeIns, "ID", "NOME", False)
            par.caricaComboBox("SELECT ID,ORARIO FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ORARIO ASC", cmbOrario, "ID", "ORARIO", False)
            If Not IsNothing(Request.QueryString("FILIALE")) Then
                cmbFiliale.SelectedValue = Request.QueryString("FILIALE")
            End If
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFiliale.SelectedValue & " ORDER BY ID", cmbSportello, "ID", "DESCRIZIONE", False)
            CaricaAppuntamenti()
        End If
        If Session.Item("MOD_CALL_SL") = "1" Then
            soloLettura()
        End If
    End Sub
    Private Sub CaricaAppuntamenti()
        Try
            connData.apri()

            par.cmd.CommandText = "SELECT COGNOME_RS,NOME,TELEFONO1,TELEFONO2,MAIL FROM SISCOM_MI.SEGNALAZIONI WHERE ID=" & idSegnalazione.Value
            Dim LettSegnalazione As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If LettSegnalazione.Read Then
                TextBoxCognome.Text = par.IfNull(LettSegnalazione("COGNOME_RS"), "")
                TextBoxNome.Text = par.IfNull(LettSegnalazione("NOME"), "")
                TextBoxTelefono.Text = par.IfNull(LettSegnalazione("TELEFONO1"), "")
                TextBoxCellulare.Text = par.IfNull(LettSegnalazione("TELEFONO2"), "")
                TextBoxEmail.Text = par.IfNull(LettSegnalazione("MAIL"), "")
                cmbFilialeIns.SelectedValue = cmbFiliale.SelectedValue
            End If
            LettSegnalazione.Close()

            Dim condizioneFiliali As String = ""
            If cmbFiliale.SelectedValue <> "-1" Then
                condizioneFiliali = " AND APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA=" & cmbFiliale.SelectedValue
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPUNTAMENTI_ORARI ORDER BY ORARIO ASC"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim daOrari As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtOrari As New Data.DataTable
            daOrari.Fill(dtOrari)
            daOrari.Dispose()

            Dim dt As New Data.DataTable
            dt.Columns.Clear()
            dt.Columns.Add("ID")
            dt.Columns.Add("NOME")
            dt.Columns.Add("OPERATORE")
            dt.Columns.Add("DATA_INSERIMENTO")
            dt.Columns.Add("DATA_APPUNTAMENTO")
            dt.Columns.Add("ORA_APPUNTAMENTO")
            dt.Columns.Add("APPUNTAMENTO_CON")
            dt.Columns.Add("TELEFONO")
            dt.Columns.Add("CELLULARE")
            dt.Columns.Add("EMAIL")
            dt.Columns.Add("NOTE")
            dt.Columns.Add("SEGNALAZIONE")
            dt.Columns.Add("ELIMINA")
            dt.Columns.Add("FL_ELIMINA")

            Dim riga As Data.DataRow
            Dim LettoreApp As Oracle.DataAccess.Client.OracleDataReader

            For Each elemento As Data.DataRow In dtOrari.Rows
                par.cmd.CommandText = "SELECT APPUNTAMENTI_CALL_CENTER.ID, " _
                    & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_APPUNTAMENTO," _
                    & " OPERATORI.OPERATORE, " _
                    & " TAB_FILIALI.NOME, " _
                    & "'" & elemento.Item("ORARIO") & "' AS ORA_APPUNTAMENTO," _
                    & " TO_CHAR(TO_DATE(SUBSTR(DATA_INSERIMENTO,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2) AS DATA_INSERIMENTO, " _
                    & " APPUNTAMENTI_CALL_CENTER.COGNOME||' '||APPUNTAMENTI_CALL_CENTER.NOME APPUNTAMENTO_CON," _
                    & " TELEFONO," _
                    & " CELLULARE," _
                    & " EMAIL," _
                    & " NOTE,(SELECT DESCRIZIONE_RIC FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID=APPUNTAMENTI_CALL_CENTER.ID_SEGNALAZIONE) AS SEGNALAZIONE,'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA " _
                    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SEPA.OPERATORI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                    & " WHERE SEPA.OPERATORI.ID=APPUNTAMENTI_CALL_CENTER.ID_OPERATORE " _
                    & " AND TAB_FILIALI.ID=APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA " _
                    & " AND DATA_APPUNTAMENTO = '" & Request.QueryString("DATA") & "'" _
                    & condizioneFiliali _
                    & " AND APPUNTAMENTI_SPORTELLI.ID=APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO " _
                    & " AND APPUNTAMENTI_SPORTELLI.INDICE=1 " _
                    & " AND ID_ORARIO=" & elemento.Item("ID") _
                    & " ORDER BY DATA_ELIMINAZIONE ASC,ORA_APPUNTAMENTO ASC"

                LettoreApp = par.cmd.ExecuteReader
                Dim contFLelimina As Integer = 0
                Dim cont As Integer = 0
                If LettoreApp.HasRows Then
                    While LettoreApp.Read
                        cont += 1
                        riga = dt.NewRow
                        riga.Item("ID") = par.IfNull(LettoreApp("ID"), "")
                        riga.Item("NOME") = par.IfNull(LettoreApp("NOME"), "")
                        riga.Item("OPERATORE") = par.IfNull(LettoreApp("OPERATORE"), "")
                        riga.Item("DATA_INSERIMENTO") = par.IfNull(LettoreApp("DATA_INSERIMENTO"), "")
                        riga.Item("DATA_APPUNTAMENTO") = par.IfNull(LettoreApp("DATA_APPUNTAMENTO"), "")
                        riga.Item("ORA_APPUNTAMENTO") = par.IfNull(LettoreApp("ORA_APPUNTAMENTO"), "")
                        riga.Item("APPUNTAMENTO_CON") = par.IfNull(LettoreApp("APPUNTAMENTO_CON"), "")
                        riga.Item("TELEFONO") = par.IfNull(LettoreApp("TELEFONO"), "")
                        riga.Item("CELLULARE") = par.IfNull(LettoreApp("CELLULARE"), "")
                        riga.Item("EMAIL") = par.IfNull(LettoreApp("EMAIL"), "")
                        riga.Item("NOTE") = par.IfNull(LettoreApp("NOTE"), "")
                        riga.Item("SEGNALAZIONE") = par.IfNull(LettoreApp("SEGNALAZIONE"), "")
                        riga.Item("ELIMINA") = par.IfNull(LettoreApp("ELIMINA"), "")
                        riga.Item("FL_ELIMINA") = par.IfNull(LettoreApp("FL_ELIMINA"), "")
                        dt.Rows.Add(riga)
                        If par.IfNull(LettoreApp("FL_ELIMINA"), "") = "1" Then
                            contFLelimina += 1
                        End If
                    End While
                    If cont = contFLelimina Then
                        riga = dt.NewRow
                        riga.Item("ID") = ""
                        riga.Item("NOME") = ""
                        riga.Item("OPERATORE") = ""
                        riga.Item("DATA_INSERIMENTO") = ""
                        riga.Item("DATA_APPUNTAMENTO") = ""
                        riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                        riga.Item("APPUNTAMENTO_CON") = ""
                        riga.Item("TELEFONO") = ""
                        riga.Item("CELLULARE") = ""
                        riga.Item("EMAIL") = ""
                        riga.Item("NOTE") = ""
                        riga.Item("SEGNALAZIONE") = ""
                        riga.Item("ELIMINA") = ""
                        riga.Item("FL_ELIMINA") = ""
                        dt.Rows.Add(riga)
                    End If
                Else
                    riga = dt.NewRow
                    riga.Item("ID") = ""
                    riga.Item("NOME") = ""
                    riga.Item("OPERATORE") = ""
                    riga.Item("DATA_INSERIMENTO") = ""
                    riga.Item("DATA_APPUNTAMENTO") = ""
                    riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                    riga.Item("APPUNTAMENTO_CON") = ""
                    riga.Item("TELEFONO") = ""
                    riga.Item("CELLULARE") = ""
                    riga.Item("EMAIL") = ""
                    riga.Item("NOTE") = ""
                    riga.Item("SEGNALAZIONE") = ""
                    riga.Item("ELIMINA") = ""
                    riga.Item("FL_ELIMINA") = ""
                    dt.Rows.Add(riga)
                End If
                LettoreApp.Close()
            Next

            DataGridAppuntamentiSportello1.DataSource = dt
            DataGridAppuntamentiSportello1.DataBind()
            dt.Rows.Clear()

            For Each elemento As Data.DataRow In dtOrari.Rows
                par.cmd.CommandText = "SELECT APPUNTAMENTI_CALL_CENTER.ID, " _
                    & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_APPUNTAMENTO," _
                    & " OPERATORI.OPERATORE, " _
                    & " TAB_FILIALI.NOME, " _
                    & "'" & elemento.Item("ORARIO") & "' AS ORA_APPUNTAMENTO," _
                    & " TO_CHAR(TO_DATE(SUBSTR(DATA_INSERIMENTO,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2) AS DATA_INSERIMENTO, " _
                    & " APPUNTAMENTI_CALL_CENTER.COGNOME||' '||APPUNTAMENTI_CALL_CENTER.NOME APPUNTAMENTO_CON," _
                    & " TELEFONO," _
                    & " CELLULARE," _
                    & " EMAIL," _
                    & " NOTE,(SELECT DESCRIZIONE_RIC FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID=APPUNTAMENTI_CALL_CENTER.ID_SEGNALAZIONE) AS SEGNALAZIONE,'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA " _
                    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SEPA.OPERATORI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                    & " WHERE SEPA.OPERATORI.ID=APPUNTAMENTI_CALL_CENTER.ID_OPERATORE " _
                    & " AND TAB_FILIALI.ID=APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA " _
                    & " AND DATA_APPUNTAMENTO = '" & Request.QueryString("DATA") & "'" _
                    & condizioneFiliali _
                    & " AND APPUNTAMENTI_SPORTELLI.ID=APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO " _
                    & " AND APPUNTAMENTI_SPORTELLI.INDICE=2 " _
                    & " AND ID_ORARIO=" & elemento.Item("ID") _
                    & " ORDER BY DATA_ELIMINAZIONE ASC,ORA_APPUNTAMENTO ASC"

                LettoreApp = par.cmd.ExecuteReader
                Dim contFLelimina As Integer = 0
                Dim cont As Integer = 0
                If LettoreApp.HasRows Then
                    While LettoreApp.Read
                        cont += 1
                        riga = dt.NewRow
                        riga.Item("ID") = par.IfNull(LettoreApp("ID"), "")
                        riga.Item("NOME") = par.IfNull(LettoreApp("NOME"), "")
                        riga.Item("OPERATORE") = par.IfNull(LettoreApp("OPERATORE"), "")
                        riga.Item("DATA_INSERIMENTO") = par.IfNull(LettoreApp("DATA_INSERIMENTO"), "")
                        riga.Item("DATA_APPUNTAMENTO") = par.IfNull(LettoreApp("DATA_APPUNTAMENTO"), "")
                        riga.Item("ORA_APPUNTAMENTO") = par.IfNull(LettoreApp("ORA_APPUNTAMENTO"), "")
                        riga.Item("APPUNTAMENTO_CON") = par.IfNull(LettoreApp("APPUNTAMENTO_CON"), "")
                        riga.Item("TELEFONO") = par.IfNull(LettoreApp("TELEFONO"), "")
                        riga.Item("CELLULARE") = par.IfNull(LettoreApp("CELLULARE"), "")
                        riga.Item("EMAIL") = par.IfNull(LettoreApp("EMAIL"), "")
                        riga.Item("NOTE") = par.IfNull(LettoreApp("NOTE"), "")
                        riga.Item("SEGNALAZIONE") = par.IfNull(LettoreApp("SEGNALAZIONE"), "")
                        riga.Item("ELIMINA") = par.IfNull(LettoreApp("ELIMINA"), "")
                        riga.Item("FL_ELIMINA") = par.IfNull(LettoreApp("FL_ELIMINA"), "")
                        dt.Rows.Add(riga)
                        If par.IfNull(LettoreApp("FL_ELIMINA"), "") = "1" Then
                            contFLelimina += 1
                        End If
                    End While
                    If cont = contFLelimina Then
                        riga = dt.NewRow
                        riga.Item("ID") = ""
                        riga.Item("NOME") = ""
                        riga.Item("OPERATORE") = ""
                        riga.Item("DATA_INSERIMENTO") = ""
                        riga.Item("DATA_APPUNTAMENTO") = ""
                        riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                        riga.Item("APPUNTAMENTO_CON") = ""
                        riga.Item("TELEFONO") = ""
                        riga.Item("CELLULARE") = ""
                        riga.Item("EMAIL") = ""
                        riga.Item("NOTE") = ""
                        riga.Item("SEGNALAZIONE") = ""
                        riga.Item("ELIMINA") = ""
                        riga.Item("FL_ELIMINA") = ""
                        dt.Rows.Add(riga)
                    End If
                Else
                    riga = dt.NewRow
                    riga.Item("ID") = ""
                    riga.Item("NOME") = ""
                    riga.Item("OPERATORE") = ""
                    riga.Item("DATA_INSERIMENTO") = ""
                    riga.Item("DATA_APPUNTAMENTO") = ""
                    riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                    riga.Item("APPUNTAMENTO_CON") = ""
                    riga.Item("TELEFONO") = ""
                    riga.Item("CELLULARE") = ""
                    riga.Item("EMAIL") = ""
                    riga.Item("NOTE") = ""
                    riga.Item("SEGNALAZIONE") = ""
                    riga.Item("ELIMINA") = ""
                    riga.Item("FL_ELIMINA") = ""
                    dt.Rows.Add(riga)
                End If
                LettoreApp.Close()

            Next

            DataGridAppuntamentiSportello2.DataSource = dt
            DataGridAppuntamentiSportello2.DataBind()
            dt.Rows.Clear()

            For Each elemento As Data.DataRow In dtOrari.Rows
                par.cmd.CommandText = "SELECT APPUNTAMENTI_CALL_CENTER.ID, " _
                    & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_APPUNTAMENTO," _
                    & " OPERATORI.OPERATORE, " _
                    & " TAB_FILIALI.NOME, " _
                    & "'" & elemento.Item("ORARIO") & "' AS ORA_APPUNTAMENTO," _
                    & " TO_CHAR(TO_DATE(SUBSTR(DATA_INSERIMENTO,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2) AS DATA_INSERIMENTO, " _
                    & " APPUNTAMENTI_CALL_CENTER.COGNOME||' '||APPUNTAMENTI_CALL_CENTER.NOME APPUNTAMENTO_CON," _
                    & " TELEFONO," _
                    & " CELLULARE," _
                    & " EMAIL," _
                    & " NOTE,(SELECT DESCRIZIONE_RIC FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID=APPUNTAMENTI_CALL_CENTER.ID_SEGNALAZIONE) AS SEGNALAZIONE,'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA " _
                    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SEPA.OPERATORI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                    & " WHERE SEPA.OPERATORI.ID=APPUNTAMENTI_CALL_CENTER.ID_OPERATORE " _
                    & " AND TAB_FILIALI.ID=APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA " _
                    & " AND DATA_APPUNTAMENTO = '" & Request.QueryString("DATA") & "'" _
                    & condizioneFiliali _
                    & " AND APPUNTAMENTI_SPORTELLI.ID=APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO " _
                    & " AND APPUNTAMENTI_SPORTELLI.INDICE=3 " _
                    & " AND ID_ORARIO=" & elemento.Item("ID") _
                    & " ORDER BY DATA_ELIMINAZIONE ASC,ORA_APPUNTAMENTO ASC"

                LettoreApp = par.cmd.ExecuteReader
                Dim contFLelimina As Integer = 0
                Dim cont As Integer = 0
                If LettoreApp.HasRows Then
                    While LettoreApp.Read
                        cont += 1
                        riga = dt.NewRow
                        riga.Item("ID") = par.IfNull(LettoreApp("ID"), "")
                        riga.Item("NOME") = par.IfNull(LettoreApp("NOME"), "")
                        riga.Item("OPERATORE") = par.IfNull(LettoreApp("OPERATORE"), "")
                        riga.Item("DATA_INSERIMENTO") = par.IfNull(LettoreApp("DATA_INSERIMENTO"), "")
                        riga.Item("DATA_APPUNTAMENTO") = par.IfNull(LettoreApp("DATA_APPUNTAMENTO"), "")
                        riga.Item("ORA_APPUNTAMENTO") = par.IfNull(LettoreApp("ORA_APPUNTAMENTO"), "")
                        riga.Item("APPUNTAMENTO_CON") = par.IfNull(LettoreApp("APPUNTAMENTO_CON"), "")
                        riga.Item("TELEFONO") = par.IfNull(LettoreApp("TELEFONO"), "")
                        riga.Item("CELLULARE") = par.IfNull(LettoreApp("CELLULARE"), "")
                        riga.Item("EMAIL") = par.IfNull(LettoreApp("EMAIL"), "")
                        riga.Item("NOTE") = par.IfNull(LettoreApp("NOTE"), "")
                        riga.Item("SEGNALAZIONE") = par.IfNull(LettoreApp("SEGNALAZIONE"), "")
                        riga.Item("ELIMINA") = par.IfNull(LettoreApp("ELIMINA"), "")
                        riga.Item("FL_ELIMINA") = par.IfNull(LettoreApp("FL_ELIMINA"), "")
                        dt.Rows.Add(riga)
                        If par.IfNull(LettoreApp("FL_ELIMINA"), "") = "1" Then
                            contFLelimina += 1
                        End If
                    End While
                    If cont = contFLelimina Then
                        riga = dt.NewRow
                        riga.Item("ID") = ""
                        riga.Item("NOME") = ""
                        riga.Item("OPERATORE") = ""
                        riga.Item("DATA_INSERIMENTO") = ""
                        riga.Item("DATA_APPUNTAMENTO") = ""
                        riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                        riga.Item("APPUNTAMENTO_CON") = ""
                        riga.Item("TELEFONO") = ""
                        riga.Item("CELLULARE") = ""
                        riga.Item("EMAIL") = ""
                        riga.Item("NOTE") = ""
                        riga.Item("SEGNALAZIONE") = ""
                        riga.Item("ELIMINA") = ""
                        riga.Item("FL_ELIMINA") = ""
                        dt.Rows.Add(riga)
                    End If
                Else
                    riga = dt.NewRow
                    riga.Item("ID") = ""
                    riga.Item("NOME") = ""
                    riga.Item("OPERATORE") = ""
                    riga.Item("DATA_INSERIMENTO") = ""
                    riga.Item("DATA_APPUNTAMENTO") = ""
                    riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                    riga.Item("APPUNTAMENTO_CON") = ""
                    riga.Item("TELEFONO") = ""
                    riga.Item("CELLULARE") = ""
                    riga.Item("EMAIL") = ""
                    riga.Item("NOTE") = ""
                    riga.Item("SEGNALAZIONE") = ""
                    riga.Item("ELIMINA") = ""
                    riga.Item("FL_ELIMINA") = ""
                    dt.Rows.Add(riga)
                End If
                LettoreApp.Close()

            Next

            DataGridAppuntamentiSportello3.DataSource = dt
            DataGridAppuntamentiSportello3.DataBind()
            dt.Rows.Clear()

            For Each elemento As Data.DataRow In dtOrari.Rows
                par.cmd.CommandText = "SELECT APPUNTAMENTI_CALL_CENTER.ID, " _
                    & " TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA_APPUNTAMENTO," _
                    & " OPERATORI.OPERATORE, " _
                    & " TAB_FILIALI.NOME, " _
                    & "'" & elemento.Item("ORARIO") & "' AS ORA_APPUNTAMENTO," _
                    & " TO_CHAR(TO_DATE(SUBSTR(DATA_INSERIMENTO,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' '||SUBSTR(DATA_INSERIMENTO,9,2)||':'||SUBSTR(DATA_INSERIMENTO,11,2) AS DATA_INSERIMENTO, " _
                    & " APPUNTAMENTI_CALL_CENTER.COGNOME||' '||APPUNTAMENTI_CALL_CENTER.NOME APPUNTAMENTO_CON," _
                    & " TELEFONO," _
                    & " CELLULARE," _
                    & " EMAIL," _
                    & " NOTE,(SELECT DESCRIZIONE_RIC FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID=APPUNTAMENTI_CALL_CENTER.ID_SEGNALAZIONE) AS SEGNALAZIONE,'Eliminata dall''operatore '||(SELECT OPERATORE FROM SEPA.OPERATORI A WHERE A.ID=APPUNTAMENTI_cALL_cENTER.ID_OPERATORE_ELIMINAZIONE)||' il '||TO_CHAR(TO_DATE(SUBSTR(DATA_ELIMINAZIONE,1,8),'yyyyMMdd'),'dd/MM/yyyy')||' alle ore '||SUBSTR(DATA_ELIMINAZIONE,9,2)||':'||SUBSTR(DATA_ELIMINAZIONE,11,2) AS ELIMINA,CASE WHEN DATA_ELIMINAZIONE IS NULL THEN 0 ELSE 1 END AS FL_ELIMINA " _
                    & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SEPA.OPERATORI,SISCOM_MI.TAB_FILIALI,SISCOM_MI.APPUNTAMENTI_SPORTELLI " _
                    & " WHERE SEPA.OPERATORI.ID=APPUNTAMENTI_CALL_CENTER.ID_OPERATORE " _
                    & " AND TAB_FILIALI.ID=APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA " _
                    & " AND DATA_APPUNTAMENTO = '" & Request.QueryString("DATA") & "'" _
                    & condizioneFiliali _
                    & " AND APPUNTAMENTI_SPORTELLI.ID=APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO " _
                    & " AND APPUNTAMENTI_SPORTELLI.INDICE=4 " _
                    & " AND ID_ORARIO=" & elemento.Item("ID") _
                    & " ORDER BY DATA_ELIMINAZIONE ASC,ORA_APPUNTAMENTO ASC"

                LettoreApp = par.cmd.ExecuteReader
                Dim contFLelimina As Integer = 0
                Dim cont As Integer = 0
                If LettoreApp.HasRows Then
                    While LettoreApp.Read
                        cont += 1
                        riga = dt.NewRow
                        riga.Item("ID") = par.IfNull(LettoreApp("ID"), "")
                        riga.Item("NOME") = par.IfNull(LettoreApp("NOME"), "")
                        riga.Item("OPERATORE") = par.IfNull(LettoreApp("OPERATORE"), "")
                        riga.Item("DATA_INSERIMENTO") = par.IfNull(LettoreApp("DATA_INSERIMENTO"), "")
                        riga.Item("DATA_APPUNTAMENTO") = par.IfNull(LettoreApp("DATA_APPUNTAMENTO"), "")
                        riga.Item("ORA_APPUNTAMENTO") = par.IfNull(LettoreApp("ORA_APPUNTAMENTO"), "")
                        riga.Item("APPUNTAMENTO_CON") = par.IfNull(LettoreApp("APPUNTAMENTO_CON"), "")
                        riga.Item("TELEFONO") = par.IfNull(LettoreApp("TELEFONO"), "")
                        riga.Item("CELLULARE") = par.IfNull(LettoreApp("CELLULARE"), "")
                        riga.Item("EMAIL") = par.IfNull(LettoreApp("EMAIL"), "")
                        riga.Item("NOTE") = par.IfNull(LettoreApp("NOTE"), "")
                        riga.Item("SEGNALAZIONE") = par.IfNull(LettoreApp("SEGNALAZIONE"), "")
                        riga.Item("ELIMINA") = par.IfNull(LettoreApp("ELIMINA"), "")
                        riga.Item("FL_ELIMINA") = par.IfNull(LettoreApp("FL_ELIMINA"), "")
                        dt.Rows.Add(riga)
                        If par.IfNull(LettoreApp("FL_ELIMINA"), "") = "1" Then
                            contFLelimina += 1
                        End If
                    End While
                    If cont = contFLelimina Then
                        riga = dt.NewRow
                        riga.Item("ID") = ""
                        riga.Item("NOME") = ""
                        riga.Item("OPERATORE") = ""
                        riga.Item("DATA_INSERIMENTO") = ""
                        riga.Item("DATA_APPUNTAMENTO") = ""
                        riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                        riga.Item("APPUNTAMENTO_CON") = ""
                        riga.Item("TELEFONO") = ""
                        riga.Item("CELLULARE") = ""
                        riga.Item("EMAIL") = ""
                        riga.Item("NOTE") = ""
                        riga.Item("SEGNALAZIONE") = ""
                        riga.Item("ELIMINA") = ""
                        riga.Item("FL_ELIMINA") = ""
                        dt.Rows.Add(riga)
                    End If
                Else
                    riga = dt.NewRow
                    riga.Item("ID") = ""
                    riga.Item("NOME") = ""
                    riga.Item("OPERATORE") = ""
                    riga.Item("DATA_INSERIMENTO") = ""
                    riga.Item("DATA_APPUNTAMENTO") = ""
                    riga.Item("ORA_APPUNTAMENTO") = elemento.Item("ORARIO")
                    riga.Item("APPUNTAMENTO_CON") = ""
                    riga.Item("TELEFONO") = ""
                    riga.Item("CELLULARE") = ""
                    riga.Item("EMAIL") = ""
                    riga.Item("NOTE") = ""
                    riga.Item("SEGNALAZIONE") = ""
                    riga.Item("ELIMINA") = ""
                    riga.Item("FL_ELIMINA") = ""
                    dt.Rows.Add(riga)
                End If
                LettoreApp.Close()

            Next

            DataGridAppuntamentiSportello4.DataSource = dt
            DataGridAppuntamentiSportello4.DataBind()
            dt.Rows.Clear()

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - CaricaAppuntamenti - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub DataGridAppuntamenti_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppuntamentiSportello1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (document.getElementById('daElimina').value==0){if(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "FL_ELIMINA")).Text & "==0){if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ID")).Text & "';document.getElementById('btnAggiornaForm').click();}}")
            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "FL_ELIMINA")).Text = "0" Then
                If Session.Item("MOD_CALL_CENTER_SL") <> "1" Then
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ELIMINA")).Text = "<img src=""Immagini/Elimina.png"" alt=""elimina"" onclick=""javascript:EliminaAppuntamento(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ID")).Text & ");"""
                Else
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ELIMINA")).Text = ""
                End If
            End If
            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "FL_ELIMINA")).Text = "1" Then
                e.Item.ForeColor = Drawing.Color.Gray
            End If
        End If
    End Sub
    Protected Sub DataGridAppuntamentiSportello2_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppuntamentiSportello2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (document.getElementById('daElimina').value==0){if(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "FL_ELIMINA")).Text & "==0){if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "ID")).Text & "';document.getElementById('btnAggiornaForm').click();}}")

            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "FL_ELIMINA")).Text = "0" Then
                If Session.Item("MOD_CALL_CENTER_SL") <> "1" Then
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "ELIMINA")).Text = "<img src=""Immagini/Elimina.png"" alt=""elimina"" onclick=""javascript:EliminaAppuntamento(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "ID")).Text & ");"""
                Else
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "ELIMINA")).Text = ""
                End If
            End If
            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "FL_ELIMINA")).Text = "1" Then
                e.Item.ForeColor = Drawing.Color.Gray
            End If
        End If
    End Sub
    Protected Sub DataGridAppuntamentiSportello3_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppuntamentiSportello3.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (document.getElementById('daElimina').value==0){if(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "FL_ELIMINA")).Text & "==0){if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "ID")).Text & "';document.getElementById('btnAggiornaForm').click();}}")

            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "FL_ELIMINA")).Text = "0" Then
                If Session.Item("MOD_CALL_CENTER_SL") <> "1" Then
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "ELIMINA")).Text = "<img src=""Immagini/Elimina.png"" alt=""elimina"" onclick=""javascript:EliminaAppuntamento(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "ID")).Text & ");"""
                Else
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "ELIMINA")).Text = ""
                End If
            End If
            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "FL_ELIMINA")).Text = "1" Then
                e.Item.ForeColor = Drawing.Color.Gray
            End If
        End If
    End Sub
    Protected Sub DataGridAppuntamentiSportello4_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridAppuntamentiSportello4.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (Selezionato!=this) {OldColor=this.style.backgroundColor;this.style.backgroundColor='yellow';};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (Selezionato!=this) {this.style.backgroundColor=OldColor;OldColor='';};")
            e.Item.Attributes.Add("onclick", "if (document.getElementById('daElimina').value==0){if(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "FL_ELIMINA")).Text & "==0){if (Selezionato!=this) {if (Selezionato) {Selezionato.style.backgroundColor=SelColo;};SelColo=OldColor;};Selezionato=this;this.style.backgroundColor='#FF9900';" _
                                & "document.getElementById('idSelected').value='" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "ID")).Text & "';document.getElementById('btnAggiornaForm').click();}}")

            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "FL_ELIMINA")).Text = "0" Then
                If Session.Item("MOD_CALL_CENTER_SL") <> "1" Then
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "ELIMINA")).Text = "<img src=""Immagini/Elimina.png"" alt=""elimina"" onclick=""javascript:EliminaAppuntamento(" & e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "ID")).Text & ");"""
                Else
                    e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "ELIMINA")).Text = ""
                End If
            End If
            If e.Item.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "FL_ELIMINA")).Text = "1" Then
                e.Item.ForeColor = Drawing.Color.Gray
            End If
        End If
    End Sub

    Protected Sub cmbFiliale_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFiliale.SelectedValue & " ORDER BY ID", cmbSportello, "ID", "DESCRIZIONE", False)
        CaricaAppuntamenti()
    End Sub

    Protected Sub ButtonSalva_Click(sender As Object, e As System.EventArgs) Handles ButtonSalva.Click
        Try
            Dim errore As Boolean = False
            Dim testoErrore As String = ""
            'If Trim(TextBoxOra.Text) = "" Or Trim(TextBoxMinuto.Text) = "" Then
            '    errore = True
            '    testoErrore &= "L\'orario appuntamento è obbligatorio.\n"
            'End If
            'If IsNumeric(TextBoxOra.Text) AndAlso (CInt(TextBoxOra.Text) < 0 Or CInt(TextBoxOra.Text) > 23) Then
            '    errore = True
            '    testoErrore &= "Inserire l\'ora correttamente.\n"
            'End If
            'If IsNumeric(TextBoxMinuto.Text) AndAlso (CInt(TextBoxMinuto.Text) < 0 Or CInt(TextBoxMinuto.Text) > 59) Then
            '    errore = True
            '    testoErrore &= "Inserire i minuti correttamente.\n"
            'End If
            If cmbFilialeIns.SelectedValue = "-1" Then
                errore = True
                testoErrore &= "Selezionare una filiale.\n"
            End If
            If Trim(TextBoxDataAppuntamento.Text) = "" Or Len(TextBoxDataAppuntamento.Text) <> 10 Then
                errore = True
                testoErrore &= "Data appuntamento obbligatoria.\n"
            End If
            If Trim(TextBoxCognome.Text) = "" Then
                errore = True
                testoErrore &= "Il cognome è obbligatorio.\n"
            End If
            If Trim(TextBoxTelefono.Text) = "" Then
                errore = True
                testoErrore &= "Il campo ""Telefono 1"" è obbligatorio.\n"
            End If
            If Not errore Then
                connData.apri()
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID_SEGNALAZIONE=" & idSegnalazione.Value _
                    & " AND DATA_ELIMINAZIONE IS NULL"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim numeroSegnalazioni As Integer = 0
                If lettore.Read Then
                    numeroSegnalazioni = par.IfNull(lettore(0), 0)
                End If
                lettore.Close()
                If numeroSegnalazioni = 0 Then
                    Dim dataOdierna As Date = Format(Now, "dd/MM/yyyy")
                    Dim dataAppuntamento As Date = TextBoxDataAppuntamento.Text
                    If dataAppuntamento > dataOdierna Then

                        If Not par.IsFestivo(TextBoxDataAppuntamento.Text, True, cmbOrario.SelectedValue) Then
                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPUNTAMENTI_CALL_CENTER ( " _
                            & " ID, DATA_APPUNTAMENTO, DATA_INSERIMENTO, ID_ORARIO, ID_SPORTELLO , ID_STRUTTURA,  " _
                            & " ID_OPERATORE, NOME, COGNOME,  " _
                            & " TELEFONO, CELLULARE, EMAIL, NOTE, ID_SEGNALAZIONE)  " _
                            & " VALUES (SISCOM_MI.SEQ_APPUNTAMENTI_CALL_CENTER.NEXTVAL, " _
                            & " '" & par.FormatoDataDB(TextBoxDataAppuntamento.Text) & "', " _
                            & " '" & Format(Now, "yyyyMMddHHmmss") & "', " _
                            & cmbOrario.SelectedValue & ", " _
                            & cmbSportello.SelectedValue & ", " _
                            & cmbFilialeIns.SelectedValue & " , " _
                            & Session.Item("ID_OPERATORE") & ", " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxNome.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxCognome.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxTelefono.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxCellulare.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxEmail.Text)) & "', " _
                            & "'" & par.PulisciStrSql(UCase(TextBoxNote.Text)) & "', " _
                            & idSegnalazione.Value & ")"
                            Try
                                par.cmd.ExecuteNonQuery()
                            Catch ex As Exception
                                connData.chiudi()
                                Response.Write("<script>if(window.opener.document.getElementById('btnAggiorna')!=null){window.opener.document.getElementById('btnAggiorna').click();};alert('Slot appuntamento già occupato!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                                'Response.Write("alert('Slot appuntamento già occupato!');</script>")
                            End Try
                            connData.chiudi()
                            Response.Write("<script>if(window.opener.document.getElementById('btnAggiorna')!=null){window.opener.document.getElementById('btnAggiorna').click();};alert('Appuntamento inserito correttamente');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                        Else
                            connData.chiudi()
                            Response.Write("<script>alert('Appuntamento non inserito! Gli sportelli\nnella data e nell\'ora selezionati sono chiusi.');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                        End If
                    Else
                        Response.Write("<script>alert('Appuntamento non inserito!\nNon è più possibile prendere appuntamento\nnella giornata selezionata.');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                    End If
                Else
                    par.cmd.CommandText = "SELECT TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy') AS DATA FROM SISCOM_MI.APPUNTAMENTI_CALL_cENTER WHERE ID_sEGNALAZIONE=" & idSegnalazione.Value & " AND DATA_ELIMINAZIONE IS NULL"
                    Dim lettoreData As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim data As String = ""
                    If lettoreData.Read Then
                        data = par.IfNull(lettoreData("DATA"), "")
                    End If
                    lettoreData.Close()
                    connData.chiudi()
                    If data = "" Then
                        Response.Write("<script>alert('Appuntamento già esistente!\nModificare l\'appuntamento esistente!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                    Else
                        Response.Write("<script>alert('Appuntamento già esistente in data " & data & "!\nModificare l\'appuntamento esistente!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                    End If
                End If
                CaricaAppuntamenti()
            Else
                Response.Write("<script>alert('Appuntamento non inserito!\n" & testoErrore & "');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ButtonSalva_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub


    

    Protected Sub ButtonModifica_Click(sender As Object, e As System.EventArgs) Handles ButtonModifica.Click
        If idSelected.Value <> "-1" Then
            Try
                Dim errore As Boolean = False
                Dim testoErrore As String = ""
                'If Trim(TextBoxOra.Text) = "" Or Trim(TextBoxMinuto.Text) = "" Then
                '    errore = True
                '    testoErrore &= "L\'orario appuntamento è obbligatorio.\n"
                'End If
                'If IsNumeric(TextBoxOra.Text) AndAlso (CInt(TextBoxOra.Text) < 0 Or CInt(TextBoxOra.Text) > 23) Then
                '    errore = True
                '    testoErrore &= "Inserire l\'ora correttamente.\n"
                'End If
                'If IsNumeric(TextBoxMinuto.Text) AndAlso (CInt(TextBoxMinuto.Text) < 0 Or CInt(TextBoxMinuto.Text) > 59) Then
                '    errore = True
                '    testoErrore &= "Inserire i minuti correttamente.\n"
                'End If
                If cmbFilialeIns.SelectedValue = "-1" Then
                    errore = True
                    testoErrore &= "Selezionare una filiale.\n"
                End If
                If Trim(TextBoxDataAppuntamento.Text) = "" Or Len(TextBoxDataAppuntamento.Text) <> 10 Then
                    errore = True
                    testoErrore &= "Data appuntamento obbligatoria.\n"
                End If
                If Trim(TextBoxCognome.Text) = "" Then
                    errore = True
                    testoErrore &= "Il cognome è obbligatorio.\n"
                End If
                If Trim(TextBoxTelefono.Text) = "" Then
                    errore = True
                    testoErrore &= "Il campo ""Telefono 1"" è obbligatorio.\n"
                End If
                If Not errore Then
                    Dim dataOdierna As Date = Format(Now, "dd/MM/yyyy")
                    Dim dataAppuntamento As Date = TextBoxDataAppuntamento.Text
                    If dataAppuntamento > dataOdierna Then
                        If Not par.IsFestivo(TextBoxDataAppuntamento.Text, True, cmbOrario.SelectedValue) Then
                            connData.apri()
                            Try
                                par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_CENTER " _
                                    & " SET " _
                                    & "ID_ORARIO=" & cmbOrario.SelectedValue _
                                    & ",ID_SPORTELLO=" & cmbSportello.SelectedValue _
                                    & ",ID_STRUTTURA=" & cmbFilialeIns.SelectedValue _
                                    & ",DATA_APPUNTAMENTO='" & par.FormatoDataDB(TextBoxDataAppuntamento.Text) & "'" _
                                    & ",DATA_MODIFICA='" & Format(Now, "yyyyMMddHHmmss") & "'" _
                                    & ",ID_OPERATORE_MODIFICA=" & Session.Item("ID_OPERATORE") _
                                    & ",NOME='" & par.PulisciStrSql(UCase(TextBoxNome.Text)) & "'" _
                                    & ",TELEFONO='" & par.PulisciStrSql(UCase(TextBoxTelefono.Text)) & "'" _
                                    & ",CELLULARE='" & par.PulisciStrSql(UCase(TextBoxCellulare.Text)) & "'" _
                                    & ",EMAIL='" & par.PulisciStrSql(UCase(TextBoxEmail.Text)) & "'" _
                                    & ",NOTE='" & par.PulisciStrSql(UCase(TextBoxNote.Text)) & "'" _
                                    & ",COGNOME='" & par.PulisciStrSql(UCase(TextBoxCognome.Text)) & "'" _
                                    & " WHERE ID=" & idSelected.Value
                                par.cmd.ExecuteNonQuery()
                            Catch ex As Exception
                                connData.chiudi()
                                Response.Write("<script>if(window.opener.document.getElementById('btnAggiorna')!=null){window.opener.document.getElementById('btnAggiorna').click();};alert('Slot appuntamento già occupato!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                                'Response.Write("alert('Slot appuntamento già occupato!');</script>")
                            End Try
                            connData.chiudi()
                            Response.Write("<script>if(window.opener.document.getElementById('btnAggiorna')!=null){window.opener.document.getElementById('btnAggiorna').click();};alert('Appuntamento modificato correttamente');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                            CaricaAppuntamenti()
                        Else
                            Response.Write("<script>alert('Appuntamento non modificato! Gli sportelli\nnella data e nell\'ora selezionati sono chiusi.');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                        End If
                    Else
                        Response.Write("<script>alert('Appuntamento non modificato!\nNon è più possibile prendere appuntamento\nnella giornata selezionata.');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                    End If
                Else
                    Response.Write("<script>alert('Appuntamento non modificato!\n" & testoErrore & "');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                End If
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - ButtonModifica_Click - " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)
            End Try
        Else
            Response.Write("<script>alert('Selezionare l\'appuntamento da modificare!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
        End If
    End Sub

    Protected Sub btnAggiornaForm_Click(sender As Object, e As System.EventArgs) Handles btnAggiornaForm.Click
        If idSelected.Value <> "-1" Then
            Try
                connData.apri()
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER WHERE ID=" & idSelected.Value
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    TextBoxDataAppuntamento.Text = par.FormattaData(lettore("DATA_APPUNTAMENTO"))
                    cmbOrario.SelectedValue = lettore("ID_ORARIO")
                    cmbFilialeIns.SelectedValue = lettore("ID_STRUTTURA")
                    par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFilialeIns.SelectedValue & " ORDER BY ID", cmbSportello, "ID", "DESCRIZIONE", False)
                    Try
                        cmbSportello.SelectedValue = lettore("ID_SPORTELLO")
                    Catch ex As Exception
                    End Try
                    TextBoxCognome.Text = lettore("COGNOME")
                    TextBoxNome.Text = par.IfNull(lettore("NOME"), "")
                    TextBoxTelefono.Text = par.IfNull(lettore("TELEFONO"), "")
                    TextBoxCellulare.Text = par.IfNull(lettore("CELLULARE"), "")
                    TextBoxEmail.Text = par.IfNull(lettore("EMAIL"), "")
                    TextBoxNote.Text = par.IfNull(lettore("NOTE"), "")
                End If
                lettore.Close()
                connData.chiudi()
                For Each elemento As DataGridItem In DataGridAppuntamentiSportello1.Items
                    If idSelected.Value = elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ID")).Text.ToString Then
                        elemento.BackColor = Drawing.ColorTranslator.FromHtml("#FF9900")
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                Next
                For Each elemento As DataGridItem In DataGridAppuntamentiSportello2.Items
                    If idSelected.Value = elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ID")).Text.ToString Then
                        elemento.BackColor = Drawing.ColorTranslator.FromHtml("#FF9900")
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                Next
                For Each elemento As DataGridItem In DataGridAppuntamentiSportello3.Items
                    If idSelected.Value = elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ID")).Text.ToString Then
                        elemento.BackColor = Drawing.ColorTranslator.FromHtml("#FF9900")
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                Next
                For Each elemento As DataGridItem In DataGridAppuntamentiSportello4.Items
                    If idSelected.Value = elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ID")).Text.ToString Then
                        elemento.BackColor = Drawing.ColorTranslator.FromHtml("#FF9900")
                    Else
                        elemento.BackColor = Drawing.Color.White
                    End If
                Next
            Catch ex As Exception
                connData.chiudi()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - btnAggiornaForm_Click - " & ex.Message)
                Response.Redirect("../../Errore.aspx", False)
            End Try
        Else
            Response.Write("<script>alert('Nessun appuntamento selezionato!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
        End If
    End Sub

    Protected Sub cmbFilialeIns_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbFilialeIns.SelectedIndexChanged
        Try
            connData.apri()
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.APPUNTAMENTI_SPORTELLI WHERE ID_FILIALE=" & cmbFilialeIns.SelectedValue & " ORDER BY ID", cmbSportello, "ID", "DESCRIZIONE", False)
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - cmbFilialeIns_SelectedIndexChanged - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

	Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Try
            If confermaGenerica.Value = "1" Then
                confermaGenerica.Value = "0"
                If idSelected.Value <> "-1" Then
                    connData.apri()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_cENTER SET ID_OPERATORE_ELIMINAZIONE=" & Session.Item("ID_OPERATORE") & ",DATA_ELIMINAZIONE=" & Format(Now, "yyyyMMddHHmmss") & " WHERE ID=" & idSelected.Value
                    par.cmd.ExecuteNonQuery()
                    connData.chiudi()
                    Response.Write("<script>if(window.opener.document.getElementById('btnAggiorna')!=null){window.opener.document.getElementById('btnAggiorna').click();};alert('Appuntamento eliminato correttamente!');location.replace('DettagliAppuntamenti.aspx" & Request.Url.Query & "');</script>")
                End If
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - btnElimina_Click - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

        End Try
    End Sub

    Private Sub soloLettura()
        TextBoxDataAppuntamento.Enabled = False
        TextBoxNome.Enabled = False
        TextBoxCognome.Enabled = False
        TextBoxNote.Enabled = False
        TextBoxTelefono.Enabled = False
        TextBoxCellulare.Enabled = False
        TextBoxEmail.Enabled = False
        cmbSportello.Enabled = False
        cmbFilialeIns.Enabled = False
        cmbOrario.Enabled = False
        ButtonSalva.Visible = False
        cmbFiliale.Enabled = False
        For Each elemento As DataGridItem In DataGridAppuntamentiSportello1.Items
            elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello1, "ELIMINA")).Text = ""
        Next
        For Each elemento As DataGridItem In DataGridAppuntamentiSportello2.Items
            elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello2, "ELIMINA")).Text = ""
        Next
        For Each elemento As DataGridItem In DataGridAppuntamentiSportello3.Items
            elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello3, "ELIMINA")).Text = ""
        Next
        For Each elemento As DataGridItem In DataGridAppuntamentiSportello4.Items
            elemento.Cells(par.IndDGC(DataGridAppuntamentiSportello4, "ELIMINA")).Text = ""
        Next
    End Sub
End Class
