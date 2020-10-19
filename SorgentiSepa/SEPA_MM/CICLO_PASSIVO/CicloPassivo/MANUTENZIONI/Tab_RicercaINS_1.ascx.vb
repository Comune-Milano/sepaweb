' TAB GENERALE DEL DETTAGLIO DELLA MOROSITA' DELL'INQUILINO

Partial Class Tab_RicercaINS_1
    Inherits UserControlSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            'If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
            ' FrmSolaLettura()
            ' If
        End If
    End Sub

    Private Sub FrmSolaLettura()
        Try

            Dim CTRL As Control = Nothing
            For Each CTRL In Me.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).Enabled = False
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            'Me.LblErrore.Visible = True
            'LblErrore.Text = ex.Message
        End Try
    End Sub


    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaServizi()
        Dim FlagConnessione As Boolean

        Try

            Me.cmbServizio.Items.Clear()
            '     Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            '   Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))


            If Me.cmbEsercizio.SelectedValue <> "-1" Then

                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    FlagConnessione = True
                End If


                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                End If

                Me.cmbServizio.Items.Clear()
                ' Me.cmbServizio.Items.Add(New ListItem(" ", -1))

                par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                                   & " from SISCOM_MI.TAB_SERVIZI " _
                                   & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                                & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                                   & " from SISCOM_MI.LOTTI " _
                                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " ) " _
                                                & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

                Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                & " order by DESCRIZIONE asc"

                par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                'End While
                'myReader1.Close()

                Me.cmbServizio.SelectedValue = "-1"
                '**************************

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub cmbServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged

        FiltraDettaglio()

    End Sub



    Private Sub FiltraDettaglio()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean


        Try


            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If

            Me.cmbServizioVoce.Items.Clear()
            'Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizio.SelectedValue <> "-1" Then


                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If


                'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.PF_VOCI_IMPORTO " _
                '                    & "where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                & " where  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                & " and    ID_LOTTO in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO )" _
                '                                & " and    ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)) " _
                '                    & "  and ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " _
                '                   & " order by SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE asc"


                'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.PF_VOCI_IMPORTO " _
                '                    & "where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                & " where  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                & " and    ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )" _
                '                & " and    ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)) " _
                '    & "  and ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " _
                '   & " order by SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE asc"

                'par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.PF_VOCI_IMPORTO " _
                '                     & "where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                  & " where  ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )" _
                '                                  & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)) " _
                '                     & "  and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                     & "  and ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " _
                '                     & " order by DESCRIZIONE asc"


                par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                    & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                     & "where  PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                  & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                  & " where  ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )" _
                                                                  & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                     & "  and PF_VOCI_IMPORTO.ID_SERVIZIO<>15" _
                                     & "  and  exists (select ID  from SISCOM_MI.LOTTI  " _
                                                                       & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & " and PF_VOCI_IMPORTO.ID_LOTTO=lotti.id " _
                                                                       & "  and PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                                                        & "  and PF_VOCI_IMPORTO.ID_SERVIZIO<>15" _
                                                                       & " )"


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
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizioVoce, "ID", "DESCRIZIONE", True)

                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

                '    i = i + 1
                'End While
                'myReader1.Close()
                '**************************

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                If i = 1 Then
                    cmbServizioVoce.Items(1).Selected = True
                    FiltraAppalti()
                End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraAppalti()
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.cmbAppalto.Items.Clear()
            ' Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))


            'Dim sFiliale As String = ""
            'If Session.Item("LIVELLO") <> "1" Then
            '    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            'End If

            ' If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 1 Then

            If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                'par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                '                    & " from  SISCOM_MI.APPALTI " _
                '                    & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                 & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                 & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                '                                 & "   and ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
                '                    & "  and ID_STATO=1" _
                '                    & " order by NUM_REPERTORIO "

                par.cmd.CommandText = " select appalti.ID, appalti.id_fornitore, TRIM(NUM_REPERTORIO) ||' - ' ||  fornitori.ragione_sociale as descrizione" _
                                                    & " from  SISCOM_MI.APPALTI, siscom_mi.fornitori " _
                                                    & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                 & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                                 & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
                                                    & "  and ID_STATO=1" _
                                                    & "  and fornitori.id = appalti.id_fornitore " _
                                                    & " order by NUM_REPERTORIO "

            Else

                'par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                '                    & " from  SISCOM_MI.APPALTI " _
                '                    & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                 & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                 & "   and ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
                '                    & "   and ID_STATO=1" _
                '                    & " order by NUM_REPERTORIO "

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
                Exit Sub
            End If
            ' Else
            'If Me.cmbServizioVoce.SelectedValue <> "-1" Then

            '    'par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
            '    '                    & " from  SISCOM_MI.APPALTI " _
            '    '                    & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '    '                                 & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
            '    '                                 & "   and ID_PF_VOCE_IMPORTO<>" & Me.cmbServizioVoce.SelectedValue _
            '    '                                 & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
            '    '                                                              & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ")) " _
            '    '                                 & "   and ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
            '    '                                 & "   and ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
            '    '                    & "   and ID_STATO=1" _
            '    '                    & " order by NUM_REPERTORIO "

            '    par.cmd.CommandText = " select ID,TRIM(NUM_REPERTORIO) AS NUM_REPERTORIO " _
            '                        & " from  SISCOM_MI.APPALTI " _
            '                        & " where ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                     & " where " _
            '                                     & "       ID_PF_VOCE_IMPORTO<>" & Me.cmbServizioVoce.SelectedValue _
            '                                     & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
            '                                                                  & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ") " _
            '                                                                  & "   and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
            '                                                                  & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")) " _
            '                                     & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO )) " _
            '                        & "   and ID_STATO=1" _
            '                        & " order by NUM_REPERTORIO "

            'Else
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If
            ' End If



            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "DESCRIZIONE", True)

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True

                If RBL1.SelectedIndex = 0 Then
                    FiltraComplessi()
                Else
                    FiltraEdifici()
                End If
            End If



        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub cmbServizioVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizioVoce.SelectedIndexChanged
        FiltraAppalti()
    End Sub

    Protected Sub cmbAppalto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAppalto.SelectedIndexChanged

        If RBL1.SelectedIndex = 0 Then
            FiltraComplessi()
        Else
            FiltraEdifici()
        End If

    End Sub


    Private Sub FiltraComplessi()
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try


            Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            If par.IfEmpty(Me.cmbAppalto.SelectedValue, -1) <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                'Dim sFiliale As String = ""
                'If Session.Item("LIVELLO") <> "1" Then
                '    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                'End If

                Select Case CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value
                    Case 0 ' NORMALE (EDIFICI e/o IMPIANTI)
                        par.cmd.CommandText = "select distinct ID,TRIM(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                            & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                          & "where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & "))" _
                                           & " order by DENOMINAZIONE asc"

                    Case 1 ' NORMALE (solo IMPIANTI)
                        par.cmd.CommandText = "select distinct ID,TRIM(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                            & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                          & "where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                                      & " where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ")))" _
                                            & "    or ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.IMPIANTI " _
                                                         & " where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & "))" _
                                           & " order by DENOMINAZIONE asc"

                    Case 2 ' FUORI LOTTO (EDIFICI e/o IMPIANTI)


                        par.cmd.CommandText = "select distinct ID,TRIM(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                           & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                        & " where ID NOT in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & "))" _
                                           & " order by DENOMINAZIONE asc"

                        '                     & " where ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                         & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                        '                                         & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                        '                                                                    & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                                                    & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))))" _
                        '& " order by DENOMINAZIONE asc"


                    Case 3 ' FUORI LOTTO (solo IMPIANTI)

                        par.cmd.CommandText = "select distinct ID,TRIM(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                          & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                        & "where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                                    & " where ID NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ")))" _
                                          & "    or ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.IMPIANTI " _
                                                       & " where ID NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & "))" _
                                         & " order by DENOMINAZIONE asc"

                        'If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                        '    par.cmd.CommandText = "select distinct ID,TRIM(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                        '                       & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                        '                                    & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                        '                                                 & " where ID NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                               & " where ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                                                                     & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                        '                                                                                     & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                        '                                                                                                                 & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                                                                                                 & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")))))" _
                        '                        & "   or ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.IMPIANTI " _
                        '                                     & " where ID NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                   & " where ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                                                          & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                        '                                                                          & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                        '                                                                                                      & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                                                                                      & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))))" _
                        '                            & " order by DENOMINAZIONE asc"

                        'Else
                        '    If FlagConnessione = True Then
                        '        par.cmd.Dispose()
                        '        par.OracleConn.Close()
                        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        '    End If
                        '    Exit Sub
                        'End If
                End Select

                par.caricaComboTelerik(par.cmd.CommandText, cmbComplesso, "ID", "DENOMINAZIONE", True)
               
                '**************************

                If i = 1 Then
                    Me.cmbComplesso.Items(1).Selected = True
                End If

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If



                'If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Then
                '    par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '                        & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '                                & "where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & "))" _
                '                        & "    or ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '                                & "where ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI where ID in (select ID_IMPIANTO " _
                '                                                                    & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ")))" _
                '                        & "    or ID in (select ID_COMPLESSO from SISCOM_MI.IMPIANTI where ID in (select ID_IMPIANTO " _
                '                                                                    & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO  where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & "))" _
                '                       & " order by DENOMINAZIONE asc"



                '    '    If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                '    '        par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '    '                           & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '    '                                        & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                '    '                                                     & " where ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '    '                                                                        & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue _
                '    '                                                                        & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '    '                                                                        & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))) " _
                '    '                           & " order by DENOMINAZIONE asc"


                '    '    Else
                '    '        par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '    '                           & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '    '                                        & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                '    '                                                     & " where ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '    '                                                                         & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue _
                '    '                                                                         & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue & "))) " _
                '    '                           & " order by DENOMINAZIONE asc"




                '    '    End If
                'Else
                '    If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                '        'par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '        '                   & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '        '                                & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '        '                                             & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '        '                                                                 & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '        '                                                                 & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '        '                                                                 & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")))" _
                '        '                        & " order by DENOMINAZIONE asc"

                '        par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '                           & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '                                        & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                                     & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                                         & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '                                                                         & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '                                                                                                    & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                                                                                    & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))))" _
                '                            & "   or ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '                                        & " where ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI " _
                '                                                                            & " where ID in (select ID_IMPIANTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                                                                                            & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                                                                                 & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '                                                                                                                 & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '                                                                                                                                            & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                                                                                                                            & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")))))" _
                '                            & "   or ID in (select ID_COMPLESSO from SISCOM_MI.IMPIANTI " _
                '                                                                            & " where ID in (select ID_IMPIANTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                                                                                            & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                                                                                 & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '                                                                                                                 & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '                                                                                                                                            & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                                                                                                                            & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))))" _
                '                                & " order by DENOMINAZIONE asc"

                '        '& "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '        '                             & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ")" _
                '        '                             & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & "))))) " 


                '    Else
                '        par.cmd.Dispose()
                '        par.OracleConn.Close()
                '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '        Exit Sub
                '    End If
                'End If

            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraEdifici()
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try


            Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            If par.IfEmpty(Me.cmbAppalto.SelectedValue, -1) <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                Dim sFiliale As String = ""
                If Session.Item("LIVELLO") <> "1" Then
                    sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
                End If


                Select Case CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value
                    Case 0 ' NORMALE (EDIFICI e/o IMPIANTI)
                        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                           & " from SISCOM_MI.EDIFICI " _
                                           & " where ID<>1 and ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                        & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ") " _
                                           & " order by DENOMINAZIONE asc"

                    Case 1 ' NORMALE (solo IMPIANTI)
                        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                           & " from SISCOM_MI.EDIFICI " _
                                           & " where ID<>1 and ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                         & " where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                       & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ")) " _
                                           & " order by DENOMINAZIONE asc"
                    Case 2 ' FUORI LOTTO (EDIFICI e/o IMPIANTI)
                        'If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                                           & " where ID<>1 and ID NOT in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                            & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ") " _
                                           & " order by DENOMINAZIONE asc"

                        '                                    & " where ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                                        & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                        '                                                        & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                        '                                                                                  & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                                                                  & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))) " _
                        '                       & " order by DENOMINAZIONE asc"

                        'Else
                        '    If FlagConnessione = True Then
                        '        par.cmd.Dispose()
                        '        par.OracleConn.Close()
                        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        '    End If

                        '    Exit Sub
                        'End If

                    Case 3 ' FUORI LOTTO (solo IMPIANTI)

                        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                              & " from SISCOM_MI.EDIFICI " _
                                              & " where ID<>1 and ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                            & " where ID NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                          & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ")) " _
                                              & " order by DENOMINAZIONE asc"

                        'If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                        '    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                        '                       & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                        '                                     & " where ID NOT in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                   & " where ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                                                         & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                        '                                                                         & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                        '                                                                                                     & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                                                                                     & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")))) " _
                        '                            & " order by DENOMINAZIONE asc"

                        'Else
                        '    If FlagConnessione = True Then
                        '        par.cmd.Dispose()
                        '        par.OracleConn.Close()
                        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        '    End If
                        '    Exit Sub
                        'End If


                End Select
                par.caricaComboTelerik(par.cmd.CommandText, cmbEdificio, "ID", "DENOMINAZIONE", True)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    i = i + 1
                '    Me.cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                'End While

                'myReader1.Close()
                '**************************

                If i = 1 Then
                    Me.cmbEdificio.Items(1).Selected = True
                End If

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                'If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Then
                '    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                '                       & " from SISCOM_MI.EDIFICI " _
                '                       & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                    & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ") " _
                '                       & "    or ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI " _
                '                                                & " where ID in (select ID_IMPIANTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                                            & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue & ")) " _
                '                       & " order by DENOMINAZIONE asc"



                '    '    If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                '    '        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                '    '                           & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                '    '                                        & " where ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '    '                                                            & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue _
                '    '                                                            & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '    '                                                            & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")) " _
                '    '                           & " order by DENOMINAZIONE asc"

                '    '        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                '    '                           & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '    '                                        & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '    '                                                            & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue _
                '    '                                                            & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '    '                                                            & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")) " _
                '    '                           & " order by DENOMINAZIONE asc"

                '    '    Else
                '    '        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                '    '                           & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                '    '                                        & " where ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '    '                                                            & " where ID_APPALTO=" & Me.cmbAppalto.SelectedValue _
                '    '                                                            & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue & ")) " _
                '    '                           & " order by DENOMINAZIONE asc"

                '    '    End If
                'Else
                '    If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                '        'par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                '        '                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '        '                                & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '        '                                                    & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '        '                                                    & "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '        '                                                    & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))" _
                '        '                   & " order by DENOMINAZIONE asc"

                '        '& "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '        '                             & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ")" _
                '        '                             & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")))) " _



                '        par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI " _
                '                           & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                        & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                            & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '                                                            & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '                                                                                      & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                                                                      & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & "))) " _
                '                           & "    or ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI " _
                '                                                & " where ID in (select ID_IMPIANTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                                              & " where ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                                                    & " where ID_APPALTO<>" & Me.cmbAppalto.SelectedValue _
                '                                                                                    & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                '                                                                                                               & "   where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                                                                                               & "     and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")))) " _
                '                                & " order by DENOMINAZIONE asc"

                '    Else
                '        par.cmd.Dispose()
                '        par.OracleConn.Close()
                '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '        Exit Sub
                '    End If
                'End If


               
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub RBL1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL1.SelectedIndexChanged
        If RBL1.SelectedIndex = 0 Then
            Me.cmbComplesso.Enabled = True
            Me.LblComplesso.Enabled = True

            Me.cmbEdificio.Enabled = False
            Me.LblEdificio.Enabled = False
            FiltraComplessi()
        Else
            Me.cmbComplesso.Enabled = False
            Me.LblComplesso.Enabled = False

            Me.cmbEdificio.Enabled = True
            Me.LblEdificio.Enabled = True
            FiltraEdifici()
        End If
    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        CaricaServizi()
    End Sub



    'RICAVA LO STATO DELL'ESERCIZIO SELEZIONATO (5,6,7)
    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try

            CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value = -1

            If ID_ESERCIZIO < 0 Then Exit Sub

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN " _
                               & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & ID_ESERCIZIO

            Dim myReaderF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderF.Read Then
                CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value = par.IfNull(myReaderF("ID_STATO"), -1)
            End If
            myReaderF.Close()

            par.cmd.Parameters.Clear()

            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception
            ' Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

End Class
