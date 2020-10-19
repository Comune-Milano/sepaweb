'*** RICERCA VERIFICHE IMPIANTI

Partial Class RicercaVerifiche
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

            cmbComplesso.SelectedValue = "-1"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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

            '**** CARICO L'ELENCO COMPLESSI (IMMOBILI)
            Me.cmbTipoImpianto.Items.Clear()

            par.cmd.CommandText = "select distinct SISCOM_MI.TIPOLOGIA_IMPIANTI.COD,SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE from SISCOM_MI.TIPOLOGIA_IMPIANTI order by SISCOM_MI.TIPOLOGIA_IMPIANTI.DESCRIZIONE asc"

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

        Try


        Catch ex As Exception
            Response.Write(ex.Message)

        End Try

        Response.Write("<script>location.replace('RisultatiVerifiche.aspx?CO=" & Me.cmbComplesso.SelectedValue.ToString & "&ED=" & Me.cmbEdificio.SelectedValue.ToString & "&IM=" & Me.cmbTipoImpianto.SelectedValue.ToString & "&ORD=" & Me.RBList1.Text & "&VER=" & Me.RBverifiche.Text & "');</script>")

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
