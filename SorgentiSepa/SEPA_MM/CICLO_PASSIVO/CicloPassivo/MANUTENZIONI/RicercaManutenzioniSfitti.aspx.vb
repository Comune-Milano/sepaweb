'*** RICERCA MANUTENZIONI x gli ALLOGGI SFITTI

Partial Class MANUTENZIONI_RicercaManutenzioniSfitti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String = "-1"
    Public sBP_Generale As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            vId = 0
            '**TipoImmobile = Session.Item("BUILDING_TYPE")
            vId = Session.Item("ID")


            If Session.Item("LIVELLO") <> "1" Then
                If (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                    sFiliale = Session.Item("ID_STRUTTURA")
                Else
                    sBP_Generale = Session.Item("ID_STRUTTURA")
                End If
            End If

            CaricaStrutture()

            CaricaStati()

        End If

    End Sub

    Private Property vId() As Long
        Get
            If Not (ViewState("par_idEdificio") Is Nothing) Then
                Return CLng(ViewState("par_idEdificio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idEdificio") = value
        End Set

    End Property


    'CARICO COMBO STRUTTURE (FILIARI)
    Private Sub CaricaStrutture()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbStruttura.Items.Clear()
            Me.cmbStruttura.Items.Add(New ListItem(" ", -1))


            If sFiliale <> "-1" Then
                par.cmd.CommandText = "select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            Else
                par.cmd.CommandText = " select distinct ID,NOME from SISCOM_MI.TAB_FILIALI " _
                                   & " where  ID in (select ID_STRUTTURA from SISCOM_MI.MANUTENZIONI,SISCOM_MI.MANUTENZIONI_interventi  " _
                                                 & " where manutenzioni_interventi.ID_UNITA_IMMOBILIARe is not null " _
                                                 & " and manutenzioni.id=manutenzioni_interventi.id_manutenzione " _
                                                 & "   and ID_PF_VOCE is null ) " _
                                   & " order by NOME asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))

                If sFiliale <> "-1" Then
                    Me.cmbStruttura.SelectedValue = par.IfNull(myReader1("ID"), -1)
                    Me.cmbStruttura.Enabled = False
                End If

            End While
            myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


            If sFiliale <> "-1" Then
                Me.cmbStruttura.SelectedValue = sFiliale
                Me.cmbStruttura.Enabled = False

                CaricaEsercizio()

            Else
                If sBP_Generale <> "" Then
                    Me.cmbStruttura.SelectedValue = sBP_Generale
                    CaricaEsercizio()

                Else
                    Me.cmbStruttura.SelectedValue = "-1"
                    CaricaEsercizio()
                End If
            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub




    'CARICO COMBO EDIFICI
    Private Sub FiltraEdifici()

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            '**** CARICO L'ELENCO EDIFICI (FABBRICATI)
            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            'par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(SISCOM_MI.EDIFICI.ID,1,1)= " & gest & " order by SISCOM_MI.EDIFICI.DENOMINAZIONE asc"

            par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                               & " from SISCOM_MI.EDIFICI " _
                               & " where ID in (select ID_EDIFICIO from SISCOM_MI.MANUTENZIONI " _
                                                 & " where ID_UNITA_IMMOBILIARI is not null " _
                                                 & "   and ID_PF_VOCE is null  " _
                                                 & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") "


            If sFiliale <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale & ")"
            Else
                par.cmd.CommandText = par.cmd.CommandText & ")"
            End If

            par.cmd.CommandText = par.cmd.CommandText & " order by DENOMINAZIONE asc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()

            Me.cmbEdificio.SelectedValue = "-1"
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

   
    'CARICO COMBO STATI
    Private Sub CaricaStati()

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbStato.Items.Clear()
            Me.cmbStato.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select * from SISCOM_MI.TAB_STATI_ODL where ID<6 order by ID"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmbStato.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbStato.SelectedValue = "-1"


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try



    End Sub


    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged

        FiltraUnita()

    End Sub

    Protected Sub cmbUnita_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnita.SelectedIndexChanged

        FiltraFornitori()
        FiltraAppalti()

    End Sub


    Protected Sub cmbFornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFornitore.SelectedIndexChanged

        FiltraAppalti()

    End Sub



    Private Sub FiltraUnita()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.cmbUnita.Items.Clear()
            cmbUnita.Items.Add(New ListItem(" ", -1))


            If Me.cmbEdificio.SelectedValue <> "-1" Then


                par.cmd.CommandText = "select SISCOM_MI.UNITA_IMMOBILIARI.ID as ID_UNITA_IMMOBILIARI," _
                                          & "SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO" _
                                                                         & "||' - -Scala '||SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE" _
                                                                         & "||' - -Piano '||SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE" _
                                                                         & "||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO as DENOMINAZIONE_UNITA" _
                                    & " from  SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI " _
                                     & " where  SISCOM_MI.UNITA_IMMOBILIARI.ID in (select ID_UNITA_IMMOBILIARI from SISCOM_MI.MANUTENZIONI " _
                                                                                 & " where ID_UNITA_IMMOBILIARI is not null " _
                                                                                 & "   and ID_PF_VOCE is null  " _
                                                                                 & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") )" _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID  " _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD  " _
                                     & " order by DENOMINAZIONE_UNITA asc"



                'par.cmd.CommandText = "select ID,COD_UNITA_IMMOBILIARE from SISCOM_MI.UNITA_IMMOBILIARI " _
                '                   & " where ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue _
                '                   & "   and ID in (select ID_UNITA_IMMOBILIARI from SISCOM_MI.MANUTENZIONI)" _
                '                   & " order by COD_UNITA_IMMOBILIARE asc"


            Else

                par.cmd.CommandText = "select SISCOM_MI.UNITA_IMMOBILIARI.ID as ID_UNITA_IMMOBILIARI," _
                                          & "SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO" _
                                                                         & "||' - -Scala '||SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE" _
                                                                         & "||' - -Piano '||SISCOM_MI.TIPO_LIVELLO_PIANO.DESCRIZIONE" _
                                                                         & "||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO as DENOMINAZIONE_UNITA" _
                                    & " from  SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.TIPO_LIVELLO_PIANO,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI " _
                                     & " where  SISCOM_MI.UNITA_IMMOBILIARI.ID in (select ID_UNITA_IMMOBILIARI from SISCOM_MI.MANUTENZIONI " _
                                                 & " where ID_UNITA_IMMOBILIARI is not null " _
                                                 & "   and ID_PF_VOCE is null  " _
                                                 & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") )" _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+)  " _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA=SCALE_EDIFICI.ID  " _
                                     & "   and  SISCOM_MI.UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO=SISCOM_MI.TIPO_LIVELLO_PIANO.COD  " _
                                     & " order by DENOMINAZIONE_UNITA asc"

            End If


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                Me.cmbUnita.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE_UNITA"), " "), par.IfNull(myReader1("ID_UNITA_IMMOBILIARI"), -1)))
            End While
            myReader1.Close()


            myReader1.Close()
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If i = 1 Then
                Me.cmbUnita.Items(1).Selected = True
            End If

            FiltraFornitori()


        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraAppalti()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.cmbAppalto.Items.Clear()
            cmbAppalto.Items.Add(New ListItem(" ", -1))

            If Me.cmbFornitore.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                                    & " from  SISCOM_MI.APPALTI " _
                                    & " where ID_FORNITORE=" & Me.cmbFornitore.SelectedValue

                If Me.cmbUnita.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & "  and ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_UNITA_IMMOBILIARI=" & Me.cmbUnita.SelectedValue & ")" _
                                    & " order by SISCOM_MI.APPALTI.NUM_REPERTORIO asc"
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "  and ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                          & " where ID_UNITA_IMMOBILIARI is not null " _
                                                                          & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                                          & "   and ID_PF_VOCE is null) " _
                                    & " order by SISCOM_MI.APPALTI.NUM_REPERTORIO asc"
                End If

            Else

                par.cmd.CommandText = " select SISCOM_MI.APPALTI.ID,SISCOM_MI.APPALTI.NUM_REPERTORIO " _
                                    & " from  SISCOM_MI.APPALTI where "

                If Me.cmbUnita.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & "  ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI where ID_UNITA_IMMOBILIARI=" & Me.cmbUnita.SelectedValue & ")" _
                                    & " order by SISCOM_MI.APPALTI.NUM_REPERTORIO asc"
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "  ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                      & " where ID_UNITA_IMMOBILIARI is not null " _
                                                                      & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                                      & "   and ID_PF_VOCE is null) " _
                                    & " order by SISCOM_MI.APPALTI.NUM_REPERTORIO asc"
                End If

            End If


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                Me.cmbAppalto.Items.Add(New ListItem(par.IfNull(myReader1("NUM_REPERTORIO"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If i = 1 Then
                Me.cmbAppalto.Items(1).Selected = True
            End If



        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub FiltraFornitori()
        Dim sWhere As String = ""
        Dim i As Integer = 0

        Try

            If par.OracleConn.State = Data.ConnectionState.Open Then
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Me.cmbFornitore.Items.Clear()
            Me.cmbFornitore.Items.Add(New ListItem(" ", -1))

            If Me.cmbUnita.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME, trim(COD_FORNITORE) as COD_FORNITORE from SISCOM_MI.FORNITORI " _
                                    & " where FL_BLOCCATO=0 " _
                                    & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                    & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                                & "  where  ID_UNITA_IMMOBILIARI=" & Me.cmbUnita.SelectedValue & ")) " _
                                    & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            Else
                par.cmd.CommandText = " select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME, trim(COD_FORNITORE) as COD_FORNITORE from SISCOM_MI.FORNITORI " _
                                    & " where FL_BLOCCATO=0 " _
                                    & "  and SISCOM_MI.FORNITORI.ID in (select ID_FORNITORE from SISCOM_MI.APPALTI " _
                                                                    & " where ID in (select ID_APPALTO from SISCOM_MI.MANUTENZIONI " _
                                                                                 & " where ID_UNITA_IMMOBILIARI is not null " _
                                                                                 & "   and ID_PIANO_FINANZIARIO=(select ID from SISCOM_MI.PF_MAIN where ID_ESERCIZIO_FINANZIARIO=" & Me.cmbEsercizio.SelectedValue & ") " _
                                                                                 & "   and ID_PF_VOCE is null "

                If sFiliale <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & " and ID_STRUTTURA=" & sFiliale & "))"
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "))"
                End If

                par.cmd.CommandText = par.cmd.CommandText & " order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                i = i + 1
                If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
                    Else
                        Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
                    End If
                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
                    Else
                        Me.cmbFornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
                    End If
                End If

            End While
            myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            If i = 1 Then
                Me.cmbFornitore.Items(1).Selected = True
            End If

            FiltraAppalti()

        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        
        Try


            Response.Write("<script>location.replace('RisultatiManutenzioni.aspx?FI=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                             & "&UI=" & Me.cmbUnita.SelectedValue.ToString _
                                                                             & "&ED=" & Me.cmbEdificio.SelectedValue.ToString _
                                                                             & "&FO=" & Me.cmbFornitore.SelectedValue.ToString _
                                                                             & "&AP=" & Me.cmbAppalto.SelectedValue.ToString _
                                                                             & "&ST=" & Me.cmbStato.SelectedValue.ToString _
                                                                             & "&EF_R=" & Me.cmbEsercizio.SelectedValue.ToString _
                                                                             & "&PROVENIENZA=RICERCA_SFITTI" & "');</script>")



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


    Protected Overrides Sub Finalize()
        MyBase.Finalize()
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

            If Me.cmbStruttura.SelectedValue <> "-1" Then

                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE, SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_UNITA_IMMOBILIARI is not null " _
                                                                   & "   and ID_PF_VOCE is null " _
                                                                   & "   and ID_STRUTTURA=" & Me.cmbStruttura.SelectedValue & ") " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID "


            Else
                par.cmd.CommandText = "select SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as FINE, SISCOM_MI.PF_STATI.DESCRIZIONE as STATO " _
                                   & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,SISCOM_MI.PF_STATI " _
                                   & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                                   & "   and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO " _
                                   & "   and  SISCOM_MI.PF_MAIN.ID in (select distinct(ID_PIANO_FINANZIARIO) " _
                                                                   & " from SISCOM_MI.MANUTENZIONI " _
                                                                   & " where ID_UNITA_IMMOBILIARI is not null " _
                                                                   & "   and ID_PF_VOCE is null) " _
                                   & "   and SISCOM_MI.PF_MAIN.ID_STATO=SISCOM_MI.PF_STATI.ID "


            End If



            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1

                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If

                Me.cmbEsercizio.Items.Add(New ListItem(par.IfNull(myReader1("INIZIO") & "-" & myReader1("FINE") & " (" & myReader1("STATO") & ")", " "), par.IfNull(myReader1("ID"), -1)))

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
                Me.cmbEsercizio.Items(0).Selected = True
                Me.cmbEsercizio.Enabled = False
            ElseIf i = 0 Then
                Me.cmbEsercizio.Items.Clear()
                Me.cmbEsercizio.Items.Add(New ListItem(" ", -1))
                Me.cmbEsercizio.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbEsercizio.SelectedValue = ID_ANNO_EF_CORRENTE
                End If

                FiltraEdifici()
                FiltraUnita()
                FiltraFornitori()
                FiltraAppalti()
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

        FiltraEdifici()
        FiltraUnita()
        FiltraFornitori()
        FiltraAppalti()

    End Sub

End Class
