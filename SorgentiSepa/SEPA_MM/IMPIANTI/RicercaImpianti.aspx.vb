'*** RICERCA IMPIANTI

Partial Class IMPIANTI_RicercaImpianti
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            vId = 0
            '**TipoImmobile = Session.Item("BUILDING_TYPE")
            vId = Session.Item("ID")

            RiempiComplessi()
            RiempiTipologiaImpianto()

            'lblMatricola.Visible = False
            'txtNumMatricola.Visible = False

            'lblNumLotto.Visible = False
            'txtNumLotto.Visible = False
            'RBList1.Items(4).Enabled = False

            cmbTipoImpianto.Attributes.Add("onchange", "javascript:selezTipologia();")

        End If


        'If cmbTipoImpianto.SelectedItem.Text = "SOLLEVAMENTO" Then

        '    lblMatricola.Visible = True
        '    txtNumMatricola.Visible = True

        '    lblNumLotto.Visible = True
        '    txtNumLotto.Visible = True

        '    RBList1.Items(4).Enabled = True

        'Else

        '    lblMatricola.Visible = False
        '    txtNumMatricola.Visible = False

        '    lblNumLotto.Visible = False
        '    txtNumLotto.Visible = False
        '    RBList1.Items(4).Enabled = False

        'End If




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

    'CARICO COMBO COMPLESSI
    Private Sub RiempiComplessi()
        Dim gest As Integer = 0

        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            '**** CARICO L'ELENCO COMPLESSI (IMMOBILI)
            Me.cmbComplesso.Items.Clear()

            If gest > 0 Then
                par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where substr(SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,1,1)= " & gest & " and ID<>1 order by SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct SISCOM_MI.COMPLESSI_IMMOBILIARI.ID,SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID<>1 order by SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            cmbComplesso.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            myReader1.Close()
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            cmbComplesso.SelectedValue = "-1"

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

        CaricaEdifici()

    End Sub


    'CARICO COMBO EDIFICI
    Private Sub CaricaEdifici()
        Dim gest As Integer = 0

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
            cmbEdificio.Items.Add(New ListItem(" ", -1))

            If gest <> 0 Then
                par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(SISCOM_MI.EDIFICI.ID,1,1)= " & gest & " order by SISCOM_MI.EDIFICI.DENOMINAZIONE asc"
            Else
                par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,(SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI order by DENOMINAZIONE asc"
            End If

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            End While

            myReader1.Close()
            cmbEdificio.SelectedValue = "-1"
            '**************************

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub


    'CARICO COMBO TIPOLOGIA IMPIANTO
    Private Sub RiempiTipologiaImpianto()
        Try

            ' APRO CONNESSIONE
            If par.OracleConn.State = Data.ConnectionState.Open Then
                Response.Write("IMPOSSIBILE VISUALIZZARE")
                Exit Sub
            Else
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbTipoImpianto.Items.Clear()

            par.cmd.CommandText = "select SISCOM_MI.TIPOLOGIA_IMPIANTI.COD,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE from SISCOM_MI.TIPOLOGIA_IMPIANTI order by SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            cmbTipoImpianto.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbTipoImpianto.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("COD"), -1)))
            End While

            myReader1.Close()
            '**************************

            cmbTipoImpianto.SelectedValue = "-1"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub
    
    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        If Me.cmbComplesso.SelectedValue <> "-1" Then
            FiltraEdifici()
        Else
            Me.cmbTipoImpianto.Items.Clear()
            CaricaEdifici()
        End If

    End Sub


    Private Sub FiltraEdifici()
        Dim gest As Integer = 0

        Try
            If Me.cmbComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))

                If gest <> 0 Then
                    par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,SISCOM_MI.EDIFICI.DENOMINAZIONE from SISCOM_MI.EDIFICI where substr(SISCOM_MI.EDIFICI.ID,1,1)= " & gest & " order by SISCOM_MI.EDIFICI.DENOMINAZIONE asc"
                Else
                    par.cmd.CommandText = "select distinct SISCOM_MI.EDIFICI.ID,SISCOM_MI.EDIFICI.DENOMINAZIONE from SISCOM_MI.EDIFICI where SISCOM_MI.EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by SISCOM_MI.EDIFICI.DENOMINAZIONE asc"
                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                End While

                myReader1.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If


        Catch ex As Exception
            par.OracleConn.Close()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try

    End Sub




    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'Dim sStringaSql As String
        'Dim sWhere As String

        'sStringaSql = ""
        'sWhere = ""

        Try

            'sStringaSql = "select  SISCOM_MI.IMPIANTI.ID AS ""ID_IMPIANTO""," _
            '                    & "SISCOM_MI.COMPLESSI_IMMOBILIARI.COD_COMPLESSO," _
            '                    & "SISCOM_MI.COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DENOMINAZIONE_COMPLESSO""," _
            '                    & "SISCOM_MI.EDIFICI.DENOMINAZIONE AS ""DENOMINAZIONE_EDIFICIO""," _
            '                    & "SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE AS ""TIPO_IMPIANTO""," _
            '                    & "SISCOM_MI.IMPIANTI.DESCRIZIONE AS ""DENOMINAZIONE_IMPIANTO""" _
            '            & " from SISCOM_MI.IMPIANTI, SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.EDIFICI, SISCOM_MI.TIPOLOGIA_IMPIANTI " _
            '            & " where     SISCOM_MI.IMPIANTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) and " _
            '            & "     SISCOM_MI.IMPIANTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) and " _
            '            & "     SISCOM_MI.IMPIANTI.COD_TIPOLOGIA=SISCOM_MI.TIPOLOGIA_IMPIANTI.COD (+) "

            'If IfEmpty(cmbComplesso.Text, "") <> "" And cmbComplesso.Text <> "-1" Then
            '    'If sWhere = "" Then
            '    '    sWhere = " where  SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString
            '    'Else
            '    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_COMPLESSO=" & Me.cmbComplesso.SelectedValue.ToString
            '    'End If
            'End If

            'If IfEmpty(cmbEdificio.Text, "") <> "" And cmbEdificio.Text <> "-1" Then
            '    'If sWhere = "" Then
            '    '    sWhere = " where  SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue.ToString
            '    'Else
            '    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.ID_EDIFICIO=" & Me.cmbEdificio.SelectedValue.ToString
            '    'End If
            'End If

            'If IfEmpty(cmbTipoImpianto.Text, "") <> "" And cmbTipoImpianto.Text <> "-1" Then
            '    'If sWhere = "" Then
            '    '    sWhere = " where  SISCOM_MI.IMPIANTI.COD_TIPOLOGIA='" & Me.cmbTipoImpianto.SelectedValue.ToString & "' "
            '    'Else
            '    sWhere = sWhere & " and  SISCOM_MI.IMPIANTI.COD_TIPOLOGIA='" & Me.cmbTipoImpianto.SelectedValue.ToString & "' "
            '    'End If
            'End If

            'sStringaSql = sStringaSql & sWhere

        Catch ex As Exception
            Response.Write(ex.Message)

        End Try

        'Session.Clear()

        Response.Write("<script>location.replace('RisultatiImpianti.aspx?CO=" & Me.cmbComplesso.SelectedValue.ToString & "&ED=" & Me.cmbEdificio.SelectedValue.ToString & "&IM=" & Me.cmbTipoImpianto.SelectedValue.ToString & "&MA=" & par.VaroleDaPassare(Me.txtNumMatricola.Text) & "&LO=" & par.VaroleDaPassare(Me.txtNumLotto.Text) & "&ORD=" & Me.RBList1.Text & "');</script>")

        'Session.Add("IMP1", sStringaSql)
        'Session.Add("IMP2", sWhere)

        'Response.Redirect("RisultatiImpianti.aspx")

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




End Class
