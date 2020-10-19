
Partial Class Condomini_RicercaIndirizzo
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../Portale.aspx""</script>")
            End If

            If Not IsPostBack Then

                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                '***CARICAMENTO LISTA COMPLESSI
                DrLComplesso.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT DISTINCT complessi_immobiliari.id,COD_COMPLESSO,COMPLESSI_IMMOBILIARI.denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.EDIFICI where  complessi_immobiliari.ID=EDIFICI.ID_COMPLESSO AND EDIFICI.CONDOMINIO = 1  order by denominazione asc "
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader2.Read
                    ' DrLComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader2("cod_complesso"), " ") & "- -" & par.IfNull(myReader2("denominazione"), " "), par.IfNull(myReader2("id"), -1)))
                    DrLComplesso.Items.Add(New ListItem(par.IfNull(myReader2("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader2("cod_complesso"), " "), par.IfNull(myReader2("id"), -1)))
                End While
                myReader2.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaEdifici()
                'CaricaIndirizzi()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Sub
    'Private Sub CaricaIndirizzi()
    '    Try
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        cmbIndirizzo.Items.Add(" ")

    '        par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by descrizione asc"
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        While myReader1.Read
    '            cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
    '        End While
    '        myReader1.Close()

    '        cmbIndirizzo.Text = " "

    '        cmbCivico.Items.Clear()

    '        If cmbIndirizzo.Text <> " " Then


    '            par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
    '            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader2.Read
    '                cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
    '            End While
    '            myReader2.Close()
    '        End If

    '        cmbInterno.Items.Clear()
    '        If cmbCivico.Text <> "" Then
    '            cmbInterno.Items.Add((New ListItem(" ", "-1")))

    '            par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
    '            Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader3.Read
    '                cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
    '            End While
    '            myReader3.Close()
    '        End If
    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message
    '        par.OracleConn.Close()
    '    End Try
    'End Sub
    Private Sub CaricaEdifici()
        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbEdificio.Items.Clear()

            cmbEdificio.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND EDIFICI.CONDOMINIO = 1 order by denominazione asc"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

                'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            End While
            myReader1.Close()

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub DrLComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLComplesso.SelectedIndexChanged
        If Me.DrLComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            Me.CaricaEdificiComp()
            'Me.filtraindirizzi()
        Else
            Me.cmbEdificio.Items.Clear()
            CaricaEdifici()
            'Me.CaricaIndirizzi()
        End If
        'Me.TextBox1.Value = 1
        'Me.ListEdifci.Items.Clear()
        'Me.TxtDescInd.Text = ""
    End Sub

    'Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
    '    'If Me.cmbEdificio.SelectedValue <> "-1" Then
    '    '    Me.cmbIndirizzo.Items.Clear()
    '    '    Me.cmbCivico.Items.Clear()
    '    '    Me.filtraindirizzi()
    '    'Else
    '    '    Me.cmbIndirizzo.Items.Clear()
    '    '    Me.cmbCivico.Items.Clear()
    '    '    Me.CaricaIndirizzi()
    '    'End If
    'End Sub
    'Private Sub filtraindirizzi()
    '    Try
    '        If Me.cmbEdificio.SelectedValue <> "-1" Or Me.DrLComplesso.SelectedValue <> "-1" Then

    '            If par.OracleConn.State = Data.ConnectionState.Closed Then
    '                par.OracleConn.Open()
    '                par.SettaCommand(par)
    '            End If
    '            Me.cmbIndirizzo.Items.Clear()

    '            cmbIndirizzo.Items.Add(" ")

    '            If Me.cmbEdificio.SelectedValue <> "-1" Then
    '                par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
    '            ElseIf Me.DrLComplesso.SelectedValue <> "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then
    '                par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & Me.DrLComplesso.SelectedValue & ") order by descrizione asc"
    '            End If

    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader1.Read
    '                cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
    '            End While
    '            myReader1.Close()

    '            cmbIndirizzo.Text = " "

    '            cmbCivico.Items.Clear()

    '            If cmbIndirizzo.Text <> " " Then


    '                par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
    '                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                While myReader2.Read
    '                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
    '                End While
    '                myReader2.Close()
    '            End If

    '            cmbInterno.Items.Clear()
    '            If cmbCivico.Text <> "" Then
    '                cmbInterno.Items.Add((New ListItem(" ", "-1")))

    '                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
    '                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                While myReader3.Read
    '                    cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
    '                End While
    '                myReader3.Close()
    '            End If
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message
    '        par.OracleConn.Close()
    '    End Try
    'End Sub
    Private Sub CaricaEdificiComp()
        Try

            If Me.DrLComplesso.SelectedValue <> "-1" Then


                '*******************APERURA CONNESSIONE*********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If


                Dim gest As Integer = 0
                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))


                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.DrLComplesso.SelectedValue.ToString & " order by denominazione asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
                    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While


                myReader1.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub



    'Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
    '    Try
    '        If cmbIndirizzo.Text <> "" Then
    '            If par.OracleConn.State = Data.ConnectionState.Closed Then
    '                par.OracleConn.Open()
    '                par.SettaCommand(par)
    '            End If

    '            cmbCivico.Items.Clear()

    '            par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader1.Read
    '                cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " ")))
    '            End While
    '            myReader1.Close()

    '            cmbInterno.Items.Clear()
    '            cmbInterno.Items.Add(New ListItem(" ", "-1"))
    '            If cmbCivico.Text <> "" Then
    '                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale IN (SELECT ID FROM siscom_mi.INDIRIZZI WHERE INDIRIZZI.descrizione = '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "' AND INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Me.cmbCivico.SelectedItem.Text) & "' ) and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
    '                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '                While myReader3.Read
    '                    cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
    '                End While
    '                myReader3.Close()
    '            End If

    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If
    '        'Me.TextBox1.Value = 1
    '        'Me.ListEdifci.Items.Clear()
    '        'Me.TxtDescInd.Text = ""

    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If
    '    End Try
    'End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Response.Redirect("RisultatiIndirizzo.aspx?E=" & Me.cmbEdificio.SelectedValue.ToString & "&C=" & Me.DrLComplesso.SelectedValue.ToString)
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>parent.main.location.replace('pagina_home.aspx');</script>")

    End Sub

End Class
