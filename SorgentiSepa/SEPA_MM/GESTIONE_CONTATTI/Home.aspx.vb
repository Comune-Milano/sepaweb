
Imports Telerik.Web.UI

Partial Class GESTIONE_CONTATTI_Home
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or Session.Item("FL_SUPERDIRETTORE") = "1" Then
                    'DIRETTORE LAVORI
                    gestioneDirettoreLavori()
                End If
                If Session.Item("MOD_FO_LIMITAZIONI") = "1" Then
                    'FORNITORE ESTERNO
                    gestioneFornitoreEsterno()
                End If
                'lblVersione.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
                If Not IsNothing(Session.Item("FL_AUTORIZZAZIONE_ODL")) AndAlso Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Or
                Not IsNothing(Session.Item("FL_SUPERDIRETTORE")) AndAlso Session.Item("FL_SUPERDIRETTORE") = "1" Then
                    RicavaNotifiche()
                End If
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Home - Page_Load - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        Try
            CType(Me.Master.FindControl("noClose"), HiddenField).Value = 0
            CType(Me.Master.FindControl("optMenu"), HiddenField).Value = 0
            If CType(Me.Master.FindControl("FLGC"), HiddenField).Value = "0" Then
                par.modalDialogMessage("Agenda e Segnalazioni", "Operatore non abilitato al modulo Agenda e Segnalazioni!", Page, "info", , True)
                Exit Sub
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Agenda e Segnalazioni - Home - Page_LoadComplete - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Function RicavaNumeroManutenzioniAlert() As Integer
        Dim filtroDL As String = ""
        If Session.Item("FL_SUPERDIRETTORE") <> "1" Or Session.Item("FL_FQM") <> "1" Then
            filtroDL = "AND ID_PROGRAMMA_ATTIVITA IN (SELECT ID FROM SISCOM_MI.PROGRAMMA_ATTIVITA WHERE ID_GRUPPO IN " _
                & " (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE DATA_FINE_INCARICO = '30000000' AND ID_OPERATORE =" & Session.Item("ID_OPERATORE") & "))"
        End If
        Dim idTipologia As String = "-1"
        par.cmd.CommandText = "SELECT count(*) " _
                                            & " FROM SISCOM_MI.combinazione_tipologie, SISCOM_MI.SEGNALAZIONI " _
                                            & " WHERE combinazione_tipologie.id_tipo_segnalazione(+) = segnalazioni.id_tipo_segnalazione " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_1(+),0) = nvl(segnalazioni.id_tipo_segn_livello_1,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_2(+),0) = nvl(segnalazioni.id_tipo_segn_livello_2,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_3(+),0) = nvl(segnalazioni.id_tipo_segn_livello_3,0) " _
                                            & " and nvl(combinazione_tipologie.id_tipo_segnalazione_livello_4(+),0) = nvl(segnalazioni.id_tipo_segn_livello_4,0) " _
                                            & " and segnalazioni.id_tipologia_manutenzione<>combinazione_tipologie.id_tipo_manutenzione " _
                                            & " and segnalazioni.id_tipologia_manutenzione is not null and segnalazioni.fl_tipologia_confermata = 0 " _
                                            & " and fl_rich_mod_tipologia = 1 " _
                                            & filtroDL
        RicavaNumeroManutenzioniAlert = CInt(par.IfNull(par.cmd.ExecuteScalar, "0"))

    End Function

    Private Sub gestioneFornitoreEsterno()
        par.cmd.CommandText = "SELECT MOD_FO_ID_FO FROM SEPA.OPERATORI WHERE OPERATORI.ID=" & Session.Item("ID_OPERATORE")
        Dim idOperatore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
        idFornitore.Value = idOperatore
        idDirettoreLavori.Value = 0
    End Sub

    Private Sub gestioneDirettoreLavori()
        Dim idOperatore As Integer = Session.Item("ID_OPERATORE")
        idDirettoreLavori.Value = idOperatore
        idFornitore.Value = 0
    End Sub
    Private Function RicavaNumeroCronoProgramma() As Integer

        Dim condizioneDirettoreLavori As String = ""
        Dim condizioneFornitori As String = ""
        If idDirettoreLavori.Value > 0 Then
            condizioneDirettoreLavori = " AND APPALTI.ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE=" & idDirettoreLavori.Value & " AND DATA_FINE_INCARICO='30000000')"
        End If
        If idFornitore.Value > 0 Then
            condizioneFornitori = " AND FORNITORI.ID=" & idFornitore.Value
        End If
        par.cmd.CommandText = " SELECT  count(*)" _
            & " FROM SISCOM_MI.PROGRAMMA_ATTIVITA,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
            & " WHERE " _
            & " PROGRAMMA_aTTIVITA.ID_FORNITORE=FORNITORI.ID " _
            & " AND FORNITORI.ID=APPALTI.ID_FORNITORE " _
            & " AND APPALTI.ID_GRUPPO=PROGRAMMA_ATTIVITA.ID_GRUPPO " _
            & condizioneDirettoreLavori _
            & condizioneFornitori _
            & " AND APPALTI.ID_GRUPPO=APPALTI.ID "
        RicavaNumeroCronoProgramma = CInt(par.IfNull(par.cmd.ExecuteScalar, "0"))

    End Function

    Private Function RicavaNotifiche() As Integer
        Dim NumeroNotifiche As Long = 0
        Dim literalNotifiche As Literal = CType(Me.Master.FindControl("RadNotificationNotifiche"), RadNotification).ContentContainer.FindControl("lblNotifiche")
        Dim TestoNotifiche As String = ""
        Try

            Dim s As String = ""
            par.OracleConn.Open()
            par.cmd = par.OracleConn.CreateCommand()
            If RicavaNumeroManutenzioniAlert() > 0 Then
                'lbl.Text = lbl.Text & "<br/>" & [String].Concat(New Object() {"<a href='#' onclick=" & Chr(34) & "javascript:ApriModuloStandard('SegnalazioniAlert.aspx');" & Chr(34) & "><table cellpadding='0' cellspacing='0' width='100%'><tr><td width='5%'><img alt='P' src='Immagini/info2.png'></td><td width='95%'>Segnalazioni alert ", " !</td></tr></table></a>"})
                'RicavaNotifiche = RicavaNotifiche + 1
                TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Segnalazioni a canone con tipologia modificata dal fornitore.", "location.href='SegnalazioniAlert.aspx';", False)
                NumeroNotifiche = NumeroNotifiche + 1
            End If
            If RicavaNumeroCronoProgramma() > 0 Then
                'lbl.Text = lbl.Text & "<br/>" & [String].Concat(New Object() {"<a href='#' onclick=" & Chr(34) & "javascript:ApriModuloStandard('SegnalazioniAlert.aspx');" & Chr(34) & "><table cellpadding='0' cellspacing='0' width='100%'><tr><td width='5%'><img alt='P' src='Immagini/info2.png'></td><td width='95%'>Segnalazioni alert ", " !</td></tr></table></a>"})
                'RicavaNotifiche = RicavaNotifiche + 1
                TestoNotifiche = par.CreaTabellaNotifica(TestoNotifiche, CM.Global.TipoNotifica.Urgenza, "Cronoprogrammi presenti.", "ApriModuloStandard('../FORNITORI/Home.aspx?FORNITORI=1','Fornitori');", False)
                NumeroNotifiche = NumeroNotifiche + 1
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            If NumeroNotifiche = 0 Then
                literalNotifiche.Text = ""
            Else
                literalNotifiche.Text = TestoNotifiche
                ScriptManager.RegisterStartupScript(Me, Me.[GetType](), "key", "function f(){NotificaTelerik('MasterPage_RadNotificationNotifiche', 'Sep@Com', ''); Sys.Application.remove_load(f);}Sys.Application.add_load(f);", True)
            End If

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Function
    Protected Sub OnCallbackUpdate(sender As Object, e As RadNotificationEventArgs)
        'If newMsgs = 5 OrElse newMsgs = 7 OrElse newMsgs = 8 OrElse newMsgs = 9 Then
        '    newMsgs = 0
        'End If
        'newMsgs = 100
        'lbl.Text = [String].Concat(New Object() {"You have ", newMsgs, " new messages!"})
        'RadNotification1.Value = newMsgs.ToString()
        RicavaNotifiche()
    End Sub




End Class
