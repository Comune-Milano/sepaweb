
Partial Class Condomini_Condominio
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public classetab As String = ""
    Public tabvisibility As String = ""
    Public tabdefault1 As String = ""
    Public tabdefault2 As String = ""
    Public tabdefault3 As String = ""
    Public tabdefault4 As String = ""
    Public tabdefault5 As String = ""
    Public tabdefault6 As String = ""
    Public tabdefault7 As String = ""
    Public tabdefault8 As String = ""

    Dim StoUscendo As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session.Item("OPERATORE") = "" Then
                Response.Write("<script>top.location.href=""../AccessoNegato.htm""</script>")
            End If
            Dim Str As String
            Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:500; border:1px dashed #660000;"
            Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            Str = Str & "<" & "/div>"


            Response.Write(Str)
            Page.Validate("a")
            If par.IfEmpty(ConfEliminaEdifici.Value, 0) = 1 Then
                DeleteEdificiFromCondominio()
                Me.ConfEliminaEdifici.Value = 0
                Me.EdificiToDelete.Value = 0

            Else
                Me.ConfEliminaEdifici.Value = 0
                Me.EdificiToDelete.Value = 0
                AddSelectedEdifici()
            End If

            If Not IsPostBack Then
                Response.Flush()
                vSelezionati = 0
                ModificheCampi()
                'Me.ListEdifici.Attributes.Add("onClick", "javascript:chkToccato(event,this);")

                'Session.Add("CHIAMA", "COND")
                RiempiCampi()
                txtDataCost.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtDataFine.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                txtNuovaDataInizio.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                vIdCondominio = Request.QueryString("IdCond")
                vIdConnessione = Format(Now, "yyyyMMddHHmmss")

                If vIdCondominio <> "" Then
                    ApriRicerca()
                    Me.btnIndietro.Visible = True
                    Me.btnFornitore.Visible = True
                Else
                    'vIdFornitore = 0
                    btnFornitore.Visible = False
                    '*******************APERURA CONNESSIONE*********************
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)
                        HttpContext.Current.Session.Add("CONNCOND" & vIdConnessione, par.OracleConn)
                    End If
                End If
                'ModificheCampi()

            End If

            If vIdCondominio = "" Then
                classetab = "tabberhide"
                tabvisibility = "hidden"
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub
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
    Public Property vIdCondominio() As String
        Get
            If Not (ViewState("par_idCondominio") Is Nothing) Then
                Return CStr(ViewState("par_idCondominio"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idCondominio") = value
        End Set

    End Property
    Public Property vSelezionati() As Integer
        Get
            If Not (ViewState("par_Selezionati") Is Nothing) Then
                Return CInt(ViewState("par_Selezionati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_Selezionati") = value
        End Set

    End Property

    Private Sub RiempiCampi()
        Try

            Dim s As String = ""

            s = "SELECT ID, (COGNOME||' '||NOME) as AMMINISTRATORE FROM SISCOM_MI.COND_AMMINISTRATORI ORDER BY COGNOME ASC"
            Me.cmbAmministratori.Items.Add(New ListItem(" ", -1))
            Me.CmbAmministratori2.Items.Add(New ListItem(" ", -1))
            par.caricaComboBox(s, cmbAmministratori, "ID", "AMMINISTRATORE", False)
            par.caricaComboBox(s, CmbAmministratori2, "ID", "AMMINISTRATORE", False)

            s = "SELECT COD, NOME FROM SEPA.COMUNI_NAZIONI WHERE CAP IS NOT NULL AND (SIGLA = 'MI' OR SIGLA='MB')"
            Me.cmbComune.Items.Add(New ListItem(" ", -1))
            par.caricaComboBox(s, cmbComune, "COD", "NOME", False)

            cmbComune.SelectedValue = "F205"
            Me.txtProvincia.Text = "MI"


            CaricaFornitori()
            'CaricaComplessi()
            Me.CaricaEdifici()


            ' ''*******************APERURA CONNESSIONE*********************
            ''If par.OracleConn.State = Data.ConnectionState.Closed Then
            ''    par.OracleConn.Open()
            ''    par.SettaCommand(par)
            ''End If



            'par.cmd.CommandText = "SELECT ID, COGNOME, NOME FROM SISCOM_MI.COND_AMMINISTRATORI ORDER BY COGNOME ASC"
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'Me.cmbAmministratori.Items.Add(New ListItem(" ", -1))
            'Me.CmbAmministratori2.Items.Add(New ListItem(" ", -1))
            'While myReader1.Read
            '    'DdLComplesso.Items.Add(New ListItem(par.IfNull(myReader1("DENOMINAZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
            '    cmbAmministratori.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("id"), -1)))
            '    CmbAmministratori2.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()


            'par.cmd.CommandText = "SELECT COD, NOME FROM SEPA.COMUNI_NAZIONI WHERE CAP IS NOT NULL AND SIGLA = 'MI'"

            'myReader1 = par.cmd.ExecuteReader()
            'Me.cmbComune.Items.Add(New ListItem(" ", -1))
            'While myReader1.Read
            '    cmbComune.Items.Add(New ListItem(par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("COD"), "")))
            'End While
            'myReader1.Close()
            ''*********************CHIUSURA CONNESSIONE**********************
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()



        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Sub
    'Private Sub CaricaComplessi()
    '    '*******************APERURA CONNESSIONE*********************
    '    If par.OracleConn.State = Data.ConnectionState.Closed Then
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    Me.cmbEdifScelti.Items.Clear()

    '    par.cmd.CommandText = "SELECT complessi_immobiliari.id,COD_COMPLESSO,denominazione FROM SISCOM_MI.complessi_immobiliari, SISCOM_MI.indirizzi where  complessi_immobiliari.ID_INDIRIZZO_RIFERIMENTO=indirizzi.id AND complessi_immobiliari.ID<>1 order by denominazione asc"
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '    Me.cmbEdifScelti.Items.Add(New ListItem(" ", -1))
    '    While myReader1.Read
    '        cmbEdifScelti.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & " cod." & par.IfNull(myReader1("cod_complesso"), " "), par.IfNull(myReader1("id"), -1)))
    '    End While
    '    myReader1.Close()
    '    cmbEdifScelti.SelectedValue = "-1"
    '    '*********************CHIUSURA CONNESSIONE**********************
    '    par.OracleConn.Close()
    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    'End Sub
    Private Sub ApriRicerca()
        Dim scriptblock As String

        Try

            classetab = "tabbertab"
            tabvisibility = "visible"

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNCOND" & vIdConnessione, par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
            End If


            If Session.Item("LIVELLO") <> 1 Then
                If Session.Item("ID_CAF") = 6 Then
                    Session.Add("MOD_CONDOMINIO_MOR", 1)
                    par.OracleConn.Close()
                    ApriFrmSolaLettura()
                    Me.btnSalva.Visible = True
                    Exit Sub
                End If
                If Session.Item("MOD_CONDOMINIO_SL") = 1 Then
                    par.OracleConn.Close()
                    ApriFrmSolaLettura()
                    Exit Sub
                ElseIf Session.Item("MOD_CONDOMINIO_MOR") = 1 Then
                    par.OracleConn.Close()
                    ApriFrmSolaLettura()
                    Exit Sub
                End If
            End If


            '***************************************************************************
            '**********************BLOCCO DEL CONDOMINIO FOR UPDATE NOWAIT**************
            par.cmd.CommandText = "SELECT CONDOMINI.* FROM SISCOM_MI.CONDOMINI WHERE CONDOMINI.ID = " & vIdCondominio ''& " FOR UPDATE NOWAIT"
            Dim id_fornitore As String = ""
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtCodCondominio.Text = Format(CDbl(vIdCondominio), "00000")
                Me.txtDenCondominio.Text = myReader1("DENOMINAZIONE").ToString
                Me.txtDataCost.Text = par.FormattaData(myReader1("DATA_COSTITUZIONE").ToString)
                Me.cmbComune.SelectedValue = par.IfNull(myReader1("COD_COMUNE"), "-1")
                'Me.txtComune.Text = myReader1("COMUNE").ToString

                'vIdFornitore = par.IfNull(myReader1("ID_FORNITORE"), 0)

                Me.txtGestione.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_INIZIO").ToString), "/2000", "")
                Me.txtGestioneAl.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_FINE").ToString), "/2000", "")
                id_fornitore = par.IfNull(myReader1("id_fornitore"), "")
                idFornitore.Value = id_fornitore
                Me.txtNote.Text = myReader1("NOTE").ToString
                Me.cmbTipoGestione.SelectedValue = myReader1("TIPO_GESTIONE").ToString
                Me.TxtCodFiscale.Text = myReader1("COD_FISCALE").ToString
                Me.cmbFornitori.SelectedValue = par.IfNull(myReader1("id_fornitore").ToString, "-1")

                'Me.TxtIBAN.Text = myReader1("IBAN").ToString
                Me.cmbTipoCond.SelectedValue = myReader1("TIPOLOGIA").ToString
                DirectCast(Me.TabMillesimalil1.FindControl("TxtTotAlloggi"), TextBox).Text = par.IfNull(myReader1("TOT_ALLOGGI"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("txtTotBox"), TextBox).Text = par.IfNull(myReader1("TOT_BOX"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("TxtTotNegozi"), TextBox).Text = par.IfNull(myReader1("TOT_NEGOZI"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("txtTotDiversi"), TextBox).Text = par.IfNull(myReader1("TOT_DIVERSI"), "")

                par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

                Dim dt As New Data.DataTable()
                da.Fill(dt)
                For Each r As Data.DataRow In dt.Rows
                    Me.ListEdifici.Items.FindByValue(r.Item("ID_EDIFICIO")).Selected = True

                Next
                Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReadEdifici.Read
                    Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
                End While
                myReadEdifici.Close()
                AddSelectedEdifici()
            End If
            myReader1.Close()

            '++++nuovo iban in combo box
            'If Not String.IsNullOrEmpty(id_fornitore) Then
            '    par.cmd.CommandText = "select id_fornitore, iban from siscom_mi.FORNITORI_IBAN where id_fornitore = " & id_fornitore
            '    myReader1 = par.cmd.ExecuteReader
            '    While myReader1.Read
            '        cmbIban.Items.Add(New ListItem(par.IfNull(myReader1("IBAN"), " "), par.IfNull(myReader1("iban"), -1)))
            '    End While

            '    myReader1.Close()
            'Else
            '    cmbIban.Items.Add(New ListItem(" ", -1))
            'End If
            If Not String.IsNullOrEmpty(id_fornitore) Then
                par.caricaComboBox("select id_fornitore, iban from siscom_mi.FORNITORI_IBAN where id_fornitore = " & id_fornitore & " ORDER BY FL_ATTIVO DESC ", Me.cmbIban, "iban", "iban", False)
            Else
                Me.cmbIban.Items.Clear()
            End If

            '+++++SELEZIONE SIGLA DEL COMUNE
            par.cmd.CommandText = "SELECT  COMUNI_NAZIONI.SIGLA FROM SISCOM_MI.CONDOMINI, SEPA.COMUNI_NAZIONI WHERE  CONDOMINI.COD_COMUNE = COMUNI_NAZIONI.COD AND CONDOMINI.ID = " & vIdCondominio
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtProvincia.Text = myReader1("SIGLA").ToString
            End If

            'AMMINISTRATORE
            par.cmd.CommandText = "SELECT ID_AMMINISTRATORE FROM SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.cmbAmministratori.SelectedValue = myReader1(0)
                Me.cmbAmministratori.Enabled = False
            End If
            myReader1.Close()

            'DIV CAMBIO AMMINISTRATORE
            par.cmd.CommandText = "SELECT COGNOME, NOME, DATA_INIZIO FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtAmmCorrente.Text = myReader1("COGNOME") & " " & myReader1("NOME")
                Me.txtDataInizio.Text = par.FormattaData(myReader1("DATA_INIZIO").ToString)
            End If
            myReader1.Close()

            'If Me.DrLEdificio.SelectedValue = "-1" Then
            '    FiltraEdifici()
            'End If
            'vIdEdificio = DrLEdificio.SelectedValue

            If Not Session.Item("LAVORAZIONE") = 1 Then
                'Apro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)
            End If

            ImgVisibility.Value = 1
            '*************GESTIONE DELL'ECCEZZIONE IN CASO DI APERTURA DELLO STESSO CONDOMINIO DA DUE O PIù UTENTI***********
        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Condominio aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                ApriFrmWithDBLock()
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If
            Else
                par.OracleConn.Close()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../Errore.aspx';</script>")
            End If



        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub
    Private Sub CaricaFornitori()
        Dim ConnOpenNow As Boolean = False
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                ConnOpenNow = True
            End If

            Me.cmbTipoGestione.Items.Add(New ListItem(" ", ""))
            Me.cmbTipoGestione.Items.Add(New ListItem("DIRETTA", "D"))
            Me.cmbTipoGestione.Items.Add(New ListItem("INDIRETTA", "I"))
            par.cmd.CommandText = "SELECT ID,(cod_fornitore||' - '||(CASE WHEN fornitori.ragione_sociale IS NOT NULL THEN (CASE WHEN LENGTH(ragione_sociale)>80 THEN (SUBSTR(ragione_sociale,0,80)||'[...]')ELSE(ragione_sociale)END) ELSE (CASE WHEN LENGTH(cognome||' '||nome)>80 THEN (SUBSTR(cognome||' '||nome,0,80)||'[...]')ELSE(cognome||' '||nome)END) END))AS cod_fornitore FROM siscom_mi.fornitori WHERE cod_fornitore IS NOT NULL ORDER BY cod_fornitore ASC"
            par.caricaComboBox(par.cmd.CommandText, cmbFornitori, "id", "cod_fornitore", True)


            If par.OracleConn.State = Data.ConnectionState.Open And ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open And ConnOpenNow = True Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try

    End Sub
    Private Sub CaricaEdifici()
        Try

            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'Me.DrLEdificio.Items.Clear()

            'DrLEdificio.Items.Add(New ListItem(" ", -1))
            '31/08/2010 MODIFICA SULLA SELEZIONE DEGLI EDIFICI DA ASSOCIARE AL CONDOMINIO, VENGONO SELEZIONATI TUTTI NON SOLO QUELLI CON TIPOLOGIA CONDOMINIO! AND EDIFICI.CONDOMINIO = 1 
            par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE EDIFICI.ID_COMPLESSO = COMPLESSI_IMMOBILIARI.ID and COMPLESSI_IMMOBILIARI.ID <> 1 order by denominazione asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt As New Data.DataTable

            da.Fill(dt)
            For Each r As Data.DataRow In dt.Rows
                Me.ListEdifici.Items.Add(New ListItem(par.IfNull(r("denominazione"), " ") & "- -" & "cod." & par.IfNull(r("COD_EDIFICIO"), " "), par.IfNull(r("id"), -1)))
            Next
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
            '    'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
            '    Me.ListEdifici.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
            'End While
            'myReader1.Close()
            'DrLEdificio.SelectedValue = "-1"
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

        '**********CONTROLLO CAMPI OBBLIGATORI DEL CONDOMINIO
        'RIGA ORIGINALE 
        'If Not String.IsNullOrEmpty(Me.txtDenCondominio.Text) AndAlso Selezionati() = True AndAlso Not String.IsNullOrEmpty(Me.txtDataCost.Text) AndAlso Not String.IsNullOrEmpty(Me.cmbAmministratori.SelectedValue) Then
        'MODIFICA: DATA COSTITUZIONE CONDOMINIO NON OBBLIGATORIA
        'If Not String.IsNullOrEmpty(Me.txtDenCondominio.Text) AndAlso Selezionati() = True AndAlso Me.cmbAmministratori.SelectedValue <> "-1" Then
        If Page.IsValid Then
            If Not IsDate(Me.txtGestione.Text & "/2000") Or Not IsDate(Me.txtGestioneAl.Text & "/2000") Then
                Response.Write("<script>alert('Date periodo di gestione errate!');</script>")
                Exit Sub
            End If

            If vIdCondominio <> "" Then
                Update()
                txtModificato.Value = "0"
            Else
                Salva()
                txtModificato.Value = "0"
            End If
            TabMillesimalil1.CercaScale()
            'Else
            'Response.Write("<script>alert('Riempire tutti i dati obbligatori!');</script>")
            'End If
        Else
            Response.Write("<script>alert('Ci sono errori nella pagina!Verificare i dati!');</script>")
            Response.Flush()

        End If
    End Sub
    Private Sub Salva()
        Try


            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************APERURA TRANSAZIONE*********************
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)

            'If vIdFornitore = 0 Then

            '    Dim Cognome As String = ""
            '    Dim Nome As String = ""
            '    Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            '    '*************************CONGOME E NOME DELL'AMMINISTRATORE
            '    par.cmd.CommandText = "SELECT COGNOME, NOME from SISCOM_MI.COND_AMMINISTRATORI WHERE ID = " & Me.cmbAmministratori.SelectedValue
            '    lettore = par.cmd.ExecuteReader()
            '    If lettore.Read Then
            '        Cognome = par.IfNull(lettore("COGNOME"), "")
            '        Nome = par.IfNull(lettore("NOME"), "")
            '    End If
            '    lettore.Close()

            '    '*************************DI FORNITORE NUOVO
            '    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_FORNITORI.NEXTVAL FROM DUAL"
            '    lettore = par.cmd.ExecuteReader()
            '    If lettore.Read Then
            '        vIdFornitore = lettore(0)
            '    End If
            '    lettore.Close()

            '    '************************SALVO IN FORNITORI
            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI(ID,TIPO,RAGIONE_SOCIALE,COGNOME,NOME,COD_FISCALE,PARTITA_IVA,TIPO_R,IBAN,FL_BLOCCATO) VALUES " _
            '    & " (" & vIdFornitore & ",'G','CONDOMINIO " & par.PulisciStrSql(Me.txtDenCondominio.Text.ToUpper) & "','" & par.PulisciStrSql(Cognome) _
            '    & "','" & par.PulisciStrSql(Nome) & "','" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "','" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "','L','" & par.PulisciStrSql(Me.TxtIBAN.Text) & "',1)"
            '    par.cmd.ExecuteNonQuery()

            'End If

            ''*******************TROVO I MILLESSIMI DI PROPRIETA' DEL COMUNE E QUELLI ASCENSORE DI PROPRIETA' DEL COMUNE**********************
            'Dim MilPropComu As String
            'Dim IdScala As String = ""
            'Dim MillGestComuScale As String = ""
            ''MILLESIMI PROPRIETA' 
            'If DirectCast(Me.Page.FindControl("DrlEdificio"), DropDownList).SelectedValue <> "-1" Then
            '    par.cmd.CommandText = "SELECT SUM(VALORE_MILLESIMO) FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE VALORI_MILLESIMALI.ID_TABELLA = TABELLE_MILLESIMALI.ID AND TABELLE_MILLESIMALI.COD_TIPOLOGIA = 'PR' AND ID_EDIFICIO = " & DirectCast(Me.Page.FindControl("DrlEdificio"), DropDownList).SelectedValue
            'Else
            '    par.cmd.CommandText = "SELECT SUM(VALORE_MILLESIMO) FROM SISCOM_MI.VALORI_MILLESIMALI, SISCOM_MI.TABELLE_MILLESIMALI WHERE VALORI_MILLESIMALI.ID_TABELLA = TABELLE_MILLESIMALI.ID AND TABELLE_MILLESIMALI.COD_TIPOLOGIA = 'PR' AND ID_COMPLESSO = " & DirectCast(Me.Page.FindControl("cmbComplesso"), DropDownList).SelectedValue
            'End If

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            '    MilPropComu = myReader1(0).ToString
            'End If
            'myReader1.Close()
            'If Not IsDate(Me.txtGestione.Text & "/" & "2009") Or Not IsDate(Me.txtGestioneAl.Text & "/" & "2009") Then
            '    Response.Write("<script>alert('Date periodo di gestione errate!');</script>")
            '    Exit Sub
            'End If
            '*******SELEZIONE DELL'ID DEL CONDOMINIO
            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_CONDOMINI.NEXTVAL FROM DUAL"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdCondominio = myReader1(0)
            End If
            myReader1.Close()
            '*********INSERIMENTO NUOVO CONDOMINIO IN BASE A COMPLESSO O EDIFICIO

            'If Selezionati() = True Then
            Dim TipoRisc As String = ""
            '***CONTROLLARE***
            'par.cmd.CommandText = "SELECT COD_TIPOLOGIA_IMP_RISCALD FROM SISCOM_MI.EDIFICI WHERE ID = " & (Me.DrLEdificio.SelectedValue.ToString)
            'myReader1 = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            '    TipoRisc = par.IfNull(myReader1(0), "")
            'End If
            'myReader1.Close()
            Dim InizioGestione As String = Replace(par.AggiustaData(txtGestione.Text & "/2000"), "2000", "")
            Dim FineGestione As String = Replace(par.AggiustaData(txtGestioneAl.Text & "/2000"), "2000", "")

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.CONDOMINI(ID,DENOMINAZIONE,DATA_COSTITUZIONE,COD_COMUNE,TIPO_GESTIONE,GESTIONE_INIZIO,GESTIONE_FINE, NOTE, COD_TIPOLOGIA_IMP_RISCALD,id_fornitore,COD_FISCALE,TIPOLOGIA) " _
            & " VALUES (" & vIdCondominio & ", '" & par.PulisciStrSql(Me.txtDenCondominio.Text.ToUpper) & "', '" & par.AggiustaData(Me.txtDataCost.Text) & "'," _
            & " '" & Me.cmbComune.SelectedValue.ToString & "', '" & Me.cmbTipoGestione.SelectedValue.ToString & "', " _
            & "'" & par.PulisciStrSql(InizioGestione.ToUpper) & "','" & FineGestione & "', '" & par.PulisciStrSql(Me.txtNote.Text) & "',  '', " & RitornaNullSeMenoUno(Me.cmbFornitori.SelectedValue) & ", '" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "','" & Me.cmbTipoCond.SelectedValue & "')"
            par.cmd.ExecuteNonQuery()
            Me.txtCodCondominio.Text = Format(CDbl(vIdCondominio), "00000")
            Dim I As Integer
            For I = 0 To Me.ListEdifici.Items.Count() - 1
                If Me.ListEdifici.Items(I).Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_EDIFICI (ID_CONDOMINIO,ID_EDIFICIO) VALUES ( " & vIdCondominio & ", " & Me.ListEdifici.Items(I).Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            'GestioneCondEdificiMillScale()

            'AggiornaScale() 'Inserisce in cond_mil_scale le scale del condominio

            ''***********INSERIMENTO DELLE SCALE E DEI VALORI MILLESIMALI SULLE STESSE DI COMPETENZA DEL COMUNE
            'par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID AS id_scala,(SELECT SUM(VALORE_MILLESIMO) AS MILL_SCALA FROM SISCOM_MI.VALORI_MILLESIMALI WHERE ID_TABELLA = TABELLE_MILLESIMALI_SCALE.ID_TABELLA) AS GEST_COMUNE FROM SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TABELLE_MILLESIMALI_SCALE WHERE ID_EDIFICIO = " & DirectCast(Me.Page.FindControl("DrLEdificio"), DropDownList).SelectedValue & " AND TABELLE_MILLESIMALI_SCALE.ID_SCALA(+) = SCALE_EDIFICI.ID"
            'myReader1 = par.cmd.ExecuteReader()
            'While myReader1.Read
            '    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MIL_SCALE (ID_SCALA, MIL_TOT_COM) VALUES (" & myReader1("ID_SCALA") & "," & par.VirgoleInPunti(par.IfNull(myReader1("GEST_COMUNE"), "Null")) & ")"
            '    par.cmd.ExecuteNonQuery()
            'End While
            'myReader1.Close()
            'ListSuperCond


            For I = 0 To DirectCast(Me.TabDatiTecnici1.FindControl("ListSuperCond"), CheckBoxList).Items.Count() - 1
                If DirectCast(Me.TabDatiTecnici1.FindControl("ListSuperCond"), CheckBoxList).Items(I).Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_SUPER (ID_CONDOMINIO,ID_SUPERCONDOMINIO) VALUES ( " & vIdCondominio & ", " & DirectCast(Me.TabDatiTecnici1.FindControl("ListSuperCond"), CheckBoxList).Items(I).Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            'Else
            'Response.Write("<script>alert('Selezionare almeno un edifcio!');</script>")
            'End If


            '********INSERIMENTO AMMINISTRATORE DI CONDOMINIO NELLA TABELLA DI RELAZIONE
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AMMINISTRAZIONE (ID_CONDOMINIO,ID_AMMINISTRATORE,DATA_INIZIO,DATA_FINE) VALUES ( " & vIdCondominio & "," & Me.cmbAmministratori.SelectedValue & ", '" & par.AggiustaData(Me.txtDataCost.Text) & "', '')"
            par.cmd.ExecuteNonQuery()


            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F32','')"
            par.cmd.ExecuteNonQuery()
            '*******************************FINE****************************************
            '***************************************************************************



            Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
            'COMMIT GENERALE
            par.myTrans.Commit()




            Me.btnFornitore.Visible = True
            If Not Session.Item("LAVORAZIONE") = 1 Then
                'Apro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)
            End If

            '***************************************************************************
            '**********************BLOCCO DEL CONDOMINIO FOR UPDATE NOWAIT**************
            par.cmd.CommandText = "SELECT CONDOMINI.* FROM SISCOM_MI.CONDOMINI WHERE CONDOMINI.ID = " & vIdCondominio ''& " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader
            myReader1.Close()

            AggiornaDati()


            ImgVisibility.Value = 1


        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.myTrans.Rollback()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        End Try
    End Sub
    Private Sub Update()

        Try

            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            'If Selezionati() = True Then
            If Not IsDate(Me.txtGestione.Text & "/" & "2009") Or Not IsDate(Me.txtGestioneAl.Text & "/" & "2009") Then
                Response.Write("<script>alert('Date periodo di gestione errate!');</script>")
                Exit Sub
            End If
            Dim InizioGestione As String = Replace(par.AggiustaData(txtGestione.Text & "/2000"), "2000", "")
            Dim FineGestione As String = Replace(par.AggiustaData(txtGestioneAl.Text & "/2000"), "2000", "")

            ''''''''****************NUOVA SEZIONE FORNITORI****************
            ''If vIdFornitore > 0 Then


            ''    Dim Cognome As String = ""
            ''    Dim Nome As String = ""
            ''    Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            ''    '*************************CONGOME E NOME DELL'AMMINISTRATORE
            ''    par.cmd.CommandText = "SELECT COGNOME, NOME from SISCOM_MI.COND_AMMINISTRATORI WHERE ID = " & Me.cmbAmministratori.SelectedValue
            ''    lettore = par.cmd.ExecuteReader()
            ''    If lettore.Read Then
            ''        Cognome = par.IfNull(lettore("COGNOME"), "")
            ''        Nome = par.IfNull(lettore("NOME"), "")
            ''    End If
            ''    lettore.Close()
            ''    par.cmd.CommandText = "UPDATE SISCOM_MI.FORNITORI SET RAGIONE_SOCIALE = 'CONDOMINIO " & par.PulisciStrSql(Me.txtDenCondominio.Text.ToUpper) & "'" _
            ''    & " ,COGNOME='" & par.PulisciStrSql(Cognome) & "',NOME='" & par.PulisciStrSql(Nome) & "',COD_FISCALE='" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "',PARTITA_IVA='" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "', IBAN = '" & par.PulisciStrSql(Me.TxtIBAN.Text) & "' WHERE ID = " & vIdFornitore

            ''    par.cmd.ExecuteNonQuery()

            ''Else
            ''    Dim Cognome As String = ""
            ''    Dim Nome As String = ""
            ''    Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            ''    '*************************CONGOME E NOME DELL'AMMINISTRATORE
            ''    par.cmd.CommandText = "SELECT COGNOME, NOME from SISCOM_MI.COND_AMMINISTRATORI WHERE ID = " & Me.cmbAmministratori.SelectedValue
            ''    lettore = par.cmd.ExecuteReader()
            ''    If lettore.Read Then
            ''        Cognome = par.IfNull(lettore("COGNOME"), "")
            ''        Nome = par.IfNull(lettore("NOME"), "")
            ''    End If
            ''    lettore.Close()

            ''    '*************************DI FORNITORE NUOVO
            ''    par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_FORNITORI.NEXTVAL FROM DUAL"
            ''    lettore = par.cmd.ExecuteReader()
            ''    If lettore.Read Then
            ''        vIdFornitore = lettore(0)
            ''    End If
            ''    lettore.Close()

            ''    '************************SALVO IN FORNITORI
            ''    par.cmd.CommandText = "INSERT INTO SISCOM_MI.FORNITORI(ID,TIPO,RAGIONE_SOCIALE,COGNOME,NOME,COD_FISCALE,PARTITA_IVA,TIPO_R,IBAN,FL_BLOCCATO) VALUES " _
            ''    & " (" & vIdFornitore & ",'G','CONDOMINIO " & par.PulisciStrSql(Me.txtDenCondominio.Text.ToUpper) & "','" & par.PulisciStrSql(Cognome) _
            ''    & "','" & par.PulisciStrSql(Nome) & "','" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "','" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "','L','" & par.PulisciStrSql(Me.TxtIBAN.Text) & "',1)"
            ''    par.cmd.ExecuteNonQuery()

            ''End If

            par.cmd.CommandText = "UPDATE SISCOM_MI.CONDOMINI SET " _
            & " DENOMINAZIONE= '" & par.PulisciStrSql(Me.txtDenCondominio.Text.ToUpper) & "', DATA_COSTITUZIONE= '" & par.AggiustaData(Me.txtDataCost.Text) & "'," _
            & " COD_COMUNE= '" & Me.cmbComune.SelectedValue.ToString & "', TIPO_GESTIONE= '" & Me.cmbTipoGestione.SelectedValue.ToString & "', " _
            & " GESTIONE_INIZIO= '" & InizioGestione & "',GESTIONE_FINE= '" & FineGestione & "', NOTE= '" & par.PulisciStrSql(Me.txtNote.Text) & "', " _
            & " ID_BUILDING_MANAGER = " & DirectCast(Me.TabDatiTecnici1.FindControl("cmbBuildingManager"), DropDownList).SelectedValue & ",ID_FILIALE=" & DirectCast(Me.TabDatiTecnici1.FindControl("cmbFiliale"), DropDownList).SelectedValue & ", " _
            & " COD_TIPOLOGIA_IMP_RISCALD='" & DirectCast(Me.TabDatiTecnici1.FindControl("cmbTipoRisc"), DropDownList).SelectedValue & "', ID_CENTRALE_TERMICA = " & RitornaNullSeMenoUno(DirectCast(Me.TabDatiTecnici1.FindControl("cmbDenRiscald"), DropDownList).SelectedValue) & ", " _
            & " MIL_PRO_TOT_COND = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("txtMilProp"), TextBox).Text), "Null") & ", MIL_COMPRO_TOT_COND= " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("txtMillComp"), TextBox).Text), "Null") & ", MIL_SUP_TOT_COND= " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("TxtMillSup"), TextBox).Text), "Null") & ", " _
            & " MIL_GEST_TOT_COND = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("txtMillGest"), TextBox).Text), "Null") & ", " _
            & " TOT_ALLOGGI = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("TxtTotAlloggi"), TextBox).Text), "Null") & ", " _
            & " TOT_BOX = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("txtTotBox"), TextBox).Text), "Null") & ", TOT_NEGOZI = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("TxtTotNegozi"), TextBox).Text), "Null") & "," _
            & " TOT_DIVERSI = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("txtTotDiversi"), TextBox).Text), "Null") & ", TOT_SCALE = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("TxtTotScale"), TextBox).Text), "Null") & ", id_fornitore = " & RitornaNullSeMenoUno(Me.cmbFornitori.SelectedValue) & ",COD_FISCALE ='" & par.PulisciStrSql(Me.TxtCodFiscale.Text) & "',TIPOLOGIA = '" & Me.cmbTipoCond.SelectedValue.ToString & "'," _
            & " MIL_PRES_ASS_TOT_COND = " & par.IfEmpty(par.VirgoleInPunti(DirectCast(Me.TabMillesimalil1.FindControl("txtMillPres"), TextBox).Text), "Null") _
            & " WHERE ID = " & vIdCondominio
            par.cmd.ExecuteNonQuery()

            'par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
            'par.cmd.ExecuteNonQuery()

            'Dim I As Integer
            'For I = 0 To Me.ListEdifici.Items.Count() - 1
            '    If Me.ListEdifici.Items(I).Selected = True Then
            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_EDIFICI (ID_CONDOMINIO,ID_EDIFICIO) VALUES ( " & vIdCondominio & ", " & Me.ListEdifici.Items(I).Value & ")"
            '        par.cmd.ExecuteNonQuery()
            '    End If
            'Next

            'GestioneCondEdificiMillScale()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_SUPER WHERE ID_CONDOMINIO = " & vIdCondominio
            par.cmd.ExecuteNonQuery()
            Dim I As Integer
            For I = 0 To DirectCast(Me.TabDatiTecnici1.FindControl("ListSuperCond"), CheckBoxList).Items.Count() - 1
                If DirectCast(Me.TabDatiTecnici1.FindControl("ListSuperCond"), CheckBoxList).Items(I).Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_SUPER (ID_CONDOMINIO,ID_SUPERCONDOMINIO) VALUES ( " & vIdCondominio & ", " & DirectCast(Me.TabDatiTecnici1.FindControl("ListSuperCond"), CheckBoxList).Items(I).Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            'Else
            'Response.Write("<script>alert('Caricare almeno un edificio nella lista degli edifici!');</script>")
            'End If

            '****************MYEVENT*****************
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','AGGIORNAMENTO DATI DEL CONDOMINIO')"
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()


            'Riapro una nuova transazione
            Session.Item("LAVORAZIONE") = "1"
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)
            Response.Write("<SCRIPT>alert('Operazione completata correttamente!');</SCRIPT>")
            '***************************************************************************
            '**********************BLOCCO DEL CONDOMINIO FOR UPDATE NOWAIT**************
            par.cmd.CommandText = "SELECT CONDOMINI.* FROM SISCOM_MI.CONDOMINI WHERE CONDOMINI.ID = " & vIdCondominio ''& " FOR UPDATE NOWAIT"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1.Close()
            AggiornaDati()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)
        End Try
    End Sub


    'Private Sub FiltraEdifici()
    '    Try

    '        If Me.cmbEdifScelti.SelectedValue <> "-1" Then


    '            '*******************APERURA CONNESSIONE*********************
    '            If par.OracleConn.State = Data.ConnectionState.Closed Then
    '                par.OracleConn.Open()
    '                par.SettaCommand(par)
    '            End If


    '            Dim gest As Integer = 0
    '            'Me.DrLEdificio.Items.Clear()
    '            'DrLEdificio.Items.Add(New ListItem(" ", -1))


    '            par.cmd.CommandText = "SELECT distinct EDIFICI.id,EDIFICI.COD_EDIFICIO,EDIFICI.denominazione FROM SISCOM_MI.edifici where id_complesso = " & Me.cmbEdifScelti.SelectedValue.ToString & " order by denominazione asc"

    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader1.Read
    '                'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
    '                ListEdifici.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " ") & "- -" & "cod." & par.IfNull(myReader1("COD_EDIFICIO"), " "), par.IfNull(myReader1("id"), -1)))
    '                'DrLEdificio.Items.Add(New ListItem(par.IfNull(myReader1("denominazione"), " "), par.IfNull(myReader1("id"), -1)))
    '            End While


    '            myReader1.Close()

    '            If vIdCondominio = "" Then
    '                '*********************CHIUSURA CONNESSIONE**********************
    '                par.OracleConn.Close()
    '                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            End If


    '        End If

    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '        If par.OracleConn.State = Data.ConnectionState.Open Then
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If
    '    End Try
    'End Sub

    'Protected Sub cmbComplesso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbEdifScelti.SelectedIndexChanged
    '    If Me.cmbEdifScelti.SelectedValue <> "-1" Then
    '        FiltraEdifici()
    '        '*******************APERURA CONNESSIONE*********************
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONDOMINI WHERE ID_COMPLESSO = " & Me.cmbEdifScelti.SelectedValue.ToString
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader1.Read Then
    '            Response.Write("<script>alert('Su questo complesso è stato già registrato un condominio!');</script>")
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            'CaricaComplessi()
    '            CaricaEdifici()
    '            Exit Sub
    '        End If
    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    Else
    '        CaricaEdifici()
    '    End If
    '    ComuneDaComplesso()
    'End Sub
    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        If valorepass = "-1" Then
            RitornaNullSeMenoUno = "NULL"
        Else
            RitornaNullSeMenoUno = valorepass
        End If
        Return RitornaNullSeMenoUno
    End Function
    Private Sub DisattivaCampiModifica()
        'Me.cmbComplesso.Enabled = False
        'Me.DrLEdificio.Enabled = False

    End Sub

    Protected Sub btnEsci_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnEsci.Click
        If txtModificato.Value <> "111" Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSCOND" & vIdConnessione)
            End If
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HttpContext.Current.Session.Remove("CONNCOND" & vIdConnessione)
            Session.Item("LAVORAZIONE") = 0
            StoUscendo = True
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
            Response.Flush()
        Else
            txtModificato.Value = "1"
            'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub
    Protected Sub btnSalvaCambioAmm_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalvaCambioAmm.Click
        Try

            If Me.CmbAmministratori2.SelectedValue <> "-1" Then

                If Not String.IsNullOrEmpty(Me.txtDataFine.Text) AndAlso Not String.IsNullOrEmpty(Me.txtNuovaDataInizio.Text) Then
                    If par.AggiustaData(Me.txtDataFine.Text) < par.AggiustaData(Me.txtDataInizio.Text) Then
                        Response.Write("<script>alert('La data fine non può essere inferiore alla data inizio!');</script>")
                        Me.TextBox1.Value = 2
                        Exit Sub
                    End If
                    If par.AggiustaData(Me.txtNuovaDataInizio.Text) < par.AggiustaData(txtDataFine.Text) Then
                        Response.Write("<script>alert('La Nuova data inizio non può essere inferiore alla data di fine!');</script>")
                        Me.TextBox1.Value = 2
                        Exit Sub
                    End If
                    '*******************RICHIAMO LA CONNESSIONE*********************
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)
                    '*******************RICHIAMO LA TRANSAZIONE*********************
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans

                    par.cmd.CommandText = "UPDATE SISCOM_MI.COND_AMMINISTRAZIONE SET DATA_INIZIO = '" & par.AggiustaData(Me.txtDataInizio.Text) & "',DATA_FINE = '" & par.AggiustaData(Me.txtDataFine.Text) & "' WHERE ID_AMMINISTRATORE = " & Me.cmbAmministratori.SelectedValue & " AND ID_CONDOMINIO = " & vIdCondominio
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_AMMINISTRAZIONE (ID_CONDOMINIO,ID_AMMINISTRATORE,DATA_INIZIO,DATA_FINE) VALUES ( " & vIdCondominio & "," & Me.CmbAmministratori2.SelectedValue & ", '" & par.AggiustaData(Me.txtNuovaDataInizio.Text) & "', '')"
                    par.cmd.ExecuteNonQuery()

                    '****************MYEVENT*****************
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F31','')"
                    par.cmd.ExecuteNonQuery()

                    Me.cmbAmministratori.SelectedValue = Me.CmbAmministratori2.SelectedValue
                    '*************TEST
                    TabAmministratori1.cerca()


                    AggiornaDati()
                    Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                    Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")
                    Me.TextBox1.Value = 1

                Else
                    Response.Write("<script>alert('Per modificare l\'amministratore è  obbligatoria la data fine e la nuova data inizio!');</script>")

                End If
            Else
                If Not String.IsNullOrEmpty(Me.txtDataFine.Text) Then
                    If par.AggiustaData(Me.txtDataFine.Text) < par.AggiustaData(Me.txtDataInizio.Text) Then
                        Response.Write("<script>alert('La data fine non può essere inferiore alla data inizio!');</script>")
                        Me.TextBox1.Value = 2
                        Exit Sub
                    End If

                End If
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "UPDATE SISCOM_MI.COND_AMMINISTRAZIONE SET DATA_INIZIO = '" & par.AggiustaData(Me.txtDataInizio.Text) & "',DATA_FINE = '" & par.AggiustaData(Me.txtDataFine.Text) & "' WHERE ID_AMMINISTRATORE = " & Me.cmbAmministratori.SelectedValue & " AND ID_CONDOMINIO = " & vIdCondominio
                par.cmd.ExecuteNonQuery()


                AggiornaDati()
                Response.Write("<script>alert('Operazione eseguita correttamente!');</script>")
                Response.Write("<script>alert('Per completare l\'operazione fare click sul pulsante salva della finestra principale!');</script>")
                Me.TextBox1.Value = 1


            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)

        End Try
    End Sub
    Private Sub AggiornaDati()
        Try

            classetab = "tabbertab"
            tabvisibility = "visible"
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "SELECT CONDOMINI.*, COMUNI_NAZIONI.SIGLA FROM SISCOM_MI.CONDOMINI, SEPA.COMUNI_NAZIONI WHERE CONDOMINI.ID =  " & vIdCondominio & "  AND CONDOMINI.COD_COMUNE = COMUNI_NAZIONI.COD "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtDenCondominio.Text = myReader1("DENOMINAZIONE").ToString
                Me.txtDataCost.Text = par.FormattaData(myReader1("DATA_COSTITUZIONE").ToString)
                'Me.txtComune.Text = myReader1("COMUNE").ToString
                'Me.txtProvincia.Text = myReader1("PR").ToString
                Me.txtGestione.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_INIZIO").ToString), "/2000", "")
                Me.TXTgESTIONEAL.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_FINE").ToString), "/2000", "")

                Me.txtNote.Text = myReader1("NOTE").ToString
                Me.cmbTipoGestione.SelectedValue = myReader1("TIPO_GESTIONE").ToString


                par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
                Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReadEdifici.Read
                    Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
                End While
                myReadEdifici.Close()
                '    Me.DrLEdificio.SelectedValue = myReader1("ID_EDIFICIO")
                '    par.cmd.CommandText = "SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = " & Me.DrLEdificio.SelectedValue.ToString
                '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                '    If myReader2.Read Then
                '        Me.cmbComplesso.SelectedValue = myReader2("ID_COMPLESSO")
                '    End If
                '    myReader2.Close()

                idFornitore.Value = par.IfNull(myReader1("id_fornitore"), "")

            End If
            myReader1.Close()
            If Not String.IsNullOrEmpty(idFornitore.Value) Then
                par.caricaComboBox("select id_fornitore, iban from siscom_mi.FORNITORI_IBAN where id_fornitore = " & idFornitore.Value, Me.cmbIban, "iban", "iban", False)
            Else
                Me.cmbIban.Items.Clear()
            End If
            'AMMINISTRATORE
            par.cmd.CommandText = "SELECT ID_AMMINISTRATORE FROM SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.cmbAmministratori.SelectedValue = myReader1(0)
                Me.cmbAmministratori.Enabled = False
            End If
            myReader1.Close()

            'DIV CAMBIO AMMINISTRATORE
            Me.txtDataInizio.Text = ""
            Me.txtDataFine.Text = ""
            Me.txtNuovaDataInizio.Text = ""
            par.cmd.CommandText = "SELECT COGNOME, NOME, DATA_INIZIO FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtAmmCorrente.Text = myReader1("COGNOME") & " " & myReader1("NOME")
                Me.txtDataInizio.Text = par.FormattaData(myReader1("DATA_INIZIO").ToString)
            End If
            myReader1.Close()
            'Me.TabMillesimalil1.CalcolaPercentuale()
            ''*********TEST
            Me.TabInquilini1.Cerca()
            Me.TabMillesimalil1.CercaScale()
            TabDatiTecnici1.ApriRicerca()
            TabAmministratori1.cerca()
            Tab_Contabilita1.Cerca()
            'TabDatiTecnici1.CercaScale()
            'LE PERCENTUALI DEL CONDOMINIO IN ESAME POSSONO CAMBIARE E VANNO AGGIORNATE!!!!
            CType(Me.TabConvocazione1.FindControl("txtPercMilProp"), TextBox).Text = CType(Me.Page.FindControl("TabMillesimalil1").FindControl("txtMilPropComunePerc"), TextBox).Text
            CType(Me.TabConvocazione1.FindControl("txtpercSup"), TextBox).Text = CType(Me.Page.FindControl("TabMillesimalil1").FindControl("TxtMillSupComunePerc"), TextBox).Text
            CType(Me.TabConvocazione1.FindControl("txtPercMillComp"), TextBox).Text = CType(Me.Page.FindControl("TabMillesimalil1").FindControl("txtMillCompComunePerc"), TextBox).Text
            CType(Me.TabConvocazione1.FindControl("txtPercMillPresAss"), TextBox).Text = CType(Me.Page.FindControl("TabMillesimalil1").FindControl("txtMillCompComunePerc"), TextBox).Text

            ''********TEST*********
            'TabAmministratori1.cerca()
            'TabDatiTecnici1.CercaFabbricati()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)

        End Try
    End Sub
    Private Sub ApriFrmWithDBLock()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNCOND" & vIdConnessione, par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
            End If

            Dim id_fornitore As String = ""
            par.cmd.CommandText = "SELECT CONDOMINI.*, COMUNI_NAZIONI.SIGLA FROM SISCOM_MI.CONDOMINI, SEPA.COMUNI_NAZIONI WHERE CONDOMINI.ID =  " & vIdCondominio & "  AND CONDOMINI.COD_COMUNE = COMUNI_NAZIONI.COD"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtDenCondominio.Text = myReader1("DENOMINAZIONE").ToString
                Me.txtDataCost.Text = par.FormattaData(myReader1("DATA_COSTITUZIONE").ToString)
                Me.cmbComune.SelectedValue = myReader1("COD_COMUNE")
                'Me.txtComune.Text = myReader1("COMUNE").ToString
                Me.txtProvincia.Text = myReader1("SIGLA").ToString
                Me.txtGestione.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_INIZIO").ToString), "/2000", "")
                Me.txtGestioneAl.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_FINE").ToString), "/2000", "")
                Me.txtNote.Text = myReader1("NOTE").ToString
                Me.cmbTipoGestione.SelectedValue = myReader1("TIPO_GESTIONE").ToString
                Me.TxtCodFiscale.Text = myReader1("COD_FISCALE").ToString
                Me.cmbFornitori.SelectedValue = myReader1("id_fornitore").ToString
                'Me.TxtIBAN.Text = myReader1("IBAN").ToString
                Me.cmbTipoCond.SelectedValue = myReader1("TIPOLOGIA").ToString
                Me.txtCodCondominio.Text = Format(CDbl(vIdCondominio), "00000")
                id_fornitore = par.IfNull(myReader1("id_fornitore"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("TxtTotAlloggi"), TextBox).Text = par.IfNull(myReader1("TOT_ALLOGGI"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("txtTotBox"), TextBox).Text = par.IfNull(myReader1("TOT_BOX"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("TxtTotNegozi"), TextBox).Text = par.IfNull(myReader1("TOT_NEGOZI"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("txtTotDiversi"), TextBox).Text = par.IfNull(myReader1("TOT_DIVERSI"), "")
                par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
                Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReadEdifici.Read
                    Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
                End While
                myReadEdifici.Close()
                AddSelectedEdifici()

                '+++++SELEZIONE SIGLA DEL COMUNE
                par.cmd.CommandText = "SELECT  COMUNI_NAZIONI.SIGLA FROM SISCOM_MI.CONDOMINI, SEPA.COMUNI_NAZIONI WHERE  CONDOMINI.COD_COMUNE = COMUNI_NAZIONI.COD AND CONDOMINI.ID = " & vIdCondominio
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtProvincia.Text = myReader1("SIGLA").ToString
                End If


                'Me.DrLEdificio.SelectedValue = myReader1("ID_EDIFICIO")
                'par.cmd.CommandText = "SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = " & Me.DrLEdificio.SelectedValue.ToString
                'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader2.Read Then
                '    Me.cmbComplesso.SelectedValue = myReader2("ID_COMPLESSO")
                'End If
                'myReader2.Close()
            End If
            myReader1.Close()
            '++++nuovo iban in combo box
            If Not String.IsNullOrEmpty(id_fornitore) Then
                par.cmd.CommandText = "select id_fornitore, iban from siscom_mi.FORNITORI_IBAN where id_fornitore = " & id_fornitore
                myReader1 = par.cmd.ExecuteReader
                While myReader1.Read
                    cmbIban.Items.Add(New ListItem(par.IfNull(myReader1("IBAN"), " "), par.IfNull(myReader1("iban"), -1)))
                End While

                myReader1.Close()
            Else
                cmbIban.Items.Add(New ListItem(" ", -1))
            End If

            'AMMINISTRATORE
            par.cmd.CommandText = "SELECT ID_AMMINISTRATORE FROM SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.cmbAmministratori.SelectedValue = myReader1(0)
                Me.cmbAmministratori.Enabled = False
            End If
            myReader1.Close()

            'DIV CAMBIO AMMINISTRATORE
            par.cmd.CommandText = "SELECT COGNOME, NOME, DATA_INIZIO FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtAmmCorrente.Text = myReader1("COGNOME") & " " & myReader1("NOME")
                Me.txtDataInizio.Text = par.FormattaData(myReader1("DATA_INIZIO").ToString)
            End If
            myReader1.Close()

            'If Selezionati() = False Then
            '    FiltraEdifici()
            'End If

            classetab = "tabbertab"
            tabvisibility = "visible"
            '****NASCONDO IL BOTTONE PER IL CAMBIO DELL'AMMINISTRATORE
            ImgVisibility.Value = 0

            Me.btnSalva.Visible = False
            DirectCast(TabConvocazione1.FindControl("btnVisualizza"), ImageButton).Visible = False
            DirectCast(TabMillesimalil1.FindControl("btnVisualizza"), ImageButton).Visible = False
            DirectCast(TabInquilini1.FindControl("btnVisualizza"), ImageButton).Visible = False

            ''*********************CHIUSURA CONNESSIONE**********************
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            FrmSolaLettura()
            TabMillesSolaLett()
            TabAmministSolaLett()
            TabConvocSolaLett()
            TabDatiTecSolaLett()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try


    End Sub

    Private Sub ApriFrmSolaLettura()
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNCOND" & vIdConnessione, par.OracleConn) 'MEMORIZZAZIONE CONNESSIONE IN VARIABILE DI SESSIONE
            End If

            Dim id_fornitore As String = ""
            par.cmd.CommandText = "SELECT CONDOMINI.*, COMUNI_NAZIONI.SIGLA FROM SISCOM_MI.CONDOMINI, SEPA.COMUNI_NAZIONI WHERE CONDOMINI.ID =  " & vIdCondominio & "  AND CONDOMINI.COD_COMUNE = COMUNI_NAZIONI.COD"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtDenCondominio.Text = myReader1("DENOMINAZIONE").ToString
                Me.txtDataCost.Text = par.FormattaData(myReader1("DATA_COSTITUZIONE").ToString)
                Me.cmbComune.SelectedValue = myReader1("COD_COMUNE")
                'Me.txtComune.Text = myReader1("COMUNE").ToString
                Me.txtProvincia.Text = myReader1("SIGLA").ToString
                Me.txtGestione.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_INIZIO").ToString), "/2000", "")
                Me.txtGestioneAl.Text = Replace(par.FormattaData("2000" & myReader1("GESTIONE_FINE").ToString), "/2000", "")
                Me.txtNote.Text = myReader1("NOTE").ToString
                Me.cmbTipoGestione.SelectedValue = myReader1("TIPO_GESTIONE").ToString
                Me.TxtCodFiscale.Text = myReader1("COD_FISCALE").ToString
                Me.cmbFornitori.SelectedValue = myReader1("id_fornitore").ToString
                'Me.TxtIBAN.Text = myReader1("IBAN").ToString
                Me.cmbTipoCond.SelectedValue = myReader1("TIPOLOGIA").ToString
                id_fornitore = par.IfNull(myReader1("id_fornitore"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("TxtTotAlloggi"), TextBox).Text = par.IfNull(myReader1("TOT_ALLOGGI"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("txtTotBox"), TextBox).Text = par.IfNull(myReader1("TOT_BOX"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("TxtTotNegozi"), TextBox).Text = par.IfNull(myReader1("TOT_NEGOZI"), "")
                DirectCast(Me.TabMillesimalil1.FindControl("txtTotDiversi"), TextBox).Text = par.IfNull(myReader1("TOT_DIVERSI"), "")
                par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
                Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReadEdifici.Read
                    Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
                End While
                myReadEdifici.Close()
                AddSelectedEdifici()

                '+++++SELEZIONE SIGLA DEL COMUNE
                par.cmd.CommandText = "SELECT  COMUNI_NAZIONI.SIGLA FROM SISCOM_MI.CONDOMINI, SEPA.COMUNI_NAZIONI WHERE  CONDOMINI.COD_COMUNE = COMUNI_NAZIONI.COD AND CONDOMINI.ID = " & vIdCondominio
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtProvincia.Text = myReader1("SIGLA").ToString
                End If


                'Me.DrLEdificio.SelectedValue = myReader1("ID_EDIFICIO")
                'par.cmd.CommandText = "SELECT ID_COMPLESSO FROM SISCOM_MI.EDIFICI WHERE EDIFICI.ID = " & Me.DrLEdificio.SelectedValue.ToString
                'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'If myReader2.Read Then
                '    Me.cmbComplesso.SelectedValue = myReader2("ID_COMPLESSO")
                'End If
                'myReader2.Close()
            End If
            myReader1.Close()
            '++++nuovo iban in combo box
            If Not String.IsNullOrEmpty(id_fornitore) Then
                par.cmd.CommandText = "select id_fornitore, iban from siscom_mi.FORNITORI_IBAN where id_fornitore = " & id_fornitore
                myReader1 = par.cmd.ExecuteReader
                While myReader1.Read
                    cmbIban.Items.Add(New ListItem(par.IfNull(myReader1("IBAN"), " "), par.IfNull(myReader1("iban"), -1)))
                End While

                myReader1.Close()
            Else
                cmbIban.Items.Add(New ListItem(" ", -1))
            End If

            'AMMINISTRATORE
            par.cmd.CommandText = "SELECT ID_AMMINISTRATORE FROM SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.cmbAmministratori.SelectedValue = myReader1(0)
                Me.cmbAmministratori.Enabled = False
            End If
            myReader1.Close()

            'DIV CAMBIO AMMINISTRATORE
            par.cmd.CommandText = "SELECT COGNOME, NOME, DATA_INIZIO FROM SISCOM_MI.COND_AMMINISTRATORI, SISCOM_MI.COND_AMMINISTRAZIONE WHERE ID_CONDOMINIO =" & vIdCondominio & " AND COND_AMMINISTRATORI.ID = COND_AMMINISTRAZIONE.ID_AMMINISTRATORE AND DATA_FINE IS NULL"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtAmmCorrente.Text = myReader1("COGNOME") & " " & myReader1("NOME")
                Me.txtDataInizio.Text = par.FormattaData(myReader1("DATA_INIZIO").ToString)
            End If
            myReader1.Close()

            'If Selezionati() = False Then
            '    FiltraEdifici()
            'End If

            classetab = "tabbertab"
            tabvisibility = "visible"
            '****NASCONDO IL BOTTONE PER IL CAMBIO DELL'AMMINISTRATORE
            ImgVisibility.Value = 0

            Me.btnSalva.Visible = False
            DirectCast(TabConvocazione1.FindControl("btnVisualizza"), ImageButton).Visible = False
            DirectCast(TabMillesimalil1.FindControl("btnVisualizza"), ImageButton).Visible = False
            DirectCast(TabInquilini1.FindControl("btnVisualizza"), ImageButton).Visible = False

            ''*********************CHIUSURA CONNESSIONE**********************
            'par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            FrmSolaLettura()
            TabMillesSolaLett()
            TabAmministSolaLett()
            TabConvocSolaLett()
            TabDatiTecSolaLett()
            If Not Session.Item("LAVORAZIONE") = 1 Then
                'Apro una nuova transazione
                Session.Item("LAVORAZIONE") = "1"
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)
            End If

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub TabAmministSolaLett()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.TabAmministratori1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
    End Sub
    Private Sub TabMillesSolaLett()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.TabMillesimalil1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
    End Sub
    Private Sub TabConvocSolaLett()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.TabConvocazione1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
        DirectCast(Me.TabConvocazione1.FindControl("btnVisualizza"), ImageButton).Visible = False
    End Sub
    Private Sub TabDatiTecSolaLett()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.TabDatiTecnici1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
    End Sub

    Private Sub FrmSolaLettura()
        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
        Next
        Me.ListEdifici.Enabled = False
    End Sub
    'Private Sub ComuneDaComplesso()
    '    '*******************APERURA CONNESSIONE*********************
    '    If par.OracleConn.State = Data.ConnectionState.Closed Then
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If
    '    If Selezionati() = True Then
    '        par.cmd.CommandText = "SELECT SIGLA, COD FROM SEPA.COMUNI_NAZIONI, SISCOM_MI.INDIRIZZI, SISCOM_MI.COMPLESSI_IMMOBILIARI WHERE COMPLESSI_IMMOBILIARI.ID_INDIRIZZO_RIFERIMENTO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD AND COMPLESSI_IMMOBILIARI.ID = " & Me.cmbEdifScelti.SelectedValue
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader1.Read Then
    '            Me.cmbComune.SelectedValue = myReader1("COD").ToString
    '            Me.txtProvincia.Text = myReader1("SIGLA").ToString
    '        End If
    '    Else
    '        Me.cmbComune.SelectedValue = "-1"
    '        Me.txtProvincia.Text = ""
    '    End If
    '    '*********************CHIUSURA CONNESSIONE**********************
    '    par.OracleConn.Close()
    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    'End Sub
    '***CONTROLLARE***
    'Private Sub ComuneDaEdificio()

    '    If Me.cmbComplesso.SelectedValue = "-1" Then
    '        '*******************APERURA CONNESSIONE*********************
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        If Selezionati() = True Then
    '            par.cmd.CommandText = "SELECT SIGLA, COD FROM SEPA.COMUNI_NAZIONI, SISCOM_MI.INDIRIZZI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD AND EDIFICI.ID = " & Me.DrLEdificio.SelectedValue
    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader1.Read Then
    '                Me.cmbComune.SelectedValue = myReader1("COD").ToString
    '                Me.txtProvincia.Text = myReader1("SIGLA").ToString
    '            End If
    '        Else
    '            Me.cmbComune.SelectedValue = "-1"
    '            Me.txtProvincia.Text = ""
    '        End If
    '    End If

    '    '*********************CHIUSURA CONNESSIONE**********************
    '    par.OracleConn.Close()
    '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    'End Sub

    'Protected Sub DrLEdificio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DrLEdificio.SelectedIndexChanged
    '    If Me.DrLEdificio.SelectedValue <> "-1" Then
    '        '*******************APERURA CONNESSIONE*********************
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)
    '        End If
    '        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONDOMINI WHERE ID_EDIFICIO = " & Me.DrLEdificio.SelectedValue.ToString
    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '        If myReader1.Read Then
    '            Response.Write("<script>alert('Su questo edificio è stato già registrato un condominio!');</script>")
    '            '*********************CHIUSURA CONNESSIONE**********************
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '            FiltraEdifici()
    '            Exit Sub
    '        End If
    '        '*********************CHIUSURA CONNESSIONE**********************
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '    End If

    '    ComuneDaEdificio()
    '    AggiornaScale()
    'End Sub
    'Private Sub AggiornaScale()

    '    Try
    '        'vIdEdificio = DrLEdificio.SelectedValue

    '        '*******************RICHIAMO LA CONNESSIONE*********************
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)

    '        '*******************RICHIAMO LA TRANSAZIONE*********************
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & CType(Me.Page, Object).vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '        ‘‘par.cmd.Transaction = par.myTrans

    '        If vIdCondominio <> "" AndAlso Selezionati() = True Then
    '            'par.cmd.CommandText = "SELECT COND_MIL_SCALE.*,SCALE_EDIFICI.DESCRIZIONE,  ((100*MIL_TOT_COM)/1000) AS PERCENTU FROM SISCOM_MI.COND_MIL_SCALE, SISCOM_MI.SCALE_EDIFICI WHERE SCALE_EDIFICI.ID = ID_SCALA AND ID_EDIFICIO = " & vIdEdificio

    '            par.cmd.CommandText = "SELECT COND_MIL_SCALE.*,SCALE_EDIFICI.DESCRIZIONE,  ((100*MIL_TOT_COM)/1000) AS PERCENTU FROM SISCOM_MI.COND_MIL_SCALE, SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.COND_EDIFICI WHERE SCALE_EDIFICI.ID = ID_SCALA AND SCALE_EDIFICI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO AND COND_EDIFICI.ID_CONDOMINIO = " & vIdCondominio
    '            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
    '            Dim dt As New Data.DataTable
    '            da.Fill(dt)
    '            For Each row As Data.DataRow In dt.Rows
    '                par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_MIL_SCALE WHERE ID_SCALA = " & row.Item("ID_SCALA")
    '                par.cmd.ExecuteNonQuery()
    '            Next

    '            '***********INSERIMENTO DELLE SCALE E DEI VALORI MILLESIMALI SULLE STESSE DI COMPETENZA DEL COMUNE
    '            par.cmd.CommandText = "SELECT SCALE_EDIFICI.ID AS id_scala,(SELECT SUM(VALORE_MILLESIMO) AS MILL_SCALA FROM SISCOM_MI.VALORI_MILLESIMALI WHERE ID_TABELLA = TABELLE_MILLESIMALI_SCALE.ID_TABELLA) AS GEST_COMUNE FROM SISCOM_MI.SCALE_EDIFICI, SISCOM_MI.TABELLE_MILLESIMALI_SCALE, COND_EDIFICI WHERE SCALE_EDIFICI.ID_EDIFICIO = COND_EDIFICI.ID_EDIFICIO AND COND_EDIFICI.ID_CONDOMINIO = " & vIdCondominio & " AND TABELLE_MILLESIMALI_SCALE.ID_SCALA(+) = SCALE_EDIFICI.ID"

    '            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            While myReader1.Read
    '                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MIL_SCALE (ID_SCALA, MIL_TOT_COM) VALUES (" & myReader1("ID_SCALA") & "," & par.VirgoleInPunti(par.IfNull(myReader1("GEST_COMUNE"), "Null")) & ")"
    '                par.cmd.ExecuteNonQuery()
    '            End While
    '            myReader1.Close()
    '            '****************MYEVENT*****************
    '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F34','MILLESIMI SCALE CONDOMINIO')"
    '            par.cmd.ExecuteNonQuery()
    '            ''*****TEST
    '            TabMillesimalil1.CercaScale()
    '        End If
    '        'vIdEdificio = DrLEdificio.SelectedValue
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)

    '    End Try

    'End Sub
    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete

        If StoUscendo = False Then
            tabdefault1 = ""
            tabdefault2 = ""
            tabdefault3 = ""
            tabdefault4 = ""
            tabdefault5 = ""
            tabdefault6 = ""
            tabdefault7 = ""
            tabdefault8 = ""


            Select Case txttab.Value
                Case "1"
                    tabdefault1 = "tabbertabdefault"
                Case "2"
                    tabdefault2 = "tabbertabdefault"
                Case "3"
                    tabdefault3 = "tabbertabdefault"
                Case "4"
                    tabdefault4 = "tabbertabdefault"
                Case "5"
                    tabdefault5 = "tabbertabdefault"
                Case "6"
                    tabdefault6 = "tabbertabdefault"
                Case "7"
                    tabdefault7 = "tabbertabdefault"
                Case "8"
                    tabdefault8 = "tabbertabdefault"


            End Select
            Me.TabConvocazione1.cerca()
            Me.TabMillesimalil1.CalcolaPercentuale()
            'TabConvocazione1.RiempiCampi()
            'Me.Tab_Contabilita1.Cerca()
            Response.Flush()

        Else
            StoUscendo = False
        End If




    End Sub
    'Private Sub CalcolaPercentuale()

    '    '*********PERCENTUALE SU MILLESIMI PROPRIETA'
    '    If Not String.IsNullOrEmpty(DirectCast(Me.TabMillesimalil1.FindControl("txtMilPropComune"), TextBox).Text) AndAlso par.IfEmpty(DirectCast(Me.TabMillesimalil1.FindControl("txtMilProp"), TextBox).Text, 0) > 0 Then
    '        DirectCast(Me.TabMillesimalil1.FindControl("txtMilPropComunePerc"), TextBox).Text = Format(((DirectCast(Me.TabMillesimalil1.FindControl("txtMilPropComune"), TextBox).Text * 100) / DirectCast(Me.TabMillesimalil1.FindControl("txtMilProp"), TextBox).Text), "0.00")
    '    End If
    '    '*********PERCENTUALE SU MILLESIMI COMPROPRIETA'
    '    If Not String.IsNullOrEmpty(DirectCast(Me.TabMillesimalil1.FindControl("txtMillCompComune"), TextBox).Text) AndAlso par.IfEmpty(DirectCast(Me.TabMillesimalil1.FindControl("txtMillComp"), TextBox).Text, 0) > 0 Then
    '        DirectCast(Me.TabMillesimalil1.FindControl("txtMillCompComunePerc"), TextBox).Text = Format(((DirectCast(Me.TabMillesimalil1.FindControl("txtMillCompComune"), TextBox).Text * 100) / DirectCast(Me.TabMillesimalil1.FindControl("txtMillComp"), TextBox).Text), "0.00")
    '    End If

    '    '*********PERCENTUALE SU MILLESIMI SUPERFICI
    '    If Not String.IsNullOrEmpty(DirectCast(Me.TabMillesimalil1.FindControl("TxtMillSupComune"), TextBox).Text) AndAlso par.IfEmpty(DirectCast(Me.TabMillesimalil1.FindControl("TxtMillSup"), TextBox).Text, 0) > 0 Then
    '        DirectCast(Me.TabMillesimalil1.FindControl("TxtMillSupComunePerc"), TextBox).Text = Format(((DirectCast(Me.TabMillesimalil1.FindControl("TxtMillSupComune"), TextBox).Text * 100) / DirectCast(Me.TabMillesimalil1.FindControl("TxtMillSup"), TextBox).Text), "0.00")
    '    End If
    '    '*********PERCENTUALE SU MILLESIMI GESTIONE
    '    If Not String.IsNullOrEmpty(DirectCast(Me.TabMillesimalil1.FindControl("txtMillGestComune"), TextBox).Text) AndAlso par.IfEmpty(DirectCast(Me.TabMillesimalil1.FindControl("txtMillGest"), TextBox).Text, 0) > 0 Then
    '        DirectCast(Me.TabMillesimalil1.FindControl("txtMillGestComunePerc"), TextBox).Text = Format(((DirectCast(Me.TabMillesimalil1.FindControl("txtMillGestComune"), TextBox).Text * 100) / DirectCast(Me.TabMillesimalil1.FindControl("txtMillGest"), TextBox).Text), "0.00")
    '    End If

    'End Sub
    Private Sub ModificheCampi()


        'CTRL = Nothing

        'For Each CTRL In Me.TabAmministratori1.Controls
        '    If TypeOf CTRL Is TextBox Then
        '        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
        '    ElseIf TypeOf CTRL Is DropDownList Then
        '        DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
        '    End If
        'Next

        'Controllo modifica campi nel form
        Dim CTRL As Control
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next

        CTRL = Nothing
        For Each CTRL In Me.TabMillesimalil1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next

        CTRL = Nothing

        For Each CTRL In Me.TabConvocazione1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next

        CTRL = Nothing

        For Each CTRL In Me.TabDatiTecnici1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
        For Each CTRL In Me.TabInquilini1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            End If
        Next
    End Sub

    Protected Sub btnIndietro_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnIndietro.Click
        If txtModificato.Value <> "111" Then
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSCOND" & vIdConnessione)
            End If
            par.OracleConn.Close()
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HttpContext.Current.Session.Remove("CONNCOND" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = 0
            Select Case Request.QueryString("CALL")
                Case "RisultatiInquilini"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?P=" & Request.QueryString("P"))
                Case "RisultatiIndirizzo"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?C=" & Request.QueryString("C") & "&E=" & Request.QueryString("E") & "&P=" & Request.QueryString("P"))
                Case "ResultAmministratore"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?AMM=" & Request.QueryString("AMM") & "&P=" & Request.QueryString("P"))
                Case "RisSoloIndirizzo"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?I=" & Request.QueryString("I") & "&Civ=" & Request.QueryString("Civ") & "&P=" & Request.QueryString("P"))
                Case "AnCondomini"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?P=" & Request.QueryString("P"))
                Case "PagamScadenza"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx")
                Case "RisultatiPagamenti"
                    Dim condSelezionati As String = "-1"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?COND=" & Request.QueryString("CONDSELEZ") & "&ANNODA=" & Request.QueryString("ANNODA") & "&ANNOA=" & Request.QueryString("ANNOA") & "&NUMADPDA=" & Request.QueryString("NUMADPDA") & "&NUMADPA=" & Request.QueryString("NUMADPA") & "&IMPADPDA=" & Request.QueryString("IMPADPDA") & "&IMPADPA=" & Request.QueryString("IMPADPA") & "&NUMMANDA=" & Request.QueryString("NUMMANDA") & "&NUMMANA=" & Request.QueryString("NUMMANA") & "&DATAMANDDA=" & Request.QueryString("DATAMANDDA") & "&DATAMANDA=" & Request.QueryString("DATAMANDA"))
                Case "RptMorEmesse"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx?IDCOND=" & Request.QueryString("IDC") & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL"))
                Case "AnCondomini"
                    Response.Redirect(Request.QueryString("CALL") & ".aspx")
                Case Else
                    Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                    'Case Else
                    '    Response.Redirect(Request.QueryString("C") & ".aspx?E=" & Request.QueryString("EDIFICIO"))
            End Select
        Else
            txtModificato.Value = "1"
            'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub
    Public Function Selezionati() As Boolean
        Selezionati = False
        Try
            Dim I As Integer
            Selezionati = False
            vSelezionati = False
            For I = 0 To Me.ListEdifici.Items.Count() - 1
                If Me.ListEdifici.Items(I).Selected = True Then
                    Selezionati = True
                    vSelezionati = 1
                    Exit For
                End If
            Next
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Function

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        AddSelectedEdifici()
        'AGGIORNO LA LISTA DEL TAB INQUILINI NEL CASO IN CUI VENGA AGGIUNTO UN NUOVO EDIFICIO O ELIMINATO UN EDIFICIO DAL CONDOMINIO
        If vIdCondominio <> "" Then
            GestioneCondEdificiMillScale()
        End If
        If vIdCondominio = "" Then
            ComuneDaEdSelected()
        End If
        TabInquilini1.Cerca()

    End Sub
    'Private Sub UpdateCondEdif()
    '    Try


    '        If vIdCondominio <> "" Then
    '            '*******************RICHIAMO LA CONNESSIONE*********************
    '            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)
    '            '*******************RICHIAMO LA TRANSAZIONE*********************
    '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '            ‘‘par.cmd.Transaction = par.myTrans

    '            par.cmd.CommandText = "DELETE FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
    '            par.cmd.ExecuteNonQuery()

    '            Dim I As Integer
    '            For I = 0 To Me.ListEdifici.Items.Count() - 1
    '                If Me.ListEdifici.Items(I).Selected = True Then
    '                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_EDIFICI (ID_CONDOMINIO,ID_EDIFICIO) VALUES ( " & vIdCondominio & ", " & Me.ListEdifici.Items(I).Value & ")"
    '                    par.cmd.ExecuteNonQuery()
    '                End If
    '            Next
    '            AggiornaDati()
    '        End If
    '    Catch ex As Exception
    '        Me.lblErrore.Visible = True
    '        lblErrore.Text = ex.Message
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSCOND" & vIdConnessione, par.myTrans)

    '    End Try
    'End Sub
    Private Sub AddSelectedEdifici()
        Try
            Dim COLORE As String = "#E6E6E6"

            lblEdifici.Text = ""
            Dim testoTabella As String
            'Me.cmbEdifScelti.Items.Clear()

            If Selezionati() = True Then
                testoTabella = ""
                testoTabella = "<table cellpadding='0' cellspacing='0' width='100%'>" & vbCrLf
                Dim I As Integer
                For I = 0 To Me.ListEdifici.Items.Count() - 1
                    If Me.ListEdifici.Items(I).Selected = True Then
                        testoTabella = testoTabella _
                            & "<tr bgcolor = '" & COLORE & "'>" _
                            & "<td style='height: 19px'>" _
                            & "<span style='font-size: 8pt; font-family: Arial'>" & ListEdifici.Items(I).Text & "</a></span></td>" _
                            & "<td style='height: 19px'>" _
                            & "</td>" _
                            & "</tr>"
                        If COLORE = "#FFFFFF" Then
                            COLORE = "#E6E6E6"
                        Else
                            COLORE = "#FFFFFF"
                        End If

                        'cmbEdifScelti.Items.Add(New ListItem(ListEdifici.Items(I).Text, ListEdifici.Items(I).Value))
                    End If
                Next
                lblEdifici.Text = testoTabella & "</table>"
            End If

            Me.ScegCompVis.Value = 1

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message

        End Try
    End Sub
    Private Sub DeleteEdificiFromCondominio()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            '*******************RICHIAMO LA TRANSAZIONE*********************
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "DELETE SISCOM_MI.COND_MIL_SCALE WHERE ID_EDIFICIO IN(" & EdificiToDelete.Value & ") AND ID_CONDOMINIO = " & vIdCondominio
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE SISCOM_MI.COND_UI WHERE ID_EDIFICIO IN(" & EdificiToDelete.Value & ") AND ID_CONDOMINIO = " & vIdCondominio
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "DELETE SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO IN(" & EdificiToDelete.Value & ") AND ID_CONDOMINIO = " & vIdCondominio
            par.cmd.ExecuteNonQuery()

            Dim lista() As String = EdificiToDelete.Value.Split(New Char() {","})

            For Each s As String In lista
                Me.ListEdifici.Items.FindByValue(s).Selected = False
            Next


            par.cmd.CommandText = "SELECT ID_EDIFICIO FROM SISCOM_MI.COND_EDIFICI WHERE ID_CONDOMINIO = " & vIdCondominio
            Dim myReadEdifici As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReadEdifici.Read
                Me.ListEdifici.Items.FindByValue(myReadEdifici("ID_EDIFICIO")).Selected = True
            End While
            myReadEdifici.Close()
            AddSelectedEdifici()

        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
            par.myTrans.Rollback()
        End Try
    End Sub

    Private Sub GestioneCondEdificiMillScale()
        Try


            If vIdCondominio <> "" Then

                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                '*******************RICHIAMO LA TRANSAZIONE*********************
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSCOND" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT COND_EDIFICI.*, edifici.denominazione FROM  SISCOM_MI.COND_EDIFICI, SISCOM_MI.EDIFICI WHERE EDIFICI.ID=COND_EDIFICI.ID_EDIFICIO AND ID_CONDOMINIO = " & vIdCondominio
                Dim I As Integer
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                Dim primo As Boolean = True

                While myReader1.Read
                    If Me.ListEdifici.Items.FindByValue(myReader1("ID_EDIFICIO")).Selected = False Then
                        Try
                            par.cmd.CommandText = "DELETE SISCOM_MI.COND_EDIFICI WHERE ID_EDIFICIO = " & myReader1("ID_EDIFICIO") & " AND ID_CONDOMINIO = " & vIdCondominio
                            par.cmd.ExecuteNonQuery()

                        Catch exeptOra As Oracle.DataAccess.Client.OracleException
                            If exeptOra.Number = 2292 Then
                                If primo = True Then
                                    Dim codeFunction As String = "var chiediConferma;" _
                                                               & " chiediConferma = window.confirm('Attenzione...per l\'edificio che si vuole cancellare sono stati inseriti dati!\nConfermi la cancellazione?');" _
                                                               & " if (chiediConferma == true) {" _
                                                               & " document.getElementById('ConfEliminaEdifici').value = '1';" _
                                                               & "} form1.submit();"
                                    ClientScript.RegisterStartupScript(GetType(String), "MyMessage", "<script language='javascript'>" & codeFunction & "</script>")
                                    primo = False
                                    EdificiToDelete.Value = myReader1("ID_EDIFICIO")
                                    Me.ListEdifici.Items.FindByValue(myReader1("ID_EDIFICIO")).Selected = True
                                Else
                                    Me.ListEdifici.Items.FindByValue(myReader1("ID_EDIFICIO")).Selected = True
                                    Me.EdificiToDelete.Value = EdificiToDelete.Value & "," & myReader1("ID_EDIFICIO")
                                End If

                            End If

                            ''****************MYEVENT*****************
                            'par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','CANCELLAZIONE EDIFICIO E RELATIVE SCALE " & par.PulisciStrSql(par.IfNull(myReader1("DENOMINAZIONE"), "")) & "')"
                            'par.cmd.ExecuteNonQuery()
                            ''*******************************FINE****************************************
                            ''***************************************************************************

                            'MODIFICATA DELETE DOPO AGGIUNTA DI ID_EDIFICIO NELLA TABELLA COND_MIL_SCALE, MA IN TEORIA NON DOVREI FARE NULLA PERCHè CANCELLA CAMPI A CASCATA
                            'par.cmd.CommandText = "DELETE SISCOM_MI.COND_MIL_SCALE WHERE ID_SCALA IN (SELECT ID FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & myReader1("ID_EDIFICIO") & ")"
                            'par.cmd.CommandText = "DELETE SISCOM_MI.COND_MIL_SCALE WHERE ID_EDIFICIO= " & myReader1("ID_EDIFICIO") & " AND ID_CONDOMINIO = " & vIdCondominio

                            'par.cmd.ExecuteNonQuery()
                        End Try

                    End If
                End While
                myReader1.Close()

                For I = 0 To Me.ListEdifici.Items.Count() - 1
                    If Me.ListEdifici.Items(I).Selected = True Then
                        Try
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_EDIFICI (ID_CONDOMINIO,ID_EDIFICIO) VALUES ( " & vIdCondominio & ", " & Me.ListEdifici.Items(I).Value & ")"
                            par.cmd.ExecuteNonQuery()


                            '****************MYEVENT*****************
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONDOMINI (ID_CONDOMINIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdCondominio & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','AGGIUNTA EDIFICIO E RELATIVE SCALE " & par.PulisciStrSql(Me.ListEdifici.Items(I).Text) & "')"
                            par.cmd.ExecuteNonQuery()
                            '*******************************FINE****************************************
                            '***************************************************************************

                            par.cmd.CommandText = "SELECT ID AS SCALA FROM SISCOM_MI.SCALE_EDIFICI WHERE ID_EDIFICIO = " & Me.ListEdifici.Items(I).Value
                            myReader1 = par.cmd.ExecuteReader()
                            While myReader1.Read
                                '++++SCRIVO ANCE L'ID_EDIFICIO IN COND_MIL_SCALE 30/04/2010
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.COND_MIL_SCALE (ID_SCALA, ID_CONDOMINIO,ID_EDIFICIO) VALUES (" & myReader1("SCALA") & ", " & vIdCondominio & "," & Me.ListEdifici.Items(I).Value & ")"
                                par.cmd.ExecuteNonQuery()
                            End While


                        Catch EX1 As Oracle.DataAccess.Client.OracleException
                            If EX1.Number = 1 Then

                            Else
                                Exit Try
                            End If
                        End Try
                    End If
                Next
            End If
            TabMillesimalil1.CercaScale()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try
    End Sub
    Private Sub ComuneDaEdSelected()
        Try

            '    '*******************APERURA CONNESSIONE*********************
            par.OracleConn.Open()
            par.SettaCommand(par)

            If Selezionati() = True Then
                Dim I As Integer
                Dim IdEdif As String = ""
                For I = 0 To Me.ListEdifici.Items.Count() - 1
                    If Me.ListEdifici.Items(I).Selected = True Then
                        IdEdif = ListEdifici.Items(I).Value
                        Exit For
                    End If
                Next

                par.cmd.CommandText = "SELECT SIGLA, COD FROM SEPA.COMUNI_NAZIONI, SISCOM_MI.INDIRIZZI,  SISCOM_MI.EDIFICI WHERE EDIFICI.ID_INDIRIZZO_PRINCIPALE = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = SEPA.COMUNI_NAZIONI.COD AND EDIFICI.ID =" & IdEdif
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.cmbComune.SelectedValue = myReader1("COD").ToString
                    Me.txtProvincia.Text = myReader1("SIGLA").ToString
                End If
            Else
                Me.cmbComune.SelectedValue = "-1"
                Me.txtProvincia.Text = ""
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            Me.lblErrore.Visible = True
            lblErrore.Text = ex.Message
        End Try

    End Sub


    Protected Sub btnFornitore_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnFornitore.Click
        CaricaFornitori()
        Me.cmbFornitori.SelectedValue = idFornitore.Value
    End Sub
End Class
