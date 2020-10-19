' TAB GENERALE DEL DETTAGLIO DELLA MOROSITA' DELL'INQUILINO

Partial Class Tab_RicercaINS_2
    Inherits UserControlSetIdMode
    Dim par As New CM.Global



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            'If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then
            'FrmSolaLettura()
            'End If
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



    Protected Sub BtnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnCerca.Click
        Dim FlagConnessione As Boolean
        Dim i As Integer

        Try

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

            Dim condizione As String = ""
            Dim StringaSql As String = ""
            'Dim wherecond As Boolean = False
            'Dim primo As Boolean = False

            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then

                If RBL1.SelectedIndex = 0 Then
                    'StringaSql = "select DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
                    '          & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
                    '          & "   and ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                       & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                    '                                    & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                    '                                                 & " where  ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                                                      & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                    '                                                                                          & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
                    '                                                                      & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                    '                                                                     & "  and  ID_SERVIZIO<>15)))) " _
                    '          & " order by DESCRIZIONE asc"


                    If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then
                        'COMPLESSO (EDIFICI e/o IMPIANTI)
                        StringaSql = "select TRIM(DESCRIZIONE) as DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
                                  & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
                                  & "   and ID in (select distinct(ID_INDIRIZZO_RIFERIMENTO) from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                               & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                            & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                         & " where D_FINE IS NULL AND  ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                & " where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                               & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                               & "   and ID_SERVIZIO<>15) " _
                                                                                & " )))) " _
                                  & " order by DESCRIZIONE asc"

                    Else
                        'COMPLESSO (LOTTO Solo IMPIANTI)
                        StringaSql = "select TRIM(DESCRIZIONE) as DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
                                  & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
                                  & "   and ID in (select distinct(ID_INDIRIZZO_RIFERIMENTO) from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                               & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.IMPIANTI " _
                                                            & " where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                         & " where D_FINE IS NULL AND  ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                & " where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                               & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                               & "   and ID_SERVIZIO<>15) " _
                                                                                & " )))) " _
                                  & " order by DESCRIZIONE asc"
                    End If

                    par.cmd.CommandText = StringaSql

                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader1.Read
                        ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                    End While

                Else
                    ' StringaSql = "select DISTINCT DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                    '           & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
                    '           & "   and ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI " _
                    '                        & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                    '                                     & " where  ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                                          & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                    '                                                                              & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
                    '                                                          & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                    '                                                          & "  and  ID_SERVIZIO<>15))) " _
                    '& " order by DESCRIZIONE asc"

                    If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then
                        'EDIFICI (LOTTO EDIFICI e/o IMPIANTI)
                        StringaSql = "select DISTINCT(TRIM(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                  & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
                                  & "   and ID in (select distinct(ID_INDIRIZZO_PRINCIPALE) from SISCOM_MI.EDIFICI " _
                                               & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                            & " where D_FINE IS NULL AND  ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                   & " where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                   & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                 & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                 & "   and ID_SERVIZIO<>15) " _
                                  & "       ))) order by DESCRIZIONE asc"

                    Else
                        'EDIFICI (Lotto Solo IMPIANTI)
                        StringaSql = "select DISTINCT(TRIM(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                  & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' " _
                                  & "   and ID in (select distinct(ID_INDIRIZZO_PRINCIPALE) from SISCOM_MI.EDIFICI " _
                                                & " where ID in (select ID_EDIFICIO from SISCOM_MI.IMPIANTI " _
                                                              & " where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                           & " where D_FINE IS NULL AND  ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                  & " where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                  & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                                & "   and ID_SERVIZIO<>15) " _
                                  & "       )))) order by DESCRIZIONE asc"

                    End If

                    par.cmd.CommandText = StringaSql

                    i = 1
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader1.Read
                        ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), i))
                        i = i + 1
                    End While

                End If
            End If


            If ListEdifci.Items.Count = 0 Then
                Me.LblNoResult.Visible = True
            Else
                Me.LblNoResult.Visible = False
            End If

            Me.TextBox1.Value = 2

                '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click

        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                If RBL1.SelectedIndex = 0 Then
                    Me.cmbIndirizzo.SelectedValue = Me.ListEdifci.SelectedValue.ToString
                Else
                    Me.cmbIndirizzo.SelectedValue = Me.ListEdifci.SelectedItem.Text
                End If

                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.TextBox1.Value = 1
                Me.LblNoResult.Visible = False
            Else
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.LblNoResult.Visible = False
                Me.TextBox1.Value = 1
            End If

            FiltraServizio()


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged


        Me.TextBox1.Value = ""
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()

        FiltraServizio()
    End Sub


    Private Sub FiltraServizio()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean


        Try

            Me.cmbServizio.Items.Clear()
            ' Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            ' Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            Me.cmbAppalto.Items.Clear()
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))

            If par.IfEmpty(Me.cmbIndirizzo.SelectedValue, "-1") <> "-1" Or Strings.Len(Strings.Trim(Me.cmbIndirizzo.SelectedItem.Text)) > 0 Then


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


                If RBL1.SelectedIndex = 0 Then
                    'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                    '                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                & " where ID_LOTTO in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO " _
                    '                                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                                                         & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                                                & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & "))" _
                    '                                                  & "   and ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                    '                                                                       & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & "))" _
                    '                                & "   and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                    '                                & "   and ID_SERVIZIO<>15) " _
                    '     & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE asc"

                    'COMPLESSO
                    If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then

                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.TAB_SERVIZI  " _
                                            & " where ID in (select ID_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                         & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                         & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                         & "   and PF_VOCI_IMPORTO.ID_SERVIZIO <> 15 " _
                                                         & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select distinct(ID_APPALTO) " _
                                                                                                    & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                    & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                           & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                                                    & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) )"

                        Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') )" _
                                            & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE asc"

                    Else
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.TAB_SERVIZI  " _
                                            & " where ID in (select ID_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                         & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                         & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                         & "   and PF_VOCI_IMPORTO.ID_SERVIZIO <> 15 " _
                                                         & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select distinct(ID_APPALTO) " _
                                                                                                    & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                    & " where D_FINE IS NULL AND ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI " _
                                                                                                                          & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                                                    & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) )"

                        Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') )" _
                                            & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE asc"
                    End If


                Else
                    'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                    '                   & " where ID in (select ID_SERVIZIO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                & " where ID_LOTTO  in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO " _
                    '                                                    & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                                                           & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                    '                                                                                                             & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')) " _
                    '                                                    & "   and ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                    '                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & "))" _
                    '                                & "   and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                    '                                & "   and ID_SERVIZIO<>15) " _
                    '     & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE asc"

                    'EDIFICIO
                    If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then

                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.TAB_SERVIZI  " _
                                            & " where ID in (select ID_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                         & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                         & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ") " _
                                                         & "   and PF_VOCI_IMPORTO.ID_SERVIZIO <> 15 " _
                                                         & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select distinct(ID_APPALTO) " _
                                                                                                    & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                    & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI  " _
                                                                                                                          & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')) )"


                        Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') )" _
                                            & " order by DESCRIZIONE asc"
                    Else
                        par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.TAB_SERVIZI  " _
                                            & " where ID in (select ID_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                         & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                                         & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ") " _
                                                         & "   and PF_VOCI_IMPORTO.ID_SERVIZIO <> 15 " _
                                                         & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select distinct(ID_APPALTO) " _
                                                                                                    & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                                    & " where D_FINE IS NULL AND ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                                                                                  & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')) ) ) "

                        Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                            Case 6
                                If Session.Item("FL_COMI") <> 1 Then
                                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                                End If
                            Case 7
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        End Select

                        par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') )" _
                                            & " order by DESCRIZIONE asc"

                    End If

                    'If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Then
                    '    par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                    '                       & " where ID in (select ID_SERVIZIO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                    & " where ID_LOTTO  in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO " _
                    '                                                        & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                               & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & ")" _
                    '                                                        & "   and ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                    '                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & "))" _
                    '                                    & "   and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                    '                                    & "   and ID_SERVIZIO<>15) " _
                    '         & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE asc"

                    'Else
                    '    par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.TAB_SERVIZI " _
                    '                       & " where ID in (select ID_SERVIZIO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                    & " where ID_LOTTO  in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO " _
                    '                                                        & " where ID_COMPLESSO not in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                               & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & ")" _
                    '                                                        & "   and ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                    '                                                                           & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & "))" _
                    '                                    & "   and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                    '                                    & "   and ID_SERVIZIO<>15) " _
                    '         & " order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE asc"

                    'End If

                End If
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizio, "ID", "DESCRIZIONE", True)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                '    i = i + 1
                'End While
                'myReader1.Close()
                '**************************

                If i = 1 Then
                    Me.cmbServizio.Items(1).Selected = True
                    FiltraDettaglio()
                End If


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


            If cmbServizio.SelectedValue <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                If RBL1.SelectedIndex = 0 Then
                    'COMPLESSO
                    'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.PF_VOCI_IMPORTO " _
                    '                    & "where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                 & " where ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    '                                                       & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                                                              & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                                                     & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & ")))" _
                    '                                  & " and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                    '                                  & " and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)) " _
                    '                    & "  and ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " _
                    '                    & " order by SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE asc"

                    '& " and  ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " 

                    If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then

                        par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                            & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                            & "where PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                        & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                        & " where ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                             & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                                          & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) )" _
                                                                        & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                            & "  and PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                            & "  and PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                              & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                            & "  and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                            & " order by DESCRIZIONE asc"

                    Else
                        par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                            & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                            & "where PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                        & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                        & " where ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                             & " where D_FINE IS NULL AND ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI " _
                                                                                                                   & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                                          & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) ) " _
                                                                        & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                            & " and  PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                            & " and  PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                       & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                            & " and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                            & " order by DESCRIZIONE asc"
                    End If

                Else
                    'EDIFICI
                    'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.PF_VOCI_IMPORTO " _
                    '                    & "where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                 & " where ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    '                                                       & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                                                             & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                    '                                                                                                               & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) " _
                    '                                  & " and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                    '                                  & " and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)) " _
                    '                    & "  and ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " _
                    '                    & " order by SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE asc"

                    If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then

                        par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                            & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                            & "where PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                        & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                        & " where ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                             & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                    & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                                                                                      & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')) )" _
                                                                        & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                            & " and  PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                            & " and  PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                              & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                            & " and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                            & " order by DESCRIZIONE asc"
                    Else
                        par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                            & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                            & "where PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO " _
                                                                        & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                        & " where ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                             & " where D_FINE IS NULL AND ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI " _
                                                                                                                  & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                                        & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                                                                                                        & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')) )  )" _
                                                                        & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                            & " and  PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                            & " and  PF_VOCI_IMPORTO.ID_LOTTO in (select ID  from SISCOM_MI.LOTTI  " _
                                                                              & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                            & " and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                            & " order by DESCRIZIONE asc"
                    End If

                End If

                'par.cmd.CommandText = "select ID,DESCRIZIONE from SISCOM_MI.PF_VOCI_IMPORTO " _
                '                    & "where ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                & " where  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                '                                & " and    ID_LOTTO in (select ID_LOTTO from SISCOM_MI.LOTTI_PATRIMONIO )" _
                '                                & " and    ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)) " _
                '                    & "  and ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value & ") " _
                '                   & " order by SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE asc"
                par.caricaComboTelerik(par.cmd.CommandText, cmbServizioVoce, "ID", "DESCRIZIONE", True)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    i = i + 1
                '    Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
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

    Protected Sub cmbServizioVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizioVoce.SelectedIndexChanged
        FiltraAppalti()
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
            'Me.cmbAppalto.Items.Add(New ListItem(" ", -1))



            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = " and ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If


            If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Then

                If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                    If RBL1.SelectedIndex = 0 Then
                        'par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                        '                    & " from  SISCOM_MI.APPALTI " _
                        '                    & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                 & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                 & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                        '                                 & "   and ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                        '                                                                          & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                        '                                                                                                 & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & ")))) " _
                        '                    & "   and ID_STATO=1" _
                        '                    & " order by NUM_REPERTORIO "

                        If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then

                            par.cmd.CommandText = " select appalti.ID,TRIM(NUM_REPERTORIO) || ' - ' ||  fornitori.ragione_sociale as descrizione  " _
                                                & " from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                                & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                             & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                             & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                               & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                      & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                             & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) ) )" _
                                                & "   and ID_STATO=1" _
                                                & "   and appalti.TIPO='P'" _
                                                & "   and fornitori.id = appalti.id_fornitore" _
                                                & " order by NUM_REPERTORIO "
                        Else
                            par.cmd.CommandText = " select appalti.ID,TRIM(NUM_REPERTORIO) || ' - ' ||  fornitori.ragione_sociale as descrizione   " _
                                                & " from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                                & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                             & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                             & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                               & " where D_FINE IS NULL AND ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI " _
                                                                                                    & " where  ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                             & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) ) ) " _
                                                & "   and ID_STATO=1" _
                                                & "   and appalti.TIPO='P'" _
                                                & "   and fornitori.id = appalti.id_fornitore" _
                                                & " order by NUM_REPERTORIO "
                        End If


                    Else
                        If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then

                            par.cmd.CommandText = " select appalti.ID,TRIM(NUM_REPERTORIO) || ' - ' ||  fornitori.ragione_sociale as descrizione   " _
                                                & " from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                                & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                             & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                             & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                               & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                     & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                                                                       & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')) ) )" _
                                                & "   and ID_STATO=1" _
                                                & "   and appalti.TIPO='P'" _
                                                & "   and fornitori.id = appalti.id_fornitore" _
                                                & " order by NUM_REPERTORIO "
                        Else
                            par.cmd.CommandText = " select appalti.ID,TRIM(NUM_REPERTORIO) || ' - ' ||  fornitori.ragione_sociale as descrizione   " _
                                                & " from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                                & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                             & " where ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                                                             & "   and ID_APPALTO in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                               & " where D_FINE IS NULL AND ID_IMPIANTO in (select ID from SISCOM_MI.IMPIANTI " _
                                                                                                      & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                                            & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                                                                                              & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))) ) ) " _
                                                & "   and ID_STATO=1" _
                                                & "   and appalti.TIPO='P'" _
                                                & "   and fornitori.id = appalti.id_fornitore" _
                                                & " order by NUM_REPERTORIO "
                        End If

                    End If
                Else

                    'If RBL1.SelectedIndex = 0 Then
                    '    par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                    '                        & " from  SISCOM_MI.APPALTI " _
                    '                        & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                     & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                    '                                     & "   and ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    '                                                       & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                                                              & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                    '                                                                                                     & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & ")))) " _
                    '                        & "   and ID_STATO=1" _
                    '                        & " order by NUM_REPERTORIO "
                    'Else
                    '    par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                    '                        & " from  SISCOM_MI.APPALTI " _
                    '                        & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    '                                     & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                    '                                     & "   and ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    '                                                       & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                    '                                                                             & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                    '                                                                                                               & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')))) " _
                    '                        & "   and ID_STATO=1" _
                    '                        & " order by NUM_REPERTORIO "
                    'End If
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If

                    Exit Sub
                End If
            Else
                If Me.cmbServizioVoce.SelectedValue <> "-1" Then

                    If RBL1.SelectedIndex = 0 Then
                        '    par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                        '                        & " from  SISCOM_MI.APPALTI " _
                        '                        & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                        '                                     & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                                     & "   and ID_PF_VOCE_IMPORTO<>" & Me.cmbServizioVoce.SelectedValue _
                        '                                     & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                        '                                                                  & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ")) " _
                        '                                     & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ") " _
                        '                                     & "   and  ID_APPALTO not in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                        '                                                            & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                        '                                                                                  & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                        '                                                                                                        & " where ID_INDIRIZZO_RIFERIMENTO=" & Me.cmbIndirizzo.SelectedValue & ")))) " _
                        '                        & "   and ID_STATO=1" _
                        '                        & " order by NUM_REPERTORIO "
                        'COMPLESSO
                        par.cmd.CommandText = " select appalti.ID,TRIM(NUM_REPERTORIO)  || ' - ' || fornitori.ragione_sociale as DESCRIZIONE " _
                                            & " from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                            & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                         & " where ID_PF_VOCE_IMPORTO<>" & Me.cmbServizioVoce.SelectedValue _
                                                         & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                                                                                      & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ") " _
                                                                                      & "   and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                                                                      & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")) " _
                                                         & "   and  ID_APPALTO not in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                   & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                         & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                                                                                & " where ID_INDIRIZZO_RIFERIMENTO IN(select ID from SISCOM_MI.INDIRIZZI where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%'))))) " _
                                            & "   and ID_STATO=1" _
                                            & "   and appalti.TIPO='P'" _
                                            & "   and fornitori.id = appalti.id_fornitore" _
                                            & " order by NUM_REPERTORIO "

                    Else
                        'EDIFICIO
                        par.cmd.CommandText = " select appalti.ID,TRIM(NUM_REPERTORIO)  || ' - ' || fornitori.ragione_sociale as DESCRIZIONE " _
                                            & " from  SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI " _
                                            & " where appalti.ID in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                         & " where ID_PF_VOCE_IMPORTO<>" & Me.cmbServizioVoce.SelectedValue _
                                                         & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
                                                                                      & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ") " _
                                                                                      & "   and  ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                                                                      & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")) " _
                                                         & "   and ID_APPALTO not in (select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                               & " where D_FINE IS NULL AND ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                                                      & " where ID_INDIRIZZO_PRINCIPALE in (select ID from SISCOM_MI.INDIRIZZI " _
                                                                                                                                        & " where DESCRIZIONE like '%" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "%')))) " _
                                            & "   and ID_STATO=1" _
                                            & "   and appalti.TIPO='P'" _
                                            & "   and fornitori.id = appalti.id_fornitore" _
                                            & " order by NUM_REPERTORIO "

                    End If

                Else
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                    Exit Sub
                End If
            End If


            'If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Then

            '    If Me.cmbServizioVoce.SelectedValue <> "-1" Then

            '        par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
            '                            & " from  SISCOM_MI.APPALTI " _
            '                            & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                         & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
            '                                         & "   and ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")" _
            '                            & "   and ID_STATO=1" _
            '                            & " order by NUM_REPERTORIO "
            '    Else

            '        par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
            '                            & " from  SISCOM_MI.APPALTI " _
            '                            & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                         & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue & ")" _
            '                            & "   and ID_STATO=1" _
            '                            & " order by NUM_REPERTORIO "

            '    End If
            'Else
            '    If Me.cmbServizioVoce.SelectedValue <> "-1" Then

            '        par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
            '                            & " from  SISCOM_MI.APPALTI " _
            '                            & " where ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                         & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
            '                                         & "   and ID_PF_VOCE_IMPORTO<>" & Me.cmbServizioVoce.SelectedValue _
            '                                         & "   and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO  " _
            '                                                      & " where  ID_VOCE_SERVIZIO=(select ID_VOCE_SERVIZIO from SISCOM_MI.PF_VOCI_IMPORTO where ID=" & Me.cmbServizioVoce.SelectedValue & ")" _
            '                                                      & "   and  ID_LOTTO in (select ID from SISCOM_MI.LOTTI  where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")))" _
            '                            & "   and ID_STATO=1" _
            '                            & " order by NUM_REPERTORIO "

            '    Else
            '        par.cmd.Dispose()
            '        par.OracleConn.Close()
            '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '        Exit Sub
            '    End If
            'End If

            par.caricaComboTelerik(par.cmd.CommandText, cmbAppalto, "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    Dim spazio As String = " - "
            '    i = i + 1
            '    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " ") & spazio & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
        CaricaIndirizzi()
        Me.TextBox1.Value = ""
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()

        FiltraServizio()
    End Sub


    'CARICO COMBO INDIRIZZI del TAB. RICERCA 2
    Private Sub CaricaIndirizzi()
        Dim FlagConnessione As Boolean
        Dim i As Integer

        Try
            ' APRO CONNESSIONE
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

            Me.cmbIndirizzo.Items.Clear()
            'Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            If RBL1.SelectedIndex = 0 Then
                'par.cmd.CommandText = "select DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
                '                   & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '                                & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '                                             & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                '                                                          & " where  ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                                               & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                '                                                                                                   & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
                '                                                                               & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                '                                                                               & "  and  ID_SERVIZIO<>15)))) " _
                '                    & " order by DESCRIZIONE asc"

                'par.cmd.CommandText = "select DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
                '                   & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '                                & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                '                                             & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                                          & " where  ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                                                 & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                '                                                                                                      & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
                '                                                                                 & "   and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                '                                                                                  & "  and  ID_SERVIZIO<>15)))) " _
                '                    & " order by DESCRIZIONE asc"

                'COMPLESSO (EDIFICI e/o IMPIANTI)
                If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then
                    par.cmd.CommandText = "select TRIM(DESCRIZIONE) as DESCRIZIONE,MAX(ID) AS ID from SISCOM_MI.INDIRIZZI " _
                                       & " where ID in (select distinct(ID_INDIRIZZO_RIFERIMENTO) from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                    & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                                 & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                              & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                     & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                     & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                    & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                                    & "  and  ID_SERVIZIO<>15"

                    Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                            End If
                        Case 7
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                    End Select


                    par.cmd.CommandText = par.cmd.CommandText & ") ) ) )     ) GROUP BY DESCRIZIONE order by DESCRIZIONE asc"

                Else
                    ' COMPLESSO (solo IMPIANTI)
                    par.cmd.CommandText = "select TRIM(DESCRIZIONE) as DESCRIZIONE,MAX(ID) AS ID from SISCOM_MI.INDIRIZZI " _
                                       & " where ID in (select distinct(ID_INDIRIZZO_RIFERIMENTO) from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                    & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.IMPIANTI " _
                                                                 & " where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                              & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                     & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                     & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                    & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                                    & "  and  ID_SERVIZIO<>15"

                    Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                            End If
                        Case 7
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                    End Select

                    par.cmd.CommandText = par.cmd.CommandText & ") ) ) ) " _
                                                    & "   or ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                                 & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                                            & "  where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                    & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                     & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                     & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                    & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                        & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                                    & "  and  ID_SERVIZIO<>15"

                    Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                            End If
                        Case 7
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                    End Select

                    par.cmd.CommandText = par.cmd.CommandText & ") ) ) ) )    ) GROUP BY DESCRIZIONE order by DESCRIZIONE asc"

                End If
                par.caricaComboTelerik(par.cmd.CommandText, cmbIndirizzo, "ID", "DESCRIZIONE", True)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), "-1")))
                'End While
                myReader1.Close()

            Else
                'par.cmd.CommandText = "select distinct DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                '      & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI " _
                '                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
                '                                & " where  ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                     & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                '                                                                         & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
                '                                                     & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                '                                                     & "  and  ID_SERVIZIO<>15))) " _
                '    & " order by DESCRIZIONE asc"

                'par.cmd.CommandText = "select distinct DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                '      & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI " _
                '                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                '                                & " where  ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                '                                                       & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                '                                                                         & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
                '                                                        & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
                '                                                        & "  and  ID_SERVIZIO<>15))) " _
                '    & " order by DESCRIZIONE asc"

                If CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 0 Or CType(Me.Page.FindControl("txtTIPO"), HiddenField).Value = 2 Then
                    'EDIFCI (Edifici e/o IMPIANTI)
                    par.cmd.CommandText = "select distinct DESCRIZIONE,MAX(id) AS ID from SISCOM_MI.INDIRIZZI " _
                          & " where ID in (select distinct(ID_INDIRIZZO_PRINCIPALE) from SISCOM_MI.EDIFICI " _
                                       & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                    & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                           & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                           & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                          & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                              & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                          & "  and  ID_SERVIZIO<>15"

                    Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                            End If
                        Case 7
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                    End Select

                    par.cmd.CommandText = par.cmd.CommandText & ")))  ) GROUP BY DESCRIZIONE order by DESCRIZIONE asc"

                Else
                    'EDIFCI (Edifici e/o IMPIANTI)
                    par.cmd.CommandText = "select distinct DESCRIZIONE,id from SISCOM_MI.INDIRIZZI " _
                          & " where ID in (select distinct(ID_INDIRIZZO_PRINCIPALE) from SISCOM_MI.EDIFICI " _
                                       & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                  & "  where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                      & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                      & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                    & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                      & " where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                                                                                                                    & "  and  ID_SERVIZIO<>15"

                    Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                            End If
                        Case 7
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

                    End Select

                    par.cmd.CommandText = par.cmd.CommandText & " )))) ) order by DESCRIZIONE asc"

                End If

                i = 1
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                par.caricaComboTelerik(par.cmd.CommandText, cmbIndirizzo, "ID", "DESCRIZIONE", True)
                'While myReader1.Read
                '    Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("DESCRIZIONE"), "-1")))
                '    i = i + 1
                'End While
                myReader1.Close()

            End If

            Me.cmbIndirizzo.SelectedValue = "-1"
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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


    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
        CaricaIndirizzi()
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
