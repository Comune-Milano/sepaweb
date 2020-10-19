'*** RICERCA LOTTI per SCAMBIARE I COMPLESSI

Partial Class LOTTI_RicercaLottiScambio
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then

            Me.cmbEsercizio.Items.Clear()
            cmbEsercizio.Items.Add(New ListItem(" ", -1))

            Me.cmbFiliale.Items.Clear()
            cmbFiliale.Items.Add(New ListItem(" ", -1))

            Me.cmbTipoServizio.Items.Clear()
            cmbTipoServizio.Items.Add(New ListItem(" ", -1))

            CaricaLotti() 'ricerco tutti gli ID_LOTTO da APPALTI_LOTTI_SERVIZI dover hanno più di un ID_APPALTO 
            '              questo perchè quelli che hanno più di un appalto non possono subire variazioni
            CaricaEsercizio()



        End If

    End Sub



    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaLotti()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.txtID_LOTTI.Value = ""


            par.cmd.CommandText = "select distinct ID_LOTTO from SISCOM_MI.APPALTI_LOTTI_SERVIZI"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read

                par.cmd.CommandText = "select count(distinct ID_APPALTO) from SISCOM_MI.APPALTI_LOTTI_SERVIZI where ID_LOTTO=" & par.IfNull(myReader1(0), -1)
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read() = True Then
                    If par.IfNull(myReader2(0), -1) > 1 Then
                        If Me.txtID_LOTTI.Value = "" Then
                            Me.txtID_LOTTI.Value = par.IfNull(myReader1(0), -1)
                        Else
                            Me.txtID_LOTTI.Value = Me.txtID_LOTTI.Value & "," & par.IfNull(myReader1(0), -1)
                        End If
                    End If
                End If
                myReader2.Close()

            End While
            myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    'CARICO COMBO ESERCIZIO FINANZIARIO
    Private Sub CaricaEsercizio()

        Try

            Dim sFiliale As String = ""
            If Session.Item("LIVELLO") <> "1" Then
                sFiliale = "  ID_FILIALE=" & Session.Item("ID_STRUTTURA")
            End If


            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.cmbEsercizio.Items.Clear()

            ' CARICO tutti gli Esercizi Finanziari presenti dei LOTTI caricati tranne quelli che hanno più appalti in 
            '       APPALTI_LOTTI_SERVIZII e quelli della FILIALE dell'Operatore se l'operatore non è AMMINISTRATORE

            par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID," _
                                     & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as ""INIZIO""," _
                                     & " TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS ""FINE""" _
                               & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                               & " where ID in (select ID_ESERCIZIO_FINANZIARIO " _
                                            & " from SISCOM_MI.LOTTI "

            If sFiliale <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " where " & sFiliale

                If Strings.Len(Me.txtID_LOTTI.Value) > 0 Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID not in (" & Me.txtID_LOTTI.Value & ") "
                End If
            ElseIf Strings.Len(Me.txtID_LOTTI.Value) > 0 Then
                par.cmd.CommandText = par.cmd.CommandText & " where ID not in (" & Me.txtID_LOTTI.Value & ") "
            End If


            par.cmd.CommandText = par.cmd.CommandText & ") order by ID DESC"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))

            While myReader1.Read
                Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO"), " ") & "-" & par.IfNull(myReader1("FINE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            cmbEsercizio.SelectedValue = "-1"


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    'CARICO COMBO FILIALE 
    Private Sub FiltraFiliale()
        Dim i As Integer = 0

        Try

            Me.cmbFiliale.Items.Clear()
            cmbFiliale.Items.Add(New ListItem(" ", -1))

            If Me.cmbEsercizio.SelectedValue <> "-1" Then

                ' APRO CONNESSIONE
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI " _
                                       & " where SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID (+) " _
                                       & "   and SISCOM_MI.TAB_FILIALI.ID in (select ID_FILIALE  " _
                                                                          & " from SISCOM_MI.LOTTI " _
                                                                          & " where ID_ESERCIZIO_FINANZIARIO=" & cmbEsercizio.SelectedValue.ToString
                If Strings.Len(Me.txtID_LOTTI.Value) > 0 Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID not in (" & Me.txtID_LOTTI.Value & ") "
                End If

                par.cmd.CommandText = par.cmd.CommandText & ") order by nome asc"

                'par.cmd.CommandText = "select * from SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, OPERATORI " _
                '                   & " where OPERATORI.ID=" & Session.Item("ID_OPERATORE") _
                '                   & "   and TAB_FILIALI.ID=OPERATORI.ID_UFFICIO " _
                '                   & "   and SISCOM_MI.TAB_FILIALI.ID_INDIRIZZO = SISCOM_MI.INDIRIZZI.ID " _
                '                   & "   and SISCOM_MI.TAB_FILIALI.ID in (select ID_FILIALE  " _
                '                                                      & " from SISCOM_MI.LOTTI "

                'If Strings.Len(Me.txtID_LOTTI.Value) > 0 Then
                '    par.cmd.CommandText = par.cmd.CommandText & " where ID not in (" & Me.txtID_LOTTI.Value & ") "
                'End If
                'par.cmd.CommandText = par.cmd.CommandText & ") order by TAB_FILIALI.nome asc"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    i = i + 1
                    cmbFiliale.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " ") & "  -  " & par.IfNull(myReader1("DESCRIZIONE"), "") & " " & par.IfNull(myReader1("CIVICO"), "") & " " & par.IfNull(myReader1("LOCALITA"), ""), par.IfNull(myReader1("ID"), -1)))
                End While

                myReader1.Close()
                '**************************

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If i = 1 Then
                    Me.cmbFiliale.Items(1).Selected = True
                End If

            End If

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Private Sub FiltraServizio()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            Me.cmbTipoServizio.Items.Clear()
            cmbTipoServizio.Items.Add(New ListItem(" ", -1))

            If Me.cmbFiliale.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "select * " _
                                   & " from  SISCOM_MI.TAB_SERVIZI " _
                                   & " where SISCOM_MI.TAB_SERVIZI.ID in (select ID_SERVIZIO " _
                                                                      & " from SISCOM_MI.LOTTI_SERVIZI " _
                                                                      & " where ID_LOTTO in (select ID " _
                                                                                         & " from SISCOM_MI.LOTTI " _
                                                                                         & " where ID_FILIALE=" & cmbFiliale.SelectedValue.ToString _
                                                                                         & "   and ID_ESERCIZIO_FINANZIARIO=" & cmbEsercizio.SelectedValue.ToString
                If Strings.Len(Me.txtID_LOTTI.Value) > 0 Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID not in (" & Me.txtID_LOTTI.Value & ") "
                End If

                par.cmd.CommandText = par.cmd.CommandText & ")) order by SISCOM_MI.TAB_SERVIZI.DESCRIZIONE ASC"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    i = i + 1
                    cmbTipoServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While

                myReader1.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If i = 1 Then
                    cmbTipoServizio.Items(1).Selected = True
                End If

            End If



        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Private Sub FiltraDettaglioServizio()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            Me.cmbDettaglioServizio.Items.Clear()
            cmbDettaglioServizio.Items.Add(New ListItem(" ", -1))

            If Me.cmbTipoServizio.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                par.cmd.CommandText = "select * " _
                                   & " from  SISCOM_MI.TAB_SERVIZI_VOCI " _
                                   & " where SISCOM_MI.TAB_SERVIZI_VOCI.ID in (select ID_VOCE_SERVIZIO " _
                                                                           & " from  SISCOM_MI.PF_VOCI_IMPORTO " _
                                                                           & " where ID_SERVIZIO=" & Me.cmbTipoServizio.SelectedValue _
                                                                           & "   and ID_LOTTO in (select ID " _
                                                                                              & " from  SISCOM_MI.LOTTI " _
                                                                                              & " where ID_FILIALE=" & cmbFiliale.SelectedValue.ToString _
                                                                                              & "   and ID_ESERCIZIO_FINANZIARIO=" & cmbEsercizio.SelectedValue.ToString
                If Strings.Len(Me.txtID_LOTTI.Value) > 0 Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID not in (" & Me.txtID_LOTTI.Value & ") "
                End If

                par.cmd.CommandText = par.cmd.CommandText & ")) order by SISCOM_MI.TAB_SERVIZI_VOCI.DESCRIZIONE ASC"


                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    i = i + 1
                    cmbDettaglioServizio.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While

                myReader1.Close()


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                If i = 1 Then
                    cmbDettaglioServizio.Items(1).Selected = True
                End If

            End If



        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Try


            If Me.cmbEsercizio.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare l\'esercizio finanziario!');</script>")
                Exit Sub
            End If

            If Me.cmbFiliale.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare la Struttura!');</script>")
                Exit Sub
            End If

            If Me.cmbTipoServizio.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare il Tipo Servizio!');</script>")
                Exit Sub
            End If

            If Me.cmbDettaglioServizio.SelectedValue = -1 Then
                Response.Write("<script>alert('Selezionare il Dettaglio del Servizio!');</script>")
                Exit Sub
            End If
            
            Response.Write("<script>location.replace('RisultatiLotti.aspx?FI=" & par.PulisciStrSql(cmbFiliale.SelectedValue) _
                                                                      & "&EF=" & cmbEsercizio.SelectedValue _
                                                                      & "&SE=" & par.PulisciStrSql(cmbTipoServizio.SelectedValue) _
                                                                      & "&DT=" & par.PulisciStrSql(cmbDettaglioServizio.SelectedValue) _
                                                                      & "&TIPO=SCAMBIO_LOTTI" _
                                                                      & "');</script>")

            

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
    End Sub

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function


    Protected Sub cmbFiliale_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFiliale.SelectedIndexChanged
        FiltraServizio()
        FiltraDettaglioServizio()
    End Sub

    Protected Sub cmbEsercizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEsercizio.SelectedIndexChanged
        FiltraFiliale()
        FiltraServizio()
        FiltraDettaglioServizio()
    End Sub

    Protected Sub cmbTipoServizio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoServizio.SelectedIndexChanged
        FiltraDettaglioServizio()
    End Sub
End Class
