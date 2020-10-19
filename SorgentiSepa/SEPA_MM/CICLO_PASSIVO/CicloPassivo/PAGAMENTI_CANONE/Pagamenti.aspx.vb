Imports System.Collections
Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Math
Imports Telerik.Web.UI

Partial Class PAGAMENTI_CANONE_Pagamenti
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public sValoreEsercizioFinanziarioR As String

    Public sValorestruttura As String
    Public sValoreTipo As String    'RICERCA_DIRETTA, RICERCA_SELETTIVA, DA_APPROVARE, SCADENZA

    Public sValoreFornitore As String
    Public sValoreAppalto As String

    Public sValoreDataP_Dal As String
    Public sValoreDataP_Al As String

    Public sValoreDataS_Dal As String
    Public sValoreDataS_Al As String

    Public sValoreODL As String
    Public sValoreAnno As String

    Public sOrdinamento As String

    Public sValoreProvenienza As String

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""

    Public TabberHide As String = ""


    Public importoAPR, oneriAPR, risultato1APR, astaAPR, risultato2APR, ritenutaAPR, risultato3APR, ivaAPR, risultato4APR, risultatoImponibileAPR As Decimal
    Public importoPRE, oneriPRE, risultato1PRE, astaPRE, risultato2PRE, ritenutaPRE, ritenutaNoIvaT, risultato3PRE, ivaPRE, risultato4PRE, risultatoImponibilePRE As Decimal

    Public importoAPR_P, oneriAPR_P, risultato1APR_P, astaAPR_P, risultato2APR_P, ritenutaAPR_P, risultato3APR_P, ivaAPR_P, risultato4APR_P, risultatoImponibileAPR_P As Decimal
    Public importoPRE_P, oneriPRE_P, risultato1PRE_P, astaPRE_P, risultato2PRE_P, ritenutaPRE_P, risultato3PRE_P, ivaPRE_P, risultato4PRE_P, risultatoImponibilePRE_P As Decimal

    Public SommaPenale As Decimal

    Public lstListaRapporti As System.Collections.Generic.List(Of Epifani.ListaGenerale)


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Try
            HFGriglia.Value = CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).ClientID
            HFTAB.Value = "tab1,tab2,tab3,tab4"
            HFAltezzaTab.Value = 410
            HFAltezzaFGriglie.Value = "480"
            Response.Expires = 0

            ' Dim Str As String

            'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            'Str = Str & "<" & "/div>"

            'Response.Write(Str)

            lstListaRapporti = CType(HttpContext.Current.Session.Item("LSTLISTAGENERALE1"), System.Collections.Generic.List(Of Epifani.ListaGenerale))


                sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

                sValorestruttura = Request.QueryString("ST")
                sValoreFornitore = Request.QueryString("FO")
                sValoreAppalto = Request.QueryString("AP")


                'DA RICERCE PER DATE
                sValoreDataS_Dal = Request.QueryString("DALS")
                sValoreDataS_Al = Request.QueryString("ALS")

                sValoreDataP_Dal = Request.QueryString("DALP")
                sValoreDataP_Al = Request.QueryString("ALP")

                sValoreTipo = Request.QueryString("TIPO")  'della query di ricerca  ''APPROVATI,APPROVATI_SCADENZA,DA_STAMPARE_PAG
                sOrdinamento = Request.QueryString("ORD")

                sValoreProvenienza = Request.QueryString("PROVENIENZA")


                'CAMPI CHIAVE
                Me.txtID_APPALTO.Value = Request.QueryString("ID_A")
                Me.txtID_FORNITORE.Value = Request.QueryString("ID_F")
                'Me.txtDataScadenza.Text = par.FormattaData(par.IfNull(Request.QueryString("DATA"), ""))
                '**********************************

            If Not IsPostBack Then
                ' Response.Flush()
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("Ordine")

                txtDataDel.SelectedDate = Format(Now, "dd/MM/yyyy")
                'DA RICERCA SELETTIVA


                ' CONNESSIONE DB
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'CType(TabDettagli1.FindControl("txtConnessione"), TextBox).Text = CStr(lIdConnessione)
                Me.txtConnessione.Text = CStr(lIdConnessione)

                If par.OracleConn.State = Data.ConnectionState.Open Then
                    RadWindowManager1.RadAlert("Impossibile visualizzare!", 300, 150, "Attenzione", "", "null")

                    Exit Sub
                Else
                    par.OracleConn.Open()
                    par.SettaCommand(par)
                    HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)
                    ' HttpContext.Current.Session.Add("SESSION_MANUTENZIONI", par.OracleConn)
                End If


                Me.HLink_Fornitore.Text = ""
                Me.HLink_Appalto.Text = ""

                Me.txtVisualizza.Value = 0         'SEMPRE o SOLO LETTURA
                Me.txtSTATO.Value = 0              '0=DA APPROVARE 1=APPROVATO 2=STAMPATO SAL 3=STAMPATO PAGAMENTO
                Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value
                Me.txtStatoPagamento.Value = 0      'PAGAMENTO NON STAMPATO

                vIdPrenotazioni = ""

                vIdPagamento = 0

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    vIdPagamento = Request.QueryString("ID")

                Else
                    vIdPagamento = Session.Item("ID")
                End If
                Me.txtID_PAGAMENTI.Value = vIdPagamento
                HiddenIDPagamento.Value = vIdPagamento


                'CaricaStati()
                'Setta_StatoPagamento()
                SettaggioCampi()    'APPALTO e FORNITORE
                caricaImportoResiduoDaTrattenere()


                If vIdPagamento <> 0 Then
                    'VISUALIZZAZIONE IL PAGAMENTO CON TUTTE LE PRENOTAZIONI

                    lstListaRapporti.Clear()
                    'Nella Visualizzazione, prendo la struttura presente in una delle prenotazioni del pagamento selezionato
                    VisualizzaDati()

                    TabberHide = "tabbertabhide"
                    NascondiTab()
                    Tabber1 = "tabbertabdefault"
                    txtindietro.Text = 0
                    CType(Tab_SAL_Riepilogo.FindControl("lblImportoApprovazione"), Label).Text = "IMPORTO APPROVATO"
                    CType(Tab_SAL_RiepilogoProg.FindControl("lblImportoApprovazione"), Label).Text = "IMPORTO APPROVATO"
                Else

                    Dim gen As Epifani.ListaGenerale

                    For Each gen In lstListaRapporti
                        If vIdPrenotazioni <> "" Then
                            vIdPrenotazioni = vIdPrenotazioni & "," & gen.STR
                        Else
                            vIdPrenotazioni = gen.STR
                        End If
                    Next

                    'Nell'inserimento, prendo la struttura dell'Operatore
                    Me.txtID_STRUTTURA.Value = Session.Item("ID_STRUTTURA")

                    SettaValori()

                    TabberHide = "tabbertabhide"
                    NascondiTab()
                    Tabber1 = "tabbertabdefault"
                    txtindietro.Text = 0 '1
                End If


                'PERCENTUALE ONERI DI SICUREZZA
                par.cmd.CommandText = " SELECT MAX(ROUND(ONERI_SICUREZZA_CANONE/IMPORTO_CANONE*100,4)) FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    & " WHERE IMPORTO_CANONE>0 AND ID_APPALTO=" & txtID_APPALTO.Value & " AND ID_PF_VOCE_IMPORTO IN (SELECT ID_VOCE_PF_IMPORTO FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & vIdPrenotazioni & ")) "
                Dim percentualeOneriSicurezza As Decimal = -1
                percentualeOneriSicurezza = par.IfNull(par.cmd.ExecuteScalar, -1)
                If IsNumeric(percentualeOneriSicurezza) AndAlso percentualeOneriSicurezza <> -1 Then
                    CType(Me.Page.FindControl("Tab_SAL_Riepilogo").FindControl("lblOneri"), Label).Text = "Oneri di Sicurezza (" & percentualeOneriSicurezza & " %)"
                    CType(Me.Page.FindControl("Tab_SAL_Riepilogo").FindControl("lblOneriC"), Label).Text = "Oneri di Sicurezza (" & percentualeOneriSicurezza & " %)"
                End If
 'VERIFICA ABILITAZIONE PER LA RIELABORAZIONE DEL CDP E DEL SAL
                If par.IfNull(Session.Item("BP_PC_RIELABORA_CDP"), "0") = "0" Then
                    HiddenFieldMostraRielPag.Value = "0"
                Else
                    HiddenFieldMostraRielPag.Value = "1"
                End If
                If par.IfNull(Session.Item("BP_PC_RIELABORA_SAL"), "0") = "0" Then
                    HiddenFieldMostraRielSAL.Value = "0"
                Else
                    HiddenFieldMostraRielSAL.Value = "1"
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
                    ElseIf TypeOf CTRL Is CheckBox Then
                        DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                'Me.cmbStato.Attributes.Add("onChange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';CambiaStato();")
                'Me.cmbStato.Attributes.Add("onChange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                Me.cmbStatoSAL.Attributes.Add("onChange", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")


                Me.txtDataSal.Attributes.Add("onkeypress", "javascript:document.getElementById('USCITA').value='1';")
                Me.txtDataDel.Attributes.Add("onkeypress", "javascript:document.getElementById('USCITA').value='1';")
                Me.txtScadenza.Attributes.Add("onkeypress", "javascript:document.getElementById('USCITA').value='1';")

                Dim rad As Object = CType(Tab_SAL_Dettagli.FindControl("RadWindowServizi"), RadWindow).Controls.Item(0)


                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Attributes.Add("onkeypress", "javascript:$onkeydown(event);")

                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Attributes.Add("onkeypress", "javascript:$onkeydown(event);")




                CType(Tab_SAL_Riepilogo.FindControl("HLink_ElencoMandati"), HyperLink).Attributes.Add("onClick", "javascript:window.open('../PAGAMENTI/Tab_ElencoMandati.aspx?ID_PAGAMENTO=" & vIdPagamento & "','Elenco','height=600,width=800');")


                If Session.Item("BP_PC_L") = "1" Or IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    Me.txtVisualizza.Value = 1 'SOLO LETTURA
                    FrmSolaLettura()
                End If

                ' SE l'operatore è BP_GENERALE=1 (può vedere tutte le strutture) perrò la sua struttura + diversa da quella selezionata allora la maschera è in SOLO LETTURA
                If Session.Item("BP_GENERALE") = "1" And par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) <> Session.Item("ID_STRUTTURA") Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    Me.txtVisualizza.Value = 1 'SOLO LETTURA
                    FrmSolaLettura()
                End If


                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                    Me.txtVisualizza.Value = 2 'SOLO LETTURA CHIAMATA DIRETTA

                    FrmSolaLettura()
                    Me.btnINDIETRO.Visible = False

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
                    Me.txtConnessione.Text = "-1"

                End If

                'Verifica che il contratto legato al pagamento abbia o meno la spunta sull'anticipo contrattuale
                Dim test As String = vIdPagamento
                par.cmd.CommandText = "select APPALTI.FL_ANTICIPO  from siscom_mi.appalti where id = " & txtID_APPALTO.Value
                Dim anticipo As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If anticipo = 0 Then
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Enabled = False
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Enabled = False
                    CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto"), TextBox).Enabled = False
                    CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Enabled = False
                Else
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Enabled = True
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Enabled = True
                    CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto"), TextBox).Enabled = True
                    CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Enabled = True

                End If

            End If
            idSAL.Value = vIdPagamento
        Catch ex As Exception

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

        'ANNULLO NON POSSIBILE QUANDO ESERCIZIO è IN STATO 6
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
                & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
                & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.FL_CC=0"
            Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lett.Read Then
                'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                If par.IfNull(Lett(0), 0) = 6 Then
                    'btnAnnulla.Visible = False
                    ANNULLO.Value = "1"
                End If
            End If
            Lett.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Else

            par.cmd.CommandText = "SELECT PF_MAIN.ID_STATO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN " _
               & "WHERE PRENOTAZIONI.ID_VOCE_PF = PF_VOCI.ID AND PF_MAIN.ID=PF_VOCI.ID_PIANO_FINANZIARIO " _
               & "AND PAGAMENTI.ID IN (" & vIdPagamento & ") AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO AND PF_VOCI.FL_CC=0"
            Dim Lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lett.Read Then
                'If par.IfNull(Lett(0), 0) = 6 And Session.Item("FL_COMI") <> 1 Then
                If par.IfNull(Lett(0), 0) = 6 Then
                    'btnAnnulla.Visible = False
                    ANNULLO.Value = "1"
                End If
            End If
            Lett.Close()

        End If

    End Sub


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""

        If ChkRipartizioni.Checked = True Then
            TabberHide = "tabbertab"
        Else
            TabberHide = "tabbertabhide"
            NascondiTab()
        End If

        Select Case txttab.Text
            Case "1"
                Tabber1 = "tabbertabdefault"
            Case "2"
                Tabber2 = "tabbertabdefault"
            Case "3"
                Tabber3 = "tabbertabdefault"
            Case "4"
                Tabber4 = "tabbertabdefault"
        End Select
        ControlloPulsanti()
        'TabberHide = "tabbertab"
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Blocca_SbloccaMenu(1);", True)
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

    Public Property vIdPrenotazioni() As String
        Get
            If Not (ViewState("par_idPrenotazioni") Is Nothing) Then
                Return CStr(ViewState("par_idPrenotazioni"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_idPrenotazioni") = value
        End Set

    End Property

    Public Property vIdPagamento() As Long
        Get
            If Not (ViewState("par_idPagamento") Is Nothing) Then
                Return CLng(ViewState("par_idPagamento"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idPagamento") = value
        End Set

    End Property
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
                    par.cmd.CommandText = "select " & Column & " from siscom_Mi." & Table & " where id = " & vIdPagamento

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

        Dim SommaValoreLordo As Decimal = 0
        Dim SommaValoreAssestatoLordo As Decimal = 0
        Dim SommaAssesato As Decimal = 0

        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0

        Dim SommaPenale As Decimal = 0

        Dim Somma1 As Decimal = 0
        Dim sRisultato As String = ""

        Dim gen As Epifani.ListaGenerale

        Try


            'DATI PAGAMENTO
            Select Case par.IfNull(myReader1("ID_STATO"), "0")
                Case "0"
                    Me.txtSTATO.Value = 1 'APPROVATO
                Case "1"

                    If par.IfNull(myReader1("DATA_STAMPA"), "") <> "" Then
                        Me.txtStatoPagamento.Value = 1 'PAGAMENTO APPROVATO e STAMPATO
                        Me.txtSTATO.Value = 3 'STAMPATO il PAGAMENTO

                        Me.txtDescrizioneP.ReadOnly = True
                        Me.txtDataSal.Enabled = True
                        Me.txtDataDel.Enabled = True
                        Me.cmbContoCorrente.Enabled = False
                    Else
                        Me.txtStatoPagamento.Value = 0 'PAGAMENTO APPROVATO da STAMPARE
                        Me.txtSTATO.Value = 2 'STAMPATO il SAL
                    End If

                Case "5"
                    'LIQUIDATO
                    Me.txtStatoPagamento.Value = 1 'PAGAMENTO APPROVATO e STAMPATO
                    Me.txtSTATO.Value = 3 'STAMPATO il PAGAMENTO

                    'CType(Tab_SAL_Riepilogo.FindControl("txtDataMandato"), TextBox).Text = par.FormattaData(par.IfNull(myReader1("DATA_MANDATO"), ""))
                    'CType(Tab_SAL_Riepilogo.FindControl("txtNumMandato"), TextBox).Text = par.IfNull(myReader1("NUMERO_MANDATO"), "")

            End Select

            Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value


            Me.lblPROG_Pagamento.Text = par.IfNull(myReader1("PROGR"), "") & "/" & par.IfNull(myReader1("ANNO"), "")

            Me.txtPAGAMENTI_PROGR_APPALTO.Value = par.IfNull(myReader1("PROGR_APPALTO"), "")

            Me.lblSAL.Text = "SeTATO SAL (" & par.IfNull(myReader1("PROGR_APPALTO"), "") & ")"
            If Not String.IsNullOrEmpty(par.FormattaData(par.IfNull(myReader1("DATA_SAL"), ""))) Then
                Me.txtDataSal.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_SAL"), ""))
            End If
            If Not String.IsNullOrEmpty(par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), ""))) Then
                Me.txtDataDel.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), ""))
            End If

            Dim modalita As String = "null"
            Dim condizione As String = "null"
            par.cmd.CommandText = "select  ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO from siscom_mi.appalti where id = " & txtID_APPALTO.Value
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                modalita = par.IfNull(reader("ID_TIPO_MODALITA_PAG"), "null")
                condizione = par.IfNull(reader("ID_TIPO_PAGAMENTO"), "null")
            End If
            reader.Close()
            If Not String.IsNullOrEmpty(par.FormattaData(CalcolaDataScadenza(modalita, condizione, par.IfNull(myReader1("data_scadenza"), "")))) Then
                Me.txtScadenza.SelectedDate = par.FormattaData(CalcolaDataScadenza(modalita, condizione, par.IfNull(myReader1("data_scadenza"), "")))
            End If

            Me.cmbStatoSAL.SelectedValue = par.IfNull(myReader1("STATO_FIRMA"), "0")

            Me.txtDescrizioneP.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
            Me.cmbContoCorrente.SelectedValue = par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01")

            '    'Me.lblPROG_Prenotazione.Text = par.IfNull(myReader1("PROGR_FORNITORE"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
            '    'Me.lblDataPrenotazione.Text = par.FormattaData(par.IfNull(myReader1("DATA_PRENOTAZIONE"), ""))

            '    vIdPagamento = par.IfNull(myReader1("ID_PAGAMENTO"), 0)
            '    Me.txtID_PAGAMENTI.Value = vIdPagamento

            '    Me.txtSTATO.Value = par.IfNull(myReader1("ID_STATO"), "0") '0=DA APPROVARE, 1=APPROVATO

            '*****************************



            'ESTRAGGO TUTTE LE PRENOTAZIONI del PAGAMENTO
            vIdPrenotazioni = ""

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select ID,ID_STRUTTURA from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & vIdPagamento
            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader2.Read

                gen = New Epifani.ListaGenerale(lstListaRapporti.Count, par.IfNull(myReader2("ID"), -1))
                lstListaRapporti.Add(gen)
                gen = Nothing

                'Nella Visualizzazione, prendo la struttura presente in una delle prenotazioni del pagamento selezionato
                Me.txtID_STRUTTURA.Value = par.IfNull(myReader2("ID_STRUTTURA"), -1)
            End While
            myReader2.Close()


            For Each gen In lstListaRapporti
                If vIdPrenotazioni <> "" Then
                    vIdPrenotazioni = vIdPrenotazioni & "," & gen.STR
                Else
                    vIdPrenotazioni = gen.STR
                End If
            Next
            '***************************************

            'IMPORTO PAGATO
            Dim SommaPagato As Decimal = 0
            par.cmd.CommandText = " select  sum(nvl(PAGAMENTI_LIQUIDATI.IMPORTO,0) ) as IMPORTO " _
                                           & " from  SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                           & " where ID_PAGAMENTO=" & vIdPagamento

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                SommaPagato = par.IfNull(myReader2(0), 0)
            End If
            myReader2.Close()

            If SommaPagato = 0 Then
                CType(Tab_SAL_Riepilogo.FindControl("cmb_Liquidazione"), DropDownList).SelectedValue = 0

            ElseIf SommaPagato = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) Then
                CType(Tab_SAL_Riepilogo.FindControl("cmb_Liquidazione"), DropDownList).SelectedValue = 3
            Else
                CType(Tab_SAL_Riepilogo.FindControl("cmb_Liquidazione"), DropDownList).SelectedValue = 2
            End If
            '*****************************************************


            SettaValori()

            'FlagConnessione = False
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            '    FlagConnessione = True
            'End If


            'Dim sFiliale As String = ""
            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            '    sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            'End If


            'SOMMA VALORE_CANONE
            'par.cmd.CommandText = " select  to_char(SUM(VALORE_CANONE)) from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                     & " where ID in (select distinct(ID_VOCE_PF_IMPORTO) from SISCOM_MI.PRENOTAZIONI " _
            '                                  & " where ID in (" & vIdPrenotazioni & "))"

            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaValoreLordo = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*************************

            'SOMMA VALORE_CANONE_ASS
            'par.cmd.CommandText = " select  to_char(SUM(VALORE_CANONE_ASS)) from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                    & " where ID in (select distinct(ID_VOCE_PF_IMPORTO) from SISCOM_MI.PRENOTAZIONI " _
            '                                  & " where ID in (" & vIdPrenotazioni & "))"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()

            'SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo
            '***************************


            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
            'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
            '                    & " from    SISCOM_MI.PRENOTAZIONI " _
            '                    & " where   ID_STATO=0 " _
            '                    & "   and   ID_PAGAMENTO is null " _
            '                    & "   and   TIPO_PAGAMENTO=6" & sFiliale _
            '                    & "   and   ID_VOCE_PF in ( select distinct(ID_VOCE_PF) from SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where ID in (" & vIdPrenotazioni & "))"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaPrenotato = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '**********************************


            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
            '                    & " from    SISCOM_MI.PRENOTAZIONI " _
            '                    & " where   ID_STATO=1 " _
            '                    & "   and   ID_PAGAMENTO is null " _
            '                    & "   and   TIPO_PAGAMENTO=6" & sFiliale _
            '                    & "   and   ID_VOCE_PF in ( select distinct(ID_VOCE_PF) from SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where ID in (" & vIdPrenotazioni & "))"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*********************************


            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            'par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
            '                    & " from    SISCOM_MI.PRENOTAZIONI " _
            '                    & " where   ID_STATO=2 " _
            '                    & "   and   ID_PAGAMENTO is not null " _
            '                    & "   and   TIPO_PAGAMENTO=6" & sFiliale _
            '                    & "   and   ID_VOCE_PF in ( select distinct(ID_VOCE_PF) from SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where ID in (" & vIdPrenotazioni & "))"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*********************************


            'IMPORTO CONSUNTIVATO e LIQUIDATO
            'par.cmd.CommandText = "select  * from SISCOM_MI.PAGAMENTI " _
            '                   & "  where  PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF in ( select distinct(ID_VOCE_PF) from SISCOM_MI.PRENOTAZIONI " _
            '                                                                   & " where ID in (" & vIdPrenotazioni & "))" _
            '                                            & "   and  TIPO_PAGAMENTO=6" & sFiliale _
            '                                            & "   and  ID_STATO>1 )"

            'myReader2 = par.cmd.ExecuteReader
            'While myReader2.Read
            '    Select Case par.IfNull(myReader2("ID_STATO"), 0)
            '        Case 1
            '            sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
            '            Somma1 = Decimal.Parse(sRisultato)
            '            SommaConsuntivato = SommaConsuntivato + Somma1 'par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
            '        Case 5
            '            sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
            '            Somma1 = Decimal.Parse(sRisultato)
            '            SommaLiquidato = SommaLiquidato + Somma1 'par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
            '    End Select
            'End While
            'myReader2.Close()

            ''MODIFICA DEL 29 APR 2011
            'SommaPrenotato = SommaPrenotato - SommaConsuntivato

            'SommaResiduo = SommaAssesato - (SommaPrenotato + SommaConsuntivato + SommaLiquidato)

            'Me.txtImporto.Text = IsNumFormat(SommaValoreLordo, "", "##,##0.00")
            'Me.txtImporto1.Text = IsNumFormat(SommaAssesato, "", "##,##0.00")

            'Me.txtImporto2.Text = IsNumFormat(SommaPrenotato, "", "##,##0.00")
            'Me.txtImporto3.Text = IsNumFormat(SommaConsuntivato, "", "##,##0.00")
            'Me.txtImporto4.Text = IsNumFormat(SommaLiquidato, "", "##,##0.00")

            'Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")


            ''LETTURA PENALE
            'CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text = SommaPenale
            'par.cmd.CommandText = " select to_char(SUM(IMPORTO) ) from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE in (" & vIdPrenotazioni & ")"
            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaPenale = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '****************************


            'RIEPILOGO SAL
            'par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
            '                          & " ID_VOCE_PF_IMPORTO,ID_APPALTO  " _
            '                   & " from   SISCOM_MI.PRENOTAZIONI" _
            '                   & " where ID in (" & vIdPrenotazioni & ")"


            'myReader2 = par.cmd.ExecuteReader()

            'While myReader2.Read
            '    sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")
            '    Somma1 = Decimal.Parse(sRisultato)
            '    CalcolaImporti(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")


            '    sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")
            '    Somma1 = Decimal.Parse(sRisultato)
            '    CalcolaImporti(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")


            '    'CalcolaImporti(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
            '    'CalcolaImporti(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")


            'End While
            'myReader2.Close()


            '' 1) riempire la griglia con tutte le manutenzioni
            '' 2) calcola importi
            '' 3) la somma di "Calcola importi"


            'CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(SommaPenale, 0), "", "##,##0.00")


            'CType(Tab_SAL_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(risultato4APR, "", "##,##0.00")

            'CType(Tab_SAL_Riepilogo.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriAPR, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1APR, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaAPR, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2APR, "", "##,##0.00")

            'CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaAPR, "", "##,##0.00") '6 campo

            'CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3APR, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaAPR, "", "##,##0.00")

            'CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(importoAPR, "", "##,##0.00")

            ''****************
            'CType(Tab_SAL_Riepilogo.FindControl("txtImporto2"), TextBox).Text = IsNumFormat(risultato4PRE, "", "##,##0.00")

            'CType(Tab_SAL_Riepilogo.FindControl("txtOneri2"), TextBox).Text = IsNumFormat(oneriPRE, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto2"), TextBox).Text = IsNumFormat(risultato1PRE, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta2"), TextBox).Text = IsNumFormat(astaPRE, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtNetto2"), TextBox).Text = IsNumFormat(risultato2PRE, "", "##,##0.00")

            'CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta2"), TextBox).Text = IsNumFormat(ritenutaPRE, "", "##,##0.00") '6 campo

            'CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri2"), TextBox).Text = IsNumFormat(risultato3PRE, "", "##,##0.00")
            'CType(Tab_SAL_Riepilogo.FindControl("txtIVA2"), TextBox).Text = IsNumFormat(ivaPRE, "", "##,##0.00")

            'CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text = IsNumFormat(importoPRE, "", "##,##0.00")

            ''*************

            ''IMPORTI PROGRESIVI
            ''NOTA aggiungere anche nella where di PAGAMENTI and ANNO=" & myReader1("ANNO")
            'par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
            '                          & " ID_VOCE_PF_IMPORTO,ID_APPALTO,ID  " _
            '                   & " from   SISCOM_MI.PRENOTAZIONI" _
            '                   & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI where TIPO_PAGAMENTO=6  and ID_APPALTO=" & Me.txtID_APPALTO.Value & ") "
            'myReader2 = par.cmd.ExecuteReader

            'SommaPenale = 0
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text = 0

            'While myReader2.Read

            '    sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")
            '    Somma1 = Decimal.Parse(sRisultato)
            '    CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")

            '    sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")
            '    Somma1 = Decimal.Parse(sRisultato)
            '    CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")


            '    'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
            '    'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")

            '    'LETTURA PENALE
            '    par.cmd.CommandText = " select * from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE=" & par.IfNull(myReader2("ID"), 0)
            '    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    myReader3 = par.cmd.ExecuteReader()
            '    If myReader3.Read Then
            '        SommaPenale = SommaPenale + par.IfNull(myReader3("IMPORTO"), 0)
            '    End If
            '    myReader3.Close()
            '    '****************************

            'End While
            'myReader2.Close()

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(SommaPenale, 0), "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto"), TextBox).Text = IsNumFormat(risultato4APR, "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriAPR, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1APR, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaAPR, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2APR, "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaAPR, "", "##,##0.00") '6 campo

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3APR, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaAPR, "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(importoAPR, "", "##,##0.00")

            ''****************
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto2"), TextBox).Text = IsNumFormat(risultato4PRE, "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri2"), TextBox).Text = IsNumFormat(oneriPRE, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto2"), TextBox).Text = IsNumFormat(risultato1PRE, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta2"), TextBox).Text = IsNumFormat(astaPRE, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto2"), TextBox).Text = IsNumFormat(risultato2PRE, "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta2"), TextBox).Text = IsNumFormat(ritenutaPRE, "", "##,##0.00") '6 campo

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri2"), TextBox).Text = IsNumFormat(risultato3PRE, "", "##,##0.00")
            'CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA2"), TextBox).Text = IsNumFormat(ivaPRE, "", "##,##0.00")

            'CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA2"), TextBox).Text = IsNumFormat(importoPRE, "", "##,##0.00")


            'If vIdPagamento <> 0 Then
            'LETTURA CAMPI PAGAMENTO
            'par.cmd.CommandText = " select * from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamento
            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then

            '    Me.txtSTATO.Value = 2 'STAMPATO SAL

            '    If par.IfNull(myReader2("DATA_STAMPA"), "") <> "" Then
            '        Me.txtStatoPagamento.Value = 1 'PAGAMENTO APPROVATO e STAMPATO
            '        Me.txtSTATO.Value = 3 'STAMPATO il PAGAMENTO
            '    End If

            '    If par.IfNull(myReader2("ID_STATO"), "1") = "5" Then
            '        'LIQUIDATO
            '        CType(Tab_SAL_Riepilogo.FindControl("txtDataMandato"), TextBox).Text = par.FormattaData(par.IfNull(myReader2("DATA_MANDATO"), ""))
            '        CType(Tab_SAL_Riepilogo.FindControl("txtNumMandato"), TextBox).Text = par.IfNull(myReader2("NUMERO_MANDATO"), "")
            '    End If

            '    Me.lblPROG_Pagamento.Text = par.IfNull(myReader2("PROGR"), "") & "/" & par.IfNull(myReader2("ANNO"), "")

            '    Me.txtDataSAL.Text = par.FormattaData(par.IfNull(myReader2("DATA_EMISSIONE"), ""))
            '    Me.cmbStatoSAL.SelectedValue = par.IfNull(myReader2("STATO_FIRMA"), "0")

            'End If
            'myReader2.Close()

            'End If
            '****************************


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


        'If Me.txtSTATO.Value = 2 Then 'EMESSO PAGAMENTO
        '    FrmSolaLettura()
        '    Me.txtVisualizza.Value = 1
        'End If


    End Sub

    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        Dim ds As New Data.DataSet()

        Try
            'sValoreStato = Request.QueryString("ST") '0=PRENOTATO (1=EMESSO e 5=LIQUIDATO da PAGAMENTI)

            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            If vIdPagamento > 0 Then
                ' LEGGO PAGAMENTI
                par.cmd.Parameters.Clear()

                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento
                Else
                    par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                End If

                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                'par.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            End If

            'Dim sFiliale As String = ""
            'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
            '    sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
            'End If


            'par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI  " _
            '                           & " where ID_APPALTO=" & Me.txtID_APPALTO.Value _
            '                           & "   and TIPO_PAGAMENTO=6" _
            '                           & "   and ID_FORNITORE=" & Me.txtID_FORNITORE.Value _
            '                           & "   and DATA_SCADENZA=" & par.AggiustaData(Me.txtDataScadenza.Text) & sFiliale _
            '                           & " FOR UPDATE NOWAIT"

            'myReader1 = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    ' Per ogni riga:
            '    If vIdPrenotazioni = "" Then
            '        vIdPrenotazioni = par.IfNull(myReader1("ID"), "")
            '    Else
            '        vIdPrenotazioni = vIdPrenotazioni & "," & par.IfNull(myReader1("ID"), "")
            '    End If

            '    Me.txtDescrizioneP.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
            '    'Me.lblPROG_Prenotazione.Text = par.IfNull(myReader1("PROGR_FORNITORE"), "") & "/" & par.IfNull(myReader1("ANNO"), "")
            '    'Me.lblDataPrenotazione.Text = par.FormattaData(par.IfNull(myReader1("DATA_PRENOTAZIONE"), ""))

            '    vIdPagamento = par.IfNull(myReader1("ID_PAGAMENTO"), 0)
            '    Me.txtID_PAGAMENTI.Value = vIdPagamento

            '    Me.txtSTATO.Value = par.IfNull(myReader1("ID_STATO"), "0") '0=DA APPROVARE, 1=APPROVATO

            'End While
            'myReader1.Close()

            'RiempiCampi()


        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Pagamenti aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento
                myReader1 = par.cmd.ExecuteReader()

                If myReader1.Read() Then
                    RiempiCampi(myReader1)
                End If

                myReader1.Close()

                'par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI  " _
                '                                       & " where ID_APPALTO=" & Me.txtID_APPALTO.Value _
                '                                       & "   and TIPO_PAGAMENTO=6" _
                '                                       & "   and ID_FORNITORE=" & Me.txtID_FORNITORE.Value _
                '                                       & "   and DATA_SCADENZA=" & par.AggiustaData(Me.txtDataScadenza.Text)

                'myReader1 = par.cmd.ExecuteReader()

                'While myReader1.Read
                '    ' Per ogni riga:
                '    If vIdPrenotazioni = "" Then
                '        vIdPrenotazioni = par.IfNull(myReader1("ID"), "")
                '    Else
                '        vIdPrenotazioni = vIdPrenotazioni & "," & par.IfNull(myReader1("ID"), "")
                '    End If
                'End While
                'myReader1.Close()

                'RiempiCampi()

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                Me.txtVisualizza.Value = 1 'SOLO LETTURA
                FrmSolaLettura()

            Else
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                Page.Dispose()

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
        Dim i As Integer = 0
        Dim di As DataGridItem


        Try

            'If Me.txtSTATO.Value >= 2 Then
            '    ' RIPRENDO LA CONNESSIONE
            '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '    par.SettaCommand(par)

            '    'CREO LA TRANSAZIONE
            '    par.myTrans = par.OracleConn.BeginTransaction()
            '    ‘‘par.cmd.Transaction = par.myTrans
            '    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            'Else

            If ControllaRipartizioni() = False Then
                Exit Sub
            End If

            If par.IfEmpty(Me.txtDataSal.SelectedDate.ToString, "Null") = "Null" And par.IfEmpty(Me.txtDataDel.SelectedDate.ToString, "Null") = "Null" Then
                RadWindowManager1.RadAlert("Inserire la Data del SAL e la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            ElseIf par.IfEmpty(Me.txtDataSal.SelectedDate.ToString, "Null") = "Null" And par.IfEmpty(Me.txtDataDel.SelectedDate.ToString, "Null") <> "Null" Then
                RadWindowManager1.RadAlert("Inserire la Data del SAL!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            ElseIf par.IfEmpty(Me.txtDataSal.SelectedDate.ToString, "Null") <> "Null" And par.IfEmpty(Me.txtDataDel.SelectedDate.ToString, "Null") = "Null" Then
                RadWindowManager1.RadAlert("Inserire la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE

            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)

                '‘par.cmd.Transaction = par.myTrans
            End If

            If IsNothing(par.myTrans.Connection) Then
                par.myTrans = par.OracleConn.BeginTransaction()
            End If


            '‘par.cmd.Transaction = par.myTrans
            'End If

            'GIANCARLO 16/02/2018
            'Se non viene inserito un SAL firmato non è possibile effettuare l'aggiornamento di stato da NON FIRMATO a FIRMATO
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
            Dim NOME As String = par.cmd.ExecuteScalar
            If Not cmbStatoSAL.SelectedValue.Equals("0") Then
                If String.IsNullOrEmpty(NOME) Then
                    RadWindowManager1.RadAlert("Attenzione...Non è possibile modificare lo stato del Pagamento in <strong>FIRMATO</strong><br />senza prima aver allegato un <strong>SAL firmato</strong>!", 300, 150, "Attenzione", "", "null")
                    cmbStatoSAL.ClearSelection()
                    cmbStatoSAL.SelectedValue = "0"
                    Exit Sub
                End If
            Else
                If Not String.IsNullOrEmpty(NOME) Then
                    cmbStatoSAL.ClearSelection()
                    cmbStatoSAL.SelectedValue = "2"
                End If
            End If




            'SALVO STATO SAL
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                                & " set DATA_EMISSIONE=:data_emissione,DATA_SAL=:data_sal,STATO_FIRMA=:stato_firma, DESCRIZIONE=:descrizione,CONTO_CORRENTE=:conto_corrente " _
                                & " where ID=" & vIdPagamento

            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_emissione", par.AggiustaData(Me.txtDataDel.SelectedDate)))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_sal", par.AggiustaData(par.IfNull(Me.txtDataSal.SelectedDate, ""))))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato_firma", Me.cmbStatoSAL.SelectedValue))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Me.txtDescrizioneP.Text))
            par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "12000X01")))

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            '********************************************
            CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Enabled = False
            CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Enabled = False


            ''****Scrittura evento MODIFICA DEL PAGAMENTO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                               & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Modificato lo stato della firma in: " & Me.cmbStatoSAL.SelectedItem.Text & "')"
            par.cmd.ExecuteNonQuery()
            '****************************************************
            HiddenIDPagamento.Value = vIdPagamento

            'RIPARTIZIONI

            'DELETE
            par.cmd.Parameters.Clear()
            If par.IfEmpty(Me.txtTipo_LOTTO.Value, "E") = "E" Then
                par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI_EDIFICI where ID_PAGAMENTO=" & vIdPagamento
            Else
                par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI_IMPIANTI where ID_PAGAMENTO=" & vIdPagamento
            End If
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            '*************************************


            If ChkRipartizioni.Checked = True Then

                For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1
                    di = CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i)

                    If par.IfEmpty(Me.txtTipo_LOTTO.Value, "E") = "E" Then
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI_EDIFICI " _
                                                & " (ID_PAGAMENTO,ID_EDIFICIO,IMPORTO) " _
                                            & "values (:id_pagamento,:id,:importo)"

                    Else
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI_IMPIANTI " _
                                                & " (ID_PAGAMENTO,ID_IMPIANTO,IMPORTO) " _
                                            & "values (:id_pagamento,:id,:importo)"

                    End If

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_pagamento", vIdPagamento))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i).Cells(0).Text))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(par.IfEmpty(CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text.Replace(".", ""), 0))))

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    par.cmd.Parameters.Clear()
                Next

                ''****Scrittura evento MODIFICA RIPARTIZIONI IMPORTI DEL PAGAMENTO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Modificati gli importi ripartiti')"
                par.cmd.ExecuteNonQuery()
                par.cmd.Parameters.Clear()
                '****************************************************

            End If


            ' COMMIT
            par.myTrans.Commit()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()


            'If Me.txtSTATO.Value < 2 Then

            If vIdPagamento > 0 Then
                '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader1.Close()
            End If

            'CREO LA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            'End If

            If Me.txtSTATO.Value = 0 Then
                CType(Tab_SAL_Dettagli.FindControl("btnApri1"), Button).Visible = True
            Else
                CType(Tab_SAL_Dettagli.FindControl("btnApri1"), Button).Visible = False
            End If
            RadNotificationNote.Text = "Operazione completata correttamente!"
            RadNotificationNote.Show()
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
            Me.txtModificato.Text = "0"

            Session.Add("LAVORAZIONE", "1")

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


    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click

        sValoreProvenienza = Request.QueryString("PROVENIENZA")

        If txtModificato.Text <> "111" Then


            If sValoreProvenienza <> "CHIAMATA_DIRETTA" Then

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

            Page.Dispose()
            '**************************


            If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "1"
                Response.Write("<script>window.close();</script>")
            Else
                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

    End Sub

    Protected Sub btnINDIETRO_Click(sender As Object, e As System.EventArgs) Handles btnINDIETRO.Click
        If txtModificato.Text <> "111" Then


            'DA RICERCA SELETTIVA
            sValoreEsercizioFinanziarioR = Strings.Trim(Request.QueryString("EF_R"))

            sValorestruttura = Request.QueryString("ST")
            sValoreFornitore = Request.QueryString("FO")
            sValoreAppalto = Request.QueryString("AP")


            'DA RICERCE PER DATE
            sValoreDataS_Dal = Request.QueryString("DALS")
            sValoreDataS_Al = Request.QueryString("ALS")

            sValoreDataP_Dal = Request.QueryString("DALP")
            sValoreDataP_Al = Request.QueryString("ALP")


            sValoreTipo = Request.QueryString("TIPO")
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

                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
            Else
                Select Case sValoreTipo

                    Case "DA_APPROVARE"
                        Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & sValorestruttura _
                                                                                         & "&FO=" & sValoreFornitore _
                                                                                         & "&AP=" & sValoreAppalto _
                                                                                         & "&DALS=" & sValoreDataS_Dal _
                                                                                         & "&ALS=" & sValoreDataS_Al _
                                                                                         & "&DALP=" & sValoreDataP_Dal _
                                                                                         & "&ALP=" & sValoreDataP_Al _
                                                                                         & "&TIPO=" & sValoreTipo _
                                                                                         & "&ID_A=" & Me.txtID_APPALTO.Value _
                                                                                         & "&ID_F=" & Me.txtID_FORNITORE.Value _
                                                                                         & "&ORD=" & sOrdinamento & "');</script>")

                    Case "DA_APPROVARE_IN_SCADENZA"

                        Response.Write("<script>location.replace('RisultatiPagamenti.aspx?ST=" & sValorestruttura _
                                                                                         & "&FO=" & sValoreFornitore _
                                                                                         & "&AP=" & sValoreAppalto _
                                                                                         & "&DALS=" & sValoreDataS_Dal _
                                                                                         & "&ALS=" & sValoreDataS_Al _
                                                                                         & "&DALP=" & sValoreDataP_Dal _
                                                                                         & "&ALP=" & sValoreDataP_Al _
                                                                                         & "&TIPO=" & sValoreTipo _
                                                                                         & "&ID_A=" & Me.txtID_APPALTO.Value _
                                                                                         & "&ID_F=" & Me.txtID_FORNITORE.Value _
                                                                                         & "&ORD=" & sOrdinamento & "');</script>")



                    Case "APPROVATI"

                        Response.Write("<script>location.replace('RisultatiPagamentiStampa.aspx?ST=" & sValorestruttura _
                                                                                             & "&FO=" & sValoreFornitore _
                                                                                             & "&AP=" & sValoreAppalto _
                                                                                             & "&ID_A=" & Me.txtID_APPALTO.Value _
                                                                                             & "&ID_F=" & Me.txtID_FORNITORE.Value _
                                                                                             & "&TIPO=" & sValoreTipo _
                                                                                             & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                             & "&ORD=" & sOrdinamento & "');</script>")


                    Case "APPROVATI_SCADENZA"
                        Response.Write("<script>location.replace('RisultatiPagamentiStampa.aspx?ST=" & sValorestruttura _
                                                                                             & "&FO=" & sValoreFornitore _
                                                                                             & "&AP=" & sValoreAppalto _
                                                                                             & "&ID_A=" & Me.txtID_APPALTO.Value _
                                                                                             & "&ID_F=" & Me.txtID_FORNITORE.Value _
                                                                                             & "&TIPO=" & sValoreTipo _
                                                                                             & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                             & "&ORD=" & sOrdinamento & "');</script>")


                    Case "DA_STAMPARE_PAG"

                        Response.Write("<script>location.replace('RisultatiPagamentiStampa.aspx?ST=" & sValorestruttura _
                                                                                             & "&FO=" & sValoreFornitore _
                                                                                             & "&AP=" & sValoreAppalto _
                                                                                             & "&ID_A=" & Me.txtID_APPALTO.Value _
                                                                                             & "&ID_F=" & Me.txtID_FORNITORE.Value _
                                                                                             & "&TIPO=" & sValoreTipo _
                                                                                             & "&EF_R=" & sValoreEsercizioFinanziarioR _
                                                                                             & "&ORD=" & sOrdinamento & "');</script>")
                    Case Else
                        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                End Select

            End If

        Else
            txtModificato.Text = "1"
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        End If

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


    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)

        Me.cmbStatoSAL.Enabled = False
        Me.txtDataSal.Enabled = False
        Me.txtDataDel.Enabled = False

        CType(Tab_SAL_Dettagli.FindControl("btnApri1"), Button).Visible = False


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

    End Sub




    Sub CalcolaImporti(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String, ByVal PERC_IVA_PRENOTAZIONI As Decimal)
        Dim sStr1 As String
        Dim perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, ritenuta_IVATA, risultato1, risultato2, risultato3, risultato4 As Decimal
        Dim perc_oneri, importoDaPagare As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.* " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                & "  where ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   ID_APPALTO=" & Id_Appalto

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReaderT = par.cmd.ExecuteReader

            If myReaderT.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                'perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

                'D3= D1(-(D1 * D2 / 100))
                'D9= D4*100/D3
                Dim D3 As Decimal = 0
                D3 = par.IfNull(myReaderT("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderT("IMPORTO_CANONE"), 0) * par.IfNull(myReaderT("SCONTO_CANONE"), 0)) / 100)

                perc_oneri = (par.IfNull(myReaderT("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3

                perc_sconto = par.IfNull(myReaderT("SCONTO_CANONE"), 0)

                If PERC_IVA_PRENOTAZIONI = -1 Then
                    perc_iva = par.IfNull(myReaderT("IVA_CANONE"), 0)
                Else
                    perc_iva = PERC_IVA_PRENOTAZIONI
                End If
            End If
            myReaderT.Close()

            If Tipo = "APPROVATO" Then

                risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
                'risultato3 = Round(risultato3, 2)
                risultato3APR = risultato3APR + risultato3


                'ALIQUOTA 0,5% 
                If par.IfNull(fl_ritenuta, 0) = 1 Then
                    'X=(G*100)/(100+PERC_ONERI)
                    'F=(X*0,5)/(100-0,5)

                    'ritenuta = ((risultato3 * 100) / (100 + perc_oneri))
                    'ritenuta = (ritenuta * 0.5) / (100 - 0.5)

                    ritenuta = (risultato3 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                    ritenuta_IVATA = Round(ritenuta_IVATA, 2)
                Else
                    ritenuta = 0
                    ritenuta_IVATA = 0
                End If
                ritenutaAPR = ritenutaAPR + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok

                'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
                risultatoImponibileAPR = risultatoImponibileAPR + risultato3 - ritenuta


                oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                'oneri = Round(oneri, 2)
                oneriAPR = oneriAPR + oneri

                risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
                risultato2APR = risultato2APR + risultato2

                asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                'asta = Round(asta, 2)
                astaAPR = astaAPR + asta

                risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
                risultato1APR = risultato1APR + risultato1

                risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
                risultato4APR = risultato4APR + risultato4

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                'importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                importoDaPagare = risultato3 + (((risultato3) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                'importoDaPagare = Round(importoDaPagare, 2)

                importoAPR = importoAPR + importoDaPagare

                iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                'iva = Round(iva, 2)
                ivaAPR = ivaAPR + iva

            Else

                risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
                'risultato3 = Round(risultato3, 2)
                risultato3PRE = risultato3PRE + risultato3

                'ALIQUOTA 0,5% 
                If par.IfNull(fl_ritenuta, 0) = 1 Then
                    'X=(G*100)/(100+PERC_ONERI)
                    'F=(X*0,5)/(100-0,5)

                    'ritenuta = ((risultato3 * 100) / (100 + perc_oneri))
                    'ritenuta = (ritenuta * 0.5) / (100 - 0.5)

                    ritenuta = (risultato3 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                    ritenuta_IVATA = Round(ritenuta_IVATA, 2)



                Else
                    ritenuta = 0
                    ritenuta_IVATA = 0
                End If
                ritenutaPRE = ritenutaPRE + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok
                ritenutaNoIvaT = ritenutaNoIvaT + ritenuta
                'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
                risultatoImponibilePRE = risultatoImponibilePRE + risultato3 - ritenuta


                oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                'oneri = Round(oneri, 2)
                oneriPRE = oneriPRE + oneri

                risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
                risultato2PRE = risultato2PRE + risultato2

                asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                'asta = Round(asta, 2)
                astaPRE = astaPRE + asta

                risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
                risultato1PRE = risultato1PRE + risultato1

                risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
                risultato4PRE = risultato4PRE + risultato4

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                'importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                importoDaPagare = risultato3 + (((risultato3) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                'importoDaPagare = Round(importoDaPagare, 2)

                importoPRE = importoPRE + importoDaPagare

                iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                'iva = Round(iva, 2)
                ivaPRE = ivaPRE + iva

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
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    ' E diverso da quello di sopra perchè nella stampa del pagamento
    Sub CalcolaImportiStampaPagamento(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String, ByVal PERC_IVA_PRENOTAZIONI As Decimal)

        Dim sStr1 As String
        Dim perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, ritenuta_IVATA, risultato2, risultato3, risultato4 As Decimal

        Dim perc_oneri, importoDaPagare As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.* " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                & "  where ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   ID_APPALTO=" & Id_Appalto

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReaderT = par.cmd.ExecuteReader

            If myReaderT.Read Then

                'perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

                'D3= D1(-(D1 * D2 / 100))
                'D9= D4*100/D3
                Dim D3 As Decimal = 0
                D3 = par.IfNull(myReaderT("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderT("IMPORTO_CANONE"), 0) * par.IfNull(myReaderT("SCONTO_CANONE"), 0)) / 100)

                perc_oneri = (par.IfNull(myReaderT("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3

                perc_sconto = par.IfNull(myReaderT("SCONTO_CANONE"), 0)

                If PERC_IVA_PRENOTAZIONI = -1 Then
                    perc_iva = par.IfNull(myReaderT("IVA_CANONE"), 0)
                Else
                    perc_iva = PERC_IVA_PRENOTAZIONI
                End If

            End If
            myReaderT.Close()

            risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
            'risultato3 = Round(risultato3, 2)
            risultato3PRE = risultato3PRE + risultato3


            'ALIQUOTA 0,5% 
            If par.IfNull(fl_ritenuta, 0) = 1 Then
                'X=(G*100)/(100+PERC_ONERI)
                'F=(X*0,5)/(100-0,5)

                'ritenuta = ((risultato3 * 100) / (100 + perc_oneri))
                'ritenuta = (ritenuta * 0.5) / (100 - 0.5)

                ritenuta = (risultato3 * 0.5) / 100
                ritenuta = Round(ritenuta, 2)
                ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                ritenuta_IVATA = Round(ritenuta_IVATA, 2)
            Else
                ritenuta = 0
                ritenuta_IVATA = 0
            End If
            ritenutaPRE = ritenutaPRE + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok
            ritenutaNoIvaT = ritenutaNoIvaT + ritenuta
            'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
            risultatoImponibilePRE = risultatoImponibilePRE + risultato3 - ritenuta


            oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
            'oneri = Round(oneri, 2)
            oneriPRE = oneriPRE + oneri

            risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
            risultato2PRE = risultato2PRE + risultato2

            asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
            'asta = Round(asta, 2)
            astaPRE = astaPRE + asta

            risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
            risultato1PRE = risultato1PRE + risultato1

            risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
            risultato4PRE = risultato4PRE + risultato4


            importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
            importoDaPagare = Round(importoDaPagare, 2)
            importoPRE = importoPRE + importoDaPagare


            iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
            'iva = Round(iva, 2)
            ivaPRE = ivaPRE + iva

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

            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub CalcolaImporti_VECCHIO(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String)
        'Dim sStr1 As String
        'Dim perc_sconto, perc_iva, Y As Decimal
        'Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4 As Decimal

        'Dim perc_oneri As Decimal

        'Try

        '    '*******************APERURA CONNESSIONE*********************
        '    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '        par.OracleConn.Open()
        '        par.SettaCommand(par)
        '    End If



        '    sStr1 = "select APPALTI_LOTTI_SERVIZI.* " _
        '        & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
        '        & "  where ID_PF_VOCE_IMPORTO=" & Id_Voce _
        '        & "  and   ID_APPALTO=" & Id_Appalto

        '    Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
        '    par.cmd.CommandText = sStr1
        '    myReaderT = par.cmd.ExecuteReader

        '    If myReaderT.Read Then

        '        ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
        '        perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

        '        perc_sconto = par.IfNull(myReaderT("SCONTO_CANONE"), 0)
        '        perc_iva = par.IfNull(myReaderT("IVA_CANONE"), 0)
        '    End If
        '    myReaderT.Close()

        '    If Tipo = "APPROVATO" Then
        '        'I=G+H+ (F)                 NETTO + ONERI + IVA + (RITENUTA)
        '        importoAPR = importoAPR + importo

        '        'G=(I*100)/(100 + PERC_IVA) NETTO + ONERI + RITENUTA (LORDO - SCONTO)
        '        risultato3 = ((importo * 100) / (100 + perc_iva))
        '        risultato3APR = risultato3APR + risultato3

        '        'H=I-G (IVA)
        '        iva = importo - risultato3
        '        ivaAPR = ivaAPR + iva

        '        'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
        '        If par.IfNull(fl_ritenuta, 0) = 1 Then
        '            'X=(G*100)/(100+PERC_ONERI)
        '            'F=(X*0,5)/(100-0,5)
        '            ritenuta = ((risultato3 * 100) / (100 + perc_oneri))
        '            ritenuta = (ritenuta * 0.5) / (100 - 0.5)
        '        Else
        '            ritenuta = 0
        '        End If
        '        ritenutaAPR = ritenutaAPR + ritenuta

        '        'Y=G+F (A-D)
        '        Y = risultato3 + ritenuta

        '        'C=(Y*100)/(100+PERC_SCONTO)    LORDO-ONERI
        '        risultato1 = ((Y * 100) / (100 + perc_sconto))
        '        risultato1APR = risultato1APR + risultato1

        '        'D=(C*PERC_SCONTO)/100      
        '        asta = (risultato1 * perc_sconto) / 100
        '        astaAPR = astaAPR + asta

        '        'E=C-D                  LORDO- (ONERI+SCONTO) = NETTO
        '        risultato2 = risultato1 - asta
        '        risultato2APR = risultato2APR + risultato2

        '        'B=Y-E              ONERI
        '        oneri = Y - risultato2
        '        oneriAPR = oneriAPR + oneri

        '        'A=C+B              LORDO + ONERI
        '        risultato4 = risultato1 + oneri
        '        risultato4APR = risultato4APR + risultato4

        '    Else

        '        'I=G+H+ (F)                 NETTO + ONERI + IVA + (RITENUTA)
        '        importoPRE = importoPRE + importo

        '        'G=(I*100)/(100 + PERC_IVA) NETTO + ONERI + RITENUTA (LORDO - SCONTO)
        '        risultato3 = ((importo * 100) / (100 + perc_iva))
        '        risultato3PRE = risultato3PRE + risultato3

        '        'H=I-G (IVA)
        '        iva = importo - risultato3
        '        ivaPRE = ivaPRE + iva

        '        'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
        '        If par.IfNull(fl_ritenuta, 0) = 1 Then
        '            'X=(G*100)/(100+PERC_ONERI)
        '            'F=(X*0,5)/(100-0,5)
        '            ritenuta = ((risultato3 * 100) / (100 + perc_oneri))
        '            ritenuta = (ritenuta * 0.5) / (100 - 0.5)
        '        Else
        '            ritenuta = 0
        '        End If
        '        ritenutaPRE = ritenutaPRE + ritenuta

        '        'Y=G+F (A-D)
        '        Y = risultato3 + ritenuta

        '        'C=(Y*100)/(100+PERC_SCONTO)    LORDO-ONERI
        '        risultato1 = ((Y * 100) / (100 + perc_sconto))
        '        risultato1PRE = risultato1PRE + risultato1

        '        'D=(C*PERC_SCONTO)/100      
        '        asta = (risultato1 * perc_sconto) / 100
        '        astaPRE = astaPRE + asta

        '        'E=C-D                  LORDO- (ONERI+SCONTO) = NETTO
        '        risultato2 = risultato1 - asta
        '        risultato2PRE = risultato2PRE + risultato2

        '        'B=Y-E              ONERI
        '        oneri = Y - risultato2
        '        oneriPRE = oneriPRE + oneri

        '        'A=C+B              LORDO + ONERI
        '        risultato4 = risultato1 + oneri
        '        risultato4PRE = risultato4PRE + risultato4
        '    End If



        'Catch ex As Exception
        '    par.OracleConn.Close()
        '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        '    Session.Item("LAVORAZIONE") = "0"

        '    Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
        '    Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        'End Try

    End Sub

    Sub CalcolaImportiProgress(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String, ByVal PERC_IVA_PRENOTAZIONI As Decimal)
        Dim sStr1 As String
        Dim perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, ritenuta_IVATA, risultato2, risultato3, risultato4 As Decimal
        Dim perc_oneri, importoDaPagare As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.* " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                & "  where ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   ID_APPALTO=" & Id_Appalto

            'Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            'myReaderT = par.cmd.ExecuteReader

            'If myReaderT.Read Then

            '    ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
            '    'perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

            '    'D3= D1-(D1*D2/100)
            '    'D9= D4*100/D3
            '    Dim D3 As Decimal = 0
            '    D3 = par.IfNull(myReaderT("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderT("IMPORTO_CANONE"), 0) * par.IfNull(myReaderT("SCONTO_CANONE"), 0)) / 100)

            '    perc_oneri = (par.IfNull(myReaderT("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3

            '    perc_sconto = par.IfNull(myReaderT("SCONTO_CANONE"), 0)

            '    If PERC_IVA_PRENOTAZIONI = -1 Then
            '        perc_iva = par.IfNull(myReaderT("IVA_CANONE"), 0)
            '    Else
            '        perc_iva = PERC_IVA_PRENOTAZIONI
            '    End If

            'End If
            'myReaderT.Close()


            For Each riga As Data.DataRow In dt.Rows

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                'perc_oneri = par.IfNull(myReaderT("PERC_ONERI_SIC_CAN"), 0)

                'D3= D1-(D1*D2/100)
                'D9= D4*100/D3
                Dim D3 As Decimal = 0
                D3 = par.IfNull(riga.Item("IMPORTO_CANONE"), 0) - ((par.IfNull(riga.Item("IMPORTO_CANONE"), 0) * par.IfNull(riga.Item("SCONTO_CANONE"), 0)) / 100)

                perc_oneri = (par.IfNull(riga.Item("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3

                perc_sconto = par.IfNull(riga.Item("SCONTO_CANONE"), 0)

                If PERC_IVA_PRENOTAZIONI = -1 Then
                    perc_iva = par.IfNull(riga.Item("IVA_CANONE"), 0)
                Else
                    perc_iva = PERC_IVA_PRENOTAZIONI
                End If

            Next

            If Tipo = "APPROVATO" Then

                risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
                '   risultato3 = Round(risultato3, 2)
                risultato3APR_P = risultato3APR_P + risultato3


                'ALIQUOTA 0,5% 
                If par.IfNull(fl_ritenuta, 0) = 1 Then

                    ritenuta = (risultato3 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                    ritenuta_IVATA = Round(ritenuta_IVATA, 2)
                Else
                    ritenuta = 0
                    ritenuta_IVATA = 0
                End If
                ritenutaAPR_P = ritenutaAPR_P + ritenuta_IVATA                               'Ritenuta di legge 0,5% txtRitenuta Ok

                'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
                risultatoImponibileAPR_P = risultatoImponibileAPR_P + risultato3 - ritenuta


                oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                'oneri = Round(oneri, 2)
                oneriAPR_P = oneriAPR_P + oneri

                risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
                risultato2APR_P = risultato2APR_P + risultato2

                asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                'asta = Round(asta, 2)
                astaAPR_P = astaAPR_P + asta

                risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
                risultato1APR_P = risultato1APR_P + risultato1

                risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
                risultato4APR_P = risultato4APR_P + risultato4


                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                'importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                importoDaPagare = risultato3 + (((risultato3) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                'importoDaPagare = Round(importoDaPagare, 2)

                importoAPR_P = importoAPR_P + importoDaPagare

                iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                'iva = Round(iva, 2)
                ivaAPR_P = ivaAPR_P + iva

            Else

                risultato3 = ((importo * 100) / (100 + perc_iva))               'A netto compresi oneri (o Imponibile ) txtNettoOneri OK
                'risultato3 = Round(risultato3, 2)
                risultato3PRE_P = risultato3PRE_P + risultato3


                'ALIQUOTA 0,5% 
                If par.IfNull(fl_ritenuta, 0) = 1 Then

                    ritenuta = (risultato3 * 0.5) / 100
                    ritenuta = Round(ritenuta, 2)
                    ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                    ritenuta_IVATA = Round(ritenuta_IVATA, 2)
                Else
                    ritenuta = 0
                    ritenuta_IVATA = 0
                End If
                ritenutaPRE_P = ritenutaPRE_P + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok

                'e-mail del 13/07/2011 'MODIFICA (imponibile che appariva prima - la ritenuta senza iva) diventato "Imponibile netto trattenute"
                risultatoImponibilePRE_P = risultatoImponibilePRE_P + risultato3 - ritenuta


                oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                'oneri = Round(oneri, 2)
                oneriPRE_P = oneriPRE_P + oneri

                risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK
                risultato2PRE_P = risultato2PRE_P + risultato2

                asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                'asta = Round(asta, 2)
                astaPRE_P = astaPRE_P + asta

                risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)
                risultato1PRE_P = risultato1PRE_P + risultato1

                risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)
                risultato4PRE_P = risultato4PRE_P + risultato4

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                'importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                importoDaPagare = risultato3 + (((risultato3) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                'importoDaPagare = Round(importoDaPagare, 2)

                importoPRE_P = importoPRE_P + importoDaPagare

                iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                'iva = Round(iva, 2)
                ivaPRE_P = ivaPRE_P + iva


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
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub



    Sub CalcolaImportiProgressCONSUMO(ByVal importo As Decimal, ByVal IVA_CONSUMO As Decimal, ByVal rimborso As Decimal, ByVal penale As Decimal, ByVal Id_Voce As Decimal, ByVal Id_Appalto As Decimal, ByVal Oneri_Inseriti As Decimal, ByVal Tipo As String)
        Dim sStr1 As String
        Dim perc_oneri, perc_sconto, perc_iva As Decimal
        Dim oneri, asta, iva, risultato1, risultato2, risultato3, risultato4, ritenuta, ritenutaIVATA, risultato5 As Decimal
        Dim FlagConnessione As Boolean

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If



            sStr1 = "select APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CON,APPALTI.FL_RIT_LEGGE " _
                & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & Id_Voce _
                & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & Id_Appalto _
                & "  and   APPALTI.ID=" & Id_Appalto

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader2 = par.cmd.ExecuteReader

            If myReader2.Read Then

                ' importo = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                perc_oneri = par.IfNull(myReader2("PERC_ONERI_SIC_CON"), 0)

                perc_sconto = par.IfNull(myReader2("SCONTO_CONSUMO"), 0)
                perc_iva = IVA_CONSUMO 'par.IfNull(myReader2("IVA_CONSUMO"), 0) '************


                If Tipo = "IMPORTO_PRESUNTO" Then
                    risultato4APR_P = risultato4APR_P + importo

                    If Oneri_Inseriti = -1 Then
                        'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                        oneri = importo - ((importo * 100) / (100 + perc_oneri))
                    Else
                        oneri = Oneri_Inseriti
                    End If
                    oneri = Round(oneri, 2)
                    oneriAPR_P = oneriAPR_P + oneri

                    'LORDO senza ONERI= IMPORTO-oneri
                    risultato1 = importo - oneri
                    risultato1APR_P = risultato1APR_P + risultato1

                    'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                    asta = (risultato1 * perc_sconto) / 100
                    asta = Round(asta, 2)
                    astaAPR_P = astaAPR_P + asta

                    'NETTO senza ONERI= (LORDO senza oneri-asta) 
                    risultato2 = risultato1 - asta '- penale
                    risultato2APR_P = risultato2APR_P + risultato2

                    'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                    risultato3 = risultato2 + oneri

                    'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
                    If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                        ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                        ritenuta = Round(ritenuta, 2)
                        ritenutaIVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                        ritenutaIVATA = Round(ritenutaIVATA, 2)
                    Else
                        ritenuta = 0
                        ritenutaIVATA = 0
                    End If
                    ritenutaAPR_P = ritenutaAPR_P + ritenutaIVATA

                    'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                    'risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri (prima del 13/07/2011 vedi mail)
                    '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                    risultato5 = risultato3


                    ' ora la ritenuta viene sottratta all'imponibile
                    risultato3APR_P = risultato3APR_P + risultato3

                    risultatoImponibileAPR_P = risultatoImponibileAPR_P + risultato3 - ritenuta


                    'IVA= (NETTO con oneri*perc_iva)/100
                    iva = Math.Round((risultato5 * perc_iva) / 100, 2)
                    ivaAPR_P = ivaAPR_P + iva

                    'NETTO+ONERI+IVA
                    risultato4 = risultato5 + iva + Round(rimborso, 2)
                    importoAPR_P = importoAPR_P + risultato4


                Else
                    SommaPenale = SommaPenale + Round(penale, 2)

                    risultato4PRE_P = risultato4PRE_P + importo

                    If Oneri_Inseriti = -1 Then
                        'ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100 ora diventa (IMPORTO*100)/(100+perc_oneri)
                        oneri = importo - ((importo * 100) / (100 + perc_oneri))
                    Else
                        oneri = Oneri_Inseriti
                    End If
                    oneri = Round(oneri, 2)
                    oneriPRE_P = oneriPRE_P + oneri

                    'LORDO senza ONERI= IMPORTO-oneri
                    risultato1 = importo - oneri
                    risultato1PRE_P = risultato1PRE_P + risultato1

                    'RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
                    asta = (risultato1 * perc_sconto) / 100
                    asta = Round(asta, 2)
                    astaPRE_P = astaPRE_P + asta

                    'NETTO senza ONERI= (LORDO senza oneri-asta) 
                    risultato2 = risultato1 - asta '- penale
                    risultato2PRE_P = risultato2PRE_P + risultato2

                    'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                    risultato3 = risultato2 + oneri

                    'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
                    If par.IfNull(myReader2("FL_RIT_LEGGE"), 0) = 1 Then
                        ritenuta = (risultato3 * 0.5) / 100 '(risultato2 * 0.5) / 100
                        ritenuta = Round(ritenuta, 2)
                        ritenutaIVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                        ritenutaIVATA = Round(ritenutaIVATA, 2)
                    Else
                        ritenuta = 0
                        ritenutaIVATA = 0
                    End If
                    ritenutaPRE_P = ritenutaPRE_P + ritenutaIVATA

                    'NETTO con ONERI= (IMPORTO-asta) invece diventa NETTO SENZA ONERI - RITENUTA + ONERI  (risultato2-ritenuta+oneri)
                    'risultato5 = risultato3 - ritenuta 'risultato2 - ritenuta + oneri (prima del 13/07/2011 vedi mail)
                    '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                    risultato5 = risultato3

                    ' ora la ritenuta viene sottratta all'imponibile
                    risultato3PRE_P = risultato3PRE_P + risultato3

                    risultatoImponibilePRE_P = risultatoImponibilePRE_P + risultato3 - ritenuta


                    'IVA= (NETTO con oneri*perc_iva)/100
                    iva = Math.Round((risultato5 * perc_iva) / 100, 2)
                    ivaPRE_P = ivaPRE_P + iva

                    'NETTO+ONERI+IVA
                    risultato4 = risultato5 + iva + Round(rimborso, 2)
                    importoPRE_P = importoPRE_P + risultato4


                End If


            End If
            myReader2.Close()

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

            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub


    'Private Sub Setta_StatoPagamento()
    '
    '
    '       Me.cmbStatoSAL.Items.Clear()
    '
    '       Me.cmbStatoSAL.Items.Add(New ListItem("NON FIRMATO", 0))
    '      Me.cmbStatoSAL.Items.Add(New ListItem("FIRMATO CON RISERVA", 1))
    '     Me.cmbStatoSAL.Items.Add(New ListItem("FIRMATO", 2))
    '
    '       Me.cmbStatoSAL.SelectedValue = 0
    '
    '
    '   End Sub


    Private Sub Pdf_SAL()
        Dim sStr1 As String
        Dim sDescrizione As String
        Dim FlagConnessione As Boolean
        Dim riga As Integer

        Dim Importo_ConsuntivatoTotale As Decimal = 0

        Try

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloSAL.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()

            '*******************APERURA CONNESSIONE*********************
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            If vIdPagamento <= 0 Then


                '' RIPRENDO LA CONNESSIONE
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)

                ''RIPRENDO LA TRANSAZIONE
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                '' RIPRENDO LA CONNESSIONE
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)

                ''CREO LA TRANSAZIONE
                'par.myTrans = par.OracleConn.BeginTransaction()
                '''par.cmd.Transaction = par.myTrans
                'HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


                '2) CREO PAGAMENTO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
                Dim myReaderP As Oracle.DataAccess.Client.OracleDataReader

                myReaderP = par.cmd.ExecuteReader
                If myReaderP.Read Then
                    vIdPagamento = myReaderP(0)
                    Me.txtID_PAGAMENTI.Value = vIdPagamento
                    HiddenIDPagamento.Value = vIdPagamento
                End If
                myReaderP.Close()


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI  where ID in (" & vIdPrenotazioni & ")"
                myReaderP = par.cmd.ExecuteReader()

                While myReaderP.Read

                    If par.IfNull(myReaderP("IMPORTO_APPROVATO"), 0) = 0 Then
                        Dim Val, Rit_Legge As Decimal
                        Val = Decimal.Parse(par.IfNull(myReaderP("IMPORTO_PRENOTATO"), 0))
                        Rit_Legge = Decimal.Parse(par.IfNull(myReaderP("RIT_LEGGE_IVATA"), 0))

                        Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

                    Else
                        Dim Val, Rit_Legge As Decimal
                        Val = Decimal.Parse(par.IfNull(myReaderP("IMPORTO_APPROVATO"), 0))
                        Rit_Legge = Decimal.Parse(par.IfNull(myReaderP("RIT_LEGGE_IVATA"), 0))

                        Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge
                    End If
                End While
                Dim modalita As String = "null"
                Dim condizione As String = "null"
                par.cmd.CommandText = "select  ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO from siscom_mi.appalti where id = " & txtID_APPALTO.Value
                Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If reader.Read Then
                    modalita = par.IfNull(reader("ID_TIPO_MODALITA_PAG"), "null")
                    condizione = par.IfNull(reader("ID_TIPO_PAGAMENTO"), "null")
                End If
                reader.Close()

                'RIT_LEGGE  trasferita in Pagamenti
                Dim voce As Integer = 0
                Dim tipo As Integer = 0
                par.cmd.CommandText = "SELECT APPALTI_ANTICIPI_CONTRATTUALI.TIPO AS TIPO, " _
                    & " APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO AS VOCE" _
                    & " FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI " _
                    & " WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT MAX(ID_APPALTO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & vIdPrenotazioni & "))))"
                Dim lettoreAnticipo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettoreAnticipo.Read Then
                    tipo = par.IfNull(lettoreAnticipo("TIPO"), 0)
                    voce = par.IfNull(lettoreAnticipo("VOCE"), 0)
                End If
                lettoreAnticipo.Close()
                Dim importo_consuntivato_ant As Decimal = 0
                If tipo = 0 Then
                    importo_consuntivato_ant = Importo_ConsuntivatoTotale
                Else
                    par.cmd.CommandText = "SELECT SUM(IMPORTO_aPPROVATO-NVL(RIT_LEGGE_IVATA,0)) " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO in " _
                    & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce & ") " _
                    & " UNION " _
                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce & ")" _
                    & ") AND PRENOTAZIONI.ID IN (" & vIdPrenotazioni & ")"
                    importo_consuntivato_ant = par.IfNull(par.cmd.ExecuteScalar, 0)
                End If
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI  " _
                                            & " (ID,DATA_SAL, DATA_EMISSIONE,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,STATO_FIRMA,CONTO_CORRENTE,data_scadenza,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO,importo_consuntivato_ant) " _
                                    & "values (:id,:data_sal,:data,:descrizione,:importo,:id_fornitore,:id_appalto,1,6,:stato_firma,:conto_corrente,:data_scadenza,:ID_TIPO_MODALITA_PAG,:ID_TIPO_PAGAMENTO,:importo_consuntivato_ant) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamento))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_sal", par.AggiustaData(par.IfNull(Me.txtDataSal.SelectedDate, ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtDataDel.SelectedDate)))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_stampa", Format(Now, "yyyyMMdd")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Me.txtDescrizioneP.Text))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Importo_ConsuntivatoTotale)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", Me.txtID_FORNITORE.Value))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Me.txtID_APPALTO.Value))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato_firma", Convert.ToInt32(Me.cmbStatoSAL.SelectedValue)))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta2"), TextBox).Text.Replace(".", ""))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "")))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", par.AggiustaData(par.IfNull(Me.txtScadenza.SelectedDate, ""))))

                If modalita <> "null" Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_MODALITA_PAG", modalita))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_MODALITA_PAG", DBNull.Value))
                End If


                If condizione <> "null" Then
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_PAGAMENTO", condizione))
                Else
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_PAGAMENTO", DBNull.Value))
                End If
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_consuntivato_ant", importo_consuntivato_ant))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '************************************

                'UPDATE PRENOTAZIONI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI " _
                                   & " set  ID_STATO=2, " _
                                   & "      ID_PAGAMENTO=" & vIdPagamento _
                                   & " where ID in (" & vIdPrenotazioni & " )"
                '& " where TIPO_PAGAMENTO=6 " _
                '& "   and ID_FORNITORE=" & Me.txtID_FORNITORE.Value _
                '& "   and ID_APPALTO=" & Me.txtID_APPALTO.Value _
                '& "   and DATA_SCADENZA=" & par.AggiustaData(Me.txtDataScadenza.Text)

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""


                'UPDATE PRENOTAZIONI se DATA_PRENOTAZIONE è maggiore della DATA_EMISSIONE di PAGAMENTI
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_PRENOTAZIONE=" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                                   & " where ID_PAGAMENTO=" & vIdPagamento _
                                   & "   and DATA_PRENOTAZIONE>" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                                   & "   and substr(DATA_PRENOTAZIONE,1,4)=" & Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4)

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '**********************************


                ''****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','APPROVAZIONE PAGAMENTO')"
                par.cmd.ExecuteNonQuery()
                '****************************************************

                ''****Scrittura evento STAMPA DEL PAGAMENTO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F90','Stampa SAL')"
                par.cmd.ExecuteNonQuery()
                '****************************************************

                par.cmd.CommandText = ""

            ElseIf Me.txtSTATO.Value = 1 Then


                ' RIPRENDO LA CONNESSIONE
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)

                ''RIPRENDO LA TRANSAZIONE
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans



                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI  where ID in (" & vIdPrenotazioni & ")"
                Dim myReaderP As Oracle.DataAccess.Client.OracleDataReader

                myReaderP = par.cmd.ExecuteReader

                While myReaderP.Read

                    'If par.IfNull(myReaderP("IMPORTO_APPROVATO"), 0) = 0 Then
                    '    Dim Val, Rit_Legge As Decimal
                    '    Val = Decimal.Parse(par.IfNull(myReaderP("IMPORTO_PRENOTATO"), 0))
                    '    Rit_Legge = Decimal.Parse(par.IfNull(myReaderP("RIT_LEGGE_IVATA"), 0))

                    '    Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

                    'Else
                    '    Dim Val, Rit_Legge As Decimal
                    '    Val = Decimal.Parse(par.IfNull(myReaderP("IMPORTO_APPROVATO"), 0))
                    '    Rit_Legge = Decimal.Parse(par.IfNull(myReaderP("RIT_LEGGE_IVATA"), 0))

                    '    Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge
                    'End If

                    Dim Val, Rit_Legge As Decimal
                    Val = Decimal.Parse(par.IfNull(myReaderP("IMPORTO_APPROVATO"), 0))
                    Rit_Legge = Decimal.Parse(par.IfNull(myReaderP("RIT_LEGGE_IVATA"), 0))
                    Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

                End While
                myReaderP.Close()

                '    'UPDATE PAGAMENTI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " update SISCOM_MI.PAGAMENTI " _
                                    & " set DATA_SAL=:data_sal,DATA_EMISSIONE=:data,STATO_FIRMA=:stato_firma, DESCRIZIONE=:descrizione,CONTO_CORRENTE=:conto_corrente,IMPORTO_CONSUNTIVATO=:importo,ID_STATO=:stato,data_scadenza=:scadenza " _
                                    & " where ID=" & vIdPagamento

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_sal", par.AggiustaData(par.IfNull(Me.txtDataSal.SelectedDate, ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtDataDel.SelectedDate)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato_firma", Me.cmbStatoSAL.SelectedValue))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Me.txtDescrizioneP.Text))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "")))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""))))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Importo_ConsuntivatoTotale)))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("scadenza", par.AggiustaData(par.IfNull(Me.txtScadenza.SelectedDate, ""))))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato", "1"))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '************************************


                ''****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','Stampa SAL')"
                par.cmd.ExecuteNonQuery()
                '****************************************************

                'UPDATE PRENOTAZIONI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI " _
                                   & " set  ID_STATO=2 " _
                                   & " where ID in (" & vIdPrenotazioni & " )"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                'UPDATE PRENOTAZIONI se DATA_PRENOTAZIONE è maggiore della DATA_EMISSIONE di PAGAMENTI
                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_PRENOTAZIONE=" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                                   & " where ID_PAGAMENTO=" & vIdPagamento _
                                   & "   and DATA_PRENOTAZIONE>" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                                   & "   and substr(DATA_PRENOTAZIONE,1,4)=" & Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4)

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                '**********************************



                ''****Scrittura evento STAMPA DEL PAGAMENTO
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                   & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F90','Stampa SAL')"
                par.cmd.ExecuteNonQuery()
                '****************************************************

                par.cmd.CommandText = ""


            End If

            Me.txtSTATO.Value = 2
            SettaValori()

            Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value

            'FlagConnessione = False
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            '    FlagConnessione = True
            'End If

            par.cmd.Parameters.Clear()


            sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR_APPALTO,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_SAL,PAGAMENTI.DATA_STAMPA,PAGAMENTI.IMPORTO_CONSUNTIVATO," _
                        & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.DATA_INIZIO,APPALTI.DATA_FINE,APPALTI.DURATA_MESI," _
                        & " FORNITORI.ID as ""ID_FORNITORE"",FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE " _
                 & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                 & " where   PAGAMENTI.ID=" & vIdPagamento _
                     & " and PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                     & " and PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "
            '& " and PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO (+) "

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = sStr1
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then

                contenuto = Replace(contenuto, "$REPERTORIO$", myReader1("NUM_REPERTORIO"))
                contenuto = Replace(contenuto, "$DATA_REPERTORIO$", par.FormattaData(myReader1("DATA_REPERTORIO")))
                contenuto = Replace(contenuto, "$DESC_REPERTORIO$", myReader1("APPALTI_DESC"))


                'DATI FORNITORE
                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " - " & par.IfNull(myReader1("NOME"), ""))
                    End If
                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    Else
                        contenuto = Replace(contenuto, "$fornitore$", par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), ""))
                    End If
                End If

                'INDIRIZZO FORNITORE
                Dim sIndirizzoFornitore1 As String = ""

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReader1("ID_FORNITORE"), 0)

                Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
                myReaderT = par.cmd.ExecuteReader
                While myReaderT.Read

                    sIndirizzoFornitore1 = par.IfNull(myReaderT("TIPO"), "") _
                                    & " " & par.IfNull(myReaderT("INDIRIZZO"), "") _
                                    & " " & par.IfNull(myReaderT("CIVICO"), "") _
                                    & " - " & par.IfNull(myReaderT("CAP"), "") _
                                    & " " & par.IfNull(myReaderT("COMUNE"), "")

                End While
                myReaderT.Close()
                contenuto = Replace(contenuto, "$fornitore_indirizzo$", sIndirizzoFornitore1)
                '**********************************************


                contenuto = Replace(contenuto, "$SAL$", myReader1("PROGR_APPALTO"))
                contenuto = Replace(contenuto, "$DATA_SAL$", par.FormattaData(par.IfNull(myReader1("DATA_SAL"), "")))
                contenuto = Replace(contenuto, "$DATA_DEL$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "")))

                Me.txtPAGAMENTI_PROGR_APPALTO.Value = par.IfNull(myReader1("PROGR_APPALTO"), "")

                Me.lblSAL.Text = "STATO SAL (" & par.IfNull(myReader1("PROGR_APPALTO"), "") & ")"

                If par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")) <> "" Then
                    contenuto = Replace(contenuto, "$data_stampa$", "Milano, li " & par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))
                Else
                    contenuto = Replace(contenuto, "$data_stampa$", "")
                End If


                'contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(importoPRE_P, 0), "", "##,##0.00"))
                sDescrizione = "" 'Pagamento ODL (Vedi Allegato)" '& vbCrLf & vbCrLf

                Dim S2 As String = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & sDescrizione & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta2"), TextBox).Text & "</td>"
                'S2 = S2 & "</tr><tr>"
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile (netto delle trattenute) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtImponibile"), HiddenField).Value & "</td>"
                'S2 = S2 & "</tr><tr>"
                '*******************************

                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA2"), TextBox).Text & "</td>"

                If par.IfEmpty(CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text, 0) > 0 Then

                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat("-" & CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If

                If par.IfEmpty(CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, 0) > 0 Then
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If


                S2 = S2 & "</tr></table>"


                Dim T As String = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$imp_progressivo$", T)


                '************************************************

                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(importoPRE, 0), "", "##,##0.00"))
                sDescrizione = "" 'Pagamento ODL (Vedi Allegato)" '& vbCrLf & vbCrLf

                S2 = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & sDescrizione & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtImporto2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtOneri2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtNetto2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"

                '********** 'NOTA del 25 Agosto 2011, nella stampa del SAl non deve più apparire e calcolare la ritenuta di legge del 0,5%
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta2"), TextBox).Text & "</td>"
                'S2 = S2 & "</tr><tr>"
                ''S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Imponibile (netto delle trattenute) €</td>"
                'S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtImponibile"), HiddenField).Value & "</td>"
                'S2 = S2 & "</tr><tr>"
                '******************************

                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtIVA2"), TextBox).Text & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text & "</td>"

                If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text, 0) > 0 Then

                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat("-" & CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If

                If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, 0) > 0 Then
                    S2 = S2 & "</tr><tr>"
                    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, "", "##,##0.00") & "</td>"
                    'S2 = S2 & "</tr><tr>"
                End If

                S2 = S2 & "</tr></table>"


                T = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$imp_SAL$", T)
                '*********************************************************

                'DATE SAL

                S2 = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>DATA CONSEGNA LAVORI:</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.FormattaData(par.IfNull(myReader1("DATA_INIZIO"), "")) & "</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>DURATA CONTRATTUALE:</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DURATA_MESI"), "0") & " giorni</td>"
                S2 = S2 & "</tr><tr>"
                'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>SCADENZA CONTRATTUALE:</td>"
                S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & par.FormattaData(par.IfNull(myReader1("DATA_FINE"), "")) & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "</tr></table>"


                T = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$Date_APPALTI$", T)
                '*********************************************************



                '*********** DETTAGLIO GRIGLIA MANUTENZIONI
                'TestoPagina = TestoPagina & "</table>"
                Dim TestoGrigliaM As String = "<p style='page-break-before: always'>&nbsp;</p>"

                TestoGrigliaM = TestoGrigliaM & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>PROG/ANNO</td>" _
                                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DESCRIZIONE</td>" _
                                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Prenotato</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Approvato</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Recupero</td>" _
                                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Penale</td>" _
                                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                          & "</tr>"

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select SISCOM_MI.PRENOTAZIONI.ID,(SISCOM_MI.PRENOTAZIONI.PROGR_FORNITORE||'/'||SISCOM_MI.PRENOTAZIONI.ANNO) as ""PROG_ANNO""," _
                                       & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_PRENOTAZIONE,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_PRENOTAZIONE""," _
                                       & " to_char(to_date(substr(SISCOM_MI.PRENOTAZIONI.DATA_SCADENZA,1,8),'YYYYmmdd'),'DD/MM/YYYY') as ""DATA_SCADENZA""," _
                                       & " SISCOM_MI.PF_VOCI.DESCRIZIONE AS ""VOCE_SERVIZIO""," _
                                       & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_PRENOTATO,'9G999G999G999G999G990D99')) AS ""PREN_LORDO"", " _
                                       & " TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.IMPORTO_APPROVATO,'9G999G999G999G999G990D99')) AS ""CONS_LORDO""," _
                                       & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_PENALI.IMPORTO,'9G999G999G999G999G990D99')) AS ""PENALE""," _
                                       & " SISCOM_MI.PRENOTAZIONI.DESCRIZIONE," _
                                       & " SISCOM_MI.APPALTI_PENALI.ID as ""ID_PENALE""" _
                                       & " ,TRIM(TO_CHAR(SISCOM_MI.PRENOTAZIONI.ANTICIPO_CONTRATTUALE,'9G999G999G999G999G990D99')) AS ANTICIPO_CONTRATTUALE " _
                               & " from SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI,SISCOM_MI.APPALTI_PENALI " _
                               & " where SISCOM_MI.PRENOTAZIONI.ID_VOCE_PF=SISCOM_MI.PF_VOCI.ID (+) " _
                               & "  and  SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) " _
                               & "  and  SISCOM_MI.PRENOTAZIONI.ID in (" & vIdPrenotazioni & ")" _
                               & " order by  PROG_ANNO"


                myReaderT = par.cmd.ExecuteReader

                riga = 1
                Dim RigheTotali As Integer
                Dim TotPagine As Integer
                Dim Pagine As Integer = 1
                'Dim j As Integer
                Dim ValTotale As Decimal = 0

                RigheTotali = CType(Tab_SAL_Dettagli.FindControl("DataGrid1"), RadGrid).Items.Count
                TotPagine = Int(RigheTotali / 40)

                While myReaderT.Read

                    ValTotale = ValTotale + par.IfNull(myReaderT("CONS_LORDO"), 0)

                    If riga >= 40 Then

                        TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PROG_ANNO"), "") & "</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("DESCRIZIONE"), "") & "</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("VOCE_SERVIZIO"), "") & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PREN_LORDO"), 0) & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("CONS_LORDO"), 0) & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("ANTICIPO_CONTRATTUALE"), 0) & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PENALE"), 0) & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                              & "</tr>"

                        riga = 1
                    Else

                        TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PROG_ANNO"), "") & "</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("DESCRIZIONE"), "") & "</td>" _
                                              & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("VOCE_SERVIZIO"), "") & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PREN_LORDO"), 0) & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("CONS_LORDO"), 0) & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("ANTICIPO_CONTRATTUALE"), 0) & "</td>" _
                                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReaderT("PENALE"), 0) & "</td>" _
                                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                              & "</tr>"
                        riga = riga + 1
                    End If

                    If riga = 1 Then
                        If Pagine < TotPagine Then

                            TestoGrigliaM = TestoGrigliaM & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
                            TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>PROG/ANNO</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DESCRIZIONE</td>" _
                                                      & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Prenotato</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Approvato</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Recupero</td>" _
                                                      & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Imp. Penale</td>" _
                                                      & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                                      & "</tr>"
                            TestoGrigliaM = TestoGrigliaM & "<p style='page-break-before : always'>&nbsp;</p>"
                            Pagine = Pagine + 1
                        End If
                    End If
                End While
                myReaderT.Close()

                par.cmd.CommandText = "select sum(anticipo_contrattuale) from siscom_mi.prenotazioni where id in (" & vIdPrenotazioni & ") AND PRENOTAZIONI.ID_sTATO<>-3"
                Dim anticipo As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)


                TestoGrigliaM = TestoGrigliaM & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "Totale:" & "</td>" _
                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & IsNumFormat(CDec(ValTotale - anticipo), "", "##,##0.00") & "</td>" _
                          & "<td align='left'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                          & "</tr>"

                contenuto = Replace(contenuto, "$grigliaM$", TestoGrigliaM)
                '********************************

            End If
            myReader1.Close()

            ''*********************CHIUSURA CONNESSIONE**********************
            'If FlagConnessione = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If



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

            Dim nomefile As String = "AttSAL_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            nomefile = par.NomeFileManut("SAL", vIdPagamento) & ".pdf"
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))





            'GIANCARLO 16-02-2017
            'inserimento della stampa cdp negli allegati
            Dim descrizione As String = "Stampa SAL"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA")
            'Imposto le vecchie rielaborazioni a 2...per barrare il nome
            par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                & "SET STATO = 2 " _
                                & "WHERE " _
                                & "TIPO = " & idTipoOggetto _
                                & "AND ID_OGGETTO = " & vIdPagamento
            par.cmd.ExecuteNonQuery()
            If HiddenFieldRielabPagam.Value = "1" Then
                descrizione = "Stampa rielaborazione SAL"
                idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE SAL DI SISTEMA")
                'Imposto le vecchie rielaborazioni a 2...per barrare il nome
                par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                    & "SET STATO = 2 " _
                                    & "WHERE " _
                                    & "TIPO = " & idTipoOggetto _
                                    & "AND ID_OGGETTO = " & vIdPagamento
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
            Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
           
            par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, vIdPagamento, "../../../ALLEGATI/ORDINI/")
            If vIdPagamento <= 0 Then
                ' COMMIT
                par.myTrans.Commit()

            ElseIf Me.txtSTATO.Value = 1 Then
                ' COMMIT
                par.myTrans.Commit()

            Else
                'par.cmd.Dispose()
                'par.OracleConn.Close()
                'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                '' RIPRENDO LA CONNESSIONE
                'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                'par.SettaCommand(par)

                ''RIPRENDO LA TRANSAZIONE
                'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

               
            End If

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');", True)



        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub

    Protected Sub btnStampaSal_Click(sender As Object, e As System.EventArgs) Handles btnStampaSAL.Click

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If par.IfEmpty(Me.txtDataSal.SelectedDate.ToString, "Null") = "Null" And par.IfEmpty(Me.txtDataDel.SelectedDate.ToString, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire la Data del SAL e la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        ElseIf par.IfEmpty(Me.txtDataSal.SelectedDate.ToString, "Null") = "Null" And par.IfEmpty(Me.txtDataDel.SelectedDate.ToString, "Null") <> "Null" Then
            RadWindowManager1.RadAlert("Inserire la Data del SAL!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        ElseIf par.IfEmpty(Me.txtDataSal.SelectedDate.ToString, "Null") <> "Null" And par.IfEmpty(Me.txtDataDel.SelectedDate.ToString, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        End If

        If Me.txtElimina.Value = "1" Then
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE SAL DI SISTEMA")
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
            Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
            If String.IsNullOrEmpty(nome) Then
                HiddenFieldRielabPagam.Value = "0"
            Pdf_SAL()
        Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../ALLEGATI/ORDINI/" & nome & "','SalPagamento','');self.close();", True)
            End If
        Else
            CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
        End If


    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.EventArgs) Handles btnAnnulla.Click
        Dim s1 As String = ""

        Try

            If Me.txtElimina.Value = "1" Then

                s1 = ControllaSALsuccessivi()

                If s1 <> "" Then
                    RadWindowManager1.RadAlert("Attenzione: Impossibile annullare questo SAL perché ne sono stati emessi successivi (" & s1 & ")", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If

                If PagamentoStampato() = True Then
                    RadWindowManager1.RadAlert("Attenzione: Impossibile annullare questo SAL perché è stato già stampato l\'attestato di pagamento", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If

                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans


                'UPDATE PRENOTAZIONI
                par.cmd.Parameters.Clear()
                '27/032013 modifica annullo prenotazioni
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=0, ID_PAGAMENTO=Null,IMPORTO_APPROVATO=0 where ID_PAGAMENTO=" & vIdPagamento

                par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set  ID_STATO=-3 where ID_PAGAMENTO=" & vIdPagamento
                par.cmd.ExecuteNonQuery()

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI_EDIFICI where ID_PAGAMENTO =" & vIdPagamento
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI_IMPIANTI where ID_PAGAMENTO =" & vIdPagamento
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = "delete from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamento
                par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set id_stato =-3 where ID=" & vIdPagamento
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                vIdPagamento = 0


                'LOG CANCELLAZIONE
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                                        & ":cod_oggetto,:descrizione,:note) "

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "ANNULLO SAL in PAGAMENTI a CANONE"))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "PAGAMENTI a CANONE"))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", "PAGAMENTO " & Me.lblSAL.Text & " del:" & Me.txtDataDel.SelectedDate))
                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(par.IfEmpty(Me.txtDescrizioneP.Text, ""), 200)))

                par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", "PAGAMENTO DELLA PRENOTAZIONE CON: ID_FORNITORE=" & Me.txtID_FORNITORE.Value & " - ID_APPALTO=" & Me.txtID_APPALTO.Value))

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()
                '****************************************

                ' COMMIT
                par.myTrans.Commit()

                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                Me.txtSTATO.Value = 0
                Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value

                radNotificationNote.Text = "Annullamento completato correttamente!"
                RadNotificationNote.Show()

                SettaValori()

                Session.Add("LAVORAZIONE", "0")


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

    'Protected Sub imgStampa_Click(sender As Object, e As System.EventArgs) Handles imgStampa.Click
    '    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"


    '    If Me.txtElimina.Value = "1" Then
    '        Pdf_Pagamento()
    '    Else
    '        CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
    '    End If
    'End Sub


    Protected Sub btnApprovazione_Click(sender As Object, e As System.EventArgs) Handles btnApprovazione.Click

        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0

        Dim SommaValoreLordo As Decimal = 0
        Dim SommaValoreAssestatoLordo As Decimal = 0
        Dim SommaValoreVariazioni As Decimal = 0
        Dim SommaAssesato As Decimal = 0

        Dim Valore1 As Decimal = 0

        Dim Importo_ConsuntivatoTotale As Decimal = 0
        Dim sRisultato As String = ""

        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        Try

            If Me.txtElimina.Value = "1" Then

                If IsNothing(txtDataSal.SelectedDate) And IsNothing(txtDataDel.SelectedDate) Then
                    RadWindowManager1.RadAlert("Inserire la Data del SAL e la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                ElseIf IsNothing(txtDataSal.SelectedDate) And IsNothing(txtDataDel.SelectedDate) Then
                    RadWindowManager1.RadAlert("Inserire la Data del SAL!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                ElseIf IsNothing(txtDataSal.SelectedDate) And IsNothing(txtDataDel.SelectedDate) Then
                    RadWindowManager1.RadAlert("Inserire la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If
                If Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4) <> DateTime.Now.Year Then
                    RadWindowManager1.RadAlert("Attenzione...L\'anno della data di Emissione Pagamento è diverso dal: " & DateTime.Now.Year & "!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If
                If CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text) > CDec(ImportoResiduoDaTrattenere.Value) Then
                    RadWindowManager1.RadAlert("Attenzione...Recupero anticipazione contrattuale maggiore del residuo da trattenere!", 300, 150, "Attenzione", "", "null")

                    Exit Sub
                End If
                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans





                'Dim sFiliale As String = ""
                'If Session.Item("LIVELLO") <> "1" And (Session.Item("BP_GENERALE") = "0" Or IsNothing(Session.Item("BP_GENERALE"))) Then
                '    sFiliale = " and ID_STRUTTURA=" & Session.Item("ID_STRUTTURA")
                'End If

                '12/10/2011 prima di Appprovare, controllo se per ogni prenotazione l'importo non superi il budget approvato per la vove di BP
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI  where ID in (" & vIdPrenotazioni & ")"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read

                    ' SE ho inserito manualmente IMPORTO APPROVATO, non vado a conteggiarlo, perchè viene aggiunto sotto
                    Dim sStr1 As String = ""
                    If par.IfNull(myReader2("IMPORTO_APPROVATO"), 0) <> 0 Then
                        sStr1 = " and ID not in (" & par.IfNull(myReader2("ID"), 0) & ") "
                    End If
                    '*********************

                    'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                    par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                        & " from    SISCOM_MI.PRENOTAZIONI " _
                                        & " where   ID_STATO=0 " _
                                        & "   and   ID_PAGAMENTO is null " _
                                        & "   and   ID_VOCE_PF=" & par.IfNull(myReader2("ID_VOCE_PF"), 0) _
                                        & "   and   ID_STRUTTURA=" & par.IfNull(myReader2("ID_STRUTTURA"), -1) & sStr1



                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                    If myReader3.Read Then
                        sRisultato = par.IfNull(myReader3(0), "0")
                        SommaPrenotato = Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()
                    '******************************************


                    'IMPORTO PRENOTATO [EMESSO o APPROVATO]
                    par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                        & " from    SISCOM_MI.PRENOTAZIONI " _
                                        & " where   ID_STATO=1 " _
                                        & "   and   ID_PAGAMENTO is null " _
                                        & "   and   ID_VOCE_PF=" & par.IfNull(myReader2("ID_VOCE_PF"), 0) _
                                        & "   and   ID_STRUTTURA=" & par.IfNull(myReader2("ID_STRUTTURA"), -1) & sStr1

                    myReader3 = par.cmd.ExecuteReader()

                    If myReader3.Read Then
                        sRisultato = par.IfNull(myReader3(0), "0")
                        SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()
                    '********************************************

                    'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si può prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
                    par.cmd.CommandText = "select to_char(SUM(IMPORTO_APPROVATO) )" _
                                        & " from   SISCOM_MI.PRENOTAZIONI " _
                                        & " where  ID_STATO>1 " _
                                        & "   and  ID_PAGAMENTO is not null " _
                                        & "   and   ID_VOCE_PF=" & par.IfNull(myReader2("ID_VOCE_PF"), 0) _
                                        & "   and   ID_STRUTTURA=" & par.IfNull(myReader2("ID_STRUTTURA"), -1) & sStr1


                    myReader3 = par.cmd.ExecuteReader
                    If myReader3.Read Then
                        sRisultato = par.IfNull(myReader3(0), "0")
                        SommaConsuntivato = Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()
                    '****************************************************

                    'SOMMA_LORDO
                    par.cmd.CommandText = "select to_char(SUM(VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                       & " where ID_VOCE=" & par.IfNull(myReader2("ID_VOCE_PF"), 0) _
                                       & "   and ID_STRUTTURA=" & par.IfNull(myReader2("ID_STRUTTURA"), -1)

                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        sRisultato = par.IfNull(myReader3(0), "0")
                        SommaValoreLordo = Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()
                    '*******************************************************

                    'SOMMA_LORDO_ASSESSTATO
                    par.cmd.CommandText = "select to_char(SUM(ASSESTAMENTO_VALORE_LORDO)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                       & " where ID_VOCE=" & par.IfNull(myReader2("ID_VOCE_PF"), 0) _
                                       & "   and ID_STRUTTURA=" & par.IfNull(myReader2("ID_STRUTTURA"), -1)


                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        sRisultato = par.IfNull(myReader3(0), "0")
                        SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()
                    '********************************************************

                    'VARIAZIONI
                    par.cmd.CommandText = "select to_char(SUM(VARIAZIONI)) from SISCOM_MI.PF_VOCI_STRUTTURA " _
                                       & " where ID_VOCE=" & par.IfNull(myReader2("ID_VOCE_PF"), 0) _
                                       & "   and ID_STRUTTURA=" & par.IfNull(myReader2("ID_STRUTTURA"), -1)


                    myReader3 = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        sRisultato = par.IfNull(myReader3(0), "0")
                        SommaValoreVariazioni = Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()
                    '*******************************************************

                    '[DA_APPROVARE + (PRENOTATO + CONSUNTIVATO)] < [somma(VALORE_LORDO) + somma(ASSESTAMENTO_VALORE_LORDO) + Somma(VARIAZIONI)]
                    Valore1 = par.IfNull(myReader2("IMPORTO_APPROVATO"), 0) + (SommaPrenotato + SommaConsuntivato)
                    SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo + SommaValoreVariazioni

                    If Valore1 > SommaAssesato Then
                        Dim strMsg As String = "L\'importo della prenotazione PROG:" & par.IfNull(myReader2("PROGR_FORNITORE"), "") & "/" & par.IfNull(myReader2("ANNO"), "") & " supera il budget approvato per la voce di BP, impossibile emettere il SAL!"
                        myReader2.Close()
                        par.myTrans.Rollback()

                        par.cmd.CommandText = ""
                        par.cmd.Parameters.Clear()

                        SettaValori()
                        RadWindowManager1.RadAlert(strMsg, 300, 150, "Attenzione", "", "null")
                        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                        Me.txtModificato.Text = "0"

                        Session.Add("LAVORAZIONE", "0")
                        Exit Sub
                    End If

                End While
                myReader2.Close()
                '***************************************

                Me.txtSTATO.Value = 1   'APPROVATO
                Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value


                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI  where ID in (" & vIdPrenotazioni & ")"
                '& " where ID_APPALTO=" & Me.txtID_APPALTO.Value _
                '& "   and TIPO_PAGAMENTO=6" _
                '& "   and ID_FORNITORE=" & Me.txtID_FORNITORE.Value _
                '& "   and DATA_SCADENZA=" & par.AggiustaData(Me.txtDataScadenza.Text) & sFiliale

                myReader2 = par.cmd.ExecuteReader()

                While myReader2.Read

                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI " _
                                       & " set ID_STATO=" & Me.txtSTATO.Value

                    'If par.IfNull(myReader2("IMPORTO_APPROVATO"), 0) = 0 Then
                    '    Dim Val, Rit_Legge As Decimal
                    '    Val = Decimal.Parse(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0))
                    '    Rit_Legge = Decimal.Parse(par.IfNull(myReader2("RIT_LEGGE_IVATA"), 0))

                    '    Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

                    '    par.cmd.CommandText = par.cmd.CommandText & ",IMPORTO_APPROVATO=" & par.VirgoleInPunti(Val)
                    'Else
                    '    Dim Val, Rit_Legge As Decimal
                    '    Val = Decimal.Parse(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0))
                    '    Rit_Legge = Decimal.Parse(par.IfNull(myReader2("RIT_LEGGE_IVATA"), 0))

                    '    Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

                    'End If

                    Dim Val, Rit_Legge As Decimal
                    Val = Decimal.Parse(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0))
                    Rit_Legge = Decimal.Parse(par.IfNull(myReader2("RIT_LEGGE_IVATA"), 0))
                    par.cmd.CommandText = par.cmd.CommandText & ",IMPORTO_APPROVATO=" & par.VirgoleInPunti(Val)
                    Importo_ConsuntivatoTotale = Importo_ConsuntivatoTotale + Val - Rit_Legge

                    par.cmd.CommandText = par.cmd.CommandText & " where ID=" & par.IfNull(myReader2("ID"), 0)
                    par.cmd.ExecuteNonQuery()

                    ' ''****Scrittura evento PRENOTAZIONE
                    'par.cmd.Parameters.Clear()
                    'par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PRENOTAZIONI (ID_PRENOTAZIONE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & par.IfNull(myReader2("ID"), 0) & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F02','Cambio Stato: DA APPROVARE ad APPROVATA')"
                    'par.cmd.ExecuteNonQuery()
                    ''****************************************************

                End While
                myReader2.Close()


                If CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Text) >= CDec(Importo_ConsuntivatoTotale) Then
                    RadWindowManager1.RadAlert("Attenzione...Recupero anticipazione contrattuale maggiore dell\'importo da consuntivare!", 300, 150, "Attenzione", "", "null")
                    par.myTrans.Rollback()
                    Me.txtSTATO.Value = 0   'DA APPROVARE
                    Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value
                    SettaValori()
                    Exit Sub
                End If

                If CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text) > CDec(ImportoResiduoDaTrattenere.Value) Then
                    RadWindowManager1.RadAlert("Attenzione...Recupero anticipazione contrattuale maggiore dell\'importo residuo da trattenere!", 300, 150, "Attenzione", "", "null")
                    par.myTrans.Rollback()
                    Me.txtSTATO.Value = 0   'DA APPROVARE
                    Me.cmbStatoPAGAMENTO.SelectedValue = Me.txtSTATO.Value
                    SettaValori()
                    Exit Sub
                End If

                '' COMMIT
                'par.myTrans.Commit()
                'GIANCARLO 16/02/2018
                'Se non viene inserito un SAL firmato non è possibile effettuare l'aggiornamento di stato da NON FIRMATO a FIRMATO
                If Not cmbStatoSAL.SelectedValue.Equals("0") Then
                    Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
                    par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
                    Dim NOME As String = par.cmd.ExecuteScalar
                    If String.IsNullOrEmpty(NOME) Then
                        RadWindowManager1.RadAlert("Attenzione...Non è possibile modificare lo stato del Pagamento in <strong>FIRMATO</strong><br />senza prima aver allegato un <strong>SAL firmato</strong>!", 300, 150, "Attenzione", "", "null")
                        par.myTrans.Rollback()
                        SettaValori()
                        Exit Sub
                    End If
                End If


                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                'SettaValori()


                If vIdPagamento <= 0 Then

                    '' RIPRENDO LA CONNESSIONE
                    'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    'par.SettaCommand(par)

                    ''RIPRENDO LA TRANSAZIONE
                    'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '''par.cmd.Transaction = par.myTrans


                    '2) CREO PAGAMENTO
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "select SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
                    Dim myReaderP As Oracle.DataAccess.Client.OracleDataReader

                    myReaderP = par.cmd.ExecuteReader
                    If myReaderP.Read Then
                        vIdPagamento = myReaderP(0)
                        Me.txtID_PAGAMENTI.Value = vIdPagamento
                        HiddenIDPagamento.Value = vIdPagamento
                    End If
                    myReaderP.Close()

                    Dim modalita As String = "null"
                    Dim condizione As String = "null"
                    par.cmd.CommandText = "select  ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO from siscom_mi.appalti where id = " & txtID_APPALTO.Value
                    Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If reader.Read Then
                        modalita = par.IfNull(reader("ID_TIPO_MODALITA_PAG"), "null")
                        condizione = par.IfNull(reader("ID_TIPO_PAGAMENTO"), "null")
                    End If
                    reader.Close()
                    'RIT_LEGGE  trasferita in Pagamenti

                    Dim voce As Integer = 0
                    Dim tipo As Integer = 0
                    par.cmd.CommandText = "SELECT APPALTI_ANTICIPI_CONTRATTUALI.TIPO AS TIPO, " _
                    & " APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO AS VOCE" _
                    & " FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI " _
                    & " WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=(SELECT MAX(ID_APPALTO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID IN (" & vIdPrenotazioni & "))))"
                    Dim lettoreAnticipo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettoreAnticipo.Read Then
                        tipo = par.IfNull(lettoreAnticipo("TIPO"), 0)
                        voce = par.IfNull(lettoreAnticipo("VOCE"), 0)
                    End If
                    lettoreAnticipo.Close()
                    Dim importo_consuntivato_ant As Decimal = 0
                    If tipo = 0 Then
                        importo_consuntivato_ant = Importo_ConsuntivatoTotale
                    Else
                        par.cmd.CommandText = "SELECT SUM(IMPORTO_aPPROVATO-NVL(RIT_LEGGE_IVATA,0)) " _
                    & " FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO in " _
                    & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce & ") " _
                    & " UNION " _
                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce & ")" _
                    & ") AND PRENOTAZIONI.ID IN (" & vIdPrenotazioni & ")"
                        importo_consuntivato_ant = par.IfNull(par.cmd.ExecuteScalar, 0)
                    End If


                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.PAGAMENTI  " _
                                                    & " (ID, DATA_SAL,DATA_EMISSIONE,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,STATO_FIRMA,CONTO_CORRENTE,data_scadenza,ID_TIPO_MODALITA_PAG, ID_TIPO_PAGAMENTO, anticipo_contrattuale,importo_consuntivato_ant) " _
                                            & "values (:id,:data_sal,:data,:descrizione,:importo,:id_fornitore,:id_appalto,0,6,:stato_firma,:conto_corrente,:data_scadenza,:ID_TIPO_MODALITA_PAG, :ID_TIPO_PAGAMENTO, :anticipo_contrattuale,:importo_consuntivato_ant) "

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id", vIdPagamento))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_sal", par.AggiustaData(par.IfNull(Me.txtDataSal.SelectedDate, ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data", par.AggiustaData(Me.txtDataDel.SelectedDate)))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Me.txtDescrizioneP.Text))
                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo", strToNumber(Importo_ConsuntivatoTotale)))

                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_fornitore", Me.txtID_FORNITORE.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_appalto", Me.txtID_APPALTO.Value))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("stato_firma", Convert.ToInt32(Me.cmbStatoSAL.SelectedValue)))

                    'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("rit_legge", strToNumber(CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta2"), TextBox).Text.Replace(".", ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("conto_corrente", par.IfEmpty(Me.cmbContoCorrente.SelectedItem.ToString, "")))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_scadenza", par.AggiustaData(par.IfNull(Me.txtScadenza.SelectedDate, ""))))
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("anticipo_contrattuale", CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text)))
                    If modalita <> "null" Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_MODALITA_PAG", modalita))
                    Else
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_MODALITA_PAG", DBNull.Value))
                    End If


                    If condizione <> "null" Then
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_PAGAMENTO", condizione))
                    Else
                        par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("ID_TIPO_PAGAMENTO", DBNull.Value))
                    End If
                    par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("importo_consuntivato_ant", importo_consuntivato_ant))


                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '************************************


                    'UPDATE PRENOTAZIONI
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI " _
                                       & " set  ID_STATO=2,ID_PAGAMENTO=" & vIdPagamento _
                                       & " where ID in (" & vIdPrenotazioni & " )"

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""


                    'UPDATE PRENOTAZIONI se DATA_PRENOTAZIONE è maggiore della DATA_EMISSIONE di PAGAMENTI
                    par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set DATA_PRENOTAZIONE=" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                                           & " where ID_PAGAMENTO=" & vIdPagamento _
                                       & "   and DATA_PRENOTAZIONE>" & par.AggiustaData(Me.txtDataDel.SelectedDate) _
                                       & "   and substr(DATA_PRENOTAZIONE,1,4)=" & Left(par.AggiustaData(Me.txtDataDel.SelectedDate), 4)

                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""
                    '**********************************


                    ''****Scrittura evento PRENOTAZIONE DEL PAGAMENTO
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                       & " values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F97','APPROVAZIONE PAGAMENTO')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.Parameters.Clear()
                    '****************************************************

                    '' COMMIT
                    'par.myTrans.Commit()

                    'par.cmd.CommandText = ""
                    'par.cmd.Parameters.Clear()

                    'SettaValori()
                End If

                ' COMMIT
                par.myTrans.Commit()

                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                SettaValori()
                'CREO LA TRANSAZIONE




                CType(Tab_SAL_Riepilogo.FindControl("lblImportoApprovazione"), Label).Text = "IMPORTO APPROVATO"
                CType(Tab_SAL_RiepilogoProg.FindControl("lblImportoApprovazione"), Label).Text = "IMPORTO APPROVATO"

                radNotificationNote.Text = "Approvazione completata correttamente!"
                RadNotificationNote.Show()
                CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                Me.txtModificato.Text = "0"

                Session.Add("LAVORAZIONE", "0")


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

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try


    End Sub


    Private Sub SettaggioCampi()
        Dim FlagConnessione As Boolean
        Dim SelectIdAppalto As String = ""

        Try

            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If
            par.myTrans = par.OracleConn.BeginTransaction()
            'APPALTO
            par.cmd.Parameters.Clear()

            If par.IfEmpty(Strings.Trim(Me.txtID_APPALTO.Value), -1) = "-1" Then
                SelectIdAppalto = "(select ID_APPALTO from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamento & ") "
            Else
                SelectIdAppalto = Me.txtID_APPALTO.Value
            End If

            par.cmd.CommandText = "select APPALTI.ID,APPALTI.ID_LOTTO,APPALTI.NUM_REPERTORIO,APPALTI.DESCRIZIONE,APPALTI.FL_RIT_LEGGE,APPALTI.ID_FORNITORE, " _
                                      & " LOTTI.TIPO " _
                               & " from SISCOM_MI.APPALTI,SISCOM_MI.LOTTI " _
                               & " where SISCOM_MI.APPALTI.ID=" & SelectIdAppalto _
                               & "   and SISCOM_MI.APPALTI.ID_LOTTO=SISCOM_MI.LOTTI.ID (+)"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then

                Me.txtID_APPALTO.Value = par.IfNull(myReader1("ID"), -1)

                Me.HLink_Appalto.Text = par.IfNull(myReader1("NUM_REPERTORIO"), "")
                Me.HLink_Appalto.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Appalti.aspx?ID=" & par.IfNull(myReader1("ID"), "") & "&A=" & par.IfNull(myReader1("ID"), "") & "&IDL=-1','Appalto','height=700,width=1300');")

                Me.txtDescrizioneAppalto.Text = par.IfNull(myReader1("DESCRIZIONE"), "")

                Me.txtFL_RIT_LEGGE.Value = par.IfNull(myReader1("FL_RIT_LEGGE"), 0)
                Me.txtPERC_ONERI_SIC_CAN.Value = 0 'par.IfNull(myReader1("PERC_ONERI_SIC_CAN"), 0)


                Me.txtID_LOTTO.Value = par.IfNull(myReader1("ID_LOTTO"), "-1")
                Me.txtTipo_LOTTO.Value = par.IfNull(myReader1("TIPO"), "E")

                Me.txtID_FORNITORE.Value = par.IfNull(myReader1("ID_FORNITORE"), "-1")
            End If
            myReader1.Close()
            '*****************************

            'FORNITORE
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select ID,trim(RAGIONE_SOCIALE) as RAGIONE_SOCIALE,trim(COGNOME) as COGNOME,trim(NOME) as NOME,trim(TIPO) as TIPO,trim(COD_FORNITORE) as COD_FORNITORE " _
                               & " from SISCOM_MI.FORNITORI where ID=" & Me.txtID_FORNITORE.Value


            myReader1 = par.cmd.ExecuteReader()

            If myReader1.Read Then

                If par.IfNull(myReader1("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    Else
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("COGNOME"), "") & " " & par.IfNull(myReader1("NOME"), "")
                    End If
                Else
                    If par.IfNull(myReader1("COD_FORNITORE"), "") = "" Then
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                    Else
                        Me.HLink_Fornitore.Text = par.IfNull(myReader1("COD_FORNITORE"), "") & " - " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                    End If
                End If

                If par.IfNull(myReader1("TIPO"), "") = "F" Then
                    Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader1("ID"), "") & "','Fornitore','height=700,width=1300');")
                Else
                    Me.HLink_Fornitore.Attributes.Add("onClick", "javascript:window.open('../APPALTI/Fornitori.aspx?ID=" & par.IfNull(myReader1("ID"), "") & "','Fornitore','height=700,width=1300');")
                End If

                'If Strings.Len(Me.HLink_Fornitore.Text) >= 25 Then
                '    Me.HLink_Fornitore.Text = Left(Me.HLink_Fornitore.Text, 25) & "..."
                'End If
                'Me.txtIBAN.Text = par.IfNull(myReader1("IBAN"), "")

            End If
            myReader1.Close()
            '*****************************


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


    Sub SettaValori()
        Dim scriptblock As String
        Dim FlagConnessione As Boolean

        Dim SommaValoreLordo As Decimal = 0
        'Dim SommaValoreAssestatoLordo As Decimal = 0
        Dim SommaAssesato As Decimal = 0

        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0


        Dim Somma1 As Decimal = 0
        Dim sRisultato As String = ""



        Try

            'FlagConnessione = False
            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn.Open()
            '    par.SettaCommand(par)

            '    FlagConnessione = True
            'End If

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            End If

            Dim sFiliale As String = " and ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, "-1")

            If vIdPagamento <= 0 Then

                'CONTROLLO se nel frattempo qualcuno ha selezionato le stesse manutenzioni (se si va in EX1.Number = 54 )
                par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI where ID in (" & vIdPrenotazioni & " ) FOR UPDATE NOWAIT"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                myReaderB = par.cmd.ExecuteReader()
                myReaderB.Close()

                '*******************************


                'CONTROLLO se un istante prima la selezione delle manutenzioni, qualcuno ha creato il pagamento ed è uscito dalla maschera
                par.cmd.CommandText = "select count(*) from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO is not null and ID in (" & vIdPrenotazioni & " )"
                myReaderB = par.cmd.ExecuteReader()

                If myReaderB.Read Then
                    If par.IfNull(myReaderB(0), 0) > 0 Then
                        RadWindowManager1.RadAlert("Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!", 300, 150, "Attenzione", "", "null")
                        myReaderB.Close()

                        If FlagConnessione = True Then
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            FlagConnessione = False
                        End If

                        CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                        Me.txtVisualizza.Value = 1 'SOLO LETTURA
                        FrmSolaLettura()

                        Exit Sub
                    End If
                End If
                myReaderB.Close()
                '*************************************
            End If

            'SOMMA VALORE_CANONE
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = " select  to_char(SUM(VALORE_CANONE)) from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                     & " where ID in (select distinct(ID_VOCE_PF_IMPORTO) from SISCOM_MI.PRENOTAZIONI " _
            '                                  & " where ID in (" & vIdPrenotazioni & "))"

            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaValoreLordo = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*************************


            'SOMMA VALORE_CANONE_ASS
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = " select  to_char(SUM(VALORE_CANONE_ASS)) from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                    & " where ID in (select distinct(ID_VOCE_PF_IMPORTO) from SISCOM_MI.PRENOTAZIONI " _
            '                                  & " where ID in (" & vIdPrenotazioni & "))"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()

            'SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo
            '***************************

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(TOT_CANONE) " _
                               & " from SISCOM_MI.APPALTI " _
                               & " where APPALTI.ID=" & Me.txtID_APPALTO.Value

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaValoreLordo = Decimal.Parse(sRisultato)
            End If
            myReader2.Close()
            '*************************************
            SommaAssesato = SommaValoreLordo

            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]

            'PRIMA era cosi
            '      and   TIPO_PAGAMENTO=6"
            '& "   and   ID_VOCE_PF in ( select distinct(ID_VOCE_PF) from SISCOM_MI.PRENOTAZIONI " _
            ' " where ID in (" & vIdPrenotazioni & "))"

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   (ID_STATO=0 or ID_STATO=1) " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   TIPO_PAGAMENTO=6 " _
                                & "   and   ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPrenotato = Decimal.Parse(sRisultato) 'PRENOTATO STATO=0 [BOZZA o DA APPROVARE]
            End If
            myReader2.Close()
            '**********************************

            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=1 " _
                                & "   and   ID_PAGAMENTO is not null " _
                                & "   and   TIPO_PAGAMENTO=6 " _
                                & "   and   ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato) 'PRENOTATO STATO=1 [EMESSO o APPROVATO] forse mai verificabile
            End If
            myReader2.Close()
            '*********************************

            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=2 " _
                                & "   and   ID_PAGAMENTO is not null " _
                                & "   and   TIPO_PAGAMENTO=6 " _
                                & "   and   ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato) 'PRENOTATO STATO=2 [STAMPATO SAL]
            End If
            myReader2.Close()
            '*********************************


            'IMPORTO APPROVATO MA NON EMESSO IL SAL PRIMA DEL SAL [EMESSO o APPROVATO]
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select  to_char(SUM(IMPORTO_CONSUNTIVATO) ) from SISCOM_MI.PAGAMENTI " _
            '                    & "  where  DATA_STAMPA is null " _
            '                    & "    and  PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                             & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                             & " where  ID_APPALTO=" & Me.txtID_APPALTO.Value & sFiliale _
            '                                             & "   and ID_PAGAMENTO is not null " _
            '                                             & "   and TIPO_PAGAMENTO=6 " _
            '                                             & "   and ID_STATO=1 )" _
            '                    & "   and ID_STATO=0"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*********************************

            'EMESSO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI ,SISCOM_MI.PAGAMENTI " _
                                & " where   PRENOTAZIONI.ID_STATO>1 " _
                                & "   and   PRENOTAZIONI.ID_PAGAMENTO is not null " _
                                & "   and   PRENOTAZIONI.TIPO_PAGAMENTO=6 " _
                                & "   and   PRENOTAZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale _
                                & "   and   SISCOM_MI.PAGAMENTI.ID=SISCOM_MI.PRENOTAZIONI.ID_PAGAMENTO " _
                                & "   and   SISCOM_MI.PAGAMENTI.DATA_STAMPA is not null"

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaConsuntivato = SommaConsuntivato + Decimal.Parse(sRisultato) 'par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
            End If
            myReader2.Close()
            '********************************



            'IMPORTO LIQUIDATO
            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select to_char(SUM(IMPORTO_CONSUNTIVATO) ) " _
            '                   & " from SISCOM_MI.PAGAMENTI " _
            '                   & "  where DATA_STAMPA is NOT null " _
            '                   & "    and ID_STATO=5 "

            par.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
                               & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                               & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                      & " from   SISCOM_MI.PRENOTAZIONI " _
                                                      & " where  ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale _
                                                      & "   and  TIPO_PAGAMENTO=6 " _
                                                      & "   and  ID_STATO>1  )"

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                'IMPORTO_CONSUNTIVATO è quello pagato senza la ritenuta ivata
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaLiquidato = SommaLiquidato + Decimal.Parse(sRisultato) 'par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
            End If
            myReader2.Close()
            '*******************************

            'MODIFICA DEL 29 APR 2011 (solo per TIPO_PAGAMENTO=6)
            SommaPrenotato = SommaPrenotato - SommaConsuntivato
            SommaConsuntivato = SommaConsuntivato - SommaLiquidato


            SommaResiduo = Fix(SommaAssesato * 100) / 100.0 - (Fix(SommaPrenotato * 100) / 100.0 + Fix(SommaConsuntivato * 100) / 100.0 + Fix(SommaLiquidato * 100) / 100.0)

            Me.txtImporto.Text = IsNumFormat(Fix(SommaValoreLordo * 100) / 100.0, "", "##,##0.00")
            'Me.txtImporto1.Text = IsNumFormat(SommaAssesato, "", "##,##0.00")

            Me.txtImporto2.Text = IsNumFormat(Fix(SommaPrenotato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto3.Text = IsNumFormat(Fix(SommaConsuntivato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto4.Text = IsNumFormat(Fix(SommaLiquidato * 100) / 100.0, "", "##,##0.00")

            Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")

            'LETTURA PENALE
            CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text = SommaPenale

            SommaPenale = 0
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select to_char(SUM(IMPORTO) ) from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE in (" & vIdPrenotazioni & ")"
            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPenale = Decimal.Parse(sRisultato)
            End If
            myReader2.Close()
            '****************************

            importoAPR = 0
            oneriAPR = 0
            risultato1APR = 0
            astaAPR = 0
            risultato2APR = 0
            ritenutaAPR = 0
            risultato3APR = 0
            ivaAPR = 0
            risultato4APR = 0

            importoPRE = 0
            oneriPRE = 0
            risultato1PRE = 0
            astaPRE = 0
            risultato2PRE = 0
            ritenutaPRE = 0
            risultato3PRE = 0
            ivaPRE = 0
            risultato4PRE = 0

            importoAPR_P = 0
            oneriAPR_P = 0
            risultato1APR_P = 0
            astaAPR_P = 0
            risultato2APR_P = 0
            ritenutaAPR_P = 0
            risultato3APR_P = 0
            ivaAPR_P = 0
            risultato4APR_P = 0

            importoPRE_P = 0
            oneriPRE_P = 0
            risultato1PRE_P = 0
            astaPRE_P = 0
            risultato2PRE_P = 0
            ritenutaPRE_P = 0
            risultato3PRE_P = 0
            ivaPRE_P = 0
            risultato4PRE_P = 0



            Dim risultato3TparzialeConAnticipo As Decimal = 0
            Dim risultato3Tparziale As Decimal = 0
            Dim anticipoCalcolato As Decimal = 0




            'RIEPILOGO SAL
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                & " ID_VOCE_PF_IMPORTO,ID_APPALTO,PERC_IVA, anticipo_contrattuale,  " _
                                & " NVL((SELECT APPALTI_ANTICIPI_CONTRATTUALI.TIPO FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE APPALTI_ANTICIPI_CONTRATTUALI.ID_APPALTO=PRENOTAZIONI.ID_APPALTO AND APPALTI_ANTICIPI_CONTRATTUALI.ID_PF_VOCE_IMPORTO " _
                                & " in  " _
                                & "(SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (PRENOTAZIONI.ID_VOCE_PF_IMPORTO) " _
                                & "      UNION " _
                                & "      SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (PRENOTAZIONI.ID_VOCE_PF_IMPORTO) " _
                                & "     ) " _
                                & "),0) AS TIPO " _
                                & " from   SISCOM_MI.PRENOTAZIONI" _
                                & " where ID in (" & vIdPrenotazioni & ")" _
                                & " ORDER BY TIPO DESC "


            myReader2 = par.cmd.ExecuteReader()
            Dim importoTratt As Decimal = 0
            Dim perciva As Decimal = 0

            While myReader2.Read
                sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImporti(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO", par.IfNull(myReader2("PERC_IVA"), -1))


                sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImporti(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))

                importoTratt += par.IfNull(myReader2("ANTICIPO_CONTRATTUALE"), 0)


                If par.IfNull(myReader2("TIPO"), 0) = "1" Then
                    par.cmd.CommandText = "SELECT id FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID in " _
                                            & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce.Value & ") " _
                                            & " UNION " _
                                            & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce.Value & ")" _
                                            & ")"
                    Dim dtVociImporto As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
                    For Each riga As Data.DataRow In dtVociImporto.Rows
                        If par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0) = riga.Item("ID") Then
                            anticipoCalcolato = risultato3APR * 0.2D
                        End If
                    Next
                    'If par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0) = voce.Value Then
                    '    anticipoCalcolato = risultato3APR * 0.2D
                    'End If
                End If


                'CalcolaImporti(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
                'CalcolaImporti(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")
                perciva = par.IfNull(myReader2("PERC_IVA"), 0)


            End While
            myReader2.Close()


            ' 1) riempire la griglia con tutte le manutenzioni
            ' 2) calcola importi
            ' 3) la somma di "Calcola importi"


            CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(SommaPenale, 0), "", "##,##0.00")


            CType(Tab_SAL_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(risultato4APR, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriAPR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1APR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaAPR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2APR, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaAPR, "", "##,##0.00") '6 campo

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3APR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaAPR, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(importoAPR, "", "##,##0.00")

            Dim importo_iva As Decimal = 0
            If Me.txtSTATO.Value >= 1 Then



                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Text = IsNumFormat(importoTratt, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text = IsNumFormat(importoTratt, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                importo_iva = IsNumFormat((importoTratt * perciva) / 100, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + importoTratt, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva + importoTratt, "", "##,##0.00")
                CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

            Else
                If tipoAnticipo.Value = "1" Then
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Text = IsNumFormat(importoDaProporre.Value, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text = IsNumFormat(importoDaProporre.Value, "", "##,##0.00")

                    importo_iva = IsNumFormat((CDec(importoDaProporre.Value) * perciva) / 100, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + CDec(importoDaProporre.Value), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")


                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva + CDec(importoDaProporre.Value), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                Else
                    par.cmd.CommandText = "SELECT NVL(VOCE_ANTICIPO,-1) FROM SISCOM_MI.APPALTI WHERE ID = " & sValoreAppalto
                    Dim voceAnticipo As String = par.IfNull(par.cmd.ExecuteScalar, "-1")
                    Dim risultatoAnticipo As Decimal = risultato3APR * 0.2D
                    If voceAnticipo <> "-1" Then
                        If anticipoCalcolato <> 0 Then
                            risultatoAnticipo = anticipoCalcolato
                        Else
                            risultatoAnticipo = 0
                        End If
                    End If

                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Text = IsNumFormat(Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text = IsNumFormat(Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")


                    importo_iva = IsNumFormat((Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)) * perciva) / 100, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva + Math.Min(risultatoAnticipo, CDec(ImportoResiduoDaTrattenere.Value)), "", "##,##0.00")
                    CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

                End If
            End If


            CType(Tab_SAL_Riepilogo.FindControl("txtImporto2"), TextBox).Text = IsNumFormat(risultato4PRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtOneri2"), TextBox).Text = IsNumFormat(oneriPRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto2"), TextBox).Text = IsNumFormat(risultato1PRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta2"), TextBox).Text = IsNumFormat(astaPRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNetto2"), TextBox).Text = IsNumFormat(risultato2PRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta2"), TextBox).Text = IsNumFormat(ritenutaPRE, "", "##,##0.00") '6 campo

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri2"), TextBox).Text = IsNumFormat(risultato3PRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtIVA2"), TextBox).Text = IsNumFormat(ivaPRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text = IsNumFormat(importoPRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtImponibile"), HiddenField).Value = IsNumFormat(risultatoImponibilePRE, "", "##,##0.00")

            '*************


            'IMPORTI PROGRESIVI 
            Dim FiltraStoricoSAL As String = ""
            If vIdPagamento > 0 Then
                FiltraStoricoSAL = " And ID<=" & vIdPagamento
            End If

            'RIEPILOGO PRENOTAZIONI (IMPORTI A CANONE)
            'NOTA aggiungere anche nella where di PAGAMENTI and ANNO=" & myReader1("ANNO")
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                      & " ID_VOCE_PF_IMPORTO,ID_APPALTO,ID,PERC_IVA,anticipo_contrattuale  " _
                               & " from   SISCOM_MI.PRENOTAZIONI" _
                               & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=6 and id_stato >=0 " _
                                                                   & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & ")) " & FiltraStoricoSAL & ")"

            SommaPenale = 0
            CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text = 0
            importoTratt = 0

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            For Each riga As Data.DataRow In dt.Rows
                importoTratt += par.IfNull(riga.Item("ANTICIPO_CONTRATTUALE"), 0)

                sRisultato = par.IfNull(riga.Item("IMPORTO_PRENOTATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(riga.Item("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(riga.Item("ID_APPALTO"), 0), "APPROVATO", par.IfNull(riga.Item("PERC_IVA"), -1))

                sRisultato = par.IfNull(riga.Item("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(riga.Item("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(riga.Item("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(riga.Item("PERC_IVA"), -1))


                'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
                'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")

                'LETTURA PENALE
                par.cmd.Parameters.Clear()
                'par.cmd.CommandText = " select * from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE=" & par.IfNull(riga.Item("ID"), 0)
                par.cmd.CommandText = " select IMPORTO from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE=" & par.IfNull(riga.Item("ID"), 0)
                SommaPenale = SommaPenale + par.IfNull(par.cmd.ExecuteScalar, 0)
                '****************************

            Next


            '***************************


            'RIEPILOGO MANUTENZIONI (IMPORTI A CONSUMO)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IMPORTO_PRESUNTO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                                      & ",(SELECT ANTICIPO_CONTRATTUALE FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID=MANUTENZIONI.ID_PRENOTAZIONE_PAGAMENTO) AS ANTICIPO_CONTRATTUALE, " _
                                      & "(select PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID= manutenzioni.id_appalto )) as perc_iva " _
                                      & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=3 and id_stato >= 0" _
                                                                   & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & FiltraStoricoSAL & ")" _
                               & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


            myReader2 = par.cmd.ExecuteReader()
			importo_iva = 0

            While myReader2.Read

                importoTratt += par.IfNull(myReader2("ANTICIPO_CONTRATTUALE"), 0)

                If par.IfNull(myReader2("IMPORTO_PRESUNTO"), 0) > 0 Then

                    sRisultato = par.IfNull(myReader2("IMPORTO_PRESUNTO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgressCONSUMO(Somma1, par.IfNull(myReader2("IVA_CONSUMO"), 0), par.IfNull(myReader2("RIMBORSI"), 0), par.IfNull(myReader2("PENALE2"), 0), par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0), "IMPORTO_PRESUNTO")

                End If


                If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) > 0 Then

                    sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgressCONSUMO(Somma1, par.IfNull(myReader2("IVA_CONSUMO"), 0), par.IfNull(myReader2("RIMBORSI"), 0), par.IfNull(myReader2("PENALE2"), 0), par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0), "IMPORTO_CONSUNTIVATO")

                End If
            End While
            myReader2.Close()
            '****************************
			importo_iva += IsNumFormat((importoTratt * perciva) / 100, "", "##,##0.00")




            CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(SommaPenale, 0), "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto"), TextBox).Text = IsNumFormat(risultato4APR_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriAPR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1APR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaAPR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2APR_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaAPR_P, "", "##,##0.00") '6 campo

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3APR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaAPR_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(importoAPR_P, "", "##,##0.00")



            If Me.txtSTATO.Value >= 2 Then
                CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto"), TextBox).Text = IsNumFormat(importoTratt, "", "##,##0.00")
                CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text = IsNumFormat(importoTratt, "", "##,##0.00")
                CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")
				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Attributes.Add("ReadOnly", "ReadOnly")

				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")
				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva, "", "##,##0.00")


                CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(importo_iva + importoTratt, "", "##,##0.00")
                CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Text = IsNumFormat(importo_iva + importoTratt, "", "##,##0.00")
            Else
                CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto"), TextBox).Text = IsNumFormat(0, "", "##,##0.00")
				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text = IsNumFormat(0, "", "##,##0.00")

				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoIvaTrattenuto"), TextBox).Text = IsNumFormat(0, "", "##,##0.00")
				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoIvaTrattenuto2"), TextBox).Text = IsNumFormat(0, "", "##,##0.00")

				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTotaleTrattenuto"), TextBox).Text = IsNumFormat(0, "", "##,##0.00")
				CType(Tab_SAL_RiepilogoProg.FindControl("TextBoxImportoTotaleTrattenuto2"), TextBox).Text = IsNumFormat(0, "", "##,##0.00")
			End If

            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto2"), TextBox).Text = IsNumFormat(risultato4PRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri2"), TextBox).Text = IsNumFormat(oneriPRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto2"), TextBox).Text = IsNumFormat(risultato1PRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta2"), TextBox).Text = IsNumFormat(astaPRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto2"), TextBox).Text = IsNumFormat(risultato2PRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta2"), TextBox).Text = IsNumFormat(ritenutaPRE_P, "", "##,##0.00") '6 campo

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri2"), TextBox).Text = IsNumFormat(risultato3PRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA2"), TextBox).Text = IsNumFormat(ivaPRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA2"), TextBox).Text = IsNumFormat(importoPRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtImponibile"), HiddenField).Value = IsNumFormat(risultatoImponibilePRE_P, "", "##,##0.00")


            'If FlagConnessione = True Then
            '    par.cmd.Dispose()
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'End If

            BindGrid_Ripartizioni()

            If vIdPagamento > 0 Then
                '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
                If sValoreProvenienza = "CHIAMATA_DIRETTA" Then
                    par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento
                Else
                    par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where SISCOM_MI.PAGAMENTI.ID = " & vIdPagamento & " FOR UPDATE NOWAIT"
                End If
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                myReader1.Close()
            End If

            'If Me.txtSTATO.Value < 2 Then
            'CREO LA TRANSAZIONE
            'par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
            'End If

            If Me.txtSTATO.Value = 0 Then
                CType(Tab_SAL_Dettagli.FindControl("btnApri1"), Button).Visible = True
                AggiornamentoPrenotazioni()
            Else
                CType(Tab_SAL_Dettagli.FindControl("btnApri1"), Button).Visible = False
            End If

            Session.Add("LAVORAZIONE", "1")



        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                Me.txtVisualizza.Value = 1 'SOLO LETTURA
                FrmSolaLettura()


            Else
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

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

            Session.Add("LAVORAZIONE", "0")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Sub SettaValoriInApprovazione()
        Dim scriptblock As String
        Dim FlagConnessione As Boolean

        Dim SommaValoreLordo As Decimal = 0
        'Dim SommaValoreAssestatoLordo As Decimal = 0
        Dim SommaAssesato As Decimal = 0

        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0


        Dim Somma1 As Decimal = 0
        Dim sRisultato As String = ""



        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            End If


            Dim sFiliale As String = " and ID_STRUTTURA=" & par.IfEmpty(Me.txtID_STRUTTURA.Value, "-1")

            If vIdPagamento <= 0 Then

                'CONTROLLO se nel frattempo qualcuno ha selezionato le stesse manutenzioni (se si va in EX1.Number = 54 )
                par.cmd.CommandText = "select * from SISCOM_MI.PRENOTAZIONI where ID in (" & vIdPrenotazioni & " ) FOR UPDATE NOWAIT"
                Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                myReaderB = par.cmd.ExecuteReader()
                myReaderB.Close()

                '*******************************


                'CONTROLLO se un istante prima la selezione delle manutenzioni, qualcuno ha creato il pagamento ed è uscito dalla maschera
                par.cmd.CommandText = "select count(*) from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO is not null and ID in (" & vIdPrenotazioni & " )"
                myReaderB = par.cmd.ExecuteReader()

                If myReaderB.Read Then
                    If par.IfNull(myReaderB(0), 0) > 0 Then
                        RadWindowManager1.RadAlert("Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!", 300, 150, "Attenzione", "", "null")
                        myReaderB.Close()

                        If FlagConnessione = True Then
                            par.cmd.Dispose()
                            par.OracleConn.Close()
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                            FlagConnessione = False
                        End If

                        CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                        Me.txtVisualizza.Value = 1 'SOLO LETTURA
                        FrmSolaLettura()

                        Exit Sub
                    End If
                End If
                myReaderB.Close()
                '*************************************
            End If

            'SOMMA VALORE_CANONE
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = " select  to_char(SUM(VALORE_CANONE)) from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                     & " where ID in (select distinct(ID_VOCE_PF_IMPORTO) from SISCOM_MI.PRENOTAZIONI " _
            '                                  & " where ID in (" & vIdPrenotazioni & "))"

            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaValoreLordo = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*************************


            'SOMMA VALORE_CANONE_ASS
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = " select  to_char(SUM(VALORE_CANONE_ASS)) from SISCOM_MI.PF_VOCI_IMPORTO " _
            '                    & " where ID in (select distinct(ID_VOCE_PF_IMPORTO) from SISCOM_MI.PRENOTAZIONI " _
            '                                  & " where ID in (" & vIdPrenotazioni & "))"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaValoreAssestatoLordo = Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()

            'SommaAssesato = SommaValoreLordo + SommaValoreAssestatoLordo
            '***************************

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(TOT_CANONE) " _
                               & " from SISCOM_MI.APPALTI " _
                               & " where APPALTI.ID=" & Me.txtID_APPALTO.Value

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaValoreLordo = Decimal.Parse(sRisultato)
            End If
            myReader2.Close()
            '*************************************
            SommaAssesato = SommaValoreLordo

            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]

            'PRIMA era cosi
            '      and   TIPO_PAGAMENTO=6"
            '& "   and   ID_VOCE_PF in ( select distinct(ID_VOCE_PF) from SISCOM_MI.PRENOTAZIONI " _
            ' " where ID in (" & vIdPrenotazioni & "))"

            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_PRENOTATO) ) " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   (ID_STATO=0 or ID_STATO=1) " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   TIPO_PAGAMENTO=6 " _
                                & "   and   ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPrenotato = Decimal.Parse(sRisultato) 'PRENOTATO STATO=0 [BOZZA o DA APPROVARE]
            End If
            myReader2.Close()
            '**********************************

            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=1 " _
                                & "   and   ID_PAGAMENTO is not null " _
                                & "   and   TIPO_PAGAMENTO=6 " _
                                & "   and   ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato) 'PRENOTATO STATO=1 [EMESSO o APPROVATO] forse mai verificabile
            End If
            myReader2.Close()
            '*********************************

            'IMPORTO PRENOTATO [EMESSO o APPROVATO]
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   ID_STATO=2 " _
                                & "   and   ID_PAGAMENTO is not null " _
                                & "   and   TIPO_PAGAMENTO=6 " _
                                & "   and   ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale

            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato) 'PRENOTATO STATO=2 [STAMPATO SAL]
            End If
            myReader2.Close()
            '*********************************


            'IMPORTO APPROVATO MA NON EMESSO IL SAL PRIMA DEL SAL [EMESSO o APPROVATO]
            'par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select  to_char(SUM(IMPORTO_CONSUNTIVATO) ) from SISCOM_MI.PAGAMENTI " _
            '                    & "  where  DATA_STAMPA is null " _
            '                    & "    and  PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                             & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                             & " where  ID_APPALTO=" & Me.txtID_APPALTO.Value & sFiliale _
            '                                             & "   and ID_PAGAMENTO is not null " _
            '                                             & "   and TIPO_PAGAMENTO=6 " _
            '                                             & "   and ID_STATO=1 )" _
            '                    & "   and ID_STATO=0"

            'myReader2 = par.cmd.ExecuteReader()
            'If myReader2.Read Then
            '    sRisultato = par.IfNull(myReader2(0), "0")
            '    SommaPrenotato = SommaPrenotato + Decimal.Parse(sRisultato)
            'End If
            'myReader2.Close()
            '*********************************

            'EMESSO
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select  to_char(SUM(IMPORTO_APPROVATO) )" _
                                & " from    SISCOM_MI.PRENOTAZIONI ,SISCOM_MI.PAGAMENTI " _
                                & " where   PRENOTAZIONI.ID_STATO>1 " _
                                & "   and   PRENOTAZIONI.ID_PAGAMENTO is not null " _
                                & "   and   PRENOTAZIONI.TIPO_PAGAMENTO=6 " _
                                & "   and   PRENOTAZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale _
                                & "   and   SISCOM_MI.PAGAMENTI.ID=SISCOM_MI.PRENOTAZIONI.ID_PAGAMENTO " _
                                & "   and   SISCOM_MI.PAGAMENTI.DATA_STAMPA is not null"

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaConsuntivato = SommaConsuntivato + Decimal.Parse(sRisultato) 'par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
            End If
            myReader2.Close()
            '********************************



            'IMPORTO LIQUIDATO
            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select to_char(SUM(IMPORTO_CONSUNTIVATO) ) " _
            '                   & " from SISCOM_MI.PAGAMENTI " _
            '                   & "  where DATA_STAMPA is NOT null " _
            '                   & "    and ID_STATO=5 "

            par.cmd.CommandText = "select to_char(SUM(IMPORTO) ) " _
                               & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                               & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                      & " from   SISCOM_MI.PRENOTAZIONI " _
                                                      & " where  ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & sFiliale _
                                                      & "   and  TIPO_PAGAMENTO=6 " _
                                                      & "   and  ID_STATO>1  )"

            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then
                'IMPORTO_CONSUNTIVATO è quello pagato senza la ritenuta ivata
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaLiquidato = SommaLiquidato + Decimal.Parse(sRisultato) 'par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0)
            End If
            myReader2.Close()
            '*******************************

            'MODIFICA DEL 29 APR 2011 (solo per TIPO_PAGAMENTO=6)
            SommaPrenotato = SommaPrenotato - SommaConsuntivato
            SommaConsuntivato = SommaConsuntivato - SommaLiquidato


            SommaResiduo = Fix(SommaAssesato * 100) / 100.0 - (Fix(SommaPrenotato * 100) / 100.0 + Fix(SommaConsuntivato * 100) / 100.0 + Fix(SommaLiquidato * 100) / 100.0)

            Me.txtImporto.Text = IsNumFormat(Fix(SommaValoreLordo * 100) / 100.0, "", "##,##0.00")
            'Me.txtImporto1.Text = IsNumFormat(SommaAssesato, "", "##,##0.00")

            Me.txtImporto2.Text = IsNumFormat(Fix(SommaPrenotato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto3.Text = IsNumFormat(Fix(SommaConsuntivato * 100) / 100.0, "", "##,##0.00")
            Me.txtImporto4.Text = IsNumFormat(Fix(SommaLiquidato * 100) / 100.0, "", "##,##0.00")

            Me.txtImporto5.Text = IsNumFormat(SommaResiduo, "", "##,##0.00")

            'LETTURA PENALE
            CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text = SommaPenale

            SommaPenale = 0
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = " select to_char(SUM(IMPORTO) ) from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE in (" & vIdPrenotazioni & ")"
            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                sRisultato = par.IfNull(myReader2(0), "0")
                SommaPenale = Decimal.Parse(sRisultato)
            End If
            myReader2.Close()
            '****************************

            importoAPR = 0
            oneriAPR = 0
            risultato1APR = 0
            astaAPR = 0
            risultato2APR = 0
            ritenutaAPR = 0
            risultato3APR = 0
            ivaAPR = 0
            risultato4APR = 0

            importoPRE = 0
            oneriPRE = 0
            risultato1PRE = 0
            astaPRE = 0
            risultato2PRE = 0
            ritenutaPRE = 0
            risultato3PRE = 0
            ivaPRE = 0
            risultato4PRE = 0

            importoAPR_P = 0
            oneriAPR_P = 0
            risultato1APR_P = 0
            astaAPR_P = 0
            risultato2APR_P = 0
            ritenutaAPR_P = 0
            risultato3APR_P = 0
            ivaAPR_P = 0
            risultato4APR_P = 0

            importoPRE_P = 0
            oneriPRE_P = 0
            risultato1PRE_P = 0
            astaPRE_P = 0
            risultato2PRE_P = 0
            ritenutaPRE_P = 0
            risultato3PRE_P = 0
            ivaPRE_P = 0
            risultato4PRE_P = 0





            'RIEPILOGO SAL
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                                      & " ID_VOCE_PF_IMPORTO,ID_APPALTO,PERC_IVA  " _
                               & " from   SISCOM_MI.PRENOTAZIONI" _
                               & " where ID in (" & vIdPrenotazioni & ")"


            myReader2 = par.cmd.ExecuteReader()

            While myReader2.Read
                sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImporti(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO", par.IfNull(myReader2("PERC_IVA"), -1))


                sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImporti(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))


                'CalcolaImporti(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
                'CalcolaImporti(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")


            End While
            myReader2.Close()


            ' 1) riempire la griglia con tutte le manutenzioni
            ' 2) calcola importi
            ' 3) la somma di "Calcola importi"


            CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(SommaPenale, 0), "", "##,##0.00")


            CType(Tab_SAL_Riepilogo.FindControl("txtImporto"), TextBox).Text = IsNumFormat(risultato4APR, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriAPR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1APR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaAPR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2APR, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaAPR, "", "##,##0.00") '6 campo

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3APR, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaAPR, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(importoAPR, "", "##,##0.00")

            '****************
            CType(Tab_SAL_Riepilogo.FindControl("txtImporto2"), TextBox).Text = IsNumFormat(risultato4PRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtOneri2"), TextBox).Text = IsNumFormat(oneriPRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtOneriImporto2"), TextBox).Text = IsNumFormat(risultato1PRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtRibassoAsta2"), TextBox).Text = IsNumFormat(astaPRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtNetto2"), TextBox).Text = IsNumFormat(risultato2PRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtRitenuta2"), TextBox).Text = IsNumFormat(ritenutaPRE, "", "##,##0.00") '6 campo

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneri2"), TextBox).Text = IsNumFormat(risultato3PRE, "", "##,##0.00")
            CType(Tab_SAL_Riepilogo.FindControl("txtIVA2"), TextBox).Text = IsNumFormat(ivaPRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text = IsNumFormat(importoPRE, "", "##,##0.00")

            CType(Tab_SAL_Riepilogo.FindControl("txtImponibile"), HiddenField).Value = IsNumFormat(risultatoImponibilePRE, "", "##,##0.00")

            '*************


            'IMPORTI PROGRESIVI 
            Dim FiltraStoricoSAL As String = ""
            If vIdPagamento > 0 Then
                FiltraStoricoSAL = " and ID<=" & vIdPagamento
            End If

            'RIEPILOGO PRENOTAZIONI (IMPORTI A CANONE)
            'NOTA aggiungere anche nella where di PAGAMENTI and ANNO=" & myReader1("ANNO")
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO," _
                & " ID_VOCE_PF_IMPORTO,ID_APPALTO,ID,PERC_IVA  " _
                & " from   SISCOM_MI.PRENOTAZIONI" _
                & " where PRENOTAZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                & " where TIPO_PAGAMENTO=6 and id_stato >=0 " _
                & " and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & ")) " & FiltraStoricoSAL & ") " _
                & " union select to_char( importo_prenotato),to_char(importo_approvato),PRENOTAZIONI.ID_VOCE_PF_IMPORTO ,id_appalto,id,perc_iva from siscom_mi.prenotazioni where id in (" & vIdPrenotazioni & ")"

            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            'myReader2 = par.cmd.ExecuteReader

            SommaPenale = 0
            CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text = 0

            For Each riga As Data.DataRow In dt.Rows
                sRisultato = par.IfNull(riga.Item("IMPORTO_PRENOTATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(riga.Item("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(riga.Item("ID_APPALTO"), 0), "APPROVATO", par.IfNull(riga.Item("PERC_IVA"), -1))

                sRisultato = par.IfNull(riga.Item("IMPORTO_APPROVATO"), "0")
                Somma1 = Decimal.Parse(sRisultato)
                CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(riga.Item("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(riga.Item("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(riga.Item("PERC_IVA"), -1))


                'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
                'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")

                'LETTURA PENALE
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = " select IMPORTO from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE=" & par.IfNull(riga.Item("ID"), 0)
                SommaPenale = SommaPenale + par.IfNull(par.cmd.ExecuteScalar, 0)
                '****************************

            Next

            'While myReader2.Read

            '    sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")
            '    Somma1 = Decimal.Parse(sRisultato)
            '    CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO", par.IfNull(myReader2("PERC_IVA"), -1))

            '    sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")
            '    Somma1 = Decimal.Parse(sRisultato)
            '    CalcolaImportiProgress(Somma1, Me.txtFL_RIT_LEGGE.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))


            '    'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_PRENOTATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "APPROVATO")
            '    'CalcolaImportiProgress(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), Me.txtFL_RIT_LEGGE.Value, Me.txtPERC_ONERI_SIC_CAN.Value, par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO")

            '    'LETTURA PENALE
            '    par.cmd.Parameters.Clear()
            '    par.cmd.CommandText = " select * from SISCOM_MI.APPALTI_PENALI where ID_PRENOTAZIONE=" & par.IfNull(myReader2("ID"), 0)
            '    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    myReader3 = par.cmd.ExecuteReader()
            '    If myReader3.Read Then
            '        SommaPenale = SommaPenale + par.IfNull(myReader3("IMPORTO"), 0)
            '    End If
            '    myReader3.Close()
            '    '****************************

            'End While
            'myReader2.Close()
            '***************************


            'RIEPILOGO MANUTENZIONI (IMPORTI A CONSUMO)
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "select MANUTENZIONI.DESCRIZIONE,MANUTENZIONI.IMPORTO_CONSUNTIVATO,MANUTENZIONI.IMPORTO_PRESUNTO,MANUTENZIONI.IVA_CONSUMO," _
                                      & " MANUTENZIONI.RIMBORSI,MANUTENZIONI.ID_PF_VOCE_IMPORTO," _
                                      & " MANUTENZIONI.ID_APPALTO,MANUTENZIONI.IMPORTO_ONERI_CONS,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                               & " from   SISCOM_MI.MANUTENZIONI,SISCOM_MI.APPALTI_PENALI" _
                               & " where MANUTENZIONI.ID_PAGAMENTO in (select ID from SISCOM_MI.PAGAMENTI " _
                                                                   & " where TIPO_PAGAMENTO=3 and id_stato >= 0" _
                                                                   & "   and ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & Me.txtID_APPALTO.Value & "))" & FiltraStoricoSAL & ")" _
                               & "   and SISCOM_MI.MANUTENZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_MANUTENZIONE (+) "


            myReader2 = par.cmd.ExecuteReader()

            While myReader2.Read

                If par.IfNull(myReader2("IMPORTO_PRESUNTO"), 0) > 0 Then

                    sRisultato = par.IfNull(myReader2("IMPORTO_PRESUNTO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgressCONSUMO(Somma1, par.IfNull(myReader2("IVA_CONSUMO"), 0), par.IfNull(myReader2("RIMBORSI"), 0), par.IfNull(myReader2("PENALE2"), 0), par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0), "IMPORTO_PRESUNTO")
                End If


                If par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), 0) > 0 Then

                    sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
                    Somma1 = Decimal.Parse(sRisultato)

                    CalcolaImportiProgressCONSUMO(Somma1, par.IfNull(myReader2("IVA_CONSUMO"), 0), par.IfNull(myReader2("RIMBORSI"), 0), par.IfNull(myReader2("PENALE2"), 0), par.IfNull(myReader2("ID_PF_VOCE_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), par.IfNull(myReader2("IMPORTO_ONERI_CONS"), 0), "IMPORTO_CONSUNTIVATO")
                End If
            End While
            myReader2.Close()
            '****************************




            CType(Tab_SAL_RiepilogoProg.FindControl("txtPenale"), TextBox).Text = IsNumFormat(par.IfNull(SommaPenale, 0), "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto"), TextBox).Text = IsNumFormat(risultato4APR_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri"), TextBox).Text = IsNumFormat(oneriAPR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto"), TextBox).Text = IsNumFormat(risultato1APR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta"), TextBox).Text = IsNumFormat(astaAPR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto"), TextBox).Text = IsNumFormat(risultato2APR_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta"), TextBox).Text = IsNumFormat(ritenutaAPR_P, "", "##,##0.00") '6 campo

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri"), TextBox).Text = IsNumFormat(risultato3APR_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA"), TextBox).Text = IsNumFormat(ivaAPR_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA"), TextBox).Text = IsNumFormat(importoAPR_P, "", "##,##0.00")

            '****************
            CType(Tab_SAL_RiepilogoProg.FindControl("txtImporto2"), TextBox).Text = IsNumFormat(risultato4PRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneri2"), TextBox).Text = IsNumFormat(oneriPRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtOneriImporto2"), TextBox).Text = IsNumFormat(risultato1PRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtRibassoAsta2"), TextBox).Text = IsNumFormat(astaPRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtNetto2"), TextBox).Text = IsNumFormat(risultato2PRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtRitenuta2"), TextBox).Text = IsNumFormat(ritenutaPRE_P, "", "##,##0.00") '6 campo

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneri2"), TextBox).Text = IsNumFormat(risultato3PRE_P, "", "##,##0.00")
            CType(Tab_SAL_RiepilogoProg.FindControl("txtIVA2"), TextBox).Text = IsNumFormat(ivaPRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtNettoOneriIVA2"), TextBox).Text = IsNumFormat(importoPRE_P, "", "##,##0.00")

            CType(Tab_SAL_RiepilogoProg.FindControl("txtImponibile"), HiddenField).Value = IsNumFormat(risultatoImponibilePRE_P, "", "##,##0.00")





        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

                CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
                Me.txtVisualizza.Value = 1 'SOLO LETTURA
                FrmSolaLettura()


            Else
                If FlagConnessione = True Then
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    FlagConnessione = False
                End If

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

            Session.Add("LAVORAZIONE", "0")
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Private Function ControllaRipartizioni() As Boolean
        Dim i As Integer = 0
        Dim di As DataGridItem
        Dim Somma1 As Decimal = 0


        ControllaRipartizioni = True

        If ChkRipartizioni.Checked = True Then
            For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1

                di = CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i)

                Somma1 = Somma1 + CDbl(par.IfEmpty(CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text, 0))
            Next

            If Somma1 > Decimal.Parse(par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""), "0")) Then
                Somma1 = Somma1 - Decimal.Parse(par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""), "0"))

                RadWindowManager1.RadAlert("Attenzione: la somma degl\'importi inseriti da ripartire è superare a: " & IsNumFormat(Somma1, "", "##,##0.00") & " euro!", 300, 150, "Attenzione", "", "null")
                ControllaRipartizioni = False
                Exit Function

            ElseIf Somma1 < Decimal.Parse(par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""), "0")) Then
                Somma1 = Decimal.Parse(par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text.Replace(".", ""), "0")) - Somma1
                RadWindowManager1.RadAlert("Attenzione: la somma degl\'importi inseriti da ripartire è inferiore a; " & IsNumFormat(Somma1, "", "##,##0.00") & " euro!", 300, 150, "Attenzione", "", "null")
                ControllaRipartizioni = False
                Exit Function

            End If
        End If

    End Function


    'Private Sub AddJavascriptFunction()

    '    Dim i As Integer = 0
    '    Dim di As DataGridItem

    '    CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

    '    For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1
    '        di = CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i)

    '        CType(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

    '    Next

    'End Sub



    Protected Sub ChkRipartizioni_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkRipartizioni.CheckedChanged
        Dim i As Integer
        Dim FlagConnessione As Boolean
        Dim Trovato As Boolean = False
        Dim StringaSql As String


        Try

            If ChkRipartizioni.Checked = True Then
                TabberHide = "tabbertab"
                txttab.Text = 4

                If vIdPagamento <> 0 Then
                    FlagConnessione = False
                    If par.OracleConn.State = Data.ConnectionState.Closed Then
                        par.OracleConn.Open()
                        par.SettaCommand(par)

                        FlagConnessione = True
                    End If


                    If par.IfEmpty(Me.txtTipo_LOTTO.Value, "E") = "E" Then
                        par.cmd.CommandText = "select count(ID_EDIFICIO) from SISCOM_MI.PAGAMENTI_EDIFICI where ID_PAGAMENTO=" & vIdPagamento
                    Else
                        par.cmd.CommandText = "select count(ID_IMPIANTO) from SISCOM_MI.PAGAMENTI_IMPIANTI where ID_PAGAMENTO=" & vIdPagamento
                    End If

                    Dim myReaderTMP2 As Oracle.DataAccess.Client.OracleDataReader
                    myReaderTMP2 = par.cmd.ExecuteReader()

                    If myReaderTMP2.Read Then
                        If myReaderTMP2(0) > 0 Then
                            Trovato = True
                        End If
                    Else
                        Trovato = False
                    End If
                    myReaderTMP2.Close()

                    If par.IfEmpty(Me.txtTipo_LOTTO.Value, "E") = "E" Then
                        If Trovato = True Then
                            StringaSql = "select EDIFICI.ID,trim(EDIFICI.DENOMINAZIONE) as DENOMINAZIONE," _
                                            & " 'EDIFICIO' as TIPO, trim(TO_CHAR(SISCOM_MI.PAGAMENTI_EDIFICI.IMPORTO,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                                        & " from SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.PAGAMENTI_EDIFICI " _
                                        & " where APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=EDIFICI.ID  " _
                                        & "   and EDIFICI.ID=PAGAMENTI_EDIFICI.ID_EDIFICIO (+) " _
                                        & "   and PAGAMENTI_EDIFICI.ID_PAGAMENTO=" & vIdPagamento _
                                        & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & Me.txtID_APPALTO.Value _
                                        & " order by DENOMINAZIONE "

                        Else
                            StringaSql = "select EDIFICI.ID,trim(EDIFICI.DENOMINAZIONE) as DENOMINAZIONE," _
                                            & " 'EDIFICIO' as TIPO, trim(TO_CHAR(0,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                                        & " from SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                                        & " where APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=EDIFICI.ID  " _
                                        & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & Me.txtID_APPALTO.Value _
                                        & " order by DENOMINAZIONE "

                        End If
                    Else
                        If Trovato = True Then

                            StringaSql = "select IMPIANTI.ID, (trim(TIPOLOGIA_IMPIANTI.DESCRIZIONE)|| ' - - ' || trim(IMPIANTI.DESCRIZIONE) ) AS DENOMINAZIONE," _
                                            & " 'IMPIANTO' as TIPO,trim(TO_CHAR(SISCOM_MI.PAGAMENTI_IMPIANTI.IMPORTO,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                                      & " from SISCOM_MI.IMPIANTI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.PAGAMENTI_IMPIANTI " _
                                      & " where APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=IMPIANTI.ID   " _
                                      & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
                                      & "   and IMPIANTI.ID=PAGAMENTI_IMPIANTI.ID_IMPIANTO (+) " _
                                      & "   and PAGAMENTI_IMPIANTI.ID_PAGAMENTO=" & vIdPagamento _
                                      & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & Me.txtID_APPALTO.Value _
                                      & " order by DENOMINAZIONE "

                        Else
                            StringaSql = "select IMPIANTI.ID, (trim(TIPOLOGIA_IMPIANTI.DESCRIZIONE)|| ' - - ' || trim(IMPIANTI.DESCRIZIONE) ) AS DENOMINAZIONE," _
                                            & " 'IMPIANTO' as TIPO, trim(TO_CHAR(0,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                                        & " from SISCOM_MI.IMPIANTI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI " _
                                        & " where APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=IMPIANTI.ID  " _
                                        & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
                                        & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & Me.txtID_APPALTO.Value _
                                        & " order by DENOMINAZIONE "

                        End If

                    End If

                    par.cmd.CommandText = StringaSql

                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds As New Data.DataTable()

                    da.Fill(ds) ', "APPALTI_LOTTI_PATRIMONIO")

                    CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).DataSource = ds
                    CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).DataBind()

                    da.Dispose()
                    ds.Dispose()


                    i = 0
                    Dim di As DataGridItem

                    For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1
                        di = CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i)

                        CType(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

                        CType(CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = False
                    Next

                    If CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count > 1 Then
                        CType(Tab_SAL_Ripartizioni.FindControl("btnRipartisci"), ImageButton).Visible = True
                    Else
                        CType(Tab_SAL_Ripartizioni.FindControl("btnRipartisci"), ImageButton).Visible = False
                    End If

                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        FlagConnessione = False
                    End If

                End If


                'SETTO EDITABILE il campo IMPORTO della griglia, se la griglia ha yìun solo record, valorizzo in automatico l'importo
                For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1
                    CType(CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = False

                    If CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count = 1 Then
                        Dim di As DataGridItem
                        di = CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i)
                        CType(di.Cells(1).FindControl("txtImporto"), TextBox).Text = CType(Tab_SAL_Riepilogo.FindControl("txtNettoOneriIVA2"), TextBox).Text
                    End If
                Next i

            Else
                TabberHide = "tabbertabhide"
                NascondiTab()
                txttab.Text = 1

                'SETTO NON EDITABILE il campo IMPORTO della griglia
                For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1
                    CType(CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = True
                Next i

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


    'PRENOTAZIONI GRID1
    Private Sub BindGrid_Ripartizioni()
        Dim StringaSql As String
        Dim myReaderTMP2 As Oracle.DataAccess.Client.OracleDataReader
        Dim Trovato As Boolean

        Dim FlagConnessione As Boolean

        Try

            '' RIPRENDO LA CONNESSIONE
            'PAR.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & IdConnessione), Oracle.DataAccess.Client.OracleConnection)
            'par.SettaCommand(par)

            ''RIPRENDO LA TRANSAZIONE
            'PAR.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & IdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '''par.cmd.Transaction = par.myTrans


            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            Trovato = False

            par.cmd.Parameters.Clear()

            If vIdPagamento > 0 Then
                If par.IfEmpty(Me.txtTipo_LOTTO.Value, "E") = "E" Then
                    par.cmd.CommandText = "select count(ID_EDIFICIO) from SISCOM_MI.PAGAMENTI_EDIFICI where ID_PAGAMENTO=" & vIdPagamento
                Else
                    par.cmd.CommandText = "select count(ID_IMPIANTO) from SISCOM_MI.PAGAMENTI_IMPIANTI where ID_PAGAMENTO=" & vIdPagamento
                End If
                myReaderTMP2 = par.cmd.ExecuteReader()

                If myReaderTMP2.Read Then
                    If myReaderTMP2(0) > 0 Then
                        Trovato = True
                    End If
                Else
                    Trovato = False
                End If
                myReaderTMP2.Close()
            End If

            'Se in 'SOLO LETTURA e non trovato nulla in PAGAMENTI_EDIFICI o PAGAMENTI_IMPIANTI allora non carico tutti i record di ripartizioni, tanto non servono
            If Session.Item("BP_PC_L") = "1" Or IsNothing(Session.Item("ID_STRUTTURA")) Or Session.Item("ID_STRUTTURA") = "-1" Then
                If Trovato = False Then
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                    Exit Sub
                End If

            End If

            ' SE l'operatore è BP_GENERALE=1 (può vedere tutte le strutture) perrò la sua struttura + diversa da quella selezionata allora la maschera è in SOLO LETTURA
            If Session.Item("BP_GENERALE") = "1" And par.IfEmpty(Me.txtID_STRUTTURA.Value, -1) <> Session.Item("ID_STRUTTURA") Then
                If Trovato = False Then
                    If FlagConnessione = True Then
                        par.cmd.Dispose()
                        par.OracleConn.Close()
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    End If
                    Exit Sub
                End If
            End If

            If par.IfEmpty(Me.txtTipo_LOTTO.Value, "E") = "E" Then

                If Trovato = True Then
                    StringaSql = "select EDIFICI.ID,trim(EDIFICI.DENOMINAZIONE) as DENOMINAZIONE," _
                                    & " 'EDIFICIO' as TIPO, trim(TO_CHAR(SISCOM_MI.PAGAMENTI_EDIFICI.IMPORTO,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                                & " from SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO,SISCOM_MI.PAGAMENTI_EDIFICI " _
                                & " where APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=EDIFICI.ID  " _
                                & "   and EDIFICI.ID=PAGAMENTI_EDIFICI.ID_EDIFICIO (+) " _
                                & "   and PAGAMENTI_EDIFICI.ID_PAGAMENTO=" & vIdPagamento _
                                & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & Me.txtID_APPALTO.Value _
                                & " order by DENOMINAZIONE "

                Else
                    StringaSql = "" 'CARICO la GRIGLIA solo sul check delle ripartizioni

                    'StringaSql = "select EDIFICI.ID,trim(EDIFICI.DENOMINAZIONE) as DENOMINAZIONE," _
                    '                & " 'EDIFICIO' as TIPO, trim(TO_CHAR(0,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                    '            & " from SISCOM_MI.EDIFICI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                    '            & " where APPALTI_LOTTI_PATRIMONIO.ID_EDIFICIO=EDIFICI.ID  " _
                    '            & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & Me.txtID_APPALTO.Value _
                    '            & " order by DENOMINAZIONE "

                End If

            Else
                If Trovato = True Then

                    StringaSql = "select IMPIANTI.ID, (trim(TIPOLOGIA_IMPIANTI.DESCRIZIONE)|| ' - - ' || trim(IMPIANTI.DESCRIZIONE) ) AS DENOMINAZIONE," _
                                    & " 'IMPIANTO' as TIPO,trim(TO_CHAR(SISCOM_MI.PAGAMENTI_IMPIANTI.IMPORTO,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                              & " from SISCOM_MI.IMPIANTI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI,SISCOM_MI.PAGAMENTI_IMPIANTI " _
                              & " where APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=IMPIANTI.ID   " _
                              & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
                              & "   and IMPIANTI.ID=PAGAMENTI_IMPIANTI.ID_IMPIANTO (+) " _
                              & "   and PAGAMENTI_IMPIANTI.ID_PAGAMENTO=" & vIdPagamento _
                              & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO=" & Me.txtID_APPALTO.Value _
                              & " order by DENOMINAZIONE "

                Else
                    StringaSql = "" 'CARICO la GRIGLIA solo sul check delle ripartizioni
                    'StringaSql = "select IMPIANTI.ID, (trim(TIPOLOGIA_IMPIANTI.DESCRIZIONE)|| ' - - ' || trim(IMPIANTI.DESCRIZIONE) ) AS DENOMINAZIONE," _
                    '                & " 'IMPIANTO' as TIPO, trim(TO_CHAR(0,'9G999G999G999G999G990D99')) AS ""IMPORTO""" _
                    '            & " from SISCOM_MI.IMPIANTI, SISCOM_MI.APPALTI_LOTTI_PATRIMONIO, SISCOM_MI.TIPOLOGIA_IMPIANTI " _
                    '            & " where APPALTI_LOTTI_PATRIMONIO.ID_IMPIANTO=IMPIANTI.ID  " _
                    '            & "   and TIPOLOGIA_IMPIANTI.COD = IMPIANTI.COD_TIPOLOGIA " _
                    '            & "   and APPALTI_LOTTI_PATRIMONIO.ID_APPALTO =" & Me.txtID_APPALTO.Value _
                    '            & " order by DENOMINAZIONE "

                End If
            End If

            If StringaSql <> "" Then
                par.cmd.CommandText = StringaSql

                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ds As New Data.DataTable()

                da.Fill(ds) ', "APPALTI_LOTTI_PATRIMONIO")

                CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).DataSource = ds
                CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).DataBind()

                da.Dispose()
                ds.Dispose()


                Dim i As Integer = 0
                Dim di As DataGridItem

                For i = 0 To CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count - 1
                    di = CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i)

                    CType(di.Cells(1).FindControl("txtImporto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

                    CType(CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items(i).Cells(0).FindControl("txtImporto"), TextBox).ReadOnly = False
                Next
            End If

            Me.ChkRipartizioni.Checked = Trovato

            If vIdPagamento > 0 Then
                Me.ChkRipartizioni.Visible = True
            Else
                Me.ChkRipartizioni.Visible = False
            End If

            If CType(Tab_SAL_Ripartizioni.FindControl("DataGrid1"), DataGrid).Items.Count > 1 Then
                CType(Tab_SAL_Ripartizioni.FindControl("btnRipartisci"), ImageButton).Visible = True
            Else
                CType(Tab_SAL_Ripartizioni.FindControl("btnRipartisci"), ImageButton).Visible = False
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


    Function ControllaSALsuccessivi() As String
        Dim FlagConnessione As Boolean

        ControllaSALsuccessivi = ""

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'CONTROLLO SE SONO STATI FATTI ALTRI SAL
            par.cmd.CommandText = "select PROGR_APPALTO from SISCOM_MI.PAGAMENTI " _
                            & " where TIPO_PAGAMENTO=6 and id_stato >= 0 " _
                            & "   and ID_APPALTO in ( (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =(select distinct(ID_APPALTO) from SISCOM_MI.PAGAMENTI where ID=" & vIdPagamento & " ))))" _
                            & "    and PROGR_APPALTO>" & Val(Me.txtPAGAMENTI_PROGR_APPALTO.Value) _
                            & " order by PROGR_APPALTO "


            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            myReaderT = par.cmd.ExecuteReader
            While myReaderT.Read
                If ControllaSALsuccessivi <> "" Then
                    ControllaSALsuccessivi = ControllaSALsuccessivi & "," & par.IfNull(myReaderT("PROGR_APPALTO"), "")
                Else
                    ControllaSALsuccessivi = par.IfNull(myReaderT("PROGR_APPALTO"), "")
                End If
            End While
            myReaderT.Close()


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
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function


    Function PagamentoStampato() As Boolean
        Dim FlagConnessione As Boolean

        PagamentoStampato = False

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            'CONTROLLO SE L'ATTESTATAO DI PAGAMENTO è STATO STAMPATO
            par.cmd.CommandText = "select count(*) from SISCOM_MI.EVENTI_PAGAMENTI where COD_EVENTO='F98' and ID_PAGAMENTO=" & vIdPagamento

            Dim myReaderT As Oracle.DataAccess.Client.OracleDataReader
            myReaderT = par.cmd.ExecuteReader

            If myReaderT.Read Then
                If par.IfNull(myReaderT(0), 0) > 0 Then
                    PagamentoStampato = True
                End If
            End If
            myReaderT.Close()


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
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function




    Protected Sub imgStampa_Click(sender As Object, e As System.EventArgs) Handles imgStampa.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            If Me.txtElimina.Value = "1" Then
                Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
                Dim NOME As String = par.cmd.ExecuteScalar
                If Not String.IsNullOrEmpty(NOME) Then
                    If Not cmbStatoSAL.SelectedValue.Equals("0") Then
                        idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA") & "," & par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
                        par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN ( " & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & "Order BY ID DESC"
                        NOME = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                        If String.IsNullOrEmpty(NOME) Then
                            HiddenFieldRielabPagam.Value = "0"
                            Pdf_Pagamento()
                        Else
                            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../ALLEGATI/ORDINI/" & NOME & "','AttPagamento','');self.close();", True)
                        End If
                    Else
                        RadWindowManager1.RadAlert("Impossibile stampare il CDP!<br />Salvare prima il Pagamento", 300, 150, "Attenzione", "", "null")
                    End If
                Else
                    RadWindowManager1.RadAlert("Impossibile stampare il CDP!<br />Inserire prima un <strong>SAL firmato</strong>", 300, 150, "Attenzione", "", "null")
                End If


            Else
                        CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
            End If
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        End Try
    End Sub





    Private Sub Pdf_Pagamento()
        Dim sStr1 As String
        Dim FlagConnessione As Boolean

        Dim perc_sconto, perc_iva, perc_oneri As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, risultato4Tot, risultato3Tot, importoDaPagare As Decimal


        Try


            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            Dim myReaderP As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where  ID=" & vIdPagamento
            myReaderP = par.cmd.ExecuteReader

            If myReaderP.Read Then
                If par.IfNull(myReaderP("DATA_STAMPA"), "") = "" Then
                    myReaderP.Close()

                    'UPDATE PAGAMENTI
                    par.cmd.Parameters.Clear()
                    par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set ID_STATO=1, DATA_STAMPA=" & Format(Now, "yyyyMMdd") & " where ID=" & vIdPagamento
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = ""


                    radNotificationNote.Text = "Il pagamento è stato emesso e storicizzato!"
                    RadNotificationNote.Show()

                    Me.txtModificato.Text = 0
                    Me.txtStatoPagamento.Value = 1

                    Me.txtSTATO.Value = 3
                Else
                    myReaderP.Close()
                End If
            Else
                myReaderP.Close()
            End If

            Me.txtModificato.Text = 0

            'UPDATE EVENTI PAGAMENTI
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & vIdPagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','Stampa Mandato di Pagamento')"
            par.cmd.ExecuteNonQuery()

            '****************************************************

            par.cmd.CommandText = ""

            Me.cmbStatoPAGAMENTO.SelectedValue = 3



            'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoMANU2.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()



            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            par.cmd.Parameters.Clear()
            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(vIdPagamento))
            'PAGAMENTI.IMPORTO_NO_IVA
            '& " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"", "
            '& "   and  PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) " _
            '     & "   and  PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID (+) "
            ',SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO
            sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.PROGR_APPALTO,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_SAL,PAGAMENTI.DATA_STAMPA,PAGAMENTI.DESCRIZIONE," _
                        & " PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.CONTO_CORRENTE," _
                        & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE, FORNITORI.ID as ID_FORNITORE, " _
                        & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                        & " APPALTI.FL_RIT_LEGGE , " _
                        & "(SELECT descrizione FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = pagamenti.id_tipo_modalita_pag) AS tipo_modalita,(SELECT descrizione FROM siscom_mi.TIPO_pagamento WHERE ID = pagamenti.id_tipo_pagamento) AS tipo_pag,pagamenti.data_scadenza " _
                 & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                 & " where  PAGAMENTI.ID=" & vIdPagamento _
                 & "   and  PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                 & "   and  PAGAMENTI.ID_APPALTO=APPALTI.ID (+) "



            par.cmd.CommandText = sStr1
            myReaderP = par.cmd.ExecuteReader

            If myReaderP.Read Then
                'contenuto = Replace(contenuto, "$chiamante$", "") '"CONTRATTO:")

                contenuto = Replace(contenuto, "$anno$", myReaderP("ANNO"))
                contenuto = Replace(contenuto, "$progr$", myReaderP("PROGR"))
                contenuto = Replace(contenuto, "$annoSAL$", myReaderP("ANNO"))
                contenuto = Replace(contenuto, "$progrSAL$", myReaderP("PROGR_APPALTO"))

                Me.lblPROG_Pagamento.Text = par.IfNull(myReaderP("PROGR"), "") & "/" & par.IfNull(myReaderP("ANNO"), "")

                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReaderP("DATA_EMISSIONE"), "-1")))
                contenuto = Replace(contenuto, "$data_sal$", par.FormattaData(par.IfNull(myReaderP("DATA_SAL"), "-1")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReaderP("DATA_STAMPA"), "")))

                'contenuto = Replace(contenuto, "$dettagli_chiamante$", "") ' "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(par.IfNull(myReader1("DATA_REPERTORIO"), "-1")) & " " & myReader1("APPALTI_DESC"))

                contenuto = Replace(contenuto, "$contratto$", "N." & myReaderP("NUM_REPERTORIO") & " del " & par.FormattaData(myReaderP("DATA_REPERTORIO")) & " " & myReaderP("APPALTI_DESC"))

                contenuto = Replace(contenuto, "$CIG$", par.IfNull(myReaderP("CIG"), ""))
                contenuto = Replace(contenuto, "$conto_corrente$", par.IfNull(myReaderP("CONTO_CORRENTE"), ""))

                contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReaderP("tipo_modalita"), ""))
                contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReaderP("tipo_pag"), ""))
                contenuto = Replace(contenuto, "$datascadenza$", par.FormattaData(par.IfNull(myReaderP("data_scadenza"), "")))
                contenuto = Replace(contenuto, "$descrpag$", par.IfNull(myReaderP("DESCRIZIONE"), ""))

                'FORNITORI
                Dim sFORNITORI As String = ""
                If par.IfNull(myReaderP("RAGIONE_SOCIALE"), "") = "" Then
                    If par.IfNull(myReaderP("COD_FORNITORE"), "") = "" Then
                        sFORNITORI = par.IfNull(myReaderP("COGNOME"), "") & " - " & par.IfNull(myReaderP("NOME"), "")
                    Else
                        sFORNITORI = par.IfNull(myReaderP("COD_FORNITORE"), "") & " - " & par.IfNull(myReaderP("COGNOME"), "") & " - " & par.IfNull(myReaderP("NOME"), "")
                    End If

                Else
                    If par.IfNull(myReaderP("COD_FORNITORE"), "") = "" Then
                        sFORNITORI = par.IfNull(myReaderP("RAGIONE_SOCIALE"), "")
                    Else
                        sFORNITORI = par.IfNull(myReaderP("COD_FORNITORE"), "") & " - " & par.IfNull(myReaderP("RAGIONE_SOCIALE"), "")
                    End If
                End If
                contenuto = Replace(contenuto, "$fornitoreIntestazione$", sFORNITORI)
                'INDIRIZZO FORNITORE
                Dim sIndirizzoFornitore1 As String = ""
                Dim sComuneFornitore1 As String = ""
                par.cmd.CommandText = "select TIPO,INDIRIZZO,CIVICO,CAP,COMUNE " _
                                    & " from   SISCOM_MI.FORNITORI_INDIRIZZI" _
                                    & " where ID_FORNITORE=" & par.IfNull(myReaderP("ID_FORNITORE"), 0)

                Dim myReaderTT As Oracle.DataAccess.Client.OracleDataReader
                myReaderTT = par.cmd.ExecuteReader
                While myReaderTT.Read

                    sIndirizzoFornitore1 = par.IfNull(myReaderTT("TIPO"), "") _
                                        & " " & par.IfNull(myReaderTT("INDIRIZZO"), "") _
                                        & " " & par.IfNull(myReaderTT("CIVICO"), "")

                    sComuneFornitore1 = par.IfNull(myReaderTT("CAP"), "") _
                                        & " " & par.IfNull(myReaderTT("COMUNE"), "")

                End While
                myReaderTT.Close()
                contenuto = Replace(contenuto, "$fornitore_indirizzo$", sIndirizzoFornitore1)
                contenuto = Replace(contenuto, "$comuneIntestazione$", sComuneFornitore1)



                'IBAN **************************************************
                par.cmd.CommandText = "select IBAN from SISCOM_MI.FORNITORI_IBAN " _
                                   & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & par.IfNull(myReaderP("ID_APPALTO"), 0) & ")"

                Dim myReaderBP As Oracle.DataAccess.Client.OracleDataReader
                myReaderBP = par.cmd.ExecuteReader

                While myReaderBP.Read
                    sFORNITORI = sFORNITORI & "<br/>" & par.IfNull(myReaderBP("IBAN"), "")
                End While
                myReaderBP.Close()
                contenuto = Replace(contenuto, "$fornitori$", sFORNITORI)
                '*********************************************************

                importoPRE = 0
                oneriPRE = 0
                risultato1PRE = 0
                astaPRE = 0
                risultato2PRE = 0
                ritenutaPRE = 0
                risultato3PRE = 0
                ivaPRE = 0
                risultato4PRE = 0
                risultatoImponibilePRE = 0

                'RIEPILOGO SAL
                par.cmd.CommandText = "select PRENOTAZIONI.* " _
                                   & " from   SISCOM_MI.PRENOTAZIONI" _
                                   & " where PRENOTAZIONI.ID_PAGAMENTO=" & vIdPagamento

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                myReader2 = par.cmd.ExecuteReader()
				Dim id_appalto As String = "-1"
                While myReader2.Read
                    CalcolaImportiStampaPagamento(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), par.IfNull(myReaderP("FL_RIT_LEGGE"), 0), par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))
					id_appalto = par.IfNull(myReader2("ID_APPALTO"), "-1")
                End While
                myReader2.Close()


                contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(importoPRE, 0), "", "##,##0.00"))

                'modifica marco/pepep 05/01/2011
                par.cmd.CommandText = "select rit_legge_ivata from siscom_mi.prenotazioni where id IN ( " & vIdPrenotazioni & ") and id_pagamento = " & vIdPagamento
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim ritDb As Decimal
                Dim A As Integer = 0
                While lettore.Read
                    ritDb = par.IfNull(lettore("rit_legge_ivata"), 0)
                    If ritDb <> 0 Then
                        A = 1
                    End If
                End While
                lettore.Close()
                If A <> 1 Then
                    ritenutaPRE = 0
                End If



                Dim S2 As String = "<table style='width:100%;'>"
                S2 = S2 & "<tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'> </td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato4PRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Oneri di sicurezza €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(oneriPRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A lordo esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato1PRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ribasso d'asta €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(astaPRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto esclusi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato2PRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultato3PRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Ritenuta di legge 0,5% (con IVA) €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ritenutaPRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Imponibile (al netto trattenute) €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(risultatoImponibilePRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>IVA €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaPRE, "", "##,##0.00") & "</td>"
                S2 = S2 & "</tr><tr>"
				Dim percIva As String = "0"
				If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, 0) > 0 Then
					Dim totaleIVA As String = ""
					par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) as PERC_IVA FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & id_appalto & ")"
					Dim lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
					If lettore1.Read Then
						percIva = par.IfNull(lettore1("PERC_IVA"), "0")
					End If
					lettore1.Close()
					totaleIVA = (CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto"), TextBox).Text) * CDec(percIva) / 100)
					S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Recupero anticipazione contrattuale €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, "", "##,##0.00") & "</td>"
					S2 = S2 & "</tr><tr>"
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Importo IVA recupero (" & IsNumFormat(percIva, "", "##,##0") & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(totaleIVA, "", "##,##0.00") & "</td>"
					S2 = S2 & "</tr><tr>"
				End If
                S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>A netto compresi oneri e IVA €</td>"
                S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importoPRE, "", "##,##0.00") & "</td>"

                Dim penale As Decimal = CDec(CType(Tab_SAL_Riepilogo.FindControl("txtPenale"), TextBox).Text)
                If penale > 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(-penale, "", "##,##0.00") & "</td>"
                '    S2 = S2 & "</tr><tr>"
                End If




                If par.IfEmpty(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text, 0) > 0 Then
					S2 = S2 & "</tr><tr>"
					S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importoPRE - (CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text) + (CDec(CType(Tab_SAL_Riepilogo.FindControl("TextBoxImportoTrattenuto2"), TextBox).Text) * CDec(percIva) / 100)) - penale, "", "##,##0.00") & "</td>"
                ElseIf penale > 0 Then
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale compreso IVA €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importoPRE - penale, "", "##,##0.00") & "</td>"
                End If

                S2 = S2 & "</tr></table>"

                Dim T As String = "<table style='width:100%;'>"
                T = T & "<tr>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                T = T & "</tr></table>"

                contenuto = Replace(contenuto, "$dettagli$", T)
                ''****************************
                Dim TestoGrigliaINTESTAZIONE As String = "" ' "<p style='page-break-after: always'>&nbsp;</p>"

                TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<table cellspacing='0' style='width:50%; border: 1px solid black;border-collapse: collapse;' >"
                TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >A netto compresi oneri</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultato3PRE, "", "##,##0.00") & "</td>" _
                                              & "</tr>" _
                                              & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Ritenuta di legge 0,5 % (senza IVA)</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(ritenutaNoIvaT, "", "##,##0.00") & "</td>" _
                                              & "</tr>" _
                                              & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile (al netto delle trattenute) </td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px '  >€ " & IsNumFormat(risultatoImponibilePRE, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                'If rimborsoT > 0 Then
                '    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                '                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Imponibile rimborsi</td>" _
                '                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(imponibile_rimborso, "", "##,##0.00") & "</td>" _
                '                              & "</tr>"
                'End If
                If penale > 0 Then
                    TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "<tr style='height: 30px;'>" _
                                              & "<td align='left' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >Penale</td>" _
                                              & "<td align='right' style='width:50%;border: 1px solid #000000; font-family: arial; font-size: 10pt;heigth:10px ' >€ " & IsNumFormat(-penale, "", "##,##0.00") & "</td>" _
                                              & "</tr>"
                End If

                TestoGrigliaINTESTAZIONE = TestoGrigliaINTESTAZIONE & "</table>"
                contenuto = Replace(contenuto, "$grigliaIntestazione$", TestoGrigliaINTESTAZIONE)


                '*********** DETTAGLIO GRIGLIA VOCI BP
                'TestoPagina = TestoPagina & "</table>"
                Dim TestoGrigliaBP As String = "" '"<p style='page-break-before: always'>&nbsp;</p>"

                TestoGrigliaBP = TestoGrigliaBP & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 10pt; font-weight: bold'>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>CODICE BP</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width: 10%; border-bottom-width: 1px; border-bottom-color: #000000'>ANNO BP</td>" _
                                          & "<td align='left' style='border-bottom-style: dashed; width: 60%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE BP</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed; width: 20%; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td>" _
                                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                          & "</tr>"


                'ESTRAGGO TUTTE LE VOCI BP DIVERSE
                risultato4Tot = 0

                'par.cmd.CommandText = "select distinct(ID_VOCE) from SISCOM_MI.PF_VOCI_IMPORTO " _
                '                  & " where ID in (select ID_VOCE_PF_IMPORTO from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & vIdPagamento & ")"

                par.cmd.CommandText = "select distinct(ID_VOCE_PF) as ID_VOCE, " _
                                        & "(SELECT DISTINCT RTRIM (LTRIM (SUBSTR (inizio, 1, 4))) AS annoBp FROM siscom_mi.T_ESERCIZIO_FINANZIARIO  WHERE id IN " _
                                        & "(select id_esercizio_finanziario from siscom_mi.pf_main where id in (SELECT DISTINCT id_piano_finanziario FROM siscom_mi.pf_voci WHERE id = PRENOTAZIONI.ID_VOCE_PF))" _
                                        & ") AS ANNO " _
                    & "from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & vIdPagamento


                myReaderBP = par.cmd.ExecuteReader

                While myReaderBP.Read
                    'X OGNI TIPO DI VOCE
                    par.cmd.CommandText = "select PRENOTAZIONI.* " _
                                      & " from   SISCOM_MI.PRENOTAZIONI " _
                                      & " where ID_PAGAMENTO=" & vIdPagamento _
                                      & "   and ID_VOCE_PF=" & par.IfNull(myReaderBP("ID_VOCE"), 0)

                    ' & "   and ID_VOCE_PF_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO where ID_VOCE=" & par.IfNull(myReaderBP("ID_VOCE"), 0) & ")"
                    ' & "   and SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "


                    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                    myReaderB = par.cmd.ExecuteReader

                    While myReaderB.Read
                        '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0

                        If par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) <> 0 Then

                            If par.IfNull(myReaderB("ID_VOCE_PF_IMPORTO"), 0) > 0 Then

                                sStr1 = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                                    & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                                    & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReaderB("ID_VOCE_PF_IMPORTO"), 0) _
                                    & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & par.IfNull(myReaderB("ID_APPALTO"), 0) _
                                    & "  and   APPALTI.ID=" & par.IfNull(myReaderB("ID_APPALTO"), 0)


                                Dim myReaderB2 As Oracle.DataAccess.Client.OracleDataReader
                                par.cmd.CommandText = sStr1
                                myReaderB2 = par.cmd.ExecuteReader

                                If myReaderB2.Read Then

                                    'perc_oneri = par.IfNull(myReaderB2("PERC_ONERI_SIC_CAN"), 0)

                                    'D3= D1(-(D1 * D2 / 100))
                                    'D9= D4*100/D3
                                    Dim D3 As Decimal = 0
                                    D3 = par.IfNull(myReaderB2("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderB2("IMPORTO_CANONE"), 0) * par.IfNull(myReaderB2("SCONTO_CANONE"), 0)) / 100)

                                    perc_oneri = (par.IfNull(myReaderB2("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3


                                    perc_sconto = par.IfNull(myReaderB2("SCONTO_CANONE"), 0)

                                    If par.IfNull(myReaderB("PERC_IVA"), -1) = -1 Then
                                        perc_iva = par.IfNull(myReaderB2("IVA_CANONE"), 0)
                                    Else
                                        perc_iva = par.IfNull(myReaderB("PERC_IVA"), 0)
                                    End If

                                    risultato3 = ((par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) * 100) / (100 + perc_iva))

                                    'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
                                    If par.IfNull(myReaderB2("FL_RIT_LEGGE"), 0) = 1 Then
                                        ritenuta = (risultato3 * 0.5) / 100
                                        ritenuta = Round(ritenuta, 2)
                                        'ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)

                                    Else
                                        ritenuta = 0
                                    End If

                                    oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                                    oneri = Round(oneri, 2)

                                    risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK

                                    asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                                    asta = Round(asta, 2)

                                    risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)

                                    risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)

                                    importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                                    importoDaPagare = Round(importoDaPagare, 2)

                                    'risultato3Tot = risultato3Tot + par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0)
                                    risultato3Tot = risultato3Tot + importoDaPagare


                                    iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                                    iva = Round(iva, 2)



                                    Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                                    par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderBP("ID_VOCE"), 0)
                                    myReaderB3 = par.cmd.ExecuteReader

                                    If myReaderB3.Read Then
                                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("anno"), "") & "</td>" _
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                                                                        & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(importoDaPagare, "", "##,##0.00") & "</td>" _
                                                                        & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                                                                        & "</tr>"

                                    End If
                                    myReaderB3.Close()

                                End If
                                myReaderB2.Close()
                            Else

                                Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                                par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderB("ID_VOCE_PF"), 0)
                                myReaderB3 = par.cmd.ExecuteReader

                                If myReaderB3.Read Then

                                    risultato3Tot = risultato3Tot + par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0)
                                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                                                                    & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0), "", "##,##0.00") & "</td>" _
                                                                    & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                                                                    & "</tr>"

                                End If
                                myReaderB3.Close()

                            End If

                        End If


                    End While
                    myReaderB.Close()

                End While
                myReaderBP.Close()


                par.cmd.CommandText = "select sum(anticipo_contrattuale) " _
                                      & " from   SISCOM_MI.PRENOTAZIONI " _
                                      & " where ID_PAGAMENTO=" & vIdPagamento _
                                      & " AND PRENOTAZIONI.ID_sTATO<>-3 "

                Dim anticipoContrattuale As Decimal = par.IfNull(par.cmd.ExecuteScalar, 0)

				If anticipoContrattuale <> 0 Then


                TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " RECUPERO ANTICIPAZIONE CONTRATTUALE   : " & IsNumFormat(anticipoContrattuale + (anticipoContrattuale * percIva / 100), "", "##,##0.00") & "</td>" _
                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                          & "</tr>"
                End If


                TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                          & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                          & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " TOTALE   : " & IsNumFormat(risultato3Tot - anticipoContrattuale, "", "##,##0.00") & "</td>" _
                          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                          & "</tr>"

                contenuto = Replace(contenuto, "$grigliaBP$", TestoGrigliaBP)
                '********************************


                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "") 'NumeroInLettere(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)))

                'contenuto = Replace(contenuto, "$dettaglio$", "SPESE")

                'contenuto = Replace(contenuto, "$cod_capitolo$", "") 'par.IfNull(myReader1("COD_VOCE"), ""))
                'contenuto = Replace(contenuto, "$voce_pf$", "") ' par.IfNull(myReader1("DESC_VOCE"), ""))
                'contenuto = Replace(contenuto, "$finanziamento$", "Gestione Comune di Milano")

                par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim chiamante2 As String = ""
                If lett.Read Then
                    chiamante2 = par.IfNull(lett(0), "")
                End If
                lett.Close()
                contenuto = Replace(contenuto, "$chiamante2$", chiamante2)
                par.cmd.CommandText = "SELECT INITCAP(GESTORI_ORDINI.DESCRIZIONE) FROM SISCOM_MI.GESTORI_ORDINI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI " _
                        & " WHERE APPALTI.ID_gESTORE_ORDINI=GESTORI_ORDINI.ID AND PAGAMENTI.ID_APPALTO=APPALTI.ID AND PAGAMENTI.ID=" & txtID_PAGAMENTI.Value
                lett = par.cmd.ExecuteReader
                Dim gestore As String = ""
                If lett.Read Then
                    gestore = par.IfNull(lett(0), "")
                End If
                lett.Close()
                contenuto = Replace(contenuto, "$proponente$", gestore)
                contenuto = Replace(contenuto, "$grigliaM$", "")


            End If
            myReaderP.Close()


            '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            nomefile = par.NomeFileManut("CDP", vIdPagamento) & ".pdf"

            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

            'Dim i As Integer = 0
            'For i = 0 To 10000
            'Next



            'GIANCARLO 16-02-2017
            'inserimento della stampa cdp negli allegati
            Dim descrizione As String = "Stampa pagamento"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
            'Imposto le vecchie rielaborazioni a 2...per barrare il nome
            par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                & "SET STATO = 2 " _
                                & "WHERE " _
                                & "TIPO = " & idTipoOggetto _
                                & "AND ID_OGGETTO = " & vIdPagamento
            par.cmd.ExecuteNonQuery()
            If HiddenFieldRielabPagam.Value = "1" Then
                descrizione = "Stampa rielaborazione pagamento"
                idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA RIELABORAZIONE PAGAMENTO DI SISTEMA")
                'Imposto le vecchie rielaborazioni a 2...per barrare il nome
                par.cmd.CommandText = "UPDATE SISCOM_MI.ALLEGATI_WS " _
                                    & "SET STATO = 2 " _
                                    & "WHERE " _
                                    & "TIPO = " & idTipoOggetto _
                                    & "AND ID_OGGETTO = " & vIdPagamento
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato.Value
            Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
           
            par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato.Value, vIdPagamento, "../../../ALLEGATI/ORDINI/")
            ' COMMIT
            par.myTrans.Commit()
            SettaValori()


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');self.close();", True)


        Catch ex As Exception
            CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            Page.Dispose()

            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub

    Private Sub AggiornamentoPrenotazioni()
        Dim scriptblock As String = ""
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            End If
            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_APPROVATO=IMPORTO_PRENOTATO WHERE PRENOTAZIONI.ID IN (" & vIdPrenotazioni & " ) "
            par.cmd.ExecuteNonQuery()


            'Catch EX1 As Oracle.DataAccess.Client.OracleException
            '    If EX1.Number = 54 Then
            '        'par.OracleConn.Close()
            '        scriptblock = "<script language='javascript' type='text/javascript'>" _
            '        & "alert('Scheda Pagamento aperta da un altro utente. Non è possibile effettuare modifiche!');" _
            '        & "</script>"
            '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
            '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
            '        End If

            '        If FlagConnessione = True Then
            '            par.cmd.Dispose()
            '            par.OracleConn.Close()
            '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '            FlagConnessione = False
            '        End If

            '        CType(Me.Page.FindControl("SOLO_LETTURA"), TextBox).Text = "1"
            '        Me.txtVisualizza.Value = 1 'SOLO LETTURA
            '        FrmSolaLettura()


            '    Else
            '        If FlagConnessione = True Then
            '            par.cmd.Dispose()
            '            par.OracleConn.Close()
            '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '            FlagConnessione = False
            '        End If

            '        Session.Item("LAVORAZIONE") = "0"
            '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
            '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            '    End If

            SettaValoriInApprovazione()

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

    Private Sub ControlloPulsanti()
        If txtSTATO.Value = "0" Then
            'DA APPROVARE
            lblPagamento.Visible = False
            lblPROG_Pagamento.Visible = False

            btnSalva.Enabled = False
            btnApprovazione.Enabled = True

            btnAnnulla.Enabled = False
            btnStampaSAL.Enabled = False
            imgStampa.Enabled = False

            'document.getElementById('Tab_SAL_Ripartizioni_btnApri1').style.visibility = 'hidden';
        End If
        If txtSTATO.Value = "1" Then
            'APPROVATA
            lblPagamento.Visible = True
            lblPROG_Pagamento.Visible = True
            btnSalva.Enabled = True
            btnApprovazione.Enabled = False
            btnAnnulla.Enabled = False
            btnStampaSAL.Enabled = True
            imgStampa.Enabled = False
            'document.getElementById('Tab_SAL_Ripartizioni_btnApri1').style.visibility = 'visible';
        End If
        If txtSTATO.Value = "2" Then
            'STAMPATO il SAL
            lblPagamento.Visible = True
            lblPROG_Pagamento.Visible = True
            btnSalva.Enabled = True
            btnApprovazione.Enabled = False
            btnAnnulla.Enabled = True
            btnStampaSAL.Enabled = True
            imgStampa.Enabled = True
            'document.getElementById('Tab_SAL_Ripartizioni_btnApri1').style.visibility = 'visible';
        End If
        If txtSTATO.Value = "3" Then
            'STAMPATO IL PAGAMENTO
            lblPagamento.Visible = True
            lblPROG_Pagamento.Visible = True
            btnSalva.Visible = True
            btnApprovazione.Visible = False
            btnAnnulla.Enabled = False
            btnStampaSAL.Enabled = True
            imgStampa.Enabled = True
            'document.getElementById('Tab_SAL_Ripartizioni_btnApri1').style.visibility = 'hidden';
        End If
        If txtVisualizza.Value = "1" Then
            'SOLO LETTURA
            btnSalva.Enabled = False
            btnApprovazione.Enabled = False
            btnAnnulla.Enabled = False
            If txtSTATO.Value = "3" Then
                btnStampaSAL.Enabled = True
                imgStampa.Enabled = True
            Else
                btnStampaSAL.Enabled = False
                imgStampa.Enabled = False
            End If
            ChkRipartizioni.Visible = False
            'document.getElementById('Tab_SAL_Ripartizioni_btnApri1').style.visibility = 'hidden';
        End If
        If txtVisualizza.Value = "2" Then
            'SOLO LETTURA CHIAMATA DIRETTA
            btnSalva.Enabled = False
            btnApprovazione.Enabled = False

            btnAnnulla.Enabled = False

            btnStampaSAL.Enabled = False
            imgStampa.Enabled = False

            ChkRipartizioni.Visible = False
        End If
    End Sub

    Private Sub NascondiTab()
        RadTabStrip.Tabs.FindTabByValue("Elenco_Ripartizioni").Visible = False
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
    Protected Sub btnRielbSal_Click(sender As Object, e As System.EventArgs) Handles btnRielbSal.Click
        CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"

        If IsNothing(txtDataSal.SelectedDate) And IsNothing(txtDataDel.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la Data del SAL e la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        ElseIf IsNothing(txtDataSal.SelectedDate) And IsNothing(txtDataDel.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la Data del SAL!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        ElseIf IsNothing(txtDataSal.SelectedDate) And IsNothing(txtDataDel.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la Data di Emissione del Pagamento!", 300, 150, "Attenzione", "", "null")
            Exit Sub
        End If
        If par.OracleConn.State = Data.ConnectionState.Closed Then
            par.OracleConn.Open()
            par.SettaCommand(par)


        End If

        par.cmd.Parameters.Clear()
        Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL DI SISTEMA")
        '' RIPRENDO LA CONNESSIONE
        'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleConnection)
        'par.SettaCommand(par)
        ''RIPRENDO LA TRANSAZIONE
        'If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
        '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & txtConnessione.Text), Oracle.DataAccess.Client.OracleTransaction)
        '    '‘par.cmd.Transaction = par.myTrans
        'End If
        par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento
        Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
        If Not String.IsNullOrEmpty(nome) Then
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            HiddenFieldRielabPagam.Value = "1"
            Pdf_SAL()
        Else
            RadWindowManager1.RadAlert("Impossibile rielaborare!\nStampare prima il SAL!", 300, 150, "Attenzione", "", "null")
        End If

    End Sub

    Protected Sub btnRielaboraPagamento_Click(sender As Object, e As System.EventArgs) Handles btnRielaboraPagamento.Click
        '*******************APERURA CONNESSIONE*********************
        ' RIPRENDO LA CONNESSIONE
        HiddenFieldRielabPagam.Value = "1"
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA SAL FIRMATO", TipoAllegato.Value)
        par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO IN (" & idTipoOggetto & ") AND STATO=0 AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento & " ORDER BY ID DESC"
        Dim NOME As String = par.cmd.ExecuteScalar
        If Not String.IsNullOrEmpty(NOME) Then
            If Not cmbStatoSAL.SelectedValue.Equals("0") Then
                idTipoOggetto = par.getIdOggettoTipoAllegatiWs("STAMPA PAGAMENTO DI SISTEMA")
                par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdPagamento
                NOME = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
                If Not String.IsNullOrEmpty(nome) Then
                    CType(Me.Page.FindControl("USCITA"), TextBox).Text = "0"
                    HiddenFieldRielabPagam.Value = "1"
                    Pdf_Pagamento()
                Else
                    RadWindowManager1.RadAlert("Impossibile rielaborare!\nStampare prima il CDP!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Impossibile stampare il CDP!<br />Salvare prima il SAL", 300, 150, "Attenzione", "", "null")
            End If
        Else
            RadWindowManager1.RadAlert("Impossibile rielaborare il CDP!<br />Inserire prima un <strong>sal firmato</strong>", 300, 150, "Attenzione", "", "null")
        End If

    End Sub

    Private Sub caricaImportoResiduoDaTrattenere()
        Dim FlagConnessione As Boolean
        Try
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
                FlagConnessione = True
            End If
            Dim stringaPagamenti As String = ""
            For Each elemento In lstListaRapporti
                If stringaPagamenti = "" Then
                    stringaPagamenti = elemento.STR
                Else
                    stringaPagamenti &= "," & elemento.STR
                End If
            Next
            Dim importoTotale As Decimal = 0
            'par.cmd.CommandText = "SELECT SUM(IMPORTO) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & txtID_APPALTO.Value & ")"
            Dim condizioneVoce As String = ""
            par.cmd.CommandText = "SELECT TIPO,ID_PF_VOCE_IMPORTO AS VOCE FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(Select ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & ")"
            Dim lettoreVoce As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettoreVoce.Read Then
                tipo.Value = par.IfNull(lettoreVoce("TIPO"), 0)
                voce.Value = par.IfNull(lettoreVoce("VOCE"), 0)
            End If
            lettoreVoce.Close()
            If tipo.Value = 1 Then
                condizioneVoce = " AND ID_PF_VOCE_IMPORTO in " _
                    & " (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID=ID_OLD START WITH ID IN (" & voce.Value & ") " _
                    & " UNION " _
                    & " SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR ID_OLD=ID START WITH ID IN (" & voce.Value & ")" _
                    & ")"
            End If
            par.cmd.CommandText = "SELECT SUM(IMPONIBILE) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto & ")  " & condizioneVoce
            importoTotale = par.IfNull(par.cmd.ExecuteScalar, 0)
            Dim anticipoTrattenuto As Decimal = 0
            If stringaPagamenti <> "" Then
                par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & txtID_APPALTO.Value & ")) AND PRENOTAZIONI.ID_sTATO<>-3"
                anticipoTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)
            End If
            Dim numeroRate As Integer = 0
            par.cmd.CommandText = "SELECT N_RATE_ANTICIPO,FL_ANTICIPO FROM SISCOM_MI.APPALTI WHERE ID=" & sValoreAppalto
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore.Read Then
                numeroRate = par.IfNull(lettore("N_RATE_ANTICIPO"), 0)
                tipoAnticipo.value = par.IfNull(lettore("FL_ANTICIPO"), 0)
            End If
            lettore.Close()
            ImportoResiduoDaTrattenere.Value = importoTotale - anticipoTrattenuto
            Dim importoRata As Decimal = 0
            If numeroRate <> 0 Then
                importoRata = Math.Round(importoTotale / numeroRate, 2)
            End If
            importoDaProporre.Value = Math.Min(CDec(ImportoResiduoDaTrattenere.Value), importoRata)
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
End Class
