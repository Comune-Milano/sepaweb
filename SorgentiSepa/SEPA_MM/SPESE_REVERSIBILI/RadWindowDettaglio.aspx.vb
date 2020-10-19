
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_RadWindowDettaglio
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                CaricaDettaglio()
                VisisbilitaRettifica()
                HFGriglia.Value = RadGridDettaglio.ClientID.ToString.Replace("ctl00", "MasterPage")
                CType(Master.FindControl("TitoloMaster"), Label).Text = "Consuntivi e Conguagli - Ricerca - Dettaglio"
                CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
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
    Private Sub CaricaDettaglio()
        Try
            connData.apri()
            Dim IdElaborazione As String = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            idUnita.Value = Request.QueryString("idu")
            idContratto.Value = Request.QueryString("idc")
            Dim idUnitaImmobiliare As Integer = 0
            If IsNumeric(idUnita.Value) AndAlso CInt(idUnita.Value) > 0 Then
                idUnitaImmobiliare = CInt(idUnita.Value)
            End If
            Dim idRapportiUtenza As Integer = 0
            If IsNumeric(idContratto.Value) AndAlso CInt(idContratto.Value) > 0 Then
                idRapportiUtenza = CInt(idContratto.Value)
            End If
            par.cmd.CommandText = "SELECT " _
                & " COD_UNITA_IMMOBILIARE, " _
                & " COD_CONTRATTO, " _
                & " INTESTATARIO, " _
                & " TIPOLOGIA," _
                & " NUMERO_GIORNI," _
                & " TOTALE_ONERI," _
                & " TOTALE_BOLLETTATO," _
                & " TOTALE_CONGUAGLIO," _
                & " SCALA," _
                & " INTERNO," _
                & " PIANO, " _
                & " EDIFICIO " _
                & " FROM SISCOM_MI.EXPORT_CONGUAGLI " _
                & " WHERE NVL(ID_UNITA,0)=" & idUnitaImmobiliare _
                & " AND NVL(ID_CONTRATTO,0)=" & idRapportiUtenza _
                & " AND ID_PF=" & IdElaborazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                RadLabelCodiceUnitaImmobiliare.Text = par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "")
                RadLabelContratto.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                RadLabelGiorni.Text = par.IfNull(lettore("NUMERO_GIORNI"), "")
                RadLabelTipologia.Text = par.IfNull(lettore("TIPOLOGIA"), "")
                RadLabelEmesso.Text = par.IfNull(lettore("TOTALE_BOLLETTATO"), "")
                RadLabelConsuntivo.Text = "€ " & Format(Math.Round(CDec(par.IfNull(lettore("TOTALE_ONERI"), "0")), 2), "#,##0.00")
                RadLabelConguaglio.Text = "€ " & Format(Math.Round(CDec(par.IfNull(lettore("TOTALE_CONGUAGLIO"), "0")), 2), "#,##0.00")
                RadLabelIntestatario.Text = par.IfNull(lettore("INTESTATARIO"), "")
                RadLabelEdificio.Text = par.IfNull(lettore("EDIFICIO"), "")
                RadLabelScala.Text = par.IfNull(lettore("SCALA"), "")
                RadLabelInterno.Text = par.IfNull(lettore("PIANO"), "")
                RadLabelPiano.Text = par.IfNull(lettore("INTERNO"), "")
            End If
            lettore.Close()
            If IsNumeric(RadLabelConguaglio.Text) AndAlso CDec(RadLabelConguaglio.Text) > 0 Then
                RadLabelConguaglio.CssClass = "radLabelConguaglioG"
            End If

            Dim query As String = " SELECT PF_CONS_RIPARTIZIONI.ID,SUBSTR(DESCRIZIONE,1,INSTR(DESCRIZIONE,'#')-1) AS SPESA, " _
                & " (SELECT PF_CATEGORIE.GRUPPO FROM SISCOM_MI.PF_cATEGORIE WHERE PF_CATEGORIE.ID=ID_cATEGORIA) AS GRUPPO, " _
                & " (SELECT PF_CATEGORIE.DESCRIZIONE FROM SISCOM_MI.PF_cATEGORIE WHERE PF_CATEGORIE.ID=ID_cATEGORIA) AS CATEGORIA, " _
                & " (CASE WHEN ID_COMPLESSO IS NOT NULL THEN (SELECT COD_COMPLESSO||'-'||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO) ELSE NULL END) AS COMPLESSO, " _
                & " (CASE WHEN ID_EDIFICIO IS NOT NULL THEN (SELECT COD_EDIFICIO||'-'||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) ELSE NULL END) AS EDIFICIO, " _
                & " (SELECT COD_IMPIANTO||'-'||DESCRIZIONE FROM SISCOM_MI.IMPIANTI WHERE IMPIANTI.ID=ID_IMPIANTO) AS IMPIANTO, " _
                & " PF_CONSUNTIVI_dETT.IMPORTO, " _
                & " ( " _
                & " SELECT SUM(NVL(B.IMPORTO,0)) " _
                & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI B " _
                & " WHERE     (NVL (B.ID_CATEGORIA, 0), " _
                & " NVL (B.ID_COMPLESSO, 0), " _
                & " NVL (B.ID_EDIFICIO, 0), " _
                & " NVL (B.ID_SCALA, 0), " _
                & " NVL (B.ID_IMPIANTO, 0), " _
                & " NVL (B.ID_LOTTO, 0), " _
                & " NVL (B.ID_AGGREGAZIONE, 0), " _
                & " NVL (B.ID_UNITA_IMM, 0)) IN (SELECT NVL (A.ID_CATEGORIA, 0), " _
                & " NVL (A.ID_COMPLESSO, 0), " _
                & " NVL (A.ID_EDIFICIO, 0), " _
                & " NVL (A.ID_SCALA, 0), " _
                & " NVL (A.ID_IMPIANTO, 0), " _
                & " NVL (A.ID_LOTTO, 0), " _
                & " NVL (A.ID_AGGREGAZIONE, 0), " _
                & " NVL (A.ID_UNITA_IMM, 0) " _
                & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI A " _
                & " WHERE A.ID = PF_CONS_RIPARTIZIONI.ID) " _
                & " AND B.ID_PF = 20 " _
                & " ) " _
                & " AS CENTRO_DI_COSTO " _
                & " FROM SISCOM_MI.PF_CONSUNTIVI_DETT, SISCOM_MI.PF_CONS_RIPARTIZIONI " _
                & " WHERE NVL(PF_CONSUNTIVI_DETT.ID_UNITA,0)= " & idUnitaImmobiliare _
                & " AND NVL(PF_CONSUNTIVI_DETT.ID_CONTRATTO,0)=" & idRapportiUtenza _
                & " AND PF_CONSUNTIVI_DETT.ID_PF = " & IdElaborazione _
                & " AND PF_CONSUNTIVI_DETT.IMPORTO <> 0 " _
                & " AND PF_CONS_RIPARTIZIONI.ID=ID_PF_CONS_RIPARTIZIONI "

            Dim dt As Data.DataTable = par.getDataTableGrid(query)
            'RadGridDettaglio.DataSource = dt
            'RadGridDettaglio.DataBind()
            connData.chiudi()
            Session.Add("DT_DETTAGLIO", dt)
            'Dim script As String = "function f(){var test = $find(""" + RadWindowDettaglio.ClientID + """); test.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub solaLettura()
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
    End Sub

    Protected Sub EsportaDettagli_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_DETTAGLIO")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        dtNuova.Columns.RemoveAt(0)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_UNITA_DETTAGLIO", "SCHEDA_UNITA_DETTAGLIO", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ' RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        RadGridDettaglio.Rebind()
    End Sub
    Protected Sub RefreshDettagli_Click(sender As Object, e As System.EventArgs)
        RadGridDettaglio.Rebind()
    End Sub

    Private Sub RadGridDettaglio_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGridDettaglio.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_DETTAGLIO")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: dgvAppalti_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento dei dettagli", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
    Private Sub VisisbilitaRettifica()
        Try
            connData.apri(False)
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            Select Case idElaborazione
                Case "17"
                    par.cmd.CommandText = "SELECT C84,C94 FROM TMP_CONG_MM_2015 WHERE nvl(C1,0)=nvl('" & RadLabelCodiceUnitaImmobiliare.Text & "',0) AND nvl(C2,0)=nvl('" & RadLabelContratto.Text & "',0)"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        lblConsuntivoRett.Text = "€ " & Format(Math.Round(CDec(par.IfNull(lettore("C84"), "0")), 2), "#,##0.00")
                        lblConguaglioRett.Text = "€ " & Format(Math.Round(CDec(par.IfNull(lettore("C94"), "0")), 2), "#,##0.00")
                    End If
                    lettore.Close()
                Case "18"
                    par.cmd.CommandText = "SELECT C84,C94 FROM TMP_CONG_MM_2014 WHERE nvl(C1,0)=nvl('" & RadLabelCodiceUnitaImmobiliare.Text & "',0) AND nvl(C2,0)=nvl('" & RadLabelContratto.Text & "',0)"
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        lblConsuntivoRett.Text = "€ " & Format(Math.Round(CDec(par.IfNull(lettore("C84"), "0")), 2), "#,##0.00")
                        lblConguaglioRett.Text = "€ " & Format(Math.Round(CDec(par.IfNull(lettore("C94"), "0")), 2), "#,##0.00")
                    End If
                    lettore.Close()
                Case Else
                    lblConguaglioRett.Visible = False
                    lblConsuntivoRett.Visible = False
                    lblTextTotConguaglio.Visible = False
                    lblTextTotConsuntivo.Visible = False
            End Select
            connData.chiudi(False)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: VisisbilitaRettifica - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento dei dettagli", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
End Class
