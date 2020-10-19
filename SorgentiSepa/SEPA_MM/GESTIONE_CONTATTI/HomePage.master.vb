
Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_HomePage
    Inherits System.Web.UI.MasterPage
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Me.ID = "MasterPage"
            If Not IsPostBack Then
                lblOperatore.Text = Session.Item("OPERATORE")
            End If
            Select Case Session.Item("ID_CAF")
                Case "63"
                    'OPERATORE DI CALL CENTER
                    operatoreCC.Value = "1"
                    operatoreFiliale.Value = "0"
                    operatoreFilialeTecnico.Value = "0"
                    operatoreComune.Value = "0"
                Case "2"
                    'OPERATORE MM
                    operatoreCC.Value = "0"
                    operatoreFiliale.Value = "1"
                    operatoreFilialeTecnico.Value = "0"
                    operatoreComune.Value = "0"
                Case "6"
                    'OPERATORE COMUNALE
                    operatoreCC.Value = "0"
                    operatoreFiliale.Value = "0"
                    operatoreFilialeTecnico.Value = "0"
                    operatoreComune.Value = "1"
                Case Else
                    par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
            End Select
            'Session.Item("MOD_FO_LIMITAZIONI") = "1"
            If Session.Item("MOD_GESTIONE_CONTATTI") = "1" Or Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                FLGC.Value = "1"
            Else
                FLGC.Value = "0"
            End If
            If Session.Item("MOD_GESTIONE_CONTATTI_SL") = "1" Then
                FLGC_SL.Value = "1"
            Else
                FLGC_SL.Value = "0"
            End If
            If Session.Item("FL_GC_SUPERVISORE") = "1" Then
                supervisore.Value = "1"
            Else
                supervisore.Value = "0"
            End If
            If Session.Item("FL_GC_CALENDARIO") = "1" Then
                FL_GC_CALENDARIO.Value = "1"
            Else
                FL_GC_CALENDARIO.Value = "0"
            End If
            If Session.Item("FL_GC_TABELLE_SUPP") = "1" Then
                FL_GC_TABELLE_SUPP.Value = "1"
            Else
                FL_GC_TABELLE_SUPP.Value = "0"
            End If
            If operatoreCC.Value = "0" And operatoreFiliale.Value = "0" And operatoreFilialeTecnico.Value = "0" And operatoreComune.Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
            End If
            NascondiMenu()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Master - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub NascondiMenu()
        Try
            '***************************************************************
            '********************* NuovaSegnalazione  **********************
            '***************************************************************
            If Not IsNothing(Session.Item("FL_GC_SEGNALAZIONI")) AndAlso
                Session.Item("FL_GC_SEGNALAZIONI").ToString <> "1" _
                AndAlso FLGC.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "NuovaSegnalazione")
            Else
                If FLGC_SL.Value = "1" Then
                    par.RimuoviNodoMenuAsp(NavigationMenu, "NuovaSegnalazione")
                End If
            End If
            '***************************************************************
            '********************* RicercaSegnalazioni  ********************
            '***************************************************************
            'Tutti possono accedere alla ricerca delle segnalazioni
            '***************************************************************
            '********************* StatoIntervento  ************************
            '********************* SituazioneIntervento  *******************
            '********************* Tempi  **********************************
            '***************************************************************
            'I report sono tutti disabilitati per qualsiasi tipo di 
            'operatore momentaneamente
            'If Not IsNothing(Session.Item("FL_GC_REPORT")) AndAlso Session.Item("FL_GC_REPORT").ToString <> "1" Then
            par.RimuoviNodoMenuAsp(NavigationMenu, "Report")
            'End If
            '***************************************************************
            '********************* Agenda  *********************************
            '***************************************************************
            'Tutti possono accedere all'agenda
            '***************************************************************
            '********************* AttivazioneSportelli  *******************
            '***************************************************************
            If supervisore.Value = "0" Or operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "AttivazioneSportelli")
            End If
            '***************************************************************
            '********************* Aggregazione ****************************
            '***************************************************************
            If operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "RicercaSegnalazioniUnione")
            End If
            '***************************************************************
            '********************* OperativitaSportelli  *******************
            '***************************************************************
            If FL_GC_CALENDARIO.Value = "0" Or operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "OperativitaSportelli")
            End If
            If supervisore.Value = "0" And FL_GC_CALENDARIO.Value = "0" Or operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Sportelli")
            End If
            If FL_GC_CALENDARIO.Value = "0" Or operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "AperturaSportelli")
            End If
            If supervisore.Value = "0" And FL_GC_CALENDARIO.Value = "0" Or operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Sportelli")
            End If
            '***************************************************************
            '********************* Eventi Generali  ************************
            '***************************************************************
            'Filtrato nella pagina a seconda della tipologia dell'utente connesso
            '***************************************************************
            '********************* Numeri utili  ************************
            '***************************************************************
            If FL_GC_TABELLE_SUPP.Value = "0" Or operatoreFiliale.Value <> "1" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "NumeriUtili")
            End If
            '***************************************************************
            '********************* Semafori  *******************************
            '***************************************************************
            If FL_GC_TABELLE_SUPP.Value = "0" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Semafori")
                par.RimuoviNodoMenuAsp(NavigationMenu, "ParametriSemafori")
                par.RimuoviNodoMenuAsp(NavigationMenu, "SemaforiUfficio")
            End If
            '***************************************************************
            '********************* Documentazione **************************
            '***************************************************************
            If FL_GC_TABELLE_SUPP.Value = "0" Then
                par.RimuoviNodoMenuAsp(NavigationMenu, "Documentazione")
            End If
            '***************************************************************
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Master - NascondiMenu - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub NavigationMenu_MenuItemClick(sender As Object, e As System.Web.UI.WebControls.MenuEventArgs) Handles NavigationMenu.MenuItemClick
        Try
            If optMenu.Value = 0 Then
                Select Case NavigationMenu.SelectedValue
                    Case "Home"
                        'Response.Redirect("Home.aspx", False)
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
                        noClose.Value = 0
                    Case "NuovaSegnalazione"
                        Response.Redirect("NuovaSegnalazione.aspx", False)
                    Case "RicercaSegnalazioni"
                        Response.Redirect("RicercaSegnalazioni.aspx", False)
                    Case "RicercaSegnalazioniUnione"
                        Response.Redirect("RicercaSegnalazioniUn.aspx", False)
                    Case "StatoIntervento"
                        Response.Redirect("StatoIntervento.aspx", False)
                    Case "SituazioneIntervento"
                        Response.Redirect("SituazioneIntervento.aspx", False)
                    Case "Agenda"
                        Response.Redirect("AgendaGestioneContatti.aspx", False)
                    Case "AttivazioneSportelli"
                        Response.Redirect("AttivazioneSportelli.aspx", False)
                    Case "OperativitaSportelli"
                        Response.Redirect("OperativitaSportelli.aspx", False)
                    Case "AperturaSportelli"
                        Response.Redirect("AperturaSportelli.aspx", False)
                    Case "Tempi"
                        Response.Redirect("Tempi.aspx", False)
                    Case "EventiGenerali"
                        Response.Redirect("EventiGenerali.aspx", False)
                    Case "NumeriUtili"
                        Response.Redirect("GestioneNumeriUtili.aspx", False)
                    Case "RicercaNumeriUtili"
                        Response.Redirect("RicercaNumeriUtili.aspx", False)
                    Case "Semafori"
                        Response.Redirect("Semaforo.aspx", False)
                    Case "ParametriSemafori"
                        Response.Redirect("ParametriSemaforo.aspx", False)
                    Case "Documentazione"
                        Response.Redirect("Documentazione.aspx", False)
                    Case "Esci"
                        ScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType, "close", "validNavigation=true;self.close();", True)
                        noClose.Value = 0
                End Select
            Else
                par.modalDialogMessage("Attenzione", "Chiudere la pagina in cui si sta lavorando prima di Procedere!", Me.Page)
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Master - NavigationMenu_MenuItemClick - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
End Class

