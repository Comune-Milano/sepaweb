
Partial Class GESTIONE_CONTATTI_SituazioneIntervento
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Me.connData = New CM.datiConnessione(par, False, False)
        If Not IsPostBack Then
            lbTitolo.Text = "Situazione interventi"
            RiempiOperatori()
            CaricaSediTerritoriali()
            CaricaStato()
            CaricaTipo()
            txtInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtApertoDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato ad eseguire questa operazione.", Page, "info", "Home.aspx")
                Exit Sub
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Situazione Intervento - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Ricerca()
        Try
            Dim whereCond As String = ""
            connData.apri()
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            lblSpiega.Text = "Informazioni sulle richieste d'intervento"
            Dim swhere As Boolean = True
            Dim e As String = ""
            lblFiltri.Text = ""

            Dim DAL As String = par.AggiustaData(txtInizio.Text)
            Dim AL As String = par.AggiustaData(txtFine.Text)
            Dim TIPO As String = cmbTipo.SelectedValue.ToString
            Dim STR As String = cmbStruttura.SelectedItem.Value
            Dim OP As String = cmbOpSegnalante.SelectedItem.Value
            Dim ST As String = cmbstato.SelectedValue
            Dim Day As String = par.AggiustaData(txtApertoDa.Text)

            If Not String.IsNullOrEmpty(DAL) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_ORA_RICHIESTA,1,8),'30000000') >='" & DAL & "'"
                lblFiltri.Text = lblFiltri.Text & " Data apertura dal: " & par.FormattaData(DAL)
            End If
            If Not String.IsNullOrEmpty(AL) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_CHIUSURA,1,8),'10000000') <= '" & AL & "'"
                lblFiltri.Text = lblFiltri.Text & " Data chiusura al: " & par.FormattaData(AL)
            End If
            If par.IfEmpty(TIPO, "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " ID_TIPOLOGIE =" & TIPO
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE ID = " & TIPO
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Tipologia: " & par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                lettore.Close()
            End If
            If par.IfEmpty(STR, "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " segnalazioni.ID_STRUTTURA  = " & STR
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & STR
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Filiale: " & par.IfNull(lettore("NOME"), "")
                End If
                lettore.Close()
            End If
            If par.IfEmpty(OP, "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " ID_OPERATORE_INS  = " & OP
                par.cmd.CommandText = "SELECT OPERATORE FROM OPERATORI WHERE ID = " & OP
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Operatore: " & par.IfNull(lettore("OPERATORE"), "")
                End If
                lettore.Close()
            End If
            If par.IfEmpty(ST, "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " SEGNALAZIONI.ID_STATO  = " & ST
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM siscom_mi.TAB_STATI_SEGNALAZIONI WHERE ID = " & ST
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Stato Segnalazione: " & par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                lettore.Close()
            End If
            If par.IfEmpty(Day, "") <> "" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " AND NVL(TO_DATE(TO_CHAR(SYSDATE,'yyyymmdd'),'yyyymmdd') - TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'yyyymmdd') ,0 )>=" & Day
                lblFiltri.Text = lblFiltri.Text & " Aperto da: " & Day
            End If
            'If whereCond <> "" Then
            '    whereCond = "WHERE " & whereCond
            'End If
            par.cmd.CommandText = "SELECT SEGNALAZIONI.ID,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'yyyymmdd'),'dd/mm/yyyy') AS data_ora_richiesta, " _
                                & "TO_CHAR(TO_DATE(SUBSTR(data_in_carico,0,8),'yyyymmdd'),'dd/mm/yyyy') AS data_in_carico, " _
                                & "TO_CHAR(TO_DATE(SUBSTR(data_chiusura,0,8),'yyyymmdd'),'dd/mm/yyyy') AS data_chiusura, " _
                                & "TIPOLOGIE_GUASTI.descrizione  AS tipo, " _
                                & "DESCRIZIONE_RIC " _
                                & "FROM siscom_mi.SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI WHERE TIPOLOGIE_GUASTI.ID = SEGNALAZIONI.id_tipologie " & whereCond _
                                & " ORDER BY data_ora_richiesta DESC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            If dt.Rows.Count > 0 Then
                DataGridRptSituaz.DataSource = dt
                DataGridRptSituaz.DataBind()
                DataGridRptSituaz.Visible = True
                visualizzaPaginaRicerca()
            Else
                DataGridRptSituaz.Visible = False
                visualizzaPaginaParametriRicerca()
                par.modalDialogMessage("Ricerca situazione intervento", "Nessun risultato trovato", Page, "info", , )
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Situazione Intervento - Ricerca - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRptSituaz, "RptSegn", False)
        If IO.File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            par.modalDialogMessage("Ricerca situazione intervento", "Esportazione eseguita correttamente.", Page, "successo", "../FileTemp/" & nomeFile)
        Else
            par.modalDialogMessage("Ricerca situazione intervento", "Si è verificato un errore durante l\'esportazione. Riprovare!", Page, "errore", , )
        End If
    End Sub
    Private Sub RiempiOperatori()
        Try
            connData.apri()
            Dim query As String = "select distinct operatori.id, operatori.operatore as nome from operatori, SISCOM_MI.SEGNALAZIONI WHERE OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS order by operatori.operatore asc"
            par.caricaComboBox(query, cmbOpSegnalante, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Situazione Intervento - RiempiOperatori - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaSediTerritoriali()
        Try
            connData.apri()
            Dim query As String = "SELECT * FROM SISCOM_MI.TAB_FILIALI ORDER BY NOME"
            par.caricaComboBox(query, cmbStruttura, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Situazione Intervento - CaricaSediTerritoriali - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaStato()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI WHERE ID >=0"
            par.caricaComboBox(query, cmbstato, "ID", "DESCRIZIONE")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Situazione Intervento - CaricaStato - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub CaricaTipo()
        Try
            connData.apri()
            Dim query As String = "SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI"
            par.caricaComboBox(query, cmbTipo, "ID", "DESCRIZIONE")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Situazione Intervento - CaricaTipo - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click
        Ricerca()
    End Sub
    Protected Sub visualizzaPaginaRicerca()
        MultiView1.ActiveViewIndex = 1
        MultiView2.ActiveViewIndex = 1
    End Sub
    Protected Sub visualizzaPaginaParametriRicerca()
        MultiView1.ActiveViewIndex = 0
        MultiView2.ActiveViewIndex = 0
    End Sub
    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        visualizzaPaginaParametriRicerca()
    End Sub
End Class