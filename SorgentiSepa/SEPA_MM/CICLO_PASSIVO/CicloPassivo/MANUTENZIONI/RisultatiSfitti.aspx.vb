'*** LISTA RISULTATO ALLOGGI SFITTI

Partial Class MANUTENZIONI_RisultatiSfitti
    Inherits PageSetIdMode

    Dim par As New CM.Global

    Public sStringaSql As String
    Dim sWhere As String
    Dim sOrder As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then


            CaricaEsercizio()

        End If


    End Sub

  
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Session.Remove("IMP1")
        Response.Write("<script>document.location.href=""../../Pagina_home.aspx""</script>")
    End Sub


    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click



        If ControlloCampi() = True Then
            Session.Add("ID", Request.QueryString("ID"))


            Response.Write("<script>location.replace('Manutenzioni.aspx?TIPO=0" _
                                                                    & "&ED=" & Me.cmbIndirizzo.SelectedValue.ToString _
                                                                    & "&SE=" & Me.cmbServizio.SelectedValue.ToString _
                                                                    & "&SV=" & Me.cmbServizioVoce.SelectedValue.ToString _
                                                                    & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                    & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                    & "&TIPOR=4" _
                                                                    & "&PROVENIENZA=RISULTATI_SFITTI" & "');</script>")

        End If

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

            'ESERCIZI FINANZIARI
            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE, SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                               & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                               & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO" _
                               & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            If i = 1 Then
                Me.cmbEsercizio.Items(0).Selected = True
                Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            ElseIf i > 1 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If
                RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)
            End If
            '****************************


            'INDIRIZZO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA,SCALE_EDIFICI.ID as ID_SCALA, " _
                                      & " (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'|| SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE_EDIFICIO " _
                            & " from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI " _
                            & " where SISCOM_MI.UNITA_IMMOBILIARI.ID=" & Request.QueryString("ID") _
                            & "   and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA = SISCOM_MI.SCALE_EDIFICI.ID " _
                            & "   and SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO = SISCOM_MI.EDIFICI.ID "


            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then

                Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE_EDIFICIO"), " "), par.IfNull(myReader1("ID_EDIFICIO"), -1)))
                Me.cmbIndirizzo.Enabled = False

            End If
            myReader1.Close()


            '************CHIUSURA CONNESSIONE**********
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



            Setta_Servizio(0, "=")
            If Me.cmbServizio.Items.Count = 2 Then
                Me.cmbServizio.SelectedValue = Me.cmbServizio.Items(1).Value
            End If

            Setta_VoceServizio(0, "=")
            If Me.cmbServizioVoce.Items.Count = 2 Then
                Me.cmbServizioVoce.SelectedValue = Me.cmbServizioVoce.Items(1).Value
            End If

            Setta_Lotto_Appalto_Fornitore(">=")

            '********************





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


 Private Sub Setta_Servizio(ByVal ID As Long, ByVal sCondizione As String)
        Dim FlagConnessione As Boolean
        Dim sStr1 As String

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

            Me.cmbServizio.Items.Clear()
            Me.cmbServizio.Items.Add(New ListItem(" ", -1))

            Me.cmbServizioVoce.Items.Clear()
            Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            'EDIFICI
            sStr1 = "select ID,TRIM(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TAB_SERVIZI  " _
                 & " where ID in (select distinct(ID_SERVIZIO) from SISCOM_MI.PF_VOCI_IMPORTO, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                              & " where PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
                              & "   and PF_VOCI_IMPORTO.ID_LOTTO in (select ID from SISCOM_MI.LOTTI where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & sFiliale & ")" _
                              & "   and PF_VOCI_IMPORTO.ID_SERVIZIO <> 15 " _
                              & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID_APPALTO " _
                                                                         & " from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                         & " where ID_EDIFICIO= " & Me.cmbIndirizzo.SelectedValue & ")"

            Select Case par.IfEmpty(Me.txtSTATO_PF.Value, 5)
                Case 6
                    If Session.Item("FL_COMI") <> 1 Then
                        sStr1 = sStr1 & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                    End If
                Case 7
                    sStr1 = sStr1 & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1 and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "

            End Select

            sStr1 = sStr1 & "   and APPALTI_LOTTI_SERVIZI.ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P') )" _
                  & " order by DESCRIZIONE asc"


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            Me.cmbServizio.SelectedValue = "-1"


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


 Private Sub Setta_VoceServizio(ByVal ID As Long, ByVal sCondizione As String)
        Dim FlagConnessione As Boolean

        Try

            Me.cmbServizioVoce.Items.Clear()
            Me.cmbServizioVoce.Items.Add(New ListItem(" ", -1))

            'CType(Tab_Manu_Riepilogo.FindControl("txtLotto"), TextBox).Text = ""
            'CType(Tab_Manu_Riepilogo.FindControl("HLink_Appalto"), HyperLink).Text = ""
            'CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = ""



            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

          
            If Me.cmbServizio.SelectedValue <> "-1" Then

                'EDIFCIO
                par.cmd.CommandText = "select PF_VOCI_IMPORTO.ID, PF_VOCI.CODICE|| ' - ' ||TRIM(PF_VOCI_IMPORTO.DESCRIZIONE) as DESCRIZIONE " _
                                    & "from SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.PF_VOCI " _
                                    & "where PF_VOCI_IMPORTO.ID in (select ID_PF_VOCE_IMPORTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                                & " where ID_APPALTO in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                                                                     & " where ID_EDIFICIO=" & Me.cmbIndirizzo.SelectedValue & ") " _
                                                                & "   and ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='P')) " _
                                    & " and  PF_VOCI_IMPORTO.ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                                    & " and  PF_VOCI_IMPORTO.ID_SERVIZIO<>15" _

                Select Case par.IfEmpty(CType(Me.Page.FindControl("txtSTATO_PF"), HiddenField).Value, 5)
                    Case 6
                        If Session.Item("FL_COMI") <> 1 Then
                            par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                        Else
                            par.cmd.CommandText = par.cmd.CommandText & " and  PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ")) "
                        End If
                    Case 7
                        par.cmd.CommandText = par.cmd.CommandText & " and PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where FL_CC=1  and ID_PIANO_FINANZIARIO in (select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & " )) "
                    Case Else
                        par.cmd.CommandText = par.cmd.CommandText & " and  PF_VOCI_IMPORTO.ID_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ")) "

                End Select

                par.cmd.CommandText = par.cmd.CommandText & "  and  PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) " _
                                                          & " order by DESCRIZIONE asc"
            Else
                Exit Sub
            End If
          
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmbServizioVoce.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            Me.cmbServizioVoce.SelectedValue = "-1"

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

           ' If ID = 0 Then Setta_Lotto_Appalto_Fornitore(sCondizione)


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


    Private Sub Setta_Lotto_Appalto_Fornitore(ByVal sCondizione As String)
        Dim FlagConnessione As Boolean
        Dim sStr1 As String


        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.cmbAppalto.Items.Clear()
            Me.cmbAppalto.Items.Add(New ListItem(" ", -1))


            If Me.cmbServizioVoce.SelectedValue <> "-1" Then
           
                sStr1 = "select  SISCOM_MI.APPALTI.ID as ""ID_APPALTO"",(SISCOM_MI.APPALTI.NUM_REPERTORIO|| ' - ' || SISCOM_MI.APPALTI.DESCRIZIONE) as DESCRIZIONE " _
                     & " from SISCOM_MI.APPALTI " _
                     & " where SISCOM_MI.APPALTI.ID in (select ID_APPALTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                                 & " where  ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue & ")" _
                     & "  and SISCOM_MI.APPALTI.ID_STATO" & sCondizione & "1 " _
                     & "  and SISCOM_MI.APPALTI.TIPO='P' "

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), ""), par.IfNull(myReader1("ID_APPALTO"), -1)))
                End While
                myReader1.Close()

            End If
            Me.cmbAppalto.SelectedValue = "-1"

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


    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

       
        If Me.cmbServizio.SelectedValue = "-1" Then
            ControlloCampi = False
            Response.Write("<script>alert('Selezionare il Servizio!')</script>")
            Exit Function
        End If


        If Me.cmbServizioVoce.SelectedValue = "-1" Then
            ControlloCampi = False
            Response.Write("<script>alert('Selezionare la voce DGR!')</script>")
            Exit Function
        End If

        If Me.cmbAppalto.SelectedValue = "-1" Then
            ControlloCampi = False
            Response.Write("<script>alert('Selezionare il numero di repertorio!')</script>")
            Exit Function
        End If

    End Function


    Protected Sub cmbServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizio.SelectedIndexChanged

        Setta_VoceServizio(0, "=")
        If Me.cmbServizioVoce.Items.Count = 2 Then
            Me.cmbServizioVoce.SelectedValue = Me.cmbServizioVoce.Items(1).Value
        End If

        Setta_Lotto_Appalto_Fornitore(">=")
    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged

        RicavaStatoEsercizioFinanaziario(Me.cmbEsercizio.SelectedValue)

        Setta_Servizio(0, "=")
        If Me.cmbServizio.Items.Count = 2 Then
            Me.cmbServizio.SelectedValue = Me.cmbServizio.Items(1).Value
        End If

        Setta_VoceServizio(0, "=")
        If Me.cmbServizioVoce.Items.Count = 2 Then
            Me.cmbServizioVoce.SelectedValue = Me.cmbServizioVoce.Items(1).Value
        End If

        Setta_Lotto_Appalto_Fornitore(">=")
    End Sub

    Protected Sub cmbServizioVoce_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServizioVoce.SelectedIndexChanged

        Setta_Lotto_Appalto_Fornitore(">=")
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
End Class
