
Imports Telerik.Web.UI

Partial Class SPESE_REVERSIBILI_RadWindowSpese
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If controlloProfilo() Then
            If Not IsPostBack Then
                CaricaDettaglio()
                HFGriglia.Value = RadGridSpese.ClientID.ToString.Replace("ctl00", "MasterPage")
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
            Dim id_Ripartizione As Integer = 0
            idRipartizione.Value = Request.QueryString("id")
            If IsNumeric(idRipartizione.Value) AndAlso CInt(idRipartizione.Value) > 0 Then
                id_Ripartizione = CInt(idRipartizione.Value)
            End If
            Dim dettaglioSpese As String = " SELECT DESCRIZIONE,IMPORTO FROM SISCOM_MI.PF_CONS_RIPARTIZIONI " _
                & " WHERE " _
                & " (NVL(ID_CATEGORIA,0), " _
                & " NVL(ID_COMPLESSO,0), " _
                & " NVL(ID_EDIFICIO,0), " _
                & " NVL(ID_SCALA,0), " _
                & " NVL(ID_IMPIANTO,0), " _
                & " NVL(ID_LOTTO,0), " _
                & " NVL(ID_AGGREGAZIONE,0), " _
                & " NVL(ID_UNITA_IMM,0)) " _
                & " iN " _
                & " (SELECT NVL(ID_CATEGORIA,0), " _
                & " NVL(ID_COMPLESSO,0), " _
                & " NVL(ID_EDIFICIO,0), " _
                & " NVL(ID_SCALA,0), " _
                & " NVL(ID_IMPIANTO,0), " _
                & " NVL(ID_LOTTO,0), " _
                & " NVL(ID_AGGREGAZIONE,0), " _
                & " NVL(ID_UNITA_IMM,0) " _
                & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE ID=" & idRipartizione.Value & ") " _
                & " And ID_PF = " & IdElaborazione

            Dim dts As Data.DataTable = par.getDataTableGrid(dettaglioSpese)
            'RadGridSpese.DataSource = dts
            'RadGridSpese.DataBind()
            connData.chiudi()
            Session.Add("DT_SPESE", dts)
        Catch ex As Exception
        End Try
    End Sub
    Private Sub solaLettura()
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "dim", "setDimensioni();", True)
    End Sub

    Protected Sub EsportaSpese_Click(sender As Object, e As System.EventArgs)
        Dim xls As New ExcelSiSol
        Dim dt As Data.DataTable = Session.Item("DT_SPESE")
        Dim dtNuova As New Data.DataTable
        dtNuova.Merge(dt)
        Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "SCHEDA_UNITA_DETTAGLIO", "SCHEDA_UNITA_DETTAGLIO", dtNuova)
        If IO.File.Exists(Server.MapPath("..\/FileTemp\/") & nomeFile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            ' RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
        RadGridSpese.Rebind()
    End Sub
    Protected Sub RefreshSpese_Click(sender As Object, e As System.EventArgs)
        RadGridSpese.Rebind()
    End Sub

    Private Sub RadGridSpese_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles RadGridSpese.NeedDataSource
        Try
            connData.apri(False)
            Dim DT As Data.DataTable = Session.Item("DT_SPESE")
            TryCast(sender, RadGrid).DataSource = DT
            connData.chiudi(False)
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "dimensioni", "setDimensioni();", True)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: RadGridSpese_NeedDataSource - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante il caricamento delle spese", 300, 150, "Attenzione", "function f(sender,args){location.href='../Errore.aspx';}", "null")
        End Try
    End Sub
End Class
