Imports System.Collections
Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Collections.Generic
Imports Telerik.Web.UI
Imports System.Math


Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_Appalti
    Inherits PageSetIdMode
    Dim par As New CM.Global
    '*********************************
    Dim sUnita(19) As String
    Dim sDecina(9) As String

    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""
    Public Tabber7 As String = ""
    Public Tabber8 As String = ""

    Public Tabber9 As String = ""
    Public Tabber10 As String = ""
    Public TabberHide As String = ""

    Public numero As String
    Public CIG As String
    Public datadal As String
    Public dataal As String
    Public fornitore As String
    Public lotto As String
    'Public tipo As String
    Public idlotto As String

    Dim importoPRE, oneriPRE, risultato1PRE, astaPRE, risultato2PRE, ritenutaPRE, risultato3PRE, ivaPRE, risultato4PRE, risultatoImponibilePRE As Decimal


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Try
            Response.Expires = 0
            'Dim Str As String
            'Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:500; border:1px dashed #660000;"
            'Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
            'Str = Str & "<" & "/div>"


            'Response.Write(Str)

            If Not IsPostBack Then
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("Contratto")
                'Panel1.Visible = False
                RadTabStrip.Tabs.FindTabByValue("ElencoPrezzi").Visible = False
                TipoAllegato.Value = par.getIdOggettoAllegatiWs("Contratto")
                HFGriglia.Value = CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).ClientID & "," _
                    & CType(Tab_Fornitori1.FindControl("DataGridFornitori"), RadGrid).ClientID & "," _
                    & CType(Tab_Composizione1.FindControl("DataGridComposizione"), RadGrid).ClientID & "," _
                    & CType(Tab_SLA.FindControl("DataGridSLA"), RadGrid).ClientID & "," _
                    & CType(Tab_VariazioneImporti1.FindControl("DataGridVCan"), RadGrid).ClientID

                HFTAB.Value = "tab1,tab2,tab3,tab6,tab7,tab8,tab9,tab11,tab12,tab13"
                HFAltezzaTab.Value = 410
                HFAltezzaFGriglie.Value = "480,480,480,480,500"
                Session.Remove("ATI_FORNITORI")
                'Response.Flush()

                numero = UCase(Request.QueryString("NU"))
                ' tipo = UCase(Request.QueryString("TI"))
                fornitore = UCase(Request.QueryString("FO"))
                idfornitore.Value = UCase(Request.QueryString("F")) 'id fornitore da showdialog
                lotto = UCase(Request.QueryString("LO"))
                datadal = UCase(Request.QueryString("DAL"))
                dataal = UCase(Request.QueryString("AL"))
                x.Value = UCase(Request.QueryString("X")) 'serve per sapere se è aperto come finestra di dialogo

                '******PEPPE MODIFY 30/09/2010

                Me.txtIdPianoFinanziario.Value = -1

                If lotto = "" Then ' se non lo passo dalla ricerca
                    idlotto = UCase(Request.QueryString("IDL")) 'lo ricavo dalla scelta in fase di inserimento o dalla showdialog
                Else
                    idlotto = lotto
                End If

                vIdAppalti = 0
                If UCase(Request.QueryString("A")) = "" Then 'vedo da dove ricevo id appalto
                    vIdAppalti = Session.Item("IDA")
                Else
                    vIdAppalti = UCase(Request.QueryString("A"))
                End If

                If x.Value <> "0" Then
                    tipo = "_self"
                Else
                    tipo = ""
                End If

                '***SETTAGGIOI PROPERTY PER IDCONNESSIONE
                lIdConnessione = Format(Now, "yyyyMMddHHmmss")
                '***SETTA ANCHE UN HIDDEN FIELD CON L'ID DELLA CONNESSIONE...NN SO ANCORA IL PERCHE'16/02/2011 ORE 9:05
                Me.txtConnessione.Value = CStr(lIdConnessione)


                Me.txtIdAppalto.Value = "-1"
                Me.txtidlotto.Value = idlotto

                '***INSTANZIA CONNESSIONE E LA MEMORIZZA IN UNA VARIABILE HTTP
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)


                CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "0"

                SettaggioCampi()
                'AbilitazioneOggetti(True)

                If vIdAppalti <> 0 Then
                    TabberHide = "tabbertab"
                    If Session.Item("OPERATORE") <> "*" Then
                        If Session.Item("BP_CC_V") = 0 Then
                            Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
                        Else
                            Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = True
                        End If
                    Else
                        Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = True
                    End If
                    txtindietro.Value = 0
                    VisualizzaDati()
                    Tabber1 = "tabbertabdefault"

                    If Me.lblStato.Text <> "ATTIVO" Then
                        Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
                        Me.btnTornBozza.Visible = False
                    Else
                        If Me.SOLO_LETTURA.Value = 0 Then
                            If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                                btnTornBozza.Visible = False
                            Else
                                btnTornBozza.Visible = True
                            End If
                        End If

                    End If
                Else
                    Dim dt As New Data.DataTable
                    dt.Columns.Add("ID_APPALTO")
                    dt.Columns.Add("SCADENZA")
                    dt.Columns.Add("IMPORTO")
                    dt.Columns.Add("ID_PF_VOCE_IMPORTO")
                    Session.Add("DTSCADENZE", dt)

                    Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
                    TabberHide = "tabbertabhide"
                    NascondiTab()
                    Me.txtIdAppalto.Value = -1
                    Me.txtidlotto.Value = idlotto
                    Me.btnElimina.Enabled = False
                    Tabber1 = "tabbertabdefault"
                    txtindietro.Value = 1
                End If

                Select Case UCase(lblStato.Text)
                    Case "BOZZA"
                        cmbGestore.Enabled = True
                    Case "ATTIVO"
                        If Session.Item("FL_SUPERDIRETTORE") = "1" Then
                            cmbGestore.Enabled = True
                        Else
                            cmbGestore.Enabled = False
                        End If
                    Case "CHIUSO"
                        cmbGestore.Enabled = False
                    Case Else
                        cmbGestore.Enabled = False
                End Select


                Dim CTRL As Control

                Me.txtnumero.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                Me.txtCup.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                Me.txtCIG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                txtdatarepertorio.DateInput.Attributes.Add("onkeydown", "CalendarDatePickerHide(this, event);")
                txtannoinizio.DateInput.Attributes.Add("onkeydown", "CalendarDatePickerHide(this, event);")
                txtannofine.DateInput.Attributes.Add("onkeydown", "CalendarDatePickerHide(this, event);")
                'DirectCast(Me.Tab_Appalto_generale.FindControl("txtDataInizioPag"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                Me.txtdescrizione.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")

                '*** FORM GENERALE
                For Each CTRL In Me.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                '*** FORM APPALTI
                For Each CTRL In Tab_Appalto_generale.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next

                'durata in mesi
                '*******PEPPE MODIFY 04/09/2010
                txtannofine.Attributes.Add("Onblur", "javascript:CalcolaDurata(document.getElementById('txtannoinizio').value,document.getElementById('txtannofine').value);")
                txtannoinizio.Attributes.Add("Onblur", "javascript:CalcolaDurata(document.getElementById('txtannoinizio').value,document.getElementById('txtannofine').value);")

                '*** FORM SERVIZI
                For Each CTRL In Me.Tab_Servizio.Controls
                    If TypeOf CTRL Is TextBox Then
                        DirectCast(CTRL, TextBox).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
                        DirectCast(CTRL, TextBox).Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                    ElseIf TypeOf CTRL Is DropDownList Then
                        DirectCast(CTRL, DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
                    ElseIf TypeOf CTRL Is CheckBoxList Then
                        DirectCast(CTRL, CheckBoxList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")
                    End If
                Next


                CType(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(Tab_Appalto_generale.FindControl("txtastaconsumo"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_Appalto_generale.FindControl("txtastaconsumo"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

                '*****PEPPE ADD HERE 31/09/2010

                CType(Tab_Appalto_generale.FindControl("txtfondopenali"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_Appalto_generale.FindControl("txtfondopenali"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                CType(Tab_Appalto_generale.FindControl("txtfondoritenute"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_Appalto_generale.FindControl("txtfondoritenute"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

                '' '' ''CType(Tab_Appalto_generale.FindControl("txtcosto"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                '' '' ''CType(Tab_Appalto_generale.FindControl("txtannoinizio"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
                '' '' ''CType(Tab_Appalto_generale.FindControl("txtannofine"), TextBox).Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


                '' '' ''CType(Tab_Meteoriche_Generale.FindControl("cmbQuadro"), DropDownList).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';document.getElementById('USCITA').value='1';")

                '' '' ''Response.Write("<script>parent.funzioni.Form1.Image3.src='../IMPIANTI/Immagini/Titolo_IMPIANTI_Meteoriche.png';</script>")

                If Session.Item("BP_CC_L") = 1 Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                    FrmSolaLettura()
                End If


                'Session.Add("STAPPALTO", Me.lblStato.Text)

            End If

            If btnFineContratto.Enabled = True Then
                ModificaRitenute.Value = "1"
            End If


        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If esciImmediato = "0" Then
            CompletaLoad()

            If Session.Item("FL_CP_VARIAZ_COMP") = "0" Then
                Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
            End If
            If lblStato.Text <> "CHIUSO" Then
                If Session.Item("FL_CP_MOD_PAGAMENTO") = "1" Then
                    cmbCondizionePagamento.Enabled = True
                    cmbModalitaPagamento.Enabled = True
                End If
            End If
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
    Public Property esciImmediato As String
        Get
            If Not (ViewState("esciImmediato") Is Nothing) Then
                Return CStr(ViewState("esciImmediato"))
            Else
                Return "0"
            End If
        End Get
        Set(ByVal value As String)
            ViewState("esciImmediato") = value
        End Set
    End Property
    Public Property vdtScadenze() As Object
        Get
            If Not (ViewState("par_vdtScadenze") Is Nothing) Then
                Return CObj(ViewState("par_vdtScadenze"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Object)
            ViewState("par_vdtScadenze") = value
        End Set

    End Property
    Public Property vIdAppalti() As Long
        Get
            If Not (ViewState("par_idAppalti") Is Nothing) Then
                Return CLng(ViewState("par_idAppalti"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idAppalti") = value
        End Set

    End Property
    Public Property vMonteVirgole() As Decimal
        Get
            If Not (ViewState("par_vMonteVirgole") Is Nothing) Then
                Return CDec(ViewState("par_vMonteVirgole"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Decimal)
            ViewState("par_vMonteVirgole") = value
        End Set

    End Property

    Private Sub SettaggioCampi()

        'CARICO CAMPI
        Dim gest As Integer
        gest = 0
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

        Try
            'SELEZIONE DEL LOTTO SE APPALTO > 0

            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_MODALITA_PAG ORDER BY DESCRIZIONE", cmbModalitaPagamento, "ID", "DESCRIZIONE", True)
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTO ORDER BY DESCRIZIONE", cmbCondizionePagamento, "ID", "DESCRIZIONE", True)
            'par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.GESTORI_ORDINI ORDER BY DESCRIZIONE", cmbGestore, "ID", "DESCRIZIONE", False)
            par.caricaComboTelerik("SELECT ID,COGNOME ||' '|| NOME AS DESCRIZIONE FROM SEPA.OPERATORI WHERE FL_AUTORIZZAZIONE_ODL=1 and nvl(revoca,0)=0 and nvl(fl_eliminato,0)=0 and nvl(data_pw,'29991231')>'" & Format(Now, "yyyyMMdd") & "' AND ID_cAF=2 ORDER BY 2", cmbGestore, "ID", "DESCRIZIONE", True)
            'SCHEDE DI IMPUTAZIONE
            'Giancarlo
            Dim i As Integer = 0
            Dim ID_ANNO_EF_CORRENTE As Long = -1
            Dim connAperta As Boolean = False

            'par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN WHERE SISCOM_MI.PF_MAIN.ID_STATO=5 AND SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO"
            par.cmd.CommandText = "SELECT SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID, TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS INIZIO, " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') || '-' || " _
                                & "TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') as stato FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN,siscom_mi.PF_STATI " _
                                & "WHERE id_stato > = 5 and SISCOM_MI.T_ESERCIZIO_FINANZIARIO.ID=SISCOM_MI.PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND id_stato = PF_STATI.ID order by T_ESERCIZIO_FINANZIARIO.ID desc"
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                i = i + 1
                If Strings.Right(par.IfNull(myReader1("INIZIO"), 1000), 4) = Now.Year Then
                    ID_ANNO_EF_CORRENTE = par.IfNull(myReader1("ID"), -1)
                End If
            End While
            myReader1.Close()
            par.caricaComboTelerik(par.cmd.CommandText, cmbElencoPrezzi, "ID", "STATO", False)
            If i = 1 Then
                Me.cmbElencoPrezzi.Items(0).Selected = True
                Me.cmbElencoPrezzi.Enabled = False
            ElseIf i = 0 Then
                Me.cmbElencoPrezzi.Items.Clear()
                Me.cmbElencoPrezzi.Enabled = False
            End If

            If i > 0 Then
                If ID_ANNO_EF_CORRENTE <> -1 Then
                    Me.cmbElencoPrezzi.SelectedValue = ID_ANNO_EF_CORRENTE
                End If
            End If

            Dim idTipoModalitaPag As String = "-1"
            Dim idTipoPagamento As String = "-1"
            Dim idGestoreOrdini As String = "-1"

            If idlotto = "-1" Then
                par.cmd.CommandText = "SELECT APPALTI.*,(SELECT DISTINCT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO=APPALTI.ID_GRUPPO AND DATA_FINE_INCARICO='30000000') AS DL FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    idlotto = par.IfNull(myReader1("ID_LOTTO"), "-1")
                    idTipoModalitaPag = par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), "-1")
                    idTipoPagamento = par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), "-1")
                    idGestoreOrdini = par.IfNull(myReader1("DL"), "-1")
                End If
                myReader1.Close()
            Else
                par.cmd.CommandText = "SELECT APPALTI.*,(SELECT DISTINCT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO=APPALTI.ID_GRUPPO AND DATA_FINE_INCARICO='30000000') AS DL FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    idTipoModalitaPag = par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), "-1")
                    idTipoPagamento = par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), "-1")
                    idGestoreOrdini = par.IfNull(myReader1("DL"), "-1")

                End If
                myReader1.Close()
            End If
            If IsNumeric(idlotto) AndAlso CDec(idlotto) <> -1 Then
                par.cmd.CommandText = "SELECT SUBsTR(INIZIO,1,4) FROM SISCOM_MI.T_eSERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=(select id_Esercizio_finanziario from siscom_mi.lotti where lotti.id=" & idlotto & ")"
                lblEsercizioF.Text = "Anno B.P. " & par.cmd.ExecuteScalar
            Else
                If IsNumeric(lotto) AndAlso CDec(lotto) <> -1 Then
                    par.cmd.CommandText = "SELECT SUBsTR(INIZIO,1,4) FROM SISCOM_MI.T_eSERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=(select id_Esercizio_finanziario from siscom_mi.lotti where lotti.id=" & lotto & ")"
                    lblEsercizioF.Text = "Anno B.P. " & par.cmd.ExecuteScalar
                Else
                    lblEsercizioF.Text = ""
                End If
            End If
            cmbModalitaPagamento.SelectedValue = idTipoModalitaPag
            cmbCondizionePagamento.SelectedValue = idTipoPagamento
            cmbGestore.SelectedValue = idGestoreOrdini

            cmbModalitaPagamento.Enabled = True
            cmbCondizionePagamento.Enabled = True
            cmbGestore.Enabled = True


            par.cmd.CommandText = ""


            'Modifica IVA
            Dim messaggioIVA As String = ""
            par.cmd.CommandText = "SELECT VALORE,ID_ALIQUOTA FROM SISCOM_MI.IVA WHERE FL_DISPONIBILE=1"
            Dim LettoreIVA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While LettoreIVA.Read
                If par.IfNull(LettoreIVA("ID_ALIQUOTA"), 0) = 3 Then
                    ValoreAttualeIvaOrdinaria.Value = par.IfNull(LettoreIVA("VALORE"), "")
                End If
                If par.IfNull(LettoreIVA("ID_ALIQUOTA"), 0) = 2 Then
                    ValoreAttualeIvaRidotta.Value = par.IfNull(LettoreIVA("VALORE"), "")
                End If
                If par.IfNull(LettoreIVA("ID_ALIQUOTA"), 0) = 1 Then
                    ValoreAttualeIvaMinima.Value = par.IfNull(LettoreIVA("VALORE"), "")
                End If
                messaggioIVA &= par.IfNull(LettoreIVA("VALORE"), "") & ";"
            End While
            LettoreIVA.Close()
            MessaggioIvaDisponibili.Value = "Inserire uno tra i seguenti valori: " & messaggioIVA




            'SELEZIONE DESCRIZIONE DEL LOTTO
            par.cmd.CommandText = "select SISCOM_MI.LOTTI.DESCRIZIONE,LOTTI.TIPO,lotti.id,lotti.id_filiale from SISCOM_MI.LOTTI where SISCOM_MI.LOTTI.ID=" & par.IfEmpty(idlotto, "-1")
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                txtlotto.Text = myReader1("DESCRIZIONE")
                Me.TipoLotto.Value = par.IfNull(myReader1("TIPO"), "")
                Me.txtidlotto.Value = par.IfNull(myReader1("ID"), "")
                Me.IdStruttura.Value = par.IfNull(myReader1("id_filiale"), 0)
            End If
            myReader1.Close()
            par.cmd.CommandText = "select * from SISCOM_MI.PF_MAIN where ID_STATO>=5 and ID_ESERCIZIO_FINANZIARIO=(select ID_ESERCIZIO_FINANZIARIO from SISCOM_MI.LOTTI where ID=" & par.IfEmpty(idlotto, "-1") & ")"
            myReader1 = par.cmd.ExecuteReader

            If myReader1.Read Then
                txtIdPianoFinanziario.Value = par.IfNull(myReader1("ID"), -1)
                idStatoPf.Value = par.IfNull(myReader1("id_stato"), 0)
            End If
            myReader1.Close()
            par.cmd.CommandText = ""
            '***SELEZIONE DELLE FILIALI
            If Not String.IsNullOrEmpty(idlotto) Then
                par.cmd.CommandText = "SELECT TAB_FILIALI.NOME, TAB_FILIALI.ID, (INDIRIZZI.DESCRIZIONE ||' '|| INDIRIZZI.CIVICO||', '||INDIRIZZI.CAP||' '||COMUNI_NAZIONI.NOME) AS INDIRIZZO FROM SISCOM_MI.TAB_FILIALI, SISCOM_MI.INDIRIZZI, COMUNI_NAZIONI,SISCOM_MI.LOTTI WHERE TAB_FILIALI.ID_INDIRIZZO = INDIRIZZI.ID AND INDIRIZZI.COD_COMUNE = COMUNI_NAZIONI.COD AND TAB_FILIALI.ID = LOTTI.ID_FILIALE AND LOTTI.ID = " & par.IfEmpty(idlotto, "-1")
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    txtFiliale.Text = par.IfNull(myReader1("NOME"), " ") & " - " & par.IfNull(myReader1("INDIRIZZO"), "")
                End If
                myReader1.Close()
            End If


            'carico servizi assegnati al lotto scelto
            ' CType(Tab_Servizio.FindControl("cmbservizio"), DropDownList).Items.Add(New ListItem(" ", -1))
            Dim rad As Object = CType(Tab_Servizio.FindControl("RadWindowServizi"), RadWindow).Controls.Item(0)

            'rad.FindControl("cmbservizio").Items.Add(New ListItem(" ", -1))
            'Tab_Servizio.cmbservizio.Items.Add(New ListItem(" ", -1))
            'par.cmd.CommandText = "select distinct * from SISCOM_MI.TAB_SERVIZI,SISCOM_MI.LOTTI_SERVIZI where SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=TAB_SERVIZI.ID and SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=" & idlotto
            Select Case idStatoPf.Value
                Case 5
                    par.cmd.CommandText = "select distinct * from SISCOM_MI.TAB_SERVIZI,SISCOM_MI.LOTTI_SERVIZI where SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=TAB_SERVIZI.ID and SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=" & par.IfEmpty(idlotto, "-1")
                Case 6
                    If Session.Item("FL_COMI") = 1 Then
                        par.cmd.CommandText = "select distinct * from SISCOM_MI.TAB_SERVIZI,SISCOM_MI.LOTTI_SERVIZI where SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=TAB_SERVIZI.ID and SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=" & par.IfEmpty(idlotto, "-1")
                    Else
                        par.cmd.CommandText = "select distinct * " _
                                            & "from SISCOM_MI.TAB_SERVIZI,SISCOM_MI.LOTTI_SERVIZI " _
                                            & "where SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=TAB_SERVIZI.ID and " _
                                            & "SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=" & par.IfEmpty(idlotto, "-1") & " " _
                                            & "and id in (select id_servizio from siscom_mi.tab_servizi_voci where id_voce in (select id from siscom_mi.pf_voci where FL_CC = 1 and id_piano_finanziario =" & txtIdPianoFinanziario.Value & "))"
                    End If
                Case 7
                    par.cmd.CommandText = "select distinct * " _
                                        & "from SISCOM_MI.TAB_SERVIZI,SISCOM_MI.LOTTI_SERVIZI " _
                                        & "where SISCOM_MI.LOTTI_SERVIZI.ID_SERVIZIO=TAB_SERVIZI.ID and " _
                                        & "SISCOM_MI.LOTTI_SERVIZI.ID_LOTTO=" & par.IfEmpty(idlotto, "-1") & " " _
                                        & "and id in (select id_servizio from siscom_mi.tab_servizi_voci where id_voce in (select id from siscom_mi.pf_voci where FL_CC = 1 and id_piano_finanziario =" & txtIdPianoFinanziario.Value & "))"
            End Select

            par.cmd.CommandText &= " ORDER BY SISCOM_MI.TAB_SERVIZI.DESCRIZIONE"
            par.caricaComboTelerik(par.cmd.CommandText, rad.FindControl("cmbservizio"), "ID", "DESCRIZIONE", True)
            'Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'While myReader2.Read
            '    rad.FindControl("cmbservizio").Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
            '    'CType(Tab_Servizio.FindControl("cmbservizio"), DropDownList).Items.Add(New ListItem(par.IfNull(myReader2("DESCRIZIONE"), " "), par.IfNull(myReader2("ID"), -1)))
            'End While
            'myReader2.Close()

            'CType(Tab_Servizio.FindControl("cmbservizio"), DropDownList).SelectedValue = -1

            'max 08/03/2016 SLA
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.SLA_PRIORITA ORDER BY DESCRIZIONE", CType(Tab_SLA.FindControl("cmbPriorita"), RadComboBox), "ID", "DESCRIZIONE", True)



            'par.cmd.CommandText = ""

            'carico fornitori
            par.caricaComboTelerik("SELECT ID,LPAD(COD_FORNITORE,5,' ')||'-'||(CASE WHEN RAGIONE_SOCIALE IS NOT NULL THEN RAGIONE_SOCIALE||'-'||PARTITA_IVA ELSE COGNOME||' '||NOME||'-'||PARTITA_IVA END) AS FORNITORE FROM SISCOM_MI.FORNITORI WHERE FL_BLOCCATO=0 ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC", cmbfornitore, "ID", "FORNITORE", True)
            par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_FORNITORE ORDER BY ID", DropDownListTipo, "ID", "DESCRIZIONE", False)
            'DropDownListTipo.Enabled = False

            'ID ESERCIZIO FINANZIARIO






        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub



    Private Sub RiempiCampi(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Try


            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

            'If CType(Tab_Servizio.FindControl("cmbservizio"), DropDownList).Items.Count = 1 And par.IfNull(myReader1("ID_STATO"), 0) < 1 Then
            '    Response.Write("<script>alert('Impossibile caricare un servizio per il lotto e l\'esercizio selezionato!\nIMPOSSIBILE PROCEDERE!');</script>")
            '    EsciAppalto()
            'End If


            'LEGGO(Appalti)
            If CStr(par.IfNull(myReader1("NUM_REPERTORIO_OLD"), "")) <> "" Then
                lblProgr.Text = "Nr. rep. ALER" & CStr(par.IfNull(myReader1("NUM_REPERTORIO_OLD"), ""))
            Else
                lblProgr.Text = ""
            End If
            Me.txtIdAppalto.Value = par.IfNull(myReader1("ID"), "-1")
            Me.txtnumero.Text = par.IfNull(myReader1("NUM_REPERTORIO"), "")
            Me.txtdescrizione.Text = par.IfNull(myReader1("DESCRIZIONE"), "")
            Me.txtdatarepertorio.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_REPERTORIO"), ""))
            '**********PEPPE MODIFY 04/09/2010***
            Me.txtannoinizio.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO"), ""))
            Me.txtannofine.SelectedDate = par.FormattaData(par.IfNull(myReader1("DATA_FINE"), ""))
            Me.txtdurata.Text = par.IfNull(myReader1("DURATA_MESI"), "")
            '**********END PEPPE MODIFY**********'
            DurataMesi()


            CType(Tab_Penali1.FindControl("txtpenali"), TextBox).Text = par.IfNull(myReader1("PENALI"), "")
            Me.lblStato.Text = StatoDaNum(par.IfNull(myReader1("ID_STATO"), ""))

            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeRup"), TextBox).Text = par.IfNull(myReader1("RUP_COGNOME"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeRup"), TextBox).Text = par.IfNull(myReader1("RUP_NOME"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelefonoRup"), TextBox).Text = par.IfNull(myReader1("RUP_TEL"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailRup"), TextBox).Text = par.IfNull(myReader1("RUP_EMAIL"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognDirect"), TextBox).Text = par.IfNull(myReader1("DL_COGNOME"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomDirect"), TextBox).Text = par.IfNull(myReader1("DL_NOME"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelDirect"), TextBox).Text = par.IfNull(myReader1("DL_TEL"), "")
            DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailDirect"), TextBox).Text = par.IfNull(myReader1("DL_EMAIL"), "")
            Me.txtCup.Text = par.IfNull(myReader1("CUP"), "")
            Me.txtCIG.Text = par.IfNull(myReader1("CIG"), "")
            par.cmd.CommandText = "SELECT COGNOME, NOME FROM SISCOM_MI.gestori_ordini WHERE ID = " & par.IfNull(myReader1("ID_GESTORE_ORDINI"), "-1")
            Dim dt As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)
            If dt.Rows.Count > 0 Then
                For Each riga As Data.DataRow In dt.Rows
                    DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeProponente"), TextBox).Text = riga.Item("COGNOME").ToString
                    DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeProponente"), TextBox).Text = riga.Item("NOME").ToString
                Next
            End If


            If myReader1("ID_STATO") = 1 Then
                Me.btnFineContratto.Enabled = True
                If VerificaAllegatoPolizza() = False Then
                    CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = True
                End If
                'Me.btnVisRate.Visible = True
            End If


            If myReader1("ID_STATO") = 0 Then
                If VerificaAllegatoAttContratto() = False Then
                    Me.btnAttivaContratto.Enabled = True
                Else
                    Me.btnAttivaContratto.Enabled = False
                End If
                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = False
            Else
                Me.btnAttivaContratto.Enabled = False
            End If


            If par.IfNull(myReader1("FL_RIT_LEGGE"), "0") = "1" Then
                Me.chkRitenute.Checked = True
            End If
            If par.IfNull(myReader1("FL_PENALI"), "0") = "1" Then
                Me.chkPenale.Checked = True
            End If

            If par.IfNull(myReader1("MODULO_FORNITORI"), "0") = "1" Then
                Me.ChkModuloFO.Checked = True
            End If
            'max aggiunto il 14/07/2017 dopo improvvisa sparizione!
            If par.IfNull(myReader1("MODULO_FORNITORI_GE"), "0") = "1" Then
                Me.ChkGestEsternaMF.Checked = True
            End If
            '-----

            '*************'

            If par.IfNull(myReader1("FL_ANTICIPO"), "0") > 0 Then
                RadComboBoxAnticipo.SelectedValue = par.IfNull(myReader1("FL_ANTICIPO"), "0")
            End If

            RadNumericTextBoxNumeroRate.Text = par.IfNull(myReader1("N_RATE_ANTICIPO"), 1)

            cmbfornitore.SelectedValue = par.IfNull(myReader1("ID_FORNITORE"), -1)
            'CType(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue = par.IfNull(myReader1("FEQUENZA_PAGAMENTO"), 0)
            trovato_cmbfornitore.Value = par.IfNull(myReader1("ID_FORNITORE"), -1)
            LoadIndirizzo()
            LoadIbanFornitore()
            Me.chkIbanF.SelectedValue = par.IfNull(myReader1("ID_IBAN"), -1)

            DropDownListTipo.SelectedValue = par.IfNull(myReader1("ID_TIPO_FORNITORE"), -1)

            If par.IfNull(myReader1("VOCE_ANTICIPO"), -1) <> -1 Then
                ImpostaComboServizi()
                RadComboBoxVoci.SelectedValue = par.IfNull(myReader1("VOCE_ANTICIPO"), -1)
            End If


            'LEGGO SERVIZI
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti
            myReader2 = par.cmd.ExecuteReader()
            If myReader2.Read Then
                Me.txtidlotto.Value = par.IfNull(myReader2("ID_LOTTO"), "-1")
            End If
            myReader2.Close()

            '*****PEPPE MODIFY 14/12/2010 
            'l'importo base d'asta canone e base d'asta consumo non viene più memorizzato nel DB ma è dato dal totale dei sigoli servizi caricati sull'appalo
            par.cmd.CommandText = "SELECT SUM(IMPORTO_CANONE) as ""IMPORTO_CANONE"",SUM(IMPORTO_CONSUMO) AS ""IMPORTO_CONSUMO"" FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = " & vIdAppalti
            Dim myReaderlotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text = IsNumFormat(myReaderlotto("IMPORTO_CANONE"), "", "##,##0.00")
                CType(Tab_Appalto_generale.FindControl("txtastaconsumo"), TextBox).Text = IsNumFormat(myReaderlotto("IMPORTO_CONSUMO"), "", "##,##0.00")
                If par.IfNull(myReaderlotto("IMPORTO_CANONE"), 0) = 0 Then
                    Me.btnVisRate.Visible = False
                End If

            End If
            myReaderlotto.Close()
            '***************14/02/2014 variazioni canone
            par.cmd.CommandText = "SELECT SUM(importo) as ""VAR_CANONE"" FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI where id_variazione in (SELECT ID FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_TIPOLOGIA = 5 AND  ID_APPALTO = " & vIdAppalti & ")"
            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_Appalto_generale.FindControl("txtVarCan"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto("VAR_CANONE"), 0), "", "##,##0.00")
            End If
            myReaderlotto.Close()
            '***************14/02/2014 variazioni CONSUMO
            par.cmd.CommandText = "SELECT SUM(importo) as ""VAR_CONSUMO"" FROM SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI where id_variazione in (SELECT ID FROM SISCOM_MI.APPALTI_VARIAZIONI WHERE ID_TIPOLOGIA = 6 AND  ID_APPALTO = " & vIdAppalti & ")"
            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_Appalto_generale.FindControl("txtVarCons"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto("VAR_CONSUMO"), 0), "", "##,##0.00")
            End If
            myReaderlotto.Close()


            par.cmd.CommandText = "select SISCOM_MI.LOTTI.DESCRIZIONE from SISCOM_MI.LOTTI where SISCOM_MI.LOTTI.ID=" & txtidlotto.Value
            myReaderlotto = par.cmd.ExecuteReader()
            If myReaderlotto.Read Then
                txtlotto.Text = myReaderlotto("DESCRIZIONE")
            End If
            myReaderlotto.Close()

            '******LEGGO GLI IMPORTI DI PENALE DA MANUTENZIONI
            par.cmd.CommandText = "SELECT SUM(importo) FROM SISCOM_MI.APPALTI_PENALI WHERE id_appalto in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) " _
                & " and id_prenotazione not in (select id from siscom_mi.prenotazioni where prenotazioni.id = id_prenotazione and id_stato = -3)"
            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_Appalto_generale.FindControl("txtfondopenali"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto(0), 0), "0", "##,##0.00")
            End If
            myReaderlotto.Close()

            'LEGGO GLI IMPORTI DI RITENUTE DI LEGGE DA MANUTENZIONI
            'par.cmd.CommandText = "SELECT SUM(RIT_LEGGE) FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO = " & vIdAppalti
            'MODIFICA 15/03/2011 CREATA TABELLA APPOSTA PER LE RITENUTE DI LEGGE E CARICO I DATI DA LI
            par.cmd.CommandText = "SELECT SUM(RIT_LEGGE_IVATA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and id_pagamento in (select id from siscom_mi.pagamenti where id=prenotazioni.id_pagamento and id_stato>0)"

            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_Appalto_generale.FindControl("txtfondoritenute"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto(0), 0), "0", "##,##0.00")
            End If
            myReaderlotto.Close()

            Dim totaleAnticipo As Decimal = 0
            par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUNTIVATO) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND TIPO_PAGAMENTO=15"
            totaleAnticipo = par.IfNull(par.cmd.ExecuteScalar, 0)

            Dim totaleTrattenuto As Decimal = 0
            par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE_CON_IVA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_STATO<>-3"
            totaleTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)
            ''AGGIUNGO L'IVA AL TOTALE TRATTENUTO
            'par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=" & vIdAppalti
            'Dim perc_iva As Decimal = CDec(par.cmd.ExecuteScalar)
            'totaleTrattenuto = totaleTrattenuto + (totaleTrattenuto * perc_iva / 100)
            CType(Tab_Appalto_generale.FindControl("txtfondoTrattenuto"), TextBox).Text = IsNumFormat(totaleAnticipo - totaleTrattenuto, "0", "##,##0.00")
            CType(Tab_Appalto_generale.FindControl("txtfondoAnticipo"), TextBox).Text = IsNumFormat(totaleAnticipo, "0", "##,##0.00")

            If Me.lblStato.Text = "ATTIVO" Then
                FrmSolaLetturaPerManutenzioni()
            End If
            If Session.Item("ID_STRUTTURA") <> myReader1("ID_STRUTTURA") Then
                FrmSolaLettura()
                Me.SOLO_LETTURA.Value = 1
            End If

            If txtIdAppalto.Value <> par.IfNull(myReader1("id_gruppo"), "0") Then
                FrmSolaLettura()
                Me.SOLO_LETTURA.Value = 1
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "attenzione", "alert('Appalto in sola lettura perchè derivante da appalto pluriennale definito in un esercizio precedente');", True)
            End If
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub DurataMesi()
        If IsDate(Me.txtannoinizio.SelectedDate) AndAlso IsDate(Me.txtannofine.SelectedDate) Then
            'If Me.txtannoinizio.Text.Substring(6, 4) = Me.txtannofine.Text.Substring(6, 4) Then

            '    DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value = CInt(Me.txtannofine.Text.Substring(3, 2)) - CInt(Me.txtannoinizio.Text.Substring(3, 2))

            'Else

            '    DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value = 0
            '    Dim anni As Integer = CInt(Me.txtannofine.Text.Substring(6, 4)) - CInt(Me.txtannoinizio.Text.Substring(6, 4))
            '    Dim i As Integer

            '    DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value = 12 - CInt(Me.txtannoinizio.Text.Substring(3, 2)) + 1
            '    If anni > 1 Then
            '        For i = 1 To anni
            '            DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value = CInt(DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value) + 12
            '            i = i + 1
            '        Next
            '    End If

            '    DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value = CInt(DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value) + CInt(Me.txtannofine.Text.Substring(3, 2))

            '    End If

            DirectCast(Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value = DateDiff(DateInterval.Month, CDate(txtannoinizio.SelectedDate), CDate(txtannofine.SelectedDate)) + 1


        End If
    End Sub
    Private Sub VisualizzaDati()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        'Dim dlist As CheckBoxList
        Dim ds As New Data.DataSet()

        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            If vIdAppalti <> 0 Then
                ' LEGGO APPALTI

                par.cmd.CommandText = " select SISCOM_MI.APPALTI.* " _
                        & "  from SISCOM_MI.APPALTI " _
                        & "  where SISCOM_MI.APPALTI.ID=" & vIdAppalti & "" _
                        & "  FOR UPDATE NOWAIT"


                myReader1 = par.cmd.ExecuteReader()
                'Peppe Modify 08/10/2010 PERCHè ESEGUE IL CICLO ????Metto if era con while!
                If myReader1.Read Then
                    RiempiCampi(myReader1)
                End If
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")




                If Request.QueryString("ID") > 0 Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                    FrmSolaLettura()
                    Me.btnIndietro.Visible = False
                End If
                If lblStato.Text = "CHIUSO" Then
                    SOLO_LETTURA.Value = 1
                    FrmSolaLettura()
                End If

                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO = " & vIdAppalti & " AND TIPO_PAGAMENTO = 9 AND ID_STATO <> -3"
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    CType(Tab_Appalto_generale.FindControl("btnPrintPagParz"), ImageButton).Visible = True
                    Me.idPagRitLegge.Value = par.IfNull(myReader1("id"), 0)
                Else
                    CType(Tab_Appalto_generale.FindControl("btnPrintPagParz"), ImageButton).Visible = False
                End If

                Dim idEsFin As String = ""
                If Not IsNothing(Request.QueryString("EF")) And Not String.IsNullOrEmpty(Request.QueryString("EF")) Then
                    idEsFin = Request.QueryString("EF")
                Else
                    par.cmd.CommandText = "select id_esercizio_finanziario from siscom_mi.lotti where lotti.id = " & par.IfEmpty(idlotto, "-1")
                    myReader1 = par.cmd.ExecuteReader
                    If myReader1.Read Then
                        idEsFin = par.IfNull(myReader1("id_esercizio_finanziario"), "")
                    End If
                End If

                Dim appaltoAttivo As Boolean = False
                If IsNumeric(vIdAppalti) AndAlso vIdAppalti > 0 Then
                    par.cmd.CommandText = "select id_stato from SISCOM_MI.pf_main where id_esercizio_finanziario in (select id_esercizio_finanziario from SISCOM_MI.lotti where id in (select id_lotto from SISCOM_MI.appalti where id_gruppo in (select id_gruppo from SISCOM_MI.appalti where id=" & vIdAppalti & "))) and id_stato=5 "
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lettore.HasRows Then
                        appaltoAttivo = True
                    End If
                    lettore.Close()
                End If

                par.cmd.CommandText = "select id_stato from siscom_mi.pf_main where id_esercizio_finanziario = " & idEsFin
                myReader1 = par.cmd.ExecuteReader


                If myReader1.Read Then
                    If (par.IfNull(myReader1("id_stato"), 5) = 6) And lblStato.Text <> "BOZZA" And (Session.Item("FL_COMI") = 1 Or appaltoAttivo = True) Then
                        'SOLO_LETTURA.Value = 1
                        'FrmSolaLettura()
                        If lblStato.Text <> "CHIUSO" Then
                            btnFineContratto.Enabled = True
                            Me.CmbContoCorrente.Enabled = True
                            If VerificaAllegatoPolizza() = False Then
                                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = True

                            End If

                        End If
                    ElseIf par.IfNull(myReader1("id_stato"), 5) > 5 And lblStato.Text <> "BOZZA" Then
                        SOLO_LETTURA.Value = 1
                        FrmSolaLettura()
                        If lblStato.Text <> "CHIUSO" Then
                            btnFineContratto.Enabled = True
                            Me.CmbContoCorrente.Enabled = True
                            If VerificaAllegatoPolizza() = False Then
                                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = True

                            End If

                        End If


                    End If
                End If

                myReader1.Close()


                'le ritenute devono restare bloccate se ci sono prenotazioni pagate
                par.cmd.CommandText = "select count(id) from siscom_mi.prenotazioni where id_appalto in (select id from siscom_mi.appalti  where id_gruppo = (select a.id_gruppo from siscom_mi.appalti a where a.id = " & txtIdAppalto.Value & ")) and id_stato >= 1"
                Dim contaPrenRit As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                If UCase(lblStato.Text.Trim) = "BOZZA" Then
                    If contaPrenRit > 0 Then
                        Me.chkRitenute.Enabled = False
                    Else
                        Me.chkRitenute.Enabled = True
                    End If
                End If
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                RadWindowManager1.RadAlert("Contratto aperto da un altro utente. Non è possibile effettuare modifiche!", 300, 150, "Attenzione", "", "null")

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = " select SISCOM_MI.APPALTI.* " _
                & "  from SISCOM_MI.APPALTI " _
                & "  where SISCOM_MI.APPALTI.ID=" & vIdAppalti
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    RiempiCampi(myReader1)
                End While
                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                FrmSolaLettura()

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click

        'assegno il valore del campo nascosto per i campi di tipo readonly che altrimenti non si aggiornerebbero 
        'If CType(Tab_Appalto_generale.FindControl("canone"), HiddenField).Value <> "" Then
        '    CType(Tab_Appalto_generale.FindControl("txtpercanone"), TextBox).Text = CType(Tab_Appalto_generale.FindControl("canone"), HiddenField).Value
        'End If
        'If CType(Tab_Appalto_generale.FindControl("consumo"), HiddenField).Value <> "" Then
        '    CType(Tab_Appalto_generale.FindControl("txtperconsumo"), TextBox).Text = CType(Tab_Appalto_generale.FindControl("consumo"), HiddenField).Value
        'End If

        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdAppalti = 0 Then
            Me.Salva()
            Tab_Servizio.CalcolaResiduo()

        Else
            If CType(Me.Tab_VariazioneImporti1.FindControl("DataGridVCan"), RadGrid).Items.Count > 0 Then
                Tab_Servizio.CalcolaResiduo()
                If ControlResiduoVariazioni() = True Then
                    Me.Update()
                End If
            Else
                Me.Update()
                Tab_Servizio.CalcolaResiduo()

            End If



        End If
        Session.Add("IDA", vIdAppalti)

    End Sub
    Public Function ControlResiduoVariazioni() As Boolean

        ControlResiduoVariazioni = True

        If CDec(par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtResiduoCanone"), TextBox).Text, 0)) < 0 Then
            ControlResiduoVariazioni = False
            RadWindowManager1.RadAlert("ATTENZIONE!Il residuo contrattuale a CANONE è NEGATIVO!Verificare le variazioni, prima di salvare!Modifica interrotta!", 300, 150, "Attenzione", "", "null")
            ' Response.Write("<script>alert('ATTENZIONE!Il residuo contrattuale a CANONE è NEGATIVO!\nVerificare le variazioni, prima di salvare!\nModifica interrotta!');</script>")

        End If

        If CDec(par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtresiduoConsumo"), TextBox).Text, 0)) < 0 Then
            ControlResiduoVariazioni = False
            RadWindowManager1.RadAlert("ATTENZIONE!Il residuo contrattuale a CONSUMO è NEGATIVO!Verificare le variazioni, prima di salvare!Modifica interrotta!", 300, 150, "Attenzione", "", "null")

        End If




    End Function
    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

        'If Val(CType(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text.Replace(".", "")) < Val(CType(Tab_Appalto_generale.FindControl("txtonericanone"), TextBox).Text.Replace(".", "")) Then
        '    Response.Write("<script>alert('Importo oneri canone superiore alla base d\'asta canone !');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        'If Val(CType(Tab_Appalto_generale.FindControl("txtastaconsumo"), TextBox).Text.Replace(".", "")) < Val(CType(Tab_Appalto_generale.FindControl("txtonericonsumo"), TextBox).Text.Replace(".", "")) Then
        '    Response.Write("<script>alert('Importo oneri canone superiore alla base d\'asta consumo !');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If
        If Not IsDate(Me.txtannoinizio.SelectedDate) Then
            RadWindowManager1.RadAlert("Riempire la data inizio!", 300, 150, "Attenzione", "", "null")

            ControlloCampi = False
            Exit Function
        End If
        If Not IsDate(Me.txtannofine.SelectedDate) Then
            RadWindowManager1.RadAlert("Riempire la data fine!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If par.IfEmpty(Me.txtnumero.Text, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire il numero dell\'appalto!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            txtnumero.Focus()
            Exit Function
        End If

        If par.IfEmpty(Me.txtdescrizione.Text, "Null") = "Null" Then
            RadWindowManager1.RadAlert("Inserire la descrizione dell\'appalto!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            txtdescrizione.Focus()
            Exit Function
        End If


        If cmbfornitore.SelectedValue = "-1" Then
            RadWindowManager1.RadAlert("Scegliere un fornitore!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If
        'If cmbIbanFornitore.SelectedValue = "-1" Then
        '    Response.Write("<script>alert('Attenzione!Il!');</script>")
        '    'ControlloCampi = False
        '    'Exit Function
        'End If
        '*********PEPPE MODIFY!!!!07/10/2009
        'If vIdAppalti = 0 And CType(Tab_Servizio.FindControl("cmbservizio"), DropDownList).SelectedValue = "-1" Then

        If CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).Items.Count <= 0 Then 'CIOè ALMENO UN SERVIZIO è STATO INSERITO NELLA TABELLA!
            RadWindowManager1.RadAlert("Inserire almeno una voce di servizio!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If Not IsDate(Me.txtdatarepertorio.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire la data repertorio!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            txtdatarepertorio.Focus()
            Exit Function
        End If

        'PEPPE MODIFY 16/03/2011 CUP NON OBBLIGATORIO ----- Or String.IsNullOrEmpty(Me.txtCup.Text) 
        If String.IsNullOrEmpty(Me.txtCIG.Text) Then
            RadWindowManager1.RadAlert("Avvalorare il campo CIG!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            txtdatarepertorio.Focus()
            Exit Function
        End If


        '*****CONTROLLO SELEZIONE DI ALMENO UN EDIFICIO/IMPIANTO PER LA COMPOSIZIONE DELL'APPALTO

        If CType(Tab_Composizione1.FindControl("DataGridComposizione"), RadGrid).Items.Count > 0 Then
            Me.Tab_Composizione1.AggiustaCompSessione()
            Dim i As Integer = 0
            'Dim di As DataGridItem
            Dim Sel As Boolean = False
            Dim dt As Data.DataTable = Session.Item("dtComp")
            For Each row As Data.DataRow In dt.Rows
                If row.Item("CHECKED") = 1 Then
                    Sel = True
                    Exit For
                End If
            Next

            'For i = 0 To CType(Tab_Composizione1.FindControl("DataGridComposizione"), DataGrid).Items.Count - 1
            '    di = CType(Tab_Composizione1.FindControl("DataGridComposizione"), DataGrid).Items(i)
            '    If DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
            '        Sel = True
            '        Exit For
            '    End If
            'Next
            If Sel = False Then
                ControlloCampi = False
                RadWindowManager1.RadAlert("Selezionare almeno un elemento dal tab composizione!", 300, 150, "Attenzione", "", "null")
                Exit Function

            End If
        Else
            RadWindowManager1.RadAlert("Attenzione, la composizione dell\'appalto è vuota!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If DropDownListTipo.SelectedValue <> "1" Then
            If Not IsNothing(Session.Item("ATI_FORNITORI")) Then
                Dim dt As Data.DataTable = CType(Session.Item("ATI_FORNITORI"), Data.DataTable)
                If dt.Rows.Count < 2 Then
                    RadWindowManager1.RadAlert("Attenzione, elenco fornitori ATI non completo!<br />Inserire almeno 2 fornitori!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                End If
            Else
                If CType(Tab_Fornitori1.FindControl("DataGridFornitori"), RadGrid).Items.Count < 2 Then
                    RadWindowManager1.RadAlert("Attenzione, elenco fornitori ATI non completo!<br />Inserire almeno 2 fornitori!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                End If
            End If
        End If


        'CONTROLLO DATE SCADENZE MANUALI
        If Not IsNothing(CType(Session.Item("DTSCADENZE"), Data.DataTable)) Then
            Dim lstscdenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
            lstscdenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
            For Each r As Mario.ScadenzeManuali In lstscdenze
                If par.AggiustaData(r.SCADENZA) < par.AggiustaData(Me.txtannoinizio.SelectedDate) Then
                    RadWindowManager1.RadAlert("Attenzione!<br />Una rata manuale risulta inferiore al periodo di durata dell\'appalto!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If

                If par.AggiustaData(r.SCADENZA) > par.AggiustaData(Me.txtannofine.SelectedDate) Then
                    RadWindowManager1.RadAlert("Attenzione!<br />Una rata manuale risulta superiore al periodo di durata dell\'appalto!", 300, 150, "Attenzione", "", "null")
                    ControlloCampi = False
                    Exit Function
                End If


            Next


        End If






    End Function

    Private Sub Salva()
        'Dim i As Integer

        Try

            'CONTROLLO SE APPALTO E' GIA' STATO INSERITO CON STESSO NUMERO
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'APRO UNA NUOVA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            Dim a As Double = (5 / 0)

            par.cmd.CommandText = "select SISCOM_MI.APPALTI.NUM_REPERTORIO from SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.NUM_REPERTORIO='" _
                                & par.PulisciStrSql(Me.txtnumero.Text) & "' AND ID_FORNITORE = " & Me.cmbfornitore.SelectedValue
            Dim myreadnum As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myreadnum.Read Then
                RadWindowManager1.RadAlert("Attenzione...Numero repertorio già presente nei nostri archivi.", 300, 150, "Attenzione", "", "null")

                myreadnum.Close()
                par.myTrans.Commit()
                Exit Sub
            End If



            ' '' Ricavo vIdAppalti
            par.cmd.CommandText = " select SISCOM_MI.SEQ_APPALTI.NEXTVAL FROM dual "
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                vIdAppalti = myReader1(0)
            End If

            myReader1.Close()
            par.cmd.CommandText = ""

            Me.txtIdAppalto.Value = vIdAppalti

            ' APPALTI DA INSERIRE
            par.cmd.Parameters.Clear()
            Dim fl_ritenute As String = "0"
            If Me.chkRitenute.Checked = True Then
                fl_ritenute = "1"
            End If
            Dim fl_penale As String = "0"
            If Me.chkPenale.Checked = True Then
                fl_penale = "1"
            End If
            Dim fl_modulo_FO As String = "0"
            If Me.ChkModuloFO.Checked = True Then
                fl_modulo_FO = "1"
            End If
            Dim fl_ANTICIPO As String = "0"
            If RadComboBoxAnticipo.SelectedValue > 0 Then
                fl_ANTICIPO = RadComboBoxAnticipo.SelectedValue
            End If
            If Not IsNumeric(RadNumericTextBoxNumeroRate.Text) Then
                RadNumericTextBoxNumeroRate.Text = 1
            End If
            Dim NRateAnticipo As Integer = CInt(RadNumericTextBoxNumeroRate.Text)

            'MODIFICA NELl'inserimento della percentuale di oneri di sicurezza. QUella nel database è quella dell'importo contrattuale netto.Quella visualizzata è quella dell'importo lordo!
            'Dim PercCanone As Decimal = CorreggiPerc(par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtImpContCanone"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtonericanone"), TextBox).Text.Replace(".", ""), 0))
            'Dim PercConsumo As Decimal = CorreggiPerc(par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtImpContConsumo"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtonericonsumo"), TextBox).Text.Replace(".", ""), 0))

            ' PEPPE MODIFY 16/12/2010 gli importi base asta consumo e base asta canone non esistono più nella tabella appalti!! li elimino dal salva e dall'update
            Dim idTipoModalitaPag As String = "NULL"
            If cmbModalitaPagamento.SelectedValue <> "-1" Then
                idTipoModalitaPag = cmbModalitaPagamento.SelectedValue
            End If
            Dim idTipoPagamento As String = "NULL"
            If cmbCondizionePagamento.SelectedValue <> "-1" Then
                idTipoPagamento = cmbCondizionePagamento.SelectedValue
            End If
            Dim idGestoreOrdini As String = "NULL"
            'If cmbGestore.SelectedValue <> "-1" Then
            '    idGestoreOrdini = cmbGestore.SelectedValue
            'End If
            Dim fl_modulo_FOGE As String = "0"
            If Me.ChkGestEsternaMF.Checked = True Then
                fl_modulo_FOGE = "1"
            End If

            'Giancarlo 04/04/2018
            'Scheda di imputazione



            par.cmd.CommandText = "insert into SISCOM_MI.APPALTI(ID,ID_FORNITORE,ID_LOTTO,DESCRIZIONE,NUM_REPERTORIO,DATA_INIZIO,DATA_FINE,DURATA_MESI," _
                & "DATA_REPERTORIO,PENALI,ID_STATO,FL_RIT_LEGGE, " _
                & "RUP_COGNOME,RUP_NOME,RUP_TEL,RUP_EMAIL,DL_COGNOME,DL_NOME,DL_TEL,DL_EMAIL,CUP,CIG,ID_STRUTTURA,FL_PENALI,ID_INDIRIZZO_FORNITORE,id_tipo_modalita_pag,id_tipo_pagamento,id_gestore_ordini,MODULO_FORNITORI,ID_TIPO_FORNITORE,MODULO_FORNITORI_GE,FL_ANTICIPO,N_RATE_ANTICIPO, ID_ELENCO_PREZZI) " _
                & "VALUES(" & vIdAppalti & "," & RitornaNullSeMenoUno(cmbfornitore.SelectedValue) & "," & txtidlotto.Value & ",'" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 500)) & "','" & par.PulisciStrSql(Me.txtnumero.Text) & "'" _
                & ",'" & par.AggiustaData(txtannoinizio.SelectedDate) & "','" & par.AggiustaData(txtannofine.SelectedDate) & "'," & par.IfEmpty(txtdurata.Text, "Null") & "" _
                & ",'" & par.AggiustaData(Me.txtdatarepertorio.SelectedDate) & "'" _
                & ",'" & Strings.Left(par.PulisciStrSql(CType(Tab_Penali1.FindControl("txtpenali"), TextBox).Text), 499) & "'," & NumDaStato(Me.lblStato.Text) & "," & fl_ritenute _
                & " , '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeRup"), TextBox).Text.ToUpper) & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeRup"), TextBox).Text.ToUpper) & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelefonoRup"), TextBox).Text.ToUpper) _
                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailRup"), TextBox).Text.ToUpper) & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognDirect"), TextBox).Text.ToUpper) & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomDirect"), TextBox).Text.ToUpper) _
                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelDirect"), TextBox).Text.ToUpper) & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailDirect"), TextBox).Text.ToUpper) & "','" & par.PulisciStrSql(Me.txtCup.Text.ToUpper) & "','" & par.PulisciStrSql(Me.txtCIG.Text.ToUpper) _
                & "'," & Me.IdStruttura.Value & "," & fl_penale & "," & RitornaNullSeMenoUno(Me.cmbIndirizzoF.SelectedValue) & "," & idTipoModalitaPag & "," & idTipoPagamento & "," & idGestoreOrdini & "," & fl_modulo_FO & "," & DropDownListTipo.SelectedValue & "," & fl_modulo_FOGE & "," & fl_ANTICIPO & "," & NRateAnticipo _
                & "," & cmbElencoPrezzi.SelectedValue & ")"
            par.cmd.ExecuteNonQuery()
            If RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = False Then
                par.cmd.CommandText = " update siscom_mi.appalti set ID_ELENCO_PREZZI = '' " _
                    & " where id = " & vIdAppalti
                par.cmd.ExecuteNonQuery()
            End If
            SalvaProponente()
            Dim dt As Data.DataTable = CType(Session.Item("ATI_FORNITORI"), Data.DataTable)
            If Not IsNothing(dt) Then
                For Each riga As Data.DataRow In dt.Rows
                    Dim capofila As Integer = 0
                    If riga.Item("capofila") = "C" Then
                        capofila = 1
                    End If
                    If dt.Rows.Count = 1 Then
                        capofila = 1
                    End If
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_FORNITORI (ID_APPALTO,ID_FORNITORE,CAPOFILA) VALUES (" & vIdAppalti & "," & riga.Item("ID") & "," & capofila & ")"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO, ID_IBAN) VALUES (" & vIdAppalti & ",(SELECT ID FROM SISCOM_MI.FORNITORI_IBAN WHERE FL_ATTIVO=1 AND ID_FORNITORE=" & riga.Item("ID") & "))"
                    par.cmd.ExecuteNonQuery()
                Next
                Session.Remove("ATI_FORNITORI")
            End If
            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_DL (" _
                & " ID_OPERATORE, ID_GRUPPO, DATA_INIZIO_INCARICO, " _
                & " DATA_FINE_INCARICO) " _
                & " VALUES (" & cmbGestore.SelectedValue & "," _
                & "(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ")" & " , " _
                & "'" & Format(Now, "yyyyMMdd") & "', " _
                & "'30000000')"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""

            par.cmd.CommandText = "SELECT NUM_REPERTORIO_OLD FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If Lettore.Read Then
                If CStr(par.IfNull(Lettore("NUM_REPERTORIO_OLD"), "")) <> "" Then
                    lblProgr.Text = "Nr. rep. ALER " & CStr(par.IfNull(myReader1("NUM_REPERTORIO_OLD"), ""))
                Else
                    lblProgr.Text = ""
                End If
            End If
            Lettore.Close()

            For Each I As ListItem In Me.chkIbanF.Items
                If I.Selected = True Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO,ID_IBAN) VALUES " _
                                        & "(" & vIdAppalti & "," & I.Value & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next
            txtIdAppalto.Value = vIdAppalti

            par.cmd.Parameters.Clear()

            SalvaComposizione(vIdAppalti)
            If Session.Item("OPERATORE") <> "*" Then
                If Session.Item("BP_CC_V") = 0 Then
                    Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
                Else
                    Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = True
                End If
            Else
                Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = True
            End If

            trovato_cmbfornitore.Value = cmbfornitore.SelectedValue

            '**** Ricavo ID dell'APPALTO
            'par.cmd.CommandText = " select SISCOM_MI.SEQ_APPALTI.CURRVAL FROM dual "
            'Dim myReaderI As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReaderI.Read Then
            '    vIdAppalti = myReaderI(0)
            'End If
            'myReaderI.Close()
            'par.cmd.CommandText = ""
            '**********

            'APPALTI

            Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)

            lstservizi = CType(HttpContext.Current.Session.Item("LSTSERVIZI"), System.Collections.Generic.List(Of Mario.VociServizi))


            Dim pfVociBuone As New List(Of String)


            For Each gen As Mario.VociServizi In lstservizi


                '*********************************
                'CALCOLO: INPUT
                '       1) gen.IMPORTO_CONSUMO  txtimportoconsumo
                '       2) gen.SCONTO_CONSUMO   txtscontoconsumo
                '       3) gen.IVA_CONSUMO      txtivaconsumo
                '       4) oneri=PAR.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0)

                'RESIDUO_CONSUMO= Importo al consumo -  %sconto al consumo  + % IVA Consumo + ( Oneri + % iva consumo)  

                'Dim Residuo As Decimal
                'Residuo = CalcolaResiduo(par.IfEmpty(gen.IMPORTO_CONSUMO, 0), par.IfEmpty(gen.SCONTO_CONSUMO, 0), par.IfEmpty(gen.IVA_CONSUMO, 0), par.IfEmpty(CType(Me.Page.FindControl("Tab_Appalto_generale").FindControl("txtonericonsumo"), TextBox).Text, 0))
                '**************************************
                Dim PercOneriConsumo As Decimal = 0
                If gen.IMPORTO_CONSUMO > 0 Then
                    PercOneriConsumo = Math.Round((gen.ONERI_SICUREZZA_CONSUMO / gen.IMPORTO_CONSUMO) * 100, 4)
                End If

                Dim PercOneriCanone As Decimal = 0
                If gen.IMPORTO_CANONE > 0 Then
                    PercOneriCanone = Math.Round((gen.ONERI_SICUREZZA_CANONE / gen.IMPORTO_CANONE) * 100, 4)
                End If

                par.cmd.CommandText = "insert into SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                            & " (ID_APPALTO,ID_PF_VOCE_IMPORTO," _
                                            & "  IMPORTO_CANONE,SCONTO_CANONE,ONERI_SICUREZZA_CANONE,PERC_ONERI_SIC_CAN ,IVA_CANONE,FREQUENZA_PAGAMENTO," _
                                            & "  IMPORTO_CONSUMO,SCONTO_CONSUMO,ONERI_SICUREZZA_CONSUMO,PERC_ONERI_SIC_CON,IVA_CONSUMO) " _
                                            & "values (" & vIdAppalti & "," & gen.ID_PF_VOCE_IMPORTO & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(Replace(gen.IMPORTO_CANONE, ".", "")), "0") _
                                                 & "," & par.IfEmpty(par.VirgoleInPunti(gen.SCONTO_CANONE), "0") & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(gen.ONERI_SICUREZZA_CANONE.Replace(".", "")), "0") & "," _
                                                 & par.VirgoleInPunti(PercOneriCanone) & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(gen.IVA_CANONE), "0") & "," _
                                                 & par.IfEmpty(gen.FREQUENZA_CANONE, "0") & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(Replace(gen.IMPORTO_CONSUMO, ".", "")), "0") & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(gen.SCONTO_CONSUMO), "0") & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(gen.ONERI_SICUREZZA_CONSUMO.Replace(".", "")), "0") & "," _
                                                 & par.VirgoleInPunti(PercOneriConsumo) & "," _
                                                 & par.IfEmpty(par.VirgoleInPunti(gen.IVA_CONSUMO), "0") & ")"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                '*** EVENTI_APPALTI
                InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - Inserimento voce servizio")


                pfVociBuone.Add(gen.ID_PF_VOCE_IMPORTO)
            Next



            '*** EVENTI_APPALTI
            InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 89, "")
            If cmbfornitore.SelectedValue <> -1 Then
                '*** EVENTI_FORNITORI
                InserisciEventoFornitore(par.cmd, cmbfornitore.SelectedValue, Session.Item("ID_OPERATORE"), 89, "")
            End If




            '***************SALVATAGGIO ELENCO SCADENZE DA LISTA
            Dim lstscadenze As System.Collections.Generic.List(Of Mario.ScadenzeManuali)
            lstscadenze = CType(HttpContext.Current.Session.Item("LSTSCADENZE"), System.Collections.Generic.List(Of Mario.ScadenzeManuali))
            For Each gen As Mario.ScadenzeManuali In lstscadenze
                If pfVociBuone.Contains(gen.ID_PF_VOCE_IMPORTO) Then
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_SCADENZE (ID_APPALTO, SCADENZA, IMPORTO,ID_PF_VOCE_IMPORTO) " _
                            & " VALUES(" & vIdAppalti & "," & par.AggiustaData(gen.SCADENZA) & ", " _
                           & par.VirgoleInPunti(par.IfEmpty(gen.IMPORTO, 0)) & "," & par.IfEmpty(gen.ID_PF_VOCE_IMPORTO, "Null") & ")"
                    par.cmd.ExecuteNonQuery()

                End If

            Next

            ''***************SALAVATAGGIO ELENCO PREZZI DA LISTA
            Dim lstprezzi As System.Collections.Generic.List(Of Mario.ElencoPrezzi)

            lstprezzi = CType(HttpContext.Current.Session.Item("LSTPREZZI"), System.Collections.Generic.List(Of Mario.ElencoPrezzi))



            For Each gen As Mario.ElencoPrezzi In lstprezzi
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_EL_PREZZI (ID,ID_APPALTO,DESCRIZIONE) " _
                     & "VALUES (SISCOM_MI.SEQ_APPALTI_EL_PREZZI.NEXTVAL, " & vIdAppalti & ", '" _
                     & par.PulisciStrSql(gen.DESCRIZIONE.Replace(Chr(13), "").Replace(Chr(10), "<br/>")) & "')"
                par.cmd.ExecuteNonQuery()
            Next





            ' COMMIT
            par.myTrans.Commit()
            par.OracleConn.Close()

            par.cmd.CommandText = ""

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE" & lIdConnessione, par.OracleConn)

            'APRO UNA NUOVA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)


            '* BLOCCO LA SCHEDA (STESSA COSA CHE ACCEDE IN VISUALIZZA DATI)
            par.cmd.CommandText = "select * from SISCOM_MI.APPALTI where SISCOM_MI.APPALTI.ID = " & vIdAppalti & " FOR UPDATE NOWAIT"
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then

            End If
            myReader1.Close()

            If par.IfEmpty(vIdAppalti, 0) > 0 Then
                If VerificaAllegatoAttContratto() = False Then
                    Me.btnAttivaContratto.Enabled = True
                End If
            End If

            CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).Rebind()
            'Tab_Servizio.BindGrid_Servizi()
            ''*** SERVIZI

            ''par.cmd.CommandText = " select rownum AS ""ID"",SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_SERVIZIO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO,SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE,TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CORPO,'9G999G990D99')) AS ""IMPORTO_CORPO"",SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CORPO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CORPO," _
            ''& "TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO,'9G999G990D99')) AS ""IMPORTO_CONSUMO"",SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CONSUMO from SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & vIdAppalti


            'par.cmd.CommandText = " select rownum as ""ID"",SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO," _
            '                 & " SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_SERVIZIO,TAB_SERVIZI.DESCRIZIONE AS SERVIZIO,SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO," _
            '                 & " SISCOM_MI.PF_VOCI_IMPORTO.DESCRIZIONE,TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE,'9G999G999G999G999G990D99')) AS ""IMPORTO_CANONE""," _
            '                 & " TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CANONE,'9G999G990D9999')AS ""SCONTO_CANONE"",SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CANONE," _
            '                 & " TRIM(TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.IMPORTO_CONSUMO,'9G999G999G999G999G990D99')) AS ""IMPORTO_CONSUMO""," _
            '                 & " TO_CHAR(SISCOM_MI.APPALTI_LOTTI_SERVIZI.SCONTO_CONSUMO,'9G999G990D9999')AS ""SCONTO_CONSUMO"",SISCOM_MI.APPALTI_LOTTI_SERVIZI.IVA_CONSUMO " _
            '          & " from SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO,SISCOM_MI.TAB_SERVIZI " _
            '          & " where SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=SISCOM_MI.PF_VOCI_IMPORTO.ID " _
            '          & "   and SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & vIdAppalti & " AND SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_SERVIZIO = TAB_SERVIZI.ID"

            'Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim ds1 As New Data.DataSet()

            'da1.Fill(ds1, "APPALTI_LOTTI_SERVIZI")

            'CType(Tab_Servizio.FindControl("DataGrid3"), DataGrid).DataSource = ds1
            'CType(Tab_Servizio.FindControl("DataGrid3"), DataGrid).DataBind()
            'ds1.Dispose()
            ''*******************************


            '*******************************

            SalvaPriorita()




            RadNotificationNote.Text = "Salvataggio effettuato correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()
            'Response.Write("<SCRIPT>alert('Salvataggio effettuato correttamente!');</SCRIPT>")



            USCITA.Value = "0"
            txtModificato.Value = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "Salva:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Private Sub SalvaPriorita()
        par.cmd.CommandText = "DELETE FROM SISCOM_MI.SLA_TEMPI WHERE ID_APPALTO = " & vIdAppalti
        par.cmd.ExecuteNonQuery()
        Select Case CType(Tab_SLA.FindControl("cmbPriorita"), RadComboBox).SelectedItem.Value
            Case Is >= 0
                par.cmd.CommandText = "select SLA_VERIFICHE.ID AS ID_VERIFICA,SLA_VERIFICHE.DESCRIZIONE,SLA_default.ORE,SLA_default.GIORNI from SISCOM_MI.SLA_default,SISCOM_MI.SLA_VERIFICHE where SLA_VERIFICHE.ID=SLA_default.ID_VERIFICA AND SLA_default.id_priorita=" & CType(Tab_SLA.FindControl("cmbPriorita"), RadComboBox).SelectedItem.Value & " ORDER BY SLA_VERIFICHE.DESCRIZIONE"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Do While lettore.Read
                    par.cmd.CommandText = "Insert into SISCOM_MI.SLA_TEMPI (ID_APPALTO, ID_PRIORITA, ID_VERIFICA, ORE, GIORNI) Values (" & vIdAppalti & ", " & CType(Tab_SLA.FindControl("cmbPriorita"), RadComboBox).SelectedItem.Value & ", " & lettore("ID_VERIFICA") & ", " & Val(par.IfEmpty(lettore("ORE"), 0)) & ", " & Val(par.IfEmpty(lettore("GIORNI"), 0)) & ")"
                    par.cmd.ExecuteNonQuery()
                Loop
                lettore.Close()
        End Select
    End Sub

    'INIZIO PEPPE 
    Private Sub SalvaComposizione(ByVal idAppalto As String)
        Try


            If idAppalto > 0 Then

                '*********CONNESSIONE RIAPERTURA DELLA PRECEDENTE SOTTO TRANSAZIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                'CANCELLO LA LISTA DEGLI EDIFICI/IMPIANTI LEGATI ALL'APPALTO
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & idAppalto
                par.cmd.ExecuteNonQuery()
                Me.Tab_Composizione1.AggiustaCompSessione()
                Dim dt As Data.DataTable = Session.Item("dtComp")

                For Each row As Data.DataRow In dt.Rows
                    If row.Item("CHECKED") = 1 Then

                        If Me.TipoLotto.Value = "E" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_EDIFICIO,D_INIZIO) VALUES " _
                        & "(" & idAppalto & "," & row.Item("id") & ",(SELECT DATA_INIZIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & idAppalto & "))"
                            par.cmd.ExecuteNonQuery()

                        ElseIf Me.TipoLotto.Value = "I" Then
                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_IMPIANTO,D_INIZIO) VALUES " _
                        & "(" & idAppalto & "," & row.Item("id") & ",(SELECT DATA_INIZIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & idAppalto & "))"
                            par.cmd.ExecuteNonQuery()

                        End If
                    End If

                Next

                'aggiorno la composizione degli appalti nati a partire dal padre pluriennale se presenti
                par.cmd.CommandText = "select id from siscom_mi.appalti where id_gruppo = " & idAppalto & " and id_gruppo <> id"
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dtApp As New Data.DataTable
                da.Fill(dtApp)

                For Each r As Data.DataRow In dtApp.Rows
                    par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & r.Item("id")
                    par.cmd.ExecuteNonQuery()

                    For Each row As Data.DataRow In dt.Rows
                        If row.Item("CHECKED") = 1 Then

                            If Me.TipoLotto.Value = "E" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_EDIFICIO,D_INIZIO) VALUES " _
                                                    & "(" & r.Item("id") & "," & row.Item("id") & ",(SELECT DATA_INIZIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & idAppalto & "))"
                                par.cmd.ExecuteNonQuery()

                            ElseIf Me.TipoLotto.Value = "I" Then
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_IMPIANTO,D_INIZIO) VALUES " _
                                                    & "(" & r.Item("id") & "," & row.Item("id") & ",(SELECT DATA_INIZIO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & idAppalto & "))"
                                par.cmd.ExecuteNonQuery()

                            End If
                        End If

                    Next
                Next



                'Dim i As Integer = 0
                'Dim di As DataGridItem
                'For i = 0 To CType(Tab_Composizione1.FindControl("DataGridComposizione"), DataGrid).Items.Count - 1
                '    di = CType(Tab_Composizione1.FindControl("DataGridComposizione"), DataGrid).Items(i)
                '    If DirectCast(di.Cells(1).FindControl("ChkSeleziona"), CheckBox).Checked = True Then
                '        If Me.TipoLotto.Value = "E" Then
                '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_EDIFICIO) VALUES " _
                '                                & "(" & idAppalto & "," & di.Cells(0).Text & ")"
                '            par.cmd.ExecuteNonQuery()
                '        Else
                '            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO (ID_APPALTO,ID_IMPIANTO) VALUES " _
                '                                & "(" & idAppalto & "," & di.Cells(0).Text & ")"
                '            par.cmd.ExecuteNonQuery()

                '        End If

                '    End If
                'Next
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Add("ERRORE", "SalvaComposizione:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub
    'END PEPPE

    '*****************FUNZIONI PER LA GESTIONE DELLO STATO CONTRATTUALE---SE SI MODIFICA LA TABELLA STATI_APPALTI MODIFICARE ANCHE LE FUNZIONI
    Private Function NumDaStato(ByVal txtStato As String) As Integer
        Select Case txtStato
            Case "BOZZA"
                NumDaStato = 0
            Case "ATTIVO"
                NumDaStato = 1
            Case "CHIUSO"
                NumDaStato = 5
            Case Else
                NumDaStato = "null"
        End Select

    End Function
    Private Function StatoDaNum(ByVal idStato As Integer) As String
        Select Case idStato
            Case 0
                StatoDaNum = "BOZZA"
            Case 1
                StatoDaNum = "ATTIVO"
            Case 5
                StatoDaNum = "CHIUSO"
            Case Else
                StatoDaNum = "N.D"
        End Select
    End Function

    Private Sub Update()
        'Dim i As Integer

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            ' APPALTI
            par.cmd.Parameters.Clear()

            'par.cmd.CommandText = "update SISCOM_MI.APPALTI set " _
            '                & "ID_FORNITORE=" & RitornaNullSeMenoUno(cmbfornitore.SelectedValue) & "," _
            '                & "DESCRIZIONE='" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 50)) & "'," _
            '                & "NUM_REPERTORIO='" & par.PulisciStrSql(Me.txtnumero.Text) & "'," _
            '                & "DATA_INIZIO='" & par.AggiustaData(txtannoinizio.Text) & "'," _
            '                & "DATA_FINE='" & par.AggiustaData(txtannofine.Text) & "'," _
            '                & "DURATA_MESI=" & par.IfEmpty(txtdurata.Text, "Null") & "," _
            '                & "BASE_ASTA_CANONE=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "BASE_ASTA_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtastaconsumo"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "ONERI_SICUREZZA_CANONE=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtonericanone"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "ONERI_SICUREZZA_CONSUMO=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtonericonsumo"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "PERC_ONERI_SIC_CAN=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtpercanone"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "PERC_ONERI_SIC_CON=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtperconsumo"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "ANNO_RIF_INIZIO=" & par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtannorifinizio"), TextBox).Text, "Null") & "," _
            '                & "ANNO_RIF_FINE=" & par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtannorifine"), TextBox).Text, "Null") & "," _
            '                & "COSTO_GRADO_GIORNO=" & par.IfEmpty(par.VirgoleInPunti(CType(Tab_Appalto_generale.FindControl("txtcosto"), TextBox).Text.Replace(".", "")), "Null") & "," _
            '                & "DATA_REPERTORIO='" & par.AggiustaData(Me.txtdatarepertorio.Text) & "'," _
            '                & "SAL=" & CType(Tab_Appalto_generale.FindControl("cmbsal"), DropDownList).SelectedItem.Value & "," _
            '                & "PENALI='" & par.PulisciStrSql(par.PulisciStringaInvio(CType(Tab_Penali1.FindControl("txtpenali"), TextBox).Text, 50)) & "'" _
            '                & " where ID=" & vIdAppalti
            '*************************PEPPE MODIFY 06/10/2010
            Dim Ritenute As String = 0
            If Me.chkRitenute.Checked = True Then
                Ritenute = 1
            End If
            Dim Penali As String = 0
            If Me.chkPenale.Checked = True Then
                Penali = 1
            End If
            Dim fl_modulo_FO As String = 0
            If Me.ChkModuloFO.Checked = True Then
                fl_modulo_FO = 1
            End If

            Dim fl_modulo_FOGE As String = 0
            If Me.ChkGestEsternaMF.Checked = True Then
                fl_modulo_FOGE = 1
            End If
            Dim FL_ANTICIPO As String = "0"
            If RadComboBoxAnticipo.SelectedValue > 0 Then
                FL_ANTICIPO = RadComboBoxAnticipo.SelectedValue
            End If
            If Not IsNumeric(RadNumericTextBoxNumeroRate.Text) Then
                RadNumericTextBoxNumeroRate.Text = 1
            End If
            Dim NRateAnticipo = CInt(RadNumericTextBoxNumeroRate.Text)

            If Me.ChkGestEsternaMF.Checked = True Then
                fl_modulo_FOGE = 1
            End If

            'MODIFICA NELl'inserimento della percentuale di oneri di sicurezza. QUella nel database è quella dell'importo contrattuale netto.Quella visualizzata è quella dell'importo lordo!
            'Dim PercCanone As Decimal = CorreggiPerc(par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtImpContCanone"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtonericanone"), TextBox).Text.Replace(".", ""), 0))
            'Dim PercConsumo As Decimal = CorreggiPerc(par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtImpContConsumo"), TextBox).Text, 0), par.IfEmpty(CType(Tab_Appalto_generale.FindControl("txtonericonsumo"), TextBox).Text.Replace(".", ""), 0))


            '***** PEPPE MODIFY 16/12/2010 ELIMINATI BASE_ASTA_CONSUMO E BASE_ASTA_CANONE PERCHè NON PIù PRESENTI NELLA TABELLA APPALTI!
            Dim idTipoModalitaPag As String = "NULL"
            Dim idTipoPagamento As String = "NULL"
            Dim idGestoreOrdini As String = "NULL"
            If cmbModalitaPagamento.SelectedValue <> "-1" Then
                idTipoModalitaPag = cmbModalitaPagamento.SelectedValue
            End If
            If cmbCondizionePagamento.SelectedValue <> "-1" Then
                idTipoPagamento = cmbCondizionePagamento.SelectedValue
            End If
            If cmbGestore.SelectedValue <> "-1" Then
                idGestoreOrdini = cmbGestore.SelectedValue
            End If

            Dim filtroSingolaVoce As String = ""
            If RadComboBoxVoci.Visible = True AndAlso RadComboBoxVoci.Enabled = True Then
                filtroSingolaVoce = ", VOCE_ANTICIPO = " & par.RitornaNullSeMenoUno(RadComboBoxVoci.SelectedValue)
            End If

            par.cmd.CommandText = "update SISCOM_MI.APPALTI set " _
                & "ID_FORNITORE=" & RitornaNullSeMenoUno(cmbfornitore.SelectedValue) & "," _
                & "DESCRIZIONE='" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 500)) & "'," _
                & "NUM_REPERTORIO='" & par.PulisciStrSql(Me.txtnumero.Text) & "'," _
                & "DATA_INIZIO='" & par.AggiustaData(txtannoinizio.SelectedDate) & "'," _
                & "DATA_FINE='" & par.AggiustaData(txtannofine.SelectedDate) & "'," _
                & "DURATA_MESI=" & par.IfEmpty(txtdurata.Text, "Null") & "," _
                & "DATA_REPERTORIO='" & par.AggiustaData(Me.txtdatarepertorio.SelectedDate) & "'," _
                & "PENALI='" & Strings.Left(par.PulisciStrSql(CType(Tab_Penali1.FindControl("txtpenali"), TextBox).Text), 499) & "', " _
                & "FL_RIT_LEGGE =" & Ritenute & ", " _
                & "ID_STATO = " & NumDaStato(Me.lblStato.Text) & ", " _
                & "RUP_COGNOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeRup"), TextBox).Text.ToUpper) & "'," _
                & "RUP_NOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeRup"), TextBox).Text.ToUpper) & "'," _
                & "RUP_TEL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelefonoRup"), TextBox).Text.ToUpper) & "'," _
                & "RUP_EMAIL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailRup"), TextBox).Text.ToUpper) & "'," _
                & "DL_COGNOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognDirect"), TextBox).Text.ToUpper) & "'," _
                & "DL_NOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomDirect"), TextBox).Text.ToUpper) & "'," _
                & "DL_TEL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelDirect"), TextBox).Text.ToUpper) & "'," _
                & "DL_EMAIL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailDirect"), TextBox).Text.ToUpper) & "'," _
                & "CUP = '" & par.PulisciStrSql(Me.txtCup.Text.ToUpper) & "'," _
                & "CIG = '" & par.PulisciStrSql(Me.txtCIG.Text.ToUpper) & "'," _
                & "FONDO_RIT_LEGGE = " & Ritenute & "," _
                & "FL_PENALI = " & Penali & ", " _
                & "ID_TIPO_MODALITA_PAG = " & idTipoModalitaPag & ", " _
                & "ID_TIPO_PAGAMENTO = " & idTipoPagamento & ", " _
                & "ID_TIPO_FORNITORE = " & DropDownListTipo.SelectedValue & ", " _
                & "/*ID_GESTORE_ORDINI = " & idGestoreOrdini & ", " _
                & "*/ID_INDIRIZZO_FORNITORE = " & RitornaNullSeMenoUno(Me.cmbIndirizzoF.SelectedValue) _
                & ",MODULO_FORNITORI=" & fl_modulo_FO _
                & ",MODULO_FORNITORI_GE=" & fl_modulo_FOGE _
                 & ",FL_ANTICIPO=" & FL_ANTICIPO _
                & ",N_RATE_ANTICIPO=" & NRateAnticipo _
                & ", ID_ELENCO_PREZZI = " & cmbElencoPrezzi.SelectedValue _
                & filtroSingolaVoce _
                & "  where ID_gruppo=(select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & ")"
            ' & "DATA_INIZIO_PAGAMENTO = " & par.AggiustaData(DirectCast(Tab_Appalto_generale.FindControl("txtDataInizioPag"), TextBox).Text) _
            par.cmd.ExecuteNonQuery()
            SalvaProponente()

            If RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = False Then
                par.cmd.CommandText = " update siscom_mi.appalti set ID_ELENCO_PREZZI = '' " _
                    & " where id = " & vIdAppalti
                par.cmd.ExecuteNonQuery()
            End If
            par.cmd.CommandText = "Select ID_OPERATORE,COGNOME||' '||NOME AS NOME FROM SISCOM_MI.APPALTI_DL,SEPA.OPERATORI WHERE ID_OPERATORE=OPERATORI.ID AND ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ") AND DATA_FINE_INCARICO='30000000'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim id_operatore As Integer = 0
            Dim nome As String = ""
            If lettore.Read Then
                id_operatore = par.IfNull(lettore("ID_OPERATORE"), 0)
                nome = par.IfNull(lettore("NOME"), 0)
            End If
            lettore.Close()

            If CStr(id_operatore) <> idGestoreOrdini Then
                If idGestoreOrdini <> "NULL" Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_DL SET DATA_FINE_INCARICO='" & Format(Now, "yyyyMMdd") & "' WHERE ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ") AND DATA_FINE_INCARICO='30000000'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_DL (" _
                     & " ID_OPERATORE, ID_GRUPPO, DATA_INIZIO_INCARICO, " _
                     & " DATA_FINE_INCARICO) " _
                     & " VALUES (" & idGestoreOrdini & "," _
                     & "(SELECT ID_gRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ")" & " , " _
                     & "'" & Format(Now, "yyyyMMdd") & "', " _
                     & "'30000000')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & vIdAppalti & "," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F255','" & par.PulisciStrSql("DL precedente: " & nome) & "')"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_DL SET DATA_FINE_INCARICO='" & Format(Now, "yyyyMMdd") & "' WHERE ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ") AND DATA_FINE_INCARICO='30000000'"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & vIdAppalti & "," & Session.Item("id_operatore") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F255','" & par.PulisciStrSql("DL non presente-DL precedente: " & nome) & "')"
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            par.cmd.CommandText = "select num_repertorio from siscom_mi.appalti where id = " & vIdAppalti
            Me.txtnumero.Text = par.IfNull(par.cmd.ExecuteScalar, Me.txtnumero.Text)

            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_IBAN WHERE ID_APPALTO = " & vIdAppalti
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO = " & vIdAppalti
            par.cmd.ExecuteNonQuery()
            'For Each I As ListItem In Me.chkIbanF.Items
            '    If I.Selected = True Then
            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO,ID_IBAN) VALUES " _
            '                            & "(" & vIdAppalti & "," & I.Value & ")"
            '        par.cmd.ExecuteNonQuery()
            '    End If
            'Next
            Dim capofila As String = ""
            Dim c As Integer = 0
            For Each elemento As GridDataItem In CType(Tab_Fornitori1.FindControl("DatagridFornitori"), RadGrid).Items
                capofila = elemento.Cells(par.IndRDGC(CType(Tab_Fornitori1.FindControl("DatagridFornitori"), RadGrid), "CAPOFILA")).Text
                If capofila = "C" Then
                    c = 1
                Else
                    c = 0
                End If
                If CType(Tab_Fornitori1.FindControl("DatagridFornitori"), RadGrid).Items.Count = 1 Then
                    c = 1
                End If
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_FORNITORI (ID_APPALTO,ID_FORNITORE,CAPOFILA) VALUES (" & vIdAppalti & "," & elemento.Cells(par.IndRDGC(CType(Tab_Fornitori1.FindControl("DatagridFornitori"), RadGrid), "ID")).Text & "," & c & ")"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO, ID_IBAN) VALUES (" & vIdAppalti & ",(SELECT ID FROM SISCOM_MI.FORNITORI_IBAN WHERE FL_ATTIVO=1 AND ID_FORNITORE=" & elemento.Cells(par.IndRDGC(CType(Tab_Fornitori1.FindControl("DatagridFornitori"), RadGrid), "ID")).Text & "))"
                par.cmd.ExecuteNonQuery()
            Next

            'For Each I As ListItem In Me.chkIbanF.Items
            '    If I.Selected = True Then
            '        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO,ID_IBAN) VALUES " _
            '                            & "(" & vIdAppalti & "," & I.Value & ")"
            '        par.cmd.ExecuteNonQuery()
            '    End If
            'Next
            txtIdAppalto.Value = vIdAppalti

            '***************PULIZIA DI APPALTI SCADENZE PER ELIMINARE QUELLI CHE MAGARI VENGONO INSERITI  
            '***************E POI CANCELLATA LA VOCE DELL'APPALTO
            par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_SCADENZE WHERE ID_APPALTO = " & vIdAppalti _
                                & "AND ID_PF_VOCE_IMPORTO NOT IN (SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                & "WHERE ID_APPALTO = " & vIdAppalti & ")"
            par.cmd.ExecuteNonQuery()
            If VerificaAllegatoAttContratto() = False Then
                Me.btnAttivaContratto.Enabled = True
            End If
            '***********************END CODE DELETE
            If NumDaStato(Me.lblStato.Text) = 1 Then
                Me.btnFineContratto.Enabled = True
                Me.btnAttivaContratto.Enabled = False
                If VerificaAllegatoPolizza() = False Then
                    CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = True
                End If
            Else
                Me.btnFineContratto.Enabled = False
                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = False
            End If

            '*************peppe modify 04/10/2010
            'CType(Tab_Appalto_generale.FindControl("cmbfornitore"), DropDownList).SelectedValue = CType(Tab_Appalto_generale.FindControl("cmbfornitore"), DropDownList).SelectedValue
            '*************Era così e l'ho modificato ....a parte lo spostamento da un tab a aspx principale, significava una riassegnazione ricorsiva sullo stesso oggetto...suppongo dovesse essere come 
            '*************riassegnazione dopo il metodo SALVA
            trovato_cmbfornitore.Value = cmbfornitore.SelectedValue


            '*** EVENTI_FORNITORI
            InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica dati appalto")
            '************************************
            If cmbfornitore.SelectedValue <> -1 Then
                '*** EVENTI_FORNITORI
                InserisciEventoFornitore(par.cmd, cmbfornitore.SelectedValue, Session.Item("ID_OPERATORE"), 2, "Modifica dati appalto")
            End If

            SalvaComposizione(vIdAppalti)

            SalvaPriorita()

            par.myTrans.Commit() 'COMMIT



            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)

            RadNotificationNote.Text = "Aggiornamento effettuato correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()
            'Response.Write("<SCRIPT>alert('Aggiornamento effettuato correttamente!');</SCRIPT>")
            USCITA.Value = "0"
            txtModificato.Value = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")


        End Try

    End Sub


    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        EsciAppalto()
        esciImmediato = "1"
    End Sub
    Private Sub EsciAppalto()
        Try
            If txtModificato.Value <> "111" Then
                Session.Add("LAVORAZIONE", "0")
                Session.Remove("DTSCADENZE")

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                If Not IsNothing(par.OracleConn) Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    If Not IsNothing(par.myTrans) Then
                        par.myTrans.Rollback()
                    End If

                    par.OracleConn.Close()

                    Session.Item("LAVORAZIONE") = "0"

                    HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    'Page.Dispose()
                End If

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                If x.Value = "1" Then
                    Response.Write("<script language='javascript'> { self.close(); }</script>")
                Else
                    If Request.QueryString("ID") > 0 Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                    End If
                End If

                Session.Remove("dtComp")
            Else
                txtModificato.Value = "1"
                USCITA.Value = "0"
            End If
        Catch ex As Exception
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try
    End Sub

    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        Try


            If txtModificato.Value <> "111" Then
                Session.Add("LAVORAZIONE", "0")

                numero = UCase(Request.QueryString("NU"))
                'tipo = UCase(Request.QueryString("TI"))
                fornitore = UCase(Request.QueryString("FO"))
                lotto = UCase(Request.QueryString("LO"))
                datadal = UCase(Request.QueryString("DAL"))
                dataal = UCase(Request.QueryString("AL"))
                If Not IsNothing(Request.QueryString("CIG")) Then
                    CIG = UCase(Request.QueryString("CIG"))
                Else
                    CIG = ""
                End If


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)

                If Not IsNothing(par.OracleConn) Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    If Not IsNothing(par.myTrans) Then
                        par.myTrans.Rollback()
                    End If

                    par.OracleConn.Close()

                    HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    ' Page.Dispose()

                End If
                If txtindietro.Value = 1 Then
                    If x.Value = "1" Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                    End If
                Else
                    If x.Value = "1" Then
                        Response.Write("<script language='javascript'> { self.close(); }</script>")
                    Else
                        Response.Write("<script>location.replace('RisultatiAppalti.aspx?CIG=" & CIG & "&NU=" & par.PulisciStrSql(numero) & "&FO=" & fornitore & "&LO=" & lotto & "&DAL=" & par.IfEmpty(datadal, "") & "&AL=" & par.IfEmpty(dataal, "") & "&TIPO=P&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF") & "');</script>")
                    End If
                End If
                Session.Remove("dtComp")

            Else
                txtModificato.Value = "1"
                USCITA.Value = "0"
            End If

            esciImmediato = "1"

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub

    'Protected Sub imgStampa_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStampa.Click
    '    Dim scriptblock As String

    '    Try
    '        imgStampa.Attributes.Add("OnClick", "javascript:window.open('Report/ReportFornitoriF.aspx?ID_FORNITORE=" & vIdAppalti & "','Report');")

    '        scriptblock = "<script language='javascript' type='text/javascript'>" _
    '        & "window.open('Report/ReportFornitoriF.aspx?ID_FORNITORE=" & vIdAppalti & "','Report');" _
    '        & "</script>"
    '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
    '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
    '        End If

    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../Errore.aspx';</script>")
    '    End Try

    'End Sub

    Protected Sub btnElimina_Click(sender As Object, e As System.EventArgs) Handles btnElimina.Click
        'Dim sNote As String

        USCITA.Value = "0"

        Try

            If txtElimina.Value = "1" Then

                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                par.cmd.CommandText = "select SISCOM_MI.MANUTENZIONI.ID_APPALTO from SISCOM_MI.MANUTENZIONI where SISCOM_MI.MANUTENZIONI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    RadWindowManager1.RadAlert("Impossibile eliminare. Appalto legato ad una manutenzione!", 300, 150, "Attenzione", "", "null")
                    myReader1.Close()
                    Exit Sub
                End If
                myReader1.Close()

                par.cmd.CommandText = ""

                par.cmd.CommandText = "select COUNT(*) FROM SISCOM_MI.PAGAMENTI WHERE SISCOM_MI.PAGAMENTI.ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                Dim nPagamenti As String = par.IfNull(par.cmd.ExecuteScalar, "0")
                If nPagamenti <> "0" Then
                    RadWindowManager1.RadAlert("Impossibile eliminare. Appalto legato ad un pagamento!", 300, 150, "Attenzione", "", "null")
                    Exit Sub
                End If

                'elimina appalto

                par.cmd.CommandText = "SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    InserisciEventoFornitore(par.cmd, lettore("ID_FORNITORE"), Session.Item("ID_OPERATORE"), 56, "Elimina appalto")
                End If
                lettore.Close()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_FORNITORI WHERE ID_APPALTO=" & vIdAppalti
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_IBAN WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI_S WHERE ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI WHERE ID IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO IN (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""

                If cmbfornitore.SelectedValue <> -1 Then
                    '*** EVENTI_FORNITORI
                    InserisciEventoFornitore(par.cmd, cmbfornitore.SelectedValue, Session.Item("ID_OPERATORE"), 56, "Elimina appalto")
                End If

                'elimina appalto
                par.cmd.CommandText = ""

                'LOG CANCELLAZIONE
                'par.cmd.Parameters.Clear()

                'par.cmd.CommandText = "insert into SISCOM_MI.LOG  " _
                '                        & " (ID_OPERATORE,DATA_OPERAZIONE,TIPO_OPERAZIONE,TIPO_OGGETTO," _
                '                        & " COD_OGGETTO,DESCR_OGGETTO,NOTE) " _
                '                 & " values (:id_operatore,:data_operazione,:tipo_operazione,:tipo_oggetto," _
                '                        & ":cod_oggetto,:descrizione,:note) "

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("id_operatore", Session.Item("ID_OPERATORE")))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("data_operazione", Format(Now, "yyyyMMdd")))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_operazione", "CANCELLAZIONE IMPIANTO"))

                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("tipo_oggetto", "IMPIANTO ACQUE METEORICHE"))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("cod_oggetto", par.IfEmpty(Me.txtCodImpianto.Text, "")))
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("descrizione", Strings.Left(Me.txtDenominazione.Text, 200)))

                'sNote = "Cancellazione Impianto Acque Meteoriche del complesso: " & Me.cmbComplesso.SelectedItem.Text & " - Edificio:" & Me.DrLEdificio.SelectedItem.Text
                'par.cmd.Parameters.Add(New Oracle.DataAccess.Client.OracleParameter("note", sNote))

                'par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = ""
                'par.cmd.Parameters.Clear()
                '****************************************


                ' COMMIT
                par.myTrans.Commit()

                RadWindowManager1.RadAlert("Eliminazione completata correttamente!", 300, 150, "Attenzione", "", "null")

                Session.Add("LAVORAZIONE", "0")

                numero = UCase(Request.QueryString("NU"))
                'tipo = UCase(Request.QueryString("TI"))
                fornitore = UCase(Request.QueryString("FO"))
                lotto = UCase(Request.QueryString("LO"))
                datadal = UCase(Request.QueryString("DAL"))
                dataal = UCase(Request.QueryString("AL"))
                idlotto = UCase(Request.QueryString("IDL"))


                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                If txtindietro.Value = 1 Then
                    Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                Else
                    Response.Write("<script>location.replace('RisultatiAppalti.aspx?NU=" & par.PulisciStrSql(numero) & "&FO=" & fornitore & "&LO=" & lotto & "&DAL=" & par.IfEmpty(par.AggiustaData(datadal), "") & "&AL=" & par.IfEmpty(par.AggiustaData(dataal), "") & idlotto & "');</script>")
                End If

            Else
                CType(Me.Page.FindControl("txtElimina"), HiddenField).Value = "0"
            End If

        Catch ex As Exception
            USCITA.Value = "0"
            Session.Item("LAVORAZIONE") = "0"

            par.myTrans.Rollback()
            par.OracleConn.Close()

            Session.Add("ERRORE", "btnElimina_Click:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub



    Private Sub AbilitazioneOggetti(ByVal ValorePass As Boolean)
        Try

            'cmbComplesso.Enabled = ValorePass
            'DrLEdificio.Enabled = ValorePass

            'txtDenominazione.ReadOnly = Not (ValorePass)
            'txtCodImpianto.ReadOnly = True


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try

    End Sub

    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try
            Me.btnSalva.Enabled = False
            Me.btnElimina.Enabled = False
            'Me.imginvioallegati.Enabled = False
            'ImgNuovo.Visible = False
            Me.btnAttivaContratto.Enabled = False
            Me.btnFineContratto.Enabled = False
            If VerificaAllegatoPolizza() = False Then
                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = True
            End If
            DirectCast(Me.Tab_Penali1.FindControl("txtpenali"), TextBox).Enabled = False
            Me.chkRitenute.Enabled = False
            Me.chkPenale.Enabled = False
            Me.btnTornBozza.Visible = False
            Me.cmbGestore.Enabled = False
            Me.ChkModuloFO.Enabled = False
            Me.ChkGestEsternaMF.Enabled = False
            RadComboBoxAnticipo.Enabled = False
            RadNumericTextBoxNumeroRate.Enabled = False
            Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
            'Me.btnVisRate.Visible = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is RadDatePicker Then
                    DirectCast(CTRL, RadDatePicker).Enabled = False
                End If
            Next
            'Me.chkIbanF.Enabled = False
            Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub FrmSolaLetturaPerManutenzioni()
        'Disabilita il form (SOLO LETTURA)
        Try
            'Me.btnSalva.enabled = False
            Me.btnElimina.Enabled = False
            'Me.imginvioallegati.Enabled = False
            'ImgNuovo.Visible = False
            DirectCast(Me.Tab_Penali1.FindControl("txtpenali"), TextBox).Enabled = False
            Me.chkRitenute.Enabled = False
            Me.chkPenale.Enabled = False
            Me.ChkModuloFO.Enabled = False
            Me.ChkGestEsternaMF.Enabled = False
            RadComboBoxAnticipo.Enabled = False
            RadNumericTextBoxNumeroRate.Enabled = False
            Dim CTRL As Control = Nothing
            For Each CTRL In Me.form1.Controls
                If TypeOf CTRL Is TextBox Then
                    DirectCast(CTRL, TextBox).ReadOnly = True
                ElseIf TypeOf CTRL Is DropDownList Then
                    DirectCast(CTRL, DropDownList).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is RadDatePicker Then
                    DirectCast(CTRL, RadDatePicker).Enabled = False
                End If
            Next
            Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = False
            'Me.Tab_Composizione1.soloLettura()
            Me.Page.FindControl("Tab_Composizione1").FindControl("btnVariazConf").Visible = True
            'Me.chkIbanF.Enabled = False
            Me.CmbContoCorrente.Enabled = True
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
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
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
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

    'Protected Sub imgEventi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgEventi.Click
    '    Dim scriptblock As String

    '    Try
    '        imgEventi.Attributes.Add("OnClick", "javascript:window.open('EventiAppalti.aspx?ID_APPALTO=" & vIdAppalti & "','Report');")

    '        scriptblock = "<script language='javascript' type='text/javascript'>" _
    '        & "window.open('EventiAppalti.aspx?ID_APPALTO=" & vIdAppalti & "','Report');" _
    '        & "</script>"
    '        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript55")) Then
    '            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript55", scriptblock)
    '        End If

    '    Catch ex As Exception
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '        Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '    End Try
    'End Sub

    Public Function InserisciEventoAppalto(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdAppalto As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEventoAppalto = False

            MyPar.Parameters.Clear()
            If InStr(Motivazione, "Modifica") Then
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & vIdAppalti & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F0" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            Else
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_APPALTI (ID_APPALTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & vIdAppalti & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            End If
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEventoAppalto = False
            MyPar.Parameters.Clear()
        End Try

    End Function

    Public Function InserisciEventoFornitore(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdFornitore As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEventoFornitore = False

            MyPar.Parameters.Clear()
            If InStr(Motivazione, "Modifica") Then
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_FORNITORI (ID_FORNITORE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & vIdFornitore & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F0" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            Else
                MyPar.CommandText = "insert into SISCOM_MI.EVENTI_FORNITORI (ID_FORNITORE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                    & "VALUES (" & vIdFornitore & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            End If
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEventoFornitore = False
            MyPar.Parameters.Clear()
        End Try

    End Function

    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDec(v), Precision)
        End If
    End Function

    Public Property tipo() As String
        Get
            If Not (ViewState("par_tipo") Is Nothing) Then
                Return CStr(ViewState("par_tipo"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_tipo") = value
        End Set

    End Property





    'Protected Sub ImgNuovo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgNuovo.Click
    '    'carico fornitori

    '    If par.OracleConn.State = Data.ConnectionState.Closed Then
    '        par.OracleConn.Open()
    '        par.SettaCommand(par)
    '    End If

    '    cmbfornitore.Items.Clear() 'devo pulire la lista altrimenti vedo un doppio caricamento

    '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE FL_BLOCCATO=0 ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
    '    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '    cmbfornitore.Items.Add(New ListItem(" ", -1))
    '    Do While myReader1.Read
    '        If IsDBNull(myReader1("RAGIONE_SOCIALE")) Then
    '            cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("COGNOME"), " ") & " " & par.IfNull(myReader1("NOME"), " "), par.IfNull(myReader1("ID"), -1)))
    '        Else
    '            cmbfornitore.Items.Add(New ListItem(par.IfNull(myReader1("RAGIONE_SOCIALE"), " "), par.IfNull(myReader1("ID"), -1)))
    '        End If
    '    Loop

    '    cmbfornitore.ClearSelection()
    '    If Session.Item("IDF") <> "0" And Not Session.Item("IDF") Is Nothing Then 'se lo passo dal salvataggio del nuovo fornitore
    '        cmbfornitore.Items.FindByValue(Session.Item("IDF")).Selected = True
    '    Else
    '        If Me.trovato_cmbfornitore.Value <> -1 Then ' se è stato precedentemente assegnato
    '            cmbfornitore.SelectedValue = Me.trovato_cmbfornitore.Value
    '        End If
    '    End If



    '    myReader1.Close()
    '    par.cmd.Dispose()
    '    par.OracleConn.Close()
    'End Sub

    Protected Sub btnAttivaContratto_Click(sender As Object, e As System.EventArgs) Handles btnAttivaContratto.Click
        Try



            If txtModificato.Value = "0" Or txtModificato.Value = "" Then

                Dim Ev As Boolean = True
                If IsDate(Me.txtannoinizio.SelectedDate) And IsDate(Me.txtannofine.SelectedDate) Then
                    'controlli aggiunti 11/04/2011 per controllare contratti di appalto annuali

                    ' RIPRENDO LA CONNESSIONE
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.SettaCommand(par)

                    'RIPRENDO LA TRANSAZIONE
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                    Dim tipologiaAppalto As Integer = 0
                    If IsNumeric(RadComboBoxVoci.SelectedValue) AndAlso CInt(RadComboBoxVoci.SelectedValue) > 0 Then
                        tipologiaAppalto = 1
                    End If
                    Dim condizioneVoceSingola As String = ""
                    If tipologiaAppalto = 1 Then
                        'appalto su singola voce
                        condizioneVoceSingola = " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & RadComboBoxVoci.SelectedValue
                    End If


                    Dim ivaPrevalente As Decimal = 0
                    'DETERMINO L'IMPORTO "PREVALENTE" LEGGENDO SOLO LA PRIMA RIGA
                    par.cmd.CommandText = " SELECT SUM(IMPORTO_CANONE - (IMPORTO_CANONE * SCONTO_CANONE /100) + ONERI_SICUREZZA_CANONE),IVA_CANONE " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                            & " WHERE ID_APPALTO = " & txtIdAppalto.Value _
                            & condizioneVoceSingola _
                            & " GROUP BY IVA_CANONE " _
                            & " UNION " _
                            & " SELECT SUM(IMPORTO_CONSUMO - (IMPORTO_CONSUMO * SCONTO_CONSUMO /100) + ONERI_SICUREZZA_CONSUMO),IVA_CONSUMO " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                            & " WHERE ID_APPALTO = " & txtIdAppalto.Value _
                            & condizioneVoceSingola _
                            & " GROUP BY IVA_CONSUMO " _
                            & " ORDER BY 1 DESC,2 DESC "
                    Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If Lettore.Read Then
                        ivaPrevalente = par.IfNull(Lettore(1), 0)
                    End If
                    Lettore.Close()
                    'CALCOLO DELL'IMPORTO IMPONIBILE CONTRATTO
                    par.cmd.CommandText = "SELECT " _
                            & " SUM(ROUND(NVL(IMPORTO_CONSUMO,0) * (1 - NVL(SCONTO_CONSUMO,0) / 100),2) + ROUND(NVL(ONERI_SICUREZZA_CONSUMO,0),2)) AS IMP_CONSUMO, " _
                            & " SUM(ROUND(NVL(IMPORTO_CANONE, 0) * (1 - NVL(SCONTO_CANONE, 0) / 100), 2) + ROUND(NVL(ONERI_SICUREZZA_CANONE, 0), 2)) AS IMP_CANONE " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO=" & txtIdAppalto.Value _
                            & condizioneVoceSingola
                    Lettore = par.cmd.ExecuteReader
                    Dim importoContrattualeConsumo As Decimal = 0
                    Dim importoContrattualeCanone As Decimal = 0
                    Dim importoAnticipo As Decimal = 0
                    If Lettore.Read Then
                        importoContrattualeConsumo = par.IfNull(Lettore("IMP_CONSUMO"), 0)
                        importoContrattualeCanone = par.IfNull(Lettore("IMP_CANONE"), 0)
                        importoAnticipo = Math.Round(((importoContrattualeCanone + importoContrattualeConsumo) * 20 / 100) * (1 + ivaPrevalente / 100), 2)
                    End If
                    Lettore.Close()



                    'CALCOLO DELL'IMPORTO TOTALE CONTRATTUALE
                    par.cmd.CommandText = " SELECT ID_PF_VOCE_IMPORTO, " _
                            & " IMPORTO_CANONE, " _
                            & " SCONTO_CANONE, " _
                            & " IVA_CANONE, " _
                            & " IMPORTO_CONSUMO, " _
                            & " SCONTO_CONSUMO, " _
                            & " IVA_CONSUMO, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " ONERI_SICUREZZA_CANONE " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_sERVIZI " _
                            & " WHERE ID_APPALTO = " & txtIdAppalto.Value _
                            & condizioneVoceSingola
                    Lettore = par.cmd.ExecuteReader
                    While Lettore.Read
                        'Dim iva As Integer = Math.Max(par.IfNull(Lettore("IVA_CONSUMO"), 0), par.IfNull(Lettore("IVA_CANONE"), 0))
                        If RadComboBoxAnticipo.SelectedValue > 0 Then
                            par.cmd.CommandText = "select count(id_appalto) from siscom_mi.APPALTI_ANTICIPI_CONTRATTUALI where id_appalto = " & txtIdAppalto.Value
                            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                            If n = 0 Then
                                'MARCO INSERT ANTICIPO CONTRATTUALE
                                '**********************************************************************
                                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
                                Dim ris As Integer = 0
                                Dim idPagamento As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                                HiddenFieldIdPagamento.Value = idPagamento
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.PAGAMENTI ( " _
                                        & " ID, ANNO, PROGR,  " _
                                        & " DATA_EMISSIONE, DATA_STAMPA, DESCRIZIONE,  " _
                                        & " IMPORTO_CONSUNTIVATO, ID_FORNITORE, ID_APPALTO,  " _
                                        & " ID_STATO, DATA_MANDATO, NUMERO_MANDATO,  " _
                                        & " TIPO_PAGAMENTO, PROGR_FORNITORE, PROGR_APPALTO,  " _
                                        & " STATO_FIRMA, RIT_LEGGE, CONTO_CORRENTE,  " _
                                        & " DATA_EMISSIONE_TMP, DATA_SAL, ID_IBAN_FORNITORE,  " _
                                        & " DATA_EMISSIONE_PAGAMENTO, DATA_AGGIORNAMENTO, ID_TIPO_MODALITA_PAG,  " _
                                        & " ID_TIPO_PAGAMENTO, DATA_SCADENZA, DESCRIZIONE_BREVE,  " _
                                        & " DATA_TRASMISSIONE, ORA_TRASMISSIONE, PRG_ODL)  " _
                                        & " VALUES (" & idPagamento & "/* ID */, " _
                                        & Format(Now, "yyyy") & " /* ANNO */, " _
                                        & "NULL/* PROGR */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_EMISSIONE */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_STAMPA */, " _
                                        & "'ANTICIPO CONTRATTUALE' /* DESCRIZIONE */, " _
                                        & "NULL /* IMPORTO_CONSUNTIVATO */, " _
                                        & "(SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & txtIdAppalto.Value & ") /* ID_FORNITORE */, " _
                                        & txtIdAppalto.Value & "/* ID_APPALTO */, " _
                                        & "0 /* ID_STATO */, " _
                                        & "NULL /* DATA_MANDATO */, " _
                                        & "NULL /* NUMERO_MANDATO */, " _
                                        & "15 /* TIPO_PAGAMENTO */, " _
                                        & "NULL /* PROGR_FORNITORE */, " _
                                        & "NULL /* PROGR_APPALTO */, " _
                                        & "NULL /* STATO_FIRMA */, " _
                                        & "0 /* RIT_LEGGE */, " _
                                        & "NULL /* CONTO_CORRENTE */, " _
                                        & "NULL /* DATA_EMISSIONE_TMP */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_SAL */, " _
                                        & "NULL /* ID_IBAN_FORNITORE */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_EMISSIONE_PAGAMENTO */, " _
                                        & "NULL /* DATA_AGGIORNAMENTO */, " _
                                        & par.RitornaNullSeMenoUno(cmbModalitaPagamento.SelectedValue) & " /* ID_TIPO_MODALITA_PAG */, " _
                                        & par.RitornaNullSeMenoUno(cmbCondizionePagamento.SelectedValue) & " /* ID_TIPO_PAGAMENTO */, " _
                                        & "NULL /* DATA_SCADENZA */, " _
                                        & "NULL /* DESCRIZIONE_BREVE */, " _
                                        & "NULL /* DATA_TRASMISSIONE */, " _
                                        & "NULL /* ORA_TRASMISSIONE */, " _
                                        & "NULL /* PRG_ODL */ ) "
                                ris = par.cmd.ExecuteNonQuery()
                                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL FROM DUAL"
                                Dim idPrenotazione As String = par.cmd.ExecuteScalar
                                HiddenFieldIdPrenotazione.Value = idPrenotazione
                                par.cmd.CommandText = " INSERT INTO SISCOM_MI.PRENOTAZIONI ( " _
                                        & " ID, ID_FORNITORE, ID_APPALTO,  " _
                                        & " ID_VOCE_PF, ID_VOCE_PF_IMPORTO, ID_STATO,  " _
                                        & " ID_PAGAMENTO, TIPO_PAGAMENTO, DESCRIZIONE,  " _
                                        & " DATA_PRENOTAZIONE, IMPORTO_PRENOTATO, IMPORTO_APPROVATO,  " _
                                        & " PROGR_FORNITORE, ANNO, DATA_SCADENZA,  " _
                                        & " DATA_STAMPA, ID_STRUTTURA, RIT_LEGGE_IVATA,  " _
                                        & " PERC_IVA, ID_PAGAMENTO_RIT_LEGGE, DATA_PRENOTAZIONE_TMP,  " _
                                        & " DATA_CONSUNTIVAZIONE, DATA_CERTIFICAZIONE, DATA_CERT_RIT_LEGGE,  " _
                                        & " IMPORTO_LIQUIDATO, DATA_ANNULLO, IMPORTO_RIT_LIQUIDATO,  " _
                                        & " FUORI_CAMPO_IVA, IMPONIBILE, IVA,  " _
                                        & " IMPONIBILE_LIQUIDATO, IVA_LIQUIDATA)  " _
                                        & " VALUES (" & idPrenotazione & " /* ID */, " _
                                        & " (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & txtIdAppalto.Value & ")/* ID_FORNITORE */, " _
                                        & txtIdAppalto.Value & "/* ID_APPALTO */, " _
                                        & "(SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE PF_VOCI_IMPORTO.ID=" & Lettore("ID_PF_VOCE_IMPORTO") & ") /* ID_VOCE_PF */, " _
                                        & Lettore("ID_PF_VOCE_IMPORTO") & " /* ID_VOCE_PF_IMPORTO */, " _
                                        & "2 /* ID_STATO */, " _
                                        & idPagamento & " /* ID_PAGAMENTO */, " _
                                        & "15 /* TIPO_PAGAMENTO */, " _
                                        & "'ANTICIPO CONTRATTUALE' /* DESCRIZIONE */, " _
                                        & Format(Now, "yyyyMMdd") & " /* DATA_PRENOTAZIONE */, " _
                                        & par.VirgoleInPunti(importoAnticipo) & " /* IMPORTO_PRENOTATO */, " _
                                        & par.VirgoleInPunti(importoAnticipo) & " /* IMPORTO_APPROVATO */, " _
                                        & "NULL /* PROGR_FORNITORE */, " _
                                        & Format(Now, "yyyy") & " /* ANNO */, " _
                                        & "NULL /* DATA_SCADENZA */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_STAMPA */, " _
                                        & Session.Item("ID_STRUTTURA") & " /* ID_STRUTTURA */, " _
                                        & "0 /* RIT_LEGGE_IVATA */, " _
                                        & ivaPrevalente & " /* PERC_IVA */, " _
                                        & "NULL /* ID_PAGAMENTO_RIT_LEGGE */, " _
                                        & "NULL /* DATA_PRENOTAZIONE_TMP */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_CONSUNTIVAZIONE */, " _
                                        & "'" & Format(Now, "yyyyMMdd") & "' /* DATA_CERTIFICAZIONE */, " _
                                        & "NULL /* DATA_CERT_RIT_LEGGE */, " _
                                        & "NULL /* IMPORTO_LIQUIDATO */, " _
                                        & "NULL /* DATA_ANNULLO */, " _
                                        & "NULL /* IMPORTO_RIT_LIQUIDATO */, " _
                                        & "NULL /* FUORI_CAMPO_IVA */, " _
                                        & "NULL /* IMPONIBILE */, " _
                                        & "NULL /* IVA */, " _
                                        & "NULL /* IMPONIBILE_LIQUIDATO */, " _
                                        & "NULL /* IVA_LIQUIDATA */ ) "
                                ris = par.cmd.ExecuteNonQuery()
                                Dim imponibile As Decimal = 0
                                Dim iva As Decimal = 0
                                par.cmd.CommandText = "SELECT IMPONIBILE, IVA FROM SISCOM_MI.PRENOTAZIONI WHERE ID = " & idPrenotazione
                                Dim Lettore1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                While Lettore1.Read
                                    imponibile = Math.Round(CDec(par.IfEmpty(Lettore1("IMPONIBILE"), 0).ToString), 2)
                                    iva = Math.Round(CDec(par.IfEmpty(Lettore1("IVA"), 0).ToString), 2)
                                End While
                                Lettore1.Close()
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI (" _
                                        & "ID_APPALTO,ID_PF_VOCE_IMPORTO,IMPORTO, IMPONIBILE, iva, perc_iva,TIPO)" _
                                        & "VALUES" _
                                        & "((SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & txtIdAppalto.Value & ")," & Lettore("ID_PF_VOCE_IMPORTO") & "," & par.VirgoleInPunti(importoAnticipo) _
                                        & ", " & par.VirgoleInPunti(imponibile) & ", " & par.VirgoleInPunti(iva) & ", " & ivaPrevalente & "," & tipologiaAppalto & ")"
                                ris = par.cmd.ExecuteNonQuery

                                par.cmd.CommandText = "UPDATE SISCOM_MI.PAGAMENTI SET IMPORTO_CONSUNTIVATO=(SELECT SUM(IMPORTO_APPROVATO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=" & idPagamento & ") WHERE PAGAMENTI.ID=" & idPagamento
                                ris = par.cmd.ExecuteNonQuery()
                                'stampa del CdP

                            End If
                        End If
                    End While
                    Lettore.Close()

                    '**********************************************************************

                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_LOTTI_SERVIZI SET IVA_CANONE = (SELECT VALORE FROM SISCOM_MI.IVA WHERE ID_ALIQUOTA=3 AND FL_DISPONIBILE=1) WHERE ID_APPALTO = " & vIdAppalti & " AND IVA_CANONE IN (SELECT VALORE FROM SISCOM_MI.IVA WHERE ID_ALIQUOTA=3 AND FL_DISPONIBILE=0)"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_LOTTI_SERVIZI SET IVA_CONSUMO =(SELECT VALORE FROM SISCOM_MI.IVA WHERE ID_ALIQUOTA=3 AND FL_DISPONIBILE=1) WHERE ID_APPALTO = " & vIdAppalti & " AND IVA_CONSUMO IN (SELECT VALORE FROM SISCOM_MI.IVA WHERE ID_ALIQUOTA=3 AND FL_DISPONIBILE=0)"
                    par.cmd.ExecuteNonQuery()

                    Dim AnnoEsercizio As String = ""
                    '****SELEZIONE DELLA'ANNO DELL'ESERCIZIO FINANZIARIO APPROVATO PER UN CONFRONTO CON L'ANNO DEL PAGAMENTO DA PRENOTARE!********
                    par.cmd.CommandText = "SELECT SUBSTR(FINE,0,4)AS ANNO_ESERCIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.LOTTI " _
                            & "WHERE T_ESERCIZIO_FINANZIARIO.ID = LOTTI.ID_ESERCIZIO_FINANZIARIO AND LOTTI.ID = (SELECT DISTINCT ID_LOTTO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & ")"
                    Lettore = par.cmd.ExecuteReader
                    If Lettore.Read Then
                        AnnoEsercizio = Lettore(0)
                    Else
                        RadWindowManager1.RadAlert("Nessun Esercizio Finanziario approvato trovato per attivare il contratto!", 300, 150, "Attenzione", "", "null")
                        Lettore.Close()
                        Exit Sub

                    End If
                    Lettore.Close()

                    'CONTROLLO SE IL CONTRATTO + PLURIENNALE, IN QUESTO CASO NON VERIFICO IL BUDGET E LASCIO ATTIVARE E PRENOTARE GLI IMPORTI SULLE VOCI DEL CONTRATTO.
                    If (Me.txtannoinizio.SelectedDate.ToString.Substring(6, 4) = Me.txtannofine.SelectedDate.ToString.Substring(6, 4)) AndAlso AnnoEsercizio = Me.txtannofine.SelectedDate.ToString.Substring(6, 4) Then
                        ControlloBudget()
                    End If


                    par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO,FREQUENZA_PAGAMENTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO =" & vIdAppalti _
                            & " and id_pf_voce_importo in (select id from siscom_mi.pf_voci_importo where PF_VOCI_IMPORTO.ID_VOCE in (select id from siscom_mi.pf_voci where id_piano_finanziario in (select id from siscom_mi.pf_main where id_Stato<6))) "
                    Lettore = par.cmd.ExecuteReader
                    Dim Freq As Integer = 0
                    If CInt(par.IfEmpty(DirectCast(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text, 0)) > 0 Then
                        While Lettore.Read
                            par.cmd.CommandText = "delete from siscom_mi.prenotazioni where " _
                                    & "  PRENOTAZIONI.ID_VOCE_PF in (select id from siscom_mi.pf_voci where id_piano_finanziario in (select id from siscom_mi.pf_main where id_Stato<6)) and" _
                                    & " id_Stato<>-3 and id_appalto in (select id from siscom_mi.appalti where id_gruppo =  " & txtIdAppalto.Value & ") and tipo_pagamento = 6 and id_pagamento is null and id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & Lettore("ID_PF_VOCE_IMPORTO") & ")"
                            par.cmd.ExecuteNonQuery()

                            ' ''par.cmd.CommandText = "select id from siscom_mi.prenotazioni where id_appalto = " & txtIdAppalto.Value & " and tipo_pagamento = 6 and id_pagamento is not null and id_voce_pf_importo = " & lettore("ID_PF_VOCE_IMPORTO")

                            ' ''Dim isPagato As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
                            'Se una prenotazione è stata pagata, dovranno procedere con l'inserile manualmente.
                            ''If isPagato = 0 Then
                            If Lettore("FREQUENZA_PAGAMENTO") = 13 Then
                                PrenotaScadManuale(Lettore("ID_PF_VOCE_IMPORTO"), Ev)
                                If Ev = False Then
                                    Exit Sub
                                End If
                            Else
                                PrenotaPagamento(Lettore("ID_PF_VOCE_IMPORTO"), Ev)
                                If Ev = False Then
                                    Exit Sub
                                End If
                            End If
                            par.cmd.CommandText = "select * from siscom_mi.prenotazioni where id_appalto in (select id from siscom_mi.appalti where id_gruppo =  " & txtIdAppalto.Value & ") and tipo_pagamento = 6 and id_pagamento is null " _
                                                    & "and id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & Lettore("ID_PF_VOCE_IMPORTO") & ") and data_scadenza in " _
                                                    & "(" _
                                                    & "select data_scadenza from siscom_mi.prenotazioni where id_appalto in (select id from siscom_mi.appalti where id_gruppo =  " & txtIdAppalto.Value & ") and tipo_pagamento = 6 and id_pagamento is not null and id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & Lettore("ID_PF_VOCE_IMPORTO") & ")" _
                                                    & ")"
                            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dt As New Data.DataTable
                            da.Fill(dt)
                            da.Dispose()

                            par.cmd.CommandText = "delete from siscom_mi.prenotazioni where id_appalto in (select id from siscom_mi.appalti where id_gruppo =  " & txtIdAppalto.Value & ") and tipo_pagamento = 6 and id_pagamento is null " _
                                                    & "and id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & Lettore("ID_PF_VOCE_IMPORTO") & ") and data_scadenza in " _
                                                    & "(" _
                                                    & "select data_scadenza from siscom_mi.prenotazioni where id_appalto in (select id from siscom_mi.appalti where id_gruppo =  " & txtIdAppalto.Value & ") and tipo_pagamento = 6 and id_pagamento is not null and id_voce_pf_importo IN (SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO CONNECT BY PRIOR PF_VOCI_IMPORTO.ID = PF_VOCI_IMPORTO.id_old START WITH PF_VOCI_IMPORTO.ID = " & Lettore("ID_PF_VOCE_IMPORTO") & ")" _
                                                    & ")"
                            par.cmd.ExecuteNonQuery()
                            ''End If
                            Freq = Lettore("FREQUENZA_PAGAMENTO")
                        End While
                        Lettore.Close()


                        par.cmd.CommandText = ""


                        If Freq <= 13 Then
                            If Ev = True Then

                                Me.SalvaAttiva.Value = 1
                                'Response.Write("<script>window.showModalDialog('RiepPrenotazioni.aspx?IdAppalto= " & vIdAppalti & "&IDCON=" & lIdConnessione & "','ScPrenotazioni','status:no;dialogWidth:515px;dialogHeight:320px;dialogHide:true;help:no;scroll:no');submit();</script>")
                                'Response.Write("<script language='javascript'>form1.submit();</script>")
                                Dim script As String = "function f(){var test = $find(""" + RadWindow1.ClientID + """);test.setUrl('RiepPrenotazioni.aspx?IdAppalto=" & vIdAppalti & "&IDCON=" & lIdConnessione & "&IMPMAS=' + document.getElementById('Tab_Appalto_generale_txtImpTotPlusOneriCan').value.replace('.', '') + '&ATTCONTR=' + document.getElementById('HiddenFieldAttivaContratto').value); test.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                                RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                            End If
                        Else
                            If Ev = True Then
                                If ctrlPlurEStatoSei() = True Then

                                    Me.txtModificato.Value = 1
                                    Me.lblStato.Text = "ATTIVO"
                                    If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                                        btnTornBozza.Visible = False
                                    Else
                                        btnTornBozza.Visible = True
                                    End If


                                    Tab_Servizio.FindControl("btnEliminaAppalti").Visible = False
                                    Tab_Servizio.FindControl("btnApriAppalti").Visible = False
                                    FrmSolaLetturaPerManutenzioni()
                                    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                                    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                                    RadNotificationNote.Text = "Il contratto è stato ATTIVATO!Salvare le modifiche apportate tramite il pulsante Salva!"
                                    RadNotificationNote.AutoCloseDelay = "0"
                                    RadNotificationNote.Show()


                                    'Response.Write("<script>alert('Il contratto è stato ATTIVATO!Salvare le modifiche apportate tramite il pulsante Salva!');</script>")
                                    Me.btnAttivaContratto.Enabled = False

                                    If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                                        btnTornBozza.Visible = False
                                    Else
                                        btnTornBozza.Visible = True
                                    End If

                                    'VISUALIZZAZIONE DELL'ANTICIPO NON APPENA IL CONTRATTO VIENE ATTIVATO
                                    Dim totaleAnticipo As Decimal = 0
                                    par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUNTIVATO) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND TIPO_PAGAMENTO=15"
                                    totaleAnticipo = par.IfNull(par.cmd.ExecuteScalar, 0)

                                    Dim totaleTrattenuto As Decimal = 0
                                    par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE_CON_IVA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_STATO<>-3"
                                    totaleTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)

                                    CType(Tab_Appalto_generale.FindControl("txtfondoTrattenuto"), TextBox).Text = IsNumFormat(totaleAnticipo - totaleTrattenuto, "0", "##,##0.00")
                                    CType(Tab_Appalto_generale.FindControl("txtfondoAnticipo"), TextBox).Text = IsNumFormat(totaleAnticipo, "0", "##,##0.00")

                                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET " _
                                                            & " TOT_CANONE = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCan"), TextBox).Text.Replace(".", ""), 0)) _
                                                            & ", TOT_CONSUMO = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCons"), TextBox).Text.Replace(".", ""), 0)) _
                                                            & " WHERE ID = " & vIdAppalti
                                    par.cmd.ExecuteNonQuery()

                                Else
                                    RadWindowManager1.RadAlert("Il contratto NON è stato attivato!Operazione annullata", 300, 150, "Attenzione", "", "null")

                                    Me.btnTornBozza.Visible = False

                                    ' RIPRENDO LA CONNESSIONE
                                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                                    par.SettaCommand(par)

                                    'RIPRENDO LA TRANSAZIONE
                                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                                    par.myTrans.Rollback()

                                    par.myTrans = par.OracleConn.BeginTransaction()
                                    HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                                    SalvaAttiva.Value = 0
                                    Session.Remove("PrenAttAuto")

                                End If


                            End If

                        End If
                    Else
                        If ctrlPlurEStatoSei() Then
                            Me.txtModificato.Value = 1
                            Me.lblStato.Text = "ATTIVO"

                            If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                                btnTornBozza.Visible = False
                            Else
                                btnTornBozza.Visible = True
                            End If

                            'Tab_Servizio.FindControl("btnEliminaAppalti").Visible = False
                            'Tab_Servizio.FindControl("btnApriAppalti").Visible = False
                            FrmSolaLetturaPerManutenzioni()
                            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                            RadNotificationNote.Text = "Il contratto è stato ATTIVATO!Salvare le modifiche apportate tramite il pulsante Salva!"
                            RadNotificationNote.AutoCloseDelay = "0"
                            RadNotificationNote.Show()
                            'Response.Write("<script>alert('Il contratto è stato ATTIVATO!Salvare le modifiche apportate tramite il pulsante Salva!');</script>")
                            If VerificaAllegatoTerminiTemporali() = False Then
                                txtannoinizio.Enabled = True
                                txtannofine.Enabled = True
                            End If
                            Me.btnAttivaContratto.Enabled = False
                            par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET " _
                                                    & " TOT_CANONE = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCan"), TextBox).Text.Replace(".", ""), 0)) _
                                                    & ", TOT_CONSUMO = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCons"), TextBox).Text.Replace(".", ""), 0)) _
                                                    & " WHERE ID = " & vIdAppalti
                            par.cmd.ExecuteNonQuery()

                            'VISUALIZZAZIONE DELL'ANTICIPO NON APPENA IL CONTRATTO VIENE ATTIVATO
                            Dim totaleAnticipo As Decimal = 0
                            par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUNTIVATO) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND TIPO_PAGAMENTO=15"
                            totaleAnticipo = par.IfNull(par.cmd.ExecuteScalar, 0)

                            Dim totaleTrattenuto As Decimal = 0
                            par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE_CON_IVA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_STATO<>-3"
                            totaleTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)

                            CType(Tab_Appalto_generale.FindControl("txtfondoTrattenuto"), TextBox).Text = IsNumFormat(totaleAnticipo - totaleTrattenuto, "0", "##,##0.00")
                            CType(Tab_Appalto_generale.FindControl("txtfondoAnticipo"), TextBox).Text = IsNumFormat(totaleAnticipo, "0", "##,##0.00")

                        Else
                            RadWindowManager1.RadAlert("Il contratto NON è stato attivato!Operazione annullata", 300, 150, "Attenzione", "", "null")

                            ' RIPRENDO LA CONNESSIONE
                            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.SettaCommand(par)

                            'RIPRENDO LA TRANSAZIONE
                            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                            par.myTrans.Rollback()

                            par.myTrans = par.OracleConn.BeginTransaction()
                            HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                            SalvaAttiva.Value = 0
                            Session.Remove("PrenAttAuto")

                        End If


                    End If
                    'Tab_VarAutomatica1.CaricaImpServiziCanone()

                    'Tab_VarAutomatica1.CaricaImpServiziConsumo()

                    CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).Rebind()
                    'Tab_Servizio.BindGrid_Servizi()

                    Tab_VariazioneImporti1.CaricaVarServizi()
                    'Tab_VarAutomatica1.CaricaImpServiziCanone()

                    'Tab_VarAutomatica1.CaricaImpServiziConsumo()
                    'Dim script As String = "function f(){var test = $find(""" + RadWindow1.ClientID + """);test.setUrl('RiepPrenotazioni.aspx'); test.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)

                    HiddenFieldAttivaContratto.Value = 1

                    'Tab_VarAutomatica1.CaricaImpServiziConsumo()
                    'Dim script As String = "function f(){var test = $find(""" + RadWindow1.ClientID + """);test.setUrl('RiepPrenotazioni.aspx?IdAppalto=" & vIdAppalti & "&IDCON=" & lIdConnessione & "&IMPMAS=' + document.getElementById('Tab_Appalto_generale_txtImpTotPlusOneriCan').value.replace('.', '') + '&ATTCONTR=' + document.getElementById('HiddenFieldAttivaContratto').value); test.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
                    'RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
                Else
                    RadWindowManager1.RadAlert("Impossibile attivare il contratto senza una data inizio nella sezione IMPORTI!", 300, 150, "Attenzione", "", "null")

                End If
            Else
                RadWindowManager1.RadAlert("Salvare le modifiche apportate prima di effettuare l\'operazione!", 300, 150, "Attenzione", "", "null")
            End If


        Catch ex As Exception
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub



    Private Function ctrlPlurEStatoSei() As Boolean
        ctrlPlurEStatoSei = False
        Dim pfAppalto As Integer
        Dim StPfAppalto As Integer
        Dim pfCorrente As Integer
        Dim idEsFAppalto As Integer
        Dim StPfCorrente As Integer
        Dim idEsCorrente As Integer
        Dim AnnoStato5 As Integer

        ' RIPRENDO LA CONNESSIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        'RIPRENDO LA TRANSAZIONE
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


        par.cmd.CommandText = "select * from siscom_mi.pf_main where pf_main.id_esercizio_finanziario = (select id_esercizio_finanziario  from siscom_mi.lotti where id = (select id_lotto from siscom_mi.appalti where id = " & vIdAppalti & "))"
        Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        If reader.Read Then
            StPfAppalto = par.IfNull(reader("id_stato"), -1)
            pfAppalto = par.IfNull(reader("id_esercizio_finanziario"), -1)
            idEsFAppalto = par.IfNull(reader("id"), -1)
            pfCorrente = pfAppalto + 1

        End If
        reader.Close()

        par.cmd.CommandText = "select * from siscom_mi.pf_main where id_esercizio_finanziario = " & pfCorrente
        reader = par.cmd.ExecuteReader
        If reader.Read Then
            StPfCorrente = par.IfNull(reader("id_stato"), -1)
            idEsCorrente = par.IfNull(reader("id"), -1)
        End If
        reader.Close()
        If pfCorrente > 0 Then
            par.cmd.CommandText = "select substr(inizio,0,4) as anno from siscom_mi.t_esercizio_finanziario where id = " & pfCorrente
            reader = par.cmd.ExecuteReader
            If reader.Read Then
                AnnoStato5 = par.IfNull(reader("anno"), 0)
            End If
            reader.Close()

        End If
        If Me.txtannofine.SelectedDate.Value.ToString.Substring(6, 4) > txtannoinizio.SelectedDate.Value.ToString.Substring(6, 4) Then

            If StPfAppalto = 6 And StPfCorrente = 5 Then
                If DuplicaAppalto(idEsFAppalto, idEsCorrente, pfAppalto, AnnoStato5) = True Then
                    ctrlPlurEStatoSei = True
                End If
            Else
                ctrlPlurEStatoSei = True

            End If
        Else
            ctrlPlurEStatoSei = True

        End If

    End Function
    Private Function DuplicaAppalto(ByVal idPfOld As Integer, ByVal idPfNew As Integer, ByVal idEsFinanzAttuale As Integer, ByVal AnnoStato5 As Integer) As Boolean
        DuplicaAppalto = False
        Try

            'duplicazione appalti
            Dim vIdPfMainOld As Integer = idPfOld
            Dim vIdPfMainNew As Integer = idPfNew
            Dim idEsercizioFinanziarioAttuale As Integer = idEsFinanzAttuale
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dt As New Data.DataTable
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            par.cmd.CommandText = "select * from siscom_mi.appalti where id_gruppo=(select b.id_gruppo from siscom_mi.appalti b where b.id=" & vIdAppalti & ") " _
                & " and id_lotto in (select id from siscom_mi.lotti where id_Esercizio_finanziario=(select id_esercizio_finanziario from siscom_mi.pf_main where id=" & idPfNew & "))"
            Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim appaltoEsiste As Boolean = False
            If lett.HasRows Then
                appaltoEsiste = True
                DuplicaAppalto = True
            End If
            If appaltoEsiste = False Then


                par.cmd.CommandText = " SELECT * FROM SISCOM_MI.APPALTI " _
                    & " WHERE " _
                    & " id = " & vIdAppalti _
                    & " and id IN (SELECT MAX(ID) FROM SISCOM_MI.APPALTI " _
                    & " WHERE DATA_FINE >(SELECT FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID=" & idEsercizioFinanziarioAttuale & ") " _
                    & " AND DATA_INIZIO <(SELECT FINE FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE ID=" & idEsercizioFinanziarioAttuale & ") " _
                    & " AND ID IN (SELECT ID_APPALTO " _
                    & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                    & " WHERE ID_PF_VOCE_IMPORTO IN " _
                    & " (SELECT ID " _
                    & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                    & " WHERE ID_VOCE IN (SELECT ID " _
                    & " FROM SISCOM_MI.PF_VOCI " _
                    & " WHERE ID_PIANO_FINANZIARIO =" & vIdPfMainOld & "))) " _
                    & " GROUP BY ID_GRUPPO) " _
                    & " ORDER BY ID ASC "

                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                dt = New Data.DataTable
                da.Fill(dt)
                da.Dispose()
                If dt.Rows.Count > 0 Then
                    Dim vIdAppaltoNew As Integer
                    Dim vIdAppaltoOld As Integer
                    Dim vidLotto As Integer
                    Dim vidStruttura As Integer


                    Dim vID As String
                    Dim vID_FORNITORE As String
                    Dim vID_LOTTO As String
                    Dim vNUM_REPERTORIO As String
                    Dim vDATA_REPERTORIO As String
                    Dim vDESCRIZIONE As String
                    Dim vANNO_INIZIO As String
                    Dim vDURATA_MESI As String
                    Dim vONERI_SICUREZZA_CANONE As String
                    Dim vONERI_SICUREZZA_CONSUMO As String
                    Dim vPERC_ONERI_SIC_CAN As String
                    Dim vPERC_ONERI_SIC_CON As String
                    Dim vANNO_RIF_INIZIO As String
                    Dim vANNO_RIF_FINE As String
                    Dim vSAL As String
                    Dim vPENALI As String
                    Dim vCOSTO_GRADO_GIORNO As String
                    Dim vDATA_INIZIO As String
                    Dim vDATA_FINE As String
                    Dim vFONDO_PENALE As String
                    Dim vFONDO_RIT_LEGGE As String
                    Dim vFEQUENZA_PAGAMENTO As String
                    Dim vID_STATO As String
                    Dim vFL_RIT_LEGGE As String
                    Dim vDATA_INIZIO_PAGAMENTO As String
                    Dim vRUP_COGNOME As String
                    Dim vRUP_NOME As String
                    Dim vRUP_TEL As String
                    Dim vRUP_EMAIL As String
                    Dim vDL_COGNOME As String
                    Dim vDL_NOME As String
                    Dim vDL_TEL As String
                    Dim vDL_EMAIL As String
                    Dim vCUP As String
                    Dim vCIG As String
                    Dim vTIPO As String
                    Dim vIdStrutturaNew As String
                    Dim vFL_PENALI As String
                    Dim vID_IBAN As String
                    Dim vTOT_CANONE As String
                    Dim vTOT_CONSUMO As String
                    Dim vID_INDIRIZZO_FORNITORE As String
                    Dim vID_GRUPPO As String


                    For Each riga As Data.DataRow In dt.Rows

                        par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_APPALTI.NEXTVAL FROM DUAL"
                        Dim LettoreSeq As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreSeq.Read Then
                            vIdAppaltoNew = par.IfNull(LettoreSeq(0), 0)
                        End If
                        LettoreSeq.Close()
                        vIdAppaltoOld = par.IfNull(riga.Item(0), 0)
                        riga.Item(0) = vIdAppaltoNew
                        If Not IsDBNull(riga.Item(2)) Then
                            vidLotto = par.IfNull(riga.Item(2), 0)

                            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.LOTTI WHERE ID_OLD=" & vidLotto
                            Dim lettoreLotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If lettoreLotto.Read Then
                                riga.Item(2) = lettoreLotto(0)
                            End If
                            lettoreLotto.Close()
                        End If

                        'modifica struttura
                        vidStruttura = riga.Item(36)
                        vIdStrutturaNew = vidStruttura

                        vID = riga.Item(0)
                        vID_FORNITORE = par.IfNull(riga.Item(1), "NULL")
                        vID_LOTTO = par.IfNull(riga.Item(2), "NULL")
                        vNUM_REPERTORIO = par.IfNull(riga.Item(3), "")
                        vDATA_REPERTORIO = par.IfNull(riga.Item(4), "")
                        vDESCRIZIONE = par.IfNull(riga.Item(5), "")
                        vANNO_INIZIO = par.IfNull(riga.Item(6), "NULL")
                        vDURATA_MESI = par.IfNull(riga.Item(7), "NULL")
                        vONERI_SICUREZZA_CANONE = par.IfNull(riga.Item(8), "NULL")
                        vONERI_SICUREZZA_CONSUMO = par.IfNull(riga.Item(9), "NULL")
                        vPERC_ONERI_SIC_CAN = par.IfNull(riga.Item(10), "NULL")
                        vPERC_ONERI_SIC_CON = par.IfNull(riga.Item(11), "NULL")
                        vANNO_RIF_INIZIO = par.IfNull(riga.Item(12), "NULL")
                        vANNO_RIF_FINE = par.IfNull(riga.Item(13), "NULL")
                        vSAL = par.IfNull(riga.Item(14), "NULL")
                        vPENALI = par.IfNull(riga.Item(15), "")
                        vCOSTO_GRADO_GIORNO = par.IfNull(riga.Item(16), "NULL")
                        vDATA_INIZIO = par.IfNull(riga.Item(17), "")
                        vDATA_FINE = par.IfNull(riga.Item(18), "")
                        vFONDO_PENALE = par.IfNull(riga.Item(19), "NULL")
                        vFONDO_RIT_LEGGE = par.IfNull(riga.Item(20), "NULL")
                        vFEQUENZA_PAGAMENTO = par.IfNull(riga.Item(21), "NULL")
                        vID_STATO = par.IfNull(riga.Item(22), "NULL")
                        vFL_RIT_LEGGE = par.IfNull(riga.Item(23), "NULL")
                        vDATA_INIZIO_PAGAMENTO = par.IfNull(riga.Item(24), "")
                        vRUP_COGNOME = par.IfNull(riga.Item(25), "")
                        vRUP_NOME = par.IfNull(riga.Item(26), "")
                        vRUP_TEL = par.IfNull(riga.Item(27), "")
                        vRUP_EMAIL = par.IfNull(riga.Item(28), "")
                        vDL_COGNOME = par.IfNull(riga.Item(29), "")
                        vDL_NOME = par.IfNull(riga.Item(30), "")
                        vDL_TEL = par.IfNull(riga.Item(31), "")
                        vDL_EMAIL = par.IfNull(riga.Item(32), "")
                        vCUP = par.IfNull(riga.Item(33), "")
                        vCIG = par.IfNull(riga.Item(34), "")
                        vTIPO = par.IfNull(riga.Item(35), "")
                        vFL_PENALI = par.IfNull(riga.Item(36), "NULL")
                        vID_IBAN = par.IfNull(riga.Item(38), "NULL")
                        vTOT_CANONE = par.IfNull(riga.Item(39), "NULL")
                        vTOT_CONSUMO = par.IfNull(riga.Item(40), "NULL")
                        vID_INDIRIZZO_FORNITORE = par.IfNull(riga.Item(41), "NULL")
                        vID_GRUPPO = par.IfNull(riga.Item(42), "NULL")


                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI (ID, " _
                            & " ID_FORNITORE, " _
                            & " ID_LOTTO, " _
                            & " NUM_REPERTORIO, " _
                            & " DATA_REPERTORIO, " _
                            & " DESCRIZIONE, " _
                            & " ANNO_INIZIO, " _
                            & " DURATA_MESI, " _
                            & " ONERI_SICUREZZA_CANONE, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " PERC_ONERI_SIC_CAN, " _
                            & " PERC_ONERI_SIC_CON, " _
                            & " ANNO_RIF_INIZIO, " _
                            & " ANNO_RIF_FINE, " _
                            & " SAL, " _
                            & " PENALI, " _
                            & " COSTO_GRADO_GIORNO, " _
                            & " DATA_INIZIO, " _
                            & " DATA_FINE, " _
                            & " FONDO_PENALE, " _
                            & " FONDO_RIT_LEGGE, " _
                            & " FEQUENZA_PAGAMENTO, " _
                            & " ID_STATO, " _
                            & " FL_RIT_LEGGE, " _
                            & " DATA_INIZIO_PAGAMENTO, " _
                            & " RUP_COGNOME, " _
                            & " RUP_NOME, " _
                            & " RUP_TEL, " _
                            & " RUP_EMAIL, " _
                            & " DL_COGNOME, " _
                            & " DL_NOME, " _
                            & " DL_TEL, " _
                            & " DL_EMAIL, " _
                            & " CUP, " _
                            & " CIG, " _
                            & " TIPO, " _
                            & " ID_STRUTTURA, " _
                            & " FL_PENALI, " _
                            & " ID_IBAN, " _
                            & " TOT_CANONE, " _
                            & " TOT_CONSUMO, " _
                            & " ID_INDIRIZZO_FORNITORE, " _
                            & " ID_GRUPPO) " _
                            & " VALUES (" & vID & ", " _
                            & vID_FORNITORE & ", " _
                            & vID_LOTTO & ", " _
                            & "'" & Replace(vNUM_REPERTORIO, "'", "''") & "', " _
                            & "'" & Replace(vDATA_REPERTORIO, "'", "''") & "', " _
                            & "'" & Replace(vDESCRIZIONE, "'", "''") & "', " _
                            & vANNO_INIZIO & ", " _
                            & vDURATA_MESI & ", " _
                            & par.VirgoleInPunti(vONERI_SICUREZZA_CANONE) & ", " _
                            & par.VirgoleInPunti(vONERI_SICUREZZA_CONSUMO) & ", " _
                            & par.VirgoleInPunti(vPERC_ONERI_SIC_CAN) & ", " _
                            & par.VirgoleInPunti(vPERC_ONERI_SIC_CON) & ", " _
                            & vANNO_RIF_INIZIO & ", " _
                            & vANNO_RIF_FINE & ", " _
                            & vSAL & ", " _
                            & "'" & Replace(vPENALI, "'", "''") & "', " _
                            & par.VirgoleInPunti(vCOSTO_GRADO_GIORNO) & ", " _
                            & "'" & Replace(vDATA_INIZIO, "'", "''") & "', " _
                            & "'" & Replace(vDATA_FINE, "'", "''") & "', " _
                            & par.VirgoleInPunti(vFONDO_PENALE) & ", " _
                            & par.VirgoleInPunti(vFONDO_RIT_LEGGE) & ", " _
                            & vFEQUENZA_PAGAMENTO & ", " _
                            & vID_STATO & ", " _
                            & vFL_RIT_LEGGE & ", " _
                            & "'" & Replace(vDATA_INIZIO_PAGAMENTO, "'", "''") & "', " _
                            & "'" & Replace(vRUP_COGNOME, "'", "''") & "', " _
                            & "'" & Replace(vRUP_NOME, "'", "''") & "', " _
                            & "'" & Replace(vRUP_TEL, "'", "''") & "', " _
                            & "'" & Replace(vRUP_EMAIL, "'", "''") & "', " _
                            & "'" & Replace(vDL_COGNOME, "'", "''") & "', " _
                            & "'" & Replace(vDL_NOME, "'", "''") & "', " _
                            & "'" & Replace(vDL_TEL, "'", "''") & "', " _
                            & "'" & Replace(vDL_EMAIL, "'", "''") & "', " _
                            & "'" & Replace(vCUP, "'", "''") & "', " _
                            & "'" & Replace(vCIG, "'", "''") & "', " _
                            & "'" & Replace(vTIPO, "'", "''") & "', " _
                            & vIdStrutturaNew & ", " _
                            & vFL_PENALI & ", " _
                            & vID_IBAN & ", " _
                            & par.VirgoleInPunti(vTOT_CANONE) & ", " _
                            & par.VirgoleInPunti(vTOT_CONSUMO) & ", " _
                            & vID_INDIRIZZO_FORNITORE & ", " _
                            & vID_GRUPPO & ")"
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI_EL_PREZZI " _
                            & " (ID, ID_APPALTO, DESCRIZIONE) SELECT SISCOM_MI.SEQ_APPALTI_EL_PREZZI.NEXTVAL," _
                            & vIdAppaltoNew & ", DESCRIZIONE FROM SISCOM_MI.APPALTI_EL_PREZZI WHERE ID_APPALTO=" & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI_IBAN " _
                            & " (ID_APPALTO, ID_IBAN) SELECT " & vIdAppaltoNew & ", ID_IBAN " _
                            & " FROM SISCOM_MI.APPALTI_IBAN WHERE ID_APPALTO =" & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_PATRIMONIO " _
                            & " (ID_APPALTO, ID_EDIFICIO, ID_IMPIANTO,D_INIZIO) SELECT " & vIdAppaltoNew & ", ID_EDIFICIO, " _
                            & " ID_IMPIANTO,D_INIZIO " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_PATRIMONIO WHERE ID_APPALTO = " & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()


                        par.cmd.CommandText = " SELECT * " _
                            & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                            & " WHERE ID_APPALTO = " & vIdAppaltoOld _
                            & " AND ID_PF_VOCE_IMPORTO IN " _
                            & " (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                            & " WHERE ID_VOCE IN (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI " _
                            & " WHERE ID_PIANO_FINANZIARIO =" & vIdPfMainOld & ")) "

                        Dim daAL As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                        Dim dtAL As New Data.DataTable
                        daAL.Fill(dtAL)
                        daAL.Dispose()

                        If dtAL.Rows.Count > 0 Then

                            Dim vALIdPfVoceImportoNew As String = ""
                            Dim vALIdPfVoceImporto As String
                            Dim vALIMPORTO_CANONE As String
                            Dim vALSCONTO_CANONE As String
                            Dim vALIVA_CANONE As String
                            Dim vALIMPORTO_CONSUMO As String
                            Dim vALSCONTO_CONSUMO As String
                            Dim vALIVA_CONSUMO As String
                            Dim vALRESIDUO_CONSUMO As String
                            Dim vALONERI_SICUREZZA_CANONE As String
                            Dim vALONERI_SICUREZZA_CONSUMO As String
                            Dim vALPERC_ONERI_SIC_CAN As String
                            Dim vALPERC_ONERI_SIC_CON As String
                            Dim vALFREQUENZA_PAGAMENTO As String
                            Dim lettoreAL As Oracle.DataAccess.Client.OracleDataReader
                            For Each rigaAL As Data.DataRow In dtAL.Rows
                                vALIdPfVoceImporto = rigaAL.Item(1)
                                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                    & " WHERE ID_OLD=" & vALIdPfVoceImporto
                                lettoreAL = par.cmd.ExecuteReader
                                If lettoreAL.Read Then
                                    vALIdPfVoceImportoNew = par.IfNull(lettoreAL(0), 0)
                                Else
                                    vALIdPfVoceImportoNew = 0
                                End If
                                lettoreAL.Close()

                                vALIMPORTO_CANONE = par.IfNull(rigaAL.Item(2), "NULL")
                                vALSCONTO_CANONE = par.IfNull(rigaAL.Item(3), "NULL")
                                vALIVA_CANONE = par.IfNull(rigaAL.Item(4), "NULL")
                                vALIMPORTO_CONSUMO = par.IfNull(rigaAL.Item(5), "NULL")
                                vALSCONTO_CONSUMO = par.IfNull(rigaAL.Item(6), "NULL")
                                vALIVA_CONSUMO = par.IfNull(rigaAL.Item(7), "NULL")
                                vALRESIDUO_CONSUMO = par.IfNull(rigaAL.Item(8), "NULL")
                                vALONERI_SICUREZZA_CANONE = par.IfNull(rigaAL.Item(9), "NULL")
                                vALONERI_SICUREZZA_CONSUMO = par.IfNull(rigaAL.Item(10), "NULL")
                                vALPERC_ONERI_SIC_CAN = par.IfNull(rigaAL.Item(11), "NULL")
                                vALPERC_ONERI_SIC_CON = par.IfNull(rigaAL.Item(12), "NULL")
                                vALFREQUENZA_PAGAMENTO = par.IfNull(rigaAL.Item(13), "NULL")



                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                    & " (ID_APPALTO, ID_PF_VOCE_IMPORTO, IMPORTO_CANONE, SCONTO_CANONE, IVA_CANONE, " _
                                    & " IMPORTO_CONSUMO, SCONTO_CONSUMO, IVA_CONSUMO, RESIDUO_CONSUMO, " _
                                    & " ONERI_SICUREZZA_CANONE, ONERI_SICUREZZA_CONSUMO, PERC_ONERI_SIC_CAN, " _
                                    & " PERC_ONERI_SIC_CON, FREQUENZA_PAGAMENTO) " _
                                    & " VALUES (" _
                                    & vIdAppaltoNew & ", " _
                                    & vALIdPfVoceImportoNew & ", " _
                                    & par.VirgoleInPunti(vALIMPORTO_CANONE) & ", " _
                                    & par.VirgoleInPunti(vALSCONTO_CANONE) & ", " _
                                    & "(SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=" & vALIVA_CANONE & ")), " _
                                    & par.VirgoleInPunti(vALIMPORTO_CONSUMO) & ", " _
                                    & par.VirgoleInPunti(vALSCONTO_CONSUMO) & ", " _
                                    & "(SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=" & vALIVA_CONSUMO & ")), " _
                                    & par.VirgoleInPunti(vALRESIDUO_CONSUMO) & ",  " _
                                    & par.VirgoleInPunti(vALONERI_SICUREZZA_CANONE) & ", " _
                                    & par.VirgoleInPunti(vALONERI_SICUREZZA_CONSUMO) & ", " _
                                    & par.VirgoleInPunti(vALPERC_ONERI_SIC_CAN) & ", " _
                                    & par.VirgoleInPunti(vALPERC_ONERI_SIC_CON) & ", " _
                                    & par.VirgoleInPunti(vALFREQUENZA_PAGAMENTO) _
                                    & " )"
                                par.cmd.ExecuteNonQuery()

                            Next


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_RIT_LEGGE " _
                                & " (ID, ID_APPALTO, ID_MANUTENZIONE, ID_PRENOTAZIONE, ID_PAGAMENTO, IMPORTO) " _
                                & " SELECT ID, " & vIdAppaltoNew & ", ID_MANUTENZIONE, ID_PRENOTAZIONE, ID_PAGAMENTO, " _
                                & " IMPORTO FROM SISCOM_MI.APPALTI_RIT_LEGGE WHERE ID_APPALTO = " & vIdAppaltoOld
                            par.cmd.ExecuteNonQuery()


                            par.cmd.CommandText = "SELECT * " _
                                & " FROM SISCOM_MI.APPALTI_SCADENZE " _
                                & " WHERE ID_APPALTO = " & vIdAppaltoOld _
                                & " AND scadenza>(SELECT FINE FROM siscom_mi.T_ESERCIZIO_FINANZIARIO " _
                                & " WHERE ID=" & idEsercizioFinanziarioAttuale & ")"

                            Dim daAS As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                            Dim dtAS As New Data.DataTable
                            daAS.Fill(dtAS)
                            daAS.Dispose()


                            If dtAS.Rows.Count > 0 Then
                                Dim vIdPfVoceImportoNew As String = ""
                                Dim vIdPfVoceImporto As String
                                Dim vSCADENZA As String
                                Dim vIMPORTO As String
                                Dim lettorePFVoceImporto As Oracle.DataAccess.Client.OracleDataReader
                                For Each elementi As Data.DataRow In dtAS.Rows
                                    vIdPfVoceImporto = elementi.Item(1)
                                    vSCADENZA = elementi.Item(2)
                                    vIMPORTO = elementi.Item(3)
                                    par.cmd.CommandText = "SELECT iD FROM siscom_mi.PF_VOCI_IMPORTO " _
                                        & " WHERE ID_OLD=" & vIdPfVoceImporto
                                    lettorePFVoceImporto = par.cmd.ExecuteReader
                                    If lettorePFVoceImporto.Read Then
                                        vIdPfVoceImportoNew = lettorePFVoceImporto(0)
                                    End If
                                    lettorePFVoceImporto.Close()
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_SCADENZE " _
                                        & " (ID_APPALTO, ID_PF_VOCE_IMPORTO," _
                                        & " SCADENZA, IMPORTO) VALUES " _
                                        & " (" & vIdAppaltoNew & "," & vIdPfVoceImportoNew _
                                        & " ," & vSCADENZA & "," & par.VirgoleInPunti(vIMPORTO) & " )"

                                    par.cmd.ExecuteNonQuery()
                                Next
                            End If
                        Else
                            'Beep()
                        End If
                        par.cmd.CommandText = " INSERT INTO SISCOM_MI.APPALTI_VOCI_PF (ID_APPALTO, " _
                            & " ID_PF_VOCE, " _
                            & " IMPORTO_CANONE, " _
                            & " SCONTO_CANONE, " _
                            & " IVA_CANONE, " _
                            & " IMPORTO_CONSUMO, " _
                            & " SCONTO_CONSUMO, " _
                            & " IVA_CONSUMO, " _
                            & " RESIDUO_CONSUMO, " _
                            & " ONERI_SICUREZZA_CANONE, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " PERC_ONERI_SIC_CAN, " _
                            & " PERC_ONERI_SIC_CON) " _
                            & " SELECT " & vIdAppaltoNew & ", " _
                            & " (SELECT ID " _
                            & " FROM SISCOM_MI.PF_VOCI " _
                            & " WHERE ID_OLD = ID_PF_VOCE), " _
                            & " IMPORTO_CANONE, " _
                            & " SCONTO_CANONE, " _
                            & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CANONE)) AS IVA_CANONE, " _
                            & " IMPORTO_CONSUMO, " _
                            & " SCONTO_CONSUMO, " _
                            & " (SELECT MAX(VALORE) from siscom_mi.iva WHERE FL_DISPONIBILE=1 AND ID_ALIQUOTA IN (SELECT A.ID_ALIQUOTA from siscom_mi.iva A WHERE A.VALORE=IVA_CONSUMO)) AS IVA_CONSUMO, " _
                            & " RESIDUO_CONSUMO, " _
                            & " ONERI_SICUREZZA_CANONE, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " PERC_ONERI_SIC_CAN, " _
                            & " PERC_ONERI_SIC_CON " _
                            & " FROM SISCOM_MI.APPALTI_VOCI_PF " _
                            & " WHERE ID_APPALTO =" & vIdAppaltoOld
                        par.cmd.ExecuteNonQuery()

                    Next

                    'mette a posto le prenotazioni
                    par.cmd.CommandText = "select * from siscom_mi.prenotazioni where id_stato = -1 and tipo_pagamento = 6 and id_appalto = " & vIdAppalti
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim stato As Integer = -1
                    Dim annoPren As Integer = 0
                    While lettore.Read
                        stato = -1
                        annoPren = par.IfNull(lettore("data_scadenza"), "00000000")
                        annoPren = annoPren.ToString.Substring(0, 4)

                        If annoPren = AnnoStato5 Then
                            stato = 1
                        End If

                        par.cmd.CommandText = "update siscom_mi.prenotazioni set id_stato = " & stato & ", id_appalto = " & vIdAppaltoNew & ",id_voce_pf= (select id from siscom_mi.pf_voci where id_old = " & lettore("id_voce_pf") & "),id_voce_pf_importo = (select id from siscom_mi.pf_voci_importo where id_old = " & lettore("id_voce_pf_importo") & ") where id = " & lettore("id")
                        par.cmd.ExecuteNonQuery()
                    End While
                    lettore.Close()


                    DuplicaAppalto = True
                End If
            End If

        Catch ex As Exception
            'par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            'If Not IsNothing(par.myTrans) Then
            '    par.myTrans.Rollback()
            'End If
            'par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            'If Not IsNothing(par.OracleConn) Then
            '    par.OracleConn.Close()
            'End If

            'HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            'HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            'Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Session.Item("LAVORAZIONE") = "0"
            'Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            'Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            DuplicaAppalto = False
        End Try


    End Function
#Region "Controlli Canone e Consumo CORRETTI"

    Private Sub ControlloBudget()
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            Dim TOTPRENOTATO As Decimal = 0
            Dim RESIDUO As Decimal = 0
            par.cmd.CommandText = "select id_appalto,id_pf_voce_importo from siscom_mi.APPALTI_LOTTI_SERVIZI where id_appalto = " & vIdAppalti
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dtVoci As New Data.DataTable()
            da.Fill(dtVoci)

            For Each row As Data.DataRow In dtVoci.Rows
                TOTPRENOTATO = 0
                RESIDUO = 0

                '04/07/2011 MODIFICA PER CONTROLLO SU OGNI VOCE DI SERVIZIO ASSOCIATA ALL'APPALTO CHE SI STA PER ATTIVARE



                'par.cmd.CommandText = "SELECT APPALTI.ID AS ID_APPALTO,IMPORTO_CONSUMO,(IMPORTO_CONSUMO-((IMPORTO_CONSUMO*SCONTO_CONSUMO)/100)) IMP_SCONTATO_CONSUMO, " _
                '                    & "IVA_CONSUMO ,APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CONSUMO,APPALTI_VARIAZIONI_IMPORTI.IMPORTO ," _
                '                    & "IMPORTO_CANONE,(IMPORTO_CANONE-((IMPORTO_CONSUMO*SCONTO_CANONE)/100)) IMP_SCONTATO_CANONE, " _
                '                    & "IVA_CANONE ,APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CANONE " _
                '                    & "FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI,SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI,SISCOM_MI.APPALTI_VARIAZIONI " _
                '                    & "WHERE APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO IN  " _
                '                    & "(SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_STRUTTURA = " & IdStruttura.Value & " AND ID_APPALTO = " & vIdAppalti & ") " _
                '                    & "AND APPALTI.ID = APPALTI_LOTTI_SERVIZI.ID_APPALTO " _
                '                    & "AND APPALTI_VARIAZIONI.ID_APPALTO(+) =APPALTI.ID AND APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE(+) = APPALTI_VARIAZIONI.ID"

                par.cmd.CommandText = "SELECT APPALTI_LOTTI_SERVIZI.ID_APPALTO AS ID_APPALTO,IMPORTO_CONSUMO, " _
                                    & "(IMPORTO_CONSUMO-((IMPORTO_CONSUMO*SCONTO_CONSUMO)/100)) IMP_SCONTATO_CONSUMO, " _
                                    & "IVA_CONSUMO ,APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CONSUMO,APPALTI_VARIAZIONI_IMPORTI.IMPORTO , " _
                                    & "IMPORTO_CANONE,(IMPORTO_CANONE-((IMPORTO_CONSUMO*SCONTO_CANONE)/100)) IMP_SCONTATO_CANONE, " _
                                    & "IVA_CANONE ,APPALTI_LOTTI_SERVIZI.ONERI_SICUREZZA_CANONE " _
                                    & "FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI," _
                                    & "SISCOM_MI.APPALTI_VARIAZIONI_IMPORTI,SISCOM_MI.APPALTI_VARIAZIONI " _
                                    & "WHERE APPALTI.ID_STRUTTURA = " & IdStruttura.Value & " AND APPALTI.ID = APPALTI_LOTTI_SERVIZI.ID_APPALTO AND " _
                                    & "APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO  =  " & row.Item("id_pf_voce_importo") _
                                    & "AND APPALTI_VARIAZIONI.ID_APPALTO(+) =APPALTI_LOTTI_SERVIZI.ID_APPALTO " _
                                    & "AND APPALTI_VARIAZIONI_IMPORTI.ID_VARIAZIONE(+) = APPALTI_VARIAZIONI.ID"
                da = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim ScontPlusOneri As Decimal
                Dim dt As New Data.DataTable()
                da.Fill(dt)

                For Each r As Data.DataRow In dt.Rows
                    ScontPlusOneri = par.IfNull(r.Item("IMP_SCONTATO_CONSUMO"), 0) + par.IfNull(r.Item("ONERI_SICUREZZA_CONSUMO"), 0)
                    TOTPRENOTATO = TOTPRENOTATO + (ScontPlusOneri + ((ScontPlusOneri * par.IfNull(r.Item("IVA_CONSUMO"), 0)) / 100))

                    ScontPlusOneri = par.IfNull(r.Item("IMP_SCONTATO_CANONE"), 0) + par.IfNull(r.Item("ONERI_SICUREZZA_CANONE"), 0)
                    TOTPRENOTATO = TOTPRENOTATO + (ScontPlusOneri + ((ScontPlusOneri * par.IfNull(r.Item("IVA_CANONE"), 0)) / 100))
                Next

                Dim Lettore As Oracle.DataAccess.Client.OracleDataReader

                'par.cmd.CommandText = "SELECT id_voce,pf_voci.codice,pf_voci.descrizione, " _
                '                    & "ROUND(SUM (VALORE_CONSUMO+((VALORE_CONSUMO*PF_VOCI_IMPORTO.IVA_CONSUMO)/100)+VALORE_CONSUMO_ASS+VALORE_canone+((VALORE_CANONE*PF_VOCI_IMPORTO.IVA_CANONE)/100)+VALORE_CANONE_ASS),4)AS TOT_SPENDIBILE " _
                '                    & "FROM siscom_mi.pf_voci_importo, siscom_mi.pf_voci " _
                '                    & "WHERE ID_voce IN( " _
                '                    & "SELECT DISTINCT(id_voce) " _
                '                    & "FROM siscom_mi.appalti_lotti_servizi, siscom_mi.pf_voci_importo " _
                '                    & "WHERE pf_voci_importo.ID = appalti_lotti_servizi.id_pf_voce_importo " _
                '                    & "AND id_appalto IN (SELECT ID FROM siscom_mi.appalti WHERE id_struttura = " & IdStruttura.Value & " AND ID_APPALTO = " & vIdAppalti & "))" _
                '                    & "AND pf_voci.ID = pf_voci_importo.id_voce " _
                '                    & "GROUP BY (id_voce,pf_voci.descrizione,pf_voci.codice)"

                '08/07/2011 prendo lo stanziato che viene scritto in pf_voci_struttura invece che in pf_voci_importo
                'par.cmd.CommandText = "SELECT id_voce,pf_voci.codice,pf_voci.descrizione, " _
                '    & "ROUND(SUM (VALORE_CONSUMO+((VALORE_CONSUMO*PF_VOCI_IMPORTO.IVA_CONSUMO)/100)+VALORE_CONSUMO_ASS+VALORE_canone+((VALORE_CANONE*PF_VOCI_IMPORTO.IVA_CANONE)/100)+VALORE_CANONE_ASS),4)AS TOT_SPENDIBILE " _
                '    & "FROM siscom_mi.pf_voci_importo, siscom_mi.pf_voci " _
                '    & "WHERE pf_voci_importo.ID= " & row.Item("id_pf_voce_importo") _
                '    & " AND pf_voci.ID = pf_voci_importo.id_voce " _
                '    & "GROUP BY (id_voce,pf_voci.descrizione,pf_voci.codice)"


                par.cmd.CommandText = "SELECT (nvl(valore_lordo,0) + nvl(assestamento_valore_lordo,0) + nvl(VARIAZIONI,0)) AS TOT_SPENDIBILE FROM siscom_mi.PF_VOCI_STRUTTURA WHERE id_struttura = " & IdStruttura.Value & " AND id_voce = (select id_voce from siscom_mi.pf_voci_importo where id = " & row.Item("id_pf_voce_importo") & ")"


                Lettore = par.cmd.ExecuteReader
                Dim testoMSG As String = ""
                If Lettore.Read Then
                    RESIDUO = RESIDUO + Lettore("TOT_SPENDIBILE")
                    'testoMSG = testoMSG & Lettore("CODICE") & " " & Lettore("DESCRIZIONE") & "\n"
                End If
                Lettore.Close()
                If RESIDUO > 0 Then
                    RESIDUO = RESIDUO - TOTPRENOTATO
                End If

                'RESIDUO = RESIDUO - CDec(CType(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCons"), TextBox).Text)

                If RESIDUO <= 0 Then
                    RadWindowManager1.RadAlert("ATTENZIONE!<br />Il Budget per gli importi  insufficente! <br />Ricordare di richiedere la modifica del budget!", 300, 150, "Attenzione", "", "null")
                    Exit For
                End If

            Next

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try
    End Sub
    'Private Sub ControlloCanone(ByRef Esito As Boolean)
    '    Try
    '        ' RIPRENDO LA CONNESSIONE
    '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '        par.SettaCommand(par)

    '        'RIPRENDO LA TRANSAZIONE
    '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '

    '        par.cmd.CommandText = "SELECT ROUND(NVL(SUM(IMPORTO_PRENOTATO),0),0) AS TOT_PRENOTATO " _
    '                            & "FROM SISCOM_MI.PRENOTAZIONI, SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
    '                            & "WHERE PRENOTAZIONI.ID_VOCE_PF_IMPORTO = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO " _
    '                            & "AND APPALTI_LOTTI_SERVIZI.ID_APPALTO = APPALTI.ID " _
    '                            & "AND APPALTI.ID_STRUTTURA = " & IdStruttura.Value

    '        Dim TOTPRENOTATO As Decimal = 0
    '        Dim RESIDUO As Decimal = 0
    '        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
    '        If Lettore.Read Then
    '            TOTPRENOTATO = Lettore("TOT_PRENOTATO")
    '        End If
    '        Lettore.Close()

    '        par.cmd.CommandText = "SELECT id_voce,pf_voci.codice,pf_voci.descrizione, " _
    '                            & "ROUND(SUM (VALORE_CANONE+((VALORE_CANONE*PF_VOCI_IMPORTO.IVA_CANONE)/100)),4)AS TOT_SPENDIBILE " _
    '                            & "FROM siscom_mi.pf_voci_importo, siscom_mi.pf_voci " _
    '                            & "WHERE ID_voce IN( " _
    '                            & "SELECT DISTINCT(id_voce) " _
    '                            & "FROM siscom_mi.appalti_lotti_servizi, siscom_mi.pf_voci_importo " _
    '                            & "WHERE pf_voci_importo.ID = appalti_lotti_servizi.id_pf_voce_importo " _
    '                            & "AND id_appalto IN (SELECT ID FROM siscom_mi.appalti WHERE id_struttura = " & IdStruttura.Value & ")) " _
    '                            & "AND pf_voci.ID = pf_voci_importo.id_voce " _
    '                            & "GROUP BY (id_voce,pf_voci.descrizione,pf_voci.codice)"
    '        Lettore = par.cmd.ExecuteReader
    '        While Lettore.Read
    '            RESIDUO = Lettore("TOT_SPENDIBILE")
    '        End While
    '        Lettore.Close()
    '        RESIDUO = RESIDUO - TOTPRENOTATO
    '        RESIDUO = RESIDUO - CDec(CType(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCons"), TextBox).Text)

    '        If RESIDUO <= 0 Then
    '            Esito = False
    '            Response.Write("<script>alert('ATTENZIONE!\nIl Budget per gli importi a  canone è insufficente!');</script>")

    '        End If


    '    Catch ex As Exception
    '        Me.LblErrore.Visible = True
    '        LblErrore.Text = ex.Message

    '    End Try
    'End Sub
#End Region


    Protected Sub btnFineContratto_Click(sender As Object, e As System.EventArgs) Handles btnFineContratto.Click

        Try


            'If Me.txtConfChiusura.Value = 1 Then
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'select sulle manutenzioni per vedere se STATO = 1
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND STATO = 1"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                RadWindowManager1.RadAlert("Non è possibile chiudere il contratto perchè sono presenti manutenzioni da consuntivare!", 300, 150, "Attenzione", "", "null")
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If
            myReader1.Close()


            par.cmd.CommandText = "select * from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_pagamento is null and id_stato <> -3"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                RadWindowManager1.RadAlert("Non è possibile chiudere il contratto perchè presenti prenotazioni non consuntivate!", 300, 150, "Attenzione", "", "null")
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub

            End If
            myReader1.Close()
            'par.cmd.CommandText = "update siscom_mi.prenotazioni set  id_stato = 0,importo_approvato = 0,importo_prenotato = 0 where id_appalto = " & vIdAppalti & " and id_pagamento is null"
            'par.cmd.ExecuteNonQuery()
            '************************scrittura evento chiusura contratto**************************************
            InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - CHIUSURA CONTRATTO")
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Me.lblStato.Text = "CHIUSO"
            Me.SOLO_LETTURA.Value = 1
            Me.txtModificato.Value = 1
            Update()
            Me.FrmSolaLettura()
            RadWindowManager1.RadAlert("Il contratto è stato chiuso!", 300, 150, "Attenzione", "", "null")



            'Else
            ' RadWindowManager1.RadAlert("Nessuna modifica apportata!", 300, 150, "Attenzione", "", "null")

            'End If

        Catch ex As Exception
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Session.Add("ERRORE", "Salva:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try



        '********COMMENTO TUTTO PERCHE' SPOSTATO NEL CONFERMA DEL DIV DI SCELTA DEL CONTO CORRENTE PER EMISSIONE PAGAMENTO RITENUTE DI LEGGE

        ''If Me.txtConfChiusura.Value = 1 Then
        ''    '*******************APERURA CONNESSIONE*********************
        ''    If par.OracleConn.State = Data.ConnectionState.Closed Then
        ''        par.OracleConn.Open()
        ''        par.SettaCommand(par)
        ''    End If

        ''    'select sulle manutenzioni per vedere se STATO = 1
        ''    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND STATO = 1"
        ''    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        ''    If myReader1.Read Then
        ''        Response.Write("<script>alert('Non è possibile chiudere il contratto perchè presente manutenzione attiva!');</script>")
        ''        myReader1.Close()
        ''        '*********************CHIUSURA CONNESSIONE**********************
        ''        par.OracleConn.Close()
        ''        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        ''        Exit Sub
        ''    End If
        ''    myReader1.Close()

        ''    If Me.chkRitenute.Checked = True Then
        ''        If ControlloCampi() = False Then
        ''            Exit Sub
        ''        End If
        ''        Dim Rimanente As Double = 0

        ''        'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID_PIANO_FINANZIARIO =" & txtIdPianoFinanziario.Value _
        ''        '    & " AND CODICE = '1.6'"
        ''        'myReader1 = par.cmd.ExecuteReader()

        ''        'If myReader1.Read Then

        ''        '***controllo che il valore preventivato di spesa esista e sia maggiore di 0
        ''        'Dim Stanziato As Double = 0
        ''        'Stanziato = CDbl(par.IfNull(myReader1("VALORE_LORDO"), 0) + par.IfNull(myReader1("ASSESTAMENTO_VALORE_LORDO"), 0))
        ''        'If Stanziato > 0 Then

        ''        ''********SOMMO TUTT I PAGAMENTI EFFETTUATI CON QUELLA VOCE DI PREVENTIVO DI SPESA DEL PIANO FINANZIARIO
        ''        'par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_PRENOTATO),0) as TOT_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_VOCE_PF = " & myReader1("ID") & " AND ID_PAGAMENTO IS NULL"
        ''        'Dim PagatiPrenotati As Double = 0
        ''        'Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        ''        'If lettore.Read Then
        ''        '    PagatiPrenotati = par.IfNull(lettore("TOT_PRENOTATO"), 0)
        ''        'End If
        ''        'lettore.Close()
        ''        'par.cmd.CommandText = "SELECT NVL(SUM(IMPORTO_CONSUNTIVATO),0) as TOT_PAGATO FROM SISCOM_MI.PAGAMENTI WHERE ID_VOCE_PF = " & myReader1("ID")
        ''        'lettore = par.cmd.ExecuteReader
        ''        'If lettore.Read Then
        ''        '    PagatiPrenotati = PagatiPrenotati + par.IfNull(lettore("TOT_PAGATO"), 0)

        ''        '    '*******Differenza fra preventivato e importi fino a ora pagati
        ''        '    Rimanente = Stanziato - PagatiPrenotati
        ''        '    '******Se il rimanente è positivo ed è superiore alla cifra da pagare procedo con il pagamento
        ''        '    If Rimanente >= 10 Then

        ''        '        If Rimanente >= CDbl(DirectCast(Tab_Appalto_generale.FindControl("txtfondoritenute"), TextBox).Text) Then

        ''        '******Scrittura del nuovo pagamento nell'apposita tabella!*******
        ''        Dim Pagamento As String = CreaPagamento("VUOTO")
        ''        If Pagamento <> "" Then
        ''            Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
        ''            'lettore.Close()
        ''            myReader1.Close()
        ''            '************************scrittura evento chiusura contratto**************************************
        ''            InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - CHIUSURA CONTRATTO")
        ''            '*********************CHIUSURA CONNESSIONE**********************
        ''            par.OracleConn.Close()
        ''            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        ''            Me.lblStato.Text = "CHIUSO"
        ''            Me.SOLO_LETTURA.Value = 1
        ''            Me.txtModificato.Value = 1
        ''            PdfPagamento(Pagamento)
        ''            Update()
        ''            Me.FrmSolaLettura()
        ''            Response.Write("<script>alert('Il contratto è stato chiuso!');</script>")
        ''            Response.Flush()
        ''            Exit Sub
        ''        Else
        ''            Response.Write("<script>alert('Inserire un fornitore e rieseguire l\'operazione!');</script>")
        ''            'lettore.Close()
        ''            myReader1.Close()

        ''            '*********************CHIUSURA CONNESSIONE**********************
        ''            par.OracleConn.Close()
        ''            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        ''        End If

        ''        '    Else
        ''        '        Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
        ''        '        Exit Sub
        ''        '        lettore.Close()
        ''        '        myReader1.Close()
        ''        '    End If
        ''        'Else
        ''        '    Response.Write("<script>alert('Sono stati spesi più soldi di quelli previsti nel Piano Finanziario!!Impossibile continuare!!');</script>")
        ''        '    Exit Sub
        ''        '    lettore.Close()
        ''        '    myReader1.Close()
        ''        'End If

        ''        'End If
        ''        'lettore.Close()
        ''        'Else
        ''        '    Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento!');</script>")
        ''        '    Exit Sub
        ''        '    myReader1.Close()
        ''        'End If
        ''        'Else
        ''        '    Response.Write("<script>alert('Nessun Piano Finanziario approvato per questa voce!');</script>")
        ''        '    Exit Sub

        ''        'End If
        ''        'myReader1.Close()
        ''    Else
        ''        '************************scrittura evento chiusura contratto**************************************
        ''        InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - CHIUSURA CONTRATTO")
        ''        par.OracleConn.Close()
        ''        par.OracleConn.Dispose()
        ''        Me.lblStato.Text = "CHIUSO"
        ''        Me.SOLO_LETTURA.Value = 1
        ''        Me.txtModificato.Value = 1
        ''        Update()
        ''        Me.FrmSolaLettura()
        ''        Response.Write("<script>alert('Il contratto è stato chiuso!');</script>")

        ''    End If

        ''Else
        ''    Response.Write("<script>alert('Nessuna modifica apportata!');</script>")

        ''End If
        ''If Me.lblStato.Text = "CHIUSO" Then
        ''    DirectCast(Me.Tab_ElencoPrezzi1.FindControl("btnApriAppalti"), ImageButton).Visible = False
        ''    DirectCast(Me.Tab_ElencoPrezzi1.FindControl("btnEliminaAppalti"), ImageButton).Visible = False

        ''    DirectCast(Me.Tab_Variazioni1.FindControl("btnEliminaServ"), ImageButton).Visible = False
        ''    DirectCast(Me.Tab_Variazioni1.FindControl("btnEliminaServCons"), ImageButton).Visible = False

        ''    DirectCast(Me.Tab_VariazioniLavori1.FindControl("btnEliminaLavCan"), ImageButton).Visible = False
        ''    DirectCast(Me.Tab_VariazioniLavori1.FindControl("btnEliminaLavCons"), ImageButton).Visible = False


        ''End If
    End Sub
    Private Sub PrenotaPagamento(ByVal id_pf_voce_importo As String, ByRef Esito As Boolean)
        Try
            'Esito è impostata di default a true

            Esito = True
            If CInt(par.IfEmpty(DirectCast(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text, 0)) > 0 Then


                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)




                par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO AS PF_VOCI_IMPORTO_ID,PF_VOCI_IMPORTO.ID_VOCE AS PF_VOCI_IMPORTO_IDVOCE," _
                    & "SISCOM_MI.APPALTI.ID_LOTTO,APPALTI_LOTTI_SERVIZI.importo_canone, APPALTI_LOTTI_SERVIZI.FREQUENZA_PAGAMENTO, " _
                    & "((APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE -((APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE * APPALTI_LOTTI_SERVIZI.SCONTO_CANONE)/100))) AS IMPORTO, APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN, APPALTI_LOTTI_SERVIZI.IVA_CANONE " _
                    & " FROM SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO  WHERE ID_APPALTO = " & vIdAppalti _
                    & " AND ID_PF_VOCE_IMPORTO = " & id_pf_voce_importo & " AND PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO AND APPALTI.ID = APPALTI_LOTTI_SERVIZI.ID_APPALTO "
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim IMPORTO As Decimal
                'Dim UltimoInserito As String
                Dim PF_ID_VOCE As String = ""

                '****Il numero per cui dividere l'importo del servizio da ripartire nei pagamenti
                'peppe modify 002/11/2010
                'il divisore abiamo deciso diventa il numero di mesi per cui vale l'applto 
                'Dim Divisore As Double = DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value / DirectCast(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue
                Dim Divisore As Double = DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value

                Dim DataScad As String = ""
                Dim stato As String = ""
                Dim AnnoEsercizio As String = ""
                Dim conditionEsercizio As String = ""
                If Session.Item("FL_COMI") = 1 Then
                    conditionEsercizio = "AND ID_STATO in (5,6)"
                Else
                    conditionEsercizio = "AND ID_STATO = 5"
                End If
                '****SELEZIONE DELLA'ANNO DELL'ESERCIZIO FINANZIARIO APPROVATO PER UN CONFRONTO CON L'ANNO DEL PAGAMENTO DA PRENOTARE!********
                par.cmd.CommandText = "SELECT SUBSTR(FINE,0,4)AS ANNO_ESERCIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.LOTTI,SISCOM_MI.PF_MAIN " _
                                        & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID " & conditionEsercizio & "  AND " _
                                        & "T_ESERCIZIO_FINANZIARIO.ID = LOTTI.ID_ESERCIZIO_FINANZIARIO AND LOTTI.ID = " _
                                        & "(SELECT DISTINCT ID_LOTTO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & ")"
                Dim LettoreAnnoEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreAnnoEsercizio.Read Then
                    AnnoEsercizio = LettoreAnnoEsercizio(0)
                Else
                    LettoreAnnoEsercizio.Close()

                    If idStatoPf.Value > 5 Then
                        par.cmd.CommandText = "SELECT SUBSTR(FINE,0,4)AS ANNO_ESERCIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                                            & "WHERE PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID AND ID_STATO = 5 "
                        LettoreAnnoEsercizio = par.cmd.ExecuteReader
                        If LettoreAnnoEsercizio.Read Then
                            AnnoEsercizio = LettoreAnnoEsercizio(0)
                        End If
                    Else
                        Exit Sub
                        RadWindowManager1.RadAlert("Nessun Esercizio Finanziario approvato trovato per attivare il contratto!", 300, 150, "Attenzione", "", "null")
                        LettoreAnnoEsercizio.Close()
                        Esito = False

                    End If
                End If
                LettoreAnnoEsercizio.Close()


                Dim TotaleAppalto As Double = 0
                '******PER OGNI SERVIZIO CON IMPORTO A CANONE INSERISCO UNA PRENOTAZIONE DI PAGAMENTO
                While MyReader.Read

                    If MyReader("FREQUENZA_PAGAMENTO") = 0 Then
                        Exit Sub
                    End If
                    '****PEPPE MODIFY 24/03/2011****
                    Dim PlusOneri As Decimal = 0
                    Dim PlusIvaOn As Decimal = 0
                    PlusOneri = (par.IfNull(MyReader("IMPORTO_CANONE"), 0) * par.IfNull(MyReader("PERC_ONERI_SIC_CAN"), 0)) / 100 'Sull'importo netto calcolo la quota di oneri da sommare
                    PlusIvaOn = ((par.IfNull(MyReader("IMPORTO"), 0) + PlusOneri) * par.IfNull(MyReader("IVA_CANONE"), 0)) / 100
                    TotaleAppalto = par.IfNull(MyReader("IMPORTO"), 0) + PlusOneri + PlusIvaOn
                    '******************AGGIUNGO LA PERCENTUALE DI ONERI SUL SERVIZIO
                    'IMPORTO = par.IfNull(MyReader("IMPORTO"), 0) + ((par.IfNull(MyReader("IMPORTO"), 0) * par.IfNull(MyReader("PERC_ONERI_SIC_CAN"), 0)) / 100)
                    '****PEPPE MODIFY 02/11/2010
                    '****MOLTIPLICO L'IMPORTO MENSILE DA VERSARE PER IL NUMERO DI RATE

                    IMPORTO = (TotaleAppalto / Divisore) * par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0)
                    Dim i As Integer = 0
                    Dim nuovoAnno As Integer = 0
                    DataScad = Me.txtannoinizio.SelectedDate
                    '**** PEPPE MODIFY 
                    vMonteVirgole = 0
                    For i = 1 To Fix(((Divisore / par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0)) * 100) / 100)





                        If (nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)).Substring(0, 4) <= AnnoEsercizio And (nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)).Substring(0, 4) = par.AggiustaData(DataScad).Substring(0, 4) Then
                            '*******NORMALE PRENOTAZIONE PAGAMENTO
                            stato = "0"
                            DataScad = nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)
                            Dim ritenute As Decimal = OttieniRitenutaIvata(Fix((IMPORTO * 100) / 100), par.IfNull(MyReader("IVA_CANONE"), 0))


                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                            & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((IMPORTO * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                            & MyReader("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO'," & DataScad & "," _
                            & MyReader("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "," & par.VirgoleInPunti(par.IfNull(MyReader("IVA_CANONE"), 0)) & ")"
                            par.cmd.ExecuteNonQuery()

                            DataScad = Date.Parse(par.FormattaData(DataScad), New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")

                            vMonteVirgole = vMonteVirgole + Math.Round((IMPORTO - Fix((IMPORTO * 100) / 100)), 4)

                        Else

                            'Il nuovoAnno è l'anno per il quale devo prenotare i pagamenti se supera quello dell'esercizio
                            If nuovoAnno = 0 And nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad).Substring(0, 4) > nuovoAnno Then
                                'PRENOTAZIONE QUANDO SCATTA IL NUOVO ANNO DOPO TUTTE QUELLE NORMALI
                                '*******Devo inserire una prenotazione per il 31/12 dell'esercizio in corso con l'importo di conguaglio di competenza dell'esercizio stesso
                                Dim DayLeftToEnd As Integer
                                Dim ImpACavllo As Decimal

                                'If par.AggiustaData(DataScad).Substring(4, 2) = 12 Then
                                'sono i giorni che mancano alla fine dell'anno
                                DayLeftToEnd = DateDiff(DateInterval.Day, CDate("" & par.FormattaData(DataScad) & ""), CDate("31/12/" & AnnoEsercizio))
                                If DayLeftToEnd > 0 Then

                                    ImpACavllo = ((IMPORTO / par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0)) / 30) * DayLeftToEnd
                                    'Else
                                    '    MonthLeftToEnd = 12 - par.AggiustaData(DataScad).Substring(4, 2)
                                    '    ImpACavllo = (IMPORTO / DirectCast(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) * MonthLeftToEnd
                                    'End If

                                    Dim AnnoToLeft As Integer = (nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)).Substring(0, 4) - 1
                                    If AnnoToLeft = AnnoEsercizio Or AnnoToLeft = par.AggiustaData(DataScad).Substring(0, 4) Then
                                        stato = 0
                                    Else
                                        stato = -1
                                    End If
                                    Dim ritenute As Decimal = OttieniRitenutaIvata(Fix((ImpACavllo * 100) / 100), par.IfNull(MyReader("IVA_CANONE"), 0))

                                    'Prenotazione al 31/12 dell'anno X
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                    & MyReader("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'IMPORTO FINE E.F SU CONTRATTO DI APPALTO E CONGUAGLIO ARROTONDAMENTI'," & par.AggiustaData("31/12/" & AnnoToLeft & "") & "," _
                                    & MyReader("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "," & par.VirgoleInPunti(par.IfNull(MyReader("IVA_CANONE"), 0)) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    vMonteVirgole = vMonteVirgole + Math.Round((ImpACavllo - Fix((ImpACavllo * 100) / 100)), 4)


                                    '**************DEVO PRENOTARE GLI IMPORTI ARROTONDATI OGNI FINE ANNO

                                    'Aggiornamento sull'ultima prenotazione per eliminazione errori di arrotondamento
                                    par.cmd.CommandText = "Select Sum(importo_prenotato)AS TOT_PRENOTATO ,MAX(ID) AS ID_DA_AGGIORNARE from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_voce_pf_importo = " & MyReader("PF_VOCI_IMPORTO_ID")
                                    Dim LettScritto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettScritto.Read Then
                                        par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND ID = " & par.IfNull(LettScritto("ID_DA_AGGIORNARE"), 0)
                                        Dim LettoreAggiornamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        If LettoreAggiornamento.Read Then
                                            ritenute = OttieniRitenutaIvata(vMonteVirgole, par.IfNull(MyReader("IVA_CANONE"), 0))
                                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 2)) & ",RIT_LEGGE_IVATA = NVL(RIT_LEGGE_IVATA,0) + " & par.VirgoleInPunti(Math.Round(ritenute, 2)) & " WHERE ID =" & LettScritto("ID_DA_AGGIORNARE")
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                        LettoreAggiornamento.Close()
                                    End If
                                    LettScritto.Close()

                                    vMonteVirgole = 0
                                    '***************************************************************************************************************************************************************************
                                    'Prenotazione alla scadenza calcolata per il rimanente
                                    stato = "-1"
                                    DataScad = nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)
                                    'DataScad = par.AggiustaData(DataScad)
                                    nuovoAnno = DataScad.Substring(0, 4)

                                    ImpACavllo = IMPORTO - ImpACavllo
                                    ritenute = OttieniRitenutaIvata(ImpACavllo, par.IfNull(MyReader("IVA_CANONE"), 0))
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                    & MyReader("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO '," & DataScad & "," _
                                    & MyReader("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "," & par.VirgoleInPunti(par.IfNull(MyReader("IVA_CANONE"), 0)) & ")"
                                    par.cmd.ExecuteNonQuery()

                                    DataScad = Date.Parse(par.FormattaData(DataScad), New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")
                                    'DataScad = par.FormattaData(DataScad)
                                    vMonteVirgole = vMonteVirgole + Math.Round(ImpACavllo - Fix((ImpACavllo * 100) / 100), 4)
                                Else
                                    stato = "-1"
                                    DataScad = nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)
                                    'DataScad = par.AggiustaData(DataScad)
                                    nuovoAnno = DataScad.Substring(0, 4)
                                    DataScad = Date.Parse(par.FormattaData(DataScad), New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")
                                    'DataScad = par.FormattaData(DataScad)
                                    vMonteVirgole = vMonteVirgole + Math.Round(ImpACavllo - Fix((ImpACavllo * 100) / 100), 4)

                                End If

                            Else

                                'PRENOTAZIONE DELLE RATE CHE VANNO dall'ANNO SUCCESSIVO A QUELLO SUCCESSIVO (es. inizio 2010, prenotazione 2011, in questo caso entra quando prenoterà l'importo, se esiste, successivo al 2011) ;-)
                                '********ATTENZIONE!!!*********
                                '*******NORMALE PRENOTAZIONE PAGAMENTO
                                If nuovoAnno > 0 And nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad).Substring(0, 4) <= nuovoAnno Then

                                    stato = "-1"
                                    DataScad = nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)
                                    Dim ritenute As Decimal = OttieniRitenutaIvata(Fix((IMPORTO * 100) / 100), par.IfNull(MyReader("IVA_CANONE"), 0))
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((IMPORTO * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                    & MyReader("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO'," & DataScad & "," _
                                    & MyReader("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "," & par.VirgoleInPunti(par.IfNull(MyReader("IVA_CANONE"), 0)) & ")"
                                    par.cmd.ExecuteNonQuery()
                                    DataScad = Date.Parse(par.FormattaData(DataScad), New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")

                                    vMonteVirgole = vMonteVirgole + Math.Round((IMPORTO - Fix((IMPORTO * 100) / 100)), 4)
                                Else

                                    'PRENOTAZIONE QUANDO SCATTA IL NUOVO ANNO DOPO TUTTE QUELLE NORMALI
                                    '*******Devo inserire una prenotazione per il 31/12 dell'esercizio in corso con l'importo di conguaglio di competenza dell'esercizio stesso
                                    Dim DayLeftToEnd As Integer
                                    Dim ImpACavllo As Decimal

                                    'If par.AggiustaData(DataScad).Substring(4, 2) = 12 Then
                                    'sono i giorni che mancano alla fine dell'anno
                                    DayLeftToEnd = DateDiff(DateInterval.Day, CDate("" & par.FormattaData(DataScad) & ""), CDate("31/12/" & nuovoAnno & ""))
                                    ImpACavllo = ((IMPORTO / par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0)) / 30) * DayLeftToEnd
                                    'Else
                                    '    MonthLeftToEnd = 12 - par.AggiustaData(DataScad).Substring(4, 2)
                                    '    ImpACavllo = (IMPORTO / DirectCast(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) * MonthLeftToEnd
                                    'End If

                                    Dim AnnoToLeft As Integer = (nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)).Substring(0, 4) - 1
                                    'stato = "-1"
                                    Dim ritenute As Decimal = OttieniRitenutaIvata(Fix((ImpACavllo * 100) / 100), par.IfNull(MyReader("IVA_CANONE"), 0))
                                    'Prenotazione al 31/12 dell'anno X
                                    If ImpACavllo > 0 Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                                        & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                        & MyReader("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'IMPORTO FINE E.F SU CONTRATTO DI APPALTO E CONGUAGLIO ARROTONDAMENTI'," & par.AggiustaData("31/12/" & AnnoToLeft & "") & "," _
                                        & MyReader("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "," & par.VirgoleInPunti(par.IfNull(MyReader("IVA_CANONE"), 0)) & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                    vMonteVirgole = vMonteVirgole + Math.Round((ImpACavllo - Fix((ImpACavllo * 100) / 100)), 4)


                                    '**************DEVO PRENOTARE GLI IMPORTI ARROTONDATI OGNI FINE ANNO

                                    'Aggiornamento sull'ultima prenotazione per eliminazione errori di arrotondamento
                                    par.cmd.CommandText = "Select Sum(importo_prenotato)AS TOT_PRENOTATO ,MAX(ID) AS ID_DA_AGGIORNARE from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_voce_pf_importo = " & MyReader("PF_VOCI_IMPORTO_ID")
                                    Dim LettScritto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettScritto.Read Then
                                        par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND ID = " & par.IfNull(LettScritto("ID_DA_AGGIORNARE"), 0)
                                        Dim LettoreAggiornamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        If LettoreAggiornamento.Read Then
                                            vMonteVirgole = Fix((vMonteVirgole * 100) / 100)

                                            ritenute = OttieniRitenutaIvata(vMonteVirgole, par.IfNull(MyReader("IVA_CANONE"), 0))
                                            'par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 8)) & " WHERE ID =" & LettScritto("ID_DA_AGGIORNARE")
                                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 2)) & ",RIT_LEGGE_IVATA = NVL(RIT_LEGGE_IVATA,0) + " & par.VirgoleInPunti(Math.Round(ritenute, 2)) & " WHERE ID =" & LettScritto("ID_DA_AGGIORNARE")
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                        LettoreAggiornamento.Close()
                                    End If
                                    LettScritto.Close()

                                    vMonteVirgole = 0
                                    '***************************************************************************************************************************************************************************
                                    'Prenotazione alla scadenza calcolata per il rimanente
                                    stato = "-1"
                                    DataScad = nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)
                                    'DataScad = nuovaData(par.IfNull(MyReader("FREQUENZA_PAGAMENTO"), 0), DataScad)
                                    nuovoAnno = DataScad.Substring(0, 4)

                                    ImpACavllo = IMPORTO - ImpACavllo
                                    ritenute = OttieniRitenutaIvata(ImpACavllo, par.IfNull(MyReader("IVA_CANONE"), 0))
                                    If ImpACavllo > 0 Then
                                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                                        & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                        & MyReader("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO '," & DataScad & "," _
                                        & MyReader("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "," & par.VirgoleInPunti(par.IfNull(MyReader("IVA_CANONE"), 0)) & ")"
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                    DataScad = Date.Parse(par.FormattaData(DataScad), New System.Globalization.CultureInfo("it-IT", False)).AddDays(1).ToString("dd/MM/yyyy")
                                    'DataScad = par.FormattaData(DataScad)

                                    vMonteVirgole = vMonteVirgole + Math.Round(ImpACavllo - Fix((ImpACavllo * 100) / 100), 4)


                                End If

                            End If



                        End If



                    Next

                    'Aggiornamento sull'ultima prenotazione per eliminazione errori di arrotondamento
                    par.cmd.CommandText = "Select Sum(importo_prenotato)AS TOT_PRENOTATO ,MAX(ID) AS ID_DA_AGGIORNARE from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_voce_pf_importo = " & MyReader("PF_VOCI_IMPORTO_ID")
                    Dim TotPrenotato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If TotPrenotato.Read Then
                        If par.IfNull(TotPrenotato("ID_DA_AGGIORNARE"), 0) > 0 Then
                            par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND ID = " & par.IfNull(TotPrenotato("ID_DA_AGGIORNARE"), 0)
                            Dim LettoreAggiornamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                            If LettoreAggiornamento.Read Then
                                vMonteVirgole = Fix((vMonteVirgole * 100) / 100)

                                Dim ritenute As Decimal = OttieniRitenutaIvata(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 2), par.IfNull(MyReader("IVA_CANONE"), 0))

                                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 2)) & ",RIT_LEGGE_IVATA = " & par.VirgoleInPunti(Math.Round(ritenute, 2)) & "  WHERE ID =" & TotPrenotato("ID_DA_AGGIORNARE")
                                par.cmd.ExecuteNonQuery()
                            End If
                            LettoreAggiornamento.Close()
                        End If
                    End If
                    TotPrenotato.Close()

                    vMonteVirgole = 0

                    '*************************************************
                    '*************************************************
                    '************''Regulating Prenotation*************
                    '*************************************************
                    '*************************************************

                    par.cmd.CommandText = "SELECT SUM(IMPORTO_PRENOTATO) AS PRENOTATO, COUNT(ID) AS NUMERO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " and id_voce_pf_importo = " & MyReader("PF_VOCI_IMPORTO_ID")
                    Dim Prenotato As Decimal = 0
                    Dim Nprenotation As Integer = 0
                    Dim AddPrenotation As Decimal = 0
                    Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If reader.Read Then
                        Prenotato = par.IfNull(reader("PRENOTATO"), 0)
                        Nprenotation = par.IfNull(reader("NUMERO"), 0)
                    End If
                    reader.Close()
                    '19/03/2012 aggiorno solo la prima prenotazione in modo da aumentare la precisione delle prenotazioni al centesimo.
                    If TotaleAppalto > Prenotato Then

                        'AddPrenotation = (TotaleAppalto - Prenotato) / Nprenotation
                        AddPrenotation = (TotaleAppalto - Prenotato)
                        'AddPrenotation = Fix((AddPrenotation * 100) / 100)
                        par.cmd.CommandText = "SELECT ID,IMPORTO_PRENOTATO AS PRENOTATO FROM SISCOM_MI.PRENOTAZIONI  WHERE ID_APPALTO = " & vIdAppalti & " and id_voce_pf_importo = " & MyReader("PF_VOCI_IMPORTO_ID") & " order by id asc"
                        reader = par.cmd.ExecuteReader
                        If reader.Read Then
                            Dim ritenute As Decimal = OttieniRitenutaIvata(AddPrenotation, par.IfNull(MyReader("IVA_CANONE"), 0))
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti((par.IfNull(reader("PRENOTATO"), 0) + AddPrenotation)) & ",RIT_LEGGE_IVATA = NVL(RIT_LEGGE_IVATA,0) + " & par.VirgoleInPunti(Math.Round(ritenute, 2)) & " WHERE ID =" & reader("ID")
                            par.cmd.ExecuteNonQuery()
                        End If
                        'While reader.Read
                        'Dim ritenute As Decimal = OttieniRitenutaIvata(AddPrenotation, par.IfNull(MyReader("IVA_CANONE"), 0))
                        ''par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti((par.IfNull(reader("PRENOTATO"), 0) + AddPrenotation)) & " WHERE ID = " & reader("ID")
                        'par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti((par.IfNull(reader("PRENOTATO"), 0) + AddPrenotation)) & ",RIT_LEGGE_IVATA = NVL(RIT_LEGGE_IVATA,0) + " & par.VirgoleInPunti(ritenute) & " WHERE ID =" & reader("ID")
                        'par.cmd.ExecuteNonQuery()
                        'End While
                        reader.Close()
                    End If
                    '*************************************************
                    '*************************************************
                    '************''Regulating Prenotation*************
                    '*************************************************
                    '*************************************************
                End While
                MyReader.Close()

            End If


        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            Esito = False
            Exit Sub
            Me.btnSalva.Enabled = False
        End Try

    End Sub
    Private Sub PrenotaScadManuale(ByVal id_pf_voce_importo As String, ByRef Esito As Boolean)
        Try
            'Esito è impostata di default a true
            Dim Divisore As Integer = 0
            Dim TotAppalto As Decimal = 0
            Dim IMPORTO As Decimal = 0
            Dim stato As String = "0"
            Esito = True
            If CInt(par.IfEmpty(DirectCast(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text, 0)) > 0 Then
                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim AnnoEsercizio As String = ""
                '****SELEZIONE DELLA'ANNO DELL'ESERCIZIO FINANZIARIO APPROVATO PER UN CONFRONTO CON L'ANNO DEL PAGAMENTO DA PRENOTARE!********
                par.cmd.CommandText = "SELECT SUBSTR(FINE,0,4)AS ANNO_ESERCIZIO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO, SISCOM_MI.LOTTI " _
                    & "WHERE T_ESERCIZIO_FINANZIARIO.ID = LOTTI.ID_ESERCIZIO_FINANZIARIO AND LOTTI.ID = (SELECT DISTINCT ID_LOTTO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & ")"
                Dim LettoreAnnoEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreAnnoEsercizio.Read Then
                    AnnoEsercizio = LettoreAnnoEsercizio(0)
                Else
                    Exit Sub
                    RadWindowManager1.RadAlert("Nessun Esercizio Finanziario approvato trovato per attivare il contratto!", 300, 150, "Attenzione", "", "null")
                    Esito = False
                    LettoreAnnoEsercizio.Close()
                End If
                LettoreAnnoEsercizio.Close()



                par.cmd.CommandText = "SELECT COUNT(APPALTI_SCADENZE.ID_APPALTO) AS NUM FROM SISCOM_MI.APPALTI_SCADENZE,SISCOM_MI.APPALTI_LOTTI_SERVIZI " _
                                    & "WHERE APPALTI_LOTTI_SERVIZI.ID_APPALTO = APPALTI_SCADENZE.ID_APPALTO AND " _
                                    & "APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO =" & id_pf_voce_importo & " AND APPALTI_LOTTI_SERVIZI.ID_APPALTO = " & vIdAppalti
                Dim L As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If L.Read Then
                    Divisore = par.IfNull(L("NUM"), 0)
                End If
                L.Close()
                If Divisore <= 0 Then
                    RadWindowManager1.RadAlert("Impossibile attivare il contratto!<br />Se si è scelta una scadenza manuale delle rate,definire le date di scadenza e gli importi delle rate!", 300, 150, "Attenzione", "", "null")
                    Esito = False
                    'se Esito è false allora viene interrota l'operazione gestita dall'hendler dell'evento che scatta sul click del bottone AttivaContratto
                    Exit Sub
                End If
                'par.cmd.CommandText = "SELECT SUM(IMPORTO) as TOT_RATE FROM SISCOM_MI.APPALTI_SCADENZE WHERE ID_APPALTO = " & vIdAppalti
                'L = par.cmd.ExecuteReader
                'If L.Read Then
                '    TotAppalto = par.IfNull(L("TOT_RATE"), 0)
                'End If
                'L.Close()
                'If TotAppalto > (par.IfEmpty(DirectCast(Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCan"), TextBox).Text, 0)) Then
                '    Response.Write("<script>alert('Impossibile attivare il contratto!\nGli importi delle rate prenotate sono SUPERIORI al totale dell\'applato!');</script>")
                '    Esito = False
                '    'se Esito è false allora viene interrota l'operazione gestita dall'hendler dell'evento che scatta sul click del bottone AttivaContratto
                '    Exit Sub
                'End If

                par.cmd.CommandText = "SELECT ID_PF_VOCE_IMPORTO AS PF_VOCI_IMPORTO_ID,PF_VOCI_IMPORTO.ID_VOCE AS PF_VOCI_IMPORTO_IDVOCE," _
                    & "SISCOM_MI.APPALTI.ID_LOTTO,APPALTI_LOTTI_SERVIZI.importo_canone," _
                    & "((APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE -((APPALTI_LOTTI_SERVIZI.IMPORTO_CANONE * APPALTI_LOTTI_SERVIZI.SCONTO_CANONE)/100))) AS IMPORTO, APPALTI_LOTTI_SERVIZI.PERC_ONERI_SIC_CAN, APPALTI_LOTTI_SERVIZI.IVA_CANONE " _
                    & " FROM SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.PF_VOCI_IMPORTO  WHERE ID_APPALTO = " & vIdAppalti & " AND APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO = " & id_pf_voce_importo & " AND PF_VOCI_IMPORTO.ID = APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO AND APPALTI.ID = APPALTI_LOTTI_SERVIZI.ID_APPALTO "
                Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                Dim dt As New Data.DataTable()
                da.Fill(dt)

                'For Each r As Data.DataRow In dt.Rows

                'Next

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.APPALTI_SCADENZE WHERE ID_APPALTO = " & vIdAppalti & " AND APPALTI_SCADENZE.ID_PF_VOCE_IMPORTO = " & id_pf_voce_importo
                L = par.cmd.ExecuteReader

                While L.Read
                    'If AnnoEsercizio = L("SCADENZA").Substring(0, 4) Thenhttp://localhost:5993/CM_V2_2008/CICLO_PASSIVO/CicloPassivo/APPALTI/Appalti.aspx.vb
                    '    stato = "-1"
                    'Else
                    '    stato = "-1"
                    'End If
                    stato = 0
                    If dt.Rows.Count > 1 Then
                        Dim impDiviso As Decimal = 0

                        For Each r As Data.DataRow In dt.Rows
                            impDiviso = (TotAppalto * Math.Round((par.IfEmpty(r.Item("IMPORTO_CANONE"), 0) * 100) / par.IfEmpty(DirectCast(Tab_Appalto_generale.FindControl("txtastacanone"), TextBox).Text, 0), 4)) / 100
                            impDiviso = Math.Round(impDiviso, 4)

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA) VALUES" _
                                                & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Math.Round(impDiviso, 2)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                                & r.Item("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO'," & L("SCADENZA") & "," _
                                                & r.Item("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & ")"
                            par.cmd.ExecuteNonQuery()

                        Next

                    Else
                        Dim ritenute As String
                        If Me.chkRitenute.Checked = True Then
                            ritenute = OttieniRitenutaIvata(par.IfEmpty(L("IMPORTO"), 0), par.IfNull(dt.Rows(0).Item("IVA_CANONE"), 0))
                            ritenute = par.VirgoleInPunti(Math.Round(CDec(ritenute), 2))
                        Else
                            ritenute = "Null"
                        End If

                        par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO,ID_STRUTTURA,RIT_LEGGE_IVATA,PERC_IVA) VALUES" _
                                            & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(par.IfEmpty(L("IMPORTO"), 0)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                            & dt.Rows(0).Item("PF_VOCI_IMPORTO_IDVOCE") & ", 6," & stato & ", 'CONTRATTO DI APPALTO'," & L("SCADENZA") & "," _
                                            & dt.Rows(0).Item("PF_VOCI_IMPORTO_ID") & "," & Me.IdStruttura.Value & "," & ritenute & "," & dt.Rows(0).Item("IVA_CANONE") & ")"
                        par.cmd.ExecuteNonQuery()

                    End If

                End While


            End If
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
            Me.btnSalva.Enabled = False
        End Try
    End Sub
    'Private Function ControllaFrequenza(ByVal Ripartizio As String) As Boolean
    '    ControllaFrequenza = False
    '    Select Case Ripartizio.ToUpper
    '        Case "MENSILE"
    '            If DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value >= 1 Then
    '                ControllaFrequenza = True
    '            Else
    '                ControllaFrequenza = True
    '            End If
    '        Case "BIMESTRALE"
    '            If DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value >= 2 Then
    '                ControllaFrequenza = True
    '            Else
    '                ControllaFrequenza = True
    '            End If
    '        Case "TRIMESTRALE"
    '            If DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value >= 3 Then
    '                ControllaFrequenza = True
    '            Else
    '                ControllaFrequenza = True
    '            End If
    '        Case "QUADRIMESTRALE"
    '            If DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value >= 4 Then
    '                ControllaFrequenza = True
    '            Else
    '                ControllaFrequenza = True
    '            End If
    '        Case "SEMESTRALE"
    '            If DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value >= 6 Then
    '                ControllaFrequenza = True
    '            Else
    '                ControllaFrequenza = True
    '            End If
    '        Case "ANNUALE"
    '            If DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value >= 12 Then
    '                ControllaFrequenza = True
    '            Else
    '                ControllaFrequenza = True
    '            End If
    '    End Select


    '    Return ControllaFrequenza
    'End Function
    Private Function DivisoreRate(ByVal Ripartizio As String) As Integer
        DivisoreRate = -1
        Select Case Ripartizio.ToUpper
            Case "MENSILE"
                DivisoreRate = 12
            Case "BIMESTRALE"
                DivisoreRate = 6
            Case "TRIMESTRALE"
                DivisoreRate = 4
            Case "QUADRIMESTRALE"
                DivisoreRate = 3
            Case "SEMESTRALE"
                DivisoreRate = 2
            Case "ANNUALE"
                DivisoreRate = 1
        End Select

        Return DivisoreRate
    End Function

    Private Function ottieniScadenza(ByVal DataAggiustata As String, ByVal T As Integer) As String
        Dim anno As Integer = DataAggiustata.Substring(0, 4)
        Dim month As Integer = DataAggiustata.Substring(4, 2)
        Dim giorno As Integer = DataAggiustata.Substring(6, 2)
        ottieniScadenza = ""
        Dim AddMonth As Integer
        Dim i As Integer
        i = 0
        If T = 0 Then
            ottieniScadenza = anno & MeseQuery(month) & GiornoQuery(giorno)
        Else

            AddMonth = T
            ottieniScadenza = Date.Parse(par.FormattaData(DataAggiustata), New System.Globalization.CultureInfo("it-IT", False)).AddMonths(T).AddDays(-1).ToString("dd/MM/yyyy")



            ottieniScadenza = par.AggiustaData(ottieniScadenza)




            'If month + AddMonth <= 12 Then
            '    ottieniScadenza = anno & MeseQuery(month + AddMonth) & GiornoQuery(giorno)
            'Else
            '    Dim finito As Boolean = False
            '    Dim M As Integer = (month + AddMonth) - 12
            '    While finito = False
            '        i = i + 1
            '        If M <= 11 Then
            '            anno = anno + i
            '            finito = True
            '        Else
            '            M = M - 12
            '        End If
            '    End While
            '    ottieniScadenza = anno & MeseQuery(M) & GiornoQuery(giorno)
            'End If
        End If
        Return ottieniScadenza
    End Function
    Private Function MeseQuery(ByVal month As Integer) As String
        MeseQuery = ""
        If month.ToString.Length <= 1 Then
            MeseQuery = 0 & month
        Else
            MeseQuery = month
        End If
        Return MeseQuery
    End Function
    Private Function GiornoQuery(ByVal day As Integer) As String
        GiornoQuery = ""
        If day.ToString.Length <= 1 Then
            GiornoQuery = 0 & day
        Else
            GiornoQuery = day
        End If
        Return GiornoQuery
    End Function
    Private Function nuovaData(ByVal Ripartizio As Integer, ByVal Data As String) As String
        nuovaData = -1


        Select Case Ripartizio
            Case 1
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 1)
            Case 2
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 2)
            Case 3
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 3)
            Case 4
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 4)
            Case 6
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 6)
            Case 12
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 11)
        End Select

        Return nuovaData

    End Function


    Private Function CreaPagamento(ByVal VocePF As String) As String

        Try
            Dim Id_Fornitore As String = Me.cmbfornitore.SelectedValue.ToString
            Dim Id_Pagamento As String = ""
            'par.cmd.CommandText = "SELECT FORNITORI.ID AS ID_FORNITORE FROM SISCOM_MI.FORNITORI WHERE ID = (SELECT ID_FORNITORE FROM SISCOM_MI.CONDOMINI WHERE ID = " & idCond.Value & ")"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If myReader1.Read Then
            '    Id_Fornitore = par.IfNull(myReader1("ID_FORNITORE"), "Null")
            'End If
            'myReader1.Close()
            If vIdAppalti > 0 Then

                par.cmd.CommandText = "SELECT SISCOM_MI.SEQ_PAGAMENTI.NEXTVAL FROM DUAL"
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    Id_Pagamento = myReader1(0)
                End If

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DATA_STAMPA,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO,CONTO_CORRENTE) " _
                & " VALUES (" & Id_Pagamento & "," & Format(Now, "yyyyMMdd") & ", " & Format(Now, "yyyyMMdd") & ",'PAGAMENTO RITENUTA DI LEGGE " & par.PulisciStrSql(Me.txtnumero.Text) & "/" & Me.txtdatarepertorio.SelectedDate.Value.ToString.Substring(6) & "'," & par.VirgoleInPunti(DirectCast(Tab_Appalto_generale.FindControl("txtfondoritenute"), TextBox).Text.Replace(".", "")) & "," & Id_Fornitore & "," & vIdAppalti & ",1,9,'" & par.PulisciStrSql(Me.CmbContoCorrente.SelectedValue.ToString) & "')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET ID_PAGAMENTO_RIT_LEGGE = " & Id_Pagamento & " WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and id_pagamento in (select id from siscom_mi.pagamenti where id=prenotazioni.id_pagamento and id_stato>0)"
                par.cmd.ExecuteNonQuery()

                '****Scrittura evento EMISSIONE DEL PAGAMENTO
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) VALUES ( " & Id_Pagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','')"
                par.cmd.ExecuteNonQuery()
            Else
                RadWindowManager1.RadAlert("Non esiste un fornitore per emettere il pagamento!Impossibile procedere", 300, 150, "Attenzione", "", "null")

                Id_Pagamento = ""
            End If

            Return Id_Pagamento
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try

    End Function


#Region "CorreggiPercenOneri"
    'Function CorreggiPerc(ByVal ImpNetto As Decimal, ByVal oneri As Decimal) As Decimal
    '    CorreggiPerc = 0
    '    If ImpNetto > 0 Then
    '        CorreggiPerc = (oneri * 100) / ImpNetto
    '    End If

    '    Return Math.Round(CorreggiPerc, 8)

    'End Function

    'Function ReplacePerc(ByVal ImpLordo As Decimal, ByVal oneri As Decimal) As Decimal
    '    ReplacePerc = 0

    '    If ImpLordo > 0 Then
    '        ReplacePerc = (oneri * 100) / ImpLordo
    '    End If


    '    Return Math.Round(ReplacePerc, 8)
    'End Function

#End Region


    Public Sub PdfPagamento(ByVal ID As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoRitLegge.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
            Dim contenuto As String = sr1.ReadToEnd()
            sr1.Close()
            Dim InizioES As String = ""
            Dim FineEs As String = ""
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            Dim tb1 As String = "<table style='width:100%;'>"
            Dim tb2 As String = "<table style='width:100%;'>"

            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE ID = " & Me.cmbfornitore.SelectedValue.ToString
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CONDOMINI WHERE ID =" & idCond.Value
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$chiamante$", "CONTRATTO:")
                contenuto = Replace(contenuto, "$dettaglio$", par.IfNull(myReader1("RAGIONE_SOCIALE"), "--"))
                contenuto = Replace(contenuto, "$copia$", "")
                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>  " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "--") & "</td>"
                tb1 = tb1 & "</tr>"
                'For I As Integer = 0 To Me.chkIbanF.Items.Count() - 1

                'If chkIbanF.Items(I).Selected = True Then
                '    tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>  IBAN :" & chkIbanF.Items(I).Text & " </td>"
                '    tb1 = tb1 & "</tr>"
                'End If
                'Next
                tb1 = tb1 & "</table>"
                '*****************FINE SCRITTURA DETTAGLI
            End If
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,siscom_mi.PF_MAIN WHERE PF_MAIN.ID = " & txtIdPianoFinanziario.Value & " AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                InizioES = par.FormattaData(myReader1("INIZIO"))
                FineEs = par.FormattaData(myReader1("FINE"))
            End If
            myReader1.Close()
            contenuto = Replace(contenuto, "$dettagli_chiamante$", txtnumero.Text.ToUpper)
            contenuto = Replace(contenuto, "$cig$", txtCIG.Text.ToUpper)
            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")

            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(ID, True))

            Dim idvocePf As String = ""
            'par.cmd.CommandText = "SELECT SUM (ROUND (rit_legge_ivata * 100/ (100 + perc_iva), 2)) AS IMPONIBILE,SUM (round((ROUND (rit_legge_ivata * 100 / (100 + perc_iva), 2)*perc_iva/100),2)) AS iva, SUM (ROUND (rit_legge_ivata * 100 / (100 + perc_iva), 2))+ SUM (round((ROUND (rit_legge_ivata * 100 / (100 + perc_iva), 2)*perc_iva/100),2)) AS totale FROM prenotazioni where id_pagamento_rit_legge=" & ID
            'par.cmd.CommandText = "SELECT ROUND (IMPORTO_CONSUNTIVATO * 100 / (  (SELECT MAX (PRENOTAZIONI.PERC_IVA) " _
            '                    & "FROM SISCOM_MI.PRENOTAZIONI WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID " _
            '                    & "AND ID_STATO <> -3)+ 100),2) AS IMPONIBILE, IMPORTO_CONSUNTIVATO - ROUND  " _
            '                    & "( IMPORTO_CONSUNTIVATO * 100 / (  (SELECT MAX (PRENOTAZIONI.PERC_IVA) FROM SISCOM_MI.PRENOTAZIONI  " _
            '                    & "WHERE PRENOTAZIONI.ID_PAGAMENTO_RIT_LEGGE = PAGAMENTI.ID AND ID_STATO <> -3) + 100), 2) AS IVA " _
            '                    & "FROM SISCOM_MI.PAGAMENTI " _
            '                    & "WHERE ID =  " & ID

            par.cmd.CommandText = "SELECT SUM(ROUND(SUM(RIT_LEGGE_IVATA)*100/(PERC_IVA+100),2)) AS IMPONIBILE," _
                & " SUM(ROUND(SUM(RIT_LEGGE_IVATA)-SUM(RIT_LEGGE_IVATA)*100/(PERC_IVA+100),2)) AS IVA " _
                & " FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO_RIT_LEGGE=" & ID & " AND ID_STATO<>-3 GROUP BY PERC_IVA"

            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim imponibile As Decimal = 0
            Dim iva As Decimal = 0
            Dim totale As Decimal = 0
            If lettore.Read Then
                imponibile = par.IfNull(lettore("IMPONIBILE"), 0)
                iva = par.IfNull(lettore("IVA"), 0)
                totale = par.IfNull(lettore("IMPONIBILE"), 0) + par.IfNull(lettore("IVA"), 0)
            End If
            lettore.Close()
            Dim importoConsuntivato As Decimal = 0
            par.cmd.CommandText = "select num_repertorio from SISCOM_MI.appalti where id = " & vIdAppalti
            Dim numRepertorio As String = par.IfNull(par.cmd.ExecuteScalar, "")
            Dim idApp As Integer = 0
            If numRepertorio = "2014/24" Then
                par.cmd.CommandText = "select distinct id_gruppo from SISCOM_MI.appalti where num_repertorio = '2014/19'"
                idApp = par.IfNull(par.cmd.ExecuteScalar, "-1")
                importoConsuntivato = totale
            End If
            If numRepertorio = "2014/5" Then
                par.cmd.CommandText = "select distinct id_gruppo from SISCOM_MI.appalti where num_repertorio = '2014/7'"
                idApp = par.IfNull(par.cmd.ExecuteScalar, "-1")
                importoConsuntivato = totale
            End If

            'par.cmd.CommandText = "SELECT PAGAMENTI.*, T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PAGAMENTI.ID = " & ID & " AND PF_VOCI.ID = PAGAMENTI.ID_VOCE_PF AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            par.cmd.CommandText = "SELECT (select siscom_mi.getdata(data_inizio)||' - '||siscom_mi.getdata(data_fine) from siscom_mi.appalti where appalti.id=pagamenti.id_appalto) as data_contratto,pagamenti.* FROM SISCOM_MI.PAGAMENTI WHERE ID = " & ID
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                If importoConsuntivato = 0 Then
                    importoConsuntivato = par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0)
                End If
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(importoConsuntivato, "##,##0.00"))
                'contenuto = Replace(contenuto, "$TOTSING$", Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00"))
                contenuto = Replace(contenuto, "$contocorrente$", par.IfNull(myReader1("CONTO_CORRENTE"), "n.d."))
                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>Emissione svincolo ritenuta di legge " & txtnumero.Text.ToUpper & " a seguito di C.R.E.</td></tr>"
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "N.D") & "</td></tr>"
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>Esercizio Finanziario dal " & InizioES & " al " & FineEs & "</td></tr>"
                'tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>Periodo contrattuale: " & par.IfNull(myReader1("DATA_CONTRATTO"), "N.D") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPONIBILE €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(imponibile, "##,##0.00") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IVA €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(importoConsuntivato - imponibile, "##,##0.00") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPORTO €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(importoConsuntivato, "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr></table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(importoConsuntivato, "##,##0.00")))

            End If
            myReader1.Close()

            tb1 = "<table style='width:100%;'>"
            tb2 = "<table style='width:100%;'>"
            Dim tb3 As String = "<table style='width:100%;'>"
            If idApp <> 0 Then
                par.cmd.CommandText = "SELECT PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE,SUM(PRENOTAZIONI.RIT_LEGGE_IVATA) as RIT_LEGGE_IVATA FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI " _
                                    & "WHERE prenotazioni.id_Stato<>-3 and PF_VOCI.ID = PRENOTAZIONI.id_voce_pf AND rit_legge_ivata IS NOT NULL AND rit_legge_ivata > 0 AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo IN (select id_gruppo from siscom_mi.appalti where id IN (" & vIdAppalti & "," & idApp & ")))) " _
                                    & "GROUP BY PF_VOCI.CODICE, PF_VOCI.DESCRIZIONE "
            Else
                par.cmd.CommandText = "SELECT PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE,SUM(PRENOTAZIONI.RIT_LEGGE_IVATA) as RIT_LEGGE_IVATA FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI " _
                                & "WHERE prenotazioni.id_Stato<>-3 and PF_VOCI.ID = PRENOTAZIONI.id_voce_pf AND rit_legge_ivata IS NOT NULL AND rit_legge_ivata > 0 AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) " _
                                & "GROUP BY PF_VOCI.CODICE, PF_VOCI.DESCRIZIONE "
            End If

            myReader1 = par.cmd.ExecuteReader

            While myReader1.Read
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("CODICE"), "") & "</td>"
                tb1 = tb1 & "</tr>"

                tb2 = tb2 & "<tr><td style='text-align: left; font-size:12pt;font-family :Arial ;'> " & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                tb2 = tb2 & "</tr>"

                tb3 = tb3 & "<tr><td style='text-align: right; font-size:12pt;font-family :Arial ;'> €." & Format(par.IfNull(myReader1("RIT_LEGGE_IVATA"), 0), "##,##0.00") & "</td>"
                tb3 = tb3 & "</tr>"

            End While

            tb1 = tb1 & "</table>"
            tb2 = tb2 & "</table>"
            tb3 = tb3 & "</table>"

            contenuto = Replace(contenuto, "$cod_capitolo$", tb1)
            contenuto = Replace(contenuto, "$voce_pf$", tb2)
            contenuto = Replace(contenuto, "$TOTSING$", tb3)

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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "open", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');", True)






            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub
    '******************************************************************************
    '                               NumeroToLettere
    '
    '                Converte il numero intero in lettere
    '
    ' Input : ImportoN                -->Importo Numerico
    '
    ' Ouput : NumeroToLettere         -->Il numero in lettere
    '******************************************************************************
    Function NumeroInLettere(ByVal Numero As String) As String

        '************************
        'Gestisce la virgola
        '************************
        Dim PosVirg As Integer
        Dim Lettere As String

        Numero$ = ChangeStr(Numero$, ".", "")
        PosVirg% = InStr(Numero$, ",")

        If PosVirg% Then
            Lettere$ = NumInLet(Mid(Numero$, 1, Len(Numero) + PosVirg% - 1))
            Lettere$ = Lettere$ & "\" & Format(CInt(Mid(Numero$, PosVirg% + 1, Len(Numero$))), "00")
        Else
            Lettere$ = NumInLet(CDbl(Numero$))
        End If

        NumeroInLettere = Lettere$

    End Function

    Private Function NumInLet(ByVal N As Double) As String

        '************************************************
        'inizializzo i due arry di numeri
        '************************************************
        SetNumeri()

        Dim ValT As Double     'Valore Temporaneo per la conversione
        Dim iCent As Integer    'Valore su cui calcolare le centinaia
        Dim L As String     'Importo in Lettere

        NumInLet = "zero"

        If N = 0 Then Exit Function

        ValT = N
        L = ""

        'miliardi
        iCent = Int(ValT / 1000000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = "unmiliardo"
            Else
                L = LCent(iCent) + "miliardi"
            End If
            ValT = ValT - CDbl(iCent) * 1000000000.0#
        End If

        'milioni
        iCent = Int(ValT / 1000000.0#)
        If iCent Then
            If iCent = 1 Then
                L = L + "unmilione"
            Else
                L = L + LCent(iCent) + "milioni"
            End If
            ValT = ValT - CDbl(iCent) * 1000000.0#
        End If

        'miliaia
        iCent = Int(ValT / 1000)
        If iCent Then
            If iCent = 1 Then
                L = L + "mille"
            Else
                L = L + LCent(iCent) + "mila"
            End If
            ValT = ValT - CDbl(iCent) * 1000
        End If

        ''centinaia
        'If ValT Then
        '    L = L + LCent(CInt(ValT))
        'End If
        If ValT Then
            L = L + LCent(Fix(CDbl(ValT)))
        End If

        NumInLet = L

    End Function

    Function LCent(ByVal N As Integer) As String

        ' Ritorna xx% (1/999) convertito in lettere
        Dim Numero As String
        Dim Lettere As String
        Dim Centinaia As Integer
        Dim Decine As Integer
        Dim x As Integer
        Dim Unita As Integer
        Dim sDec As String

        Numero$ = Format(N, "000")

        Lettere$ = ""
        Centinaia% = Val(Left$(Numero$, 1))
        If Centinaia% Then
            If Centinaia% > 1 Then
                Lettere = sUnita(Centinaia%)
            End If
            Lettere = Lettere + "cento"
        End If

        Decine% = (N Mod 100)
        If Decine% Then
            Select Case Decine%
                Case Is >= 20                               'Decine
                    sDec = sDecina(Val(Mid$(Numero$, 2, 1)))
                    x% = Len(sDec)
                    Unita% = Val(Right$(Numero$, 1))          'Unita
                    If Unita% = 1 Or Unita% = 8 Then x% = x% - 1
                    Lettere$ = Lettere$ & Left(sDec, x%) & sUnita(Unita%)    'Tolgo l'ultima lettera della decina per i
                Case Else
                    Lettere$ = Lettere$ + sUnita(Decine)
            End Select
        End If

        LCent$ = Lettere$

    End Function


    Sub SetNumeri()

        '************************************************
        ' Stringhe per traslitterazione numeri
        '************************************************
        sUnita(1) = "uno"
        sUnita(2) = "due"
        sUnita(3) = "tre"
        sUnita(4) = "quattro"
        sUnita(5) = "cinque"
        sUnita(6) = "sei"
        sUnita(7) = "sette"
        sUnita(8) = "otto"
        sUnita(9) = "nove"
        sUnita(10) = "dieci"
        sUnita(11) = "undici"
        sUnita(12) = "dodici"
        sUnita(13) = "tredici"
        sUnita(14) = "quattordici"
        sUnita(15) = "quindici"
        sUnita(16) = "sedici"
        sUnita(17) = "diciassette"
        sUnita(18) = "diciotto"
        sUnita(19) = "diciannove"

        sDecina(1) = "dieci"
        sDecina(2) = "venti"
        sDecina(3) = "trenta"
        sDecina(4) = "quaranta"
        sDecina(5) = "cinquanta"
        sDecina(6) = "sessanta"
        sDecina(7) = "settanta"
        sDecina(8) = "ottanta"
        sDecina(9) = "novanta"

    End Sub

    '*********************************************************************
    '                ChangeStr - da usare con versioni minori del Vb6
    '
    'Input  = Stringa                           -->Da convertire
    '         Lettera da sostituire             -->Da convertire
    '         Nuova lettera da rimpiazzare      -->Da convertire
    '
    'Ouput  = Stringa rimpiazzata
    '
    '*********************************************************************
    Function ChangeStr(ByRef sBuffer As String, ByRef OldChar As String,
                       ByRef NewChar As String) As String

        Dim TmpBuf As String
        Dim p As Integer

        On Error GoTo ErrChangeStr

        ChangeStr$ = ""   'Default Error

        TmpBuf$ = sBuffer$
        p% = InStr(TmpBuf$, OldChar$)
        Do While p > 0
            TmpBuf$ = Left$(TmpBuf$, p% - 1) + NewChar$ + Mid$(TmpBuf$, p% + Len(OldChar$))
            p% = InStr(p% + Len(NewChar$), TmpBuf$, OldChar$)
        Loop
        ChangeStr$ = TmpBuf$

        Exit Function

ErrChangeStr:
        ChangeStr$ = ""

    End Function


    'Protected Sub btnVariazConf_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVariazConf.Click
    '    Me.Tab_Composizione1.SelAssociati()
    '    Tabber7 = "tabbertabdefault"
    '    'Response.Write("<script>alert('La Composizione Patrimoniale dell\'appalto è stata correttamente modificata!');</script>")

    'End Sub

    Protected Sub cmbfornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfornitore.SelectedIndexChanged
        LoadIbanFornitore()
        LoadIndirizzo(True)
    End Sub
    Private Sub LoadIbanFornitore()
        Try

            If Me.cmbfornitore.SelectedValue <> -1 Then
                Me.LabelIBAN.Visible = True
                Me.chkIbanF.Items.Clear()
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                If vIdAppalti > 0 Then
                    'RIPRENDO LA TRANSAZIONE
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                End If
                par.cmd.CommandText = "SELECT ID,iban,fl_attivo,(SELECT  COUNT(id_iban) " _
                                    & "FROM siscom_mi.appalti_iban WHERE id_iban= fornitori_iban.ID " _
                                    & "AND id_appalto = " & vIdAppalti & ") AS cheked_old,1 as CHEKED " _
                                    & "FROM siscom_mi.fornitori_iban " _
                                    & "WHERE id_fornitore =" & Me.cmbfornitore.SelectedValue _
                                    & " and fl_Attivo=1"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                'chkIbanF.Items.Add(New ListItem(" ", -1))
                Dim i As Integer = 0
                While lettore.Read
                    chkIbanF.Items.Add(New ListItem(par.IfNull(lettore("IBAN"), "---"), par.IfNull(lettore("ID"), -1)))
                    If lettore("CHEKED") = 1 Then
                        chkIbanF.Items(i).Selected = True
                    Else
                        chkIbanF.Items(i).Selected = False
                    End If

                    If lettore("FL_ATTIVO") = 0 Then
                        chkIbanF.Items(i).Enabled = False
                    Else
                        chkIbanF.Items(i).Enabled = True
                    End If

                    i = i + 1
                End While
                lettore.Close()

            Else
                Me.LabelIBAN.Visible = True
                Me.chkIbanF.Items.Clear()
                chkIbanF.Items.Add(New ListItem(" ", -1))
            End If
            Me.chkIbanF.Enabled = False
        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try

    End Sub
    Private Sub LoadIndirizzo(Optional ByVal caricaModalitaPagamento As Boolean = False)
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            Me.cmbIndirizzoF.Items.Clear()
            If vIdAppalti > 0 Then
                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            par.caricaComboTelerik("SELECT ID,(INDIRIZZO || ' '||CIVICO||' - '||CAP||' '||COMUNE)AS DESCRIZIONE FROM SISCOM_MI.FORNITORI_INDIRIZZI WHERE ID_FORNITORE = " & Me.cmbfornitore.SelectedValue, cmbIndirizzoF, "id", "descrizione", False)
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader

            If caricaModalitaPagamento Then
                par.cmd.CommandText = "SELECT ID_TIPO_MODALITA_PAG,ID_TIPO_PAGAMENTO FROM SISCOM_MI.FORNITORI WHERE ID= " & Me.cmbfornitore.SelectedValue
                lettore = par.cmd.ExecuteReader
                While lettore.Read
                    cmbModalitaPagamento.SelectedValue = par.IfNull(lettore("ID_TIPO_MODALITA_PAG"), "-1")
                    cmbCondizionePagamento.SelectedValue = par.IfNull(lettore("ID_TIPO_PAGAMENTO"), "-1")
                End While
                lettore.Close()
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message

        End Try

    End Sub
    Function OttieniRitenutaIvata(ByVal Importo As Decimal, ByVal iva As Decimal) As Decimal
        OttieniRitenutaIvata = 0
        Try
            If Me.chkRitenute.Checked = True Then
                If Importo > 0 And iva > 0 Then
                    Dim Ritenute As Decimal = 0
                    Importo = Importo - ((Importo * iva) / (100 + iva)) 'ottengo l'importo esentiva
                    Ritenute = Math.Round((Importo * 0.5) / 100, 2)
                    OttieniRitenutaIvata = Math.Round(Ritenute + ((Ritenute * iva) / 100), 2) '
                End If
            End If

        Catch ex As Exception
            Me.LblErrore.Visible = True
            LblErrore.Text = ex.Message
        End Try


        Return OttieniRitenutaIvata
    End Function

    Protected Sub btnPagamento_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnPagamento.Click
        If Me.txtConfChiusura.Value = 1 Then
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            Dim idOperatoreAttuale As Integer = Session.Item("ID_OPERATORE")
            par.cmd.CommandText = "SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE data_inizio_incarico<='" & Format(Now, "yyyyMMdd") & "' and data_fine_incarico>='" & Format(Now, "yyyyMMdd") & "' and APPALTI_DL.ID_GRUPPO=(SELECT APPALTI.ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID= " & vIdAppalti & " )"
            Dim operatoreDL As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            par.cmd.CommandText = "select nvl(fl_superdirettore,0) from sepa.operatori where sepa.operatori.id=" & idOperatoreAttuale
            Dim fl_superdirettore As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)

            If fl_superdirettore = 0 Then
                If idOperatoreAttuale <> operatoreDL Then
                    RadWindowManager1.RadAlert("Il contratto può essere chiuso esclusivamente dal Direttore Lavori!", 300, 150, "Attenzione", "", "null")

                    Exit Sub
                    '*********************CHIUSURA CONNESSIONE**********************
                End If
            End If

            'select sulle manutenzioni per vedere se STATO = 1
            Dim ManutAttive As String = ""
            'select sulle manutenzioni per vedere se STATO = 1
            par.cmd.CommandText = "SELECT progr , anno FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND STATO = 1"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            While myReader1.Read
                ManutAttive = ManutAttive & "- " & myReader1("progr") & "/" & myReader1("anno") & "\n   "
            End While
            myReader1.Close()

            If Not String.IsNullOrEmpty(ManutAttive) Then
                RadWindowManager1.RadAlert("Non è possibile chiudere il contratto perchè presenta ordini aperti!\n\nODL/ANNO:\n   " & ManutAttive, 300, 150, "Attenzione", "", "null")
                Exit Sub
            End If
            'If myReader1.Read Then
            '    myReader1.Close()
            '    '*********************CHIUSURA CONNESSIONE**********************
            '    par.OracleConn.Close()
            '    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            '    Exit Sub
            'End If

            If Me.chkRitenute.Checked = True Then
                If ControlloCampi() = False Then
                    Exit Sub
                End If
                Dim Rimanente As Double = 0

                '******Scrittura del nuovo pagamento nell'apposita tabella!*******
                Dim Pagamento As String = CreaPagamento("VUOTO")
                If Pagamento <> "" Then

                    'lettore.Close()
                    myReader1.Close()
                    '************************scrittura evento chiusura contratto**************************************
                    InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - CHIUSURA CONTRATTO")

                    Me.lblStato.Text = "CHIUSO"
                    Me.SOLO_LETTURA.Value = 1
                    Me.txtModificato.Value = 1
                    par.cmd.CommandText = "SELECT SUM (ROUND (rit_legge_ivata * 100/ (100 + perc_iva), 2)) AS IMPONIBILE,SUM (round((ROUND (rit_legge_ivata * 100 / (100 + perc_iva), 2)*perc_iva/100),2)) AS iva, SUM (ROUND (rit_legge_ivata * 100 / (100 + perc_iva), 2))+ SUM (round((ROUND (rit_legge_ivata * 100 / (100 + perc_iva), 2)*perc_iva/100),2)) AS totale FROM prenotazioni where id_pagamento_rit_legge=" & Pagamento
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim imponibile As Decimal = 0
                    Dim iva As Decimal = 0
                    Dim totale As Decimal = 0
                    If lettore.Read Then
                        totale = par.IfNull(lettore("TOTALE"), 0)
                    End If
                    lettore.Close()
                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    If CInt(totale) > 0 Then
                        RadWindowManager1.RadAlert("Il pagamento è stato emesso e storicizzato!", 300, 150, "Attenzione", "", "null")
                        PdfPagamento(Pagamento)
                    End If
                    Update()
                    Me.idPagRitLegge.Value = Pagamento
                    CType(Tab_Appalto_generale.FindControl("btnPrintPagParz"), ImageButton).Visible = True

                    Me.FrmSolaLettura()
                    RadWindowManager1.RadAlert("Il contratto è stato chiuso!", 300, 150, "Attenzione", "", "null")

                    Exit Sub
                Else
                    RadWindowManager1.RadAlert("Inserire un fornitore e rieseguire l\'operazione!", 300, 150, "Attenzione", "", "null")
                    'lettore.Close()
                    myReader1.Close()

                    '*********************CHIUSURA CONNESSIONE**********************
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                End If

                '    Else
                '        Response.Write("<script>alert('L\'ammontare residuo preventivato per questa spesa è insufficiente a eseguirne la liquidazione!');</script>")
                '        Exit Sub
                '        lettore.Close()
                '        myReader1.Close()
                '    End If
                'Else
                '    Response.Write("<script>alert('Sono stati spesi più soldi di quelli previsti nel Piano Finanziario!!Impossibile continuare!!');</script>")
                '    Exit Sub
                '    lettore.Close()
                '    myReader1.Close()
                'End If

                'End If
                'lettore.Close()
                'Else
                '    Response.Write("<script>alert('Nessun importo stanziato per questo tipo di pagamento!');</script>")
                '    Exit Sub
                '    myReader1.Close()
                'End If
                'Else
                '    Response.Write("<script>alert('Nessun Piano Finanziario approvato per questa voce!');</script>")
                '    Exit Sub

                'End If
                'myReader1.Close()
            Else
                '************************scrittura evento chiusura contratto**************************************
                InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - CHIUSURA CONTRATTO")
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Me.lblStato.Text = "CHIUSO"
                Me.SOLO_LETTURA.Value = 1
                Me.txtModificato.Value = 1
                Update()
                Me.FrmSolaLettura()
                RadWindowManager1.RadAlert("Il contratto è stato chiuso!", 300, 150, "Attenzione", "", "null")

            End If

        Else
            RadWindowManager1.RadAlert("Nessuna modifica apportata!", 300, 150, "Attenzione", "", "null")

        End If
        If Me.lblStato.Text = "CHIUSO" Then
            DirectCast(Me.Tab_ElencoPrezzi1.FindControl("btnApriAppalti"), ImageButton).Visible = False
            DirectCast(Me.Tab_ElencoPrezzi1.FindControl("btnEliminaAppalti"), ImageButton).Visible = False

            'DirectCast(Me.Tab_Variazioni1.FindControl("btnEliminaServ"), ImageButton).Visible = False
            'DirectCast(Me.Tab_Variazioni1.FindControl("btnEliminaServCons"), ImageButton).Visible = False
            'DirectCast(Me.Tab_VariazioniLavori1.FindControl("btnEliminaLavCan"), ImageButton).Visible = False
            'DirectCast(Me.Tab_VariazioniLavori1.FindControl("btnEliminaLavCons"), ImageButton).Visible = False


        End If
    End Sub

    Protected Sub btnTornBozza_Click(sender As Object, e As System.EventArgs) Handles btnTornBozza.Click
        If Me.ConfRitBozza.Value = 1 Then
            '*******************APERURA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans



            ' APPALTI
            par.cmd.CommandText = "update siscom_mi.appalti set id_stato = 0 where id in (select id from siscom_mi.appalti b where b.id_gruppo = " & txtIdAppalto.Value & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT nvl(COUNT(ID),0) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO = " & vIdAppalti & " AND ID_STATO <>-3 AND TIPO_PAGAMENTO=6"
            Dim numPagamenti As Integer = CInt(par.IfNull(par.cmd.ExecuteScalar.ToString, 0))
            If numPagamenti = 0 Then
                'DELETE
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND TIPO_PAGAMENTO=6"
                par.cmd.ExecuteNonQuery()
            End If

            par.cmd.CommandText = "SELECT nvl(COUNT(ID),0) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO = " & vIdAppalti & " AND ID_STATO <>-3"
            numPagamenti = 0
            numPagamenti = CInt(par.IfNull(par.cmd.ExecuteScalar.ToString, 0))
            If numPagamenti = 0 Then
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO = " & vIdAppalti
                par.cmd.ExecuteNonQuery()
                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = False
            Else
                'Response.Write("<script>alert('L\'anticipo non può essere modificato in quanto è stato già emesso un CDP!');</script>")
                CType(Tab_Appalto_generale.FindControl("btnStampaCDP"), ImageButton).Visible = True
            End If

            par.myTrans.Commit()
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Page.Dispose()





            Response.Redirect("Appalti.aspx?CIG=" & Request.QueryString("CIG") & "&FO=" & par.PulisciStrSql(Request.QueryString("FO")) & "&NU=" & par.PulisciStrSql(Request.QueryString("NU")) & "&LO=" & par.PulisciStrSql(Request.QueryString("LO")) & "&DAL=" & par.IfEmpty(Request.QueryString("DAL"), "") & "&AL=" & par.IfEmpty(Request.QueryString("AL"), "") & "&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF") & "&IDL=" & UCase(Request.QueryString("IDL")))
        End If

    End Sub


    Protected Sub btnTerminaAttivazione_Click(sender As Object, e As System.EventArgs) Handles btnTerminaAttivazione.Click
        HiddenFieldAttivaContratto.Value = 0
        CompletaLoad()
    End Sub

    Protected Sub CompletaLoad()
        Try
            HiddenResiduoCanone.Value = CType(Tab_Appalto_generale.FindControl("txtResiduoCanone"), TextBox).Text
            HiddenResiduoConsumo.Value = CType(Tab_Appalto_generale.FindControl("txtresiduoConsumo"), TextBox).Text
            Tabber1 = ""
            Tabber2 = ""
            Tabber3 = ""
            Tabber4 = ""
            Tabber5 = ""
            Tabber6 = ""
            Tabber7 = ""
            Tabber8 = ""

            If vIdAppalti <> 0 And Me.lblStato.Text = "ATTIVO" Or Me.lblStato.Text = "CHIUSO" Then
                If VerificaAllegatoTerminiTemporali() = False Then
                    txtannoinizio.Enabled = True
                    txtannofine.Enabled = True
                End If
                If Session.Item("FL_CP_VARIAZIONE_IMPORTI") = "1" Then
                    If VerificaAllegatoVariazioni() Then
                        RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = False
                    Else
                        RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = True
                    End If
                Else
                    RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = False
                End If
                TabberHide = "tabbertab"

            Else
                TabberHide = "tabbertabhide"
                NascondiTab()
            End If
            If TabberHide = "tabbertab" Then
                Select Case txttab.Value
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
                    Case "7"
                        Tabber7 = "tabbertabdefault"
                    Case "8"
                        Tabber8 = "tabbertabdefault"

                    Case "9"
                        Tabber9 = "tabbertabdefault"
                    Case "10"
                        Tabber10 = "tabbertabdefault"

                End Select
            Else
                Select Case txttab.Value
                    Case "1"
                        Tabber1 = "tabbertabdefault"
                    Case "2"
                        Tabber2 = "tabbertabdefault"
                    Case "3"
                        Tabber5 = "tabbertabdefault"
                    Case "4"
                        Tabber6 = "tabbertabdefault"
                    Case "5"
                        Tabber7 = "tabbertabdefault"
                    Case "6"
                        Tabber8 = "tabbertabdefault"
                    Case "7"
                        Tabber10 = "tabbertabdefault"


                End Select
                NascondiTab()
            End If

            If SalvaAttiva.Value = 1 And IsNothing(Session.Item("PrenAttAuto")) = False Then
                If Session.Item("PrenAttAuto") = 1 Then
                    If ctrlPlurEStatoSei() = True Then
                        ' RIPRENDO LA CONNESSIONE
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        'RIPRENDO LA TRANSAZIONE
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                        Me.txtModificato.Value = 1
                        Me.lblStato.Text = "ATTIVO"
                        If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                            btnTornBozza.Visible = False
                        Else
                            btnTornBozza.Visible = True
                        End If
                        Dim rad As Object = CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).Controls.Item(0)
                        '  Tab_Servizio.FindControl("btnEliminaAppalti").Visible = False

                        'Tab_Servizio.FindControl("btnApriAppalti").Visible = False
                        CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                        CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).MasterTableView.Columns.FindByUniqueName("modificaServizio").Visible = False
                        CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
                        CType(Tab_Servizio.FindControl("DataGrid3"), RadGrid).Rebind()


                        CType(Tab_Fornitori1.FindControl("DataGridFornitori"), RadGrid).MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
                        CType(Tab_Fornitori1.FindControl("DataGridFornitori"), RadGrid).MasterTableView.Columns.FindByUniqueName("modificaFornitore").Visible = False
                        CType(Tab_Fornitori1.FindControl("DataGridFornitori"), RadGrid).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
                        CType(Tab_Fornitori1.FindControl("DataGridFornitori"), RadGrid).Rebind()

                        FrmSolaLetturaPerManutenzioni()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                        RadNotificationNote.Text = "Il contratto è stato ATTIVATO!Salvare le modifiche apportate tramite il pulsante Salva!"
                        RadNotificationNote.AutoCloseDelay = "0"
                        RadNotificationNote.Show()
                        'VISUALIZZAZIONE DELL'ANTICIPO NON APPENA IL CONTRATTO VIENE ATTIVATO
                        If VerificaAllegatoTerminiTemporali() = False Then
                            txtannoinizio.Enabled = True
                            txtannofine.Enabled = True
                        End If
                        Dim totaleAnticipo As Decimal = 0
                        par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUNTIVATO) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND TIPO_PAGAMENTO=15"
                        totaleAnticipo = par.IfNull(par.cmd.ExecuteScalar, 0)

                        Dim totaleTrattenuto As Decimal = 0
                        par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE_CON_IVA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_STATO<>-3"
                        totaleTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)

                        CType(Tab_Appalto_generale.FindControl("txtfondoTrattenuto"), TextBox).Text = IsNumFormat(totaleAnticipo - totaleTrattenuto, "0", "##,##0.00")
                        CType(Tab_Appalto_generale.FindControl("txtfondoAnticipo"), TextBox).Text = IsNumFormat(totaleAnticipo, "0", "##,##0.00")

                        'par.caricaComboBox("SELECT ID, DESCRIZIONE FROM SISCOM_MI.PF_VOCI_IMPORTO WHERE ID IN (SELECT ID_PF_VOCE_IMPORTO FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = " & CType(Me.Page, Object).vIdAppalti & ") ORDER BY DESCRIZIONE", CType(Me.Tab_VariazioneImporti1.FindControl("ddlVoceServizio"), DropDownList), "ID", "DESCRIZIONE", True)
                        Me.btnAttivaContratto.Visible = False
                        par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET " _
                                            & " TOT_CANONE = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCan"), TextBox).Text.Replace(".", ""), 0)) _
                                            & ", TOT_CONSUMO = " & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_Appalto_generale.FindControl("txtImpTotPlusOneriCons"), TextBox).Text.Replace(".", ""), 0)) _
                                            & " WHERE ID = " & vIdAppalti
                        par.cmd.ExecuteNonQuery()
                        SalvaAttiva.Value = 0
                        Session.Remove("PrenAttAuto")

                    Else
                        RadWindowManager1.RadAlert("Impossibile attivare il contratto perchè non duplicabile!", 300, 150, "Attenzione", "", "null")
                        ' RIPRENDO LA CONNESSIONE
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        'RIPRENDO LA TRANSAZIONE
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                        par.myTrans.Rollback()

                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                        SalvaAttiva.Value = 0
                        Session.Remove("PrenAttAuto")


                    End If

                Else
                    If HiddenFieldAttivaContratto.Value = 0 Then
                        RadWindowManager1.RadAlert("Il contratto NON è stato attivato!Operazione annullata", 300, 150, "Attenzione", "", "null")
                        ' RIPRENDO LA CONNESSIONE
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        'RIPRENDO LA TRANSAZIONE
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                        par.myTrans.Rollback()

                        par.myTrans = par.OracleConn.BeginTransaction()
                        HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                        SalvaAttiva.Value = 0
                        Session.Remove("PrenAttAuto")
                    End If


                End If
                SalvaAttiva.Value = 0
            Else
                Session.Remove("PrenAttAuto")
            End If
            If UCase(Request.QueryString("A")) <> "" Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                If Not IsNothing(par.OracleConn) Then
                    par.OracleConn.Close()
                    Session.Item("LAVORAZIONE") = "0"

                    HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
                    HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
                    par.cmd.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    '  Page.Dispose()
                End If
            End If
            If vIdAppalti <> 0 And Me.lblStato.Text = "ATTIVO" Then
                RadComboBoxAnticipo.Enabled = False
            End If
            If txtIdAppalto.Value <> -1 And Me.lblStato.Text <> "ATTIVO" Then
                'Verifica allegato di tipo polizza fideiussoria
                If VerificaAllegatoPolizza() Then
                    RadNumericTextBoxNumeroRate.Enabled = False
                    RadComboBoxAnticipo.SelectedValue = 0
                Else
                    'Verifica se appalto è in bozza dopo aver premuto RITORNA IN BOZZA
                    If VerificaTornaInBozza() Then
                        RadNumericTextBoxNumeroRate.Enabled = True
                        ImpostaComboServizi()
                        RadComboBoxAnticipo.Enabled = True
                    Else
                        RadNumericTextBoxNumeroRate.Enabled = False
                        RadComboBoxAnticipo.Enabled = False
                    End If
                End If

            End If
            If RadComboBoxAnticipo.SelectedValue = 2 Then
                RadNumericTextBoxNumeroRate.Text = 1
                RadNumericTextBoxNumeroRate.Enabled = False

            End If
            HiddenResiduoCanone.Value = CType(Tab_Appalto_generale.FindControl("txtResiduoCanone"), TextBox).Text
            HiddenResiduoConsumo.Value = CType(Tab_Appalto_generale.FindControl("txtresiduoConsumo"), TextBox).Text

            'Verifica che il contratto abbia le 4 voci utili all' imputazione delle pulizie, e la voce per l' imputazione appalti
            VerificaImputazione()
            If VerificaAllegatoPolizza() = False AndAlso Session.Item("CP_APPALTO_SINGOLA_VOCE") = "1" Then
                RadComboBoxVoci.Visible = True
            Else
                RadComboBoxVoci.ClearSelection()
                RadComboBoxVoci.Visible = False
            End If


            ScriptManager.RegisterStartupScript(Me, Me.GetType, "key", "Blocca_SbloccaMenu(1);", True)
        Catch ex As Exception
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdAppalti)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

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

    Private Sub NascondiTab()
        RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = False
        RadTabStrip.Tabs.FindTabByValue("ElencoPrezzi").Visible = False
        'RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = False
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

    Private Function VerificaAllegatoPolizza() As Boolean
        VerificaAllegatoPolizza = True
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("POLIZZA FIDEIUSSORIA")
            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdAppalti & " and stato = 0"
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                VerificaAllegatoPolizza = True
            Else
                VerificaAllegatoPolizza = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function
    Private Function VerificaTornaInBozza() As Boolean
        VerificaTornaInBozza = True
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT COUNT(ID_APPALTO) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO = " & txtIdAppalto.Value
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                VerificaTornaInBozza = True
            Else
                VerificaTornaInBozza = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Private Sub StampaCdP(sValorePagamento As Integer, TipoAllegato As String)
        Dim sStr1 As String

        Dim perc_sconto, perc_iva, perc_oneri As Decimal
        Dim oneri, asta, iva, ritenuta, risultato1, risultato2, risultato3, risultato4, risultato4Tot, risultato3Tot, importoDaPagare As Decimal

        Dim FlagConnessione As String = False

        Try
            ' APRO CONNESSIONE
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            Dim descrizione As String = "Stampa anticipo contrattuale"
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("STAMPA ANTICIPO CONTRATTUALE")
            par.cmd.CommandText = "SELECT NOME FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato & " AND ID_OGGETTO = " & vIdAppalti
            Dim nome As String = par.IfEmpty(par.IfNull(par.cmd.ExecuteScalar, ""), "")
            If String.IsNullOrEmpty(nome) Then
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                par.cmd.CommandText = "select * from SISCOM_MI.PAGAMENTI where  ID=" & sValorePagamento
                myReader1 = par.cmd.ExecuteReader
                If myReader1.Read Then
                    If par.IfNull(myReader1("DATA_STAMPA"), "") = "" Then
                        myReader1.Close()
                        'UPDATE PAGAMENTI
                        par.cmd.Parameters.Clear()
                        par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set DATA_STAMPA=" & Format(Now, "yyyyMMdd") & " where ID=" & sValorePagamento
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""
                        RadWindowManager1.RadAlert("Il pagamento è stato emesso e storicizzato!", 300, 150, "Attenzione", "", "null")
                        'Response.Write("<script>alert('Il pagamento è stato emesso e storicizzato!');</script>")
                    Else
                        myReader1.Close()



                    End If
                Else
                    myReader1.Close()
                End If

                par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set ID_STATO=1 where ID=" & sValorePagamento
                par.cmd.ExecuteNonQuery()
                Dim importo_totale As Decimal = 0
                par.cmd.CommandText = "select importo_consuntivato from siscom_mi.pagamenti where id=" & sValorePagamento
                importo_totale = par.IfNull(par.cmd.ExecuteScalar, 0)

                'UPDATE PRENOTAZIONI
                'par.cmd.CommandText = "update SISCOM_MI.PRENOTAZIONI set ID_STATO=2  where ID_PAGAMENTO=" & sValorePagamento
                'par.cmd.ExecuteNonQuery()
                'par.cmd.CommandText = ""


                'UPDATE EVENTI PAGAMENTI
                par.cmd.Parameters.Clear()
                par.cmd.CommandText = "insert into SISCOM_MI.EVENTI_PAGAMENTI (ID_PAGAMENTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) values ( " & sValorePagamento & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "', 'F98','Stampa Mandato di Pagamento')"
                par.cmd.ExecuteNonQuery()

                'Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamentoMANU2_ANTICIPO.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
                Dim contenuto As String = sr1.ReadToEnd()
                sr1.Close()
                contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(sValorePagamento))


                'PAGAMENTI.IMPORTO_NO_IVA
                '& " PF_VOCI.CODICE as ""COD_VOCE"",PF_VOCI.DESCRIZIONE as ""DESC_VOCE"", "
                '& "   and  PRENOTAZIONI.ID_VOCE_PF=PF_VOCI.ID (+) " _
                '     & "   and  PRENOTAZIONI.ID_VOCE_PF_IMPORTO=PF_VOCI_IMPORTO.ID (+) "
                ',SISCOM_MI.PF_VOCI,SISCOM_MI.PF_VOCI_IMPORTO
                sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_SAL,PAGAMENTI.DATA_STAMPA,PAGAMENTI.DESCRIZIONE," _
                     & " PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.CONTO_CORRENTE," _
                     & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE," _
                     & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                     & " APPALTI.FL_RIT_LEGGE , " _
                     & "(SELECT descrizione FROM siscom_mi.TIPO_MODALITA_PAG WHERE ID = pagamenti.id_tipo_modalita_pag) AS tipo_modalita,(SELECT descrizione FROM siscom_mi.TIPO_pagamento WHERE ID = pagamenti.id_tipo_pagamento) AS tipo_pag,pagamenti.data_scadenza " _
                     & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                     & " where  PAGAMENTI.ID=" & sValorePagamento _
                     & "   and  PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                     & "   and  PAGAMENTI.ID_APPALTO=APPALTI.ID (+) "

                ' ''sStr1 = "select PAGAMENTI.ANNO,PAGAMENTI.PROGR,PAGAMENTI.DATA_EMISSIONE,PAGAMENTI.DATA_STAMPA,PAGAMENTI.DESCRIZIONE," _
                ' ''            & " PAGAMENTI.IMPORTO_CONSUNTIVATO,PAGAMENTI.CONTO_CORRENTE," _
                ' ''            & " FORNITORI.RAGIONE_SOCIALE, FORNITORI.COGNOME, FORNITORI.NOME,FORNITORI.COD_FORNITORE," _
                ' ''            & " APPALTI.ID as ""ID_APPALTO"",APPALTI.NUM_REPERTORIO,APPALTI.DATA_REPERTORIO,APPALTI.DESCRIZIONE AS ""APPALTI_DESC"",APPALTI.CIG," _
                ' ''            & " APPALTI.FL_RIT_LEGGE " _
                ' ''     & " from SISCOM_MI.PAGAMENTI,SISCOM_MI.FORNITORI,SISCOM_MI.APPALTI " _
                ' ''     & " where  PAGAMENTI.ID=" & sValorePagamento _
                ' ''     & "   and  PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                ' ''     & "   and  PAGAMENTI.ID_APPALTO=APPALTI.ID (+) "



                par.cmd.CommandText = sStr1
                myReader1 = par.cmd.ExecuteReader

                If myReader1.Read Then
                    'contenuto = Replace(contenuto, "$chiamante$", "") '"CONTRATTO:")

                    contenuto = Replace(contenuto, "$anno$", myReader1("ANNO"))
                    contenuto = Replace(contenuto, "$progr$", myReader1("PROGR"))


                    contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(par.IfNull(myReader1("DATA_EMISSIONE"), "-1")))
                    contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(par.IfNull(myReader1("DATA_STAMPA"), "")))

                    'contenuto = Replace(contenuto, "$dettagli_chiamante$", "") ' "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(par.IfNull(myReader1("DATA_REPERTORIO"), "-1")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$contratto$", "N." & myReader1("NUM_REPERTORIO") & " del " & par.FormattaData(myReader1("DATA_REPERTORIO")) & " " & myReader1("APPALTI_DESC"))

                    contenuto = Replace(contenuto, "$CIG$", par.IfNull(myReader1("CIG"), ""))
                    contenuto = Replace(contenuto, "$conto_corrente$", par.IfNull(myReader1("CONTO_CORRENTE"), "12000X01"))
                    contenuto = Replace(contenuto, "$modalita$", par.IfNull(myReader1("tipo_modalita"), ""))
                    contenuto = Replace(contenuto, "$condizione$", par.IfNull(myReader1("tipo_pag"), ""))
                    contenuto = Replace(contenuto, "$datascadenza$", par.FormattaData(par.IfNull(myReader1("data_scadenza"), "")))
                    contenuto = Replace(contenuto, "$descrpag$", par.IfNull(myReader1("DESCRIZIONE"), ""))


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


                    'IBAN **************************************************
                    par.cmd.CommandText = "select IBAN from SISCOM_MI.FORNITORI_IBAN " _
                                       & " where ID in (select ID_IBAN from SISCOM_MI.APPALTI_IBAN where ID_APPALTO=" & par.IfNull(myReader1("ID_APPALTO"), 0) & ")"

                    Dim myReaderBP As Oracle.DataAccess.Client.OracleDataReader
                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        sFORNITORI = sFORNITORI & "<br/>" & par.IfNull(myReaderBP("IBAN"), "")
                    End While
                    myReaderBP.Close()
                    contenuto = Replace(contenuto, "$fornitori$", sFORNITORI)
                    '*********************************************************

                    'RIEPILOGO SAL
                    par.cmd.CommandText = "select PRENOTAZIONI.* " _
                                       & " from   SISCOM_MI.PRENOTAZIONI" _
                                       & " where PRENOTAZIONI.ID_PAGAMENTO=" & sValorePagamento

                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader
                    myReader2 = par.cmd.ExecuteReader()
                    'While myReader2.Read
                    '    CalcolaImporti(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), par.IfNull(myReader1("FL_RIT_LEGGE"), 0), par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))
                    'End While
                    myReader2.Close()


                    contenuto = Replace(contenuto, "$TOT$", IsNumFormat(par.IfNull(importoPRE, 0), "", "##,##0.00"))

                    'modifica marco/pepep 05/01/2011
                    par.cmd.CommandText = "select rit_legge_ivata from siscom_mi.prenotazioni where id_pagamento = " & sValorePagamento
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

                    par.cmd.CommandText = "SELECT IMPONIBILE, IVA, PERC_IVA FROM SISCOM_MI.PRENOTAZIONI " _
                                      & " WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = " _
                                      & " (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) " _
                                      & " AND PRENOTAZIONI.ID_STATO<>-3" _
                                    & " and id_pagamento = " & sValorePagamento
                    lettore = par.cmd.ExecuteReader
                    Dim imponibileAnticipo As Decimal = 0
                    Dim ivaAnticipo As Decimal = 0
                    Dim percIvaAnticipo As Decimal = 0
                    While lettore.Read
                        imponibileAnticipo = par.IfNull(lettore("IMPONIBILE"), "")
                        ivaAnticipo = par.IfNull(lettore("IVA"), "")
                        percIvaAnticipo = par.IfNull(lettore("PERC_IVA"), "")
                    End While
                    lettore.Close()

                    Dim S2 As String = "<table style='width:100%;'>"
                    S2 = S2 & "<tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "") & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Imponibile €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(imponibileAnticipo, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>IVA (" & IsNumFormat(percIvaAnticipo, "", "##,##0") & "%) €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(ivaAnticipo, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'>Totale €</td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(importo_totale, "", "##,##0.00") & "</td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "</tr><tr>"
                    S2 = S2 & "<td style='text-align: left; width:40%; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    S2 = S2 & "<td style='text-align: right; width:40%; font-size:14pt;font-family :Arial ;'></td>"
                    S2 = S2 & "<td style='text-align: right; width:20%; font-size:14pt;font-family :Arial ;'></td>"

                    'If penaleT > 0 Then
                    '    S2 = S2 & "</tr><tr>"
                    '    'S2 = S2 & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & "   " & "</td>"
                    '    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>Penale €</td>"
                    '    S2 = S2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & IsNumFormat(penale, "", "##,##0.00") & "</td>"
                    '    'S2 = S2 & "</tr><tr>"
                    'End If


                    S2 = S2 & "</tr></table>"

                    Dim T As String = "<table style='width:100%;'>"
                    T = T & "<tr>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & S2 & "</td>"
                    T = T & "<td style='text-align: left; font-size:14pt;font-family :Arial ;'></td>"

                    T = T & "</tr></table>"

                    contenuto = Replace(contenuto, "$dettagli$", T)
                    ''****************************


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

                    par.cmd.CommandText = "SELECT CODICE,PRENOTAZIONI.ANNO,PF_VOCI.DESCRIZIONE,PRENOTAZIONI.IMPORTO_APPROVATO " _
                        & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PRENOTAZIONI,SISCOM_MI.PF_VOCI " _
                        & " WHERE PAGAMENTI.ID=" & sValorePagamento _
                        & " AND PAGAMENTI.ID=PRENOTAZIONI.ID_PAGAMENTO " _
                        & " AND PF_VOCI.ID=PRENOTAZIONI.ID_VOCE_PF "

                    'par.cmd.CommandText = "select distinct(ID_VOCE) from SISCOM_MI.PF_VOCI_IMPORTO " _
                    '                  & " where ID in (select ID_VOCE_PF_IMPORTO from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento & ")"

                    'par.cmd.CommandText = "select distinct(ID_VOCE_PF)  as ID_VOCE , " _
                    '                       & "(SELECT DISTINCT RTRIM (LTRIM (SUBSTR (inizio, 1, 4))) AS annoBp FROM siscom_mi.T_ESERCIZIO_FINANZIARIO  WHERE id IN " _
                    '                       & "(select id_esercizio_finanziario from siscom_mi.pf_main where id in (SELECT DISTINCT id_piano_finanziario FROM siscom_mi.pf_voci WHERE id = PRENOTAZIONI.ID_VOCE_PF))" _
                    '                       & ") AS ANNO " _
                    '                   & " from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento


                    myReaderBP = par.cmd.ExecuteReader
                    While myReaderBP.Read
                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                            & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("CODICE"), "") & "</td>" _
                            & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("ANNO"), "") & "</td>" _
                            & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("DESCRIZIONE"), "") & "</td>" _
                            & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderBP("IMPORTO_APPROVATO"), 0), "", "##,##0.00") & "</td>" _
                            & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                            & "</tr>"
                    End While

                    'While myReaderBP.Read
                    '    'X OGNI TIPO DI VOCE
                    '    par.cmd.CommandText = "select PRENOTAZIONI.* " _
                    '                      & " from   SISCOM_MI.PRENOTAZIONI " _
                    '                      & " where ID_PAGAMENTO=" & sValorePagamento _
                    '                      & "   and ID_VOCE_PF=" & par.IfNull(myReaderBP("ID_VOCE"), 0)

                    '    'par.cmd.CommandText = "select PRENOTAZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                    '    '                  & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI_PENALI" _
                    '    '                  & " where ID_PAGAMENTO=" & sValorePagamento _
                    '    '                  & "   and ID_VOCE_PF_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO where ID_VOCE=" & par.IfNull(myReaderBP("ID_VOCE"), 0) & ")" _
                    '    '                  & "   and SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "


                    '    Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                    '    myReaderB = par.cmd.ExecuteReader

                    '    While myReaderB.Read
                    '        '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                    '        If par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) > 0 Then

                    '            If par.IfNull(myReaderB("ID_VOCE_PF_IMPORTO"), 0) > 0 Then
                    '                sStr1 = "select APPALTI_LOTTI_SERVIZI.*,APPALTI.FL_RIT_LEGGE " _
                    '                    & "  from   SISCOM_MI.APPALTI_LOTTI_SERVIZI,SISCOM_MI.APPALTI " _
                    '                    & "  where APPALTI_LOTTI_SERVIZI.ID_PF_VOCE_IMPORTO=" & par.IfNull(myReaderB("ID_VOCE_PF_IMPORTO"), 0) _
                    '                    & "  and   APPALTI_LOTTI_SERVIZI.ID_APPALTO=" & par.IfNull(myReaderB("ID_APPALTO"), 0) _
                    '                    & "  and   APPALTI.ID=" & par.IfNull(myReaderB("ID_APPALTO"), 0)


                    '                Dim myReaderB2 As Oracle.DataAccess.Client.OracleDataReader
                    '                par.cmd.CommandText = sStr1
                    '                myReaderB2 = par.cmd.ExecuteReader

                    '                If myReaderB2.Read Then

                    '                    'perc_oneri = par.IfNull(myReaderB2("PERC_ONERI_SIC_CAN"), 0)

                    '                    'D3= D1(-(D1 * D2 / 100))
                    '                    'D9= D4*100/D3
                    '                    Dim D3 As Decimal = 0
                    '                    D3 = par.IfNull(myReaderB2("IMPORTO_CANONE"), 0) - ((par.IfNull(myReaderB2("IMPORTO_CANONE"), 0) * par.IfNull(myReaderB2("SCONTO_CANONE"), 0)) / 100)

                    '                    perc_oneri = (par.IfNull(myReaderB2("ONERI_SICUREZZA_CANONE"), 0) * 100) / D3


                    '                    perc_sconto = par.IfNull(myReaderB2("SCONTO_CANONE"), 0)

                    '                    If par.IfNull(myReaderB("PERC_IVA"), -1) = -1 Then
                    '                        perc_iva = par.IfNull(myReaderB2("IVA_CANONE"), 0)
                    '                    Else
                    '                        perc_iva = par.IfNull(myReaderB("PERC_IVA"), 0)
                    '                    End If

                    '                    risultato3 = ((par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) * 100) / (100 + perc_iva))

                    '                    'ALIQUOTA 0,5% sul NETTO senza ONERI vedi sopra
                    '                    If par.IfNull(myReaderB2("FL_RIT_LEGGE"), 0) = 1 Then
                    '                        ritenuta = (risultato3 * 0.5) / 100
                    '                        ritenuta = Round(ritenuta, 2)
                    '                        'ritenuta_IVATA = ritenuta + ((ritenuta * perc_iva) / 100)
                    '                    Else
                    '                        ritenuta = 0
                    '                    End If

                    '                    oneri = risultato3 - (risultato3 * 100 / (100 + perc_oneri))        'Oneri di Sicurezza (txtOneri)
                    '                    oneri = Round(oneri, 2)

                    '                    risultato2 = risultato3 - oneri                                     'A netto esclusi oneri (txtNetto) OK

                    '                    asta = (risultato2 / ((100 - perc_sconto) / 100)) - risultato2      'Ribasso d'asta  (txtRibassoAsta) OK
                    '                    asta = Round(asta, 2)

                    '                    risultato1 = risultato2 + asta                                      'A lordo esclusi oneri (txtOneriImporto)

                    '                    risultato4 = risultato1 + oneri                                     'A lordo compresi oneri (txtImporto)

                    '                    importoDaPagare = risultato3 - ritenuta + (((risultato3 - ritenuta) * perc_iva) / 100)  'A netto compresi oneri e IVA (txtNettoOneriIVA)
                    '                    importoDaPagare = Round(importoDaPagare, 2)

                    '                    'risultato3Tot = risultato3Tot + par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0)
                    '                    risultato3Tot = risultato3Tot + importoDaPagare


                    '                    iva = importoDaPagare - ((importoDaPagare * 100) / (100 + perc_iva))            'IVA
                    '                    iva = Round(iva, 2)



                    '                    Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                    '                    par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderBP("ID_VOCE"), 0)
                    '                    myReaderB3 = par.cmd.ExecuteReader

                    '                    If myReaderB3.Read Then
                    '                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                    '                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                    '                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("ANNO"), "") & "</td>" _
                    '                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                    '                                                        & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0), "", "##,##0.00") & "</td>" _
                    '                                                        & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                    '                                                        & "</tr>"

                    '                    End If
                    '                    myReaderB3.Close()

                    '                End If
                    '                myReaderB2.Close()
                    '            Else

                    '                Dim myReaderB3 As Oracle.DataAccess.Client.OracleDataReader
                    '                par.cmd.CommandText = "select CODICE,DESCRIZIONE from SISCOM_MI.PF_VOCI where ID=" & par.IfNull(myReaderB("ID_VOCE_PF"), 0)
                    '                myReaderB3 = par.cmd.ExecuteReader

                    '                If myReaderB3.Read Then
                    '                    risultato3Tot = risultato3Tot + par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0)
                    '                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                    '                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("CODICE"), "") & "</td>" _
                    '                                                    & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                    '                                                    & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0), "", "##,##0.00") & "</td>" _
                    '                                                    & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & "" & "</td>" _
                    '                                                    & "</tr>"

                    '                End If
                    '                myReaderB3.Close()

                    '            End If

                    '        End If
                    '    End While
                    '    myReaderB.Close()

                    'End While
                    myReaderBP.Close()
                    If importo_totale <> 0 Then
                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                 & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                 & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                 & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                 & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " IMPONIBILE   : " & IsNumFormat(imponibileAnticipo, "", "##,##0.00") & "</td>" _
                                 & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                 & "</tr>"

                        TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                                  & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " IVA   : " & IsNumFormat(ivaAnticipo, "", "##,##0.00") & "</td>" _
                                  & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                  & "</tr>"
                    End If

                    TestoGrigliaBP = TestoGrigliaBP & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " " & "</td>" _
                              & "<td align='right'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & " TOTALE   : " & IsNumFormat(importo_totale, "", "##,##0.00") & "</td>" _
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

                    'par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID=" & Session.Item("ID_OPERATORE")
                    par.cmd.CommandText = "SELECT INITCAP(COGNOME||' '||NOME) FROM SEPA.OPERATORI WHERE ID = (SELECT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE ID_GRUPPO =(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =  " & vIdAppalti & ") AND DATA_FINE_INCARICO > " & Format(Now, "yyyyMMdd") & ")"
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    Dim chiamante2 As String = ""
                    If lett.Read Then
                        chiamante2 = par.IfNull(lett(0), "")
                    End If
                    lett.Close()
                    contenuto = Replace(contenuto, "$chiamante2$", chiamante2)
                    par.cmd.CommandText = "SELECT INITCAP(GESTORI_ORDINI.DESCRIZIONE) FROM SISCOM_MI.GESTORI_ORDINI,SISCOM_MI.APPALTI,SISCOM_MI.PAGAMENTI " _
                            & " WHERE APPALTI.ID_gESTORE_ORDINI=GESTORI_ORDINI.ID AND PAGAMENTI.ID_APPALTO=APPALTI.ID AND PAGAMENTI.ID=" & sValorePagamento
                    lett = par.cmd.ExecuteReader
                    Dim gestore As String = ""
                    If lett.Read Then
                        gestore = par.IfNull(lett(0), "")
                    End If
                    lett.Close()
                    contenuto = Replace(contenuto, "$proponente$", gestore)
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
                pdfConverter1.PdfFooterOptions.ShowPageNumber = False

                Dim nomefile As String = "AttPagamento_" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
                nomefile = par.NomeFileManut("CDP", sValorePagamento) & ".pdf"
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))

                'Dim i As Integer = 0
                'For i = 0 To 10000
                'Next
                'GIANCARLO 16-02-2017
                'inserimento della stampa cdp negli allegati


                par.cmd.CommandText = "SELECT ID_CARTELLA FROM SISCOM_MI.ALLEGATI_WS_OGGETTI WHERE ID = " & TipoAllegato
                Dim idCartella As String = par.IfNull(par.cmd.ExecuteScalar.ToString, "")
                par.AllegaDocumentoWS(Server.MapPath("../../../FileTemp/" & nomefile), nomefile, idCartella, descrizione, idTipoOggetto, TipoAllegato, vIdAppalti, "../../../ALLEGATI/APPALTI/")
                par.myTrans.Commit() 'COMMIT



                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & lIdConnessione, par.myTrans)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "open", "window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "open", "window.open('../../../ALLEGATI/APPALTI/" & nome & "','SAL','');self.close();", True)
            End If
            '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If


        Catch ex As Exception

            '*********************CHIUSURA CONNESSIONE**********************
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Sub CalcolaImporti(ByVal importo As Decimal, ByVal fl_ritenuta As Integer, ByVal Id_Voce As Long, ByVal Id_Appalto As Long, ByVal Tipo As String, ByVal PERC_IVA_PRENOTAZIONI As Decimal)

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
            End If
            ritenutaPRE = ritenutaPRE + ritenuta_IVATA                                'Ritenuta di legge 0,5% txtRitenuta Ok

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
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub

    'Protected Sub ImgAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgAnnulla.Click
    '    'Panel1.Visible = False
    'End Sub

    Protected Sub ImgConferma_Click(sender As Object, e As EventArgs) Handles ImgConferma.Click
        Try
            'RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            StampaCdP(HiddenFieldIdPagamento.Value, TipoAllegato.Value)
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

        'Panel1.Visible = False
    End Sub

    Public Sub StampaAnticipo()
        ' RIPRENDO LA CONNESSIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        'RIPRENDO LA TRANSAZIONE
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        par.cmd.CommandText = "SELECT NVL(PAGAMENTI.DATA_EMISSIONE_PAGAMENTO,PAGAMENTI.DATA_EMISSIONE) AS DATA_EMISSIONE,DATA_SCADENZA,DESCRIZIONE_BREVE,PAGAMENTI.DESCRIZIONE,PAGAMENTI.PROGR,PAGAMENTI.ANNO, " _
                     & "(select descrizione from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as modalita," _
                     & "(select descrizione from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as condizione, " _
                     & "(select id from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_modalita," _
                     & "(select id from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_condizione " _
                     & ",'SAL n. '||pagamenti.progr_appalto||'/'||pagamenti.anno||' del '||siscom_mi.getdata (pagamenti.data_sal) as sal, pagamenti.id as id_pagamento " _
                     & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                     & " WHERE   PAGAMENTI.ID in (select id from siscom_mi.pagamenti where id_appalto = " & vIdAppalti & ") " _
                     & " AND PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                     & " AND PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) " _
                     & " and pagamenti.tipo_pagamento=15 " _
                     & " order by progr_appalto desc"

        Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
        Dim sal As String = ""
        If Lettore.Read Then
            HiddenFieldIdPagamento.Value = par.IfNull(Lettore("id_pagamento"), "-1")
            DataEmissione.Text = par.FormattaData(par.IfNull(Lettore("DATA_EMISSIONE"), ""))
            ADP.Text = "Attestato di pagamento N." & par.IfNull(Lettore("PROGR"), "") & "/" & par.IfNull(Lettore("ANNO"), "")
            txtModalitaPagamento.Text = par.IfNull(Lettore("modalita"), "")
            txtCondizionePagamento.Text = par.IfNull(Lettore("condizione"), "")
            idCondizione.Value = par.IfNull(Lettore("id_Condizione"), "NULL")
            idModalita.Value = par.IfNull(Lettore("id_Modalita"), "NULL")
            Me.txtDescrizioneBreve.Text = par.IfNull(Lettore("descrizione"), "")
            sal = par.IfNull(Lettore("sal"), "")
            Dim script As String = "function f(){var test = $find(""" + RadWindow4.ClientID + """); test.show(); Sys.Application.remove_load(f);}Sys.Application.add_load(f);"
            RadScriptManager.RegisterStartupScript(Page, Page.GetType(), "key", script, True)
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "alert('Nessun anticipo contrattuale presente per questo contratto!');", True)
        End If
        Lettore.Close()


    End Sub
    Private Sub SalvaProponente()
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

            '***SALVATAGGIO DEL PROPONENTE***'
            If Not String.IsNullOrEmpty(par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeProponente"), TextBox).Text.ToUpper)) Or Not String.IsNullOrEmpty(par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeProponente"), TextBox).Text.ToUpper)) Then
                par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.GESTORI_ORDINI WHERE UPPER(DESCRIZIONE) LIKE '%" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeProponente"), TextBox).Text.ToUpper) & " " & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeProponente"), TextBox).Text.ToUpper) & "%'"
                Dim numOccorrenze As String = par.cmd.ExecuteScalar
                If numOccorrenze = 0 Then
                    par.cmd.CommandText = "select siscom_mi.SEQ_GESTORI_ORDINI.nextval from dual"
                    Dim id_gestori_ordini As String = par.cmd.ExecuteScalar
                    Dim codGest = id_gestori_ordini.ToString.PadLeft(3, "0")
                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.GESTORI_ORDINI ( " _
                                        & " CODICE, DESCRIZIONE, ID, COGNOME, NOME)  " _
                                        & " VALUES ('" & codGest & "', '" _
                                        & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeProponente"), TextBox).Text.ToUpper) _
                                        & " " & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeProponente"), TextBox).Text.ToUpper) & "'," _
                                        & id_gestori_ordini & ", '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeProponente"), TextBox).Text.ToUpper) & "' , '" _
                                        & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeProponente"), TextBox).Text.ToUpper) & "')"
                    par.cmd.ExecuteNonQuery()
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET ID_GESTORE_ORDINI = " & id_gestori_ordini & " WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ")"
                    par.cmd.ExecuteNonQuery()
                Else
                    par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.GESTORI_ORDINI WHERE UPPER(DESCRIZIONE) LIKE '%" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeProponente"), TextBox).Text.ToUpper) & " " & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeProponente"), TextBox).Text.ToUpper) & "%'"
                    Dim idGestore As String = par.cmd.ExecuteScalar
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET ID_GESTORE_ORDINI = " & idGestore & " WHERE ID_GRUPPO = (SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            End If
            '*******************************'
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza: SalvaProponente " & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub
    Private Function VerificaAllegatoVariazioni() As Boolean
        VerificaAllegatoVariazioni = True
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("MODIFICA IMPORTI")
            par.cmd.CommandText = "SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & vIdAppalti
            Dim idGruppo As String = par.cmd.ExecuteScalar.ToString
            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & idGruppo & " and stato = 0"
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                VerificaAllegatoVariazioni = True
            Else
                VerificaAllegatoVariazioni = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Private Function VerificaAllegatoAttContratto() As Boolean
        VerificaAllegatoAttContratto = True
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("ATTIVAZIONE CONTRATTO")
            'par.cmd.CommandText = "SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & vIdAppalti
            'Dim idGruppo As String = par.cmd.ExecuteScalar.ToString
            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdAppalti & " and stato = 0"
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                VerificaAllegatoAttContratto = True
            Else
                VerificaAllegatoAttContratto = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Private Function VerificaAllegatoTerminiTemporali() As Boolean
        VerificaAllegatoTerminiTemporali = True
        Try
            '*******************APERURA CONNESSIONE*********************
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            Dim idTipoOggetto As String = par.getIdOggettoTipoAllegatiWs("MODIFICA TERMINI TEMPORALI")
            'par.cmd.CommandText = "SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID =" & vIdAppalti
            'Dim idGruppo As String = par.cmd.ExecuteScalar.ToString
            par.cmd.CommandText = "SELECT COUNT(ID) FROM SISCOM_MI.ALLEGATI_WS WHERE TIPO = " & idTipoOggetto & " AND OGGETTO = " & TipoAllegato.Value & " AND ID_OGGETTO = " & vIdAppalti & " and stato = 0"
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                VerificaAllegatoTerminiTemporali = True
            Else
                VerificaAllegatoTerminiTemporali = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function
    Private Sub VerificaImputazione()
        ' RIPRENDO LA CONNESSIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        'RIPRENDO LA TRANSAZIONE
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        If vIdAppalti <> 0 Then
            par.cmd.CommandText = "select * from siscom_Mi.appalti_lotti_Servizi where id_appalto= " & vIdAppalti _
                                & " and exists (select id from siscom_Mi.pf_voci_importo where id_Servizio=1 and descrizione like '05. Pulizia%' and pf_voci_importo.id=appalti_lotti_servizi.id_pf_voce_importo) " _
                                & " union " _
                                & " select * from siscom_Mi.appalti_lotti_Servizi where id_appalto= " & vIdAppalti _
                                & " and exists (select id from siscom_Mi.pf_voci_importo where id_Servizio=1 and descrizione like '06. Resa%' and pf_voci_importo.id=appalti_lotti_servizi.id_pf_voce_importo) " _
                                & " union " _
                                & " select * from siscom_Mi.appalti_lotti_Servizi where id_appalto= " & vIdAppalti _
                                & " and exists (select id from siscom_Mi.pf_voci_importo where id_Servizio=1 and descrizione like '07. Lavaggio%' and pf_voci_importo.id=appalti_lotti_servizi.id_pf_voce_importo) " _
                                & " union " _
                                & " select * from siscom_Mi.appalti_lotti_Servizi where id_appalto= " & vIdAppalti _
                                & " and exists (select id from siscom_Mi.pf_voci_importo where id_Servizio=1 and descrizione like '15. Servizio%' and pf_voci_importo.id=appalti_lotti_servizi.id_pf_voce_importo) "
            Dim dtImputazionePulizie As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)

            par.cmd.CommandText = "select * from siscom_Mi.appalti_lotti_Servizi where id_appalto= " & vIdAppalti _
                                & " and exists (select id from siscom_Mi.pf_voci_importo where id_Servizio=4 and descrizione like '04. Manutenzione in abbonamento%' and pf_voci_importo.id=appalti_lotti_servizi.id_pf_voce_importo) "
            Dim dtImputazioneAscensori As Data.DataTable = par.getDataTableGrid(par.cmd.CommandText)

            Select Case lblStato.Text.ToUpper
                Case "BOZZA"
                    '********* IMPUTAZIONE PULIZIE *********'
                    If dtImputazionePulizie.Rows.Count = 4 Then
                        RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = True
                    Else
                        RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = False
                    End If
                    btnImputazionePulizie.Visible = False
                    '***************************************'

                    '********* IMPUTAZIONE ASCENSORI *********'
                    If dtImputazioneAscensori.Rows.Count = 1 Then
                        RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = True
                    Else
                        RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = False
                    End If
                    btnImputazioneAscensori.Visible = False
                    '***************************************'
                Case "ATTIVO"
                    '********* IMPUTAZIONE PULIZIE *********'
                    If dtImputazionePulizie.Rows.Count = 4 Then
                        btnImputazionePulizie.Visible = True
                    Else
                        btnImputazionePulizie.Visible = False
                    End If
                    cmbElencoPrezzi.Enabled = False
                    '***************************************'

                    '********* IMPUTAZIONE ASCENSORI *********'
                    If dtImputazioneAscensori.Rows.Count = 1 Then
                        btnImputazioneAscensori.Visible = True
                    Else
                        btnImputazioneAscensori.Visible = False
                    End If
                    cmbElencoPrezzi.Enabled = False
                    '*****************************************'
                Case Else
                    btnImputazioneAscensori.Visible = False
                    btnImputazionePulizie.Visible = False
            End Select
        Else
            RadTabStrip.Tabs.FindTabByValue("SchedeImputazione").Visible = False
            btnImputazionePulizie.Visible = False
            btnImputazioneAscensori.Visible = False
        End If
    End Sub

    Private Sub ImpostaComboServizi()
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & lIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            If RadComboBoxAnticipo.SelectedValue = 2 Then
                'MEMORIZZO IL VALORE VECCHIO PRIMA DI RICARICARE LA COMBOBOX
                Dim vecchioValoreCombo As Integer = par.IfEmpty(RadComboBoxVoci.SelectedValue, -1)
                par.caricaComboTelerik("SELECT ID, DESCRIZIONE " _
                                       & " FROM SISCOM_MI.PF_VOCI_IMPORTO " _
                                       & " WHERE ID IN (SELECT ID_PF_VOCE_IMPORTO " _
                                       & " FROM SISCOM_MI.APPALTI_LOTTI_SERVIZI WHERE ID_APPALTO = " & vIdAppalti & ")", RadComboBoxVoci, "ID", "DESCRIZIONE", True, "-1", "")
                For Each item As RadComboBoxItem In RadComboBoxVoci.Items
                    If item.Value = vecchioValoreCombo Then
                        RadComboBoxVoci.SelectedValue = vecchioValoreCombo
                    End If
                Next
                RadComboBoxVoci.Enabled = True
            Else
                RadComboBoxVoci.ClearSelection()
                RadComboBoxVoci.Enabled = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNESSIONE" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSAZIONE" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub RadComboBoxAnticipo_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles RadComboBoxAnticipo.SelectedIndexChanged
        ImpostaComboServizi()
    End Sub
End Class