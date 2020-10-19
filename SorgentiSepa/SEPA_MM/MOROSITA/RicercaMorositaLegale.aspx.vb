'*** RICERCA Affidamento della pratica al legale

Partial Class MOROSITA_RicercaMorositaLegale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sFiliale As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            Exit Sub
        End If

        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then

            Response.Flush()

            'If Session.Item("LIVELLO") <> "1" Then
            '    sFiliale = Session.Item("ID_STRUTTURA")
            'End If


            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            Me.cmbCivico.Items.Clear()
            Me.cmbCivico.Items.Add(New ListItem(" ", -1))

            Me.cmbTribunali.Items.Clear()
            Me.cmbTribunali.Items.Add(New ListItem(" ", -1))


            CaricaStrutture()

            CaricaComplessi()
            CaricaEdifici()
            CaricaIndirizzi()

            CaricaTipologiaUI()

            CaricaTribunali()


            Me.txtImporto1.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            Me.txtImporto2.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")


            'Dim ScadenzaBollettino As String
            'Dim s1 As String
            ''s1 = Format(Now, "yyyyMMddHHmmss")
            'Dim d1 As Date
            'd1 = New Date(2011, 9, 24)
            's1 = d1.DayOfWeek
            ''ScadenzaBollettino = par.AggiustaData(DateAdd("d", 40, s1))

            '' APRO CONNESSIONE
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            'End If

            'par.cmd.CommandText = "select MOROSITA.TIPO_INVIO,MOROSITA.DATA_PROTOCOLLO,MOROSITA.PROTOCOLLO_ALER," _
            '                          & " MOROSITA_LETTERE.ID as ID_MOROSITA_LETTERE,MOROSITA_LETTERE.COD_CONTRATTO,MOROSITA_LETTERE.Importo, MOROSITA_LETTERE.ID_ANAGRAFICA, MOROSITA_LETTERE.EMISSIONE, MOROSITA_LETTERE.INIZIO_PERIODO, MOROSITA_LETTERE.FINE_PERIODO, MOROSITA_LETTERE.NUM_LETTERE," _
            '                          & " ANAGRAFICA.COGNOME,ANAGRAFICA.NOME,ANAGRAFICA.RAGIONE_SOCIALE,ANAGRAFICA.COD_FISCALE,ANAGRAFICA.PARTITA_IVA " _
            '                             & " from  SISCOM_MI.MOROSITA,SISCOM_MI.ANAGRAFICA,SISCOM_MI.MOROSITA_LETTERE " _
            '                             & " where MOROSITA.ID in (10,11,14,22,30) " _
            '                             & "   and MOROSITA_LETTERE.ID_ANAGRAFICA=ANAGRAFICA.ID " _
            '                             & "   and MOROSITA.ID                   =MOROSITA_LETTERE.ID_MOROSITA " _
            '                             & " order by MOROSITA_LETTERE.ID_ANAGRAFICA,MOROSITA_LETTERE.ID_CONTRATTO,MOROSITA_LETTERE.NUM_LETTERE "

            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Do While myReader.Read
            '    ScadenzaBollettino = par.AggiustaData(DateAdd("d", 80, CDate(par.FormattaData(par.IfNull(myReader("emissione"), "")))))

            '    d1 = New Date(Mid(ScadenzaBollettino, 1, 4), Mid(ScadenzaBollettino, 5, 2), Mid(ScadenzaBollettino, 7, 2))
            '    s1 = d1.DayOfWeek
            'Loop

            'myReader.Close()

        End If

    End Sub



    'CARICO COMBO STRUTTURE (FILIARI)
    Private Sub CaricaStrutture()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.CheckStrutture.Items.Clear()
            Me.txtID_STRUTTURE.Value = ""
            Me.txtID_STRUTTURE_SEL.Value = ""


            par.cmd.CommandText = "select ID,trim(NOME) as NOME from SISCOM_MI.TAB_FILIALI " _
                               & " where ID in (select distinct(ID_FILIALE) from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                            & " where ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                         & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                      & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                                  & " where ID_CONTRATTO in (select distinct(ID_CONTRATTO) from SISCOM_MI.MOROSITA_LETTERE) " _
                               & " )))) " _
                               & " order by NOME asc"




            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.CheckStrutture.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
                If Me.txtID_STRUTTURE.Value = "" Then
                    Me.txtID_STRUTTURE.Value = par.IfNull(myReader1("ID"), -1)
                    Me.txtID_STRUTTURE_SEL.Value = par.IfNull(myReader1("ID"), -1)
                Else
                    Me.txtID_STRUTTURE.Value = Me.txtID_STRUTTURE.Value & "," & par.IfNull(myReader1("ID"), -1)
                    Me.txtID_STRUTTURE_SEL.Value = Me.txtID_STRUTTURE_SEL.Value & "," & par.IfNull(myReader1("ID"), -1)
                End If
            End While
            myReader1.Close()
            '**************************

            Dim i As Integer
            For i = 0 To Me.CheckStrutture.Items.Count - 1
                Me.CheckStrutture.Items(i).Selected = True
            Next

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO COMPLESSI
    Private Sub CaricaComplessi()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                               & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                      & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                   & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                                & " where ID_CONTRATTO in (select distinct(ID_CONTRATTO) from SISCOM_MI.MOROSITA_LETTERE) " _
                               & " ))) " _
                               & " order by DENOMINAZIONE asc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                Me.cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO EDIFICI
    Private Sub CaricaEdifici()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            '**** CARICO L'ELENCO EDIFICI (FABBRICATI)
            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select distinct ID,(trim(DENOMINAZIONE)||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE  from SISCOM_MI.EDIFICI " _
                                & " where ID<>1 and ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                       & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                    & " where ID_CONTRATTO in (select distinct(ID_CONTRATTO) from SISCOM_MI.MOROSITA_LETTERE) " _
                               & " )) " _
                               & " order by DENOMINAZIONE asc"




            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try
    End Sub

    'CARICO COMBO EDIFICI
    Private Sub CaricaIndirizzi()
        Dim i As Integer
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = " select distinct(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                & " where ID in (select distinct(ID_INDIRIZZO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                          & " where ID_CONTRATTO in (select distinct(ID_CONTRATTO) from SISCOM_MI.MOROSITA_LETTERE ) ) ) " _
                                & " order by DESCRIZIONE asc"



            i = 0
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), i))
                i = i + 1
            End While
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub

    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaTipologiaUI()

        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbTipologiaUI.Items.Clear()
            'cmbTipologiaUI.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select trim(DESCRIZIONE) as DESCRIZIONE,COD from SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI " _
                               & " order by DESCRIZIONE asc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbTipologiaUI.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            Me.cmbTipologiaUI.Items.Add("TUTTI")
            Me.cmbTipologiaUI.Items.FindByText("TUTTI").Selected = True

            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    'CARICO COMBO TRIBUNALI
    Private Sub CaricaTribunali()
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            Me.cmbTribunali.Items.Clear()
            Me.cmbTribunali.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select TAB_TRIBUNALI_COMPETENTI.ID, T_COMUNI.NOME as COMUNE,TAB_TRIBUNALI_COMPETENTI.COMPETENZA  " _
                               & " from SISCOM_MI.TAB_TRIBUNALI_COMPETENTI,SEPA.T_COMUNI " _
                               & " where TAB_TRIBUNALI_COMPETENTI.COD_COMUNE=T_COMUNI.COD " _
                               & "   and TAB_TRIBUNALI_COMPETENTI.ID in (select distinct ID_TRIBUNALI_COMPETENTI from SISCOM_MI.MOROSITA_LEGALI) " _
                               & " order by COMPETENZA,COMUNE ASC"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbTribunali.Items.Add(New ListItem(par.IfNull(myReader1("COMPETENZA"), "") & " - " & par.IfNull(myReader1("COMUNE"), ""), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean
        Dim sTipoImm As String

        Try

            ControlloCampi = True

            If Strings.Len(Strings.Trim(Me.txtID_STRUTTURE_SEL.Value)) = 0 Then
                Response.Write("<script>alert('Attenzione...Selezionare una o più Strutture!');</script>")
                ControlloCampi = False
                Exit Sub
            End If


            If Me.cmbTipologiaUI.Items.FindByText("TUTTI").Selected = True Then
                sTipoImm = ""
            Else
                sTipoImm = Me.cmbTipologiaUI.SelectedItem.Value
            End If

            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiMorositaLegale.aspx?FI=" & par.VaroleDaPassare(Me.txtID_STRUTTURE_SEL.Value) _
                                                                                 & "&AREAC=" & par.VaroleDaPassare(Me.txtID_AREE_SEL.Value) _
                                                                                 & "&CO=" & Me.cmbComplesso.SelectedValue.ToString _
                                                                                 & "&ED=" & Me.cmbEdificio.SelectedValue.ToString _
                                                                                 & "&IN=" & par.VaroleDaPassare(Me.cmbIndirizzo.SelectedItem.Text) _
                                                                                 & "&CI=" & par.VaroleDaPassare(Me.cmbCivico.SelectedItem.Text) _
                                                                                 & "&CG=" & par.VaroleDaPassare(Me.txtCognome.Text) _
                                                                                 & "&NM=" & par.VaroleDaPassare(Me.txtNome.Text) _
                                                                                 & "&CD=" & par.VaroleDaPassare(Me.txtCodice.Text) _
                                                                                 & "&TI=" & sTipoImm _
                                                                                 & "&IMP1=" & par.VirgoleInPunti(Me.txtImporto1.Text.Replace(".", "")) _
                                                                                 & "&IMP2=" & par.VirgoleInPunti(Me.txtImporto2.Text.Replace(".", "")) _
                                                                                 & "&PRA_DA=" & Me.txtNumPraticheDA.Text _
                                                                                 & "&PRA_A=" & Me.txtNumPraticheA.Text _
                                                                                 & "&TR=" & par.VaroleDaPassare(Me.cmbTribunali.SelectedValue) _
                                                                                & "&ORD=" & "" _
                                                                             & "');</script>")
            End If


        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
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


    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        FiltraEdifici()
        FiltraIndirizzi()
        FiltraCivici()
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        FiltraIndirizzi()
        FiltraCivici()
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        FiltraCivici()
    End Sub


    Private Sub FiltraComplessi()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            If Me.txtID_STRUTTURE_SEL.Value <> "" Then

                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID<>1 and ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                                                & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                             & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                         & " where ID_CONTRATTO in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
                                   & " ))) " _
                                   & "   and ID_FILIALE in (" & Me.txtID_STRUTTURE_SEL.Value & ") " _
                                   & " order by DENOMINAZIONE asc"



            Else

                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID<>1 and ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                                                & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                             & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                         & " where ID_CONTRATTO in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
                                   & " ))) " _
                                   & " order by DENOMINAZIONE asc"

            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                Me.cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Private Sub FiltraEdifici()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

           
            par.cmd.CommandText = " select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE  " _
                                & " from SISCOM_MI.EDIFICI " _
                                & " where ID<>1 " _
                                & "   and ID in (select distinct(ID_EDIFICIO) " _
                                             & " from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select distinct(ID_UNITA) " _
                                                         & " from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                         & " where ID_CONTRATTO in (select distinct(ID_CONTRATTO) from SISCOM_MI.MOROSITA_LETTERE) ) ) "

            If Me.txtID_STRUTTURE_SEL.Value <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & "  and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 and ID_FILIALE in (" & Me.txtID_STRUTTURE_SEL.Value & ") )"
            End If

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & "  and ID_COMPLESSO= " & Me.cmbComplesso.SelectedValue
            End If

            par.cmd.CommandText = par.cmd.CommandText & "  order by DENOMINAZIONE asc"
           

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                Me.cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Private Sub FiltraIndirizzi()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = " select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE " _
                                & " from SISCOM_MI.INDIRIZZI " _
                                & " where ID in (select distinct(ID_INDIRIZZO) " _
                                             & " from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select distinct(ID_UNITA) " _
                                                          & " from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                          & " where ID_CONTRATTO in (select distinct(ID_CONTRATTO) from SISCOM_MI.MOROSITA_LETTERE ) ) "

            If Me.cmbEdificio.SelectedValue <> "-1" Then
                par.cmd.CommandText = par.cmd.CommandText & " and ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue
            ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then

                par.cmd.CommandText = par.cmd.CommandText & " and ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                              & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue & ")"
            End If

            par.cmd.CommandText = par.cmd.CommandText & " ) order by DESCRIZIONE asc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), i))
                i = i + 1
            End While
            myReader1.Close()

            Me.cmbIndirizzo.SelectedValue = "-1"
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Private Sub FiltraCivici()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            '**** CARICO I CIVICI PER L'INDIRIZZO SELEZIONATO
            Me.cmbCivico.Items.Clear()
            Me.cmbCivico.Items.Add(New ListItem(" ", -1))


            If Me.cmbIndirizzo.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select DISTINCT CIVICO from SISCOM_MI.INDIRIZZI " _
                                    & " where DESCRIZIONE='" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "'" _
                                    & " order by CIVICO asc"



                i = 0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("CIVICO"), " "), i))
                    i = i + 1
                End While
                myReader1.Close()

                Me.cmbCivico.SelectedValue = "-1"
                '**************************

            End If

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                FlagConnessione = False
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub


    Protected Sub btnDeselTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselTutti.Click
        Dim i As Integer


        Me.txtID_STRUTTURE_SEL.Value = ""


        For i = 0 To Me.CheckStrutture.Items.Count - 1
            Me.CheckStrutture.Items(i).Selected = False
        Next

        Me.cmbComplesso.Items.Clear()
        Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

        Me.cmbEdificio.Items.Clear()
        Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

        Me.cmbIndirizzo.Items.Clear()
        Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

        Me.cmbCivico.Items.Clear()
        Me.cmbCivico.Items.Add(New ListItem(" ", -1))

    End Sub

    Protected Sub btnSelTutti_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelTutti.Click
        Dim i As Integer


        Me.txtID_STRUTTURE_SEL.Value = ""


        For i = 0 To Me.CheckStrutture.Items.Count - 1
            Me.CheckStrutture.Items(i).Selected = True


            If Me.txtID_STRUTTURE_SEL.Value = "" Then
                Me.txtID_STRUTTURE_SEL.Value = Me.CheckStrutture.Items(i).Value
            Else
                Me.txtID_STRUTTURE_SEL.Value = Me.txtID_STRUTTURE_SEL.Value & "," & Me.CheckStrutture.Items(i).Value
            End If
        Next

        FiltraComplessi()
        FiltraEdifici()
        FiltraIndirizzi()
        FiltraCivici()

    End Sub

    Protected Sub btnDeselTuttiAREA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselTuttiAREA.Click
        Dim i As Integer

        Me.txtID_AREE_SEL.Value = ""

        For i = 0 To Me.CheckAreaCanone.Items.Count - 1
            Me.CheckAreaCanone.Items(i).Selected = False
        Next
    End Sub

    Protected Sub btnSelTuttiAREA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSelTuttiAREA.Click
        Dim i As Integer

        Me.txtID_AREE_SEL.Value = ""

        For i = 0 To Me.CheckAreaCanone.Items.Count - 1
            Me.CheckAreaCanone.Items(i).Selected = True


            If Me.txtID_AREE_SEL.Value = "" Then
                Me.txtID_AREE_SEL.Value = Me.CheckAreaCanone.Items(i).Value
            Else
                Me.txtID_AREE_SEL.Value = Me.txtID_AREE_SEL.Value & "," & Me.CheckAreaCanone.Items(i).Value
            End If
        Next
    End Sub


    Protected Sub CheckStrutture_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckStrutture.SelectedIndexChanged
        Dim i As Integer


        Me.txtID_STRUTTURE_SEL.Value = ""


        For i = 0 To Me.CheckStrutture.Items.Count - 1
            If Me.CheckStrutture.Items(i).Selected = True Then


                If Me.txtID_STRUTTURE_SEL.Value = "" Then
                    Me.txtID_STRUTTURE_SEL.Value = Me.CheckStrutture.Items(i).Value
                Else
                    Me.txtID_STRUTTURE_SEL.Value = Me.txtID_STRUTTURE_SEL.Value & "," & Me.CheckStrutture.Items(i).Value
                End If
            End If
        Next

        FiltraComplessi()
        FiltraEdifici()
        FiltraIndirizzi()
        FiltraCivici()

    End Sub
End Class
