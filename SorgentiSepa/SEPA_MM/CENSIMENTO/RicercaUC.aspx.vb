
Partial Class CENSIMENTO_RicercaUC
    Inherits PageSetIdMode
    Dim passato As String = ""
    Dim edificio As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""
    Dim par As New CM.Global
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
                Dim gest As Integer = 3

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                    Exit Sub
                End If


                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                ' '' '' '' '' ''****CARICA LISTA EDIFICI
                '' '' '' '' ''cmbEdificio.Items.Add(New ListItem(" ", -1))
                '' '' '' '' ''par.cmd.CommandText = "SELECT distinct id,denominazione FROM SISCOM_MI.edifici where substr(id,1,1)= " & gest & " order by denominazione asc"
                '' '' '' '' ''Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '' '' '' '' ''While myReader1.Read
                '' '' '' '' ''    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                '' '' '' '' ''End While
                '' '' '' '' ''myReader1.Close()
                '' '' '' '' ''cmbEdificio.Text = "-1"
                '' '' '' '' ''cmbEdificio.Items.Add(New ListItem(" ", -1))

                '***CARICAMENTO LISTA COMPLESSI

                DrLComplesso.Items.Add(New ListItem(" ", -1))
                If Session("PED2_ESTERNA") = "1" Then
                    par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.ID <> 1  order by denominazione asc "
                Else
                    par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.ID <> 1 order by denominazione asc "
                End If

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                While myReader2.Read
                    'DrLComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader2("cod_complesso"), " ") & "- -" & par.IfNull(myReader2("denominazione"), " "), par.IfNull(myReader2("id"), -1)))
                    DrLComplesso.Items.Add(New ListItem(par.IfNull(myReader2("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader2("cod_complesso"), " "), par.IfNull(myReader2("id"), -1)))

                End While
                myReader2.Close()

                cmbTipologia.Items.Add(New ListItem(" ", "-1"))
                par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipo_unita_comune order by descrizione asc"

                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
                End While
                myReader2.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                DrLComplesso.Text = "-1"


                CaricaEdifici()
                'CaricaTuttiIndirizzi()

            Else
                Exit Sub
            End If
            TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            TextBoxDescIndEd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

            par.OracleConn.Close()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
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
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 and complessi_immobiliari.ID <> 1 order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and complessi_immobiliari.ID <> 1 order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
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

    Private Sub CaricaTuttiIndirizzi()
        Try

            '*********************APERTURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi where id in (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI)order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " ")))
            End While
            myReader1.Close()
            cmbIndirizzo.Text = "-1"

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub CaricaIndirizzi()
        Try

            If cmbEdificio.Text <> "" Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                'Prima era così
                'par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by descrizione asc"
                'Adesso è così

                par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE SISCOM_MI.INDIRIZZI.ID =(SELECT EDIFICI.ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE ID= " & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
                End While
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                cmbCivico.Items.Clear()
                cmbIndirizzo.Text = "-1"

            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub
    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Dim bTrovato As Boolean = False
        If Me.DrLComplesso.Items.Count > 0 And Me.cmbEdificio.Items.Count > 0 Then
            If Me.DrLComplesso.SelectedValue <> "-1" Or Me.cmbEdificio.SelectedValue <> "-1" Then

                If Me.cmbEdificio.Text <> "-1" Then
                    edificio = Me.cmbEdificio.SelectedValue
                    passato = "ED"
                ElseIf Me.DrLComplesso.Text <> "-1" Then
                    edificio = Me.DrLComplesso.SelectedValue
                    passato = "COM"
                End If
                If Me.cmbIndirizzo.Items.Count > 0 Then
                    If Me.cmbIndirizzo.Text <> "-1" Then
                        indirizzo = Me.cmbIndirizzo.SelectedItem.Text
                        If Me.cmbCivico.Text <> "-1" Then
                            civico = Me.cmbCivico.SelectedItem.Text
                        End If
                    End If
                End If

            Else
                Response.Write("<SCRIPT>alert('Bisogna selezionare almeno un Edificio o un Complesso');</SCRIPT>")
                Exit Sub

            End If

        Else
            Response.Write("<SCRIPT>alert('Non e\' stato trovato alcun Complesso o Edificio!');</SCRIPT>")
            Exit Sub
        End If

        Session.Add("CENSIMENTO", sStringaSql)
        Response.Redirect("RisultatiUC.aspx?E=" & edificio & "&I=" & par.VaroleDaPassare(par.PulisciStrSql(indirizzo)) & "&CIV=" & par.VaroleDaPassare(par.PulisciStrSql(civico)) & "&TIPOL=" & par.VaroleDaPassare(par.PulisciStrSql(Me.cmbTipologia.SelectedValue.ToString)) & "&PAS=" & passato)
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.Text <> "-1" Then
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.CaricaIndirizzi()
        Else
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            'Me.CaricaTuttiIndirizzi()
        End If
        Me.TextBox2.Value = 1
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.ListEdifici2.Items.Clear()
        Me.TextBoxDescIndEd.Text = ""
        Me.TxtDescInd.Text = ""
    End Sub

    Protected Sub DrLComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLComplesso.SelectedIndexChanged
        If Me.DrLComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            Me.CaricaEdificiComp()

        Else
            Me.cmbEdificio.Items.Clear()
            CaricaEdifici()
        End If

        Me.TextBox2.Value = 1
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.ListEdifici2.Items.Clear()
        Me.TextBoxDescIndEd.Text = ""
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

                Me.cmbEdificio.Items.Clear()
                '****CARICA LISTA EDIFICI
                cmbEdificio.Items.Add(New ListItem(" ", -1))
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.EDIFICI  where id_complesso = " & Me.DrLComplesso.SelectedValue.ToString & " order by denominazione asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
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
            If cmbEdificio.Text <> "" Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbCivico.Items.Clear()

                par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Me.cmbCivico.Items.Add(New ListItem(" ", -1))
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("id"), "-1")))
                End While
                myReader1.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Me.cmbCivico.Text = "-1"
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
            Me.ListEdifci.Items.Clear()
            If Session("PED2_ESTERNA") = "1" Then
                'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text & "%'and lotto > 3 order by denominazione asc"

            Else
                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.complessi_immobiliari WHERE denominazione like '%" & Me.TxtDescInd.Text & "%'order by denominazione asc"

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

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        If Me.ListEdifci.SelectedValue.ToString <> "" Then
            Me.DrLComplesso.SelectedValue = Me.ListEdifci.SelectedValue.ToString
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.TextBox1.Value = 1
            Me.LblNoResult.Visible = False
            Me.CaricaEdificiComp()

        Else
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.LblNoResult.Visible = False
            Me.TextBox1.Value = 1
        End If
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        If par.IfEmpty(Me.TextBoxDescIndEd.Text, "Null") <> "Null" Then
            Me.ListEdifici2.Items.Clear()

            If Session("PED2_ESTERNA") = "1" Then
                If Me.DrLComplesso.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%'and lotto > 3 order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.DrLComplesso.SelectedValue.ToString & " order by denominazione asc"

                End If
            Else
                If Me.DrLComplesso.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%'order by denominazione asc"
                Else
                    par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TextBoxDescIndEd.Text & "%' AND EDIFICI.ID_COMPLESSO = " & Me.DrLComplesso.SelectedValue.ToString & "order by denominazione asc"

                End If

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                ListEdifici2.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("ID"), -1)))
            End While
        End If
        If ListEdifici2.Items.Count = 0 Then
            Me.LblNoresult2.Visible = True
        Else
            Me.LblNoresult2.Visible = False
        End If
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Me.TextBox2.Value = 2
    End Sub

    Protected Sub ImageButton3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        If Me.ListEdifici2.SelectedValue.ToString <> "" Then
            Me.cmbEdificio.SelectedValue = Me.ListEdifici2.SelectedValue.ToString
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.TextBox2.Value = 1
            Me.LblNoresult2.Visible = False
        Else
            Me.TxtDescInd.Text = ""
            Me.ListEdifci.Items.Clear()
            Me.LblNoresult2.Visible = False
            Me.TextBox2.Value = 1
        End If
    End Sub
End Class
