'*** RICERCA MOROSITA'

Partial Class MOROSITA_RicercaMorosita
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
            'vId = 0
            '**TipoImmobile = Session.Item("BUILDING_TYPE")
            'vId = Session.Item("ID")

            Response.Flush()

            'If Session.Item("LIVELLO") <> "1" Then
            '    sFiliale = Session.Item("ID_STRUTTURA")
            'End If

            SettaggioCampi()

            'Me.cmbComplesso.Items.Clear()
            'Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            'Me.cmbEdificio.Items.Clear()
            'Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            'Me.cmbIndirizzo.Items.Clear()
            'Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            Me.cmbCivico.Items.Clear()
            Me.cmbCivico.Items.Add(New ListItem(" ", -1))

            CaricaStrutture()

            CaricaComplessi()
            CaricaEdifici()
            CaricaIndirizzi()

            

            CaricaTipologiaUI()


            Me.txtDataDAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            Me.txtDataDAL_P.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataAL_P.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


        End If

    End Sub



    'CARICO COMBO STRUTTURE (FILIARI)
    Private Sub CaricaStrutture()
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

            Me.cmbStruttura.Items.Clear()
            Me.cmbStruttura.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select ID,trim(NOME) as NOME from SISCOM_MI.TAB_FILIALI " _
                               & " where ID in (select ID_FILIALE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                            & " where ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                                                         & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                      & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                                  & " where ID_CONTRATTO in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
                               & " )))) " _
                               & " order by NOME asc"




            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
            myReader1.Close()
            '**************************

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            'Me.cmbStruttura.SelectedValue = "-1"



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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If

            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                               & " where ID<>1 and ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                                            & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                         & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                     & " where ID_CONTRATTO in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
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
            End If

            'Me.cmbComplesso.SelectedValue = "-1"


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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO L'ELENCO EDIFICI (FABBRICATI)
            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select distinct ID,(trim(DENOMINAZIONE)||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE  from SISCOM_MI.EDIFICI " _
                                & " where ID<>1 and ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                         & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _
                                                                                & "  where ID in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
                   & " ))) " _
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

            'Me.cmbEdificio.SelectedValue = "-1"

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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))


            par.cmd.CommandText = "select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                & " where ID in (select ID_INDIRIZZO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                         & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _
                                                                                & "  where ID in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE ) " _
                   & " ))) " _
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
            End If

            'If i = 1 Then
            '    Me.cmbIndirizzo.Items(1).Selected = True
            'Else
            '    Me.cmbIndirizzo.SelectedValue = "-1"
            'End If


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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
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
    Private Sub SettaggioCampi()
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

            'NOTA GIUSEPPE: selezionare solo le date per ID_MOROSITA not null 
            par.cmd.CommandText = "select MIN(RIF_DA) from SISCOM_MI.MOROSITA "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtDataDAL.Text = par.FormattaData(par.IfNull(myReader1(0), ""))
                Me.txtDataAL.Text = par.FormattaData(Format(Now, "yyyyMMdd"))
            End If
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


    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean
        Dim sTipoImm As String
        Dim sTipoRicerca As String = ""

        Try

            ControlloCampi = True

            If Strings.Len(Strings.Trim(Me.txtDataDAL.Text)) = 0 Then
                Response.Write("<script>alert('Attenzione...Inserire la data di inizio consistenza debito!');</script>")
                ControlloCampi = False
                Exit Sub
            End If


            If Strings.Len(Strings.Trim(Me.txtDataAL.Text)) = 0 Then
                Response.Write("<script>alert('Attenzione...Inserire la data di fine consistenza debito!');</script>")
                ControlloCampi = False
                Exit Sub
            End If

            If Strings.Len(Me.txtDataAL.Text) > 0 Then
                If par.AggiustaData(Me.txtDataAL.Text) < par.AggiustaData(Me.txtDataDAL.Text) Then

                    Response.Write("<script>alert('Attenzione...Controllare il range delle date della consistenza debito!');</script>")
                    ControlloCampi = False
                    Exit Sub
                End If
            End If


            If Strings.Len(Me.txtDataAL_P.Text) > 0 Then
                If par.AggiustaData(Me.txtDataAL_P.Text) < par.AggiustaData(Me.txtDataDAL_p.Text) Then

                    Response.Write("<script>alert('Attenzione...Controllare il range delle date del protocollo!');</script>")
                    ControlloCampi = False
                    Exit Sub
                End If
            End If

            If Me.cmbTipologiaUI.Items.FindByText("TUTTI").Selected = True Then
                sTipoImm = ""
            Else
                sTipoImm = Me.cmbTipologiaUI.SelectedItem.Value
            End If


            For i = 0 To CheckBoxMora.Items.Count - 1
                If CheckBoxMora.Items(i).Selected = True Then
                    sTipoRicerca = sTipoRicerca & "1"
                Else
                    sTipoRicerca = sTipoRicerca & "0"
                End If
            Next i

            If ControlloCampi = True Then
                Response.Write("<script>location.replace('RisultatiMorosita.aspx?FI=" & Me.cmbStruttura.SelectedValue.ToString _
                                                                             & "&CO=" & Me.cmbComplesso.SelectedValue.ToString _
                                                                             & "&ED=" & Me.cmbEdificio.SelectedValue.ToString _
                                                                             & "&TI=" & sTipoImm _
                                                                             & "&IN=" & par.VaroleDaPassare(Me.cmbIndirizzo.SelectedItem.Text) _
                                                                             & "&CI=" & par.VaroleDaPassare(Me.cmbCivico.SelectedItem.Text) _
                                                                            & "&DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataDAL.Text), "") _
                                                                             & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataAL.Text), "") _
                                                                             & "&CD=" & par.VaroleDaPassare(Me.txtCodice.Text) _
                                                                             & "&CG=" & par.VaroleDaPassare(Me.txtCognome.Text) _
                                                                             & "&NM=" & par.VaroleDaPassare(Me.txtNome.Text) _
                                                                             & "&DAL_P=" & par.IfEmpty(par.AggiustaData(Me.txtDataDAL_P.Text), "") _
                                                                             & "&AL_P=" & par.IfEmpty(par.AggiustaData(Me.txtDataAL_P.Text), "") _
                                                                             & "&PRO=" & par.VaroleDaPassare(Me.txtProtocollo.Text) _
                                                                             & "&MORA=" & sTipoRicerca _
                                                                             & "&ORD=" & Me.RBList1.Text _
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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            If Me.cmbStruttura.SelectedValue <> "-1" Then

                par.cmd.CommandText = "select distinct ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID<>1 and ID in (select ID_COMPLESSO from SISCOM_MI.EDIFICI " _
                                                & " where ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                             & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                                         & " where ID_CONTRATTO in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
                                   & " ))) " _
                                   & "   and ID_FILIALE=" & Me.cmbStruttura.SelectedValue _
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
            End If

            'If i = 1 Then
            '    Me.cmbComplesso.Items(1).Selected = True
            'Else
            '    Me.cmbComplesso.SelectedValue = "-1"
            'End If


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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID<>1 and ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue _
                                   & " order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE  from SISCOM_MI.EDIFICI " _
                                    & " where ID<>1 and ID in (select ID_EDIFICIO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                 & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                             & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _
                                                                                    & "  where ID in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE) " _
                       & " ))) "

                If Me.cmbStruttura.SelectedValue <> "-1" Then
                    par.cmd.CommandText = par.cmd.CommandText & "  and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 and ID_FILIALE=" & Me.cmbStruttura.SelectedValue & ")"
                End If
                par.cmd.CommandText = par.cmd.CommandText & " order by DENOMINAZIONE asc"
            End If


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
            End If

            'If i = 1 Then
            '    Me.cmbEdificio.Items(1).Selected = True
            'Else
            '    Me.cmbEdificio.SelectedValue = "-1"
            'End If



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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If



            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))


            If Me.cmbEdificio.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select  DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                    & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI where ID=" & Me.cmbEdificio.SelectedValue & " )" _
                                    & " order by DESCRIZIONE asc"

            ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then

                par.cmd.CommandText = " select  DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                    & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI" _
                                                 & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & Me.cmbComplesso.SelectedValue & " ))" _
                                    & " order by DESCRIZIONE asc"

            ElseIf Me.cmbStruttura.SelectedValue <> "-1" Then

                par.cmd.CommandText = " select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                    & " where ID in (select ID_INDIRIZZO from SISCOM_MI.UNITA_IMMOBILIARI  " _
                                                 & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE  " _
                                                              & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA  " _
                                                                                     & " where ID in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE ) ) ) " _
                                                 & "   and ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI " _
                                                                       & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI  " _
                                                                                              & " where ID <> 1  " _
                                                                                              & "   and ID_FILIALE=" & Me.cmbStruttura.SelectedValue & ") )" _
                                    & "      ) order by DESCRIZIONE asc"
            Else


                par.cmd.CommandText = "select  DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                    & " where ID in (select ID_INDIRIZZO from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                 & " where ID in (select ID_UNITA from SISCOM_MI.UNITA_CONTRATTUALE " _
                                                             & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _
                                                                                    & "  where ID in (select ID_CONTRATTO from SISCOM_MI.MOROSITA_LETTERE ) " _
                       & " ))) " _
                       & " order by DESCRIZIONE asc"

            End If

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
            End If

            'If i = 1 Then
            '    Me.cmbIndirizzo.Items(1).Selected = True
            'Else
            '    Me.cmbIndirizzo.SelectedValue = "-1"
            'End If



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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
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
            End If

            'If i = 1 Then
            '    Me.cmbCivico.Items(1).Selected = True
            'End If


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

    Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
        FiltraComplessi()
        FiltraEdifici()
        FiltraIndirizzi()
        FiltraCivici()
    End Sub


End Class
