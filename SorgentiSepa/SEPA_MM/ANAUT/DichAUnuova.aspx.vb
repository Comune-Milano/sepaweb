Imports System.IO
Imports ExpertPdf.HtmlToPdf

Partial Class ANAUT_DichAUnuova
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Dim sUnita(19) As String
    Dim sDecina(9) As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False And bEseguito = False Then

            PageID.Value = par.getPageId

            vIdConnessione = Format(Now, "yyyyMMddHHmmss")
            'txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            imgUscita.Attributes.Add("onclick", "javascript:Uscita=1;")
            cmbAnnoReddituale.Attributes.Add("onclick", "javascript:Uscita=1;")
            MenuStampe.Attributes.Add("onclick", "javascript:Uscita=1;")
            rdApplica.Attributes.Add("onclick", "javascript:document.getElementById('txt36').value=1;")
            rdNoApplica.Attributes.Add("onclick", "javascript:document.getElementById('txt36').value=0;")

            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")


            txtCognome.Attributes.Add("OnChange", "javascript:AzzeraCF(txtCF,txtbinserito);")
            txtNome.Attributes.Add("OnChange", "javascript:AzzeraCF(txtCF,txtbinserito);")
            cmbComuneNas.Attributes.Add("OnChange", "javascript:AzzeraCF(txtCF,txtbinserito);")
            txtCF.Attributes.Add("OnChange", "javascript:AttendiCF();")

            txtDataNascita.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtDataPG.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtCSData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtPSScade.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtCIData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtPSData.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")
            txtPSRinnovo.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")

            txtData392.Attributes.Add("onkeypress", "javascript:CompletaData(event,this);")


            CType(Tab_ISEE1.FindControl("textISEE"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CType(Tab_ISEE1.FindControl("textISEE"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            CType(Tab_ISEE1.FindControl("textISE"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CType(Tab_ISEE1.FindControl("textISE"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            CType(Tab_ISEE1.FindControl("textISP"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CType(Tab_ISEE1.FindControl("textISP"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            CType(Tab_ISEE1.FindControl("textISR"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CType(Tab_ISEE1.FindControl("textISR"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            bMemorizzato = False
            lIdDichiarazione = Request.QueryString("ID")
            SoloLettura = Request.QueryString("LE")

            CHIUDI = Request.QueryString("CHIUDI")
            TORNA = Request.QueryString("TORNA")

            If Session.Item("ID_CAF") = "6" Then
                If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
                    SoloLettura = "1"
                    nonstampare.Value = "1"
                End If
            End If

            If Session.Item("LIVELLO") = "1" Then
                SoloLettura = "0"
            End If

            If Session.Item("PROP_DEC") = "1" Then
                propdec.Value = "0"
            Else
                propdec.Value = "1"
            End If
            If SoloLettura = "1" Then
                cmbStato.Visible = False
                rdApplica.Enabled = False
                rdNoApplica.Enabled = False
                cmbAnnoReddituale.Enabled = False
            End If



            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If

            'VISUALIZZO IMG_CANONE SOLO PER GLI ABUSIVI
            If par.IfNull(Request.QueryString("CR"), "") = "1" Then
                IMGCanone.Visible = True
            Else
                IMGCanone.Visible = False
            End If

            assTemp.Value = Request.QueryString("ASST")
            If lIdDichiarazione = -1 Then
                lNuovaDichiarazione = 1
                txtbinseritoTab.Value = "0"
                lbl45_Lotto.Visible = True
                txtCodConvocazione.Visible = True
                lotto45.Value = "1"
            Else
                lNuovaDichiarazione = 0
                VisualizzaDichiarazione()
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.CriptaMolto(lIdDichiarazione) & "&T=1','Anagrafe2','top=0,left=0,width=600,height=400');")
                End If
            End If
            bEseguito = True
            Session.Add("COLIGHT", CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text)
            Session.Add("AULIGHT", par.CriptaMolto(txtPosizione.Text))

            If SoloLettura = "1" Or AprisolaLettura = "1" Then
                InLettura.Value = par.Cripta(lIdDichiarazione & "-LETTURA")
            Else
                InLettura.Value = par.Cripta(lIdDichiarazione & "-MODIFICA")
            End If

            'max 31/03/2016
            lblData392.Visible = False
            txtData392.Visible = False
            If Request.QueryString("N") = "1" Or RU392.Value = "1" Then
                If txtCognome.Text = "" Or txtCF.Text = "" Then
                    RU392.Value = "1"
                    DatiSoloLettura392()
                    txtCognome.Enabled = True
                    txtNome.Enabled = True
                    txtCF.Enabled = True
                End If
                lblData392.Visible = True
                txtData392.Visible = True
            Else
                RU392.Value = "0"
                lblImportaRes.Visible = False
                btnImportaRes.Visible = False
                btnImportaRec.Visible = False
                lblImportaRec.Visible = False
            End If
            If Request.QueryString("N") = "1" Or RU431.Value = "1" Then
                If txtCognome.Text = "" Or txtCF.Text = "" Then
                    RU431.Value = "1"
                    DatiSoloLettura392()
                    txtCognome.Enabled = True
                    txtNome.Enabled = True
                    txtCF.Enabled = True
                End If
            Else
                RU431.Value = "0"
                lblImportaRes.Visible = False
                btnImportaRes.Visible = False
                btnImportaRec.Visible = False
                lblImportaRec.Visible = False
            End If


            If SoloLettura = "1" Or AprisolaLettura = "1" Then
                lblImportaRes.Visible = False
                btnImportaRes.Visible = False

                btnImportaRec.Visible = False
                lblImportaRec.Visible = False
            End If
        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey111", "document.getElementById('caric').style.visibility = 'hidden';", True)
        End If
        VerificaIndirizzoRecapito()
        If inUscita.Value <> "1" Then
            SettaControlModifiche(Me)
            If Session.Item("ANAGRAFE") = "1" Then
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../Anagrafe.aspx?ID=" & par.CriptaMolto(lIdDichiarazione) & "&T=1','Anagrafe2','top=0,left=0,width=600,height=400');")
            End If

            'datapicker su date
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey112", "$(function () {$(" & Chr(34) & "#txtData392" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });$(" & Chr(34) & "#txtPSRinnovo" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });$(" & Chr(34) & "#txtPSScade" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });$(" & Chr(34) & "#txtDataPG" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10); } });$(" & Chr(34) & "#txtPSData" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});$(" & Chr(34) & "#txtCSData" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});$(" & Chr(34) & "#txtCIData" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});$(" & Chr(34) & "#txtDataNascita" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});$(" & Chr(34) & "#Tab_InfoContratto1_txtDataCessazione" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});$(" & Chr(34) & "#Tab_InfoContratto1_txtDataDec" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});$(" & Chr(34) & "#dic_Documenti1_txtDataPresentaz" & Chr(34) & ").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(" & Chr(34) & ".ui-datepicker" & Chr(34) & ").css('font-size', 10);}});});", True)

            If Session.Item("ANAGRAFE_UTENZA_LIGHT") = "1" Then
                imgPatrimonio.Attributes.Add("onclick", "if (document.getElementById('txtPosizione').value != '') {window.open('VisualizzaUnita.aspx','UImmob','height=600,top=0,left=0,width=790');} else {alert('Codice unità non valido!');}")
                CType(Tab_InfoContratto1.FindControl("imgRU"), System.Web.UI.WebControls.Image).Attributes.Add("onclick", "if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {window.open('VisualizzaContratto.aspx','RU','height=780,top=0,left=0,width=1160');} else {alert('Codice contratto non valido!');}")
            Else
                imgPatrimonio.Attributes.Add("onclick", "if (document.getElementById('txtPosizione').value != '') {window.open('../CENSIMENTO/InserimentoUniImmob.aspx?X=1&LE=1&ID=" & txtPosizione.Text & "','UImmob','height=580,top=0,left=0,width=780');} else {alert('Codice unità non valido!');}")
                CType(Tab_InfoContratto1.FindControl("imgRU"), System.Web.UI.WebControls.Image).Attributes.Add("onclick", "if (document.getElementById('Tab_InfoContratto1_TxtRapporto').value != '') {window.open('../Contratti/Contratto.aspx?LT=1&ID=" & IndiceContratto & "','Contratto" & Format(Now, "hhss") & "','height=780,width=1160');} else {alert('Codice contratto non valido!');}")
            End If

        End If
    End Sub

    Private Sub SettaControlModifiche(ByVal obj As Control)
        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                SettaControlModifiche(CTRL)
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Attributes.Add("onchange", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Attributes.Add("onclick", "javascript:document.getElementById('txtModificato').value='1';")
            ElseIf TypeOf CTRL Is Button Then
                DirectCast(CTRL, Button).Attributes.Add("onclick", "javascript:document.getElementById('caric').style.visibility ='visible';")
            End If
        Next
    End Sub

    Public Property dtComp() As Data.DataTable
        Get
            If Not (ViewState("dtComp") Is Nothing) Then
                Return ViewState("dtComp")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtComp") = value
        End Set
    End Property

    Public Property dtCompSpese() As Data.DataTable
        Get
            If Not (ViewState("dtCompSpese") Is Nothing) Then
                Return ViewState("dtCompSpese")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtCompSpese") = value
        End Set
    End Property

    Public Property dtRedd() As Data.DataTable
        Get
            If Not (ViewState("dtRedd") Is Nothing) Then
                Return ViewState("dtRedd")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtRedd") = value
        End Set
    End Property

    Public Property dtDetraz() As Data.DataTable
        Get
            If Not (ViewState("dtDetraz") Is Nothing) Then
                Return ViewState("dtDetraz")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtDetraz") = value
        End Set
    End Property

    Public Property dtDoc() As Data.DataTable
        Get
            If Not (ViewState("dtDoc") Is Nothing) Then
                Return ViewState("dtDoc")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtDoc") = value
        End Set
    End Property

    Public Property dtDocPresenti() As Data.DataTable
        Get
            If Not (ViewState("dtdtDocPresenti") Is Nothing) Then
                Return ViewState("dtdtDocPresenti")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtdtDocPresenti") = value
        End Set
    End Property

    Public Property dtMob() As Data.DataTable
        Get
            If Not (ViewState("dtMob") Is Nothing) Then
                Return ViewState("dtMob")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtMob") = value
        End Set
    End Property

    Public Property dtImmob() As Data.DataTable
        Get
            If Not (ViewState("dtImmob") Is Nothing) Then
                Return ViewState("dtImmob")
            Else
                Return New Data.DataTable
            End If
        End Get
        Set(ByVal value As Data.DataTable)
            ViewState("dtImmob") = value
        End Set
    End Property

    Public Sub CaricaNucleo()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            Dim DataRiferimentoMinori As String = ""
            par.cmd.CommandText = "SELECT TASSO_RENDIMENTO,ANNO_AU FROM UTENZA_BANDI WHERE ID=" & lIndice_Bando
            Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader11.Read() Then
                DataRiferimentoMinori = par.IfNull(myReader11("ANNO_AU"), Year(Now)) & "1231"
            End If
            myReader11.Close()

            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.*,TO_CHAR(TO_DATE(DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASC, NATURA_INVAL,(CASE WHEN INDENNITA_ACC ='0' THEN 'NO' ELSE 'SI' END) AS IND_ACCOMP, T_TIPO_PARENTELA.DESCRIZIONE,(CASE WHEN TIPO_INVAL ='D' THEN 'Definitiva' WHEN TIPO_INVAL ='P' THEN 'Provvisoria' ELSE '' END) AS TIPO_INVALIDITA FROM UTENZA_COMP_NUCLEO,T_TIPO_PARENTELA" _
                & " where UTENZA_COMP_NUCLEO.GRADO_PARENTELA=T_TIPO_PARENTELA.COD and UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " ORDER BY PROGR ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtComp = New Data.DataTable
            da.Fill(dtComp)
            da.Dispose()

            Dim eta As Integer = 0

            N_INV_100_ACC = 0
            N_INV_100_NO_ACC = 0
            N_INV_100_66 = 0
            MINORI_15 = 0
            MAGGIORI_65 = 0

            If dtComp.Rows.Count > 0 Then

                For Each row As Data.DataRow In dtComp.Rows
                    eta = par.RicavaEtaChiusura(par.IfNull(row.Item("DATA_NASCITA"), ""), DataRiferimentoMinori)

                    If eta < 15 Then
                        MINORI_15 = MINORI_15 + 1
                    End If
                    If eta > 65 Then
                        MAGGIORI_65 = MAGGIORI_65 + 1
                    End If
                    If par.IfNull(row.Item("INDENNITA_ACC"), "") = "1" And par.IfNull(row.Item("PERC_INVAL"), 0) = 100 Then
                        N_INV_100_ACC = N_INV_100_ACC + 1
                    End If
                    If par.IfNull(row.Item("INDENNITA_ACC"), "") = "0" And par.IfNull(row.Item("PERC_INVAL"), 0) = 100 Then
                        N_INV_100_NO_ACC = N_INV_100_NO_ACC + 1
                    End If
                    If par.IfNull(row.Item("INDENNITA_ACC"), "") = "0" And par.IfNull(row.Item("PERC_INVAL"), 0) >= 66 And par.IfNull(row.Item("PERC_INVAL"), 0) < 100 Then
                        N_INV_100_66 = N_INV_100_66 + 1
                    End If

                    '22/06/2016 controllo cf componenti nucleo
                    If par.ControllaValiditaCF(par.IfNull(row.Item("COD_FISCALE"), ""), par.IfNull(row.Item("COGNOME"), ""), par.IfNull(row.Item("NOME"), ""), par.FormattaData(par.IfNull(row.Item("DATA_NASCITA"), ""))) = False Then
                        strErroriCF = strErroriCF & par.IfNull(row.Item("COD_FISCALE"), "") & "\n"
                    End If
                Next


                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataSource = dtComp
                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataBind()

                numComp.Value = dtComp.Rows.Count
            End If

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader11 = par.cmd.ExecuteReader()
            If myReader11.Read() Then
                txtCognome.Text = par.IfNull(myReader11("COGNOME"), "")
                txtNome.Text = par.IfNull(myReader11("NOME"), "")
                txtCF.Text = par.IfNull(myReader11("COD_FISCALE"), "")
                txtDataNascita.Text = par.FormattaData(par.IfNull(myReader11("DATA_NASCITA"), ""))
            End If
            myReader11.Close()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaNucleo() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub CaricaNucleoSpese()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_COMP_ELENCO_SPESE.ID,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_COMP_ELENCO_SPESE.IMPORTO,UTENZA_COMP_ELENCO_SPESE.DESCRIZIONE from UTENZA_COMP_ELENCO_SPESE,UTENZA_COMP_NUCLEO,UTENZA_DICHIARAZIONI WHERE " _
                & " UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE=UTENZA_DICHIARAZIONI.ID AND utenza_comp_nucleo.id_dichiarazione=" & lIdDichiarazione & " and utenza_comp_elenco_spese.id_componente= utenza_comp_nucleo.id order by utenza_comp_nucleo.progr asc "
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtCompSpese = New Data.DataTable
            da.Fill(dtCompSpese)
            da.Dispose()
            If dtCompSpese.Rows.Count > 0 Then
                CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataSource = dtCompSpese
                CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataBind()
            Else
                rowDT = dtCompSpese.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("COGNOME") = "&nbsp"
                rowDT.Item("NOME") = "&nbsp"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                dtCompSpese.Rows.Add(rowDT)
                CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataSource = dtCompSpese
                CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataBind()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaNucleoSpese() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub CaricaDocumenti()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_DOC_MANCANTE.DESCRIZIONE, UTENZA_DOC_MANCANTE.ID_DOC AS ID,UTENZA_DOC_NECESSARI.descrizione as ID_TIPO FROM UTENZA_DOC_MANCANTE,UTENZA_DOC_NECESSARI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND UTENZA_DOC_MANCANTE.id_DOC=UTENZA_DOC_NECESSARI.id ORDER BY UTENZA_DOC_MANCANTE.DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtDoc = New Data.DataTable
            Dim dtQuery As New Data.DataTable

            dtDoc.Columns.Add("id")
            dtDoc.Columns.Add("id_tipo")
            dtDoc.Columns.Add("DESCRIZIONE")

            da.Fill(dtQuery)
            da.Dispose()
            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtDoc.NewRow()
                    rowDT.Item("ID") = row.Item("ID")
                    rowDT.Item("id_tipo") = row.Item("ID_TIPO")
                    rowDT.Item("DESCRIZIONE") = Replace(row.Item("DESCRIZIONE"), vbCrLf, " ")
                    dtDoc.Rows.Add(rowDT)
                Next

                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataSource = dtDoc
                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataBind()
            Else
                rowDT = dtDoc.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("id_tipo") = "&nbsp"

                rowDT.Item("DESCRIZIONE") = "&nbsp"
                dtDoc.Rows.Add(rowDT)

                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataSource = dtDoc
                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataBind()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaDocumenti() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub CaricaDocumentiPresenti()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_DOC_PRESENTATA.DESCRIZIONE, UTENZA_DOC_PRESENTATA.ID_DOC AS ID,UTENZA_DOC_NECESSARI.descrizione as ID_TIPO,UTENZA_DOC_PRESENTATA.DATA_PRESENTAZIONE FROM UTENZA_DOC_PRESENTATA,UTENZA_DOC_NECESSARI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND UTENZA_DOC_PRESENTATA.id_DOC=UTENZA_DOC_NECESSARI.id ORDER BY UTENZA_DOC_PRESENTATA.DATA_PRESENTAZIONE DESC,UTENZA_DOC_PRESENTATA.DESCRIZIONE ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtDocPresenti = New Data.DataTable
            Dim dtQuery As New Data.DataTable

            dtDocPresenti.Columns.Add("id")
            dtDocPresenti.Columns.Add("id_tipo")
            dtDocPresenti.Columns.Add("DESCRIZIONE")
            dtDocPresenti.Columns.Add("DATA_PRESENTAZIONE")

            da.Fill(dtQuery)
            da.Dispose()
            If dtQuery.Rows.Count > 0 Then
                For Each row As Data.DataRow In dtQuery.Rows
                    rowDT = dtDocPresenti.NewRow()
                    rowDT.Item("ID") = par.IfNull(row.Item("ID"), 0)
                    rowDT.Item("id_tipo") = par.IfNull(row.Item("ID_TIPO"), 0)
                    rowDT.Item("DESCRIZIONE") = Replace(par.IfNull(row.Item("DESCRIZIONE"), ""), vbCrLf, " ")
                    rowDT.Item("DATA_PRESENTAZIONE") = Replace(par.FormattaData(par.IfNull(row.Item("DATA_PRESENTAZIONE"), "")), vbCrLf, " ")
                    dtDocPresenti.Rows.Add(rowDT)
                Next

                CType(dic_Documenti1.FindControl("DataGridPresenti"), DataGrid).DataSource = dtDocPresenti
                CType(dic_Documenti1.FindControl("DataGridPresenti"), DataGrid).DataBind()
            Else
                rowDT = dtDocPresenti.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("id_tipo") = "&nbsp"
                rowDT.Item("DESCRIZIONE") = "&nbsp"
                rowDT.Item("DATA_PRESENTAZIONE") = "&nbsp"
                dtDocPresenti.Rows.Add(rowDT)

                CType(dic_Documenti1.FindControl("DataGridPresenti"), DataGrid).DataSource = dtDocPresenti
                CType(dic_Documenti1.FindControl("DataGridPresenti"), DataGrid).DataBind()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaDocumentiPresentati() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub CaricaPatrimMob()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_COMP_PATR_MOB.PERC_PROPRIETA AS PROPRIETA,UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_COMP_PATR_MOB.ID AS IDMOB,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_COMP_PATR_MOB.*,TIPOLOGIA_PATR_MOB.descrizione as TIPO_MOB FROM TIPOLOGIA_PATR_MOB,UTENZA_COMP_PATR_MOB,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_PATR_MOB.ID_COMPONENTE=UTENZA_COMP_NUCLEO.ID and UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND UTENZA_COMP_PATR_MOB.ID_TIPO=TIPOLOGIA_PATR_MOB.id (+) order by UTENZA_COMP_PATR_MOB.id_componente asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtMob = New Data.DataTable

            da.Fill(dtMob)
            da.Dispose()
            If dtMob.Rows.Count > 0 Then
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataSource = dtMob
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataBind()
            Else
                rowDT = dtMob.NewRow()
                rowDT.Item("IDCOMP") = "-1"
                rowDT.Item("IDMOB") = "-1"

                rowDT.Item("COGNOME") = "&nbsp"
                dtMob.Rows.Add(rowDT)

                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataSource = dtMob
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataBind()
            End If


        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaPatrimMob() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Function CaricaPatrimImmob() As Boolean
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If
            Dim patrImmob As Boolean = False
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_COMP_PATR_IMMOB.ID AS IDIMMOB,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_COMP_PATR_IMMOB.*,TIPO_PIENA_PATR_IMMOB.descrizione as TIPO_PROPR,T_TIPO_PATR_IMMOB.DESCRIZIONE AS TIPO_IMMOB, DECODE(FL_VENDUTO,0,'NO',1,'SI') AS VENDUTO FROM tipo_piena_patr_immob,UTENZA_COMP_PATR_IMMOB,UTENZA_COMP_NUCLEO,T_TIPO_PATR_IMMOB WHERE T_TIPO_PATR_IMMOB.COD=UTENZA_COMP_PATR_IMMOB.ID_TIPO AND UTENZA_COMP_PATR_IMMOB.ID_COMPONENTE=UTENZA_COMP_NUCLEO.ID and UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND UTENZA_COMP_PATR_IMMOB.ID_TIPO_PROPRIETA=tipo_piena_patr_immob.id (+) order by UTENZA_COMP_PATR_IMMOB.id_componente asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtImmob = New Data.DataTable

            da.Fill(dtImmob)
            da.Dispose()
            If dtImmob.Rows.Count > 0 Then
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()
                patrImmob = True
            Else
                rowDT = dtImmob.NewRow()
                rowDT.Item("IDCOMP") = "-1"
                rowDT.Item("IDIMMOB") = "-1"

                rowDT.Item("COGNOME") = "&nbsp"
                dtImmob.Rows.Add(rowDT)

                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()
            End If

            Return patrImmob

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaPatrimImmob() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Function

    Public Sub CaricaRedditi()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If
            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDDITI.ID AS IDREDD,UTENZA_COMP_NUCLEO.*,UTENZA_REDDITI.*,(NVL(NON_IMPONIBILI,0)+ NVL(PENS_ESENTE,0)) AS PENSIONE2,NVL(AUTONOMO,0)+NVL(DOM_AG_FAB,0)+NVL(OCCASIONALI,0) AS AUTONOMO1,NVL(ONERI,0)+NVL(NO_ISEE,0) AS NO_ISEE,NVL(DIPENDENTE,0)+NVL(PENSIONE,0)+ NVL(AUTONOMO,0)+NVL(NON_IMPONIBILI,0)+  NVL(DOM_AG_FAB,0) + NVL(OCCASIONALI,0) + NVL(ONERI,0)+ NVL(PENS_ESENTE,0)+NVL(NO_ISEE,0) AS TOT_REDDITI FROM UTENZA_COMP_NUCLEO,UTENZA_REDDITI " _
            & "where UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE ORDER BY UTENZA_REDDITI.ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtRedd = New Data.DataTable
            da.Fill(dtRedd)
            da.Dispose()
            If dtRedd.Rows.Count > 0 Then
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
            Else
                rowDT = dtRedd.NewRow()
                rowDT.Item("IDCOMP") = "-1"
                rowDT.Item("ID") = "-1"
                rowDT.Item("COGNOME") = "&nbsp"
                rowDT.Item("NOME") = "&nbsp"
                dtRedd.Rows.Add(rowDT)
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
            End If

            Dim rowDT2 As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_COMP_DETRAZIONI.ID AS IDDETR,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_COMP_DETRAZIONI.IMPORTO,T_TIPO_DETRAZIONI.descrizione,UTENZA_COMP_DETRAZIONI.IMPORTO AS TOT_DETRAZ FROM T_TIPO_DETRAZIONI,UTENZA_COMP_DETRAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_DETRAZIONI.ID_COMPONENTE=UTENZA_COMP_NUCLEO.ID and UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND UTENZA_COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by UTENZA_comp_detrazioni.id_componente asc"
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtDetraz = New Data.DataTable
            da2.Fill(dtDetraz)
            da2.Dispose()
            If dtDetraz.Rows.Count > 0 Then
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
            Else
                rowDT2 = dtDetraz.NewRow()
                rowDT2.Item("IDCOMP") = "-1"
                rowDT2.Item("IDDETR") = "-1"
                rowDT2.Item("COGNOME") = "&nbsp"
                rowDT2.Item("NOME") = "&nbsp"
                rowDT2.Item("IMPORTO") = "0"
                rowDT2.Item("DESCRIZIONE") = "&nbsp"
                dtDetraz.Rows.Add(rowDT2)

                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaRedditi() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
        'Try
        '    If par.OracleConn.State = Data.ConnectionState.Closed Then
        '        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        '        par.SettaCommand(par)
        '    End If
        '    If IsNothing(par.myTrans) Then
        '        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '    End If

        '    Dim rowDT As System.Data.DataRow
        '    Dim dtapp As New Data.DataTable
        '    dtRedd = New Data.DataTable

        '    par.cmd.CommandText = "select '' as IDCOMP,'' as IDREDD,'' as COGNOME,'' as NOME,'' as COD_FISCALE,'' as DIP1,'' as DIP2,'' as DIP3,'' as DIP4,'' as DIP5,'' as DIP6 " _
        '        & ",'' as DIP7,'' as PENS1,'' as PENS2,'' as PENS3,'' as PENS4,'' as PENS5,'' as PENS6,'' as PENS_ES1,'' as PENS_ES2,'' as PENS_ES3,'' as PENS_ES4 " _
        '        & ",'' as AUTON1,'' as AUTON2,'' as AUTON3,'' as AUTON4,'' as AUTON5,'' as AUTON6,'' as AUTON7,'' as AUTON8,'' as AUTON9 " _
        '        & ",'' as NO_ISEE1,'' as NO_ISEE2,'' as TOT_REDDITI from dual"
        '    Dim daB1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    daB1.Fill(dtapp)
        '    daB1.Dispose()

        '    dtRedd = dtapp.Clone

        '    par.cmd.CommandText = "select * from utenza_comp_nucleo where id_dichiarazione=" & lIdDichiarazione
        '    Dim da1 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    Dim dt1 As New Data.DataTable
        '    da1.Fill(dt1)
        '    da1.Dispose()
        '    Dim totReddito As Decimal = 0
        '    If dt1.Rows.Count > 0 Then
        '        For Each row1 As Data.DataRow In dt1.Rows
        '            totReddito = 0
        '            rowDT = dtRedd.NewRow()
        '            rowDT.Item("COGNOME") = par.IfNull(row1.Item("COGNOME"), 0)
        '            rowDT.Item("NOME") = par.IfNull(row1.Item("NOME"), 0)
        '            rowDT.Item("COD_FISCALE") = par.IfNull(row1.Item("COD_FISCALE"), 0)
        '            par.cmd.CommandText = "select UTENZA_REDDITI_DIPENDENTE.id as idt,UTENZA_COMP_NUCLEO.*,UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDD_DIPEND_IMPORTI.ID_REDD_TOT as IDREDD,UTENZA_REDDITI_DIPENDENTE.*,UTENZA_REDD_DIPEND_IMPORTI.ID AS IDIMPORTI,UTENZA_REDD_DIPEND_IMPORTI.NUM_GG,UTENZA_REDD_DIPEND_IMPORTI.IMPORTO from UTENZA_REDDITI_DIPENDENTE, " _
        '                & " UTENZA_COMP_NUCLEO,UTENZA_REDD_DIPEND_IMPORTI,UTENZA_REDDITI where UTENZA_REDDITI_DIPENDENTE.id=UTENZA_REDD_DIPEND_IMPORTI.ID_REDD_DIPENDENTE(+) AND UTENZA_REDDITI.ID=UTENZA_REDD_DIPEND_IMPORTI.ID_REDD_TOT " _
        '                & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE AND UTENZA_COMP_NUCLEO.id = " & row1.Item("id")
        '            Dim da00 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dtQuery00 As New Data.DataTable

        '            da00.Fill(dtQuery00)
        '            da00.Dispose()

        '            If dtQuery00.Rows.Count > 0 Then
        '                For Each row As Data.DataRow In dtQuery00.Rows
        '                    rowDT.Item("IDREDD") = par.IfNull(row.Item("IDREDD"), 0)
        '                    rowDT.Item("IDCOMP") = par.IfNull(row.Item("IDCOMP"), 0)

        '                    Select Case par.IfNull(row.Item("idt"), 0)
        '                        Case "1"
        '                            rowDT.Item("DIP5") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "2"
        '                            rowDT.Item("DIP2") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "3"
        '                            rowDT.Item("DIP6") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "4"
        '                            rowDT.Item("DIP3") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "5"
        '                            rowDT.Item("DIP7") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "6"
        '                            rowDT.Item("DIP1") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "7"
        '                            rowDT.Item("DIP4") = par.IfNull(row.Item("IMPORTO"), 0)
        '                    End Select
        '                    totReddito = totReddito + par.IfNull(row.Item("IMPORTO"), 0)
        '                Next

        '            End If

        '            par.cmd.CommandText = "select UTENZA_REDDITI_AUTONOMO.id as idt,UTENZA_COMP_NUCLEO.*,UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT as IDREDD,UTENZA_REDDITI_AUTONOMO.*,UTENZA_REDD_AUTONOMO_IMPORTI.ID AS IDIMPORTI,UTENZA_REDD_AUTONOMO_IMPORTI.NUM_GG,UTENZA_REDD_AUTONOMO_IMPORTI.IMPORTO from UTENZA_REDDITI_AUTONOMO, " _
        '                & " UTENZA_COMP_NUCLEO,UTENZA_REDD_AUTONOMO_IMPORTI,UTENZA_REDDITI where UTENZA_REDDITI_AUTONOMO.id=UTENZA_REDD_AUTONOMO_IMPORTI.ID_REDD_AUTONOMO(+) AND UTENZA_REDDITI.ID=UTENZA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT " _
        '                & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE AND UTENZA_COMP_NUCLEO.id = " & row1.Item("id")
        '            Dim da01 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dtQuery01 As New Data.DataTable

        '            da01.Fill(dtQuery01)
        '            da01.Dispose()

        '            If dtQuery01.Rows.Count > 0 Then
        '                For Each row As Data.DataRow In dtQuery01.Rows
        '                    rowDT.Item("IDREDD") = par.IfNull(row.Item("IDREDD"), 0)
        '                    rowDT.Item("IDCOMP") = par.IfNull(row.Item("IDCOMP"), 0)
        '                    Select Case par.IfNull(row.Item("idt"), 0)
        '                        Case "1"
        '                            rowDT.Item("AUTON2") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "2"
        '                            rowDT.Item("AUTON8") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "3"
        '                            rowDT.Item("AUTON7") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "4"
        '                            rowDT.Item("AUTON4") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "6"
        '                            rowDT.Item("AUTON6") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "7"
        '                            rowDT.Item("AUTON3") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "8"
        '                            rowDT.Item("AUTON9") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "9"
        '                            rowDT.Item("AUTON5") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "10"
        '                            rowDT.Item("AUTON1") = par.IfNull(row.Item("IMPORTO"), 0)
        '                    End Select
        '                    totReddito = totReddito + par.IfNull(row.Item("IMPORTO"), 0)
        '                Next

        '            End If


        '            par.cmd.CommandText = "select UTENZA_REDDITI_PENSIONE.id as idt,UTENZA_COMP_NUCLEO.*,UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDD_PENS_IMPORTI.ID_REDD_TOT as IDREDD,UTENZA_REDDITI_PENSIONE.*,UTENZA_REDD_PENS_IMPORTI.ID AS IDIMPORTI,UTENZA_REDD_PENS_IMPORTI.NUM_GG,UTENZA_REDD_PENS_IMPORTI.IMPORTO from UTENZA_REDDITI_PENSIONE, " _
        '                & " UTENZA_COMP_NUCLEO,UTENZA_REDD_PENS_IMPORTI,UTENZA_REDDITI where UTENZA_REDDITI_PENSIONE.id=UTENZA_REDD_PENS_IMPORTI.ID_REDD_PENSIONE(+) AND UTENZA_REDDITI.ID=UTENZA_REDD_PENS_IMPORTI.ID_REDD_TOT " _
        '                & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE AND UTENZA_COMP_NUCLEO.id = " & row1.Item("id")
        '            Dim da02 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dtQuery02 As New Data.DataTable

        '            da02.Fill(dtQuery02)
        '            da02.Dispose()

        '            If dtQuery02.Rows.Count > 0 Then
        '                For Each row As Data.DataRow In dtQuery02.Rows
        '                    rowDT.Item("IDREDD") = par.IfNull(row.Item("IDREDD"), 0)
        '                    rowDT.Item("IDCOMP") = par.IfNull(row.Item("IDCOMP"), 0)
        '                    Select Case par.IfNull(row.Item("idt"), 0)
        '                        Case "1"
        '                            rowDT.Item("PENS2") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "2"
        '                            rowDT.Item("PENS3") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "3"
        '                            rowDT.Item("PENS4") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "4"
        '                            rowDT.Item("PENS6") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "5"
        '                            rowDT.Item("PENS5") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "6"
        '                            rowDT.Item("PENS1") = par.IfNull(row.Item("IMPORTO"), 0)
        '                    End Select
        '                    totReddito = totReddito + par.IfNull(row.Item("IMPORTO"), 0)
        '                Next
        '            End If


        '            par.cmd.CommandText = "select UTENZA_REDDITI_PENS_ESENTI.id as idt,UTENZA_COMP_NUCLEO.*,UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT as IDREDD,UTENZA_REDDITI_PENS_ESENTI.*,UTENZA_REDD_PENS_ES_IMPORTI.ID AS IDIMPORTI,UTENZA_REDD_PENS_ES_IMPORTI.NUM_GG,UTENZA_REDD_PENS_ES_IMPORTI.IMPORTO from UTENZA_REDDITI_PENS_ESENTI, " _
        '                & " UTENZA_COMP_NUCLEO,UTENZA_REDD_PENS_ES_IMPORTI,UTENZA_REDDITI where UTENZA_REDDITI_PENS_ESENTI.id=UTENZA_REDD_PENS_ES_IMPORTI.ID_REDD_PENS_ESENTI(+) AND UTENZA_REDDITI.ID=UTENZA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT " _
        '                & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE AND UTENZA_COMP_NUCLEO.id = " & row1.Item("id")
        '            Dim da03 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dtQuery03 As New Data.DataTable

        '            da03.Fill(dtQuery03)
        '            da03.Dispose()

        '            If dtQuery03.Rows.Count > 0 Then
        '                For Each row As Data.DataRow In dtQuery03.Rows
        '                    rowDT.Item("IDREDD") = par.IfNull(row.Item("IDREDD"), 0)
        '                    rowDT.Item("IDCOMP") = par.IfNull(row.Item("IDCOMP"), 0)
        '                    Select Case par.IfNull(row.Item("idt"), 0)
        '                        Case "1"
        '                            rowDT.Item("PENS_ES3") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "2"
        '                            rowDT.Item("PENS_ES2") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "3"
        '                            rowDT.Item("PENS_ES1") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "4"
        '                            rowDT.Item("PENS_ES4") = par.IfNull(row.Item("IMPORTO"), 0)
        '                    End Select
        '                    totReddito = totReddito + par.IfNull(row.Item("IMPORTO"), 0)
        '                Next
        '            End If

        '            par.cmd.CommandText = "select UTENZA_REDDITI_NO_ISEE.id as idt,UTENZA_COMP_NUCLEO.*,UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT as IDREDD,UTENZA_REDDITI_NO_ISEE.*,UTENZA_REDD_NO_ISEE_IMPORTI.ID AS IDIMPORTI,UTENZA_REDD_NO_ISEE_IMPORTI.NUM_GG,UTENZA_REDD_NO_ISEE_IMPORTI.IMPORTO from UTENZA_REDDITI_NO_ISEE, " _
        '                & " UTENZA_COMP_NUCLEO,UTENZA_REDD_NO_ISEE_IMPORTI,UTENZA_REDDITI where UTENZA_REDDITI_NO_ISEE.id=UTENZA_REDD_NO_ISEE_IMPORTI.ID_REDD_NO_ISEE(+) AND UTENZA_REDDITI.ID=UTENZA_REDD_NO_ISEE_IMPORTI.ID_REDD_TOT " _
        '                & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE AND UTENZA_COMP_NUCLEO.id = " & row1.Item("id")
        '            Dim da04 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '            Dim dtQuery04 As New Data.DataTable

        '            da04.Fill(dtQuery04)
        '            da04.Dispose()

        '            If dtQuery04.Rows.Count > 0 Then
        '                For Each row As Data.DataRow In dtQuery04.Rows
        '                    rowDT.Item("IDREDD") = par.IfNull(row.Item("IDREDD"), 0)
        '                    rowDT.Item("IDCOMP") = par.IfNull(row.Item("IDCOMP"), 0)
        '                    Select Case par.IfNull(row.Item("idt"), 0)
        '                        Case "1"
        '                            rowDT.Item("NO_ISEE2") = par.IfNull(row.Item("IMPORTO"), 0)
        '                        Case "2"
        '                            rowDT.Item("NO_ISEE1") = par.IfNull(row.Item("IMPORTO"), 0)
        '                    End Select
        '                    totReddito = totReddito + par.IfNull(row.Item("IMPORTO"), 0)
        '                Next
        '            End If


        '            rowDT.Item("TOT_REDDITI") = totReddito
        '            If totReddito > 0 Then
        '                dtRedd.Rows.Add(rowDT)
        '            End If
        '        Next
        '    End If


        '    If dtRedd.Rows.Count > 0 Then
        '        CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
        '        CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
        '    Else
        '        rowDT = dtRedd.NewRow()
        '        rowDT.Item("IDCOMP") = "-1"
        '        rowDT.Item("COGNOME") = "&nbsp"
        '        rowDT.Item("NOME") = "&nbsp"
        '        dtRedd.Rows.Add(rowDT)
        '        CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
        '        CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
        '    End If


        '    'par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_REDDITI.ID AS IDREDD,UTENZA_COMP_NUCLEO.*,UTENZA_REDDITI.*,(NVL(NON_IMPONIBILI,0)+ NVL(PENS_ESENTE,0)) AS PENSIONE2,NVL(AUTONOMO,0)+NVL(DOM_AG_FAB,0)+NVL(OCCASIONALI,0) AS AUTONOMO1,NVL(ONERI,0)+NVL(NO_ISEE,0) AS NO_ISEE,NVL(DIPENDENTE,0)+NVL(PENSIONE,0)+ NVL(AUTONOMO,0)+NVL(NON_IMPONIBILI,0)+  NVL(DOM_AG_FAB,0) + NVL(OCCASIONALI,0) + NVL(ONERI,0)+ NVL(PENS_ESENTE,0)+NVL(NO_ISEE,0) AS TOT_REDDITI FROM UTENZA_COMP_NUCLEO,UTENZA_REDDITI " _
        '    '& "where UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND UTENZA_COMP_NUCLEO.ID=UTENZA_REDDITI.ID_COMPONENTE ORDER BY UTENZA_REDDITI.ID ASC"
        '    'Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    'dtRedd = New Data.DataTable
        '    'da.Fill(dtRedd)
        '    'da.Dispose()
        '    'If dtRedd.Rows.Count > 0 Then
        '    '    CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
        '    '    CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
        '    'Else
        '    '    rowDT = dtRedd.NewRow()
        '    '    rowDT.Item("IDCOMP") = "-1"
        '    '    rowDT.Item("ID") = "-1"
        '    '    rowDT.Item("COGNOME") = "&nbsp"
        '    '    rowDT.Item("NOME") = "&nbsp"
        '    '    dtRedd.Rows.Add(rowDT)
        '    '    CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
        '    '    CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
        '    'End If

        '    Dim rowDT2 As System.Data.DataRow
        '    par.cmd.CommandText = "SELECT UTENZA_COMP_NUCLEO.ID AS IDCOMP,UTENZA_COMP_DETRAZIONI.ID AS IDDETR,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME,UTENZA_COMP_DETRAZIONI.IMPORTO,T_TIPO_DETRAZIONI.descrizione,UTENZA_COMP_DETRAZIONI.IMPORTO AS TOT_DETRAZ FROM T_TIPO_DETRAZIONI,UTENZA_COMP_DETRAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_COMP_DETRAZIONI.ID_COMPONENTE=UTENZA_COMP_NUCLEO.ID and UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND UTENZA_COMP_DETRAZIONI.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by UTENZA_comp_detrazioni.id_componente asc"
        '    Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        '    dtDetraz = New Data.DataTable
        '    da2.Fill(dtDetraz)
        '    da2.Dispose()
        '    If dtDetraz.Rows.Count > 0 Then
        '        CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
        '        CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
        '    Else
        '        rowDT2 = dtDetraz.NewRow()
        '        rowDT2.Item("IDCOMP") = "-1"
        '        rowDT2.Item("IDDETR") = "-1"
        '        rowDT2.Item("COGNOME") = "&nbsp"
        '        rowDT2.Item("NOME") = "&nbsp"
        '        rowDT2.Item("IMPORTO") = "0"
        '        rowDT2.Item("DESCRIZIONE") = "&nbsp"
        '        dtDetraz.Rows.Add(rowDT2)
        '        CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
        '        CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
        '    End If

        'Catch ex As Exception
        '    par.myTrans.Rollback()
        '    par.myTrans = par.OracleConn.BeginTransaction()
        '    HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
        '    Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaRedditi() - " & ex.Message)
        '    Response.Redirect("../Errore.aspx", False)
        'End Try
    End Sub

    Private Sub CaricaDatiISEE()
        Try
            par.cmd.CommandText = "select * from INFO_ISEE_AU where id_istanza=" & lIdDichiarazione
            Dim myReaderIse As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIse.Read Then
                'CType(Tab_ISEE1.FindControl("textISE"), TextBox).Text = Format(par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISE").ToString.Replace(".", ""), 0))), "0.00")
                'CType(Tab_ISEE1.FindControl("textISEE"), TextBox).Text = Format(par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISEE").ToString.Replace(".", ""), 0))), "0.00")
                'CType(Tab_ISEE1.FindControl("textISP"), TextBox).Text = Format(par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISP").ToString.Replace(".", ""), 0))), "0.00")
                'CType(Tab_ISEE1.FindControl("textISR"), TextBox).Text = Format(par.VirgoleInPunti(CStr(par.IfNull(myReaderIse("ISR").ToString.Replace(".", ""), 0))), "0.00")
                CType(Tab_ISEE1.FindControl("textISE"), TextBox).Text = Format((CDbl(par.IfNull(myReaderIse("ISE"), 0))), "##,##0.000")
                CType(Tab_ISEE1.FindControl("textISEE"), TextBox).Text = Format((CDbl(par.IfNull(myReaderIse("ISEE"), 0))), "##,##0.00")
                CType(Tab_ISEE1.FindControl("textISP"), TextBox).Text = Format((CDbl(par.IfNull(myReaderIse("ISP"), 0))), "##,##0.00")
                CType(Tab_ISEE1.FindControl("textISR"), TextBox).Text = Format((CDbl(par.IfNull(myReaderIse("ISR"), 0))), "##,##0.00")
                CType(Tab_ISEE1.FindControl("txtProtocolloDSU"), TextBox).Text = par.IfNull(myReaderIse("NUM_DSU"), "")
            End If
            myReaderIse.Close()
        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaDatiISEE() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
       
    End Sub

    Private Sub CaricaDiffide()
        Try
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT UTENZA_FILE_DIFFIDE.ID,PROTOCOLLO,DESCRIZIONE AS TIPO1,TO_CHAR(TO_DATE(SUBSTR(DATA_CREAZIONE, 1, 8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_CREAZIONE,TO_CHAR(TO_DATE(DATA_ANNULLO, 'yyyymmdd'), 'dd/mm/yyyy') AS DATA_ANNULLO,NOTE,(CASE WHEN UTENZA_FILE_DIFFIDE.NOME_FILE is not null THEN replace(replace('<a href=£javascript:void(0)£ onclick=£window.open(''../ALLEGATI/ANAGRAFE_UTENZA/DIFFIDE/'||UTENZA_FILE_DIFFIDE.NOME_FILE||''',''Dettagli'','''');£>Visualizza File</a>','$','&'),'£','" & Chr(34) & "') ELSE '' END) as Visualizza from UTENZA_FILE_DIFFIDE,UTENZA_FILE_DIFFIDE_DETT,UTENZA_TIPO_DIFFIDE where UTENZA_TIPO_DIFFIDE.ID=UTENZA_FILE_DIFFIDE.TIPO AND UTENZA_FILE_DIFFIDE.id=UTENZA_FILE_DIFFIDE_DETT.ID_FILE_DIFFIDE and UTENZA_FILE_DIFFIDE_DETT.id_dichiarazione=" & lIdDichiarazione
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            Dim dt2 As New Data.DataTable
            da.Fill(dt2)
            da.Dispose()
            If dt2.Rows.Count > 0 Then
                imgDiffide.Visible = True
                CType(Tab_Diffide1.FindControl("DataGridDiffide"), DataGrid).DataSource = dt2
                CType(Tab_Diffide1.FindControl("DataGridDiffide"), DataGrid).DataBind()
            Else
                rowDT = dt2.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("PROTOCOLLO") = "&nbsp"
                rowDT.Item("DATA_CREAZIONE") = "&nbsp"
                'rowDT.Item("IMPORTO") = "&nbsp"
                rowDT.Item("DATA_ANNULLO") = "&nbsp"
                dt2.Rows.Add(rowDT)

                CType(Tab_Diffide1.FindControl("DataGridDiffide"), DataGrid).DataSource = dt2
                CType(Tab_Diffide1.FindControl("DataGridDiffide"), DataGrid).DataBind()
            End If
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaDiffide() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Sub VisualizzaDichiarazione()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long


        Dim DESCRIZIONE As String = ""
        Dim CodiceUnita As String = ""

        Dim canoneApplicato As Boolean = False
        Dim idContratto As Long = 0
        Dim codTipoContr As String = ""


        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE" & vIdConnessione, par.OracleConn)

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            hiddenLockCorrenti.Value = "ANAGRAFE_UTENZA" & lIdDichiarazione
            Dim risultato = par.EseguiLock(PageID.Value, hiddenLockCorrenti.Value)
            Select Case risultato.esito
                Case SepacomLock.EsitoLockUnlock.Success
                    AprisolaLettura = 0
                Case SepacomLock.EsitoLockUnlock.InUso
                    AprisolaLettura = 1
                Case Else
                    ' ''Beep()
            End Select

            par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ART_15,UTENZA_DICHIARAZIONI.RAPPORTO,UTENZA_BANDI.DATA_INIZIO,UTENZA_BANDI.STATO,UTENZA_DICHIARAZIONI.ID_BANDO,utenza_dichiarazioni.DATA_PG,UTENZA_DICHIARAZIONI.FL_SOSPENSIONE FROM UTENZA_BANDI,UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID=" & lIdDichiarazione & " AND UTENZA_DICHIARAZIONI.ID_BANDO=UTENZA_BANDI.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                par.cmd.CommandText = "SELECT INDIRIZZI.DESCRIZIONE,INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.LOCALITA,RAPPORTI_UTENZA.ID,RAPPORTI_UTENZA.COD_TIPOLOGIA_CONTR_LOC,RAPPORTI_UTENZA.PROVENIENZA_ASS FROM SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI WHERE INDIRIZZI.ID(+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.COD_CONTRATTO='" & par.IfNull(myReader("RAPPORTO"), "") & "'"
                Dim myReaderID As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderID.Read Then
                    idContratto = par.IfNull(myReaderID("ID"), "")
                    codTipoContr = par.IfNull(myReaderID("COD_TIPOLOGIA_CONTR_LOC"), "")
                    IndiceContratto = par.IfNull(myReaderID("ID"), "")
                    If par.IfNull(myReaderID("PROVENIENZA_ASS"), "") = "10" Then
                        FFOO = True
                    Else
                        FFOO = False
                    End If
                    sIndirizzoUI.Value = par.Cripta(par.IfNull(myReaderID("DESCRIZIONE"), "") & " " & par.IfNull(myReaderID("CIVICO"), "") & " " & par.IfNull(myReaderID("CAP"), "") & " " & par.IfNull(myReaderID("LOCALITA"), ""))
                End If
                myReaderID.Close()



                If codTipoContr = "EQC392" Then
                    RU392.Value = "1"
                Else
                    RU392.Value = "0"
                End If
                If codTipoContr = "L43198" Then
                    RU431.Value = "1"
                Else
                    RU431.Value = "0"
                End If

                lblAvviso393.Visible = False
                lblAvviso394.Visible = False
                If RU392.Value = "1" Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=40"
                    Dim myReaderAU392 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderAU392.Read Then
                        lblAvviso393.Visible = True
                        lblAvviso393.Text = "Non prima del "
                        lblAvviso394.Visible = True
                        lblAvviso394.Text = par.FormattaData(myReaderAU392("valore"))
                    End If
                    myReaderAU392.Close()
                End If

                If RU392.Value = "0" And RU431.Value = "0" Then
                    If Request.QueryString("CR") = "1" And (myReader("art_15") = "1" Or Request.QueryString("ASST") = "1") Then

                    Else
                        'Forzatura manuale come da richiesta del 27/05/2014 (Oggetto: I: Pedercini Giacomina Andreoli Emanuele)
                        If lIdDichiarazione <> "97747" And lIdDichiarazione <> "90452" Then
                            If myReader("STATO") <> 1 Then
                                'If par.IfNull(myReader("FL_SOSPENSIONE"), "0") = "0" Then
                                btnSalva.Visible = False
                                imgStampa.Visible = False
                                SoloLettura = "1"
                                cmbStato.Enabled = False
                                cmbAnnoReddituale.Enabled = False
                                DatiSoloLettura()
                                nonstampare.Value = "1"
                                MessJQuery("Non è possibile apportare modifiche!", 0, "Attenzione")
                                'End If
                            End If

                            '15/05/2013 AGGIUNTO CONTROLLO PER BLOCCARE LE DICHIARAZIONI PER CUI IL CANONE NON RISULTA APPLICATO (non presente in canoni_ec)

                            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONI_EC WHERE ID_BANDO_AU=" & par.IfNull(myReader("ID_BANDO"), 0) & " AND TIPO_PROVENIENZA<>1 AND ID_CONTRATTO=" & idContratto
                            Dim myReaderAU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderAU.Read Then
                                canoneApplicato = True
                            Else
                                canoneApplicato = False
                            End If
                            myReaderAU.Close()

                            If myReader("STATO") = "1" Or myReader("STATO") = "2" Then
                                If canoneApplicato = True Then
                                    btnSalva.Visible = False
                                    imgStampa.Visible = False
                                    cmbStato.Enabled = False
                                    cmbAnnoReddituale.Enabled = False
                                    SoloLettura = "1"
                                    DatiSoloLettura()
                                    nonstampare.Value = "1"
                                    MessJQuery("Non è possibile apportare modifiche! Utilizzare la funzione di INSERIMENTO DOMANDA DI VERIFICA AU.", 0, "Attenzione")
                                    DatiSoloLettura()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            myReader.Close()

            '02/07/2012 per i contratti presenti in RAPPORTI_UTENZA_AU_ABUSIVI
            If Request.QueryString("CR") = "1" Then
                cmbAnnoReddituale.Enabled = False
            End If
            If SoloLettura = "1" Then
                btnSalva.Visible = False
                imgStampa.Visible = False
                nonstampare.Value = "1"
            End If

            txtDataPG.Text = Format(Now, "dd/MM/yyyy")
            Dim lsiFrutto As New ListItem("DA COMPLETARE", "0")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("COMPLETA", "1")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("DA CANCELLARE", "2")
            cmbStato.Items.Add(lsiFrutto)

            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneNas, "ID", "NOME", True)
            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneRes, "ID", "NOME", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrNas, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrRes, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", cmbTipoIRes, "COD", "DESCRIZIONE", True)

            par.caricaComboBox("SELECT DESCRIZIONE,COD FROM SISCOM_MI.TIPOLOGIA_CONTRATTO_LOCAZIONE ORDER BY COD ASC", Tab_InfoContratto1.FindControl("cmbTipoRU"), "COD", "DESCRIZIONE", True)


            ''***** 16/07/2014 COMMENTO TALE CONTROLLO A CAUSA DEL QUALE NON VIENE MOSTRATO L'ALERT PER LA STAMPA DELLA DICHIARAZIONE DOPO DELLE MODIFICHE
            'par.cmd.CommandText = "SELECT COUNT(ID_PRATICA) FROM UTENZA_EVENTI_DICHIARAZIONI WHERE COD_EVENTO='F132' AND ID_PRATICA=" & lIdDichiarazione
            'myReader = par.cmd.ExecuteReader()
            'If myReader.Read Then
            '    If myReader(0) > 0 Then
            '        Session.Item("STAMPATO") = "1"
            '    Else
            '        Session.Item("STAMPATO") = "0"
            '    End If
            'Else
            '    Session.Item("STAMPATO") = "0"
            'End If
            'myReader.Close()
            ''***** 16/07/2014 FINE COMMENTO TALE CONTROLLO A CAUSA DEL QUALE NON VIENE MOSTRATO L'ALERT PER LA STAMPA DELLA DICHIARAZIONE DOPO DELLE MODIFICHE

RIPETI_LETTURA:
            If AprisolaLettura = 0 Then
                'par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            Else
                btnSalva.Visible = False
                imgStampa.Visible = False
                cmbStato.Enabled = False
                cmbAnnoReddituale.Enabled = False
                nonstampare.Value = "1"
                SoloLettura = "1"
                DatiSoloLettura()
                MessJQuery("Pratica aperta da un altro utente. Non è possibile effettuare modifiche!", 0, "Attenzione")
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            End If
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then

                RiempiCmbAnniRedd(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))

                par.cmd.CommandText = "select UNITA_IMMOBILIARI.COD_UNITA_IMMOBILIARE,UNITA_IMMOBILIARI.ID from SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,siscom_mi.rapporti_utenza where UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND cod_contratto='" & par.IfNull(myReader("RAPPORTO"), "0") & "'"
                Dim myReaderUN As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderUN.Read Then
                    CodiceUnita = par.IfNull(myReaderUN("COD_UNITA_IMMOBILIARE"), "-1")
                    lIdUnita.Value = par.IfNull(myReaderUN("ID"), "-1")
                End If
                myReaderUN.Close()


                If par.IfNull(myReader("ART_15"), "0") = "1" Then
                    lblArt15.Visible = True
                End If
                lIndice_Bando = myReader("ID_BANDO")
                Session.Add("idBandoAU", lIndice_Bando)
                lblPG.Text = par.IfNull(myReader("pg"), "")
                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                lIdDichImport = par.IfNull(myReader("ID_DICH_IMPORT"), 0)

                CType(Tab_InfoContratto1.FindControl("txtDataCessazione"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_cessazione"), ""))
                CType(Tab_InfoContratto1.FindControl("txtDataDec"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data_decorrenza"), ""))

                cmbAnnoReddituale.SelectedValue = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                lotto45.Value = "0"
                If par.IfNull(myReader("FL_4_5_LOTTO"), 0) = 1 Then
                    lbl45_Lotto.Visible = True
                    txtCodConvocazione.Text = par.IfNull(myReader("COD_CONVOCAZIONE"), "")
                    txtCodConvocazione.Visible = True
                    lotto45.Value = "1"
                Else
                    txtPosizione.Enabled = False
                End If

                If cmbAnnoReddituale.SelectedValue = "2006" Then
                    imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;return confirm('ATTENZIONE..Stai elaborando utilizzando il 2006 come anno fiscale, SENZA TENERE CONTO di quanto previsto dalla LG 36/2008. Proseguire?');")
                    cmbAnnoReddituale.Items.FindByValue("2006").Selected = True
                    txt36.Value = "0"
                Else
                    cmbAnnoReddituale.SelectedIndex = -1
                    If par.IfNull(myReader("fl_392"), "0") = "1" Then
                        cmbAnnoReddituale.Enabled = True
                    Else
                        cmbAnnoReddituale.Enabled = False
                    End If
                    '
                    Select Case cmbAnnoReddituale.SelectedValue
                        Case "2007"
                            rdApplica.Visible = True
                            rdNoApplica.Visible = True
                            lblApplica36.Visible = True
                            imgStampa.Attributes.Add("onclick", "javascript:Uscita=1;if (document.getElementById('txt36').value==0) {return confirm('ATTENZIONE..Stai elaborando utilizzando il 2007 come anno fiscale, SENZA TENERE CONTO di quanto previsto dalla LG 36/2008. Proseguire?');} else {return confirm('ATTENZIONE..Stai elaborando utilizzando il 2007 come anno fiscale, TENENDO CONTO di quanto previsto dalla LG 36/2008. Proseguire?');}")
                            If par.IfNull(myReader("FL_APPLICA_36"), "0") = "0" Then
                                rdNoApplica.Checked = True
                                rdApplica.Checked = False
                            Else
                                rdApplica.Checked = True
                                rdNoApplica.Checked = False
                            End If
                        Case Else
                            rdApplica.Visible = True
                            rdNoApplica.Visible = True
                            lblApplica36.Visible = True
                            rdApplica.Checked = True
                            rdNoApplica.Checked = False
                            rdNoApplica.Enabled = False
                            lblApplica36.Enabled = False
                    End Select
                End If
                If cmbAnnoReddituale.SelectedValue = "2010" Then
                    cmbAnnoReddituale.Enabled = False
                End If
                annoRedd = cmbAnnoReddituale.SelectedItem.Value
                CType(Tab_InfoContratto1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE_WEB"), "")
                cmbTipoDocumento.SelectedIndex = -1
                cmbTipoDocumento.Items.FindByValue(par.IfNull(myReader("TIPO_DOCUMENTO"), 0)).Selected = True
                txtCINum.Text = par.IfNull(myReader("CARTA_I"), "")
                txtCIData.Text = par.FormattaData(par.IfNull(myReader("CARTA_I_DATA"), ""))
                txtCSData.Text = par.FormattaData(par.IfNull(myReader("CARTA_SOGG_DATA"), ""))
                txtPSScade.Text = par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_SCADE"), ""))
                txtPSRinnovo.Text = par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_RINNOVO"), ""))
                txtPSData.Text = par.FormattaData(par.IfNull(myReader("PERMESSO_SOGG_DATA"), ""))
                txtCIRilascio.Text = par.IfNull(myReader("CARTA_I_RILASCIATA"), "")
                txtPSNum.Text = par.IfNull(myReader("PERMESSO_SOGG_N"), "")
                txtCSNum.Text = par.IfNull(myReader("CARTA_SOGG_N"), "")
                If par.IfNull(myReader("FL_NATO_ESTERO"), "0") = "1" Then
                    ChNatoEstero.Checked = True
                End If

                If par.IfNull(myReader("FL_CITTADINO_IT"), "0") = "1" Then
                    ChCittadinanza.Checked = True
                End If

                txtData392.Text = par.FormattaData(par.IfNull(myReader("DATA_DISDETTA_392"), ""))

                cmbLavorativa.SelectedIndex = -1
                cmbLavorativa.Items.FindByValue(par.IfNull(myReader("fl_attivita_lav"), 0)).Selected = True

                txtTelefono.Text = par.IfNull(myReader("N_TELEFONO_2"), "")
                txtEmail.Text = par.IfNull(myReader("EMAIL"), "")
                txtIndirizzo.Text = par.IfNull(myReader("INDIRIZZO_SPEDIZIONE"), "")
                txtCapSpediz.Text = par.IfNull(myReader("CAP_SPEDIZ"), "")
                txtCivicoSpediz.Text = par.IfNull(myReader("CIVICO_SPEDIZIONE"), "")
                CType(dic_Documenti1.FindControl("txtDataPresentaz"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_PRES_DOCUMENTAZ"), ""))
                txtPressoIndirizzo.Text = par.IfNull(myReader("PRESSO_COMUNICAZIONI"), "")
                If par.IfNull(myReader("FL_COMUNICAZIONI"), 0) = 1 Then
                    chkRicevePresso.Checked = True
                Else
                    chkRicevePresso.Checked = False
                End If

                '''''''''''''''carica dati spedizione



                par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrSpediz, "SIGLA", "SIGLA", True)
                par.caricaComboBox("SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", cmbTipoISped, "COD", "DESCRIZIONE", True)
                par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI ORDER BY NOME ASC", cmbComuneSpediz, "ID", "NOME", True)

                cmbTipoISped.SelectedValue = par.IfNull(myReader("ID_TIPO_IND_SPEDIZ"), "-1")


                If par.IfNull(myReader("ID_LUOGO_SPEDIZ"), -1) <> -1 Then

                    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & myReader("ID_LUOGO_SPEDIZ")
                    Dim myReaderj As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderj.Read() Then
                        cmbPrSpediz.Items.FindByText(myReaderj("SIGLA")).Selected = True
                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReaderj("SIGLA") & "' ORDER BY NOME ASC", cmbComuneSpediz, "ID", "NOME", True)
                        cmbComuneSpediz.Items.FindByText(myReaderj("NOME")).Selected = True
                    End If
                    myReaderj.Close()
                End If
                cT = txtCAPRes
                cT.Text = par.IfNull(myReader("CAP_RES_DNTE"), "")

                cT = txtIndRes
                cT.Text = par.IfNull(myReader("IND_RES_DNTE"), "")

                cT = txtCivicoRes
                cT.Text = par.IfNull(myReader("CIVICO_RES_DNTE"), "")

                cT = txtTelRes
                cT.Text = par.IfNull(myReader("TELEFONO_DNTE"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS_DNTE")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES_DNTE")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND_RES_DNTE")

                CType(Dic_Reddito1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader("minori_carico"), "0")
                txtFoglio.Text = par.IfNull(myReader("FOGLIO"), "")
                txtMappale.Text = par.IfNull(myReader("MAPPALE"), "")
                txtSub.Text = par.IfNull(myReader("SUB"), "")

                txtScala.Text = par.IfNull(myReader("scala"), "")
                txtPiano.Text = par.IfNull(myReader("piano"), "")
                txtAlloggio.Text = par.IfNull(myReader("alloggio"), "")

                CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text = par.IfNull(myReader("RAPPORTO"), "")
                txtPosizione.Text = par.IfNull(myReader("POSIZIONE"), "")


                If par.IfNull(myReader("COD_TIPO_CONTR"), "") <> "" Then
                    CType(Tab_InfoContratto1.FindControl("cmbTipoRU"), DropDownList).SelectedValue = par.IfNull(myReader("COD_TIPO_CONTR"), "")
                Else
                    CType(Tab_InfoContratto1.FindControl("cmbTipoRU"), DropDownList).SelectedValue = codTipoContr
                End If

                If par.IfNull(myReader("int_c"), "0") = "0" Then
                    chIntestatario.Checked = False
                Else
                    chIntestatario.Checked = True
                End If

                If par.IfNull(myReader("int_a"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("cmbAbusivo"), DropDownList).SelectedValue = "0"
                Else
                    CType(Tab_InfoContratto1.FindControl("cmbAbusivo"), DropDownList).SelectedValue = "1"
                End If

                If par.IfNull(myReader("int_M"), "0") = "0" Then
                    chCompNucleo.Checked = False
                Else
                    chCompNucleo.Checked = True
                End If

                If par.IfNull(myReader("FL_TUTORE"), "0") = "0" Then
                    chRappLegale.Checked = False
                Else
                    chRappLegale.Checked = True
                End If

                If par.IfNull(myReader("FL_RIC_POSTA"), "0") = "0" Then
                    chPosta.Checked = False
                Else
                    chPosta.Checked = True
                End If

                If par.IfNull(myReader("FL_RIC_SINDACATO"), "0") = "0" Then
                    chSindacato.Checked = False
                Else
                    chSindacato.Checked = True
                End If

                If par.IfNull(myReader("FL_COMITATI"), "0") = "0" Then
                    chComitati.Checked = False
                Else
                    chComitati.Checked = True
                End If

                If par.IfNull(myReader("FL_RIC_DOM"), "0") = "0" Then
                    chDomicilio.Checked = False
                Else
                    chDomicilio.Checked = True
                End If

                '**** FLAG FF.OO. IN SERVIZIO ****
                Dim item As MenuItem
                If par.IfNull(myReader("FL_IN_SERVIZIO"), "0") = "0" Then
                    chInServizio.Checked = False
                Else
                    chInServizio.Checked = True
                    item = New MenuItem("Autocert.stato di servizio", "AutocertStServ", "", "javascript:AutocertStServ();")
                    MenuStampe.Items(0).ChildItems.AddAt(MenuStampe.Items(0).ChildItems.Count - 1, item)
                End If

                If par.IfNull(myReader("FL_SOSP_1"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("chVAIN"), CheckBox).Checked = False
                Else
                    CType(Tab_InfoContratto1.FindControl("chVAIN"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_2"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("chCAIN"), CheckBox).Checked = False
                Else
                    CType(Tab_InfoContratto1.FindControl("chCAIN"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_3"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("chVerTitol"), CheckBox).Checked = False
                Else
                    CType(Tab_InfoContratto1.FindControl("chVerTitol"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_4"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("chRilascio"), CheckBox).Checked = False
                Else
                    CType(Tab_InfoContratto1.FindControl("chRilascio"), CheckBox).Checked = True
                End If


                If par.IfNull(myReader("FL_SOSP_5"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("chAMPL"), CheckBox).Checked = False
                Else
                    CType(Tab_InfoContratto1.FindControl("chAMPL"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_SOSP_7"), "0") = "0" Then
                    CType(Tab_InfoContratto1.FindControl("chDocManc"), CheckBox).Checked = False
                Else
                    CType(Tab_InfoContratto1.FindControl("chDocManc"), CheckBox).Checked = True
                End If

                If par.IfNull(myReader("FL_DA_VERIFICARE"), "0") = "1" Then
                    daVerificare = "1"

                    If par.IfNull(myReader("FL_VERIFICA_REDDITO"), "0") = "1" Then
                        CType(Tab_InfoContratto1.FindControl("chRedditi"), CheckBox).Checked = True
                        flagDaVerific = "1"
                    End If
                    If par.IfNull(myReader("FL_VERIFICA_NUCLEO"), "0") = "1" Then
                        CType(Tab_InfoContratto1.FindControl("chNucleo"), CheckBox).Checked = True
                        flagDaVerific = "1"
                    End If
                    If par.IfNull(myReader("FL_VERIFICA_PATRIMONIO"), "0") = "1" Then
                        CType(Tab_InfoContratto1.FindControl("chImmob"), CheckBox).Checked = True
                        flagDaVerific = "1"
                    End If

                End If

                lblISEE.Text = par.IfNull(myReader("isee"), "0,00")
                ISEE_DICHIARAZIONE = lblISEE.Text


                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = cmbNazioneNas
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        CT1 = cmbPrNas
                        CT1.Visible = False
                        CT1 = cmbComuneNas
                        CT1.Visible = False
                        lblComune.Visible = False
                        lblProvincia.Visible = False
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = cmbPrNas
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        CT1.Visible = True
                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas, "ID", "NOME", True)
                        CT1 = cmbComuneNas
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        CT1.Visible = True
                        lblComune.Visible = True
                        lblProvincia.Visible = True
                    End If
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = cmbTipoIRes
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader1("DESCRIZIONE")).Selected = True
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read() Then
                    CT1 = cmbNazioneRes
                    CT1.SelectedIndex = -1
                    If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                        cmbPrRes.Enabled = False
                        cmbComuneRes.Enabled = False
                    Else
                        If AprisolaLettura = 0 Then
                            cmbPrRes.Enabled = True
                            cmbComuneRes.Enabled = True
                        End If
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = cmbPrRes
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True
                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", cmbComuneRes, "ID", "NOME", True)

                        CT1 = cmbComuneRes
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    End If
                End If
                myReader1.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                txtCognome.Text = par.IfNull(myReader("COGNOME"), "")
                txtNome.Text = par.IfNull(myReader("NOME"), "")
                txtCF.Text = par.IfNull(myReader("COD_FISCALE"), "")
                txtDataNascita.Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
            End If
            myReader.Close()




            If FFOO = True Then
                chInServizio.Visible = True
            Else
                chInServizio.Visible = False
            End If
            '***** FINE Controllo se il contratto appartiene o meno alle FFOO *****


            'RAGGRUPPAMENTO FUNZIONI CARICAMENTO
            CaricaNucleo()
            CaricaNucleoSpese()
            CaricaDocumenti()
            CaricaDocumentiPresenti()
            CaricaRedditi()
            CaricaPatrimMob()
            CaricaPatrimImmob()
            CaricaDiffide()
            CaricaDatiISEE()

            Session.Add("LAVORAZIONE", "1")

            If lIndice_Bando = 0 Then
                MenuStampe.Visible = False
            End If

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 54 Then
                AprisolaLettura = 1
                GoTo RIPETI_LETTURA
            Else
                imgStampa.Enabled = False
                btnSalva.Enabled = False
                par.OracleConn.Close()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
                HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
                Session.Item("LAVORAZIONE") = "0"
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza: Visualizza Dichiarazione - " & EX1.Message)
                Response.Redirect("../Errore.aspx", False)
            End If

        Catch ex As Exception
            imgStampa.Enabled = False
            btnSalva.Enabled = False

            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza: Visualizza Dichiarazione - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
    End Sub

    Private Sub MessJQuery(ByVal Messaggio As String, ByVal Tipo As Integer, Optional ByVal Titolo As String = "Messaggio")
        Try
            Dim sc As String = ""
            If Tipo = 0 Then
                sc = ScriptErrori(Messaggio, Titolo)
            Else
                sc = ScriptChiudi(Messaggio, Titolo)
            End If
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptMsg", sc, True)

        Catch ex As Exception

        End Try
    End Sub

    Private Function ScriptErrori(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#dialog').text('" & Messaggio & "');")
            sb.Append("$('#dialog').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Function ScriptChiudi(ByVal Messaggio As String, Optional ByVal Titolo As String = "Messaggio") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#dialog').text('" & Messaggio & "');")
            sb.Append("$('#dialog').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "',buttons: {'Ok': function() {$(this).dialog('close');}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub SceltaJQuery(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "")
        Try
            Dim sc As String = ScriptScelta(Messaggio, Funzione, Titolo, Funzione2)
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "ScriptScelta", sc, True)
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - SceltaJQuery() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Private Sub DatiinModifica392()

        ChNatoEstero.Enabled = True
        ChCittadinanza.Enabled = True
        txtAlloggio.Enabled = True

        txtCAPRes.Enabled = True
        txtCIData.Enabled = True
        txtCINum.Enabled = True
        txtCIRilascio.Enabled = True
        txtCivicoRes.Enabled = True
        txtCodConvocazione.Enabled = True

        txtCSData.Enabled = True
        txtCSNum.Enabled = True
        txtDataNascita.Enabled = True
        txtDataPG.Enabled = True
        txtFoglio.Enabled = True
        txtIndRes.Enabled = True
        txtMappale.Enabled = True

        txtPiano.Enabled = True
        txtPosizione.Enabled = True
        txtPSData.Enabled = True
        txtPSNum.Enabled = True
        txtPSRinnovo.Enabled = True
        txtPSScade.Enabled = True
        txtScala.Enabled = True
        txtSub.Enabled = True
        txtTelRes.Enabled = True


        cmbComuneNas.Enabled = True
        cmbComuneRes.Enabled = True
        cmbLavorativa.Enabled = True
        cmbNazioneNas.Enabled = True
        cmbNazioneRes.Enabled = True
        cmbPrNas.Enabled = True
        cmbPrRes.Enabled = True
        cmbTipoDocumento.Enabled = True
        cmbTipoIRes.Enabled = True

        txtTelefono.Enabled = True
        txtEmail.Enabled = True
        chIntestatario.Enabled = True
        chCompNucleo.Enabled = True
        chRappLegale.Enabled = True
        chPosta.Enabled = True
        chSindacato.Enabled = True
        chComitati.Enabled = True
        chInServizio.Enabled = True

        txtIndirizzo.Enabled = True
        txtCivicoSpediz.Enabled = True
        txtCapSpediz.Enabled = True
        chkRicevePresso.Enabled = True
        txtPressoIndirizzo.Enabled = True

        cmbTipoISped.Enabled = True
        cmbPrSpediz.Enabled = True
        cmbComuneSpediz.Enabled = True

        chDomicilio.Enabled = True

        For Each ctrl As Control In Me.Tab_Nucleo1.Controls
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = True
            End If
        Next
        For Each ctrl As Control In Me.Dic_Reddito1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = True
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = True
            End If

            CType(Dic_Reddito1.FindControl("imgAnteprima"), Image).Visible = False
        Next
        For Each ctrl As Control In Me.Dic_Patrimonio1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = True
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = True
            End If

        Next
        For Each ctrl As Control In Me.Tab_InfoContratto1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = True
            End If
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Enabled = True
            End If
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = True
            End If

        Next
        For Each ctrl As Control In Me.dic_Documenti1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = True
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = True
            End If
            If TypeOf ctrl Is ImageButton Then
                DirectCast(ctrl, ImageButton).Visible = True
            End If
            If TypeOf ctrl Is ImageButton Then
                DirectCast(ctrl, ImageButton).Visible = True
            End If
        Next


        lblImportaRes.Visible = True
        btnImportaRes.Visible = True

        btnImportaRec.Visible = True
        lblImportaRec.Visible = True

        lblData392.Enabled = True
        txtData392.Enabled = True
    End Sub


    Private Sub DatiSoloLettura392()

        ChNatoEstero.Enabled = False
        ChCittadinanza.Enabled = False
        txtAlloggio.Enabled = False
        txtCF.Enabled = False
        txtCAPRes.Enabled = False
        txtCIData.Enabled = False
        txtCINum.Enabled = False
        txtCIRilascio.Enabled = False
        txtCivicoRes.Enabled = False
        txtCodConvocazione.Enabled = False
        txtCognome.Enabled = False
        txtCSData.Enabled = False
        txtCSNum.Enabled = False
        txtDataNascita.Enabled = False
        txtDataPG.Enabled = False
        txtFoglio.Enabled = False
        txtIndRes.Enabled = False
        txtMappale.Enabled = False
        txtNome.Enabled = False
        txtPiano.Enabled = False
        txtPosizione.Enabled = False
        txtPSData.Enabled = False
        txtPSNum.Enabled = False
        txtPSRinnovo.Enabled = False
        txtPSScade.Enabled = False
        txtScala.Enabled = False
        txtSub.Enabled = False
        txtTelRes.Enabled = False
        lblISEE.Enabled = False
        lblPG.Enabled = False
        cmbComuneNas.Enabled = False
        cmbComuneRes.Enabled = False
        cmbLavorativa.Enabled = False
        cmbNazioneNas.Enabled = False
        cmbNazioneRes.Enabled = False
        cmbPrNas.Enabled = False
        cmbPrRes.Enabled = False
        cmbTipoDocumento.Enabled = False
        cmbTipoIRes.Enabled = False

        txtTelefono.Enabled = False
        txtEmail.Enabled = False
        chIntestatario.Enabled = False
        chCompNucleo.Enabled = False
        chRappLegale.Enabled = False
        chPosta.Enabled = False
        chSindacato.Enabled = False
        chComitati.Enabled = False
        chInServizio.Enabled = False

        txtIndirizzo.Enabled = False
        txtCivicoSpediz.Enabled = False
        txtCapSpediz.Enabled = False
        chkRicevePresso.Enabled = False
        txtPressoIndirizzo.Enabled = False

        cmbTipoISped.Enabled = False
        cmbPrSpediz.Enabled = False
        cmbComuneSpediz.Enabled = False

        chDomicilio.Enabled = False
        For Each ctrl As Control In Me.Tab_Nucleo1.Controls
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If
        Next
        For Each ctrl As Control In Me.Dic_Reddito1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If

            CType(Dic_Reddito1.FindControl("imgAnteprima"), Image).Visible = True
        Next
        For Each ctrl As Control In Me.Dic_Patrimonio1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If

        Next
        For Each ctrl As Control In Me.Tab_InfoContratto1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Enabled = False
            End If
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = False
            End If

        Next
        For Each ctrl As Control In Me.dic_Documenti1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If
            If TypeOf ctrl Is ImageButton Then
                DirectCast(ctrl, ImageButton).Visible = False
            End If
        Next


        lblImportaRes.Visible = False
        btnImportaRes.Visible = False

        btnImportaRec.Visible = False
        lblImportaRec.Visible = False

        lblData392.Enabled = False
        txtData392.Enabled = False

    End Sub


    Private Sub DatiSoloLettura()

        ChNatoEstero.Enabled = False
        ChCittadinanza.Enabled = False
        txtAlloggio.Enabled = False
        txtCF.Enabled = False
        txtCAPRes.Enabled = False
        txtCIData.Enabled = False
        txtCINum.Enabled = False
        txtCIRilascio.Enabled = False
        txtCivicoRes.Enabled = False
        txtCodConvocazione.Enabled = False
        txtCognome.Enabled = False
        txtCSData.Enabled = False
        txtCSNum.Enabled = False
        txtDataNascita.Enabled = False
        txtDataPG.Enabled = False
        txtFoglio.Enabled = False
        txtIndRes.Enabled = False
        txtMappale.Enabled = False
        txtNome.Enabled = False
        txtPiano.Enabled = False
        txtPosizione.Enabled = False
        txtPSData.Enabled = False
        txtPSNum.Enabled = False
        txtPSRinnovo.Enabled = False
        txtPSScade.Enabled = False
        txtScala.Enabled = False
        txtSub.Enabled = False
        txtTelRes.Enabled = False
        lblISEE.Enabled = False
        lblPG.Enabled = False
        cmbComuneNas.Enabled = False
        cmbComuneRes.Enabled = False
        cmbLavorativa.Enabled = False
        cmbNazioneNas.Enabled = False
        cmbNazioneRes.Enabled = False
        cmbPrNas.Enabled = False
        cmbPrRes.Enabled = False
        cmbTipoDocumento.Enabled = False
        cmbTipoIRes.Enabled = False

        txtTelefono.Enabled = False
        txtEmail.Enabled = False
        chIntestatario.Enabled = False
        chCompNucleo.Enabled = False
        chRappLegale.Enabled = False
        chPosta.Enabled = False
        chSindacato.Enabled = False
        chComitati.Enabled = False
        chInServizio.Enabled = False

        txtIndirizzo.Enabled = False
        txtCivicoSpediz.Enabled = False
        txtCapSpediz.Enabled = False

        chkRicevePresso.Enabled = False
        txtPressoIndirizzo.Enabled = False

        cmbTipoISped.Enabled = False
        cmbPrSpediz.Enabled = False
        cmbComuneSpediz.Enabled = False

        chDomicilio.Enabled = False

        For Each ctrl As Control In Me.Tab_Nucleo1.Controls
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If
        Next
        For Each ctrl As Control In Me.Dic_Reddito1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If
            CType(Dic_Reddito1.FindControl("imgAnteprima"), Image).Visible = True
        Next
        For Each ctrl As Control In Me.Dic_Patrimonio1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If
        Next
        For Each ctrl As Control In Me.Tab_InfoContratto1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Enabled = False
            End If
            If TypeOf ctrl Is DropDownList Then
                DirectCast(ctrl, DropDownList).Enabled = False
            End If
        Next
        For Each ctrl As Control In Me.dic_Documenti1.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Enabled = False
            End If
            If TypeOf ctrl Is Image Then
                DirectCast(ctrl, Image).Visible = False
            End If
            If TypeOf ctrl Is ImageButton Then
                DirectCast(ctrl, ImageButton).Visible = False
            End If
        Next

    End Sub

    Private Function ScriptScelta(ByVal Messaggio As String, ByVal Funzione As String, Optional ByVal Titolo As String = "Messaggio", Optional ByVal Funzione2 As String = "") As String
        Try
            Dim retvalue As String = ""
            Dim sb As New StringBuilder
            sb.Append("$(document).ready(function(){")
            sb.Append("$('#ScriptScelta').text('" & Messaggio & "');")
            sb.Append("$('#ScriptScelta').dialog({ autoOpen:true, modal:true, show:'blind', hide:'explode', title:'" & Titolo & "', buttons: {'Si': function() { __doPostBack('" & Funzione & "', '');{$(this).dialog('close');} },'No': function() {$(this).dialog('close');" & Funzione2 & "}}});")
            sb.Append("});")
            retvalue = sb.ToString()
            Return retvalue
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Private Sub RiempiCmbAnniRedd(ByVal annoredd As Integer)
        cmbAnnoReddituale.Items.Clear()
        For i As Integer = annoredd To Year(Now)
            cmbAnnoReddituale.Items.Add(New ListItem(i))
        Next
    End Sub

    Public Sub EliminaComponente()
        Try
            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtComp.Rows.Count
                    Dim rowScart As Data.DataRow = dtComp.Rows(k)
                    If rowScart.ItemArray(0) = idComp.Value Then
                        If rowScart.ItemArray(2) = "0" Then
                            MessJQuery("Non è possibile eliminare il dichiarante!", 0, "Attenzione")
                            Exit Try
                        Else
                            rowScart.Delete()
                        End If
                    End If
                    k = k + 1
                Loop
                dtComp.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_NUCLEO WHERE ID=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                Dim k2 As Integer = 0
                Do While k2 < dtCompSpese.Rows.Count
                    Dim rowScart As Data.DataRow = dtCompSpese.Rows(k2)
                    If rowScart.ItemArray(1) = idComp.Value Then
                        rowScart.Delete()
                    End If
                    k2 = k2 + 1
                Loop
                dtCompSpese.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_componente=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataSource = dtComp
                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataBind()

                CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataSource = dtCompSpese
                CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                EliminaRiferimentiComp()
                'CaricaNucleo()
                'CaricaNucleoSpese()
                MessJQuery("Operazione effettuata!", 0, "Info")
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
            End If




        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaComponente() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Protected Sub EliminaRiferimentiComp()

        Try

            Dim trovato As Integer = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                '‘par.cmd.Transaction = par.myTrans
            End If





            'cancella riferimenti a redditi

            Dim z As Integer = 0
            Do While z < dtRedd.Rows.Count
                Dim rowScart As Data.DataRow = dtRedd.Rows(z)
                If IsDBNull(rowScart.ItemArray(0)) = False Then
                    If rowScart.ItemArray(0) = idComp.Value Then
                        rowScart.Delete()
                        trovato = 1
                    End If
                End If
                z = z + 1
            Loop
            dtRedd.AcceptChanges()

            If trovato = 1 Then
                par.cmd.CommandText = "DELETE FROM UTENZA_REDDITI WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()
            End If

            CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
            CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()


            'cancella riferimenti a DETRAZIONI

            trovato = 0
            z = 0
            Do While z < dtDetraz.Rows.Count
                Dim rowScart As Data.DataRow = dtDetraz.Rows(z)
                If IsDBNull(rowScart.ItemArray(0)) = False Then
                    If rowScart.ItemArray(0) = idComp.Value Then
                        rowScart.Delete()
                        trovato = 1
                    End If
                End If
                z = z + 1
            Loop
            dtDetraz.AcceptChanges()

            If trovato = 1 Then

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_DETRAZIONI WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()
            End If

            CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
            CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()



            ' cancella riferimenti a patrimonio mobiliare
            trovato = 0
            z = 0
            Do While z < dtMob.Rows.Count
                Dim rowScart As Data.DataRow = dtMob.Rows(z)
                If IsDBNull(rowScart.ItemArray(0)) = False Then
                    If rowScart.ItemArray(0) = idComp.Value Then
                        rowScart.Delete()
                        trovato = 1
                    End If
                End If
                z = z + 1
            Loop
            dtMob.AcceptChanges()

            If trovato = 1 Then
                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()
            End If

            CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataSource = dtMob
            CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataBind()


            ' cancella riferimenti a patrimonio IMMOBILIARE

            trovato = 0
            z = 0
            Do While z < dtImmob.Rows.Count
                Dim rowScart As Data.DataRow = dtImmob.Rows(z)
                If IsDBNull(rowScart.ItemArray(0)) = False Then
                    If rowScart.ItemArray(0) = idComp.Value Then
                        rowScart.Delete()
                        trovato = 1
                    End If
                End If
                z = z + 1
            Loop
            dtImmob.AcceptChanges()

            If trovato = 1 Then
                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

            End If

            CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
            CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaRiferimentiComp() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try



    End Sub

    Public Sub EliminaDocumentiP()
        Dim DescrizioneCompleta As String = ""
        Try
            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtDocPresenti.Rows.Count
                    Dim rowScart As Data.DataRow = dtDocPresenti.Rows(k)
                    If rowScart.ItemArray(0) = CType(dic_Documenti1.FindControl("idDocP"), HiddenField).Value Then
                        If rowScart.ItemArray(2) = CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value Then
                            rowScart.Delete()
                        End If
                    End If
                    k = k + 1
                Loop
                dtDocPresenti.AcceptChanges()
                DescrizioneCompleta = CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value
                CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value = Trim(Replace(Replace(Replace(Replace((CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value), vbCrLf, ""), "/", ""), "\", ""), " ", ""))

                par.cmd.CommandText = "DELETE FROM UTENZA_DOC_PRESENTATA WHERE trim(replace(replace(replace(DESCRIZIONE,' ',''),'/',''),'\',''))='" & par.PulisciStrSql(CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value) & "'" & " AND ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(dic_Documenti1.FindControl("idDocP"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                 & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                 & "'F03','ELIMINATO DOC. PRESENTATO - " & par.PulisciStrSql(Mid(DescrizioneCompleta, 1, 70)) & "','I')"
                par.cmd.ExecuteNonQuery()


                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(dic_Documenti1.FindControl("DataGridPresenti"), DataGrid).DataSource = dtDocPresenti
                CType(dic_Documenti1.FindControl("DataGridPresenti"), DataGrid).DataBind()
                confCanc.Value = 0
                '                funzioneSalva()
                CType(dic_Documenti1.FindControl("idDocP"), HiddenField).Value = "-1"
                MessJQuery("Operazione effettuata!", 0, "Info")

            Else
                confCanc.Value = 0
            End If


        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaDocumentiPresentati() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub






    Public Sub EliminaDocumenti()
        Dim DescrizioneCompleta As String = ""

        Try
            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtDoc.Rows.Count
                    Dim rowScart As Data.DataRow = dtDoc.Rows(k)
                    If rowScart.ItemArray(0) = CType(dic_Documenti1.FindControl("idDoc"), HiddenField).Value Then
                        If rowScart.ItemArray(2) = CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value Then
                            rowScart.Delete()
                        End If
                    End If
                    k = k + 1
                Loop
                dtDoc.AcceptChanges()
                DescrizioneCompleta = CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value
                CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value = Trim(Replace(Replace(Replace(Replace((CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value), vbCrLf, ""), "/", ""), "\", ""), " ", ""))


                par.cmd.CommandText = "DELETE FROM UTENZA_DOC_MANCANTE WHERE trim(replace(replace(replace(DESCRIZIONE,' ',''),'/',''),'\',''))='" & par.PulisciStrSql(CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value) & "'" & " AND ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(dic_Documenti1.FindControl("idDoc"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                 & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                 & "'F03','ELIMINATO DOC. MANCANTE - " & par.PulisciStrSql(Mid(DescrizioneCompleta, 1, 70)) & "','I')"
                par.cmd.ExecuteNonQuery()

                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataSource = dtDoc
                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataBind()
                confCanc.Value = 0
                '                funzioneSalva()
                CType(dic_Documenti1.FindControl("idDoc"), HiddenField).Value = "-1"
                MessJQuery("Operazione effettuata!", 0, "Info")

            Else
                confCanc.Value = 0
            End If


        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaDocumenti() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub EliminaDetrazioni()
        Try
            If confCanc.Value = 1 Then

                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtDetraz.Rows.Count
                    Dim rowScart As Data.DataRow = dtDetraz.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Reddito1.FindControl("idDetraz"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtDetraz.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_DETRAZIONI WHERE ID=" & CType(Dic_Reddito1.FindControl("idDetraz"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                'funzioneSalva()
                MessJQuery("Operazione effettuata!", 0, "Info")
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaDetrazioni() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub EliminaRedditi()
        Try
            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtRedd.Rows.Count
                    Dim rowScart As Data.DataRow = dtRedd.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Reddito1.FindControl("idReddito"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtRedd.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM UTENZA_REDDITI WHERE ID=" & CType(Dic_Reddito1.FindControl("idReddito"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & CType(Dic_Reddito1.FindControl("idCompReddito"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & CType(Dic_Reddito1.FindControl("idCompReddito"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                'funzioneSalva()
                MessJQuery("Operazione effettuata!", 0, "Info")
            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaRedditi() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub EliminaMobili()
        Try

            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtMob.Rows.Count
                    Dim rowScart As Data.DataRow = dtMob.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Patrimonio1.FindControl("idPatrMob"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtMob.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_PATR_MOB WHERE ID=" & CType(Dic_Patrimonio1.FindControl("idPatrMob"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataSource = dtMob
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                MessJQuery("Operazione effettuata!", 0, "Info")

            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)



            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaMobili()" & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Public Sub EliminaImmobili()
        Try
            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)

                Dim k As Integer = 0
                Do While k < dtImmob.Rows.Count
                    Dim rowScart As Data.DataRow = dtImmob.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Patrimonio1.FindControl("idPatrImmob"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtImmob.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM UTENZA_COMP_PATR_IMMOB WHERE ID=" & CType(Dic_Patrimonio1.FindControl("idPatrImmob"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                If Not IsNothing(par.myTrans) Then
                    par.myTrans.Commit()
                End If
                par.myTrans = par.OracleConn.BeginTransaction()
                HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                'funzioneSalva()
                MessJQuery("Operazione effettuata!", 0, "Info")

            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)



            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaImmobili() " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
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

    Public Property strErroriCF() As String
        Get
            If Not (ViewState("par_strErroriCF") Is Nothing) Then
                Return CStr(ViewState("par_strErroriCF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_strErroriCF") = value
        End Set

    End Property

    Public Property FFOO() As Boolean
        Get
            If Not (ViewState("par_FFOO") Is Nothing) Then
                Return CLng(ViewState("par_FFOO"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_FFOO") = value
        End Set

    End Property

    Public Property ISEE_DICHIARAZIONE() As String
        Get
            If Not (ViewState("par_ISEE_DICHIARAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_ISEE_DICHIARAZIONE"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_ISEE_DICHIARAZIONE") = value
        End Set

    End Property

    Public Property annoRedd() As String
        Get
            If Not (ViewState("par_annoRedd") Is Nothing) Then
                Return CStr(ViewState("par_annoRedd"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_annoRedd") = value
        End Set

    End Property

    Public Property lIndice_Bando() As Long
        Get
            If Not (ViewState("par_lIndice_Bando1") Is Nothing) Then
                Return CLng(ViewState("par_lIndice_Bando1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIndice_Bando1") = value
        End Set

    End Property

    Public Property lIdDichiarazione() As Long
        Get
            If Not (ViewState("par_lIdDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichiarazione") = value
        End Set

    End Property

    Public Property lIdDichImport() As Long
        Get
            If Not (ViewState("par_lIdDichImport") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichImport"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichImport") = value
        End Set

    End Property

    Public Property bMemorizzato() As Boolean
        Get
            If Not (ViewState("par_bMemorizzato1") Is Nothing) Then
                Return CLng(ViewState("par_bMemorizzato1"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_bMemorizzato1") = value
        End Set

    End Property

    Public Property SoloLettura() As Long
        Get
            If Not (ViewState("par_SoloLettura") Is Nothing) Then
                Return CLng(ViewState("par_SoloLettura"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_SoloLettura") = value
        End Set

    End Property

    Public Property lNuovaDichiarazione() As Long
        Get
            If Not (ViewState("par_lNuovaDichiarazione") Is Nothing) Then
                Return CLng(ViewState("par_lNuovaDichiarazione"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lNuovaDichiarazione") = value
        End Set

    End Property

    Public Property CHIUDI() As Long
        Get
            If Not (ViewState("par_CHIUDI") Is Nothing) Then
                Return CLng(ViewState("par_CHIUDI"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_CHIUDI") = value
        End Set

    End Property

    Public Property TORNA() As Long
        Get
            If Not (ViewState("par_TORNA") Is Nothing) Then
                Return CLng(ViewState("par_TORNA"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_TORNA") = value
        End Set

    End Property

    Protected Sub imgUscita_Click(sender As Object, e As System.EventArgs) Handles imgUscita.Click
        opUscita()
    End Sub

    Private Function VerificaDati(ByRef S As String) As Boolean
        VerificaDati = True

        If txtCognome.Text = "" Then
            MessJQuery("ATTENZIONE...Campo <COGNOME> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If
        If txtNome.Text = "" Then
            MessJQuery("ATTENZIONE...Campo <NOME> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If
        If txtCF.Text = "" Then
            MessJQuery("ATTENZIONE...Campo <CODICE FISCALE> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If

        If txtDataNascita.Text = "" Then
            MessJQuery("ATTENZIONE...Campo <DATA DI NASCITA> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If

        If par.ControllaCF(txtCF.Text) = False Then
            MessJQuery("ATTENZIONE...Il codice fiscale dell\'intestatario non è corretto!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If
        If par.ControllaValiditaCF(txtCF.Text, txtCognome.Text, txtNome.Text, txtDataNascita.Text) = False Then
            MessJQuery("ATTENZIONE...Il codice fiscale dell\'intestatario non è corretto!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If

        If txtCIData.Text <> "" Then
            If Len(txtCIData.Text) <> 10 Then
                MessJQuery("ATTENZIONE...Campo <DATA RILASCIO DOCUMENTO> errato!", 0, "Attenzione")
                VerificaDati = False
                Exit Function
            End If
        End If

        If txtPSData.Text <> "" Then
            If Len(txtPSData.Text) <> 10 Then
                MessJQuery("ATTENZIONE...Campo <DATA RILASCIO PERMESSO SOGG.> errato!", 0, "Attenzione")
                VerificaDati = False
                Exit Function
            End If
        End If

        If txtCSData.Text <> "" Then
            If Len(txtCSData.Text) <> 10 Then
                MessJQuery("ATTENZIONE...Campo <DATA RILASCIO CARTA SOGG.> errato!", 0, "Attenzione")
                VerificaDati = False
                Exit Function
            End If
        End If

        If txtData392.Text <> "" Then
            If Len(txtData392.Text) <> 10 Then
                MessJQuery("ATTENZIONE...Campo <DATA RIC./INVIO DISDETTA 392> errato!", 0, "Attenzione")
                VerificaDati = False
                Exit Function
            End If
            If par.AggiustaData(txtData392.Text) < par.AggiustaData(lblAvviso394.Text) Then
                MessJQuery("ATTENZIONE...Campo <DATA RIC./INVIO DISDETTA 392> deve essere successivo o uguale al " & lblAvviso394.Text & " !", 0, "Attenzione")
                VerificaDati = False
                Exit Function
            End If
        End If

    End Function

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        funzioneSalva()
        Session.Item("STAMPATO") = "0"
    End Sub


    Private Sub funzioneSalva()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            End If

            Dim patrImmob As Boolean = False


            bMemorizzato = False

            Dim sospensione As String = "0"
            If Valore01(CType(Tab_InfoContratto1.FindControl("chVAIN"), CheckBox).Checked) = "1" Then
                sospensione = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chCAIN"), CheckBox).Checked) = "1" Then
                sospensione = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chAMPL"), CheckBox).Checked) = "1" Then
                sospensione = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chDocManc"), CheckBox).Checked) = "1" Then
                sospensione = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chVerTitol"), CheckBox).Checked) = "1" Then
                sospensione = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chRilascio"), CheckBox).Checked) = "1" Then
                sospensione = "1"
            End If

            'Dim daVerificare As String = "0"
            flagDaVerific = "0"

            If Valore01(CType(Tab_InfoContratto1.FindControl("chRedditi"), CheckBox).Checked) = "1" Then
                flagDaVerific = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chNucleo"), CheckBox).Checked) = "1" Then
                flagDaVerific = "1"
            End If
            If Valore01(CType(Tab_InfoContratto1.FindControl("chImmob"), CheckBox).Checked) = "1" Then
                flagDaVerific = "1"
            End If

            If flagDaVerific = "0" Then
                daVerificare = "0"
            Else
                daVerificare = "1"
            End If

            Dim valorizzaIntest As Integer = 0

            If Valore01(chIntestatario.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If
            If Valore01(chCompNucleo.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If
            If Valore01(chRappLegale.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If
            If Valore01(chPosta.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If
            If Valore01(chSindacato.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If
            If Valore01(chComitati.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If
            If Valore01(chInServizio.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If

            If Valore01(chDomicilio.Checked) = "1" Then
                valorizzaIntest = valorizzaIntest + 1
            End If

            'CaricaRedditi()
            'CaricaNucleo()
            'CaricaNucleoSpese()
            'CaricaPatrimMob()
            'patrImmob = CaricaPatrimImmob()
            'CaricaDocumenti()

            ''max verifica dei singoli cf momentaneamente sospesa
            'If cmbStato.SelectedItem.Text = "COMPLETA" Then
            '    Dim TROVATOSIPO As Boolean = True
            '    par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            '    Dim myReadersipo As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    While myReadersipo.Read
            '        par.cmd.CommandText = "SELECT * FROM UTENZA_EVENTI_DICHIARAZIONI WHERE COD_EVENTO='F166' AND TRIM(UPPER(MOTIVAZIONE))='" & UCase(par.IfNull(myReadersipo("COD_FISCALE"), "")) & "' AND ID_PRATICA=" & lIdDichiarazione
            '        Dim myReadersipo1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReadersipo1.HasRows = False Then
            '            TROVATOSIPO = False
            '        End If
            '        myReadersipo1.Close()
            '    End While
            '    myReadersipo.Close()
            '    If TROVATOSIPO = False Then
            '        MessJQuery("Attenzione...Con stato domanda = COMPLETA tutti i componenti devono essere stati verificati tramite SIPO. Premere il pulsante EVENTI per visualizzare i componenti senza verifica.\n Memorizzazione non effettuata!", 0, "Attenzione")
            '        Exit Sub
            '    End If

            'End If

            If CType(Tab_ISEE1.FindControl("txtProtocolloDSU"), TextBox).Text <> "" Then
                If Len(CType(Tab_ISEE1.FindControl("txtProtocolloDSU"), TextBox).Text) <> 27 Then
                    MessJQuery("ATTENZIONE...Protocollo DSU NON VALIDO.\n Memorizzazione non effettuata!", 0, "Attenzione")
                    Exit Try
                End If
                If InStr(CType(Tab_ISEE1.FindControl("txtProtocolloDSU"), TextBox).Text, "INPS-ISEE-") = 0 Then
                    MessJQuery("ATTENZIONE...Protocollo DSU NON VALIDO.\n Memorizzazione non effettuata!", 0, "Attenzione")
                    Exit Try
                End If
            End If

            If strErroriCF <> "" Then
                MessJQuery("ATTENZIONE...Errori nei codici fiscali:\n" & strErroriCF & ".\n Memorizzazione non effettuata!", 0, "Attenzione")
                Exit Try
            End If


            Dim S As String
            S = ""
            If VerificaDati(S) = False Then
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If

            If IsDate(txtDataPG.Text) = False Then
                MessJQuery("ATTENZIONE...La data protocollo non è corretta! Memorizzazione non effettuata.", 0, "Attenzione")
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If

            If DateDiff("m", DateSerial(Mid(txtDataNascita.Text, 7, 4), Mid(txtDataNascita.Text, 4, 2), Mid(txtDataNascita.Text, 1, 2)), Now) / 12 < 18 Then
                MessJQuery("ATTENZIONE...Il richiedente deve essere maggiorenne! Memorizzazione non effettuata.", 0, "Attenzione")
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If

            If Len(txtCAPRes.Text) < 5 Then
                MessJQuery("ATTENZIONE...Il CAP di Residenza deve avere 5 caratteri! Memorizzazione non effettuata.", 0, "Attenzione")
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If


            If lbl45_Lotto.Visible = True Then
                If txtCodConvocazione.Text = "" Then
                    MessJQuery("ATTENZIONE...Specificare il codice di convocazione!", 0, "Attenzione")
                    'imgStampa.Enabled = False
                    'imgStampa.Text = ""
                    Exit Try
                End If
            End If

            If Len(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text) = 19 Then
                txtPosizione.Text = Mid(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, 1, 17)
            End If

            If valorizzaIntest = 0 Then
                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                    MessJQuery("ATTENZIONE...Specificare se intestatario di contratto o altro!", 0, "Attenzione")
                    'imgStampa.Enabled = False
                    'imgStampa.Text = ""
                    Exit Try
                Else
                    'imgStampa.Enabled = False
                    'imgStampa.Text = ""

                End If
            End If



            Dim COMUNITARIO As Boolean = True
            vcomunitario.Value = "0"

            If cmbNazioneNas.SelectedItem.Text <> "ITALIA" Then

                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(cmbNazioneNas.SelectedItem.Text) & "'"
                Dim myReadernaz As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReadernaz.Read() Then
                    If par.IfNull(myReadernaz("SIGLA"), "") = "C" Or par.IfNull(myReadernaz("SIGLA"), "") = "I" Then
                        COMUNITARIO = True
                        vcomunitario.Value = "0"
                    Else
                        COMUNITARIO = False
                        vcomunitario.Value = "1"
                    End If
                Else
                    COMUNITARIO = False
                    vcomunitario.Value = "1"
                End If
                myReadernaz.Close()

            End If


            If cmbNazioneNas.SelectedItem.Text <> "ITALIA" And ChNatoEstero.Checked = False And COMUNITARIO = False And ChCittadinanza.Checked = False Then


                If txtPSNum.Text = "" And txtCSNum.Text = "" Then
                    If cmbStato.SelectedItem.Text = "COMPLETA" Then
                        MessJQuery("ATTENZIONE...Intestatario extra comunitario. Inserire gli estremi del permesso o carta di soggiorno! Memorizzazione non effettuata.", 0, "Attenzione")
                        'imgStampa.Enabled = False
                        'imgStampa.Text = ""
                        Exit Try
                    Else
                        imgStampa.Enabled = False
                        imgStampa.Text = ""
                    End If
                End If

                If par.IfEmpty(txtCSNum.Text, "") = "" Then
                    If Len(par.AggiustaData(par.PulisciStrSql(txtPSScade.Text))) = 8 Then
                        If par.AggiustaData(par.PulisciStrSql(txtPSScade.Text)) < Format(Now, "yyyyMMdd") Then
                            If par.AggiustaData(par.PulisciStrSql(txtPSRinnovo.Text)) = "" Or par.AggiustaData(par.PulisciStrSql(txtPSRinnovo.Text)) < par.AggiustaData(par.PulisciStrSql(txtPSScade.Text)) Then
                                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                                    MessJQuery("ATTENZIONE...Intestatario extra comunitario. Il permesso di soggiorno è scaduto! Memorizzazione non effettuata.", 0, "Attenzione")
                                    'imgStampa.Enabled = False
                                    'imgStampa.Text = ""
                                    Exit Try
                                Else
                                    imgStampa.Enabled = False
                                    imgStampa.Text = ""
                                End If
                            End If
                        End If
                    Else
                        If par.AggiustaData(par.PulisciStrSql(txtPSScade.Text)) < Format(Now, "yyyyMMdd") Then
                            If par.AggiustaData(par.PulisciStrSql(txtPSRinnovo.Text)) = "" Or par.AggiustaData(par.PulisciStrSql(txtPSRinnovo.Text)) < par.AggiustaData(par.PulisciStrSql(txtPSScade.Text)) Then
                                If cmbStato.SelectedItem.Text = "COMPLETA" Then
                                    MessJQuery("ATTENZIONE...Intestatario extra comunitario. Il permesso di soggiorno è scaduto! Memorizzazione non effettuata.", 0, "Attenzione")
                                    'imgStampa.Enabled = False
                                    'imgStampa.Text = ""
                                    Exit Try
                                Else
                                    imgStampa.Enabled = False
                                    imgStampa.Text = ""
                                End If
                            End If
                        End If
                    End If
                End If

                If txtPSRinnovo.Text = "//0" Then txtPSRinnovo.Text = ""

                If cmbLavorativa.SelectedItem.Value = 1 Then
                    If par.AggiustaData(par.PulisciStrSql(txtPSRinnovo.Text)) <> "" Then
                        If DateDiff("d", CDate(txtPSScade.Text), CDate(txtPSRinnovo.Text)) > 60 Then 'And frmBandiERP.casContinuativo.Text = "0" Then
                            MessJQuery("ATTENZIONE...il permesso di soggiorno risulta rinnovato oltre i termini di legge. La dichiarazione verrà inserita automaticamente nelle dichiarazioni -Da Verificare-", 0, "Attenzione")
                            CType(Tab_InfoContratto1.FindControl("chNucleo"), CheckBox).Checked = True
                            'Exit Try
                        End If
                    End If
                Else
                    MessJQuery("ATTENZIONE...Trattasi di cittadino extra comunitario senza attività lavorativa. La dichiarazione verrà inserita automaticamente nelle dichiarazioni -Da Verificare-", 0, "Attenzione")
                    CType(Tab_InfoContratto1.FindControl("chNucleo"), CheckBox).Checked = True
                    'Exit Try
                End If
            Else
                If txtCINum.Text = "" Or txtCIData.Text = "" Or cmbTipoDocumento.SelectedItem.Text = "--" Then
                    If cmbStato.SelectedItem.Text = "COMPLETA" Then
                        MessJQuery("ATTENZIONE...Inserire la tipologia, il numero e la data del documento di riconoscimento! Memorizzazione non effettuata.", 0, "Attenzione")
                        'imgStampa.Enabled = False
                        'imgStampa.Text = ""
                        Exit Try
                    Else
                        imgStampa.Enabled = False
                        imgStampa.Text = ""
                    End If
                End If
            End If

            If sospensione = "1" And daVerificare = "1" Then
                MessJQuery("ATTENZIONE...La dichiarazione può essere sospesa o da verificare! Memorizzazione non effettuata.", 0, "Attenzione")
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If



            If sospensione = "1" And cmbStato.SelectedItem.Text = "COMPLETA" Then
                MessJQuery("ATTENZIONE...La dichiarazione non può essere completa se è in fase di sospensione! Memorizzazione non effettuata.", 0, "Attenzione")
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If

            If lbl45_Lotto.Visible = False Then
                If txtPiano.Text = "" Then
                    If cmbStato.SelectedItem.Text = "COMPLETA" Then
                        MessJQuery("ATTENZIONE...Inserire il piano. Memorizzazione non effettuata.", 0, "Attenzione")
                        'imgStampa.Enabled = False
                        'imgStampa.Text = ""
                        Exit Try
                    Else
                        imgStampa.Enabled = False
                        imgStampa.Text = ""
                    End If
                End If
            End If

            'max...a cosa serve questo controllo??
            If daVerificare = "1" And flagDaVerific = "0" Then
                MessJQuery("ATTENZIONE...La dichiarazione risulta in stato Da Verificare. Selezionare il motivo della verifica!", 0, "Attenzione")
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
                Exit Try
            End If

            Dim descrConfrAU As String = ""
            Dim risultatoConfrAU As Integer = 0

            'CONTROLLO: se compilato il patrimonio immobiliare -> Alert bloccante nel caso non ci siano valori diversi da zero nei 
            'redditi da fabbricato e/o terreni nella sezione dei redditi di tipo autonomo.
            If cmbStato.SelectedValue = "1" Then
                If salvaEsterno.Value = "0" Then

                    Dim NOpensCivile As Integer = 0
                    Dim NOindACC As Integer = 0

                    If N_INV_100_ACC >= 1 Then
                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_IMPORTI,UTENZA_REDDITI where ID_REDD_PENSIONE=3 AND UTENZA_REDD_PENS_IMPORTI.ID_REDD_TOT=UTENZA_REDDITI.ID AND ID_UTENZA=" & lIdDichiarazione
                        Dim myReaderRedd0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            NOpensCivile = 1
                        End If
                        myReaderRedd0.Close()

                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_ES_IMPORTI,UTENZA_REDDITI where ID_REDD_PENS_ESENTI=4 AND UTENZA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT=UTENZA_REDDITI.ID AND ID_UTENZA=" & lIdDichiarazione
                        myReaderRedd0 = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            NOindACC = 1
                        End If
                        myReaderRedd0.Close()
                    End If

                    If NOindACC = 1 Then
                        salvaEsterno.Value = "0"
                        MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100% con accompagnamento. Inserire l\'indennità di accompagnamento nella sezione PENSIONI ESENTI!", 0, "Attenzione")
                        Exit Try
                    End If

                    par.cmd.CommandText = "SELECT * FROM utenza_redd_pens_importi,utenza_redditi_pensione,utenza_redditi,utenza_comp_nucleo WHERE utenza_redd_pens_importi.id_redd_tot = utenza_redditi.ID AND utenza_redd_pens_importi.ID_REDD_PENSIONE=utenza_redditi_pensione.id AND utenza_comp_nucleo.ID = utenza_redditi.id_componente AND ID_UTENZA=" & lIdDichiarazione
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtPens As New Data.DataTable
                    da.Fill(dtPens)
                    da.Dispose()
                    If dtPens.Rows.Count > 0 Then
                        For Each row As Data.DataRow In dtPens.Rows
                            par.cmd.CommandText = "SELECT * FROM utenza_redd_pens_importi,utenza_redditi,utenza_comp_nucleo WHERE utenza_redd_pens_importi.id_redd_tot(+) = utenza_redditi.ID AND utenza_comp_nucleo.ID = utenza_redditi.id_componente AND utenza_redditi.id_utenza<" & lIdDichiarazione & " AND COD_FISCALE='" & par.IfNull(row.Item("COD_FISCALE"), "") & "' order by id_utenza desc"
                            Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderReddP.Read Then
                                If CDec(par.IfNull(myReaderReddP("PENSIONE"), 0)) > CDec(par.IfNull(row.Item("PENSIONE"), 0)) Then
                                    salvaEsterno.Value = "0"
                                    MessJQuery("ATTENZIONE...Il valore della pensione inserito è inferiore a quello inserito nella dichiarazione precedente!", 0, "Attenzione")
                                End If
                            End If
                            myReaderReddP.Close()
                        Next
                    End If

                    If NOpensCivile = 1 And NOindACC = 1 Then
                        salvaEsterno.Value = "0"
                        MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100% con accompagnamento. Inserire pensione di invalidità lavoro nella sezione PENSIONI e l\'indennità di accompagnamento nella sezione PENSIONI ESENTI!", 0, "Attenzione")
                    ElseIf NOpensCivile = 1 Then
                        salvaEsterno.Value = "0"
                        MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100% con accompagnamento. Inserire pensione di invalidità lavoro nella sezione PENSIONI!", 0, "Attenzione")
                    End If

                    'If RedditoImmobPresente = "1" Then
                    '    par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_AUTONOMO_IMPORTI,UTENZA_REDDITI where (ID_REDD_AUTONOMO=6 or ID_REDD_AUTONOMO=7) AND UTENZA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT=UTENZA_REDDITI.ID AND ID_UTENZA=" & lIdDichiarazione
                    '    Dim myReaderRedd0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '    If myReaderRedd0.Read = False Then
                    '        salvaEsterno.Value = "0"
                    '        MessJQuery("ATTENZIONE...Per coerenza di dati inserire i redditi da fabbricato e/o da terreni!", 0, "Attenzione")
                    '    End If
                    '    myReaderRedd0.Close()
                    'End If

                    If N_INV_100_NO_ACC >= 1 Then
                        par.cmd.CommandText = "SELECT * FROM UTENZA_REDD_PENS_IMPORTI,UTENZA_REDDITI where ID_REDD_PENSIONE=3 AND UTENZA_REDD_PENS_IMPORTI.ID_REDD_TOT=UTENZA_REDDITI.ID AND ID_UTENZA=" & lIdDichiarazione
                        Dim myReaderRedd0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            salvaEsterno.Value = "0"
                            MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100%. Inserire pensione d\'invalidità lavoro nella sezione PENSIONI!", 0, "Attenzione")
                        End If
                        myReaderRedd0.Close()
                    End If

                    If lIdDichImport <> 0 Then
                        risultatoConfrAU = par.ConfrontaAU(lIdDichiarazione, lIdDichImport, descrConfrAU)
                    End If
                    '1=Differenze di reddito
                    '2=Differenze di Patrimonio
                    '3=Differenze di Reddito e Patrimonio
                    '4=Nessuna Differenza
                    Select Case risultatoConfrAU
                        Case 1
                            MessJQuery("ATTENZIONE...Non risultano esserci differenze di patrimonio rispetto alla dichiarazione importata!", 0, "Attenzione")
                        Case 2
                            MessJQuery("ATTENZIONE...Non risultano esserci differenze di reddito rispetto alla dichiarazione importata!", 0, "Attenzione")
                        Case 3

                        Case 4
                            MessJQuery("ATTENZIONE...Non risultano esserci differenze nella scheda redditi nè nella scheda patrimonio rispetto alla dichiarazione importata! ", 0, "Attenzione")
                    End Select
                End If
            End If


            Dim item As MenuItem

            If chInServizio.Checked = True Then
                item = New MenuItem("Autocert.stato di servizio", "AutocertStServ", "", "javascript:AutocertStServ();")
                MenuStampe.Items(0).ChildItems.AddAt(MenuStampe.Items(0).ChildItems.Count - 1, item)
            End If

            Dim Tipo_Ass As String

            If CType(Tab_InfoContratto1.FindControl("cmbTipoRU"), DropDownList).SelectedValue = "ERP" Then
                Tipo_Ass = "1"
            Else
                Tipo_Ass = "0"
            End If


            Dim stringaSpedizione As String = ""
            If cmbComuneSpediz.SelectedItem.Value <> "-1" Then
                stringaSpedizione = stringaSpedizione & " ID_LUOGO_SPEDIZ=" & cmbComuneSpediz.SelectedItem.Value & ", "
            Else
                stringaSpedizione = stringaSpedizione & " ID_LUOGO_SPEDIZ=NULL" & ", "
            End If


            If cmbTipoISped.SelectedItem.Value <> "-1" Then
                stringaSpedizione = stringaSpedizione & " ID_TIPO_IND_SPEDIZ=" & cmbTipoISped.SelectedItem.Value & " "
            Else
                stringaSpedizione = stringaSpedizione & " ID_TIPO_IND_SPEDIZ=NULL" & " "
            End If

            par.cmd.CommandText = "SELECT COUNT(ID) FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderComp.Read() Then
                numComp.Value = myReaderComp(0)
            End If
            myReaderComp.Close()


            Dim sStringaSql As String = ""
            sStringaSql = "UPDATE UTENZA_DICHIARAZIONI SET DATA_DISDETTA_392='" & par.AggiustaData(txtData392.Text) & "',PRESENZA_MIN_15=" & MINORI_15 & ",PRESENZA_MAG_65=" & MAGGIORI_65 & ", COD_CONVOCAZIONE='" & txtCodConvocazione.Text & "',fl_applica_36='1',FL_UBICAZIONE='0', data_decorrenza='" & par.AggiustaData(CType(Tab_InfoContratto1.FindControl("txtDataDec"), TextBox).Text) & "',data_cessazione='" & par.AggiustaData(CType(Tab_InfoContratto1.FindControl("txtDataCessazione"), TextBox).Text) _
                          & "',PG='" & lblPG.Text & "', DATA_PG='" _
                          & par.AggiustaData(txtDataPG.Text) & "',LUOGO='Milano',DATA='" _
                          & par.AggiustaData(txtDataPG.Text) _
                          & "',NOTE_WEB='" & par.PulisciStrSql(CType(Tab_InfoContratto1.FindControl("txtNote"), TextBox).Text) _
                          & "',ID_STATO=" & cmbStato.SelectedItem.Value _
                          & ",N_COMP_NUCLEO=" & numComp.Value & ",N_INV_100_CON=" & N_INV_100_ACC _
                          & ",N_INV_100_SENZA=" & N_INV_100_NO_ACC _
                          & ",N_INV_100_66=" & N_INV_100_66 _
                          & ",ANNO_SIT_ECONOMICA=" & cmbAnnoReddituale.SelectedValue _
                          & ",LUOGO_S='Milano',DATA_S='" _
                          & "',LUOGO_INT_ERP='Milano',DATA_INT_ERP=''," _
                          & "MINORI_CARICO='" & Val(CType(Dic_Reddito1.FindControl("txtMinori"), TextBox).Text) & "',TIPO_ASS='" & Tipo_Ass & "',ALLOGGIO='" _
                          & par.PulisciStrSql(txtAlloggio.Text) _
                          & "',PIANO='" & par.PulisciStrSql(txtPiano.Text) _
                          & "',POSIZIONE='" & par.PulisciStrSql(txtPosizione.Text) _
                          & "',RAPPORTO='" & CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text _
                          & "',RAPPORTO_REALE=''," _
                          & "SCALA='" & par.PulisciStrSql(txtScala.Text) _
                          & "',INT_A='" & CType(Tab_InfoContratto1.FindControl("cmbAbusivo"), DropDownList).SelectedValue _
                          & "',INT_V=''" _
                          & ",INT_C='" & Valore01(chIntestatario.Checked) _
                          & "',INT_M='" & Valore01(chCompNucleo.Checked) _
                          & "',FL_TUTORE='" & Valore01(chRappLegale.Checked) _
                          & "',FL_DELEGATO=''," _
                          & " FL_RIC_POSTA='" & Valore01(chPosta.Checked) _
                          & "', FL_RIC_SINDACATO='" & Valore01(chSindacato.Checked) _
                          & "',FL_COMITATI=" & Valore01(chComitati.Checked) _
                          & ",FL_RIC_DOM=" & Valore01(chDomicilio.Checked) _
                          & ",FL_DA_VERIFICARE='" & flagDaVerific _
                          & "',FL_SOSPENSIONE='" & sospensione _
                          & "',FL_SOSP_1='" & Valore01(CType(Tab_InfoContratto1.FindControl("chVAIN"), CheckBox).Checked) _
                          & "',FL_SOSP_2='" & Valore01(CType(Tab_InfoContratto1.FindControl("chCAIN"), CheckBox).Checked) _
                          & "',FL_SOSP_3='" & Valore01(CType(Tab_InfoContratto1.FindControl("chVerTitol"), CheckBox).Checked) _
                          & "',FL_SOSP_4='" & Valore01(CType(Tab_InfoContratto1.FindControl("chRilascio"), CheckBox).Checked) _
                          & "',FL_SOSP_5='" & Valore01(CType(Tab_InfoContratto1.FindControl("chAMPL"), CheckBox).Checked) _
                          & "',FL_SOSP_6=''" _
                          & ",FL_SOSP_7='" & Valore01(CType(Tab_InfoContratto1.FindControl("chDocManc"), CheckBox).Checked) _
                          & "',COD_ALLOGGIO='" & par.PulisciStrSql(txtPosizione.Text) _
                          & "',FOGLIO='" & par.PulisciStrSql(txtFoglio.Text) _
                          & "',MAPPALE='" & par.PulisciStrSql(txtMappale.Text) _
                          & "',SUB='" & par.PulisciStrSql(txtSub.Text) _
                          & "', TIPO_DOCUMENTO=" & cmbTipoDocumento.SelectedItem.Value & "," _
                          & "CARTA_I='" & par.PulisciStrSql(par.PulisciStrSql(txtCINum.Text)) & "'," _
                          & "CARTA_I_DATA='" & par.AggiustaData(par.PulisciStrSql(txtCIData.Text)) & "'," _
                          & "CARTA_I_RILASCIATA='" & par.PulisciStrSql(txtCIRilascio.Text) & "'," _
                          & "PERMESSO_SOGG_N='" & par.PulisciStrSql(txtPSNum.Text) & "'," _
                          & "PERMESSO_SOGG_DATA='" & par.AggiustaData(par.PulisciStrSql(txtPSData.Text)) & "'," _
                          & "PERMESSO_SOGG_SCADE='" & par.AggiustaData(par.PulisciStrSql(txtPSScade.Text)) & "'," _
                          & "PERMESSO_SOGG_RINNOVO='" & par.AggiustaData(par.PulisciStrSql(txtPSRinnovo.Text)) & "'," _
                          & "CARTA_SOGG_N='" & par.PulisciStrSql(txtCSNum.Text) & "'," _
                          & "FL_NATO_ESTERO='" & Valore01(ChNatoEstero.Checked) & "'," _
                          & "FL_CITTADINO_IT='" & Valore01(ChCittadinanza.Checked) & "'," _
                          & "CARTA_SOGG_DATA='" & par.AggiustaData(par.PulisciStrSql(txtCSData.Text)) & "', " _
                          & "FL_ATTIVITA_LAV='" & cmbLavorativa.SelectedValue & "', " _
                          & "FL_VERIFICA_REDDITO=" & Valore01(CType(Tab_InfoContratto1.FindControl("chRedditi"), CheckBox).Checked) & "," _
                          & "FL_VERIFICA_NUCLEO=" & Valore01(CType(Tab_InfoContratto1.FindControl("chNucleo"), CheckBox).Checked) & "," _
                          & "FL_VERIFICA_PATRIMONIO=" & Valore01(CType(Tab_InfoContratto1.FindControl("chImmob"), CheckBox).Checked) & ", " _
                          & "N_TELEFONO_2='" & par.IfEmpty(txtTelefono.Text, "") & "'," _
                          & "EMAIL='" & par.IfEmpty(txtEmail.Text, "") & "'," _
                          & "INDIRIZZO_SPEDIZIONE='" & par.PulisciStrSql(par.IfEmpty(txtIndirizzo.Text, "")) & "'," _
                          & "CIVICO_SPEDIZIONE='" & par.PulisciStrSql(par.IfEmpty(txtCivicoSpediz.Text, "")) & "'," _
                          & "CAP_SPEDIZ='" & par.PulisciStrSql(par.IfEmpty(txtCapSpediz.Text, "")) & "'," _
                          & "" & stringaSpedizione & "," _
                          & "DATA_PRES_DOCUMENTAZ='" & par.AggiustaData(par.IfEmpty(CType(dic_Documenti1.FindControl("txtDataPresentaz"), TextBox).Text, "")) & "', " _
                          & "COD_TIPO_CONTR='" & par.IfEmpty(CType(Tab_InfoContratto1.FindControl("cmbTipoRU"), DropDownList).SelectedValue, "") & "'," _
                          & "PRESSO_COMUNICAZIONI='" & par.PulisciStrSql(txtPressoIndirizzo.Text) & "',FL_COMUNICAZIONI=" & Valore01(chkRicevePresso.Checked)

            If cmbNazioneRes.SelectedItem.Text = "ITALIA" Then
                sStringaSql = sStringaSql & ",ID_LUOGO_RES_DNTE=" & cmbComuneRes.SelectedItem.Value _
                              & ",ID_TIPO_IND_RES_DNTE=" & cmbTipoIRes.SelectedItem.Value _
                              & ",IND_RES_DNTE='" & par.PulisciStrSql(txtIndRes.Text) _
                              & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(txtCivicoRes.Text) _
                              & "',TELEFONO_DNTE='" & par.PulisciStrSql(txtTelRes.Text) & "' "
            Else
                sStringaSql = sStringaSql & ",ID_LUOGO_RES_DNTE=" & cmbNazioneRes.SelectedItem.Value _
                              & ",ID_TIPO_IND_RES_DNTE=" & cmbTipoIRes.SelectedItem.Value _
                              & ",IND_RES_DNTE='" & par.PulisciStrSql(txtIndRes.Text) _
                              & "',CIVICO_RES_DNTE='" & par.PulisciStrSql(txtCivicoRes.Text) _
                              & "',TELEFONO_DNTE='" & par.PulisciStrSql(txtTelRes.Text) & "' "
            End If

            If cmbNazioneNas.SelectedItem.Text = "ITALIA" Then
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(txtCAPRes.Text) & "',ID_LUOGO_NAS_DNTE=" & cmbComuneNas.SelectedValue
            Else
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(txtCAPRes.Text) & "',ID_LUOGO_NAS_DNTE=" & cmbNazioneNas.SelectedValue
            End If

            If FFOO = True Then
                sStringaSql = sStringaSql & ",FL_IN_SERVIZIO=" & Valore01(chInServizio.Checked)
            End If
            par.cmd.CommandText = sStringaSql & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()


            Dim DIP As Double = 0
            Dim ALT As Double = 0

            par.cmd.CommandText = "SELECT * FROM utenza_redditi WHERE ID_utenza=" & lIdDichiarazione
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader.Read
                DIP = DIP + CDbl(par.IfNull(myReader("DIPENDENTE"), 0)) + CDbl(par.IfNull(myReader("PENSIONE"), 0)) + CDbl(par.IfNull(myReader("NON_IMPONIBILI"), 0))
                ALT = ALT + CDbl(par.IfNull(myReader("OCCASIONALI"), 0)) + CDbl(par.IfNull(myReader("AUTONOMO"), 0)) + CDbl(par.IfNull(myReader("DOM_AG_FAB"), 0))
            End While
            myReader.Close()

            If DIP > ((ALT + DIP) * 80) / 100 Then
                PREVALENTE_DIP = 1
            Else
                PREVALENTE_DIP = 0
            End If

            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET PREVALENTE_DIP = " & PREVALENTE_DIP & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()


            par.cmd.CommandText = "select * from INFO_ISEE_AU where id_istanza=" & lIdDichiarazione
            Dim myReaderIse As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIse.HasRows Then
                par.cmd.CommandText = "UPDATE INFO_ISEE_AU" _
                    & " SET " _
                    & " ISE  = " & par.VirgoleInPunti(Replace(par.IfEmpty(CType(Tab_ISEE1.FindControl("textISE"), TextBox).Text, 0), ".", "")) & "," _
                    & " ISEE = " & par.VirgoleInPunti(Replace(par.IfEmpty(CType(Tab_ISEE1.FindControl("textISEE"), TextBox).Text, 0), ".", "")) & "," _
                    & " ISP  = " & par.VirgoleInPunti(Replace(par.IfEmpty(CType(Tab_ISEE1.FindControl("textISP"), TextBox).Text, 0), ".", "")) & "," _
                    & " NUM_DSU  = '" & Trim(par.IfEmpty(CType(Tab_ISEE1.FindControl("txtProtocolloDSU"), TextBox).Text, "")) & "'," _
                    & " ISR  = " & par.VirgoleInPunti(Replace(par.IfEmpty(CType(Tab_ISEE1.FindControl("textISR"), TextBox).Text, 0), ".", "")) _
                    & " WHERE  ID_ISTANZA = " & lIdDichiarazione
                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "INSERT INTO INFO_ISEE_AU ( ID_ISTANZA, NUM_DSU, ISE, ISEE, ISP, ISR) " _
                 & "  VALUES ( " & lIdDichiarazione & "," _
                 & "'" & Trim(par.IfEmpty(CType(Tab_ISEE1.FindControl("txtProtocolloDSU"), TextBox).Text, "")) & "'," _
                 & par.VirgoleInPunti(par.IfEmpty(Replace(CType(Tab_ISEE1.FindControl("textISE"), TextBox).Text, ".", ""), 0)) & "," _
                 & par.VirgoleInPunti(par.IfEmpty(Replace(CType(Tab_ISEE1.FindControl("textISEE"), TextBox).Text, ".", ""), 0)) & "," _
                 & par.VirgoleInPunti(par.IfEmpty(Replace(CType(Tab_ISEE1.FindControl("textISP"), TextBox).Text, ".", ""), 0)) & "," _
                 & par.VirgoleInPunti(par.IfEmpty(Replace(CType(Tab_ISEE1.FindControl("textISR"), TextBox).Text, ".", ""), 0)) & ")"
                par.cmd.ExecuteNonQuery()
            End If


            CType(Tab_ISEE1.FindControl("textISR"), TextBox).Attributes.Add("onkeypress", "javascript:SostPuntVirg(event,this);")
            CType(Tab_ISEE1.FindControl("textISR"), TextBox).Attributes.Add("onBlur", "javascript:AutoDecimal2(this);")

            If Not IsNothing(par.myTrans) Then
                par.myTrans.Commit()
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            'par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            'myReader = par.cmd.ExecuteReader()
            'myReader.Close()

            bMemorizzato = True

            imgStampa.Enabled = True
            imgStampa.Text = "Stampa"

            txtModificato.Value = "0"
            salvaEsterno.Value = "0"
            If stampaClick.Value = "0" Then
                MessJQuery("Operazione effettuata!", 1, "Avviso")
            End If



        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub btnSalva() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try


    End Sub


    Public Property MINORI_15() As Integer
        Get
            If Not (ViewState("par_MINORI_15") Is Nothing) Then
                Return CInt(ViewState("par_MINORI_15"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_MINORI_15") = value
        End Set

    End Property

    Public Property MAGGIORI_65() As Integer
        Get
            If Not (ViewState("par_MAGGIORI_65") Is Nothing) Then
                Return CInt(ViewState("par_MAGGIORI_65"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_MAGGIORI_65") = value
        End Set

    End Property

    Public Property PREVALENTE_DIP() As Integer
        Get
            If Not (ViewState("par_PREVALENTE_DIP") Is Nothing) Then
                Return CInt(ViewState("par_PREVALENTE_DIP"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_PREVALENTE_DIP") = value
        End Set

    End Property

    Public Property IndiceContratto() As String
        Get
            If Not (ViewState("par_IndiceContratto") Is Nothing) Then
                Return CStr(ViewState("par_IndiceContratto"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_IndiceContratto") = value
        End Set

    End Property


    Function CalcolaStampa()


        Dim DATI_ANAGRAFICI As String
        Dim DATI_NUCLEO As String
        Dim SPESE_SOSTENUTE As String
        Dim PATRIMONIO_MOB As String
        Dim PATRIMONIO_IMMOB As String
        Dim REDDITO_NUCLEO As String
        Dim dichiarante As String
        Dim DATI_DICHIARANTE As String
        Dim REDDITO_IRPEF As String
        Dim REDDITO_DETRAZIONI As String
        Dim ANNO_SIT_ECONOMICA As String
        Dim CAT_CATASTALE As String
        Dim IMMAGINE_A As String
        Dim IMMAGINE_B As String
        Dim IMMAGINE_C As String
        Dim IMMAGINE_C1 As String
        Dim IMMAGINE_D As String
        Dim LUOGO As String = ""
        Dim SDATA As String = ""
        Dim LUOGO_REDDITO As String = ""
        Dim DATA_REDDITO As String = ""
        Dim numero As String
        Dim sStringasql As String
        Dim GIA_TITOLARI As String = ""


        Dim i As Integer

        Try

            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '‘par.cmd.Transaction = par.myTrans

            Dim TestoVirtuali As String = ""
            Dim TestoTutti As String = ""
            Dim TestoDecadenti As String = ""

            Dim CODICEANAGRAFICO As String = ""
            par.cmd.CommandText = "SELECT operatori.*,caf_web.cod_caf as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
            Dim myReaderENTE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderENTE.Read() Then
                CODICEANAGRAFICO = par.IfNull(myReaderENTE("ENTE"), "") & " - " & par.IfNull(myReaderENTE("COD_ANA"), "")
            End If
            myReaderENTE.Close()

            'DATI ISEE
            Dim TestoISEE As String = "<tr>"
            par.cmd.CommandText = "select * from INFO_ISEE_AU where id_istanza=" & lIdDichiarazione
            Dim myReaderIse As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderIse.Read Then
                TestoISEE = TestoISEE & "<td><center><small><small>" & par.IfNull(myReaderIse("NUM_DSU"), "") & "</small></small></center></td>"
                TestoISEE = TestoISEE & "<td><center><small><small>" & Format((CDbl(par.IfNull(myReaderIse("ISP"), 0))), "0.00") & "</small></small></center></td>"
                TestoISEE = TestoISEE & "<td><center><small><small>" & Format((CDbl(par.IfNull(myReaderIse("ISR"), 0))), "0.00") & "</small></small></center></td>"
                TestoISEE = TestoISEE & "<td><center><small><small>" & Format((CDbl(par.IfNull(myReaderIse("ISE"), 0))), "0.00") & "</small></small></center></td>"
                TestoISEE = TestoISEE & "<td><center><small><small>" & Format((CDbl(par.IfNull(myReaderIse("ISEE"), 0))), "0.00") & "</small></small></center></td>"
            End If
            myReaderIse.Close()
            TestoISEE = TestoISEE & "</tr>"

            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta WHERE ID=57"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                TestoVirtuali = par.IfNull(myReader("VALORE"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta WHERE ID=58"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                TestoTutti = par.IfNull(myReader("VALORE"), "")
            End If
            myReader.Close()

            par.cmd.CommandText = "select * from siscom_mi.parametri_bolletta WHERE ID=59"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                TestoDecadenti = par.IfNull(myReader("VALORE"), "")
            End If
            myReader.Close()

            If cmbNazioneNas.SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & txtCognome.Text & "</I>   " _
                                & ", NOME:   <I>" & txtNome.Text & "</I><BR>" _
                                & "NATO A:   <I>" & cmbComuneNas.SelectedItem.Text & "</I>   , " _
                                & "PROVINCIA:   <I>" & cmbPrNas.SelectedItem.Text & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & txtDataNascita.Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & txtTelRes.Text & "</I><BR>"

            Else
                DATI_ANAGRAFICI = "<BR>   COGNOME:   <I>" & txtCognome.Text & "</I>   " _
                                & ", NOME:   <I>" & txtNome.Text & "</I><BR>" _
                                & "STATO ESTERO:   <I>" & cmbNazioneNas.SelectedItem.Text & "</I>" _
                                & "<BR>" _
                                & "DATA DI NASCITA:   <I>" & txtDataNascita.Text & "</I>   , " _
                                & "pref. e n. telefonico (facoltativo):   <I>" & txtTelRes.Text & "</I><BR>"


            End If


            Dim INDIRIZZOSTAMPA As String = ""

            If cmbNazioneRes.SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & cmbComuneRes.SelectedItem.Text & "</I>   , " _
                & "PROVINCIA:   <I>" & cmbPrRes.SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & cmbTipoIRes.SelectedItem.Text & " " & txtIndRes.Text & "</I>   ," _
                & "N. CIVICO:   <I>" & txtCivicoRes.Text & "</I>   , CAP:   <I>" & txtCAPRes.Text & "</I>"
            Else
                Dim COMUNERES As String = ""
                If cmbNazioneRes.SelectedItem.Text <> "ITALIA" Then
                    COMUNERES = "---"
                Else
                    COMUNERES = cmbComuneRes.SelectedItem.Text
                End If
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & COMUNERES & "</I>   , " _
                & "STATO ESTERO:   <I>" & cmbNazioneRes.SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & cmbTipoIRes.SelectedItem.Text & " " & txtIndRes.Text & "</I>   ," _
                & "N. CIVICO:   <I>" & txtCivicoRes.Text & "</I>   , CAP:   <I>" & txtCAPRes.Text & "</I>"
            End If
            INDIRIZZOSTAMPA = "INDIRIZZO:" & cmbTipoIRes.SelectedItem.Text & " " & txtIndRes.Text & ", " & txtCivicoRes.Text & " CAP:" & txtCAPRes.Text

            If par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") <> "" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/>COD RAPPORTO DI UTENZA: <I>" & par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") & "</I>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/>COD CONVOCAZIONE: <I>" & txtCodConvocazione.Text & "</I>"
            End If

            DATI_ANAGRAFICI = DATI_ANAGRAFICI & " - TIPOLOGIA: <i>" & par.IfEmpty(CType(Tab_InfoContratto1.FindControl("cmbTipoRU"), DropDownList).SelectedValue, "") & "</i><BR>"

            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR><b>DATI RELATIVI ALL'ALLOGGIO:</b><BR>"
            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>FOGLIO: " & txtFoglio.Text & "&nbsp; MAPPALE: " & txtMappale.Text & "&nbsp; SUB: " & txtSub.Text
            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>SCALA: " & txtScala.Text & "&nbsp; PIANO: " & txtPiano.Text & "&nbsp; N.ALLOGGIO/INTERNO: " & txtAlloggio.Text

            If Valore01(chIntestatario.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR><img src=block_checked.gif width=10 height=10 border=1>Intestatario di Contratto"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR><img src=block.gif width=10 height=10 border=1>Intestatario di Contratto"
            End If

            'If Valore01(CType(Dic_Utenza1.FindControl("ch2"), CheckBox).Checked) = "1" Then
            '    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Utente in corso di Voltura"
            'Else
            '    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Utente in corso di Voltura"
            'End If

            If CType(Tab_InfoContratto1.FindControl("cmbAbusivo"), DropDownList).SelectedValue = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Occupante Abusivo"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Occupante Abusivo"
            End If

            If Valore01(chCompNucleo.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Altro Componente Maggiorenne"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Altro Componente Maggiorenne"
            End If

            If Valore01(chRappLegale.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Rapp. Legale"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Rapp. Legale"
            End If

            If Valore01(chSindacato.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Presentata da Sindacato"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Presentata da Sindacato"
            End If

            If Valore01(chComitati.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Presentata da Comitati"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Presentata da Comitati"
            End If

            If Valore01(chPosta.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Presentata tramite posta"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Presentata tramite posta"
            End If
            If Valore01(chDomicilio.Checked) = "1" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block_checked.gif width=10 height=10 border=1>Presentata tramite visita a domicilio"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "&nbsp;-&nbsp;<img src=block.gif width=10 height=10 border=1>Presentata tramite visita a domicilio"
            End If

            DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/><b>ESTREMI DOCUMENTO DI RICONOSCIMENTO</b><br/> "

            If cmbNazioneNas.SelectedItem.Text <> "ITALIA" And vcomunitario.Value = "1" And ChCittadinanza.Checked = False Then
                If txtPSNum.Text <> "" Then
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "PERMESSO DI SOGGIORNO N.: <i>" & txtPSNum.Text & "</i> Data Rilascio: <i>" & txtPSData.Text & "</i>"
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI & " Data Scadenza: <i>" & txtPSScade.Text & "</i> Data Rinnovo : <i>" & txtPSRinnovo.Text & "</i><br/>"
                Else
                    DATI_ANAGRAFICI = DATI_ANAGRAFICI & "CARTA DI SOGGIORNO N.: <i>" & txtCSNum.Text & "</i> Data Rilascio: <i>" & txtCSData.Text & "</i><br/>"
                End If
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & cmbTipoDocumento.SelectedItem.Text & " N.: <i>" & txtCINum.Text & "</i> Data Rilascio: <i>" & txtCIData.Text & "</i> Rilasciato da: <i>" & txtCIRilascio.Text & "</i><br/>"
            End If

            If chkRicevePresso.Checked = True Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<br/><b>INDIRIZZO COMUNICAZIONI</b><br/> "
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "INDIRIZZO:   <I>" & UCase(cmbTipoISped.SelectedItem.Text) & " " & UCase(txtIndirizzo.Text) & "</I>   ," _
                & "N. CIVICO:   <I>" & UCase(txtCivicoSpediz.Text) & "</I>   , CAP:   <I>" & UCase(txtCapSpediz.Text) & "</I>   , PROVINCIA: <I>" & UCase(cmbPrSpediz.SelectedItem.Text) & "</I>   , COMUNE: <I>" & UCase(cmbComuneSpediz.SelectedItem.Text) & "</I>   , PRESSO: <I>" & UCase(txtPressoIndirizzo.Text) & "</I>"
            End If



            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>COD. CONTRATTO: <I>" & CType(Dic_Utenza1.FindControl("txtcodALLOGGIO"), TextBox).Text & "</I>"
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>POSIZIONE: <I>" & CType(Dic_Utenza1.FindControl("txtPosizione"), TextBox).Text & "</I>"
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>RAPPORTO: <I>" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "</I>"
            '            If CType(Dic_Utenza1.FindControl("R1"), RadioButton).Checked = True Then
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>TIPOLOGIA: <I>ERP</I><BR>"
            'Else
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>TIPOLOGIA: <i>EC</i><BR>"
            'End If
            'DATI_ANAGRAFICI = DATI_ANAGRAFICI & "<BR>RAPPORTO REALE: <I>" & CType(Dic_Utenza1.FindControl("txtRapportoReale"), TextBox).Text & "</I>"
            DATI_NUCLEO = ""

            For Each row As Data.DataRow In dtComp.Rows
                DATI_NUCLEO = DATI_NUCLEO & "<TR>" _
                            & "<TD width=5%><small><small>    <center>" & i & "</center></small></small></TD>" _
                            & "<TD width=20%><small><small>   <I>" & par.IfNull(row.Item("COD_FISCALE"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=20%><small><small>   <I>" & par.IfNull(row.Item("COGNOME"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("NOME"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.FormattaData(par.IfNull(row.Item("DATA_NASCITA"), "")) & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("GRADO_PARENTELA"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("PERC_INVAL"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("INDENNITA_ACC"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("USL"), "") & "</I>   </small></small></TD>" _
                            & "</TR>"
                i = i + 1
            Next

            SPESE_SOSTENUTE = ""
            ' ''For i = 0 To CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items.Count - 1
            ' ''    If Val(par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6)) > 0 Then

            ' ''        SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
            ' ''                        & "<td width=50%><small><small><CENTER>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 1, 52) & "</CENTER></small></small></td>" _
            ' ''                        & "<td align=right width=50%><small><small>   <I>" & par.RicavaTesto(CType(Dic_Nucleo1.FindControl("listbox2"), ListBox).Items(i).Text, 54, 6) & ",00" & "</I></small></small></td>" _
            ' ''                        & "</tr>"
            ' ''    End If
            ' ''Next i

            ANNO_SIT_ECONOMICA = cmbAnnoReddituale.SelectedValue

            PATRIMONIO_MOB = ""
            For Each row As Data.DataRow In dtMob.Rows
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                               & "<TR>" _
                               & "<TD width=25%><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></TD>" _
                               & "<TD  width=25%><small><small>   <I>" & par.IfNull(row.Item("COD_INTERMEDIARIO"), "") & "</I>   </small></small></TD>" _
                               & "<TD width=50%><small><small>   <I>" & par.IfNull(row.Item("INTERMEDIARIO"), "") & "</I>   </small></small></TD>" _
                               & "<TD  align=right  width=50%><small><small>   <I>" & par.IfNull(row.Item("IMPORTO"), "") & ",00</I></small></small></TD>" _
                               & "<TD  align=right  width=50%><small><small>   <I>" & par.IfNull(row.Item("PROPRIETA"), "") & "</I></small></small></TD>" _
                               & "</TR>"
            Next

            PATRIMONIO_IMMOB = ""
            For Each row As Data.DataRow In dtImmob.Rows
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                                   & "<TR>" _
                                   & "<TD><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("TIPO_IMMOB"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small><p align=right>   <I>" & par.IfNull(row.Item("PERC_PATR_IMMOBILIARE"), "") & "</I>   %   </p></small></small></TD>" _
                                   & "<TD><small><small><p align=right>   <I>" & par.IfNull(row.Item("VALORE"), "") & "</I>   </p></small></small></TD>" _
                                   & "<TD><small><small><p align=right>   <I>" & par.IfNull(row.Item("MUTUO"), "") & "</I>   </p></small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.IfNull(row.Item("CAT_CATASTALE"), "") & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.IfNull(row.Item("COMUNE"), "") & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.IfNull(row.Item("N_VANI"), "") & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.IfNull(row.Item("SUP_UTILE"), "") & "</center><I></I>   </small></small></TD>" _
                                   & "<TD><small><small><I></I><center>" & par.IfNull(row.Item("VENDUTO"), "") & "</center><I></I>   </small></small></TD>" _
                                   & "</TR>"
            Next
            If PATRIMONIO_IMMOB <> "" Then
                'Modifiche Anagrafe Utenza
                'If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True Then
                '    CAT_CATASTALE = CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Text
                'Else
                '    CAT_CATASTALE = ""
                'End If
            Else
                CAT_CATASTALE = ""
            End If

            ' ''REDDITO_NUCLEO = ""
            ' ''For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
            ' ''    REDDITO_NUCLEO = REDDITO_NUCLEO _
            ' ''    & "<TR>" _
            ' ''    & "<TD><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 35) & "</I>   </center></small></small></TD>" _
            ' ''    & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) & ",00</I>   </small></small></p></TD>" _
            ' ''    & "<TD><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ",00</I>   </small></small></p></TD>" _
            ' ''    & "</TR>"
            ' ''Next i


            REDDITO_NUCLEO = ""
            For Each row As Data.DataRow In dtRedd.Rows
                'REDDITO_NUCLEO = REDDITO_NUCLEO _
                '                   & "<TR>" _
                '                   & "<TD><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("COD_FISCALE"), "") & "</I>   </small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & Format(par.IfNull(row.Item("DIP1"), 0) + par.IfNull(row.Item("DIP2"), 0) + par.IfNull(row.Item("DIP3"), 0) + par.IfNull(row.Item("DIP4"), 0) + par.IfNull(row.Item("DIP5"), 0) + par.IfNull(row.Item("DIP6"), 0) + par.IfNull(row.Item("DIP7"), 0), "0.00") & "</I>   </small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & Format(par.IfNull(row.Item("PENS1"), 0) + par.IfNull(row.Item("PENS2"), 0) + par.IfNull(row.Item("PENS3"), 0) + par.IfNull(row.Item("PENS4"), 0) + par.IfNull(row.Item("PENS5"), 0) + par.IfNull(row.Item("PENS6"), 0), "0.00") & "</I>   </small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & Format(par.IfNull(row.Item("PENS_ES1"), 0) + par.IfNull(row.Item("PENS_ES2"), 0) + par.IfNull(row.Item("PENS_ES3"), 0) + par.IfNull(row.Item("PENS_ES4"), 0), "0,00") & "</I>   </small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & Format(par.IfNull(row.Item("AUTON1"), 0) + par.IfNull(row.Item("AUTON2"), 0) + par.IfNull(row.Item("AUTON3"), 0) + par.IfNull(row.Item("AUTON4"), 0) + par.IfNull(row.Item("AUTON5"), 0) + par.IfNull(row.Item("AUTON6"), 0) + par.IfNull(row.Item("AUTON7"), 0) + par.IfNull(row.Item("AUTON8"), 0) + par.IfNull(row.Item("AUTON9"), 0), "0,00") & "</I>   </small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & Format(par.IfNull(row.Item("NO_ISEE1"), 0) + par.IfNull(row.Item("NO_ISEE2"), 0), "0,00") & "</I>   </small></small></TD>" _
                '                   & "<TD><small><small>   <I>" & Format(par.IfNull(row.Item("TOT_REDDITI"), 0), "0,00") & "</I>   </small></small></TD>" _
                '                   & "</TR>"
                REDDITO_NUCLEO = REDDITO_NUCLEO _
                                   & "<TR>" _
                                   & "<TD><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("COD_FISCALE"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("DIPENDENTE"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("PENSIONE"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("PENSIONE2"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("AUTONOMO1"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("NO_ISEE"), "") & "</I>   </small></small></TD>" _
                                   & "<TD><small><small>   <I>" & par.IfNull(row.Item("TOT_REDDITI"), "") & "</I>   </small></small></TD>" _
                                   & "</TR>"
            Next

            IMMAGINE_A = "<img src=block_checked.gif width=10 height=10 border=1>"
            IMMAGINE_B = "<img src=block_checked.gif width=10 height=10 border=1>"

            If PATRIMONIO_MOB <> "" Then
                IMMAGINE_C = "<img src=block_checked.gif width=10 height=10 border=1>"
            Else
                IMMAGINE_C = "<img src=block.gif width=10 height=10 border=1>"
            End If

            If PATRIMONIO_IMMOB <> "" Then
                IMMAGINE_C1 = "<img src=block_checked.gif width=10 height=10 border=1>"
            Else
                IMMAGINE_C1 = "<img src=block.gif width=10 height=10 border=1>"
            End If

            If REDDITO_NUCLEO <> "" Then
                IMMAGINE_D = "<img src=block_checked.gif width=10 height=10 border=1>"
            Else
                IMMAGINE_D = "<img src=block.gif width=10 height=10 border=1>"
            End If

            LUOGO = "Milano"

            DATI_DICHIARANTE = "<BR></BR>"
            'End If

            REDDITO_IRPEF = ""
            ' ''For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
            ' ''    REDDITO_IRPEF = REDDITO_IRPEF _
            ' ''    & "<TR>" _
            ' ''    & "<TD width=40%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 50) & "</I>   </center></small></small></TD>" _
            ' ''    & "<TD  width=505%><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 52, 8) & ",00</I>   </p></small></small></TD>" _
            ' ''    & "</TR>"
            ' ''Next i






            REDDITO_DETRAZIONI = ""
            For Each row As Data.DataRow In dtDetraz.Rows
                REDDITO_DETRAZIONI = REDDITO_DETRAZIONI _
                & "<TR>" _
                & "<TD width=25%><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></TD>" _
                & "<TD  width=25%><small><small>   <I>" & par.IfNull(row.Item("DESCRIZIONE"), "") & "</I>   </center></small></small></TD>" _
                & "<TD  width=25%><small><small><p align=right>   <I>" & par.IfNull(row.Item("IMPORTO"), "") & "</I>   </p></small></small></TD>" _
                & "</TR>"
            Next

            LUOGO_REDDITO = "Milano"



            numero = lblPG.Text & " del " & Format(Now, "dd/MM/yyyy")


            sStringasql = "<HTML><HEAD><TITLE>Finestra di Stampa</TITLE>"
            sStringasql = sStringasql & "<style type='text/css'> .style2 { font-family: Arial; font-size: medium; font-weight: bold; } .style3 { font-family: Arial; font-size: small; } .style4 { font-family: Arial; font-weight: bold; font-size: small; } .style5 { font-family: Arial; font-weight: bold; font-size: small; font-style: italic; } .style6 { font-family: Arial; font-size: small; height: 16px; } .style7 { font-family: Arial; font-weight: bold; font-size: xx-small; }    </style>"
            sStringasql = sStringasql & "</HEAD><BODY><UL><UL><NOBR></NOBR><basefont SIZE=1></UL></UL>"
            sStringasql = sStringasql & "<img border='0' src='..\IMG\logo.gif' width='166' height='104'><CENTER><B><p style='font-family: arial; font-size: 16pt; font-weight: bold'>DICHIARAZIONE SOSTITUTIVA </p><p style='font-family: arial; font-size: 14pt;'>ai sensi degli artt. 46 e 47 D.P.R. 445 del 28/12/2000<br/>Il sottoscritto, consapevole delle responsabilita' e delle sanzioni penali previste dagli artt. 75 e 76 del D.P.R. n. 445/2000 per chi rende false attestazioni e dichiarazioni mendaci, sotto la propria personale responsabilita'</p><br/><p style='font-family: arial; font-size: 16pt; font-weight: bold'>DICHIARA</p></b></CENTER>   "
            sStringasql = sStringasql & "<center><table border=1 cellspacing=0 width=95%><tr><td><medium>   <B>DATI ANAGRAFICI DEL RICHIEDENTE</B><BR>"
            sStringasql = sStringasql & DATI_ANAGRAFICI & "<br></medium></td></tr></table></center>"
            sStringasql = sStringasql & "<BR><UL>   </UL><NOBR></NOBR><center>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=95%><tr><td><br><medium>   SOGGETTI COMPONENTI IL NUCLEO FAMILIARE: richiedente"
            sStringasql = sStringasql & " componenti la famiglia anagrafica e altri soggetti considerati a carico ai fini IRPEF"
            sStringasql = sStringasql & "<BR>"
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & "<center>"
            sStringasql = sStringasql & "</medium>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=90%><tr><td>"
            sStringasql = sStringasql & "<p align='center'><small>A</small></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><small>B</small></p>"
            sStringasql = sStringasql & "</td><td colspan=2>"
            sStringasql = sStringasql & "<p align='center'><small>C</small></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><small>D</small></p>"
            sStringasql = sStringasql & "</td><td>"
            sStringasql = sStringasql & "<p align='center'><small>E</small></td><td>"
            sStringasql = sStringasql & "<p align='center'><small>F</small></td><td>"
            sStringasql = sStringasql & "<p align='center'><small>G</small></td><td>"
            sStringasql = sStringasql & "<p align='center'><small>H</small></p>"
            sStringasql = sStringasql & "</td></tr>   <small>   <tr><td bgcolor=#C0C0C0><center><small><small>N.Progr.</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE FISCALE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>COGNOME</small></small></center></td><td   bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>DATA DI NASCITA</small></small></center></td>"
            sStringasql = sStringasql & "</small>"
            sStringasql = sStringasql & "<td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><small><small><small>GR. PARENTELA</small></small></small></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><small><small><small>&nbsp;% INVALIDITA'</small></small></small></td><td bgcolor=#C0C0C0>"
            sStringasql = sStringasql & "<p align='center'><small><small><small>INDENNITA' ACC.</small></small></small></td>   <td bgcolor=#C0C0C0><small><small><small>ASL&nbsp;</small></small></small></td></tr><UL><UL>   <NOBR></NOBR>"
            sStringasql = sStringasql & DATI_NUCLEO
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</table></center>"
            sStringasql = sStringasql & "<BR><UL>   <NOBR></NOBR><p style='font-family: ARIAL; font-size: 14pt;'>   <B>Altre informazioni sul nucleo familiare</B><BR></p>"
            sStringasql = sStringasql & "<p><p style='font-family: ARIAL; font-size: 14pt;'>Nel nucleo famigliare del richiedente <b>" & GIA_TITOLARI & "</b>"
            sStringasql = sStringasql & " titolari di un contratto di assegnazione di alloggio di edilizia residenziale pubblica<BR>"
            sStringasql = sStringasql & "</p></p>"
            sStringasql = sStringasql & "<table cellspacing=0 border=0 width=90%><tr><td height=18 width=35% ><p style='font-family: ARIAL; font-size: 14pt;'>   - nel nucleo familiare sono presenti n.   </p></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right><p style='font-family: ARIAL; font-size: 14pt;'>   <I>" & N_INV_100_ACC & "</I>   </p></td></tr></table></td><td width=50% ></p><p style='font-family: ARIAL; font-size: 14pt;'>   componenti con invalidit&agrave; al 100% (con indennit&agrave; di accompagnamento)   </p></td></tr><tr><td><p style='font-family: ARIAL; font-size: 14pt;'><CENTER><p style='font-family: ARIAL; font-size: 14pt;'>Spese effettivamente sostenute distinte per componente</p><table border=1 cellpadding=0 cellspacing=0 width=50%>   <tr><td width=50%><small><CENTER><b>A</b></CENTER></small></td><td align=right width=50%><small><CENTER><b>B</b></CENTER></small></td></tr>   <tr><td bgcolor=#C0C0C0 width=50%><CENTER><small><small>Nome</small></small></small></CENTER></td><small>   <td bgcolor=#C0C0C0 align=right width=50%><small><small><CENTER>SPESA</CENTER></small></small></td></tr><UL><UL>   <NOBR></NOBR>" & SPESE_SOSTENUTE & "</UL></UL>   <NOBR></NOBR></table></CENTER></td><td>&nbsp;</td><td>&nbsp;<BR>"
            sStringasql = sStringasql & "</p></td></tr><tr><td height=18 width=30% ><p style='font-family: ARIAL; font-size: 14pt;'>   - nel nucleo sono presenti n.   </p></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I> <p style='font-family: ARIAL; font-size: 14pt;'>   " & N_INV_100_NO_ACC & "</p></I>   </p></td></tr></table></td><td width=55% ><p style='font-family: ARIAL; font-size: 14pt;'>   componenti con invalidit&agrave; al 100% senza indennit&agrave; di accompagnamento<BR>"
            sStringasql = sStringasql & "</p></td></tr><tr><td height=18 width=30% ><p style='font-family: ARIAL; font-size: 14pt;'>   - nel nucleo sono presenti n.   </p></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><p style='font-family: ARIAL; font-size: 14pt;'>" & N_INV_100_66 & "</p></I>   </p></td></tr></table></td><td width=55% ><p style='font-family: ARIAL; font-size: 14pt;'>   componenti con invalidit&agrave; inferiore al 100% e superiore al 66%<BR>"
            sStringasql = sStringasql & "</p></td></tr></table>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=95%><tr><td><medium><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </medium><br><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><medium><BR>"
            sStringasql = sStringasql & "<B>SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   " _
                & "<B><UL><BR>ISEE ORDINARIO</B>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>PROTOCOLLO DSU</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>ISP</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>ISR</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>ISE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>ISEE</small></small></center></td></tr>" & TestoISEE & "</table></center>" _
                & "<B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>posseduto alla data del 31 dicembre " & ANNO_SIT_ECONOMICA & "&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td></tr>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE INTERMEDIARIO O GESTORE</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>INTERMEDIARIO O GESTORE (indicare se &egrave;  Banca"
            sStringasql = sStringasql & " Posta"
            sStringasql = sStringasql & " SIM"
            sStringasql = sStringasql & " SGR"
            sStringasql = sStringasql & " Impresa di investimento comunitaria o extracomunitaria"
            sStringasql = sStringasql & " Agente di cambio"
            sStringasql = sStringasql & " ecc.)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>VALORE INVESTIMENTO</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>N.INTESTATARI</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>posseduto alla data del 31"
            sStringasql = sStringasql & " Dicembre " & ANNO_SIT_ECONOMICA
            sStringasql = sStringasql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td><td><center>G</center></td><td><center>H</center></td><td><center>I</center></td><td><center>L</center></td><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TIPO DI PATRIMONIO  IMMOBILIARE</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>QUOTA POSSEDUTA (percentuale)</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
            sStringasql = sStringasql & " come definita ai fini ICI)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>CAT.CATASTALE</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>COMUNE</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>VANI</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>SUP.UTILE</small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>VENDUTO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"
            sStringasql = sStringasql & "</table></center><br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=90%><tr><td width=80%><p align=right><small>      </small></p></td><td width=10% style='border: thin solid rgb(0"
            sStringasql = sStringasql & " 0"
            sStringasql = sStringasql & " 0)'><small><p align=center>   <I>" & CAT_CATASTALE & "</I>   </p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><medium>   <B>REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td><td><center>G</center></td><td><center>H</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>COD. FISCALE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>DIPENDENTE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>PENSIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>PENSIONE ESENTE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>AUTONOMO</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>ASSEGN. MAN. FIGLI E ONERI</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TOT. REDDITI</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
            sStringasql = sStringasql & "</table></center><br>"
            sStringasql = sStringasql & "</B>" & dichiarante & "<BR>"
            sStringasql = sStringasql & DATI_DICHIARANTE
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</small></td></tr></table></center><BR>"
            sStringasql = sStringasql & "</center>"
            sStringasql = sStringasql & "<BR><center><table  cellspacing=0 border=1 width=95%><tr><td><medium>   <B>INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ERP</B><BR>"
            sStringasql = sStringasql & "<B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>TIPO DETRAZIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>IMPORTO DETRAZIONE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center><BR>"
            sStringasql = sStringasql & "<BR><BR><CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I>" & LUOGO_REDDITO & "</I>   </center></small></td><td width=33%><small><center>"
            sStringasql = sStringasql & " li   <I>" & DATA_REDDITO & "</I>   </center></small></td><td width=34%><center></center></td></tr><tr><td width=33% height=15><small><center>(luogo)</center></small></td><td width=33%><small><center>(data)</center></small></td><td width=34%><small><center></center></small></td></tr></table></CENTER><BR>"
            sStringasql = sStringasql & "</small>"
            sStringasql = sStringasql & "</table>"
            sStringasql = sStringasql & "</br>"
            sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<p align='left'><img border='0' src='..\IMG\logo.gif' width='166' height='104'></p>"

            If ANNO_SIT_ECONOMICA <> "" Then
                sStringasql = sStringasql & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ")</p></font></p>"
            Else
                sStringasql = sStringasql & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA 2007 (Redditi 2006)</p></font></p>"
            End If

            If par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") <> "" Then
                sStringasql = sStringasql & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & txtCognome.Text & "   " _
                    & " " & txtNome.Text & "</I> - COD. RAPPORTO UTENZA: " & par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") & " " & INDIRIZZOSTAMPA & "</p>"

            Else
                sStringasql = sStringasql & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & txtCognome.Text & "   " _
                & " " & txtNome.Text & "</I> - COD. CONVOCAZIONE: " & txtCodConvocazione.Text & " " & INDIRIZZOSTAMPA & "</p>"

            End If
            '

            'sStringasql = sStringasql & CalcolaRedditoDatabase(lIdDichiarazione)
            sStringasql = sStringasql & "</BR>"
            'sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
            Dim sCalcoloISEE As String = ""


            sCalcoloISEE = CalcolaIsee(lIdDichiarazione)
            sStringasql = sStringasql & sCalcoloISEE

            Dim ISEE_DECADENZA As String = ""
            Dim PATRIMONIO_DECADENZA As String = ""
            CalcolaISEEDecadenza(lIdDichiarazione, ISEE_DECADENZA, PATRIMONIO_DECADENZA)


            Dim ss1 As String = ""
            If ANNO_SIT_ECONOMICA <> "" Then
                ss1 = ss1 & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ")</p></font></p>"
            Else
                ss1 = ss1 & "<p align='center'><font face='Arial'><p style='font-family: ARIAL; font-size: 16pt;'>ANAGRAFE UTENZA 2007 (Redditi 2006)</p></font></p>"
            End If

            If par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") <> "" Then
                ss1 = ss1 & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & txtCognome.Text & "   " _
                    & " " & txtNome.Text & "</I> - COD. RAPPORTO UTENZA: " & par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") & " " & INDIRIZZOSTAMPA & "</p>"
            Else
                ss1 = ss1 & "<p align='left' style='font-family: ARIAL; font-size: 16pt;'><BR>NOMINATIVO: <I>" & txtCognome.Text & "   " _
                    & " " & txtNome.Text & "</I> - COD. CONVOCAZIONE: " & txtCodConvocazione.Text & " " & INDIRIZZOSTAMPA & "</p>"
            End If

            ss1 = ss1 & "</BR>"

            sCalcoloISEE = ss1 & sCalcoloISEE

            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & " &nbsp;"
            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & "<b><p style='font-family: ARIAL; font-size: 14pt;'>Il sottoscritto attesta di essere a conoscenza che sui dati dichiarati potranno essere eseguiti controlli sulla veridicita' degli stessi ai sensi dell’articolo 71 del Dpr 445/2000 e da parte della Guardia di Finanza presso gli istituti di credito o altri intermediari finanziari, ai sensi dell’art.4, comma 10 D.Lgs. 109/1998 e dell’art.6 D.P.C.M. 221/1999</b><br/><br/>"
            sStringasql = sStringasql & " Dic.N. " & numero & "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Firma del Dichiarante"
            sStringasql = sStringasql & "<br>Elaborata da: " & CODICEANAGRAFICO & "</p>"

            Dim sStringasql1 As String = ""
            If rdApplica.Checked = True And rdApplica.Visible = True Then
                sStringasql1 = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                 & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                 & "'F132','APPLICAZIONE LG 36/2008','I')"
            Else
                sStringasql1 = "INSERT INTO UTENZA_EVENTI_DICHIARAZIONI (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F132','','I')"
            End If
            par.cmd.CommandText = sStringasql1
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET ISEE='" & ISEE_DICHIARAZIONE & "',ISEE_DEC='" & ISEE_DECADENZA & "',PATRIMONIO_DEC='" & PATRIMONIO_DECADENZA & "' WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE UTENZA_DICHIARAZIONI SET PREVALENTE_DIP = " & PREVALENTE_DIP & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            'simulazione canone 392
            If RU392.Value = "1" Or RU431.Value = "1" Then
                If Mid(par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, ""), 1, 6) <> "000000" And Mid(par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, ""), 1, 2) <> "41" And Mid(par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, ""), 1, 2) <> "42" And Mid(par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, ""), 1, 2) <> "43" Then
                    Dim ISTAT_1_PR As Double = 0
                    Dim ISTAT_2_PR As Double = 0
                    Dim ISTAT_1_AC As Double = 0
                    Dim ISTAT_2_AC As Double = 0
                    Dim ISTAT_1_PE As Double = 0
                    Dim ISTAT_2_PE As Double = 0
                    Dim ISTAT_1_DE As Double = 0
                    Dim ISTAT_2_DE As Double = 0

                    Dim ICI_1_2 As Double = 0
                    Dim ICI_3_4 As Double = 0
                    Dim ICI_5_6 As Double = 0
                    Dim ICI_7 As Double = 0

                    Dim LimiteA4 As Double = 0
                    Dim LimiteA5 As Double = 0
                    Dim InizioB1 As Double = 0
                    Dim InizioC12 As Double = 0

                    Dim CanoneMinimoA5 As Double = 0
                    Dim Perc_Inc_ISE_A5 As Double = 0
                    Dim Perc_Inc_Loc_A5 As Double = 0

                    Dim CanoneMinimoB1 As Double = 0
                    Dim Perc_Inc_ISE_B1 As Double = 0
                    Dim Perc_Inc_Loc_B1 As Double = 0

                    Dim CanoneMinimoC12 As Double = 0
                    Dim Perc_Inc_ISE_C12 As Double = 0
                    Dim Perc_Inc_Loc_C12 As Double = 0

                    Dim CanoneMinimoD4 As Double = 0
                    Dim Perc_Inc_ISE_D4 As Double = 0
                    Dim Perc_Inc_Loc_D4 As Double = 0

                    Dim LimitePensioneAU As Double = 0
                    Dim AnnoAU As String = ""
                    Dim AnnoRedditi As String = ""
                    Dim InizioCanone As String = ""
                    Dim FineCanone As String = ""
                    Dim TipoProvenienza As Integer = 0
                    Dim IDAU As Long = 0

                    par.cmd.CommandText = "select utenza_bandi.* from utenza_bandi,utenza_dichiarazioni where utenza_bandi.id=utenza_dichiarazioni.id_bando and utenza_dichiarazioni.id=" & lIdDichiarazione
                    Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderX.Read() Then
                        IDAU = myReaderX("ID")
                        AnnoAU = myReaderX("anno_au")
                        AnnoRedditi = myReaderX("anno_isee")
                        InizioCanone = myReaderX("inizio_canone")
                        FineCanone = myReaderX("fine_canone")
                        TipoProvenienza = myReaderX("ID_TIPO_PROVENIENZA")
                    End If
                    myReaderX.Close()

                    par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI_PARAMETRI WHERE ID_AU=" & IDAU
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        ISTAT_1_PR = myReader("ISTAT_1_PR")
                        ISTAT_2_PR = myReader("ISTAT_2_PR")

                        ISTAT_1_AC = myReader("ISTAT_1_AC")
                        ISTAT_2_AC = myReader("ISTAT_2_AC")

                        ISTAT_1_PE = myReader("ISTAT_1_PE")
                        ISTAT_2_PE = myReader("ISTAT_2_PE")

                        ISTAT_1_DE = myReader("ISTAT_1_DE")
                        ISTAT_2_DE = myReader("ISTAT_2_DE")

                        ICI_1_2 = myReader("ICI_1_2")
                        ICI_3_4 = myReader("ICI_3_4")
                        ICI_5_6 = myReader("ICI_5_6")
                        ICI_7 = myReader("ICI_7")

                        LimitePensioneAU = par.IfNull(myReader("limite_pensione"), 0)

                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='A4'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        LimiteA4 = par.IfNull(myReader("ISEE_ERP"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='A5'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        LimiteA5 = par.IfNull(myReader("ISEE_ERP"), 0)
                        InizioB1 = par.IfNull(myReader("ISEE_ERP"), 0) + 1
                        CanoneMinimoA5 = par.IfNull(myReader("canone_minimo"), 0)
                        Perc_Inc_ISE_A5 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                        Perc_Inc_Loc_A5 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='C11'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        InizioC12 = par.IfNull(myReader("ISEE_ERP"), 0) + 1
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='B1'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CanoneMinimoB1 = par.IfNull(myReader("canone_minimo"), 0)
                        Perc_Inc_ISE_B1 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                        Perc_Inc_Loc_B1 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='C12'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CanoneMinimoC12 = par.IfNull(myReader("canone_minimo"), 0)
                        Perc_Inc_ISE_C12 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                        Perc_Inc_Loc_C12 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " WHERE SOTTO_AREA='D4'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        CanoneMinimoD4 = par.IfNull(myReader("canone_minimo"), 0)
                        Perc_Inc_ISE_D4 = par.IfNull(myReader("INC_MAX_ISEE_ERP"), 0)
                        Perc_Inc_Loc_D4 = par.IfNull(myReader("PERC_VAL_LOCATIVO"), 0)
                    End If
                    myReader.Close()

                    Dim comunicazioni As String = ""
                    Dim LimiteIsee As Integer = 0
                    'Dim S As String = par.CalcolaCanone27_AU_2009(lIdDichiarazione, lIdUnita.Value, CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL)

                    Dim IndiceContratto As String = "0"
                    Dim sCONTOcOSTObASE As String = "0"

                    par.cmd.CommandText = "SELECT rapporti_utenza.*,NVL(EDIFICI.SCONTO_COSTO_BASE,-1000) AS SCONTO_COSTO_BASE FROM SISCOM_MI.EDIFICI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.RAPPORTI_UTENZA WHERE EDIFICI.ID=UNITA_IMMOBILIARI.ID_EDIFICIO AND UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND COD_CONTRATTO='" & CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text & "'"
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        IndiceContratto = myReader("id")
                        sCONTOcOSTObASE = myReader("SCONTO_COSTO_BASE")
                    End If
                    myReader.Close()

                    Dim PS As String = "0"
                    par.cmd.CommandText = "SELECT PATR_SUPERATO FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
                    myReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        PS = par.IfNull(myReader("PATR_SUPERATO"), "0")
                    End If
                    myReader.Close()

                    Dim S As String = par.CalcolaCanone27_ANAGRAFE_UTENZA(IndiceContratto, PS, sCONTOcOSTObASE, ISTAT_1_PR, ISTAT_2_PR, ISTAT_1_AC, ISTAT_2_AC, ISTAT_1_PE, ISTAT_2_PE, ISTAT_1_DE, ISTAT_2_DE, LimiteA4, LimiteA5, InizioB1, InizioC12, CanoneMinimoA5, Perc_Inc_ISE_A5, Perc_Inc_Loc_A5, CanoneMinimoB1, Perc_Inc_ISE_B1, Perc_Inc_Loc_B1, CanoneMinimoC12, Perc_Inc_ISE_C12, Perc_Inc_Loc_C12, CanoneMinimoD4, Perc_Inc_ISE_D4, Perc_Inc_Loc_D4, LimitePensioneAU, ICI_7, ICI_5_6, ICI_3_4, ICI_1_2, AnnoAU, AnnoRedditi, InizioCanone, FineCanone, IDAU, lIdDichiarazione, CDbl(lIdUnita.Value), CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, CanoneCorrente, VAL_LOCATIVO_UNITA, comunicazioni, AreaEconomica, sISEE, sISE, sISR, sISP, sVSE, sREDD_DIP, sREDD_ALT, sLimitePensione, sPER_VAL_LOC, sPERC_INC_MAX_ISE_ERP, sCANONE_MIN, sISE_MIN, sCanone, sNOTE, sDEM, sSUPCONVENZIONALE, sCOSTOBASE, sZONA, sPIANO, sCONSERVAZIONE, sVETUSTA, sPSE, sINCIDENZAISE, sCOEFFFAM, sSOTTOAREA, sMOTIVODECADENZA, sNUMCOMP, sNUMCOMP66, sNUMCOMP100, sNUMCOMP100C, sPREVDIP, sDETRAZIONI, sMOBILIARI, sIMMOBILIARI, sCOMPLESSIVO, sDETRAZIONEF, sANNOCOSTRUZIONE, sLOCALITA, sASCENSORE, sDESCRIZIONEPIANO, sSUPNETTA, sALTRESUP, sMINORI15, sMAGGIORI65, sSUPACCESSORI, sVALORELOCATIVO, sCANONECLASSE, sCANONESOPP, sVALOCIICI, sALLOGGIOIDONEO, sISTAT, sISTAT2ANNO, sCANONECLASSEISTAT, sANNOINIZIOVAL, sANNOFINEVAL, sTIPOCANONEAPPLICATO, sCOMPETENZA1ANNO, sCOMPETENZA2ANNO, sESCLUSIONE1243, sDELTA12432, sDELTA12431, sCANONE12432, sCANONE12431)


                    par.cmd.CommandText = "INSERT INTO UTENZA_DICH_CANONI_EC (ID,CANONE_CLASSE,CANONE_SOPPORTABILE,DECADENZA_ALL_ADEGUATO,DECADENZA_VAL_ICI,CANONE_MINIMO_AREA,VALORE_LOCATIVO,SUP_ACCESSORI,MINORI_15,MAGGIORI_65,DATA_STIPULA,COD_CONTRATTO,ID_CONTRATTO,DATA_CALCOLO,ID_AREA_ECONOMICA,ISEE,ISE, ISR, ISP, PSE, VSE, REDDITI_DIP, REDDITI_ATRI, LIMITE_PENSIONI, " _
                                                      & "ISEE_27, PERC_VAL_LOC, INC_MAX, CANONE,NOTE,ID_BANDO_AU,DEM, SUPCONVENZIONALE, COSTOBASE, ZONA, PIANO, CONSERVAZIONE, VETUSTA,INCIDENZA_ISE," _
                                                      & "COEFF_NUCLEO_FAM,SOTTO_AREA,ANNOTAZIONI,PATRIMONIO_SUP,NON_RISPONDENTE,LIMITE_ISEE,ID_DICHIARAZIONE,CANONE_ATTUALE,ADEGUAMENTO,ISTAT," _
                                                      & "NUM_COMP,NUM_COMP_66,NUM_COMP_100,NUM_COMP_100_CON,REDD_PREV_DIP,DETRAZIONI,REDD_MOBILIARI,REDD_IMMOBILIARI,REDD_COMPLESSIVO,DETRAZIONI_FRAGILITA,ANNO_COSTRUZIONE," _
                                                      & "LOCALITA,PRESENTE_ASCENSORE,NUMERO_PIANO,SUP_NETTA,ALTRE_SUP,PERC_ISTAT_APPLICATA,CANONE_CLASSE_ISTAT,CANONE_91," _
                                                      & "INIZIO_VALIDITA_CAN,FINE_VALIDITA_CAN,TIPO_PROVENIENZA,SCONTO_COSTO_BASE,CANONE_1243_12,DELTA_1243_12,ESCLUSIONE_1243_12,TIPO_CANONE_APP,COMPETENZA) " _
                                                      & "VALUES (SEQ_UTENZA_DICH_CANONI_EC.NEXTVAL,'" & sCANONECLASSE & "','" & sCANONESOPP & "','" & sALLOGGIOIDONEO & "','" & sVALOCIICI & "','" & sCANONE_MIN & "','" & sVALORELOCATIVO & "','" & par.PulisciStrSql(sSUPACCESSORI) _
                                                      & "'," & par.IfEmpty(sMINORI15, 0) & "," & par.IfEmpty(sMAGGIORI65, 0) & ",'','" & CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text & "'," & IndiceContratto & ",'" & Format(Now, "yyyyMMddHHmmss") _
                                                      & "'," & AreaEconomica & ",'" & sISEE & "','" & sISE & "','" & sISR & "','" & sISP & "','" & sPSE & "','" & sVSE & "','" & sREDD_DIP & "','" _
                                                      & sREDD_ALT & "','" & sLimitePensione & "','" & sISE_MIN & "','" & sPER_VAL_LOC & "','" & sPERC_INC_MAX_ISE_ERP & "','" & sCanone & "','" _
                                                      & par.PulisciStrSql(sNOTE) & "'," & par.IfEmpty(IDAU, "null") & ",'" & sDEM & "','" & sSUPCONVENZIONALE & "','" & sCOSTOBASE & "','" & sZONA & "','" & sPIANO & "','" _
                                                      & sCONSERVAZIONE & "','" & sVETUSTA & "','" & sINCIDENZAISE & "','" & sCOEFFFAM & "','" & sSOTTOAREA & "','" & sMOTIVODECADENZA & "'," _
                                                      & PS & ",0," & par.IfEmpty(LimiteIsee, 0) & "," & lIdDichiarazione _
                                                      & ",'" & CanoneCorrente & "','','" _
                                                      & "'," & par.IfEmpty(sNUMCOMP, 0) & "," & par.IfEmpty(sNUMCOMP66, 0) & "," & par.IfEmpty(sNUMCOMP100, 0) & "," & par.IfEmpty(sNUMCOMP100C, 0) & "," & par.IfEmpty(sPREVDIP, 0) _
                                                      & ",'" & sDETRAZIONI & "','" & sMOBILIARI & "','" & sIMMOBILIARI & "','" & sCOMPLESSIVO & "','" & sDETRAZIONEF & "','" & sANNOCOSTRUZIONE & "','" & par.PulisciStrSql(sLOCALITA) _
                                                      & "','" & sASCENSORE & "','" & par.PulisciStrSql(sDESCRIZIONEPIANO) & "','" & sSUPNETTA & "','" & par.PulisciStrSql(sALTRESUP) & "','" & sISTAT & "','" & sCANONECLASSEISTAT & "','','" & InizioCanone & "','" & FineCanone _
                                                      & "','" & TipoProvenienza & "','" & Replace(sCONTOcOSTObASE, "1000", "") & "','" & sCANONE12431 & "','" & sDELTA12431 & "','" & sESCLUSIONE1243 & "','" & sTIPOCANONEAPPLICATO & "'," & Mid(InizioCanone, 1, 4) & ") "
                    par.cmd.ExecuteNonQuery()

                    sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style2' width='50%'>"
                    sStringasql = sStringasql & "COMUNE DI MILANO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td align='right' class='style2' width='50%'>"
                    sStringasql = sStringasql & "ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ") - " & Format(Now, "dd/MM/yyyy")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "<p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "INTESTATARIO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & txtCognome.Text & " " & txtNome.Text
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "CONTRATTO COD."
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "ALLOGGIO COD."
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & txtPosizione.Text
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "INDIRIZZO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & par.DeCripta(sIndirizzoUI.Value)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</p>"
                    sStringasql = sStringasql & "<br/>"
                    If AreaEconomica <> 4 Then
                        sStringasql = sStringasql & "<br/>"
                        sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & TestoTutti & "</p>"
                        sStringasql = sStringasql & "<br/>"
                    End If

                    sStringasql = sStringasql & "<p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style4'>"
                    sStringasql = sStringasql & "DETERMINAZIONE DEL VALORE LOCATIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td>"
                    sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "COEFF. DEMOGRAFIA"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sDEM
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "SUP.CONVENZIONALE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sSUPCONVENZIONALE
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "COSTO BASE MQ"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & "Anno " & sANNOCOSTRUZIONE & " - " & sCOSTOBASE
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "INDICE UBICAZIONE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sZONA
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "INDICE PIANO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sDESCRIZIONEPIANO & " - " & sPIANO
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "INDICE CONSERVAZIONE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sCONSERVAZIONE
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "INDICE VETUSTA"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & "Anno " & sANNOCOSTRUZIONE & " - " & sVETUSTA
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "VALORE CONVENZIONALE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format((par.Tronca(sVALORELOCATIVO) * 100) / 5, "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>"
                    sStringasql = sStringasql & "<td width='60%' class='style5'>"
                    sStringasql = sStringasql & "VALORE LOCATIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style5'>"
                    sStringasql = sStringasql & Format(CDbl(par.Tronca(sVALORELOCATIVO)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</p>"
                    sStringasql = sStringasql & "<p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style4'>"
                    sStringasql = sStringasql & "DATI REDDITUALI - CALCOLO ISE-ERP ED ISEE-ERP"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td>"
                    sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "NUMERO COMP."
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sNUMCOMP
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "NUMERO COMP. MINORI 15 ANNI"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sMINORI15
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "NUMERO COMP. MAGGIORI 65 ANNI"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sMAGGIORI65
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "NUMERO COMP. INVALIDI 66%-99%"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sNUMCOMP66
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "NUMERO COMP. INVALIDI 100%"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sNUMCOMP100
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "NUMERO COMP. INVALIDI 100%CON IND.ACC."
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sNUMCOMP100C
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "DETRAZIONI"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sDETRAZIONI, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "DETRAZIONI PER FRAGILITA&#39;"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sDETRAZIONEF, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "VALORE MOBILIARE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sMOBILIARI, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "VALORE IMMOBILIARE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sIMMOBILIARI, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "REDDITO COMPLESSIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCOMPLESSIVO, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style5'>"
                    sStringasql = sStringasql & "ISEE ERP EFFETTIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & par.Tronca(sISEE)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style5'>"
                    sStringasql = sStringasql & "ISE ERP EFFETTIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & par.Tronca(sISE)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "ISR"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & par.Tronca(sISR)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "ISP"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sISP
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "VSE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sVSE
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "REDDITI DIPENDENTI O ASSIMILATI"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sREDD_DIP, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "ALTRI TIPI DI REDDITO IMPONIBILI"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sREDD_ALT, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "LIMITE 2 PENSIONI INPS, MINIMA+SOCIALE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(sLimitePensione), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "PREVALENTEMENTE DIPENDENTE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sPREVDIP
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</p>"
                    sStringasql = sStringasql & "<p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style4'>"
                    sStringasql = sStringasql & "DETERMINAZIONE DEL CANONE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td>"
                    sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"

                    Select Case AreaEconomica
                        Case 1
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & "AREA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & "PROTEZIONE"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                            sStringasql = sStringasql & "<tr>"
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & "FASCIA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & sSOTTOAREA & "&nbsp; <span class='style7'>" & sNOTE & "</span>"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                        Case 2
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & "AREA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & "ACCESSO"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                            sStringasql = sStringasql & "<tr>"
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & "FASCIA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & sSOTTOAREA & "&nbsp; <span class='style7'>" & sNOTE & "</span>"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                        Case 3
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & "AREA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & "PERMANENZA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                            sStringasql = sStringasql & "<tr>"
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & "FASCIA"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & sSOTTOAREA & "&nbsp; <span class='style7'>" & sNOTE & "</span>"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                        Case 4
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & ""
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & "MANCANZA DEI REQUISITI DI ACCESSO ALL'ERP"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                            sStringasql = sStringasql & "<tr>"
                            sStringasql = sStringasql & "<td width='60%' class='style3'>"
                            sStringasql = sStringasql & ""
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "<td width='40%' class='style5'>"
                            sStringasql = sStringasql & "&nbsp; <span class='style7'></span>"
                            sStringasql = sStringasql & "</td>"
                            sStringasql = sStringasql & "</tr>"
                    End Select


                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "ISEE-ERP L.R. 27"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & par.Tronca(sISE_MIN / sPSE)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "ISE-ERP L.R. 27"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & par.Tronca(sISE_MIN)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "PERCENTUALE VALORE LOCATIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sPER_VAL_LOC
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "INCIDENZA PERC. VALORE LOCATIVO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sPERC_INC_MAX_ISE_ERP
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "VALORE INCIDENZA SU ISE-ERP"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONESOPP, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "COEFF. NUCLEI FAMILIARI"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & sCOEFFFAM
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style3'>"
                    sStringasql = sStringasql & "CANONE MINIMO MENSILE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style3'>"
                    sStringasql = sStringasql & Format(CDbl(sCANONE_MIN), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style6'>"
                    sStringasql = sStringasql & "CANONE CLASSE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style6'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONECLASSE, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style6'>"
                    sStringasql = sStringasql & "% ISTAT APPLICATA CANONE CLASSE"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style6'>"
                    sStringasql = sStringasql & sISTAT
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td width='60%' class='style6'>"
                    sStringasql = sStringasql & "CANONE CLASSE CON ISTAT"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td width='40%' class='style6'>"
                    sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCANONECLASSEISTAT, 0)), "##,##0.00")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"

                    If AreaEconomica <> 4 Then

                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style6'>"
                        sStringasql = sStringasql & "CANONE ERP ANNUALE REGIME"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style6'>"
                        sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCanone, 0)), "##,##0.00")
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style5'>"
                        sStringasql = sStringasql & "CANONE ERP MENSILE CALCOLATO"
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & Format(CDbl(par.IfEmpty(sCanone, 0) / 12), "##,##0.00")
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                    Else
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style6'>"
                        sStringasql = sStringasql & ""
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style6'>"
                        sStringasql = sStringasql & ""
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                        sStringasql = sStringasql & "<tr>"
                        sStringasql = sStringasql & "<td width='60%' class='style5'>"
                        sStringasql = sStringasql & ""
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "<td width='40%' class='style5'>"
                        sStringasql = sStringasql & ""
                        sStringasql = sStringasql & "</td>"
                        sStringasql = sStringasql & "</tr>"
                    End If
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</p>"

                    If AreaEconomica = 4 Then
                        sStringasql = sStringasql & "<br/>"
                        sStringasql = sStringasql & "<br/>"
                        sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & TestoDecadenti & "</p>"
                        sStringasql = sStringasql & "<br/>"
                    End If


                Else
                    sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style2' width='50%'>"
                    sStringasql = sStringasql & "COMUNE DI MILANO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td align='right' class='style2' width='50%'>"
                    sStringasql = sStringasql & "ANAGRAFE UTENZA " & ANNO_SIT_ECONOMICA + 1 & " (Redditi " & ANNO_SIT_ECONOMICA & ") - " & Format(Now, "dd/MM/yyyy")
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "<p>"
                    sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "INTESTATARIO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & txtCognome.Text & " " & txtNome.Text
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "CONTRATTO COD."
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "ALLOGGIO COD."
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & txtPosizione.Text
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td class='style3' height='20px' width='30%'>"
                    sStringasql = sStringasql & "INDIRIZZO"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "<td class='style3' width='70%'>"
                    sStringasql = sStringasql & par.DeCripta(sIndirizzoUI.Value)
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                    sStringasql = sStringasql & "</p>"
                    'sStringasql = sStringasql & "<br/>"
                    'sStringasql = sStringasql & "<br/>"
                    'sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & TestoTutti & "</p>"
                    'sStringasql = sStringasql & "<br/>"
                    sStringasql = sStringasql & "<br/>"
                    sStringasql = sStringasql & "<p>"
                    sStringasql = sStringasql & "<table style='width: 100%;' cellpadding='0' cellspacing='0'>"
                    sStringasql = sStringasql & "<tr>"
                    sStringasql = sStringasql & "<td>"
                    sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & TestoVirtuali & "</p>"
                    sStringasql = sStringasql & "</td>"
                    sStringasql = sStringasql & "</tr>"
                    sStringasql = sStringasql & "</table>"
                End If
                sStringasql = sStringasql & "<br /><br />"
                sStringasql = sStringasql & "<table style='border: 1px solid #000000; width: 100%;' cellpadding='0' cellspacing='0'>"
                sStringasql = sStringasql & "<tr>"
                sStringasql = sStringasql & "<td width='20%'>"
                sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>Annotazioni:</p>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "<td width='80%'>"
                sStringasql = sStringasql & "<p style='font-family: arial, Helvetica, sans-serif; font-size: 12pt'>" & sMOTIVODECADENZA & "</p>"
                sStringasql = sStringasql & "</td>"
                sStringasql = sStringasql & "</tr>"
                sStringasql = sStringasql & "</table>"


            End If

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            sStringasql = sStringasql & "<p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<font face='Arial'><table style='style='font-family: ARIAL; font-size: 12pt;width: 90%;'>" _
                                    & "<tr>" _
                                    & "<td>" _
                                    & "<B><p style='font-family: ARIAL; font-size: 16pt;'>INFORMATIVA AI SENSI DELL'ARTICOLO 13 DEL DECRETO LEGISLATIVO 30 GIUGNO 2003, N. 196 -Codice in materia di protezione dei dati personali-" _
                                    & "<br />" _
                                    & "</p></B>" _
                                    & "<br />" _
                                    & "<p style='font-family: ARIAL; font-size: 12pt;'>Gentile Signore/a," _
                                    & "<br />" _
                                    & "Il D. Lgs. 30 giugno 2003, n.196 ""Codice in materia di protezione dei dati personali"" prevede la tutela delle persone e di " _
                                    & "altri soggetti rispetto al trattamento dei dati personali. Il trattamento in base al citato ""Codice"" e' " _
                                    & "disciplinato assicurando un elevato livello di tutela dei diritti e delle " _
                                    & "liberta' fondamentali nonché della dignita' dell’interessato secondo principi di " _
                                    & "correttezza, liceita', trasparenza e di tutela della riservatezza. A tal fine il " _
                                    & "Comune di Milano in qualita' di Titolare del trattamento dei dati personali, ai " _
                                    & "sensi dell' art. 13 del Codice, Le fornisce le seguenti informazioni." _
                                    & "<br />" _
                                    & "<br />" _
                                    & "1. Oggetto e finalita' del trattamento I dati personali sono raccolti e trattati per l'esclusivo assolvimento degli obblighi istituzionali dell'Amministrazione comunale, riguardanti in particolare l'assegnazione in locazione di alloggi di edilizia residenziale pubblica e per finalita' amministrative strettamente connesse e strumentali alla gestione delle procedure di assegnazione degli alloggi stessi, nonche' alle disposizioni definite dalle normative nazionale e regionali in tema di edilizia residenziale pubblica." _
                                    & "<br /><br />" _
                                    & "2. Modalita' del trattamento In relazione alle finalita' indicate, il trattamento dei dati sara' effettuato attraverso modalita' cartacee e/o informatizzate. I trattamenti saranno effettuati solo da soggetti autorizzati con l’attenzione e la cautela previste dalle norme in materia garantendo la massima sicurezza e riservatezza dei dati personali, sensibili e giudiziari qualora raccolti per gli adempimenti necessari." _
                                    & "<br /><br />" _
                                    & "3. Natura del trattamento Il conferimento dei dati e' obbligatorio per la realizzazione delle finalita' descritte e l'eventuale rifiuto determinera' l'impossibilita' di dar corso alla Sua istanza e di porre in essere gli adempimenti conseguenti e inerenti la procedura per l'assegnazione degli alloggi." _
                                    & "<br /><br />" _
                                    & "4. Ambito di comunicazione e diffusione dei dati I dati personali, con esclusione di quelli idonei a rivelare lo stato di salute, potranno essere oggetto di diffusione. La graduatoria approvata dagli organi competenti in esito alla procedura di assegnazione verra' diffusa mediante pubblicazione nelle forme previste dalle norme in materia e anche attraverso il sito internet del Comune di Milano nel rispetto dei principi di pertinenza e non eccedenza. I dati personali verranno comunicati a soggetti pubblici o privati se previsto da disposizioni di legge o di regolamento." _
                                    & "<br /><br />" _
                                    & "5. Responsabili del trattamento dei dati I Responsabili del trattamento sono:<BR/>- il Comune di Milano nella persona del Direttore del Settore Gestione Patrimonio Abitativo Pubblico, via Larga 12, 20123 Milano, in qualita' di titolare del trattamento ai sensi dell’art.29 del D.Lgs.196/2003<BR/>- MM S.P.A., Via del Vecchio Politecnico, 8 - 20121 Milano, nella persona del Direttore Generale o del rappresentante pro tempore, in qualita' di responsabile per il trattamento dei dati personali, sensibili e /o giudiziari, per le finalita' di gestione del patrimonio abitativo pubblico  e degli annessi usi diversi" _
                                    & "<BR/><BR/>" _
                                    & "6. Consenso Il Comune di Milano, in quanto soggetto pubblico, non deve richiedere il consenso degli interessati per poter trattare i loro dati." _
                                    & "<br /><br />" _
                                    & "7.Diritti dell'interessato L’interessato potra' esercitare i diritti previsti dall'art. 7 del D. Lgs.196/03 ed in particolare ottenere la conferma dell'esistenza o meno di dati personali che lo riguardano, dell’origine dei dati personali, delle modalita' del trattamento, della logica applicata in caso di trattamento effettuato con l'ausilio di strumenti elettronici, nonché l'aggiornamento, la rettificazione ovvero quando vi ha interesse, l'integrazione dei dati." _
                                    & "<br /><BR/>" _
                                    & "L'interessato ha in oltre diritto:<br />- di ottenere la cancellazione, la trasformazione in forma anonima o il blocco dei dati trattati in violazione di legge;<br />- di opporsi, in tutto o in parte, per motivi legittimi al trattamento dei dati personali che lo riguardano, ancorché pertinenti allo scopo della raccolta.<br/><br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;FIRMA DEL DICHIARANTE_______________________________</td></tr></table>"


            sStringasql = sStringasql & "</font></BODY></HTML>"

            HttpContext.Current.Session.Remove("DICHIARAZIONE")



            Dim url As String = Server.MapPath("..\ALLEGATI\ANAGRAFE_UTENZA\")

            Dim pdfConverter1 As PdfConverter = New PdfConverter

            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")
            If Licenza <> "" Then
                pdfConverter1.LicenseKey = Licenza
            End If

            pdfConverter1.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter1.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Portrait
            pdfConverter1.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfDocumentOptions.ShowFooter = True
            pdfConverter1.PdfDocumentOptions.LeftMargin = 10
            pdfConverter1.PdfDocumentOptions.RightMargin = 10
            pdfConverter1.PdfDocumentOptions.TopMargin = 10
            pdfConverter1.PdfDocumentOptions.BottomMargin = 10
            pdfConverter1.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter1.PdfDocumentOptions.ShowHeader = False
            pdfConverter1.PdfFooterOptions.FooterText = ("Dichiarazione N. " & numero & " / " & CODICEANAGRAFICO)
            pdfConverter1.PdfFooterOptions.FooterTextColor = Drawing.Color.Black
            pdfConverter1.PdfFooterOptions.DrawFooterLine = False
            pdfConverter1.PdfFooterOptions.PageNumberText = "Pagina"
            pdfConverter1.PdfFooterOptions.ShowPageNumber = True

            Dim nomefile As String = ""
            If lbl45_Lotto.Visible = False Then
                nomefile = "00_" & par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") & "_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            Else
                nomefile = "00_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf"
            End If
            pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sStringasql, url & nomefile, Server.MapPath("..\IMG\"))
            pdfConverter1.PdfFooterOptions.ShowPageNumber = False

            'If File.Exists(url & "02_" & par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") & "_" & lIdDichiarazione & ".pdf") = True Then

            'End If

            If lbl45_Lotto.Visible = False Then
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sCalcoloISEE, url & "02_" & par.IfNull(CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text, "") & "_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf", Server.MapPath("..\IMG\"))
            Else
                pdfConverter1.SavePdfFromHtmlStringToFileWithTempFile(sCalcoloISEE, url & "02_" & lIdDichiarazione & "-" & Format(Now, "yyyyMMddHHmmss") & ".pdf", Server.MapPath("..\IMG\"))
            End If
            Dim ix As Integer = 0
            For ix = 0 To 200

            Next



            'par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'myReader.Close()


            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.open('../ALLEGATI/ANAGRAFE_UTENZA/" & nomefile & "','Dichiarazione','');", True)



        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CalcolaStampa() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        Finally

        End Try
    End Function

    Public Property PatrSuperato() As String
        Get
            If Not (ViewState("par_PatrSuperato") Is Nothing) Then
                Return CStr(ViewState("par_PatrSuperato"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_PatrSuperato") = value
        End Set

    End Property

    Public Property daVerificare() As String
        Get
            If Not (ViewState("par_daVerificare") Is Nothing) Then
                Return CStr(ViewState("par_daVerificare"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_daVerificare") = value
        End Set

    End Property

    Public Property flagDaVerific() As String
        Get
            If Not (ViewState("par_flagDaVerific") Is Nothing) Then
                Return CStr(ViewState("par_flagDaVerific"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_flagDaVerific") = value
        End Set

    End Property


    Public Property N_INV_100_ACC() As Integer
        Get
            If Not (ViewState("par_N_INV_100_ACC") Is Nothing) Then
                Return CInt(ViewState("par_N_INV_100_ACC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_N_INV_100_ACC") = value
        End Set

    End Property

    Public Property N_INV_100_NO_ACC() As Integer
        Get
            If Not (ViewState("par_N_INV_100_NO_ACC") Is Nothing) Then
                Return CInt(ViewState("par_N_INV_100_NO_ACC"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_N_INV_100_NO_ACC") = value
        End Set

    End Property

    Public Property N_INV_100_66() As Integer
        Get
            If Not (ViewState("par_N_INV_100_66") Is Nothing) Then
                Return CInt(ViewState("par_N_INV_100_66"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_N_INV_100_66") = value
        End Set

    End Property

    Public Property AprisolaLettura() As Integer
        Get
            If Not (ViewState("par_AprisolaLettura") Is Nothing) Then
                Return CInt(ViewState("par_AprisolaLettura"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AprisolaLettura") = value
        End Set

    End Property

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function

    Private Function CalcolaISEEDecadenza(ByVal lIdDichiarazione As Long, ByRef ISEE_DECADENZA As String, ByRef PATRIMONIO_DEC As String)
        Dim DETRAZIONI As Long


        Dim INV_100_CON As Integer
        Dim INV_100_NO As Integer
        Dim INV_66_99 As Integer
        Dim TOT_COMPONENTI As Integer
        Dim REDDITO_COMPLESSIVO As Double
        Dim TOT_SPESE As Long
        Dim DETRAZIONI_FRAGILE As Long
        Dim DETRAZIONI_FR As Long

        Dim MOBILI As Double
        Dim TASSO_RENDIMENTO As Double
        Dim FIGURATIVO_MOBILI As Double
        Dim TOTALE_ISEE_ERP As Double
        Dim IMMOBILI As Long
        Dim MUTUI As Long
        Dim IMMOBILI_RESIDENZA As Long
        Dim MUTUI_RESIDENZA As Long
        Dim TOTALE_PATRIMONIO_ISEE_ERP As Double
        Dim TOTALE_IMMOBILI As Long
        Dim LIMITE_PATRIMONIO As Double

        Dim ISR_ERP As Double
        Dim ISP_ERP As Double
        Dim ISE_ERP As Double
        Dim VSE As Double
        Dim ISEE_ERP As Double
        Dim ESCLUSIONE As String


        Dim PARAMETRO_MINORI As Double

        Dim MINORI As Integer
        Dim adulti As Integer


        Dim limite_isee As Long


        MINORI = 0
        adulti = 0
        ISR_ERP = 0
        ISP_ERP = 0
        ISE_ERP = 0
        VSE = 0
        TOT_COMPONENTI = 0
        DETRAZIONI = 0
        REDDITO_COMPLESSIVO = 0
        TOT_SPESE = 0
        DETRAZIONI_FRAGILE = 0
        DETRAZIONI_FR = 0
        ISEE_ERP = 0
        MOBILI = 0
        FIGURATIVO_MOBILI = 0
        IMMOBILI = 0
        MUTUI = 0
        IMMOBILI_RESIDENZA = 0
        MUTUI_RESIDENZA = 0
        TOTALE_IMMOBILI = 0
        TOTALE_ISEE_ERP = 0
        TOTALE_PATRIMONIO_ISEE_ERP = 0
        LIMITE_PATRIMONIO = 0
        ESCLUSIONE = ""
        Dim data_pg As String = ""
        Dim DataRiferimentoMinori As String = ""

        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
            INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
            INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
            data_pg = myReader("DATA_PG")
        End If
        myReader.Close()

        par.cmd.CommandText = "SELECT TASSO_RENDIMENTO,ANNO_AU FROM UTENZA_BANDI WHERE ID=" & lIndice_Bando
        Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        If myReader11.Read() Then

            TASSO_RENDIMENTO = par.IfNull(myReader11("TASSO_RENDIMENTO"), 0)
            limite_isee = 35000
            DataRiferimentoMinori = par.IfNull(myReader11("ANNO_AU"), Year(Now)) & "1231"


        End If
        myReader11.Close()

        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
        myReader = par.cmd.ExecuteReader()
        TOT_COMPONENTI = 0

        Do While myReader.Read()

            Dim EtaChiusura As Integer = par.RicavaEtaChiusura(par.FormattaData(myReader("DATA_NASCITA")), DataRiferimentoMinori)

            If EtaChiusura >= 15 Then
                If EtaChiusura >= 18 Then
                    adulti = adulti + 1
                End If
            Else
                MINORI = MINORI + 1
            End If
            '---fine

            DETRAZIONI = 0

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
            End While
            myReader1.Close()

            DETRAZIONI_FRAGILE = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.HasRows Then
                While myReader1.Read
                    DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReader1("IMPORTO"), 0)
                    TOT_SPESE = TOT_SPESE + par.IfNull(myReader1("IMPORTO"), 0)
                    If DETRAZIONI_FRAGILE > 10000 Then
                        DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                    Else
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    End If

                End While
                myReader1.Close()
            Else
                If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    TOT_SPESE = TOT_SPESE + 10000
                End If
                myReader1.Close()
            End If

            par.cmd.CommandText = "SELECT ROUND(IMPORTO/NVL(PERC_PROPRIETA,1),2) AS IMPORTO FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                    IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + par.IfNull(myReader1("VALORE"), 0)
                    MUTUI_RESIDENZA = MUTUI_RESIDENZA + par.IfNull(myReader1("MUTUO"), 0)
                Else
                    IMMOBILI = IMMOBILI + par.IfNull(myReader1("VALORE"), 0)
                    MUTUI = MUTUI + par.IfNull(myReader1("MUTUO"), 0)
                End If
            End While
            myReader1.Close()
            TOT_COMPONENTI = TOT_COMPONENTI + 1
        Loop
        myReader.Close()

        MOBILI = MOBILI - 25000
        If MOBILI < 0 Then MOBILI = 0



        DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

        FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165
        ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
        If ISEE_ERP < 0 Then
            ISEE_ERP = 0
        End If
        ISR_ERP = ISEE_ERP
        ISEE_ERP = 0

        TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA) - 25000
        If TOTALE_IMMOBILI < 0 Then TOTALE_IMMOBILI = 0

        TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

        ISP_ERP = TOTALE_ISEE_ERP

        TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)

        Dim PARAMETRO As Double
        Select Case TOT_COMPONENTI
            Case 1
                PARAMETRO = 1
            Case 2
                PARAMETRO = (138 / 100)
            Case 3
                PARAMETRO = (167 / 100)
            Case 4
                PARAMETRO = (190 / 100)
            Case 5
                PARAMETRO = (211 / 100)
            Case Else
                PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
        End Select

        PARAMETRO_MINORI = 0
        VSE = PARAMETRO
        If adulti >= 2 Then
            VSE = VSE - (MINORI * (1 / 10))
            PARAMETRO_MINORI = (MINORI * (1 / 10))
        End If

        LIMITE_PATRIMONIO = 16000 + (6000 * VSE)

        ISE_ERP = ISR_ERP + ISP_ERP

        ISEE_ERP = ISE_ERP / VSE

        ISEE_DECADENZA = ISE_ERP
        PATRIMONIO_DEC = TOTALE_PATRIMONIO_ISEE_ERP



    End Function


    Private Function CalcolaIsee(ByVal Pratica_Id As Long) As String

        Dim DETRAZIONI As Long


        Dim INV_100_CON As Integer
        Dim INV_100_NO As Integer
        Dim INV_66_99 As Integer
        Dim TOT_COMPONENTI As Integer
        Dim REDDITO_COMPLESSIVO As Double
        Dim TOT_SPESE As Long
        Dim DETRAZIONI_FRAGILE As Long
        Dim DETRAZIONI_FR As Long

        Dim MOBILI As Double
        Dim TASSO_RENDIMENTO As Double
        Dim FIGURATIVO_MOBILI As Double
        Dim TOTALE_ISEE_ERP As Double
        Dim IMMOBILI As Long
        Dim MUTUI As Long
        Dim IMMOBILI_RESIDENZA As Long
        Dim MUTUI_RESIDENZA As Long
        Dim TOTALE_PATRIMONIO_ISEE_ERP As Double
        Dim TOTALE_IMMOBILI As Long
        Dim LIMITE_PATRIMONIO As Double

        Dim ISR_ERP As Double
        Dim ISP_ERP As Double
        Dim ISE_ERP As Double
        Dim VSE As Double
        Dim ISEE_ERP As Double
        Dim ESCLUSIONE As String


        Dim PARAMETRO_MINORI As Double

        Dim MINORI As Integer
        Dim adulti As Integer

        Dim STRINGA_STAMPA As String


        Dim TIPO_ALLOGGIO As Integer
        Dim Calcola_36 As Boolean
        Dim Da_Calcolare36 As Boolean
        Dim limite_isee As Long

        Da_Calcolare36 = False
        MINORI = 0
        adulti = 0

        ISR_ERP = 0
        ISP_ERP = 0
        ISE_ERP = 0

        VSE = 0

        TOT_COMPONENTI = 0

        DETRAZIONI = 0
        REDDITO_COMPLESSIVO = 0
        TOT_SPESE = 0
        DETRAZIONI_FRAGILE = 0
        DETRAZIONI_FR = 0
        ISEE_ERP = 0
        MOBILI = 0
        FIGURATIVO_MOBILI = 0

        IMMOBILI = 0
        MUTUI = 0
        IMMOBILI_RESIDENZA = 0
        MUTUI_RESIDENZA = 0
        TOTALE_IMMOBILI = 0

        TOTALE_ISEE_ERP = 0
        TOTALE_PATRIMONIO_ISEE_ERP = 0
        LIMITE_PATRIMONIO = 0

        STRINGA_STAMPA = ""

        TIPO_ALLOGGIO = -1

        ESCLUSIONE = ""
        Dim CONTRATTO_30_ANNI As Boolean

        Dim DataRiferimentoMinori As String = ""


        If CType(Tab_InfoContratto1.FindControl("txtDataDec"), TextBox).Text <> "" Then
            If par.RicavaEta(par.IfEmpty(par.AggiustaData(CType(Tab_InfoContratto1.FindControl("txtDataDec"), TextBox).Text), Format(Now, "yyyMMdd"))) > 30 Then
                CONTRATTO_30_ANNI = True
            Else
                CONTRATTO_30_ANNI = False
            End If
        Else
            CONTRATTO_30_ANNI = False
        End If

        Calcola_36 = False
        If rdApplica.Checked = True And rdApplica.Visible = True Then
            Calcola_36 = True
        End If

        par.cmd.CommandText = "SELECT TASSO_RENDIMENTO,ANNO_AU FROM UTENZA_BANDI WHERE ID=" & lIndice_Bando
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        If myReader.Read() Then

            TASSO_RENDIMENTO = par.IfNull(myReader("TASSO_RENDIMENTO"), 0)
            limite_isee = 35000
            DataRiferimentoMinori = par.IfNull(myReader("ANNO_AU"), Year(Now)) & "1231"


        End If
        myReader.Close()



        If Calcola_36 = True Then
            TASSO_RENDIMENTO = par.RicavaTasso(CInt(cmbAnnoReddituale.SelectedItem.Text))
            If CInt(cmbAnnoReddituale.SelectedItem.Text) > 2006 Then
                limite_isee = 35000
            End If
        End If

        par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
        myReader = par.cmd.ExecuteReader()



        TOT_COMPONENTI = 0
        Dim ETA_RICHIEDENTE As Integer
        Dim VECCHI As Integer = 0

        Dim Entro70km As Boolean = False

        Do While myReader.Read()
            ETA_RICHIEDENTE = par.RicavaEtaChiusura(par.FormattaData(myReader("DATA_NASCITA")), DataRiferimentoMinori)
            If ETA_RICHIEDENTE >= 15 Then
                If ETA_RICHIEDENTE >= 18 Then
                    adulti = adulti + 1
                    If ETA_RICHIEDENTE > 65 Then
                        VECCHI = VECCHI + 1
                    End If
                End If
            Else
                MINORI = MINORI + 1
            End If

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DETRAZIONI = DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
            End While
            myReader1.Close()

            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()

            DETRAZIONI_FRAGILE = 0
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.HasRows Then
                While myReader1.Read
                    DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReader1("IMPORTO"), 0)
                    TOT_SPESE = TOT_SPESE + par.IfNull(myReader1("IMPORTO"), 0)

                    If DETRAZIONI_FRAGILE > 10000 Then
                        DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                    Else
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    End If

                End While
                myReader1.Close()
            Else
                If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                    DETRAZIONI_FR = DETRAZIONI_FR + 10000
                    TOT_SPESE = TOT_SPESE + 10000
                End If
                myReader1.Close()
            End If



            par.cmd.CommandText = "SELECT ROUND(IMPORTO/NVL(PERC_PROPRIETA,1),2) AS IMPORTO FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()



            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE nvl(FL_VENDUTO,0)=0 AND ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                If par.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                    IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + par.IfNull(myReader1("VALORE"), 0)
                    MUTUI_RESIDENZA = MUTUI_RESIDENZA + par.IfNull(myReader1("MUTUO"), 0)
                Else
                    IMMOBILI = IMMOBILI + par.IfNull(myReader1("VALORE"), 0)
                    MUTUI = MUTUI + par.IfNull(myReader1("MUTUO"), 0)
                End If

                If par.IfNull(myReader1("fl_70km"), "0") = "1" And Mid(par.IfNull(myReader1("cat_catastale"), "0"), 1, 1) = "A" Then
                    Entro70km = True
                End If
            End While
            myReader1.Close()
            TOT_COMPONENTI = TOT_COMPONENTI + 1
        Loop
        'End If
        myReader.Close()



        par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
            INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
            INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
        End If
        myReader.Close()

        DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)

        'STRINGA_STAMPA = "< center > "
        STRINGA_STAMPA = STRINGA_STAMPA & "<table border='1' cellpadding='0' cellspacing='0' width='95%'>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><font face='Arial' size='3'><b>CALCOLO ISEE  ai fini dell’art. 18 c. 1 lett. e  LR 1/2004</b></font>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><table border='0' cellpadding='0' cellspacing='0' width='81%' style='font-family: Arial; font-size: 12 pt'>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO COMPLESSIVO DEL NUCLEO FAMILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >FIGURATIVO REDDITO MOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI DAL REDDITO LORDO</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% CON INDENNITA'</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_CON & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >SPESE SOSTENUTE PER ASSISTENZA</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOT_SPESE, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% SENZA INDENNITA'</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_NO & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 66%-99%</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_66_99 & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Detrazioni per nucleo familiare affetto da fragilità (" & INV_100_CON + INV_100_NO + INV_66_99 & " invalidi)</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI_FR, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
        If ISEE_ERP < 0 Then
            ISEE_ERP = 0
        End If

        ISR_ERP = ISEE_ERP

        ISEE_ERP = 0

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE DEL REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO MOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(FIGURATIVO_MOBILI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI PER MUTUI CONTRATTI</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI - MUTUI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        If IMMOBILI_RESIDENZA > 0 Then
            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE DELLA RESIDENZA DI PROPRIETA'</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA, "##,##0.00")) & "</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >MUTUO RESIDUO</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE NETTO</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA - MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
        End If

        TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE COMPLESSIVO DEL PATRIMONIO DA CONSIDERARE AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_IMMOBILI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Coefficiente della valutazione del patrimonio immobiliare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;0,20</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

        ISP_ERP = TOTALE_ISEE_ERP

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_ISEE_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



        TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>PATRIMONIO COMPLESSIVO AI FINI ERP</B></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(TOTALE_PATRIMONIO_ISEE_ERP, "##,##0.00")) & "</B></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        Dim PARAMETRO As Double

        Select Case TOT_COMPONENTI
            Case 1
                PARAMETRO = 1
            Case 2
                PARAMETRO = (138 / 100)
            Case 3
                PARAMETRO = (167 / 100)
            Case 4
                PARAMETRO = (190 / 100)
            Case 5
                PARAMETRO = (211 / 100)
            Case Else
                PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
        End Select

        PARAMETRO_MINORI = 0
        VSE = PARAMETRO
        If adulti >= 2 Then
            VSE = VSE - (MINORI * (1 / 10))
            PARAMETRO_MINORI = (MINORI * (1 / 10))
        End If

        LIMITE_PATRIMONIO = 16000 + (6000 * VSE)

        'If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO Then

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>Limite Patrimonio Familiare</b></font></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</font></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(LIMITE_PATRIMONIO * 3, "##,##0.00")) & "</b></font></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        'End If


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di componenti del nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & TOT_COMPONENTI & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro corrispondente alla composizione del nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di minori di 15 anni nel nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & MINORI & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro per minori</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >-</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO_MINORI, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di adulti nel nucleo familiare</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & adulti & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISP_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        ISE_ERP = ISR_ERP + ISP_ERP

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >ISE-erp: indicatore della situazione economica (erp)</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISE_ERP, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VSE: Valore della scala di equivalenza</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(VSE, "##,##0.00")) & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        ISEE_ERP = ISE_ERP / VSE

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%'><font face='Arial' size='3'><b>ISEE-erp: Indicatore della Situazione Economica Equivalente (erp) (*)<b></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='2'>Euro</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(ISEE_ERP, "##,##0.00")) & "</b></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        ISEE_DICHIARAZIONE = par.Converti(Format(ISEE_ERP, "##,##0.00"))


        If ISEE_ERP > limite_isee Then
            'If limite_isee = 35000 Then
            '    If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
            '        Calcola_36 = False
            '    Else
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            '    End If
            'Else
            '    If (VECCHI = TOT_COMPONENTI) And CONTRATTO_30_ANNI = True Then
            '        Calcola_36 = False
            '    Else
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
            '        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            '    End If
            'End If

            If limite_isee = 35000 Then
                If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
                    Calcola_36 = False
                    '    Else
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
                End If
            Else
                If (VECCHI = TOT_COMPONENTI) And CONTRATTO_30_ANNI = True Then
                    Calcola_36 = False
                    '    Else
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                    '        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
                End If
            End If

            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

        End If

        If ISEE_ERP > 35000 And Calcola_36 = True Then
            If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
                Calcola_36 = False
            Else
                Da_Calcolare36 = True
            End If
        End If

        PatrSuperato = "0"
        If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO * 3 Then
            'If limite_isee = 35000 Then
            '    If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
            '        Calcola_36 = False
            '        '    Else
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            '        '        PatrSuperato = "1"
            '    End If
            'Else
            '    If (VECCHI = TOT_COMPONENTI) And CONTRATTO_30_ANNI = True Then
            '        Calcola_36 = False
            '        '    Else
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
            '        '        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            '        '        PatrSuperato = "1"
            '    End If
            'End If
            STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
            STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            PatrSuperato = "1"
        End If

        If PatrSuperato = "1" Then
            Da_Calcolare36 = True
        End If

        STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='2'>* L'indice ISEE ERP è stato elaborato sulla base della dichiarazione sostitutiva sottoscritta e potrà subire variazioni a seguito di successive verifiche e controlli da parte degli uffici competenti.</font>" & "</b></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


        STRINGA_STAMPA = STRINGA_STAMPA & "</table>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</td>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
        STRINGA_STAMPA = STRINGA_STAMPA & "</table>"

        par.cmd.CommandText = "update utenza_dichiarazioni set " _
                       & "" _
                       & "" _
                       & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                       & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
                       & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
                       & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "',PATR_SUPERATO='" & PatrSuperato & "' WHERE ID=" & Pratica_Id
        par.cmd.ExecuteNonQuery()

        'Modifiche Anagrafe Utenza
        'If CType(Dic_Patrimonio1.FindControl("cmbUI"), DropDownList).SelectedItem.Value = 1 Or CType(Dic_Patrimonio1.FindControl("chUbicazione"), CheckBox).Checked = True Then
        '    Da_Calcolare36 = True
        'End If
        If Entro70km = True Then
            Da_Calcolare36 = True
        End If

        If Da_Calcolare36 = True And Calcola_36 = True Then

            'STRINGA_STAMPA = STRINGA_STAMPA & "<p style='page-break-before: always'>&nbsp;</p>"
            MINORI = 0
            adulti = 0

            ISR_ERP = 0
            ISP_ERP = 0
            ISE_ERP = 0

            VSE = 0

            TOT_COMPONENTI = 0

            DETRAZIONI = 0
            REDDITO_COMPLESSIVO = 0
            TOT_SPESE = 0
            DETRAZIONI_FRAGILE = 0
            DETRAZIONI_FR = 0
            ISEE_ERP = 0
            MOBILI = 0
            FIGURATIVO_MOBILI = 0

            IMMOBILI = 0
            MUTUI = 0
            IMMOBILI_RESIDENZA = 0
            MUTUI_RESIDENZA = 0
            TOTALE_IMMOBILI = 0

            TOTALE_ISEE_ERP = 0
            TOTALE_PATRIMONIO_ISEE_ERP = 0
            LIMITE_PATRIMONIO = 0



            TIPO_ALLOGGIO = -1

            ESCLUSIONE = ""
            par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_NUCLEO WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()



            TOT_COMPONENTI = 0

            Do While myReader.Read()

                If par.RicavaEtaChiusura(par.FormattaData(myReader("DATA_NASCITA")), DataRiferimentoMinori) >= 15 Then
                    If par.RicavaEtaChiusura(par.FormattaData(myReader("DATA_NASCITA")), DataRiferimentoMinori) >= 18 Then
                        adulti = adulti + 1
                    End If
                Else
                    MINORI = MINORI + 1
                End If

                'par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_DETRAZIONI WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                'Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                'While myReader1.Read
                DETRAZIONI = 0 'DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
                'End While
                'myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_REDDITO WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ALTRI_REDDITI WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
                End While
                myReader1.Close()


                DETRAZIONI_FRAGILE = 0
                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.HasRows Then
                    While myReader1.Read
                        DETRAZIONI_FRAGILE = DETRAZIONI_FRAGILE + par.IfNull(myReader1("IMPORTO"), 0)
                        TOT_SPESE = TOT_SPESE + par.IfNull(myReader1("IMPORTO"), 0)

                        If DETRAZIONI_FRAGILE > 10000 Then
                            DETRAZIONI_FR = DETRAZIONI_FR + DETRAZIONI_FRAGILE
                        Else
                            DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        End If

                    End While
                    myReader1.Close()
                Else
                    If par.IfNull(myReader("indennita_acc"), 0) = "1" Then
                        DETRAZIONI_FR = DETRAZIONI_FR + 10000
                        TOT_SPESE = TOT_SPESE + 10000
                    End If
                    myReader1.Close()
                End If



                par.cmd.CommandText = "SELECT ROUND(IMPORTO/NVL(PERC_PROPRIETA,1),2) AS IMPORTO FROM UTENZA_COMP_PATR_MOB WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
                End While
                myReader1.Close()



                par.cmd.CommandText = "SELECT * FROM UTENZA_COMP_PATR_IMMOB WHERE NVL(FL_VENDUTO,0)=0 AND ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    If par.IfNull(myReader1("F_RESIDENZA"), 0) = 1 Then
                        IMMOBILI_RESIDENZA = IMMOBILI_RESIDENZA + par.IfNull(myReader1("VALORE"), 0)
                        MUTUI_RESIDENZA = MUTUI_RESIDENZA + par.IfNull(myReader1("MUTUO"), 0)
                    Else
                        IMMOBILI = IMMOBILI + par.IfNull(myReader1("VALORE"), 0)
                        MUTUI = MUTUI + par.IfNull(myReader1("MUTUO"), 0)
                    End If
                End While
                myReader1.Close()
                TOT_COMPONENTI = TOT_COMPONENTI + 1
            Loop
            'End If
            myReader.Close()

            MOBILI = MOBILI - 25000
            If MOBILI < 0 Then MOBILI = 0

            par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
                INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
                INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
            End If
            myReader.Close()

            DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)


            'STRINGA_STAMPA = STRINGA_STAMPA & "<BR><BR><table border='1' cellpadding='0' cellspacing='0' width='95%'>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><font face='Arial' size='2'><b>CALCOLO ISEE per Decadenza</b></font>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='95%'><table border='0' cellpadding='0' cellspacing='0' width='81%' style='font-family: Arial; font-size: 8 pt'>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO COMPLESSIVO DEL NUCLEO FAMILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(REDDITO_COMPLESSIVO, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >FIGURATIVO REDDITO MOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100), "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI DAL REDDITO LORDO</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% CON INDENNITA'</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_CON & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >SPESE SOSTENUTE PER ASSISTENZA</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOT_SPESE, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 100% SENZA INDENNITA'</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_100_NO & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >INVALIDI 66%-99%</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & INV_66_99 & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Detrazioni per nucleo familiare affetto da fragilità (" & INV_100_CON + INV_100_NO + INV_66_99 & " invalidi)</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(DETRAZIONI_FR, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP

            ISEE_ERP = 0

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE DEL REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO MOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(FIGURATIVO_MOBILI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >DETRAZIONI PER MUTUI CONTRATTI</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE CONSISTENZA DEL PATRIMONIO IMMOBILIARE</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format((IMMOBILI - MUTUI), "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE DELLA RESIDENZA DI PROPRIETA'</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >MUTUO RESIDUO PER LA RESIDENZA</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALORE NETTO DELLA CASA DI RESIDENZA</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(IMMOBILI_RESIDENZA - MUTUI_RESIDENZA, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA) - 25000
            If TOTALE_IMMOBILI < 0 Then TOTALE_IMMOBILI = 0

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >TOTALE COMPLESSIVO DEL PATRIMONIO DA CONSIDERARE AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_IMMOBILI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Coefficiente della valutazione del patrimonio immobiliare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;0,20</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            TOTALE_ISEE_ERP = (TOTALE_IMMOBILI) * 0.20000000000000001

            ISP_ERP = TOTALE_ISEE_ERP

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(TOTALE_ISEE_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"



            TOTALE_PATRIMONIO_ISEE_ERP = (TOTALE_IMMOBILI + FIGURATIVO_MOBILI)

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>PATRIMONIO COMPLESSIVO AI FINI ERP</B></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(TOTALE_PATRIMONIO_ISEE_ERP, "##,##0.00")) & "</B></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"




            Select Case TOT_COMPONENTI
                Case 1
                    PARAMETRO = 1
                Case 2
                    PARAMETRO = (138 / 100)
                Case 3
                    PARAMETRO = (167 / 100)
                Case 4
                    PARAMETRO = (190 / 100)
                Case 5
                    PARAMETRO = (211 / 100)
                Case Else
                    PARAMETRO = (211 / 100) + ((TOT_COMPONENTI - 5) * (17 / 100))
            End Select

            PARAMETRO_MINORI = 0
            VSE = PARAMETRO
            If adulti >= 2 Then
                VSE = VSE - (MINORI * (1 / 10))
                PARAMETRO_MINORI = (MINORI * (1 / 10))
            End If

            LIMITE_PATRIMONIO = 16000 + (6000 * VSE)


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><font face='Arial' size='3'><b>Limite Patrimonio Familiare</b></font></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</font></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(LIMITE_PATRIMONIO * 3, "##,##0.00")) & "</b></font></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"





            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di componenti del nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & TOT_COMPONENTI & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro corrispondente alla composizione del nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di minori di 15 anni nel nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & MINORI & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Parametro per minori</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >-</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(PARAMETRO_MINORI, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >Numero di adulti nel nucleo familiare</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >N°</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & adulti & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >REDDITO DA CONSIDERARE AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISR_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VALUTAZIONE DEL PATRIMONIO AI FINI ISEE-erp</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISP_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            ISE_ERP = ISR_ERP + ISP_ERP

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >ISE-erp: indicatore della situazione economica (erp)</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' >Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(ISE_ERP, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"


            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' >VSE: Valore della scala di equivalenza</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' >&nbsp;" & par.Converti(Format(VSE, "##,##0.00")) & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            ISEE_ERP = ISE_ERP / VSE

            'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%'><font face='Arial' size='3'><b>ISEE-erp: Indicatore della Situazione Economica Equivalente (erp)<b></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ><font face='Arial' size='1'>Euro</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ><font face='Arial' size='3'><b>&nbsp;" & par.Converti(Format(ISEE_ERP, "##,##0.00")) & "</b></td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            ' ISEE_DICHIARAZIONE = par.Converti(Format(ISEE_ERP, "##,##0.00"))


            If ISEE_ERP > limite_isee Then

                'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>ISEE superiore al limite ERP (" & limite_isee & ")</font>" & "</b></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            End If


            If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO * 3 Then
                'STRINGA_STAMPA = STRINGA_STAMPA & "<tr>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='55%' ><b><font face='Arial' size='4'>Limite Patrimoniale superato</font>" & "</b></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='4%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='5%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "<td width='20%' ></td>"
                'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"

            End If


            'STRINGA_STAMPA = STRINGA_STAMPA & "</table>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</td>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</tr>"
            'STRINGA_STAMPA = STRINGA_STAMPA & "</table>"

        End If


        STRINGA_STAMPA = STRINGA_STAMPA & "</center>"



        CalcolaIsee = STRINGA_STAMPA

        'par.cmd.CommandText = "update utenza_dichiarazioni set " _
        '                       & "" _
        '                       & "" _
        '                       & "ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
        '                       & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
        '                       & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
        '                       & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "',PATR_SUPERATO='" & PatrSuperato & "' WHERE ID=" & Pratica_Id
        'par.cmd.ExecuteNonQuery()

    End Function

    Protected Sub imgStampa_Click(sender As Object, e As System.EventArgs) Handles imgStampa.Click
        Try
            stampaClick.Value = "1"
            funzioneSalva()
            If bMemorizzato = True Then
                If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                    MessJQuery("Attenzione...Lo stato della dichiarazione deve essere COMPLETA! I dati sono stati comunque memorizzati.", 0, "Attenzione")
                    Exit Sub
                End If
                CalcolaStampa()
                Session.Item("STAMPATO") = "1"
                lblISEE.Text = ISEE_DICHIARAZIONE
            Else
                'imgStampa.Enabled = False
                'imgStampa.Text = ""
            End If
            stampaClick.Value = "0"

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CalcolaStampa() - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try

    End Sub

    Protected Sub txtCF_TextChanged(sender As Object, e As System.EventArgs) Handles txtCF.TextChanged
        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            CFLABEL.Visible = True
            CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
            txtCF.Text = ""
        Else

            CFLABEL.Text = ""
            If txtCF.Text <> "" Then
                If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                    If Correlazioni(UCase(txtCF.Text)) = True Then
                        CFLABEL.Visible = True
                        CFLABEL.Text = UCase(txtCF.Text) & ": TROVATO IN ALTRE DICHIARAZIONI!"
                        CompletaDati(UCase(txtCF.Text))
                        CFLABEL.Attributes.Add("OnClick", "javascript:window.open('../CorrelazioniUtenza.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & lIdDichiarazione & "','Correlazioni','top=0,left=0,width=600,height=400');")
                        If txtbinserito.Value = "0" Or txtbinserito.Value = "" Then
                            CType(Tab_Nucleo1.FindControl("txtProgr"), HiddenField).Value = "0"
                            txtbinserito.Value = "1"
                        End If
                    Else
                        CFLABEL.Text = ""
                        CompletaDati(UCase(txtCF.Text))
                        If txtbinserito.Value = "0" Or txtbinserito.Value = "" Then
                            CType(Tab_Nucleo1.FindControl("txtProgr"), HiddenField).Value = "0"
                            txtbinserito.Value = "1"
                        End If
                        par.SetFocusControl(Page, "cmbNazioneNas")
                    End If
                    If RU392.Value = "1" Or RU431.Value = "1" Then
                        txtCognome.Enabled = False
                        txtNome.Enabled = False
                        txtCF.Enabled = False
                        SalvaComponente0()
                        DatiinModifica392()
                        lblImportaRes.Visible = True
                        btnImportaRes.Visible = True
                        btnImportaRec.Visible = True
                        lblImportaRec.Visible = True
                    End If
                Else
                    CFLABEL.Visible = True
                    CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
                End If
            Else
                CFLABEL.Text = ""
            End If
        End If

    End Sub

    Private Function SalvaComponente0()
        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)


            par.cmd.CommandText = "Insert into UTENZA_COMP_NUCLEO (ID, ID_DICHIARAZIONE, PROGR, COD_FISCALE, COGNOME, NOME, SESSO, DATA_NASCITA, USL, GRADO_PARENTELA, PERC_INVAL, INDENNITA_ACC) " _
                                & "Values " _
                                & "(seq_utenza_comp_nucleo.nextval, " & lIdDichiarazione & ", 0, '" _
                                & par.PulisciStrSql(UCase(txtCF.Text)) & "', '" _
                                & par.PulisciStrSql(UCase(txtCognome.Text)) _
                                & "', '" & par.PulisciStrSql(UCase(txtNome.Text)) & "', '" _
                                & par.RicavaSesso(UCase(txtCF.Text)) & "', '" _
                                & par.PulisciStrSql(par.AggiustaData(txtDataNascita.Text)) & "', NULL, 1, 0, '0')"
            par.cmd.ExecuteNonQuery()
            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            CaricaNucleo()
            'funzioneSalva()

        Catch ex As Exception

            par.myTrans.Rollback()
            par.OracleConn.Close()
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza: Anagrafe Utenza - Inserimento intestatario " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Private Function CompletaDati(ByVal CF As String)
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans


        par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE COD='" & Mid(CF, 12, 4) & "'"
        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader1.Read() Then

            If myReader1("SIGLA") = "E" Or myReader1("SIGLA") = "C" Then
                cmbNazioneNas.SelectedIndex = -1
                cmbNazioneNas.Items.FindByText(myReader1("NOME")).Selected = True
                cmbPrNas.SelectedIndex = -1
                cmbComuneNas.SelectedIndex = -1
                cmbPrNas.Visible = False
                cmbComuneNas.Visible = False
                lblComune.Visible = False
                lblProvincia.Visible = False
            Else
                cmbNazioneNas.SelectedIndex = -1
                cmbNazioneNas.Items.FindByText("ITALIA").Selected = True

                cmbPrNas.SelectedIndex = -1
                cmbPrNas.Items.FindByText(myReader1("SIGLA")).Selected = True

                par.cmd.CommandText = "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC"
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

                cmbComuneNas.SelectedIndex = -1
                While myReader2.Read
                    Dim lsiFrutto As New ListItem(myReader2("NOME"), myReader2("ID"))
                    cmbComuneNas.Items.Add(lsiFrutto)
                End While
                myReader2.Close()
                cmbComuneNas.SelectedIndex = -1
                cmbComuneNas.Items.FindByText(myReader1("NOME")).Selected = True
                cmbPrNas.Visible = True
                cmbComuneNas.Visible = True
                lblComune.Visible = True
                lblProvincia.Visible = True
            End If
            Dim MIADATA As String
            txtDataNascita.Text = ""
            If Val(Mid(CF, 10, 2)) > 40 Then
                MIADATA = Format(Val(Mid(CF, 10, 2)) - 40, "00")
            Else
                MIADATA = Mid(CF, 10, 2)
            End If

            Select Case Mid(CF, 9, 1)
                Case "A"
                    MIADATA = MIADATA & "/01"
                Case "B"
                    MIADATA = MIADATA & "/02"
                Case "C"
                    MIADATA = MIADATA & "/03"
                Case "D"
                    MIADATA = MIADATA & "/04"
                Case "E"
                    MIADATA = MIADATA & "/05"
                Case "H"
                    MIADATA = MIADATA & "/06"
                Case "L"
                    MIADATA = MIADATA & "/07"
                Case "M"
                    MIADATA = MIADATA & "/08"
                Case "P"
                    MIADATA = MIADATA & "/09"
                Case "R"
                    MIADATA = MIADATA & "/10"
                Case "S"
                    MIADATA = MIADATA & "/11"
                Case "T"
                    MIADATA = MIADATA & "/12"
            End Select
            If Mid(CF, 7, 1) = "0" Then
                MIADATA = MIADATA & "/200" & Mid(CF, 8, 1)
                If Format(CDate(MIADATA), "yyyyMMdd") > Format(Now, "yyyyMMdd") Then
                    MIADATA = Mid(MIADATA, 1, 6) & "19" & Mid(MIADATA, 9, 2)
                End If
            Else
                MIADATA = MIADATA & "/19" & Mid(CF, 7, 2)
            End If
            txtDataNascita.Text = MIADATA
        End If
        myReader1.Close()

    End Function

    Private Function Correlazioni(ByVal CF As String) As Boolean
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        '‘par.cmd.Transaction = par.myTrans

        Correlazioni = False

        par.cmd.CommandText = "SELECT UTENZA_DICHIARAZIONI.ID FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO WHERE UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND (UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "' OR UTENZA_COMP_NUCLEO.COD_FISCALE='" & CF & "') AND UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE<>" & lIdDichiarazione

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            'txtCAPRes.Text = myReader(0)
            Correlazioni = True
        End If
        myReader.Close()

    End Function

    Protected Sub btnfunzelimina_Click(sender As Object, e As System.EventArgs) Handles btnfunzelimina.Click


        confCanc.Value = 1
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 1;", True)

        Select Case provenienza.Value

            Case "nucleo"
                EliminaComponente()
                idComp.Value = 0

            Case "reddito"
                EliminaRedditi()
                CType(Dic_Reddito1.FindControl("idCompReddito"), HiddenField).Value = ""
                CType(Dic_Reddito1.FindControl("idCompDetraz"), HiddenField).Value = ""
            Case "detrazioni"
                EliminaDetrazioni()
                CType(Dic_Reddito1.FindControl("idCompReddito"), HiddenField).Value = ""
                CType(Dic_Reddito1.FindControl("idCompDetraz"), HiddenField).Value = ""
            Case "pmobile"
                EliminaMobili()
                CType(Dic_Patrimonio1.FindControl("idPatrMob"), HiddenField).Value = 0
            Case "pimmobile"
                EliminaImmobili()
                CType(Dic_Patrimonio1.FindControl("idPatrImmob"), HiddenField).Value = 0
            Case "doc"
                EliminaDocumenti()
                CType(dic_Documenti1.FindControl("idDoc"), HiddenField).Value = "-1"
            Case "docP"
                EliminaDocumentiP()
                CType(dic_Documenti1.FindControl("idDocP"), HiddenField).Value = "-1"
            Case Else
                confCanc.Value = 0
        End Select

        confCanc.Value = 0
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
    End Sub


    Public Sub avviso()
        MessJQuery("Selezionare un componente dalla lista!", 0, "Attenzione")
    End Sub



    Protected Sub btnfunzesci2_Click(sender As Object, e As System.EventArgs) Handles btnfunzesci2.Click
        txtModificato.Value = "0"
        opUscita()
    End Sub

    Protected Sub opUscita()

        Try
            Dim dataDisd392 As String = ""
            Dim msgDataOdierna As String = ""
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKeyUsc", "Uscita=1;", True)

            If txtData392.Text = "" Then
                'dataDisd392 = Format(Now, "dd/MM/yyyy")
                If AreaEconomica <> 4 Then
                    msgDataOdierna = "La data disdetta è l\'odierna perchè l\'Utente non ha prodotto documenti per il diritto pregresso"
                End If
            Else
                dataDisd392 = txtData392.Text
                If dataDisd392 <> Format(Now, "dd/MM/yyyy") Then
                    msgDataOdierna = ""
                End If
            End If

            If txtModificato.Value = "1" Or txtModificato.Value = "111" Then
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "ConfermaEsci();", True)
            Else
                If txtModificato.Value <> "111" And txtModificato.Value <> "222" Then
                    If Session.Item("LAVORAZIONE") = "1" Then
                        If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then
                            MessJQuery("La dichiarazione deve essere stampata!", 0, "Attenzione")
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('caric').style.visibility = 'hidden';", True)
                            inUscita.Value = ""
                            Exit Sub
                        End If

                        par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)

                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            par.myTrans.Rollback()
                            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
                        End If
                        par.OracleConn.Close()
                        HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        Session.Item("LAVORAZIONE") = "0"
                        Session.Remove("STAMPATO")
                        Session.Remove("idBandoAU")


                        If Request.QueryString("PR") = "TC" Then
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKeyTC", "window.opener.document.forms['form1'].elements['txtDataDisdetta'].value='" & dataDisd392 & "';window.opener.document.forms['form1'].elements['dataDisdetta'].value='" & par.AggiustaData(dataDisd392) & "';window.opener.document.forms['form1'].elements['idAreaEconomica'].value='" & AreaEconomica & "';window.close();", True)
                            'ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKeyTC", "window.opener.document.forms['form1'].elements['idAreaEconomica'].value='" & AreaEconomica & "';self.close();", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)

                        End If

                    Else
                        Session.Item("LAVORAZIONE") = "0"

                        par.EseguiReleaseLock(PageID.Value, hiddenLockCorrenti.Value)

                        If Not (CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)) Is Nothing Then
                            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                            par.myTrans.Rollback()
                            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
                        End If

                        If Not (CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)) Is Nothing Then
                            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                            par.OracleConn.Close()
                            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
                            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                        End If

                        If Request.QueryString("PR") = "TC" Then
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKeyTC", "window.opener.document.forms['form1'].elements['txtDataDisdetta'].value='" & dataDisd392 & "';window.opener.document.forms['form1'].elements['dataDisdetta'].value='" & par.AggiustaData(dataDisd392) & "';window.opener.document.forms['form1'].elements['lblMsgData'].value='" & msgDataOdierna & "';window.close();", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.close();", True)
                        End If
                    End If

                Else
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('txtModificato').value = 1;", True)
                    inUscita.Value = ""
                End If

            End If

        Catch EX As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - ImgUscita.click() " & EX.Message)
            Response.Redirect("../Errore.aspx", False)
        Finally

        End Try


    End Sub

    Protected Sub cmbNazioneRes_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbNazioneRes.SelectedIndexChanged
        If cmbNazioneRes.SelectedItem.Text = "ITALIA" Then
            If AprisolaLettura = 0 Then
                cmbPrRes.Enabled = True
                cmbComuneRes.Enabled = True
            End If
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI WHERE SIGLA NOT IN ('EE','E','C','I','00') ORDER BY SIGLA ASC", cmbPrRes, "SIGLA", "SIGLA", True)
        Else
            cmbPrRes.SelectedIndex = -1
            cmbPrRes.SelectedItem.Value = "-1"
            cmbPrRes.Enabled = False
            cmbComuneRes.SelectedIndex = -1
            cmbComuneRes.Enabled = False
        End If
    End Sub

    Protected Sub cmbPrRes_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPrRes.SelectedIndexChanged
        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & cmbPrRes.SelectedItem.Value & "' ORDER BY NOME ASC", cmbComuneRes, "ID", "NOME", True)
    End Sub


    Protected Sub cmbNazioneNas_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbNazioneNas.SelectedIndexChanged
        If cmbNazioneNas.SelectedItem.Text = "ITALIA" Then
            If AprisolaLettura = 0 Then
                cmbPrNas.Enabled = True
                cmbComuneNas.Enabled = True
            End If
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI WHERE SIGLA NOT IN ('EE','E','C','I','00') ORDER BY SIGLA ASC", cmbPrNas, "SIGLA", "SIGLA", True)
        Else
            cmbPrNas.SelectedIndex = -1
            cmbPrNas.SelectedItem.Value = "-1"
            cmbPrNas.Enabled = False
            cmbComuneNas.SelectedIndex = -1
            cmbComuneNas.Enabled = False
        End If
    End Sub

    Protected Sub cmbPrNas_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPrNas.SelectedIndexChanged
        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & cmbPrNas.SelectedItem.Value & "' ORDER BY NOME ASC", cmbComuneNas, "ID", "NOME", True)
    End Sub

    Protected Sub filtraComuni()
        Try

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            If cmbPrSpediz.SelectedValue <> "-1" Then
                par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE SIGLA='" & cmbPrSpediz.SelectedValue & "'"
                Dim myReaderj As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderj.Read() Then
                    par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReaderj("SIGLA") & "' ORDER BY NOME ASC", cmbComuneSpediz, "ID", "NOME", True)
                    cmbComuneSpediz.SelectedValue = "-1"
                End If
                myReaderj.Close()
            Else
                par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI ORDER BY NOME ASC", cmbComuneSpediz, "ID", "NOME", True)
            End If
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            ''*********************CHIUSURA CONNESSIONE**********************
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub cmbPrSpediz_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbPrSpediz.SelectedIndexChanged
        filtraComuni()
    End Sub

    Protected Sub cmbComuneSpediz_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbComuneSpediz.SelectedIndexChanged

    End Sub

    Protected Sub btnCaricaDoc_Click(sender As Object, e As System.EventArgs) Handles btnCaricaDoc.Click
        CaricaDocumentiPresenti()
    End Sub

    Protected Sub btnImportaRes_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnImportaRes.Click
        CaricaDatiResidenza()
    End Sub

    Private Sub CaricaDatiResidenza()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "SELECT TIPO_LIVELLO_PIANO.DESCRIZIONE AS PIANO,SCALE_EDIFICI.DESCRIZIONE AS SCALA,IDENTIFICATIVI_CATASTALI.FOGLIO,IDENTIFICATIVI_CATASTALI.NUMERO,IDENTIFICATIVI_CATASTALI.SUB,UNITA_IMMOBILIARI.INTERNO,INDIRIZZI.CIVICO,INDIRIZZI.CAP,INDIRIZZI.DESCRIZIONE,COMUNI_NAZIONI.NOME,COMUNI_NAZIONI.ID FROM SISCOM_MI.SCALE_EDIFICI,SISCOM_MI.IDENTIFICATIVI_CATASTALI,COMUNI_NAZIONI,SISCOM_MI.UNITA_IMMOBILIARI,SISCOM_MI.INDIRIZZI,SISCOM_MI.TIPO_LIVELLO_PIANO WHERE TIPO_LIVELLO_PIANO.COD(+)=UNITA_IMMOBILIARI.COD_TIPO_LIVELLO_PIANO AND SCALE_EDIFICI.ID(+)=UNITA_IMMOBILIARI.ID_SCALA AND IDENTIFICATIVI_CATASTALI.ID(+)=UNITA_IMMOBILIARI.ID_CATASTALE AND COMUNI_NAZIONI.COD(+)=INDIRIZZI.COD_COMUNE AND  INDIRIZZI.ID (+)=UNITA_IMMOBILIARI.ID_INDIRIZZO AND COD_UNITA_IMMOBILIARE='" & txtPosizione.Text & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                If myReader.Read Then
                    txtAlloggio.Text = par.IfNull(myReader("INTERNO"), "")
                    txtCivicoRes.Text = par.IfNull(myReader("CIVICO"), "")
                    txtCAPRes.Text = par.IfNull(myReader("CAP"), "")

                    cmbTipoIRes.SelectedIndex = -1
                    cmbTipoIRes.Items.FindByText(par.RicavaVial(par.IfNull(myReader("DESCRIZIONE"), "VIA"))).Selected = True

                    txtIndRes.Text = Trim(Replace(par.IfNull(myReader("DESCRIZIONE"), "VIA"), cmbTipoIRes.SelectedItem.Text, ""))

                    txtFoglio.Text = par.IfNull(myReader("FOGLIO"), "")
                    txtSub.Text = par.IfNull(myReader("SUB"), "")
                    txtMappale.Text = par.IfNull(myReader("NUMERO"), "")

                    txtScala.Text = par.IfNull(myReader("SCALA"), "")
                    txtPiano.Text = par.IfNull(myReader("PIANO"), "")
                End If
            Else

                MessJQuery("Il codice Unità non è valido!", 0, "Attenzione")
            End If
            myReader.Close()

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:ANAGRAFE UTENZA - CaricaDatiResidenza - " & ex.Message)
            Session.Item("LAVORAZIONE") = "0"
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub

    Protected Sub btnImportaRec_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnImportaRec.Click
        CaricaDatiRecapito()
    End Sub

    Private Sub CaricaDatiRecapito()
        Try
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.cmd.CommandText = "select TIPO_COR,VIA_COR,CIVICO_COR,CAP_COR,LUOGO_COR,SIGLA_COR,PRESSO_COR from SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & CType(Tab_InfoContratto1.FindControl("txtRapporto"), TextBox).Text & "'"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.HasRows = True Then
                If myReader.Read Then

                    cmbTipoISped.SelectedIndex = -1
                    cmbTipoISped.Items.FindByText(par.IfNull(myReader("TIPO_COR"), "VIA")).Selected = True

                    txtIndirizzo.Text = par.IfNull(myReader("VIA_COR"), "")
                    txtCivicoSpediz.Text = par.IfNull(myReader("CIVICO_COR"), "")
                    txtCapSpediz.Text = par.IfNull(myReader("CAP_COR"), "")
                    txtPressoIndirizzo.Text = par.IfNull(myReader("PRESSO_COR"), "")

                    cmbPrSpediz.SelectedIndex = -1
                    cmbPrSpediz.Items.FindByText(par.IfNull(myReader("SIGLA_COR"), "MI")).Selected = True

                    cmbComuneSpediz.SelectedIndex = -1
                    cmbComuneSpediz.Items.FindByText(par.IfNull(myReader("LUOGO_COR"), "MILANO")).Selected = True
                End If

            Else

                MessJQuery("Il codice Contratto non è valido!", 0, "Attenzione")
            End If
            myReader.Close()

            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
        Catch ex As Exception
            If par.OracleConn.State = Data.ConnectionState.Open Then
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If
            Session.Add("ERRORE", "Provenienza:ANAGRAFE UTENZA - CaricaDatiRecapito - " & ex.Message)
            Session.Item("LAVORAZIONE") = "0"
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Sub


    Public Property sANNOINIZIOVAL() As String
        Get
            If Not (ViewState("par_sANNOINIZIOVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOINIZIOVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOINIZIOVAL") = value
        End Set
    End Property

    Public Property sANNOFINEVAL() As String
        Get
            If Not (ViewState("par_sANNOFINEVAL") Is Nothing) Then
                Return CStr(ViewState("par_sANNOFINEVAL"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOFINEVAL") = value
        End Set
    End Property




    Public Property sCOMPETENZA2ANNO() As String
        Get
            If Not (ViewState("par_sCOMPETENZA2ANNO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPETENZA2ANNO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPETENZA2ANNO") = value
        End Set
    End Property

    Public Property sCOMPETENZA1ANNO() As String
        Get
            If Not (ViewState("par_sCOMPETENZA1ANNO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPETENZA1ANNO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPETENZA1ANNO") = value
        End Set
    End Property

    Public Property sTIPOCANONEAPPLICATO() As String
        Get
            If Not (ViewState("par_sTIPOCANONEAPPLICATO") Is Nothing) Then
                Return CStr(ViewState("par_sTIPOCANONEAPPLICATO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sTIPOCANONEAPPLICATO") = value
        End Set
    End Property

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property



    Public Property AreaEconomica() As Integer
        Get
            If Not (ViewState("par_AreaEconomica") Is Nothing) Then
                Return CInt(ViewState("par_AreaEconomica"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_AreaEconomica") = value
        End Set

    End Property

    Public Property sISEE() As String
        Get
            If Not (ViewState("par_sISEE") Is Nothing) Then
                Return CStr(ViewState("par_sISEE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISEE") = value
        End Set
    End Property

    Public Property sISE() As String
        Get
            If Not (ViewState("par_sISE") Is Nothing) Then
                Return CStr(ViewState("par_sISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE") = value
        End Set
    End Property

    Public Property sVSE() As String
        Get
            If Not (ViewState("par_sVSE") Is Nothing) Then
                Return CStr(ViewState("par_sVSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVSE") = value
        End Set
    End Property


    Public Property sPSE() As String
        Get
            If Not (ViewState("par_sPSE") Is Nothing) Then
                Return CStr(ViewState("par_sPSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPSE") = value
        End Set
    End Property

    Public Property sISP() As String
        Get
            If Not (ViewState("par_sISP") Is Nothing) Then
                Return CStr(ViewState("par_sISP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISP") = value
        End Set
    End Property


    Public Property sISR() As String
        Get
            If Not (ViewState("par_sISR") Is Nothing) Then
                Return CStr(ViewState("par_sISR"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISR") = value
        End Set
    End Property

    Public Property sREDD_DIP() As String
        Get
            If Not (ViewState("par_sREDD_DIP") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_DIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_DIP") = value
        End Set
    End Property

    Public Property sREDD_ALT() As String
        Get
            If Not (ViewState("par_sREDD_ALT") Is Nothing) Then
                Return CStr(ViewState("par_sREDD_ALT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sREDD_ALT") = value
        End Set
    End Property

    Public Property sLimitePensione() As String
        Get
            If Not (ViewState("par_sLimitePensione") Is Nothing) Then
                Return CStr(ViewState("par_sLimitePensione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLimitePensione") = value
        End Set
    End Property

    Public Property sPER_VAL_LOC() As String
        Get
            If Not (ViewState("par_sPER_VAL_LOC") Is Nothing) Then
                Return CStr(ViewState("par_sPER_VAL_LOC"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPER_VAL_LOC") = value
        End Set
    End Property


    Public Property sCanone() As String
        Get
            If Not (ViewState("par_sCanone") Is Nothing) Then
                Return CStr(ViewState("par_sCanone"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCanone") = value
        End Set
    End Property

    Public Property sPERC_INC_MAX_ISE_ERP() As String
        Get
            If Not (ViewState("par_sPERC_INC_MAX_ISE_ERP") Is Nothing) Then
                Return CStr(ViewState("par_sPERC_INC_MAX_ISE_ERP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPERC_INC_MAX_ISE_ERP") = value
        End Set
    End Property

    Public Property sCANONE_MIN() As String
        Get
            If Not (ViewState("par_sCANONE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE_MIN") = value
        End Set
    End Property

    Public Property sISE_MIN() As String
        Get
            If Not (ViewState("par_sISE_MIN") Is Nothing) Then
                Return CStr(ViewState("par_sISE_MIN"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISE_MIN") = value
        End Set
    End Property

    Public Property sNOTE() As String
        Get
            If Not (ViewState("par_sNOTE") Is Nothing) Then
                Return CStr(ViewState("par_sNOTE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNOTE") = value
        End Set
    End Property

    Public Property sDEM() As String
        Get
            If Not (ViewState("par_sDEM") Is Nothing) Then
                Return CStr(ViewState("par_sDEM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDEM") = value
        End Set
    End Property

    Public Property sSUPCONVENZIONALE() As String
        Get
            If Not (ViewState("par_sSUPCONVENZIONALE") Is Nothing) Then
                Return CStr(ViewState("par_sSUPCONVENZIONALE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPCONVENZIONALE") = value
        End Set
    End Property

    Public Property sCOSTOBASE() As String
        Get
            If Not (ViewState("par_sCOSTOBASE") Is Nothing) Then
                Return CStr(ViewState("par_sCOSTOBASE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOSTOBASE") = value
        End Set
    End Property

    Public Property sZONA() As String
        Get
            If Not (ViewState("par_sZONA") Is Nothing) Then
                Return CStr(ViewState("par_sZONA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sZONA") = value
        End Set
    End Property

    Public Property sPIANO() As String
        Get
            If Not (ViewState("par_sPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPIANO") = value
        End Set
    End Property

    Public Property sCONSERVAZIONE() As String
        Get
            If Not (ViewState("par_sCONSERVAZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sCONSERVAZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCONSERVAZIONE") = value
        End Set
    End Property

    Public Property sVETUSTA() As String
        Get
            If Not (ViewState("par_sVETUSTA") Is Nothing) Then
                Return CStr(ViewState("par_sVETUSTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVETUSTA") = value
        End Set
    End Property

    Public Property sINCIDENZAISE() As String
        Get
            If Not (ViewState("par_sINCIDENZAISE") Is Nothing) Then
                Return CStr(ViewState("par_sINCIDENZAISE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sINCIDENZAISE") = value
        End Set
    End Property


    Public Property sCOEFFFAM() As String
        Get
            If Not (ViewState("par_sCOEFFFAM") Is Nothing) Then
                Return CStr(ViewState("par_sCOEFFFAM"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOEFFFAM") = value
        End Set
    End Property

    Public Property sSOTTOAREA() As String
        Get
            If Not (ViewState("par_sSOTTOAREA") Is Nothing) Then
                Return CStr(ViewState("par_sSOTTOAREA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSOTTOAREA") = value
        End Set
    End Property

    Public Property sMOTIVODECADENZA() As String
        Get
            If Not (ViewState("par_sMOTIVODECADENZA") Is Nothing) Then
                Return CStr(ViewState("par_sMOTIVODECADENZA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOTIVODECADENZA") = value
        End Set
    End Property

    Public Property sCANONE12431() As String
        Get
            If Not (ViewState("par_sCANONE12431") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE12431"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE12431") = value
        End Set
    End Property

    Public Property sCANONE12432() As String
        Get
            If Not (ViewState("par_sCANONE12432") Is Nothing) Then
                Return CStr(ViewState("par_sCANONE12432"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONE12432") = value
        End Set
    End Property



    Public Property sDELTA12431() As String
        Get
            If Not (ViewState("par_sDELTA12431") Is Nothing) Then
                Return CStr(ViewState("par_sDELTA12431"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDELTA12431") = value
        End Set
    End Property

    Public Property sDELTA12432() As String
        Get
            If Not (ViewState("par_sDELTA12432") Is Nothing) Then
                Return CStr(ViewState("par_sDELTA12432"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDELTA12432") = value
        End Set
    End Property

    Public Property sESCLUSIONE1243() As String
        Get
            If Not (ViewState("par_sESCLUSIONE1243") Is Nothing) Then
                Return CStr(ViewState("par_sESCLUSIONE1243"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sESCLUSIONE1243") = value
        End Set
    End Property

    Public Property sNUMCOMP() As String
        Get
            If Not (ViewState("par_sNUMCOMP") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP") = value
        End Set
    End Property

    Public Property sNUMCOMP66() As String
        Get
            If Not (ViewState("par_sNUMCOMP66") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP66"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP66") = value
        End Set
    End Property

    Public Property sNUMCOMP100() As String
        Get
            If Not (ViewState("par_sNUMCOMP100") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100") = value
        End Set
    End Property

    Public Property sNUMCOMP100C() As String
        Get
            If Not (ViewState("par_sNUMCOMP100C") Is Nothing) Then
                Return CStr(ViewState("par_sNUMCOMP100C"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sNUMCOMP100C") = value
        End Set
    End Property

    Public Property sPREVDIP() As String
        Get
            If Not (ViewState("par_sPREVDIP") Is Nothing) Then
                Return CStr(ViewState("par_sPREVDIP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sPREVDIP") = value
        End Set
    End Property

    Public Property sDETRAZIONI() As String
        Get
            If Not (ViewState("par_sDETRAZIONI") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONI") = value
        End Set
    End Property

    Public Property sMOBILIARI() As String
        Get
            If Not (ViewState("par_sMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMOBILIARI") = value
        End Set
    End Property


    Public Property sIMMOBILIARI() As String
        Get
            If Not (ViewState("par_sIMMOBILIARI") Is Nothing) Then
                Return CStr(ViewState("par_sIMMOBILIARI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sIMMOBILIARI") = value
        End Set
    End Property

    Public Property sCOMPLESSIVO() As String
        Get
            If Not (ViewState("par_sCOMPLESSIVO") Is Nothing) Then
                Return CStr(ViewState("par_sCOMPLESSIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCOMPLESSIVO") = value
        End Set
    End Property

    Public Property sDETRAZIONEF() As String
        Get
            If Not (ViewState("par_sDETRAZIONEF") Is Nothing) Then
                Return CStr(ViewState("par_sDETRAZIONEF"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDETRAZIONEF") = value
        End Set
    End Property

    Public Property sANNOCOSTRUZIONE() As String
        Get
            If Not (ViewState("par_sANNOCOSTRUZIONE") Is Nothing) Then
                Return CStr(ViewState("par_sANNOCOSTRUZIONE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sANNOCOSTRUZIONE") = value
        End Set
    End Property

    Public Property sLOCALITA() As String
        Get
            If Not (ViewState("par_sLOCALITA") Is Nothing) Then
                Return CStr(ViewState("par_sLOCALITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sLOCALITA") = value
        End Set
    End Property

    Public Property sASCENSORE() As String
        Get
            If Not (ViewState("par_sASCENSORE") Is Nothing) Then
                Return CStr(ViewState("par_sASCENSORE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sASCENSORE") = value
        End Set
    End Property

    Public Property sDESCRIZIONEPIANO() As String
        Get
            If Not (ViewState("par_sDESCRIZIONEPIANO") Is Nothing) Then
                Return CStr(ViewState("par_sDESCRIZIONEPIANO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sDESCRIZIONEPIANO") = value
        End Set
    End Property

    Public Property sSUPNETTA() As String
        Get
            If Not (ViewState("par_sSUPNETTA") Is Nothing) Then
                Return CStr(ViewState("par_sSUPNETTA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPNETTA") = value
        End Set
    End Property

    Public Property sALTRESUP() As String
        Get
            If Not (ViewState("par_sALTRESUP") Is Nothing) Then
                Return CStr(ViewState("par_sALTRESUP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALTRESUP") = value
        End Set
    End Property

    Public Property sMINORI15() As String
        Get
            If Not (ViewState("par_sMINORI15") Is Nothing) Then
                Return CStr(ViewState("par_sMINORI15"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMINORI15") = value
        End Set
    End Property


    Public Property sMAGGIORI65() As String
        Get
            If Not (ViewState("par_sMAGGIORI65") Is Nothing) Then
                Return CStr(ViewState("par_sMAGGIORI65"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sMAGGIORI65") = value
        End Set
    End Property

    Public Property sSUPACCESSORI() As String
        Get
            If Not (ViewState("par_sSUPACCESSORI") Is Nothing) Then
                Return CStr(ViewState("par_sSUPACCESSORI"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sSUPACCESSORI") = value
        End Set
    End Property

    Public Property sVALORELOCATIVO() As String
        Get
            If Not (ViewState("par_sVALORELOCATIVO") Is Nothing) Then
                Return CStr(ViewState("par_sVALORELOCATIVO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALORELOCATIVO") = value
        End Set
    End Property

    Public Property sCANONEMINIMO() As String
        Get
            If Not (ViewState("par_sCANONEMINIMO") Is Nothing) Then
                Return CStr(ViewState("par_sCANONEMINIMO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONEMINIMO") = value
        End Set
    End Property

    Public Property sCANONECLASSE() As String
        Get
            If Not (ViewState("par_sCANONECLASSE") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSE"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSE") = value
        End Set
    End Property

    Public Property sCANONESOPP() As String
        Get
            If Not (ViewState("par_sCANONESOPP") Is Nothing) Then
                Return CStr(ViewState("par_sCANONESOPP"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONESOPP") = value
        End Set
    End Property


    Public Property sVALOCIICI() As String
        Get
            If Not (ViewState("par_sVALOCIICI") Is Nothing) Then
                Return CStr(ViewState("par_sVALOCIICI"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sVALOCIICI") = value
        End Set
    End Property

    Public Property sALLOGGIOIDONEO() As String
        Get
            If Not (ViewState("par_sALLOGGIOIDONEO") Is Nothing) Then
                Return CStr(ViewState("par_sALLOGGIOIDONEO"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sALLOGGIOIDONEO") = value
        End Set
    End Property

    Public Property sISTAT() As String
        Get
            If Not (ViewState("par_sISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT") = value
        End Set
    End Property

    Public Property sISTAT2ANNO() As String
        Get
            If Not (ViewState("par_sISTAT2ANNO") Is Nothing) Then
                Return CStr(ViewState("par_sISTAT2ANNO"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sISTAT2ANNO") = value
        End Set
    End Property

    Public Property sCANONECLASSEISTAT() As String
        Get
            If Not (ViewState("par_sCANONECLASSEISTAT") Is Nothing) Then
                Return CStr(ViewState("par_sCANONECLASSEISTAT"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sCANONECLASSEISTAT") = value
        End Set
    End Property

    Public Property VAL_LOCATIVO_UNITA() As String
        Get
            If Not (ViewState("par_VAL_LOCATIVO_UNITA") Is Nothing) Then
                Return CStr(ViewState("par_VAL_LOCATIVO_UNITA"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_VAL_LOCATIVO_UNITA") = value
        End Set

    End Property



    Public Property CanoneCorrente() As Double
        Get
            If Not (ViewState("par_CanoneCorrente") Is Nothing) Then
                Return CDbl(ViewState("par_CanoneCorrente"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Double)
            ViewState("par_CanoneCorrente") = value
        End Set

    End Property

    Protected Sub chkRicevePresso_CheckedChanged(sender As Object, e As System.EventArgs) Handles chkRicevePresso.CheckedChanged
        VerificaIndirizzoRecapito()
    End Sub

    Private Sub VerificaIndirizzoRecapito()
        If chkRicevePresso.Checked = True Then
            If SoloLettura = "0" Then
                cmbTipoISped.Enabled = True
                txtIndirizzo.Enabled = True
                txtCivicoSpediz.Enabled = True
                txtCapSpediz.Enabled = True
                cmbPrSpediz.Enabled = True
                cmbComuneSpediz.Enabled = True
                txtPressoIndirizzo.Enabled = True
            Else
                cmbTipoISped.Enabled = False
                txtIndirizzo.Enabled = False
                txtCivicoSpediz.Enabled = False
                txtCapSpediz.Enabled = False
                cmbPrSpediz.Enabled = False
                cmbComuneSpediz.Enabled = False
                txtPressoIndirizzo.Enabled = False
            End If
        Else
            cmbTipoISped.Enabled = False
            txtIndirizzo.Enabled = False
            txtCivicoSpediz.Enabled = False
            txtCapSpediz.Enabled = False
            cmbPrSpediz.Enabled = False
            cmbComuneSpediz.Enabled = False
            txtPressoIndirizzo.Enabled = False
        End If
    End Sub
End Class




