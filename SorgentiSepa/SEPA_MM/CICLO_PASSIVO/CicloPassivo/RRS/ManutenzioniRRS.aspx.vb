Imports System.Collections
Imports System.Math
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports Telerik.Web.UI

Partial Class RRS_ManutenzioniRRS
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""

    Public TabberHide As String = ""

    Public sValoreEsercizioFinanziarioR As String



    Public sValoreComplesso As String
    Public sValoreEdificio As String
    Public sValoreVoce As String
    Public sValoreFornitore As String
    Public sValoreAppalto As String
    Public sValoreData_Dal As String
    Public sValoreData_Al As String
    Public sValoreUnita As String
    Public sOrdinamento As String
    Public sValoreStato As String
    Public sValoreRepertorio As String
    Public sValoreODL As String
    Public sValoreAnno As String
    Public sValoreIndirizzoR As String
    Public sValoreUbicazione As String
    Public sValoreProvenienza As String
    Public sValoreVoceR As String
    Public sValoreAppaltoR As String
    Public sValoreStruttura As String
    Public sValoreEsercizioF As String

    Public Property sStrSqlAllegati() As String
        Get
            If Not (ViewState("par_sStrSqlAllegati") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlAllegati"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlAllegati") = value
        End Set
    End Property

    Public Property sStrSqlIrregolarita() As String
        Get
            If Not (ViewState("par_sStrSqlIrregolarita") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlIrregolarita"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlIrregolarita") = value
        End Set
    End Property

    Public Property sStrSqlEventi() As String
        Get
            If Not (ViewState("par_sStrSqlEventi") Is Nothing) Then
                Return CStr(ViewState("par_sStrSqlEventi"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStrSqlEventi") = value
        End Set
    End Property


    Private Sub CaricaAllegati(ByVal sIndice As String)
        sStrSqlAllegati = "select operatori.operatore,SEGNALAZIONI_FO_ALLEGATI.ID,TO_CHAR(TO_DATE(SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,9,2)||':'||SUBSTR(SEGNALAZIONI_FO_ALLEGATI.DATA_ORA,11,2) AS DATA_ORA,SEGNALAZIONI_FO_ALLEGATI.DESCRIZIONE,replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../../../ALLEGATI/FORNITORI/'||NOME_FILE||''',''Allegati'','''');£>Visualizza</a>','$','&'),'£','" & Chr(34) & "') AS NOME_FILE,SEGNALAZIONI_FO_ALL_TIPI.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.SEGNALAZIONI_FO_ALLEGATI,SISCOM_MI.SEGNALAZIONI_FO_ALL_TIPI,SISCOM_MI.SEGNALAZIONI_FORNITORI,operatori WHERE operatori.id(+)=SEGNALAZIONI_FO_ALLEGATI.id_operatore and SEGNALAZIONI_FO_ALL_TIPI.ID(+)=SEGNALAZIONI_FO_ALLEGATI.ID_TIPO AND SEGNALAZIONI_FO_ALLEGATI.ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE=" & sIndice & " ORDER BY DATA_ORA DESC"
        par.cmd.CommandText = sStrSqlAllegati
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim ds1 As New Data.DataTable()
        da1.Fill(ds1)
        CType(Tab_Manu_Allegati.FindControl("DataGridAllegati"), RadGrid).DataSource = ds1
        CType(Tab_Manu_Allegati.FindControl("DataGridAllegati"), RadGrid).DataBind()
    End Sub

    Private Sub CaricaIrregolarita(ByVal sIndice As String)
        sStrSqlIrregolarita = "select DECODE(SEGNALAZIONI_FO_IRR.VISIBILE,0,'NO',1,'SI') AS VISIBILE,SEGNALAZIONI_FO_IRR.ID,SEGNALAZIONI_FO_IRR.ID_TIPO,TO_CHAR(TO_DATE(SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,1,8),'yyyymmdd'),'dd/mm/yyyy')||' '||SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,9,2)||':'||SUBSTR(SEGNALAZIONI_FO_IRR.DATA_ORA,11,2) AS DATA_ORA,SEGNALAZIONI_FO_IRR.DESCRIZIONE,SEGNALAZIONI_FO_TIPI_IRR.DESCRIZIONE AS TIPOLOGIA FROM SISCOM_MI.SEGNALAZIONI_FO_IRR,SISCOM_MI.SEGNALAZIONI_FO_TIPI_IRR,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE SEGNALAZIONI_FO_TIPI_IRR.ID(+)=SEGNALAZIONI_FO_IRR.ID_TIPO AND SEGNALAZIONI_FO_IRR.ID_SEGNALAZIONE=SEGNALAZIONI_FORNITORI.ID AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE=" & sIndice & " ORDER BY DATA_ORA DESC"
        par.cmd.CommandText = sStrSqlIrregolarita
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim ds1 As New Data.DataTable()
        da1.Fill(ds1)
        CType(Tab_Manu_Irregolarita1.FindControl("DataGridIrregolarita"), RadGrid).DataSource = ds1
        CType(Tab_Manu_Irregolarita1.FindControl("DataGridIrregolarita"), RadGrid).DataBind()
    End Sub

    Private Function CaricaEventi(ByVal sIndice As String)
        'sStrSqlEventi = "SELECT OPERATORI.OPERATORE, TO_CHAR (TO_DATE (SUBSTR (data_ora, 1, 8), 'yyyymmdd'), 'dd/mm/yyyy') AS DATA_EVENTO,MOTIVAZIONE,TAB_EVENTI.DESCRIZIONE AS EVENTO FROM OPERATORI, SISCOM_MI.TAB_EVENTI, siscom_mi.EVENTI_SEGNALAZIONI_FO,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE OPERATORI.ID = EVENTI_SEGNALAZIONI_FO.ID_OPERATORE AND TAB_EVENTI.COD = EVENTI_SEGNALAZIONI_FO.COD_EVENTO AND ID_SEGNALAZIONE_FO = SEGNALAZIONI_FORNITORI.ID AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE= " & sIndice & " ORDER BY DATA_ORA DESC"
        sStrSqlEventi = "SELECT (SELECT NVL (RAGIONE_SOCIALE, '') FROM SISCOM_MI.FORNITORI WHERE ID = MOD_FO_ID_FO) AS ENTE_DITTA,OPERATORI.OPERATORE,TO_DATE (DATA_ORA, 'yyyyMMddHH24MISS') AS DATA_EVENTO, MOTIVAZIONE, TAB_EVENTI.DESCRIZIONE AS EVENTO FROM OPERATORI,SISCOM_MI.TAB_EVENTI,siscom_mi.EVENTI_SEGNALAZIONI_FO,SISCOM_MI.SEGNALAZIONI_FORNITORI WHERE OPERATORI.ID = EVENTI_SEGNALAZIONI_FO.ID_OPERATORE AND TAB_EVENTI.COD = EVENTI_SEGNALAZIONI_FO.COD_EVENTO AND ID_SEGNALAZIONE_FO = SEGNALAZIONI_FORNITORI.ID AND SEGNALAZIONI_FORNITORI.ID_MANUTENZIONE = " & sIndice _
        & "UNION " _
        & "SELECT SEPA.CAF_WEB.COD_CAF AS ENTE_DITTA,SEPA.OPERATORI.OPERATORE,TO_DATE (SISCOM_MI.EVENTI_MANUTENZIONE.DATA_ORA, 'yyyyMMddHH24MISS') AS DATA_ORA,SISCOM_MI.EVENTI_MANUTENZIONE.MOTIVAZIONE,SISCOM_MI.TAB_EVENTI.DESCRIZIONE AS EVENTO    FROM SEPA.CAF_WEB, SISCOM_MI.EVENTI_MANUTENZIONE,SISCOM_MI.TAB_EVENTI,SEPA.OPERATORI WHERE SISCOM_MI.EVENTI_MANUTENZIONE.ID_MANUTENZIONE = " & sIndice & " AND SISCOM_MI.EVENTI_MANUTENZIONE.COD_EVENTO = SISCOM_MI.TAB_EVENTI.COD(+) AND SISCOM_MI.EVENTI_MANUTENZIONE.ID_OPERATORE = SEPA.OPERATORI.ID(+) AND SEPA.CAF_WEB.ID = SEPA.OPERATORI.ID_CAF ORDER BY 3 DESC"

        par.cmd.CommandText = sStrSqlEventi
        Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim ds1 As New Data.DataTable()
        da1.Fill(ds1)
        CType(Tab_Manu_Eventi1.FindControl("DataGridEventi"), RadGrid).DataSource = ds1
        CType(Tab_Manu_Eventi1.FindControl("DataGridEventi"), RadGrid).DataBind()
    End Function


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0

        If Not IsPostBack Then
            TipoAllegato.Value = par.getIdOggettoAllegatiWs("ODL")
            HFGriglia.Value = CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).ClientID & "," _
                 & CType(Tab_Manu_Allegati.FindControl("DataGridAllegati"), RadGrid).ClientID & "," _
                 & CType(Tab_Manu_Irregolarita1.FindControl("DataGridIrregolarita"), RadGrid).ClientID & "," _
                 & CType(Tab_Manu_Eventi1.FindControl("DataGridEventi"), RadGrid).ClientID
            HFTAB.Value = "tab1,tab2,tab3,tab4,tab5,tab6"

            HFAltezzaTab.Value = 300
            HFAltezzaFGriglie.Value = "480,380,380,380"
            Try
                'TipoAllegato.Value = par.getIdOggettoAllegatiWs("ODL")
                'HFGriglia.Value = CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).ClientID & "," _
                '  & CType(Tab_Manu_Allegati.FindControl("DataGridAllegati"), RadGrid).ClientID & "," _
                '   & CType(Tab_Manu_Eventi1.FindControl("DataGridEventi"), RadGrid).ClientID
                'HFTAB.Value = "tab1,tab2,tab3,tab4,tab5,tab6"
                'HFAltezzaTab.Value = 300
                'HFAltezzaFGriglie.Value = "480,480,400,400,400,400"
                'RICERCA MANUTENZIONI RRS
                sValoreStruttura = Request.QueryString("STR")
                sValoreEsercizioF = Request.QueryString("ES")

                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreVoce = Request.QueryString("SV")

                sValoreFornitore = Request.QueryString("FO")
                sValoreAppalto = Request.QueryString("AP")
                sValoreData_Dal = Request.QueryString("DAL")
                sValoreData_Al = Request.QueryString("AL")

                sValoreStato = Request.QueryString("ST")

                'RICERCA DIRETTA
                sValoreRepertorio = Request.QueryString("REP")
                sValoreODL = Request.QueryString("ODL") 'Request.QueryString("PROGR")
                sValoreAnno = Request.QueryString("ANNO")
                '************

                'Da Ricerca PRE-INSERIMENTO RicercaRRS_INS.aspx
                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
                sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
                sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

                sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))
                sOrdinamento = Request.QueryString("ORD")
                sValoreProvenienza = Request.QueryString("PROVENIENZA")


                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")

                sValoreAppalto = Request.QueryString("AP")

                If sValoreVoce = "-1" Then
                    sValoreVoce = Request.QueryString("SV_R")
                Else
                    sValoreVoce = Request.QueryString("SV")
                End If

                '******************************************************

                'RICERCA DIRETTA
                'sValoreRepertorio = Request.QueryString("REP")
                'sValoreODL = Request.QueryString("ODL")
                'sValoreAnno = Request.QueryString("ANNO")
                '************

                'RICERCA SFITTI
                sValoreUnita = Request.QueryString("UI")
                '***********************************************

                'SETTAGGIO VARIABILI
                CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = ""
                CType(Tab_Manu_Riepilogo.FindControl("HLink_Appalto"), HyperLink).Text = ""


                Me.txtIdComplesso.Text = -1
                Me.txtIdEdificio.Text = -1
                Me.txtIdScala.Text = -1

                Me.txtVisualizza.Value = 0  'NO BOTTONI ALLEGATI
                Me.txtSTATO.Value = 0       'NO BOTTONI STAMPA

                Me.txtTIPO.Value = 0    '0=NORMALE 1=SFITTI

                Me.txtID_Segnalazioni.Value = -1
                Me.txtID_Fornitore.Value = -1
                Me.txtID_Prenotazione.Value = -1
                Me.txtID_Unita.Value = -1

                Me.txtIdPenale.Value = -1
                Me.txtFL_RIT_LEGGE.Value = 0

                Me.txtID_Pagamento.Value = -1

                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                Me.txtConnessione.Text = CStr(lIdConnessione)
                Me.txtIdManutenzione.Text = "-1"

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    RadWindowManager1.RadAlert("Impossibile visualizzare!", 300, 150, "Attenzione", "", "null")

                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "0"
                Me.BLOCCATO.Value = "0"

                Me.RBL1.Items(0).Selected = True 'dafault COMPLESSI




                vIdManutenzione = 0

                If sValoreProvenienza = "RISULTATI_SFITTI" Then
                    Me.txtID_Unita.Value = Session.Item("ID")
                Else
                    vIdManutenzione = Session.Item("ID")
                End If
                SettaggioCampi()
                Setta_StataoODL(0)


                If vIdManutenzione <> 0 Then
                    'VISUALIZZAZIONE MANUTENZIONE

                    'Visibile = "style=" & Chr(34) & "visibility:Visible" & Chr(34)
                    VisualizzaDati()

                    TabberHide = "tabbertabhide"
                    Tabber1 = "tabbertabdefault"
                    txtindietro.Text = 0
                Else

                    'If sValoreProvenienza = "RISULTATI_SFITTI" Then
                    '    'SFITTI                        
                    '    SettaSfitti()

                    '    cmbScala.Enabled = False
                    '    cmbStato.Enabled = False

                    '    Me.txtIdManutenzione.Text = -1

                    '    TabberHide = "tabbertabhide"
                    '    Tabber1 = "tabbertabdefault"
                    '    txtindietro.Text = 1
                    'Else
                    If sValoreProvenienza = "RISULTATI_RRS_INS" Or sValoreProvenienza = "RICERCARRS_INS" Then

                        Me.txtID_STRUTTURA.Value = Session.Item("ID_STRUTTURA")

                        SettaValRicercaIND(sValoreUbicazione)

                        cmbScala.Enabled = False
                        cmbStato.Enabled = False

                        Me.txtIdManutenzione.Text = -1

                        TabberHide = "tabbertabhide"
                        Tabber1 = "tabbertabdefault"
                        txtindietro.Text = 1
                    End If
                End If


                Dim CTRL As Control

                '*** FORM PRINCIPALE
                For Each CTRL In Me.form1.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is RadComboBox Then
                        DirectCast(CTRL, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                cmbIndirizzo.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                cmbVocePF.Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

                Me.txtDataInizio.Attributes.Add("onkeypress", "javascript:document.getElementById('USCITA').value='1';")
                Me.txtDataFine.Attributes.Add("onkeypress", "javascript:document.getElementById('USCITA').value='1';")


                '*** FORM RIEPILOGO
                For Each CTRL In Me.Tab_Manu_Riepilogo.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                ''*** FORM DETTAGLI
                For Each CTRL In Me.Tab_Manu_Dettagli.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is RadComboBox Then
                        DirectCast(CTRL, RadComboBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next


                'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Attributes.Add("onclick", "ApriConsuntivo();")
                'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Attributes.Add("onclick", "ApriConsuntivo();")


                CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                'CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Attributes.Add("onChange", "javascript:CalcolaPenale(this);")
                CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")


                CType(Tab_Manu_Dettagli.FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(Tab_Manu_Dettagli.FindControl("txtImporto"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event);")


                CType(Tab_Manu_Riepilogo.FindControl("txtOneri"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(Tab_Manu_Riepilogo.FindControl("txtOneri"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_Manu_Riepilogo.FindControl("txtOneri"), TextBox).Attributes.Add("onChange", "javascript:CalcolaImporto(1);")
                CType(Tab_Manu_Riepilogo.FindControl("txtOneri"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event,1);")

                CType(Tab_Manu_Riepilogo.FindControl("txtOneriC"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(Tab_Manu_Riepilogo.FindControl("txtOneriC"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_Manu_Riepilogo.FindControl("txtOneriC"), TextBox).Attributes.Add("onChange", "javascript:CalcolaImportoC(1);")
                CType(Tab_Manu_Riepilogo.FindControl("txtOneriC"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);javascript:$onkeydown(event,1);")

                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Attributes.Add("onChange", "javascript:CalcolaImporto();")
                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Attributes.Add("onChange", "javascript:CalcolaImportoC();")


                If vIdManutenzione = 0 Then AbilitaDisabilita()

                If par.IfEmpty(Me.txtSTATO.Value, 0) = 0 Then
                    Select Case Me.txtStatoPF.Value
                        Case 6
                            If Session.Item("FL_COMI") <> 1 Then
                                If Me.txtFlagVOCI.Value = 0 Then
                                    'Se 1 allora le voci SPECIALI
                                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                                    FrmSolaLettura()
                                End If
                            End If
                        Case 7
                            If Me.txtFlagVOCI.Value = 0 Then
                                'Se 1 allora le voci SPECIALI
                                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                                FrmSolaLettura()
                            End If
                    End Select
                End If


                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")
                'Or Session.Item("BP_GENERALE") = "1" 
                If Session.Item("BP_RSS_L") = "1" Or IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                End If

                ' SE l'operatore è BP_GENERALE=1 (può vedere tutte le strutture) perrò la sua struttura + diversa da quella selezionata allora la maschera è in SOLO LETTURA
                If Session.Item("BP_GENERALE") = "1" And par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) <> Session.Item("ID_STRUTTURA") Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()
                End If




                'GESTIONE LINK SAL
                '....................................................................................

                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "select PROGR_APPALTO || '/' || MANUTENZIONI.ANNO,MANUTENZIONI.ID_PAGAMENTO from SISCOM_MI.manutenzioni," _
                        & "SISCOM_MI.PAGAMENTI where MANUTENZIONI.id='" & vIdManutenzione & "' AND PAGAMENTI.ID=MANUTENZIONI.ID_PAGAMENTO " _
                        & "AND MANUTENZIONI.STATO=4 "
                    Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lett.Read Then
                        If par.IfNull(Lett(0), "") <> "" Then
                            lblSAL.Text = "SAL N°  "
                            lblNsal.Text = "<a href=""javascript:window.open('SAL_RRS.aspx?PROVENIENZA=CHIAMATA_DIRETTA&ID=" & par.IfNull(Lett(1), 0) & "&MANU=1','_blank','resizable=no,height=700,width=1300,top=100,left=100,scrollbars=no');void(0);"">" & Lett(0) & "</a>"
                        End If
                    End If
                    Lett.Close()

                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Else

                    par.cmd.CommandText = "select PROGR_APPALTO || '/' || MANUTENZIONI.ANNO,MANUTENZIONI.ID_PAGAMENTO from SISCOM_MI.manutenzioni," _
                        & "SISCOM_MI.PAGAMENTI where MANUTENZIONI.id='" & vIdManutenzione & "' AND PAGAMENTI.ID=MANUTENZIONI.ID_PAGAMENTO " _
                        & "AND MANUTENZIONI.STATO=4 "
                    Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lett.Read Then
                        If par.IfNull(Lett(0), "") <> "" Then
                            lblSAL.Text = "SAL N°  "
                            lblNsal.Text = "<a href=""javascript:window.open('SAL_RRS.aspx?PROVENIENZA=CHIAMATA_DIRETTA&ID=" & par.IfNull(Lett(1), 0) & "&MANU=1','_blank','resizable=no,height=700,width=1300,top=100,left=100,scrollbars=no');void(0);"">" & Lett(0) & "</a>"
                        End If
                    End If
                    Lett.Close()

                End If
                '....................................................................................





            Catch ex As Exception
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

            End Try
        End If


        Try
            'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                'par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                '    & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                '    & "AND MANUTENZIONI.ID IN (" & vIdManutenzione & ") AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID AND PF_VOCI.FL_CC=0 " _
                '    & "AND PRENOTAZIONI.ID_STATO<>-3 AND MANUTENZIONI.STATO<>5"

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO " _
                                    & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PF_MAIN " _
                                    & "WHERE " _
                                    & "MANUTENZIONI.ID IN (" & vIdManutenzione & ") " _
                                    & "AND MANUTENZIONI.ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0) " _
                                    & "AND MANUTENZIONI.STATO NOT IN (5,6) " _
                                    & "AND MANUTENZIONI.ID_PIANO_FINANZIARIO=PF_MAIN.ID "

                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) >= 6 Then
                        If Session.Item("FL_COMI") = 1 Or Session.Item("FL_ANNULLA_ODL") = "1" Then
                            ANNULLAVISIBILE.Value = 0
                        Else
                            ANNULLAVISIBILE.Value = 1
                        End If
                    End If
                End If
                Lett.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO " _
                                    & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PF_MAIN " _
                                    & "WHERE " _
                                    & "MANUTENZIONI.ID IN (" & vIdManutenzione & ") " _
                                    & "AND MANUTENZIONI.ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0) " _
                                    & "AND MANUTENZIONI.STATO NOT IN (5,6) " _
                                    & "AND MANUTENZIONI.ID_PIANO_FINANZIARIO=PF_MAIN.ID "

                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) >= 6 Then
                        If Session.Item("FL_COMI") = 1 Or Session.Item("FL_ANNULLA_ODL") = "1" Then
                            ANNULLAVISIBILE.Value = 0
                        Else
                            ANNULLAVISIBILE.Value = 1
                        End If
                    End If
                End If
                Lett.Close()

            End If



            'bottone consuntivo
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO,MANUTENZIONI.STATO FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                    & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                    & "AND MANUTENZIONI.ID IN (" & vIdManutenzione & ") AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID AND PF_VOCI.FL_CC=0 " _
                    & "AND PRENOTAZIONI.ID_STATO<>-3 AND MANUTENZIONI.STATO<>5"
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) = 6 And par.IfNull(Lett(1), 0) >= 2 Then
                        '' CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                    End If
                End If
                Lett.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO,MANUTENZIONI.STATO FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                    & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                    & "AND MANUTENZIONI.ID IN (" & vIdManutenzione & ") AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID AND PF_VOCI.FL_CC=0 " _
                    & "AND PRENOTAZIONI.ID_STATO<>-3 AND MANUTENZIONI.STATO<>5"
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) = 6 And par.IfNull(Lett(1), 0) >= 2 Then
                        ' CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                    End If
                End If
                Lett.Close()

            End If

        Catch ex As Exception

        End Try
        Dim direttoreLavori As Integer = 0
        Try
            '12/01/2015
            'Controllo Autorizzazione
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                '*********************************************
                'controllo autorizzazione direttore dei lavori
                par.cmd.CommandText = "SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & txtIdAppalto.Value & ") AND DATA_FINE_INCARICO='30000000'"
                direttoreLavori = par.IfNull(par.cmd.ExecuteScalar, 0)
                If direttoreLavori <> 0 And direttoreLavori = Session.Item("ID_OPERATORE") Then
                End If
                '*********************************************
                par.cmd.CommandText = "SELECT FL_AUTORIZZAZIONE,OPERATORE_AUTORIZZAZIONE FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID = " & vIdManutenzione
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    autorizzazione.Value = par.IfNull(Lett("FL_AUTORIZZAZIONE"), 0)
                    operatoreIniziale.Value = par.IfNull(Lett("OPERATORE_AUTORIZZAZIONE"), 0)
                Else
                    autorizzazione.Value = 0
                    operatoreIniziale.Value = 0
                End If
                Lett.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else
                '*********************************************
                'controllo autorizzazione direttore dei lavori
                par.cmd.CommandText = "SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & txtIdAppalto.Value & ") AND DATA_FINE_INCARICO='30000000'"
                direttoreLavori = par.IfNull(par.cmd.ExecuteScalar, 0)
                If direttoreLavori <> 0 And direttoreLavori = Session.Item("ID_OPERATORE") Then
                End If
                '*********************************************
                par.cmd.CommandText = "SELECT FL_AUTORIZZAZIONE,OPERATORE_AUTORIZZAZIONE FROM SISCOM_MI.MANUTENZIONI WHERE MANUTENZIONI.ID = " & vIdManutenzione
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    autorizzazione.Value = par.IfNull(Lett("FL_AUTORIZZAZIONE"), 0)
                    operatoreIniziale.Value = par.IfNull(Lett("OPERATORE_AUTORIZZAZIONE"), 0)
                Else
                    autorizzazione.Value = "0"
                    operatoreIniziale.Value = 0
                End If
                Lett.Close()
            End If
            If autorizzazione.Value = "0" Then
                cmbStato.Enabled = False
            End If
        Catch ex As Exception
            autorizzazione.Value = "0"
        End Try
        If (Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" And direttoreLavori = Session.Item("ID_OPERATORE")) Or Session.Item("FL_SUPERDIRETTORE") = "1" Then
            ChkAutorizzazioneEmissione.Enabled = True
            If txtSTATO.Value >= 1 Then
                ChkAutorizzazioneEmissione.Enabled = False
            End If
        Else
            ChkAutorizzazioneEmissione.Enabled = False
        End If
        'max 06/07/2016
        CaricaAllegati(vIdManutenzione)
        CaricaIrregolarita(vIdManutenzione)
        CaricaEventi(vIdManutenzione)

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""
        Tabber6 = ""

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
            Case "4"
                Tabber4 = "tabbertabdefault"
            Case "5"
                Tabber5 = "tabbertabdefault"
            Case "6"
                Tabber6 = "tabbertabdefault"

        End Select

        If vIdManutenzione <> 0 Then
            If txtSTATO.Value = 5 Then
                TabberHide = "tabbertab"
                Tabber3 = "tabbertabdefault"
            Else
                NascondiTab()
                TabberHide = "tabbertabhide"
            End If

        Else
            NascondiTab()
            TabberHide = "tabbertabhide"
        End If
        SettaPulsanti()
        Dim i As Integer = 0
        Dim esci As Integer = 0
        For Each item As RadTab In RadTabStrip.Tabs
            If item.Visible = True Then
                If esci = HiddenTabSelezionato.Value Then
                    Exit For
                End If
                esci += 1
            End If
            i += 1
        Next
        RadMultiPage1.SelectedIndex = i
        RadTabStrip.SelectedIndex = i
        If HiddenMostraPulsantiDett.Value = 0 Then
            If CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 0 Then
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Enabled = False
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
                'CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Rebind()

            ElseIf CType(Me.Page.FindControl("HiddenMostraPulsantiDett"), HiddenField).Value = 1 Then
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = True
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Enabled = True
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top
                ' CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Rebind()
                'BindGrid_Consuntivi()
            End If
        End If
        CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Visible = True
        CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = True
        If vIdManutenzione <> 0 Then
            If Session.Item("FL_SUPERDIRETTORE") = "1" Or Session.Item("FL_AUTORIZZAZIONE_ODL") = "1" Then
                btnAnnulla.Enabled = True
            End If
        End If
        btnOrdineIntegrativo.Visible = False
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Blocca_SbloccaMenu(1);setDimensioni();", True)
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

    Public Property vIdManutenzione() As Long
        Get
            If Not (ViewState("par_idManutenzione") Is Nothing) Then
                Return CLng(ViewState("par_idManutenzione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idManutenzione") = value
        End Set

    End Property

    Public Property autorizzazioneIniziale() As Integer
        Get
            If Not (ViewState("autorizzazioneIniziale") Is Nothing) Then
                Return CInt(ViewState("autorizzazioneIniziale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("autorizzazioneIniziale") = value
        End Set

    End Property

    Private Sub SettaggioCampi()
        'CARICO COMBO TAB SECODNDARI

        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'CType(Tab_Manu_Dettagli.FindControl("cmbDettaglio"), RadComboBox).Items.Clear()
            'CType(Tab_Manu_Dettagli.FindControl("cmbDettaglio"), RadComboBox).Items.Add(New ListItem(" ", -1))


            '*** TIPOLOGIA OGGETTO INTERVENTI
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Clear()
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem(" ", -1))

            If RBL1.Items(0).Selected = True Then
                CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("COMPLESSO", "COMPLESSO"))
            End If

            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("EDIFICIO", "EDIFICIO"))
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("UNITA IMMOBILIARE", "UNITA IMMOBILIARE"))
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("UNITA COMUNE", "UNITA COMUNE"))


            Me.lblDataInizio.Text = "Data Presunta Inizio Lavori:"
            Me.lblDataFine.Text = "Data Presunta Fine Lavori"

            'ID PIANO FINANZIARIO e ANNO ESERCIZIO FINANZIARIO
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))
            If sValoreEsercizioFinanziarioR = "-1" Or sValoreEsercizioFinanziarioR = "" Then
                par.cmd.CommandText = "select id_esercizio_finanziario from siscom_mi.pf_main where id in (select id_piano_finanziario from siscom_mi.manutenzioni where id=" & vIdManutenzione & ")"
                sValoreEsercizioFinanziarioR = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If

            par.cmd.CommandText = "select SISCOM_MI.PF_MAIN.ID,SUBSTR(SISCOM_MI.T_ESERCIZIO_FINANZIARIO.INIZIO,1,4) as ANNO,SISCOM_MI.PF_MAIN.ID_STATO " _
                              & " from SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO " _
                              & " where SISCOM_MI.PF_MAIN.ID_STATO>=5 " _
                              & "   and SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO=" & sValoreEsercizioFinanziarioR _
                              & "   and SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO=SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                Me.txtIdPianoFinanziario.Value = par.IfNull(myReader1("ID"), -1)
                Me.txtAnnoEsercizioF.Value = Val(par.IfNull(myReader1("ANNO"), 0))
                Me.txtStatoPF.Value = Val(par.IfNull(myReader1("ID_STATO"), 5))
            End If
            myReader1.Close()
            '***********************************


            ''IVA del PREVENTIVO
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Clear()
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Add(New ListItem("", -1))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Add(New ListItem("0", 0))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Add(New ListItem("4", 4))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Add(New ListItem("10", 10))
            ''CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Add(New ListItem("20", 20))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Items.Add(New ListItem("21", 21)) ' IVA 21% 
            Dim queryIVA As String = "SELECT VALORE FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1 ORDER BY VALORE ASC"
            par.caricaComboBox(queryIVA, CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList), "VALORE", "VALORE", True, "-1", " ")
            CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).SelectedValue = -1


            ''IVA del CONSUNTIVO
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Clear()
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Add(New ListItem("", -1))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Add(New ListItem("0", 0))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Add(New ListItem("4", 4))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Add(New ListItem("10", 10))
            ''CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Add(New ListItem("20", 20))
            'CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Items.Add(New ListItem("21", 21)) ' IVA 21% 
            par.caricaComboBox(queryIVA, CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList), "VALORE", "VALORE", True, "-1", " ")
            CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).SelectedValue = -1


            If IsNumeric(Request.QueryString("AP")) Then
                Dim idApp As Integer = Request.QueryString("AP")
                par.caricaComboTelerik("SELECT ID,COD_FORNITORE||'-'||RAGIONE_SOCIALE AS DESCRIZIONE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=" & idApp & ")", cmbIntestazione, "ID", "DESCRIZIONE", True)
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

    Private Sub Setta_Ubicazione(ByVal RBL1 As Integer, ByVal ID As Long, ByVal sCondizione As String)
        Dim FlagConnessione As Boolean


        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'Me.cmbIndirizzo.Items.Clear()
            'Me.cmbIndirizzo.Items.Add(New ListItem(" ", -1))

            txtIdComplesso.Text = -1
            txtIdEdificio.Text = -1
            txtIdScala.Text = -1


            If RBL1 = 0 Then
                'CARICO INDIRIZZI COMPLESSI

                If ID <> 0 Then
                    par.cmd.CommandText = "select ID,DENOMINAZIONE from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID= " & ID & "order by DENOMINAZIONE asc"
                Else
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        FlagConnessione = False
                    End If
                    Exit Sub
                End If
            Else
                'CARICO INDIRIZZI EDIFICI
                If ID <> 0 Then
                    par.cmd.CommandText = "select distinct ID,(DENOMINAZIONE||' - -Cod.'||COD_EDIFICIO) as DENOMINAZIONE from SISCOM_MI.EDIFICI where ID= " & ID & " order by DENOMINAZIONE asc"
                Else
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        FlagConnessione = False
                    End If
                    Exit Sub
                End If
            End If

            par.caricaComboTelerik(par.cmd.CommandText, cmbIndirizzo, "ID", "DENOMINAZIONE", True)

            Me.cmbIndirizzo.SelectedValue = "-1"

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            'If ID = 0 Then
            '    cmbIndirizzo.SelectedValue = "-1"
            '    Setta_Scala(sCondizione)
            '    Setta_TipologiaDettaglio()
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

    'Protected Sub cmbIndirizzo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.SelectedIndexChanged
    '    'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

    '    'Setta_Scala("=")
    '    'Setta_TipologiaDettaglio()

    'End Sub

    'Protected Sub cmbIndirizzo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbIndirizzo.TextChanged
    '    'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    'End Sub

    Private Sub Setta_Scala(ByVal sCondizione As String)
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try
            'Me.cmbScala.Items.Clear()
            'Me.cmbScala.Items.Add(New ListItem(" ", -1))

            'Me.cmbInterno.Items.Clear()
            'cmbInterno.Items.Add(New ListItem(" ", -1))

            If RBL1.Items(0).Selected = True Then
                cmbScala.Enabled = False
                'cmbInterno.Enabled = False
                txtIdEdificio.Text = -1
                txtIdScala.Text = -1
                'Setta_Servizio(0, sCondizione)
                Exit Sub
            Else
                cmbScala.Enabled = True
                'cmbInterno.Enabled = True
            End If


            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            If Me.cmbIndirizzo.SelectedValue <> "-1" Then

                'par.cmd.CommandText = "select ID,DESCRIZIONE  from SISCOM_MI.PIANI where ID_EDIFICIO= " & Me.cmbIndirizzo.SelectedValue & " order by DESCRIZIONE asc"
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select ID,DESCRIZIONE  from SISCOM_MI.SCALE_EDIFICI where ID_EDIFICIO= " & Me.cmbIndirizzo.SelectedValue & " order by DESCRIZIONE asc"

                par.caricaComboTelerik(par.cmd.CommandText, cmbScala, "ID", "DESCRIZIONE", True)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                '    Me.cmbScala.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))
                '    i = i + 1
                'End While
                'myReader1.Close()
            End If
            Me.cmbScala.SelectedValue = "-1"

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Setta_VocePF(0, sCondizione)

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


    Private Sub Setta_TipologiaDettaglio()
        Dim i As Integer = 0
        Dim FlagConnessione As Boolean

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            CType(Tab_Manu_Dettagli.FindControl("cmbDettaglio"), DropDownList).Items.Clear()
            CType(Tab_Manu_Dettagli.FindControl("cmbDettaglio"), DropDownList).Items.Add(New ListItem(" ", -1))

            '*** TIPOLOGIA OGGETTO INTERVENTI
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Clear()
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem(" ", -1))


            If RBL1.Items(0).Selected = True Then
                CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("COMPLESSO", "COMPLESSO"))
            End If

            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("EDIFICIO", "EDIFICIO"))
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("UNITA IMMOBILIARE", "UNITA IMMOBILIARE"))
            CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("UNITA COMUNE", "UNITA COMUNE"))


            If Me.cmbIndirizzo.SelectedValue <> "-1" And Me.cmbIndirizzo.SelectedValue <> "" Then

                par.cmd.CommandText = "select distinct TIPOLOGIA_IMPIANTI.DESCRIZIONE from SISCOM_MI.TIPOLOGIA_IMPIANTI " _
                                   & " where COD in (select COD_TIPOLOGIA from SISCOM_MI.IMPIANTI "

                If RBL1.Items(0).Selected = True Then
                    par.cmd.CommandText = par.cmd.CommandText & " where ID_COMPLESSO=" & Me.cmbIndirizzo.SelectedValue _
                                    & "   or ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & Me.cmbIndirizzo.SelectedValue & ")" _
                                    & "   or ID_UNITA_IMMOBILIARE in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                   & " where ID_EDIFICIO in (select ID from SISCOM_MI.EDIFICI where ID_COMPLESSO=" & Me.cmbIndirizzo.SelectedValue & ")) "
                Else
                    par.cmd.CommandText = par.cmd.CommandText & " where ID_EDIFICIO=" & Me.cmbIndirizzo.SelectedValue _
                                    & "   or ID_UNITA_IMMOBILIARE in (select ID from SISCOM_MI.UNITA_IMMOBILIARI " _
                                                                   & " where ID_EDIFICIO=" & Me.cmbIndirizzo.SelectedValue & ") "

                End If

                par.cmd.CommandText = par.cmd.CommandText & ") order by DESCRIZIONE"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                myReader1 = par.cmd.ExecuteReader


                While myReader1.Read
                    CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).Items.Add(New ListItem("IMPIANTO - " & par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("DESCRIZIONE"), -1)))
                End While
                myReader1.Close()

                CType(Tab_Manu_Dettagli.FindControl("cmbTipologia"), DropDownList).SelectedValue = "-1"
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


    Private Sub Setta_VocePF(ByVal ID As Long, ByVal sCondizione As String)
        Dim FlagConnessione As Boolean

        Try

            Me.cmbVocePF.Items.Clear()
            'Me.cmbVocePF.Items.Add(New ListItem(" ", -1))

            CType(Tab_Manu_Riepilogo.FindControl("txtLotto"), TextBox).Text = ""
            CType(Tab_Manu_Riepilogo.FindControl("HLink_Appalto"), HyperLink).Text = ""
            CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = ""


            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If
            If Me.cmbIndirizzo.Items.Count > 0 Then
                If Me.cmbIndirizzo.SelectedValue <> "-1" Then
                    If RBL1.Items(0).Selected = True Then
                        txtIdComplesso.Text = Me.cmbIndirizzo.SelectedValue
                    Else
                        txtIdEdificio.Text = Me.cmbIndirizzo.SelectedValue
                    End If
                End If
            End If

            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))


            If ID <> 0 Then
                par.cmd.CommandText = "select ID,(CODICE || ' - ' ||DESCRIZIONE) as DESCRIZIONE,CODICE from SISCOM_MI.PF_VOCI where ID=" & ID
            Else
                par.cmd.CommandText = "select ID,(CODICE || ' - ' ||DESCRIZIONE) as DESCRIZIONE,CODICE " _
                                   & " from SISCOM_MI.PF_VOCI " _
                                   & " where ID in (select ID_PF_VOCE from SISCOM_MI.APPALTI_VOCI_PF " _
                                               & " where ID_APPALTO in (select ID from SISCOM_MI.APPALTI where ID_STATO=1 and TIPO='N')) " _
                                 & "    and ID_PIANO_FINANZIARIO=" & sValoreEsercizioFinanziarioR _
                                 & " order by DESCRIZIONE asc"
            End If
            par.caricaComboTelerik(par.cmd.CommandText, cmbVocePF, "ID", "DESCRIZIONE", True)
            'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    Me.cmbVocePF.Items.Add(New ListItem(par.IfNull(myReader1("DESCRIZIONE"), " "), par.IfNull(myReader1("ID"), -1)))

            'End While
            'myReader1.Close()


            Me.cmbVocePF.SelectedValue = "-1"



            Dim idEsercizioFinanziario As Integer = sValoreEsercizioFinanziarioR
            If IsNumeric(sValoreEsercizioFinanziarioR) AndAlso sValoreEsercizioFinanziarioR > 0 Then
                par.cmd.CommandText = "select substr(inizio,1,4) as anno from siscom_mi.t_Esercizio_finanziario " _
                    & " where t_esercizio_finanziario.id=" & sValoreEsercizioFinanziarioR
                lblEsercizioFinanziario.Text = "Anno B.P. " & par.cmd.ExecuteScalar
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

    Private Sub Setta_Appalto_Fornitore(ByVal sCondizione As String)
        Dim FlagConnessione As Boolean
        Dim sStr1 As String
        Dim SommaResiduo As Decimal = 0

        Try

            CType(Tab_Manu_Riepilogo.FindControl("HLink_Appalto"), HyperLink).Text = ""
            CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = ""


            Me.txtID_Fornitore.Value = -1

            Me.txtPercOneri.Value = 0
            Me.txtScontoConsumo.Value = 0
            Me.txtPercIVA_P.Value = 0
            Me.txtPercIVA_C.Value = 0
            Me.txtOneriSicurezza.Value = 0

            CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).Text = ""
            CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value = ""

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            If Me.cmbVocePF.SelectedValue <> "-1" Then



                If Me.txtIdAppalto.Value < 0 Then
                    sStr1 = "select  SISCOM_MI.APPALTI.ID as ""ID_APPALTO"",SISCOM_MI.APPALTI.DESCRIZIONE as ""APPALTO"",SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.FL_RIT_LEGGE, " _
                                 & "  SISCOM_MI.FORNITORI.ID as ""ID_FORNITORE"",SISCOM_MI.FORNITORI.RAGIONE_SOCIALE,SISCOM_MI.FORNITORI.COGNOME,SISCOM_MI.FORNITORI.NOME,SISCOM_MI.FORNITORI.TIPO,SISCOM_MI.FORNITORI.COD_FISCALE,SISCOM_MI.FORNITORI.PARTITA_IVA,SISCOM_MI.FORNITORI.COD_FORNITORE " _
                         & " from SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                         & " where SISCOM_MI.APPALTI.ID in (select ID_APPALTO from SISCOM_MI.APPALTI_VOCI_PF " _
                                                     & " where  ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue & ")" _
                         & "  and SISCOM_MI.APPALTI.ID_STATO" & sCondizione & "1 " _
                         & "  and SISCOM_MI.APPALTI.TIPO='N' " _
                         & "  and SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)" _
                         & " order by ID_APPALTO desc"
                Else

                    sStr1 = "select  SISCOM_MI.APPALTI.ID as ""ID_APPALTO"",SISCOM_MI.APPALTI.DESCRIZIONE as ""APPALTO"",SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.FL_RIT_LEGGE, " _
                                 & "  SISCOM_MI.FORNITORI.ID as ""ID_FORNITORE"",SISCOM_MI.FORNITORI.RAGIONE_SOCIALE,SISCOM_MI.FORNITORI.COGNOME,SISCOM_MI.FORNITORI.NOME,SISCOM_MI.FORNITORI.TIPO,SISCOM_MI.FORNITORI.COD_FISCALE,SISCOM_MI.FORNITORI.PARTITA_IVA,SISCOM_MI.FORNITORI.COD_FORNITORE " _
                         & " from SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                         & " where SISCOM_MI.APPALTI.ID=" & Me.txtIdAppalto.Value _
                         & "   and SISCOM_MI.APPALTI.TIPO='N' " _
                         & "   and SISCOM_MI.APPALTI.ID_FORNITORE=SISCOM_MI.FORNITORI.ID (+)"

                End If

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                        Else
                            CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                        End If
                    Else
                        If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                            CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        Else
                            CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        End If
                    End If


                    'If par.IfNull(myReader1("TIPO"), "") = "F" Then
                    CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") & "','Fornitore','height=700,width=1300');")
                    '    CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../APPALTI/FornitoreF.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") & "','Fornitore','height=550,width=800');")
                    'Else
                    '    CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../APPALTI/FornitoreG.aspx?ID=" & par.IfNull(myReader1("ID_FORNITORE"), "") & "','Fornitore','height=550,width=800');")
                    'End If

                    If Strings.Len(CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text) >= 25 Then
                        CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text = Left(CType(Tab_Manu_Riepilogo.FindControl("HLink_Fornitore"), HyperLink).Text, 25) & "..."
                    End If

                    CType(Tab_Manu_Riepilogo.FindControl("HLink_Appalto"), HyperLink).Text = par.IfNull(myReader1("NUM_REPERTORIO"), "")
                    CType(Tab_Manu_Riepilogo.FindControl("HLink_Appalto"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../APPALTI/AppaltiNP.aspx?IDA=" & par.IfNull(myReader1("ID_APPALTO"), "") & "&LE=1','Appalto','height=700,width=1300');")



                    If Me.txtIdAppalto.Value < 0 Then
                        Me.txtIdAppalto.Value = par.IfNull(myReader1("ID_APPALTO"), "")
                    End If


                    'PEPPE MODIFY 02/05/2011 PER PRELEVARE ONERI DA APPALTI_VOCI_PF
                    par.cmd.CommandText = "SELECT ONERI_SICUREZZA_CONSUMO, PERC_ONERI_SIC_CON " _
                                        & "FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & Me.txtIdAppalto.Value & " AND ID_PF_VOCE = " & Me.cmbVocePF.SelectedValue
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.Read Then
                        Me.txtOneriSicurezza.Value = par.IfNull(lettore("ONERI_SICUREZZA_CONSUMO"), 0)
                        Me.txtPercOneri.Value = par.IfNull(lettore("PERC_ONERI_SIC_CON"), 0)
                    Else
                        Me.txtPercOneri.Value = 0
                        Me.txtOneriSicurezza.Value = 0

                    End If
                    lettore.Close()


                    Me.txtID_Fornitore.Value = par.IfNull(myReader1("ID_FORNITORE"), -1)
                    Me.txtFL_RIT_LEGGE.Value = par.IfNull(myReader1("FL_RIT_LEGGE"), 0)

                End If
                myReader1.Close()


                Setta_ImportoResiduo()


                'ESTRAGGO per ID_PF_VOCE lo sconto e iva al consumo
                sStr1 = "select * from SISCOM_MI.APPALTI_VOCI_PF " _
                     & " where ID_APPALTO=" & Me.txtIdAppalto.Value _
                       & " and ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    Me.txtScontoConsumo.Value = par.IfNull(myReader1("SCONTO_CONSUMO"), 0)

                    If vIdManutenzione = 0 Then
                        Me.txtPercIVA_P.Value = par.IfNull(myReader1("IVA_CONSUMO"), -1)
                        Me.txtPercIVA_C.Value = par.IfNull(myReader1("IVA_CONSUMO"), -1)

                        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).SelectedValue = par.SetCmbIva(CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList), Me.txtPercIVA_P.Value)
                        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).SelectedValue = par.SetCmbIva(CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList), Me.txtPercIVA_C.Value)

                        'If Me.txtPercIVA.Value < 0 Then
                        '    CType(Tab_Manu_Riepilogo.FindControl("lblIVAC"), Label).Text = "IVA ( %)"
                        'Else
                        '    CType(Tab_Manu_Riepilogo.FindControl("lblIVAC"), Label).Text = "IVA (" & Me.txtPercIVA.Value & "%)"
                        'End If

                    End If

                    If txtSTATO.Value = 0 Then AbilitaDisabilita()
                    txtIdScala.Text = Me.cmbScala.SelectedValue
                Else
                    If txtSTATO.Value = 0 Then AbilitaDisabilita()
                End If
                myReader1.Close()


                '********* STATO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select count(*) from SISCOM_MI.PF_VOCI " _
                                    & " where FL_CC=1  " _
                                    & "   and ID_PIANO_FINANZIARIO=" & CType(Me.Page.FindControl("txtIdPianoFinanziario"), HiddenField).Value _
                                    & "   and ID=" & Me.cmbVocePF.SelectedValue

                Me.txtFlagVOCI.Value = 0    'Se 1 allora le voci SPECIALI

                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtFlagVOCI.Value = par.IfNull(myReader1(0), 0)
                End If
                myReader1.Close()
                '********************************************

            End If



            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Setta_TipologiaDettaglio()


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



    Private Function Setta_ImportoResiduo() As Boolean
        Dim FlagConnessione As Boolean
        Dim sStr1 As String

        Dim SommaResiduo As Decimal = 0
        Dim ris1, ris2 As Decimal

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Setta_ImportoResiduo = True

            'CALCOLO l'IMPORTO RESIDUO dato da:

            '1) la somma di eventuali variazioni all'importo residuo di APPALTI_VARIAZIONI_IMPORTI

            sStr1 = "select SUM(IMPORTO) " _
                 & " from   SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
                 & " where  ID_VARIAZIONE in (select ID from SISCOM_MI.APPALTI_VARIAZIONI " _
                                         & " where ID_APPALTO=" & Me.txtIdAppalto.Value & ")"

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                SommaResiduo = par.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()


            '2)la somma dell'importo calcolato (IMPORTO-CONSUMO+IVA) di ID_SERVIZIO per tutte le ID_VOCE_SERVIZIO

            sStr1 = "select * from SISCOM_MI.APPALTI_VOCI_PF " _
                 & " where ID_APPALTO=" & Me.txtIdAppalto.Value

            'non filtrato più per voce dal 21/11/2017
            '& "   and ID_PF_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=(select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & Me.cmbVocePF.SelectedValue & ")) "

            '& "   and ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue 'Prima cercavo solo la VOCE

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                'IMPORTO a CONSUMO senza IVA=IMPORTO_CONSUMO-(IMPORTO_COSUMO*SCONTO_CONSUMO/100)
                'ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - par.IfNull(myReader1("SCONTO_CONSUMO"), 0)
                ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) * par.IfNull(myReader1("SCONTO_CONSUMO"), 0) / 100
                ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - ris1

                ris1 = ris1 + par.IfNull(myReader1("ONERI_SICUREZZA_CONSUMO"), 0) 'par.IfEmpty(Me.txtOneriSicurezza.Value, 0)

                If par.IfNull(myReader1("IVA_CONSUMO"), 0) > 0 Then
                    ris2 = ris1 * par.IfNull(myReader1("IVA_CONSUMO"), 0) / 100
                Else
                    ris2 = 0
                End If
                SommaResiduo = SommaResiduo + ris1 + ris2
            End While
            myReader1.Close()


            '3)la SommaResiduo va sottratto alla somma dell'IMPORTO PRENOTATO o CONSUNTIVATO o EMESSO PAGAMENTO (SAL) da MANUTENZIONI 
            ' sStr1 = "select SUM(IMPORTO_TOT) from SISCOM_MI.MANUTENZIONI " _
            '     & " where ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtIdAppalto.Value & ")) " _
            '    & "   and ID_PF_VOCE_IMPORTO is null " _
            '   & "   /*and ID_PF_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where CODICE=(SELECT CODICE FROM SISCOM_MI.PF_VOCI WHERE ID=" & Me.cmbVocePF.SelectedValue & " )))*/ " _
            '  & "   and STATO in (1,2,4)"
            sStr1 = "select sum(nvl(importo_prenotato,0)) as tot  from siscom_mi.prenotazioni where id_appalto in (SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo=(SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID= " & Me.txtIdAppalto.Value & ")) and id_stato = 0 and tipo_pagamento = 7"

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                SommaResiduo = Math.Round(SommaResiduo, 2) - Math.Round(par.IfNull(myReader1(0), 0), 2) '+ par.IfEmpty(Me.txtOneriSicurezza.Value, 0)
            End If
            myReader1.Close()

            'sStr1 = "select sum(nvl(importo_approvato,0)) as tot  from siscom_mi.prenotazioni where id_appalto = " & Me.txtIdAppalto.Value & " and id_stato > 0 and tipo_pagamento = 3"
            sStr1 = "select sum(nvl(importo_approvato,0)) as tot  from siscom_mi.prenotazioni where id_appalto in (SELECT ID FROM siscom_mi.APPALTI WHERE id_gruppo=(SELECT id_gruppo FROM siscom_mi.APPALTI WHERE ID= " & Me.txtIdAppalto.Value & ")) and id_stato > 0 and tipo_pagamento = 7"

            'par.cmd.CommandText = "select sum(nvl(importo_approvato,0)) as tot  from siscom_mi.prenotazioni where id_appalto = " & Me.txtIdAppalto.Value & " and id_stato > 0 and tipo_pagamento = 3"


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                SommaResiduo = Math.Round(SommaResiduo, 2) - Math.Round(par.IfNull(myReader1(0), 0), 2) '+ par.IfEmpty(Me.txtOneriSicurezza.Value, 0)
            End If
            myReader1.Close()

            If SommaResiduo < 0 Then
                Setta_ImportoResiduo = False
            End If

            CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).Text = IsNumFormat(SommaResiduo, "", "##,##0.00")
            CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value = IsNumFormat(SommaResiduo, "", "##,##0.00")

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Setta_ImportoResiduo = False
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

    End Function

    Private Sub Setta_StataoODL(ByVal Tipo As Integer)
        '   Me.cmbStato.Items.Clear()
        Me.cmbStato.Items.Clear()

        Dim item1 As New RadComboBoxItem
        item1.Value = 0
        item1.Text = "BOZZA"
        cmbStato.Items.Add(item1)
        Dim item2 As New RadComboBoxItem
        item2.Value = 1
        item2.Text = "EMESSO"
        cmbStato.Items.Add(item2)
        Dim item3 As New RadComboBoxItem
        item3.Value = 2
        item3.Text = "CONSUNTIVATO"
        cmbStato.Items.Add(item3)

        If Tipo = 3 Then
            Dim item4 As RadComboBoxItem = New RadComboBoxItem
            item4.Value = 3
            item4.Text = "INTEGRATO"
            cmbStato.Items.Add(item4)
        End If
        If Tipo = 4 Then
            Dim item5 As RadComboBoxItem = New RadComboBoxItem
            item5.Value = 4
            item5.Text = "EMESSO PAGAMENTO"
            cmbStato.Items.Add(item5)
        End If
        If Tipo = 5 Then
            Dim item6 As RadComboBoxItem = New RadComboBoxItem
            item6.Value = 5
            item6.Text = "ANNULLATO"
            cmbStato.Items.Add(item6)
        End If
        cmbStato.SelectedValue = 0
    End Sub



    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        par.caricaComboTelerik("SELECT ID,COD_FORNITORE||'-'||RAGIONE_SOCIALE AS DESCRIZIONE FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID IN (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=(select ID_APPALTO from siscom_mi.manutenzioni where manutenzioni.id=" & vIdManutenzione & "))", cmbIntestazione, "ID", "DESCRIZIONE", True)
        cmbIntestazione.SelectedValue = par.IfNull(myReader1("ID_FORNITORE_STAMPA"), -1)

        'par.cmd.CommandText = "SELECT RAGIONE_SOCIALE,MAIL FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID =(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & vIdManutenzione & "))"
        par.cmd.CommandText = "SELECT RAGIONE_SOCIALE,(SELECT EMAIL FROM SISCOM_MI.FORNITORI_INDIRIZZI WHERE ID_FORNITORE=FORNITORI.ID AND ID =(SELECT MAX(ID) FROM SISCOM_MI.FORNITORI_INDIRIZZI WHERE ID_FORNITORE=FORNITORI.ID)) AS MAIL FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID =(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & vIdManutenzione & "))"
        Dim lettoreF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim Impresa As String = ""
        If lettoreF.Read Then
            indirizzoEmail.Value = par.IfNull(lettoreF("MAIL"), "")
            Impresa = par.IfNull(lettoreF("RAGIONE_SOCIALE"), "")
        End If
        lettoreF.Close()
        Dim idApp As Integer = 0
        par.cmd.CommandText = "SELECT num_repertorio,ID,modulo_fornitori FROM siscom_mi.APPALTI WHERE ID =(select ID_APPALTO from siscom_mi.manutenzioni where manutenzioni.id=" & vIdManutenzione & ")"
        lettoreF = par.cmd.ExecuteReader
        Dim numeroRepertorio As String = ""
        If lettoreF.Read Then
            numeroRepertorio = par.IfNull(lettoreF("NUM_REPERTORIO"), "-")
            'ModuloFornitoriAttivo = par.IfNull(lettoreF("modulo_fornitori"), 0)
            idApp = par.IfNull(lettoreF("ID"), 0)
        End If
        lettoreF.Close()

        oggettoEmail.Value = "ORDINE DI LAVORO N. " & par.IfNull(myReader1("PROGR"), "-") & "/" & par.IfNull(myReader1("ANNO"), "-") & " CON RIFERIEMENTO AL CONTRATTO N. " & numeroRepertorio & " - " & Impresa
        bodyEmail.Value = "Con la presente si trasmette regolare ordine di lavoro. Attendiamo riscontro in merito alla programmazione e conclusione dei lavori. Cordiali saluti"

        '*** FORM PRINCIPALE
        Me.txtIdManutenzione.Text = par.IfNull(myReader1("ID"), "-1")
        Me.txtID_STRUTTURA.Value = par.IfNull(myReader1("ID_STRUTTURA"), "-1")
        Me.txtIdPianoFinanziario.Value = par.IfNull(myReader1("ID_PIANO_FINANZIARIO"), "-1")

        Me.txtID_Unita.Value = par.IfNull(myReader1("ID_UNITA_IMMOBILIARI"), "-1")
        If par.IfNull(myReader1("ID_UNITA_IMMOBILIARI"), "-1") <> "-1" Then
            Me.txtTIPO.Value = 1    'Appare il bottone "Unità Immobiliare" Visualizza la scheda dell unita
        Else
            Me.txtTIPO.Value = 0
        End If

        If par.IfNull(myReader1("fl_autorizzazione"), "0") = "1" Then
            ChkAutorizzazioneEmissione.Checked = True
            autorizzazioneIniziale = 1
        Else
            ChkAutorizzazioneEmissione.Checked = False
            autorizzazioneIniziale = 0
        End If

        If par.IfNull(myReader1("ID_COMPLESSO"), "-1") <> "-1" Then
            RBL1.Items(0).Selected = True
            RBL1.Items(1).Selected = False

            Setta_Ubicazione(0, myReader1("ID_COMPLESSO"), ">=")
            Me.cmbIndirizzo.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
            Me.txtIdComplesso.Text = Me.cmbIndirizzo.SelectedValue

            Setta_Scala(">=")
            'Setta_TipologiaDettaglio()
        ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "-1") <> "-1" Then
            RBL1.Items(1).Selected = True
            RBL1.Items(0).Selected = False

            Setta_Ubicazione(1, myReader1("ID_EDIFICIO"), ">=")
            Me.cmbIndirizzo.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")
            Me.txtIdEdificio.Text = Me.cmbIndirizzo.SelectedValue

            Setta_Scala(">=")
            'Setta_TipologiaDettaglio()
            Me.cmbScala.SelectedValue = par.IfNull(myReader1("ID_SCALA"), "-1")
        End If



        Setta_VocePF(par.IfNull(myReader1("ID_PF_VOCE"), "-1"), ">=")
        Me.cmbVocePF.SelectedValue = par.IfNull(myReader1("ID_PF_VOCE"), "-1")

        Dim idVoce As Integer = par.IfNull(myReader1("ID_PF_VOCE"), 0)
        Dim idVoceImporto As Integer = par.IfNull(myReader1("ID_PF_VOCE_IMPORTO"), 0)
        If IsNumeric(idVoce) AndAlso idVoce > 0 Then
            par.cmd.CommandText = "select substr(inizio,1,4) as anno " _
                & " from siscom_mi.pf_main, siscom_mi.t_Esercizio_finanziario " _
                & " where pf_main.id_esercizio_finanziario=t_esercizio_finanziario.id " _
                & " and pf_main.id =(select id_piano_finanziario from siscom_mi.pf_voci where pf_voci.id=" & idVoce & ")"
            lblEsercizioFinanziario.Text = "Anno B.P. " & par.cmd.ExecuteScalar
        Else
            If IsNumeric(idVoceImporto) AndAlso idVoceImporto > 0 Then
                par.cmd.CommandText = "select substr(inizio,1,4) as anno " _
                    & " from siscom_mi.pf_main,siscom_mi.t_Esercizio_finanziario " _
                    & " where pf_main.id_esercizio_finanziario=t_esercizio_finanziario.id " _
                    & " and pf_main.id=(select id_piano_finanziario from siscom_mi.pf_voci " _
                    & " where pf_voci.id=(select PF_VOCI_IMPORTO.ID_VOCE " _
                    & " from siscom_mi.pf_voci_importo where pf_voci_importo.id=" & idVoceImporto & "))"
                lblEsercizioFinanziario.Text = "Anno B.P. " & par.cmd.ExecuteScalar
            End If
        End If


        Me.txtIdAppalto.Value = par.IfNull(myReader1("ID_APPALTO"), "")
        Setta_Appalto_Fornitore(">=")

        CType(Tab_Manu_Note.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader1("NOTE"), "")

        CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text = par.IfNull(myReader1("DESCRIZIONE"), "")
        CType(Tab_Manu_Dettagli.FindControl("txtDanneggiante"), TextBox).Text = par.IfNull(myReader1("DANNEGGIANTE"), "")
        CType(Tab_Manu_Dettagli.FindControl("txtDanneggiato"), TextBox).Text = par.IfNull(myReader1("DANNEGGIATO"), "")

        Me.lblData.Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_ORDINE"), ""))

        If Not String.IsNullOrEmpty(par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))) Then
            Me.txtDataInizio.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_INTERVENTO"), ""))
        End If
        If Not String.IsNullOrEmpty(par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))) Then
            txtDataFine.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_FINE_INTERVENTO"), ""))
        End If

        lblODL1.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
        Me.txtAnnoManutenzione.Value = par.IfNull(myReader1("ANNO"), "")

        txtSTATO.Value = par.IfNull(myReader1("STATO"), "0")

        If Val(txtSTATO.Value) >= 2 Then
            'Se è CONSUNTIVATO, ANNULLATO, INTEGRATO o EMESSO PAGAMENTO alloro non prendo il valore ricavato da 
            ' Setta_importoResiduo ma quello presente in MANUTENZIONI, come se fosse una fotografia della situazione del residuo nel perido di creazione della manutenzionie
            CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_RESIDUO"), 0), "", "##,##0.00")
            CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).ToolTip = "Importo residuo calcolato alla data di consuntivazione dell’ordine."

            CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value = IsNumFormat(par.IfNull(myReader1("IMPORTO_RESIDUO"), 0), "", "##,##0.00")
        Else
            CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).ToolTip = "Importo residuo."
        End If

        Me.cmbStato.Enabled = True
        Setta_StataoODL(txtSTATO.Value)

        Me.cmbStato.SelectedValue = par.IfNull(myReader1("STATO"), "0")

        CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_PRESUNTO"), 0), "", "##,##0.00")
        CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text = IsNumFormat(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "", "##,##0.00")

        CType(Tab_Manu_Riepilogo.FindControl("txtOneriP_MANO"), HiddenField).Value = par.IfNull(myReader1("IMPORTO_ONERI_PREV"), -1)
        CType(Tab_Manu_Riepilogo.FindControl("txtOneriC_MANO"), HiddenField).Value = par.IfNull(myReader1("IMPORTO_ONERI_CONS"), -1)


        'PENALE
        Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
        par.cmd.CommandText = "select * from SISCOM_MI.APPALTI_PENALI where ID_MANUTENZIONE=" & vIdManutenzione
        myReaderTMP = par.cmd.ExecuteReader()

        If myReaderTMP.Read Then

            CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(myReaderTMP("IMPORTO"), 0), "", "##,##0.00")

            Me.txtPenaleOLD.Value = IsNumFormat(par.IfNull(myReaderTMP("IMPORTO"), 0), "", "##,##0.00")
            Me.txtIdPenale.Value = par.IfNull(myReaderTMP("ID"), -1)
        End If
        myReaderTMP.Close()
        '***********************

        Me.txtPercIVA_P.Value = par.IfNull(myReader1("IVA_CONSUMO_P"), 0)   'IVA PREVENTIVO
        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).SelectedValue = par.SetCmbIva(CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList), Me.txtPercIVA_P.Value)

        Me.txtPercIVA_C.Value = par.IfNull(myReader1("IVA_CONSUMO"), 0)   'IVA CONSUNTIVO
        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).SelectedValue = par.SetCmbIva(CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList), Me.txtPercIVA_C.Value)

        'CType(Tab_Manu_Riepilogo.FindControl("lblIVAC"), Label).Text = "IVA (" & Me.txtPercIVA.Value & "%)"


        CalcolaImporti(par.IfNull(myReader1("IMPORTO_PRESUNTO"), 0), par.IfEmpty(Me.txtPercOneri.Value, 0), par.IfEmpty(Me.txtScontoConsumo.Value, 0), par.IfEmpty(Me.txtPercIVA_P.Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))

        'txtID_Segnalazioni.Value = par.IfNull(myReader1("ID_SEGNALAZIONI"), -1)
        Me.txtID_Pagamento.Value = par.IfNull(myReader1("ID_PAGAMENTO"), -1)
        Me.txtID_Prenotazione.Value = par.IfNull(myReader1("ID_PRENOTAZIONE_PAGAMENTO"), -1)

        'PERCENTUALE ONERI DI SICUREZZA
        par.cmd.CommandText = " SELECT ROUND(ONERI_SICUREZZA_CONSUMO/IMPORTO_CONSUMO*100,4) FROM SISCOM_MI.APPALTI_VOCI_PF" _
            & " WHERE IMPORTO_CONSUMO>0 AND ID_APPALTO=" & idApp & " AND ID_PF_VOCE=" & cmbVocePF.SelectedValue
        Dim percentualeOneriSicurezza As Decimal = -1
        percentualeOneriSicurezza = par.IfNull(par.cmd.ExecuteScalar, -1)
        If IsNumeric(percentualeOneriSicurezza) AndAlso percentualeOneriSicurezza <> -1 Then
            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("lblOneri"), Label).Text = "Oneri di Sicurezza (" & percentualeOneriSicurezza & "%)"
            CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("lblOneriC"), Label).Text = "Oneri di Sicurezza (" & percentualeOneriSicurezza & "%)"
        End If


        Me.lblODL_INTEGRAZIONE.Visible = False
        Me.lblOLDINTEGRAZIONE.Visible = False
        Me.lblODL_INTEGRATO.Visible = False
        Me.lblOLDINTEGRATO.Visible = False


        If txtSTATO.Value = 3 Then
            'MANUTENZIONE INTEGRATA
            par.cmd.CommandText = "select PROGR,ANNO from SISCOM_MI.MANUTENZIONI where ID=" & par.IfNull(myReader1("ID_FIGLIO"), "-1")
            myReaderTMP = par.cmd.ExecuteReader()

            If myReaderTMP.Read Then
                Me.txtIntegrato.Value = par.IfNull(myReader1("ID_FIGLIO"), -1)

                Me.lblODL_INTEGRATO.Visible = True
                Me.lblODL_INTEGRATO.Text = "INTEGRATO DAL N° "
                Me.lblOLDINTEGRATO.Visible = True
                Me.lblOLDINTEGRATO.Text = "<a href=""javascript:Integrato();"">" & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & "</a>"

            End If
            myReaderTMP.Close()


            par.cmd.CommandText = "select ID,PROGR,ANNO from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & par.IfNull(myReader1("ID"), "-1")
            myReaderTMP = par.cmd.ExecuteReader()

            If myReaderTMP.Read Then
                Me.txtIntegrazione.Value = par.IfNull(myReaderTMP("ID"), -1)

                'MANUTENZIONE INTEGRAZIONE 
                Me.lblODL_INTEGRAZIONE.Visible = True
                Me.lblODL_INTEGRAZIONE.Text = "INTEGRAZIONE DEL N° "
                Me.lblOLDINTEGRAZIONE.Visible = True
                Me.lblOLDINTEGRAZIONE.Text = "<a href=""javascript:Integrazione();"">" & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & "</a>"

            End If
            myReaderTMP.Close()

        Else

            par.cmd.CommandText = "select ID,PROGR,ANNO from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & par.IfNull(myReader1("ID"), "-1")
            myReaderTMP = par.cmd.ExecuteReader()

            If myReaderTMP.Read Then
                Me.txtIntegrazione.Value = par.IfNull(myReaderTMP("ID"), -1)

                'MANUTENZIONE INTEGRAZIONE 
                Me.lblODL_INTEGRAZIONE.Visible = True
                Me.lblODL_INTEGRAZIONE.Text = "INTEGRAZIONE DEL N° "
                Me.lblOLDINTEGRAZIONE.Visible = True
                Me.lblOLDINTEGRAZIONE.Text = "<a href=""javascript:Integrazione();"">" & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & "</a>"


            End If
            myReaderTMP.Close()

        End If

        'ABILITO/DISABILITO CAMPI in base allo stato
        AbilitaDisabilita()


        If Me.txtSTATO.Value = 2 Or Me.txtSTATO.Value = 4 Then
            Me.lblDataInizio.Text = "Data Effettiva Inizio Lavori:"
            Me.lblDataFine.Text = "Data Effettiva Fine Lavori"
        Else
            Me.lblDataInizio.Text = "Data Presunta Inizio Lavori:"
            Me.lblDataFine.Text = "Data Presunta Fine Lavori"
        End If

    End Sub

    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdManutenzione <> 0 Then
                ' LEGGO MANUTENZIONI

                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")

            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Manutenzione aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If

                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                Me.txtVisualizza.Value = 2   ' solo consultazione allegati

                Me.BLOCCATO.Value = "1"      'BLOCCATO da ALTRI UTENTI

            Else
                par.OracleConn.Close()

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

        If vIdManutenzione = 0 Then
            Me.Salva()
        Else
            Me.Update()
        End If
        CType(Tab_Manu_Dettagli, Object).BindGrid_Interventi()
        'MODIFICA MARCO 13/01/2015
        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        cmbStato.Enabled = True
        If cmbStato.SelectedValue = 2 Then
            btnOrdineIntegrativo.Visible = False
        End If
        If autorizzazione.Value = "0" Then
            cmbStato.Enabled = False
        End If

        'max 06/07/2016
        CaricaAllegati(vIdManutenzione)
        CaricaIrregolarita(vIdManutenzione)
        CaricaEventi(vIdManutenzione)

        '~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    End Sub


    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

        'If Me.cmbIndirizzo.SelectedValue = -1 Then
        '    Response.Write("<script>alert('Selezionare un Complesso Immobiliare!');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        If Me.cmbVocePF.SelectedValue = -1 Then
            RadWindowManager1.RadAlert("Selezionare la Voce del Piano Finanziario!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If


        If Me.txtIdAppalto.Value = -1 Then
            RadWindowManager1.RadAlert("Selezionare il numero di repertorio!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If Me.txtID_Fornitore.Value = -1 Then
            RadWindowManager1.RadAlert("Selezionare il fornitore!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).SelectedValue = -1 Then
            RadWindowManager1.RadAlert("Selezionare la percentuale dell\'iva del preventivo!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        ' IVA 21%INIZIO *****************+
        'PER QUELLI NUOVI L'IVA del 20% non esiste più
        If CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = True Then
            If CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).SelectedValue = 20 Then
                RadWindowManager1.RadAlert("Attenzione... Stai utilizzando l\'aliquota iva del preventivo del 20%!", 300, 150, "Attenzione", "", "null")
                'ControlloCampi = False
                'Exit Function
            End If
        End If

        If par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0) > 0 Then
            If CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = True Then
                If CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).SelectedValue = 20 Then
                    RadWindowManager1.RadAlert("Attenzione... Stai utilizzando l\'aliquota iva del preventivo del 20%!", 300, 150, "Attenzione", "", "null")
                    'Response.Write("<script>alert('Selezionare la percentuale dell\'iva del consuntivo diversa dal 20%!');</script>")
                    'ControlloCampi = False
                    'Exit Function
                End If
            End If
        End If
        'IVA 21% FINE ******************************
        Dim statoVecchio As String = cmbStato.SelectedValue
        If ChkAutorizzazioneEmissione.Checked And cmbStato.SelectedValue = "0" Then
            cmbStato.SelectedValue = "1"
        End If

        Select Case Me.cmbStato.SelectedValue
            Case 0  'BOZZA
                If txtSTATO.Value <> 0 Then
                    RadWindowManager1.RadAlert("Ordine emesso, impossibile ritornare a bozza!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If

            Case 1  'EMESSO ORDINE

                If CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).SelectedValue = -1 Then
                    RadWindowManager1.RadAlert("Selezionare la percentuale dell\'iva del consuntivo!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If

                If txtSTATO.Value = 2 Then
                    If par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0) > 0 Then
                        RadWindowManager1.RadAlert("In fase di consuntivo, impossibile ritornare a ordine emesso!", 300, 150, "Attenzione", "", "null")
                        ControlloCampi = False
                        cmbStato.SelectedValue = statoVecchio
                        Exit Function
                    End If
                End If


                If IsNothing(Me.txtDataInizio.SelectedDate) Then
                    RadWindowManager1.RadAlert("Inserire la Data Presunta di Inizio Lavori!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    txtDataInizio.Focus()
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If


                If Me.txtStatoPF.Value > "5" Then
                    'SE USO ES vecchi (esempio: 6 o 7 del 2011) posso comunque fare lavori nel 2012
                    ' prima andavo a prendere l'anno dell'Es approvato, ora in data 16/01/2012 tolto perchè non hanno ancora ES approvati
                    'l'anno per 6 e 7, se nuovi, l'anno deve essere uguale a MANUTENZIONI.ANNO (ovvero 2012 anno del sistema) (19/01/2012)

                    'If Left(par.AggiustaData(Me.txtDataInizio.Text), 4) <> TrovaANNO_ES_APPROVATO() Then
                    '    Response.Write("<script>alert('La Data Effettiva di Inizio Lavori deve cadere nell\'anno finanziario in corso!');</script>")
                    '    ControlloCampi = False
                    '    txtDataInizio.Focus()
                    '    Exit Function
                    'End If


                    'MODIFICA MARCO 26/07/2012
                    'If voce_fl_cc.Value = 0 Then

                    'If par.IfEmpty(Me.txtAnnoManutenzione.Value, "") <> "" Then
                    '    If Left(par.AggiustaData(Me.txtDataInizio.SelectedDate), 4) <> Me.txtAnnoManutenzione.Value Then
                    '        RadWindowManager1.RadAlert("La Data Presunta Inizio Lavori deve cadere nel: " & Me.txtAnnoManutenzione.Value & "!", 300, 150, "Attenzione", "", "null")
                    '        ControlloCampi = False
                    '        txtDataInizio.Focus()
                    '        cmbStato.SelectedValue = statoVecchio
                    '        Exit Function
                    '    End If
                    'Else
                    '    If Left(par.AggiustaData(Me.txtDataInizio.SelectedDate), 4) <> DateTime.Now.Year Then
                    '        RadWindowManager1.RadAlert("Attenzione...L\'anno della data Presunta di Inizio Lavori è diverso dal: " & DateTime.Now.Year & "!", 300, 150, "Attenzione", "", "null")
                    '        txtDataInizio.Focus()
                    '        cmbStato.SelectedValue = statoVecchio
                    '        Exit Function

                    '    End If
                    'End If
                    'End If

                Else
                    If Left(par.AggiustaData(Me.txtDataInizio.SelectedDate), 4) <> Me.txtAnnoEsercizioF.Value Then

                        'Response.Write("<script>alert('La Data Effettiva di Inizio Lavori deve cadere nell\'anno finanziario in corso!');</script>")
                        'ControlloCampi = False
                        'txtDataInizio.Focus()
                        'Exit Function
                    End If
                End If


                If par.IfEmpty(Me.txtDataFine.SelectedDate.ToString, "Null") = "Null" Then
                    RadWindowManager1.RadAlert("Inserire la Data Presunta di Fine Lavori!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    txtDataFine.Focus()
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If

                If par.AggiustaData(Me.txtDataFine.SelectedDate) < par.AggiustaData(Me.txtDataInizio.SelectedDate) Then
                    RadWindowManager1.RadAlert("La data di fine lavori deve essere successiva a: " & Me.txtDataInizio.SelectedDate, 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If


                If par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text, "Null") = "Null" Then
                    RadWindowManager1.RadAlert("Inserire la descrizione dell\'intervento di manutenzione!", 300, 150, "Attenzione", "", "null")
                    cmbStato.SelectedValue = statoVecchio
                    ControlloCampi = False
                    Exit Function
                End If

                'If par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text, 0) = 0 Then
                '    Response.Write("<script>alert('Inserire un intervento di manutenzione!');</script>")
                '    ControlloCampi = False
                '    Exit Function
                'End If



            Case 2  'CONSUNTIVO (FINE)


                If IsNothing(Me.txtDataInizio.SelectedDate) Then
                    RadWindowManager1.RadAlert("Inserire la Data Effettiva di Inizio Lavori!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    txtDataInizio.Focus()
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If


                If Me.txtStatoPF.Value > "5" Then
                    'SE USO ES vecchi (esempio: 6 o 7 del 2011) posso comunque fare lavori nel 2012
                    ' prima andavo a prendere l'anno dell'Es approvato, ora in data 16/01/2012 tolto perchè non hanno ancora ES approvati
                    'l'anno per 6 e 7, se nuovi, l'anno deve essere uguale a MANUTENZIONI.ANNO (ovvero 2012 anno del sistema) (19/01/2012)

                    'If Left(par.AggiustaData(Me.txtDataInizio.Text), 4) <> TrovaANNO_ES_APPROVATO() Then
                    '    Response.Write("<script>alert('La Data Effettiva di Inizio Lavori deve cadere nell\'anno finanziario in corso!');</script>")
                    '    ControlloCampi = False
                    '    txtDataInizio.Focus()
                    '    Exit Function
                    'End If

                    'If voce_fl_cc.Value = 0 Then

                    If par.IfEmpty(Me.txtAnnoManutenzione.Value, "") <> "" Then
                        'If par.IfEmpty(Me.txtAnnoManutenzione.Value, "") <> "" Then
                        '    If Left(par.AggiustaData(Me.txtDataInizio.SelectedDate), 4) <> Me.txtAnnoManutenzione.Value Then
                        '        RadWindowManager1.RadAlert("La Data Effettiva Inizio Lavori deve cadere nel: " & Me.txtAnnoManutenzione.Value & "!", 300, 150, "Attenzione", "", "null")
                        '        ControlloCampi = False
                        '        txtDataInizio.Focus()
                        '        cmbStato.SelectedValue = statoVecchio
                        '        Exit Function
                        '    End If
                        'Else
                        '    If Left(par.AggiustaData(Me.txtDataInizio.SelectedDate), 4) <> DateTime.Now.Year Then
                        '        RadWindowManager1.RadAlert("Attenzione...L\'anno della data Effettiva di Inizio Lavori è diverso dal: " & DateTime.Now.Year & "!", 300, 150, "Attenzione", "", "null")
                        '        txtDataInizio.Focus()
                        '        cmbStato.SelectedValue = statoVecchio
                        '    End If
                        'End If
                    End If
                    'End If

                Else
                    If Left(par.AggiustaData(Me.txtDataInizio.SelectedDate), 4) <> Me.txtAnnoEsercizioF.Value Then
                        'Response.Write("<script>alert('La Data Effettiva di Inizio Lavori deve cadere nell\'anno finanziario in corso!');</script>")
                        'ControlloCampi = False
                        'txtDataInizio.Focus()
                        cmbStato.SelectedValue = statoVecchio
                        'Exit Function
                    End If
                End If

                If IsNothing(Me.txtDataFine.SelectedDate) Then
                    RadWindowManager1.RadAlert("Inserire la Data Effettiva di Fine Lavori!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    txtDataFine.Focus()
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If

                If par.AggiustaData(Me.txtDataFine.SelectedDate) < par.AggiustaData(Me.txtDataInizio.SelectedDate) Then
                    RadWindowManager1.RadAlert("La data di fine lavori deve essere successiva a: " & Me.txtDataInizio.SelectedDate, 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    cmbStato.SelectedValue = statoVecchio
                    Exit Function
                End If

                If Format(Now, "yyyyMMdd") < par.AggiustaData(Me.txtDataInizio.SelectedDate) Then
                    RadWindowManager1.RadAlert("Non si può consuntivare prima della data di inizio lavori prevista per il: " & Me.txtDataInizio.SelectedDate, 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Me.cmbStato.SelectedValue = 1
                    Exit Function
                End If

                'If Format(Now, "yyyyMMdd") > par.AggiustaData(Me.txtDataFine.Text) Then
                '    Response.Write("<script>alert('Non si può consuntivare dopo la data di fine lavori prevista per il: " & Me.txtDataFine.Text & " ');</script>")
                '    ControlloCampi = False
                '    Me.cmbStato.SelectedValue = 1
                '    Exit Function
                'End If

                'If par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0) = 0 Then
                '    Response.Write("<script>alert('Inserire un consuntivo di manutenzione!');</script>")
                '    ControlloCampi = False
                '    Exit Function
                'End If

        End Select

    End Function

    'PRIMO SALVATAGGIO (stato= BOZZA)
    Private Sub Salva()
        Dim importo_tot As Decimal 'A netto compresi oneri e IVA + Ritenuta di legge al 5%

        Try



            ' MANUTENZIONE

            'IMPORTO_PRESUNTO        NUMBER, (dovrebbe essere il totale di dettagli)
            'REVERSIBILE             NUMBER(1)             DEFAULT 0,
            'COSTO                   NUMBER,
            'COSTO_REVERSIBILE       NUMBER

            'COME ERA PRIMA
            ' calcolo dell'ODL (ORA VIENE CALCOLATO AUTOMATICAMENTE x APPALTO
            'par.cmd.CommandText = "select PROGRESSIVO from SISCOM_MI.MANUTENZIONI_ODL where SISCOM_MI.MANUTENZIONI_ODL.ANNO='" & DateTime.Now.Year & "'"
            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    ODL = myReader2(0) + 1
            '    myReader2.Close()
            '    par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI_ODL set PROGRESSIVO=" & ODL & " where ANNO='" & DateTime.Now.Year & "'"

            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            'Else
            '    ODL = 1
            '    myReader2.Close()
            '    par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_ODL (PROGRESSIVO,ANNO) values " _
            '               & "(" & ODL & ",'" & DateTime.Now.Year & "')"

            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            'End If


            Me.txtPercIVA_C.Value = Me.txtPercIVA_P.Value
            CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).SelectedValue = par.SetCmbIva(CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList), Me.txtPercIVA_C.Value)


            'A netto compresi oneri e IVA + Ritenuta di legge al 5%
            If Me.cmbStato.SelectedValue = 1 Then
                'EMESSO
                importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenuta"), TextBox).Text.Replace(".", ""))

            ElseIf Me.cmbStato.SelectedValue = 2 Then
                'CONSUNTIVO
                importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVAC"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))
            End If

            par.cmd.Parameters.Clear()

            'Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
            '*********************************** modifica marco 23/07/2015 ***********************************
            Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value

            Dim flautorizzazione As String = "0"
            Dim inserimentoEventoAutorizzazione As Boolean = False
            Dim inserimentoEventoAutorizzazioneTolta As Boolean = False
            Dim flcontinuaAutorizz As Boolean = False
            If ChkAutorizzazioneEmissione.Checked = True Then
                If cmbStato.SelectedValue = 0 Or cmbStato.SelectedValue = 1 Then
                    If CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items.Count > 0 Then
                        If ControlloCampiInterventi() = True Then
                            If Setta_ImportoResiduo() = True Then
                                For Each elementO As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                                    Dim dett As String = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    If dett <> "" Then
                                        If IsNumeric(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) AndAlso CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) > 0 Then
                                            flcontinuaAutorizz = True
                                            cmbStato.ClearSelection()
                                            cmbStato.SelectedValue = 1
                                            txtSTATO.Value = 1
                                        Else
                                            RadWindowManager1.RadAlert("Inserire correttamente gli importi degli interventi!", 300, 150, "Attenzione", "", "null")
                                            Exit Sub
                                        End If
                                    End If
                                Next
                                par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE, "Da Bozza ad Emesso Ordine")
                            Else
                                Dim val1 As Decimal
                                val1 = Math.Round(CDbl(strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))), 2) - Math.Round(importo_tot, 2)
                                If val1 < 0 Then
                                    Me.cmbStato.SelectedValue = 1
                                    RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
                                    Exit Sub
                                End If
                            End If
                        Else
                            Exit Sub
                        End If
                    Else
                        cmbStato.SelectedValue = 0
                        txtSTATO.Value = 0
                        Response.Write("<script>alert('Inserire almeno un dettaglio prima di emettere l\'ordine!');</script>")
                        Exit Sub

                    End If
                End If
                flautorizzazione = "1"
                If autorizzazioneIniziale = 0 Then
                    inserimentoEventoAutorizzazione = True
                    operatoreIniziale.Value = Session.Item("ID_OPERATORE")
                End If
            Else
                If autorizzazioneIniziale = 1 Then
                    inserimentoEventoAutorizzazioneTolta = True
                    operatoreIniziale.Value = 0
                End If
            End If
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            ' '' Ricavo vIdManutenzione
            par.cmd.CommandText = " select SISCOM_MI.SEQ_MANUTENZIONI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdManutenzione = myReader1(0)
            End If
            myReader1.Close()

            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            Me.txtIdManutenzione.Text = vIdManutenzione

            CType(Tab_Manu_Dettagli.FindControl("txtIdManuPadre"), HiddenField).Value = vIdManutenzione
            par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI " _
                                        & " (ID,ID_COMPLESSO,ID_EDIFICIO,ID_SCALA,ID_PF_VOCE,ID_APPALTO,DESCRIZIONE," _
                                        & " DATA_INIZIO_ORDINE,DATA_FINE_ORDINE,DATA_INIZIO_INTERVENTO,DATA_FINE_INTERVENTO,STATO,IMPORTO_CONSUNTIVATO,IMPORTO_PRESUNTO," _
                                        & " ID_SEGNALAZIONI,ID_PIANO_FINANZIARIO,RIT_LEGGE,RIMBORSI,IMPORTO_TOT,IMPORTO_RESIDUO,ID_UNITA_IMMOBILIARI,IVA_CONSUMO_P,IVA_CONSUMO,DANNEGGIANTE,DANNEGGIATO,ID_STRUTTURA,IMPORTO_ONERI_PREV,IMPORTO_ONERI_CONS,fl_autorizzazione,operatore_autorizzazione,ID_FORNITORE_STAMPA) " _
                                & "values (:id,:id_complesso,:id_edificio,:id_scala,:id_voce,:id_appalto,:descrizione," _
                                        & " :data_inizio,:data_fine,:data_inizio_inter,:data_fine_inter,:stato,:imp_consuntivo,:imp_presunto," _
                                        & " :id_segnalazioni,:id_piano_finanziario,:rit_legge,:rimborsi,:importo_tot,:importo_residuo,:id_unita_immobiliare,:iva_consumoP,:iva_consumoC,:danneggiante,:danneggiato,:id_struttura,:imp_oneri_presunto,:imp_oneri_consuntivo,:fl_autorizzazione,:operatore_autorizzazione,:ID_FORNITORE_STAMPA) "

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdManutenzione))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(txtIdComplesso.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(txtIdEdificio.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbScala.SelectedValue, -1)))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", Convert.ToInt32(Me.cmbVocePF.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text, 4000)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_inizio", par.AggiustaData(Me.lblData.Text)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_fine", ""))
            Dim dataInizio As String = ""
            If Not IsNothing(txtDataInizio.SelectedDate) Then
                dataInizio = txtDataInizio.SelectedDate
            End If
            Dim dataFine As String = ""
            If Not IsNothing(txtDataFine.SelectedDate) Then
                dataFine = txtDataFine.SelectedDate
            End If
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_inizio_inter", par.AggiustaData(dataInizio)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_fine_inter", par.AggiustaData(dataFine)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_consuntivo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_presunto", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text.Replace(".", ""))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_segnalazioni", RitornaNullSeIntegerMenoUno(Me.txtID_Segnalazioni.Value)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_piano_finanziario", RitornaNullSeIntegerMenoUno(Me.txtIdPianoFinanziario.Value)))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rimborsi", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text.Replace(".", ""))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_tot", strToNumber(importo_tot)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_residuo", strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita_immobiliare", RitornaNullSeIntegerMenoUno(Me.txtID_Unita.Value)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumoP", Convert.ToInt32(Me.txtPercIVA_P.Value)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumoC", Convert.ToInt32(Me.txtPercIVA_C.Value)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("danneggiante", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDanneggiante"), TextBox).Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("danneggiato", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDanneggiato"), TextBox).Text, 500)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Session.Item("ID_STRUTTURA"))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_oneri_presunto", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtOneriP_MANO"), HiddenField).Value.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_oneri_consuntivo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtOneriC_MANO"), HiddenField).Value.Replace(".", ""))))

            Dim idFS As Integer = 0
            If IsNumeric(cmbIntestazione.SelectedValue) AndAlso CInt(cmbIntestazione.SelectedValue) > 0 Then
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_FORNITORE_STAMPA", Convert.ToInt32(cmbIntestazione.SelectedValue)))
            Else
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_FORNITORE_STAMPA", DBNull.Value))
            End If


            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_autorizzazione", flautorizzazione))
            If operatoreIniziale.Value = 0 Then
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_autorizzazione", DBNull.Value))
            Else
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_autorizzazione", operatoreIniziale.Value))
            End If
            par.cmd.ExecuteNonQuery()
            autorizzazione.Value = flautorizzazione
            autorizzazioneIniziale = autorizzazione.Value
            If cmbStato.SelectedValue >= 1 Then
                ChkAutorizzazioneEmissione.Enabled = False
            End If
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            '*****************************************

            If Me.cmbStato.SelectedValue <> stato Then
                If Setta_ImportoResiduo() = False Then
                    Me.cmbStato.SelectedValue = 1
                    Response.Write("<script>alert('L\'importo inserito è superiore all\'importo contrattuale residuo!');</script>")
                    Exit Sub
                ElseIf stato = 0 Then
                    Dim val1 As Decimal
                    val1 = Math.Round(CDbl(strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))), 2) - Math.Round(importo_tot, 2)
                    If val1 < 0 Then
                        Me.cmbStato.SelectedValue = 1
                        Response.Write("<script>alert('L\'importo inserito è superiore all\'importo contrattuale residuo!');</script>")
                        Exit Sub
                    Else
                        Select Case Me.cmbStato.SelectedValue
                            Case 1
                                sAggiornaSEFO = "INSERT INTO SISCOM_MI.SEGNALAZIONI_FORNITORI (ID,ID_SEGNALAZIONE,ID_FORNITORE,ID_STATO,ID_APPALTO,ID_MANUTENZIONE) VALUES (SISCOM_MI.SEQ_SEGNALAZIONI_FORNITORI.NEXTVAL,NULL," & Me.txtID_Fornitore.Value & ",1," & Convert.ToInt32(Me.txtIdAppalto.Value) & "," & vIdManutenzione & ")"
                            Case 2
                                sAggiornaSEFO = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO=7 WHERE ID_MANUTENZIONE=" & vIdManutenzione

                        End Select
                    End If
                End If
            End If
            If sAggiornaSEFO <> "" Then
                par.cmd.CommandText = sAggiornaSEFO
                par.cmd.ExecuteNonQuery()
            End If


            'vIdSegnalazione = par.IfNull(RitornaNullSeIntegerMenoUno(Me.txtID_Segnalazioni.Value), 0)


            '****** LETTURA ODL
            par.cmd.CommandText = "select PROGR,ANNO from SISCOM_MI.MANUTENZIONI where ID=" & vIdManutenzione
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then

                lblODL1.Text = par.IfNull(myReader2("PROGR"), "") & "/" & par.IfNull(myReader2("ANNO"), "")
                lblData.Text = DateTime.Now.Date
                Me.txtAnnoManutenzione.Value = par.IfNull(myReader2("ANNO"), "")
            End If
            myReader2.Close()
            '*****************************************

            '*** EVENTI_MANUTENZIONE
            par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_MANUTENZIONE, "")
            If inserimentoEventoAutorizzazione = True Then
                par.cmd.CommandText = "Insert into SISCOM_MI.EVENTI_MANUTENZIONE " _
                        & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                        & " Values " _
                        & " (" & vIdManutenzione & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F247', 'Autorizzazione ODL') "
                par.cmd.ExecuteNonQuery()
            End If
            If inserimentoEventoAutorizzazioneTolta = True Then
                par.cmd.CommandText = "Insert into SISCOM_MI.EVENTI_MANUTENZIONE " _
                        & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                        & " Values " _
                        & " (" & vIdManutenzione & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F247', 'Revoca autorizzazione ODL') "
                par.cmd.ExecuteNonQuery()
            End If


            If ControlloCampiInterventi() = True Then
                ' MANUTENZIONI_INTERVENTI
                '****************************************** MODIFICA MARCO 23/07/2015 ******************************************
                Select Case CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value
                    Case "0", "1"
                        'bozza
                        Dim TIPOLOGIA As String = ""
                        Dim IMPORTO_PRESUNTO As String = ""
                        Dim ID_UNITA_IMMOBILIARE As String = "NULL"
                        Dim ID_UNITA_COMUNE As String = "NULL"
                        Dim ID_MANUTENZIONE As String = ""
                        Dim ID_IMPIANTO As String = "NULL"
                        Dim ID_EDIFICIO As String = "NULL"
                        Dim ID_COMPLESSO As String = "NULL"
                        Dim ID_SCALA As String = "NULL"
                        Dim ID As String = ""
                        Dim FL_BLOCCATO As String = "0"
                        For Each elementO As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                            ID_UNITA_IMMOBILIARE = "NULL"
                            ID_UNITA_COMUNE = "NULL"
                            ID_IMPIANTO = "NULL"
                            ID_EDIFICIO = "NULL"
                            ID_COMPLESSO = "NULL"
                            ID_SCALA = "NULL"
                            TIPOLOGIA = CType(elementO.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue.ToString
                            Dim dett As String = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                            If dett <> "" Then
                                If IsNumeric(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) AndAlso CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) > 0 Then
                                    IMPORTO_PRESUNTO = CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString)
                                    Select Case TIPOLOGIA
                                        Case "COMPLESSO"
                                            ID_COMPLESSO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "SCALA"
                                            ID_SCALA = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "EDIFICIO"
                                            ID_EDIFICIO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "UNITA IMMOBILIARE"
                                            ID_UNITA_IMMOBILIARE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "UNITA COMUNE"
                                            ID_UNITA_COMUNE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "CENTRALE TERMICA"
                                            ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "SOLLEVAMENTO"
                                            ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "TELERISCALDAMENTO"
                                            ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    End Select
                                    ID_MANUTENZIONE = vIdManutenzione
                                    ID = par.IfNull(elementO.Cells(par.IndRDGC(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid), "ID")).Text.Replace("&nbsp;", ""), "0")
                                    FL_BLOCCATO = "0"
                                    If IsNumeric(ID) AndAlso CDec(ID) > 0 Then
                                        'update
                                        par.cmd.CommandText = " UPDATE SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & " SET TIPOLOGIA        = '" & TIPOLOGIA & "', " _
                                            & " IMPORTO_PRESUNTO     = " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                            & " ID_UNITA_IMMOBILIARE = " & ID_UNITA_IMMOBILIARE & ", " _
                                            & " ID_UNITA_COMUNE      = " & ID_UNITA_COMUNE & ", " _
                                            & " ID_MANUTENZIONE      = " & ID_MANUTENZIONE & ", " _
                                            & " ID_IMPIANTO          = " & ID_IMPIANTO & ", " _
                                            & " ID_EDIFICIO          = " & ID_EDIFICIO & ", " _
                                            & " ID_COMPLESSO         = " & ID_COMPLESSO & ", " _
                                            & " ID_SCALA             = " & ID_SCALA & ", " _
                                            & " FL_BLOCCATO          = " & FL_BLOCCATO _
                                            & " WHERE  ID            = " & ID
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        'inserimento
                                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_INTERVENTI ( " _
                                            & " TIPOLOGIA, IMPORTO_PRESUNTO, ID_UNITA_IMMOBILIARE,  " _
                                            & " ID_UNITA_COMUNE, ID_MANUTENZIONE, ID_IMPIANTO,  " _
                                            & " ID_EDIFICIO, ID_COMPLESSO, ID, ID_SCALA,  " _
                                            & " FL_BLOCCATO)  " _
                                            & " VALUES ('" & TIPOLOGIA & "', " _
                                            & " " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                            & ID_UNITA_IMMOBILIARE & ", " _
                                            & ID_UNITA_COMUNE & ", " _
                                            & ID_MANUTENZIONE & ", " _
                                            & ID_IMPIANTO & ", " _
                                            & ID_EDIFICIO & ", " _
                                            & ID_COMPLESSO & ", " _
                                            & "SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL, " _
                                            & ID_SCALA & ", " _
                                            & "0)"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                Else
                                    RadWindowManager1.RadAlert("Inserire correttamente gli importi degli interventi!", 300, 150, "Attenzione", "", "null")
                                    Exit Sub
                                End If
                            Else

                                RadWindowManager1.RadAlert("Inserire correttamente il dettaglio oggetto!", 300, 150, "Attenzione", "", "null")
                                Exit Sub
                            End If
                        Next
                End Select
                '*** SOMMA IMPORTO
                Dim SommaTot As Decimal = 0
                For Each elemento As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                    If IsNumeric(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) > 0 Then
                        SommaTot += CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                    End If
                Next
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
                CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")
                CalcolaImporti(SommaTot, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
                '**********************
                CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
                AbilitaDisabilita()
            Else
                Exit Sub
            End If
            'Dim lstInterventi As System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi)

            'lstInterventi = CType(HttpContext.Current.Session.Item("LSTINTERVENTI"), System.Collections.Generic.List(Of Epifani.Manutenzioni_Interventi))

            'For Each gen As Epifani.Manutenzioni_Interventi In lstInterventi

            '    par.cmd.Parameters.Clear()
            '    par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_INTERVENTI  " _
            '                                & " (ID, ID_MANUTENZIONE,ID_IMPIANTO,ID_COMPLESSO,ID_EDIFICIO,ID_UNITA_IMMOBILIARE,ID_UNITA_COMUNE,TIPOLOGIA,IMPORTO_PRESUNTO) " _
            '                        & "values (SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL,:id_manu,:id_impianto,:id_complesso,:id_edificio,:id_unita,:id_comune,:tipologia,:importo) "

            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manu", vIdManutenzione))


            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", RitornaNullSeIntegerMenoUno(gen.ID_IMPIANTO)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(gen.ID_COMPLESSO)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(gen.ID_EDIFICIO)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(gen.ID_UNITA_IMMOBILIARE)))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_comune", RitornaNullSeIntegerMenoUno(gen.ID_UNITA_COMUNE)))

            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", gen.TIPOLOGIA))
            '    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(gen.IMPORTO_PRESUNTO.Replace(".", ""))))

            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            '    par.cmd.Parameters.Clear()

            '    '*** EVENTI_MANUTENZIONE
            '    par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_DETTAGLI_MANUTENZIONE, "Intervento di Manutenzione")

            'Next
            '*******************************************************************************************************
            txtSTATO.Value = Me.cmbStato.SelectedValue
            'SFITTI
            If Me.txtID_Unita.Value <> -1 Then
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.V_ALLOGGI_SFITTI set ID_MANUTENZIONE=" & vIdManutenzione _
                                    & " where ID_UNITA=" & Me.txtID_Unita.Value

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
            End If
            '*****************************

            'PRENOTAZIONI
            GestionePrenotazioni()

            'PENALE
            GestionePENALE()

            ' IN CASO DI SEGNALAZIONE
            'sValoreProvenienza = Request.QueryString("PROVENIENZA")

            'If sValoreProvenienza = "SEGNALAZIONI" Then
            '    par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=4  where ID=" & txtID_Segnalazioni.Value
            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""

            '    Dim sLink As String

            '    If Me.txtID_Prenotazione.Value > 0 Then
            '        sLink = "<a href='../CICLO_PASSIVO/CICLOPASSIVO/MANUTENZIONI/StampaOrdine.aspx?COD=" & vIdManutenzione & "' target='_blank'>Emesso Ordine di Lavoro. Clicca per Visualizzare</a>"
            '    Else
            '        sLink = "Creato l'ordine di lavoro num: " & Me.lblODL1.Text & " in data: " & Me.lblData.Text
            '    End If

            '    par.cmd.CommandText = "insert into SISCOM_MI.SEGNALAZIONI_NOTE " _
            '                & " (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) values " _
            '                & " (" & Me.txtID_Segnalazioni.Value & ",'" & par.PulisciStrSql(sLink) & "','" _
            '                & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""

            'End If


            ' COMMIT
            par.myTrans.Commit()
            par.cmd.CommandText = ""


            '**** AGGIORNO I TAB con GRIGLIE

            '*** MANUTENZIONI_INTERVENTI

            Dim sSQL_DettaglioIMPIANTO As String
            sSQL_DettaglioIMPIANTO = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                        & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                                & "(select  (CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                                & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                        & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END) " _
                                                & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                        & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " ELSE '' " _
                                    & " END) as DETTAGLIO "


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                       & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                      & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
                 & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                       & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                       & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                       & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                       & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                 & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                      & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                      & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                      & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                      & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                      & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                       & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                      & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                      & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                      & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                      & "       SISCOM_MI.EDIFICI.DENOMINAZIONE||' - Scala '||SCALE_EDIFICI.descrizione  AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                      & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                        & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                      & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                      & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI " _
                      & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                      & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID " _
                & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                       & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                        & " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                       & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                 & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                      & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                      & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,'' AS TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                & "      '' AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
