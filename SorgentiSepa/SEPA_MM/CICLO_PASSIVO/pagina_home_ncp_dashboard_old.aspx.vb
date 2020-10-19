
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_pagina_home_ncp_dashboard_old
    Inherits System.Web.UI.Page
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String
    Dim sStringaSqlFiltri As String

    'MARIO
    Public lstAppalti As New System.Collections.Generic.List(Of Mario.Appalti)
    Public lstservizi As New System.Collections.Generic.List(Of Mario.VociServizi)
    Public lstprezzi As New System.Collections.Generic.List(Of Mario.ElencoPrezzi)
    Public lstscadenze As New System.Collections.Generic.List(Of Mario.ScadenzeManuali)

    '*** EPIFANI
    Public lstInterventi As New System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi)
    Public lstListaGenerale1 As New System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Public lstListaGenerale2 As New System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Dim lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)

    Public connData As CM.datiConnessione = Nothing
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            'Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If
        lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))
        connData = New CM.datiConnessione(par, False, False)
        ' Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")

        If Not IsPostBack Then
            imgSegnAperte.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltSegnAperte');")
            imgSegnAperte.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltSegnAperte');")

            imgSegnInCorso.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltSegnInCorso');")
            imgSegnInCorso.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltSegnInCorso');")

            imgSegnEvase.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltSegnEvase');")
            imgSegnEvase.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltSegnEvase');")

            imgSegnChiuse.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltSegnChiuse');")
            imgSegnChiuse.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltSegnChiuse');")

            imgODLEmessi.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltODLEmessi');")
            imgODLEmessi.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltODLEmessi');")

            imgODLEmessiNosegn.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltODLEmessiNoSegn');")
            imgODLEmessiNosegn.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltODLEmessiNoSegn');")

            imgSegnAperte30gg.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltSegnAperte30gg');")
            imgSegnAperte30gg.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltSegnAperte30gg');")

            imgODLBozzaNoEmessi.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltODLBozzaNoEmessi');")
            imgODLBozzaNoEmessi.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltODLBozzaNoEmessi');")

            imgODLConsNoCDP.Attributes.Add("onmouseover", "javascript:VisualizzaTooltipTelerik('tltOdlConsNoCDP');")
            imgODLConsNoCDP.Attributes.Add("onmouseout", "javascript:NascondiTooltipTelerik('tltOdlConsNoCDP');")

            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                btnDL.Visible = True
                If Session.Item("MOD_BUILDING_MANAGER") = "1" Then
                    btnBuildingManager.Visible = True
                Else
                    btnBuildingManager.Visible = False
                End If
                ' CaricaAppalti()
            Else
                btnDL.Visible = False
                btnBuildingManager.Visible = False
            End If
            HFGriglia.Value = dgvSegnalazioni.ClientID & "," _
                    & dgvODL.ClientID
            HFAltezzaFGriglie.Value = "450,400"
            'lblVersione.Text = "VERSIONE " & Mid(System.Configuration.ConfigurationManager.AppSettings("Versione"), 10)
            Session.LCID = 1040
            If Not IsNothing(lstListaRapporti) Then
                lstListaRapporti.Clear()
            End If
            'MARIO
            Session.Add("LSTSERVIZI", lstservizi)
            Session.Add("LSTAPPALTI", lstAppalti)
            Session.Add("LSTPREZZI", lstprezzi)
            Session.Add("LSTSCADENZE", lstscadenze)

            '*** EPIFANI
            Session.Add("LSTINTERVENTI", lstInterventi)
            Session.Add("LSTLISTAGENERALE1", lstListaGenerale1)
            Session.Add("LSTLISTAGENERALE2", lstListaGenerale2)

            'GiorniAp = ""
            'nGiorno = Format(Now, "dddd")
            'Select Case LCase(nGiorno)
            '    Case "lunedì", "monday"
            '        nGiorno = "1"
            '    Case "martedì", "tuesday"
            '        nGiorno = "2"
            '    Case "mercoledì", "wednesday"
            '        nGiorno = "3"
            '    Case "giovedì", "thursday"
            '        nGiorno = "4"
            '    Case "venerdì", "friday"
            '        nGiorno = "5"
            '    Case "sabato", "saturday"
            '        nGiorno = "6"
            '    Case "domenica", "sunday"
            '        nGiorno = "7"
            'End Select
            'nGiornoRif = System.Configuration.ConfigurationManager.AppSettings("Giorni")

            'If InStr(nGiornoRif, nGiorno) = 0 Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) < Val(System.Configuration.ConfigurationManager.AppSettings("OraAp") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If

            'If Val(Format(Hour(Now), "00") & Format(Minute(Now), "00")) > Val(System.Configuration.ConfigurationManager.AppSettings("OraCh") & "00") Then
            '    scriptblock = "<script language='javascript' type='text/javascript'>" _
            '                & "alert('ATTENZIONE, Il servizio SEPA@Web non è più disponibile!');" _
            '                & "</script>"
            '    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript310")) Then
            '        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript310", scriptblock)
            '    End If
            '    Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            '    Exit Sub
            'End If


            'Me.imgPagApprovare.Attributes.Add("onclick", "ApriApprovazione();")
            'Me.imgPagScad.Attributes.Add("onclick", "ApriScaduti();")
            'Me.segnalazioni.Attributes.Add("onclick", "ApriSegnalazioni();")


            Try


                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If

                If Session.Item("LIVELLO") = "1" Then
                    Session.Add("ID_STRUTTURA", "-1")
                Else
                    par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read = True Then
                        Session.Add("ID_STRUTTURA", myReader("ID_UFFICIO"))
                    End If
                    myReader.Close()
                End If
                par.caricaComboTelerik("SELECT ID, DESCRIZIONE FROM SISCOM_MI.PERICOLO_SEGNALAZIONI ORDER BY DESCRIZIONE ASC", cmbCriticita, "ID", "DESCRIZIONE", True)
                ImpostaKPI()
                If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                    VerificaDashBoard(1)

                    CaricaDashBoard()
                Else
                    VerificaDashBoard(0)
                End If


                'par.cmd.CommandText = "select PF_VOCI.ID_PIANO_FINANZIARIO from siscom_mi.pf_voci_STRUTTURA,SISCOM_MI.PF_VOCI where pf_voci_struttura.id_struttura=" & Session.Item("ID_STRUTTURA") & " And PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE And PF_VOCI_STRUTTURA.completo='0' and PF_VOCI_STRUTTURA.completo_aler='1' and PF_VOCI_STRUTTURA.valore_lordo_aler<>PF_VOCI_STRUTTURA.valore_lordo AND (PF_VOCI.ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ")) order by PF_VOCI.codice asc"


                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader1.HasRows = True Then
                '    If myReader1.Read Then
                '        imgEventi.Visible = True
                '        imgEventi.Attributes.Add("OnClick", "javascript:alert('ATTENZIONE...Delle voci del Piano finanziario non sono state approvate dal Gestore.\nVerrà ora mostrato il riepilogo.');window.location.href='CicloPassivo/Plan/ElencoNonApprovate.aspx?IDP=" & myReader1("id_piano_finanziario") & "';")
                '    End If

                'End If
                'myReader1.Close()

                PagamentiInScadenza(Session.Item("ID_STRUTTURA"))
                ' txtmessaggio.Text = Session.Item("ORARIO")
                ' Label3.Text = Session.Item("ORARIO")
                TrovaSegnalazioni()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try


        End If

        'par.OracleConn.Dispose()

    End Sub

    Private Sub PagamentiInScadenza(ByVal sStruttura As String)
        Try

            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            Dim giorni As Integer = 30


            'PAGAMENTI A CANONE CON ATTESTATO non STAMPATO
            'If sStruttura <> "-1" Then
            'par.cmd.CommandText = "select ID from SISCOM_MI.PAGAMENTI " _
            '                   & " where  DATA_STAMPA is null " _
            '                   & "   and TIPO_PAGAMENTO=6 " _
            '                   & "   and ID_STATO=1 " _
            '                   & "   and ID in (select ID_PAGAMENTO " _
            '                                 & " from SISCOM_MI.PRENOTAZIONI " _
            '                                 & " where ID_STATO=2 " _
            '                                 & "   and TIPO_PAGAMENTO=6 " _
            '                                 & "   and ID_STRUTTURA= " & sStruttura & ")"


            'myReader1 = par.cmd.ExecuteReader()

            'If myReader1.Read Then
            '    Me.imgPagScad.Visible = True
            'Else
            '    Me.imgPagScad.Visible = False
            'End If

            'myReader1.Close()
            '*************************************************

            'PAGAMENTI A CANONE DA APPROVARE in SCADENZA
            'par.cmd.CommandText = "select ID from SISCOM_MI.PRENOTAZIONI " _
            '                              & " where  (NVL(TO_DATE(DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
            '                              & "   and TIPO_PAGAMENTO=6 " _
            '                              & "   and ID_STATO=0 " _
            '                              & "   and  IMPORTO_PRENOTATO>0 " _
            '                              & "   and ID_STRUTTURA=" & sStruttura

            'myReader1 = par.cmd.ExecuteReader()

            'If myReader1.Read Then
            '    Me.imgPagApprovare.Visible = True
            'Else
            '    Me.imgPagApprovare.Visible = False
            'End If

            'myReader1.Close()
            'End If

            If ApertaNow Then connData.chiudi(False)


        Catch ex As Exception
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub


    Private Sub TrovaSegnalazioni()
        If Not IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") <> "-1" Then



            Try
                Dim sStr1 As String = ""
                Dim sOrder As String = ""


                Dim sStrID_TAB_FILIALI As String = ""
                Dim sStrID_ID_TIPOLOGIE As String = "-1"
                Dim sID_TIPO_ST As String = ""

                Dim sTipoRichiesta As String = "1" 'TIPO_RICHIESTA = 1  SEGNALAZIONI GUASTI prima era '0=GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE'



                Dim sFiliale As String = "-1"
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = Session.Item("ID_STRUTTURA")
                End If


                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If


                'sFiliale = 26 X LE PROVE

                If sFiliale <> "" Then

                    'par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
                    'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    'If myReader1.Read Then
                    '    sID_TIPO_ST = par.IfNull(myReader1("ID_TIPO_ST"), 0)
                    'End If
                    'myReader1.Close()


                    ''0=FILIALE AMMINISTRATIVA
                    ''1=FILIALE TECNICA
                    ''2=UFFICIO CENTRALE

                    'par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                    '                  & " where ID_TIPO_ST=" & sID_TIPO_ST

                    'If sID_TIPO_ST = 2 And sFiliale <> 64 And sFiliale <> 65 Then
                    '    '2=UFFICIO CENTRALE vede le segnalazioni di tutti i complessi però con ID_TIPOLOGIE fltrata anche per ID_STRUTTURA
                    '    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
                    'End If
                    'myReader1 = par.cmd.ExecuteReader
                    par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        sID_TIPO_ST = par.IfNull(myReader1("ID_TIPO_ST"), 0)
                    Else
                        sID_TIPO_ST = "-1"
                    End If
                    myReader1.Close()


                    '0=FILIALE AMMINISTRATIVA
                    '1=FILIALE TECNICA
                    '2=UFFICIO CENTRALE

                    par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                                      & " where ID_TIPO_ST=" & sID_TIPO_ST

                    If sID_TIPO_ST = "2" And sFiliale <> 64 And sFiliale <> 65 Then
                        '2=UFFICIO CENTRALE vede le segnalazioni di tutti i complessi però con ID_TIPOLOGIE fltrata anche per ID_STRUTTURA
                        par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
                    End If
                    myReader1 = par.cmd.ExecuteReader()


                    While myReader1.Read
                        If sStrID_ID_TIPOLOGIE = "" Then
                            sStrID_ID_TIPOLOGIE = myReader1(0)
                        Else
                            sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                        End If
                    End While
                    myReader1.Close()

                    If sID_TIPO_ST = 1 Then
                        par.cmd.CommandText = "select ID from SISCOM_MI.TAB_FILIALI where ID_TECNICA=" & sFiliale
                        myReader1 = par.cmd.ExecuteReader()

                        While myReader1.Read
                            If sStrID_TAB_FILIALI = "" Then
                                sStrID_TAB_FILIALI = myReader1(0)
                            Else
                                sStrID_TAB_FILIALI = sStrID_TAB_FILIALI & "," & myReader1(0)
                            End If
                        End While
                        myReader1.Close()
                        If sStrID_TAB_FILIALI = "" Then sStrID_TAB_FILIALI = "-1"
                    End If

                    If sID_TIPO_ST = 2 And sFiliale = 64 Then
                        sStrID_ID_TIPOLOGIE = ""
                        par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                                      & " where ID_TIPO_ST=0"

                        myReader1 = par.cmd.ExecuteReader()

                        While myReader1.Read
                            If sStrID_ID_TIPOLOGIE = "" Then
                                sStrID_ID_TIPOLOGIE = myReader1(0)
                            Else
                                sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                            End If
                        End While
                        myReader1.Close()



                        sStrID_TAB_FILIALI = "1,6,9"

                    End If

                    If sID_TIPO_ST = 2 And sFiliale = 65 Then
                        sStrID_ID_TIPOLOGIE = ""
                        par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
                                             & " where ID_TIPO_ST=0"
                        myReader1 = par.cmd.ExecuteReader()

                        While myReader1.Read
                            If sStrID_ID_TIPOLOGIE = "" Then
                                sStrID_ID_TIPOLOGIE = myReader1(0)
                            Else
                                sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
                            End If
                        End While
                        myReader1.Close()

                        sStrID_TAB_FILIALI = "2,8,22"

                    End If
                    sStr1 = sStr1 & " select SEGNALAZIONI.ID, " _
                                & " ID_COMPLESSO as IDENTIFICATIVO," _
                                & " 'C' as TIPO_S," _
                                & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                         & " from SISCOM_MI.SEGNALAZIONI " _
                         & " where SEGNALAZIONI.ID_STATO=0" _
                         & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                         & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
                         & "   and SEGNALAZIONI.ID_COMPLESSO is NOT NULL " _
                         & "   and SEGNALAZIONI.ID_EDIFICIO is NULL " _
                         & "   and SEGNALAZIONI.ID_UNITA is NULL "

                    If sID_TIPO_ST = 0 Then
                        '0=FILIALE AMMINISTRATIVA vede le segnalazioni con i complessi della propria filiale (come era la logica di prima)
                        sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                       & " where ID_FILIALE=" & sFiliale & ") "
                    ElseIf sID_TIPO_ST = 1 Then
                        '1=FILIALE TECNICA   vede tutti  complessi della propria filiale più quelle dope TAB_FILALE.ID_TECNICA è uguale alla propria FILIALE
                        sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                              & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ") ) "
                    End If


                    '& "   and SEGNALAZIONI.ID_COMPLESSO is NULL " 
                    'EDIFICIO
                    sStr1 = sStr1 & " union select SEGNALAZIONI.ID, " _
                                & " ID_EDIFICIO as IDENTIFICATIVO," _
                                & " 'E' as TIPO_S," _
                                & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                         & " from SISCOM_MI.SEGNALAZIONI " _
                         & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=0" _
                         & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                         & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
                         & "   and SEGNALAZIONI.ID_EDIFICIO is NOT NULL " _
                         & "   and SEGNALAZIONI.ID_UNITA is NULL "

                    If sID_TIPO_ST = 0 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                      & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                             & " where ID_FILIALE=" & sFiliale & ") ) "

                    ElseIf sID_TIPO_ST = 1 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                      & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                             & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "

                    ElseIf sID_TIPO_ST = 2 And (sFiliale = 64 Or sFiliale = 65) Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                              & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                    & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "

                    End If

                    'UNITA
                    '& "   and SEGNALAZIONI.ID_COMPLESSO Is not NULL " _
                    '& "   and SEGNALAZIONI.ID_EDIFICIO Is not NULL " 
                    sStr1 = sStr1 & " union select SEGNALAZIONI.ID, " _
                                & " ID_UNITA as IDENTIFICATIVO," _
                                & " 'U' as TIPO_S," _
                                & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
                         & " from SISCOM_MI.SEGNALAZIONI " _
                         & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=0" _
                         & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
                         & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
                         & "   and SEGNALAZIONI.ID_UNITA is NOT NULL "


                    If sID_TIPO_ST = 0 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                         & " where ID_FILIALE=" & sFiliale & ") ))"

                    ElseIf sID_TIPO_ST = 1 Then
                        sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                            & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                         & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ))"
                    End If



                Else

                    sStr1 = "select SEGNALAZIONI.ID," _
                                & " case when ID_UNITA      is not null then ID_UNITA " _
                                      & "when ID_EDIFICIO   is not null then ID_EDIFICIO " _
                                      & "when ID_COMPLESSO  is not null then ID_COMPLESSO " _
                                & " end as IDENTIFICATIVO," _
                                & " case when ID_UNITA      is not null then 'U' " _
                                     & " when ID_EDIFICIO   is not null then 'E' " _
                                     & " when ID_COMPLESSO  is not null then 'C' " _
                                & " end as TIPO_S,COGNOME_RS||' '||NOME as RICHIEDENTE," _
                                & "to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO," _
                                & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC " _
                            & " from SISCOM_MI.SEGNALAZIONI " _
                            & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=0" _
                            & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta


                End If

                sStr1 = sStr1 & sOrder

                '*** CARICO LA GRIGLIA
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStr1, par.OracleConn)
                Dim dt As New Data.DataTable()

                da.Fill(dt)


                'If dt.Rows.Count >= 1 Then
                '    Me.segnalazioni.Visible = True
                'Else
                '    segnalazioni.Visible = False
                'End If



                If ApertaNow Then connData.chiudi(False)


            Catch ex As Exception
                connData.chiudi(False)

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try
        Else

            '   segnalazioni.Visible = False

        End If

    End Sub

    Private Sub VerificaDashBoard(ByVal tipologia As Integer)
        Try
            If tipologia = 0 Then
                tblSegnalazioni.ColSpan = 2
                tblOrdini.Visible = False
                dgvODL.Visible = False
                btnVisualizzaManutenzioni.Visible = False
                lblNumODLNoSegn.Visible = False
                lblOdlEmessiNoSegn.Visible = False
                imgODLEmessiNosegn.Visible = False
            Else
                'tblSegnalazioni.ColSpan = 2
                tblOrdini.Visible = True
                'imgLogo.Visible = False
                'dgvSegnalazioni.Visible = True
                'fieldSetSegnalazioni.Visible = True
                'fieldSetOdl.Visible = True
                'dgvODL.Visible = True
                'tblKPI.Visible = True
                'fieldSetKPI.Visible = True
            End If

        Catch ex As Exception

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub CaricaDashBoard()
        Try
            Dim FlagConnessione As Boolean = False
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If




            If ApertaNow Then connData.chiudi(False)
        Catch ex As Exception
            '************CHIUSURA CONNESSIONE**********


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub dgvSegnalazioni_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvSegnalazioni.NeedDataSource
        Try
            Dim Query As String = EsportaQuerySegnalazioni()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            dgvSegnalazioni.DataSource = dt
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub


    Private Function EsportaQuerySegnalazioni() As String
        Dim filtroDL As String = ""
        If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" AndAlso buildingManager.Value = "1" Then
            filtroDL = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
        ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
            filtroDL = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
        ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
            filtroDL = " and segnalazioni.id in " _
                     & " (select id_segnalazioni from siscom_mi.manutenzioni where id_appalto in  " _
                     & " (select id from siscom_mi.appalti where id_gruppo in  " _
                     & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "'))) "

        End If
        Dim filtroStato As String = ""
        Dim filtroStatoSegnalazioni As String = ""
        If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
            filtroStato = " AND MANUTENZIONI.STATO IN (0,1,2) "
            filtroStatoSegnalazioni = "0,6,7"
        ElseIf Session.Item("FL_CP_TECN_AMM") = "1" Then
            filtroStato = " AND MANUTENZIONI.STATO IN (0) "
            filtroStatoSegnalazioni = "0,6,7"
        ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" And Session.Item("FL_CP_TECN_AMM") = "0" Then
            filtroStato = " AND MANUTENZIONI.STATO IN (0) "
            filtroStatoSegnalazioni = "0"

        End If
        Dim filtroOrdiniBozza As String = ""
        If ordiniBozza.Value = "1" Then
            ordiniEmesso.Value = "0"
            'ordiniBozza.Value = "0"
            filtroOrdiniBozza = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE STATO = 0)"
            dgvODL.Rebind()
        End If
        Dim filtroOrdiniEmesso As String = ""
        If ordiniEmesso.Value = "1" Then
            ordiniBozza.Value = "0"
            'ordiniEmesso.Value = "0"
            filtroOrdiniEmesso = " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONI FROM SISCOM_MI.MANUTENZIONI WHERE STATO = 1)"
            dgvODL.Rebind()
        End If
        Dim filtroStatoSegnalazione As String = ""
        If segnAperte.Value = "1" Then
            segnAperte.Value = "0"
            filtroStatoSegnalazione = " and segnalazioni.id_stato = 0"
        End If
        If segnInCorso.Value = "1" Then
            segnInCorso.Value = "0"
            filtroStatoSegnalazione = " and segnalazioni.id_stato = 6"
        End If
        If segnEvase.Value = "1" Then
            segnEvase.Value = "0"
            filtroStatoSegnalazione = " and segnalazioni.id_stato = 7"
        End If
        If segnChiuse.Value = "1" Then
            segnChiuse.Value = "0"
            filtroStatoSegnalazione = " and segnalazioni.id_stato = 10"
        End If
        If allSegn.Value = "1" Then
            allSegn.Value = "0"
            filtroStatoSegnalazione = ""
        End If
        Dim filtroCriticita As String = ""
        If segn30gg.Value = "1" Then
            segn30gg.Value = "0"
            filtroCriticita = " AND TO_DATE(SUBSTR(TO_CHAR(SYSDATE,'YYYYMMDD'),1,8),'YYYYMMDD') - TO_DATE(SUBSTR (DATA_ORA_RICHIESTA, 1, 8),'YYYYMMDD') > 30 "
        End If
        sStringaSqlFiltri = " FROM SISCOM_MI.TAB_STATI_SEGNALAZIONI, " _
                            & " SISCOM_MI.SEGNALAZIONI, " _
                            & " SISCOM_MI.TAB_FILIALI, " _
                            & " SISCOM_MI.EDIFICI, " _
                            & " SISCOM_MI.UNITA_IMMOBILIARI, " _
                            & " SISCOM_MI.TIPOLOGIE_GUASTI, " _
                            & " OPERATORI " _
                            & " WHERE TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO " _
                            & " AND SEGNALAZIONI.ID_STATO  IN (" & filtroStatoSegnalazioni & ") " _
                            & " AND SEGNALAZIONI.ID_STRUTTURA = TAB_FILIALI.ID(+) " _
                            & " AND SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID(+) " _
                            & " AND SISCOM_MI.UNITA_IMMOBILIARI.ID(+) = SISCOM_MI.SEGNALAZIONI.ID_UNITA " _
                            & " AND OPERATORI.ID = SEGNALAZIONI.ID_OPERATORE_INS " _
                            & " AND SEGNALAZIONI.ID_TIPOLOGIE = TIPOLOGIE_GUASTI.ID(+) " _
                            & " AND ID_SEGNALAZIONE_PADRE IS NULL  AND ID_TIPO_SEGNALAZIONE = 1 " _
                            & filtroDL

        EsportaQuerySegnalazioni = " SELECT 'false' as CHECK1, SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,0) AS BUILDING_MANAGER, " _
                            & "(SELECT COUNT(*) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = SEGNALAZIONI.ID " & filtroStato & ") AS NUM_MANUTENZIONI, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                            & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (SELECT MAX (COD_CONTRATTO) " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                            & " WHERE ID IN " _
                            & " (SELECT ID_CONTRATTO " _
                            & " FROM SISCOM_MI. " _
                            & " UNITA_CONTRATTUALE " _
                            & " WHERE UNITA_CONTRATTUALE. " _
                            & " ID_UNITA = " _
                            & " SEGNALAZIONI.ID_UNITA) " _
                            & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                            & " 1, " _
                            & " 8) " _
                            & "  BETWEEN NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_DECORRENZA, " _
                            & " '10000000') " _
                            & " AND NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_RICONSEGNA, " _
                            & " '30000000')) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & ",ID_sEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE " _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA, ID_TIPO_SEGNALAZIONE AS TIPOLOGIA " _
                            & sStringaSqlFiltri & filtroOrdiniBozza & filtroOrdiniEmesso & filtroStatoSegnalazione & filtroCriticita _
                            & "  union " _
                            & " SELECT 'false' as CHECK1, SEGNALAZIONI.ID, " _
                            & " SEGNALAZIONI.ID AS NUM, " _
                            & " SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,0) AS BUILDING_MANAGER, " _
                            & "(SELECT COUNT(*) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = SEGNALAZIONI.ID  " & filtroStato & ") AS NUM_MANUTENZIONI, " _
                            & " SEGNALAZIONI.ID_TIPO_SEGNALAZIONE AS TIPO, " _
                             & " (case when segnalazioni.id_tipo_segnalazione=1 then tipologie_guasti.descrizione else null end) AS tipo_int, " _
                            & " (SELECT DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE TIPO_SEGNALAZIONE.ID=ID_TIPO_SEGNALAZIONE) AS TIPO0," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_1 WHERE TIPO_SEGNALAZIONE_LIVELLO_1.ID=ID_TIPO_SEGN_LIVELLO_1) AS TIPO1," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_2 WHERE TIPO_SEGNALAZIONE_LIVELLO_2.ID=ID_TIPO_SEGN_LIVELLO_2) AS TIPO2," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_3 WHERE TIPO_SEGNALAZIONE_LIVELLO_3.ID=ID_TIPO_SEGN_LIVELLO_3) AS TIPO3," _
                            & " (SELECT REPLACE(DESCRIZIONE, '#','') AS DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE_LIVELLO_4 WHERE TIPO_SEGNALAZIONE_LIVELLO_4.ID=ID_TIPO_SEGN_LIVELLO_4) AS TIPO4, " _
                            & " TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, segnalazioni.id_stato, " _
                            & " EDIFICI.DENOMINAZIONE AS INDIRIZZO, " _
                            & " COGNOME_RS || ' ' || SEGNALAZIONI.NOME AS RICHIEDENTE, " _
                            & " (CASE " _
                            & " WHEN SEGNALAZIONI.ID_UNITA " _
                            & " IS NOT NULL " _
                            & " THEN " _
                            & " (SELECT MAX (COD_CONTRATTO) " _
                            & " FROM SISCOM_MI.RAPPORTI_UTENZA " _
                            & " WHERE ID IN " _
                            & " (SELECT ID_CONTRATTO " _
                            & " FROM SISCOM_MI. " _
                            & " UNITA_CONTRATTUALE " _
                            & " WHERE UNITA_CONTRATTUALE. " _
                            & " ID_UNITA = " _
                            & " SEGNALAZIONI.ID_UNITA) " _
                            & " AND SUBSTR (SEGNALAZIONI.DATA_ORA_RICHIESTA, " _
                            & " 1, " _
                            & " 8) " _
                            & "  BETWEEN NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_DECORRENZA, " _
                            & " '10000000') " _
                            & " AND NVL ( " _
                            & " RAPPORTI_UTENZA. " _
                            & " DATA_RICONSEGNA, " _
                            & " '30000000')) " _
                            & " ELSE " _
                            & " NULL " _
                            & " END) " _
                            & " AS CODICE_RU, " _
                            & "(select count(*) from siscom_mi.segnalazioni_note where id_segnalazione = segnalazioni.id and sollecito = 1) as n_solleciti, " _
                            & " TO_CHAR (TO_DATE (SUBSTR (DATA_ORA_RICHIESTA, 1, 8), 'YYYYMMDD'),'DD/MM/YYYY') AS DATA_INSERIMENTO, " _
                            & " REPLACE (SEGNALAZIONI.DESCRIZIONE_RIC, '''', '') AS DESCRIZIONE, " _
                            & " NVL (SISCOM_MI.TAB_FILIALI.NOME, ' ') AS FILIALE, " _
                            & " (CASE WHEN ID_STATO = 10 THEN (SELECT distinct NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = SEGNALAZIONI.ID AND " _
                            & " id_tipo_segnalazione_note=2 AND data_ora = (SELECT MAX(data_ora) FROM siscom_mi.segnalazioni_note WHERE id_segnalazione = segnalazioni.ID AND id_tipo_segnalazione_note=2)" _
                            & " ) ELSE '' END) AS NOTE_C,id_pericolo_Segnalazione " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI " _
                            & " ,(SELECT COUNT(ID) FROM SISCOM_MI.SEGNALAZIONI B WHERE B.ID_SEGNALAZIONE_PADRE=SEGNALAZIONI.ID) AS FIGLI2 " _
                            & " ,ID_SEGNALAZIONE_PADRE AS ID_sEGNALAZIONE_PADRE" _
                            & ",(SELECT MAX(SISCOM_MI.GETDATA(DATA_INIZIO_ORDINE)) FROM SISCOM_MI.MANUTENZIONI WHERE stato not in (5,6) and ID_SEGNALAZIONI=SEGNALAZIONI.ID) AS DATA_EMISSIONE " _
                            & ",SISCOM_MI.GETDATA(DATA_CHIUSURA) AS DATA_CHIUSURA " _
                            & " ,DATA_ORA_RICHIESTA,ID_TIPO_SEGNALAZIONE AS TIPOLOGIA " _
                            & " FROM siscom_mi.tab_stati_segnalazioni, " _
                            & " siscom_mi.segnalazioni, " _
                            & " siscom_mi.tab_filiali, " _
                            & " siscom_mi.edifici, " _
                            & " siscom_mi.unita_immobiliari, " _
                            & " siscom_mi.TIPOLOGIE_GUASTI, " _
                            & " OPERATORI " _
                            & " WHERE tab_stati_segnalazioni.ID = segnalazioni.id_stato " _
                            & " AND segnalazioni.id_stato in (" & filtroStatoSegnalazioni & ") " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & " AND siscom_mi.segnalazioni.id_edificio = siscom_mi.edifici.ID(+) " _
                            & " AND siscom_mi.unita_immobiliari.ID(+) = siscom_mi.segnalazioni.id_unita " _
                            & " AND OPERATORI.ID = segnalazioni.id_operatore_ins " _
                            & " AND segnalazioni.id_tipologie = TIPOLOGIE_GUASTI.ID(+) " _
                            & " AND SEGNALAZIONI.ID IN (SELECT ID_SEGNALAZIONE_PADRE " _
                            & " FROM SISCOM_MI.SEGNALAZIONI " _
                            & " ,siscom_mi.tab_filiali " _
                            & " WHERE ID_SEGNALAZIONE_PADRE IS NOT NULL " _
                            & " AND segnalazioni.id_struttura = tab_filiali.ID(+) " _
                            & ") AND ID_TIPO_SEGNALAZIONE = 1 " _
                            & filtroDL & filtroOrdiniBozza & filtroOrdiniEmesso & filtroStatoSegnalazione & filtroCriticita
        'EsportaQuery = "SELECT ID FROM SISCOM_MI.SEGNALAZIONI " _
        '    & " WHERE siscom_mi.GETBUILDINGMANAGERSEGNALAZIONI(segnalazioni.id,1) = '" & Session.Item("ID_OPERATORE") & "'" _
        '    & " AND ID_TIPO_SEGNALAZIONE = 1 AND ID_STATO IN (0,6,7)"
    End Function

    Private Sub dgvSegnalazioni_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles dgvSegnalazioni.ItemDataBound
        If e.Item.ItemType = GridItemType.Item Or e.Item.ItemType = GridItemType.AlternatingItem Then
            Dim dataItem As GridDataItem = TryCast(e.Item, GridDataItem)
            If dataItem("TIPOLOGIA").Text = "1" Then
                Select Case dataItem("ID_PERICOLO_SEGNALAZIONE").Text
                    Case "1"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-white-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "2"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-green-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "3"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-yellow-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "4"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-red-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case "0"
                        dataItem("TIPO_INT").Text = "<table style=""font-family:Arial;font-size:8pt;"">" _
                            & "<tr><td><img src=""CicloPassivo/MANUTENZIONI/Immagini/Ball-blue-128.png"" alt="""" height=""16"" width=""16"" /></td>" _
                            & "<td>" & dataItem("TIPO_INT").Text & "</td></<tr></table>"
                    Case Else
                End Select
                'CType(dataItem("DESCRIZIONE"), TableCell).ToolTip = CType(dataItem("DESCRIZIONE"), TableCell).Text
                'If Trim(Len(CType(dataItem("DESCRIZIONE"), TableCell).Text)) > 50 Then
                '    CType(dataItem("DESCRIZIONE"), TableCell).Text = Mid(CType(dataItem("DESCRIZIONE"), TableCell).Text, 1, 50) & "..."
                'End If
            End If

        End If
        If TypeOf e.Item Is GridFilteringItem Then
            Dim fitem As GridFilteringItem = DirectCast(e.Item, GridFilteringItem)
            par.caricaComboTelerik("SELECT DISTINCT TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS DESCRIZIONE " & sStringaSqlFiltri _
              & " order by   1 ASC", TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoOrdine"), RadComboBox), "DESCRIZIONE", "DESCRIZIONE", True, "Tutti", "Tutti")
            Dim altro1 As New RadComboBoxItem
            altro1.Value = "NON DEFINITO"
            altro1.Text = "NON DEFINITO"
            'TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoSal"), RadComboBox).Items.Add(altro1)
            If Not String.IsNullOrEmpty(Trim(HFFiltroEventoStatoOrdine.Value.ToString)) Then
                TryCast(TryCast(e.Item, GridFilteringItem).FindControl("RadComboBoxStatoOrdine"), RadComboBox).SelectedValue = HFFiltroEventoStatoOrdine.Value.ToString
            End If
        End If
    End Sub

    Private Sub btnPrendiInCarico_Click(sender As Object, e As EventArgs) Handles btnPrendiInCarico.Click
        Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQuerySegnalazioni())
        CheckBoxSegnalazioni(dt)
        Dim dtFiltrata As New Data.DataTable
        Dim view As New Data.DataView(dt)
        view.RowFilter = "CHECK1 = 'TRUE'"
        dtFiltrata = view.ToTable
        If dtFiltrata.Rows.Count > 0 Then
            For Each riga As Data.DataRow In dtFiltrata.Rows
                If riga.Item("ID_STATO") = "0" Then
                    Salva(6, riga.Item("ID"))
                End If
            Next
            RadNotificationNote.Show()
            dgvSegnalazioni.Rebind()
        Else
            RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
        End If

        'If Not String.IsNullOrEmpty(idSelected.Value) Then
        '    If idStatoSegnalazione.Value = "0" Then
        '        If Salva(6) = True Then
        '            RadNotificationNote.Show()
        '            dgvSegnalazioni.Rebind()
        '        End If
        '    Else
        '        RadWindowManager1.RadAlert("E\' possibile prendere in carico esclusivamente segnalazioni in stato <strong>aperta</strong>!", 300, 150, "Attenzione", "", "null")
        '    End If

        'Else
        '    RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
        'End If

    End Sub

    Function Salva(ByVal Stato As Integer, ByVal id As String) As Boolean

        Try
            Salva = False
            ' APRO CONNESSIONE
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Session.Add("LAVORAZIONE", "1")
            'If Stato = 1 Then
            '    par.cmd.CommandText = "select * from siscom_mi.segnalazioni_sopralluogo where id_segnalazione = " & idSelected.Value
            '    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            '    If lettore.Read Then
            '        Stato = Stato
            '    Else
            '        Stato = 0
            '    End If
            '    lettore.Close()
            'End If
            Dim dataInCarico As String = ""
            If Stato = 1 Then
                dataInCarico = " , data_in_carico = '" & Format(Now, "yyyyMMdd") & "'"
            End If
            ' SEGNALAZIONI
            par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=" & Stato & dataInCarico & " where ID=" & id
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & id & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','APERTA','IN CORSO')"
            par.cmd.ExecuteNonQuery()
            WriteEvent("F02", "AGGIORNAMENTO SEGNALAZIONE", id)
            '************************************
            Salva = True
            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then connData.chiudi(True)
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Salva = False
            '************CHIUSURA CONNESSIONE**********
            connData.chiudi(False)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String, ByVal idSegnalazione As String)
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES ( " & idSegnalazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
            & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"
            par.cmd.ExecuteNonQuery()
            If ApertaNow Then connData.chiudi(True)
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnProcediEF_Click(sender As Object, e As EventArgs) Handles btnProcediEF.Click
        Try
            If Me.cmbEsercizio.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare  l\'esercizio finanaziario!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbServizio.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare il Servizio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbServizioVoce.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare la voce DGR!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If

            If Me.cmbAppalto.SelectedValue = "-1" Then
                Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                RadWindowManager1.RadAlert("Selezionare il numero di repertorio!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If
            If txtannullo.Value = "1" Then

                txtannullo.Value = "0"
                connData.chiudi(True)
                Session.Add("ID", 0)
                Response.Write("<script>location.replace('CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?SE=" & Me.cmbServizio.SelectedValue.ToString _
                                                                        & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                        & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                        & "&CO=" & idSelected.Value _
                                                                        & "&TIPOR=0" _
                                                                        & "&ED=" & statoSegnalazione.Value _
                                                                        & "&NUOVA=1 " _
                                                                        & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                       & "&PROVENIENZA=SEGNALAZIONI&NASCONDIINDIETRO=1" & "');</script>")
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaEsercizio()

        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try
            ' APRO CONNESSIONE
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Me.cmbEsercizio.Items.Clear()
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE , SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN ,SISCOM_MI.PF_STATI " _
                               & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                               & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                               & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by 1 desc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                Me.cmbEsercizio.Items.Add(New RadComboBoxItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))

            End While
            myReader1.Close()
            'par.caricaComboTelerik(par.cmd.CommandText, cmbEsercizio, "ID", "STATO")

            If ApertaNow Then connData.chiudi(False)

            Me.cmbEsercizio.Enabled = True
            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                'Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                '  Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)


                Me.cmbServizio.Enabled = True
                Me.cmbServizioVoce.Enabled = True
                Me.cmbAppalto.Enabled = True
                CaricaServizi()
            End If



        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try

            Me.txtSTATO_PF.Value = -1

            If ID_ESERCIZIO < 0 Then Exit Sub

            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                Me.txtSTATO_PF.Value = par.IfNull(myReaderF("ID_STATO"), -1)
            End If
            myReaderF.Close()

            par.cmd.Parameters.Clear()

            If ApertaNow Then connData.chiudi(False)

        Catch ex As Exception
            ' Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaServizi()


        Try

            Me.cmbServizio.Items.Clear()
            ' Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            '    Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbEsercizio.SelectedValue <> "-1" Then

                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If



                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                End If

                Me.cmbServizio.Items.Clear()
                '  Me.cmbServizio.Items.Add(New ListItem(" ", -1))


                Select Case Me.tipo.Value
                    Case "C"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "


                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                               & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                               & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                     & " where ID_COMPLESSO=" & Me.identificativo.Value & ") ) ) " _
                                          & " order by DESCRIZIONE asc"

                    Case "E"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                               & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                               & " where ID_EDIFICIO=" & Me.identificativo.Value & ") ) " _
                                          & " order by DESCRIZIONE asc"

                    Case "U"
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                           & " from SISCOM_MI.TAB_SERVIZI " _
                                           & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                        & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                        & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                        & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                           & " from SISCOM_MI.LOTTI " _
                                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                        & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        End Select
                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                                                                  & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                                                   & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                         & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & Me.identificativo.Value & ")))) " _
                                          & " order by DESCRIZIONE asc"
                    Case Else

                        'par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                        '                   & " from SISCOM_MI.TAB_SERVIZI " _
                        '                   & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                        '                                & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                        '                                & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                        '                                                                   & " from SISCOM_MI.LOTTI " _
                        '                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                        '                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                        'Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                        '    Case 6
                        '        If Session.Item("FL_COMI") <> 1 Then
                        '            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        '        End If
                        '    Case 7
                        '        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                        'End Select
                        'par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') " _
                        '                                          & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                        '                                                                           & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                                           & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                        '                                                                                                 & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI where ID=" & Me.identificativo.Value & ")))) " _
                        '                  & " order by DESCRIZIONE asc"

                End Select
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)

                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                'End While
                'myReader1.Close()

                Me.cmbServizio.SelectedValue = "-1"
                '**************************
                If ApertaNow Then connData.chiudi(False)
            End If


        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        CaricaServizi()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub btnManutenzione_Click(sender As Object, e As EventArgs) Handles btnManutenzione.Click
        Try
            If Not String.IsNullOrEmpty(idSelected.Value) Then
                If idStatoSegnalazione.Value <= "6" Then
                    Dim ApertaNow As Boolean = False
                    If Not IsNothing(Me.connData) Then
                        Me.connData.RiempiPar(par)
                    Else
                        connData.apri(False)
                        ApertaNow = True
                    End If

                    par.cmd.CommandText = "SELECT ID_UNITA, ID_EDIFICIO, ID_COMPLESSO FROM SISCOM_MI.SEGNALAZIONI WHERE ID = " & idSelected.Value
                    Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                    For Each riga As Data.DataRow In dt.Rows
                        'CONTROLLO UNITA
                        If par.IfNull(riga.Item("ID_UNITA"), "-1") <> "-1" Then
                            tipo.Value = "U"
                            identificativo.Value = par.IfNull(riga.Item("ID_UNITA"), "-1")
                            'CONTROLLO EDIFICIO
                        ElseIf par.IfNull(riga.Item("ID_EDIFICIO"), "-1") <> "-1" Then
                            tipo.Value = "E"
                            identificativo.Value = par.IfNull(riga.Item("ID_EDIFICIO"), "-1")
                        Else
                            tipo.Value = "C"
                            identificativo.Value = par.IfNull(riga.Item("ID_COMPLESSO"), "-1")
                        End If
                    Next

                    CaricaEsercizio()
                    If ApertaNow Then connData.chiudi(False)
                    Dim Script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show();Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    If Not String.IsNullOrWhiteSpace(Script) Then
                        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", Script, True)
                    End If
                Else
                    RadWindowManager1.RadAlert("Verificare che la segnalazione selezionata non sia in stato <strong>evasa</strong> o <strong>chiusa</strong>!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            connData.chiudi(False)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged
        FiltraDettaglio()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub FiltraDettaglio()
        Dim sWhere As String = ""
        Dim i As Integer = 0


        Try


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbServizioVoce.Items.Clear()
            '  Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            '  Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizio.SelectedValue <> "-1" Then


                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If



                par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                    & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                     & "where  PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                  & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                  & " where  ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )" _
                                                                  & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO<>15" _
                                     & "  and PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                       & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")"

                Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & "  and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                     & " order by DESCRIZIONE asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                '    i = i + 1
                'End While
                'myReader1.Close()
                '**************************
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizioVoce, "ID", "DESCRIZIONE", True)
                If ApertaNow Then connData.chiudi(False)

                If i = 2 Then
                    cmbServizioVoce.Items(1).Selected = True
                    FiltraAppalti()
                End If

            End If


        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Private Sub FiltraAppalti()
        Dim i As Integer = 0


        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If


            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                par.cmd.CommandText = " select ID,TRIM(NUM_REPERTORIO) AS NUM_REPERTORIO,TRIM(NUM_REPERTORIO) || ' - ' || (select ragione_sociale from siscom_mi.fornitori where fornitori.id=appalti.id_fornitore) as fornitore " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                 & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                 & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
                                    & "  and ID_STATO=1" _
                                    & " order by NUM_REPERTORIO "

            Else

                If ApertaNow Then connData.chiudi(False)
                Exit Sub
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "FORNITORE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    i = i + 1
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " ") & "-" & par.IfNull(myReader1("FORNITORE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While

            'myReader1.Close()

            If ApertaNow Then connData.chiudi(False)

            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If


        Catch ex As Exception
            connData.chiudi(False)

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub cmbServizioVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizioVoce.SelectedIndexChanged
        FiltraAppalti()
        Dim script As String = "function f(){$find(""" + RadWindowEmOrdine.ClientID + """).show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
        RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
    End Sub

    Private Sub btnChiudiEF_Click(sender As Object, e As EventArgs) Handles btnChiudiEF.Click
        cmbEsercizio.ClearSelection()
        cmbServizio.ClearSelection()
        cmbServizioVoce.ClearSelection()
        cmbAppalto.ClearSelection()
    End Sub

    Private Sub dgvODL_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles dgvODL.NeedDataSource
        Try
            'If Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Then
            '    If Not String.IsNullOrEmpty(idSelected.Value) Then
            '        Dim Query As String = EsportaQueryODL()
            '        Dim dt As New Data.DataTable
            '        dt = par.getDataTableGrid(Query)
            '        dgvODL.DataSource = dt
            '    End If
            'Else
            '    'If Not String.IsNullOrEmpty(idSelected.Value) Then
            '    Dim Query As String = EsportaQueryODL()
            '    Dim dt As New Data.DataTable
            '    dt = par.getDataTableGrid(Query)
            '    dgvODL.DataSource = dt
            '    'End If
            'End If


            Dim Query As String = EsportaQueryODL()
            Dim dt As New Data.DataTable
            dt = par.getDataTableGrid(Query)
            dgvODL.DataSource = dt

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Function EsportaQueryODL(Optional ByVal lista As String = "") As String
        Dim filtroIdManutenzioni As String = ""
        If Not String.IsNullOrEmpty(lista) Then
            filtroIdManutenzioni = " and manutenzioni.id in (" & lista & ") "
        End If
        Dim filtroDL As String = ""
        If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" AndAlso buildingManager.Value = "1" Then
            filtroDL = " AND SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
        ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
            filtroDL = " AND SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
        ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
            filtroDL = " and id_segnalazioni in " _
                     & " (select id_segnalazioni from siscom_mi.manutenzioni where id_appalto in  " _
                     & " (select id from siscom_mi.appalti where id_gruppo in  " _
                     & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "'))) "
        End If
        Dim filtroStato As String = ""
        Dim filtroSegnalazione As String = ""
        Dim filtroAppalto As String = ""
        If ODLBozzaNonEmessi.Value = "1" Then
            filtroStato = " MANUTENZIONI.STATO IN (0) "
            filtroDL = " and id_appalto in  " _
                     & " (select id from siscom_mi.appalti where id_gruppo in  " _
                     & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "
            'ODLBozzaNonEmessi.Value = "0"
        ElseIf ODLEmessiNoCons.Value = "1" Then
            filtroStato = "  MANUTENZIONI.STATO IN (1) "
            filtroDL = " and id_appalto in  " _
                     & " (select id from siscom_mi.appalti where id_gruppo in  " _
                     & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "
            'ODLEmessiNoCons.Value = "0"
        ElseIf ODLConsNoCDP.Value = "1" Then
            filtroStato = "  MANUTENZIONI.STATO IN (2) "
            filtroDL = " and id_appalto in  " _
                     & " (select id from siscom_mi.appalti where id_gruppo in  " _
                     & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "
            'ODLConsNoCDP.Value = "0"
        Else
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                filtroStato = "  MANUTENZIONI.STATO IN (0,1,2) "
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
                filtroStato = "  MANUTENZIONI.STATO IN (0) "
            End If

            If par.IfEmpty(idSelected.Value, "-1") <> "-1" Then
                filtroSegnalazione = " and id_segnalazioni = " & par.IfEmpty(idSelected.Value, "-1")
            Else
                filtroDL = " and id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) and id_segnalazioni is null"
            End If
            'filtroAppalto = " AND MANUTENZIONI.ID_APPALTO = " & par.IfEmpty(cmbRepertorio.SelectedValue, "-1")
            'filtroAppalto = " AND MANUTENZIONI.ID_APPALTO in (select id from appalti where id_gruppo = (SELECT ID_GRUPPO FROM APPALTI WHERE ID =" & par.IfEmpty(cmbRepertorio.SelectedValue, "-1") & " ) )"
        End If
        filtroAppalto = " AND MANUTENZIONI.ID_APPALTO in (select id from appalti where id_gruppo = (SELECT ID_GRUPPO FROM APPALTI WHERE ID =" & par.IfEmpty(cmbRepertorio.SelectedValue, "-1") & " ) )"

        Dim filtroOrdiniBozza As String = ""
        If ordiniBozza.Value = "1" Then
            ordiniEmesso.Value = "0"
            'ordiniBozza.Value = "0"
            filtroStato = " STATO = 0"
        End If
        Dim filtroOrdiniEmesso As String = ""
        If ordiniEmesso.Value = "1" Then
            ordiniBozza.Value = "0"
            'ordiniEmesso.Value = "0"
            filtroStato = " STATO = 1"
        End If

        EsportaQueryODL = "SELECT 'false' as CHECK1, ID, PROGR || '/' || ANNO AS ODL, MANUTENZIONI.ID_SEGNALAZIONI AS SEGNALAZIONE,  " _
                & " (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS NUM_REPERTORIO, " _
                & " manutenzioni.stato as id_stato, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.TAB_STATI_ODL WHERE ID = MANUTENZIONI.STATO) AS STATO, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID =MANUTENZIONI.ID_PF_VOCE_IMPORTO) AS VOCE_DGR, " _
                & " (SELECT DESCRIZIONE FROM SISCOM_MI.PF_VOCI WHERE ID =MANUTENZIONI.ID_PF_VOCE) AS VOCE_BP, " _
                & " (SELECT MAX(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYY/MM/DD')) FROM SISCOM_MI.EVENTI_MANUTENZIONE  " _
                & " WHERE COD_EVENTO = 'F92' " _
                & " AND MOTIVAZIONE = 'Da Bozza ad Emesso Ordine' AND ID_MANUTENZIONE = MANUTENZIONI.ID) AS DATA_EMISSIONE, " _
                & " TO_DATE(DATA_INIZIO_INTERVENTO,'YYYY/MM/DD') AS DATA_INIZIO_INTERVENTO, " _
                & " TO_DATE(DATA_FINE_INTERVENTO,'YYYY/MM/DD') AS DATA_FINE_INTERVENTO, " _
                & " TO_DATE(MANUTENZIONI.DATA_PGI,'YYYY/MM/DD') AS DATA_PGI, " _
                & " TO_DATE(MANUTENZIONI.DATA_TDL,'YYYY/MM/DD') AS DATA_TDL, " _
                & " (CASE WHEN (SELECT SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE = MANUTENZIONI.ID) IS NOT NULL THEN " _
                & " TO_DATE((SELECT SEGNALAZIONI_FORNITORI.DATA_FINE_INTERVENTO FROM SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE ID_MANUTENZIONE = MANUTENZIONI.ID),'YYYY/MM/DD')  " _
                & " ELSE " _
                & " TO_DATE(MANUTENZIONI.DATA_FINE_ORDINE,'YYYY/MM/DD') END) AS DATA_CHIUSURA_LAVORI, " _
                & " (SELECT ID_ESERCIZIO_FINANZIARIO FROM SISCOM_MI.PF_MAIN WHERE ID IN (ID_PIANO_FINANZIARIO)) AS ID_ESERCIZIO_FINANZIARIO, " _
                & " MANUTENZIONI.IMPORTO_PRESUNTO, " _
                & " PROGR, ANNO, (SELECT NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS REP_APPALTO, " _
                & "(SELECT ID FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS ID_APPALTO, " _
                & "(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID = MANUTENZIONI.ID_APPALTO) AS ID_FORNITORE, " _
                & " MANUTENZIONI.ID_SERVIZIO " _
                & " FROM SISCOM_MI.MANUTENZIONI " _
                & " WHERE " & filtroStato _
                & filtroSegnalazione _
                & filtroIdManutenzioni _
               & filtroDL & filtroAppalto

    End Function

    Private Sub btnVisualizzaManu_Click(sender As Object, e As EventArgs) Handles btnVisualizzaManu.Click
        Try
            If Not String.IsNullOrEmpty(idSelectedManu.Value) Then
                Session.Add("ID", idSelectedManu.Value)
                Response.Write("<script>location.replace('CicloPassivo/MANUTENZIONI/Manutenzioni.aspx?REP=" & par.VaroleDaPassare(repAppalto.Value) _
                                                                        & "&ODL=" & progrManutenzione.Value _
                                                                        & "&ANNO=" & annoManutenzione.Value _
                                                                        & "&EF_R=" & idEsercizioFinanziario.Value _
                                                                        & "&PROVENIENZA=RICERCA_DIRETTA&NASCONDIINDIETRO=1');</script>")
            Else
                RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")
            End If

        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnEmettiSal_Click(sender As Object, e As EventArgs) Handles btnEmettiSal.Click
        Try
            Dim continua As Boolean = True


            Dim oDataGridItem As GridDataItem
            'Dim chkExport As System.Web.UI.WebControls.CheckBox
            Dim chkExport As RadButton
            Dim Trovato As Boolean
            Dim i As Integer

            Dim gen As Epifani.ListaGenerale


            For Each oDataGridItem In Me.dgvODL.Items

                chkExport = oDataGridItem.FindControl("CheckBox1")

                If chkExport.Checked Then

                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    If Not IsNothing(lstListaRapporti) Then
                        For Each gen In lstListaRapporti
                            If gen.STR = oDataGridItem.Cells(2).Text Then  ''SISCOM_MI.MANUTENZIONI.ID
                                Trovato = True
                                Exit For
                            End If
                        Next
                    End If


                    If Trovato = False Then
                        gen = New Epifani.ListaGenerale(lstListaRapporti.Count, oDataGridItem.Cells(2).Text)
                        lstListaRapporti.Add(gen)
                        'Me.Label3.Value = Val(Label3.Value) + 1
                        gen = Nothing
                    End If
                Else

                    ' CONTROLLO SE GIA INSERITO nella LISTA
                    Trovato = False
                    For Each gen In lstListaRapporti

                        If gen.STR = oDataGridItem.Cells(2).Text Then
                            Trovato = True
                            Exit For
                        End If
                    Next

                    If Trovato = True Then
                        i = 0
                        For Each gen In lstListaRapporti
                            If gen.STR = oDataGridItem.Cells(2).Text Then
                                lstListaRapporti.RemoveAt(i)
                                'Me.Label3.Value = Val(Label3.Value) - 1
                                Exit For
                            End If
                            i = i + 1
                        Next
                        gen = Nothing
                        Dim indice As Integer = 0
                        For Each gen In lstListaRapporti
                            gen.ID = indice
                            indice += 1
                        Next
                    End If
                End If
            Next

            If lstListaRapporti.Count > 0 Then
                Dim ElencoID_Rapporti As String = ""
                For Each gen In lstListaRapporti
                    If ElencoID_Rapporti <> "" Then
                        ElencoID_Rapporti = ElencoID_Rapporti & "," & gen.STR
                    Else
                        ElencoID_Rapporti = gen.STR
                    End If
                Next

                ImpostaValori()
                Session.Add("ID", 0)



                Session.Remove("NOME_FILE")
                Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQueryODL(ElencoID_Rapporti))
                If dt.Rows.Count > 0 Then
                    For Each riga As Data.DataRow In dt.Rows
                        If riga.Item("id_stato") <> "2" Then
                            continua = False
                        End If
                    Next
                Else
                    continua = False
                End If
                If continua = True Then
                    Response.Write("<script>location.replace('CicloPassivo/MANUTENZIONI/SAL.aspx?FO=" & idFornitore.Value _
                                                        & "&AP=" & idAppalto.Value _
                                                        & "&SV=" & idServizio.Value _
                                                        & "&DAL=" & dataEmissione.Value _
                                                        & "&AL=" & dataEmissione.Value _
                                                        & "&EF_R=" & idEsercizioFinanziario.Value _
                                                        & "&PROVENIENZA=RISULTATI_SAL" _
                                                 & "');</script>")
                Else
                    RadWindowManager1.RadAlert("Verificare che le manutenzioni selezionate siano in stato <strong>consuntivato</strong>!", 300, 150, "Attenzione", "", "null")
                End If


            Else
                RadWindowManager1.RadAlert("Nessuna riga selezionata!", 300, 150, "Attenzione", "", "null")

            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub CheckBox1_CheckedChanged(sender As Object, e As System.EventArgs)
        'Dim numeroCheckati As Integer = 0
        'For Each elemento As GridDataItem In dgvODL.Items
        '    If CType(elemento.FindControl("CheckBox1"), RadButton).Checked = True Then
        '        numeroCheckati += 1
        '    End If
        'Next
        'Select Case numeroCheckati
        '    Case 0
        '        txtmia.Text = "Nessun ODL selezionato"
        '    Case 1
        '        txtmia.Text = "Selezionato 1 ODL"
        '    Case Else
        '        txtmia.Text = "Sono stati selezionati " & numeroCheckati & " ODL"
        'End Select
    End Sub
    Protected Sub ButtonSelAll_Click(sender As Object, e As System.EventArgs)
        Try
            If hiddenSelTutti.Value = "1" Then
                For Each riga As GridItem In dgvODL.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = True
                Next
            Else
                For Each riga As GridItem In dgvODL.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = False
                Next
            End If
        Catch ex As Exception
            'If par.OracleConn.State = Data.ConnectionState.Open Then
            '    connData.chiudi()
            'End If
            'Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ButtonSelAll_Click - " & ex.Message)
            'Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub
    Protected Sub ButtonSelAllSegnalazioni_Click(sender As Object, e As System.EventArgs)
        Try
            If hiddenSelTuttiSegnalazioni.Value = "1" Then
                For Each riga As GridItem In dgvSegnalazioni.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = True
                Next
            Else
                For Each riga As GridItem In dgvSegnalazioni.Items
                    DirectCast(riga.FindControl("CheckBox1"), RadButton).Checked = False
                Next
            End If
        Catch ex As Exception
            'If par.OracleConn.State = Data.ConnectionState.Open Then
            '    connData.chiudi()
            'End If
            'Session.Add("ERRORE", "Provenienza: " & Me.Page.Title & " - ButtonSelAll_Click - " & ex.Message)
            'Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub btnVisualizzaManutenzioni_Click(sender As Object, e As EventArgs) Handles btnVisualizzaManutenzioni.Click
        If Not String.IsNullOrEmpty(idSelected.Value) Then
            Dim filtroStato As String = ""
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                filtroStato = " AND MANUTENZIONI.STATO IN (0,1,2) "
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
                filtroStato = " AND MANUTENZIONI.STATO IN (0) "
            End If
            par.caricaComboTelerik("SELECT ID, NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN " _
                                   & "(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = " & idSelected.Value & filtroStato & ") ORDER BY ID DESC", cmbRepertorio, "ID", "NUM_REPERTORIO", True)
        Else
            RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
        End If

    End Sub

    Private Sub ImpostaValori()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQueryODL())
            CheckBox(dt)
            If dt.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dt.Rows
                    If riga.Item("CHECK1") = "TRUE" Then
                        idFornitore.Value = par.IfNull(riga.Item("ID_FORNITORE"), "")
                        idAppalto.Value = par.IfNull(riga.Item("ID_APPALTO"), "")
                        idServizio.Value = par.IfNull(riga.Item("ID_SERVIZIO"), "")
                        dataEmissione.Value = par.IfNull(riga.Item("DATA_EMISSIONE"), "")
                        idEsercizioFinanziario.Value = par.IfNull(riga.Item("ID_ESERCIZIO_FINANZIARIO"), "")
                    End If
                Next
            End If


            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then connData.chiudi(True)
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub CheckBox(ByRef table As Data.DataTable)
        Try

            Dim row As Data.DataRow
            For i As Integer = 0 To dgvODL.Items.Count - 1
                If DirectCast(dgvODL.Items(i).FindControl("CheckBox1"), RadButton).Checked = False Then
                    row = table.Select("id = " & dgvODL.Items(i).Cells(2).Text)(0)
                    row.Item("CHECK1") = "FALSE"
                Else
                    row = table.Select("id = " & dgvODL.Items(i).Cells(2).Text)(0)
                    row.Item("CHECK1") = "TRUE"
                End If
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - CheckBox - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub CheckBoxSegnalazioni(ByRef table As Data.DataTable)
        Try

            Dim row As Data.DataRow
            For i As Integer = 0 To dgvSegnalazioni.Items.Count - 1
                If DirectCast(dgvSegnalazioni.Items(i).FindControl("CheckBox1"), RadButton).Checked = False Then
                    row = table.Select("id = " & dgvSegnalazioni.Items(i).Cells(2).Text)(0)
                    row.Item("CHECK1") = "FALSE"
                Else
                    row = table.Select("id = " & dgvSegnalazioni.Items(i).Cells(2).Text)(0)
                    row.Item("CHECK1") = "TRUE"
                End If
            Next
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                connData.chiudi(False)
            End If
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - CheckBoxSegnalazioni - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub ImpostaKPI()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim filtroStato As String = ""
            Dim filtroStatoSegnalazioni As String = ""
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                filtroStato = "  MANUTENZIONI.STATO IN (0,1,2) "
                filtroStatoSegnalazioni = "0,6,7"
            ElseIf Session.Item("FL_CP_TECN_AMM") = "1" Then
                filtroStato = " MANUTENZIONI.STATO IN (0) "
                filtroStatoSegnalazioni = "0,6,7"
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" And Session.Item("FL_CP_TECN_AMM") = "0" Then
                filtroStato = "  MANUTENZIONI.STATO IN (0) "
                filtroStatoSegnalazioni = "0"
            End If
            Dim filtroDL As String = ""
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" AndAlso buildingManager.Value = "1" Then
                filtroDL = " AND SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
                filtroDL = " AND SISCOM_MI.GETBUILDINGMANAGER(MANUTENZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                filtroDL = " and manutenzioni.id_segnalazioni in " _
                         & " (select id_segnalazioni from siscom_mi.manutenzioni where id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "'))) "
            End If
            par.cmd.CommandText = " SELECT COUNT(ID) AS NUM_ODL, " _
                                & " SUM(IMPORTO_CONSUNTIVATO) AS IMPORTO_CONSUNTIVATO, " _
                                & " SUM (IMPORTO_PRESUNTO) AS IMPORTO_PRESUNTO,SUM (IMPORTO_CONSUNTIVATO)+SUM (IMPORTO_PRESUNTO) AS IMPORTO_IMPEGNATO, " _
                                & " ROUND(SUM(IMPORTO_CONSUNTIVATO + IMPORTO_PRESUNTO)/COUNT(ID),2) AS VALORE_MEDIO_ODL, " _
                                & " ROUND(AVG(SISCOM_MI.GETTEMPOATTRAVERSAMENTOMANU(MANUTENZIONI.ID)),2) AS TEMPO_ATTRAVERSAMENTO " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  " & filtroStato _
                                & filtroDL _
                                & " AND ID_SEGNALAZIONI IN (SELECT ID FROM siscom_mi.SEGNALAZIONI WHERE ID_STATO IN (" & filtroStatoSegnalazioni & ")) "
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            For Each riga As Data.DataRow In dt.Rows
                'lblNumODL.Text = par.IfNull(riga.Item("NUM_ODL"), 0)
                lblValoreMedio.Text = "€ " & Format(par.IfNull(riga.Item("VALORE_MEDIO_ODL"), 0), "##,##0.00")
                lblImportoImpegnato.Text = "€ " & Format(par.IfNull(riga.Item("IMPORTO_IMPEGNATO"), 0), "##,##0.00")
                lblImportoConsuntivato.Text = "€ " & Format(par.IfNull(riga.Item("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")
                lblImportoPreventivo.Text = "€ " & Format(par.IfNull(riga.Item("IMPORTO_PRESUNTO"), 0), "##,##0.00")
                lblTempoAttraversamento.Text = par.IfNull(riga.Item("TEMPO_ATTRAVERSAMENTO"), 0) & " giorni"
            Next

            par.cmd.CommandText = " SELECT COUNT(ID) AS NUM_ODL " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  MANUTENZIONI.STATO = 1 " _
                                & filtroDL _
                                & " AND ID_SEGNALAZIONI IN (SELECT ID FROM siscom_mi.SEGNALAZIONI WHERE ID_STATO IN (" & filtroStatoSegnalazioni & ")) "
            lblNumODL.Text = par.cmd.ExecuteScalar


            Dim filtroDLNoSegn As String = " and id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "
            par.cmd.CommandText = " SELECT COUNT(ID) AS NUM_ODL " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE   MANUTENZIONI.STATO = 1 " _
                                & filtroDLNoSegn _
                                & " AND ID_SEGNALAZIONI is null "
            lblNumODLNoSegn.Text = par.cmd.ExecuteScalar
            'Dim dtNoSegn As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            'For Each riga As Data.DataRow In dtNoSegn.Rows
            '    lblNumODLNoSegn.Text = par.IfNull(riga.Item("NUM_ODL"), 0)
            'Next

            par.cmd.CommandText = " SELECT COUNT(ID) AS NUM_ODL " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  MANUTENZIONI.STATO IN (0) " _
                                & filtroDLNoSegn
            Dim dtODLBozzaNoEmessi As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            For Each riga As Data.DataRow In dtODLBozzaNoEmessi.Rows
                lblOdlBozzaNoEmessi.Text = par.IfNull(riga.Item("NUM_ODL"), 0)
            Next

            par.cmd.CommandText = " SELECT COUNT(ID) AS NUM_ODL " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  MANUTENZIONI.STATO IN (1) " _
                                & filtroDLNoSegn
            Dim dtODLEmessiNoCons As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            For Each riga As Data.DataRow In dtODLEmessiNoCons.Rows
                lblODLEmessiNoCons.Text = par.IfNull(riga.Item("NUM_ODL"), 0)
            Next
            par.cmd.CommandText = " SELECT COUNT(ID) AS NUM_ODL " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  MANUTENZIONI.STATO IN (2) " _
                                & filtroDLNoSegn

            lblODLConsNoCDP.Text = par.cmd.ExecuteScalar


            Dim dtSegnalazioni As Data.DataTable = par.getDataTableGrid(EsportaQuerySegnalazioni())
            Dim numeroSegnAperte As Integer = 0
            Dim numeroSegnInCorso As Integer = 0
            Dim numeroSegnEvasa As Integer = 0
            Dim numeroSegnChiusa As Integer = 0
            Dim numeroSegnAperte30giorni As Integer = 0
            If dt.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dtSegnalazioni.Rows
                    Select Case riga.Item("id_stato")
                        Case "0"
                            'Aperta

                            Dim data As Integer = DateDiff(DateInterval.Day, CDate(riga.Item("DATA_INSERIMENTO")), Now)
                            If data > 30 Then
                                numeroSegnAperte30giorni += 1
                            End If
                            numeroSegnAperte += 1
                        Case "6"
                            'In corso
                            numeroSegnInCorso += 1
                        Case "7"
                            'Evasa
                            numeroSegnEvasa += 1
                        Case "10"
                            'Chiusa
                            numeroSegnChiusa += 1
                    End Select
                Next

            End If
            lblSegnAperte30gg.Text = numeroSegnAperte30giorni
            lblNumSegnalazioniAperte.Text = numeroSegnAperte
            lblNumSegnalazioniInCorso.Text = numeroSegnInCorso
            lblNumSegnalazioniEvase.Text = numeroSegnEvasa
            lblNumSegnalazioniChiuse.Text = numeroSegnChiusa
            lblAllSegn.Text = numeroSegnAperte + numeroSegnInCorso + numeroSegnEvasa + numeroSegnChiusa
            'ImpostaTempoGestione()




            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub ImpostaTempoGestione()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim filtroDLSegnalazioni As String = ""
            If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" AndAlso buildingManager.Value = "1" Then
                filtroDLSegnalazioni = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
                filtroDLSegnalazioni = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
            ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                filtroDLSegnalazioni = " and segnalazioni.id in " _
                         & " (select id_segnalazioni from siscom_mi.manutenzioni where id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "'))) "
            End If



            par.cmd.CommandText = "SELECT ROUND(NVL(AVG(SISCOM_MI.GETTEMPOAPRESAINCARICO(ID)),0),0) AS TEMPO_PRESA_IN_CARICO, " _
                                & " ROUND(NVL(AVG(SISCOM_MI.GETTEMPORISOLUZIONETECNICA(ID)),0),0) AS TEMPO_RISOLUZIONE_TECNICA " _
                                & " FROM SISCOM_MI.SEGNALAZIONI WHERE ID_PERICOLO_SEGNALAZIONE = " & cmbCriticita.SelectedValue _
                                & " and id_stato in (6,7) and id_tipo_segnalazione = 1 " _
                                & filtroDLSegnalazioni
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                lblTempoPresaInCarico.Text = lettore.Item("TEMPO_PRESA_IN_CARICO")
                lblTempoRisoluzioneTecnica.Text = lettore.Item("TEMPO_RISOLUZIONE_TECNICA")
                'lblTempoContabilizzazione.Text = lettore.Item("TEMPO_CONTABILIZZAZIONE")
            End If
            lettore.Close()

            'Dim dtTempoGestione As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            'If dtTempoGestione.Rows.Count > 0 Then
            '    lblTempoPresaInCarico.Text = dtTempoGestione.Rows(0).Item("TEMPO_PRESA_IN_CARICO")
            '    lblTempoRisoluzioneTecnica.Text = dtTempoGestione.Rows(0).Item("TEMPO_RISOLUZIONE_TECNICA")
            '    lblTempoContabilizzazione.Text = dtTempoGestione.Rows(0).Item("TEMPO_CONTABILIZZAZIONE")
            'End If
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnChiudiSegn_Click(sender As Object, e As EventArgs) Handles btnChiudiSegn.Click
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim dt As Data.DataTable = par.getDataTableGrid(EsportaQuerySegnalazioni())
            CheckBoxSegnalazioni(dt)
            Dim dtFiltrata As New Data.DataTable
            Dim view As New Data.DataView(dt)
            view.RowFilter = "CHECK1 = 'TRUE'"
            dtFiltrata = view.ToTable
            If dtFiltrata.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dt.Rows
                    If riga.Item("ID_STATO") = "6" Then
                        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = " & riga.Item("ID")
                        Dim numero As Integer = CInt(par.cmd.ExecuteScalar)
                        If numero = 0 Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO=10,DATA_CHIUSURA=TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') WHERE ID =" & riga.Item("ID")
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & riga.Item("ID") & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','IN CORSO','CHIUSA')"
                            par.cmd.ExecuteNonQuery()
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
                                                    & " VALUES ( " & riga.Item("ID") & ",'Chiusura segnalazione da Dashboard Ciclo Passivo',TO_CHAR(SYSDATE,'YYYYMMDDHH24MI')," & Session.Item("ID_OPERATORE") & ",2) "
                            par.cmd.ExecuteNonQuery()
                            dgvSegnalazioni.Rebind()
                            RadNotificationNote.Show()
                        End If
                    End If
                Next
            Else
                RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
                'selezionare una segnalazione
            End If


            'If Not String.IsNullOrEmpty(idSelected.Value) Then
            '    If idStatoSegnalazione.Value = "6" Then
            '        par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.MANUTENZIONI WHERE ID_SEGNALAZIONI = " & idSelected.Value
            '        Dim numero As Integer = CInt(par.cmd.ExecuteScalar)
            '        If numero = 0 Then
            '            par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_STATO=10,DATA_CHIUSURA=TO_CHAR(SYSDATE,'YYYYMMDDHH24MI') WHERE ID =" & idSelected.Value
            '            par.cmd.ExecuteNonQuery()
            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_SEGNALAZIONI (ID_SEGNALAZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE,VALORE_OLD,VALORE_NEW) VALUES (" & idSelected.Value & ", " & Session.Item("ID_OPERATORE") & ", " & Format(Now, "yyyyMMddHHmmss") & ", 'F287', 'CAMBIO STATO SEGNALAZIONE','IN CORSO','CHIUSA')"
            '            par.cmd.ExecuteNonQuery()
            '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,id_tipo_segnalazione_note) " _
            '                                    & " VALUES ( " & idSelected.Value & ",'Chiusura segnalazione da Dashboard Ciclo Passivo',TO_CHAR(SYSDATE,'YYYYMMDDHH24MI')," & Session.Item("ID_OPERATORE") & ",2) "
            '            par.cmd.ExecuteNonQuery()
            '            dgvSegnalazioni.Rebind()
            '            RadNotificationNote.Show()
            '        Else
            '            RadWindowManager1.RadAlert("E\' possibile chiudere solo segnalazioni <strong>senza ordini associati</strong>!", 300, 150, "Attenzione", "", "null")
            '        End If
            '    Else
            '        RadWindowManager1.RadAlert("E\' possibile chiudere solo segnalazioni <strong>in corso</strong>!", 300, 150, "Attenzione", "", "null")
            '    End If
            'Else
            '    RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
            '    'selezionare una segnalazione
            'End If

            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub cmbRepertorio_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbRepertorio.SelectedIndexChanged
        dgvODL.Rebind()
    End Sub

    Private Sub btnVisualizzaSegnalazione_Click(sender As Object, e As EventArgs) Handles btnVisualizzaSegnalazione.Click
        Try
            If idSelected.Value = "" Then
                Response.Write("<script>alert('Nessuna riga selezionata!')</script>")
            Else
                Session.Add("ID", idSelected.Value)
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "segnalazione", "location.replace('CicloPassivo/MANUTENZIONI/Segnalazioni.aspx?IDS=" & idSelected.Value & "&NASCONDIINDIETRO=1');", True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Private Sub btnBuildingManager_Click(sender As Object, e As EventArgs) Handles btnBuildingManager.Click
        buildingManager.Value = 1
        dgvSegnalazioni.Rebind()
        ImpostaKPI()
    End Sub

    Private Sub btnDL_Click(sender As Object, e As EventArgs) Handles btnDL.Click
        buildingManager.Value = "0"
        dgvSegnalazioni.Rebind()
        ImpostaKPI()
    End Sub

    Private Sub btnCaricaAppalti_Click(sender As Object, e As EventArgs) Handles btnCaricaAppalti.Click
        CaricaAppalti()
    End Sub

    Private Sub CaricaAppalti()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            par.cmd.CommandText = "SELECT ID, NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI_DL WHERE ID_OPERATORE = '" & Session.Item("ID_OPERATORE") & "') ORDER BY ID desc"

            par.caricaComboTelerik(par.cmd.CommandText, cmbRepertorio, "ID", "NUM_REPERTORIO", True)

            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnOrdiniBozza_Click(sender As Object, e As EventArgs) Handles btnOrdiniBozza.Click
        ordiniBozza.Value = "1"
        idSelected.Value = "-1"
        cmbRepertorio.ClearSelection()
        cmbRepertorio.Items.Clear()

        dgvSegnalazioni.Rebind()
    End Sub

    Private Sub btnOrdiniEmessi_Click(sender As Object, e As EventArgs) Handles btnOrdiniEmessi.Click
        VisualizzaOrdiniEmessi()
    End Sub

    Private Sub btnSegnAperte_Click(sender As Object, e As EventArgs) Handles btnSegnAperte.Click
        segnAperte.Value = "1"
        dgvSegnalazioni.Rebind()
    End Sub
    Private Sub btnSegnInCorso_Click(sender As Object, e As EventArgs) Handles btnSegnInCorso.Click
        segnInCorso.Value = "1"
        dgvSegnalazioni.Rebind()
    End Sub
    Private Sub btnSegnEvase_Click(sender As Object, e As EventArgs) Handles btnSegnEvase.Click
        segnEvase.Value = "1"
        dgvSegnalazioni.Rebind()
    End Sub
    Private Sub btnSegnChiuse_Click(sender As Object, e As EventArgs) Handles btnSegnChiuse.Click
        segnChiuse.Value = "1"
        dgvSegnalazioni.Rebind()
    End Sub
    Private Sub btnAllSegn_Click(sender As Object, e As EventArgs) Handles btnAllSegn.Click
        allSegn.Value = "1"
        dgvSegnalazioni.Rebind()
    End Sub
    Private Sub btnSegnAperte30gg_Click(sender As Object, e As EventArgs) Handles btnSegnAperte30gg.Click
        segnAperte.Value = "1"
        segn30gg.Value = "1"
        dgvSegnalazioni.Rebind()
    End Sub

    Private Sub btnODLEmessi_Click(sender As Object, e As EventArgs) Handles btnODLEmessi.Click
        ResetKPI6()
        VisualizzaOrdiniEmessi()
    End Sub

    Private Sub ResetKPI6()
        ODLEmessiNoCons.Value = "0"
        ODLBozzaNonEmessi.Value = "0"
        ODLConsNoCDP.Value = "0"
    End Sub

    Private Sub VisualizzaOrdiniEmessi()
        ordiniEmesso.Value = "1"
        idSelected.Value = "-1"
        cmbRepertorio.ClearSelection()
        cmbRepertorio.Items.Clear()
        dgvSegnalazioni.Rebind()
    End Sub

    Private Sub btnODLEmessiNoSegn_Click(sender As Object, e As EventArgs) Handles btnODLEmessiNoSegn.Click
        idSelected.Value = "-1"
        ResetKPI6()
        CaricaAppaltiNoSegn(1, True)
        dgvODL.Rebind()
    End Sub


    Private Sub CaricaAppaltiNoSegn(ByVal statoManutenzione As Integer, Optional ByVal NoSegnalazione As Boolean = True)
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim noSegn As String = ""
            If NoSegnalazione = 1 Then
                noSegn = " AND ID_SEGNALAZIONI is null"
            End If
            Dim filtroStato As String = ""
            filtroStato = "  MANUTENZIONI.STATO  = " & statoManutenzione

            Dim filtroDLNoSegn As String = " and id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "

            par.cmd.CommandText = "SELECT distinct ID_GRUPPO, NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN " _
                & " (SELECT id_appalto " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  " & filtroStato _
                                & filtroDLNoSegn _
                                & noSegn & ") " _
                & " ORDER BY ID_GRUPPO desc"


            par.caricaComboTelerik(par.cmd.CommandText, cmbRepertorio, "ID_GRUPPO", "NUM_REPERTORIO", True)

            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub btnODLBozzaNonEmessi_Click(sender As Object, e As EventArgs) Handles btnODLBozzaNonEmessi.Click
        'ODLBozzaNonEmessi.Value = "1"
        'dgvODL.Rebind()
        idSelected.Value = "-1"
        ODLBozzaNonEmessi.Value = "1"
        ODLEmessiNoCons.Value = "0"
        ODLConsNoCDP.Value = "0"
        CaricaAppaltiNoSegn(0, False)
        dgvODL.Rebind()
    End Sub

    Private Sub btnODLEmessiNoCons_Click(sender As Object, e As EventArgs) Handles btnODLEmessiNoCons.Click
        ODLEmessiNoCons.Value = "1"
        ODLBozzaNonEmessi.Value = "0"
        ODLConsNoCDP.Value = "0"
        idSelected.Value = "-1"
        CaricaAppaltiNoSegn(1, False)
        dgvODL.Rebind()
    End Sub

    Private Sub CaricaAppaltiODLBozza()
        Try
            Dim ApertaNow As Boolean = False
            If Not IsNothing(Me.connData) Then
                Me.connData.RiempiPar(par)
            Else
                connData.apri(False)
                ApertaNow = True
            End If
            Dim filtroStato As String = ""
            filtroStato = "  MANUTENZIONI.STATO IN (0) "
            Dim filtroDLNoSegn As String = " and id_appalto in  " _
                         & " (select id from siscom_mi.appalti where id_gruppo in  " _
                         & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "')) "

            par.cmd.CommandText = "SELECT ID, NUM_REPERTORIO FROM SISCOM_MI.APPALTI WHERE ID IN " _
                & " (SELECT id_appalto " _
                                & " FROM SISCOM_MI.MANUTENZIONI " _
                                & " WHERE  " & filtroStato _
                                & filtroDLNoSegn _
                                & ") " _
                & " ORDER BY ID desc"


            par.caricaComboTelerik(par.cmd.CommandText, cmbRepertorio, "ID", "NUM_REPERTORIO", True)

            '************CHIUSURA CONNESSIONE**********
            If ApertaNow Then
                connData.chiudi(True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub btnODLConsNoCdP_Click(sender As Object, e As EventArgs) Handles btnODLConsNoCdP.Click
        ODLEmessiNoCons.Value = "0"
        ODLBozzaNonEmessi.Value = "0"
        ODLConsNoCDP.Value = "1"
        idSelected.Value = "-1"
        CaricaAppaltiNoSegn(2, False)
        dgvODL.Rebind()
    End Sub

    Private Sub cmbCriticita_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCriticita.SelectedIndexChanged
        ImpostaTempoGestione()
    End Sub

    Private Sub btnTempoGestione_Click(sender As Object, e As EventArgs) Handles btnTempoGestione.Click
        Try
            If Not String.IsNullOrEmpty(idSelected.Value) Then
                Dim ApertaNow As Boolean = False
                If Not IsNothing(Me.connData) Then
                    Me.connData.RiempiPar(par)
                Else
                    connData.apri(False)
                    ApertaNow = True
                End If
                Dim filtroDLSegnalazioni As String = ""
                If Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" AndAlso buildingManager.Value = "1" Then
                    filtroDLSegnalazioni = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
                ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "0" Or Session.Item("FL_CP_TECN_AMM") = "1" Then
                    filtroDLSegnalazioni = " AND SISCOM_MI.GETBUILDINGMANAGERSEGNALAZIONI(SEGNALAZIONI.ID,1) = '" & Session.Item("ID_OPERATORE") & "'"
                ElseIf Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                    filtroDLSegnalazioni = " and segnalazioni.id in " _
                             & " (select id_segnalazioni from siscom_mi.manutenzioni where id_appalto in  " _
                             & " (select id from siscom_mi.appalti where id_gruppo in  " _
                             & " (select id_gruppo from siscom_mi.appalti_dl where id_operatore = '" & Session.Item("ID_OPERATORE") & "'))) "
                End If

                par.cmd.CommandText = "SELECT ROUND(NVL(AVG(SISCOM_MI.GETTEMPOAPRESAINCARICO(ID)),0),0) AS TEMPO_PRESA_IN_CARICO, " _
                                    & " ROUND(NVL(AVG(SISCOM_MI.GETTEMPORISOLUZIONETECNICA(ID)),0),0) AS TEMPO_RISOLUZIONE_TECNICA " _
                                    & " FROM SISCOM_MI.SEGNALAZIONI WHERE id = " & idSelected.Value

                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    lblTempoPresaInCarico.Text = lettore.Item("TEMPO_PRESA_IN_CARICO")
                    lblTempoRisoluzioneTecnica.Text = lettore.Item("TEMPO_RISOLUZIONE_TECNICA")
                    'lblTempoContabilizzazione.Text = lettore.Item("TEMPO_CONTABILIZZAZIONE")
                End If
                lettore.Close()
                '************CHIUSURA CONNESSIONE**********
                If ApertaNow Then
                    connData.chiudi(True)
                End If
            Else
                RadWindowManager1.RadAlert("Selezionare una segnalazione!", 300, 150, "Attenzione", "", "null")
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
