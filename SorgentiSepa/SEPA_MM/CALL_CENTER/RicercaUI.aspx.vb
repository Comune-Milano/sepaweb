
Partial Class CALL_CENTER_RicercaUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim passato As String = ""
    Dim edificio As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""
    Dim interno As String = ""
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            If Not IsPostBack Then
                Me.cmbIndirizzo.Items.Clear()
                Me.cmbCivico.Items.Clear()
                Dim gest As Integer = 0
                Session.Remove("TIPOLOGIA")

                CaricaIndirizzi()
                
            Else
                Exit Sub
            End If


            par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub CaricaIndirizzi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbIndirizzo.Items.Add(" ")

            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
            End While
            myReader1.Close()

            cmbIndirizzo.Text = " "

            cmbCivico.Items.Clear()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        'Dim bTrovato As Boolean = False
        'Dim I As Integer = 0
        'Dim TIPOLOGIA As String = ""
        'Dim Scala As String = ""
        'Dim IdInd As String = ""


        'If par.IfEmpty(Me.cmbIndirizzo.SelectedValue.ToString, "Null") <> "Null" And Me.cmbIndirizzo.SelectedValue.ToString <> "" Then
        '    If Me.cmbCivico.SelectedValue <> "-1" And Me.cmbCivico.SelectedValue <> "" Then
        '        IdInd = Me.cmbCivico.SelectedValue.ToString
        '    ElseIf Me.cmbIndirizzo.SelectedItem.Text <> "" Then
        '        indirizzo = Me.cmbIndirizzo.SelectedItem.Text
        '    End If

        '    If Me.cmbScala.SelectedValue <> "-1" Then
        '        Scala = Me.cmbScala.SelectedValue.ToString
        '    End If
        '    If Me.cmbInterno.Items.Count > 0 Then
        '        If par.IfEmpty(Me.cmbInterno.SelectedItem.ToString, "Null") <> "Null" Then
        '            interno = Me.cmbInterno.SelectedItem.ToString
        '        End If
        '    End If
        'End If

        Response.Redirect("RisultatiUI.aspx?C=" & par.IfNull(cmbCivico.SelectedValue, "") & "&IND=" & par.IfNull(par.VaroleDaPassare(cmbIndirizzo.SelectedValue), "") & "&INT=" & par.IfNull(cmbInterno.SelectedValue, "") & "&SCAL=" & par.IfNull(cmbScala.SelectedValue, ""))

    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbCivico.Items.Clear()

                par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT id_indirizzo FROM siscom_mi.unita_immobiliari WHERE ID_EDIFICIO <> 1 ) order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("civico"), " ")))
                End While
                myReader1.Close()

                Me.cmbScala.Items.Clear()
                cmbScala.Items.Add(New ListItem(" ", "-1"))

                If cmbCivico.SelectedValue <> "" And cmbIndirizzo.SelectedItem.Value <> "" Then
                    'par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = " & Me.cmbCivico.SelectedValue.ToString & " ) order by descrizione asc"
                    par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')) order by descrizione asc"
                    myReader1 = par.cmd.ExecuteReader
                    While myReader1.Read
                        cmbScala.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), " ")))
                    End While
                End If

                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If Me.cmbCivico.SelectedValue <> "" And cmbIndirizzo.SelectedItem.Value <> "" Then
                    'par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), ""))))
                    End While
                    myReader3.Close()
                End If


                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Me.TextBox1.Value = 1

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbCivico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCivico.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                If cmbCivico.Text <> "" Then
                    Me.cmbScala.Items.Clear()
                    cmbScala.Items.Add(New ListItem(" ", "-1"))
                    If cmbCivico.SelectedValue <> "" Then
                        'par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = " & Me.cmbCivico.SelectedValue.ToString & " ) order by descrizione asc"
                        par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')) order by descrizione asc"

                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader3.Read
                            cmbScala.Items.Add(New ListItem(par.IfNull(myReader3("descrizione"), " "), par.IfNull(myReader3("ID"), " ")))
                        End While
                    End If

                End If


                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If Me.cmbCivico.SelectedValue <> "" Then
                    'par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"

                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), ""))))
                    End While
                    myReader3.Close()
                End If
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Me.TextBox1.Value = 1


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbScala_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbScala.SelectedIndexChanged
        If Me.cmbScala.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbInterno.Items.Clear()
            cmbInterno.Items.Add(New ListItem(" ", "-1"))
            If Me.cmbScala.SelectedValue <> "" Then
                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari WHERE ID_SCALA = " & Me.cmbScala.SelectedValue.ToString & " order by unita_immobiliari.interno asc"
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader3.Read
                    cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), ""))))
                End While
                myReader3.Close()


            End If


        Else
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbInterno.Items.Clear()
            cmbInterno.Items.Add(New ListItem(" ", "-1"))
            If Me.cmbCivico.SelectedValue <> "" Then
                'par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader3.Read
                    cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), ""))))
                End While
                myReader3.Close()
            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End If
    End Sub
End Class
