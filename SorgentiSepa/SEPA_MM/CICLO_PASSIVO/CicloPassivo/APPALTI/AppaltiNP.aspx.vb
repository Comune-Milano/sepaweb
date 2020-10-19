Imports System.Collections
Imports System.Data.OleDb
Imports ExpertPdf.HtmlToPdf
Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Math
Imports Telerik.Web.UI

Partial Class CICLO_PASSIVO_CicloPassivo_APPALTI_AppaltiNP
    Inherits PageSetIdMode

    Dim sUnita(19) As String
    Dim sDecina(9) As String


    Public numero As String
    Public CIG As String
    Public datadal As String
    Public dataal As String
    Public fornitore As String



    Public Tabber1 As String = ""
    Public Tabber2 As String = ""
    Public Tabber3 As String = ""
    Public Tabber4 As String = ""
    Public Tabber5 As String = ""
    Public Tabber6 As String = ""
    Public Tabber7 As String = ""

    Public TabberHide As String = ""
    Dim importoPRE, oneriPRE, risultato1PRE, astaPRE, risultato2PRE, ritenutaPRE, risultato3PRE, ivaPRE, risultato4PRE, risultatoImponibilePRE As Decimal

    Dim par As New CM.Global


#Region "DECLARE PROPERTY"
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

    Public Property vIdConnessione() As String
        Get
            If Not (ViewState("par_vIdConnessione") Is Nothing) Then
                Return CStr(ViewState("par_vIdConnessione"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_vIdConnessione") = value
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
    '
    Public Property vIdEsercizio() As Long
        Get
            If Not (ViewState("par_vIdEsercizio") Is Nothing) Then
                Return CLng(ViewState("par_vIdEsercizio"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_vIdEsercizio") = value
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

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If


        If CType(Page.FindControl("Tab_ImportiNP1").FindControl("consumo"), HiddenField).Value <> "" Then
            CType(Page.FindControl("Tab_ImportiNP1").FindControl("txtperconsumo"), TextBox).Text = CType(Page.FindControl("Tab_ImportiNP1").FindControl("consumo"), HiddenField).Value
        End If

        If Not IsPostBack Then
            '  Panel1.Visible = False
            TipoAllegato.Value = par.getIdOggettoAllegatiWs("Contratto")
            HFGriglia.Value = CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).ClientID & "," _
                                & CType(Tab_SLA.FindControl("DataGridSLA"), RadGrid).ClientID


            HFTAB.Value = "tab1,tab2,tab3,tab6,tab7,tab9"
            HFAltezzaTab.Value = 410
            HFAltezzaFGriglie.Value = "480,480,480,480,500"
            Try
                lIdConnessione = ""
                Response.Flush()

                Me.txtIdPianoFinanziario.Value = -1
                Me.txtnumero.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                Me.txtCup.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
                Me.txtCIG.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")


                Me.txtdatarepertorio.Attributes.Add("onkeypress", "javascript:CalendarDatePickerHide(event,this);")
                Me.txtannoinizio.Attributes.Add("onkeypress", "javascript:CalendarDatePickerHide(event,this);")
                Me.txtannofine.Attributes.Add("onkeypress", "javascript:CalendarDatePickerHide(event,this);")

                'CType(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                CType(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")
                'CType(Tab_ImportiNP1.FindControl("txtastaconsumo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                'CType(Tab_ImportiNP1.FindControl("txtonericanone"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

                'CType(Tab_ImportiNP1.FindControl("txtonericanone"), TextBox).Attributes.Add("onChange", "javascript:CalcolaPercentuale(this,document.getElementById('Tab_ImportiNP1_txtastacanone').value,document.getElementById('Tab_ImportiNP1_txtpercanone').value);")
                'CType(Tab_ImportiNP1.FindControl("txtonericonsumo"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                'CType(Tab_ImportiNP1.FindControl("txtonericonsumo"), TextBox).Attributes.Add("onChange", "javascript:CalcolaPercentuale(this,document.getElementById('Tab_ImportiNP1_txtImpContConsumo').value,document.getElementById('Tab_ImportiNP1_txtperconsumo').value);")
                'CType(Tab_VociNPl1.FindControl("txtOnerCanone"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                'CType(Tab_VociNPl1.FindControl("txtOnerConsumo"), TextBox).Attributes.Add("onBlur", "javascript:CalcolaPercentuale(this,document.getElementById('Tab_VociNPl1_txtimportoconsumo').value,document.getElementById('Tab_VociNPl1_txtperconsumo').value);")
                'CType(Tab_VociNPl1.FindControl("txtOnerConsumo"), TextBox).Attributes.Add("onChange", "javascript:")

                '*****PEPPE ADD HERE 31/09/2010

                CType(Tab_ImportiNP1.FindControl("txtfondopenali"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_ImportiNP1.FindControl("txtfondopenali"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

                CType(Tab_ImportiNP1.FindControl("txtfondoritenute"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
                CType(Tab_ImportiNP1.FindControl("txtfondoritenute"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

                'CType(Tab_ImportiNP1.FindControl("txtfondopenali"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")
                'CType(Tab_ImportiNP1.FindControl("txtfondoritenute"), TextBox).Attributes.Add("onBlur", "javascript:valid(this,'notnumbers');AutoDecimal2(this);")

                'durata in mesi
                '*******PEPPE MODIFY 04/09/2010
                txtannofine.Attributes.Add("Onblur", "javascript:CalcolaDurata(document.getElementById('txtannoinizio').value,document.getElementById('txtannofine').value);")
                txtannoinizio.Attributes.Add("Onblur", "javascript:CalcolaDurata(document.getElementById('txtannoinizio').value,document.getElementById('txtannofine').value);")


                '***SETTAGGIOI PROPERTY PER IDCONNESSIONE
                vIdConnessione = Format(Now, "yyyyMMddHHmmss")

                'Apro la connessione 
                par.OracleConn.Open()
                par.SettaCommand(par)
                HttpContext.Current.Session.Add("CONNANP" & vIdConnessione, par.OracleConn)

                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader

                'par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_MODALITA_PAG ORDER BY DESCRIZIONE", cmbModalitaPagamento, "ID", "DESCRIZIONE", True)
                'par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.TIPO_PAGAMENTO ORDER BY DESCRIZIONE", cmbCondizionePagamento, "ID", "DESCRIZIONE", True)
                'par.caricaComboBox("SELECT ID,DESCRIZIONE FROM SISCOM_MI.GESTORI_ORDINI ORDER BY DESCRIZIONE", cmbGestore, "ID", "DESCRIZIONE", False)
                par.caricaComboTelerik("SELECT ID,COGNOME ||' '|| NOME AS DESCRIZIONE FROM SEPA.OPERATORI WHERE FL_AUTORIZZAZIONE_ODL=1 and nvl(revoca,0)=0 and nvl(fl_eliminato,0)=0 and nvl(data_pw,'29991231')>'" & Format(Now, "yyyyMMdd") & "' AND ID_cAF=2 ORDER BY 2", cmbGestore, "ID", "DESCRIZIONE", True)
                Dim idTipoModalitaPag As String = "-1"
                Dim idTipoPagamento As String = "-1"
                Dim idGestoreOrdini As String = "-1"



                If Request.QueryString("IDA") > 0 Then
                    vIdAppalti = Request.QueryString("IDA")
                    RiempiCampi()
                    TabberHide = "tabbertab"
                    txtindietro.Value = 0
                    ApriRicerca()
                    Tabber1 = "tabbertabdefault"
                Else
                    vIdEsercizio = Request.QueryString("IDESERC")
                    RiempiCampi()
                    txtindietro.Value = 1
                End If

                'If idlotto = "-1" Then
                par.cmd.CommandText = "SELECT APPALTI.*,(SELECT DISTINCT ID_OPERATORE FROM SISCOM_MI.APPALTI_DL WHERE APPALTI_DL.ID_GRUPPO=APPALTI.ID_GRUPPO AND DATA_FINE_INCARICO='30000000') AS DL FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    'idlotto = par.IfNull(myReader1("ID_LOTTO"), "-1")
                    'idTipoModalitaPag = par.IfNull(myReader1("ID_TIPO_MODALITA_PAG"), "-1")
                    'idTipoPagamento = par.IfNull(myReader1("ID_TIPO_PAGAMENTO"), "-1")
                    idGestoreOrdini = par.IfNull(myReader1("DL"), "-1")
                End If
                myReader1.Close()
                'End If
                'If IsNumeric(idlotto) AndAlso CDec(idlotto) <> -1 Then
                '    par.cmd.CommandText = "SELECT SUBsTR(INIZIO,1,4) FROM SISCOM_MI.T_eSERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=(select id_Esercizio_finanziario from siscom_mi.lotti where lotti.id=" & idlotto & ")"
                '    lblEsercizioF.Text = "Anno B.P. " & par.cmd.ExecuteScalar
                'Else
                '    If IsNumeric(lotto) AndAlso CDec(lotto) <> -1 Then
                '        par.cmd.CommandText = "SELECT SUBsTR(INIZIO,1,4) FROM SISCOM_MI.T_eSERCIZIO_FINANZIARIO WHERE T_ESERCIZIO_FINANZIARIO.ID=(select id_Esercizio_finanziario from siscom_mi.lotti where lotti.id=" & lotto & ")"
                '        lblEsercizioF.Text = "Anno B.P. " & par.cmd.ExecuteScalar
                '    Else
                '        lblEsercizioF.Text = ""
                '    End If
                'End If
                'cmbModalitaPagamento.SelectedValue = idTipoModalitaPag
                'cmbCondizionePagamento.SelectedValue = idTipoPagamento
                cmbGestore.SelectedValue = idGestoreOrdini

                'cmbModalitaPagamento.Enabled = True
                'cmbCondizionePagamento.Enabled = True
                cmbGestore.Enabled = True

                Select Case UCase(lblStato.Text)
                    Case "BOZZA"
                        cmbGestore.Enabled = True
                    Case "ATTIVO"
                        If Session.Item("FL_SUPERDIRETTORE") = "1" Then
                            cmbGestore.Enabled = True
                        Else
                            cmbGestore.Enabled = False
                        End If
                        If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                            btnTornBozza.Visible = False
                        Else
                            btnTornBozza.Visible = True
                        End If

                    Case "CHIUSO"
                        cmbGestore.Enabled = False
                    Case Else
                        cmbGestore.Enabled = False
                End Select

                'max 08/03/2016 SLA
                par.caricaComboTelerik("SELECT ID,DESCRIZIONE FROM SISCOM_MI.SLA_PRIORITA ORDER BY DESCRIZIONE", CType(Tab_SLA.FindControl("cmbPriorita"), RadComboBox), "ID", "DESCRIZIONE", True)



            Catch ex As Exception
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                If Not IsNothing(par.OracleConn) Then
                    par.OracleConn.Close()
                End If
                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Rollback()
                End If
                Session.Item("LAVORAZIONE") = "0"
                HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
                HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Item("LAVORAZIONE") = "0"
                Session.Add("ERRORE", Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub
    Private Sub DurataMesi()
        If IsDate(Me.txtannoinizio.SelectedDate) AndAlso IsDate(Me.txtannofine.SelectedDate) Then
            DirectCast(Tab_ImportiNP1.FindControl("durataMesi"), HiddenField).Value = DateDiff(DateInterval.Month, CDate(txtannoinizio.SelectedDate), CDate(txtannofine.SelectedDate)) + 1
        End If
    End Sub

    Private Sub RiempiCampi()
        Try
            '*******************RICHIAMO LA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)


            'carico fornitori
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI WHERE FL_BLOCCATO=0 ORDER BY RAGIONE_SOCIALE ASC, COGNOME ASC, NOME ASC"
            Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            cmbfornitore.Items.Add(New RadComboBoxItem(" ", -1))
            While Lettore.Read
                If IsDBNull(Lettore("RAGIONE_SOCIALE")) Then
                    cmbfornitore.Items.Add(New RadComboBoxItem(par.IfNull(Lettore("COD_FORNITORE"), " ").ToString.PadRight(5) & " - " & par.IfNull(Lettore("COGNOME"), " ") & " " & par.IfNull(Lettore("NOME"), " ") & " - (" & par.IfNull(Lettore("PARTITA_IVA"), "n.d.") & ")", par.IfNull(Lettore("ID"), -1)))
                Else
                    cmbfornitore.Items.Add(New RadComboBoxItem(par.IfNull(Lettore("COD_FORNITORE"), " ").ToString.PadRight(5) & " - " & par.IfNull(Lettore("RAGIONE_SOCIALE"), " ") & " - (" & par.IfNull(Lettore("PARTITA_IVA"), " ") & ")", par.IfNull(Lettore("ID"), -1)))
                End If
            End While
            ' CType(Tab_Appalto_generale.FindControl("cmbfornitore"), DropDownList).SelectedValue = -1
            Lettore.Close()

            If vIdAppalti > 0 Then
                'ID ESERCIZIO FINANZIARIO
                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_MAIN WHERE ID_STATO=5 AND ID=(SELECT DISTINCT(id_piano_finanziario) FROM SISCOM_MI.appalti_voci_pf, siscom_mi.pf_voci WHERE appalti_voci_pf.id_pf_voce = pf_voci.ID AND appalti_voci_pf.id_appalto =" & vIdAppalti & ")"
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader

                If myReader1.Read Then
                    vIdEsercizio = par.IfNull(myReader1("ID"), -1)
                End If
                myReader1.Close()

            End If

            '/* DATO ID_ESERCIZIO RECUPERO L'ID DI PF_MAIN DEGLI ESERCIZI FINANZIARI
            If vIdEsercizio > 0 Then
                par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PF_MAIN WHERE ID_ESERCIZIO_FINANZIARIO = " & vIdEsercizio
                Lettore = par.cmd.ExecuteReader
                If Lettore.Read Then
                    vIdEsercizio = par.IfNull(Lettore("ID"), 0)
                End If
            End If
            Lettore.Close()
            txtIdPianoFinanziario.Value = vIdEsercizio


            par.cmd.CommandText = "SELECT ID FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO = " & vIdAppalti & " AND TIPO_PAGAMENTO = 9 AND ID_STATO <> -3"
            Lettore = par.cmd.ExecuteReader()
            If Lettore.Read Then
                CType(Tab_ImportiNP1.FindControl("btnPrintPagParz"), ImageButton).Visible = True
                Me.idPagRitLegge.Value = par.IfNull(Lettore("id"), 0)
            Else
                CType(Tab_ImportiNP1.FindControl("btnPrintPagParz"), ImageButton).Visible = False
            End If
            Lettore.Close()

            '/* CARICO VOCI DI SERVIZIO NEL TAB PER LE VOCI
            'CType(Tab_VociNPl1.FindControl("cmbvoce"), DropDownList).Items.Add(New ListItem(" ", -1))
            'par.cmd.CommandText = "select * from siscom_mi.pf_voci where codice like '1.02.08' and id_piano_finanziario =" & vIdEsercizio _
            '                    & " AND ID IN ( SELECT ID_VOCE FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_STRUTTURA = " & Session.Item("ID_STRUTTURA") _
            '                    & " AND (VALORE_LORDO +ASSESTAMENTO_VALORE_LORDO>0))"
            'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            'Dim dt As New Data.DataTable
            'Dim moreRow As Boolean = False
            'da.Fill(dt)
            'If dt.Rows.Count > 0 Then
            '    par.cmd.CommandText = "select * from siscom_mi.pf_voci where ID_VOCE_MADRE = " & dt.Rows(0).Item("ID")
            '    lettore = par.cmd.ExecuteReader
            '    While lettore.Read
            '        CType(Tab_VociNPl1.FindControl("cmbvoce"), DropDownList).Items.Add(New ListItem(par.IfNull(lettore("DESCRIZIONE"), ""), par.IfNull(lettore("ID"), "")))
            '        moreRow = True
            '    End While

            '    If moreRow = False Then
            '        CType(Tab_VociNPl1.FindControl("cmbvoce"), DropDownList).Items.Add(New ListItem(par.IfNull(dt.Rows(0).Item("DESCRIZIONE"), ""), par.IfNull(dt.Rows(0).Item("ID"), "")))
            '    End If
            'End If



            'dt.Dispose()
            'da.Dispose()
            'lettore.Close()




            par.cmd.CommandText = "SELECT ID_UFFICIO FROM OPERATORI WHERE ID = " & Session.Item("ID_OPERATORE")

            Lettore = par.cmd.ExecuteReader
            If Lettore.Read Then
                idStruttura.Value = Lettore("ID_UFFICIO")
            End If
            Lettore.Close()
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub ApriRicerca()
        Dim scriptblock As String
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
        'Dim dlist As CheckBoxList
        Dim ds As New Data.DataSet()

        Try
            'Riprendo la CONNESSIONE con il DB
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
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
                    CaricaDati(myReader1)
                End If
                myReader1.Close()

                par.myTrans = par.OracleConn.BeginTransaction()
                '‘par.cmd.Transaction = par.myTrans
                HttpContext.Current.Session.Add("TRANSANP" & vIdConnessione, par.myTrans)

                Session.Add("LAVORAZIONE", "1")

                If Request.QueryString("ID") > 0 Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                    'FrmSolaLettura()
                    Me.btnIndietro.Visible = False
                End If
                If lblStato.Text = "CHIUSO" Then
                    SOLO_LETTURA.Value = 1
                    'FrmSolaLettura()
                End If
                If Request.QueryString("LE") = 1 Then
                    CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                    FrmSolaLettura()
                    Me.btnIndietro.Visible = False

                End If
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                'par.OracleConn.Close()
                scriptblock = "<script language='javascript' type='text/javascript'>" _
                & "alert('Contratto aperto da un altro utente. Non è possibile effettuare modifiche!');" _
                & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
                End If

                ' LEGGO IL RECORD IN SOLO LETTURA
                par.cmd.CommandText = " select SISCOM_MI.APPALTI.* " _
                & "  from SISCOM_MI.APPALTI " _
                & "  where SISCOM_MI.APPALTI.ID=" & vIdAppalti
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    CaricaDati(myReader1)
                End While
                myReader1.Close()

                CType(Me.Page.FindControl("SOLO_LETTURA"), HiddenField).Value = "1"
                FrmSolaLettura()

                If Request.QueryString("LE") = 1 Then
                    Me.btnIndietro.Visible = False

                End If

            Else
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX1.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End If

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Sub
    Private Sub CaricaDati(ByVal myReader1 As Oracle.DataAccess.Client.OracleDataReader)
        Try




            'LEGGO APPALTI
            'Me.txtIdAppalto.Value = par.IfNull(myReader1("ID"), "-1")
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
            'DirectCast(Tab_Appalto_generale.FindControl("txtDataInizioPag"), TextBox).Text = par.FormattaData(par.IfNull(myReader1("DATA_INIZIO_PAGAMENTO"), ""))
            If myReader1("ID_STATO") = 1 Then
                Me.btnFineContratto.Enabled = True
                If VerificaAllegatoPolizza() = False Then
                    CType(Tab_ImportiNP1.FindControl("btnStampaCDP"), ImageButton).Visible = True
                End If
            End If

            If myReader1("ID_STATO") = 0 Then
                If VerificaAllegatoAttContratto() = False Then
                    Me.btnAttivaContratto.Enabled = True
                Else
                    Me.btnAttivaContratto.Enabled = False
                End If
            Else
                Me.btnAttivaContratto.Enabled = False
            End If


            If par.IfNull(myReader1("FL_RIT_LEGGE"), "-1") = 1 Then
                Me.chkRitenute.Checked = True
            End If
            If par.IfNull(myReader1("MODULO_FORNITORI"), "-1") = 1 Then
                Me.ChkModuloFO.Checked = True
            End If
            If par.IfNull(myReader1("MODULO_FORNITORI_GE"), "0") = 1 Then
                Me.ChkGestEsternaMF.Checked = True
            End If

            cmbfornitore.SelectedValue = par.IfNull(myReader1("ID_FORNITORE"), -1)
            If Me.cmbfornitore.SelectedValue > 0 Then
                LoadIbanFornitore()
                'Me.cmbIbanFornitore.SelectedValue = par.IfNull(myReader1("ID_IBAN"), -1)
            End If
            CType(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue = par.IfNull(myReader1("FEQUENZA_PAGAMENTO"), 0)
            trovato_cmbfornitore.Value = par.IfNull(myReader1("ID_FORNITORE"), -1)

            If par.IfNull(myReader1("FL_ANTICIPO"), "0") > 0 Then
                RadComboBoxAnticipo.SelectedValue = par.IfNull(myReader1("FL_ANTICIPO"), "0")
            End If

            RadNumericTextBoxNumeroRate.Text = par.IfNull(myReader1("N_RATE_ANTICIPO"), 1)

            If par.IfNull(myReader1("VOCE_ANTICIPO_NP"), -1) <> -1 Then
                ImpostaComboServizi()
                RadComboBoxVoci.SelectedValue = par.IfNull(myReader1("VOCE_ANTICIPO_NP"), -1)
            End If


            '*****PEPPE MODIFY 14/12/2010 
            'l'importo base d'asta canone e base d'asta consumo non viene più memorizzato nel DB ma è dato dal totale dei sigoli servizi caricati sull'appalo
            par.cmd.CommandText = "SELECT SUM(IMPORTO_CANONE) as ""IMPORTO_CANONE"",SUM(IMPORTO_CONSUMO) AS ""IMPORTO_CONSUMO"" FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & vIdAppalti
            Dim myReaderlotto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Text = IsNumFormat(myReaderlotto("IMPORTO_CANONE"), "", "##,##0.00")
                CType(Tab_ImportiNP1.FindControl("txtastaconsumo"), TextBox).Text = IsNumFormat(myReaderlotto("IMPORTO_CONSUMO"), "", "##,##0.00")
            End If
            myReaderlotto.Close()




            '******LEGGO GLI IMPORTI DI PENALE DA MANUTENZIONI
            'par.cmd.CommandText = "SELECT SUM(importo) FROM SISCOM_MI.APPALTI_PENALI WHERE ID_APPALTO = " & vIdAppalti

            par.cmd.CommandText = "SELECT SUM(importo) FROM SISCOM_MI.APPALTI_PENALI WHERE id_appalto in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) " _
                & " and id_prenotazione not in (select id from siscom_mi.prenotazioni where prenotazioni.id = id_prenotazione and id_stato = -3)"
            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_ImportiNP1.FindControl("txtfondopenali"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto(0), 0), "0", "##,##0.00")
            End If
            myReaderlotto.Close()

            'LEGGO GLI IMPORTI DI RITENUTE DI LEGGE DA MANUTENZIONI
            par.cmd.CommandText = "SELECT SUM(RIT_LEGGE) FROM SISCOM_MI.MANUTENZIONI WHERE STATO NOT IN (5,6) AND ID_APPALTO IN (SELECT ID FROM SISCOM_MI.APPALTI WHERE ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID = " & vIdAppalti & "))"
            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                CType(Tab_ImportiNP1.FindControl("txtfondoritenute"), TextBox).Text = IsNumFormat(par.IfNull(myReaderlotto(0), 0), "0", "##,##0.00")
            End If
            myReaderlotto.Close()

            par.cmd.CommandText = "SELECT ID_IBAN FROM SISCOM_MI.APPALTI_IBAN WHERE ID_APPALTO = " & vIdAppalti
            myReaderlotto = par.cmd.ExecuteReader
            If myReaderlotto.Read Then
                Me.cmbIbanFornitore.SelectedValue = par.IfNull(myReaderlotto("ID_IBAN"), "-1")
            End If
            myReaderlotto.Close()

            Dim totaleAnticipo As Decimal = 0
            par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUNTIVATO) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND TIPO_PAGAMENTO=15"
            totaleAnticipo = par.IfNull(par.cmd.ExecuteScalar, 0)

            Dim totaleTrattenuto As Decimal = 0
            par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE_CON_IVA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_STATO<>-3"
            totaleTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)

            'AGGIUNGO L'IVA AL TOTALE TRATTENUTO
            par.cmd.CommandText = "SELECT nvl(PERC_IVA,0) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO=" & vIdAppalti
            Dim perc_iva As Decimal = CDec(par.cmd.ExecuteScalar)
            totaleTrattenuto = totaleTrattenuto + (totaleTrattenuto * perc_iva / 100)
            CType(Tab_ImportiNP1.FindControl("txtfondoTrattenuto"), TextBox).Text = IsNumFormat(totaleAnticipo - totaleTrattenuto, "0", "##,##0.00")
            CType(Tab_ImportiNP1.FindControl("txtfondoAnticipo"), TextBox).Text = IsNumFormat(totaleAnticipo, "0", "##,##0.00")



            If Me.lblStato.Text = "ATTIVO" Then
                FrmSolaLetturaPerManutenzioni()
            End If

            If Session.Item("ID_STRUTTURA") <> myReader1("ID_STRUTTURA") Then
                FrmSolaLettura()
                Me.SOLO_LETTURA.Value = 1
            End If
            'CType(Tab_ImportiNP1.FindControl("txtpercanone"), TextBox).Text = ReplacePerc(par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Text, 0), par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtonericanone"), TextBox).Text, 0))
            'CType(Tab_ImportiNP1.FindControl("txtperconsumo"), TextBox).Text = ReplacePerc(par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtastaconsumo"), TextBox).Text, 0), par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtonericonsumo"), TextBox).Text, 0))


        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Function IsNumFormat(ByVal v As Object, ByVal S As Object, ByVal Precision As Object) As Object
        If IsDBNull(v) Then
            IsNumFormat = S
        Else
            IsNumFormat = Format(CDbl(v), Precision)
        End If
    End Function


    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        Try
            If txtModificato.Value <> "111" Then
                Session.Add("LAVORAZIONE", "0")

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.OracleConn.Close()

                Session.Item("LAVORAZIONE") = "0"

                HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
                HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)

                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                'Page.Dispose()

                'Response.Write("<script>parent.funzioni.Form1.Image3.src='../NuoveImm/Titolo_IMPIANTI.png';</script>")
                If Request.QueryString("ID") > 0 Or Request.QueryString("LE") = 1 Then
                    Response.Write("<script language='javascript'> { self.close(); }</script>")
                Else
                    Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
                End If
            Else
                txtModificato.Value = "1"
                USCITA.Value = "0"
            End If
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub


    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        If CType(Tab_ImportiNP1.FindControl("canone"), HiddenField).Value <> "" Then
            CType(Tab_ImportiNP1.FindControl("txtpercanone"), TextBox).Text = CType(Tab_ImportiNP1.FindControl("canone"), HiddenField).Value
        End If
        If CType(Tab_ImportiNP1.FindControl("consumo"), HiddenField).Value <> "" Then
            CType(Tab_ImportiNP1.FindControl("txtperconsumo"), TextBox).Text = CType(Tab_ImportiNP1.FindControl("consumo"), HiddenField).Value
        End If



        If ControlloCampi() = False Then
            Exit Sub
        End If

        If vIdAppalti > 0 Then
            Update()
            Tab_VociNPl1.CalcolaResiduo()

        Else
            Salva()
            Tab_VociNPl1.CalcolaResiduo()

        End If
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


    Private Sub Salva()
        'Dim i As Integer

        Try
            'CONTROLLO SE APPALTO E' GIA' STATO INSERITO CON STESSO NUMERO
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'APRO UNA NUOVA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSANP" & vIdConnessione, par.myTrans)

            par.cmd.CommandText = "select SISCOM_MI.APPALTI.NUM_REPERTORIO from SISCOM_MI.APPALTI WHERE SISCOM_MI.APPALTI.NUM_REPERTORIO='" & par.PulisciStrSql(Me.txtnumero.Text) & "'  AND ID_FORNITORE = " & Me.cmbfornitore.SelectedValue
            Dim myreadnum As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myreadnum.Read Then
                RadWindowManager1.RadAlert("Attenzione...Numero repertorio già presente nei nostri archivi.", 300, 150, "Attenzione", "", "null")
                myreadnum.Close()
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
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

            'Me.txtIdAppalto.Value = vIdAppalti

            ' APPALTI DA INSERIRE
            par.cmd.Parameters.Clear()
            Dim fl_ritenute As String = "0"
            If Me.chkRitenute.Checked = True Then
                fl_ritenute = "1"
            End If
            Dim fl_ModuloFO As String = "0"
            If Me.ChkModuloFO.Checked = True Then
                fl_ModuloFO = "1"
            End If
            Dim fl_ModuloFOGE As String = "0"
            If Me.ChkGestEsternaMF.Checked = True Then
                fl_ModuloFOGE = "1"
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
            'Dim PercCanone As Decimal = par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtpercanone"), TextBox).Text, 0)


            'CorreggiPerc(par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtImpContCanone"), TextBox).Text, 0), par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtonericanone"), TextBox).Text.Replace(".", ""), 0))
            'Dim PercConsumo As Decimal = par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtperconsumo"), TextBox).Text, 0)



            'CorreggiPerc(par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtImpContConsumo"), TextBox).Text, 0), par.IfEmpty(CType(Tab_ImportiNP1.FindControl("txtonericonsumo"), TextBox).Text.Replace(".", ""), 0))

            ' PEPPE MODIFY 16/12/2010 gli importi base asta consumo e base asta canone non esistono più nella tabella appalti!! li elimino dal salva e dall'update
            par.cmd.CommandText = "insert into SISCOM_MI.APPALTI" _
                                & "(ID," _
                                & "ID_FORNITORE, " _
                                & "ID_LOTTO, " _
                                & "DESCRIZIONE, " _
                                & "NUM_REPERTORIO, " _
                                & "DATA_INIZIO, " _
                                & "DATA_FINE, " _
                                & "DURATA_MESI, " _
                                & "DATA_REPERTORIO, " _
                                & "PENALI, " _
                                & "FEQUENZA_PAGAMENTO, " _
                                & "ID_STATO, " _
                                & "FL_RIT_LEGGE, " _
                                & "RUP_COGNOME, " _
                                & "RUP_NOME, " _
                                & "RUP_TEL, " _
                                & "RUP_EMAIL, " _
                                & "DL_COGNOME, " _
                                & "DL_NOME, " _
                                & "DL_TEL, " _
                                & "DL_EMAIL, " _
                                & "CUP, " _
                                & "CIG, " _
                                & "TIPO, " _
                                & "ID_STRUTTURA,MODULO_FORNITORI,MODULO_FORNITORI_GE, FL_ANTICIPO,N_RATE_ANTICIPO " _
                                & ") " _
                                & "VALUES( " & vIdAppalti & "," _
                                & RitornaNullSeMenoUno(cmbfornitore.SelectedValue) & "," _
                                & "null, " _
                                & "'" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text.ToUpper, 500)) & "'," _
                                & "'" & par.PulisciStrSql(Me.txtnumero.Text) & "'" _
                                & ",'" & par.AggiustaData(txtannoinizio.SelectedDate) & "'," _
                                & "'" & par.AggiustaData(txtannofine.SelectedDate) & "'," & par.IfEmpty(txtdurata.Text, "Null") & "" _
                                & ",'" & par.AggiustaData(Me.txtdatarepertorio.SelectedDate) & "'," _
                                & "'" & par.PulisciStrSql(par.PulisciStringaInvio(CType(Tab_Penali1.FindControl("txtpenali"), TextBox).Text, 50)) _
                                & "'," & CType(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue _
                                & "," & NumDaStato(Me.lblStato.Text) & "," & fl_ritenute _
                                & ",'" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeRup"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeRup"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelefonoRup"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailRup"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognDirect"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomDirect"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelDirect"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailDirect"), TextBox).Text.ToUpper) _
                                & "','" & par.PulisciStrSql(Me.txtCup.Text.ToUpper) _
                                & "','" & par.PulisciStrSql(Me.txtCIG.Text.ToUpper) _
                                & "'," _
                                & "'N'," _
                                & Me.idStruttura.Value & "," & fl_ModuloFO & "," & fl_ModuloFOGE & "," & fl_ANTICIPO & "," & NRateAnticipo _
                                & ")"

            par.cmd.ExecuteNonQuery()

            If cmbGestore.SelectedValue.ToString <> "-1" Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_DL (" _
               & " ID_OPERATORE, ID_GRUPPO, DATA_INIZIO_INCARICO, " _
               & " DATA_FINE_INCARICO) " _
               & " VALUES (" & cmbGestore.SelectedValue & "," _
               & "(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ")" & " , " _
               & "'" & Format(Now, "yyyyMMdd") & "', " _
               & "'30000000')"
                par.cmd.ExecuteNonQuery()
            End If




            par.cmd.CommandText = ""

            If Me.cmbIbanFornitore.SelectedValue <> -1 Then
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO,ID_IBAN) VALUES (" & vIdAppalti & "," & Me.cmbIbanFornitore.SelectedValue & ")"
                par.cmd.ExecuteNonQuery()
            End If


            par.cmd.Parameters.Clear()

            trovato_cmbfornitore.Value = cmbfornitore.SelectedValue

            Dim lstservizi As System.Collections.Generic.List(Of Mario.VociServizi)

            lstservizi = CType(HttpContext.Current.Session.Item("LSTSERVIZI"), System.Collections.Generic.List(Of Mario.VociServizi))

            For Each gen As Mario.VociServizi In lstservizi

                Dim PercOneriConsumo As Decimal = 0
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
                PercOneriConsumo = Math.Round((gen.ONERI_SICUREZZA_CONSUMO / gen.IMPORTO_CONSUMO) * 100, 4)

                par.cmd.CommandText = "insert into SISCOM_MI.APPALTI_VOCI_PF " _
                                            & " (ID_APPALTO," _
                                            & "ID_PF_VOCE," _
                                            & "IMPORTO_CANONE," _
                                            & "SCONTO_CANONE," _
                                            & "IVA_CANONE," _
                                            & "IMPORTO_CONSUMO," _
                                            & "SCONTO_CONSUMO," _
                                            & "IVA_CONSUMO," _
                                            & "ONERI_SICUREZZA_CONSUMO," _
                                            & "PERC_ONERI_SIC_CON) " _
                                            & "values (" & vIdAppalti & "," _
                                            & gen.ID_PF_VOCE_IMPORTO & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(Replace(gen.IMPORTO_CANONE, ".", "")), "Null") & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(gen.SCONTO_CANONE), "Null") & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(gen.IVA_CANONE), "Null") & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(Replace(gen.IMPORTO_CONSUMO, ".", "")), "Null") & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(gen.SCONTO_CONSUMO), "Null") & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(gen.IVA_CONSUMO), "Null") & "," _
                                            & par.IfEmpty(par.VirgoleInPunti(gen.ONERI_SICUREZZA_CONSUMO.Replace(".", "")), "Null") & "," _
                                            & par.VirgoleInPunti(PercOneriConsumo) & ")"

                par.cmd.ExecuteNonQuery()
                par.cmd.CommandText = ""
                par.cmd.Parameters.Clear()

                '*** EVENTI_APPALTI
                InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - Inserimento voce servizio")

            Next



            '*** EVENTI_APPALTI
            InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 89, "")
            If cmbfornitore.SelectedValue <> -1 Then
                '*** EVENTI_FORNITORI
                InserisciEventoFornitore(par.cmd, cmbfornitore.SelectedValue, Session.Item("ID_OPERATORE"), 89, "")
            End If

            SalvaPriorita()


            ' COMMIT
            par.myTrans.Commit()
            par.OracleConn.Close()

            par.cmd.CommandText = ""

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNANP" & vIdConnessione, par.OracleConn)

            'APRO UNA NUOVA TRANSAZIONE
            par.myTrans = par.OracleConn.BeginTransaction()
            '‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSANP" & vIdConnessione, par.myTrans)


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

            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).Rebind()


            RadNotificationNote.Text = "Salvataggio effettuato correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()



            USCITA.Value = "0"
            txtModificato.Value = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub Update()
        'Dim i As Integer

        Try

            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

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
            Dim fl_ModuloFO As String = 0
            If Me.ChkModuloFO.Checked = True Then
                fl_ModuloFO = 1
            End If
            Dim fl_ModuloFOGE As String = 0
            If Me.ChkGestEsternaMF.Checked = True Then
                fl_ModuloFOGE = 1
            End If
            Dim FL_ANTICIPO As String = 0
            If RadComboBoxAnticipo.SelectedValue > 0 Then
                FL_ANTICIPO = RadComboBoxAnticipo.SelectedValue
            End If
            If Not IsNumeric(RadNumericTextBoxNumeroRate.Text) Then
                RadNumericTextBoxNumeroRate.Text = 1
            End If
            Dim NRateAnticipo = CInt(RadNumericTextBoxNumeroRate.Text)
            'MODIFICA NELl'inserimento della percentuale di oneri di sicurezza. QUella nel database è quella dell'importo contrattuale netto.Quella visualizzata è quella dell'importo lordo!
            'Dim PercCanone As Decimal = par.IfEmpty((CType(Tab_ImportiNP1.FindControl("txtpercanone"), TextBox).Text), 0)
            'Dim PercConsumo As Decimal = par.IfEmpty((CType(Tab_ImportiNP1.FindControl("txtperconsumo"), TextBox).Text), 0)

            '***** PEPPE MODIFY 16/12/2010 ELIMINATI BASE_ASTA_CONSUMO E BASE_ASTA_CANONE PERCHè NON PIù PRESENTI NELLA TABELLA APPALTI!
            par.cmd.CommandText = "update SISCOM_MI.APPALTI set " _
                & "ID_FORNITORE=" & RitornaNullSeMenoUno(cmbfornitore.SelectedValue) & "," _
                & "DESCRIZIONE='" & par.PulisciStrSql(par.PulisciStringaInvio(Me.txtdescrizione.Text, 500)) & "'," _
                & "NUM_REPERTORIO='" & par.PulisciStrSql(Me.txtnumero.Text) & "'," _
                & "DATA_INIZIO='" & par.AggiustaData(txtannoinizio.SelectedDate) & "'," _
                & "DATA_FINE='" & par.AggiustaData(txtannofine.SelectedDate) & "'," _
                & "DURATA_MESI=" & par.IfEmpty(txtdurata.Text, "Null") & "," _
                & "DATA_REPERTORIO='" & par.AggiustaData(Me.txtdatarepertorio.SelectedDate) & "'," _
                & "PENALI='" & par.PulisciStrSql(par.PulisciStringaInvio(CType(Tab_Penali1.FindControl("txtpenali"), TextBox).Text, 50)) & "', " _
                & "FL_RIT_LEGGE =" & Ritenute & ", " _
                & "ID_STATO = " & NumDaStato(Me.lblStato.Text) & ", " _
                & "FEQUENZA_PAGAMENTO = " & CType(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue & "," _
                & "RUP_COGNOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognomeRup"), TextBox).Text.ToUpper) & "'," _
                & "RUP_NOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomeRup"), TextBox).Text.ToUpper) & "'," _
                & "RUP_TEL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelefonoRup"), TextBox).Text.ToUpper) & "'," _
                & "RUP_EMAIL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailRup"), TextBox).Text.ToUpper) & "'," _
                & "DL_COGNOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtCognDirect"), TextBox).Text.ToUpper) & "'," _
                & "DL_NOME = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtNomDirect"), TextBox).Text.ToUpper) & "'," _
                & "DL_TEL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtTelDirect"), TextBox).Text.ToUpper) & "'," _
                & "DL_EMAIL = '" & par.PulisciStrSql(DirectCast(Me.Tab_DatiAmminist1.FindControl("txtEmailDirect"), TextBox).Text.ToUpper) & "'," _
                & "CUP = '" & par.PulisciStrSql(Me.txtCup.Text.ToUpper) & "'," _
                & "CIG = '" & par.PulisciStrSql(Me.txtCIG.Text.ToUpper) & "', " _
                & "MODULO_FORNITORI=" & fl_ModuloFO _
                & ",MODULO_FORNITORI_GE=" & fl_ModuloFOGE _
                & ",FL_ANTICIPO=" & FL_ANTICIPO _
                & ",N_RATE_ANTICIPO=" & NRateAnticipo _
                & ", VOCE_ANTICIPO_NP = " & par.RitornaNullSeMenoUno(RadComboBoxVoci.SelectedValue) _
                & "  where ID_gruppo=(select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & ")"
            ' & "DATA_INIZIO_PAGAMENTO = " & par.AggiustaData(DirectCast(Tab_Appalto_generale.FindControl("txtDataInizioPag"), TextBox).Text) _

            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "select num_repertorio from siscom_mi.appalti where id = " & vIdAppalti
            Me.txtnumero.Text = par.IfNull(par.cmd.ExecuteScalar, Me.txtnumero.Text)

            par.cmd.CommandText = "SELECT ID_OPERATORE,COGNOME||' '||NOME AS NOME FROM SISCOM_MI.APPALTI_DL,SEPA.OPERATORI WHERE ID_OPERATORE=OPERATORI.ID AND ID_GRUPPO=(SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE ID=" & vIdAppalti & ") AND DATA_FINE_INCARICO='30000000'"
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            Dim id_operatore As Integer = 0
            Dim nome As String = ""
            If lettore.Read Then
                id_operatore = par.IfNull(lettore("ID_OPERATORE"), 0)
                nome = par.IfNull(lettore("NOME"), 0)
            End If
            lettore.Close()

            Dim idGestoreOrdini As String = "NULL"
            If cmbGestore.SelectedValue <> "-1" Then
                idGestoreOrdini = cmbGestore.SelectedValue
            End If

            If CStr(id_operatore) <> idGestoreOrdini And idGestoreOrdini <> "-1" Then
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



            par.cmd.CommandText = "SELECT ID_IBAN FROM SISCOM_MI.APPALTI_IBAN WHERE ID_APPALTO = " & vIdAppalti
            Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If reader.Read Then
                If par.IfNull(reader("id_iban"), 0) <> 0 Then
                    par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI_IBAN SET ID_IBAN = " & Me.cmbIbanFornitore.SelectedValue & " WHERE ID_APPALTO = " & vIdAppalti
                    par.cmd.ExecuteNonQuery()
                End If
            Else
                par.cmd.CommandText = "INSERT INTO SISCOM_MI.APPALTI_IBAN (ID_APPALTO,ID_IBAN) VALUES (" & vIdAppalti & "," & RitornaNullSeMenoUno(Me.cmbIbanFornitore.SelectedValue) & ")"
                par.cmd.ExecuteNonQuery()
            End If
            reader.Close()

            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = ""
            par.cmd.Parameters.Clear()
            If VerificaAllegatoAttContratto() = False Then
                Me.btnAttivaContratto.Enabled = True
            End If
            If NumDaStato(Me.lblStato.Text) = 1 Then
                Me.btnFineContratto.Enabled = True
                Me.btnAttivaContratto.Enabled = False
                If VerificaAllegatoPolizza() = False Then
                    CType(Tab_ImportiNP1.FindControl("btnStampaCDP"), ImageButton).Visible = True
                End If
            Else
                Me.btnFineContratto.Enabled = False
                If VerificaAllegatoPolizza() = False Then
                    CType(Tab_ImportiNP1.FindControl("btnStampaCDP"), ImageButton).Visible = False
                End If
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

            'SalvaComposizione(vIdAppalti)

            SalvaPriorita()

            par.myTrans.Commit() 'COMMIT



            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSANP" & vIdConnessione, par.myTrans)

            RadNotificationNote.Text = "Aggiornamento effettuato correttamente!"
            RadNotificationNote.AutoCloseDelay = "500"
            RadNotificationNote.Show()
            USCITA.Value = "0"
            txtModificato.Value = "0"


        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub
    Public Function ControlloCampi() As Boolean

        ControlloCampi = True

        'If Val(CType(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Text.Replace(".", "")) < Val(CType(Tab_ImportiNP1.FindControl("txtonericanone"), TextBox).Text.Replace(".", "")) Then
        '    Response.Write("<script>alert('Importo oneri canone superiore alla base d\'asta canone !');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

        'If Val(CType(Tab_ImportiNP1.FindControl("txtastaconsumo"), TextBox).Text.Replace(".", "")) < Val(CType(Tab_ImportiNP1.FindControl("txtonericonsumo"), TextBox).Text.Replace(".", "")) Then
        '    Response.Write("<script>alert('Importo oneri canone superiore alla base d\'asta consumo !');</script>")
        '    ControlloCampi = False
        '    Exit Function
        'End If

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

        'If Me.cmbIbanFornitore.SelectedValue = "-1" Then
        '    Response.Write("<script>alert('Scegliere IBAN del fornitore!');</script>")
        '    ControlloCampi = False
        '    Exit Function

        'End If

        If CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).Items.Count <= 0 Then 'CIOè ALMENO UN SERVIZIO è STATO INSERITO NELLA TABELLA!
            RadWindowManager1.RadAlert("Inserire almeno una voce di servizio!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            Exit Function
        End If

        If Not IsDate(Me.txtdatarepertorio.SelectedDate) Then
            RadWindowManager1.RadAlert("Inserire il data repertorio!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            txtdatarepertorio.Focus()
            Exit Function
        End If

        If String.IsNullOrEmpty(Me.txtCIG.Text) Then
            RadWindowManager1.RadAlert("Avvalorare il campo CIG!", 300, 150, "Attenzione", "", "null")
            ControlloCampi = False
            txtdatarepertorio.Focus()
            Exit Function
        End If




    End Function
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


    '    Return ReplacePerc
    'End Function

#End Region

#Region "Eventi"
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

#End Region


    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Tabber1 = ""
        Tabber2 = ""
        Tabber3 = ""
        Tabber4 = ""
        Tabber5 = ""
        Tabber6 = ""
        Tabber7 = ""

        If vIdAppalti <> 0 And Me.lblStato.Text = "ATTIVO" Or Me.lblStato.Text = "CHIUSO" Then
            If VerificaAllegatoTerminiTemporali() = False Then
                txtannoinizio.Enabled = True
                txtannofine.Enabled = True
            End If
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).MasterTableView.Columns.FindByUniqueName("modificaServizio").Visible = False
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).Rebind()
            TabberHide = "tabbertab"
            If Session.Item("FL_CP_VARIAZIONE_IMPORTI") = "1" Then
                If VerificaAllegatoVariazioni() Then
                    RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = False
                Else
                    RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = True
                End If
            Else
                RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = False
            End If
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

            End Select
        Else
            Select Case txttab.Value
                Case "1"
                    Tabber1 = "tabbertabdefault"
                Case "2"
                    Tabber2 = "tabbertabdefault"
                Case "3"
                    Tabber3 = "tabbertabdefault"
                Case "4"
                    Tabber4 = "tabbertabdefault"
                Case "7"
                    Tabber7 = "tabbertabdefault"
            End Select
        End If
        If Me.SOLO_LETTURA.Value = "1" Then
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).MasterTableView.Columns.FindByUniqueName("DeleteColumn").Visible = False
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).MasterTableView.Columns.FindByUniqueName("modificaServizio").Visible = False
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None
            CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).Rebind()
            Session.Add("LAVORAZIONE", "0")
            par.cmd.Dispose()
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            HttpContext.Current.Session.Remove("CONNANP" & vIdAppalti)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

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
                '   Verifica se appalto è in bozza dopo aver premuto RITORNA IN BOZZA
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

        txtIdAppalto.Value = vIdAppalti
        If RadComboBoxAnticipo.SelectedValue = 2 Then
            RadNumericTextBoxNumeroRate.Text = 1
            RadNumericTextBoxNumeroRate.Enabled = False
        End If

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
        HiddenResiduoConsumo.Value = CType(Tab_ImportiNP1.FindControl("txtresiduoConsumo"), TextBox).Text
        RadMultiPage1.SelectedIndex = i
        RadTabStrip.SelectedIndex = i
        If VerificaAllegatoPolizza() = False AndAlso Session.Item("CP_APPALTO_SINGOLA_VOCE") = "1" Then
            RadComboBoxVoci.Visible = True
        Else
            RadComboBoxVoci.ClearSelection()
            RadComboBoxVoci.Visible = False
        End If
        ScriptManager.RegisterStartupScript(Me, Me.GetType, "", "setDimensioni();", True)
    End Sub
    Private Sub FrmSolaLettura()
        'Disabilita il form (SOLO LETTURA)
        Try
            Me.btnSalva.Visible = False
            'Me.btnElimina.Visible = False
            'ImgNuovo.Visible = False
            Me.btnAttivaContratto.Enabled = False
            Me.btnFineContratto.Enabled = False
            If VerificaAllegatoPolizza() = False Then
                CType(Tab_ImportiNP1.FindControl("btnStampaCDP"), ImageButton).Visible = True
            End If
            Me.cmbGestore.Enabled = False
            DirectCast(Me.Tab_Penali1.FindControl("txtpenali"), TextBox).Enabled = False
            Me.chkRitenute.Enabled = False
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
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is RadDatePicker Then
                    DirectCast(CTRL, RadDatePicker).Enabled = False
                End If
            Next

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub FrmSolaLetturaPerManutenzioni()
        'Disabilita il form (SOLO LETTURA)
        Try
            'Me.btnSalva.Visible = False
            'Me.btnElimina.Visible = False
            'ImgNuovo.Visible = False
            DirectCast(Me.Tab_Penali1.FindControl("txtpenali"), TextBox).Enabled = False
            Me.chkRitenute.Enabled = False
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
                ElseIf TypeOf CTRL Is CheckBox Then
                    DirectCast(CTRL, CheckBox).Enabled = False
                ElseIf TypeOf CTRL Is RadComboBox Then
                    DirectCast(CTRL, RadComboBox).Enabled = False
                ElseIf TypeOf CTRL Is RadDatePicker Then
                    DirectCast(CTRL, RadDatePicker).Enabled = False
                End If
            Next

            'Me.Tab_Composizione1.soloLettura()

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Protected Sub btnAttivaContratto_Click(sender As Object, e As System.EventArgs) Handles btnAttivaContratto.Click
        Try
            If txtModificato.Value = "0" Or txtModificato.Value = "" Then


                Dim Ev As Boolean = True
                If IsDate(Me.txtannoinizio.SelectedDate) And IsDate(Me.txtannofine.SelectedDate) Then
                    'PrenotaPagamento(Ev)
                    If Ev = True Then

                        Me.txtModificato.Value = 1
                        Me.lblStato.Text = "ATTIVO"

                        FrmSolaLetturaPerManutenzioni()
                        If Session.Item("FL_CP_RITORNA_BOZZA") = "0" Then
                            btnTornBozza.Visible = False
                        Else
                            btnTornBozza.Visible = True
                        End If
                        Me.btnAttivaContratto.Enabled = False

                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.SettaCommand(par)

                        'RIPRENDO LA TRANSAZIONE
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        '‘par.cmd.Transaction = par.myTrans

                        par.cmd.CommandText = "UPDATE SISCOM_MI.APPALTI SET TOT_CONSUMO = " _
                                            & par.VirgoleInPunti(par.IfEmpty(DirectCast(Me.Tab_ImportiNP1.FindControl("txtImpTotPlusOneriCon"), TextBox).Text.Replace(".", ""), 0)) _
                                            & " WHERE ID = " & vIdAppalti
                        par.cmd.ExecuteNonQuery()


                        RadNotificationNote.Text = "Il contratto è stato ATTIVATO!Salvare le modifiche apportate tramite il pulsante Salva!"
                        RadNotificationNote.AutoCloseDelay = "3600"
                        RadNotificationNote.Show()
                        If VerificaAllegatoTerminiTemporali() = False Then
                            txtannoinizio.Enabled = True
                            txtannofine.Enabled = True
                        End If

                        Dim tipologiaAppalto As Integer = 0
                        If IsNumeric(RadComboBoxVoci.SelectedValue) AndAlso CInt(RadComboBoxVoci.SelectedValue) > 0 Then
                            tipologiaAppalto = 1
                        End If
                        Dim condizioneVoceSingola As String = ""
                        If tipologiaAppalto = 1 Then
                            'appalto su singola voce
                            condizioneVoceSingola = " AND APPALTI_VOCI_PF.ID_PF_VOCE =" & RadComboBoxVoci.SelectedValue
                        End If


                        Dim ivaPrevalente As Decimal = 0
                        'DETERMINO L'IMPORTO "PREVALENTE" LEGGENDO SOLO LA PRIMA RIGA
                        par.cmd.CommandText = " SELECT SUM(IMPORTO_CANONE),IVA_CANONE " _
                            & " FROM SISCOM_MI.APPALTI_VOCI_PF " _
                            & " WHERE ID_APPALTO = " & txtIdAppalto.Value _
                            & condizioneVoceSingola _
                            & " GROUP BY IVA_CANONE " _
                            & " UNION " _
                            & " SELECT SUM(IMPORTO_CONSUMO),IVA_CONSUMO " _
                            & " FROM SISCOM_MI.APPALTI_VOCI_PF " _
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
                            & " FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO=" & txtIdAppalto.Value _
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
                        par.cmd.CommandText = " SELECT ID_PF_VOCE, " _
                            & " IMPORTO_CANONE, " _
                            & " SCONTO_CANONE, " _
                            & " IVA_CANONE, " _
                            & " IMPORTO_CONSUMO, " _
                            & " SCONTO_CONSUMO, " _
                            & " IVA_CONSUMO, " _
                            & " ONERI_SICUREZZA_CONSUMO, " _
                            & " ONERI_SICUREZZA_CANONE " _
                            & " FROM SISCOM_MI.APPALTI_VOCI_PF " _
                            & " WHERE ID_APPALTO = " & txtIdAppalto.Value _
                            & condizioneVoceSingola
                        Lettore = par.cmd.ExecuteReader


                        ''CALCOLO DELL'IMPORTO TOTALE CONTRATTUALE
                        'par.cmd.CommandText = " SELECT ID_PF_VOCE, " _
                        '    & " IMPORTO_CONSUMO, " _
                        '    & " SCONTO_CONSUMO, " _
                        '    & " IVA_CONSUMO, " _
                        '    & " ONERI_SICUREZZA_CONSUMO, " _
                        '    & " ONERI_SICUREZZA_CANONE " _
                        '    & " FROM SISCOM_MI.APPALTI_VOCI_PF " _
                        '    & " WHERE ID_APPALTO = " & txtIdAppalto.Value
                        'Dim Lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        While Lettore.Read
                            'Dim iva As Integer = par.IfNull(Lettore("IVA_CONSUMO"), 0)
                            'Dim importoContrattualeConsumo = Math.Round((Math.Round(par.IfNull(Lettore("IMPORTO_CONSUMO"), 0) * (1 - par.IfNull(Lettore("SCONTO_CONSUMO"), 0) / 100), 2) + par.IfNull(Lettore("ONERI_SICUREZZA_CONSUMO"), 0)) * (1 + par.IfNull(Lettore("IVA_CONSUMO"), 0) / 100), 2)
                            'Dim importoAnticipo As Decimal = Math.Round(importoContrattualeConsumo * 20 / 100, 2)
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
                                        & " DATA_EMISSIONE_PAGAMENTO, DATA_AGGIORNAMENTO,  " _
                                        & "  DATA_SCADENZA, DESCRIZIONE_BREVE,  " _
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
                                  & " VALUES (" & HiddenFieldIdPrenotazione.Value & " /* ID */, " _
                                  & " (SELECT ID_FORNITORE FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & txtIdAppalto.Value & ")/* ID_FORNITORE */, " _
                                  & txtIdAppalto.Value & "/* ID_APPALTO */, " _
                                        & Lettore("ID_PF_VOCE") & " /* ID_VOCE_PF */, " _
                                        & "NULL /* ID_VOCE_PF_IMPORTO */, " _
                                  & "2 /* ID_STATO */, " _
                                  & idPagamento & " /* ID_PAGAMENTO */, " _
                                  & "15 /* TIPO_PAGAMENTO */, " _
                                  & "'ANTICIPO CONTRATTUALE' /* DESCRIZIONE */, " _
                                  & Format(Now, "yyyyyyyyMMdd") & " /* DATA_PRENOTAZIONE */, " _
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
                                       & "ID_APPALTO,ID_PF_VOCE,IMPORTO, IMPONIBILE, iva, perc_iva,TIPO )" _
                                  & "VALUES" _
                                       & "((SELECT ID_GRUPPO FROM SISCOM_MI.APPALTI WHERE APPALTI.ID=" & txtIdAppalto.Value & ")," & Lettore("ID_PF_VOCE") & "," & par.VirgoleInPunti(importoAnticipo) _
                                       & ", " & par.VirgoleInPunti(imponibile) & ", " & par.VirgoleInPunti(iva) & ", " & ivaPrevalente & "," & tipologiaAppalto & ")"
                                    ris = par.cmd.ExecuteNonQuery


                                    par.cmd.CommandText = "UPDATE SISCOM_MI.PAGAMENTI SET IMPORTO_CONSUNTIVATO=(SELECT SUM(IMPORTO_APPROVATO) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_PAGAMENTO=" & idPagamento & ") WHERE PAGAMENTI.ID=" & idPagamento
                                    ris = par.cmd.ExecuteNonQuery()


                                    'VISUALIZZAZIONE DELL'ANTICIPO NON APPENA IL CONTRATTO VIENE ATTIVATO
                                    Dim totaleAnticipo As Decimal = 0
                                    par.cmd.CommandText = "SELECT SUM(IMPORTO_CONSUNTIVATO) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND TIPO_PAGAMENTO=15"
                                    totaleAnticipo = par.IfNull(par.cmd.ExecuteScalar, 0)

                                    Dim totaleTrattenuto As Decimal = 0
                                    par.cmd.CommandText = "SELECT SUM(ANTICIPO_CONTRATTUALE_CON_IVA) FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) AND PRENOTAZIONI.ID_STATO<>-3"
                                    totaleTrattenuto = par.IfNull(par.cmd.ExecuteScalar, 0)

                                    CType(Tab_ImportiNP1.FindControl("txtfondoTrattenuto"), TextBox).Text = IsNumFormat(totaleAnticipo - totaleTrattenuto, "0", "##,##0.00")
                                    CType(Tab_ImportiNP1.FindControl("txtfondoAnticipo"), TextBox).Text = IsNumFormat(totaleAnticipo, "0", "##,##0.00")
                                    CType(Tab_VociNPl1.FindControl("DataGrid3"), RadGrid).Rebind()
                                End If
                            End If
                        End While
                        Lettore.Close()

                        '**********************************************************************

                    End If

                Else
                    RadWindowManager1.RadAlert("Impossibile attivare il contratto senza una data inizio nella sezione IMPORTI!", 300, 150, "Attenzione", "", "null")
                End If
            Else
                RadWindowManager1.RadAlert("Salvare le modifiche apportate prima di effettuare l\'operazione!", 300, 150, "Attenzione", "", "null")
            End If

        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub
    Private Sub PrenotaPagamento(ByRef Esito As Boolean)
        Try
            'Esito è impostata di default a true
            Esito = True
            If CInt(par.IfEmpty(DirectCast(Tab_ImportiNP1.FindControl("txtastacanone"), TextBox).Text, 0)) > 0 Then

                If CInt(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) > CInt(DirectCast(Me.Tab_ImportiNP1.FindControl("durataMesi"), HiddenField).Value) Then
                    RadWindowManager1.RadAlert("Impossibile attivare il contratto!La frequenza è superiore alla durata del contratto!", 300, 150, "Attenzione", "", "null")
                    Esito = False
                    'se Esito è false allora viene interrota l'operazione gestita dall'hendler dell'evento che scatta sul click del bottone AttivaContratto
                    Exit Sub
                End If
                If CInt(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) = 0 Then
                    RadWindowManager1.RadAlert("Definire la frequenza di pagamento degli importi a canone!", 300, 150, "Attenzione", "", "null")

                    Esito = False
                    'se Esito è false allora viene interrota l'operazione gestita dall'hendler dell'evento che scatta sul click del bottone AttivaContratto
                    Exit Sub

                End If
                ' RIPRENDO LA CONNESSIONE
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                'RIPRENDO LA TRANSAZIONE
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans

                par.cmd.CommandText = "SELECT ID_PF_VOCE AS PF_VOCI_IMPORTO_ID,importo_canone," _
                                    & "((IMPORTO_CANONE -((IMPORTO_CANONE * SCONTO_CANONE)/100))+(((IMPORTO_CANONE -((IMPORTO_CANONE * SCONTO_CANONE)/100))*APPALTI_VOCI_PF.IVA_CANONE)/100)) AS IMPORTO, " _
                                    & "PERC_ONERI_SIC_CAN  FROM SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_VOCI_PF " _
                                    & "WHERE ID_APPALTO = " & vIdAppalti & " AND APPALTI.ID = APPALTI_VOCI_PF.ID_APPALTO "
                Dim MyReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                Dim IMPORTO As Decimal
                'Dim UltimoInserito As String
                Dim PF_ID_VOCE As String = ""

                '****Il numero per cui dividere l'importo del servizio da ripartire nei pagamenti
                'peppe modify 002/11/2010
                'il divisore abiamo deciso diventa il numero di mesi per cui vale l'applto 
                'Dim Divisore As Double = DirectCast(Me.Tab_Appalto_generale.FindControl("durataMesi"), HiddenField).Value / DirectCast(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue
                Dim Divisore As Double = DirectCast(Me.Tab_ImportiNP1.FindControl("durataMesi"), HiddenField).Value

                Dim DataScad As String = ""
                Dim stato As String = ""
                Dim AnnoEsercizio As String = ""
                '****SELEZIONE DELLA'ANNO DELL'ESERCIZIO FINANZIARIO APPROVATO PER UN CONFRONTO CON L'ANNO DEL PAGAMENTO DA PRENOTARE!********
                par.cmd.CommandText = "SELECT SUBSTR(FINE,0,4)AS ANNO_ESERCIZIO " _
                                    & "FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_MAIN " _
                                    & "WHERE T_ESERCIZIO_FINANZIARIO.ID = PF_MAIN.ID_ESERCIZIO_FINANZIARIO AND SISCOM_MI.PF_MAIN.ID = " & vIdEsercizio
                Dim LettoreAnnoEsercizio As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If LettoreAnnoEsercizio.Read Then
                    AnnoEsercizio = LettoreAnnoEsercizio(0)
                Else
                    Exit Sub
                    RadWindowManager1.RadAlert("Nessun Esercizio Finanziario approvato trovato per attivare il contratto!", 300, 150, "Attenzione", "", "null")
                    LettoreAnnoEsercizio.Close()
                End If
                LettoreAnnoEsercizio.Close()


                Dim TotaleAppalto As Double = 0
                '******PER OGNI SERVIZIO CON IMPORTO A CANONE INSERISCO UNA PRENOTAZIONE DI PAGAMENTO
                While MyReader.Read

                    TotaleAppalto = par.IfNull(MyReader("IMPORTO"), 0) + ((par.IfNull(MyReader("IMPORTO"), 0) * par.IfNull(MyReader("PERC_ONERI_SIC_CAN"), 0)) / 100)
                    '******************AGGIUNGO LA PERCENTUALE DI ONERI SUL SERVIZIO
                    IMPORTO = par.IfNull(MyReader("IMPORTO"), 0) + ((par.IfNull(MyReader("IMPORTO"), 0) * par.IfNull(MyReader("PERC_ONERI_SIC_CAN"), 0)) / 100)
                    '****PEPPE MODIFY 02/11/2010
                    '****MOLTIPLICO L'IMPORTO MENSILE DA VERSARE PER IL NUMERO DI RATE

                    IMPORTO = (IMPORTO / Divisore) * DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue
                    Dim i As Integer = 0
                    Dim nuovoAnno As Integer = 0
                    DataScad = Me.txtannoinizio.SelectedDate
                    '**** PEPPE MODIFY 
                    vMonteVirgole = 0
                    For i = 1 To Fix(((Divisore / DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) * 100) / 100)





                        If (nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)).Substring(0, 4) <= AnnoEsercizio And (nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)).Substring(0, 4) = par.AggiustaData(DataScad).Substring(0, 4) Then
                            '*******NORMALE PRENOTAZIONE PAGAMENTO
                            stato = "0"
                            DataScad = nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)

                            par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO) VALUES" _
                            & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((IMPORTO * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                            & MyReader("PF_VOCI_IMPORTO_ID") & ", 6," & stato & ", 'CONTRATTO DI APPALTO N.P.'," & DataScad & "," _
                            & "NULL)"
                            par.cmd.ExecuteNonQuery()
                            DataScad = par.FormattaData(DataScad)

                            vMonteVirgole = vMonteVirgole + (IMPORTO - Fix((IMPORTO * 100) / 100))

                        Else

                            'Il nuovoAnno è l'anno per il quale devo prenotare i pagamenti se supera quello dell'esercizio
                            If nuovoAnno = 0 And nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad).Substring(0, 4) > nuovoAnno Then
                                'PRENOTAZIONE QUANDO SCATTA IL NUOVO ANNO DOPO TUTTE QUELLE NORMALI
                                '*******Devo inserire una prenotazione per il 31/12 dell'esercizio in corso con l'importo di conguaglio di competenza dell'esercizio stesso
                                Dim DayLeftToEnd As Integer
                                Dim ImpACavllo As Decimal

                                'If par.AggiustaData(DataScad).Substring(4, 2) = 12 Then
                                'sono i giorni che mancano alla fine dell'anno
                                DayLeftToEnd = DateDiff(DateInterval.Day, CDate("" & par.FormattaData(DataScad) & ""), CDate("31/12/2011"))
                                ImpACavllo = ((IMPORTO / DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) / 30) * DayLeftToEnd
                                'Else
                                '    MonthLeftToEnd = 12 - par.AggiustaData(DataScad).Substring(4, 2)
                                '    ImpACavllo = (IMPORTO / DirectCast(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) * MonthLeftToEnd
                                'End If

                                Dim AnnoToLeft As Integer = (nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)).Substring(0, 4) - 1
                                If AnnoToLeft = AnnoEsercizio Or AnnoToLeft = par.AggiustaData(DataScad).Substring(0, 4) Then
                                    stato = 1
                                Else
                                    stato = -1
                                End If

                                'Prenotazione al 31/12 dell'anno X
                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO) VALUES" _
                                & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                & MyReader("PF_VOCI_IMPORTO_ID") & ", 6," & stato & ", 'IMPORTO FINE E.F SU CONTRATTO DI APPALTO N.P. E CONGUAGLIO ARROTONDAMENTI'," & par.AggiustaData("31/12/" & AnnoToLeft & "") & "," _
                                & " NULL )"
                                par.cmd.ExecuteNonQuery()

                                vMonteVirgole = vMonteVirgole + (ImpACavllo - Fix((ImpACavllo * 100) / 100))


                                '**************DEVO PRENOTARE GLI IMPORTI ARROTONDATI OGNI FINE ANNO

                                'Aggiornamento sull'ultima prenotazione per eliminazione errori di arrotondamento
                                par.cmd.CommandText = "Select Sum(importo_prenotato)AS TOT_PRENOTATO ,MAX(ID) AS ID_DA_AGGIORNARE from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_voce_pf = " & MyReader("PF_VOCI_IMPORTO_ID")
                                Dim LettScritto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                If LettScritto.Read Then
                                    par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND ID = " & LettScritto("ID_DA_AGGIORNARE")
                                    Dim LettoreAggiornamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettoreAggiornamento.Read Then
                                        par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 8)) & " WHERE ID =" & LettScritto("ID_DA_AGGIORNARE")
                                        par.cmd.ExecuteNonQuery()
                                    End If
                                    LettoreAggiornamento.Close()
                                End If
                                LettScritto.Close()

                                vMonteVirgole = 0
                                '***************************************************************************************************************************************************************************
                                'Prenotazione alla scadenza calcolata per il rimanente
                                stato = "-1"
                                DataScad = nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)
                                nuovoAnno = DataScad.Substring(0, 4)

                                ImpACavllo = IMPORTO - ImpACavllo

                                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO) VALUES" _
                                & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                & MyReader("PF_VOCI_IMPORTO_ID") & ", 6," & stato & ", 'CONTRATTO DI APPALTO N.P. RIMANENZA CONGUAGLIO'," & DataScad & "," _
                                & " Null)"
                                par.cmd.ExecuteNonQuery()

                                DataScad = par.FormattaData(DataScad)

                                vMonteVirgole = vMonteVirgole + ImpACavllo - Fix((ImpACavllo * 100) / 100)

                            Else

                                'PRENOTAZIONE DELLE RATE CHE VANNO NELL'ANNO SUCCESSIVO A QUELLO SUCCESSIVO (es. inizio 2010, prenotazione 2011, in questo caso entra quando prenoterà l'importo, se esiste, successivo al 2011) ;-)
                                '********ATTENZIONE!!!*********
                                '*******NORMALE PRENOTAZIONE PAGAMENTO
                                If nuovoAnno > 0 And nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad).Substring(0, 4) <= nuovoAnno Then

                                    stato = "-1"
                                    DataScad = nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO) VALUES" _
                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((IMPORTO * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                    & MyReader("PF_VOCI_IMPORTO_ID") & ", 6," & stato & ", 'CONTRATTO DI APPALTO N.P.'," & DataScad & "," _
                                    & " NULL)"
                                    par.cmd.ExecuteNonQuery()
                                    DataScad = par.FormattaData(DataScad)

                                    vMonteVirgole = vMonteVirgole + (IMPORTO - Fix((IMPORTO * 100) / 100))
                                Else

                                    'PRENOTAZIONE QUANDO SCATTA IL NUOVO ANNO DOPO TUTTE QUELLE NORMALI
                                    '*******Devo inserire una prenotazione per il 31/12 dell'esercizio in corso con l'importo di conguaglio di competenza dell'esercizio stesso
                                    Dim DayLeftToEnd As Integer
                                    Dim ImpACavllo As Decimal

                                    'If par.AggiustaData(DataScad).Substring(4, 2) = 12 Then
                                    'sono i giorni che mancano alla fine dell'anno
                                    DayLeftToEnd = DateDiff(DateInterval.Day, CDate("" & par.FormattaData(DataScad) & ""), CDate("31/12/" & nuovoAnno & ""))
                                    ImpACavllo = ((IMPORTO / DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) / 30) * DayLeftToEnd
                                    'Else
                                    '    MonthLeftToEnd = 12 - par.AggiustaData(DataScad).Substring(4, 2)
                                    '    ImpACavllo = (IMPORTO / DirectCast(Tab_Appalto_generale.FindControl("cmbFreqPagamento"), DropDownList).SelectedValue) * MonthLeftToEnd
                                    'End If

                                    Dim AnnoToLeft As Integer = (nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)).Substring(0, 4) - 1
                                    stato = -1

                                    'Prenotazione al 31/12 dell'anno X
                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO) VALUES" _
                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                    & MyReader("PF_VOCI_IMPORTO_ID") & ", 6," & stato & ", 'IMPORTO FINE E.F SU CONTRATTO DI APPALTO N.P. E CONGUAGLIO ARROTONDAMENTI'," & par.AggiustaData("31/12/" & AnnoToLeft & "") & "," _
                                    & " NULL )"
                                    par.cmd.ExecuteNonQuery()

                                    vMonteVirgole = vMonteVirgole + (ImpACavllo - Fix((ImpACavllo * 100) / 100))


                                    '**************DEVO PRENOTARE GLI IMPORTI ARROTONDATI OGNI FINE ANNO

                                    'Aggiornamento sull'ultima prenotazione per eliminazione errori di arrotondamento
                                    par.cmd.CommandText = "Select Sum(importo_prenotato)AS TOT_PRENOTATO ,MAX(ID) AS ID_DA_AGGIORNARE from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_voce_pf = " & MyReader("PF_VOCI_IMPORTO_ID")
                                    Dim LettScritto As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                    If LettScritto.Read Then
                                        par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND ID = " & LettScritto("ID_DA_AGGIORNARE")
                                        Dim LettoreAggiornamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                                        If LettoreAggiornamento.Read Then
                                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 8)) & " WHERE ID =" & LettScritto("ID_DA_AGGIORNARE")
                                            par.cmd.ExecuteNonQuery()
                                        End If
                                        LettoreAggiornamento.Close()
                                    End If
                                    LettScritto.Close()

                                    vMonteVirgole = 0
                                    '***************************************************************************************************************************************************************************
                                    'Prenotazione alla scadenza calcolata per il rimanente
                                    stato = "-1"
                                    DataScad = nuovaData(DirectCast(Tab_ImportiNP1.FindControl("cmbFreqPagamento"), DropDownList).SelectedItem.Text, DataScad)
                                    nuovoAnno = DataScad.Substring(0, 4)

                                    ImpACavllo = IMPORTO - ImpACavllo

                                    par.cmd.CommandText = "INSERT INTO SISCOM_MI.PRENOTAZIONI(ID, DATA_PRENOTAZIONE,IMPORTO_PRENOTATO,ID_FORNITORE,ID_APPALTO,ID_VOCE_PF,TIPO_PAGAMENTO,ID_STATO,DESCRIZIONE,DATA_SCADENZA,ID_VOCE_PF_IMPORTO) VALUES" _
                                    & "(SISCOM_MI.SEQ_PRENOTAZIONI.NEXTVAL, " & par.AggiustaData(Format(Now.Date, "dd/MM/yyyy")) & ", " & par.VirgoleInPunti(Fix((ImpACavllo * 100) / 100)) & " ," & Me.cmbfornitore.SelectedValue.ToString & " ," & vIdAppalti & "," _
                                    & MyReader("PF_VOCI_IMPORTO_ID") & ", 6," & stato & ", 'CONTRATTO DI APPALTO N.P. RIMANENZA CONGUAGLIO'," & DataScad & "," _
                                    & " NULL )"
                                    par.cmd.ExecuteNonQuery()

                                    DataScad = par.FormattaData(DataScad)

                                    vMonteVirgole = vMonteVirgole + ImpACavllo - Fix((ImpACavllo * 100) / 100)


                                End If

                            End If



                        End If



                    Next

                    'Aggiornamento sull'ultima prenotazione per eliminazione errori di arrotondamento
                    par.cmd.CommandText = "Select Sum(importo_prenotato)AS TOT_PRENOTATO ,MAX(ID) AS ID_DA_AGGIORNARE from siscom_mi.prenotazioni where id_appalto = " & vIdAppalti & " and id_voce_pf = " & MyReader("PF_VOCI_IMPORTO_ID")
                    Dim TotPrenotato As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If TotPrenotato.Read Then
                        par.cmd.CommandText = "SELECT IMPORTO_PRENOTATO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND ID = " & TotPrenotato("ID_DA_AGGIORNARE")
                        Dim LettoreAggiornamento As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                        If LettoreAggiornamento.Read Then
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti(par.IfNull(LettoreAggiornamento("IMPORTO_PRENOTATO"), 0) + Math.Round(vMonteVirgole, 8)) & " WHERE ID =" & TotPrenotato("ID_DA_AGGIORNARE")
                            par.cmd.ExecuteNonQuery()
                        End If
                        LettoreAggiornamento.Close()
                    End If
                    TotPrenotato.Close()

                    vMonteVirgole = 0

                    '*************************************************
                    '*************************************************
                    '************''Regulating Prenotation*************
                    '*************************************************
                    '*************************************************

                    par.cmd.CommandText = "SELECT SUM(IMPORTO_PRENOTATO) AS PRENOTATO, COUNT(ID) AS NUMERO FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti & " and id_voce_pf= " & MyReader("PF_VOCI_IMPORTO_ID")
                    Dim Prenotato As Decimal = 0
                    Dim Nprenotation As Integer = 0
                    Dim AddPrenotation As Decimal = 0
                    Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If reader.Read Then
                        Prenotato = par.IfNull(reader("PRENOTATO"), 0)
                        Nprenotation = par.IfNull(reader("NUMERO"), 0)
                    End If
                    reader.Close()

                    If TotaleAppalto > Prenotato Then
                        AddPrenotation = (TotaleAppalto - Prenotato) / Nprenotation
                        par.cmd.CommandText = "SELECT ID,IMPORTO_PRENOTATO AS PRENOTATO FROM SISCOM_MI.PRENOTAZIONI  WHERE ID_APPALTO = " & vIdAppalti & " and id_voce_pf = " & MyReader("PF_VOCI_IMPORTO_ID")
                        reader = par.cmd.ExecuteReader
                        While reader.Read
                            par.cmd.CommandText = "UPDATE SISCOM_MI.PRENOTAZIONI SET IMPORTO_PRENOTATO = " & par.VirgoleInPunti((par.IfNull(reader("PRENOTATO"), 0) + AddPrenotation)) & " WHERE ID = " & reader("ID")
                            par.cmd.ExecuteNonQuery()
                        End While
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
        End Try

    End Sub
    Private Function nuovaData(ByVal Ripartizio As String, ByVal Data As String) As String
        nuovaData = -1


        Select Case Ripartizio.ToUpper
            Case "MENSILE"
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 1)
            Case "BIMESTRALE"
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 2)
            Case "TRIMESTRALE"
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 3)
            Case "QUADRIMESTRALE"
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 4)
            Case "SEMESTRALE"
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 6)
            Case "ANNUALE"
                nuovaData = ottieniScadenza(par.AggiustaData(Data), 11)
        End Select

        Return nuovaData

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
            If month + AddMonth <= 12 Then
                ottieniScadenza = anno & MeseQuery(month + AddMonth) & GiornoQuery(giorno)
            Else
                Dim finito As Boolean = False
                Dim M As Integer = (month + AddMonth) - 12
                While finito = False
                    i = i + 1
                    If M <= 11 Then
                        anno = anno + i
                        finito = True
                    Else
                        M = M - 12
                    End If
                End While
                ottieniScadenza = anno & MeseQuery(M) & GiornoQuery(giorno)
            End If
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

    Protected Sub btnIndietro_Click(sender As Object, e As System.EventArgs) Handles btnIndietro.Click
        If txtModificato.Value <> "111" Then
            Session.Add("LAVORAZIONE", "0")

            numero = UCase(Request.QueryString("NU"))
            'tipo = UCase(Request.QueryString("TI"))
            fornitore = UCase(Request.QueryString("FO"))
            datadal = UCase(Request.QueryString("DAL"))
            dataal = UCase(Request.QueryString("AL"))
            If Not IsNothing(Request.QueryString("CIG")) Then
                CIG = UCase(Request.QueryString("CIG"))
            Else
                CIG = ""
            End If


            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.OracleConn.Close()

            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Page.Dispose()


            If txtindietro.Value = 1 Then
                Response.Write("<script>document.location.href=""../../pagina_home_ncp.aspx""</script>")
            Else
                Response.Write("<script>location.replace('RisultatiAppalti.aspx?CIG=" & CIG & "&NU=" & par.PulisciStrSql(numero) & "&FO=" & fornitore & "&DAL=" & par.IfEmpty(datadal, "") & "&AL=" & par.IfEmpty(dataal, "") & "&TIPO=N&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF") & "');</script>")
            End If
        Else
            txtModificato.Value = "1"
            USCITA.Value = "0"
        End If
    End Sub

    Protected Sub btnFineContratto_Click(sender As Object, e As System.EventArgs) Handles btnFineContratto.Click
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
                    Response.Write("<script>alert('Il contratto può essere chiuso esclusivamente dal Direttore Lavori!');</script>")
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                    '*********************CHIUSURA CONNESSIONE**********************
                End If
            End If

            'select sulle manutenzioni per vedere se STATO = 1
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.MANUTENZIONI WHERE ID_APPALTO = " & vIdAppalti & " AND STATO = 1"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If myReader1.Read Then
                RadWindowManager1.RadAlert("Non è possibile chiudere il contratto perchè presente manutenzione attiva!", 300, 150, "Attenzione", "", "null")
                myReader1.Close()
                '*********************CHIUSURA CONNESSIONE**********************
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Exit Sub
            End If

            If Me.chkRitenute.Checked = True Then
                If ControlloCampi() = False Then
                    Exit Sub
                End If
                Dim Rimanente As Double = 0

                '******Scrittura del nuovo pagamento nell'apposita tabella!*******
                Dim Pagamento As String = CreaPagamento("VUOTO")
                If Pagamento <> "" Then
                    RadNotificationNote.Text = "Il pagamento è stato emesso e storicizzato!"
                    RadNotificationNote.AutoCloseDelay = "600"
                    RadNotificationNote.Show()

                    'lettore.Close()
                    myReader1.Close()
                    Me.lblStato.Text = "CHIUSO"
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

                        RadNotificationNote.Text = "Il pagamento è stato emesso e storicizzato!"
                        RadNotificationNote.AutoCloseDelay = "600"
                        RadNotificationNote.Show()
                        CType(Tab_ImportiNP1.FindControl("btnPrintPagParz"), ImageButton).Visible = True
                        PdfPagamento(Pagamento)
                        idPagRitLegge.Value = Pagamento
                    End If
                    Update()

                    Response.Flush()
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
                Me.lblStato.Text = "CHIUSO"
                Me.SOLO_LETTURA.Value = 1
                Me.txtModificato.Value = 1
                '************************scrittura evento chiusura contratto**************************************
                InserisciEventoAppalto(par.cmd, vIdAppalti, Session.Item("ID_OPERATORE"), 2, "Modifica appalti - CHIUSURA CONTRATTO")


                RadNotificationNote.Text = "Il contratto è stato chiuso!"
                RadNotificationNote.AutoCloseDelay = "600"
                RadNotificationNote.Show()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                Update()
                Me.FrmSolaLettura()

            End If

        Else
            RadWindowManager1.RadAlert("Nessuna modifica apportata!", 300, 150, "Attenzione", "", "null")

        End If

    End Sub
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

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PAGAMENTI (ID,DATA_EMISSIONE,DATA_STAMPA,DESCRIZIONE,IMPORTO_CONSUNTIVATO,ID_FORNITORE,ID_APPALTO,ID_STATO,TIPO_PAGAMENTO) " _
                & " VALUES (" & Id_Pagamento & "," & Format(Now, "yyyyMMdd") & ", " & Format(Now, "yyyyMMdd") & ",'PAGAMENTO RITENUTA DI LEGGE APPALTO " & par.PulisciStrSql(Me.txtnumero.Text) & "/" & par.PulisciStrSql(Me.txtdatarepertorio.SelectedDate.ToString.Substring(6)) & "'," & par.VirgoleInPunti(DirectCast(Tab_ImportiNP1.FindControl("txtfondoritenute"), TextBox).Text.Replace(".", "")) & "," & Id_Fornitore & "," & vIdAppalti & ",1,9)"
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
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

    End Function
    Private Sub PdfPagamentoOLD(ByVal ID As String)
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim sr1 As StreamReader = New StreamReader(Server.MapPath("..\..\..\TestoModelli\ModelloPagamento.htm"), System.Text.Encoding.GetEncoding("iso-8859-1"))
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
                'contenuto = Replace(contenuto, "$den_chiamante$", myReader1("DENOMINAZIONE"))
                contenuto = Replace(contenuto, "$dettaglio$", par.IfNull(myReader1("RAGIONE_SOCIALE"), "--"))
                'contenuto = Replace(contenuto, "$sc_rata$", par.FormattaData(txtScadenza.Value))
                'contenuto = Replace(contenuto, "$iban$", par.IfNull(myReader1("IBAN"), "N.D."))
                '*****************SCRITTURA TABELLA DETTAGLI dettagli
                tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>  " & par.IfNull(myReader1("RAGIONE_SOCIALE"), "--") & "</td>"
                tb1 = tb1 & "</tr></table>"
                'tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> Num.Repertorio: " & Me.txtnumero.Text & "</td>"
                'tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> Data: " & Me.txtdatarepertorio.Text & "</td>"
                'tb1 = tb1 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'> Dal: " & Me.txtannoinizio.Text & " al: " & Me.txtannofine.Text & "</td></table>"

                '*****************FINE SCRITTURA DETTAGLI
            End If
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,siscom_mi.PF_MAIN WHERE PF_MAIN.ID = " & vIdEsercizio & " AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                InizioES = par.FormattaData(myReader1("INIZIO"))
                FineEs = par.FormattaData(myReader1("FINE"))
            End If
            myReader1.Close()
            contenuto = Replace(contenuto, "$dettagli_chiamante$", "CONTRATTO")
            contenuto = Replace(contenuto, "$fornitori$", tb1)
            contenuto = Replace(contenuto, "$grigliaM$", "")
            contenuto = Replace(contenuto, "$annobp$", par.AnnoBPPag(ID))

            ' Dim idvocePf As String = ""
            'par.cmd.CommandText = "SELECT PAGAMENTI.*, T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PAGAMENTI.ID = " & ID & " AND PF_VOCI.ID = PAGAMENTI.ID_VOCE_PF AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PAGAMENTI WHERE ID = " & ID
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00"))

                '*****************SCRITTURA TABELLA CENTRALE DETTAGLI PAGAMENTO
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>" & par.IfNull(myReader1("DESCRIZIONE"), "N.D") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'>Esercizio Finanziario dal " & InizioES & " al " & FineEs & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: left; font-size:14pt;font-family :Arial ;'></td></tr>"

                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPORTO €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr></table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")))
                ' idvocePf = par.IfNull(myReader1("ID_PF_VOCE"), "")

            End If
            myReader1.Close()
            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE id = " & idvocePf
            'myReader1 = par.cmd.ExecuteReader
            'If myReader1.Read Then
            contenuto = Replace(contenuto, "$cod_capitolo$", "N.D")
            contenuto = Replace(contenuto, "$voce_pf$", "N.D")
            contenuto = Replace(contenuto, "$finanziamento$", "FONDO RITENUTE DI LEGGE")
            'End If
            'myReader1.Close()
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
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(contenuto, url & nomefile, Server.MapPath("..\..\..\NuoveImm\"))
            Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');</script>")





            '*********************CHIUSURA CONNESSIONE**********************
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try


    End Sub
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
            'par.cmd.CommandText = "SELECT PAGAMENTI.*, T_ESERCIZIO_FINANZIARIO.INIZIO AS INIZIO_ESERCIZIO,T_ESERCIZIO_FINANZIARIO.FINE AS FINE_ESERCIZIO FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.PF_VOCI,SISCOM_MI.PF_MAIN,SISCOM_MI.T_ESERCIZIO_FINANZIARIO WHERE PAGAMENTI.ID = " & ID & " AND PF_VOCI.ID = PAGAMENTI.ID_VOCE_PF AND PF_VOCI.ID_PIANO_FINANZIARIO = PF_MAIN.ID AND PF_MAIN.ID_ESERCIZIO_FINANZIARIO = T_ESERCIZIO_FINANZIARIO.ID"
            par.cmd.CommandText = "SELECT (select siscom_mi.getdata(data_inizio)||' - '||siscom_mi.getdata(data_fine) from siscom_mi.appalti where appalti.id=pagamenti.id_appalto) as data_contratto,pagamenti.* FROM SISCOM_MI.PAGAMENTI WHERE ID = " & ID
            myReader1 = par.cmd.ExecuteReader
            If myReader1.Read Then
                contenuto = Replace(contenuto, "$anno$", par.IfNull(myReader1("ANNO"), ""))
                contenuto = Replace(contenuto, "$progr$", par.IfNull(myReader1("PROGR"), ""))
                contenuto = Replace(contenuto, "$data_emissione$", par.FormattaData(myReader1("DATA_EMISSIONE")))
                contenuto = Replace(contenuto, "$data_stampa$", par.FormattaData(myReader1("DATA_STAMPA")))
                contenuto = Replace(contenuto, "$TOT$", Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00"))
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
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0) - imponibile, "##,##0.00") & "</td></tr>"
                tb2 = tb2 & "<tr><td style='text-align: right; font-size:14pt;font-family :Arial ;'>IMPORTO €.</td>"
                tb2 = tb2 & "<td style='text-align: right; font-size:14pt;font-family :Arial ;'>" & Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00") & "</td>"
                tb2 = tb2 & "</tr></table>"
                contenuto = Replace(contenuto, "$dettagli$", tb2)
                '*****************FINE SCRITTURA DETTAGLI
                contenuto = Replace(contenuto, "$imp_letterale$", "EURO " & NumeroInLettere(Format(par.IfNull(myReader1("IMPORTO_CONSUNTIVATO"), 0), "##,##0.00")))

            End If
            myReader1.Close()

            tb1 = "<table style='width:100%;'>"
            tb2 = "<table style='width:100%;'>"
            Dim tb3 As String = "<table style='width:100%;'>"
            par.cmd.CommandText = "SELECT PF_VOCI.CODICE,PF_VOCI.DESCRIZIONE,SUM(PRENOTAZIONI.RIT_LEGGE_IVATA) as RIT_LEGGE_IVATA FROM SISCOM_MI.PF_VOCI, SISCOM_MI.PRENOTAZIONI " _
                                & "WHERE prenotazioni.id_Stato<>-3 and PF_VOCI.ID = PRENOTAZIONI.id_voce_pf AND rit_legge_ivata IS NOT NULL AND rit_legge_ivata > 0 AND PRENOTAZIONI.ID_PAGAMENTO IS NOT NULL and ID_APPALTO in ((select id from siscom_mi.appalti where id_gruppo = (select id_gruppo from siscom_mi.appalti where id = " & vIdAppalti & "))) " _
                                & "GROUP BY PF_VOCI.CODICE, PF_VOCI.DESCRIZIONE "
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
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
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

    Protected Sub cmbfornitore_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbfornitore.SelectedIndexChanged
        LoadIbanFornitore()


    End Sub
    Private Sub LoadIbanFornitore()
        Try

            If Me.cmbfornitore.SelectedValue <> -1 Then
                Me.cmbIbanFornitore.Items.Clear()
                '*******************RICHIAMO LA CONNESSIONE*********************
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
                If vIdAppalti > 0 Then
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    '‘par.cmd.Transaction = par.myTrans

                End If

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.FORNITORI_IBAN WHERE ID_FORNITORE = " & Me.cmbfornitore.SelectedValue
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                cmbIbanFornitore.Items.Add(New RadComboBoxItem(" ", -1))
                While lettore.Read
                    cmbIbanFornitore.Items.Add(New RadComboBoxItem(par.IfNull(lettore("IBAN"), "---"), par.IfNull(lettore("ID"), -1)))

                End While
                lettore.Close()

            Else
                Me.cmbIbanFornitore.Items.Clear()
                cmbIbanFornitore.Items.Add(New RadComboBoxItem(" ", -1))
            End If
        Catch ex As Exception
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            If Not IsNothing(par.OracleConn) Then
                par.OracleConn.Close()
            End If
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")

        End Try

    End Sub


    Protected Sub btnTornBozza_Click(sender As Object, e As System.EventArgs) Handles btnTornBozza.Click
        If Me.ConfRitBozza.Value = 1 Then
            '*******************APERURA CONNESSIONE*********************
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans



            ' APPALTI
            par.cmd.CommandText = "update siscom_mi.appalti set id_stato = 0 where id in (select id from siscom_mi.appalti b where b.id_gruppo = " & vIdAppalti & ")"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "SELECT nvl(COUNT(ID),0) FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO = " & vIdAppalti & " AND ID_STATO = 0"
            Dim numPagamenti As Integer = CInt(par.cmd.ExecuteScalar.ToString)
            If numPagamenti > 0 Then
                'DELETE
                par.cmd.CommandText = "DELETE FROM SISCOM_MI.PRENOTAZIONI WHERE ID_APPALTO = " & vIdAppalti
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.PAGAMENTI WHERE ID_APPALTO = " & vIdAppalti
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO = " & vIdAppalti
                par.cmd.ExecuteNonQuery()
                CType(Tab_ImportiNP1.FindControl("btnStampaCDP"), ImageButton).Visible = False
            Else
                RadWindowManager1.RadAlert("Impossibile gestire l'anticipo!E' stato emesso il CDP.", 300, 150, "Attenzione", "", "null")
                CType(Tab_ImportiNP1.FindControl("btnStampaCDP"), ImageButton).Visible = True
            End If

            par.myTrans.Commit()
            par.OracleConn.Close()

            Session.Item("LAVORAZIONE") = "0"

            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)

            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            'Page.Dispose()


            'Response.Redirect("AppaltiNP.aspx?CIG=" & Request.QueryString("CIG") & "&FO=" & par.PulisciStrSql(Request.QueryString("FO")) & "&NU=" & par.PulisciStrSql(Request.QueryString("NU")) & "&DAL=" & par.IfEmpty(Request.QueryString("DAL"), "") & "&AL=" & par.IfEmpty(Request.QueryString("AL"), "") & "&IDA=" & Request.QueryString("IDA") & "&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF"))

            Response.Redirect("AppaltiNP.aspx?CIG=" & Request.QueryString("CIG") & "&FO=" & Request.QueryString("FO") & "&NU=" & Request.QueryString("NU") & "&DAL=" & Request.QueryString("DAL") & "&AL=" & Request.QueryString("AL") & "&IDA=" & Request.QueryString("IDA") & "&ST=" & Request.QueryString("ST") & "&EF=" & Request.QueryString("EF"))

        End If
    End Sub
    Private Sub NascondiTab()
        'RadTabStrip.Tabs.FindTabByValue("VariazServizi").Visible = False
        'RadTabStrip.Tabs.FindTabByValue("VariazLavori").Visible = False
        RadTabStrip.Tabs.FindTabByValue("Variazioni").Visible = False
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
            ' APRO CONNESSIONE

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)


            End If

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
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
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
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
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans


            par.cmd.CommandText = "SELECT COUNT(ID_APPALTO) FROM SISCOM_MI.APPALTI_ANTICIPI_CONTRATTUALI WHERE ID_APPALTO = " & vIdAppalti
            Dim n As Integer = par.IfNull(par.cmd.ExecuteScalar, 0)
            If n = 0 Then
                VerificaTornaInBozza = True
            Else
                VerificaTornaInBozza = False
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNANP" & vIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & vIdConnessione)
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
                        par.cmd.CommandText = "update SISCOM_MI.PAGAMENTI set ID_STATO=1, DATA_STAMPA=" & Format(Now, "yyyyMMdd") & " where ID=" & sValorePagamento
                        par.cmd.ExecuteNonQuery()
                        par.cmd.CommandText = ""

                        RadNotificationNote.Text = "Il pagamento è stato emesso e storicizzato!"
                        RadNotificationNote.AutoCloseDelay = "600"
                        RadNotificationNote.Show()

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
                    While myReader2.Read
                        CalcolaImporti(par.IfNull(myReader2("IMPORTO_APPROVATO"), 0), par.IfNull(myReader1("FL_RIT_LEGGE"), 0), par.IfNull(myReader2("ID_VOCE_PF_IMPORTO"), 0), par.IfNull(myReader2("ID_APPALTO"), 0), "PRENOTATO", par.IfNull(myReader2("PERC_IVA"), -1))
                    End While
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

                    'par.cmd.CommandText = "select distinct(ID_VOCE) from SISCOM_MI.PF_VOCI_IMPORTO " _
                    '                  & " where ID in (select ID_VOCE_PF_IMPORTO from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento & ")"

                    par.cmd.CommandText = "select distinct(ID_VOCE_PF)  as ID_VOCE , " _
                                            & "(SELECT DISTINCT RTRIM (LTRIM (SUBSTR (inizio, 1, 4))) AS annoBp FROM siscom_mi.T_ESERCIZIO_FINANZIARIO  WHERE id IN " _
                                            & "(select id_esercizio_finanziario from siscom_mi.pf_main where id in (SELECT DISTINCT id_piano_finanziario FROM siscom_mi.pf_voci WHERE id = PRENOTAZIONI.ID_VOCE_PF))" _
                                            & ") AS ANNO " _
                                        & " from SISCOM_MI.PRENOTAZIONI where ID_PAGAMENTO=" & sValorePagamento


                    myReaderBP = par.cmd.ExecuteReader

                    While myReaderBP.Read
                        'X OGNI TIPO DI VOCE
                        par.cmd.CommandText = "select PRENOTAZIONI.* " _
                                          & " from   SISCOM_MI.PRENOTAZIONI " _
                                          & " where ID_PAGAMENTO=" & sValorePagamento _
                                          & "   and ID_VOCE_PF=" & par.IfNull(myReaderBP("ID_VOCE"), 0)

                        'par.cmd.CommandText = "select PRENOTAZIONI.*,APPALTI_PENALI.IMPORTO as ""PENALE2"" " _
                        '                  & " from   SISCOM_MI.PRENOTAZIONI,SISCOM_MI.APPALTI_PENALI" _
                        '                  & " where ID_PAGAMENTO=" & sValorePagamento _
                        '                  & "   and ID_VOCE_PF_IMPORTO in (select ID from SISCOM_MI.PF_VOCI_IMPORTO where ID_VOCE=" & par.IfNull(myReaderBP("ID_VOCE"), 0) & ")" _
                        '                  & "   and SISCOM_MI.PRENOTAZIONI.ID=SISCOM_MI.APPALTI_PENALI.ID_PRENOTAZIONE (+) "


                        Dim myReaderB As Oracle.DataAccess.Client.OracleDataReader
                        myReaderB = par.cmd.ExecuteReader

                        While myReaderB.Read
                            '***controllo che il valore CONSUNTIVATO di spesa esista e sia maggiore di 0
                            If par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0) > 0 Then

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
                                                                            & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("ANNO"), "") & "</td>" _
                                                                            & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderB3("DESCRIZIONE"), "") & "</td>" _
                                                                            & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("ANNO"), "") & "</td>" _
                                                                            & "<td align='right'   style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & IsNumFormat(par.IfNull(myReaderB("IMPORTO_APPROVATO"), 0), "", "##,##0.00") & "</td>" _
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
                                                                        & "<td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 9pt'>" & par.IfNull(myReaderBP("ANNO"), "") & "</td>" _
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
                HttpContext.Current.Session.Add("TRANSANP" & lIdConnessione, par.myTrans)
                Response.Write("<script>window.open('../../../FileTemp/" & nomefile & "','AttPagamento','');</script>")
            Else
                Response.Write("<script>window.open('../../../ALLEGATI/APPALTI/" & nome & "','SAL','');self.close();</script>")

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
    '    Panel1.Visible = False
    'End Sub

    Protected Sub ImgConferma_Click(sender As Object, e As System.EventArgs) Handles ImgConferma.Click
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans
            StampaCdP(HiddenFieldIdPagamento.Value, TipoAllegato.Value)
        Catch ex As Exception
            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:Stampa Pagamento Manutenzione" & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try

        '  Panel1.Visible = False
    End Sub

    Public Sub StampaAnticipo()
        ' RIPRENDO LA CONNESSIONE
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)

        'RIPRENDO LA TRANSAZIONE
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans
        par.cmd.CommandText = "SELECT NVL(PAGAMENTI.DATA_EMISSIONE_PAGAMENTO,PAGAMENTI.DATA_EMISSIONE) AS DATA_EMISSIONE,DATA_SCADENZA,DESCRIZIONE_BREVE,PAGAMENTI.DESCRIZIONE,PAGAMENTI.PROGR,PAGAMENTI.ANNO, " _
                     & "(select descrizione from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as modalita," _
                     & "(select descrizione from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as condizione, " _
                     & "(select id from siscom_mi.tipo_modalita_pag where id in (select id_tipo_modalita_pag from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_modalita," _
                     & "(select id from siscom_mi.tipo_pagamento where id in (select id_tipo_pagamento from siscom_mi.appalti where appalti.id=pagamenti.id_appalto)) as id_condizione " _
                     & ",'SAL n. '||pagamenti.progr_appalto||'/'||pagamenti.anno||' del '||siscom_mi.getdata (pagamenti.data_sal) as sal, pagamenti.id as id_pagamento " _
                     & " FROM SISCOM_MI.PAGAMENTI,SISCOM_MI.APPALTI,SISCOM_MI.FORNITORI " _
                     & " WHERE   PAGAMENTI.ID in (select id from siscom_mi.pagamenti where id_appalto = " & vIdAppalti & ") " _
                     & " AND PAGAMENTI.ID_APPALTO=APPALTI.ID (+) " _
                     & " AND PAGAMENTI.ID_FORNITORE=FORNITORI.ID (+) "
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

    Private Function VerificaAllegatoAttContratto() As Boolean
        VerificaAllegatoAttContratto = True
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
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
            HttpContext.Current.Session.Remove("CONNANP" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function


    Private Function VerificaAllegatoVariazioni() As Boolean
        VerificaAllegatoVariazioni = True
        Try
            Dim FlagConnessione As Boolean
            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

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

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("CONNANP" & lIdConnessione)
            HttpContext.Current.Session.Remove("TRANSANP" & lIdConnessione)
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Private Sub ImpostaComboServizi()
        Try
            ' RIPRENDO LA CONNESSIONE
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNANP" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            'RIPRENDO LA TRANSAZIONE
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSANP" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            If RadComboBoxAnticipo.SelectedValue = 2 Then
                'MEMORIZZO IL VALORE VECCHIO PRIMA DI RICARICARE LA COMBOBOX
                Dim vecchioValoreCombo As Integer = par.IfEmpty(RadComboBoxVoci.SelectedValue, -1)
                par.caricaComboTelerik("SELECT ID, DESCRIZIONE " _
                                       & " FROM SISCOM_MI.PF_VOCI " _
                                       & " WHERE ID IN (SELECT ID_PF_VOCE " _
                                       & " FROM SISCOM_MI.APPALTI_VOCI_PF WHERE ID_APPALTO = " & vIdAppalti & ")", RadComboBoxVoci, "ID", "DESCRIZIONE", True, "-1", "")
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

End Class

