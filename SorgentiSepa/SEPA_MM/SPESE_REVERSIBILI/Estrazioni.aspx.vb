
Partial Class SPESE_REVERSIBILI_Estrazioni
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        connData = New CM.datiConnessione(par, False, False)
        If controlloProfilo() Then
            CType(Master.FindControl("TitoloMaster"), Label).Text = "Estrazioni"
            CType(Master.FindControl("StatoSpeseReversibili"), Label).Text = Session.Item("SPESE_REVERSIBILI_NOTE")
            If Not IsPostBack Then
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
            RadWindowManager1.RadAlert("E\' necessario selezionare almeno una elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
            Return False
        End If
        If Session.Item("FL_SPESE_REV_SL") = "1" Then
            solaLettura()
        End If
        connData = New CM.datiConnessione(par, False, False)
        Return True
    End Function
    Private Sub solaLettura()
    End Sub

    Protected Sub ButtonConguagli_Click(sender As Object, e As System.EventArgs) Handles ButtonConguagli.Click
        Try
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            connData.apri()
            par.cmd.CommandText = "select file_conguaglio from siscom_mi.elaborazione_spese_reversibili where id=" & idElaborazione
            Dim nomefile As String = par.IfNull(par.cmd.ExecuteScalar, "")
            connData.chiudi()
            'par.cmd.CommandText = " SELECT " _
            '    & " COD_UNITA_IMMOBILIARE AS ""CODICE UNITA' IMMOBILIARE"", " _
            '    & " COD_CONTRATTO AS ""CODICE CONTRATTO"", " _
            '    & " INTESTATARIO AS ""INTESTATARIO"", " _
            '    & " STATO AS ""STATO CONTRATTO"", " _
            '    & " DATA_DECORRENZA AS ""DATA DECORRENZA"", " _
            '    & " DATA_RICONSEGNA AS ""DATA SLOGGIO"", " _
            '    & " NUMERO_GIORNI AS ""GIORNI EFFETTIVI"", " _
            '    & " EDIFICIO AS ""EDIFICIO"", " _
            '    & " TIPOLOGIA AS ""TIPOLOGIA_UI"", " _
            '    & " SCALA AS ""SCALA"", " _
            '    & " INTERNO AS ""INTERNO"", " _
            '    & " PIANO AS ""PIANO"", " _
            '    & " MILLESIMI_SERVIZI_COMPLESSO+MILLESIMI_SERVIZI_COMPLESSO_P AS ""CDR COMPLESSO UI"", " _
            '    & " MILLESIMI_SERVIZI_EDIFICIO+MILLESIMI_SERVIZI_EDIFICIO_P AS ""CDR EDIFICIO UI"", " _
            '    & " MILLESIMI_RISCALDAMENTO+MILLESIMI_RISCALDAMENTO_P AS ""CDR RISCALDAMENTO UI"", " _
            '    & " MILLESIMI_SCALA_ASCENSORE+MILLESIMI_SCALA_ASCENSORE_P AS ""CDR ASCENSORE UI"", " _
            '    & " CDR_TOTALE_COMPLESSO AS ""CDR TOTALE COMPLESSO"", " _
            '    & " CDR_TOTALE_EDIFICIO AS ""CDR TOTALE EDIFICIO"", " _
            '    & " CDR_RISCALDAMENTO_IMPIANTO AS ""CDR RISCALDAMENTO IMPIANTO"", " _
            '    & " CDR_SERVIZI_AUTOGESTIONE AS ""CDR TOTALE SERVIZI AU"", " _
            '    & " CDR_RISCALDAMENTO_AUTOGESTIONE AS ""CDR TOTALE RISCALDAMENTO AU"", " _
            '    & " CDR_ASCENSORE_IMPIANTO AS ""CDR TOTALE ASCENSORE IMPIANTO"", " _
            '    & " CDR_ASCENSORE_COMPLESSO AS ""CDR TOTALE ASCENSORE COMPLESSO"", " _
            '    & " SERVIZI AS ""ONERI SERVIZI"", " _
            '    & " SERV_COMP_ALTRO + SERV_EDI_ALTRO AS ""ALTRO"", " _
            '    & " SERV_COMP_ALTRO AS ""ALTRO COMPLESSO"", " _
            '    & " SERV_EDI_ALTRO AS ""ALTRO EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_ALTRO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""ALTRO COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_ALTRO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""ALTRO EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_ALTRO AS ""ALTRO TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_ALTRO AS ""ALTRO TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_ACQUA + SERV_EDI_ACQUA AS ""ACQUA"", " _
            '    & " SERV_COMP_ACQUA AS ""ACQUA COMPLESSO"", " _
            '    & " SERV_EDI_ACQUA AS ""ACQUA EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_ACQUA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""ACQUA COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_ACQUA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""ACQUA EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_ACQUA AS ""ACQUA TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_ACQUA AS ""ACQUA TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_CONDUZIONE + SERV_EDI_CONDUZIONE AS ""CONDUZIONE"", " _
            '    & " SERV_COMP_CONDUZIONE AS ""CONDUZIONE COMPLESSO"", " _
            '    & " SERV_EDI_CONDUZIONE AS ""CONDUZIONE EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_CONDUZIONE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""CONDUZIONE COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_CONDUZIONE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""CONDUZIONE EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_CONDUZIONE AS ""CONDUZIONE TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_CONDUZIONE AS ""CONDUZIONE TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_CUSTODI + SERV_EDI_CUSTODI AS ""CUSTODI"", " _
            '    & " SERV_COMP_CUSTODI AS ""CUSTODI COMPLESSO"", " _
            '    & " SERV_EDI_CUSTODI AS ""CUSTODI EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_CUSTODI) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""CUSTODI COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_CUSTODI) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""CUSTODI EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_CUSTODI AS ""CUSTODI TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_CUSTODI AS ""CUSTODI TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_CUSTODI_AUTO + SERV_EDI_CUSTODI_AUTO AS ""CUSTODI_AUTO"", " _
            '    & " SERV_COMP_CUSTODI_AUTO AS ""CUSTODI AUTO COMPLESSO"", " _
            '    & " SERV_EDI_CUSTODI_AUTO AS ""CUSTODI AUTO EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_CUSTODI_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""CUSTODI AUTO COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_CUSTODI_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""CUSTODI AUTO EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_CUSTODI_AUTO AS ""CUSTODI AUTO TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_CUSTODI_AUTO AS ""CUSTODI AUTO TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_FOGNATURA + SERV_EDI_FOGNATURA AS ""FOGNATURA"", " _
            '    & " SERV_COMP_FOGNATURA AS ""FOGNATURA COMPLESSO"", " _
            '    & " SERV_EDI_FOGNATURA AS ""FOGNATURA EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_FOGNATURA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""FOGNATURA COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_FOGNATURA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""FOGNATURA EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_FOGNATURA AS ""FOGNATURA TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_FOGNATURA AS ""FOGNATURA TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_PARTI_COMUNI + SERV_EDI_PARTI_COMUNI AS ""PARTI_COMUNI"", " _
            '    & " SERV_COMP_PARTI_COMUNI AS ""PARTI COMUNI COMPLESSO"", " _
            '    & " SERV_EDI_PARTI_COMUNI AS ""PARTI COMUNI EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_PARTI_COMUNI) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PARTI COMUNI COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_PARTI_COMUNI) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PARTI COMUNI EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_PARTI_COMUNI AS ""PARTI COMUNI TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_PARTI_COMUNI AS ""PARTI COMUNI TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_PULIZIA + SERV_EDI_PULIZIA AS ""PULIZIA"", " _
            '    & " SERV_COMP_PULIZIA AS ""PULIZIA COMPLESSO"", " _
            '    & " SERV_EDI_PULIZIA AS ""PULIZIA EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_PULIZIA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PULIZIA COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_PULIZIA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PULIZIA EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_PULIZIA AS ""PULIZIA TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_PULIZIA AS ""PULIZIA TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_PULIZIA_AUTO + SERV_EDI_PULIZIA_AUTO AS ""PULIZIA AUTO"", " _
            '    & " SERV_COMP_PULIZIA_AUTO AS ""PULIZIA AUTO COMPLESSO"", " _
            '    & " SERV_EDI_PULIZIA_AUTO AS ""PULIZIA AUTO EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_PULIZIA_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PULIZIA AUTO COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_PULIZIA_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PULIZIA AUTO EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_PULIZIA_AUTO AS ""PULIZIA AUTO TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_PULIZIA_AUTO AS ""PULIZIA AUTO TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_PULIZIA_PC + SERV_EDI_PULIZIA_PC AS ""PULIZIA PC"", " _
            '    & " SERV_COMP_PULIZIA_PC AS ""PULIZIA PC COMPLESSO"", " _
            '    & " SERV_EDI_PULIZIA_PC AS ""PULIZIA PC EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_PULIZIA_PC) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PULIZIA PC COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_PULIZIA_PC) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""PULIZIA PC EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_PULIZIA_PC AS ""PULIZIA PC TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_PULIZIA_PC AS ""PULIZIA PC TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_UTENZE_ELETTR + SERV_EDI_UTENZE_ELETTR AS ""UTENZE ELETTR"", " _
            '    & " SERV_COMP_UTENZE_ELETTR AS ""UTENZE ELETTR COMPLESSO"", " _
            '    & " SERV_EDI_UTENZE_ELETTR AS ""UTENZE ELETTR EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_UTENZE_ELETTR) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""UTENZE ELETTR COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_UTENZE_ELETTR) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""UTENZE ELETTR EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_UTENZE_ELETTR AS ""UTENZE ELETTR TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_UTENZE_ELETTR AS ""UTENZE ELETTR TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_UTENZE_IDRICHE + SERV_EDI_UTENZE_IDRICHE AS ""UTENZE IDRICHE"", " _
            '    & " SERV_COMP_UTENZE_IDRICHE AS ""UTENZE IDRICHE COMPLESSO"", " _
            '    & " SERV_EDI_UTENZE_IDRICHE AS ""UTENZE IDRICHE EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_UTENZE_IDRICHE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""UTENZE IDRICHE COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_UTENZE_IDRICHE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""UTENZE IDRICHE EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_UTENZE_IDRICH AS ""UTENZE IDRICHE TOT COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_UTENZE_IDRICHE AS ""UTENZE IDRICHE TOT EDIFICIO"", " _
            '    & " SERV_COMP_VARIE + SERV_EDI_VARIE AS ""VARIE"", " _
            '    & " SERV_COMP_VARIE AS ""VARIE COMPLESSO"", " _
            '    & " SERV_EDI_VARIE AS ""VARIE EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_VARIE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VARIE COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_VARIE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VARIE EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_VARIE AS ""VARIE TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_VARIE AS ""VARIE TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_VARIE_AUTO + SERV_EDI_VARIE_AUTO AS ""VARIE AUTO"", " _
            '    & " SERV_COMP_VARIE_AUTO AS ""VARIE AUTO COMPLESSO"", " _
            '    & " SERV_EDI_VARIE_AUTO AS ""VARIE AUTO EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_VARIE_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VARIE AUTO COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_VARIE_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VARIE AUTO EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_VARIE_AUTO AS ""VARIE AUTO TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_VARIE_AUTO AS ""VARIE AUTO TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_VERDE + SERV_EDI_VERDE AS ""VERDE"", " _
            '    & " SERV_COMP_VERDE AS ""VERDE COMPLESSO"", " _
            '    & " SERV_EDI_VERDE AS ""VERDE EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_VERDE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VERDE COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_VERDE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VERDE EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_VERDE AS ""VERDE TOTALE COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_VERDE AS ""VERDE TOTALE EDIFICIO"", " _
            '    & " SERV_COMP_VERDE_AUTO + SERV_EDI_VERDE_AUTO AS ""VERDE AUTO"", " _
            '    & " SERV_COMP_VERDE_AUTO AS ""VERDE AUTO COMPLESSO"", " _
            '    & " SERV_EDI_VERDE_AUTO AS ""VERDE AUTO EDIFICIO"", " _
            '    & " (SELECT SUM(SERV_COMP_VERDE_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VERDE AUTO COMPLESSO UI"", " _
            '    & " (SELECT SUM(SERV_EDI_VERDE_AUTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""VERDE AUTO EDIFICIO UI"", " _
            '    & " SERV_TOTALE_COMP_VERDE_AUTO AS ""VERDE TOT AUTO COMPLESSO"", " _
            '    & " SERV_TOTALE_EDI_VERDE_AUTO AS ""VERDE TOT AUTO EDIFICIO"", " _
            '    & " RISCALDAMENTO AS ""ONERI RISCALDAMENTO"", " _
            '    & " RISCALDAMENTO_ACQUA AS ""RISCALDAMENTO ACQUA"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_ACQUA) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO ACQUA UI"", " _
            '    & " RISCALDAMENTO_TOTALE_ACQUA AS ""RISCALDAMENTO TOTALE ACQUA"", " _
            '    & " RISCALDAMENTO_APPALTO AS ""RISCALDAMENTO APPALTO"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_APPALTO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO APPALTO UI"", " _
            '    & " RISCALDAMENTO_TOTALE_APPALTO AS ""RISCALDAMENTO TOTALE APPALTO"", " _
            '    & " RISCALDAMENTO_AUTOGESTIONE AS ""RISCALDAMENTO AUTOGESTIONE"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_AUTOGESTIONE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO AUTOGESTIONE UI"", " _
            '    & " RISCALDAMENTO_TOTALE_AUTO AS ""RISCALDAMENTO TOTALE AUTOG"", " _
            '    & " RISCALDAMENTO_FORZA_MOTRICE AS ""RISCALDAMENTO FORZA MOTRICE"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_FORZA_MOTRICE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO FORZA MOTRICE UI"", " _
            '    & " RISCALDAMENTO_TOTALE_FORZA AS ""RISCALDAMENTO TOTALE FORZA M."", " _
            '    & " RISCALDAMENTO_GAS_AUTOGESTIONE AS ""RISCALDAMENTO GAS AUTO"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_GAS_AUTOGESTIONE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO GAS AUTO"", " _
            '    & " RISCALDAMENTO_TOTALE_GAS_AUTO AS ""RISCALDAMENTO TOTALE GAS AUTO"", " _
            '    & " RISCALDAMENTO_GAS_METANO AS ""RISCALDAMENTO GAS METANO"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_GAS_METANO) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO GAS METANO UI"", " _
            '    & " RISCALDAMENTO_TOTALE_GAS_MET AS ""RISCALDAMENTO TOTALE GAS MET"", " _
            '    & " RISCALDAMENTO_GESTIONE_CALORE AS ""RISCALDAMENTO CALORE"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_GESTIONE_CALORE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO CALORE UI"", " _
            '    & " RISCALDAMENTO_TOTALE_CALORE AS ""RISCALDAMENTO TOTALE CALORE"", " _
            '    & " RISCALDAMENTO_TR_10 AS ""RISCALDAMENTO TR_10"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_TR_10) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO TR_10 UI"", " _
            '    & " RISCALDAMENTO_TOTALE_TR_10 AS ""RISCALDAMENTO TOTALE TR_10"", " _
            '    & " RISCALDAMENTO_TR_21 AS ""RISCALDAMENTO TR_21"", " _
            '    & " (SELECT SUM(RISCALDAMENTO_TR_21) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""RISCALDAMENTO TR_21 UI"", " _
            '    & " RISCALDAMENTO_TOTALE_TR_21 AS ""RISCALDAMENTO TOTALE TR_21"", " _
            '    & " SCALA_ASCENSORE AS ""ONERI ASCENSORE"", " _
            '    & " ASCENSORI_FORZA_MOTRICE AS ""ASCENSORI FORZA MOTRICE"", " _
            '    & " (SELECT SUM(ASCENSORI_FORZA_MOTRICE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""ASCENSORI FORZA MOTRICE UI"", " _
            '    & " ASCENSORI_TOTALE_FORZA_MOTRICE AS ""ASCENSORI TOTALE FORZA MOTRICE"", " _
            '    & " ASCENSORI_MANUTENZIONE AS ""ASCENSORI MANUTENZIONE"", " _
            '    & " (SELECT SUM(ASCENSORI_MANUTENZIONE) FROM SISCOM_MI.EXPORT_CONGUAGLI B WHERE B.ID_UNITA=EXPORT_CONGUAGLI.ID_UNITA AND ID_PF=" & idElaborazione & ") AS ""ASCENSORI MANUTENZIONE UI"", " _
            '    & " ASCENSORI_TOTALE_MANUTENZIONE AS ""ASCENSORI TOTALE MANUTENZIONE"", " _
            '    & " MONTASCALE AS ""ONERI MONTASCALE"", " _
            '    & " TOTALE_ONERI AS ""TOTALE ONERI CONSUNTIVO"", " _
            '    & " BOLLETTATO_SERVIZI AS ""EMESSO SERVIZI"", " _
            '    & " BOLLETTATO_RISCALDAMENTO AS ""EMESSO RISCALDAMENTO"", " _
            '    & " BOLLETTATO_SCALA_ASCENSORE AS ""EMESSO ASCENSORE"", " _
            '    & " BOLLETTATO_MONTASCALE AS ""EMESSO MONTASCALE"", " _
            '    & " TOTALE_BOLLETTATO AS ""EMESSO TOTALE"", " _
            '    & " CONGUAGLIO_SERVIZI AS ""CONGUAGLIO SERVIZI"", " _
            '    & " CONGUAGLIO_RISCALDAMENTO AS ""CONGUAGLIO RISCALDAMENTO"", " _
            '    & " CONGUAGLIO_SCALA_ASCENSORE AS ""CONGUAGLIO ASCENSORE"", " _
            '    & " CONGUAGLIO_MONTASCALE AS ""CONGUAGLIO MONTASCALE"", " _
            '    & " TOTALE_CONGUAGLIO AS ""CONGUAGLIO TOTALE"" " _
            '    & " FROM SISCOM_MI.EXPORT_CONGUAGLI " _
            '    & " WHERE ID_PF = " & idElaborazione
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'connData.chiudi()
            If Not String.IsNullOrEmpty(nomefile) Then
                Esporta(nomefile)
            End If
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: ButtonConguagli_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End Try
    End Sub
    Protected Sub ButtonDettaglioSpese_Click(sender As Object, e As System.EventArgs) Handles ButtonDettaglioSpese.Click
        Try
            Dim idElaborazione As Integer = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
            connData.apri()
            par.cmd.CommandText = "select file_spese from siscom_mi.elaborazione_spese_reversibili where id=" & idElaborazione
            Dim nomefile As String = par.IfNull(par.cmd.ExecuteScalar, "")
            connData.chiudi()
            'connData.apri()
            'par.cmd.CommandText = " SELECT " _
            '    & " id, " _
            '    & " DESCRIZIONE AS SPESA, " _
            '    & " IMPORTO AS IMPORTO, " _
            '    & " (SELECT PF_CATEGORIE.GRUPPO FROM SISCOM_MI.PF_cATEGORIE WHERE PF_CATEGORIE.ID=ID_cATEGORIA) AS GRUPPO, " _
            '    & " (SELECT PF_CATEGORIE.DESCRIZIONE FROM SISCOM_MI.PF_cATEGORIE WHERE PF_CATEGORIE.ID=ID_cATEGORIA) AS CATEGORIA, " _
            '    & " (CASE WHEN ID_COMPLESSO IS NOT NULL THEN (SELECT COD_COMPLESSO||'-'||DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID=ID_COMPLESSO) ELSE NULL END) AS COMPLESSO, " _
            '    & " (CASE WHEN ID_EDIFICIO IS NOT NULL THEN (SELECT COD_EDIFICIO||'-'||DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID=ID_EDIFICIO) ELSE NULL END) AS EDIFICIO, " _
            '    & " (SELECT COD_IMPIANTO||'-'||DESCRIZIONE FROM SISCOM_MI.IMPIANTI WHERE IMPIANTI.ID=ID_IMPIANTO) AS IMPIANTO " _
            '    & " , " _
            '    & " (CASE WHEN PF_CONS_RIPARTIZIONI.DESCRIZIONE LIKE '%DA ODL REVERSIBILI%' THEN " _
            '    & " (SELECT TAB_SERVIZI.DESCRIZIONE FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.TAB_sERVIZI,SISCOM_MI.TAB_SERVIZI_VOCI,SISCOM_MI.PF_VOCI_IMPORTO  " _
            '    & " WHERE MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO = REPLACE(PF_CONS_RIPARTIZIONI.DESCRIZIONE,SUBSTR(PF_CONS_RIPARTIZIONI.DESCRIZIONE,1,1+INSTR(PF_CONS_RIPARTIZIONI.DESCRIZIONE,'#')),'') " _
            '    & " AND PF_VOCI_IMPORTO.ID=MANUTENZIONI.ID_PF_VOCE_IMPORTO " _
            '    & " AND TAB_SERVIZI_VOCI.ID=PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO " _
            '    & " AND TAB_SERVIZI_VOCI.ID_SERVIZIO=TAB_SERVIZI.ID " _
            '    & " )  " _
            '    & " ELSE NULL END) AS SERVIZIO " _
            '    & " , " _
            '    & " (CASE WHEN PF_CONS_RIPARTIZIONI.DESCRIZIONE LIKE '%DA ODL REVERSIBILI%' THEN " _
            '    & " (SELECT TAB_SERVIZI_VOCI.DESCRIZIONE FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.TAB_sERVIZI,SISCOM_MI.TAB_SERVIZI_VOCI,SISCOM_MI.PF_VOCI_IMPORTO  " _
            '    & " WHERE MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO = REPLACE(PF_CONS_RIPARTIZIONI.DESCRIZIONE,SUBSTR(PF_CONS_RIPARTIZIONI.DESCRIZIONE,1,1+INSTR(PF_CONS_RIPARTIZIONI.DESCRIZIONE,'#')),'') " _
            '    & " AND PF_VOCI_IMPORTO.ID=MANUTENZIONI.ID_PF_VOCE_IMPORTO " _
            '    & " AND TAB_SERVIZI_VOCI.ID=PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO " _
            '    & " AND TAB_SERVIZI_VOCI.ID_SERVIZIO=TAB_SERVIZI.ID " _
            '    & " )  " _
            '    & " ELSE NULL END) AS VOCE " _
            '    & " , " _
            '    & " (CASE WHEN PF_CONS_RIPARTIZIONI.DESCRIZIONE LIKE '%DA ODL REVERSIBILI%' THEN " _
            '    & " (SELECT MANUTENZIONI.DESCRIZIONE FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.TAB_sERVIZI,SISCOM_MI.TAB_SERVIZI_VOCI,SISCOM_MI.PF_VOCI_IMPORTO  " _
            '    & " WHERE MANUTENZIONI.PROGR||'/'||MANUTENZIONI.ANNO = REPLACE(PF_CONS_RIPARTIZIONI.DESCRIZIONE,SUBSTR(PF_CONS_RIPARTIZIONI.DESCRIZIONE,1,1+INSTR(PF_CONS_RIPARTIZIONI.DESCRIZIONE,'#')),'') " _
            '    & " AND PF_VOCI_IMPORTO.ID=MANUTENZIONI.ID_PF_VOCE_IMPORTO " _
            '    & " AND TAB_SERVIZI_VOCI.ID=PF_VOCI_IMPORTO.ID_VOCE_SERVIZIO " _
            '    & " AND TAB_SERVIZI_VOCI.ID_SERVIZIO=TAB_SERVIZI.ID " _
            '    & " )  " _
            '    & " ELSE NULL END) AS MANUTENZIONE " _
            '    & " FROM SISCOM_MI.PF_CONS_RIPARTIZIONI WHERE " _
            '    & " ID_PF= " & idElaborazione _
            '    & " and nvl(importo,0)<>0 " _
            '    & " AND FL_NON_REVERSIBILE=0 " _
            '    & " AND IN_CONDOMINIO=0   "
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'connData.chiudi()
            'dt.Columns.RemoveAt(0)
            Esporta(nomefile)
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: ButtonDettaglioSpese_Click - " & ex.Message)
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End Try
    End Sub
    Private Sub Esporta(ByVal nomefile As String)
        If IO.File.Exists(Server.MapPath("..\/ALLEGATI\/SPESE_REVERSIBILI\/") & nomefile) Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../ALLEGATI/SPESE_REVERSIBILI/" & nomefile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
        Else
            RadWindowManager1.RadAlert("Si è verificato un errore durante l\'esportazione. Riprovare!", 300, 150, "Attenzione", "", "null")
        End If
    End Sub
End Class
