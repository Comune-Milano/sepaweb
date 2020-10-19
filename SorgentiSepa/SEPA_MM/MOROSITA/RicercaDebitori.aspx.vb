'*** RICERCA DEBOTORI x MESSA in MORA
Partial Class MOROSITA_RicercaDebitori
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href=""../Portale.aspx"";", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            cmbTipoContr.Visible = "False"
            lblTipoContr.Visible = "False"
            'NON FILTRATI PER OPERATORE MA PER SCELTA LIBERA
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
            CaricaStrutture()
            CaricaComplessi()
            CaricaEdifici()
            CaricaIndirizzi()
            CaricaTipologiaUI()         'TIPOLOGIA_UNITA_IMMOBILIARI
            CaricaTipologiaContratto()  'TIPOLOGIA_CONTRATTO_LOCAZIONE.COD (RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC)
            Me.txtImporto1.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            'Me.txtImporto1.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")
            Me.txtImporto2.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
            'Me.txtImporto2.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")
            Me.txtDataRIF_DAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataRIF_AL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataS_DAL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtDataS_AL.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
        End If
    End Sub
    'CARICO COMBO STRUTTURE (FILIARI)
    Private Sub CaricaStrutture()
        Dim FlagConnessione As Boolean
        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            'Me.cmbStruttura.Items.Clear()
            'Me.cmbStruttura.Items.Add(New ListItem(" ", -1))
            Me.CheckStrutture.Items.Clear()
            Me.txtID_STRUTTURE.Value = ""
            Me.txtID_STRUTTURE_SEL.Value = ""
            'If sFiliale <> "" Then
            '    par.cmd.CommandText = "select ID,NOME from SISCOM_MI.TAB_FILIALI where ID=" & sFiliale
            'Else
            par.cmd.CommandText = "select ID,trim(NOME) as NOME from SISCOM_MI.TAB_FILIALI where ID_TIPO_ST=0 order by NOME asc"
            'End If
            'Dim s1 As String
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                'Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
                's1 = par.IfNull(myReader1("NOME"), " ")
                'String.Format("{0,-100}", s1.PadRight(100).Substring(0, 100))
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
            End If
            'Me.cmbStruttura.SelectedValue = "-1"
            CaricaComplessi()
        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)
        End Try
    End Sub
    'CARICO COMBO COMPLESSI
    Private Sub CaricaComplessi()
        Dim FlagConnessione As Boolean
        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            '& "  where ID in (select ID_CONTRATTO from SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            '& " where (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') " _
            '     & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _


            par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                               & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                       & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                    & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
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
            'CaricaEdifici()


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try

    End Sub


    'CARICO COMBO EDIFICI
    Private Sub CaricaEdifici()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO L'ELENCO EDIFICI (FABBRICATI)
            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            '   & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _

            '& "  where ID in (select ID_CONTRATTO from SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            ' & " where (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') " _

            par.cmd.CommandText = "select distinct ID,(trim(DENOMINAZIONE)||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE  from SISCOM_MI.EDIFICI " _
                                & " where ID<>1 and ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
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

            'Me.cmbEdificio.SelectedValue = "-1"
            'CaricaIndirizzi()


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try


    End Sub


    'CARICO COMBO EDIFICI
    Private Sub CaricaIndirizzi()
        Dim i As Integer
        Dim FlagConnessione As Boolean


        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            ' & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _

            '& "  where ID in (select ID_CONTRATTO from SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            '& " where (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') " _

            par.cmd.CommandText = "select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                & " where ID in (select distinct(ID_INDIRIZZO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                             & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                   & " )) " _
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


            'Me.cmbIndirizzo.SelectedValue = "-1"
            'FiltraCivici()


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try


    End Sub

    'CARICO COMBO TIPOLOGIA SERVIZI
    Private Sub CaricaTipologiaUI()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbTipologiaUI.Items.Clear()
            'cmbTipologiaUI.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select COD,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI " _
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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try

    End Sub


    'CARICO COMBO TIPOLOGIA_CONTRATTO_LOCAZIONE
    Private Sub CaricaTipologiaContratto()
        Dim FlagConnessione As Boolean

        Try
            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbTipologiaRapporto.Items.Clear()
            Me.cmbTipologiaRapporto.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select COD,trim(DESCRIZIONE) as DESCRIZIONE from SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                               & " order by DESCRIZIONE asc"


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                Me.cmbTipologiaRapporto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While
            myReader1.Close()

            Me.cmbTipologiaRapporto.SelectedValue = "-1"
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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try

    End Sub




    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "document.location.href=""pagina_home.aspx"";", True)
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
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbComplesso.Items.Clear()
            Me.cmbComplesso.Items.Add(New ListItem(" ", -1))

            ' & " where ID_CONTRATTO in (select ID from SISCOM_MI.RAPPORTI_UTENZA " _

            '& "  where ID in (select ID_CONTRATTO from SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            '& " where (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') " _

            'If Me.cmbStruttura.SelectedValue <> "-1" Then
            If Me.txtID_STRUTTURE_SEL.Value <> "" Then

                par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                          & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                       & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                   & " ))) " _
                                   & "   and ID_FILIALE in (" & Me.txtID_STRUTTURE_SEL.Value & ") " _
                                   & " order by DENOMINAZIONE asc"

            Else
                par.cmd.CommandText = "select distinct ID,trim(DENOMINAZIONE) as DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                   & " where ID<>1 and ID in (select distinct(ID_COMPLESSO) from SISCOM_MI.EDIFICI " _
                                                          & " where ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                       & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                   & " ))) " _
                                   & "   and ID_FILIALE in (" & Me.txtID_STRUTTURE.Value & ") " _
                                   & " order by DENOMINAZIONE asc"
            End If


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
            'FiltraEdifici()


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try

    End Sub



    Private Sub FiltraEdifici()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            Me.cmbEdificio.Items.Clear()
            Me.cmbEdificio.Items.Add(New ListItem(" ", -1))

            '& "  where ID in (select ID_CONTRATTO from SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            '    & " where (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') " _

            If Me.cmbComplesso.SelectedValue <> "-1" Then
                par.cmd.CommandText = "select distinct ID,(trim(DENOMINAZIONE)||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE " _
                                   & " from SISCOM_MI.EDIFICI " _
                                   & " where ID<>1 and ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue _
                                   & " order by DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct ID,(trim(DENOMINAZIONE)||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE  from SISCOM_MI.EDIFICI " _
                                    & " where ID<>1 and ID in (select distinct(ID_EDIFICIO) from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                           & " where ID in (select distinct(ID_UNITA) from SISCOM_MI.UNITA_CONTRATTUALE " _
                                   & " )) "

                If Me.txtID_STRUTTURE_SEL.Value <> "" Then
                    par.cmd.CommandText = par.cmd.CommandText & "  and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 and ID_FILIALE in (" & Me.txtID_STRUTTURE_SEL.Value & ") )"
                Else
                    par.cmd.CommandText = par.cmd.CommandText & "  and ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 and ID_FILIALE in (" & Me.txtID_STRUTTURE.Value & ") )"
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
            'FiltraIndirizzi()


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)


        End Try

    End Sub

    Private Sub FiltraIndirizzi()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO GLI INDIRIZZI
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            '& "  where ID in (select ID_CONTRATTO from SISCOM_MI.SOGGETTI_CONTRATTUALI " _
            '                & " where (SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='INTE' or SOGGETTI_CONTRATTUALI.COD_TIPOLOGIA_OCCUPANTE='COINT') " _

            If Me.cmbEdificio.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                    & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI where ID=" & Me.cmbEdificio.SelectedValue & " )" _
                                    & " order by DESCRIZIONE asc"

            ElseIf Me.cmbComplesso.SelectedValue <> "-1" Then

                par.cmd.CommandText = " select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                    & " where ID in (select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI" _
                                                 & " where ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue & " )" _
                                    & " order by DESCRIZIONE asc"

            Else

                If Me.txtID_STRUTTURE_SEL.Value <> "" Then
                    par.cmd.CommandText = " select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                        & " where ID in (select ID_INDIRIZZO_PRINCIPALE " _
                                                     & " from SISCOM_MI.EDIFICI " _
                                                     & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                            & " where ID<>1 and ID_FILIALE in (" & Me.txtID_STRUTTURE_SEL.Value & ") ) )" _
                                        & " order by DESCRIZIONE asc"

                Else
                    par.cmd.CommandText = " select DISTINCT(trim(DESCRIZIONE)) as DESCRIZIONE from SISCOM_MI.INDIRIZZI " _
                                        & " where ID in (select ID_INDIRIZZO_PRINCIPALE " _
                                                     & " from SISCOM_MI.EDIFICI " _
                                                     & " where ID_COMPLESSO in (select ID from SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                                                                            & " where ID<>1 and ID_FILIALE in (" & Me.txtID_STRUTTURE.Value & ") ) )" _
                                        & " order by DESCRIZIONE asc"
                End If

            End If

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
            'FiltraCivici()


        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)

        End Try

    End Sub

    Private Sub FiltraCivici()
        Dim sWhere As String = ""
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                'Response.Write("IMPOSSIBILE VISUALIZZARE")
                'Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If


            '**** CARICO I CIVICI PER L'INDIRIZZO SELEZIONATO
            Me.cmbCivico.Items.Clear()
            Me.cmbCivico.Items.Add(New ListItem(" ", -1))


            If Me.cmbIndirizzo.SelectedValue <> "-1" Then
                par.cmd.CommandText = " select DISTINCT(trim(CIVICO)) as CIVICO from SISCOM_MI.INDIRIZZI " _
                                    & " where DESCRIZIONE='" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "'" _
                                    & " order by CIVICO asc"



                'i = 0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    Me.cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("CIVICO"), " "), i))
                    'i = i + 1
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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)


        End Try

    End Sub

    'Protected Sub cmbStruttura_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbStruttura.SelectedIndexChanged
    '    'FiltraComplessi()
    '    'FiltraEdifici()
    '    'FiltraIndirizzi()
    '    'FiltraCivici()
    'End Sub

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

    Protected Sub btnDeselTuttiAREA_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDeselTuttiAREA.Click
        Dim i As Integer

        Me.txtID_AREE_SEL.Value = ""

        For i = 0 To Me.CheckAreaCanone.Items.Count - 1
            Me.CheckAreaCanone.Items(i).Selected = False
        Next

    End Sub


    Private Sub CheckAreaCanone_Selezionati()
        Dim i As Integer
        Dim conta As Integer = 0

        Me.txtID_AREE_SEL.Value = ""

        For i = 0 To Me.CheckAreaCanone.Items.Count - 1
            If Me.CheckAreaCanone.Items(i).Selected = True Then

                conta = conta + 1
                If Me.txtID_AREE_SEL.Value = "" Then
                    Me.txtID_AREE_SEL.Value = Me.CheckAreaCanone.Items(i).Value
                Else
                    Me.txtID_AREE_SEL.Value = Me.txtID_AREE_SEL.Value & "," & Me.CheckAreaCanone.Items(i).Value
                End If
            End If
        Next

        If conta = 4 Then Me.txtID_AREE_SEL.Value = ""
    End Sub


    Protected Sub cmbTipologiaRapporto_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipologiaRapporto.SelectedIndexChanged
        If cmbTipologiaRapporto.SelectedValue = "ERP" Then
            cmbTipoContr.Items.Clear()
            cmbTipoContr.Items.Add(New ListItem("TUTTI", "0"))
            cmbTipoContr.Items.Add(New ListItem("Canone Convenzionato", "12"))
            cmbTipoContr.Items.Add(New ListItem("Art.22 C.10 RR 1/2004", "8"))
            cmbTipoContr.Items.Add(New ListItem("Forze dell'Ordine", "10"))
            cmbTipoContr.Items.Add(New ListItem("ERP Moderato", "2"))
            cmbTipoContr.Items.Add(New ListItem("ERP Sociale", "1"))
            cmbTipoContr.Visible = "True"
            lblTipoContr.Visible = "True"
        ElseIf cmbTipologiaRapporto.SelectedValue = "L43198" Then
            cmbTipoContr.Items.Clear()
            cmbTipoContr.Items.Add(New ListItem("TUTTI", "-1"))
            cmbTipoContr.Items.Add(New ListItem("Standard", "0"))
            cmbTipoContr.Items.Add(New ListItem("Cooperative", "C"))
            cmbTipoContr.Items.Add(New ListItem("431 P.O.R.", "P"))
            cmbTipoContr.Items.Add(New ListItem("431/98 Art.15 R.R.1/2004", "D"))
            cmbTipoContr.Items.Add(New ListItem("431/98 Speciali", "S"))
            cmbTipoContr.Visible = "True"
            lblTipoContr.Visible = "True"
        Else
            cmbTipoContr.Visible = "False"
            lblTipoContr.Visible = "False"
        End If
    End Sub

    Protected Sub btnCerca_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim ControlloCampi As Boolean
        Dim sTipoImm As String
        Dim sTipoRicerca As String = ""
        Dim i As Integer
        Try

            ControlloCampi = True

            If Strings.Len(Strings.Trim(Me.txtID_STRUTTURE_SEL.Value)) = 0 Then
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione...Selezionare una o più Strutture!');", True)
                ControlloCampi = False
                Exit Sub
            End If

            'If Strings.Len(Strings.Trim(Me.txtDataDAL.Text)) = 0 Then
            '    Response.Write("<script>alert('Attenzione...Inserire la data di inizio consistenza debito!');</script>")
            '    ControlloCampi = False
            '    Exit Sub
            'End If


            'If Strings.Len(Strings.Trim(Me.txtDataAL.Text)) = 0 Then
            '    Response.Write("<script>alert('Attenzione...Inserire la data di fine consistenza debito!');</script>")
            '    ControlloCampi = False
            '    Exit Sub
            'End If

            If Strings.Len(Me.txtDataRIF_AL.Text) > 0 Then
                If par.AggiustaData(Me.txtDataRIF_AL.Text) < par.AggiustaData(Me.txtDataRIF_DAL.Text) Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione...Controllare il range delle date di competenza delle bollette!');", True)
                    ControlloCampi = False
                    Exit Sub
                End If
            End If
            If Strings.Len(Me.txtDataS_AL.Text) > 0 Then
                If par.AggiustaData(Me.txtDataS_AL.Text) < par.AggiustaData(Me.txtDataS_DAL.Text) Then

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Attenzione...Controllare il range delle date della stipula del contratto!');", True)
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
            'If Me.chkFiltraMor.Checked = True Then
            'If String.IsNullOrEmpty(Me.txtDataRIF_DAL.Text) Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Definire la data inizio del periodo di riferimento!');", True)
            '    ControlloCampi = False
            '    Exit Sub

            'End If
            'If String.IsNullOrEmpty(Me.txtDataRIF_AL.Text) Then
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert('Definire la data fine del periodo di riferimento!');", True)
            '    ControlloCampi = False
            '    Exit Sub

            'End If
            If Not String.IsNullOrEmpty(Me.txtDataRIF_DAL.Text) And Not String.IsNullOrEmpty(Me.txtDataRIF_AL.Text) Then
                If par.AggiustaData(Me.txtDataRIF_DAL.Text) > par.AggiustaData(Me.txtDataRIF_AL.Text) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert(Periodo di riferimento incoerente!');", True)
                    ControlloCampi = False
                    Exit Sub

                End If
            End If
            If Not String.IsNullOrEmpty(Me.txtImporto1.Text) And Not String.IsNullOrEmpty(Me.txtImporto2.Text) Then
                If CDec(txtImporto1.Text) > CDec(Me.txtImporto2.Text) Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "alert(Range di importi incoerenti!');", True)
                    ControlloCampi = False
                    Exit Sub

                End If
            End If



            'End If
            'Salva in txtID_AREE_SEL le aree canoni selezionate
            CheckAreaCanone_Selezionati()
            If ControlloCampi = True Then
                Dim filtrBolInMora As String = 0
                If Me.chkFiltraMor.Checked Then
                    filtrBolInMora = "&FILTDATE=1"
                End If
                Dim testo As String = ""
                testo = "location.replace('RisultatiDebitori.aspx?FI=" & par.VaroleDaPassare(Me.txtID_STRUTTURE_SEL.Value) _
                               & "&CO=" & Me.cmbComplesso.SelectedValue.ToString _
                               & "&ED=" & Me.cmbEdificio.SelectedValue.ToString _
                               & "&IN=" & par.VaroleDaPassare(Me.cmbIndirizzo.SelectedItem.Text) _
                               & "&CI=" & par.VaroleDaPassare(Me.cmbCivico.SelectedItem.Text) _
                               & "&CG=" & par.VaroleDaPassare(Me.txtCognome.Text) _
                               & "&NM=" & par.VaroleDaPassare(Me.txtNome.Text) _
                               & "&CD=" & par.VaroleDaPassare(Me.txtCodice.Text) _
                               & "&TI=" & sTipoImm _
                               & "&DAL_S=" & par.IfEmpty(par.AggiustaData(Me.txtDataS_DAL.Text), "") _
                               & "&AL_S=" & par.IfEmpty(par.AggiustaData(Me.txtDataS_AL.Text), "") _
                               & "&IMP1=" & par.VirgoleInPunti(Me.txtImporto1.Text.Replace(".", "")) _
                               & "&IMP2=" & par.VirgoleInPunti(Me.txtImporto2.Text.Replace(".", "")) _
                               & "&BOLDA=" & Me.txtNumBolletteDA.Text _
                               & "&BOLA=" & Me.txtNumBolletteA.Text _
                               & "&RAPP=" & Me.cmbTipologiaRapporto.SelectedValue.ToString _
                               & "&ST=" & Me.cmbStato.SelectedValue.ToString _
                               & "&MORA=" & sTipoRicerca _
                               & "&ORD=" & Me.RBList1.SelectedValue _
                               & "&DAL=" & par.IfEmpty(par.AggiustaData(Me.txtDataRIF_DAL.Text), "") _
                               & "&AL=" & par.IfEmpty(par.AggiustaData(Me.txtDataRIF_AL.Text), "") _
                               & "&AREAC=" & par.VaroleDaPassare(Me.txtID_AREE_SEL.Value) _
                               & "&TIPOCONTR=" & par.VaroleDaPassare(Me.cmbTipoContr.SelectedValue) & filtrBolInMora _
                               & "');"

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", testo, True)
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "msg", "top.location.href='../Errore.aspx';", True)
        End Try
    End Sub
End Class
