
Partial Class CALL_CENTER_NuovaS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then
            CaricaEdifici()
            par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_SEGNALAZIONE ORDER BY DESCRIZIONE ASC", cmbTipoSegnalazione, "ID", "DESCRIZIONE", False)
            cmbTipoIntervento.Items.Add(New ListItem("--", "-1"))
            'Cerca()
            Me.cmbTipoRichiesta.SelectedValue = 1
            RiempiGuasti()

        End If
    End Sub
    Private Sub CaricaEdifici()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            cmbEdificio.Items.Add(New ListItem("--", "-1"))
            par.cmd.CommandText = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where SISCOM_MI.EDIFICI.id <> 1 order by denominazione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_edificio"), " "), par.IfNull(myReader1("id"), -1)))
            End While

            myReader1.Close()
            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            cmbEdificio.Text = "-1"
            TxtDescInd.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Private Sub HelpRicerca()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            If par.IfEmpty(Me.TxtDescInd.Text, "Null") <> "Null" Then
                Me.ListEdifci.Items.Clear()

                par.cmd.CommandText = "SELECT distinct ID,denominazione FROM siscom_mi.edifici WHERE denominazione like '" & par.PulisciStrSql(Me.TxtDescInd.Text.ToUpper) & "%' order by denominazione asc"


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
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        HelpRicerca()
    End Sub

    Protected Sub BtnConferma_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles BtnConferma.Click
        Try
            If Me.ListEdifci.SelectedValue.ToString <> "" Then
                Me.cmbEdificio.SelectedValue = Me.ListEdifci.SelectedValue.ToString
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.TextBox1.Value = 1
                Me.LblNoResult.Visible = False

                unita.Value = 0

                FiltraIntScPi()
                Me.txtCognomeInt.Text = ""
                Me.TextBoxContratto.Text = ""
                Me.txtNomeInt.Text = ""
                CarStrutture()


            Else
                Me.TxtDescInd.Text = ""
                Me.ListEdifci.Items.Clear()
                Me.LblNoResult.Visible = False
                Me.TextBox1.Value = 1
            End If
            unita.Value = 0

            'FiltraIntScPi()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnRefreshDb_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnRefreshDb.Click
        Cerca()
        Me.txtmia.Text = "Nessuna Selezione"
    End Sub
    Private Sub Cerca()
        Try
            Dim condizioni As String = ""

            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As Data.DataTable
            If Me.cmbEdificio.SelectedValue <> "-1" Then
                condizioni = condizioni & " and id_edificio = " & Me.cmbEdificio.SelectedValue
            End If

            If Me.cmbEdificio.SelectedValue <> "-1" Then



                If Me.cmbInterno.SelectedValue <> "-1" Then

                    Dim scala As String = ""
                    If IsNumeric(cmbScala.SelectedValue) Then
                        scala = "and id_scala = " & Me.cmbScala.SelectedValue
                    End If

                    par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where " _
                        & "id_edificio = " & Me.cmbEdificio.SelectedValue & scala & " and interno = '" & Me.cmbInterno.SelectedValue & "' "
                    If Me.cmbPiano.SelectedValue <> "-1" Then
                        par.cmd.CommandText = par.cmd.CommandText & " and COD_TIPO_LIVELLO_PIANO = " & Me.cmbPiano.SelectedValue

                    End If
                    da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
                    dt = New Data.DataTable
                    da.Fill(dt)

                    If dt.Rows.Count = 1 Then
                        condizioni = condizioni & " and id_unita = " & dt.Rows(0).Item("id")
                    End If
                End If


                If Me.cmbTipoRichiesta.SelectedValue <> "-1" Then
                    condizioni = condizioni & " and tipo_richiesta = " & Me.cmbTipoRichiesta.SelectedValue
                End If

                If Me.cmbTipoIntervento.SelectedValue <> "-1" Then
                    condizioni = condizioni & " and id_tipologie = " & Me.cmbTipoIntervento.SelectedValue
                End If

                par.cmd.CommandText = "SELECT SEGNALAZIONI.ID,SEGNALAZIONI.ID AS NUM,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO, " _
                                    & "TIPOLOGIE_GUASTI.DESCRIZIONE AS TIPO_INT,TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                                    & "(COGNOME_RS ||' '|| NOME) AS RICHIEDENTE, " _
                                    & "TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'yyyymmdd'),'dd/mm/yyyy') AS DATA_INSERIMENTO, " _
                                    & "DESCRIZIONE_RIC AS DESCRIZIONE " _
                                    & "FROM siscom_mi.SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI,SISCOM_MI.TAB_STATI_SEGNALAZIONI " _
                                    & "WHERE TIPOLOGIE_GUASTI.ID(+) = SEGNALAZIONI.ID_TIPOLOGIE " _
                                    & "AND TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO " & condizioni & " order by id asc "

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

                dt = New Data.DataTable
                da.Fill(dt)
                DataGridSegnalaz.DataSource = dt
                DataGridSegnalaz.DataBind()
            Else

                'par.cmd.CommandText = "SELECT SEGNALAZIONI.ID,SEGNALAZIONI.ID AS NUM,DECODE(SEGNALAZIONI.TIPO_RICHIESTA,0,'INFORMAZIONI',1,'GUASTI') AS TIPO, " _
                '                    & "TIPOLOGIE_GUASTI.DESCRIZIONE AS TIPO_INT,TAB_STATI_SEGNALAZIONI.DESCRIZIONE AS STATO, " _
                '                    & "(COGNOME_RS ||' '|| NOME) AS RICHIEDENTE, " _
                '                    & "TO_CHAR(TO_DATE(SUBSTR(DATA_ORA_RICHIESTA,0,8),'yyyymmdd'),'dd/mm/yyyy') AS DATA_INSERIMENTO, " _
                '                    & "DESCRIZIONE_RIC AS DESCRIZIONE " _
                '                    & "FROM siscom_mi.SEGNALAZIONI,SISCOM_MI.TIPOLOGIE_GUASTI,SISCOM_MI.TAB_STATI_SEGNALAZIONI " _
                '                    & "WHERE TIPOLOGIE_GUASTI.ID(+) = SEGNALAZIONI.ID_TIPOLOGIE " _
                '                    & "AND TAB_STATI_SEGNALAZIONI.ID = SEGNALAZIONI.ID_STATO and segnalazioni.id = -1 order by id asc "
                'da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)

                'dt = New Data.DataTable
                'da.Fill(dt)
                'DataGridSegnalaz.DataSource = dt
                'DataGridSegnalaz.DataBind()

            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbTipoRichiesta_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoRichiesta.SelectedIndexChanged
        If Me.cmbTipoRichiesta.SelectedValue = 1 Then
            RiempiGuasti()
        Else
            cmbTipoIntervento.Items.Clear()
            cmbTipoIntervento.Items.Add(New ListItem("--", "-1"))
        End If
    End Sub
    Private Sub RiempiGuasti()
        Try
            par.OracleConn.Open()
             par.SettaCommand(par)
            cmbTipoIntervento.Items.Clear()
            cmbTipoIntervento.Items.Add(New ListItem("--", "-1"))
            par.cmd.CommandText = "SELECT id,descrizione FROM SISCOM_MI.tipologie_guasti where id_tipo_st is not null order by descrizione asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbTipoIntervento.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), "--"), par.IfNull(myReader1("id"), "-1")))
                'Me.cmbTipoIntervento.SelectedValue = par.IfNull(myReader1("id"), "-1")

            End While
            myReader1.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub cmbEdificio_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbEdificio.SelectedIndexChanged
        If Me.cmbEdificio.SelectedValue <> "-1" Then
            unita.Value = 0

            FiltraIntScPi()
            Me.txtCognomeInt.Text = ""
            Me.TextBoxContratto.Text = ""
            Me.txtNomeInt.Text = ""
            CarStrutture()
        Else
            Me.txtNomeInt.Text = ""
            Me.txtCognomeInt.Text = ""
            Me.TextBoxContratto.Text = ""
            Me.cmbScala.Items.Clear()
            Me.cmbInterno.Items.Clear()
            Me.cmbPiano.Items.Clear()
            Me.cmbStruttura.Items.Clear()
        End If

    End Sub

    Private Sub FiltraIntScPi(Optional idUnita As String = "", Optional ByVal idScala As String = "")
        Try

            Dim connOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
                connOpenNow = True
            End If

            cmbScala.Items.Clear()
            cmbInterno.Items.Clear()
            cmbPiano.Items.Clear()
            Dim condunita As String = ""
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            If Not String.IsNullOrEmpty(idUnita) Then
                condunita = " AND ID = " & idUnita
            End If

            '************ carico scala ***********
            If idUnita = "" Then
                cmbScala.Items.Add(New ListItem("--", "-1"))
            End If

            par.cmd.CommandText = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM siscom_mi.UNITA_IMMOBILIARI WHERE id_edificio =" & Me.cmbEdificio.SelectedValue & condunita & ") order by descrizione asc"
            lettore = par.cmd.ExecuteReader
            While lettore.Read
                cmbScala.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), "--"), par.IfNull(lettore("id"), "-1")))
            End While
            lettore.Close()
            If Not String.IsNullOrEmpty(idScala) Then
                cmbScala.SelectedValue = idScala
            End If

            '************ carico interno ***********


            If idUnita = "" Then
                cmbInterno.Items.Add(New ListItem("--", "-1"))
            End If
            If String.IsNullOrEmpty(idScala) Or idScala = "-1" Then
                par.cmd.CommandText = "SELECT DISTINCT interno FROM(siscom_mi.UNITA_IMMOBILIARI) WHERE id_edificio = " & Me.cmbEdificio.SelectedValue & condunita & " order by interno asc"
            Else
                par.cmd.CommandText = "SELECT DISTINCT interno FROM(siscom_mi.UNITA_IMMOBILIARI) WHERE id_edificio = " & Me.cmbEdificio.SelectedValue & condunita & " and id_scala = " & idScala & " order by interno asc"

            End If
            lettore = par.cmd.ExecuteReader

            While lettore.Read
                cmbInterno.Items.Add(New ListItem(par.IfNull(lettore("interno"), "--"), par.IfNull(lettore("interno"), "-1")))
            End While
            lettore.Close()




            '********** carico piani *********
            If idUnita = "" Then
                cmbPiano.Items.Add(New ListItem("--", "-1"))
            End If
            If Not String.IsNullOrEmpty(condunita) Then
                par.cmd.CommandText = "SELECT COD, descrizione FROM siscom_mi.TIPO_LIVELLO_PIANO where COD =(SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID =" & idUnita & ")"
            Else
                par.cmd.CommandText = "SELECT COD, descrizione FROM siscom_mi.TIPO_LIVELLO_PIANO WHERE COD IN (SELECT DISTINCT COD_TIPO_LIVELLO_PIANO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID_EDIFICIO =" & Me.cmbEdificio.SelectedValue & ") order by descrizione asc"
            End If
            lettore = par.cmd.ExecuteReader

            While lettore.Read
                cmbPiano.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), "--"), par.IfNull(lettore("COD"), "-1")))
            End While
            lettore.Close()


            If String.IsNullOrEmpty(idUnita) Then
                Me.txtCognomeInt.Text = ""
                Me.TextBoxContratto.Text = ""
                Me.txtNomeInt.Text = ""
                

            Else

                If Not IsNothing(Session.Item("idanagrafica")) Then
                    '********************* trvo intestatario se unita immobiliare locata ******************************
                    par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME, (select cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=siscom_mi.soggetti_contrattuali.id_contratto) as COD_CONTRATTO   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                            & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & idUnita & " /*AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%'*/) " _
                                            & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA " _
                                            & "AND ANAGRAFICA.ID=" & Session.Item("idanagrafica")
                Else
                    '********************* trvo intestatario se unita immobiliare locata ******************************
                    par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME, (select cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=siscom_mi.soggetti_contrattuali.id_contratto) as COD_CONTRATTO   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                            & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & idUnita & " /*AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%'*/) " _
                                            & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                End If
                

                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("RAGIONE_SOCIALE"), "") <> "" Then
                        Me.txtCognomeInt.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                    Else
                        Me.txtCognomeInt.Text = par.IfNull(lettore("COGNOME"), "")
                        Me.txtNomeInt.Text = par.IfNull(lettore("NOME"), "")
                    End If
                    TextBoxContratto.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                Else
                    Me.txtCognomeInt.Text = ""
                    Me.txtNomeInt.Text = ""
                    Me.TextBoxContratto.Text = ""


                End If
                lettore.Close()
            End If


            '*********************CHIUSURA CONNESSIONE**********************
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try


    End Sub

    Private Sub FiltraIntScPiChiama(Optional idUnita As String = "", Optional ByVal idScala As String = "", Optional ByVal idanagrafica As String = "")
        Try

            Dim connOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                connOpenNow = True
            End If

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            If String.IsNullOrEmpty(idUnita) Then
                Me.txtCognomeInt.Text = ""
                Me.TextBoxContratto.Text = ""
                Me.txtNomeInt.Text = ""
                Me.txtCognChiama.Text = ""
                Me.txtNomeChiama.Text = ""

            Else

                '********************* trvo intestatario se unita immobiliare locata ******************************

                If idanagrafica <> "" Then
                    par.cmd.CommandText = "SELECT id  , RAGIONE_SOCIALE,COGNOME ,NOME ,TELEFONO,TELEFONO_R,'' AS COD_cONTRATTO  FROM siscom_mi.anagrafica where ANAGRAFICA.ID = " & idanagrafica
                Else
                    par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME ,TELEFONO,TELEFONO_R, (select cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=siscom_mi.soggetti_contrattuali.id_contratto) as COD_CONTRATTO  FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                    & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & idUnita & " /*AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%'*/) " _
                    & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                End If
                
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("RAGIONE_SOCIALE"), "") <> "" Then
                        txtCognChiama.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                    Else
                        txtCognChiama.Text = par.IfNull(lettore("COGNOME"), "")
                        txtNomeChiama.Text = par.IfNull(lettore("NOME"), "")
                    End If
                    txtTel1.Text = par.IfNull(lettore("TELEFONO"), "")
                    txtTel2.Text = par.IfNull(lettore("TELEFONO_R"), "")
                    txtMail.Text = ""
                Else
                    Me.txtCognChiama.Text = ""
                    Me.txtNomeChiama.Text = ""
                End If
                lettore.Close()
            End If


            '*********************CHIUSURA CONNESSIONE**********************
            If connOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try


    End Sub

    Protected Sub cmbInterno_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbInterno.SelectedIndexChanged
        If Me.cmbInterno.SelectedValue <> "-1" Then
            unita.Value = 0
            ScalaPianoDaInterno()
        Else
            FiltraIntScPi()
        End If
    End Sub
    Private Sub ScalaPianoDaInterno()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            Me.cmbScala.Items.Clear()
            Me.cmbPiano.Items.Clear()
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            par.cmd.CommandText = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM siscom_mi.UNITA_IMMOBILIARI WHERE id_edificio =" & Me.cmbEdificio.SelectedValue & " and interno = '" & par.PulisciStrSql(Me.cmbInterno.SelectedValue) & "') order by descrizione asc"
            lettore = par.cmd.ExecuteReader
            While lettore.Read
                cmbScala.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), "--"), par.IfNull(lettore("id"), "-1")))
            End While
            lettore.Close()
            Dim condScala As String = ""
            If Me.cmbScala.Items.Count > 0 Then
                If Me.cmbScala.SelectedValue <> -1 Then
                    condScala = " and id_scala = " & Me.cmbScala.SelectedValue & " "
                End If
            End If
            '********** carico piani *********
            par.cmd.CommandText = "select COD, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where COD in (select COD_TIPO_LIVELLO_PIANO from siscom_mi.unita_immobiliari where id_edificio =" & Me.cmbEdificio.SelectedValue & " and interno = '" & par.PulisciStrSql(Me.cmbInterno.SelectedValue) & "'" & condScala & ") order by descrizione asc"
            lettore = par.cmd.ExecuteReader
            While lettore.Read
                cmbPiano.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), "--"), par.IfNull(lettore("COD"), "-1")))
            End While
            If lettore.HasRows = False Then
                cmbPiano.Items.Add(New ListItem("--", "-1"))
            End If
            lettore.Close()

            '********************* trvo intestatario se unita immobiliare locata ******************************
            par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where id_edificio = " & Me.cmbEdificio.SelectedValue & " and interno = '" & Me.cmbInterno.SelectedValue & "' " & condScala

            If Me.cmbPiano.SelectedValue <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and COD_TIPO_LIVELLO_PIANO = " & Me.cmbPiano.SelectedValue
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count = 1 Then
                unita.Value = dt.Rows(0).Item("id")
                par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME, (select cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=siscom_mi.soggetti_contrattuali.id_contratto) as COD_CONTRATTO   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                    & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE (id_unita = " & dt.Rows(0).Item("ID") & " OR id_unita = (SELECT NVL(id_unita_principale,0) FROM siscom_mi.unita_immobiliari WHERE ID = " & dt.Rows(0).Item("ID") & "))" _
                                    & "/*AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%'*/) " _
                                    & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("RAGIONE_SOCIALE"), "") <> "" Then
                        Me.txtCognomeInt.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                        Me.txtCognChiama.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                    Else
                        Me.txtCognomeInt.Text = par.IfNull(lettore("COGNOME"), "")
                        Me.txtNomeInt.Text = par.IfNull(lettore("NOME"), "")
                        Me.txtCognChiama.Text = par.IfNull(lettore("COGNOME"), "")
                        Me.txtNomeChiama.Text = par.IfNull(lettore("NOME"), "")
                    End If
                    Me.TextBoxContratto.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                Else
                    Me.txtCognomeInt.Text = ""
                    Me.TextBoxContratto.Text = ""
                    Me.txtNomeInt.Text = ""
                    Me.txtCognChiama.Text = ""
                    Me.txtNomeChiama.Text = ""

                End If
                lettore.Close()
            Else
                Me.txtCognomeInt.Text = ""
                Me.TextBoxContratto.Text = ""
                Me.txtNomeInt.Text = ""

            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try


    End Sub

    Protected Sub btnFindImmobile_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFindImmobile.Click
        If Not String.IsNullOrEmpty(Me.txtCognomeInt.Text) Or Not String.IsNullOrEmpty(Me.TextBoxContratto.Text) Then
            SetUidaInte()
            CarStrutture()
            'Me.txtNomeChiama.Text = Me.txtNomeInt.Text
            'Me.txtCognChiama.Text = Me.txtCognomeInt.Text
        End If
    End Sub
    Private Sub SetUidaInte()
        Try


            If Not String.IsNullOrEmpty(Session.Item("idui")) Then
                If Session.Item("idui") > 0 Then

                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                    End If

                    par.cmd.CommandText = "select id_edificio from siscom_mi.unita_immobiliari where id = " & Session.Item("idui")
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        If Not IsNothing(Me.cmbEdificio.Items.FindByValue(par.IfNull(lettore("id_edificio"), "-1"))) Then
                            Me.cmbEdificio.SelectedValue = par.IfNull(lettore("id_edificio"), "-1")
                        Else
                            ScriptManager.RegisterClientScriptBlock(Me.Page, Me.Page.GetType(), "msg", "alert('Unità contrattuale non definita!');", True)
                        End If
                    End If
                    lettore.Close()


                    Me.unita.Value = Session.Item("idui")

                    FiltraIntScPi(Session.Item("idui"))
                    Session.Remove("idui")

                Else
                    Me.cmbEdificio.SelectedValue = "-1"
                    FiltraIntScPi()
                    Session.Remove("idui")
                    Me.unita.Value = 0
                    Me.txtCognomeInt.Text = ""
                    Me.TextBoxContratto.Text = ""
                    Me.txtNomeInt.Text = ""
                    'Response.Write("<script>alert('Nessuna corrispondenza trovata!');</script>")
                End If


                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()





            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub cmbScala_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbScala.SelectedIndexChanged
        FiltraIntScPi("", Me.cmbScala.SelectedValue)
        unita.Value = 0
    End Sub

    Protected Sub DataGridSegnalaz_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridSegnalaz.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='white'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''} Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la segnalazione N°: " & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='yellow'};this.style.cursor='pointer';")
            e.Item.Attributes.Add("onmouseout", "if (this.style.backgroundColor!='red') {this.style.backgroundColor='Gainsboro'}")
            e.Item.Attributes.Add("onclick", "if (Selezionato) {Selezionato.style.backgroundColor=''} Selezionato=this;this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato la segnalazione N°: " & e.Item.Cells(0).Text.Replace("'", "\'") & "';document.getElementById('idSelected').value='" & e.Item.Cells(0).Text.Replace("'", "\'") & "';")

        End If

    End Sub


    Private Sub CarStrutture()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            cmbStruttura.Items.Clear()
            'cmbStruttura.Items.Add(New ListItem("--", "-1"))
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim idTipoStruttura As String = ""

            Dim condizione As String = ""
            If Me.cmbEdificio.SelectedValue <> "-1" Then 'And Me.cmbTipoIntervento.SelectedValue = "-1" Then
                Dim FILIALE As String = "ID_FILIALE"
                par.cmd.CommandText = "SELECT FL_FILIALE_AMM FROM SISCOM_MI.TIPO_SEGNALAZIONE WHERE ID=" & cmbTipoSegnalazione.SelectedValue
                Dim ris As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If ris = 1 Then
                    FILIALE = "ID_FILIALE_AMM"
                End If
                par.cmd.CommandText = "SELECT * FROM siscom_mi.tab_filiali WHERE ID IN (SELECT DISTINCT " & FILIALE & " FROM siscom_mi.complessi_immobiliari where id in (select id_complesso from siscom_mi.edifici where id = " & Me.cmbEdificio.SelectedValue & "))"

                'ElseIf Me.cmbTipoIntervento.SelectedValue <> "-1" And Me.cmbEdificio.SelectedValue <> "-1" Then

                '    par.cmd.CommandText = "select id_tipo_st, id_struttura from siscom_mi.tipologie_guasti where id = " & Me.cmbTipoIntervento.SelectedValue
                '    lettore = par.cmd.ExecuteReader
                '    If lettore.Read Then
                '        idTipoStruttura = par.IfNull(lettore("id_tipo_st"), "")
                '        If idTipoStruttura = "2" Then
                '            par.cmd.CommandText = "select * from siscom_mi.tab_filiali where id = " & par.IfNull(lettore("id_struttura"), "-1")
                '        ElseIf idTipoStruttura = "1" Then
                '            par.cmd.CommandText = "select * from siscom_mi.tab_filiali where id = (SELECT id_tecnica FROM siscom_mi.tab_filiali WHERE ID IN (SELECT DISTINCT id_filiale FROM siscom_mi.complessi_immobiliari where id in (select id_complesso from siscom_mi.edifici where id = " & Me.cmbEdificio.SelectedValue & ")))"
                '        Else
                '            par.cmd.CommandText = "SELECT *  FROM siscom_mi.tab_filiali WHERE ID = (SELECT DISTINCT id_filiale FROM siscom_mi.complessi_immobiliari WHERE ID IN (SELECT id_complesso FROM siscom_mi.edifici WHERE ID = " & Me.cmbEdificio.SelectedValue & "))"
                '        End If
                '    End If
                '    lettore.Close()
            Else
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Exit Sub
            End If


            'par.cmd.CommandText = "SELECT * FROM siscom_mi.tab_filiali WHERE ID IN (SELECT DISTINCT id_filiale FROM siscom_mi.complessi_immobiliari where id in (select id_complesso from siscom_mi.edifici where id = " & Me.cmbEdificio.SelectedValue & "))"

            lettore = par.cmd.ExecuteReader
            While lettore.Read
                cmbStruttura.Items.Add(New ListItem(par.IfNull(lettore("nome"), ""), par.IfNull(lettore("id"), "-1")))
            End While
            lettore.Close()





            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub cmbTipoIntervento_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoIntervento.SelectedIndexChanged
        'CarStrutture()
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Dim script As String = ""
        If CheckControl() = True Then

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim condominio As Integer = 0
            Dim condizioneCond As String = "&cond=0"
            If unita.Value <> "-1" Then
                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.COND_UI WHERE ID_UI=" & unita.Value
                condominio = par.IfNull(par.cmd.ExecuteScalar, 0)
                If condominio > 0 Then
                    Response.Write("<script>alert('L\'unità selezionata è in condominio!');</script>")
                    condizioneCond = "&cond=1"
                End If
            Else
                If cmbEdificio.SelectedValue <> "-1" Then
                    par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO=" & unita.Value
                    condominio = par.IfNull(par.cmd.ExecuteScalar, 0)
                    If condominio > 0 Then
                        Response.Write("<script>alert('L\'edificio selezionato è in condominio!');</script>")
                        condizioneCond = "&cond=1"
                    End If
                End If
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            ScriptManager.GetCurrent(Me).Dispose()
            script = "<script>window.open('SegnalazioneP.aspx?ID=-1"
            script = script & "&CogInt=" & par.VaroleDaPassare(Me.txtCognomeInt.Text.ToUpper) _
                            & "&NomInt=" & par.VaroleDaPassare(Me.txtNomeInt.Text.ToUpper) _
                            & "&Edif=" & Me.cmbEdificio.SelectedValue _
                            & "&Inte=" & Me.cmbInterno.SelectedValue _
                            & "&Scal=" & Me.cmbScala.SelectedValue _
                            & "&Pian=" & Me.cmbPiano.SelectedValue _
                            & "&TipR=" & Me.cmbTipoRichiesta.SelectedValue _
                            & "&TipI=" & Me.cmbTipoIntervento.SelectedValue _
                            & "&CogCall=" & par.VaroleDaPassare(Me.txtCognChiama.Text.ToUpper) _
                            & "&NomCall=" & par.VaroleDaPassare(Me.txtNomeChiama.Text.ToUpper) _
                            & "&Tel1=" & par.VaroleDaPassare(Me.txtTel1.Text.ToUpper) _
                            & "&Tel2=" & par.VaroleDaPassare(Me.txtTel2.Text.ToUpper) _
                            & "&Mail=" & par.VaroleDaPassare(Me.txtMail.Text.ToUpper) _
                            & "&IdUi=" & Me.unita.Value _
                            & "&IdSt=" & Me.cmbStruttura.SelectedValue _
                            & "&tipoS=" & Me.cmbTipoSegnalazione.SelectedValue _
                            & condizioneCond

            script = script & "','Segnalazione','height=700px,width=900px,resizable=yes');</script>"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "JSCR", script, False)
        End If

    End Sub
    Private Function CheckControl() As Boolean
        CheckControl = True
        Dim script As String = ""

        Try

            If Me.cmbEdificio.SelectedValue = "-1" Then
                CheckControl = False
                script = "<script>alert('Definire l\'Oggetto della Chiamata prima di aprire una Nuova Segnalazione!');</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERJSCR", script, False)
            ElseIf Me.cmbTipoRichiesta.SelectedValue = -1 Then
                CheckControl = False
                script = "<script>alert('Definire il tipo di richiesta!')</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERJSCR", script, False)

            ElseIf Me.cmbTipoSegnalazione.SelectedValue = 1 And Me.cmbTipoIntervento.SelectedValue = "-1" Then
                CheckControl = False
                script = "<script>alert('Definire il tipo di intervento!')</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERJSCR", script, False)

            ElseIf String.IsNullOrEmpty(Me.txtCognChiama.Text) Then
                CheckControl = False
                script = "<script>alert('Definire il chiamante!')</script>"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "ERJSCR", script, False)
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

        Return CheckControl
    End Function

    Protected Sub cmbPiano_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPiano.SelectedIndexChanged
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader
            Dim condScala As String = ""
            If Me.cmbScala.Items.Count > 0 Then
                If Me.cmbScala.SelectedValue <> -1 Then
                    condScala = " and id_scala = " & Me.cmbScala.SelectedValue & " "
                End If
            End If

            '********************* trvo intestatario se unita immobiliare locata ******************************
            par.cmd.CommandText = "select id from siscom_mi.unita_immobiliari where id_edificio = " & Me.cmbEdificio.SelectedValue & " and interno = '" & Me.cmbInterno.SelectedValue & "'" & condScala

            If Me.cmbPiano.SelectedValue <> "" Then
                par.cmd.CommandText = par.cmd.CommandText & " and COD_TIPO_LIVELLO_PIANO = " & Me.cmbPiano.SelectedValue
            End If
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)
            If dt.Rows.Count = 1 Then
                unita.Value = dt.Rows(0).Item("id")
                par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME, (select cod_contratto from siscom_mi.rapporti_utenza where rapporti_utenza.id=siscom_mi.soggetti_contrattuali.id_contratto) as COD_CONTRATTO   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                    & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE (id_unita = " & dt.Rows(0).Item("ID") & " OR id_unita = (SELECT NVL(id_unita_principale,0) FROM siscom_mi.unita_immobiliari WHERE ID = " & dt.Rows(0).Item("ID") & "))" _
                                    & "/*AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%'*/) " _
                                    & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    If par.IfNull(lettore("RAGIONE_SOCIALE"), "") <> "" Then
                        Me.txtCognomeInt.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                        Me.txtCognChiama.Text = par.IfNull(lettore("RAGIONE_SOCIALE"), "")
                    Else
                        Me.txtCognomeInt.Text = par.IfNull(lettore("COGNOME"), "")
                        Me.txtNomeInt.Text = par.IfNull(lettore("NOME"), "")
                        Me.txtCognChiama.Text = par.IfNull(lettore("COGNOME"), "")
                        Me.txtNomeChiama.Text = par.IfNull(lettore("NOME"), "")
                    End If
                    Me.TextBoxContratto.Text = par.IfNull(lettore("COD_CONTRATTO"), "")
                Else
                    Me.txtCognomeInt.Text = ""
                    Me.TextBoxContratto.Text = ""
                    Me.txtNomeInt.Text = ""
                    Me.txtCognChiama.Text = ""
                    Me.txtNomeChiama.Text = ""

                End If
                lettore.Close()
            Else
                Me.txtCognomeInt.Text = ""
                Me.TextBoxContratto.Text = ""
                Me.txtNomeInt.Text = ""

            End If

            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub cmbTipoSegnalazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazione.SelectedIndexChanged
        If cmbTipoSegnalazione.SelectedValue = "1" Then
            cmbTipoIntervento.Enabled = True
        Else
            cmbTipoIntervento.Enabled = False
        End If
        CarStrutture()
    End Sub

    Protected Sub btnFindImmobileChiamante_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFindImmobileChiamante.Click
        If Not String.IsNullOrEmpty(Me.txtCognChiama.Text) Then
            If Not IsNothing(Session.Item("idui")) And IsNumeric(Session.Item("idui")) And Not IsNothing(Session.Item("idanagrafica")) And IsNumeric(Session.Item("idanagrafica")) Then
                FiltraIntScPiChiama(Session.Item("idui"), , Session.Item("idanagrafica"))
            Else
                FiltraIntScPiChiama()
            End If
            'CarStrutture()
            'Me.txtNomeChiama.Text = Me.txtNomeInt.Text
            'Me.txtCognChiama.Text = Me.txtCognomeInt.Text
            SetUidaInte()
            CarStrutture()
        End If
        'If Not String.IsNullOrEmpty(Me.txtNomeChiama.Text) Then
        '    SetUidaInte()
        '    CarStrutture()
        '    'Me.txtNomeChiama.Text = Me.txtNomeInt.Text
        '    'Me.txtCognChiama.Text = Me.txtCognomeInt.Text
        'End If
    End Sub

    Protected Sub pulisciChiamante()
        txtCognChiama.Text = ""
        txtNomeChiama.Text = ""
        txtTel1.Text = ""
        txtTel2.Text = ""
        txtMail.Text = ""
    End Sub

    'Protected Sub btnPulisci_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPulisci.Click
    '    pulisciChiamante()
    'End Sub

    Protected Sub pulisciIntestatario()
        txtCognomeInt.Text = ""
        TextBoxContratto.Text = ""
        txtNomeInt.Text = ""
        cmbEdificio.SelectedValue = "-1"
        cmbScala.Items.Clear()
        cmbInterno.Items.Clear()
        cmbPiano.Items.Clear()
        cmbStruttura.Items.Clear()
    End Sub

    'Protected Sub btnPulisci2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnPulisci2.Click
    '    pulisciIntestatario()
    'End Sub

    'Protected Sub imgCopia_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgCopia.Click
    '    If Not String.IsNullOrEmpty(Me.txtNomeChiama.Text) Then
    '        SetUidaInte()
    '        CarStrutture()
    '        'Me.txtNomeChiama.Text = Me.txtNomeInt.Text
    '        'Me.txtCognChiama.Text = Me.txtCognomeInt.Text
    '    End If
    'End Sub

    Protected Sub imgSvuota_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgSvuota.Click
        pulisciChiamante()
        pulisciIntestatario()
        Session.Remove("idui")
    End Sub
End Class
