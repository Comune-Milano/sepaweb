
Partial Class PED_RicercaStatoUI
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try



            If Not IsPostBack Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                cmbComplesso.Items.Add(New ListItem(" ", -1))
                If Session("PED2_ESTERNA") = "1" Then
                    par.cmd.CommandText = "SELECT complessi_immobiliari.id,(denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico)as denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where lotto > 3 and complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "
                Else
                    par.cmd.CommandText = "SELECT complessi_immobiliari.id,(denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico)as denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id  order by denominazione asc "

                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While

                myReader1.Close()

                cmbTipologia.Items.Add(New ListItem(" ", "-1"))
                par.cmd.CommandText = "SELECT cod, descrizione FROM SISCOM_MI.tipologia_unita_immobiliari order by descrizione asc"

                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), " "), par.IfNull(myReader1("cod"), "-1")))
                End While
                myReader1.Close()

                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaEdifici()
                CaricaIndirizzi()


            End If
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


    Private Sub CaricaEdifici()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim gest As Integer = 0
            Me.cmbEdificio.Items.Clear()

            cmbEdificio.Items.Add(New ListItem(" ", -1))

            If Session("PED2_ESTERNA") = "1" Then
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,(EDIFICI.denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico) as denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.indirizzi WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND COMPLESSI_IMMOBILIARI.LOTTO > 3 and edifici.id_indirizzo_principale = indirizzi.id order by denominazione asc"
            Else
                par.cmd.CommandText = "SELECT distinct EDIFICI.id,(EDIFICI.denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico) as denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI,SISCOM_MI.indirizzi WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID AND edifici.id_indirizzo_principale = indirizzi.id order by denominazione asc"

            End If
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
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


    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")
    End Sub

    'Protected Sub cmbGestore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbGestore.SelectedIndexChanged
    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Exit Sub
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If

    '    cmbTipologia.Items.Clear()
    '    cmbProvenienza.Text = "-1"
    '    cmbComplesso.Items.Clear()
    '    cmbEdificio.Items.Clear()

    '    cmbTipologia.Items.Add(New ListItem(" ", "-1"))
    '    par.cmd.CommandText = "SELECT distinct cod_tipologia FROM SISCOM_MI.unita_immobiliari where substr(id,1,1)=" & cmbGestore.SelectedValue & " order by cod_tipologia asc"
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '    While myReader1.Read
    '        par.cmd.CommandText = "SELECT descrizione FROM SISCOM_MI.tipologia_unita_immobiliari where cod='" & par.IfNull(myReader1("cod_tipologia"), "-1") & "' order by descrizione asc"
    '        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader2.Read Then
    '            cmbTipologia.Items.Add(New ListItem(par.IfNull(myReader2("descrizione"), " "), par.IfNull(myReader1("cod_tipologia"), "-1")))
    '        End If
    '        myReader2.Close()
    '    End While
    '    myReader1.Close()
    '    par.OracleConn.Close()
    '    cmbTipologia.Text = "-1"
    'End Sub

    'Protected Sub cmbProvenienza_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProvenienza.SelectedIndexChanged
    '    If par.OracleConn.State = Data.ConnectionState.Open Then
    '        Exit Sub
    '    Else
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If

    '    'Select Case cmbGestore.SelectedValue
    '    '    Case 1
    '    '        Gestore()
    '    '    Case 2

    '    '    Case 3
    '    'End Select

    '    cmbComplesso.Items.Clear()
    '    cmbEdificio.Items.Clear()

    '    cmbComplesso.Items.Add(New ListItem(" ", -1))
    '    par.cmd.CommandText = "SELECT id,denominazione FROM SISCOM_MI.complessi_immobiliari where substr(id,1,1)=" & cmbGestore.SelectedValue & " and  cod_tipologia_provenienza='" & cmbProvenienza.SelectedValue & "' order by denominazione asc"
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '    While myReader1.Read
    '        cmbComplesso.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
    '    End While
    '    myReader1.Close()
    '    par.OracleConn.Close()
    '    cmbComplesso.Text = "-1"
    'End Sub

    Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbComplesso.SelectedIndexChanged

        If Me.cmbComplesso.SelectedValue <> "-1" Then
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.cmbScala.Items.Clear()
            Me.cmbInterno.Items.Clear()
            FiltraEdifici()
            Me.filtraindirizzi()
        Else
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.cmbScala.Items.Clear()
            Me.cmbInterno.Items.Clear()
            CaricaEdifici()
        End If


    End Sub
    Private Sub FiltraEdifici()
        Try
            If Me.cmbComplesso.SelectedValue <> "-1" Then
                '*********************APERTURA CONNESSIONE**********************
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If

                Me.cmbEdificio.Items.Clear()
                cmbEdificio.Items.Add(New ListItem(" ", -1))

                par.cmd.CommandText = "SELECT distinct EDIFICI.id,(EDIFICI.denominazione||'- -'||indirizzi.descrizione||','||indirizzi.civico) as denominazione  FROM SISCOM_MI.edifici, SISCOM_MI.indirizzi where id_complesso = " & Me.cmbComplesso.SelectedValue.ToString & " and edifici.id_indirizzo_principale = indirizzi.id order by denominazione asc"

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
                End While


                myReader1.Close()

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

    Private Function IfEmpty(ByVal v As Object, ByVal s As Object) As Object
        If v = "" Or v = " " Or UCase(v) = "NOT FOUND" Then
            IfEmpty = s
        Else
            IfEmpty = v
        End If
    End Function

    Protected Sub btnCerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCerca.Click
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String
        Dim TIPOLOGIA As String = ""
        Dim Scala As String = ""
        Dim civInd As String = ""
        Dim Indirizzo As String = ""
        Dim interno As String = ""

        bTrovato = False
        sStringaSql = ""

        Dim condizione As String = ""


        If IfEmpty(cmbTipologia.Text, "") <> "" And cmbTipologia.Text <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = cmbTipologia.SelectedValue
            If InStr(sValore, "*") Then
                sCompara = " = "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " unita_immobiliari.cod_tipologia" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If


        If IfEmpty(cmbComplesso.Text, "") <> "" And cmbComplesso.Text <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = cmbComplesso.SelectedValue
            If InStr(sValore, "*") Then
                sCompara = " = "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " edifici.id_complesso" & sCompara & "" & par.PulisciStrSql(sValore) & " "
        End If

        If IfEmpty(cmbEdificio.Text, "") <> "" And cmbEdificio.Text <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = cmbEdificio.SelectedValue
            If InStr(sValore, "*") Then
                sCompara = " = "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " unita_immobiliari.id_edificio" & sCompara & "" & par.PulisciStrSql(sValore) & " "
        End If

        If IfEmpty(cmbEdificio.Text, "") <> "" And cmbEdificio.Text <> "-1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = cmbEdificio.SelectedValue
            If InStr(sValore, "*") Then
                sCompara = " = "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " edifici.id" & sCompara & "" & par.PulisciStrSql(sValore) & " "
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



        '*******MODIFICA PER LA RICERCA ELIMINANDO I DOPPIONI DEI CIVICI
        If par.IfEmpty(Indirizzo, "Null") <> "Null" Then
            sValore = Indirizzo
            'condizione = condizione & "AND EDIFICI.ID_INDIRIZZO_PRINCIPALE IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (par.PulisciStrSql(sValore)) & "' "
            condizione = condizione & "AND unita_immobiliari.ID_INDIRIZZO IN (SELECT SISCOM_MI.INDIRIZZI.ID FROM SISCOM_MI.INDIRIZZI WHERE SISCOM_MI.INDIRIZZI.DESCRIZIONE = '" & (par.PulisciStrSql(sValore)) & "' "
            If par.IfEmpty(civInd, "Null") <> "Null" Then
                sValore = civInd
                condizione = condizione & "AND SISCOM_MI.INDIRIZZI.CIVICO = '" & sValore & "'"
            End If
            condizione = condizione & ")"
        End If

        If par.IfEmpty(Scala, "Null") <> "Null" Then
            sValore = Scala
            condizione = condizione & " AND UNITA_IMMOBILIARI.ID_SCALA = " & Scala
        End If


        If par.IfEmpty(interno, "Null") <> "Null" Then
            sValore = interno
            condizione = condizione & " AND UNITA_IMMOBILIARI.INTERNO ='" & par.PulisciStrSql(sValore) & "' "
        End If



        If sStringaSql <> "" Then sStringaSql = " AND " & sStringaSql

        If condizione <> "" Then sStringaSql = sStringaSql & " " & condizione



        'sStringaSql = "select unita_immobiliari.id,unita_immobiliari.cod_unita_immobiliare,(select descrizione from siscom_mi.scale_edifici where id=unita_immobiliari.id_scala) as SCALA,piani.descrizione as ""piano"",identificativi_catastali.SUB,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,tipo_disponibilita.descrizione as ""disponibilita"",INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,tipologia_unita_immobiliari.descrizione AS tipologia from SISCOM_MI.tipo_disponibilita,SISCOM_MI.unita_immobiliari,SISCOM_MI.identificativi_catastali,SISCOM_MI.edifici,SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari,SISCOM_MI.piani,siscom_mi.tipologia_unita_immobiliari where unita_immobiliari.cod_tipo_disponibilita=tipo_disponibilita.cod (+) and unita_immobiliari.cod_tipo_disponibilita<>'VEND' and unita_immobiliari.id_piano=piani.id (+) and edifici.id_complesso=complessi_immobiliari.id (+) and unita_immobiliari.id_indirizzo=indirizzi.id (+) and UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) and unita_immobiliari.id_edificio=edifici.id (+) and unita_immobiliari.id_unita_principale is null and unita_immobiliari.id_edificio<>1 AND tipologia_unita_immobiliari.cod = unita_immobiliari.cod_tipologia " & sStringaSql & " ORDER BY civico asc,scala asc,piani.descrizione asc,interno asc"
        sStringaSql = "SELECT unita_immobiliari.ID,unita_immobiliari.cod_unita_immobiliare,(SELECT descrizione FROM siscom_mi.scale_edifici WHERE ID=unita_immobiliari.id_scala) AS SCALA,tipo_livello_piano.descrizione AS piano,identificativi_catastali.SUB,identificativi_catastali.FOGLIO,identificativi_catastali.NUMERO,tipo_disponibilita.descrizione AS disponibilita,INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,UNITA_IMMOBILIARI.INTERNO,tipologia_unita_immobiliari.descrizione AS tipologia FROM SISCOM_MI.tipo_disponibilita,SISCOM_MI.unita_immobiliari,SISCOM_MI.identificativi_catastali,SISCOM_MI.edifici,SISCOM_MI.INDIRIZZI,SISCOM_MI.complessi_immobiliari,SISCOM_MI.tipo_livello_piano,siscom_mi.tipologia_unita_immobiliari WHERE unita_immobiliari.cod_tipo_disponibilita=tipo_disponibilita.cod (+) AND unita_immobiliari.cod_tipo_disponibilita<>'VEND' AND tipo_livello_piano.cod=unita_immobiliari.cod_tipo_livello_piano (+) AND edifici.id_complesso=complessi_immobiliari.ID (+) AND unita_immobiliari.id_indirizzo=indirizzi.ID (+) AND UNITA_IMMOBILIARI.ID_CATASTALE=IDENTIFICATIVI_CATASTALI.ID (+) AND unita_immobiliari.id_edificio=edifici.ID (+) AND unita_immobiliari.id_unita_principale IS NULL AND unita_immobiliari.id_edificio<>1 AND tipologia_unita_immobiliari.cod = unita_immobiliari.cod_tipologia " & sStringaSql & " ORDER BY civico ASC,scala ASC,tipo_livello_piano.descrizione ASC,interno ASC"

        Dim Gestore As String = ""
        Dim Edificio As String = ""
        Dim Foglio As String = ""
        Dim Mappale As String = ""
        Dim Complesso As String = ""

        If cmbComplesso.SelectedIndex = -1 Then
            Complesso = "Tutti"
        Else
            Complesso = cmbComplesso.SelectedItem.Text
        End If

        'If cmbGestore.SelectedIndex = -1 Then
        '    Gestore = "Tutti"
        'Else
        '    Gestore = cmbGestore.SelectedItem.Text
        'End If

        If cmbEdificio.SelectedIndex = -1 Then
            Edificio = "Tutti"
        Else
            Edificio = cmbEdificio.SelectedItem.Text
        End If
        Session.Add("TITOLO", "Gestore: " & Gestore & " - Complesso: " & Complesso & "<p>Edificio: " & Edificio & "</p>Indirizzo: " & Indirizzo & " Civico: " & civInd & " Scala: " & Scala & " Interno: " & interno)
        Session.Add("EUI", sStringaSql)

        Response.Write("<script>window.open('ElencoUI.aspx','','');</script>")
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
                par.cmd.CommandText = "SELECT DISTINCT civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT id_indirizzo FROM siscom_mi.unita_immobiliari WHERE " & CondEdifici & " ) order by civico asc"
                'par.cmd.CommandText = "SELECT DISTINCT ID, civico FROM SISCOM_MI.indirizzi where descrizione='" & par.PulisciStrSql(cmbIndirizzo.Text) & "'AND ID IN ( SELECT id_indirizzo FROM siscom_mi.unita_immobiliari WHERE ID_EDIFICIO <> 1 ) order by civico asc"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbCivico.Items.Add(New ListItem(par.IfNull(myReader1("civico"), " "), par.IfNull(myReader1("civico"), " ")))
                End While
                myReader1.Close()
                cmbCivico.Items.Add(New ListItem(" ", "-1"))
                cmbCivico.Items.FindByValue("-1").Selected = True

                Me.cmbScala.Items.Clear()
                cmbScala.Items.Add(New ListItem(" ", "-1"))
                If cmbCivico.SelectedValue <> "" Then
                    '****MODIFICHE 08/07/2010 PER NON VISUALIZZARE DOPPIONI IN CIVICI**********
                    If Me.cmbEdificio.SelectedValue <> -1 Then
                        CondEdifici = " AND ID_EDIFICIO =" & Me.cmbEdificio.SelectedValue
                    Else
                        CondEdifici = ""
                    End If
                    par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID,DESCRIZIONE FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT UNITA_IMMOBILIARI.ID_SCALA FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE UNITA_IMMOBILIARI.ID_INDIRIZZO in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "')" & CondEdifici & ") order by descrizione asc"
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
                    par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo in (select id from siscom_mi.indirizzi where civico='" & Me.cmbCivico.SelectedValue.ToString & "' and descrizione='" & par.PulisciStrSql(cmbIndirizzo.SelectedItem.Text) & "') order by interno asc"
                    'par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
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
                '                par.cmd.CommandText = "SELECT distinct unita_immobiliari.interno FROM SISCOM_MI.unita_immobiliari where unita_immobiliari.id_indirizzo = " & Me.cmbCivico.SelectedValue.ToString & " order by interno asc"
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

    Protected Sub cmbEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.Text <> "-1" Then
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.cmbScala.Items.Clear()
            Me.cmbInterno.Items.Clear()
            Me.filtraindirizzi()
        Else
            Me.cmbIndirizzo.Items.Clear()
            Me.cmbCivico.Items.Clear()
            Me.cmbScala.Items.Clear()
            Me.cmbInterno.Items.Clear()
            Me.CaricaIndirizzi()
        End If

    End Sub

    Private Sub filtraindirizzi()
        Try
            If Me.cmbEdificio.SelectedValue <> "-1" Or Me.cmbComplesso.SelectedValue <> "-1" Then

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                Me.cmbIndirizzo.Items.Clear()

                cmbIndirizzo.Items.Add(" ")

                If Me.cmbEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
                ElseIf Me.cmbComplesso.SelectedValue <> "-1" AndAlso Me.cmbEdificio.SelectedValue = "-1" Then
                    par.cmd.CommandText = "SELECT distinct descrizione FROM SISCOM_MI.indirizzi WHERE ID IN (SELECT DISTINCT ID_INDIRIZZO FROM SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.EDIFICI WHERE UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND EDIFICI.ID_COMPLESSO = " & Me.cmbComplesso.SelectedValue & ") order by descrizione asc"
                End If

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    cmbIndirizzo.Items.Add(par.IfNull(myReader1("descrizione"), " "))
                End While
                myReader1.Close()

                cmbIndirizzo.Text = " "

                cmbCivico.Items.Clear()

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
