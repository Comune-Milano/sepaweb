
Partial Class VSA_RicercaUI
    Inherits PageSetIdMode
    Dim passato As String = ""
    Dim edificio As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""
    Dim interno As String = ""
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Me.cmbIndirizzo.Items.Clear()
                Me.cmbCivico.Items.Clear()
                Dim gest As Integer = 0

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                    Exit Sub
                End If


                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                '***CARICAMENTO LISTA COMPLESSI
                DrLComplesso.Items.Add(New ListItem(" ", -1))


                par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.ID <> 1 order by denominazione asc "


                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader2.Read
                    ' DrLComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader2("cod_complesso"), " ") & "- -" & par.IfNull(myReader2("denominazione"), " "), par.IfNull(myReader2("id"), -1)))
                    DrLComplesso.Items.Add(New ListItem(par.IfNull(myReader2("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader2("cod_complesso"), " "), par.IfNull(myReader2("id"), -1)))

                End While
                myReader2.Close()

                'cmbTipologia.Items.Add(New ListItem(" ", "-1"))
                'par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipologia_unita_immobiliari order by descrizione asc"

                'myReader2 = par.cmd.ExecuteReader()
                'While myReader2.Read
                '    cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
                'End While
                'myReader2.Close()


                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                DrLComplesso.Text = "-1"
                CaricaEdifici()
                CaricaIndirizzi()

            Else
                Exit Sub
            End If
            TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

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

            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) and indirizzi.ID <> 1 order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
            End While
            myReader1.Close()

            cmbIndirizzo.Text = " "

            cmbCivico.Items.Clear()

            If cmbIndirizzo.Text <> " " Then


                par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
                End While
                myReader2.Close()
            End If

            cmbInterno.Items.Clear()
            If cmbCivico.Text <> "" Then
                cmbInterno.Items.Add((New ListItem(" ", "-1")))

                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader3.Read
                    cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                End While
                myReader3.Close()
            End If
            '*********************CHIUSURA CONNESSIONE**********************
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
        Response.Write("<script>self.close();</script>")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean = False
        If Me.cmbEdificio.Items.Count > 0 Then
            If Me.cmbEdificio.Text <> "-1" Then
                edificio = Me.cmbEdificio.SelectedValue.ToString
            End If
        Else
            Response.Write("<SCRIPT>alert('Non è stato caricato alcun edificio!');</SCRIPT>")
            Exit Sub
        End If

        If par.IfEmpty(Me.cmbIndirizzo.SelectedValue.ToString, "Null") <> "Null" And Me.cmbIndirizzo.SelectedValue.ToString <> "" Then
            indirizzo = Me.cmbIndirizzo.SelectedItem.Text
            If Me.cmbCivico.Text <> "" Then
                civico = Me.cmbCivico.SelectedItem.Text
            End If
            If par.IfEmpty(Me.cmbInterno.SelectedItem.ToString, "Null") <> "Null" Then
                interno = Me.cmbInterno.SelectedItem.ToString
            End If
        End If

        'If cmbIndirizzo.SelectedValue.ToString <> -1 OrElse Me.cmbIndirizzo.Text <> " " Then
        '    indirizzo = Me.cmbIndirizzo.SelectedItem.Text
        '    If Me.cmbCivico.Text <> -1 OrElse Me.cmbCivico.Text <> " " Then
        '        civico = Me.cmbCivico.SelectedItem.Text
        '    End If
        'End If
        'Session.Add("CENSIMENTO", sStringaSql)
        'Response.Write("<script>location.href='RisultatiUI.aspx';</script>")
        Response.Redirect("RisultatiUI.aspx?E=" & edificio & "&I=" & par.VaroleDaPassare(par.PulisciStrSql(indirizzo)) & "&CIV=" & par.VaroleDaPassare(par.PulisciStrSql(civico)) & "&COMP=" & par.VaroleDaPassare(par.PulisciStrSql(Me.DrLComplesso.SelectedValue.ToString)) & "&TIPOL=" & par.VaroleDaPassare(par.PulisciStrSql("AL")) & "&INT=" & interno)

    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.Text <> "-1" Then
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.filtraindirizzi()
        Else
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.CaricaIndirizzi()
        End If
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.TxtDescInd.Text = ""
    End Sub

    Private Sub filtraindirizzi()
        Try
            If Me.cmbEdificio.SelectedValue <> "-1" Or Me.DrLComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.cmbIndirizzo.Items.Clear()

                cmbIndirizzo.Items.Add(" ")

                If Me.cmbEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
                ElseIf Me.DrLComplesso.SelectedValue <> "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_RIFERIMENTO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE ID = " & Me.DrLComplesso.SelectedValue & ") order by descrizione asc"
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
                End While
                myReader1.Close()

                cmbIndirizzo.Text = " "

                cmbCivico.Items.Clear()

                If cmbIndirizzo.Text <> " " Then


                    par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader2.Read
                        cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
                    End While
                    myReader2.Close()
                End If

                cmbInterno.Items.Clear()
                If cmbCivico.Text <> "" Then
                    cmbInterno.Items.Add((New ListItem(" ", "-1")))

                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                    End While
                    myReader3.Close()
                End If
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub
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

            If Session("PED2_ESTERNA") = "1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 and edifici.ID <> 1 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and edifici.ID <> 1 order by denominazione asc"

            End If
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
            Me.filtraindirizzi()
        Else
            Me.cmbEdificio.Items.Clear()
            CaricaEdifici()
            Me.CaricaIndirizzi()
        End If
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.TxtDescInd.Text = ""
    End Sub

    Private Sub CaricaEdificiComp()
        Try
            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If


            Dim gest As Integer = 3
            If Me.DrLComplesso.Text <> "-1" Then
                '****CARICA LISTA EDIFICI
                cmbEdificio.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.EDIFICI  where id_complesso = " & Me.DrLComplesso.SelectedValue.ToString & " order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                cmbEdificio.Text = "-1"
                cmbEdificio.Items.Add(New ListItem(" ", -1))
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try


    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbCivico.Items.Clear()

                par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " ")))
                End While
                myReader1.Close()

                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If cmbCivico.Text <> "" Then
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale IN (SELECT ID FROM siscom_mi.INDIRIZZI WHERE INDIRIZZI.descrizione = '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "' AND INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Me.cmbCivico.SelectedItem.Text) & "' ) and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                    End While
                    myReader3.Close()
                End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        If Me.ListEdifci.SelectedValue.ToString <> "" Then
            Me.cmbEdificio.SelectedValue = Me.ListEdifci.SelectedValue.ToString
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.TextBox1.Value = 1
            Me.LblNoResult.Visible = False
        Else
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.LblNoResult.Visible = False
            Me.TextBox1.Value = 1
        End If
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
            Me.ListEdifci.Items.Clear()

            If Session("PED2_ESTERNA") = "1" Then
                If Me.DrLComplesso.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.DrLComplesso.SelectedValue.ToString & " order by denominazione asc"

                End If
            Else
                If Me.DrLComplesso.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' AND EDIFICI.ID_COMPLESSO = " & Me.DrLComplesso.SelectedValue.ToString & "order by denominazione asc"

                End If

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                ListEdifci.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
        End If
        If ListEdifci.Items.Count = 0 Then
            Me.LblNoResult.Visible = True
        Else
            Me.LblNoResult.Visible = False
        End If
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Me.TextBox1.Value = 2
    End Sub

    Protected Sub cmbCivico_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCivico.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If cmbCivico.Text <> "" Then
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale IN (SELECT ID FROM siscom_mi.INDIRIZZI WHERE INDIRIZZI.descrizione = '" & par.PulisciStrSql(Me.cmbIndirizzo.SelectedItem.Text) & "' AND INDIRIZZI.CIVICO = '" & par.PulisciStrSql(Me.cmbCivico.SelectedItem.Text) & "' ) and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                    End While
                    myReader3.Close()
                End If

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub


End Class
