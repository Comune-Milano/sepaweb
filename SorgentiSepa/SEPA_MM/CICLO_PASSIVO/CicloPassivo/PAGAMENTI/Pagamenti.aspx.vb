Imports System.Collections
Imports System.Math
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing
Imports Telerik.Web.UI


Partial Class PAGAMENTI_Pagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreStato As String
    'Public sValoreStatoPagamento As String

    Public sValoreEsercizioFinanziarioR As String

    Public sValoreFornitore As String
    Public sValoreStruttura As String

    Public sValoreDataP_Dal As String
    Public sValoreDataP_Al As String

    'Public sValoreDataE_Dal As String
    'Public sValoreDataE_Al As String

    Public sValoreODL As String
    Public sValoreAnno As String

    Public sOrdinamento As String
    Public sValoreProvenienza As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0


        If Not IsPostBack Then

            Try
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("Ordine")
                idVoce = par.IfEmpty(Strings.Trim(Request.QueryString("V")), -1)   'ID_VOCE_PF
                txtDScad.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

                Me.txtDScad.Text = Format(Now, "dd/MM/yyyy")

                'DA RICERCA SELETTIVA
                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreStato = Request.QueryString("ST")    'della query di ricerca 0=PRENOTATO (1=EMESSO e 5=LIQUIDATO da PAGAMENTI)
                'sValoreStatoPagamento = Request.QueryString("STATO")    'PRENOTATO, EMESSO, LIQUIDATO

                sValoreFornitore = Request.QueryString("FO")    'ID_FORNITORE
                sValoreStruttura = Request.QueryString("STR")

                sValoreDataP_Dal = Request.QueryString("DALP")
                sValoreDataP_Al = Request.QueryString("ALP")

                'sValoreDataE_Dal = Request.QueryString("DALE")
                'sValoreDataE_Al = Request.QueryString("ALE")

                'DA RICERCA DIRETTA
                sValoreODL = Request.QueryString("ODL")
                sValoreAnno = Request.QueryString("ANNO")


                sOrdinamento = Request.QueryString("ORD")

                sValoreProvenienza = Request.QueryString("PROVENIENZA")

                vIdODL = 0
                vIdPrenotazioni = 0
                vIdPagamenti = 0
                vIdPagamentiRIT = 0


                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    vIdODL = Request.QueryString("ID")
                Else
                    vIdODL = Session.Item("ID")
                End If

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    RadWindowManager1.RadAlert("Impossibile visualizzare!", 300, 150, "", "")
                    'Response.Write("IMPOSSIBILE VISUALIZZARE")
                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If


                Me.txtVisualizza.Value = 0         'BOZZA
                Me.txtStatoPagamento.Value = -1    'BOZZA
                Me.txtSTATO.Value = 0

                Setta_StataoODL(0)              'BOZZA
                CaricaFornitori()

                If vIdODL <> 0 Then
                    Me.btnINDIETRO.Enabled = True
                    'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)
                    VisualizzaDati()
                    txtindietro.Text = 0
                Else
                    Me.txtID_STRUTTURA.Value = Session.Item("ID_STRUTTURA")

                    Me.cmbStato.Enabled = False
                    'Me.cmbContoCorrente.Enabled = False
                    Me.btnINDIETRO.Enabled = False
                    txtindietro.Text = 1
                End If

                SettaggioCampi()
                If par.IfNull(Session.Item("BP_OP_RIELABORA_CDP"), "0") = "0" Then
                    HiddenFieldMostraRielPag.Value = "0"
                    btnRielaboraPagamento.Visible = False
                Else
                    HiddenFieldMostraRielPag.Value = "1"
                    btnRielaboraPagamento.Visible = True
                End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                cmbfornitore.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

                'Me.txtDataMandato.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);document.getElementById('USCITA').value='1';")

                Me.txtNettoP.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtNettoP.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtNettoP.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")


                Me.txtImponibileP.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtImponibileP.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtImponibileP.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")


                Me.txtNettoC.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtNettoC.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtNettoC.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtImponibileC.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtImponibileC.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtImponibileC.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtPenaleC.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtPenaleC.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtPenaleC.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")


                'Me.HLink_ElencoPag.Attributes.Add("onClick", "javascript:window.open('Tab_ElencoPagamenti.aspx?V=" & UCase(Request.QueryString("V")) & "&TIPO_RICERCA=3&IDSEL=" & Me.txtTipoFiltroSelect.Value & "&ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) & "','Elenco','height=550,width=800');")

                Me.HLink_Prenotato.Attributes.Add("onClick", "javascript:window.open('Tab_ElencoPagamenti.aspx?V=" & idVoce & "&TIPO_RICERCA=1&IDSEL=" & Me.txtTipoFiltroSelect.Value & "&ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) & "','Elenco','height=600,width=800');")
                Me.HLink_Consuntivo.Attributes.Add("onClick", "javascript:window.open('Tab_ElencoPagamenti.aspx?V=" & idVoce & "&TIPO_RICERCA=2&IDSEL=" & Me.txtTipoFiltroSelect.Value & "&ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) & "','Elenco','height=600,width=800');")
                'Me.HLink_Pagato.Attributes.Add("onClick", "javascript:window.open('Tab_ElencoPagamenti.aspx?V=" & idVoce & "&TIPO_RICERCA=3&IDSEL=" & Me.txtTipoFiltroSelect.Value & "&ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) & "','Elenco','height=550,width=800');")


                Me.HLink_ElencoMandati.Attributes.Add("onClick", "javascript:window.open('Tab_ElencoMandati.aspx?ID_PAGAMENTO=" & vIdPagamenti & "','Elenco','height=600,width=800');")


                'CASSA - IVA - RITENUTA (PRENOTATO)
                Me.txtCass_PRE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCass_PRE.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtCass_PRE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtRivalsa_PRE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtRivalsa_PRE.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtRivalsa_PRE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtIMPScaricoAzienda.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtIMPScaricoAzienda.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtIMPScaricoAzienda.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtIMPScaricoPercipiente.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtIMPScaricoPercipiente.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtIMPScaricoPercipiente.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtIVA_PRE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtIVA_PRE.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtIVA_PRE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtRitenuta_PRE.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtRitenuta_PRE.Attributes.Add("onChange", "javascript:CalcolaLordo(1);")
                Me.txtRitenuta_PRE.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                'CASSA - IVA - RITENUTA (CONSUNTIVATO)
                Me.txtCass_CONS.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtCass_CONS.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtCass_CONS.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtRivalsa_CONS.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtRivalsa_CONS.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtRivalsa_CONS.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtIMPScaricoAziendaC.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtIMPScaricoAziendaC.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtIMPScaricoAziendaC.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtIMPScaricoPercipienteC.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtIMPScaricoPercipienteC.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtIMPScaricoPercipienteC.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtPenaleC.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtPenaleC.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtPenaleC.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtIVA_CONS.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtIVA_CONS.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtIVA_CONS.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                Me.txtRitenuta_CONS.Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                Me.txtRitenuta_CONS.Attributes.Add("onChange", "javascript:CalcolaLordo(2);")
                Me.txtRitenuta_CONS.Attributes.Add("onkeypress", "javascript:$onkeydown(event);")


                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")


                If par.IfEmpty(Me.txtSTATO.Value, 0) = 0 Then
                    Select Case Me.txtStatoPF.Value
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                If Me.txtFlagVOCI.Value = 0 Then
                                    'Se 1 allora le voci SPECIALI
                                    Me.txtVisualizza.Value = 2 'SOLO LETTURA
                                    FrmSolaLettura()
                                End If
                            End If
                        Case 7
                            If Me.txtFlagVOCI.Value = 0 Then
                                'Se 1 allora le voci SPECIALI
                                Me.txtVisualizza.Value = 2 'SOLO LETTURA
                                FrmSolaLettura()
                            End If
                    End Select
                End If


                'Or Session.Item("BP_GENERALE") = "1"
                If Session.Item("BP_OP_L") = "1" Or IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    Me.txtVisualizza.Value = 2 'SOLO LETTURA
                    FrmSolaLettura()
                End If

                ' SE l'operatore è BP_GENERALE=1 (può vedere tutte le strutture) però se la sua struttura è diversa da quella selezionata allora la maschera è in SOLO LETTURA
                If Session.Item("BP_GENERALE") = "1" And par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) <> Session.Item("ID_STRUTTURA") Then
                    Me.txtVisualizza.Value = 2 'SOLO LETTURA
                    FrmSolaLettura()
                End If

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    Me.txtVisualizza.Value = 3 'SOLO LETTURA DA CHIAMANTE
                    FrmSolaLettura()
                    Me.btnINDIETRO.Enabled = False

                    'CHIUSURA DB
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                    If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                    End If

                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                    HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                    Session.Item("LAVORAZIONE") = "0"
                End If


            Catch ex As Exception
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                Page.Dispose()

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

            End Try
        End If
        HiddenID.Value = vIdODL

        'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.ODL,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                & "WHERE ODL.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                & "AND ODL.ID IN (" & vIdODL & ") AND PF_VOCI.FL_CC=0"
            Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lett.Read Then
                'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                If par.IfNull(Lett(0), 0) = 6 Then
                    'btnAnnulla.enabled = False
                    ANNULLO.Value = "1"
                End If
            End If
            Lett.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Else

            par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.ODL,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                & "WHERE ODL.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                & "AND ODL.ID IN (" & vIdODL & ") AND PF_VOCI.FL_CC=0"
            Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

            If Lett.Read Then
                'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                If par.IfNull(Lett(0), 0) = 6 Then
                    'btnAnnulla.enabled = False
                    ANNULLO.Value = "1"
                End If
            End If
            Lett.Close()

        End If

    End Sub

    Public Property lIdConnessione() As String
        Get
            If Not (ViewState("par_lIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_lIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_lIdConnessione") = value
        End Set

    End Property

    Public Property vIdODL() As Long
        Get
            If Not (ViewState("par_idODL") Is Nothing) Then
                Return CLng(ViewState("par_idODL"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idODL") = value
        End Set

    End Property

    Public Property vIdPrenotazioni() As Long
        Get
            If Not (ViewState("par_idPrenotazioni") Is Nothing) Then
                Return CLng(ViewState("par_idPrenotazioni"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPrenotazioni") = value
        End Set

    End Property


    Public Property vIdPrenotazioniRIT() As Long
        Get
            If Not (ViewState("par_idPrenotazioniRIT") Is Nothing) Then
                Return CLng(ViewState("par_idPrenotazioniRIT"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPrenotazioniRIT") = value
        End Set

    End Property

    Public Property vIdPagamentiRIT() As Long
        Get
            If Not (ViewState("par_idPagamentiRIT") Is Nothing) Then
                Return CLng(ViewState("par_idPagamentiRIT"))
            Else
                Return -1
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPagamentiRIT") = value
        End Set

    End Property

    Public Property vIdPagamenti() As Long
        Get
            If Not (ViewState("par_idPagamenti") Is Nothing) Then
                Return CLng(ViewState("par_idPagamenti"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPagamenti") = value
        End Set

    End Property


    Public Property idVoce() As Long
        Get
            If Not (ViewState("par_idVoce") Is Nothing) Then
                Return CLng(ViewState("par_idVoce"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idVoce") = value
        End Set

    End Property

    Private Sub SettaggioCampi()
        'CARICO COMBO TAB SECODNDARI
        Dim FlagConnessione As Boolean

        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0

        Dim SommaValoreLordo As Decimal = 0
        Dim SommaValoreAssestatoLordo As Decimal = 0
        Dim SommaValoreVariazioni As Decimal = 0

        Dim SommaResiduo As Decimal = 0
        Dim SommaAssesato As Decimal = 0


        Dim SommaPrenotatoControllo As Decimal = 0
        Dim SommaConsuntivatoControllo As Decimal = 0
        Dim SommaLiquidatoControllo As Decimal = 0

        Dim SommaValoreLordoControllo As Decimal = 0
        Dim SommaValoreAssestatoLordoControllo As Decimal = 0
        Dim SommaValoreVariazioniControllo As Decimal = 0

        Dim SommaResiduoControllo As Decimal = 0
        Dim SommaAssesatoControllo As Decimal = 0

        Dim sRisultato As String = ""

        Dim sSelect1 As String = ""

        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            If idVoce = -1 Then
                idVoce = par.IfEmpty(Strings.Trim(Request.QueryString("V")), -1)   'ID_VOCE_PF
            End If


            If IsNumeric(idVoce) AndAlso idVoce > 0 Then
                par.cmd.CommandText = "select substr(inizio,1,4) as anno " _
                    & " from siscom_mi.pf_main, siscom_mi.t_Esercizio_finanziario " _
                    & " where pf_main.id_esercizio_finanziario=t_esercizio_finanziario.id " _
                    & " and pf_main.id =(select id_piano_finanziario from siscom_mi.pf_voci where pf_voci.id=" & idVoce & ")"
                lblEsercizioFinanziario.Text = "Anno B.P. " & par.cmd.ExecuteScalar
            End If


            Dim sFiliale As String = " and ID_STRUTTURA=" & Me.txtID_STRUTTURA.Value
            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            '    sFiliale = "ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            'End If

            'VOCE

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  PF_VOCI.*,PF_MAIN.ID_STATO" _
                                & " from    SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                                & " where   PF_VOCI.ID=" & idVoce _
                                & "   and   PF_VOCI.ID_PIANO_FINANZIARIO=PF_MAIN.ID "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                Me.txtCODICE.Text = par.IfNull(myReader1("CODICE"), "")
                Me.txtDESCRIZIONE.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

                Me.txtStatoPF.Value = Val(par.IfNull(myReader1("ID_STATO"), 5))

                Me.txtFlagVOCI.Value = 0
                'If Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) = "2.04.01" Or Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) = "2.04.04" Or Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) = "3.01.01" Or Strings.Left(par.IfNull(myReader1("CODICE"), ""), 7) = "3.02.01" Then
                If par.IfNull(myReader1("FL_CC"), 0) = 1 Then
                    Me.txtFlagVOCI.Value = 1
                End If

                If Strings.Left(par.IfNull(myReader1("CODICE"), ""), 2) = "1." Or Strings.Left(par.IfNull(myReader1("CODICE"), ""), 2) = "01" Then

                    Me.txtTipoFiltroSelect.Value = "1"
                    sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & idVoce & "))"


                    'SOMMA_LORDO
                    par.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                    myReader2 = par.cmd.ExecuteReader()

                    If myReader2.Read Then
                        sRisultato = par.IfNull(myReader2(0), "0")
                        SommaValoreLordoControllo = Decimal.Parse(sRisultato)
                    End If
                    myReader2.Close()

                    'SOMMA_LORDO_ASSESSTATO
                    par.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                    myReader2 = par.cmd.ExecuteReader()

                    If myReader2.Read Then
                        sRisultato = par.IfNull(myReader2(0), "0")
                        SommaValoreAssestatoLordoControllo = Decimal.Parse(sRisultato)
                    End If
                    myReader2.Close()

                    'VARIAZIONI
                    par.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                    myReader2 = par.cmd.ExecuteReader()

                    If myReader2.Read Then
                        sRisultato = par.IfNull(myReader2(0), "0")
                        SommaValoreVariazioniControllo = Decimal.Parse(sRisultato)
                    End If
                    myReader2.Close()

                    SommaAssesatoControllo = SommaValoreLordoControllo + SommaValoreAssestatoLordoControllo + SommaValoreVariazioniControllo
                    '*******************************
                End If


                sSelect1 = "=" & idVoce

                'SOMMA_LORDO
                par.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                myReader2 = par.cmd.ExecuteReader()

                If myReader2.Read Then
                    sRisultato = par.IfNull(myReader2(0), "0")
                    SommaValoreLordo = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()


                'SOMMA_LORDO_ASSESSTATO
                par.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                myReader2 = par.cmd.ExecuteReader()

                If myReader2.Read Then
                    sRisultato = par.IfNull(myReader2(0), "0")
                    SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()


                'VARIAZIONI
                par.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE" & sSelect1 & sFiliale
                myReader2 = par.cmd.ExecuteReader()

                If myReader2.Read Then
                    sRisultato = par.IfNull(myReader2(0), "0")
                    SommaValoreVariazioni = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()

                SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni
                '*******************************

                'SommaResiduo = par.IfNull(myReader1("VALORE_LORDO"), 0)
                'SommaAssesato = SommaResiduo + par.IfNull(myReader1("ASSESTAMENTO_VALORE_LORDO"), 0)
                'Me.txtIVA.Text = IsNumFormat(par.IfNull(myReader1("IVA"), 0), "", "##,##0.00")
            End If
            myReader1.Close()


            sSelect1 = "=" & idVoce

            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE] +  [EMESSO ]
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=0 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                sRisultato = par.IfNull(myReader1(0), "0")
                SommaPrenotato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()
            '******************************************


            ''IMPORTO APPROVATO in EMESSO APPROVATO e NON ANCORA STAMPATO il PAGAMENTO
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=1 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                sRisultato = par.IfNull(myReader1(0), "0")
                SommaConsuntivato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()
            '********************************************


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si può prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            par.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_VOCE_PF" & sSelect1 _
                                & "   and  ID_STATO>=1 " & sFiliale _
                                & "   and  ID_PAGAMENTO is not null "


            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                sRisultato = par.IfNull(myReader1(0), "0")
                SommaConsuntivato = SommaConsuntivato + Decimal.Parse(sRisultato)
            End If
            myReader1.Close()


            'IMPORTO  LIQUIDATO (per il momento si prende da PAGAMENTI, ma è da AGGIUSTARE)
            'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
            '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF=" & idVoce _
            '                                            & "   and  ID_STATO>1 " & sFiliale & ")"

            'myReader1 = par.cmd.ExecuteReader
            'While myReader1.Read
            '    Select Case par.IfNull(myReader1("ID_STATO"), 0)
            '        'Case 1  'EMESSO
            '        '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
            '        Case 5  'LIQUIDATO
            '            SommaLiquidato = SommaLiquidato + par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
            '    End Select
            'End While
            'myReader1.Close()


            'COMMENTATO 
            'SOLO CONSUNTIVATI anche se sono stata PAGATI (per il momento fin quando non ci dicono come fare, 
            ' perchè più prenotazioni  con voci doverse possono avere un unico pagamento
            'par.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
            '                   & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
            '                   & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF" & sSelect1 _
            '                                            & "   and  ID_STATO>1 " & sFiliale & ")"

            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then
            '    sRisultato = par.IfNull(myReader1(0), "0")
            '    SommaLiquidato = Decimal.Parse(sRisultato)
            'End If
            'myReader1.Close()

            'SommaConsuntivato = SommaConsuntivato - SommaLiquidato
            '*********** FINE COMMENTO


            'SommaResiduo = SommaAssesato - (SommaPrenotato + SommaConsuntivato + SommaLiquidato)
            SommaResiduo = Fix(SommaAssesato * 100) / 100.0 - (Fix(SommaPrenotato * 100) / 100.0 + Fix(SommaConsuntivato * 100) / 100.0 + Fix(SommaLiquidato * 100) / 100.0)

            Me.txtImporto.Text = IsNumFormat(Fix(SommaValoreLordo * 100) / 100.0, "", "##,##0.00")     'Budget o consistenza inizale
            Me.txtImporto1.Text = IsNumFormat(Fix(SommaAssesato * 100) / 100.0, "", "##,##0.00")       'Budget assestato o consistenza assestante

            Me.txtImporto2.Text = IsNumFormat(Fix(SommaPrenotato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto3.Text = IsNumFormat(Fix(SommaConsuntivato * 100) / 100.0, "", "##,##0.00")
            'Me.txtImporto4.Text = IsNumFormat(Fix(SommaLiquidato * 100) / 100.0, "", "##,##0.00")

            Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")        'Disponibilità residua



            If Me.txtTipoFiltroSelect.Value = "1" Then

                sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & idVoce & "))"

                'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                    & " from    SISCOM_MI.PRENOTAZIONI " _
                                    & " where   ID_STATO=0 " _
                                    & "   and   ID_PAGAMENTO is null " _
                                    & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    sRisultato = par.IfNull(myReader1(0), "0")
                    SommaPrenotatoControllo = Decimal.Parse(sRisultato)
                End If
                myReader1.Close()
                '******************************************


                'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                    & " from    SISCOM_MI.PRENOTAZIONI " _
                                    & " where   ID_STATO=1 " _
                                    & "   and   ID_PAGAMENTO is null " _
                                    & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    sRisultato = par.IfNull(myReader1(0), "0")
                    SommaConsuntivatoControllo = Decimal.Parse(sRisultato)
                End If
                myReader1.Close()
                '********************************************


                'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                par.cmd.CommandText = "select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                    & " from   SISCOM_MI.PRENOTAZIONI " _
                                    & " where  ID_VOCE_PF" & sSelect1 _
                                    & "   and  ID_STATO>=1 " & sFiliale _
                                    & "   and  ID_PAGAMENTO is not null "

                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    sRisultato = par.IfNull(myReader1(0), "0")
                    SommaConsuntivatoControllo = SommaConsuntivatoControllo + Decimal.Parse(sRisultato)
                End If
                myReader1.Close()


                'COMMENTATO 
                'SOLO CONSUNTIVATI anche se sono stata PAGATI (per il momento fin quando non ci dicono come fare, 
                ' perchè più prenotazioni  con voci doverse possono avere un unico pagamento
                'par.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
                '                   & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                '                   & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
                '                                            & " where  ID_VOCE_PF" & sSelect1 _
                '                                            & "   and  ID_STATO>1 " & sFiliale & ")"

                'myReader1 = par.cmd.ExecuteReader
                'If myReader1.Read Then
                '    sRisultato = par.IfNull(myReader1(0), "0")
                '    SommaLiquidatoControllo = Decimal.Parse(sRisultato)
                'End If
                'myReader1.Close()

                'SommaConsuntivatoControllo = SommaConsuntivatoControllo - SommaLiquidatoControllo

                SommaResiduoControllo = SommaAssesatoControllo - (SommaPrenotatoControllo + SommaConsuntivatoControllo + SommaLiquidatoControllo)

                Me.txtResiduoControllo.Value = IsNumFormat(SommaResiduoControllo, "", "##,##0.00")
            Else
                Me.txtResiduoControllo.Value = Me.txtImporto5.Text
            End If


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Private Sub CaricaFornitori()
        'CARICO COMBO FORNITORI
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Me.cmbfornitore.Items.Clear()
            ' Me.cmbfornitore.Items.Add(New ListItem(" ", -1))

            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(COD_FORNITORE) as COD_FORNITORE, " _
                                & "(case when ragione_sociale is null then (case when cod_fornitore is null then cognome || ' ' || nome else cod_fornitore ||'-'|| cognome || ' ' || nome end) " _
                                & "else (case when cod_fornitore is null then ragione_sociale else cod_fornitore || '-' || ragione_sociale  end) end) as descrizione " _
                                & " from SISCOM_MI.FORNITORI where FL_BLOCCATO=0 order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
            '        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
            '            Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '        Else
            '            Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
            '        End If
            '    Else
            '        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
            '            Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            '        Else
            '            Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
            '        End If
            '    End If
            'End While
            'myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbfornitore, "ID", "DESCRIZIONE", True)

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub
    '11/05/2015 MODIFICA PER RECUPERARE DATA SCADENZA DALLE TABELLE DI SUPPORTO
    Private Function CalcolaDataScadenza(ByVal TipoModalita As String, ByVal tipoPagamento As String, ByVal DataScadPagamento As String) As String
        CalcolaDataScadenza = ""
        TipoModalita = TipoModalita.ToUpper.Replace("NULL", "")
        tipoPagamento = tipoPagamento.ToUpper.Replace("NULL", "")

        If String.IsNullOrEmpty(DataScadPagamento) Then
            If Not String.IsNullOrEmpty(TipoModalita) Then
                Dim Table As String = ""
                Dim Column As String = ""
                Dim FlSomma As Integer = 0
                Dim DaySum As Integer = 0
                par.cmd.CommandText = "SELECT tab_rif,fld_rif,fl_somma_giorni FROM siscom_mi.TAB_DATE_MODALITA_PAG WHERE ID = (SELECT id_data_riferimento FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = " & TipoModalita & ")"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    Table = par.IfNull(lettore("tab_rif"), "")
                    Column = par.IfNull(lettore("fld_rif"), "")
                    FlSomma = par.IfNull(lettore("fl_somma_giorni"), "")
                End If
                lettore.Close()

                If Not String.IsNullOrEmpty(Table) And Not String.IsNullOrEmpty(Column) Then
                    par.cmd.CommandText = "select " & Column & " from siscom_Mi." & Table & " where id = " & vIdPagamenti

                    CalcolaDataScadenza = par.IfNull(par.cmd.ExecuteScalar, "")
                End If

                If Not String.IsNullOrEmpty(CalcolaDataScadenza) Then
                    If FlSomma = 1 Then
                        par.cmd.CommandText = "select nvl(num_giorni,0) from siscom_mi.tipo_pagamento where id = " & tipoPagamento
                        DaySum = par.IfNull(par.cmd.ExecuteScalar, 0)

                        If DaySum > 0 Then
                            CalcolaDataScadenza = Date.Parse(par.FormattaData(CalcolaDataScadenza), New System.Globalization.CultureInfo("it-IT", False)).AddDays(DaySum).ToString("dd/MM/yyyy")
                            CalcolaDataScadenza = par.AggiustaData(CalcolaDataScadenza)
                        End If
                    End If
                End If
            End If
        End If

        If String.IsNullOrEmpty(CalcolaDataScadenza) Then
            CalcolaDataScadenza = DataScadPagamento
        End If

    End Function

    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()


        If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
            vIdODL = par.IfNull(myReader1("ID"), -1)
        Else
            par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID=" & vIdODL & " FOR UPDATE NOWAIT"
        End If

        par.cmd.CommandText = "SELECT NVL(DATA_EMISSIONE,0) FROM SISCOM_MI.PAGAMENTI WHERE ID in (SELECT ID_PAGAMENTO FROM SISCOM_MI.ODL WHERE ID=" & vIdODL & ")"
        HiddenFieldDataEmissione.Value = "0"
        Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If lettore.Read Then
            HiddenFieldDataEmissione.Value = par.IfNull(lettore(0), "0")
        End If
        lettore.Close()
        If HiddenFieldDataEmissione.Value = "0" Then
            HiddenFieldDataEmissione.Value = Format(Now, "yyyyMMdd")
        End If

        idVoce = par.IfNull(myReader1("ID_VOCE_PF"), -1)

        Me.txtVisualizza.Value = 1  'SEMPRE

        Me.cmbfornitore.Items.FindItemByValue(par.IfNull(myReader1("ID_FORNITORE"), "-1")).Selected = True
        par.cmd.CommandText = " select CASSA,RIT_ACCONTO,0 AS RIVALSA,(select codice from siscom_mi.tipo_ritenuta where tipo_ritenuta.id=id_tipo_ritenuta) as ritenuta,tipo from SISCOM_MI.FORNITORI where ID=" & cmbfornitore.SelectedValue
        Dim tipo As String = "0"
        Dim myReaderTt As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        myReaderTt = par.cmd.ExecuteReader()

        If myReaderTt.Read Then
            tipo = par.IfNull(myReaderTt("TIPO"), "0")
            HiddenFieldTipoProfessionista.Value = tipo
            'Me.txtPercIVA.Value = 0 'par.IfNull(myReaderT("IVA"), 0)
            'Me.txtPercCassa.Value = par.IfNull(myReaderT("CASSA"), 0)
            'Me.txtPercRivalsa.Value = par.IfNull(myReaderT("RIVALSA"), 0)
            'Me.txtPercRitenuta.Value = par.IfNull(myReaderT("RIT_ACCONTO"), 0)
            Me.codRitenuta.Value = par.IfNull(myReaderTt("RITENUTA"), "301")
            'Me.lblIVAP.Text = "IVA (" & Me.txtPercIVA.Value & "%)"
            'Me.lblCassaP.Text = "Cassa (" & Me.txtPercCassa.Value & "%)"
            'Me.lblRitenutaP.Text = "Rit. d'acconto (" & Me.txtPercRitenuta.Value & "%)"
            'Me.txtIVA_PRE.Text = Me.txtPercIVA.Value
            'Me.txtCass_PRE.Text = Me.txtPercCassa.Value
            'Me.txtRivalsa_PRE.Text = Me.txtPercRivalsa.Value
            'Me.txtRitenuta_PRE.Text = Me.txtPercRitenuta.Value
            'Me.lblIVAC.Text = "IVA (" & Me.txtPercIVA.Value & "%)"
            'Me.lblCassaC.Text = "Cassa (" & Me.txtPercCassa.Value & "%)"
            'Me.lblRitenutaC.Text = "Rit. d'acconto (" & Me.txtPercRitenuta.Value & "%)"
        End If
        myReaderTt.Close()
        par.cmd.CommandText = "SELECT CODICE FROM SISCOM_MI.TIPO_RITENUTA WHERE ID=(select id_tipo_ritenuta from siscom_mi.fornitori where fornitori.id=" & par.IfNull(myReader1("ID_FORNITORE"), "-1") & ")"
        codRitenuta.Value = par.cmd.ExecuteScalar

        Me.txtDescrizioneP.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

        Me.txtID_STRUTTURA.Value = par.IfNull(myReader1("ID_STRUTTURA"), -1)

        'PERCENTUALI del FORNITORE
        Me.txtPercIVA.Value = par.IfNull(myReader1("IVA"), 0)
        Me.txtPercCassa.Value = par.IfNull(myReader1("CASSA"), 0)
        Me.txtPercRivalsa.Value = par.IfNull(myReader1("RIVALSA"), 0)
        Me.txtPercRitenuta.Value = par.IfNull(myReader1("RIT_ACCONTO"), 0)
        Me.txtAzienda.Value = par.IfNull(myReader1("AZIENDA"), 0)
        Me.txtPercipiente.Value = par.IfNull(myReader1("PERCIPIENTE"), 0)


        Me.txtPercIVA_C.Value = par.IfNull(myReader1("IVA_CONS"), 0)
        Me.txtPercCassa_C.Value = par.IfNull(myReader1("CASSA_CONS"), 0)
        Me.txtPercRivalsa_C.Value = par.IfNull(myReader1("RIVALSA_CONS"), 0)
        Me.txtPercRitenuta_C.Value = par.IfNull(myReader1("RIT_ACCONTO_CONS"), 0)
        Me.txtAzienda_C.Value = par.IfNull(myReader1("AZIENDA_CONS"), 0)
        Me.txtPercipiente_C.Value = par.IfNull(myReader1("PERCIPIENTE_CONS"), 0)
        Me.codRitenuta.Value = par.IfNull(myReader1("COD_RITENUTA"), 0)


        Dim idTipoPagamento As String = "null"
        Dim idModPagamento As String = "null"
        par.cmd.CommandText = "SELECT FORNITORI.*, (select id from siscom_mi.fornitori_iban where id_fornitore = fornitori.id and fl_attivo = 1) as id_iban " _
                           & "FROM SISCOM_MI.FORNITORI WHERE id = " & Me.cmbfornitore.SelectedValue
        myReaderT = par.cmd.ExecuteReader
        If myReaderT.Read Then
            idModPagamento = par.IfNull(myReaderT("ID_TIPO_MODALITA_PAG"), -1)
            idTipoPagamento = par.IfNull(myReaderT("ID_TIPO_PAGAMENTO"), -1)
        End If
        myReaderT.Close()
        Me.txtDScad.Text = par.FormattaData(CalcolaDataScadenza(idModPagamento, idTipoPagamento, par.IfNull(myReader1("DATA_SCAD_PAGAMENTO"), Format(Now, "yyyyMMdd"))))

        'Me.txtDScad.Text = par.FormattaData(par.IfNull(myReader1("DATA_SCAD_PAGAMENTO"), Format(Now, "yyyyMMdd")))


        'Me.lblIVAP.Text = "IVA (" & Me.txtPercIVA.Value & "%)"
        'Me.lblCassaP.Text = "Cassa (" & Me.txtPercCassa.Value & "%)"
        'Me.lblRitenutaP.Text = "Rit. d'acconto (" & Me.txtPercRitenuta.Value & "%)"

        Me.txtIVA_PRE.Text = IsNumFormat(par.IfEmpty(Me.txtPercIVA.Value, 0), "", "##,##0.00")
        Me.txtCass_PRE.Text = IsNumFormat(par.IfEmpty(Me.txtPercCassa.Value, 0), "", "##,##0.00")
        Me.txtRivalsa_PRE.Text = IsNumFormat(par.IfEmpty(Me.txtPercRivalsa.Value, 0), "", "##,##0.00")
        Me.txtRitenuta_PRE.Text = IsNumFormat(par.IfEmpty(Me.txtPercRitenuta.Value, 0), "", "##,##0.00")
        Me.txtIMPScaricoAzienda.Text = IsNumFormat(par.IfEmpty(Me.txtAzienda.Value, 0), "", "##,##0.00")
        Me.txtIMPScaricoPercipiente.Text = IsNumFormat(par.IfEmpty(Me.txtPercipiente.Value, 0), "", "##,##0.00")

        'Me.lblIVAC.Text = "IVA (" & Me.txtPercIVA.Value & "%)"
        'Me.lblCassaC.Text = "Cassa (" & Me.txtPercCassa.Value & "%)"
        'Me.lblRitenutaC.Text = "Rit. d'acconto (" & Me.txtPercRitenuta.Value & "%)"

        Me.txtIVA_CONS.Text = IsNumFormat(par.IfEmpty(Me.txtPercIVA_C.Value, 0), "", "##,##0.00")
        Me.txtCass_CONS.Text = IsNumFormat(par.IfEmpty(Me.txtPercCassa_C.Value, 0), "", "##,##0.00")
        Me.txtRivalsa_CONS.Text = IsNumFormat(par.IfEmpty(Me.txtPercRivalsa_C.Value, 0), "", "##,##0.00")
        Me.txtRitenuta_CONS.Text = IsNumFormat(par.IfEmpty(Me.txtPercRitenuta_C.Value, 0), "", "##,##0.00")
        Me.txtIMPScaricoAziendaC.Text = IsNumFormat(par.IfEmpty(Me.txtAzienda_C.Value, 0), "", "##,##0.00")
        Me.txtIMPScaricoPercipienteC.Text = IsNumFormat(par.IfEmpty(Me.txtPercipiente_C.Value, 0), "", "##,##0.00")

        Me.txtPenaleC.Text = par.IfNull(myReader1("PENALE"), 0)
        Me.txtPenaleC.Text = IsNumFormat(Me.txtPenaleC.Text, "", "##,##0.00")

        lblODL1.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
        lblData.Text = par.FormattaData(par.IfNull(myReader1("DATA_ORDINE"), ""))

        Me.txtSTATO.Value = par.IfNull(myReader1("ID_STATO"), "0") '0=BOZZA, 1=EMESSO 2=CONSUNTIVATO 3=INTEGRATO 4=EMESSO PAGAMENTO (ex LIQUIDATO) 5=ANNULLATO

        vIdPrenotazioni = par.IfNull(myReader1("ID_PRENOTAZIONE"), 0)
        vIdPrenotazioniRIT = par.IfNull(myReader1("ID_PRENOTAZIONE_RIT"), 0)

        'RICAVO eventuale ID_PAGAMENTO di  PRENOTAZIONI x RITENUTA ACCONTO
        par.cmd.CommandText = " select ID_PAGAMENTO from SISCOM_MI.PRENOTAZIONI where ID=" & vIdPrenotazioniRIT
        myReaderT = par.cmd.ExecuteReader()

        If myReaderT.Read Then
            vIdPagamentiRIT = par.IfNull(myReaderT(0), 0)
        End If
        myReaderT.Close()
        '**********************************************


        vIdPagamenti = par.IfNull(myReader1("ID_PAGAMENTO"), 0)

        Me.txtStatoPagamento.Value = txtSTATO.Value

        If vIdPrenotazioni <> 0 Then
            par.cmd.CommandText = " select DATA_STAMPA from SISCOM_MI.PRENOTAZIONI where ID=" & vIdPrenotazioni
            myReaderT = par.cmd.ExecuteReader()
            If myReaderT.Read Then
                If par.IfNull(myReaderT("DATA_STAMPA"), "") <> "" Then
                    'Me.txtStatoPagamento.Value = 1 'EMESSO e STAMPATO
                End If
            End If
            myReaderT.Close()
        End If
        '**************************************

        If vIdPagamenti <> 0 Then

            'LETTURA CAMPI PAGAMENTO
            par.cmd.CommandText = " select DATA_STAMPA,CONTO_CORRENTE,data_scadenza from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamenti
            myReaderT = par.cmd.ExecuteReader()
            If myReaderT.Read Then

                'Me.txtDataMandato.Text = par.FormattaData(par.IfNull(myReaderT("DATA_MANDATO"), ""))
                'Me.txtNumMandato.Text = par.IfNull(myReaderT("NUMERO_MANDATO"), "")
                Me.txtDScad.Text = par.FormattaData(par.IfNull(myReaderT("data_scadenza"), ""))
                'Me.cmbContoCorrente.Enabled = False
                'Me.cmbContoCorrente.SelectedValue = par.IfNull(myReaderT("CONTO_CORRENTE"), "-1")


                If par.IfNull(myReaderT("DATA_STAMPA"), "") <> "" Then
                    Me.txtStatoPagamento.Value = 4 'PAGATO e STAMPATO EMESSO PAGAMENTO
                    Me.txtVisualizza.Value = 2    'SOLO LETTURA
                Else
                    Me.txtStatoPagamento.Value = 3 'PAGATO NON STAMPATO
                End If

                'If par.IfNull(myReaderT("DATA_MANDATO"), "") <> "" Then
                '    txtSTATO.Value = 5 'LIGUIDATO
                '    Me.txtVisualizza.Value = 2    'SOLO LETTURA
                'End If

                If par.IfNull(myReader1("ID_STATO"), "0") <> 5 And Val(txtSTATO.Value) = 5 Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.ODL set ID_STATO=5  where ID=" & vIdODL

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                End If

            End If
            myReaderT.Close()
        End If

        Me.txtNettoC.Text = IsNumFormat(par.IfNull(myReader1("CONS_NETTO"), 0), "", "##,##0.00")
        Me.txtImponibileC.Text = IsNumFormat(par.IfNull(myReader1("CONS_NO_IVA"), 0), "", "##,##0.00")

        'SERVONO PER IL LOG EVENTI
        Me.txtNettoC_ODL.Value = IsNumFormat(par.IfNull(myReader1("CONS_NETTO"), 0), "", "##,##0.00")
        Me.txtImponibileC_ODL.Value = IsNumFormat(par.IfNull(myReader1("CONS_NO_IVA"), 0), "", "##,##0.00")
        '*************************

        CalcolaImporti(par.IfEmpty(Me.txtNettoC.Text, 0), par.IfEmpty(Me.txtImponibileC.Text, 0), par.IfEmpty(Me.txtPercIVA_C.Value, 0), par.IfEmpty(Me.txtPercCassa_C.Value, 0), par.IfEmpty(Me.txtPercRitenuta_C.Value, 0), par.IfEmpty(Me.txtPenaleC.Text, 0), "CONSUNTIVO", par.IfEmpty(Me.txtPercRivalsa_C.Value, 0), par.IfEmpty(Me.txtPercipiente_C.Value, 0), par.IfEmpty(Me.txtAzienda_C.Value, 0), codRitenuta.Value)
        '**************************************

        Me.txtNettoP.Text = IsNumFormat(par.IfNull(myReader1("PREN_NETTO"), 0), "", "##,##0.00")
        Me.txtImponibileP.Text = IsNumFormat(par.IfNull(myReader1("PREN_NO_IVA"), 0), "", "##,##0.00")

        'SERVONO PER IL LOG EVENTI
        Me.txtNettoP_ODL.Value = IsNumFormat(par.IfNull(myReader1("PREN_NETTO"), 0), "", "##,##0.00")
        Me.txtImponibileP_ODL.Value = IsNumFormat(par.IfNull(myReader1("PREN_NO_IVA"), 0), "", "##,##0.00")
        '*************************

        CalcolaImporti(par.IfEmpty(Me.txtNettoP.Text, 0), par.IfEmpty(Me.txtImponibileP.Text, 0), par.IfEmpty(Me.txtPercIVA.Value, 0), par.IfEmpty(Me.txtPercCassa.Value, 0), par.IfEmpty(Me.txtPercRitenuta.Value, 0), 0, "PRENOTATO", par.IfEmpty(Me.txtPercRivalsa.Value, 0), par.IfEmpty(Me.txtPercipiente.Value, 0), par.IfEmpty(Me.txtAzienda.Value, 0), codRitenuta.Value)


        'IMPORTO PAGATO
        Dim SommaPagato As Decimal = 0
        par.cmd.CommandText = " select  sum(nvl(PAGAMENTI_LIQUIDATI.IMPORTO,0) ) as IMPORTO " _
                                       & " from  SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                       & " where ID_PAGAMENTO=" & vIdPagamenti

        myReaderT = par.cmd.ExecuteReader
        If myReaderT.Read Then
            SommaPagato = par.IfNull(myReaderT(0), 0)
        End If
        myReaderT.Close()
        If SommaPagato = 0 Then
            Me.cmb_Liquidazione.SelectedValue = 0
        ElseIf SommaPagato = strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", "")) Then
            Me.cmb_Liquidazione.SelectedValue = 3
        Else
            Me.cmb_Liquidazione.SelectedValue = 2
        End If
        '*****************************************************


        Setta_StataoODL(Me.txtSTATO.Value)

        Me.cmbStato.SelectedValue = Me.txtSTATO.Value

        If Me.txtStatoPagamento.Value >= 4 Then 'PAGATO e STAMPATO
            FrmSolaLettura()
            'ElseIf Me.txtStatoPagamento.Value > 2 Then
            '    Me.cmbStato.Enabled = False
            '    Me.cmbfornitore.Enabled = False
        End If

        If Me.txtSTATO.Value = 5 Then
            txtStatoPagamento.Value = 5
            txtVisualizza.Value = 20
            cmbStato.Enabled = False
            cmbfornitore.Enabled = False
        End If

    End Sub

    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try
            sValoreStato = Request.QueryString("ST") '0=PRENOTATO (1=EMESSO e 5=LIQUIDATO da PAGAMENTI)

            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdODL <> 0 Then
                ' LEGGO ODL

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID_PAGAMENTO=" & vIdODL
                Else
                    par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID=" & vIdODL & " FOR UPDATE NOWAIT"
                End If

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                If sValoreProvenienza <> "CHIAMATA_DIRETTA" Then
                    'CREO LA TRANSAZIONE
                    par.myTrans = par.OracleConn.BeginTransaction()
                    '‘par.cmd.Transaction = par.myTrans
                    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                End If

                Session.Add("LAVORAZIONE", "1")
            End If


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                'scriptblock = "<script language='javascript' type='text/javascript'>" _
                '& "alert('Scheda Pagamenti aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                '& "</script>"
                'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                'End If
                RadWindowManager1.RadAlert("Scheda Pagamenti aperto da un altro utente. Non è possibile effettuare modifiche!", 300, 150, "Attenzione", "", "null")
                ' LEGGO IL RECORD IN SOLO LETTURA

                par.cmd.CommandText = "select * from SISCOM_MI.ODL where ID=" & vIdODL
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                Me.txtVisualizza.Value = 2 'SOLO LETTURA
                FrmSolaLettura()

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
 Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdODL = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If
    End Sub

    Public Function ControlloCampi() As Boolean

        ControlloCampi = True


        If par.IfEmpty(Me.txtNettoP.Text, 0) = 0 Then

            RadWindowManager1.RadAlert("L\'mporto prenotato netto non può essere ugale a zero!", 300, 150, "Attenzione", "", "null")
            '  Response.Write("<script>alert('L\'mporto prenotato netto non può essere ugale a zero!');</script>")
            ControlloCampi = False
            Exit Function
        End If

        If Me.cmbfornitore.SelectedValue = -1 Then
            RadWindowManager1.RadAlert("Selezionare un Fornitore!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If par.IfEmpty(Me.txtDescrizioneP.Text, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire la Descrizione!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        Select Case Me.cmbStato.SelectedValue
            Case 0 'BOZZA
                If Val(Me.txtSTATO.Value) <> 0 Then
                    RadWindowManager1.RadAlert("Ordine emesso, impossibile ritornare a bozza!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If

            Case 1

                If Val(txtSTATO.Value) = 2 Then
                    RadWindowManager1.RadAlert("In fase di consuntivo, impossibile ritornare a ordine emesso!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If

                If par.IfEmpty(Me.txtNettoP.Text, 0) = 0 Then
                    RadWindowManager1.RadAlert("Inserire l\'importo prenotato!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If

            Case 2

                'If Me.cmbContoCorrente.SelectedValue = "" Then
                '    Me.cmbContoCorrente.SelectedValue = "-1"
                'End If

                CalcolaImporti(par.IfEmpty(Me.txtNettoC.Text, 0), par.IfEmpty(Me.txtImponibileC.Text, 0), par.IfEmpty(Me.txtPercIVA_C.Value, 0), par.IfEmpty(Me.txtPercCassa_C.Value, 0), par.IfEmpty(Me.txtPercRitenuta_C.Value, 0), par.IfEmpty(Me.txtPenaleC.Text, 0), "CONSUNTIVO", par.IfEmpty(Me.txtPercRivalsa_C.Value, 0), par.IfEmpty(Me.txtPercipiente_C.Value, 0), par.IfEmpty(Me.txtAzienda_C.Value, 0), codRitenuta.Value)
                If par.IfEmpty(Me.txtNettoC.Text, 0) = 0 Then
                    RadWindowManager1.RadAlert("Inserire l\'importo consuntivato!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If

                If par.IfEmpty(Me.txtLordoC.Text, 0) = 0 Then
                    RadWindowManager1.RadAlert("Inserire o ridigitare l\'importo consuntivato!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If
            Case 3
                'If par.IfEmpty(Me.txtNumMandato.Text, "Null") <> "Null" Then
                '    If par.IfEmpty(Me.txtDataMandato.Text, "Null") = "Null" Then
                '        Response.Write("<script>alert('Inserire la Data di Mandato Pagamento!');</script>")
                '        ControlloCampi = False
                '        Exit Function
                '    End If
                'ElseIf par.IfEmpty(Me.txtDataMandato.Text, "Null") <> "Null" Then
                '    If par.IfEmpty(Me.txtNumMandato.Text, "Null") = "Null" Then
                '        Response.Write("<script>alert('Inserire la Numero di Mandato Pagamento!');</script>")
                '        ControlloCampi = False
                '        Exit Function
                '    End If
                'End If

        End Select




    End Function


    Private Sub Salva()
        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0

        Dim sRisultato As String = ""
        Dim sSelect1 As String = ""

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            Dim sFiliale As String = " and ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, "-1")
            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            '    sFiliale = "  ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            'End If

            If idVoce = -1 Then
                idVoce = par.IfEmpty(Strings.Trim(Request.QueryString("V")), -1)   'ID_VOCE_PF
            End If

            If Me.txtTipoFiltroSelect.Value = 1 Then
                sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & idVoce & "))"
            Else
                sSelect1 = "=" & idVoce
            End If
            sSelect1 = "=" & idVoce


            '***********1° SALVATAGGIO
            '1) INSERT PRENOTAZIONI
            '2) INSEER PRENOTAZIONE X RITENUTA ACCONTO (5) SE txtRitenutaP o txtRitenutaC GestioneRITENUTA_ACCONTO
            '3) INSERT ODL


            'MODIFICA MARCO 31/01/2012
            '.........................................................................
            '' '' Ricavo vIdPrenotazioni
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = " select SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM dual "
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader1.Read Then
            '    vIdPrenotazioni = myReader1(0)
            'End If
            'myReader1.Close()
            'par.cmd.CommandText = ""


            '' PRENOTAZIONI
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "insert into SISCOM_MI.PRENOTAZIONI " _
            '                            & " (ID,DESCRIZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_VOCE_PF,TIPO_PAGAMENTO,DATA_PRENOTAZIONE,ID_STATO,ID_STRUTTURA) " _
            '                    & "values (:id,:descrizione,:importo,:id_fornitore,:id_voce,:tipo_pagamento,:data_prenotazione,:id_stato,:id_struttura)"

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPrenotazioni))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtPrenotatoLordoRIT.Value.Replace(".", ""))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 4))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_prenotazione", Format(Now, "yyyyMMdd")))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 0))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Me.txtID_STRUTTURA.Value)))

            ''par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imponibile", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))

            'par.cmd.ExecuteNonQuery()
            'par.cmd.CommandText = ""
            'par.cmd.Parameters.Clear()

            '**********************

            'GestioneRITENUTA_ACCONTO()

            ' ODL (Tabella Principa:->2 Prenotazioni    (ALTRI Pagamenti + Ritenuta acconto)
            '                       ->2 Pagamenti       (ALTRI Pagamenti + Ritenuta Acconto)

            'CREATE TABLE ODL
            '(
            '  ID_PAGAMENTO      NUMBER,
            '  CONS_NETTO        NUMBER,
            '  CONS_CASSA        NUMBER,
            '  CONS_IVA          NUMBER,
            '  CONS_RIT_ACCONTO  NUMBER,
            '  CONS_NO_IVA       NUMBER,
            '  CONS_LORDO        NUMBER

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            '...............................................................................
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select SISCOM_MI.SEQ_ODL.NEXTVAL FROM dual "
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdODL = myReader1(0)
            End If
            myReader1.Close()
            par.cmd.CommandText = ""

            vIdPrenotazioni = -1
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.ODL " _
                                        & " (ID,DATA_ORDINE,DESCRIZIONE,ID_VOCE_PF,ID_FORNITORE,ID_PRENOTAZIONE,ID_PRENOTAZIONE_RIT," _
                                        & " PREN_NETTO, CASSA, RIVALSA, IVA, RIT_ACCONTO, PREN_NO_IVA, PREN_LORDO,ID_STATO,PENALE,ID_STRUTTURA,CASSA_CONS,RIVALSA_CONS, IVA_CONS, RIT_ACCONTO_CONS, PERCIPIENTE,PERCIPIENTE_CONS,AZIENDA,AZIENDA_CONS,COD_RITENUTA)" _
                                & "values ('" & vIdODL & "'," _
                                & "'" & Format(Now, "yyyyMMdd") & "', " _
                                & "'" & par.PulisciStrSql(par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))) & "', " _
                                & "'" & RitornaNullSeIntegerMenoUno(idVoce) & "', " _
                                & "'" & RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue)) & "', " _
                                & "NULL, " _
                                & "NULL, " _
                                & "'" & strToNumber(Me.txtNettoP.Text.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercCassa.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercRivalsa.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercIVA.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercRitenuta.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtImponibileP.Text.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPrenotatoLordo.Value.Replace(".", "")) & "', " _
                                & "'" & Convert.ToInt32(Me.cmbStato.SelectedValue) & "', " _
                                & "'" & strToNumber(Me.txtPenaleC.Text.Replace(".", "")) & "', " _
                                & "'" & RitornaNullSeIntegerMenoUno(Me.txtID_STRUTTURA.Value) & "', " _
                                & "'" & strToNumber(Me.txtPercCassa_C.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercRivalsa_C.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercIVA_C.Value.Replace(".", "")) & "', " _
                                & "'" & strToNumber(Me.txtPercRitenuta_C.Value.Replace(".", "")) & "'," _
                                & "'" & strToNumber(Me.txtPercipiente.Value.Replace(".", "")) & "'," _
                                & "'" & strToNumber(Me.txtPercipiente_C.Value.Replace(".", "")) & "'," _
                                & "'" & strToNumber(Me.txtAzienda.Value.Replace(".", "")) & "'," _
                                & "'" & strToNumber(Me.txtAzienda_C.Value.Replace(".", "")) & "'," _
                                & "'" & strToNumber(Me.codRitenuta.Value.Replace(".", "")) & "')"




            '',:descrizione,:id_voce,:id_fornitore,'NULL','NULL'," _
            ''                                      & " :pren_netto,:cassa,:iva,:rit_acconto,:pren_no_iva,:pren_lordo,:stato,:penale,:id_struttura,:cassa_c,:iva_c,:rit_acconto_c)"

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdODL))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_ordine", Format(Now, "yyyyMMdd")))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione", vIdPrenotazioni))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione_rit", RitornaNullSeIntegerMenoUno(vIdPrenotazioniRIT)))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_netto", strToNumber(Me.txtNettoP.Text.Replace(".", ""))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa", strToNumber(Me.txtPercCassa.Value.Replace(".", ""))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva", strToNumber(Me.txtPercIVA.Value.Replace(".", ""))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto", strToNumber(Me.txtPercRitenuta.Value.Replace(".", ""))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_no_iva", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_lordo", strToNumber(Me.txtPrenotatoLordo.Value.Replace(".", ""))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(Me.txtPenaleC.Text.Replace(".", ""))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Me.txtID_STRUTTURA.Value)))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa_C", strToNumber(Me.txtPercCassa_C.Value.Replace(".", ""))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_C", strToNumber(Me.txtPercIVA_C.Value.Replace(".", ""))))
            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto_C", strToNumber(Me.txtPercRitenuta_C.Value.Replace(".", ""))))


            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            HiddenID.Value = vIdODL

            ''****Scrittura evento PAGAMENTO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F55','')"
            par.cmd.ExecuteNonQuery()
            '****************************************************

            '****** LETTURA ODL (ex PROGR_FORNITORE)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select PROGR,ANNO from SISCOM_MI.ODL where ID=" & vIdODL
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then

                lblODL1.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
                lblData.Text = DateTime.Now.Date
            End If
            myReader1.Close()
            '*****************************************

            txtSTATO.Value = Me.cmbStato.SelectedValue
            Me.cmbStato.Enabled = True

            ' COMMIT
            par.myTrans.Commit()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            'SERVONO PER IL LOG EVENTI
            Me.txtNettoP_ODL.Value = Me.txtNettoP.Text
            Me.txtImponibileP_ODL.Value = Me.txtImponibileP.Text
            '*************************


            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=0 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                sRisultato = par.IfNull(myReader1(0), "0")
                SommaPrenotato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()
            '************************************************


            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=1 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                sRisultato = par.IfNull(myReader1(0), "0")
                SommaConsuntivato = Decimal.Parse(sRisultato)
            End If
            myReader1.Close()
            '************************************************


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_VOCE_PF" & sSelect1 _
                                & "   and  ID_STATO>=1 " & sFiliale _
                                & "   and  ID_PAGAMENTO is not null "


            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                sRisultato = par.IfNull(myReader1(0), "0")
                SommaConsuntivato = SommaConsuntivato + Decimal.Parse(sRisultato)
            End If
            myReader1.Close()



            'IMPORTO  LIQUIDATO 
            'COMMENTATO 
            'SOLO CONSUNTIVATI anche se sono stata PAGATI (per il momento fin quando non ci dicono come fare, 
            ' perchè più prenotazioni  con voci doverse possono avere un unico pagamento
            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI "
            'par.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
            '                   & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
            '                   & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF" & sSelect1 _
            '                                            & "   and  ID_STATO>1 " & sFiliale & ")"

            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then
            '    sRisultato = par.IfNull(myReader1(0), "0")
            '    SommaLiquidato = Decimal.Parse(sRisultato)
            'End If
            'myReader1.Close()
            '******* FINE COMMENTO

            'myReader1 = par.cmd.ExecuteReader
            'While myReader1.Read
            '    Select Case par.IfNull(myReader1("ID_STATO"), 0)
            '        'Case 1 'EMESSO
            '        '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
            '        Case 5  'LIQUIDATO
            '            SommaLiquidato = SommaLiquidato + par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
            '    End Select

            'End While
            'myReader1.Close()


            SommaResiduo = Me.txtImporto1.Text

            SommaConsuntivato = SommaConsuntivato '- SommaLiquidato
            SommaResiduo = SommaResiduo - (SommaPrenotato + SommaConsuntivato) ' + SommaLiquidato)


            Me.txtImporto2.Text = IsNumFormat(SommaPrenotato, "", "##,##0.00")
            Me.txtImporto3.Text = IsNumFormat(SommaConsuntivato, "", "##,##0.00")
            'Me.txtImporto4.Text = IsNumFormat(SommaLiquidato, "", "##,##0.00")

            Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")



            CalcolaImporti(par.IfEmpty(Me.txtNettoP.Text, 0), par.IfEmpty(Me.txtImponibileP.Text, 0), par.IfEmpty(Me.txtPercIVA.Value, 0), par.IfEmpty(Me.txtPercCassa.Value, 0), par.IfEmpty(Me.txtPercRitenuta.Value, 0), 0, "PRENOTATO", par.IfEmpty(Me.txtPercRivalsa.Value, 0), par.IfEmpty(Me.txtPercipiente.Value, 0), par.IfEmpty(Me.txtAzienda.Value, 0), codRitenuta.Value)

            Me.txtStatoPagamento.Value = 0      'PRIMO SALVATAGGIO
            Me.txtVisualizza.Value = 1          'SEMPRE

            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID = " & vIdODL & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()



            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            RadNotificationNote.Text = "Operazione completata correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()



            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Private Sub Update()
        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0

        Dim risultatoRIT As Decimal = 0
        Dim sRisultato As String = ""
        Dim sSelect1 As String = ""

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader

            idVoce = UCase(Request.QueryString("V"))


            Dim sFiliale As String = " and ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, "-1")
            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            '    sFiliale = "  ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            'End If

            idVoce = UCase(Request.QueryString("V"))

            If Me.txtTipoFiltroSelect.Value = 1 Then
                sSelect1 = " in (select ID from SISCOM_MI.PF_VOCI " _
                                 & " where  ID_VOCE_MADRE in (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & idVoce & "))"
            Else
                sSelect1 = "=" & idVoce
            End If
            sSelect1 = "=" & idVoce

            par.cmd.Parameters.Clear()

            CalcolaImporti(par.IfEmpty(Me.txtNettoP.Text, 0), par.IfEmpty(Me.txtImponibileP.Text, 0), par.IfEmpty(Me.txtPercIVA.Value, 0), par.IfEmpty(Me.txtPercCassa.Value, 0), par.IfEmpty(Me.txtPercRitenuta.Value, 0), 0, "PRENOTATO", par.IfEmpty(Me.txtPercRivalsa.Value, 0), par.IfEmpty(Me.txtPercipiente.Value, 0), par.IfEmpty(Me.txtAzienda.Value, 0), codRitenuta.Value)
            CalcolaImporti(par.IfEmpty(Me.txtNettoC.Text, 0), par.IfEmpty(Me.txtImponibileC.Text, 0), par.IfEmpty(Me.txtPercIVA_C.Value, 0), par.IfEmpty(Me.txtPercCassa_C.Value, 0), par.IfEmpty(Me.txtPercRitenuta_C.Value, 0), par.IfEmpty(Me.txtPenaleC.Text, 0), "CONSUNTIVO", par.IfEmpty(Me.txtPercRivalsa_C.Value, 0), par.IfEmpty(Me.txtPercipiente_C.Value, 0), par.IfEmpty(Me.txtAzienda_C.Value, 0), codRitenuta.Value)

            par.cmd.Parameters.Clear()

            Select Case cmbStato.SelectedValue
                Case 1 'EMESSO


                    'MODIFICA MARCO 31/01/2012
                    '.............................................................................................
                    'QUANDO LO STATO è EMESSO INSERIMENTO DELLA PRENOTAZIONE


                    If vIdPrenotazioni <> -1 And vIdPrenotazioni <> 0 Then

                        ' PRENOTAZIONI
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set " _
                            & "DESCRIZIONE=:descrizione,IMPORTO_PRENOTATO=:importo,ID_FORNITORE=:id_fornitore,PERC_IVA=:perciva," _
                            & "ID_STATO=:id_stato " _
                            & " where ID=" & vIdPrenotazioni

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtPrenotatoLordoRIT.Value.Replace(".", ""))))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                        'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_prenotazione", Format(Now, "yyyyMMdd")))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 0))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_PRE.Text.Replace(".", ""))))
                        par.cmd.ExecuteNonQuery()
                        HiddenID.Value = vIdODL
                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()

                        GestioneRITENUTA_ACCONTO()

                    Else

                        ' Ricavo vIdPrenotazioni
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = " select SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM dual "
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            vIdPrenotazioni = myReader1(0)
                        End If
                        myReader1.Close()
                        par.cmd.CommandText = ""


                        ' PRENOTAZIONI
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.PRENOTAZIONI " _
                                                    & " (ID,DESCRIZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_VOCE_PF,TIPO_PAGAMENTO,DATA_PRENOTAZIONE,ID_STATO,ID_STRUTTURA,perc_iva) " _
                                            & "values (:id,:descrizione,:importo,:id_fornitore,:id_voce,:tipo_pagamento,:data_prenotazione,:id_stato,:id_struttura,:perciva)"

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPrenotazioni))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtPrenotatoLordoRIT.Value.Replace(".", ""))))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 4))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_prenotazione", Format(Now, "yyyyMMdd")))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 0))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Me.txtID_STRUTTURA.Value)))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_PRE.Text.Replace(".", ""))))
                        'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imponibile", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))

                        par.cmd.ExecuteNonQuery()
                        HiddenID.Value = vIdODL
                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()

                        GestioneRITENUTA_ACCONTO()

                    End If

                    'If cmbStato.SelectedValue = 1 Then
                    '    Me.cmbContoCorrente.Enabled = False
                    '    Me.cmbContoCorrente.SelectedValue = "-1"
                    'End If

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.ODL " _
                                        & " set DESCRIZIONE=:descrizione,ID_FORNITORE=:id_fornitore,ID_STATO=:stato, " _
                                        & "     PREN_NETTO=:pren_netto,PREN_NO_IVA=:pren_no_iva,PREN_LORDO=:pren_lordo," _
                                        & "     CASSA=:cassa,RIVALSA=:rivalsa,IVA=:iva,RIT_ACCONTO=:rit_acconto," _
                                        & "     CONS_NETTO=:cons_netto,CONS_NO_IVA=:cons_no_iva,CONS_LORDO=:cons_lordo," _
                                        & "     ID_PRENOTAZIONE_RIT=:id_prenotazione_rit, " _
                                        & "     ID_PRENOTAZIONE=:id_prenotazione, " _
                                        & "     PENALE=:penale, " _
                                        & "     CASSA_CONS=:cassa_c,RIVALSA_CONS=:RIVALSA_c,IVA_CONS=:iva_c,RIT_ACCONTO_CONS=:rit_acconto_c, " _
                                        & "     AZIENDA=:azienda, " _
                                        & "     AZIENDA_CONS=:azienda_c, " _
                                        & "     PERCIPIENTE=:percipiente, " _
                                        & "     PERCIPIENTE_CONS=:percipiente_c, " _
                                        & "     COD_RITENUTA=:COD_RITENUTA, " _
                                        & "     DATA_SCAD_PAGAMENTO=:d_scad_pag " _
                                        & " where ID=" & vIdODL

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_netto", strToNumber(Me.txtNettoP.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_no_iva", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_lordo", strToNumber(Me.txtPrenotatoLordo.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa", strToNumber(Me.txtPercCassa.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rivalsa", strToNumber(Me.txtPercRivalsa.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva", strToNumber(Me.txtPercIVA.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto", strToNumber(Me.txtPercRitenuta.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_netto", strToNumber(Me.txtNettoC.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_no_iva", strToNumber(Me.txtImponibileC.Text.Replace(".", ""))))

                    If CDbl(par.IfEmpty(Me.txtConsuntivatoLordo.Value, 0)) > 0 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtConsuntivatoLordo.Value.Replace(".", ""))))
                    ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtLordoC.Text.Replace(".", ""))))
                    Else
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtConsuntivatoLordo.Value.Replace(".", ""))))
                    End If

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione", RitornaNullSeIntegerMenoUno(vIdPrenotazioni)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione_rit", RitornaNullSeIntegerMenoUno(vIdPrenotazioniRIT)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(Me.txtPenaleC.Text.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa_C", strToNumber(Me.txtPercCassa_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("RIVALSA_C", strToNumber(Me.txtPercRivalsa_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_C", strToNumber(Me.txtPercIVA_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto_C", strToNumber(Me.txtPercRitenuta_C.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("d_scad_pag", par.AggiustaData(Me.txtDScad.Text)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("azienda", strToNumber(Me.txtAzienda.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("azienda_C", strToNumber(Me.txtAzienda_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("percipiente", strToNumber(Me.txtPercipiente.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("percipiente_C", strToNumber(Me.txtPercipiente_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("COD_RITENUTA", strToNumber(Me.codRitenuta.Value.Replace(".", ""))))


                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                    Me.txtStatoPagamento.Value = 1 'EMESSO non STAMPATO
                    '..................................................................................................
                Case 0 'BOZZA

                    'If cmbStato.SelectedValue = 1 Then
                    '    Me.cmbContoCorrente.Enabled = False
                    '    Me.cmbContoCorrente.SelectedValue = "-1"
                    'End If

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.ODL " _
                                        & " set DESCRIZIONE=:descrizione,ID_FORNITORE=:id_fornitore,ID_STATO=:stato, " _
                                        & "     PREN_NETTO=:pren_netto,PREN_NO_IVA=:pren_no_iva,PREN_LORDO=:pren_lordo," _
                                        & "     CASSA=:cassa,RIVALSA=:rivalsa,IVA=:iva,RIT_ACCONTO=:rit_acconto," _
                                        & "     CONS_NETTO=:cons_netto,CONS_NO_IVA=:cons_no_iva,CONS_LORDO=:cons_lordo,ID_PRENOTAZIONE_RIT=:id_prenotazione_rit, " _
                                        & "     PENALE=:penale, " _
                                        & "     CASSA_CONS=:cassa_c,RIVALSA_CONS=:RIVALSA_c,IVA_CONS=:iva_c,RIT_ACCONTO_CONS=:rit_acconto_c, " _
                                        & "     AZIENDA=:azienda, " _
                                        & "     AZIENDA_CONS=:azienda_c, " _
                                        & "     PERCIPIENTE=:percipiente, " _
                                        & "     PERCIPIENTE_CONS=:percipiente_c, " _
                                        & "     COD_RITENUTA=:COD_RITENUTA, " _
                                        & "     DATA_SCAD_PAGAMENTO=:d_scad_pag " _
                                        & " where ID=" & vIdODL

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_netto", strToNumber(Me.txtNettoP.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_no_iva", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("pren_lordo", strToNumber(Me.txtPrenotatoLordo.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa", strToNumber(Me.txtPercCassa.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rivalsa", strToNumber(Me.txtPercRivalsa.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva", strToNumber(Me.txtPercIVA.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto", strToNumber(Me.txtPercRitenuta.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_netto", strToNumber(Me.txtNettoC.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_no_iva", strToNumber(Me.txtImponibileC.Text.Replace(".", ""))))

                    If CDbl(par.IfEmpty(Me.txtConsuntivatoLordo.Value, 0)) > 0 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtConsuntivatoLordo.Value.Replace(".", ""))))
                    ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtLordoC.Text.Replace(".", ""))))
                    Else
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtConsuntivatoLordo.Value.Replace(".", ""))))
                    End If


                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione_rit", RitornaNullSeIntegerMenoUno(vIdPrenotazioniRIT)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(Me.txtPenaleC.Text.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa_C", strToNumber(Me.txtPercCassa_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("RIVALSA_C", strToNumber(Me.txtPercRivalsa_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_C", strToNumber(Me.txtPercIVA_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto_C", strToNumber(Me.txtPercRitenuta_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("d_scad_pag", par.AggiustaData(Me.txtDScad.Text)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("azienda", strToNumber(Me.txtAzienda.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("azienda_C", strToNumber(Me.txtAzienda_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("percipiente", strToNumber(Me.txtPercipiente.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("percipiente_C", strToNumber(Me.txtPercipiente_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("COD_RITENUTA", strToNumber(Me.codRitenuta.Value.Replace(".", ""))))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                    Me.txtStatoPagamento.Value = 1 'EMESSO non STAMPATO

                Case 2  'CONSUNTIVATO


                    'If vIdPagamenti <> 0 Then
                    '    'MODIFICA TAB PAGAMENTI
                    '    par.cmd.Parameters.Clear()
                    '    par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                    '                        & " set DESCRIZIONE=:descrizione, IMPORTO_CONSUNTIVATO=:importo,ID_FORNITORE=:id_fornitore,CONTO_CORRENTE=:conto_corrente " _
                    '                        & " where ID=" & vIdPagamenti

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                    '    If CDbl(par.IfEmpty(Me.txtConsuntivatoLordoRIT.Value, 0)) > 0 Then
                    '        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                    '    ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                    '        risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0) - par.IfEmpty(Me.txtRitenutaC.Text, 0)
                    '        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", risultatoRIT))
                    '    Else
                    '        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                    '    End If

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                    '    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imponibile", strToNumber(Me.txtImponibileC.Text.Replace(".", ""))))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))


                    '    par.cmd.ExecuteNonQuery()
                    '    par.cmd.Parameters.Clear()
                    'Else
                    '    'CREAZIONE TAB PAGAMENTI
                    '    par.cmd.Parameters.Clear()
                    '    par.cmd.CommandText = " select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM dual "
                    '    myReaderT = par.cmd.ExecuteReader()
                    '    If myReaderT.Read Then
                    '        vIdPagamenti = myReaderT(0)
                    '    End If
                    '    myReaderT.Close()


                    '    par.cmd.Parameters.Clear()
                    '    par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI " _
                    '                                & " (ID,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_STATO,TIPO_PAGAMENTO,DATA_EMISSIONE,CONTO_CORRENTE) " _
                    '                        & "values (:id,:descrizione,:importo,:id_fornitore,:id_stato,:tipo_pagamento,:data,:conto_corrente)"

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamenti))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                    '    If CDbl(par.IfEmpty(Me.txtConsuntivatoLordoRIT.Value, 0)) > 0 Then
                    '        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                    '    ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                    '        risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0) - par.IfEmpty(Me.txtRitenutaC.Text, 0)
                    '        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", risultatoRIT))
                    '    Else
                    '        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                    '    End If

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                    '    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 4))
                    '    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imponibile", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMdd")))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))


                    '    par.cmd.ExecuteNonQuery()
                    '    par.cmd.CommandText = ""
                    'End If

                    'MODIFICA PRENOTAZIONI
                    If vIdPrenotazioni <> -1 Then

                        'MODIFICA PRENOTAZIONI
                        par.cmd.Parameters.Clear()
                        'ID_PAGAMENTO=:id_pagamento ora in stampa pagamento 23/12/2012
                        par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                                            & " set IMPORTO_APPROVATO=:importoC,ID_STATO=:id_stato,PERC_IVA=:perciva " _
                                            & " where ID=" & vIdPrenotazioni

                        If CDbl(par.IfEmpty(Me.txtConsuntivatoLordoRIT.Value, 0)) > 0 Then
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                        ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                            'risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0) - par.IfEmpty(Me.txtRitenutaC.Text, 0)

                            If HiddenFieldTipoProfessionista.Value.ToString.ToUpper = "P" Then
                                risultatoRIT = CDec(par.IfEmpty(Me.txtDaPagareC.Text, 0)) + CDec(par.IfEmpty(Me.txtIVAC.Text, 0))
                            Else
                            risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)
                            End If

                            'If par.IfEmpty(Me.txtRitenutaC.Text, 0) = 0 Then
                            '    risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0)
                            'Else
                            '    risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)
                            'End If

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", risultatoRIT))
                        Else
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                        End If

                        'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_pagamento", vIdPagamenti))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_CONS.Text.Replace(".", ""))))
                    Else
                        '*****15/11/2012***********PEPPE/MARCO MODIFY*******************

                        ' Ricavo vIdPrenotazioni
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = " select SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM dual "
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            vIdPrenotazioni = myReader1(0)
                        End If
                        myReader1.Close()
                        par.cmd.CommandText = ""

                        par.cmd.CommandText = "insert into SISCOM_MI.PRENOTAZIONI " _
                                                    & " (ID,DESCRIZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,ID_FORNITORE,ID_VOCE_PF,TIPO_PAGAMENTO,DATA_PRENOTAZIONE,ID_STATO,ID_STRUTTURA,PERC_IVA) " _
                                            & "values (:id,:descrizione,:importo,:importoC,:id_fornitore,:id_voce,:tipo_pagamento,:data_prenotazione,:id_stato,:id_struttura,:perciva)"


                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPrenotazioni))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtPrenotatoLordoRIT.Value.Replace(".", ""))))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 4))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_prenotazione", Format(Now, "yyyyMMdd")))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Me.txtID_STRUTTURA.Value)))


                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_PRE.Text.Replace(".", ""))))
                        If CDbl(par.IfEmpty(Me.txtConsuntivatoLordoRIT.Value, 0)) > 0 Then
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                        ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                            'risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0) - par.IfEmpty(Me.txtRitenutaC.Text, 0)


                            If HiddenFieldTipoProfessionista.Value.ToString.ToUpper = "P" Then
                                risultatoRIT = CDec(par.IfEmpty(Me.txtDaPagareC.Text, 0)) + CDec(par.IfEmpty(Me.txtIVAC.Text, 0))
                            Else
                            risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)
                            End If

                            'If par.IfEmpty(Me.txtRitenutaC.Text, 0) = 0 Then
                            '    risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0)
                            'Else
                            'risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)
                            'End If

                            'risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", risultatoRIT))
                        Else
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                        End If

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))
                    End If
                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()
                    GestioneRITENUTA_ACCONTO()
                    'MODIFICA ODL
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.ODL " _
                                        & " set ID_STATO=:stato, " _
                                        & "     CONS_NETTO=:cons_netto,CONS_NO_IVA=:cons_no_iva,CONS_LORDO=:cons_lordo, " _
                                        & "     PENALE=:penale,CONTO_CORRENTE=:conto_corrente, " _
                                        & "     CASSA_CONS=:cassa_c,RIVALSA_CONS=:RIVALSA_c,IVA_CONS=:iva_c,RIT_ACCONTO_CONS=:rit_acconto_c," _
                                        & "     AZIENDA=:azienda, " _
                                        & "     AZIENDA_CONS=:azienda_c, " _
                                        & "     PERCIPIENTE=:percipiente, " _
                                        & "     PERCIPIENTE_CONS=:percipiente_c, " _
                                        & "     COD_RITENUTA=:COD_RITENUTA, " _
                                        & " ID_PRENOTAZIONE_RIT=:id_prenotazione_rit " _
                                        & " where ID=" & vIdODL

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamenti))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_netto", strToNumber(Me.txtNettoC.Text.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_no_iva", strToNumber(Me.txtImponibileC.Text.Replace(".", ""))))

                    If CDbl(par.IfEmpty(Me.txtConsuntivatoLordo.Value, 0)) > 0 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtConsuntivatoLordo.Value.Replace(".", ""))))
                    ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtLordoC.Text.Replace(".", ""))))
                    Else
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cons_lordo", strToNumber(Me.txtConsuntivatoLordo.Value.Replace(".", ""))))
                    End If

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(Me.txtPenaleC.Text.Replace(".", ""))))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", Me.cmbContoCorrente.SelectedItem.ToString))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cassa_C", strToNumber(Me.txtPercCassa_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("RIVALSA_C", strToNumber(Me.txtPercRivalsa_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_C", strToNumber(Me.txtPercIVA_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_acconto_C", strToNumber(Me.txtPercRitenuta_C.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione_rit", RitornaNullSeIntegerMenoUno(vIdPrenotazioniRIT)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("azienda", strToNumber(Me.txtAzienda.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("azienda_C", strToNumber(Me.txtAzienda_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("percipiente", strToNumber(Me.txtPercipiente.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("percipiente_C", strToNumber(Me.txtPercipiente_C.Value.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("COD_RITENUTA", strToNumber(Me.codRitenuta.Value.Replace(".", ""))))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                    Me.txtStatoPagamento.Value = 2 'PAGAMENTO no STAMPATO
                    Me.cmbfornitore.Enabled = False

                Case 5
                    'par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                    '                    & " set NUMERO_MANDATO=:descrizione, DATA_MANDATO=:data,ID_STATO=5" _
                    '                    & " where ID=" & vIdPagamenti

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtNumMandato.Text, 50)))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtDataMandato.Text)))

                    'par.cmd.ExecuteNonQuery()

            End Select


            ''****Scrittura evento PAGAMENTO
            Select Case Me.cmbStato.SelectedValue
                Case 0 'BOZZA
                    If par.IfEmpty(Me.txtNettoP.Text, 0) <> par.IfEmpty(Me.txtNettoP_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Prenotato lordo da:" & par.IfEmpty(Me.txtNettoP_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtNettoP.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If par.IfEmpty(Me.txtImponibileP.Text, 0) <> par.IfEmpty(Me.txtImponibileP_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Imponibile non soggetto a IVA da: " & par.IfEmpty(Me.txtImponibileP_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtImponibileP.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                Case 1

                    If Val(txtSTATO.Value) = 0 Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Da BOZZA a EMESSO')"
                        par.cmd.ExecuteNonQuery()
                    End If


                    If par.IfEmpty(Me.txtNettoP.Text, 0) <> par.IfEmpty(Me.txtNettoP_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Prenotato lordo da: " & par.IfEmpty(Me.txtNettoP_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtNettoP.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If Me.txtImponibileP.Text <> Me.txtImponibileP_ODL.Value Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Imponibile non soggetto a IVA da: " & par.IfEmpty(Me.txtImponibileP_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtImponibileP.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If par.IfEmpty(Me.txtNettoC.Text, 0) <> par.IfEmpty(Me.txtNettoC_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Consuntivato lordo da: " & par.IfEmpty(Me.txtNettoC_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtNettoC.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If par.IfEmpty(Me.txtImponibileC.Text, 0) <> par.IfEmpty(Me.txtImponibileC_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Imponibile non soggetto a IVA da: " & par.IfEmpty(Me.txtImponibileC_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtImponibileC.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                Case 2

                    If Val(txtSTATO.Value) = 1 Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Da EMESSO a CONSUNTIVATO')"
                        par.cmd.ExecuteNonQuery()
                    End If


                    If par.IfEmpty(Me.txtNettoC.Text, 0) <> par.IfEmpty(Me.txtNettoC_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Consuntivato lordo da: " & par.IfEmpty(Me.txtNettoC_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtNettoC.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If

                    If par.IfEmpty(Me.txtImponibileC.Text, 0) <> par.IfEmpty(Me.txtImponibileC_ODL.Value, 0) Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Imponibile non soggetto a IVA da: " & par.IfEmpty(Me.txtImponibileC_ODL.Value, 0) & " a " & par.IfEmpty(Me.txtImponibileC.Text, 0) & "')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    Me.txtStatoPagamento.Value = 3 'consuntivato - pagamento non stampato 
            End Select
            txtSTATO.Value = Me.cmbStato.SelectedValue
            '****************************************************



            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""
            HiddenID.Value = vIdODL
            par.cmd.Parameters.Clear()


            'SERVONO PER IL LOG EVENTI
            Me.txtNettoP_ODL.Value = Me.txtNettoP.Text
            Me.txtImponibileP_ODL.Value = Me.txtImponibileP.Text

            Me.txtNettoC_ODL.Value = Me.txtNettoC.Text
            Me.txtImponibileC_ODL.Value = Me.txtImponibileC.Text
            '*************************

            'IMPORTO PRENOTATO in BOZZA DA APPROVARE e EMESSO
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=0 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                sRisultato = par.IfNull(myReaderT(0), "0")
                SommaPrenotato = Decimal.Parse(sRisultato)
            End If
            myReaderT.Close()

            ''IMPORTO APPROVATO in EMESSO APPROVATO e NON ANCORA STAMPATO il PAGAMENTO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=1 " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   ID_VOCE_PF" & sSelect1 & sFiliale

            myReaderT = par.cmd.ExecuteReader()

            If myReaderT.Read Then
                sRisultato = par.IfNull(myReaderT(0), "0")
                SommaConsuntivato = Decimal.Parse(sRisultato)
            End If
            myReaderT.Close()


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select SUM(IMPORTO_APPROVATO) " _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_VOCE_PF" & sSelect1 _
                                & "   and  ID_STATO>=1 " & sFiliale _
                                & "   and   ID_PAGAMENTO is not null "


            myReaderT = par.cmd.ExecuteReader
            If myReaderT.Read Then
                sRisultato = par.IfNull(myReaderT(0), "0")
                SommaConsuntivato = SommaConsuntivato + Decimal.Parse(sRisultato)
            End If
            myReaderT.Close()


            'IMPORTO  LIQUIDATO (per il momento si prende da PAGAMENTI, ma è da AGGIUSTARE)
            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
            '       & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                & " where  ID_VOCE_PF=" & idVoce _
            '                                & "   and  ID_STATO>1 " & sFiliale & ")"


            ''par.cmd.CommandText = "select  IMPORTO_CONSUNTIVATO,ID_STATO,DATA_STAMPA" _
            ''        & " from SISCOM_MI.PAGAMENTI where PAGAMENTI.ID_VOCE_PF=" & idVoce

            'myReaderT = par.cmd.ExecuteReader()

            'While myReaderT.Read
            '    Select Case par.IfNull(myReaderT("ID_STATO"), 0)
            '        'Case 1 'EMESSO
            '        '    SommaConsuntivato = SommaConsuntivato + par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0)
            '        Case 5  'LIQUIDATO
            '            SommaLiquidato = SommaLiquidato + par.IfNull(myReaderT("IMPORTO_CONSUNTIVATO"), 0)
            '    End Select
            'End While
            'myReaderT.Close()

            'COMMENTATO 
            'SOLO CONSUNTIVATI anche se sono stata PAGATI (per il momento fin quando non ci dicono come fare, 
            ' perchè più prenotazioni  con voci doverse possono avere un unico pagamento
            'par.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
            '                   & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
            '                   & "  where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF" & sSelect1 _
            '                                            & "   and  ID_STATO>1 " & sFiliale & ")"

            'myReaderT = par.cmd.ExecuteReader
            'If myReaderT.Read Then
            '    sRisultato = par.IfNull(myReaderT(0), "0")
            '    SommaLiquidato = Decimal.Parse(sRisultato)
            'End If
            'myReaderT.Close()
            '********** FINE COMMENTO

            SommaResiduo = Me.txtImporto1.Text

            SommaConsuntivato = SommaConsuntivato '- SommaLiquidato

            SommaResiduo = SommaResiduo - (SommaPrenotato + SommaConsuntivato) ' + SommaLiquidato)

            Me.txtImporto2.Text = IsNumFormat(SommaPrenotato, "", "##,##0.00")
            Me.txtImporto3.Text = IsNumFormat(SommaConsuntivato, "", "##,##0.00")
            'Me.txtImporto4.Text = IsNumFormat(SommaLiquidato, "", "##,##0.00")

            Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")


            CalcolaImporti(par.IfEmpty(Me.txtNettoP.Text, 0), par.IfEmpty(Me.txtImponibileP.Text, 0), par.IfEmpty(Me.txtPercIVA.Value, 0), par.IfEmpty(Me.txtPercCassa.Value, 0), par.IfEmpty(Me.txtPercRitenuta.Value, 0), 0, "PRENOTATO", par.IfEmpty(Me.txtPercRivalsa.Value, 0), par.IfEmpty(Me.txtPercipiente.Value, 0), par.IfEmpty(Me.txtAzienda.Value, 0), codRitenuta.Value)
            CalcolaImporti(par.IfEmpty(Me.txtNettoC.Text, 0), par.IfEmpty(Me.txtImponibileC.Text, 0), par.IfEmpty(Me.txtPercIVA_C.Value, 0), par.IfEmpty(Me.txtPercCassa_C.Value, 0), par.IfEmpty(Me.txtPercRitenuta_C.Value, 0), par.IfEmpty(Me.txtPenaleC.Text, 0), "CONSUNTIVO", par.IfEmpty(Me.txtPercRivalsa_C.Value, 0), par.IfEmpty(Me.txtPercipiente_C.Value, 0), par.IfEmpty(Me.txtAzienda_C.Value, 0), codRitenuta.Value)


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID = " & vIdODL & " FOR UPDATE NOWAIT"
            myReaderT = par.cmd.ExecuteReader()
            myReaderT.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            RadNotificationNote.Text = "Operazione completata correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()



            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            txtModificato.Text = "0"


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


        End Try

    End Sub
 Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click

        sValoreProvenienza = Request.QueryString("PROVENIENZA")

        If txtModificato.Text <> "111" Then

            'CHIUSURA DB
            If sValoreProvenienza <> "CHIAMATA_DIRETTA" Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Item("LAVORAZIONE") = "0"

            End If

            Page.Dispose()
            '**************************
            If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"
                Response.Write("<script>window.close();</script>")
            ElseIf x.Value = "1" Then
                Response.Write("<script language='javascript'> { self.close(); }</script>")
            Else
                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub
Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Dim sNote As String

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If Me.txtElimina.Value = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "delete from SISCOM_MI.ODL where ID = " & vIdODL
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & vIdPrenotazioni
                '27/03/2013 MODIFICA ANNUO PRENOTAZIONE
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & vIdPrenotazioni
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=-3 where ID=" & vIdPrenotazioni

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & vIdPrenotazioniRIT
                '27/03/2013 MODIFICA ANNUO PRENOTAZIONE
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & vIdPrenotazioniRIT
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=-3 where ID=" & vIdPrenotazioniRIT

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                If vIdPagamentiRIT <> 0 Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamentiRIT
                    vIdPagamentiRIT = 0
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                End If

                'LOG CANCELLAZIONE
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE PAGAMENTO ODL"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "PAGAMENTO ODL"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.lblODL.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(par.IfEmpty(txtDescrizioneP.Text, ""), 200)))

                sNote = "Cancellazione Pagamento del beneficiario: " & Me.cmbfornitore.SelectedItem.Text
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", sNote))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************


                ' COMMIT
                par.myTrans.Commit()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                RadNotificationNote.Text = "Eliminazione completata correttamente!"
                RadNotificationNote.AutoCloseDelay = "500"
                RadNotificationNote.Show()

                'DA RICERCA SELETTIVA
                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))
                sValoreStato = Request.QueryString("ST")

                sValoreFornitore = Request.QueryString("FO")
                sValoreStruttura = Request.QueryString("STR")

                sValoreDataP_Dal = Request.QueryString("DALP")
                sValoreDataP_Al = Request.QueryString("ALP")

                'sValoreDataE_Dal = Request.QueryString("DALE")
                'sValoreDataE_Al = Request.QueryString("ALE")

                'DA RICERCA DIRETTA
                sValoreODL = Request.QueryString("ODL")
                sValoreAnno = Request.QueryString("ANNO")

                sOrdinamento = Request.QueryString("ORD")


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Item("LAVORAZIONE") = "0"

                Page.Dispose()


                If txtindietro.Text = 1 Then
                    If x.Value = "1" Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
                    End If
                Else
                    If x.Value = "1" Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        If sValoreStato = "RICERCA_DIRETTA" Then
                            Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & sValoreStato _
                                                                                         & "&ODL=" & sValoreODL _
                                                                                         & "&ANNO=" & sValoreAnno _
                                                                                         & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                         & "&ORD=" & sOrdinamento & "');</script>")
                        Else
                            Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & sValoreStato _
                                                                                          & "&FO=" & sValoreFornitore _
                                                                                          & "&DALP=" & sValoreDataP_Dal _
                                                                                          & "&ALP=" & sValoreDataP_Al _
                                                                                          & "&STR=" & sValoreStruttura _
                                                                                          & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                          & "&ORD=" & sOrdinamento & "');</script>")
                        End If
                    End If
                End If

            Else
                CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
 Protected Sub btnINDIETRO_Click(sender As Object, e As System.EventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then

            'DA RICERCA SELETTIVA
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreStato = Request.QueryString("ST")

            sValoreFornitore = Request.QueryString("FO")
            sValoreStruttura = Request.QueryString("STR")

            sValoreDataP_Dal = Request.QueryString("DALP")
            sValoreDataP_Al = Request.QueryString("ALP")

            'sValoreDataE_Dal = Request.QueryString("DALE")
            'sValoreDataE_Al = Request.QueryString("ALE")


            'DA RICERCA DIRETTA
            sValoreODL = Request.QueryString("ODL")
            sValoreAnno = Request.QueryString("ANNO")


            sOrdinamento = Request.QueryString("ORD")


            'CHIUSURA DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()
            '**************************


            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")


            If txtindietro.Text = 1 Then
                If x.Value = "1" Then
                    Response.Write("<script language='javascript'> { self.close(); }</script>")
                Else
                    Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
                End If
            Else
                If x.Value = "1" Then
                    Response.Write("<script language='javascript'> { self.close(); }</script>")
                Else
                    If sValoreStato = "RICERCA_DIRETTA" Then
                        Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & sValoreStato _
                                                                                     & "&ODL=" & sValoreODL _
                                                                                     & "&ANNO=" & sValoreAnno _
                                                                                     & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                     & "&ORD=" & sOrdinamento & "');</script>")
                    Else
                        Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & sValoreStato _
                                                                                      & "&FO=" & sValoreFornitore _
                                                                                      & "&DALP=" & sValoreDataP_Dal _
                                                                                      & "&ALP=" & sValoreDataP_Al _
                                                                                      & "&STR=" & sValoreStruttura _
                                                                                      & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                      & "&ORD=" & sOrdinamento & "');</script>")

                    End If
                End If
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub

   
   Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If Me.txtElimina.Value = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                par.cmd.Parameters.Clear()
                '27/03/2013 MODIFICA ANNULLO ODL
                'par.cmd.CommandText = "update SISCOM_MI.ODL set ID_STATO=5,ID_PRENOTAZIONE=Null,ID_PRENOTAZIONE_RIT=Null where ID=" & vIdODL
                par.cmd.CommandText = "update SISCOM_MI.ODL set ID_STATO=5 where ID=" & vIdODL
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & vIdPrenotazioni
                '27/03/2013 MODIFICA ANNULLO PRENOTAZIONI
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & vIdPrenotazioni
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=-3 where ID=" & vIdPrenotazioni

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & vIdPrenotazioniRIT
                '27/03/2013 MODIFICA ANNULLO PRENOTAZIONI RITENUTE D'ACCONTO
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & vIdPrenotazioniRIT
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=-3 where ID=" & vIdPrenotazioniRIT

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Da EMESSO ad ANNULLATO')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                Setta_StataoODL(5)  'ANNULLATO
                Me.cmbStato.SelectedValue = 5

                Me.txtSTATO.Value = 5
                Me.txtStatoPagamento.Value = 5

                Me.txtVisualizza.Value = 2    'SOLO LETTURA

                ' COMMIT
                par.myTrans.Commit()

                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID = " & vIdODL & " FOR UPDATE NOWAIT"
                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReaderT.Close()

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                RadNotificationNote.Text = "Annullamento completato correttamente!"
                RadNotificationNote.AutoCloseDelay = "500"
                RadNotificationNote.Show()
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                Me.txtModificato.Text = "0"

            Else
                CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value
        Try

            If valorepass <> -1 Then
                a = valorepass
            End If

        Catch ex As Exception

        End Try

        Return a
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
        End Try
        Return a

    End Function


    Public Function strToNumber(ByVal s0 As String) As Object
        Dim s As String = s0.Replace(".", ",")

        Dim d As Double

        If Double.TryParse(s, d) = True Then
            Return d
        Else
            Return DBNull.Value
        End If
    End Function




    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Sub CalcolaImporti(ByVal netto As Decimal, ByVal imponibile As Decimal, ByVal iva As Decimal, ByVal cassa As Decimal, ByVal ritenuta As Decimal, ByVal penale As Decimal, ByVal flag As String, ByVal rivalsa As Decimal, ByVal percipiente As Decimal, ByVal azienda As Decimal, ByVal CodRitenuta As String)
        'ris1) netto
        'ris2) rivalsa=rivalsa% su ris1             ris1*RIVALSA/100
        'ris3) cassa=cassa% su ris1                 ris1*CASSA/100
        'ris4) iva=iva% su ris1)+ris2)+ris3)        (ris1+ris2+ris3)*IVA/100
        'ris5) rit=rit% su ris1)+ris2)              (ris1+ris2)*RIT/100
        'se ritenuta in (001,002,010,012)
        '       ris9) lordo=ris1+ris2+ris3+ris4+ris6
        '       ris10) daPagare=ris9-ris5
        'else
        '       ris9) lordo=ris1+ris2+ris3+ris4+ris6+ris8
        '       ris10) daPagare=ris1+ris2+ris3+ris4+ris6-ris5-ris7

        Dim ris1 As Decimal = netto
        If flag = "CONSUNTIVO" Then
            ris1 = netto - penale
        End If
        Dim ris2 As Decimal = Round(ris1 * rivalsa / 100, 2)
        Dim ris3 As Decimal = Round(ris1 * cassa / 100, 2)
        Dim ris4 As Decimal = Round((ris1 + ris2 + ris3) * iva / 100, 2)
        Dim ris5 As Decimal = (ris1 + ris2) * ritenuta / 100
        Dim ris6 As Decimal = imponibile
        Dim ris7 As Decimal = percipiente
        Dim ris8 As Decimal = azienda
        Dim ris9 As Decimal = 0
        Dim ris10 As Decimal = 0
        Dim dataEmissione As String = HiddenFieldDataEmissione.Value
        If dataEmissione = "0" Then
            dataEmissione = Format(Now, "yyyyMMdd")
        End If
        If CodRitenuta = "301" Then
            ris9 = ris1 + ris2 + ris3 + ris4 + ris6 + ris8
            If dataEmissione >= "20170701" And dataEmissione < "20180715" Then
                '16/11/2017
                'lascio scritto ris4-ris4 per ricordarci della modifica
                If HiddenFieldTipoProfessionista.Value.ToString.ToUpper = "P" Then
                ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7 - ris4
            Else
            ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7
            End If
        Else
                ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7
            End If
        Else
            ris9 = ris1 + ris2 + ris3 + ris4 + ris6
            If dataEmissione >= "20170701" And dataEmissione < "20180715" Then
                If HiddenFieldTipoProfessionista.Value.ToString.ToUpper = "P" Then
                ris10 = ris9 - ris5 - ris4
            Else
            ris10 = ris9 - ris5
        End If
            Else
                ris10 = ris9 - ris5
            End If
        End If

        If flag = "PRENOTATO" Then
            txtNettoP.Text = IsNumFormat(netto, "", "##,##0.00")
            txtRivalsa_PRE.Text = IsNumFormat(rivalsa, "", "##,##0.00")
            txtCass_PRE.Text = IsNumFormat(cassa, "", "##,##0.00")
            txtIVA_PRE.Text = IsNumFormat(iva, "", "##,##0.00")
            txtRitenuta_PRE.Text = IsNumFormat(ritenuta, "", "##,##0.00")
            txtRivalsaP.Text = IsNumFormat(ris2, "", "##,##0.00")
            txtCassaP.Text = IsNumFormat(ris3, "", "##,##0.00")
            txtIVAP.Text = IsNumFormat(ris4, "", "##,##0.00")
            txtRitenutaP.Text = IsNumFormat(ris5, "", "##,##0.00")
            txtImponibileP.Text = IsNumFormat(ris6, "", "##,##0.00")
            txtIMPScaricoPercipiente.Text = IsNumFormat(ris7, "", "##,##0.00")
            txtIMPScaricoAzienda.Text = IsNumFormat(ris8, "", "##,##0.00")
            txtLordoP.Text = IsNumFormat(ris9, "", "##,##0.00")
            txtDaPagare.Text = IsNumFormat(ris10, "", "##,##0.00")
        Else
            txtNettoC.Text = IsNumFormat(netto, "", "##,##0.00")
            txtRivalsa_CONS.Text = IsNumFormat(rivalsa, "", "##,##0.00")
            txtCass_CONS.Text = IsNumFormat(cassa, "", "##,##0.00")
            txtIVA_CONS.Text = IsNumFormat(iva, "", "##,##0.00")
            txtRitenuta_CONS.Text = IsNumFormat(ritenuta, "", "##,##0.00")
            txtRivalsaC.Text = IsNumFormat(ris2, "", "##,##0.00")
            txtCassaC.Text = IsNumFormat(ris3, "", "##,##0.00")
            txtIVAC.Text = IsNumFormat(ris4, "", "##,##0.00")
            txtRitenutaC.Text = IsNumFormat(ris5, "", "##,##0.00")
            txtImponibileC.Text = IsNumFormat(ris6, "", "##,##0.00")
            txtIMPScaricoPercipienteC.Text = IsNumFormat(ris7, "", "##,##0.00")
            txtIMPScaricoAziendaC.Text = IsNumFormat(ris8, "", "##,##0.00")
            txtLordoC.Text = IsNumFormat(ris9, "", "##,##0.00")
            txtDaPagareC.Text = IsNumFormat(ris10, "", "##,##0.00")
        End If

        'If flag = "CONSUNTIVO" Then
        '    netto = netto - penale
        'End If

        ''B) CASSA
        'cassa = (netto * cassa) / 100
        'cassa = Round(cassa, 2)

        ''C) A+B
        'risultato1 = netto + cassa

        ''D)
        'iva = (risultato1 * iva) / 100
        'iva = Round(iva, 2)

        ''E) C+D ovvero A+B+D + IMPONIBILE
        'risultato1 = netto + cassa + iva + imponibile '- ritenuta

        ''F) RITENUTA
        'ritenuta = (netto * ritenuta) / 100
        'ritenuta = Round(ritenuta, 2)

        '' QUELLO CHE VA IN PRENOTAZIONE 1 o PAGAMENTO 1 cioè l'importo LORDO meno la ritenuta
        'risultatoRIT = netto + cassa + iva + imponibile - ritenuta

    End Sub



    Sub GestioneRITENUTA_ACCONTO()
        Dim FlagConnessione As Boolean

        Try
            If vIdPrenotazioniRIT <= 0 Then

                If cmbStato.SelectedValue < 2 Then
                    If par.IfEmpty(Me.txtRitenutaP.Text, 0) = 0 Then
                        Exit Sub
                    End If
                Else
                    If par.IfEmpty(Me.txtRitenutaC.Text, 0) = 0 Then
                        Exit Sub
                    End If
                End If


                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                'CREO PRENOTAZIONI x RITENUTA ACCONTO
                par.cmd.CommandText = " select SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM dual "
                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReaderT.Read Then
                    vIdPrenotazioniRIT = myReaderT(0)
                End If
                myReaderT.Close()



                ' PRENOTAZIONI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.PRENOTAZIONI " _
                                            & " (ID,DESCRIZIONE,IMPORTO_PRENOTATO,IMPORTO_APPROVATO,ID_FORNITORE,ID_VOCE_PF,TIPO_PAGAMENTO,DATA_PRENOTAZIONE,ID_STATO,DATA_CONSUNTIVAZIONE,ID_STRUTTURA,perc_iva) " _
                                    & "values (:id,:descrizione,:importoP,:importoC,:id_fornitore,:id_voce,:tipo_pagamento,:data_prenotazione,:id_stato,:data_consuntivazione,:id_struttura,:perciva)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPrenotazioniRIT))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                If cmbStato.SelectedValue < 2 Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", 0))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))
                End If

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 5))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_prenotazione", Format(Now, "yyyyMMdd")))


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_PRE.Text.Replace(".", ""))))
                If cmbStato.SelectedValue < 2 Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 0))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_consuntivazione", DBNull.Value))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_consuntivazione", Format(Now, "yyyyMMdd")))
                End If


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Session.Item("ID_STRUTTURA"))))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                '****************************************************
            Else

                If par.IfEmpty(Me.txtRitenutaP.Text, 0) = 0 And par.IfEmpty(Me.txtRitenutaC.Text, 0) = 0 Then
                    If vIdODL > 0 Then
                        par.cmd.Parameters.Clear()
                        '27/03/2013
                        'MOFIDIFCA ANNULLO RITENUTE D'ACCONTO, non cancello il riferimento alla prenotazione della ritenuta d'acconto dall'odl
                        'par.cmd.CommandText = "update SISCOM_MI.ODL set ID_PRENOTAZIONE_RIT=Null where ID=" & vIdODL

                        'par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                    End If

                    par.cmd.Parameters.Clear()
                    'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID=" & vIdPrenotazioniRIT
                    '27/03/2013 MODIFICA ANNULLO RITENUTE D'ACCONTO
                    'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & vIdPrenotazioniRIT
                    par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=-3 where ID=" & vIdPrenotazioniRIT

                    vIdPrenotazioniRIT = -1
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    If vIdPagamentiRIT <> 0 Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamentiRIT
                        vIdPagamentiRIT = 0
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                    End If

                Else

                    Select Case cmbStato.SelectedValue
                        Case 0, 1 'BOZZA ed EMESSO
                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                                                & " set DESCRIZIONE=:descrizione,IMPORTO_PRENOTATO=:importoP,ID_FORNITORE=:id_fornitore,ID_STATO=:id_stato,PERC_IVA=:perciva " _
                                                & " where ID=" & vIdPrenotazioniRIT

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 0))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_PRE.Text.Replace(".", ""))))
                            par.cmd.ExecuteNonQuery()
                            par.cmd.Parameters.Clear()

                            'Case 1 ' EMESSO
                            '    par.cmd.Parameters.Clear()
                            '    par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                            '                        & " set DESCRIZIONE=:descrizione,IMPORTO_PRENOTATO=:importoP,IMPORTO_APPROVATO=:importoC,ID_FORNITORE=:id_fornitore,ID_STATO=:id_stato " _
                            '                        & " where ID=" & vIdPrenotazioniRIT

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))

                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.Parameters.Clear()

                        Case 2  'CONSUNTIVATO


                            par.cmd.Parameters.Clear()
                            par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                                                & " set DESCRIZIONE=:descrizione,IMPORTO_PRENOTATO=:importoP,IMPORTO_APPROVATO=:importoC,ID_FORNITORE=:id_fornitore,ID_STATO=:id_stato,PERC_IVA=:perciva " _
                                                & " where ID=" & vIdPrenotazioniRIT

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))

                            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("perciva", strToNumber(Me.txtIVA_CONS.Text.Replace(".", ""))))
                            par.cmd.ExecuteNonQuery()
                            par.cmd.Parameters.Clear()

                            'If vIdPagamentiRIT <> 0 Then
                            '    par.cmd.Parameters.Clear()
                            '    par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                            '                        & " set DESCRIZIONE=:descrizione, IMPORTO_CONSUNTIVATO=:importo,ID_FORNITORE=:id_fornitore,CONTO_CORRENTE=:conto_corrente " _
                            '                        & " where ID=" & vIdPagamentiRIT

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))


                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.Parameters.Clear()


                            'Else
                            '    'CREAZIONE TAB PAGAMENTI
                            '    par.cmd.CommandText = " select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM dual "
                            '    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            '    If myReaderT.Read Then
                            '        vIdPagamentiRIT = myReaderT(0)
                            '    End If
                            '    myReaderT.Close()

                            '    par.cmd.Parameters.Clear()
                            '    par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI " _
                            '                                & " (ID,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_STATO,TIPO_PAGAMENTO,DATA_EMISSIONE,CONTO_CORRENTE) " _
                            '                        & "values (:id,:descrizione,:importo,:id_fornitore,:id_stato,:tipo_pagamento,:data,:conto_corrente)"

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamentiRIT))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 5))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMdd")))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))


                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.CommandText = ""

                            '    par.cmd.Parameters.Clear()
                            '    par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                            '                        & " set IMPORTO_APPROVATO=:importo,ID_PAGAMENTO=:id_pagamento,ID_STATO=:id_stato " _
                            '                        & " where ID=" & vIdPrenotazioniRIT

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_pagamento", vIdPagamentiRIT))
                            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))


                            '    par.cmd.ExecuteNonQuery()
                            '    par.cmd.Parameters.Clear()

                            'End If


                    End Select

                    'If cmbStato.SelectedValue = 2 Then
                    '    par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                    '                        & " set DESCRIZIONE=:descrizione,IMPORTO_PRENOTATO=:importoP,IMPORTO_APPROVATO=:importoC,ID_FORNITORE=:id_fornitore,ID_STATO=:id_stato " _
                    '                        & " where ID=" & vIdPrenotazioniRIT

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoC", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))

                    '    par.cmd.ExecuteNonQuery()
                    '    par.cmd.Parameters.Clear()

                    'Else
                    '    par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                    '                    & " set DESCRIZIONE=:descrizione,IMPORTO_PRENOTATO=:importoP,ID_FORNITORE=:id_fornitore,ID_STATO=:id_stato " _
                    '                    & " where ID=" & vIdPrenotazioniRIT

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql (Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importoP", strToNumber(Me.txtRitenutaP.Text.Replace(".", ""))))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                    '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 0))

                    '    par.cmd.ExecuteNonQuery()
                    '    par.cmd.Parameters.Clear()

                    'End If
                End If
            End If

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    Private Sub SettaIMPORTO_INTERVENTI()

        'Dim FlagConnessione As Boolean

        'Try

        '    FlagConnessione = False
        '    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)

        '        FlagConnessione = True
        '    End If


        '    CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text = 0

        '    par.cmd.CommandText = "select SUM(IMPORTO_PRESUNTO) from SISCOM_MI.MANUTENZIONI_INTERVENTI where ID_MANUTENZIONE=" & vIdManutenzione
        '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        '    If myReader1.Read Then
        '        CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(par.IfNull(myReader1(0), 0), "", "##,##0.00")
        '        CalcolaImporti(par.IfNull(myReader1(0), 0), par.IfEmpty(Me.txtPercOneri.Value, 0), par.IfEmpty(Me.txtScontoConsumo.Value, 0), par.IfEmpty(Me.txtPercIVA.Value, 0))
        '    Else
        '        CalcolaImporti(0, par.IfEmpty(Me.txtPercOneri.Value, 0), par.IfEmpty(Me.txtScontoConsumo.Value, 0), par.IfEmpty(Me.txtPercIVA.Value, 0))
        '    End If
        '    myReader1.Close()


        'Catch ex As Exception
        '    par.OracleConn.Close()

        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        'End Try

    End Sub



    Protected Sub cmbfornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfornitore.SelectedIndexChanged

        Dim FlagConnessione As Boolean

        Try

            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            If Me.cmbfornitore.SelectedValue <> "-1" Then

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                par.cmd.CommandText = " select CASSA,RIT_ACCONTO,0 AS RIVALSA,(select codice from siscom_mi.tipo_ritenuta where tipo_ritenuta.id=id_tipo_ritenuta) as ritenuta,tipo from SISCOM_MI.FORNITORI where ID=" & cmbfornitore.SelectedValue
                Dim tipo As String = "0"
                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReaderT = par.cmd.ExecuteReader()

                If myReaderT.Read Then
                    tipo = par.IfNull(myReaderT("TIPO"), "0")
                    HiddenFieldTipoProfessionista.Value = tipo
                    'Me.txtPercIVA.Value = 0 'par.IfNull(myReaderT("IVA"), 0)
                    'Me.txtPercCassa.Value = par.IfNull(myReaderT("CASSA"), 0)
                    'Me.txtPercRivalsa.Value = par.IfNull(myReaderT("RIVALSA"), 0)
                    'Me.txtPercRitenuta.Value = par.IfNull(myReaderT("RIT_ACCONTO"), 0)
                    Me.codRitenuta.Value = par.IfNull(myReaderT("RITENUTA"), "301")
                    'Me.lblIVAP.Text = "IVA (" & Me.txtPercIVA.Value & "%)"
                    'Me.lblCassaP.Text = "Cassa (" & Me.txtPercCassa.Value & "%)"
                    'Me.lblRitenutaP.Text = "Rit. d'acconto (" & Me.txtPercRitenuta.Value & "%)"
                    'Me.txtIVA_PRE.Text = Me.txtPercIVA.Value
                    'Me.txtCass_PRE.Text = Me.txtPercCassa.Value
                    'Me.txtRivalsa_PRE.Text = Me.txtPercRivalsa.Value
                    'Me.txtRitenuta_PRE.Text = Me.txtPercRitenuta.Value
                    'Me.lblIVAC.Text = "IVA (" & Me.txtPercIVA.Value & "%)"
                    'Me.lblCassaC.Text = "Cassa (" & Me.txtPercCassa.Value & "%)"
                    'Me.lblRitenutaC.Text = "Rit. d'acconto (" & Me.txtPercRitenuta.Value & "%)"

                End If
                myReaderT.Close()
                abilitaCampi(tipo, Me.codRitenuta.Value)
            End If
            CalcolaImporti(par.IfEmpty(Me.txtNettoP.Text, 0), par.IfEmpty(Me.txtImponibileP.Text, 0), par.IfEmpty(Me.txtPercIVA.Value, 0), par.IfEmpty(Me.txtPercCassa.Value, 0), par.IfEmpty(Me.txtPercRitenuta.Value, 0), 0, "PRENOTATO", par.IfEmpty(Me.txtPercRivalsa.Value, 0), par.IfEmpty(Me.txtPercipiente.Value, 0), par.IfEmpty(Me.txtAzienda.Value, 0), codRitenuta.Value)
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub



    Protected Sub cmbfornitore_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfornitore.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub

    'Protected Sub ImgNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNuovo.Click
    '    Dim FlagConnessione As Boolean
    '    'carico fornitori

    '    Try

    '        FlagConnessione = False
    '        If par.OracleConn.State = Data.ConnectionState.Closed Then
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            FlagConnessione = True
    '        End If
    '        Me.cmbfornitore.Items.Clear() 'devo pulire la lista altrimenti vedo un doppio caricamento
    '        Me.cmbfornitore.Items.Add(New ListItem(" ", -1))

    '        par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(TIPO) as TIPO,trim(COD_FORNITORE) as COD_FORNITORE " _
    '                           & " from SISCOM_MI.FORNITORI where FL_BLOCCATO=0 order by RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"

    '        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

    '        Do While myReader1.Read
    '            If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
    '                If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
    '                    Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
    '                Else
    '                    Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
    '                End If
    '            Else
    '                If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
    '                    Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
    '                Else
    '                    Me.cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
    '                End If
    '            End If
    '        Loop
    '        myReader1.Close()

    '        Me.cmbfornitore.ClearSelection()
    '        If Session.Item("IDF") <> "0" And Not Session.Item("IDF") Is Nothing Then 'se lo passo dal salvataggio del nuovo fornitore
    '            cmbfornitore.Items.FindByValue(Session.Item("IDF")).Selected = True
    '        Else
    '            If Me.trovato_cmbfornitore.Value <> -1 Then ' se è stato precedentemente assegnato
    '                Me.cmbfornitore.SelectedValue = Me.trovato_cmbfornitore.Value
    '            End If
    '        End If



    '        If FlagConnessione = True Then
    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    '        End If

    '    Catch ex As Exception

    '        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

    '        par.cmd.Dispose()
    '        par.OracleConn.Close()
    '        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '        HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
    '        Page.Dispose()

    '        Session.Item("LAVORAZIONE") = "0"
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

    '    End Try

    'End Sub


    Private Sub Setta_StataoODL(ByVal Tipo As Integer)

        Me.cmbStato.Items.Clear()

        Dim item0 As RadComboBoxItem = New RadComboBoxItem
        item0.Value = 0
        item0.Text = "BOZZA"
        cmbStato.Items.Add(item0)
        Dim item1 As RadComboBoxItem = New RadComboBoxItem
        item1.Value = 1
        item1.Text = "EMESSO"
        cmbStato.Items.Add(item1)
        Dim item2 As RadComboBoxItem = New RadComboBoxItem
        item2.Value = 2
        item2.Text = "CONSUNTIVATO"
        cmbStato.Items.Add(item2)

        If Tipo = 3 Then
            Dim item3 As RadComboBoxItem = New RadComboBoxItem
            item3.Value = 3
            item3.Text = "INTEGRATO"
            cmbStato.Items.Add(item3)

            ' cmbStato.Items.Add(New ListItem("INTEGRATO", 3))
        End If

        If Tipo = 4 Then
            Dim item4 As RadComboBoxItem = New RadComboBoxItem
            item4.Value = 4
            item4.Text = "EMESSO PAGAMENTO"
            cmbStato.Items.Add(item4)

            '  cmbStato.Items.Add(New ListItem("EMESSO PAGAMENTO", 4))
        End If

        If Tipo = 5 Then
            Dim item5 As RadComboBoxItem = New RadComboBoxItem
            item5.Value = 5
            item5.Text = "ANNULLATO"
            cmbStato.Items.Add(item5)

            '    cmbStato.Items.Add(New ListItem("ANNULLATO", 5))
        End If

        cmbStato.SelectedValue = 0



    End Sub



    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)

        Me.cmbStato.Enabled = False
        Me.cmbfornitore.Enabled = False

        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = True
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
        Next
        cmbStato.Enabled = False
        cmbfornitore.Enabled = False
        ImgAllegaFile.Enabled = False

    End Sub
    'Protected Sub ImgStampa_Click(sender As Object, e As System.EventArgs) Handles ImgStampa.Click
    '       'STAMPA IMPEGNO DI SPESA
    '       CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


    '       StampaSpesa()


    '   End Sub

    Protected Sub ImgStampaPag_Click(sender As Object, e As System.EventArgs) Handles ImgStampaPag.Click
        'STAMPA PAGAMENTO
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        Dim FlagConnessione As Boolean
        Try
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

        If Me.txtElimina.Value = "1" Then
                Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO in (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdODL & " ORDER BY ID DESC"
                Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                If String.IsNullOrEmpty(nome) Then
                    HiddenFieldRielabPagam.Value = "0"
            If StampaPagamento() = True Then
                Setta_StataoODL(4)  'EMESSO PAGAMENTO
                Me.cmbStato.SelectedValue = 4
                FrmSolaLettura()
            End If
        Else

                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../ALLEGATI/ORDINI/" & nome & "','AttPagamento','');self.close();", True)
                End If
            Else
            CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
        End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    Protected Sub ImgStampa_Click(sender As Object, e As System.EventArgs) Handles ImgStampa.Click
        'STAMPA IMPEGNO DI SPESA
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        ' If Me.txtElimina.Value = "1" Then
        StampaSpesa()
        ' Else
        ' CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
        ' End If

    End Sub

    Private Function StampaPagamento() As Boolean
        Dim risultatoRIT As Decimal = 0
        Dim sData As String = ""
        Dim sStr1 As String
        StampaPagamento = False
        Try

            '' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            If IsNothing(par.myTrans) Then

                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            Else
                '‘par.cmd.Transaction = par.myTrans
            End If
            '***********************************************

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader

            If vIdPagamenti <> 0 Then
                'MODIFICA TAB PAGAMENTI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                                    & " set DESCRIZIONE=:descrizione, IMPORTO_CONSUNTIVATO=:importo,ID_FORNITORE=:id_fornitore,CONTO_CORRENTE=:conto_corrente " _
                                    & " where ID=" & vIdPagamenti

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                If CDbl(par.IfEmpty(Me.txtConsuntivatoLordoRIT.Value, 0)) > 0 Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                    'risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0) - par.IfEmpty(Me.txtRitenutaC.Text, 0)
                    If HiddenFieldTipoProfessionista.Value.ToString.ToUpper = "P" Then
                        risultatoRIT = CDec(par.IfEmpty(Me.txtDaPagareC.Text, 0)) + CDec(par.IfEmpty(Me.txtIVAC.Text, 0))
                    Else
                    risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)
                    End If
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", risultatoRIT))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                End If

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imponibile", strToNumber(Me.txtImponibileC.Text.Replace(".", ""))))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))


                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
            Else
                If par.AggiustaData(Me.txtDScad.Text) < Format(Now, "yyyyMMdd") Then
                    RadWindowManager1.RadAlert("La data scadenza del pagamento non può essere inferiore a quella odierna!", 300, 150, "Attenzione", "", "null")
                    Exit Function
                End If
                'CREAZIONE TAB PAGAMENTI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM dual "
                myReaderT = par.cmd.ExecuteReader()
                If myReaderT.Read Then
                    vIdPagamenti = myReaderT(0)
                End If
                myReaderT.Close()
                Dim idTipoPagamento As String = "null"
                Dim idModPagamento As String = "null"
                Dim idIbanFornitore As Integer = 0
                par.cmd.CommandText = "SELECT FORNITORI.*, (select id from siscom_mi.fornitori_iban where id_fornitore = fornitori.id and fl_attivo = 1) as id_iban " _
                                   & "FROM SISCOM_MI.FORNITORI WHERE id = " & Me.cmbfornitore.SelectedValue
                myReaderT = par.cmd.ExecuteReader
                If myReaderT.Read Then
                    idModPagamento = par.IfNull(myReaderT("ID_TIPO_MODALITA_PAG"), -1)
                    idTipoPagamento = par.IfNull(myReaderT("ID_TIPO_PAGAMENTO"), -1)
                    idIbanFornitore = par.IfNull(myReaderT("id_iban"), -1)
                End If
                myReaderT.Close()

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI " _
                                            & " (ID,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_STATO,TIPO_PAGAMENTO,DATA_EMISSIONE,CONTO_CORRENTE,ID_IBAN_FORNITORE,data_scadenza,ID_TIPO_PAGAMENTO,ID_TIPO_MODALITA_PAG) " _
                                    & "values (:id,:descrizione,:importo,:id_fornitore,:id_stato,:tipo_pagamento,:data,:conto_corrente,:id_iban_forn,:dat_scad_pag,:id_t_pag,:id_t_modalita)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamenti))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                If CDbl(par.IfEmpty(Me.txtConsuntivatoLordoRIT.Value, 0)) > 0 Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                ElseIf CDbl(par.IfEmpty(Me.txtLordoC.Text, 0)) > 0 Then
                    'risultatoRIT = par.IfEmpty(Me.txtLordoC.Text, 0) - par.IfEmpty(Me.txtRitenutaC.Text, 0)
                    If HiddenFieldTipoProfessionista.Value.ToString.ToUpper = "P" Then
                        risultatoRIT = CDec(par.IfEmpty(Me.txtDaPagareC.Text, 0)) + CDec(par.IfEmpty(Me.txtIVAC.Text, 0))
                    Else
                    risultatoRIT = par.IfEmpty(Me.txtDaPagareC.Text, 0)
                    End If
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", risultatoRIT))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtConsuntivatoLordoRIT.Value.Replace(".", ""))))
                End If

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(idVoce)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 4))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imponibile", strToNumber(Me.txtImponibileP.Text.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMdd")))
                HiddenFieldDataEmissione.Value = Format(Now, "yyyyMMdd")
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_iban_forn", par.IfEmpty(idIbanFornitore, 0)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("dat_scad_pag", par.IfEmpty(par.AggiustaData(Me.txtDScad.Text), Format(Now, "yyyyMMdd"))))

                If idTipoPagamento = "-1" Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_t_pag", DBNull.Value))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_t_pag", par.IfEmpty(idTipoPagamento, DBNull.Value)))
                End If

                If idModPagamento = "-1" Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_t_modalita", DBNull.Value))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_t_modalita", par.IfEmpty(idModPagamento, DBNull.Value)))
                End If
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_t_pag", par.IfEmpty(idTipoPagamento, "null")))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

            End If

            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            If IsNumeric(txtRitenuta_CONS.Text) AndAlso CDec(txtRitenuta_CONS.Text) <> 0 Then
                If vIdPagamentiRIT <> 0 Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                                        & " set DESCRIZIONE=:descrizione, IMPORTO_CONSUNTIVATO=:importo,ID_FORNITORE=:id_fornitore,CONTO_CORRENTE=:conto_corrente " _
                                        & " where ID=" & vIdPagamentiRIT

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))


                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                Else
                    'CREAZIONE TAB PAGAMENTI TIPO 5
                    par.cmd.CommandText = " select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM dual "
                    myReaderT = par.cmd.ExecuteReader()
                    If myReaderT.Read Then
                        vIdPagamentiRIT = myReaderT(0)
                    End If
                    myReaderT.Close()

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI " _
                                                & " (ID,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_STATO,TIPO_PAGAMENTO,DATA_EMISSIONE,CONTO_CORRENTE,DATA_STAMPA) " _
                                        & "values (:id,:descrizione,:importo,:id_fornitore,:id_stato,:tipo_pagamento,:data,:conto_corrente,:data_stampa)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamentiRIT))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.PulisciStrSql(Strings.Left(Me.txtDescrizioneP.Text, 2000))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbfornitore.SelectedValue))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 1))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 5))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", Format(Now, "yyyyMMdd")))
                    HiddenFieldDataEmissione.Value = Format(Now, "yyyyMMdd")
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", DBNull.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_stampa", Format(Now, "yyyyMMdd")))


                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                                        & " set IMPORTO_APPROVATO=:importo,ID_PAGAMENTO=:id_pagamento,ID_STATO=:id_stato " _
                                        & " where ID=" & vIdPrenotazioniRIT

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtRitenutaC.Text.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_pagamento", vIdPagamentiRIT))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_stato", 2))


                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()

                End If
            End If

            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select DATA_STAMPA from SISCOM_MI.PAGAMENTI where PAGAMENTI.ID=" & vIdPagamenti
            myReaderS = par.cmd.ExecuteReader

            If myReaderS.Read Then
                sData = par.IfNull(myReaderS(0), "")
            End If
            myReaderS.Close()


            '' ID_STATO=1       (TAB_STATI_PAGAMENTI.ID =0=PRENOTATO, 1 EMESSO, 5=PAGATO)
            If sData = "" Then

                'PAGAMENTI
                par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set DATA_STAMPA='" & Format(Now, "yyyyMMdd") & "'" _
                                  & " where ID=" & vIdPagamenti

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '********************************************

                'CAMBIO ID_STATO all' ODL da CONSUNTIVATO=2 a EMESSO PAGAMENTO=4
                par.cmd.CommandText = "update SISCOM_MI.ODL set ID_STATO=4,ID_PAGAMENTO=" & vIdPagamenti _
                                  & " where ID=" & vIdODL

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '********************************************

                'CAMBIO ID_STATO alle prenotazioni
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=2,ID_PAGAMENTO=" & vIdPagamenti _
                                  & " where ID=" & vIdPrenotazioni

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '********************************************



                '****Scrittura evento EMISSIONE DEL PAGAMENTO
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & vIdPagamenti & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                RadNotificationNote.Text = "Il pagamento è stato emesso e storicizzato!"
                RadNotificationNote.AutoCloseDelay = "500"
                RadNotificationNote.Show()

                'Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")

            End If

            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F90','Stampa Modulo di Pagamento')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            par.myTrans.Commit() 'COMMIT
            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID = " & vIdODL & " FOR UPDATE NOWAIT"
            myReaderT = par.cmd.ExecuteReader()
            myReaderT.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            '''  par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoODL.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'PAGAMENTI.IMPORTO_NO_IVA
            'PRENOTAZIONI.ID_VOCE_PF
            '& " and PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO (+) " 
            '& "  and PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID (+) "
            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(vIdPagamenti))

            sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_STAMPA,PAGAMENTI.DESCRIZIONE,PAGAMENTI.CONTO_CORRENTE," _
                        & " ODL.CONS_NETTO,ODL.IVA_CONS,ODL.CASSA_CONS,ODL.RIT_ACCONTO_CONS,ODL.CONS_NO_IVA,ODL.PENALE,ODL.RIVALSA_CONS,ODL.PERCIPIENTE_CONS,ODL.AZIENDA_CONS,ODL.COD_RITENUTA,FORNITORI.TIPO AS TIPO_FORNITORE," _
                        & " FORNITORI.ID as ""ID_FORNITORE"",FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE," _
                        & " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"",pagamenti.ID_TIPO_MODALITA_PAG as idModalpag,pagamenti.ID_TIPO_PAGAMENTO as id_tipo_pag,SISCOM_MI.GETDATA(PAGAMENTI.DATA_SCADENZA) AS D_SCAD " _
                 & " from  SISCOM_MI.PAGAMENTI" _
                      & " ,SISCOM_MI.FORNITORI" _
                      & " ,SISCOM_MI.PF_VOCI" _
                      & " ,SISCOM_MI.ODL" _
                 & " where   PAGAMENTI.ID=" & vIdPagamenti _
                     & " and PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                     & " and PAGAMENTI.ID=ODL.ID_PAGAMENTO (+) " _
                     & " and ODL.ID_VOCE_PF=PF_VOCI.ID (+) "

            Dim ibanFornitore As String = "- - -"
            par.cmd.CommandText = "select iban from siscom_mi.fornitori_iban where ID  = (select id_IBAN_FORNITORE from siscom_mi.pagamenti where id = " & vIdPagamenti & ")"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                ibanFornitore = par.IfNull(myReader1("iban"), "- - -")
            End If
            myReader1.Close()


            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then

                'PAGAMENTI
                contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                contenuto = Replace(contenuto, "$progr$", myReader1("PROGR"))

                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "-1")))
                HiddenFieldDataEmissione.Value = par.IfNull(myReader1("DATA_EMISSIONE"), "-1")
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))

                contenuto = Replace(contenuto, "$conto_corrente$", par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01"))

                'FORNITORI
                Dim sFORNITORI As String = ""
                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        sFORNITORI = par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                    Else
                        sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), "")
                    End If

                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        sFORNITORI = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                    Else
                        sFORNITORI = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                    End If
                End If


                par.cmd.CommandText = "select iban,tipo from siscom_mi.fornitori_iban where ID  = (select id_IBAN_FORNITORE from siscom_mi.pagamenti where id = " & vIdPagamenti & ")"

                'IBAN **************************************************
                'par.cmd.CommandText = "select IBAN from SISCOM_MI.FORNITORI_IBAN " _
                '                   & " where ID_FORNITORE=" & par.IfNull(myReader1("ID_FORNITORE"), 0)
                par.cmd.CommandText = "select iban from siscom_mi.fornitori_iban where ID  = (select id_IBAN_FORNITORE from siscom_mi.pagamenti where id = " & vIdPagamenti & ")"
                Dim myReaderBP As Oracle.DataAccess.Client.OracleDataReader
                myReaderBP = par.cmd.ExecuteReader
                While myReaderBP.Read
                    sFORNITORI = sFORNITORI & "<br/>" & par.IfNull(myReaderBP("IBAN"), "")
                End While
                myReaderBP.Close()
                contenuto = Replace(contenuto, "$fornitori$", sFORNITORI)
                '*********************************************************


                'ris1) netto
                'ris2) rivalsa=rivalsa% su ris1             ris1*RIVALSA/100
                'ris3) cassa=cassa% su ris1                 ris1*CASSA/100
                'ris4) iva=iva% su ris1)+ris2)+ris3)        (ris1+ris2+ris3)*IVA/100
                'ris5) rit=rit% su ris1)+ris2)              (ris1+ris2)*RIT/100
                'se ritenuta in (001,002,010,012)
                '       ris9) lordo=ris1+ris2+ris3+ris4+ris6
                '       ris10) daPagare=ris9-ris5
                'else
                '       ris9) lordo=ris1+ris2+ris3+ris4+ris6+ris8
                '       ris10) daPagare=ris1+ris2+ris3+ris4+ris6-ris5-ris7

                Dim tipo As String = par.IfNull(myReader1("TIPO_FORNITORE"), 0)
                Dim penale As String = par.IfNull(myReader1("PENALE"), 0)
                Dim netto As Decimal = par.IfNull(myReader1("cons_NETTO"), 0)
                Dim rivalsa As Decimal = par.IfNull(myReader1("RIVALSA_cons"), 0)
                Dim cassa As Decimal = par.IfNull(myReader1("CASSA_cons"), 0)
                Dim iva As Decimal = par.IfNull(myReader1("IVA_cons"), 0)
                Dim ritenuta As Decimal = par.IfNull(myReader1("RIT_ACCONTO_CONS"), 0)
                Dim imponibile As Decimal = par.IfNull(myReader1("CONS_NO_IVA"), 0)
                Dim percipiente As Decimal = par.IfNull(myReader1("PERCIPIENTE_CONS"), 0)
                Dim azienda As Decimal = par.IfNull(myReader1("AZIENDA_CONS"), 0)
                Dim codRitenuta As Decimal = par.IfNull(myReader1("COD_RITENUTA"), 0)
                Dim ris1 As Decimal = netto - penale
                Dim ris2 As Decimal = Round(ris1 * rivalsa / 100, 2)
                Dim ris3 As Decimal = Round(ris1 * cassa / 100, 2)
                Dim ris4 As Decimal = Round((ris1 + ris2 + ris3) * iva / 100, 2)
                Dim ris5 As Decimal = (ris1 + ris2) * ritenuta / 100
                Dim ris6 As Decimal = imponibile
                Dim ris7 As Decimal = percipiente
                Dim ris8 As Decimal = azienda
                Dim ris9 As Decimal = 0
                Dim ris10 As Decimal = 0
                Dim dataEmissione As String = HiddenFieldDataEmissione.Value
                If dataEmissione = "0" Then
                    dataEmissione = Format(Now, "yyyyMMdd")
                End If
                If codRitenuta = "301" Then
                    ris9 = ris1 + ris2 + ris3 + ris4 + ris6 + ris8
                    If dataEmissione >= "20170701" And dataEmissione < "20180715" Then
                        '16/11/2017
                        'lascio scritto ris4-ris4 per ricordarci della modifica
                        ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7 - ris4
                    Else
                    ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7
                    End If
                Else
                    ris9 = ris1 + ris2 + ris3 + ris4 + ris6
                    If dataEmissione >= "20170701" And dataEmissione < "20180715" Then
                        ris10 = ris9 - ris5 - ris4
                    Else
                    ris10 = ris9 - ris5
                End If
                End If

                ''ODL
                'Dim cassa, iva, ritenuta, imponibile, netto, lordo As Decimal
                'Dim risultato1 As Decimal
                '
                ''A) netto
                ''B) cassa=cassa% su A (netto*CASSA)/100
                ''C) risultato1=A+B
                ''D) iva=iva% su C (risultato1*iva)/100
                ''E) TOTALE FATTURA= C+D + IMPONIBILE
                ''F) RITENUTA ACCONTO= ritenuta% su A (netto*ritenuta)/100
                '
                '
                ''A) IMPORTO
                'netto = par.IfNull(myReader1("CONS_NETTO"), 0)
                'netto = netto - par.IfNull(myReader1("PENALE"), 0)
                '
                'imponibile = par.IfNull(myReader1("CONS_NO_IVA"), 0)
                '
                'iva = par.IfNull(myReader1("IVA_CONS"), 0)
                'cassa = par.IfNull(myReader1("CASSA_CONS"), 0)
                'ritenuta = par.IfNull(myReader1("RIT_ACCONTO_CONS"), 0)
                '
                '
                ''B) CASSA
                'cassa = (netto * cassa) / 100
                'cassa = Round(cassa, 2)
                '
                ''C) A+B
                'risultato1 = netto + cassa
                '
                ''D)
                'iva = (risultato1 * iva) / 100
                'iva = Round(iva, 2)
                '
                ''E)  C+D ovvero A+B+D + IMPONIBILE
                'lordo = netto + cassa + iva + imponibile
                '
                ''F) RITENUTA
                'ritenuta = (netto * ritenuta) / 100
                'ritenuta = Round(ritenuta, 2)

                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(ris9, 0), "", "##,##0.00"))

                Dim S2 As String = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                S2 = S2 & "<td style='text-align: left; width:45%;  font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Importo Netto €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(myReader1("CONS_NETTO"), 0), "", "##,##0.00") & "</td>"

                If par.IfNull(myReader1("PENALE"), 0) > 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Penale  €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(myReader1("PENALE"), 0), "", "##,##0.00") & "</td>"
                End If

                If rivalsa <> 0 And tipo = "P" Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Rivalsa INPS (" & par.IfNull(myReader1("RIVALSA_CONS"), 0) & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris2, 0), "", "##,##0.00") & "</td>"
                End If

                If cassa <> 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Cassa di Previdenza (" & par.IfNull(myReader1("CASSA_CONS"), 0) & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris3, 0), "", "##,##0.00") & "</td>"
                End If

                If iva <> 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>IVA (" & par.IfNull(myReader1("IVA_CONS"), 0) & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris4, 0), "", "##,##0.00") & "</td>"
                End If

                If imponibile <> 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Imponibile non soggetta a IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(imponibile, 0), "", "##,##0.00") & "</td>"
                End If

                If percipiente <> 0 And tipo = "P" Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>INPS carico Percipiente €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris7, 0), "", "##,##0.00") & "</td>"
                End If

                If azienda <> 0 And tipo = "P" Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>INPS carico Azienda €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris8, 0), "", "##,##0.00") & "</td>"
                End If

                Dim daPagare As String = ""
                If tipo = "P" Then
                    daPagare = "</tr><tr>"
                    daPagare &= "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    daPagare &= "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Da Pagare €</td>"
                    daPagare &= "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(txtDaPagareC.Text, "", "##,##0.00") & "</td>"
                End If

                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Importo Lordo €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris9, 0), "", "##,##0.00") & "</td>"

                If ritenuta <> 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>di cui Rit. Acconto (" & par.IfNull(myReader1("RIT_ACCONTO_CONS"), 0) & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris5, 0), "", "##,##0.00") & "</td>"
                End If

                S2 = S2 & daPagare

                S2 = S2 & "</tr></table>"


                Dim T As String = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$dettagli$", T)
                ''****************************

                'Dim T = "<table style='width:100%;'>"

                'T = T & "</tr>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Netto €</td>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(netto, "", "##,##0.00") & "</td>"

                'T = T & "</tr><tr>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Cassa di Previdenza €</td>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(cassa, "", "##,##0.00") & "</td>"

                'T = T & "</tr><tr>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(iva, "", "##,##0.00") & "</td>"

                'T = T & "</tr><tr>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Rit. Accanto €</td>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenuta, "", "##,##0.00") & "</td>"

                'T = T & "</tr><tr>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile non soggetta a IVA €</td>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(imponibile, "", "##,##0.00") & "</td>"

                'T = T & "</tr><tr>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Lordo €</td>"
                'T = T & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(lordo, "", "##,##0.00") & "</td>"

                ''testomiaTable = testomiaTable & "<tr><td style='width:100%'>Importo Netto €.</td>"
                ''testomiaTable = testomiaTable & "<td style='width: 22px'>" & IsNumFormat(importo, "", "##,##0.00") & "</td>"
                ''testomiaTable = testomiaTable & "<td style='width: 22px'>IVA</td>"

                'T = T & "</tr>"
                'T = T & "</table>"

                'Dim S As String = "<table style='width:100%;'>"
                'S = S & "<tr>"
                'S = S & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                'S = S & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & T & "</td>"
                ''S = S & "</tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Netto €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importo, "", "##,##0.00") & "</td>"
                ''S = S & "</tr><tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importo, "", "##,##0.00") & "</td>"
                ''S = S & "</tr><tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Lordo €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(asta, "", "##,##0.00") & "</td>"
                ''S = S & "</tr><tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2, "", "##,##0.00") & "</td>"
                ''S = S & "</tr><tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3, "", "##,##0.00") & "</td>"
                ''S = S & "</tr><tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(iva, "", "##,##0.00") & "</td>"
                ''S = S & "</tr><tr>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Lordo €</td>"
                ''S = S & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4, "", "##,##0.00") & "</td>"
                'S = S & "</tr></table>"

                'contenuto = Replace(contenuto, "$dettagli$", S)
                'End If
                'myReaderT.Close()
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))
                'contenuto = Replace(contenuto, "$dettaglio$", "SPESE")
                Dim modalita As String
                Dim condizione As String

                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_modalita_pag where id =" & par.IfNull(myReader1("idModalpag"), -1)
                modalita = par.IfNull(par.cmd.ExecuteScalar, "")
                par.cmd.CommandText = "select descrizione from siscom_mi.tipo_pagamento where id =" & par.IfNull(myReader1("id_tipo_pag"), -1)
                condizione = par.IfNull(par.cmd.ExecuteScalar, "")
                contenuto = Replace(contenuto, "$modalita$", par.IfEmpty(modalita, "n.d."))
                contenuto = Replace(contenuto, "$condizione$", par.IfEmpty(condizione, "n.d."))
                contenuto = Replace(contenuto, "$dscadenza$", par.IfNull(myReader1("D_SCAD"), "---"))



                contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("COD_VOCE"), ""))
                contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESC_VOCE"), ""))
                'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")
                par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim chiamante2 As String = ""
                If lett.Read Then
                    chiamante2 = par.IfNull(lett(0), "")
                End If
                lett.Close()

                contenuto = Replace(contenuto, "$chiamante2$", chiamante2) ' EX "UFFICIO CONTABILITA' E RENDICONTAZIONE"
                contenuto = Replace(contenuto, "$grigliaM$", "")

            End If
            myReader1.Close()







            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
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
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""

            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            nomefile = par.NomeFileManut("ODP", vIdPagamenti) & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))



            Me.txtModificato.Text = "0"
            Me.txtStatoPagamento.Value = 4


            'GIANCARLO 16-02-2017
            'inserimento della stampa cdp negli allegati
            Dim descrizione As String = "Stampa pagamento"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
            'Imposto le vecchie rielaborazioni a 2...per barrare il nome
            par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                & "SET STATO = 2 " _
                                & "WHERE " _
                                & "TIPO = " & idTipoOggetto _
                                & "AND ID_OGGETTO = " & vIdODL
            par.cmd.ExecuteNonQuery()
            If HiddenFieldRielabPagam.Value = "1" Then
                descrizione = "Stampa rielaborazione pagamento"
                idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
                'Imposto le vecchie rielaborazioni a 2...per barrare il nome
                par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                    & "SET STATO = 2 " _
                                    & "WHERE " _
                                    & "TIPO = " & idTipoOggetto _
                                    & "AND ID_OGGETTO = " & vIdODL
                par.cmd.ExecuteNonQuery()
            End If
            par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
            Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
            
            par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, vIdODL, "../../../ALLEGATI/ORDINI/")

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();", True)
            StampaPagamento = True
            par.myTrans.Commit() 'COMMIT
            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID = " & vIdODL & " FOR UPDATE NOWAIT"
            myReaderT = par.cmd.ExecuteReader()
            myReaderT.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Function

    Sub StampaSpesa()
        Dim sData As String = ""
        Dim sStr1 As String

        Try

            '' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            If IsNothing(par.myTrans) Then

                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            Else
                '‘par.cmd.Transaction = par.myTrans
            End If
            '***********************************************


            Dim myReaderS As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select DATA_STAMPA from SISCOM_MI.PRENOTAZIONI where PRENOTAZIONI.ID=" & vIdPrenotazioni
            myReaderS = par.cmd.ExecuteReader

            If myReaderS.Read Then
                sData = par.IfNull(myReaderS(0), "")
            End If
            myReaderS.Close()


            '' ID_STATO=1       (TAB_STATI_PAGAMENTI.ID =0=PRENOTATO, 1 EMESSO, 5=PAGATO)
            If sData = "" Then

                'PRENOTAZIONI
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_STAMPA='" & Format(Now, "yyyyMMdd") & "'" _
                                  & "  where ID=" & vIdPrenotazioni

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '********************************************

                '****Scrittura evento EMISSIONE DEL PAGAMENTO in ODL
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                RadNotificationNote.Text = "Impegno di spesa emesso e storicizzato!"
                RadNotificationNote.AutoCloseDelay = "500"
                RadNotificationNote.Show()

            End If

            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_ODL (ID_ODL,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdODL & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F90','Stampa Impegno di Spesa e Ordine di Lavoro')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""


            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""


            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloSpesa.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            'PF_VOCI_IMPORTO.ID as ""ID_VOCI""

            sStr1 = "select ODL.ANNO,ODL.PROGR,PRENOTAZIONI.DATA_PRENOTAZIONE,PRENOTAZIONI.DATA_STAMPA,PRENOTAZIONI.DESCRIZIONE," _
                        & " ODL.ID as ID_ODL,ODL.PREN_NETTO,ODL.IVA,ODL.CASSA,ODL.RIT_ACCONTO,ODL.PREN_NO_IVA,ODL.RIVALSA,ODL.PERCIPIENTE,ODL.AZIENDA,ODL.COD_RITENUTA," _
                        & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE," _
                        & " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"" " _
                 & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.FORNITORI,SISCOM_MI.PF_VOCI,SISCOM_MI.ODL " _
                 & " where   PRENOTAZIONI.ID=" & vIdPrenotazioni _
                     & " and PRENOTAZIONI.ID_FORNITORE=FORNITORI.ID (+) " _
                     & "  and PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) " _
                     & "  and PRENOTAZIONI.ID=ODL.ID_PRENOTAZIONE (+) "

            '& " and PAGAMENTI.ID_VOCE_PF=PF_VOCI_IMPORTO.ID (+) " 
            '& " and PF_VOCI_IMPORTO.ID_VOCE=PF_VOCI.ID (+) "
            '& "  and PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID (+) " 


            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then

                'REFERENTE
                par.cmd.CommandText = "select COGNOME,NOME from SEPA.OPERATORI " _
                                   & " where ID=(select DISTINCT(ID_OPERATORE) from SISCOM_MI.EVENTI_ODL " _
                                             & " where ID_ODL=" & par.IfNull(myReader1("ID_ODL"), -1) _
                                             & "   and COD_EVENTO='F97')"
                myReaderS = par.cmd.ExecuteReader
                If myReaderS.Read Then
                    contenuto = Replace(contenuto, "$chiamante$", "REFERENTE:")
                    contenuto = Replace(contenuto, "$dettagli_chiamante$", par.IfNull(myReaderS("COGNOME"), "") & " - " & par.IfNull(myReaderS("NOME"), ""))
                Else
                    contenuto = Replace(contenuto, "$chiamante$", "REFERENTE:")
                    contenuto = Replace(contenuto, "$dettagli_chiamante$", "")

                End If
                myReaderS.Close()


                contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), "")) 'myReader1("PROGR_FORNITORE"))

                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_PRENOTAZIONE"), "-1")))



                contenuto = Replace(contenuto, "$IBAN_TITOLO$", "")
                contenuto = Replace(contenuto, "$iban$", "") 'myReader1("IBAN"))

                'FORNITORI
                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    End If

                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitori$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    End If
                End If


                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))



                'ris1) netto
                'ris2) rivalsa=rivalsa% su ris1             ris1*RIVALSA/100
                'ris3) cassa=cassa% su ris1                 ris1*CASSA/100
                'ris4) iva=iva% su ris1)+ris2)+ris3)        (ris1+ris2+ris3)*IVA/100
                'ris5) rit=rit% su ris1)+ris2)              (ris1+ris2)*RIT/100
                'se ritenuta in (001,002,010,012)
                '       ris9) lordo=ris1+ris2+ris3+ris4+ris6
                '       ris10) daPagare=ris9-ris5
                'else
                '       ris9) lordo=ris1+ris2+ris3+ris4+ris6+ris8
                '       ris10) daPagare=ris1+ris2+ris3+ris4+ris6-ris5-ris7

                Dim netto As Decimal = par.IfNull(myReader1("PREN_NETTO"), 0)
                Dim rivalsa As Decimal = par.IfNull(myReader1("RIVALSA"), 0)
                Dim cassa As Decimal = par.IfNull(myReader1("CASSA"), 0)
                Dim iva As Decimal = par.IfNull(myReader1("IVA"), 0)
                Dim ritenuta As Decimal = par.IfNull(myReader1("RIT_ACCONTO"), 0)
                Dim imponibile As Decimal = par.IfNull(myReader1("PREN_NO_IVA"), 0)
                Dim percipiente As Decimal = par.IfNull(myReader1("PERCIPIENTE"), 0)
                Dim azienda As Decimal = par.IfNull(myReader1("AZIENDA"), 0)
                Dim codRitenuta As Decimal = par.IfNull(myReader1("COD_RITENUTA"), 0)
                Dim ris1 As Decimal = netto
                ris1 = netto
                Dim ris2 As Decimal = Round(ris1 * rivalsa / 100, 2)
                Dim ris3 As Decimal = Round(ris1 * cassa / 100, 2)
                Dim ris4 As Decimal = Round((ris1 + ris2 + ris3) * iva / 100, 2)
                Dim ris5 As Decimal = (ris1 + ris2) * ritenuta / 100
                Dim ris6 As Decimal = imponibile
                Dim ris7 As Decimal = percipiente
                Dim ris8 As Decimal = azienda
                Dim ris9 As Decimal = 0
                Dim ris10 As Decimal = 0
                Dim dataEmissione As String = HiddenFieldDataEmissione.Value
                If dataEmissione = "0" Then
                    dataEmissione = Format(Now, "yyyyMMdd")
                End If
                If codRitenuta = "301" Then
                    ris9 = ris1 + ris2 + ris3 + ris4 + ris6 + ris8
                    If dataEmissione >= "20170701" And dataEmissione < "20180715" Then
                        '16/11/2017
                        'lascio scritto ris4-ris4 per ricordarci della modifica
                        ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7 - ris4
                    Else
                    ris10 = ris1 + ris2 + ris3 + ris4 + ris6 - ris5 - ris7
                    End If
                Else
                    ris9 = ris1 + ris2 + ris3 + ris4 + ris6
                    If dataEmissione >= "20170701" And dataEmissione < "20180715" Then
                        ris10 = ris9 - ris5 - ris4
                    Else
                    ris10 = ris9 - ris5
                End If
                End If

                'Dim cassa, iva, ritenuta, imponibile, netto, lordo, rivalsa, percipiente, azienda As Decimal
                'Dim risultato1 As Decimal
                '
                ''A) netto
                ''B) cassa=cassa% su A (netto*CASSA)/100
                ''C) risultato1=A+B
                ''D) iva=iva% su C (risultato1*iva)/100
                ''E) TOTALE FATTURA= C+D + IMPONIBILE
                ''F) RITENUTA ACCONTO= ritenuta% su A (netto*ritenuta)/100
                '
                '
                ''A) IMPORTO
                'netto = par.IfNull(myReader1("PREN_NETTO"), 0)
                'imponibile = par.IfNull(myReader1("PREN_NO_IVA"), 0)
                '
                'iva = par.IfNull(myReader1("IVA"), 0)
                'cassa = par.IfNull(myReader1("CASSA"), 0)
                'ritenuta = par.IfNull(myReader1("RIT_ACCONTO"), 0)
                '
                '
                ''B) CASSA
                'cassa = (netto * cassa) / 100
                'cassa = Round(cassa, 2)
                '
                ''C) A+B
                'risultato1 = netto + cassa
                '
                ''D)
                'iva = (risultato1 * iva) / 100
                'iva = Round(iva, 2)
                '
                '
                ''E)  C+D ovvero A+B+D + IMPONIBILE
                'lordo = netto + cassa + iva + imponibile
                '
                ''F) RITENUTA
                'ritenuta = (netto * ritenuta) / 100
                'ritenuta = Round(ritenuta, 2)

                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(ris9, 0), "", "##,##0.00"))

                Dim S2 As String = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>Importo Lordo Prenotato €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris9, 0), "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Cassa di Previdenza (" & par.IfNull(myReader1("CASSA"), 0) & "%) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(cassa, 0), "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA (" & par.IfNull(myReader1("IVA"), 0) & "%) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(iva, 0), "", "##,##0.00") & "</td>"

                If ritenuta <> 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:45%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:35%; font-size:14pt;font-family :Arial ;'>di cui Rit. Acconto (" & par.IfNull(myReader1("RIT_ACCONTO"), 0) & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(ris5, 0), "", "##,##0.00") & "</td>"
                End If

                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile non soggetta a IVA €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(imponibile, 0), "", "##,##0.00") & "</td>"
                'S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Importo Lordo €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(par.IfNull(lordo, 0), "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr></table>"


                Dim T As String = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$dettagli$", T)

                contenuto = Replace(contenuto, "$dettagli2$", par.IfNull(myReader1("DESCRIZIONE"), ""))

                contenuto = Replace(contenuto, "$pagina$", "<p style='page-break-before: always'>&nbsp;</p>")


                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))

                'contenuto = Replace(contenuto, "$dettaglio$", "SPESE")

                contenuto = Replace(contenuto, "$cod_capitolo$", par.IfNull(myReader1("COD_VOCE"), ""))
                contenuto = Replace(contenuto, "$voce_pf$", par.IfNull(myReader1("DESC_VOCE"), ""))
                'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")


                contenuto = Replace(contenuto, "$chiamante2$", "") ' EX "UFFICIO CONTABILITA' E RENDICONTAZIONE"

                Dim TestoGrigliaM As String = "<p style='page-break-before: always'>&nbsp;</p>"


            End If
            myReader1.Close()


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.ODL  where ID = " & vIdODL & " FOR UPDATE NOWAIT"
            myReaderS = par.cmd.ExecuteReader()
            myReaderS.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            Me.txtModificato.Text = "0"

            If Me.txtStatoPagamento.Value < 2 Then
                Me.txtStatoPagamento.Value = 2
            End If

            Dim url As String = Server.MapPath("..\..\..\FileTemp\")
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
            pdfConverter1.PdfFooterOptions.FooterText = ("")
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Blue
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = ""

            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            nomefile = par.NomeFileManut("PRENODP", vIdPrenotazioni) & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

            'Dim i As Integer = 0
            'For i = 0 To 10000
            'Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');", True)
            ' Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();</script>")



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub
    Private Sub abilitaCampi(ByVal tipo As String, ByVal ritenuta As String)
        If tipo <> "P" Then
            txtRivalsaP.Enabled = False
            txtRivalsaP.Text = "0,00"
            txtRivalsa_PRE.Enabled = False
            txtRivalsa_PRE.Text = "0,00"
            txtRivalsaC.Enabled = False
            txtRivalsaC.Text = "0,00"
            txtRivalsa_CONS.Enabled = False
            txtRivalsa_CONS.Text = "0,00"
            txtPercRivalsa.Value = "0"
            txtPercRivalsa_C.Value = "0"

            txtIMPScaricoPercipiente.Enabled = False
            txtIMPScaricoPercipiente.Text = "0,00"
            txtIMPScaricoPercipienteC.Enabled = False
            txtIMPScaricoPercipienteC.Text = "0,00"
            txtPercipiente.Value = "0"
            txtPercipiente_C.Value = "0"

            txtIMPScaricoAzienda.Enabled = False
            txtIMPScaricoAzienda.Text = "0,00"
            txtIMPScaricoAziendaC.Enabled = False
            txtIMPScaricoAziendaC.Text = "0,00"
            txtAzienda.Value = "0"
            txtAzienda_C.Value = "0"

            txtCass_CONS.Enabled = False
            txtCass_PRE.Enabled = False
            txtRitenuta_PRE.Enabled = False
            txtRitenuta_CONS.Enabled = False

        Else

            txtCass_CONS.Enabled = True
            txtCass_PRE.Enabled = True
            txtRitenuta_PRE.Enabled = True
            txtRitenuta_CONS.Enabled = True

            txtRivalsaP.Enabled = False
            txtRivalsa_PRE.Enabled = True
            txtRivalsaC.Enabled = False
            txtRivalsa_CONS.Enabled = True
            txtIMPScaricoPercipiente.Enabled = True
            txtIMPScaricoPercipienteC.Enabled = True
            txtIMPScaricoAzienda.Enabled = True
            txtIMPScaricoAziendaC.Enabled = True

            If ritenuta = "001" Or ritenuta = "002" Or ritenuta = "010" Or ritenuta = "012" Then
                txtIMPScaricoPercipiente.Enabled = False
                txtIMPScaricoPercipiente.Text = "0,00"
                txtIMPScaricoPercipienteC.Enabled = False
                txtIMPScaricoPercipienteC.Text = "0,00"
                txtPercipiente.Value = "0"
                txtPercipiente_C.Value = "0"

                txtIMPScaricoAzienda.Enabled = False
                txtIMPScaricoAzienda.Text = "0,00"
                txtIMPScaricoAziendaC.Enabled = False
                txtIMPScaricoAziendaC.Text = "0,00"
                txtAzienda.Value = "0"
                txtAzienda_C.Value = "0"
            End If

        End If


    End Sub
    Protected Sub btnRielaboraPagamento_Click(sender As Object, e As System.EventArgs) Handles btnRielaboraPagamento.Click
        'STAMPA PAGAMENTO
        Try
            '*******************APERURA CONNESSIONE*********************
            ' RIPRENDO LA CONNESSIONE
            HiddenFieldRielabPagam.Value = "1"
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            Dim dataEmissione As String = HiddenFieldDataEmissione.Value
            If dataEmissione = "0" Then
                dataEmissione = Format(Now, "yyyyMMdd")
            End If
            'If dataEmissione >= "20170701" Then
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdODL
            Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
            If Not String.IsNullOrEmpty(nome) Then
                HiddenFieldRielabPagam.Value = "1"
                If StampaPagamento() = True Then
                    Setta_StataoODL(4)  'EMESSO PAGAMENTO
                    Me.cmbStato.SelectedValue = 4
                    FrmSolaLettura()
                End If
            Else

                RadWindowManager1.RadAlert("Impossibile rielaborare!\nStampare prima il CDP!", 300, 150, "Attenzione", "", "null")
            End If
            'Else
            '    Response.Write("<script>alert('Impossibile rielaborare stampe di CDP anteriori al 01/0!');</script>")
            'End If
        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
        If HiddenFieldDataEmissione.Value = "0" Then
            HiddenFieldDataEmissione.Value = Format(Now, "yyyyMMdd")
        End If
        ControlloPulsanti()
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Blocca_SbloccaMenu(1);", True)
    End Sub

    Private Sub ControlloPulsanti()
        Try
            If txtStatoPagamento.Value = "-1" Then
                ImgStampa.Enabled = False
                ImgStampaPag.Enabled = False
                btnElimina.Enabled = False
                btnAnnulla.Enabled = False
                txtDescrizioneP.ReadOnly = False
                'PRENOTATO
                txtNettoP.ReadOnly = False
                txtImponibileP.ReadOnly = False
                txtIVA_PRE.ReadOnly = False
                txtCass_PRE.ReadOnly = False
                txtRivalsa_PRE.ReadOnly = False
                txtRitenuta_PRE.ReadOnly = False
                'CONSUNTIVATO
                txtNettoC.ReadOnly = True
                txtDScad.ReadOnly = True
                txtImponibileC.ReadOnly = True
                txtPenaleC.ReadOnly = True
                txtIVA_CONS.ReadOnly = True
                txtCass_CONS.ReadOnly = True
                txtRivalsa_CONS.ReadOnly = True
                txtRitenuta_CONS.ReadOnly = True
            End If
            If txtStatoPagamento.Value = 0 Then
                'PRIMO SALVATAGGIO
                ImgStampa.Enabled = False
                ImgStampaPag.Enabled = False
                btnRielaboraPagamento.Enabled = False
                btnElimina.Enabled = False
                btnAnnulla.Enabled = False
                'ImgNuovo.Enabled = True
                txtDescrizioneP.ReadOnly = False
                'PRENOTATO
                txtNettoP.ReadOnly = False
                txtImponibileP.ReadOnly = False
                txtIVA_PRE.ReadOnly = False
                txtCass_PRE.ReadOnly = False
                txtRivalsa_PRE.ReadOnly = False
                txtRitenuta_PRE.ReadOnly = False
                'CONSUNTIVATO                                
                txtNettoC.ReadOnly = True
                txtDScad.ReadOnly = True
                txtImponibileC.ReadOnly = True
                txtPenaleC.ReadOnly = True
                txtIVA_CONS.ReadOnly = True
                txtCass_CONS.ReadOnly = True
                txtRivalsa_CONS.ReadOnly = True
                txtRitenuta_CONS.ReadOnly = True
                'txtNumMandato.readOnly = true
                'txtDataMandato.readOnly = true              
            End If
            If txtStatoPagamento.Value = "1" Then
                'EMESSO non STAMPATO
                ImgStampa.Enabled = True
                ImgStampaPag.Enabled = False
                btnRielaboraPagamento.Enabled = False
                btnElimina.Enabled = False
                btnAnnulla.Enabled = True
                'ImgNuovo.Enabled = true 
                txtDescrizioneP.ReadOnly = False
                'PRENOTATO
                txtNettoP.ReadOnly = False
                txtImponibileP.ReadOnly = False
                txtIVA_PRE.ReadOnly = False
                txtCass_PRE.ReadOnly = False
                txtRivalsa_PRE.ReadOnly = False
                txtRitenuta_PRE.ReadOnly = False
                'CONSUNTIVATO
                txtNettoC.ReadOnly = False
                txtDScad.ReadOnly = False
                txtImponibileC.ReadOnly = False
                txtPenaleC.ReadOnly = False
                txtIVA_CONS.ReadOnly = False
                txtCass_CONS.ReadOnly = False
                txtRivalsa_CONS.ReadOnly = False
                txtRitenuta_CONS.ReadOnly = False
                'txtNumMandato.readOnly = true
                'txtDataMandato.readOnly = true;          
            End If
            If txtStatoPagamento.Value = "2" Then
                'EMESSO STAMPATO
                ImgStampa.Enabled = True
                'ImgStampaPag').style.visibility = 'hidden';
                ImgStampaPag.Enabled = False
                btnRielaboraPagamento.Enabled = False
                btnElimina.Enabled = False
                btnAnnulla.Enabled = True
                'ImgNuovo').style.visibility = 'hidden';   
                txtDescrizioneP.ReadOnly = False
                'PRENOTATO
                txtNettoP.ReadOnly = True
                txtImponibileP.ReadOnly = True
                txtIVA_PRE.ReadOnly = True
                txtCass_PRE.ReadOnly = True
                txtRivalsa_PRE.ReadOnly = True
                txtRitenuta_PRE.ReadOnly = True
                'CONSUNTIVATO
                txtNettoC.ReadOnly = False
                txtDScad.ReadOnly = False
                txtImponibileC.ReadOnly = False
                txtPenaleC.ReadOnly = False
                txtIVA_CONS.ReadOnly = False
                txtCass_CONS.ReadOnly = False
                txtRivalsa_CONS.ReadOnly = False
                txtRitenuta_CONS.ReadOnly = False
                'txtNumMandato.readOnly = true
                'txtDataMandato.readOnly = true 
            End If
            If txtStatoPagamento.Value = "3" Then
                'CONSUNTIVATO NON STAMPATO
                ImgStampa.Enabled = True
                ImgStampaPag.Enabled = True
                btnRielaboraPagamento.Enabled = True
                btnElimina.Enabled = False
                btnAnnulla.Enabled = True
                'ImgNuovo').style.visibility = 'hidden';   
                txtDescrizioneP.ReadOnly = False
                'PRENOTATO
                txtNettoP.ReadOnly = True
                txtImponibileP.ReadOnly = True
                txtIVA_PRE.ReadOnly = True
                txtCass_PRE.ReadOnly = True
                txtRivalsa_PRE.ReadOnly = True
                txtRitenuta_PRE.ReadOnly = True
                'CONSUNTIVATO
                txtNettoC.ReadOnly = False
                txtDScad.ReadOnly = False
                txtImponibileC.ReadOnly = False
                txtPenaleC.ReadOnly = False
                txtIVA_CONS.ReadOnly = False
                txtCass_CONS.ReadOnly = False
                txtRivalsa_CONS.ReadOnly = False
                txtRitenuta_CONS.ReadOnly = False
                'txtNumMandato.readOnly = true
                'txtDataMandato.readOnly = true
            End If
            If txtStatoPagamento.Value = "4" Then
                'CONSUNTIVATO E STAMPATO O LIQUIDATO
                ImgStampa.Enabled = True
                ImgStampaPag.Enabled = True
                btnElimina.Enabled = False
                btnSalva.Enabled = False
                ImgAllegaFile.Enabled = True
                btnAnnulla.Enabled = False
                'ImgNuovo.Enabled = false
                txtDescrizioneP.ReadOnly = True
                'PRENOTATO
                txtNettoP.ReadOnly = True
                txtImponibileP.ReadOnly = True
                txtIVA_PRE.ReadOnly = True
                txtCass_PRE.ReadOnly = True
                txtRivalsa_PRE.ReadOnly = True
                txtRitenuta_PRE.ReadOnly = True
                'CONSUNTIVATO                                
                txtNettoC.ReadOnly = True
                txtDScad.ReadOnly = True
                txtImponibileC.ReadOnly = True
                txtPenaleC.ReadOnly = True
                txtIVA_CONS.ReadOnly = True
                txtCass_CONS.ReadOnly = True
                txtRivalsa_CONS.ReadOnly = True
                txtRitenuta_CONS.ReadOnly = True
                'txtNumMandato.readOnly = true
                'txtDataMandato.readOnly = true 
            End If
            If txtStatoPagamento.Value = "5" Then
                'ANNULLATO
                ImgStampa.Enabled = False
                ImgStampaPag.Enabled = False
                btnElimina.Enabled = False
                btnSalva.Enabled = False
                btnAnnulla.Enabled = False
                'ImgNuovo.Enabled = False
                txtDescrizioneP.ReadOnly = True
                'PRENOTATO
                txtNettoP.ReadOnly = True
                txtImponibileP.ReadOnly = True
                txtIVA_PRE.ReadOnly = True
                txtCass_PRE.ReadOnly = True
                txtRivalsa_PRE.ReadOnly = True
                txtRitenuta_PRE.ReadOnly = True
                'CONSUNTIVATO                                
                txtNettoC.ReadOnly = True
                txtDScad.ReadOnly = True
                txtImponibileC.ReadOnly = True
                txtPenaleC.ReadOnly = True
                txtIVA_CONS.ReadOnly = True
                txtCass_CONS.ReadOnly = True
                txtRivalsa_CONS.ReadOnly = True
                txtRitenuta_CONS.ReadOnly = True
                'txtNumMandato.readOnly = true
                'txtDataMandato.readOnly = true
            End If
            If txtVisualizza.Value = "0" Then
                'BOZZA
                ImgAllegaFile.Enabled = False

            End If
            If txtVisualizza.Value = "1" Then
                'SEMPRE
                ImgAllegaFile.Enabled = True

            End If
            If txtVisualizza.Value = "2" Then
                'SOLO LETTURA O CONSUNTIVATO E STAMPATO O LIQUIDATO
                'ImgAllegaFile.Enabled = False

                btnElimina.Enabled = False
                btnSalva.Enabled = False
                btnAnnulla.Enabled = False
                'ImgNuovo').style.visibility = 'hidden';  
                txtDescrizioneP.ReadOnly = True
                'PRENOTATO
                txtNettoP.ReadOnly = True
                txtImponibileP.ReadOnly = True
                txtIVA_PRE.ReadOnly = True
                txtCass_PRE.ReadOnly = True
                txtRivalsa_PRE.ReadOnly = True
                txtRitenuta_PRE.ReadOnly = True
                'CONSUNTIVATO                                
                txtNettoC.ReadOnly = True
                txtDScad.ReadOnly = True
                txtImponibileC.ReadOnly = True
                txtPenaleC.ReadOnly = True
                txtIVA_CONS.ReadOnly = True
                txtCass_CONS.ReadOnly = True
                txtRivalsa_CONS.ReadOnly = True
                txtRitenuta_CONS.ReadOnly = True
                cmbfornitore.Enabled = False
                cmbStato.Enabled = False
                'txtNumMandato').readOnly = true;
                'txtDataMandato').readOnly = true;                 
            End If
            If txtVisualizza.Value = "3" Then
                'SOLO LETTURA DA CHIAMANTE
                ImgAllegaFile.Enabled = False
                btnElimina.Enabled = False
                btnSalva.Enabled = False
                btnAnnulla.Enabled = False
                ImgStampa.Enabled = False
                ImgStampaPag.Enabled = False
                'ImgNuovo').style.visibility = 'hidden';  
                txtDescrizioneP.ReadOnly = True
                'PRENOTATO
                txtNettoP.ReadOnly = True
                txtImponibileP.ReadOnly = True
                txtIVA_PRE.ReadOnly = True
                txtCass_PRE.ReadOnly = True
                txtRivalsa_PRE.ReadOnly = True
                txtRitenuta_PRE.ReadOnly = True
                'CONSUNTIVATO                                
                txtNettoC.ReadOnly = True
                txtDScad.ReadOnly = True
                txtImponibileC.ReadOnly = True
                txtPenaleC.ReadOnly = True
                txtIVA_CONS.ReadOnly = True
                txtCass_CONS.ReadOnly = True
                txtRivalsa_CONS.ReadOnly = True
                txtRitenuta_CONS.ReadOnly = True

                'txtNumMandato.readOnly = true;
                'txtDataMandato.readOnly = true;                 
            End If
            If ANNULLO.Value = "1" Then
                btnAnnulla.Enabled = False
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class

