
Partial Class CENSIMENTO_RicercaEdifici
    Inherits PageSetIdMode
    Dim sStringaSql As String = ""
    Dim par As New CM.Global
    Dim edificio As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try


            If Not IsPostBack Then
                Me.cmbIndirizzo.Items.Clear()
                Me.cmbCivico.Items.Clear()
                '****VARIABBILE DA DECIDERE SE ADOTTARALA PER FILTRAGGIO OPERATORE *****
                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                    Exit Sub
                End If

                CaricaZone()
                CaricaComplessi()
                CaricaEdifici()

                'cmbEdificio.Text = "-1"
                CaricaTuttiIndirizzi()

                cmbAscensore.Items.Add(New ListItem(" ", -1))
                cmbAscensore.Items.Add(New ListItem("NO", 0))
                cmbAscensore.Items.Add(New ListItem("SI", 1))
                Me.cmbAscensore.SelectedValue = "-1"

                cmbCondominio.Items.Add(New ListItem(" ", -1))
                cmbCondominio.Items.Add(New ListItem("NO", 0))
                cmbCondominio.Items.Add(New ListItem("SI", 1))
                Me.cmbCondominio.SelectedValue = "-1"


            Else
                Exit Sub
            End If
            TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub CaricaComplessi(Optional filtroZona As String = "")
        Me.cmbComplesso.Items.Clear()
        '*********************APERTURA CONNESSIONE**********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        cmbComplesso.Items.Add(New ListItem(" ", -1))
        If Session("PED2_ESTERNA") = "1" Then
            'par.cmd.CommandText = "SELECT COMPLESSI_IMMOBILIARI.ID, COMPLESSI_IMMOBILIARI.DENOMINAZIONE , INDIRIZZI.DESCRIZIONE, INDIRIZZI.CIVICO FROM SISCOM_MI.COMPLESSI_IMMOBILIARI, SISCOM_MI.INDIRIZZI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID order by DENOMINAZIONE asc"
            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.id<>1 " & filtroZona & " order by denominazione asc "
        Else
            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id and complessi_immobiliari.id<>1 " & filtroZona & " order by denominazione asc "

        End If
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        While myReader1.Read
            'cmbComplesso.Items.Add(New ListItem(par.IfNull("cod." & myReader1("cod_complesso"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))

        End While





        myReader1.Close()

        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub
    Private Sub CaricaZone()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        par.cmd.CommandText = "SELECT * FROM ZONA_ALER order by zona asc"
        myReader1 = par.cmd.ExecuteReader
        Me.cmbZona.Items.Add(New ListItem(" ", -1))

        While myReader1.Read
            cmbZona.Items.Add(New ListItem(par.IfNull(myReader1("zona"), " "), par.IfNull(myReader1("cod"), -1)))
        End While
        myReader1.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub

    Private Sub CaricaEdifici(Optional ByVal filtZona As String = "")
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
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 and complessi_immobiliari.id<>1 " & filtZona & " order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and complessi_immobiliari.id<>1 " & filtZona & " order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

            End While
            Me.btnCerca.Visible = True
            Me.btnVisualizza.Visible = False

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
            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi where id in (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID)order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
            While myReader1.Read
                cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " ")))
            End While
            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            cmbIndirizzo.Text = "-1"
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub
    'Private Function CaricaTuttiIndirizzi()

    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Exit Function
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi where id in (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI)order by descrizione asc"
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '    Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
    '    While myReader1.Read
    '        cmbIndirizzo.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " ")))
    '    End While
    '    myReader1.Close()
    '    cmbIndirizzo.Text = "-1"

    'End Function

    'Private Function CaricaIndirizzi()
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
    '    Dim idIndirizzoEdificio As String

    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Exit Function

    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    If cmbEdificio.Text <> "" Then
    '        'Prima era così
    '        'par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI) order by descrizione asc"
    '        'Adesso è così
    '        par.cmd.CommandText = "SELECT ID_INDIRIZZO_PRINCIPALE FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.cmbEdificio.SelectedValue.ToString
    '        myReader1 = par.cmd.ExecuteReader
    '        While myReader1.Read
    '            idIndirizzoEdificio = myReader1(0)
    '        End While
    '        myReader1.Close()



    '        par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE SISCOM_MI.INDIRIZZI.ID = " & idIndirizzoEdificio & " order by descrizione asc"
    '        myReader1 = par.cmd.ExecuteReader
    '        Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))
    '        While myReader1.Read
    '            cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
    '        End While
    '        myReader1.Close()
    '        par.OracleConn.Close()

    '        cmbCivico.Items.Clear()
    '        cmbIndirizzo.Text = "-1"

    '    End If

    'End Function

    'Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
    '    If cmbEdificio.Text <> "" Then
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            Exit Sub
    '        Else
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If

    '        cmbCivico.Items.Clear()

    '        par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        Me.cmbCivico.Items.Add(New ListItem(" ", -1))
    '        While myReader1.Read
    '            cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("id"), "-1")))
    '        End While
    '        myReader1.Close()
    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Me.cmbCivico.Text = "-1"

    '    End If
    'End Sub

    'Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
    '    If Me.cmbEdificio.Text <> "-1" Then
    '        Me.cmbIndirizzo.Items.Clear()
    '        Me.cmbCivico.Items.Clear()
    '        Me.CaricaIndirizzi()
    '    Else
    '        Me.cmbIndirizzo.Items.Clear()
    '        Me.cmbCivico.Items.Clear()
    '        Me.CaricaTuttiIndirizzi()
    '    End If
    'End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub
    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click

        Try

            Dim bTrovato As Boolean = False
            If Me.cmbEdificio.Items.Count > 0 Then
                If Me.cmbEdificio.Text <> "-1" Then
                    edificio = Me.cmbEdificio.SelectedValue.ToString
                End If
            End If
            'If Me.cmbIndirizzo.Text <> "-1" Then
            '    indirizzo = Me.cmbIndirizzo.SelectedItem.Text
            '    If Me.cmbCivico.Text <> "-1" Then
            '        civico = Me.cmbCivico.SelectedItem.Text
            '    End If
            'End If
            Session.Add("CENSIMENTO", sStringaSql)
            'Response.Redirect("RisultatiEdifici.aspx?E=" & edificio & "&C=" & Me.cmbComplesso.SelectedValue.ToString & "&I=" & Me.cmbIndirizzo.SelectedValue.ToString & "ASC=" & Me.cmbAscensore.SelectedValue.ToString)
            Response.Redirect("RisultatiEdifici.aspx?E=" & edificio & "&C=" & Me.cmbComplesso.SelectedValue.ToString & "&ASC=" & Me.cmbAscensore.SelectedValue.ToString & "&COND=" & Me.cmbCondominio.SelectedValue.ToString & "&ZONA=" & Me.cmbZona.SelectedValue)

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged
        Dim filtZona As String = ""
        If Me.cmbZona.SelectedValue <> "-1" Then
            filtZona = " and edifici.id_zona = " & Me.cmbZona.SelectedValue & " "
        Else
            filtZona = ""
        End If

        If Me.cmbComplesso.SelectedValue <> "-1" Then

            FiltraEdifici(filtZona)
        Else
            CaricaEdifici(filtZona)
        End If
        Me.TextBox1.Value = ""
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()
    End Sub
    Private Sub FiltraEdifici(Optional ByVal FiltZone As String = "")
        Try
            If Me.cmbComplesso.SelectedValue <> "-1" OrElse Me.cmbAscensore.SelectedValue <> "-1" OrElse Me.cmbCondominio.SelectedValue <> "-1" Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Dim wherecond As Boolean = False
                Dim primo As Boolean
                Dim concatena As String = ""

                Dim condizione As String = ""
                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))
                Dim StringaSql As String = ""

                If Me.cmbComplesso.SelectedValue <> "-1" Then
                    StringaSql = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString
                    wherecond = True
                Else
                    If Session("PED2_ESTERNA") = "1" Then
                        StringaSql = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where lotto > 3 "
                        primo = False
                        wherecond = True
                    Else
                        StringaSql = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici "
                        primo = True
                    End If
                End If
                'CONDIZIONE ASCENSORE
                If Me.cmbAscensore.SelectedValue = "-1" Then
                    condizione = ""
                ElseIf Me.cmbAscensore.SelectedValue = "0" Then
                    If primo = False Then
                        concatena = " AND "
                    Else
                        concatena = " "
                        primo = False
                    End If
                    condizione = concatena & " (EDIFICI.NUM_ASCENSORI = 0 OR EDIFICI.NUM_ASCENSORI IS NULL) "

                ElseIf Me.cmbAscensore.SelectedValue = "1" Then
                    If primo = False Then
                        concatena = " AND "
                    Else
                        concatena = " "
                        primo = False
                    End If
                    condizione = concatena & " EDIFICI.NUM_ASCENSORI > 0 "
                End If
                'CONDIZIONE CONDOMINIO
                If cmbCondominio.SelectedValue <> "-1" Then
                    If primo = False Then
                        concatena = " AND "
                    Else
                        concatena = " "
                        primo = False
                    End If
                    condizione = condizione & concatena & " EDIFICI.CONDOMINIO = " & cmbCondominio.SelectedValue.ToString
                End If
                If wherecond = False Then
                    StringaSql = StringaSql & " WHERE " & condizione & " order by denominazione asc"
                Else
                    StringaSql = StringaSql & condizione & " order by denominazione asc"

                End If

                par.cmd.CommandText = StringaSql

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    'cmbEdificio.Items.Add(New ListItem("cod." & par.IfNull(myReader1("COD_EDIFICIO"), " ") & "- -" & par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))

                End While

                myReader1.Close()
                Me.btnCerca.Visible = True
                Me.btnVisualizza.Visible = False
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                CaricaEdifici()
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub


    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim condizione As String = ""
            Dim StringaSql As String = ""
            'Dim wherecond As Boolean = False
            'Dim primo As Boolean = False

            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
                Me.ListEdifci.Items.Clear()
                If Session("PED2_ESTERNA") = "1" Then
                    StringaSql = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 "
                Else
                    StringaSql = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' "
                End If

                'CONDIZIONE ASCENSORE
                If Me.cmbAscensore.SelectedValue = "-1" Then
                    condizione = ""
                ElseIf Me.cmbAscensore.SelectedValue = "0" Then
                    condizione = " AND (EDIFICI.NUM_ASCENSORI = 0 OR EDIFICI.NUM_ASCENSORI IS NULL) "

                ElseIf Me.cmbAscensore.SelectedValue = "1" Then
                    condizione = " AND EDIFICI.NUM_ASCENSORI > 0 "
                End If


                'CONDIZIONE CONDOMINIO
                If cmbCondominio.SelectedValue <> "-1" Then
                    condizione = condizione & " AND EDIFICI.CONDOMINIO = " & cmbCondominio.SelectedValue.ToString
                End If

                StringaSql = StringaSql & condizione & " order by denominazione asc "

                par.cmd.CommandText = StringaSql
                ''Ascensore = -1 tutto come prima
                'If Me.cmbAscensore.SelectedValue = "-1" Then
                '    If Session("PED2_ESTERNA") = "1" Then
                '        If Me.cmbComplesso.SelectedValue = "-1" Then
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 order by denominazione asc"
                '        Else
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " order by denominazione asc"
                '        End If
                '    Else
                '        If Me.cmbComplesso.SelectedValue = "-1" Then
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'order by denominazione asc"
                '        Else
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & "order by denominazione asc"

                '        End If
                '    End If
                'ElseIf Me.cmbAscensore.SelectedValue = 0 Then
                '    'Ascensore = 0 edifici senza ascensore
                '    condAscensore = " and (edifici.num_ascensori = 0 or edifici.num_ascensori is null) "
                '    If Session("PED2_ESTERNA") = "1" Then
                '        If Me.cmbComplesso.SelectedValue = "-1" Then
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 " & condAscensore & "order by denominazione asc"
                '        Else
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " " & condAscensore & "  order by denominazione asc"
                '        End If
                '    Else
                '        If Me.cmbComplesso.SelectedValue = "-1" Then
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' " & condAscensore & " order by denominazione asc"
                '        Else
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " " & condAscensore & " order by denominazione asc"

                '        End If
                '    End If


                'ElseIf Me.cmbAscensore.SelectedValue = 1 Then
                '    'Ascensore = 1 edifici con ascensore
                '    condAscensore = " and edifici.num_ascensori > 0 "

                '    If Session("PED2_ESTERNA") = "1" Then
                '        If Me.cmbComplesso.SelectedValue = "-1" Then
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 " & condAscensore & " order by denominazione asc"
                '        Else
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%'and lotto > 3 AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " " & condAscensore & " order by denominazione asc"
                '        End If
                '    Else
                '        If Me.cmbComplesso.SelectedValue = "-1" Then
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' " & condAscensore & " order by denominazione asc"
                '        Else
                '            par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '%" & Me.TxtDescInd.Text.ToUpper & "%' AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue.ToString & " " & condAscensore & " order by denominazione asc"

                '        End If
                '    End If



                'End If

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
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub BtnConferma_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
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
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        Me.TextBox1.Value = ""
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()
        ControllaSelezione()
    End Sub

    Protected Sub cmbAscensore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAscensore.SelectedIndexChanged
        FiltraEdifici()
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.TxtDescInd.Text = ""

    End Sub

    Protected Sub cmbCondominio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCondominio.SelectedIndexChanged
        FiltraEdifici()
        Me.TextBox1.Value = ""
        Me.TxtDescInd.Text = ""
        Me.ListEdifci.Items.Clear()
    End Sub
    Private Sub ControllaSelezione()
        Try
            If Me.cmbEdificio.SelectedValue <> "-1" Then
                Me.btnCerca.Visible = False
                Me.btnVisualizza.Visible = True
            Else
                Me.btnVisualizza.Visible = False
                Me.btnCerca.Visible = True
            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try
    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        Response.Redirect("InserimentoEdifici.aspx?C=RicercaEdifici&EDIFICIO=" & Request.QueryString("E") & "&COMPLESSO=" & Request.QueryString("C") & "&ID=" & Me.cmbEdificio.SelectedValue)

    End Sub

    Protected Sub cmbZona_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbZona.SelectedIndexChanged
        If Me.cmbZona.SelectedValue <> "-1" Then
            CaricaComplessi(" and exists(select * from siscom_mi.edifici where id_zona = " & Me.cmbZona.SelectedValue & " and edifici.id_complesso = complessi_immobiliari.id)")
            CaricaEdifici(" and edifici.id_zona = " & Me.cmbZona.SelectedValue & " ")
        Else
            CaricaComplessi()
            CaricaEdifici()

        End If

    End Sub
End Class
