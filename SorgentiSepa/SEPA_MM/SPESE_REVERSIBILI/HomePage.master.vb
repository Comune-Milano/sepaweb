
Partial Class SPESE_REVERSIBILI_HomePage
    Inherits PageSetMasterIdMode
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        ' par.settaConnection(Session)
    End Sub
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Me.ID = "MasterPage"
        If Not IsPostBack Then
            CaricaOperatore()
            NascondiMenuOperatori()
        End If
        'NavigationMenu.Items.Remove(NavigationMenu.FindItem("Preventivi"))
        'NavigationMenu.Items.Remove(NavigationMenu.FindItem("CreaBolletta"))
    End Sub

    Private Sub NascondiMenuOperatori()
        Try
            If Session.Item("FL_SR_GESTIONE_TOTALE") = "0" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportTotale")
            End If
            If Session.Item("FL_SR_GESTIONE_IMPORT") = "0" Then
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportaMulte")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportaCustodi")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportFattIdriche")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportFattElettriche")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportaNoleggio")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportaAutogestioni")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportODL")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "SchedaImputazione")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "ImputazioneAscensori")
                par.RimuoviNodoMenuTelerik(NavigationMenu, "Imputazione")
                'par.RimuoviNodoMenuTelerik(NavigationMenu, "ImportRiscaldamentoDaXLS")
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Spese Reversibili - HomePage_Master - NascondiMenuOperatori - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        End Try
    End Sub

    Public Sub NascondiMenu()
        NavigationMenu.Visible = False
    End Sub
    Private Sub CaricaOperatore()
        connData = New CM.datiConnessione(par, False, False)
        Try
            lblOperatore.Text = par.IfNull(Session.Item("NOME_OPERATORE").ToString, "- - -")
            If Not IsNothing(Session.Item("ID_STRUTTURA")) Then
                connData.apri(False)
                par.cmd.CommandText = "SELECT NOME, TIPOLOGIA_STRUTTURA_ALER.DESCRIZIONE " _
                                    & "FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.TIPOLOGIA_STRUTTURA_ALER " _
                                    & "WHERE TIPOLOGIA_STRUTTURA_ALER.ID(+) = TAB_FILIALI.ID_TIPO_ST AND TAB_FILIALI.ID = " & par.insDbValue(Session.Item("ID_STRUTTURA").ToString, True)
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If MyReader.Read Then
                    lblFiliale.Text = par.IfEmpty(MyReader("NOME"), "- - -").ToString.ToUpper & "<br>" & par.IfEmpty(MyReader("DESCRIZIONE"), "").ToString.ToUpper
                Else
                    lblFiliale.Text = "- - -<br>- - -"
                End If
                MyReader.Close()
                connData.chiudi(False)
            Else
                lblFiliale.Text = "- - -<br>- - -"
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza: Spese Reversibili - HomePage_Master - CaricaOperatore - " & ex.Message)
            Response.Redirect("../Errore.aspx", True)
        Finally
            connData.PulisciPool()
        End Try
    End Sub
    Protected Sub NavigationMenu_ItemClick(sender As Object, e As Telerik.Web.UI.RadMenuEventArgs) Handles NavigationMenu.ItemClick
        If (Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE") = 0 Or IsNothing(Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE"))) And NavigationMenu.SelectedValue <> "Home" Then
            RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili oppure è necessario selezionare un\'elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
        Else
            Select Case NavigationMenu.SelectedValue
                Case "AnomalieConguagli"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("AnomalieConguagli.aspx", True)
                    'End If
                Case "AnomaliePreventivo"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("AnomaliePreventivo.aspx", True)
                    'End If
                Case "Ascensore"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("Ascensori.aspx", True)
                    'End If
                Case "Riscaldamento"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("Riscaldamento.aspx", True)
                    'End If
                Case "Servizi"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("Servizi.aspx", True)
                    'End If
                Case "ProspettoConsuntivi"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Try
                        connData = New CM.datiConnessione(par, False, False)
                        connData.apri()
                        Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                        'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
                        par.cmd.CommandText = "SELECT ID_STATO_APPLICAZIONE FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                        Dim applicazione As Integer = 4
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            applicazione = par.IfNull(lettore(0), 4)
                        End If
                        lettore.Close()
                        connData.chiudi()
                        Select Case applicazione
                            Case 0, 3
                                Response.Redirect("ProspettoConsuntivi.aspx", False)
                            Case 1
                                RadWindowManager1.RadAlert("I consuntivi/conguagli per l\'elaborazione selezionata sono già stati applicati nello schema bollette! Non è possibile simulare nuovi consuntivi/conguagli!", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?sl=1';}", "null")
                            Case 2
                                RadWindowManager1.RadAlert("I consuntivi/conguagli per l\'elaborazione selezionata sono già stati applicati in bolletta! Non è possibile simulare nuovi consuntivi/conguagli!", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?sl=1';}", "null")
                            Case Else
                                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili oppure è necessario selezionare un\'elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                        End Select
                    Catch ex As Exception
                        connData.chiudi()
                        RadWindowManager1.RadAlert("I consuntivi/conguagli per l\'elaborazione selezionata sono già stati applicati! Non è possibile simulare nuovi consuntivi/conguagli!", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?sl=1';}", "null")
                    End Try
                    'End If
                Case "Conguagli"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("RicercaConsuntivi.aspx", True)
                    'End If
                Case "Prospetto"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Try
                        connData = New CM.datiConnessione(par, False, False)
                        connData.apri()
                        Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                        'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
                        par.cmd.CommandText = "SELECT ID_STATO_APPLICAZIONE FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                        Dim applicazione As Integer = 4
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            applicazione = par.IfNull(lettore(0), 4)
                        End If
                        lettore.Close()
                        connData.chiudi()
                        Select Case applicazione
                            Case 0, 3
                                Response.Redirect("ProspettoPreventivi.aspx", False)
                            Case 1
                                RadWindowManager1.RadAlert("I preventivi per l\'elaborazione selezionata sono già stati applicati nello schema bollette! Non è possibile simulare nuovi preventivi!", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?sl=1';}", "null")
                            Case 2
                                RadWindowManager1.RadAlert("I preventivi per l\'elaborazione selezionata sono già stati applicati in bolletta! Non è possibile simulare nuovi preventivi!", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?sl=1';}", "null")
                            Case Else
                                RadWindowManager1.RadAlert("Operatore non abilitato alla gestione delle spese reversibili oppure è necessario selezionare un\'elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                        End Select
                    Catch ex As Exception
                        connData.chiudi()
                        RadWindowManager1.RadAlert("I consuntivi/conguagli per l\'elaborazione selezionata sono già stati applicati! Non è possibile simulare nuovi consuntivi/conguagli.", 300, 150, "Attenzione", "function f(sender,args){location.href='ProspettoConsuntivi.aspx?';}", "null")
                    End Try
                    'End If
                Case "Estrazioni"
                    Response.Redirect("Estrazioni.aspx", True)
                Case "Emesso"
                    Response.Redirect("Emesso.aspx", True)
                Case "ImportaEmesso"
                    Response.Redirect("ImportaEmesso.aspx", True)
                Case "ImportODL"
                    Response.Redirect("ImportODL.aspx", True)
                Case "Home"
                    'Response.Redirect("Default.aspx", True)
                    ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "self.close();", True)

                Case "QueryUI"
                    Response.Redirect("RicercaPreventivi.aspx", True)
                Case "ModificaManualeCaratura"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("ModificaManualeCaratura.aspx", True)
                    'End If
                Case "ModificaMassivaCaratura"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Response.Redirect("ModificaMassivaCaratura.aspx", True)
                    'End If
                Case "CreaBolletta"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Try
                        connData = New CM.datiConnessione(par, False, False)
                        connData.apri()
                        Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                        'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
                        par.cmd.CommandText = "SELECT APPLICAZIONE_BOL_CONS,APPLICAZIONE_BOL_SCHEMA_CONS,APPLICAZIONE_BOL_PREV,APPLICAZIONE_BOL_SCHEMA_PREV FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                        Dim APPLICAZIONE_BOL_CONS As Integer = 1
                        Dim APPLICAZIONE_BOL_SCHEMA_CONS As Integer = 1
                        Dim APPLICAZIONE_BOL_PREV As Integer = 1
                        Dim APPLICAZIONE_BOL_SCHEMA_PREV As Integer = 1
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            APPLICAZIONE_BOL_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_CONS"), 1)
                            APPLICAZIONE_BOL_SCHEMA_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_SCHEMA_CONS"), 1)
                            APPLICAZIONE_BOL_PREV = par.IfNull(lettore("APPLICAZIONE_BOL_PREV"), 1)
                            APPLICAZIONE_BOL_SCHEMA_PREV = par.IfNull(lettore("APPLICAZIONE_BOL_SCHEMA_PREV"), 1)
                        End If
                        lettore.Close()
                        connData.chiudi()
                        If (APPLICAZIONE_BOL_CONS = 1 Or APPLICAZIONE_BOL_SCHEMA_CONS = 1) And (APPLICAZIONE_BOL_PREV = 1 Or APPLICAZIONE_BOL_SCHEMA_PREV = 1) Then
                            RadWindowManager1.RadAlert("Le bollette per l\'esercizio finanziario selezionato sono già state create! Non è possibile creare nuove bollette per questa elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                        Else
                            Response.Redirect("CreaBollette.aspx", True)
                        End If
                    Catch ex As Exception
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Le bollette per l\'esercizio finanziario selezionato sono già state create! Non è possibile creare nuove bollette per questo anno.", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
                    End Try
                    'End If
                Case "CreaPreventivo"
                    Try
                        connData = New CM.datiConnessione(par, False, False)
                        connData.apri()
                        Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                        'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
                        par.cmd.CommandText = "SELECT APPLICAZIONE_BOL_CONS,APPLICAZIONE_BOL_SCHEMA_CONS,APPLICAZIONE_BOL_PREV,APPLICAZIONE_BOL_SCHEMA_PREV FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                        Dim APPLICAZIONE_BOL_CONS As Integer = 1
                        Dim APPLICAZIONE_BOL_SCHEMA_CONS As Integer = 1
                        Dim APPLICAZIONE_BOL_PREV As Integer = 1
                        Dim APPLICAZIONE_BOL_SCHEMA_PREV As Integer = 1
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            APPLICAZIONE_BOL_PREV = par.IfNull(lettore("APPLICAZIONE_BOL_PREV"), 1)
                            APPLICAZIONE_BOL_SCHEMA_PREV = par.IfNull(lettore("APPLICAZIONE_BOL_SCHEMA_PREV"), 1)
                        End If
                        lettore.Close()
                        connData.chiudi()
                        If APPLICAZIONE_BOL_PREV = 1 Or APPLICAZIONE_BOL_SCHEMA_PREV = 1 Then
                            RadWindowManager1.RadAlert("Le bollette per l\'elaborazione selezionata sono già state create! Non è possibile creare nuove bollette per questa elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                        Else
                            Response.Redirect("CreaBollettePreventivo.aspx", True)
                        End If
                    Catch ex As Exception
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Le bollette per l\'elaborazione selezionata sono già state create! Non è possibile creare nuove bollette per questa elaborazione.", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
                    End Try
                    'End If
                Case "CreaConguaglio"
                    'If Session.Item("FL_SPESE_REV_SL") = "1" Then

                    'Else
                    Try
                        connData = New CM.datiConnessione(par, False, False)
                        connData.apri()
                        Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                        'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
                        par.cmd.CommandText = "SELECT APPLICAZIONE_BOL_CONS,APPLICAZIONE_BOL_SCHEMA_CONS,APPLICAZIONE_BOL_PREV,APPLICAZIONE_BOL_SCHEMA_PREV FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                        Dim APPLICAZIONE_BOL_CONS As Integer = 1
                        Dim APPLICAZIONE_BOL_SCHEMA_CONS As Integer = 1
                        Dim APPLICAZIONE_BOL_PREV As Integer = 1
                        Dim APPLICAZIONE_BOL_SCHEMA_PREV As Integer = 1
                        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If lettore.Read Then
                            APPLICAZIONE_BOL_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_CONS"), 1)
                            APPLICAZIONE_BOL_SCHEMA_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_SCHEMA_CONS"), 1)
                        End If
                        lettore.Close()
                        connData.chiudi()
                        If APPLICAZIONE_BOL_CONS = 1 Or APPLICAZIONE_BOL_SCHEMA_CONS = 1 Then
                            RadWindowManager1.RadAlert("Le bollette per l\'elaborazione selezionata sono già state create! Non è possibile creare nuove bollette per questa elaborazione!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx';}", "null")
                        Else
                            Response.Redirect("CreaBolletteConsuntivo.aspx", True)
                        End If
                    Catch ex As Exception
                        connData.chiudi()
                        RadWindowManager1.RadAlert("Le bollette per l\'elaborazione selezionata sono già state create! Non è possibile creare nuove bollette per questa elaborazione.", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
                    End Try
                Case "Edifici_Cond"
                    Response.Redirect("Edifici_Condominio.aspx", True)
                    'End If
                Case "Imputazione"
                    Response.Redirect("Imputazione.aspx", True)
                Case "ImportRiscaldamentoDaXLS"
                    Response.Redirect("ImportRiscaldamentoDaXLS.aspx", True)
                Case "ImputazioneComplessi"
                    Response.Redirect("ImputazioneComplessi.aspx", True)
                Case "ImputazioneEdifici"
                    Response.Redirect("ImputazioneEdifici.aspx", True)
                Case "ImputazioneAscensori"
                    Response.Redirect("ImputazioneAscensore.aspx", True)
                Case "ImportFattIdriche"
                    Response.Redirect("ImportFattureIdriche.aspx", True)
                Case "ImportFattElettriche"
                    Response.Redirect("ImportFattureElettriche.aspx", True)
                Case "ImportaMulte"
                    Response.Redirect("ImportMulte.aspx", True)
                Case "ImportaCustodi"
                    Response.Redirect("ImportCustodi.aspx", True)
                Case "ImportaNoleggio"
                    Response.Redirect("ImportNoleggio.aspx", True)
                Case "ImportaAutogestioni"
                    Response.Redirect("ImportAutogestioni.aspx", True)
                Case "ImportTotale"
                    connData = New CM.datiConnessione(par, False, False)
                    connData.apri()
                    Dim idElaborazione = Session.Item("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                    'CONTROLLO CHE SIANO STATI APPLICATI I CONSUNTIVI/CONGUAGLIO
                    par.cmd.CommandText = "SELECT APPLICAZIONE_BOL_CONS,APPLICAZIONE_BOL_SCHEMA_CONS,APPLICAZIONE_BOL_PREV,APPLICAZIONE_BOL_SCHEMA_PREV FROM SISCOM_MI.ELABORAZIONE_SPESE_REVERSIBILI WHERE ID=" & idElaborazione
                    Dim APPLICAZIONE_BOL_CONS As Integer = 1
                    Dim APPLICAZIONE_BOL_SCHEMA_CONS As Integer = 1
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        APPLICAZIONE_BOL_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_CONS"), 1)
                        APPLICAZIONE_BOL_SCHEMA_CONS = par.IfNull(lettore("APPLICAZIONE_BOL_SCHEMA_CONS"), 1)
                    End If
                    lettore.Close()
                    connData.chiudi()
                    If APPLICAZIONE_BOL_CONS = 1 Or APPLICAZIONE_BOL_SCHEMA_CONS = 1 Then
                        RadWindowManager1.RadAlert("I conguagli sono già stati applicati in bolletta!", 300, 150, "Attenzione", "function f(sender,args){location.href='Default.aspx?';}", "null")
                    Else
                    Response.Redirect("ImportTotale.aspx", True)
                    End If
                Case "Eventi"
                    Response.Redirect("Eventi.aspx", True)
                Case "CambiaElaborazione"
                    Session.Remove("SPESE_REVERSIBILI_ID_ELABORAZIONE")
                    Response.Redirect("Default.aspx", True)
                Case Else
                    Response.Redirect("Default.aspx", True)
            End Select
        End If
    End Sub
End Class