& " TRIM (TO_CHAR ((SELECT SUM (SISCOM_MI.MANUTENZIONI_CONSUNTIVI.perc_iva_rimborso) FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
& "       WHERE     SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI = SISCOM_MI.MANUTENZIONI_INTERVENTI.ID " _
& "       AND SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'), '9G999G999G999G999G990D99')) AS ""perc_iva_rimborso"", " _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
             & " from SISCOM_MI.MANUTENZIONI_INTERVENTI   " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and (SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA is null or tipologia='-1') "




            Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim ds1 As New Data.DataTable()

            da1.Fill(ds1) ', "MANUTENZIONI_INTERVENTI")


            CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).DataSource = ds1
            CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).DataBind()

            da1.Dispose()
            ds1.Dispose()

            If CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items.Count = 1 Then
                'CType(Tab_Manu_Dettagli.FindControl("txtSel1"), TextBox).Text = par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), DataGrid).Items(0).Cells(1).Text, "")
                CType(Tab_Manu_Dettagli.FindControl("txtIdComponente"), TextBox).Text = par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items(0).Cells(2).Text, "")
                CType(Tab_Manu_Dettagli.FindControl("txt_FL_BLOCCATO"), HiddenField).Value = par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items(0).Cells(13).Text, "")
            End If

            ''*******************************

            'ABILITO/DISABILITO CAMPI in base allo stato
            If Me.txtID_Unita.Value <> -1 Then
                Me.txtTIPO.Value = 1    'Appare il bottene "Unità Immobiliare" Visualizza la scheda dell unita
            Else
                Me.txtTIPO.Value = 0
            End If

            Me.cmbStato.Enabled = True
            AbilitaDisabilita()


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI  where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                RiempiCampi(myReader1)
            End If
            myReader1.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            RadNotificationNote.Text = "Operazione completata correttamente!"
            RadNotificationNote.Show()
            TabberHide = "tabbertab"

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

            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    'Successivi SALVATAGGIO (BOZZA,EMESSO o CONSUNTIVO)
    Private Sub Update()
        Dim importo_tot As Decimal 'A netto compresi oneri e IVA + Ritenuta di legge al 5%

        Try


            CType(Tab_Manu_Dettagli.FindControl("txtIdManuPadre"), HiddenField).Value = vIdManutenzione

            CalcolaImporti(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
            CalcolaImportiC(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))


            'A netto compresi oneri e IVA + Ritenuta di legge al 5%
            If Me.cmbStato.SelectedValue = 1 Then
                'EMESSO
                importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenuta"), TextBox).Text.Replace(".", ""))
                'ERRORE RICALCOLO IMPORTO_TOT
                Try
                    If importo_tot = 0 Then
                        Dim importo As Decimal = 0
                        For Each elemento As DataGridItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), DataGrid).Items
                            If IsNumeric(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) > 0 Then
                                importo += CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                            End If
                        Next
                        CalcolaImporti(importo, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
                        importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenuta"), TextBox).Text.Replace(".", ""))
                    End If
                Catch ex As Exception
                End Try

            ElseIf Me.cmbStato.SelectedValue = 2 Then
                'CONSUNTIVO
                importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVAC"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))
                'ERRORE RICALCOLO IMPORTO_TOT
                Try
                    If importo_tot = 0 Then
                        Dim importo As Decimal = 0
                        Dim rimborsi As Decimal = 0
                        For Each elemento As DataGridItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), DataGrid).Items
                            If IsNumeric(CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text) > 0 Then
                                importo += CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
                            End If
                            If IsNumeric(CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text) > 0 Then
                                rimborsi += CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text
                            End If
                        Next
                        ' CalcolaImportiC(importo, rimborsi, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
                        CalcolaImporti(importo, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
                        importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVAC"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))
                    End If
                Catch ex As Exception
                End Try
            End If
            sAggiornaSEFO = ""

            Dim stato As String = CType(Me.Page.FindControl("txtSTATO"), HiddenField).Value

            If Me.cmbStato.SelectedValue <> stato Then
                If Setta_ImportoResiduo() = False Then
                    Me.cmbStato.SelectedValue = 1
                    RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                ElseIf stato = 0 Then
                    Dim val1 As Decimal
                    val1 = Math.Round(CDbl(strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))), 2) - Math.Round(importo_tot, 2)
                    If val1 < 0 Then
                        Me.cmbStato.SelectedValue = 1
                        RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
                        Exit Sub
                    Else
                        Select Case Me.cmbStato.SelectedValue
                            Case 1

                                sAggiornaSEFO = "INSERT INTO SISCOM_MI.SEGNALAZIONI_FORNITORI (ID,ID_SEGNALAZIONE,ID_FORNITORE,ID_STATO,ID_APPALTO,ID_MANUTENZIONE) VALUES (SISCOM_MI.SEQ_SEGNALAZIONI_FORNITORI.NEXTVAL,NULL," & Me.txtID_Fornitore.Value & ",1," & Convert.ToInt32(Me.txtIdAppalto.Value) & "," & vIdManutenzione & ")"
                            Case 2
                                sAggiornaSEFO = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO=7 WHERE ID_MANUTENZIONE=" & vIdManutenzione



                        End Select
                    End If
                Else
                    Dim val1 As Decimal
                    val1 = Math.Round(CDbl(strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))), 2) - Math.Round(importo_tot, 2)
                    If val1 < 0 Then
                        Me.cmbStato.SelectedValue = 1
                        RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
                        Exit Sub
                    Else
                        Select Case Me.cmbStato.SelectedValue
                            Case 2
                                sAggiornaSEFO = "UPDATE SISCOM_MI.SEGNALAZIONI_FORNITORI SET ID_STATO=7 WHERE ID_MANUTENZIONE=" & vIdManutenzione



                        End Select
                    End If
                End If
            End If

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            If sAggiornaSEFO <> "" Then
                par.cmd.CommandText = sAggiornaSEFO
                par.cmd.ExecuteNonQuery()
            End If

            '*********************************** modifica marco 23/07/2015 ***********************************

            '*********************************** modifica marco 23/07/2015 ***********************************
            Dim flautorizzazione As String = "0"
            Dim inserimentoEventoAutorizzazione As Boolean = False
            Dim inserimentoEventoAutorizzazioneTolta As Boolean = False
            Dim flcontinuaAutorizz As Boolean = False
            If ChkAutorizzazioneEmissione.Checked = True Then
                If cmbStato.SelectedValue = 0 Or cmbStato.SelectedValue = 1 Then
                    If CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items.Count > 0 Then
                        If ControlloCampiInterventi() = True Then
                            If Setta_ImportoResiduo() = True Then
                                For Each elementO As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                                    Dim dett As String = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    If dett <> "" Then
                                        If IsNumeric(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) AndAlso CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) > 0 Then
                                            flcontinuaAutorizz = True
                                            cmbStato.ClearSelection()
                                            cmbStato.SelectedValue = 1
                                            txtSTATO.Value = 1
                                        Else
                                            RadWindowManager1.RadAlert("Inserire correttamente gli importi degli interventi!", 300, 150, "Attenzione", "", "null")
                                            Exit Sub
                                        End If
                                    End If
                                Next
                                par.cmd.CommandText = "SELECT COUNT(*) FROM SISCOM_MI.EVENTI_MANUTENZIONE " _
                                    & "WHERE ID_MANUTENZIONE = " & vIdManutenzione & " And COD_EVENTO = 'F" & Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE & "' " _
                                    & "AND UPPER(MOTIVAZIONE) = 'DA BOZZA AD EMESSO ORDINE'"
                                Dim eventoInserito As Integer = par.cmd.ExecuteScalar
                                If eventoInserito = 0 Then
                                par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE, "Da Bozza ad Emesso Ordine")
                                End If
                            Else
                                Dim val1 As Decimal
                                val1 = Math.Round(CDbl(strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))), 2) - Math.Round(importo_tot, 2)
                                If val1 < 0 Then
                                    Me.cmbStato.SelectedValue = 1
                                    RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
                                    Exit Sub
                                End If
                            End If

                        Else
                            Exit Sub
                        End If
                    Else
                        cmbStato.SelectedValue = 0
                        txtSTATO.Value = 0

                        RadWindowManager1.RadAlert("Inserire almeno un dettaglio prima di emettere l\'ordine!", 300, 150, "Attenzione", "", "null")
                        Exit Sub

                    End If
                End If
                flautorizzazione = "1"
                If autorizzazioneIniziale = 0 Then
                    inserimentoEventoAutorizzazione = True
                    operatoreIniziale.Value = Session.Item("ID_OPERATORE")
                End If
            Else
                If autorizzazioneIniziale = 1 Then
                    inserimentoEventoAutorizzazioneTolta = True
                    operatoreIniziale.Value = 0
                End If
            End If
            Dim esci As Boolean = False
            If stato > 0 Then
                If ControlloCampiConsuntivo() = False Then
                    esci = True
                End If
            Else
                If ControlloCampiInterventi() = False Then
                    esci = True
                End If
            End If
            If esci = False Then
                Select Case stato
                    Case "0"
                        'bozza
                        Dim TIPOLOGIA As String = ""
                        Dim IMPORTO_PRESUNTO As String = ""
                        Dim ID_UNITA_IMMOBILIARE As String = "NULL"
                        Dim ID_UNITA_COMUNE As String = "NULL"
                        Dim ID_MANUTENZIONE As String = ""
                        Dim ID_IMPIANTO As String = "NULL"
                        Dim ID_EDIFICIO As String = "NULL"
                        Dim ID_COMPLESSO As String = "NULL"
                        Dim ID_SCALA As String = "NULL"
                        Dim ID As String = ""
                        Dim FL_BLOCCATO As String = "0"
                        For Each elementO As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                            ID_UNITA_IMMOBILIARE = "NULL"
                            ID_UNITA_COMUNE = "NULL"
                            ID_IMPIANTO = "NULL"
                            ID_EDIFICIO = "NULL"
                            ID_COMPLESSO = "NULL"
                            ID_SCALA = "NULL"
                            TIPOLOGIA = CType(elementO.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue.ToString
                            Dim dett As String = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                            If dett <> "" Then
                                If IsNumeric(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) AndAlso CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString) > 0 Then
                                    IMPORTO_PRESUNTO = CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString)
                                    Select Case TIPOLOGIA
                                        Case "COMPLESSO"
                                            ID_COMPLESSO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "SCALA"
                                            ID_SCALA = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "EDIFICIO"
                                            ID_EDIFICIO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "UNITA IMMOBILIARE"
                                            ID_UNITA_IMMOBILIARE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "UNITA COMUNE"
                                            ID_UNITA_COMUNE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "CENTRALE TERMICA"
                                            ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "SOLLEVAMENTO"
                                            ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                        Case "TELERISCALDAMENTO"
                                            ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    End Select
                                    ID_MANUTENZIONE = vIdManutenzione
                                    ID = par.IfNull(elementO.Cells(par.IndRDGC(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid), "ID")).Text.Replace("&nbsp;", ""), "0")
                                    FL_BLOCCATO = "0"
                                    If IsNumeric(ID) AndAlso CDec(ID) > 0 Then
                                        'update
                                        par.cmd.CommandText = " UPDATE SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & " SET TIPOLOGIA        = '" & TIPOLOGIA & "', " _
                                            & " IMPORTO_PRESUNTO     = " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                            & " ID_UNITA_IMMOBILIARE = " & ID_UNITA_IMMOBILIARE & ", " _
                                            & " ID_UNITA_COMUNE      = " & ID_UNITA_COMUNE & ", " _
                                            & " ID_MANUTENZIONE      = " & ID_MANUTENZIONE & ", " _
                                            & " ID_IMPIANTO          = " & ID_IMPIANTO & ", " _
                                            & " ID_EDIFICIO          = " & ID_EDIFICIO & ", " _
                                            & " ID_COMPLESSO         = " & ID_COMPLESSO & ", " _
                                            & " ID_SCALA             = " & ID_SCALA & ", " _
                                            & " FL_BLOCCATO          = " & FL_BLOCCATO _
                                            & " WHERE  ID            = " & ID
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        'inserimento
                                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_INTERVENTI ( " _
                                            & " TIPOLOGIA, IMPORTO_PRESUNTO, ID_UNITA_IMMOBILIARE,  " _
                                            & " ID_UNITA_COMUNE, ID_MANUTENZIONE, ID_IMPIANTO,  " _
                                            & " ID_EDIFICIO, ID_COMPLESSO, ID, ID_sCALA,  " _
                                            & " FL_BLOCCATO)  " _
                                            & " VALUES ('" & TIPOLOGIA & "', " _
                                            & " " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                            & ID_UNITA_IMMOBILIARE & ", " _
                                            & ID_UNITA_COMUNE & ", " _
                                            & ID_MANUTENZIONE & ", " _
                                            & ID_IMPIANTO & ", " _
                                            & ID_EDIFICIO & ", " _
                                            & ID_COMPLESSO & ", " _
                                            & "SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL, " _
                                            & ID_SCALA & ", " _
                                            & "0)"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                Else
                                    RadWindowManager1.RadAlert("Inserire correttamente gli importi degli interventi!", 300, 150, "Attenzione", "", "null")
                                    Exit Sub
                                End If
                            Else
                                RadWindowManager1.RadAlert("Inserire correttamente il dettaglio oggetto!", 300, 150, "Attenzione", "", "null")
                                Exit Sub
                            End If
                        Next
                        '*** SOMMA IMPORTO
                        Dim SommaTot As Decimal = 0
                        For Each elemento As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                            If IsNumeric(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) > 0 Then
                                SommaTot += CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
                            End If
                        Next
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImporto"), TextBox).Text = IsNumFormat(SommaTot, "", "##,##0.00")
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControllo"), HiddenField).Value = IsNumFormat(SommaTot, "", "##,##0.00")
                        CalcolaImporti(SommaTot, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
                        '**********************
                        CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
                        AbilitaDisabilita()
                    Case "1", "2"
                        'emesso
                        Dim TIPOLOGIA As String = ""
                        Dim IMPORTO_PRESUNTO As String = ""
                        Dim ID_UNITA_IMMOBILIARE As String = "NULL"
                        Dim ID_UNITA_COMUNE As String = "NULL"
                        Dim ID_MANUTENZIONE As String = ""
                        Dim ID_IMPIANTO As String = "NULL"
                        Dim ID_EDIFICIO As String = "NULL"
                        Dim ID_COMPLESSO As String = "NULL"
                        Dim ID_SCALA As String = "NULL"
                        Dim ID As String = ""
                        Dim FL_BLOCCATO As String = "0"
                        Dim PREZZO_UNITARIO As Decimal = 0
                        Dim RIMBORSO As Decimal = 0
                        Dim IMPONIBILE_RIMBORSO As Decimal = 0
                        Dim PERC_IVA_RIMBORSO As Decimal = 0
                        Dim controlloPrezzi As Integer = 0
                        For Each elementO As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                            ID_UNITA_IMMOBILIARE = "NULL"
                            ID_UNITA_COMUNE = "NULL"
                            ID_IMPIANTO = "NULL"
                            ID_EDIFICIO = "NULL"
                            ID_COMPLESSO = "NULL"
                            ID_SCALA = "NULL"
                            TIPOLOGIA = CType(elementO.FindControl("DropDownListTipologiaOggetto"), DropDownList).SelectedValue.ToString
                            IMPORTO_PRESUNTO = CDec(CType(elementO.FindControl("TextBoxImportoPresunto"), TextBox).Text.ToString)
                            If IsNumeric(CType(elementO.FindControl("TextBoxImportoConsuntivo"), TextBox).Text.ToString) AndAlso CDec(CType(elementO.FindControl("TextBoxImportoConsuntivo"), TextBox).Text.ToString) > 0 Then
                                PREZZO_UNITARIO = CDec(CType(elementO.FindControl("TextBoxImportoConsuntivo"), TextBox).Text.ToString)
                            Else
                                PREZZO_UNITARIO = 0
                            End If
                            If IsNumeric(CType(elementO.FindControl("TextBoxImportoRimborso"), TextBox).Text.ToString) AndAlso CDec(CType(elementO.FindControl("TextBoxImportoRimborso"), TextBox).Text.ToString) > 0 Then
                                IMPONIBILE_RIMBORSO = CDec(par.IfEmpty(CType(elementO.FindControl("TextBoxImportoRimborso"), TextBox).Text.ToString, "0"))
                                PERC_IVA_RIMBORSO = CDec(par.IfEmpty(CType(elementO.FindControl("TextBoxIvaRimborso"), DropDownList).SelectedValue.ToString, "0"))
                                RIMBORSO = IMPONIBILE_RIMBORSO + (IMPONIBILE_RIMBORSO * PERC_IVA_RIMBORSO / 100)
                            Else
                                RIMBORSO = 0
                            End If
                            If PREZZO_UNITARIO >= 0 Or cmbStato.SelectedValue = "1" Then
                                Select Case TIPOLOGIA
                                    Case "COMPLESSO"
                                        ID_COMPLESSO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "SCALA"
                                        ID_SCALA = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "EDIFICIO"
                                        ID_EDIFICIO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "UNITA IMMOBILIARE"
                                        ID_UNITA_IMMOBILIARE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "UNITA COMUNE"
                                        ID_UNITA_COMUNE = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "CENTRALE TERMICA"
                                        ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "SOLLEVAMENTO"
                                        ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                    Case "TELERISCALDAMENTO"
                                        ID_IMPIANTO = CType(elementO.FindControl("DropDownListTipologiaOggettoDettaglio"), DropDownList).SelectedValue.ToString
                                End Select
                                ID_MANUTENZIONE = vIdManutenzione
                                'RIMBORSO OPERE SPECIALISTICHE
                                ID = par.IfNull(elementO.Cells(par.IndRDGC(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid), "ID")).Text.Replace("&nbsp;", ""), "0")
                                FL_BLOCCATO = "0"
                                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE ID_MANUTENZIONI_INTERVENTI=" & ID & " AND COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'"
                                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                If lettore.HasRows Then
                                    'update
                                    If PREZZO_UNITARIO < 0 Then
                                        par.cmd.CommandText = " DELETE FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & " WHERE  ID_MANUTENZIONI_INTERVENTI=" & ID _
                                            & " AND COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'"
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = " UPDATE SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & " SET TIPOLOGIA        = '" & TIPOLOGIA & "', " _
                                            & " IMPORTO_PRESUNTO     = " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                            & " ID_UNITA_IMMOBILIARE = " & ID_UNITA_IMMOBILIARE & ", " _
                                            & " ID_UNITA_COMUNE      = " & ID_UNITA_COMUNE & ", " _
                                            & " ID_MANUTENZIONE      = " & ID_MANUTENZIONE & ", " _
                                            & " ID_IMPIANTO          = " & ID_IMPIANTO & ", " _
                                            & " ID_EDIFICIO          = " & ID_EDIFICIO & ", " _
                                            & " ID_COMPLESSO         = " & ID_COMPLESSO & ", " _
                                            & " ID_SCALA             = " & ID_SCALA & ", " _
                                            & " FL_BLOCCATO          = " & FL_BLOCCATO _
                                            & " WHERE  ID            = " & ID
                                        par.cmd.ExecuteNonQuery()
                                        par.cmd.CommandText = "SELECT PREZZO_TOTALE FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                                            & " WHERE  ID_MANUTENZIONI_INTERVENTI=" & ID _
                                                            & " AND COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'"
                                        Dim prezzo As String = par.IfEmpty(par.cmd.ExecuteScalar, "0")
                                        par.cmd.CommandText = " UPDATE SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & " SET    QUANTITA            = 1, " _
                                            & " PREZZO_UNITARIO            = " & par.VirgoleInPunti(PREZZO_UNITARIO) & ", " _
                                            & " PREZZO_TOTALE              = " & par.VirgoleInPunti(PREZZO_UNITARIO) & ", " _
                                            & " ID_UM                      = 4, " _
                                            & " ID_MANUTENZIONI_INTERVENTI = " & ID & ", " _
                                            & " DESCRIZIONE                = '***', " _
                                            & " COD_ARTICOLO               = '***' " _
                                            & " WHERE  ID_MANUTENZIONI_INTERVENTI=" & ID _
                                            & " AND COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'"
                                        par.cmd.ExecuteNonQuery()
                                        If prezzo <> PREZZO_UNITARIO Then
                                            par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), 306, "Modifica importo consuntivato da € " & prezzo & " a € " & PREZZO_UNITARIO)
                                        End If
                                    End If
                                Else
                                    'inserimento
                                    If PREZZO_UNITARIO >= 0 Then
                                        If ID < 0 Then
                                            par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL FROM DUAL"
                                            ID = par.cmd.ExecuteScalar
                                            par.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_INTERVENTI ( " _
                                                & " TIPOLOGIA, IMPORTO_PRESUNTO, ID_UNITA_IMMOBILIARE,  " _
                                                & " ID_UNITA_COMUNE, ID_MANUTENZIONE, ID_IMPIANTO,  " _
                                                & " ID_EDIFICIO, ID_COMPLESSO, ID, ID_SCALA,  " _
                                                & " FL_BLOCCATO)  " _
                                                & " VALUES ('" & TIPOLOGIA & "', " _
                                                & " " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                                & ID_UNITA_IMMOBILIARE & ", " _
                                                & ID_UNITA_COMUNE & ", " _
                                                & ID_MANUTENZIONE & ", " _
                                                & ID_IMPIANTO & ", " _
                                                & ID_EDIFICIO & ", " _
                                                & ID_COMPLESSO & ", " _
                                                & ID & ", " _
                                                & ID_SCALA & ", " _
                                                & "0)"
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_CONSUNTIVI ( " _
                                            & " QUANTITA, PREZZO_UNITARIO, PREZZO_TOTALE,  " _
                                            & " ID_UM, ID_MANUTENZIONI_INTERVENTI, ID,  " _
                                            & " DESCRIZIONE, COD_ARTICOLO)  " _
                                            & " VALUES (1, " _
                                            & par.VirgoleInPunti(PREZZO_UNITARIO) & " , " _
                                            & par.VirgoleInPunti(PREZZO_UNITARIO) & " , " _
                                            & "4, " _
                                            & ID & ", " _
                                            & "SISCOM_MI.SEQ_MANUTENZIONI_CONSUNTIVI.NEXTVAL, " _
                                            & "'***', " _
                                            & "'***') "
                                        par.cmd.ExecuteNonQuery()
                                        par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), 305, "Inserimento importo consuntivato di € " & PREZZO_UNITARIO)
                                    End If
                                End If
                                lettore.Close()
                                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI WHERE ID_MANUTENZIONI_INTERVENTI=" & ID & " AND COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'"
                                lettore = par.cmd.ExecuteReader
                                If lettore.HasRows Then
                                    'update
                                    If RIMBORSO = 0 Then
                                        par.cmd.CommandText = " DELETE FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & " WHERE  ID_MANUTENZIONI_INTERVENTI=" & ID _
                                            & " AND COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'"
                                        par.cmd.ExecuteNonQuery()
                                    Else
                                        par.cmd.CommandText = " UPDATE SISCOM_MI.MANUTENZIONI_INTERVENTI " _
                                            & " SET TIPOLOGIA        = '" & TIPOLOGIA & "', " _
                                            & " IMPORTO_PRESUNTO     = " & par.VirgoleInPunti(IMPORTO_PRESUNTO) & ", " _
                                            & " ID_UNITA_IMMOBILIARE = " & ID_UNITA_IMMOBILIARE & ", " _
                                            & " ID_UNITA_COMUNE      = " & ID_UNITA_COMUNE & ", " _
                                            & " ID_MANUTENZIONE      = " & ID_MANUTENZIONE & ", " _
                                            & " ID_IMPIANTO          = " & ID_IMPIANTO & ", " _
                                            & " ID_EDIFICIO          = " & ID_EDIFICIO & ", " _
                                            & " ID_COMPLESSO         = " & ID_COMPLESSO & ", " _
                                            & " ID_SCALA             = " & ID_SCALA & ", " _
                                            & " FL_BLOCCATO          = " & FL_BLOCCATO _
                                            & " WHERE  ID            = " & ID
                                        par.cmd.ExecuteNonQuery()
                                        par.cmd.CommandText = "SELECT PREZZO_TOTALE FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                                            & " WHERE  ID_MANUTENZIONI_INTERVENTI=" & ID _
                                                            & " AND COD_ARTICOLO = 'RIMBORSO OPERE SPECIALISTICHE'"
                                        Dim prezzo As String = par.IfEmpty(par.cmd.ExecuteScalar, "0")
                                        par.cmd.CommandText = " UPDATE SISCOM_MI.MANUTENZIONI_CONSUNTIVI " _
                                            & " SET    QUANTITA            = 1, " _
                                            & " PREZZO_UNITARIO            = " & par.VirgoleInPunti(RIMBORSO) & ", " _
                                            & " PREZZO_TOTALE              = " & par.VirgoleInPunti(RIMBORSO) & ", " _
                                            & " ID_UM                      = 4, " _
                                            & " ID_MANUTENZIONI_INTERVENTI = " & ID & ", " _
                                            & " DESCRIZIONE                = '***', " _
                                            & " COD_ARTICOLO               = 'RIMBORSO OPERE SPECIALISTICHE', " _
                                            & " IMPONIBILE_RIMBORSO        = " & par.VirgoleInPunti(IMPONIBILE_RIMBORSO) & ", " _
                                            & " PERC_IVA_RIMBORSO          = " & par.VirgoleInPunti(PERC_IVA_RIMBORSO) _
                                            & " WHERE  ID_MANUTENZIONI_INTERVENTI=" & ID _
                                            & " AND COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'"
                                        par.cmd.ExecuteNonQuery()
                                        If prezzo <> RIMBORSO Then
                                            par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), 308, "Modifica rimborso da € " & prezzo & " a € " & RIMBORSO)
                                        End If
                                    End If
                                Else
                                    'inserimento
                                    If RIMBORSO > 0 Then
                                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.MANUTENZIONI_CONSUNTIVI ( " _
                                            & " QUANTITA, PREZZO_UNITARIO, PREZZO_TOTALE,  " _
                                            & " ID_UM, ID_MANUTENZIONI_INTERVENTI, ID,  " _
                                            & " DESCRIZIONE, COD_ARTICOLO, PERC_IVA_RIMBORSO, IMPONIBILE_RIMBORSO)  " _
                                            & " VALUES (1, " _
                                            & par.VirgoleInPunti(RIMBORSO) & " , " _
                                            & par.VirgoleInPunti(RIMBORSO) & " , " _
                                            & "4, " _
                                            & ID & ", " _
                                            & "SISCOM_MI.SEQ_MANUTENZIONI_CONSUNTIVI.NEXTVAL, " _
                                            & "'***', " _
                                            & "'RIMBORSO OPERE SPECIALISTICHE'," & par.VirgoleInPunti(PERC_IVA_RIMBORSO) & "," & par.VirgoleInPunti(IMPONIBILE_RIMBORSO) & ") "
                                        par.cmd.ExecuteNonQuery()
                                        par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), 307, "Inserimento rimborso di € " & RIMBORSO)
                                    End If
                                End If
                                lettore.Close()
                                If PREZZO_UNITARIO > 0 Then
                                    controlloPrezzi = controlloPrezzi + 1
                                End If
                            Else
                                RadWindowManager1.RadAlert("Inserire correttamente gli importi consuntivati!", 300, 150, "Attenzione", "", "null")
                                Exit Sub
                            End If
                        Next
                        par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI" _
                            & " SET RIMBORSI =" _
                            & " NVL((SELECT SUM (NVL (MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE, 0))" _
                            & " FROM SISCOM_MI.MANUTENZIONI_CONSUNTIVI" _
                            & " WHERE MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO =" _
                            & " 'RIMBORSO OPERE SPECIALISTICHE'" _
                            & " AND ID_MANUTENZIONI_INTERVENTI in (select id from siscom_mi.manutenzioni_interventi where id_manutenzione = " & vIdManutenzione & ")),0)" _
                            & " WHERE ID IN (SELECT ID_MANUTENZIONE" _
                            & " FROM SISCOM_MI.MANUTENZIONI_INTERVENTI" _
                            & " WHERE ID = " & ID & ")"
                        par.cmd.ExecuteNonQuery()

                        If controlloPrezzi = 0 And cmbStato.SelectedValue = 2 Then
                            RadWindowManager1.RadAlert("Inserire correttamente gli importi consuntivati!", 300, 150, "Attenzione", "", "null")
                            Exit Sub
                        ElseIf controlloPrezzi > 0 Then
                            cmbStato.SelectedValue = 2
                            txtSTATO.Value = 2
                        End If

                        Dim rimb As Decimal = 0
                        If IsNumeric(vIdManutenzione) AndAlso vIdManutenzione > 0 Then
                            par.cmd.CommandText = "SELECT NVL(RIMBORSI,0) FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & vIdManutenzione
                            rimb = par.cmd.ExecuteScalar
                        End If

                        '*** SOMMA IMPORTO

                        Dim SommaTotC As Decimal = 0
                        Dim SommaTotRimborsi As Decimal = 0
                        For Each elemento As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
                            If IsNumeric(CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) > 0 Then
                                SommaTotC += CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
                                If CDec(CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text) <= 0 Then
                                    RadWindowManager1.RadAlert("Inserire correttamente gli importi consuntivati!", 300, 150, "Attenzione", "", "null")
                                    Exit Sub
                                End If
                            End If
                        Next
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoC"), TextBox).Text = IsNumFormat(SommaTotC, "", "##,##0.00")
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtImportoControlloC"), HiddenField).Value = IsNumFormat(SommaTotC + SommaTotRimborsi, "", "##,##0.00")
                        'CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(SommaTotRimborsi, "", "##,##0.00")
                        CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRimborsi"), TextBox).Text = IsNumFormat(rimb, "", "##,##0.00")
                        CalcolaImportiC(SommaTotC, par.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtRimborsi"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
                        CType(Me.Page.FindControl("txtmodificato"), TextBox).Text = "1"
                        AbilitaDisabilita()
                End Select
            Else
                'IMPORTO INSERITO SUPERIORE ALL'IMPORTO CONTRATTUALE
                'Response.Write("<script>alert('L\'importo inserito è superiore all\'importo contrattuale residuo!');</script>")
                Exit Sub
            End If
            '*************************************************************************************************



            ' MANUTENZIONI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update SISCOM_MI.MANUTENZIONI " _
                                & " set ID_COMPLESSO=:id_complesso, ID_EDIFICIO=:id_edificio,ID_SCALA=:id_scala,ID_PF_VOCE=:id_voce,ID_APPALTO=:id_appalto," _
                                & "     DESCRIZIONE=:descrizione,DATA_INIZIO_ORDINE=:data_inizio,DATA_INIZIO_INTERVENTO=:data_inizio_inter,DATA_FINE_INTERVENTO=:data_fine_inter,STATO=:stato," _
                                & "     IMPORTO_CONSUNTIVATO=:imp_consuntivo,IMPORTO_PRESUNTO=:imp_presunto,RIT_LEGGE=:rit_legge,RIMBORSI=:rimborsi,IMPORTO_TOT=:importo_tot,IMPORTO_RESIDUO=:importo_residuo,IVA_CONSUMO_P=:iva_consumoP,IVA_CONSUMO=:iva_consumoC,DANNEGGIANTE=:danneggiante,DANNEGGIATO=:danneggiato," _
        & " IMPORTO_ONERI_PREV=:imp_oneri_presunto,IMPORTO_ONERI_CONS=:imp_oneri_consuntivo,FL_AUTORIZZAZIONE=:FL_AUTORIZZAZIONE,OPERATORE_AUTORIZZAZIONE=:OPERATORE_AUTORIZZAZIONE " _
  & "     ,ID_FORNITORE_STAMPA=:ID_FORNITORE_STAMPA " _
                                & " where ID=:id_manutenzione"

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(txtIdComplesso.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(txtIdEdificio.Text)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbScala.SelectedValue, -1)))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", Convert.ToInt32(Me.cmbVocePF.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text, 4000)))

            If Strings.Len(lblData.Text) = 0 Then
                lblData.Text = DateTime.Now.Date
            End If
            Dim dataInizio As String = ""
            If Not IsNothing(txtDataInizio.SelectedDate) Then
                dataInizio = txtDataInizio.SelectedDate
            End If
            Dim dataFine As String = ""
            If Not IsNothing(txtDataFine.SelectedDate) Then
                dataFine = txtDataFine.SelectedDate
            End If
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_inizio", par.AggiustaData(Me.lblData.Text)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_inizio_inter", par.AggiustaData(dataInizio)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_fine_inter", par.AggiustaData(dataFine)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_consuntivo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_presunto", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text.Replace(".", ""))))

            'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rimborsi", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text.Replace(".", ""))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_tot", strToNumber(importo_tot)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_residuo", strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manutenzione", vIdManutenzione))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumoP", Convert.ToInt32(Me.txtPercIVA_P.Value)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumoC", Convert.ToInt32(Me.txtPercIVA_C.Value)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("danneggiante", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDanneggiante"), TextBox).Text, 500)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("danneggiato", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDanneggiato"), TextBox).Text, 500)))

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_oneri_presunto", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtOneriP_MANO"), HiddenField).Value.Replace(".", ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_oneri_consuntivo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtOneriC_MANO"), HiddenField).Value.Replace(".", ""))))
            Dim idFS As Integer = 0
            If IsNumeric(cmbIntestazione.SelectedValue) AndAlso CInt(cmbIntestazione.SelectedValue) > 0 Then
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_FORNITORE_STAMPA", Convert.ToInt32(cmbIntestazione.SelectedValue)))
            Else
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_FORNITORE_STAMPA", DBNull.Value))
            End If

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("fl_autorizzazione", flautorizzazione))
            If operatoreIniziale.Value = 0 Then
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_autorizzazione", DBNull.Value))
            Else
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("operatore_autorizzazione", operatoreIniziale.Value))
            End If
            par.cmd.ExecuteNonQuery()
            autorizzazione.Value = flautorizzazione
            autorizzazioneIniziale = autorizzazione.Value
            If cmbStato.SelectedValue >= 1 Then
                ChkAutorizzazioneEmissione.Enabled = False
            End If
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            '************************************

            If inserimentoEventoAutorizzazione = True Then
                par.cmd.CommandText = "Insert into SISCOM_MI.EVENTI_MANUTENZIONE " _
                        & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                        & " Values " _
                        & " (" & vIdManutenzione & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F247', 'Autorizzazione ODL') "
                par.cmd.ExecuteNonQuery()
            End If
            If inserimentoEventoAutorizzazioneTolta = True Then
                par.cmd.CommandText = "Insert into SISCOM_MI.EVENTI_MANUTENZIONE " _
                        & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                        & " Values " _
                        & " (" & vIdManutenzione & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F247', 'Revoca autorizzazione ODL') "
                par.cmd.ExecuteNonQuery()
            End If


            'PRENOTAZIONI
            GestionePrenotazioni() 'GestionePagamento()

            'PENALE
            GestionePENALE()

            'STATO
            If Me.cmbStato.SelectedValue <> stato Then
                Select Case Me.cmbStato.SelectedValue
                    Case 1  'EMESSO ORDINE

                        'APPALTI_VOCI_PF
                        Setta_ImportoResiduo()

                        'Dim Val As Decimal
                        'Val = Decimal.Parse(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).Text, "0")) - Decimal.Parse(par.IfEmpty(Me.txtNettoOneriIVA_TMP.Value, "0"))

                        'par.cmd.CommandText = "update SISCOM_MI.APPALTI_LOTTI_SERVIZI set RESIDUO_CONSUMO=" & par.VirgoleInPunti(Val) _
                        '                   & " where ID_SERVIZIO=" & Me.cmbServizio.SelectedValue _
                        '                   & " and   ID_PF_VOCE_IMPORTO=" & Me.cmbServizioVoce.SelectedValue _
                        '                  & "  and   ID_APPALTO=" & txtIdAppalto.Value

                        'par.cmd.ExecuteNonQuery()
                        'par.cmd.CommandText = ""

                        'CType(Tab_Manu_Riepilogo.FindControl("txtImportoTotale"), TextBox).Text = IsNumFormat(par.IfNull(Val, 0), "", "##,##0.00")


                        '*** EVENTI_MANUTENZIONE
                        par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE, "Da Bozza ad Emesso Ordine")


                        ' IN CASO DI PROVENIENZA DA SEGNALAZIONE
                        'sValoreProvenienza = Request.QueryString("PROVENIENZA")

                        'If sValoreProvenienza = "SEGNALAZIONI" Then
                        '    par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=4  where ID=" & txtID_Segnalazioni.Value
                        '    par.cmd.ExecuteNonQuery()
                        '    par.cmd.CommandText = ""

                        '    Dim sLink As String

                        '    If Me.txtID_Prenotazione.Value > 0 Then
                        '        sLink = "<a href='../CICLO_PASSIVO/CICLOPASSIVO/MANUTENZIONI/StampaOrdine.aspx?COD=" & vIdManutenzione & "' target='_blank'>Emesso Ordine di Lavoro. Clicca per Visualizzare</a>"
                        '    Else
                        '        sLink = "Creato l'ordine di lavoro num:" & Me.lblODL1.Text & " in data: " & Me.lblData.Text
                        '    End If

                        '    par.cmd.CommandText = "insert into SISCOM_MI.SEGNALAZIONI_NOTE " _
                        '                & " (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) values " _
                        '                & " (" & Me.txtID_Segnalazioni.Value & ",'" & par.PulisciStrSql(sLink) & "','" _
                        '                & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                        '    par.cmd.ExecuteNonQuery()
                        '    par.cmd.CommandText = ""
                        'End If
                        '*******************************

                        Me.lblDataInizio.Text = "Data Presunta Inizio Lavori:"
                        Me.lblDataFine.Text = "Data Presunta Fine Lavori"

                        CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = False

                    Case 2  'CONSUNTIVO (FINE)

                        'APPALTI_VOCI_PF
                        Setta_ImportoResiduo()

                        'DATA CONSUNTIVO
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set " _
                                                & " IMPORTO_RESIDUO=" & par.VirgoleInPunti(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", "")) _
                                                & " ,DATA_FINE_ORDINE=" & Format(Now, "yyyyMMdd") _
                                           & " where ID=" & vIdManutenzione

                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        '*** EVENTI_MANUTENZIONE
                        par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE, "Da Emesso Ordine a Consuntivato")


                        ' IN CASO DI PREVENIENZA DA SEGNALAZIONI
                        'sValoreProvenienza = Request.QueryString("PROVENIENZA")

                        'If sValoreProvenienza = "SEGNALAZIONI" Then
                        '    par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=10  where ID=" & txtID_Segnalazioni.Value

                        '    par.cmd.ExecuteNonQuery()
                        '    par.cmd.CommandText = ""
                        'End If
                        '*******************************

                        Me.lblDataInizio.Text = "Data Effettiva Inizio Lavori:"
                        Me.lblDataFine.Text = "Data Effettiva Fine Lavori"

                End Select
            End If


            If Setta_ImportoResiduo() = False Then
                par.myTrans.Rollback()
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI  where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
                Dim myReade As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReade.Close()

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!\nLa manutenzione verrà ripristinata allo stato precedente!", 300, 150, "Attenzione", "", "null")
                'Response.Write("<script>alert('L\'importo inserito è superiore all\'importo contrattuale residuo!\nLa manutenzione verrà ripristinata allo stato precedente!');</script>")
                Exit Sub

            Else



                RadNotificationNote.Text = "Operazione completata correttamente!"
                RadNotificationNote.AutoCloseDelay = "600"
                RadNotificationNote.Show()

            End If


            'Modifica di correzione errore importo_residuo e importo_tot delle manutenzioni 21/11/2017
            If Me.cmbStato.SelectedValue = 2 Then
                Dim importoTotManutenzioni As Decimal = CDec(par.IfNull(strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVAC"), TextBox).Text.Replace(".", "")), 0)) + CDec(par.IfNull(strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", "")), 0))
                Dim sommaresiduo As Decimal = Round(CDec(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value), 2)
                If importoTotManutenzioni > 0 Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.MANUTENZIONI " _
                        & " SET IMPORTO_TOT=" & par.VirgoleInPunti(CDec(Round(importoTotManutenzioni, 2))) _
                        & " ,IMPORTO_RESIDUO=" & par.VirgoleInPunti(CDec(Round(sommaresiduo, 2))) _
                        & " WHERE ID=" & vIdManutenzione
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            par.cmd.CommandText = "SELECT RAGIONE_SOCIALE,(SELECT EMAIL FROM SISCOM_MI.FORNITORI_INDIRIZZI WHERE ID_FORNITORE=FORNITORI.ID AND ID =(SELECT MAX(ID) FROM SISCOM_MI.FORNITORI_INDIRIZZI WHERE ID_FORNITORE=FORNITORI.ID)) AS MAIL FROM SISCOM_MI.FORNITORI WHERE FORNITORI.ID=(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID =(SELECT ID_APPALTO FROM SISCOM_MI.MANUTENZIONI WHERE ID=" & vIdManutenzione & "))"
            Dim lettoreF As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim Impresa As String = ""
            If lettoreF.Read Then
                indirizzoEmail.Value = par.IfNull(lettoreF("MAIL"), "")
                Impresa = par.IfNull(lettoreF("RAGIONE_SOCIALE"), "")
            End If
            lettoreF.Close()
            par.cmd.CommandText = "SELECT num_repertorio FROM siscom_mi.APPALTI WHERE ID =(select ID_APPALTO from siscom_mi.manutenzioni where manutenzioni.id=" & vIdManutenzione & ")"
            lettoreF = par.cmd.ExecuteReader
            Dim numeroRepertorio As String = ""
            If lettoreF.Read Then
                numeroRepertorio = par.IfNull(lettoreF("NUM_REPERTORIO"), "-")
            End If
            lettoreF.Close()
            par.cmd.CommandText = "SELECT * FROM siscom_mi.manutenzioni WHERE id=" & vIdManutenzione
            lettoreF = par.cmd.ExecuteReader
            If lettoreF.Read Then
                oggettoEmail.Value = "ORDINE DI LAVORO N. " & par.IfNull(lettoreF("PROGR"), "-") & "/" & par.IfNull(lettoreF("ANNO"), "-") & " CON RIFERIEMENTO AL CONTRATTO N. " & numeroRepertorio & " - " & Impresa
                bodyEmail.Value = "Con la presente si trasmette regolare ordine di lavoro. Attendiamo riscontro in merito alla programmazione e conclusione dei lavori. Cordiali saluti"
            End If
            lettoreF.Close()

            par.myTrans.Commit() 'COMMIT
            par.cmd.CommandText = ""

            txtSTATO.Value = Me.cmbStato.SelectedValue

            'ABILITO/DISABILITO CAMPI in base allo stato
            AbilitaDisabilita()

            CalcolaImporti(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
            CalcolaImportiC(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI  where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


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

            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub
    Function ControlloCampiConsuntivo() As Boolean

        ControlloCampiConsuntivo = True
        'Controllo che la somma degli importi non superi quello RESIDUO TOTALE (APPALTI_LOTTI_SERVIZI.RESIDUO_CONSUMO)
        Dim SommaTot As Decimal = 0
        Dim SommaTotRimborsi As Decimal = 0
        Dim SommaTot1 As Decimal = 0
        'SommaTot = TotalePrezzoConsuntivo()
        For Each elemento As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
            If IsNumeric(CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text) > 0 Then
                SommaTot += CType(elemento.FindControl("TextBoxImportoConsuntivo"), TextBox).Text
            End If
            If IsNumeric(CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text) > 0 Then
                SommaTotRimborsi += CType(elemento.FindControl("TextBoxImportoRimborso"), TextBox).Text
            End If
        Next
        'If Me.txtCodArticolo.Text = "RIMBORSO OPERE SPECIALISTICHE" Then
        '    SommaTotRimborsi = SommaTotRimborsi - CDbl(par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("txtImportoODL"), HiddenField).Value, 0)) + Decimal.Parse(par.IfEmpty(Me.txtTotale.Text.Replace(".", ""), "0"))
        'Else
        '    SommaTot = SommaTot - Decimal.Parse(par.IfEmpty(Me.txtImportoODL.Value.Replace(".", ""), "0")) + Decimal.Parse(par.IfEmpty(Me.txtTotale2.Value.Replace(".", ""), "0"))
        'End If
        SommaTot1 = CalcolaImportiControllo1(SommaTot, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Tab_Manu_Riepilogo.FindControl("txtOneriC_MANO"), HiddenField).Value, 0))
        'In caso di rimborsi, non viene applicato lo sconto ecc
        SommaTot1 = SommaTot1 + SommaTotRimborsi
        If Math.Round(SommaTot1, 2) > Math.Round(Ricalcola_ImportoResiduo(), 2) Then
            'If Math.Round(SommaTot1, 5) > Math.Round((Decimal.Parse(par.IfEmpty(Me.txtResiduoConsumo.Value, "0")) + TotalePrezzoPreventivato()), 5) Then
            RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
            ControlloCampiConsuntivo = False
            Exit Function
        End If
    End Function

    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        If txtModificato.Text <> "111" Then

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub
    Protected Sub btnINDIETRO_Click(sender As Object, e As System.EventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then
            Session.Add("LAVORAZIONE", "0")
            'RICERCA MANUTENZIONI RRS
            sValoreStruttura = Request.QueryString("STR")

            sValoreEsercizioF = Request.QueryString("ES")
            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreVoce = Request.QueryString("SV")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreStato = Request.QueryString("ST")

            'RICERCA DIRETTA
            sValoreRepertorio = Request.QueryString("REP")
            sValoreODL = Request.QueryString("ODL") 'Request.QueryString("PROGR")
            sValoreAnno = Request.QueryString("ANNO")
            '************

            'Da Ricerca PRE-INSERIMENTO RicercaRRS_INS.aspx
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
            sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))
            sOrdinamento = Request.QueryString("ORD")
            sValoreProvenienza = Request.QueryString("PROVENIENZA")

            'RICERCA SFITTI
            sValoreUnita = Request.QueryString("UI")


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                par.myTrans.Rollback()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            End If

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")

            Select Case sValoreProvenienza


                Case "RICERCA_CONSUNTIVI_RRS_S"
                    Response.Write("<script>location.replace('RisultatiConsuntiviRRS_S.aspx?FO=" & sValoreFornitore _
                                                                                       & "&AP=" & sValoreAppalto _
                                                                                      & "&DAL=" & sValoreData_Dal _
                                                                                       & "&AL=" & sValoreData_Al _
                                                                                       & "&ES=" & sValoreEsercizioF _
                                                                                     & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                      & "&ORD=" & sOrdinamento & "');</script>")

                Case "RICERCA_CONSUNTIVI_RRS_D"
                    Response.Write("<script>location.replace('RisultatiConsuntiviRRS_D.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                                    & "&ODL=" & sValoreODL _
                                                                                    & "&ANNO=" & sValoreAnno _
                                                                                    & "&ORD=" & sOrdinamento _
                                                                                     & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                    & "');</script>")


                Case "RICERCA_RRS_DIRETTA"
                    Response.Write("<script>location.replace('RisultatiRRS_D.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                              & "&ODL=" & sValoreODL _
                                                                              & "&ANNO=" & sValoreAnno _
                                                                              & "&ORD=" & sOrdinamento _
                                                                             & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                              & "&ST=" & sValoreStato & "');</script>")

                Case "RICERCA_RRS"
                    Response.Write("<script>location.replace('RisultatiRRS.aspx?CO=" & sValoreComplesso _
                                                                             & "&ED=" & sValoreEdificio _
                                                                             & "&SV=" & sValoreVoce _
                                                                             & "&FO=" & sValoreFornitore _
                                                                             & "&AP=" & sValoreAppalto _
                                                                             & "&DAL=" & sValoreData_Dal _
                                                                             & "&AL=" & sValoreData_Al _
                                                                             & "&UI=" & sValoreUnita _
                                                                             & "&ST=" & sValoreStato _
                                                                             & "&STR=" & sValoreStruttura _
                                                                             & "&ORD=" & sOrdinamento _
                                                                             & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                             & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")
                Case "RISULTATI_RRS_INS"

                    Response.Write("<script>location.replace('RisultatiRRS_INS.aspx?IN_R=" & par.VaroleDaPassare(sValoreIndirizzoR) _
                                                                                & "&SV_R=" & sValoreVoceR _
                                                                                & "&AP_R=" & sValoreAppaltoR _
                                                                                & "&ORD=" & sOrdinamento _
                                                                                & "&UBI=" & sValoreUbicazione _
                                                                                & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                & "&PROVENIENZA=RISULTATI_RRS_INS" & "');</script>")

                Case "RICERCARRS_INS"

                    Response.Write("<script>location.replace('RicercaRRS_INS.aspx');</script>")



                Case Else
                    If txtindietro.Text = 1 Then
                        Response.Write("<script>document.location.href=""../../pagina_home.aspx""</script>")
                    End If
            End Select
        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub
    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        Dim sNote As String

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtElimina.Text = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                'Se è integrativo, allora il padre passa da INTEGRATO=3 a EMESSO=1
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select ID from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & vIdManutenzione
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=1,ID_FIGLIO=Null where ID=" & myReader1(0)

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                End If
                myReader1.Close()
                '****************************

                'If Me.txtID_Segnalazioni.Value > 0 Then
                '    par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=0 where ID=" & Me.txtID_Segnalazioni.Value

                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""


                '    par.cmd.CommandText = "insert into SISCOM_MI.SEGNALAZIONI_NOTE " _
                '                & " (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) values " _
                '                & " (" & Me.txtID_Segnalazioni.Value & ",'ORDINE num: " & Me.lblODL1.Text & " in data: " & Me.lblData.Text & " ELIMINATO','" _
                '                & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""

                'End If

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.MANUTENZIONI where ID=" & vIdManutenzione
                par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=6 where ID=" & vIdManutenzione
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set  ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                'LOG CANCELLAZIONE
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE MANUTENZIONE NON PATRIMONIALE"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "MANUTENZIONE NON PATRIMONIALE"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.lblODL.Text, "") & par.IfEmpty(Me.lblODL1.Text, "")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text, ""), 200)))

                sNote = "Cancellazione Manutenzione non patrimoniale del complesso\edificio: " & Me.cmbIndirizzo.SelectedItem.Text
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", sNote))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************


                ' COMMIT
                par.myTrans.Commit()


                RadNotificationNote.Text = "Eliminazione completata correttamente!"
                RadNotificationNote.Show()
                Session.Add("LAVORAZIONE", "0")


                'RICERCA MANUTENZIONI RRS
                sValoreStruttura = Request.QueryString("STR")
                sValoreComplesso = Request.QueryString("CO")
                sValoreEdificio = Request.QueryString("ED")
                sValoreVoce = Request.QueryString("SV")

                sValoreFornitore = Request.QueryString("FO")
                sValoreAppalto = Request.QueryString("AP")
                sValoreData_Dal = Request.QueryString("DAL")
                sValoreData_Al = Request.QueryString("AL")

                sValoreStato = Request.QueryString("ST")

                'RICERCA DIRETTA
                sValoreRepertorio = Request.QueryString("REP")
                sValoreODL = Request.QueryString("ODL") 'Request.QueryString("PROGR")
                sValoreAnno = Request.QueryString("ANNO")
                '************

                'Da Ricerca PRE-INSERIMENTO RicercaRRS_INS.aspx
                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
                sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
                sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

                sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))
                sOrdinamento = Request.QueryString("ORD")
                sValoreProvenienza = Request.QueryString("PROVENIENZA")
                '***********************************************

                'RICERCA SFITTI
                sValoreUnita = Request.QueryString("UI")


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                End If

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)

                Session.Item("LAVORAZIONE") = "0"

                Page.Dispose()

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../../../NuoveImm/Titolo_IMPIANTI.png';</script>")

                Select Case sValoreProvenienza


                    Case "RICERCA_CONSUNTIVI_RRS_S"
                        Response.Write("<script>location.replace('RisultatiConsuntiviRRS_S.aspx?FO=" & sValoreFornitore _
                                                                                       & "&AP=" & sValoreAppalto _
                                                                                      & "&DAL=" & sValoreData_Dal _
                                                                                       & "&AL=" & sValoreData_Al _
                                                                                     & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                      & "&ORD=" & sOrdinamento & "');</script>")

                    Case "RICERCA_CONSUNTIVI_RRS_D"
                        Response.Write("<script>location.replace('RisultatiConsuntiviRRS_D.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                                        & "&ODL=" & sValoreODL _
                                                                                        & "&ANNO=" & sValoreAnno _
                                                                                        & "&ORD=" & sOrdinamento _
                                                                                       & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                        & "');</script>")

                    Case "RICERCA_RRS_DIRETTA"
                        Response.Write("<script>location.replace('RisultatiRRS_D.aspx?REP=" & par.VaroleDaPassare(sValoreRepertorio) _
                                                                                  & "&ODL=" & sValoreODL _
                                                                                  & "&ANNO=" & sValoreAnno _
                                                                                  & "&ORD=" & sOrdinamento _
                                                                                 & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                  & "&ST=" & sValoreStato & "');</script>")

                    Case "RICERCA_RRS"
                        Response.Write("<script>location.replace('RisultatiRRS.aspx?CO=" & sValoreComplesso _
                                                                                 & "&ED=" & sValoreEdificio _
                                                                                 & "&SV=" & sValoreVoce _
                                                                                 & "&FO=" & sValoreFornitore _
                                                                                 & "&AP=" & sValoreAppalto _
                                                                                 & "&DAL=" & sValoreData_Dal _
                                                                                 & "&AL=" & sValoreData_Al _
                                                                                 & "&UI=" & sValoreUnita _
                                                                                 & "&ST=" & sValoreStato _
                                                                                 & "&STR=" & sValoreStruttura _
                                                                                 & "&ORD=" & sOrdinamento _
                                                                                & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                 & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")


                    Case "RISULTATI_RRS_INS"

                        Response.Write("<script>location.replace('RisultatiRRS_INS.aspx?IN_R=" & sValoreIndirizzoR _
                                                                                    & "&SV_R=" & sValoreVoceR _
                                                                                    & "&AP_R=" & sValoreAppaltoR _
                                                                                    & "&ORD=" & sOrdinamento _
                                                                                    & "&UBI=" & sValoreUbicazione _
                                                                                   & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                    & "&PROVENIENZA=RISULTATI_RRS_INS" & "');</script>")

                    Case "RICERCARRS_INS"

                        Response.Write("<script>location.replace('RicercaRRS_INS.aspx');</script>")

                    Case Else
                        If txtindietro.Text = 1 Then
                            Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                        End If
                End Select


            Else
                CType(Me.Page.FindControl("txtElimina"), TextBox).Text = "0"
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


    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)

        Me.btnSalva.Enabled = False
        Me.btnElimina.Enabled = False
        Me.btnAnnulla.Enabled = False
        Me.btnOrdineIntegrativo.Enabled = False


        Dim CTRL As Control = Nothing
        For Each CTRL In Me.form1.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = True
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadComboBox Then
                DirectCast(CTRL, RadComboBox).Enabled = False
            ElseIf TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
        Next

        '*** FORM RIEPILOGO
        For Each CTRL In Me.Tab_Manu_Riepilogo.Controls
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).ReadOnly = True
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            ElseIf TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
        Next

        Me.cmbIndirizzo.Enabled = False
        Me.cmbScala.Enabled = False

        Me.cmbVocePF.Enabled = False
        txtDataInizio.Enabled = False
        txtDataFine.Enabled = False

    End Sub

    Private Function RitornaNullSeIntegerMenoUno(ByVal valorepass As Integer) As Object
        Dim a As Object = DBNull.Value

        If valorepass <> -1 Then
            a = valorepass
        End If

        Return a
    End Function

    Private Function RitornaNullSeMenoUno(ByVal valorepass As String) As String
        Dim a As String = "Null"

        If valorepass = "-1" Then
            a = "Null"
        ElseIf valorepass <> "-1" Then
            a = "'" & valorepass & "'"
        End If

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


    'Protected Sub RBL1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RBL1.SelectedIndexChanged
    '    'CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

    '    'Setta_Ubicazione(RBL1.SelectedIndex, 0, "=")
    '    'SettaggioCampi()

    'End Sub


    Protected Sub cmbScala_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbScala.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        'Setta_Interno()
    End Sub

    Protected Sub cmbScala_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbScala.TextChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub



    Protected Sub cmbVocePF_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbVocePF.SelectedIndexChanged
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Setta_Appalto_Fornitore("=")

    End Sub


    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Sub CalcolaImporti(ByVal importo As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Integer)

        Dim oneri, asta, ritenuta, iva, risultato1, risultato2, risultato3, risultato4, ritenutaIVATA, risultato5 As Decimal

        'importo = importo.replace '.', '')

        'perc_oneri=perc_oneri.replace('.', '');
        'perc_sconto=perc_sconto.replace('.', '');
        'perc_iva=perc_iva.replace('.', '');

        'importo=importo.replace(',', '.');
        'perc_oneri=perc_oneri.replace(',', '.');
        'perc_sconto=perc_sconto.replace(',', '.');
        'perc_iva=perc_iva.replace(',', '.');

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva

        'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = par.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriP_MANO"), HiddenField).Value, 0)

        If oneri = -1 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
            oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = Math.Round((risultato3 * 0.5) / 100, 4) '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Math.Round(ritenuta + ((ritenuta * perc_iva) / 100), 2)
            ritenutaIVATA = Round(ritenutaIVATA, 2)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If


        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri


        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        iva = Math.Round((risultato5 * perc_iva) / 100, 2)

        'I) NETTO+ONERI+IVA
        risultato4 = risultato5 + iva

        CType(Tab_Manu_Riepilogo.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneri, "", "##,##0.00")
        CType(Tab_Manu_Riepilogo.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1, "", "##,##0.00")
        CType(Tab_Manu_Riepilogo.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(asta, "", "##,##0.00")
        CType(Tab_Manu_Riepilogo.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2, "", "##,##0.00")

        CType(Tab_Manu_Riepilogo.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaIVATA, "", "##,##0.00") '6 campo

        CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3, "", "##,##0.00")
        CType(Tab_Manu_Riepilogo.FindControl("txtIVA"), TextBox).Text = IsNumFormat(iva, "", "##,##0.00")
        CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(risultato4, "", "##,##0.00")

        risultato4 = risultato4 + ritenutaIVATA
        'risultato4 = Math.Round(risultato4, 2) + Math.Round(ritenutaIVATA, 2) 'risultato4 + ritenutaIVATA
        Me.txtNettoOneriIVA_TMP.Value = IsNumFormat(risultato4, "", "##,##0.00")


    End Sub


    Sub CalcolaImportiC(ByVal importo As Decimal, ByVal rimborso As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Integer)

        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, ritenutaIVATA, risultato5 As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva

        'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = par.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriC_MANO"), HiddenField).Value, 0)

        If oneri = -1 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
            oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta '- penale


        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Math.Round(ritenuta + ((ritenuta * perc_iva) / 100), 2)
            ritenutaIVATA = Round(ritenutaIVATA, 2)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri


        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato5 * perc_iva) / 100, 2)
        End If

        'I) NETTO+ONERI+IVA
        risultato4 = risultato5 + iva + Round(rimborso, 2) ' penale

        CType(Tab_Manu_Riepilogo.FindControl("txtOneriC"), TextBox).Text = IsNumFormat(oneri, "", "##,##0.00") '2 campo
        CType(Tab_Manu_Riepilogo.FindControl("txtOneriImportoC"), TextBox).Text = IsNumFormat(risultato1, "", "##,##0.00") '3 campo
        CType(Tab_Manu_Riepilogo.FindControl("txtRibassoAstaC"), TextBox).Text = IsNumFormat(asta, "", "##,##0.00") '4 campo
        CType(Tab_Manu_Riepilogo.FindControl("txtNettoC"), TextBox).Text = IsNumFormat(risultato2, "", "##,##0.00") '5 campo

        CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text = IsNumFormat(ritenutaIVATA, "", "##,##0.00") '6 campo

        CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriC"), TextBox).Text = IsNumFormat(risultato3, "", "##,##0.00") '7 campo
        CType(Tab_Manu_Riepilogo.FindControl("txtIVAC"), TextBox).Text = IsNumFormat(iva, "", "##,##0.00") '8 campo
        CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVAC"), TextBox).Text = IsNumFormat(risultato4, "", "##,##0.00") '10 campo
        risultato4 = risultato4 + ritenutaIVATA
        'risultato4 = Math.Round(risultato4, 2) + Math.Round(ritenutaIVATA, 2) 'risultato4 + ritenutaIVATA
        Me.txtNettoOneriIVAC_TMP.Value = IsNumFormat(risultato4, "", "##,##0.00")

    End Sub

    Sub AbilitaDisabilita()
        ' si trova in FORM LOAD (per primo inserimento) + RIEMPI CAMPI
        '          in SALVA + UPDATE
        '          in Crea ORDINE INTEGRATIVO
        'sValoreProvenienza = Request.QueryString("PROVENIENZA")


        Select Case txtSTATO.Value
            Case 0  'BOZZA
                ' Posso inserire MANUTENZIONI_INTERVENTI + Descrizione
                ' Posso eliminare la manutenzione, se Integrativa, allora pulisco il campo MANUTENZIONI.ID_FIGLIO

                If Val(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoControllo"), HiddenField).Value, 0)) > 0 Then
                    RBL1.Enabled = False
                    Me.cmbIndirizzo.Enabled = False
                    Me.cmbScala.Enabled = False
                    Me.cmbVocePF.Enabled = False

                Else
                    ' se ho cancellato tutti gli INTERVENTI inseriti, posso cambiare l'indirizzo o servizio

                    RBL1.Enabled = False
                    Me.cmbIndirizzo.Enabled = False
                    If RBL1.Items(1).Selected = True Then Me.cmbScala.Enabled = True
                    Me.cmbVocePF.Enabled = True


                End If

                If Me.cmbVocePF.SelectedValue <> "-1" Then
                    'CType(Tab_Manu_Dettagli.FindControl("btnAgg1"), ImageButton).Visible = True
                    CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = True
                    'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = True
                Else
                    'CType(Tab_Manu_Dettagli.FindControl("btnAgg1"), ImageButton).Visible = False
                    CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = False
                    'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = False
                End If

                'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = False

                CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = True

                If Me.txtIdManutenzione.Text = "-1" Then
                    'PRIMO INSERIMENTO
                    Me.btnElimina.Visible = False
                    Me.txtVisualizza.Value = 0  'NO ALLEGATI
                Else
                    Me.btnElimina.Visible = True
                    Me.txtVisualizza.Value = 1  'possibilità di inserire ALLEGATO
                End If

                Me.btnAnnulla.Enabled = False
                Me.btnOrdineIntegrativo.Visible = False
                CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = False

                If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then FrmSolaLettura()

            Case 1  'EMESSO ORDINE
                ' Disabilito l'inserimento di MANUTENZIONI_INTERVENTI
                ' abilito il bottone "Consuntivo"
                ' Posso inserire MANUTENZIONI_CONSUNTIVI anche a MANUTENZIONI_INTERVENTI.FL_BLOCCATO=1 (provenienti da integrativi)
                ' scalo la somma MANUTENZIONI_INTERVENTI da APPALTI_LOTTI_SERVIZIO.RESIDUO_CONSUMO solo di MANUTENZIONI_INTERVENTI.FL_BLOCCATO=0
                ' abilito il bottone "Ordine Integrativo"
                ' disabilto il bottone "Elimina" e abilito "Annulla Ordine" 
                ' NOTA: 
                '       su "Annulla Ordine" rimetto la somma MANUTENZIONI_INTERVENTI in APPALTI_LOTTI_SERVIZIO.RESIDUO_CONSUMO 
                '                           solo di MANUTENZIONI_INTERVENTI.FL_BLOCCATO=0 e blocca la MANUNETZIONE con MANUTENZIONE.FL_ORDINE_BLOCCATO=1


                If Val(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoControllo"), HiddenField).Value, 0)) > 0 Then
                    RBL1.Enabled = False
                    Me.cmbIndirizzo.Enabled = False
                    Me.cmbScala.Enabled = False
                    Me.cmbVocePF.Enabled = False

                    CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Visible = False
                    CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = False
                    'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = False

                    If Val(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoControlloC"), HiddenField).Value, 0)) > 0 Then
                        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
                    Else
                        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = True
                    End If

                    'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = True
                    'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = False

                    CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = False

                    CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = True

                Else
                    ' se ho cancellato tutti gli INTERVENTI inseriti, posso cambiare l'indirizzo o servizio
                    RBL1.Enabled = False 'True
                    Me.cmbIndirizzo.Enabled = False 'True
                    If RBL1.Items(1).Selected = True Then Me.cmbScala.Enabled = True

                    Me.cmbVocePF.Enabled = True


                    If Me.cmbVocePF.SelectedValue <> "-1" Then
                        CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Visible = True
                        CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = True
                        'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = True

                        'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                        'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = False

                        CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = True

                        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = True
                        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False
                    End If
                End If
                CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = False
                Me.btnElimina.Enabled = False
                Me.btnAnnulla.Enabled = True
                Me.btnOrdineIntegrativo.Enabled = True
                Me.txtVisualizza.Value = 1  'possibilità di inserire allegati

                CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True

                If CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1" Then FrmSolaLettura()
                'CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = True
                'CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Enabled = True
                HiddenMostraPulsantiDett.Value = 1

            Case 2  'CONSUNTIVO (FINE)
                'Disabilito tutti i campi
                'rendo invisibile il bottone "Salva", "Elimina", "Ordine Integrativo"

                'Setta_StatoPagamento()

                If par.IfEmpty(Me.txtID_Pagamento.Value, -1) > 0 Then
                    'NOTA: se ho emessso il SAL allora non posso modificare l'IVA

                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    FrmSolaLettura()

                    Me.txtVisualizza.Value = 2   ' solo consultazione allegati
                    Me.txtSTATO.Value = 2        ' solo RI stampa PAGAMENTO

                    'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                    'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = True


                    CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True
                    CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
                    CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False

                Else
                    Me.txtVisualizza.Value = 2   ' solo consultazione allegati
                    Me.txtSTATO.Value = 2        ' solo RI stampa PAGAMENTO

                    CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Visible = False
                    CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = False
                    'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = False

                    'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = True
                    'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = False


                    CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True
                    CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
                    CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = True

                    Me.btnElimina.Enabled = False
                    Me.btnAnnulla.Enabled = True
                End If

                Me.txtModificato.Text = "0"
                'CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), ImageButton).enabled = False
                'CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = True
                'CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Enabled = True
                HiddenMostraPulsantiDett.Value = 1
            Case 3 'INTEGRATO

                'se ha FIGLI INTEGRATIVI, la scheda viene BLOCCATO, solo gli allegati si possono consultare e RI Stampare l'ordine
                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                Me.BLOCCATO.Value = "2"      ' BLOCCATO COME INTEGRATIVO
                Me.txtVisualizza.Value = 2   ' solo consultazione allegati
                Me.txtSTATO.Value = 3        ' solo RI stampa ORDINE

                'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = True

                CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True

                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False

                Me.txtModificato.Text = "0"
                CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = False
            Case 4 'EMESSO PAGAMENTO (ex LIQUIDATO)

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Visible = False
                CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = False
                'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = False

                'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = True

                CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = False

                CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True

                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False

                Me.txtVisualizza.Value = 2   ' solo consultazione allegati
                Me.txtSTATO.Value = 4        ' NO stampa 

                Me.txtModificato.Text = "0"
                CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = False
            Case 5 'ANNULLATO

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                FrmSolaLettura()

                CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Visible = False
                CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Visible = False
                'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).Visible = False

                'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).Visible = True

                CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = False

                CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True

                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
                CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False

                Me.txtVisualizza.Value = 2   ' solo consultazione allegati
                Me.txtSTATO.Value = 5        ' NO stampa 

                Me.txtModificato.Text = "0"
                TabberHide = "tabbertab"
                CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = False
        End Select

        If txtSTATO.Value > 0 And Me.cmbVocePF.SelectedValue = "-1" Then
            ' QUANDO QUALCUNO cancella la voce di servizio (Non dovrebbe mai capitare)
            CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
            FrmSolaLettura()

            ' CType(Tab_Manu_Dettagli.FindControl("ImageButtonAggiorna"), RadButton).Enabled = False
            ' CType(Tab_Manu_Dettagli.FindControl("btnElimina1"), RadButton).Enabled = False
            HiddenMostraPulsantiDett.Value = 0
            'CType(Tab_Manu_Dettagli.FindControl("btnApri1"), ImageButton).enabled = False

            'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).enabled = True
            'CType(Tab_Manu_Dettagli.FindControl("btnVisualConsuntivo"), ImageButton).enabled = False

            CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).ReadOnly = False

            CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).ReadOnly = True

            CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
            CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False

            Me.txtVisualizza.Value = 2   ' solo consultazione allegati
            Me.txtSTATO.Value = 5        ' NO stampa 

            Me.txtModificato.Text = "0"
        End If

        'If Me.txtID_Segnalazioni.Value > 0 Then
        '    RBL1.Enabled = False
        '    Me.cmbIndirizzo.Enabled = False
        '    Me.cmbScala.Enabled = False
        'End If


        Try
            'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO " _
                                    & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PF_MAIN " _
                                    & "WHERE " _
                                    & "MANUTENZIONI.ID IN (" & vIdManutenzione & ") " _
                                    & "AND MANUTENZIONI.ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0) " _
                                    & "AND MANUTENZIONI.STATO NOT IN (5,6) " _
                                    & "AND MANUTENZIONI.ID_PIANO_FINANZIARIO=PF_MAIN.ID "

                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) >= 6 Then
                        If Session.Item("FL_COMI") = 1 Or Session.Item("FL_ANNULLA_ODL") = "1" Then
                            ANNULLAVISIBILE.Value = 0
                        Else
                            ANNULLAVISIBILE.Value = 1
                        End If
                    End If
                End If
                Lett.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO " _
                                    & "FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PF_MAIN " _
                                    & "WHERE " _
                                    & "MANUTENZIONI.ID IN (" & vIdManutenzione & ") " _
                                    & "AND MANUTENZIONI.ID_PF_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE FL_CC=0) " _
                                    & "AND MANUTENZIONI.STATO NOT IN (5,6) " _
                                    & "AND MANUTENZIONI.ID_PIANO_FINANZIARIO=PF_MAIN.ID "

                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) >= 6 Then
                        If Session.Item("FL_COMI") = 1 Or Session.Item("FL_ANNULLA_ODL") = "1" Then
                            ANNULLAVISIBILE.Value = 0
                        Else
                            ANNULLAVISIBILE.Value = 1
                        End If
                    End If
                End If
                Lett.Close()

            End If


            'bottone consuntivo
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO,MANUTENZIONI.STATO FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                    & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                    & "AND MANUTENZIONI.ID IN (" & vIdManutenzione & ") AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID AND PF_VOCI.FL_CC=0 " _
                    & "AND PRENOTAZIONI.ID_STATO<>-3 AND MANUTENZIONI.STATO<>5"
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) = 6 And par.IfNull(Lett(1), 0) >= 2 Then
                        '  CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                    End If
                End If
                Lett.Close()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Else

                par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO,MANUTENZIONI.STATO FROM SISCOM_MI.MANUTENZIONI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                    & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                    & "AND MANUTENZIONI.ID IN (" & vIdManutenzione & ") AND MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO=PRENOTAZIONI.ID AND PF_VOCI.FL_CC=0 " _
                    & "AND PRENOTAZIONI.ID_STATO<>-3 AND MANUTENZIONI.STATO<>5"
                Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If Lett.Read Then
                    'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                    If par.IfNull(Lett(0), 0) = 6 And par.IfNull(Lett(1), 0) >= 2 Then
                        'CType(Tab_Manu_Dettagli.FindControl("btnConsuntivo"), ImageButton).Visible = False
                    End If
                End If
                Lett.Close()

            End If



        Catch ex As Exception

        End Try

        '*** 19/10/2017 ***
        'IVA BLOCCATA PER RICHIESTA SD 1855/2017
        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_P"), DropDownList).Enabled = False
        CType(Tab_Manu_Riepilogo.FindControl("cmbIVA_C"), DropDownList).Enabled = False
        '*** 19/10/2017 ***


    End Sub


    Sub SettaSegnalazioni(ByVal ID As Long)

        Dim FlagConnessione As Boolean

        Try

            'FlagConnessione = False
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            '    FlagConnessione = True
            'End If


            'If ID <> -1 Then
            '    'CONTROLLO SE ESISTE GIA UNA MANUTENZIONE PER QUELLA SEGNALAZIONE
            '    par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI where ID_SEGNALAZIONI= " & ID
            '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            '    If myReader1.Read Then
            '        'SE SI VISUALIZZO LA SCHEDA MANUTENZIONE
            '        vIdManutenzione = par.IfNull(myReader1("ID"), "-1")
            '        myReader1.Close()
            '        VisualizzaDati()
            '    Else
            '        'SE NO allora RICAVO SOLO l'UBICAZIONE
            '        myReader1.Close()

            '        par.cmd.CommandText = "select * from SISCOM_MI.SEGNALAZIONI where ID= " & ID
            '        myReader1 = par.cmd.ExecuteReader()

            '        If myReader1.Read Then

            '            If par.IfNull(myReader1("ID_COMPLESSO"), "-1") <> "-1" Then
            '                RBL1.Items(0).Selected = True
            '                RBL1.Items(1).Selected = False

            '                Setta_Ubicazione(0, myReader1("ID_COMPLESSO"), "=")
            '                Me.cmbIndirizzo.SelectedValue = par.IfNull(myReader1("ID_COMPLESSO"), "-1")
            '                Me.txtIdComplesso.Text = Me.cmbIndirizzo.SelectedValue

            '                Setta_Scala("=")
            '                'Setta_TipologiaDettaglio()
            '            ElseIf par.IfNull(myReader1("ID_EDIFICIO"), "-1") <> "-1" Then
            '                RBL1.Items(1).Selected = True
            '                RBL1.Items(0).Selected = False

            '                Setta_Ubicazione(1, myReader1("ID_EDIFICIO"), "=")
            '                Me.cmbIndirizzo.SelectedValue = par.IfNull(myReader1("ID_EDIFICIO"), "-1")
            '                Me.txtIdEdificio.Text = Me.cmbIndirizzo.SelectedValue

            '                Setta_Scala("=")
            '                'Setta_TipologiaDettaglio()
            '                'Me.cmbScala.SelectedValue = par.IfNull(myReader1("ID_SCALA"), "-1")
            '            ElseIf par.IfNull(myReader1("ID_UNITA"), "-1") <> "-1" Then

            '                par.cmd.CommandText = "select * from SISCOM_MI.UNITA_IMMOBILIARI where ID= " & par.IfNull(myReader1("ID_UNITA"), "-1")

            '                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            '                If myReader2.Read Then
            '                    RBL1.Items(1).Selected = True
            '                    RBL1.Items(0).Selected = False

            '                    Setta_Ubicazione(1, myReader2("ID_EDIFICIO"), "=")
            '                    Me.cmbIndirizzo.SelectedValue = par.IfNull(myReader2("ID_EDIFICIO"), "-1")
            '                    Me.txtIdEdificio.Text = Me.cmbIndirizzo.SelectedValue

            '                    Setta_Scala("=")
            '                    'Setta_TipologiaDettaglio()
            '                    'Me.cmbScala.SelectedValue = par.IfNull(myReader1("ID_SCALA"), "-1")
            '                End If
            '                myReader2.Close()

            '            End If
            '            myReader1.Close()

            '            Me.txtIdAppalto.Value = -1
            '            Setta_Servizio(0, "=")

            '            Session.Add("LAVORAZIONE", "1")
            '        End If
            '    End If
            'End If


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Add("LAVORAZIONE", "0")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Sub SettaSfitti()
        Dim FlagConnessione As Boolean

        Try

            'FlagConnessione = False
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            '    FlagConnessione = True
            'End If


            'If Me.txtID_Unita.Value <> -1 Then

            '    par.cmd.CommandText = "select SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO,SISCOM_MI.SCALE_EDIFICI.DESCRIZIONE AS SCALA,SCALE_EDIFICI.ID as ID_SCALA " _
            '                    & " from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.SCALE_EDIFICI " _
            '                    & " where SISCOM_MI.UNITA_IMMOBILIARI.ID=" & Me.txtID_Unita.Value _
            '                    & "   and SISCOM_MI.UNITA_IMMOBILIARI.ID_SCALA = SCALE_EDIFICI.ID "

            '    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            '    If myReader2.Read Then
            '        RBL1.Items(1).Selected = True
            '        RBL1.Items(0).Selected = False

            '        Setta_Ubicazione(1, myReader2("ID_EDIFICIO"), "=")
            '        Me.cmbIndirizzo.SelectedValue = par.IfNull(myReader2("ID_EDIFICIO"), "-1")
            '        Me.txtIdEdificio.Text = Me.cmbIndirizzo.SelectedValue

            '        Me.cmbScala.Items.Clear()
            '        Me.cmbScala.Items.Add(New ListItem(" ", -1))

            '        Me.cmbScala.Items.Add(New ListItem(par.IfNull(myReader2("SCALA"), " "), par.IfNull(myReader2("ID_SCALA"), -1)))
            '        Me.cmbScala.SelectedValue = par.IfNull(myReader2("ID_SCALA"), "-1")
            '        Me.cmbScala.Enabled = False

            '        Me.cmbIndirizzo.Enabled = False
            '        Me.RBL1.Enabled = False

            '        'Setta_TipologiaDettaglio()

            '        Setta_VocePF(0, "=")
            '        If Me.cmbVocePF.Items.Count = 2 Then
            '            Me.cmbVocePF.SelectedValue = Me.cmbVocePF.Items(1).Value
            '        End If

            '        Me.txtIdAppalto.Value = -1
            '        Setta_Appalto_Fornitore(">=")

            '    End If
            '    myReader2.Close()

            'End If

            'Session.Add("LAVORAZIONE", "1")


        Catch ex As Exception
            'If FlagConnessione = True Then par.OracleConn.Close()

            'Session.Add("LAVORAZIONE", "0")
            'Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    'Valori passati dalla maschera di ricerca PRE-INSERIMENTO
    Sub SettaValRicercaIND(ByVal Ubicazione As String)
        Dim IDComplessoTMP As Long
        Dim FlagConnessione As Boolean = False

        Try

            If Ubicazione = 1 Then
                'EDIFICIO
                RBL1.Items(1).Selected = True
                RBL1.Items(0).Selected = False

                Setta_Ubicazione(1, sValoreEdificio, "=")
                Me.cmbIndirizzo.SelectedValue = par.IfNull(sValoreEdificio, "-1")
                Me.txtIdEdificio.Text = Me.cmbIndirizzo.SelectedValue

            Else
                'COMPLESSO
                RBL1.Items(0).Selected = True
                RBL1.Items(1).Selected = False

                IDComplessoTMP = TrovaIDComplesso(sValoreEdificio)
                Setta_Ubicazione(0, IDComplessoTMP, "=")
                Me.cmbIndirizzo.SelectedValue = par.IfNull(IDComplessoTMP, "-1")
                Me.txtIdComplesso.Text = par.IfEmpty(Me.cmbIndirizzo.SelectedValue, -1)
            End If

            Setta_Scala("=")

            Setta_VocePF(par.IfNull(sValoreVoce, "-1"), "=")
            Me.cmbVocePF.SelectedValue = par.IfNull(sValoreVoce, "-1")

            Me.txtIdAppalto.Value = par.IfNull(sValoreAppalto, "")
            Setta_Appalto_Fornitore("=")


            AbilitaDisabilita()
            Session.Add("LAVORAZIONE", "1")


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Add("LAVORAZIONE", "0")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Sub GestionePrenotazioni()
        Dim FlagConnessione As Boolean
        Dim bTrovato As Boolean = False

        Try
            If Me.cmbStato.SelectedValue > 0 Then
                If Me.txtID_Prenotazione.Value < 0 Then

                    FlagConnessione = False
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)

                        FlagConnessione = True
                    End If

                    'Verifica caso mai è stata gia assegnata e creata una prenotazione alla manutenzione
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select ID_PRENOTAZIONE_PAGAMENTO from SISCOM_MI.MANUTENZIONI where ID=" & vIdManutenzione & " and MANUTENZIONI.STATO<>5 "
                    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReaderT.Read Then
                        If par.IfNull(myReaderT("ID_PRENOTAZIONE_PAGAMENTO"), -1) > 0 Then
                            Me.txtID_Prenotazione.Value = par.IfNull(myReaderT("ID_PRENOTAZIONE_PAGAMENTO"), -1)
                            bTrovato = True
                        End If
                    End If
                    myReaderT.Close()
                    '********************

                    If bTrovato = True Then
                        If FlagConnessione = True Then
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        End If
                        Exit Sub
                    End If



                    'CREO PRENOTAZIONI
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL"
                    myReaderT = par.cmd.ExecuteReader()

                    If myReaderT.Read Then
                        Me.txtID_Prenotazione.Value = myReaderT(0)
                    End If
                    myReaderT.Close()

                    '' TIPO_PAGAMENTO=3 (TIPO_PAGAMENTI.ID =3 ODL MANUTENZIONI)


                    ' PRENOTAZIONI
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.PRENOTAZIONI " _
                                                & " (ID,DESCRIZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,DATA_PRENOTAZIONE,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) " _
                                        & "values (:id,:descrizione,:importo,:id_fornitore,:id_appalto,:id_voce,:tipo_pagamento,:data_prenotazione,:id_struttura,:rit_legge,:iva_consumo)"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Convert.ToInt32(Me.txtID_Prenotazione.Value)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text, 2000)))

                    'CONTIENE GIA L'IMPORTO DA PAGARE + EVENTUALE RITENUTA IVATA
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtNettoOneriIVA_TMP.Value.Replace(".", ""))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", Convert.ToInt32(Me.txtID_Fornitore.Value)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbVocePF.SelectedValue))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_pagamento", 7))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_prenotazione", par.AggiustaData(Me.lblData.Text)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Session.Item("ID_STRUTTURA"))))

                    'RITENUTA IVATA + % DELL'IVA A CONSUMO
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumo", Convert.ToInt32(Me.txtPercIVA_P.Value)))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '*********************************

                    'MANUTENZIONI (UPDATE)
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set ID_PRENOTAZIONE_PAGAMENTO=" & Convert.ToInt32(Me.txtID_Prenotazione.Value) _
                                       & " where ID=" & vIdManutenzione

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""

                    ''****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
                    'par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & Convert.ToInt32(Me.txtID_Pagamento.Value) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','')"
                    'par.cmd.ExecuteNonQuery()
                    '****************************************************

                Else
                    'MODIFICO PRENOTAZIONI
                    par.cmd.Parameters.Clear()

                    If Me.cmbStato.SelectedValue = 2 Then
                        'CONSUNTIVO
                        par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                                               & " set ID_STATO=1, IMPORTO_APPROVATO=:importo,"

                        'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=1,IMPORTO_APPROVATO=" & par.VirgoleInPunti(par.IfEmpty(Me.txtNettoOneriIVAC_TMP.Value.Replace(".", ""), 0)) & ","
                    Else
                        par.cmd.CommandText = " update SISCOM_MI.PRENOTAZIONI " _
                                               & " set ID_STATO=0, IMPORTO_PRENOTATO=:importo,"

                        'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0,IMPORTO_PRENOTATO=" & par.VirgoleInPunti(par.IfEmpty(Me.txtNettoOneriIVA_TMP.Value.Replace(".", ""), 0)) & ","
                    End If

                    'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set IMPORTO_PRENOTATO=" & par.VirgoleInPunti(par.IfEmpty(Me.txtNettoOneriIVA_TMP.Value.Replace(".", ""), 0)) & ","

                    'If Me.cmbStato.SelectedValue = 2 Then
                    '    'CONSUNTIVO (FINE)
                    '    par.cmd.CommandText = par.cmd.CommandText & "     IMPORTO_PRENOTATO=" & par.VirgoleInPunti(par.IfEmpty(Me.txtNettoOneriIVAC_TMP.Value.Replace(".", ""), 0)) & ","
                    'End If

                    'par.cmd.CommandText = par.cmd.CommandText & "         ID_APPALTO=" & Convert.ToInt32(Me.txtIdAppalto.Value) & "," _
                    '                                                 & "  ID_FORNITORE=" & Convert.ToInt32(Me.txtID_Fornitore.Value) & "," _
                    '                                                 & "  ID_VOCE_PF=" & Convert.ToInt32(Me.cmbVocePF.SelectedValue) _
                    '                   & " where ID=" & Convert.ToInt32(Me.txtID_Prenotazione.Value)


                    par.cmd.CommandText = par.cmd.CommandText & "ID_APPALTO=:id_appalto,ID_FORNITORE=:id_fornitore,ID_VOCE_PF=:id_voce_pf,RIT_LEGGE_IVATA=:rit_legge,PERC_IVA=:iva_consumo " _
                                        & " where ID=" & Convert.ToInt32(Me.txtID_Prenotazione.Value)

                    'CONTIENE GIA L'IMPORTO DA PAGARE + EVENTUALE RITENUTA IVATA
                    If Me.cmbStato.SelectedValue = 2 Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtNettoOneriIVAC_TMP.Value.Replace(".", ""))))
                    Else
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Me.txtNettoOneriIVA_TMP.Value.Replace(".", ""))))
                    End If

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", Convert.ToInt32(Me.txtID_Fornitore.Value)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce_pf", RitornaNullSeIntegerMenoUno(Convert.ToInt32(Me.cmbVocePF.SelectedValue))))

                    'RITENUTA IVATA + % DELL'IVA A CONSUMO
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))))

                    If Me.cmbStato.SelectedValue = 2 Then
                        'CONSUNTIVO
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumo", Convert.ToInt32(Me.txtPercIVA_C.Value)))
                    Else
                        'PREVENTIVO
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumo", Convert.ToInt32(Me.txtPercIVA_P.Value)))
                    End If

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                    '********************************************

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



    Sub GestionePENALE()
        Dim FlagConnessione As Boolean

        Try
            If Me.txtIdPenale.Value < 0 Then

                If par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text, 0) = 0 Then
                    Exit Sub
                End If

                FlagConnessione = False
                If par.OracleConn.State = Data.ConnectionState.Closed Then
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    FlagConnessione = True
                End If

                'CREO APPALTI_PENALI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select SISCOM_MI.SEQ_APPALTI_PENALI.NEXTVAL FROM DUAL"
                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReaderT.Read Then
                    Me.txtIdPenale.Value = myReaderT(0)
                End If
                myReaderT.Close()


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.APPALTI_PENALI " _
                                            & " (ID,ID_MANUTENZIONE,ID_APPALTO,IMPORTO) " _
                                    & "values (:id,:id_manu,:id_appalto,:importo)"

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Me.txtIdPenale.Value))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manu", vIdManutenzione))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text.Replace(".", ""))))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************************
            Else

                If par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text, 0) = 0 Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "delete from SISCOM_MI.APPALTI_PENALI where ID=" & Me.txtIdPenale.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    Me.txtIdPenale.Value = -1
                Else

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " update SISCOM_MI.APPALTI_PENALI " _
                                        & " set ID_APPALTO=:id_appalto, IMPORTO=:importo " _
                                        & " where ID=:id"

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Me.txtIdPenale.Value))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()

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



    'Protected Sub btn_Chiudi1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Chiudi1.Click
    '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    'End Sub

















    Protected Sub btnOrdineIntegrativo_Click(sender As Object, e As System.EventArgs) Handles btnOrdineIntegrativo.Click
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

        Dim Id_ManInterventi As Long
        'Dim ODL As Integer
        Dim sODL_ANNO_OLD As String
        Dim sOLD_DATA As String

        Dim importo_tot As Decimal 'A netto compresi oneri e IVA + Ritenuta di legge al 5%

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtOrdine.Value = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                ' '' Ricavo vIdManutenzione
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select SISCOM_MI.SEQ_MANUTENZIONI.NEXTVAL FROM dual "
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    Me.txtIdManutenzione.Text = myReader1(0)
                End If
                myReader1.Close()
                par.cmd.CommandText = ""
                '**********************

                ' calcolo dell'ODL COME ERA PRIMA
                'par.cmd.CommandText = "select PROGRESSIVO from SISCOM_MI.MANUTENZIONI_ODL where SISCOM_MI.MANUTENZIONI_ODL.ANNO='" & DateTime.Now.Year & "'"
                'myReader1 = par.cmd.ExecuteReader()
                'If myReader1.Read Then
                '    ODL = myReader1(0) + 1
                '    myReader1.Close()
                '    par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI_ODL set PROGRESSIVO=" & ODL & " where ANNO='" & DateTime.Now.Year & "'"

                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""
                'Else
                '    ODL = 1
                '    myReader1.Close()
                '    par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_ODL (PROGRESSIVO,ANNO) values " _
                '               & "(" & ODL & ",'" & DateTime.Now.Year & "')"

                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""
                'End If


                '***************************************

                Me.cmbStato.SelectedValue = 0 'SETTO LA NUOVA MANUTENZIONE A BOZZA

                'A netto compresi oneri e IVA + Ritenuta di legge al 5%
                If Me.cmbStato.SelectedValue = 1 Then
                    'EMESSO
                    importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenuta"), TextBox).Text.Replace(".", ""))

                ElseIf Me.cmbStato.SelectedValue = 2 Then
                    'CONSUNTIVO
                    importo_tot = strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtNettoOneriIVAC"), TextBox).Text.Replace(".", "")) + strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))
                End If



                ' INSERT MANUTENZIONI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI " _
                                            & " (ID,ID_COMPLESSO,ID_EDIFICIO,ID_SCALA,ID_PF_VOCE,ID_APPALTO,DESCRIZIONE," _
                                            & " DATA_INIZIO_ORDINE,DATA_FINE_ORDINE,DATA_INIZIO_INTERVENTO,DATA_FINE_INTERVENTO,STATO,IMPORTO_CONSUNTIVATO,IMPORTO_PRESUNTO," _
                                            & " ID_SEGNALAZIONI,ID_PIANO_FINANZIARIO,RIT_LEGGE,RIMBORSI,ID_PRENOTAZIONE_PAGAMENTO,IMPORTO_TOT,IMPORTO_RESIDUO,ID_UNITA_IMMOBILIARI,IVA_CONSUMO_P,IVA_CONSUMO,DANNEGGIANTE,DANNEGGIATO,ID_STRUTTURA,IMPORTO_ONERI_PREV,IMPORTO_ONERI_CONS,ID_FORNITORE_STAMPA) " _
                                    & "values (:id,:id_complesso,:id_edificio,:id_scala,:id_voce,:id_appalto,:descrizione," _
                                            & " :data_inizio,:data_fine,:data_inizio_inter,:data_fine_inter,:stato,:imp_consuntivo,:imp_presunto," _
                                            & " :id_segnalazioni,:id_piano_finanziario,:rit_legge,:rimborsi,:id_prenotazione,:importo_tot,:importo_residuo,:id_unita_immobiliare,:iva_consumoP,:iva_consumoC,:danneggiante,:danneggiato,:id_struttura,:imp_oneri_presunto,:imp_oneri_consuntivo,:ID_FORNITORE_STAMPA)"
                Dim dataInizio As String = ""
                If Not IsNothing(txtDataInizio.SelectedDate) Then
                    dataInizio = txtDataInizio.SelectedDate
                End If

                Dim dataFine As String = ""
                If Not IsNothing(txtDataFine.SelectedDate) Then
                    dataFine = txtDataFine.SelectedDate
                End If
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Me.txtIdManutenzione.Text))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(txtIdComplesso.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(txtIdEdificio.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_scala", RitornaNullSeIntegerMenoUno(Convert.ToInt32(par.IfEmpty(Me.cmbScala.SelectedValue, -1)))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_voce", Convert.ToInt32(Me.cmbVocePF.SelectedValue)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Convert.ToInt32(Me.txtIdAppalto.Value)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDescrizione"), TextBox).Text, 4000)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_inizio", par.AggiustaData(Me.lblData.Text)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_fine", ""))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_inizio_inter", par.AggiustaData(dataInizio)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_fine_inter", par.AggiustaData(dataFine)))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anno", DateTime.Now.Year))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", Convert.ToInt32(Me.cmbStato.SelectedValue)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_consuntivo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_presunto", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtImporto"), TextBox).Text.Replace(".", ""))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_segnalazioni", RitornaNullSeIntegerMenoUno(Me.txtID_Segnalazioni.Value)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_piano_finanziario", RitornaNullSeIntegerMenoUno(Me.txtIdPianoFinanziario.Value)))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("penale", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtPenale"), TextBox).Text.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRitenutaC"), TextBox).Text.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rimborsi", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text.Replace(".", ""))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_prenotazione", RitornaNullSeIntegerMenoUno(Me.txtID_Prenotazione.Value)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_tot", strToNumber(importo_tot)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_residuo", strToNumber(CType(Tab_Manu_Dettagli.FindControl("txtResiduoConsumo"), HiddenField).Value.Replace(".", ""))))


                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita_immobiliare", RitornaNullSeIntegerMenoUno(Me.txtID_Unita.Value)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumoP", Convert.ToInt32(Me.txtPercIVA_P.Value)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("iva_consumoC", Convert.ToInt32(Me.txtPercIVA_C.Value)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("danneggiante", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDanneggiante"), TextBox).Text, 500)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("danneggiato", Strings.Left(CType(Tab_Manu_Dettagli.FindControl("txtDanneggiato"), TextBox).Text, 500)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_struttura", RitornaNullSeIntegerMenoUno(Session.Item("ID_STRUTTURA"))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_oneri_presunto", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtOneriP_MANO"), HiddenField).Value.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("imp_oneri_consuntivo", strToNumber(CType(Tab_Manu_Riepilogo.FindControl("txtOneriC_MANO"), HiddenField).Value.Replace(".", ""))))


                Dim idFS As Integer = 0
                If IsNumeric(cmbIntestazione.SelectedValue) AndAlso CInt(cmbIntestazione.SelectedValue) > 0 Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_FORNITORE_STAMPA", Convert.ToInt32(cmbIntestazione.SelectedValue)))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_FORNITORE_STAMPA", DBNull.Value))
                End If


                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '*****************************************

                'PENALE
                If Me.txtIdPenale.Value > 0 Then
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "delete from SISCOM_MI.APPALTI_PENALI where ID=" & Me.txtIdPenale.Value
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    Me.txtIdPenale.Value = -1

                    GestionePENALE()
                End If



                '****** LETTURA ODL
                sODL_ANNO_OLD = lblODL1.Text
                sOLD_DATA = lblData.Text

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select PROGR,ANNO from SISCOM_MI.MANUTENZIONI where ID=" & Me.txtIdManutenzione.Text
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then

                    lblODL1.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
                    lblData.Text = DateTime.Now.Date
                End If
                myReader1.Close()
                '*****************************************

                '*** EVENTI_MANUTENZIONE
                par.InserisciEventoManu(par.cmd, Me.txtIdManutenzione.Text, Session.Item("ID_OPERATORE"), Epifani.TabEventi.INSERIMENTO_MANUTENZIONE, "")


                'INSERT MANUTENZIONI_INTERVENTI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI_INTERVENTI where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE = " & vIdManutenzione
                myReader1 = par.cmd.ExecuteReader()

                While myReader1.Read

                    ' '' Ricavo ID MANUTENZIONI INTERVENTI
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = " select SISCOM_MI.SEQ_MANUTENZIONI_INTERVENTI.NEXTVAL FROM dual "
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        Id_ManInterventi = myReader2(0)
                    End If
                    myReader2.Close()
                    par.cmd.CommandText = ""
                    '**********************

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_INTERVENTI  " _
                                                & " (ID, ID_MANUTENZIONE,ID_IMPIANTO,ID_COMPLESSO,ID_EDIFICIO,ID_UNITA_IMMOBILIARE,ID_UNITA_COMUNE,TIPOLOGIA,IMPORTO_PRESUNTO,FL_BLOCCATO,ID_SCALA) " _
                                        & "values (:id,:id_manu,:id_impianto,:id_complesso,:id_edificio,:id_unita,:id_comune,:tipologia,:importo,1,:ID_SCALA) "

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", Id_ManInterventi))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manu", Me.txtIdManutenzione.Text))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_impianto", RitornaNullSeIntegerMenoUno(par.IfNull(myReader1("ID_IMPIANTO"), "-1"))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_complesso", RitornaNullSeIntegerMenoUno(par.IfNull(myReader1("ID_COMPLESSO"), "-1"))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_edificio", RitornaNullSeIntegerMenoUno(par.IfNull(myReader1("ID_EDIFICIO"), "-1"))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_unita", RitornaNullSeIntegerMenoUno(par.IfNull(myReader1("ID_UNITA_IMMOBILIARE"), "-1"))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_comune", RitornaNullSeIntegerMenoUno(par.IfNull(myReader1("ID_UNITA_COMUNE"), "-1"))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_SCALA", RitornaNullSeIntegerMenoUno(par.IfNull(myReader1("ID_SCALA"), "-1"))))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipologia", par.IfNull(myReader1("TIPOLOGIA"), "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", par.IfNull(myReader1("IMPORTO_PRESUNTO"), "")))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""


                    'INSERT MANUTENZIONI_CONSUNTIVI
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where ID_MANUTENZIONI_INTERVENTI=" & par.IfNull(myReader1("ID"), "-1")
                    myReader2 = par.cmd.ExecuteReader()

                    While myReader2.Read

                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.MANUTENZIONI_CONSUNTIVI  " _
                                                    & " (ID, ID_MANUTENZIONI_INTERVENTI,ID_UM,COD_ARTICOLO,DESCRIZIONE,QUANTITA,PREZZO_UNITARIO,PREZZO_TOTALE) " _
                                            & "values (SISCOM_MI.SEQ_MANUTENZIONI_CONSUNTIVI.NEXTVAL,:id_manu,:id_um,:cod_articolo,:descrizione,:quantita,:prezzo_uni,:prezzo_tot) "

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_manu", Id_ManInterventi))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_um", RitornaNullSeIntegerMenoUno(par.IfNull(myReader2("ID_UM"), "-1"))))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_articolo", par.IfNull(myReader2("COD_ARTICOLO"), "")))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", par.IfNull(myReader2("DESCRIZIONE"), "")))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("quantita", par.IfNull(myReader2("QUANTITA"), "")))

                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_uni", par.IfNull(myReader2("PREZZO_UNITARIO"), "")))
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("prezzo_tot", par.IfNull(myReader2("PREZZO_TOTALE"), "")))


                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()

                    End While
                    myReader2.Close()

                End While
                myReader1.Close()

                '******************************************
                '******************************************


                ' UPDATE MANUTENZIONI VECCHIO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set " _
                                        & " STATO=3," _
                                        & " ID_PRENOTAZIONE_PAGAMENTO=Null," _
                                        & " ID_SEGNALAZIONI=Null," _
                                        & " ID_FIGLIO=" & Me.txtIdManutenzione.Text _
                                    & "where ID=" & vIdManutenzione

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '********************************


                vIdManutenzione = Me.txtIdManutenzione.Text

                ' COMMIT
                par.myTrans.Commit()


                '**** AGGIORNO I TAB con GRIGLIE

                '*** MANUTENZIONI_INTERVENTI
                Dim sSQL_DettaglioIMPIANTO As String
                sSQL_DettaglioIMPIANTO = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                            & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                                    & "(select  (CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                                    & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                            & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END) " _
                                                    & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                            & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                            & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                        & " ELSE '' " _
                                        & " END) as DETTAGLIO "

                par.cmd.Parameters.Clear()
                '       par.cmd.CommandText = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                '                  & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '                  & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '                  & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                '                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                '                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
                '            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                '                  & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '                  & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '                  & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                '                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                '                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                '            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                '                 & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '                  & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '                 & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                '                 & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                '                 & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                '           & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                '                 & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '                  & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '                 & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                '                 & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                '                 & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
                '           & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE," _
                '                  & "       SISCOM_MI.EDIFICI.DENOMINAZIONE||' - Scala '||SCALE_EDIFICI.descrizione  AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '                  & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '                  & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI " _
                '                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                '                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID " _
                '           & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                '                  & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '                  & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '            & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                '                 & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) " _
                '                                       & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,'' AS TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                '       & "      '' AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                '          & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                '& "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.imponibile_rimborso) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                '    & " from SISCOM_MI.MANUTENZIONI_INTERVENTI   " _
                '         & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and (SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA is null or tipologia='-1') "
                par.cmd.CommandText = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                           & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                           & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                           & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                           & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                           & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
                     & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                           & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                           & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                           & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                           & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                           & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
                     & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                          & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                           & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                          & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                          & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                          & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
                    & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                          & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                           & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                          & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                          & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                          & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                           & "       SISCOM_MI.EDIFICI.DENOMINAZIONE||' - Scala '||SCALE_EDIFICI.descrizione  AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                           & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                           & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI " _
                           & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                           & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID " _
                    & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                           & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                           & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                     & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) "




                Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ds1 As New Data.DataTable()

                da1.Fill(ds1) ', "MANUTENZIONI_INTERVENTI")


                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).DataSource = ds1
                CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).DataBind()

                da1.Dispose()
                ds1.Dispose()

                If CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items.Count = 1 Then
                    'CType(Tab_Manu_Dettagli.FindControl("txtSel1"), TextBox).Text = par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), DataGrid).Items(0).Cells(1).Text, "")
                    CType(Tab_Manu_Dettagli.FindControl("txtIdComponente"), TextBox).Text = par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items(0).Cells(2).Text, "")
                    CType(Tab_Manu_Dettagli.FindControl("txt_FL_BLOCCATO"), HiddenField).Value = par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items(0).Cells(13).Text, "")
                End If
                ''*******************************

                Me.lblDataInizio.Text = "Data Presunta Inizio Lavori:"
                Me.lblDataFine.Text = "Data Presunta Fine Lavori"

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                'APPALTI_LOTTI_SERVIZI
                Setta_ImportoResiduo()

                'ABILITO/DISABILITO CAMPI in base allo stato
                AbilitaDisabilita()

                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                txtModificato.Text = "0"
                txtOrdine.Value = "0"


                RadNotificationNote.Text = "Operazione completata correttamente! L\'ordine " & lblODL1.Text & " è integrativo dell\'ordine " & sODL_ANNO_OLD
                RadNotificationNote.Show()
                imgUscita.Visible = True
                btnElimina.Visible = False
                btnINDIETRO.Visible = False
                'BloccoOrdineIntegrativo.Value = "1"
                CType(Tab_Manu_Dettagli, Object).BindGrid_Interventi()

                ' MODIFICA MARCO 27/02/2012
                '~~~~~~~~~~~~~~~~~~~~~~~~~~
                cmbStato.SelectedValue = 1
                cmbStato.Enabled = False


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

            Session.Item("LAVORAZIONE") = "0"

            Page.Dispose()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub











    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If txtElimina.Text = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                'Se è integrativo, allora il padre passa da INTEGRATO=3 a EMESSO=1
                par.cmd.CommandText = "select ID from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & vIdManutenzione
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=1,ID_FIGLIO=Null where ID=" & myReader1(0)

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                End If
                myReader1.Close()
                '****************************

                'If Me.txtID_Segnalazioni.Value > 0 Then
                '    par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=0 where ID=" & Me.txtID_Segnalazioni.Value

                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""


                '    par.cmd.CommandText = "insert into SISCOM_MI.SEGNALAZIONI_NOTE " _
                '                & " (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) values " _
                '                & " (" & Me.txtID_Segnalazioni.Value & ",'ORDINE num: " & Me.lblODL1.Text & " in data: " & Me.lblData.Text & " ANNULLATO','" _
                '                & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""

                'End If

                par.cmd.Parameters.Clear()
                '27/03/2013
                'par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=5,ID_PRENOTAZIONE_PAGAMENTO=Null where ID=" & vIdManutenzione
                par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=5 where ID=" & vIdManutenzione
                par.cmd.ExecuteNonQuery()

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set  ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set  ID_STATO=-3 where ID=" & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()


                'PENALE
                'If Me.txtIdPenale.Value > 0 Then
                '    par.cmd.Parameters.Clear()
                '    par.cmd.CommandText = "delete from SISCOM_MI.APPALTI_PENALI where ID=" & Me.txtIdPenale.Value
                '    par.cmd.ExecuteNonQuery()
                '    par.cmd.CommandText = ""
                '    Me.txtIdPenale.Value = -1
                'End If


                '*** EVENTI_MANUTENZIONE
                par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE, "Da Emesso Ordine ad Annullato")

                Setta_StataoODL(5)

                Me.cmbStato.SelectedValue = 5

                Me.txtSTATO.Value = 5   'ANNULLATO
                Me.txtID_Prenotazione.Value = -1

                ' COMMIT
                par.myTrans.Commit()

                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                'ABILITO/DISABILITO CAMPI in base allo stato
                AbilitaDisabilita()

                Me.lblODL_INTEGRAZIONE.Visible = False
                Me.lblOLDINTEGRAZIONE.Visible = False
                Me.lblODL_INTEGRATO.Visible = False
                Me.lblOLDINTEGRATO.Visible = False


                'APPALTI_LOTTI_SERVIZI
                Setta_ImportoResiduo()

                CalcolaImportiC(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))


                '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI  where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
                myReader1 = par.cmd.ExecuteReader()
                myReader1.Close()

                'CREO LA TRANSAZIONE
                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                RadNotificationNote.Text = "Annullamento completato correttamente!"
                RadNotificationNote.Show()
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                Me.txtModificato.Text = "0"

            Else
                CType(Me.Page.FindControl("txtElimina"), TextBox).Text = "0"
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

    Protected Sub btnAnnullaManu_Click(sender As Object, e As System.EventArgs) Handles btnAnnullaManu.Click
        Try
            If String.IsNullOrEmpty(txtNote.Text) Then
                RadWindowManager1.RadAlert("Inserire la motivazione!", 300, 150, "Attenzione", "", "null")
                Me.txtAppare1.Value = "1"
                Exit Sub
            End If


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim idOperatoreAttuale As Integer = Session.Item("ID_OPERATORE")
            par.cmd.CommandText = "SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL " _
                & " WHERE data_inizio_incarico<='" & Format(Now, "yyyyMMdd") _
                & "' and data_fine_incarico>'" & Format(Now, "yyyyMMdd") _
                & "' and APPALTI_DL.ID_GRUPPO IN(SELECT APPALTI.ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID IN (select id_appalto from siscom_mi.MANUTENZIONI where id=" & par.IfEmpty(vIdManutenzione, 0) & "))"
            Dim operatoreDL As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = "select nvl(fl_superdirettore,0) from sepa.operatori where sepa.operatori.id=" & idOperatoreAttuale
            Dim fl_superdirettore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)

            If fl_superdirettore = 0 Then
                If idOperatoreAttuale <> operatoreDL Then
                    RadWindowManager1.RadAlert("L\'ordine può essere annullato esclusivamente dal Direttore Lavori!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                    '*********************CHIUSURA CONNESSIONE**********************
                End If
            End If


            CType(Tab_Manu_Note.FindControl("txtNote"), TextBox).Text = Me.txtNote.Text

            'Se è integrativo, allora il padre passa da INTEGRATO=3 a EMESSO=1
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select ID from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & vIdManutenzione
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set STATO=1,ID_FIGLIO=Null where ID=" & myReader1(0)

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
            End If
            myReader1.Close()
            '****************************


            'If Me.txtID_Segnalazioni.Value > 0 Then
            '    par.cmd.CommandText = "update SISCOM_MI.SEGNALAZIONI set ID_STATO=0 where ID=" & Me.txtID_Segnalazioni.Value

            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""


            '    par.cmd.CommandText = "insert into SISCOM_MI.SEGNALAZIONI_NOTE " _
            '                & " (ID_SEGNALAZIONE, NOTE, DATA_ORA, ID_OPERATORE) values " _
            '                & " (" & Me.txtID_Segnalazioni.Value & ",'ORDINE num: " & Me.lblODL1.Text & " in data: " & Me.lblData.Text & " ANNULLATO','" _
            '                & Format(Now, "yyyyMMddHHmm") & "', " & Session.Item("ID_OPERATORE") & ")"
            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""

            'End If

            par.cmd.Parameters.Clear()
            '27/03/2013 CAMBIO ANNULLO MANUTENZIONI
            'par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set " _
            '              & " STATO=5," _
            '              & " ID_PRENOTAZIONE_PAGAMENTO=Null," _
            '              & " NOTE='" & par.PulisciStrSql(txtNote.Text) & "'" _
            '              & " where ID=" & vIdManutenzione


            par.cmd.CommandText = "update SISCOM_MI.MANUTENZIONI set " _
                                      & " STATO=5," _
                                      & " NOTE='" & par.PulisciStrSql(txtNote.Text) & "'" _
                                      & " where ID=" & vIdManutenzione
            par.cmd.ExecuteNonQuery()

            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "delete from SISCOM_MI.PRENOTAZIONI where ID = " & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
            '27/032013 CAMBIO ANNULLO MANUTENZIONI
            'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set  ID_STATO=0, IMPORTO_PRENOTATO=0,IMPORTO_APPROVATO=0 where ID=" & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)

            par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set  ID_STATO=-3 where ID=" & par.IfEmpty(Me.txtID_Prenotazione.Value, 0)
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            'PENALE
            'If Me.txtIdPenale.Value > 0 Then
            '    par.cmd.Parameters.Clear()
            '    par.cmd.CommandText = "delete from SISCOM_MI.APPALTI_PENALI where ID=" & Me.txtIdPenale.Value
            '    par.cmd.ExecuteNonQuery()
            '    par.cmd.CommandText = ""
            '    Me.txtIdPenale.Value = -1
            'End If


            '*** EVENTI_MANUTENZIONE
            par.InserisciEventoManu(par.cmd, vIdManutenzione, Session.Item("ID_OPERATORE"), Epifani.TabEventi.MODIFICA_STATO_MANUTENZIONE, "Da Emesso Ordine ad Annullato")

            Setta_StataoODL(5)

            Me.cmbStato.SelectedValue = 5

            Me.txtSTATO.Value = 5   'ANNULLATO
            Me.txtID_Prenotazione.Value = -1

            ' COMMIT
            par.myTrans.Commit()

            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()

            'APPALTI_LOTTI_SERVIZI
            Setta_ImportoResiduo()

            'ABILITO/DISABILITO CAMPI in base allo stato
            AbilitaDisabilita()

            Me.lblODL_INTEGRAZIONE.Visible = False
            Me.lblOLDINTEGRAZIONE.Visible = False
            Me.lblODL_INTEGRATO.Visible = False
            Me.lblOLDINTEGRATO.Visible = False

            CalcolaImportiC(par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtImportoC"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Manu_Riepilogo.FindControl("txtRimborsi"), TextBox).Text, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_C"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI  where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            myReader1.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            RadNotificationNote.Text = "Annullamento completato correttamente!"
            RadNotificationNote.Show()
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Me.txtModificato.Text = "0"


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
    Protected Sub btnChiudiAnnullaManu_Click(sender As Object, e As System.EventArgs) Handles btnChiudiAnnullaManu.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
    End Sub
    'Protected Sub btn_VisUnita_Click(sender As Object, e As System.EventArgs) Handles btn_VisUnita.Click
    '    Response.Write("<script>window.open('../../../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & Me.txtID_Unita.Value & "','Dettagli" & Format(Now, "hhss") & "','height=580,top=0,left=0,width=780');</script>")

    '    '<a href='javascript:void(0)' onclick=" & Chr(34) & "window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & par.IfNull(myReader("cod_unita_immobiliare"), "") & "','Dettagli" & Format(Now, "hhss") & "','height=580,top=0,left=0,width=780');" & Chr(34) & ">" & tuoTestoConLink & "</a>

    'End Sub
    Private Function TrovaIDComplesso(ByVal IdEdificio As String) As Long
        Dim FlagConnessione As Boolean

        Try

            TrovaIDComplesso = 0

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = "select ID_COMPLESSO  from SISCOM_MI.EDIFICI where ID= " & IdEdificio

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                TrovaIDComplesso = par.IfNull(myReader1("ID_COMPLESSO"), "0")
            End While
            myReader1.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            TrovaIDComplesso = 0

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function
    'TROVA l'ANNO dell'ultimo ESERCIZIO FINANAZIO APPROVATO
    ' serve se sto utilizzando ES chiuso  o non utilizzabile (6,7) del 2011
    ' e sto inserendo data inizio lavori al 2012 
    Private Function TrovaANNO_ES_APPROVATO() As Integer
        Dim FlagConnessione As Boolean

        Try

            TrovaANNO_ES_APPROVATO = 0

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            par.cmd.CommandText = "select SUBSTR(FINE,0,4) AS ANNO_ESERCIZIO " _
                                & " from SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                                & " where PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID and ID_STATO = 5"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                TrovaANNO_ES_APPROVATO = par.IfNull(myReader1("ANNO_ESERCIZIO"), "0")
            End While
            myReader1.Close()

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            TrovaANNO_ES_APPROVATO = 0

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function
    Protected Sub btnINFOintegrato_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnINFOintegrato.Click
        If txtModificato.Text <> "111" Then

            'RICERCA MANUTENZIONI RRS
            sValoreStruttura = Request.QueryString("STR")
            sValoreEsercizioF = Request.QueryString("ES")

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreVoce = Request.QueryString("SV")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreStato = Request.QueryString("ST")

            'RICERCA DIRETTA
            sValoreRepertorio = Request.QueryString("REP")
            sValoreODL = Request.QueryString("ODL") 'Request.QueryString("PROGR")
            sValoreAnno = Request.QueryString("ANNO")
            '************

            'Da Ricerca PRE-INSERIMENTO RicercaRRS_INS.aspx
            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
            sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))
            sOrdinamento = Request.QueryString("ORD")
            sValoreProvenienza = Request.QueryString("PROVENIENZA")

            '******************************************************


            'RICERCA SFITTI
            sValoreUnita = Request.QueryString("UI")
            '***********************************************


            Session.Add("ID", Me.txtIntegrato.Value)

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

            Response.Write("<script>location.replace('ManutenzioniRRS.aspx?ED=" & sValoreEdificio _
                                                                        & "&CO=" & sValoreComplesso _
                                                                        & "&SV=" & sValoreVoce _
                                                                        & "&FO=" & sValoreFornitore _
                                                                        & "&AP=" & sValoreAppalto _
                                                                        & "&DAL=" & sValoreData_Dal _
                                                                        & "&AL=" & sValoreData_Al _
                                                                        & "&ST=" & sValoreStato _
                                                                        & "&REP=" & sValoreRepertorio _
                                                                        & "&ODL=" & sValoreODL _
                                                                        & "&ANNO=" & sValoreAnno _
                                                                        & "&UI=" & sValoreUnita _
                                                                        & "&IN_R=" & sValoreIndirizzoR _
                                                                        & "&SV_R=" & sValoreVoceR _
                                                                        & "&AP_R=" & sValoreAppaltoR _
                                                                        & "&ORD=" & sOrdinamento _
                                                                        & "&UBI=" & sValoreUbicazione _
                                                                        & "&STR=" & sValoreStruttura _
                                                                        & "&es=" & sValoreEsercizioF _
                                                                        & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")



        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub


    Protected Sub btnINFOintegrazione_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnINFOintegrazione.Click
        If txtModificato.Text <> "111" Then

            'RICERCA MANUTENZIONI RRS
            sValoreStruttura = Request.QueryString("STR")
            sValoreEsercizioF = Request.QueryString("ES")

            sValoreComplesso = Request.QueryString("CO")
            sValoreEdificio = Request.QueryString("ED")
            sValoreVoce = Request.QueryString("SV")

            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")
            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            sValoreStato = Request.QueryString("ST")

            'RICERCA DIRETTA
            sValoreRepertorio = Request.QueryString("REP")
            sValoreODL = Request.QueryString("ODL") 'Request.QueryString("PROGR")
            sValoreAnno = Request.QueryString("ANNO")
            '************

            'Da Ricerca PRE-INSERIMENTO RicercaRRS_INS.aspx
            sValoreIndirizzoR = Strings.Trim(Request.QueryString("IN_R"))
            sValoreVoceR = Strings.Trim(Request.QueryString("SV_R"))
            sValoreAppaltoR = Strings.Trim(Request.QueryString("AP_R"))

            sValoreUbicazione = Strings.Trim(Request.QueryString("UBI"))
            sOrdinamento = Request.QueryString("ORD")
            sValoreProvenienza = Request.QueryString("PROVENIENZA")

            '******************************************************


            'RICERCA SFITTI
            sValoreUnita = Request.QueryString("UI")
            '***********************************************


            Session.Add("ID", Me.txtIntegrazione.Value)

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

            Response.Write("<script>location.replace('ManutenzioniRRS.aspx?ED=" & sValoreEdificio _
                                                                        & "&CO=" & sValoreComplesso _
                                                                        & "&SV=" & sValoreVoce _
                                                                        & "&FO=" & sValoreFornitore _
                                                                        & "&AP=" & sValoreAppalto _
                                                                        & "&DAL=" & sValoreData_Dal _
                                                                        & "&AL=" & sValoreData_Al _
                                                                        & "&ST=" & sValoreStato _
                                                                        & "&REP=" & sValoreRepertorio _
                                                                        & "&ODL=" & sValoreODL _
                                                                        & "&ANNO=" & sValoreAnno _
                                                                        & "&UI=" & sValoreUnita _
                                                                        & "&IN_R=" & sValoreIndirizzoR _
                                                                        & "&SV_R=" & sValoreVoceR _
                                                                        & "&AP_R=" & sValoreAppaltoR _
                                                                        & "&ORD=" & sOrdinamento _
                                                                        & "&UBI=" & sValoreUbicazione _
                                                                        & "&STR=" & sValoreStruttura _
                                                                        & "&es=" & sValoreEsercizioF _
                                                                        & "&PROVENIENZA=" & sValoreProvenienza & "');</script>")



        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If
    End Sub
    Protected Sub StampaOrdine()
        Dim sUBICAZIONE As String = ""
        Dim sUBICAZIONE2 As String = ""
        Dim sModello As String = ""

        Dim sODL As String = ""
        Dim sODL_DATA As String = ""
        Dim sODL_EXTRA As String = ""


        '*******************APERURA CONNESSIONE*********************
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)
        End If

        '$ODL                   MANUTENZIONI.ODL ||'/'|| MANUTENZIONI.ANNO
        '$ODL_DATA              MANUTENZIONI.DATA_INIZIO_ORDINE   
        '$$ODL_EXTRA$$          MANUTENZIONI.ODL ||'/'|| MANUTENZIONI.ANNO ||' del '|| MANUTENZIONI.DATA_INIZIO_ORDINE (della manutenzione INTEGRATA)
        '                       oppure(ANNULLATO)

        '$REPERTORIO            APPALTI.NUM_REPERTORIO
        '$DATA_REPERTORIO       APPALTI.DATA_REPERTORIO
        '$DESCRIZIONE_APPALTO   APPALTI.DESCRIZIONE as ""APPALTO""

        '$FORNITORE             FORNITORI.RAGIONE_SOCIALE,FORNITORI.COGNOME,FORNITORI.NOME " _
        '$FORNITORI_INDIRIZZI$  FORNITORI_INDIRIZZI.TIPO FORNITORI_INDIRIZZI.INDIRIZZ FORNITORI_INDIRIZZI.CIVICO FORNITORI_INDIRIZZI.CAP  ||' - '|| FORNITORI_INDIRIZZI.COMUNE


        '$UBICAZIONE_INDIRIZZO  COMPLESSO o (EDICIIO e/0 SCALA) (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP) AS INDIRIZZO,
        '$UBICAZIONE_INDIRIZZO2 INDIRIZZI.CAP ||' - '|| INDIRIZZI.LOCALITA

        '$DESCRIZIONE_MANUTENZIONI$ MANUTENZIONI.DESCRIZIONE

        '$FRASE_MODELLO$        'frase + MANUTENZIONI.DATA_FINE_INTERVENTO


        Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloOrdineRRS2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
        Dim contenuto As String = sr1.ReadToEnd()
        sr1.Close()

        'par.cmd.CommandText = "select data_inizio_INTERVENTO from siscom_mi.manutenzioni where id=" & Request.QueryString("COD")
        'Dim data_inizio_ordine As String = par.IfNull(par.cmd.ExecuteScalar, "")
        Dim condizioneDataOrdine As String = ""
        'If data_inizio_ordine <> "" Then
        condizioneDataOrdine = " INIZIO_VALIDITA<='" & Format(Now, "yyyyMMdd") & "' AND FINE_VALIDITA>='" & Format(Now, "yyyyMMdd") & "' AND "
        'End If
        par.cmd.CommandText = "SELECT (CASE WHEN COGNOME IS NOT NULL THEN COGNOME||' 'ELSE NULL END )||" _
                & "(CASE WHEN NOME IS NOT NULL THEN NOME||'; ' ELSE NULL END)||" _
                & "(CASE WHEN (select max(contatto_telefonico) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1) IS NOT NULL THEN (select max(contatto_telefonico) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1)||';' ELSE NULL END)|| " _
                & "(CASE WHEN (select max(contatto_mail) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1) IS NOT NULL THEN (select max(contatto_mail) from siscom_mi.building_manager_operatori where " & condizioneDataOrdine & " building_manager_operatori.id_operatore=operatori.id and TIPO_OPERATORE=1) ELSE NULL END) " _
                & " FROM SEPA.OPERATORI WHERE ID IN (" _
                & " SELECT ID_OPERATORE FROM SISCOM_MI.BUILDING_MANAGER_OPERATORI WHERE " _
                & condizioneDataOrdine _
                & " TIPO_OPERATORE=1" _
            & " AND ID_BM IN (" _
            & " SELECT DISTINCT ID_BM FROM SISCOM_MI.EDIFICI WHERE ID IN (" _
            & " SELECT ID FROM SISCOM_MI.EDIFICI WHERE ID_COMPLESSO IN (SELECT ID_COMPLESSO FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_COMPLESSO IS NOT NULL AND ID_MANUTENZIONE=" & vIdManutenzione & ")" _
            & " UNION " _
            & " SELECT ID_EDIFICIO FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_EDIFICIO IS NOT NULL AND ID_MANUTENZIONE=" & vIdManutenzione & "" _
            & " UNION " _
            & " SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID IN (SELECT ID_UNITA_IMMOBILIARE FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_UNITA_IMMOBILIARE IS NOT NULL AND ID_MANUTENZIONE=" & vIdManutenzione & ")" _
            & " UNION" _
            & " SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_COMUNI WHERE ID IN (SELECT ID_UNITA_COMUNE FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_UNITA_COMUNE IS NOT NULL AND ID_MANUTENZIONE=" & vIdManutenzione & ")" _
            & " UNION " _
            & " SELECT ID_EDIFICIO FROM SISCOM_MI.SCALE_EDIFICI WHERE ID IN (SELECT ID_SCALA FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_SCALA IS NOT NULL AND ID_MANUTENZIONE=" & vIdManutenzione & ")" _
            & " UNION " _
            & " SELECT ID_EDIFICIO FROM SISCOM_MI.UNITA_IMMOBILIARI WHERE ID IN (SELECT IMPIANTI_UI.ID_UNITA FROM SISCOM_MI.IMPIANTI_UI WHERE ID_IMPIANTO IN (SELECT ID FROM SISCOM_MI.IMPIANTI WHERE COD_TIPOLOGIA<>'SO' AND ID IN (SELECT ID_IMPIANTO FROM SISCOM_MI.MANUTENZIONI_INTERVENTI WHERE ID_IMPIANTO IS NOT NULL AND ID_MANUTENZIONE=" & vIdManutenzione & ")))" _
            & ")))"
        Dim buildingManager As String = ""
        Dim lettore2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        While lettore2.Read
            buildingManager &= par.IfNull(lettore2(0), "") & "<br>"
        End While
        lettore2.Close()



        par.cmd.CommandText = "select MANUTENZIONI.ID,MANUTENZIONI.PROGR,MANUTENZIONI.ANNO,MANUTENZIONI.DESCRIZIONE," _
                                  & " MANUTENZIONI.ID_COMPLESSO,MANUTENZIONI.ID_EDIFICIO,MANUTENZIONI.ID_SCALA," _
                                  & " MANUTENZIONI.DATA_INIZIO_ORDINE,MANUTENZIONI.DATA_INIZIO_INTERVENTO,MANUTENZIONI.DATA_FINE_INTERVENTO," _
                                  & " MANUTENZIONI.STATO,MANUTENZIONI.ID_FIGLIO,SISCOM_MI.TAB_SERVIZI.DESCRIZIONE as ""TIPO_SERVIZIO"",MANUTENZIONI.DANNEGGIANTE,MANUTENZIONI.DANNEGGIATO," _
                                  & " SISCOM_MI.APPALTI.NUM_REPERTORIO,SISCOM_MI.APPALTI.DATA_REPERTORIO,SISCOM_MI.APPALTI.DESCRIZIONE as ""APPALTO"", " _
                                  & " FORNITORI.ID as ""ID_FORNITORE"",FORNITORI.RAGIONE_SOCIALE,FORNITORI.COGNOME,FORNITORI.NOME,FORNITORI.COD_FORNITORE, " _
                                  & " manutenzioni.operatore_autorizzazione " _
                           & " from SISCOM_MI.MANUTENZIONI, SISCOM_MI.APPALTI, SISCOM_MI.FORNITORI,SISCOM_MI.TAB_SERVIZI " _
                           & " where     MANUTENZIONI.ID=" & vIdManutenzione _
                                & " and  MANUTENZIONI.ID_APPALTO=APPALTI.ID (+) " _
                                & " AND (CASE WHEN MANUTENZIONI.ID_FORNITORE_STAMPA IS NOT NULL THEN MANUTENZIONI.ID_FORNITORE_STAMPA ELSE APPALTI.ID_FORNITORE END)=FORNITORI.ID " _
                                & " and  MANUTENZIONI.ID_SERVIZIO=SISCOM_MI.TAB_SERVIZI.ID (+)"


        Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReaderA.Read Then

            If par.IfNull(myReaderA("STATO"), 0) = 3 Then
                'MANUTENZIONE INTEGRATA
                Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select PROGR,ANNO,DATA_INIZIO_ORDINE from SISCOM_MI.MANUTENZIONI where ID=" & par.IfNull(myReaderA("ID_FIGLIO"), "-1")
                myReaderTMP = par.cmd.ExecuteReader()

                If myReaderTMP.Read Then

                    sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                    sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                    sODL_EXTRA = " INTEGRATO DELL'ODL " & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReaderTMP("DATA_INIZIO_ORDINE"), ""))
                End If
                myReaderTMP.Close()

            ElseIf par.IfNull(myReaderA("STATO"), 0) = 5 Then
                'ANNULLATO
                sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                sODL_EXTRA = "( ANNULLATO )"

            Else
                Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select ID,PROGR,ANNO,DATA_INIZIO_ORDINE from SISCOM_MI.MANUTENZIONI where ID_FIGLIO=" & par.IfNull(myReaderA("ID"), "-1")
                myReaderTMP = par.cmd.ExecuteReader()

                If myReaderTMP.Read Then
                    'MANUTENZIONE INTEGRAZIONE 

                    sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                    sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
                    sODL_EXTRA = " INTEGRAZIONE DELL'ODL " & par.IfNull(myReaderTMP("PROGR"), "") & "/" & par.IfNull(myReaderTMP("ANNO"), "") & " del " & par.FormattaData(par.IfNull(myReaderTMP("DATA_INIZIO_ORDINE"), ""))

                End If
                myReaderTMP.Close()

            End If

            If sODL = "" Then
                sODL = par.IfNull(myReaderA("PROGR"), "") & "/" & par.IfNull(myReaderA("ANNO"), "")
                sODL_DATA = par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_ORDINE"), ""))
            End If


            Dim firmaResponsabile As String = "<span class=""style13"">.................................................................................</span>"
            Dim operatoreAutorizzazione As Integer = par.IfNull(myReaderA("operatore_autorizzazione"), 0)
            par.cmd.CommandText = "SELECT FIRMA,COGNOME,NOME FROM SISCOM_MI.FIRME_OPERATORI,SEPA.OPERATORI WHERE OPERATORI.ID=ID_OPERATORE AND ID_OPERATORE=" & operatoreAutorizzazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                If IO.File.Exists(Server.MapPath("../../../ALLEGATI/FIRME/" & par.IfNull(lettore("FIRMA"), ""))) Then
                    firmaResponsabile = "<table><tr><td>" & par.IfNull(lettore("NOME"), "") & " " & par.IfNull(lettore("COGNOME"), "") & "</td></tr><tr><td><img alt="""" src=""" & par.IfNull(lettore("FIRMA"), "") & """ width=""200"" heigth=""80"" /></td></tr></table>"
                End If
            End If
            lettore.Close()
            contenuto = Replace(contenuto, "$firmaResponsabile$", firmaResponsabile)

            contenuto = Replace(contenuto, "$ODL$", sODL)

            contenuto = Replace(contenuto, "$ODL$", sODL)
            contenuto = Replace(contenuto, "$ODL_DATA$", sODL_DATA)
            contenuto = Replace(contenuto, "$ODL_EXTRA$", sODL_EXTRA)


            contenuto = Replace(contenuto, "$REPERTORIO$", par.IfNull(myReaderA("NUM_REPERTORIO"), ""))
            contenuto = Replace(contenuto, "$DATA_REPERTORIO$", par.FormattaData(par.IfNull(myReaderA("DATA_REPERTORIO"), "")))
            contenuto = Replace(contenuto, "$DESCRIZIONE_APPALTO$", par.IfNull(myReaderA("APPALTO"), ""))

            'DATI FORNITORE
            If par.IfNull(myReaderA("RAGIONE_SOCIALE"), "") = "" Then
                If par.IfNull(myReaderA("COD_FORNITORE"), "") = "" Then
                    contenuto = Replace(contenuto, "$FORNITORE$", par.IfNull(myReaderA("COGNOME"), "") & " " & par.IfNull(myReaderA("NOME"), ""))
                Else
                    contenuto = Replace(contenuto, "$FORNITORE$", par.IfNull(myReaderA("COD_FORNITORE"), "") & " - " & par.IfNull(myReaderA("COGNOME"), "") & " " & par.IfNull(myReaderA("NOME"), ""))
                End If
            Else
                If par.IfNull(myReaderA("COD_FORNITORE"), "") = "" Then
                    contenuto = Replace(contenuto, "$FORNITORE$", par.IfNull(myReaderA("RAGIONE_SOCIALE"), ""))
                Else
                    contenuto = Replace(contenuto, "$FORNITORE$", par.IfNull(myReaderA("COD_FORNITORE"), "") & " - " & par.IfNull(myReaderA("RAGIONE_SOCIALE"), ""))
                End If
            End If


            'INDIRIZZO FORNITORE
            Dim sIndirizzoFornitore1 As String = ""
            Dim sIndirizzoFornitore2 As String = ""

            par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                & " where ID_FORNITORE=" & par.IfNull(myReaderA("ID_FORNITORE"), 0)

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            myReaderT = par.cmd.ExecuteReader
            While myReaderT.Read

                sIndirizzoFornitore1 = par.IfNull(myReaderT("TIPO"), "") _
                               & " " & par.IfNull(myReaderT("INDIRIZZO"), "") _
                               & " " & par.IfNull(myReaderT("CIVICO"), "")

                sIndirizzoFornitore2 = par.IfNull(myReaderT("CAP"), "") _
                            & " - " & par.IfNull(myReaderT("COMUNE"), "")

            End While
            myReaderT.Close()

            contenuto = Replace(contenuto, "$FORNITORE_INDIRIZZO$", sIndirizzoFornitore1)
            contenuto = Replace(contenuto, "$FORNITORE_INDIRIZZO2$", sIndirizzoFornitore2)

            '**********************************************



            If par.IfNull(myReaderA("DANNEGGIANTE"), "") <> "" Then
                contenuto = Replace(contenuto, "$DANNEGGIANTE$", "Danneggiante " & par.IfNull(myReaderA("DANNEGGIANTE"), ""))
            Else
                contenuto = Replace(contenuto, "$DANNEGGIANTE$", "")
            End If

            If par.IfNull(myReaderA("DANNEGGIATO"), "") <> "" Then
                contenuto = Replace(contenuto, "$DANNEGGIATO$", "Danneggiato " & par.IfNull(myReaderA("DANNEGGIATO"), ""))
            Else
                contenuto = Replace(contenuto, "$DANNEGGIATO$", "")
            End If


            ' UBICAZIONE
            If par.IfNull(myReaderA("ID_COMPLESSO"), "-1") <> "-1" Then

                Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select * from SISCOM_MI.INDIRIZZI where ID=(select ID_INDIRIZZO_RIFERIMENTO from SISCOM_MI.COMPLESSI_IMMOBILIARI where ID=" & myReaderA("ID_COMPLESSO") & ")"
                myReaderTMP = par.cmd.ExecuteReader()
                If myReaderTMP.Read Then

                    sUBICAZIONE = "COMPLESSO in " & par.IfNull(myReaderTMP("DESCRIZIONE"), "") & " N. " & par.IfNull(myReaderTMP("CIVICO"), "")
                    sUBICAZIONE2 = par.IfNull(myReaderTMP("CAP"), "") & " - " & par.IfNull(myReaderTMP("LOCALITA"), "")
                End If
                myReaderTMP.Close()

            ElseIf par.IfNull(myReaderA("ID_EDIFICIO"), "-1") <> "-1" Then

                Dim myReaderTMP As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select * from SISCOM_MI.INDIRIZZI where ID=(select ID_INDIRIZZO_PRINCIPALE from SISCOM_MI.EDIFICI where ID=" & myReaderA("ID_EDIFICIO") & ")"
                myReaderTMP = par.cmd.ExecuteReader()
                If myReaderTMP.Read Then

                    sUBICAZIONE = "EDIFICIO in " & par.IfNull(myReaderTMP("DESCRIZIONE"), "") & " N. " & par.IfNull(myReaderTMP("CIVICO"), "")
                    sUBICAZIONE2 = par.IfNull(myReaderTMP("CAP"), "") & " - " & par.IfNull(myReaderTMP("LOCALITA"), "")
                End If
                myReaderTMP.Close()

                If par.IfNull(myReaderA("ID_SCALA"), "-1") <> "-1" Then

                    par.cmd.CommandText = "select DESCRIZIONE  from SISCOM_MI.SCALE_EDIFICI where ID= " & par.IfNull(myReaderA("ID_SCALA"), "-1")
                    myReaderTMP = par.cmd.ExecuteReader()

                    If myReaderTMP.Read Then
                        sUBICAZIONE = sUBICAZIONE & " SCALA=" & par.IfNull(myReaderTMP("DESCRIZIONE"), "")
                    End If
                    myReaderTMP.Close()
                End If
            End If
            '****************************


            contenuto = Replace(contenuto, "$UBICAZIONE_INDIRIZZO$", sUBICAZIONE)
            contenuto = Replace(contenuto, "$UBICAZIONE_INDIRIZZO2$", sUBICAZIONE2)

            contenuto = Replace(contenuto, "$DESCRIZIONE_MANUTENZIONI$", par.IfNull(myReaderA("DESCRIZIONE"), ""))


            Dim building As String = ""
            If buildingManager <> "" Then
                building = "Building Manager: " & buildingManager
            End If

            sModello = "Attenersi agli obblighi in materia di tutela della salute e della sicurezza nei luoghi di lavoro Titolo 4° DLgs 81/2008 E s.m.i. <br><br>" & vbCrLf _
                & "Programma attività: " _
                & "<br> " _
                & "-inizio il " & par.FormattaData(par.IfNull(myReaderA("DATA_INIZIO_INTERVENTO"), "")) _
                & "<br> " _
                & "-fine il " & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_INTERVENTO"), "")) _
                & "<br> " _
                & building

            '& " Intervento da eseguirsi entro il " & par.FormattaData(par.IfNull(myReaderA("DATA_FINE_INTERVENTO"), "")) & " oltre appl.art.11 C.S.p.1 "

            contenuto = Replace(contenuto, "$FRASE_MODELLO$", sModello)

            'contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(Format(Now, "yyyyMMdd")))
            contenuto = Replace(contenuto, "$data_stampa$", "")

        End If

        myReaderA.Close()

        Dim sSQL_DettaglioIMPIANTO As String
        sSQL_DettaglioIMPIANTO = "(CASE IMPIANTI.COD_TIPOLOGIA " _
                                    & " WHEN 'AN' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CF' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'CI' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'EL' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'GA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ID' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'ME' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'SO' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - '||" _
                                            & "(select  (CASE when SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO is null THEN " _
                                                            & " 'Num. Matr. '||SISCOM_MI.I_SOLLEVAMENTO.MATRICOLA " _
                                                    & " ELSE 'Num. Imp. '||SISCOM_MI.I_SOLLEVAMENTO.NUM_IMPIANTO END) " _
                                            & " from SISCOM_MI.I_SOLLEVAMENTO where ID=IMPIANTI.ID) " _
                                    & " WHEN 'TA' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TE' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TR' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TU' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                    & " WHEN 'TV' THEN SISCOM_MI.IMPIANTI.DESCRIZIONE||' - - Cod. '||SISCOM_MI.IMPIANTI.COD_IMPIANTO " _
                                & " ELSE '' " _
                                & " END) as DETTAGLIO "

        '*** MANUTENZIONI_INTERVENTI
        par.cmd.CommandText = " select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & sSQL_DettaglioIMPIANTO & ",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.IMPIANTI" _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO is not null and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO=SISCOM_MI.IMPIANTI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "       COMPLESSI_IMMOBILIARI.DENOMINAZIONE AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.COMPLESSI_IMMOBILIARI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='COMPLESSO' and  SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO=SISCOM_MI.COMPLESSI_IMMOBILIARI.ID (+) " _
             & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                  & "        (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Cod.'||SISCOM_MI.EDIFICI.COD_EDIFICIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                  & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='EDIFICIO' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID (+) " _
            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                  & "       (SISCOM_MI.EDIFICI.DENOMINAZIONE||' - -Scala '||(select descrizione from siscom_mi.scale_edifici where siscom_mi.scale_edifici.id=unita_immobiliari.id_scala)||' - -Interno '||SISCOM_MI.UNITA_IMMOBILIARI.INTERNO||' - '||SISCOM_MI.INTESTATARI_UI.INTESTATARIO) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                  & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INTESTATARI_UI " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                  & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA IMMOBILIARE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE=SISCOM_MI.UNITA_IMMOBILIARI.ID (+) and	SISCOM_MI.UNITA_IMMOBILIARI.ID_EDIFICIO=SISCOM_MI.EDIFICI.ID  (+)  and SISCOM_MI.UNITA_IMMOBILIARI.ID=SISCOM_MI.INTESTATARI_UI.ID_UI (+) " _
            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "       SISCOM_MI.EDIFICI.DENOMINAZIONE||' - Scala '||SCALE_EDIFICI.descrizione  AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
                   & " from  SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.EDIFICI, SISCOM_MI.SCALE_EDIFICI " _
                   & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione _
                   & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='SCALA' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA=SISCOM_MI.SCALE_EDIFICI.ID and SCALE_EDIFICI.ID_EDIFICIO=EDIFICI.ID " _
            & " union select SISCOM_MI.MANUTENZIONI_INTERVENTI.ID,SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_IMPIANTO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_COMPLESSO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_EDIFICIO, SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_IMMOBILIARE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE,SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_SCALA," _
                   & "      (SISCOM_MI.TIPO_UNITA_COMUNE.DESCRIZIONE||' - '||SISCOM_MI.UNITA_COMUNI.LOCALIZZAZIONE) AS ""DETTAGLIO"",TRIM(TO_CHAR(SISCOM_MI.MANUTENZIONI_INTERVENTI.IMPORTO_PRESUNTO,'9G999G999G999G999G990D99')) AS ""IMPORTO_PRESUNTO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO<>'RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_CONSUNTIVO""," _
                   & "      TRIM(TO_CHAR((select SUM(SISCOM_MI.MANUTENZIONI_CONSUNTIVI.PREZZO_TOTALE) from SISCOM_MI.MANUTENZIONI_CONSUNTIVI where SISCOM_MI.MANUTENZIONI_CONSUNTIVI.ID_MANUTENZIONI_INTERVENTI=SISCOM_MI.MANUTENZIONI_INTERVENTI.ID and SISCOM_MI.MANUTENZIONI_CONSUNTIVI.COD_ARTICOLO='RIMBORSO OPERE SPECIALISTICHE'),'9G999G999G999G999G990D99')) as ""IMPORTO_RIMBORSO"",SISCOM_MI.MANUTENZIONI_INTERVENTI.FL_BLOCCATO " _
             & " from SISCOM_MI.MANUTENZIONI_INTERVENTI, SISCOM_MI.UNITA_COMUNI, SISCOM_MI.TIPO_UNITA_COMUNE  " _
                  & " where SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_MANUTENZIONE =" & vIdManutenzione & " and   SISCOM_MI.MANUTENZIONI_INTERVENTI.TIPOLOGIA='UNITA COMUNE' and SISCOM_MI.MANUTENZIONI_INTERVENTI.ID_UNITA_COMUNE=SISCOM_MI.UNITA_COMUNI.ID (+) and SISCOM_MI.UNITA_COMUNI.COD_TIPOLOGIA=SISCOM_MI.TIPO_UNITA_COMUNE.COD  (+) "





        Dim TestoGrigliaM As String
        TestoGrigliaM = "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
        TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TIPOLOGIA </td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DETTAGLIO </td>" _
                                  & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                  & "</tr>"


        myReaderA = par.cmd.ExecuteReader()
        While myReaderA.Read

            TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderA("TIPOLOGIA"), "") & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderA("DETTAGLIO"), "") & "</td>" _
                                  & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "</tr>"


        End While
        myReaderA.Close()

        contenuto = Replace(contenuto, "$TITOLO_OGGETTO_MANUTENZIONI$", "OGGETTO INTERVENTI:")
        contenuto = Replace(contenuto, "$OGGETTO_MANUTENZIONI$", TestoGrigliaM)



        '*********************CHIUSURA CONNESSIONE**********************
        'par.OracleConn.Close()
        'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        'STAMPA PDF
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
        pdfConverter1.PdfFooterOptions.ShowPageNumber = False

        'Dim NomeFile1 As String = "Ordine_" & Request.QueryString("cod") & "_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        'pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & NomeFile1, Server.MapPath("..\..\..\NuoveImm\"))
        'Response.Write("<script>window.open('../../../FileTemp/" & NomeFile1 & "','stampa','');self.close();</script>")

        'Dim NomeFile1 As String = "Ordine_" & Request.QueryString("cod") & "_" & Format(Now, "yyyyMMddHHmmss") '& ".pdf"
        'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm", False, System.Text.Encoding.Default)
        'sr.WriteLine(contenuto)
        'sr.Close()

        'pdfConverter1.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".pdf")
        'If IO.File.Exists(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm") Then
        '    IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile1 & ".htm")
        'End If
        'Response.Write("<script>window.open('../../../FileTemp/" & NomeFile1 & ".pdf','stampa','');self.close();</script>")

        If IO.File.Exists(Server.MapPath("../../../NuoveImm/MM_113_84.png")) Then
            If Not Directory.Exists(Server.MapPath("../../../ALLEGATI/FIRME")) Then
                Directory.CreateDirectory(Server.MapPath("../../../ALLEGATI/FIRME"))
            End If
            If File.Exists(Server.MapPath("../../../ALLEGATI/FIRME/MM_113_84.png")) Then
                'IO.File.Delete(Server.MapPath("../../../ALLEGATI/FIRME/MM_113_84.png"))
            Else
                IO.File.Copy(Server.MapPath("../../../NuoveImm/MM_113_84.png"), Server.MapPath("../../../ALLEGATI/FIRME/MM_113_84.png"))

            End If
        End If

        Dim NomeFile1 As String = "Ordine_" & Request.QueryString("cod") & "_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
        NomeFile1 = par.NomeFileManut("MAN", Request.QueryString("cod")) & ".pdf"
        pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & NomeFile1, Server.MapPath("..\..\..\ALLEGATI\FIRME\"))
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "", "window.open('../../../FileTemp/" & NomeFile1 & "','stampa','');self.close();", True)
    End Sub
    Protected Sub ImgEmail_Click(sender As Object, e As System.EventArgs) Handles ImgEmail.Click
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            StampaOrdine()

            par.cmd.CommandText = "Insert into SISCOM_MI.EVENTI_MANUTENZIONE " _
                       & " (ID_MANUTENZIONE, ID_OPERATORE, DATA_ORA, COD_EVENTO, MOTIVAZIONE) " _
                       & " Values " _
                       & " (" & vIdManutenzione & ", " & Session.Item("ID_OPERATORE") & ", '" & Format(Now, "yyyyMMddHHmmss") & "', 'F246', 'Stampa e invio e-mail') "
            par.cmd.ExecuteNonQuery()


            par.myTrans.Commit() 'COMMIT
            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select * from SISCOM_MI.MANUTENZIONI  where SISCOM_MI.MANUTENZIONI.ID = " & vIdManutenzione & " FOR UPDATE NOWAIT"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1.Close()

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

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

            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Function ControlloCampiInterventi() As Boolean

        ControlloCampiInterventi = True
        'Controllo che la somma degli importi non superi quello RESIDUO TOTALE (APPALTI_LOTTI_SERVIZI.RESIDUO_CONSUMO)
        Dim SommaTot As Decimal = 0
        Dim SommaTot1 As Decimal = 0
        For Each elemento As GridDataItem In CType(Tab_Manu_Dettagli.FindControl("DataGrid1"), RadGrid).Items
            If IsNumeric(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) AndAlso CDec(CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text) > 0 Then
                SommaTot += CType(elemento.FindControl("TextBoxImportoPresunto"), TextBox).Text
            End If
        Next
        SommaTot = SommaTot - CDbl(par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("txtImportoODL"), HiddenField).Value, 0)) + CDbl(par.IfEmpty(CType(Tab_Manu_Dettagli.FindControl("txtImporto"), TextBox).Text, 0))
        SommaTot1 = CalcolaImportiControllo(SommaTot, par.IfEmpty(CType(Me.Page.FindControl("txtPercOneri"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtScontoConsumo"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtPercIVA_P"), HiddenField).Value, 0), par.IfEmpty(CType(Me.Page.FindControl("txtFL_RIT_LEGGE"), HiddenField).Value, 0))
        '*****************

        'SommaTot1                =[SOMMA DI TUTTO CIO' CHE HO PREVENTIVATO dalla GRIGLIA] 
        'Ricalcola_ImportoResiduo =[ (TOTALE DISPONIBILE x APPALTO - Tutto quello PREVENT e CONSUN e EMESSO SAL per APPALTO tranne la manutenzione in questione]
        'Esempio: 10000 totale;  4000 già impeganto da altri; 2000 mio (1000 già salvato 1000 messo ora) 
        '         quindi 2000 non deve superare (10000-4000)=6000

        If Math.Round(SommaTot1, 2) > Math.Round(Ricalcola_ImportoResiduo(), 2) Then
            'If SommaTot1 > CDbl(txtResiduoConsumo.Value) Then
            RadWindowManager1.RadAlert("L\'importo inserito è superiore all\'importo contrattuale residuo!", 300, 150, "Attenzione", "", "null")
            ControlloCampiInterventi = False
            Exit Function
        End If

    End Function

    Private Function Ricalcola_ImportoResiduo() As Decimal
        Dim FlagConnessione As Boolean
        Dim sStr1 As String

        Dim ris1, ris2 As Decimal

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Ricalcola_ImportoResiduo = 0

            'CALCOLO l'IMPORTO RESIDUO dato da:

            '1) la somma di eventuali variazioni all'importo residuo di APPALTI_VARIAZIONI_IMPORTI

            sStr1 = "select SUM(IMPORTO) " _
               & " from   SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI " _
               & " where  ID_PF_VOCE=" & CType(Page.FindControl("cmbVocePF"), RadComboBox).SelectedValue _
                 & " and  ID_VARIAZIONE in (select ID from SISCOM_MI.APPALTI_VARIAZIONI " _
                                       & " where ID_APPALTO=" & CType(Page.FindControl("txtIdAppalto"), HiddenField).Value & ")"

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader1.Read Then
                Ricalcola_ImportoResiduo = par.IfNull(myReader1(0), 0)
            End If
            myReader1.Close()


            '2)la somma dell'importo calcolato (IMPORTO-CONSUMO+IVA) 

            sStr1 = "select * from SISCOM_MI.APPALTI_VOCI_PF " _
                 & " where ID_APPALTO=" & CType(Page.FindControl("txtIdAppalto"), HiddenField).Value
            '& "   and ID_PF_VOCE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=(select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID=" & CType(Page.FindControl("cmbVocePF"), DropDownList).SelectedValue & ")) "

            '& "   and ID_PF_VOCE=" & Me.cmbVocePF.SelectedValue 'Prima cercavo solo la VOCE


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader()

            While myReader1.Read
                'IMPORTO a CONSUMO senza IVA=IMPORTO_CONSUMO-(IMPORTO_COSUMO*SCONTO_CONSUMO/100)
                ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) * par.IfNull(myReader1("SCONTO_CONSUMO"), 0) / 100
                ris1 = par.IfNull(myReader1("IMPORTO_CONSUMO"), 0) - ris1

                ris1 = ris1 + par.IfNull(myReader1("ONERI_SICUREZZA_CONSUMO"), 0) 'par.IfEmpty(Me.txtOneriSicurezza.Value, 0)

                If par.IfNull(myReader1("IVA_CONSUMO"), 0) > 0 Then
                    ris2 = ris1 * par.IfNull(myReader1("IVA_CONSUMO"), 0) / 100
                Else
                    ris2 = 0
                End If
                Ricalcola_ImportoResiduo = Ricalcola_ImportoResiduo + ris1 + ris2
            End While
            myReader1.Close()


            '3)la SommaResiduo va sottratto alla somma dell'IMPORTO PRENOTATO o CONSUNTIVATO o EMESSO PAGAMENTO (SAL) da MANUTENZIONI 
            sStr1 = "select SUM(IMPORTO_TOT) from SISCOM_MI.MANUTENZIONI " _
                 & " where ID_APPALTO=" & CType(Page.FindControl("txtIdAppalto"), HiddenField).Value _
                 & "   and ID_PF_VOCE_IMPORTO is null " _
                 & "   and STATO in (1,2,4)"

            'TUTTO CIO' SPESO tranne quelle speso dalla manutenzioni in modifica
            If vIdManutenzione > 0 Then
                sStr1 = sStr1 & " and ID<>" & vIdManutenzione
            End If

            'in data 2/11/2011 non è più filtrato per Voce Business Plan
            '& "   and ID_SERVIZIO=" & Me.cmbServizio.SelectedValue 


            par.cmd.Parameters.Clear()
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then
                Ricalcola_ImportoResiduo = Ricalcola_ImportoResiduo - par.IfNull(myReader1(0), 0) '+ par.IfEmpty(Me.txtOneriSicurezza.Value, 0)
            End If
            myReader1.Close()


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            Ricalcola_ImportoResiduo = 0

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Function
    Function CalcolaImportiControllo1(ByVal importo As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Decimal, ByVal oneri As Decimal) As Decimal

        Dim asta, iva, ritenuta, risultato1, risultato2, risultato3, ritenutaIVATA As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva


        oneri = importo - ((importo * 100) / (100 + perc_oneri))
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
            'ritenutaIVATA = ritenuta + Math.Round(((ritenuta * perc_iva) / 100), 4)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri


        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato3 * perc_iva) / 100, 2)
        End If

        'I) NETTO+ONERI+IVA
        CalcolaImportiControllo1 = risultato3 + iva + ritenutaIVATA


    End Function
    Function CalcolaImportiControllo(ByVal importo As Decimal, ByVal perc_oneri As Decimal, ByVal perc_sconto As Decimal, ByVal perc_iva As Decimal, ByVal fl_rit_legge As Integer) As Decimal

        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, ritenutaIVATA As Decimal

        'A) Importo
        'V) perc_oneri
        'Y) perc_sconto
        'Z) perc_iva


        'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
        oneri = par.IfEmpty(CType(Me.Page.FindControl("Tab_Manu_Riepilogo").FindControl("txtOneriP_MANO"), HiddenField).Value, 0)

        If oneri = -1 Then
            'B) A-(A*100)/(100+V) ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
            oneri = importo - ((importo * 100) / (100 + perc_oneri))
        End If
        oneri = Round(oneri, 2)

        'C) A-B LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri

        'D) RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100
        asta = Round(asta, 2)

        'E) C-D NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta

        'AGGIUNTO
        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato2 + oneri

        'F) ALIQUOTA 0,5% sul NETTO senza ONERI ora in data 12/05/2011 la ritenuta va calcolato con gli ONERI
        If fl_rit_legge = 1 Then
            ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
            ritenuta = Round(ritenuta, 2)
            ritenutaIVATA = Round((ritenuta + ((ritenuta * perc_iva) / 100)), 2)
            ritenutaIVATA = Round(ritenutaIVATA, 2)
        Else
            ritenuta = 0
            ritenutaIVATA = 0
        End If

        'G) E-F+B  NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
        risultato3 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri

        'H) (G*Z)/100 IVA= (NETTO con oneri*perc_iva)/100
        If perc_iva < 0 Then
            iva = 0
        Else
            iva = Math.Round((risultato3 * perc_iva) / 100, 2)
        End If
        iva = Round(iva, 2)

        'I) NETTO+ONERI+IVA
        CalcolaImportiControllo = risultato3 + iva + ritenutaIVATA


    End Function

    Public Property sAggiornaSEFO() As String
        Get
            If Not (ViewState("par_sAggiornaSEFO") Is Nothing) Then
                Return CStr(ViewState("par_sAggiornaSEFO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sAggiornaSEFO") = value
        End Set
    End Property


    'Protected Sub ImgEventi_Click(sender As Object, e As System.EventArgs) Handles ImgEventi.Click
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('Report/Eventi.aspx?ID_MANUTENZIONE=" & vIdManutenzione & "','Report', '');", True)
    'End Sub



    'Protected Sub ImgAllegaFile_Click(sender As Object, e As System.EventArgs) Handles ImgAllegaFile.Click
    '    ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../InvioAllegato.aspx?T=6&ID=" & vIdManutenzione & "', 'Allegati', '');", True)
    'End Sub


    Private Sub NascondiTab()
        RadTabStrip.Tabs.FindTabByValue("MotivazioniAnnullamento").Visible = False
        Dim i As Integer = 0
        For Each item As RadTab In RadTabStrip.Tabs
            If item.Visible = True Then
                RadMultiPage1.SelectedIndex = i
                RadTabStrip.SelectedIndex = i
                Exit For
            End If
            i += 1
        Next
    End Sub

    Private Sub SettaPulsanti()
        Try
            If txtVisualizza.Value = "0" Then
                'BOZZA
                ' ImgAllegaFile.Enabled = False
                ImgAllegati.Enabled = False
            End If
            If txtVisualizza.Value = "1" Then
                'SEMPRE
                ' ImgAllegaFile.Enabled = True
                ImgAllegati.Enabled = True
            End If
            If txtVisualizza.Value = "2" Then
                'SOLO LETTURA, CONSUNTIVATO, LIQUIDATO, ANNULLATO
                ' ImgAllegaFile.Enabled = False
                ImgAllegati.Enabled = True
            End If
            If txtSTATO.Value = "0" Then
                'BOZZA
                ImgStampa.Enabled = False
                ImgEmail.Enabled = False
            End If
            If txtSTATO.Value = "1" Then
                'EMESSO
                ImgStampa.Enabled = True
                ImgEmail.Enabled = True

            End If
            If txtSTATO.Value = "2" Then
                'CONSUNTIVO
                ImgStampa.Enabled = True
                ImgEmail.Enabled = True

            End If
            If txtSTATO.Value = "3" Then
                'INTEGRATO
                ImgStampa.Enabled = True
                ImgEmail.Enabled = True
            End If
            If txtSTATO.Value = "4" Then
                'EMESSO PAGAMENTO (ex LIQUIDATO)
                ImgStampa.Enabled = True
                ImgEmail.Enabled = True
            End If
            If txtSTATO.Value = "5" Then
                'ANNULLATO
                ImgStampa.Enabled = False
                ImgEmail.Enabled = False
            End If

            If BLOCCATO.Value = "1" Then
                ImgStampa.Enabled = False
                ImgEmail.Enabled = False
            End If
            If txtTIPO.Value = "0" Then
                CType(Tab_Manu_Riepilogo.FindControl("btnAggAppalto"), ImageButton).Visible = False
                ' btn_VisUnita.Enabled = False
            End If
            If txtTIPO.Value = "1" Then
                CType(Tab_Manu_Riepilogo.FindControl("btnAggAppalto"), ImageButton).Visible = False
                'btn_VisUnita.Enabled = True
            End If
            If ANNULLAVISIBILE.Value = "1" Then
                btnAnnulla.Enabled = False
            End If
            If autorizzazione.Value = "0" Then
                btnElimina.Enabled = False
                '  ImgAllegaFile.Enabled = False
                ImgAllegati.Enabled = False
                ImgStampa.Enabled = False
                ImgEmail.Enabled = False
            End If
        Catch ex As Exception
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
End Class
