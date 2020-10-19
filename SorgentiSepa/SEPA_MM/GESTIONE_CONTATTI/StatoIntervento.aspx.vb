
Partial Class GESTIONE_CONTATTI_StatoIntervento
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
            lblTitolo.Text = "Stato interventi"
            RiempiOperatori()
            CaricaSediTerritoriali()
            CaricaTipo()
            txtInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Stato Intervento - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub Ricerca()
        Try
            Dim whereCond As String = ""
            connData.apri()
            Dim TOTAPERTI As Integer = 0
            Dim TOTCARICO As Integer = 0
            Dim TOTSOPRALL As Integer = 0
            Dim TOTORDINE As Integer = 0
            Dim TOTRESPINTI As Integer = 0
            Dim TOTCHIUSI As Integer = 0
            Dim TOTANNULLATI As Integer = 0
            Dim TOTTOTALE As Integer = 0
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Me.lblTitolo.Text = "Sintesi stato interventi"
            Me.lblSpiega.Text = "Informazioni sul numero degli interventi in relazione al loro stato"
            Me.lblFiltri.Text = ""

            Dim DAL As String = par.AggiustaData(txtInizio.Text)
            Dim AL As String = par.AggiustaData(txtFine.Text)
            Dim TIPO As String = cmbTipo.SelectedValue.ToString
            Dim STR As String = cmbStruttura.SelectedItem.Value
            Dim OP As String = cmbOpSegnalante.SelectedItem.Value

            Dim swhere As Boolean = True
            Dim e As String = ""
            If Not String.IsNullOrEmpty(Request.QueryString("DAL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_ORA_RICHIESTA,1,8),'30000000') >='" & Request.QueryString("DAL") & "' "
                Me.lblFiltri.Text = lblFiltri.Text & " Data apertura dal: " & par.FormattaData(Request.QueryString("DAL"))
            End If
            If Not String.IsNullOrEmpty(Request.QueryString("AL")) Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " nvl(substr(DATA_CHIUSURA,1,8),'10000000') <='" & Request.QueryString("AL") & "'"
                Me.lblFiltri.Text = lblFiltri.Text & " Data chiusura al: " & par.FormattaData(Request.QueryString("AL"))
            End If
            If par.IfEmpty(Request.QueryString("TIPO"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " ID_TIPOLOGIE =" & Request.QueryString("TIPO")
                par.cmd.CommandText = "SELECT DESCRIZIONE FROM SISCOM_MI.TIPOLOGIE_GUASTI WHERE ID = " & Request.QueryString("TIPO")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Tipologia: " & par.IfNull(lettore("DESCRIZIONE"), "")
                End If
                lettore.Close()
            End If
            If par.IfEmpty(Request.QueryString("STR"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " ID_STRUTTURA  = " & Request.QueryString("STR")
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID = " & Request.QueryString("STR")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Filiale: " & par.IfNull(lettore("NOME"), "")
                End If
                lettore.Close()
            End If
            If par.IfEmpty(Request.QueryString("OP"), "-1") <> "-1" Then
                If swhere = True Then
                    e = " and "
                Else
                    e = ""
                    swhere = True
                End If
                whereCond = whereCond & " " & e & " ID_OPERATORE_INS  = " & Request.QueryString("OP")
                par.cmd.CommandText = "SELECT OPERATORE FROM OPERATORI WHERE ID = " & Request.QueryString("OP")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblFiltri.Text = lblFiltri.Text & " Operatore: " & par.IfNull(lettore("OPERATORE"), "")
                End If
                lettore.Close()
            End If
            par.cmd.CommandText = "SELECT TIPOLOGIE_GUASTI.DESCRIZIONE AS TIPO," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=0 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS APERTI, " _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=3 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS IN_cARICO," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=1 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS IN_SOPRALLUOGO," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=4 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS IN_ORDINE," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=5 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS RESPINTI," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=10 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS CHIUSI," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE ID_sTATO=2 AND SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS ANNULLATI," _
                & "(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI WHERE SEGNALAZIONI.ID_TIPOLOGIE=TIPOLOGIE_gUASTI.ID " & whereCond & ") AS TOTALE " _
                & "FROM SISCOM_MI.TIPOLOGIE_GUASTI ORDER BY TIPOLOGIE_GUASTI.DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            da.Dispose()
            Dim riga As Data.DataRow
            For Each row As Data.DataRow In dt.Rows
                TOTAPERTI = TOTAPERTI + row.Item("APERTI")
                TOTCARICO = TOTCARICO + row.Item("IN_CARICO")
                TOTSOPRALL = TOTSOPRALL + row.Item("IN_SOPRALLUOGO")
                TOTORDINE = TOTORDINE + row.Item("IN_ORDINE")
                TOTRESPINTI = TOTRESPINTI + row.Item("RESPINTI")
                TOTCHIUSI = TOTCHIUSI + row.Item("CHIUSI")
                TOTANNULLATI = TOTANNULLATI + row.Item("ANNULLATI")
                TOTTOTALE = TOTTOTALE + row.Item("TOTALE")
            Next
            riga = dt.NewRow()
            riga.Item("TIPO") = "TOTALE COMPLESSIVO"
            riga.Item("APERTI") = TOTAPERTI
            riga.Item("IN_CARICO") = TOTCARICO
            riga.Item("IN_SOPRALLUOGO") = TOTSOPRALL
            riga.Item("IN_ORDINE") = TOTORDINE
            riga.Item("RESPINTI") = TOTRESPINTI
            riga.Item("CHIUSI") = TOTCHIUSI
            riga.Item("ANNULLATI") = TOTANNULLATI
            riga.Item("TOTALE") = TOTTOTALE
            dt.Rows.Add(riga)
            Me.DataGridRptTipo.DataSource = dt
            Me.DataGridRptTipo.DataBind()
            connData.chiudi()
            visualizzaPaginaRicerca()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Stato Intervento - Ricerca - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub btnExport_Click(sender As Object, e As System.EventArgs) Handles btnExport.Click
        Dim nomeFile As String = par.EsportaExcelAutomaticoDaDataGrid(DataGridRptTipo, "RptSegn", False)
        If IO.File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
            par.modalDialogMessage("Ricerca stato intervento", "Esportazione eseguita correttamente.", Me.Page, "successo", "../FileTemp/" & nomeFile)
        Else
            par.modalDialogMessage("Ricerca stato intervento", "Si è verificato un errore durante l\'esportazione. Riprovare!", Me.Page, "errore", , )
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Stato Intervento - RiempiOperatori - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Stato Intervento - CaricaSediTerritoriali - " & ex.Message)
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
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Report Stato Intervento - CaricaTipo - " & ex.Message)
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
