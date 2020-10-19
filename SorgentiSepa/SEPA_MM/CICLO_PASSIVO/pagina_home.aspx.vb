
Partial Class ANAUT_pagina_home
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim scriptblock As String
    Dim nGiorno As String
    Dim nGiornoRif As String
    Dim GiorniAp As String

    'MARIO
    Public lstAppalti As New System.Collections.Generic.List(Of Mario.Appalti)
    Public lstservizi As New System.Collections.Generic.List(Of Mario.VociServizi)
    Public lstprezzi As New System.Collections.Generic.List(Of Mario.ElencoPrezzi)
    Public lstscadenze As New System.Collections.Generic.List(Of Mario.ScadenzeManuali)

    '*** EPIFANI
    Public lstInterventi As New System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi)
    Public lstListaGenerale1 As New System.Collections.Generic.List(Of Epifani.ListaGenerale)
    Public lstListaGenerale2 As New System.Collections.Generic.List(Of Epifani.ListaGenerale)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            'Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

        Label1.Text = System.Configuration.ConfigurationManager.AppSettings("Versione")
        If Not IsPostBack Then
            Session.LCID = 1040

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


                par.OracleConn.Open()
                par.SettaCommand(par)

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




                par.cmd.CommandText = "select PF_VOCI.ID_PIANO_FINANZIARIO from siscom_mi.pf_voci_STRUTTURA,SISCOM_MI.PF_VOCI where pf_voci_struttura.id_struttura=" & Session.Item("ID_STRUTTURA") & " and PF_VOCI.ID=PF_VOCI_STRUTTURA.ID_VOCE AND PF_VOCI_STRUTTURA.completo='0' and PF_VOCI_STRUTTURA.completo_aler='1' and PF_VOCI_STRUTTURA.valore_lordo_aler<>PF_VOCI_STRUTTURA.valore_lordo AND (PF_VOCI.ID IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ") OR PF_VOCI.ID_VOCE_MADRE IN (SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_OPERATORI WHERE ID_OPERATORE=" & Session.Item("ID_OPERATORE") & ")) order by PF_VOCI.codice asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.HasRows = True Then
                    If myReader1.Read Then
                        imgEventi.Visible = True
                        imgEventi.Attributes.Add("OnClick", "javascript:alert('ATTENZIONE...Delle voci del Piano finanziario non sono state approvate dal Gestore.\nVerrà ora mostrato il riepilogo.');window.location.href='CicloPassivo/Plan/ElencoNonApprovate.aspx?IDP=" & myReader1("id_piano_finanziario") & "';")
                    End If
                End If
                myReader1.Close()



                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                ' PagamentiInScadenza(Session.Item("ID_STRUTTURA"))

                txtmessaggio.Text = Session.Item("ORARIO")
                Label3.Text = Session.Item("ORARIO")


                'TrovaSegnalazioni()

            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End Try


        End If

        'par.OracleConn.Dispose()

    End Sub

    'Private Sub PagamentiInScadenza(ByVal sStruttura As String)
    '    Try

    '        '*******************APERURA CONNESSIONE*********************
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
    '        Dim giorni As Integer = 30


    '        'PAGAMENTI A CANONE CON ATTESTATO non STAMPATO
    '        If sStruttura <> "-1" Then
    '            par.cmd.CommandText = "select ID from SISCOM_MI.PAGAMENTI " _
    '                               & " where  DATA_STAMPA is null " _
    '                               & "   and TIPO_PAGAMENTO=6 " _
    '                               & "   and ID_STATO=1 " _
    '                               & "   and ID in (select ID_PAGAMENTO " _
    '                                             & " from SISCOM_MI.PRENOTAZIONI " _
    '                                             & " where ID_STATO=2 " _
    '                                             & "   and TIPO_PAGAMENTO=6 " _
    '                                             & "   and ID_STRUTTURA= " & sStruttura & ")"


    '            myReader1 = par.cmd.ExecuteReader()
    '            If myReader1.Read Then
    '                Me.imgPagScad.Visible = True
    '            Else
    '                Me.imgPagScad.Visible = False
    '            End If
    '            myReader1.Close()
    '            '*************************************************

    '            'PAGAMENTI A CANONE DA APPROVARE in SCADENZA
    '            par.cmd.CommandText = "select ID from SISCOM_MI.PRENOTAZIONI " _
    '                                          & " where  (NVL(TO_DATE(DATA_SCADENZA,'yyyymmdd')- TO_DATE(SYSDATE),21) <= 30) " _
    '                                          & "   and TIPO_PAGAMENTO=6 " _
    '                                          & "   and ID_STATO=0 " _
    '                                          & "   and  IMPORTO_PRENOTATO>0 " _
    '                                          & "   and ID_STRUTTURA=" & sStruttura

    '            myReader1 = par.cmd.ExecuteReader()
    '            If myReader1.Read Then
    '                Me.imgPagApprovare.Visible = True
    '            Else
    '                Me.imgPagApprovare.Visible = False
    '            End If
    '            myReader1.Close()
    '        End If

    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


    '    Catch ex As Exception
    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    End Try
    'End Sub


    'Private Sub TrovaSegnalazioni()
    '    If Not IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") <> "-1" Then

    '        Dim FlagConnessione As Boolean = False

    '        Try
    '            Dim sStr1 As String = ""
    '            Dim sOrder As String = ""


    '            Dim sStrID_TAB_FILIALI As String = ""
    '            Dim sStrID_ID_TIPOLOGIE As String = "-1"
    '            Dim sID_TIPO_ST As String = ""

    '            Dim sTipoRichiesta As String = "1" 'TIPO_RICHIESTA = 1  SEGNALAZIONI GUASTI prima era '0=GUASTI',1,'RECLAMI',2,'PROPOSTE',3,'VARIE'



    '            Dim sFiliale As String = "-1"
    '            If Session.Item("LIVELLO") <> "1" Then
    '                sFiliale = Session.Item("ID_STRUTTURA")
    '            End If


    '            ' APRO CONNESSIONE
    '            If par.OracleConn.State = Data.ConnectionState.Closed Then
    '                par.OracleConn.Open()
    '                par.SettaCommand(par)
    '                FlagConnessione = True
    '            End If


    '            'sFiliale = 26 X LE PROVE

    '            If sFiliale <> "" Then

    '                'par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
    '                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '                'If myReader1.Read Then
    '                '    sID_TIPO_ST = par.IfNull(myReader1("ID_TIPO_ST"), 0)
    '                'End If
    '                'myReader1.Close()


    '                ''0=FILIALE AMMINISTRATIVA
    '                ''1=FILIALE TECNICA
    '                ''2=UFFICIO CENTRALE

    '                'par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
    '                '                  & " where ID_TIPO_ST=" & sID_TIPO_ST

    '                'If sID_TIPO_ST = 2 And sFiliale <> 64 And sFiliale <> 65 Then
    '                '    '2=UFFICIO CENTRALE vede le segnalazioni di tutti i complessi però con ID_TIPOLOGIE fltrata anche per ID_STRUTTURA
    '                '    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
    '                'End If
    '                'myReader1 = par.cmd.ExecuteReader
    '                par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
    '                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '                If myReader1.Read Then
    '                    sID_TIPO_ST = par.IfNull(myReader1("ID_TIPO_ST"), 0)
    '                Else
    '                    sID_TIPO_ST = "-1"
    '                End If
    '                myReader1.Close()


    '                '0=FILIALE AMMINISTRATIVA
    '                '1=FILIALE TECNICA
    '                '2=UFFICIO CENTRALE

    '                par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
    '                                  & " where ID_TIPO_ST=" & sID_TIPO_ST

    '                If sID_TIPO_ST = "2" And sFiliale <> 64 And sFiliale <> 65 Then
    '                    '2=UFFICIO CENTRALE vede le segnalazioni di tutti i complessi però con ID_TIPOLOGIE fltrata anche per ID_STRUTTURA
    '                    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale
    '                End If
    '                myReader1 = par.cmd.ExecuteReader()


    '                While myReader1.Read
    '                    If sStrID_ID_TIPOLOGIE = "" Then
    '                        sStrID_ID_TIPOLOGIE = myReader1(0)
    '                    Else
    '                        sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
    '                    End If
    '                End While
    '                myReader1.Close()

    '                If sID_TIPO_ST = 1 Then
    '                    par.cmd.CommandText = "select ID from SISCOM_MI.TAB_FILIALI where ID_TECNICA=" & sFiliale
    '                    myReader1 = par.cmd.ExecuteReader()

    '                    While myReader1.Read
    '                        If sStrID_TAB_FILIALI = "" Then
    '                            sStrID_TAB_FILIALI = myReader1(0)
    '                        Else
    '                            sStrID_TAB_FILIALI = sStrID_TAB_FILIALI & "," & myReader1(0)
    '                        End If
    '                    End While
    '                    myReader1.Close()
    '                    If sStrID_TAB_FILIALI = "" Then sStrID_TAB_FILIALI = "-1"
    '                End If

    '                If sID_TIPO_ST = 2 And sFiliale = 64 Then
    '                    sStrID_ID_TIPOLOGIE = ""
    '                    par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
    '                                  & " where ID_TIPO_ST=0"

    '                    myReader1 = par.cmd.ExecuteReader()

    '                    While myReader1.Read
    '                        If sStrID_ID_TIPOLOGIE = "" Then
    '                            sStrID_ID_TIPOLOGIE = myReader1(0)
    '                        Else
    '                            sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
    '                        End If
    '                    End While
    '                    myReader1.Close()



    '                    sStrID_TAB_FILIALI = "1,6,9"

    '                End If

    '                If sID_TIPO_ST = 2 And sFiliale = 65 Then
    '                    sStrID_ID_TIPOLOGIE = ""
    '                    par.cmd.CommandText = "select ID from SISCOM_MI.TIPOLOGIE_GUASTI " _
    '                                         & " where ID_TIPO_ST=0"
    '                    myReader1 = par.cmd.ExecuteReader()

    '                    While myReader1.Read
    '                        If sStrID_ID_TIPOLOGIE = "" Then
    '                            sStrID_ID_TIPOLOGIE = myReader1(0)
    '                        Else
    '                            sStrID_ID_TIPOLOGIE = sStrID_ID_TIPOLOGIE & "," & myReader1(0)
    '                        End If
    '                    End While
    '                    myReader1.Close()

    '                    sStrID_TAB_FILIALI = "2,8,22"

    '                End If
    '                sStr1 = sStr1 & " select SEGNALAZIONI.ID, " _
    '                            & " ID_COMPLESSO as IDENTIFICATIVO," _
    '                            & " 'C' as TIPO_S," _
    '                            & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
    '                            & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
    '                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
    '                     & " from SISCOM_MI.SEGNALAZIONI " _
    '                     & " where SEGNALAZIONI.ID_STATO=0" _
    '                     & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
    '                     & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
    '                     & "   and SEGNALAZIONI.ID_COMPLESSO is NOT NULL " _
    '                     & "   and SEGNALAZIONI.ID_EDIFICIO is NULL " _
    '                     & "   and SEGNALAZIONI.ID_UNITA is NULL "

    '                If sID_TIPO_ST = 0 Then
    '                    '0=FILIALE AMMINISTRATIVA vede le segnalazioni con i complessi della propria filiale (come era la logica di prima)
    '                    sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                   & " where ID_FILIALE=" & sFiliale & ") "
    '                ElseIf sID_TIPO_ST = 1 Then
    '                    '1=FILIALE TECNICA   vede tutti  complessi della propria filiale più quelle dope TAB_FILALE.ID_TECNICA è uguale alla propria FILIALE
    '                    sStr1 = sStr1 & "   and SEGNALAZIONI.ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                                          & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ") ) "
    '                End If


    '                '& "   and SEGNALAZIONI.ID_COMPLESSO is NULL " 
    '                'EDIFICIO
    '                sStr1 = sStr1 & " union select SEGNALAZIONI.ID, " _
    '                            & " ID_EDIFICIO as IDENTIFICATIVO," _
    '                            & " 'E' as TIPO_S," _
    '                            & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
    '                            & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
    '                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
    '                     & " from SISCOM_MI.SEGNALAZIONI " _
    '                     & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=0" _
    '                     & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
    '                     & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
    '                     & "   and SEGNALAZIONI.ID_EDIFICIO is NOT NULL " _
    '                     & "   and SEGNALAZIONI.ID_UNITA is NULL "

    '                If sID_TIPO_ST = 0 Then
    '                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
    '                                                                  & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                                         & " where ID_FILIALE=" & sFiliale & ") ) "

    '                ElseIf sID_TIPO_ST = 1 Then
    '                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
    '                                                                  & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                                         & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "

    '                ElseIf sID_TIPO_ST = 2 And (sFiliale = 64 Or sFiliale = 65) Then
    '                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
    '                                          & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ) "

    '                End If

    '                'UNITA
    '                '& "   and SEGNALAZIONI.ID_COMPLESSO Is not NULL " _
    '                '& "   and SEGNALAZIONI.ID_EDIFICIO Is not NULL " 
    '                sStr1 = sStr1 & " union select SEGNALAZIONI.ID, " _
    '                            & " ID_UNITA as IDENTIFICATIVO," _
    '                            & " 'U' as TIPO_S," _
    '                            & " COGNOME_RS||' '||NOME as RICHIEDENTE," _
    '                            & " to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO, " _
    '                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC  " _
    '                     & " from SISCOM_MI.SEGNALAZIONI " _
    '                     & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=0" _
    '                     & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta _
    '                     & "   and SEGNALAZIONI.ID_TIPOLOGIE  in (" & sStrID_ID_TIPOLOGIE & ")" _
    '                     & "   and SEGNALAZIONI.ID_UNITA is NOT NULL "


    '                If sID_TIPO_ST = 0 Then
    '                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
    '                                                        & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
    '                                                               & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                     & " where ID_FILIALE=" & sFiliale & ") ))"

    '                ElseIf sID_TIPO_ST = 1 Then
    '                    sStr1 = sStr1 & "   and SISCOM_MI.SEGNALAZIONI.ID_UNITA in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
    '                                                        & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
    '                                                               & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
    '                                                                     & " where ID_FILIALE in (" & sStrID_TAB_FILIALI & ")) ))"
    '                End If



    '            Else

    '                sStr1 = "select SEGNALAZIONI.ID," _
    '                            & " case when ID_UNITA      is not null then ID_UNITA " _
    '                                  & "when ID_EDIFICIO   is not null then ID_EDIFICIO " _
    '                                  & "when ID_COMPLESSO  is not null then ID_COMPLESSO " _
    '                            & " end as IDENTIFICATIVO," _
    '                            & " case when ID_UNITA      is not null then 'U' " _
    '                                 & " when ID_EDIFICIO   is not null then 'E' " _
    '                                 & " when ID_COMPLESSO  is not null then 'C' " _
    '                            & " end as TIPO_S,COGNOME_RS||' '||NOME as RICHIEDENTE," _
    '                            & "to_char(to_date(substr(DATA_ORA_RICHIESTA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as DATA_INSERIMENTO," _
    '                            & " substr(SEGNALAZIONI.DESCRIZIONE_RIC,1,100)||'...' as DESCRIZIONE_RIC " _
    '                        & " from SISCOM_MI.SEGNALAZIONI " _
    '                        & " where SISCOM_MI.SEGNALAZIONI.ID_STATO=0" _
    '                        & "   and SEGNALAZIONI.TIPO_RICHIESTA=" & sTipoRichiesta


    '            End If

    '            sStr1 = sStr1 & sOrder

    '            '*** CARICO LA GRIGLIA
    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStr1, par.OracleConn)
    '            Dim dt As New Data.DataTable()

    '            da.Fill(dt)

    '            If dt.Rows.Count >= 1 Then
    '                Me.segnalazioni.Visible = True
    '            Else
    '                segnalazioni.Visible = False
    '            End If


    '            '************CHIUSURA CONNESSIONE**********
    '            If FlagConnessione = True Then
    '                par.cmd.Dispose()
    '                par.OracleConn.Close()
    '                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '                FlagConnessione = False
    '            End If


    '        Catch ex As Exception

    '            '************CHIUSURA CONNESSIONE**********
    '            If FlagConnessione = True Then
    '                par.cmd.Dispose()
    '                par.OracleConn.Close()
    '                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            End If

    '            Session.Item("LAVORAZIONE") = "0"
    '            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

    '        End Try
    '    Else
    '        segnalazioni.Visible = False

    '    End If

    'End Sub


End Class
