
Partial Class CENSIMENTO_RicercaUI
    Inherits PageSetIdMode
    Dim passato As String = ""
    Dim edificio As String = ""
    Dim indirizzo As String = ""
    Dim civico As String = ""
    Dim interno As String = ""
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
                Dim gest As Integer = 0
                Session.Remove("TIPOLOGIA")
                Session.Remove("PED")
                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                    Exit Sub
                End If
                CaricaZone()
                CaricaComplessi()


                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader



                DrlProgrEventi.Items.Add(New ListItem(" ", "-1"))
                par.cmd.CommandText = "select * from SISCOM_MI.PROGRAMMAZIONE_INTERVENTI order by descrizione asc"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    DrlProgrEventi.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("id"), "null")))
                End While
                myReader2.Close()



                'par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipologia_unita_immobiliari order by descrizione asc"

                'myReader2 = par.cmd.ExecuteReader()
                'While myReader2.Read
                '    cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
                'End While
                'myReader2.Close()

                par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipologia_unita_immobiliari order by descrizione asc"
                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read
                    '    cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
                    Me.chkListTipologie.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), "-1")))
                End While
                myReader2.Close()


                'Destinazione d'uso aggiunta marco 19/04/2012
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.DESTINAZIONI_USO_UI"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Me.DrlDestUso.Items.Clear()
                Me.DrlDestUso.Items.Add(New ListItem("", -1))
                While myReader1.Read
                    DrlDestUso.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                DrlDestUso.SelectedValue = "-1"
                myReader1.Close()


                '******************disponibilità*************
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.TIPO_DISPONIBILITA"
                myReader2 = par.cmd.ExecuteReader
                DrLDisponib.Items.Add(New ListItem(" ", -1))
                While myReader2.Read
                    DrLDisponib.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader2("cod"), -1)))
                End While
                'DrLDisponib.SelectedValue = "INDEF"
                myReader2.Close()


                '****************SEDE TERRITORIALE*****************
                par.caricaComboBox("select id,nome from siscom_mi.tab_filiali where exists (select * from siscom_mi.filiali_ui where id_filiale = tab_filiali.id)  order by nome asc", cmbSedeTerr, "ID", "NOME", True)



                '***************ZONA OSMI*************************
                par.caricaComboBox("select id ,descrizione from siscom_mi.tab_zona_osmi order by descrizione asc", cmbZonaOsmi, "ID", "DESCRIZIONE", True)

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                DrLComplesso.Text = "-1"
                CaricaEdifici()
                CaricaIndirizzi()
                cmbAscensore.Items.Add(New ListItem(" ", -1))
                cmbAscensore.Items.Add(New ListItem("NO", 0))
                cmbAscensore.Items.Add(New ListItem("SI", 1))
                Me.cmbAscensore.SelectedValue = "-1"

                cmbCondominio.Items.Add(New ListItem(" ", -1))
                cmbCondominio.Items.Add(New ListItem("SI", 1))
                cmbCondominio.Items.Add(New ListItem("NO", 0))

                cmbRendita.Items.Add(New ListItem(" ", -1))
                cmbRendita.Items.Add(New ListItem("NO", 0))
                cmbRendita.Items.Add(New ListItem("SI", 1))
                cmbRendita.SelectedValue = "-1"

                cmbHandicap.Items.Add(New ListItem(" ", -1))
                cmbHandicap.Items.Add(New ListItem("SI", 1))
                cmbHandicap.Items.Add(New ListItem("NO", 0))
                Me.cmbHandicap.SelectedValue = "-1"
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


    Private Sub CaricaComplessi(Optional ByVal FiltZona As String = "")
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        Me.DrLComplesso.Items.Clear()
        '***CARICAMENTO LISTA COMPLESSI
        DrLComplesso.Items.Add(New ListItem(" ", -1))

        If Session("PED2_ESTERNA") = "1" Then
            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 AND COMPLESSI_IMMOBILIARI.ID<>1 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id " & FiltZona & " order by denominazione asc "
        Else
            par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id AND COMPLESSI_IMMOBILIARI.ID<>1 " & FiltZona & " order by denominazione asc "
        End If

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



    End Sub
    Private Sub ControllaSeCondominio()
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.EDIFICI WHERE ID = " & Me.cmbEdificio.SelectedValue & " AND CONDOMINIO = 0"
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read Then
            Me.cmbCondominio.Enabled = False
        Else
            Me.cmbCondominio.Enabled = True
        End If
        myReader.Close()
        '*********************CHIUSURA CONNESSIONE**********************
        par.cmd.Dispose()
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    End Sub
    Private Sub CaricaIndirizzi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Me.cmbIndirizzo.Items.Clear()
            cmbIndirizzo.Items.Add(" ")
            par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI where UNITA_IMMOBILIARI.ID_EDIFICIO <> 1) order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
            End While
            myReader1.Close()

            cmbIndirizzo.Text = " "

            cmbCivico.Items.Clear()

            'If cmbIndirizzo.Text <> " " Then


            '    par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
            '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    While myReader2.Read
            '        cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
            '    End While
            '    myReader2.Close()
            'End If

            'cmbInterno.Items.Clear()
            'If cmbCivico.Text <> "" Then
            '    cmbInterno.Items.Add((New ListItem(" ", "-1")))

            '    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
            '    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    While myReader3.Read
            '        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
            '    End While
            '    myReader3.Close()
            'End If
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
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean = False
        Dim I As Integer = 0
        Dim TIPOLOGIA As String = ""
        Dim Scala As String = ""
        Dim civInd As String = ""
        Dim sedeTer As Integer = -1

        If Me.cmbEdificio.Items.Count > 0 Then
            If Me.cmbEdificio.Text <> "-1" Then
                edificio = Me.cmbEdificio.SelectedValue.ToString
            End If
        Else
            Response.Write("<SCRIPT>alert('Non è stato caricato alcun edificio!');</SCRIPT>")
            Exit Sub
        End If



        If par.IfEmpty(Me.cmbIndirizzo.SelectedValue.ToString, "Null") <> "Null" And Me.cmbIndirizzo.SelectedValue.ToString <> "" Then
            If Me.cmbCivico.SelectedValue <> "-1" And Me.cmbCivico.SelectedValue <> "" Then
                civInd = Me.cmbCivico.SelectedValue.ToString
            ElseIf Me.cmbIndirizzo.SelectedItem.Text <> "" Then
                indirizzo = Me.cmbIndirizzo.SelectedItem.Text
            End If

            If Me.cmbScala.SelectedValue <> "-1" Then
                Scala = Me.cmbScala.SelectedValue.ToString
            End If
            If Me.cmbInterno.Items.Count > 0 Then
                If par.IfEmpty(Me.cmbInterno.SelectedItem.ToString, "Null") <> "Null" Then
                    interno = Me.cmbInterno.SelectedItem.ToString
                End If
            End If
        End If

        indirizzo = Me.cmbIndirizzo.SelectedItem.Text
        'If Me.cmbCivico.Text <> -1 OrElse Me.cmbCivico.Text <> " " Then
        '    civico = Me.cmbCivico.SelectedItem.Text
        'End If
        If Me.cmbSedeTerr.SelectedValue <> -1 Then
            sedeTer = Me.cmbSedeTerr.SelectedValue
        End If
        For I = 0 To chkListTipologie.Items.Count - 1
            If Me.chkListTipologie.Items(I).Selected = True Then
                If TIPOLOGIA <> "" Then TIPOLOGIA = TIPOLOGIA & " OR "
                TIPOLOGIA = TIPOLOGIA & "UNITA_IMMOBILIARI.COD_TIPOLOGIA='" & Me.chkListTipologie.Items(I).Value & "'"
            End If
        Next
        If TIPOLOGIA <> "" Then
            Session.Add("TIPOLOGIA", TIPOLOGIA)
        End If
        

        Response.Redirect("RisultatiUI.aspx?REN=" & cmbRendita.SelectedItem.Value & "&SNDA=" & par.Cripta(TxtSupDa.Text) & "&SNA=" & par.Cripta(TxtSupA.Text) & "&PRG=" & DrlProgrEventi.SelectedItem.Value & "&DEST=" & DrlDestUso.SelectedValue & "&E=" & edificio & "&IDIND=" & civInd & "&IND=" & par.VaroleDaPassare(par.PulisciStrSql(indirizzo)) & "&COMP=" & par.VaroleDaPassare(par.PulisciStrSql(Me.DrLComplesso.SelectedValue.ToString)) & "&INT=" & interno & "&SCAL=" & Scala & "&ASC=" & Me.cmbAscensore.SelectedValue.ToString & "&DISP=" & par.VaroleDaPassare(Me.DrLDisponib.SelectedValue.ToString) & "&COND=" & par.VaroleDaPassare(Me.cmbCondominio.SelectedValue) & "&HAN=" & par.VaroleDaPassare(Me.cmbHandicap.SelectedValue) & "&SEDE=" & par.VaroleDaPassare(sedeTer) & "&ZONA=" & Me.cmbZona.SelectedValue & "&ZOSMI=" & Me.cmbZonaOsmi.SelectedValue)

    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.Text <> "-1" Then
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.filtraindirizzi()
            ControllaSeCondominio()
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
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
                ElseIf Me.DrLComplesso.SelectedValue <> "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO = " & Me.DrLComplesso.SelectedValue & ") order by descrizione asc"
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
                End While
                myReader1.Close()

                cmbIndirizzo.Text = " "

                cmbCivico.Items.Clear()

                'If cmbIndirizzo.Text <> " " Then


                '    par.cmd.CommandText = "SELECT id,civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "' order by civico asc"
                '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '    While myReader2.Read
                '        cmbCivico.Items.Add(New ListItem(par.IfNull(myReader2("civico"), " "), par.IfNull(myReader2("id"), "-1")))
                '    End While
                '    myReader2.Close()
                'End If

                'cmbInterno.Items.Clear()
                'If cmbCivico.Text <> "" Then
                '    cmbInterno.Items.Add((New ListItem(" ", "-1")))

                '    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari,SISCOM_MI.edifici where edifici.id_indirizzo_principale=" & cmbCivico.SelectedValue & " and edifici.id=unita_immobiliari.id_edificio  order by unita_immobiliari.interno asc"
                '    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '    While myReader3.Read
                '        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), "-1"))))
                '    End While
                '    myReader3.Close()
                'End If
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
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND EDIFICI.ID<>1 AND COMPLESSI_IMMOBILIARI.LOTTO > 3 " & filtZona & " order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND EDIFICI.ID<>1 " & filtZona & " order by denominazione asc"

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
        Dim filtro As String = ""
        If Me.cmbZona.SelectedValue <> "-1" Then
            filtro = " and edifici.id_zona = " & Me.cmbZona.SelectedValue & " "
        End If

        If Me.DrLComplesso.Text <> "-1" Then
            Me.cmbEdificio.Items.Clear()
            Me.CaricaEdificiComp(filtro)
            Me.filtraindirizzi()
        Else
            Me.cmbEdificio.Items.Clear()
            CaricaEdifici(filtro)
            Me.CaricaIndirizzi()
        End If
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.TxtDescInd.Text = ""
    End Sub
    Private Sub CaricaEdificiComp(Optional ByVal filtZona As String = "")
        Try


            Dim StringaSql As String = ""
            Dim condizione As String = ""
            Dim wherecond As Boolean = False
            Dim primo As Boolean
            Dim concatena As String = ""

            Dim gest As Integer = 3
            If Me.DrLComplesso.Text <> "-1" OrElse Me.cmbAscensore.SelectedValue <> "-1" Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                '****CARICA LISTA EDIFICI
                If Me.DrLComplesso.SelectedValue <> "-1" Then
                    StringaSql = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.EDIFICI  where id_complesso = " & Me.DrLComplesso.SelectedValue.ToString & "  " & filtZona
                    wherecond = True

                Else
                    If Session("PED2_ESTERNA") = "1" Then
                        StringaSql = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.EDIFICI where lotto > 3  and id <> 1 " & filtZona
                        wherecond = True

                    Else
                        StringaSql = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.EDIFICI  where id <> 1  " & filtZona
                        primo = True

                    End If
                End If
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

                If wherecond = False Then
                    StringaSql = StringaSql & " WHERE " & condizione & " order by denominazione asc"
                Else
                    StringaSql = StringaSql & condizione & " order by denominazione asc"

                End If

                par.cmd.CommandText = StringaSql
                cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
                End While
                myReader1.Close()
                cmbEdificio.Text = "-1"
                cmbEdificio.Items.Add(New ListItem(" ", -1))
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


            ' ''CONDIZIONE CONDOMINIO
            ''If cmbCondominio.SelectedValue <> "-1" Then
            ''    condizione = condizione & " AND EDIFICI.CONDOMINIO = " & cmbCondominio.SelectedValue.ToString
            ''End If

            StringaSql = StringaSql & condizione & " order by denominazione asc "

            par.cmd.CommandText = StringaSql
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
                'cmbInterno.Items.Clear()
                If cmbCivico.Text <> "" Then
                    Me.cmbScala.Items.Clear()
                    cmbScala.Items.Add(New ListItem(" ", "-1"))
                    If cmbCivico.SelectedValue <> "" Then
                        '**************MODIFICA 08/07/2010 PER ELIMINARE I DOPPI CIVICI
                        par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')) order by descrizione asc"
                        '                        par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = " & Me.cmbCivico.SelectedValue.ToString & " ) order by descrizione asc"
                        Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        While myReader3.Read
                            cmbScala.Items.Add(New ListItem(par.IfNull(myReader3("descrizione"), " "), par.IfNull(myReader3("ID"), " ")))
                        End While
                    End If

                End If


                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If Me.cmbCivico.SelectedValue <> "" Then
                    '**************MODIFICA 08/07/2010 PER ELIMINARE I DOPPI CIVICI
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    '                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
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
            Me.ListEdifci.Items.Clear()
            Me.TxtDescInd.Text = ""

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
        Try
            If cmbIndirizzo.Text <> "" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Dim CondEdifici As String
                cmbCivico.Items.Clear()

                '****MODIFICHE 08/07/2010 PER NON VISUALIZZARE DOPPIONI IN CIVICI**********
                If Me.cmbEdificio.SelectedValue <> -1 Then
                    CondEdifici = "ID_EDIFICIO =" & Me.cmbEdificio.SelectedValue
                Else
                    CondEdifici = "ID_EDIFICIO <> 1"
                End If
                par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT id_indirizzo FROM siscom_mi.unita_immobiliari WHERE ID_EDIFICIO<>1 AND " & CondEdifici & " ) order by civico asc"
                'par.cmd.CommandText = "SELECT DISTINCT ID, civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT id_indirizzo FROM siscom_mi.unita_immobiliari WHERE ID_EDIFICIO <> 1 ) order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("civico"), " ")))
                End While
                myReader1.Close()

                Me.cmbScala.Items.Clear()
                cmbScala.Items.Add(New ListItem(" ", "-1"))
                If cmbCivico.SelectedValue <> "" Then
                    '****MODIFICHE 08/07/2010 PER NON VISUALIZZARE DOPPIONI IN CIVICI**********
                    If Me.cmbEdificio.SelectedValue <> -1 Then
                        CondEdifici = " AND ID_EDIFICIO =" & Me.cmbEdificio.SelectedValue
                    Else
                        CondEdifici = ""
                    End If
                    par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO<>1 AND UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')" & CondEdifici & ") order by descrizione asc"
                    'par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO = " & Me.cmbCivico.SelectedValue.ToString & " ) order by descrizione asc"
                    myReader1 = par.cmd.ExecuteReader
                    While myReader1.Read
                        cmbScala.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("ID"), " ")))
                    End While
                End If


                cmbInterno.Items.Clear()
                cmbInterno.Items.Add(New ListItem(" ", "-1"))
                If Me.cmbCivico.SelectedValue <> "" Then
                    '****MODIFICHE 08/07/2010 PER NON VISUALIZZARE DOPPIONI IN CIVICI**********
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where ID_EDIFICIO<>1 AND unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    'par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    While myReader3.Read
                        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), ""))))
                    End While
                    myReader3.Close()
                End If

                'cmbInterno.Items.Clear()
                'cmbInterno.Items.Add(New ListItem(" ", "-1"))
                'If Me.cmbScala.SelectedValue <> "" Then
                '    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari WHERE ID_SCALA = " & Me.cmbScala.SelectedValue.ToString & " order by unita_immobiliari.interno asc"
                '    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '    While myReader3.Read
                '        cmbInterno.Items.Add((New ListItem(par.IfNull(myReader3("interno"), " "), par.IfNull(myReader3("interno"), ""))))
                '    End While
                '    myReader3.Close()
                'End If


                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Me.TextBox1.Value = 1
            Me.ListEdifci.Items.Clear()
            Me.TxtDescInd.Text = ""

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            par.OracleConn.Close()
        End Try
    End Sub


    Protected Sub cmbAscensore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAscensore.SelectedIndexChanged
        CaricaEdificiComp()
        Me.TextBox1.Value = 1
        Me.ListEdifci.Items.Clear()
        Me.TxtDescInd.Text = ""

    End Sub

    Protected Sub btnSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSelezionaTutto.Click
        If Selezionati = "" Then
            Selezionati = 1
        Else
            Selezionati = ""
        End If
        Dim a As Integer
        Dim i As Integer = 0
        If Selezionati <> "" Then
            a = chkListTipologie.Items.Count.ToString
            While i < a
                Me.chkListTipologie.Items(i).Selected = True
                i = i + 1
            End While
        Else
            a = chkListTipologie.Items.Count.ToString
            While i < a
                Me.chkListTipologie.Items(i).Selected = False
                i = i + 1
            End While
        End If
    End Sub
    Public Property Selezionati() As String
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CStr(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Selezionati") = value
        End Set

    End Property

    Protected Sub cmbScala_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbScala.SelectedIndexChanged
        If Me.cmbScala.SelectedValue <> "-1" Then
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            cmbInterno.Items.Clear()
            cmbInterno.Items.Add(New ListItem(" ", "-1"))
            If Me.cmbScala.SelectedValue <> "" Then
                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari WHERE ID_EDIFICIO<>1 AND ID_SCALA = " & Me.cmbScala.SelectedValue.ToString & " order by unita_immobiliari.interno asc"
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
                '                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where ID_EDIFICIO<>1 AND unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
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
