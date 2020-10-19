Imports System.IO
Imports ExpertPdf.HtmlToPdf

Partial Class ANAUT_DichAUnuova
    Inherits PageSetIdMode
    Dim lValoreCorrente As Long
    Dim sAnnoIsee As String
    Dim sAnnoCanone As String
    Dim par As New CM.Global
    Dim bEseguito As Boolean = False
    Dim scriptblock As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Write("<script>top.location.href=""../../Portale.aspx""</script>")
            Exit Sub
        End If

        If IsPostBack = False And bEseguito = False Then

            txtTab.Value = "1"
            Response.Expires = 0
            txtCognome.Attributes.Add("OnChange", "javascript:AzzeraCF(txtCF,txtbinserito);")
            txtNome.Attributes.Add("OnChange", "javascript:AzzeraCF(txtCF,txtbinserito);")
            cmbComuneNas.Attributes.Add("OnChange", "javascript:AzzeraCF(txtCF,txtbinserito);")
            txtCF.Attributes.Add("OnChange", "javascript:AttendiCF();")
            txtCognome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNome.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCF.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtDataPG.Attributes.Add("onblur", "javascript:return mask(this.value,this,'2,5','/');")
            txtIndRes.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtCognomeS.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtNomeS.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            txtIndResSott.Attributes.Add("Onblur", "javascript:this.value=this.value.toUpperCase();")
            cmbAnnoReddituale.Attributes.Add("onclick", "javascript:Uscita=1;")

            rdbListSott.Attributes.Add("onclick", "javascript:visibileSottoscr();")
            H1.Value = "1"

            vIdConnessione = Format(Now, "yyyyMMddHHmmss")

            bMemorizzato = False
            lIdDichiarazione = Request.QueryString("ID")
            Fl_Integrazione = Request.QueryString("INT")
            Fl_US = Request.QueryString("US")
            SoloLettura = Request.QueryString("LE")
            'If Fl_Integrazione = "1" Then
            '    H1.Value = "1"
            '    H2.Value = "1"
            '    Image3.Visible = True
            '    Label5.Visible = True
            'End If
            txtTab.Value = "1"
            If Request.QueryString("GLocat") <> "" Then
                MenuStampe.Visible = False
                btnApplica.Visible = False
                IMGCanone.Visible = False
            End If
            ControlliSottoscritt()
            If lIdDichiarazione = -1 Then
                lNuovaDichiarazione = 1
                ' ''CType(Dic_Dichiarazione1.FindControl("Label20"), Label).Text = lNuovaDichiarazione
                ' ''imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                ' ''CType(Dic_Dichiarazione1.FindControl("txtbinserito"), TextBox).Text = "0"
                NuovaDichiarazione()
                ' ''CType(Dic_Nucleo1.FindControl("ListBox1"), ListBox).Items.Add("")
            Else
                lNuovaDichiarazione = 0
                '''CType(Dic_Dichiarazione1.FindControl("Label20"), Label).Text = lNuovaDichiarazione
                imgStampa.Enabled = True

                VisualizzaDichiarazione()
                If Session.Item("ANAGRAFE") = "1" Then
                    imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=4','Anagrafe','top=0,left=0,width=600,height=400');")
                End If
            End If
            bEseguito = True

        Else
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey111", "document.getElementById('caric').style.visibility = 'hidden';", True)
        End If

        SettaControlModifiche(Me)

        If Request.QueryString("CH") = 1 Then
            Me.imgUscita.Visible = True
        ElseIf Request.QueryString("CH") = 2 Then
            Me.imgUscita.Visible = True
            Me.imgVaiDomanda.Visible = False

        End If

        If par.IfNull(tipoCausale.Value, "") = "30" And par.IfNull(Request.QueryString("CH"), "") = "1" Then
            imgVaiDomanda.Visible = True
        End If

        'scriptblock = "<script language='javascript' type='text/javascript'>" _
        '    & "function CalcolaReddito() {window.open('RedditoConv.aspx?ID=" & lIdDichiarazione & "',null,'');}" _
        '    & "</script>"
        'If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript3000")) Then
        '    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript3000", scriptblock)
        'End If

        ' ''If IsPostBack = True And HiddenField1.Value = "1" Then
        ' ''    CaricaComponenti()
        ' ''    HiddenField1.Value = "0"
        ' ''End If
        H1.Value = "1"
        '''Dic_Dichiarazione1.FindControl("chTitolare").Visible = False

    End Sub

    Private Function NuovaDichiarazione()
        Dim CT1 As DropDownList

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT ANNO_ISEE,ANNO_CANONE,ID,TIPO_BANDO,DATA_INIZIO FROM BANDI_VSA WHERE STATO=1 order by id desc"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                If myReader.HasRows = False Then
                    If lIdDichiarazione = -1 Then
                        MessJQuery("NESSUN BANDO CAMBI IN EMERGENZA APERTO. Non è possibile inserire nuove dichiarazioni!", 0, "Attenzione")
                        'Response.Write("<script>alert('NESSUN BANDO CAMBI IN EMERGENZA APERTO. Non è possibile inserire nuove dichiarazioni!');location.replace('pagina_home.aspx');</script>")

                    Else
                        par.OracleConn.Close()
                        Session.Item("LAVORAZIONE") = "0"
                        MessJQuery("Il Bando a cui appartiene questa dichiarazione è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!", 0, "Attenzione")
                        'Response.Write("<script>alert('Il Bando a cui appartiene questa dichiarazione è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!');</script>")
                    End If
                    Exit Function
                Else
                    sAnnoIsee = myReader(0)
                    sAnnoCanone = myReader(1)
                    RiempiCmbAnniRedd(sAnnoIsee)

                    'CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    'CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(sAnnoIsee)
                    'CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = sAnnoIsee
                    'CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = sAnnoIsee
                    'CType(Dic_Reddito_Conv1.FindControl("Label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & sAnnoIsee

                    lIndice_Bando = myReader(2)
                    lBando = par.IfNull(myReader("TIPO_BANDO"), "-1")

                End If
            Else
                If lIdDichiarazione = -1 Then
                    par.OracleConn.Close()
                    Session.Item("LAVORAZIONE") = "0"
                    MessJQuery("NESSUN BANDO CAMBI IN EMERGENZA APERTO. Non è possibile inserire nuove dichiarazioni!", 0, "Attenzione")
                    'Response.Write("<script>alert('NESSUN BANDO CAMBI IN EMERGENZA APERTO. Non è possibile inserire nuove dichiarazioni!');location.replace('pagina_home.aspx');</script>")

                Else
                    MessJQuery("Il Bando a cui appartiene questa dichiarazione è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!", 0, "Attenzione")
                    'Response.Write("<script>alert('Il Bando a cui appartiene questa dichiarazione è CHIUSO. Per apportare modifiche utilizzare le funzioni di INTEGRAZIONE!');</script>")
                End If
                Exit Function
            End If
            myReader.Close()

            txtDataPG.Text = Format(Now, "dd/MM/yyyy")
            Dim lsiFrutto As New ListItem("DA COMPLETARE", "0")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("COMPLETA", "1")
            cmbStato.Items.Add(lsiFrutto)
            lsiFrutto = New ListItem("DA CANCELLARE", "2")
            cmbStato.Items.Add(lsiFrutto)

            cmbStato.SelectedItem.Text = "DA COMPLETARE"

            lblAvviso.Visible = False
            imgAlert.Visible = False

            'CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")
            'CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")
            'CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = Format(Now, "dd/MM/yyyy")

            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneNas, "ID", "NOME", True)
            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneRes, "ID", "NOME", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrNas, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrRes, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", cmbTipoIRes, "COD", "DESCRIZIONE", True)

            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneNasSott, "ID", "NOME", True)
            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneResSott, "ID", "NOME", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrNasSott, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrResSott, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", cmbTipoIResSott, "COD", "DESCRIZIONE", True)

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=4415"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                CT1 = cmbNazioneNas
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = cmbPrNas
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas, "ID", "NOME", True)
                CT1 = cmbComuneNas
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True

                CT1 = cmbNazioneRes
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = cmbPrRes
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneRes, "ID", "NOME", True)

                CT1 = cmbComuneRes
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True
                txtCAPRes.Text = par.IfNull(myReader("CAP"), "")

                CT1 = cmbNazioneNasSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = cmbPrNasSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas2, "ID", "NOME", True)

                CT1 = cmbComuneNas2
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True

                CT1 = cmbNazioneResSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText("ITALIA").Selected = True
                CT1 = cmbPrResSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneResSott, "ID", "NOME", True)
                CT1 = cmbComuneResSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("NOME")).Selected = True
                txtCAPRes.Text = par.IfNull(myReader("CAP"), "")


            End If
            myReader.Close()

            cmbTipoIRes.SelectedItem.Text = "VIA"
            cmbTipoIRes.SelectedItem.Value = "6"
            cmbTipoIRes.SelectedItem.Text = "VIA"
            cmbTipoIRes.SelectedItem.Value = "6"

            par.cmd.CommandText = "SELECT MAX(ID) FROM NUM_PROTOCOLLI_VSA"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                lValoreCorrente = par.IfNull(myReader(0), 0) + 1
            End If
            myReader.Close()
            par.cmd.CommandText = "INSERT INTO NUM_PROTOCOLLI_VSA VALUES (" & lValoreCorrente & ")"
            par.cmd.ExecuteNonQuery()
            lblPG.Text = Format(lValoreCorrente, "0000000000")



            cmbAnnoReddituale.SelectedIndex = -1
            cmbAnnoReddituale.Items.FindByValue(sAnnoIsee).Selected = True

            ''CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & cmbAnnoReddituale.SelectedItem.Value
            ''CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
            ''CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
            ''CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
            ''CType(Dic_Integrazione1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value

            CaricaNucleo()
            CaricaNucleoSpese()
            CaricaDocumenti()
            CaricaRedditi()
            CaricaPatrimMob()
            CaricaPatrimImmob()

            par.cmd.CommandText = "INSERT INTO Dichiarazioni_VSA (ID,ID_CAF,ID_BANDO) VALUES (SEQ_DICHIARAZIONI_VSA.NEXTVAL," & Session.Item("ID_CAF") & "," & lIndice_Bando & ")"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "SELECT SEQ_DICHIARAZIONI_VSA.CURRVAL FROM DUAL"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lIdDichiarazione = myReader(0)

                '''CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
            End If
            myReader.Close()
            lblPG.ToolTip = lIdDichiarazione
            Session.Add("LAVORAZIONE", "1")
            Session.Add("STAMPATO", "0")

            HttpContext.Current.Session.Add("CONNESSIONE" & vIdConnessione, par.OracleConn)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

        Catch ex1 As Oracle.DataAccess.Client.OracleException

            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza: Nuova Dichiarazione - " & ex1.Message)
            Response.Redirect("../../Errore.aspx", False)


        Catch ex As Exception

            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza: Nuova Dichiarazione - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)


        Finally

        End Try
    End Function

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

    Private Sub DatiSolaLettura(ByVal obj As Control)

        Dim CTRL As Control
        For Each CTRL In obj.Controls
            If CTRL.Controls.Count > 0 Then
                DatiSolaLettura(CTRL)
            End If
            If TypeOf CTRL Is DropDownList Then
                DirectCast(CTRL, DropDownList).Enabled = False
            End If
            If TypeOf CTRL Is TextBox Then
                DirectCast(CTRL, TextBox).Enabled = False
            End If
            If TypeOf CTRL Is CheckBox Then
                DirectCast(CTRL, CheckBox).Enabled = False
            End If
            If TypeOf CTRL Is CheckBoxList Then
                DirectCast(CTRL, CheckBoxList).Enabled = False
            End If
            If TypeOf CTRL Is RadioButtonList Then
                DirectCast(CTRL, RadioButtonList).Enabled = False
            End If
            If TypeOf CTRL Is Image Then
                DirectCast(CTRL, Image).Visible = False
            End If
            If TypeOf CTRL Is ImageButton Then
                DirectCast(CTRL, ImageButton).Visible = False
            End If
        Next

    End Sub

    Public Property lBando() As Long
        Get
            If Not (ViewState("par_lBando1") Is Nothing) Then
                Return CLng(ViewState("par_lBando1"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lBando1") = value
        End Set

    End Property

    Public Property Fl_Integrazione() As String
        Get
            If Not (ViewState("par_Fl_Integrazione") Is Nothing) Then
                Return CStr(ViewState("par_Fl_Integrazione"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Fl_Integrazione") = value
        End Set

    End Property

    Public Property Fl_US() As String
        Get
            If Not (ViewState("par_Fl_US") Is Nothing) Then
                Return CStr(ViewState("par_Fl_US"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Fl_US") = value
        End Set

    End Property

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

    Private Sub CaricaNucleo()
        Try
            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.*,TO_CHAR(TO_DATE(DATA_NASCITA,'yyyymmdd'),'dd/mm/yyyy') AS DATA_NASC,TO_CHAR(TO_DATE(DATA_INGRESSO_NUCLEO,'yyyymmdd'),'dd/mm/yyyy') AS DATA_INGRESSO,data_ingresso_nucleo, (CASE WHEN NVL(data_ingresso_nucleo,'NULL') = 'NULL' THEN 'NO' ELSE 'SI' END) AS NUOVO_COMP,NATURA_INVAL,(CASE WHEN INDENNITA_ACC ='0' THEN 'NO' ELSE 'SI' END) AS IND_ACCOMP, T_TIPO_PARENTELA.DESCRIZIONE,(CASE WHEN TIPO_INVAL ='D' THEN 'Definitiva' WHEN TIPO_INVAL ='P' THEN 'Provvisoria' ELSE '' END) AS TIPO_INVALIDITA FROM COMP_NUCLEO_VSA,T_TIPO_PARENTELA,NUOVI_COMP_NUCLEO_VSA " _
                & " where COMP_NUCLEO_VSA.GRADO_PARENTELA=T_TIPO_PARENTELA.COD(+) AND COMP_NUCLEO_VSA.ID=NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE(+) and COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " ORDER BY PROGR ASC"
            'UTENZA_COMP_NUCLEO.id_DICHIARAZIONE = " & lIdDichiarazione & " AND
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtComp = New Data.DataTable
            da.Fill(dtComp)
            da.Dispose()

            Dim eta As Integer = 0

            Dim rowDT As System.Data.DataRow
            If dtComp.Rows.Count > 0 Then

                For Each row As Data.DataRow In dtComp.Rows
                    eta = par.RicavaEta(par.IfNull(row.Item("DATA_NASCITA"), ""))
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
                    If par.IfNull(row.Item("INDENNITA_ACC"), "") = "0" And par.IfNull(row.Item("PERC_INVAL"), 0) > 66 And par.IfNull(row.Item("PERC_INVAL"), 0) < 100 Then
                        N_INV_100_66 = N_INV_100_66 + 1
                    End If
                    If par.IfNull(row.Item("DATA_INGRESSO_NUCLEO"), "") <> "" Then
                        newComp = 1
                    End If
                Next

                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataSource = dtComp
                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataBind()

                numComp.Value = dtComp.Rows.Count
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey112", "document.getElementById('numComp').value = " & dtComp.Rows.Count & ";", True)
            Else
                rowDT = dtComp.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("COGNOME") = "&nbsp"
                rowDT.Item("NOME") = "&nbsp"

                dtComp.Rows.Add(rowDT)

                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataSource = dtComp
                CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataBind()
                numComp.Value = "0"
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaNucleo() - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaNucleoSpese()
        Try
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT COMP_ELENCO_SPESE_VSA.ID,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_ELENCO_SPESE_VSA.IMPORTO,COMP_ELENCO_SPESE_VSA.DESCRIZIONE from COMP_ELENCO_SPESE_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA WHERE " _
                & " COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID AND comp_nucleo_VSA.id_dichiarazione=" & lIdDichiarazione & " and COMP_ELENCO_SPESE_VSA.id_componente=comp_nucleo_VSA.id order by comp_nucleo_VSA.progr asc "
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
                'rowDT.Item("IMPORTO") = "&nbsp"
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
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaDocumenti()
        Try
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT VSA_DOC_MANCANTI.DESCRIZIONE, VSA_DOC_MANCANTI.ID_DOC AS ID,VSA_DOC_NECESSARI.descrizione as ID_TIPO FROM VSA_DOC_MANCANTI,VSA_DOC_NECESSARI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND VSA_DOC_MANCANTI.id_DOC=VSA_DOC_NECESSARI.id ORDER BY VSA_DOC_MANCANTI.DESCRIZIONE ASC"
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
                    rowDT.Item("DESCRIZIONE") = Replace(par.MiaFormat(row.Item("DESCRIZIONE"), 250), vbCrLf, "</br>")
                    docMancante = True
                    dtDoc.Rows.Add(rowDT)
                Next

                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataSource = dtDoc
                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataBind()
            Else
                docMancante = False
                rowDT = dtDoc.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("id_tipo") = "&nbsp"

                rowDT.Item("DESCRIZIONE") = "&nbsp"
                dtDoc.Rows.Add(rowDT)

                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataSource = dtDoc
                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataBind()
            End If

            If CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items.Count = 0 Then
                'CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items.Clear()

                par.cmd.CommandText = "select id,descrizione from vsa_doc_necessari order by descrizione asc"
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettore.Read
                    CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items.Add(New ListItem(par.IfNull(lettore("descrizione"), " "), par.IfNull(lettore("id"), -1)))
                End While
                lettore.Close()
            End If


            If CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items.Count > 1 Then
                par.cmd.CommandText = "select id_doc from vsa_doc_allegati where id_dichiarazione = " & lIdDichiarazione
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                While lettore.Read
                    CType(dic_Documenti1.FindControl("documAlleg"), HiddenField).Value = 1
                    CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items.FindByValue(lettore("id_doc")).Selected = True
                End While
                lettore.Close()
            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CaricaDocumenti() - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub CaricaPatrimMob()
        Try
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.ID AS IDCOMP,COMP_PATR_MOB_VSA.ID AS IDMOB,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_PATR_MOB_VSA.*,TIPOLOGIA_PATR_MOB.descrizione as TIPO_MOB FROM TIPOLOGIA_PATR_MOB,COMP_PATR_MOB_VSA,COMP_NUCLEO_VSA WHERE COMP_PATR_MOB_VSA.ID_COMPONENTE=COMP_NUCLEO_VSA.ID and COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " AND COMP_PATR_MOB_VSA.ID_TIPO=TIPOLOGIA_PATR_MOB.id (+) order by COMP_PATR_MOB_VSA.id_componente asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtMob = New Data.DataTable
            'Dim dtQuery As New Data.DataTable

            'dtMob.Columns.Add("idComp")
            'dtMob.Columns.Add("idMob")
            'dtMob.Columns.Add("COGNOME")
            'dtMob.Columns.Add("NOME")
            'dtMob.Columns.Add("TIPO_MOB")
            'dtMob.Columns.Add("CODICE")
            'dtMob.Columns.Add("INTERMEDIARIO")
            'dtMob.Columns.Add("IMPORTO")

            da.Fill(dtMob)
            da.Dispose()
            If dtMob.Rows.Count > 0 Then
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataSource = dtMob
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataBind()
            Else
                rowDT = dtMob.NewRow()

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
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Function CaricaPatrimImmob() As Boolean
        Try
            Dim patrImmob As Boolean = False
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.ID AS IDCOMP,COMP_PATR_IMMOB_VSA.ID AS IDIMMOB,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_PATR_IMMOB_VSA.*,TIPO_PIENA_PATR_IMMOB.descrizione as TIPO_PROPR,T_TIPO_PATR_IMMOB.DESCRIZIONE AS TIPO_IMMOB FROM tipo_piena_patr_immob,COMP_PATR_IMMOB_VSA,COMP_NUCLEO_VSA,T_TIPO_PATR_IMMOB WHERE T_TIPO_PATR_IMMOB.COD=COMP_PATR_IMMOB_VSA.ID_TIPO AND COMP_PATR_IMMOB_VSA.ID_COMPONENTE=COMP_NUCLEO_VSA.ID and COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " AND COMP_PATR_IMMOB_VSA.ID_TIPO_PROPRIETA=tipo_piena_patr_immob.id (+) order by COMP_PATR_IMMOB_VSA.id_componente asc"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtImmob = New Data.DataTable
            'Dim dtQuery As New Data.DataTable

            'dtMob.Columns.Add("idComp")
            'dtMob.Columns.Add("idMob")
            'dtMob.Columns.Add("COGNOME")
            'dtMob.Columns.Add("NOME")
            'dtMob.Columns.Add("TIPO_MOB")
            'dtMob.Columns.Add("CODICE")
            'dtMob.Columns.Add("INTERMEDIARIO")
            'dtMob.Columns.Add("IMPORTO")

            da.Fill(dtImmob)
            da.Dispose()
            If dtImmob.Rows.Count > 0 Then
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()
                patrImmob = True
            Else
                rowDT = dtImmob.NewRow()

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
            Response.Redirect("../../Errore.aspx", False)
        End Try

    End Function

    Private Sub CaricaRedditi()
        Try
            Dim rowDT As System.Data.DataRow
            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.ID AS IDCOMP,DOMANDE_redditi_VSA.ID AS IDREDD,COMP_NUCLEO_VSA.*,DOMANDE_redditi_VSA.*,(NVL(NON_IMPONIBILI,0)+ NVL(PENS_ESENTE,0)) AS PENSIONE2,NVL(AUTONOMO,0)+NVL(DOM_AG_FAB,0)+NVL(OCCASIONALI,0) AS AUTONOMO1,NVL(ONERI,0)+NVL(NO_ISEE,0) AS NO_ISEE,NVL(DIPENDENTE,0)+NVL(PENSIONE,0)+ NVL(AUTONOMO,0)+NVL(NON_IMPONIBILI,0)+  NVL(DOM_AG_FAB,0) + NVL(OCCASIONALI,0) + NVL(ONERI,0)+ NVL(PENS_ESENTE,0)+NVL(NO_ISEE,0) AS TOT_REDDITI FROM COMP_NUCLEO_VSA,DOMANDE_redditi_VSA " _
            & "where COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " AND COMP_NUCLEO_VSA.ID=DOMANDE_redditi_VSA.ID_COMPONENTE ORDER BY DOMANDE_redditi_VSA.ID ASC"
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtRedd = New Data.DataTable
            da.Fill(dtRedd)
            da.Dispose()
            If dtRedd.Rows.Count > 0 Then
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()

                'numComp.Value = dtComp.Rows.Count
            Else
                rowDT = dtRedd.NewRow()
                rowDT.Item("ID") = "-1"
                rowDT.Item("COGNOME") = "&nbsp"
                rowDT.Item("NOME") = "&nbsp"
                'rowDT.Item("COD_FISCALE") = "&nbsp"
                'rowDT.Item("DIPENDENTE") = "&nbsp"
                'rowDT.Item("AUTONOMO1") = "&nbsp"
                'rowDT.Item("PENSIONE") = "&nbsp"
                'rowDT.Item("PENSIONE2") = "&nbsp"
                'rowDT.Item("NO_ISEE") = "&nbsp"
                'rowDT.Item("TOT_REDDITI") = "&nbsp"
                dtRedd.Rows.Add(rowDT)

                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
            End If

            Dim rowDT2 As System.Data.DataRow
            par.cmd.CommandText = "SELECT COMP_NUCLEO_VSA.ID AS IDCOMP,COMP_DETRAZIONI_VSA.ID AS IDDETR,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME,COMP_DETRAZIONI_VSA.IMPORTO,T_TIPO_DETRAZIONI.descrizione,COMP_DETRAZIONI_VSA.IMPORTO AS TOT_DETRAZ FROM T_TIPO_DETRAZIONI,COMP_DETRAZIONI_VSA,COMP_NUCLEO_VSA WHERE COMP_DETRAZIONI_VSA.ID_COMPONENTE=COMP_NUCLEO_VSA.ID and COMP_NUCLEO_VSA.id_DICHIARAZIONE = " & lIdDichiarazione & " AND COMP_DETRAZIONI_VSA.ID_TIPO=T_TIPO_DETRAZIONI.cod (+) order by COMP_DETRAZIONI_VSA.id_componente asc"
            Dim da2 As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
            dtDetraz = New Data.DataTable
            da2.Fill(dtDetraz)
            da2.Dispose()
            If dtDetraz.Rows.Count > 0 Then
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
            Else
                rowDT2 = dtDetraz.NewRow()
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
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub



    Sub VisualizzaDichiarazione()
        Dim CT1 As DropDownList
        Dim cT As TextBox
        Dim CTT As Label
        Dim lIndiceAppoggio_0 As Long
        Dim lIndiceAppoggio_1 As Long
        Dim lIndiceAppoggio_2 As Long

        Dim RESIDENZA As String
        Dim SOMMA As Long
        Dim DESCRIZIONE As String
        Dim i As Integer
        Dim MIOPROGR As Integer
        Dim scriptblock As String

        Dim presDocManca As Integer
        Dim dataPresDocM As String = ""
        Dim fl_autorizza As Integer = 0

        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            HttpContext.Current.Session.Add("CONNESSIONE" & vIdConnessione, par.OracleConn)

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            ‘‘par.cmd.Transaction = par.myTrans

            If Session.Item("ANAGRAFE") = "0" Then
                imgAnagrafe.Visible = False
                imgAnagrafe.Attributes.Clear()
            Else
                imgAnagrafe.Visible = True
            End If

            par.cmd.CommandText = "SELECT BANDI_VSA.TIPO_BANDO,BANDI_VSA.DATA_INIZIO,BANDI_VSA.STATO FROM BANDI_VSA,DICHIARAZIONI_VSA WHERE DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND DICHIARAZIONI_VSA.ID_BANDO=BANDI_VSA.ID"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                If myReader("STATO") <> 1 And Fl_Integrazione <> "1" And Session.Item("ID_CAF") <> "6" Then
                    btnSalva.Visible = False
                    cmbAnnoReddituale.Enabled = False

                    imgStampa.Visible = False
                    btnApplica.Visible = False

                    DatiSolaLettura(Me)
                    DatiSolaLettura(Tab_Nucleo1)
                    DatiSolaLettura(dic_Documenti1)
                    DatiSolaLettura(Dic_Patrimonio1)
                    DatiSolaLettura(Dic_Reddito1)

                    cmbStato.Enabled = False
                    If Fl_US <> "1" Then
                        MessJQuery("Non è possibile apportare modifiche! Il bando a cui appartiene la domande è CHIUSO.", 0, "Attenzione")
                    End If
                Else
                    lIndice_Bando = par.IfNull(myReader("TIPO_BANDO"), "-1")

                End If
            End If
            myReader.Close()

            If SoloLettura = "1" Then
                btnSalva.Visible = False

                imgStampa.Visible = False
                btnApplica.Visible = False

                DatiSolaLettura(Me)
                DatiSolaLettura(Tab_Nucleo1)
                DatiSolaLettura(dic_Documenti1)
                DatiSolaLettura(Dic_Patrimonio1)
                DatiSolaLettura(Dic_Reddito1)
                ChFO.Enabled = False
                cmbStato.Enabled = False

                cmbAnnoReddituale.Enabled = False
            End If

            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.UNITA_ASSEGNATE WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND PROVENIENZA='Z' AND GENERATO_CONTRATTO=1"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                btnSalva.Visible = False

                imgStampa.Visible = False
                btnApplica.Visible = False

                DatiSolaLettura(Me)
                DatiSolaLettura(Tab_Nucleo1)
                DatiSolaLettura(dic_Documenti1)
                DatiSolaLettura(Dic_Patrimonio1)
                DatiSolaLettura(Dic_Reddito1)

                ChFO.Enabled = False
                cmbStato.Enabled = False
                cmbAnnoReddituale.Enabled = False
                MessJQuery("Non è possibile apportare modifiche! Il contratto è stato già inserito per questa dichiarazione. Se il contratto è ancora in BOZZA, eliminarlo e poi effettuare le modifiche sulla dichiarazione!", 0, "Attenzione")
            End If
            myReader.Close()

            Dim statto_domanda As String = ""

            '*********peppe modify 20/09/2011
            par.cmd.CommandText = "SELECT DOMANDE_BANDO_VSA.ID,DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA,T_MOTIVO_DOMANDA_VSA.DESCRIZIONE AS MOTIVO_DOM,T_CAUSALI_DOMANDA_VSA.DESCRIZIONE,T_CAUSALI_DOMANDA_VSA.COD AS IDCAUS,DICHIARAZIONI_VSA.MOD_PRESENTAZIONE,DICHIARAZIONI_VSA.ID_CAF,DOMANDE_BANDO_VSA.N_DISTINTA,CAF_WEB.COD_CAF,DOMANDE_BANDO_VSA.ID_STATO,DOMANDE_BANDO_VSA.FL_PRESENT_DOC_MANC,DOMANDE_BANDO_VSA.DATA_PRESENT_DOC_MANC,DOMANDE_BANDO_VSA.FL_AUTORIZZAZIONE,DICHIARAZIONI_VSA.PG_COLLEGATO FROM DOMANDE_BANDO_VSA,T_MOTIVO_DOMANDA_VSA,DICHIARAZIONI_VSA,CAF_WEB,T_CAUSALI_DOMANDA_VSA WHERE DICHIARAZIONI_VSA.ID_CAF=CAF_WEB.ID AND DICHIARAZIONI_VSA.ID=" & lIdDichiarazione & " AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID AND DOMANDE_BANDO_VSA.ID_MOTIVO_DOMANDA=T_MOTIVO_DOMANDA_VSA.ID(+) and DOMANDE_BANDO_VSA.ID_CAUSALE_DOMANDA=T_CAUSALI_DOMANDA_VSA.COD(+)"
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                descrMotivoDom = par.IfNull(myReader("MOTIVO_DOM"), "")
                modPresent = par.IfNull(myReader("MOD_PRESENTAZIONE"), "")
                sStato = myReader("id_stato")
                tipoRichiesta.Value = par.IfNull(myReader("ID_MOTIVO_DOMANDA"), "")
                tipoCausale.Value = par.IfNull(myReader("IDCAUS"), "")
                lIdDomanda = par.IfNull(myReader("ID"), "")
                If tipoRichiesta.Value = "2" Then
                    lblAvviso.Text = "ATTENZIONE, questa è una dichiarazione per " & par.IfNull(myReader("DESCRIZIONE"), "") & ". Aggiungere i nuovi componenti nell'apposita sezione!"
                Else
                    lblAvviso.Visible = False
                    imgAlert.Visible = False

                End If

                '''LBLENTE.Visible = True
                '''LBLENTE.Text = "Ente Ins.: " & par.IfNull(myReader("COD_CAF"), "")

                statto_domanda = par.IfNull(myReader("ID_STATO"), -1)
                If par.IfNull(myReader("ID_STATO"), -1) = "9" Or par.IfNull(myReader("ID_STATO"), -1) = "10" Then
                    btnSalva.Visible = False
                    imgStampa.Visible = False
                    btnApplica.Visible = False

                    DatiSolaLettura(Me)
                    DatiSolaLettura(Tab_Nucleo1)
                    DatiSolaLettura(dic_Documenti1)
                    DatiSolaLettura(Dic_Patrimonio1)
                    DatiSolaLettura(Dic_Reddito1)

                    cmbStato.Enabled = False

                    cmbAnnoReddituale.Enabled = False
                    MessJQuery("Non è possibile modificare. La domanda è IN ASSEGNAZIONE!", 0, "Attenzione")

                End If

                If Request.QueryString("GLocat") <> "" Then
                    ChFO.Visible = True
                    lblAvviso.Visible = False
                    imgAlert.Visible = False
                    IMGCanone.Visible = False
                    MenuStampe.Visible = False
                Else
                    If tipoRichiesta.Value = 5 Then
                        lblPGcoll.Visible = True
                        lblSlash.Visible = True
                        lblPGcoll.Text = par.IfNull(myReader("pg_collegato"), "")
                        PGcollegato = par.IfNull(myReader("pg_collegato"), "")

                    End If
                    ChFO.Visible = False
                    If tipoRichiesta.Value = 2 Or tipoRichiesta.Value = 3 Then
                        'IMGCanone.Visible = True
                        IMGCanone.Visible = False
                    Else
                        IMGCanone.Visible = False
                    End If
                End If

                presDocManca = myReader("FL_PRESENT_DOC_MANC")
                dataPresDocM = par.IfNull(myReader("DATA_PRESENT_DOC_MANC"), "")
                fl_autorizza = par.IfNull(myReader("FL_AUTORIZZAZIONE"), "")
            End If

            'RICAVO L'ID DELLA DICHIARAZIONE ASSOCIATA
            If tipoRichiesta.Value = "5" Then
                par.cmd.CommandText = "SELECT * from DICHIARAZIONI_VSA WHERE PG=" & PGcollegato
                myReader = par.cmd.ExecuteReader()
                If myReader.Read Then
                    lIdDichCollegata = par.IfNull(myReader("ID"), 0)
                End If
                myReader.Close()
                lblNumDich.Visible = True
                lblNumDich.Attributes.Add("onclick", "today = new Date();document.getElementById('H1').value='0';window.open('DichAUnuova.aspx?CH=2&ID=" & lIdDichCollegata & "','DichCollegata'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');")
                '= "<< Num. dich. associata <a href='javascript:void(0)' onclick=" & Chr(34) & "today = new Date();document.getElementById('H1').value='0';window.open('max.aspx?CH=2&ID=" & lIdDichCollegata & "','DichCollegata'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">" & PGcollegato & " </a> >>"
            End If

            par.cmd.CommandText = "SELECT * from DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & lIdDomanda
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                id_contr.Value = par.IfNull(myReader("NUM_CONTRATTO"), "")
                codUI.Value = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "")
            End If
            myReader.Close()

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


            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneNasSott, "ID", "NOME", True)
            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneResSott, "ID", "NOME", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrNasSott, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrResSott, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", cmbTipoIResSott, "COD", "DESCRIZIONE", True)


            Session.Item("STAMPATO") = "0"

            If Fl_US <> "1" Then
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Else
                par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione
            End If
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then
                RiempiCmbAnniRedd(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                '''CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                lIndice_Bando = myReader("ID_BANDO")
                lblPG.Text = par.IfNull(myReader("pg"), "")

                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                '''CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                '''CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))

                annoRedd = cmbAnnoReddituale.SelectedItem.Value

                cmbAnnoReddituale.SelectedValue = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                '''CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text
                '''CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                '''CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                '''CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value

                '''CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                '''CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))

                CType(dic_Documenti1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")
                CType(Dic_Reddito1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader("minori_carico"), "0")
                '''CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))
                '''CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))


                If par.IfNull(myReader("TIPO"), "0") = "1" Then
                    ChFO.Checked = True
                    lblDomPG.Text = "ISEE"
                    lblDomAssociata.Text = Format(CDbl(par.IfNull(myReader("ISEE"), "0")), "##,##0.00")
                Else
                    ChFO.Checked = False
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

                If par.IfNull(myReader("FL_GIA_TITOLARE"), "0") = "0" Then
                    '''CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = False
                Else
                    '''CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = True
                End If

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
                        CTT = lblComNas
                        CTT.Visible = False
                        CTT = lblPr
                        CTT.Visible = False

                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = cmbPrNas
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True

                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas, "ID", "NOME", True)

                        CT1 = cmbComuneNas
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
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
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = cmbPrRes
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True

                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", cmbComuneRes, "ID", "NOME", True)

                        '''par.RiempiDList(cmbComuneRes, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = cmbComuneRes
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    End If
                End If
                myReader1.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDomAssociata.Text = par.IfNull(myReader("PG"), "")
                codcontr2.Value = par.IfNull(myReader("COD_CONTRATTO_SCAMBIO"), "")
            End If
            myReader.Close()
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                txtCognome.Text = par.IfNull(myReader("COGNOME"), "")
                txtNome.Text = par.IfNull(myReader("NOME"), "")
                txtCF.Text = par.IfNull(myReader("COD_FISCALE"), "")
                txtDataNascita.Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                '''CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

                txtCognomeS.Text = par.IfNull(myReader("COGNOME"), "")
                txtNomeS.Text = par.IfNull(myReader("NOME"), "")
                txtDataNascitaSott.Text = par.FormattaData(par.IfNull(myReader("DATA_NAS"), ""))

                cT = txtIndResSott
                cT.Text = par.IfNull(myReader("IND"), "")

                cT = txtCivicoResSott
                cT.Text = par.IfNull(myReader("CIVICO"), "")

                cT = txtTelResSott
                cT.Text = par.IfNull(myReader("TELEFONO"), "")

                cT = txtCAPResSott
                cT.Text = par.IfNull(myReader("CAP_RES"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = cmbNazioneNasSott
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    CT1 = cmbPrNasSott
                    CT1.Visible = False
                    CT1 = cmbComuneNas2
                    CT1.Visible = False
                    CTT = lblPrNasSott
                    CTT.Visible = False
                    CTT = lblComuneNas2
                    CTT.Visible = False
                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = cmbPrNasSott
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas2, "ID", "NOME", True)
                    CT1 = cmbComuneNas2
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = cmbTipoIResSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = cmbNazioneResSott
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    CT1 = cmbPrResSott
                    CT1.Visible = False
                    CT1 = cmbComuneResSott
                    CT1.Visible = False
                    CTT = lblComuneResSott
                    CTT.Visible = False
                    CTT = lblPrResSott
                    CTT.Visible = False

                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = cmbPrResSott
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneResSott, "ID", "NOME", True)

                    CT1 = cmbComuneResSott
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    'cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                    'cT.Text = myReader("CAP")
                End If
            End If
            myReader.Close()


            txtbinserito.Value = "1"

            'RAGGRUPPAMENTO FUNZIONI CARICAMENTO
            CaricaNucleo()
            CaricaNucleoSpese()
            CaricaDocumenti()
            CaricaRedditi()
            CaricaPatrimMob()
            CaricaPatrimImmob()


            If docMancante = 1 Then
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Enabled = True
            End If

            If presDocManca = 1 Then
                docMancante = 0
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Checked = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Text = par.FormattaData(dataPresDocM)

                '''CType(Dic_Note1.FindControl("Button1"), Button).Enabled = False
                '''CType(Dic_Note1.FindControl("Button2"), Button).Enabled = False
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Enabled = True
            End If

            If par.IfEmpty(tipoRichiesta.Value, -1) = 2 Or par.IfEmpty(tipoRichiesta.Value, -1) = 3 Then
                If fl_autorizza = 1 Then
                    IMGCanone.Visible = False
                Else
                    'IMGCanone.Visible = True
                    IMGCanone.Visible = False
                End If
            End If

            par.cmd.CommandText = "SELECT * FROM VSA_SOPRALLUOGHI WHERE ID_DOMANDA=" & lIdDomanda
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sopralluogo = True
            Else
                sopralluogo = False
            End If
            myReader.Close()

            'Funzione per creare il Menu Stampe Dinamico come in DOMANDA
            If Request.QueryString("GLocat") = "" Then
                SetMenuStampe(tipoRichiesta.Value)
                lblTipoDom.Text = "per " & descrMotivoDom.ToLower
            End If
            If tipoRichiesta.Value = "4" Then
                MenuStampe.Visible = False
            End If

            Session.Add("LAVORAZIONE", "1")

        Catch EX1 As Oracle.DataAccess.Client.OracleException
            '''If EX1.Number = 54 Then
            ' ''scriptblock = "<script language='javascript' type='text/javascript'>" _
            ' ''& "alert('Pratica aperta da un altro utente. Non è possibile effettuare modifiche!');" _
            ' ''& "</script>"
            ' ''If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript4")) Then
            ' ''    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript4", scriptblock)
            ' ''End If
            btnSalva.Visible = False
            imgStampa.Visible = False

            cmbStato.Enabled = False
            cmbAnnoReddituale.Enabled = False

            nonstampare.Value = "1"

            DatiSolaLettura(Me)
            DatiSolaLettura(Tab_Nucleo1)
            DatiSolaLettura(dic_Documenti1)
            DatiSolaLettura(Dic_Patrimonio1)
            DatiSolaLettura(Dic_Reddito1)

            MessJQuery("Pratica aperta da un altro utente. Non è possibile effettuare modifiche!", 0, "Attenzione")


            'RICAVO L'ID DELLA DICHIARAZIONE ASSOCIATA
            If tipoRichiesta.Value = "5" Then
                par.cmd.CommandText = "SELECT * from DICHIARAZIONI_VSA WHERE PG=" & PGcollegato
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    lIdDichCollegata = par.IfNull(myReader1("ID"), 0)
                End If
                myReader1.Close()
                lblNumDich.Visible = True
                lblNumDich.Attributes.Add("onclick", "today = new Date();document.getElementById('H1').value='0';window.open('DichAUnuova.aspx?CH=2&ID=" & lIdDichCollegata & "','DichCollegata'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');")
                '= "<< Num. dich. associata <a href='javascript:void(0)' onclick=" & Chr(34) & "today = new Date();document.getElementById('H1').value='0';window.open('max.aspx?CH=2&ID=" & lIdDichCollegata & "','DichCollegata'+ today.getMinutes() + today.getSeconds(),'top=250,left=650,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');" & Chr(34) & ">" & PGcollegato & " </a> >>"
            End If

            par.cmd.CommandText = "SELECT * from DOMANDE_VSA_ALLOGGIO WHERE ID_DOMANDA=" & lIdDomanda
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                id_contr.Value = par.IfNull(myReader("NUM_CONTRATTO"), "")
                codUI.Value = par.IfNull(myReader("COD_UNITA_IMMOBILIARE"), "")
            End If
            myReader.Close()

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


            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneNasSott, "ID", "NOME", True)
            par.caricaComboBox("SELECT * FROM COMUNI_NAZIONI WHERE SIGLA IN ('I','C','E') ORDER BY NOME ASC", cmbNazioneResSott, "ID", "NOME", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrNasSott, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DISTINCT SIGLA FROM COMUNI_NAZIONI ORDER BY SIGLA ASC", cmbPrResSott, "SIGLA", "SIGLA", True)
            par.caricaComboBox("SELECT DESCRIZIONE,COD FROM T_TIPO_INDIRIZZO ORDER BY DESCRIZIONE ASC", cmbTipoIResSott, "COD", "DESCRIZIONE", True)


            Session.Item("STAMPATO") = "0"

            'If Fl_US <> "1" Then
            'par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            'Else
            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione
            'End If
            myReader = par.cmd.ExecuteReader()
            lblPG.ToolTip = lIdDichiarazione
            If myReader.Read() Then
                RiempiCmbAnniRedd(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))
                '''CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                lIndice_Bando = myReader("ID_BANDO")
                lblPG.Text = par.IfNull(myReader("pg"), "")

                txtDataPG.Text = par.FormattaData(par.IfNull(myReader("data_pg"), ""))
                '''CType(Dic_Dichiarazione1.FindControl("txtId"), TextBox).Text = lIdDichiarazione
                '''CType(Dic_Dichiarazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("data"), ""))

                annoRedd = cmbAnnoReddituale.SelectedItem.Value

                cmbAnnoReddituale.SelectedValue = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")

                '''CType(Dic_Reddito_Conv1.FindControl("label3"), Label).Text = "REDDITO DICHIARATO/PERCEPITO PER L'ANNO D'IMPOSTA " & CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text
                '''CType(Dic_Reddito1.FindControl("lblAnnoR"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                '''CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = cmbAnnoReddituale.SelectedItem.Value
                '''CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = cmbAnnoReddituale.SelectedItem.Value

                '''CType(Dic_Patrimonio1.FindControl("lblDataMob"), Label).Text = par.IfNull(myReader("ANNO_SIT_ECONOMICA"), "")
                '''CType(Dic_Patrimonio1.FindControl("lblDataImm"), Label).Text = Val(par.IfNull(myReader("ANNO_SIT_ECONOMICA"), ""))

                CType(dic_Documenti1.FindControl("txtNote"), TextBox).Text = par.IfNull(myReader("NOTE"), "")
                CType(Dic_Reddito1.FindControl("txtMinori"), TextBox).Text = par.IfNull(myReader("minori_carico"), "0")
                '''CType(Dic_Integrazione1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_INT_ERP"), ""))
                '''CType(Dic_Sottoscrittore1.FindControl("txtData1"), TextBox).Text = par.FormattaData(par.IfNull(myReader("DATA_S"), ""))


                If par.IfNull(myReader("TIPO"), "0") = "1" Then
                    ChFO.Checked = True
                    lblDomPG.Text = "ISEE"
                    lblDomAssociata.Text = Format(CDbl(par.IfNull(myReader("ISEE"), "0")), "##,##0.00")
                Else
                    ChFO.Checked = False
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

                If par.IfNull(myReader("FL_GIA_TITOLARE"), "0") = "0" Then
                    '''CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = False
                Else
                    '''CType(Dic_Dichiarazione1.FindControl("chTitolare"), CheckBox).Checked = True
                End If

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
                        CTT = lblComNas
                        CTT.Visible = False
                        CTT = lblPr
                        CTT.Visible = False

                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = cmbPrNas
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True

                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas, "ID", "NOME", True)

                        CT1 = cmbComuneNas
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
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
                    Else
                        CT1.Items.FindByText("ITALIA").Selected = True
                        CT1 = cmbPrRes
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("SIGLA")).Selected = True

                        par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", cmbComuneRes, "ID", "NOME", True)

                        '''par.RiempiDList(cmbComuneRes, par.OracleConn, "cmbComuneRes", "SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader1("SIGLA") & "' ORDER BY NOME ASC", "NOME", "ID")
                        CT1 = cmbComuneRes
                        CT1.SelectedIndex = -1
                        CT1.Items.FindByText(myReader1("NOME")).Selected = True
                    End If
                End If
                myReader1.Close()
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                lblDomAssociata.Text = par.IfNull(myReader("PG"), "")
                codcontr2.Value = par.IfNull(myReader("COD_CONTRATTO_SCAMBIO"), "")
            End If
            myReader.Close()
            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_VSA where PROGR=0 AND id_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                txtCognome.Text = par.IfNull(myReader("COGNOME"), "")
                txtNome.Text = par.IfNull(myReader("NOME"), "")
                txtCF.Text = par.IfNull(myReader("COD_FISCALE"), "")
                txtDataNascita.Text = par.FormattaData(par.IfNull(myReader("DATA_NASCITA"), ""))
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM SOTTOSCRITTORI_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then

                '''CType(Dic_Sottoscrittore1.FindControl("txtS"), TextBox).Text = "1"

                txtCognomeS.Text = par.IfNull(myReader("COGNOME"), "")
                txtNomeS.Text = par.IfNull(myReader("NOME"), "")
                txtDataNascitaSott.Text = par.FormattaData(par.IfNull(myReader("DATA_NAS"), ""))

                cT = txtIndResSott
                cT.Text = par.IfNull(myReader("IND"), "")

                cT = txtCivicoResSott
                cT.Text = par.IfNull(myReader("CIVICO"), "")

                cT = txtTelResSott
                cT.Text = par.IfNull(myReader("TELEFONO"), "")

                cT = txtCAPResSott
                cT.Text = par.IfNull(myReader("CAP_RES"), "")

                lIndiceAppoggio_0 = myReader("ID_LUOGO_NAS")
                lIndiceAppoggio_1 = myReader("ID_LUOGO_RES")
                lIndiceAppoggio_2 = myReader("ID_TIPO_IND")
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_0
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = cmbNazioneNasSott
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    CT1 = cmbPrNasSott
                    CT1.Visible = False
                    CT1 = cmbComuneNas2
                    CT1.Visible = False
                    CTT = lblPrNasSott
                    CTT.Visible = False
                    CTT = lblComuneNas2
                    CTT.Visible = False
                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = cmbPrNasSott
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneNas2, "ID", "NOME", True)
                    CT1 = cmbComuneNas2
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True
                End If
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM T_TIPO_INDIRIZZO WHERE COD=" & lIndiceAppoggio_2
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = cmbTipoIResSott
                CT1.SelectedIndex = -1
                CT1.Items.FindByText(myReader("DESCRIZIONE")).Selected = True
            End If
            myReader.Close()

            par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE ID=" & lIndiceAppoggio_1
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CT1 = cmbNazioneResSott
                CT1.SelectedIndex = -1
                If myReader("SIGLA") = "E" Or myReader("SIGLA") = "C" Then
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    CT1 = cmbPrResSott
                    CT1.Visible = False
                    CT1 = cmbComuneResSott
                    CT1.Visible = False
                    CTT = lblComuneResSott
                    CTT.Visible = False
                    CTT = lblPrResSott
                    CTT.Visible = False

                Else
                    CT1.Items.FindByText("ITALIA").Selected = True
                    CT1 = cmbPrResSott
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("SIGLA")).Selected = True
                    par.caricaComboBox("SELECT ID,NOME FROM COMUNI_NAZIONI WHERE SIGLA='" & myReader("SIGLA") & "' ORDER BY NOME ASC", cmbComuneResSott, "ID", "NOME", True)

                    CT1 = cmbComuneResSott
                    CT1.SelectedIndex = -1
                    CT1.Items.FindByText(myReader("NOME")).Selected = True

                    'cT = Dic_Sottoscrittore1.FindControl("txtCAPRes")
                    'cT.Text = myReader("CAP")
                End If
            End If
            myReader.Close()


            txtbinserito.Value = "1"

            'RAGGRUPPAMENTO FUNZIONI CARICAMENTO
            CaricaNucleo()
            CaricaNucleoSpese()
            CaricaDocumenti()
            CaricaRedditi()
            CaricaPatrimMob()
            CaricaPatrimImmob()


            If docMancante = 1 Then
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Enabled = True
            End If

            If presDocManca = 1 Then
                docMancante = 0
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Checked = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Text = par.FormattaData(dataPresDocM)

                '''CType(Dic_Note1.FindControl("Button1"), Button).Enabled = False
                '''CType(Dic_Note1.FindControl("Button2"), Button).Enabled = False
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Enabled = True
            End If

            If par.IfEmpty(tipoRichiesta.Value, -1) = 2 Or par.IfEmpty(tipoRichiesta.Value, -1) = 3 Then
                If fl_autorizza = 1 Then
                    IMGCanone.Visible = False
                Else
                    'IMGCanone.Visible = True
                    IMGCanone.Visible = False
                End If
            End If

            par.cmd.CommandText = "SELECT * FROM VSA_SOPRALLUOGHI WHERE ID_DOMANDA=" & lIdDomanda
            myReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                sopralluogo = True
            Else
                sopralluogo = False
            End If
            myReader.Close()

            'Funzione per creare il Menu Stampe Dinamico come in DOMANDA
            If Request.QueryString("GLocat") = "" Then
                SetMenuStampe(tipoRichiesta.Value)
                lblTipoDom.Text = "per " & descrMotivoDom.ToLower
            End If
            If tipoRichiesta.Value = "4" Then
                MenuStampe.Visible = False
            End If

            CaricaNucleo()
            CaricaNucleoSpese()
            CaricaDocumenti()
            CaricaRedditi()
            CaricaPatrimMob()
            CaricaPatrimImmob()

            


            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            imgStampa.Enabled = False
            btnSalva.Enabled = False
            'Label4.Visible = True
            'Label4.Text = ex.Message
            par.OracleConn.Close()
            HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
            HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
            Session.Item("LAVORAZIONE") = "0"
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza: Visualizza Dichiarazione - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)

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
            'lblErrore.Text = ex.Message
            'lblErrore.Visible = True
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
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Private Sub SetMenuStampe(ByVal TipoDomanda As Integer)

        '0) Voltura;
        '1) Subentro;
        '2) Ampliamento;
        '3) Riduzione Canone;
        '4) Cambio art22 c10 rr 1/2004
        '5) Cambio Diretto
        Try
            Select Case TipoDomanda
                Case 0
                    MenuVoltura()
                Case 1, 6
                    MenuSubentro()
                Case 2
                    MenuAmpliamento()
                Case 3
                    MenuRiduzione()
                Case 5
                    MenuCambioCons()
                Case 7
                    MenuOspitalita()
            End Select

        Catch ex As Exception
            Response.Write("<script>document.location.href=""../../ErrorPage.aspx""</script>")
        End Try
    End Sub

    Private Sub MenuSubentro()
        Dim item As MenuItem
        If tipoRichiesta.Value = 1 Then


            item = New MenuItem("Avvio Procedimento", "AvvProcSUB", "", "javascript:AvvioProcSUB();")
            MenuStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Domanda di Subentro", "DomandaSUB", "", "javascript:DomSubentro();")
            MenuStampe.Items(0).ChildItems.Add(item)

            If tipoCausale.Value = 2 Then
                item = New MenuItem("Perm. Requisiti per rinunciante", "PermReqRSUB", "", "javascript:CertRinunciante();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If


            If docMancante = 1 Then
                item = New MenuItem("Richiesta Doc.Mancante", "DocMancanteSUB", "", "javascript:DocMancanteSUB();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If

            If sopralluogo = True Then
                item = New MenuItem("Sopralluogo", "SoprallSUB", "", "javascript:Sopralluogo();")
                MenuStampe.Items(0).ChildItems.Add(item)

                item = New MenuItem("Comunicaz. Sopralluogo", "ComSopralSUB", "", "javascript:ComSoprall();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If
        Else
            item = New MenuItem("Richiesta variazione assegn.", "DomandaSUBFFO", "", "javascript:DomSubentroFFOO();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("ELENCO STAMPE", "ElStamp", "", "javascript:ElStampe();")
        MenuStampe.Items(0).ChildItems.Add(item)

    End Sub

    Private Sub MenuAmpliamento()
        Dim item As MenuItem
        If tipoCausale.Value <> 30 Then
            item = New MenuItem("Avvio Procedimento", "AvvioProcAMPL", "", "javascript:AvvProcedim();")
            MenuStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Stato Famiglia Ospite", "StFamigliaAMPL", "", "javascript:AUcertStFamiglia();")
            MenuStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Permanenza Requisiti ERP (titolare)", "PermanenzaAMPL1", "", "javascript:PermReqERP();")
            MenuStampe.Items(0).ChildItems.Add(item)

            If newComp = 1 Then
                item = New MenuItem("Permanenza Requisiti ERP (ospite)", "PermanenzaAMPL2", "", "javascript:PermReqERP2();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If

            If sopralluogo = True Then
                item = New MenuItem("Sopralluogo", "SoprallAMPL", "", "javascript:SopralluogoAMPL();")
                MenuStampe.Items(0).ChildItems.Add(item)

                item = New MenuItem("Comunicazione Sospensione Sopralluogo", "ComSoprallAMPL", "", "javascript:ComSoprallAMPL();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If

            If docMancante = 1 Then
                item = New MenuItem("Richiesta Doc.Mancante", "DocMancanteAMPL", "", "javascript:DocMancante();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If

            If tipoCausale.Value = 4 And newComp = 1 Then
                item = New MenuItem("Conviv. More Uxorio", "MoreUxorioAMPL", "", "javascript:MoreUxorio();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If

            If tipoCausale.Value = 5 Then
                item = New MenuItem("Conviv. per Assistenza", "AssistenzaAMPL", "", "javascript:Assistenza();")
                MenuStampe.Items(0).ChildItems.Add(item)
            End If
        End If
        item = New MenuItem("ELENCO STAMPE", "ElStamp", "", "javascript:ElStampe();")
        MenuStampe.Items(0).ChildItems.Add(item)

    End Sub

    Private Sub MenuRiduzione()
        Dim item As MenuItem
        If modPresent = "0" Then
            item = New MenuItem("Ricezione Richiesta", "RichRC", "", "javascript:RicezRichiesta();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("Avvio Procedimento", "AvvProcRC", "", "javascript:AvvioProc();")
        MenuStampe.Items(0).ChildItems.Add(item)

        'If CType(Dic_Note1.FindControl("documMancante"), HiddenField).Value = 1 Then
        '    item = New MenuItem("Richiesta Doc.Mancante", "DocMancRC", "", "javascript:StampaDoc();")
        '    MenuStampe.Items(0).ChildItems.Add(item)
        'End If

        'item = New MenuItem("Mod.Autocert.Redditi", "AutoCertRC", "", "javascript:AutoCert();")
        'MenuStampe.Items(0).ChildItems.Add(item)

        item = New MenuItem("ELENCO STAMPE", "ElStamp", "", "javascript:ElStampe();")
        MenuStampe.Items(0).ChildItems.Add(item)

    End Sub

    Private Sub MenuOspitalita()

        Dim item As MenuItem

        item = New MenuItem("Avvio Procedimento", "AvvioProcOSP", "", "javascript:AvvioProcOSP();")
        MenuStampe.Items(0).ChildItems.Add(item)

        item = New MenuItem("Autocertificazione Stato di Famiglia", "StFamigliaOSP", "", "javascript:StFamigliaOSP();")
        MenuStampe.Items(0).ChildItems.Add(item)

        If docMancante = 1 Then
            item = New MenuItem("Richiesta Doc.Mancante", "DocMancanteOSP", "", "javascript:DocMancanteOSP();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        If sopralluogo = True Then
            item = New MenuItem("Sopralluogo", "SopralluogoOSP", "", "javascript:SopralluogoOSP();")
            MenuStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Comunicaz. Sopralluogo", "ComSoprallOSP", "", "javascript:ComSoprallOSP();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("ELENCO STAMPE", "ElStamp", "", "javascript:ElStampe();")
        MenuStampe.Items(0).ChildItems.Add(item)

    End Sub


    Private Sub MenuVoltura()
        Dim item As MenuItem

        If modPresent = "0" Then
            item = New MenuItem("Ricezione Richiesta", "RicezRicVOL", "", "javascript:RichVoltura();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("Avvio Procedimento", "AvvProcedVOL", "", "javascript:AvvProcVoltura();")
        MenuStampe.Items(0).ChildItems.Add(item)

        If sopralluogo = True Then
            item = New MenuItem("Sopralluogo", "ModuloSoprallVOL", "", "javascript:ModSoprall();")
            MenuStampe.Items(0).ChildItems.Add(item)

            item = New MenuItem("Comunicaz. Sopralluogo", "SoprUtenteVOL", "", "javascript:SoprallUtente();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("ELENCO STAMPE", "ElStamp", "", "javascript:ElStampe();")
        MenuStampe.Items(0).ChildItems.Add(item)

    End Sub

    Private Sub MenuCambioCons()
        Dim item As MenuItem

        If modPresent = "0" Then
            item = New MenuItem("Richiesta Cambio Consensuale", "RichCAMB", "", "javascript:RichCAMB();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("Dichiarazione Permanenza Requisiti ERP", "DichPermanenza", "", "javascript:DichPermanenzaReq();")
        MenuStampe.Items(0).ChildItems.Add(item)


        If docMancante = 1 Then
            item = New MenuItem("Richiesta Doc.Mancante", "DocMancCAMB", "", "javascript:DocMancanteCAMB();")
            MenuStampe.Items(0).ChildItems.Add(item)
        End If

        item = New MenuItem("Avvio Procedimento", "AvvProcedCAMB", "", "javascript:AvvioProcCAMB();")
        MenuStampe.Items(0).ChildItems.Add(item)


        item = New MenuItem("Sopralluogo", "SoprallCAMB", "", "javascript:SopralluogoCAMB();")
        MenuStampe.Items(0).ChildItems.Add(item)

        item = New MenuItem("Comunicaz. Sopralluogo", "ComSoprallCAMB", "", "javascript:ComSoprallCAMB();")
        MenuStampe.Items(0).ChildItems.Add(item)


        item = New MenuItem("ELENCO STAMPE", "ElStamp", "", "javascript:ElStampe();")
        MenuStampe.Items(0).ChildItems.Add(item)

    End Sub

    'Private Sub DatiSoloLettura()

    '    ChNatoEstero.Enabled = False
    '    ChCittadinanza.Enabled = False
    '    txtAlloggio.Enabled = False
    '    txtCF.Enabled = False
    '    txtCAPRes.Enabled = False
    '    txtCIData.Enabled = False
    '    txtCINum.Enabled = False
    '    txtCIRilascio.Enabled = False
    '    txtCivicoRes.Enabled = False
    '    txtCodConvocazione.Enabled = False
    '    txtCognome.Enabled = False
    '    txtCSData.Enabled = False
    '    txtCSNum.Enabled = False
    '    txtDataNascita.Enabled = False
    '    txtDataPG.Enabled = False
    '    txtFoglio.Enabled = False
    '    txtIndRes.Enabled = False
    '    txtMappale.Enabled = False
    '    txtNome.Enabled = False
    '    txtPiano.Enabled = False
    '    txtPosizione.Enabled = False
    '    txtPSData.Enabled = False
    '    txtPSNum.Enabled = False
    '    txtPSRinnovo.Enabled = False
    '    txtPSScade.Enabled = False
    '    txtScala.Enabled = False
    '    txtSub.Enabled = False
    '    txtTelRes.Enabled = False
    '    lblISEE.Enabled = False
    '    lblPG.Enabled = False
    '    cmbComuneNas.Enabled = False
    '    cmbComuneRes.Enabled = False
    '    cmbLavorativa.Enabled = False
    '    cmbNazioneNas.Enabled = False
    '    cmbNazioneRes.Enabled = False
    '    cmbPrNas.Enabled = False
    '    cmbPrRes.Enabled = False
    '    cmbTipoDocumento.Enabled = False
    '    cmbTipoIRes.Enabled = False

    '    'For Each ctrl As Control In Me.Controls
    '    '    If TypeOf ctrl Is DropDownList Then
    '    '        DirectCast(ctrl, DropDownList).Enabled = False
    '    '    End If
    '    '    If TypeOf ctrl Is TextBox Then
    '    '        DirectCast(ctrl, TextBox).Enabled = False
    '    '    End If
    '    '    If TypeOf ctrl Is CheckBox Then
    '    '        DirectCast(ctrl, CheckBox).Enabled = False
    '    '    End If
    '    'Next
    '    For Each ctrl As Control In Me.Tab_Nucleo1.Controls
    '        If TypeOf ctrl Is Image Then
    '            DirectCast(ctrl, Image).Visible = False
    '        End If
    '    Next
    '    For Each ctrl As Control In Me.Dic_Reddito1.Controls
    '        If TypeOf ctrl Is TextBox Then
    '            DirectCast(ctrl, TextBox).Enabled = False
    '        End If
    '        If TypeOf ctrl Is Image Then
    '            DirectCast(ctrl, Image).Visible = False
    '        End If
    '    Next
    '    For Each ctrl As Control In Me.Dic_Patrimonio1.Controls
    '        If TypeOf ctrl Is TextBox Then
    '            DirectCast(ctrl, TextBox).Enabled = False
    '        End If
    '        If TypeOf ctrl Is Image Then
    '            DirectCast(ctrl, Image).Visible = False
    '        End If
    '    Next
    '    For Each ctrl As Control In Me.Tab_InfoContratto1.Controls
    '        If TypeOf ctrl Is TextBox Then
    '            DirectCast(ctrl, TextBox).Enabled = False
    '        End If
    '        If TypeOf ctrl Is CheckBox Then
    '            DirectCast(ctrl, CheckBox).Enabled = False
    '        End If
    '        If TypeOf ctrl Is DropDownList Then
    '            DirectCast(ctrl, DropDownList).Enabled = False
    '        End If
    '    Next
    '    For Each ctrl As Control In Me.dic_Documenti1.Controls
    '        If TypeOf ctrl Is TextBox Then
    '            DirectCast(ctrl, TextBox).Enabled = False
    '        End If
    '        If TypeOf ctrl Is Image Then
    '            DirectCast(ctrl, Image).Visible = False
    '        End If
    '        If TypeOf ctrl Is ImageButton Then
    '            DirectCast(ctrl, ImageButton).Visible = False
    '        End If
    '    Next

    'End Sub

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

    'Public Sub EliminaComponente()
    '    Try
    '        If confCanc.value = 1 Then

    '            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
    '            par.SettaCommand(par)

    '            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
    '            ‘‘par.cmd.Transaction = par.myTrans

    '            Dim k As Integer = 0
    '            Do While k < dtComp.Rows.Count
    '                Dim rowScart As Data.DataRow = dtComp.Rows(k)
    '                If rowScart.ItemArray(0) = idComp.Value Then
    '                    If rowScart.ItemArray(2) = "0" Then
    '                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "alert('Non è possibile eliminare il dichiarante!');", True)
    '                        MessJQuery("Non è possibile eliminare il dichiarante!", 0, "Attenzione")
    '                        Exit Try
    '                    Else
    '                        rowScart.Delete()
    '                    End If
    '                End If
    '                k = k + 1
    '            Loop
    '            dtComp.AcceptChanges()

    '            'ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "Verifica();", True)

    '            par.cmd.CommandText = "DELETE FROM COMP_NUCLEO_VSA WHERE ID=" & idComp.Value
    '            par.cmd.ExecuteNonQuery()

    '            Dim k2 As Integer = 0
    '            Do While k2 < dtCompSpese.Rows.Count
    '                Dim rowScart As Data.DataRow = dtCompSpese.Rows(k2)
    '                If rowScart.ItemArray(1) = idComp.Value Then
    '                    rowScart.Delete()
    '                End If
    '                k2 = k2 + 1
    '            Loop
    '            dtCompSpese.AcceptChanges()

    '            par.cmd.CommandText = "DELETE FROM UTENZA_COMP_ELENCO_SPESE WHERE ID_componente=" & idComp.Value
    '            par.cmd.ExecuteNonQuery()


    '            CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataSource = dtComp
    '            CType(Tab_Nucleo1.FindControl("DataGridComponenti"), DataGrid).DataBind()

    '            CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataSource = dtCompSpese
    '            CType(Tab_Nucleo1.FindControl("DataGridSpese"), DataGrid).DataBind()
    '            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
    '            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
    '            confCanc.Value = 0
    '            EliminaRiferimentiComp()
    '            funzioneSalva()

    '        Else
    '            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
    '            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
    '        End If


    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
    '        Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaComponente() " & ex.Message)
    '        Response.Redirect("../../Errore.aspx", False)
    '    End Try
    'End Sub

    Protected Sub EliminaRiferimentiComp()
        Try
            Dim trovato As Integer = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
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
                par.cmd.CommandText = "DELETE FROM DOMANDE_REDDITI_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & idComp.Value
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
                par.cmd.CommandText = "DELETE FROM COMP_DETRAZIONI_VSA WHERE ID_COMPONENTE=" & idComp.Value
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
                par.cmd.CommandText = "DELETE FROM COMP_PATR_MOB_VSA WHERE ID_COMPONENTE=" & idComp.Value
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
                par.cmd.CommandText = "DELETE FROM COMP_PATR_IMMOB_VSA WHERE ID_COMPONENTE=" & idComp.Value
                par.cmd.ExecuteNonQuery()
            End If

            CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
            CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()


        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaRiferimentiComp() " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try

    End Sub



    Public Sub EliminaDocumenti()
        Try
            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

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



                CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value = Replace((CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value), vbCrLf, "")

                'CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value = CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value


                par.cmd.CommandText = "DELETE FROM VSA_DOC_MANCANTI WHERE DESCRIZIONE like '" & par.PulisciStrSql(par.Elimina160(CType(dic_Documenti1.FindControl("descrizione"), HiddenField).Value)) & "%'" & " AND ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(dic_Documenti1.FindControl("idDoc"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataSource = dtDoc
                CType(dic_Documenti1.FindControl("DataGridDocum"), DataGrid).DataBind()
                '  ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ' ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0

                funzioneSalva()
                CType(dic_Documenti1.FindControl("idDoc"), HiddenField).Value = "-1"
            Else
                '  ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                '    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
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
                ‘‘par.cmd.Transaction = par.myTrans

                Dim k As Integer = 0
                Do While k < dtDetraz.Rows.Count
                    Dim rowScart As Data.DataRow = dtDetraz.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Reddito1.FindControl("idDetraz"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtDetraz.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM COMP_DETRAZIONI_VSA WHERE ID=" & CType(Dic_Reddito1.FindControl("idDetraz"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataSource = dtDetraz
                CType(Dic_Reddito1.FindControl("DataGridDetraz"), DataGrid).DataBind()
                ' ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                funzioneSalva()

            Else
                '       ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
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
                ‘‘par.cmd.Transaction = par.myTrans

                Dim k As Integer = 0
                Do While k < dtRedd.Rows.Count
                    Dim rowScart As Data.DataRow = dtRedd.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Reddito1.FindControl("idReddito"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtRedd.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM DOMANDE_REDDITI_VSA WHERE ID=" & CType(Dic_Reddito1.FindControl("idReddito"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_REDDITO_VSA WHERE ID_COMPONENTE=" & CType(Dic_Reddito1.FindControl("idCompReddito"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "DELETE FROM COMP_ALTRI_REDDITI_VSA WHERE ID_COMPONENTE=" & CType(Dic_Reddito1.FindControl("idCompReddito"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataSource = dtRedd
                CType(Dic_Reddito1.FindControl("DataGridRedditi"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                funzioneSalva()

            Else
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)



            End If

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - EliminaRedditi() " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        End Try
    End Sub

    Public Sub EliminaMobili()
        Try

            If confCanc.Value = 1 Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)

                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans

                Dim k As Integer = 0
                Do While k < dtMob.Rows.Count
                    Dim rowScart As Data.DataRow = dtMob.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Patrimonio1.FindControl("idPatrMob"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtMob.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM COMP_PATR_MOB_VSA WHERE ID=" & CType(Dic_Patrimonio1.FindControl("idPatrMob"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataSource = dtMob
                CType(Dic_Patrimonio1.FindControl("DataGridMob"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                funzioneSalva()

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
                ‘‘par.cmd.Transaction = par.myTrans

                Dim k As Integer = 0
                Do While k < dtImmob.Rows.Count
                    Dim rowScart As Data.DataRow = dtImmob.Rows(k)
                    If rowScart.ItemArray(1) = CType(Dic_Patrimonio1.FindControl("idPatrImmob"), HiddenField).Value Then
                        rowScart.Delete()
                    End If
                    k = k + 1
                Loop
                dtImmob.AcceptChanges()

                par.cmd.CommandText = "DELETE FROM COMP_PATR_IMMOB_VSA WHERE ID=" & CType(Dic_Patrimonio1.FindControl("idPatrImmob"), HiddenField).Value
                par.cmd.ExecuteNonQuery()

                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataSource = dtImmob
                CType(Dic_Patrimonio1.FindControl("DataGridImmob"), DataGrid).DataBind()
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('provenienza').value = 0;", True)
                confCanc.Value = 0
                funzioneSalva()

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

    Public Property descrMotivoDom() As String
        Get
            If Not (ViewState("par_descrMotivoDom") Is Nothing) Then
                Return CStr(ViewState("par_descrMotivoDom"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_descrMotivoDom") = value
        End Set

    End Property

    Public Property lIdDichCollegata() As Long
        Get
            If Not (ViewState("par_lIdDichCollegata") Is Nothing) Then
                Return CLng(ViewState("par_lIdDichCollegata"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDichCollegata") = value
        End Set

    End Property

    Public Property PGcollegato() As String
        Get
            If Not (ViewState("PGcollegato") Is Nothing) Then
                Return CStr(ViewState("PGcollegato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("PGcollegato") = value
        End Set

    End Property

    Public Property lIdDomanda() As Long
        Get
            If Not (ViewState("par_lIdDomanda") Is Nothing) Then
                Return CLng(ViewState("par_lIdDomanda"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_lIdDomanda") = value
        End Set

    End Property

    Public Property sopralluogo() As Boolean
        Get
            If Not (ViewState("par_sopralluogo") Is Nothing) Then
                Return CLng(ViewState("par_sopralluogo"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_sopralluogo") = value
        End Set

    End Property

    Public Property docMancante() As Boolean
        Get
            If Not (ViewState("par_docMancante") Is Nothing) Then
                Return CLng(ViewState("par_docMancante"))
            Else
                Return False
            End If
        End Get

        Set(ByVal value As Boolean)
            ViewState("par_docMancante") = value
        End Set

    End Property

    Public Property sStato() As String
        Get
            If Not (ViewState("par_sStato") Is Nothing) Then
                Return CStr(ViewState("par_sStato"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStato") = value
        End Set

    End Property

    Public Property modPresent() As String
        Get
            If Not (ViewState("modPresent") Is Nothing) Then
                Return CStr(ViewState("modPresent"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("modPresent") = value
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

    Public Property newComp() As Integer
        Get
            If Not (ViewState("par_newComp") Is Nothing) Then
                Return CInt(ViewState("par_newComp"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_newComp") = value
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

    Protected Sub opUscita()
        Try
            If txtModificato.Value = "1" Or txtModificato.Value = "111" Then
                ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "ConfermaEsci();", True)
            Else
                If txtModificato.Value <> "111" And txtModificato.Value <> "222" Then
                    If Session.Item("LAVORAZIONE") = "1" Then
                        If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then

                            MessJQuery("La dichiarazione deve essere elaborata!", 0, "Attenzione")
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('caric').style.visibility = 'hidden';", True)
                            'H1.Text = "0"
                            Exit Sub
                        End If
                        'H1.Text = "1"
                        'If par.OracleConn.State = Data.ConnectionState.Open Then
                        '    par.OracleConn.Close()
                        'End If
                        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                        ‘‘par.cmd.Transaction = par.myTrans
                        par.myTrans.Rollback()
                        'par.myTrans.Dispose()
                        par.OracleConn.Close()
                        par.OracleConn.Dispose()
                        HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
                        HttpContext.Current.Session.Remove("CONNESSIONE" & vIdConnessione)
                        Session.Item("LAVORAZIONE") = "0"
                        Session.Remove("STAMPATO")
                        Session.Remove("idBandoAU")
                        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                        If TORNA = "1" Then
                            'Response.Write("<script>location.replace('RisultatoRicercaD.aspx?XX=1&ENTE=ALTRI ENTI&CO=" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "');</script>")
                        End If

                        If CHIUDI <> "1" Then
                            'Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")

                            'Response.Write("<script>window.close();</script>")
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                        End If
                    Else
                        Session.Item("LAVORAZIONE") = "0"

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


                        If TORNA = "1" Then
                            'Response.Write("<script>location.replace('RisultatoRicercaD.aspx?XX=1&ENTE=ALTRI ENTI&CO=" & CType(Dic_Utenza1.FindControl("txtRapporto"), TextBox).Text & "');</script>")
                        End If

                        If CHIUDI <> "1" Then
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                        Else
                            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                        End If
                    End If
                Else
                    '   document.txtModificato.Value = "1"
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('txtModificato').value = 1;", True)
                End If
            End If
        Catch EX As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Item("LAVORAZIONE") = "0"
            Session.Add("ERRORE", "Provenienza: " & Page.Title & " - ImgUscita.click() " & EX.Message)
            Response.Redirect("../../Errore.aspx", False)
        Finally

        End Try
    End Sub

    Private Function VerificaDati(ByRef S As String) As Boolean
        VerificaDati = True

        If txtCognome.Text = "" Then
            MessJQuery("Attenzione...Campo <COGNOME> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If
        If txtNome.Text = "" Then
            MessJQuery("Attenzione...Campo <NOME> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If
        If txtCF.Text = "" Then
            MessJQuery("Attenzione...Campo <CODICE FISCALE> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If

        If txtDataNascita.Text = "" Then
            MessJQuery("Attenzione...Campo <DATA DI NASCITA> deve essere valorizzato!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If

        If par.ControllaValiditaCF(txtCF.Text, txtCognome.Text, txtNome.Text, txtDataNascita.Text) = False Then
            MessJQuery("Attenzione...Il codice fiscale non è corretto!", 0, "Attenzione")
            VerificaDati = False
            Exit Function
        End If

    End Function


    Private Function CalcolaISEEDomanda()

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
        Dim STRINGA_STAMPA_1 As String
        Dim STRINGA_STAMPA_2 As String
        Dim STRINGA_STAMPA_3 As String
        Dim STRINGA_STAMPA_4 As String
        Dim STRINGA_STAMPA_5 As String
        Dim STRINGA_STAMPA_6 As String
        Dim STRINGA_STAMPA_66 As String
        Dim STRINGA_STAMPA_7 As String
        Dim TIPO_ALLOGGIO As Integer

        Dim Testo_Da_Scrivere As String
        Dim glIndice_Bando_Origine As Long
        Dim DescrizioneBandoAggiornamento As String
        Dim CONTRATTO_30_ANNI As Boolean = False

        Try

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
            STRINGA_STAMPA_1 = ""
            STRINGA_STAMPA_2 = ""
            STRINGA_STAMPA_3 = ""
            STRINGA_STAMPA_4 = ""
            STRINGA_STAMPA_5 = ""
            STRINGA_STAMPA_6 = ""
            STRINGA_STAMPA_66 = ""
            STRINGA_STAMPA_7 = ""
            TIPO_ALLOGGIO = -1

            ESCLUSIONE = ""

            Dim lIdDomanda As Long

            DescrizioneBandoAggiornamento = ""

            'If par.OracleConn.State = Data.ConnectionState.Closed Then
            '    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            '    par.SettaCommand(par)
            'End If

            'If IsNothing(par.myTrans) Then
            '    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            '    ‘‘par.cmd.Transaction = par.myTrans
            'End If


            par.cmd.CommandText = "select * from domande_bando_vsa where id_dichiarazione = " & lIdDichiarazione
            Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If lettore.Read Then
                lIdDomanda = par.IfNull(lettore("ID"), 0)


                If par.RicavaEta(par.IfNull(lettore("CONTRATTO_DATA_DEC"), Format(Now, "yyyMMdd"))) > 30 Then
                    CONTRATTO_30_ANNI = True
                Else
                    CONTRATTO_30_ANNI = False
                End If

                If par.IfNull(lettore("fl_rinnovo"), "0") = "1" Then
                    par.cmd.CommandText = "select * from bandi_VSA where stato=1 order by id desc"
                    Dim myReader12 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader12.Read Then
                        'lIndice_Bando = par.IfNull(myReader12("id"), "-1")
                        DescrizioneBandoAggiornamento = par.IfNull(myReader12("descrizione"), "")
                    End If
                    myReader12.Close()
                End If
            End If
            lettore.Close()



            glIndice_Bando_Origine = lIndice_Bando
            'par.cmd.CommandText = "select fl_rinnovo from domande_bando_vsa where id=" & lIdDomanda
            'Dim myReader11 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            'If myReader11.Read Then

            'End If
            'myReader11.Close()




            If Fl_Integrazione = "1" Then
                par.cmd.CommandText = "select * from bandi_vsa where stato=1 order by id desc"
                Dim myReader14 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader14.Read Then
                    'lIndice_Bando = par.IfNull(myReader12("id"), "-1")
                    DescrizioneBandoAggiornamento = par.IfNull(myReader14("descrizione"), "")
                End If
                myReader14.Close()

            End If


            par.cmd.CommandText = "SELECT TASSO_RENDIMENTO FROM BANDI_vsa WHERE ID=" & lIndice_Bando
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            If myReader.Read() Then
                TASSO_RENDIMENTO = par.IfNull(myReader("TASSO_RENDIMENTO"), 0)
            End If
            myReader.Close()


            Dim CODICEANAGRAFICO As String = ""

            par.cmd.CommandText = "SELECT operatori.*,caf_web.descrizione as ENTE from operatori,caf_web where operatori.id_caf=caf_web.id and operatori.ID=" & Session.Item("ID_OPERATORE")
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                CODICEANAGRAFICO = par.IfNull(myReader("ente"), "") & " - " & par.IfNull(myReader("COD_ANA"), "")
            End If
            myReader.Close()



            par.cmd.CommandText = "SELECT DICHIARAZIONI_vsa.ANNO_SIT_ECONOMICA FROM DICHIARAZIONI_vsa,DOMANDE_BANDO_vsa WHERE DOMANDE_BANDO_vsa.ID=" & lIdDomanda & " AND DOMANDE_BANDO_vsa.ID_DICHIARAZIONE=DICHIARAZIONI_vsa.ID"
            Dim myReader13 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader13.Read Then
                TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(myReader13("ANNO_SIT_ECONOMICA"), 0))
            End If
            myReader13.Close()

            par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_vsa WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()

            'If myReader.Read() Then

            TOT_COMPONENTI = 0
            Dim VECCHI As Integer = 0
            Dim ETA_COMPONENTE As Integer = 0

            Do While myReader.Read()
                ETA_COMPONENTE = par.RicavaEta(par.IfNull(myReader("DATA_NASCITA"), ""))
                If ETA_COMPONENTE >= 15 Then
                    If ETA_COMPONENTE >= 18 Then
                        adulti = adulti + 1
                        If ETA_COMPONENTE > 65 Then
                            VECCHI = VECCHI + 1
                        End If
                    End If
                Else
                    MINORI = MINORI + 1
                End If

                par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_vsa WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader1.Read
                    DETRAZIONI = DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
                End While
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
                End While
                myReader1.Close()


                DETRAZIONI_FRAGILE = 0
                par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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



                par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
                myReader1 = par.cmd.ExecuteReader()
                While myReader1.Read
                    MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
                End While
                myReader1.Close()



                par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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



            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_vsa WHERE ID=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
                INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
                INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
            End If
            myReader.Close()

            DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)



            FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165


            ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
            If ISEE_ERP < 0 Then
                ISEE_ERP = 0
            End If

            ISR_ERP = ISEE_ERP

            ISEE_ERP = 0

            TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)

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
            PARAMETRO = Math.Round(PARAMETRO, 2)
            VSE = PARAMETRO
            If adulti >= 2 Then
                VSE = VSE - (MINORI * (1 / 10))
                PARAMETRO_MINORI = (MINORI * (1 / 10))
            End If

            LIMITE_PATRIMONIO = 16000 + (6000 * VSE)

            ISE_ERP = ISR_ERP + ISP_ERP

            ISEE_ERP = ISE_ERP / VSE

            'If ISEE_ERP <= 35000 Then
            '    TIPO_ALLOGGIO = 0
            'Else
            '    If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
            '        TIPO_ALLOGGIO = 0
            '    Else
            '        ESCLUSIONE = "<li>ISEE superiore al limite ERP</li>"
            '        par.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET REQUISITO6='0' WHERE ID=" & lIdDomanda
            '        par.cmd.ExecuteNonQuery()

            '    End If
            'End If

            'If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO * 3 Then
            '    If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then

            '    Else
            '        ESCLUSIONE = ESCLUSIONE & "<li>Limite Patrimoniale superato</li>"
            '        par.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET REQUISITO6='0' WHERE ID=" & lIdDomanda
            '        par.cmd.ExecuteNonQuery()
            '    End If
            '    '***CasReq6.Text = "0"
            'End If


            Dim Totale_Risultati As Long
            Dim Valore
            Dim Valore1 As String = ""

            Totale_Risultati = 0

            Dim F1 As Double
            Dim F2 As Double
            Dim F3 As Double

            Dim F10 As Double
            Dim F20 As Double
            Dim F30 As Double

            F1 = 0
            F2 = 0
            F3 = 0

            F10 = 0
            F20 = 0
            F30 = 0


            If ESCLUSIONE <> "" Then
                ISEE_ERP = 0
            End If

            If ESCLUSIONE = "" Then
                If tipoRichiesta.Value <> 5 Then
                    par.cmd.CommandText = "update domande_bando_vsa set FL_CONTROLLA_REQUISITI='2',FL_ASS_ESTERNA='1',FL_COMPLETA='1',FL_RINNOVO='0'," _
                        & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                        & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='8'," _
                        & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                        & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                        & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                        & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                        & TIPO_ALLOGGIO & ",ISBARC=" & "0" _
                         & ",ISBAR=" & "0" & ",ISBARC_R=" _
                        & "0" & ",DISAGIO_A=" _
                        & "0" & ",DISAGIO_F=" _
                        & "0" & ",DISAGIO_E=" _
                        & "0" & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                Else
                    par.cmd.CommandText = "update domande_bando_vsa set FL_CONTROLLA_REQUISITI='2',FL_ASS_ESTERNA='1',FL_COMPLETA='1',FL_RINNOVO='0'," _
                        & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                        & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='4'," _
                        & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                        & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) & ",ISP_ERP=" _
                        & par.VirgoleInPunti(ISP_ERP) & ",REDDITO_ISEE=" _
                        & par.VirgoleInPunti(ISEE_ERP) & ", TIPO_ALLOGGIO=" _
                        & TIPO_ALLOGGIO & ",ISBARC=" & "0" _
                         & ",ISBAR=" & "0" & ",ISBARC_R=" _
                        & "0" & ",DISAGIO_A=" _
                        & "0" & ",DISAGIO_F=" _
                        & "0" & ",DISAGIO_E=" _
                        & "0" & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                End If

                par.cmd.ExecuteNonQuery()
            Else
                par.cmd.CommandText = "update domande_bando_vsa set FL_CONTROLLA_REQUISITI='2',FL_ASS_ESTERNA='1',FL_COMPLETA='1',FL_RINNOVO='0'," _
                                           & "ID_VECCHIO_STATO='',ID_TIPO_CONTENZIOSO=NULL," _
                                           & "FL_ISTRUTTORIA_COMPLETA='1',ID_STATO='4'," _
                                           & "FL_ESAMINATA='1',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                                           & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
                                           & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
                                           & ",REDDITO_ISEE=" & par.VirgoleInPunti(ISEE_ERP) _
                                           & ",TIPO_ALLOGGIO=" & TIPO_ALLOGGIO _
                                           & ",ISBARC=" & "0" _
                                           & ",ISBAR=" & "0" _
                                           & ",ISBARC_R=" & "0" _
                                           & ",DISAGIO_A=" & "0" _
                                           & ",DISAGIO_F=" & "0" _
                                           & ",DISAGIO_E=" & "0" _
                                           & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDomanda
                par.cmd.ExecuteNonQuery()
            End If



            'EVENTO ELABORAZIONE DICHIARAZIONE
            par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                & "','F207','','')"
            par.cmd.ExecuteNonQuery()


            'par.myTrans.Commit()
            'par.myTrans = par.OracleConn.BeginTransaction()
            'HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)




        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CalcolaIseeDomanda() - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        Finally

        End Try

    End Function

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

    Private Function Valore01(ByVal valore As Boolean) As String
        If valore = True Then
            Valore01 = "1"
        Else
            Valore01 = "0"
        End If
    End Function


    'Protected Sub imgStampa_Click(sender As Object, e As System.EventArgs) Handles imgStampa.Click
    '    Try
    '        CalcolaStampa()
    '        Session.Item("STAMPATO") = "1"
    '        lblISEE.Text = ISEE_DICHIARAZIONE

    '    Catch ex As Exception
    '        par.myTrans.Rollback()
    '        par.myTrans = par.OracleConn.BeginTransaction()
    '        HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
    '        Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CalcolaStampa() - " & ex.Message)
    '        Response.Redirect("../Errore.aspx", False)
    '    End Try

    'End Sub

    Protected Sub txtCF_TextChanged(sender As Object, e As System.EventArgs) Handles txtCF.TextChanged
        If par.ControllaCF(UCase(txtCF.Text)) = False Then
            CFLABEL.Visible = True
            CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
            txtCF.Text = ""
        Else
            'CFLABEL.Visible = False
            CFLABEL.Text = ""
            If txtCF.Text <> "" Then
                If par.ControllaCFNomeCognome(UCase(txtCF.Text), txtCognome.Text, txtNome.Text) = True Then
                    If Correlazioni(UCase(txtCF.Text)) = True Then
                        CFLABEL.Visible = True
                        CFLABEL.Text = UCase(txtCF.Text) & ": TROVATO IN ALTRE DICHIARAZIONI!"
                        CompletaDati(UCase(txtCF.Text))
                        CFLABEL.Attributes.Add("OnClick", "javascript:window.open('../../CorrelazioniUtenza.aspx?" & "CF=" & par.VaroleDaPassare(txtCF.Text) & "&ID=" & lIdDichiarazione & "','Correlazioni','top=0,left=0,width=600,height=400');")
                        If txtbinserito.Value = "0" Or txtbinserito.Value = "" Then
                            'CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)
                            'CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Value = 0
                            CType(Tab_Nucleo1.FindControl("txtProgr"), HiddenField).Value = "0"
                            txtbinserito.Value = "1"
                        End If
                    Else
                        'CFLABEL.Visible = False
                        CFLABEL.Text = ""
                        CompletaDati(UCase(txtCF.Text))
                        If txtbinserito.Value = "0" Or txtbinserito.Value = "" Then
                            'CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Text = par.MiaFormat(txtCognome.Text, 25) & " " & par.MiaFormat(txtNome.Text, 25) & " " & par.MiaFormat(txtDataNascita.Text, 10) & " " & par.MiaFormat(txtCF.Text, 16) & " " & par.MiaFormat("CAPOFAMIGLIA", 25) & " " & par.MiaFormat("0", 6) & " " & par.MiaFormat("-----", 5) & " " & par.MiaFormat("NO", 2)
                            'CType(Page.Form.FindControl("Dic_Nucleo1").FindControl("ListBox1"), ListBox).Items(0).Value = 0
                            CType(Tab_Nucleo1.FindControl("txtProgr"), HiddenField).Value = "0"
                            txtbinserito.Value = "1"
                           
                        End If
                        par.SetFocusControl(Page, "cmbNazioneNas")
                    End If
                Else
                    CFLABEL.Visible = True
                    CFLABEL.Text = UCase(txtCF.Text) & ": CODICE FISCALE ERRATO!"
                End If
            Else
                'CFLABEL.Visible = False
                CFLABEL.Text = ""
            End If
        End If

    End Sub

    Private Function CompletaDati(ByVal CF As String)
        par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
        par.SettaCommand(par)
        par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
        ‘‘par.cmd.Transaction = par.myTrans


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
        ‘‘par.cmd.Transaction = par.myTrans

        Correlazioni = False

        par.cmd.CommandText = "SELECT DICHIARAZIONI_VSA.ID FROM DICHIARAZIONI_VSA,COMP_NUCLEO_VSA WHERE DICHIARAZIONI_VSA.ID=COMP_NUCLEO_VSA.ID_DICHIARAZIONE (+) AND (COMP_NUCLEO_VSA.COD_FISCALE='" & CF & "' OR COMP_NUCLEO_VSA.COD_FISCALE='" & CF & "') AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE<>" & lIdDichiarazione

        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            'txtCAPRes.Text = myReader(0)
            Correlazioni = True
        End If
        myReader.Close()

    End Function

    Protected Sub btnSalva_Click(sender As Object, e As System.EventArgs) Handles btnSalva.Click
        funzioneSalva()
    End Sub

    Private Sub funzioneSalva()
        Dim idComponenti(15) As Long
        Dim S As String
        Dim i As Integer
        Dim j As Integer
        'Dim progr As Integer
        Dim NUM_PARENTI As Integer
        Dim idDomanda As Long
        Dim patrImmob As Boolean = False
        Dim sStringaSql As String

        Try
            bMemorizzato = False

            NUM_PARENTI = 0

            N_INV_100_ACC = 0
            N_INV_100_NO_ACC = 0
            N_INV_100_66 = 0

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                par.SettaCommand(par)
            End If

            If IsNothing(par.myTrans) Then
                par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                ‘‘par.cmd.Transaction = par.myTrans
            End If

            If VerificaDati(S) = False Then
                btnApplica.Visible = False
                imgStampa.Enabled = False
                'imgStampa.ImageUrl = "../NuoveImm/Img_No_Stampa.png"
                Exit Try
            End If

            If cmbStato.SelectedValue = "1" Then
                If DateDiff("m", DateSerial(Mid(txtDataNascita.Text, 7, 4), Mid(txtDataNascita.Text, 4, 2), Mid(txtDataNascita.Text, 1, 2)), Now) / 12 < 18 Then
                    MessJQuery("Attenzione...Il richiedente deve essere maggiorenne! Memorizzazione non effettuata.", 0, "Attenzione")

                    imgStampa.Enabled = False
                    imgStampa.Text = ""
                    Exit Try
                End If

                If Len(txtCAPRes.Text) < 5 Then
                    MessJQuery("Attenzione...Il CAP di Residenza deve essere 5 caratteri! Memorizzazione non effettuata.", 0, "Attenzione")
                    imgStampa.Enabled = False
                    imgStampa.Text = ""
                    Exit Try
                End If

                '17/02/2012 CONTROLLO CHE L'ANNO REDDITO SIA SUPERIORE DIVERSO DAL 2006
                If tipoRichiesta.Value = "3" And cmbAnnoReddituale.SelectedValue <= "2006" Then
                    MessJQuery("Attenzione...si prega di inserire il corretto anno di riferimento reddituale!", 0, "Attenzione")
                    Exit Try
                End If

                S = ""

            End If

            If Request.QueryString("CH") = 1 Then
                If tipoRichiesta.Value <> "3" Then

                    If cmbStato.SelectedValue <> "2" Then
                        If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                            MessJQuery("Attenzione...Lo stato della dichiarazione deve essere COMPLETA! Memorizzazione non effettuata.", 0, "Attenzione")
                            Exit Sub
                        End If
                        'Session.Item("STAMPATO") = "1"
                        'If Request.QueryString("GLocat") = "" Then
                        '    CalcolaISEEDomanda()
                        'End If
                        'MessJQuery("Elaborazione effettuata!", 1, "Avviso")
                    End If
                End If
            End If

            '****** 09/03/2012 INSERIMENTO DOC ALLEGATI *******
            Dim docAllegati As Boolean = False
            For i = 0 To CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items.Count - 1
                If CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Selected = True Then
                    CType(dic_Documenti1.FindControl("documAlleg"), HiddenField).Value = 1
                    par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ""
                    Dim lett As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett.Read = False Then
                        par.cmd.CommandText = "INSERT INTO VSA_DOC_ALLEGATI (ID_DICHIARAZIONE,ID_DOC) VALUES (" & lIdDichiarazione & "," & CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ")"
                        par.cmd.ExecuteNonQuery()
                        docAllegati = True
                    End If
                    lett.Close()
                Else
                    par.cmd.CommandText = "SELECT * FROM VSA_DOC_ALLEGATI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ""
                    Dim lett2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                    If lett2.Read Then
                        par.cmd.CommandText = "DELETE FROM VSA_DOC_ALLEGATI WHERE ID_DICHIARAZIONE=" & lIdDichiarazione & " AND ID_DOC=" & CType(dic_Documenti1.FindControl("chkListDocumenti"), CheckBoxList).Items(i).Value & ""
                        par.cmd.ExecuteNonQuery()
                        CType(dic_Documenti1.FindControl("documAlleg"), HiddenField).Value = 0

                        'EVENTO CANCELLAZIONE DOC.ALLEGATI
                        par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                            & "','F206','','')"
                        par.cmd.ExecuteNonQuery()
                    End If
                    lett2.Close()
                End If
            Next
            If docAllegati = True Then
                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                            & "','F194','','')"
                par.cmd.ExecuteNonQuery()
            End If
            '****** 09/03/2012 FINE INSERIMENTO DOC ALLEGATI *******

            If numComp.Value = "0" Then
                If txtCF.Text <> "" Then
                    par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_VSA (ID,ID_DICHIARAZIONE,PROGR,COGNOME,NOME,GRADO_PARENTELA,COD_FISCALE,PERC_INVAL,DATA_NASCITA,USL,INDENNITA_ACC,SESSO, TIPO_INVAL, NATURA_INVAL" _
                                & ") VALUES (SEQ_COMP_NUCLEO_VSA.NEXTVAL," & lIdDichiarazione & ",0,'" & par.PulisciStrSql(par.IfEmpty(txtCognome.Text, "")) & "','" & par.PulisciStrSql(par.IfEmpty(txtNome.Text, "")) _
                                & "',NULL,'" & par.IfEmpty(txtCF.Text, "") & "',NULL,'" & par.AggiustaData(par.IfEmpty(txtDataNascita.Text, "")) & "',''," _
                                & "'','" & par.RicavaSesso(par.IfEmpty(txtCF.Text, "")) & "','','')"
                    par.cmd.ExecuteNonQuery()
                End If
            End If

            CaricaRedditi()
            CaricaNucleo()
            CaricaNucleoSpese()
            CaricaPatrimMob()
            patrImmob = CaricaPatrimImmob()
            CaricaDocumenti()

            '***** 23/11/11 CONTROLLO SE MORE UXORIO OPPURE PER ASSISTENZA SOLO 1 NUOVO COMPONENTE *****
            Dim tipoAmpl As Integer
            If cmbStato.SelectedValue = "1" Then
                If tipoRichiesta.Value = "2" Then
                    par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                    Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If lettore.Read Then
                        tipoAmpl = par.IfNull(lettore("ID_CAUSALE_DOMANDA"), "-1")
                    End If
                    lettore.Close()

                    If newComp = 0 Then
                        MessJQuery("ATTENZIONE...Aggiungere il nuovo componente nell\'apposita sezione. Memorizzazione non effettuata!", 0, "Attenzione")

                        Exit Try
                    End If

                End If
            End If
            '***** FINE CONTROLLO SE MORE UXORIO OPPURE PER ASSISTENZA SOLO 1 NUOVO COMPONENTE *****

            'CONTROLLO: se compilato il patrimonio immobiliare -> Alert bloccante nel caso non ci siano valori diversi da zero nei 
            'redditi da fabbricato e/o terreni nella sezione dei redditi di tipo autonomo.
            If cmbStato.SelectedValue = "1" Then
                If salvaEsterno.Value = "0" Then
                    
                    Dim NOpensCivile As Integer = 0
                    Dim NOindACC As Integer = 0

                    If N_INV_100_ACC >= 1 Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI,DOMANDE_REDDITI_VSA where ID_REDD_PENSIONE=3 AND VSA_REDD_PENS_IMPORTI.ID_REDD_TOT=DOMANDE_REDDITI_VSA.ID AND ID_DOMANDA=" & lIdDichiarazione
                        Dim myReaderRedd0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            NOpensCivile = 1
                        End If
                        myReaderRedd0.Close()

                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_ES_IMPORTI,DOMANDE_REDDITI_VSA where ID_REDD_PENS_ESENTI=4 AND VSA_REDD_PENS_ES_IMPORTI.ID_REDD_TOT=DOMANDE_REDDITI_VSA.ID AND ID_DOMANDA=" & lIdDichiarazione
                        myReaderRedd0 = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            NOindACC = 1
                        End If
                        myReaderRedd0.Close()
                    End If

                    If NOpensCivile = 1 And NOindACC = 1 Then
                        salvaEsterno.Value = "0"
                        MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100% con accompagnamento. Inserire pensione d\'invalidità civile e l\'indennità di accompagnamento!", 0, "Attenzione")
                        Exit Try
                    ElseIf NOpensCivile = 1 Then
                        salvaEsterno.Value = "0"
                        MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100% con accompagnamento. Inserire pensione d\'invalidità civile!", 0, "Attenzione")
                        Exit Try
                    ElseIf NOindACC = 1 Then
                        salvaEsterno.Value = "0"
                        MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100% con accompagnamento. Inserire l\'indennità di accompagnamento!", 0, "Attenzione")
                        Exit Try
                    End If

                    par.cmd.CommandText = "SELECT * FROM vsa_redd_pens_importi, vsa_redditi_pensione,DOMANDE_REDDITI_VSA,comp_nucleo_vsa WHERE vsa_redd_pens_importi.id_redd_tot = DOMANDE_REDDITI_VSA.ID AND vsa_redd_pens_importi.ID_REDD_PENSIONE=vsa_redditi_pensione.id AND comp_nucleo_vsa.ID = DOMANDE_REDDITI_VSA.id_componente AND ID_DOMANDA=" & lIdDichiarazione
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim dtPens As New Data.DataTable
                    da.Fill(dtPens)
                    da.Dispose()
                    If dtPens.Rows.Count > 0 Then
                        For Each row As Data.DataRow In dtPens.Rows
                            par.cmd.CommandText = "SELECT * FROM vsa_redd_pens_importi,DOMANDE_REDDITI_VSA,comp_nucleo_vsa WHERE vsa_redd_pens_importi.id_redd_tot(+) = DOMANDE_REDDITI_VSA.ID AND comp_nucleo_vsa.ID = DOMANDE_REDDITI_VSA.id_componente AND DOMANDE_REDDITI_VSA.id_DOMANDA<" & lIdDichiarazione & " AND COD_FISCALE='" & par.IfNull(row.Item("COD_FISCALE"), "") & "' order by id_DOMANDA desc"
                            Dim myReaderReddP As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReaderReddP.Read Then
                                If CDec(par.IfNull(myReaderReddP("PENSIONE"), 0)) > CDec(par.IfNull(row.Item("PENSIONE"), 0)) Then
                                    salvaEsterno.Value = "0"
                                    MessJQuery("ATTENZIONE...Il valore della pensione non può essere inferiore a quello inserito nella dichiarazione precedente!", 0, "Attenzione")
                                End If
                            End If
                            myReaderReddP.Close()
                        Next
                    End If

                    If patrImmob = True Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_AUTONOMO_IMPORTI,DOMANDE_REDDITI_VSA where (ID_REDD_AUTONOMO=6 or ID_REDD_AUTONOMO=7) AND VSA_REDD_AUTONOMO_IMPORTI.ID_REDD_TOT=DOMANDE_REDDITI_VSA.ID AND ID_DOMANDA=" & lIdDichiarazione
                        Dim myReaderRedd0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            salvaEsterno.Value = "0"
                            MessJQuery("ATTENZIONE...Per coerenza di dati inserire i redditi da fabbricato e/o da terreni!", 0, "Attenzione")
                        End If
                        myReaderRedd0.Close()
                    End If
                    If N_INV_100_NO_ACC >= 1 Then
                        par.cmd.CommandText = "SELECT * FROM VSA_REDD_PENS_IMPORTI,DOMANDE_REDDITI_VSA where ID_REDD_PENSIONE=3 AND VSA_REDD_PENS_IMPORTI.ID_REDD_TOT=DOMANDE_REDDITI_VSA.ID AND ID_DOMANDA=" & lIdDichiarazione
                        Dim myReaderRedd0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReaderRedd0.Read = False Then
                            salvaEsterno.Value = "0"
                            MessJQuery("ATTENZIONE...Presenza di un componente invalido al 100%. Inserire pensione d\'invalidità civile!", 0, "Attenzione")
                            Exit Try
                        End If
                        myReaderRedd0.Close()
                    End If
                End If
            End If


            Dim COD_PARENTE As Long
            Dim INDENNITA As String

            COD_PARENTE = 1
            INDENNITA = ""


            par.cmd.CommandText = "SELECT COD FROM T_TIPO_PARENTELA WHERE DESCRIZIONE=''"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                COD_PARENTE = myReader("COD")
            End If
            myReader.Close()

            Dim codContratto As String = ""
            par.cmd.CommandText = "SELECT * FROM DOMANDE_BANDO_VSA WHERE ID=" & lIdDomanda
            Dim myReaderContr As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderContr.Read Then
                codContratto = par.IfNull(myReaderContr("CONTRATTO_NUM"), "")
            End If
            myReader.Close()


            ' ''par.cmd.CommandText = "INSERT INTO COMP_NUCLEO_CANCELL (ID_DICHIARAZIONE,NOME,COGNOME,DATA_NASCITA,COD_FISCALE,ID_MOTIVO,DATA_USCITA) VALUES " _
            ' ''& "(" & lIdDichiarazione & ",'" & par.PulisciStrSql(par.IfNull(myReaderCanc("NOME"), "")) & "','" & par.PulisciStrSql(par.IfNull(myReaderCanc("COGNOME"), "")) & "'," _
            ' ''& "'" & par.IfNull(myReaderCanc("DATA_NASCITA"), "") & "','" & par.IfNull(myReaderCanc("COD_FISCALE"), "") & "','" _
            ' ''& CType(Dic_Nucleo1.FindControl("MotivoUscita"), HiddenField).Value & "','" & par.AggiustaData(CType(Dic_Nucleo1.FindControl("DataUscita"), HiddenField).Value) & "')"
            ' ''par.cmd.ExecuteNonQuery()

            '****** 14/03/2012 FINE INSERIMENTO COMPONENTE DA CANCELLARE *******

            par.cmd.CommandText = "SELECT COUNT(ID_COMPONENTE) AS NUM_REFE FROM NUOVI_COMP_NUCLEO_VSA,COMP_NUCLEO_VSA WHERE NUOVI_COMP_NUCLEO_VSA.ID_COMPONENTE = COMP_NUCLEO_VSA.ID AND " _
                & "COMP_NUCLEO_VSA.ID_DICHIARAZIONE =" & lIdDichiarazione & " AND FL_REFERENTE = 1 GROUP BY (ID_COMPONENTE)"
            Dim myReaderR As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderR.Read Then
                If myReaderR("NUM_REFE") > 1 Then
                    MessJQuery("Attenzione...il nuovo nucleo non può avere più di un referente!", 0, "Attenzione")
                    Exit Try
                End If
            End If
            myReaderR.Close()

            

            sStringaSql = ""
            sStringaSql = "UPDATE DICHIARAZIONI_VSA SET TIPO=" & Valore01(ChFO.Checked) & "," _
                      & "PG='" & lblPG.Text & "'" _
                      & ",DATA_PG='" _
                      & par.AggiustaData(txtDataPG.Text) & "',LUOGO='Milano',DATA='" _
                      & par.AggiustaData(txtDataPG.Text) _
                      & "',NOTE='" & par.PulisciStrSql(CType(dic_Documenti1.FindControl("txtNote"), TextBox).Text) _
                      & "',ID_STATO=" & cmbStato.SelectedItem.Value _
                      & ",N_COMP_NUCLEO=" & NUM_PARENTI & ",N_INV_100_CON=" & N_INV_100_ACC _
                      & ",N_INV_100_SENZA=" & N_INV_100_NO_ACC _
                      & ",N_INV_100_66=" & N_INV_100_66 _
                      & ",ANNO_SIT_ECONOMICA=" & cmbAnnoReddituale.SelectedValue _
                      & ",LUOGO_S='Milano',DATA_S=' " _
                      & "',LUOGO_INT_ERP='Milano',DATA_INT_ERP=' " _
                      & "',FL_GIA_TITOLARE='' " _
                      & ",MINORI_CARICO='" & Val(CType(Dic_Reddito1.FindControl("txtMinori"), TextBox).Text) & "' "

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
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(txtCAPRes.Text) & "',ID_LUOGO_NAS_DNTE=" & cmbComuneNas.SelectedItem.Value
            Else
                sStringaSql = sStringaSql & ",CAP_RES_DNTE='" & par.PulisciStrSql(txtCAPRes.Text) & "',ID_LUOGO_NAS_DNTE=" & cmbNazioneNas.SelectedItem.Value
            End If


            par.cmd.CommandText = sStringaSql & " WHERE ID=" & lIdDichiarazione
            par.cmd.ExecuteNonQuery()


            ' ''    FL_70KM = "0"
            ' ''    par.cmd.CommandText = "SELECT * FROM COMUNI_NAZIONI WHERE NOME='" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 102, 20)) & "'"
            ' ''    Dim myReaderCC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            ' ''    If myReaderCC.Read() Then
            ' ''        If myReaderCC("ENTRO_70KM") = "1" Then
            ' ''            FL_70KM = "1"
            ' ''        End If
            ' ''    End If
            ' ''    myReaderCC.Close()

            ' ''    'par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 86, 30))
            ' ''    sStringaSql = "INSERT INTO COMP_PATR_IMMOB_VSA (ID,ID_COMPONENTE,ID_TIPO,PERC_PATR_IMMOBILIARE,VALORE,MUTUO,F_RESIDENZA,CAT_CATASTALE,COMUNE,N_VANI,SUP_UTILE,FL_70KM,PIENA_PROPRIETA,REND_CATAST_DOMINICALE) VALUES " _
            ' ''                    & " (SEQ_UTENZA_COMP_PATR_IMMOB.NEXTVAL," & idComponenti(INDICE) & "," & ID_TIPO _
            ' ''                    & "," & par.VirgoleInPunti(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 48, 7)) _
            ' ''                    & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 73, 15)) _
            ' ''                    & "," & Val(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 84, 19)) _
            ' ''                    & ",'" & RESIDENZA _
            ' ''                    & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 56, 3) _
            ' ''                    & "','" & par.PulisciStrSql(par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 102, 20)) _
            ' ''                    & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 123, 2) _
            ' ''                    & "','" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 126, 7) _
            ' ''                    & "','" & FL_70KM & "'," & PienaP & "," & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 60, 8) & ")"
            ' ''    par.cmd.CommandText = sStringaSql
            ' ''    par.cmd.ExecuteNonQuery()
            ' ''Next i
            'FINE MODIFICA 27/01/2012 DA MASSIMILIANO


            ' ''For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
            ' ''    For j = 0 To cmbComp.Items.Count - 1
            ' ''        If cmbComp.Items(j).Value = CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Value Then
            ' ''            INDICE = j
            ' ''            Exit For
            ' ''        End If
            ' ''    Next
            ' ''    sStringaSql = "INSERT INTO COMP_REDDITO_VSA (ID,ID_COMPONENTE,REDDITO_IRPEF,PROV_AGRARI) VALUES (SEQ_COMP_REDDITO_VSA.NEXTVAL," _
            ' ''               & idComponenti(INDICE) & "," _
            ' ''               & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) _
            ' ''               & "," & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ")"
            ' ''    par.cmd.CommandText = sStringaSql
            ' ''    par.cmd.ExecuteNonQuery()

            ' ''Next i

            sStringaSql = "DELETE FROM SOTTOSCRITTORI_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            par.cmd.CommandText = sStringaSql
            par.cmd.ExecuteNonQuery()



            If rdbListSott.SelectedValue = "1" Then
                sStringaSql = "INSERT INTO SOTTOSCRITTORI_VSA (ID_DICHIARAZIONE) VALUES (" & lIdDichiarazione & ")"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()

                If txtCognomeS.Text = "" Or txtNomeS.Text = "" Then
                    MessJQuery("Attenzione...Mancanza di dati nel Tab Sottoscrittore!", 0, "Attenzione")
                    Exit Try
                End If

                If cmbNazioneResSott.SelectedItem.Text = "ITALIA" Then
                    sStringaSql = "UPDATE SOTTOSCRITTORI_VSA SET " _
                    & "ID_LUOGO_RES=" & cmbComuneResSott.SelectedItem.Value _
                    & ",ID_TIPO_IND=" & cmbTipoIResSott.SelectedItem.Value _
                    & ",IND='" & par.PulisciStrSql(txtIndResSott.Text) _
                    & "',CIVICO='" & par.PulisciStrSql(txtCivicoResSott.Text) _
                    & "',TELEFONO='" & par.PulisciStrSql(txtTelResSott.Text) _
                    & "',COGNOME='" & par.PulisciStrSql(txtCognomeS.Text) _
                    & "',NOME='" & par.PulisciStrSql(txtNomeS.Text) _
                    & "',DATA_NAS='" & par.AggiustaData(txtDataNascitaSott.Text) & "' " _
                    & ",CAP_RES='" & par.PulisciStrSql(txtCAPResSott.Text) & "' "
                Else
                    sStringaSql = "UPDATE SOTTOSCRITTORI_VSA SET " _
                    & "ID_LUOGO_RES=" & cmbNazioneResSott.SelectedItem.Value _
                    & ",ID_TIPO_IND=" & cmbTipoIResSott.SelectedItem.Value _
                    & ",IND='" & par.PulisciStrSql(txtIndResSott.Text) _
                    & "',CIVICO='" & par.PulisciStrSql(txtCivicoResSott.Text) _
                    & "',TELEFONO='" & par.PulisciStrSql(txtTelResSott.Text) _
                    & "',COGNOME='" & par.PulisciStrSql(txtCognomeS.Text) _
                    & "',NOME='" & par.PulisciStrSql(txtNomeS.Text) _
                    & "',DATA_NAS='" & par.AggiustaData(txtDataNascitaSott.Text) & "' " _
                    & ",CAP_RES='" & par.PulisciStrSql(txtCAPResSott.Text) & "' "
                End If

                If cmbNazioneNasSott.SelectedItem.Text = "ITALIA" Then
                    sStringaSql = sStringaSql & ",ID_LUOGO_NAS=" & cmbComuneNas2.SelectedItem.Value
                Else
                    sStringaSql = sStringaSql & ",ID_LUOGO_NAS=" & cmbNazioneNasSott.SelectedItem.Value
                End If

                par.cmd.CommandText = sStringaSql & " WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
                par.cmd.ExecuteNonQuery()

            End If



            If lNuovaDichiarazione = 1 Then
                sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI_VSA (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F130','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
                Session.Add("ID_NUOVA_DIC", lblPG.Text)
                lNuovaDichiarazione = 0
            Else
                sStringaSql = "INSERT INTO EVENTI_DICHIARAZIONI_VSA (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                            & "'F131','','I')"
                par.cmd.CommandText = sStringaSql
                par.cmd.ExecuteNonQuery()
            End If

            '-------------- 23/03/2012 BLOCCO SPOSTATO SOTTO


            Dim dt As New Data.DataTable


            If docMancante = True Then
                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI_vsa (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                            & "VALUES (" & lIdDomanda & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','" & sStato _
                            & "','F193','','')"
                par.cmd.ExecuteNonQuery()
            End If

            '-------- Abilito Chkbox per Documentazione Mancante Presentata ------------
            Dim dataDocManc As String = ""
            par.cmd.CommandText = "SELECT * FROM EVENTI_BANDI_VSA WHERE ID_DOMANDA = " & lIdDomanda & " AND COD_EVENTO = 'F193' ORDER BY DATA_ORA DESC"
            Dim lettore00 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            If lettore00.Read Then
                dataDocManc = par.IfNull(lettore00("DATA_ORA"), "").ToString.Substring(0, 8)
            End If
            lettore00.Close()

            If docMancante = True Then
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Enabled = True
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Enabled = True

            Else
                CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Enabled = False
                CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Enabled = False
            End If

            If CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Text <> "" And CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Checked = False Then
                MessJQuery("ATTENZIONE, impossibile procedere! Valorizzare il flag relativo alla presentazione della documentaz. mancante, altrimenti cancellare la data!", 0, "Attenzione")
                Exit Try
            End If

            If CType(dic_Documenti1.FindControl("chkDocManc"), CheckBox).Checked = True Then
                If CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Text <> "" Then
                    If Not par.ControllaData(CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox)) Then
                        MessJQuery("ATTENZIONE, Inserire una data valida!", 0, "Attenzione")
                        Exit Try
                    Else
                        If DateDiff(DateInterval.Day, CDate(par.FormattaData(dataDocManc)), CDate(par.FormattaData(Format(Now, "yyyyMMdd")))) <= 30 Then
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PRESENT_DOC_MANC = 1,DATA_PRESENT_DOC_MANC='" & par.AggiustaData(CType(dic_Documenti1.FindControl("txtDataDocManc"), TextBox).Text) & "'" _
                                & " WHERE ID=" & lIdDomanda
                            par.cmd.ExecuteNonQuery()
                            docMancante = False

                            '''CType(dic_Documenti1.FindControl("Button1"), Button).Enabled = False
                            '''CType(dic_Documenti1.FindControl("Button2"), Button).Enabled = False

                        Else
                            MessJQuery("ATTENZIONE, impossibile procedere! Documentazione mancante non integrata entro il termine di 30 gg!", 0, "Attenzione")
                            Exit Try
                        End If
                    End If
                Else
                    MessJQuery("ATTENZIONE, indicare la data in cui è stata presentata la documentazione mancante, altrimenti togliere il flag relativo!", 0, "Attenzione")
                    Exit Try
                End If
            Else
                par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PRESENT_DOC_MANC = 0,DATA_PRESENT_DOC_MANC=''" _
                & " WHERE ID=" & lIdDomanda
                par.cmd.ExecuteNonQuery()
                '''CType(dic_Documenti1.FindControl("Button1"), Button).Enabled = True
                '''CType(dic_Documenti1.FindControl("Button2"), Button).Enabled = True
            End If

            'controllo che non siano trascorsi più di 30 gg per la presentaz. dei documenti


            '--------- 23/03/2012 Alert per Documentazione Allegata!! -----------
            If cmbStato.SelectedValue = "1" Then
                If par.IfNull(tipoCausale.Value, "") <> "30" Then
                    If CType(dic_Documenti1.FindControl("documAlleg"), HiddenField).Value = 0 Then
                        MessJQuery("ATTENZIONE, indicare la documentazione da allegare alla richiesta prima di procedere!", 0, "Attenzione")

                        Exit Try
                    End If
                End If
            End If


            '------------- AGGIORNAMENTO DOMANDE_BANDO_VSA 23/03/2012 (STAVA SOPRA)
            par.cmd.CommandText = "SELECT ID,PG,FL_AUTORIZZAZIONE FROM DOMANDE_BANDO_VSA WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
            myReader = par.cmd.ExecuteReader()
            If myReader.Read() Then
                idDomanda = par.IfNull(myReader("ID"), "")

                If par.IfNull(myReader("FL_AUTORIZZAZIONE"), 0) = 0 Then
                    sStringaSql = "UPDATE DOMANDE_BANDO_VSA SET FL_RINNOVO='1',ISBAR=0,ISBARC=0,ISBARC_R=0,DISAGIO_F=0,DISAGIO_E=0,DISAGIO_A=0,REDDITO_ISEE=0,ID_STATO='2',fl_completa='1',FL_ESAMINATA='0',FL_ISTRUTTORIA_COMPLETA='0' WHERE ID=" & myReader("ID")
                    par.cmd.CommandText = sStringaSql
                    par.cmd.ExecuteNonQuery()
                End If
                If cmbStato.Text <> "2" Then
                    If Fl_Integrazione = "1" Then
                        Session.Item("CONFERMATO") = "1"
                    End If
                    If tipoRichiesta.Value = "3" Then
                        imgElabora.Visible = True
                        'lblElaborare.Visible = True
                        'lblElaborare.Text = "ELABORARE TRAMITE IL PULSANTE " & Chr(34) & "Elabora" & Chr(34) & ""
                    End If
                    'scriptblock = "<script language='javascript' type='text/javascript'>" _
                    '            & "alert('ATTENZIONE, La domanda " & myReader("PG") & " deve essere nuovamente elaborata e stampata!');" _
                    '            & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript29")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript29", scriptblock)
                    End If
                Else

                    imgElabora.Visible = False
                    MessJQuery("ATTENZIONE, Questa dichiarazione e la domanda " & myReader("PG") & " saranno cancellate!", 0, "Attenzione")
                    Exit Try
                End If

            End If
            myReader.Close()

            For Each Menu As MenuItem In MenuStampe.Items
                Menu.ChildItems.Clear()
            Next
            If Request.QueryString("GLocat") = "" Then
                SetMenuStampe(tipoRichiesta.Value)
            End If

            'Dic_Note1.CaricaLista()

            If Request.QueryString("GLocat") <> "" Then
                imgStampa.Enabled = True
                '''imgStampa.ImageUrl = "../NuoveImm/Img_Stampa.png"
                bMemorizzato = True
            Else
                imgStampa.Enabled = True
                '''imgStampa.ImageUrl = "../NuoveImm/Img_Stampa.png"
                If tipoRichiesta.Value = "3" Then
                    btnApplica.Visible = True
                    btnApplica.Enabled = True
                End If
                bMemorizzato = True
            End If

            If Session.Item("ANAGRAFE") = "1" Then
                imgAnagrafe.Attributes.Add("onclick", "javascript:window.open('../../Anagrafe.aspx?ID=" & par.criptamolto(lIdDichiarazione) & "&T=4','Anagrafe','top=0,left=0,width=600,height=400');")
            End If


            If tipoRichiesta.Value <> "3" Then
                If bMemorizzato = True Then
                    If cmbStato.SelectedValue <> "2" Then
                        'If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                        '    MessJQuery("Attenzione...Lo stato della dichiarazione deve essere COMPLETA! Memorizzazione non effettuata.", 0, "Attenzione")
                        '    Exit Sub
                        'End If
                        Session.Item("STAMPATO") = "1"
                        If Request.QueryString("GLocat") = "" Then
                            CalcolaISEEDomanda()
                        End If
                    End If
                End If
            End If

            If stampaClick.Value = "0" Then
                MessJQuery("Operazione effettuata!", 1, "Avviso")
            End If

            If Not IsNothing(par.myTrans) Then
                par.myTrans.Commit()
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            myReader = par.cmd.ExecuteReader()
            myReader.Close()

            txtModificato.Value = "0"
            salvaEsterno.Value = "0"

        Catch EX As Exception
            'Label4.Text = EX.Message
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & EX.Message)
            Response.Redirect("../../Errore.aspx", False)

        Finally
        End Try
    End Sub

    Protected Sub imgVaiDomanda_Click(sender As Object, e As System.EventArgs) Handles imgVaiDomanda.Click
        Try
            If Session.Item("LAVORAZIONE") = "1" Then
                If Fl_Integrazione = "1" Then
                    H1.Value = "1"
                End If
                If Session.Item("STAMPATO") = "0" And cmbStato.Text = "0" Then

                    MessJQuery("ATTENZIONE, La Dichiarazione deve essere completa!", 0, "Attenzione")

                    Exit Sub
                End If
                If Session.Item("STAMPATO") = "0" And cmbStato.Text = "1" Then
                    MessJQuery("ATTENZIONE, La Dichiarazione deve essere elaborata!", 0, "Attenzione")
                    Exit Sub
                End If
                If par.OracleConn.State = Data.ConnectionState.Open Then
                    par.OracleConn.Close()
                End If
                Dim idDomanda As String = ""
                If Fl_US <> "1" Then
                    par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
                    par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
                    ‘‘par.cmd.Transaction = par.myTrans
                    par.myTrans.Rollback()
                End If

                par.SettaCommand(par)
                par.cmd.CommandText = "select id from domande_bando_vsa where id_dichiarazione =" & lIdDichiarazione
                Dim lettore As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
                If lettore.Read Then
                    idDomanda = par.IfNull(lettore(0), "-1")
                End If
                lettore.Close()

                'par.myTrans.Dispose()
                par.OracleConn.Close()
                par.OracleConn.Dispose()
                HttpContext.Current.Session.Remove("TRANSAZIONE" & vIdConnessione)
                HttpContext.Current.Session.Remove(vIdConnessione)
                Session.Item("LAVORAZIONE") = "0"
                Session.Remove("STAMPATO")
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Page.Dispose()
                If Fl_Integrazione = "1" Or Fl_US = "1" Then
                    Session.Item("LAVORAZIONE") = "1"
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                Else
                    'apro domanda
                    If idDomanda <> "" Then
                        'ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();window.open('../domanda.aspx?ID=" & idDomanda & "&ID1=" & lIdDichiarazione & "&PROGR=-1&INT=0&CH=" & Request.QueryString("CH") & "','','height=550,top=200,left=350,width=670');", True)
                        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();window.open('../NuovaDomandaVSA/domandaNuova.aspx?ID=" & idDomanda & "&ID1=" & lIdDichiarazione & "&PROGR=-1&INT=0&CH=" & Request.QueryString("CH") & "');", True)
                        'Response.Write("<script>location.replace('domanda.aspx?ID=" & idDomanda & "&ID1=" & lIdDichiarazione & "&PROGR=-1&INT=0&CH=" & Request.QueryString("CH") & "','','');</script>")
                    Else
                        MessJQuery("Impossibile aprire la domanda!", 0, "Attenzione")
                    End If
                End If
            Else
                Session.Item("LAVORAZIONE") = "0"
                Page.Dispose()
                If Fl_Integrazione = "1" Or Fl_US = "1" Then
                    Session.Item("LAVORAZIONE") = "1"
                    ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "self.close();", True)
                Else
                    '''Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
                End If
            End If

        Catch EX As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub VaiADomanda() - " & EX.Message)
            Response.Redirect("../../Errore.aspx", False)
        Finally
        End Try
    End Sub

    Protected Sub btnApplica_Click(sender As Object, e As System.EventArgs) Handles btnApplica.Click
        funzioneSalva()
        If bMemorizzato = True Then
            If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                MessJQuery("Attenzione...Lo stato della dichiarazione deve essere COMPLETA!", 0, "Attenzione")
                Exit Sub
            End If

            Session.Item("STAMPATO") = "1"
            If Request.QueryString("GLocat") = "" Then
                CalcolaISEEDomanda()
            End If
            imgElabora.Visible = False
            'lblElaborare.Visible = False
            MessJQuery("Elaborazione effettuata!", 0, "Attenzione")

            btnApplica.Visible = False

        Else
            btnApplica.Enabled = False
            btnApplica.Visible = False
        End If
    End Sub

    Protected Sub imgStampa_Click(sender As Object, e As System.EventArgs) Handles imgStampa.Click
        If Request.QueryString("GLocat") <> "" Then
            stampaClick.Value = "1"
            funzioneSalva()
            If bMemorizzato = True Then
                If cmbStato.SelectedItem.Text <> "COMPLETA" Then
                    MessJQuery("Attenzione...Lo stato della dichiarazione deve essere COMPLETA!", 0, "Attenzione")
                    'Response.Write("<script>alert('Attenzione...Lo stato della dichiarazione deve essere COMPLETA!')</script>")
                    Exit Sub
                End If
                CalcolaStampa()
                Session.Item("STAMPATO") = "1"
            Else
                imgStampa.Enabled = False
                imgStampa.Text = ""
            End If
            If Request.QueryString("CH") = 1 And Session.Item("STAMPATO") = "1" Then
                Me.imgVaiDomanda.Visible = True
                Me.btnSalva.Visible = False
                'Me.MenuStampe.Visible = False
                Me.imgAnagrafe.Visible = False
            End If
        Else
            CalcolaStampa()
            If Request.QueryString("CH") = 1 And Session.Item("STAMPATO") = "1" Then
                Me.imgVaiDomanda.Visible = True
                Me.btnSalva.Visible = True
                'Me.MenuStampe.Visible = False
                Me.imgAnagrafe.Visible = False
                Me.btnApplica.Visible = False
                imgElabora.Visible = False
                'Me.lblElaborare.Visible = False
            End If
        End If

    End Sub

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
        Dim LUOGO As String
        Dim SDATA As String
        Dim LUOGO_REDDITO As String
        Dim DATA_REDDITO As String
        Dim numero As String
        Dim sStringasql As String
        Dim GIA_TITOLARI As String


        Dim i As Integer

        Try
            par.OracleConn = CType(HttpContext.Current.Session.Item("CONNESSIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleConnection)
            par.SettaCommand(par)
            par.myTrans = CType(HttpContext.Current.Session.Item("TRANSAZIONE" & vIdConnessione), Oracle.DataAccess.Client.OracleTransaction)
            ‘‘par.cmd.Transaction = par.myTrans


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

            If cmbNazioneRes.SelectedItem.Text = "ITALIA" Then
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & cmbComuneRes.SelectedItem.Text & "</I>   , " _
                & "PROVINCIA:   <I>" & cmbPrRes.SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & cmbTipoIRes.SelectedItem.Text & " " & txtIndRes.Text & "</I>   ," _
                & "N. CIVICO:   <I>" & txtCivicoRes.Text & "</I>   , CAP:   <I>" & txtCAPRes.Text & "</I>"
            Else
                DATI_ANAGRAFICI = DATI_ANAGRAFICI & "RESIDENTE A:   <I>" & cmbComuneRes.SelectedItem.Text & "</I>   , " _
                & "STATO ESTERO:   <I>" & cmbNazioneRes.SelectedItem.Text & "</I><BR>" _
                & "INDIRIZZO:   <I>" & cmbTipoIRes.SelectedItem.Text & " " & txtIndRes.Text & "</I>   ," _
                & "N. CIVICO:   <I>" & txtCivicoRes.Text & "</I>   , CAP:   <I>" & txtCAPRes.Text & "</I>"
            End If


            'If chTitolare.Checked = True Then
            '    GIA_TITOLARI = "ESISTONO"
            'Else
            '    GIA_TITOLARI = "NON ESISTONO"
            'End If
            DATI_NUCLEO = ""

            For Each row As Data.DataRow In dtComp.Rows
                DATI_NUCLEO = DATI_NUCLEO & "<TR>" _
                            & "<TD width=5%><small><small>    <center>" & i & "</center></small></small></TD>" _
                            & "<TD width=20%><small><small>   <I>" & par.IfNull(row.Item("COD_FISCALE"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=20%><small><small>   <I>" & par.IfNull(row.Item("COGNOME"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("NOME"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("DATA_NASCITA"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("GRADO_PARENTELA"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("PERC_INVAL"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("INDENNITA_ACC"), "") & "</I>   </small></small></TD>" _
                            & "<TD width=15%><small><small>   <I>" & par.IfNull(row.Item("USL"), "") & "</I>   </small></small></TD>" _
                            & "</TR>"
                i = i + 1
            Next

            SPESE_SOSTENUTE = ""


            For Each row2 As Data.DataRow In dtCompSpese.Rows
                If par.IfNull(row2.Item("importo"), 0) > 0 Then
                    SPESE_SOSTENUTE = SPESE_SOSTENUTE & "<tr>" _
                                    & "<td width=50%><small><small><CENTER>" & row2.Item("COGNOME") & " " & row2.Item("NOME") & "</CENTER></small></small></td>" _
                                    & "<td align=right width=50%><small><small><I>" & row2.Item("IMPORTO") & ",00" & "</I></small></small></td>" _
                                    & "</tr>"
                End If
            Next

            ANNO_SIT_ECONOMICA = cmbAnnoReddituale.SelectedValue

            PATRIMONIO_MOB = ""
            For Each row As Data.DataRow In dtMob.Rows
                PATRIMONIO_MOB = PATRIMONIO_MOB _
                               & "<TR>" _
                               & "<TD width=25%><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></TD>" _
                               & "<TD  width=25%><small><small>   <I>" & par.IfNull(row.Item("COD_INTERMEDIARIO"), "") & "</I>   </small></small></TD>" _
                               & "<TD width=50%><small><small>   <I>" & par.IfNull(row.Item("INTERMEDIARIO"), "") & "</I>   </small></small></TD>" _
                               & "<TD  align=right  width=50%><small><small>   <I>" & par.IfNull(row.Item("IMPORTO"), "") & ",00</I></small></small></TD>" _
                               & "</TR>"
            Next

            PATRIMONIO_IMMOB = ""
            Dim pienapropr As String = ""
            For Each row As Data.DataRow In dtImmob.Rows
                'pienapropr = par.IfNull(row.Item("TIPO_PROPR"), "")
                'If pienapropr = "1" Then
                '    pienapropr = Replace(pienapropr, pienapropr, "X")
                'End If
                PATRIMONIO_IMMOB = PATRIMONIO_IMMOB _
                                   & "<tr>" _
                                   & "<td><small><small><center>   <I>" & par.IfNull(row.Item("COGNOME"), "") & " " & par.IfNull(row.Item("NOME"), "") & "</I>   </center></small></small></td>" _
                                   & "<td><small><small>   <I>" & par.IfNull(row.Item("TIPO_IMMOB"), "") & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.IfNull(row.Item("PERC_PATR_IMMOBILIARE"), "0") & "</I>   %   </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.IfNull(row.Item("REND_CATAST_DOMINICALE"), "0") & ",00</I>    </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.IfNull(row.Item("VALORE"), "0") & ",00</I>   </p></small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.IfNull(row.Item("MUTUO"), "0") & ",00</I>   </p></small></small></td>" _
                                   & "<td><small><small><center>   <I>" & par.IfNull(row.Item("CAT_CATASTALE"), "") & "</I>   </small></small></td>" _
                                   & "<td><small><small><center>   <I>" & par.IfNull(row.Item("COMUNE"), "") & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.IfNull(row.Item("N_VANI"), "") & "</I>   </small></small></td>" _
                                   & "<td><small><small><p align=right>   <I>" & par.IfNull(row.Item("SUP_UTILE"), "") & "</I>   </small></small></td>" _
                                   & "<td><small><small><center>   <I>" & par.IfNull(row.Item("TIPO_PROPR"), "") & "</center><I></I>   </small></small></td>" _
                                   & "</tr>"
            Next

            '& "<td><small><small>   <I></I><center>" & par.RicavaTesto(CType(Dic_Patrimonio1.FindControl("listbox2"), ListBox).Items(i).Text, 79, 2) & "</center><I></I>   </small></small></td>" _


            CAT_CATASTALE = ""
            'If CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).Enabled = True Then
            '    CAT_CATASTALE = "Categoria catastale dell'immobile ad uso abitativo del nucleo : " & CType(Dic_Patrimonio1.FindControl("cmbTipoImm"), DropDownList).SelectedItem.Text & "<br/>"
            'Else
            '    CAT_CATASTALE = "Categoria catastale dell'immobile ad uso abitativo del nucleo : ---<br/>"
            'End If


            ' ''REDDITO_NUCLEO = ""
            ' ''For i = 0 To CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items.Count - 1
            ' ''    REDDITO_NUCLEO = REDDITO_NUCLEO _
            ' ''    & "<tr>" _
            ' ''    & "<td><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 35) & "</I>   </center></small></small></td>" _
            ' ''    & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 37, 15) & ",00</I>   </small></small></p></td>" _
            ' ''    & "<td><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Reddito1.FindControl("listbox1"), ListBox).Items(i).Text, 56, 15) & ",00</I>   </small></small></p></td>" _
            ' ''    & "</tr>"
            ' ''Next i




            REDDITO_NUCLEO = ""
            For Each row As Data.DataRow In dtRedd.Rows
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
            '''SDATA = CType(Dic_Sottoscrittore1.FindControl("txtdata1"), TextBox).Text


            If rdbListSott.SelectedValue = "1" Then
                dichiarante = "<img src=block_checked.gif width=10 height=10 border=1>La presente dichiarazione &egrave; resa dal dichiarante in nome e per conto del richiedente incapace <BR>   <B>DATI ANAGRAFICI DEL DICHIARANTE"
            Else
                dichiarante = " "
            End If

            If rdbListSott.SelectedValue = "1" Then

                If cmbNazioneNas.SelectedItem.Text = "ITALIA" Then
                    DATI_DICHIARANTE = "<BR>   COGNOME:   <I>" & txtCognomeS.Text & "</I>   " _
                    & ", NOME:   <I>" & txtNomeS.Text & "</I><BR>" _
                    & "NATO A:   <I>" & cmbComuneNas2.SelectedItem.Text & "</I>   , " _
                    & "PROVINCIA:   <I>" & cmbPrNasSott.SelectedItem.Text & "</I>" _
                    & "<BR>" _
                    & "DATA DI NASCITA:   <I>" & txtDataNascitaSott.Text & "</I>   , " _
                    & "pref. e n. telefonico (facoltativo):   <I>" & txtTelResSott.Text & "</I><BR>"
                Else
                    DATI_DICHIARANTE = "<BR>   COGNOME:   <I>" & txtCognomeS.Text & "</I>   " _
                                    & ", NOME:   <I>" & txtNomeS.Text & "</I><BR>" _
                                    & "STATO ESTERO:   <I>" & cmbNazioneNasSott.SelectedItem.Text & "</I>" _
                                    & "<BR>" _
                                    & "DATA DI NASCITA:   <I>" & txtDataNascitaSott.Text & "</I>   , " _
                                    & "pref. e n. telefonico (facoltativo):   <I>" & txtTelResSott.Text & "</I><BR>"

                End If
                If cmbNazioneResSott.SelectedItem.Text = "ITALIA" Then
                    DATI_DICHIARANTE = DATI_DICHIARANTE & "RESIDENTE A:   <I>" & cmbComuneResSott.SelectedItem.Text & "</I>   , " _
                    & "PROVINCIA:   <I>" & cmbPrResSott.SelectedItem.Text & "</I><BR>" _
                    & "INDIRIZZO:   <I>" & cmbTipoIResSott.SelectedItem.Text & " " & txtIndResSott.Text & "</I>   ," _
                    & "N. CIVICO:   <I>" & txtCivicoResSott.Text & "</I>   , CAP:   <I>" & txtCAPResSott.Text & "</I>"
                Else
                    DATI_DICHIARANTE = DATI_DICHIARANTE & ", " _
                    & "RESIDENTE IN:   <I>" & cmbNazioneResSott.SelectedItem.Text & "</I><BR>" _
                    & "INDIRIZZO:   <I>" & cmbTipoIResSott.SelectedItem.Text & " " & txtIndResSott.Text & "</I>   ," _
                    & "N. CIVICO:   <I>" & txtCivicoResSott.Text & "</I>   , CAP:   <I>" & txtCAPResSott.Text & "</I>"
                End If
            Else
                DATI_DICHIARANTE = "<BR></BR>"
            End If

            REDDITO_IRPEF = ""
            ' ''For i = 0 To CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items.Count - 1
            ' ''    REDDITO_IRPEF = REDDITO_IRPEF _
            ' ''    & "<tr>" _
            ' ''    & "<td width=40%><small><small><center>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 1, 50) & "</I>   </center></small></small></td>" _
            ' ''    & "<TD  width=505%><small><small><p align=right>   <I>" & par.RicavaTesto(CType(Dic_Integrazione1.FindControl("listbox1"), ListBox).Items(i).Text, 52, 8) & ",00</I>   </p></small></small></td>" _
            ' ''    & "</tr>"
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

            '''DATA_REDDITO = CType(Dic_Integrazione1.FindControl("txtdata1"), TextBox).Text


            numero = lblPG.Text & " del " & Format(Now, "dd/MM/yyyy")

            sStringasql = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title>Finestra di Stampa</title></head><BODY><UL><UL>   <NOBR></NOBR><basefont SIZE=2></UL></UL>"
            sStringasql = sStringasql & "<p align='center'><b><font size='4'>COMUNE DI MILANO</font></b><P><CENTER>DICHIARAZIONE SOSTITUTIVA DELLE CONDIZIONI ECONOMICHE DEL NUCLEO FAMILIARE PER LA RICHIESTA DI PRESTAZIONI SOCIALI AGEVOLATE</CENTER>   <BR>"
            sStringasql = sStringasql & "<CENTER>Io sottoscritto/a"
            sStringasql = sStringasql & "</CENTER>   <NOBR></NOBR>   <CENTER>ai sensi del DPR 28 dicembre 2000"
            sStringasql = sStringasql & " n. 445"
            sStringasql = sStringasql & " dichiaro quanto segue:</CENTER><BR>"
            sStringasql = sStringasql & "<center><table border=1 cellspacing=0 width=95%><tr><td><small>   <B>QUADRO A: DATI ANAGRAFICI DEL RICHIEDENTE</B><BR>"
            sStringasql = sStringasql & DATI_ANAGRAFICI & "<br><br></small></td></tr></table></center>"
            sStringasql = sStringasql & "<BR><UL>   </UL><NOBR></NOBR><center>"
            sStringasql = sStringasql & "<table border=1 cellspacing=0 width=95%><tr><td><br><small>   QUADRO B: SOGGETTI COMPONENTI IL NUCLEO FAMILIARE: richiedente"
            sStringasql = sStringasql & " componenti la famiglia anagrafica e altri soggetti considerati a carico ai fini IRPEF"
            sStringasql = sStringasql & "<BR>"
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & ""
            sStringasql = sStringasql & "<center>"
            sStringasql = sStringasql & "</small>"
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
            sStringasql = sStringasql & "<BR><UL>   <NOBR></NOBR><small>   <B>Altre informazioni sul nucleo familiare</B><BR></small>"
            sStringasql = sStringasql & "<p><small></b>"
            sStringasql = sStringasql & " <BR>"
            sStringasql = sStringasql & "</p>"
            sStringasql = sStringasql & "<table cellspacing=0 border=0 width=90%><tr><td height=18 width=35% ><small>   - nel nucleo familiare sono presenti n.   </small></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right><small>   <I>" & N_INV_100_ACC & "</I>   </p></td></tr></table></td><td width=50% ><small>   componenti con invalidit&agrave; al 100% (con indennit&agrave; di accompagnamento)   </small></td></tr><tr><td><small><CENTER>Spese effettivamente sostenute distinte per componente</small><table border=1 cellpadding=0 cellspacing=0 width=50%>   <tr><td width=50%><small><CENTER><b>A</b></CENTER></small></td><td align=right width=50%><small><CENTER><b>B</b></CENTER></small></td></tr>   <tr><td bgcolor=#C0C0C0 width=50%><CENTER><small><small>Nome</small></small></small></CENTER></td><small>   <td bgcolor=#C0C0C0 align=right width=50%><small><small><CENTER>SPESA</CENTER></small></small></td></tr><UL><UL>   <NOBR></NOBR>" & SPESE_SOSTENUTE & "</UL></UL>   <NOBR></NOBR></table></CENTER></td><td>&nbsp;</td><td>&nbsp;<BR>"
            sStringasql = sStringasql & "</small></td></tr><tr><td height=18 width=30% ><small>   - nel nucleo sono presenti n.   </small></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><small>   <small>   " & N_INV_100_NO_ACC & "</small></small></I>   </p></td></tr></table></td><td width=55% ><small>   componenti con invalidit&agrave; al 100% senza indennit&agrave; di accompagnamento<BR>"
            sStringasql = sStringasql & "</small></td></tr><tr><td height=18 width=30% ><small>   - nel nucleo sono presenti n.   </small></td><td width=5%><table border=1 bordercolor=#000000 cellspacing=0 width=100%><tr><td width=100%><p align=center><p align=right>   <I><small>   <small>" & N_INV_100_66 & "</small></small></I>   </p></td></tr></table></td><td width=55% ><small>   componenti con invalidit&agrave; inferiore al 100% e superiore al 66%<BR>"
            sStringasql = sStringasql & "</small></td></tr></table>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</td></tr></table><p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=95%><tr><td><small><br>   <B>ANNO DI RIFERIMENTO DELLA SITUAZIONE ECONOMICA*</B>   :   <I>" & ANNO_SIT_ECONOMICA & "</I>   </small><br>"
            If ChFO.Checked = True Then
                sStringasql = sStringasql & "<br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><small><BR>"
            Else
                'sStringasql = sStringasql & "<small>* In caso di domande di Riduzione canone, l'anno di riferimento reddituale è da intendersi : " & Year(Now) & "</small><br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><small><BR>"
                'posseduto alla data del 31 dicembre
                sStringasql = sStringasql & "<br></td></tr></table></center><br><center><table cellspacing=0 border=1 width=95%><tr><td><small><BR>"

            End If
            sStringasql = sStringasql & "<B>QUADRO C: SITUAZIONE PATRIMONIALE DEL NUCLEO FAMILIARE</B>   <B><UL><BR>CONSISTENZA DEL PATRIMONIO MOBILIARE</B>   <BR>&nbsp;<BR>   <BR>   DATI SUI SOGGETTI CHE GESTISCONO IL PATRIMONIO MOBILIARE</UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td></tr>   <tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>CODICE INTERMEDIARIO O GESTORE</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>INTERMEDIARIO O GESTORE (indicare se &egrave;  Banca"
            sStringasql = sStringasql & " Posta"
            sStringasql = sStringasql & " SIM"
            sStringasql = sStringasql & " SGR"
            sStringasql = sStringasql & " Impresa di investimento comunitaria o extracomunitaria"
            sStringasql = sStringasql & " Agente di cambio"
            sStringasql = sStringasql & " ecc.)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>VALORE INVESTIMENTO</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & PATRIMONIO_MOB & "<BR>"
            sStringasql = sStringasql & "</table></center>   <B><BR><UL>CONSISTENZA DEL PATRIMONIO IMMOBILIARE</B>   <BR>"
            sStringasql = sStringasql & " "
            sStringasql = sStringasql & " <br></UL>   <center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td><td><center>G</center><td><center>H</center><td><center>I</center><td><center>L</center></td><td><center>M</center></td>" _
                                      & "<tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TIPO DI PATRIMONIO  IMMOBILIARE</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>QUOTA POSSEDUTA (percentuale)</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>RENDITA CATAST.</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>VALORE AI FINI ICI (valore della quota posseduta dell'immobile"
            sStringasql = sStringasql & " come definita ai fini ICI)</small></small></center></td>   <td bgcolor=#C0C0C0><center><small><small>QUOTA CAPITALE RESIDUA DEL MUTUO (valore della quota posseduta)   </small></small></center></td><td  width=10% bgcolor=#C0C0C0><center><small><small>CAT.CATASTALE AD USO ABITATIVO DEL NUCLEO</small></small></center></td>" _
                & "<td  width=10% bgcolor=#C0C0C0><center><small><small>COMUNE</small></small></center></td><td width=10% bgcolor=#C0C0C0><center><small><small>NUM.VANI</small></small></center></td><td width=10% bgcolor=#C0C0C0><center><small><small>SUP.UTILE</small></small></center></td><td width=10% bgcolor=#C0C0C0><center><small><small>TIPO PROPRIETA'</small></small></center></td></tr>" _
                                      & "<UL><UL>   <NOBR></NOBR>" & PATRIMONIO_IMMOB & "<BR>"

            sStringasql = sStringasql & "</table></center>" _
                                      & "<br><ul>   </ul><NOBR></NOBR><center><table cellspacing=0 border=0  width=100%><tr><td width=100%><small><p align=left><I>" & CAT_CATASTALE & "</I></p></small></td></tr></table></center>   <br><br></small></td></tr></table></center><p style='page-break-before: always'>&nbsp;</p><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>QUADRO D: REDDITO DEI COMPONENTI DEL NUCLEO FAMILIARE</B>   <BR><BR>"
            sStringasql = sStringasql & "<center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td><td><center>D</center></td><td><center>E</center></td><td><center>F</center></td><td><center>G</center></td><td><center>H</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>COD. FISCALE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>DIPENDENTE</small></small><center></td><td bgcolor=#C0C0C0><center><small><small>PENSIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>PENSIONE ESENTE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>AUTONOMO</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>ASSEGN. MAN. FIGLI E ONERI</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>TOT. REDDITI</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_NUCLEO & "<BR>"
            sStringasql = sStringasql & "</table></center><br>"
            sStringasql = sStringasql & "<BR><UL>"
            sStringasql = sStringasql & "<BR></UL><HR><BR><BR>   Io sottoscritto/a"
            sStringasql = sStringasql & " consapevole delle responsabilit&agrave; penali che mi assumo"
            sStringasql = sStringasql & " ai sensi dell'articolo 76 del DPR 28 dicembre 2000"
            sStringasql = sStringasql & " n. 445"
            sStringasql = sStringasql & " per falsit&agrave; in atti e dichiarazioni mendaci"
            sStringasql = sStringasql & " dichiaro di aver compilato i Quadri:"
            sStringasql = sStringasql & "<BR><I>" & IMMAGINE_A & "A"
            sStringasql = sStringasql & "</I>   <I>" & IMMAGINE_B & "B"
            sStringasql = sStringasql & "</I>   <I>" & IMMAGINE_C & "C - patrimonio mobiliare"
            sStringasql = sStringasql & "</I>   <I>" & IMMAGINE_C1 & "C - patrimonio immobiliare"
            sStringasql = sStringasql & "</I>   <I>" & IMMAGINE_D & "D"
            sStringasql = sStringasql & "</I>   in n.   <I>1</I>   modello/i"
            sStringasql = sStringasql & ")"
            sStringasql = sStringasql & "<BR>e che quanto in essi espresso &egrave; vero ed &egrave; documentabile su richiesta delle amministrazioni competenti.   <BR>   Dichiaro"
            sStringasql = sStringasql & " altres&igrave;"
            sStringasql = sStringasql & " di essere a conoscenza che"
            sStringasql = sStringasql & " nel caso di erogazione di una prestazione agevolata"
            sStringasql = sStringasql & " potranno essere eseguiti controlli"
            sStringasql = sStringasql & " diretti ad accertare la veridicit&agrave; delle informazioni fornite ed effettuati"
            sStringasql = sStringasql & " da parte della Guardia di finanza"
            sStringasql = sStringasql & " presso gli istituti di credito e gli altri intermediari finanziari che gestiscono il patrimonio mobiliare"
            sStringasql = sStringasql & " ai sensi degli articoli 4"
            sStringasql = sStringasql & " comma 2"
            sStringasql = sStringasql & " del decreto legislativo 31 marzo 1998"
            sStringasql = sStringasql & " n. 109"
            sStringasql = sStringasql & " e 6"
            sStringasql = sStringasql & " comma 3"
            sStringasql = sStringasql & " del decreto del Presidente del Consiglio dei   Ministri 7 maggio 1999"
            sStringasql = sStringasql & " n. 221"
            sStringasql = sStringasql & " e che potranno essere effettuati controlli sulla verirdicit&agrave; della situazione familiare dichiarata e confronti dei dati reddituali e patrimoniali con i dati in possesso del sistema informativo del Ministero delle finanze.   <BR><BR>"
            sStringasql = sStringasql & "<CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I>" & LUOGO & "</I>   </center></small></td><td width=33%><small><center>"
            sStringasql = sStringasql & " li   <I>" & Format(Now, "dd/MM/yyyy") & "</I>   </center></small></td><td width=34%><center>   _______________________________   </center></td></tr><tr><td width=33% height=15><small><center>(luogo)</center></small></td><td width=33%><small><center>(data)</center></small></td><td width=34%><small><center>(firma)</center></small></td></tr></table></CENTER>"
            sStringasql = sStringasql & "<p>&nbsp;</p>"
            sStringasql = sStringasql & "<table border='1' cellpadding='0' cellspacing='0' width='100%'>"
            sStringasql = sStringasql & "<tr>"
            sStringasql = sStringasql & "<td width='100%'><font face='Arial' size='1'>DICHIARAZIONE RESA E SOTTOSCRITTA IN NOME E PER CONTO DEL RICHIEDENTE DA<BR>"
            sStringasql = sStringasql & "(COGNOME)___________________________________(NOME)___________________________________<BR>"
            sStringasql = sStringasql & "(DOC. DIRICONOSCIMENTO, N°.)________________________<BR>"
            sStringasql = sStringasql & "IN QUALITA' DI (GRADO PARENTELA)_________________________, COMPONENENTE MAGGIORENNE IL NUCLEO FAMILIARE<br>"
            sStringasql = sStringasql & "RICHIEDENTE L'ALLOGGIO, MUNITO DI DELEGA ALLEGATA AGLIA ATTI.<br>"
            sStringasql = sStringasql & "<br>"
            sStringasql = sStringasql & "L'OPERATORE______________</font></td>"
            sStringasql = sStringasql & "</tr>"
            sStringasql = sStringasql & "</table>"
            sStringasql = sStringasql & "<UL><UL><BR>"
            sStringasql = sStringasql & "</B>" & dichiarante & "<BR>"
            sStringasql = sStringasql & DATI_DICHIARANTE
            sStringasql = sStringasql & "<br><br>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</ul>"
            sStringasql = sStringasql & "</small></td></tr></table></center><BR>"
            sStringasql = sStringasql & "</center>"
            sStringasql = sStringasql & "<p><p style='page-break-before: always'>&nbsp;</p>"
            sStringasql = sStringasql & "<BR><center><table  cellspacing=0 border=1 width=95%><tr><td><small>   <B>INTEGRAZIONE ALLA SITUAZIONE REDDITUALE AI FINI ERP</B><BR>"
            sStringasql = sStringasql & "<B><BR><UL>DETRAZIONI</ul></B>   <NOBR></NOBR><center><table cellspacing=0 border=1 width=90%><tr><td><center>A</center></td><td><center>B</center></td><td><center>C</center></td></tr><tr><td bgcolor=#C0C0C0><center><small><small>NOME</small></small></center></td><td bgcolor=#C0C0C0>   <center><small><small>TIPO DETRAZIONE</small></small></center></td><td bgcolor=#C0C0C0><center><small><small>IMPORTO DETRAZIONE</small></small></center></td></tr><UL><UL>   <NOBR></NOBR>" & REDDITO_DETRAZIONI & "</UL></UL><BR>"
            sStringasql = sStringasql & "</table></center><BR>"
            sStringasql = sStringasql & "<BR><BR><CENTER><table cellspacing=0 border=0 width=80%> <tr> <td width=33%  height=20><small><center>   <I>" & LUOGO_REDDITO & "</I>   </center></small></td><td width=33%><small><center>"
            sStringasql = sStringasql & " li   <I>" & Format(Now, "dd/MM/yyyy") & "</I>   </center></small></td><td width=34%><center>   _______________________________   </center></td></tr><tr><td width=33% height=15><small><center>(luogo)</center></small></td><td width=33%><small><center>(data)</center></small></td><td width=34%><small><center>(firma)</center></small></td></tr></table></CENTER><BR>"
            sStringasql = sStringasql & "</small>"
            sStringasql = sStringasql & "</table>"
            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & " &nbsp;"
            sStringasql = sStringasql & "<p align='left'>"
            sStringasql = sStringasql & " Dic.N. " & numero & "   <p><b><font face='Arial' size='2'>Data Elaborazione: " & Format(Now, "dd/MM/yyyy") & "</font></b></p>"
            sStringasql = sStringasql & "<BR><BR>"
            sStringasql = sStringasql & "</center>"
            sStringasql = sStringasql & "<p align='center'><font face='Arial'></font></p>"
            sStringasql = sStringasql & "</BODY></html>"

            HttpContext.Current.Session.Add("DICHIARAZIONE", sStringasql)
            ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "window.open('../StampaDichiarazione.aspx','StmpDichiarazione','');", True)


            'Response.Write("<script>window.open('../FileTemp/" & nomefile & "','ExpPagScadenza','');</script>")

            sStringasql = "INSERT INTO EVENTI_DICHIARAZIONI_VSA (ID_PRATICA,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
            & "VALUES (" & lIdDichiarazione & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
            & "'F132','','I')"

            par.cmd.CommandText = sStringasql
            par.cmd.ExecuteNonQuery()


            If ChFO.Checked = True Then
                CalcolaIsee()
            End If

            par.myTrans.Commit()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)

            par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_VSA WHERE ID=" & lIdDichiarazione & " FOR UPDATE NOWAIT"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            myReader.Close()

        Catch ex As Exception
            par.myTrans.Rollback()
            par.myTrans = par.OracleConn.BeginTransaction()
            HttpContext.Current.Session.Add("TRANSAZIONE" & vIdConnessione, par.myTrans)
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " Sub CalcolaStampa() - " & ex.Message)
            Response.Redirect("../../Errore.aspx", False)
        Finally

        End Try
    End Function

    Private Function CalcolaIsee()
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




        par.cmd.CommandText = "SELECT DICHIARAZIONI_vsa.ANNO_SIT_ECONOMICA FROM DICHIARAZIONI_vsa WHERE ID=" & lIdDichiarazione
        Dim myReader13 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
        If myReader13.Read Then
            'Select Case par.IfNull(myReader13("ANNO_SIT_ECONOMICA"), 0)
            '    Case "2008"
            '        TASSO_RENDIMENTO = 4.75
            '    Case "2007"
            '        TASSO_RENDIMENTO = 4.41
            '    Case "2006"
            '        TASSO_RENDIMENTO = 3.95
            '    Case "2005"
            '        TASSO_RENDIMENTO = 3.54
            '    Case "2004"
            '        TASSO_RENDIMENTO = 4.29
            '    Case "2003"
            '        TASSO_RENDIMENTO = 4.2
            '    Case "2002"
            '        TASSO_RENDIMENTO = 5.04
            '    Case "2001"
            '        TASSO_RENDIMENTO = 5.13
            '    Case "2000"
            '        TASSO_RENDIMENTO = 5.57
            '    Case "1999"
            '        TASSO_RENDIMENTO = 5.57
            '    Case "1998"
            '        TASSO_RENDIMENTO = 5.57
            '    Case "1997"
            '        TASSO_RENDIMENTO = 5.57
            'End Select
            TASSO_RENDIMENTO = par.RicavaTasso(par.IfNull(myReader13("ANNO_SIT_ECONOMICA"), 0))
        End If
        myReader13.Close()

        par.cmd.CommandText = "SELECT * FROM COMP_NUCLEO_vsa WHERE ID_DICHIARAZIONE=" & lIdDichiarazione
        Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

        'If myReader.Read() Then

        TOT_COMPONENTI = 0
        Dim VECCHI As Integer = 0
        Dim ETA_COMPONENTE As Integer = 0

        Do While myReader.Read()
            ETA_COMPONENTE = par.RicavaEta(myReader("DATA_NASCITA"))
            If ETA_COMPONENTE >= 15 Then
                If ETA_COMPONENTE >= 18 Then
                    adulti = adulti + 1
                    If ETA_COMPONENTE > 65 Then
                        VECCHI = VECCHI + 1
                    End If
                End If
            Else
                MINORI = MINORI + 1
            End If

            par.cmd.CommandText = "SELECT * FROM COMP_DETRAZIONI_vsa WHERE id_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                DETRAZIONI = DETRAZIONI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()



            par.cmd.CommandText = "SELECT * FROM COMP_REDDITO_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("REDDITO_IRPEF"), 0) + par.IfNull(myReader1("PROV_AGRARI"), 0)
            End While
            myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM COMP_ALTRI_REDDITI_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                REDDITO_COMPLESSIVO = REDDITO_COMPLESSIVO + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()


            DETRAZIONI_FRAGILE = 0
            par.cmd.CommandText = "SELECT * FROM COMP_ELENCO_SPESE_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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



            par.cmd.CommandText = "SELECT * FROM COMP_PATR_MOB_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
            myReader1 = par.cmd.ExecuteReader()
            While myReader1.Read
                MOBILI = MOBILI + par.IfNull(myReader1("IMPORTO"), 0)
            End While
            myReader1.Close()



            par.cmd.CommandText = "SELECT * FROM COMP_PATR_IMMOB_vsa WHERE ID_COMPONENTE=" & par.IfNull(myReader("ID"), -1)
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



        par.cmd.CommandText = "SELECT * FROM DICHIARAZIONI_vsa WHERE ID=" & lIdDichiarazione
        myReader = par.cmd.ExecuteReader()
        If myReader.Read() Then
            INV_100_CON = par.IfNull(myReader("N_INV_100_CON"), 0)
            INV_100_NO = par.IfNull(myReader("N_INV_100_SENZA"), 0)
            INV_66_99 = par.IfNull(myReader("N_INV_100_66"), 0)
        End If
        myReader.Close()

        DETRAZIONI_FR = DETRAZIONI_FR + (INV_100_NO * 3000) + (INV_66_99 * 1500)
        FIGURATIVO_MOBILI = (MOBILI \ 5165) * 5165
        ISEE_ERP = REDDITO_COMPLESSIVO + ((FIGURATIVO_MOBILI * TASSO_RENDIMENTO) / 100) - DETRAZIONI - DETRAZIONI_FR
        If ISEE_ERP < 0 Then
            ISEE_ERP = 0
        End If
        ISR_ERP = ISEE_ERP
        ISEE_ERP = 0

        TOTALE_IMMOBILI = (IMMOBILI - MUTUI) + (IMMOBILI_RESIDENZA - MUTUI_RESIDENZA)

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

        par.cmd.CommandText = "update dichiarazioni_vsa set ISEE='" & ISEE_ERP _
                       & "',ISE_ERP=" & par.VirgoleInPunti(ISE_ERP) _
                       & ",ISR_ERP=" & par.VirgoleInPunti(ISR_ERP) _
                       & ",ISP_ERP=" & par.VirgoleInPunti(ISP_ERP) _
                       & ",PSE='" & PARAMETRO & "',VSE='" & VSE & "' WHERE ID=" & lIdDichiarazione
        par.cmd.ExecuteNonQuery()

        '''Label10.Visible = True
        lblDomAssociata.Visible = True

        '''Label10.Text = "ISEE"
        lblDomAssociata.Text = par.Converti(Format(ISEE_ERP, "##,##0.00"))


        'If ISEE_ERP <= 35000 Then
        '    TIPO_ALLOGGIO = 0
        'Else
        '    If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then
        '        TIPO_ALLOGGIO = 0
        '    Else
        '        If CType(Dom_Familiari1.FindControl("cmbF1"), DropDownList).SelectedIndex = 1 And CType(Dom_Familiari1.FindControl("Alert4"), Image).Visible = False Then

        '        Else
        '            ESCLUSIONE = "<li>ISEE superiore al limite ERP</li>"
        '            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET REQUISITO6='0' WHERE ID=" & lIdDomanda
        '            par.cmd.ExecuteNonQuery()

        '        End If
        '    End If
        'End If

        'If UCase(CType(Dom_Alloggio_ERP1.FindControl("txtCOMUNE"), TextBox).Text) <> "MILANO" Then
        '    ESCLUSIONE = ESCLUSIONE & "<li>Alloggio non situato a Milano</li>"
        'End If

        'If TOTALE_PATRIMONIO_ISEE_ERP > LIMITE_PATRIMONIO * 3 Then
        '    If (VECCHI = TOT_COMPONENTI Or INV_100_CON > 0 Or INV_100_NO > 0 Or INV_66_99 > 0) And CONTRATTO_30_ANNI = True Then

        '    Else
        '        ESCLUSIONE = ESCLUSIONE & "<li>Limite Patrimoniale superato</li>"
        '        par.cmd.CommandText = "UPDATE DOMANDE_BANDO_vsa SET REQUISITO6='0' WHERE ID=" & lIdDomanda
        '        par.cmd.ExecuteNonQuery()
        '    End If
        '    '***CasReq6.Text = "0"
        'End If

    End Function

    Protected Sub rdbListSott_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbListSott.SelectedIndexChanged
        ControlliSottoscritt()
    End Sub

    Private Sub ControlliSottoscritt()
        If rdbListSott.SelectedValue = "1" Then
            lblCognomeS.Visible = True
            txtCognomeS.Visible = True

            lblNomeS.Visible = True
            txtNomeS.Visible = True

            lblNatoS.Visible = True
            cmbNazioneNasSott.Visible = True

            lblPrNasSott.Visible = True
            cmbPrNasSott.Visible = True

            lblComuneNas2.Visible = True
            cmbComuneNas2.Visible = True

            lblDataNascS.Visible = True
            txtDataNascitaSott.Visible = True

            lblResidS.Visible = True
            cmbNazioneResSott.Visible = True

            lblPrResSott.Visible = True
            cmbPrResSott.Visible = True

            lblComuneResSott.Visible = True
            cmbComuneResSott.Visible = True

            lblIndirSott.Visible = True
            cmbTipoIResSott.Visible = True
            txtIndResSott.Visible = True

            lblCivicoSott.Visible = True
            txtCivicoResSott.Visible = True

            lblCapSot.Visible = True
            txtCAPResSott.Visible = True

            lblTelSot.Visible = True
            txtTelResSott.Visible = True
        Else
            lblCognomeS.Visible = False
            txtCognomeS.Visible = False

            lblNomeS.Visible = False
            txtNomeS.Visible = False

            lblNatoS.Visible = False
            cmbNazioneNasSott.Visible = False

            lblPrNasSott.Visible = False
            cmbPrNasSott.Visible = False

            lblComuneNas2.Visible = False
            cmbComuneNas2.Visible = False

            lblDataNascS.Visible = False
            txtDataNascitaSott.Visible = False

            lblResidS.Visible = False
            cmbNazioneResSott.Visible = False

            lblPrResSott.Visible = False
            cmbPrResSott.Visible = False

            lblComuneResSott.Visible = False
            cmbComuneResSott.Visible = False

            lblIndirSott.Visible = False
            cmbTipoIResSott.Visible = False
            txtIndResSott.Visible = False

            lblCivicoSott.Visible = False
            txtCivicoResSott.Visible = False

            lblCapSot.Visible = False
            txtCAPResSott.Visible = False

            lblTelSot.Visible = False
            txtTelResSott.Visible = False
        End If
    End Sub

    Protected Sub btnfunzesci2_Click(sender As Object, e As System.EventArgs) Handles btnfunzesci2.Click
        txtModificato.Value = "0"
        opUscita()
    End Sub

    Protected Sub btnfunzelimina_Click(sender As Object, e As System.EventArgs) Handles btnfunzelimina.Click
        confCanc.Value = 1
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 1;", True)

        Select Case provenienza.Value

            Case "nucleo"
                'EliminaComponente()
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

            Case Else
                confCanc.Value = 0
                '  ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)

        End Select

        confCanc.Value = 0
        ScriptManager.RegisterStartupScript(Page, GetType(Page), "MyScriptKey", "document.getElementById('confCanc').value = 0;", True)


    End Sub
End Class
