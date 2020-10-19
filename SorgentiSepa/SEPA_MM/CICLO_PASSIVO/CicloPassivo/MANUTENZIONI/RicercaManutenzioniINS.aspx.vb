Imports Telerik.Web.UI

'*** RICERCA MANUTENZIONI fase pre INSERIMENTO 

Partial Class MANUTENZIONI_RicercaManutenzioniINS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public TabberHide As String = ""

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            If Not IsPostBack Then
                TabberHide = "tabbertabhide"
                Tabber1 = "tabbertabdefault"

                Me.txtTIPO.Value = Request.QueryString("TIPOR")
                Me.txtSTATO_PF.Value = -1

                'ID ESERCIZIO FINANZIARIO
                'par.cmd.CommandText = "select ID from SISCOM_MI.PF_MAIN where ID_STATO=5 and ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                'If myReader1.Read Then
                '    Me.txtIdPianoFinanziario.Value = par.IfNull(myReader1("ID"), -1)
                'End If
                'myReader1.Close()
                '***********************************

                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CType(Tab_Ricerca1.FindControl("RBL1"), RadioButtonList).SelectedValue = 0
                CType(Tab_Ricerca2.FindControl("RBL1"), RadioButtonList).SelectedValue = 0

                CType(Tab_Ricerca1.FindControl("cmbEdificio"), RadComboBox).Enabled = False
                CType(Tab_Ricerca1.FindControl("LblEdificio"), Label).Enabled = False


                CaricaEsercizio() 'CaricaServizi()
                'CaricaIndirizzi()


            End If



            Me.txtTIPO.Value = Request.QueryString("TIPOR")
            CType(Tab_Ricerca1.FindControl("txtTIPO"), HiddenField).Value = Request.QueryString("TIPOR")
            CType(Tab_Ricerca2.FindControl("txtTIPO"), HiddenField).Value = Request.QueryString("TIPOR")


        Catch ex As Exception


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub


    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaEsercizio()
        Dim FlagConnessione As Boolean
        Dim i As Integer = 0
        Dim ID_ANNO_EF_CORRENTE As Long = -1

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as inizio, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') ||'-'|| TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') || '(' || SISCOM_MI.PF_STATI.DESCRIZIONE  || ')' as STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                               & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                               & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                               & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID order by SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID desc"
            par.caricaComboTelerik(par.cmd.CommandText, CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox), "ID", "STATO", False)
            par.caricaComboTelerik(par.cmd.CommandText, CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox), "ID", "STATO", False)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                'CType(Tab_Ricerca1.FindControl("cmbEsercizio"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("STATO"), " "), par.IfNull(myReader1("ID"), -1)))
                'CType(Tab_Ricerca2.FindControl("cmbEsercizio"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))

            End While

            myReader1.Close()


            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

            If i = 1 Then
                CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).Items(0).Selected = True
                CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).Enabled = False
                CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).Items(0).Selected = True
                CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).Enabled = False

            ElseIf i = 0 Then
                CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).Items.Clear()
                '   CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).Items.Add(New ListItem(" ", -1))
                CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).Enabled = False

                CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).Items.Clear()
                '  CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).Items.Add(New ListItem(" ", -1))
                CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).Enabled = False

                'TAB_1
                CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).Enabled = False
                CType(Tab_Ricerca1.FindControl("cmbServizioVoce"), RadComboBox).Enabled = False
                CType(Tab_Ricerca1.FindControl("cmbAppalto"), RadComboBox).Enabled = False
                CType(Tab_Ricerca1.FindControl("cmbComplesso"), RadComboBox).Enabled = False
                CType(Tab_Ricerca1.FindControl("cmbEdificio"), RadComboBox).Enabled = False

                'TAB_2
                CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).Enabled = False
                CType(Tab_Ricerca2.FindControl("cmbServizio"), RadComboBox).Enabled = False
                CType(Tab_Ricerca2.FindControl("cmbServizioVoce"), RadComboBox).Enabled = False
                CType(Tab_Ricerca2.FindControl("cmbAppalto"), RadComboBox).Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue = ID_ANNO_EF_CORRENTE
                    CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                RicavaStatoEsercizioFinanaziario(CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue)

                CaricaServizi()
                CaricaIndirizzi()
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


    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaServizi()
        Dim FlagConnessione As Boolean

        Try
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

            CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).Items.Clear()
            ' CType(Tab_Ricerca1.FindControl("cmbServizio"), DropDownList).Items.Add(New ListItem(" ", -1))



            par.cmd.CommandText = "select ID,TRIM(DESCRIZIONE) AS DESCRIZIONE  " _
                               & " from SISCOM_MI.TAB_SERVIZI " _
                               & " where ID in (select distinct(PF_VOCI_IMPORTO.ID_SERVIZIO) " _
                                            & " from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                            & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                                            & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID " _
                                                                               & " from SISCOM_MI.LOTTI " _
                                                                               & " where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & sFiliale & " ) " _
                                            & "   and PF_VOCI_IMPORTO.ID_SERVIZIO<>15 "

            Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                Case 6
                    If Session.Item("FL_COMI") <> 1 Then
                        'Prima del 13/01/2010 CODICE in ('2.04.01','2.04.04','3.01.01','3.02.01') ora FL_CC=1
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "
                    End If
                Case 7
                    par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1 and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "

            End Select

            par.cmd.CommandText = par.cmd.CommandText & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                            & " order by DESCRIZIONE asc"

            par.caricaComboTelerik(par.cmd.CommandText, CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox), "ID", "DESCRIZIONE", True)
            'par.caricaComboTelerik(par.cmd.CommandText, CType(Tab_Ricerca2.FindControl("cmbServizio"), RadComboBox), "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    CType(Tab_Ricerca1.FindControl("cmbServizio"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            'myReader1.Close()

            CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).SelectedValue = "-1"
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



    'CARICO COMBO INDIRIZZI del TAB. RICERCA 2
    Private Sub CaricaIndirizzi()
        Dim FlagConnessione As Boolean

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

            CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).Items.Clear()
            '   CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), DropDownList).Items.Add(New ListItem(" ", -1))


            'par.cmd.CommandText = "select distinct ID,DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
            '      & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                   & " where ID in (select ID_COMPLESSO from SISCOM_MI.LOTTI_PATRIMONIO " _
            '                                & " where  ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                                     & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
            '                                                                         & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
            '                                                     & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
            '                                                     & "  and  ID_SERVIZIO<>15))) " _
            '    & " order by DESCRIZIONE asc"

            'par.cmd.CommandText = "select DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
            '          & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                       & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
            '                                    & " where ID in (select ID_EDIFICIO from SISCOM_MI.LOTTI_PATRIMONIO " _
            '                                                 & " where  ID_LOTTO in ( select ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                                                      & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
            '                                                                                          & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
            '                                                                      & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
            '                                                                     & "  and  ID_SERVIZIO<>15)))) " _
            '          & " order by DESCRIZIONE asc"


            '  par.cmd.CommandText = "select DESCRIZIONE,ID from SISCOM_MI.INDIRIZZI " _
            '         & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
            '                      & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
            '                                   & " where ID in (select ID_EDIFICIO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
            '                                                & " where  ID_APPALTO in ( select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
            '                                                                       & " where  ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
            '                                                                                  & " where ID_ESERCIZIO_FINANZIARIO=" & par.RicavaEsercizioUltimoApprovato & sFiliale & ")" _
            '                                                               & "  and  ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1)" _
            '                                                               & "  and  ID_SERVIZIO<>15)))) " _
            '& " order by DESCRIZIONE asc"

            If Me.txtTIPO.Value = 0 Or Me.txtTIPO.Value = 2 Then
                par.cmd.CommandText = "select max(ID) as id,TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                   & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                             & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                          & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                 & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                 & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & sFiliale & ")" _
                                                                                                                                & "  and  ID_SERVIZIO<>15"

                Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & ") ) ) )     ) GROUP BY DESCRIZIONE order by DESCRIZIONE asc"

            Else


                par.cmd.CommandText = "select MAX(ID),TRIM(DESCRIZIONE) AS DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                   & " where ID in (select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.IMPIANTI " _
                                                             & " where ID_EDIFICIO is null and ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                          & " where D_FINE IS NULL AND ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                 & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                 & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & sFiliale & ")" _
                                                                                                                                & "  and  ID_SERVIZIO<>15"

                Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & ") ) ) ) " _
                                                & "   or ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                             & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.IMPIANTI " _
                                                                        & "  where ID in (select distinct(ID_IMPIANTO) from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                & " where D_FINE IS NULL AND  ID_APPALTO in ( select distinct(ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                                                 & "  where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')" _
                                                                                                 & "    and ID_PF_VOCE_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                                                                                & " where ID_LOTTO in (select ID from SISCOM_MI.LOTTI " _
                                                                                                                                                    & " where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & sFiliale & ")" _
                                                                                                                                & "  and  ID_SERVIZIO<>15"

                Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue & " )) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & ") ) ) ) )   ) GROUP BY DESCRIZIONE order by DESCRIZIONE asc"

            End If
            par.caricaComboTelerik(par.cmd.CommandText, CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox), "ID", "DESCRIZIONE", True)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            'End While
            myReader1.Close()

            CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1"
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
    Protected Sub btnCerca_Click(sender As Object, e As System.EventArgs) Handles btnCerca.Click


        Try

            If ControlloCampi() = True Then
                If Request.QueryString("TIPOR") <> 2 Then

                    If HiddenTabSelezionato.Value = 0 Then

                        Response.Write("<script>location.replace('RisultatiManutenzioniINS.aspx?IN_R=" & par.VaroleDaPassare(CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedItem.Text) _
                                                                                            & "&SE_R=" & CType(Tab_Ricerca2.FindControl("cmbServizio"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&SV_R=" & CType(Tab_Ricerca2.FindControl("cmbServizioVoce"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&AP_R=" & CType(Tab_Ricerca2.FindControl("cmbAppalto"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&UBI=" & CType(Tab_Ricerca2.FindControl("RBL1"), RadioButtonList).SelectedIndex _
                                                                                            & "&TIPOR=" & Me.txtTIPO.Value _
                                                                                            & "&EF_R=" & CType(Tab_Ricerca2.FindControl("cmbEsercizio"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&PROVENIENZA=RISULTATI_MANUTENZIONI_INS" & "');</script>")

                    Else

                        Response.Write("<script>location.replace('RisultatiManutenzioniINS.aspx?CO_R=" & CType(Tab_Ricerca1.FindControl("cmbComplesso"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&ED_R=" & CType(Tab_Ricerca1.FindControl("cmbEdificio"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&SE_R=" & CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&SV_R=" & CType(Tab_Ricerca1.FindControl("cmbServizioVoce"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&AP_R=" & CType(Tab_Ricerca1.FindControl("cmbAppalto"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&UBI=" & CType(Tab_Ricerca1.FindControl("RBL1"), RadioButtonList).SelectedIndex _
                                                                                            & "&TIPOR=" & Me.txtTIPO.Value _
                                                                                            & "&EF_R=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue.ToString _
                                                                                            & "&PROVENIENZA=RISULTATI_MANUTENZIONI_INS" & "');</script>")

                    End If
                Else
                    Response.Write("<script>location.replace('RisultatiManutenzioniINS.aspx?CO_R=" & CType(Tab_Ricerca1.FindControl("cmbComplesso"), RadComboBox).SelectedValue.ToString _
                                                                    & "&ED_R=" & CType(Tab_Ricerca1.FindControl("cmbEdificio"), RadComboBox).SelectedValue.ToString _
                                                                    & "&SE_R=" & CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).SelectedValue.ToString _
                                                                    & "&SV_R=" & CType(Tab_Ricerca1.FindControl("cmbServizioVoce"), RadComboBox).SelectedValue.ToString _
                                                                    & "&AP_R=" & CType(Tab_Ricerca1.FindControl("cmbAppalto"), RadComboBox).SelectedValue.ToString _
                                                                    & "&UBI=" & CType(Tab_Ricerca1.FindControl("RBL1"), RadioButtonList).SelectedIndex _
                                                                    & "&TIPOR=" & Me.txtTIPO.Value _
                                                                    & "&EF_R=" & CType(Tab_Ricerca1.FindControl("cmbEsercizio"), RadComboBox).SelectedValue.ToString _
                                                                    & "&PROVENIENZA=RISULTATI_MANUTENZIONI_INS" & "');</script>")


                End If

            Else
                Exit Sub
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub






    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

        If Me.txtTIPO.Value = 0 Or Me.txtTIPO.Value = 1 Then
            'NORMALE
            If HiddenTabSelezionato.Value = 0 Then
                'RICERCA2
                If CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), RadComboBox).SelectedValue = "-1" Then
                    ControlloCampi = False
                    RadWindowManager1.RadAlert("Selezionare l\'indirizzo!", 300, 150, "Attenzione", "", "null")

                    Exit Function
                End If
            Else
                'RICERCA1
                If CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).SelectedValue = "-1" Then
                    ControlloCampi = False
                    RadWindowManager1.RadAlert("Selezionare il Servizio!", 300, 150, "Attenzione", "", "null")
                    Exit Function
                End If
            End If
        Else
            'FUORI LOTTO
            If HiddenTabSelezionato.Value = 0 Then
                'RICERCA2 (NON  c'è più il tab RICERCA 2 per il fuori lottoo
                'If CType(Tab_Ricerca2.FindControl("cmbIndirizzo"), DropDownList).SelectedValue = "-1" Then
                '    ControlloCampi = False
                '    Response.Write("<script>alert('Selezionare l\'indirizzo!')</script>")
                '    Exit Function
                'End If

                'If CType(Tab_Ricerca2.FindControl("cmbServizio"), DropDownList).SelectedValue = "-1" Then
                '    ControlloCampi = False
                '    Response.Write("<script>alert('Selezionare il Servizio!')</script>")
                '    Exit Function
                'End If

                'If CType(Tab_Ricerca2.FindControl("cmbServizioVoce"), DropDownList).SelectedValue = "-1" Then
                '    ControlloCampi = False
                '    Response.Write("<script>alert('Selezionare la voce DGR!')</script>")
                '    Exit Function
                'End If
            Else
                'RICERCA1
                If CType(Tab_Ricerca1.FindControl("cmbServizio"), RadComboBox).SelectedValue = "-1" Then
                    ControlloCampi = False
                    RadWindowManager1.RadAlert("Selezionare il Servizio!", 300, 150, "Attenzione", "", "null")
                    Exit Function
                End If

                If CType(Tab_Ricerca1.FindControl("cmbServizioVoce"), RadComboBox).SelectedValue = "-1" Then
                    ControlloCampi = False
                    RadWindowManager1.RadAlert("Selezionare la voce DGR!", 300, 150, "Attenzione", "", "null")
                    'Response.Write("<script>alert('Selezionare la voce DGR!')</script>")
                    Exit Function
                End If

                If CType(Tab_Ricerca1.FindControl("cmbAppalto"), RadComboBox).SelectedValue = "-1" Then
                    ControlloCampi = False
                    RadWindowManager1.RadAlert("Selezionare il numero di repertorio!", 300, 150, "Attenzione", "", "null")
                    Exit Function
                End If

            End If

        End If

    End Function
    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
    End Sub

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
        End Select

        If Me.txtTIPO.Value = 2 Or Me.txtTIPO.Value = 3 Then
            'SUL FUORI LOTTO NON FACCIO APPARIRE LA RICERCA PER INDIRIZZO
            NascondiTab()
            TabberHide = "tabbertabhide"
        Else
            TabberHide = "tabbertab"
        End If


    End Sub


    'RICAVA LO STATO DELL'ESERCIZIO SELEZIONATO (5,6,7)
    Private Sub RicavaStatoEsercizioFinanaziario(ByVal ID_ESERCIZIO As Long)
        Dim FlagConnessione As Boolean

        Try
            Me.txtSTATO_PF.Value = -1

            If par.IfEmpty(ID_ESERCIZIO, -1) < 0 Then Exit Sub

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
                Me.txtSTATO_PF.Value = par.IfNull(myReaderF("ID_STATO"), -1)
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
            Me.txtSTATO_PF.Value = -1

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Private Sub NascondiTab()
        RadTabStrip.Tabs.FindTabByValue("ISC").Visible = False
        Dim i As Integer = 0
        For Each item As RadTab In RadTabStrip.Tabs
            If item.Visible = True Then
                RadMultiPage1.SelectedIndex = i
                RadTabStrip.SelectedIndex = i
                Exit For
            End If
            i += 1
        Next
    End Sub


End Class
