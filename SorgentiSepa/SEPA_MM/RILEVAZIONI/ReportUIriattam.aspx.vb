﻿
Partial Class RILEVAZIONI_ReportUIriattam
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try

            Me.connData = New CM.datiConnessione(par, False, False)
            If Not IsPostBack Then
                caricaSedeTerr()
                caricaStudioProf()
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Gestione Rilevazioni - Report UI - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaComplesso()
        Try
            connData.apri()
            Dim filialiSelezionate As String = ""
            For Each Items As ListItem In chkListSedeTerr.Items
                If Items.Selected = True Then
                    filialiSelezionate &= Items.Value & ","
                End If
            Next
            Dim condizRilevate As String = ""
            condizRilevate = " and rilievo_ui.id_unita in (select id_unita_immobiliare from siscom_mi.rilievo_alloggio_sfitto) "
            Dim condizioneFiliali As String = ""
            If filialiSelezionate <> "" Then
                filialiSelezionate = Left(filialiSelezionate, Len(filialiSelezionate) - 1)
                condizioneFiliali = " AND COMPLESSI_IMMOBILIARI.ID IN (SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.FILIALI_UI,siscom_mi.RILIEVO_UI WHERE EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND RILIEVO_UI.id_lotto IS NOT NULL" & condizRilevate & " AND unita_immobiliari.id=rilievo_ui.id_unita AND FILIALI_UI.ID_UI=UNITA_IMMOBILIARI.ID AND ID_FILIALE IN (" & filialiSelezionate & ")) "
                par.caricaCheckBoxList("SELECT ID,DENOMINAZIONE FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID<>1 " & condizioneFiliali & " ORDER BY DENOMINAZIONE", chkListComplesso, "ID", "DENOMINAZIONE")

                condizioneFiliali = " and id in (select id_utente from siscom_mi.rilievo_lotti where id in (select id_lotto from siscom_mi.rilievo_ui,siscom_mi.rilievo_alloggio_sfitto where rilievo_ui.id_unita=rilievo_alloggio_sfitto.id_unita_immobiliare and id_unita in (select id from siscom_mi.unita_immobiliari,siscom_mi.filiali_ui where FILIALI_UI.ID_UI=UNITA_IMMOBILIARI.ID AND ID_FILIALE IN (" & filialiSelezionate & ") ) and id_lotto is not null))"
                par.caricaCheckBoxList("SELECT ID,DESCRIZIONE FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE ID_RILIEVO=(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1)" & condizioneFiliali & " ORDER BY DESCRIZIONE ASC", chkListStudioProfess, "ID", "DESCRIZIONE")
            Else
                chkListComplesso.Items.Clear()
            End If

            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Rilevazioni - RicercaUnita - caricaComplesso - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaEdificio()
        Try
            connData.apri()
            Dim complessiSelezionati As String = ""
            For Each Items As ListItem In chkListComplesso.Items
                If Items.Selected = True Then
                    complessiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizRilevate As String = ""
            condizRilevate = " and rilievo_ui.id_unita in (select id_unita_immobiliare from siscom_mi.rilievo_alloggio_sfitto) "
            Dim condizioneComplessi As String = ""
            If complessiSelezionati <> "" Then
                complessiSelezionati = Left(complessiSelezionati, Len(complessiSelezionati) - 1)
                condizioneComplessi = " AND ID_COMPLESSO IN (" & complessiSelezionati & ") "
                par.caricaCheckBoxList("select distinct edifici.id,edifici.denominazione from siscom_mi.edifici,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id_edificio=edifici.id and unita_immobiliari.id=rilievo_ui.id_unita and edifici.ID<>1" & condizRilevate & " and RILIEVO_UI.id_lotto IS NOT NULL " & condizioneComplessi & " ORDER BY DENOMINAZIONE", chkListEdificio, "ID", "DENOMINAZIONE")
            Else
                chkListEdificio.Items.Clear()
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Rilevazioni - RicercaUnita - caricaEdificio - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaSedeTerr()
        Try
            connData.apri()
            Dim condizRilevate As String = ""
            condizRilevate = " and rilievo_ui.id_unita in (select id_unita_immobiliare from siscom_mi.rilievo_alloggio_sfitto) "
            par.caricaCheckBoxList("SELECT ID,NOME FROM SISCOM_MI.TAB_FILIALI WHERE ID IN (SELECT DISTINCT ID_FILIALE FROM SISCOM_MI.FILIALI_UI,siscom_mi.unita_immobiliari,siscom_mi.RILIEVO_UI where unita_immobiliari.id=rilievo_ui.id_unita" & condizRilevate & " and unita_immobiliari.id=filiali_ui.id_ui and RILIEVO_UI.id_lotto IS NOT NULL) ORDER BY NOME", chkListSedeTerr, "ID", "NOME")
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Demanio - RicercaUnita - caricaSedeTerr - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub caricaStudioProf()
        Try
            connData.apri()
            Dim edificiSelezionati As String = ""
            For Each Items As ListItem In chkListEdificio.Items
                If Items.Selected = True Then
                    edificiSelezionati &= Items.Value & ","
                End If
            Next
            Dim condizioneEdifici As String = ""
            If edificiSelezionati <> "" Then
                edificiSelezionati = Left(edificiSelezionati, Len(edificiSelezionati) - 1)
                condizioneEdifici = " and id in (select id_utente from siscom_mi.rilievo_lotti where id in (select id_lotto from siscom_mi.rilievo_ui where id_unita in (select id from siscom_mi.unita_immobiliari where id_Edificio in (" & edificiSelezionati & ") and id_lotto is not null)))"
                par.caricaCheckBoxList("SELECT ID,DESCRIZIONE FROM SISCOM_MI.RILIEVO_TAB_UTENTI WHERE ID_RILIEVO=(SELECT ID FROM SISCOM_MI.RILIEVO WHERE FL_ATTIVO=1)" & condizioneEdifici & " ORDER BY DESCRIZIONE ASC", chkListStudioProfess, "ID", "DESCRIZIONE")
            End If
            connData.chiudi()
        Catch ex As Exception
            connData.chiudi()
            Session.Add("ERRORE", "Provenienza: Demanio - RicercaUnita - caricaStudioProf - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnAvvia_Click(sender As Object, e As System.EventArgs) Handles btnAvvia.Click
        Dim parametriDaPassare As String = ""
        parametriDaPassare &= "?Rilevati=1"


        'FILTRO FILIALI
        Dim listaFiliali As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In chkListSedeTerr.Items
            If Items.Selected = True Then
                If Not listaFiliali.Contains(Items.Value) Then
                    listaFiliali.Add(Items.Value)
                End If
            Else
                If listaFiliali.Contains(Items.Value) Then
                    listaFiliali.Remove(Items.Value)
                End If
            End If
        Next

        Session.Add("listaFiliali", listaFiliali)

        'FILTRO STUDI PROF
        Dim listaStProf As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In chkListStudioProfess.Items
            If Items.Selected = True Then
                If Not listaStProf.Contains(Items.Value) Then
                    listaStProf.Add(Items.Value)
                End If
            Else
                If listaStProf.Contains(Items.Value) Then
                    listaStProf.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaStProf", listaStProf)

        'FILTRO COMPLESSI
        Dim listaComplessi As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In chkListComplesso.Items
            If Items.Selected = True Then
                If Not listaComplessi.Contains(Items.Value) Then
                    listaComplessi.Add(Items.Value)
                End If
            Else
                If listaComplessi.Contains(Items.Value) Then
                    listaComplessi.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaComplessi", listaComplessi)

        'FILTRO EDIFICI
        Dim listaEdifici As New System.Collections.Generic.List(Of String)
        For Each Items As ListItem In chkListEdificio.Items
            If Items.Selected = True Then
                If Not listaEdifici.Contains(Items.Value) Then
                    listaEdifici.Add(Items.Value)
                End If
            Else
                If listaEdifici.Contains(Items.Value) Then
                    listaEdifici.Remove(Items.Value)
                End If
            End If
        Next
        Session.Add("listaEdifici", listaEdifici)
        Response.Redirect("RisultatiUIriattam.aspx" & parametriDaPassare & "", False)
    End Sub

    Protected Sub chkComplesso_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkComplesso.CheckedChanged
        Try
            If chkListComplesso.Items.Count > 0 Then
                If chkComplesso.Checked = True Then
                    For Each Items As ListItem In chkListComplesso.Items
                        Items.Selected = True
                    Next
                    caricaEdificio()
                Else
                    Dim controllo As Boolean = False
                    For Each Items As ListItem In chkListSedeTerr.Items
                        If Items.Selected = True Then
                            controllo = True
                            Exit For
                        End If
                    Next

                    chkListComplesso.Items.Clear()
                    If controllo = True Then
                        caricaComplesso()
                    End If
                    chkListEdificio.Items.Clear()
                End If
            Else
                chkComplesso.Checked = False
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Protected Sub chkSedeTerr_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkSedeTerr.CheckedChanged
        Try
            If chkSedeTerr.Checked = True Then
                For Each Items As ListItem In chkListSedeTerr.Items
                    Items.Selected = True
                Next
                caricaComplesso()
            Else
                For Each Items As ListItem In chkListSedeTerr.Items
                    Items.Selected = False
                Next
                chkListComplesso.Items.Clear()
                chkListEdificio.Items.Clear()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Private Sub scrollPositionPanel()
        Try
            Dim script As String = ""
            script = "if(document.getElementById('PanelFiliali')!=null){document.getElementById('PanelFiliali').scrollTop = document.getElementById('yPosFiliali').value;}"

            script &= "if(document.getElementById('PanelStudioProf')!=null){document.getElementById('PanelStudioProf').scrollTop = document.getElementById('yPosStudioProf').value;}"

            script &= "if(document.getElementById('PanelComplessi')!=null){document.getElementById('PanelComplessi').scrollTop = document.getElementById('yPosComplessi').value;}"

            script &= "if(document.getElementById('PanelEdifici')!=null){document.getElementById('PanelEdifici').scrollTop = document.getElementById('yPosEdifici').value;}"

            ScriptManager.RegisterStartupScript(Page, GetType(Panel), Page.ClientID, script, True)
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub chkListComplesso_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles chkListComplesso.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In chkListComplesso.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                chkComplesso.Checked = False
            Else
                chkComplesso.Checked = True
            End If
            Dim controlloAlmenoUnSelezionato As Boolean = False
            For Each Items As ListItem In chkListComplesso.Items
                If Items.Selected = True Then
                    controlloAlmenoUnSelezionato = True
                    Exit For
                End If
            Next
            If controlloAlmenoUnSelezionato = True Then
                caricaEdificio()
                scrollPositionPanel()
            Else
                Dim controllo As Boolean = False
                For Each Items As ListItem In chkListSedeTerr.Items
                    If Items.Selected = True Then
                        controllo = True
                        Exit For
                    End If
                Next

                chkComplesso.Checked = False
                chkListComplesso.Items.Clear()
                If controllo = True Then
                    caricaComplesso()
                End If
                chkListEdificio.Items.Clear()
                scrollPositionPanel()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Protected Sub chkListSedeTerr_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles chkListSedeTerr.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In chkListSedeTerr.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                chkSedeTerr.Checked = False
            End If
            caricaComplesso()
            scrollPositionPanel()
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Protected Sub chkEdificio_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkEdificio.CheckedChanged
        Try
            If chkListEdificio.Items.Count > 0 Then
                If chkEdificio.Checked = True Then
                    For Each Items As ListItem In chkListEdificio.Items
                        Items.Selected = True
                    Next
                    caricaStudioProf()
                Else
                    Dim controllo As Boolean = False
                    For Each Items As ListItem In chkListComplesso.Items
                        If Items.Selected = True Then
                            controllo = True
                            Exit For
                        End If
                    Next
                    chkListEdificio.Items.Clear()
                    If controllo = True Then
                        caricaEdificio()
                    End If
                End If
            Else
                chkEdificio.Checked = False
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub

    Protected Sub chkListEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles chkListEdificio.SelectedIndexChanged
        Try
            Dim controlloSeTuttiSelezionati As Boolean = True
            For Each Items As ListItem In chkListEdificio.Items
                If Items.Selected = False Then
                    controlloSeTuttiSelezionati = False
                    Exit For
                End If
            Next
            If controlloSeTuttiSelezionati = False Then
                chkEdificio.Checked = False
            Else
                chkEdificio.Checked = True
            End If
            Dim controlloAlmenoUnSelezionato As Boolean = False
            For Each Items As ListItem In chkListEdificio.Items
                If Items.Selected = True Then
                    controlloAlmenoUnSelezionato = True
                    Exit For
                End If
            Next
            If controlloAlmenoUnSelezionato = True Then
                caricaStudioProf()
                scrollPositionPanel()
            Else
                Dim controllo As Boolean = False
                For Each Items As ListItem In chkListComplesso.Items
                    If Items.Selected = True Then
                        controllo = True
                        Exit For
                    End If
                Next
                chkEdificio.Checked = False
                chkListEdificio.Items.Clear()
                If controllo = True Then
                    caricaEdificio()
                End If
                scrollPositionPanel()
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Si è verificato un errore durante il caricamento dei dati!');self.close();", True)
        End Try
    End Sub
End Class
