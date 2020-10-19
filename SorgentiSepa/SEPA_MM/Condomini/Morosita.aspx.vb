Partial Class Condomini_Morosita
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Public Property vIdConnModale() As String
        Get
            If Not (ViewState("par_vIdConnModale") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnModale"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnModale") = value
        End Set

    End Property
    Public Property vIdMorosita() As String
        Get
            If Not (ViewState("par_vIdMorosita") Is Nothing) Then
                Return CStr(ViewState("par_vIdMorosita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdMorosita") = value
        End Set

    End Property

    Public Property vTipoCondominio() As String
        Get
            If Not (ViewState("par_vTipoCondominio") Is Nothing) Then
                Return CStr(ViewState("par_vTipoCondominio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vTipoCondominio") = value
        End Set

    End Property

    Public Property vIdCondominio() As String
        Get
            If Not (ViewState("par_vIdCondominio") Is Nothing) Then
                Return CStr(ViewState("par_vIdCondominio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdCondominio") = value
        End Set

    End Property

    Public Property vIdVisual() As String
        Get
            If Not (ViewState("par_vIdVisual") Is Nothing) Then
                Return CStr(ViewState("par_vIdVisual"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdVisual") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Not IsPostBack Then
            Try

                If Session.Item("OPERATORE") = "" Then
                    Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
                End If
                If Request.QueryString("IDCON") <> "" Then
                    vIdConnModale = Request.QueryString("IDCON")
                End If

                If Request.QueryString("IDCONDOMINIO") <> "" Then
                    vIdCondominio = Request.QueryString("IDCONDOMINIO")
                End If

                If Request.QueryString("IDVISUAL") <> "" Then
                    vIdVisual = Request.QueryString("IDVISUAL")
                End If

                Me.Session.Add("MODIFYMODAL", 0)
                txtDataRichiesta.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataArrivo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataArrivoAler.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataRifA.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataRifDa.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataRichRimborso.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataDoc.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataMandato.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtImportoMandato.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                txtImportoMandato.Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                txtNumeroMandato.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');")
                Me.btnConfermaDataDoc.Attributes.Add("onclick", "NuovoDettMorInquilini();")


                If Request.QueryString("IDMOROSITA") <> "" Then
                    vIdMorosita = Request.QueryString("IDMOROSITA")
                    ApriRicerca()
                Else
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    'CREO SUL DATABASE UN PALETTO PER IL ROLBACK FINO A QUI
                    par.cmd.CommandText = "SAVEPOINT MOROSITA"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_COND_MOROSITA.NEXTVAL FROM DUAL"
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader1.Read Then
                        vIdMorosita = myReader1(0)
                    End If
                    myReader1.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MOROSITA (ID,ID_CONDOMINIO) VALUES (" & vIdMorosita & "," & Request.QueryString("IDCONDOMINIO") & ")"
                    par.cmd.ExecuteNonQuery()

                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','INSERIMENTO MOROSITA SUL CONDOMINIO')"
                    par.cmd.ExecuteNonQuery()

                End If

                RiempiCampi()
                AggiornaComboDataDoc()
                CercaMorosita()
                'AddJavascriptFunction()
                'CONTROLLO SE GIA' AVVENUTA EMISSIONE MAV, IN TAL CASO BLOCCO IN SOLA LETTURA .
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE ID_MOROSITA = " & vIdMorosita & " AND BOLLETTINO IS NOT NULL OR BOLLETTINO <> ''"
                Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    FrmReadOnlyMorStampata()
                    'ReadOnlyxMoro.Value = 1
                    Me.ChkCompleto.Enabled = True

                End If
                myReader.Close()

                ''''''''******Se il form principale è in sola lettura anche i form chiamati vengono resi ReadOnly
                If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) And Session.Item("MOD_CONDOMINIO_MOR") = 0 Then
                    Me.btnSalvaCambioAmm.Visible = False
                    Me.btnDelete.Visible = False
                    SettaFrmReadOnly()
                End If

                If Session.Item("MOD_CONDOMINIO_MOR") = 1 Then
                    FrmReadOnlyMorStampata()
                    ReadOnlyxMoro.Value = 1
                    'SettaFrmReadOnly()
                    'Else
                    '    Me.ChkCompleto.Enabled = True
                Else
                    CampiMandatoLettura()
                End If
                ''''''''******FINE******

                If Session.Item("MOD_CONDOMINIO_MOR") <> 1 Then
                    par.cmd.CommandText = "SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita & ")"
                    myReader = par.cmd.ExecuteReader
                    If myReader.Read Then
                        If par.IfNull(myReader("id_pagamento"), 0) > 0 Then
                            SettaFrmReadOnly()
                            Response.Write("<script>alert('Impossibile modificare i dati della morosità, perchè è stato già emesso il pagamento!');</script>")
                            ReadOnlyxMoro.Value = 1
                        End If
                    End If
                    myReader.Close()
                End If


            Catch ex As Exception
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Redirect("..\Errore.aspx")
            End Try

        Else
        End If



    End Sub
    Private Sub RiempiCampi()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'par.cmd.CommandText = "SELECT ID, COGNOME, NOME FROM SISCOM_MI.COND_AMMINISTRATORI ORDER BY COGNOME ASC"
            'Me.cmbAmministratori.Items.Add(New ListItem(" ", -1))
            'While myReader1.Read
            '    'DdLComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            '    cmbAmministratori.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()

            par.caricaComboBox("SELECT ID, COGNOME ||' ' ||NOME as nominativo FROM SISCOM_MI.COND_AMMINISTRATORI ORDER BY COGNOME ASC", Me.cmbAmministratori, "ID", "nominativo", True)

            Me.cmbTipoInvio.Items.Add(New ListItem("", ""))
            Me.cmbTipoInvio.Items.Add(New ListItem("POSTA", "POSTA"))
            Me.cmbTipoInvio.Items.Add(New ListItem("FAX", "FAX"))
            Me.cmbTipoInvio.Items.Add(New ListItem("MAIL", "MAIL"))
            Me.cmbTipoInvio.Items.Add(New ListItem("A MANO", "A MANO"))

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If Request.QueryString("IDCONDOMINIO") <> "" Then
                par.cmd.CommandText = "SELECT ID_AMMINISTRATORE,TIPOLOGIA, DENOMINAZIONE,COMUNI_NAZIONI.nome AS comune  FROM SISCOM_MI.CONDOMINI,SISCOM_MI.COND_AMMINISTRAZIONE, sepa.COMUNI_NAZIONI WHERE condomini.cod_comune = COMUNI_NAZIONI.cod(+) and COND_AMMINISTRAZIONE.ID_CONDOMINIO = CONDOMINI.ID and data_fine is null AND condomini.ID = " & Request.QueryString("IDCONDOMINIO")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.lblTitolo.Text = "Morosità del Condominio: " & myReader1("DENOMINAZIONE") & " - " & myReader1("comune")
                    vTipoCondominio = myReader1("TIPOLOGIA")
                    Me.cmbAmministratori.SelectedValue = myReader1("ID_AMMINISTRATORE")
                End If
                myReader1.Close()
            End If


            '***********************controllo per campo totale editabile quando condominio senza patrimonio************************

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_UI WHERE ID_CONDOMINIO = " & par.IfEmpty(Request.QueryString("IDCONDOMINIO"), 0)

            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
            Else
                Me.txtTotale.Enabled = True
                Me.noPatrimonio.Value = 1
            End If
            myReader1.Close()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Private Sub AggiornaComboDataDoc()
        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            cmbDataDocum.Items.Clear()
            par.caricaComboBox("SELECT to_char(to_date(COND_MOROSITA_INQUILINI_DET.DATA,'yyyymmdd'),'dd/mm/yyyy') AS DATA FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_MOROSITA= " & vIdMorosita & " GROUP BY DATA ORDER BY DATA DESC", Me.cmbDataDocum, "DATA", "DATA", True)

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Private Sub ApriRicerca()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            'CREO SUL DATABASE UN PALETTO PER IL ROLBACK FINO A QUI
            par.cmd.CommandText = "SAVEPOINT MOROSITA"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtDataRichiesta.Text = par.FormattaData(par.IfNull(myReader1("DATA_RICHIESTA"), ""))
                Me.txtProtocollo.Text = par.IfNull(myReader1("PROTOCOLLO_ALER"), "")
                Me.txtDataArrivo.Text = par.FormattaData(par.IfNull(myReader1("DATA_ARRIVO"), ""))
                Me.txtDataArrivoAler.Text = par.FormattaData(par.IfNull(myReader1("DATA_ARRIVO_ALER"), ""))
                Me.cmbTipoInvio.SelectedValue = par.IfNull(myReader1("TIPO_INVIO"), "")
                Me.cmbAmministratori.SelectedValue = myReader1("ID_AMMINISTRATORE")
                Me.txtNote.Text = par.IfNull(myReader1("NOTE"), "")
                Me.txtDataRifDa.Text = par.FormattaData(par.IfNull(myReader1("RIF_DA"), ""))
                Me.txtDataRifA.Text = par.FormattaData(par.IfNull(myReader1("RIF_A"), ""))
                Me.txtDataRichRimborso.Text = par.FormattaData(par.IfNull(myReader1("DATA_RICHIESTA_RIMBORSO"), ""))
                Me.txtDataMandato.Text = par.FormattaData(par.IfNull(myReader1("DATA_MANDATO_COM"), ""))
                Me.txtNumeroMandato.Text = par.IfNull(myReader1("NUM_MANDATO_COM"), "")
                Me.txtImportoMandato.Text = Format(par.IfNull(myReader1("IMPORTO"), 0), "##,##0.00")

                If par.IfNull(myReader1("FL_COMPLETO"), 0) = 1 Then
                    Me.ChkCompleto.Checked = True
                    Me.chkChecked.Value = 1
                Else
                    Me.ChkCompleto.Checked = False
                    Me.chkChecked.Value = 0

                End If

                par.cmd.CommandText = "SELECT PRENOTAZIONI.IMPORTO_PRENOTATO,ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & par.IfNull(myReader1("ID_PRENOTAZIONE"), 0)

            End If
            myReader1.Close()
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                If noPatrimonio.Value > 0 Then
                    Me.txtTotale.Text = Format(par.IfNull(myReader1("IMPORTO_PRENOTATO"), 0), "##,##0.00")
                End If
                'If par.IfNull(myReader1("ID_PAGAMENTO"), 0) > 0 Then
                '    Me.btnSalvaCambioAmm.Visible = False
                '    Me.btnDelete.Visible = False
                '    Me.txtTotale.Enabled = False
                '    SettaFrmReadOnly()

                'End If
            End If
            myReader1.Close()

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Private Sub CercaMorosita()
        Try

            If vIdMorosita <> "" Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                par.cmd.CommandText = "SELECT UNITA_IMMOBILIARI.ID AS ID_UI,UNITA_IMMOBILIARI.INTERNO, RAPPORTI_UTENZA.DATA_DECORRENZA, SCALE_EDIFICI.DESCRIZIONE AS SCALA,COND_UI.POSIZIONE_BILANCIO,PIANI.DESCRIZIONE AS PIANO,COND_MOROSITA_INQUILINI.ID_MOROSITA, " _
                                    & "('<a href=""javascript:window.open(''RiepDettMorosita.aspx?IDMOR='||COND_MOROSITA_INQUILINI.ID_MOROSITA||'&IDUI='||COND_MOROSITA_INQUILINI.ID_UI||'&IDINQ='||COND_MOROSITA_INQUILINI.id_intestatario||'&IDCON=" & vIdConnModale & "'',''RiepMorInquilini'',''height=350px,top=0,left=0,width=500px,toolbar=no'');void(0);"">'||trim(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99'))||'</a>') AS IMPORTO, " _
                                    & " trim(TO_CHAR(IMPORTO,'9G999G999G999G999G990D99')) as EURO, " _
                                    & "(CASE WHEN UNITA_CONTRATTUALE.ID_CONTRATTO IS NULL THEN 'LIBERO'  ELSE  SISCOM_MI.Getstatocontratto2(UNITA_CONTRATTUALE.ID_CONTRATTO,0) END) AS STATO," _
                                    & "(CASE WHEN unita_contrattuale.id_contratto IS NULL THEN '' ELSE siscom_mi.Getstatocontratto2 (unita_contrattuale.id_contratto, 0 ) END) AS stato_dt_select, siscom_mi.GetIntestatari(UNITA_CONTRATTUALE.ID_CONTRATTO,0) AS INTESTATARIO, " _
                                    & "('<a href=""javascript:window.open(''DetMorMAVInq.aspx?IDMOR='||COND_MOROSITA_INQUILINI.ID_MOROSITA||'&IDINQ='||COND_MOROSITA_INQUILINI.id_intestatario||'&IDCON=" & vIdConnModale & "'',''DetMorInq'',''height=400px,top=0,left=0,width=550px'');void(0);"">'||(CASE WHEN ANAGRAFICA.ragione_sociale IS NOT NULL THEN ragione_sociale ELSE RTRIM (LTRIM (cognome || ' ' || nome))END)||'</a>') AS nominativo, " _
                                    & "COND_MOROSITA_INQUILINI.ID_INTESTATARIO as ID_INTESTARIO,unita_contrattuale.id_contratto, rapporti_utenza.COD_CONTRATTO,trim(TO_CHAR(VARIAZIONE_COMUNE,'9G999G999G999G999G990D99')) as VARIAZIONE " _
                                    & "FROM SISCOM_MI.TIPOLOGIA_UNITA_IMMOBILIARI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.PIANI,SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.EDIFICI, SISCOM_MI.COND_UI, SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.ANAGRAFICA,SISCOM_MI.COND_MOROSITA_INQUILINI,SISCOM_MI.RAPPORTI_UTENZA " _
                                    & "WHERE UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID(+) AND UNITA_IMMOBILIARI.ID_PIANO = PIANI.ID(+) AND UNITA_IMMOBILIARI.COD_TIPOLOGIA = TIPOLOGIA_UNITA_IMMOBILIARI.COD AND UNITA_IMMOBILIARI.ID_EDIFICIO = EDIFICI.ID AND UNITA_CONTRATTUALE.ID_CONTRATTO = RAPPORTI_UTENZA.ID AND " _
                                    & "UNITA_IMMOBILIARI.ID = UNITA_CONTRATTUALE.ID_UNITA(+) AND COND_UI.ID_UI = UNITA_IMMOBILIARI.ID AND COD_TIPO_DISPONIBILITA <> 'VEND' AND  COND_MOROSITA_INQUILINI.ID_INTESTATARIO=ANAGRAFICA.ID(+) AND (cond_ui.id_condominio=" & Request.QueryString("IDCONDOMINIO") & ") AND ID_INTESTATARIO  IS NOT NULL AND EDIFICI.ID IN (SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & Request.QueryString("IDCONDOMINIO") & ") AND COND_UI.ID_UI=COND_MOROSITA_INQUILINI.ID_UI(+) AND COND_MOROSITA_INQUILINI.ID_MOROSITA=" & vIdMorosita & " ORDER BY POSIZIONE_BILANCIO ASC, NOMINATIVO ASC ,  ID_UI ASC,DATA_DECORRENZA DESC "

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()

                da.Fill(dt)
                DataGridMorosita.DataSource = FiltraContrattiVeri(dt)

                DataGridMorosita.DataBind()
                If ReadOnlyxMoro.Value = 1 Then
                    FrmReadOnlyMorStampata()
                End If

            End If
            AddJavascriptFunction()
            SommaColonne()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub
    Private Function FiltraContrattiVeri(ByVal Table As Data.DataTable) As Data.DataTable
        FiltraContrattiVeri = Table.Clone()
        Dim idUi As Integer = 0
        Dim idInte As Integer = 0
        Try
            Dim rSelect As Data.DataRow()

            For i As Integer = 0 To Table.Rows.Count - 1
                If par.IfNull(Table.Rows(i).Item("COD_CONTRATTO"), "") <> "" Then
                    If Table.Rows(i).Item("ID_UI") <> idUi Or Table.Rows(i).Item("id_intestario") <> idInte Then
                        rSelect = Table.Select("ID_INTESTARIO = " & Table.Rows(i).Item("id_intestario") & " AND ID_UI = " & Table.Rows(i).Item("ID_UI") & " AND STATO_DT_SELECT LIKE '%IN CORSO%'")
                        If rSelect.Length > 0 Then
                            FiltraContrattiVeri.Rows.Add(rSelect(0).ItemArray)
                            idUi = rSelect(0).Item("ID_UI")
                            idInte = rSelect(0).Item("ID_INTESTARIO")
                        Else
                            FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                            idUi = Table.Rows(i).Item("ID_UI")
                            idInte = Table.Rows(i).Item("ID_INTESTARIO")

                        End If
                    End If
                Else
                    FiltraContrattiVeri.Rows.Add(Table.Rows(i).ItemArray)
                End If
            Next

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
        Return FiltraContrattiVeri
    End Function


    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        Update()
    End Sub
    Private Function PrenotaPagamento() As Boolean
        PrenotaPagamento = False

        Try
            Dim totPrenotazione As Decimal = 0
            Dim idVoce As String = ""
            Dim idStruttura As String = ""
            Dim idFornitore As String = ""
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            '*********PEPPE CREATED 27/04/2011
            If noPatrimonio.Value = 0 Then
                par.cmd.CommandText = "select * from siscom_mi.cond_morosita_inquilini where id_morosita = " & vIdMorosita & " and (importo is null or importo = 0)"
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    Response.Write("<script>alert('Il flag Completa e Stamapabile è attivabile solo se\ntutti gli importi degli inquilini sono maggiori di zero!');</script>")
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA SET FL_COMPLETO = 0 " _
                    & "WHERE ID = " & vIdMorosita & " AND ID_CONDOMINIO=" & Request.QueryString("IDCONDOMINIO")
                    par.cmd.ExecuteNonQuery()
                    ChkCompleto.Checked = False
                    Exit Function
                End If
                lettore.Close()


                'Seleziono l'importo da prenotare che è stato appena salvato
                par.cmd.CommandText = "SELECT SUM(IMPORTO) as TOT_MOROSITA FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_MOROSITA = " & vIdMorosita
                lettore = par.cmd.ExecuteReader
                If lettore.Read Then
                    totPrenotazione = par.IfNull(lettore("TOT_MOROSITA"), 0)
                End If
                lettore.Close()
            Else
                totPrenotazione = par.IfEmpty(Me.txtTotale.Text, "0").Replace(".", "")
            End If
            Dim annoEsFinApprovato As String = ""
            '########### ricavo la data fine dell'ultimo esercizio approvato per il quale sono conosciute le voci_pf ############
            par.cmd.CommandText = "SELECT substr(fine,1,4)as fine FROM siscom_mi.T_ESERCIZIO_FINANZIARIO where id = (select max(id_esercizio_finanziario) from siscom_mi.pf_main where id_stato = 5)"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                annoEsFinApprovato = par.IfNull(lettore("FINE"), "")
            End If
            lettore.Close()
            If String.IsNullOrEmpty(annoEsFinApprovato) Then
                annoEsFinApprovato = Format(Now, "yyyymmdd").Substring(0, 4)
            End If

            '
            'seleziono id_voce e Id_struttura dal piano finanziario corrente
            par.cmd.CommandText = "SELECT id_struttura,id_voce FROM siscom_mi.pf_voci_struttura WHERE id_voce =" _
                                & "(SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO = " _
                                & "(SELECT PF_MAIN.ID FROM SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                                & "WHERE ID_STATO = 5 AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID " _
                                & "AND SUBSTR(INIZIO,0,4) = '" & annoEsFinApprovato & "') AND CODICE = '2.02.07.01')" _
                                & "AND (valore_lordo + nvl(ASSESTAMENTO_VALORE_LORDO,0) + nvl(VARIAZIONI,0)) > 0"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idVoce = par.IfNull(lettore("ID_VOCE"), 0)
                'idStruttura = par.IfNull(lettore("ID_STRUTTURA"), 0)
            End If
            lettore.Close()

            par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idStruttura = par.IfNull(lettore("ID_UFFICIO"), "-1")
            End If
            lettore.Close()


            'seleziono il fornitore del condominio
            par.cmd.CommandText = "SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID =" & Request.QueryString("IDCONDOMINIO")
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                idFornitore = par.IfNull(lettore("ID_FORNITORE"), "")
            End If
            lettore.Close()
            Dim IdPrenotazione As String = 0
            '************09/10/2011 controllo se prenotare nuovamente il pagamento
            Dim Riprenota As Boolean = False
            par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita & ")"
            lettore = par.cmd.ExecuteReader
            If lettore.Read Then
                If par.IfNull(lettore(0), 0) <> totPrenotazione And par.IfEmpty(Session.Item("MOD_CONDOMINIO_MOR"), 0) = 0 Then
                    Riprenota = True
                ElseIf par.IfNull(lettore(0), 0) <> totPrenotazione And par.IfEmpty(Session.Item("MOD_CONDOMINIO_MOR"), 0) = 1 Then
                    par.cmd.CommandText = "update siscom_mi.cond_morosita set fl_completo = 0 where id = " & vIdMorosita
                    par.cmd.ExecuteNonQuery()

                End If
            Else
                Riprenota = True
            End If
            lettore.Close()


            If Riprenota = True Then
                Dim GiaPrenotato As Boolean = False
                par.cmd.CommandText = "SELECT  ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita
                lettore = par.cmd.ExecuteReader

                If lettore.Read Then
                    IdPrenotazione = par.IfNull(lettore("ID_PRENOTAZIONE"), 0)
                Else
                    IdPrenotazione = 0
                End If
                lettore.Close()

                If IdPrenotazione = 0 Then

                    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL"
                    lettore = par.cmd.ExecuteReader
                    If lettore.Read Then
                        IdPrenotazione = par.IfNull(lettore(0), 0)
                    End If
                    lettore.Close()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI (ID,ID_FORNITORE,ID_VOCE_PF,ID_STATO,TIPO_PAGAMENTO,DESCRIZIONE,DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_STRUTTURA)" _
                    & "VALUES (" & IdPrenotazione & "," & idFornitore & "," & idVoce & ",0,2,'PRENOTAZIONE LIQUIDAZIONE PAGAMENTO MOROSITA CONDOMINI'," _
                    & "" & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & "," & par.VirgoleInPunti(totPrenotazione) & "," & idStruttura & ")"

                Else
                    'par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET DATA_PRENOTAZIONE = '" & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & "', " _
                    '                    & "IMPORTO_PRENOTATO = " & par.VirgoleInPunti(totPrenotazione) & " WHERE ID = " & IdPrenotazione

                    par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET " _
                                        & "IMPORTO_PRENOTATO = " & par.VirgoleInPunti(totPrenotazione) & " WHERE ID = " & IdPrenotazione


                End If


                If String.IsNullOrEmpty(idVoce) Or String.IsNullOrEmpty(idStruttura) Or String.IsNullOrEmpty(idFornitore) Then
                    Response.Write("<script>alert('Impossibile completare la prenotazione!\nVerrà annullata l\'opzione selezionata di morosita completa e stampabile,\ne la relativa prenotazione degli importi');</script>")
                    PrenotaPagamento = False
                Else


                    'eseguo comando se controllo campi andato a buon fine
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA SET ID_PRENOTAZIONE = " & IdPrenotazione & " WHERE ID = " & vIdMorosita
                    par.cmd.ExecuteNonQuery()
                    Response.Write("<script>alert('Prenotazione dell\'importo di morosità avvenuto correttamente!');</script>")
                    PrenotaPagamento = True
                End If
            Else
                PrenotaPagamento = True

            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Function

    Private Sub Update()
        Try

            If Me.cmbAmministratori.SelectedItem.Text <> "" And Not String.IsNullOrEmpty(Me.txtDataRifDa.Text) AndAlso Not String.IsNullOrEmpty(Me.txtDataRifA.Text) Then
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                If par.AggiustaData(Me.txtDataRifDa.Text) > par.AggiustaData(Me.txtDataRifA.Text) Then
                    Response.Write("<script>alert('Periodo di riferimento non corretto!');</script>")
                    Exit Sub
                End If
                Dim completa As Integer = 0
                If Me.noPatrimonio.Value = 0 Then
                    If Me.ChkCompleto.Checked = True Then
                        completa = 1
                        SommaColonne()
                    End If

                    If completa = 1 And par.IfEmpty(Me.txtTotale.Text, 0) <= 0 Then
                        ChkCompleto.Checked = False
                        Response.Write("<script>alert('Attenzione!Avvalorare gli importi della morosità per procedere!');</script>")
                        Exit Sub
                    End If
                Else
                    If Me.ChkCompleto.Checked = True Then
                        completa = 1
                    End If
                End If

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA SET DATA_RICHIESTA = '" & par.IfEmpty(par.AggiustaData(Me.txtDataRichiesta.Text), "") & "', PROTOCOLLO_ALER = '" & par.PulisciStrSql(Me.txtProtocollo.Text) & "',DATA_ARRIVO_ALER ='" & par.IfEmpty(par.AggiustaData(Me.txtDataArrivoAler.Text), "") & "'," _
                & " ID_AMMINISTRATORE=" & Me.cmbAmministratori.SelectedValue & ",DATA_ARRIVO='" & par.IfEmpty(par.AggiustaData(Me.txtDataArrivo.Text), "") & "',TIPO_INVIO='" & Me.cmbTipoInvio.SelectedValue.ToString & "',NOTE='" & par.PulisciStrSql(Me.txtNote.Text) & "',RIF_DA='" & par.IfEmpty(par.AggiustaData(Me.txtDataRifDa.Text), "") & "',RIF_A= '" & par.IfEmpty(par.AggiustaData(Me.txtDataRifA.Text), "") & "'" _
                & ", FL_COMPLETO = " & completa & ",DATA_RICHIESTA_RIMBORSO='" & par.IfEmpty(par.AggiustaData(Me.txtDataRichRimborso.Text), "") & "', DATA_MANDATO_COM='" & par.IfEmpty(par.AggiustaData(Me.txtDataMandato.Text), "") & "', NUM_MANDATO_COM='" & par.IfEmpty(Me.txtNumeroMandato.Text, "") & "', IMPORTO=" & par.IfEmpty(par.VirgoleInPunti(Me.txtImportoMandato.Text.Replace(".", "")), 0) & " WHERE ID = " & vIdMorosita & " AND ID_CONDOMINIO=" & Request.QueryString("IDCONDOMINIO")
                par.cmd.ExecuteNonQuery()


                Dim i As Integer = 0
                Dim di As DataGridItem
                Dim IdIntestatario As String = ""
                Dim IdUi As String = ""
                Dim Importo As String = ""
                Dim ImportoVecchio As String = ""
                Dim Variazione As String = ""
                Dim VariazVecchia As String = ""
                Dim Diverso As Boolean = False
                Dim DiversVariaz As Boolean = False
                Dim descEvent As String = ""
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_MOROSITA = " & vIdMorosita
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable
                da.Fill(dt)

                For i = 0 To Me.DataGridMorosita.Items.Count - 1
                    di = Me.DataGridMorosita.Items(i)
                    IdIntestatario = Me.DataGridMorosita.Items(i).Cells(1).Text
                    IdUi = Me.DataGridMorosita.Items(i).Cells(0).Text
                    Importo = par.RimuoviHTML(Me.DataGridMorosita.Items(i).Cells(11).Text)
                    Variazione = par.IfEmpty(DirectCast(di.Cells(1).FindControl("txtVariazione"), TextBox).Text, "0")
                    For Each row As Data.DataRow In dt.Rows
                        If IdIntestatario = row.Item("ID_INTESTATARIO") AndAlso IdUi = row.Item("ID_UI") AndAlso Importo <> par.IfNull(row.Item("IMPORTO"), "") Then
                            Diverso = True
                            ImportoVecchio = par.IfNull(row.Item("IMPORTO"), "")
                            'Exit For
                        End If
                        If IdIntestatario = row.Item("ID_INTESTATARIO") AndAlso IdUi = row.Item("ID_UI") AndAlso Variazione <> par.IfNull(row.Item("VARIAZIONE_COMUNE"), "") Then
                            DiversVariaz = True
                            VariazVecchia = par.IfNull(row.Item("VARIAZIONE_COMUNE"), "0")

                        End If
                    Next
                    If Not String.IsNullOrEmpty(Importo) Then
                        If Diverso = True Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_INQUILINI  SET IMPORTO =  " & par.IfEmpty(par.VirgoleInPunti(Importo.Replace(".", "")), "Null") & " " _
                            & " WHERE ID_MOROSITA=" & vIdMorosita & " AND ID_INTESTATARIO=" & IdIntestatario & " AND ID_UI= " & IdUi
                            par.cmd.ExecuteNonQuery()
                            '****************MYEVENT*****************
                            descEvent = "MOROSITA DAL " & par.IfEmpty(Me.txtDataRifDa.Text, "--") & " AL " & par.IfEmpty(Me.txtDataRifA.Text, "--") & ""
                            descEvent = descEvent & " INQUILINO  " & di.Cells(3).Text
                            descEvent = descEvent & " MODIFICATO IMPORTO DA " & par.IfEmpty(ImportoVecchio, "0") & " A " & Importo
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','" & par.PulisciStrSql(descEvent) & "')"
                            par.cmd.ExecuteNonQuery()
                            '****************FINE MYEVENT*****************
                        End If
                    Else
                        Response.Write("<script>alert('Importo obbligatorio per tutti gli inquilini in elenco!');</script>")
                        Exit Sub
                    End If

                    If Not String.IsNullOrEmpty(Variazione) And DiversVariaz = True Then
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA_INQUILINI  SET VARIAZIONE_COMUNE =  " & par.IfEmpty(par.VirgoleInPunti(Variazione.Replace(".", "")), "Null") & " " _
                                            & " WHERE ID_MOROSITA=" & vIdMorosita & " AND ID_INTESTATARIO=" & IdIntestatario & " AND ID_UI= " & IdUi
                        par.cmd.ExecuteNonQuery()
                        If VariazVecchia = "" Or VariazVecchia = "0" Then
                            'evento inserimento variazione da parte di operatore comunale
                            '****************MYEVENT*****************
                            descEvent = "MOROSITA DAL " & par.IfEmpty(Me.txtDataRifDa.Text, "--") & " AL " & par.IfEmpty(Me.txtDataRifA.Text, "--") & ""
                            descEvent = descEvent & " INQUILINO " & par.RimuoviHTML(di.Cells(3).Text.Replace("'", "\'"))
                            descEvent = descEvent & " INSERITA VARIAZIONE " & par.IfEmpty(Variazione, "Null")
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','" & par.PulisciStrSql(descEvent) & "')"
                            par.cmd.ExecuteNonQuery()
                            '****************FINE MYEVENT*****************
                        Else
                            'evento di modifica della vecchia variazione
                            '****************MYEVENT*****************
                            descEvent = "MOROSITA DAL " & par.IfEmpty(Me.txtDataRifDa.Text, "--") & " AL " & par.IfEmpty(Me.txtDataRifA.Text, "--") & ""
                            descEvent = descEvent & " INQUILINO " & par.RimuoviHTML(di.Cells(3).Text.Replace("'", "\'"))
                            descEvent = descEvent & " MODIFICATA VARIAZIONE DA " & par.IfEmpty(VariazVecchia, "0") & " A " & par.IfEmpty(Variazione, "Null")
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','" & par.PulisciStrSql(descEvent) & "')"
                            par.cmd.ExecuteNonQuery()
                            '****************FINE MYEVENT*****************
                        End If

                    End If

                    Diverso = False
                    DiversVariaz = False
                Next

                CercaMorosita()
                FrmReadOnlyMorStampata()
                Session("MODIFYMODAL") = 1
                If completa = 1 Then

                    If PrenotaPagamento() = True Then

                        Response.Write("<script>alert('Operazone eseguita correttamente!');</script>")
                    Else
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA SET FL_COMPLETO = 0 " _
                                            & "WHERE ID = " & vIdMorosita & " AND ID_CONDOMINIO=" & Request.QueryString("IDCONDOMINIO")
                        par.cmd.ExecuteNonQuery()
                        Me.ChkCompleto.Checked = False
                        Response.Write("<script>alert('Salvataggio Morosità Inquilini eseguito correttamente!');</script>")
                        Response.Write("<script>alert('Impossibile completare la prenotazione!\nVerrà annullata l\'opzione selezionata di morosita completa e stampabile,\ne la relativa prenotazione degli importi');</script>")

                    End If

                Else

                    par.cmd.CommandText = "SELECT  ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim idPrenotazione As Integer = 0
                    If lettore.Read Then
                        idPrenotazione = par.IfNull(lettore("ID_PRENOTAZIONE"), 0)
                    End If
                    lettore.Close()

                    If idPrenotazione > 0 Then
                        'se esisteva una prenotazione la rendo non visibile
                        par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET " _
                                            & "id_stato = -3 WHERE ID = " & idPrenotazione
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = "UPDATE SISCOM_MI.COND_MOROSITA SET id_prenotazione = null " _
                                            & "WHERE ID = " & vIdMorosita & " AND ID_CONDOMINIO=" & Request.QueryString("IDCONDOMINIO")
                        par.cmd.ExecuteNonQuery()

                    End If


                    Response.Write("<script>alert('Operazone eseguita correttamente!');</script>")

                End If
                'Response.Write("<script>window.close();</script>")
                Me.txtSalvato.Value = 1
                Me.txtModificato.Value = 0

            Else
                Response.Write("<script>alert('Riempire tutti i campi obbligatori!');</script>")
                Exit Sub
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Sub
    Private Sub AddJavascriptFunction()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridMorosita.Items.Count - 1
                di = Me.DataGridMorosita.Items(i)
                DirectCast(di.Cells(1).FindControl("txtVariazione"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                DirectCast(di.Cells(1).FindControl("txtVariazione"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")

            Next
            Me.txtTotale.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try

    End Sub


    Private Sub SettaFrmReadOnly()
        Try

            Dim i As Integer = 0
            Dim di As DataGridItem
            For i = 0 To Me.DataGridMorosita.Items.Count - 1
                di = Me.DataGridMorosita.Items(i)
                DirectCast(di.Cells(1).FindControl("txtVariazione"), TextBox).Enabled = False
            Next

            Me.txtDataRichiesta.Enabled = False
            Me.txtProtocollo.Enabled = False
            Me.txtDataArrivo.Enabled = False
            Me.txtDataArrivoAler.Enabled = False
            Me.cmbTipoInvio.Enabled = False
            Me.cmbAmministratori.Enabled = False
            Me.txtNote.Enabled = False
            Me.txtDataRifDa.Enabled = False
            Me.txtDataRifA.Enabled = False
            Me.btnDelete.Visible = False
            Me.btnSalvaCambioAmm.Visible = False
            Me.ChkCompleto.Enabled = False
            Me.txtDataRichRimborso.Enabled = False
            Me.SoloLettura.Value = 1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Protected Sub DataGridMorosita_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridMorosita.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & par.RimuoviHTML(e.Item.Cells(3).Text.Replace("'", "\'")) & "';document.getElementById('txtidIntestatario').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtinquilino').value='" & par.RimuoviHTML(e.Item.Cells(3).Text.Replace("'", "\'")) & "';document.getElementById('txtimporto').value='" & par.RimuoviHTML(e.Item.Cells(7).Text & "'") & ";document.getElementById('txtIdUi').value='" & (e.Item.Cells(0).Text) & "';")

        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='yellow'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='Gainsboro'")
            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('txtmia').value='Hai selezionato " & par.RimuoviHTML(e.Item.Cells(3).Text.Replace("'", "\'")) & "';document.getElementById('txtidIntestatario').value='" & e.Item.Cells(1).Text & "';document.getElementById('txtinquilino').value='" & par.RimuoviHTML(e.Item.Cells(3).Text.Replace("'", "\'")) & "';document.getElementById('txtimporto').value='" & par.RimuoviHTML(e.Item.Cells(7).Text & "'") & ";document.getElementById('txtIdUi').value='" & (e.Item.Cells(0).Text) & "';")
        End If
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
        Try
            If Me.txtidIntestatario.Value <> 0 Then
                If txtConfElimina.Value = 1 Then

                    Dim descEvent As String = ""


                    Dim continua As Boolean = True
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    '**********COMMENTARE BLOCCO SE DA PUBBLICARE IN PRODUZIONE**********COMMENTARE BLOCCO SE DA PUBBLICARE IN PRODUZIONE **********COMMENTARE BLOCCO SE DA PUBBLICARE IN PRODUZIONE ******************
                    par.cmd.CommandText = "select * from siscom_mi.bol_bollette where rif_bollettino = (select bollettino FROM SISCOM_MI.COND_MOROSITA_lettere WHERE ID_anagrafica = " & txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita & ")"
                    Dim MYREADER As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If MYREADER.HasRows = True Then
                        If MYREADER.Read Then
                            If par.IfNull(MYREADER("DATA_PAGAMENTO"), "") <> "" Then
                                Response.Write("<script>alert('Impossibile eliminare la morosità, perchè risulta già pagata!');</script>")
                                continua = False
                            Else
                                'annullo bolletta
                                par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE SET FL_ANNULLATA = '1', DATA_ANNULLO = '" & Format(Now, "yyyyMMdd") & "' WHERE ID = " & par.IfNull(MYREADER("ID"), "0") & " AND RIF_FILE = 'MOR' AND RIF_BOLLETTINO = '" & par.IfNull(MYREADER("RIF_BOLLETTINO"), "0") & "'"
                                par.cmd.ExecuteNonQuery()

                                par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_LETTERE WHERE ID_MOROSITA = " & vIdMorosita & " AND ID_ANAGRAFICA = " & txtidIntestatario.Value
                                par.cmd.ExecuteNonQuery()

                                'scrittura evento
                                ''****************MYEVENT*****************
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLATA MOROSITA INQUILINO')"
                                par.cmd.ExecuteNonQuery()
                                '****************MYEVENT*****************
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & MYREADER("ID_CONTRATTO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F07','ANNULLO BOLLETTA MOROSITA')"
                                par.cmd.ExecuteNonQuery()

                            End If
                        End If
                    End If
                    MYREADER.Close()
                    '**********COMMENTARE BLOCCO SE DA PUBBLICARE IN PRODUZIONE**********COMMENTARE BLOCCO SE DA PUBBLICARE IN PRODUZIONE **********COMMENTARE BLOCCO SE DA PUBBLICARE IN PRODUZIONE ******************


                    If continua = True Then

                        par.cmd.CommandText = "SELECT FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_INTESTATARIO = " & Me.txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_INTESTATARIO = " & Me.txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita & " AND ID_UI = " & Me.txtIdUi.Value
                        par.cmd.ExecuteNonQuery()

                        descEvent = "MOROSITA DAL " & par.IfEmpty(Me.txtDataRifDa.Text, "--") & " AL " & par.IfEmpty(Me.txtDataRifA.Text, "--") & ""
                        descEvent = descEvent & " CANCELLATA MOROSITA INQUILINO " & txtinquilino.Value
                        '****************MYEVENT*****************
                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Request.QueryString("IDCONDOMINIO") & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','" & par.PulisciStrSql(descEvent) & "')"
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI_DET WHERE ID_INTESTATARIO = " & Me.txtidIntestatario.Value & " AND ID_MOROSITA = " & vIdMorosita & " AND ID_UI = " & Me.txtIdUi.Value
                        par.cmd.ExecuteNonQuery()

                        Session("MODIFYMODAL") = 1
                        CercaMorosita()
                        txtidIntestatario.Value = 0
                        txtConfElimina.Value = 0
                        txtinquilino.Value = ""
                        txtIdUi.Value = ""
                        Me.txtimporto.Value = 0

                        AggiornaComboDataDoc()
                    End If

                Else
                    txtConfElimina.Value = 0

                End If

            Else
                Response.Write("<script>alert('Selezionare un elemento da eliminare!');</script>")

            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Try

            If txtModificato.Value <> "111" Then

                If Request.QueryString("IDVISUAL") = "0" And Not String.IsNullOrEmpty(Request.QueryString("IDVISUAL")) Then
                    Response.Write("<script>window.close();</script>")
                Else
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                    If Me.txtSalvato.Value = 0 Then
                        par.cmd.CommandText = "ROLLBACK TO SAVEPOINT MOROSITA"
                        par.cmd.ExecuteNonQuery()
                    End If

                    DeleteCampiVuoti()
                    Response.Write("<script>window.close();</script>")
                End If
            Else
                txtModificato.Value = 1
            End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub
    Private Sub DeleteCampiVuoti()

        '*******************RICHIAMO LA CONNESSIONE*********************
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        '*******************RICHIAMO LA TRANSAZIONE*********************
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans

        Dim i As Integer = 0
        Dim di As DataGridItem
        Dim IdIntestatario As String = ""
        Dim IdUi As String = ""
        Dim Importo As String = ""

        For i = 0 To Me.DataGridMorosita.Items.Count - 1
            di = Me.DataGridMorosita.Items(i)
            IdIntestatario = Me.DataGridMorosita.Items(i).Cells(1).Text
            IdUi = Me.DataGridMorosita.Items(i).Cells(0).Text
            Importo = par.RimuoviHTML(Me.DataGridMorosita.Items(i).Cells(11).Text)
            If String.IsNullOrEmpty(Importo) Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MOROSITA_INQUILINI" _
                & " WHERE ID_MOROSITA=" & vIdMorosita & " AND ID_INTESTATARIO=" & IdIntestatario & " AND ID_UI= " & IdUi
                par.cmd.ExecuteNonQuery()
            End If

        Next

    End Sub


    Protected Sub btnConguaglio_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConguaglio.Click
        SommaColonne()
    End Sub
    Private Sub SommaColonne()
        Try
            If noPatrimonio.Value = 0 Then

                Dim i As Integer = 0
                Dim di As DataGridItem

                Me.txtTotale.Text = 0

                For i = 0 To Me.DataGridMorosita.Items.Count - 1
                    di = Me.DataGridMorosita.Items(i)

                    Me.txtTotale.Text = Format(CDbl(txtTotale.Text) + CDbl(par.RimuoviHTML(par.IfEmpty(Me.DataGridMorosita.Items(i).Cells(11).Text, 0))), "##,##0.00")
                Next
            End If

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub

    Private Sub FrmReadOnlyMorStampata()
        Try
            ''*******************RICHIAMO LA CONNESSIONE*********************
            'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)
            ''*******************RICHIAMO LA TRANSAZIONE*********************
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnModale), Oracle.DataAccess.Client.OracleTransaction)
            '''par.cmd.Transaction = par.myTrans
            'Dim trovato As Boolean = False

            'par.cmd.CommandText = "SELECT COND_MOROSITA_INQUILINI.ID_INTESTATARIO FROM SISCOM_MI.BOL_BOLLETTE, SISCOM_MI.COND_MOROSITA_LETTERE,SISCOM_MI.COND_MOROSITA_INQUILINI WHERE ID_INTESTATARIO = ID_ANAGRAFICA AND COND_MOROSITA_LETTERE.ID_MOROSITA= COND_MOROSITA_INQUILINI.ID_MOROSITA AND COND_MOROSITA_LETTERE.BOLLETTINO= BOL_BOLLETTE.RIF_BOLLETTINO AND bol_bollette.fl_annullata = 0 AND COND_MOROSITA_INQUILINI.ID_MOROSITA =" & vIdMorosita
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'da.Fill(dt)
            'If dt.Rows.Count = 0 Then
            '    Dim i As Integer = 0
            '    Dim di As DataGridItem
            '    'For i = 0 To Me.DataGridMorosita.Items.Count - 1
            '    '    di = Me.DataGridMorosita.Items(i)
            '    '    DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Enabled = True
            '    'Next
            'Else
            '    Dim i As Integer = 0
            '    Dim di As DataGridItem
            '    For i = 0 To Me.DataGridMorosita.Items.Count - 1
            '        di = Me.DataGridMorosita.Items(i)
            '        trovato = False
            '        For Each row As Data.DataRow In dt.Rows
            '            If di.Cells(1).Text = row.Item("ID_INTESTATARIO") Then
            '                trovato = True
            '                Exit For
            '            End If
            '        Next
            '        'If trovato = True Then
            '        '    DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Enabled = False
            '        'Else
            '        '    DirectCast(di.Cells(1).FindControl("txtImporto"), TextBox).Enabled = True
            '        '    'Me.ChkCompleto.Enabled = True
            '        'End If

            '    Next
            '    dt.Dispose()
            '    da.Dispose()
            'End If

            Me.txtDataRichiesta.Enabled = False
            Me.txtProtocollo.Enabled = False
            Me.txtDataArrivo.Enabled = False
            Me.txtDataArrivoAler.Enabled = False
            Me.cmbTipoInvio.Enabled = False
            Me.cmbAmministratori.Enabled = False
            Me.txtDataRifDa.Enabled = False
            Me.txtDataRifA.Enabled = False
            Me.txtDataRichRimborso.Enabled = False
            If Session.Item("MOD_CONDOMINIO_MOR") = 1 Then
                Me.btnDelete.Visible = False
            End If
            Me.btnSalvaCambioAmm.Visible = True
            If Session.Item("MOD_CONDOMINIO_MOR") = 1 Then
                Me.ChkCompleto.Enabled = False
            Else
                Me.ChkCompleto.Enabled = True
            End If

            par.cmd.CommandText = "SELECT ID_PAGAMENTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID = (SELECT ID_PRENOTAZIONE FROM SISCOM_MI.COND_MOROSITA WHERE ID = " & vIdMorosita & ")"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader.Read Then
                If par.IfNull(myReader("id_pagamento"), 0) > 0 Then
                    Me.ChkCompleto.Enabled = False
                End If
            End If
            myReader.Close()

            'If trovato = False Then
            '    Me.ChkCompleto.Enabled = True
            'Else
            '    Me.ChkCompleto.Enabled = False
            'End If
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub


    Private Sub CampiMandatoLettura()
        Try
            Me.txtDataMandato.Enabled = False
            Me.txtNumeroMandato.Enabled = False
            Me.txtImportoMandato.Enabled = False
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("..\Errore.aspx")
        End Try
    End Sub


    Protected Sub btnConfermaDataDoc_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnConfermaDataDoc.Click
        'Cerca()
        Me.txtDataDoc.Text = ""
        CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        CercaMorosita()
        AggiornaComboDataDoc()

        'Variabile di sessione per sapere se sono state apportate modifiche e salvataggio alle finestre modali.
        'If Session("MODIFYMODAL") = 1 Then
        'End If
        'Session.Remove("MODIFYMODAL")
    End Sub


    Public Sub cmbDataDocum_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDataDocum.SelectedIndexChanged

    End Sub


    Protected Sub cercaDettaglio_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles cercaDettaglio.Click
        Me.txtDataDoc.Text = ""
        If Session("MODIFYMODAL") = 1 Then
            CType(Me.Page.FindControl("txtModificato"), HiddenField).Value = 1
        End If
        CercaMorosita()
        AggiornaComboDataDoc()
        If Me.SoloLettura.Value = 1 Then
            SettaFrmReadOnly()
        End If
    End Sub


End Class
