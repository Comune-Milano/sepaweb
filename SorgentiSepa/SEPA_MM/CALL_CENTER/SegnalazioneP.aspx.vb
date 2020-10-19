Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports ExpertPdf.HtmlToPdf
Imports System.Data.OleDb
Partial Class CALL_CENTER_SegnalazioneP
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public TabellaNote As String = ""
    Public TabellaAllegati As String = ""
    Public Property vIdSegnalazione() As String
        Get
            If Not (ViewState("par_IdSegnalazione") Is Nothing) Then
                Return CStr(ViewState("par_IdSegnalazione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IdSegnalazione") = value
        End Set

    End Property
    Public Property tipoS() As String
        Get
            If Not (ViewState("tipoS") Is Nothing) Then
                Return CStr(ViewState("tipoS"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("tipoS") = value
        End Set

    End Property
    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
        End Set

    End Property


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        If Not IsPostBack Then
            vIdSegnalazione = Request.QueryString("ID")
            If vIdSegnalazione = "-1" Then
                tipoS = Request.QueryString("tipoS")
            End If
            If Not IsNothing(Request.QueryString("COND")) AndAlso Request.QueryString("COND") = "1" Then
                lblInCondominio.Text = "IN CONDOMINIO"
            Else
                lblInCondominio.Text = ""
            End If

            CaricaDatiSegnalazione()
            cmbNoteChiusura.Attributes.Add("onChange", "getDropDownListvalue();")
            lblDataIns.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataCInt.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            Me.txtOraCInt.Text = Format(Now, "HH:mm")
            Me.txtDataCInt.Text = Format(Now, "dd/MM/yyyy")

        End If
        If Me.vIdSegnalazione > 0 Then
            CaricaElencoAllegati()
            CaricaTabellaNote(vIdSegnalazione)
        End If
        If IsNumeric(vIdSegnalazione) AndAlso vIdSegnalazione > 0 AndAlso tipoS = "0" Then
            ControllaAppuntamento()
            lblAppuntamento.Visible = True
        Else
            lblAppuntamento.Visible = False
        End If
        If tipoS = "1" Then
            cmbUrgenza.Visible = True
            lblUrgenza.Visible = True
            lblTipoIntervento.Visible = True
            cmbTipoIntervento.Visible = True
        Else
            cmbUrgenza.Visible = False
            lblUrgenza.Visible = False
            lblTipoIntervento.Visible = False
            cmbTipoIntervento.Visible = False
        End If
        If Session.Item("MOD_CALL_SL") = 1 Then
            FrmSoloLettura()
        End If
        If Not IsNothing(Request.QueryString("PROV")) Then
            If Request.QueryString("PROV") = "S" Then
                FrmSoloLettura()
            End If
        End If
    End Sub
    Private Sub CaricaDatiSegnalazione()
        Try
            If vIdSegnalazione = "-1" Then
                NuovaSegnalazione()
            Else
                id.Value = vIdSegnalazione

                ApriSegnalazione()
            End If


        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
    Private Sub NuovaSegnalazione()
        Try
            Me.txtCognomeInt.Text = Request.QueryString("CogInt")
            Me.txtNomeInt.Text = Request.QueryString("NomInt")
            Me.txtCognChiama.Text = Request.QueryString("CogCall")
            Me.txtNomeChiama.Text = Request.QueryString("NomCall")
            Me.txtTel1.Text = Request.QueryString("Tel1")
            Me.txtTel2.Text = Request.QueryString("Tel2")
            Me.txtMail.Text = Request.QueryString("Mail")
            Me.lblStato.Text = "NUOVA"
            'Me.lblData.Text = Format(Now, "dd/MM/yyyy")
            Me.lblDataIns.Text = Format(Now, "dd/MM/yyyy")
            Me.txtOra.Text = Format(Now, "HH:mm")
            Me.lblNrich.Text = "x x x"
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If

            par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE ORDER BY DESCRIZIONE ASC", cmbTipoSegnalazione, "ID", "DESCRIZIONE", False)
            Try
                cmbTipoSegnalazione.SelectedValue = tipoS
            Catch ex As Exception
            End Try

            par.cmd.CommandText = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id = " & Request.QueryString("Edif")
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While lettore.Read
                cmbEdificio.Items.Add(New ListItem(par.IfNull(lettore("denominazione"), " ") & "- -" & " cod." & par.IfNull(lettore("cod_edificio"), " "), par.IfNull(lettore("id"), -1)))
            End While
            lettore.Close()

            If IsNumeric(Request.QueryString("Scal")) Then
                par.cmd.CommandText = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID = " & Request.QueryString("Scal")
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    cmbScala.Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), "--"), par.IfNull(lettore("id"), "-1")))
                End While
                lettore.Close()
            End If

            If Request.QueryString("IdUI") <> "" Then
                unita.Value = Request.QueryString("IdUI")
                par.cmd.CommandText = "SELECT COD_UNITA_IMMOBILIARE,interno FROM siscom_mi.UNITA_IMMOBILIARI  WHERE id = " & Request.QueryString("IdUI")
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    cmbInterno.Items.Add(New ListItem(par.IfNull(lettore("interno"), "--"), par.IfNull(lettore("interno"), "-1")))
                    lblOggetto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "") & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">U.I. cod." & par.IfNull(lettore("COD_UNITA_IMMOBILIARE"), "") & "</a>"
                    lblOggetto.Visible = True
                End If
                lettore.Close()

                par.cmd.CommandText = "SELECT siscom_mi.Getstatocontratto2(id_contratto,1) FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & Request.QueryString("IdUI") & " AND siscom_mi.Getstatocontratto2(id_contratto,0) like '%CORSO%'"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    Me.lblContratto.Text = par.IfNull(lettore(0), "")
                    Me.lblContratto.Visible = True
                    lblcont.Visible = True
                End If
                lettore.Close()
            Else
                lblOggetto.Visible = False
            End If
            'par.cmd.CommandText = "select COD, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where COD = " & Request.QueryString("Pian")

            'lettore = par.cmd.ExecuteReader
            'While lettore.Read
            '    cmbPiano.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), "--"), par.IfNull(lettore("COD"), "-1")))
            'End While
            'lettore.Close()

            par.cmd.CommandText = "select cod, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where cod = " & Request.QueryString("Pian")
            lettore = par.cmd.ExecuteReader
            While lettore.Read
                cmbPiano.Items.Add(New ListItem(par.IfNull(lettore("descrizione"), "--"), par.IfNull(lettore("COD"), "-1")))
            End While
            lettore.Close()

            If Not IsNothing(Request.QueryString("IdSt")) AndAlso IsNumeric(Request.QueryString("IdSt")) Then
                par.cmd.CommandText = "select * from siscom_mi.tab_filiali where id = " & Request.QueryString("IdSt")
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    cmbStruttura.Items.Add(New ListItem(par.IfNull(lettore("Nome"), "--"), par.IfNull(lettore("id"), "-1")))
                End While
                lettore.Close()
            End If


            If Request.QueryString("IdUI") <> "" Then
                par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_UNITA = " & Request.QueryString("IdUI")
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    lblImpianto.Visible = True
                    lblImpianto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('ElencoImpiantiUI.aspx?ID=" & Request.QueryString("IdUI") & "&T=U','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & "IMPIANTI </a>"
                Else
                    lblImpianto.Visible = False
                End If
            Else
                par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    lblImpianto.Visible = True
                Else
                    lblImpianto.Visible = False
                End If

            End If
            Me.btnSollecito.Visible = False
            Me.imgChiudiSegnalazione.Visible = False
            Me.btnAnnulla.Visible = False


            '*********************CHIUSURA CONNESSIONE**********************
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Me.cmbTipoRichiesta.SelectedValue = Request.QueryString("TipR")
            If Me.cmbTipoRichiesta.SelectedValue = 1 Then
                RiempiGuasti()
                Me.cmbTipoIntervento.SelectedValue = Request.QueryString("TipI")
                RiempiNoteChiusura()

            Else
                cmbTipoIntervento.Items.Clear()
                cmbTipoIntervento.Items.Add(New ListItem("--", "-1"))
            End If

            Me.txtTel1.ReadOnly = False
            Me.txtTel2.ReadOnly = False
            Me.txtMail.ReadOnly = False

            Me.btnStampSopr.Visible = False

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Private Sub ApriSegnalazione()



        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            Me.lblNrich.Text = vIdSegnalazione
            par.cmd.CommandText = "select * from siscom_mi.segnalazioni where id = " & vIdSegnalazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If lettore.Read Then

                Me.txtCognChiama.Text = par.IfNull(lettore("COGNOME_RS"), "")


                tipoS = par.IfNull(lettore("ID_TIPO_SEGNALAZIONE"), "")
                If tipoS = "1" Then
                    Me.cmbUrgenza.SelectedIndex = par.IfNull(lettore("ID_PERICOLO_SEGNALAZIONE"), "")
                End If
                par.caricaComboBox("SELECT * FROM SISCOM_MI.TIPO_SEGNALAZIONE ORDER BY DESCRIZIONE ASC", cmbTipoSegnalazione, "ID", "DESCRIZIONE", False)
                Try
                    cmbTipoSegnalazione.SelectedValue = tipoS
                Catch ex As Exception
                End Try

                Me.txtNomeChiama.Text = par.IfNull(lettore("NOME"), "")
                Me.txtTel1.Text = par.IfNull(lettore("TELEFONO1"), "")
                Me.txtTel2.Text = par.IfNull(lettore("TELEFONO2"), "")
                Me.txtMail.Text = par.IfNull(lettore("MAIL"), "")
                Me.txtDescrizione.Text = par.IfNull(lettore("DESCRIZIONE_RIC"), "")
                Me.cmbTipoPervenuta.Items.FindByText(par.IfNull(lettore("TIPO_PERVENUTA"), "Telefonica")).Selected = True
                Me.cmbTipoRichiesta.SelectedValue = par.IfNull(lettore("TIPO_RICHIESTA"), "2")
                If par.IfNull(lettore("fl_sollecito"), 0) = 1 Then
                    Me.lblsollecito.Text = "SOLLECITATA"
                    Me.lblsollecito.ForeColor = Drawing.Color.Red

                End If

                If Me.cmbTipoRichiesta.SelectedValue = 1 Then
                    RiempiGuasti()
                    Me.cmbTipoIntervento.SelectedValue = par.IfNull(lettore("ID_TIPOLOGIE"), "-1")
                    RiempiNoteChiusura()


                Else
                    cmbTipoIntervento.Items.Clear()
                    cmbTipoIntervento.Items.Add(New ListItem("--", "-1"))
                End If

                'Me.lblData.Text = Format(Now, "dd/MM/yyyy")
                Me.lblDataIns.Text = par.FormattaData(Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "                 "), 1, 8))
                Me.txtOra.Text = Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "          "), 9, 2) & ":" & Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "          "), 11, 2)

                par.cmd.CommandText = "select descrizione from siscom_mi.tab_stati_segnalazioni where id = " & par.IfNull(lettore("id_stato"), "")
                Dim readerInt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If readerInt.Read Then
                    Me.lblStato.Text = par.IfNull(readerInt(0), "")
                End If
                readerInt.Close()
                'If par.IfNull(lettore("id_stato"), "") = 1 Then

                '    par.cmd.CommandText = "select * from siscom_mi.segnalazioni_sopralluogo where id_segnalazione =" & vIdSegnalazione
                '    readerInt = par.cmd.ExecuteReader
                '    If readerInt.Read Then
                '        Me.lblStato.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('Sopralluogo.aspx?IdSegn=" & vIdSegnalazione & "&LE=1','window', 'status:no;dialogWidth:700px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');" & Chr(34) & ">" & lblStato.Text & "</a>"

                '    End If
                '    readerInt.Close()
                'End If


                par.cmd.CommandText = "SELECT EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id = " & par.IfNull(lettore("ID_EDIFICIO"), "")
                readerInt = par.cmd.ExecuteReader
                If readerInt.Read Then
                    cmbEdificio.Items.Add(New ListItem(par.IfNull(readerInt("denominazione"), " ") & "- -" & " cod." & par.IfNull(readerInt("cod_edificio"), " "), par.IfNull(readerInt("id"), -1)))
                End If
                readerInt.Close()


                par.cmd.CommandText = "select id,nome from siscom_mi.tab_filiali where id = " & par.IfNull(lettore("id_struttura"), "-1")
                readerInt = par.cmd.ExecuteReader
                If readerInt.Read Then
                    Me.cmbStruttura.Items.Add(New ListItem(par.IfNull(readerInt("nome"), " "), par.IfNull(readerInt("id"), -1)))
                End If
                readerInt.Close()

                '************info ricavabili da id_unita******************
                unita.Value = par.IfNull(lettore("id_unita"), "")
                If par.IfEmpty(unita.Value, 0) > 0 Then

                    par.cmd.CommandText = "SELECT * FROM siscom_mi.UNITA_IMMOBILIARI  WHERE id = " & unita.Value
                    readerInt = par.cmd.ExecuteReader
                    If readerInt.Read Then
                        '**************INTERNO*************
                        cmbInterno.Items.Add(New ListItem(par.IfNull(readerInt("interno"), "--"), par.IfNull(readerInt("interno"), "-1")))
                        lblOggetto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(readerInt("COD_UNITA_IMMOBILIARE"), "") & "','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">U.I. cod." & par.IfNull(readerInt("COD_UNITA_IMMOBILIARE"), "") & "</a>"
                        lblOggetto.Visible = True
                        '**************SCALA*************
                        par.cmd.CommandText = "SELECT ID,descrizione FROM siscom_mi.SCALE_EDIFICI WHERE ID = " & par.IfNull(readerInt("id_scala"), 0)
                        Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If myreader.Read Then
                            cmbScala.Items.Add(New ListItem(par.IfNull(myreader("DESCRIZIONE"), "--"), par.IfNull(myreader("id"), "-1")))
                        End If
                        myreader.Close()
                        '**************PIANO*************
                        par.cmd.CommandText = "select COD, descrizione from siscom_mi.TIPO_LIVELLO_PIANO where COD = " & par.IfNull(readerInt("COD_TIPO_LIVELLO_PIANO"), "-1")
                        myreader = par.cmd.ExecuteReader
                        If myreader.Read Then
                            cmbPiano.Items.Add(New ListItem(par.IfNull(myreader("descrizione"), "--"), par.IfNull(myreader("cod"), "-1")))
                        End If
                        myreader.Close()

                    End If
                    readerInt.Close()

                    par.cmd.CommandText = "SELECT id_contratto,siscom_mi.Getstatocontratto2(id_contratto,1)as contlink FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita.Value & " AND siscom_mi.Getstatocontratto2(id_contratto,0) like '%CORSO%'"
                    readerInt = par.cmd.ExecuteReader

                    If readerInt.Read Then
                        Me.lblContratto.Text = par.IfNull(readerInt("contlink"), "")
                        Me.lblContratto.Visible = True
                        lblcont.Visible = True
                    End If
                    readerInt.Close()

                    par.cmd.CommandText = "SELECT ID_ANAGRAFICA  , RAGIONE_SOCIALE,COGNOME ,NOME   FROM siscom_mi.SOGGETTI_CONTRATTUALI ,SISCOM_MI.ANAGRAFICA " _
                                        & "WHERE id_contratto IN (SELECT id_contratto FROM siscom_mi.UNITA_CONTRATTUALE WHERE id_unita = " & unita.Value & " AND siscom_mi.Getstatocontratto2(id_contratto,0) LIKE '%CORSO%') " _
                                        & "AND COD_TIPOLOGIA_OCCUPANTE = 'INTE'  AND ANAGRAFICA.ID = ID_ANAGRAFICA "
                    readerInt = par.cmd.ExecuteReader
                    If readerInt.Read Then
                        If par.IfNull(readerInt("RAGIONE_SOCIALE"), "") <> "" Then
                            Me.txtCognomeInt.Text = par.IfNull(readerInt("RAGIONE_SOCIALE"), "")
                        Else
                            Me.txtCognomeInt.Text = par.IfNull(readerInt("COGNOME"), "")
                            Me.txtNomeInt.Text = par.IfNull(readerInt("NOME"), "")
                        End If
                    Else
                        Me.txtCognomeInt.Text = ""
                        Me.txtNomeInt.Text = ""
                    End If
                    readerInt.Close()

                End If
            End If
            lettore.Close()
            If par.IfEmpty(unita.Value, 0) <> "0" Then
                par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_UNITA = " & unita.Value
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    lblImpianto.Visible = True
                    lblImpianto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('ElencoImpiantiUI.aspx?ID=" & unita.Value & "&T=U','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & "IMPIANTI </a>"
                Else
                    lblImpianto.Visible = False
                End If
            Else
                par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_EDIFICIO = " & Me.cmbEdificio.SelectedValue
                lettore = par.cmd.ExecuteReader
                If lettore.HasRows Then
                    lblImpianto.Visible = True
                    lblImpianto.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('ElencoImpiantiUI.aspx?ID=" & cmbEdificio.SelectedValue & "&T=E','Dettagli','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & "IMPIANTI </a>"

                Else
                    lblImpianto.Visible = False
                End If

            End If

            CaricaElencoAllegati()
            CaricaTabellaNote(vIdSegnalazione)
            StatoSegnalazione()

            par.cmd.CommandText = "select * from siscom_mi.segnalazioni_sopralluogo where id_segnalazione =" & vIdSegnalazione

            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                Me.lblStato.Text = "<a href='javascript:void(0)' onclick=" & Chr(34) & "window.showModalDialog('Sopralluogo.aspx?IdSegn=" & vIdSegnalazione & "&LE=1','window', 'status:no;dialogWidth:700px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');" & Chr(34) & ">" & lblStato.Text & "</a>"
            End If
            lettore.Close()



            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If



        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
    Private Sub RiempiGuasti()
        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
                ConnOpenNow = True
            End If

            cmbTipoIntervento.Items.Clear()
            cmbTipoIntervento.Items.Add(New ListItem("--", "-1"))
            par.cmd.CommandText = "SELECT id,descrizione FROM SISCOM_MI.tipologie_guasti where id_tipo_st is not null"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                cmbTipoIntervento.Items.Add(New ListItem(par.IfNull(myReader1("descrizione"), "--"), par.IfNull(myReader1("id"), "-1")))
            End While
            myReader1.Close()

            If ConnOpenNow = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Sub ControllaAppuntamento()
        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = " SELECT TO_CHAR(TO_DATE(DATA_APPUNTAMENTO,'yyyyMMdd'),'dd/MM/yyyy')||'-'||ORARIO||'<br >'||TAB_FILIALI.NOME||'-'||APPUNTAMENTI_SPORTELLI.DESCRIZIONE AS APPUNTAMENTO  " _
                & " FROM SISCOM_MI.APPUNTAMENTI_CALL_CENTER,SISCOM_MI.APPUNTAMENTI_ORARI,SISCOM_MI.APPUNTAMENTI_SPORTELLI,SISCOM_MI.TAB_FILIALI  " _
                & " WHERE ID_SEGNALAZIONE= " & vIdSegnalazione _
                & " AND APPUNTAMENTI_ORARI.ID=APPUNTAMENTI_CALL_CENTER.ID_ORARIO  " _
                & " AND APPUNTAMENTI_SPORTELLI.ID=APPUNTAMENTI_CALL_CENTER.ID_SPORTELLO " _
                & " AND TAB_FILIALI.ID=APPUNTAMENTI_CALL_CENTER.ID_STRUTTURA " _
                & " AND DATA_ELIMINAZIONE IS NULL " _
                & " ORDER BY DATA_APPUNTAMENTO DESC, ORARIO DESC "
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore("APPUNTAMENTO"), "0") = "0" Then
                    lblAppuntamento.Text = "<a href=""javascript:ApriAgenda();void(0);"">Richiedi appuntamento</a>"
                    appuntamentoPresente.Value = "0"
                Else
                    lblAppuntamento.Text = "<a href=""javascript:ApriAgenda();void(0);"">" & lettore("APPUNTAMENTO") & "</a>"
                    appuntamentoPresente.Value = "1"
                End If
            Else
                lblAppuntamento.Text = "<a href=""javascript:ApriAgenda();void(0);"">Richiedi appuntamento</a>"
                appuntamentoPresente.Value = "0"
            End If
            lettore.Close()

            If ConnOpenNow = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If CheckControl() = True Then

            If vIdSegnalazione = "-1" Then
                SalvaSegnalazione()
                Me.btnSollecito.Visible = True
                Me.imgChiudiSegnalazione.Visible = True
                Me.btnAnnulla.Visible = True

            Else
                UpdateSegnalazione()
            End If
            If IsNumeric(vIdSegnalazione) AndAlso vIdSegnalazione > 0 AndAlso tipoS = "0" Then
                ControllaAppuntamento()
                lblAppuntamento.Visible = True
            Else
                lblAppuntamento.Visible = False
            End If
        End If

    End Sub
    Private Sub SalvaSegnalazione()
        Try
            If Not String.IsNullOrEmpty(Me.lblDataIns.Text) And Not String.IsNullOrEmpty(Me.txtOra.Text) Then

                '************CONNESSIONE E TRANSAZIONE********************

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                     par.SettaCommand(par)
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                '******************TIPO OPERATORE***************
                Dim STATOS As String = "0"
                Dim ORIGINE As String = "A"


                'If Session.Item("OP_COM") = "0" Then
                '    STATOS = "0"
                '    ORIGINE = "A"
                'Else
                '    STATOS = "-1"
                '    ORIGINE = "C"
                'End If
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select siscom_mi.seq_segnalazioni.nextval from dual"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    vIdSegnalazione = par.IfNull(lettore(0), "-1")
                    id.Value = vIdSegnalazione
                Else
                    Response.Write("<script>alert('Impossibile recuperare un id per la nuova segnalazione')</script>")
                End If
                lettore.Close()

                Dim valoreUrgenza As String = "NULL"
                If tipoS = "1" Then
                    valoreUrgenza = cmbUrgenza.SelectedIndex
                End If

                par.cmd.CommandText = "insert into siscom_mi.segnalazioni(id,id_stato,id_edificio,id_unita,cognome_rs,data_ora_richiesta,telefono1,telefono2,mail," _
                                    & "descrizione_ric,id_operatore_ins,nome,tipo_richiesta,origine,id_tipologie,TIPO_PERVENUTA,id_struttura,ID_PERICOLO_SEGNALAZIONE,ID_TIPO_SEGNALAZIONE) values " _
                                    & "(" & vIdSegnalazione & "," & STATOS & "," & RitornaNullSeMenoUno(Me.cmbEdificio.SelectedValue) & "," & par.IfEmpty(ZeroToEmpty(unita.Value), "null") _
                                    & ",'" & par.PulisciStrSql(Me.txtCognChiama.Text.ToUpper) & "','" & par.AggiustaData(Me.lblDataIns.Text) & Me.txtOra.Text.Replace(":", "").Replace(".", "") & "'" _
                                    & ",'" & par.PulisciStrSql(Me.txtTel1.Text) & "','" & par.PulisciStrSql(Me.txtTel2.Text) & "','" & par.PulisciStrSql(Me.txtMail.Text) & "'" _
                                    & ",'" & par.PulisciStrSql(txtDescrizione.Text) & "'," & Session.Item("ID_OPERATORE") & ",'" & par.PulisciStrSql(Me.txtNomeChiama.Text.ToUpper) & "'" _
                                    & "," & Me.cmbTipoRichiesta.SelectedValue & ",'" & ORIGINE & "'," & RitornaNullSeMenoUno(Me.cmbTipoIntervento.SelectedValue) & ",'" & Me.cmbTipoPervenuta.SelectedValue _
                                    & "', " & RitornaNullSeMenoUno(Me.cmbStruttura.SelectedValue) & "," & valoreUrgenza & "," & cmbTipoSegnalazione.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()

                WriteEvent("F55", "NUOVA SEGNALAZIONE")


                If txtNote.Text <> "" Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
                                        & "VALUES (" & vIdSegnalazione & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                    par.cmd.ExecuteNonQuery()
                    txtNote.Text = ""
                    WriteEvent("F02", "NOTA SEGNALAZIONE")

                End If




                par.myTrans.Commit()
                Me.lblNrich.Text = vIdSegnalazione
                Response.Write("<script>alert('Operazione effettuata!');</script>")

                CaricaTabellaNote(vIdSegnalazione)
                CaricaElencoAllegati()


                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If

                Me.txtTel1.ReadOnly = True
                Me.txtTel2.ReadOnly = True
                Me.txtMail.ReadOnly = True
                Me.btnStampSopr.Visible = True
            Else
                Response.Write("<script>alert('Definire correttamente la data e l\'ora della segnalazione!');</script>")

            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub
    Function ZeroToEmpty(ByVal a As String) As String
        ZeroToEmpty = a
        If a = "0" Then
            a = ""
        End If
        Return a
    End Function
    Private Sub UpdateSegnalazione()
        Try

            '*******************CONNESSIONE e TRANSAZIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans


            Dim valoreTipoIntervento As String = "NULL"
            Dim valoreUrgenza As String = "NULL"
            If cmbTipoSegnalazione.SelectedValue = "1" Then
                valoreUrgenza = cmbUrgenza.SelectedIndex
                valoreTipoIntervento = cmbTipoIntervento.SelectedValue
            End If
            If cmbTipoSegnalazione.SelectedValue <> "0" Then
                par.cmd.CommandText = "UPDATE SISCOM_MI.APPUNTAMENTI_CALL_cENTER SET ID_OPERATORE_ELIMINAZIONE=" & Session.Item("ID_OPERATORE") & ",DATA_ELIMINAZIONE=" & Format(Now, "yyyyMMddHHmmss") & " WHERE ID_SEGNALAZIONE=" & vIdSegnalazione
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "update siscom_mi.segnalazioni " _
                & "set ID_PERICOLO_sEGNALAZIONE=" & valoreUrgenza _
                & ", DATA_ORA_RICHIESTA = '" & par.AggiustaData(Me.lblDataIns.Text) & Me.txtOra.Text.Replace(":", "").Replace(".", "") _
                & "', descrizione_ric = '" & par.PulisciStrSql(Me.txtDescrizione.Text) _
                & "',tipo_pervenuta = '" & Me.cmbTipoPervenuta.SelectedValue _
                & "',ID_TIPO_SEGNALAZIONE= " & Me.cmbTipoSegnalazione.SelectedValue _
                & ",ID_TIPOLOGIE= " & valoreTipoIntervento _
                & " where id =" & vIdSegnalazione
            par.cmd.ExecuteNonQuery()
            WriteEvent("F02", "AGGIORNAMENTO DATI DELLA SEGNALAZIONE")


            If txtNote.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) " _
                                    & "VALUES (" & vIdSegnalazione & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                par.cmd.ExecuteNonQuery()
                txtNote.Text = ""
                WriteEvent("F02", "NOTA SEGNALAZIONE")

            End If

            tipoS = cmbTipoSegnalazione.SelectedValue


            par.myTrans.Commit()
            Me.lblNrich.Text = vIdSegnalazione
            Response.Write("<script>alert('Operazione effettuata!');</script>")

            CaricaTabellaNote(vIdSegnalazione)
            CaricaElencoAllegati()


            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If




        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub
    Private Function RicavaFile(ByVal sFile) As String
        Dim N

        For N = Len(sFile) To 1 Step -1
            If Mid(sFile, N, 1) = "\" Then
                Exit For
            End If
        Next

        RicavaFile = Right(sFile, Len(sFile) - N)

    End Function

    Private Sub CaricaElencoAllegati()
        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If


            Dim I As Integer = 0
            Dim MIOCOLORE As String = ""
            'Dim ElencoFile() As String
            Dim j As Integer

            TabellaAllegati = "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='200px'>ALLEGATO</td><td width='200px'>DESCRIZIONE</td><td width='150px'>DATA-ORA</td></tr>"

            I = 0
            MIOCOLORE = "#FFFFFF"



            'For Each foundFile As String In My.Computer.FileSystem.GetFiles(Server.MapPath("../ALLEGATI/SEGNALAZIONI/"), FileIO.SearchOption.SearchTopLevelOnly, id.Value & "_*.zip")
            '    ReDim Preserve ElencoFile(I)
            '    ElencoFile(I) = foundFile
            '    I = I + 1
            'Next

            'Dim kk As Long
            'Dim jj As Long
            'Dim scambia

            'For kk = 0 To I - 2
            '    For jj = kk + 1 To I - 1
            '        If CLng(Mid(RicavaFile(ElencoFile(kk)), InStr(RicavaFile(ElencoFile(kk)), "_") + 1, 12)) < CLng(Mid(RicavaFile(ElencoFile(jj)), InStr(RicavaFile(ElencoFile(jj)), "_") + 1, 12)) Then
            '            scambia = ElencoFile(kk)
            '            ElencoFile(kk) = ElencoFile(jj)
            '            ElencoFile(jj) = scambia
            '        End If
            '    Next
            'Next

            'Dim NomeFile As String = ""
            'Dim DataFile As String = ""
            'Dim descrizione As String = ""

            'If I > 0 Then
            par.cmd.CommandText = "select * from siscom_mi.allegati_segnalazioni where id_segnalazione = " & vIdSegnalazione

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            Dim dt As New Data.DataTable
            da.Fill(dt)

            Dim nomeFileAll As String = ""
            Dim dataFileAll As String = ""
            Dim descrizioneAll As String = ""
            Dim idSegnalazione As String = ""
            For Each elemento As Data.DataRow In dt.Rows
                nomeFileAll = par.IfNull(elemento.Item("NOME_FILE"), "")
                dataFileAll = par.IfNull(elemento.Item("DATA_ORA"), "")
                descrizioneAll = par.IfNull(elemento.Item("DESCRIZIONE"), "")
                idSegnalazione = par.IfNull(elemento.Item("ID_SEGNALAZIONE"), "")

                If File.Exists(Server.MapPath("~\/ALLEGATI\/SEGNALAZIONI\/") & idSegnalazione & "_" & dataFileAll & "-" & nomeFileAll & ".zip") Then
                    TabellaAllegati = TabellaAllegati & "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'" _
                                                  & "><td style='border-bottom-style: solid; border-bottom-width: 1px; " _
                                                  & "border-bottom-color: #000000;' width='200px'><a  alt='Download' href='../ALLEGATI/SEGNALAZIONI/" & idSegnalazione & "_" & dataFileAll & "-" & nomeFileAll & ".zip' target='_blank'>" _
                                                  & nomeFileAll & "</a></td><td style='border-bottom-style: solid; border-bottom-width: 1px; " _
                                                  & "border-bottom-color: #000000;' width='200px'>" & descrizioneAll & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & dataFileAll & "</td></tr>"
                End If

            Next




            'For j = 0 To I - 1
            '    NomeFile = Mid(UCase(RicavaFile(ElencoFile(j))), InStr(RicavaFile(ElencoFile(j)), "-") + 1, 20)
            '    NomeFile = Mid(NomeFile, 1, Len(NomeFile) - 4)
            '    DataFile = par.FormattaData(Mid(UCase(RicavaFile(ElencoFile(j))), InStr(RicavaFile(ElencoFile(j)), "_") + 1, 8))
            '    If dt.Select("NOME_FILE ='" & NomeFile & "'").Length > 0 Then
            '        descrizione = par.IfNull(dt.Select("NOME_FILE ='" & NomeFile & "'")(0).Item("descrizione"), "---")
            '    Else
            '        descrizione = ""
            '    End If


            'Next j
            'End If


            TabellaAllegati = TabellaAllegati & "</table>"



            If ConnOpenNow = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
            TabellaAllegati = TabellaAllegati & "</table>"

        End Try

    End Sub

    Private Sub CaricaTabellaNote(ByVal IdNota As String)
        '************CONNESSIONE ********************


        TabellaNote = "<table cellpadding='0' cellspacing='0' style='width:95%;'><tr style='font-family: ARIAL; font-size: 8pt; font-weight: bold'><td width='100px'>DATA-ORA</td><td width='150px'>OPERATORE</td><td>NOTE</td></tr>"

        Try
            Dim ConnOpenNow As Boolean = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
                ConnOpenNow = True
            End If

            par.cmd.CommandText = "SELECT segnalazioni_note.*,operatori.operatore FROM sepa.operatori,siscom_mi.segnalazioni_note where segnalazioni_note.id_segnalazione=" & IdNota & " and segnalazioni_note.id_operatore=operatori.id (+) order by data_ora desc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("NOTE"), "").ToString <> "" Then
                    TabellaNote = TabellaNote & "<tr style='height: 16px;font-family: ARIAL; font-size: 8pt;'><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='100px'>" & par.FormattaData(Mid(par.IfNull(myReader1("data_ora"), ""), 1, 8)) & " " & Mid(par.IfNull(myReader1("data_ora"), ""), 9, 2) & ":" & Mid(par.IfNull(myReader1("data_ora"), ""), 11, 2) & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;' width='150px'>" & par.IfNull(myReader1("operatore"), "") & "</td><td style='border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #000000;'>" & Replace(par.IfNull(myReader1("note"), ""), vbCrLf, "</br>") & "</td></tr>"
                End If
                'If myReader1("SOLLECITO") <> 0 Then
                '    lblsollecito.Visible = True
                '    sollecito = "SOLLECITO N°: "
                '    lblsollecito.Text = sollecito
                '    'txtSollecito.Visible = True
                '    soll = True
                'Else
                '    lblsollecito.Visible = True
                '    lblsollecito.Text = "<b>NON SOLLECITATA</b>"
                'End If
            End While

            myReader1.Close()

            'If soll = True Then
            '    par.cmd.CommandText = "SELECT max(rownum) as SOLL from SISCOM_MI.SEGNALAZIONI_NOTE WHERE SISCOM_MI.SEGNALAZIONI_NOTE.SOLLECITO = 1 AND SISCOM_MI.SEGNALAZIONI_NOTE.ID_SEGNALAZIONE=" & id.Value
            '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    If myReader2.Read() Then
            '        lblsollecito.Visible = True
            '        'txtSollecito.Visible = True
            '        lblsollecito.Text = sollecito & " <b>" & myReader2("SOLL") & "</b>"
            '    End If
            '    myReader2.Close()
            'End If

            If ConnOpenNow = True Then
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If
            End If

        Catch ex As Exception

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

        TabellaNote = TabellaNote & "</table>"

    End Sub

    Private Function CheckControl() As Boolean
        CheckControl = True
        Try
            If String.IsNullOrEmpty(Me.txtDescrizione.Text) Then
                CheckControl = False
                Response.Write("<script>alert('Inserire la descrizione della richiesta!')</script>")
            End If

            If Me.cmbTipoRichiesta.SelectedValue = 0 Then
                CheckControl = False
                Response.Write("<script>alert('Definire il tipo di richiesta!')</script>")
            End If

            If tipos = "1" And Me.cmbTipoIntervento.SelectedValue = "-1" Then
                CheckControl = False
                Response.Write("<script>alert('Definire il tipo di intervento!')</script>")
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

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        Try
            If valorepass = "-1" Then
                a = "Null"
            ElseIf valorepass <> "-1" Then
                a = "'" & valorepass & "'"
            End If

        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
        Return a

    End Function

    Protected Sub img_InserisciBolletta_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciBolletta.Click
        Dim nFile As String = ""
        Try


            If FileUpload1.HasFile = True Then
                If txtDescrizioneA.Text <> "" Then

                    Dim ConnOpenNow As Boolean = False
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                         par.SettaCommand(par)
                        ConnOpenNow = True
                    End If

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.ALLEGATI_SEGNALAZIONI WHERE NOME_FILE = '" & par.PulisciStrSql(Me.txtDescrizioneA.Text.ToUpper) & "' AND ID_SEGNALAZIONE = " & vIdSegnalazione
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.HasRows Then
                        Response.Write("<script>alert('Esiste già un allegato con lo stesso nome!\nImpossibile procedere!');</script>")
                        Exit Sub
                    Else
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.ALLEGATI_SEGNALAZIONI (ID,NOME_FILE,ID_SEGNALAZIONE,DATA_ORA,DESCRIZIONE) VALUES " _
                                            & "(SISCOM_MI.SEQ_SEGNALAZIONI.NEXTVAL,'" & par.PulisciStrSql(Me.txtDescrizioneA.Text.ToUpper) & "'," & vIdSegnalazione & "," _
                                            & "'" & Format(Now, "yyyyMMddHHmmss") & "', '" & par.PulisciStrSql(Me.txtDescrizioneAll.Text.ToUpper) & "')"
                        par.cmd.ExecuteNonQuery()

                    End If
                    lettore.Close()
                    If ConnOpenNow = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If





                    nFile = Server.MapPath("..\ALLEGATI\CONTRATTI\" & FileUpload1.FileName)
                    FileUpload1.SaveAs(nFile)
                    Dim objCrc32 As New Crc32()
                    Dim strmZipOutputStream As ZipOutputStream
                    Dim zipfic As String
                    zipfic = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\" & id.Value & "_" & Format(Now, "yyyyMMddHHmmss") & "-" & UCase(txtDescrizioneA.Text) & ".zip")
                    strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                    strmZipOutputStream.SetLevel(6)
                    Dim strFile As String
                    strFile = nFile
                    Dim strmFile As FileStream = File.OpenRead(strFile)
                    Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                    strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                    Dim sFile As String = Path.GetFileName(strFile)
                    Dim theEntry As ZipEntry = New ZipEntry(sFile)
                    Dim fi As New FileInfo(strFile)
                    theEntry.DateTime = fi.LastWriteTime
                    theEntry.Size = strmFile.Length
                    strmFile.Close()
                    objCrc32.Reset()
                    objCrc32.Update(abyBuffer)
                    theEntry.Crc = objCrc32.Value
                    strmZipOutputStream.PutNextEntry(theEntry)
                    strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                    File.Delete(strFile)

                    If Not String.IsNullOrEmpty(Me.txtDescrizioneAll.Text) Then
                        Dim fileDescrizione As String = ""

                        fileDescrizione = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\") & UCase(txtDescrizioneA.Text) & Format(Now, "yyyyMMdd") & "_DESCRIZIONE.txt"
                        Dim sr As StreamWriter = New StreamWriter(fileDescrizione, False, System.Text.Encoding.Default)
                        sr.WriteLine("Data del documento:" & par.FormattaData(Format(Now, "yyyyMMdd")) & vbCrLf & "DESCRIZIONE:" & vbCrLf & txtDescrizioneAll.Text.ToUpper)
                        sr.Close()

                        strFile = Server.MapPath("..\ALLEGATI\SEGNALAZIONI\") & UCase(txtDescrizioneA.Text) & Format(Now, "yyyyMMdd") & "_DESCRIZIONE.txt"
                        strmFile = File.OpenRead(strFile)
                        Dim abyBuffer12(Convert.ToInt32(strmFile.Length - 1)) As Byte
                        strmFile.Read(abyBuffer12, 0, abyBuffer12.Length)
                        Dim sFile12 As String = Path.GetFileName(strFile)
                        theEntry = New ZipEntry(sFile12)
                        fi = New FileInfo(strFile)
                        theEntry.DateTime = fi.LastWriteTime
                        theEntry.Size = strmFile.Length
                        strmFile.Close()
                        objCrc32.Reset()
                        objCrc32.Update(abyBuffer12)
                        theEntry.Crc = objCrc32.Value
                        strmZipOutputStream.PutNextEntry(theEntry)
                        strmZipOutputStream.Write(abyBuffer12, 0, abyBuffer12.Length)
                        File.Delete(strFile)

                    End If
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()






                    Response.Write("<script>alert('Operazione Effettuata!');</script>")

                    WriteEvent("F02", "AGGIUNTO ALLEGATO")


                Else
                    Response.Write("<script>alert('Inserire il nome!');</script>")
                End If
            End If
            CaricaTabellaNote(id.Value)
            CaricaElencoAllegati()
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try

    End Sub

    Protected Sub btnSalvaSoll_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaSoll.Click
        Try
            par.OracleConn.Open()
             par.SettaCommand(par)
            If txtNoteSoll.Text <> "" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,SOLLECITO) VALUES (" & id.Value & ", '" & par.PulisciStrSql(txtNoteSoll.Text) & " (nota di sollecito) ', '" & Format(Now, "yyyyMMddHHmm") & "'," & Session.Item("ID_OPERATORE") & ", 1)"
                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Segnalazione sollecitata correttamente!')</script>")
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,SOLLECITO) VALUES (" & id.Value & ", '" & par.PulisciStrSql(txtNoteSoll.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "'," & Session.Item("ID_OPERATORE") & ", 1)"
                par.cmd.ExecuteNonQuery()
                Response.Write("<script>alert('Segnalazione sollecitata correttamente!')</script>")
            End If
            par.cmd.CommandText = "update siscom_mi.segnalazioni set fl_sollecito = 1 where id = " & vIdSegnalazione
            par.cmd.ExecuteNonQuery()
            Me.lblsollecito.Text = "SOLLECITATA"
            Me.lblsollecito.ForeColor = Drawing.Color.Red

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            CaricaTabellaNote(id.Value)
            CaricaElencoAllegati()
            txtNoteSoll.Text = ""

            WriteEvent("F02", "SEGNALAZIONE SOLLECITATA")



        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")

        End Try


    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If sicuro.Value = "1" Then
            Try
                par.OracleConn.Open()
                 par.SettaCommand(par)

                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET ID_OPERATORE_CH=" & Session.Item("ID_OPERATORE") _
                & ", ID_STATO=2 WHERE ID=" & id.Value
                par.cmd.ExecuteNonQuery()

                StatoSegnalazione()
                WriteEvent("F02", "ANNULLO SEGNALAZIONE")

                par.myTrans.Commit()
                par.cmd.Dispose()

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaTabellaNote(id.Value)
                CaricaElencoAllegati()
                FrmSoloLettura()
                Response.Write("<script>alert('Operazione Effettuata!');</script>")

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")


            End Try
        End If

    End Sub
    Private Sub StatoSegnalazione()

        par.cmd.CommandText = "select tab_stati_segnalazioni.id,tab_stati_segnalazioni.descrizione from siscom_mi.tab_stati_segnalazioni,siscom_mi.segnalazioni where tab_stati_segnalazioni.id=segnalazioni.id_stato and segnalazioni.id=" & id.Value
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        Dim idStato As String = 0
        If myReader1.Read Then
            lblStato.Text = par.IfNull(myReader1("descrizione"), "")
            idStato = myReader1("id")

            If myReader1("id") = 2 Or myReader1("id") = 10 Then
                Me.btnSollecito.Visible = False
            End If
        End If
        myReader1.Close()

        Select Case idStato
            Case 0
            Case 1
                Me.imgChiudiSegnalazione.Visible = False
                Me.btnAnnulla.Visible = False
            Case 2
                FrmSoloLettura()
            Case 3
                Me.imgChiudiSegnalazione.Visible = False
                Me.btnAnnulla.Visible = False
            Case 5
                FrmSoloLettura()
                Me.imgChiudiSegnalazione.Visible = True
                Me.cmbNoteChiusura.Enabled = True
                Me.txtDescNoteChiusura.Enabled = True
                Me.txtOraCInt.Enabled = True
                Me.txtDataCInt.Enabled = True
            Case 4
                FrmSoloLettura()
            Case 10
                'Me.imgChiudiSegnalazione.Visible = False
                FrmSoloLettura()

        End Select

        'Un operatore COMUNALE può chiudere usando il modulo CALL CENTER solo le richieste che ha generato lui o un altro operatore comunale
        If Session.Item("ID_CAF") = 6 Then
            par.cmd.CommandText = "select id_caf from operatori where id in (select distinct id_operatore from siscom_mi.eventi_segnalazioni where id_segnalazione = " & id.Value & " and cod_evento = 'F55')"
            myReader1 = par.cmd.ExecuteReader
            While myReader1.Read
                If par.IfNull(myReader1("id_caf"), 0) <> 6 Then
                    Me.imgChiudiSegnalazione.Visible = False
                    Exit While
                End If

            End While
            myReader1.Close()
        End If


    End Sub

    Protected Sub imgConferma_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgConferma.Click
        If sicuro.Value = "1" Then
            Try
                par.OracleConn.Open()
                 par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=0 where id=" & id.Value
                par.cmd.ExecuteNonQuery()

                StatoSegnalazione()

                par.myTrans.Commit()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                imgConferma.Visible = False
                btnAnnulla.Visible = False

                CaricaTabellaNote(id.Value)
                CaricaElencoAllegati()

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")

            End Try

        End If

    End Sub

    Protected Sub imgChiudiSegnalazione_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgChiudiSegnalazione.Click
        'If sicuro.Value = "1" Then

        '    Try
        '        par.OracleConn.Open()
        '         par.SettaCommand(par)
        '        par.myTrans = par.OracleConn.BeginTransaction()
        '        ‘‘par.cmd.Transaction = par.myTrans

        '        par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10,DATA_CHIUSURA = '" & Format(Now, "yyyyMMdd") & "' where id=" & id.Value
        '        par.cmd.ExecuteNonQuery()

        '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,ID_TIPO_SEGNALAZIONE) " _
        '        & "VALUES (" & vIdSegnalazione & ", '" & par.PulisciStrSql(txtNote.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
        '        par.cmd.ExecuteNonQuery()
        '        txtNote.Text = ""



        '        StatoSegnalazione()


        '        par.myTrans.Commit()
        '        par.cmd.Dispose()
        '        par.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '        Response.Write("<script>alert('Operazione Effettuata!');</script>")
        '        Me.btnSollecito.Visible = False
        '        WriteEvent("F02", "SEGNALAZIONE CHIUSA")
        '        CaricaTabellaNote(vIdSegnalazione)

        '    Catch ex As Exception
        '        par.myTrans.Rollback()
        '        par.OracleConn.Close()
        '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")


        '    End Try
        'End If

    End Sub

    Private Sub FrmSoloLettura()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
        Me.btnSalva.Visible = False
        Me.imgAllega.Visible = False
        Me.btnAnnulla.Visible = False
        Me.imgChiudiSegnalazione.Visible = False
        Me.btnSalvaSoll.Visible = False
        Me.btnSollecito.Visible = False
        cmbUrgenza.Enabled = False
        lblAppuntamento.Text = par.EliminaLink(lblAppuntamento.Text)
    End Sub

    Protected Sub WriteEvent(ByVal cod As String, ByVal motivo As String)
        Dim connOpNow As Boolean = False

        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
                connOpNow = True
            End If


            par.cmd.CommandText = "INSERT INTO SISCOM_MI.eventi_segnalazioni (id_segnalazione,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
            & "VALUES ( " & vIdSegnalazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', " _
            & "'" & cod & "','" & par.PulisciStrSql(motivo) & "')"

            par.cmd.ExecuteNonQuery()

            If par.OracleConn.State = Data.ConnectionState.Open And connOpNow = True Then
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
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")


        End Try
    End Sub

    Protected Sub btnStampSopr_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnStampSopr.Click
        'Response.Write("<script>alert('Funzione non disponibile!')</script>")
        Try


            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                 par.SettaCommand(par)
            End If

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\TestoModelli\SopralluogoCallC.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            Dim richiesta As String = ""
            Dim note As String = ""
            Dim condominio As String = ""
            Dim gestAuto As String = ""
            Dim sfratto As String = ""
            Dim morosità As String = ""
            Dim sloggio As String = ""
            Dim idContratto As String = ""
            par.cmd.CommandText = "select * from siscom_mi.segnalazioni where id = " & vIdSegnalazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then

                contenuto = Replace(contenuto, "$nrichiesta$", par.IfNull(lettore("ID"), ""))
                contenuto = Replace(contenuto, "$datarichiesta$", par.FormattaData(Mid(par.IfNull(lettore("DATA_ORA_RICHIESTA"), "                 "), 1, 8)))
                contenuto = Replace(contenuto, "$descrizione$", par.IfNull(lettore("descrizione_ric"), ""))
                contenuto = Replace(contenuto, "$richiedente$", par.IfNull(lettore("COGNOME_RS"), "") & " " & par.IfNull(lettore("NOME"), ""))

                contenuto = Replace(contenuto, "$numerotelefono1$", par.IfNull(lettore("TELEFONO1"), ""))
                contenuto = Replace(contenuto, "$numerotelefono2$", par.IfNull(lettore("TELEFONO2"), ""))



                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_SOPRALLUOGO WHERE ID_SEGNALAZIONE = " & vIdSegnalazione
                Dim myreader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If myreader.Read Then

                    contenuto = Replace(contenuto, "$rapporto$", par.IfNull(myreader("rapporto"), ""))
                    contenuto = Replace(contenuto, "$tecnico$", par.IfNull(myreader("tecnico"), ""))

                    If myreader("fl_pericolo") = 1 Then
                        contenuto = Replace(contenuto, "$pericolo$", "SI")
                    ElseIf myreader("fl_pericolo") = 0 Then
                        contenuto = Replace(contenuto, "$pericolo$", "NO")
                    Else
                        contenuto = Replace(contenuto, "$pericolo$", "")

                    End If
                Else
                    contenuto = Replace(contenuto, "$pericolo$", "")
                    contenuto = Replace(contenuto, "$rapporto$", "&nbsp; ")
                    contenuto = Replace(contenuto, "$tecnico$", "")

                End If
                myreader.Close()

                If par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "1" Then
                    richiesta = "SEGNALAZIONE GUASTI"
                    par.cmd.CommandText = "select descrizione from siscom_mi.tipologie_guasti where id = " & par.IfNull(lettore("id_tipologie"), "0")
                    myreader = par.cmd.ExecuteReader
                    If myreader.Read Then
                        richiesta = richiesta & " - " & par.IfNull(myreader("descrizione"), "")
                    End If
                    myreader.Close()
                ElseIf par.IfNull(lettore("TIPO_RICHIESTA"), "-1") = "0" Then
                    richiesta = "RICHIESTA INFORMAZIONI"

                End If
                contenuto = Replace(contenuto, "$tiporichiesta$", richiesta)

                par.cmd.CommandText = "SELECT NOTE FROM SISCOM_MI.SEGNALAZIONI_NOTE WHERE ID_SEGNALAZIONE = " & vIdSegnalazione
                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    note = note & par.IfNull(myreader("note"), "") & "<br/>"
                End While
                myreader.Close()
                contenuto = Replace(contenuto, "$note$", note)

                Dim indirizzo As String = ""
                par.cmd.CommandText = "SELECT COD_EDIFICIO,DENOMINAZIONE FROM SISCOM_MI.EDIFICI WHERE ID = " & par.IfNull(lettore("id_edificio"), "0")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = par.IfNull(myreader("denominazione"), "")
                    contenuto = Replace(contenuto, "$edificio$", "EDIFICIO COD." & par.IfNull(myreader("cod_edificio"), ""))
                End If
                myreader.Close()

                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.cod_unita_immobiliare, UNITA_IMMOBILIARI.interno,TIPO_LIVELLO_PIANO.descrizione AS PIANO,SCALE_EDIFICI.descrizione AS SCALA, " _
                                    & "siscom_mi.Getintestatari(id_contratto) AS intestatario " _
                                    & "FROM siscom_mi.UNITA_IMMOBILIARI,siscom_mi.SCALE_EDIFICI,siscom_mi.TIPO_LIVELLO_PIANO, siscom_mi.UNITA_CONTRATTUALE " _
                                    & "WHERE UNITA_IMMOBILIARI.id_scala = SCALE_EDIFICI.ID (+) " _
                                    & "AND UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO = TIPO_LIVELLO_PIANO.COD(+) " _
                                    & "AND UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.id_unita(+)" _
                                    & "AND UNITA_IMMOBILIARI.ID = " & par.IfNull(lettore("id_unita"), 0)
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    indirizzo = indirizzo & " " & "SCALA: " & par.IfNull(myreader("SCALA"), "--") & " PIANO: " & par.IfNull(myreader("PIANO"), "--") & " INTERNO:" & par.IfNull(myreader("interno"), "--")
                    contenuto = Replace(contenuto, "$unita$", "U.I. cod." & par.IfNull(myreader("COD_UNITA_IMMOBILIARE"), ""))
                Else
                    contenuto = Replace(contenuto, "$unita$", "")

                End If
                myreader.Close()
                contenuto = Replace(contenuto, "$indirizzo$", indirizzo)

                par.cmd.CommandText = "Select nome from siscom_mi.tab_filiali Where id = " & par.IfNull(lettore("id_struttura"), "")
                myreader = par.cmd.ExecuteReader
                If myreader.Read Then
                    contenuto = Replace(contenuto, "$struttura$", "STRUTTURA: " & par.IfNull(myreader("nome"), ""))
                End If
                myreader.Close()


                par.cmd.CommandText = "SELECT ID_CONTRATTO, RAPPORTI_UTENZA.COD_CONTRATTO, rapporti_utenza.data_riconsegna, siscom_mi.Getintestatari (id_contratto) AS intestatario, " _
                                    & "SISCOM_MI.Getstatocontratto(ID_CONTRATTO) AS STATO,TIPOLOGIA_CONTRATTO_LOCAZIONE.DESCRIZIONE AS TIPO_CONTRATTO " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & "AND NVL(DATA_RICONSEGNA,'50000000')=(" _
                                    & "SELECT MAX(NVL(DATA_RICONSEGNA,'50000000')) " _
                                    & "FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE ,SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE " _
                                    & "WHERE RAPPORTI_UTENZA.ID = UNITA_CONTRATTUALE.ID_CONTRATTO AND TIPOLOGIA_CONTRATTO_LOCAZIONE.COD = COD_TIPOLOGIA_CONTR_LOC " _
                                    & "AND ID_UNITA =" & par.IfNull(lettore("id_unita"), 0) _
                                    & ")"
                myreader = par.cmd.ExecuteReader
                Dim datiCont As String = ""
                If myreader.Read Then
                    idContratto = par.IfNull(myreader("ID_CONTRATTO"), "")
                    datiCont = "CONTRATTO: " & par.IfNull(myreader("tipo_contratto"), "") & " " & par.IfNull(myreader("cod_contratto"), "") & " Stato Contratto: " & par.IfNull(myreader("stato"), "") & " Saldo: " & Format(par.CalcolaSaldoAttuale(par.IfNull(myreader("ID_CONTRATTO"), "0")), "##,##0.00") & " Euro "
                    contenuto = Replace(contenuto, "$intestatario$", par.IfNull(myreader("intestatario"), ""))
                    If par.IfNull(myreader("data_riconsegna"), "") <> "" Then
                        sloggio = "SLOGGIO: " & par.FormattaData(par.IfNull(myreader("data_riconsegna"), ""))
                    End If
                Else
                    contenuto = Replace(contenuto, "$intestatario$", "")

                End If
                contenuto = Replace(contenuto, "$contratto$", datiCont)





                par.cmd.CommandText = "SELECT condomini.denominazione, (cond_amministratori.cognome || ' ' ||cond_amministratori.nome) AS amministratore " _
                                & "FROM siscom_mi.condomini,siscom_mi.cond_amministratori,siscom_mi.cond_amministrazione " _
                                & "WHERE condomini.ID =cond_amministrazione.id_condominio AND cond_amministratori.ID = id_amministratore AND cond_amministrazione.data_fine IS NULL " _
                                & "AND condomini.ID IN (SELECT id_condominio FROM siscom_mi.cond_edifici WHERE id_edificio = " & par.IfNull(lettore("id_edificio"), 0) & ")"

                myreader = par.cmd.ExecuteReader
                While myreader.Read
                    condominio = condominio & "CONDOMINIO: " & par.IfNull(myreader("denominazione"), "") & " AMMINISTRATORE: " & par.IfNull(myreader("amministratore"), "")
                End While
                myreader.Close()

                contenuto = Replace(contenuto, "$condomino$", condominio)
                contenuto = Replace(contenuto, "$gestauto$", gestAuto)
                contenuto = Replace(contenuto, "$morosità$", morosità)
                contenuto = Replace(contenuto, "$sfratto$", sfratto)
                contenuto = Replace(contenuto, "$sloggio$", sloggio)

                If idContratto <> "" Then
                    par.cmd.CommandText = "SELECT ID_MOROSITA ,(CASE WHEN COD_STATO = 'M20' THEN 'SI' ELSE 'NO' END)AS PRATICA_LEGALE FROM SISCOM_MI.MOROSITA_LETTERE where  cod_stato not in ('M94','M98','M100') and id_contratto =" & idContratto
                    myreader = par.cmd.ExecuteReader

                    If myreader.Read Then

                        morosità = "MESSA IN MORA "
                        If par.IfNull(myreader("pratica_legale"), "NO") = "SI" Then
                            morosità = morosità & "- AVVIATA PRATICA LEGALE"
                        End If
                    End If
                    myreader.Close()
                End If


            End If
            lettore.Close()


            Dim url As String = Server.MapPath("..\FileTemp\")
            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = False
            pdfConverter1.PdfDocumentOptions.LeftMargin = 30
            pdfConverter1.PdfDocumentOptions.RightMargin = 30
            pdfConverter1.PdfDocumentOptions.TopMargin = 30
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            'pdfConverter1.PdfFooterOptions.FooterText = ("Emesso da N° Matricola :" & Matricola)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            'pdfConverter1.PdfFooterOptions.PageNumberText = "pag. "
            'pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            'sostituire nuovo codice da qui
            Dim nomefile As String = vIdSegnalazione & "-" & Format(Now, "yyyyMMddHHmmss")
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile & ".pdf", Server.MapPath("..\IMG\"))

            Response.Write("<script>window.open('../FileTemp/" & nomefile & ".pdf','','');</script>")

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
    Private Sub RiempiNoteChiusura()
        Dim ConnOpenNow As Boolean = False

        Try

            If Me.cmbTipoIntervento.SelectedValue <> "-1" Then
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                     par.SettaCommand(par)
                    ConnOpenNow = True
                End If

                cmbNoteChiusura.Items.Add(New ListItem("--", "-1"))

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.SEGNALAZIONI_NOTE_CHIUSURA WHERE ID_TIPO_GUASTO = " & Me.cmbTipoIntervento.SelectedValue
                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While Lettore.Read
                    cmbNoteChiusura.Items.Add(New ListItem(par.IfNull(Lettore("descrizione"), "--"), par.IfNull(Lettore("descrizione"), "-1")))

                End While
                Lettore.Close()

                If ConnOpenNow = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End If


            End If
        Catch ex As Exception

            If ConnOpenNow = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)


        End Try
    End Sub

    Protected Sub img_InserisciBolletta0_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles img_InserisciBolletta0.Click
        If sicuro.Value = "1" Then

            Try
                par.OracleConn.Open()
                 par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans
                If par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") >= par.AggiustaData(Me.lblDataIns.Text) & Me.txtOra.Text.Replace(":", "").Replace(".", "") Then

                    par.cmd.CommandText = "UPDATE SISCOM_MI.SEGNALAZIONI SET id_stato=10,DATA_CHIUSURA = '" & par.AggiustaData(Me.txtDataCInt.Text) & Me.txtOraCInt.Text.Replace(":", "").Replace(".", "") & "' where id=" & id.Value
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.SEGNALAZIONI_NOTE (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE,ID_TIPO_SEGNALAZIONE_note) " _
                    & "VALUES (" & vIdSegnalazione & ", '" & par.PulisciStrSql(Me.txtDescNoteChiusura.Text) & "', '" & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ",2)"
                    par.cmd.ExecuteNonQuery()
                    txtNote.Text = ""



                    StatoSegnalazione()


                    par.myTrans.Commit()
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    Response.Write("<script>alert('Operazione Effettuata!');</script>")
                    Me.btnSollecito.Visible = False
                    WriteEvent("F02", "SEGNALAZIONE CHIUSA")
                    CaricaTabellaNote(vIdSegnalazione)
                Else
                    Response.Write("<script>alert('Attenzione...\nLa data e l\'ora di chiusura deve essere successiva a quella di apertura!\nImpossibile Procedere');</script>")

                End If

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")


            End Try
        End If

    End Sub

    Protected Sub btnControllaAppuntamento_Click(sender As Object, e As System.EventArgs) Handles btnControllaAppuntamento.Click
        ControllaAppuntamento()
    End Sub

    Protected Sub cmbTipoSegnalazione_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbTipoSegnalazione.SelectedIndexChanged
        If confermaGenerica.Value = "1" Then
            confermaGenerica.Value = "0"
            tipoS = cmbTipoSegnalazione.SelectedValue
            If IsNumeric(vIdSegnalazione) AndAlso vIdSegnalazione > 0 AndAlso tipoS = "0" Then
                ControllaAppuntamento()
                lblAppuntamento.Visible = True
            Else
                lblAppuntamento.Visible = False
            End If
            If tipoS = "1" Then
                cmbUrgenza.Visible = True
                lblUrgenza.Visible = True
                lblTipoIntervento.Visible = True
                cmbTipoIntervento.Visible = True
            Else
                cmbUrgenza.Visible = False
                lblUrgenza.Visible = False
                lblTipoIntervento.Visible = False
                cmbTipoIntervento.Visible = False
            End If
        Else
            cmbTipoSegnalazione.SelectedValue = "0"
            tipoS = cmbTipoSegnalazione.SelectedValue
        End If
    End Sub

    Protected Sub cmbInterno_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbInterno.SelectedIndexChanged

    End Sub
End Class
